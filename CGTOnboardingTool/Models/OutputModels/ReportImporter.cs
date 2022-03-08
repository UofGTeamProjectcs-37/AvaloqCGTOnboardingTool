using CGTOnboardingTool.Models.DataModels;

namespace CGTOnboardingTool.Models.OutputModels
{
    public class ReportImporter
    {
        public Report report;

        public ReportImporter(ref Report report)
        {
            this.report = report;
        }

    }
}
