using System.Collections.Generic;
using System.Xml.Linq;
using MultiObjectiveOptimizationLib.FileManager.XmlFileManager.XElementLoaders;

namespace MultiObjectiveOptimizationLib.FileManager.XmlFileManager
{
    public abstract class CollectionXmlLoader<T> : AbstractXmlLoader<IEnumerable<T>>
    {
        protected readonly AbstractXElementLoader<T> _xElementLoader;

        protected CollectionXmlLoader(AbstractXElementLoader<T> xElementLoader)
        {
            _xElementLoader = xElementLoader;
        }

        public override IEnumerable<T> LoadFromFile(string fileName)
        {
            return CreateElements(GetXElements(fileName), _xElementLoader);
        }

        protected abstract IEnumerable<XElement> GetXElements(string fileName);

        
    }
}