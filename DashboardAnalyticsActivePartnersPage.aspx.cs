using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.IO;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI.Entities;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.Services.StripeAPI;
using WdS.ElioPlus.Lib.Analytics;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace WdS.ElioPlus
{
    public partial class DashboardAnalyticsActivePartnersPage : System.Web.UI.Page
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
            //LblDashPage.Text = "Active Partners";
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
                        table = (DrpYears.SelectedValue == "0") ? StatsDB.GetVendorActivePartnersStatisticsTable(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, RtbxCompanyNameEmail.Text.Trim(), 0, null, session) : StatsDB.GetVendorActivePartnersStatisticsTable(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, RtbxCompanyNameEmail.Text.Trim(), 0, Convert.ToInt32(DrpYears.SelectedItem.Text), session);
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