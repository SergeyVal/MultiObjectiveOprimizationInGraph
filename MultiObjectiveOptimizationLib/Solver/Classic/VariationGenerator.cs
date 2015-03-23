using System;
using System.Collections.Generic;
using System.Linq;
using MultiObjectiveOptimizationLib.Extensions;

namespace MultiObjectiveOptimizationLib.Solver.Classic
{
    public static class VariationGenerator<T>
    {
        public static List<List<T>> Generate(List<T> set, int positions)
        {
            if(set.Count<positions || positions<0 || set.Count==0) throw new ArgumentException();
            if (positions == 1)
            {
                return set.Select(x => new List<T> {x}).ToList();
            }
            var ret = new List<List<T>>();
            return set
                .Aggregate(ret, (current, element) => 
                    current
                    .Union(ItemSetMultiplication(element,Generate(set.Without(element).ToList(), positions - 1)))
                    .ToList());
        }

        

        private static List<List<T>> ItemSetMultiplication(T item, List<List<T>> set)
        {
            return set
                .Select(x =>
                {
                    var list = new List<T> { item };
                    list.AddRange(x);
                    return list;
                })
            .ToList();
        } 
        
    }
}