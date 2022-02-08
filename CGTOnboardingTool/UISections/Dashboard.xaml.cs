using CGTOnboardingTool.Securities;
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
            if (cbFilter.SelectedIndex.ToString() == "2")
            {
                cbFilterFunction.Visibility = Visibility.Visible;
                cbFilterSecurity.Visibility = Visibility.Hidden;

                cbFilterDateFrom.Visibility = Visibility.Hidden;
                cbFilterDateTo.Visibility = Visibility.Hidden;
                LblReportFilterDateFrom.Visibility = Visibility.Hidden;
                LblReportFilterDateTo.Visibility = Visibility.Hidden;
            }
            else if (cbFilter.SelectedIndex.ToString() == "1")
            {
                cbFilterFunction.Visibility = Visibility.Hidden;
                cbFilterSecurity.Visibility = Visibility.Visible;

                cbFilterDateFrom.Visibility = Visibility.Hidden;
                cbFilterDateTo.Visibility = Visibility.Hidden;
                LblReportFilterDateFrom.Visibility = Visibility.Hidden;
                LblReportFilterDateTo.Visibility = Visibility.Hidden;
            }
            else if (cbFilter.SelectedIndex.ToString() == "0")
            {
                cbFilterFunction.Visibility = Visibility.Hidden;
                cbFilterSecurity.Visibility = Visibility.Hidden;

                cbFilterDateFrom.Visibility = Visibility.Visible;
                cbFilterDateTo.Visibility = Visibility.Visible;
                LblReportFilterDateFrom.Visibility = Visibility.Visible;
                LblReportFilterDateTo.Visibility = Visibility.Visible;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

           /* List<ReportEntry> t = Report.Rows();


             Stream myStream;
             SaveFileDialog saveFile = new SaveFileDialog();
             saveFile.Filter = "txt files (*.txt) | *.*" ;
             saveFile.FilterIndex = 2;
             saveFile.RestoreDirectory = true;
             UnicodeEncoding uniEncoding = new UnicodeEncoding();
             if (saveFile.ShowDialog() == true)
             {
                 if ((myStream = saveFile.OpenFile())!=null) {

                     for (int  i=0; i<Report.Count(); i++)
                     {
                         char[] row = { Convert.ToChar(t[i].EntryID), Convert.ToChar(t[i].FunctionPerformed) };

                         myStream.Write(uniEncoding.GetBytes(row));
                     }
                     myStream.Close();
                 }
             }



             for (int i=0; i<Report.Count();i++)
             {
                 System.Diagnostics.Debug.WriteLine(t[i].EntryID);
                 System.Diagnostics.Debug.WriteLine(t[i].DatePerformed);
                 System.Diagnostics.Debug.WriteLine(t[i].FunctionPerformed);
                 System.Diagnostics.Debug.WriteLine(t[i].SecuritiesAffected);
                 System.Diagnostics.Debug.WriteLine(t[i].PricesAffected);
                 System.Diagnostics.Debug.WriteLine(t[i].QuantitiesAffected);
                 System.Diagnostics.Debug.WriteLine(t[i].AssociatedCosts);
                 System.Diagnostics.Debug.WriteLine(t[i].Section104sAfter);
             }*/
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
