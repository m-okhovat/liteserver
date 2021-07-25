using System.Collections.Generic;

namespace LiteServer.Routing
{
    public interface IEndpointRouteBuilder
    {

        /// <summary>
        /// Gets the endpoint data sources configured in the builder.
        /// </summary>
        ICollection<Endpoint> DataSources { get; }
    }
}