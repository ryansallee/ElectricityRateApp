using System;

namespace ElectricityRateApp.Models
{
    //Class to model a UtilitySearchResult-obtaining the name of an electric uitlity provider.
    public class UtilitySearchResult
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public string UtilityName { get; set; }
    }
}
 

    
