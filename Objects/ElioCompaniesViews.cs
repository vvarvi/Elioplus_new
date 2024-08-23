using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_companies_views")]
    public class ElioCompaniesViews : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("company_id")]
        public int CompanyId { get; set; }       

        [FieldInfo("views")]
        public int Views { get; set; }

        [FieldInfo("date")]
        public DateTime Date { get; set; }    
    }
}