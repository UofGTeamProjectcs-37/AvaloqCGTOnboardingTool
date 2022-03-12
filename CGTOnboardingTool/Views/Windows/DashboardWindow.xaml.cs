using CGTOnboardingTool.Models.DataModels;
using System.Windows;

namespace CGTOnboardingTool.Views.Windows
{
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
            mainFrame.Navigate(new DashboardView(ref report));
        }

        private void btnBuild_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new BuildView(ref report));
        }

        private void btnReduce_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ReduceView(ref report));

        }

        private void btnRebuild_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new RebuildView(ref report));

        }

        private void btnAddNewSec_Click(object sender, RoutedEventArgs e)
        {
            //mainFrame.Navigate(new AddSecurityView(ref report));
            AddSecurityWindow addSecurityWindow = new AddSecurityWindow();
            addSecurityWindow.Show();

        }
    }
}