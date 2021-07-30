using FluentAssertions;
using LiteServer.Http.Extensions;
using LiteServer.Http.Features;
using LiteServer.Http.HttpContext;
using LiteServer.Http.Tests.Utilities;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LiteServer.Http.Tests
{
    public class HttpResponseExtensionsTests
    {
        [Fact]
        public async Task writing_on_http_response_body()
        {
            var featureCollection = new FeatureCollection()
                .Set<IHttpResponseFeature>(new HttpResponseTest(new NameValueCollection(), new MemoryStream(), 0));

            var httpResponse = new LiteHttpResponse(featureCollection);
            var content = "some content for testing";
            await httpResponse.WriteAsync(content);

            var result = MemoryStreamContent(httpResponse.Body);
            result.Should().Be(content);
        }

        private static string MemoryStreamContent(Stream body)
        {
            return Encoding.UTF8.GetString(((MemoryStream)body).ToArray());
        }
    }
}