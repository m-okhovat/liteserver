using System;

namespace LiteServer.Hosting
{
    public class BasePath
    {
        public string HostName { get; }
        public int Port { get; }
        public string Url { get; }
        public bool UseHttps { get; }

        public BasePath(string hostName, int port, string url, bool useHttps = false)
        {
            HostName = hostName ?? throw new ArgumentNullException(nameof(hostName));
            Port = port;
            UseHttps = useHttps;
            Url = url;
        }

        public override string ToString()
        {
            var s = UseHttps ? "s" : string.Empty;
            return $"http{s}://{HostName}:{Port}/{Url}";
        }
    }
}