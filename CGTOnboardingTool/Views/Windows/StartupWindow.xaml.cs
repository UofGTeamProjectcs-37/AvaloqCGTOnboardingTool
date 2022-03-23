namespace CGTOnboardingTool.Views.Windows
{
    //Author: Aidan Neil

    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow
    {
        public StartupWindow()
        {
            InitializeComponent();
            StartUpFrame.Navigate(new StartUpView(this, StartUpFrame));
        }
    }
}
