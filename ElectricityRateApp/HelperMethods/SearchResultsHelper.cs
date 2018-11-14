using System;

namespace ElectricityRateApp.HelperMethods
{
    public static class SearchResultsHelper
    {
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
 