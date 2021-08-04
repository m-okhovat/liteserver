using LiteServer.Listener.Handlers;
using System;

namespace LiteServer.AppBuilders
{
    public interface ILiteApplicationBuilder
    {
        IServiceProvider ServiceProvider { get; }

        ILiteApplicationBuilder Use(Func<HandlerDelegate, HandlerDelegate> middleware);
        HandlerDelegate Build();
    }
}