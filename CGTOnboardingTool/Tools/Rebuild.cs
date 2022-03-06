using CGTOnboardingTool.Securities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Tools
{
    // Rebuild BUC which subclasses the CGTFunction abstract class
    public class Rebuild : CGTFunction
    {

        Security securityOld;
        decimal quantityOldReduce;
        Security securityNew;
        decimal quantityNewBuild;
        DateOnly date;

        // Rebuild constructor 
        public Rebuild(Security oldSecurity, decimal quantityToReduce, Security newSecurity, decimal quantityToBuild, DateOnly date)
        {
            this.securityOld = oldSecurity;
            this.quantityOldReduce = quantityToReduce;
            this.securityNew = newSecurity;
            this.quantityNewBuild = quantityToBuild;
            this.date = date;
        }

        public override ReportEntry perform(ref Report report)
        {
            // Get current values for S104 and security holdings
            var holdingsOldSecurityCurrent = report.GetHoldings(securityOld, date);
            var S104OldSecurityCurrent = report.GetSection104(securityOld, date);
            var S104NewSecurityCurrent = report.GetSection104(securityNew, date);

            var reductionRatio = quantityOldReduce / holdingsOldSecurityCurrent;

            // Calculate new S104 and Gain/Loss
            decimal S104OldSecurityUpdated = (1 - reductionRatio) * S104OldSecurityCurrent;
            decimal S104NewSecurityUpdated = S104NewSecurityCurrent + (reductionRatio * S104OldSecurityCurrent);
            decimal gainLossOldSecurity = S104OldSecurityCurrent - S104OldSecurityUpdated;
            decimal gainLossNewSecurity = S104NewSecurityCurrent - S104NewSecurityUpdated;

            // Add to report
            var associatedEntry = report.Add(
                function: this,
                date: date,
                securities: new Security[] { securityOld, securityNew },
                quantities: new decimal[] { (-1 * quantityOldReduce), quantityNewBuild },
                gainLosses: new decimal[] { gainLossOldSecurity, gainLossNewSecurity },
                section104s: new decimal[] { S104OldSecurityUpdated, S104NewSecurityUpdated }
                );

            return associatedEntry;
        }
    }
}
