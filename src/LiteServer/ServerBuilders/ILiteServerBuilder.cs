using LiteServer.AppBuilders;
using LiteServer.Hosting;
using LiteServer.Listener.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LiteServer.ServerBuilders
{
    public interface ILiteServerBuilder
    {
        ILiteServerBuilder Configure(Action<ILiteApplicationBuilder> config);
        ILiteServerBuilder ConfigureContainer(Action<IServiceCollection> config);

        ILiteServerBuilder UseServer(IServer server);
        ILiteHost Build();
    }
}