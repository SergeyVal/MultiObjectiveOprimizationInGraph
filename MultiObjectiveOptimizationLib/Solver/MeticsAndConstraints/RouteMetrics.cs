using System;
using System.Linq;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;
using MultiObjectiveOptimizationLib.NodeCollections.LowLevelParts;

namespace MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints
{
    public static class RouteMetrics
    {
        private static Func<Coordinates, Coordinates, double> _metric = Metrics.EuclideanDistance;
        public static double Lambda { get; set; }

        static RouteMetrics()
        {
            Lambda = 100;
        }

        public static double Length(Route route)
        {
            return route.GetLinks().Aggregate(0, (double tmp, Link link) => tmp + link.Length);
        }

        public static double MaxLength(Route route)
        {
            return route.Count == 1 ? 0 : route.GetLinks().Max(x => x.Length);
        }

        public static double FSL(Route route)
        {
            return route.GetLinks().Max(x => FSL(x.Length,Lambda));
        }

        private static double FSL(double distance, double lambda)
        {
            return (4*Math.PI*distance/lambda)
                   *(4*Math.PI*distance/lambda);
        }

        public static double BandWidth(Route route, double flowBandwidth)
        {
            return route.GetLinks().Count()*flowBandwidth;
        }

        
    }
}