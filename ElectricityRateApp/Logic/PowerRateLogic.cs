using CsvHelper;
using ElectricityRateApp.Data;
using ElectricityRateApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ElectricityRateApp.Logic
{
    class PowerRateLogic
    {
        public static void AddPowerRates()
        {
            //Method to deserialize CSV file, to populate the properties of the PowerRate model
            //and to persist each instance to the EF code-first table PowerRates in the Rates database.
            using (var context = new ElectricityRatesContext())
            {
                if (context.PowerRates.Any())
                    return;
                Console.WriteLine("Since this is the first run of the app, we are adding electricty rate information to the database. This process may take a few minutes.");
                var directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                string fileName = Path.Combine(directory, "iouzipcodes2016.csv");
                int i = 1;
                //Changed adding each instance of the model class to the context to adding it to a List
                //of PowerRate since Add on a DbContext uses a lot of processing power.
                List<PowerRate> powerRatesList = new List<PowerRate>();

                using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var bs = new BufferedStream(fs))
                using (var reader = new StreamReader(bs))
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
                            if (double.TryParse(csv[8], out residentialRate));

                            PowerRate rate = new PowerRate(zipCode, utilityName, residentialRate);
                            powerRatesList.Add(rate);

                            if (i % 5000 == 0)
                            {
                                Console.WriteLine("Adding more rates.");
                            }
                            i++;
                        }
                    }
                    context.PowerRates.AddRange(powerRatesList);
                    context.SaveChanges();
                }
                Console.WriteLine("Electrity rate information added! Proceeding to the Main Menu.");
            }
        }
    }
}
