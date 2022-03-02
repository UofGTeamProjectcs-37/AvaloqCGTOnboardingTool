using CGTOnboardingTool.Securities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CGTOnboardingTool.UISections
{
    /// <summary>
    /// Interaction logic for Rebuild.xaml
    /// </summary>
    public partial class Rebuild : Page
    {
        public Report report;
        private List<Security> securities;

        public Rebuild(ref Report report)
        {
            InitializeComponent();
            this.report = report;

            Security gsk = new Security("GlaxoSmithKline", "GSK");
            Security fgp = new Security("FGP Systems", "FGP");
            Security ibe = new Security("Iberdrola", "IBE");
            Security tsla = new Security("Tesla", "TSLA");
            Security aapl = new Security("Apple", "AAPL");

            securities = new List<Security>();
            securities.Add(gsk);
            securities.Add(fgp);
            securities.Add(ibe);
            securities.Add(tsla);
            securities.Add(aapl);

            DropRebuildOldSecurity.ItemsSource = this.report.GetSecurities();
        }

        private static DateOnly ParseDate(string dateStr)
        {
            var yymmdd = dateStr.Split('/');
            int year = int.Parse(yymmdd[0]);
            int month = int.Parse(yymmdd[1]);
            int day = int.Parse(yymmdd[2]);

            return new DateOnly(year, month, day);
        }

        private void BtnRebuildCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Dashboard(ref report));
        }

        private void DropRebuildOldSecurity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DropRebuildNewSecurity.Items.Clear();

            var selected = DropRebuildOldSecurity.SelectedItem;

            foreach (var sec in securities)
            {
                if (!sec.Equals(selected))
                {
                    DropRebuildNewSecurity.Items.Add(sec);
                }
            }
        }

        private void BtnRebuild_Click(object sender, RoutedEventArgs e)
        {
            //returns true if input is not in the correct format
            bool incorrect = Validate();

            if (!incorrect)
            {
                var userInputDate = ParseDate(TxtRebuildDate.Text);

                var userInputOldSecurity = DropRebuildOldSecurity.SelectedItem as Security;
                var userInputNewSecurity = DropRebuildNewSecurity.SelectedItem as Security;

                var userInputOldSecuirtyReduce = Convert.ToDecimal(TxtRebuildOldQuantityReduce.Text);
                var userInputNewSecuirtyQuantity = Convert.ToDecimal(TxtRebuildNewQuantity.Text);

                Tools.Rebuild rb = new Tools.Rebuild(oldSecurity: userInputOldSecurity, quantityToReduce: userInputOldSecuirtyReduce, newSecurity: userInputNewSecurity, quantityToBuild: userInputNewSecuirtyQuantity, date: userInputDate);

                rb.perform(ref report);

                this.NavigationService.Navigate(new Dashboard(ref report));
            }
        }

        //Checks all inputs are in the correct format
        private bool Validate()
        {
            //Resets any previous incorrect validations
            LblRebuildDateIncorrect.Visibility = Visibility.Hidden;
            TxtRebuildDate.BorderThickness = new Thickness(0);
            LblRebuildOldSecurityIncorrect.Visibility = Visibility.Hidden;
            RebuildComboBoxOldBorder.BorderThickness = new Thickness(0);
            LblRebuildNewSecurityIncorrect.Visibility = Visibility.Hidden;
            RebuildComboBoxNewBorder.BorderThickness = new Thickness(0);
            LblRebuildOldQuantityReduceIncorrect.Visibility = Visibility.Hidden;
            TxtRebuildOldQuantityReduce.BorderThickness = new Thickness(0);
            LblRebuildNewQuantityIncorrect.Visibility = Visibility.Hidden;
            TxtRebuildNewQuantity.BorderThickness = new Thickness(0);

            try
            {
                ParseDate(TxtRebuildDate.Text);
            }
            catch
            {
                LblRebuildDateIncorrect.Visibility = Visibility.Visible;
                TxtRebuildDate.BorderThickness = new Thickness(5);

                return true;
            }

            if ((Security)DropRebuildOldSecurity.SelectedItem == null)
            {
                LblRebuildOldSecurityIncorrect.Visibility = Visibility.Visible;
                RebuildComboBoxOldBorder.BorderThickness = new Thickness(5);

                return true;
            }

            if ((Security)DropRebuildNewSecurity.SelectedItem == null)
            {
                LblRebuildNewSecurityIncorrect.Visibility = Visibility.Visible;
                RebuildComboBoxNewBorder.BorderThickness = new Thickness(5);

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtRebuildOldQuantityReduce.Text);
            } 
            catch
            {
                LblRebuildOldQuantityReduceIncorrect.Visibility = Visibility.Visible;   
                TxtRebuildOldQuantityReduce.BorderThickness = new Thickness(5);

                return true;
            }

            try
            {
                Convert.ToDecimal(TxtRebuildNewQuantity.Text);
            }
            catch
            {
                LblRebuildNewQuantityIncorrect.Visibility = Visibility.Visible;
                TxtRebuildNewQuantity.BorderThickness = new Thickness(5);

                return true;
            }

            return false;
        }
    }
}
