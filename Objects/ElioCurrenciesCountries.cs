using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_currencies
    /// </summary>
    [ClassInfo("Elio_currencies_countries")]
    public partial class ElioCurrenciesCountries : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : cur_id
        /// </summary>
        [FieldInfo("cur_id")]
        public string CurId { get; set; }
        /// <summary>
        /// Database Field : currency_id
        /// </summary>
        [FieldInfo("currency_id")]
        public string CurrencyId { get; set; }
        /// <summary>
        /// Database Field : alpha3
        /// </summary>
        [FieldInfo("alpha3")]
        public string Alpha3 { get; set; }
        /// <summary>
        /// Database Field : currency_name
        /// </summary>
        [FieldInfo("currency_name")]
        public string CurrencyName { get; set; }
        /// <summary>
        /// Database Field : currency_symbol
        /// </summary>
        [FieldInfo("currency_symbol")]
        public string CurrencySymbol { get; set; }        
        /// <summary>
        /// Database Field : name
        /// </summary>
        [FieldInfo("name")]
        public string Name { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
        /// <summary>
        /// Database Field : last_update
        /// </summary>
        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}
