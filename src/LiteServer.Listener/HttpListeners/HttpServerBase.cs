using System;
using System.Threading;
using System.Threading.Tasks;
using LiteServer.Listener.Handlers;
using LiteServer.Listener.Hosting;

namespace LiteServer.Listener.HttpListeners
{
    public abstract class HttpServerBase : IServer
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private Task _serverTask;

        public Task StartAsync(HandlerDelegate handler)
        {
            if (_serverTask != null)
                throw new InvalidOperationException("Server has been started already.");

            _serverTask = StartServer(handler, _cts.Token);
            return _serverTask;
        }

        public async Task StopAsync()
        {
            _cts?.Cancel();

            try
            {
                if (_serverTask == null)
                    return; // Never started.

                // This will re-throw any exception that was caught on the StartServerAsync thread.
                await _serverTask;
            }
            catch (OperationCanceledException)
            {
                //since it can easily get thrown by whatever checks the CancellationToken.
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
            }
        }

        public void Stop()
        {
            StopAsync().GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            Stop();
        }

        public abstract Task StartServer(HandlerDelegate handler, CancellationToken cancel);
    }
}