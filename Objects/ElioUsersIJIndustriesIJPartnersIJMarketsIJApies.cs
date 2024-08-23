﻿using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioUsersIJIndustriesIJPartnersIJMarketsIJApies : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
               
        [FieldInfo("company_name")]
        public string CompanyName { get; set; }

        [FieldInfo("overview")]
        public string Overview { get; set; }

        [FieldInfo("company_logo")]
        public string CompanyLogo { get; set; }

        [FieldInfo("country")]
        public string Country { get; set; }

        [FieldInfo("industry_description")]
        public string IndustryDescription { get; set; }

        [FieldInfo("partner_description")]
        public string PartnerDescription { get; set; }

        [FieldInfo("market_description")]
        public string MarketDescription { get; set; }

        [FieldInfo("api_description")]
        public string ApiDescription { get; set; }
    }
}