using System;
using System.Collections.Specialized;
using System.IO;

namespace LiteServer.Http.Features
{
    public interface IHttpRequestFeature
    {
        Uri Uri { get; }
        Stream Body { get; }
        NameValueCollection Headers { get; }

    }
}