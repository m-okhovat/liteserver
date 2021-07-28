using System.Collections.Specialized;
using System.IO;

namespace LiteServer.Http.Features
{
    public interface IHttpResponseFeature
    {
         NameValueCollection Headers { get; }
         Stream Body { get; }
         int StatusCode { get; set; }
    }
}