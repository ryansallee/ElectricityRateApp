namespace ElectricityRateApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using ElectricityRateApp.Data;
    using System.IO;
    using ElectricityRateApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ElectricityRateApp.Data.ElectricityRatesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        
        protected override void Seed(ElectricityRateApp.Data.ElectricityRatesContext context)
        {
            var utilSearch1 = new UtilitySearchResult()
            {
                Id = 1,
                Time = DateTime.Now,
                City = "CHARLOTTE",
                StateAbbreviation = "NC",
                UtilityName = "Duke Energy Carolinas, LLC"
            };

            var utilSearch2 = new UtilitySearchResult()
            {
                Id = 2,
                Time = DateTime.Now,
                City = "IOWA CITY",
                StateAbbreviation = "IA",
                UtilityName = "Interstate Power and Light Co"
            };

            var utilSearch3 = new UtilitySearchResult()
            {
                Id = 3,
                Time = DateTime.Now,
                City = "HARTFORD",
                StateAbbreviation = "CT",
                UtilityName = "Connecticut Light & Power Co"
            };

            var utilSearch4 = new UtilitySearchResult()
            {
                Id = 4,
                Time = DateTime.Now,
                City = "PITTSBURGH",
                StateAbbreviation = "PA",
                UtilityName = "Duquesne Light Co"
            };

            context.UtilitySearchResults.AddOrUpdate(utilSearch1, utilSearch2, utilSearch3, utilSearch4);

            var chargeResult1 = new ResidentialChargeResult()
            {
                Id = 1,
                Time = DateTime.Now,
                City = "ERIE",
                StateAbbreviation = "PA",
                Rate = 0.221704144,
                Charge = 221.70414399999999,
                Usage = 1000
            };

            var chargeResult2 = new ResidentialChargeResult()
            {
                Id = 2,
                Time = DateTime.Now,
                City = "LEXINGTON",
                StateAbbreviation = "KY",
                Rate = 0.098671037,
                Charge = 98.671037,
                Usage = 1000
            };

            var chargeResult3 = new ResidentialChargeResult()
            {
                Id = 3,
                Time = DateTime.Now,
                City = "LOUISVILLE",
                StateAbbreviation = "KY",
                Rate = 0.104106287,
                Charge = 145.7488018,
                Usage = 1400
            };

            context.ResidentialChargeResults.AddOrUpdate(chargeResult1, chargeResult2, chargeResult3);
        }
    }
}
