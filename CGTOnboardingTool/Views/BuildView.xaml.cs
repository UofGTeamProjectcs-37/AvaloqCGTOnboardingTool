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

        //Performs the CGTFunction      
        private void BtnBuildComplete_Click(object sender, RoutedEventArgs e)
        {
            // Returns true if input is not in the correct format
            bool valid = Validate();

            if (!valid)
            {
                // Read in all user input 
                var selected = DropBuildSecurities.SelectedItem as DropDownItem;
                viewModel.security = (Security)selected.Value;
                viewModel.date = ParseDate(TxtBuildDate.Text);
                viewModel.quantity = decimal.Parse(TxtBuildQuantity_P_C.Text);
                viewModel.pps = decimal.Parse(TxtBuildPrice.Text);
                viewModel.cost = decimal.Parse(TxtBuildCost.Text);

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
            //BuildComboBoxBorder.BorderThickness = new Thickness(0);
            TxtBuildDate.BorderThickness = new Thickness(0);
            //TxtBuildQuantity.BorderThickness = new Thickness(0);
            TxtBuildPrice.BorderThickness = new Thickness(0);
            TxtBuildCost.BorderThickness = new Thickness(0);

            if (DropBuildSecurities.SelectedItem == null)
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
