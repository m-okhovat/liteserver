using System.Collections.Specialized;
using System.IO;
using LiteServer.Http.Extensions;
using LiteServer.Http.Features;

namespace LiteServer.Http.HttpContext
{
    public class LiteHttpResponse
    {
        private readonly IHttpResponseFeature _responseFeature;
        public LiteHttpResponse(IFeatureCollection featureCollection)
        {
            _responseFeature = featureCollection.Get<IHttpResponseFeature>();
        }

        public int StatusCode => _responseFeature.StatusCode;
        public Stream Response => _responseFeature.Response;
        public NameValueCollection Headers => _responseFeature.Headers;
    }
}