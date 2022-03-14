using CGTOnboardingTool.Models.DataModels;
using System.IO;


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
            Stream myStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = theDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                        // Insert code to read the stream here.
                        }
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Error: " + ex.Message);
                }       
            }


        }

    }
}
