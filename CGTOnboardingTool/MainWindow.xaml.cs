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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string path = @"c:file.csv";
            var csv = new StringBuilder();
           
            List<ReportEntry> t = Report.Rows();
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

                var new_line = string.Format("{0},{1}", t[i].EntryID, t[i].DatePerformed);
                csv.AppendLine(new_line);
            }

            File.AppendAllText(path, csv.ToString());




        }
    }
}
