using System.Windows;
using System.Windows.Shapes;

namespace MultiObjectiveOptimizationDrawer.Extensions
{
    public static class EllipseExtensions
    {
        public static Point Center(this Ellipse ellipse)
        {
            var p = ellipse.Margin;
            var x = p.Left + ellipse.Width / 2;
            var y = p.Top + ellipse.Height / 2;
            return new Point(x,y);
        }
    }
}