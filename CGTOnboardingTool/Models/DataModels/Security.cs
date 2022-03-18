using System;

namespace CGTOnboardingTool.Models.DataModels
{
    public class Security
    {
        public String ShortName { get; init; }
        public String Name { get; init; }

        /// <summary>
        /// Security constructor
        /// </summary>
        /// <param name="shortName"></param>
        /// <param name="name"></param>
        public Security(string shortName, String name)
        {
            this.ShortName = shortName;
            this.Name = name;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", this.ShortName, this.Name);
        }


        public override bool Equals(object? obj)
        {
            return obj is Security security &&
                   Name == security.Name &&
                   ShortName == security.ShortName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ShortName);
        }
    }
}
