using ConsoleTables;
using ElectricityRateApp.Data;
using System;
using System.Linq;

namespace ElectricityRateApp.Models
{
    //Class to model a ResidentialChargeResult.
    //Implements ICheckRate<T> interface as well as inherits and implements AbstractResult<T>.
    public class ResidentialChargeResult:AbstractResult<ResidentialChargeResult>, IRate<ResidentialChargeResult>
    {
        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public double Rate { get; set; }
        public double Charge { get; set; }
        public int Usage { get; set; }

        // Method using the methods of GetAndCalculateHelpers, implementations of the members of AbstractResult<ResidentialChargeResult>
        // and Iate<T> to instantiate a ResidentialChargeResult, populate its properties
        // (an estimation of eletricity usage charges), and persist that instance of a ResidentialChargeResult
        // to the database.
        public void Calculate(ResidentialChargeResult chargeResult)
        {
            try
            {   chargeResult.GetInput(chargeResult);
                if (!chargeResult.CheckValidInput(chargeResult))
                    return;                

                string zipCode = ZipCodes.GetZipCode(chargeResult.City, chargeResult.StateAbbreviation).Result;
                if (!DoesCityExist(zipCode, chargeResult.City, chargeResult.StateAbbreviation))
                    return;

                chargeResult.Rate = chargeResult.GetRate(zipCode);
                chargeResult.Charge = chargeResult.Rate * chargeResult.Usage;

                if (!chargeResult.CheckIfRate0(chargeResult))
                    return;
                Console.WriteLine(string.Format("Your estimated non-fixed charges for {0} kilowatt hours is {1:C}!", chargeResult.Usage, chargeResult.Charge));
                chargeResult.Save(chargeResult);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }

        // Method to get a user-specified length of IQueryable<ResidentialChargeResult> and displays them 
        // to the console using the ConsoleTables NuGet extension.
        public static void GetHistory()
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

        //Implementation of abstract method.
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

        //Implementation of abstract method.
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

        //Implementation of abstract method.
        protected override void Save(ResidentialChargeResult chargeResult)
        {
            chargeResult.Time = DateTime.Now;
            using (var context = new ElectricityRatesContext())
            {
                context.ResidentialChargeResults.Add(chargeResult);
                context.SaveChanges();
            }
        }

        //Implementation of the IRate<T>.
        public bool CheckIfRate0(ResidentialChargeResult chargeResult)
        {
            if (chargeResult.Rate == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", chargeResult.City, chargeResult.StateAbbreviation));
                chargeResult.Save(chargeResult);
                return false;
            }
            return true;
        }

        //Implementation of IRate<T>.
        public double GetRate(string zipCode)
        {
            using (var context = new ElectricityRatesContext())
            {
                return context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                            .Select(pr => pr.ResidentialRate)
                            .DefaultIfEmpty(0)
                            .Sum();
            }
        }
    }
}
