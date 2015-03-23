using System.Collections.Generic;

namespace MultiObjectiveOptimizationLib.Extensions
{
    public static class ListExtensions
    {
        public static void AddFirst<T>(this List<T> list, T element)
        {
            list.Insert(0,element);
        }
    }
}