using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus.Lib.Services.GoogleGeolocationAPI
{
    class GoogleGeolocationProvider : IGeolocationProvider
    {
        public XDocument resolveAddress(string addressQuery)
        {
            return resolveAddress(addressQuery, "", false, false);
        }

        public XDocument resolveAddress(string addressQuery, string CountryCode)
        {
            return resolveAddress(addressQuery, CountryCode, false, false);
        }

        public XDocument resolveAddress(string addressQuery, string CountryCode, bool skipCountry)
        {
            return resolveAddress(addressQuery, CountryCode, skipCountry, false);
        }

        public XDocument resolveAddress(string addressQuery, string CountryCode, bool skipCountry, bool IsKeyword)
        {
            //Remove blacklisted words
            //addressQuery = addressQuery.RemoveInvalidCharacters().RemoveBlackListedWord();

            //url encode the address so that google can understand it.
            String escapedAddress = Uri.EscapeDataString(addressQuery);
            XDocument resolvedAddress = new XDocument();
            String googleUrl = "";
            if (!skipCountry) googleUrl = getGoogleUrl(escapedAddress, CountryCode);
            if (skipCountry) googleUrl = getGoogleUrl(escapedAddress, "");
            try
            {
                resolvedAddress = XDocument.Parse(getRequest(googleUrl));
            }
            catch// (Exception ex)
            {
                String error = "function: GoogleGeolocationProvider.resolveAddress. Possible time out...";                
                Logger.DetailedError("WdS.ElioPlus.Lib.Services.GoogleGeolocationAPI.GoogleGeolocationProvider.cs", error);
                resolvedAddress = XDocument.Parse("<GeocodeResponse><status>CONNECTION_ERROR</status></GeocodeResponse>");

            }

            XDocument formatedResponce = ParseGoogleResponce(resolvedAddress);

            return formatedResponce;
        }

        XDocument ParseGoogleResponce(XDocument googleResponce)
        {
            XDocument parsed = new XDocument();

            XElement NewDataSet = new XElement("NewDataSet");
            XElement Table = new XElement("Table");
            XElement QueryStatus = new XElement("QueryStatus");
            XElement FORMATED_ADDRESS = new XElement("FORMATED_ADDRESS");
            XElement COUNTRY_CODE = new XElement("COUNTRY_CODE");
            XElement COUNTRY_NAME = new XElement("COUNTRY_NAME");
            XElement CITY = new XElement("CITY");
            XElement LATITUDE = new XElement("LATITUDE");
            XElement LONGITUDE = new XElement("LONGITUDE");
            XElement CONFIDENCE = new XElement("CONFIDENCE");
            XElement PROVIDER = new XElement("PROVIDER");

            parsed.Add(NewDataSet);
            NewDataSet.Add(Table);
            NewDataSet.Add(QueryStatus);
            Table.Add(FORMATED_ADDRESS);
            Table.Add(COUNTRY_CODE);
            Table.Add(COUNTRY_NAME);
            Table.Add(CITY);
            Table.Add(LATITUDE);
            Table.Add(LONGITUDE);
            Table.Add(CONFIDENCE);
            Table.Add(PROVIDER);

            var GeocodeResponse = googleResponce.Element("GeocodeResponse");
            var status = googleResponce.Element("GeocodeResponse").Element("status");

            XElement result = new XElement("result");

            XElement formatedAddress = new XElement("formatedAddress");
            List<XElement> city = new List<XElement>();
            List<XElement> country = new List<XElement>();
            XElement lat = new XElement("lat");
            XElement lng = new XElement("lng");
            XElement location_type = new XElement("location_type");

            if (status.Value.ToLower() == "ok")
            {
                result = GeocodeResponse.Elements("result").First();
                formatedAddress = result.Element("formatted_address");
                lat = result.Element("geometry").Element("location").Element("lat");
                lng = result.Element("geometry").Element("location").Element("lng");
                location_type = result.Element("geometry").Element("location_type");
                city = (from cit in result.Elements("address_component")
                        where cit.Elements("type").Count() > 0 && cit.Elements("type").ElementAt(0).Value == "locality" && cit.Elements("type").ElementAt(1).Value == "political"
                        select cit).ToList();
                country = (from coun in result.Elements("address_component")
                           where coun.Elements("type").Count() > 0 && coun.Elements("type").ElementAt(0).Value == "country" && coun.Elements("type").ElementAt(1).Value == "political"
                           select coun).ToList();

                FORMATED_ADDRESS.Value = (formatedAddress != null ? formatedAddress.Value : "");
                COUNTRY_CODE.Value = (country.Count > 0 ? country[0].Element("short_name").Value : "");
                COUNTRY_NAME.Value = (country.Count > 0 ? country[0].Element("long_name").Value : "");
                CITY.Value = (city.Count > 0 ? city[0].Element("long_name").Value : "");
                LATITUDE.Value = (lat != null ? lat.Value : "");
                LONGITUDE.Value = (lng != null ? lng.Value : "");
                CONFIDENCE.Value = (location_type != null ? location_type.Value : "");
                PROVIDER.Value = "GoogleGeolocation";
                QueryStatus.Value = "ok";
            }

            else if (status.Value.ToLower() == "connection_error")
            {
                QueryStatus.Value = "connection_error";
            }
            else
            {
                QueryStatus.Value = "zero_results";
            }

            return parsed;
        }

        static string getGoogleUrl(string adrressQuery, string countryCode)
        {
            if (adrressQuery.ToLower().Contains(countryCode.ToLower())) countryCode = "";
            if (countryCode == "")
            {
                //return "http://maps.googleapis.com/maps/api/geocode/xml?oe=utf-8&address=" + adrressQuery + "&sensor=false";//without key
                return "https://maps.googleapis.com/maps/api/geocode/xml?oe=utf-8&key=AIzaSyBqioip44VZCxNC1YmJfjOEcBUu3y6SrWo&address=" + adrressQuery + "&sensor=false";//with key
            }
            else
            {
                //return "http://maps.googleapis.com/maps/api/geocode/xml?oe=utf-8&address=" + adrressQuery + " " + countryCode + "&sensor=false";//without key
                return "https://maps.googleapis.com/maps/api/geocode/xml?oe=utf-8&key=AIzaSyBqioip44VZCxNC1YmJfjOEcBUu3y6SrWo&address=" + adrressQuery + " " + countryCode + "&sensor=false";//with key
            }
        }

        static string getRequest(String url)
        {
            WebClient client = new WebClient();
            string responce = "";
            // Add a user agent header in case the 
            // requested URI contains a query.

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            try
            {
                Stream data = client.OpenRead(url);
                StreamReader reader = new StreamReader(data);
                responce = reader.ReadToEnd();
                data.Close();
                reader.Close();
            }
            catch //(WebException ex)
            {
                Logger.DetailedError("WdS.ElioPlus.Lib.Services.GoogleGeolocationAPI.GoogleGeolocationProvider.cs", "WebException in utils.getRequest");
                
                //throw ex;
            }

            return responce;
        }
    }
}