using FluentAssertions;
using LiteServer.Http.Extensions;
using LiteServer.Http.Features;
using LiteServer.Http.HttpContext;
using LiteServer.Http.Tests.Unit.Utilities;
using Xunit;

namespace LiteServer.Http.Tests.Unit
{
    public class HttpContextTests
    {
        [Fact]
        public void httpContext_consists_of_httpResponse_and_httpRequest()
        {
            var httpRequestFeature = HttpRequestFeatureTestFactory.SomeRequest();
            var httpResponseFeature = HttpResponseFeatureTestFactory.SomeResponse();
            var features = new FeatureCollection()
                .Set<IHttpRequestFeature>(httpRequestFeature)
                .Set<IHttpResponseFeature>(httpResponseFeature);

            var liteHttpContext = new LiteHttpContext(features);

            liteHttpContext.Request.Should().BeEquivalentTo(httpRequestFeature);
            liteHttpContext.Response.Should().BeEquivalentTo(httpResponseFeature);
        }
    }



}