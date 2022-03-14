using CGTOnboardingTool.Models.DataModels;
using System;

namespace CGTOnboardingTool.ViewModels
{
    public class ReduceViewModel : CGTFunctionBaseViewModel
    {
        private Report report;

        public Security? security { get; set; }
        public DateOnly? date { get; set; }
        public bool usingGross { get; set; } = false;

        public decimal? quantity { get; set; }
        public decimal? pps { get; set; }
        public decimal? cost { get; set; }
        public decimal? gross { get; set; }

        public enum CGTREDUCE_ERROR
        {
            CGTREDUCE_FAILED = -1,
            CGTREDUCE_SUCCESS = 0,
            CGTREDUCE_VALID = 1,
            CGTREDUCE_NULL_SECURITY = 2,
            CGTREDUCE_INVALID_SECURITY = 3,
            CGTREDUCE_NULL_DATE = 4,
            CGTREDUCE_INVALID_DATE = 5,
            CGTREDUCE_NULL_QUANTITY = 6,
            CGTREDUCE_INVALID_QUANTITY = 7,
            CGTREDUCE_NULL_PRICE = 8,
            CGTREDUCE_INVALID_PRICE = 9,
            CGTREDUCE_NULL_COSTS = 10,
            CGTREDUCE_INVALID_COSTS = 11,
            CGTREDUCE_NULL_GROSS = 12,
            CGTREDUCE_INVALID_GROSS = 13,
        }

        public ReduceViewModel(ref Report report)
        {
            this.report = report;
        }

        public Security[] GetSecurities()
        {
            return report.GetSecurities();
        }

        public override ReportEntry? PerformCGTFunction(out int err, out string errMessage)
        {
            var validation = this.validate(out err, out errMessage);
            if (validation == CGTREDUCE_ERROR.CGTREDUCE_VALID)
            {
                if (usingGross)
                {
                    err = (int)CGTREDUCE_ERROR.CGTREDUCE_SUCCESS;
                    errMessage = "";
                    return this.performUsingGross();
                }
                else
                {
                    err = (int)CGTREDUCE_ERROR.CGTREDUCE_SUCCESS;
                    errMessage = "";
                    return this.performUsingQuantityPrice();
                }
            }
            else
            {
                return null;
            }
        }

        private ReportEntry performUsingQuantityPrice()
        {
            // Cast to explicits (non nulls)
            Security security = this.security;
            DateOnly date = (DateOnly)this.date;
            decimal quantity = (decimal)this.quantity;
            decimal pps = (decimal)this.pps;
            decimal cost = (decimal)this.cost;

            // Get current values for S104 and security holdings
            var S104Current = report.GetSection104(security, date);
            var holdingsCurrent = report.GetHoldings(security, date);

            // Calculate new holdings and the reduction 
            var reductionRatio = quantity / holdingsCurrent;

            // Calculate new S104 and Gain/Loss
            decimal S104Updated = (1 - reductionRatio) * S104Current;
            decimal holdingsUpdated = holdingsCurrent - quantity;
            decimal gainLoss = S104Current - S104Updated;

            // Add to report
            var associatedEntry = report.AddUsingQuantityPrice(
              function: this,
              date: date,
              security: security,
              quantity: -1 * quantity,
              price: pps,
              associatedCosts: cost,
              gainLoss: -1 * gainLoss,
              holdings: holdingsUpdated,
              section104: S104Updated
              );

            return associatedEntry;
        }

        private ReportEntry performUsingGross()
        {
            // Cast to explicits (non nulls)
            Security security = this.security;
            DateOnly date = (DateOnly)this.date;
            decimal quantity = (decimal)this.quantity;
            decimal gross = (decimal)this.gross;

            // Get current values for S104 and security holdings
            var S104Current = report.GetSection104(security, date);
            var holdingsCurrent = report.GetHoldings(security, date);

            // Calculate new holdings and the reduction 
            var reductionRatio = quantity / holdingsCurrent;

            // Calculate new S104 and Gain/Loss
            decimal S104Updated = (1 - reductionRatio) * S104Current;
            decimal holdingsUpdated = holdingsCurrent - quantity;
            decimal gainLoss = S104Current - S104Updated;


            // Add to report 
            var associatedEntry = report.AddUsingGross(
                function: this,
                date: date,
                security: security,
                quantity: -1 * quantity,
                gross: gross,
                gainLoss: -1 * gainLoss,
                holdings: holdingsUpdated,
                section104: S104Updated
                );

            return associatedEntry;
        }

        private CGTREDUCE_ERROR validate(out int err, out string errMessage)
        {
            if (security is null)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_NULL_SECURITY;
                errMessage = "Security Not Specified";
                return CGTREDUCE_ERROR.CGTREDUCE_NULL_SECURITY;
            }
            else if (this.date is null)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_NULL_DATE;
                errMessage = "Date Not Specified";
                return CGTREDUCE_ERROR.CGTREDUCE_NULL_DATE;
            }

            // Explicit cast of 
            DateOnly date = (DateOnly)this.date;

            int yearStart = report.GetYearStart();
            int yearEnd = report.GetYearEnd();

            int year = date.Year;
            if (year < yearStart || year > yearEnd)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_INVALID_DATE;
                errMessage = "Date does not lie within report period.";
                return CGTREDUCE_ERROR.CGTREDUCE_INVALID_DATE;
            }

            CGTREDUCE_ERROR valid;
            if (usingGross)
            {
                valid = validateGross(out err, out errMessage);
            }
            else
            {
                valid = validateQuantityPrice(out err, out errMessage);
            }

            return valid;
        }

        private CGTREDUCE_ERROR validateQuantityPrice(out int err, out string errMessage)
        {
            if (quantity is null)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_NULL_QUANTITY;
                errMessage = "Quantity not specified.";
                return CGTREDUCE_ERROR.CGTREDUCE_NULL_QUANTITY;
            }

            if (quantity <= 0)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_INVALID_QUANTITY;
                errMessage = "Quantity must be greater than 0.";
                return CGTREDUCE_ERROR.CGTREDUCE_INVALID_QUANTITY;
            }

            if (pps is null)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_NULL_PRICE;
                errMessage = "Price not specified.";
                return CGTREDUCE_ERROR.CGTREDUCE_NULL_PRICE;
            }

            if (pps < 0)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_INVALID_PRICE;
                errMessage = "Price must be greater than 0.";
                return CGTREDUCE_ERROR.CGTREDUCE_INVALID_PRICE;
            }

            if (cost is null)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_NULL_COSTS;
                errMessage = "Cost not specified.";
                return CGTREDUCE_ERROR.CGTREDUCE_NULL_COSTS;
            }

            if (cost < 0)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_INVALID_COSTS;
                errMessage = "Cost must be greater than 0.";
                return CGTREDUCE_ERROR.CGTREDUCE_INVALID_COSTS;
            }

            err = (int)CGTREDUCE_ERROR.CGTREDUCE_VALID;
            errMessage = "SUCCESS";
            return CGTREDUCE_ERROR.CGTREDUCE_VALID;
        }

        private CGTREDUCE_ERROR validateGross(out int err, out string errMessage)
        {
            if (quantity is null)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_NULL_QUANTITY;
                errMessage = "Quantity not specified.";
                return CGTREDUCE_ERROR.CGTREDUCE_NULL_QUANTITY;
            }

            if (quantity <= 0)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_INVALID_QUANTITY;
                errMessage = "Quantity must be greater than 0.";
                return CGTREDUCE_ERROR.CGTREDUCE_INVALID_QUANTITY;
            }

            if (gross is null)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_NULL_GROSS;
                errMessage = "Gross not specified.";
                return CGTREDUCE_ERROR.CGTREDUCE_NULL_GROSS;
            }

            if (gross < 0)
            {
                err = (int)CGTREDUCE_ERROR.CGTREDUCE_INVALID_GROSS;
                errMessage = "Gross must be greater than 0.";
                return CGTREDUCE_ERROR.CGTREDUCE_INVALID_GROSS;
            }

            err = (int)CGTREDUCE_ERROR.CGTREDUCE_VALID;
            errMessage = "SUCCESS";
            return CGTREDUCE_ERROR.CGTREDUCE_VALID;
        }

        public override string ToString()
        {
            return "Reduce";
        }
    }
}
