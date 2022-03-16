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
    /// Interaction logic for RebuildView.xaml
    /// </summary>
    public partial class RebuildView : Page
    {
        private MetroWindow window;
        private RebuildViewModel viewModel;

        public RebuildView(MetroWindow window, RebuildViewModel viewModel)
        {
            InitializeComponent();
            this.window = window;
            this.viewModel = viewModel;
            initExistingSecuirtyDropdown();
            initBuildSecurityDropdown();
        }

        private void initExistingSecuirtyDropdown()
        {
            List<DropDownItem> selections = new List<DropDownItem>();

            Security[] securities = viewModel.GetSecuritiesExisting();
            foreach (Security security in securities)
            {
                DropDownItem dropDownItem = new DropDownItem();
                dropDownItem.Text = security.ToString();
                dropDownItem.Value = security;
                selections.Add(dropDownItem);
            }

            DropRebuildOldSecurity.ItemsSource = selections;
        }

        private void initBuildSecurityDropdown()
        {
            List<DropDownItem> selections = new List<DropDownItem>();

            Security[] securities = viewModel.GetSecuritiesBuild();
            foreach (Security security in securities)
            {
                DropDownItem dropDownItem = new DropDownItem();
                dropDownItem.Text = security.ToString();
                dropDownItem.Value = security;
                selections.Add(dropDownItem);
            }

            DropRebuildNewSecurity.ItemsSource = selections;
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
            Report report = viewModel.GetReport();
            DashboardViewModel dashViewModel = new DashboardViewModel(ref report);
            this.NavigationService.Navigate(new DashboardView(window, dashViewModel));
        }

        // Do not let user rebuild same security
        //private void DropRebuildOldSecurity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    DropRebuildNewSecurity.Items.Clear();

        //    var selected = DropRebuildOldSecurity.SelectedItem;

        //    foreach (var sec in securities)
        //    {
        //        if (!sec.Equals(selected))
        //        {
        //            DropRebuildNewSecurity.Items.Add(sec);
        //        }
        //    }
        //}

        // Save button functionality
        private void BtnRebuild_Click(object sender, RoutedEventArgs e)
        {
            // Returns true if input is not in the correct format
            bool valid = Validate();

            if (!valid)
            {
                // Read in all user input
                viewModel.date = ParseDate(TxtRebuildDate.Text);


                var selectedSecurityOld = DropRebuildOldSecurity.SelectedItem as DropDownItem;
                viewModel.securityOld = (Security)selectedSecurityOld.Value;

                var selectedSecurityNew = DropRebuildNewSecurity.SelectedItem as DropDownItem;
                viewModel.securityNew = (Security)selectedSecurityNew.Value;

                viewModel.quantityOldReduce = decimal.Parse(TxtRebuildOldQuantityReduce.Text);
                viewModel.quantityNewBuild = decimal.Parse(TxtRebuildNewQuantity.Text);

                // Perform the rebuild
                int err;
                string errMessage;
                viewModel.PerformCGTFunction(out err, out errMessage);
                if (err == 0)
                {
                    Report report = viewModel.GetReport();
                    DashboardViewModel dashboardViewModel = new DashboardViewModel(ref report);
                    this.NavigationService.Navigate(new DashboardView(window, dashboardViewModel));
                }
                else
                {
                    window.ShowMessageAsync("Error: " + (RebuildViewModel.CGTREBUILD_ERROR)err, errMessage);
                }  
            }
        }

        // Checks all inputs are in the correct format
        private bool Validate()
        {
            // Resets any previous incorrect validations
            TxtRebuildDate.BorderThickness = new Thickness(0);
            RebuildComboBoxOldBorder.BorderThickness = new Thickness(0);
            RebuildComboBoxNewBorder.BorderThickness = new Thickness(0);
            TxtRebuildOldQuantityReduce.BorderThickness = new Thickness(0);
            TxtRebuildNewQuantity.BorderThickness = new Thickness(0);

            try
            {
                ParseDate(TxtRebuildDate.Text);
            }
            catch
            {
                TxtRebuildDate.BorderThickness = new Thickness(5);
                TxtRebuildDate.Text = "";
                TextBoxHelper.SetWatermark(TxtRebuildDate, "Please ensure Date is in the format (dd/mm/yyyy)");

                return true;
            }

            if ((Security)DropRebuildOldSecurity.SelectedItem == null)
            {
                DropRebuildOldSecurity.Text = "Please Select a Security";
                RebuildComboBoxOldBorder.BorderThickness = new Thickness(5);

                return true;
            }

            if ((Security)DropRebuildNewSecurity.SelectedItem == null)
            {
                DropRebuildNewSecurity.Text = "Please Select a Security";
                RebuildComboBoxNewBorder.BorderThickness = new Thickness(5);

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtRebuildOldQuantityReduce.Text);
            }
            catch
            {
                TxtRebuildOldQuantityReduce.BorderThickness = new Thickness(5);
                TxtRebuildOldQuantityReduce.Text = "";
                TextBoxHelper.SetWatermark(TxtRebuildOldQuantityReduce, "Please ensure Quantity only Contains Integers and/or is in Decimal Format");

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtRebuildNewQuantity.Text);
            }
            catch
            {
                TxtRebuildNewQuantity.BorderThickness = new Thickness(5);
                TxtRebuildNewQuantity.Text = "";
                TextBoxHelper.SetWatermark(TxtRebuildNewQuantity, "Please ensure Quantity only Contains Integers and/or is in Decimal Format");

                return true;
            }

            return false;
        }
    }
}
