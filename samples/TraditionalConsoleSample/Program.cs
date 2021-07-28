using LiteServer.Extensions;
using LiteServer.Hosting;
using LiteServer.Http.Extensions;
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
            liteServerBuilder.Configure(builder =>
                builder.UseEndpoints(routeBuilder =>
                {
                    routeBuilder.Map("/Hello", context => context.Response.WriteAsync("Hello world"));
                }));
            liteServerBuilder.UseHttpListenerServer(pathConfigurations);

            liteServerBuilder.Build().StartAsync();

            Console.ReadKey();

        }
    }
}
