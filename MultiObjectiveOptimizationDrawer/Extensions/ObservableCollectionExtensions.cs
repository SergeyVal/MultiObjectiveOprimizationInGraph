using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MultiObjectiveOptimizationDrawer.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void Remove<T>(this ObservableCollection<T> collection, IEnumerable<T> toRemove)
        {
            foreach (var uiElement in toRemove)
            {
                collection.Remove(uiElement);
            }
        }

    }
}