using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.ViewModels;
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
  
                    // Read the file and display it line by line.  
                    foreach (string line in System.IO.File.ReadLines(pathToFile))
                    {
                    string[] lineArray = line.Split(",");
                    if (lineArray[1] == "Build")
                    {
                        if (lineArray.Length == 10) {
              

                        
                        }


                    }
                    else if (lineArray[1] == "Reduce")
                    {
                        ReduceViewModel newReduce = new ReduceViewModel(ref report);
                    }
                    else if (lineArray[1] == "Rebuild")
                    { 
                    
                        RebuildViewModel newRebuild = new RebuildViewModel(ref report);
                    }

                    }  
                      
                    System.Console.WriteLine("There were {0} lines.");  
            
                }
            



        }

    }
}



