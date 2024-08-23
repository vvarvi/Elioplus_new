using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Stripe_users_accounts_customers")]
    public class StripeUsersAccountsCustomers : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("account_id")]
        public int AccountId { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("stripe_account_id")]
        public string StripeAccountId { get; set; }

        [FieldInfo("stripe_customer_id")]
        public string StripeCustomerId { get; set; }

        [FieldInfo("customer_email")]
        public string CustomerEmail { get; set; }

        [FieldInfo("payment_method_type")]
        public int PaymentMethodType { get; set; }

        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }

        [FieldInfo("status")]
        public string Status { get; set; }
    }
}