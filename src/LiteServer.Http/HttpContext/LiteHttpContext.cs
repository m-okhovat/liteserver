using LiteServer.Http.Features;

namespace LiteServer.Http.HttpContext
{
    public class LiteHttpContext
    {
        public LiteHttpContext(IFeatureCollection features)
        {
            Request = new LiteHttpRequest(features);
            Response = new LiteHttpResponse(features);
        }

        public LiteHttpRequest Request { get; private set; }
        public LiteHttpResponse Response { get; private set; }
    }
}