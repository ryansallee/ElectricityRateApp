using ElectricityRateApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.Models
{
   public abstract class RateGetters<T>: Result<T>
    {
        protected double GetRate(string zipCode)
        {
            using (var context = new ElectricityRatesContext())
            {
                return context.PowerRates.Where(pr => pr.ZipCode == zipCode)
                            .Select(pr => pr.ResidentialRate)
                            .DefaultIfEmpty(0)
                            .Sum();
            }
        }

        abstract protected bool CheckIfRate0(T t);
    }
}
