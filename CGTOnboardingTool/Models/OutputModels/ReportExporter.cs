﻿using CGTOnboardingTool.Models.DataModels;
using Microsoft.Win32;
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
            List<ReportEntry> t = new List<ReportEntry>(report.Rows());

            Stream myStream;
            // Display windows file dialog, returns path
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "txt files (*.txt) | *.*";
            saveFile.Filter = "csv files (*.csv) | *.*";
            saveFile.DefaultExt = "csv";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

            ReportHeader header = report.reportHeader;


            if (saveFile.ShowDialog() == true)
            {
                if ((myStream = saveFile.OpenFile()) != null)
                {
                    string[] headerDetails = { header.ClientName, "\n" + header.DateStart.ToString(), header.DateEnd.ToString() };
                    // Join together the report row
                    char[] headerRow = string.Join(", ", headerDetails).ToCharArray();
                    // Use stream to write the row as bytes
                    myStream.Write(uniEncoding.GetBytes(headerRow));

                    for (int i = 0; i < report.Count(); i++)
                    {
                        if (t[i].Function == "Build" || t[i].Function == "Reduce")
                        {
                            string[] _currentRow = {"\n" + t[i].Function.ToString(),
                            t[i].Date.ToString(), t[i].Security[0].ShortName.ToString(), t[i].Security[0].Name.ToString().ToString() ,t[i].Quantity[t[i].Security[0]].ToString(),
                            t[i].Price[t[i].Security[0]].ToString(), t[i].AssociatedCosts[0].ToString(),
                            t[i].GainLoss[t[i].Security[0]].ToString(), t[i].Holdings[t[i].Security[0]].ToString(),
                            t[i].Section104[t[i].Security[0]].ToString()};

                            // Join together the report row
                            char[] row = string.Join(", ", _currentRow).ToCharArray();

                            // Use stream to write the row as bytes
                            myStream.Write(uniEncoding.GetBytes(row));
                        }
                        else // the report entry is a rebuild
                        {
                            //add old and new securities
                            string[] _currentRow = {"\nRebuild", t[i].Date.ToString(), t[i].Security[0].ShortName.ToString(), t[i].Security[0].Name.ToString().ToString() ,t[i].Quantity[t[i].Security[0]].ToString(),
                            t[i].Price[t[i].Security[0]].ToString(), t[i].AssociatedCosts[0].ToString(),
                            t[i].GainLoss[t[i].Security[0]].ToString(), t[i].Holdings[t[i].Security[0]].ToString(),
                            t[i].Section104[t[i].Security[0]].ToString()};

                            // Join together the report row
                            char[] row = string.Join(", ", _currentRow).ToCharArray();

                            // Use stream to write the row as bytes
                            myStream.Write(uniEncoding.GetBytes(row));
                        }
                    }
                    myStream.Close();
                }
            }
        }
    }
}
