using System;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;
using MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts;

namespace MultiObjectiveOptimizationLib.NodeCollections.NodeCollectionFactorys
{
    public static class NodeFactory
    {
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);

        public static Node GetRandomNode(Coordinates minCoordinates, Coordinates maxCoordinates)
        {
            var x = _random.NextDouble(minCoordinates.X, maxCoordinates.X);
            var y = _random.NextDouble(minCoordinates.Y, maxCoordinates.Y);
            return new Node(new Coordinates(x,y));
        }
        
    }
}