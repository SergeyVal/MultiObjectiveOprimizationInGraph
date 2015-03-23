using System.Xml.Linq;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;
using MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementLoaders
{
    public class NodeXElementLoader : AbstractXElementLoader<Node>
    {
        public override Node CreateInstanceFromXmlElement(XElement xElement)
        {
            var id = (int)xElement.Attribute("Id");
            var x = (double)xElement.Attribute("X");
            var y = (double)xElement.Attribute("Y");
            return new Node(new Id(id),new Coordinates(x,y));
        }
    }
}