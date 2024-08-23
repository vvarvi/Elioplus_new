using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Paypal_credentials")]
    public class PaypalCredentials : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("username")]
        public string Username { get; set; }

        [FieldInfo("paypal_password")]
        public string PaypalPassword { get; set; }

        [FieldInfo("paypal_signature")]
        public string PaypalSignature { get; set; }

        [FieldInfo("version")]
        public string Version { get; set; }

        [FieldInfo("currency_code")]
        public string CurrencyCode { get; set; }

        [FieldInfo("locale_code")]
        public string LocaleCode { get; set; }

        [FieldInfo("payment_action")]
        public string PaymentAction { get; set; }

        [FieldInfo("customer_service_number")]
        public string CustomerServiceNumber { get; set; }

        [FieldInfo("hdr_back_color")]
        public string Hdrbackcolor { get; set; }

        [FieldInfo("hdr_border_color")]
        public string Hdrbordercolor { get; set; }

        [FieldInfo("hdr_img")]
        public string Hdrimg { get; set; }

        [FieldInfo("no_shipping")]
        public string Noshipping { get; set; }

        [FieldInfo("allow_note")]
        public string AllowNote { get; set; }

        [FieldInfo("gift_message_enable")]
        public string GiftMessageEnable { get; set; }

        [FieldInfo("gift_receipt_enable")]
        public string GiftReceiptEnable { get; set; }

        [FieldInfo("gift_wrap_enable")]
        public string GiftWrapEnable { get; set; }

        [FieldInfo("l_name")]
        public string LName { get; set; }

        [FieldInfo("l_desc")]
        public string LDesc { get; set; }
    }
}