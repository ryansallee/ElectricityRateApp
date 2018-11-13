using ElectricityRateApp.Models;
using System;

namespace ElectricityRateApp.Data
{
    public static class SaveSearchResults
    {
        //Overloaded Method to persist RateCommparisonResult ResidentialChargeResult, and UtilitySearchResults
        //Static polymorphism
        public static void Save(UtilitySearchResult searchResult)
        {
            searchResult.Time = DateTime.Now;
            using (var context = new ElectricityRatesContext())
            {
                context.UtilitySearchResults.Add(searchResult);
                context.SaveChanges();
            }
        }

        public static void Save(ResidentialChargeResult searchResult)
        {
            searchResult.Time = DateTime.Now;
            using (var context = new ElectricityRatesContext())
            {
                context.ResidentialChargeResults.Add(searchResult);
                context.SaveChanges();
            }
        }

        public static void Save(RateComparisonResult searchResult)
        {
            searchResult.Time = DateTime.Now;
            using (var context = new ElectricityRatesContext())
            {
                context.RateComparisonResults.Add(searchResult);
                context.SaveChanges();
            }
        }


    }
}
