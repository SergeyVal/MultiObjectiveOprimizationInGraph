using System;
using System.Windows;
using System.Xml.Schema;

namespace MultiObjectiveOptimizationDrawer.Extensions
{
    public static class PointExtensions
    {
        private const double _epsilon = 0.05;

        public static double Distance(this Point point, Point otherPoint)
        {
            var x = point.X - otherPoint.X;
            var y = point.Y - otherPoint.Y;
            return x*x - y*y;
        }

        public static bool AlmostEqual(this Point point, Point other)
        {
            return point.AlmostEqual(other.X, other.Y);
        }

        public static bool AlmostEqual(this Point point, double x, double y)
        {
            return Math.Abs(point.X - x) < _epsilon && Math.Abs(point.Y - y) < _epsilon;
        }
    }
}