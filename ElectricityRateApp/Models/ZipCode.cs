using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElectricityRateApp.Models
{
    // Class to model 
    public class ZipCode
    {
        [JsonProperty(PropertyName = "zip_codes")]
         public string[] ZipCodes{ get; set; }

        //Method to populate the property of the ZipCode model. This method is a helper
        //to the Get() methods of RateCommparisonResult ResidentialChargeResult, and UtilitySearchResult
        //to convert a city name and state to the first zipcode for that locale.
        public static async Task<string> GetZipCode(string city, string stateAbbreviation)
        {
            using (var httpClient = new HttpClient())
            {
                string apiKey = "vMGbJYN2IJf2EbjVN2h8bERvkD55ZwNuFcfSWzMtxwPKdi6t0dCV0k6LgZp127rG";

                var responseMessage = await httpClient.GetAsync("https://www.zipcodeapi.com/rest/" + apiKey + "/city-zips.json/" + city + "/" + stateAbbreviation);

                if (!responseMessage.IsSuccessStatusCode)
                    throw new System.Exception("Unable to connect to ZipCodeApi to obtain city zipCode");

                string responseString = await responseMessage.Content.ReadAsStringAsync();

                ZipCode zipCodesResult = JsonConvert.DeserializeObject<ZipCode>(responseString);

                if (zipCodesResult.ZipCodes.Length < 1)
                    return null;
                else
                    return zipCodesResult.ZipCodes[0];
            }
        }

    }
}


