namespace CGTOnboardingTool.Models.DataModels
{
    public class DropDownItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
