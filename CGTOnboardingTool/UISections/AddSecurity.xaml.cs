using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddSecurity.xaml
    /// </summary>
    public partial class AddSecurity : Page
    {
        public Report report;
        public AddSecurity(ref Report report)
        {
            InitializeComponent();
            this.report = report;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Dashboard(ref report));
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            bool incorrect = Validate();

            if (!incorrect)
            {
                //Add security into list



                //Resets Text Boxes to allow for securities to inuted sequentially 
                TxtAddNewName.Text = "Enter Name of the Security Here";
                TxtAddNewShort.Text = "Enter the ShortHand for the Security Here";
            }
        }

        private bool Validate()
        {
            LblAddNewNameIncorrect.Visibility = Visibility.Hidden;
            TxtAddNewName.BorderThickness = new Thickness(0);
            LblAddNewShortIncorrect.Visibility = Visibility.Hidden;
            TxtAddNewShort.BorderThickness = new Thickness(0);

            foreach (char c in TxtAddNewName.Text)
            {
                //Regex checks that all characters are either a letter or an integer
                if (!Regex.IsMatch(c.ToString(), @"^[A-Za-z0-9\s@]*$"))
                {
                    LblAddNewNameIncorrect.Visibility = Visibility.Visible;
                    TxtAddNewName.BorderThickness = new Thickness(5);

                    return true;
                }
            }
            
            foreach (char c in TxtAddNewShort.Text)
            {
                //Regex checks that all characters are letters
                if (!Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$"))
                {
                    LblAddNewShortIncorrect.Visibility = Visibility.Visible;
                    TxtAddNewShort.BorderThickness = new Thickness(5);

                    return true;
                }
            }

            return false;
        }
    }
}
