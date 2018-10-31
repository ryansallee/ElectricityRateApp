using ElectricityRateApp.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace ElectricityRateApp.Data
{
    public static class ZipCodeMethods
    {
        public static HttpClient HttpClient;
        public static async Task<string> GetZipCode(string city, string stateAbbreviation)
        {
            if (HttpClient == null)
                HttpClient = new HttpClient();

            
            var responseMessage = await HttpClient.GetAsync("https://www.zipcodeapi.com/rest/vMGbJYN2IJf2EbjVN2h8bERvkD55ZwNuFcfSWzMtxwPKdi6t0dCV0k6LgZp127rG/city-zips.json/" + city + "/" + stateAbbreviation);

            if (!responseMessage.IsSuccessStatusCode)
                throw new System.Exception("Unable to connect to ZipCodeApi to obtain city zipCode");

            var responseString = await responseMessage.Content.ReadAsStringAsync();

            var zipCodesResult = JsonConvert.DeserializeObject<ZipCode>(responseString);

            if (zipCodesResult.ZipCodes.Length < 1)
            {
                return null;
            }
            else
            {
                return zipCodesResult.ZipCodes[0];
            }    

        } 

    }
}
