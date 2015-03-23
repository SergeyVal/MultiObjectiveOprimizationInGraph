using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;
using MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts;

namespace MultiObjectiveOptimizationLib.NodeCollections
{
    public abstract class AbstractNodeCollection : INodeCollection
    {
        protected List<Node> _nodes = new List<Node>();
        protected List<Link> _links = new List<Link>();
        private const double _epsilon=0.0005;

        protected AbstractNodeCollection(IEnumerable<Node> nodes)
        {
            foreach (var node in nodes)
            {
                Add(node);
            }
        }

        protected AbstractNodeCollection(List<Node> nodes, List<Link> links)
        {
            _nodes = nodes;
            _links = links;
        }

        protected AbstractNodeCollection()
        {
        }

        public abstract void Add(Node node);

        public void AddRange(IEnumerable<Node> nodes)
        {
            nodes.ForEach(Add);
        }

        public void Set(IEnumerable<Node> nodes, IEnumerable<Link> links)
        {
            _nodes = nodes.ToList();
            _links = links.ToList();
        }

        public IEnumerable<Link> GetLinks()
        {
            return _links;
        }
        
        public Node GetNode(Coordinates coordinates)
        {
            return _nodes.Find(node => node.Coordinates == coordinates);
        }

        public Node GetNode(double x, double y)
        {
            return _nodes.Find(node => Math.Abs(node.Coordinates.X - x) < _epsilon && Math.Abs(node.Coordinates.Y - y) < _epsilon);
        }

        public Node GetNode(Id id)
        {
            return _nodes.Find(node => node.Id == id);
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Node this[int index]
        {
            get { return _nodes[index]; }
        }

        public int Count
        {
            get { return _nodes.Count; }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < Count - 1 ; i++)
            {
                str.Append(_nodes[i] + "; ");
            }
            str.Append(_nodes[_nodes.Count]);
            return str.ToString();


        }
    }
}