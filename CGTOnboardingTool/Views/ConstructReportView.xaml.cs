using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using CGTOnboardingTool.Views.Windows;
using CGTOnboardingTool.ViewModels;
using CGTOnboardingTool.Models.DataModels;

namespace CGTOnboardingTool.Views
{
    /// <summary>
    /// Interaction logic for ConstructReportView.xaml
    /// </summary>
    public partial class ConstructReportView : Page
    {
        private MetroWindow startUpWindow;
        private ConstructReportViewModel viewModel;

        public ConstructReportView(MetroWindow StartUpWindow, ConstructReportViewModel constructReportViewModel)
        {
            InitializeComponent();
            this.startUpWindow = StartUpWindow;
            this.viewModel = constructReportViewModel;
            initYearSelectors();
        }

        private void initYearSelectors()
        {
            var currentYear = DateTime.Now.Year;
            var sevenYearPrior = currentYear - 7;

            List<DropDownItem> yearStartSelections = new List<DropDownItem>();
            List<DropDownItem> yearEndSelections = new List<DropDownItem>();

            for (int i = currentYear; i >= sevenYearPrior; i--)
            {
                DropDownItem dropDownItem = new DropDownItem();
                dropDownItem.Text = i.ToString();
                dropDownItem.Value = i;
                if (i == currentYear)
                {
                    dropDownItem.Text += " (current)";
                }
                yearStartSelections.Add(dropDownItem);
                yearEndSelections.Add(dropDownItem);
            }

            dropdownYearStart.ItemsSource = yearStartSelections;
            dropdownYearEnd.ItemsSource = yearEndSelections;

            dropdownYearStart.SelectedIndex = 1;
            dropdownYearEnd.SelectedIndex = 0;
        }


        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SetClientName(txtClientName.Text);

            bool invalidDate = false;
            var selectedYearStart = dropdownYearStart.SelectedItem as DropDownItem;
            if (!viewModel.SetYearStart(selectedYearStart.Value.ToString()))
            {
                invalidDate = true;
            };

            var selectedYearEnd = dropdownYearEnd.SelectedItem as DropDownItem;
            if (!viewModel.SetYearEnd(selectedYearEnd.Value.ToString()))
            {
                invalidDate = true;
            };

            var reportConstucted = viewModel.GenerateReport();
            if (reportConstucted == ConstructReportViewModel.CONSTRUCTREPORT_ERROR.CONSTRUCTREPORT_SUCCESS)
            {
                var report = viewModel.GetReport();
                DashboardWindow dashboardWindow = new DashboardWindow(report);
                dashboardWindow.Show();
                startUpWindow.Close();
            }
            else if (reportConstucted == ConstructReportViewModel.CONSTRUCTREPORT_ERROR.CONSTRUCTREPORT_INVALID_CLIENT_NAME)
            {
                startUpWindow.ShowMessageAsync("Error: INVALID_CLIENT_NAME", "Cannot create report due to \"Invalid Client Name\". Re-enter client name and try again.");
            }
            else if (reportConstucted == ConstructReportViewModel.CONSTRUCTREPORT_ERROR.CONSTRUCTREPORT_INVALID_DATE || invalidDate)
            {
                startUpWindow.ShowMessageAsync("Error: INVALID_DATE", "Cannot create report due to \"Invalid Date\". Re-enter date and try again.");
            }
            else
            {
                startUpWindow.ShowMessageAsync("Error: UNKNOWN_ERROR", "Unknown error");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
