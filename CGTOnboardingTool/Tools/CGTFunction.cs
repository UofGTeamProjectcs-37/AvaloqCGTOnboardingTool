﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Tools
{
    // Abstract function for BUC's to subclass 
    public abstract class CGTFunction
    {
        public abstract ReportEntry perform(ref Report report);

        public override string ToString()
        {
            // Return name of BUC 
            string[] function = base.ToString().Split('.');
            return function[2];
        }
    }
}
