using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus
{
    public partial class Downloads : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!string.IsNullOrEmpty(Request.QueryString["case"]))
                {
                    if (Request.QueryString["case"].ToString() == "MyMatchesData")
                    {
                        #region MyMatchesData

                        if (vSession.ViewStateDataStoreForExport != null)
                        {
                            ExportLib.ExportDataTableToCSV(vSession.ViewStateDataStoreForExport, Request.QueryString["case"].ToString());
                        }
                        else
                        {

                        }

                        #endregion
                    }
                    else if (Request.QueryString["case"].ToString() == "MyCompanyData")
                    {
                        #region MyCompanyData

                        if (vSession.ViewStateDataStoreDS != null)
                        {
                            if (vSession.ViewStateDataStoreDS != null && vSession.ViewStateDataStoreDS.Tables.Count > 0 && vSession.ViewStateDataStoreDS.Tables[0].Rows.Count > 0)
                            {
                                ExportLib.ExportDataSetToCSV(vSession.ViewStateDataStoreDS, Request.QueryString["case"].ToString() + "_" + vSession.User.Id);
                            }
                        }
                        else
                        {

                        }

                        #endregion
                    }
                    else if (Request.QueryString["case"].ToString() == "StripeInvoices")
                    {
                        #region StripeInvoices

                        if (Request.QueryString["userID"].ToString() != "" && Request.QueryString["paymentID"].ToString() != "")
                        {
                            int userId = Convert.ToInt32(Request.QueryString["userID"].ToString());
                            int paymentId = Convert.ToInt32(Request.QueryString["paymentID"].ToString());

                            if (userId != 0 && paymentId != 0)
                            {
                                try
                                {
                                    if (session.Connection.State == ConnectionState.Closed)
                                        session.OpenConnection();

                                    string fileName = paymentId.ToString() + "invoice_export";

                                    ElioBillingUserOrdersPayments payment = Sql.GetBillingPaymentById(paymentId, session);
                                    if (payment != null)
                                    {
                                        //string[] parts = payment.ChargeId.Split('-').ToArray();
                                        //fileName = parts[0];
                                        fileName = payment.CurrentPeriodStart.Day.ToString() + "." + payment.CurrentPeriodStart.Month.ToString() + "." + payment.CurrentPeriodStart.Year.ToString() +
                                            "_"
                                            + payment.CurrentPeriodEnd.Day.ToString() + "." + payment.CurrentPeriodEnd.Month.ToString() + "." + payment.CurrentPeriodEnd.Year.ToString() + "_invoice_export";
                                    }

                                    byte[] b = InvoicesPdfGenerator.GetUserInvoiceByOrder(userId, paymentId, session);

                                    try
                                    {
                                        Response.Clear();
                                        Response.ContentType = "application/pdf";
                                        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
                                        Response.ContentType = "application/pdf";
                                        Response.Buffer = true;
                                        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                                        Response.BinaryWrite(b);
                                        //Response.Flush();
                                        Response.End();
                                        Response.Close();
                                    }
                                    catch (System.Threading.ThreadAbortException ex)
                                    {
                                        Logger.Debug(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                                finally
                                {
                                    session.CloseConnection();
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }

                        #endregion
                    }
                    else if (Request.QueryString["case"].ToString() == "PartnersReportData")
                    {
                        #region PartnersReportData

                        if (Request.QueryString["type"].ToString() == "pdf")
                        {
                            if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && Request.QueryString["mode"].ToString() == "a")
                            {
                                #region Export To PDF with criteria weight

                                if (vSession.User != null)
                                {
                                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    {
                                        string country = Request.QueryString["country"] != null ? Request.QueryString["country"].ToString() : "";
                                        string partnerName = Request.QueryString["partnername"] != null ? Request.QueryString["partnername"].ToString() : "";

                                        bool isClosed = false;

                                        try
                                        {
                                            if (session.Connection.State == ConnectionState.Closed)
                                            {
                                                session.OpenConnection();
                                                isClosed = true;
                                            }

                                            string query = @"SELECT u.company_name as 'COMPANY NAME',
                                                            u.email as 'EMAIL',
                                                            tier_status as 'TIER',
                                                            case when isnull(val.avg,'0') = 0 then '0' else val.avg end as SCORE,
                                                            country as 'COUNTRY',
                                                            case when isnull(cc.criteria_description,'') = '' then 'NOT SELECTED' else cc.criteria_description end as 'CRITERIA DESCRIPTION',
                                                            case when isnull(v.value,'') = '' then 'NOT SELECTED' else v.value end as 'CRITERIA',
                                                            case when isnull(v.weight,'') = '' then '-' else v.weight end as 'CRITERIA WEIGHT'
                                                            FROM Elio_collaboration_vendors_resellers cvr
                                                            cross apply
                                                            (
	                                                            SELECT sum(cast([weight] as int))/7 as avg
	                                                            FROM Elio_tier_management_criteria_values_custom
	                                                            where id in 
	                                                            (
		                                                            select criteria_values_id
		                                                            from Elio_tier_management_users_criteria_scores_custom
		                                                            where user_id = cvr.partner_user_id
	                                                            )
                                                            )val
                                                            inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
	                                                            on cvr_inv.vendor_reseller_id = cvr.id ";

                                            if (vSession.LoggedInSubAccountRoleID > 0 && !vSession.IsAdminRole)
                                            {
                                                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
                                                            on cvmr.vendor_reseller_id = cvr.id 
                                                                and cvmr.partner_user_id = cvr.partner_user_id
                                                        inner join Elio_users_sub_accounts usa 
                                                            on usa.user_id = cvr.master_user_id 
                                                                and usa.id = cvmr.sub_account_id ";
                                            }

                                            query += @"inner join elio_users u 
	                                                on u.id = cvr.partner_user_id 
                                                    left join Elio_tier_management_users_criteria_scores_custom ucr
	                                                    on ucr.user_id = cvr.partner_user_id
                                                    left join Elio_tier_management_criteria_values_custom v
	                                                    on v.id = ucr.criteria_values_id
                                                    left join Elio_tier_management_criteria_custom cc
	                                                    on cc.id = v.criteria_id
                                                where cvr.master_user_id = @master_user_id and cvr.is_active = 1 ";

                                            if (partnerName != "")
                                                query += " AND (u.company_name like '" + partnerName.Replace("_", " ").Replace("%", "&") + "%' OR u.email = '" + partnerName + "') ";

                                            if (country != "")
                                                query += " AND u.country = '" + country.Replace("-", " ") + "' ";

                                            if (vSession.LoggedInSubAccountRoleID > 0 && !vSession.IsAdminRole && vSession.SubAccountEmailLogin != "")
                                                query += " and usa.team_role_id = " + vSession.LoggedInSubAccountRoleID + " and usa.email = '" + vSession.SubAccountEmailLogin + "' ";

                                            query += " AND invitation_status = 'Confirmed' ";

                                            query += " ORDER BY invitation_status DESC";

                                            DataTable dt = session.GetDataTable(query
                                                            , DatabaseHelper.CreateIntParameter("@master_user_id", vSession.User.Id));

                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                ExportLib.ExportDataTableToPdf(dt, vSession.User.CompanyName);
                                            }

                                            if (isClosed)
                                                session.CloseConnection();
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }
                                    }
                                }
                                else
                                    Response.Redirect(ControlLoader.Login, false);

                                #endregion
                            }
                            else
                            {
                                #region Export To PDF

                                if (vSession.User != null)
                                {
                                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    {
                                        string country = Request.QueryString["country"] != null ? Request.QueryString["country"].ToString() : "";
                                        string partnerName = Request.QueryString["partnername"] != null ? Request.QueryString["partnername"].ToString() : "";

                                        bool isClosed = false;

                                        try
                                        {
                                            if (session.Connection.State == ConnectionState.Closed)
                                            {
                                                session.OpenConnection();
                                                isClosed = true;
                                            }

                                            string query = @"SELECT u.company_name as 'COMPANY NAME',
                                                        --isnull(usa.last_name, '') + ' ' + isnull(usa.first_name, '') as 'MAIN CONTACT PERSON',
                                                        u.email as 'EMAIL',
                                                        tier_status as 'TIER',
                                                        case when isnull(val.avg,'0') = 0 then '0' else val.avg end as SCORE,
                                                        country as 'COUNTRY'
                                                    FROM Elio_collaboration_vendors_resellers cvr
                                                    cross apply
                                                    (
	                                                    SELECT sum(cast([weight] as int))/7 as avg
	                                                    FROM Elio_tier_management_criteria_values_custom
	                                                    where id in 
	                                                    (
		                                                    select criteria_values_id
		                                                    from Elio_tier_management_users_criteria_scores_custom
		                                                    where user_id = cvr.partner_user_id
	                                                    )
                                                    )val
                                                    inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
	                                                    on cvr_inv.vendor_reseller_id = cvr.id ";

                                            if (vSession.LoggedInSubAccountRoleID > 0 && !vSession.IsAdminRole)
                                            {
                                                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
                                                            on cvmr.vendor_reseller_id = cvr.id 
                                                                and cvmr.partner_user_id = cvr.partner_user_id
                                                        inner join Elio_users_sub_accounts usa 
                                                            on usa.user_id = cvr.master_user_id 
                                                                and usa.id = cvmr.sub_account_id ";
                                            }

                                            query += @"inner join elio_users u 
	                                                on u.id = cvr.partner_user_id                                                    
                                                where cvr.master_user_id = @master_user_id and cvr.is_active = 1 ";

                                            if (partnerName != "")
                                                query += " AND (u.company_name like '" + partnerName.Replace("_", " ").Replace("%", "&") + "%' OR u.email = '" + partnerName + "') ";

                                            if (country != "")
                                                query += " AND u.country = '" + country.Replace("-", " ") + "' ";

                                            if (vSession.LoggedInSubAccountRoleID > 0 && !vSession.IsAdminRole && vSession.SubAccountEmailLogin != "")
                                                query += " and usa.team_role_id = " + vSession.LoggedInSubAccountRoleID + " and usa.email = '" + vSession.SubAccountEmailLogin + "' ";

                                            query += " AND invitation_status = 'Confirmed' ";

                                            query += " ORDER BY invitation_status DESC";

                                            DataTable dt = session.GetDataTable(query
                                                            , DatabaseHelper.CreateIntParameter("@master_user_id", vSession.User.Id));

                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                ExportLib.ExportDataTableToPdf(dt, vSession.User.CompanyName);
                                            }

                                            if (isClosed)
                                                session.CloseConnection();
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }
                                    }
                                }
                                else
                                    Response.Redirect(ControlLoader.Login, false);

                                #endregion+
                            }
                        }
                        else if (Request.QueryString["type"].ToString() == "csv")
                        {
                            if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && Request.QueryString["mode"].ToString() == "a")
                            {
                                #region Export To CSV with criteria weight

                                if (vSession.User != null)
                                {
                                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    {
                                        string country = Request.QueryString["country"] != null ? Request.QueryString["country"].ToString() : "";
                                        string partnerName = Request.QueryString["partnername"] != null ? Request.QueryString["partnername"].ToString() : "";

                                        bool isClosed = false;

                                        try
                                        {
                                            if (session.Connection.State == ConnectionState.Closed)
                                            {
                                                session.OpenConnection();
                                                isClosed = true;
                                            }

                                            string query = @"SELECT u.company_name as 'COMPANY NAME',
                                                            u.email as 'EMAIL',
                                                            tier_status as 'TIER',
                                                            case when isnull(val.avg,'0') = 0 then '0' else val.avg end as SCORE,
                                                            country as 'COUNTRY',
                                                            case when isnull(cc.criteria_description,'') = '' then 'NOT SELECTED' else cc.criteria_description end as 'CRITERIA DESCRIPTION',
                                                            case when isnull(v.value,'') = '' then 'NOT SELECTED' else v.value end as 'CRITERIA',
                                                            case when isnull(v.weight,'') = '' then '-' else v.weight end as 'CRITERIA WEIGHT'
                                                            FROM Elio_collaboration_vendors_resellers cvr
                                                            cross apply
                                                            (
	                                                            SELECT sum(cast([weight] as int))/7 as avg
	                                                            FROM Elio_tier_management_criteria_values_custom
	                                                            where id in 
	                                                            (
		                                                            select criteria_values_id
		                                                            from Elio_tier_management_users_criteria_scores_custom
		                                                            where user_id = cvr.partner_user_id
	                                                            )
                                                            )val
                                                            inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
	                                                            on cvr_inv.vendor_reseller_id = cvr.id ";

                                            if (vSession.LoggedInSubAccountRoleID > 0 && !vSession.IsAdminRole)
                                            {
                                                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
                                                            on cvmr.vendor_reseller_id = cvr.id 
                                                                and cvmr.partner_user_id = cvr.partner_user_id
                                                        inner join Elio_users_sub_accounts usa 
                                                            on usa.user_id = cvr.master_user_id 
                                                                and usa.id = cvmr.sub_account_id ";
                                            }

                                            query += @" inner join elio_users u 
	                                                    on u.id = cvr.partner_user_id
                                                        left join Elio_tier_management_users_criteria_scores_custom ucr
	                                                        on ucr.user_id = cvr.partner_user_id
                                                        left join Elio_tier_management_criteria_values_custom v
	                                                        on v.id = ucr.criteria_values_id
                                                        left join Elio_tier_management_criteria_custom cc
	                                                        on cc.id = v.criteria_id
                                                        where cvr.master_user_id = @master_user_id and cvr.is_active = 1 ";

                                            if (partnerName != "")
                                                query += " AND (u.company_name like '" + partnerName.Replace("_", " ").Replace("%", "&") + "%' OR u.email = '" + partnerName + "') ";

                                            if (country != "")
                                                query += " AND u.country = '" + country.Replace("-", " ") + "' ";

                                            if (vSession.LoggedInSubAccountRoleID > 0 && !vSession.IsAdminRole && vSession.SubAccountEmailLogin != "")
                                                query += " and usa.team_role_id = " + vSession.LoggedInSubAccountRoleID + " and usa.email = '" + vSession.SubAccountEmailLogin + "' ";

                                            query += " AND invitation_status = 'Confirmed' ";

                                            query += " ORDER BY invitation_status DESC";

                                            DataTable dt = session.GetDataTable(query
                                                            , DatabaseHelper.CreateIntParameter("@master_user_id", vSession.User.Id));

                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                //SaveCSVFile(dt);
                                                ExportLib.ExportPartnersDataTableToCSV(dt);
                                            }

                                            if (isClosed)
                                                session.CloseConnection();
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }
                                    }
                                }
                                else
                                    Response.Redirect(ControlLoader.Login, false);

                                #endregion
                            }
                            else
                            {
                                #region Export To CSV

                                if (vSession.User != null)
                                {
                                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    {
                                        string country = Request.QueryString["country"] != null ? Request.QueryString["country"].ToString() : "";
                                        string partnerName = Request.QueryString["partnername"] != null ? Request.QueryString["partnername"].ToString() : "";

                                        bool isClosed = false;

                                        try
                                        {
                                            if (session.Connection.State == ConnectionState.Closed)
                                            {
                                                session.OpenConnection();
                                                isClosed = true;
                                            }

                                            string query = @"SELECT u.company_name as 'COMPANY NAME',
                                                        --isnull(usa.last_name, '') + ' ' + isnull(usa.first_name, '') as 'MAIN CONTACT PERSON',
                                                        u.email as 'EMAIL',
                                                        tier_status as 'TIER',
                                                        case when isnull(val.avg,'0') = 0 then '0' else val.avg end as SCORE,
                                                        country as 'COUNTRY'
                                                    FROM Elio_collaboration_vendors_resellers cvr
                                                    cross apply
                                                    (
	                                                    SELECT sum(cast([weight] as int))/7 as avg
	                                                    FROM Elio_tier_management_criteria_values_custom
	                                                    where id in 
	                                                    (
		                                                    select criteria_values_id
		                                                    from Elio_tier_management_users_criteria_scores_custom
		                                                    where user_id = cvr.partner_user_id
	                                                    )
                                                    )val
                                                    inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
	                                                    on cvr_inv.vendor_reseller_id = cvr.id ";

                                            if (vSession.LoggedInSubAccountRoleID > 0 && !vSession.IsAdminRole)
                                            {
                                                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
                                                            on cvmr.vendor_reseller_id = cvr.id 
                                                                and cvmr.partner_user_id = cvr.partner_user_id
                                                        inner join Elio_users_sub_accounts usa 
                                                            on usa.user_id = cvr.master_user_id 
                                                                and usa.id = cvmr.sub_account_id ";
                                            }

                                            query += @" inner join elio_users u 
	                                                    on u.id = cvr.partner_user_id
                                                    where cvr.master_user_id = @master_user_id and cvr.is_active = 1 ";

                                            if (partnerName != "")
                                                query += " AND (u.company_name like '" + partnerName.Replace("_", " ").Replace("%", "&") + "%' OR u.email = '" + partnerName + "') ";

                                            if (country != "")
                                                query += " AND u.country = '" + country.Replace("-", " ") + "' ";

                                            if (vSession.LoggedInSubAccountRoleID > 0 && !vSession.IsAdminRole && vSession.SubAccountEmailLogin != "")
                                                query += " and usa.team_role_id = " + vSession.LoggedInSubAccountRoleID + " and usa.email = '" + vSession.SubAccountEmailLogin + "' ";

                                            query += " AND invitation_status = 'Confirmed' ";

                                            query += " ORDER BY invitation_status DESC";

                                            DataTable dt = session.GetDataTable(query
                                                            , DatabaseHelper.CreateIntParameter("@master_user_id", vSession.User.Id));

                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                //SaveCSVFile(dt);
                                                ExportLib.ExportPartnersDataTableToCSV(dt);
                                            }

                                            if (isClosed)
                                                session.CloseConnection();
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }
                                    }
                                }
                                else
                                    Response.Redirect(ControlLoader.Login, false);

                                #endregion
                            }
                        }

                        #endregion
                    }
                    else if (Request.QueryString["case"].ToString() == "PartnersTierManagementReportingData")
                    {
                        #region Tier Management

                        if (vSession.User != null)
                        {
                            if (vSession.User.CompanyType == Types.Vendors.ToString())
                            {
                                if (Request.QueryString["partnerViewID"] != null)
                                {
                                    int partnerId = Convert.ToInt32(Session[Request.QueryString["partnerViewID"]]);
                                    if (partnerId > 0)
                                    {
                                        if (session.Connection.State == ConnectionState.Closed)
                                            session.OpenConnection();

                                        DataTable table = Sql.GetPartnerTierManagementReportingTbl(partnerId, true, session);
                                        if (table.Rows.Count > 0)
                                        {
                                            ExportLib.ExportDataTableToPdf(table, vSession.User.CompanyName);
                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                    else if (Request.QueryString["case"].ToString() == "AnonymousTrackingReportData")
                    {
                        if (vSession.ViewStateDataStoreForExport != null)
                        {
                            ExportLib.ExportDataTableToCSV(vSession.ViewStateDataStoreForExport, Request.QueryString["case"].ToString());
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        public void SaveCSVFile(DataTable dt)
        {
            try
            {
                string fileName = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();

                Response.Clear();
                HttpContext.Current.Response.ContentType = "Application/csv";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Partners_report_" + fileName + ".csv");
                HttpContext.Current.Response.Write(ExportLib.ExportToCSVFile(dt)); 
            }
            finally
            {
                Response.End();
            }
        }

        public string CaseStudy { get; set; }
    }
}