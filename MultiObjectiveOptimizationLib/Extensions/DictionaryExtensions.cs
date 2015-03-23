using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiObjectiveOptimizationLib.Extensions
{
    public static class DictionaryExtensions
    {
        public static KeyValuePair<T,U> Min<T,U>(this Dictionary<T,U> dictionary, Func<KeyValuePair<T,U>, double> predicate)
        {
            KeyValuePair<T, U> min = dictionary.First();
            foreach (var pair in dictionary)
            {
                if (predicate(pair) < predicate(min)) min = pair;
            }
            return min;
        }

        public static void TryAdd<T, U>(this Dictionary<T, U> dictionary, T key, U value)
        {
            if(dictionary.ContainsKey(key)) return;
            dictionary.Add(key,value);
        }
    }
}