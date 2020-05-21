/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 */
using System.Collections.Generic;

namespace Automation.TestRail.Extensions
{
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// Add or replace keys/values in the target dictionary
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary</typeparam>
        /// <param name="to">Target dictionary</param>
        /// <param name="key">The object defined in each key/value pair</param>
        /// <param name="value">The definition associated with key</param>
        public static void AddOrReplace<TKey, TValue>(this IDictionary<TKey, TValue> to, TKey key, TValue value)
        {
            // apply key with default value if not exists
            if (!to.ContainsKey(key))
            {
                to.Add(key, default);
            }

            // apply/replace new value
            to[key] = value;
        }
    }
}