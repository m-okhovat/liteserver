using LiteServer.Http.Features;
using System;
using System.Collections.Specialized;
using System.IO;

namespace LiteServer.Http.Tests.Utilities
{

    public class HttpFeatureRequestTest : IHttpRequestFeature
    {
        public HttpFeatureRequestTest(Uri uri = default, Stream body = default, NameValueCollection headers = default)
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
