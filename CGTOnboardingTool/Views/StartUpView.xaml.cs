using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views
{
    /// <summary>
    /// Interaction logic for StartUpView.xaml
    /// </summary>
    public partial class StartUpView : Page
    {
        public MetroWindow StartUpWindow;
        public Frame StartUpFrame;
        
        public StartUpView(MetroWindow startUpWindow, Frame startUpFrame)
        {
            InitializeComponent();
            this.StartUpWindow = startUpWindow;
            this.StartUpFrame = startUpFrame;
        }

        private void StartUpNew_Click(object sender, RoutedEventArgs e)
        {
            StartUpFrame.Navigate(new ConstructReportView(StartUpWindow));
        }
    }
}
