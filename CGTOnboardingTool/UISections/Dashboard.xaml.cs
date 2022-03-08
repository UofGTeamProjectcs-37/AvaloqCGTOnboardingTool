using CGTOnboardingTool.Securities;
using CGTOnboardingTool.ReportTools;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CGTOnboardingTool.Tools;

namespace CGTOnboardingTool.UISections
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        DateOnly? filterDateFrom;
        DateOnly? filterDateTo;

        public Report report;

        public Dashboard(ref Report report)
        {
            InitializeComponent();
            this.report = report;
            display(report.Rows());
        }




            //var rows = report.Rows();

            ///*   if (rows.Count() > 0)
            //   {
            //       var row = rows[0];


            //       MessageBox.Show(row.Security.ToString());
            //   }*/

            List<Entry> list_row = initReport();

            DashboardReportView.ItemsSource= list_row;
        }

        // Display report on dashboard 
        public Dashboard(ref Report report, ref String client, ref String tax)
        {
            InitializeComponent();

            LblClientName.Content = client; 
            LblTaxYear.Content = tax;

            this.report = report;

            List<Entry> list_row = initReport();

            DashboardReportView.ItemsSource = list_row;
        }

        // Filter rows drop-down menu
        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hideComboBoxes();
            // Filter by function
            if (cbFilter.SelectedIndex.ToString() == "2")
            {
                cbFilterFunction.Visibility = Visibility.Visible;
            }
            //Filter by Security
            else if (cbFilter.SelectedIndex.ToString() == "1")
            {
                cbFilterSecurity.Visibility = Visibility.Visible;
            }
            // Filter by date
            else if (cbFilter.SelectedIndex.ToString() == "0")
            {
                cbFilterDateFrom.Visibility = Visibility.Visible;
                cbFilterDateTo.Visibility = Visibility.Visible;
                LblReportFilterDateFrom.Visibility = Visibility.Visible;
                LblReportFilterDateTo.Visibility = Visibility.Visible;
            }
        }

        private void hideComboBoxes()
        {
            cbFilterFunction.Visibility = Visibility.Hidden;
            cbFilterSecurity.Visibility = Visibility.Hidden;
            cbFilterDateFrom.Visibility = Visibility.Hidden;
            cbFilterDateTo.Visibility = Visibility.Hidden;
            LblReportFilterDateFrom.Visibility = Visibility.Hidden;
            LblReportFilterDateTo.Visibility = Visibility.Hidden;
        }

        // Save button functionality
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ReportExporter exporter = new ReportExporter(ref report);
            exporter.ExportToText();
        }


        // Initialise report
        private List<Entry> initReport()
        {
            var rows = report.Rows();

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

            //    list_row.Add(new Entry { Function = Function, Date = Date, Securities = SecurityCol, Quantity = Quantity, Price = Price, Cost = Cost, Gross = Gross, Gain_loss = Gain_loss, Holdings = Holdings, S104 = S104 });

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

                displayRows.Add(new DisplayRow { Function = strFunction, Date = strDate, Securities = strSecurity, Quantity = strQuantity, Price = strPrice, Cost = strCosts, Gross = strGross, GainLoss = strGainLoss, Holdings = strHoldings, S104 = strS104 });
            }
            DashboardReportView.ItemsSource = displayRows;
        }
    

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hideComboBoxes();
            //Function
            if (cbFilter.SelectedIndex.ToString() == "2")
            {
                CGTFunction[] funcitonsUsed = report.GetFunctionsUsed();
                List<DropdownItem> selections = new List<DropdownItem>();
                foreach (CGTFunction funciton in funcitonsUsed)
                {
                    selections.Add(new DropdownItem { Text = funciton.GetType().Name, Value = funciton.GetType() });
                }
                cbFilterFunction.ItemsSource = selections;
                cbFilterFunction.Visibility = Visibility.Visible;
            }
            //Security
            else if (cbFilter.SelectedIndex.ToString() == "1")
            {
                Security[] securities = report.GetSecurities();
                List<DropdownItem> selections = new List<DropdownItem>();
                foreach (Security security in securities)
                {
                    selections.Add(new DropdownItem { Text = security.ShortName, Value = security });
                }
                cbFilterSecurity.ItemsSource = selections;
                cbFilterSecurity.Visibility = Visibility.Visible;
            }
            //Date
            else if (cbFilter.SelectedIndex.ToString() == "0")
            {
                cbFilterDateFrom.Visibility = Visibility.Visible;
                cbFilterDateTo.Visibility = Visibility.Visible;
                LblReportFilterDateFrom.Visibility = Visibility.Visible;
                LblReportFilterDateTo.Visibility = Visibility.Visible;
            }
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

        private void hideComboBoxes()
        {
            cbFilterFunction.Visibility = Visibility.Hidden;
            cbFilterSecurity.Visibility = Visibility.Hidden;
            cbFilterDateFrom.Visibility = Visibility.Hidden;
            cbFilterDateTo.Visibility = Visibility.Hidden;
            LblReportFilterDateFrom.Visibility = Visibility.Hidden;
            LblReportFilterDateTo.Visibility = Visibility.Hidden;
        }

            return list_row;
        }

        private class DisplayRow
        {
            public string Function { get; set; }
            public string Date { get; set; }
            public string Securities { get; set; }
            public string Quantity { get; set; }
            public string Price { get; set; }
            public string Cost { get; set; }
            public string Gross { get; set; }
            public string GainLoss { get; set; }
            public string Holdings { get; set; }
            public string S104 { get; set; }
        }

        public class DropdownItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void cbFilterSecurity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DropdownItem selected = (DropdownItem) cbFilterSecurity.SelectedItem;
            display(report.FilterBySecurity((Security) selected.Value));
        }

        private void cbFilterDateTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterDateTo = (DateOnly)cbFilterDateTo.SelectedItem;

            if (filterDateFrom != null && filterDateTo != null)
            {
                display(report.FilterByDate((DateOnly)filterDateFrom, (DateOnly)filterDateTo));
            }
        }

        private void cbFilterDateFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterDateFrom = (DateOnly)cbFilterDateFrom.SelectedItem;

            if (filterDateFrom != null && filterDateTo != null)
            {
                display(report.FilterByDate((DateOnly)filterDateFrom, (DateOnly)filterDateTo));
            }
        }

        private void cbFilterFunction_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DropdownItem selected = (DropdownItem)cbFilterFunction.SelectedItem;
            display(report.FilterByFunction((CGTFunction)selected.Value));
        }
    }

    

    
    

}
