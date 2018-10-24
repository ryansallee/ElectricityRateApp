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
        
        public static void GetProviderName(string zipCode)
        {
            string electricProvider;
            using (var context = new ElectricityRatesContext())
            {
                electricProvider = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                                    .Select(pr => pr.UtilityName)
                                    .SingleOrDefault();
            }

            Console.WriteLine(string.Format("Your electric utility provider is {0}.", electricProvider));
        }

        /// <summary>
        /// Calculates and displayes estimated electricity charges for Commer
        /// </summary>s
        /// <param name="usage">An int that represents the number of kiolwatt hours</param>
        public static void CalculateResidentialCharges(string zipCode, int usage)
        {
            double rate;
            double charge;
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
