using System.Data.Entity;
using ElectricityRateApp.Models;

namespace ElectricityRateApp.Data
{
    public class ElectricityRatesContext: DbContext
    {
        public ElectricityRatesContext() : base("Rates")
        { }
        public DbSet<PowerRate> PowerRates { get; set; }
        public DbSet<ProviderSearchResult> ProviderSearchResults { get; set; }
        public DbSet<ResidentialChargeResult> ResidentialChargeResults { get; set; }
        public DbSet<RateComparisonResult> RateComparisonResults { get; set; }
    }
}
