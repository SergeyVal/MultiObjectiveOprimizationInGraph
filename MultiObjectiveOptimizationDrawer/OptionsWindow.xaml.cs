using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using MultiObjectiveOptimizationDrawer.Events;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.FileManager;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver;
using MultiObjectiveOptimizationLib.Solver.Builders;
using MultiObjectiveOptimizationLib.Solver.Classic;
using MultiObjectiveOptimizationLib.Solver.MeticsAndConstraints;

namespace MultiObjectiveOptimizationDrawer
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private readonly GeneticSolverBuilder _geneticSolverBuilder;
        private readonly BruteForceSolverBuilder _bruteForceSolverBuilder;
        public event OptionsChanged Changed;
        public OptionsWindow(GeneticSolverBuilder geneticSolverBuilder, BruteForceSolverBuilder bruteForceSolverBuilder)
        {
            _geneticSolverBuilder = geneticSolverBuilder;
            _bruteForceSolverBuilder = bruteForceSolverBuilder;
            InitializeComponent();
            InitializeTextBoxes();
            GetOptions();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void InitializeTextBoxes()
        {
            BandwidthOfFlowTextBox.Text = RouteConstraints.FlowBandwidth.ToString();
            BandwidthAverageTextBox.Text = BandWidthFactory.Average.ToString();
            BandwidthDeviationTextBox.Text = BandWidthFactory.Deviation.ToString();
            LambdaTextBox.Text = RouteMetrics.Lambda.ToString();
            ScalarizationTypeComboBox.ItemsSource = Enum.GetValues(typeof(EScalarizator)).Cast<EScalarizator>();
            ScalarizationTypeComboBox.SelectedItem = EScalarizator.WeightedSum;
        }
        
        private void GetOptions()
        {
            GetGeneralOptions();
            GetBruteForceOptions();
            GetGeneticOptions();
        }

        private void GetGeneralOptions()
        {
            BandWidthFactory.Average = BandwidthAverageTextBox.Text.ToDouble();
            BandWidthFactory.Deviation = BandwidthDeviationTextBox.Text.ToDouble();
            RouteMetrics.Lambda = LambdaTextBox.Text.ToDouble();
            RouteConstraints.FlowBandwidth = BandwidthOfFlowTextBox.Text.ToDouble();
        }

        private void GetBruteForceOptions()
        {
            EScalarizator chosed = EScalarizator.WeightedSum;
            EScalarizator.TryParse(ScalarizationTypeComboBox.SelectedItem.ToString(), true, out chosed);
            _bruteForceSolverBuilder.SetScalarizationType(chosed);
            _bruteForceSolverBuilder.SetScalariztionStep(ScalarizationStepTextBox.Text.ToDouble());
        }

        private void GetGeneticOptions()
        {
            _geneticSolverBuilder.SetMaxGenerationCount(MaxGenerationTextBox.Text.ToInt());
            _geneticSolverBuilder.SetPopulationsCounts(PopulationTextBox.Text.ToInt(),
                ExternalPopulationTextBox.Text.ToInt(), InitPopulationTextBox.Text.ToInt());
            _geneticSolverBuilder.SetProbabilities(BfsChildAddingProbTextBox.Text.ToInt(),
                MutationProbTextBox.Text.ToInt(), CrossoverProbTextBox.Text.ToInt());
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetOptions();
                Hide();
                OnChanged();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Invalid Input");
                Log.Save(exception);
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetOptions();
                OnChanged();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Invalid Input");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }


        protected virtual void OnChanged()
        {
            var handler = Changed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
