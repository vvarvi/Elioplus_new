using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Stripe_users_accounts")]
    public class StripeUsersAccounts : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("stripe_account_id")]
        public string StripeAccountId { get; set; }

        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }

        [FieldInfo("status")]
        public string Status { get; set; }

        [FieldInfo("email")]
        public string Email { get; set; }

        [FieldInfo("account_type")]
        public string AccountType { get; set; }

        [FieldInfo("country_iso")]
        public string CountryIso { get; set; }

        [FieldInfo("bussiness_type")]
        public string BussinessType { get; set; }
    }
}