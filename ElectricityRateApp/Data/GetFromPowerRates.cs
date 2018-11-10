using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.Data
{
    //Methods to read from the PowerRates table by using LINQ queries.
    //These methods prevent Long Method code smells for the Get methods in the RateCommparisonResult
    //ResidentialChargeResult, and UtilitySearchResult Get methods.
    public static class GetFromPowerRates
    {
        public static double GetRate(string zipCode)
        {
            using (var context = new ElectricityRatesContext())
            {
                return context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                            .Select(pr => pr.ResidentialRate)
                            .DefaultIfEmpty(0)
                            .Sum();
            }
        }

        public static string GetUtilityProviderName(string zipCode)
        {
            using (var context = new ElectricityRatesContext())
            {
                return context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                                       .Select(pr => pr.UtilityName)
                                       .FirstOrDefault();
            }
        }
    }
    
}
