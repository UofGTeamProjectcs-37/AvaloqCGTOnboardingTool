using CGTOnboardingTool.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views.Controls.DashboardView
{
    /// <summary>
    /// Interaction logic for FilterBySecurity.xaml
    /// </summary>
    public partial class FilterBySecurity : UserControl
    {
        private Security[] securities;
        public FilterBySecurity(Security[] securities)
        {
            InitializeComponent();
            this.securities = securities;
            initExistingSecuirtyDropdown();
        }

        private void initExistingSecuirtyDropdown()
        {

            List<DropDownItem> selections = new List<DropDownItem>();

            foreach (Security security in securities)
            {
                DropDownItem dropDownItem = new DropDownItem();
                dropDownItem.Text = security.ToString();
                dropDownItem.Value = security;
                selections.Add(dropDownItem);
            }

            cbFilterSecurity.ItemsSource = selections;

        }

        public Security? GetSecurity()
        {
            var selected = cbFilterSecurity.SelectedItem as DropDownItem;
            return selected.Value as Security;
        }
    }
}
