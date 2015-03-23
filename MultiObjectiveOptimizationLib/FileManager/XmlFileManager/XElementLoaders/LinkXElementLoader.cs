using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;
using MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementLoaders
{
    public class LinkXElementLoader :AbstractXElementLoader<Link>
    {
        private readonly IEnumerable<Node> _nodes;

        public LinkXElementLoader(IEnumerable<Node> nodes)
        {
            _nodes = nodes;
        }

        public override Link CreateInstanceFromXmlElement(XElement xElement)
        {
            
            var bandWidth = (double)xElement.Attribute("Bandwidth");
            var firstId = new Id((int)xElement.Attribute("First"));
            var secondID = new Id((int)xElement.Attribute("Second"));
            var first = _nodes.First(x => x.Id == firstId);
            var second = _nodes.First(x => x.Id == secondID);
            return new Link(first,second,bandWidth);
        }
    }
}