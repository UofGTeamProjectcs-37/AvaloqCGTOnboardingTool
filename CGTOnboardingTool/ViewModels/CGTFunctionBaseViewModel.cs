using CGTOnboardingTool.Models.DataModels;

namespace CGTOnboardingTool.ViewModels
{
    // Abstract function for BUC's to subclass 
    public abstract class CGTFunctionBaseViewModel
    {
        public abstract ReportEntry? PerformCGTFunction(out int err, out string errMesage);

        public abstract override string ToString();
    }
}
