using System;
using System.Collections.Specialized;
using System.IO;

namespace LiteServer.Http.Features
{
    public interface IHttpRequestFeature
    {
        public Uri Uri { get; }
        public Stream Body { get; }
        public NameValueCollection Headers { get; }

    }
}