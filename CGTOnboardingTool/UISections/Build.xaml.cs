using CGTOnboardingTool.Securities;
using System;
using System.Collections.Generic;
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

            Security gsk = new Security("GlaxoSmithKline", "GSK");
            Security fgp = new Security("FGP Systems", "FGP");
            Security ibe = new Security("Iberdrola", "IBE");
            Security tsla = new Security("Tesla", "TSLA");
            Security aapl = new Security("Apple", "AAPL");

            List<Security> securities = new List<Security>();
            securities.Add(gsk);
            securities.Add(fgp);
            securities.Add(ibe);
            securities.Add(tsla);
            securities.Add(aapl);

            DropBuildSecurities.ItemsSource = securities;
            DropBuildSecurities.Text = "Select a Security to Build";
        }

        private void BtnBuildCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Dashboard(ref report));
        }

        private void BtnBuildComplete_Click(object sender, RoutedEventArgs e)
        {
            var userInputDate = TxtBuildDate.Text;
            var userInputQuantity = TxtBuildQuantity.Text;
            var userInputPrice = TxtBuildPrice.Text;
            var userInputCost = TxtBuildCost.Text;

            //Tools.Build b = new Tools.Build(security: ____ , 
            //    quantity: (decimal)userInputQuantity, 
            //    pps: (decimal)userInputPrice, 
            //    cost: (decimal)userInputCost),
            //    date: (DateOnly) userInputDate);

            //b.perform(ref report);

            this.NavigationService.Navigate(new Dashboard(ref report));
        }

    }
}
