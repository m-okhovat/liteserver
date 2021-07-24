using FluentAssertions;
using LiteServer.Http.Extensions;
using LiteServer.Http.Features;
using LiteServer.Http.HttpContext;
using LiteServer.Http.Tests.Utilities;
using System;
using System.Collections.Specialized;
using System.IO;
using Xunit;

namespace LiteServer.Http.Tests
{
    public class LiteHttpRequestTests
    {
        [Fact]
        public void instantiating_LiteHttpRequest()
        {
            var featureCollection = new FeatureCollection();
            var headers = new NameValueCollection();
            headers.Add("testKey", "testValue");
            var uri = new Uri("http://testurl.com");
            var body = Stream.Null;
            featureCollection.Set<IHttpRequestFeature>(new HttpFeatureRequestTest(uri, body, headers));

            var liteHttpRequest = new LiteHttpRequest(featureCollection);

            liteHttpRequest.Uri.Should().Be(uri);
            liteHttpRequest.Body.Should().Be(body);
            liteHttpRequest.Headers.Should().BeEquivalentTo(headers);
        }
    }
}