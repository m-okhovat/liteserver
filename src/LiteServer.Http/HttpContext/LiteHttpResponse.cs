using LiteServer.Http.Extensions;
using LiteServer.Http.Features;
using System.Collections.Specialized;
using System.IO;

namespace LiteServer.Http.HttpContext
{
    public class LiteHttpResponse
    {
        private readonly IHttpResponseFeature _responseFeature;
        public LiteHttpResponse(IFeatureCollection featureCollection)
        {
            _responseFeature = featureCollection.Get<IHttpResponseFeature>();
        }

        public int StatusCode
        {
            get => _responseFeature.StatusCode;
            set => _responseFeature.StatusCode = value;
        }

        public Stream Body => _responseFeature.Body;
        public NameValueCollection Headers => _responseFeature.Headers;
    }
}