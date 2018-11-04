using ElectricityRateApp.Data;
using System;
using ElectricityRateApp.GetAndCalculate;

namespace ElectricityRateApp
{
    class Program
    {
        static void  Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth-10, Console.LargestWindowHeight-15);
            CSVtoDB.CreateDatabase();

            Console.WriteLine("Obtain the name of the electric provider in your city");
            Console.WriteLine("Provide the name of the first city");
            string city1 = Console.ReadLine().ToUpper();
            Console.WriteLine("Provide the name of the first state.");
            string stateAbbreviation1 = Console.ReadLine().ToUpper();
            SearchAndCalculate.GetProviderName(city1, stateAbbreviation1);
            Console.WriteLine("Provide the name of the second city.");
            string city2 = Console.ReadLine().ToUpper();
            Console.WriteLine("Provide the name of the second state.");
            string stateAbbreviation2 = Console.ReadLine().ToUpper();
            SearchAndCalculate.CompareRates(city1, stateAbbreviation1, city2, stateAbbreviation2);


            Console.WriteLine("You can calculate your variable (non-fixed) monthly electricity usage charge. Please provide an estimated usage in kilowatt hours");
            int usage = int.Parse(Console.ReadLine());
            Console.WriteLine("Provide the city name.");
            string city = Console.ReadLine().ToUpper();
            Console.WriteLine("Provide the state abbreviation");
            string stateAbbreviation = Console.ReadLine().ToUpper();
            SearchAndCalculate.CalculateResidentialCharges(city, stateAbbreviation, usage);
            Console.ReadKey();

            SearchResults.GetProviderSearchHistory();
            
            Console.ReadKey();
            SearchResults.GetChargeCalcuationHistory();
            Console.ReadKey();
            SearchResults.GetRateComparisonHistory();
            Console.ReadKey();
        }
    }
}
