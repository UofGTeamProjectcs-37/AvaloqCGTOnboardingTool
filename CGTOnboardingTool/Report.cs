using CGTOnboardingTool.Securities;
using CGTOnboardingTool.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool
{
    public class Report
    {
        public record ReportEntry
        {
            public int EntryID { get; init; }
            public DateOnly DatePerformed { get; init; }
            public CGTFunction FunctionPerformed { get; init; }
            public Security[] SecuritiesAffected { get; init; }
            public Dictionary<Security, decimal> PricesAffected { get; init; }
            public Dictionary<Security, int> QuantitiesAffected { get; init; }
            public decimal[] AssociatedCosts { get; init; }
            public Dictionary<Security, decimal> Section104sAfter { get; init; }
        }

        public record Section104Log
        {
            public int AssociatedReportEntryID { get; init; }
            public Security Security { get; init; }
            public decimal Section104 { get; init; }
        }

        public record SecurityHoldingLog
        {
            public int AssociatedReportEntryID { get; init; }
            public Security Security { get; init; }
            public int Quantitiy { get; init; }
        }

        public record SecurityPriceLog
        {
            public int AssociatedReportEntryID { get; init; }
            public Security Security { get; init; }
            public decimal Price { get; init; }
        }

        public record SecurityAllowableCostsLog
        {
            public int AssociatedReportEntryID { get; init; }
            public Security Security { get; init; }
            public decimal AllowableCost { get; init; }
        }

        private List<ReportEntry> _reportList;

        private List<Security> _securities;

        private Dictionary<Security, decimal> _securityPrices;
        private List<SecurityPriceLog> _securityPriceHistory;

        private Dictionary<Security, int> _holdings;
        private List<SecurityHoldingLog> _holdingsHistory;

        private Dictionary<Security, decimal> _section104;
        private List<Section104Log> _section104History;

        private Dictionary<Security, decimal> _securityAllowableCosts;
        private List<SecurityAllowableCostsLog> _securityAllowableCostsHistory;

        private Dictionary<Security, Security> _relatedSecurities;
        private int _rowCount;

        public Report()
        {
            _reportList = new List<ReportEntry>();
            _securities = new List<Security>();
            _section104 = new Dictionary<Security, decimal>();
            _section104History = new List<Section104Log>();
            _rowCount = 0;
        }

        public List<ReportEntry> Rows()
        {
            return this._reportList;
        }

        public int Count()
        {
            return this._rowCount;
        }

        public List<ReportEntry> Copy()
        {
            List<ReportEntry> reportCpy = new();
            foreach (ReportEntry entry in _reportList)
            {
                reportCpy.Add(entry);
            }
            return reportCpy;
        }

        public bool HasSecurity(Security s)
        {
            return _securities.Contains(s);
        }

        public void AddSecurity(Security sec)
        {
            if (!this.HasSecurity(sec))
            {
                this._securities.Add(sec);
            }
        }

        public Nullable<decimal> GetLastSecurityPrice(Security security)
        {
            if(this.HasSecurity(security))
            {
                return (this._securityPrices[security]);
            }
            else
            {
                return null;
            }
        }

        public List<SecurityPriceLog> GetSecurityPriceHistory(Security security)
        {
            List<SecurityPriceLog> list = new();

            foreach (SecurityPriceLog entry in _securityPriceHistory)
            {
                if (entry.Security.Equals(security))
                {
                    list.Add(entry);
                }
            }
            return list;
        }


        public Nullable<int> GetHoldings(Security security)
        {
            if (this.HasSecurity(security))
            {
                return (this._holdings[security]);
            }
            else
            {
                return null;
            }
        }

        public List<SecurityHoldingLog> GetHoldingsHistory(Security security)
        {
            List<SecurityHoldingLog> list = new();

            foreach (SecurityHoldingLog entry in _holdingsHistory)
            {
                if (entry.Security.Equals(security))
                {
                    list.Add(entry);
                }
            }
            return list;
        }

        public Nullable<decimal> GetAllowableCost(Security security)
        {
            if (this.HasSecurity(security))
            {
                return (this._securityAllowableCosts[security]);
            }
            else
            {
                return null;
            }
        }

        public List<SecurityAllowableCostsLog> GetAllowableCostsHistory(Security security)
        {
            List<SecurityAllowableCostsLog> list = new();

            foreach (SecurityAllowableCostsLog entry in _securityAllowableCostsHistory)
            {
                if (entry.Security.Equals(security))
                {
                    list.Add(entry);
                }
            }
            return list;
        }


        public Nullable<decimal> GetSection104(Security security)
        {
            if (this.HasSecurity(security))
            {
                return (this._section104[security]);
            }
            else
            {
                return null;
            }
        }

        public decimal GetSection104OrDefault(Security security, decimal suppliedDefault)
        {
            var s104 = this.GetSection104(security);
            if (s104 == null)
            {
                return suppliedDefault;
            }
            return (decimal)s104;
        }

        public List<Section104Log> GetSection104History(Security security)
        {
            List<Section104Log> list = new();

            foreach (Section104Log entry in _section104History)
            {
                if (entry.Security.Equals(security))
                {
                    list.Add(entry);
                }
            }
            return list;
        }


        public void UpdateSection104(ReportEntry associatedEntry, Security security, decimal newValue)
        {
            this._section104[security] = newValue;
            var log = new Section104Log
            {
                AssociatedReportEntryID = associatedEntry.EntryID,
                Security = security,
                Section104 = newValue
            };
            this._section104History.Add(log);
        }

        public void UpdateHoldings(ReportEntry associatedEntry, Security security, int newValue)
        {
            this._holdings[security] = newValue;
            var log = new SecurityHoldingLog
            {
                AssociatedReportEntryID = associatedEntry.EntryID,
                Security = security,
                Quantitiy = newValue
            };
            this._holdingsHistory.Add(log);
        }

        public void UpdatePrice(ReportEntry associatedEntry, Security security, decimal newValue)
        {
            this._securityPrices[security] = newValue;
            var log = new SecurityPriceLog
            {
                AssociatedReportEntryID = associatedEntry.EntryID,
                Security = security,
                Price = newValue,
            };
        }

        public void UpdateAllowableCost(ReportEntry associatedEntry, Security security, decimal newValue)
        {
            this._securityAllowableCosts[security] = newValue;
            var log = new SecurityAllowableCostsLog
            {
                AssociatedReportEntryID = associatedEntry.EntryID,
                Security = security,
                AllowableCost = newValue,
            };
        }

        public ReportEntry Add(CGTFunction FunctionPerformed, Security[] SecuritiesAffected, Dictionary<Security, decimal> PricesAffected, Dictionary<Security,int> QuantitiesAffected, decimal[] AssociatedCosts, Dictionary<Security, decimal> Section104sAfter, DateOnly DatePerformed)
        {
            var entry = new ReportEntry
            {
                EntryID = _rowCount + 1,
                DatePerformed = DatePerformed,
                FunctionPerformed = FunctionPerformed,
                SecuritiesAffected = SecuritiesAffected,
                PricesAffected = PricesAffected,
                QuantitiesAffected = QuantitiesAffected,
                AssociatedCosts = AssociatedCosts,
                Section104sAfter = Section104sAfter
            };
            _reportList.Add(entry);
            _rowCount++;
            return entry;
        }

    }
}
