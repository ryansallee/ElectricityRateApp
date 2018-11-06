using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.GetAndCalculate
{
    public static class SearchResultsHelper
    {
        public static bool NumberOfResults(out int numberofResults)
        {
            Console.WriteLine("How many of the most recent electric utility provider search results would you like?");
            bool success = int.TryParse(Console.ReadLine(), out numberofResults);
            if (!success)
                Console.WriteLine("Please provide a valid integer. Please try again");
            return success;
        }
    }
}
