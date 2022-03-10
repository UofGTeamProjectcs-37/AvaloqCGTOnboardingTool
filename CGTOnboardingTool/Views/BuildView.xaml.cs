using CGTOnboardingTool.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views
{
    /// <summary>
    /// Interaction logic for BuildView.xaml
    /// </summary>
    public partial class BuildView : Page
    {
        public Report report;

        // Function to split user given date
        private static DateOnly ParseDate(string dateStr)
        {
            var yymmdd = dateStr.Split('/');
            int year = int.Parse(yymmdd[0]);
            int month = int.Parse(yymmdd[1]);
            int day = int.Parse(yymmdd[2]);

            return new DateOnly(year, month, day);
        }

        public BuildView(ref Report report)
        {
            InitializeComponent();
            this.report = report;

            // Create securities to show in drop-down menu 
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

            // Drop-down menu 
            DropBuildSecurities.ItemsSource = securities;
            DropBuildSecurities.Text = "Select a Security to BuildView";
        }

        // Cancel button navigation
        private void BtnBuildCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashboardView(ref report));
        }

        // Save button navigation
        private void BtnBuildComplete_Click(object sender, RoutedEventArgs e)
        {
            // Returns true if input is not in the correct format
            bool incorrect = Validate();

            if (!incorrect)
            {
                // Read in all user input 
                Security userInputSecurity = (Security)DropBuildSecurities.SelectedItem;
                DateOnly userInputDate = ParseDate(TxtBuildDate.Text);
                Decimal userInputQuantity = Convert.ToDecimal(TxtBuildQuantity.Text);
                Decimal userInputPrice = Convert.ToDecimal(TxtBuildPrice.Text);
                Decimal userInputCost = Convert.ToDecimal(TxtBuildCost.Text);

                // Perform the build 
                ViewModels.BuildViewModel b = new ViewModels.BuildViewModel(security: userInputSecurity, quantity: userInputQuantity, pps: userInputPrice, cost: userInputCost, date: userInputDate);
                b.perform(ref report);

                this.NavigationService.Navigate(new DashboardView(ref report));
            }
        }

        // Checks all inputs are in the correct format
        private bool Validate()
        {
            // Resets any previous incorrect validations
            LblBuildComboBoxIncorrect.Visibility = Visibility.Hidden;
            BuildComboBoxBorder.BorderThickness = new Thickness(0);
            LblBuildDateIncorrect.Visibility = Visibility.Hidden;
            TxtBuildDate.BorderThickness = new Thickness(0);
            LblBuildQuantityIncorrect.Visibility = Visibility.Hidden;
            TxtBuildQuantity.BorderThickness = new Thickness(0);
            LblBuildPriceIncorrect.Visibility = Visibility.Hidden;
            TxtBuildPrice.BorderThickness = new Thickness(0);
            LblBuildCostIncorrect.Visibility = Visibility.Hidden;
            TxtBuildCost.BorderThickness = new Thickness(0);

            if ((Security)DropBuildSecurities.SelectedItem == null)
            {
                LblBuildComboBoxIncorrect.Visibility = Visibility.Visible;
                BuildComboBoxBorder.BorderThickness = new Thickness(5);
                   
                return true;
            }

            try
            {
                ParseDate(TxtBuildDate.Text);
            } 
            catch
            {
                LblBuildDateIncorrect.Visibility = Visibility.Visible;
                TxtBuildDate.BorderThickness = new Thickness(5);

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtBuildQuantity.Text);
            }
            catch
            {
                LblBuildQuantityIncorrect.Visibility = Visibility.Visible;
                TxtBuildQuantity.BorderThickness = new Thickness(5);
                return true;
            }

            try
            {
                Convert.ToDecimal(TxtBuildPrice.Text);
            }
            catch
            {
                LblBuildPriceIncorrect.Visibility = Visibility.Visible;
                TxtBuildPrice.BorderThickness = new Thickness(5);
                return true;
            }

            try
            {
                Convert.ToDecimal(TxtBuildCost.Text);
            } 
            catch 
            {
                LblBuildCostIncorrect.Visibility = Visibility.Visible;
                TxtBuildCost.BorderThickness = new Thickness(5);
                return true;
            }

            return false;
        }
    }
}
