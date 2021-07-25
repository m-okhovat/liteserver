using LiteServer.Listener.Handlers;
using System;

namespace LiteServer.Builders
{
    public interface ILiteApplicationBuilder
    {
        ILiteApplicationBuilder Use(Func<HandlerDelegate, HandlerDelegate> middleware);
        HandlerDelegate Build();
    }
}