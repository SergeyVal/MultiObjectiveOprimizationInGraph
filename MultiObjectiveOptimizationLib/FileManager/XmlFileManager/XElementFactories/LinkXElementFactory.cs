using System.Xml.Linq;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementFactories
{
    public class LinkXElementFactory : AbstractXElementFactory<Link>
    {
        public override XElement CreateXmlElement(Link x)
        {
            return new XElement("Link",
                new XAttribute("First",x.FirstNode.Id),
                new XAttribute("Second", x.SecondNode.Id),
                new XAttribute("Bandwidth",x.BandWidth)
                );
        }
    }
}