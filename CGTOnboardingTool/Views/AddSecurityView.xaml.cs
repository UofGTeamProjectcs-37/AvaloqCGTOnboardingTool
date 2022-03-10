using CGTOnboardingTool.Models.DataModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views
{
    /// <summary>
    /// Interaction logic for AddSecurityView.xaml
    /// </summary>
    public partial class AddSecurityView : Page
    {
        public Report report;
        public AddSecurityView(ref Report report)
        {
            InitializeComponent();
            this.report = report;
        }

        // Cancel button
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new OldDashboardView(ref report));
        }

        // Add button functionality
        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {   
            // Returns true if input is not in the correct format
            bool incorrect = Validate();

            if (!incorrect)
            {
                // Add security into list
                // Resets Text Boxes to allow for securities to inuted sequentially 
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
