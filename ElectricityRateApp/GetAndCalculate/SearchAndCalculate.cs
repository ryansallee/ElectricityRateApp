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

            //TODO GetRate Method
            using (var context = new ElectricityRatesContext())
            {
               chargeResult.Rate = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                        .Select(pr => pr.ResidentialRate)
                        .DefaultIfEmpty(0)
                        .Sum();                       
            }
            chargeResult.Charge = chargeResult.Rate * chargeResult.Usage;

            if(!SearchAndCalculateHelpers.CheckIfRateIs0(chargeResult))
                return;
            Console.WriteLine(string.Format("Your estimated non-fixed charges for {0} kilowatt hours is {1:C}!", chargeResult.Usage, chargeResult.Charge));
            SaveSearchResults.SaveRateCalculation(chargeResult);
        }

        public static void CompareRates()
        {
            Console.WriteLine("Please provide the name of the first city in your rate comparsion");
            string city1 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            string stateAbbreviation1 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the name of the second city.");
            string city2 = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            string stateAbbreviation2 = Console.ReadLine().ToUpper();
            if (!SearchAndCalculateHelpers.CheckValidInput(city1, stateAbbreviation1, city2, stateAbbreviation2))
                return;

            string zipCode1 = ZipCodeMethods.GetZipCode(city1, stateAbbreviation1).Result;
            string zipCode2 = ZipCodeMethods.GetZipCode(city2, stateAbbreviation2).Result;

            if (!SearchAndCalculateHelpers.DoesCityExist(zipCode1, city1, stateAbbreviation1, 
                    zipCode2, city2, stateAbbreviation2))
                return;

            //TODO Get Rate Method
            double rate1;
            double rate2;
            using (var context = new ElectricityRatesContext())
            {

                rate1 = context.PowerRates.Where(pr => pr.ZipCode == zipCode1)
                         .Select(pr => pr.ResidentialRate)
                         .DefaultIfEmpty(0)
                         .Sum();

                rate2 = context.PowerRates.Where(pr => pr.ZipCode == zipCode2)
                         .Select(pr => pr.ResidentialRate)
                         .DefaultIfEmpty(0)
                         .Sum();
            }

            //TODO Check if rate is 0 method?.
            if (rate1 == 0 && rate2 == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} and {2}, {3}. " +
                   "We cannot calculate any rates.",
                   city1, stateAbbreviation1, city2, stateAbbreviation2));
                return;
            }
            else if (rate1 == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} " +
                    "and cannot compare the rates of {0}, {1} with {2}, {3}.",
                    city1, stateAbbreviation1, city2, stateAbbreviation2));
                return;
            }
            else if (rate2 == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} " +
                   "and cannot compare the rates of {0}, {1} with {2}, {3}.",
                   city2, stateAbbreviation2, city1, stateAbbreviation1));
                return;
            }

            double difference = (rate1 - rate2) / rate2;
            
            if (rate1 > rate2)
            {
                Console.WriteLine(String.Format("The rate in {0}, {1} is {2:P2} more than in {3}, {4}.",
                    city1, stateAbbreviation1, Math.Abs(difference), city2, stateAbbreviation2));
                
            }
            else if (rate2 > rate1)
            {
                Console.WriteLine(String.Format("The rate in  {0}, {1} is {2:P2} less than in {3}, {4}.",
                    city1, stateAbbreviation1, Math.Abs(difference), city2, stateAbbreviation2));
            }
            SaveSearchResults.SaveRateComparison(city1, stateAbbreviation1, rate1, difference, city2, stateAbbreviation2, rate2);
        }         
        
    }
}
