using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LiteServer.Http.Extensions;
using LiteServer.Http.Features;
using LiteServer.Http.HttpContext;
using LiteServer.Listener.Extensions;
using LiteServer.Listener.Handlers;

namespace LiteServer.Listener.HttpListeners
{
    public class HttpServer : HttpServerBase
    {
        private readonly HttpListener _httpListener = new HttpListener();
        public HttpServer(IEnumerable<string> paths)
        {
            if (paths == null || !paths.Any()) throw new ArgumentNullException(nameof(paths));

            foreach (var path in paths)
            {
                _httpListener.TryAddPrefix(path);
            }
        }

        public HttpServer()
        {
            _httpListener.Prefixes.Add("http://localhost:5000/");
        }

        public override Task StartServer(HandlerDelegate handler, CancellationToken cancel)
        {
            _httpListener.Start();
            foreach (var httpListenerPrefix in _httpListener.Prefixes)
            {
                Console.WriteLine($"{nameof(HttpServer)} Server started listening on {httpListenerPrefix}");
            }

            return Task.Factory.StartNew(() =>
            {
                try
                {
                    Thread.CurrentThread.Name = "HttpListener Server";

                    while (!cancel.IsCancellationRequested)
                    {
                        var getContextAsync = _httpListener.GetContextAsync();
                        getContextAsync.Wait(cancel);

                        var httpListenerContext = getContextAsync.Result;
                        var httpListenerFeature = new HttpListenerFeature(httpListenerContext);
                        var featureCollection = new FeatureCollection()
                            .Set<IHttpRequestFeature>(httpListenerFeature)
                            .Set<IHttpResponseFeature>(httpListenerFeature);
                        var httpContext = new LiteHttpContext(featureCollection);

                        Task.Factory.StartNew(async () =>
                        {
                            var response = httpListenerContext.Response;
                            try
                            {
                                var httpListenerRequest = httpListenerContext.Request;
                                Console.WriteLine(
                                    $"[{DateTime.Now}] {httpListenerRequest.HttpMethod} Http request received at {httpListenerRequest.LocalEndPoint} {httpListenerRequest.Url}");

                                await handler(httpContext);
                            }
                            catch (Exception e) when (!(e is OperationCanceledException))
                            {
                                if (!_httpListener.IsListening)
                                    return;
                                Trace.WriteLine($"Error in {nameof(HttpServer)}: {e}");

                                try
                                {
                                    response.StatusCode = 500;
                                }
                                catch
                                {
                                    // Might be too late in request processing to set response code, so just ignore.
                                }
                            }
                            catch (EntryPointNotFoundException e)
                            {
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                            }
                            finally
                            {
                                response.Close();
                            }
                        }, cancel);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    _httpListener.Stop();
                    _httpListener.Close();
                    foreach (var httpListenerPrefix in _httpListener.Prefixes)
                    {
                        Console.WriteLine(
                            $"{nameof(HttpServer)} Server stopped listening on {httpListenerPrefix}");
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}