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
            saveFile.Filter = "txt files (*.txt) | *.*";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

            for (int i = 0; i < report.Count(); i++)
            {
                System.Diagnostics.Debug.WriteLine(t[i].Id);
                System.Diagnostics.Debug.WriteLine(t[i].Date);

                System.Diagnostics.Debug.WriteLine(t[i].Function.ToString());

                System.Diagnostics.Debug.WriteLine(t[i].Security);
                System.Diagnostics.Debug.WriteLine(t[i].Price);
                System.Diagnostics.Debug.WriteLine(t[i].Quantity);
                System.Diagnostics.Debug.WriteLine(t[i].AssociatedCosts);
                System.Diagnostics.Debug.WriteLine(t[i].GainLoss);
                System.Diagnostics.Debug.WriteLine(t[i].Holdings);
                System.Diagnostics.Debug.WriteLine(t[i].Section104);
            }

            if (saveFile.ShowDialog() == true)
            {
                if ((myStream = saveFile.OpenFile()) != null)
                {

                    for (int i = 0; i < report.Count(); i++)
                    {
                        string str = t[i].Function.ToString();
                        str = str.Insert(0, t[i].Id.ToString());
                        str = str.Insert(1, ", ");


                        str += '\n';
                        char[] row = str.ToCharArray();

                        myStream.Write(uniEncoding.GetBytes(row));
                    }
                    myStream.Close();
                }
            }

            
        }





    }
}
