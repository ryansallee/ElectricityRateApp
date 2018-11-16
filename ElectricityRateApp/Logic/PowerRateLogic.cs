using CsvHelper;
using ElectricityRateApp.Data;
using ElectricityRateApp.Models;
using System;
using System.IO;
using System.Linq;

namespace ElectricityRateApp.Logic
{
    class PowerRateLogic
    {
        public void AddPowerRates()
        {
            //Method to deserialize CSV file, to populate the properties of the PowerRate model
            //when the class is instantiated, and to persist each instance to the EF code-first table PowerRates
            //in the Rates database.
            using (var context = new ElectricityRatesContext())
            {
                if (context.PowerRates.Any())
                    return;
                Console.WriteLine("Since this is the first run of the app, we are adding electricty rate information to the database. This process may take a few minutes.");
                var directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                string fileName = Path.Combine(directory, "iouzipcodes2016.csv");
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
