using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.Enums;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.Models;
using Newtonsoft.Json.Linq;

namespace WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.Helpers
{
    public static class RequestHelper
    {
        public const string FreeBaseUrl = "https://free.currencyconverterapi.com/api/v6/";
        public const string PremiumBaseUrl = "https://api.currencyconverterapi.com/api/v6/";    //https://api.currconv.com/api/v7/

        public static List<Currency> GetAllCurrencies(string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "currencies";
            else
                url = PremiumBaseUrl + "currencies" + "?apiKey=" + apiKey;

            var jsonString = GetResponse(url);

            var data = JObject.Parse(jsonString)["results"].ToArray();
            return data.Select(item => item.First.ToObject<Currency>()).ToList();
        }

        public static List<Country> GetAllCountries(string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "countries";
            else
                url = PremiumBaseUrl + "countries" + "?apiKey=" + apiKey;

            var jsonString = GetResponse(url);

            var data = JObject.Parse(jsonString)["results"].ToArray();

            return data.Select(item => item.First.ToObject<Country>()).ToList();
        }

        public static List<CurrencyHistory> GetHistoryRange(CurrencyType from, CurrencyType to, string startDate, string endDate, string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + startDate + "&endDate=" + endDate;
            else
                url = PremiumBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + startDate + "&endDate=" + endDate + "&apiKey=" + apiKey;

            var jsonString = GetResponse(url);
            var data = JObject.Parse(jsonString).First.ToArray();
            return (from item in data
                    let obj = (JObject)item
                    from prop in obj.Properties()
                    select new CurrencyHistory
                    {
                        Date = prop.Name,
                        ExchangeRate = item[prop.Name].ToObject<double>()
                    }).ToList();
        }

        public static CurrencyHistory GetHistory(CurrencyType from, CurrencyType to, string date, string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + date;
            else
                url = PremiumBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + date + "&apiKey=" + apiKey;

            var jsonString = GetResponse(url);
            var data = JObject.Parse(jsonString);
            return data.Properties().Select(prop => new CurrencyHistory
            {
                Date = prop.Name,
                ExchangeRate = data[prop.Name][date].ToObject<double>()
            }).FirstOrDefault();
        }

        public static double ExchangeRate(CurrencyType from, CurrencyType to, string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "convert?q=" + from + "_" + to + "&compact=y";
            else
                url = PremiumBaseUrl + "convert?q=" + from + "_" + to + "&compact=y&apiKey=" + apiKey;

            var jsonString = GetResponse(url);

            //var jsonData = JObject.Parse(jsonString).Children();
            JObject jo = JObject.Parse(jsonString);
            
            //List<JToken> tokens = jsonData.Children().ToList();

            string result = (from + "_" + to).ToUpper();
            JToken tn = jo[result];
            string val = tn.ToString();

            return val != "" ? Convert.ToDouble(val) : 0;
            //return JObject.Parse(jsonString).First["val"].ToObject<double>();
        }

        public static double ExchangeRate(string from, string to, string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "convert?q=" + from + "_" + to + "&compact=y";
            else
                url = PremiumBaseUrl + "convert?q=" + from + "_" + to + "&compact=y&apiKey=" + apiKey;

            var jsonString = GetResponse(url);

            var jsonData = JObject.Parse(jsonString).Children();
            //JObject jo = JObject.Parse(jsonString);
            //dynamic d = JObject.Parse(jsonString);
            List<JToken> tokens = jsonData.Children().ToList();
            string newValue = "";

            foreach (JProperty prop in tokens.Children())
            {
                JToken vale = prop.Value;
                if (vale != null)
                {
                    newValue = vale.ToString();
                    if (newValue != "")
                        break;
                    //if (vale.Type == JTokenType.Object)
                    //{
                    //    string vv = ((JObject)vale).GetValue("val").ToString();
                    //}

                    //string vaa = JObject.Parse(prop.ToString()).Property("val").ToString();
                }
            }

            //foreach (JProperty currProperty in JObject.Parse(jsonString).Children().OfType<JProperty>())
            //{
            //    JToken currElement = currProperty.Value;
            //    JToken gIdValue = currElement.SelectToken("val");
            //    if (gIdValue.Value<string>() != "")
            //    {
            //        newValue = gIdValue.ToString();
            //        break;
            //    }
            //}

            //JToken  token = tokens[0].First["val"];
            //string value = token.ToString();

            //string result = (from + "_" + to).ToUpper();
            ////JToken tn = d[result].First["val"];
            //JToken tkn = d[result];
            //string val = tkn.ToString();

            if (newValue != "")
                return Convert.ToDouble(newValue);
            else
                return 0;

            ////return val != "" ? Convert.ToDouble(val) : 0;
            //return JObject.Parse(jsonString).First["val"].ToObject<double>();
        }

        private static string GetResponse(string url)
        {
            string jsonString;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }

            return jsonString;
        }
    }
}
