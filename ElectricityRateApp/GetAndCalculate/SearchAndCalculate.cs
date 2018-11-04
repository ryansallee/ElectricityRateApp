using System;
using System.Linq;
using ElectricityRateApp.Data;

namespace ElectricityRateApp.GetAndCalculate
{
    public static class SearchAndCalculate
    {        
        public static void GetProviderName()
        {
            Console.WriteLine("Please provide the name of the city for which you would like to find the Electric Utility Proivder.");
            string city = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            string stateAbbreviation = Console.ReadLine().ToUpper();
            if (!SearchAndCalculateHelpers.CheckValidInput(city, stateAbbreviation))
                return;
                        
            string zipCode = ZipCodeMethods.GetZipCode(city, stateAbbreviation).Result;
            if (!SearchAndCalculateHelpers.DoesCityExist(zipCode, city, stateAbbreviation))
                return;

            //TODO Get provider method.
            string electricProvider;
            using (var context = new ElectricityRatesContext())
            {
                electricProvider = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                                   .Select(pr => pr.UtilityName)
                                   .FirstOrDefault();                
            }
            //TODO Check if provider is null method?
            if (electricProvider == null)
            {
                Console.WriteLine("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", city, stateAbbreviation);
                SaveSearchResults.SaveProviderResult(city, stateAbbreviation, "No Provider Info Found");
            }
            else
            {
                Console.WriteLine(string.Format("The electric utility provider in {0}, {1} is {2}.", city, stateAbbreviation, electricProvider));
                SaveSearchResults.SaveProviderResult(city, stateAbbreviation, electricProvider);
            }
        }

        /// <summary>
        /// Calculates and displayes estimated electricity charges for Commer
        /// </summary>
        /// <param name="usage">An int that represents the number of kiolwatt hours</param>
        public static void CalculateResidentialCharges()
        {
            Console.WriteLine("Please provide the name of the city for which you would like estimate your usage-based charges.");
            string city = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            string stateAbbreviation = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the usage in kilowatt hours(kWh). Most often your utility bill will have this information");
            int usage;
            int.TryParse(Console.ReadLine(), out usage);
            if (!SearchAndCalculateHelpers.CheckValidInput(city, stateAbbreviation, usage))
                return;

            string zipCode = ZipCodeMethods.GetZipCode(city, stateAbbreviation).Result;
            if (!SearchAndCalculateHelpers.DoesCityExist(zipCode, city, stateAbbreviation))
                return;

            //TODO GetRate Method
            double rate;
            using (var context = new ElectricityRatesContext())
            {

               rate = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                        .Select(pr => pr.ResidentialRate)
                        .DefaultIfEmpty(0)
                        .Sum();
                       
            }
            //TODO Check if rate is 0 method?
            if (rate == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", city, stateAbbreviation));
                SaveSearchResults.SaveRateCalculation(city, stateAbbreviation, 0, 0, usage);
                return;
            }
            double charge = (double)(rate * usage);
            Console.WriteLine(string.Format("Your estimated non-fixed charges for {0} kilowatt hours is {1:C}!", usage, charge));
            SaveSearchResults.SaveRateCalculation(city, stateAbbreviation, rate, charge, usage);
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
