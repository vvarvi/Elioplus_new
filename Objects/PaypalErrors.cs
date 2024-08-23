using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Paypal_errors
    /// </summary>
    [ClassInfo("Paypal_errors")]
    public partial class PaypalErrors  : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : error
        /// </summary>
        [FieldInfo("error")]
        public string Error { get; set; }
        /// <summary>
        /// Database Field : ip
        /// </summary>
        [FieldInfo("ip")]
        public string Ip { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
    }
}
