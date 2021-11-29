using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Tools
{
    public class Build
    {
        //security saved as string until securities class made
        string security = "apple";
        int no_shares = 18;
        int pps = 2;
        float cost = 10;
        float gross;



        public float performBuild(string security, int no_shares, int pps, float cost, float gross) {

            float s104p = (no_shares * pps) + cost;

            return s104p;
        }
    }
}
