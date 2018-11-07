using ElectricityRateApp.Models;
using System;
using ElectricityRateApp.Data;

namespace ElectricityRateApp.Data
{
    public static class SaveSearchResults
    {
        public static void Save(UtilitySearchResult searchResult)
        {
            searchResult.Time = DateTime.Now.ToString("MM/dd/yy HH:mm");
            using (var context = new ElectricityRatesContext())
            {
                context.ProviderSearchResults.Add(searchResult);
                context.SaveChanges();
            }
        }

        public static void Save(ResidentialChargeResult searchResult)
        {
            searchResult.Time = DateTime.Now.ToString("MM/dd/yy HH:mm");
            using (var context = new ElectricityRatesContext())
            {
                context.ResidentialChargeResults.Add(searchResult);
                context.SaveChanges();
            }
        }

        public static void Save(RateComparisonResult searchResult)
        {
            searchResult.Time = DateTime.Now.ToString("MM/dd/yy HH:mm");
            using (var context = new ElectricityRatesContext())
            {
                context.RateComparisonResults.Add(searchResult);
                context.SaveChanges();
            }
        }


    }
}
