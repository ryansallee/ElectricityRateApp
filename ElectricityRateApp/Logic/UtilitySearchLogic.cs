using ConsoleTables;
using ElectricityRateApp.Data;
using ElectricityRateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.Logic
{
    class UtilitySearchLogic:Result<UtilitySearchResult>
    {
        private ZipCodeLogic _zipCodeLogic = new ZipCodeLogic();

        // Method using implementations of the members of Result<T> to populate the properties
        // of a UtilitySearchResult (the name of the electric utility provider for a city),  
        // and persist that instance of a UtilitySearchResult to the database.
        public void PopulateAndDisplayResult(UtilitySearchResult utilitySearch, UtilitySearchLogic logic)
        {
            try
            {
                logic.GetInput(utilitySearch);
                if (!logic.CheckValidInput(utilitySearch))
                    return;

                string zipCode = _zipCodeLogic.GetZipCode(utilitySearch.City, utilitySearch.StateAbbreviation).Result;

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
                logic.Save(utilitySearch);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }

        // Method to get a user-specified length of IQueryable<UtilitySearchResult> and displays them 
        // to the console using the ConsoleTables NuGet extension.
        public void GetHistory()
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

        //Implementation of the GetInput abstract method.
        protected override UtilitySearchResult GetInput(UtilitySearchResult utilitySearch)
        {
            Console.WriteLine("Please provide the name of the city for which you would like to find the electric utility proivder.");
            utilitySearch.City = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            utilitySearch.StateAbbreviation = Console.ReadLine().ToUpper();
            return utilitySearch;
        }

        //Implementation of the CheckValidInput abstract method.
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

        //Implementation of the Save abstract method.
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

