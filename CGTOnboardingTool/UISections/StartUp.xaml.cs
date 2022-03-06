using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CGTOnboardingTool.UISections
{
    /// <summary>
    /// Interaction logic for StartUp.xaml
    /// </summary>
    public partial class StartUp : Page
    {
        public Report report;
        public Grid grid;
        public StartUp(ref Report Report, ref Grid MainWindowGrid)
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
            this.NavigationService.Navigate(new Dashboard(ref report, ref client, ref tax));
        }

        // Cancel button functionality
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            StartButtonsGrid.Visibility = Visibility.Visible;
            StartNewGrid.Visibility = Visibility.Hidden;
        }
    }

   
}
