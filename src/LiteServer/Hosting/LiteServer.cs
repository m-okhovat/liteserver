using LiteServer.Listener.Handlers;
using LiteServer.Listener.Hosting;
using System.Threading.Tasks;

namespace LiteServer.Hosting
{
    public class LiteServer : ILiteHost
    {
        private readonly IServer _server;
        private readonly HandlerDelegate _handler;

        public LiteServer(IServer server, HandlerDelegate handler)
        {
            _server = server;
            _handler = handler;
        }

        public Task StartAsync()
        {
            return _server.StartAsync(_handler);
        }

        public Task StopAsync()
        {
            return _server.StopAsync();
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}