using System;
using MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts;
using MultiObjectiveOptimizationLib.Solver;
using MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints;

namespace MultiObjectiveOptimizationLib.NodeCollections.GraphParts
{
    public class Link 
    {
        public Node FirstNode { get; private set; }

        public Node SecondNode { get; private set; }

        public double Length { get; private set; }

        public double BandWidth { get; private set; }
        
        public Link(Node firstNode, Node secondNode, double bandWidth)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
            BandWidth = bandWidth;
            Length = _metric(firstNode.Coordinates,secondNode.Coordinates);
        }

        private static readonly Func<Coordinates, Coordinates, double> _metric = Metrics.EuclideanDistance;



    }
}