using ElectricityRateApp.Data;
using System;
using ElectricityRateApp.Calculation_Methods;

namespace ElectricityRateApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CSVtoDB.CreateDatabase();

            Console.WriteLine("Compare rates by zip code");
            Console.WriteLine("What is the first zip code");
            string zipCode = Console.ReadLine();
            SearchAndCalculate.GetProviderName(zipCode);
            Console.ReadKey();
        }
    }
}
