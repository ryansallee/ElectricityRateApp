using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.Models
{
    public class RateComparisonResult
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string City1 { get; set; }
        public string StateAbbreviation1 { get; set; }
        public double Rate1 { get; set; }
        public double Difference { get; set; }
        public string City2 {get; set;}
        public string StateAbbreviation2 { get; set; }
        public double Rate2 { get; set; }


    }
}
