using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using CGTOnboardingTool.Views.Windows;


namespace CGTOnboardingTool.Views
{
    /// <summary>
    /// Interaction logic for ConstructReportView.xaml
    /// </summary>
    public partial class ConstructReportView : Page
    {
        public MetroWindow StartUpWindow;
        public String ClientName = "";
        public String ClientManager = "";
        public ConstructReportView(MetroWindow StartUpWindow)
        {
            InitializeComponent();
            this.StartUpWindow = StartUpWindow; 
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            ClientName = TxtConstructClient.Text.ToString();
            ClientManager = TxtConstructClientManager.Text.ToString();

            DashboardWindow dashboardWindow = new DashboardWindow();
            dashboardWindow.Show();
            
            //Get Meta Data, Close Window and Open Dashboard
            StartUpWindow.Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
