using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Roles;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Dashboard.Components.Grid
{
    public partial class TeamsGridControl : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        FixPage();

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

        #region methods

        private void FixPage()
        {
            if (!IsPostBack)
            {
                UcMessageAlert.Visible = false;
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
                    HtmlGenericControl div = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div");
                    
                    if (row["id"].ToString() == "0")
                        div.Visible = false;

                    if (div.Visible)
                    {
                        HtmlGenericControl divRibbonOK = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divRibbonOK");
                        HtmlGenericControl divRibbon = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divRibbon");
                        HtmlGenericControl spanRibbon = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanRibbon");
                        Label lblPendingInfo = (Label)ControlFinder.FindControlRecursive(item, "LblPendingInfo");
                        Image img = (Image)ControlFinder.FindControlRecursive(item, "img");

                        if (Convert.ToInt32(row["is_confirmed"].ToString()) == 0)
                        {
                            HtmlGenericControl liResendInvitation = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "liResendInvitation");

                            spanRibbon.Attributes["class"] = "ribbon-inner bg-danger";
                            lblPendingInfo.Text = "Not Confirmed";
                            divRibbon.Visible = true;
                            liResendInvitation.Visible = true;
                        }

                        if (Convert.ToInt32(row["is_active"].ToString()) == 0)
                        {
                            spanRibbon.Attributes["class"] = "ribbon-inner bg-warning";
                            lblPendingInfo.Text = "Not Active";
                            divRibbon.Visible = true;
                        }

                        if (Convert.ToInt32(row["is_active"].ToString()) == 1 && Convert.ToInt32(row["is_confirmed"].ToString()) == 1)
                        {
                            divRibbonOK.Visible = true;
                            divRibbon.Visible = false;
                        }

                        if (string.IsNullOrEmpty(row["personal_image"].ToString()))
                        {
                            HtmlGenericControl divImg = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divImg");
                            HtmlGenericControl divImgNo = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divImgNo");
                            divImg.Visible = false;
                            divImgNo.Visible = true;
                        }

                        Label lblSubAccountName = (Label)ControlFinder.FindControlRecursive(item, "LblSubAccountName");
                        if (Convert.ToInt32(row["account_status"].ToString()) == Convert.ToInt32(AccountStatus.Completed))
                        {
                            if (!string.IsNullOrEmpty(row["first_name"].ToString()) && !string.IsNullOrEmpty(row["last_name"].ToString()))
                            {
                                lblSubAccountName.Text = row["last_name"].ToString() + " " + row["first_name"].ToString();
                            }
                            else
                                lblSubAccountName.Text = "";
                        }
                        else
                            lblSubAccountName.Text = "";

                        HtmlAnchor aSettings = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSettings");
                        if (aSettings != null)
                        {
                            string viewId = Guid.NewGuid().ToString();
                            Session[viewId] = row["id"].ToString();
                            aSettings.HRef = ControlLoader.Dashboard(vSession.User, "team-edit") + "?subAccountViewID=" + viewId;
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
                    //List<ElioUsersSubAccountsIJRolesIJUsers> subAccounts = Sql.GetUserSubAccountsIJRolesIJUsers(vSession.User.Id, 5, session);
                    List<ElioUsersSubAccountsIJPermissionsRolesIJUsers> subAccounts = SqlRoles.GetUserSubAccountsIJRolesIJUsers(vSession.User.Id, 5, 0, "", session);

                    if (subAccounts.Count > 0)
                    {
                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("user_id");
                        table.Columns.Add("email");
                        table.Columns.Add("personal_image");
                        table.Columns.Add("team_role_id");
                        table.Columns.Add("team_role_name");
                        table.Columns.Add("last_name");
                        table.Columns.Add("first_name");
                        table.Columns.Add("position");
                        table.Columns.Add("guid");
                        table.Columns.Add("is_confirmed");
                        table.Columns.Add("is_active");
                        table.Columns.Add("confirmation_url");
                        table.Columns.Add("account_status");

                        foreach (ElioUsersSubAccountsIJPermissionsRolesIJUsers subAccount in subAccounts)
                        {
                            table.Rows.Add(subAccount.Id, subAccount.UserId, subAccount.Email, subAccount.PersonalImage,
                                subAccount.TeamRoleId, subAccount.RoleName, subAccount.LastName, subAccount.FirstName,
                                subAccount.Position, subAccount.Guid, subAccount.IsConfirmed, subAccount.IsActive, subAccount.ConfirmationUrl, subAccount.AccountStatus);
                        }

                        UcMessageAlert.Visible = false;
                        RdgResults.Visible = true;
                        RdgResults.DataSource = table;
                        RdgResults.DataBind();
                    }
                    else
                    {
                        RdgResults.Visible = false;
                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "You have no team members", MessageTypes.Info, true, false, true, true, false);
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

        private void LoadItemData(int id)
        {
            if (id > 0)
            {
                string viewId = Guid.NewGuid().ToString();
                Session[viewId] = id;
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "team-edit") + "?subAccountViewID=" + viewId, false);
            }
        }

        # endregion

        #region Grids

        protected void RdgSendInvitationsConfirmed_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    BuildRepeaterItem(args);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgSendInvitationsConfirmed_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                LoadGridData();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Buttons

        protected void ImgBtnSendEmail_OnClick(object sender, EventArgs args)
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
                            HiddenField hdnConfirmationUrl = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnConfirmationUrl");
                            HiddenField hdnEmail = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnEmail");

                            if (hdnConfirmationUrl != null && hdnEmail != null)
                            {
                                try
                                {
                                    EmailSenderLib.InvitationEmail(hdnEmail.Value, vSession.User.CompanyName, hdnConfirmationUrl.Value.ToString(), vSession.Lang, session);
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }

                                LoadGridData();

                                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "4")).Text.Replace("{companyemail}", hdnEmail.Value);

                                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Success, true, false, true, true, false);
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
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, false, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void ImgBtnSettings_OnClick(object sender, EventArgs args)
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
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");
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

        # endregion

        #region DropDownLists


        #endregion
    }
}