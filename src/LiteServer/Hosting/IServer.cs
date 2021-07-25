using LiteServer.Handlers;
using System;
using System.Threading.Tasks;

namespace LiteServer.Hosting
{
    public interface IServer : IDisposable
    {
        Task StartAsync(HandlerDelegate handler);
        Task StopAsync();
        void Stop();
    }
}