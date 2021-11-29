using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Securities
{
    public class Security
    {
        public String Name { get; init; }
        public String ShortName { get; init; }
        
        
        public Security(string Name, String ShortName)
        {
            this.Name = Name;
            this.ShortName = ShortName;
        }
    }
}