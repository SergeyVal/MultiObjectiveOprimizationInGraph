using System.Linq;
using MultiObjectiveOptimizationLib.NodeCollections;

namespace MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints
{
    public static class RouteConstraints
    {
        public static double FlowBandwidth { get; set; }

        static RouteConstraints()
        {
            FlowBandwidth = 10;
        }

        public static bool BandWidthConstraint(Route route, double flowBandwidth)
        {
            var bandwidth = RouteMetrics.BandWidth(route, flowBandwidth);
            return route.GetLinks().Any(x => x.BandWidth > bandwidth);
        }

        public static bool BandWidthConstraint(Route route)
        {
            return BandWidthConstraint(route, FlowBandwidth);
        }

        public static bool NoLoops(Route route)
        {
            return route.Select(x => route.Count(y => y == x)).Sum() == route.Count;
        }
    }
}