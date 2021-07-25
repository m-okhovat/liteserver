using System;
using System.Collections.Generic;
using LiteServer.AppBuilders;

namespace LiteServer.Routing
{
    public class EndpointRouteBuilder : IEndpointRouteBuilder
    {
        public ILiteApplicationBuilder LiteApplicationBuilder { get; }
        public EndpointRouteBuilder(ILiteApplicationBuilder liteApplicationBuilder)
        {
            LiteApplicationBuilder = liteApplicationBuilder ?? throw new ArgumentNullException(nameof(liteApplicationBuilder));
            DataSources = new List<Endpoint>();
        }

        public ICollection<Endpoint> DataSources { get; }
    }
}