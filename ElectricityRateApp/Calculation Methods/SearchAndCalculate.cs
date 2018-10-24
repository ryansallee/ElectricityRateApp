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
        /// Calculates and displayes estimated electricity charges
        /// </summary>s
        /// <param name="usage">An int that represents the number of kiolwatt hours</param>
        public static void CalculateCommericalRates(string zipCode, int usage)
        {
            double rate;
            double charge;
            using (var context = new ElectricityRatesContext())
            {

               rate = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                        .Select(pr => pr.CommercialRate)
                        .Sum();
                       
            }
            charge = (double)(rate * usage);
            Console.WriteLine(string.Format("Your estimated non-fixed charges for {0} kilowatt hours is {1:C}!", usage, charge));
        }
    }
}
