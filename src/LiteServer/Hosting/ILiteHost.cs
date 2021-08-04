using System.Threading;
using System.Threading.Tasks;

namespace LiteServer.Hosting
{
    public interface ILiteHost
    {
        Task StartAsync(CancellationToken token = default);
        Task StopAsync();
        void Stop();
    }
}