﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricityRateApp.HelperMethods;
using ElectricityRateApp.Data;
using ConsoleTables;

namespace ElectricityRateApp.Models
{
    //Class to model a UtilitySearchResult
    public class UtilitySearchResult
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public string UtilityName { get; set; }

        // Method using the methods of GetAndCalculateHelpers class to instantiate a UtilitySearchResult,
        // populate its properties(the name of the electric utility provider for a city), and persist that instance 
        // of a UtilitySearchResult to the database.
        public static void Get()
        {
            UtilitySearchResult utilitySearch = new UtilitySearchResult();
            GetAndCalculateHelpers.GetInput(utilitySearch);
            if (!GetAndCalculateHelpers.CheckValidInput(utilitySearch.City, utilitySearch.StateAbbreviation))
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
            SaveSearchResults.Save(utilitySearch);
        }

        // Method to get a user-specified length of IQueryable<UtilitySearchResult> and displays them 
        // to the console using the ConsoleTables NuGet extension.
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
