using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver;
using MultiObjectiveOptimizationLib.Solver.Builders;
using MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints;

namespace MultiObjectiveOptimizationDrawer.Data
{
    public class SolversHolder
    {
        public event SolvedEvent Solved;
        private readonly GeneticSolverBuilder _geneticSolverBuilder;
        private readonly BruteForceSolverBuilder _bruteForceSolverBuilder;
        private readonly NodeCollectionStorage<FullConnectedGraph> _storage; 
        private Dictionary<AbstractSolverBuilder, bool> _optionsChanged;
        private Dictionary<AbstractSolverBuilder, bool> _storageChanged;
        private ISolver _bruteForceSolver;
        private ISolver _geneticSolver;

        public SolversHolder(NodeCollectionStorage<FullConnectedGraph> storage, GeneticSolverBuilder geneticSolverBuilder, 
            BruteForceSolverBuilder bruteForceSolverBuilder, OptionsWindow optionsWindow)
        {
            _storage = storage;
            _geneticSolverBuilder = geneticSolverBuilder;
            _bruteForceSolverBuilder = bruteForceSolverBuilder;
            _storage.CollectionChanged += StorageChangedHandler;
            optionsWindow.Changed += OptionsChangedHandler;
            InitializeSolvers();
            InitializeFlagDictionarys();
        }

        private void InitializeFlagDictionarys()
        {
            _optionsChanged = new Dictionary<AbstractSolverBuilder, bool>()
            {
                {_geneticSolverBuilder, false},
                {_bruteForceSolverBuilder, false}
            };
            _storageChanged = new Dictionary<AbstractSolverBuilder, bool>()
            {
                {_geneticSolverBuilder, false},
                {_bruteForceSolverBuilder, false}
            };
        }

        public ISolver GeneticSolver
        {
            get { return GetSolver(_geneticSolver, _geneticSolverBuilder); }
        }

        public ISolver BruteForceSolver
        {
            get { return GetSolver(_bruteForceSolver, _bruteForceSolverBuilder); }
        }

        private ISolver GetSolver(ISolver solver, AbstractSolverBuilder builder)
        {
            if (!IsNecessaryToCreateSolver(solver, builder)) return solver;
            if (_storageChanged[builder]) builder.SetGraph(_storage.GetNodeCollection());
            if (solver != null) solver.Solved -= OnSolved;
            solver = builder.GetResult();
            solver.Solved += OnSolved;
            _storageChanged[builder] = false;
            _optionsChanged[builder] = false;
            return solver;
        }

        private void InitializeSolvers()
        {
            _geneticSolverBuilder.SetGraph(_storage.GetNodeCollection());
            _bruteForceSolverBuilder.SetGraph(_storage.GetNodeCollection());
            InitializeSolversFuncs();
        }

        private void InitializeSolversFuncs()
        {
            var len = new Func<Route, double>(RouteMetrics.Length);
            var maxlen = new Func<Route, double>(x => RouteMetrics.MaxLength(x));
            var noloops = new Func<Route, bool>(RouteConstraints.NoLoops);
            _geneticSolverBuilder.AddConstraint(noloops);
            _bruteForceSolverBuilder.AddConstraint(noloops);
            _geneticSolverBuilder.AddObjective(len);
            _geneticSolverBuilder.AddObjective(maxlen);
            _bruteForceSolverBuilder.AddObjective(len);
            _bruteForceSolverBuilder.AddObjective(maxlen);
        }
        
        private bool IsNecessaryToCreateSolver(ISolver solver, AbstractSolverBuilder builder)
        {
            return solver == null || _optionsChanged[builder] || _storageChanged[builder];
        }

        private void OptionsChangedHandler(object sender, EventArgs e)
        {
            _optionsChanged[_geneticSolverBuilder] = true;
            _optionsChanged[_bruteForceSolverBuilder] = true;
        }

        private void StorageChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            _storageChanged[_geneticSolverBuilder] = true;
            _storageChanged[_bruteForceSolverBuilder] = true;
        }

        protected virtual void OnSolved(object sender ,SolvedEventArgs args)
        {
            var handler = Solved;
            if (handler != null) handler(this, args);
        }
    }
}