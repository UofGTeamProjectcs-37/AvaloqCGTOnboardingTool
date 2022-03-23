using CGTOnboardingTool.Models.DataModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CGTOnboardingTool.Models.OutputModels
{
    public class ReportExporter
    {
        // Author: Joachim Vanneste

        /// <summary>
        /// Export the report to csv
        /// Report header added before entries
        /// </summary>
        /// <param name="report"></param>
        public static void ExportToCSV(ref Report report)
        {
            // List of current rows in the report
            List<ReportEntry> row = new List<ReportEntry>(report.Rows());

            // Display windows file dialog, returns path
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "txt files (*.txt) | *.*";
            saveFile.Filter = "csv files (*.csv) | *.*";
            saveFile.DefaultExt = "csv";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;

            ReportHeader header = report.reportHeader;

            if (saveFile.ShowDialog() == true)
            {
                using (StreamWriter output = new StreamWriter(saveFile.FileName, true))
                {
                    string[] headerDetails = { header.ClientName, "\n" + header.DateStart.ToString(), header.DateEnd.ToString() };

                    // Join together the report row
                    char[] headerRow = string.Join(", ", headerDetails).ToCharArray();
                    output.Write(headerRow);

                    for (int i = 0; i < report.Count(); i++)
                    {
                        string[] _currentRow = { "\n" + row[i].Function.ToString(), row[i].Date.ToString() };

                        string securities = "";
                        string quantites = "";
                        string prices = "";
                        string costs = "";
                        string grosses = "";
                        string gains = "";
                        string holdings = "";
                        string s104s = "";

                        int k = 0;
                        foreach (Security s in row[i].Security)
                        {
                            if (k > 0)
                            {
                                securities += "&&";
                            }
                            securities += s.ShortName.ToString() + ":" + s.Name.ToString();
                            k++;
                        }

                        k = 0;
                        foreach (KeyValuePair<Security, decimal> kvp in row[i].Quantity)
                        {
                            if (k > 0)
                            {
                                quantites += "&&";
                            }

                            if (row[i].Quantity.Count > 1)
                            {
                                quantites += kvp.Key.ShortName.ToString() + ":" + kvp.Value.ToString();
                            }
                            else
                            {
                                quantites += kvp.Value.ToString();
                            }
                            k++;
                        }

                        k = 0;
                        if (row[i].Price == null)
                        {
                            foreach (Security s in row[i].Security)
                            {
                                prices = "0";
                            }
                        }
                        else
                        {
                            foreach (KeyValuePair<Security, decimal> kvp in row[i].Price)
                            {
                                if (k > 0)
                                {
                                    prices += "&&";
                                }

                                if (row[i].Price.Count > 1)
                                {

                                    prices += kvp.Key.ShortName.ToString() + ":" + kvp.Value.ToString();
                                }
                                else
                                {
                                    prices += kvp.Value.ToString();
                                }
                                k++;
                            }
                        }


                        if (row[i].AssociatedCosts == null)
                        {
                            costs = "0";
                        }
                        else
                        {
                            costs += row[i].AssociatedCosts[0].ToString();
                        }

                        k = 0;
                        if (row[i].Gross == null)
                        {
                            grosses = "0";
                        }
                        else
                        {
                            foreach (KeyValuePair<Security, decimal> kvp in row[i].Gross)
                            {
                                if (k > 0)
                                {
                                    grosses += "&&";
                                }

                                if (row[i].Gross.Count > 1)
                                {
                                    grosses += kvp.Key.ShortName.ToString() + ":" + kvp.Value.ToString();
                                }
                                else
                                {
                                    grosses += kvp.Value.ToString();
                                }
                                k++;
                            }
                        }

                        k = 0;
                        foreach (KeyValuePair<Security, decimal> kvp in row[i].GainLoss)
                        {
                            if (k > 0)
                            {
                                gains += "&&";
                            }

                            if (row[i].GainLoss.Count > 1)
                            {
                                gains += kvp.Key.ShortName.ToString() + ":" + kvp.Value.ToString();
                            }
                            else
                            {
                                gains += kvp.Value.ToString();
                            }
                            k++;
                        }

                        k = 0;
                        foreach (KeyValuePair<Security, decimal> kvp in row[i].Holdings)
                        {
                            if (k > 0)
                            {
                                holdings += "&&";
                            }

                            if (row[i].Holdings.Count > 1)
                            {
                                holdings += kvp.Key.ShortName.ToString() + ":" + kvp.Value.ToString();
                            }
                            else
                            {
                                holdings += kvp.Value.ToString();
                            }
                            k++;
                        }

                        k = 0;
                        foreach (KeyValuePair<Security, decimal> kvp in row[i].Section104)
                        {
                            if (k > 0)
                            {
                                s104s += "&&";
                            }

                            if (row[i].Section104.Count > 1)
                            {
                                s104s += kvp.Key.ShortName.ToString() + ":" + kvp.Value.ToString();
                            }
                            else
                            {
                                s104s += kvp.Value.ToString();
                            }
                            k++;
                        }

                        _currentRow = Append(_currentRow, securities);
                        _currentRow = Append(_currentRow, quantites);
                        _currentRow = Append(_currentRow, prices);
                        _currentRow = Append(_currentRow, costs);
                        _currentRow = Append(_currentRow, grosses);
                        _currentRow = Append(_currentRow, gains);
                        _currentRow = Append(_currentRow, holdings);
                        _currentRow = Append(_currentRow, s104s);

                        // Join together the report row
                        char[] rows = string.Join(",", _currentRow).ToCharArray();

                        output.Write(rows);
                    }
                }
            }
        }
        private static string[] Append(string[] array, string item)
        {
            if (array == null)
            {
                return new string[] { item };
            }
            string[] result = new string[array.Length + 1];
            array.CopyTo(result, 0);
            result[array.Length] = item;
            return result;
        }
    }
}