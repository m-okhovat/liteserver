using LiteServer.Http.Extensions;
using LiteServer.Http.Features;
using System;
using System.Collections.Specialized;
using System.IO;

namespace LiteServer.Http.HttpContext
{
    public class LiteHttpRequest
    {
        private readonly IHttpRequestFeature _requestFeature;
        public LiteHttpRequest(IFeatureCollection features)
        {
            _requestFeature = features.Get<IHttpRequestFeature>();
        }

        public Uri Uri => _requestFeature.Uri;
        public Stream Body => _requestFeature.Body;
        public NameValueCollection Headers => _requestFeature.Headers;
    }

}