using System;
using System.Collections.Generic;

namespace LiteServer.Http.Features
{
    public class FeatureCollection : Dictionary<Type, object>, IFeatureCollection
    {

    }
}