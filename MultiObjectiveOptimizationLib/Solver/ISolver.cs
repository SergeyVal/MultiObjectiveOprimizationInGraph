using System.Collections.Generic;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;

namespace MultiObjectiveOptimizationLib.Solver
{
    public interface ISolver
    {
        event SolvedEvent Solved;
        List<Route> Solve(Node start, Node end);
        Result LastResult { get; }
    }
}