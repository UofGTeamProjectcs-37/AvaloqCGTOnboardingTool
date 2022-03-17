using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.Models.AccessModels;

namespace CGTOnboardingTool.ViewModels
{
    public class AddSecurityViewModel
    {
        public string ShortName { get; set; }
        public string Name{ get; set; }

        public bool AddSecurity()
        {
            bool valid = Validate();
            if (valid) { 
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
