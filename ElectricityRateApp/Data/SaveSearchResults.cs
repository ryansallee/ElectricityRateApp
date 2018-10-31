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
    }
}
