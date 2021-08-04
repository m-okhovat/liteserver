using LiteServer.Listener.Handlers;
using LiteServer.Listener.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace LiteServer.Hosting
{
    internal class LiteServer : ILiteHost
    {
        private readonly IServer _server;
        private readonly HandlerDelegate _handler;
        private readonly IServiceCollection _services;
        public LiteServer(IServer server, HandlerDelegate handler, IServiceCollection services)
        {
            _server = server;
            _handler = handler;
            _services = services;
        }

        public Task StartAsync(CancellationToken token = default)
        {
            var provider = _services.BuildServiceProvider();
            var hostedServices = provider.GetServices<IHostedService>();
            foreach (var hostedService in hostedServices)
            {
                hostedService.StartAsync(token);
            }

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