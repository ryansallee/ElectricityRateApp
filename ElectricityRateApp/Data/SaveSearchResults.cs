using ElectricityRateApp.Models;
using System;
using ElectricityRateApp.Data;

namespace ElectricityRateApp.Data
{
    public static class SaveSearchResults
    {
        public static void SaveProviderResult(string city, string stateAbbreviation, string provider)
        {
            ProviderSearchResult searchResult = new ProviderSearchResult()
            {
                City = city,
                StateAbbreviation = stateAbbreviation,
                ProviderName = provider,
                Time = DateTime.Now.ToString(),
            };
            using (var context = new ElectricityRatesContext())
            {
                context.ProviderSearchResults.Add(searchResult);
                context.SaveChanges();
            }

        }

        public static void SaveRateCalculation(string city, string stateAbbreviation, double rate, double charge, int usage)
        {
            ResidentialChargeResult searchResult = new ResidentialChargeResult()
            {
                City = city,
                StateAbbreviation = stateAbbreviation,
                Time = DateTime.Now.ToString(),
                Rate = rate,
                Charge = charge,
                Usage = usage
            };
            using (var context = new ElectricityRatesContext())
            {
                context.ResidentialChargeResults.Add(searchResult);
                context.SaveChanges();
            }
        }
    }
}
