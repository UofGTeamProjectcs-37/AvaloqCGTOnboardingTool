using CGTOnboardingTool.Models.DataModels;
using System;
using System.Collections.Generic;

namespace CGTOnboardingTool.ViewModels
{
    //Author: Aidan Neil

    public class DashboardViewModel
    {
        private Report report;
        public DashboardViewModel(ref Report report)
        {
            this.report = report;
        }
        public Report GetReport()
        {
            return this.report;
        }

        public ReportEntry[] Rows()
        {
            return report.Rows();
        }

        public Security[] GetSecuritiesExisting()
        {
            return report.GetSecurities();
        }

        public String[] GetFunctionsOnReport()
        {
            return report.GetFunctionsUsed();
        }

        public string GetYearEnd()
        {
            return report.GetYearEnd().ToString();
        }

        public string GetYearStart()
        {
            return report.GetYearStart().ToString();
        }

        public string GetClientName()
        {
            return report.GetClientName();
        }

        /// <summary>
        /// Filters report by date
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        public ReportEntry[] FilterByDate(DateOnly dateFrom, DateOnly dateTo)
        {
            ReportEntry[] reportRows = report.Rows();
            List<ReportEntry> filteredRows = new List<ReportEntry>();

            DateOnly startDate = dateFrom; //change to user input from drop down menu
            DateOnly endDate = dateTo; //change to user input from drop down menu

            foreach (ReportEntry row in reportRows)
            {
                if ((row.Date < endDate) && (row.Date > startDate))
                {
                    filteredRows.Add(row);
                }
            }
            return filteredRows.ToArray();
        }

        /// <summary>
        /// Filters report by security
        /// </summary>
        /// <param name="security"></param>
        public ReportEntry[] FilterBySecuirty(Security security)
        {
            List<ReportEntry> filteredRows = new List<ReportEntry>();
            ReportEntry[] reportRows = report.Rows();

            foreach (ReportEntry row in reportRows)
            {
                foreach (Security sec in row.Security)
                {
                    if (sec.Equals(security))
                    {
                        filteredRows.Add(row);
                    }
                }
            }
            return filteredRows.ToArray();
        }

        /// <summary>
        /// Filters report by function
        /// </summary>
        /// <param name="function"></param>
        public ReportEntry[] FilterByFunction(String function)
        {
            List<ReportEntry> filteredRows = new List<ReportEntry>();
            ReportEntry[] reportRows = report.Rows();

            foreach (ReportEntry row in report.Rows())
            {
                if (row.Function.Equals(function))
                {
                    filteredRows.Add(row);
                }
            }
            return filteredRows.ToArray();
        }
    }
}