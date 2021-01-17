using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            return dict.TryGetValue(key, out var value)
                ? value
                : defaultValue;
        }

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
                return false;

            dict.Add(
                new KeyValuePair<TKey, TValue>(key, value)
            );

            return true;
        }

        public static void AddOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else
                dict.Add(
                    new KeyValuePair<TKey, TValue>(key, value)
                );
        }

        public static void Remove<TKey, TValue>(this IDictionary<TKey, TValue> dict, IEnumerable<TKey> keysList)
        {
            var keysArray = keysList.ToArray();

            foreach (var key in keysArray)
                dict.Remove(key);
        }
    }
}
