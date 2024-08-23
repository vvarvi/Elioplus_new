using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Stripe_users_accounts_products")]
    public class StripeUsersAccountsProducts : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("stripe_account_id")]
        public string StripeAccountId { get; set; }

        [FieldInfo("stripe_customer_id")]
        public string StripeCustomerId { get; set; }

        [FieldInfo("product_id")]
        public string ProductId { get; set; }

        [FieldInfo("name")]
        public string Name { get; set; }

        [FieldInfo("type")]
        public string Type { get; set; }

        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }

        [FieldInfo("status")]
        public string Status { get; set; }

        [FieldInfo("elio_service_id")]
        public int ElioServiceId { get; set; }
    }
}