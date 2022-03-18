namespace CGTOnboardingTool.Models.DataModels
{
    public class ReportHeader
    {
        public string ClientName { get; set; }
        public int DateStart { get; set; }
        public int DateEnd { get; set; }

        public ReportHeader()
        {
            ClientName = "Unknown";
            DateStart = 0;
            DateEnd = 0;
        }

        /// <summary>
        /// Report header constructor
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        public ReportHeader(string clientName, int dateStart, int dateEnd)
        {
            ClientName = clientName;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }
    }
}
