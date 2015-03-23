using System;
using System.Collections.Generic;
using System.Linq;
using MultiObjectiveOptimizationLib.NodeCollections;

namespace MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints
{
    public static class ObjectivesCalculator
    {
        public static Vector<double> CalculateObjectives(Route route, List<Func<Route,double>> objectives)
        {
            return new Vector<double>(objectives.Select(x => x(route)));
        } 
    }
}