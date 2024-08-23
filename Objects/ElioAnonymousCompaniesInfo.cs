using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_anonymous_companies_info")]
    public class ElioAnonymousCompaniesInfo : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("domain")]
        public string Domain { get; set; }

        [FieldInfo("city_name")]
        public string CityName { get; set; }

        [FieldInfo("country_name")]
        public string CountryName { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }

        [FieldInfo("industries")]
        public string Industries { get; set; }

        [FieldInfo("logo")]
        public string Logo { get; set; }

        [FieldInfo("phone_number")]
        public string PhoneNumber { get; set; }

        [FieldInfo("revenue")]
        public string Revenue { get; set; }

        [FieldInfo("linkedin")]
        public string Linkedin { get; set; }

        [FieldInfo("state_name")]
        public string StateName { get; set; }

        [FieldInfo("total_employees")]
        public string TotalEmployees { get; set; }

        [FieldInfo("year_founded")]
        public int YearFounded { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}