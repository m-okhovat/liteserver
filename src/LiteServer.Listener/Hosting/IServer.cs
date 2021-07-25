using System;
using System.Threading.Tasks;
using LiteServer.Listener.Handlers;

namespace LiteServer.Listener.Hosting
{
    public interface IServer : IDisposable
    {
        Task StartAsync(HandlerDelegate handler);
        Task StopAsync();
        void Stop();
    }
}