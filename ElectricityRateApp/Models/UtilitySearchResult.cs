using System;
using System.Linq;

using ElectricityRateApp.Data;
using ConsoleTables;

namespace ElectricityRateApp.Models
{
    //Class to model a UtilitySearchResult.
    //Inherits and implements Result<T>.
    public class UtilitySearchResult
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public string UtilityName { get; set; }
    }
}
 

    
