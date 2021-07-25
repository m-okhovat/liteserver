using LiteServer.AppBuilders;
using LiteServer.Hosting;
using LiteServer.Listener.Hosting;
using System;

namespace LiteServer.ServerBuilders
{
    public interface ILiteServerBuilder
    {
        ILiteServerBuilder Configure(Action<ILiteApplicationBuilder> config);
        ILiteServerBuilder UseServer(IServer server);
        ILiteHost Build();
    }
}