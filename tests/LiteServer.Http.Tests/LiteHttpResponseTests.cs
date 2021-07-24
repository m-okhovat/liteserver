using FluentAssertions;
using LiteServer.Http.Extensions;
using LiteServer.Http.Features;
using LiteServer.Http.HttpContext;
using LiteServer.Http.Tests.Utilities;
using System.Collections.Specialized;
using System.IO;
using Xunit;

namespace LiteServer.Http.Tests
{
    public class LiteHttpResponseTests
    {
        [Fact]
        public void creating_http_response_message()
        {
            var statusCode = 200;
            var body = Stream.Null;
            var headers = new NameValueCollection();
            headers.Add("testName", "testValue");
            var featureCollection =
                new FeatureCollection().Set<IHttpResponseFeature>(new HttpResponseTest(headers, body, statusCode));
            var liteHttpResponse = new LiteHttpResponse(featureCollection);

            liteHttpResponse.StatusCode.Should().Be(statusCode);
            liteHttpResponse.Response.Should().Be(body);
            liteHttpResponse.Headers.Should().BeEquivalentTo(headers);
        }
    }
}