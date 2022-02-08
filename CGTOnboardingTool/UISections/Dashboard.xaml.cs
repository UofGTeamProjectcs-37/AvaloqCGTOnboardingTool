using CGTOnboardingTool.Securities;
using System;
using System.Collections.Generic;
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
                string Function = row.Function.ToString();
                string Date = row.Date.ToString();

                var securities = row.Security;
                Security[] secList = new Security[securities.Count()];
                if (securities.Count() > 0)
                {
                    foreach (var sec in securities)
                    {
                        secList.Append(sec);
                    }
                }
                string SecurityCol = secList.ToString();

                string Quantity;
                if (row.Quantity is null)
                {
                    Quantity = "Null";
                } else
                {
                    Quantity = row.Quantity.ToString();
                }

                string Price;
                if (row.Price is null)
                {
                    Price = "Null";
                } else
                {
                    Price = row.Price.ToString();
                }
                
                string Cost = row.AssociatedCosts.ToString();

                string Gross;
                if (row.Gross is null)
                {
                    Gross = "Null";
                } else
                {
                    Gross = row.Gross.ToString();
                }
                
                string Gain_loss = row.GainLoss.ToString();
                string Holdings = row.Holdings.ToString();
                string S104 = row.Section104.ToString();

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
