using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CGTOnboardingTool
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            mainFrame.Navigate(typeof(UISections.Dashboard));
        }

        private void button_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Navigate(typeof(UISections.Dashboard));
        }

        private void button_Build_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(UISections.Build));
        }

        private void button_Reduce_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(UISections.Reduce));
        }

        private void button_Rebuild_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(UISections.Rebuild));
        }
    }
}
