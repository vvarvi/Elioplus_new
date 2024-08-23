using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_intent_signals_data")]
    public class ElioIntentSignalsData : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("type")]
        public string Type { get; set; }       

        [FieldInfo("country")]
        public string Country { get; set; }

        [FieldInfo("city")]
        public string City { get; set; }

        [FieldInfo("product")]
        public string Product { get; set; }

        [FieldInfo("users_count")]
        public int UsersCount { get; set; }

        [FieldInfo("insert_by_user_id")]
        public int InsertByUserId { get; set; }

        [FieldInfo("date_insert")]
        public DateTime DateInsert { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}