using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.FileManager;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints;

namespace MultiObjectiveOptimizationLib.Solver.GeneticAlgorythm
{
    public static class Fitness
    {
        private static readonly Func<Vector<double>, Vector<double>, double> _metrc = Metrics.EuclideanDistance;
        public static Dictionary<Route, double> FitnessValues(List<Route> union, Dictionary<Route, Vector<double>> routesObjectiveVectors)
        {
            try
            {
                var rawFit = RawFitnesses(union, routesObjectiveVectors);
                return rawFit;
            }
            catch (Exception e)
            {
                Log.Save(e);
                var ret = new Dictionary<Route, double>();
                union.ForEach(x => ret.TryAdd(x, double.MaxValue));
                return ret;
            }
        }

        private static Dictionary<Route, double> RawFitnesses(List<Route> population,
            IReadOnlyDictionary<Route, Vector<double>> routesObjectiveVectors)
        {
            var ret = new Dictionary<Route, double>();
            population.ForEach(x => ret.TryAdd(x, routesObjectiveVectors.Count(y => y.Value < routesObjectiveVectors[x])));
            return ret;
        }

        private static Dictionary<Route, double> Densitys(IReadOnlyList<Route> population,
            IReadOnlyDictionary<Route, Vector<double>> routesObjectiveVectors)
        {
            if(population.Count==1) return new Dictionary<Route, double>(){{population[0],0}};
            var k = Convert.ToInt32(Math.Round(Math.Sqrt(population.Count)))-1;
            var res = new Dictionary<Route, double>();
            foreach (var x in population)
            {
                var kTH =
                    routesObjectiveVectors.Where(y => y.Key != x)
                        .OrderBy(y => _metrc(routesObjectiveVectors[x], y.Value)).ElementAt(k).Key;
                var dist = _metrc(routesObjectiveVectors[x], routesObjectiveVectors[kTH]);
                res.Add(x,1.0/(dist+2.0));
            }
            return res;
        }


        private static double InternalPopulationFitness(Route route, 
            Dictionary<Route,double> externalPopulationFitnessValues,
            IReadOnlyDictionary<Route, Vector<double>> routesObjectiveVectors)
        {
            return 1.0 /
            (1.0 + externalPopulationFitnessValues
                .Where(x => routesObjectiveVectors[x.Key] >= routesObjectiveVectors[route])
                .Sum(x => 1.0/x.Value));
        }

        private static double ExternalPopulationFitness(Route route, IReadOnlyCollection<Route> internalPopulation, 
            IReadOnlyDictionary<Route, Vector<double>> routesObjectiveVectors)
        {
            return 
            (1.0 + internalPopulation.Count)  /
            internalPopulation.Count(x => routesObjectiveVectors[route] >= routesObjectiveVectors[x]);
        }
    }
}