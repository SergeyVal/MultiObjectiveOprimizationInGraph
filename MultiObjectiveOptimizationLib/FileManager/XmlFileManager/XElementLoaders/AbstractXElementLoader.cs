using System.Xml.Linq;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementLoaders
{
    public abstract class AbstractXElementLoader<T>
    {
        public abstract T CreateInstanceFromXmlElement(XElement xElement);
    }
}