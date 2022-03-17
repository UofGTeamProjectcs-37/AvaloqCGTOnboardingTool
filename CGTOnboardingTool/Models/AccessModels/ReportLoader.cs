using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.Views;
using CGTOnboardingTool.ViewModels;
using CGTOnboardingTool.Helpers; 
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace CGTOnboardingTool.Models.AccessModels
{
    public class ReportLoader
    {
        public Report report;

        public ReportLoader(ref Report report)
        {
            this.report = report;
        }

        // Import a report from txt or csv file



        public void ImportReport()
        {
            string pathToFile = "";//to save the location of the selected object

            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open file";
            theDialog.Filter = "Files | *.txt";
            theDialog.InitialDirectory = @"C:\";

            if (theDialog.ShowDialog() == true)
            {
                MessageBox.Show(theDialog.FileName.ToString());
                pathToFile = theDialog.FileName;
            }

            if (File.Exists(pathToFile))
            {

                BuildViewModel viewModel = new BuildViewModel(ref report);
                int h = 0;
                // Read the file and display it line by line.  
                foreach (string line in System.IO.File.ReadLines(pathToFile))
                {
                    string[] lineArray = line.Split(",");
                    if (h == 0)
                    {
                        String accountHolderName = lineArray[0];
                    }
                    else if (h == 1) {
                        string startDate = lineArray[0];
                        string endDate = lineArray[1];
                    }
                    else {

                        string function = lineArray[0];

                        DateOnly date = ParseDateInput.DashSeparated(lineArray[1]);

                        Security security = new Security(lineArray[1], lineArray[2]);
                        Debug.WriteLine(security);
                        decimal quantity = Convert.ToDecimal(lineArray[3]); 
                        Debug.WriteLine(quantity);
                        decimal price = Convert.ToDecimal(lineArray[4]);
                        Debug.WriteLine(price);
                        decimal cost = Convert.ToDecimal(lineArray[5]);
                        Debug.WriteLine(cost);
                        decimal gainLoss = Convert.ToDecimal(lineArray[6]);
                        Debug.WriteLine(gainLoss);
                        decimal holdings = Convert.ToDecimal(lineArray[7]);
                        Debug.WriteLine(holdings);
                        decimal s104 = Convert.ToDecimal(lineArray[8]);
                        Debug.WriteLine(s104);
                        ReportEntry entry = new ReportEntry(0, function, date, security, quantity, price, cost, gainLoss, holdings, s104);
                    }
                    h++;
                }




            }

        }
    }
}



