using ElectricityRateApp.Data;
using ElectricityRateApp.Models;
using System;

namespace ElectricityRateApp.HelperMethods
{
    // Overloaded methods to help the Get, Calculate, and Compare methods of the RateComparsionResult,
    // ResidentialChargeResult, and UtilitySearch models. These methods help prevent methods smells.
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
              
    }
}
