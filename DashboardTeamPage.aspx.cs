using System;
using System.Collections.Generic;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Controls.Modals;
using WdS.ElioPlus.Lib.Roles.EnumsRoles;
using WdS.ElioPlus.Lib.Roles;

namespace WdS.ElioPlus
{
    public partial class DashboardTeamPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

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

                    if (!IsPostBack)
                    {
                        try
                        {
                            if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
                                if (vSession.LoggedInSubAccountRoleID == 0)
                                    ManageRoles.InsertSystemRolePermissionsByUser(vSession.User.Id, session);
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }
                    }

                    bool hasRight = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTeamPage", Actions.View, session);
                    if (!hasRight)
                    {
                        Response.Redirect(ControlLoader.PageDash405, false);
                        return;
                    }

                    FixPage();
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

        private void LoadUserRoles()
        {
            DrpRoles.Items.Clear();

            List<ElioPermissionsUsersRoles> roles = SqlRoles.GetUserPermissionRoles(vSession.User.Id, "", session);

            ListItem item = new ListItem();

            item.Value = "0";
            item.Text = "Select role";
            DrpRoles.Items.Add(item);

            if (roles.Count > 0)
            {
                foreach (ElioPermissionsUsersRoles role in roles)
                {
                    item = new ListItem();

                    item.Value = role.Id.ToString();
                    item.Text = role.RoleName;

                    DrpRoles.Items.Add(item);
                }
            }

            DrpRoles.SelectedValue = "0";
            DrpRoles.SelectedItem.Text = "Select role";
        }

        private void FixPage()
        {
            if (!IsPostBack)
            {
                UpdateStrings();
                SetLinks();
                LoadUserRoles();

                aAddTeam.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTeamPage", Actions.Invite, session);

                RtbxTeamMemberName.Text = "";

            }

            #region to delete

            //divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

            //if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
            //    LblRenewalHead.Visible = LblRenewal.Visible = true;
            //    LblRenewalHead.Text = "Renewal date: ";

            //    try
            //    {
            //        LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");
            //    }
            //    catch (Exception)
            //    {
            //        LblRenewalHead.Visible = LblRenewal.Visible = false;

            //        Logger.Debug(string.Format("User {0} seems to be premium but he has no order in his account", vSession.User.Id.ToString()));
            //    }
            //}
            //else
            //{
            //    LblRenewalHead.Visible = LblRenewal.Visible = false;
            //}

            //aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingType.Freemium) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
            //aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

            #endregion
        }

        private void SetLinks()
        {
            //aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
        }

        private void UpdateStrings()
        {
        //    if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
        //    {
        //        ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
        //        if (packet != null)
        //        {
        //            LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
        //        }
        //    }
        //    else
        //    {
        //        LblPricingPlan.Text = "You are currently on a free plan";
        //    }

        //    LblElioplusDashboard.Text = "";

        //    LblDashboard.Text = "Dashboard";

        //    aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;

        //    if (aBtnGoPremium.Visible)
        //    {
        //        LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
        //        LblPricingPlan.Visible = false;
        //    }

        //    LblGoFull.Text = "Complete your registration";
        //    LblDashPage.Text = "Team";
        //    LblDashSubTitle.Text = "";
        }

        private void LoadItemData(int id)
        {
            if (id > 0)
            {
                string viewId = Guid.NewGuid().ToString();
                Session[viewId] = id;
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "team-edit") + "?subAccountViewID=" + viewId, false);
            }
        }

        private void BuildRepeaterItem(RepeaterItemEventArgs args)
        {
            if (session.Connection.State == ConnectionState.Closed)
                session.OpenConnection();

            if (vSession.User == null)
            {
                Response.Redirect(ControlLoader.Login, false);
                return;
            }
            RepeaterItem item = (RepeaterItem)args.Item;
            if (item != null)
            {
                DataRowView row = (DataRowView)item.DataItem;
                if (row != null)
                {
                    HtmlGenericControl div1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div1");
                    HtmlGenericControl div2 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div2");
                    HtmlGenericControl div3 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div3");
                    HtmlGenericControl div4 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div4");

                    if (row["id1"].ToString() == "0")
                        div1.Visible = false;
                    if (row["id2"].ToString() == "0")
                        div2.Visible = false;
                    if (row["id3"].ToString() == "0")
                        div3.Visible = false;
                    if (row["id4"].ToString() == "0")
                        div4.Visible = false;

                    if (div1.Visible)
                    {
                        HtmlGenericControl divRibbonOK = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divRibbonOK1");
                        HtmlGenericControl divRibbon = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divRibbon1");
                        HtmlGenericControl spanRibbon = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanRibbon1");
                        Label lblPendingInfo = (Label)ControlFinder.FindControlRecursive(item, "LblPendingInfo1");
                        Image img = (Image)ControlFinder.FindControlRecursive(item, "img1");

                        if (Convert.ToInt32(row["is_confirmed1"].ToString()) == 0)
                        {
                            spanRibbon.Attributes["class"] = "ribbon-inner bg-danger";
                            lblPendingInfo.Text = "Not Confirmed";
                            divRibbon.Visible = true;
                        }

                        if (Convert.ToInt32(row["is_active1"].ToString()) == 0)
                        {
                            spanRibbon.Attributes["class"] = "ribbon-inner bg-warning";
                            lblPendingInfo.Text = "Not Active";
                            divRibbon.Visible = true;
                        }

                        if (Convert.ToInt32(row["is_active1"].ToString()) == 1 && Convert.ToInt32(row["is_confirmed1"].ToString()) == 1)
                        {
                            divRibbonOK.Visible = true;
                            divRibbon.Visible = false;
                        }

                        if (string.IsNullOrEmpty(row["personal_image1"].ToString()))
                        {
                            HtmlGenericControl divImg = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divImg1");
                            HtmlGenericControl divImgNo = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divImgNo1");
                            divImg.Visible = false;
                            divImgNo.Visible = true;
                        }

                        Label lblSubAccountName = (Label)ControlFinder.FindControlRecursive(item, "LblSubAccountName1");
                        if (Convert.ToInt32(row["account_status1"].ToString()) == Convert.ToInt32(AccountStatus.Completed))
                        {
                            if (!string.IsNullOrEmpty(row["first_name1"].ToString()) && !string.IsNullOrEmpty(row["last_name1"].ToString()))
                            {
                                lblSubAccountName.Text = row["last_name1"].ToString() + " " + row["first_name1"].ToString();
                            }
                            else
                                lblSubAccountName.Text = "";
                        }
                        else
                            lblSubAccountName.Text = "";

                        Label lblemail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail1");
                        if (row["email1"].ToString().Length > 20)
                        {
                            HtmlGenericControl divEmailTooltip = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divEmailTooltip1");
                            divEmailTooltip.Attributes["data-toggle"] = "tooltip";
                            divEmailTooltip.Attributes["title"] = row["email1"].ToString();
                            lblemail.Text = row["email1"].ToString().Substring(0, 18) + "...";
                        }
                        else
                            lblemail.Text = row["email1"].ToString();

                        HtmlAnchor aSend = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSend1");
                        aSend.Visible = (Convert.ToInt32(row["is_confirmed1"].ToString()) == 1) ? false : true;

                        HtmlAnchor aSettings = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSettings1");
                        if (aSettings != null)
                        {
                            string viewId1 = Guid.NewGuid().ToString();
                            Session[viewId1] = row["id1"].ToString();
                            aSettings.HRef = ControlLoader.Dashboard(vSession.User, "team-edit") + "?subAccountViewID=" + viewId1;
                        }

                    }
                    if (div2.Visible)
                    {
                        HtmlGenericControl divRibbonOK = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divRibbonOK2");
                        HtmlGenericControl divRibbon = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divRibbon2");
                        HtmlGenericControl spanRibbon = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanRibbon2");
                        Label lblPendingInfo = (Label)ControlFinder.FindControlRecursive(item, "LblPendingInfo2");
                        Image img = (Image)ControlFinder.FindControlRecursive(item, "img2");

                        if (Convert.ToInt32(row["is_confirmed2"].ToString()) == 0)
                        {
                            spanRibbon.Attributes["class"] = "ribbon-inner bg-danger";
                            lblPendingInfo.Text = "Not Confirmed";
                            divRibbon.Visible = true;
                        }

                        if (Convert.ToInt32(row["is_active2"].ToString()) == 0)
                        {
                            spanRibbon.Attributes["class"] = "ribbon-inner bg-warning";
                            lblPendingInfo.Text = "Not Active";
                            divRibbon.Visible = true;
                        }

                        if (Convert.ToInt32(row["is_active2"].ToString()) == 1 && Convert.ToInt32(row["is_confirmed2"].ToString()) == 1)
                        {
                            divRibbonOK.Visible = true;
                            divRibbon.Visible = false;
                        }

                        if (string.IsNullOrEmpty(row["personal_image2"].ToString()))
                        {
                            HtmlGenericControl divImg = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divImg2");
                            HtmlGenericControl divImgNo = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divImgNo2");
                            divImg.Visible = false;
                            divImgNo.Visible = true;
                        }

                        Label lblSubAccountName = (Label)ControlFinder.FindControlRecursive(item, "LblSubAccountName2");
                        if (Convert.ToInt32(row["account_status2"].ToString()) == Convert.ToInt32(AccountStatus.Completed))
                        {
                            if (!string.IsNullOrEmpty(row["first_name2"].ToString()) && !string.IsNullOrEmpty(row["last_name2"].ToString()))
                            {
                                lblSubAccountName.Text = row["last_name2"].ToString() + " " + row["first_name2"].ToString();
                            }
                            else
                                lblSubAccountName.Text = "";
                        }
                        else
                            lblSubAccountName.Text = "";

                        Label lblemail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail2");
                        if (row["email2"].ToString().Length > 20)
                        {
                            HtmlGenericControl divEmailTooltip = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divEmailTooltip2");
                            divEmailTooltip.Attributes["data-toggle"] = "tooltip";
                            divEmailTooltip.Attributes["title"] = row["email2"].ToString();
                            lblemail.Text = row["email2"].ToString().Substring(0, 18) + "...";
                        }
                        else
                            lblemail.Text = row["email2"].ToString();

                        HtmlAnchor aSend = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSend2");
                        aSend.Visible = (Convert.ToInt32(row["is_confirmed2"].ToString()) == 1) ? false : true;

                        HtmlAnchor aSettings = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSettings2");
                        if (aSettings != null)
                        {
                            string viewId2 = Guid.NewGuid().ToString();
                            Session[viewId2] = row["id2"].ToString();
                            aSettings.HRef = ControlLoader.Dashboard(vSession.User, "team-edit") + "?subAccountViewID=" + viewId2;
                        }
                    }
                    if (div3.Visible)
                    {
                        HtmlGenericControl divRibbonOK = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divRibbonOK3");
                        HtmlGenericControl divRibbon = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divRibbon3");
                        HtmlGenericControl spanRibbon = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanRibbon3");
                        Label lblPendingInfo = (Label)ControlFinder.FindControlRecursive(item, "LblPendingInfo3");
                        Image img = (Image)ControlFinder.FindControlRecursive(item, "img3");

                        if (Convert.ToInt32(row["is_confirmed3"].ToString()) == 0)
                        {
                            spanRibbon.Attributes["class"] = "ribbon-inner bg-danger";
                            lblPendingInfo.Text = "Not Confirmed";
                            divRibbon.Visible = true;
                        }
                        
                        if (Convert.ToInt32(row["is_active3"].ToString()) == 0)
                        {
                            spanRibbon.Attributes["class"] = "ribbon-inner bg-warning";
                            lblPendingInfo.Text = "Not Active";
                            divRibbon.Visible = true;
                        }

                        if (Convert.ToInt32(row["is_active3"].ToString()) == 1 && Convert.ToInt32(row["is_confirmed3"].ToString()) == 1)
                        {
                            divRibbonOK.Visible = true;
                            divRibbon.Visible = false;
                        }

                        if (string.IsNullOrEmpty(row["personal_image3"].ToString()))
                        {
                            HtmlGenericControl divImg = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divImg3");
                            HtmlGenericControl divImgNo = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divImgNo3");
                            divImg.Visible = false;
                            divImgNo.Visible = true;
                        }
                        
                        Label lblSubAccountName = (Label)ControlFinder.FindControlRecursive(item, "LblSubAccountName3");
                        if (Convert.ToInt32(row["account_status3"].ToString()) == Convert.ToInt32(AccountStatus.Completed))
                        {
                            if (!string.IsNullOrEmpty(row["first_name3"].ToString()) && !string.IsNullOrEmpty(row["last_name3"].ToString()))
                            {
                                lblSubAccountName.Text = row["last_name3"].ToString() + " " + row["first_name3"].ToString();
                            }
                            else
                                lblSubAccountName.Text = "";
                        }
                        else
                            lblSubAccountName.Text = "";

                        Label lblemail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail3");
                        if (row["email3"].ToString().Length > 20)
                        {
                            HtmlGenericControl divEmailTooltip = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divEmailTooltip3");
                            divEmailTooltip.Attributes["data-toggle"] = "tooltip";
                            divEmailTooltip.Attributes["title"] = row["email3"].ToString();
                            lblemail.Text = row["email3"].ToString().Substring(0, 18) + "...";
                        }
                        else
                            lblemail.Text = row["email3"].ToString();

                        HtmlAnchor aSend = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSend3");
                        aSend.Visible = (Convert.ToInt32(row["is_confirmed3"].ToString()) == 1) ? false : true;

                        HtmlAnchor aSettings = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSettings3");
                        if (aSettings != null)
                        {
                            string viewId3 = Guid.NewGuid().ToString();
                            Session[viewId3] = row["id3"].ToString();
                            aSettings.HRef = ControlLoader.Dashboard(vSession.User, "team-edit") + "?subAccountViewID=" + viewId3;
                        }
                    }
                    if (div4.Visible)
                    {
                        HtmlGenericControl divRibbonOK = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divRibbonOK4");
                        HtmlGenericControl divRibbon = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divRibbon4");
                        HtmlGenericControl spanRibbon = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanRibbon4");
                        Label lblPendingInfo = (Label)ControlFinder.FindControlRecursive(item, "LblPendingInfo4");
                        Image img = (Image)ControlFinder.FindControlRecursive(item, "img4");

                        if (Convert.ToInt32(row["is_confirmed4"].ToString()) == 0)
                        {
                            spanRibbon.Attributes["class"] = "ribbon-inner bg-danger";
                            lblPendingInfo.Text = "Not Confirmed";
                            divRibbon.Visible = true;
                        }

                        if (Convert.ToInt32(row["is_active4"].ToString()) == 0)
                        {
                            spanRibbon.Attributes["class"] = "ribbon-inner bg-warning";
                            lblPendingInfo.Text = "Not Active";
                            divRibbon.Visible = true;
                        }

                        if (Convert.ToInt32(row["is_active4"].ToString()) == 1 && Convert.ToInt32(row["is_confirmed4"].ToString()) == 1)
                        {
                            divRibbonOK.Visible = true;
                            divRibbon.Visible = false;
                        }

                        if (string.IsNullOrEmpty(row["personal_image4"].ToString()))
                        {
                            HtmlGenericControl divImg = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divImg4");
                            HtmlGenericControl divImgNo = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divImgNo4");
                            divImg.Visible = false;
                            divImgNo.Visible = true;
                        }
                        
                        Label lblSubAccountName = (Label)ControlFinder.FindControlRecursive(item, "LblSubAccountName4");
                        if (Convert.ToInt32(row["account_status4"].ToString()) == Convert.ToInt32(AccountStatus.Completed))
                        {
                            if (!string.IsNullOrEmpty(row["first_name4"].ToString()) && !string.IsNullOrEmpty(row["last_name4"].ToString()))
                            {
                                lblSubAccountName.Text = row["last_name4"].ToString() + " " + row["first_name4"].ToString();
                            }
                            else
                                lblSubAccountName.Text = "";
                        }
                        else
                            lblSubAccountName.Text = "";

                        Label lblemail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail4");
                        if (row["email4"].ToString().Length > 20)
                        {
                            HtmlGenericControl divEmailTooltip = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divEmailTooltip4");
                            divEmailTooltip.Attributes["data-toggle"] = "tooltip";
                            divEmailTooltip.Attributes["title"] = row["email4"].ToString();
                            lblemail.Text = row["email4"].ToString().Substring(0, 18) + "...";
                        }
                        else
                            lblemail.Text = row["email4"].ToString();

                        HtmlAnchor aSend = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSend4");
                        aSend.Visible = (Convert.ToInt32(row["is_confirmed4"].ToString()) == 1) ? false : true;

                        HtmlAnchor aSettings = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSettings4");
                        if (aSettings != null)
                        {
                            string viewId4 = Guid.NewGuid().ToString();
                            Session[viewId4] = row["id4"].ToString();
                            aSettings.HRef = ControlLoader.Dashboard(vSession.User, "team-edit") + "?subAccountViewID=" + viewId4;
                        }
                    }
                }
            }
        }

        public void LoadGridData()
        {
            try
            {
                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                if (vSession.User != null)
                {
                    int roleId = 0;
                    if (DrpRoles.SelectedValue != "" && DrpRoles.SelectedValue != "0")
                        roleId = Convert.ToInt32(DrpRoles.SelectedValue);

                    //List<ElioUsersSubAccountsIJRolesIJUsers> subAccounts = Sql.GetUserSubAccountsIJRolesIJUsers(vSession.User.Id, 0, session);
                    List<ElioUsersSubAccountsIJPermissionsRolesIJUsers> subAccounts = SqlRoles.GetUserSubAccountsIJRolesIJUsers(vSession.User.Id, 0, roleId, RtbxTeamMemberName.Text, session);

                    if (subAccounts.Count > 0)
                    {
                        DataTable table = new DataTable();

                        table.Columns.Add("id1");
                        table.Columns.Add("id2");
                        table.Columns.Add("id3");
                        table.Columns.Add("id4");
                        table.Columns.Add("user_id1");
                        table.Columns.Add("user_id2");
                        table.Columns.Add("user_id3");
                        table.Columns.Add("user_id4");
                        table.Columns.Add("email1");
                        table.Columns.Add("email2");
                        table.Columns.Add("email3");
                        table.Columns.Add("email4");
                        table.Columns.Add("personal_image1");
                        table.Columns.Add("personal_image2");
                        table.Columns.Add("personal_image3");
                        table.Columns.Add("personal_image4");
                        table.Columns.Add("team_role_id1");
                        table.Columns.Add("team_role_id2");
                        table.Columns.Add("team_role_id3");
                        table.Columns.Add("team_role_id4");
                        table.Columns.Add("team_role_name1");
                        table.Columns.Add("team_role_name2");
                        table.Columns.Add("team_role_name3");
                        table.Columns.Add("team_role_name4");
                        table.Columns.Add("last_name1");
                        table.Columns.Add("last_name2");
                        table.Columns.Add("last_name3");
                        table.Columns.Add("last_name4");
                        table.Columns.Add("first_name1");
                        table.Columns.Add("first_name2");
                        table.Columns.Add("first_name3");
                        table.Columns.Add("first_name4");
                        table.Columns.Add("position1");
                        table.Columns.Add("position2");
                        table.Columns.Add("position3");
                        table.Columns.Add("position4");
                        table.Columns.Add("guid1");
                        table.Columns.Add("guid2");
                        table.Columns.Add("guid3");
                        table.Columns.Add("guid4");
                        table.Columns.Add("is_confirmed1");
                        table.Columns.Add("is_confirmed2");
                        table.Columns.Add("is_confirmed3");
                        table.Columns.Add("is_confirmed4");
                        table.Columns.Add("is_active1");
                        table.Columns.Add("is_active2");
                        table.Columns.Add("is_active3");
                        table.Columns.Add("is_active4");
                        table.Columns.Add("confirmation_url1");
                        table.Columns.Add("confirmation_url2");
                        table.Columns.Add("confirmation_url3");
                        table.Columns.Add("confirmation_url4");
                        table.Columns.Add("account_status1");
                        table.Columns.Add("account_status2");
                        table.Columns.Add("account_status3");
                        table.Columns.Add("account_status4");

                        //foreach (ElioUsersSubAccountsIJRolesIJUsers subAccount in subAccounts)
                        //{
                        //    table.Rows.Add(subAccount.Id, subAccount.UserId, subAccount.Email, subAccount.PersonalImage, subAccount.TeamRoleId, subAccount.RoleDescription, subAccount.LastName, subAccount.FirstName, subAccount.Position, subAccount.Guid, subAccount.IsConfirmed, subAccount.IsActive, subAccount.ConfirmationUrl, subAccount.AccountStatus, subAccount.CommunityStatus);
                        //}

                        int rows = subAccounts.Count / 4;
                        int columns = subAccounts.Count % 4;
                        int index = 0;

                        for (int i = 0; i < rows; i++)
                        {
                            DataRow row = table.NewRow();
                            row["id1"] = subAccounts[index].Id.ToString();
                            row["id2"] = subAccounts[index + 1].Id.ToString();
                            row["id3"] = subAccounts[index + 2].Id.ToString();
                            row["id4"] = subAccounts[index + 3].Id.ToString();
                            row["user_id1"] = subAccounts[index].UserId.ToString();
                            row["user_id2"] = subAccounts[index + 1].UserId.ToString();
                            row["user_id3"] = subAccounts[index + 2].UserId.ToString();
                            row["user_id4"] = subAccounts[index + 3].UserId.ToString();
                            row["email1"] = subAccounts[index].Email.ToString();
                            row["email2"] = subAccounts[index + 1].Email.ToString();
                            row["email3"] = subAccounts[index + 2].Email.ToString();
                            row["email4"] = subAccounts[index + 3].Email.ToString();
                            row["personal_image1"] = subAccounts[index].PersonalImage.ToString();
                            row["personal_image2"] = subAccounts[index + 1].PersonalImage.ToString();
                            row["personal_image3"] = subAccounts[index + 2].PersonalImage.ToString();
                            row["personal_image4"] = subAccounts[index + 3].PersonalImage.ToString();
                            row["team_role_id1"] = subAccounts[index].TeamRoleId.ToString();
                            row["team_role_id2"] = subAccounts[index + 1].TeamRoleId.ToString();
                            row["team_role_id3"] = subAccounts[index + 2].TeamRoleId.ToString();
                            row["team_role_id4"] = subAccounts[index + 3].TeamRoleId.ToString();
                            row["team_role_name1"] = subAccounts[index].RoleName.ToString();
                            row["team_role_name2"] = subAccounts[index + 1].RoleName.ToString();
                            row["team_role_name3"] = subAccounts[index + 2].RoleName.ToString();
                            row["team_role_name4"] = subAccounts[index + 3].RoleName.ToString();
                            row["last_name1"] = subAccounts[index].LastName.ToString();
                            row["last_name2"] = subAccounts[index + 1].LastName.ToString();
                            row["last_name3"] = subAccounts[index + 2].LastName.ToString();
                            row["last_name4"] = subAccounts[index + 3].LastName.ToString();
                            row["first_name1"] = subAccounts[index].FirstName.ToString();
                            row["first_name2"] = subAccounts[index + 1].FirstName.ToString();
                            row["first_name3"] = subAccounts[index + 2].FirstName.ToString();
                            row["first_name4"] = subAccounts[index + 3].FirstName.ToString();
                            row["position1"] = subAccounts[index].Position.ToString();
                            row["position2"] = subAccounts[index + 1].Position.ToString();
                            row["position3"] = subAccounts[index + 2].Position.ToString();
                            row["position4"] = subAccounts[index + 3].Position.ToString();
                            row["guid1"] = subAccounts[index].Guid.ToString();
                            row["guid2"] = subAccounts[index + 1].Guid.ToString();
                            row["guid3"] = subAccounts[index + 2].Guid.ToString();
                            row["guid4"] = subAccounts[index + 3].Guid.ToString();
                            row["is_confirmed1"] = subAccounts[index].IsConfirmed.ToString();
                            row["is_confirmed2"] = subAccounts[index + 1].IsConfirmed.ToString();
                            row["is_confirmed3"] = subAccounts[index + 2].IsConfirmed.ToString();
                            row["is_confirmed4"] = subAccounts[index + 3].IsConfirmed.ToString();
                            row["is_active1"] = subAccounts[index].IsActive.ToString();
                            row["is_active2"] = subAccounts[index + 1].IsActive.ToString();
                            row["is_active3"] = subAccounts[index + 2].IsActive.ToString();
                            row["is_active4"] = subAccounts[index + 3].IsActive.ToString();
                            row["confirmation_url1"] = subAccounts[index].ConfirmationUrl.ToString();
                            row["confirmation_url2"] = subAccounts[index + 1].ConfirmationUrl.ToString();
                            row["confirmation_url3"] = subAccounts[index + 2].ConfirmationUrl.ToString();
                            row["confirmation_url4"] = subAccounts[index + 3].ConfirmationUrl.ToString();
                            row["account_status1"] = subAccounts[index].AccountStatus.ToString();
                            row["account_status2"] = subAccounts[index + 1].AccountStatus.ToString();
                            row["account_status3"] = subAccounts[index + 2].AccountStatus.ToString();
                            row["account_status4"] = subAccounts[index + 3].AccountStatus.ToString();

                            index = index + 4;

                            table.Rows.Add(row);
                        }

                        if (columns == 1)
                        {
                            DataRow row = table.NewRow();
                            row["id1"] = subAccounts[index].Id.ToString();
                            row["id2"] = "0";
                            row["id3"] = "0";
                            row["id4"] = "0";
                            row["user_id1"] = subAccounts[index].UserId.ToString();
                            row["user_id2"] = "0";
                            row["user_id3"] = "0";
                            row["user_id4"] = "0";
                            row["email1"] = subAccounts[index].Email.ToString();
                            row["email2"] = "-";
                            row["email3"] = "-";
                            row["email4"] = "-";
                            row["personal_image1"] = subAccounts[index].PersonalImage.ToString();
                            row["personal_image2"] = "";
                            row["personal_image3"] = "";
                            row["personal_image4"] = "";
                            row["team_role_id1"] = subAccounts[index].TeamRoleId.ToString();
                            row["team_role_id2"] = "0";
                            row["team_role_id3"] = "0";
                            row["team_role_id4"] = "0";
                            row["team_role_name1"] = subAccounts[index].RoleName.ToString();
                            row["team_role_name2"] = "";
                            row["team_role_name3"] = "";
                            row["team_role_name4"] = "";
                            row["last_name1"] = subAccounts[index].LastName.ToString();
                            row["last_name2"] = "";
                            row["last_name3"] = "";
                            row["last_name4"] = "";
                            row["first_name1"] = subAccounts[index].FirstName.ToString();
                            row["first_name2"] = "";
                            row["first_name3"] = "";
                            row["first_name4"] = "";
                            row["position1"] = subAccounts[index].Position.ToString();
                            row["position2"] = "";
                            row["position3"] = "";
                            row["position4"] = "";
                            row["guid1"] = subAccounts[index].Guid.ToString();
                            row["guid2"] = "";
                            row["guid3"] = "";
                            row["guid4"] = "";
                            row["is_confirmed1"] = subAccounts[index].IsConfirmed.ToString();
                            row["is_confirmed2"] = "0";
                            row["is_confirmed3"] = "0";
                            row["is_confirmed4"] = "0";
                            row["is_active1"] = subAccounts[index].IsActive.ToString();
                            row["is_active2"] = "0";
                            row["is_active3"] = "0";
                            row["is_active4"] = "0";
                            row["confirmation_url1"] = subAccounts[index].ConfirmationUrl.ToString();
                            row["confirmation_url2"] = "";
                            row["confirmation_url3"] = "";
                            row["confirmation_url4"] = "";
                            row["account_status1"] = subAccounts[index].AccountStatus.ToString();
                            row["account_status2"] = "0";
                            row["account_status3"] = "0";
                            row["account_status4"] = "0";

                            table.Rows.Add(row);
                        }
                        else if (columns == 2)
                        {
                            DataRow row = table.NewRow();
                            row["id1"] = subAccounts[index].Id.ToString();
                            row["id2"] = subAccounts[index + 1].Id.ToString();
                            row["id3"] = "0";
                            row["id4"] = "0";
                            row["user_id1"] = subAccounts[index].UserId.ToString();
                            row["user_id2"] = subAccounts[index + 1].UserId.ToString();
                            row["user_id3"] = "0";
                            row["user_id4"] = "0";
                            row["email1"] = subAccounts[index].Email.ToString();
                            row["email2"] = subAccounts[index + 1].Email.ToString();
                            row["email3"] = "-";
                            row["email4"] = "-";
                            row["personal_image1"] = subAccounts[index].PersonalImage.ToString();
                            row["personal_image2"] = subAccounts[index + 1].PersonalImage.ToString();
                            row["personal_image3"] = "";
                            row["personal_image4"] = "";
                            row["team_role_id1"] = subAccounts[index].TeamRoleId.ToString();
                            row["team_role_id2"] = subAccounts[index + 1].TeamRoleId.ToString();
                            row["team_role_id3"] = "0";
                            row["team_role_id4"] = "0";
                            row["team_role_name1"] = subAccounts[index].RoleName.ToString();
                            row["team_role_name2"] = subAccounts[index + 1].RoleName.ToString();
                            row["team_role_name3"] = "";
                            row["team_role_name4"] = "";
                            row["last_name1"] = subAccounts[index].LastName.ToString();
                            row["last_name2"] = subAccounts[index + 1].LastName.ToString();
                            row["last_name3"] = "";
                            row["last_name4"] = "";
                            row["first_name1"] = subAccounts[index].FirstName.ToString();
                            row["first_name2"] = subAccounts[index + 1].FirstName.ToString();
                            row["first_name3"] = "";
                            row["first_name4"] = "";
                            row["position1"] = subAccounts[index].Position.ToString();
                            row["position2"] = subAccounts[index + 1].Position.ToString();
                            row["position3"] = "";
                            row["position4"] = "";
                            row["guid1"] = subAccounts[index].Guid.ToString();
                            row["guid2"] = subAccounts[index + 1].Guid.ToString();
                            row["guid3"] = "";
                            row["guid4"] = "";
                            row["is_confirmed1"] = subAccounts[index].IsConfirmed.ToString();
                            row["is_confirmed2"] = subAccounts[index + 1].IsConfirmed.ToString();
                            row["is_confirmed3"] = "0";
                            row["is_confirmed4"] = "0";
                            row["is_active1"] = subAccounts[index].IsActive.ToString();
                            row["is_active2"] = subAccounts[index + 1].IsActive.ToString();
                            row["is_active3"] = "0";
                            row["is_active4"] = "0";
                            row["confirmation_url1"] = subAccounts[index].ConfirmationUrl.ToString();
                            row["confirmation_url2"] = subAccounts[index + 1].ConfirmationUrl.ToString();
                            row["confirmation_url3"] = "";
                            row["confirmation_url4"] = "";
                            row["account_status1"] = subAccounts[index].AccountStatus.ToString();
                            row["account_status2"] = subAccounts[index + 1].AccountStatus.ToString();
                            row["account_status3"] = "0";
                            row["account_status4"] = "0";

                            table.Rows.Add(row);
                        }
                        else if (columns == 3)
                        {
                            DataRow row = table.NewRow();
                            row["id1"] = subAccounts[index].Id.ToString();
                            row["id2"] = subAccounts[index + 1].Id.ToString();
                            row["id3"] = subAccounts[index + 2].Id.ToString();
                            row["id4"] = "0";
                            row["user_id1"] = subAccounts[index].UserId.ToString();
                            row["user_id2"] = subAccounts[index + 1].UserId.ToString();
                            row["user_id3"] = subAccounts[index + 2].UserId.ToString();
                            row["user_id4"] = "0";
                            row["email1"] = subAccounts[index].Email.ToString();
                            row["email2"] = subAccounts[index + 1].Email.ToString();
                            row["email3"] = subAccounts[index + 2].Email.ToString();
                            row["email4"] = "-";
                            row["personal_image1"] = subAccounts[index].PersonalImage.ToString();
                            row["personal_image2"] = subAccounts[index + 1].PersonalImage.ToString();
                            row["personal_image3"] = subAccounts[index + 2].PersonalImage.ToString();
                            row["personal_image4"] = "";
                            row["team_role_id1"] = subAccounts[index].TeamRoleId.ToString();
                            row["team_role_id2"] = subAccounts[index + 1].TeamRoleId.ToString();
                            row["team_role_id3"] = subAccounts[index + 2].TeamRoleId.ToString();
                            row["team_role_id4"] = "0";
                            row["team_role_name1"] = subAccounts[index].RoleName.ToString();
                            row["team_role_name2"] = subAccounts[index + 1].RoleName.ToString();
                            row["team_role_name3"] = subAccounts[index + 2].RoleName.ToString();
                            row["team_role_name4"] = "";
                            row["last_name1"] = subAccounts[index].LastName.ToString();
                            row["last_name2"] = subAccounts[index + 1].LastName.ToString();
                            row["last_name3"] = subAccounts[index + 2].LastName.ToString();
                            row["last_name4"] = "";
                            row["first_name1"] = subAccounts[index].FirstName.ToString();
                            row["first_name2"] = subAccounts[index + 1].FirstName.ToString();
                            row["first_name3"] = subAccounts[index + 2].FirstName.ToString();
                            row["first_name4"] = "";
                            row["position1"] = subAccounts[index].Position.ToString();
                            row["position2"] = subAccounts[index + 1].Position.ToString();
                            row["position3"] = subAccounts[index + 2].Position.ToString();
                            row["position4"] = "";
                            row["guid1"] = subAccounts[index].Guid.ToString();
                            row["guid2"] = subAccounts[index + 1].Guid.ToString();
                            row["guid3"] = subAccounts[index + 2].Guid.ToString();
                            row["guid4"] = "";
                            row["is_confirmed1"] = subAccounts[index].IsConfirmed.ToString();
                            row["is_confirmed2"] = subAccounts[index + 1].IsConfirmed.ToString();
                            row["is_confirmed3"] = subAccounts[index + 2].IsConfirmed.ToString();
                            row["is_confirmed4"] = "0";
                            row["is_active1"] = subAccounts[index].IsActive.ToString();
                            row["is_active2"] = subAccounts[index + 1].IsActive.ToString();
                            row["is_active3"] = subAccounts[index + 2].IsActive.ToString();
                            row["is_active4"] = "0";
                            row["confirmation_url1"] = subAccounts[index].ConfirmationUrl.ToString();
                            row["confirmation_url2"] = subAccounts[index + 1].ConfirmationUrl.ToString();
                            row["confirmation_url3"] = subAccounts[index + 2].ConfirmationUrl.ToString();
                            row["confirmation_url4"] = "";
                            row["account_status1"] = subAccounts[index].AccountStatus.ToString();
                            row["account_status2"] = subAccounts[index + 1].AccountStatus.ToString();
                            row["account_status3"] = subAccounts[index + 2].AccountStatus.ToString();
                            row["account_status4"] = "0";

                            table.Rows.Add(row);
                        }

                        RdgResults.Visible = true;
                        RdgResults.DataSource = table;
                        RdgResults.DataBind();

                        UcMessageAlertTop.Visible = UcMessageAlert.Visible = false;
                    }
                    else
                    {
                        RdgResults.Visible = false;
                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "You have no team members", MessageTypes.Info, true, true, false, true, false);
                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlertTop, "You have no team members", MessageTypes.Info, true, true, false, true, false);
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

        # endregion

        #region Grids

        protected void RdgResults_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        if (vSession.User != null)
                        {
                            BuildRepeaterItem(args);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgResults_Load(object sender, EventArgs args)
        {
            try
            {
                if (DrpRoles.SelectedItem != null && DrpRoles.SelectedValue != "")
                    LoadGridData();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Buttons

        protected void ImgSubAccountLogo_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");
                            HiddenField hdnCategoryId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId");
                            HiddenField hdnFileTypeId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileTypeId");
                            HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName");

                            if (hdnId != null && hdnCategoryId != null && hdnFileTypeId != null && hdnFileName != null)
                            {
                                LoadItemData(Convert.ToInt32(hdnId.Value));
                            }
                        }
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
        }

        protected void ImgBtnSettings1_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId1");
                        if (hdnId != null)
                        {
                            LoadItemData(Convert.ToInt32(hdnId.Value));
                        }
                    }
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

        protected void ImgBtnSettings2_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId2");
                        if (hdnId != null)
                        {
                            LoadItemData(Convert.ToInt32(hdnId.Value));
                        }
                    }
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

        protected void ImgBtnSettings3_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId3");
                        if (hdnId != null)
                        {
                            LoadItemData(Convert.ToInt32(hdnId.Value));
                        }
                    }
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

        protected void ImgBtnSettings4_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId4");
                        if (hdnId != null)
                        {
                            LoadItemData(Convert.ToInt32(hdnId.Value));
                        }
                    }
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

        protected void ImgBtnSendEmail1_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnConfirmationUrl = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnConfirmationUrl1");
                            Label lblEmail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail1");
                            if (hdnConfirmationUrl != null && lblEmail!=null)
                            {
                                try
                                {
                                    EmailSenderLib.InvitationEmail(lblEmail.Text, vSession.User.CompanyName, hdnConfirmationUrl.Value.ToString(), vSession.Lang, session);
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }

                                LoadGridData();

                                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "4")).Text.Replace("{companyemail}", lblEmail.Text);


                                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Success, true, true, true, true, false);
                                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlertTop, alert, MessageTypes.Success, true, true, true, true, false);
                            }
                        }
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
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, true, true, false);
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlertTop, ex.Message.ToString(), MessageTypes.Error, true, true, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void ImgBtnSendEmail2_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnConfirmationUrl = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnConfirmationUrl2");
                            Label lblEmail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail2");
                            if (hdnConfirmationUrl != null && lblEmail != null)
                            {
                                try
                                {
                                    EmailSenderLib.InvitationEmail(lblEmail.Text, vSession.User.CompanyName, hdnConfirmationUrl.Value.ToString(), vSession.Lang, session);
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }

                                LoadGridData();

                                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "4")).Text.Replace("{companyemail}", lblEmail.Text);


                                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Success, true, true, true, true, false);
                                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlertTop, alert, MessageTypes.Success, true, true, true, true, false);
                            }
                        }
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
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, true, true, false);
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlertTop, ex.Message.ToString(), MessageTypes.Error, true, true, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void ImgBtnSendEmail3_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnConfirmationUrl = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnConfirmationUrl3");
                            Label lblEmail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail3");
                            if (hdnConfirmationUrl != null && lblEmail != null)
                            {
                                try
                                {
                                    EmailSenderLib.InvitationEmail(lblEmail.Text, vSession.User.CompanyName, hdnConfirmationUrl.Value.ToString(), vSession.Lang, session);
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }

                                LoadGridData();

                                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "4")).Text.Replace("{companyemail}", lblEmail.Text);


                                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Success, true, true, true, true, false);
                                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlertTop, alert, MessageTypes.Success, true, true, true, true, false);
                            }
                        }
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
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, true, true, false);
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlertTop, ex.Message.ToString(), MessageTypes.Error, true, true, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void ImgBtnSendEmail4_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnConfirmationUrl = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnConfirmationUrl4");
                            Label lblEmail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail4");
                            if (hdnConfirmationUrl != null && lblEmail != null)
                            {
                                try
                                {
                                    EmailSenderLib.InvitationEmail(lblEmail.Text, vSession.User.CompanyName, hdnConfirmationUrl.Value.ToString(), vSession.Lang, session);
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }

                                LoadGridData();

                                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "4")).Text.Replace("{companyemail}", lblEmail.Text);


                                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Success, true, true, true, true, false);
                                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlertTop, alert, MessageTypes.Success, true, true, true, true, false);
                            }
                        }
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
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, true, true, false);
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlertTop, ex.Message.ToString(), MessageTypes.Error, true, true, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void aAddTeam_ServerClick(object sender, EventArgs e)
        {
            try
            {
                AddToTeamForm control = (AddToTeamForm)ControlFinder.FindControlRecursive(this, "UcAddToTeamForm");
                if (control != null)
                    control.ResetFields(true);

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenInvitationPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //session.OpenConnection();

                //LoadGridData();
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