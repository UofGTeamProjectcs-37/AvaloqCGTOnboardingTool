using CGTOnboardingTool.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views.Controls.DashboardView
{
    //Author: Aidan Neil

    /// <summary>
    /// Interaction logic for FilterByFunction.xaml
    /// </summary>
    public partial class FilterByFunction : UserControl
    {
        private string[] functions;
        public FilterByFunction(string[] functions)
        {
            InitializeComponent();
            this.functions = functions;
            initExistingSecuirtyDropdown();
        }
        /// <summary>
        /// Show functions in drop down
        /// </summary>
        private void initExistingSecuirtyDropdown()
        {
            List<DropDownItem> selections = new List<DropDownItem>();

            foreach (String function in functions)
            {
                DropDownItem dropDownItem = new DropDownItem();
                dropDownItem.Text = function.ToString();
                dropDownItem.Value = function;
                selections.Add(dropDownItem);
            }
            cbFilterFunction.ItemsSource = selections;
        }
        public string? GetFunction()
        {
            var selected = cbFilterFunction.SelectedItem as DropDownItem;
            return selected.Value.ToString();
        }
    }
}