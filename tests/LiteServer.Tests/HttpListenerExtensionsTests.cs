using FluentAssertions;
using LiteServer.Extensions;
using System.Linq;
using System.Net;
using Xunit;

namespace LiteServer.Tests
{
    public class HttpListenerExtensionsTests
    {
        [Fact]
        public void adding_prefixing_to_http_listener()
        {
            var httpListener = new HttpListener();

            const string httpListenerPrefix = "http://localhost:5000/";
            httpListener.TryAddPrefix(httpListenerPrefix);

            var prefix = httpListener.Prefixes.First();
            prefix.Should().Be(httpListenerPrefix);
        }
    }
}