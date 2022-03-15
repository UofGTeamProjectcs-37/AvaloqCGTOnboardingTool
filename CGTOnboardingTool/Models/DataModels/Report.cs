using CGTOnboardingTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGTOnboardingTool.Models.DataModels
{
    //Author: Aidan Neil
    public class Report
    {
        private ReportHeader reportHeader;

        private int count = 0; // Number of entries in a report
        private LinkedList<ReportEntry> entries = new LinkedList<ReportEntry>(); // A linked list holding all report entries in cronilogical order
        private List<ReportEntry> entriesUnordered = new List<ReportEntry>(); // A list of entries in the order that they are entered

        private List<Security> securities = new List<Security>(); // All the secuirties that have been referenced within a report

        private Dictionary<DateOnly, List<ReportEntry>> dateEntries = new Dictionary<DateOnly, List<ReportEntry>>(); // The entries that have happened on a given date
        private Dictionary<Security, List<DateOnly>> securityDates = new Dictionary<Security, List<DateOnly>>(); // The dates where there has been an action on a security
        private Dictionary<Security, List<ReportEntry>> securityEntries = new Dictionary<Security, List<ReportEntry>>(); // The cronilogical ordering of entries related to a security

        private List<String> functionsUsed = new List<String>();
        private Dictionary<Security, List<Security>> relatedSecurities = new Dictionary<Security, List<Security>>(); // A list of securities that are related through some CGTFunciton

        public Report(ReportHeader header)
        {
            this.reportHeader = header;
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

        public ReportEntry[] Rows()
        {
            return entries.ToArray();
        }

        public int Count()
        {
            return count;
        }

        public bool HasSecurity(Security security)
        {
            return securities.Contains(security);
        }

        public Security[] GetSecurities()
        {
            return this.securities.ToArray();
        }

        // AddSecurityAction
        //
        // Given a security, adds it to the overall report alongside the date of the action.
        // Appends to securities and adds to securityDates[s] in sorted order.
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

        public String[] GetFunctionsUsed()
        {
            return functionsUsed.ToArray();
        }

        private void addFunction(String function)
        {
            if (!functionsUsed.Contains(function))
            {
                functionsUsed.Add(function);
            }
        }


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

        public ReportEntry AddUsingQuantityPrice(String function, DateOnly date, Security[] securities, decimal[] quantities, decimal[] prices, decimal[] costs, decimal[] gainLoss, decimal[] holdings, decimal[] section104)
        {
            addFunction(function);

            throw new NotImplementedException();
        }

        public ReportEntry AddUsingGross(String function, DateOnly date, Security[] securities, decimal[] quantities, decimal[] grosses, decimal[] gainLosses, decimal[] holdings, decimal[] section104s)
        {
            throw new NotImplementedException();
        }






























        public ReportEntry Add(String function, DateOnly date, Security security, decimal price, decimal quantity, decimal associatedCosts, decimal gainLoss, decimal section104, decimal gross)
        {
            if (!functionsUsed.Contains(function))
            {
                functionsUsed.Add(function);
            }

            // 1. Add security action
            this.addSecurityActionDate(security, date);

            // 2. Calculate what the updated holdings will be
            var holdings = this.GetHoldings(security, date) + quantity;

            // 3. Construct Entry
            var newEntry = new ReportEntry(
                id: count++,
                function: function,
                date: date,
                security: security,
                price: price,
                quantity: quantity,
                associatedCosts: associatedCosts,
                gainLoss: gainLoss,
                holdings: holdings,
                section104: section104
                );

            // 4. Insert entry in correct place
            entriesUnordered.Add(newEntry);

            var securityActionDates = securityDates[security];
            var dateIndex = securityActionDates.IndexOf(date);

            var securityEntryList = securityEntries[security];
            if (securityEntryList.Count == 0)
            {
                securityEntryList.Add(newEntry);

                foreach (var s in securities)
                {
                    if (s.Equals(security))
                    {
                        continue;
                    }
                    var sDates = securityDates[s];


                }

                entries.AddLast(newEntry);
            }

            if (dateIndex == 0)
            {
                //
            }
            else
            {
                var previous = securityEntryList[dateIndex - 1];
                securityEntryList.Insert(dateIndex, newEntry);
                var previousNode = entries.Find(previous);

                if (previousNode != null)
                {
                    entries.AddAfter(previousNode, newEntry);
                    this.reflectChanges(newEntry);
                }
                else
                {
                    entries.AddLast(newEntry);
                }
            }

            // 5. Return the entry
            return newEntry;
        }

        public ReportEntry Add(String function, DateOnly date, Security[] securities, decimal[] quantities, decimal[] gainLosses, decimal[] section104s, decimal[] holdings)
        {
            decimal[] holdingsCurrent = new decimal[securities.Length];
            decimal[] oldholdings = new decimal[securities.Length];

            for (int i = 0; i < securities.Length; i++)
            {
                this.addSecurityActionDate(security: securities[i], date: date);
                holdingsCurrent[i] = this.GetHoldings(security: securities[i], date: date);
                holdings[i] = holdingsCurrent[i] + quantities[i];
            }

            var newEntry = new ReportEntry(
                id: count++,
                function: function,
                date: date,
                securities: securities,
                prices: null,
                quantities: quantities,
                associatedCosts: null,
                gainLoss: gainLosses,
                holdings: holdings,
                section104s: section104s
                );

            entriesUnordered.Add(newEntry);

            entries.AddLast(newEntry);

            return newEntry;
        }


        public ReportEntry Add(String function, DateOnly date, Security[] securities, decimal[] prices, decimal[] quantities, decimal associatedCost, decimal[] gainLoss, decimal[] section104s)
        {
            decimal[] currentHoldings = new decimal[securities.Length];
            decimal[] holdings = new decimal[securities.Length];

            for (int i = 0; i < securities.Length; i++)
            {
                this.addSecurityActionDate(security: securities[i], date: date);
                currentHoldings[i] = this.GetHoldings(security: securities[i], date: date);
                holdings[i] = currentHoldings[i] + quantities[i];
            }

            var associatedCostArr = new decimal[] { associatedCost };
            var newEntry = new ReportEntry(
                id: count++,
                function: function,
                date: date,
                securities: securities,
                prices: prices,
                quantities: quantities,
                associatedCosts: new decimal[] { associatedCost },
                gainLoss: gainLoss,
                holdings: holdings,
                section104s: section104s
                );

            entriesUnordered.Add(newEntry);

            entries.AddLast(newEntry);
            //
            // TO DO
            // Insert in position
            //

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
                index --;
                lastDate = securityActionDates[index];
            }

            // If index is 0 and the dates are not equal, then date searching is less than all what is on report and so 0 
            if (index == 0 && date < lastDate){
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