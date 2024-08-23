using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Stripe_users_accounts_customers_settings")]
    public class StripeUsersAccountsCustomersSettings : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("customer_id")]
        public string CustomerId { get; set; }

        [FieldInfo("payment_method")]
        public int PaymentMethod { get; set; }

        [FieldInfo("payment_days_after")]
        public int PaymentDaysAfter { get; set; }

        [FieldInfo("first_notification_days_before")]
        public int FirstNotificationDaysBefore { get; set; }

        [FieldInfo("second_notification_days_before")]
        public int SecondNotificationDaysBefore { get; set; }

        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }

        [FieldInfo("is_active")]
        public int IsActive { get; set; }
    }
}