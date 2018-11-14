using System;

namespace ElectricityRateApp.Models
{
    //Abstraction
    //When the class is inherited and implemented it must take a type.
    public abstract class AbstractResult<T>
    {
        //Both of these properties are shared amongst RateComparsionResult, ResidentialChargeResult
        public int Id { get; set; }
        public DateTime Time { get; set; }

        // Abstract method to help the Get, Calculate, and Compare methods of the RateComparsionResult get inputs,
        // ResidentialChargeResult, and UtilitySearch models. These methods help prevent long method smells
        public abstract T GetInput(T t);

        // Abstract method to check the inputs obtained in the GetInput method. If the input is
        // not valid, the method returns false so that its parent method returns and executes no further code.
        public abstract bool CheckValidInput(T t);

        //Abstract method to persist RateCommparisonResult ResidentialChargeResult, and UtilitySearchResults
        public abstract void Save(T t);

        // Method to help the GetHistory methods of the RateComparisonResult, ResidentialChargeResult,
        // and UtilitySearchResult models the the number of previous results. It also checks
        // to make sure that the input given is valid. If the input is not valid, the method
        // returns false so that the GetHistory method does not execute further code.
        public static bool NumberOfResults(out int numberofResults, string resultName)
        {
            Console.WriteLine(string.Format("How many of the most recent {0} would you like?", resultName));
            bool success = int.TryParse(Console.ReadLine(), out numberofResults);
            if (!success)
                Console.WriteLine("Please provide a valid integer. Please try again");
            return success;
        }

    }
}
