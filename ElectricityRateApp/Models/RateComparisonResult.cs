using ConsoleTables;
using ElectricityRateApp.Data;
using System;
using System.Linq;

namespace ElectricityRateApp.Models
{
    //Class to model a RateComparsionResult.
    
    public class RateComparisonResult
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string City1 { get; set; }
        public string StateAbbreviation1 { get; set; }
        public double Rate1 { get; set; }
        public double Difference { get; set; }
        public string City2 { get; set; }
        public string StateAbbreviation2 { get; set; }
        public double Rate2 { get; set; }


    }
}
