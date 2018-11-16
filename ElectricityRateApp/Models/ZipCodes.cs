using Newtonsoft.Json;

namespace ElectricityRateApp.Models
{
    // Class to model to ZipCodes
    public class ZipCodes
    {
        [JsonProperty(PropertyName = "zip_codes")]
        public string[] ZipCodesArr { get; set; }

   
    }

}



