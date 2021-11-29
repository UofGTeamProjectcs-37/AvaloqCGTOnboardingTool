using CGTOnboardingTool.Securities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Tools
{
    public class Build : CGTFunction
    {

        // Use these instead
        Security security;
        int quantity;
        decimal pps;
        decimal cost;
        decimal gross;
        DateOnly date;



        // Old stub variables
        string security_name = "Apple";
        string short_name = "AAPL";
        int no_shares = 18;
        //double pps = 217;
        //double cost = 10;
        //double gross;


        public Build(Security security, int quantity, decimal pps, decimal cost, decimal gross, DateOnly date)
        {
            this.security = security;
            this.quantity = quantity;
            this.pps = pps;
            this.cost = cost;
            this.gross = gross;
            this.date = date;

            
        }

        public override Report.ReportEntry perform(ref Report report)
        {
            // implement the Build Funciton here using appropriate Report methods (Such as UpdateSection104)
            // return the report entry given when the Build is added to the report using report.Add()


            return new Report.ReportEntry();
        }

        public void performBuild(string name, string short_name, int no_shares, int pps, double cost)
        {
            //Securities.Security new_security = new Securities.Security(name, short_name);

            //new_security.Section104 = (no_shares * pps) + cost;
        }
    }
}
