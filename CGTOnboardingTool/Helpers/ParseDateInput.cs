using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Helpers
{
    internal class ParseDateInput
    {
        public static DateOnly DashSeparated(string dateStr) {
            var ddmmyyyy = dateStr.Split('/');

            int day = int.Parse(ddmmyyyy[0]);
            int month = int.Parse(ddmmyyyy[1]);
            int year = int.Parse(ddmmyyyy[2]);

            return new DateOnly(year, month, day);
        }
    }
}
