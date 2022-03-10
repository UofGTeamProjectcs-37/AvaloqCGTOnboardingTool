using CGTOnboardingTool.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views
{
    /// <summary>
    /// Interaction logic for RebuildView.xaml
    /// </summary>
    public partial class RebuildView : Page
    {
        public Report report;
        private List<Security> securities;

        public RebuildView(ref Report report)
        {
            InitializeComponent();
            this.report = report;

            // Create securities to show in drop-down menu 
            Security gsk = new Security("GlaxoSmithKline", "GSK");
            Security fgp = new Security("FGP Systems", "FGP");
            Security ibe = new Security("Iberdrola", "IBE");
            Security tsla = new Security("Tesla", "TSLA");
            Security aapl = new Security("Apple", "AAPL");

            securities = new List<Security>();
            securities.Add(gsk);
            securities.Add(fgp);
            securities.Add(ibe);
            securities.Add(tsla);
            securities.Add(aapl);

            DropRebuildOldSecurity.ItemsSource = this.report.GetSecurities();
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
        private void BtnRebuildCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new OldDashboardView(ref report));
        }

        // Do not let user rebuild same security
        private void DropRebuildOldSecurity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DropRebuildNewSecurity.Items.Clear();

            var selected = DropRebuildOldSecurity.SelectedItem;

            foreach (var sec in securities)
            {
                if (!sec.Equals(selected))
                {
                    DropRebuildNewSecurity.Items.Add(sec);
                }
            }
        }

        // Save button functionality
        private void BtnRebuild_Click(object sender, RoutedEventArgs e)
        {
            // Returns true if input is not in the correct format
            bool incorrect = Validate();

            if (!incorrect)
            {
                // Read in all user input
                var userInputDate = ParseDate(TxtRebuildDate.Text);

                var userInputOldSecurity = DropRebuildOldSecurity.SelectedItem as Security;
                var userInputNewSecurity = DropRebuildNewSecurity.SelectedItem as Security;

                var userInputOldSecuirtyReduce = Convert.ToDecimal(TxtRebuildOldQuantityReduce.Text);
                var userInputNewSecuirtyQuantity = Convert.ToDecimal(TxtRebuildNewQuantity.Text);

                // Perform the rebuild
                ViewModels.RebuildViewModel rb = new ViewModels.RebuildViewModel(oldSecurity: userInputOldSecurity, quantityToReduce: userInputOldSecuirtyReduce, newSecurity: userInputNewSecurity, quantityToBuild: userInputNewSecuirtyQuantity, date: userInputDate);
                rb.perform(ref report);

                this.NavigationService.Navigate(new OldDashboardView(ref report));
            }
        }

        // Checks all inputs are in the correct format
        private bool Validate()
        {
            // Resets any previous incorrect validations
            LblRebuildDateIncorrect.Visibility = Visibility.Hidden;
            TxtRebuildDate.BorderThickness = new Thickness(0);
            LblRebuildOldSecurityIncorrect.Visibility = Visibility.Hidden;
            RebuildComboBoxOldBorder.BorderThickness = new Thickness(0);
            LblRebuildNewSecurityIncorrect.Visibility = Visibility.Hidden;
            RebuildComboBoxNewBorder.BorderThickness = new Thickness(0);
            LblRebuildOldQuantityReduceIncorrect.Visibility = Visibility.Hidden;
            TxtRebuildOldQuantityReduce.BorderThickness = new Thickness(0);
            LblRebuildNewQuantityIncorrect.Visibility = Visibility.Hidden;
            TxtRebuildNewQuantity.BorderThickness = new Thickness(0);

            try
            {
                ParseDate(TxtRebuildDate.Text);
            }
            catch
            {
                LblRebuildDateIncorrect.Visibility = Visibility.Visible;
                TxtRebuildDate.BorderThickness = new Thickness(5);

                return true;
            }

            if ((Security)DropRebuildOldSecurity.SelectedItem == null)
            {
                LblRebuildOldSecurityIncorrect.Visibility = Visibility.Visible;
                RebuildComboBoxOldBorder.BorderThickness = new Thickness(5);

                return true;
            }

            if ((Security)DropRebuildNewSecurity.SelectedItem == null)
            {
                LblRebuildNewSecurityIncorrect.Visibility = Visibility.Visible;
                RebuildComboBoxNewBorder.BorderThickness = new Thickness(5);

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtRebuildOldQuantityReduce.Text);
            } 
            catch
            {
                LblRebuildOldQuantityReduceIncorrect.Visibility = Visibility.Visible;   
                TxtRebuildOldQuantityReduce.BorderThickness = new Thickness(5);

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtRebuildNewQuantity.Text);
            }
            catch
            {
                LblRebuildNewQuantityIncorrect.Visibility = Visibility.Visible;
                TxtRebuildNewQuantity.BorderThickness = new Thickness(5);

                return true;
            }

            return false;
        }
    }
}
