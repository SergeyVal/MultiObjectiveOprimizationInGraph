using System.Xml.Linq;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementFactories
{
    public class NodeXElementFactory : AbstractXElementFactory<Node>
    {
        public override XElement CreateXmlElement(Node node)
        {
            return new XElement("Node",
                new XAttribute("Id", node.Id.ToString()),
                new XAttribute("X", node.Coordinates.X),
                new XAttribute("Y", node.Coordinates.Y));
        }
    }
}