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
                Time = DateTime.Now.ToString("MM/dd/yy HH:mm")
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

        public static void SaveRateComparison(string city1, string stateAbbreivation1, double rate1, double difference,
            string city2, string stateAbbreviation2, double rate2)
        {
            RateComparisonResult searchResult = new RateComparisonResult()
            {
                Time = DateTime.Now.ToString(),
                City1 = city1,
                StateAbbreviation1 = stateAbbreivation1,
                Rate1 = rate1,
                Difference = difference,
                City2 = city2,
                StateAbbreviation2 = stateAbbreviation2,
                Rate2 = rate2
            };

            using (var context = new ElectricityRatesContext())
            {
                context.RateComparisonResults.Add(searchResult);
                context.SaveChanges();
            }
        }
    }
}
