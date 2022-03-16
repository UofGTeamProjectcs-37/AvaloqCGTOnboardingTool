using CGTOnboardingTool.Models.AccessModels;
using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.Models.OutputModels;
using CGTOnboardingTool.ViewModels;
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
        DateOnly? filterDateFrom;
        DateOnly? filterDateTo;

        public MetroWindow window;
        public DashboardViewModel viewModel;

        public DashboardView(MetroWindow window, DashboardViewModel viewModel)
        {
            InitializeComponent();
            LblClientName.Content = viewModel.GetClientName();
            LblTaxYear.Content = viewModel.GetYearStart() + " - " + viewModel.GetYearEnd();
            display(viewModel.Rows());
        }

        //// Filter rows drop-down menu
        //private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    hideComboBoxes();
        //    // Filter by function
        //    if (cbFilter.SelectedIndex.ToString() == "2")
        //    {
        //        cbFilterFunction.Visibility = Visibility.Visible;
        //    }
        //    //Filter by Security
        //    else if (cbFilter.SelectedIndex.ToString() == "1")
        //    {
        //        cbFilterSecurity.Visibility = Visibility.Visible;
        //    }
        //    // Filter by date
        //    else if (cbFilter.SelectedIndex.ToString() == "0")
        //    {
        //        cbFilterDateFrom.Visibility = Visibility.Visible;
        //        cbFilterDateTo.Visibility = Visibility.Visible;
        //        LblReportFilterDateFrom.Visibility = Visibility.Visible;
        //        LblReportFilterDateTo.Visibility = Visibility.Visible;
        //    }
        //}

        private void hideComboBoxes()
        {
            //cbFilterFunction.Visibility = Visibility.Hidden;
            //cbFilterSecurity.Visibility = Visibility.Hidden;
            //cbFilterDateFrom.Visibility = Visibility.Hidden;
            //cbFilterDateTo.Visibility = Visibility.Hidden;
            //LblReportFilterDateFrom.Visibility = Visibility.Hidden;
            //LblReportFilterDateTo.Visibility = Visibility.Hidden;
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


        // Initialise report
        //private List<Entry> initReport()
        //{
        //    var rows = report.Rows();

        //foreach(var row in rows)
        //{
        //    string Function = row.Function.GetType().Name;
        //    string Date = row.Date.ToString();

        //    string SecurityCol = "";
        //    foreach (var sec in row.Security)
        //    {
        //        SecurityCol += sec.ShortName;
        //    }

        //    string Quantity = "Null";
        //    if (row.Quantity != null)
        //    {
        //        foreach (KeyValuePair<Security, decimal> pair in row.Quantity)
        //        {
        //            Quantity = pair.Value.ToString();
        //        }
        //    }

        //    string Price = "Null";
        //    if (row.Price != null)
        //    { 
        //        foreach(KeyValuePair<Security, decimal> pair in row.Price)
        //        {
        //            Price = "£" + pair.Value;
        //        }
        //    }

        //    string Cost = "Null";
        //    if (row.AssociatedCosts != null)
        //    {
        //        Cost = "£" + row.AssociatedCosts[0].ToString();
        //    }

        //    string Gross;
        //    if (row.Gross is null)
        //    {
        //        Gross = "Null";
        //    } else
        //    {
        //        Gross = row.Gross.GetType().Name;
        //    }

        //    string Gain_loss = "Null";
        //    if (row.GainLoss != null)
        //    {
        //        foreach (KeyValuePair<Security, decimal> pair in row.GainLoss)
        //        {
        //            Gain_loss = pair.Value.ToString();
        //        }
        //    }

        //    string Holdings = "Null";
        //    if (row.Holdings != null)
        //    {
        //        foreach (KeyValuePair<Security, decimal> pair in row.Holdings)
        //        {
        //            Holdings = pair.Value.ToString();
        //        }
        //    }

        //    string S104 = "Null";
        //    if (row.Section104 != null)
        //    {
        //        foreach (KeyValuePair<Security, decimal> pair in row.Section104)
        //        {
        //            S104 = pair.Value.ToString();
        //        }
        //    }

        //    list_row.Add(new Entry { Function = Function, Date = Date, Models = SecurityCol, Quantity = Quantity, Price = Price, Cost = Cost, Gross = Gross, Gain_loss = Gain_loss, Holdings = Holdings, S104 = S104 });

        //}
        //DashboardReportView.ItemsSource= list_row;
        //}

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


        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hideComboBoxes();
            //Function
            //if (cbFilterType.SelectedIndex.ToString() == "2")
            //{
            //    CGTFunctionBaseViewModel[] funcitonsUsed = report.GetFunctionsUsed();
            //    List<DropdownItem> selections = new List<DropdownItem>();
            //    foreach (CGTFunctionBaseViewModel funciton in funcitonsUsed)
            //    {
            //        selections.Add(new DropdownItem { Text = funciton.GetType().Name, Value = funciton.GetType() });
            //    }
            //    cbFilterFunction.ItemsSource = selections;
            //    cbFilterFunction.Visibility = Visibility.Visible;
            //}
            ////Security
            //else if (cbFilterType.SelectedIndex.ToString() == "1")
            //{
            //    Security[] securities = report.GetSecurities();
            //    List<DropdownItem> selections = new List<DropdownItem>();
            //    foreach (Security security in securities)
            //    {
            //        selections.Add(new DropdownItem { Text = security.ShortName, Value = security });
            //    }
            //    cbFilterSecurity.ItemsSource = selections;
            //    cbFilterSecurity.Visibility = Visibility.Visible;
            //}
            ////Date
            //else if (cbFilterType.SelectedIndex.ToString() == "0")
            //{
            //    cbFilterDateFrom.Visibility = Visibility.Visible;
            //    cbFilterDateTo.Visibility = Visibility.Visible;
            //    LblReportFilterDateFrom.Visibility = Visibility.Visible;
            //    LblReportFilterDateTo.Visibility = Visibility.Visible;
            //}
        }


        //private void cbFilterSecurity_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        //{
        //    Security selected = (Security)cbFilterSecurity.SelectedItem;
        //    display(report.FilterBySecurity(selected));
        //}

        //private void cbFilterDateFrom_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        //{
        //    filterDateFrom = (DateOnly)cbFilterDateFrom.SelectedItem;

        //    if (filterDateFrom != null && filterDateTo != null)
        //    {
        //        display(report.FilterByDate((DateOnly)filterDateFrom, (DateOnly)filterDateTo));
        //    }
        //}

        //private void cbFilterDateTo_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        //{
        //    filterDateTo = (DateOnly)cbFilterDateTo.SelectedItem;

        //    if (filterDateFrom != null && filterDateTo != null)
        //    {
        //        display(report.FilterByDate((DateOnly)filterDateFrom, (DateOnly)filterDateTo));
        //    }

        //}

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

        private void cbFilterSecurity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DropdownItem selected = (DropdownItem) cbFilterSecurity.SelectedItem;
            //display(report.FilterBySecurity((Security) selected.Value));
        }

        private void cbFilterDateTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //filterDateTo = (DateOnly)cbFilterDateTo.SelectedItem;

            //if (filterDateFrom != null && filterDateTo != null)
            //{
            //    display(report.FilterByDate((DateOnly)filterDateFrom, (DateOnly)filterDateTo));
            //}
        }

        private void cbFilterDateFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //filterDateFrom = (DateOnly)cbFilterDateFrom.SelectedItem;

            //if (filterDateFrom != null && filterDateTo != null)
            //{
            //    display(report.FilterByDate((DateOnly)filterDateFrom, (DateOnly)filterDateTo));
            //}
        }

        private void cbFilterFunction_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //    DropdownItem selected = (DropdownItem)cbFilterFunction.SelectedItem;
            //    display(report.FilterByFunction((CGTFunctionBaseViewModel)selected.Value));
        }

        private void cbFilterType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}



