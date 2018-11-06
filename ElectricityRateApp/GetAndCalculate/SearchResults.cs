using System;
using ElectricityRateApp.Data;
using ElectricityRateApp.Models;
using System.Linq;
using System.Collections.Generic;
using ConsoleTables;

namespace ElectricityRateApp.GetAndCalculate
{
    public class SearchResults
    {
        public static void GetProviderSearchHistory()
        {
            if(!SearchResultsHelper.NumberOfResults(out int numberOfResults))
                return;
            Console.WriteLine(string.Format("Here are the last {0} results", numberOfResults));
            var table = new ConsoleTable("Time", "City", "State", "Provider");
            using (var context = new ElectricityRatesContext())
            {
                var results = context.ProviderSearchResults.OrderByDescending(r => r.Id)
                    .Take(numberOfResults);                             
                
                foreach (var result in results)
                {      
                    table.AddRow(result.Time, result.City, result.StateAbbreviation, result.ProviderName);
                }                
            }
            table.Write();
            Console.WriteLine();
        }

        public static void GetChargeCalcuationHistory()
        {
            if (!SearchResultsHelper.NumberOfResults(out int numberOfResults))
                return;
            Console.WriteLine(string.Format("Here are the last {0} results:", numberOfResults));
            var table = new ConsoleTable("Time", "City", "State", "Rate", "Charge", "Usage(kWh)");
            using (var context = new ElectricityRatesContext())
            {
                var results = context.ResidentialChargeResults.OrderByDescending(r => r.Id)
                    .Take(numberOfResults);
                                
                foreach (var result in results)
                {
                    table.AddRow(result.Time, result.City, result.StateAbbreviation, 
                        string.Format("{0:C}",result.Rate), string.Format("{0:C}", result.Charge), result.Usage);
                }                
            }
            table.Write();
            Console.WriteLine();
        }

        public static void GetRateComparisonHistory()
        {
            if (!SearchResultsHelper.NumberOfResults(out int numberOfResults))
                return;
            Console.WriteLine(string.Format("Here are the last {0} results:", numberOfResults));
            Console.WriteLine("A negative percentage in the Difference Column means the first city's rate is less.");
            var table = new ConsoleTable("Time", "City 1", "State 1", "Rate 1", "City 2", "State 2", "Rate 2", "Difference");
            using (var context = new ElectricityRatesContext())
            {
                var results = context.RateComparisonResults.OrderByDescending(r => r.Id)
                    .Take(numberOfResults);

                foreach (var result in results)
                {
                    table.AddRow(result.Time, result.City1, result.StateAbbreviation1, string.Format("{0:C}", result.Rate1),
                        result.City2, result.StateAbbreviation2, string.Format("{0:C}", result.Rate2), string.Format("{0:P2}",result.Difference));
                }
            }
            table.Write();
            Console.WriteLine();
        }
    }
}
