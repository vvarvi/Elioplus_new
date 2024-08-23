using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Analytics;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Roles;
using WdS.ElioPlus.Lib.Roles.EnumsRoles;

namespace WdS.ElioPlus
{
    public partial class DashboardAnalyticsSalesLeaderboardPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public bool HasDiscount { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, session);

                    if (isError)
                    {
                        Response.Redirect(vSession.Page = errorPage, false);
                        return;
                    }

                    bool hasRight = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardAnalyticsSalesLeaderboardPage", Actions.View, session);
                    if (!hasRight)
                    {
                        Response.Redirect(ControlLoader.PageDash405, false);
                        return;
                    }

                    SetLinks();

                    if (!IsPostBack)
                    {
                        FixPage();
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Default(), false);
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

        # region Methods

        private void LoadYearsList()
        {
            DrpYears.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "1";
            item.Text = DateTime.Now.Year.ToString();
            item.Selected = true;

            DrpYears.Items.Add(item);

            item = new ListItem();
            item.Value = "2";
            item.Text = DateTime.Now.AddYears(-1).Year.ToString();

            DrpYears.Items.Add(item);

            item = new ListItem();
            item.Value = "3";
            item.Text = DateTime.Now.AddYears(-2).Year.ToString();

            DrpYears.Items.Add(item);

            item = new ListItem();
            item.Value = "0";
            item.Text = "All";

            DrpYears.Items.Add(item);

            DrpYears.DataBind();
        }

        private void FixPage()
        {
            if (!IsPostBack)
            {
                UpdateStrings();
                LoadYearsList();
            }

            if (vSession.User.Id == GlobalMethods.GetRandstadCustomerID())
            {
                RdgResellers.MasterTableView.GetColumn("leads_count").Display = false;
                RdgResellers.MasterTableView.GetColumn("leads_amount").Display = false;
                RdgResellers.MasterTableView.GetColumn("leads_result").Display = false;
                RdgResellers.MasterTableView.GetColumn("leads_won_perc").Display = false;
            }

            //if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
                //divPayments.Visible = liPayments.Visible = true;
            //    LblRenewalHead.Visible = LblRenewal.Visible = true;
            //    LblRenewalHead.Text = "Renewal date: ";

            //    ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
            //    if (packet != null)
            //    {
            //        LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
            //    }

            //    try
            //    {
            //        LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");      //Sql.GetUserRenewalDateFromActiveOrder(vSession.User.Id, session).ToString("MM/dd/yyyy");
            //    }
            //    catch (Exception)
            //    {
            //        LblRenewalHead.Visible = LblRenewal.Visible = false;

            //        Logger.Debug(string.Format("User {0} seems to be premium but he has no subscription in his account", vSession.User.Id.ToString()));
            //    }
            //}
            //else
            //{
            //    //divPayments.Visible = liPayments.Visible = Sql.HasUserOrderByStatus(vSession.User.Id, Convert.ToInt32(OrderStatus.Canceled), session);
            //    LblRenewalHead.Visible = LblRenewal.Visible = false;
            //    LblPricingPlan.Text = "You are currently on a free plan";
            //}

            //aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

            if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {

            }
            else
            {

            }

            //LblDashboard.Text = "Dashboard";
            //LblGoFull.Text = "Complete your registration";
            //LblDashPage.Text = "Sales Leaderboard";
        }

        private void UpdateStrings()
        {
            ////LblPlanName.Text = "Premium plan";
            ////LblConnectionTrialTxt.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "58")).Text;
            ////LblConnectionTrialTxt2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "59")).Text;
            ////LblPricing.Text = "Feature Details";
            ////LblConHead.Text = "Connections";
            ////LblConMain.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "40")).Text;
            ////LblLeadsHead.Text = "Lead notifications";
            ////LblLeadsContent.Text = "Receive in your dashboard and inbox leads from companies that showed an interest and browsed your company profile.";
            ////LblSearchHead.Text = "Searches";
            ////LblMessHead.Text = "Direct messages";
            ////LblMessContent.Text = "Send direct messages to companies in our platform that you want to engage directly. After you browse and visit a company's profile you'll be able to send a direct message from the tab option or from your dashboard.";
            //////LblPurchasesTitle.Text = "Your purchases";
            ////LblFirstPage.Text = "1st page in results";
            ////LblUnlimProf.Text = "View unlimited profiles";
            ////LblRetarget.Text = "Opportunities management";
            ////LblFirstPg.Text = "1st page results";
            ////LblFirstPgCont.Text = "Appear in first place in search results every time someone searches for your industry, product or vertical, location etc.";
            ////LblUnlim.Text = "Searches";
            ////LblUnlimCont.Text = "Search and view unlimited profiles in our platform. Additionally, you can use the advance search to laser target your potential partners.";
            ////LblRetar.Text = "Opportunities management";
            ////LblRetarCont.Text = "We offer to all our premium accounts the ability to manage their partnership opportunities with one single feature. If a connection we provide with a company fit your needs, you can add it as a partnership opportunity and you can even create and add your current partners.";
            ////LblSearchContent.Text = "Save those companies that you find interesting and would like to revisit in the future directly in your dashboard. You can add or delete anytime and keep your list always updated. Search and view unlimited profiles in our platform. Additionally, you can use the advance search to laser target your potential partners.";

            //LblSignUp.Text = (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "60")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "31")).Text;
        }

        private void SetLinks()
        {

        }

        private bool IsValidData()
        {
            bool isValid = true;

            return isValid;
        }

        #endregion

        #region Grids

        protected void RdgResellers_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent

                    GridDataItem item = (GridDataItem)e.Item;

                    //int totalDealsCount = StatsDB.GetVendorTotalDealsNotPendingCountByChannelPartner(vSession.User.Id, Convert.ToInt32(item["partner_user_id"].Text), session);
                    //if (totalDealsCount > 0)
                    //{
                    //    int dealsWon = StatsDB.GetVendorTotalDealsCountByResultByChannelPartner(vSession.User.Id, Convert.ToInt32(item["partner_user_id"].Text), DealResultStatus.Won.ToString(), session);
                    //    int dealsLost = StatsDB.GetVendorTotalDealsCountByResultByChannelPartner(vSession.User.Id, Convert.ToInt32(item["partner_user_id"].Text), DealResultStatus.Lost.ToString(), session);

                    //    item["deals_result"].Text = dealsWon.ToString() + "/" + dealsLost.ToString();

                    //    decimal winPerc = (dealsWon * 100) / totalDealsCount;

                    //    item["deals_won_perc"].Text = winPerc.ToString() + " %";

                    //    decimal dealsSize = Convert.ToDecimal(StatsDB.GetVendorDealsAmountSizeByChannelPartner(vSession.User.Id, Convert.ToInt32(item["partner_user_id"].Text), session));
                    //    decimal averageSize = dealsSize / totalDealsCount;

                    //    item["deals_average_size"].Text = "$ " + averageSize.ToString("0.00");

                    //    item["deals_average_sales_cycle"].Text = StatsDB.GetVendorAverageDealsSalesCycleDaysByStatusByChannelPartner(vSession.User.Id, Convert.ToInt32(item["partner_user_id"].Text), (int)DealStatus.Closed, session).ToString() + " days";

                    //    item["deals_amount"].Text = "$ " + item["deals_amount"].Text;
                    //    //string averageSize = "";
                    //    //item["deals_amount"].Text = GlobalDBMethods.ConvertDealAmountToVendorCurrency(deal, averageSize, session);
                    //    //item["deals_average_size"].Text = averageSize;
                    //}
                    //else
                    //{
                    //    item["deals_result"].Text = "0/0";
                    //    item["deals_won_perc"].Text = "0 %";
                    //    item["deals_average_size"].Text = "$ 0";
                    //    item["deals_average_sales_cycle"].Text = "0 days";
                    //    item["deals_amount"].Text = "$ 0";
                    //}

                    //int totalLeadsCount = StatsDB.GetVendorTotalLeadsNotPendingCountByChannelPartner(vSession.User.Id, Convert.ToInt32(item["partner_user_id"].Text), session);
                    //if (totalLeadsCount > 0)
                    //{
                    //    int leadsWon = StatsDB.GetVendorTotalLeadsCountByResultByChannelPartner(vSession.User.Id, Convert.ToInt32(item["partner_user_id"].Text), DealResultStatus.Won.ToString(), session);
                    //    int leadsLost = StatsDB.GetVendorTotalLeadsCountByResultByChannelPartner(vSession.User.Id, Convert.ToInt32(item["partner_user_id"].Text), DealResultStatus.Lost.ToString(), session);

                    //    item["leads_result"].Text = leadsWon.ToString() + "/" + leadsLost.ToString();

                    //    decimal winPerc = (leadsWon * 100) / totalLeadsCount;

                    //    item["leads_won_perc"].Text = winPerc.ToString() + " %";

                    //    item["leads_amount"].Text = "$ " + item["leads_amount"].Text;
                    //}
                    //else
                    //{
                    //    item["leads_result"].Text = "0/0";
                    //    item["leads_won_perc"].Text = "0 %";
                    //    item["leads_amount"].Text = "$ 0";
                    //}

                    //int openLeads = StatsDB.GetVendorLeadsByStatusAndResultByChannelPartner(vSession.User.Id, Convert.ToInt32(item["partner_user_id"].Text), (int)DealStatus.Open, DealResultStatus.Pending.ToString(), session);
                    //int openDeals = StatsDB.GetVendorDealsByStatusAndResultByChannelPartner(vSession.User.Id, Convert.ToInt32(item["partner_user_id"].Text), (int)DealStatus.Open, DealResultStatus.Pending.ToString(), session);

                    //item["deals_leads_pending"].Text = openLeads.ToString() + " leads / " + openDeals.ToString() + " deals";

                    #endregion
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

        protected void RdgResellers_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable partnersTbl = null;

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        partnersTbl = StatsDB.GetVendorChannelPartnersList(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, RtbxCompanyNameEmail.Text.Trim(), null, session);
                    }

                    if (partnersTbl != null && partnersTbl.Rows.Count > 0)
                    {
                        DataTable table = new DataTable();

                        table.Columns.Add("vendor_reseller_id");
                        table.Columns.Add("master_user_id");
                        table.Columns.Add("partner_user_id");
                        table.Columns.Add("company_name");
                        table.Columns.Add("deals_count");
                        table.Columns.Add("deals_amount");
                        table.Columns.Add("leads_count");
                        table.Columns.Add("leads_amount");
                        table.Columns.Add("deals_result");
                        table.Columns.Add("deals_won_perc");
                        table.Columns.Add("leads_result");
                        table.Columns.Add("leads_won_perc");
                        table.Columns.Add("deals_leads_pending");
                        table.Columns.Add("deals_average_size");
                        table.Columns.Add("deals_average_sales_cycle");

                        string dealsAverageSize = "";
                        string leadsAverageSize = "";
                        string deals_result = "";
                        string deals_won_perc = "";
                        int totalDealsCount = 0;
                        string deals_amount = "";
                        string deals_average_size = "";
                        string deals_average_sales_cycle = "";

                        string leads_result = "";
                        string leads_won_perc = "";
                        int totalLeadsCount = 0;
                        string leads_amount = "";
                        string vendorCurrencySymbol = "";

                        foreach (DataRow row in partnersTbl.Rows)
                        {
                            totalDealsCount = (DrpYears.SelectedValue == "0") ? StatsDB.GetVendorTotalDealsNotPendingCountByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), null, session) : StatsDB.GetVendorTotalDealsNotPendingCountByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), Convert.ToInt32(DrpYears.SelectedItem.Text), session);
                            if (totalDealsCount > 0)
                            {
                                int dealsWon = (DrpYears.SelectedValue == "0") ? StatsDB.GetVendorTotalDealsCountByResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Won.ToString(), null, session) : StatsDB.GetVendorTotalDealsCountByResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Won.ToString(), Convert.ToInt32(DrpYears.SelectedItem.Text), session);
                                int dealsLost = (DrpYears.SelectedValue == "0") ? StatsDB.GetVendorTotalDealsCountByResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Lost.ToString(), null, session):StatsDB.GetVendorTotalDealsCountByResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Lost.ToString(), Convert.ToInt32(DrpYears.SelectedItem.Text), session);

                                deals_result = dealsWon.ToString() + "/" + dealsLost.ToString();

                                decimal winPerc = (dealsWon * 100) / totalDealsCount;

                                deals_won_perc = winPerc.ToString() + " %";

                                List<ElioRegistrationDeals> deals = (DrpYears.SelectedValue == "0") ? Sql.GetDealsBy(Convert.ToInt32(row["vendor_reseller_id"].ToString()), Convert.ToInt32(row["master_user_id"].ToString()), Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Won.ToString(), (int)DealStatus.Closed, null, session) : Sql.GetDealsBy(Convert.ToInt32(row["vendor_reseller_id"].ToString()), Convert.ToInt32(row["master_user_id"].ToString()), Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Won.ToString(), (int)DealStatus.Closed, Convert.ToInt32(DrpYears.SelectedItem.Text), session);

                                if (deals.Count > 0)
                                {
                                    deals_amount = GlobalDBMethods.ConvertDealsSumAmountToVendorCurrency(vSession.User.Id, deals, out dealsAverageSize, session);

                                    deals_average_size = dealsAverageSize;

                                    deals_average_sales_cycle = (DrpYears.SelectedValue == "0") ? StatsDB.GetVendorAverageDealsSalesCycleDaysByStatusByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), (int)DealStatus.Closed, null, session).ToString() + " days" : StatsDB.GetVendorAverageDealsSalesCycleDaysByStatusByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), (int)DealStatus.Closed, Convert.ToInt32(DrpYears.SelectedItem.Text), session).ToString() + " days";
                                }
                            }
                            else
                            {
                                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vSession.User.Id, session);
                                if (vendorCurrency != null)
                                {
                                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                                }
                                else
                                {
                                    ElioUsers vendor = Sql.GetUserById(vSession.User.Id, session);
                                    if (vendor != null)
                                    {
                                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                                        if (countryCurrency != null)
                                        {
                                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;
                                        }
                                    }
                                }

                                deals_result = "0/0";
                                deals_won_perc = "0 %";
                                deals_average_size = vendorCurrencySymbol + " 0";
                                deals_average_sales_cycle = "0 days";
                                deals_amount = vendorCurrencySymbol + " 0";
                            }

                            int openLeads = 0;

                            if (vSession.User.Id != GlobalMethods.GetRandstadCustomerID())
                            {
                                totalLeadsCount = (DrpYears.SelectedValue == "0") ? StatsDB.GetVendorTotalLeadsNotPendingCountByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), null, session) : StatsDB.GetVendorTotalLeadsNotPendingCountByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), Convert.ToInt32(DrpYears.SelectedItem.Text), session);
                                if (totalLeadsCount > 0)
                                {
                                    int leadsWon = (DrpYears.SelectedValue == "0") ? StatsDB.GetVendorTotalLeadsCountByResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Won.ToString(), null, session): StatsDB.GetVendorTotalLeadsCountByResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Won.ToString(), Convert.ToInt32(DrpYears.SelectedItem.Text), session);
                                    int leadsLost = (DrpYears.SelectedValue == "0") ? StatsDB.GetVendorTotalLeadsCountByResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Lost.ToString(), null, session): StatsDB.GetVendorTotalLeadsCountByResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Lost.ToString(), Convert.ToInt32(DrpYears.SelectedItem.Text), session);

                                    leads_result = leadsWon.ToString() + " /" + leadsLost.ToString();

                                    decimal winPerc = (leadsWon * 100) / totalLeadsCount;

                                    leads_won_perc = winPerc.ToString() + " %";

                                    List<ElioLeadDistributions> leads = (DrpYears.SelectedValue == "0") ? Sql.GetLeadsBy(Convert.ToInt32(row["vendor_reseller_id"].ToString()), Convert.ToInt32(row["master_user_id"].ToString()), Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Won.ToString(), (int)DealStatus.Closed, null, session) : Sql.GetLeadsBy(Convert.ToInt32(row["vendor_reseller_id"].ToString()), Convert.ToInt32(row["master_user_id"].ToString()), Convert.ToInt32(row["partner_user_id"].ToString()), DealResultStatus.Won.ToString(), (int)DealStatus.Closed, Convert.ToInt32(DrpYears.SelectedItem.Text), session);

                                    if (leads.Count > 0)
                                    {
                                        leads_amount = GlobalDBMethods.ConvertLeadsSumAmountToVendorCurrency(vSession.User.Id, leads, out leadsAverageSize, session);
                                    }
                                }
                                else
                                {
                                    if (vendorCurrencySymbol == "")
                                    {
                                        ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vSession.User.Id, session);
                                        if (vendorCurrency != null)
                                        {
                                            vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                                        }
                                        else
                                        {
                                            ElioUsers vendor = Sql.GetUserById(vSession.User.Id, session);
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

                                    leads_result = "0/0";
                                    leads_won_perc = "0 %";
                                    leads_amount = vendorCurrencySymbol + " 0";
                                }

                                openLeads = (DrpYears.SelectedValue == "0") ? StatsDB.GetVendorLeadsByStatusAndResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), (int)DealStatus.Open, DealResultStatus.Pending.ToString(), null, session) : StatsDB.GetVendorLeadsByStatusAndResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), (int)DealStatus.Open, DealResultStatus.Pending.ToString(), Convert.ToInt32(DrpYears.SelectedItem.Text), session);
                            }

                            int openDeals = (DrpYears.SelectedValue == "0") ? StatsDB.GetVendorDealsByStatusAndResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), (int)DealStatus.Open, DealResultStatus.Pending.ToString(), null, session) : StatsDB.GetVendorDealsByStatusAndResultByChannelPartner(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(row["partner_user_id"].ToString()), (int)DealStatus.Open, DealResultStatus.Pending.ToString(), Convert.ToInt32(DrpYears.SelectedItem.Text), session);

                            string deals_leads_pending = "";

                            if (vSession.User.Id != GlobalMethods.GetRandstadCustomerID())
                                deals_leads_pending = openLeads.ToString() + " leads / " + openDeals.ToString() + " deals";
                            else
                                deals_leads_pending = openDeals.ToString() + " deals";

                            table.Rows.Add(row["vendor_reseller_id"], row["master_user_id"], row["partner_user_id"], row["company_name"], totalDealsCount, deals_amount, totalLeadsCount, leads_amount,
                                deals_result, deals_won_perc, leads_result, leads_won_perc, deals_leads_pending, deals_average_size, deals_average_sales_cycle);
                        }
                        
                        RdgResellers.Visible = true;
                        UcMessageAlert.Visible = false;

                        RdgResellers.DataSource = table;
                    }
                    else
                    {
                        RdgResellers.Visible = false;
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "There are no statistics for your Partners", MessageTypes.Info, true, true, false, true, false);
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
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

        #endregion

        #region Buttons

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    RdgResellers.Rebind();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}