using System.Collections.Generic;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;
using MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts;

namespace MultiObjectiveOptimizationLib.NodeCollections
{
    public interface INodeCollection : IEnumerable<Node>
    {
        void Add(Node node);
        void AddRange(IEnumerable<Node> nodes);
        void Set(IEnumerable<Node> nodes, IEnumerable<Link> links);
        IEnumerable<Link> GetLinks();
        Node GetNode(Coordinates coordinates);
        Node GetNode(Id id);
        Node GetNode(double x, double y);
        Node this[int index] { get; }
        int Count { get; }

    }
}