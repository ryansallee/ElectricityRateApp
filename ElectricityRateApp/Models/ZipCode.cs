﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElectricityRateApp.Models
{
    // Class to model 
    public class ZipCode
    {
        [JsonProperty(PropertyName = "zip_codes")]
        public string[] ZipCodes { get; set; }

        //Method to populate the property of the ZipCode model. This method is a helper
        //to the Get() methods of RateCommparisonResult ResidentialChargeResult, and UtilitySearchResult
        //to get a zipcode from zipcodeapi.com for a given locale This allows the rate information to be searched
        //in the PowerRate table since a user may not know a zipcode for a locale.
        public static async Task<string> GetZipCode(string city, string stateAbbreviation)
        {
            using (var httpClient = new HttpClient())
            {
                string apiKey = "vMGbJYN2IJf2EbjVN2h8bERvkD55ZwNuFcfSWzMtxwPKdi6t0dCV0k6LgZp127rG";

                var responseMessage = await httpClient.GetAsync("https://www.zipcodeapi.com/rest/" + apiKey + "/city-zips.json/" + city + "/" + stateAbbreviation);
                
                if (!responseMessage.IsSuccessStatusCode)
                    throw new System.Exception("Unable to obtain a ZIP code from ZipCodeApi and proceed with your request. \r\n" +
                        "Only 10 requests are allowed per hour. Please wait and try later.");           

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



