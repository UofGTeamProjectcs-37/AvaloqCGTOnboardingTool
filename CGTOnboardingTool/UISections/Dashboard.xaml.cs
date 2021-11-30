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
        public Report Report;

        public Dashboard(ref Report report)
        {
            InitializeComponent();
            this.Report = report;
            
            var security = new Security[] { new Security("GlaxoSmithKlien","GSK") };
            var prices = new Dictionary<Security,decimal>();
            prices.Add(security[0], (decimal)16.36);
            var qty = new Dictionary<Security, int>();
            qty.Add(security[0], 1000);
            var costs = new decimal[] { (decimal)23.27 };
            var section104 = (qty[security[0]] * prices[security[0]]) + costs[0];
            var section104After = new Dictionary<Security,decimal>();
            section104After.Add(security[0], section104);
            var date = new DateOnly(2021, 11, 30);

            Tools.Build b = new Tools.Build(security[0], qty[security[0]], prices[security[0]], costs[0], 0, date);

            this.Report.Add(b, security, prices, qty, costs, section104After, date);

            var security2 = new Security[] { new Security("Tesla", "TSLA") };
            var prices2 = new Dictionary<Security, decimal>();
            prices2.Add(security2[0], (decimal)20.15);
            var qty2 = new Dictionary<Security, int>();
            qty2.Add(security2[0], 100);
            var costs2 = new decimal[] { (decimal)42.30 };
            var section1042 = (qty2[security2[0]] * prices2[security2[0]]) + costs2[0];
            var section104After2 = new Dictionary<Security, decimal>();
            section104After2.Add(security2[0], section1042);
            var date2 = new DateOnly(2021, 12, 1);

            Tools.Build b2 = new Tools.Build(security2[0], qty2[security2[0]], prices2[security2[0]], costs2[0], 0, date2);
            this.Report.Add(b2, security2, prices2, qty2, costs2, section104After2, date2);


            this.DashboardReportView.ItemsSource = Report.Rows();
           

        }

        
    }
}
