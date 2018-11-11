using ConsoleTables;
using ElectricityRateApp.Data;
using ElectricityRateApp.HelperMethods;
using System;
using System.Linq;

namespace ElectricityRateApp.Models
{
    //Class to model a RateComparsionResult.
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

        // Method using the methods of GetAndCalculateHelpers class to instantiate a RateComparsionResult,
        // populate its properties(a comparison of rates between two cities), and persist that instance 
        // of a RateComparsionResult to the database.
        public static void Compare()
        {
            RateComparisonResult rateComparison = new RateComparisonResult();
            GetAndCalculateHelpers.GetInput(rateComparison);
            if (!GetAndCalculateHelpers.CheckValidInput(rateComparison.City1, rateComparison.StateAbbreviation1, rateComparison.City2, rateComparison.StateAbbreviation2))
                return;

            string zipCode1 = ZipCode.GetZipCode(rateComparison.City1, rateComparison.StateAbbreviation1).Result;
            string zipCode2 = ZipCode.GetZipCode(rateComparison.City2, rateComparison.StateAbbreviation2).Result;

            if (!GetAndCalculateHelpers.DoesCityExist(zipCode1, rateComparison.City1, rateComparison.StateAbbreviation1,
                    zipCode2, rateComparison.City2, rateComparison.StateAbbreviation2))
                return;

            rateComparison.Rate1 = GetFromPowerRates.GetRate(zipCode1);
            rateComparison.Rate2 = GetFromPowerRates.GetRate(zipCode2);

            if (!GetAndCalculateHelpers.CheckIfRateIs0(rateComparison))
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
            else
            {
                Console.WriteLine(string.Format("The rates in {0}, {1} and {2}, {3} are the same.", rateComparison.City1, rateComparison.StateAbbreviation1,
                     rateComparison.City2, rateComparison.StateAbbreviation2));
            }
            SaveSearchResults.Save(rateComparison);
        }

        // Method to get a user-specified length IQueryable<RateComparisonResult> and displays them 
        // to the console using the ConsoleTables NuGet extension.
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
