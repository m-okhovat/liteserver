using LiteServer.AppBuilders;
using LiteServer.Hosting;
using LiteServer.Listener.Hosting;
using System;
using System.Collections.Generic;

namespace LiteServer.ServerBuilders
{
    public class LiteServerBuilder : ILiteServerBuilder
    {
        private IServer _server;
        private readonly List<Action<ILiteApplicationBuilder>> _configs = new List<Action<ILiteApplicationBuilder>>();
        public ILiteServerBuilder Configure(Action<ILiteApplicationBuilder> config)
        {
            _configs.Add(config);
            return this;
        }

        public ILiteServerBuilder UseServer(IServer server)
        {
            _server = server;
            return this;
        }

        public ILiteHost Build()
        {
            var applicationBuilder = new DefaultApplicationBuilder();
            foreach (var config in _configs)
            {
                config(applicationBuilder);
            }

            return new Hosting.LiteServer(_server, applicationBuilder.Build());
        }
    }
}