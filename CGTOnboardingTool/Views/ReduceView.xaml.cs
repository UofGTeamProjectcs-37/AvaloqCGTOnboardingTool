using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views
{
    /// <summary>
    /// Interaction logic for ReduceView.xaml
    /// </summary>
    public partial class ReduceView : Page
    {
        private MetroWindow window;
        private ReduceViewModel viewModel;

        public ReduceView(MetroWindow window, ReduceViewModel viewModel)
        {
            InitializeComponent();
            this.window = window;
            this.viewModel = viewModel;
            initSecurityDropdown();
        }

        private void initSecurityDropdown()
        {
            List<DropDownItem> selections = new List<DropDownItem>();

            Security[] securities = viewModel.GetSecurities();
            if (securities.Length > 0)
            {

                foreach (Security security in securities)
                {
                    DropDownItem dropDownItem = new DropDownItem();
                    dropDownItem.Text = security.ToString();
                    dropDownItem.Value = security;
                    selections.Add(dropDownItem);
                }
                DropReduceSecurities.IsEnabled = true;
                DropReduceSecurities.ItemsSource = selections;
            }


        }

        // Cancel button navigation
        private void BtnReduceCancel_Click(object sender, RoutedEventArgs e)
        {
            Report report = viewModel.GetReport();
            DashboardViewModel dashViewModel = new DashboardViewModel(ref report);
            this.NavigationService.Navigate(new DashboardView(window, dashViewModel));
        }

        /// <summary>
        /// Save button functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReduce_Click(object sender, RoutedEventArgs e)
        {
            // Returns true if input is not in the correct format
            bool valid = Validate();

            if (!valid)
            {
                // Read in all user input
                var selected = DropReduceSecurities.SelectedItem as DropDownItem;
                viewModel.security = (Security)selected.Value;
                viewModel.date = ParseDate(TxtReduceDate.Text);

                if (UsingGross.IsSelected)
                {
                    viewModel.quantity = decimal.Parse(TxtReduceQuantity_G.Text);
                    viewModel.gross = decimal.Parse(TxtReduceGross.Text);
                    viewModel.usingGross = true;
                }
                else
                {
                    viewModel.quantity = decimal.Parse(TxtReduceQuantity_P_C.Text);
                    viewModel.pps = decimal.Parse(TxtReducePrice.Text);
                    viewModel.cost = decimal.Parse(TxtReduceCost.Text);
                }

                // Perform the reduce
                int err;
                string errMessage;
                viewModel.PerformCGTFunction(out err, out errMessage);
                // Display error message
                if (err == 0)
                {
                    Report report = viewModel.GetReport();
                    DashboardViewModel dashboardViewModel = new DashboardViewModel(ref report);
                    this.NavigationService.Navigate(new DashboardView(window, dashboardViewModel));
                }
                else
                {
                    window.ShowMessageAsync("Error: " + (BuildViewModel.CGTBUILD_ERROR)err, errMessage);
                }

            }
        }

        // Function to split user given date
        private static DateOnly ParseDate(string dateStr)
        {
            var ddmmyyyy = dateStr.Split('/');

            int day = int.Parse(ddmmyyyy[0]);
            int month = int.Parse(ddmmyyyy[1]);
            int year = int.Parse(ddmmyyyy[2]);

            return new DateOnly(year, month, day);
        }

        /// <summary>
        /// Checks all inputs are in the correct format
        /// </summary>
        private bool Validate()
        {
            // Resets any previous incorrect validations
            //ReduceComboBoxBorder.BorderThickness = new Thickness(0);
            TxtReduceDate.BorderThickness = new Thickness(0);
            //TxtReduceQuantity.BorderThickness = new Thickness(0);
            TxtReducePrice.BorderThickness = new Thickness(0);
            TxtReduceCost.BorderThickness = new Thickness(0);

            var selected = DropReduceSecurities.SelectedItem as DropDownItem;
            if ((Security)selected.Value == null)
            {
                DropReduceSecurities.Text = "Please Select a Security";
                //ReduceComboBoxBorder.BorderThickness = new Thickness(5);

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
                TextBoxHelper.SetWatermark(TxtReduceDate, "Please ensure Date is in the format (dd/mm/yyyy)");

                return true;
            }

            if (UsingGross.IsSelected)
            {
                try
                {
                    Convert.ToDecimal(TxtReduceQuantity_G.Text);
                }
                catch
                {
                    TxtReduceQuantity_G.BorderThickness = new Thickness(5);
                    TxtReduceQuantity_G.Text = "";
                    TextBoxHelper.SetWatermark(TxtReduceQuantity_G, "Please ensure Quantity only Contains Integers and/or is in Decimal Format");

                    return true;
                }

                try
                {
                    Convert.ToDecimal(TxtReduceGross.Text);
                }
                catch
                {
                    TxtReduceGross.BorderThickness = new Thickness(5);
                    TxtReduceGross.Text = "";
                    TextBoxHelper.SetWatermark(TxtReduceGross, "Please ensure Gross only Contains Integers and/or is in Decimal Format");

                    return true;
                }
                return false;
            }
            else
            {

                try
                {
                    Convert.ToDecimal(TxtReduceQuantity_P_C.Text);
                }
                catch
                {
                    TxtReduceQuantity_P_C.BorderThickness = new Thickness(5);
                    TxtReduceQuantity_P_C.Text = "";
                    TextBoxHelper.SetWatermark(TxtReduceQuantity_P_C, "Please ensure Quantity only Contains Integers and/or is in Decimal Format");

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
        private void cbReduceSecurity_Changed(object sender, SelectionChangedEventArgs e)
        {
            var selected = DropReduceSecurities.SelectedItem as DropDownItem;

            DateOnly? date;
            try
            {
                date = Helpers.ParseDateInput.DashSeparated(TxtReduceDate.Text);
            }
            catch
            {
                return;
            }

            var qty = viewModel.GetHoldings((Security)selected.Value, (DateOnly)date);
            if (UsingGross.IsSelected)
            {
                LblReduceHoldings_G.Content = "/" + qty.ToString();
            }
            else
            {
                LblReduceHoldings_P_C.Content = "/" + qty.ToString();
            }
        }

        private void TxtReduceDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            var selected = DropReduceSecurities.SelectedItem as DropDownItem;
            if (selected == null || selected.Value == null)
            {
                return;
            }
            DateOnly? date;
            try
            {
                date = Helpers.ParseDateInput.DashSeparated(TxtReduceDate.Text);
            }
            catch
            {
                return;
            }

            var qty = viewModel.GetHoldings((Security)selected.Value, (DateOnly)date);
            if (UsingGross.IsSelected)
            {
                LblReduceHoldings_G.Content = "/" + qty.ToString();
            }
            else
            {
                LblReduceHoldings_P_C.Content = "/" + qty.ToString();
            }
        }
    }
}
