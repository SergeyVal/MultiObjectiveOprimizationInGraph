using System.Xml.Linq;
using MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementFactories;
using MultiObjectiveOptimizationLib.NodeCollections;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager
{
    public class NodeCollectionXmlSaver : AbstractXmlSaver<INodeCollection>
    {
        private readonly NodeXElementFactory _nodeXElementFactory = new NodeXElementFactory();
        private readonly LinkXElementFactory _linkXElementFactory = new LinkXElementFactory();
        public override void SaveToFile(string fileName, INodeCollection info)
        {
            XDocument xDocument = new XDocument();
            XElement root = new XElement("root");
            AppeneElementListToXelement(info, _nodeXElementFactory, root);
            AppeneElementListToXelement(info.GetLinks(),_linkXElementFactory,root);
            xDocument.Add(root);
            xDocument.Save(fileName);
        }

    }
}