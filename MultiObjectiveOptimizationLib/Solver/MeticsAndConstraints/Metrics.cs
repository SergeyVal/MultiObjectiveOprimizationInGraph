using System;
using System.Linq;
using MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts;

namespace MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints
{
    public static class Metrics
    {
        public static double EuclideanDistance(Coordinates firstPoint, Coordinates secondPoint)
        {
            var x = (firstPoint.X - secondPoint.X);
            var y = (firstPoint.Y - secondPoint.Y);
            return Math.Sqrt(x*x + y*y);
        }

        public static double EuclideanDistance(Vector<double> firstPoint, Vector<double> secondPoint)
        {
            if(firstPoint.Count!=secondPoint.Count) throw new ArgumentException();
            return Math.Sqrt(firstPoint.Select((x, i) => (x - secondPoint[i])).Select(x => x*x).Sum());
        }

        public static double ManhattanDistance(Coordinates firstPoint, Coordinates secondPoint)
        {
            return 
                Math.Abs(firstPoint.X - secondPoint.X) 
                + 
                Math.Abs(firstPoint.Y - secondPoint.Y);
        }

        public static double ManhattanDistance(Vector<double> firstPoint, Vector<double> secondPoint)
        {
            if (firstPoint.Count != secondPoint.Count) throw new ArgumentException();
            return firstPoint.Select((x, i) => Math.Abs(x - secondPoint[i])).Sum();
        }

        public static double DiscreteDistance(Coordinates firstPoint, Coordinates secondPoint)
        {
            return firstPoint == secondPoint ? 0 : 1;
        }

        public static double DiscreteDistance(Vector<double> firstPoint, Vector<double> secondPoint)
        {
            if (firstPoint.Count != secondPoint.Count) throw new ArgumentException();
            return firstPoint == secondPoint ? 0 : 1;
        }
    }
}