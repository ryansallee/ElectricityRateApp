using System;
using System.Linq;

using ElectricityRateApp.Data;
using ConsoleTables;

namespace ElectricityRateApp.Models
{
    //Class to model a UtilitySearchResult.
    //Inherits and implements AbstractResult<T>.
    public class UtilitySearchResult : Result<UtilitySearchResult>
    {
        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public string UtilityName { get; set; }

        // Method using the methods of GetAndCalculateHelpers and implementations of the members of AbstractResult<T>
        // to instantiate a UtilitySearchResult, populates its properties(the name of the electric utility provider for a city),  
        // and persist that instance of a UtilitySearchResult to the database.
        public void Get(UtilitySearchResult utilitySearch)
        {
            try
            {
                utilitySearch.GetInput(utilitySearch);
                if (!utilitySearch.CheckValidInput(utilitySearch))
                    return;
                                
                string zipCode = ZipCodes.GetZipCode(utilitySearch.City, utilitySearch.StateAbbreviation).Result;

                if (!DoesCityExist(zipCode, utilitySearch.City, utilitySearch.StateAbbreviation))
                    return;

                utilitySearch.UtilityName = GetUtilityProviderName(zipCode);

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

        //Implementation of abstract method.
        protected override UtilitySearchResult GetInput(UtilitySearchResult utilitySearch)
        {
            Console.WriteLine("Please provide the name of the city for which you would like to find the electric utility proivder.");
            utilitySearch.City = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            utilitySearch.StateAbbreviation = Console.ReadLine().ToUpper();
            return utilitySearch;
        }

        //Implementation of abstract method.
        protected override bool CheckValidInput(UtilitySearchResult utilitySearch)
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

        //Implementation of abstract method.
        protected override void Save(UtilitySearchResult utilitySearch)
        {
            utilitySearch.Time = DateTime.Now;
            using (var context = new ElectricityRatesContext())
            {
                context.UtilitySearchResults.Add(utilitySearch);
                context.SaveChanges();
            }
        }

        //Helper method to get the name of the Utility provider.
        private static string GetUtilityProviderName(string zipCode)
        {
            using (var context = new ElectricityRatesContext())
            {
                return context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                                       .Select(pr => pr.UtilityName)
                                       .FirstOrDefault();
            }
        }
    }
}
