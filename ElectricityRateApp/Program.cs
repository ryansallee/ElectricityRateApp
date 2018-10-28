using ElectricityRateApp.Data;
using System;

using ElectricityRateApp.Calculation_Methods;
using System.Threading.Tasks;

namespace ElectricityRateApp
{
    class Program
    {
        static void  Main(string[] args)
        {
            CSVtoDB.CreateDatabase();

            Console.WriteLine("Obtain the name of the electric provider in your city");
            Console.WriteLine("Provide a name of a city");
            string city = Console.ReadLine();
            Console.WriteLine("Provide the state abbreviation");
            string stateAbbreviation = Console.ReadLine();
            SearchAndCalculate.GetProviderName(city, stateAbbreviation);
            Console.ReadKey();

        }
    }
}
