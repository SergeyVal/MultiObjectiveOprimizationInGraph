using System.Xml.Linq;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementFactories
{
    public abstract class AbstractXElementFactory<T>
    {
        public abstract XElement CreateXmlElement(T x);
    }
}