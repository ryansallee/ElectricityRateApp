using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.Models
{
    public abstract class AbstractResult<T>
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }

        // Abstract method to help the Get, Calculate, and Compare methods of the RateComparsionResult get inputs,
        // ResidentialChargeResult, and UtilitySearch models. These methods help prevent long method smells
        public abstract T GetInput(T t);

        // Abstract method to check the inputs obtained in the GetInput method. If the input is
        // not valid, the method returns false so that its parent method returns and executes no further code.
        public abstract bool CheckValidInput(T t);

        //Abstract method to persist RateCommparisonResult ResidentialChargeResult, and UtilitySearchResults
        public abstract void Save(T t);

        
    }
}
