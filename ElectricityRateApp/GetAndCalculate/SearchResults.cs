using System;
using ElectricityRateApp.Data;
using ElectricityRateApp.Models;
using System.Linq;
using System.Collections.Generic;

namespace ElectricityRateApp.GetAndCalculate
{
    public class SearchResults
    {
        public static void GetProviderSearchResults()
        {
            int numberOfResults;
            Console.WriteLine("How many of the most recent electric utility provider search results would you like?");
            bool success = int.TryParse(Console.ReadLine(), out numberOfResults);
            if(!success)
            {
                Console.WriteLine("Please provide a valid number. Please try again.");
                return;
            }
            Console.WriteLine(string.Format("Here are the last {0} results", numberOfResults));

            using (var context = new ElectricityRatesContext())
            {
                var results = context.ProviderSearchResults.OrderByDescending(r => r.Id)
                    .Take(numberOfResults);

                Console.WriteLine("Time \t \t City \t \t State \t Provider");
                foreach (var result in results)
                {
                    Console.WriteLine(string.Format("{0} \t {1} \t {2} \t {3}", result.Time,
                        result.City, result.StateAbbreviation, result.ProviderName));
                }
            }
        }
    }
}
