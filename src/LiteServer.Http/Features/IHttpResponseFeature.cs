using System.Collections.Specialized;
using System.IO;

namespace LiteServer.Http.Features
{
    public interface IHttpResponseFeature
    {
        public NameValueCollection Headers { get; }
        public Stream Response { get; }
        public int StatusCode { get; set; }
    }
}