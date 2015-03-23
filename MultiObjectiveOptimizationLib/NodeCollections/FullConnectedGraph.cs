using System.Collections.Generic;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;

namespace MultiObjectiveOptimizationLib.NodeCollections
{
    public class FullConnectedGraph : AbstractNodeCollection
    {
        public override void Add(Node node)
        {
            _nodes.ForEach(x => AddLink(x, node));
            _nodes.Add(node);
        }
        
        private void AddLink(Node firstNode, Node secondNode)
        {
            _links.Add(new Link(firstNode, secondNode, BandWidthFactory.GetBandWidth()));
        }


        public FullConnectedGraph(IEnumerable<Node> nodes) : base(nodes)
        {
        }

        public FullConnectedGraph() : base() { }
    }
}