using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementLoaders;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager
{
    public abstract class AbstractXmlLoader<T> : IFileLoader<T>
    {
        public abstract T LoadFromFile(string fileName);

        protected IEnumerable<U> CreateElements<U>(IEnumerable<XElement> xElements, AbstractXElementLoader<U> xElementLoader)
        {
            return xElements.Select(xElementLoader.CreateInstanceFromXmlElement);
        }

        protected IEnumerable<XElement> GetXElements(XDocument xDocument, string xpathQuery)
        {
            return xDocument.XPathSelectElements(xpathQuery);
        }
    }
}