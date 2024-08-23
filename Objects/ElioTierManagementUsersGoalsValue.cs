using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_tier_management_users_goals_value")]
    public class ElioTierManagementUsersGoalsValue : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("goal_id")]
        public int GoalId { get; set; }

        [FieldInfo("goal_value")]
        public string GoalValue { get; set; }

        [FieldInfo("user_goal_value")]
        public string UserGoalValue { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}