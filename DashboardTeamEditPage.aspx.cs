using System;
using System.Collections.Generic;
using System.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.ImagesHelper;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus
{
    public partial class DashboardTeamEditPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        private static int maxFileLenght = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileLenght"]);

        public int SubAccountViewID
        {
            get
            {
                return ViewState["subAccountViewID"] == null ? 0 : (int)ViewState["subAccountViewID"];
            }
            set
            {
                ViewState["subAccountViewID"] = value;
            }
        }

        public bool IsCbx1Ckecked { get; set; }
        public bool IsCbx2Ckecked { get; set; }
        public bool IsCbx3Ckecked { get; set; }
        public bool IsCbx4Ckecked { get; set; }
        public bool IsCbx5Ckecked { get; set; }
        public bool IsCbx6Ckecked { get; set; }
        public bool IsCbx7Ckecked { get; set; }
        public bool IsCbx8Ckecked { get; set; }
        public bool IsCbx9Ckecked { get; set; }
        public bool IsCbx10Ckecked { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                //scriptManager.RegisterPostBackControl(RbtnBack);

                if (!IsPostBack)
                {
                    FixPage();

                    if (Request.QueryString["subAccountViewID"] != null)
                    {
                        SubAccountViewID = Convert.ToInt32(Session[Request.QueryString["subAccountViewID"]]);
                        if (SubAccountViewID > 0)
                        {
                            LoadData(SubAccountViewID);
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "team"), false);
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

        #region Methods

        private void FixPage()
        {
            //divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

            UpdateStrings();
            SetLinks();
            FixButtons();
        }

        private void FixButtons()
        {
            BtnComplete.Visible = aRbtnDelete.Visible = BtnClearMessage.Visible = vSession.LoggedInSubAccountRoleID == 0 || vSession.IsAdminRole;
        }

        private void SetLinks()
        {
            aRbtnBack.HRef = ControlLoader.Dashboard(vSession.User, "team");
        }

        private void LoadData(int subAccountId)
        {
            //ElioUsersSubAccountsIJRolesIJUsers subAccount = Sql.GetSubAccountIJRolesIJUsersById(subAccountId, session);
            ElioUsersSubAccountsIJPermissionsRolesIJUsers subAccount = SqlRoles.GetSubAccountIJRolesIJUsersById(subAccountId, session);

            if (subAccount != null)
            {
                RtbxEmail.Text = subAccount.Email;
                RtbxLastName.Text = subAccount.LastName;
                RtbxFirstName.Text = subAccount.FirstName;
                RtbxJobPosition.Text = subAccount.Position;
                CbxActive.Checked = (subAccount.IsActive == 1) ? true : false;

                LoadRoles(subAccount.UserId, subAccount.TeamRoleId);
            }
            else
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "team"), false);
        }

        private void UpdateStrings()
        {
            //LblDashboard.Text = "Dashboard";
            //LblDashPage.Text = "Edit Team Account";
            //LblMaxFileContent.Text = "* Max file input " + maxFileLenght / 1000 + " bytes";
        }

        private void ResetFields(bool errorsOnly)
        {
            if (!errorsOnly)
            {

            }

            UcMessage.Visible = false;
        }

        public bool IsValidData()
        {
            bool isValid = true;

            ResetMsgControls(false);

            if (RtbxPassword.Text != string.Empty)
            {
                if (RtbxPassword.Text.Length < 8)
                {
                    GlobalMethods.ShowMessageControlDA(UcMessage, "Password must be at least 8 characters", MessageTypes.Error, true, true, true, true, false);
                    
                    return isValid = false;
                }

                if (!Validations.IsPasswordCharsValid(RtbxPassword.Text))
                {
                    GlobalMethods.ShowMessageControlDA(UcMessage, "Password contains invalid characters", MessageTypes.Error, true, true, true, true, false);
                    
                    return isValid = false;
                }

                if (RtbxRetypePassword.Text == string.Empty)
                {
                    GlobalMethods.ShowMessageControlDA(UcMessage, "Please retype password", MessageTypes.Error, true, true, true, true, false);
                    
                    return isValid = false;
                }
                else
                {
                    if (RtbxPassword.Text != RtbxRetypePassword.Text)
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessage, "Passwords do not match", MessageTypes.Error, true, true, true, true, false);
                        
                        return isValid = false;
                    }
                }
            }

            if (RtbxLastName.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControlDA(UcMessage, "Please add your Last Name", MessageTypes.Error, true, true, true, true, false);
                
                return isValid = false;
            }

            if (RtbxFirstName.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControlDA(UcMessage, "Please add your First Name", MessageTypes.Error, true, true, true, true, false);
                
                return isValid = false;
            }

            if (RtbxJobPosition.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControlDA(UcMessage, "Please add your Job Position", MessageTypes.Error, true, true, true, true, false);
                
                return isValid = false;
            }

            int selectedItemsCount = 0;

            //HtmlInputCheckBox cbxRl1 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "CbxRl1");
            //HtmlInputCheckBox cbxRl2 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "CbxRl2");
            //HtmlInputCheckBox cbxRl3 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "CbxRl3");
            //HtmlInputCheckBox cbxRl4 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "CbxRl4");
            //HtmlInputCheckBox cbxRl5 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "CbxRl5");

            if (CbxRl1.Checked)
                selectedItemsCount++;

            if (CbxRl2.Checked)
                selectedItemsCount++;

            if (CbxRl3.Checked)
                selectedItemsCount++;

            if (CbxRl4.Checked)
                selectedItemsCount++;

            if (CbxRl5.Checked)
                selectedItemsCount++;

            if (CbxRl6.Checked)
                selectedItemsCount++;

            if (CbxRl7.Checked)
                selectedItemsCount++;

            if (CbxRl8.Checked)
                selectedItemsCount++;

            if (CbxRl9.Checked)
                selectedItemsCount++;

            if (CbxRl10.Checked)
                selectedItemsCount++;

            if (selectedItemsCount == 0)
            {
                GlobalMethods.ShowMessageControlDA(UcMessage, "Select at least one role", MessageTypes.Error, true, true, true, true, false);
                return isValid = false;
            }
            else if (selectedItemsCount > 1)
            {
                GlobalMethods.ShowMessageControlDA(UcMessage, "Please only one team role", MessageTypes.Error, true, true, true, true, false);
                return isValid = false;
            }

            return isValid;
        }

        private void ResetMsgControls(bool clearAll)
        {
            UcMessage.Visible = UcPopUpConfirmationMessageAlert.Visible = false;

            if (clearAll)
            {
                RtbxEmail.Text = string.Empty;
                RtbxPassword.Text = string.Empty;
                RtbxRetypePassword.Text = string.Empty;
                RtbxLastName.Text = string.Empty;
                RtbxFirstName.Text = string.Empty;
                RtbxJobPosition.Text = string.Empty;

                if (Request.QueryString["subAccountViewID"] != null)
                {
                    SubAccountViewID = Convert.ToInt32(Session[Request.QueryString["subAccountViewID"]]);
                    if (SubAccountViewID > 0)
                    {
                        ElioUsersSubAccounts subAccount = Sql.GetSubAccountById(SubAccountViewID, session);
                        if (subAccount != null)
                            LoadRoles(subAccount.UserId, 0);
                    }
                }
            }

            //LblPasswordError.Text = string.Empty;
            //LblRetypePasswordError.Text = string.Empty;
            //LblLastNameError.Text = string.Empty;
            //LblFirstNameError.Text = string.Empty;
            //LblJobPositionError.Text = string.Empty;
        }

        public bool IsImgValid(RadAsyncUpload logo)
        {
            bool isValid = true;

            if (logo.UploadedFiles.Count == 0)
            {
                isValid = false;
            }

            return isValid;
        }

        private void LoadRoles(int masterUserId, int userRoleId)
        {
            IsCbx1Ckecked = IsCbx2Ckecked = IsCbx3Ckecked = IsCbx4Ckecked = IsCbx5Ckecked = false;
            IsCbx6Ckecked = IsCbx7Ckecked = IsCbx8Ckecked = IsCbx9Ckecked = IsCbx10Ckecked = false;

            CbxRl1.Value = CbxRl2.Value = CbxRl3.Value = CbxRl4.Value = CbxRl5.Value = "0";
            CbxRl6.Value = CbxRl7.Value = CbxRl8.Value = CbxRl9.Value = CbxRl10.Value = "0";

            List<ElioPermissionsUsersRoles> roles = SqlRoles.GetUserPermissionRoles(masterUserId, "", session);

            if (roles.Count > 0)
            {
                if (roles.Count >= 1)
                {
                    CbxRl1.Visible = true;
                    CbxRl1.Value = roles[0].Id.ToString();
                    LblCbx1.Text = roles[0].RoleName;
                    CbxRl1.Checked = IsCbx1Ckecked = userRoleId == roles[0].Id;
                }
                else
                    CbxRl1.Visible = false;

                if (roles.Count >= 2)
                {
                    CbxRl2.Visible = true;
                    CbxRl2.Value = roles[1].Id.ToString();
                    LblCbx2.Text = roles[1].RoleName;
                    CbxRl2.Checked = IsCbx2Ckecked = userRoleId == roles[1].Id;
                }
                else
                    CbxRl2.Visible = false;

                if (roles.Count >= 3)
                {
                    CbxRl3.Visible = true;
                    CbxRl3.Value = roles[2].Id.ToString();
                    LblCbx3.Text = roles[2].RoleName;
                    CbxRl3.Checked = IsCbx3Ckecked = userRoleId == roles[2].Id;
                }
                else
                    CbxRl3.Visible = false;

                if (roles.Count >= 4)
                {
                    CbxRl4.Visible = true;
                    CbxRl4.Value = roles[3].Id.ToString();
                    LblCbx4.Text = roles[3].RoleName;
                    CbxRl4.Checked = IsCbx4Ckecked = userRoleId == roles[3].Id;
                }
                else
                    CbxRl4.Visible = false;

                if (roles.Count >= 5)
                {
                    CbxRl5.Visible = true;
                    CbxRl5.Value = roles[4].Id.ToString();
                    LblCbx5.Text = roles[4].RoleName;
                    CbxRl5.Checked = IsCbx5Ckecked = userRoleId == roles[4].Id;
                }
                else
                    CbxRl5.Visible = false;

                if (roles.Count >= 6)
                {
                    CbxRl6.Visible = true;
                    CbxRl6.Value = roles[5].Id.ToString();
                    LblCbx6.Text = roles[5].RoleName;
                    CbxRl6.Checked = IsCbx6Ckecked = userRoleId == roles[5].Id;
                }
                else
                    div6.Visible = false;

                if (roles.Count >= 7)
                {
                    CbxRl7.Visible = true;
                    CbxRl7.Value = roles[6].Id.ToString();
                    LblCbx7.Text = roles[6].RoleName;
                    CbxRl7.Checked = IsCbx7Ckecked = userRoleId == roles[6].Id;
                }
                else
                    div7.Visible = false;

                if (roles.Count >= 8)
                {
                    CbxRl8.Visible = true;
                    CbxRl8.Value = roles[7].Id.ToString();
                    LblCbx8.Text = roles[7].RoleName;
                    CbxRl8.Checked = IsCbx8Ckecked = userRoleId == roles[7].Id;
                }
                else
                    div8.Visible = false;

                if (roles.Count >= 9)
                {
                    CbxRl9.Visible = true;
                    CbxRl9.Value = roles[8].Id.ToString();
                    LblCbx9.Text = roles[8].RoleName;
                    CbxRl9.Checked = IsCbx9Ckecked = userRoleId == roles[8].Id;
                }
                else
                    div9.Visible = false;

                if (roles.Count >= 10)
                {
                    CbxRl10.Visible = true;
                    CbxRl10.Value = roles[9].Id.ToString();
                    LblCbx10.Text = roles[9].RoleName;
                    CbxRl10.Checked = IsCbx10Ckecked = userRoleId == roles[9].Id;
                }
                else
                    div10.Visible = false;
            }
            else
            {
                CbxRl1.Checked = CbxRl2.Checked = CbxRl3.Checked = CbxRl4.Checked = CbxRl5.Checked = false;
                CbxRl6.Checked = CbxRl7.Checked = CbxRl8.Checked = CbxRl9.Checked = CbxRl10.Checked = false;
                CbxRl1.Value = CbxRl2.Value = CbxRl3.Value = CbxRl4.Value = CbxRl5.Value = "0";
                CbxRl6.Value = CbxRl7.Value = CbxRl8.Value = CbxRl9.Value = CbxRl10.Value = "0";
                IsCbx1Ckecked = IsCbx2Ckecked = IsCbx3Ckecked = IsCbx4Ckecked = IsCbx5Ckecked = false;
                IsCbx6Ckecked = IsCbx7Ckecked = IsCbx8Ckecked = IsCbx9Ckecked = IsCbx10Ckecked = false;
                div1.Visible = div2.Visible = div3.Visible = div4.Visible = div5.Visible = div6.Visible = div7.Visible = div8.Visible = div9.Visible = div10.Visible = false;
            }

            //List<ElioTeamRoles> roles = Sql.GetTeamPublicRoles(session);
            //if (roles.Count > 0)
            //{
            //    if (userRoleId == 1)
            //    {
            //        IsCbx1Ckecked = true;
            //        CbxRl1.Checked = true;
            //        CbxRl1.Value = "1";
            //    }
            //    else if (userRoleId == 2)
            //    {
            //        IsCbx2Ckecked = true;
            //        CbxRl2.Checked = true;
            //        CbxRl2.Value = "1";
            //    }
            //    else if (userRoleId == 3)
            //    {
            //        IsCbx3Ckecked = true;
            //        CbxRl3.Checked = true;
            //        CbxRl3.Value = "1";
            //    }
            //    else if (userRoleId == 4)
            //    {
            //        IsCbx4Ckecked = true;
            //        CbxRl4.Checked = true;
            //        CbxRl4.Value = "1";
            //    }
            //    else if (userRoleId == 5)
            //    {
            //        IsCbx5Ckecked = true;
            //        CbxRl5.Checked = true;
            //        CbxRl5.Value = "1";
            //    }
            //    else if (userRoleId == 6)
            //    {
            //        IsCbx6Ckecked = true;
            //        CbxRl6.Checked = true;
            //        CbxRl6.Value = "1";
            //    }
            //    else if (userRoleId == 7)
            //    {
            //        IsCbx7Ckecked = true;
            //        CbxRl7.Checked = true;
            //        CbxRl7.Value = "1";
            //    }
            //    else if (userRoleId == 8)
            //    {
            //        IsCbx8Ckecked = true;
            //        CbxRl8.Checked = true;
            //        CbxRl8.Value = "1";
            //    }
            //    else if (userRoleId == 9)
            //    {
            //        IsCbx9Ckecked = true;
            //        CbxRl9.Checked = true;
            //        CbxRl9.Value = "1";
            //    }
            //    else if (userRoleId == 10)
            //    {
            //        IsCbx10Ckecked = true;
            //        CbxRl10.Checked = true;
            //        CbxRl10.Value = "1";
            //    }
            //    else
            //    {
            //        CbxRl1.Checked = CbxRl2.Checked = CbxRl3.Checked = CbxRl4.Checked = CbxRl5.Checked = false;
            //        CbxRl6.Checked = CbxRl7.Checked = CbxRl8.Checked = CbxRl9.Checked = CbxRl10.Checked = false;
            //        CbxRl1.Value = CbxRl2.Value = CbxRl3.Value = CbxRl4.Value = CbxRl5.Value = "0";
            //        CbxRl6.Value = CbxRl7.Value = CbxRl8.Value = CbxRl9.Value = CbxRl10.Value = "0";
            //        IsCbx1Ckecked = IsCbx2Ckecked = IsCbx3Ckecked = IsCbx4Ckecked = IsCbx5Ckecked = false;
            //        IsCbx6Ckecked = IsCbx7Ckecked = IsCbx8Ckecked = IsCbx9Ckecked = IsCbx10Ckecked = false;
            //    }
            //}

            //ScriptManager.RegisterStartupScript(this, GetType(), "UpdateCheckBoxes", "UpdateCheckBoxes();", true);
        }

        # endregion

        #region Grids

        #endregion

        #region Buttons

        protected void BtnComplete_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (Request.QueryString["subAccountViewID"] != null)
                    {
                        SubAccountViewID = Convert.ToInt32(Session[Request.QueryString["subAccountViewID"]]);
                        if (SubAccountViewID > 0)
                        {
                            ElioUsersSubAccounts subUser = Sql.GetSubAccountById(SubAccountViewID, session);
                            if (subUser != null)
                            {
                                if (IsValidData())
                                {
                                    if (RtbxPassword.Text != string.Empty)
                                    {
                                        #region Change Password

                                        subUser.Password = RtbxPassword.Text;
                                        subUser.PasswordEncrypted = MD5.Encrypt(subUser.Password);

                                        #endregion
                                    }

                                    //HtmlInputCheckBox cbxRl1 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "CbxRl1");
                                    //HtmlInputCheckBox cbxRl2 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "CbxRl2");
                                    //HtmlInputCheckBox cbxRl3 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "CbxRl3");
                                    //HtmlInputCheckBox cbxRl4 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "CbxRl4");
                                    //HtmlInputCheckBox cbxRl5 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "CbxRl5");

                                    int selectedTeamRoleId = 0;

                                    if (CbxRl1.Checked)
                                        selectedTeamRoleId = Convert.ToInt32(CbxRl1.Value);
                                    else if (CbxRl2.Checked)
                                        selectedTeamRoleId = Convert.ToInt32(CbxRl2.Value);
                                    else if (CbxRl3.Checked)
                                        selectedTeamRoleId = Convert.ToInt32(CbxRl3.Value);
                                    else if (CbxRl4.Checked)
                                        selectedTeamRoleId = Convert.ToInt32(CbxRl4.Value);
                                    else if (CbxRl5.Checked)
                                        selectedTeamRoleId = Convert.ToInt32(CbxRl5.Value);
                                    else if (CbxRl6.Checked)
                                        selectedTeamRoleId = Convert.ToInt32(CbxRl6.Value);
                                    else if (CbxRl7.Checked)
                                        selectedTeamRoleId = Convert.ToInt32(CbxRl7.Value);
                                    else if (CbxRl8.Checked)
                                        selectedTeamRoleId = Convert.ToInt32(CbxRl8.Value);
                                    else if (CbxRl9.Checked)
                                        selectedTeamRoleId = Convert.ToInt32(CbxRl9.Value);
                                    else
                                        selectedTeamRoleId = Convert.ToInt32(CbxRl10.Value);

                                    if (selectedTeamRoleId == 0)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessage, "Please select one role!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }

                                    #region Update Sub Account

                                    subUser.LastName = RtbxLastName.Text;
                                    subUser.FirstName = RtbxFirstName.Text;
                                    subUser.Position = RtbxJobPosition.Text;
                                    subUser.LastUpdated = DateTime.Now;
                                    subUser.AccountStatus = Convert.ToInt32(AccountStatus.Completed);
                                    subUser.CommunityProfileCreated = DateTime.Now;
                                    subUser.CommunityProfileLastUpdated = DateTime.Now;
                                    subUser.CommunityStatus = Convert.ToInt32(AccountStatus.Completed);
                                    subUser.IsActive = (CbxActive.Checked) ? 1 : 0;
                                    subUser.TeamRoleId = selectedTeamRoleId;

                                    DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);
                                    loader.Update(subUser);

                                    GlobalMethods.ShowMessageControlDA(UcMessage, "Your sub account is updated successfully!", MessageTypes.Success, true, true, true, true, false);

                                    #endregion
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessage, "Sub Account can not updated. Please try again later or contact us!", MessageTypes.Error, true, true, true, true, false);
                        return;
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

        protected void BtnClearMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ResetMsgControls(true);
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

        protected void RbtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    UcPopUpConfirmationMessageAlert.Visible = UcMessage.Visible = false;

                    if (Request.QueryString["subAccountViewID"] != null)
                    {
                        SubAccountViewID = Convert.ToInt32(Session[Request.QueryString["subAccountViewID"]]);
                        if (SubAccountViewID > 0)
                        {
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmPopUp();", true);
                        }
                        else
                            GlobalMethods.ShowMessageControlDA(UcMessage, "Sub account can not be deleted. Please try again later or contact us!", MessageTypes.Error, true, true, true, true, false);
                    }
                    else
                        GlobalMethods.ShowMessageControlDA(UcMessage, "Sub account can not be deleted. Please try again later or contact us!", MessageTypes.Error, true, true, true, true, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnConfDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ResetMsgControls(false);

                if (vSession.User != null)
                {
                    if (Request.QueryString["subAccountViewID"] != null)
                    {
                        SubAccountViewID = Convert.ToInt32(Session[Request.QueryString["subAccountViewID"]]);
                        if (SubAccountViewID > 0)
                        {
                            ResetMsgControls(true);
                            Sql.DeleteSubAccountById(SubAccountViewID, session);

                            GlobalMethods.ShowMessageControlDA(UcPopUpConfirmationMessageAlert, "Sub account deleted successfully.", MessageTypes.Success, true, true, true, true, false);
                            //Response.Redirect(ControlLoader.Dashboard(vSession.User, "team"), false);

                            BtnComplete.Visible = BtnClearMessage.Visible = aRbtnDelete.Visible = false;

                        }
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessage, "Error! Sub account could not be deleted. Try again later or contact us.", MessageTypes.Error, true, true, true, true, false);
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);

                        Logger.DetailedError(Request.Url.ToString(), string.Format("User {0} tried to delete sub account but subAccount Id was 0", vSession.User.Id.ToString()));
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

        #region Upload Logo

        protected void PersonalImage_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                vSession.SuccessfullSubAccountPersonalImageUpload = false;

                if (vSession.User != null)
                {
                    if (SubAccountViewID > 0)
                    {
                        ElioUsersSubAccounts subUser = Sql.GetSubAccountById(SubAccountViewID, session);
                        if (subUser != null)
                        {
                            e.IsValid = IsImgValid(PersonalImage);
                            if (!e.IsValid)
                            {
                                vSession.SuccessfullSubAccountPersonalImageUpload = false;
                                return;
                            }

                            vSession.SuccessfullSubAccountPersonalImageUpload = UpLoadImage.UpLoadSubAccountPersonalImage(subUser, Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["SubAccountPersonalImageTargetFolder"].ToString()), e, session);
                        }
                        else
                        {
                            vSession.SuccessfullSubAccountPersonalImageUpload = false;
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "team"), false);
                            return;
                        }
                    }
                    else
                    {
                        vSession.SuccessfullSubAccountPersonalImageUpload = false;
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "team"), false);
                        return;
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
    }
}