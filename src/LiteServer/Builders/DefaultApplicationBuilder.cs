using LiteServer.Listener.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiteServer.Builders
{
    public class DefaultApplicationBuilder : ILiteApplicationBuilder
    {
        private List<Func<HandlerDelegate, HandlerDelegate>> _middlewares =
            new List<Func<HandlerDelegate, HandlerDelegate>>();


        public ILiteApplicationBuilder Use(Func<HandlerDelegate, HandlerDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        public HandlerDelegate Build()
        {
            _middlewares.Reverse();
            HandlerDelegate next = context =>
            {
                context.Response.StatusCode = 404;
                return Task.CompletedTask;
            };

            foreach (var middleware in _middlewares)
            {
                next = middleware(next);
            }

            return next;
        }
    }
}