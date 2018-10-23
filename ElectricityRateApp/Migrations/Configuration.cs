namespace ElectricityRateApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using ElectricityRateApp.Data;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<ElectricityRateApp.Data.ElectricityRatesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        
        protected override void Seed(ElectricityRateApp.Data.ElectricityRatesContext context)
        {
        }
    }
}
