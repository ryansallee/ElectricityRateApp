using System;
using System.IO;
using System.Data.Entity;
using ElectricityRateApp.Models;
using System.Data.Entity.Migrations;
using CsvHelper;
using System.Linq;

namespace ElectricityRateApp.Data
{
    public static class CSVtoDB
    {                    
        public static void AddPowerRates()
        {
            using (var context = new ElectricityRatesContext())
            {
                if (context.PowerRates.Any())
                    return;
                Console.WriteLine("Adding electricty rate information to the database.");
                string fileName = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, "iouzipcodes2016.csv"); Console.WriteLine("creating database of electricity rates");
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

                            var rate = new PowerRate(zipCode, utilityName, residentialRate);
                            context.PowerRates.Add(rate);

                            Console.WriteLine(String.Format("Rate Added! Number{0}", i));
                            i++;

                        }
                    }
                    context.SaveChanges();
                }
                Console.WriteLine("Electrity rate information added.");
            }
        }
    }
}
