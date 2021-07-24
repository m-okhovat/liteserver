using LiteServer.Http.Features;
using System.Collections.Specialized;
using System.IO;

namespace LiteServer.Http.Tests.Utilities
{
    public class HttpResponseTest : IHttpResponseFeature
    {
        public HttpResponseTest(NameValueCollection headers = default, Stream response = default, int statusCode = default)
        {
            Headers = headers;
            Response = response;
            StatusCode = statusCode;
        }
        public NameValueCollection Headers { get; private set; }
        public Stream Response { get; private set; }
        public int StatusCode { get; set; }
    }
}