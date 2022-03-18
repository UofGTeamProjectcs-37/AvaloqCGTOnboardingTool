using CGTOnboardingTool.Models.AccessModels;
using CGTOnboardingTool.Models.DataModels;

namespace CGTOnboardingTool.ViewModels
{
    public class AddSecurityViewModel
    {
        public string ShortName { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Add security to list of securites for the current report
        /// </summary>
        public bool AddSecurity()
        {
            bool valid = Validate();
            if (valid)
            {
                Security newSecurity = new Security(ShortName, Name);
                return SecurityLoader.AddSecurity(newSecurity);
            }
            return false;
        }

        private bool Validate()
        {
            if (ShortName.Length > 5)
            {
                return false;
            }
            return true;
        }
    }
}
