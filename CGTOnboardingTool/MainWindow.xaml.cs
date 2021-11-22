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

namespace CGTOnboardingTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new UISections.Dashboard());
        }

        private void btnBuild_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new UISections.Build());
        }

        private void btnReduce_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new UISections.Reduce());

        }

        private void btnRebuild_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new UISections.Rebuild());

        }
    }
}
