using System;
using System.Collections.Generic;

namespace MultiObjectiveOptimizationLib.Solver.Classic
{
    public interface IScalarizator<T>
    {
        List<T> GetParetoFront(List<Func<T, double>> functions, List<T> domainSet);
    }
}