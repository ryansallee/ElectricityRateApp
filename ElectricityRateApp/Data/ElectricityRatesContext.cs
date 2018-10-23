using System.Data.Entity;
using ElectricityRateApp.Models;

namespace ElectricityRateApp.Data
{
    public class ElectricityRatesContext: DbContext
    {
        public ElectricityRatesContext() : base("Rates")
        { }
        public DbSet<PowerRate> PowerRates { get; set; }
    }
}
