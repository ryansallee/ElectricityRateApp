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

            var comparison1 = new RateComparisonResult()
            {
                Id = 1,
                Time = DateTime.Now,
                City1 = "LOUISVILLE",
                StateAbbreviation1 = "KY",
                Rate1 = 0.104106287,
                Difference = -0.74478717612071588,
                City2 = "NEW YORK CITY",
                StateAbbreviation2 = "NY",
                Rate2 = 0.40791949800000005
            };

            var comparison2 = new RateComparisonResult()
            {
                Id = 2,
                Time = DateTime.Now,
                City1 = "PORTLAND",
                StateAbbreviation1 = "OR",
                Rate1 = 0.114040148,
                Difference = 0.10739359834497882,
                City2 ="BOISE",
                StateAbbreviation2 = "ID",
                Rate2 = 0.102980682
            };

            var comparison3 = new RateComparisonResult()
            {
                Id = 3,
                Time = DateTime.Now,
                City1 = "HELENA",
                StateAbbreviation1 = "MT",
                Rate1 = 0.167670018,
                Difference = 0.461756731501489,
                City2 = "BOULDER",
                StateAbbreviation2 = "CO",
                Rate2 = 0.114704461
            };

            context.RateComparisonResults.AddOrUpdate(comparison1, comparison2, comparison3);
        }
    }
}
