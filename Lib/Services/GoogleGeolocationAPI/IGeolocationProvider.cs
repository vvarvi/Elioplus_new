using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WdS.ElioPlus.Lib.Services.GoogleGeolocationAPI
{
    public interface IGeolocationProvider
    {
        XDocument resolveAddress(String addressQuery);
        XDocument resolveAddress(String addressQuery, String CountryCode);
        XDocument resolveAddress(String addressQuery, String CountryCode, bool SkipCountry);
        XDocument resolveAddress(String addressQuery, String CountryCode, bool SkipCountry, bool IsKeyword);
    }
}