using CGTOnboardingTool.Models.DataModels;

namespace CGTOnboardingTool.ViewModels
{
    // Abstract function for BUC's to subclass 
    public abstract class CGTFunctionBaseViewModel
    {
        public abstract ReportEntry perform(ref Report report);

        public override string ToString()
        {
            // Return name of BUC 
            string[] function = base.ToString().Split('.');
            return function[2];
        }
    }
}
