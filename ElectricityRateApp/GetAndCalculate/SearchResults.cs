using System;
using ElectricityRateApp.Data;
using ElectricityRateApp.Models;
using System.Linq;
using System.Collections.Generic;

namespace ElectricityRateApp.GetAndCalculate
{
    public class SearchResults
    {
        public static void GetProviderSearchHistory()
        {
            Console.WriteLine("How many of the most recent electric utility provider search results would you like?");
            bool success = int.TryParse(Console.ReadLine(), out int numberOfResults);
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
                    if (result.City.Length <= 5)
                    {
                        Console.WriteLine(string.Format("{0} \t {1} \t\t {2} \t {3}", result.Time,
                        result.City, result.StateAbbreviation, result.ProviderName));
                    }
                    else
                    {
                        Console.WriteLine(string.Format("{0} \t {1} \t {2} \t {3}", result.Time,
                            result.City, result.StateAbbreviation, result.ProviderName));
                    }
                }
            }
        }

        public static void GetChargeCalcuationHistory()
        {
            Console.WriteLine("How many of the most recent electric residential charge calculations would you like?");
            bool success = int.TryParse(Console.ReadLine(), out int numberOfResults);
            if (!success)
            {
                Console.WriteLine("Please provide a valid number. Please try again.");
                return;
            }

            Console.WriteLine(string.Format("Here are the last {0} results:", numberOfResults));
            using (var context = new ElectricityRatesContext())
            {
                var results = context.ResidentialChargeResults.OrderByDescending(r => r.Id)
                    .Take(numberOfResults);
                Console.WriteLine("Time \t \t City \t \t State \t Rate \t Charge\t Usage(kwh)");
                foreach (var result in results)
                {
                    if (result.City.Length <= 5)
                    {
                        Console.WriteLine(string.Format("{0} \t {1} \t\t {2} \t {3:C} \t{4:C} \t {5}",
                        result.Time, result.City, result.StateAbbreviation, result.Rate, result.Charge, result.Usage));
                    }
                    else
                    {
                        Console.WriteLine(string.Format("{0} \t {1} \t {2} \t {3:C} \t{4:C} \t {5}",
                            result.Time, result.City, result.StateAbbreviation, result.Rate, result.Charge, result.Usage));
                    }
                }
            }
        }

        public static void GetRateComparisonHistory()
        {
            Console.WriteLine("How many of the most recent rate comparisons would you like?");
            bool success = int.TryParse(Console.ReadLine(), out int numberOfResults);
            if(!success)
            {
                Console.WriteLine("Please provide a valid number. Please try again");
                return;
            }

            Console.WriteLine(string.Format("Here are the last {0} results:", numberOfResults));
            using (var context = new ElectricityRatesContext())
            {
                var results = context.RateComparisonResults.OrderByDescending(r => r.Id)
                    .Take(numberOfResults);

                Console.WriteLine();
                foreach (var result in results)
                {
                    Console.WriteLine(string.Format("{0} \t {1} \t\t {2} \t {3:C} \t{4} \t {5} \t{6:C} \t {7:P2}",
                        result.Time, result.City1, result.StateAbbreviation1, result.Rate1,
                        result.City2, result.StateAbbreviation2, result.Rate2, result.Difference));
                }
            }
        }
    }
}
