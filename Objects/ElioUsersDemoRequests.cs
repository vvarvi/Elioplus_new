using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_demo_requests")]
    public class ElioUsersDemoRequests : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("request_for_user_id")]
        public int RequestForUserId { get; set; }

        [FieldInfo("first_name")]
        public string FirstName { get; set; }

        [FieldInfo("last_name")]
        public string LastName { get; set; }

        [FieldInfo("company_name")]
        public string CompanyName { get; set; }

        [FieldInfo("company_email")]
        public string CompanyEmail { get; set; }

        [FieldInfo("company_size")]
        public string CompanySize { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("is_approved")]
        public int IsApproved { get; set; }

        [FieldInfo("date_approved")]
        public DateTime? DateApproved { get; set; }
    }
}