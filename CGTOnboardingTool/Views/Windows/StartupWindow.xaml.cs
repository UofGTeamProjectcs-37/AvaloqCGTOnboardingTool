namespace CGTOnboardingTool.Views.Windows
{
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
