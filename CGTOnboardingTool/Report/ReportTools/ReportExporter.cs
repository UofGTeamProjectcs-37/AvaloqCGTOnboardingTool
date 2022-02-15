using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CGTOnboardingTool.ReportTools
{
    public class ReportExporter
    {
        public Report report;

        public ReportExporter(ref Report report)
        {
            this.report = report;
        }

        public void ExportToText()
        {
            List<ReportEntry> t = new List<ReportEntry>(report.Rows());

            Stream myStream;
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "csv files (*.csv) | *.*";
            saveFile.DefaultExt = "csv";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

           

            if (saveFile.ShowDialog() == true)
            {
                if ((myStream = saveFile.OpenFile()) != null)
                {


                    for (int i = 0; i < report.Count(); i++)
                    {
                        string[] _currentRow = { t[i].Id.ToString(), t[i].Function.ToString(),
                            t[i].Date.ToString(), t[i].Security[0].Name,t[i].Quantity[t[i].Security[0]].ToString(),
                            t[i].Price[t[i].Security[0]].ToString(), t[i].AssociatedCosts[0].ToString(),
                            t[i].GainLoss[t[i].Security[0]].ToString(), t[i].Holdings[t[i].Security[0]].ToString(),
                            t[i].Section104[t[i].Security[0]].ToString(), "\n"};

                        char[] row = string.Join(", ", _currentRow).ToCharArray();

                     

                        myStream.Write(uniEncoding.GetBytes(row));
                    }
                    myStream.Close();
                }
            }

            
        }



}



    }