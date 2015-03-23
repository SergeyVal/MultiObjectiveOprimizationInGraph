using System.Collections.Generic;
using System.Xml.Linq;
using MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementLoaders;
using MultiObjectiveOptimizationLib.NodeCollections;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager
{
    public class NodeCollectionXmlLoader<T> : AbstractXmlLoader<T>
        where T : INodeCollection, new ()
    {
        private readonly NodeXElementLoader _nodeXElementLoader = new NodeXElementLoader();
        private LinkXElementLoader _linkXElementLoader;

        public override T LoadFromFile(string fileName)
        {
            XDocument xDocument = XDocument.Load(fileName);
            T nodeCollection = new T();
            var nodes = CreateElements(GetNodes(xDocument), _nodeXElementLoader);
            _linkXElementLoader = new LinkXElementLoader(nodes);
            var links = CreateElements(GetLinks(xDocument), _linkXElementLoader);
            nodeCollection.Set(nodes, links);
            return nodeCollection;
        }

        protected IEnumerable<XElement> GetNodes(XDocument xDocument)
        {
            return GetXElements(xDocument, "/root/Node");
        }

        protected IEnumerable<XElement> GetLinks(XDocument xDocument)
        {
            return GetXElements(xDocument, "/root/Link");
        } 
    }
}