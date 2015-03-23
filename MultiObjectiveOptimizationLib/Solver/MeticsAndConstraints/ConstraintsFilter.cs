using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints
{
    public class ConstraintsFilter<T>
    {
        private readonly List<Func<T, bool>> _constraints = new List<Func<T, bool>>();

        public void AddConstraint(Func<T, bool> constraint)
        {
            _constraints.Add(constraint);
        }

        public List<T> Filter(List<T> set)
        {
            return set.Where(x => _constraints.Count(y => y(x)) == _constraints.Count).ToList();
        } 
    }
}