using LiteServer.Hosting;
using LiteServer.Listener.HttpListeners;
using LiteServer.ServerBuilders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiteServer.Extensions
{
    public static class LiteServerExtensions
    {
        public static ILiteServerBuilder UseHttpListenerServer(this ILiteServerBuilder builder)
        {
            return builder.UseServer(new HttpServer());
        }

        public static ILiteServerBuilder UseHttpListenerServer(this ILiteServerBuilder builder,
            IEnumerable<Action<BasePathBuilder>> configurations)
        {
            var urls = new List<string>();
            foreach (var action in configurations)
            {
                var basePathBuilder = new BasePathBuilder();
                action(basePathBuilder);
                var basePath = basePathBuilder.Build();
                urls.Add(basePath.ToString());
            }

            return builder.UseServer(new HttpServer(urls));
        }

        public static ILiteServerBuilder UseHttpListenerServer(this ILiteServerBuilder builder,
            IEnumerable<string> paths)
        {
            if (paths == null || !paths.Any()) throw new ArgumentNullException(nameof(paths));

            return builder.UseServer(new HttpServer(paths));
        }

        public static ILiteServerBuilder UseHttpListenerServer(this ILiteServerBuilder builder,
            params string[] paths)
        {
            if (paths == null || !paths.Any()) throw new ArgumentNullException(nameof(paths));

            return builder.UseServer(new HttpServer(paths));
        }
    }
}