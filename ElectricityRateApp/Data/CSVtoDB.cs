﻿using System;
using System.IO;
using System.Data.Entity;
using ElectricityRateApp.Models;
using System.Data.Entity.Migrations;
using CsvHelper;

namespace ElectricityRateApp.Data
{
    public static class CSVtoDB
    {
        private static string _fileName = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, "iouzipcodes2016.csv");
        private static bool dBCreated = Database.Exists("Rates");
           
        public static void CreateDatabase()
        {
            if(dBCreated)
            {
                return;
            }
            Console.WriteLine("creating database of electricity rates");
            int i = 1;
            using (var reader = new StreamReader(_fileName))
            using (var context = new ElectricityRatesContext())
            {
                using (var csv = new CsvReader(reader))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        string zipCode = csv[0];
                        string utilityName = csv[2];
                        double residentialRate;
                        if (double.TryParse(csv[8], out residentialRate));

                        var rate = new PowerRate(zipCode, utilityName, residentialRate);
                        context.PowerRates.Add(rate);

                        Console.WriteLine(String.Format("Rate Added! Number{0}", i));
                        i++;

                    }
                }
                context.SaveChanges();
            }
            
            Console.WriteLine("Database added");
        }
    }
}