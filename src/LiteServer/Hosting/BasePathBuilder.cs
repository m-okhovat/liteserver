namespace LiteServer.Hosting
{
    public class BasePathBuilder
    {
        private string _hostName = "localhost";
        private int _port = 5000;
        private string _url = "";
        private bool _useHttps = false;

        public BasePathBuilder WithHostName(string hostName)
        {
            _hostName = hostName;
            return this;
        }

        public BasePathBuilder WithPort(int port)
        {
            _port = port;
            return this;
        }

        public BasePathBuilder WithUrl(string url)
        {
            _url = url;
            return this;
        }

        public BasePathBuilder UseHttps()
        {
            _useHttps = true;
            return this;
        }

        internal BasePath Build()
        {
            return new BasePath(_hostName, _port, _url, _useHttps);
        }

    }
}