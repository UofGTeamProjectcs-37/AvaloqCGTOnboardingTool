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

            //List<Entry> list_row = new List<Entry>();

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
            DashboardReportView.Items.Clear();

            List<DisplayEntry> displayRows = new List<DisplayEntry>();

            foreach (ReportEntry row in rows)
            {
                string strFunction = row.Function.GetType().Name;
                string strDate = row.Date.ToString();

                string strSecurity = "";
                if (row.Security.Length > 1)
                {
                    strSecurity = "[";
                    foreach (Security security in row.Security)
                    {
                        strSecurity += string.Join(",", security.ShortName);
                    }
                    strSecurity += "]";
                }
                else
                {
                    strSecurity = row.Security[0].ShortName;
                }

                string strPrice = "";
                if (row.Price.Count > 1)
                {
                    strPrice = "[";
                    foreach (var k in row.Price.Keys)
                    {
                        strPrice += string.Join(",", k.ShortName + " : £" + row.Price[k].ToString());
                    }
                    strPrice += "]";
                }
                else if (row.Price.Count == 1)
                {
                    foreach (var k in row.Price.Keys)
                    {
                        strPrice = "£" + row.Price[k].ToString();
                    }
                }

                string strQuantity = "";
                if (row.Quantity.Count > 1)
                {
                    strQuantity = "[";
                    foreach (var k in row.Price.Keys)
                    {
                        strQuantity += string.Join(",", k.ShortName + " : £" + row.Quantity[k].ToString());
                    }
                    strQuantity += "]";
                }
                else if (row.Quantity.Count == 1)
                {
                    foreach (var k in row.Price.Keys)
                    {
                        strQuantity = row.Quantity[k].ToString();
                    }
                }

                string strCosts = "";
                if (row.AssociatedCosts.Length > 1)
                {
                    strCosts = "[";
                    foreach (var cost in row.AssociatedCosts)
                    {
                        strCosts += string.Join(",", "£" + cost.ToString());
                    }
                    strCosts += "]";
                }
                else if (row.AssociatedCosts.Length == 1)
                {
                    strCosts = "£" + row.AssociatedCosts[0].ToString();
                }


                string strGross = "";
                if (row.Gross != null)
                {
                    strGross = "£" + row.Gross.ToString();
                }

                string strGainLoss = "";
                if (row.GainLoss.Count > 1)
                {
                    strGainLoss = "[";
                    foreach (var k in row.GainLoss.Keys)
                    {
                        strGainLoss += string.Join(",", k.ShortName + " : " + row.GainLoss[k].ToString());
                    }
                    strGainLoss += "]";
                }
                else
                {
                    foreach (var k in row.GainLoss.Keys)
                    {
                        strGainLoss = row.GainLoss[k].ToString();
                    }
                }

                string strHoldings = "";
                if (row.Holdings.Count > 1)
                {
                    strHoldings = "[";
                    foreach (var k in row.Holdings.Keys)
                    {
                        strHoldings += string.Join(",", k.ShortName + " : " + row.Holdings[k].ToString());
                    }
                    strHoldings += "]";
                }
                else
                {
                    foreach (var k in row.Holdings.Keys)
                    {
                        strHoldings = row.Holdings[k].ToString();
                    }
                }

                string strS104 = "";
                if (row.Section104.Count > 1)
                {
                    strS104 = "[";
                    foreach (var k in row.Section104.Keys)
                    {
                        strS104 += string.Join(",", k.ShortName + " : " + row.Section104[k].ToString());
                    }
                    strS104 += "]";
                }
                else
                {
                    foreach (var k in row.Section104.Keys)
                    {
                        strS104 = row.Section104[k].ToString();
                    }
                }

                displayRows.Add(new DisplayEntry { Function = strFunction, Date = strDate, Securities = strSecurity, Quantity = strQuantity, Price = strPrice, Cost = strCosts, Gross = strGross, Gain_loss = strGainLoss, Holdings = strHoldings, S104 = strS104 });

            }

            DashboardReportView.ItemsSource = displayRows;
        }
    

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hideComboBoxes();
            //Function
            if (cbFilter.SelectedIndex.ToString() == "2")
            {
                cbFilterFunction.Visibility = Visibility.Visible;
            }
            //Security
            else if (cbFilter.SelectedIndex.ToString() == "1")
            {
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

        private void cbFilterFunction_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            CGTFunction selected = (CGTFunction)cbFilterFunction.SelectedItem;
            display(report.FilterByFunction(selected));

        }

        private void cbFilterSecurity_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Security selected = (Security)cbFilterSecurity.SelectedItem;
            display(report.FilterBySecurity(selected));
        }

        private void cbFilterDateFrom_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            filterDateFrom = (DateOnly)cbFilterDateFrom.SelectedItem;

            if (filterDateFrom != null && filterDateTo != null)
            {
                display(report.FilterByDate((DateOnly)filterDateFrom, (DateOnly)filterDateTo));
            }
        }

        private void cbFilterDateTo_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            filterDateTo = (DateOnly)cbFilterDateTo.SelectedItem;

            if (filterDateFrom != null && filterDateTo != null)
            {
                display(report.FilterByDate((DateOnly)filterDateFrom, (DateOnly)filterDateTo));
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ReportExporter exporter = new ReportExporter(ref report);
            exporter.ExportToText();
        }

      
    }

    public class DisplayEntry
    {
        public string Function { get; set; }
        public string Date { get; set; }
        public string Securities { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Cost { get; set; }
        public string Gross { get; set; }
        public string Gain_loss { get; set; }
        public string Holdings { get; set; }
        public string S104 { get; set; }
    }

}
