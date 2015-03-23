using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementFactories;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager
{
    public abstract class AbstractXmlSaver<T> : IFileSaver<T>
    {
        public abstract void SaveToFile(string fileName, T info);

        protected void AppeneElementListToXelement<U>(IEnumerable<U> elementList, AbstractXElementFactory<U> xElementFactory,
            XElement rootElement)
        {
            elementList
                .Select(xElementFactory.CreateXmlElement)
                .ToList()
                .ForEach(rootElement.Add);

        }
    }
}