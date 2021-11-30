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
        Security security;
        int quantity;
        decimal pps;
        decimal cost;
        decimal gross;
        DateOnly date;

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
            decimal currentS104 = 0;
            decimal currentHoldings = 0;
            if (!report.HasSecurity(security))
            {
                report.AddSecurity(security);
            }
            else
            {
                var retrievedS104 = report.GetSection104(security);
                if (retrievedS104 != null)
                {
                    currentS104 = (decimal)retrievedS104;
                }
                var retrievedHoldings = report.GetHoldings(security);
                if (retrievedHoldings != null)
                {
                    currentHoldings = (decimal)retrievedHoldings;
                }
            }

            var newS104 = currentS104 + ((quantity * pps) + cost);
            var newHoldings = currentHoldings + quantity;

            Security[] securityAffected = new Security[1] { security };
            Dictionary<Security, decimal> priceAffected = new Dictionary<Security, decimal>();
            priceAffected.Add(security, pps);
            Dictionary<Security, decimal> quantityAffected = new Dictionary<Security, decimal>();
            quantityAffected.Add(security, quantity);
            decimal[] costs = new decimal[1] { (decimal)cost };
            Dictionary<Security, decimal> s104 = new Dictionary<Security, decimal>();
            s104.Add(security, newS104);

            var associatedEntry = report.Add(this, securityAffected, priceAffected, quantityAffected, costs, s104, date);

            report.UpdateHoldings(associatedEntry, security, newHoldings);
            report.UpdatePrice(associatedEntry, security, pps);
            report.UpdateSection104(associatedEntry, security, newS104);

            return associatedEntry;
        }

    }
}
