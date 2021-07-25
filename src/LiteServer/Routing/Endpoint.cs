using LiteServer.Listener.Handlers;

namespace LiteServer.Routing
{
    public class Endpoint
    {
        public HandlerDelegate HandlerDelegate { get; }
        public string Pattern { get; }
        public string DisplayName { get; }
        public Endpoint(HandlerDelegate handlerDelegate, string pattern, string displayName)
        {
            HandlerDelegate = handlerDelegate;
            Pattern = pattern;
            DisplayName = displayName;
        }
    }
}