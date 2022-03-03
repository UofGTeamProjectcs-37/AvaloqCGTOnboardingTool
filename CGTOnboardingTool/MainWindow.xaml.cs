using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using CGTOnboardingTool.ReportTools;
using System.Windows;

namespace CGTOnboardingTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Report report;

        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            report = new Report();
            mainFrame.Navigate(new UISections.StartUp(ref report, ref MainWindowGrid));
            //mainFrame.Navigate(new UISections.Dashboard(ref report));
        }

        private void btnBuild_Click(object sender, RoutedEventArgs e)
        { 
            mainFrame.Navigate(new UISections.Build(ref report));
        }

        private void btnReduce_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new UISections.Reduce(ref report));

        }

        private void btnRebuild_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new UISections.Rebuild(ref report));

        }
    }
}
