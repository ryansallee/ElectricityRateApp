using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricityRateApp.HelperMethods;
using ElectricityRateApp.Data;
using ConsoleTables;

namespace ElectricityRateApp.Models
{
    public class UtilitySearchResult
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public string UtilityName { get; set; }

        public static void Get()
        {
            UtilitySearchResult utilitySearch = new UtilitySearchResult();
            Console.WriteLine("Please provide the name of the city for which you would like to find the electric utility proivder.");
            utilitySearch.City = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            utilitySearch.StateAbbreviation = Console.ReadLine().ToUpper();
            if (!SearchAndCalculateHelpers.CheckValidInput(utilitySearch.City, utilitySearch.StateAbbreviation))
                return;

            string zipCode = ZipCodeMethods.GetZipCode(utilitySearch.City, utilitySearch.StateAbbreviation).Result;
            if (!SearchAndCalculateHelpers.DoesCityExist(zipCode, utilitySearch.City, utilitySearch.StateAbbreviation))
                return;

            utilitySearch.UtilityName = GetFromPowerRates.GetUtilityProviderName(zipCode);

            //TODO Check if provider is null method?
            if (utilitySearch.UtilityName == null)
            {
                Console.WriteLine("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", utilitySearch.City, utilitySearch.StateAbbreviation);
                utilitySearch.UtilityName = "No Provider Info Found";
                SaveSearchResults.Save(utilitySearch);
            }
            else
            {
                Console.WriteLine(string.Format("The electric utility provider in {0}, {1} is {2}.", utilitySearch.City, utilitySearch.StateAbbreviation, utilitySearch.UtilityName));
                SaveSearchResults.Save(utilitySearch);
            }
        }

        public static void GetHistory()
        {
            if (!SearchResultsHelper.NumberOfResults(out int numberOfResults, "utility provider searches"))
                return;
            Console.WriteLine(string.Format("Here are the last {0} results", numberOfResults));
            var table = new ConsoleTable("Time", "City", "State", "Provider");
            using (var context = new ElectricityRatesContext())
            {
                var results = context.ProviderSearchResults.OrderByDescending(r => r.Id)
                    .Take(numberOfResults);

                foreach (var result in results)
                {
                    table.AddRow(result.Time, result.City, result.StateAbbreviation, result.UtilityName);
                }
            }
            table.Write();
            Console.WriteLine();
        }
    }
}
