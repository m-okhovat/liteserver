using System.Threading.Tasks;

namespace LiteServer.Hosting
{
    public interface ILiteHost
    {
        Task StartAsync();
        Task StopAsync();
        void Stop();
    }
}