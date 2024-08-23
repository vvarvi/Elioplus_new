using System;
using System.Collections.Generic;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Analytics;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Roles;
using WdS.ElioPlus.Lib.Roles.EnumsRoles;
using System.Threading;

namespace WdS.ElioPlus
{
    public partial class DashboardAnalyticsGeneralPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public bool HasDiscount { get; set; }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            try
            {
                //GetChart();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

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

                    bool hasRight = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardAnalyticsGeneralPage", Actions.View, session);
                    if (!hasRight)
                    {
                        Response.Redirect(ControlLoader.PageDash405, false);
                        return;
                    }

                    SetLinks();

                    if (!IsPostBack)
                    {
                        FixPage();

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            LoadTotalStatisticsChartWithCurrencyConvert();
                            LoadStatisticsChartByMonthWithCurrencyConvert();

                            if (vSession.User.Id == GlobalMethods.GetRandstadCustomerID())
                            {
                                LoadRegisteredDealsStatisticsChart();
                                divDealsChart.Attributes["class"] = "col-xl-12";
                                divLeadsChart.Visible = false;
                            }
                            else
                            {
                                LoadRegisteredDealsStatisticsChart();
                                LoadRegisteredLeadsStatisticsChart();

                                divDealsChart.Attributes["class"] = "col-xl-6";
                                divLeadsChart.Visible = true;                                
                            }

                            LoadForecastingStatisticsChart();

                            //BuildChart();
                        }
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

        private void BuildChart()
        {
            if (15 > 0 && 20 > 0)
            {
                decimal? currentVolts = Convert.ToDecimal(15);
                decimal currentMA = Convert.ToDecimal(20);

                ScatterSeriesItem experimentalItem = new ScatterSeriesItem(currentVolts, currentMA);
                //RadHtmlChart scatterChart = HtmlChartHolder.FindControl("ScatterChart") as RadHtmlChart;
                ScatterSeries experimentalSeries = (ScatterSeries)RdChrtForeCasting.PlotArea.Series[1];
                experimentalSeries.SeriesItems.Add(experimentalItem);

                chartData nextData = new chartData();
                nextData.mATheoretical = null;
                nextData.mAExperimental = currentMA;
                nextData.volts = currentVolts.GetValueOrDefault();

                chartDataCollection.Add(nextData);
            }
        }

        #region Methods

        private void LoadYearsList()
        {
            DrpTotalRevenues.Items.Clear();
            DrpRevenuesByMonth.Items.Clear();
            DrpRegisteredDeals.Items.Clear();
            DrpRegisteredLeads.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "1";
            item.Text = DateTime.Now.Year.ToString();
            item.Selected = true;

            DrpTotalRevenues.Items.Add(item);
            DrpRevenuesByMonth.Items.Add(item);
            DrpRegisteredDeals.Items.Add(item);
            DrpRegisteredLeads.Items.Add(item);

            item = new ListItem();
            item.Value = "2";
            item.Text = DateTime.Now.AddYears(-1).Year.ToString();

            DrpTotalRevenues.Items.Add(item);
            DrpRevenuesByMonth.Items.Add(item);
            DrpRegisteredDeals.Items.Add(item);
            DrpRegisteredLeads.Items.Add(item);

            item = new ListItem();
            item.Value = "3";
            item.Text = DateTime.Now.AddYears(-2).Year.ToString();

            DrpTotalRevenues.Items.Add(item);
            DrpRevenuesByMonth.Items.Add(item);
            DrpRegisteredDeals.Items.Add(item);
            DrpRegisteredLeads.Items.Add(item);

            item = new ListItem();
            item.Value = "0";
            item.Text = "All";

            DrpTotalRevenues.Items.Add(item);
            DrpRevenuesByMonth.Items.Add(item);
            DrpRegisteredDeals.Items.Add(item);
            DrpRegisteredLeads.Items.Add(item);

            DrpTotalRevenues.DataBind();
            DrpRevenuesByMonth.DataBind();
            DrpRegisteredDeals.DataBind();
            DrpRegisteredLeads.DataBind();
        }

        private void FixPage()
        {
            UpdateStrings();
            LoadYearsList();

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                LblTotalPartners.Text = StatsDB.GetVendorPartnersCount(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), session).ToString();
                spanTotalPartners.Attributes["style"] = "width:70%";

                int totalDeals = StatsDB.GetVendorTotalDealsCount(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, null, session);
                if (totalDeals > 0)
                {
                    //decimal dealsSize = Convert.ToDecimal(StatsDB.GetVendorDealsAmountSize(vSession.User.Id, session));
                    string averageSizeAmount = "";
                    
                    string dealAmount = GlobalDBMethods.ConvertVendorDealsSumAmountToVendorCurrency(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, null, out averageSizeAmount, session);
                    if (dealAmount != "")
                    {
                        LblTotalDealSize.Text = dealAmount;
                        spanTotalDealSize.Attributes["style"] = "width:80%";
                        spanDealsSizeAverage.Attributes["data-value"] = averageSizeAmount;
                        LblDealsSizeAverage.Text = averageSizeAmount;

                        LblTotalDealRegistered.Text = totalDeals.ToString();
                        spanTotalDealRegistered.Attributes["style"] = "width:40%";

                        string vendorCurrencySymbol = "";

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

                        string dealAmountNoCurrency = dealAmount.Replace(vendorCurrencySymbol, "").Replace(" ", "");
                        string averageSizeAmountNoCurrency = averageSizeAmount.Replace(vendorCurrencySymbol, "").Replace(" ", "");

                        if (dealAmountNoCurrency != "0.00")
                        {
                            int width = Convert.ToInt32((Convert.ToDecimal(averageSizeAmountNoCurrency) * 100) / Convert.ToDecimal(dealAmountNoCurrency));
                            spanDealsSizeAverageProgress.Attributes["style"] = "width:" + width + "%";
                        }
                        else
                            spanDealsSizeAverageProgress.Attributes["style"] = "width:0%";
                    }
                    else
                    {
                        string vendorCurrencySymbol = "";

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

                        LblTotalDealSize.Text = vendorCurrencySymbol + " 0";

                        spanTotalDealSize.Attributes["style"] = "width:0%";

                        LblTotalDealRegistered.Text = "0";
                        spanTotalDealRegistered.Attributes["style"] = "width:0%";

                        spanDealsSizeAverage.Attributes["data-value"] = vendorCurrencySymbol + " 0";
                        LblDealsSizeAverage.Text = vendorCurrencySymbol + " 0";

                        spanDealsSizeAverageProgress.Attributes["style"] = "width:0%";
                    }

                    int dealsWinCount = StatsDB.GetVendorTotalDealsCountByResult(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, DealResultStatus.Won.ToString(), null, session);
                    if (dealsWinCount > 0)
                    {
                        int avgDays = StatsDB.GetVendorAverageDealsSalesCycleDaysByResult(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, DealResultStatus.Won.ToString(), null, session);
                        if (avgDays > 0)
                        {
                            spanDealsSizeAverageWonDays.Attributes["data-value"] = avgDays.ToString() + " days";
                            LblDealsSizeAverageWonDays.Text = avgDays.ToString() + " days";
                            spanDealsSizeAverageWonDaysProgress.Attributes["style"] = "width:" + avgDays + "%";
                        }

                        decimal winRate = (Convert.ToDecimal(dealsWinCount) / Convert.ToDecimal(totalDeals)) * 100;
                        spanDealsWinRate.Attributes["data-value"] = winRate.ToString("0.00") + " %";
                        LblDealsWinRateCount.Text = winRate.ToString("0.00") + " %";
                        spanDealsWinRateProgress.Attributes["style"] = "width:" + winRate + "%";
                    }
                }

                LblLeadsClosedWon.Text = StatsDB.GetVendorTotalLeadsCountByResult(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, DealResultStatus.Won.ToString(), null, session).ToString();
                spanLeadsClosedWon.Attributes["style"] = "width:60%";

                int totalLeads = StatsDB.GetVendorTotalLeadsCount(vSession.User.Id, null, session);
                if (totalLeads > 0)
                {
                    decimal winRate = (StatsDB.GetVendorTotalLeadsCountByResult(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, DealResultStatus.Won.ToString(), null, session) * 100) / totalLeads;

                    LblLeadWinRate.Text = winRate.ToString() + " %";
                    spanLeadWinRate.Attributes["style"] = "width:" + winRate + "%";
                }
            }

            UcSendMessageAlertConfirmed.Visible = false;
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
            aViewStatisticsByPartners.HRef = ControlLoader.Dashboard(vSession.User, "sales-leaderboard-analytics");
            aChannelPartnerViewAll.HRef = ControlLoader.Dashboard(vSession.User, "active-partners-analytics");
        }

        private bool IsValidData()
        {
            bool isValid = true;

            return isValid;
        }

        private void LoadTotalStatisticsChartWithCurrencyConvert()
        {
            string vendorCurrencySymbol = "";
            DataTable tbl = (DrpTotalRevenues.SelectedValue == "0") ? StatsDB.GetVendorDealsSumAmountToVendorCurrencyByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, DealResultStatus.Won.ToString(), null, out vendorCurrencySymbol, session) : StatsDB.GetVendorDealsSumAmountToVendorCurrencyByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, DealResultStatus.Won.ToString(), Convert.ToInt32(DrpTotalRevenues.SelectedItem.Text), out vendorCurrencySymbol, session);
            
            ChrtTotalStatistics.DataSource = tbl;
            
            DataTable table = new DataTable();

            DataColumn colString = new DataColumn("amount");
            colString.DataType = System.Type.GetType("System.String");
            table.Columns.Add(colString);

            DataColumn colInt32 = new DataColumn("month");
            colInt32.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(colInt32);

            if (tbl.Rows.Count > 0)
            {
                table.Rows.Add("0", 1);
                table.Rows.Add("0", 2);
                table.Rows.Add("0", 3);
                table.Rows.Add("0", 4);
                table.Rows.Add("0", 5);
                table.Rows.Add("0", 6);
                table.Rows.Add("0", 7);
                table.Rows.Add("0", 8);
                table.Rows.Add("0", 9);
                table.Rows.Add("0", 10);
                table.Rows.Add("0", 11);
                table.Rows.Add("0", 12);

                foreach (DataRow dealAmount in tbl.Rows)
                {
                    //table.Rows[Convert.ToInt32(dealAmount["month"]) - 1]["amount"] = Convert.ToDecimal(dealAmount["amount"]).ToString();
                    table.Rows[Convert.ToInt32(dealAmount["month"]) - 1]["amount"] = Convert.ToDecimal(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", dealAmount["amount"]));
                }

                for (int i = 0; i < 12; i++)
                {
                    if (i > 0)
                        table.Rows[i]["amount"] = (Convert.ToDecimal(table.Rows[i - 1]["amount"]) + Convert.ToDecimal(table.Rows[i]["amount"])).ToString();
                    else
                        table.Rows[i]["amount"] = table.Rows[i]["amount"].ToString();
                }
                //if (vendorCurrencySymbol != "")
                //{
                //    for (int i = 0; i < 12; i++)
                //    {
                //        table.Rows[i]["amount"] = (vendorCurrencySymbol + " " + table.Rows[i]["amount"].ToString());
                //    }
                //}

                ChrtTotalStatistics.ChartTitle.Text = string.Format("Total Revenues (currency is {0})", vendorCurrencySymbol != "" ? vendorCurrencySymbol : "$");
            }

            ChrtTotalStatistics.DataSource = table;
            ChrtTotalStatistics.DataBind();
        }

        private void LoadStatisticsChartByMonthWithCurrencyConvert()
        {
            string vendorCurrencySymbol = "";
            DataTable tbl = (DrpRevenuesByMonth.SelectedValue == "0") ? StatsDB.GetVendorDealsSumAmountToVendorCurrencyByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, DealResultStatus.Won.ToString(), null, out vendorCurrencySymbol, session) : StatsDB.GetVendorDealsSumAmountToVendorCurrencyByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, DealResultStatus.Won.ToString(), Convert.ToInt32(DrpRevenuesByMonth.SelectedItem.Text), out vendorCurrencySymbol, session);
            
            ChrtStatisticsByMonth.DataSource = tbl;
            
            DataTable table = new DataTable();

            DataColumn colString = new DataColumn("amount");
            colString.DataType = System.Type.GetType("System.String");
            table.Columns.Add(colString);

            DataColumn colInt32 = new DataColumn("month");
            colInt32.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(colInt32);

            if (tbl.Rows.Count > 0)
            {
                table.Rows.Add("0", 1);
                table.Rows.Add("0", 2);
                table.Rows.Add("0", 3);
                table.Rows.Add("0", 4);
                table.Rows.Add("0", 5);
                table.Rows.Add("0", 6);
                table.Rows.Add("0", 7);
                table.Rows.Add("0", 8);
                table.Rows.Add("0", 9);
                table.Rows.Add("0", 10);
                table.Rows.Add("0", 11);
                table.Rows.Add("0", 12);

                foreach (DataRow dealAmount in tbl.Rows)
                {
                    //table.Rows[Convert.ToInt32(dealAmount["month"]) - 1]["amount"] = (Convert.ToDecimal(table.Rows[Convert.ToInt32(dealAmount["month"]) - 1]["amount"]) + Convert.ToDecimal(dealAmount["amount"])).ToString();
                    table.Rows[Convert.ToInt32(dealAmount["month"]) - 1]["amount"] = string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", (Convert.ToDecimal(table.Rows[Convert.ToInt32(dealAmount["month"]) - 1]["amount"]) + Convert.ToDecimal(dealAmount["amount"])));
                }

                ChrtStatisticsByMonth.ChartTitle.Text = string.Format("Revenues by month (currency is {0})", vendorCurrencySymbol != "" ? vendorCurrencySymbol : "$");
            }

            ChrtStatisticsByMonth.DataSource = table;
            ChrtStatisticsByMonth.DataBind();
        }

        private void LoadRegisteredDealsStatisticsChart()
        {
            DataTable tbl = (DrpRegisteredDeals.SelectedValue == "0") ? StatsDB.GetVendorDealsCountByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealActivityStatus.Approved, null, session) : StatsDB.GetVendorDealsCountByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealActivityStatus.Approved, Convert.ToInt32(DrpRegisteredDeals.SelectedItem.Text), session);

            DataTable tbl2 = (DrpRegisteredDeals.SelectedValue == "0") ? StatsDB.GetVendorDealsCountByStatusByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Closed, (int)DealActivityStatus.Approved, null, session) : StatsDB.GetVendorDealsCountByStatusByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Closed, (int)DealActivityStatus.Approved, Convert.ToInt32(DrpRegisteredDeals.SelectedItem.Text), session);

            DataTable table = new DataTable();
            table.Columns.Add("count");
            table.Columns.Add("count_by_status");
            table.Columns.Add("month");

            for (int i = 0; i < 12; i++)
            {
                table.Rows.Add("0", "0", i + 1);

                foreach (DataRow deals_all in tbl.Rows)
                {
                    if (deals_all["month"].ToString() == (i + 1).ToString())
                    {
                        table.Rows[i]["count"] = deals_all["count"];
                        break;
                    }
                }

                foreach (DataRow deals_closed in tbl2.Rows)
                {
                    if (deals_closed["month"].ToString() == (i + 1).ToString())
                    {
                        table.Rows[i]["count_by_status"] = deals_closed["count_by_status"];
                        break;
                    }
                }
            }

            ChrtRegisteredDeals.DataSource = table;
            ChrtRegisteredDeals.DataBind();
        }

        private void LoadRegisteredLeadsStatisticsChart()
        {
            DataTable tbl = (DrpRegisteredLeads.SelectedValue == "0") ? StatsDB.GetVendorLeadsCountByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, null, session) : StatsDB.GetVendorLeadsCountByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, Convert.ToInt32(DrpRegisteredLeads.SelectedItem.Text), session);

            DataTable tbl2 = (DrpRegisteredLeads.SelectedValue == "0") ? StatsDB.GetVendorLeadsCountByStatusByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Closed, null, session) : StatsDB.GetVendorLeadsCountByStatusByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Closed, Convert.ToInt32(DrpRegisteredLeads.SelectedItem.Text), session);

            DataTable table = new DataTable();
            table.Columns.Add("count");
            table.Columns.Add("count_by_status");
            table.Columns.Add("month");

            for (int i = 0; i < 12; i++)
            {
                table.Rows.Add("0", "0", i + 1);

                foreach (DataRow deals_all in tbl.Rows)
                {
                    if (deals_all["month"].ToString() == (i + 1).ToString())
                    {
                        table.Rows[i]["count"] = deals_all["count"];
                        break;
                    }
                }

                foreach (DataRow deals_closed in tbl2.Rows)
                {
                    if (deals_closed["month"].ToString() == (i + 1).ToString())
                    {
                        table.Rows[i]["count_by_status"] = deals_closed["count_by_status"];
                        break;
                    }
                }
            }

            ChrtRegisteredLeads.DataSource = table;
            ChrtRegisteredLeads.DataBind();
        }

        private void LoadForecastingStatisticsChart()
        {
            string vendorCurrencySymbol = "";

            DataTable tbl = StatsDB.GetVendorForecastingDealsAmountToVendorCurrencyByMonth(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Open, DealResultStatus.Pending.ToString(), (int)DealActivityStatus.Approved, null, out vendorCurrencySymbol, session);

            if (tbl != null && tbl.Rows.Count > 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    string month = "";

                    if (i == 0)
                        month = "Jan";
                    else if (i == 1)
                        month = "Feb";
                    else if (i == 2)
                        month = "Mar";
                    else if (i == 3)
                        month = "Apr";
                    else if (i == 4)
                        month = "May";
                    else if (i == 5)
                        month = "Jun";
                    else if (i == 6)
                        month = "Jul";
                    else if (i == 7)
                        month = "Aug";
                    else if (i == 8)
                        month = "Sep";
                    else if (i == 9)
                        month = "Oct";
                    else if (i == 10)
                        month = "Nov";
                    else
                        month = "Dec";

                    tbl.Rows[i]["amount"] = month + " - (" + vendorCurrencySymbol + " " + Convert.ToDecimal(tbl.Rows[i]["amount"]).ToString("0.00") + ")";
                }
            }
            else
            {
                ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vSession.User.Country, session);
                if (countryCurrency != null)
                {
                    vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                    tbl.Rows[0]["amount"] = "Jan" + " - (" + vendorCurrencySymbol + " 0)";
                    tbl.Rows[1]["amount"] = "Feb" + " - (" + vendorCurrencySymbol + " 0)";
                    tbl.Rows[2]["amount"] = "Mar" + " - (" + vendorCurrencySymbol + " 0)";
                    tbl.Rows[3]["amount"] = "Apr" + " - (" + vendorCurrencySymbol + " 0)";
                    tbl.Rows[4]["amount"] = "May" + " - (" + vendorCurrencySymbol + " 0)";
                    tbl.Rows[5]["amount"] = "Jun" + " - (" + vendorCurrencySymbol + " 0)";
                    tbl.Rows[6]["amount"] = "Jul" + " - (" + vendorCurrencySymbol + " 0)";
                    tbl.Rows[7]["amount"] = "Aug" + " - (" + vendorCurrencySymbol + " 0)";
                    tbl.Rows[8]["amount"] = "Sep" + " - (" + vendorCurrencySymbol + " 0)";
                    tbl.Rows[9]["amount"] = "Oct" + " - (" + vendorCurrencySymbol + " 0)";
                    tbl.Rows[10]["amount"] = "Nov" + " - (" + vendorCurrencySymbol + " 0)";
                    tbl.Rows[11]["amount"] = "Dec" + " - (" + vendorCurrencySymbol + " 0)";
                }
            }

            ChrtForecasting.DataSource = tbl;
            ChrtForecasting.DataBind();
        }

        private void GetChart()
        {
            //RadHtmlChart chrt = new RadHtmlChart();

            //chrt.ID = "ScatterChart";
            //chrt.Width = Unit.Percentage(100);
            //chrt.Height = Unit.Pixel(400);

            RdChrtForeCasting.Legend.Appearance.Position = Telerik.Web.UI.HtmlChart.ChartLegendPosition.Bottom;

            RdChrtForeCasting.PlotArea.XAxis.TitleAppearance.Text = "Months";
            RdChrtForeCasting.PlotArea.YAxis.TitleAppearance.Text = "Expected Closed Deals Count";

            ScatterLineSeries theoreticalData = new ScatterLineSeries();
            theoreticalData.Name = "Theoretical Data";
            theoreticalData.LabelsAppearance.Visible = false;
            theoreticalData.TooltipsAppearance.Color = System.Drawing.Color.White;
            theoreticalData.TooltipsAppearance.DataFormatString = "{0} Volts, {1} mA";

            ScatterSeries experimentalData = new ScatterSeries();
            experimentalData.Name = "Experimental Data";
            experimentalData.LabelsAppearance.Visible = false;
            experimentalData.TooltipsAppearance.DataFormatString = "{0} Volts, {1} mA";
            experimentalData.TooltipsAppearance.Color = System.Drawing.Color.White;

            foreach (DataRow row in GetChartData().Rows)
            {
                decimal? currentVolts = (decimal?)row["Volts"];
                if (!(row["mATheoretical"] is DBNull))
                {
                    decimal? currentMATheoretical = (decimal?)row["mATheoretical"];
                    ScatterSeriesItem theoreticalItem = new ScatterSeriesItem(currentVolts, currentMATheoretical);
                    theoreticalData.SeriesItems.Add(theoreticalItem);
                }
                decimal? currentMAExperimental = (decimal?)row["mAExperimental"];
                ScatterSeriesItem experimentalItem = new ScatterSeriesItem(currentVolts, currentMAExperimental);
                experimentalData.SeriesItems.Add(experimentalItem);
            }

            RdChrtForeCasting.PlotArea.Series.Add(theoreticalData);
            RdChrtForeCasting.PlotArea.Series.Add(experimentalData);
        }

        private DataTable GetChartData()
        {
            DataTable chartDataTable = new DataTable();

            chartDataTable.Columns.Add(new DataColumn("Volts", typeof(System.Decimal)));
            chartDataTable.Columns.Add(new DataColumn("mATheoretical", typeof(System.Decimal)));
            chartDataTable.Columns.Add(new DataColumn("mAExperimental", typeof(System.Decimal)));

            foreach (chartData chartDataItem in chartDataCollection)
            {
                chartDataTable.Rows.Add(new object[] { chartDataItem.volts, chartDataItem.mATheoretical, chartDataItem.mAExperimental });
            }

            return chartDataTable;
        }

        List<chartData> chartDataCollection
        {
            get
            {
                if (Object.Equals((List<chartData>)ViewState["chartData"], null))
                {
                    List<chartData> fixedData = new List<chartData>();
                    fixedData.Add(AddChartDataItem(0m, 0m, 0m));
                    fixedData.Add(AddChartDataItem(0.001m, 0m, 0.24m));
                    fixedData.Add(AddChartDataItem(0.05m, 0m, 0.43m));
                    fixedData.Add(AddChartDataItem(0.2m, 0.1m, 0.49m));
                    fixedData.Add(AddChartDataItem(0.5m, 0.3m, 0.53m));
                    fixedData.Add(AddChartDataItem(1m, 0.6m, 0.57m));
                    fixedData.Add(AddChartDataItem(2m, 1m, 0.6m));
                    fixedData.Add(AddChartDataItem(5m, 2.5m, 0.65m));
                    fixedData.Add(AddChartDataItem(7m, 8m, 0.67m));
                    fixedData.Add(AddChartDataItem(9m, 12m, 0.68m));
                    fixedData.Add(AddChartDataItem(13m, 16m, 0.69m));
                    ViewState["chartData"] = fixedData;
                }
                return (List<chartData>)ViewState["chartData"];
            }
            set
            {
                ViewState["chartData"] = value;
            }
        }

        public chartData AddChartDataItem(decimal? theoreticalValue, decimal experimentalValue, decimal voltsValue)
        {
            chartData item = new chartData();
            item.mAExperimental = experimentalValue;
            item.mATheoretical = theoreticalValue;
            item.volts = voltsValue;
            return item;
        }

        public struct chartData
        {
            private decimal? _mATheoretical;
            private decimal _mAExperimental;
            private decimal _volts;

            public decimal volts
            {
                get
                {
                    return this._volts;
                }
                set
                {
                    this._volts = value;
                }
            }

            public decimal mAExperimental
            {
                get
                {
                    return this._mAExperimental;
                }
                set
                {
                    this._mAExperimental = value;
                }
            }

            public decimal? mATheoretical
            {
                get
                {
                    return this._mATheoretical;
                }
                set
                {
                    this._mATheoretical = value;
                }
            }
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

                    int partnerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(item["partner_user_id"].Text) : Convert.ToInt32(item["master_user_id"].Text);
                    ElioUsers company = Sql.GetUserById(partnerId, session);
                    if (company != null)
                    {
                        Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                        Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                        HtmlAnchor aCollaborationRoom = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCollaborationRoomConfirmed");

                        lblCompanyName.Text = item["company_name"].Text;

                        aCollaborationRoom.HRef = ControlLoader.Dashboard(company, "collaboration-chat-room");

                        HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                        divNotification.Visible = false;

                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");

                        if (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus))
                        {
                            aCompanyLogo.HRef = ControlLoader.Profile(company);

                            imgCompanyLogo.ToolTip = "View company's profile";
                            imgCompanyLogo.ImageUrl = company.CompanyLogo;
                            imgCompanyLogo.AlternateText = "Company logo";
                        }
                        else if (company.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty))
                        {
                            aCompanyLogo.HRef = company.WebSite;

                            aCompanyLogo.Target = "_blank";
                            imgCompanyLogo.ToolTip = "View company's site";
                            imgCompanyLogo.ImageUrl = "/images/icons/partners_th_party_2.png";
                            imgCompanyLogo.AlternateText = "Third party partners logo";
                        }
                        else if (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Collaboration))
                        {
                            if (company.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                            {
                                aCompanyLogo.HRef = company.WebSite;

                                aCompanyLogo.Target = "_blank";
                                imgCompanyLogo.ToolTip = "View company's site";
                            }

                            imgCompanyLogo.ImageUrl = company.CompanyLogo;
                            imgCompanyLogo.AlternateText = "Potential collaboration company's logo";
                        }
                    }

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
                    DataTable table = null;
                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        table = StatsDB.GetVendorActivePartnersStatisticsTable(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, RtbxCompanyNameEmail.Text.Trim(), 10, null, session);
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        RdgResellers.Visible = true;
                        UcSendMessageAlertConfirmed.Visible = false;

                        RdgResellers.DataSource = table;
                    }
                    else
                    {
                        RdgResellers.Visible = false;

                        GlobalMethods.ShowMessageControlDA(UcSendMessageAlertConfirmed, (false) ? "There are no Confirmed Invitations send to your Partners with these criteria" : "There are no Confirmed Invitations send to your Partners", MessageTypes.Info, true, true, false, true, false);
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

        protected void RptPartnerCountries_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        if (row != null)
                        {
                            //Image imgCountryLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCountryLogo");

                            //if (File.Exists(Server.MapPath("~/images/countries-flags/" + row["country_name"].ToString() + ".jpg")))
                            //    imgCountryLogo.ImageUrl = "~/images/countries-flags/" + row["country_name"].ToString() + ".jpg";
                            //else if (File.Exists(Server.MapPath("~/images/countries-flags/" + row["country_name"].ToString() + ".png")))
                            //    imgCountryLogo.ImageUrl = "~/images/countries-flags/" + row["country_name"].ToString() + ".png";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RptPartnerCountries_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable table = SqlCollaboration.GetCollaborationPartnersByCountriesTbl(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, session);

                    if (table != null && table.Rows.Count > 0)
                    {
                        RptPartnerCountries.Visible = true;
                        RptPartnerCountries.DataSource = table;
                        RptPartnerCountries.DataBind();
                    }
                    else
                    {
                        RptPartnerCountries.Visible = false;
                        GlobalMethods.ShowMessageControlDA(UcPartnerCountriesMessageAlert, "There are no partners yet", MessageTypes.Info, true, true, false, true, false);
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
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

        protected void RptTiers_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        if (row != null)
                        {
                            //if (Convert.ToInt32(row["total_partners"].ToString()) > 0)
                            //{
                            HtmlGenericControl div0 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div0");
                            div0.Visible = true;
                            Label lblTotalPartnersCount1 = (Label)ControlFinder.FindControlRecursive(item, "LblTotalPartnersCount1");
                            lblTotalPartnersCount1.Text = row["total_partners"].ToString();
                            //}

                            HtmlGenericControl div1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div1");
                            HtmlGenericControl div2 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div2");
                            HtmlGenericControl div3 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div3");

                            if (row["description1"].ToString() != "")
                            {
                                div1.Visible = true;
                                Label lblTotalPartnersCount2 = (Label)ControlFinder.FindControlRecursive(item, "LblTotalPartnersCount2");
                                lblTotalPartnersCount2.Text = row["count1"].ToString();

                                Label lblTotalPartnersText2 = (Label)ControlFinder.FindControlRecursive(item, "LblTotalPartnersText2");
                                lblTotalPartnersText2.Text = row["description1"].ToString() + " Partners";
                            }
                            else
                                div1.Visible = false;

                            if (row["description2"].ToString() != "")
                            {
                                div2.Visible = true;
                                Label lblTotalPartnersCount3 = (Label)ControlFinder.FindControlRecursive(item, "LblTotalPartnersCount3");
                                lblTotalPartnersCount3.Text = row["count2"].ToString();

                                Label lblTotalPartnersText3 = (Label)ControlFinder.FindControlRecursive(item, "LblTotalPartnersText3");
                                lblTotalPartnersText3.Text = row["description2"].ToString() + " Partners";
                            }
                            else
                                div2.Visible = false;

                            if (row["description3"].ToString() != "")
                            {
                                div3.Visible = true;
                                Label lblTotalPartnersCount4 = (Label)ControlFinder.FindControlRecursive(item, "LblTotalPartnersCount4");
                                lblTotalPartnersCount4.Text = row["count3"].ToString();

                                Label lblTotalPartnersText4 = (Label)ControlFinder.FindControlRecursive(item, "LblTotalPartnersText4");
                                lblTotalPartnersText4.Text = row["description3"].ToString() + " Partners";
                            }
                            else
                                div3.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RptTiers_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable tiers = new DataTable();

                    if (vSession.User.Id == 41078)
                    {
                        tiers = Sql.GetTierManagementCustomSettingsTbl(vSession.User.Id, session);
                    }
                    else
                    {
                        tiers = Sql.GetTierManagementUserSettingsTbl(vSession.User.Id, session);
                    }

                    DataTable table = new DataTable();

                    table.Columns.Add("total_partners");
                    table.Columns.Add("description1");
                    table.Columns.Add("description2");
                    table.Columns.Add("description3");
                    table.Columns.Add("count1");
                    table.Columns.Add("count2");
                    table.Columns.Add("count3");

                    string tierDescription1 = "";
                    string tierDescription2 = "";
                    string tierDescription3 = "";
                    int tier1Count = 0;
                    int tier2Count = 0;
                    int tier3Count = 0;

                    if (tiers.Rows.Count > 0)
                    {
                        int totalPartners = SqlCollaboration.GetTotalPartnersAndTiersCount(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), "", session);

                        for (int i = 0; i < tiers.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                tierDescription1 = tiers.Rows[i]["description"].ToString();
                                if (tierDescription1 != "")
                                {
                                    tier1Count = SqlCollaboration.GetTotalPartnersAndTiersCount(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), tierDescription1, session);
                                }
                            }
                            else if (i == 1)
                            {
                                tierDescription2 = tiers.Rows[i]["description"].ToString();
                                if (tierDescription2 != "")
                                {
                                    tier2Count = SqlCollaboration.GetTotalPartnersAndTiersCount(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), tierDescription2, session);
                                }
                            }
                            else if (i == 2)
                            {
                                tierDescription3 = tiers.Rows[i]["description"].ToString();
                                if (tierDescription3 != "")
                                {
                                    tier3Count = SqlCollaboration.GetTotalPartnersAndTiersCount(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), tierDescription3, session);
                                }
                            }
                            else
                                break;  //TO DO SOS
                        }

                        table.Rows.Add(totalPartners, tierDescription1, tierDescription2, tierDescription3, tier1Count, tier2Count, tier3Count);
                    }
                    else
                        table.Rows.Add("0", "", "", "", "0", "0", "0");

                    //if (tiers.Rows.Count > 0)
                    //{
                    //    if (table.Rows.Count <= 3)
                    //    {
                    //        int totalPartners = SqlCollaboration.GetTotalPartnersAndTiersCount(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), "", session);

                    //        string tierDescription1 = tiers.Rows[0]["description"].ToString();
                    //        if (tierDescription1 != "")
                    //        {
                    //            tier1Count = SqlCollaboration.GetTotalPartnersAndTiersCount(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), tierDescription1, session);
                    //        }

                    //        string tierDescription2 = tiers.Rows[1]["description"].ToString();
                    //        if (tierDescription2 != "")
                    //        {
                    //            tier2Count = SqlCollaboration.GetTotalPartnersAndTiersCount(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), tierDescription2, session);
                    //        }
                    //        string tierDescription3 = tiers.Rows[2]["description"].ToString();
                    //        if (tierDescription3 != "")
                    //        {
                    //            tier3Count = SqlCollaboration.GetTotalPartnersAndTiersCount(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), tierDescription3, session);
                    //        }

                    //        table.Rows.Add(totalPartners, tierDescription1, tierDescription2, tierDescription3, tier1Count, tier2Count, tier3Count);
                    //    }
                    //}
                    //else
                    //{
                    //    table.Rows.Add("0", "-", "-", "-", "0", "0", "0");
                    //}

                    RptTiers.Visible = true;
                    RptTiers.DataSource = table;
                    RptTiers.DataBind();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
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

        #region DropDownLists

        protected void DrpTotalRevenues_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                LoadTotalStatisticsChartWithCurrencyConvert();
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

        protected void DrpRevenuesByMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                LoadStatisticsChartByMonthWithCurrencyConvert();
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

        protected void DrpRegisteredDeals_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                LoadRegisteredDealsStatisticsChart();
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

        protected void DrpRegisteredLeads_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                LoadRegisteredLeadsStatisticsChart();
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
    }
}