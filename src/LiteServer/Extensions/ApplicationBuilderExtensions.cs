using LiteServer.AppBuilders;
using LiteServer.Routing;
using System;

namespace LiteServer.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static ILiteApplicationBuilder UseEndpoints(this ILiteApplicationBuilder appBuilder,
            Action<IEndpointRouteBuilder> configure)
        {
            var endpointRouteBuilder = new EndpointRouteBuilder(appBuilder);
            configure(endpointRouteBuilder);
            appBuilder.Use(handler => context =>
            {
                var requestUrl = context.Request.Uri;
                var uri = requestUrl.AbsolutePath;
                var endpoint = endpointRouteBuilder.GetEndpoint(uri);
                return endpoint.HandlerDelegate(context);
            });
            return appBuilder;
        }
    }
}