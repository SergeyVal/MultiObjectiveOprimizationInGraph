using System.Collections.Generic;

namespace MultiObjectiveOptimizationLib.Extensions
{
    public static class HashSetExtensions
    {
        public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> toAdd)
        {
            foreach (var x in toAdd)
            {
                set.Add(x);
            }
        }
    }
}