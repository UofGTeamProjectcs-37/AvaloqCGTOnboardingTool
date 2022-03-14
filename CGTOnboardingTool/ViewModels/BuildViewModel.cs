using CGTOnboardingTool.Models.AccessModels;
using CGTOnboardingTool.Models.DataModels;
using System;

namespace CGTOnboardingTool.ViewModels
{
    // BuildView BUC which subclasses the CGTFunctionBaseViewModel abstract class
    public class BuildViewModel : CGTFunctionBaseViewModel
    {
        private Report report;

        // Allow NULLS to allow construct before user input has been given
        public Security? security { get; set; }
        public DateOnly? date { get; set; }
        public bool usingGross { get; set; } = false;

        public decimal? quantity { get; set; }
        public decimal? pps { get; set; }
        public decimal? cost { get; set; }
        public decimal? gross { get; set; }

        public enum CGTBUILD_ERROR
        {
            CGTBUILD_FAILED = -1,
            CGTBUILD_SUCCESS = 0,
            CGTBUILD_VALID = 1,
            CGTBUILD_NULL_SECURITY = 2,
            CGTBUILD_INVALID_SECURITY = 3,
            CGTBUILD_NULL_DATE = 4,
            CGTBUILD_INVALID_DATE = 5,
            CGTBUILD_NULL_QUANTITY = 6,
            CGTBUILD_INVALID_QUANTITY = 7,
            CGTBUILD_NULL_PRICE = 8,
            CGTBUILD_INVALID_PRICE = 9,
            CGTBUILD_NULL_COSTS = 10,
            CGTBUILD_INVALID_COSTS = 11,
            CGTBUILD_NULL_GROSS = 12,
            CGTBUILD_INVALID_GROSS = 13,
        }

        public BuildViewModel(ref Report report)
        {
            this.report = report;
        }

        public Security[] GetSecurities()
        {
            return SecurityLoader.GetSecurities();
        }

        public override ReportEntry? PerformCGTFunction(out int err, out string errMessage)
        {
            var validation = this.validate(out err, out errMessage);
            if (validation == CGTBUILD_ERROR.CGTBUILD_VALID)
            {
                if (usingGross)
                {
                    err = (int)CGTBUILD_ERROR.CGTBUILD_SUCCESS;
                    errMessage = "";
                    return this.performUsingGross();
                }
                else
                {
                    err = (int)CGTBUILD_ERROR.CGTBUILD_SUCCESS;
                    errMessage = "";
                    return this.performUsingQuantityPrice();
                }
            }
            else
            {
                return null;
            }
        }

        // If we have gross, pps and a cost 
        private ReportEntry performUsingQuantityPrice()
        {
            // Cast to explicits (non nulls)
            Security security = this.security;
            DateOnly date = (DateOnly)this.date;
            decimal quantity = (decimal)this.quantity;
            decimal pps = (decimal)this.pps;
            decimal cost = (decimal)this.cost;

            // Update S104, Holdings and calculate Gain/Loss
            decimal currentS104 = report.GetSection104(security, date);
            decimal currentHoldings = report.GetHoldings(security, date);

            decimal gainLoss = ((quantity * pps) + cost);
            decimal newS104 = currentS104 + gainLoss;
            decimal newHoldings = currentHoldings + quantity;

            // Add to report 
            var associatedEntry = report.AddUsingQuantityPrice(
                function: this,
                date: date,
                security: security,
                quantity: quantity,
                price: pps,
                associatedCosts: cost,
                gainLoss: gainLoss,
                holdings: newHoldings,
                section104: newS104
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

            // Update S104, Holdings and calculate Gain/Loss
            decimal currentS104 = report.GetSection104(security, date);
            decimal currentHoldings = report.GetHoldings(security, date);

            decimal gainLoss = gross;
            decimal newS104 = currentS104 + gainLoss;
            decimal newHoldings = currentHoldings + quantity;

            // Add to report 
            var associatedEntry = report.AddUsingGross(
                function: this,
                date: date,
                security: security,
                quantity: quantity,
                gross: gross,
                gainLoss: gainLoss,
                holdings: newHoldings,
                section104: newS104
                );

            return associatedEntry;
        }

        private CGTBUILD_ERROR validate(out int err, out string errMessage)
        {
            if (security is null)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_NULL_SECURITY;
                errMessage = "Security Not Specified";
                return CGTBUILD_ERROR.CGTBUILD_NULL_SECURITY;
            }
            else if (this.date is null)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_NULL_DATE;
                errMessage = "Date Not Specified";
                return CGTBUILD_ERROR.CGTBUILD_NULL_DATE;
            }

            // Explicit cast of 
            DateOnly date = (DateOnly)this.date;

            int yearStart = report.GetYearStart();
            int yearEnd = report.GetYearEnd();

            int year = date.Year;
            if (year < yearStart || year > yearEnd)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_INVALID_DATE;
                errMessage = "Date does not lie within report period.";
                return CGTBUILD_ERROR.CGTBUILD_INVALID_DATE;
            }

            CGTBUILD_ERROR valid;
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

        private CGTBUILD_ERROR validateQuantityPrice(out int err, out string errMessage)
        {
            if (quantity is null)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_NULL_QUANTITY;
                errMessage = "Quantity not specified.";
                return CGTBUILD_ERROR.CGTBUILD_NULL_QUANTITY;
            }

            if (quantity <= 0)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_INVALID_QUANTITY;
                errMessage = "Quantity must be greater than 0.";
                return CGTBUILD_ERROR.CGTBUILD_INVALID_QUANTITY;
            }

            if (pps is null)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_NULL_PRICE;
                errMessage = "Price not specified.";
                return CGTBUILD_ERROR.CGTBUILD_NULL_PRICE;
            }

            if (pps < 0)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_INVALID_PRICE;
                errMessage = "Price must be greater than 0.";
                return CGTBUILD_ERROR.CGTBUILD_INVALID_PRICE;
            }

            if (cost is null)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_NULL_COSTS;
                errMessage = "Cost not specified.";
                return CGTBUILD_ERROR.CGTBUILD_NULL_COSTS;
            }

            if (cost < 0)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_INVALID_COSTS;
                errMessage = "Cost must be greater than 0.";
                return CGTBUILD_ERROR.CGTBUILD_INVALID_COSTS;
            }

            err = (int)CGTBUILD_ERROR.CGTBUILD_VALID;
            errMessage = "SUCCESS";
            return CGTBUILD_ERROR.CGTBUILD_VALID;
        }

        private CGTBUILD_ERROR validateGross(out int err, out string errMessage)
        {
            if (quantity is null)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_NULL_QUANTITY;
                errMessage = "Quantity not specified.";
                return CGTBUILD_ERROR.CGTBUILD_NULL_QUANTITY;
            }

            if (quantity <= 0)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_INVALID_QUANTITY;
                errMessage = "Quantity must be greater than 0.";
                return CGTBUILD_ERROR.CGTBUILD_INVALID_QUANTITY;
            }

            if (gross is null)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_NULL_GROSS;
                errMessage = "Gross not specified.";
                return CGTBUILD_ERROR.CGTBUILD_NULL_GROSS;
            }

            if (gross < 0)
            {
                err = (int)CGTBUILD_ERROR.CGTBUILD_INVALID_GROSS;
                errMessage = "Gross must be greater than 0.";
                return CGTBUILD_ERROR.CGTBUILD_INVALID_GROSS;
            }

            err = (int)CGTBUILD_ERROR.CGTBUILD_VALID;
            errMessage = "SUCCESS";
            return CGTBUILD_ERROR.CGTBUILD_VALID;
        }

        public override string ToString()
        {
            return "Build";
        }
    }
}
