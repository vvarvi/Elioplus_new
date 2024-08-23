using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Stripe_users_accounts_products_prices")]
    public class StripeUsersAccountsProductsPrices : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("account_product_id")]
        public int AccountProductId { get; set; }

        [FieldInfo("stripe_product_id")]
        public string StripeProductId { get; set; }

        [FieldInfo("price_id")]
        public string PriceId { get; set; }

        [FieldInfo("amount")]
        public decimal Amount { get; set; }

        [FieldInfo("currency")]
        public string Currency { get; set; }

        [FieldInfo("nick_name")]
        public string NickName { get; set; }

        [FieldInfo("payment_link_id")]
        public string PaymentLinkId { get; set; }

        [FieldInfo("payment_link")]
        public string PaymentLink { get; set; }

        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }

        [FieldInfo("status")]
        public string Status { get; set; }
    }
}