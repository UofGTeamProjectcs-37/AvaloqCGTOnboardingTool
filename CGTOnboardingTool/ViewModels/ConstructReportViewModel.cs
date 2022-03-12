using CGTOnboardingTool.Models.DataModels;
using System;

namespace CGTOnboardingTool.ViewModels
{
    public class ConstructReportViewModel
    {
        private Report? Report;
        private string ClientName = "";
        private int YearStart;
        private int YearEnd;

        public enum CONSTRUCTREPORT_ERROR
        {
            CONSTRUCTREPORT_SUCCESS = 0,
            CONSTRUCTREPORT_VALID = 1,
            CONSTRUCTREPORT_INVALID_CLIENT_NAME = 2,
            CONSTRUCTREPORT_INVALID_DATE = 3,
        }

        public Report? GetReport()
        {
            return Report;
        }

        public void SetClientName(string clientName)
        {
            this.ClientName = clientName;
        }

        public void SetYearStart(int yearStart)
        {
            this.YearStart = yearStart;
        }

        public bool SetYearStart(string yearStart)
        {
            return int.TryParse(yearStart, out this.YearStart);
        }

        public void SetYearEnd(int yearEnd)
        {
            this.YearEnd = yearEnd;
        }

        public bool SetYearEnd(string yearEnd)
        {
            return int.TryParse(yearEnd, out this.YearEnd);
        }

        private CONSTRUCTREPORT_ERROR Validate()
        {
            // DateStart < DateEnd and must be a positive integer
            if (YearStart < 1 || YearEnd < 1 || YearStart > YearEnd)
            {
                return CONSTRUCTREPORT_ERROR.CONSTRUCTREPORT_INVALID_DATE;
            }

            if (ClientName == "")
            {
                return CONSTRUCTREPORT_ERROR.CONSTRUCTREPORT_INVALID_CLIENT_NAME;
            }

            return CONSTRUCTREPORT_ERROR.CONSTRUCTREPORT_VALID;
        }

        public CONSTRUCTREPORT_ERROR GenerateReport()
        {
            var validation = this.Validate();
            if (validation == CONSTRUCTREPORT_ERROR.CONSTRUCTREPORT_VALID)
            {
                ReportHeader header = new ReportHeader(ClientName, YearStart, YearEnd);
                this.Report = new Report(header);
                return CONSTRUCTREPORT_ERROR.CONSTRUCTREPORT_SUCCESS;
            }
            return validation;
        }
    }
}
