using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityRateApp.Models
{
    public class ProviderSearchResult
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public string ProviderName { get; set; }
    }
}
