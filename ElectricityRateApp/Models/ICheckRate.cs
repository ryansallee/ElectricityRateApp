using System;

namespace ElectricityRateApp.Models
{
    //Interface for classes that obtain Rates to check if no rate is obtained.
    //Allows for Polymorphism
    //When this interface is implemented, it must take a type.
    public interface ICheckRate<T>
    { 
        // Method that returns false if the Rate is 0 and prevents its parent method from executing
        // further code. 
        bool CheckIfRate0(T t);
    }
}
