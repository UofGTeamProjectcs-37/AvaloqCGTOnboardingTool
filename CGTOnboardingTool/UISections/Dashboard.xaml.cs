﻿using CGTOnboardingTool.Securities;
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
            
            Security security = new Security("GlaxoSmithKlien","GSK");
            int qty = 1000;
            decimal price = (decimal)16.35;
            decimal cost = (decimal)23.80;
            DateOnly date = new DateOnly(2021, 11, 30);

            Tools.Build b = new Tools.Build(security,qty,price,cost,0,date);
            b.perform(ref report);

           
            int qty2 = 500;
            decimal price2 = (decimal)1.85;
            decimal cost2 = (decimal)5;

            Tools.Build b2 = new Tools.Build(security, qty2,price2,cost2,0,date);
            b2.perform(ref report);

            this.DashboardReportView.ItemsSource = Report.Rows();
           

        }

        
    }
}
