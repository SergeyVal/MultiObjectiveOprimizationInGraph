using System.Collections.Generic;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;
using MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts;

namespace MultiObjectiveOptimizationLib.NodeCollections.NodeCollectionFactorys
{
    public static class NodeCollectionFactory<T>
        where T : INodeCollection, new()
    {
        public static T GetRandomNodeCollection(int numberOfNodes, Coordinates minCoordinates, Coordinates maxCoordinates)
        {
            var nodeCollection = new T();
            nodeCollection.AddRange(CreateRandomNodes(numberOfNodes,minCoordinates,maxCoordinates));
            return nodeCollection;
        }

        public static T GetMeshNodeCollection(int xAxisCount, int yAxisCount)
        {
            var nodeCollection = new T();
            nodeCollection.AddRange(CreateMeshNodes(xAxisCount, yAxisCount));
            return nodeCollection;
        }
        

        private static IEnumerable<Node> CreateRandomNodes(int numberOfNodes, Coordinates minCoordinates, Coordinates maxCoordinates)
        {
            var nodes = new List<Node>();
            for (int i = 0; i < numberOfNodes; i++)
            {
                nodes.Add(NodeFactory.GetRandomNode(minCoordinates,maxCoordinates));
            }
            return nodes;
        }

        private static IEnumerable<Node> CreateMeshNodes(int xAxisCount, int yAxisCount)
        {
            var nodes = new List<Node>();
            for (int i = 0; i < xAxisCount; i++)
            {
                for (int j = 0; j < yAxisCount; j++)
                {
                    nodes.Add(new Node(i,j));
                }
            }
            return nodes;
        } 
    }
}