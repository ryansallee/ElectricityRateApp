using System;
using System.Linq;
using ElectricityRateApp.Data;
using ElectricityRateApp.Models;

namespace ElectricityRateApp.GetAndCalculate
{
    public static class SearchAndCalculate
    {        
        public static void GetProviderName()
        {
            UtilitySearchResult utilitySearch = new UtilitySearchResult();
            Console.WriteLine("Please provide the name of the city for which you would like to find the electric utility proivder.");
            utilitySearch.City = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            utilitySearch.StateAbbreviation = Console.ReadLine().ToUpper();
            if (!SearchAndCalculateHelpers.CheckValidInput(utilitySearch.City, utilitySearch.StateAbbreviation))
                return;
                        
            string zipCode = ZipCodeMethods.GetZipCode(utilitySearch.City, utilitySearch.StateAbbreviation).Result;
            if (!SearchAndCalculateHelpers.DoesCityExist(zipCode, utilitySearch.City, utilitySearch.StateAbbreviation))
                return;
            
            utilitySearch.UtilityName = GetFromPowerRates.GetUtilityProviderName(zipCode);

            //TODO Check if provider is null method?
            if (utilitySearch.UtilityName == null)
            {
                Console.WriteLine("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", utilitySearch.City, utilitySearch.StateAbbreviation);
                utilitySearch.UtilityName = "No Provider Info Found";
                SaveSearchResults.SaveProviderResult(utilitySearch);
            }
            else
            {
                Console.WriteLine(string.Format("The electric utility provider in {0}, {1} is {2}.", utilitySearch.City, utilitySearch.StateAbbreviation, utilitySearch.UtilityName));
                SaveSearchResults.SaveProviderResult(utilitySearch);
            }
        }

        /// <summary>
        /// Calculates and displayes estimated electricity charges for Commer
        /// </summary>
        /// <param name="usage">An int that represents the number of kiolwatt hours</param>
        public static void CalculateResidentialCharges()
        {
            ResidentialChargeResult chargeResult = new ResidentialChargeResult();
            Console.WriteLine("Please provide the name of the city for which you would like estimate your usage-based electric charges.");
            chargeResult.City = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            chargeResult.StateAbbreviation = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the usage in kilowatt hours(kWh). Most often your utility bill will have this information");
            int.TryParse(Console.ReadLine(), out int usage);            
            if (!SearchAndCalculateHelpers.CheckValidInput(chargeResult.City, chargeResult.StateAbbreviation, usage))
                return;
            chargeResult.Usage = usage;

            string zipCode = ZipCodeMethods.GetZipCode(chargeResult.City, chargeResult.StateAbbreviation).Result;
            if (!SearchAndCalculateHelpers.DoesCityExist(zipCode, chargeResult.City, chargeResult.StateAbbreviation))
                return;

            chargeResult.Rate = GetFromPowerRates.GetRate(zipCode);
            chargeResult.Charge = chargeResult.Rate * chargeResult.Usage;

            if(!SearchAndCalculateHelpers.CheckIfRateIs0(chargeResult))
                return;
            Console.WriteLine(string.Format("Your estimated non-fixed charges for {0} kilowatt hours is {1:C}!", chargeResult.Usage, chargeResult.Charge));
            SaveSearchResults.SaveRateCalculation(chargeResult);
        }

        public static void CompareRates()
        {
            RateComparisonResult rateComparison = new RateComparisonResult();
            Console.WriteLine("Please provide the name of the first city to compare rates between cities");
            rateComparison.City1 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            rateComparison.StateAbbreviation1 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the name of the second city.");
            rateComparison.City2 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            rateComparison.StateAbbreviation2 = Console.ReadLine().ToUpper();
            if (!SearchAndCalculateHelpers.CheckValidInput(rateComparison.City1, rateComparison.StateAbbreviation1, rateComparison.City2, rateComparison.StateAbbreviation2))
                return;

            string zipCode1 = ZipCodeMethods.GetZipCode(rateComparison.City1, rateComparison.StateAbbreviation1).Result;
            string zipCode2 = ZipCodeMethods.GetZipCode(rateComparison.City2, rateComparison.StateAbbreviation2).Result;

            if (!SearchAndCalculateHelpers.DoesCityExist(zipCode1, rateComparison.City1, rateComparison.StateAbbreviation1, 
                    zipCode2, rateComparison.City2, rateComparison.StateAbbreviation2))
                return;

            rateComparison.Rate1 = GetFromPowerRates.GetRate(zipCode1);
            rateComparison.Rate2 = GetFromPowerRates.GetRate(zipCode2);

            if (!SearchAndCalculateHelpers.CheckIfRateIs0(rateComparison))
                return;

             rateComparison.Difference = (rateComparison.Rate1 - rateComparison.Rate2) / rateComparison.Rate2;
            
            if (rateComparison.Rate1 > rateComparison.Rate2)
            {
                Console.WriteLine(String.Format("The rate in {0}, {1} is {2:P2} more than in {3}, {4}.",
                    rateComparison.City1, rateComparison.StateAbbreviation1, Math.Abs(rateComparison.Difference), 
                    rateComparison.City2, rateComparison.StateAbbreviation2));                
            }
            else if (rateComparison.Rate2 > rateComparison.Rate1)
            {
                Console.WriteLine(String.Format("The rate in  {0}, {1} is {2:P2} less than in {3}, {4}.",
                    rateComparison.City1, rateComparison.StateAbbreviation1, Math.Abs(rateComparison.Difference),
                    rateComparison.City2, rateComparison.StateAbbreviation2));
            }
            SaveSearchResults.SaveRateComparison(rateComparison);
        }         
        
    }
}
