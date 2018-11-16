using System;

namespace ElectricityRateApp.Models
{
    //Abstraction of a Result.
    //When the class is inherited and implemented it must take a type.
    public abstract class Result<T>
    {
        //Both of these properties are shared amongst RateComparsionResult, ResidentialChargeResult, and
        //UtilityResult. 
 

        ////Abstract method as all Classes that inherit Result<T> create results.
        //public abstract void PopulateAndDisplayResult(T t);

        // Abstract method to help the Get, Calculate, and Compare methods of the RateComparsionResult get inputs,
        // ResidentialChargeResult, and UtilitySearch models. 
        // Protected access modifier as the method should only be called on classes that inherit Result<T>
        protected abstract T GetInput(T t);

        // Abstract method to check the inputs obtained in the GetInput method. If the input is
        // not valid, the method returns false so that its parent method returns and executes no further code.
        // Protected access modifier as the method should only be called on classes that inherit Result<T>
        protected abstract bool CheckValidInput(T t);

        //Abstract method to persist RateCommparisonResult ResidentialChargeResult, and UtilitySearchResults.
        // Protected access modifier as the method should only be called on classes that inherit Result<T>
        protected abstract void Save(T t);

        // Method to help the GetHistory methods of the RateComparisonResult, ResidentialChargeResult,
        // and UtilitySearchResult models the the number of previous results. It also checks
        // to make sure that the input given is valid. If the input is not valid, the method
        // returns false so that the GetHistory method does not execute further code.
        // Protected access modifier as the method should only be called on classes that inherit Result<T>
        protected static bool NumberOfResults(out int numberofResults, string resultName)
        {
            Console.WriteLine(string.Format("How many of the most recent {0} would you like?", resultName));
            bool success = int.TryParse(Console.ReadLine(), out numberofResults);
            if (!success)
                Console.WriteLine("Please provide a valid integer. Please try again");
            return success;
        }

        // Overloaded method that returns false if GetZipCode returns null so that its parent method
        // does not execute any further code.
        // Static Polymorphism
        protected static bool DoesCityExist(string zipCode, string city, string stateAbbreviation)
        {
            if (string.IsNullOrEmpty(zipCode))
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US", city, stateAbbreviation));
                return false;
            }
            else
            {
                return true;
            }
        }

        //Overload of DoesCityExist.
        protected static bool DoesCityExist(string zipCode1, string city1, string stateAbbreviation1, string zipCode2, string city2, string stateAbbreviation2)
        {
            bool cityExists = true;
            if (string.IsNullOrEmpty(zipCode1))
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US. Unfortunately, we cannot proceed with comparing rates. Please try again.", city1, stateAbbreviation1));
                cityExists = false;
            }
            if (string.IsNullOrEmpty(zipCode2))
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US. Unfortunately, we cannot proceed with comparing rates. Please try again.", city2, stateAbbreviation2));
                cityExists = false;
            }
            return cityExists;
        }

    }
}
