using Newtonsoft.Json;

namespace WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.Models
{
    public class Currency
    {
        [JsonProperty("currencyName")]
        public string CurrencyName { get; set; }

        [JsonProperty("currencySymbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
