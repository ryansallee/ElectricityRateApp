using ConsoleTables;
using ElectricityRateApp.Data;
using ElectricityRateApp.HelperMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.Models
{
    public class RateComparisonResult
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string City1 { get; set; }
        public string StateAbbreviation1 { get; set; }
        public double Rate1 { get; set; }
        public double Difference { get; set; }
        public string City2 {get; set;}
        public string StateAbbreviation2 { get; set; }
        public double Rate2 { get; set; }

        public static void Compare()
        {
            RateComparisonResult rateComparison = new RateComparisonResult();
            Console.WriteLine("Please provide the name of the first city to compare rates between cities");
            rateComparison.City1 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            rateComparison.StateAbbreviation1 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the name of the second city.");
            rateComparison.City2 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            rateComparison.StateAbbreviation2 = Console.ReadLine().ToUpper();
            if (!SearchAndCalculateHelpers.CheckValidInput(rateComparison.City1, rateComparison.StateAbbreviation1, rateComparison.City2, rateComparison.StateAbbreviation2))
                return;

            string zipCode1 = ZipCodeMethods.GetZipCode(rateComparison.City1, rateComparison.StateAbbreviation1).Result;
            string zipCode2 = ZipCodeMethods.GetZipCode(rateComparison.City2, rateComparison.StateAbbreviation2).Result;

            if (!SearchAndCalculateHelpers.DoesCityExist(zipCode1, rateComparison.City1, rateComparison.StateAbbreviation1,
                    zipCode2, rateComparison.City2, rateComparison.StateAbbreviation2))
                return;

            rateComparison.Rate1 = GetFromPowerRates.GetRate(zipCode1);
            rateComparison.Rate2 = GetFromPowerRates.GetRate(zipCode2);

            if (!SearchAndCalculateHelpers.CheckIfRateIs0(rateComparison))
                return;

            rateComparison.Difference = (rateComparison.Rate1 - rateComparison.Rate2) / rateComparison.Rate2;

            if (rateComparison.Rate1 > rateComparison.Rate2)
            {
                Console.WriteLine(String.Format("The rate in {0}, {1} is {2:P2} more than in {3}, {4}.",
                    rateComparison.City1, rateComparison.StateAbbreviation1, Math.Abs(rateComparison.Difference),
                    rateComparison.City2, rateComparison.StateAbbreviation2));
            }
            else if (rateComparison.Rate2 > rateComparison.Rate1)
            {
                Console.WriteLine(String.Format("The rate in  {0}, {1} is {2:P2} less than in {3}, {4}.",
                    rateComparison.City1, rateComparison.StateAbbreviation1, Math.Abs(rateComparison.Difference),
                    rateComparison.City2, rateComparison.StateAbbreviation2));
            }
            SaveSearchResults.Save(rateComparison);
        }

        public static void GetHistory()
        {
            if (!SearchResultsHelper.NumberOfResults(out int numberOfResults, "rate comparisons"))
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
                        result.City2, result.StateAbbreviation2, string.Format("{0:C}", result.Rate2), string.Format("{0:P2}", result.Difference));
                }
            }
            table.Write();
            Console.WriteLine();
        }
    }
}
