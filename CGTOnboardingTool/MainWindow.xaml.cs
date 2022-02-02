﻿using System;
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
using static CGTOnboardingTool.Report;

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
            mainFrame.Navigate(new UISections.Dashboard(ref report));
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

        private static void btnSave_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("testing hello there");
            List<ReportEntry> t = Report.Rows();
            Console.WriteLine(t);

            //try
            //{
            //   string strFilePath = @"C:\.csv";
            //  StringBuilder sbOutput = new StringBuilder();
            //
            //  string seperator = ",";

                //connect report to output variable
              //  string[][] output = new string[][]
              //  {
              //      new string[]{ }
              //  };
                
                //for (int i=0; i<output.GetLength(0); i++)
               // {
               //     sbOutput.AppendLine(string.Join(seperator, output[i]));
                //}

                //File.WriteAllText(strFilePath, sbOutput.ToString());

//                File.AppendAllText(strFilePath, sbOutput.ToString());

  //          } catch (Exception ex)
    //        {
      //          MessageBox.Show(ex.ToString());
            //}
            

        }
    }
}
