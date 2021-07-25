using LiteServer.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace LiteServer.Extensions.Hosting
{
    public class LiteServerService : BackgroundService
    {
        private readonly ILiteHost _host;

        public LiteServerService(ILiteHost host)
        {
            _host = host;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(() => _host.StartAsync(), TaskCreationOptions.LongRunning);
        }
    }
}