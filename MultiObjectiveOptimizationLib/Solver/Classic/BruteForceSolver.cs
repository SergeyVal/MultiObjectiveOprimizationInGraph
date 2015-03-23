using System;
using System.Collections.Generic;
using System.Linq;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;
using MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints;

namespace MultiObjectiveOptimizationLib.Solver.Classic
{
    public class BruteForceSolver : ISolver
    {
        private readonly List<Func<Route, double>> _objectives;
        private readonly IScalarizator<Route> _scalarizator;
        private readonly FullConnectedGraph _graph;
        private readonly ConstraintsFilter<Route> _filter; 
        public Result LastResult { get; private set; }

        public event SolvedEvent Solved;

        public BruteForceSolver(List<Func<Route, double>> objectives, ConstraintsFilter<Route> filter, 
            IScalarizator<Route> scalarizator, FullConnectedGraph graph)
        {
            _objectives = objectives;
            _scalarizator = scalarizator;
            _graph = graph;
            _filter = filter;
        }

        public List<Route> Solve(Node start, Node end)
        {
            var result = _scalarizator.GetParetoFront(_objectives, GetAllPossibleRoutes(start,end));
            LastResult = new Result(result,_objectives);
            OnSolved(LastResult);
            return result;
        }

        private List<Route> GetAllPossibleRoutes(Node start, Node end)
        {
            var set = _graph.Where(x => x != start && x != end).ToList();
            var paths = new List<List<Node>>(){new List<Node>()};
            for (int i = 1; i <= set.Count; i++)
            {
                var levelpaths = VariationGenerator<Node>.Generate(set, i);
                //paths = paths.Union(levelpaths);
                paths.AddRange(levelpaths);
            }
            paths.ForEach(x => { x.AddFirst(start); x.Add(end); });
            return _filter.Filter(paths.Select(x => new Route(x)).ToList());
        }

        protected virtual void OnSolved(Result result)
        {
            var handler = Solved;
            if (handler != null) handler(this, new SolvedEventArgs(result));
        }
    }
}