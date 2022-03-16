using Microsoft.VisualStudio.TestTools.UnitTesting;
using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.ViewModels;
using System;
using System.Diagnostics;

namespace UnitTests
{
    [TestClass]
    public class TestSuite
    {
        [TestMethod]
        public void buildCalcsCorrectS104()
        {
            DateOnly date1 = new DateOnly(2021,6,26);
            DateOnly date2 = new DateOnly(2021,2,17);

            Security security = new Security("GSK", "GlaxoSmithKline");

            decimal quantity1 = 1000;
            decimal quantity2 = 500;

            decimal pps = 16.35M;
            decimal cost = 23.8M;
            decimal gross = 926.43M;

            int err;
            String errMessage;

            ReportHeader header = new ReportHeader();
            header.ClientName = "John Doe";
            header.DateStart = 2021;
            header.DateEnd = 2022;
            Report report = new Report(header);

            BuildViewModel build_with_pps = new BuildViewModel(ref report);
            build_with_pps.security = security;
            build_with_pps.quantity = quantity1;
            build_with_pps.pps = pps;
            build_with_pps.cost = cost;
            build_with_pps.date = date1;
            build_with_pps.PerformCGTFunction(out err, out errMessage);


            BuildViewModel build_with_gross = new BuildViewModel(ref report);
            build_with_gross.usingGross = true;
            build_with_gross.security = security;
            build_with_gross.quantity = quantity2;
            build_with_gross.gross = gross;
            build_with_gross.date = date2;

            build_with_gross.PerformCGTFunction(out err, out errMessage);

            ReportEntry[] rows = report.Rows();
            Debug.WriteLine(report.Count());

            decimal build_with_pps_S104 = rows[0].Section104[security];
            Debug.WriteLine(build_with_pps_S104);
            decimal build_with_gross_S104 = rows[1].Section104[security];
            Debug.WriteLine(build_with_gross_S104);
            decimal assertS104numberpps = 16373.80M;
            decimal asserts104numbergross = 17300.23M;

            Assert.AreEqual(assertS104numberpps, build_with_pps_S104, "Build with PPS S104 incorrect");
            Assert.AreEqual(asserts104numbergross , (build_with_gross_S104 + build_with_pps_S104), "Build with gross S104 incorrect");




        }
    }
}