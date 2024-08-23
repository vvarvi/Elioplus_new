using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_financial_incomes
    /// </summary>
    [ClassInfo("Elio_financial_income")]
    public partial class ElioFinancialIncome  : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : income_amount
        /// </summary>
        [FieldInfo("income_amount")]
        public Decimal IncomeAmount { get; set; }
        /// <summary>
        /// Database Field : admin_name
        /// </summary>
        [FieldInfo("admin_name")]
        public string AdminName { get; set; }
        /// <summary>
        /// Database Field : admin_id
        /// </summary>
        [FieldInfo("admin_id")]
        public int AdminId { get; set; }
        /// <summary>
        /// Database Field : income_source
        /// </summary>
        [FieldInfo("income_source")]
        public string IncomeSource { get; set; }
        /// <summary>
        /// Database Field : organization
        /// </summary>
        [FieldInfo("organization")]
        public string Organization { get; set; }
        /// <summary>
        /// Database Field : comments
        /// </summary>
        [FieldInfo("comments")]
        public string Comments { get; set; }
        /// <summary>
        /// Database Field : datein
        /// </summary>
        [FieldInfo("datein")]
        public DateTime Datein { get; set; }
        /// <summary>
        /// Database Field : last_updated
        /// </summary>
        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
        /// <summary>
        /// Database Field : vat
        /// </summary>
        [FieldInfo("vat")]
        public Decimal Vat { get; set; }
        /// <summary>
        /// Database Field : vat_amount
        /// </summary>
        [FieldInfo("vat_amount")]
        public Decimal VatAmount { get; set; }
        /// <summary>
        /// Database Field : income_amount_with_no_vat
        /// </summary>
        [FieldInfo("income_amount_with_no_vat")]
        public Decimal IncomeAmountWithNoVat { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
        /// <summary>
        /// Database Field : last_edit_user_id
        /// </summary>
        [FieldInfo("last_edit_user_id")]
        public int LastEditUserId { get; set; }        
    }
}
