using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Stripe_testing_cards
    /// </summary>
    [ClassInfo("Stripe_testing_cards")]
    public partial class StripeTestingCards : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : card_number
        /// </summary>
        [FieldInfo("card_number")]
        public string CardNumber { get; set; }
        /// <summary>
        /// Database Field : card_cvc
        /// </summary>
        [FieldInfo("card_cvc")]
        public string CardCvc { get; set; }
        /// <summary>
        /// Database Field : card_type
        /// </summary>
        [FieldInfo("card_type")]
        public string CardType { get; set; }
        /// <summary>
        /// Database Field : card_expiration_month
        /// </summary>
        [FieldInfo("card_expiration_month")]
        public string CardExpirationMonth { get; set; }
        /// <summary>
        /// Database Field : card_expiration_year
        /// </summary>
        [FieldInfo("card_expiration_year")]
        public string CardExpirationYear { get; set; }
        /// <summary>
        /// Database Field : description
        /// </summary>
        [FieldInfo("description")]
        public string Description { get; set; }
        /// <summary>
        /// Database Field : is_used
        /// </summary>
        [FieldInfo("is_used")]
        public bool IsUsed { get; set; }
    }
}