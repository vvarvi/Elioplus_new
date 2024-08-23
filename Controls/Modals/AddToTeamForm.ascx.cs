using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.StripePayment;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Controls.AlertControls;
using System.Data;
using WdS.ElioPlus.Controls.Dashboard.AlertControls;

namespace WdS.ElioPlus.Controls.Modals
{
    public partial class AddToTeamForm : System.Web.UI.UserControl
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

        #region Methods

        public void LoadRoles()
        {
            CbxR1.Value = CbxR2.Value = CbxR3.Value = CbxR4.Value = CbxR5.Value = "0";
            CbxR6.Value = CbxR7.Value = CbxR8.Value = CbxR9.Value = CbxR10.Value = "0";

            List<ElioPermissionsUsersRoles> roles = SqlRoles.GetUserPermissionRoles(vSession.User.Id, "", session);

            if (roles.Count > 0)
            {
                if (roles.Count >= 1)
                {
                    CbxR1.Visible = true;
                    CbxR1.Value = roles[0].Id.ToString();
                    LblCbx1.Text = roles[0].RoleName;
                }
                else
                    div1.Visible = false;

                if (roles.Count >= 2)
                {
                    CbxR2.Visible = true;
                    CbxR2.Value = roles[1].Id.ToString();
                    LblCbx2.Text = roles[1].RoleName;
                }
                else
                    div2.Visible = false;

                if (roles.Count >= 3)
                {
                    CbxR3.Visible = true;
                    CbxR3.Value = roles[2].Id.ToString();
                    LblCbx3.Text = roles[2].RoleName;
                }
                else
                    div3.Visible = false;

                if (roles.Count >= 4)
                {
                    CbxR4.Visible = true;
                    CbxR4.Value = roles[3].Id.ToString();
                    LblCbx4.Text = roles[3].RoleName;
                }
                else
                    div4.Visible = false;

                if (roles.Count >= 5)
                {
                    CbxR5.Visible = true;
                    CbxR5.Value = roles[4].Id.ToString();
                    LblCbx5.Text = roles[4].RoleName;
                }
                else
                    div5.Visible = false;

                if (roles.Count >= 6)
                {
                    CbxR6.Visible = true;
                    CbxR6.Value = roles[5].Id.ToString();
                    LblCbx6.Text = roles[5].RoleName;
                }
                else
                    div6.Visible = false;

                if (roles.Count >= 7)
                {
                    CbxR7.Visible = true;
                    CbxR7.Value = roles[6].Id.ToString();
                    LblCbx7.Text = roles[6].RoleName;
                }
                else
                    div7.Visible = false;

                if (roles.Count >= 8)
                {
                    CbxR8.Visible = true;
                    CbxR8.Value = roles[7].Id.ToString();
                    LblCbx8.Text = roles[7].RoleName;
                }
                else
                    div8.Visible = false;

                if (roles.Count >= 9)
                {
                    CbxR9.Visible = true;
                    CbxR9.Value = roles[8].Id.ToString();
                    LblCbx9.Text = roles[8].RoleName;
                }
                else
                    div9.Visible = false;

                if (roles.Count >= 10)
                {
                    CbxR10.Visible = true;
                    CbxR10.Value = roles[9].Id.ToString();
                    LblCbx10.Text = roles[9].RoleName;
                }
                else
                    div10.Visible = false;

                //else
                //{
                //    CbxR1.Checked = CbxR2.Checked = CbxR3.Checked = CbxR4.Checked = CbxR5.Checked = false;
                //    CbxR6.Checked = CbxR7.Checked = CbxR8.Checked = CbxR9.Checked = CbxR10.Checked = false;
                //    CbxR1.Value = CbxR2.Value = CbxR3.Value = CbxR4.Value = CbxR5.Value = "0";
                //    CbxR6.Value = CbxR7.Value = CbxR8.Value = CbxR9.Value = CbxR10.Value = "0";                    
                //}
            }
            else
            {
                CbxR1.Checked = CbxR2.Checked = CbxR3.Checked = CbxR4.Checked = CbxR5.Checked = false;
                CbxR6.Checked = CbxR7.Checked = CbxR8.Checked = CbxR9.Checked = CbxR10.Checked = false;
                CbxR1.Value = CbxR2.Value = CbxR3.Value = CbxR4.Value = CbxR5.Value = "0";
                CbxR6.Value = CbxR7.Value = CbxR8.Value = CbxR9.Value = CbxR10.Value = "0";
            }

            //List<ElioTeamRoles> roles = Sql.GetTeamPublicRoles(session);

            //if (roles.Count >= 5)
            //{
            //    CbxR1.Value = roles[0].Id.ToString();
            //    //LblR1.Text = roles[0].RoleDescription;

            //    CbxR2.Value = roles[1].Id.ToString();
            //    //LblR2.Text = roles[1].RoleDescription;

            //    CbxR3.Value = roles[2].Id.ToString();
            //    //LblR3.Text = roles[2].RoleDescription;

            //    CbxR4.Value = roles[3].Id.ToString();
            //    //LblR4.Text = roles[3].RoleDescription;

            //    CbxR5.Value = roles[4].Id.ToString();
            //    //LblR5.Text = roles[4].RoleDescription;
            //}

            #region Old Way - To Delete

            //Cbx1.Items.Clear();
            //Cbx2.Items.Clear();

            //int i = 0;
            //foreach (ElioTeamRoles role in roles)
            //{
            //    ListItem item = new ListItem();
            //    item.Text = role.RoleDescription;
            //    item.Value = role.Id.ToString();
            //    i++;
            //    if (i <= roles.Count / 2)
            //    {                   
            //        Cbx1.Items.Add(item);
            //    }
            //    else
            //    {
            //        Cbx2.Items.Add(item);
            //    }
            //}

            #endregion
        }

        private void FixPage()
        {
            ResetFields(true);
            LoadRoles();
        }

        public void ResetFields(bool isFullReset)
        {
            divAddGeneralSuccess.Visible = false;
            divAddGeneralFailure.Visible = false;

            LblAddFailure.Text = string.Empty;
            LblAddSuccess.Text = string.Empty;

            if (isFullReset)
            {
                RtbxAddTeamEmail.Text = "";
                CbxR1.Checked = CbxR2.Checked = CbxR3.Checked = CbxR4.Checked = CbxR5.Checked =
                    CbxR6.Checked = CbxR7.Checked = CbxR8.Checked = CbxR9.Checked = CbxR10.Checked = false;
            }
        }

        #endregion

        #region Buttons

        protected void BtnAdd_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ResetFields(false);

                if (vSession.User != null)
                {
                    if (RtbxAddTeamEmail.Text == string.Empty)
                    {
                        divAddGeneralFailure.Visible = true;
                        LblAddFailure.Text = "Please add Email";
                        return;
                    }
                    else
                    {
                        if (!Validations.IsEmail(RtbxAddTeamEmail.Text))
                        {
                            divAddGeneralFailure.Visible = true;
                            LblAddFailure.Text = "Please add a valid Email";
                            return;
                        }
                        else
                        {
                            if (RtbxAddTeamEmail.Text == vSession.User.Email || RtbxAddTeamEmail.Text == vSession.User.OfficialEmail)
                            {
                                divAddGeneralFailure.Visible = true;
                                LblAddFailure.Text = "This email belongs to you";
                                return;
                            }

                            if (Sql.ExistEmailToSubAccount(vSession.User.Id, RtbxAddTeamEmail.Text, session))
                            {
                                divAddGeneralFailure.Visible = true;
                                LblAddFailure.Text = "This email already exists";
                                return;
                            }
                        }
                    }

                    int roleId = 0;
                    int selectedItemsCount = 0;

                    if (CbxR1.Checked)
                    {
                        selectedItemsCount++;
                        roleId = Convert.ToInt32(CbxR1.Value);
                    }
                    if (CbxR2.Checked)
                    {
                        selectedItemsCount++;
                        roleId = Convert.ToInt32(CbxR2.Value);
                    }
                    if (CbxR3.Checked)
                    {
                        selectedItemsCount++;
                        roleId = Convert.ToInt32(CbxR3.Value);
                    }
                    if (CbxR4.Checked)
                    {
                        selectedItemsCount++;
                        roleId = Convert.ToInt32(CbxR4.Value);
                    }
                    if (CbxR5.Checked)
                    {
                        selectedItemsCount++;
                        roleId = Convert.ToInt32(CbxR5.Value);
                    }
                    if (CbxR6.Checked)
                    {
                        selectedItemsCount++;
                        roleId = Convert.ToInt32(CbxR6.Value);
                    }
                    if (CbxR7.Checked)
                    {
                        selectedItemsCount++;
                        roleId = Convert.ToInt32(CbxR7.Value);
                    }
                    if (CbxR8.Checked)
                    {
                        selectedItemsCount++;
                        roleId = Convert.ToInt32(CbxR8.Value);
                    }
                    if (CbxR9.Checked)
                    {
                        selectedItemsCount++;
                        roleId = Convert.ToInt32(CbxR9.Value);
                    }
                    if (CbxR10.Checked)
                    {
                        selectedItemsCount++;
                        roleId = Convert.ToInt32(CbxR10.Value);
                    }

                    if (selectedItemsCount == 0)
                    {
                        divAddGeneralFailure.Visible = true;
                        LblAddFailure.Text = "Please select at least one team role";
                        roleId = 0;

                        return;
                    }
                    else if (selectedItemsCount > 1)
                    {
                        divAddGeneralFailure.Visible = true;
                        LblAddFailure.Text = "Select only one role";
                        roleId = 0;

                        return;
                    }

                    #region Old Way - To Delete

                    //foreach (ListItem item in Cbx1.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        selectedItemsCount++;
                    //        roleId = Convert.ToInt32(item.Value);
                    //    }

                    //    if (selectedItemsCount > 1)
                    //    {
                    //        divAddGeneralFailure.Visible = true;
                    //        LblAddFailure.Text = "Select only one role";
                    //        roleId = 0;

                    //        return;
                    //    }
                    //}

                    //foreach (ListItem item in Cbx2.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        selectedItemsCount++;
                    //        roleId = Convert.ToInt32(item.Value);
                    //    }

                    //    if (selectedItemsCount > 1)
                    //    {
                    //        divAddGeneralFailure.Visible = true;
                    //        LblAddFailure.Text = "Select only one role";
                    //        roleId = 0;

                    //        return;
                    //    }
                    //}

                    //if (selectedItemsCount == 0 || roleId == 0)
                    //{
                    //    divAddGeneralFailure.Visible = true;
                    //    LblAddFailure.Text = "Please select at least one team role";
                    //    roleId = 0;

                    //    return;
                    //}

                    #endregion

                    ElioUsersSubAccounts subAccount = new ElioUsersSubAccounts();

                    subAccount.UserId = vSession.User.Id;
                    subAccount.Email = RtbxAddTeamEmail.Text;
                    subAccount.TeamRoleId = roleId;
                    subAccount.Password = string.Empty;
                    subAccount.PasswordEncrypted = string.Empty;
                    subAccount.Guid = Guid.NewGuid().ToString();
                    subAccount.Sysdate = DateTime.Now;
                    subAccount.LastUpdated = DateTime.Now;
                    subAccount.IsConfirmed = 0;
                    subAccount.IsActive = 1;
                    subAccount.AccountStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                    subAccount.CommunityStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                    subAccount.FirstName = string.Empty;
                    subAccount.LastName = string.Empty;
                    subAccount.PersonalImage = string.Empty;
                    subAccount.Position = string.Empty;
                    subAccount.LinkedinUrl = string.Empty;
                    subAccount.LinkedinId = string.Empty;

                    string url = "";
                    if (HttpContext.Current.Request.IsLocal)
                        url = (!HttpContext.Current.Request.Url.Authority.StartsWith("http://")) ? "http://" : "";
                    else
                        url = (!HttpContext.Current.Request.Url.Authority.StartsWith("https://")) ? "https://" : "";

                    subAccount.ConfirmationUrl = url + HttpContext.Current.Request.Url.Authority + "/free-sign-up?verificationViewID=" + subAccount.Guid;

                    //subAccount.ConfirmationUrl = "https://www.elioplus.com/free-sign-up?verificationViewID=" + subAccount.Guid;

                    DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);
                    loader.Insert(subAccount);

                    ResetFields(true);
                    //LoadRoles();
                    //RtbxAddTeamEmail.Text = string.Empty;
                    //divAddGeneralSuccess.Visible = true;
                    //LblAddSuccess.Text = "Your invitation was successfully delivered!";

                    //Response.Redirect(ControlLoader.Dashboard(vSession.User, "team"), false);

                    //RadWindow rwndAddteam = (RadWindow)ControlFinder.FindControlBackWards(this, "RwndAddteam");
                    //RadAjaxManager.GetCurrent(Page).ResponseScripts.Add(String.Format("$find('{0}').show();", rwndAddteam.ClientID));

                    try
                    {
                        EmailSenderLib.InvitationEmail(subAccount.Email, vSession.User.CompanyName, subAccount.ConfirmationUrl, vSession.Lang, session);
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseInvitationPopUp();", true);
                    divAddGeneralSuccess.Visible = true;
                    LblAddSuccess.Text = "Your invitation was successfully delivered!";

                    Repeater rpt = (Repeater)ControlFinder.FindControlBackWards(this, "RdgResults");
                    if (rpt != null)
                    {
                        //List<ElioUsersSubAccountsIJRolesIJUsers> subAccounts = Sql.GetUserSubAccountsIJRolesIJUsers(vSession.User.Id, 0, session);
                        List<ElioUsersSubAccountsIJPermissionsRolesIJUsers> subAccounts = SqlRoles.GetUserSubAccountsIJRolesIJUsers(vSession.User.Id, 0, 0, "", session);

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

                            rpt.Visible = true;
                            rpt.DataSource = table;
                            rpt.DataBind();

                            UpdatePanel pnl = (UpdatePanel)ControlFinder.FindControlBackWards(this, "UpdatePanel2");
                            if (pnl != null)
                                pnl.Update();
                        }
                    }

                    DAMessageAlertControl ucMessageAlert = (DAMessageAlertControl)ControlFinder.FindControlBackWards(this, "UcMessageAlert");
                    DAMessageAlertControl ucMessageAlertTop = (DAMessageAlertControl)ControlFinder.FindControlBackWards(this, "UcMessageAlertTop");
                    if (ucMessageAlert != null && ucMessageAlertTop != null)
                    {
                        GlobalMethods.ShowMessageAlertControlDA(ucMessageAlert, "Your invitation was successfully delivered!", MessageTypes.Success, true, true, true, true, false);
                        GlobalMethods.ShowMessageAlertControlDA(ucMessageAlertTop, "Your invitation was successfully delivered!", MessageTypes.Success, true, true, true, true, false);
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

        protected void BtnAddCancelMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ResetFields(true);
                LoadRoles();

                ScriptManager.RegisterStartupScript(Page, GetType(), "Close Modal Popup", "CloseInvitationPopUp();", true);
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