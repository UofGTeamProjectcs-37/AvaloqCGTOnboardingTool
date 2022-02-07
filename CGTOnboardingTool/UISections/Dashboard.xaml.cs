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

            foreach(var row in rows)
            {
                string function = row.Function.ToString();
                //do for all




                string[] list_row = {function, ......};
                this.DashboardReportView.Items.Add(list_row);
            }
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

}
