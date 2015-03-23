using System;

namespace MultiObjectiveOptimizationLib.Extensions
{
    public static class RandomExtensions
    {
        public static double NextDouble(this Random random, double maxValue, double minValue)
        {
            return random.NextDouble()*(maxValue - minValue) + minValue;
        }
    }
}