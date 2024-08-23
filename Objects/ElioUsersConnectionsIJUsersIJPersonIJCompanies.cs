using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    public class ElioUsersConnectionsIJUsersIJPersonIJCompanies
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

        [FieldInfo("is_new")]
        public int IsNew { get; set; }

        [FieldInfo("company_name")]
        public string CompanyName { get; set; }

        [FieldInfo("country")]
        public string Country { get; set; }

        [FieldInfo("website")]
        public string WebSite { get; set; }

        [FieldInfo("email")]
        public string Email { get; set; }

        [FieldInfo("official_email")]
        public string OfficialEmail { get; set; }

        [FieldInfo("company_logo")]
        public string CompanyLogo { get; set; }

        [FieldInfo("linkedin_url")]
        public string Linkedin { get; set; }

        [FieldInfo("billing_type")]
        public int BillingType { get; set; }

        [FieldInfo("user_application_type")]
        public int UserApplicationType { get; set; }

        [FieldInfo("avatar")]
        public string Avatar { get; set; }

        [FieldInfo("logo")]
        public string Logo { get; set; }
    }
}