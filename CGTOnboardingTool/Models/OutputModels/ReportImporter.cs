using CGTOnboardingTool.Models.DataModels;
using System.IO;
using System.Diagnostics;



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
            private void openToolStripMenuItem_Click(object sender, EventArgs e)
            {
                OpenFileDialog theDialog = new OpenFileDialog();
                theDialog.Title = "Open file";
                theDialog.Filter = "Files |*.txt | *.csv";
                theDialog.InitialDirectory = @"C:\";

                if (theDialog.ShowDialog() == DialogResult.OK)
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
}
