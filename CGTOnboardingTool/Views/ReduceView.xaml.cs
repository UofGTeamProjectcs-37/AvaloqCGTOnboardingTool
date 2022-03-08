using CGTOnboardingTool.Models.DataModels;
using System;
using System.Windows;
using System.Windows.Controls;

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
            LblReduceComboBoxIncorrect.Visibility = Visibility.Hidden;
            ReduceComboBoxBorder.BorderThickness = new Thickness(0);
            LblReduceDateIncorrect.Visibility = Visibility.Hidden;
            TxtReduceDate.BorderThickness = new Thickness(0);
            LblReduceQuantityIncorrect.Visibility = Visibility.Hidden;
            TxtReduceQuantity.BorderThickness = new Thickness(0);
            LblReducePriceIncorrect.Visibility = Visibility.Hidden;
            TxtReducePrice.BorderThickness = new Thickness(0);
            LblReduceCostIncorrect.Visibility = Visibility.Hidden;
            TxtReduceCost.BorderThickness = new Thickness(0);

            if ((Security)DropReduceSecurities.SelectedItem == null)
            {
                LblReduceComboBoxIncorrect.Visibility = Visibility.Visible;
                ReduceComboBoxBorder.BorderThickness = new Thickness(5);

                return true;
            }

            try
            {
                ParseDate(TxtReduceDate.Text);
            }
            catch
            {
                LblReduceDateIncorrect.Visibility = Visibility.Visible;
                TxtReduceDate.BorderThickness = new Thickness(5);

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtReduceQuantity.Text);
            }
            catch
            {
                LblReduceQuantityIncorrect.Visibility = Visibility.Visible;
                TxtReduceQuantity.BorderThickness = new Thickness(5);
                return true;
            }

            try
            {
                Convert.ToDecimal(TxtReducePrice.Text);
            }
            catch
            {
                LblReducePriceIncorrect.Visibility = Visibility.Visible;
                TxtReducePrice.BorderThickness = new Thickness(5);
                return true;
            }

            try
            {
                Convert.ToDecimal(TxtReduceCost.Text);
            }
            catch
            {
                LblReduceCostIncorrect.Visibility = Visibility.Visible;
                TxtReduceCost.BorderThickness = new Thickness(5);
                return true;
            }

            return false;
        }
    }
}
