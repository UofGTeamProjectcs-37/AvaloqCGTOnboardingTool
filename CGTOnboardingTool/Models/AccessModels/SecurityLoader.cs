using CGTOnboardingTool.Models.DataModels;
using CGTOnboardingTool.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace CGTOnboardingTool.Models.AccessModels
{
    public class SecurityLoader
    {
        public static string StoredPath { get; private set; } = String.Format("{0}Resources\\StoredSecurities.txt", System.AppDomain.CurrentDomain.BaseDirectory);
        public static List<Security> securities = new List<Security>();

        static SecurityLoader()
        {
            LoadSecurities(StoredPath);
        }

        public static int init()
        {
            return Count();
        }

        public static int Count()
        {
            return securities.Count;
        }

        // Load securities from a given file
        public static bool LoadSecurities(string filepath)
        {
            List<Security> loadedSecurities = new List<Security>();
            bool success = true;
            try
            {
                using (StreamReader sr = new StreamReader(StoredPath))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] splits = line.Split(',');
                        Security sec = new Security(splits[0], splits[1]);
                        if (!loadedSecurities.Contains(sec))
                        {
                            loadedSecurities.Add(sec);
                        }
                    }
                }
            }
            catch
            {
                return false; ;
            }
            securities.Clear();
            securities = loadedSecurities;
            return success;
        }

        // Adds a security
        public static bool AddSecurity(Security sec)
        {
            if (securities.Contains(sec))
            {
                return false;
            }
            else
            {
                securities.Add(sec);
                SecurityExporter.AppendToCSV(StoredPath, sec);
            }
            return false;
        }

        // Returns all securities for a given session
        public static Security[] GetSecurities()
        {
            return securities.ToArray();
        }
    }
}
