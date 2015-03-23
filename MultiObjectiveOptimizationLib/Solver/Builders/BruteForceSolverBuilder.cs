using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver.Classic;

namespace MultiObjectiveOptimizationLib.Solver.Builders
{
    public class BruteForceSolverBuilder :AbstractSolverBuilder
    {
        private double _step = 0.001;
        private EScalarizator _scalarizatorType = EScalarizator.WeightedSum;
        private IScalarizator<Route> _scalarizator;

        public void SetScalarizationType(EScalarizator type)
        {
            _scalarizatorType = type;
        }

        public void SetScalariztionStep(double step)
        {
            _step = step;
        }

        private void ScalarizatorInit()
        {
            switch (_scalarizatorType)
            {
                    case EScalarizator.WeightedSum: _scalarizator = new WeightedSumScalarizator<Route>(_step);
                    break;
            }
        }

        public override ISolver GetResult()
        {
            CheckForEmpty();
            ScalarizatorInit();
            return new BruteForceSolver(_objectives, _filter, _scalarizator, _graph);
        }
        
    }
}