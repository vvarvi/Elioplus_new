using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_tier_management_goals_custom")]
    public class ElioTierManagementGoalsCustom : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }

        [FieldInfo("date_from")]
        public DateTime DateFrom { get; set; }

        [FieldInfo("date_to")]
        public DateTime DateTo { get; set; }
    }
}