using CGTOnboardingTool.ViewModels;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.Windows.Media;

namespace CGTOnboardingTool.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddSecurityWindow.xaml
    /// </summary>
    /// Author: Andrew Bell
    public partial class AddSecurityWindow
    {
        private AddSecurityViewModel viewModel;
        public AddSecurityWindow(AddSecurityViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Logic to add a new security
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShortName = TxtAddNewShort.Text.ToString();
            viewModel.Name = TxtAddNewName.Text.ToString();

            var added = viewModel.AddSecurity();
            if (!added)
            {
                //Error message here
                this.ShowMessageAsync("Unsuccessful", "Could not add security. Check input");
            }
            else
            {
                //Success message here
                this.ShowMessageAsync("Add Security Success", "Successfully added security");
            }
        }
    }
}
