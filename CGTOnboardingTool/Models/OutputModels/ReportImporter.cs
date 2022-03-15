using CGTOnboardingTool.Models.DataModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace CGTOnboardingTool.Models.OutputModels
{
    public class ReportImporter
    {
        public Report report;

        public ReportImporter(ref Report report)
        {
            this.report = report;
        }

        // Import a report from txt or csv file
        public void ImportReport() 
        {
            string pathToFile = "";//to save the location of the selected object
            
                OpenFileDialog theDialog = new OpenFileDialog();
                theDialog.Title = "Open file";
                theDialog.Filter = "Files |*.txt | *.csv";
                theDialog.InitialDirectory = @"C:\";

                if (theDialog.ShowDialog() == true)
                {
                    MessageBox.Show(theDialog.FileName.ToString());
                    pathToFile = theDialog.FileName;
                }

                if (File.Exists(pathToFile))
                {
                    string text = "";
                    using(StreamReader sr = new StreamReader(pathToFile))
                    {
                        text = sr.ReadToEnd();//all text wil be saved in text enters are also saved
                        Debug.WriteLine(text);
                    }
                }
            



        }

    }
}
