using FluentAssertions;
using LiteServer.Http.Extensions;
using LiteServer.Http.Features;
using LiteServer.Http.Tests.Utilities;
using System.Collections.Specialized;
using System.IO;
using Xunit;

namespace LiteServer.Http.Tests
{
    public class FeatureExtensionTests
    {
        [Fact]
        public void get_http_requests_from_feature_collections()
        {
            var featureCollection = new FeatureCollection
            {
                {typeof(IHttpRequestFeature), new HttpRequestTest()},
                {typeof(IHttpResponseFeature), new HttpResponseTest()}
            };

            var request = featureCollection.Get<IHttpRequestFeature>();

            request.Should().NotBeNull();
            request.Should().BeAssignableTo<IHttpRequestFeature>();
        }

        [Fact]
        public void set_http_response_message_on_feature_collection()
        {
            var featureCollection = new FeatureCollection();
            featureCollection
                .Set<IHttpRequestFeature>(new HttpRequestTest())
                .Set<IHttpResponseFeature>(new HttpResponseTest());

            var httpResponseFeature = featureCollection.Get<IHttpResponseFeature>();
            var httpRequestFeature = featureCollection.Get<IHttpRequestFeature>();

            httpRequestFeature.Should().NotBeNull();
            httpResponseFeature.Should().NotBeNull();
        }

        [Fact]
        public void setting_more_than_one_object_per_type_on_featureCollection_only_add_the_last_one()
        {
            var featureCollection = new FeatureCollection();

            var body = Stream.Null;
            var headers = new NameValueCollection();
            featureCollection.Set<IHttpResponseFeature>(new HttpResponseTest(headers, body, 200))
                             .Set<IHttpResponseFeature>(new HttpResponseTest(headers, body, 400))
                             .Set<IHttpResponseFeature>(new HttpResponseTest(headers, body, 404));

            var httpResponseFeature = featureCollection.Get<IHttpResponseFeature>();
            httpResponseFeature.StatusCode.Should().Be(404);
        }
    }
}
