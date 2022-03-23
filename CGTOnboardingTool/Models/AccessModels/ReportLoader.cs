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
        public static List<Security> securities = new List<Security>();
        public ConstructReportViewModel viewModel = new ConstructReportViewModel();

        /// <summary>
        /// Import a selected txt or csv file and create a report header
        /// and report with corresponding entries
        /// </summary>
        public static Report ImportReport()
        {
            string pathToFile = ""; //to save the location of the selected object
            Report report = new Report();

            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open file";
            theDialog.Filter = "Files | *.csv";
            theDialog.InitialDirectory = @"C:\";

            if (theDialog.ShowDialog() == true)
            {
                MessageBox.Show(theDialog.FileName.ToString());
                pathToFile = theDialog.FileName;
            }

            if (File.Exists(pathToFile))
            {
                int lineNo = 0;
                int index = 0;

                // Read the file and display it line by line.
                foreach (string line in System.IO.File.ReadLines(pathToFile))
                {
                    string[] lineArray = line.Split(",");
                    if (lineNo == 0)
                    {
                        String clientName = lineArray[0];
                        report.reportHeader.ClientName = clientName;
                        lineNo++;
                    }
                    else if (lineNo == 1)
                    {
                        int dateStart = Convert.ToInt32(lineArray[0]);
                        int dateEnd = Convert.ToInt32(lineArray[1]);
                        report.reportHeader.DateStart = dateStart;
                        report.reportHeader.DateEnd = dateEnd;
                        lineNo++;
                    }
                    else
                    {
                        string function = lineArray[0];
                        DateOnly date = ParseDateInput.DashSeparated(lineArray[1]);

                        Security security;
                        Security[] secArray = null;
                        decimal[] quantityArray = null;
                        decimal[] priceArray = null;
                        decimal[] costArray = null;
                        decimal[] grossArray = null;
                        decimal[] gainsArray = null;
                        decimal[] holdingsArray = null;
                        decimal[] s104Array = null;

                        string[] secs = lineArray[2].Split("&&");

                        if (secs.Length > 1) //Rebuild
                        {

                            foreach (string s in secs)
                            {
                                string[] sec = s.Split(":");

                                security = new Security(sec[0], sec[1]);

                                if (!securities.Contains(security))
                                {
                                    securities.Add(security);
                                }

                                secArray = Append(secArray, security);
                            }

                            string[] quantities = lineArray[3].Split("&&");
                            foreach (string s in quantities)
                            {
                                string[] q = s.Split(":");
                                decimal d = Convert.ToDecimal(q[1]);
                                quantityArray = Append(quantityArray, d);
                            }

                            string[] prices = lineArray[4].Split("&&");
                            foreach (string s in prices)
                            {
                                string[] p = s.Split(":");
                                decimal d = Convert.ToDecimal(p[0]);
                                priceArray = Append(priceArray, d);
                            }

                            string[] costs = lineArray[5].Split("&&");
                            foreach (string s in costs)
                            {
                                string[] c = s.Split(":");
                                decimal d = Convert.ToDecimal(c[0]);
                                costArray = Append(costArray, d);
                            }

                            string[] grosses = lineArray[6].Split("&&");
                            foreach (string s in grosses)
                            {
                                string[] g = s.Split(":");
                                decimal d = Convert.ToDecimal(g[0]);
                                grossArray = Append(grossArray, d);
                            }

                            string[] gains = lineArray[7].Split("&&");
                            foreach (string s in gains)
                            {
                                string[] g = s.Split(":");
                                decimal d = Convert.ToDecimal(g[1]);
                                gainsArray = Append(gainsArray, d);
                            }

                            string[] holding = lineArray[8].Split("&&");
                            foreach (string s in holding)
                            {
                                string[] h = s.Split(":");
                                decimal d = Convert.ToDecimal(h[1]);
                                holdingsArray = Append(holdingsArray, d);
                            }

                            string[] s104s = lineArray[9].Split("&&");
                            foreach (string s in s104s)
                            {
                                string[] s1 = s.Split(':');
                                decimal d = Convert.ToDecimal(s1[1]);
                                s104Array = Append(s104Array, d);
                            }
                            report.AddEffectingMultipleSecurities(function: function, date: date, securities: secArray, quantities: quantityArray, prices: priceArray, costs: costArray, grosses: grossArray, gainLosses: gainsArray, holdings: holdingsArray, section104s: s104Array);
                            //ReportEntry entry = new ReportEntry(index, function, date, secArray, quantityArray, priceArray, costArray, grossArray, gainsArray, holdingsArray, s104Array);
                            //report.AppendEntry(entry);
                        }
                        else
                        {
                            string[] sec = lineArray[2].Split(":");
                            security = new Security(sec[0], sec[1]);

                            if (!securities.Contains(security))
                            {
                                securities.Add(security);
                            }

                            decimal quantity = Convert.ToDecimal(lineArray[3]);
                            decimal price = Convert.ToDecimal(lineArray[4]);
                            decimal cost = Convert.ToDecimal(lineArray[5]);
                            decimal gross = Convert.ToDecimal(lineArray[6]);
                            decimal gainLoss = Convert.ToDecimal(lineArray[7]);
                            decimal holdings = Convert.ToDecimal(lineArray[8]);
                            decimal s104 = Convert.ToDecimal(lineArray[9]);

                            if (gross == 0)
                            {
                                report.AddUsingQuantityPrice(function: function, date: date, security: security, quantity: quantity, price: price, associatedCosts: cost, gainLoss: gainLoss, holdings: holdings, section104: s104);
                                // ReportEntry entry = new ReportEntry(index, function, date, security, quantity, price, cost, gainLoss, holdings, s104);
                                //report.AppendEntry(entry);
                            }
                            else
                            {
                                report.AddUsingGross(function: function, date: date, security: security, quantity: quantity, gross: gross, gainLoss: gainLoss, holdings: holdings, section104: s104);
                                //ReportEntry entry = new ReportEntry(index, function, date, security, quantity, gross, gainLoss, holdings, s104);
                                //report.AppendEntry(entry);
                            }
                        }
                        index++;
                    }
                }
            }
            return report;
        }

        private static Security[] Append(Security[] array, Security item)
        {
            if (array == null)
            {
                return new Security[] { item };
            }
            Security[] result = new Security[array.Length + 1];
            array.CopyTo(result, 0);
            result[array.Length] = item;
            return result;
        }

        private static decimal[] Append(decimal[] array, decimal item)
        {
            if (array == null)
            {
                return new decimal[] { item };
            }
            decimal[] result = new decimal[array.Length + 1];
            array.CopyTo(result, 0);
            result[array.Length] = item;
            return result;
        }
    }
}