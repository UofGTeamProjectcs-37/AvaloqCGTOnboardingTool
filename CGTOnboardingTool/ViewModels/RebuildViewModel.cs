using CGTOnboardingTool.Models.AccessModels;
using CGTOnboardingTool.Models.DataModels;
using System;

namespace CGTOnboardingTool.ViewModels
{
    // RebuildView BUC which subclasses the CGTFunctionBaseViewModel abstract class
    public class RebuildViewModel : CGTFunctionBaseViewModel
    {
        private Report report;

        public DateOnly? date { get; set; }
        public Security? securityOld { get; set; }
        public Security? securityNew { get; set; }
        public decimal? quantityOldReduce { get; set; }
        public decimal? quantityNewBuild { get; set; }

        public enum CGTREBUILD_ERROR
        {
            CGTREBUILD_FAILED = -1,
            CGTREBUILD_SUCCESS = 0,
            CGTREBUILD_VALID = 1,
            CGTREBUILD_NULL_DATE = 4,
            CGTREBUILD_INVALID_DATE = 5,
            CGTREBUILD_NULL_SECURITY_OLD = 2,
            CGTREBUILD_INVALID_SECURITY_OLD = 3,
            CGTREBUILD_NULL_SECURITY_NEW = 2,
            CGTREBUILD_INVALID_SECURITY_NEW = 3,
            CGTREBUILD_NULL_QUANTITY_OLD = 6,
            CGTREBUILD_INVALID_QUANTITY_OLD = 7,
            CGTREBUILD_NULL_QUANTITY_NEW = 6,
            CGTREBUILD_INVALID_QUANTITY_NEW = 7,
        }

        public RebuildViewModel(ref Report report)
        {
            this.report = report;
        }

        public Report GetReport()
        {
            return this.report;
        }

        public Security[] GetSecuritiesExisting()
        {
            return report.GetSecurities();
        }

        public Security[] GetSecuritiesBuild()
        {
            return SecurityLoader.GetSecurities();
        }

        public override ReportEntry? PerformCGTFunction(out int err, out string errMessage)
        {
            var validation = this.validate(out err, out errMessage);
            if (validation == CGTREBUILD_ERROR.CGTREBUILD_VALID)
            {
                // Explicit casts (non-nulls)
                DateOnly date = (DateOnly)this.date;
                Security securityOld = this.securityOld;
                Security securityNew = this.securityNew;
                decimal quantityOldReduce = (decimal)this.quantityOldReduce;
                decimal quantityNewBuild = (decimal)this.quantityNewBuild;

                // Get current values for S104 and security holdings
                var holdingsOldSecurityCurrent = report.GetHoldings(securityOld, date);
                var holdingsNewSecurityCurrent = report.GetHoldings(securityNew, date);
                var S104OldSecurityCurrent = report.GetSection104(securityOld, date);
                var S104NewSecurityCurrent = report.GetSection104(securityNew, date);

                var reductionRatio = quantityOldReduce / holdingsOldSecurityCurrent;

                // Calculate new S104 and Gain/Loss
                decimal S104OldSecurityUpdated = (1 - reductionRatio) * S104OldSecurityCurrent;
                decimal S104NewSecurityUpdated = S104NewSecurityCurrent + (reductionRatio * S104OldSecurityCurrent);

                decimal gainLossOldSecurity = S104OldSecurityCurrent - S104OldSecurityUpdated;
                decimal gainLossNewSecurity = S104NewSecurityUpdated - S104NewSecurityCurrent;

                decimal holdingsOldSecurityUpdated = holdingsOldSecurityCurrent - quantityOldReduce;
                decimal holdingsNewSecurityUpdated = holdingsNewSecurityCurrent + quantityNewBuild;


                // Add to report
                var associatedEntry = report.AddEffectingMultipleSecurities(
                    function: this.ToString(),
                    date: date,
                    securities: new Security[] { securityOld, securityNew },
                    quantities: new decimal[] { (-1 * quantityOldReduce), quantityNewBuild },
                    prices: null,
                    costs: null,
                    grosses: null,
                    gainLosses: new decimal[] { (-1 * gainLossOldSecurity), gainLossNewSecurity },
                    holdings: new decimal[] { holdingsOldSecurityUpdated, holdingsNewSecurityUpdated },
                    section104s: new decimal[] { S104OldSecurityUpdated, S104NewSecurityUpdated }
                    );

                err = (int)CGTREBUILD_ERROR.CGTREBUILD_SUCCESS;
                errMessage = "";
                return associatedEntry;
            }
            else
            {

                return null;
            }
        }

        private CGTREBUILD_ERROR validate(out int err, out string errMessage)
        {
            if (securityOld is null)
            {
                err = (int)CGTREBUILD_ERROR.CGTREBUILD_NULL_SECURITY_OLD;
                errMessage = "Old Security not specified.";
                return CGTREBUILD_ERROR.CGTREBUILD_NULL_SECURITY_OLD;
            }
            if (securityNew is null)
            {
                err = (int)CGTREBUILD_ERROR.CGTREBUILD_NULL_SECURITY_NEW;
                errMessage = "New Security not specified.";
                return CGTREBUILD_ERROR.CGTREBUILD_NULL_SECURITY_NEW;
            }
            if (this.date is null)
            {
                err = (int)CGTREBUILD_ERROR.CGTREBUILD_NULL_DATE;
                errMessage = "Date Not Specified";
                return CGTREBUILD_ERROR.CGTREBUILD_NULL_DATE;
            }

            if (quantityOldReduce is null)
            {
                err = (int)CGTREBUILD_ERROR.CGTREBUILD_NULL_QUANTITY_OLD;
                errMessage = "Old quantity not specified.";
                return CGTREBUILD_ERROR.CGTREBUILD_NULL_QUANTITY_OLD;
            }

            if (quantityNewBuild is null)
            {
                err = (int)CGTREBUILD_ERROR.CGTREBUILD_NULL_QUANTITY_NEW;
                errMessage = "New  quantity not specified.";
                return CGTREBUILD_ERROR.CGTREBUILD_NULL_QUANTITY_NEW;
            }

            // Explicit cast of date
            DateOnly date = (DateOnly)this.date;

            int yearStart = report.GetYearStart();
            int yearEnd = report.GetYearEnd();

            int year = date.Year;
            if (year < yearStart || year > yearEnd)
            {
                err = (int)CGTREBUILD_ERROR.CGTREBUILD_INVALID_DATE;
                errMessage = "Date does not lie within report period.";
                return CGTREBUILD_ERROR.CGTREBUILD_INVALID_DATE;
            }

            var currentOldHoldings = report.GetHoldings(securityOld, date);
            if (quantityOldReduce <= 0 || quantityOldReduce > currentOldHoldings)
            {
                err = (int)CGTREBUILD_ERROR.CGTREBUILD_NULL_QUANTITY_OLD;
                errMessage = "New quantity must be between 0 and the quantity of holdings on report.";
                return CGTREBUILD_ERROR.CGTREBUILD_NULL_QUANTITY_OLD;
            }
            if (quantityNewBuild <= 0)
            {
                err = (int)CGTREBUILD_ERROR.CGTREBUILD_INVALID_QUANTITY_NEW;
                errMessage = "New  quantity must be greater than 0.";
                return CGTREBUILD_ERROR.CGTREBUILD_INVALID_QUANTITY_NEW;
            }

            err = (int)CGTREBUILD_ERROR.CGTREBUILD_VALID;
            errMessage = "Validation passes";
            return CGTREBUILD_ERROR.CGTREBUILD_VALID;

        }

        public override string ToString()
        {
            return "Rebuild";
        }
    }
}
