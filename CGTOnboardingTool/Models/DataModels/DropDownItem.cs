﻿namespace CGTOnboardingTool.Models.DataModels
{
    //Author: Aidan Neil

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
