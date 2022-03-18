using System.Windows;
using System.Windows.Media;
using CGTOnboardingTool.ViewModels;
using MahApps.Metro.Controls.Dialogs;

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

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShortName = TxtAddNewShort.Text.ToString();
            viewModel.Name = TxtAddNewName.Text.ToString();

            var added = viewModel.AddSecurity();
            if (!added)
            {
                //Error message here
                this.ShowMessageAsync("Unsuccessful", "Could not add security. Check input");
                TxtBlCompletionMessage.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                //Success message here
                this.ShowMessageAsync("Add Security Success", "Successfully added security");
                TxtAddNewName.Text = "";
                TxtAddNewShort.Text = "";
                TxtBlCompletionMessage.Foreground = new SolidColorBrush(Colors.Green);
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
