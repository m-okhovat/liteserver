using LiteServer.Http.HttpContext;
using System.Threading.Tasks;

namespace LiteServer.Handlers
{
    public delegate Task HandlerDelegate(LiteHttpContext context);
}