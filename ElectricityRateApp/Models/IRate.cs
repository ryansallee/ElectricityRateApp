using System;

namespace ElectricityRateApp.Models
{
    //Interface for classes that obtain Rates to get a rate and check if a rate is 0 or not.
    //Allows for Polymorphism
    //When this interface is implemented, it must take a type.
    public interface IRate<T>
    { 
        // Method that returns false if the Rate is 0 and prevents its parent method from executing
        // further code. 
        bool CheckIfRate0(T t);
        double GetRate(string zipCode);
    }
}
