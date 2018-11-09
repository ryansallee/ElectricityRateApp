using ConsoleTables;
using ElectricityRateApp.Data;
using ElectricityRateApp.HelperMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.Models
{
    public class ResidentialChargeResult
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public double Rate { get; set; }
        public double Charge { get; set; }
        public int Usage { get; set; }

        public static void Calculate()
        {
            ResidentialChargeResult chargeResult = new ResidentialChargeResult();
            GetAndCalculateHelpers.GetInput(chargeResult, out int usage);
            if (!GetAndCalculateHelpers.CheckValidInput(chargeResult.City, chargeResult.StateAbbreviation, usage))
                return;
            chargeResult.Usage = usage;

            string zipCode = ZipCodeMethods.GetZipCode(chargeResult.City, chargeResult.StateAbbreviation).Result;
            if (!GetAndCalculateHelpers.DoesCityExist(zipCode, chargeResult.City, chargeResult.StateAbbreviation))
                return;

            chargeResult.Rate = GetFromPowerRates.GetRate(zipCode);
            chargeResult.Charge = chargeResult.Rate * chargeResult.Usage;

            if (!GetAndCalculateHelpers.CheckIfRateIs0(chargeResult))
                return;
            Console.WriteLine(string.Format("Your estimated non-fixed charges for {0} kilowatt hours is {1:C}!", chargeResult.Usage, chargeResult.Charge));
            SaveSearchResults.Save(chargeResult);
        }

        public static void GetHistory()
        {
            if (!SearchResultsHelper.NumberOfResults(out int numberOfResults, "residential charge estimates"))
                return;
            Console.WriteLine(string.Format("Here are the last {0} results:", numberOfResults));
            var table = new ConsoleTable("Time", "City", "State", "Rate", "Charge", "Usage(kWh)");
            using (var context = new ElectricityRatesContext())
            {
                var results = context.ResidentialChargeResults.OrderByDescending(r => r.Id)
                    .Take(numberOfResults);

                foreach (var result in results)
                {
                    table.AddRow(result.Time, result.City, result.StateAbbreviation,
                        string.Format("{0:C}", result.Rate), string.Format("{0:C}", result.Charge), result.Usage);
                }
            }
            table.Write();
            Console.WriteLine();
        }
    }
}
