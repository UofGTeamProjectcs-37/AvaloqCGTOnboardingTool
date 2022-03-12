namespace CGTOnboardingTool.Models.DataModels
{
    public class ReportHeader
    {
        public string ClientName { get; set; }
        public int DateStart { get; set; }
        public int DateEnd { get; set; }

        public ReportHeader(string clientName, int dateStart, int dateEnd)
        {
            ClientName = clientName;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }
    }
}
