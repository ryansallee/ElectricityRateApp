using System;
using System.Linq;
using ElectricityRateApp.Data;

namespace ElectricityRateApp.Calculation_Methods
{
    public static class SearchAndCalculate
    {
        
        public static void GetProviderName(string city, string stateAbbreviation)
        {
            string electricProvider;
            string zipCode = ZipCodeMethods.GetZipCode(city, stateAbbreviation).Result;
            //TODO Null check method?
            if (zipCode == null)
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US", city, stateAbbreviation.ToUpper()));
                return;
            }
            //TODO Get provider method.
            using (var context = new ElectricityRatesContext())
            {
                electricProvider = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                                   .Select(pr => pr.UtilityName)
                                   .FirstOrDefault();                
            }

            if (electricProvider == null)
            {
                Console.WriteLine("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", city, stateAbbreviation);
            }
            else
            {
                Console.WriteLine(string.Format("The electric utility provider in {0}, {1} is {2}.", city, stateAbbreviation, electricProvider));
            }
        }

        /// <summary>
        /// Calculates and displayes estimated electricity charges for Commer
        /// </summary>
        /// <param name="usage">An int that represents the number of kiolwatt hours</param>
        public static void CalculateResidentialCharges(string city, string stateAbbreviation, int usage)
        {
            double rate;
            double charge;
            string zipCode = ZipCodeMethods.GetZipCode(city, stateAbbreviation).Result;
            if (zipCode == null)
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US. Unfortunately, we cannot proceed with calculating the estimated charges. Please try again.", city, stateAbbreviation));
                return;
            }
            //TODO GetRate Method
            using (var context = new ElectricityRatesContext())
            {

               rate = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                        .Select(pr => pr.ResidentialRate)
                        .DefaultIfEmpty(0)
                        .Sum();
                       
            }
            //TODO Handle if rate is 0.
            charge = (double)(rate * usage);
            Console.WriteLine(string.Format("Your estimated non-fixed charges for {0} kilowatt hours is {1:C}!", usage, charge));
        }

        public static void CompareRates(string city1, string stateAbbreviation1, string city2, string stateAbbreviation2)
        {
            double rate1;  
            double rate2;
            string zipCode1 = ZipCodeMethods.GetZipCode(city1, stateAbbreviation1).Result;
            //TODO null checks
            if (zipCode1 == null)
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US. Unfortunately, we cannot proceed with comparing rates. Please try again.", city1, stateAbbreviation1));
                return;
            }
            string zipCode2 = ZipCodeMethods.GetZipCode(city2, stateAbbreviation2).Result;
            if (zipCode2 == null)
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US. Unfortunately, we cannot proceed with comparing rates. Please try again.", city2, stateAbbreviation2));
                return;
            }
            //TODO Get Rate Method
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

            //TODO Handle if rate is 0.
            if(rate1 > rate2)
            {
                double difference = (rate1 - rate2) / rate2;
                Console.WriteLine(String.Format("The rate in {0}, {1} is {2:P2} more than in {3}, {4}.", city1, stateAbbreviation1, difference, city2, stateAbbreviation2));
            }
            else if (rate2 > rate1)
            {
                double difference = (rate2 - rate1) / rate2;
                Console.WriteLine(String.Format("The rate in  {0}, {1} is {2:P2} less than in {3}, {4}.", city1, stateAbbreviation1, difference, city2, stateAbbreviation2));
            }
        }
            
        
    }
}
