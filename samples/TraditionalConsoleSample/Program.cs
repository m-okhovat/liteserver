using LiteServer.Extensions;
using LiteServer.Hosting;
using LiteServer.Http.Extensions;
using LiteServer.Listener.Handlers;
using LiteServer.ServerBuilders;
using System;
using System.Collections.Generic;

namespace TraditionalConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var liteServerBuilder = new LiteServerBuilder();
            var pathConfigurations = new List<Action<BasePathBuilder>>()
            {
                builder => builder
                    .WithHostName("localhost")
                    .WithPort(5000)
            };

            var liteHost = liteServerBuilder.Configure(builder =>
                {
                    builder.Use(FirstMiddleware) //here we defined the middleware pipeline
                        .Use(SecondMiddleware);

                    builder.UseEndpoints(routeBuilder => // here we defined routing
                    {
                        routeBuilder.Map("/Hello", context => context.Response.WriteAsync("Hello world"));
                    });
                })
                .UseHttpListenerServer(pathConfigurations)
                .Build();

            liteHost.StartAsync();

            Console.ReadKey();

        }

        static HandlerDelegate FirstMiddleware(HandlerDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("first middleware says hello! =>");
                await next(context);
            };
        }

        static HandlerDelegate SecondMiddleware(HandlerDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("second middleware says hello =>");
                await next(context);
            };
        }
    }
}
