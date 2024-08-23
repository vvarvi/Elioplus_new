using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_connections")]
    public class ElioUsersConnections : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("connection_id")]
        public int ConnectionId { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }

        [FieldInfo("can_be_viewed")]
        public int CanBeViewed { get; set; }

        [FieldInfo("current_period_start")]
        public DateTime CurrentPeriodStart { get; set; }

        [FieldInfo("current_period_end")]
        public DateTime CurrentPeriodEnd { get; set; }

        [FieldInfo("status")]
        public bool Status { get; set; }

        [FieldInfo("is_new")]
        public int IsNew { get; set; }
    }
}