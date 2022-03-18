using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.ViewModels;
using CGTOnboardingTool.Helpers;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
        private MetroWindow window;
        private BuildViewModel viewModel;

        public BuildView(MetroWindow window, BuildViewModel viewModel)
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
            foreach (Security security in securities)
            {
                DropDownItem dropDownItem = new DropDownItem();
                dropDownItem.Text = security.ToString();
                dropDownItem.Value = security;
                selections.Add(dropDownItem);
            }

            DropBuildSecurities.ItemsSource = selections;
        }

        // Cancel button navigation goes back to frame root page (which should be the dashboard)
        private void BtnBuildCancel_Click(object sender, RoutedEventArgs e)
        {
            Report report = viewModel.GetReport();
            DashboardViewModel dashViewModel = new DashboardViewModel(ref report);
            this.NavigationService.Navigate(new DashboardView(window, dashViewModel));
        }

        /// <summary>
        /// Peforms the Build function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuildComplete_Click(object sender, RoutedEventArgs e)
        {
            // Returns true if input is not in the correct format
            bool valid = Validate();

            if (!valid)
            {
                // Read in all user input

                var selected = DropBuildSecurities.SelectedItem as DropDownItem;
                viewModel.security = (Security)selected.Value;
                viewModel.date = Helpers.ParseDateInput.DashSeparated(TxtBuildDate.Text);

                if (UsingGross.IsSelected)
                {
                    viewModel.quantity = decimal.Parse(TxtBuildQuantity_G.Text);
                    viewModel.gross = decimal.Parse(TxtBuildGross.Text);
                    viewModel.usingGross = true;
                }
                else
                {
                    viewModel.quantity = decimal.Parse(TxtBuildQuantity_P_C.Text);
                    viewModel.pps = decimal.Parse(TxtBuildPrice.Text);
                    viewModel.cost = decimal.Parse(TxtBuildCost.Text);
                }

                // Perform the build
                int err;
                string errMessage;
                viewModel.PerformCGTFunction(out err, out errMessage);
                // Display error message
                if (err == 0)
                {
                    Report report = viewModel.GetReport();
                    DashboardViewModel dashViewModel = new DashboardViewModel(ref report);
                    this.NavigationService.Navigate(new DashboardView(window, dashViewModel));
                }
                else
                {
                    window.ShowMessageAsync("Error: " + (BuildViewModel.CGTBUILD_ERROR)err, errMessage);
                }
            }
        }

        /// <summary>
        /// Checks all inputs are in the correct format
        /// </summary>
        private bool Validate()
        {
            // Resets any previous incorrect validations
            //BuildComboBoxBorder.BorderThickness = new Thickness(0);
            TxtBuildDate.BorderThickness = new Thickness(0);
            //TxtBuildQuantity.BorderThickness = new Thickness(0);
            TxtBuildPrice.BorderThickness = new Thickness(0);
            TxtBuildCost.BorderThickness = new Thickness(0);

            if (DropBuildSecurities.SelectedItem == null)
            {
                DropBuildSecurities.Text = "Please Select a Security";

                return true;
            }

            try
            {
                ParseDateInput.DashSeparated(TxtBuildDate.Text);
            }
            catch
            {
                TxtBuildDate.BorderThickness = new Thickness(5);
                TxtBuildDate.Text = "";
                TextBoxHelper.SetWatermark(TxtBuildDate, "Please ensure Date is in the format (dd/mm/yyyy)");

                return true;
            }

            if (UsingGross.IsSelected)
            {
                try
                {
                    Convert.ToDecimal(TxtBuildQuantity_G.Text);
                }
                catch
                {
                    TxtBuildQuantity_G.BorderThickness = new Thickness(5);
                    TxtBuildQuantity_G.Text = "";
                    TextBoxHelper.SetWatermark(TxtBuildQuantity_G, "Please ensure Quantity only Contains Integers and/or is in Decimal Format");

                    return true;
                }

                try
                {
                    Convert.ToDecimal(TxtBuildGross.Text);
                }
                catch
                {
                    TxtBuildGross.BorderThickness = new Thickness(5);
                    TxtBuildGross.Text = "";
                    TextBoxHelper.SetWatermark(TxtBuildGross, "Please ensure Gross only Contains Integers and/or is in Decimal Format");

                    return true;
                }
                return false;
            }
            else
            {

                try
                {
                    Convert.ToDecimal(TxtBuildQuantity_P_C.Text);
                }
                catch
                {
                    TxtBuildQuantity_P_C.BorderThickness = new Thickness(5);
                    TxtBuildQuantity_P_C.Text = "";
                    TextBoxHelper.SetWatermark(TxtBuildQuantity_P_C, "Please ensure Quantity only Contains Integers and/or is in Decimal Format");

                    return true;
                }

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
}
