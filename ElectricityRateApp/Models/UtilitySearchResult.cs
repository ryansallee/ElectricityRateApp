using System;
using System.Linq;
using ElectricityRateApp.HelperMethods;
using ElectricityRateApp.Data;
using ConsoleTables;

namespace ElectricityRateApp.Models
{
    //Class to model a UtilitySearchResult
    public class UtilitySearchResult : AbstractResult<UtilitySearchResult>
    {

        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public string UtilityName { get; set; }

        // Method using the methods of GetAndCalculateHelpers class to instantiate a UtilitySearchResult,
        // populate its properties(the name of the electric utility provider for a city), and persist that instance 
        // of a UtilitySearchResult to the database.
        public static void Get()
        {
            try
            {
                UtilitySearchResult utilitySearch = new UtilitySearchResult();
                utilitySearch.GetInput(utilitySearch);
                if (!utilitySearch.CheckValidInput(utilitySearch))
                    return;

                string zipCode = ZipCode.GetZipCode(utilitySearch.City, utilitySearch.StateAbbreviation).Result;

                if (!GetAndCalculateHelpers.DoesCityExist(zipCode, utilitySearch.City, utilitySearch.StateAbbreviation))
                    return;

                utilitySearch.UtilityName = GetFromPowerRates.GetUtilityProviderName(zipCode);

                if (utilitySearch.UtilityName == null)
                {
                    Console.WriteLine("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", utilitySearch.City, utilitySearch.StateAbbreviation);
                    utilitySearch.UtilityName = "No Provider Info Found";
                }
                else
                {
                    Console.WriteLine(string.Format("The electric utility provider in {0}, {1} is {2}.", utilitySearch.City, utilitySearch.StateAbbreviation, utilitySearch.UtilityName));
                }
                utilitySearch.Save(utilitySearch);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }

        // Method to get a user-specified length of IQueryable<UtilitySearchResult> and displays them 
        // to the console using the ConsoleTables NuGet extension.
        public static void GetHistory()
        {
            if (!NumberOfResults(out int numberOfResults, "utility provider searches"))
                return;

            var table = new ConsoleTable("Time", "City", "State", "Provider");
            using (var context = new ElectricityRatesContext())
            {
                var results = context.UtilitySearchResults.OrderByDescending(r => r.Id)
                    .Take(numberOfResults);

                foreach (var result in results)
                {
                    table.AddRow(result.Time.ToString(), result.City, result.StateAbbreviation, result.UtilityName);
                }
                Console.WriteLine(string.Format("Here are the last {0} result(s):", results.Count()));
            }
            table.Write();
            Console.WriteLine();
        }

        public override UtilitySearchResult GetInput(UtilitySearchResult utilitySearch)
        {
            Console.WriteLine("Please provide the name of the city for which you would like to find the electric utility proivder.");
            utilitySearch.City = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            utilitySearch.StateAbbreviation = Console.ReadLine().ToUpper();
            return utilitySearch;
        }

        public override bool CheckValidInput(UtilitySearchResult utilitySearch)
        {
            bool inputValid = true;
            if (string.IsNullOrEmpty(utilitySearch.City))
            {
                Console.WriteLine("A city was not provided. Please try again.");
                inputValid = false;
            }
            if (string.IsNullOrEmpty(utilitySearch.StateAbbreviation))
            {
                Console.WriteLine("A state abbreviation was not provided. Please try again.");
                inputValid = false;
            }
            if (utilitySearch.StateAbbreviation.Length != 2 && utilitySearch.StateAbbreviation.Length > 0)
            {
                Console.WriteLine("A valid state abbreviation is 2 letters long. Please try again.");
                inputValid = false;
            }
            return inputValid;
        }

        public override void Save(UtilitySearchResult utilitySearch)
        {
            utilitySearch.Time = DateTime.Now;
            using (var context = new ElectricityRatesContext())
            {
                context.UtilitySearchResults.Add(utilitySearch);
                context.SaveChanges();
            }
        }
    }
}
