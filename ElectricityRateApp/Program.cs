using ElectricityRateApp.Data;
using System;

using ElectricityRateApp.Models;

namespace ElectricityRateApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth-10, Console.LargestWindowHeight-15);
            Console.WriteLine("Welcome to the Electricity Rate App.");
            CSVtoDB.AddPowerRates();
            

            var menuOption = MainMenu();

            while (menuOption < 7)
            {
                switch (menuOption)
                {
                    case 1:
                        UtilitySearchResult utilitySearch = new UtilitySearchResult();
                        utilitySearch.PopulateAndDisplayResult(utilitySearch);
                        Clear();
                        break;
                    case 2:
                        ResidentialChargeResult chargeResult = new ResidentialChargeResult();
                        chargeResult.PopulateAndDisplayResult(chargeResult);
                        Clear();
                        break;
                    case 3:
                        RateComparisonResult rateComparison = new RateComparisonResult();
                        rateComparison.PopulateAndDisplayResult(rateComparison);
                        Clear();
                        break;
                    case 4:
                        UtilitySearchResult.GetHistory();
                        Clear();
                        break;
                    case 5:
                        ResidentialChargeResult.GetHistory();
                        Clear();
                        break;
                    case 6:
                        RateComparisonResult.GetHistory();
                        Clear();
                        break;
                    case 7:
                        break;
                        
                }
                if (menuOption != 7)
                    menuOption = MainMenu();
            }
            Console.WriteLine("Goodbye!");
        }

        static int MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("\t Main Menu:");
            Console.WriteLine();
            Console.WriteLine("\t 1. \t Find an Electric Utlity Provider");
            Console.WriteLine("\t 2. \t Calculate Estimated Residential Electricity Charges");
            Console.WriteLine("\t 3. \t Compare Eletricity Rates between Cities");
            Console.WriteLine("\t 4. \t Get Previous Electricity Provider Searches");
            Console.WriteLine("\t 5. \t Get Previous Electricity Charge Estimates");
            Console.WriteLine("\t 6. \t Get Prevous Rate Comparisons");
            Console.WriteLine("\t 7. \t Exit");
            Console.WriteLine();

            Console.WriteLine("Please select an option:");
            var selection = Console.ReadLine();
            bool success = int.TryParse(selection, out int option);
            if (!success || option < 1 || option > 7)
            {
                Console.WriteLine("You did not provide a valid option. Please try again");
                Clear();
                MainMenu();
            }
            return option;
        }

        static void Clear()
        {
            Console.WriteLine("Press any key to return to the Main Menu.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
