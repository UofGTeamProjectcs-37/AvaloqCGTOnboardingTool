using System;

namespace CGTOnboardingTool.Helpers
{
    //Author: Areeb Khan
    internal class ParseDateInput
    {
        /// <summary>
        /// A helper function which parses string to DateOnly
        /// </summary>
        /// <param name="dateStr"></param>
        public static DateOnly DashSeparated(string dateStr) {
            var ddmmyyyy = dateStr.Split('/');

            int day = int.Parse(ddmmyyyy[0]);
            int month = int.Parse(ddmmyyyy[1]);
            int year = int.Parse(ddmmyyyy[2]);

            return new DateOnly(year, month, day);
        }
    }
}