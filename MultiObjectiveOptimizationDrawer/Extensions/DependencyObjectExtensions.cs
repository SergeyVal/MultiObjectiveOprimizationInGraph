using System.Windows;
using System.Windows.Media;

namespace MultiObjectiveOptimizationDrawer.Extensions
{
    public static class DependencyObjectExtensions
    {
        public static T FindVisualParent<T>(this DependencyObject obj)
            where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                T typed = parent as T;
                if (typed != null)
                {
                    return typed;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }
    }
}