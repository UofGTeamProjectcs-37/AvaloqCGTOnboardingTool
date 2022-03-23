using CGTOnboardingTool.Models.AccessModels;
using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.ViewModels;
using System.Windows;

namespace CGTOnboardingTool.Views.Windows
{
    //Author: Aidan Neil

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DashboardWindow
    {
        private Report report;
        public DashboardWindow(Report report)
        {
            InitializeComponent();
            this.report = report;
            DashboardViewModel viewModel = new DashboardViewModel(ref report);
            mainFrame.Navigate(new DashboardView(this, viewModel));
            SecurityLoader.init();
        }

        private void btnBuild_Click(object sender, RoutedEventArgs e)
        {
            BuildViewModel build = new BuildViewModel(ref report);
            mainFrame.Navigate(new BuildView(this, build));
        }

        private void btnReduce_Click(object sender, RoutedEventArgs e)
        {
            ReduceViewModel reduce = new ReduceViewModel(ref report);
            mainFrame.Navigate(new ReduceView(this, reduce));

        }

        private void btnRebuild_Click(object sender, RoutedEventArgs e)
        {
            RebuildViewModel rebuild = new RebuildViewModel(ref report);
            mainFrame.Navigate(new RebuildView(this, rebuild));

        }
        private void btnAddNewSec_Click(object sender, RoutedEventArgs e)
        {
            AddSecurityViewModel viewModel = new();
            AddSecurityWindow addSecurityWindow = new AddSecurityWindow(viewModel);
            addSecurityWindow.Show();
        }
    }
}
