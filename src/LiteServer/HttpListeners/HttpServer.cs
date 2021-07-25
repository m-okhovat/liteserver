using LiteServer.Extensions;
using LiteServer.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace LiteServer.HttpListeners
{
    public class HttpServer : HttpServerBase
    {
        private HttpListener _httpListener;
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
            throw new NotImplementedException();
        }
    }
}