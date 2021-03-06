using CGTOnboardingTool.Models.AccessModels;
using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views
{
    //Author: Aidan Neil

    /// <summary>
    /// Interaction logic for StartUpView.xaml
    /// </summary>
    public partial class StartUpView : Page
    {
        private MetroWindow startUpWindow;
        private Frame startUpFrame;

        public StartUpView(MetroWindow startUpWindow, Frame startUpFrame)
        {
            InitializeComponent();
            this.startUpWindow = startUpWindow;
            this.startUpFrame = startUpFrame;
        }

        /// <summary>
        /// New Report button functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartUpNew_Click(object sender, RoutedEventArgs e)
        {
            ConstructReportViewModel constructReportViewModel = new ConstructReportViewModel();
            startUpFrame.Navigate(new ConstructReportView(startUpWindow, constructReportViewModel));
        }

        /// <summary>
        /// Import Report button functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartUpImport_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report();
            ReportLoader importer = new ReportLoader(ref report);
            importer.ImportReport();
        }
    }
}
