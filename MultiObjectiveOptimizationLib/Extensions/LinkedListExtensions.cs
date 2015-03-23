using System.Collections.Generic;

namespace MultiObjectiveOptimizationLib.Extensions
{
    public static class LinkedListExtensions
    {
        public static T TakeFirst<T>(this LinkedList<T> list)
        {
            var ret = list.First.Value;
            list.RemoveFirst();
            return ret;
        }

        public static T TakeLast<T>(this LinkedList<T> list)
        {
            var ret = list.Last.Value;
            list.RemoveLast();
            return ret;
        }
    }
}