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
            
            Security security = new Security("GlaxoSmithKline","GSK");
            int qty = 500;
            decimal price = (decimal)16.35;
            decimal cost = (decimal)23.80;
            DateOnly date = new DateOnly(2021, 11, 30);

            Tools.Build b = new Tools.Build(security,qty,price,cost,date);
            b.perform(ref report);

            int qty2 = 500;
            decimal price2 = (decimal)1.85;
            decimal cost2 = (decimal)5;

            Tools.Build b2 = new Tools.Build(security, qty2,price2,cost2,date);
            b2.perform(ref report);

            int qty3 = 500;
            decimal price3 = (decimal)18.24;
            decimal cost3 = (decimal)24.50;
           
            Tools.Reduce r = new Tools.Reduce(security,qty3,price3,cost3,date);
            r.perform(ref report);

            Security security2 = new Security("Tesla", "TSLA");
            int rQ = 250;
            int bQ = 50;

            Tools.Rebuild rb = new Tools.Rebuild(security,rQ,security2,bQ,date);
            rb.perform(ref report);

            this.DashboardReportView.ItemsSource = Report.Rows();
           

        }

        
    }
}
