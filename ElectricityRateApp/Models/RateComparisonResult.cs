using ConsoleTables;
using ElectricityRateApp.Data;
using System;
using System.Linq;

namespace ElectricityRateApp.Models
{
    //Class to model a RateComparsionResult.
    //Implements ICheckRate<T> interface as well as inherits and implements AbstractResult<T>.
    public class RateComparisonResult
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string City1 { get; set; }
        public string StateAbbreviation1 { get; set; }
        public double Rate1 { get; set; }
        public double Difference { get; set; }
        public string City2 { get; set; }
        public string StateAbbreviation2 { get; set; }
        public double Rate2 { get; set; }

        //// Implementation of the PopulateAndDisplayResult abstract method.
        //// Method using the implementations of the members of RateGetters<RateComparisonResult>
        //// to populate the properties of a RateComparsion Result (a comparison of electricity rates)
        //// and persist that instance of a ResidentialChargeResult to the database.
        //public override void PopulateAndDisplayResult(RateComparisonResult rateComparison)
        //{
        //    try
        //    {
        //        rateComparison.GetInput(rateComparison);
        //        if (!rateComparison.CheckValidInput(rateComparison))
        //            return;

        //        string zipCode1 = ZipCodes.GetZipCode(rateComparison.City1, rateComparison.StateAbbreviation1).Result;
        //        string zipCode2 = ZipCodes.GetZipCode(rateComparison.City2, rateComparison.StateAbbreviation2).Result;

        //        if (!DoesCityExist(zipCode1, rateComparison.City1, rateComparison.StateAbbreviation1,
        //                zipCode2, rateComparison.City2, rateComparison.StateAbbreviation2))
        //            return;

        //        rateComparison.Rate1 = rateComparison.GetRate(zipCode1);
        //        rateComparison.Rate2 = rateComparison.GetRate(zipCode2);

        //        if (!rateComparison.CheckIfRate0(rateComparison))
        //            return;

        //        rateComparison.Difference = (rateComparison.Rate1 - rateComparison.Rate2) / rateComparison.Rate2;

        //        if (rateComparison.Rate1 > rateComparison.Rate2)
        //        {
        //            Console.WriteLine(String.Format("The rate in {0}, {1} is {2:P2} more than in {3}, {4}.",
        //                rateComparison.City1, rateComparison.StateAbbreviation1, Math.Abs(rateComparison.Difference),
        //                rateComparison.City2, rateComparison.StateAbbreviation2));
        //        }
        //        else if (rateComparison.Rate2 > rateComparison.Rate1)
        //        {
        //            Console.WriteLine(String.Format("The rate in  {0}, {1} is {2:P2} less than in {3}, {4}.",
        //                rateComparison.City1, rateComparison.StateAbbreviation1, Math.Abs(rateComparison.Difference),
        //                rateComparison.City2, rateComparison.StateAbbreviation2));
        //        }
        //        else
        //        {
        //            Console.WriteLine(string.Format("The rates in {0}, {1} and {2}, {3} are the same.", rateComparison.City1, rateComparison.StateAbbreviation1,
        //                 rateComparison.City2, rateComparison.StateAbbreviation2));
        //        }
        //       rateComparison.Save(rateComparison);
        //    }
        //    catch (SystemException e)
        //    {
        //        Console.WriteLine(e.InnerException.Message);
        //    }
        //}

        //// Method to get a user-specified length IQueryable<RateComparisonResult> and displays them 
        //// to the console using the ConsoleTables NuGet extension.
        //public static void GetHistory()
        //{
        //    if (!NumberOfResults(out int numberOfResults, "rate comparisons"))
        //        return;
        //    var table = new ConsoleTable("Time", "City 1", "State 1", "Rate 1", "City 2", "State 2", "Rate 2", "Difference");
        //    using (var context = new ElectricityRatesContext())
        //    {
        //        var results = context.RateComparisonResults.OrderByDescending(r => r.Id)
        //            .Take(numberOfResults);

        //        foreach (var result in results)
        //        {
        //            table.AddRow(result.Time.ToString(), result.City1, result.StateAbbreviation1, string.Format("{0:C}", result.Rate1),
        //                result.City2, result.StateAbbreviation2, string.Format("{0:C}", result.Rate2), string.Format("{0:P2}", result.Difference));
        //        }
        //        Console.WriteLine(string.Format("Here are the last {0} result(s):", results.Count()));
        //        Console.WriteLine("A negative percentage in the Difference Column means the first city's rate is less.");
        //    }
        //    table.Write();
        //    Console.WriteLine();
        //}
        
        ////Implementation of the GetInput abstract method.
        //protected override RateComparisonResult GetInput(RateComparisonResult rateComparison)
        //{
        //    Console.WriteLine("Please provide the name of the first city to compare rates between cities.");
        //    rateComparison.City1 = Console.ReadLine().ToUpper();
        //    Console.WriteLine("Please provide the state abbreviation.");
        //    rateComparison.StateAbbreviation1 = Console.ReadLine().ToUpper();
        //    Console.WriteLine("Please provide the name of the second city.");
        //    rateComparison.City2 = Console.ReadLine().ToUpper();
        //    Console.WriteLine("Please provide the state abbreviation.");
        //    rateComparison.StateAbbreviation2 = Console.ReadLine().ToUpper();
        //    return rateComparison;
        //}

        ////Implementation of the CheckValidInput abstract method.
        //protected override bool CheckValidInput(RateComparisonResult rateComparison)
        //{
        //    bool inputValid = true;
        //    if (string.IsNullOrEmpty(rateComparison.City1))
        //    {
        //        Console.WriteLine("The first city was not provided. Please try again.");
        //        inputValid = false;
        //    }
        //    if (string.IsNullOrEmpty(rateComparison.StateAbbreviation1))
        //    {
        //        Console.WriteLine("The first state abbreviation was not provided. Please try again.");
        //        inputValid = false;
        //    }
        //    if (rateComparison.StateAbbreviation1.Length != 2 && rateComparison.StateAbbreviation1.Length > 0)
        //    {
        //        Console.WriteLine("A valid state abbreviation was not proivded for the first state. A valid state abbreviation is 2 letters long. Please try again.");
        //        inputValid = false;
        //    }
        //    if (string.IsNullOrEmpty(rateComparison.City2))
        //    {
        //        Console.WriteLine("The second city was not provided. Please try again.");
        //        inputValid = false;
        //    }
        //    if (string.IsNullOrEmpty(rateComparison.StateAbbreviation2))
        //    {
        //        Console.WriteLine("The second state abbreviation was not provided. Please try again.");
        //        inputValid = false;
        //    }
        //    if (rateComparison.StateAbbreviation2.Length != 2 && rateComparison.StateAbbreviation2.Length > 0)
        //    {
        //        Console.WriteLine("A valid state abbreviation was not proivded for the second state. A valid state abbreviation is 2 letters long. Please try again.");
        //        inputValid = false;
        //    }
        //    return inputValid;
        //}
        
        ////Implementation of the Save abstract method.
        //protected override void Save(RateComparisonResult rateComparison)
        //{
        //    rateComparison.Time = DateTime.Now;
        //    using (var context = new ElectricityRatesContext())
        //    {
        //        context.RateComparisonResults.Add(rateComparison);
        //        context.SaveChanges();
        //    }
        //}

        ////Implementation of the CheckIfRate0 abstract method.
        //protected override bool CheckIfRate0(RateComparisonResult rateComparison)
        //{
        //    if (rateComparison.Rate1 == 0 && rateComparison.Rate2 == 0)
        //    {
        //        Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} and {2}, {3}. " +
        //          "We cannot calculate any rates.",
        //          rateComparison.City1, rateComparison.StateAbbreviation1, rateComparison.City2, rateComparison.StateAbbreviation2));
        //        return false;
        //    }
        //    else if (rateComparison.Rate1 == 0)
        //    {
        //        Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} " +
        //           "and cannot compare the rates of {0}, {1} with {2}, {3}.",
        //           rateComparison.City1, rateComparison.StateAbbreviation1, rateComparison.City2, rateComparison.StateAbbreviation2));
        //        return false;
        //    }
        //    else if (rateComparison.Rate2 == 0)
        //    {
        //        Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} " +
        //            "and cannot compare the rates of {0}, {1} with {2}, {3}.",
        //            rateComparison.City2, rateComparison.StateAbbreviation2, rateComparison.City1, rateComparison.StateAbbreviation1));
        //        return false;
        //    }
        //    return true;
        //}

    }
}
