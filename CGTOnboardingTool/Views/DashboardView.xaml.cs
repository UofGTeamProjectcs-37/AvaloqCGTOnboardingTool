using CGTOnboardingTool.Models.AccessModels;
using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.Models.OutputModels;
using CGTOnboardingTool.ViewModels;
using CGTOnboardingTool.Views.Controls.DashboardView;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Page
    {

        public MetroWindow window;
        public DashboardViewModel viewModel;

        private UIElement filterControl;

        private string[] filters = new string[] {
            "Date",
            "Security",
            "Function",
        };

        public DashboardView(MetroWindow window, DashboardViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            LblClientName.Content = viewModel.GetClientName();
            LblTaxYear.Content = viewModel.GetYearStart() + " - " + viewModel.GetYearEnd();
            cbFilterType.ItemsSource = filters;
            FilterInputContainer.Children.Add(new FilterByDate());
            display(viewModel.Rows());
        }

        // Open button functionality
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report();

            ReportLoader importer = new ReportLoader(ref report);
            importer.ImportReport();

            this.viewModel = new DashboardViewModel(ref report);
            LblClientName.Content = viewModel.GetClientName();
            LblTaxYear.Content = viewModel.GetYearStart() + " - " + viewModel.GetYearEnd();
            display(viewModel.Rows());

        }

        // Save button functionality
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Report report = viewModel.GetReport();
            ReportExporter.ExportToCSV(ref report); ;
        }

        private void display(ReportEntry[] rows)
        {
            List<DisplayRow> displayRows = new List<DisplayRow>();

            foreach (ReportEntry row in rows)
            {
                string strFunction = row.PrintFunction();

                string strDate = row.PrintDate();

                string strSecurity = row.PrintSecurity();

                string strPrice = row.PrintPrice();

                string strQuantity = row.PrintQuantity();

                string strCosts = row.PrintCosts();

                string strGross = row.PrintGross();

                string strGainLoss = row.PrintGainLoss();

                string strHoldings = row.PrintHoldings();

                string strS104 = row.PrintSection104();

                displayRows.Add(new DisplayRow { Function = strFunction, Date = strDate, Securities = strSecurity, Quantity = strQuantity, Price = strPrice, Costs = strCosts, Gross = strGross, GainLoss = strGainLoss, Holdings = strHoldings, S104 = strS104 });
            }
            DashboardReportView.ItemsSource = displayRows;
        }

        private void cbFilterType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int children = FilterInputContainer.Children.Count;
            if (children > 0)
            {
                var selected = cbFilterType.SelectedIndex;

                //Date
                if (selected == 0)
                {
                    filterControl = new FilterByDate();
                    FilterInputContainer.Children.Clear();
                    FilterInputContainer.Children.Add(filterControl);
                }
                //Security
                else if (selected == 1)
                {
                    filterControl = new FilterBySecurity(this.viewModel.GetSecuritiesExisting());
                    FilterInputContainer.Children.Clear();
                    FilterInputContainer.Children.Add(filterControl);
                }
                //Function
                else if (selected == 2)
                {
                    filterControl = new FilterByFunction(this.viewModel.GetFunctionsOnReport());
                    FilterInputContainer.Children.Clear();
                    FilterInputContainer.Children.Add(filterControl);
                }
            }
        }
        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            var selected = cbFilterType.SelectedIndex;

            //Date
            if (selected == 0)
            {
                FilterByDate filter = this.filterControl as FilterByDate;

                DateOnly? dateFrom = filter.GetDateFrom();
                DateOnly? dateTo = filter.GetDateTo();

                if (dateFrom != null && dateTo != null)
                {
                    display(viewModel.FilterByDate((DateOnly)dateFrom, (DateOnly)dateTo));
                }

            }
            //Security
            else if (selected == 1)
            {
                FilterBySecurity filter = this.filterControl as FilterBySecurity;

                Security? selected = filter.GetSecurity();

                if (selected != null)
                {
                    display(viewModel.FilterBySecuirty(selected));
                }
            }
            //Function
            else if (selected == 2)
            {
                FilterByFunction filter = this.filterControl as FilterByFunction;

                String? selected = filter.GetFunction();

                if (selected != null)
                {
                    display(viewModel.FilterByFunction(selected));
                }
            }
        }

        private class DisplayRow
        {
            public string Function { get; set; }
            public string Date { get; set; }
            public string Securities { get; set; }
            public string Quantity { get; set; }
            public string Price { get; set; }
            public string Costs { get; set; }
            public string Gross { get; set; }
            public string GainLoss { get; set; }
            public string Holdings { get; set; }
            public string S104 { get; set; }
        }

        
    }
}