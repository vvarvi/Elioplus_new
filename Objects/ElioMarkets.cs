using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_markets")]
    public class ElioMarkets : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("market_description")]
        public string MarketDescription { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}