using ConsoleTables;
using ElectricityRateApp.Data;
using ElectricityRateApp.Models;
using System;
using System.Linq;

namespace ElectricityRateApp.Logic
{
    class ResidentialChargeResultLogic: RateGettersLogic<ResidentialChargeResult>
    {
        // Method using the implementations of the members of RateGettersLogic<ResidentialChargeResult>
        // to populate the properties of a ResidentialChargeResult (a comparison of electricity rates)
        // and persist that instance of a ResidentialChargeResult to the database.
        public void PopulateAndDisplayResult(ResidentialChargeResult chargeResult, ResidentialChargeResultLogic logic)
        {
            try
            {
                logic.GetInput(chargeResult);
                if (!logic.CheckValidInput(chargeResult))
                    return;

                string zipCode = ZIPCodeLogic.GetZipCode(chargeResult.City, chargeResult.StateAbbreviation).Result;
                if (!DoesCityExist(zipCode, chargeResult.City, chargeResult.StateAbbreviation))
                    return;

                chargeResult.Rate = logic.GetRate(zipCode);
                chargeResult.Charge = chargeResult.Rate * chargeResult.Usage;

                if (!logic.CheckIfRate0(chargeResult))
                    return;
                Console.WriteLine(string.Format("Your estimated non-fixed charges for {0} kilowatt hours is {1:C}!", chargeResult.Usage, chargeResult.Charge));
                logic.Save(chargeResult);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }

        // Implementation of the GetHistory abstract method.
        // Method to get a user-specified length of IQueryable<ResidentialChargeResult> and displays them 
        // to the console using the ConsoleTables NuGet extension.
        public override void GetHistory()
        {
            if (!NumberOfResults(out int numberOfResults, "residential charge estimates"))
                return;

            var table = new ConsoleTable("Time", "City", "State", "Rate", "Charge", "Usage(kWh)");
            using (var context = new ElectricityRatesContext())
            {
                var results = context.ResidentialChargeResults.OrderByDescending(r => r.Id)
                    .Take(numberOfResults);

                foreach (var result in results)
                {
                    table.AddRow(result.Time.ToString(), result.City, result.StateAbbreviation,
                        string.Format("{0:C}", result.Rate), string.Format("{0:C}", result.Charge), result.Usage);
                }
                Console.WriteLine(string.Format("Here are the last {0} result(s):", results.Count()));
            }
            table.Write();
            Console.WriteLine();
        }

        // Implementation of the GetInput abstract method.
        protected override ResidentialChargeResult GetInput(ResidentialChargeResult chargeResult)
        {
            Console.WriteLine("Please provide the name of the city for which you would like estimate your usage-based electric charges.");
            chargeResult.City = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the state abbreviation.");
            chargeResult.StateAbbreviation = Console.ReadLine().ToUpper();
            Console.WriteLine("Please provide the usage in kilowatt hours(kWh). Most often your utility bill will have this information");
            int.TryParse(Console.ReadLine(), out int usage);
            chargeResult.Usage = usage;
            return chargeResult;
        }

        // Implementation of the CheckValidInput method.
        protected override bool CheckValidInput(ResidentialChargeResult chargeResult)
        {
            bool inputValid = true;
            if (string.IsNullOrEmpty(chargeResult.City))
            {
                Console.WriteLine("A city was not provided. Please try again.");
                inputValid = false;
            }
            if (string.IsNullOrEmpty(chargeResult.StateAbbreviation))
            {
                Console.WriteLine("A state abbreviation was not provided. Please try again.");
                inputValid = false;
            }
            if (chargeResult.StateAbbreviation.Length != 2 && chargeResult.StateAbbreviation.Length > 0)
            {
                Console.WriteLine("A valid state abbreviation is 2 letters long. Please try again.");
                inputValid = false;
            }
            if (chargeResult.Usage == 0)
            {
                Console.WriteLine("The usage was not provided or invalid. Estimated charges cannot be provided. Please try again");
                inputValid = false;
            }
            return inputValid;
        }

        // Implementation of the Save abstract method.
        protected override void Save(ResidentialChargeResult chargeResult)
        {
            chargeResult.Time = DateTime.Now;
            using (var context = new ElectricityRatesContext())
            {
                context.ResidentialChargeResults.Add(chargeResult);
                context.SaveChanges();
            }
        }

        // Implementation of the CheckIfRate0 abstract method.
        protected override bool CheckIfRate0(ResidentialChargeResult chargeResult)
        {
            if (chargeResult.Rate == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", chargeResult.City, chargeResult.StateAbbreviation));
                return false;
            }
            return true;
        }
    }
}
