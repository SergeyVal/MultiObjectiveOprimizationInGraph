using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MultiObjectiveOptimizationDrawer.Data;
using MultiObjectiveOptimizationDrawer.Rendering;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver;

namespace MultiObjectiveOptimizationDrawer
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        private readonly ResultRender _render;
        public ResultsWindow(SolversHolder solversHolder, NodeCollectionStorage<FullConnectedGraph> storage )
        {
            InitializeComponent();
            Loaded += delegate
            {
                var t = ResultsCanvas.Width;
            };
            Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Arrange(new Rect(0, 0, Width, Height));
            _render = RenderFactory.GetResultRender(ResultsCanvas, solversHolder, storage);
            //solversHolder.Solved += SolvedHandler;
        }

        public void DrawResults(Result result)
        {
            _render.DrawResults(result);
            Show();
        }

        private void SolvedHandler(object sender, SolvedEventArgs args)
        {
            this.Show();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        

        private void ClearMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _render.Clear();
        }
    }
}
