using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver.Clustering;
using MultiObjectiveOptimizationLib.Solver.GeneticAlgorythm;

namespace MultiObjectiveOptimizationLib.Solver.Builders
{
    public class GeneticSolverBuilder : AbstractSolverBuilder
    {
        private int _maxGeneration = 1000;
        private int _externalPopulationCount = 0;
        private int _initialPopulationCount = 0;
        private int _populationCount = 0;
        private int _initialPopulationChildAddingProbability = 20;
        private int _mutationProbability = 20;
        private int _crossoverProbability = 20;
        public new void SetGraph(FullConnectedGraph graph)
        {
            _graph = graph;
            if (_externalPopulationCount == 0) _externalPopulationCount = graph.Count*5;
            if (_initialPopulationCount == 0) _initialPopulationCount = graph.Count*5;
        }

        public void SetPopulationsCounts(int populationCount ,int externalPopulationCount, int initialPopulationCount)
        {
            _populationCount = populationCount;
            _externalPopulationCount = externalPopulationCount;
            _initialPopulationCount = initialPopulationCount;
        }

        public void SetProbabilities(int initialPopulationChildAddingProbability, int mutationProbability, int crossoverProbability)
        {
            _initialPopulationChildAddingProbability = initialPopulationChildAddingProbability;
            _mutationProbability = mutationProbability;
            _crossoverProbability = crossoverProbability;
        }

        public void SetMaxGenerationCount(int maxGenerationCount)
        {
            _maxGeneration = maxGenerationCount;
        }

        public override ISolver GetResult()
        {
            return new GeneticMultiObjectiveSolver(_graph, _objectives, _filter, _maxGeneration,
                _externalPopulationCount, _populationCount, new GeneticOperators(_mutationProbability, _crossoverProbability),
                new GraphInitialSetFactory(_initialPopulationChildAddingProbability, _graph, _initialPopulationCount));
        }
    }
}