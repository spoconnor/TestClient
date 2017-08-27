using System;
using System.Collections.Generic;

namespace TestClient
{
    public static class DictionaryExtensions
    {
        public static void AddRange<TKey,TValue>(this Dictionary<TKey,TValue> dict, IEnumerable<KeyValuePair<TKey,TValue>> items)
        {
            foreach (var item in items)
                dict.Add(item.Key,item.Value);
        }
    }

}

