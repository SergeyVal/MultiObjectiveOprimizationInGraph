using System;
using System.Globalization;

namespace MultiObjectiveOptimizationLib.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        public static double ToDouble(this string str)
        {
            //return str;
            return double.Parse(str, CultureInfo.InvariantCulture);
        }
    }
}