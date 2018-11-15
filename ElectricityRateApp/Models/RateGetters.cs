using ElectricityRateApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.Models
{
    //Abstraction of a RateGetter. It inherits Result<T> as all RateGetters are also Results.
    //When the class is inhertited it must take a type.
    public abstract class RateGetters<T>: Result<T>
    {
        //Implemented method to get a rate from PowerRates as all RateGetters need to have 
        //same implmentation.
        // Protected access modifier as the method should only be called on classes that inherit RateGetters<T>.
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

        // Abstract method to check if a Rate property is 0.If the rate is 0, the implemented
        // method returns false so that its parent method returns and executes no further code.
        // Protected access modifier as the method should only be called on classes that inherit RateGetters<T>.
        abstract protected bool CheckIfRate0(T t);
    }
}
