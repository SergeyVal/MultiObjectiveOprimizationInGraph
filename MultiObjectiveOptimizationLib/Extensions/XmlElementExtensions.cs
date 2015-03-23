using System.Xml;

namespace MultiObjectiveOptimizationLib.Extensions
{
    public static class XmlElementExtensions
    {
        public static string GetAttributeInnerText(this XmlElement xmlElement, string attribute)
        {
            return xmlElement.Attributes[attribute.ToString()].InnerText;
        }
        
    }
}