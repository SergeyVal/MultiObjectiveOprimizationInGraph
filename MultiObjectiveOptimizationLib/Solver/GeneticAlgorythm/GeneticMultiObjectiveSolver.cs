using System;
using System.Collections.Generic;
using System.Linq;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.NodeCollections.GraphParts;
using MultiObjectiveOptimizationLib.Solver.Clustering;
using MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints;

namespace MultiObjectiveOptimizationLib.Solver.GeneticAlgorythm
{
    public class GeneticMultiObjectiveSolver : ISolver
    {
        private readonly FullConnectedGraph _graph;
        private readonly List<Func<Route, double>> _objectives;
        private readonly ConstraintsFilter<Route> _filter; 
        private readonly int _maxGenerationCount;
        private readonly int _externalPopulationCount;
        private readonly int _populationCount;
        private readonly GeneticOperators _geneticOperators;
        private readonly GraphInitialSetFactory _initialSetFactory;
        public Result LastResult { get; private set; }
        public event SolvedEvent Solved;

        public GeneticMultiObjectiveSolver(FullConnectedGraph graph, List<Func<Route, double>> objectives, 
            ConstraintsFilter<Route> filter, int maxGenerationCount, int externalPopulationCount, int populationCount,
            GeneticOperators geneticOperators, GraphInitialSetFactory initialSetFactory)
        {
            _graph = graph;
            _objectives = objectives;
            _filter = filter;
            _maxGenerationCount = maxGenerationCount;
            _externalPopulationCount = externalPopulationCount;
            _populationCount = populationCount;
            _geneticOperators = geneticOperators;
            _initialSetFactory = initialSetFactory;
        }
        
        public List<Route> Solve(Node start, Node end)
        {
            var population =new List<Route>(_initialSetFactory.CreateInitialPopulation(start, end));
            var archive = new List<Route>();
            Dictionary<Route, Vector<double>> objectives = null;
            Dictionary<Route, double> fitnesses = null;
            for (int generationCount = 0; generationCount < _maxGenerationCount; generationCount++)
            {
                population = population.Union(archive).ToList();
                objectives = CalculateObjectives(population);
                fitnesses = Fitness.FitnessValues(population, objectives);
                archive = GetNonDominated(population, fitnesses);
                archive = ExternalPopulationCountRegulator(archive, population, fitnesses);
                var tournired = Tournament(archive, fitnesses);
                var crossed = Crossover(tournired);
                var mutated = Mutation(crossed);
                population = _filter.Filter(mutated);
            }
            var result = GetNonDominated(archive, fitnesses);
            if (result.Count > _externalPopulationCount)
            {
                result = Cluster(result,fitnesses);
            }
            LastResult = new Result(result,_objectives);
            OnSolved(LastResult);
            return result;

        }

        private List<Route> ExternalPopulationCountRegulator(List<Route> archive, List<Route> population, Dictionary<Route, double> fitnesses )
        {
            if (archive.Count < _externalPopulationCount)
                return PopulateWithRemainingBest(population, fitnesses, archive);
            return Cluster(archive, fitnesses);
        }

        private List<Route> PopulateWithRemainingBest(IEnumerable<Route> set, Dictionary<Route, double> fitnesses, 
            IEnumerable<Route> archive)
        {
            var newArchive = new List<Route>(archive);
            var orderedroutes = set.Distinct().OrderBy(x => fitnesses[x]).Take(_externalPopulationCount - archive.Count());
            newArchive.AddRange(orderedroutes);
            return newArchive;
        }

        private List<Route> GetNonDominated(IEnumerable<Route> set, Dictionary<Route, double> fitnesses)
        {
            return set.Where(x => fitnesses[x] < 1.0).ToList();
        }

        private List<Route> Tournament(IEnumerable<Route> set, Dictionary<Route, double> fitnesses)
        {
            return _geneticOperators.Tournament(set, fitnesses, _populationCount);
        } 

        private List<Route> Crossover(IEnumerable<Route> population)
        {
            var ret = new List<Route>();
            var populationCopy = population.ToList();
            for (var i = 0; i < populationCopy.Count; i++)
            {
                var first = populationCopy[i];
                for (var j = i + 1; j < populationCopy.Count; j++)
                {
                    var second = populationCopy[j];
                    ret.AddRange(_geneticOperators.Crossover(first, second));
                }
            }
            return ret;
        }

        private List<Route> Mutation(IEnumerable<Route> population)
        {
            var list = _graph.ToList();
            return  population.Select(x => _geneticOperators.Mutation(x,list)).ToList();
        }

        private List<Route> Cluster(List<Route> population, Dictionary<Route,double> fitnesses)
        {           
            population.Distinct();
            if (population.Count>_externalPopulationCount)
                return population.OrderBy(x => fitnesses[x]).Take(_externalPopulationCount).ToList();
            return population.ToList();
        }
        
        private Dictionary<Route, Vector<double>> CalculateObjectives(IEnumerable<Route> population)
        {
            var obj = new Dictionary<Route, Vector<double>>();
            population.ForEach(x => obj.TryAdd(x, new Vector<double>(_objectives.Select(func => func(x)))));
            return obj;
        }

        protected virtual void OnSolved(Result result)
        {
            var handler = Solved;
            if (handler != null) handler(this, new SolvedEventArgs(result));
        }
    }
}