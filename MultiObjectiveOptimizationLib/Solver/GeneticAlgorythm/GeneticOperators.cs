using System;
using System.Collections.Generic;
using System.Linq;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.FileManager;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;

namespace MultiObjectiveOptimizationLib.Solver.GeneticAlgorythm
{
    public class GeneticOperators
    {

        private readonly Random _random = new Random(DateTime.Now.Millisecond);

        private const double _epsilon = 0.005;

        private readonly int _mutationProbability;

        private readonly int _crossoverProbability;

        public GeneticOperators(int mutationProbability, int crossoverProbability)
        {
            _mutationProbability = mutationProbability;
            _crossoverProbability = crossoverProbability;
        }

        public List<Route> Crossover(Route first, Route second)
        {
            var crossoverPoint = _random.Next(1,Math.Min(first.Count, second.Count));
            return Crossover(first, second, crossoverPoint);
        }

        public List<Route> Crossover(Route first, Route second, int crossoverPoint)
        {
            try
            {
                if(_random.Next(100)>_crossoverProbability) return new List<Route>(){first,second};
                if(crossoverPoint>Math.Min(first.Count,second.Count)) 
                    throw new ArgumentException(@"Crossover point is larger route length");
                var firstNewGenes = new List<Node>();
                firstNewGenes.AddRange(first.Take(crossoverPoint));
                firstNewGenes.AddRange(second.Skip(crossoverPoint));
                var secondNewGenes = new List<Node>();
                secondNewGenes.AddRange(second.Take(crossoverPoint));
                secondNewGenes.AddRange(first.Skip(crossoverPoint));
                return new List<Route> {new Route(firstNewGenes),new Route(secondNewGenes)};
            }
            catch (Exception e)
            {
                Log.Save(e);
                return new List<Route> {first, second};
            }
        }

        public Route Mutation(Route route, List<Node> allowedPositions)
        {
            var mutationPoint = _random.Next(1,route.Count);
            return Mutation(route, allowedPositions, mutationPoint);
        }

        public Route Mutation(Route route, List<Node> allowedPositions, int mutationPoint)
        {
            try
            {
                if (_random.Next(100) > _mutationProbability) return route;
                if (mutationPoint > route.Count)
                    throw new ArgumentException(@"Mutation point is larger route length");
                var newGenes = new List<Node>(route.Take(mutationPoint));
                var sequence = GenerateRandomSequence(_random.Next(allowedPositions.Count()), allowedPositions.Count());
                sequence.Select(x => allowedPositions[x]).ForEach(x => newGenes.Add(x));
                newGenes.Add(route.Last());
                return new Route(newGenes);
            }
            catch (Exception e)
            {
                Log.Save(e);
                return route;
            }
        }

        public List<Route> Tournament(IEnumerable<Route> population,  
            Dictionary<Route,double> fitnessFuncValues, int numberOfParentsSubsets)
        {
            try
            {
                var newPopulation = new List<Route>();
                for (int i = 0; i < Math.Min(numberOfParentsSubsets,population.Count()); i++)
                {
                    var sequense = GenerateRandomSequence(_random.Next(1,population.Count()), population.Count());
                    var subset = population.Where((x, index) => sequense.Contains(index));
                    newPopulation.Add(subset.MinElement(x => fitnessFuncValues[x]));
                }
                return newPopulation;
            }
            catch (Exception e)
            {
                Log.Save(e);
                return population.ToList();
            }
        }

        public List<Route> Tournament(IEnumerable<Route> population,
           Dictionary<Route, double> fitnessFuncValues)
        {
            return Tournament(population, fitnessFuncValues, _random.Next(population.Count()));
        }
        
        private IEnumerable<int> GenerateRandomSequence(int maxcount, int maxvalue)
        {
            HashSet<int> sequence = new HashSet<int>();
            for (int i = 0; i < maxcount; i++)
            {
                sequence.Add(_random.Next(maxvalue));
            }
            return sequence;
        } 

        


    }
}