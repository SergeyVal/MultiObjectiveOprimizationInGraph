using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MultiObjectiveOptimizationDrawer.Extensions
{
    public static class UIElementCollectionExtensions
    {
        public static void Remove(this UIElementCollection collection, IEnumerable<UIElement> toRemove)
        {
            var t = new List<UIElement>(toRemove);
            foreach (var uiElement in t)
            {
                collection.Remove(uiElement);
            }
        }

        
    }
}