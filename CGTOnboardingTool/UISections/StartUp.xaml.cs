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
    /// Interaction logic for StartUp.xaml
    /// </summary>
    public partial class StartUp : Page
    {
        public Report report;
        public Grid grid;
        public StartUp(ref Grid MainWindowGrid)
        {
            
        InitializeComponent();

        grid = MainWindowGrid;

    }

        private void btnStartUpNew_Click(object sender, RoutedEventArgs e)
        {
            report = new Report();
            grid.Visibility = Visibility.Visible;
            this.NavigationService.Navigate(new Dashboard(ref report));
        }
    }

   
}
