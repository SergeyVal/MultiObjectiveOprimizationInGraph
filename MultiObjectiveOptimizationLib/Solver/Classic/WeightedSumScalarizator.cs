using System;
using System.Collections.Generic;
using System.Linq;
using MultiObjectiveOptimizationLib.Extensions;

namespace MultiObjectiveOptimizationLib.Solver.Classic
{
    public class WeightedSumScalarizator<T> : IScalarizator<T>
    {
        private const double _epsilon = 0.00000000001;

        private readonly double _step;

        public WeightedSumScalarizator(double step)
        {
            _step = step;
        }

        public List<T> GetParetoFront(List<Func<T, double>> functions, List<T> domainSet)
        {
            var possibilities = domainSet.ToDictionary(x => x, x => new Vector<double>(functions.Select(y => y(x))));
            var coefficients = Coefficients(_step, functions.Count);
            var result = new List<T>();
            foreach (var vector in coefficients)
            {
                var min = possibilities.Min(pair => Scalarize(vector, pair.Value)).Key;
                if (!result.Contains(min)) result.Add(min);
            }
            return result;
        }

        private List<Vector<double>> Coefficients(double step, int count)
        {
            List<Vector<double>> result;
            if (count == 2)
            {
                result = new List<Vector<double>>();
                var edge = Range(0, 1, step);
                edge.ForEach(x => result.Add(new Vector<double>(new []{x, 1-x})));
                result.Add(new Vector<double>(new []{1.0,0}));
                return result;
            }
            else
            {
                var edge = Vector<double>.Range(0, 1, step);
                result = new List<Vector<double>>(edge);
                for (int i = 0; i < count - 2; i++)
                {
                    result = SetOperators.Mulitplication(result, edge, x => x.Sum() <= 1.0);
                }
                result = SetOperators.Mulitplication(result, edge, x => Math.Abs(x.Sum() - 1) < _epsilon);
            }
            return result;
        }

        private static double Scalarize(Vector<double> coefficients, Vector<double> values)
        {
            if(coefficients.Count!=values.Count) throw new ArgumentException();
            var s = values.Select((t, i) => coefficients[i]*t).Sum();
            return s;
        }

        private static List<double> Range(double min, double max, double step)
        {
            var range = new List<double>();
            for (var x = min; x <= max; x+=step)
            {
               range.Add(x); 
            }
            return range;
        } 
    }
}