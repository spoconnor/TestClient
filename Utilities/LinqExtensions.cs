using System;
using System.Collections.Generic;
using System.Linq;

namespace TestClient.Utilities
{
    static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static TValue ValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            var value;
            dict.TryGetValue(key, out value);
            return value;
        }
    }
}