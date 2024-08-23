using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_tier_management_users_criteria_scores_custom")]
    public class ElioTierManagementUsersCriteriaScoresCustom : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("criteria_values_id")]
        public int CriteriaValuesId { get; set; }

        [FieldInfo("insert_user")]
        public int InsertUser { get; set; }

        [FieldInfo("update_user")]
        public int UpdateUser { get; set; }

        [FieldInfo("insert_date")]
        public DateTime InsertDate { get; set; }

        [FieldInfo("update_date")]
        public DateTime UpdateDate { get; set; }

        [FieldInfo("is_active")]
        public int IsActive { get; set; }
    }
}