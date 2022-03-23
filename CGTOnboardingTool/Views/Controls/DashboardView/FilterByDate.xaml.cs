using System;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views.Controls.DashboardView
{
    //Author: Aidan Neil

    /// <summary>
    /// Interaction logic for FilterByDate.xaml
    /// </summary>
    public partial class FilterByDate : UserControl
    {
        public FilterByDate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Tries to get start date from string
        /// </summary>
        public DateOnly? GetDateFrom()
        {
            DateOnly? date;
            try
            {
                date = Helpers.ParseDateInput.DashSeparated(txtDateFrom.Text);
            }
            catch
            {
                date = null;
            }
            return date;
        }

        /// <summary>
        /// Tries to get end date from string
        /// </summary>
        public DateOnly? GetDateTo()
        {
            DateOnly? date;
            try
            {
                date = Helpers.ParseDateInput.DashSeparated(txtDateTo.Text);
            }
            catch
            {
                date = null;
            }
            return date;
        }
    }
}