namespace ElectricityRateApp.Models
{
    // Class to model PowerRates that are deserialized from the iouzipcodes2016 CSV.
    // Private set is used as an example of encapsulation(but this is an example of
    // overengineering as well.
    public class PowerRate
    {
        //Public constructor to allow the AddPowerRates method to set the properties of PowerRate.
        public PowerRate(string zipCode, string utilityName, double residentialRate)
        {
            ZipCode = zipCode;
            UtilityName = utilityName;
            ResidentialRate = residentialRate;
        }
        public int Id { get; set; }
        public string ZipCode { get; private set; }        
        public string UtilityName { get; private set; }
        public double ResidentialRate { get; private set; }
    }
}
