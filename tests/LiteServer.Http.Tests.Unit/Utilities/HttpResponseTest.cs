using LiteServer.Http.Features;
using System.Collections.Specialized;
using System.IO;

namespace LiteServer.Http.Tests.Utilities
{
    public class HttpResponseTest : IHttpResponseFeature
    {
        public HttpResponseTest(NameValueCollection headers = default, Stream body = default, int statusCode = default)
        {
            Headers = headers;
            Body = body;
            StatusCode = statusCode;
        }
        public NameValueCollection Headers { get; private set; }
        public Stream Body { get; private set; }
        public int StatusCode { get; set; }
    }
}