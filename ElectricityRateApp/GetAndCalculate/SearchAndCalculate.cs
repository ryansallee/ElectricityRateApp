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
            ProviderSearchResult providerSearch = new ProviderSearchResult();
            Console.WriteLine("Please provide the name of the city for which you would like to find the Electric Utility Proivder.");
            providerSearch.City = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            providerSearch.StateAbbreviation = Console.ReadLine().ToUpper();
            if (!SearchAndCalculateHelpers.CheckValidInput(providerSearch.City, providerSearch.StateAbbreviation))
                return;
                        
            string zipCode = ZipCodeMethods.GetZipCode(providerSearch.City, providerSearch.StateAbbreviation).Result;
            if (!SearchAndCalculateHelpers.DoesCityExist(zipCode, providerSearch.City, providerSearch.StateAbbreviation))
                return;

            //TODO Get provider method.
           using (var context = new ElectricityRatesContext())
            {
                providerSearch.ProviderName = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                                   .Select(pr => pr.UtilityName)
                                   .FirstOrDefault();                
            }
            //TODO Check if provider is null method?
            if (providerSearch.ProviderName == null)
            {
                Console.WriteLine("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", providerSearch.City, providerSearch.StateAbbreviation);
                providerSearch.ProviderName = "No Provider Info Found";
                SaveSearchResults.SaveProviderResult(providerSearch);
            }
            else
            {
                Console.WriteLine(string.Format("The electric utility provider in {0}, {1} is {2}.", providerSearch.City, providerSearch.StateAbbreviation, providerSearch.ProviderName));
                SaveSearchResults.SaveProviderResult(providerSearch);
            }
        }

        /// <summary>
        /// Calculates and displayes estimated electricity charges for Commer
        /// </summary>
        /// <param name="usage">An int that represents the number of kiolwatt hours</param>
        public static void CalculateResidentialCharges()
        {
            ResidentialChargeResult chargeResult = new ResidentialChargeResult();
            Console.WriteLine("Please provide the name of the city for which you would like estimate your usage-based charges.");
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
            Console.WriteLine("Please provide the name of the first city in your rate comparsion");
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
