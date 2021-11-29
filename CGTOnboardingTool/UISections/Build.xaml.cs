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
            var securityName = "GlaxoSmithKlein";
            var securityShortName = "GSK";

            Security security = new Security(securityName, securityShortName);


            var quantity = 1000;
            decimal price = (decimal)16.35;
            decimal cost = (decimal)23.80;
            DateOnly date = new DateOnly(2021, 11, 29);

            Tools.Build build = new Tools.Build(security, quantity, price, cost, 0, date);
            Console.WriteLine(build.ToString);
        }

    }
}
