using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiObjectiveOptimizationLib.Solver
{
    public static class SetOperators
    {
        public static List<Vector<T>> Mulitplication<T>(List<Vector<T>> first, List<Vector<T>> second) where T : IComparable<T>
        {
            var result = new List<Vector<T>>();
            foreach (var i in first)
            {
                result.AddRange(second.Select(j => i*j));
            }
            return result;
        }

        public static List<Vector<T>> Mulitplication<T>(List<Vector<T>> first, List<Vector<T>> second,
            Func<Vector<T>, bool> filterPredicate) where T : IComparable<T>
        {
            var result = new List<Vector<T>>();
            foreach (var i in first)
            {
                result.AddRange(second.Select(j => i*j).Where(filterPredicate));
            }
            return result;
        } 
    }
}