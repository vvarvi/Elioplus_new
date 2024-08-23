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
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Roles;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus
{
    public partial class DashboardPermissionsRolesManagementPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public enum Action
        {
            INSERT = 1,
            DELETE = 2,
            NONE = 0
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

                    if (!IsPostBack)
                    {
                        try
                        {
                            if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
                                if (vSession.LoggedInSubAccountRoleID == 0)
                                    ManageRoles.InsertSystemRolePermissionsByUser(vSession.User.Id, session);
                                else
                                    RBtnSave.Enabled = RBtnCancel.Enabled = RBtnSavePermissions.Enabled = RBtnCancelPermissions.Enabled = false;
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }

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

        #region Methods

        private void FixPage()
        {
            UpdateStrings();
            SetLinks();
            ResetFields();
            LoadUserRoles();

            #region To delete

            //divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
            //RdgFormPermissions.ShowHeader = false;

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

            //aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

            #endregion

            FixRolesManagementSettingsArea();
            FixButtons();
        }

        private void FixButtons()
        {
            if (divRow10.Visible)
            {
                ImgBtnAddRole9.Visible = true;
                ImgBtnAddRole.Visible = ImgBtnAddRole1.Visible = ImgBtnAddRole2.Visible = ImgBtnAddRole3.Visible = ImgBtnAddRole4.Visible = ImgBtnAddRole5.Visible = ImgBtnAddRole6.Visible = ImgBtnAddRole7.Visible = ImgBtnAddRole8.Visible = false;
            }
            else
            {
                if (divRow9.Visible)
                {
                    ImgBtnAddRole8.Visible = true;
                    ImgBtnAddRole.Visible = ImgBtnAddRole1.Visible = ImgBtnAddRole2.Visible = ImgBtnAddRole3.Visible = ImgBtnAddRole4.Visible = ImgBtnAddRole5.Visible = ImgBtnAddRole6.Visible = ImgBtnAddRole7.Visible = ImgBtnAddRole9.Visible = false;
                }
                else
                {
                    if (divRow8.Visible)
                    {
                        ImgBtnAddRole7.Visible = true;
                        ImgBtnAddRole.Visible = ImgBtnAddRole1.Visible = ImgBtnAddRole2.Visible = ImgBtnAddRole3.Visible = ImgBtnAddRole4.Visible = ImgBtnAddRole5.Visible = ImgBtnAddRole6.Visible = ImgBtnAddRole8.Visible = ImgBtnAddRole9.Visible = false;
                    }
                    else
                    {
                        if (divRow7.Visible)
                        {
                            ImgBtnAddRole6.Visible = true;
                            ImgBtnAddRole.Visible = ImgBtnAddRole1.Visible = ImgBtnAddRole2.Visible = ImgBtnAddRole3.Visible = ImgBtnAddRole4.Visible = ImgBtnAddRole5.Visible = ImgBtnAddRole7.Visible = ImgBtnAddRole8.Visible = ImgBtnAddRole9.Visible = false;
                        }
                        else
                        {
                            if (divRow6.Visible)
                            {
                                ImgBtnAddRole5.Visible = true;
                                ImgBtnAddRole.Visible = ImgBtnAddRole1.Visible = ImgBtnAddRole2.Visible = ImgBtnAddRole3.Visible = ImgBtnAddRole4.Visible = ImgBtnAddRole6.Visible = ImgBtnAddRole7.Visible = ImgBtnAddRole8.Visible = ImgBtnAddRole9.Visible = false;
                            }
                            else
                            {
                                if (divRow5.Visible)
                                {
                                    ImgBtnAddRole4.Visible = true;
                                    ImgBtnAddRole.Visible = ImgBtnAddRole1.Visible = ImgBtnAddRole2.Visible = ImgBtnAddRole3.Visible = ImgBtnAddRole5.Visible = ImgBtnAddRole6.Visible = ImgBtnAddRole7.Visible = ImgBtnAddRole8.Visible = ImgBtnAddRole9.Visible = false;
                                }
                                else
                                {
                                    if (divRow4.Visible)
                                    {
                                        ImgBtnAddRole3.Visible = true;
                                        ImgBtnAddRole.Visible = ImgBtnAddRole1.Visible = ImgBtnAddRole2.Visible = ImgBtnAddRole4.Visible = ImgBtnAddRole5.Visible = ImgBtnAddRole6.Visible = ImgBtnAddRole7.Visible = ImgBtnAddRole8.Visible = ImgBtnAddRole9.Visible = false;
                                    }
                                    else
                                    {
                                        if (divRow3.Visible)
                                        {
                                            ImgBtnAddRole2.Visible = true;
                                            ImgBtnAddRole.Visible = ImgBtnAddRole1.Visible = ImgBtnAddRole3.Visible = ImgBtnAddRole4.Visible = ImgBtnAddRole5.Visible = ImgBtnAddRole6.Visible = ImgBtnAddRole7.Visible = ImgBtnAddRole8.Visible = ImgBtnAddRole9.Visible = false;
                                        }
                                        else
                                        {
                                            if (divRow2.Visible)
                                            {
                                                ImgBtnAddRole1.Visible = true;
                                                ImgBtnAddRole.Visible = ImgBtnAddRole2.Visible = ImgBtnAddRole3.Visible = ImgBtnAddRole4.Visible = ImgBtnAddRole5.Visible = ImgBtnAddRole6.Visible = ImgBtnAddRole7.Visible = ImgBtnAddRole8.Visible = ImgBtnAddRole9.Visible = false;
                                            }
                                            else
                                            {
                                                if (divRow1.Visible)
                                                {
                                                    ImgBtnAddRole.Visible = true;
                                                    ImgBtnAddRole1.Visible = ImgBtnAddRole2.Visible = ImgBtnAddRole3.Visible = ImgBtnAddRole4.Visible = ImgBtnAddRole5.Visible = ImgBtnAddRole6.Visible = ImgBtnAddRole7.Visible = ImgBtnAddRole8.Visible = ImgBtnAddRole9.Visible = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadUserRoles()
        {
            DrpRoles.Items.Clear();
            DrpDefaultRoles.Items.Clear();

            List<ElioPermissionsUsersRoles> roles = SqlRoles.GetUserPermissionRoles(vSession.User.Id, "", session);

            if (roles.Count > 0)
            {
                foreach (ElioPermissionsUsersRoles role in roles)
                {
                    ListItem item = new ListItem();

                    item.Value = role.Id.ToString();
                    item.Text = role.RoleName;

                    DrpRoles.Items.Add(item);
                    DrpDefaultRoles.Items.Add(item);
                }

                DrpRoles.SelectedValue = DrpDefaultRoles.SelectedValue = roles[0].Id.ToString();
                DrpRoles.SelectedItem.Text = DrpDefaultRoles.SelectedItem.Text = roles[0].RoleName;
            }
        }

        private void FixRolesManagementSettingsArea()
        {
            try
            {
                bool hasRoles = SqlRoles.HasUserPermissionRoles(vSession.User.Id, "1", session);
                if (!hasRoles)
                {
                    divRoleManagementArea.Visible = true;
                    divRolePermissionsArea.Visible = false;
                }
                else
                {
                    divRoleManagementArea.Visible = true;
                    divRolePermissionsArea.Visible = true;
                    GetRolesByUser();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void GetRolesByUser()
        {
            try
            {
                List<ElioPermissionsUsersRoles> roles = SqlRoles.GetUserPermissionRoles(vSession.User.Id, "0", session);

                if (roles.Count > 0)
                {
                    if (roles.Count >= 1)
                    {
                        divRow1.Visible = true;
                        HdnRoleID1.Value = roles[0].Id.ToString();
                        TbxRoleName.Text = roles[0].RoleName;
                        TbxDescription.Text = roles[0].RoleDescription;
                    }

                    if (roles.Count >= 2)
                    {
                        divRow2.Visible = true;
                        HdnRoleID2.Value = roles[1].Id.ToString();
                        TextBox1.Text = roles[1].RoleName;
                        TextBox2.Text = roles[1].RoleDescription;
                    }

                    if (roles.Count >= 3)
                    {
                        divRow3.Visible = true;
                        HdnRoleID3.Value = roles[2].Id.ToString();
                        TextBox4.Text = roles[2].RoleName;
                        TextBox5.Text = roles[2].RoleDescription;
                    }

                    if (roles.Count >= 4)
                    {
                        divRow4.Visible = true;
                        HdnRoleID4.Value = roles[3].Id.ToString();
                        TextBox7.Text = roles[3].RoleName;
                        TextBox8.Text = roles[3].RoleDescription;
                    }

                    if (roles.Count >= 5)
                    {
                        divRow5.Visible = true;
                        HdnRoleID5.Value = roles[4].Id.ToString();
                        TextBox10.Text = roles[4].RoleName;
                        TextBox11.Text = roles[4].RoleDescription;
                    }

                    if (roles.Count >= 6)
                    {
                        divRow6.Visible = true;
                        HdnRoleID6.Value = roles[5].Id.ToString();
                        TextBox12.Text = roles[5].RoleName;
                        TextBox13.Text = roles[5].RoleDescription;
                    }

                    if (roles.Count >= 7)
                    {
                        divRow7.Visible = true;
                        HdnRoleID7.Value = roles[6].Id.ToString();
                        TextBox14.Text = roles[6].RoleName;
                        TextBox15.Text = roles[6].RoleDescription;
                    }

                    if (roles.Count >= 8)
                    {
                        divRow8.Visible = true;
                        HdnRoleID8.Value = roles[7].Id.ToString();
                        TextBox16.Text = roles[7].RoleName;
                        TextBox17.Text = roles[7].RoleDescription;
                    }

                    if (roles.Count >= 9)
                    {
                        divRow9.Visible = true;
                        HdnRoleID9.Value = roles[8].Id.ToString();
                        TextBox18.Text = roles[8].RoleName;
                        TextBox19.Text = roles[8].RoleDescription;
                    }

                    if (roles.Count >= 10)
                    {
                        divRow10.Visible = true;
                        HdnRoleID10.Value = roles[9].Id.ToString();
                        TextBox20.Text = roles[9].RoleName;
                        TextBox21.Text = roles[9].RoleDescription;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void SetLinks()
        {
            //aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
        }

        private void UpdateStrings()
        {
            //if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
            //    ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
            //    if (packet != null)
            //    {
            //        LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
            //    }
            //}
            //else
            //{
            //    LblPricingPlan.Text = "You are currently on a free plan";
            //}

            //LblElioplusDashboard.Text = "";

            //LblDashboard.Text = "Dashboard";

            //aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;

            //if (aBtnGoPremium.Visible)
            //{
            //    LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
            //    LblPricingPlan.Visible = false;
            //}

            //LblGoFull.Text = "Complete your registration";
            //LblDashPage.Text = "Permissions & Roles Management";
            //LblDashSubTitle.Text = "";
        }

        private void ResetFields()
        {
            try
            {
                divRow2.Visible = divRow3.Visible = divRow4.Visible = divRow5.Visible =
                divRow6.Visible = divRow7.Visible = divRow8.Visible = divRow9.Visible = divRow10.Visible = false;

                HdnRoleID1.Value = HdnRoleID2.Value = HdnRoleID3.Value = HdnRoleID4.Value = HdnRoleID5.Value =
                HdnRoleID6.Value = HdnRoleID7.Value = HdnRoleID8.Value = HdnRoleID9.Value = HdnRoleID10.Value = "0";

                TbxRoleName.Text = TbxDescription.Text = "";
                TextBox1.Text =
                TextBox2.Text =
                TextBox4.Text =
                TextBox5.Text =
                TextBox7.Text =
                TextBox8.Text =
                TextBox10.Text =
                TextBox11.Text =
                TextBox12.Text =
                TextBox13.Text =
                TextBox14.Text =
                TextBox15.Text =
                TextBox16.Text =
                TextBox17.Text =
                TextBox18.Text =
                TextBox19.Text =
                TextBox20.Text =
                TextBox21.Text = "";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void InsertOrUpdateRole(int roleId, string roleName, string roleDescription, List<int> formsIDs, out int insertedRoleId)
        {
            insertedRoleId = 0;

            DataLoader<ElioPermissionsUsersRoles> loader = new DataLoader<ElioPermissionsUsersRoles>(session);

            if (roleId == 0)
            {
                ElioPermissionsUsersRoles userRole = new ElioPermissionsUsersRoles();

                userRole.UserId = vSession.User.Id;
                userRole.RoleName = roleName;
                userRole.RoleDescription = roleDescription;
                userRole.IsSystem = 0;
                userRole.ParentRoleId = roleId;
                userRole.Sysdate = DateTime.Now;
                userRole.LastUpdate = DateTime.Now;
                userRole.IsActive = 1;
                userRole.IsPublic = 1;

                loader.Insert(userRole);

                insertedRoleId = userRole.Id;

                List<ElioPermissionsFormsActions> permissions = SqlRoles.GetDefaultFormActions(session);

                foreach (ElioPermissionsFormsActions permission in permissions)
                {
                    bool existFormAction = SqlRoles.ExistRoleFormAction(vSession.User.Id, userRole.Id, permission.Id, session);
                    if (!existFormAction)
                    {
                        ElioPermissionsRolesFormsActions roleFA = new ElioPermissionsRolesFormsActions();

                        roleFA.UserId = vSession.User.Id;
                        roleFA.RoleId = userRole.Id;
                        roleFA.FormActionId = permission.Id;
                        roleFA.Sysdate = DateTime.Now;
                        roleFA.LastUpdate = DateTime.Now;
                        roleFA.IsActive = 1;
                        roleFA.IsPublic = 1;

                        DataLoader<ElioPermissionsRolesFormsActions> loaderRoleFA = new DataLoader<ElioPermissionsRolesFormsActions>(session);
                        loaderRoleFA.Insert(roleFA);
                    }
                }
            }
            else
            {
                ElioPermissionsUsersRoles userRole = SqlRoles.GetUserPermissionsRole(vSession.User.Id, roleId, session);
                if (userRole != null)
                {
                    userRole.UserId = vSession.User.Id;
                    userRole.RoleName = roleName;
                    userRole.RoleDescription = roleDescription;
                    userRole.LastUpdate = DateTime.Now;

                    loader.Update(userRole);

                    insertedRoleId = userRole.Id;
                }
            }
        }

        private bool InsertOrDeletePermissionsRolesFormActions(int formId, int actionId, bool isChecked)
        {
            Action action = Action.NONE;
            bool success = true;

            bool hasUserPermissionRoleFormAction = SqlRoles.ExistUserRolesFormAction(vSession.User.Id, Convert.ToInt32(DrpRoles.SelectedValue), formId, actionId, session);
            if (hasUserPermissionRoleFormAction && !isChecked)
                action = Action.DELETE;
            else if (!hasUserPermissionRoleFormAction && isChecked)
                action = Action.INSERT;

            if (action != Action.NONE)
            {
                int formActionID = SqlRoles.GetPermissionsFormActionIdTbl(formId, actionId, session);
                if (formActionID > 0)
                {
                    switch (action)
                    {
                        case Action.INSERT:

                            ElioPermissionsRolesFormsActions userPermission = new ElioPermissionsRolesFormsActions();

                            userPermission.UserId = vSession.User.Id;
                            userPermission.RoleId = Convert.ToInt32(DrpRoles.SelectedValue);
                            userPermission.FormActionId = formActionID;
                            userPermission.Sysdate = DateTime.Now;
                            userPermission.LastUpdate = DateTime.Now;
                            userPermission.IsActive = 1;
                            userPermission.IsPublic = 1;

                            DataLoader<ElioPermissionsRolesFormsActions> loader = new DataLoader<ElioPermissionsRolesFormsActions>(session);
                            loader.Insert(userPermission);

                            success = userPermission.Id > 0;

                            break;

                        case Action.DELETE:

                            success = SqlRoles.DeletePermissionsRolesFormAction(vSession.User.Id, Convert.ToInt32(DrpRoles.SelectedValue), formActionID, session);

                            break;
                    }
                }
            }

            return success;
        }

        private void LoadFormPermissions()
        {
            List<ElioPermissionsForms> forms = SqlRoles.GetPermissionsForms(session);
            if (forms.Count > 0)
            {
                List<ElioPermissionsActions> actions = SqlRoles.GetPermissionsActions(session);
                if (actions.Count > 0 && actions.Count >= 7)
                {
                    RdgFormPermissions.Visible = true;
                    divRolePermissionsArea.Visible = true;

                    RdgFormPermissions.MasterTableView.GetColumn("action1").HeaderText = actions[0].ActionName;
                    RdgFormPermissions.MasterTableView.GetColumn("action2").HeaderText = actions[1].ActionName;
                    RdgFormPermissions.MasterTableView.GetColumn("action3").HeaderText = actions[2].ActionName;
                    RdgFormPermissions.MasterTableView.GetColumn("action4").HeaderText = actions[3].ActionName;
                    RdgFormPermissions.MasterTableView.GetColumn("action5").HeaderText = actions[4].ActionName;
                    RdgFormPermissions.MasterTableView.GetColumn("action6").HeaderText = actions[5].ActionName;
                    RdgFormPermissions.MasterTableView.GetColumn("action7").HeaderText = actions[6].ActionName;

                    DataTable table = new DataTable();

                    table.Columns.Add("form_id");
                    table.Columns.Add("form_title");
                    table.Columns.Add("actionID1");
                    table.Columns.Add("action1");
                    table.Columns.Add("actionID2");
                    table.Columns.Add("action2");
                    table.Columns.Add("actionID3");
                    table.Columns.Add("action3");
                    table.Columns.Add("actionID4");
                    table.Columns.Add("action4");
                    table.Columns.Add("actionID5");
                    table.Columns.Add("action5");
                    table.Columns.Add("actionID6");
                    table.Columns.Add("action6");
                    table.Columns.Add("actionID7");
                    table.Columns.Add("action7");

                    if (actions.Count >= 7)
                    {
                        foreach (ElioPermissionsForms form in forms)
                        {
                            //bool hasAction1 = Sql.ExistFormAction(form.Id, actions[0].Id, session);
                            //bool hasAction2 = Sql.ExistFormAction(form.Id, actions[1].Id, session);
                            //bool hasAction3 = Sql.ExistFormAction(form.Id, actions[2].Id, session);
                            //bool hasAction4 = Sql.ExistFormAction(form.Id, actions[3].Id, session);
                            //bool hasAction5 = Sql.ExistFormAction(form.Id, actions[4].Id, session);
                            //bool hasAction6 = Sql.ExistFormAction(form.Id, actions[5].Id, session);

                            table.Rows.Add(form.Id, form.FormTitle + " 's actions: "
                                , actions[0].Id, actions[0].ActionName
                                , actions[1].Id, actions[1].ActionName
                                , actions[2].Id, actions[2].ActionName
                                , actions[3].Id, actions[3].ActionName
                                , actions[4].Id, actions[4].ActionName
                                , actions[5].Id, actions[5].ActionName
                                , actions[6].Id, actions[6].ActionName);
                        }

                        RdgFormPermissions.DataSource = table;
                    }
                }
                else
                {
                    RdgFormPermissions.Visible = false;
                    divRolePermissionsArea.Visible = false;
                }
            }
        }

        private bool IsValidData()
        {
            if (divRow1.Visible)
            {
                if (TbxRoleName.Text == "" || TbxDescription.Text == "")
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please add role name and description for your first role!", MessageTypes.Error, true, true, true, true, false);
                    return false;
                }
            }

            if (divRow2.Visible)
            {
                if (TextBox1.Text == "" || TextBox2.Text == "")
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please add role name and description for your second role!", MessageTypes.Error, true, true, true, true, false);
                    return false;
                }
            }
            if (divRow3.Visible)
            {
                if (TextBox4.Text == "" || TextBox5.Text == "")
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please add role name and description for your third role!", MessageTypes.Error, true, true, true, true, false);
                    return false;
                }
            }
            if (divRow4.Visible)
            {
                if (TextBox7.Text == "" || TextBox8.Text == "")
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please add role name and description for your fourth role!", MessageTypes.Error, true, true, true, true, false);
                    return false;
                }
            }

            if (divRow5.Visible)
            {
                if (TextBox10.Text == "" || TextBox11.Text == "")
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please add role name and description for your fifth role!", MessageTypes.Error, true, true, true, true, false);
                    return false;
                }
            }

            if (divRow6.Visible)
            {
                if (TextBox12.Text == "" || TextBox13.Text == "")
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please add role name and description for your sixth role!", MessageTypes.Error, true, true, true, true, false);
                    return false;
                }
            }

            if (divRow7.Visible)
            {
                if (TextBox14.Text == "" || TextBox15.Text == "")
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please add role name and description for your seventh role!", MessageTypes.Error, true, true, true, true, false);
                    return false;
                }
            }
            if (divRow8.Visible)
            {
                if (TextBox16.Text == "" || TextBox17.Text == "")
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please add role name and description for your eighth role!", MessageTypes.Error, true, true, true, true, false);
                    return false;
                }
            }
            if (divRow9.Visible)
            {
                if (TextBox18.Text == "" || TextBox19.Text == "")
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please add role name and description for your nineth role!", MessageTypes.Error, true, true, true, true, false);
                    return false;
                }
            }

            if (divRow10.Visible)
            {
                if (TextBox20.Text == "" || TextBox21.Text == "")
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please add role name and description for your tenth role!", MessageTypes.Error, true, true, true, true, false);
                    return false;
                }
            }

            return true;
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
                    //HtmlAnchor aRemoveRole = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aRemoveRole");
                    HtmlAnchor aAddRole = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aAddRole");
                    HiddenField hdnRoleID = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnRoleID");

                    if (hdnRoleID.Value != "0")
                    {
                        int lastRoleID = SqlRoles.GetUserLastPermissionRoleIDTbl(vSession.User.Id, "0", session);
                        if (lastRoleID > 0)
                            aAddRole.Visible = lastRoleID == Convert.ToInt32(hdnRoleID.Value);
                        else
                            aAddRole.Visible = true;
                    }
                }
            }
        }

        private void LoadUserRolesData()
        {
            DataTable rolesTable = SqlRoles.GetUserPermissionRolesTbl(vSession.User.Id, "0", session);

            if (rolesTable.Rows.Count == 0)
            {
                rolesTable.Rows.Add(0, vSession.User.Id, "", "", 0, 0);
            }

            RdgRoles.DataSource = rolesTable;
            RdgRoles.DataBind();
        }

        #endregion

        #region Grids

        protected void RdgFormPermissions_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    #region Parent

                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblDescription1 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription1");
                    CheckBox cbxSelectAction1 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction1");
                    HtmlGenericControl divNotExistAction1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotExistAction1");

                    Label lblDescription2 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription2");
                    CheckBox cbxSelectAction2 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction2");
                    HtmlGenericControl divNotExistAction2 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotExistAction2");

                    Label lblDescription3 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription3");
                    CheckBox cbxSelectAction3 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction3");
                    HtmlGenericControl divNotExistAction3 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotExistAction3");

                    Label lblDescription4 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription4");
                    CheckBox cbxSelectAction4 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction4");
                    HtmlGenericControl divNotExistAction4 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotExistAction4");

                    Label lblDescription5 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription5");
                    CheckBox cbxSelectAction5 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction5");
                    HtmlGenericControl divNotExistAction5 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotExistAction5");

                    Label lblDescription6 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription6");
                    CheckBox cbxSelectAction6 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction6");
                    HtmlGenericControl divNotExistAction6 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotExistAction6");

                    Label lblDescription7 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription7");
                    CheckBox cbxSelectAction7 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction7");
                    HtmlGenericControl divNotExistAction7 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotExistAction7");

                    bool existAction1 = SqlRoles.ExistFormAction(Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID1"].Text), session);
                    if (existAction1)
                    {
                        cbxSelectAction1.Visible = true;
                        divNotExistAction1.Visible = false;
                        cbxSelectAction1.Enabled = DrpRoles.SelectedItem.Text != "Admin";
                        cbxSelectAction1.Checked = SqlRoles.ExistUserRolesFormAction(vSession.User.Id, Convert.ToInt32(DrpRoles.SelectedValue), Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID1"].Text), session);
                    }
                    else
                    {
                        cbxSelectAction1.Visible = false;
                        divNotExistAction1.Visible = true;
                    }

                    bool existAction2 = SqlRoles.ExistFormAction(Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID2"].Text), session);
                    if (existAction2)
                    {
                        cbxSelectAction2.Visible = true;
                        divNotExistAction2.Visible = false;
                        cbxSelectAction2.Enabled = DrpRoles.SelectedItem.Text != "Admin";
                        cbxSelectAction2.Checked = SqlRoles.ExistUserRolesFormAction(vSession.User.Id, Convert.ToInt32(DrpRoles.SelectedValue), Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID2"].Text), session);
                    }
                    else
                    {
                        cbxSelectAction2.Visible = false;
                        divNotExistAction2.Visible = true;
                    }

                    bool existAction3 = SqlRoles.ExistFormAction(Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID3"].Text), session);
                    if (existAction3)
                    {
                        cbxSelectAction3.Visible = true;
                        divNotExistAction3.Visible = false;
                        cbxSelectAction3.Enabled = DrpRoles.SelectedItem.Text != "Admin";
                        cbxSelectAction3.Checked = SqlRoles.ExistUserRolesFormAction(vSession.User.Id, Convert.ToInt32(DrpRoles.SelectedValue), Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID3"].Text), session);
                    }
                    else
                    {
                        cbxSelectAction3.Visible = false;
                        divNotExistAction3.Visible = true;
                    }

                    bool existAction4 = SqlRoles.ExistFormAction(Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID4"].Text), session);
                    if (existAction4)
                    {
                        cbxSelectAction4.Visible = true;
                        divNotExistAction4.Visible = false;
                        cbxSelectAction4.Enabled = DrpRoles.SelectedItem.Text != "Admin";
                        cbxSelectAction4.Checked = SqlRoles.ExistUserRolesFormAction(vSession.User.Id, Convert.ToInt32(DrpRoles.SelectedValue), Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID4"].Text), session);
                    }
                    else
                    {
                        cbxSelectAction4.Visible = false;
                        divNotExistAction4.Visible = true;
                    }

                    bool existAction5 = SqlRoles.ExistFormAction(Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID5"].Text), session);
                    if (existAction5)
                    {
                        cbxSelectAction5.Visible = true;
                        divNotExistAction5.Visible = false;
                        cbxSelectAction5.Enabled = DrpRoles.SelectedItem.Text != "Admin";
                        cbxSelectAction5.Checked = SqlRoles.ExistUserRolesFormAction(vSession.User.Id, Convert.ToInt32(DrpRoles.SelectedValue), Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID5"].Text), session);
                    }
                    else
                    {
                        cbxSelectAction5.Visible = false;
                        divNotExistAction5.Visible = true;
                    }

                    bool existAction6 = SqlRoles.ExistFormAction(Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID6"].Text), session);
                    if (existAction6)
                    {
                        cbxSelectAction6.Visible = true;
                        divNotExistAction6.Visible = false;
                        cbxSelectAction6.Enabled = DrpRoles.SelectedItem.Text != "Admin";
                        cbxSelectAction6.Checked = SqlRoles.ExistUserRolesFormAction(vSession.User.Id, Convert.ToInt32(DrpRoles.SelectedValue), Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID6"].Text), session);
                    }
                    else
                    {
                        cbxSelectAction6.Visible = false;
                        divNotExistAction6.Visible = true;
                    }

                    bool existAction7 = SqlRoles.ExistFormAction(Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID7"].Text), session);
                    if (existAction7)
                    {
                        cbxSelectAction7.Visible = true;
                        divNotExistAction7.Visible = false;
                        cbxSelectAction7.Enabled = DrpRoles.SelectedItem.Text != "Admin";
                        cbxSelectAction7.Checked = SqlRoles.ExistUserRolesFormAction(vSession.User.Id, Convert.ToInt32(DrpRoles.SelectedValue), Convert.ToInt32(item["form_id"].Text), Convert.ToInt32(item["actionID7"].Text), session);
                    }
                    else
                    {
                        cbxSelectAction7.Visible = false;
                        divNotExistAction7.Visible = true;
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

        protected void RdgFormPermissions_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                    LoadFormPermissions();
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

        protected void RdgRoles_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                    BuildRepeaterItem(args);
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

        protected void RdgRoles_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                //if (vSession.User != null)
                //LoadUserRolesData();
                //else
                //    Response.Redirect(ControlLoader.Login, false);
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

        protected void aAddRole_ServerClick(object sender, EventArgs e)
        {
            try
            {
                HtmlAnchor aBtn = (HtmlAnchor)sender;
                if (aBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)aBtn.NamingContainer;
                    if (item != null)
                    {

                    }
                }

                FixButtons();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aAddNewRole_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (divRow2.Visible == false)
                {
                    divRow2.Visible = true;
                }
                else if (divRow3.Visible == false)
                {
                    divRow3.Visible = true;
                }
                else if (divRow4.Visible == false)
                {
                    divRow4.Visible = true;
                }
                else if (divRow5.Visible == false)
                {
                    divRow5.Visible = true;
                }
                else if (divRow6.Visible == false)
                {
                    divRow6.Visible = true;
                }
                else if (divRow7.Visible == false)
                {
                    divRow7.Visible = true;
                }
                else if (divRow8.Visible == false)
                {
                    divRow8.Visible = true;
                }
                else if (divRow9.Visible == false)
                {
                    divRow9.Visible = true;
                }
                else if (divRow10.Visible == false)
                {
                    divRow10.Visible = true;
                }
                else
                {
                    LblMessageAlertfMsg.Text = "If you want to save more than 10 roles, please contact us!";
                    BtnRemoveRole.Visible = false;
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }

                FixButtons();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    UcMessageAlertControl.Visible = false;

                    if (!IsValidData())
                        return;

                    bool hasRole = false;
                    int insertedRoleId = 0;

                    List<int> formsIDs = SqlRoles.GetPermissionFormsIdsArray(session);
                    if (formsIDs.Count > 0)
                    {
                        if (TbxRoleName.Text != "" && TbxDescription.Text != "")
                        {
                            InsertOrUpdateRole(Convert.ToInt32(HdnRoleID1.Value), TbxRoleName.Text, TbxDescription.Text, formsIDs, out insertedRoleId);
                            if (insertedRoleId > 0)
                            {
                                HdnRoleID1.Value = insertedRoleId.ToString();
                                hasRole = true;
                            }
                        }

                        if (divRow2.Visible)
                        {
                            if (TextBox1.Text != "" && TextBox2.Text != "")
                            {
                                insertedRoleId = 0;
                                InsertOrUpdateRole(Convert.ToInt32(HdnRoleID2.Value), TextBox1.Text, TextBox2.Text, formsIDs, out insertedRoleId);
                                if (insertedRoleId > 0)
                                {
                                    HdnRoleID2.Value = insertedRoleId.ToString();
                                    hasRole = true;
                                }
                            }
                        }

                        if (divRow3.Visible)
                        {
                            if (TextBox4.Text != "" && TextBox5.Text != "")
                            {
                                insertedRoleId = 0;
                                InsertOrUpdateRole(Convert.ToInt32(HdnRoleID3.Value), TextBox4.Text, TextBox5.Text, formsIDs, out insertedRoleId);
                                if (insertedRoleId > 0)
                                {
                                    HdnRoleID3.Value = insertedRoleId.ToString();
                                    hasRole = true;
                                }
                            }
                        }

                        if (divRow4.Visible)
                        {
                            if (TextBox7.Text != "" && TextBox8.Text != "")
                            {
                                insertedRoleId = 0;
                                InsertOrUpdateRole(Convert.ToInt32(HdnRoleID4.Value), TextBox7.Text, TextBox8.Text, formsIDs, out insertedRoleId);
                                if (insertedRoleId > 0)
                                {
                                    HdnRoleID4.Value = insertedRoleId.ToString();
                                    hasRole = true;
                                }
                            }
                        }

                        if (divRow5.Visible)
                        {
                            if (TextBox10.Text != "" && TextBox11.Text != "")
                            {
                                insertedRoleId = 0;
                                InsertOrUpdateRole(Convert.ToInt32(HdnRoleID5.Value), TextBox10.Text, TextBox11.Text, formsIDs, out insertedRoleId);
                                if (insertedRoleId > 0)
                                {
                                    HdnRoleID5.Value = insertedRoleId.ToString();
                                    hasRole = true;
                                }
                            }
                        }

                        if (divRow6.Visible)
                        {
                            if (TextBox12.Text != "" && TextBox13.Text != "")
                            {
                                insertedRoleId = 0;
                                InsertOrUpdateRole(Convert.ToInt32(HdnRoleID6.Value), TextBox12.Text, TextBox13.Text, formsIDs, out insertedRoleId);
                                if (insertedRoleId > 0)
                                {
                                    HdnRoleID6.Value = insertedRoleId.ToString();
                                    hasRole = true;
                                }
                            }
                        }

                        if (divRow7.Visible)
                        {
                            if (TextBox14.Text != "" && TextBox15.Text != "")
                            {
                                insertedRoleId = 0;
                                InsertOrUpdateRole(Convert.ToInt32(HdnRoleID7.Value), TextBox14.Text, TextBox15.Text, formsIDs, out insertedRoleId);
                                if (insertedRoleId > 0)
                                {
                                    HdnRoleID7.Value = insertedRoleId.ToString();
                                    hasRole = true;
                                }
                            }
                        }

                        if (divRow8.Visible)
                        {
                            if (TextBox16.Text != "" && TextBox17.Text != "")
                            {
                                insertedRoleId = 0;
                                InsertOrUpdateRole(Convert.ToInt32(HdnRoleID8.Value), TextBox16.Text, TextBox17.Text, formsIDs, out insertedRoleId);
                                if (insertedRoleId > 0)
                                {
                                    HdnRoleID8.Value = insertedRoleId.ToString();
                                    hasRole = true;
                                }
                            }
                        }

                        if (divRow9.Visible)
                        {
                            if (TextBox18.Text != "" && TextBox19.Text != "")
                            {
                                insertedRoleId = 0;
                                InsertOrUpdateRole(Convert.ToInt32(HdnRoleID9.Value), TextBox18.Text, TextBox19.Text, formsIDs, out insertedRoleId);
                                if (insertedRoleId > 0)
                                {
                                    HdnRoleID9.Value = insertedRoleId.ToString();
                                    hasRole = true;
                                }
                            }
                        }

                        if (divRow10.Visible)
                        {
                            if (TextBox20.Text != "" && TextBox21.Text != "")
                            {
                                insertedRoleId = 0;
                                InsertOrUpdateRole(Convert.ToInt32(HdnRoleID10.Value), TextBox20.Text, TextBox21.Text, formsIDs, out insertedRoleId);
                                if (insertedRoleId > 0)
                                {
                                    HdnRoleID10.Value = insertedRoleId.ToString();
                                    hasRole = true;
                                }
                            }
                        }

                        if (!hasRole)
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Something went wrong. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                        else
                        {
                            divRolePermissionsArea.Visible = true;
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Your roles saved successfully!", MessageTypes.Success, true, true, true, true, false);

                            LoadUserRoles();
                            RdgFormPermissions.Rebind();
                        }
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Something went wrong. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void RBtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                FixPage();
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

        protected void RBtnSavePermissions_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    bool success = true;

                    foreach (GridDataItem item in RdgFormPermissions.Items)
                    {
                        if (item is GridDataItem)
                        {
                            #region Find Controls

                            CheckBox cbx1 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction1");
                            CheckBox cbx2 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction2");
                            CheckBox cbx3 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction3");
                            CheckBox cbx4 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction4");
                            CheckBox cbx5 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction5");
                            CheckBox cbx6 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction6");
                            CheckBox cbx7 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction7");

                            #endregion

                            int formId = Convert.ToInt32(item["form_id"].Text);
                            int actionId1 = Convert.ToInt32(item["actionID1"].Text);
                            int actionId2 = Convert.ToInt32(item["actionID2"].Text);
                            int actionId3 = Convert.ToInt32(item["actionID3"].Text);
                            int actionId4 = Convert.ToInt32(item["actionID4"].Text);
                            int actionId5 = Convert.ToInt32(item["actionID5"].Text);
                            int actionId6 = Convert.ToInt32(item["actionID6"].Text);
                            int actionId7 = Convert.ToInt32(item["actionID7"].Text);

                            if (formId > 0 && actionId1 > 0 && actionId2 > 0 && actionId3 > 0 && actionId4 > 0 && actionId5 > 0 && actionId6 > 0 && actionId7 > 0)
                            {
                                if (cbx1.Visible)
                                {
                                    success = InsertOrDeletePermissionsRolesFormActions(formId, actionId1, cbx1.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }

                                if (cbx2.Visible)
                                {
                                    success = InsertOrDeletePermissionsRolesFormActions(formId, actionId2, cbx2.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }

                                if (cbx3.Visible)
                                {
                                    success = InsertOrDeletePermissionsRolesFormActions(formId, actionId3, cbx3.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }

                                if (cbx4.Visible)
                                {
                                    success = InsertOrDeletePermissionsRolesFormActions(formId, actionId4, cbx4.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }

                                if (cbx5.Visible)
                                {
                                    success = InsertOrDeletePermissionsRolesFormActions(formId, actionId5, cbx5.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }

                                if (cbx6.Visible)
                                {
                                    success = InsertOrDeletePermissionsRolesFormActions(formId, actionId6, cbx6.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }

                                if (cbx7.Visible)
                                {
                                    success = InsertOrDeletePermissionsRolesFormActions(formId, actionId7, cbx7.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    if (success)
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your permissions updated successfully", MessageTypes.Success, true, true, true, true, false);
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

        protected void RBtnCancelPermissions_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                LoadFormPermissions();
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

        protected void ImgBtnRemoveRole_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (HdnRoleID1.Value == "0")
                {
                    divRow1.Visible = false;
                    TbxRoleName.Text = TbxDescription.Text = "";
                    HdnRoleID1.Value = "0";

                    divRow1.Visible = true;
                    ImgBtnRemoveRole.Visible = true;
                    FixButtons();
                }
                else
                {
                    bool existRoleToUser = SqlRoles.ExistRoleToSubAccount(vSession.User.Id, Convert.ToInt32(HdnRoleID1.Value), session);
                    if (existRoleToUser)
                    {
                        BtnRemoveRole.Visible = false;
                        LblMessageAlertTitle.Text = "Warning Message";
                        LblMessageAlertfMsg.Text = "You can not delete this role because it is assigned to user.";
                    }
                    else
                    {
                        HdnRoleID.Value = HdnRoleID1.Value;
                        BtnRemoveRole.Visible = true;
                        LblMessageAlertTitle.Text = "Information Message";
                        LblMessageAlertfMsg.Text = "You are going to delete this role. Do you want to proceed?";
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
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

        protected void ImgBtnRemove1_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnRoleID2.Value == "0")
                {
                    divRow2.Visible = false;
                    TextBox1.Text = TextBox2.Text = "";
                    HdnRoleID2.Value = "0";
                    FixButtons();
                }
                else
                {
                    bool existRoleToUser = SqlRoles.ExistRoleToSubAccount(vSession.User.Id, Convert.ToInt32(HdnRoleID2.Value), session);
                    if (existRoleToUser)
                    {
                        BtnRemoveRole.Visible = false;
                        LblMessageAlertTitle.Text = "Warning Message";
                        LblMessageAlertfMsg.Text = "You can not delete this role because it is assigned to user.";
                    }
                    else
                    {
                        HdnRoleID.Value = HdnRoleID2.Value;
                        BtnRemoveRole.Visible = true;
                        LblMessageAlertTitle.Text = "Information Message";
                        LblMessageAlertfMsg.Text = "You are going to delete this role. Do you want to proceed?";
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove2_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnRoleID3.Value == "0")
                {
                    divRow3.Visible = false;
                    TextBox4.Text = TextBox5.Text = "";
                    HdnRoleID3.Value = "0";
                    FixButtons();
                }
                else
                {
                    bool existRoleToUser = SqlRoles.ExistRoleToSubAccount(vSession.User.Id, Convert.ToInt32(HdnRoleID3.Value), session);
                    if (existRoleToUser)
                    {
                        BtnRemoveRole.Visible = false;
                        LblMessageAlertTitle.Text = "Warning Message";
                        LblMessageAlertfMsg.Text = "You can not delete this role because it is assigned to user.";
                    }
                    else
                    {
                        HdnRoleID.Value = HdnRoleID3.Value;
                        BtnRemoveRole.Visible = true;
                        LblMessageAlertTitle.Text = "Information Message";
                        LblMessageAlertfMsg.Text = "You are going to delete this role. Do you want to proceed?";
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove3_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnRoleID4.Value == "0")
                {
                    divRow4.Visible = false;
                    TextBox7.Text = TextBox8.Text = "";
                    HdnRoleID4.Value = "0";
                    FixButtons();
                }
                else
                {
                    bool existRoleToUser = SqlRoles.ExistRoleToSubAccount(vSession.User.Id, Convert.ToInt32(HdnRoleID4.Value), session);
                    if (existRoleToUser)
                    {
                        BtnRemoveRole.Visible = false;
                        LblMessageAlertTitle.Text = "Warning Message";
                        LblMessageAlertfMsg.Text = "You can not delete this role because it is assigned to user.";
                    }
                    else
                    {
                        HdnRoleID.Value = HdnRoleID4.Value;
                        BtnRemoveRole.Visible = true;
                        LblMessageAlertTitle.Text = "Information Message";
                        LblMessageAlertfMsg.Text = "You are going to delete this role. Do you want to proceed?";
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove4_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnRoleID5.Value == "0")
                {
                    divRow5.Visible = false;
                    TextBox10.Text = TextBox11.Text = "";
                    HdnRoleID5.Value = "0";
                    FixButtons();
                }
                else
                {
                    bool existRoleToUser = SqlRoles.ExistRoleToSubAccount(vSession.User.Id, Convert.ToInt32(HdnRoleID5.Value), session);
                    if (existRoleToUser)
                    {
                        BtnRemoveRole.Visible = false;
                        LblMessageAlertTitle.Text = "Warning Message";
                        LblMessageAlertfMsg.Text = "You can not delete this role because it is assigned to user.";
                    }
                    else
                    {
                        HdnRoleID.Value = HdnRoleID5.Value;
                        BtnRemoveRole.Visible = true;
                        LblMessageAlertTitle.Text = "Information Message";
                        LblMessageAlertfMsg.Text = "You are going to delete this role. Do you want to proceed?";
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove5_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnRoleID6.Value == "0")
                {
                    divRow6.Visible = false;
                    TextBox12.Text = TextBox13.Text = "";
                    HdnRoleID6.Value = "0";
                    FixButtons();
                }
                else
                {
                    bool existRoleToUser = SqlRoles.ExistRoleToSubAccount(vSession.User.Id, Convert.ToInt32(HdnRoleID6.Value), session);
                    if (existRoleToUser)
                    {
                        BtnRemoveRole.Visible = false;
                        LblMessageAlertTitle.Text = "Warning Message";
                        LblMessageAlertfMsg.Text = "You can not delete this role because it is assigned to user.";
                    }
                    else
                    {
                        HdnRoleID.Value = HdnRoleID6.Value;
                        BtnRemoveRole.Visible = true;
                        LblMessageAlertTitle.Text = "Information Message";
                        LblMessageAlertfMsg.Text = "You are going to delete this role. Do you want to proceed?";
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove6_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnRoleID7.Value == "0")
                {
                    divRow7.Visible = false;
                    TextBox14.Text = TextBox15.Text = "";
                    HdnRoleID7.Value = "0";
                    FixButtons();
                }
                else
                {
                    bool existRoleToUser = SqlRoles.ExistRoleToSubAccount(vSession.User.Id, Convert.ToInt32(HdnRoleID7.Value), session);
                    if (existRoleToUser)
                    {
                        BtnRemoveRole.Visible = false;
                        LblMessageAlertTitle.Text = "Warning Message";
                        LblMessageAlertfMsg.Text = "You can not delete this role because it is assigned to user.";
                    }
                    else
                    {
                        HdnRoleID.Value = HdnRoleID7.Value;
                        BtnRemoveRole.Visible = true;
                        LblMessageAlertTitle.Text = "Information Message";
                        LblMessageAlertfMsg.Text = "You are going to delete this role. Do you want to proceed?";
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove7_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnRoleID8.Value == "0")
                {
                    divRow8.Visible = false;
                    TextBox16.Text = TextBox17.Text = "";
                    HdnRoleID8.Value = "0";
                    FixButtons();
                }
                else
                {
                    bool existRoleToUser = SqlRoles.ExistRoleToSubAccount(vSession.User.Id, Convert.ToInt32(HdnRoleID8.Value), session);
                    if (existRoleToUser)
                    {
                        BtnRemoveRole.Visible = false;
                        LblMessageAlertTitle.Text = "Warning Message";
                        LblMessageAlertfMsg.Text = "You can not delete this role because it is assigned to user.";
                    }
                    else
                    {
                        HdnRoleID.Value = HdnRoleID8.Value;
                        BtnRemoveRole.Visible = true;
                        LblMessageAlertTitle.Text = "Information Message";
                        LblMessageAlertfMsg.Text = "You are going to delete this role. Do you want to proceed?";
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove8_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnRoleID9.Value == "0")
                {
                    divRow9.Visible = false;
                    TextBox18.Text = TextBox19.Text = "";
                    HdnRoleID9.Value = "0";
                    FixButtons();
                }
                else
                {
                    bool existRoleToUser = SqlRoles.ExistRoleToSubAccount(vSession.User.Id, Convert.ToInt32(HdnRoleID9.Value), session);
                    if (existRoleToUser)
                    {
                        BtnRemoveRole.Visible = false;
                        LblMessageAlertTitle.Text = "Warning Message";
                        LblMessageAlertfMsg.Text = "You can not delete this role because it is assigned to user.";
                    }
                    else
                    {
                        HdnRoleID.Value = HdnRoleID9.Value;
                        BtnRemoveRole.Visible = true;
                        LblMessageAlertTitle.Text = "Information Message";
                        LblMessageAlertfMsg.Text = "You are going to delete this role. Do you want to proceed?";
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove9_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnRoleID10.Value == "0")
                {
                    divRow10.Visible = false;
                    TextBox20.Text = TextBox21.Text = "";
                    HdnRoleID10.Value = "0";
                    FixButtons();
                }
                else
                {
                    bool existRoleToUser = SqlRoles.ExistRoleToSubAccount(vSession.User.Id, Convert.ToInt32(HdnRoleID10.Value), session);
                    if (existRoleToUser)
                    {
                        BtnRemoveRole.Visible = false;
                        LblMessageAlertTitle.Text = "Warning Message";
                        LblMessageAlertfMsg.Text = "You can not delete this role because it is assigned to user.";
                    }
                    else
                    {
                        HdnRoleID.Value = HdnRoleID10.Value;
                        BtnRemoveRole.Visible = true;
                        LblMessageAlertTitle.Text = "Information Message";
                        LblMessageAlertfMsg.Text = "You are going to delete this role. Do you want to proceed?";
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnRemoveRole_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (HdnRoleID.Value != "0")
                    {
                        bool deleted = false;

                        try
                        {
                            session.BeginTransaction();

                            deleted = SqlRoles.DeleteUserPermissionsRolesFormAction(vSession.User.Id, Convert.ToInt32(HdnRoleID.Value), session);
                            deleted = SqlRoles.DeleteUserPermissionsRole(vSession.User.Id, Convert.ToInt32(HdnRoleID.Value), session);

                            session.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        if (deleted)
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Role deleted successfully", MessageTypes.Success, true, true, true, true, false);

                            ResetFields();
                            LoadUserRoles();
                            GetRolesByUser();
                            //LoadUserRolesData();
                            LoadFormPermissions();
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Role could not be deleted. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                            throw new Exception(string.Format("User with ID: {0} tried to delete role with ID: {1}, at {2}, but it could not be deleted", vSession.User.Id, HdnRoleID.Value, DateTime.Now.ToString()));
                        }
                    }
                    else
                    {
                        //error can not delete tier
                        GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Role could not be deleted. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmationPopUp();", true);

                    UpdatePanelContent.Update();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Role could not be deleted. Please try again later!", MessageTypes.Error, true, true, true, true, false);

                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion

        #region Dropdown Lists

        protected void DrpRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                LoadFormPermissions();
                RdgFormPermissions.DataBind();
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

        #region CheckBoxes

        protected void CbxSelectAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox cbx = (CheckBox)sender;
                GridDataItem item = (GridDataItem)cbx.NamingContainer;

                CheckBox cbx2 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction2");
                CheckBox cbx3 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction3");
                CheckBox cbx4 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction4");
                CheckBox cbx5 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction5");
                CheckBox cbx6 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction6");
                CheckBox cbx7 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAction7");

                if (cbx2.Visible)
                    cbx2.Checked = cbx.Checked;

                if (cbx3.Visible)
                    cbx3.Checked = cbx.Checked;

                if (cbx4.Visible)
                    cbx4.Checked = cbx.Checked;

                if (cbx5.Visible)
                    cbx5.Checked = cbx.Checked;

                if (cbx6.Visible)
                    cbx6.Checked = cbx.Checked;

                if (cbx7.Visible)
                    cbx7.Checked = cbx.Checked;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion       
    }
}