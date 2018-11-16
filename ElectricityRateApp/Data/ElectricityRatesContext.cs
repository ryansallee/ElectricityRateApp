using System.Data.Entity;
using ElectricityRateApp.Models;

namespace ElectricityRateApp.Data
{
    public class ElectricityRatesContext : DbContext
    {
        //Constructor for the Context. The database is named Rates.
        public ElectricityRatesContext() : base("Rates")
        { }

        // DBsets for the 4 models that are used for the Rates database.
        public DbSet<PowerRate> PowerRates { get; set; }
        public DbSet<UtilitySearchResult> UtilitySearchResults { get; set; }
        public DbSet<ResidentialChargeResult> ResidentialChargeResults { get; set; }
        public DbSet<RateComparisonResult> RateComparisonResults { get; set; }
    }
}
