using System;

namespace ElectricityRateApp.Models
{
    //Class to model a ResidentialChargeResult-an estimation of usage-electricity charges.
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
