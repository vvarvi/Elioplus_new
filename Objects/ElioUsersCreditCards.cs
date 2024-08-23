using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_users_credit_cards
    /// </summary>
    [ClassInfo("Elio_users_credit_cards")]
    public partial class ElioUsersCreditCards  : DataObject
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
        /// Database Field : customer_stripe_id
        /// </summary>
        [FieldInfo("customer_stripe_id")]
        public string CustomerStripeId { get; set; }
        /// <summary>
        /// Database Field : card_stripe_id
        /// </summary>
        [FieldInfo("card_stripe_id")]
        public string CardStripeId { get; set; }
        /// <summary>
        /// Database Field : card_fullname
        /// </summary>
        [FieldInfo("card_fullname")]
        public string CardFullname { get; set; }
        /// <summary>
        /// Database Field : address_1
        /// </summary>
        [FieldInfo("address_1")]
        public string Address1 { get; set; }
        /// <summary>
        /// Database Field : address_2
        /// </summary>
        [FieldInfo("address_2")]
        public string Address2 { get; set; }
        /// <summary>
        /// Database Field : origin
        /// </summary>
        [FieldInfo("origin")]
        public string Origin { get; set; }
        /// <summary>
        /// Database Field : card_type
        /// </summary>
        [FieldInfo("card_type")]
        public string CardType { get; set; }
        /// <summary>
        /// Database Field : cvc_check
        /// </summary>
        [FieldInfo("cvc_check")]
        public string CvcCheck { get; set; }
         /// <summary>
        /// Database Field : fingerprint
        /// </summary>
        [FieldInfo("fingerprint")]
        public string Fingerprint { get; set; }        
        /// <summary>
        /// Database Field : exp_month
        /// </summary>
        [FieldInfo("exp_month")]
        public int ExpMonth { get; set; }
        /// <summary>
        /// Database Field : exp_year
        /// </summary>
        [FieldInfo("exp_year")]
        public int ExpYear { get; set; }
        /// <summary>
        /// Database Field : zip_code
        /// </summary>
        [FieldInfo("zip_code")]
        public string ZipCode { get; set; }
        /// <summary>
        /// Database Field : zip_check
        /// </summary>
        [FieldInfo("zip_check")]
        public string ZipCheck { get; set; }
        /// <summary>
        /// Database Field : is_default
        /// </summary>
        [FieldInfo("is_default")]
        public int IsDefault { get; set; }
        /// <summary>
        /// Database Field : is_deleted
        /// </summary>
        [FieldInfo("is_deleted")]
        public int IsDeleted { get; set; }
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
