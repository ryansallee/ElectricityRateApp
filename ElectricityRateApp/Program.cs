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

            Console.WriteLine("In which Zip Code?");
            string zipCode = Console.ReadLine();
            Console.WriteLine("How many kilowatt hours of usage?");
            int usage = int.Parse(Console.ReadLine());
            SearchAndCalculate.CalculateCommericalRates(zipCode, usage);
            Console.ReadKey();
        }
    }
}
