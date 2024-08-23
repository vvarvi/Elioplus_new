using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Stripe_users_checkout_sessions")]
    public class StripeUsersCheckoutSessions : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("checkout_session_id")]
        public string CheckoutSessionId { get; set; }

        [FieldInfo("checkout_url")]
        public string CheckoutUrl { get; set; }

        [FieldInfo("stripe_plan_id")]
        public string StripePlanId { get; set; }

        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_expired")]
        public int IsExpired { get; set; }
    }
}