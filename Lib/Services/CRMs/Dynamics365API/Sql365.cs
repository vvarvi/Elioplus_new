using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.DB;

namespace WdS.ElioPlus.Lib.Services.CRMs.Dynamics365API
{
    public class Sql365
    {
        public static string GetCrmDealAccountId(int dealId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select ISNULL(crm_account_id,'') as crm_account_id
                                                        from Elio_crm_deal_contacts_365 
                                                        where elio_deal_id = @elio_deal_id
                                                        and is_active = 1"
                                                    , DatabaseHelper.CreateIntParameter("@elio_deal_id", dealId));

            return (table.Rows.Count > 0) ? table.Rows[0]["crm_account_id"].ToString() : "";
        }

        public static string GetCrmDealContactId(int dealId, string dealEmail, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select ISNULL(crm_contact_id,'') as crm_contact_id
                                                        from Elio_crm_deal_contacts_365 
                                                        where deal_email = @deal_email and elio_deal_id = @elio_deal_id
                                                        and is_active = 1"
                                                    , DatabaseHelper.CreateStringParameter("@deal_email", dealEmail)
                                                    , DatabaseHelper.CreateIntParameter("@elio_deal_id", dealId));

            return (table.Rows.Count > 0) ? table.Rows[0]["crm_contact_id"].ToString() : "";
        }

        public static string GetUserCrmDealIdByElioDealId(int elioDealId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT ISNULL(crm_deal_id, '') as crm_deal_id
                                                        FROM Elio_crm_user_deals
                                                        where deal_id = @deal_id"
                                                     , DatabaseHelper.CreateIntParameter("@deal_id", elioDealId));

            return (table.Rows.Count > 0) ? table.Rows[0]["crm_deal_id"].ToString() : "";
        }
    }
}