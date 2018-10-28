using Newtonsoft.Json;

namespace ElectricityRateApp.Models
{
    public class ZipCode
    {
        [JsonProperty(PropertyName = "zip_codes")]
         public string[] ZipCodes{ get; set; }
        

    }
}


