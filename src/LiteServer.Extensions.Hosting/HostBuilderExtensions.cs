using LiteServer.AppBuilders;
using LiteServer.Hosting;
using LiteServer.ServerBuilders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiteServer.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureLiteServerDefaults(this IHostBuilder hostBuilder,
            Action<ILiteApplicationBuilder> configure, IEnumerable<Action<BasePathBuilder>> pathConfigurations)
        {
            if (pathConfigurations == null || !pathConfigurations.Any())
            {
                pathConfigurations = new List<Action<BasePathBuilder>>()
                {
                    builder => builder
                        .WithHostName("localhost")
                        .WithPort(5000)
                };
            }

            var liteServerBuilder = new LiteServerBuilder();
            liteServerBuilder.Configure(configure);
            liteServerBuilder.UseHttpListenerServer(pathConfigurations);
            var liteServerService = new LiteServerService(liteServerBuilder.Build());

            hostBuilder.ConfigureServices((context, collection) =>
            {
                collection.AddHostedService((sp => liteServerService));
            });
            return hostBuilder;
        }

        public static IHostBuilder ConfigureLiteServerDefaults(this IHostBuilder hostBuilder,
            Action<ILiteApplicationBuilder> configure, params string[] paths)
        {
            if (paths == null || !paths.Any())
                paths = new[] { "http://localhost:5000/" };

            var liteServerBuilder = new LiteServerBuilder();
            liteServerBuilder.Configure(configure);
            liteServerBuilder.UseHttpListenerServer(paths);
            var liteServerService = new LiteServerService(liteServerBuilder.Build());

            hostBuilder.ConfigureServices((context, collection) =>
            {
                collection.AddHostedService((sp => liteServerService));
            });

            return hostBuilder;
        }
    }
}
