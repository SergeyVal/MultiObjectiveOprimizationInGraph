using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints;

namespace MultiObjectiveOptimizationLib.Solver
{
    public class Result : Dictionary<Route, Vector<double>>
    {
        public Result(List<Route> routes, List<Func<Route, double>> objectives)
        {
            routes.Distinct().ForEach(x => Add(x, ObjectivesCalculator.CalculateObjectives(x, objectives)));
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (var value in this)
            {
                str.Append(value.Key + " - " + value.Value + "\n");
            }
            return str.ToString();
        }
    }
}