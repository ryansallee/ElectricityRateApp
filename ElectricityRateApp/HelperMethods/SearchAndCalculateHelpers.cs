using ElectricityRateApp.Data;
using ElectricityRateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.HelperMethods
{
    public static class SearchAndCalculateHelpers
    {
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
