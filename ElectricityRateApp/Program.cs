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
            string zipCode1 = Console.ReadLine();
            Console.WriteLine("What is the second zip code");
            string zipCode2 = Console.ReadLine();
            SearchAndCalculate.CompareRates(zipCode1, zipCode2);
            Console.ReadKey();
        }
    }
}
