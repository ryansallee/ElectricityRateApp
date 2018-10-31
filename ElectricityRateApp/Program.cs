using ElectricityRateApp.Data;
using System;
using ElectricityRateApp.Calculation_Methods;


namespace ElectricityRateApp
{
    class Program
    {
        static void  Main(string[] args)
        {
            CSVtoDB.CreateDatabase();

            Console.WriteLine("Obtain the name of the electric provider in your city");
            Console.WriteLine("Provide the name of the first city");
            string city1 = Console.ReadLine();
            Console.WriteLine("Provide the name of the first state.");
            string stateAbbreviation1 = Console.ReadLine();
            SearchAndCalculate.GetProviderName(city1, stateAbbreviation1);
            Console.WriteLine("Provide the name of the second city.");
            string city2 = Console.ReadLine();
            Console.WriteLine("Provide the name of the second state.");
            string stateAbbreviation2 = Console.ReadLine();
            SearchAndCalculate.CompareRates(city1, stateAbbreviation1, city2, stateAbbreviation2);
            Console.ReadKey();

        }
    }
}
