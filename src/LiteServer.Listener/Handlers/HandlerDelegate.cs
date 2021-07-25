using System.Threading.Tasks;
using LiteServer.Http.HttpContext;

namespace LiteServer.Listener.Handlers
{
    public delegate Task HandlerDelegate(LiteHttpContext context);
}