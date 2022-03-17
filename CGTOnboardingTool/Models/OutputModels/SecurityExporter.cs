using CGTOnboardingTool.Models.DataModels;
using System.IO;

namespace CGTOnboardingTool.Models.OutputModels
{
    // Author: Andrew Bell
    public class SecurityExporter
    {
        public static bool AppendToCSV(string filepath, Security security)
        {
            using (StreamWriter sw = File.AppendText(filepath))
            {
                sw.Write("\n" + security.ShortName + "," + security.Name);
            }
            return true;
        }
        public static bool Export(string filepath, Security[] securities)
        {
            return false;
        }
    }
}
