using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using WdS.ElioPlus.Lib.Utils;
using System.Data.SqlClient;

namespace WdS.ElioPlus.Lib.DBQueries
{
    public class SqlCollaboration
    {
        public static bool HasCustomerAccountSettings(int vendorId, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) as count
                                FROM Stripe_users_accounts_customers_settings 
                                WHERE user_id = @user_id";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@user_id", vendorId));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }
        public static bool ExistAccountAPIByChannelPartner(int resellerId, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) as count
                                FROM Stripe_users_accounts 
                                WHERE user_id = @user_id";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@user_id", resellerId));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static bool UpdateLeadPaymentStatus(int leadId, int paymentStatus, string chargeId, DBSession session)
        {
            int row = session.ExecuteQuery(@"UPDATE Elio_lead_distributions
                                                SET payment_status = @payment_status
                                                    ,charge_id = @charge_id
                                                    ,last_update = GETDATE()
                                            WHERE id = @id"
                                            , DatabaseHelper.CreateIntParameter("@payment_status", paymentStatus)
                                            , DatabaseHelper.CreateStringParameter("@charge_id", chargeId)
                                            , DatabaseHelper.CreateIntParameter("@id", leadId));

            return row > 0 ? true : false;
        }

        public static List<ElioLeadDistributions> GetUserLeadsForPayments(ElioUsers user, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int status, string result, DBSession session)
        {
            DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);

            string query = @"SELECT 
                            ld.id
                            ,u.company_name as 'partner_name'
                            ,u.company_logo
                            ,ld.company_name as 'client'
                            ,ld.email as 'client_email'
                            ,ld.country as 'location'
                            ,ld.amount
                            ,ld.cur_id as 'currency'
                            ,ld.last_update
                            ,dateadd(day, 30, ld.last_update) as 'payment_date'
                            ,case when payment_status = 0 then 'Not Paid' else 'Paid' end
                        FROM Elio_lead_distributions ld ";

            if (user.CompanyType == Types.Vendors.ToString())
            {
                query+=@" INNER JOIN elio_users u 
                            ON u.id = ld.reseller_id ";

                if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                {
                    query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = ld.reseller_id
		                            and cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
                }
            }
            else
            {
                query += @" INNER JOIN elio_users u 
                              ON u.id = ld.vendor_id ";
            }

            query += @" WHERE 1 = 1
                        AND ld.is_public = 1
                        AND ld.status = @status ";

            if (result != "")
                query += " AND lead_result = @lead_result";

            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            if (user.CompanyType == Types.Vendors.ToString() && loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += " ORDER BY created_date DESC, lead_result DESC";

            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                , DatabaseHelper.CreateIntParameter("@status", status)
                                , DatabaseHelper.CreateStringParameter("@lead_result", result));
        }

        public static DataTable GetUserLeadsForPaymentsTbl(ElioUsers user, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int status, string result, int paymentStatus, bool isUpcomming, DBSession session)
        {
            string query = @"SELECT 
                            ld.id
                            ,ld.charge_id
                            ,ld.vendor_id
                            ,ld.reseller_id
                            ,u.company_name as 'partner_name'
                            ,u.company_logo
                            ,ld.company_name as 'client'
                            ,ld.email as 'client_email'
                            ,ld.country as 'location'
                            ,ld.amount
                            ,case when tier_status is not null then cvr.tier_status else 'Not Set' end as 'tier_status'
                            ,case when isnull(tmus.id, 0) > 0 then CAST(tmus.commision as nvarchar(10)) + ' %' else + '0 %' end as 'tier_commission'
                            ,cc.currency_id as 'currency'
                            ,ld.last_update
                            ,convert(varchar, dateadd(day, 30, ld.last_update), 23) as 'payment_date'
                            ,case when payment_status = 0 then 'Not Paid' else 'Paid' end as 'payment_status'
                        FROM Elio_lead_distributions ld 
                        INNER join Elio_collaboration_vendors_resellers cvr
	                        on cvr.master_user_id = ld.vendor_id
	                        AND cvr.partner_user_id = ld.reseller_id
	                        and cvr.is_active = 1
                        INNER JOIN Elio_currencies_countries cc
	                        ON cc.cur_id = ld.cur_id
                        left joiN Elio_tier_management_users_settings tmus
	                        on tmus.user_id = ld.vendor_id
	                        and tmus.description = cvr.tier_status ";

            if (user.CompanyType == Types.Vendors.ToString())
            {
                query += @" INNER JOIN elio_users u 
                            ON u.id = ld.reseller_id ";

                if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                {
                    query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = ld.reseller_id
		                            and cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
                }

                if (isUpcomming)
                {
                    bool hasSettings = HasCustomerAccountSettings(user.Id, session);
                    if (hasSettings)
                    {
                        query += @" inner join Stripe_users_accounts_customers_settings acs
	                                on acs.user_id = ld.vendor_id 
                                    and dateadd(day, acs.payment_days_after, ld.last_update) <= DATEADD(day, 10, GETDATE()) ";
                    }
                }
            }
            else
            {
                query += @" INNER JOIN elio_users u 
                              ON u.id = ld.vendor_id ";
            }

            query += @" WHERE 1 = 1
                        AND ld.is_public = 1
                        AND ld.status = @status ";

            if (result != "")
                query += " AND lead_result = @lead_result ";

            query += " AND payment_status = @payment_status ";

            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            if (user.CompanyType == Types.Vendors.ToString() && loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += " ORDER BY created_date DESC, lead_result DESC";

            return session.GetDataTable(query
                                , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                , DatabaseHelper.CreateIntParameter("@status", status)
                                , DatabaseHelper.CreateStringParameter("@lead_result", result)
                                , DatabaseHelper.CreateIntParameter("@payment_status", paymentStatus));
        }

        public static DataTable GetUserDealsForPaymentsTbl(ElioUsers user, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int status, string result, int paymentStatus, bool isUpcomming, DBSession session)
        {
            string query = @"SELECT 
                            ld.id
                            ,ld.charge_id
                            ,ld.vendor_id
                            ,ld.reseller_id
                            ,u.company_name as 'partner_name'
                            ,u.company_logo
                            ,ld.company_name as 'client'
                            ,ld.email as 'client_email'
                            ,ld.country as 'location'
                            ,ld.amount
                            ,case when tier_status is not null then cvr.tier_status else 'Not Set' end as 'tier_status'
                            ,case when isnull(tmus.id, 0) > 0 then CAST(tmus.commision as nvarchar(10)) + ' %' else + '0 %' end as 'tier_commission'
                            ,cc.currency_id as 'currency'
                            ,ld.last_update
                            ,convert(varchar, dateadd(day, 30, ld.last_update), 23) as 'payment_date'
                            ,case when payment_status = 0 then 'Not Paid' else 'Paid' end as 'payment_status'
                        FROM Elio_registration_deals ld 
                        INNER join Elio_collaboration_vendors_resellers cvr
	                        on cvr.master_user_id = ld.vendor_id
	                        AND cvr.partner_user_id = ld.reseller_id
	                        and cvr.is_active = 1
                        INNER JOIN Elio_currencies_countries cc
	                        ON cc.cur_id = ld.cur_id
                        left joiN Elio_tier_management_users_settings tmus
	                        on tmus.user_id = ld.vendor_id
	                        and tmus.description = cvr.tier_status ";

            if (user.CompanyType == Types.Vendors.ToString())
            {
                query += @" INNER JOIN elio_users u 
                            ON u.id = ld.reseller_id ";

                if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                {
                    query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = ld.reseller_id
		                            and cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
                }

                if (isUpcomming)
                {
                    bool hasSettings = HasCustomerAccountSettings(user.Id, session);
                    if (hasSettings)
                    {
                        query += @" inner join Stripe_users_accounts_customers_settings acs
	                                on acs.user_id = ld.vendor_id 
                                    and dateadd(day, acs.payment_days_after, ld.last_update) <= DATEADD(day, 10, GETDATE()) ";
                    }
                }
            }
            else
            {
                query += @" INNER JOIN elio_users u 
                              ON u.id = ld.vendor_id ";
            }

            query += @" WHERE 1 = 1
                        AND ld.is_public = 1
                        AND ld.status = @status ";

            if (result != "")
                query += " AND deal_result = @deal_result ";

            query += " AND payment_status = @payment_status ";

            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            if (user.CompanyType == Types.Vendors.ToString() && loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += " ORDER BY created_date DESC, deal_result DESC";

            return session.GetDataTable(query
                                , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                , DatabaseHelper.CreateIntParameter("@status", status)
                                , DatabaseHelper.CreateStringParameter("@deal_result", result)
                                , DatabaseHelper.CreateIntParameter("@payment_status", paymentStatus));
        }

        public static bool UpdatePaymentMethodTypeToStripeCustomerAccount(string accountId, string customerId, int methodType, DBSession session)
        {
            int rows = session.ExecuteQuery(@"UPDATE Stripe_users_accounts_customers
                                                        SET payment_method_type = @payment_method_type
                                                        , last_updated = GETDATE() 
                                                        where stripe_account_id = @stripe_account_id 
                                                        and stripe_customer_id = @stripe_customer_id"
                                    , DatabaseHelper.CreateStringParameter("@stripe_account_id", accountId)
                                    , DatabaseHelper.CreateStringParameter("@stripe_customer_id", customerId)
                                    , DatabaseHelper.CreateIntParameter("@payment_method_type", methodType));

            return rows > 0 ? true : false;
        }

        public static bool IsAdminConfirmedCollaborationPartner(int partnerUserId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(id) as count
                                                        from Elio_collaboration_vendors_resellers
                                                        where 1 = 1  
                                                        and is_active = 1 
                                                        and master_user_id = 1
                                                        and invitation_status = 'Confirmed'
                                                        and partner_user_id = @partner_user_id"
                                    , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static StripeUsersAccounts GetStripeAccountByVendorAndPartner(int vendorId, int resellerId, DBSession session)
        {
            DataLoader<StripeUsersAccounts> loader = new DataLoader<StripeUsersAccounts>(session);

            return loader.LoadSingle(@"SELECT sua.*
                                    FROM Stripe_users_accounts sua
                                    INNER JOIN Elio_collaboration_vendors_resellers cvr 
	                                    ON sua.user_id = cvr.partner_user_id
                                    WHERE master_user_id = @master_user_id
                                    AND partner_user_id = @partner_user_id"
                            , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                            , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));
        }

        public static List<StripeUsersAccounts> GetStripeAccountsByVendor(int vendorId, DBSession session)
        {
            DataLoader<StripeUsersAccounts> loader = new DataLoader<StripeUsersAccounts>(session);

            return loader.Load(@"SELECT sua.*
                                    FROM Stripe_users_accounts sua
                                    INNER JOIN Elio_collaboration_vendors_resellers cvr 
	                                    ON sua.user_id = cvr.partner_user_id
                                    WHERE master_user_id = @master_user_id"
                            , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));
        }

        public static StripeUsersAccounts GetStripeAccountByUserId(int resellerId, DBSession session)
        {
            DataLoader<StripeUsersAccounts> loader = new DataLoader<StripeUsersAccounts>(session);

            return loader.LoadSingle(@"SELECT *  
                                        FROM Stripe_users_accounts 
                                        WHERE user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", resellerId));
        }

        public static StripeUsersAccountsCustomers GetStripeAccountsCustomerByUserIdAndStripeAccountId(int userId, string stripeAccountId, DBSession session)
        {
            DataLoader<StripeUsersAccountsCustomers> loader = new DataLoader<StripeUsersAccountsCustomers>(session);

            return loader.LoadSingle(@"SELECT *
                                        FROM Stripe_users_accounts_customers
                                        WHERE user_id = @user_id
                                        and stripe_account_id = @stripe_account_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateStringParameter("@stripe_account_id", stripeAccountId));
        }

        public static StripeUsersAccountsCustomers GetCustomerJoinAccountByVendResId(int vendorId, int resellerId, DBSession session)
        {
            DataLoader<StripeUsersAccountsCustomers> loader = new DataLoader<StripeUsersAccountsCustomers>(session);

            return loader.LoadSingle(@"SELECT uac.*
                                          FROM Stripe_users_accounts_customers uac
                                          inner join Stripe_users_accounts ua
	                                        on ua.id = uac.account_id
                                        where ua.user_id = @reseller_id
                                        and uac.user_id = @vendor_id"
                                , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId)
                                , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));
        }

        public static StripeUsersAccountsCustomers GetStripeAccountsCustomerByVendorId(int vendorId, DBSession session)
        {
            DataLoader<StripeUsersAccountsCustomers> loader = new DataLoader<StripeUsersAccountsCustomers>(session);

            return loader.LoadSingle(@"SELECT *
                                        FROM Stripe_users_accounts_customers
                                        WHERE user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", vendorId));
        }

        public static StripeUsersAccountsCustomers GetStripeAccountsCustomerByAccountIdAndStripeAccountId(int accountId, string stripeAccountId, DBSession session)
        {
            DataLoader<StripeUsersAccountsCustomers> loader = new DataLoader<StripeUsersAccountsCustomers>(session);

            return loader.LoadSingle(@"SELECT *
                                        FROM Stripe_users_accounts_customers
                                        WHERE account_id = @account_id
                                        and stripe_account_id = @stripe_account_id"
                                , DatabaseHelper.CreateIntParameter("@account_id", accountId)
                                , DatabaseHelper.CreateStringParameter("@stripe_account_id", stripeAccountId));
        }

        public static StripeUsersAccountsCustomersSettings GetStripeCustomerSettingsByVendorId(int vendorId, DBSession session)
        {
            DataLoader<StripeUsersAccountsCustomersSettings> loader = new DataLoader<StripeUsersAccountsCustomersSettings>(session);

            return loader.LoadSingle(@"SELECT acs.*
                                        FROM Stripe_users_accounts_customers_settings acs
                                        inner join Stripe_users_accounts_customers ac
	                                        on acs.user_id = ac.user_id
	                                        and acs.customer_id = ac.stripe_customer_id
                                        where ac.user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", vendorId));
        }

        public static string GetStripeAccountID(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT stripe_account_id  
                                                     FROM Stripe_users_accounts 
                                                     WHERE user_id = @user_id"
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (table.Rows.Count > 0) ? table.Rows[0]["stripe_account_id"].ToString() : "";
        }

        public static StripeUsersAccounts GetStripeAccountByUser(int resellerId, DBSession session)
        {
            DataLoader<StripeUsersAccounts> loader = new DataLoader<StripeUsersAccounts>(session);

            return loader.LoadSingle(@"SELECT *  
                                        FROM Stripe_users_accounts 
                                        WHERE user_id = @user_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", resellerId));
        }

        public static bool ExistCustomerToAccount(string accountId, string customerId, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) as count
                                FROM Stripe_users_accounts_customers 
                                WHERE stripe_account_id = @stripe_account_id
                                AND stripe_customer_id = @stripe_customer_id";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateStringParameter("@stripe_account_id", accountId)
                                                   , DatabaseHelper.CreateStringParameter("@stripe_customer_id", customerId));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static bool ExistVendorAsCustomerToAccount(int vendorId, string status, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) as count
                                FROM Stripe_users_accounts_customers 
                                WHERE user_id = @user_id
                                AND status = @status";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@user_id", vendorId)
                                                   , DatabaseHelper.CreateStringParameter("@status", status));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static int GetVendorPaymentMethod(int vendorId, DBSession session)
        {
            string strQuery = @"SELECT TOP 1 payment_method_type
                                FROM Stripe_users_accounts_customers 
                                WHERE user_id = @user_id
                                AND status = 'active'
                                ORDER BY last_updated desc";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@user_id", vendorId));

            if (table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["payment_method_type"]);
            else
                return 0;
        }

        public static string GetProductByStripeAccountID(string accountId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT product_id  
                                                     FROM Stripe_users_accounts_products 
                                                     WHERE stripe_account_id = @stripe_account_id"
                                        , DatabaseHelper.CreateStringParameter("@stripe_account_id", accountId));

            return (table.Rows.Count > 0) ? table.Rows[0]["product_id"].ToString() : "";
        }

        public static StripeUsersAccountsProducts GetAccountProductByStripeAccountIDAndServiceId(string stripeAccountId, int elioServiceId, DBSession session)
        {
            DataLoader<StripeUsersAccountsProducts> loader = new DataLoader<StripeUsersAccountsProducts>(session);

            return loader.LoadSingle(@"SELECT *  
                                        FROM Stripe_users_accounts_products 
                                        WHERE stripe_account_id = @stripe_account_id
                                        AND elio_service_id = @elio_service_id"
                        , DatabaseHelper.CreateStringParameter("@stripe_account_id", stripeAccountId)
                        , DatabaseHelper.CreateIntParameter("@elio_service_id", elioServiceId));
        }

        public static StripeUsersAccountsProductsPrices GetAccountPricetByServiceIdAndStripeAccountID(int accountProductId, string productId, DBSession session)
        {
            DataLoader<StripeUsersAccountsProductsPrices> loader = new DataLoader<StripeUsersAccountsProductsPrices>(session);

            return loader.LoadSingle(@"SELECT *  
                                        FROM Stripe_users_accounts_products_prices 
                                        WHERE account_product_id = @account_product_id
                                        AND stripe_product_id = @stripe_product_id"
                        , DatabaseHelper.CreateIntParameter("@account_product_id", accountProductId)
                        , DatabaseHelper.CreateStringParameter("@stripe_product_id", productId));
        }

        public static DataTable GetUserPartnersByTierDescriptionTbl(int userId, string tierStatus, string companyType, DBSession session)
        {
            if (companyType == Types.Vendors.ToString())
            {
                return session.GetDataTable(@"SELECT u.id,u.company_name
                                                FROM Elio_collaboration_vendors_resellers cvr
                                                inner join elio_users u
	                                                on u.id = cvr.partner_user_id
                                                where cvr.master_user_id = @user_id
                                                and cvr.invitation_status = 'Confirmed'
                                                and cvr.is_active = 1
                                                and u.company_type = 'Channel Partners'
                                                and tier_status = @tierStatus
                                                order by u.company_name"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateStringParameter("@tierStatus", tierStatus));
            }
            else
                return null;
        }

        public static DataTable GetLibraryGroupUserMembersByGroupIdTbl(int userId, int groupId, string companyType, DBSession session)
        {
            if (companyType == Types.Vendors.ToString())
            {
                return session.GetDataTable(@"SELECT u.id,u.company_name
                                            FROM Elio_collaboration_users_library_groups lg
                                            inner join Elio_collaboration_users_library_group_members lgm
	                                            on lg.id = lgm.library_group_id
                                            inner join Elio_collaboration_vendors_resellers cvr
	                                            on cvr.id = lgm.vendor_reseller_id
                                            inner join elio_users u
	                                            on u.id = cvr.partner_user_id
                                            where lg.user_id = @user_id
                                            and cvr.master_user_id = @user_id
                                            and lg.is_active = 1
                                            and lg.is_public = 1
                                            and cvr.invitation_status = 'Confirmed'
                                            and cvr.is_active = 1
                                            and u.company_type = 'Channel Partners'
                                            and lg.id = @groupId
                                            order by u.company_name"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@groupId", groupId));
            }
            else
                return null;
        }

        public static bool ExistLibraryGroupDescription(int userId, string groupDescription, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) as count
                                FROM Elio_collaboration_users_library_groups 
                                WHERE user_id = @user_id
                                AND group_description = @group_description";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@group_description", groupDescription));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static bool ExistLibraryGroupDescriptionToOtherGroupId(int userId, int groupId, string groupDescription, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) as count
                                FROM Elio_collaboration_users_library_groups 
                                WHERE user_id = @user_id
                                AND group_description = @group_description
                                AND id != @id";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@group_description", groupDescription)
                                                   , DatabaseHelper.CreateIntParameter("@id", groupId));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static bool IsLibraryGroupMember(int groupId, int vendResId, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) as count
                                FROM Elio_collaboration_users_library_group_members 
                                WHERE vendor_reseller_id = @vendor_reseller_id
                                AND library_group_id = @library_group_id";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendResId)
                                                   , DatabaseHelper.CreateIntParameter("@library_group_id", groupId));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static DataTable GetCollaborationUserLibraryGroupsTbl(int userId, DBSession session)
        {
            string query = @"SELECT id
                                ,user_id
                                ,group_description    
                            FROM Elio_collaboration_users_library_groups
                            where user_id = @user_id
                            order by group_description";

            return session.GetDataTable(query
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioCollaborationUsersLibraryGroups> GetCollaborationUserLibraryGroups(int userId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryGroups> loader = new DataLoader<ElioCollaborationUsersLibraryGroups>(session);

            return loader.Load(@"SELECT id
                                ,user_id
                                ,group_description    
                            FROM Elio_collaboration_users_library_groups
                            where user_id = @user_id
                            order by group_description"
                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetCollaborationPartnersByCountriesTbl(int userId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, DBSession session)
        {
            string query = @"select c.id,c.country_name,
                                case 
	                                when isnull(c.flag, '') = '' 
		                                then '~/images/countries-flags/' + replace(c.country_name, ' ', '-') + '.jpg'
	                                else  
		                                        '~/images/countries-flags' + c.flag end as country_flag
									                                    ,count(u.id) as partners
                                from Elio_collaboration_vendors_resellers cvr
                                inner join elio_users u
	                                on u.id = cvr.partner_user_id
                                inner join Elio_countries c
	                                on c.country_name = u.country ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = cvr.id 
						            and cvmr.partner_user_id = cvr.partner_user_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.user_id = cvr.master_user_id 
						            and usa.id = cvmr.sub_account_id ";

            query += @" where cvr.master_user_id = @master_user_id
                        and cvr.invitation_status = 'Confirmed' ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += " and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' ";

            query += @" group by c.id, c.country_name, c.flag
                        order by count(u.id) desc ";

            return session.GetDataTable(query
                                        , DatabaseHelper.CreateIntParameter("@master_user_id", userId));
        }

        public static int GetTotalPartnersAndTiersCount(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string invitationStatus, string tierStatus, DBSession session)
        {
            string strQuery = @"SELECT count(cvr.id) as count
                                FROM Elio_collaboration_vendors_resellers cvr
                                inner join elio_users u 
                                    on u.id = cvr.partner_user_id ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                strQuery += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					                on cvmr.vendor_reseller_id = cvr.id
						                and cvmr.partner_user_id = cvr.partner_user_id
				                inner join Elio_users_sub_accounts usa 
					                on usa.user_id = cvr.master_user_id 
						                and usa.id = cvmr.sub_account_id ";

            strQuery += @" where cvr.master_user_id = @master_user_id 
                          and cvr.is_active = 1 ";

            if (!string.IsNullOrEmpty(invitationStatus))
                strQuery += " AND invitation_status = '" + invitationStatus + "' ";

            if (!string.IsNullOrEmpty(tierStatus))
                strQuery += " AND tier_status = '" + tierStatus + "' ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                strQuery += " and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' ";

            //if (loggedInRoleId > 0 && !isAdminRole)
            //    strQuery += @" and usa.is_confirmed = 1  
            //                and usa.team_role_id = " + loggedInRoleId + " " +
            //                "and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(strQuery
                                                , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));

            return table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["count"].ToString()) : 0;
        }

        public static string GetCollaborationVendorResellerTierStatus(int resellerId, int vendorId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT tier_status 
                                       FROM Elio_collaboration_vendors_resellers 
                                       WHERE partner_user_id = @partner_user_id 
                                       and master_user_id = @master_user_id
                                       and is_active = 1"
                                      , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId)
                                      , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));

            return table.Rows.Count > 0 ? table.Rows[0]["tier_status"].ToString() : "";
        }

        public static bool UpdateCollaborationPartnerCompanyName(int partnerUserId, string companyName, DBSession session)
        {
            int row = session.ExecuteQuery(@"update Elio_users
                                            set company_name = @company_name
                                            where id = @id"
                                        , DatabaseHelper.CreateStringParameter("@company_name", companyName)
                                        , DatabaseHelper.CreateIntParameter("@id", partnerUserId));

            return row > 0 ? true : false;
        }

        public static bool UpdateCollaborationPartnerTierStatus(int collVenResId, int vendorId, string tierStatus, DBSession session)
        {
            int row = session.ExecuteQuery(@"update Elio_collaboration_vendors_resellers
                                            set tier_status = @tier_status
                                            where id = @id
                                            and master_user_id = @master_user_id"
                                        , DatabaseHelper.CreateStringParameter("@tier_status", tierStatus)
                                        , DatabaseHelper.CreateIntParameter("@id", collVenResId)
                                        , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));

            return row > 0 ? true : false;
        }

        public static bool UpdatePartnerTierStatus(int vendorId, int resellerId, string tierStatus, int isSetByVendor, DBSession session)
        {
            int row = session.ExecuteQuery(@"update Elio_collaboration_vendors_resellers
                                            set tier_status = @tier_status,
                                            is_set_by_vendor = @is_set_by_vendor
                                            where master_user_id = @master_user_id
                                            and partner_user_id = @partner_user_id"
                                        , DatabaseHelper.CreateStringParameter("@tier_status", tierStatus)
                                        , DatabaseHelper.CreateIntParameter("@is_set_by_vendor", isSetByVendor)
                                        , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                        , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));

            return row > 0 ? true : false;
        }

        public static bool CanSetTierStatus(int vendorId, int resellerId, DBSession session)
        {
            DataTable tableRow = session.GetDataTable(@"Select is_set_by_vendor From Elio_collaboration_vendors_resellers
                                                        where master_user_id = @master_user_id
                                                        and partner_user_id = @partner_user_id"
                                                    , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));

            return tableRow.Rows.Count > 0 ? Convert.ToInt32(tableRow.Rows[0]["is_set_by_vendor"]) == 0 : false;
        }

        public static DataTable GetCollaborationDefaultTierStatusTable(DBSession session)
        {
            return session.GetDataTable(@"SELECT [id], [status_description] FROM [Elio_collaboration_tier_default_status] WHERE [is_public] = 1 order by id");
        }

        public static List<ElioCollaborationTierDefaultStatus> GetCollaborationDefaultTierStatus(DBSession session)
        {
            DataLoader<ElioCollaborationTierDefaultStatus> loader = new DataLoader<ElioCollaborationTierDefaultStatus>(session);

            return loader.Load(@"SELECT [id], [status_description] FROM [Elio_collaboration_tier_default_status] WHERE [is_public] = 1 order by id");
        }

        public static DataTable GetCollaborationUserTierStatusTable(int vendorId, DBSession session)
        {
            return session.GetDataTable(@"SELECT [id], [status_description] 
                                            FROM [Elio_collaboration_vendor_tier_status] 
                                            WHERE [is_public] = 1 
                                            and vendor_id = @vendor_id 
                                            order by id"
                                        , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));
        }

        public static List<ElioCollaborationVendorTierStatus> GetCollaborationUserTierStatus(int vendorId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorTierStatus> loader = new DataLoader<ElioCollaborationVendorTierStatus>(session);

            return loader.Load(@"SELECT [id], [status_description] 
                                    FROM [Elio_collaboration_vendor_tier_status] 
                                    WHERE [is_public] = 1 
                                    and vendor_id = @vendor_id 
                                    order by id"
                                , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetVendorCollaborationInvitationsReceivedFromChannelPartners(int masterUserId, string companyName, string invitationStatus, string country, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string query = @"SELECT cvr.id, cvr.master_user_id, 
                                    invitation_status, cvr.partner_user_id, u.company_name, cvr.sysdate, u.website, u.email, u.country,
                                    cvr_inv.is_new
                                FROM Elio_collaboration_vendors_resellers cvr
                                inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
                                on cvr_inv.vendor_reseller_id = cvr.id and cvr_inv.user_id = cvr.partner_user_id
                                inner join elio_users u 
                                on u.id = cvr.partner_user_id
                                where cvr.master_user_id = @master_user_id and is_active = 1
                                and cvr_inv.user_id <> @master_user_id 
                                and account_status = 1 ";

            if (companyName != "")
                query += " AND (u.company_name like '" + companyName + "%' OR u.email = '" + companyName + "') ";

            if (country != "")
                query += " AND u.country = '" + country + "' ";

            if (invitationStatus != "")
                query += " AND invitation_status = '" + invitationStatus + "' ";

            query += " ORDER BY invitation_status DESC";

            return loader.Load(query
                                 , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId));
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetVendorCollaborationInvitationsToChannelPartners(int masterUserId, string companyName, string invitationStatus, string country, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string query = @"SELECT cvr.id, cvr.master_user_id, 
                                    invitation_status, cvr.partner_user_id, u.company_name, cvr.sysdate, u.email, u.country,
                                    cvr_inv.is_new
                                  FROM Elio_collaboration_vendors_resellers cvr
                                inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
	                                on cvr_inv.vendor_reseller_id = cvr.id
                                inner join elio_users u 
	                                on u.id = cvr.partner_user_id
                                  where cvr.master_user_id = @master_user_id and is_active = 1
                                  and cvr_inv.user_id = @master_user_id ";
            //and account_status = 1 ";


            if (companyName != "")
                query += " AND (u.company_name like '" + companyName + "%' OR u.email = '" + companyName + "') ";

            if (country != "")
                query += " AND u.country = '" + country + "' ";

            if (invitationStatus != "")
                query += " AND invitation_status = '" + invitationStatus + "' ";

            query += " ORDER BY invitation_status DESC";

            return loader.Load(query
                                 , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId));
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetChannelPartnerCollaborationInvitationsToVendors(int partnerUserId, string companyName, string invitationStatus, string country, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string query = @"SELECT cvr.id, cvr.master_user_id, 
                                    invitation_status, cvr.partner_user_id, u.company_name, cvr.sysdate, u.email, u.country,
                                    cvr_inv.is_new
                                  FROM Elio_collaboration_vendors_resellers cvr
                                inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
	                                on cvr_inv.vendor_reseller_id = cvr.id
                                inner join elio_users u 
	                                on u.id = cvr.master_user_id
                                  where cvr.partner_user_id = @partner_user_id and is_active = 1
                                  and cvr_inv.user_id = @partner_user_id 
                                  and account_status = 1 ";

            if (companyName != "")
                query += " AND (u.company_name like '" + companyName + "%' OR u.email = '" + companyName + "') ";

            if (country != "")
                query += " AND u.country = '" + country + "' ";

            if (invitationStatus != "")
                query += " AND invitation_status = '" + invitationStatus + "' ";

            query += " ORDER BY invitation_status DESC";

            return loader.Load(query
                                 , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetVendorCollaborationInvitationsToFromChannelPartners(int masterUserId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string companyName, string invitationStatus, string country, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            List<ElioCollaborationVendorsResellersIJUsers> allUsers = new List<ElioCollaborationVendorsResellersIJUsers>();

            string query = @"SELECT cvr.id, cvr.master_user_id, 
                                    invitation_status, cvr.tier_status, 
                                    case when isnull(val.avg,'0') = 0 then '0' else val.avg end as score,
                                    cvr.partner_user_id, u.company_name, cvr.sysdate, u.email, u.country,
                                    cvr_inv.is_new
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

            if (loggedInRoleId > 0 && !isAdminRole)
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
                                on cvmr.vendor_reseller_id = cvr.id 
                                    and cvmr.partner_user_id = cvr.partner_user_id
                            inner join Elio_users_sub_accounts usa 
                                on usa.user_id = cvr.master_user_id 
                                    and usa.id = cvmr.sub_account_id ";

            query += @" inner join elio_users u 
	                    on u.id = cvr.partner_user_id
                      where cvr.master_user_id = @master_user_id and cvr.is_active = 1";  //and cvr_inv.user_id = @master_user_id";

            if (companyName != "")
                query += " AND (u.company_name like '" + companyName + "%' OR u.email = '" + companyName + "') ";

            if (country != "")
                query += " AND u.country = '" + country + "' ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += " and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "'";

            if (invitationStatus != "")
                query += " AND invitation_status = '" + invitationStatus + "' ";

            query += " ORDER BY cvr.sysdate desc,invitation_status DESC";

            allUsers = loader.Load(query
                                 , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId));

            if (loggedInRoleId > 0 && !isAdminRole)
            {
                string unAssignedQuery = @"SELECT cvr.id, cvr.master_user_id, 
                                                invitation_status, cvr.tier_status, cvr.partner_user_id, u.company_name, cvr.sysdate, u.email, u.country,
                                                cvr_inv.is_new
                                            FROM Elio_collaboration_vendors_resellers cvr
                                            inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
	                                            on cvr_inv.vendor_reseller_id = cvr.id 
                                            inner join elio_users u 
	                                            on u.id = cvr.partner_user_id
                                            where master_user_id = @master_user_id
								            and cvr.partner_user_id not in
								            (
									            select cvmr.partner_user_id 
									            from Elio_collaboration_vendors_members_resellers cvmr 
									            inner join Elio_users_sub_accounts usa 
										            on usa.id = cvmr.sub_account_id 											            
									            where usa.is_confirmed = 1
									            and usa.is_active = 1
								            )
								            and cvr.is_active = 1";

                if (companyName != "")
                    unAssignedQuery += " AND (u.company_name like '" + companyName + "%' OR u.email = '" + companyName + "') ";

                if (country != "")
                    unAssignedQuery += " AND u.country = '" + country + "' ";

                if (invitationStatus != "")
                    unAssignedQuery += " AND invitation_status = '" + invitationStatus + "' ";

                unAssignedQuery += " ORDER BY cvr.sysdate desc,invitation_status DESC";

                List<ElioCollaborationVendorsResellersIJUsers> freeUsers = loader.Load(unAssignedQuery
                                                                    , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId));

                if (freeUsers.Count > 0)
                    allUsers.AddRange(freeUsers);
            }

            return allUsers;
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetChannelPartnerCollaborationInvitationsReceivedFromVendors(int partnerUserId, string companyName, string invitationStatus, string country, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string query = @"SELECT cvr.id, cvr.master_user_id, 
                                    invitation_status, cvr.partner_user_id, u.company_name, cvr.sysdate, u.website, u.email, u.country,
                                    cvr_inv.is_new
                                FROM Elio_collaboration_vendors_resellers cvr
                                inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
                                on cvr_inv.vendor_reseller_id = cvr.id and cvr_inv.user_id = cvr.master_user_id
                                inner join elio_users u 
                                on u.id = cvr.master_user_id
                                where cvr.partner_user_id = @partner_user_id and is_active = 1 
                                and cvr_inv.user_id <> @partner_user_id 
                                and account_status = 1 ";

            if (companyName != "")
                query += " AND (u.company_name like '" + companyName + "%' OR u.email = '" + companyName + "') ";

            if (country != "")
                query += " AND u.country = '" + country + "' ";

            if (invitationStatus != "")
                query += " AND invitation_status = '" + invitationStatus + "' ";

            query += " ORDER BY invitation_status DESC";

            return loader.Load(query
                                 , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetChannelPartnerCollaborationInvitationsToFromVendors(int partnerUserId, string companyName, string invitationStatus, string country, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string query = @"SELECT cvr.id, cvr.master_user_id, 
                                    invitation_status, cvr.tier_status, 
                                    case when isnull(val.avg,'0') = 0 then '0' else val.avg end as score,
                                    cvr.partner_user_id, u.company_name, cvr.sysdate, u.email, u.country,
                                    cvr_inv.is_new
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
	                                    on cvr_inv.vendor_reseller_id = cvr.id
                                    inner join elio_users u 
	                                    on u.id = cvr.master_user_id
                                      where cvr.partner_user_id = @partner_user_id and is_active = 1";    //and cvr_inv.user_id = @partner_user_id";

            if (companyName != "")
                query += " AND (u.company_name like '" + companyName + "%' OR u.email = '" + companyName + "') ";

            if (country != "")
                query += " AND u.country = '" + country + "' ";

            if (invitationStatus != "")
                query += " AND invitation_status = '" + invitationStatus + "' ";

            query += " ORDER BY invitation_status DESC";

            return loader.Load(query
                                 , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));
        }

        public static List<ElioCollaborationVendorsResellers> GetVendorCollaborationPartnersList(int vendorId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

            return loader.Load(@"SELECT * 
                                       FROM Elio_collaboration_vendors_resellers 
                                       WHERE master_user_id = @master_user_id"
                                       , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));
        }

        public static ElioCollaborationVendorsResellers GetInvitationByResellerByVendorByStatus(int resellerId, int vendorId, string status, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

            return loader.LoadSingle(@"SELECT * 
                                       FROM Elio_collaboration_vendors_resellers 
                                       WHERE partner_user_id = @partner_user_id 
                                       and master_user_id = @master_user_id
                                       and invitation_status = @invitation_status
                                       and is_active = 1"
                                       , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId)
                                       , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                       , DatabaseHelper.CreateStringParameter("@invitation_status", status));
        }

        public static ElioCollaborationVendorResellerInvitations GetVendorResellerInvitationByResellerByVendResIdByStatusAndRecipientEmail(int vendResId, string resellerEmail, string stepStatus, DBSession session)
        {
            DataLoader<ElioCollaborationVendorResellerInvitations> loader = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_collaboration_vendor_reseller_invitations 
                                        WHERE vendor_reseller_id = @vendor_reseller_id
                                        AND recipient_email = @recipient_email
                                        AND invitation_step_description = @invitation_step_description"
                                    , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendResId)
                                    , DatabaseHelper.CreateStringParameter("@recipient_email", resellerEmail)
                                    , DatabaseHelper.CreateStringParameter("@invitation_step_description", stepStatus));
        }

        public static ElioCollaborationVendorResellerInvitations GetVendorResellerInvitationByResellerByVendResId(int vendResId, string status, DBSession session)
        {
            DataLoader<ElioCollaborationVendorResellerInvitations> loader = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_collaboration_vendor_reseller_invitations 
                                        WHERE vendor_reseller_id = @vendor_reseller_id
                                        AND invitation_step_description = @invitation_step_description"
                                    , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendResId)
                                    , DatabaseHelper.CreateStringParameter("@invitation_step_description", status));
        }

        public static bool ExistRequestByChannelPartnerUserId(int resellerUserId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count FROM Elio_collaboration_vendor_reseller_invitations 
                                        WHERE user_id = @user_id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", resellerUserId));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static string GetCollaborationVendorNameByRequestFromResellerUserId(int resellerId, string invitationStatus, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT distinct top 1 cvr.master_user_id, u.company_name
                                        FROM Elio_collaboration_vendors_resellers cvr
                                        inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
	                                        on cvr_inv.vendor_reseller_id = cvr.id
                                        inner join elio_users u
	                                        on u.id = cvr.master_user_id
                                        where 1 = 1
                                        and cvr_inv.user_id = cvr.partner_user_id and cvr.partner_user_id = @user_id
                                        and cvr.invitation_status = @invitation_status and cvr_inv.invitation_step_description = @invitation_status
                                        and cvr.is_active = 1"
                                    , DatabaseHelper.CreateIntParameter("@user_id", resellerId)
                                    , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));

            return table.Rows.Count > 0 ? table.Rows[0]["company_name"].ToString() : "";
        }

        public static string GetCollaborationVendorNameByInvitationToResellerUserId(int resellerId, string invitationStatus, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT distinct top 1 cvr.master_user_id, u.company_name
                                        FROM Elio_collaboration_vendors_resellers cvr
                                        inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
	                                        on cvr_inv.vendor_reseller_id = cvr.id
                                        inner join elio_users u
	                                        on u.id = cvr.master_user_id
                                        where 1 = 1
                                        and cvr_inv.user_id = u.id 
                                        and cvr.master_user_id = u.id
                                        and cvr.partner_user_id = @user_id
                                        and cvr.invitation_status = @invitation_status and cvr_inv.invitation_step_description = @invitation_status
                                        and cvr.is_active = 1"
                                    , DatabaseHelper.CreateIntParameter("@user_id", resellerId)
                                    , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));

            return table.Rows.Count > 0 ? table.Rows[0]["company_name"].ToString() : "";
        }

        public static List<ElioUsers> GetP2PCollaborationResellers(int masterUserId, int resellerId, int countryId, int p2pId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load(@"SELECT distinct u.*
                                FROM Elio_users u
                                inner join Elio_collaboration_vendors_resellers cvr
	                                on cvr.partner_user_id = u.id
                                inner join Elio_countries c
	                                on c.country_name = u.country
                                inner join Elio_users_sub_industries_group_items usigi
	                                on usigi.user_id = u.id
                                inner join Elio_partner_to_partner_sub_industries p2pd
	                                on p2pd.sub_industry_group_item_id = usigi.sub_industry_group_item_id
                                where master_user_id = @master_user_id
                                and invitation_status = 'Confirmed'
                                and u.id not in (@reseller_id)
                                and c.id = @country_id
                                and p2p_id = @p2p_id"
                                , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId)
                                , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId)
                                , DatabaseHelper.CreateIntParameter("@country_id", countryId)
                                , DatabaseHelper.CreateIntParameter("@p2p_id", p2pId));
        }

        public static bool HasInvitationRequestUserByStatus(int userId, string companyType, string invitationStatus, int isNew, out int requestsCount, DBSession session)
        {
            requestsCount = 0;

            string strQuery = @"
                                SELECT COUNT(*) as COUNT
                                FROM Elio_collaboration_vendors_resellers cvr
                                inner join Elio_collaboration_vendor_reseller_invitations cvri
	                                on cvri.vendor_reseller_id = cvr.id 
                                WHERE 1 = 1 
                                AND invitation_status = @invitation_status
                                --AND is_new = @is_new " + Environment.NewLine;

            strQuery += (companyType == EnumHelper.GetDescription(Types.Resellers).ToString()) ? " AND partner_user_id = @user_id" : " AND master_user_id = @user_id";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));
            //, DatabaseHelper.CreateIntParameter("@is_new", isNew));

            if (Convert.ToInt32(table.Rows[0]["COUNT"]) > 0)
            {
                requestsCount = Convert.ToInt32(table.Rows[0]["COUNT"]);
                return true;
            }

            return false;
        }

        public static ElioCollaborationVendorsResellers GetCollaborationVendorsResellersByResellerId(int resellerId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_collaboration_vendors_resellers cvr
                                        INNER JOIN elio_users u
	                                        ON u.id = cvr.master_user_id
                                        WHERE partner_user_id = @partner_user_id"
                                    , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));
        }

        public static bool IsAdministratorReseller(int resellerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select COUNT(id) as count 
                                                    from Elio_collaboration_vendors_resellers
                                                    where master_user_id = 1
                                                    and partner_user_id = @partner_user_id"
                                                     , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool IsConfirmedReseller(int resellerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select COUNT(id) as count 
                                                    from Elio_collaboration_vendors_resellers
                                                    where 1 = 1
                                                    and partner_user_id = @partner_user_id
                                                    and invitation_status = 'Confirmed'"
                                                    , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool IsConfirmedResellerOfVendor(int resellerId, int vendorId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select COUNT(id) as count 
                                                    from Elio_collaboration_vendors_resellers
                                                    where 1 = 1
                                                    and partner_user_id = @partner_user_id
                                                    and master_user_id = @master_user_id
                                                    and invitation_status = 'Confirmed'"
                                                    , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId)
                                                    , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool HasPartnerReseller(int vendorId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select COUNT(id) as count 
                                                    from Elio_collaboration_vendors_resellers
                                                    where 1 = 1
                                                    and master_user_id = @master_user_id
                                                    and invitation_status = 'Confirmed'"
                                                    , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static List<ElioUsers> GetCollaborationVendorsByResellerUserId(int partnerUserId, string invitationStatus, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load(@"SELECT *
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr
	                                on cvr.master_user_id = u.id
                                 where partner_user_id = @partner_user_id
                                 and invitation_status = @invitation_status"
                                , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId)
                                , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));
        }

        public static bool IsPartnerOfCustomVendor(int vendorId, int resellerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                        FROM Elio_collaboration_vendors_resellers
                                                        where master_user_id = @master_user_id
                                                        and partner_user_id = @partner_user_id"
                                                , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                                , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));

            return (table.Rows.Count > 0 && Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static ElioUsers GetCustomVendorLogoByPartner(int vendorId, int resellerId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.LoadSingle(@"SELECT *
                                        FROM Elio_users u
                                        inner join Elio_collaboration_vendors_resellers cvr
                                            on u.id = cvr.master_user_id
                                        where master_user_id = @master_user_id
                                        and partner_user_id = @partner_user_id"
                                    , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                    , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));
        }

        public static bool IsPartnerOfCustomVendorAdminPartner(int vendorId, int resellerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                        FROM Elio_collaboration_vendors_resellers
                                                        where (master_user_id = @master_user_id
                                                        and partner_user_id = @partner_user_id)
                                                        or master_user_id = 1"
                                                , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                                , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));

            return (table.Rows.Count > 0 && Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static string GetVendorNameByResellerUserId(int partnerUserId, DBSession session)
        {
            ElioUsers vendor = null;
            string companyName = "";

            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            vendor = loader.LoadSingle(@"SELECT TOP 1 u.company_name
                                            FROM Elio_users u
                                            inner join Elio_collaboration_vendors_resellers cvr
                                            on cvr.master_user_id = u.id
                                            where partner_user_id = @partner_user_id 
                                            and invitation_status = 'Confirmed'"
                                , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));

            if (vendor == null)
            {
                vendor = loader.LoadSingle(@"SELECT TOP 1 u.company_name
                                            FROM Elio_users u
                                            inner join Elio_collaboration_vendors_resellers cvr
                                            on cvr.master_user_id = u.id
                                            where partner_user_id = @partner_user_id 
                                            and invitation_status = 'Pending'"
                                , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));
            }

            if (vendor != null)
                companyName = vendor.CompanyName;

            return companyName;
        }

        public static List<ElioUsers> GetCollaborationUsersByUserType(ElioUsers user, string invitationStatus, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            string query = @"SELECT u.*
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr ";

            query += user.CompanyType == Types.Vendors.ToString() ? " on cvr.partner_user_id = u.id where master_user_id = @user_id "
                                                                  : " on cvr.master_user_id = u.id where partner_user_id = @user_id ";

            query += " and invitation_status = @invitation_status";

            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));
        }

        public static List<ElioUsers> GetCollaborationPartnersByVendorId(int vendorId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            string query = @"SELECT distinct u.id
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr 
                                    on cvr.partner_user_id = u.id 
                            where master_user_id = @user_id 
                            and invitation_status = 'Confirmed'
                            and u.company_type = 'Channel Partners'
                            and u.account_status = 1
                            order by u.id";

            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@user_id", vendorId));
        }

        public static List<ElioUsers> GetCollaborationPartnersByVendorIdAndCountries(int vendorId, string countries, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            string query = @"SELECT distinct u.id
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr 
                                    on cvr.partner_user_id = u.id 
                            where master_user_id = @user_id 
                            and invitation_status = 'Confirmed'
                            and u.company_type = 'Channel Partners'
                            and u.account_status = 1
                            and u.country IN (" + countries + ") " +
                            "order by u.id";

            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@user_id", vendorId));
        }

        public static List<ElioUsers> GetCollaborationPartnersByVendorIdAndTierId(int vendorId, int tierId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            string query = @"SELECT distinct u.id
                                FROM Elio_users u
                                inner join Elio_collaboration_vendors_resellers cvr 
                                on cvr.partner_user_id = u.id 
                                inner join Elio_tier_management_users_settings t
	                                on t.user_id = cvr.master_user_id
	                                and t.description = cvr.tier_status
                                where master_user_id = @user_id 
                                and invitation_status = 'Confirmed'
                                and u.company_type = 'Channel Partners'
                                and u.account_status = 1
                                and tier_status != ''
                                and t.id = @tier_id
                                order by u.id";

            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@user_id", vendorId)
                                , DatabaseHelper.CreateIntParameter("@tier_id", tierId));
        }

        public static DataSet GetCollaborationAssignAndFreeUsersByUserTypeDS(ElioUsers user, int loggedInRoletId, string subAccountEmail, bool isAdminRole, string invitationStatus, DBSession session)
        {
            DataSet allUsers = new DataSet();

            DataTable assignedUsers = new DataTable();
            assignedUsers.TableName = "assignedUsers";

            DataTable freeUsers = new DataTable();
            freeUsers.TableName = "freeUsers";

            string query = @"SELECT u.id,u.company_name
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr ";

            query += user.CompanyType == Types.Vendors.ToString() ? " on cvr.partner_user_id = u.id "
                                                                  : " on cvr.master_user_id = u.id ";

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = cvr.id 
						            and cvmr.partner_user_id = cvr.partner_user_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.user_id = cvr.master_user_id 
						            and usa.id = cvmr.sub_account_id ";

            query += user.CompanyType == Types.Vendors.ToString() ? " where master_user_id = @user_id "
                                                                  : " where partner_user_id = @user_id ";

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.team_role_id = " + loggedInRoletId + " and usa.email = '" + subAccountEmail + "' " +
                          " and usa.is_confirmed = 1 " +
                          " and usa.is_active = 1 ";

            query += @" and invitation_status = @invitation_status
                        and cvr.is_active = 1 ";

            assignedUsers = session.GetDataTable(query
                                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                            , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
            {
                string unAssignedQuery = @"SELECT u.id,u.company_name
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr ";

                unAssignedQuery += user.CompanyType == Types.Vendors.ToString() ? " on cvr.partner_user_id = u.id "
                                                                      : " on cvr.master_user_id = u.id ";

                unAssignedQuery += @" where master_user_id = @user_id
                                        and cvr.partner_user_id not in
                                        (
	                                        select cvmr.partner_user_id 
	                                        from Elio_collaboration_vendors_members_resellers cvmr 
	                                        inner join Elio_users_sub_accounts usa 
		                                        on usa.id = cvmr.sub_account_id 
	                                        where usa.is_confirmed = 1
	                                        and usa.is_active = 1
                                        )
                                        and invitation_status = @invitation_status
                                        and cvr.is_active = 1";

                freeUsers = session.GetDataTable(unAssignedQuery
                                            , DatabaseHelper.CreateIntParameter(@"user_id", user.Id)
                                            , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));
            }

            allUsers.Tables.Add(assignedUsers);
            allUsers.Tables.Add(freeUsers);

            return allUsers;
        }

        public static DataTable GetCollaborationAssignAndFreeUsersByUserTypeTbl(ElioUsers user, int loggedInRoletId, string subAccountEmail, bool isAdminRole, string invitationStatus, DBSession session)
        {
            DataTable allUsers = new DataTable();
            allUsers.TableName = "allUsers";
            allUsers.Columns.Add("id");
            allUsers.Columns.Add("company_name");

            string query = @"SELECT u.id,u.company_name
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr ";

            query += user.CompanyType == Types.Vendors.ToString() ? " on cvr.partner_user_id = u.id "
                                                                  : " on cvr.master_user_id = u.id ";

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = cvr.id
						            and cvmr.partner_user_id = cvr.partner_user_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.user_id = cvr.master_user_id 
						            and usa.id = cvmr.sub_account_id ";

            query += user.CompanyType == Types.Vendors.ToString() ? " where master_user_id = @user_id "
                                                                  : " where partner_user_id = @user_id ";

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.team_role_id = " + loggedInRoletId + " and usa.email = '" + subAccountEmail + "' " +
                          " and usa.is_confirmed = 1 " +
                          " and usa.is_active = 1 ";

            query += @" and invitation_status = @invitation_status
                        and cvr.is_active = 1 ";

            allUsers = session.GetDataTable(query
                                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                            , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
            {
                string unAssignedQuery = @"SELECT u.id,u.company_name
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr ";

                unAssignedQuery += user.CompanyType == Types.Vendors.ToString() ? " on cvr.partner_user_id = u.id "
                                                                      : " on cvr.master_user_id = u.id ";

                unAssignedQuery += @" where master_user_id = @user_id
                                        and cvr.partner_user_id not in
                                        (
	                                        select cvmr.partner_user_id 
	                                        from Elio_collaboration_vendors_members_resellers cvmr 
	                                        inner join Elio_users_sub_accounts usa 
		                                        on usa.id = cvmr.sub_account_id 
	                                        where usa.is_confirmed = 1
	                                        and usa.is_active = 1
                                        )
                                        and invitation_status = @invitation_status
                                        and cvr.is_active = 1";

                DataTable freeUsers = session.GetDataTable(unAssignedQuery
                                            , DatabaseHelper.CreateIntParameter(@"user_id", user.Id)
                                            , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));

                if (freeUsers.Rows.Count > 0)
                {
                    foreach (DataRow freeUser in freeUsers.Rows)
                    {
                        DataRow row = allUsers.NewRow();
                        row["id"] = freeUser["id"];
                        row["company_name"] = freeUser["company_name"];

                        allUsers.Rows.Add(row);
                    }
                }
            }

            return allUsers;
        }

        public static DataTable GetCollaborationAllOrAssignedUsersByUserTypeTbl(ElioUsers user, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string invitationStatus, DBSession session)
        {
            string query = @"SELECT u.id,u.company_name
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr ";

            query += user.CompanyType == Types.Vendors.ToString() ? " on cvr.partner_user_id = u.id "
                                                                  : " on cvr.master_user_id = u.id ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = cvr.id 
						            and cvmr.partner_user_id = cvr.partner_user_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.user_id = cvr.master_user_id 
						            and usa.id = cvmr.sub_account_id ";

            query += user.CompanyType == Types.Vendors.ToString() ? " where cvr.master_user_id = @user_id "
                                                                  : " where cvr.partner_user_id = @user_id ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.team_role_id = " + loggedInRoleId + "  and usa.email = '" + subAccountEmail + "' " +
                          " and usa.is_confirmed = 1 " +
                          " and usa.is_active = 1 ";

            query += @" and invitation_status = @invitation_status
                        and cvr.is_active = 1 ";

            return session.GetDataTable(query
                                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                            , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));
        }

        public static List<ElioUsers> GetCollaborationAssignedAndFreeUsersByUserTypeAndCountry(ElioUsers user, int loggedInRoletId, string subAccountEmail, bool isAdminRole, string invitationStatus, string country, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            List<ElioUsers> allUsers = new List<ElioUsers>();

            string assignedQuery = @"SELECT *
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr ";

            assignedQuery += user.CompanyType == Types.Vendors.ToString() ? " on cvr.partner_user_id = u.id "
                                                                  : " on cvr.master_user_id = u.id ";

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
                assignedQuery += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = cvr.id
						            and cvmr.partner_user_id = cvr.partner_user_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.user_id = cvr.master_user_id 
						            and usa.id = cvmr.sub_account_id ";

            assignedQuery += user.CompanyType == Types.Vendors.ToString() ? " where master_user_id = @user_id "
                                                                  : " where partner_user_id = @user_id ";

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
                assignedQuery += @" and usa.team_role_id = " + loggedInRoletId + " and usa.email = '" + subAccountEmail + "' " +
                          " and usa.is_confirmed = 1 " +
                          " and usa.is_active = 1 ";

            assignedQuery += " and invitation_status = @invitation_status ";

            if (country != "")
                assignedQuery += " and u.country = @country";

            allUsers = loader.Load(assignedQuery
                                , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus)
                                , DatabaseHelper.CreateStringParameter("@country", country));

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
            {
                string freeQuery = @"SELECT *
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr ";

                freeQuery += user.CompanyType == Types.Vendors.ToString() ? " on cvr.partner_user_id = u.id "
                                                                  : " on cvr.master_user_id = u.id ";

                freeQuery += @" where master_user_id = @user_id
                                    and cvr.partner_user_id not in
                                    (
	                                    select cvmr.partner_user_id 
	                                    from Elio_collaboration_vendors_members_resellers cvmr 
	                                    inner join Elio_users_sub_accounts usa 
		                                    on usa.id = cvmr.sub_account_id 
	                                    where usa.is_confirmed = 1
	                                    and usa.is_active = 1
	                                    and cvmr.vendor_reseller_id = cvr.id
                                    )
                                    and cvr.is_active = 1 ";

                freeQuery += " and invitation_status = @invitation_status ";

                if (country != "")
                    freeQuery += " and u.country = @country";

                List<ElioUsers> freeUsers = loader.Load(freeQuery
                                                , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                                , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus)
                                                , DatabaseHelper.CreateStringParameter("@country", country));

                if (freeUsers.Count > 0)
                    allUsers.AddRange(freeUsers);
            }

            return allUsers;
        }

        public static List<ElioUsers> GetCollaborationAllOrAssignedUsersByUserTypeAndCountry(ElioUsers user, int loggedInRoletId, string subAccountEmail, bool isAdminRole, string invitationStatus, string country, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            string query = @"SELECT *
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr ";

            query += user.CompanyType == Types.Vendors.ToString() ? " on cvr.partner_user_id = u.id "
                                                                  : " on cvr.master_user_id = u.id ";

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = cvr.id 
						            and cvmr.partner_user_id = cvr.partner_user_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.user_id = cvr.master_user_id 
						            and usa.id = cvmr.sub_account_id ";

            query += user.CompanyType == Types.Vendors.ToString() ? " where master_user_id = @user_id "
                                                                  : " where partner_user_id = @user_id ";

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.team_role_id = " + loggedInRoletId + " and usa.email = '" + subAccountEmail + "' " +
                          " and usa.is_confirmed = 1 " +
                          " and usa.is_active = 1 ";

            query += " and invitation_status = @invitation_status ";

            if (country != "")
                query += " and u.country = @country";

            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus)
                                , DatabaseHelper.CreateStringParameter("@country", country));
        }

        public static List<ElioUsers> GetCollaborationResellersByVendorUserId(int vendorUserId, string invitationStatus, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load(@"SELECT *
                                 FROM Elio_users u
                                 inner join Elio_collaboration_vendors_resellers cvr
	                                on cvr.partner_user_id = u.id
                                 where master_user_id = @master_user_id
                                 and invitation_status = @invitation_status"
                                , DatabaseHelper.CreateIntParameter("@master_user_id ", vendorUserId)
                                , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));
        }

        public static List<string> GetResellersEmailByVendorUserId(int vendorUserId, int loggedInRoleId, string subAccountEmail, string invitationStatus, DBSession session)
        {
            List<string> emails = new List<string>();

            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            string query = @"SELECT u.email
                            FROM Elio_users u
                            inner join Elio_collaboration_vendors_resellers cvr
	                            on cvr.partner_user_id = u.id ";

            if (loggedInRoleId > 0 && subAccountEmail != "")
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = cvr.id 
						            and cvmr.partner_user_id = cvr.partner_user_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.user_id = cvr.master_user_id 
						            and usa.id = cvmr.sub_account_id";

            query += @" where master_user_id = @master_user_id
                       and invitation_status = @invitation_status ";

            if (loggedInRoleId > 0 && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            " and usa.is_active = 1 ";

            List<ElioUsers> partnerEmails = loader.Load(query
                                                        , DatabaseHelper.CreateIntParameter("@master_user_id ", vendorUserId)
                                                        , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));

            foreach (ElioUsers userEmail in partnerEmails)
            {
                emails.Add(userEmail.Email);
            }

            return emails;
        }

        public static bool HasInvitationRequestThisEmailBySpecificUser(int userId, string recipientEmail, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_collaboration_vendor_reseller_invitations 
                                                     WHERE recipient_email = @recipient_email
                                                     AND user_id = @user_id"
                                                     , DatabaseHelper.CreateStringParameter("@recipient_email", recipientEmail)
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool HasInvitationRequestByStatus(int userId, int accountStatus, string companyType, string invitationStatus, out int requestsCount, DBSession session)
        {
            requestsCount = 0;

            string strQuery = @"
                                SELECT COUNT(*) as COUNT
                                FROM Elio_collaboration_vendors_resellers
                                inner join Elio_collaboration_vendor_reseller_invitations 
	                            on Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id = Elio_collaboration_vendors_resellers.id 
                                inner join Elio_collaboration_users_invitations
	                                on Elio_collaboration_users_invitations.id = Elio_collaboration_vendor_reseller_invitations.user_invitation_id                                
                                WHERE 1 = 1 
                                    AND invitation_status = @invitation_status 
                                    AND Elio_collaboration_vendors_resellers.is_active = 1 
                                    AND Elio_collaboration_users_invitations.is_public = 1
                                    ";

            strQuery += (companyType == EnumHelper.GetDescription(Types.Resellers).ToString() || (companyType == "" && accountStatus == (int)AccountStatus.NotCompleted)) ? " AND partner_user_id = " + userId + "" : " AND master_user_id = " + userId + "";

            strQuery += " AND Elio_collaboration_vendor_reseller_invitations.user_id <> @user_id";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));

            if (Convert.ToInt32(table.Rows[0]["COUNT"]) > 0)
            {
                requestsCount = Convert.ToInt32(table.Rows[0]["COUNT"]);
                return true;
            }

            return false;
        }

        public static List<ElioUsers> GetUserCollaborationConnections(int userId, string companyType, string companyNameOrEmail, string country, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            string strQuery = @"
                                SELECT Elio_users.id, 
                                        company_type, 
                                        company_logo, 
                                        company_name, 
                                        website,
                                        user_application_type, 
                                        email, 
                                        country 
                                FROM elio_users with (nolock) 
                                INNER JOIN Elio_users_connections 
                                    ON Elio_users_connections.connection_id = Elio_users.id 
                                WHERE Elio_users_connections.user_id = @user_id 
                                AND Elio_users_connections.connection_id NOT IN 
                                (
                                    SELECT ";

            strQuery += (companyType == Types.Vendors.ToString()) ? "partner_user_id " : "master_user_id ";

            strQuery += "FROM Elio_collaboration_vendors_resellers WHERE ";

            strQuery += (companyType == Types.Vendors.ToString()) ? "master_user_id = @user_id " : "partner_user_id = @user_id ";

            strQuery += @"AND invitation_status IN ('Confirmed', 'Pending') 
                                ) ";

            if (!string.IsNullOrEmpty(companyNameOrEmail))
                strQuery += @" AND (company_name like '" + companyNameOrEmail + "%' OR email like '" + companyNameOrEmail + "%')";

            if (country != "")
                strQuery += " AND country = '" + country + "' ";

            return loader.Load(strQuery
                                       , DatabaseHelper.CreateIntParameter("@user_id", userId));



            //            return loader.Load(@"SELECT Elio_users.id, 
            //                                        company_type, 
            //                                        company_logo, 
            //                                        company_name, 
            //                                        user_application_type, 
            //                                        email, 
            //                                        country 
            //                                FROM elio_users with (nolock) 
            //                                INNER JOIN Elio_users_connections 
            //                                    ON Elio_users_connections.connection_id = Elio_users.id 
            //                                WHERE Elio_users_connections.user_id = @user_id 
            //                                AND Elio_users_connections.connection_id NOT IN 
            //                                (
            //                                SELECT partner_user_id
            //                                    FROM Elio_collaboration_vendors_resellers 
            //                                    WHERE master_user_id = @user_id 
            //                                    AND invitation_status IN ('Confirmed', 'Pending')
            //                                )"
            //                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            //strQuery += (companyType == Types.Vendors.ToString()) ? @"SELECT partner_user_id
            //                                                          FROM Elio_collaboration_vendors_resellers
            //                                                          WHERE master_user_id = @user_id AND invitation_status <> 'Confirmed'"
            //                                                          :
            //                                                          @"SELECT master_user_id
            //                                                          FROM Elio_collaboration_vendors_resellers 
            //                                                          WHERE Elio_collaboration_vendors_resellers.partner_user_id = @user_id 
            //                                                          AND invitation_status='Confirmed'";

            //strQuery += ")";
            //return loader.Load(strQuery
            //                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
            //                   , DatabaseHelper.CreateIntParameter("@vendor_id", userId));
        }

        public static int GetUserNewFilesReceived(int userId, DBSession session)
        {
            string strQuery = @"
                              SELECT count(id) as files_count
                              FROM Elio_collaboration_users_library_files
                              WHERE user_id = @user_id AND uploaded_by_user_id <> @user_id AND is_new = 1 ";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (Convert.ToInt32(table.Rows[0]["files_count"]) > 0) ? Convert.ToInt32(table.Rows[0]["files_count"]) : 0;
        }

        public static bool ExistCollaboration(int vendorId, int resellerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) AS COUNT 
                                                     FROM Elio_collaboration_vendors_resellers 
                                                     WHERE master_user_id=@master_user_id 
                                                     AND partner_user_id=@partner_user_id"
                                                     , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                                     , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));

            return Convert.ToInt32(table.Rows[0]["COUNT"]) == 0 ? false : true;
        }

        public static bool ExistUserInCollaboration(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) AS COUNT 
                                                     FROM Elio_collaboration_vendors_resellers 
                                                     WHERE master_user_id = @user_id 
                                                     OR partner_user_id = @user_id"
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["COUNT"]) == 0 ? false : true;
        }

        public static bool ExistChannelPartnerInCollaboration(int cahennelPartnerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) AS COUNT 
                                                     FROM Elio_collaboration_vendors_resellers 
                                                     WHERE 1 = 1 
                                                     AND partner_user_id = @partner_user_id"
                                                     , DatabaseHelper.CreateIntParameter("@partner_user_id", cahennelPartnerId));

            return Convert.ToInt32(table.Rows[0]["COUNT"]) == 0 ? false : true;
        }

        public static bool ExistVendorChannelPartnerCollaboration(int vendorId, int cahennelPartnerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) AS COUNT 
                                                     FROM Elio_collaboration_vendors_resellers 
                                                     WHERE 1 = 1 
                                                     AND master_user_id = @master_user_id
                                                     AND partner_user_id = @partner_user_id"
                                                     , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                                     , DatabaseHelper.CreateIntParameter("@partner_user_id", cahennelPartnerId));

            return Convert.ToInt32(table.Rows[0]["COUNT"]) == 0 ? false : true;
        }

        public static int GetCollaborationIdFromPartnerInvitation(int vendorId, string partnerEmailAddress, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT Elio_collaboration_vendors_resellers.id AS id
                                                     FROM Elio_collaboration_vendors_resellers 
                                                     INNER JOIN Elio_collaboration_vendor_reseller_invitations
                                                        ON Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id=Elio_collaboration_vendors_resellers.id
                                                     WHERE master_user_id = @master_user_id 
                                                        AND recipient_email = @recipient_email"
                                                     , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                                     , DatabaseHelper.CreateStringParameter("@recipient_email", partnerEmailAddress));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["id"]) : -1;
        }

        public static int GetCollaborationIdFromChannelPartnerInvitation(int resellerId, string partnerEmailAddress, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT Elio_collaboration_vendors_resellers.id AS id
                                                     FROM Elio_collaboration_vendors_resellers 
                                                     INNER JOIN Elio_collaboration_vendor_reseller_invitations
                                                        ON Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id=Elio_collaboration_vendors_resellers.id
                                                     WHERE partner_user_id = @partner_user_id 
                                                        AND recipient_email = @recipient_email"
                                                     , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId)
                                                     , DatabaseHelper.CreateStringParameter("@recipient_email", partnerEmailAddress));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["id"]) : -1;
        }

        public static int GetCollaborationId(int vendorId, int resellerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT id  
                                                     FROM Elio_collaboration_vendors_resellers 
                                                     WHERE master_user_id = @master_user_id 
                                                     AND partner_user_id = @partner_user_id"
                                                     , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                                     , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["id"]) : -1;
        }

        public static ElioCollaborationVendorResellerInvitations GetCollaborationVendorResellerInvitationByVenResIdAndInvId(int vendorResellerId, int userInvitationId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorResellerInvitations> loader = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);

            return loader.LoadSingle(@"SELECT *
                                       FROM Elio_collaboration_vendor_reseller_invitations WITH (NOLOCK)
                                       WHERE vendor_reseller_id = @vendor_reseller_id 
                                       AND user_invitation_id = @user_invitation_id"
                                       , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendorResellerId)
                                       , DatabaseHelper.CreateIntParameter("@user_invitation_id", userInvitationId));
        }

        public static List<ElioCollaborationVendorResellerInvitations> GetCollaborationVendorResellerInvitationsByVenResId(int vendorResellerId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorResellerInvitations> loader = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);

            return loader.Load(@"SELECT *
                                       FROM Elio_collaboration_vendor_reseller_invitations WITH (NOLOCK)
                                       WHERE vendor_reseller_id = @vendor_reseller_id"
                                       , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendorResellerId));
        }

        public static ElioCollaborationVendorResellerInvitations GetCollaborationVendorResellerInvitationsReceivedFromOtherUser(int vendorResellerId, int userId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorResellerInvitations> loader = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);

            return loader.LoadSingle(@"SELECT *
                                    FROM Elio_collaboration_vendor_reseller_invitations WITH (NOLOCK)
                                    WHERE vendor_reseller_id = @vendor_reseller_id
                                    and user_id != @user_id"
                                , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendorResellerId)
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void DeleteGroupById(int groupId, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_collaboration_users_group_mailbox 
                                    SET is_public = 0,
                                    is_deleted = 1,
                                    date_deleted = @date_deleted 
                                    WHERE group_id = @group_id"
                                    , DatabaseHelper.CreateIntParameter("@group_id", groupId)
                                    , DatabaseHelper.CreateDateTimeParameter("@date_deleted", DateTime.Now));

            session.ExecuteQuery(@"UPDATE Elio_collaboration_users_group_members 
                                    SET is_public = 0,
                                    is_active = 0 
                                    WHERE collaboration_group_id = @collaboration_group_id", DatabaseHelper.CreateIntParameter("@collaboration_group_id", groupId));

            session.ExecuteQuery(@"UPDATE Elio_collaboration_users_groups 
                                    SET is_public = 0,
                                        is_active = 0 
                                    WHERE id = @id", DatabaseHelper.CreateIntParameter("@id", groupId));

            //DataLoader<ElioCollaborationUsersGroups> loader = new DataLoader<ElioCollaborationUsersGroups>(session);

            //ElioCollaborationUsersGroups collGroup = loader.LoadSingle("Select * From Elio_collaboration_users_groups Where id = @id"
            //                                            , DatabaseHelper.CreateIntParameter("@id", groupId));

            //if (collGroup != null)
            //{
            //DataLoader<ElioCollaborationUsersGroupRetailors> loader2 = new DataLoader<ElioCollaborationUsersGroupRetailors>(session);

            //List<ElioCollaborationUsersGroupRetailors> retailors = loader2.Load("Select * From Elio_collaboration_users_group_retailors Where collaboration_group_id = @collaboration_group_id"
            //                                        , DatabaseHelper.CreateIntParameter("@collaboration_group_id", groupId));

            //DataLoader<ElioCollaborationUsersGroupRetailorsMailbox> loader3 = new DataLoader<ElioCollaborationUsersGroupRetailorsMailbox>(session);

            //List<ElioCollaborationUsersGroupRetailorsMailbox> retailorsMailbox = loader3.Load("Select * From Elio_collaboration_users_group_retailors_mailbox Where collaboration_group_id = @collaboration_group_id"
            //                                        , DatabaseHelper.CreateIntParameter("@collaboration_group_id", groupId));

            //foreach (ElioCollaborationUsersGroupRetailorsMailbox groupMailbox in retailorsMailbox)
            //{
            //    session.ExecuteQuery("Delete From Elio_collaboration_mailbox Where id = @id", DatabaseHelper.CreateIntParameter("@id", groupMailbox.MailboxId));
            //}

            //loader2.Delete(retailors);
            //loader3.Delete(retailorsMailbox);

            //loader.Delete(collGroup);
            //}
        }

        public static void DeleteCollaborationById(int id, DBSession session)
        {
            session.ExecuteQuery(@"DELETE 
                                   from Elio_collaboration_users_invitations
                                    where id in
                                    (
                                        select user_invitation_id
                                        from Elio_collaboration_vendor_reseller_invitations
                                        where vendor_reseller_id = @vendor_reseller_id
                                    )"
                            , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", id));

            session.ExecuteQuery(@"DELETE 
                                   FROM Elio_collaboration_vendor_reseller_invitations
                                   WHERE vendor_reseller_id = @vendor_reseller_id"
                                    , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", id));

            session.ExecuteQuery(@"UPDATE Elio_collaboration_users_mailbox
                                    SET is_deleted = @is_deleted,
                                        date_deleted = @date_deleted,
                                        is_public = @is_public
                                   WHERE vendors_resellers_id = @vendors_resellers_id"
                                    , DatabaseHelper.CreateIntParameter("@is_deleted", 1)
                                    , DatabaseHelper.CreateDateTimeParameter("@date_deleted", DateTime.Now)
                                    , DatabaseHelper.CreateIntParameter("@is_public", 0)
                                    , DatabaseHelper.CreateIntParameter("@vendors_resellers_id", id));

            //            int groupId = session.ExecuteScalarIntQuery(@"DELETE 
            //                                   FROM Elio_collaboration_users_group_retailors
            //                                   WHERE collaboration_vendors_resellers_id = @collaboration_vendors_resellers_id"
            //                                    , DatabaseHelper.CreateIntParameter("@collaboration_vendors_resellers_id", id));

            //            if (groupId > 0)
            //            {
            //                session.ExecuteQuery(@"DELETE 
            //                                   FROM Elio_collaboration_users_groups
            //                                   WHERE id = @id"
            //                                    , DatabaseHelper.CreateIntParameter("@id", groupId));

            //                session.ExecuteQuery(@"DELETE 
            //                                   FROM Elio_collaboration_users_group_retailors_mailbox
            //                                   WHERE collaboration_group_id = @collaboration_group_id 
            //                                   AND collaboration_vendors_resellers_id = @collaboration_vendors_resellers_id"
            //                                   , DatabaseHelper.CreateIntParameter("@id", groupId)
            //                                   , DatabaseHelper.CreateIntParameter("@collaboration_vendors_resellers_id", id));
            //            }

            #region Delete Collaboration User if not completed & ApplicationType is Collaboration

            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            ElioUsers collaborationPartner = loader.LoadSingle(@"SELECT * FROM Elio_users
                                                                 INNER JOIN Elio_collaboration_vendors_resellers 
                                                                    ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id
                                                                 WHERE Elio_collaboration_vendors_resellers.id = @id"
                                                                 , DatabaseHelper.CreateIntParameter("@id", id));

            if (collaborationPartner != null)
            {
                if (collaborationPartner.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) && collaborationPartner.UserApplicationType == Convert.ToInt32(UserApplicationType.Collaboration))
                {
                    loader.Delete(collaborationPartner);
                }
            }

            #endregion

            session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_library_group_members  
                                   WHERE vendor_reseller_id = @vendor_reseller_id"
                            , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", id));

            session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_group_retailors_mailbox  
                                   WHERE collaboration_vendors_resellers_id = @collaboration_vendors_resellers_id"
                            , DatabaseHelper.CreateIntParameter("@collaboration_vendors_resellers_id", id));

            session.ExecuteQuery(@"DELETE FROM Elio_collaboration_vendors_members_resellers  
                                   WHERE vendor_reseller_id = @vendor_reseller_id"
                            , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", id));

            session.ExecuteQuery(@"DELETE FROM Elio_collaboration_vendors_resellers  
                                   WHERE id = @id"
                            , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static void DeleteCollaborationUserInvitationById(int id, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_collaboration_users_invitations 
                                   SET is_public=0 
                                   , last_updated = @last_updated
                                   WHERE id = @id"
                                   , DatabaseHelper.CreateDateTimeParameter("@last_updated", DateTime.Now)
                                   , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static void DeleteUserCollaborationAllItems(int collaborationUserId, Types companyType, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

            string strQuery = @"SELECT * FROM Elio_collaboration_vendors_resellers 
                                WHERE 1 = 1 ";

            strQuery += (companyType == Types.Vendors) ? " AND master_user_id = @user_id" : " AND partner_user_id = @user_id";

            ElioCollaborationVendorsResellers vendRes = loader.LoadSingle(strQuery
                                                                          , DatabaseHelper.CreateIntParameter("@user_id", collaborationUserId));

            if (vendRes != null)
            {
                DataLoader<ElioCollaborationVendorResellerInvitations> invLoader = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);

                ElioCollaborationVendorResellerInvitations vendResInvitation = invLoader.LoadSingle(@"SELECT * FROM Elio_collaboration_vendor_reseller_invitations 
                                                                                                      WHERE vendor_reseller_id = @vendor_reseller_id"
                                                                                                      , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendRes.Id));

                if (vendResInvitation != null)
                {
                    invLoader.Delete(vendResInvitation);
                }

                DataLoader<ElioCollaborationUsersGroupMembers> groupMemberLoader = new DataLoader<ElioCollaborationUsersGroupMembers>(session);

                List<ElioCollaborationUsersGroupMembers> groupMember = groupMemberLoader.Load(@"SELECT * FROM Elio_collaboration_users_group_members 
                                                                                                WHERE creator_user_id = @creator_user_id 
                                                                                                AND group_retailor_id = @group_retailor_id"
                                                                                                , DatabaseHelper.CreateIntParameter("@creator_user_id", vendRes.MasterUserId)
                                                                                                , DatabaseHelper.CreateIntParameter("@group_retailor_id", vendRes.PartnerUserId));

                if (groupMember.Count > 0)
                {
                    foreach (ElioCollaborationUsersGroupMembers member in groupMember)
                    {
                        DataLoader<ElioCollaborationUsersGroupMaibox> groupMailboxLoader = new DataLoader<ElioCollaborationUsersGroupMaibox>(session);

                        List<ElioCollaborationUsersGroupMaibox> groupMailbox = groupMailboxLoader.Load(@"SELECT * FROM Elio_collaboration_users_group_mailbox
                                                                                                       WHERE group_id = @group_id
                                                                                                       AND (sender_user_id = @sender_user_id OR receiver_user_id = @receiver_user_id)
                                                                                                       AND (sender_user_id = @receiver_user_id OR receiver_user_id = @sender_user_id)"
                                                                                                       , DatabaseHelper.CreateIntParameter("@group_id", member.CollaborationGroupId)
                                                                                                       , DatabaseHelper.CreateIntParameter("@sender_user_id", vendRes.PartnerUserId)
                                                                                                       , DatabaseHelper.CreateIntParameter("@receiver_user_id", vendRes.MasterUserId));

                        if (groupMailbox.Count > 0)
                        {
                            foreach (ElioCollaborationUsersGroupMaibox grpMailbox in groupMailbox)
                            {
                                DataLoader<ElioCollaborationMailbox> grpMailboxLoader = new DataLoader<ElioCollaborationMailbox>(session);

                                ElioCollaborationMailbox mailbox = grpMailboxLoader.LoadSingle(@"SELECT * FROM Elio_collaboration_mailbox
                                                                                            WHERE id = @id"
                                                                                            , DatabaseHelper.CreateIntParameter("@id", grpMailbox.MailboxId));

                                if (mailbox != null)
                                {
                                    grpMailboxLoader.Delete(mailbox);
                                }

                                groupMailboxLoader.Delete(grpMailbox);
                            }
                        }

                        groupMemberLoader.Delete(member);
                    }
                }

                DataLoader<ElioCollaborationMailbox> mailboxLoader = new DataLoader<ElioCollaborationMailbox>(session);

                ElioCollaborationMailbox userMailbox = mailboxLoader.LoadSingle(@"SELECT * FROM Elio_collaboration_mailbox
                                                                              WHERE user_id = @user_id"
                                                                            , DatabaseHelper.CreateIntParameter("@user_id", vendRes.PartnerUserId));

                if (userMailbox != null)
                {
                    mailboxLoader.Delete(userMailbox);
                }
            }
        }

        public static List<ElioCollaborationUsersInvitations> GetCollaborationUserAllInvitationsByStatus(int userId, int isPublic, DBSession session)
        {
            DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
            return loader.Load(@"Select * from Elio_collaboration_users_invitations with (nolock) 
                                where user_id=@user_id 
                                and is_public=@is_public
                                order by date_created desc"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateIntParameter("@is_public", isPublic));
        }

        public static List<ElioCollaborationUsersInvitations> GetCollaborationUserInvitationsByVendorResellerId(int vendorResellerId, int isPublic, DBSession session)
        {
            DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
            return loader.Load(@"SELECT *
                                 FROM Elio_collaboration_users_invitations
                                 WHERE id 
                                    IN(
                                        SELECT user_invitation_id
                                        FROM Elio_collaboration_vendor_reseller_invitations
                                        WHERE vendor_reseller_id = @vendor_reseller_id                                         
                                      )
                                 AND is_public = @is_public 
                                 ORDER BY date_created DESC"
                               , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendorResellerId)
                               , DatabaseHelper.CreateIntParameter("@is_public", isPublic));
        }

        public static ElioCollaborationUsersInvitations GetCollaborationUserInvitationsByVendorResellerIdForReSend(int userId, int vendorResellerId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
            return loader.LoadSingle(@"SELECT Elio_collaboration_users_invitations.*, Elio_collaboration_vendor_reseller_invitations.recipient_email  
                                       FROM Elio_collaboration_users_invitations
                                       INNER JOIN Elio_collaboration_vendor_reseller_invitations
	                                       ON Elio_collaboration_vendor_reseller_invitations.user_invitation_id = Elio_collaboration_users_invitations.id
                                       INNER JOIN Elio_collaboration_vendors_resellers
	                                       ON Elio_collaboration_vendors_resellers.id = Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id
                                       WHERE 1 = 1
                                       AND Elio_collaboration_users_invitations.user_id = @user_id
                                       AND Elio_collaboration_vendors_resellers.id = @id"
                                       , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                       , DatabaseHelper.CreateIntParameter("@id", vendorResellerId));
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetCollaborationInvitationsSendByMaster(int masterUserId, DBSession session)
        {
            return GetCollaborationInvitationsSendByMaster(masterUserId, "", "", "", session);
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetCollaborationInvitationsSendByMaster(int masterUserId, string companyName, string invitationStatus, string country, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string query = @"SELECT Elio_collaboration_vendors_resellers.id, Elio_collaboration_vendors_resellers.master_user_id, 
                                    invitation_status, Elio_collaboration_vendors_resellers.partner_user_id, company_name, email, country,
                                    Elio_collaboration_vendor_reseller_invitations.is_new
                            FROM Elio_collaboration_vendors_resellers WITH (NOLOCK)
                            INNER JOIN Elio_users
                                    ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id 
                            INNER JOIN Elio_collaboration_vendor_reseller_invitations
		                            ON Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id = Elio_collaboration_vendors_resellers.id
			                            AND Elio_collaboration_vendor_reseller_invitations.user_id = Elio_collaboration_vendors_resellers.master_user_id
                            WHERE master_user_id = @master_user_id";

            if (companyName != "")
                query += " AND (company_name like '" + companyName + "%' OR email = '" + companyName + "') ";

            if (country != "")
                query += " AND country = '" + country + "' ";

            if (invitationStatus != "")
                query += " AND invitation_status = '" + invitationStatus + "' ";

            query += " ORDER BY invitation_status DESC";

            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId));
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetCollaborationInvitationsSendToMasterByChannelPartner(int resellerId, string companyName, string invitationStatus, string country, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string query = @"SELECT Elio_collaboration_vendors_resellers.id, Elio_collaboration_vendors_resellers.master_user_id, 
                                    invitation_status, Elio_collaboration_vendors_resellers.partner_user_id, company_name, email, country,
                                    Elio_collaboration_vendor_reseller_invitations.is_new
                            FROM Elio_collaboration_vendors_resellers WITH (NOLOCK)
                            INNER JOIN Elio_users
                                    ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id 
                            INNER JOIN Elio_collaboration_vendor_reseller_invitations
		                            ON Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id = Elio_collaboration_vendors_resellers.id
			                            AND Elio_collaboration_vendor_reseller_invitations.user_id = Elio_collaboration_vendors_resellers.partner_user_id
                            WHERE partner_user_id = @partner_user_id";

            if (companyName != "")
                query += " AND (company_name like '" + companyName + "%' OR email = '" + companyName + "') ";

            if (country != "")
                query += " AND country = '" + country + "' ";

            if (invitationStatus != "")
                query += " AND invitation_status = '" + invitationStatus + "' ";

            query += " ORDER BY invitation_status DESC";

            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));
        }

        //        public static List<ElioCollaborationVendorsResellersIJUsers> GetCollaborationInvitationsSendByMaster(int masterUserId, DBSession session)
        //        {
        //            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

        //            return loader.Load(@"SELECT Elio_collaboration_vendors_resellers.id, Elio_collaboration_vendors_resellers.master_user_id, 
        //                                       invitation_status, Elio_collaboration_vendors_resellers.partner_user_id, company_name, email, country
        //                                FROM Elio_collaboration_vendors_resellers WITH (NOLOCK)
        //                                INNER JOIN Elio_users
        //                                       ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id 
        //                                WHERE master_user_id = @master_user_id 
        //                                ORDER BY invitation_status DESC"
        //                                , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId));
        //        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetCollaborationInvitationsReceivedByMaster(int masterUserId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            return loader.Load(@"SELECT Elio_collaboration_vendors_resellers.id, Elio_collaboration_vendors_resellers.master_user_id, 
                                        invitation_status, Elio_collaboration_vendors_resellers.partner_user_id, company_name, email, country,
                                        Elio_collaboration_vendor_reseller_invitations.is_new
                                 FROM Elio_collaboration_vendors_resellers WITH (NOLOCK)
                                 INNER JOIN Elio_users
                                        ON Elio_collaboration_vendors_resellers.master_user_id = Elio_users.id 
                                 INNER JOIN Elio_collaboration_vendor_reseller_invitations
		                                ON Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id = Elio_collaboration_vendors_resellers.id
			                                AND Elio_collaboration_vendor_reseller_invitations.user_id = Elio_collaboration_vendors_resellers.master_user_id
                                 WHERE partner_user_id = @partner_user_id
                                 ORDER BY invitation_status DESC"
                                 , DatabaseHelper.CreateIntParameter("@partner_user_id", masterUserId));
        }

        public static bool HasCollaborationPartnersOrMessages(int userId, string companyType, out bool hasMessages, DBSession session)
        {
            hasMessages = false;
            string strQuery = @"SELECT DISTINCT Elio_collaboration_vendors_resellers.id AS vendors_resellers_id
                                FROM Elio_collaboration_vendors_resellers WITH (NOLOCK)
                                WHERE 1 = 1 
                                AND invitation_status = 'Confirmed' ";

            strQuery += (companyType == Types.Vendors.ToString()) ? " AND master_user_id = @user_id" : "AND partner_user_id = @user_id ";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            if (table.Rows.Count > 0)
            {
                strQuery = @"SELECT COUNT(id) AS count
                             FROM Elio_collaboration_users_mailbox
                             WHERE vendors_resellers_id = @vendors_resellers_id 
                             AND is_deleted = 0";

                DataTable tableUsersMailbox = session.GetDataTable(strQuery
                                                       , DatabaseHelper.CreateIntParameter("@vendors_resellers_id", Convert.ToInt32(table.Rows[0]["vendors_resellers_id"])));

                hasMessages = (Convert.ToInt32(tableUsersMailbox.Rows[0]["count"]) > 0) ? true : false;
            }

            return table.Rows.Count > 0 ? true : false;
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(int userId, int loggedInRoleId, bool isAdminRole, string invitationStatus, int isDeleted, int isPublic, string companyType, GlobalMethods.SearchCriteria criteria, string orderByClasuse, int libraryGroupId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string strQueryAll = @"SELECT Elio_collaboration_vendors_resellers.id, Elio_collaboration_vendors_resellers.master_user_id, 
                                       invitation_status, Elio_collaboration_vendors_resellers.partner_user_id, company_name, Elio_users.email, country, Elio_countries.region
                                 FROM Elio_collaboration_vendors_resellers WITH (NOLOCK)
                                 INNER JOIN Elio_users ";

            strQueryAll += (companyType == Types.Vendors.ToString()) ? " ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id "
                                                                  : " ON Elio_collaboration_vendors_resellers.master_user_id = Elio_users.id ";

            if (companyType == Types.Vendors.ToString() && libraryGroupId > 0)
            {
                strQueryAll += @" inner join Elio_collaboration_users_library_group_members lgm
                                    on lgm.vendor_reseller_id = Elio_collaboration_vendors_resellers.id ";
            }

            if (companyType == Types.Vendors.ToString() && loggedInRoleId > 0 && !isAdminRole)
                strQueryAll += @" inner join Elio_collaboration_vendors_members_resellers cvmr
                                        on cvmr.assign_by_user_id = Elio_collaboration_vendors_resellers.master_user_id 
                                        and cvmr.partner_user_id = Elio_collaboration_vendors_resellers.partner_user_id
                                inner join Elio_users_sub_accounts usa 
                                    on usa.user_id = Elio_collaboration_vendors_resellers.master_user_id 
                                        and usa.id = cvmr.sub_account_id ";

            strQueryAll += " INNER JOIN Elio_countries ON Elio_countries.country_name = Elio_users.country ";
            strQueryAll += @" LEFT JOIN Elio_collaboration_users_mailbox
	                        ON Elio_collaboration_users_mailbox.vendors_resellers_id = Elio_collaboration_vendors_resellers.id ";
            strQueryAll += (companyType == Types.Vendors.ToString()) ? " WHERE Elio_collaboration_vendors_resellers.master_user_id = " + userId + " " : "WHERE Elio_collaboration_vendors_resellers.partner_user_id = " + userId + " ";
            strQueryAll += " AND invitation_status = @invitation_status";
            //strQuery += " AND Elio_collaboration_users_mailbox.is_public = @is_public AND Elio_collaboration_users_mailbox.is_deleted = @is_deleted ";

            if (companyType == Types.Vendors.ToString() && loggedInRoleId > 0 && !isAdminRole)
                strQueryAll += @" and usa.is_confirmed = 1  
                               and usa.team_role_id = " + loggedInRoleId + " " +
                               "and usa.is_active = 1 ";

            if (companyType == Types.Vendors.ToString() && libraryGroupId > 0)
            {
                strQueryAll += @" and lgm.library_group_id = " + libraryGroupId + " ";
            }

            if (criteria != null)
            {
                if (criteria.CompanyName != "")
                {
                    strQueryAll += " AND company_name LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.CompanyName) + "%' ";
                }

                if (criteria.Country != "")
                {
                    strQueryAll += " AND country LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.Country) + "%' ";
                }

                if (criteria.Region != "")
                {
                    strQueryAll += " AND region LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.Region) + "%' ";
                }
            }

            //if (orderByClasuse == "")
            strQueryAll += " ORDER BY Elio_collaboration_users_mailbox.sysdate desc ";
            //else
            //    strQuery += " ORDER BY " + orderByClasuse + ", Elio_collaboration_users_mailbox.sysdate desc ";
            List<ElioCollaborationVendorsResellersIJUsers> vendorsResellersAll = loader.Load(strQueryAll
                                                                                          , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus)
                                                                                          , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                                                                                          , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted));

            //if (loggedInRoleId > 0 && !isAdminRole)
            //{
            //    string strQueryFree = @"SELECT Elio_collaboration_vendors_resellers.id, Elio_collaboration_vendors_resellers.master_user_id, 
            //                           invitation_status, Elio_collaboration_vendors_resellers.partner_user_id, company_name, Elio_users.email, country, region
            //                     FROM Elio_collaboration_vendors_resellers WITH (NOLOCK)
            //                     INNER JOIN Elio_users ";

            //    strQueryFree += (companyType == Types.Vendors.ToString()) ? " ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id "
            //                                                          : " ON Elio_collaboration_vendors_resellers.master_user_id = Elio_users.id ";

            //    strQueryFree += " INNER JOIN Elio_countries ON Elio_countries.country_name = Elio_users.country ";
            //    strQueryFree += @" LEFT JOIN Elio_collaboration_users_mailbox
            //             ON Elio_collaboration_users_mailbox.vendors_resellers_id = Elio_collaboration_vendors_resellers.id ";
            //    strQueryFree += (companyType == Types.Vendors.ToString()) ? " WHERE Elio_collaboration_vendors_resellers.master_user_id = " + userId + " " : "WHERE Elio_collaboration_vendors_resellers.partner_user_id = " + userId + " ";
            //    strQueryFree += " AND invitation_status = @invitation_status";

            //    strQueryFree += @" and Elio_collaboration_vendors_resellers.partner_user_id not in
            //                        (
            //                         select cvmr.partner_user_id 
            //                         from Elio_collaboration_vendors_members_resellers cvmr 
            //                         inner join Elio_users_sub_accounts usa 
            //                          on usa.id = cvmr.sub_account_id 
            //                           and usa.user_id = cvmr.assign_by_user_id
            //                         where usa.is_confirmed = 1
            //                         and usa.is_active = 1
            //                         and cvmr.assign_by_user_id = Elio_collaboration_vendors_resellers.master_user_id
            //                        )
            //                        and Elio_collaboration_vendors_resellers.is_active = 1";

            //    if (criteria != null)
            //    {
            //        if (criteria.CompanyName != "")
            //        {
            //            strQueryFree += " AND company_name LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.CompanyName) + "%' ";
            //        }

            //        if (criteria.Country != "")
            //        {
            //            strQueryFree += " AND country LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.Country) + "%' ";
            //        }

            //        if (criteria.Region != "")
            //        {
            //            strQueryFree += " AND region LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.Region) + "%' ";
            //        }
            //    }

            //    strQueryFree += " ORDER BY Elio_collaboration_users_mailbox.sysdate desc ";

            //    List<ElioCollaborationVendorsResellersIJUsers> vendorsResellersFree = loader.Load(strQueryFree
            //                                                                                  , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus)
            //                                                                                  , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
            //                                                                                  , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted));

            //    if (vendorsResellersFree.Count > 0)
            //        vendorsResellersAll.AddRange(vendorsResellersFree);
            //}

            List<ElioCollaborationVendorsResellersIJUsers> vendorsResellers = new List<ElioCollaborationVendorsResellersIJUsers>();

            if (vendorsResellersAll.Count > 0)
            {
                foreach (ElioCollaborationVendorsResellersIJUsers vndRsl in vendorsResellersAll)
                {
                    if (vendorsResellers.Count > 0)
                    {
                        bool exist = false;
                        foreach (ElioCollaborationVendorsResellersIJUsers vendorsReseller in vendorsResellers)
                        {
                            if (vendorsReseller.Id == vndRsl.Id)
                            {
                                exist = true;
                                break;
                            }
                        }

                        if (!exist)
                        {
                            vendorsResellers.Add(vndRsl);
                        }
                    }
                    else
                    {
                        vendorsResellers.Add(vndRsl);
                    }
                }

                List<ElioCollaborationVendorsResellersIJUsers> vendorsResellersFinallyUsers = new List<ElioCollaborationVendorsResellersIJUsers>();

                if (criteria != null && criteria.PartnerPrograms.Count > 0)
                {
                    string programs = "";
                    int count = 0;
                    foreach (string program in criteria.PartnerPrograms)
                    {
                        if (count < criteria.PartnerPrograms.Count - 1)
                            programs += "'" + program + "',";
                        else
                            programs += "'" + program + "'";

                        count++;
                    }

                    foreach (ElioCollaborationVendorsResellersIJUsers vendorReseller in vendorsResellers)
                    {
                        int vendResellerUser = (companyType == Types.Vendors.ToString()) ? vendorReseller.PartnerUserId : vendorReseller.MasterUserId;

                        DataTable table = session.GetDataTable(@"SELECT count(elio_partners.id) AS count 
                                                                 FROM elio_partners 
                                                                 INNER JOIN Elio_users_partners 
                                                                    ON Elio_users_partners.partner_id = elio_partners.id
                                                                 WHERE user_id = @user_id
                                                                 AND partner_description IN (" + programs + ")"
                                                                 , DatabaseHelper.CreateIntParameter("@user_id", vendResellerUser));

                        if (Convert.ToInt32(table.Rows[0]["count"]) > 0)
                        {
                            vendorsResellersFinallyUsers.Add(vendorReseller);
                        }
                    }

                    vendorsResellers.Clear();
                    vendorsResellers = vendorsResellersFinallyUsers.ToList();

                    return vendorsResellers;
                }
                else
                    return vendorsResellers;
            }
            else
                return vendorsResellers;

            //            List<ElioCollaborationUsersMailbox> vendorsResellersUsersMailBox = new List<ElioCollaborationUsersMailbox>();

            //            foreach (ElioCollaborationVendorsResellersIJUsers vendorsReseller in vendorsResellers)
            //            {
            //                DataLoader<ElioCollaborationUsersMailbox> loaderMailBox = new DataLoader<ElioCollaborationUsersMailbox>(session);

            //                ElioCollaborationUsersMailbox venRes = loaderMailBox.LoadSingle(@"SELECT  top 1 *
            //                                                                                  FROM Elio_collaboration_users_mailbox
            //                                                                                  where vendors_resellers_id = @vendors_resellers_id
            //                                                                                  order by sysdate desc"
            //                                                                                , DatabaseHelper.CreateIntParameter("@vendors_resellers_id", vendorsReseller.Id));

            //                vendorsResellersUsersMailBox.Add(venRes);
            //            }

            //            //vendorsResellersUsersMailBox.Sort();
            //            vendorsResellersUsersMailBox.OrderBy(x => x.Sysdate).ToList();

            //            foreach (ElioCollaborationUsersMailbox item in vendorsResellersUsersMailBox)
            //            {
            //                loader.LoadSingle(@"SELECT Elio_collaboration_vendors_resellers.id, 
            //	                                Elio_collaboration_vendors_resellers.master_user_id, 
            //	                                invitation_status, 
            //	                                Elio_collaboration_vendors_resellers.partner_user_id, 
            //	                                company_name, 
            //	                                email, 
            //	                                country, 
            //	                                region
            //	                            FROM Elio_collaboration_vendors_resellers WITH (NOLOCK)
            //	                            INNER JOIN Elio_users  
            //		                            ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id  
            //	                            INNER JOIN Elio_countries 
            //		                            ON Elio_countries.country_name = Elio_users.country  
            //	                            WHERE Elio_collaboration_vendors_resellers.id = @id
            //	                            ORDER BY Elio_collaboration_vendors_resellers.id"
            //                                , DatabaseHelper.CreateIntParameter("@id", item.VendorsResellersId));
            //            }

            //            return vendorsResellers;
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetCollaborationVendorsByChannelPartner(int partnerUserId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            return loader.Load(@"Select cvr.id, cvr.master_user_id, invitation_status, cvr.partner_user_id, cvr.tier_status
                                 company_name, u.email, u.country
                                 from Elio_collaboration_vendors_resellers cvr
                                 INNER JOIN Elio_users u
                                    ON cvr.master_user_id = u.id
                                 where 1 = 1  
                                 and is_active = 1 
                                 and partner_user_id = @partner_user_id"
                                 , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));
        }

        public static DataTable GetCollaborationVendorsByChannelPartnerTbl(int partnerUserId, DBSession session)
        {
            return session.GetDataTable(@"Select cvr.id, cvr.master_user_id,cvr.partner_user_id, cvr.invitation_status,
                                          case when isnull(cvr.tier_status, '') = '' then 'Not set'
                                          else cvr.tier_status end as tier_status, 
                                          u.company_name, u.company_logo, u.email, u.country
                                         from Elio_collaboration_vendors_resellers cvr
                                         INNER JOIN Elio_users u
                                            ON cvr.master_user_id = u.id
                                         where 1 = 1  
                                         and is_active = 1 
                                         and partner_user_id = @partner_user_id"
                                    , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetUserCollaborationPartners(int userId, DBSession session)
        {
            // 5 = Application Type of Collaboration User
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);
            return loader.Load(@"Select Elio_collaboration_vendors_resellers.id, Elio_collaboration_vendors_resellers.master_user_id, invitation_status, Elio_collaboration_vendors_resellers.partner_user_id, company_name, email, country
                                 from Elio_collaboration_vendors_resellers with (nolock)
                                 INNER JOIN Elio_users
                                 ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id
                                 where user_application_type = 5  
                                 and (invitation_status = 'Pending' or invitation_status = 'Rejected') 
                                 and master_user_id=@master_user_id"
                                 , DatabaseHelper.CreateIntParameter("@master_user_id", userId));
        }

        public static List<ElioUsers> GetAvailableUsersForCollaborationByType(string companyType, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"Select id, company_logo, company_name, user_application_type, email, country 
                                from elio_users with (nolock) 
                                where company_type=@company_type
                                and is_public = 1 
                                and account_status=1 
                                and id not in
                                (
	                                select partner_user_id
	                                from Elio_collaboration_vendors_resellers 
                                    where invitation_status='Confirmed'
                                )
                                order by id asc"
                                 , DatabaseHelper.CreateStringParameter("@company_type", companyType));
        }

        public static ElioCollaborationUsersInvitations GetUserInvitationById(int id, DBSession session)
        {
            DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
            return loader.LoadSingle(@"Select * from Elio_collaboration_users_invitations with (nolock) 
                                       where id=@id"
                               , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static bool ExistDefaultCollaborationUserInvitationById(int isDefault, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(id) as count 
                                                     from Elio_collaboration_users_invitations with (nolock) 
                                                     where is_default = @is_default"
                                                     , DatabaseHelper.CreateIntParameter("@is_default", isDefault));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static int GetUserCountMailBoxMessagesByNewAndViewStatusId(int vendorResellerId, int masterUserId, int partnerUserId, int sessionUserId, int isNew, int isViewed, int isDeleted, int isPublic, DBSession session)
        {
            string strQuery = @"SELECT Count(Elio_collaboration_users_mailbox.id) AS count
                                FROM Elio_collaboration_users_mailbox  
                                INNER JOIN Elio_collaboration_mailbox
	                                ON Elio_collaboration_mailbox.id = Elio_collaboration_users_mailbox.mailbox_id
                                WHERE vendors_resellers_id=@vendors_resellers_id
                                AND master_user_id = @master_user_id 
                                AND partner_user_id = @partner_user_id 
                                AND is_new = @is_new 
                                AND is_viewed = @is_viewed
                                AND Elio_collaboration_users_mailbox.is_deleted = @is_deleted 
                                AND Elio_collaboration_users_mailbox.is_public = @is_public 
                                AND Elio_collaboration_mailbox.user_id != @sessionUserId 
                                AND Elio_collaboration_mailbox.is_public = @is_public";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@vendors_resellers_id", vendorResellerId)
                                                    , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId)
                                                    , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId)
                                                    , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                    , DatabaseHelper.CreateIntParameter("@is_viewed", isViewed)
                                                    , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted)
                                                    , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                                                    , DatabaseHelper.CreateIntParameter("@sessionUserId", sessionUserId));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) : 0;
        }

        public static int GetNotificationsUserTotalCountMailBoxMessages(int userId, string companyType, int isNew, int isViewed, out int simpleMsgCount, out int groupMsgCount, DBSession session)
        {
            simpleMsgCount = 0;
            groupMsgCount = 0;

            string strQuery = @"SELECT 
                                Count(Elio_collaboration_users_mailbox.id) AS total_count
	                            ,sum(cast
                                    (
                                        case when collaboration_group_id is not null then 1 else 0 end as int
                                    )) as group_msgs_count,
                                    (Count(Elio_collaboration_users_mailbox.id) -sum(cast
                                    (
                                        case when collaboration_group_id is not null then 1 else 0 end as int
                                    ))) as simple_msgs_count 
                                FROM Elio_collaboration_users_mailbox  
                                INNER JOIN Elio_collaboration_mailbox
	                                ON Elio_collaboration_mailbox.id = Elio_collaboration_users_mailbox.mailbox_id 
                                LEFT JOIN Elio_collaboration_users_group_retailors_mailbox
	                                ON Elio_collaboration_users_group_retailors_mailbox.collaboration_users_mailbox = Elio_collaboration_users_mailbox.id
                                WHERE 1 = 1 ";

            strQuery += (companyType == Types.Vendors.ToString()) ? " AND Elio_collaboration_users_mailbox.master_user_id = " + userId + "" : " AND Elio_collaboration_users_mailbox.partner_user_id =" + userId + "";

            strQuery += @" AND is_new = @is_new 
                           AND is_viewed = @is_viewed 
                           AND Elio_collaboration_mailbox.user_id != @userId 
                           AND Elio_collaboration_users_mailbox.is_public = 1 
                           AND Elio_collaboration_users_mailbox.is_deleted = 0";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@userId", userId)
                                                    , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                    , DatabaseHelper.CreateIntParameter("@is_viewed", isViewed)
                                                    , DatabaseHelper.CreateIntParameter("@sessionUserId", userId));

            if (table.Rows.Count > 0)
            {
                simpleMsgCount = (!string.IsNullOrEmpty(table.Rows[0]["simple_msgs_count"].ToString())) ? Convert.ToInt32(table.Rows[0]["simple_msgs_count"]) : 0;
                groupMsgCount = (!string.IsNullOrEmpty(table.Rows[0]["group_msgs_count"].ToString())) ? Convert.ToInt32(table.Rows[0]["group_msgs_count"]) : 0;
            }

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["total_count"]) : 0;
        }

        public static int GetUserTotalSimpleNewUnreadMailBoxMessagesNotification(int userId, string companyType, int isNew, int isViewed, int isDeleted, int isPublic, out int simpleMsg, out int groupMsg, DBSession session)
        {
            simpleMsg = 0;
            groupMsg = 0;
            int totalMessages = 0;

            string strQuery = @"SELECT 
                                Count(Elio_collaboration_users_mailbox.id) AS total_simple_msgs_count	                           
                               FROM Elio_collaboration_users_mailbox  
                               INNER JOIN Elio_collaboration_mailbox
	                               ON Elio_collaboration_mailbox.id = Elio_collaboration_users_mailbox.mailbox_id
                               WHERE 1 = 1                              
                               AND Elio_collaboration_mailbox.is_public = @is_public ";

            strQuery += (companyType == Types.Vendors.ToString()) ? " AND Elio_collaboration_users_mailbox.master_user_id = " + userId + "" : " AND Elio_collaboration_users_mailbox.partner_user_id =" + userId + "";

            strQuery += @" AND is_new = @is_new 
                           AND is_viewed = @is_viewed 
                           AND Elio_collaboration_mailbox.user_id != @userId 
                           AND Elio_collaboration_users_mailbox.is_deleted = @is_deleted 
                           AND Elio_collaboration_users_mailbox.is_public = @is_public";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@userId", userId)
                                                    , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                    , DatabaseHelper.CreateIntParameter("@is_viewed", isViewed)
                                                    , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted)
                                                    , DatabaseHelper.CreateIntParameter("@is_public", isPublic));

            if (table.Rows.Count > 0)
            {
                simpleMsg = Convert.ToInt32(table.Rows[0]["total_simple_msgs_count"]);
                totalMessages = simpleMsg;
            }

            table.Clear();
            strQuery = "";

            strQuery = @"
                        SELECT distinct count(mailbox_id) AS total_group_msgs_count
                          FROM Elio_collaboration_users_group_mailbox
                        INNER JOIN Elio_collaboration_mailbox
                          ON Elio_collaboration_mailbox.id = Elio_collaboration_users_group_mailbox.mailbox_id 
                          where 1 = 1
                          AND receiver_user_id = @user_id
                          AND sender_user_id != @user_id
                          and is_new = @is_new
                          and is_viewed = @is_viewed
                          AND Elio_collaboration_users_group_mailbox.is_deleted = @is_deleted
                          AND Elio_collaboration_users_group_mailbox.is_public = @is_public 
                          AND Elio_collaboration_mailbox.is_public = @is_public 
                        ";


            table = session.GetDataTable(strQuery
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                , DatabaseHelper.CreateIntParameter("@is_viewed", isViewed)
                                                , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted)
                                                , DatabaseHelper.CreateIntParameter("@is_public", isPublic));

            if (table.Rows.Count > 0)
            {
                groupMsg = Convert.ToInt32(table.Rows[0]["total_group_msgs_count"]);
                totalMessages += groupMsg;
            }

            return totalMessages;
        }

        public static List<ElioCollaborationMailboxIJUsersMailBox> GetSendersOfNewNotViewedCollaborationMailBoxMessages(DBSession session)
        {
            DataLoader<ElioCollaborationMailboxIJUsersMailBox> loader = new DataLoader<ElioCollaborationMailboxIJUsersMailBox>(session);

            return loader.Load(@"SELECT *
                                FROM Elio_collaboration_mailbox
                                inner join Elio_collaboration_users_mailbox
	                            ON Elio_collaboration_users_mailbox.mailbox_id = Elio_collaboration_mailbox.id
                                where is_new = 1 and is_viewed = 0");
        }

        public static ElioCollaborationVendorsResellers GetCollaborationByVendorAndResellerId(int masterUserId, int partnerUserId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

            return loader.LoadSingle(@"SELECT * 
                                       FROM Elio_collaboration_vendors_resellers 
                                       WHERE master_user_id = @master_user_id 
                                       AND partner_user_id = @partner_user_id"
                                       , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId)
                                       , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));
        }

        public static ElioCollaborationVendorsResellers GetOrAllocateCollaborationByVendorAndResellerId(int masterUserId, int partnerUserId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

            ElioCollaborationVendorsResellers vendorReseller = loader.LoadSingle(@"SELECT * 
                                       FROM Elio_collaboration_vendors_resellers 
                                       WHERE 
                                        (master_user_id = @master_user_id AND partner_user_id = @partner_user_id)
                                       OR
                                        (master_user_id = @partner_user_id AND partner_user_id = @master_user_id)"
                                       , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId)
                                       , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));

            if (vendorReseller == null)
            {
                vendorReseller = new ElioCollaborationVendorsResellers();

                vendorReseller.MasterUserId = masterUserId;
                vendorReseller.PartnerUserId = partnerUserId;
                vendorReseller.IsActive = 1;
                vendorReseller.InvitationStatus = CollaborateInvitationStatus.Pending.ToString();
                vendorReseller.Sysdate = DateTime.Now;
                vendorReseller.LastUpdated = DateTime.Now;

                DataLoader<ElioCollaborationVendorsResellers> loader2 = new DataLoader<ElioCollaborationVendorsResellers>(session);
                loader2.Insert(vendorReseller);
            }

            return vendorReseller;
        }

        public static ElioCollaborationVendorsResellers GetCollaborationBetweenResellersId(int masterUserId, int partnerUserId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

            return loader.LoadSingle(@"SELECT * 
                                       FROM Elio_collaboration_vendors_resellers 
                                       WHERE 1 = 1 
                                       AND (master_user_id = @master_user_id AND partner_user_id = @partner_user_id) 
                                       OR (master_user_id = @partner_user_id AND partner_user_id = @master_user_id)"
                                       , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId)
                                       , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));
        }

        public static List<ElioCollaborationVendorsResellers> GetUserCollaborationVendorResellersByStatus(int userId, string companyType, string invitationStatus, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

            string strQuery = @"SELECT *
                                 FROM Elio_collaboration_vendors_resellers WITH (NOLOCK) ";

            strQuery += (companyType == Types.Vendors.ToString()) ? " WHERE master_user_id = @user_id "
                                                                  : " WHERE partner_user_id = @user_id ";

            strQuery += " AND invitation_status=@invitation_status";

            return loader.Load(strQuery
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));
        }

        public static ElioCollaborationVendorsResellers GetCollaborationVendorResellerById(int id, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

            return loader.LoadSingle(@"SELECT * 
                                       FROM Elio_collaboration_vendors_resellers 
                                       WHERE id = @id"
                                       , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioUsers> GetCollaborationVendorsIDByResellerID(int resellerId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load(@"SELECT u.id 
                                       FROM Elio_users u
                                       INNER JOIN Elio_collaboration_vendors_resellers cvr on cvr.master_user_id = u.id 
                                       WHERE partner_user_id = @partner_user_id"
                                       , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));
        }

        public static List<ElioCollaborationMailbox> GetUserCollaborationSendMails(int userId, DBSession session)
        {
            DataLoader<ElioCollaborationMailbox> loader = new DataLoader<ElioCollaborationMailbox>(session);

            return loader.Load(@"SELECT * 
                                 FROM Elio_collaboration_mailbox 
                                 WHERE user_id = @user_id 
                                 AND is_public = 1 
                                 ORDER BY date_created desc"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioCollaborationMailbox GetCollaborationMailBoxById(int id, DBSession session)
        {
            DataLoader<ElioCollaborationMailbox> loader = new DataLoader<ElioCollaborationMailbox>(session);

            return loader.LoadSingle(@"SELECT * 
                                 FROM Elio_collaboration_mailbox 
                                 WHERE id = @id"
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioCollaborationUsersMailbox GetUsersCollaborationMailBoxByIdAndUserType(int mailBoxId, int userId, string companyType, DBSession session)
        {
            DataLoader<ElioCollaborationUsersMailbox> loader = new DataLoader<ElioCollaborationUsersMailbox>(session);

            string strQuery = @"SELECT * 
                                 FROM Elio_collaboration_users_mailbox 
                                 WHERE mailbox_id = @mailbox_id ";

            strQuery += (companyType == Types.Vendors.ToString()) ? " AND partner_user_id = @user_id" : " AND master_user_id = @user_id";

            return loader.LoadSingle(strQuery
                                , DatabaseHelper.CreateIntParameter("@mailbox_id", mailBoxId)
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioCollaborationUsersMailbox> LoadCollaborationUsersMailBoxByVendResId(int vendResId, int isDeleted, string joinClause, int collaborationGroupId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersMailbox> loader = new DataLoader<ElioCollaborationUsersMailbox>(session);

            string strQuery = @"SELECT * 
                                 FROM Elio_collaboration_users_mailbox ";

            if (joinClause != string.Empty && collaborationGroupId != -1)
                strQuery += @"INNER JOIN Elio_collaboration_users_group_retailors_mailbox 
	                            ON Elio_collaboration_users_group_retailors_mailbox.mailbox_id = Elio_collaboration_users_mailbox.mailbox_id 
                                AND Elio_collaboration_users_mailbox.id = Elio_collaboration_users_group_retailors_mailbox.collaboration_users_mailbox ";

            strQuery += " WHERE vendors_resellers_id = @vendors_resellers_id AND is_deleted = @is_deleted ";

            if (joinClause != string.Empty && collaborationGroupId != -1)
                strQuery += " AND collaboration_group_id IN (" + collaborationGroupId + ")";

            strQuery += " ORDER BY Elio_collaboration_users_mailbox.sysdate";

            return loader.Load(strQuery
                                //"WHERE vendors_resellers_id = @vendors_resellers_id 
                                //AND is_deleted = @is_deleted
                                //ORDER BY sysdate"
                                , DatabaseHelper.CreateIntParameter("@vendors_resellers_id", vendResId)
                                , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted));
        }

        public static List<ElioCollaborationUsersMailbox> ChangeMailboxStatusByUserId(int vendResId, int isDeleted, int isPublic, int sessionUserId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersMailbox> loader = new DataLoader<ElioCollaborationUsersMailbox>(session);

            string strQuery = @"SELECT * 
                                 FROM Elio_collaboration_users_mailbox ";
            strQuery += @"INNER JOIN Elio_collaboration_mailbox
	                        ON Elio_collaboration_mailbox.id = Elio_collaboration_users_mailbox.mailbox_id";

            strQuery += " WHERE vendors_resellers_id = @vendors_resellers_id AND is_deleted = @is_deleted AND Elio_collaboration_users_mailbox.is_public = @is_public AND user_id != @user_id AND Elio_collaboration_mailbox.is_public = @is_public ";

            strQuery += " ORDER BY Elio_collaboration_users_mailbox.sysdate";

            return loader.Load(strQuery
                                , DatabaseHelper.CreateIntParameter("@vendors_resellers_id", vendResId)
                                , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted)
                                , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                                , DatabaseHelper.CreateIntParameter("@user_id", sessionUserId));
        }

        public static bool ExistMessageAndCollaborationToUsersMailBox(int vendResId, int mailBoxId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count 
                                 FROM Elio_collaboration_users_mailbox 
                                 WHERE vendors_resellers_id = @vendors_resellers_id 
                                 AND mailbox_id = @mailbox_id"
                                , DatabaseHelper.CreateIntParameter("@vendors_resellers_id", vendResId)
                                , DatabaseHelper.CreateIntParameter("@mailbox_id", mailBoxId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static void DeleteCollaborationUsersMailBoxById(int collaborationUserMailBoxId, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_collaboration_users_mailbox 
                                        SET is_deleted = 1,
                                            date_deleted = @date_deleted, 
                                            is_public = 0 
                                   WHERE id = @id"
                                , DatabaseHelper.CreateIntParameter("@is_deleted", 1)
                                , DatabaseHelper.CreateDateTimeParameter("@date_deleted", DateTime.Now)
                                , DatabaseHelper.CreateIntParameter("@is_public", 0)
                                , DatabaseHelper.CreateIntParameter("@id", collaborationUserMailBoxId));
        }

        public static List<ElioUsers> GetCollaborationPartnersImagesUrlByMailBoxIdAndUserType(int mailBoxId, string companyType, DBSession session)
        {
            //DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            List<ElioUsers> usersDetails = new List<ElioUsers>();
            //List<ElioUsers> usersImgsAndName = new List<ElioUsers>();

            string strQuery = @"SELECT Elio_users.id as id, 
                                                Elio_collaboration_users_mailbox.master_user_id as master_user_id, 
                                                Elio_collaboration_users_mailbox.partner_user_id as partner_user_id, 
                                                company_logo, 
                                                company_name, 
                                                company_type 
                                             FROM Elio_users
                                             INNER JOIN Elio_collaboration_users_mailbox ";
            strQuery += (companyType == Types.Vendors.ToString()) ? " ON Elio_collaboration_users_mailbox.partner_user_id = Elio_users.id " : " ON Elio_collaboration_users_mailbox.master_user_id = Elio_users.id ";
            strQuery += " WHERE mailbox_id= @mailbox_id";

            DataTable table = session.GetDataTable(strQuery
                                 , DatabaseHelper.CreateIntParameter("@mailbox_id", mailBoxId));

            foreach (DataRow row in table.Rows)
            {
                ElioUsers user = null;

                if (row["company_type"].ToString() == EnumHelper.GetDescription(Types.Resellers).ToString())
                    user = Sql.GetUserById(Convert.ToInt32(row["partner_user_id"].ToString()), session);

                else if (row["company_type"].ToString() == Types.Vendors.ToString())
                    user = Sql.GetUserById(Convert.ToInt32(row["master_user_id"].ToString()), session);

                if (user != null)
                {
                    if (string.IsNullOrEmpty(user.CompanyLogo))
                        user.CompanyLogo = "~/images/icons/partners_th_party_2.png";

                    usersDetails.Add(user);
                }
            }

            return usersDetails;
        }

        public static bool ShowSelectedPartnersMailsByUserType(int userId, int mailBoxId, int partnerUserId, int masterUserId, string companyType, out int? isNew, out int? isViewed, out int? isDeleted, out DateTime? dateViewed, DBSession session)
        {
            isNew = 0;
            isViewed = 0;
            isDeleted = 0;
            dateViewed = null;

            string strQuery = @"SELECT count(Elio_collaboration_mailbox.id) as count, is_new, is_viewed, date_viewed, is_deleted
                                FROM Elio_collaboration_mailbox 
                                INNER JOIN Elio_collaboration_users_mailbox
	                                ON Elio_collaboration_users_mailbox.mailbox_id = Elio_collaboration_mailbox.id
                                WHERE user_id = @user_id 
                                AND Elio_collaboration_mailbox.is_public = 1 ";

            strQuery += (companyType == Types.Vendors.ToString()) ? " AND partner_user_id= " + partnerUserId + " " : " AND master_user_id= " + masterUserId + " ";
            strQuery += " AND Elio_collaboration_mailbox.id= @id ";
            strQuery += " GROUP BY Elio_collaboration_mailbox.id, is_new, is_viewed, date_viewed, is_deleted";

            DataTable tbl = session.GetDataTable(strQuery
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                , DatabaseHelper.CreateIntParameter("@id", mailBoxId));

            if (tbl.Rows.Count > 0)
            {
                if (Convert.ToInt32(tbl.Rows[0]["count"]) > 0)
                {
                    isNew = Convert.ToInt32(tbl.Rows[0]["is_new"]);
                    isViewed = Convert.ToInt32(tbl.Rows[0]["is_viewed"]);
                    isDeleted = Convert.ToInt32(tbl.Rows[0]["is_deleted"]);
                    if (!string.IsNullOrEmpty(tbl.Rows[0]["date_viewed"].ToString()))
                        dateViewed = Convert.ToDateTime(tbl.Rows[0]["date_viewed"]);

                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static List<ElioCollaborationMailboxIJUsersMailBox> GetUserMailBoxList(int userId, string companyType, DBSession session)
        {
            DataLoader<ElioCollaborationMailboxIJUsersMailBox> loader = new DataLoader<ElioCollaborationMailboxIJUsersMailBox>(session);

            string strQuery = @"SELECT *
                                  FROM Elio_collaboration_mailbox
                                  INNER JOIN Elio_collaboration_users_mailbox ON Elio_collaboration_mailbox.id =  Elio_collaboration_users_mailbox.mailbox_id ";

            strQuery += (companyType == Types.Vendors.ToString()) ? " where master_user_id = @user_id " : " where partner_user_id=  @user_id ";
            strQuery += " order by Elio_collaboration_users_mailbox.id DESC";

            return loader.Load(strQuery, DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioCollaborationMailboxIJUsersMailBox GetCollaborationUsersMailBoxByMailboxId(int mailboxId, int partnerUserId, DBSession session)
        {
            DataLoader<ElioCollaborationMailboxIJUsersMailBox> loader = new DataLoader<ElioCollaborationMailboxIJUsersMailBox>(session);

            return loader.LoadSingle(@"SELECT mailbox_id as mailboxid, *
                                        FROM Elio_collaboration_mailbox
                                        INNER JOIN Elio_collaboration_users_mailbox ON Elio_collaboration_mailbox.id =  Elio_collaboration_users_mailbox.mailbox_id
                                        where Elio_collaboration_mailbox.id = @id 
                                        AND partner_user_id = @partner_user_id"
                                        , DatabaseHelper.CreateIntParameter("@id", mailboxId)
                                        , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId));
        }

        public static ElioCollaborationUsersGroupMaibox GetCollaborationUsersGroupMailBoxByMailboxId(int mailboxId, int receiverUserId, int isPublic, DBSession session)
        {
            DataLoader<ElioCollaborationUsersGroupMaibox> loader = new DataLoader<ElioCollaborationUsersGroupMaibox>(session);

            return loader.LoadSingle(@"SELECT *
                                        FROM Elio_collaboration_users_group_mailbox
                                        INNER JOIN Elio_collaboration_mailbox 
                                            ON Elio_collaboration_users_group_mailbox.mailbox_id = Elio_collaboration_mailbox.id 
                                        where Elio_collaboration_users_group_mailbox.is_public = @is_public
                                        AND mailbox_id = @mailbox_id 
                                        AND receiver_user_id = @receiver_user_id"
                                        , DatabaseHelper.CreateIntParameter("@mailbox_id", mailboxId)
                                        , DatabaseHelper.CreateIntParameter("@receiver_user_id", receiverUserId)
                                        , DatabaseHelper.CreateIntParameter("@is_public", isPublic));
        }

        public static bool IsGroupMailBoxByMailboxId(int mailboxId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(*) as count
                                                     FROM Elio_collaboration_users_mailbox cum
                                                     INNER JOIN Elio_collaboration_users_group_retailors_mailbox cugrm
	                                                     ON cugrm.collaboration_users_mailbox = cum.id
                                                            AND cugrm.mailbox_id = cum.mailbox_id
                                                     WHERE cum.mailbox_id = @mailbox_id"
                                        , DatabaseHelper.CreateIntParameter("@mailbox_id", mailboxId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static ElioCollaborationUsersLibraryFiles GetCollaborationUserLibraryFileById(int id, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_collaboration_users_library_files
                                 where id = @id"
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioOnboardingUsersLibraryFiles GetOnboardingUserLibraryFileById(int id, DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFiles> loader = new DataLoader<ElioOnboardingUsersLibraryFiles>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_onboarding_users_library_files
                                 where id = @id"
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioCollaborationLibraryFilesDefaultCategories> GetCollaborationUserLibraryPublicDefaultFilesCategories(DBSession session)
        {
            DataLoader<ElioCollaborationLibraryFilesDefaultCategories> loader = new DataLoader<ElioCollaborationLibraryFilesDefaultCategories>(session);
            return loader.Load(@"SELECT *  
                                 FROM Elio_collaboration_library_files_default_categories 
                                 WHERE is_default = 1
                                 AND is_public=1");
        }

        public static List<ElioOnboardingLibraryFilesDefaultCategories> GetOnboardingUserLibraryPublicDefaultFilesCategories(DBSession session)
        {
            DataLoader<ElioOnboardingLibraryFilesDefaultCategories> loader = new DataLoader<ElioOnboardingLibraryFilesDefaultCategories>(session);
            return loader.Load(@"SELECT *  
                                 FROM Elio_onboarding_library_files_default_categories 
                                 WHERE is_default = 1
                                 AND is_public=1
                                 ORDER BY id");
        }

        public static List<ElioCollaborationUsersLibraryFilesCategories> GetCollaborationUserLibraryPublicFilesCategories(int userId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFilesCategories> loader = new DataLoader<ElioCollaborationUsersLibraryFilesCategories>(session);
            return loader.Load(@"SELECT *  
                                 FROM Elio_collaboration_users_library_files_categories 
                                 WHERE user_id = @user_id
                                 AND is_public=1
                                 ORDER BY category_description"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioOnboardingUsersLibraryFilesCategories> GetOnboardingUserLibraryPublicFilesCategories(int userId, DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFilesCategories> loader = new DataLoader<ElioOnboardingUsersLibraryFilesCategories>(session);
            return loader.Load(@"SELECT *  
                                 FROM Elio_onboarding_users_library_files_categories 
                                 WHERE user_id = @user_id
                                 AND is_public=1
                                 ORDER BY category_description"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetCollaborationLibraryPublicDefaultFilesCategoriesTbl(DBSession session)
        {
            return session.GetDataTable(@"SELECT *  
                                         FROM Elio_collaboration_library_files_default_categories 
                                         WHERE is_default = 1
                                         AND is_public=1");
        }

        public static DataTable GetCollaborationUserLibraryPublicFilesCategoriesTbl(int userId, DBSession session)
        {
            return session.GetDataTable(@"SELECT *  
                                         FROM Elio_collaboration_users_library_files_categories 
                                         WHERE user_id = @user_id
                                         AND is_public=1
                                         ORDER BY category_description"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetOnboardingUserLibraryPublicFilesCategoriesTbl(int userId, DBSession session)
        {
            return session.GetDataTable(@"SELECT *  
                                         FROM Elio_onboarding_users_library_files_categories 
                                         WHERE user_id = @user_id
                                         AND is_public = 1
                                         ORDER BY category_description"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioOnboardingUsersLibraryFilesCategories> GetOnboardingUserLibraryPublicFilesCategoriesOrderByID(int userId, DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFilesCategories> loader = new DataLoader<ElioOnboardingUsersLibraryFilesCategories>(session);

            return loader.Load(@"SELECT *  
                                FROM Elio_onboarding_users_library_files_categories 
                                WHERE user_id = @user_id
                                AND is_public = 1
                                ORDER BY id"
                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioCollaborationLibraryFilesDefaultCategories GetCollaborationUserLibraryPublicDefaultFilesCategoriesByCategory(string category, DBSession session)
        {
            DataLoader<ElioCollaborationLibraryFilesDefaultCategories> loader = new DataLoader<ElioCollaborationLibraryFilesDefaultCategories>(session);
            return loader.LoadSingle(@"SELECT *  
                                 FROM Elio_collaboration_library_files_default_categories 
                                 WHERE is_default = 1
                                 AND is_public=1 
                                 AND category_description = @category_description"
                                 , DatabaseHelper.CreateStringParameter("@category_description", category));
        }

        public static ElioCollaborationLibraryFilesDefaultCategories GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(int id, DBSession session)
        {
            DataLoader<ElioCollaborationLibraryFilesDefaultCategories> loader = new DataLoader<ElioCollaborationLibraryFilesDefaultCategories>(session);
            return loader.LoadSingle(@"SELECT *  
                                 FROM Elio_collaboration_library_files_default_categories 
                                 WHERE is_default = 1
                                 AND is_public=1 
                                 AND id = @id"
                                 , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioCollaborationUsersLibraryFilesCategories GetCollaborationUserLibraryPublicFilesCategoriesById(int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFilesCategories> loader = new DataLoader<ElioCollaborationUsersLibraryFilesCategories>(session);
            return loader.LoadSingle(@"SELECT *  
                                         FROM Elio_collaboration_users_library_files_categories 
                                         WHERE user_id = @user_id
                                         AND is_public = 1 
                                         AND id = @id"
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                 , DatabaseHelper.CreateIntParameter("@id", categoryId));
        }

        public static ElioOnboardingUsersLibraryFilesCategories GetOnboardingUserLibraryPublicFilesCategoriesById(int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFilesCategories> loader = new DataLoader<ElioOnboardingUsersLibraryFilesCategories>(session);
            return loader.LoadSingle(@"SELECT *  
                                         FROM Elio_onboarding_users_library_files_categories 
                                         WHERE user_id = @user_id
                                         AND is_public = 1 
                                         AND id = @id"
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                 , DatabaseHelper.CreateIntParameter("@id", categoryId));
        }

        public static List<ElioCollaborationUsersLibraryFiles> GetUserCollaborationLibraryFilesUploadedByCategoryId(int userId, int categoryId, int isPublic, bool isPartner, DBSession session)
        {
            string strQuery = @"SELECT *  
                                 FROM Elio_collaboration_users_library_files 
                                 WHERE 1 = 1 
                                 AND user_id = @user_id 
                                 AND uploaded_by_user_id = @uploaded_by_user_id ";
            if (isPartner)
            {
                strQuery += @" AND mailbox_id <> -1 ";
            }

            strQuery += @" AND category_id = @category_id
                                 AND is_public = @is_public";

            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);

            return loader.Load(strQuery
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@uploaded_by_user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@category_id", categoryId)
                                , DatabaseHelper.CreateIntParameter("@is_public", isPublic));
        }

        public static int GetUserCollaborationLibraryFilesBySenderUserId(int receiverUserId, int senderUserId, int isPublic, int isNew, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) AS count  
                                 FROM Elio_collaboration_users_library_files 
                                 WHERE 1 = 1 
                                 AND uploaded_by_user_id = @uploaded_by_user_id 
                                 AND user_id = @user_id 
                                 AND mailbox_id <> -1 
                                 AND is_new = @is_new 
                                 AND is_public = @is_public";

            DataTable table = session.GetDataTable(strQuery
                                , DatabaseHelper.CreateIntParameter("@user_id", receiverUserId)
                                , DatabaseHelper.CreateIntParameter("@uploaded_by_user_id", senderUserId)
                                , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                , DatabaseHelper.CreateIntParameter("@is_public", isPublic));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) : 0;
        }

        public static ElioCollaborationUsersLibraryFiles GetUserCollaborationLibraryFilesById(int id, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
            return loader.LoadSingle(@"SELECT *  
                                 FROM Elio_collaboration_users_library_files 
                                 WHERE 1 = 1 
                                 AND id = @id"
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioCollaborationUsersLibraryFiles> GetCollaborationUserLibraryFilesByCategoryId(int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
            return loader.Load(@"SELECT *  
                                 FROM Elio_collaboration_users_library_files 
                                 WHERE 1 = 1 
                                 AND category_id = @category_id
                                 AND user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@category_id", categoryId)
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioOnboardingUsersLibraryFiles> GetOnboardingUserLibraryFilesByCategoryId(int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFiles> loader = new DataLoader<ElioOnboardingUsersLibraryFiles>(session);
            return loader.Load(@"SELECT *  
                                 FROM Elio_onboarding_users_library_files 
                                 WHERE 1 = 1 
                                 AND category_id = @category_id
                                 AND user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@category_id", categoryId)
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioCollaborationBlobFiles GetCollaborationBlobFileByFileId(int fileId, DBSession session)
        {
            DataLoader<ElioCollaborationBlobFiles> loader = new DataLoader<ElioCollaborationBlobFiles>(session);
            return loader.LoadSingle(@"SELECT *  
                                 FROM Elio_collaboration_blob_files 
                                 WHERE 1 = 1 
                                 AND library_files_id = @library_files_id"
                                , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
        }

        public static ElioCollaborationBlobPreviewFiles GetCollaborationBlobPreviewFileByFileId(int fileId, DBSession session)
        {
            DataLoader<ElioCollaborationBlobPreviewFiles> loader = new DataLoader<ElioCollaborationBlobPreviewFiles>(session);
            return loader.LoadSingle(@"SELECT *  
                                 FROM Elio_collaboration_blob_preview_files 
                                 WHERE 1 = 1 
                                 AND library_files_id = @library_files_id"
                                , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
        }

        public static ElioCollaborationBlobFiles GetTableBlobFileByFileId(string tableName, int fileId, DBSession session)
        {
            DataLoader<ElioCollaborationBlobFiles> loader = new DataLoader<ElioCollaborationBlobFiles>(session);
            return loader.LoadSingle(@"SELECT *  
                                 FROM " + tableName + " " +
                                 "WHERE 1 = 1 " +
                                 "AND library_files_id = @library_files_id"
                                , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
        }

        public static ElioCollaborationBlobPreviewFiles GetTableBlobPreviewFileByFileId(string tableName, int fileId, DBSession session)
        {
            DataLoader<ElioCollaborationBlobPreviewFiles> loader = new DataLoader<ElioCollaborationBlobPreviewFiles>(session);
            return loader.LoadSingle(@"SELECT *  
                                 FROM " + tableName + " " +
                                 "WHERE 1 = 1 " +
                                 "AND library_files_id = @library_files_id"
                                , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
        }

        public static List<ElioCollaborationBlobFiles> GetTableBlobFilesSizeByCategoryId(int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioCollaborationBlobFiles> loader = new DataLoader<ElioCollaborationBlobFiles>(session);
            return loader.Load(@"SELECT bf.file_size
                                    FROM Elio_collaboration_blob_files bf
                                    inner join Elio_collaboration_users_library_files lf
	                                on lf.id = bf.library_files_id
                                where lf.category_id = @category_id
                                and user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@category_id", categoryId)
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioOnboardingBlobFiles> GetTableOnboardingBlobFilesSizeByCategoryId(int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioOnboardingBlobFiles> loader = new DataLoader<ElioOnboardingBlobFiles>(session);
            return loader.Load(@"SELECT bf.file_size
                                    FROM Elio_onboarding_blob_files bf
                                    inner join Elio_onboarding_users_library_files lf
	                                on lf.id = bf.library_files_id
                                where lf.category_id = @category_id
                                and user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@category_id", categoryId)
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioOnboardingBlobFiles GetOnboardingBlobFileByFileId(int fileId, DBSession session)
        {
            DataLoader<ElioOnboardingBlobFiles> loader = new DataLoader<ElioOnboardingBlobFiles>(session);
            return loader.LoadSingle(@"SELECT *  
                                 FROM Elio_onboarding_blob_files 
                                 WHERE 1 = 1 
                                 AND library_files_id = @library_files_id"
                                , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
        }

        public static ElioOnboardingBlobPreviewFiles GetOnboardingBlobPreviewFileByFileId(int fileId, DBSession session)
        {
            DataLoader<ElioOnboardingBlobPreviewFiles> loader = new DataLoader<ElioOnboardingBlobPreviewFiles>(session);
            return loader.LoadSingle(@"SELECT *  
                                 FROM Elio_onboarding_blob_preview_files 
                                 WHERE 1 = 1 
                                 AND library_files_id = @library_files_id"
                                , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
        }

        public static bool DeleteUserCollaborationLibraryCategoryFilesAndBlobById(ElioUsers user, int categoryId, bool deleteWholeCategory, bool isOnboardingFile, DBSession session)
        {
            bool resetStorage = GlobalDBMethods.AddUserCategoryFilesStorage(user, categoryId, isOnboardingFile, session);

            if (!isOnboardingFile)
            {
                if (deleteWholeCategory)
                {
                    session.ExecuteQuery(@"DELETE
                                        from Elio_collaboration_users_library_files_categories
                                        where user_id = @user_id
                                        and id = @id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                            , DatabaseHelper.CreateIntParameter("@id", categoryId));

                    #region Old Way

                    //    session.ExecuteQuery(@"DELETE
                    //                            from Elio_collaboration_blob_files
                    //                            where library_files_id in
                    //                            (
                    //                            SELECT id     
                    //                              FROM Elio_collaboration_users_library_files
                    //                              where user_id = @user_id
                    //                              and category_id = @category_id
                    //                            )"
                    //                        , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                    //                        , DatabaseHelper.CreateIntParameter("@category_id", categoryId));

                    //    session.ExecuteQuery(@"DELETE  
                    //                            FROM Elio_collaboration_users_library_files 
                    //                            WHERE user_id = @user_id 
                    //                            AND category_id = @category_id"
                    //                        , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                    //                        , DatabaseHelper.CreateIntParameter("@category_id", categoryId));                

                    #endregion
                }
            }
            else
            {
                if (deleteWholeCategory)
                {
                    session.ExecuteQuery(@"DELETE
                                        from Elio_onboarding_users_library_files_categories
                                        where user_id = @user_id
                                        and id = @id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                            , DatabaseHelper.CreateIntParameter("@id", categoryId));
                }
            }

            return true;
        }

        public static bool DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(ElioUsers user, int fileId, bool isPreviewFile, bool isOnboardingFile, DBSession session)
        {
            bool resetStorage = false;

            if (!isOnboardingFile)
            {
                if (!isPreviewFile)
                {
                    resetStorage = GlobalDBMethods.AddUserFileStorage(user, fileId, !isOnboardingFile, session);
                    if (resetStorage)
                    {
                        GlobalDBMethods.AddUserPreviewFileStorage(user, fileId, !isOnboardingFile, session);

                        session.ExecuteQuery(@"DELETE  
                                            FROM Elio_collaboration_users_library_files 
                                            WHERE id = @id"
                                        , DatabaseHelper.CreateIntParameter("@id", fileId));

                        session.ExecuteQuery(@"DELETE  
                                           FROM Elio_collaboration_blob_files
                                           WHERE library_files_id = @library_files_id"
                                        , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));

                        session.ExecuteQuery(@"DELETE  
                                            FROM Elio_collaboration_blob_preview_files
                                            WHERE library_files_id = @library_files_id"
                                        , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
                    }
                }
                else
                {
                    resetStorage = GlobalDBMethods.AddUserPreviewFileStorage(user, fileId, !isOnboardingFile, session);
                    if (resetStorage)
                    {
                        session.ExecuteQuery(@"UPDATE Elio_collaboration_users_library_files
                                              SET preview_file_path = ''
                                            WHERE id = @id"
                                            , DatabaseHelper.CreateIntParameter("@id", fileId));

                        session.ExecuteQuery(@"DELETE  
                                                FROM Elio_collaboration_blob_preview_files
                                                WHERE library_files_id = @library_files_id"
                                            , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
                    }
                }
            }
            else
            {
                if (!isPreviewFile)
                {
                    resetStorage = GlobalDBMethods.AddUserFileStorage(user, fileId, !isOnboardingFile, session);
                    if (resetStorage)
                    {
                        GlobalDBMethods.AddUserPreviewFileStorage(user, fileId, !isOnboardingFile, session);

                        session.ExecuteQuery(@"DELETE  
                                            FROM Elio_onboarding_users_library_files 
                                            WHERE id = @id"
                                        , DatabaseHelper.CreateIntParameter("@id", fileId));

                        session.ExecuteQuery(@"DELETE  
                                           FROM Elio_onboarding_blob_files
                                           WHERE library_files_id = @library_files_id"
                                        , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));

                        session.ExecuteQuery(@"DELETE  
                                            FROM Elio_onboarding_blob_preview_files
                                            WHERE library_files_id = @library_files_id"
                                        , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
                    }
                }
                else
                {
                    resetStorage = GlobalDBMethods.AddUserPreviewFileStorage(user, fileId, !isOnboardingFile, session);
                    if (resetStorage)
                    {
                        session.ExecuteQuery(@"UPDATE Elio_onboarding_users_library_files
                                              SET preview_file_path = ''
                                            WHERE id = @id"
                                            , DatabaseHelper.CreateIntParameter("@id", fileId));

                        session.ExecuteQuery(@"DELETE  
                                                FROM Elio_onboarding_blob_preview_files
                                                WHERE library_files_id = @library_files_id"
                                            , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
                    }
                }
            }

            return resetStorage;
        }

        public static int GetCollaborationUserLibraryTotalPublicFilesByCategoryIdAndNewStatus(int userId, int categoryId, string isNew, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS count 
                                                     FROM Elio_collaboration_users_library_files 
                                                     WHERE user_id=@user_id 
                                                     AND category_id = @category_id 
                                                     AND is_public=1 
                                                     AND is_new IN (" + isNew + ")"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@category_id", categoryId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static List<ElioCollaborationUsersLibraryFiles> GetFilesSendByUserSendToUserAndUsers(int userId, int? partnerUserId, int categoryId, string isNew, bool isPartner, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);

            string strQuery = @"
                            SELECT *  
                            FROM Elio_collaboration_users_library_files
                            where 1 = 1
                                AND (";
            if (!isPartner)
            {
                strQuery += @"
                            (uploaded_by_user_id = @uploaded_by_user_id
                                and user_id = @user_id)	                            --mine
                            or
                            ";
                strQuery += @"(uploaded_by_user_id <> @uploaded_by_user_id " +		//--to me
                                "and user_id = @user_id " +
                                ")" +
                                // send by me to others files case
                                //"or " +
                                //"(" +
                                //"uploaded_by_user_id = @uploaded_by_user_id " +                //--from me to partner
                                //"and user_id <> @user_id " +
                                //")" +
                                ") " +
                                "and " +
                                "(category_id = @category_id " +
                                "AND is_public=1 " +
                                "AND is_new IN (" + isNew + "))";

                //                strQuery += @"(uploaded_by_user_id = @uploaded_by_user_id				    --by me
                //                                and user_id <> @user_id " +
                //                                    "and mailbox_id <> -1 " +
                //                                    ") " +
                //                                    "or " +
                //                                    "(uploaded_by_user_id <> @uploaded_by_user_id " +		//--to me
                //                                    "and user_id = @user_id " +
                //                                    "and mailbox_id <> -1 " +
                //                                    ")) " +
                //                                    "and " +
                //                                    "(category_id = @category_id " +
                //                                    "AND is_public=1 " +
                //                                    "AND is_new IN (" + isNew + "))";
            }
            else
            {
                if (partnerUserId != null)
                {
                    //                    strQuery += @"
                    //                            (uploaded_by_user_id = @uploaded_by_user_id
                    //                                and user_id = @user_id and mailbox_id = -1)	            --partner's
                    //                            or
                    //                            ";

                    strQuery += @"(uploaded_by_user_id = @uploaded_by_user_id				    --to me
                                and user_id = " + partnerUserId + " " +
                                ") " +
                                // send by partner files case
                                //"or " +
                                //"(uploaded_by_user_id = " + partnerUserId + " " +		        //--to me
                                //"and user_id = @user_id " +
                                //")" +
                                ") " +
                                "and " +
                                "(category_id = @category_id " +
                                "AND is_public=1 " +
                                "AND is_new IN (" + isNew + "))";
                }
            }

            return loader.Load(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@uploaded_by_user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@category_id", categoryId));
        }

        public static List<ElioCollaborationUsersLibraryFiles> GetLibraryFilesSendByUserSendToUserAndUsers(int userId, int? partnerUserId, int categoryId, string isNew, bool isPartner, bool isSendFiles, DateTime? from, DateTime? to, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);

            string strQuery = @"
                            SELECT *  
                            FROM Elio_collaboration_users_library_files
                            where 1 = 1
                                AND ";
            if (!isPartner)
            {
                if (isSendFiles)
                {
                    strQuery += @"(uploaded_by_user_id = @user_id
                                and user_id = @user_id) ";
                }
                else
                {
                    strQuery += @"(uploaded_by_user_id <> @user_id 		--to me
                                and user_id = @user_id) ";
                }

                strQuery += @" and " +
                                "(category_id = @category_id " +
                                "AND is_public=1 " +
                                "AND is_new IN (" + isNew + "))";
            }
            else
            {
                if (partnerUserId != null)
                {
                    if (isSendFiles)
                    {
                        strQuery += @"(uploaded_by_user_id = @user_id				    --to me
                                and user_id = " + partnerUserId + " ) ";
                    }
                    else
                    {
                        strQuery += @"(uploaded_by_user_id = " + partnerUserId + " " +
                                "and user_id = @user_id ) ";
                    }

                    strQuery += @" and " +
                                "(category_id = @category_id " +
                                "AND is_public=1 " +
                                "AND is_new IN (" + isNew + "))";
                }
            }

            if (from != null)
            {
                strQuery += " and date_created >= '" + from.Value.Year + "-" + from.Value.Month + "-" + from.Value.Day + "'";
            }

            if (to != null)
            {
                strQuery += " and date_created <= '" + to.Value.Year + "-" + to.Value.Month + "-" + to.Value.Day + "'";
            }

            return loader.Load(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@category_id", categoryId));
        }

        public static int GetCollaborationUserLibraryTotalPublicFilesByCategoryId(int userId, int categoryId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS new_count 
                                                     FROM Elio_collaboration_users_library_files 
                                                     WHERE user_id=@user_id 
                                                     AND category_id = @category_id 
                                                     AND is_public=1"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@category_id", categoryId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static bool UpdateUserCollaborationLibraryFileCategoryDescription(int userId, int libraryFileCategoryId, string description, bool isOnboardingLibrary, DBSession session)
        {
            if (!isOnboardingLibrary)
            {
                session.ExecuteQuery(@"UPDATE Elio_collaboration_users_library_files_categories 
                                    SET category_description = @category_description,
                                        last_update = getdate()
                                WHERE user_id = @user_id AND id = @id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateIntParameter("@id", libraryFileCategoryId)
                                    , DatabaseHelper.CreateStringParameter("@category_description", description));
            }
            else
            {
                session.ExecuteQuery(@"UPDATE Elio_onboarding_users_library_files_categories 
                                    SET category_description = @category_description,
                                        last_update = getdate()
                                WHERE user_id = @user_id AND id = @id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateIntParameter("@id", libraryFileCategoryId)
                                    , DatabaseHelper.CreateStringParameter("@category_description", description));
            }

            return true;
        }

        public static bool ExistFileByNameOrTitle(int userId, int categoryId, string fileTitleName, bool lookByName, DBSession session)
        {
            string strQuery = @"SELECT count(id) AS COUNT 
                                                     FROM Elio_collaboration_users_library_files 
                                                     WHERE user_id = @user_id 
                                                     AND category_id = @category_id ";

            strQuery += (lookByName) ? " AND file_name = @file_title_name " : " AND file_title = @file_title_name ";

            DataTable table = session.GetDataTable(strQuery
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                     , DatabaseHelper.CreateIntParameter("@category_id", categoryId)
                                                     , DatabaseHelper.CreateStringParameter("@file_title_name", fileTitleName));

            return Convert.ToInt32(table.Rows[0]["COUNT"]) == 0 ? false : true;
        }

        public static bool ExistLibraryCategoryDescription(int userId, string category, bool isOnboardingLbrary, DBSession session)
        {
            DataTable table = new DataTable();

            if (!isOnboardingLbrary)
            {
                table = session.GetDataTable(@"SELECT count(id) AS COUNT 
                                                        FROM Elio_collaboration_users_library_files_categories 
                                                        WHERE user_id = @user_id 
                                                        AND category_description = @category_description"
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                     , DatabaseHelper.CreateStringParameter("@category_description", category));
            }
            else
            {
                table = session.GetDataTable(@"SELECT count(id) AS COUNT 
                                                        FROM Elio_onboarding_users_library_files_categories 
                                                        WHERE user_id = @user_id 
                                                        AND category_description = @category_description"
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                     , DatabaseHelper.CreateStringParameter("@category_description", category));
            }

            return Convert.ToInt32(table.Rows[0]["COUNT"]) == 0 ? false : true;
        }

        public static void DeleteCollaborationLibraryFileById(int id, int userId, DBSession session)
        {
            session.ExecuteQuery(@"DELETE 
                                   FROM Elio_collaboration_users_library_files_categories
                                   WHERE id = @id AND user_id = @user_id"
                                   , DatabaseHelper.CreateIntParameter("@id", id)
                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void UpdatePublicStatusCollaborationLibraryFileById(int id, int userId, int isPublic, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_collaboration_users_library_files_categories 
                                    SET is_public = @is_public
                                   WHERE id = @id AND user_id = @user_id"
                                   , DatabaseHelper.CreateIntParameter("@id", id)
                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                   , DatabaseHelper.CreateIntParameter("@is_public", isPublic));
        }

        public static ElioCollaborationUsersLibraryFiles GetCollaborationUsersLibraryAttachmentFilesByMailboxIdUserIdCategoryId(int mailboxId, int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);

            return loader.LoadSingle(@"SELECT *
                                        FROM Elio_collaboration_users_library_files                                        
                                        where mailbox_id = @mailbox_id 
                                        and user_id = @user_id 
                                        and category_id = @category_id"
                                        , DatabaseHelper.CreateIntParameter("@mailbox_id", mailboxId)
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@category_id", categoryId));
        }

        public static ElioCollaborationUsersLibraryFiles GetCollaborationUsersLibraryAttachmentFilesByMailboxIdUserId(int mailboxId, int userId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);

            return loader.LoadSingle(@"SELECT TOP 1 *
                                        FROM Elio_collaboration_users_library_files
                                        where mailbox_id = @mailbox_id 
                                        and (uploaded_by_user_id = @user_id OR user_id = @user_id)"
                                        , DatabaseHelper.CreateIntParameter("@mailbox_id", mailboxId)
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioCollaborationUsersLibraryFilesSend GetCollaborationUsersFromLibrarySendAttachmentFilesByMailboxIdUserId(int mailboxId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFilesSend> loader = new DataLoader<ElioCollaborationUsersLibraryFilesSend>(session);

            return loader.LoadSingle(@"SELECT TOP 1 *
                                        FROM Elio_collaboration_users_library_files_send
                                        where mailbox_id = @mailbox_id"
                                        , DatabaseHelper.CreateIntParameter("@mailbox_id", mailboxId));
        }

        public static void SetCollaborationUsersLibraryFilesAsViewedByMailboxIdAndUserId(int mailboxId, int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS count
                                                     FROM Elio_collaboration_users_library_files 
                                                     where mailbox_id = @mailbox_id 
                                                     and user_id = @user_id
                                                     AND is_new = 1 "
                                                     , DatabaseHelper.CreateIntParameter("@mailbox_id", mailboxId)
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));

            if (Convert.ToInt32(table.Rows[0]["count"]) > 0)
            {
                session.ExecuteQuery(@"UPDATE Elio_collaboration_users_library_files 
                                        SET is_new = @is_new,
                                            last_update = @last_update 
                                       WHERE mailbox_id = @mailbox_id 
                                             AND user_id = @user_id"
                                            , DatabaseHelper.CreateIntParameter("@is_new", 0)
                                            , DatabaseHelper.CreateDateTimeParameter("@last_update", DateTime.Now)
                                            , DatabaseHelper.CreateIntParameter("@mailbox_id", mailboxId)
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
            }
        }

        public static void SetCollaborationLibraryFileAsViewedByFileId(int id, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_collaboration_users_library_files 
                                        SET is_new = @is_new,
                                        last_update = @last_update 
                                    WHERE id = @id"
                                        , DatabaseHelper.CreateIntParameter("@is_new", 0)
                                        , DatabaseHelper.CreateDateTimeParameter("@last_update", DateTime.Now)
                                        , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioCollaborationUsersLibraryFiles> GetCollaborationUsersLibraryFilesByFilePath(string filePath, DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);

            return loader.Load(@"SELECT *
                                        FROM Elio_collaboration_users_library_files                                        
                                        where file_path = @file_path"
                                        , DatabaseHelper.CreateStringParameter("@file_path", filePath));
        }

        public static int GetCountCollaborationUsersLibraryFilesByFilePathAndNewStatus(string filePath, int isNew, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(*) as count 
                                        FROM Elio_collaboration_users_library_files 
                                        where file_path = @file_path 
                                        and is_new = @is_new"
                                        , DatabaseHelper.CreateStringParameter("@file_path", filePath)
                                        , DatabaseHelper.CreateIntParameter("@is_new", isNew));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static List<ElioCollaborationUsersGroups> GetCollaborationUserGroups(int userId, string companyType, int isActive, int isPublic, DBSession session)
        {
            DataLoader<ElioCollaborationUsersGroups> loader = new DataLoader<ElioCollaborationUsersGroups>(session);

            string strQuery = @"SELECT distinct Elio_collaboration_users_groups.id
                                    , user_id, collaboration_group_name
                                    , Elio_collaboration_users_groups.date_created
                                    , Elio_collaboration_users_groups.last_update
                                    , Elio_collaboration_users_groups.is_active
                                    , Elio_collaboration_users_groups.is_public
                                FROM Elio_collaboration_users_groups 
                                INNER JOIN Elio_collaboration_users_group_members
	                                ON Elio_collaboration_users_group_members.collaboration_group_id = Elio_collaboration_users_groups.id
                                WHERE 1 = 1 
                                AND Elio_collaboration_users_groups.is_active = @is_active 
                                AND Elio_collaboration_users_groups.is_public= @is_public ";

            if (companyType == Types.Vendors.ToString())
            {
                strQuery += " AND creator_user_id = @user_id ";
            }
            else if (companyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                strQuery += " AND group_retailor_id = @user_id ";
            }

            strQuery += @" GROUP BY Elio_collaboration_users_groups.id
                                , user_id
                                , collaboration_group_name, Elio_collaboration_users_groups.date_created
                                , Elio_collaboration_users_groups.last_update
                                , Elio_collaboration_users_groups.is_active
                                , Elio_collaboration_users_groups.is_public";

            //            if (companyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            //            {
            //                DataTable table = session.GetDataTable(@"select id, master_user_id
            //	                        from Elio_collaboration_vendors_resellers	 
            //	                        where partner_user_id=@user_id"
            //                    , DatabaseHelper.CreateIntParameter("@user_id", userId));

            //                if (Convert.ToInt32(table.Rows.Count) > 0)
            //                {
            //                    for (int i = 0; i < table.Rows.Count; i++)
            //                    {
            //                        userIds += table.Rows[i]["master_user_id"] + ",";
            //                    }

            //                    if (userIds.EndsWith(","))
            //                    {
            //                        userIds = userIds.Substring(0, userIds.Length - 1);
            //                    }

            //                    strQuery += @"inner join Elio_collaboration_users_group_retailors 
            //		                            on Elio_collaboration_users_group_retailors.collaboration_group_id = Elio_collaboration_users_groups.id 
            //		                            and Elio_collaboration_users_group_retailors.collaboration_vendors_resellers_id=" + table.Rows[0]["id"].ToString();
            //                }
            //            }

            //            strQuery += @" WHERE Elio_collaboration_users_groups.user_id IN (" + userIds + ") " +
            //                                 "AND Elio_collaboration_users_groups.is_active = @is_active " +
            //                                 "AND Elio_collaboration_users_groups.is_public= @is_public";

            return loader.Load(strQuery
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                , DatabaseHelper.CreateIntParameter("@is_public", isPublic));
        }

        public static List<ElioUsersTrainingGroups> GetTrainingUserGroups(int userId, DBSession session)
        {
            DataLoader<ElioUsersTrainingGroups> loader = new DataLoader<ElioUsersTrainingGroups>(session);

            string strQuery = @"SELECT *
                                FROM Elio_users_training_groups                                
                                WHERE 1 = 1 
                                AND user_id = @user_id
                                AND is_active = 1 
                                AND is_public = 1 
                                ORDER BY training_group_name";

            return loader.Load(strQuery
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioCollaborationUsersGroupRetailors> GetCollaborationUserGroupRetailors(int gropuId, int userId, int isActive, int isPublic, DBSession session)
        {
            DataLoader<ElioCollaborationUsersGroupRetailors> loader = new DataLoader<ElioCollaborationUsersGroupRetailors>(session);
            return loader.Load(@"SELECT *  
                                 FROM Elio_collaboration_users_group_retailors  
                                 WHERE collaboration_group_id = @collaboration_group_id 
                                 AND user_id = @user_id 
                                 AND is_active = @is_active
                                 AND is_public= @is_public"
                                , DatabaseHelper.CreateIntParameter("@collaboration_group_id", gropuId)
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                , DatabaseHelper.CreateIntParameter("@is_public", isPublic));
        }

        public static List<ElioCollaborationUsersGroupRetailors> GetCollaborationUserGroupRetailorsByGroupId(int gropuId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersGroupRetailors> loader = new DataLoader<ElioCollaborationUsersGroupRetailors>(session);
            return loader.Load(@"SELECT *  
                                 FROM Elio_collaboration_users_group_retailors  
                                 WHERE collaboration_group_id = @collaboration_group_id
                                 AND is_active = 1
                                 AND is_public= 1"
                                , DatabaseHelper.CreateIntParameter("@collaboration_group_id", gropuId));
        }

        public static ElioCollaborationUsersGroups GetCollaborationUserGroupById(int id, DBSession session)
        {
            DataLoader<ElioCollaborationUsersGroups> loader = new DataLoader<ElioCollaborationUsersGroups>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_collaboration_users_groups WHERE id = @id"
                                     , DatabaseHelper.CreateIntParameter("@id", id));

        }

        public static bool ExistCollaborationUserGroupRetailorInGroup(int gropuId, int userId, int isActive, int isPublic, DBSession session)
        {           
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS count  
                                 FROM Elio_collaboration_users_group_members  
                                 WHERE collaboration_group_id = @collaboration_group_id 
                                 AND group_retailor_id = @group_retailor_id                                  
                                 AND is_active = @is_active
                                 AND is_public= @is_public"
                                , DatabaseHelper.CreateIntParameter("@collaboration_group_id", gropuId)
                                , DatabaseHelper.CreateIntParameter("@group_retailor_id", userId)
                                , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                , DatabaseHelper.CreateIntParameter("@is_public", isPublic));

            return (Convert.ToInt32(table.Rows[0]["count"]) == 0) ? false : true;
        }

        public static bool ExistTrainingUserGroupRetailorInGroup(int gropuId, int userId, int isActive, int isPublic, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS count  
                                 FROM Elio_users_training_group_members  
                                 WHERE training_group_id = @training_group_id 
                                 AND reseller_id = @reseller_id                                  
                                 AND is_active = @is_active
                                 AND is_public= @is_public"
                                , DatabaseHelper.CreateIntParameter("@training_group_id", gropuId)
                                , DatabaseHelper.CreateIntParameter("@reseller_id", userId)
                                , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                , DatabaseHelper.CreateIntParameter("@is_public", isPublic));

            return (Convert.ToInt32(table.Rows[0]["count"]) == 0) ? false : true;
        }

        public static bool ExistCollaborationGroupDescription(int userId, string groupDescription, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) as count
                                FROM Elio_collaboration_users_groups 
                                WHERE user_id = @user_id
                                AND collaboration_group_name = @collaboration_group_name";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@collaboration_group_name", groupDescription));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static bool ExistTrainingGroupDescription(int userId, string groupDescription, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) as count
                                FROM Elio_users_training_groups 
                                WHERE user_id = @user_id
                                AND training_group_name = @training_group_name";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@training_group_name", groupDescription));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static bool ExistCollaborationGroupDescriptionToOtherGroupId(int userId, int groupId, string groupDescription, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) as count
                                FROM Elio_collaboration_users_groups 
                                WHERE user_id = @user_id
                                AND collaboration_group_name = @collaboration_group_name
                                AND id != @id
                                AND is_public = 1";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@collaboration_group_name", groupDescription)
                                                   , DatabaseHelper.CreateIntParameter("@id", groupId));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static bool ExistTrainingGroupDescriptionToOtherGroupId(int userId, int groupId, string groupDescription, DBSession session)
        {
            string strQuery = @"SELECT COUNT(id) as count
                                FROM Elio_users_training_groups 
                                WHERE user_id = @user_id
                                AND training_group_name = @training_group_name
                                AND id != @id
                                AND is_public = 1";

            DataTable table = session.GetDataTable(strQuery
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@training_group_name", groupDescription)
                                                   , DatabaseHelper.CreateIntParameter("@id", groupId));

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
            else
                return false;
        }

        public static void UpdateCollaborationUserGroupByGroupId(int gropuId, int userId, string groupName, int isActive, int isPublic, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_collaboration_users_groups 
                                        SET collaboration_group_name = @collaboration_group_name,
                                            last_update = GETDATE(), 
                                            is_active = @is_active,
                                            is_public= @is_public
                                 WHERE id = @id 
                                 AND user_id = @user_id"
                                , DatabaseHelper.CreateStringParameter("@collaboration_group_name", groupName)
                                , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                                , DatabaseHelper.CreateIntParameter("@id", gropuId)
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void UpdateTrainingUserGroupByGroupId(int gropuId, int userId, string groupName, int isActive, int isPublic, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_users_training_groups 
                                        SET training_group_name = @training_group_name,
                                            last_update = GETDATE(), 
                                            is_active = @is_active,
                                            is_public= @is_public
                                 WHERE id = @id 
                                 AND user_id = @user_id"
                                , DatabaseHelper.CreateStringParameter("@training_group_name", groupName)
                                , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                                , DatabaseHelper.CreateIntParameter("@id", gropuId)
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void DeleteCollaborationUserGroupRetailorByGroupId(int gropuId, int groupRetailorId, DBSession session)
        {
            session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_group_members                                         
                                   WHERE collaboration_group_id = @collaboration_group_id 
                                   AND group_retailor_id = @group_retailor_id"
                                , DatabaseHelper.CreateIntParameter("@collaboration_group_id", gropuId)
                                , DatabaseHelper.CreateIntParameter("@group_retailor_id", groupRetailorId));
        }

        public static void DeleteTrainingUserGroupRetailorByGroupId(int gropuId, int groupResellerId, DBSession session)
        {
            session.ExecuteQuery(@"DELETE FROM Elio_users_training_group_members                                         
                                   WHERE training_group_id = @training_group_id 
                                   AND reseller_id = @reseller_id"
                                , DatabaseHelper.CreateIntParameter("@training_group_id", gropuId)
                                , DatabaseHelper.CreateIntParameter("@reseller_id", groupResellerId));
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupId(int collaborationGroupId, int userId, string invitationStatus, string companyType, string orderByClasuse, DBSession session)
        {
            return GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupId(1, collaborationGroupId, userId, invitationStatus, companyType, orderByClasuse, session);
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupId(int mode, int collaborationGroupId, int userId, string invitationStatus, string companyType, string orderByClasuse, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string strQuery = @"SELECT Elio_collaboration_vendors_resellers.id, Elio_collaboration_vendors_resellers.master_user_id, 
                                       invitation_status, Elio_collaboration_vendors_resellers.partner_user_id, company_name, company_type, email, country, 
                                       Elio_collaboration_users_group_retailors.* 
                                 FROM Elio_collaboration_vendors_resellers WITH (NOLOCK) 
                                 INNER JOIN Elio_collaboration_users_group_retailors on Elio_collaboration_users_group_retailors.collaboration_vendors_resellers_id = Elio_collaboration_vendors_resellers.id 
                                 INNER JOIN Elio_users ";

            if (mode == 1)
            {
                strQuery += (companyType == Types.Vendors.ToString()) ? " ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id WHERE master_user_id =" + userId + " "
                                                                      : " ON Elio_collaboration_vendors_resellers.master_user_id = Elio_users.id WHERE partner_user_id =" + userId + " ";
            }
            else if (mode == 2)
            {
                strQuery += " ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id WHERE 1 = 1 ";    //master_user_id =" + userId + " ";                                                                      
            }

            strQuery += " AND invitation_status = @invitation_status";
            strQuery += " AND collaboration_group_id = @collaboration_group_id";
            strQuery += " ORDER BY " + orderByClasuse;

            return loader.Load(strQuery
                               , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus)
                               , DatabaseHelper.CreateIntParameter("@collaboration_group_id", collaborationGroupId));
        }

        public static List<ElioCollaborationUsersGroupMembers> GetUserGroupMembers(int creatorUserId, int groupId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersGroupMembers> loader = new DataLoader<ElioCollaborationUsersGroupMembers>(session);

            return loader.Load(@"Select * From Elio_collaboration_users_group_members 
                                 Where creator_user_id = @creator_user_id
                                       And collaboration_group_id = @collaboration_group_id"
                                 , DatabaseHelper.CreateIntParameter("@creator_user_id", creatorUserId)
                                 , DatabaseHelper.CreateIntParameter("@collaboration_group_id", groupId));
        }

        public static List<ElioCollaborationUsersGroupMembersIJUsers> GetUserGroupMembersIJUsersByGroupId(int groupId, int isActive, int isPublic, GlobalMethods.SearchCriteria criteria, DBSession session)
        {
            if (session.Connection.State == ConnectionState.Closed)
                session.Connection.Open();

            DataLoader<ElioCollaborationUsersGroupMembersIJUsers> loader = new DataLoader<ElioCollaborationUsersGroupMembersIJUsers>(session);
            List<ElioCollaborationUsersGroupMembersIJUsers> members = new List<ElioCollaborationUsersGroupMembersIJUsers>();

            string strQuery = @"Select Elio_collaboration_users_group_members.id, creator_user_id, group_retailor_id, collaboration_group_id
                                        , Elio_collaboration_users_group_members.date_created
                                        , Elio_collaboration_users_group_members.last_update
                                        , Elio_collaboration_users_group_members.is_active
                                        , company_name, email, company_logo, company_type, country, region
                                 From Elio_collaboration_users_group_members 
                                 Inner join Elio_users
	                                 ON Elio_users.id = Elio_collaboration_users_group_members.group_retailor_id
                                 INNER JOIN Elio_countries 
	                                 ON Elio_countries.country_name = Elio_users.country
                                 WHERE collaboration_group_id = @collaboration_group_id 
                                       And Elio_collaboration_users_group_members.is_active = @is_active 
                                       And Elio_collaboration_users_group_members.is_public = @is_public ";

            if (criteria != null)
            {
                if (criteria.CompanyName != "")
                {
                    strQuery += " AND company_name LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.CompanyName) + "%' ";
                }

                if (criteria.Country != "")
                {
                    strQuery += " AND country LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.Country) + "%' ";
                }

                if (criteria.Region != "")
                {
                    strQuery += " AND region LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.Region) + "%' ";
                }
            }

            members = loader.Load(strQuery
                             , DatabaseHelper.CreateIntParameter("@collaboration_group_id", groupId)
                             , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                             , DatabaseHelper.CreateIntParameter("@is_public", isPublic));

            if (members.Count > 0)
            {
                List<ElioCollaborationUsersGroupMembersIJUsers> membersFinallyUsers = new List<ElioCollaborationUsersGroupMembersIJUsers>();

                if (criteria != null && criteria.PartnerPrograms.Count > 0)
                {
                    string programs = "";
                    int count = 0;
                    foreach (string program in criteria.PartnerPrograms)
                    {
                        if (count < criteria.PartnerPrograms.Count - 1)
                            programs += "'" + program + "',";
                        else
                            programs += "'" + program + "'";

                        count++;
                    }

                    foreach (ElioCollaborationUsersGroupMembersIJUsers retailor in members)
                    {
                        DataTable table = session.GetDataTable(@"SELECT count(elio_partners.id) AS count 
                                                             FROM elio_partners 
                                                             INNER JOIN Elio_users_partners 
                                                                ON Elio_users_partners.partner_id = elio_partners.id
                                                             WHERE user_id = @user_id
                                                             AND partner_description IN (" + programs + ")"
                                                                 , DatabaseHelper.CreateIntParameter("@user_id", retailor.GroupRetailorId));

                        if (Convert.ToInt32(table.Rows[0]["count"]) > 0)
                        {
                            membersFinallyUsers.Add(retailor);
                        }
                    }

                    members.Clear();
                    members = membersFinallyUsers.ToList();

                    return members;
                }
                else
                    return members;
            }
            else
                return members;
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupIdAndCriteria(int mode, int collaborationGroupId, int userId, string invitationStatus, string companyType, GlobalMethods.SearchCriteria criteria, string orderByClasuse, DBSession session)
        {
            List<ElioCollaborationVendorsResellersIJUsers> vendorsResellers = new List<ElioCollaborationVendorsResellersIJUsers>();

            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string strQuery = @"SELECT Elio_collaboration_vendors_resellers.id, Elio_collaboration_vendors_resellers.master_user_id, 
                                       invitation_status, Elio_collaboration_vendors_resellers.partner_user_id, company_name, email, country, 
                                       Elio_collaboration_users_group_retailors.* 
                                 FROM Elio_collaboration_vendors_resellers WITH (NOLOCK) 
                                 INNER JOIN Elio_collaboration_users_group_retailors on Elio_collaboration_users_group_retailors.collaboration_vendors_resellers_id = Elio_collaboration_vendors_resellers.id 
                                 INNER JOIN Elio_users ";

            if (mode == 1)
            {
                strQuery += (companyType == Types.Vendors.ToString()) ? " ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id WHERE master_user_id =" + userId + " "
                                                                      : " ON Elio_collaboration_vendors_resellers.master_user_id = Elio_users.id WHERE partner_user_id =" + userId + " ";
            }
            else if (mode == 2)
            {
                strQuery += " ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id ";    //master_user_id =" + userId + " ";                                                                      

                strQuery += " INNER JOIN Elio_countries ON Elio_countries.country_name = Elio_users.country ";

                strQuery += " WHERE 1 = 1 ";
            }

            if (criteria != null)
            {
                if (criteria.CompanyName != "")
                {
                    strQuery += " AND company_name LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.CompanyName) + "%' ";
                }

                if (criteria.Country != "")
                {
                    strQuery += " AND country LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.Country) + "%' ";
                }

                if (criteria.Region != "")
                {
                    strQuery += " AND region LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.Region) + "%' ";
                }
            }

            strQuery += " AND invitation_status = @invitation_status";
            strQuery += " AND collaboration_group_id = @collaboration_group_id";
            strQuery += " ORDER BY " + orderByClasuse;

            vendorsResellers = loader.Load(strQuery
                               , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus)
                               , DatabaseHelper.CreateIntParameter("@collaboration_group_id", collaborationGroupId));

            if (vendorsResellers.Count > 0)
            {
                List<ElioCollaborationVendorsResellersIJUsers> vendorsResellersFinallyUsers = new List<ElioCollaborationVendorsResellersIJUsers>();

                if (criteria != null && criteria.PartnerPrograms.Count > 0)
                {
                    string programs = "";
                    int count = 0;
                    foreach (string program in criteria.PartnerPrograms)
                    {
                        if (count < criteria.PartnerPrograms.Count - 1)
                            programs += "'" + program + "',";
                        else
                            programs += "'" + program + "'";

                        count++;
                    }

                    foreach (ElioCollaborationVendorsResellersIJUsers vendorReseller in vendorsResellers)
                    {
                        int vendResellerUser = (companyType == Types.Vendors.ToString()) ? vendorReseller.MasterUserId : vendorReseller.PartnerUserId;

                        DataTable table = session.GetDataTable(@"SELECT count(elio_partners.id) AS count 
                                                             FROM elio_partners 
                                                             INNER JOIN Elio_users_partners 
                                                                ON Elio_users_partners.partner_id = elio_partners.id
                                                             WHERE user_id = @user_id
                                                             AND partner_description IN (" + programs + ")"
                                                                 , DatabaseHelper.CreateIntParameter("@user_id", vendResellerUser));

                        if (Convert.ToInt32(table.Rows[0]["count"]) > 0)
                        {
                            vendorsResellersFinallyUsers.Add(vendorReseller);
                        }
                    }

                    vendorsResellers.Clear();
                    vendorsResellers = vendorsResellersFinallyUsers.ToList();

                    return vendorsResellers;
                }
                else
                    return vendorsResellers;
            }
            else
                return vendorsResellers;
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupIdAndCompanyName(int mode, int collaborationGroupId, int userId, GlobalMethods.SearchCriteria criteria, string invitationStatus, string companyType, string orderByClasuse, DBSession session)
        {
            List<ElioCollaborationVendorsResellersIJUsers> vendorsResellers = new List<ElioCollaborationVendorsResellersIJUsers>();

            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string strQuery = @"SELECT Elio_collaboration_vendors_resellers.id, Elio_collaboration_vendors_resellers.master_user_id, 
                                       invitation_status, Elio_collaboration_vendors_resellers.partner_user_id, company_name, email, country, 
                                       Elio_collaboration_users_group_retailors.* 
                                 FROM Elio_collaboration_vendors_resellers WITH (NOLOCK) 
                                 INNER JOIN Elio_collaboration_users_group_retailors on Elio_collaboration_users_group_retailors.collaboration_vendors_resellers_id = Elio_collaboration_vendors_resellers.id 
                                 INNER JOIN Elio_users ";

            if (mode == 1)
            {
                strQuery += (companyType == Types.Vendors.ToString()) ? " ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id WHERE master_user_id =" + userId + " "
                                                                      : " ON Elio_collaboration_vendors_resellers.master_user_id = Elio_users.id WHERE partner_user_id =" + userId + " ";
            }
            else if (mode == 2)
            {
                strQuery += " ON Elio_collaboration_vendors_resellers.partner_user_id = Elio_users.id ";    //master_user_id =" + userId + " ";                                                                      

                if (criteria != null && (criteria.Region != "" || criteria.Country != ""))
                    strQuery += " INNER JOIN Elio_countries ON Elio_countries.country_name = Elio_users.country ";

                strQuery += " WHERE 1 = 1 ";
            }

            strQuery += " AND invitation_status = @invitation_status";
            strQuery += " AND collaboration_group_id = @collaboration_group_id";
            //strQuery += " AND Elio_users.company_name LIKE '" + criteria.CompanyName + "%'";

            if (criteria != null && criteria.CompanyName != "")
            {
                strQuery += " AND Elio_users.company_name LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.CompanyName) + "%' ";
            }

            if (criteria != null && criteria.Country != "")
            {
                strQuery += " AND Elio_users.country LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.Country) + "%' ";
            }

            if (criteria != null && criteria.Region != "")
            {
                strQuery += " AND Elio_countries.region LIKE '" + Validations.FixInvalidSpecialCharForSqlSearch(criteria.Region) + "%' ";
            }

            strQuery += " ORDER BY " + orderByClasuse;

            vendorsResellers = loader.Load(strQuery
                               , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus)
                               , DatabaseHelper.CreateIntParameter("@collaboration_group_id", collaborationGroupId));

            //List<ElioCollaborationVendorsResellersIJUsers> vendorsResellersFinallyUsers = new List<ElioCollaborationVendorsResellersIJUsers>();
            bool hasCriteria = false;

            if (criteria != null && criteria.PartnerPrograms.Count > 0)
            {
                string programs = "";
                int count = 0;
                foreach (string program in criteria.PartnerPrograms)
                {
                    if (count < criteria.PartnerPrograms.Count - 1)
                        programs += "'" + program + "',";
                    else
                        programs += "'" + program + "'";

                    count++;
                }

                foreach (ElioCollaborationVendorsResellersIJUsers vendorReseller in vendorsResellers)
                {
                    int vendResellerUser = (companyType == Types.Vendors.ToString()) ? vendorReseller.MasterUserId : vendorReseller.PartnerUserId;

                    DataTable table = session.GetDataTable(@"SELECT count(elio_partners.id) AS count 
                                                            FROM elio_partners 
                                                            INNER JOIN Elio_users_partners 
                                                            ON Elio_users_partners.partner_id = elio_partners.id
                                                            WHERE user_id = @user_id
                                                            AND partner_description IN (" + programs + ")"
                                                                , DatabaseHelper.CreateIntParameter("@user_id", vendResellerUser));

                    if (Convert.ToInt32(table.Rows[0]["count"]) > 0)
                    {
                        //vendorsResellersFinallyUsers.Add(vendorReseller);
                        hasCriteria = true;
                        break;
                    }
                }

                //vendorsResellers.Clear();
                //vendorsResellers = vendorsResellersFinallyUsers.ToList();
            }

            if (criteria != null && criteria.PartnerPrograms.Count > 0)
            {
                if (!hasCriteria)
                    vendorsResellers = null;
            }

            return vendorsResellers;
        }

        public static ElioCollaborationMailbox GetCollaborationMailboxIJUserGroupRetailorMailboxByMailboxIdAndPartnerUserId(int partnerUserId, int mailboxId, DBSession session)
        {
            DataLoader<ElioCollaborationMailbox> loader = new DataLoader<ElioCollaborationMailbox>(session);

            return loader.LoadSingle(@"SELECT *
                                       FROM Elio_collaboration_mailbox
                                       INNER JOIN Elio_collaboration_users_group_retailors_mailbox
	                                        ON Elio_collaboration_mailbox.id = Elio_collaboration_users_group_retailors_mailbox.collaboration_users_mailbox 
                                       WHERE partner_user_id = @partner_user_id 
                                       AND mailbox_id = @mailbox_id"
                                       , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId)
                                       , DatabaseHelper.CreateIntParameter("@mailbox_id", mailboxId));
        }

        public static List<ElioCollaborationMailbox> GetCollaborationMailboxByGroupId(int groupId, int isPublic, int isDeleted, DBSession session)
        {
            DataLoader<ElioCollaborationMailbox> loader = new DataLoader<ElioCollaborationMailbox>(session);

            return loader.Load(@"SELECT *
                                 FROM Elio_collaboration_mailbox
                                 WHERE id in
                                 (
	                                 SELECT distinct mailbox_id
	                                 FROM Elio_collaboration_users_group_mailbox
	                                 WHERE is_public = @is_public 
                                     AND is_deleted = @is_deleted 
                                     AND group_id = @group_id
                                 )"
                                       , DatabaseHelper.CreateIntParameter("@group_id", groupId)
                                       , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                                       , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted));
        }

        public static bool IsMasterVendorResellerUser(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS count FROM Elio_collaboration_vendors_resellers 
                                                     WHERE master_user_id = @master_user_id"
                                                     , DatabaseHelper.CreateIntParameter("@master_user_id", userId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool IsPartnerVendorResellerUser(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS count FROM Elio_collaboration_vendors_resellers 
                                                     WHERE partner_user_id = @partner_user_id"
                                                     , DatabaseHelper.CreateIntParameter("@partner_user_id", userId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static List<ElioCollaborationVendorsResellersIJUsers> GetVendorCollaborationPartners(int masterUserId, string companyOrAddress, string invitationStatus, string country, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellersIJUsers> loader = new DataLoader<ElioCollaborationVendorsResellersIJUsers>(session);

            string query = @"SELECT cvr.id, cvr.master_user_id, 
                                    invitation_status, cvr.partner_user_id, u.company_name, u.email, u.country,
                                    cvr_inv.is_new
                                  FROM Elio_collaboration_vendors_resellers cvr
                                inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
	                                on cvr_inv.vendor_reseller_id = cvr.id
                                inner join elio_users u 
	                                on u.id = cvr.partner_user_id
                                  where cvr.master_user_id = @master_user_id and cvr_inv.user_id = @master_user_id";


            if (companyOrAddress != "")
                query += " AND (u.company_name like '%" + companyOrAddress + "%' OR u.address like '%" + companyOrAddress + "%') ";

            if (country != "")
                query += " AND u.country = '" + country + "' ";

            if (invitationStatus != "")
                query += " AND invitation_status = '" + invitationStatus + "' ";

            query += " ORDER BY u.company_name ASC";

            return loader.Load(query
                                 , DatabaseHelper.CreateIntParameter("@master_user_id", masterUserId));
        }

        public static DataTable GetVendorCollaborationPartnersLocatorTable(int vendorId, string companyOrAddress, string country, DBSession session)
        {
            string query = @"SELECT u.id
                                ,case when invitation_status = 'Confirmed' then 'Registered' else invitation_status end as Status
                                ,u.company_name as Name
                                ,u.email as Email
                                ,u.country as Country
                                ,case when isnull(u.website,'') = '' then '-' else u.website end as Website
                                ,case when isnull(u.phone,'') = '' then '-' else u.phone end as Phone
                                ,case when isnull(u.address,'') = '' then '-' else u.address end as Address
                                ,u.company_logo as Logo                       
                                FROM elio_users u
                            inner join Elio_collaboration_vendors_resellers cvr
	                            on u.id = cvr.partner_user_id
                                where cvr.master_user_id = @master_user_id
                            and invitation_status = 'Confirmed'";


            if (companyOrAddress != "")
                query += " AND (u.company_name like '" + companyOrAddress + "%' OR u.address like '" + companyOrAddress + "%') ";

            if (country != "")
                query += " AND u.country = '" + country + "' ";

            query += " ORDER BY u.company_name ASC";

            return session.GetDataTable(query
                                 , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));
        }

        public static DataTable GetVendorCollaborationPartnersLocatorCountriesTable(int vendorId, DBSession session)
        {
            return session.GetDataTable(@"SELECT distinct(c.country_name),c.id
                                            FROM elio_users u
                                            inner join Elio_collaboration_vendors_resellers cvr on u.id = cvr.partner_user_id
                                            inner join Elio_countries c on c.country_name = u.country
                                            where cvr.master_user_id = @master_user_id
                                            and invitation_status = 'Confirmed'
                                            order by country_name"
                                 , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));
        }
    }
}