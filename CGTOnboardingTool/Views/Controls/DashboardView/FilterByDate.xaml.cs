using System;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views.Controls.DashboardView
{
    /// <summary>
    /// Interaction logic for FilterByDate.xaml
    /// </summary>
    public partial class FilterByDate : UserControl
    {
        public FilterByDate()
        {
            InitializeComponent();
        }

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
