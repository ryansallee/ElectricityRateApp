using ConsoleTables;
using ElectricityRateApp.Data;
using System;
using System.Linq;

namespace ElectricityRateApp.Models
{
    //Class to model a ResidentialChargeResult.
    public class ResidentialChargeResult
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public double Rate { get; set; }
        public double Charge { get; set; }
        public int Usage { get; set; }

  


    }
}
