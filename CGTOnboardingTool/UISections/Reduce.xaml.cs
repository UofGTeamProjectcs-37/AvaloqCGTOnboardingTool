﻿using System;
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
    /// Interaction logic for Reduce.xaml
    /// </summary>
    public partial class Reduce : Page
    {
        public Report report;

        public Reduce(ref Report report)
        {
            InitializeComponent();
            this.report = report;

            DropReduceSecurities.ItemsSource = this.report.GetSecurities();
        }

        private void BtnReduceOk_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Dashboard(ref report));
        }

        private void BtnReduceCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Dashboard(ref report));
        }
    }
}
