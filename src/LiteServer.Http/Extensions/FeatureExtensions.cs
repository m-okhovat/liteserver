using LiteServer.Http.Features;

namespace LiteServer.Http.Extensions
{
    public static class FeatureExtensions
    {
        public static T Get<T>(this IFeatureCollection features)
        {
            return features.TryGetValue(typeof(T), out var requests) ? (T)requests : default;
        }

        public static IFeatureCollection Set<T>(this IFeatureCollection features, T value)
        {
            features[typeof(T)] = value;
            return features;
        }
    }
}