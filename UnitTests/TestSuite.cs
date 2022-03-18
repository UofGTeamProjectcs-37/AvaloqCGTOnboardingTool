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
        [TestBuild]
        /// <summary>
        /// Test Build calculates correct S104 (using pps and gross)
        /// </summary>
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

            // Create report
            ReportHeader header = new ReportHeader();
            header.ClientName = "John Doe";
            header.DateStart = 2021;
            header.DateEnd = 2022;
            Report report = new Report(header);

            // Create a build with pps
            BuildViewModel build_with_pps = new BuildViewModel(ref report);
            build_with_pps.security = security;
            build_with_pps.quantity = quantity1;
            build_with_pps.pps = pps;
            build_with_pps.cost = cost;
            build_with_pps.date = date1;
            build_with_pps.PerformCGTFunction(out err, out errMessage);

            // Create a build with gross
            BuildViewModel build_with_gross = new BuildViewModel(ref report);
            build_with_gross.usingGross = true;
            build_with_gross.security = security;
            build_with_gross.quantity = quantity2;
            build_with_gross.gross = gross;
            build_with_gross.date = date2;
            build_with_gross.PerformCGTFunction(out err, out errMessage);

            ReportEntry[] rows = report.Rows();

            // Get the calculated S104
            decimal build_with_pps_S104 = rows[0].Section104[security];
            decimal build_with_gross_S104 = rows[1].Section104[security];

            decimal assertS104numberpps = 16373.80M;
            decimal asserts104numbergross = 17300.23M;

            // Assertions
            Assert.AreEqual(assertS104numberpps, build_with_pps_S104, "Build with PPS S104 incorrect");
            Assert.AreEqual(asserts104numbergross , (build_with_gross_S104 + build_with_pps_S104), "Build with gross S104 incorrect");
        }


        [TestReduce]
        /// <summary>
        /// Test Reduce calculates correct S104
        /// </summary>
        public void reduceCalcsCorrectS104() {
            DateOnly date1 = new DateOnly(2021, 6, 26);
            DateOnly date2 = new DateOnly(2021, 10, 12);

            Security security = new Security("GSK", "GlaxoSmithKline");

            decimal quantity1 = 1000;
            decimal quantity2 = 1000;

            decimal pps1 = 16.35M;
            decimal pps2 = 23.82M;

            decimal cost1 = 23.82M;
            decimal cost2 = 42.50M;

            int err;
            String errMessage;

            // Create report
            ReportHeader header = new ReportHeader();
            header.ClientName = "John Doe";
            header.DateStart = 2021;
            header.DateEnd = 2022;
            Report report = new Report(header);

            // Create a build with pps
            BuildViewModel buildwithpps = new BuildViewModel(ref report);
            buildwithpps.security = security;
            buildwithpps.quantity = quantity1;
            buildwithpps.pps = pps1;
            buildwithpps.cost = cost1;
            buildwithpps.date = date1;
            buildwithpps.PerformCGTFunction(out err, out errMessage);

            // Create a reduce (reduce security we just build)
            ReduceViewModel reduce = new ReduceViewModel(ref report);
            reduce.security = security;
            reduce.quantity = quantity2;
            reduce.pps = pps2;
            reduce.cost = cost2;
            reduce.date = date2;
            reduce.PerformCGTFunction(out err, out errMessage);

            ReportEntry[] rows = report.Rows();

            // Get the calculated S104
            decimal reduces104 = rows[1].Section104[security];

            // Assertion
            Assert.AreEqual(0, reduces104, "Reduce s104 incorrect");
        }

        [TestRebuild]
        /// <summary>
        /// Test Rebuild calculates correct S104
        /// </summary>
        public void rebuildCaclulatesCorrectS104() {
            int err;
            String errMessage;

            DateOnly date1 = new DateOnly(2021, 9, 12);
            DateOnly date2 = new DateOnly(2021, 2, 17);

            Security security1 = new Security("GSK", "GlaxoSmithKline");
            Security security2 = new Security("FGP", "FGP Systems");

            decimal quantity1 = 11;
            decimal quantity2 = 3;

            decimal pps1 = 1500;
            decimal pps2 = 500;

            decimal cost1 = 800.23M;
            decimal cost2 = 26.40M;

            // Creat report
            ReportHeader header = new ReportHeader();
            header.ClientName = "John Doe";
            header.DateStart = 2021;
            header.DateEnd = 2022;
            Report report = new Report(header);
            ReportEntry[] rows = report.Rows();

            // Create two builds with pps
            BuildViewModel buildGSK = new BuildViewModel(ref report);
            buildGSK.security = security1;
            buildGSK.quantity = quantity1;
            buildGSK.pps = pps1;
            buildGSK.cost = cost1;
            buildGSK.date = date1;
            buildGSK.PerformCGTFunction(out err, out errMessage);

            BuildViewModel buildFGP = new BuildViewModel(ref report);
            buildFGP.security = security2;
            buildFGP.quantity = quantity2;
            buildFGP.pps = pps2;
            buildFGP.cost = cost2;
            buildFGP.date = date2;
            buildFGP.PerformCGTFunction(out err, out errMessage);

            // Create rebuild
            RebuildViewModel rebuildGSKFGP = new RebuildViewModel(ref report);
            rebuildGSKFGP.PerformCGTFunction(out err, out errMessage);

            // Get the calculated S104 after rebuild
            decimal rebuildS104 = rows[2].Section104[security1];

            // Assertion
            Assert.AreEqual(18826.63, rebuildS104, "Rebuild S104 incorrect");
        }
    }
}
