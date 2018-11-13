using System;
using System.IO;
using ElectricityRateApp.Models;
using CsvHelper;
using System.Linq;
using System.Diagnostics;

namespace ElectricityRateApp.Data
{
    public static class CSVtoDB
    {                    
        public static void AddPowerRates()
        {
            //Method to deserialize CSV file, to populate the properties of the PowerRate model
            //when the class is instantiated, and to persist each instance to an EF code-first database.
            using (var context = new ElectricityRatesContext())
            {
                if (context.PowerRates.Any())
                    return;
                Console.WriteLine("Adding electricty rate information to the database. This process may take a few minutes.");
                string fileName = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, "iouzipcodes2016.csv");
                int i = 1;
                using (var reader = new StreamReader(fileName))
                {
                    using (var csv = new CsvReader(reader))
                    {
                        csv.Read();
                        csv.ReadHeader();
                        while (csv.Read())
                        {
                            string zipCode;
                            if (csv[0].Length < 5)
                            {
                                zipCode = "0" + csv[0];
                            }
                            else
                            { zipCode = csv[0]; }
                            string utilityName = csv[2];
                            double residentialRate;
                            if (double.TryParse(csv[8], out residentialRate)) ;

                            PowerRate rate = new PowerRate(zipCode, utilityName, residentialRate);
                            context.PowerRates.Add(rate);

                            if (i % 5000 == 0)
                            {
                                Console.WriteLine("Adding more rates.");
                            }
                            i++;

                        }
                    }
                    context.SaveChanges();
                }
                Console.WriteLine("Electrity rate information added! Proceeding to the Main Menu.");
            }
        }
    }
}
