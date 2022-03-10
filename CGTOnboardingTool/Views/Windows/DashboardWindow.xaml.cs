using CGTOnboardingTool.Models.DataModels;
using System.Windows;

namespace CGTOnboardingTool.Views.Windows
{

        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        public partial class DashboardWindow
        {
            public Report report;

            public DashboardWindow()
            {
                InitializeComponent();
                report = new Report();
                mainFrame.Navigate(new Views.DashboardView(ref report));
            }

            private void btnBuild_Click(object sender, RoutedEventArgs e)
            {
                mainFrame.Navigate(new Views.BuildView(ref report));
            }

            private void btnReduce_Click(object sender, RoutedEventArgs e)
            {
                mainFrame.Navigate(new Views.ReduceView(ref report));

            }

            private void btnRebuild_Click(object sender, RoutedEventArgs e)
            {
                mainFrame.Navigate(new Views.RebuildView(ref report));

            }

            private void btnAddNewSec_Click(object sender, RoutedEventArgs e)
            {
                mainFrame.Navigate(new Views.AddSecurityView(ref report));

            }
        }
    }