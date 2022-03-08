using CGTOnboardingTool.Models.DataModels;
using System.Windows;

namespace CGTOnboardingTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Report report;

        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            report = new Report();
            mainFrame.Navigate(new Views.StartUpView(ref report, ref MainWindowGrid));
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
