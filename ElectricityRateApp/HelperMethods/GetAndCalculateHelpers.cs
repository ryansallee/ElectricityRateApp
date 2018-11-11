using ElectricityRateApp.Data;
using ElectricityRateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.HelperMethods
{
    // Overloaded methods to help the Get, Calculate, and Compare methods of the RateComparsionResult,
    // ResidentialChargeResult, and UtilitySearch models. These methods help prevent long method smells.
    public static class GetAndCalculateHelpers
    {
        //Overloaded method to help prompt and obtain the necessary input for a RateComparison Result,
        //ResidentialChargeResult, and Utility Search Result.
        //Static Polymorphism
        public static UtilitySearchResult GetInput(UtilitySearchResult utilitySearch)
        {
            Console.WriteLine("Please provide the name of the city for which you would like to find the electric utility proivder.");
            utilitySearch.City = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            utilitySearch.StateAbbreviation = Console.ReadLine().ToUpper();
            return utilitySearch;
        }

        public static ResidentialChargeResult GetInput(ResidentialChargeResult chargeResult, out int usage)
        {
            Console.WriteLine("Please provide the name of the city for which you would like estimate your usage-based electric charges.");
            chargeResult.City = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            chargeResult.StateAbbreviation = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the usage in kilowatt hours(kWh). Most often your utility bill will have this information");
            int.TryParse(Console.ReadLine(), out usage);
            return chargeResult;
        }

        public static RateComparisonResult GetInput(RateComparisonResult rateComparison)
        {
            Console.WriteLine("Please provide the name of the first city to compare rates between cities.");
            rateComparison.City1 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            rateComparison.StateAbbreviation1 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the name of the second city.");
            rateComparison.City2 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            rateComparison.StateAbbreviation2 = Console.ReadLine().ToUpper();
            return rateComparison;
        }

        // Overloaded method to check the inputs obtained in the GetInput method. If the input is
        // not valid, the method returns false so that its parent method returns and executes no further.
        // Static Polymorphism
        public static bool CheckValidInput(string city, string stateAbbreviation)
        {
            bool inputValid = true;
            if (string.IsNullOrEmpty(city))
            {
                Console.WriteLine("A city was not provided. Please try again.");
                inputValid = false;
            }
            if(string.IsNullOrEmpty(stateAbbreviation))
            {
                Console.WriteLine("A state abbreviation was not provided. Please try again.");
                inputValid = false;
            }
            if (stateAbbreviation.Length != 2 && stateAbbreviation.Length > 0)
            {
                Console.WriteLine("A valid state abbreviation is 2 letters long. Please try again.");
                inputValid = false;
            }
            return inputValid;
        }

        public static bool CheckValidInput(string city, string stateAbbreviation, int usage)
        {
            bool inputValid = true;
            if (string.IsNullOrEmpty(city))
            {
                Console.WriteLine("A city was not provided. Please try again.");
                inputValid = false;
            }
            if (string.IsNullOrEmpty(stateAbbreviation))
            {
                Console.WriteLine("A state abbreviation was not provided. Please try again.");
                inputValid = false;
            }
            if (stateAbbreviation.Length != 2 && stateAbbreviation.Length > 0)
            {
                Console.WriteLine("A valid state abbreviation is 2 letters long. Please try again.");
                inputValid = false;
            }
            if(usage == 0)
            {
                Console.WriteLine("The usage was not provided or invalid. Estimated charges cannot be provided. Please try again");
                inputValid = false;
            }
            return inputValid;
        }

        public static bool CheckValidInput(string city1, string stateAbbreviation1, string city2, string stateAbbreviation2)
        {
            bool inputValid = true;
            if (string.IsNullOrEmpty(city1))
            {
                Console.WriteLine("The first city was not provided. Please try again.");
                inputValid = false;
            }
            if (string.IsNullOrEmpty(stateAbbreviation1))
            {
                Console.WriteLine("The first state abbreviation was not provided. Please try again.");
                inputValid = false;
            }
            if (stateAbbreviation1.Length != 2 && stateAbbreviation1.Length > 0)
            {
                Console.WriteLine("A valid state abbreviation was not proivded for the first state. A valid state abbreviation is 2 letters long. Please try again.");
                inputValid = false;
            }
            if (string.IsNullOrEmpty(city2))
            {
                Console.WriteLine("The second city was not provided. Please try again.");
                inputValid = false;
            }
            if (string.IsNullOrEmpty(stateAbbreviation2))
            {
                Console.WriteLine("The second state abbreviation was not provided. Please try again.");
                inputValid = false;
            }
            if (stateAbbreviation2.Length != 2 && stateAbbreviation2.Length > 0)
            {
                Console.WriteLine("A valid state abbreviation was not proivded for the second state. A valid state abbreviation is 2 letters long. Please try again.");
                inputValid = false;
            }
            return inputValid;
        }

        // Overloaded method that returns false if GetZipCode returns null so that its parent method
        // does not execute any further code.
        // Static Polymorphism
        public static bool DoesCityExist(string zipCode, string city, string stateAbbreviation)
        {
            if(string.IsNullOrEmpty(zipCode))
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US", city, stateAbbreviation));
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool DoesCityExist(string zipCode1, string city1, string stateAbbreviation1, string zipCode2, string city2, string stateAbbreviation2)
        {
            bool cityExists = true;
            if(string.IsNullOrEmpty(zipCode1))
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US. Unfortunately, we cannot proceed with comparing rates. Please try again.", city1, stateAbbreviation1));
                cityExists = false;
            }
            if(string.IsNullOrEmpty(zipCode2))
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US. Unfortunately, we cannot proceed with comparing rates. Please try again.", city2, stateAbbreviation2));
                cityExists = false;
            }
            return cityExists;
        }

        // Overloaded method that returns false if the Rate is 0 and prevents its parent method from executing
        // further code.
        // Static Polymorphism.
        public static bool CheckIfRateIs0(ResidentialChargeResult chargeResult)
        {
            if(chargeResult.Rate == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", chargeResult.City, chargeResult.StateAbbreviation));
                SaveSearchResults.Save(chargeResult);
                return false;
            }
            return true;
        }

        public static bool CheckIfRateIs0(RateComparisonResult rateComparison)
        {
            if(rateComparison.Rate1 == 0 && rateComparison.Rate2 == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} and {2}, {3}. " +
                  "We cannot calculate any rates.", 
                  rateComparison.City1, rateComparison.StateAbbreviation1, rateComparison.City2, rateComparison.StateAbbreviation2));
                return false;
            }
            else if(rateComparison.Rate1 == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} " +
                   "and cannot compare the rates of {0}, {1} with {2}, {3}.", 
                   rateComparison.City1, rateComparison.StateAbbreviation1, rateComparison.City2, rateComparison.StateAbbreviation2));
                return false;
            }
            else if (rateComparison.Rate2 == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} " +
                    "and cannot compare the rates of {0}, {1} with {2}, {3}.",
                    rateComparison.City2, rateComparison.StateAbbreviation2, rateComparison.City1, rateComparison.StateAbbreviation1));
                return false;
            }
            return true;
        }


    }
}
