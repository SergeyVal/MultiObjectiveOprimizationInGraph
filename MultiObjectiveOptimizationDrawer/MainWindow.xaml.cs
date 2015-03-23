using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.XPath;
using Microsoft.Win32;
using MultiObjectiveOptimizationDrawer.Data;
using MultiObjectiveOptimizationDrawer.Rendering;
using MultiObjectiveOptimizationLib.FileManager;
using MultiObjectiveOptimizationLib.FileManager.XmlFileManager;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver;
using MultiObjectiveOptimizationLib.Solver.Builders;

namespace MultiObjectiveOptimizationDrawer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Render _render;
        private readonly NodeCollectionXmlLoader<FullConnectedGraph> _loader = new NodeCollectionXmlLoader<FullConnectedGraph>();
        private readonly NodeCollectionXmlSaver _saver = new NodeCollectionXmlSaver();
        private readonly NodeCollectionStorage<FullConnectedGraph> _nodeCollectionStorage = new NodeCollectionStorage<FullConnectedGraph>();
        private readonly SolversHolder _solversHolder;
        private readonly OptionsWindow _optionsWindow;
        private readonly ResultsWindow _resultsWindow;

        public MainWindow()
        {
            InitializeComponent();
            var bruteForceSolverBuilder = new BruteForceSolverBuilder();
            var geneticSolverBuilder = new GeneticSolverBuilder();
            _optionsWindow = new OptionsWindow(geneticSolverBuilder, bruteForceSolverBuilder);
            _solversHolder = new SolversHolder(_nodeCollectionStorage,geneticSolverBuilder,bruteForceSolverBuilder,_optionsWindow);
            _resultsWindow = new ResultsWindow(_solversHolder, _nodeCollectionStorage);
            _render = new Render(MainCanvas, _nodeCollectionStorage, _solversHolder);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        private async void Solve(ISolver solver)
        {
            SolvingProgressBar.Visibility = Visibility.Visible;
            /*When "Just My Code" is enabled, Visual Studio in some cases will break on the line that throws the 
             * exception and display an error message that says "exception not handled by user code." This error 
             * is benign. You can press F5 to continue and see the exception-handling behavior that is demonstrated 
             * in these examples. To prevent Visual Studio from breaking on the first error, just uncheck the Enable 
             * Just My Code checkbox under Tools, Options, Debugging, General.*/
            var solverTask = Task.Factory.StartNew(() =>
                solver.Solve(_nodeCollectionStorage.GetStartNode(), _nodeCollectionStorage.GetEndNode()));
            try
            {
                await solverTask;
                _render.DrawResults(solver.LastResult);
                _resultsWindow.DrawResults(solver.LastResult);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Log.Save(e);
            }
            finally
            {
                SolvingProgressBar.Visibility = Visibility.Hidden;
            }
        }
        
        private void OptionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _optionsWindow.Show();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _nodeCollectionStorage.Clear();
            MainCanvas.Children.Clear();
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                FileName = "graph",
                DefaultExt = ".xml",
                Filter = "Xml documents (.xml)|*.xml"
            };
            var result = saveFileDialog.ShowDialog();
            if (result != true) return;
            var filename = saveFileDialog.FileName;
            _saver.SaveToFile(filename, _nodeCollectionStorage.GetNodeCollection());
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".xml", 
                Filter = "Xml documents (.xml)|*.xml"
            };
            var result = openFileDialog.ShowDialog();
            if (result != true) return;
            var filename = openFileDialog.FileName;
            _nodeCollectionStorage.SetNodeCollection(_loader.LoadFromFile(filename));
        }

        private void BruteForceSolve_Click(object sender, RoutedEventArgs e)
        {
            Solve(_solversHolder.BruteForceSolver);
        }

        private void GeneticSolve_Click(object sender, RoutedEventArgs e)
        {
            Solve(_solversHolder.GeneticSolver);
        }

        
        
        
    }
}
