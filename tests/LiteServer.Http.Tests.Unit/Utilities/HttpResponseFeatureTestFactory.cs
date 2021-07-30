using System.Collections.Specialized;
using System.IO;
using LiteServer.Http.Features;

namespace LiteServer.Http.Tests.Utilities
{
    public static class HttpResponseFeatureTestFactory
    {
        public static IHttpResponseFeature SomeResponse()
        {
            var nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("testKey", "testValue");
            return new HttpResponseTest(nameValueCollection, Stream.Null, 200);
        }
    }
}