using CGTOnboardingTool.Securities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CGTOnboardingTool.UISections
{
    /// <summary>
    /// Interaction logic for Build.xaml
    /// </summary>
    public partial class Build : Page
    {
        public Report report;

        public Build(ref Report report)
        {
            InitializeComponent();
            this.report = report;
        }

        private void BtnBuildCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Dashboard(ref report));
        }

        private void BtnBuildComplete_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
