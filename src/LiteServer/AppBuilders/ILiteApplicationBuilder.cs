using System;
using LiteServer.Listener.Handlers;

namespace LiteServer.AppBuilders
{
    public interface ILiteApplicationBuilder
    {
        ILiteApplicationBuilder Use(Func<HandlerDelegate, HandlerDelegate> middleware);
        HandlerDelegate Build();
    }
}