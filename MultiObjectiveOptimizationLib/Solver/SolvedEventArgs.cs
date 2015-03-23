using System;

namespace MultiObjectiveOptimizationLib.Solver
{
    public class SolvedEventArgs :EventArgs
    {
        public Result Result { get; private set; }

        public SolvedEventArgs(Result result)
        {
            Result = result;
        }
    }
}