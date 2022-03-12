using CGTOnboardingTool.Models.DataModels;
using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace CGTOnboardingTool.Views
{
    /// <summary>
    /// Interaction logic for ReduceView.xaml
    /// </summary>
    public partial class ReduceView : Page
    {
        public Report report;

        public ReduceView(ref Report report)
        {
            InitializeComponent();
            this.report = report;

            DropReduceSecurities.ItemsSource = this.report.GetSecurities();
        }

        // Function to split user given date
        private static DateOnly ParseDate(string dateStr)
        {
            var yymmdd = dateStr.Split('/');
            int year = int.Parse(yymmdd[0]);
            int month = int.Parse(yymmdd[1]);
            int day = int.Parse(yymmdd[2]);

            return new DateOnly(year, month, day);
        }

        // Cancel button navigation
        private void BtnReduceCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashboardView(ref report));
        }

        // Save button functionality
        private void BtnReduce_Click(object sender, RoutedEventArgs e)
        {
            // Returns true if input is not in the correct format
            bool incorrect = Validate();

            if (!incorrect)
            {
                // Read in all user input
                var userInputSecurity = DropReduceSecurities.SelectedItem as Security;
                var userInputDate = ParseDate(TxtReduceDate.Text);
                var userInputQuantity = Convert.ToDecimal(TxtReduceQuantity.Text);
                var userInputPrice = Convert.ToDecimal(TxtReducePrice.Text);
                var userInputCost = Convert.ToDecimal(TxtReduceCost.Text);

                // Perform the reduce
                ViewModels.ReduceViewModel r = new ViewModels.ReduceViewModel(security: userInputSecurity, quantity: userInputQuantity, pps: userInputPrice, cost: userInputCost, date: userInputDate);
                r.perform(ref report);

                this.NavigationService.Navigate(new DashboardView(ref report));
            }
        }

        // Checks all inputs are in the correct format
        private bool Validate()
        {
            // Resets any previous incorrect validations
            ReduceComboBoxBorder.BorderThickness = new Thickness(0);
            TxtReduceDate.BorderThickness = new Thickness(0);
            TxtReduceQuantity.BorderThickness = new Thickness(0);
            TxtReducePrice.BorderThickness = new Thickness(0);
            TxtReduceCost.BorderThickness = new Thickness(0);

            if ((Security)DropReduceSecurities.SelectedItem == null)
            {
                DropReduceSecurities.Text = "Please Select a Security";
                ReduceComboBoxBorder.BorderThickness = new Thickness(5);

                return true;
            }

            try
            {
                ParseDate(TxtReduceDate.Text);
            }
            catch
            {
                TxtReduceDate.BorderThickness = new Thickness(5);
                TxtReduceDate.Text = "";
                TextBoxHelper.SetWatermark(TxtReduceDate, "Please ensure Date is in the format (yyyy/mm/dd)");

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtReduceQuantity.Text);
            }
            catch
            {
                TxtReduceQuantity.BorderThickness = new Thickness(5);
                TxtReduceQuantity.Text = "";
                TextBoxHelper.SetWatermark(TxtReduceQuantity, "Please ensure Quantity only Contains Integers and/or is in Decimal Format");

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtReducePrice.Text);
            }
            catch
            {
                TxtReducePrice.BorderThickness = new Thickness(5);
                TxtReducePrice.Text = "";
                TextBoxHelper.SetWatermark(TxtReducePrice, "Please ensure Price only Contains Integers and/or is in Decimal Format");

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtReduceCost.Text);
            }
            catch
            {
                TxtReduceCost.BorderThickness = new Thickness(5);
                TxtReduceCost.Text = "";
                TextBoxHelper.SetWatermark(TxtReduceCost, "Please ensure Cost only Contains Integers and/or is in Decimal Format");

                return true;
            }

            return false;
        }
    }
}
