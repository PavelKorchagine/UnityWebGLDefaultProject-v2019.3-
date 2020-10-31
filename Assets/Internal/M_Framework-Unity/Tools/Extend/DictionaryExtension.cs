using System;
using System.Collections.Generic;
using System.Linq;

namespace MFramework_Unity.Tools
{
    public static partial class DictionaryExtension
    {
        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, params Dictionary<TKey, TValue>[] dictionaries)
        {
            return dictionaries.Aggregate(dictionary,
                (current, self) => current.Union(self).ToDictionary(kv => kv.Key, kv => kv.Value));
        }

        public static void ForEach<K, V>(this Dictionary<K, V> self, Action<K, V> action)
        {
            var dictE = self.GetEnumerator();
            while (dictE.MoveNext())
            {
                var current = dictE.Current;
                action(current.Key, current.Value);
            }

            dictE.Dispose();
        }

        public static void AddRange<K, V>(this Dictionary<K, V> self, Dictionary<K, V> addInDict, bool isOverride = false)
        {
            var dictE = addInDict.GetEnumerator();

            while (dictE.MoveNext())
            {
                var current = dictE.Current;
                if (self.ContainsKey(current.Key))
                {
                    if (isOverride)
                        self[current.Key] = current.Value;
                    continue;
                }

                self.Add(current.Key, current.Value);
            }

            dictE.Dispose();
        }
    }
}