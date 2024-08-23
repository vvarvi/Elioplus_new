using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WdS.ElioPlus.Lib.Services.GoogleGeolocationAPI
{
    public class MultipleGeolocationProvider : IGeolocationProvider
    {

        #region IGeolocationProvider Members

        public System.Xml.Linq.XDocument resolveAddress(string addressQuery)
        {
            return resolveAddress(addressQuery, "", false, false);
        }

        public System.Xml.Linq.XDocument resolveAddress(string addressQuery, string CountryCode)
        {
            return resolveAddress(addressQuery, CountryCode, false, false);
        }

        public System.Xml.Linq.XDocument resolveAddress(string addressQuery, string CountryCode, bool SkipCountry)
        {
            return resolveAddress(addressQuery, CountryCode, SkipCountry, false);
        }

        #endregion

        #region IGeolocationProvider Members


        public System.Xml.Linq.XDocument resolveAddress(string addressQuery, string CountryCode, bool SkipCountry, bool IsKeyword)
        {
            GoogleGeolocationProvider ggp = new GoogleGeolocationProvider();
            //WebAtesGeolocationProvider wagp = new WebAtesGeolocationProvider();

            //if (!IsKeyword) {
            XDocument google = ggp.resolveAddress(addressQuery, CountryCode);
            String googleResult = google.Element("NewDataSet").Element("QueryStatus").Value;
            if (googleResult == "ok")
            {
                return google;
            }
            else if (googleResult.ToUpper() == "ZERO_RESULTS")
            {
                google = ggp.resolveAddress(addressQuery, CountryCode, true);
                googleResult = google.Element("NewDataSet").Element("QueryStatus").Value;
                if (googleResult == "ok" || googleResult == "CONNECTION_ERROR")
                    return google;
            }
            else if (googleResult == "CONNECTION_ERROR")
            {
                return google;
            }

            return google;

            //}

            //XDocument webAtes = wagp.resolveAddress(addressQuery, CountryCode);

            //if (webAtes.Element("NewDataSet").Element("Table") != null
            //    && webAtes.Element("NewDataSet").Element("Table").Element("LONGITUDE") != null
            //    && webAtes.Element("NewDataSet").Element("Table").Element("LATITUDE") != null)
            //{
            //    if (!String.IsNullOrWhiteSpace(webAtes.Element("NewDataSet").Element("Table").Element("LATITUDE").Value) &&
            //        !String.IsNullOrWhiteSpace(webAtes.Element("NewDataSet").Element("Table").Element("LONGITUDE").Value))
            //    { return webAtes; }
            //}

            //return webAtes;
        }

        #endregion
    }
}