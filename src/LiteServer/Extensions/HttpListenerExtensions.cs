﻿using System.Net;

namespace LiteServer.Extensions
{
    public static class HttpListenerExtensions
    {
        public static void TryAddPrefix(this HttpListener listener, string prefix)
        {
            if (!string.IsNullOrEmpty(prefix))
                listener.Prefixes.Add(prefix.Trim());
        }
    }
}