using System.Collections.Specialized;
using System.IO;

namespace LiteServer.Http.Features
{
    public interface IHttpResponseFeature
    {
        public NameValueCollection Headers { get; }
        public Stream Body { get; }
        public int StatusCode { get; set; }
    }
}