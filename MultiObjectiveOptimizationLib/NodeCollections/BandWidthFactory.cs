using System;
using MultiObjectiveOptimizationLib.Extensions;

namespace MultiObjectiveOptimizationLib.NodeCollections
{
    public static class BandWidthFactory
    {
        public static double Average { get; set; }
        public static double Deviation { get; set; }

        private static readonly Random _random = new Random(DateTime.Now.Millisecond);

        static BandWidthFactory()
        {
            Average = 32;
            Deviation = 2;
        }

        public static double GetBandWidth()
        {
            return _random.NextDouble(Average - Deviation, Average + Deviation);
        }
    }
}