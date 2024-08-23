using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.CurrencyConverter;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Analytics
{
    public class StatsDB
    {
        public static double GetVendorTotalRevenuesAmount(int collaborationId, int vendorId, int resellerId, out string vendorCurrencySymbol, out string tierStatus, DBSession session)
        {
            vendorCurrencySymbol = "";
            tierStatus = "";
            double deals_amount = 0;
            double leads_amount = 0;
            double totalRevenuesAmount = 0;
            bool hasTotalRevenues = true;

            int tierManagementPeriodId = Sql.GetTierManagementUserSettingsPeriodId(vendorId, session);
            if (tierManagementPeriodId > 0)
            {
                hasTotalRevenues = tierManagementPeriodId == 1 ? false : true;
            }

            int? amountYear = null;
            if (!hasTotalRevenues)
            {
                amountYear = DateTime.Now.Year;
            }

            List<ElioRegistrationDeals> deals = Sql.GetDealsBy(collaborationId, vendorId, resellerId, DealResultStatus.Won.ToString(), (int)DealStatus.Closed, amountYear, session);

            if (deals.Count > 0)
            {
                deals_amount = GlobalDBMethods.GetDealsSumAmountToVendorCurrency(vendorId, deals, out vendorCurrencySymbol, session);
            }
            else
            {
                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(vendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;
                        }
                    }
                }
            }

            List<ElioLeadDistributions> leads = Sql.GetLeadsBy(collaborationId, vendorId, resellerId, DealResultStatus.Won.ToString(), (int)DealStatus.Closed, amountYear, session);

            if (leads.Count > 0)
            {
                leads_amount = GlobalDBMethods.GetLeadsSumAmountToVendorCurrency(vendorId, leads, out vendorCurrencySymbol, session);
            }
            else
            {
                if (vendorCurrencySymbol == "")
                {
                    ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);
                    if (vendorCurrency != null)
                    {
                        vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                    }
                    else
                    {
                        ElioUsers vendor = Sql.GetUserById(vendorId, session);
                        if (vendor != null)
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                            if (countryCurrency != null)
                            {
                                vendorCurrencySymbol = countryCurrency.CurrencySymbol;
                            }
                        }
                    }
                }
            }

            if (deals_amount > 0)
                totalRevenuesAmount = deals_amount;

            if (leads_amount > 0)
                totalRevenuesAmount += leads_amount;

            if (totalRevenuesAmount > 0.0)
            {
                List<ElioTierManagementUsersSettings> userTiers = Sql.GetTierManagementUserSettingsByVolumeAsc(vendorId, session);
                if (userTiers.Count > 0)
                {
                    foreach (ElioTierManagementUsersSettings tier in userTiers)
                    {
                        if (totalRevenuesAmount > (double)tier.FromVolume && totalRevenuesAmount <= (double)tier.ToVolume)
                        {
                            tierStatus = tier.Description;
                            break;
                        }
                    }

                    tierStatus = (tierStatus == "") ? userTiers[userTiers.Count - 1].Description : tierStatus;
                }
            }

            return Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", totalRevenuesAmount));
        }

        public static DataTable GetVendorForecastingDealsAmountToVendorCurrencyByMonth(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int status, string dealRresult, int isActive, int? year, out string vendorCurrencySymbol, DBSession session)
        {
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            vendorCurrencySymbol = "";

            DataTable table = new DataTable();

            DataColumn colCount = new DataColumn("count");
            colCount.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(colCount);

            DataColumn colDecimal = new DataColumn("amount");
            colDecimal.DataType = System.Type.GetType("System.String");
            table.Columns.Add(colDecimal);

            DataColumn colMonth = new DataColumn("month");
            colMonth.DataType = System.Type.GetType("System.String");
            table.Columns.Add(colMonth);

            table.Rows.Add(0, "0", "Jan");
            table.Rows.Add(0, "0", "Feb");
            table.Rows.Add(0, "0", "Mar");
            table.Rows.Add(0, "0", "Apr");
            table.Rows.Add(0, "0", "May");
            table.Rows.Add(0, "0", "Jun");
            table.Rows.Add(0, "0", "Jul");
            table.Rows.Add(0, "0", "Aug");
            table.Rows.Add(0, "0", "Sep");
            table.Rows.Add(0, "0", "Oct");
            table.Rows.Add(0, "0", "Nov");
            table.Rows.Add(0, "0", "Dec");

            //int currMonth = DateTime.Now.Month;

            //for (int i = 0; i < 12; i++)
            //{
            //    string month = "";

            //    if (i == 0)
            //        month = "Jan";
            //    else if (i == 1)
            //        month = "Feb";
            //    else if (i == 2)
            //        month = "Mar";
            //    else if (i == 3)
            //        month = "Apr";
            //    else if (i == 4)
            //        month = "May";
            //    else if (i == 5)
            //        month = "Jun";
            //    else if (i == 6)
            //        month = "Jul";
            //    else if (i == 7)
            //        month = "Aug";
            //    else if (i == 8)
            //        month = "Sep";
            //    else if (i == 9)
            //        month = "Oct";
            //    else if (i == 10)
            //        month = "Nov";
            //    else
            //        month = "Dec";

            //    table.Rows.Add(0, 0, month);
            //}

            DataLoader<ElioRegistrationDeals> loaderDeals = new DataLoader<ElioRegistrationDeals>(session);

            string query = @"select *
                            from  Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                        and rd.vendor_id = @vendor_id
                        and rd.is_active = @is_active
                        and rd.status = @status
                        and rd.deal_result = @deal_result
                        and rd.expected_closed_date >= getdate() ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += " order by month(rd.expected_closed_date)";

            List<ElioRegistrationDeals> deals = loaderDeals.Load(query
                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealRresult));

            if (deals.Count > 0)
            {
                foreach (ElioRegistrationDeals deal in deals)
                {
                    double amount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));

                    if (string.IsNullOrEmpty(deal.CurId))
                    {
                        string partnerCountry = "";

                        ElioUsers dealPartner = Sql.GetUserById(deal.ResellerId, session);
                        if (dealPartner != null)
                            partnerCountry = dealPartner.Country;

                        if (partnerCountry != "")
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                            if (countryCurrency != null)
                            {
                                if (deal != null)
                                {
                                    deal.CurId = countryCurrency.CurId;

                                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);
                                    loader.Update(deal);

                                    resellerCurrencyID = countryCurrency.CurrencyId;
                                }
                            }
                        }
                    }
                    else
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                        if (countryCurrency != null)
                        {
                            resellerCurrencyID = countryCurrency.CurrencyId;
                        }
                    }

                    ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(deal.VendorId, session);
                    if (vendorCurrency != null)
                    {
                        vendorCurrencyID = vendorCurrency.CurrencyId;
                        vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                    }
                    else
                    {
                        ElioUsers vendor = Sql.GetUserById(deal.VendorId, session);
                        if (vendor != null)
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                            if (countryCurrency != null)
                            {
                                vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                                vendorCurrencyID = countryCurrency.CurrencyId;
                            }
                        }
                    }

                    if (vendorCurrencyID != "" && resellerCurrencyID != "")
                    {
                        if (vendorCurrencyID != resellerCurrencyID)
                        {
                            double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount)), resellerCurrencyID, vendorCurrencyID);
                            if (convertedAmount > 0)
                            {
                                amount = convertedAmount;
                            }
                            else
                                amount = convertedAmount;
                        }
                    }

                    table.Rows[deal.ExpectedClosedDate.Month - 1]["count"] = (Convert.ToInt32(table.Rows[deal.ExpectedClosedDate.Month - 1]["count"]) + 1).ToString();
                    table.Rows[deal.ExpectedClosedDate.Month - 1]["amount"] = (Convert.ToDecimal(table.Rows[deal.ExpectedClosedDate.Month - 1]["amount"]) + Convert.ToDecimal(amount)).ToString();
                }
            }
            else
            {
                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(vendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;
                        }
                    }
                }
            }

            return table;
        }

        public static DataTable GetVendorDealsSumAmountToVendorCurrencyByMonthByPartner(int vendorId, int resellerId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string dealRresult, int? year, out string vendorCurrencySymbol, DBSession session)
        {
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            vendorCurrencySymbol = "";

            DataTable table = new DataTable();

            DataColumn colDecimal = new DataColumn("amount");
            colDecimal.DataType = System.Type.GetType("System.Decimal");
            table.Columns.Add(colDecimal);

            DataColumn colInt32 = new DataColumn("month");
            colInt32.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(colInt32);

            DataLoader<ElioRegistrationDeals> loaderDeals = new DataLoader<ElioRegistrationDeals>(session);

            string query = @"select *
                            from  Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
		                            and cvmr.partner_user_id = rd.reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                        and rd.vendor_id = @vendor_id
                        and rd.reseller_id = @reseller_id
                        and rd.is_active = 1
                        and rd.deal_result = @deal_result ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            " and usa.is_active = 1 ";

            query += " order by month(rd.created_date)";

            List<ElioRegistrationDeals> deals = loaderDeals.Load(query
                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                    , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId)
                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealRresult));

            if (deals.Count > 0)
            {
                foreach (ElioRegistrationDeals deal in deals)
                {
                    double amount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));

                    if (string.IsNullOrEmpty(deal.CurId))
                    {
                        string partnerCountry = "";

                        ElioUsers dealPartner = Sql.GetUserById(deal.ResellerId, session);
                        if (dealPartner != null)
                            partnerCountry = dealPartner.Country;

                        if (partnerCountry != "")
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                            if (countryCurrency != null)
                            {
                                if (deal != null)
                                {
                                    deal.CurId = countryCurrency.CurId;

                                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);
                                    loader.Update(deal);

                                    resellerCurrencyID = countryCurrency.CurrencyId;
                                }
                            }
                        }
                    }
                    else
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                        if (countryCurrency != null)
                        {
                            resellerCurrencyID = countryCurrency.CurrencyId;
                        }
                    }

                    ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(deal.VendorId, session);
                    if (vendorCurrency != null)
                    {
                        vendorCurrencyID = vendorCurrency.CurrencyId;
                        vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                    }
                    else
                    {
                        ElioUsers vendor = Sql.GetUserById(deal.VendorId, session);
                        if (vendor != null)
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                            if (countryCurrency != null)
                            {
                                vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                                vendorCurrencyID = countryCurrency.CurrencyId;
                            }
                        }
                    }

                    if (vendorCurrencyID != "" && resellerCurrencyID != "")
                    {
                        if (vendorCurrencyID != resellerCurrencyID)
                        {
                            double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount)), resellerCurrencyID, vendorCurrencyID);
                            if (convertedAmount > 0)
                            {
                                amount = convertedAmount;
                            }
                            else
                                amount = convertedAmount;
                        }
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        bool exist = false;
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(table.Rows[i]["month"]) == deal.CreatedDate.Month)
                            {
                                table.Rows[i]["amount"] = Convert.ToDecimal(table.Rows[i]["amount"]) + Convert.ToDecimal(amount);
                                exist = true;
                                break;
                            }
                        }

                        if (!exist)
                            table.Rows.Add(amount, deal.CreatedDate.Month.ToString());
                    }
                    else
                        table.Rows.Add(amount, deal.CreatedDate.Month.ToString());
                }
            }
            else
            {
                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(vendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;
                        }
                    }
                }
            }

            return table;
        }

        public static DataTable GetVendorDealsSumAmountToVendorCurrencyByMonth(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string dealRresult, int? year, out string vendorCurrencySymbol, DBSession session)
        {
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            vendorCurrencySymbol = "";

            DataTable table = new DataTable();

            DataColumn colDecimal = new DataColumn("amount");
            colDecimal.DataType = System.Type.GetType("System.Decimal");
            table.Columns.Add(colDecimal);

            DataColumn colInt32 = new DataColumn("month");
            colInt32.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(colInt32);

            DataLoader<ElioRegistrationDeals> loaderDeals = new DataLoader<ElioRegistrationDeals>(session);

            string query = @"select *
                            from  Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
		                            and cvmr.partner_user_id = rd.reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                        and rd.vendor_id = @vendor_id
                        and rd.is_active = 1
                        and rd.deal_result = @deal_result ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            " and usa.is_active = 1 ";

            query += " order by month(rd.created_date)";

            List<ElioRegistrationDeals> deals = loaderDeals.Load(query
                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealRresult));

            if (deals.Count > 0)
            {
                //for (int i = 0; i < 12; i++)
                //{
                //    table.Rows.Add("0", i + 1);

                foreach (ElioRegistrationDeals deal in deals)
                {
                    double amount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));

                    if (string.IsNullOrEmpty(deal.CurId))
                    {
                        string partnerCountry = "";

                        ElioUsers dealPartner = Sql.GetUserById(deal.ResellerId, session);
                        if (dealPartner != null)
                            partnerCountry = dealPartner.Country;

                        if (partnerCountry != "")
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                            if (countryCurrency != null)
                            {
                                if (deal != null)
                                {
                                    deal.CurId = countryCurrency.CurId;

                                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);
                                    loader.Update(deal);

                                    resellerCurrencyID = countryCurrency.CurrencyId;
                                }
                            }
                        }
                    }
                    else
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                        if (countryCurrency != null)
                        {
                            resellerCurrencyID = countryCurrency.CurrencyId;
                        }
                    }

                    ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(deal.VendorId, session);
                    if (vendorCurrency != null)
                    {
                        vendorCurrencyID = vendorCurrency.CurrencyId;
                        vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                    }
                    else
                    {
                        ElioUsers vendor = Sql.GetUserById(deal.VendorId, session);
                        if (vendor != null)
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                            if (countryCurrency != null)
                            {
                                vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                                vendorCurrencyID = countryCurrency.CurrencyId;
                            }
                        }
                    }

                    if (vendorCurrencyID != "" && resellerCurrencyID != "")
                    {
                        if (vendorCurrencyID != resellerCurrencyID)
                        {
                            double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount)), resellerCurrencyID, vendorCurrencyID);
                            if (convertedAmount > 0)
                            {
                                amount = convertedAmount;
                            }
                            else
                                amount = convertedAmount;
                        }
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        bool exist = false;
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(table.Rows[i]["month"]) == deal.CreatedDate.Month)
                            {
                                table.Rows[i]["amount"] = Convert.ToDecimal(table.Rows[i]["amount"]) + Convert.ToDecimal(amount);
                                exist = true;
                                break;
                            }
                        }

                        if (!exist)
                            table.Rows.Add(amount, deal.CreatedDate.Month.ToString());
                    }
                    else
                        table.Rows.Add(amount, deal.CreatedDate.Month.ToString());

                    //if (deal.CreatedDate.Month > i + 1)
                    //    break;

                    //if (deal.CreatedDate.Month == i + 1)
                    //{
                    //    if (i > 0)
                    //        table.Rows[i]["amount"] = (Convert.ToDecimal(amount) + Convert.ToDecimal(table.Rows[i - 1]["amount"])).ToString();
                    //    else
                    //        table.Rows[i]["amount"] = amount;

                    //    break;
                    //}
                    //else
                    //{
                    //    if (i > 0)
                    //        table.Rows[i]["amount"] = table.Rows[i - 1]["amount"];
                    //}

                }
                //}
            }
            else
            {
                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(vendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;
                        }
                    }
                }
            }

            return table;
        }

        public static double GetVendorDealsTotalRevenuesToVendorCurrencyByPartner(int vendorId, int partnerId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, out string vendorCurrencySymbol, DBSession session)
        {
            double totalRevenues = 0;
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            vendorCurrencySymbol = "";

            DataTable table = new DataTable();

            DataColumn colDecimal = new DataColumn("amount");
            colDecimal.DataType = System.Type.GetType("System.Decimal");
            table.Columns.Add(colDecimal);

            DataLoader<ElioRegistrationDeals> loaderDeals = new DataLoader<ElioRegistrationDeals>(session);

            string query = @"select rd.amount,rd.cur_id,rd.reseller_id,rd.vendor_id,rd.collaboration_vendor_reseller_id
                            from  Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
		                            and cvmr.partner_user_id = rd.reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                        and rd.vendor_id = @vendor_id
                        and rd.reseller_id = @reseller_id
                        and rd.is_active = 1
                        and rd.deal_result = 'Won' ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            " and usa.is_active = 1 ";

            query += " order by rd.created_date";

            List<ElioRegistrationDeals> deals = loaderDeals.Load(query
                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                    , DatabaseHelper.CreateIntParameter("@reseller_id", partnerId));

            if (deals.Count > 0)
            {
                foreach (ElioRegistrationDeals deal in deals)
                {
                    double amount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));

                    if (string.IsNullOrEmpty(deal.CurId))
                    {
                        string partnerCountry = "";

                        ElioUsers dealPartner = Sql.GetUserById(deal.ResellerId, session);
                        if (dealPartner != null)
                            partnerCountry = dealPartner.Country;

                        if (partnerCountry != "")
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                            if (countryCurrency != null)
                            {
                                if (deal != null)
                                {
                                    deal.CurId = countryCurrency.CurId;

                                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);
                                    loader.Update(deal);

                                    resellerCurrencyID = countryCurrency.CurrencyId;
                                }
                            }
                        }
                    }
                    else
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                        if (countryCurrency != null)
                        {
                            resellerCurrencyID = countryCurrency.CurrencyId;
                        }
                    }

                    ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(deal.VendorId, session);
                    if (vendorCurrency != null)
                    {
                        vendorCurrencyID = vendorCurrency.CurrencyId;
                        vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                    }
                    else
                    {
                        ElioUsers vendor = Sql.GetUserById(deal.VendorId, session);
                        if (vendor != null)
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                            if (countryCurrency != null)
                            {
                                vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                                vendorCurrencyID = countryCurrency.CurrencyId;
                            }
                        }
                    }

                    if (vendorCurrencyID != "" && resellerCurrencyID != "")
                    {
                        if (vendorCurrencyID != resellerCurrencyID)
                        {
                            double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount)), resellerCurrencyID, vendorCurrencyID);
                            if (convertedAmount > 0)
                            {
                                amount = convertedAmount;
                            }
                            else
                                amount = convertedAmount;
                        }
                    }

                    totalRevenues += amount;
                }
            }
            else
            {
                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(vendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;
                        }
                    }
                }
            }

            return totalRevenues;
        }

        public static double GetVendorTotalRevenuesAmount(DataTable table)
        {
            double totalAmount = 0;

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    totalAmount += Convert.ToDouble(row["amount"]);
                }
            }

            return totalAmount;
        }

        public static DataTable GetVendorChannelPartnersList(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string companyNameOrEmail, int? year, DBSession session)
        {
            string query = @"select distinct(cvr.partner_user_id),u.company_name,u.country,cvr.id as vendor_reseller_id,cvr.master_user_id
	                            from Elio_users u
	                            inner join Elio_collaboration_vendors_resellers cvr on cvr.partner_user_id = u.id ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = cvr.id
						            and cvmr.partner_user_id = cvr.partner_user_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.user_id = cvr.master_user_id 
						            and usa.id = cvmr.sub_account_id ";

            query += @" where master_user_id = @master_user_id
	                    and cvr.invitation_status = 'Confirmed'
	                    and cvr.is_active = 1
                        and u.company_type ='Channel Partners'
                        and u.account_status = 1 ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += " and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' ";

            if (!string.IsNullOrEmpty(companyNameOrEmail))
                query += @" AND(u.company_name like '" + companyNameOrEmail + "%' OR u.email like '" + companyNameOrEmail + "%') ";

            if (year != null)
                query += " and (year(cvr.sysdate) >= " + year + " and year(cvr.sysdate) <= " + year + ") ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));

            return table;
        }

        public static List<ElioUsers> GetVendorCollaborationUsers(int vendorId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load(@"SELECT distinct partner_user_id 
                                  FROM elio_users u
                                  inner join Elio_collaboration_vendors_resellers vr on vr.partner_user_id = u.id
                                  where vr.master_user_id = @master_user_id
                                  and vr.invitation_status = 'Confirmed'
                                  and vr.is_active = 1"
                                , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));
        }

        public static int GetVendorPartnersCount(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string invitationStatus, DBSession session)
        {
            string query = @"SELECT count(cvr.id) as count
                                       FROM Elio_collaboration_vendors_resellers cvr ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = cvr.id
						            and cvmr.partner_user_id = cvr.partner_user_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.user_id = cvr.master_user_id 
						            and usa.id = cvmr.sub_account_id ";

            query += @" WHERE cvr.master_user_id = @master_user_id
                        AND cvr.is_active = 1
                        AND cvr.invitation_status = @invitation_status ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                       , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                       , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static DataTable GetVendorDealsAmountByMonth(int vendorId, string dealResult, int isActive, int? year, DBSession session)
        {
            string query = @"SELECT ISNULL(SUM(
                                case 
	                                when ISNULL(amount, 0) = 0
		                                then 0 
		                                else amount end), 0) as amount,
                                month(created_date) as month
                            FROM Elio_registration_deals
                            where is_public = 1 
                            and deal_result = @deal_result
                            and is_active = @is_active
                            and vendor_id = @vendor_id ";

            if (year != null)
                query += "and (year(created_date) >= " + year + " and year(created_date) <= " + year + ") ";

            query += @"group by month(created_date)
                       order by month(created_date)";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealResult)
                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return table;
        }

        public static DataTable GetVendorDealsCountByMonthByPartner(int vendorId, int resellerId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int isActive, int? year, DBSession session)
        {
            string query = @"SELECT count(rd.id) as count
                            ,month(rd.created_date) as month
                            FROM Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id 
		                            and cvmr.partner_user_id = rd.reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                        and rd.is_active = @is_active
                        and rd.vendor_id = @vendor_id 
                        and rd.reseller_id = @reseller_id ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += @" group by month(rd.created_date)
                       order by month(rd.created_date)";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            return table;
        }

        public static DataTable GetVendorDealsCountByMonth(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int isActive, int? year, DBSession session)
        {
            string query = @"SELECT count(rd.id) as count
                            ,month(rd.created_date) as month
                            FROM Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id 
		                            and cvmr.partner_user_id = rd.reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                        and rd.is_active = @is_active
                        and rd.vendor_id = @vendor_id ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += @" group by month(rd.created_date)
                       order by month(rd.created_date)";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return table;
        }

        public static DataTable GetVendorForecastingDealsAmountByMonth(int vendorId, int status, string dealResult, int isActive, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count,
                                                            ISNULL(SUM(
                                                                case 
	                                                                when ISNULL(amount, 0) = 0
		                                                                then 0 
		                                                                else amount end), 0) as amount,
                                                        month(expected_closed_date) as month
                                                        FROM Elio_registration_deals
                                                        where is_public = 1 
                                                        and deal_result = @deal_result
                                                        and status = @status
                                                        and is_active = @is_active
                                                        and vendor_id = @vendor_id
                                                        and year(created_date) >= year(getdate())
                                                        and expected_closed_date >= getdate()
                                                        group by month(expected_closed_date)
                                                        order by month(expected_closed_date)"
                                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealResult)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return table;
        }

        public static DataTable GetVendorLeadsCountByMonthByPartner(int vendorId, int resellerId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int? year, DBSession session)
        {
            string query = @"SELECT count(ld.id) as count
                            ,month(ld.created_date) as month
                            FROM Elio_lead_distributions ld ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
		                            and cvmr.partner_user_id = ld.reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
            }

            query += @" where ld.is_public = 1
                        and ld.vendor_id = @vendor_id
                        and ld.reseller_id = @reseller_id ";

            if (year != null)
                query += " and (year(ld.created_date) >= " + year + " and year(ld.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += @" group by month(ld.created_date)
                        order by month(ld.created_date)";

            DataTable table = session.GetDataTable(query
                                            , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                            , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            return table;
        }

        public static DataTable GetVendorLeadsCountByMonth(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int? year, DBSession session)
        {
            string query = @"SELECT count(ld.id) as count
                            ,month(ld.created_date) as month
                            FROM Elio_lead_distributions ld ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
		                            and cvmr.partner_user_id = ld.reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
            }

            query += @" where ld.is_public = 1
                        and ld.vendor_id = @vendor_id ";

            if (year != null)
                query += " and (year(ld.created_date) >= " + year + " and year(ld.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += @" group by month(ld.created_date)
                        order by month(ld.created_date)";

            DataTable table = session.GetDataTable(query
                                            , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return table;
        }

        public static DataTable GetVendorDealsCountByStatusByMonthByPartner(int vendorId, int resellerId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int status, int isActive, int? year, DBSession session)
        {
            string query = @"SELECT count(rd.id) as count_by_status
                            ,month(rd.created_date) as month
                            FROM Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                        and rd.is_active = @is_active
                        and rd.status = @status 
                        and rd.vendor_id = @vendor_id
                        and rd.reseller_id = @reseller_id ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += @" group by month(rd.created_date)
                        order by month(rd.created_date)";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            return table;
        }

        public static DataTable GetVendorDealsCountByStatusByMonth(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int status, int isActive, int? year, DBSession session)
        {
            string query = @"SELECT count(rd.id) as count_by_status
                            ,month(rd.created_date) as month
                            FROM Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                        and rd.is_active = @is_active
                        and rd.status = @status 
                        and rd.vendor_id = @vendor_id ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += @" group by month(rd.created_date)
                        order by month(rd.created_date)";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return table;
        }

        public static DataTable GetVendorLeadsCountByStatusByMonthByPartner(int vendorId, int resellerId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int status, int? year, DBSession session)
        {
            string query = @"SELECT count(ld.id) as count_by_status
                            ,month(ld.created_date) as month
                            FROM Elio_lead_distributions ld ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = ld.reseller_id
		                            and cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
            }

            query += @" where ld.is_public = 1
                        and ld.status = @status 
                        and ld.vendor_id = @vendor_id
                        and ld.reseller_id = @reseller_id ";

            if (year != null)
                query += " and (year(ld.created_date) >= " + year + " and year(ld.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += @" group by month(ld.created_date)
                       order by month(ld.created_date)";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            return table;
        }

        public static DataTable GetVendorLeadsCountByStatusByMonth(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int status, int? year, DBSession session)
        {
            string query = @"SELECT count(ld.id) as count_by_status
                            ,month(ld.created_date) as month
                            FROM Elio_lead_distributions ld ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = ld.reseller_id
		                            and cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
            }

            query += @" where ld.is_public = 1
                        and ld.status = @status 
                        and ld.vendor_id = @vendor_id ";

            if (year != null)
                query += " and (year(ld.created_date) >= " + year + " and year(ld.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += @" group by month(ld.created_date)
                       order by month(ld.created_date)";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return table;
        }

        public static string GetVendorDealsAmountSize(int vendorId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT ISNULL(SUM(
                                                            case 
	                                                            when ISNULL(amount, 0) = 0
		                                                            then 0 
		                                                            else amount end), 0) as amount
                                                     FROM Elio_registration_deals
                                                     where is_public = 1
                                                     and is_active = 1
                                                     and vendor_id = @vendor_id"
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return table.Rows[0]["amount"].ToString();
        }

        public static string GetVendorDealsAmountSizeByChannelPartner(int vendorId, int reseller_id, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT ISNULL(SUM(
                                                            case 
	                                                            when ISNULL(amount, 0) = 0
		                                                            then 0 
		                                                            else amount end), 0) as amount
                                                     FROM Elio_registration_deals
                                                     where is_public = 1
                                                     and is_active = 1
                                                     and vendor_id = @vendor_id
                                                     and reseller_id = @reseller_id"
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", reseller_id));

            return table.Rows[0]["amount"].ToString();
        }

        public static int GetVendorTotalDealsCountByResult(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string dealResult, int? year, DBSession session)
        {
            string query = @"SELECT COUNT(rd.id) as count
                                FROM Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                       and rd.is_active = 1
                       and rd.vendor_id = @vendor_id
                       and rd.deal_result = @deal_result ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            " and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealResult));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetVendorTotalDealsCountByResultByChannelPartner(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int resellerId, string dealResult, int? year, DBSession session)
        {
            string query = @"SELECT COUNT(rd.id) as count
                                                     FROM Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                      and rd.is_active = 1
                      and rd.vendor_id = @vendor_id
                      and rd.deal_result = @deal_result
                      and rd.reseller_id = @reseller_id ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealResult)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetVendorAverageDealsSalesCycleDaysByResult(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string dealResult, int? year, DBSession session)
        {
            string query = @"SELECT AVG(DATEDIFF(day,created_date,last_update)) as avg_days
                                                     FROM Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                       and rd.is_active = 1
                       and rd.vendor_id = @vendor_id
                       and rd.deal_result = @deal_result ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            " and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealResult));

            return Convert.ToInt32(table.Rows[0]["avg_days"]);
        }

        public static int GetVendorAverageDealsSalesCycleDaysByResultByChannelPartner(int vendorId, int reseller_id, string dealResult, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT ISNULL(AVG(DATEDIFF(day,created_date,last_update)), 0) as avg_days
                                                     FROM Elio_registration_deals
                                                     where is_public = 1
                                                     and is_active = 1
                                                     and vendor_id = @vendor_id
                                                     and deal_result = @deal_result
                                                     and reseller_id = @reseller_id"
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", reseller_id)
                                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealResult));

            return Convert.ToInt32(table.Rows[0]["avg_days"]);
        }

        public static int GetVendorAverageDealsSalesCycleDaysByStatusByChannelPartner(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int reseller_id, int status, int? year, DBSession session)
        {
            string query = @"SELECT ISNULL(AVG(DATEDIFF(day,rd.created_date,rd.last_update)), 0) as avg_days
                            FROM Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                      and rd.is_active = 1
                      and rd.vendor_id = @vendor_id
                      and rd.status = @status
                      and rd.reseller_id = @reseller_id ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", reseller_id)
                                                    , DatabaseHelper.CreateIntParameter("@status", status));

            return Convert.ToInt32(table.Rows[0]["avg_days"]);
        }

        public static int GetVendorTotalDealsCount(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int? year, DBSession session)
        {
            string query = @"SELECT COUNT(rd.id) as count
                            FROM Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                       and rd.is_active = 1
                       and rd.vendor_id = @vendor_id ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            " and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetVendorTotalDealsNotPendingCountByChannelPartner(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int resellerId, int? year, DBSession session)
        {
            string query = @"SELECT COUNT(rd.id) as count
                                                     FROM Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                      and rd.is_active = 1
                      and rd.vendor_id = @vendor_id
                      and rd.reseller_id = @reseller_id
                      and rd.deal_result <> 'Pending' ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static DataTable GetVendorLeadsAmount(int vendorId, string leadResult, int status, DBSession session)
        {
            return session.GetDataTable(@"SELECT SUM(amount) as amount, month(created_date) as month
                                                     FROM Elio_lead_distributions
                                                     where is_public = 1
                                                     and lead_result = @lead_result
                                                     and status = @status
                                                     and vendor_id = @vendor_id
                                                     group by month(created_date)
                                                     order by month(created_date)"
                                                    , DatabaseHelper.CreateStringParameter("@lead_result", leadResult)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));
        }

        public static int GetTotalDealsCount(string dealResult, int isActive, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_registration_deals
                                                     where is_public = 1 
                                                     and deal_result = @deal_result
                                                     and is_active = @is_active"
                                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealResult)
                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetTotalLeadsCount(string leadResult, int status, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_lead_distributions
                                                     where is_public = 1
                                                     and lead_result = @lead_result
                                                     and status = @status"
                                                    , DatabaseHelper.CreateStringParameter("@lead_result", leadResult)
                                                    , DatabaseHelper.CreateIntParameter("@status", status));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetVendorTotalLeadsCountByResult(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string leadResult, int? year, DBSession session)
        {
            string query = @"SELECT COUNT(ld.id) as count
                                FROM Elio_lead_distributions ld ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = ld.reseller_id
		                            and cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
            }

            query += @" where ld.is_public = 1
                       and ld.lead_result = @lead_result
                       and ld.vendor_id = @vendor_id ";

            if (year != null)
                query += "and (year(ld.created_date) >= " + year + " and year(ld.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            " and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateStringParameter("@lead_result", leadResult)
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetVendorTotalLeadsCountByResultByChannelPartner(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int reseller_id, string leadResult, int? year, DBSession session)
        {
            string query = @"SELECT COUNT(ld.id) as count
                            FROM Elio_lead_distributions ld ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = ld.reseller_id
		                            and cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
            }

            query += @" where ld.is_public = 1
                      and ld.lead_result = @lead_result
                      and ld.vendor_id = @vendor_id
                      and ld.reseller_id = @reseller_id ";

            if (year != null)
                query += " and (year(ld.created_date) >= " + year + " and year(ld.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateStringParameter("@lead_result", leadResult)
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", reseller_id));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetVendorTotalLeadsCount(int vendorId, int? year, DBSession session)
        {
            string query = @"SELECT COUNT(id) as count
                            FROM Elio_lead_distributions
                            where is_public = 1
                            and vendor_id = @vendor_id ";

            if (year != null)
                query += "and (year(created_date) >= " + year + " and year(created_date) <= " + year + ") ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetVendorTotalLeadsNotPendingCountByChannelPartner(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int reseller_id, int? year, DBSession session)
        {
            string query = @"SELECT COUNT(ld.id) as count
                            FROM Elio_lead_distributions ld ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = ld.reseller_id
		                            and cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
            }

            query += @" where ld.is_public = 1
                      and ld.vendor_id = @vendor_id
                      and ld.reseller_id = @reseller_id
                      and ld.lead_result <> 'Pending' ";

            if (year != null)
                query += " and (year(ld.created_date) >= " + year + " and year(ld.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", reseller_id));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetTotalPartnersCount(int vendorId, string invitationStatus, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_collaboration_vendors_resellers
                                                     where is_active = 1
                                                     and master_user_id = @master_user_id
                                                     and invitation_status = @invitation_status"
                                                    , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus)
                                                    , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static DataTable GetVendorStatisticsTable(int vendorId, string companyNameOrEmail, DBSession session)
        {
            string strQuery = @"select distinct(partner_user_id),company_name
                                ,case when l_amount.leads_amount is null then 0 else SUM(l_amount.leads_amount) end as leads_amount
                                ,case when d_amount.deals_amount is null then 0 else SUM(d_amount.deals_amount) end as deals_amount
                                from Elio_users u
                                inner join Elio_collaboration_vendors_resellers cvr on cvr.partner_user_id = u.id
                                inner join Elio_collaboration_vendor_reseller_invitations inv on inv.vendor_reseller_id = cvr.id
                                outer apply
                                (
	                                select 
	                                distinct(reseller_id)
	                                ,sum(amount) as leads_amount
	                                from Elio_lead_distributions l
	                                where 1 =1
	                                and vendor_id = cvr.master_user_id 
	                                and cvr.partner_user_id = l.reseller_id
	                                group by reseller_id
                                )l_amount
                                outer apply 
                                (
	                                select 
	                                distinct(reseller_id)
	                                ,sum(amount) as deals_amount
	                                from Elio_registration_deals d
	                                where 1 = 1
	                                and d.vendor_id = cvr.master_user_id 
	                                and d.reseller_id = cvr.partner_user_id
	                                and d.collaboration_vendor_reseller_id = cvr.id
	                                group by reseller_id
                                ) d_amount
                                where master_user_id = @master_user_id
                                and inv.invitation_step_description = 'Confirmed'
                                and cvr.partner_user_id = d_amount.reseller_id ";

            if (!string.IsNullOrEmpty(companyNameOrEmail))
                strQuery += @" AND(u.company_name like '" + companyNameOrEmail + "%' OR u.email like '" + companyNameOrEmail + "%') ";

            strQuery += @" group by partner_user_id,company_name
                           ,l_amount.leads_amount
                           ,d_amount.deals_amount 
                           order by deals_amount desc";

            DataTable table = session.GetDataTable(strQuery
                                                , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));

            return table;
        }

        public static DataTable GetVendorActivePartnersStatisticsTable(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string companyNameOrEmail, int top, int? year, DBSession session)
        {
            string query = "";
            DataTable table = null;

            if (top == 0)
            {
                query = @"select distinct (cvr.partner_user_id)
                        ,company_name
                        ,u.user_login_count
                        ,case when d_amount.deals_count is not null then d_amount.deals_count else 0 end as deals_count
                        ,u.user_application_type
                        from Elio_users u
                        inner join Elio_collaboration_vendors_resellers cvr on cvr.partner_user_id = u.id ";

                if (loggedInRoleId > 0 && !isAdminRole)
                    query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					                on cvmr.vendor_reseller_id = cvr.id 
						                and cvmr.partner_user_id = cvr.partner_user_id
				                inner join Elio_users_sub_accounts usa 
					                on usa.user_id = cvr.master_user_id 
						                and usa.id = cvmr.sub_account_id ";

                query += @" inner join Elio_collaboration_vendor_reseller_invitations inv on inv.vendor_reseller_id = cvr.id
                        outer apply 
                        (
                            select 
                            distinct(reseller_id)
                            ,count(reseller_id) as deals_count
                            from Elio_registration_deals d
                            where 1 = 1
                            and d.vendor_id = cvr.master_user_id 
                            and d.reseller_id = cvr.partner_user_id
                            and d.collaboration_vendor_reseller_id = cvr.id ";

                if (year != null)
                    query += " and (year(d.created_date) >= " + year + " and year(d.created_date) <= " + year + ") ";

                query += @"group by reseller_id
                        ) d_amount
                        where master_user_id = @master_user_id
                        and inv.invitation_step_description = 'Confirmed' ";

                if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                    query += " and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' ";

                if (!string.IsNullOrEmpty(companyNameOrEmail))
                    query += @" AND(u.company_name like '" + companyNameOrEmail + "%' OR u.email like '" + companyNameOrEmail + "%')";

                query += " order by user_login_count desc";
            }
            else
            {
                query = @"select distinct top " + top + " (cvr.partner_user_id) " +
                            ",company_name " +
                            ",u.user_login_count " +
                            ",case when d_amount.deals_count is not null then d_amount.deals_count else 0 end as deals_count " +
                            ",u.user_application_type " +
                            "from Elio_users u " +
                            "inner join Elio_collaboration_vendors_resellers cvr on cvr.partner_user_id = u.id ";

                if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                    query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					                on cvmr.vendor_reseller_id = cvr.id
						                and cvmr.partner_user_id = cvr.partner_user_id
				                inner join Elio_users_sub_accounts usa 
					                on usa.user_id = cvr.master_user_id 
						                and usa.id = cvmr.sub_account_id ";

                query += @"inner join Elio_collaboration_vendor_reseller_invitations inv on inv.vendor_reseller_id = cvr.id " +
                            "outer apply  " +
                            "( " +
                            "   select " +
                            "   distinct(reseller_id) " +
                            "   ,count(reseller_id) as deals_count " +
                            "   from Elio_registration_deals d " +
                            "   where 1 = 1 " +
                            "   and d.vendor_id = cvr.master_user_id " +
                            "   and d.reseller_id = cvr.partner_user_id " +
                            "   and d.collaboration_vendor_reseller_id = cvr.id ";


                if (year != null)
                    query += " and (year(d.created_date) >= " + year + " and year(d.created_date) <= " + year + ") ";

                query += @" group by reseller_id " +
                        ") d_amount " +
                        "where master_user_id = @master_user_id " +
                        "and inv.invitation_step_description = 'Confirmed' ";

                if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                    query += " and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' ";

                if (!string.IsNullOrEmpty(companyNameOrEmail))
                    query += @" AND(u.company_name like '" + companyNameOrEmail + "%' OR u.email like '" + companyNameOrEmail + "%')";

                query += " order by user_login_count desc";
            }

            table = session.GetDataTable(query
                                        , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));

            return table;
        }

        public static int GetVendorLeadsByStatusAndResult(int vendorId, int status, string result, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count FROM Elio_lead_distributions ld 
                                                        WHERE 1 = 1
                                                        AND ld.is_public = 1
                                                        AND ld.status = @status
                                                        AND lead_result = @lead_result
                                                        AND vendor_id = @vendor_id"
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateStringParameter("@lead_result", result));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetVendorLeadsByStatusAndResultByChannelPartner(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int reseller_id, int status, string result, int? year, DBSession session)
        {
            string query = @"SELECT COUNT(ld.id) as count FROM Elio_lead_distributions ld ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = ld.reseller_id
		                            and cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
            }

            query += @" WHERE 1 = 1
                      AND ld.is_public = 1
                      AND ld.status = @status
                      AND ld.lead_result = @lead_result
                      AND ld.vendor_id = @vendor_id
                      AND ld.reseller_id = @reseller_id ";

            if (year != null)
                query += " and (year(ld.created_date) >= " + year + " and year(ld.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", reseller_id)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateStringParameter("@lead_result", result));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetVendorDealsByStatusAndResultByChannelPartner(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int reseller_id, int status, string result, int? year, DBSession session)
        {
            string query = @"SELECT COUNT(rd.id) as count FROM Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" WHERE 1 = 1
                        AND rd.is_public = 1
                        AND rd.status = @status
                        AND rd.deal_result = @deal_result
                        AND rd.vendor_id = @vendor_id
                        AND rd.reseller_id = @reseller_id ";

            if (year != null)
                query += " and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            DataTable table = session.GetDataTable(query
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@reseller_id", reseller_id)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateStringParameter("@deal_result", result));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetVendorDealsByStatusAndResult(int vendorId, int status, string result, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count FROM Elio_registration_deals d 
                                                        WHERE 1 = 1
                                                        AND d.is_public = 1
                                                        AND ld.status = @status
                                                        AND deal_result = @deal_result
                                                        AND vendor_id = @vendor_id"
                                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateStringParameter("@deal_result", result));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static DataTable GetVendorTierManagementGoalsAmountByDescription(int vendorId, int partnerId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string year, DBSession session)
        {
            string query = @"SELECT 
                                sum(cast(isnull(ugv.goal_value, 0) as int)) as sum_goals,sum(cast(isnull(ugv.user_goal_value, 0) as int)) as sum_users_goals,
                                ug.description
                                FROM Elio_tier_management_users_goals_value ugv
                                inner join Elio_tier_management_users_goals ug
	                                on ugv.goal_id = ug.id ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_resellers cvr
	                            on cvr.master_user_id = ug.vendor_id
		                            and cvr.partner_user_id = ug.partner_id
                            inner join Elio_collaboration_vendors_members_resellers cvmr 
	                            on cvmr.vendor_reseller_id = cvr.id
		                            and cvmr.partner_user_id = cvr.partner_user_id
                            inner join Elio_users_sub_accounts usa 
	                            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ug.vendor_id ";
            }

            query += @" where ug.vendor_id = @vendor_id 
                        and ug.partner_id = @partner_id";

            if (year != "0")
                query += " and year = " + year + " ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            query += @" group by description";

            DataTable table = session.GetDataTable(query
                                            , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId));

            return table;
        }
    }
}