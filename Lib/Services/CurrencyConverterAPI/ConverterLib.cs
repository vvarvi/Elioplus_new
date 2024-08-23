using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.Enums;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.Helpers;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.Models;

namespace WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.CurrencyConverter
{
    public class ConverterLib
    {
        private static string apiKey = System.Configuration.ConfigurationManager.AppSettings["CurrencyConvertKey"].ToString();

        public static double Convert(double amount, CurrencyType from, CurrencyType to)
        {
            return RequestHelper.ExchangeRate(from, to, apiKey) * amount;
        }

        public static double Convert(double amount, string from, string to)
        {
            return RequestHelper.ExchangeRate(from, to, apiKey) * amount;
        }

        public static async Task<double> ConvertAsync(double amount, CurrencyType from, CurrencyType to)
        {
            return await System.Threading.Tasks.Task.Run(() => Convert(amount, from, to));
        }

        public static List<Currency> GetAllCurrencies()
        {
            return RequestHelper.GetAllCurrencies(apiKey);
        }

        public static async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            return await System.Threading.Tasks.Task.Run(() => GetAllCurrencies());
        }

        public static List<Country> GetAllCountries()
        {
            return RequestHelper.GetAllCountries(apiKey);
        }

        public static async Task<List<Country>> GetAllCountriesAsync()
        {
            return await System.Threading.Tasks.Task.Run(() => GetAllCountries());
        }

        public static CurrencyHistory GetHistory(CurrencyType from, CurrencyType to, DateTime date)
        {
            return RequestHelper.GetHistory(from, to, date.ToString("yyyy-MM-dd"), apiKey);
        }

        public static async Task<CurrencyHistory> GetHistoryAsync(CurrencyType from, CurrencyType to, DateTime date)
        {
            return await System.Threading.Tasks.Task.Run(() => GetHistory(from, to, date.ToString("yyyy-MM-dd")));
        }

        public static CurrencyHistory GetHistory(CurrencyType from, CurrencyType to, string date)
        {
            return RequestHelper.GetHistory(from, to, date, apiKey);
        }

        public static async Task<CurrencyHistory> GetHistoryAsync(CurrencyType from, CurrencyType to, string date)
        {
            return await System.Threading.Tasks.Task.Run(() => GetHistory(from, to, date));
        }

        public static List<CurrencyHistory> GetHistoryRange(CurrencyType from, CurrencyType to, string startDate, string endDate)
        {
            return RequestHelper.GetHistoryRange(from, to, startDate, endDate, apiKey);
        }

        public static async Task<List<CurrencyHistory>> GetHistoryRangeAsync(CurrencyType from, CurrencyType to, string startDate, string endDate)
        {
            return await System.Threading.Tasks.Task.Run(() => GetHistoryRange(from, to, startDate, endDate));
        }

        public static List<CurrencyHistory> GetHistoryRange(CurrencyType from, CurrencyType to, DateTime startDate, DateTime endDate)
        {
            return GetHistoryRange(from, to, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
        }

        public static async Task<List<CurrencyHistory>> GetHistoryRangeAsync(CurrencyType from, CurrencyType to, DateTime startDate, DateTime endDate)
        {
            return await System.Threading.Tasks.Task.Run(() => GetHistoryRange(from, to, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));
        }
    }
}
