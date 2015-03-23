using System.Windows;
using System.Windows.Shapes;

namespace MultiObjectiveOptimizationDrawer.Extensions
{
    public static class LineExtensions
    {
        public static bool Contains(this Line line, Point point)
        {
            return point.AlmostEqual(line.X1, line.Y1) || point.AlmostEqual(line.X2, line.Y2);
        }
    }
}