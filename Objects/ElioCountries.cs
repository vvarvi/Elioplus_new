using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_countries")]
    public class ElioCountries : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("country_name")]
        public string CountryName { get; set; }

        [FieldInfo("capital")]
        public string Capital { get; set; }

        [FieldInfo("prefix")]
        public string Prefix { get; set; }

        [FieldInfo("iso2")]
        public string Iso2 { get; set; }

        [FieldInfo("iso3")]
        public string Iso3 { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("region")]
        public string Region { get; set; }
    }
}