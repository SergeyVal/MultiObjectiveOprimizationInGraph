using System;
using System.Collections.Generic;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints;

namespace MultiObjectiveOptimizationLib.Solver.Builders
{
    public abstract class AbstractSolverBuilder 
    {
        protected List<Func<Route, double>> _objectives = new List<Func<Route, double>>();
        protected ConstraintsFilter<Route> _filter = new ConstraintsFilter<Route>();
        protected FullConnectedGraph _graph = null;
        public abstract ISolver GetResult();
        public void AddObjective(Func<Route, double> objective)
        {
            _objectives.Add(objective);
        }

        public void AddConstraint(Func<Route, bool> constraint)
        {
            _filter.AddConstraint(constraint);
        }

        public void SetGraph(FullConnectedGraph graph)
        {
            _graph = graph;
        }

        

        protected void CheckForEmpty()
        {
            if (_objectives.IsEmpty() || _graph == null) throw new Exception();
        }
 
    }
}