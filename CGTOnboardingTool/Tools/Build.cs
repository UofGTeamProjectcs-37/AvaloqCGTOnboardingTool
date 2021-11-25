using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Tools
{
    public class Build
    {
        //security saved as string till class avialabel 
        string security = "apple";
        int no_shares = 18;
        int pps = 2;
        int buycost = 10;

        public float performBuild(string security, int no_shares, int pps, int buy_cost) {

            float s104p = (no_shares * pps) + buy_cost;

            return s104p;
        }
    }
}
