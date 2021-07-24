using System;
using System.Collections.Specialized;
using System.IO;
using LiteServer.Http.Features;

namespace LiteServer.Http.Tests.Utilities
{
    public static class HttpRequestFeatureTestFactory
    {
        public static IHttpRequestFeature SomeRequest()
        {
            var headers = new NameValueCollection();
            headers.Add("testKey", "testValue");
            return new HttpRequestTest(new Uri("http://testUri.com"), Stream.Null, headers);
        }
    }
}