using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Tools
{
    public class Build
    {
        
        string security_name = "Apple";
        string short_name = "AAPL";
        int no_shares = 18;
        double pps = 217;
        double cost = 10;
        
        double gross;



        public void performBuild(string name, string short_name, int no_shares, int pps, double cost) {

            Securities.Security new_security = new Securities.Security(name, short_name);  

            new_security.Section104 = (no_shares * pps) + cost;


        }
    }
}
