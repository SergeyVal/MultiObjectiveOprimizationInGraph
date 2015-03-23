using System.Collections.Generic;
using System.Xml.Linq;
using MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementFactories;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager
{
    public class CollectionXmlSaver<T> : AbstractXmlSaver<IEnumerable<T>>
    {
        private readonly AbstractXElementFactory<T> _xElementFactory;

        public CollectionXmlSaver(AbstractXElementFactory<T> xElementFactory)
        {
            _xElementFactory = xElementFactory;
        }

        public override void SaveToFile(string fileName, IEnumerable<T> info)
        {
            XDocument xDocument = new XDocument();
            XElement root = new XElement("root");
            AppeneElementListToXelement(info,_xElementFactory,root);
            xDocument.Add(root);
            xDocument.Save(fileName);
        }

        
        
    }
}