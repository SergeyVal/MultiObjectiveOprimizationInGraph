using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiObjectiveOptimizationLib.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> set, Action<T> function)
        {
            foreach (var i in set)
            {
                function(i);
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> set)
        {
            return !set.Any();
        }

        public static T MinElement<T>(this IEnumerable<T> set, Func<T, double> function)
        {
            var minX = set.First();
            var min = function(minX);
            foreach (var x in set)
            {
                var valX = function(x);
                if (!(valX < min)) continue;
                minX = x;
                min = valX;
            }
            return minX;
        }

        public static T MaxElement<T>(this IEnumerable<T> set, Func<T, double> function)
        {
            var maxElement = set.First();
            var max = function(maxElement);
            foreach (var x in set)
            {
                var valX = function(x);
                if (!(valX > max)) continue;
                maxElement = x;
                max = valX;
            }
            return maxElement;
        }

        public static IEnumerable<T> Without<T>(this IEnumerable<T> set, T element)
        {
            return set.Where(x => !x.Equals(element));
            //return set.Except(new[] { element });
        }
    }
}