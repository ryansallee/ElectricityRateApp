using ElectricityRateApp.Models;
using System;
using ElectricityRateApp.Data;

namespace ElectricityRateApp.Data
{
    public static class SaveSearchResults
    {
        public static void SaveProviderResult(UtilitySearchResult searchResult)
        {
            searchResult.Time = DateTime.Now.ToString("MM/dd/yy HH:mm");
            using (var context = new ElectricityRatesContext())
            {
                context.ProviderSearchResults.Add(searchResult);
                context.SaveChanges();
            }
        }

        public static void SaveRateCalculation(ResidentialChargeResult searchResult)
        {
            searchResult.Time = DateTime.Now.ToString("MM/dd/yy HH:mm");
            using (var context = new ElectricityRatesContext())
            {
                context.ResidentialChargeResults.Add(searchResult);
                context.SaveChanges();
            }
        }

        public static void SaveRateComparison(RateComparisonResult searchResult)
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
