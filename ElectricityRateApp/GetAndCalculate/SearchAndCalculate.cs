﻿using System;
using System.Linq;
using ElectricityRateApp.Data;


namespace ElectricityRateApp.GetandCalculate

{
    public static class SearchAndCalculate
    {
        
        public static void GetProviderName(string city, string stateAbbreviation)
        {
            if(string.IsNullOrEmpty(city) || string.IsNullOrEmpty(stateAbbreviation))
            {
                Console.WriteLine("A city and/or state was not provided. Please try again.");
                return;
            }

            
            string electricProvider;
            string zipCode = ZipCodeMethods.GetZipCode(city, stateAbbreviation).Result;
            //TODO Null check method?
            if (zipCode == null)
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US", city, stateAbbreviation));
                return;
            }
            //TODO Get provider method.
            using (var context = new ElectricityRatesContext())
            {
                electricProvider = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                                   .Select(pr => pr.UtilityName)
                                   .FirstOrDefault();                
            }

            if (electricProvider == null)
            {
                Console.WriteLine("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", city, stateAbbreviation);
                SaveSearchResults.SaveProviderResult(city, stateAbbreviation, "No Info");
            }
            else
            {
                Console.WriteLine(string.Format("The electric utility provider in {0}, {1} is {2}.", city, stateAbbreviation, electricProvider));
                SaveSearchResults.SaveProviderResult(city, stateAbbreviation, electricProvider);
            }
        }

        /// <summary>
        /// Calculates and displayes estimated electricity charges for Commer
        /// </summary>
        /// <param name="usage">An int that represents the number of kiolwatt hours</param>
        public static void CalculateResidentialCharges(string city, string stateAbbreviation, int usage)
        {
            if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(stateAbbreviation))
            {
                Console.WriteLine("A city and/or state was not provided. Please try again.");
                return;
            }
            else if(usage == 0)
            {
                Console.WriteLine("The usage was not provided. Estimated charges cannot be provided. Please try again");
                return;
            }
            double rate;
            double charge;
            string zipCode = ZipCodeMethods.GetZipCode(city, stateAbbreviation).Result;
            if (zipCode == null)
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US. Unfortunately, we cannot proceed with calculating the estimated charges. Please try again.", city, stateAbbreviation));
                return;
            }
            //TODO GetRate Method
            using (var context = new ElectricityRatesContext())
            {

               rate = context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                        .Select(pr => pr.ResidentialRate)
                        .DefaultIfEmpty(0)
                        .Sum();
                       
            }
            //TODO Handle if rate is 0.
            if (rate == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1}.", city, stateAbbreviation));
                SaveSearchResults.SaveRateCalculation(city, stateAbbreviation, 0, 0, usage);
                return;
            }
            charge = (double)(rate * usage);
            Console.WriteLine(string.Format("Your estimated non-fixed charges for {0} kilowatt hours is {1:C}!", usage, charge));
            SaveSearchResults.SaveRateCalculation(city, stateAbbreviation, rate, charge, usage);
        }

        public static void CompareRates(string city1, string stateAbbreviation1, string city2, string stateAbbreviation2)
        {
            if(string.IsNullOrEmpty(city1) || string.IsNullOrEmpty(stateAbbreviation1) && string.IsNullOrEmpty(city2) || string.IsNullOrEmpty(stateAbbreviation2))
            {
                Console.WriteLine("A city and/or state was not provided for both locations. Please try again.");
                return;
            }
            else if (string.IsNullOrEmpty(city1) || string.IsNullOrEmpty(stateAbbreviation1))
            {
                Console.WriteLine("A city and/or state was not provided for the first location. Please try again.");
                return;
            }
            else if (string.IsNullOrEmpty(city2) || string.IsNullOrEmpty(stateAbbreviation2))
            {
                Console.WriteLine("A city and/or state was not provided for the second location. Please try again.");
                return;
            }
           
            
            double rate1;  
            double rate2;
            string zipCode1 = ZipCodeMethods.GetZipCode(city1, stateAbbreviation1).Result;
            //TODO null checks
            if (zipCode1 == null)
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US. Unfortunately, we cannot proceed with comparing rates. Please try again.", city1, stateAbbreviation1));
                return;
            }
            string zipCode2 = ZipCodeMethods.GetZipCode(city2, stateAbbreviation2).Result;
            if (zipCode2 == null)
            {
                Console.WriteLine(string.Format("{0}, {1} is not a city that exists in the US. Unfortunately, we cannot proceed with comparing rates. Please try again.", city2, stateAbbreviation2));
                return;
            }
            //TODO Get Rate Method
            using (var context = new ElectricityRatesContext())
            {

                rate1 = context.PowerRates.Where(pr => pr.ZipCode == zipCode1)
                         .Select(pr => pr.ResidentialRate)
                         .DefaultIfEmpty(0)
                         .Sum();

                rate2 = context.PowerRates.Where(pr => pr.ZipCode == zipCode2)
                         .Select(pr => pr.ResidentialRate)
                         .DefaultIfEmpty(0)
                         .Sum();
            }

            //TODO Handle if rate is 0.
            if (rate1 == 0 && rate2 == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} and {2}, {3}. " +
                   "We cannot calculate any rates.",
                   city1, stateAbbreviation1, city2, stateAbbreviation2));
                return;
            }
            else if (rate1 == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} " +
                    "and cannot compare the rates of {0}, {1} with {2}, {3}.",
                    city1, stateAbbreviation1, city2, stateAbbreviation2));
                return;
            }
            else if (rate2 == 0)
            {
                Console.WriteLine(string.Format("Unfortunately, we do not have any information on electric utility providers in {0}, {1} " +
                   "and cannot compare the rates of {0}, {1} with {2}, {3}.",
                   city2, stateAbbreviation2, city1, stateAbbreviation1));
                return;
            }

            double difference = Math.Abs(((rate1 - rate2) / rate2));
            
            if (rate1 > rate2)
            {
                Console.WriteLine(String.Format("The rate in {0}, {1} is {2:P2} more than in {3}, {4}.",
                    city1, stateAbbreviation1, difference, city2, stateAbbreviation2));
                
            }
            else if (rate2 > rate1)
            {
                Console.WriteLine(String.Format("The rate in  {0}, {1} is {2:P2} less than in {3}, {4}.",
                    city1, stateAbbreviation1, difference, city2, stateAbbreviation2));
            }
            SaveSearchResults.SaveRateComparison(city1, stateAbbreviation1, rate1, difference, city2, stateAbbreviation2, rate2);
        }
            
        
    }
}