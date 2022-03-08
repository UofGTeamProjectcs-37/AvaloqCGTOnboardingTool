using CGTOnboardingTool.Models.DataModels;
using System;

namespace CGTOnboardingTool.ViewModels
{
    public class ReduceViewModel : CGTFunctionBaseViewModel
    {
        Security security;
        decimal quantity;
        decimal? pps;
        decimal? cost;
        decimal? gross;
        DateOnly date;

        public ReduceViewModel(Security security, decimal quantity, decimal pps, decimal cost, DateOnly date)
        {
            this.security = security;
            this.quantity = quantity;
            this.pps = pps;
            this.cost = cost;
            this.date = date;
        }

        public ReduceViewModel(Security security, decimal quantity, decimal gross, DateOnly date)
        {
            this.security = security;
            this.quantity = quantity;
            this.gross = gross;
            this.date = date;
        }

        public override ReportEntry perform(ref Report report)
        {
            if (gross != null)
            {
                return this.performUsingGross(ref report);
            }
            else if (pps != null && cost != null)
            {
                return this.performUsingQuantityPriceCost(ref report);
            }
            else
            {
                throw new Exception("BuildView not allowed. Reconstruct build and specify either a Price & Cost pair, or a Gross");
            }
        }

        private ReportEntry performUsingGross(ref Report report)
        {
            // implement reduce using gross

            throw new NotImplementedException();
        }

        private ReportEntry performUsingQuantityPriceCost(ref Report report)
        {
            decimal pps = (decimal)this.pps;
            decimal cost = (decimal)this.cost;

            // Get current values for S104 and security holdings
            var S104Current = report.GetSection104(this.security, this.date);
            var holdingsCurrent = report.GetHoldings(this.security, this.date);

            // Calculate new holdings and the reduction 
            var reductionRatio = quantity / holdingsCurrent;

            // Calculate new S104 and Gain/Loss
            decimal S104Updated = (1 - reductionRatio) * S104Current;
            var gainLoss = S104Current - S104Updated;


            // Add to report
            var associatedEntry = report.Add(
              function: this,
              date: date,
              security: security,
              price: pps,
              quantity: -1 * quantity,
              associatedCosts: cost,
              gainLoss: gainLoss,
              section104: S104Updated
              );

            return associatedEntry;
        }
    }
}
