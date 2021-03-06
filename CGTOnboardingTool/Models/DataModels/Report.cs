using System;
using System.Collections.Generic;
using System.Linq;

namespace CGTOnboardingTool.Models.DataModels
{
    //Author: Aidan Neil
    public class Report
    {
        public ReportHeader reportHeader;

        private int count = 0; // Number of entries in a report
        private LinkedList<ReportEntry> entries = new LinkedList<ReportEntry>(); // A linked list holding all report entries in cronilogical order
        private List<ReportEntry> entriesUnordered = new List<ReportEntry>(); // A list of entries in the order that they are entered

        private List<Security> securities = new List<Security>(); // All the secuirties that have been referenced within a report

        private Dictionary<DateOnly, List<ReportEntry>> dateEntries = new Dictionary<DateOnly, List<ReportEntry>>(); // The entries that have happened on a given date
        private Dictionary<Security, List<DateOnly>> securityDates = new Dictionary<Security, List<DateOnly>>(); // The dates where there has been an action on a security
        private Dictionary<Security, List<ReportEntry>> securityEntries = new Dictionary<Security, List<ReportEntry>>(); // The cronilogical ordering of entries related to a security

        private List<String> functionsUsed = new List<String>();
        private Dictionary<Security, List<Security>> relatedSecurities = new Dictionary<Security, List<Security>>(); // A list of securities that are related through some CGTFunciton

        public Report()
        {
            this.reportHeader = new ReportHeader();
        }

        public Report(ReportHeader header)
        {
            this.reportHeader = header;
        }

        public ReportEntry[] Rows()
        {
            return entries.ToArray();
        }

        public string GetClientName()
        {
            return reportHeader.ClientName;
        }

        public int GetYearStart()
        {
            return reportHeader.DateStart;
        }

        public int GetYearEnd()
        {
            return reportHeader.DateEnd;
        }

        public int Count()
        {
            return count;
        }

        public String[] GetFunctionsUsed()
        {
            return functionsUsed.ToArray();
        }

        public bool HasSecurity(Security security)
        {
            return securities.Contains(security);
        }

        public Security[] GetSecurities()
        {
            return this.securities.ToArray();
        }


        /// <summary>
        /// Given a security, adds it to the overall report alongside the date of the action.
        /// Appends to securities and adds to securityDates[s] in sorted order.
        /// </summary>
        /// <param name="security"></param>
        /// <param name="date"></param>
        private void addSecurityActionDate(Security security, DateOnly date)
        {
            if (!this.HasSecurity(security))
            {
                this.securities.Add(security);

                List<DateOnly> dateList = new List<DateOnly> { date };
                this.securityDates.Add(security, dateList);

                List<ReportEntry> entryList = new List<ReportEntry>();
                this.securityEntries.Add(security, entryList);
            }
            else
            {
                var dates = this.securityDates[security];
                var lastDate = dates.Last();
                if (date < lastDate)
                {
                    dates.Add(date);
                    dates.Sort();
                }
                else
                {
                    dates.Add(date);
                }
            }
        }

        private void addFunction(String function)
        {
            if (!functionsUsed.Contains(function))
            {
                functionsUsed.Add(function);
            }
        }

        /// <summary>
        /// Create new report entry (using price per share)
        /// </summary>
        /// <param name="function"></param>
        /// <param name="date"></param>
        /// <param name="security"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="associatedCosts"></param>
        /// <param name="gainLoss"></param>
        /// <param name="holdings"></param>
        /// <param name="section104"></param>
        public ReportEntry AddUsingQuantityPrice(String function, DateOnly date, Security security, decimal quantity, decimal price, decimal associatedCosts, decimal gainLoss, decimal holdings, decimal section104)
        {
            this.addFunction(function);

            this.addSecurityActionDate(security, date);

            var newEntry = new ReportEntry(
                id: count++,
                function: function,
                date: date,
                security: security,
                quantity: quantity,
                price: price,
                associatedCosts: associatedCosts,
                gainLoss: gainLoss,
                holdings: holdings,
                section104: section104
                );

            securityEntries[security].Add(newEntry);
            entries.AddLast(newEntry);

            return newEntry;
        }

        /// <summary>
        /// Create new report entry (using gross)
        /// </summary>
        /// <param name="function"></param>
        /// <param name="date"></param>
        /// <param name="security"></param>
        /// <param name="quantity"></param>
        /// <param name="gross"></param>
        /// <param name="gainLoss"></param>
        /// <param name="holdings"></param>
        /// <param name="section104"></param>
        public ReportEntry AddUsingGross(String function, DateOnly date, Security security, decimal quantity, decimal gross, decimal gainLoss, decimal holdings, decimal section104)
        {
            this.addFunction(function);

            this.addSecurityActionDate(security, date);

            var newEntry = new ReportEntry(
                id: count++,
                function: function,
                date: date,
                security: security,
                quantity: quantity,
                gross: gross,
                gainLoss: gainLoss,
                holdings: holdings,
                section104: section104
                );

            securityEntries[security].Add(newEntry);
            entries.AddLast(newEntry);


            return newEntry;
        }

        public ReportEntry AddEffectingMultipleSecurities(String function, DateOnly date, Security[] securities, decimal[] quantities, decimal[]? prices, decimal[]? costs, decimal[]? grosses, decimal[] gainLosses, decimal[] holdings, decimal[] section104s)
        {
            this.addFunction(function);

            foreach (Security security in securities)
            {
                this.addSecurityActionDate(security, date);
            }

            var newEntry = new ReportEntry(
                id: count++,
                function: function,
                date: date,
                securities: securities,
                quantities: quantities,
                prices: prices,
                associatedCosts: costs,
                grosses: grosses,
                gainLosses: gainLosses,
                holdings: holdings,
                section104s: section104s);

            foreach (Security security in securities)
            {
                securityEntries[security].Add(newEntry);
            }

            entries.AddLast(newEntry);
            return newEntry;
        }

        public decimal GetSection104(Security security, DateOnly date)
        {
            // 1. Check if the security is not on report - if yes, value is 0
            if (!this.HasSecurity(security))
            {
                return 0;
            }

            // 2. Get all tge dates related to the security
            var securityActionDates = this.securityDates[security];

            // Last date with index
            var index = securityActionDates.Count - 1;
            var lastDate = securityActionDates[index];

            while (lastDate > date && index > 0)
            {
                index--;
                lastDate = securityActionDates[index];
            }

            // If index is 0 and the dates are not equal, then date searching is less than all what is on report and so 0
            if (index == 0 && date < lastDate)
            {
                return 0;
            }
            else
            {
                var securityEntries = this.securityEntries[security];
                var entry = securityEntries[index];
                return entry.Section104[security];
            }
        }
        public decimal GetHoldings(Security security, DateOnly date)
        {
            // 1. Check if the security is not on report - if yes, value is 0
            if (!this.HasSecurity(security))
            {
                return 0;
            }

            // 2. Get all tge dates related to the security
            var securityActionDates = this.securityDates[security];

            // Last date with index
            var index = securityActionDates.Count - 1;
            var lastDate = securityActionDates[index];

            while (lastDate > date && index > 0)
            {
                index--;
                lastDate = securityActionDates[index];
            }

            // If index is 0 and the dates are not equal, then date searching is less than all what is on report and so 0
            if (index == 0 && date < lastDate)
            {
                return 0;
            }
            else
            {
                var securityEntries = this.securityEntries[security];
                var entry = securityEntries[index];
                return entry.Holdings[security];
            }
        }
        private void reflectChanges(ReportEntry associatedEntry)
        {
            foreach (var security in associatedEntry.Security)
            {
                var sEntries = securityEntries[security];
                var index = sEntries.IndexOf(associatedEntry);

                var quantityChange = associatedEntry.Quantity[security];
                var gainLossChange = associatedEntry.GainLoss[security];

                for (int i = index + 1; i < sEntries.Count; i++)
                {
                    sEntries[i].Holdings[security] += quantityChange;
                    sEntries[i].Section104[security] += gainLossChange;
                }
            }
        }
    }
}