using System;
using System.Collections.Generic;
using System.Linq;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;

namespace MultiObjectiveOptimizationLib.NodeCollections
{
    public class Route : AbstractNodeCollection
    {
        public override void Add(Node node)
        {
            if (_nodes.IsEmpty())
            {
                _nodes.Add(node);
            }
            else
            {
                _links.Add(new Link(_nodes.Last(),node, BandWidthFactory.GetBandWidth()));
                _nodes.Add(node);
            }
        }

        public Route(IEnumerable<Node> nodes) : base(nodes)
        {
        }

        public Route() : base()
        {
            
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((Route)obj);
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(this.Aggregate(0.0,
                (temp, node) => temp + Math.Round(Math.E*node.Coordinates.X + Math.PI*node.Coordinates.Y)));
        }

        protected bool Equals(Route other)
        {
            if (_nodes.Count != other.Count) return false;
            return _nodes.Where((x, i) => x == other[i]).Count() == Count;
        }
        

        public static bool operator ==(Route left, Route right)
        {
            return left != null && left.Equals(right);
        }

        public static bool operator !=(Route left, Route right)
        {
            return left != null && !left.Equals(right);
        }
    }
}
