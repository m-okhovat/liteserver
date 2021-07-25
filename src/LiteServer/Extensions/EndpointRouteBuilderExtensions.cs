using System;
using System.Linq;
using LiteServer.Listener.Handlers;
using LiteServer.Routing;

namespace LiteServer.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {

        public static IEndpointRouteBuilder Map(
            this IEndpointRouteBuilder endpointsRouteBuilder,
            string pattern,
            HandlerDelegate handler
            , string displayName = "")
        {
            if (endpointsRouteBuilder == null)
            {
                throw new ArgumentNullException(nameof(endpointsRouteBuilder));
            }

            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            endpointsRouteBuilder.DataSources.Add(new Endpoint(handler, pattern, displayName));

            return endpointsRouteBuilder;

        }

        public static Endpoint GetEndpoint(this IEndpointRouteBuilder builder, string uriPattern)
        {
            var endpoint = builder.DataSources.FirstOrDefault(x => x.Pattern.Equals(uriPattern, StringComparison.OrdinalIgnoreCase));
            if (endpoint == null)
                throw new EntryPointNotFoundException(); //Todo : return a 404 endpoint
            return endpoint;
        }
    }
}