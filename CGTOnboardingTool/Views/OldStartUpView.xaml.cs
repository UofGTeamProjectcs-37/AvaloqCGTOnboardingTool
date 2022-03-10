using CGTOnboardingTool.Models.DataModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views
{
    /// <summary>
    /// Interaction logic for OldStartUpView.xaml
    /// </summary>
    public partial class OldStartUpView : Page
    {
        public Report report;
        public Grid grid;
        public OldStartUpView(ref Report Report, ref Grid MainWindowGrid)
        {
            // Initialise report 
            InitializeComponent();
            report = Report;
            grid = MainWindowGrid;

        }


        private void btnStartUpNew_Click(object sender, RoutedEventArgs e)
        {
            StartNewGrid.Visibility = Visibility.Visible;
            StartButtonsGrid.Visibility = Visibility.Hidden;
        }

        // Start button functionality
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            String client = TxtStartUpClient.Text;
            String tax = TxtStartUpTax.Text;

            grid.Visibility = Visibility.Visible;
            this.NavigationService.Navigate(new OldDashboardView(ref report, ref client, ref tax));
        }

        // Cancel button functionality
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            StartButtonsGrid.Visibility = Visibility.Visible;
            StartNewGrid.Visibility = Visibility.Hidden;
        }
    }

   
}
