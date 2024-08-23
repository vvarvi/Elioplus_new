using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_stripe_id")]
    public class ElioUsersStripeId : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : user_id
        /// </summary>
        [FieldInfo("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// Database Field : stripe_service_customer_id
        /// </summary>
        [FieldInfo("stripe_service_customer_id")]
        public string StripeServiceCustomerId { get; set; }
        /// <summary>
        /// Database Field : stripe_service_customer_subscription_email
        /// </summary>
        [FieldInfo("stripe_service_customer_subscription_email")]
        public string StripeServiceCustomerSubscriptionEmail { get; set; }
        /// <summary>
        /// Database Field : stripe_packet_customer_id
        /// </summary>
        [FieldInfo("stripe_packet_customer_id")]
        public string StripePacketCustomerId { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
        /// <summary>
        /// Database Field : last_updated
        /// </summary>
        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}