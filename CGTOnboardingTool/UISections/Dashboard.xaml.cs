using CGTOnboardingTool.Securities;
using CGTOnboardingTool.ReportTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace CGTOnboardingTool.UISections
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {



        public Report report;

        public Dashboard(ref Report report)
        {
            InitializeComponent();
            this.report = report;

            var rows = report.Rows();

            /*   if (rows.Count() > 0)
               {
                   var row = rows[0];


                   MessageBox.Show(row.Security.ToString());
               }*/

            List<Entry> list_row = new List<Entry>();

            foreach(var row in rows)
            {
                string Function = row.Function.GetType().Name;
                string Date = row.Date.ToString();

                string SecurityCol = "";
                foreach (var sec in row.Security)
                {
                    SecurityCol += sec.ShortName;
                }

                string Quantity = "Null";
                if (row.Quantity != null)
                {
                    foreach (KeyValuePair<Security, decimal> pair in row.Quantity)
                    {
                        Quantity = pair.Value.ToString();
                    }
                }

                string Price = "Null";
                if (row.Price != null)
                { 
                    foreach(KeyValuePair<Security, decimal> pair in row.Price)
                    {
                        Price = "£" + pair.Value;
                    }
                }

                string Cost = "Null";
                if (row.AssociatedCosts != null)
                {
                    Cost = "£" + row.AssociatedCosts[0].ToString();
                }

                string Gross;
                if (row.Gross is null)
                {
                    Gross = "Null";
                } else
                {
                    Gross = row.Gross.GetType().Name;
                }

                string Gain_loss = "Null";
                if (row.GainLoss != null)
                {
                    foreach (KeyValuePair<Security, decimal> pair in row.GainLoss)
                    {
                        Gain_loss = pair.Value.ToString();
                    }
                }

                string Holdings = "Null";
                if (row.Holdings != null)
                {
                    foreach (KeyValuePair<Security, decimal> pair in row.Holdings)
                    {
                        Holdings = pair.Value.ToString();
                    }
                }

                string S104 = "Null";
                if (row.Section104 != null)
                {
                    foreach (KeyValuePair<Security, decimal> pair in row.Section104)
                    {
                        S104 = pair.Value.ToString();
                    }
                }

                list_row.Add(new Entry { Function = Function, Date = Date, Securities = SecurityCol, Quantity = Quantity, Price = Price, Cost = Cost, Gross = Gross, Gain_loss = Gain_loss, Holdings = Holdings, S104 = S104 });
               
            }
            DashboardReportView.ItemsSource= list_row;
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
    public class Entry
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
