using System;
using System.Windows;

namespace MultiObjectiveOptimizationDrawer.Events
{
    public class PointChangedArgs : EventArgs
    {
        public Point Point { get; private set; }

        public PointChangedArgs(Point point)
        {
            Point = point;
        }
    }
}