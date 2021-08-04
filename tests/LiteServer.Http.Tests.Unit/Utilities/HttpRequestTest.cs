using System;
using System.Collections.Specialized;
using System.IO;
using LiteServer.Http.Features;

namespace LiteServer.Http.Tests.Unit.Utilities
{

    public class HttpRequestTest : IHttpRequestFeature
    {
        public HttpRequestTest(Uri uri = default, Stream body = default, NameValueCollection headers = default)
        {
            Uri = uri;
            Body = body;
            Headers = headers;
        }

        public Uri Uri { get; private set; }
        public Stream Body { get; private set; }
        public NameValueCollection Headers { get; private set; }

    }

}
