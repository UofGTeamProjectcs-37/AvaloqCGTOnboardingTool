using CGTOnboardingTool.Models;
using CGTOnboardingTool.Models.DataModels;
using System;

namespace CGTOnboardingTool.ViewModels
{
    // BuildView BUC which subclasses the CGTFunctionBaseViewModel abstract class
    public class BuildViewModel : CGTFunctionBaseViewModel
    {
        Security security;
        decimal quantity;
        decimal? pps;
        decimal? cost;
        decimal? gross;
        DateOnly date;

        // Overloading of BuildView constructors 
        public BuildViewModel(Security security, decimal quantity, decimal pps, decimal cost, DateOnly date)
        {
            this.security = security;
            this.quantity = quantity;
            this.pps = pps;
            this.cost = cost;
            this.date = date;
        }

        public BuildViewModel(Security security, decimal quantity, decimal gross, DateOnly date)
        {
            this.security = security;
            this.quantity = quantity;
            this.gross = gross;
            this.date = date;
        }

        public override ReportEntry perform(ref Report report)
        {
            // Call the correct perform method depending on information given
            if (gross != null)
            {
                return this.performUsingGross(ref report);
            }
            else if (pps != null && cost != null)
            {
                return this.performUsingQuantityPrice(ref report);
            }
            else
            {
                throw new Exception("Build not allowed. Reconstruct build and specify either a Price & Cost pair, or a Gross");
            }
        }

        // If we have gross, pps and a cost 
        private ReportEntry performUsingQuantityPrice(ref Report report)
        {

            
            decimal pps = (decimal)this.pps;
            decimal cost = (decimal)this.cost;

            // Update S104 and calculate Gain/Loss
            var currentS104 = report.GetSection104(this.security, this.date);
            var gainLoss = (quantity * pps) + cost;
            decimal newS104 = currentS104 + gainLoss;

            // Add to report 
            var associatedEntry = report.Add(
                function: this,
                date: this.date,
                security: this.security,
                price: pps,
                quantity: this.quantity,
                associatedCosts: cost,
                gainLoss: gainLoss,
                section104: newS104
                );

            return associatedEntry;

        }

        private ReportEntry performUsingGross(ref Report report)
        {

            throw new NotImplementedException();

            //decimal gross = (decimal)this.gross;
            //decimal currentS104 = 0;
            //decimal currentHoldings = 0;
            //if (!report.HasSecurity(security))
            //{
            //    report.AddSecurityView(security);
            //}
            //else
            //{
            //    var retrievedS104 = report.GetSection104(security);
            //    if (retrievedS104 != null)
            //    {
            //        currentS104 = (decimal)retrievedS104;
            //    }
            //    var retrievedHoldings = report.GetHoldings(security);
            //    if (retrievedHoldings != null)
            //    {
            //        currentHoldings = (decimal)retrievedHoldings;
            //    }
            //}


            //decimal newS104 = currentS104 + gross;
            //var newHoldings = currentHoldings + quantity;

            //Security[] securityAffected = new Security[1] { security };
            //Dictionary<Security, decimal> priceAffected = new Dictionary<Security, decimal>();
            //Dictionary<Security, decimal> quantityAffected = new Dictionary<Security, decimal>();
            //quantityAffected.Add(security, quantity);
            //decimal[] costs = new decimal[1];
            //Dictionary<Security, decimal> s104 = new Dictionary<Security, decimal>();
            //s104.Add(security, newS104);

            //var associatedEntry = report.Add(this, securityAffected, priceAffected, quantityAffected, costs, s104, date);

            //report.UpdateHoldings(associatedEntry, security, newHoldings);
            //report.UpdateSection104(associatedEntry, security, newS104);


            //return associatedEntry;
        }
    }
}
