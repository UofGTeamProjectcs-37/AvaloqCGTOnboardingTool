using CGTOnboardingTool.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

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
                Decimal userInputQuantity = Convert.ToDecimal(TxtBuildQuantity_P_C.Text);
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
            //BuildComboBoxBorder.BorderThickness = new Thickness(0);
            TxtBuildDate.BorderThickness = new Thickness(0);
            //TxtBuildQuantity.BorderThickness = new Thickness(0);
            TxtBuildPrice.BorderThickness = new Thickness(0);
            TxtBuildCost.BorderThickness = new Thickness(0);

            if ((Security)DropBuildSecurities.SelectedItem == null)
            {
                DropBuildSecurities.Text = "Please Select a Security";
                //BuildComboBoxBorder.BorderThickness = new Thickness(5);
                   
                return true;
            }

            try
            {
                ParseDate(TxtBuildDate.Text);
            } 
            catch
            {
                TxtBuildDate.BorderThickness = new Thickness(5);
                TxtBuildDate.Text = "";
                TextBoxHelper.SetWatermark(TxtBuildDate, "Please ensure Date is in the format (yyyy/mm/dd)");

                return true;
            }

            //try
            //{
            //    Convert.ToDecimal(TxtBuildQuantity.Text);
            //}
            //catch
            //{
            //    TxtBuildQuantity.BorderThickness = new Thickness(5);
            //    TxtBuildQuantity.Text = "";
            //    TextBoxHelper.SetWatermark(TxtBuildQuantity, "Please ensure Quantity only Contains Integers and/or is in Decimal Format");

            //    return true;
            //}

            try
            {
                Convert.ToDecimal(TxtBuildPrice.Text);
            }
            catch
            {
                TxtBuildPrice.BorderThickness = new Thickness(5);
                TxtBuildPrice.Text = "";
                TextBoxHelper.SetWatermark(TxtBuildPrice, "Please ensure Price only Contains Integers and/or is in Decimal Format");

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtBuildCost.Text);
            } 
            catch 
            {
                TxtBuildCost.BorderThickness = new Thickness(5);
                TxtBuildCost.Text = "";
                TextBoxHelper.SetWatermark(TxtBuildCost, "Please ensure Cost only Contains Integers and/or is in Decimal Format");

                return true;
            }

            return false;
        }
    }
}
