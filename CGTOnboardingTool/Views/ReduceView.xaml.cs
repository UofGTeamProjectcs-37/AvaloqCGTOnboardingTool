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
            while (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        // Save button functionality
        private void BtnReduce_Click(object sender, RoutedEventArgs e)
        {
            // Returns true if input is not in the correct format
            bool valid = Validate();

            if (!valid)
            {
                // Read in all user input
                viewModel.security = DropReduceSecurities.SelectedItem as Security;
                viewModel.date = ParseDate(TxtReduceDate.Text);
                viewModel.quantity = decimal.Parse(TxtReduceQuantity.Text);
                viewModel.pps = decimal.Parse(TxtReducePrice.Text);
                viewModel.cost = decimal.Parse(TxtReduceCost.Text);

                // Perform the reduce
                int err;
                string errMessage;
                viewModel.PerformCGTFunction(out err, out errMessage);
                // Display error message
                if (err == 0)
                {
                    while (this.NavigationService.CanGoBack)
                    {
                        this.NavigationService.GoBack();
                    }
                }

                window.ShowMessageAsync("Error: " + (BuildViewModel.CGTBUILD_ERROR)err, errMessage);
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
