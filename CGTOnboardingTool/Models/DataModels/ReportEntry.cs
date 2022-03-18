using System;
using System.Collections.Generic;

namespace CGTOnboardingTool.Models.DataModels
{
    // Author Aidan Neil
    public class ReportEntry
    {
        public int Id { get; init; }
        public String Function { get; init; }
        public DateOnly Date { get; init; }
        public Security[] Security { get; init; }
        public Dictionary<Security, decimal> Quantity { get; init; }
        public Dictionary<Security, decimal>? Price { get; init; }
        public decimal[]? AssociatedCosts { get; init; }
        public Dictionary<Security, decimal>? Gross { get; init; }
        public Dictionary<Security, decimal> GainLoss { get; init; }
        public Dictionary<Security, decimal> Holdings { get; set; }
        public Dictionary<Security, decimal> Section104 { get; set; }


        public ReportEntry(int id, String function, DateOnly date, Security[] securities, decimal[] quantities, decimal[] prices, decimal[] associatedCosts, decimal[] grosses, decimal[] gainLosses, decimal[] holdings, decimal[] section104s)
        {
            Id = id;
            Function = function;
            Date = date;
            Security = new Security[securities.Length];
            for (int i = 0; i < securities.Length; i++)
            {
                this.Security[i] = securities[i];
            }
            this.Price = new Dictionary<Security, decimal>();
            if (prices != null)
            {
                for (int i = 0; i < prices.Length; i++)
                {
                    Price.Add(securities[i], prices[i]);
                }
            }

            this.Quantity = new Dictionary<Security, decimal>();

            if (associatedCosts != null)
            {
                this.AssociatedCosts = new decimal[associatedCosts.Length];
                for (int i = 0; i < associatedCosts.Length; i++)
                {
                    this.AssociatedCosts[i] = associatedCosts[i];
                }
            }
            else
            {
                this.AssociatedCosts = null;
            }

            Gross = new Dictionary<Security, decimal>();
            if (grosses != null)
            {
                for (int i = 0; i < grosses.Length; i++)
                {
                    Gross.Add(securities[i], grosses[i]);
                }
            }
            this.GainLoss = new Dictionary<Security, decimal>();
            this.Holdings = new Dictionary<Security, decimal>();
            this.Section104 = new Dictionary<Security, decimal>();
            for (int i = 0; i < securities.Length; i++)
            {

                this.Quantity.Add(securities[i], quantities[i]);
                this.GainLoss.Add(securities[i], gainLosses[i]);
                this.Holdings.Add(securities[i], holdings[i]);
                this.Section104.Add(securities[i], section104s[i]);
            }
        }








        public ReportEntry(int id, String function, DateOnly date, Security security, decimal quantity, decimal price, decimal associatedCosts, decimal gainLoss, decimal holdings, decimal section104)
        {
            this.Id = id;
            this.Function = function;
            this.Date = date;
            this.Security = new Security[] { security };
            this.Price = new Dictionary<Security, decimal>
            {
                { security, price },
            };
            this.Quantity = new Dictionary<Security, decimal>
            {
                { security, quantity },
            };
            this.AssociatedCosts = new decimal[] { associatedCosts };
            this.Gross = new Dictionary<Security, decimal>();
            this.GainLoss = new Dictionary<Security, decimal>{
                { security, gainLoss }
            };
            this.Holdings = new Dictionary<Security, decimal>
            {
                { security, holdings }
            };
            this.Section104 = new Dictionary<Security, decimal>
            {
                { security, section104 }
            };
        }

        public ReportEntry(int id, String function, DateOnly date, Security security, decimal quantity, decimal gross, decimal gainLoss, decimal holdings, decimal section104)
        {
            this.Id = id;
            this.Function = function;
            this.Date = date;
            this.Security = new Security[] { security };
            this.Price = null;
            this.Quantity = new Dictionary<Security, decimal>
            {
                { security, quantity }
            };
            this.AssociatedCosts = null;
            this.Gross = new Dictionary<Security, decimal>
            {
                {security, gross }
            };
            this.GainLoss = this.GainLoss = new Dictionary<Security, decimal>
            {
                {security, gainLoss }
            };
            this.Holdings = new Dictionary<Security, decimal>
            {
                {security, holdings}
            };
            this.Section104 = new Dictionary<Security, decimal>
            {
                {security, section104 }
            };
        }

        public ReportEntry(int id, String function, DateOnly date, Security[] securities, decimal[]? prices, decimal[] quantities, decimal[]? associatedCosts, decimal[] gainLoss, decimal[] holdings, decimal[] section104s)
        {
            this.Id = id;
            this.Function = function;
            this.Date = date;
            this.Security = new Security[securities.Length];
            for (int i = 0; i < securities.Length; i++)
            {
                this.Security[i] = securities[i];
            }

            this.Price = new Dictionary<Security, decimal>();
            if (prices != null)
            {
                for (int i = 0; i < prices.Length; i++)
                {
                    Price.Add(securities[i], prices[i]);
                }
            }

            this.Quantity = new Dictionary<Security, decimal>();

            if (associatedCosts != null)
            {
                this.AssociatedCosts = new decimal[associatedCosts.Length];
                for (int i = 0; i < associatedCosts.Length; i++)
                {
                    this.AssociatedCosts[i] = associatedCosts[i];
                }
            }
            else
            {
                this.AssociatedCosts = null;
            }

            this.Gross = null;
            this.GainLoss = new Dictionary<Security, decimal>();
            this.Holdings = new Dictionary<Security, decimal>();
            this.Section104 = new Dictionary<Security, decimal>();
            for (int i = 0; i < securities.Length; i++)
            {

                this.Quantity.Add(securities[i], quantities[i]);
                this.GainLoss.Add(securities[i], gainLoss[i]);
                this.Holdings.Add(securities[i], holdings[i]);
                this.Section104.Add(securities[i], section104s[i]);
            }
        }

        public String PrintFunction()
        {
            return Function;
        }

        public String PrintDate()
        {
            return Date.ToString();
        }

        public String PrintSecurity()
        {
            string strSecurity = "";
            if (Security.Length > 1)
            {
                List<String> shortNames = new List<String>();
                foreach (var security in Security)
                {
                    shortNames.Add(security.ShortName);
                }
                strSecurity += String.Join(", ", shortNames.ToArray());
            }
            else
            {
                strSecurity = Security[0].ShortName;
            }
            return strSecurity;
        }

        public String PrintQuantity()
        {
            string strQuantity = "";
            if (Quantity.Count > 1)
            {
                List<String> qtys = new List<string>();
                foreach (var k in Quantity.Keys)
                {
                    qtys.Add(k.ShortName + " : " + Quantity[k].ToString());
                }
                strQuantity += String.Join(", ", qtys.ToArray());
            }
            else if (Quantity.Count == 1)
            {
                foreach (var k in Quantity.Keys)
                {
                    strQuantity = Quantity[k].ToString();
                }
            }
            return strQuantity;
        }

        public String PrintPrice()
        {
            string strPrice = "";
            if (Price != null)
            {
                if (Price.Count > 1)
                {
                    List<String> prices = new List<string>();
                    foreach (var k in Price.Keys)
                    {
                        prices.Add(k.ShortName + " : £" + Price[k].ToString());

                    }
                    strPrice += String.Join(", ", prices.ToArray());
                }
                else if (Price.Count == 1)
                {
                    foreach (var k in Price.Keys)
                    {
                        strPrice = "£" + Price[k].ToString();
                    }
                }
            }
            return strPrice;
        }

        public String PrintCosts()
        {
            string strCosts = "";
            if (AssociatedCosts != null)
            {
                if (AssociatedCosts.Length > 1)
                {
                    List<String> costs = new List<String>();
                    foreach (var cost in AssociatedCosts)
                    {
                        costs.Add("£" + cost.ToString());
                    }
                    strCosts += String.Join(", ", costs.ToArray());
                }
                else if (AssociatedCosts.Length == 1)
                {
                    strCosts = "£" + AssociatedCosts[0].ToString();
                }
            }
            return strCosts;
        }

        public String PrintGross()
        {
            string strGross = "";
            if (Gross != null)
            {
                if (Gross.Count > 1)
                {
                    List<String> grosses = new List<string>();
                    foreach (var k in Gross.Keys)
                    {
                        grosses.Add(k.ShortName + " : " + Gross[k].ToString());

                    }
                    strGross += String.Join(", ", grosses.ToArray());
                }
                else if (Gross.Count == 1)
                {
                    foreach (var k in Gross.Keys)
                    {
                        strGross = Gross[k].ToString();
                    }
                }
            }
            return strGross;
        }

        public String PrintGainLoss()
        {
            string strGainLoss = "";
            if (GainLoss.Count > 1)
            {
                List<String> gainLosses = new List<string>();
                foreach (var k in GainLoss.Keys)
                {
                    gainLosses.Add(k.ShortName + " : " + GainLoss[k].ToString());

                }
                strGainLoss += String.Join(", ", gainLosses.ToArray());
            }
            else
            {
                foreach (var k in GainLoss.Keys)
                {
                    strGainLoss = GainLoss[k].ToString();
                }
            }
            return strGainLoss;
        }

        public String PrintHoldings()
        {
            string strHoldings = "";
            if (Holdings.Count > 1)
            {
                List<String> holdings = new List<string>();
                foreach (var k in Holdings.Keys)
                {
                    holdings.Add(k.ShortName + " : " + Holdings[k].ToString());
                }
                strHoldings += String.Join(", ", holdings.ToArray());
            }
            else
            {
                foreach (var k in Holdings.Keys)
                {
                    strHoldings = Holdings[k].ToString();
                }
            }
            return strHoldings;
        }

        public String PrintSection104()
        {
            string strS104 = "";
            if (Section104.Count > 1)
            {
                List<String> s104s = new List<String>();
                foreach (var k in Section104.Keys)
                {
                    s104s.Add(k.ShortName + " : £" + String.Format("{0:n2}", Section104[k]));
                }
                strS104 += String.Join(", ", s104s.ToArray());
            }
            else
            {
                foreach (var k in Section104.Keys)
                {
                    strS104 = "£" + String.Format("{0:n2}", Section104[k]);
                }
            }
            return strS104;
        }
    }
}
