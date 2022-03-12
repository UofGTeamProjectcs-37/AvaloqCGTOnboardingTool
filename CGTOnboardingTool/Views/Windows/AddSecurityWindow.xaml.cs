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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CGTOnboardingTool.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddSecurityWindow.xaml
    /// </summary>
    public partial class AddSecurityWindow
    {
        public AddSecurityWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
