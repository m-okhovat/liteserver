using LiteServer.Http.HttpContext;
using System.Text;
using System.Threading.Tasks;

namespace LiteServer.Http.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task WriteAsync(this LiteHttpResponse response, string content)
        {
            var buffer = Encoding.UTF8.GetBytes(content);
            await response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}