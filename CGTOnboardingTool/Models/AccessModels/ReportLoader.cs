using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.ViewModels;
using CGTOnboardingTool.Helpers;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Collections.Generic;

namespace CGTOnboardingTool.Models.AccessModels
{
    public class ReportLoader
    {
        public Report report;
        public static List<Security> securities = new List<Security>();
        public ConstructReportViewModel viewModel = new ConstructReportViewModel();

        public ReportLoader(ref Report report)
        {
            this.report = report;
        }


        /// <summary>
        /// Import a selected txt or csv file and create a report header
        /// and report with corresponding entries
        /// </summary>
        public void ImportReport()
        {
            string pathToFile = ""; //to save the location of the selected object

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
                int h = 0;
                int index = 0;
                // Read the file and display it line by line.
                foreach (string line in System.IO.File.ReadLines(pathToFile))
                {
                    string[] lineArray = line.Split(",");
                    if (h == 0)
                    {
                        String accountHolderName = lineArray[0];
                        h++;
                    } else if (h == 1) {
                        int dateStart = Convert.ToInt32(lineArray[0]);
                        int dateEnd = Convert.ToInt32(lineArray[1]);
                        h++;
                    } else {
                        string function = lineArray[0];
                        DateOnly date = ParseDateInput.DashSeparated(lineArray[1]);
                        Security security = new Security(lineArray[1], lineArray[2]);

                        if (!securities.Contains(security)) {
                          securities.Add(security);
                        }
                        decimal quantity = Convert.ToDecimal(lineArray[3]);
                        decimal price = Convert.ToDecimal(lineArray[4]);
                        decimal cost = Convert.ToDecimal(lineArray[5]);
                        decimal gainLoss = Convert.ToDecimal(lineArray[6]);
                        decimal holdings = Convert.ToDecimal(lineArray[7]);
                        decimal s104 = Convert.ToDecimal(lineArray[8]);

                        ReportEntry entry = new ReportEntry(index, function, date, security, quantity, price, cost, gainLoss, holdings, s104);
                        index++;
                    }

                }
                  //ReportHeader header = new ReportHeader(accountHolderName, dateStart, dateEnd);
                 // Report report = new Report(header);
            }
        }
    }
}
