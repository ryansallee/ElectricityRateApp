using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricityRateApp.Data;

namespace ElectricityRateApp.Calculation_Methods
{
    public static class SearchAndCalculate
    {
        
        public static void GetProviderName(string city, string stateAbbreviation)
        {
            stateAbbreviation.ToUpper();
            string electricProvider;
            string zipCode = ZipCodeMethods.GetZipCode(city, stateAbbreviation).Result;
            using (var context = new ElectricityRatesContext())
            {
                electricProvider = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                                   .Select(pr => pr.UtilityName)
                                   .FirstOrDefault();                
            }
            if (electricProvider == null)
            {
                Console.WriteLine(string.Format("We unfortunately do not have data on electric utility proivders in {0}, {1}.", city, stateAbbreviation));
            }
            else
            {
                Console.WriteLine(string.Format("The electric utility provider in {0}, {1} is {2}.", city, stateAbbreviation, electricProvider));
            }
        }

        /// <summary>
        /// Calculates and displayes estimated electricity charges for Commer
        /// </summary>s
        /// <param name="usage">An int that represents the number of kiolwatt hours</param>
        public static void CalculateResidentialCharges(string city, string stateAbbreviation, int usage)
        {
            double rate;
            double charge;
            string zipCode = ZipCodeMethods.GetZipCode(city, stateAbbreviation).Result;
            using (var context = new ElectricityRatesContext())
            {

               rate = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                        .Select(pr => pr.ResidentialRate)
                        .Sum();
                       
            }
            charge = (double)(rate * usage);
            Console.WriteLine(string.Format("Your estimated non-fixed charges for {0} kilowatt hours is {1:C}!", usage, charge));
        }

        public static void CompareRates(string zipCode1, string zipCode2)
        {
            double rate1;
            double rate2;
            using (var context = new ElectricityRatesContext())
            {

                rate1 = context.PowerRates.Where(pr => pr.ZipCode == zipCode1)
                         .Select(pr => pr.ResidentialRate)
                         .Sum();

                rate2 = context.PowerRates.Where(pr => pr.ZipCode == zipCode2)
                         .Select(pr => pr.ResidentialRate)
                         .Sum();
            }

            if(rate1 > rate2)
            {
                double difference = (rate1 - rate2) / rate2;
                Console.WriteLine(String.Format("The rate in {0} is {1:P2} more than in {2}", zipCode1, difference, zipCode2));
            }
            else if (rate2 > rate1)
            {
                double difference = (rate2 - rate1) / rate2;
                Console.WriteLine(String.Format("The rate in {0} is {1:P2} less than in {2}", zipCode1, difference, zipCode2));
            }
            
        }
    }
}
