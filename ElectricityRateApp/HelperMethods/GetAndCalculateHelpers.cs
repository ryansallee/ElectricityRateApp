using ElectricityRateApp.Data;
using ElectricityRateApp.Models;
using System;

namespace ElectricityRateApp.HelperMethods
{
    // Overloaded methods to help the Get, Calculate, and Compare methods of the RateComparsionResult,
    // ResidentialChargeResult, and UtilitySearch models. These methods help prevent long method smells.
    public static class GetAndCalculateHelpers
    {

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
                chargeResult.Save(chargeResult);
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
