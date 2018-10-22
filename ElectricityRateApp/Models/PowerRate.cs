using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.Models
{
    public class PowerRate
    {
        public PowerRate(string zipCode, string utilityName, double commericalRate, double residentialRate)
        {
            ZipCode = zipCode;
            UtilityName = utilityName;
            CommercialRate = commericalRate;
            ResidentialRate = residentialRate;
        }
        public int Id { get; set; }
        public string ZipCode { get; private set; }
        public string UtilityName { get; private set; }
        public double CommercialRate { get; private set; }
        public double ResidentialRate { get; private set; }
    }
}
