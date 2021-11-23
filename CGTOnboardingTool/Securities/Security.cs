using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Securities
{
    public class Security
    {
        public String Name { get; set; }
        public String ShortName { get; set; }
        public double Section104 { get; set; }
        public List<Security>? RelatedByRebuild { get; set; }
        
        public Security(string Name, String ShortName)
        {
            this.Name = Name;
            this.ShortName = ShortName;
            Section104 = 0;
            RelatedByRebuild = new List<Security>();
        }
    }
}
