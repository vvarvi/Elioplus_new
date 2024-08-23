using System;
using System.Linq;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Web.UI;
using System.Collections.Generic;
using WdS.ElioPlus.Lib.Enums;
using System.IO;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Web;

namespace WdS.ElioPlus.Controls
{
    public partial class FullRegistrationPartner : System.Web.UI.UserControl
    {
        public Types UserType
        {
            get
            {
                if (ViewState["UserType"] != null)
                    return (Types)ViewState["UserType"];
                else
                    return Types.Resellers;
            }
            set
            {
                ViewState["UserType"] = value;
            }
        }

        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        private string VendorName
        {
            get
            {
                object value = ViewState["VendorName"];
                return value != null ? value.ToString() : "";
            }
            set
            {
                ViewState["VendorName"] = Regex.Replace(value, @"[^A-Za-z0-9]+", "_").Trim().ToLower();
            }
        }

        private int VendorID
        {
            get
            {
                object value = ViewState["VendorID"];
                return value != null ? (int)value : -1;
            }
            set
            {
                ViewState["VendorID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        #region Get Company Profile Data from Url

                        ElioUsers vendor = GlobalDBMethods.GetCompanyFromAbsoluteUrl(HttpContext.Current.Request.Url.AbsolutePath, session);
                        if (vendor != null)
                        {
                            VendorName = vendor.CompanyName;
                            VendorID = vendor.Id;
                        }
                        else
                            Response.Redirect(ControlLoader.SignUpPrm, false);

                        #endregion

                        LoadCountries();
                        UpdateStrings();

                        FixPage();
                    }
                }
                else
                {
                    string LoginUrl = (!string.IsNullOrEmpty(VendorName)) ? ControlLoader.LoginPartner.Replace("{CompanyName}", VendorName) : ControlLoader.Login;

                    Response.Redirect(LoginUrl, false);
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

        private void FixDataForCollaborationOrThirdPartyUserRegistration()
        {
            RadioButtonList rdBtnUserType = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");
            Label lblTypeInfo = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblTypeInfo");
            HtmlAnchor aDennyInvitation = (HtmlAnchor)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "aDennyInvitation");

            if (VendorID > -1)
            {
                if (SqlCollaboration.ExistVendorChannelPartnerCollaboration(VendorID, vSession.User.Id, session))
                {
                    #region Exist Collaboration

                    int pendingRequestsCount = 0;
                    if (vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.Collaboration) || vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty) || SqlCollaboration.HasInvitationRequestByStatus(vSession.User.Id, vSession.User.AccountStatus, vSession.User.CompanyType, CollaborateInvitationStatus.Pending.ToString(), out pendingRequestsCount, session))
                    {
                        bool isMasterVendorResellerUser = false;    //SqlCollaboration.IsMasterVendorResellerUser(vSession.User.Id, session);

                        if (isMasterVendorResellerUser)
                        {
                            rdBtnUserType.Enabled = false;
                            rdBtnUserType.SelectedValue = "1";

                            lblTypeInfo.Text = "You are invited to register as a " + Types.Vendors.ToString() + " company type";

                            UserType = Types.Vendors;
                        }
                        else
                        {
                            bool isPartnerVendorResellerUser = true;    //SqlCollaboration.IsPartnerVendorResellerUser(vSession.User.Id, session);

                            if (isPartnerVendorResellerUser || vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.Collaboration))
                            {
                                rdBtnUserType.Enabled = false;
                                rdBtnUserType.SelectedValue = "2";

                                UserType = Types.Resellers;
                            }

                            if (isPartnerVendorResellerUser)
                                lblTypeInfo.Text = "You must register as a " + ((isPartnerVendorResellerUser || vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty)) ? EnumHelper.GetDescription(Types.Resellers).ToString() : Types.Vendors.ToString()) + " in order to have access to " + VendorName + "'s partner portal.";
                        }

                        aDennyInvitation.Visible = false;   // true;
                        //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
                        //tbxCompanyEmail.Enabled = false;
                        //tbxCompanyEmail.Text = vSession.User.Email;
                    }

                    //LblInvitationMsg.Text = "Are you sure you want to delete collaboration Invitation?";

                    #endregion
                }
            }
            else
            {
                if (SqlCollaboration.ExistChannelPartnerInCollaboration(vSession.User.Id, session))
                {
                    #region Exist Collaboration

                    int pendingRequestsCount = 0;
                    if (vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.Collaboration) || vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty) || SqlCollaboration.HasInvitationRequestByStatus(vSession.User.Id, vSession.User.AccountStatus, vSession.User.CompanyType, CollaborateInvitationStatus.Pending.ToString(), out pendingRequestsCount, session))
                    {
                        bool isMasterVendorResellerUser = false;    //SqlCollaboration.IsMasterVendorResellerUser(vSession.User.Id, session);

                        if (isMasterVendorResellerUser)
                        {
                            rdBtnUserType.Enabled = false;
                            rdBtnUserType.SelectedValue = "1";

                            lblTypeInfo.Text = "You are invited to register as a " + Types.Vendors.ToString() + " company type";

                            UserType = Types.Vendors;
                        }
                        else
                        {
                            bool isPartnerVendorResellerUser = true;    //SqlCollaboration.IsPartnerVendorResellerUser(vSession.User.Id, session);

                            if (isPartnerVendorResellerUser || vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.Collaboration))
                            {
                                rdBtnUserType.Enabled = false;
                                rdBtnUserType.SelectedValue = "2";

                                UserType = Types.Resellers;
                            }

                            if (isPartnerVendorResellerUser)
                                lblTypeInfo.Text = "You must register as a " + ((isPartnerVendorResellerUser || vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty)) ? EnumHelper.GetDescription(Types.Resellers).ToString() : Types.Vendors.ToString()) + " in order to have access to " + VendorName + "'s partner portal.";
                        }

                        aDennyInvitation.Visible = false;   // true;
                        //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
                        //tbxCompanyEmail.Enabled = false;
                        //tbxCompanyEmail.Text = vSession.User.Email;
                    }

                    //LblInvitationMsg.Text = "Are you sure you want to delete collaboration Invitation?";

                    #endregion
                }
                else
                {
                    #region ThirdParty User

                    if (vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty))
                    {
                        lblTypeInfo.Text = "You are suggested to register as a Channel Partner company type";

                        aDennyInvitation.Visible = false;
                        //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
                        //tbxCompanyEmail.Enabled = false;
                        //tbxCompanyEmail.Text = vSession.User.Email;

                        LblInvitationMsg.Text = "Are you sure you want to delete suggestion as Channel Partner registration?";

                        rdBtnUserType.Enabled = true;
                        rdBtnUserType.SelectedValue = "2";

                        UserType = Types.Resellers;

                        #region Delete third party user all sub industries group items

                        try
                        {
                            GlobalDBMethods.ResetThirdPartyUserVerticals(vSession.User.Id, session);
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), string.Format("Reset third party user {0} sub industries group items failed at{1}, failed to do at registration step.", vSession.User.Id.ToString(), DateTime.Now));
                        }

                        #endregion
                    }

                    #endregion
                }
            }
        }

        protected void FixPage()
        {
            FixDataForCollaborationOrThirdPartyUserRegistration();

            ResetFields();
        }

        private void ResetFields()
        {
            divSuccess.Visible = false;
            divFailure.Visible = false;
            LblFailureMsg.Text = "";
            LblSuccessMsg.Text = "";
        }

        protected void RpbRegistrationSteps_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            int selectedIndex = RpbRegistrationSteps.SelectedItem.Index;
            if (selectedIndex == 1)
            {
                Panel pnlApi = (Panel)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "PnlApi");

                RadioButtonList rdBtnUserType = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");
                if (rdBtnUserType.SelectedItem.Value == "2" || rdBtnUserType.SelectedItem.Value == "1")
                {
                    pnlApi.Visible = rdBtnUserType.SelectedItem.Value == "2" ? false : true;
                }
            }
        }

        private void UpdateStrings()
        {
            RpbRegistrationSteps.Items[0].Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "2")).Text;
            RpbRegistrationSteps.Items[1].Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "3")).Text;

            Label lbl10 = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "Lbl10");
            lbl10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "1")).Text;
            Label label9 = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "Label9");
            label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "2")).Text;
            Label label2 = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "Label2");
            label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "5")).Text;
            Label label3 = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "Label3");
            label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "6")).Text;
            Label lblSelectImg = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblSelectImg");
            lblSelectImg.Text = "Select image";
            Label lblChangeImg = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblChangeImg");
            lblChangeImg.Text = "Change image";

            Image imgViewLogo = (Image)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "ImgViewLogo");
            imgViewLogo.ImageUrl = vSession.User.CompanyLogo != null ? vSession.User.CompanyLogo : "/images/no_logo3.jpg";
        }

        private void LoadCountries()
        {
            DropDownList ddlCountries = (DropDownList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "DdlCountries");
            ddlCountries.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Select Country";

            ddlCountries.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new ListItem();
                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                ddlCountries.Items.Add(item);
            }
        }

        private bool InValidData()
        {
            bool isError = false;

            return isError;
        }

        private bool CheckRegistrationData()
        {
            bool isError = false;

            #region Find Controls

            RadioButtonList rdBtnList = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");
            TextBox tbxCompanyName = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyName");
            //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
            TextBox tbxWebsite = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxWebsite");
            DropDownList ddlCountries = (DropDownList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "DdlCountries");
            TextBox tbxAddress = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxAddress");
            TextBox tbxCompanyPhone = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyPhone");

            CheckBoxList cb1 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb1");
            CheckBoxList cb2 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb2");
            CheckBoxList cb3 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb3");

            CheckBoxList cbSub1 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub1");
            CheckBoxList cbSub2 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub2");
            CheckBoxList cbSub3 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub3");
            CheckBoxList cbSub4 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub4");
            CheckBoxList cbSub5 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub5");
            CheckBoxList cbSub6 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub6");
            CheckBoxList cbSub7 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub7");
            CheckBoxList cbSub8 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub8");
            CheckBoxList cbSub9 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub9");
            CheckBoxList cbSub10 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub10");
            CheckBoxList cbSub11 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub11");
            CheckBoxList cbSub12 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub12");
            CheckBoxList cbSub13 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub13");
            CheckBoxList cbSub14 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub14");
            CheckBoxList cbSub15 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub15");
            CheckBoxList cbSub16 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub16");
            CheckBoxList cbSub17 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub17");
            CheckBoxList cbSub18 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub18");
            CheckBoxList cbSub19 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub19");
            CheckBoxList cbSub20 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub20");
            CheckBoxList cbSub21 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub21");
            CheckBoxList cbSub22 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub22");
            CheckBoxList cbSub23 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub23");
            CheckBoxList cbSub24 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub24");
            CheckBoxList cbSub25 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub25");
            CheckBoxList cbSub26 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub26");
            CheckBoxList cbSub27 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub27");
            CheckBoxList cbSub28 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub28");
            CheckBoxList cbSub29 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub29");
            CheckBoxList cbSub30 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub30");
            CheckBoxList cbSub31 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub31");
            CheckBoxList cbSub32 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub32");
            CheckBoxList cbSub33 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub33");
            CheckBoxList cbSub34 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub34");
            CheckBoxList cbSub35 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub35");
            CheckBoxList cbSub36 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub36");
            CheckBoxList cbSub37 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub37");
            CheckBoxList cbSub38 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub38");
            CheckBoxList cbSub39 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub39");
            CheckBoxList cbSub40 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub40");
            CheckBoxList cbSub41 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub41");
            CheckBoxList cbSub42 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub42");
            CheckBoxList cbSub43 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub43");
            CheckBoxList cbSub44 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub44");
            CheckBoxList cbSub45 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub45");
            CheckBoxList cbSub46 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub46");
            CheckBoxList cbSub47 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub47");

            CheckBoxList cb4 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb4");
            CheckBoxList cb5 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb5");
            CheckBoxList cb6 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb6");

            CheckBoxList cb7 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb7");
            CheckBoxList cb8 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb8");

            HtmlGenericControl divLogoFail = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divLogoFailure");
            divLogoFail.Visible = false;

            Label lblLogoFail = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblLogoFailure");
            Label lblLogoFailCont = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblLogoFailureContent");
            lblLogoFail.Text = string.Empty;
            lblLogoFailCont.Text = string.Empty;

            Label lblCompanyNameError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCompanyNameError");
            lblCompanyNameError.Text = string.Empty;
            //Label lblOfficialEmailError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblOfficialEmailError");
            //lblOfficialEmailError.Text = string.Empty;
            Label lblWeSiteError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblWeSiteError");
            lblWeSiteError.Text = string.Empty;
            Label lblCountryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCountryError");
            lblCountryError.Text = string.Empty;
            Label lblAddressError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblAddressError");
            lblAddressError.Text = string.Empty;

            Label lblIndustryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblIndustryError");
            lblIndustryError.Text = string.Empty;
            Label lblPartnerError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPartnerError");
            lblPartnerError.Text = string.Empty;
            Label lblMarketError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblMarketError");
            lblMarketError.Text = string.Empty;
            Label lblApiError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblApiError");
            lblApiError.Text = string.Empty;

            Label lblSubIndustryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblSubIndustryError");
            lblSubIndustryError.Text = string.Empty;

            Label lblOverViewError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblOverViewError");
            lblOverViewError.Text = string.Empty;
            Label lblDescriptionError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblDescriptionError");
            lblDescriptionError.Text = string.Empty;

            HtmlGenericControl divUserTypeError = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divUserTypeError");
            divUserTypeError.Visible = false;

            Label lblUserTypeError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblUserTypeError");
            lblUserTypeError.Text = string.Empty;

            Label lblUserTypeErrorContent = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblUserTypeErrorContent");
            lblUserTypeErrorContent.Text = string.Empty;

            #endregion

            #region Step 2

            if (rdBtnList.SelectedItem == null)
            {
                divUserTypeError.Visible = true;
                lblUserTypeError.Text = "Error! ";
                lblUserTypeErrorContent.Text = "You have to select your company type.";
                divUserTypeError.Focus();
                isError = true;
            }

            if (string.IsNullOrEmpty(tbxCompanyName.Text))
            {
                lblCompanyNameError.Text = "Please add company name";
                tbxCompanyName.Focus();
                isError = true;
            }
            else
            {
                if (Validations.ContainsSpecialChar(tbxCompanyName.Text))
                {
                    lblCompanyNameError.Text = "Company name contains special characters. Please add valid company name";
                    tbxCompanyName.Focus();
                    isError = true;
                }

                if (vSession.User.UserApplicationType != (int)UserApplicationType.ThirdParty)
                {
                    if (ddlCountries.SelectedItem.Value != "0")
                    {
                        if (Sql.IsCompanyNameRegisteredByCountry(tbxCompanyName.Text, ddlCountries.SelectedItem.Text, session))
                        {
                            lblCompanyNameError.Text = "User with this company name is already registered";
                            tbxCompanyName.Focus();
                            isError = true;
                        }
                    }
                }
            }

            //if (!string.IsNullOrEmpty(tbxCompanyEmail.Text))
            //{
            //    if (!Validations.IsEmail(tbxCompanyEmail.Text))
            //    {
            //        lblOfficialEmailError.Text = "Please enter a valid email";
            //        tbxCompanyEmail.Focus();
            //        isError = true;
            //    }

            //    if (Sql.ExistEmailToOtherUser(tbxCompanyEmail.Text, vSession.User.Id, session))
            //    {
            //        lblOfficialEmailError.Text = "The official email belongs to another user";
            //        tbxCompanyEmail.Focus();
            //        return true;
            //    }
            //}

            if (string.IsNullOrEmpty(tbxWebsite.Text))
            {
                lblWeSiteError.Text = "Please add website";
                tbxWebsite.Focus();
                isError = true;
            }
            else
            {
                if (tbxWebsite.Text.Split(' ').Length > 1)
                {
                    lblWeSiteError.Text = "Please add valid website";
                    tbxWebsite.Focus();
                    isError = true;
                }
            }

            if (ddlCountries.SelectedItem.Value == "0")
            {
                lblCountryError.Text = "Please select country";
                ddlCountries.Focus();
                isError = true;
            }
            else
            {
                if (vSession.User.UserApplicationType != (int)UserApplicationType.ThirdParty)
                {
                    if (Sql.IsCompanyNameRegisteredByCountry(tbxCompanyName.Text, ddlCountries.SelectedItem.Text, session))
                    {
                        lblCompanyNameError.Text = "User with this company name is already registered in this country";
                        tbxCompanyName.Focus();
                        isError = true;
                    }
                }
            }

            if (string.IsNullOrEmpty(tbxAddress.Text))
            {
                lblAddressError.Text = "Please add address";
                tbxAddress.Focus();
                isError = true;
            }

            if (string.IsNullOrEmpty(vSession.User.CompanyLogo))
            {
                divLogoFail.Visible = true;
                lblLogoFail.Text = "Error! ";
                lblLogoFailCont.Text = "You have to submit a logo for your company";
                divLogoFail.Focus();
                isError = true;
            }

            if (isError)
            {
                RpbRegistrationSteps.Items[0].Expanded = true;
                return isError;
            }

            #endregion

            #region Step 3

            bool hasSelectedItem = false;

            List<CheckBoxList> indutryList = new List<CheckBoxList>();
            indutryList.Add(cb1);
            indutryList.Add(cb2);
            indutryList.Add(cb3);

            hasSelectedItem = GlobalMethods.HasSelectedItemInCheckBoxList(indutryList);
            if (!hasSelectedItem)
            {
                lblIndustryError.Text = "Please select at least one industry category";
                Panel pnlIndustry = (Panel)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "PnlIndustry");
                pnlIndustry.Focus();
                isError = true;
            }

            if (!isError)
            {
                List<CheckBoxList> partnerList = new List<CheckBoxList>();
                partnerList.Add(cb4);
                partnerList.Add(cb5);
                partnerList.Add(cb6);

                hasSelectedItem = GlobalMethods.HasSelectedItemInCheckBoxList(partnerList);
                if (!hasSelectedItem)
                {
                    lblPartnerError.Text = "Please select at least one partner program";
                    Panel pnlPartner = (Panel)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "PnlPartner");
                    pnlPartner.Focus();
                    isError = true;
                }
            }

            if (!isError)
            {
                List<CheckBoxList> marketList = new List<CheckBoxList>();
                marketList.Add(cb7);
                marketList.Add(cb8);

                hasSelectedItem = GlobalMethods.HasSelectedItemInCheckBoxList(marketList);
                if (!hasSelectedItem)
                {
                    lblMarketError.Text = "Please select at least one market specialization";
                    Panel pnlMarket = (Panel)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "PnlMarket");
                    pnlMarket.Focus();
                    isError = true;
                }
            }


            if (isError)
            {
                RpbRegistrationSteps.Items[1].Expanded = true;
                return isError;
            }

            #endregion

            #region Step 4

            List<CheckBoxList> subIndutryList = new List<CheckBoxList>();
            subIndutryList.Add(cbSub1);
            subIndutryList.Add(cbSub2);
            subIndutryList.Add(cbSub3);
            subIndutryList.Add(cbSub4);
            subIndutryList.Add(cbSub5);
            subIndutryList.Add(cbSub6);
            subIndutryList.Add(cbSub7);
            subIndutryList.Add(cbSub8);
            subIndutryList.Add(cbSub9);
            subIndutryList.Add(cbSub10);
            subIndutryList.Add(cbSub11);
            subIndutryList.Add(cbSub12);
            subIndutryList.Add(cbSub13);
            subIndutryList.Add(cbSub14);
            subIndutryList.Add(cbSub15);
            subIndutryList.Add(cbSub16);
            subIndutryList.Add(cbSub17);
            subIndutryList.Add(cbSub18);
            subIndutryList.Add(cbSub19);
            subIndutryList.Add(cbSub20);
            subIndutryList.Add(cbSub21);
            subIndutryList.Add(cbSub22);
            subIndutryList.Add(cbSub23);
            subIndutryList.Add(cbSub24);
            subIndutryList.Add(cbSub25);
            subIndutryList.Add(cbSub26);
            subIndutryList.Add(cbSub27);
            subIndutryList.Add(cbSub28);
            subIndutryList.Add(cbSub29);
            subIndutryList.Add(cbSub30);
            subIndutryList.Add(cbSub31);
            subIndutryList.Add(cbSub32);
            subIndutryList.Add(cbSub33);
            subIndutryList.Add(cbSub34);
            subIndutryList.Add(cbSub35);
            subIndutryList.Add(cbSub36);
            subIndutryList.Add(cbSub37);
            subIndutryList.Add(cbSub38);
            subIndutryList.Add(cbSub39);
            subIndutryList.Add(cbSub40);
            subIndutryList.Add(cbSub41);
            subIndutryList.Add(cbSub42);
            subIndutryList.Add(cbSub43);
            subIndutryList.Add(cbSub44);
            subIndutryList.Add(cbSub45);
            subIndutryList.Add(cbSub46);
            subIndutryList.Add(cbSub47);

            hasSelectedItem = GlobalMethods.HasSelectedItemInCheckBoxList(subIndutryList);
            if (!hasSelectedItem)
            {
                lblSubIndustryError.Text = "Please select at least one industry vertical";
                lblSubIndustryError.Focus();
                isError = true;
            }

            if (isError)
            {
                RpbRegistrationSteps.Items[2].Expanded = true;
                return isError;
            }

            #endregion

            return isError;
        }

        private void GoToNextItem(bool check)
        {
            int selectedIndex = RpbRegistrationSteps.SelectedItem.Index;

            #region Find Controls

            RadioButtonList rdBtnList = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");
            TextBox tbxCompanyName = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyName");
            //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
            TextBox tbxWebsite = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxWebsite");
            DropDownList ddlCountries = (DropDownList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "DdlCountries");
            TextBox tbxAddress = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxAddress");
            TextBox tbxCompanyPhone = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyPhone");

            CheckBoxList cb1 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb1");
            CheckBoxList cb2 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb2");
            CheckBoxList cb3 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb3");

            CheckBoxList cbSub1 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub1");
            CheckBoxList cbSub2 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub2");
            CheckBoxList cbSub3 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub3");
            CheckBoxList cbSub4 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub4");
            CheckBoxList cbSub5 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub5");
            CheckBoxList cbSub6 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub6");
            CheckBoxList cbSub7 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub7");
            CheckBoxList cbSub8 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub8");
            CheckBoxList cbSub9 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub9");
            CheckBoxList cbSub10 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub10");
            CheckBoxList cbSub11 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub11");
            CheckBoxList cbSub12 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub12");
            CheckBoxList cbSub13 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub13");
            CheckBoxList cbSub14 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub14");
            CheckBoxList cbSub15 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub15");
            CheckBoxList cbSub16 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub16");
            CheckBoxList cbSub17 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub17");
            CheckBoxList cbSub18 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub18");
            CheckBoxList cbSub19 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub19");
            CheckBoxList cbSub20 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub20");
            CheckBoxList cbSub21 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub21");
            CheckBoxList cbSub22 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub22");
            CheckBoxList cbSub23 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub23");
            CheckBoxList cbSub24 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub24");
            CheckBoxList cbSub25 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub25");
            CheckBoxList cbSub26 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub26");
            CheckBoxList cbSub27 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub27");
            CheckBoxList cbSub28 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub28");
            CheckBoxList cbSub29 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub29");
            CheckBoxList cbSub30 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub30");
            CheckBoxList cbSub31 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub31");
            CheckBoxList cbSub32 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub32");
            CheckBoxList cbSub33 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub33");
            CheckBoxList cbSub34 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub34");
            CheckBoxList cbSub35 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub35");
            CheckBoxList cbSub36 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub36");
            CheckBoxList cbSub37 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub37");
            CheckBoxList cbSub38 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub38");
            CheckBoxList cbSub39 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub39");
            CheckBoxList cbSub40 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub40");
            CheckBoxList cbSub41 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub41");
            CheckBoxList cbSub42 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub42");
            CheckBoxList cbSub43 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub43");
            CheckBoxList cbSub44 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub44");
            CheckBoxList cbSub45 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub45");
            CheckBoxList cbSub46 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub46");
            CheckBoxList cbSub47 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub47");

            CheckBoxList cb4 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb4");
            CheckBoxList cb5 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb5");
            CheckBoxList cb6 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb6");

            CheckBoxList cb7 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb7");
            CheckBoxList cb8 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb8");

            CheckBoxList cb9 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb9");
            CheckBoxList cb10 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb10");
            CheckBoxList cb11 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb11");

            HtmlGenericControl divUserTypeError = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divUserTypeError");
            divUserTypeError.Visible = false;

            Label lblUserTypeError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblUserTypeError");
            lblUserTypeError.Text = string.Empty;

            Label lblUserTypeErrorContent = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblUserTypeErrorContent");
            lblUserTypeErrorContent.Text = string.Empty;

            Label label37 = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "Label37");
            Label label11 = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "Label11");

            TextBox tbxOverview = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxOverview");
            TextBox tbxDescription = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxDescription");

            Label lblCompanyNameError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCompanyNameError");
            lblCompanyNameError.Text = string.Empty;
            //Label lblOfficialEmailError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblOfficialEmailError");
            //lblOfficialEmailError.Text = string.Empty;
            Label lblWeSiteError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblWeSiteError");
            lblWeSiteError.Text = string.Empty;
            Label lblCountryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCountryError");
            lblCountryError.Text = string.Empty;
            Label lblAddressError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblAddressError");
            lblAddressError.Text = string.Empty;

            Label lblIndustryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblIndustryError");
            lblIndustryError.Text = string.Empty;
            Label lblPartnerError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPartnerError");
            lblPartnerError.Text = string.Empty;
            Label lblMarketError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblMarketError");
            lblMarketError.Text = string.Empty;
            Label lblApiError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblApiError");
            lblApiError.Text = string.Empty;

            Label lblSubIndustryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblSubIndustryError");
            lblSubIndustryError.Text = string.Empty;

            Label lblOverViewError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblOverViewError");
            lblOverViewError.Text = string.Empty;
            Label lblDescriptionError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblDescriptionError");
            lblDescriptionError.Text = string.Empty;

            Label label48 = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "Label48");

            Panel pnlApi = (Panel)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "PnlApi");

            RadioButtonList rdBtnUserType = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");

            #endregion

            switch (selectedIndex)
            {
                case 0:

                    if (check)
                    {
                        //FixDataForCollaborationOrThirdPartyUserRegistration();

                        #region Company Info

                        if (rdBtnList.SelectedItem == null)
                        {
                            divUserTypeError.Visible = true;
                            lblUserTypeError.Text = "Error! ";
                            lblUserTypeErrorContent.Text = "You have to select your company type.";
                            return;
                        }

                        if (string.IsNullOrEmpty(tbxCompanyName.Text))
                        {
                            lblCompanyNameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "2")).Text;
                            return;
                        }
                        else
                        {
                            if (Validations.ContainsSpecialChar(tbxCompanyName.Text))
                            {
                                lblCompanyNameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "24")).Text;
                                return;
                            }

                            if (vSession.User.UserApplicationType != (int)UserApplicationType.ThirdParty)
                            {
                                if (ddlCountries.SelectedItem.Value != "0")
                                {
                                    if (Sql.IsCompanyNameRegisteredByCountry(tbxCompanyName.Text, ddlCountries.SelectedItem.Text, session))
                                    {
                                        lblCompanyNameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "23")).Text;
                                        return;
                                    }
                                }
                            }
                        }

                        //if (!string.IsNullOrEmpty(tbxCompanyEmail.Text))
                        //{
                        //    if (!Validations.IsEmail(tbxCompanyEmail.Text))
                        //    {
                        //        lblOfficialEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "3")).Text;
                        //        return;
                        //    }
                        //}

                        if (string.IsNullOrEmpty(tbxWebsite.Text))
                        {
                            lblWeSiteError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "4")).Text;
                            return;
                        }
                        else
                        {
                            if (tbxWebsite.Text.Split(' ').Length > 1)
                            {
                                lblWeSiteError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "27")).Text;
                                return;
                            }
                        }

                        if (ddlCountries.SelectedItem.Value == "0" || string.IsNullOrEmpty(ddlCountries.SelectedItem.Text) || (ddlCountries.SelectedItem.Text != "" && ddlCountries.SelectedItem.Text == "Add Your Company Country"))
                        {
                            lblCountryError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "5")).Text;
                            return;
                        }
                        else
                        {
                            if (vSession.User.UserApplicationType != (int)UserApplicationType.ThirdParty)
                            {
                                if (Sql.IsCompanyNameRegisteredByCountry(tbxCompanyName.Text, ddlCountries.SelectedItem.Text, session))
                                {
                                    lblCompanyNameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "23")).Text;
                                    return;
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(tbxAddress.Text))
                        {
                            lblAddressError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "6")).Text;
                            return;
                        }

                        #endregion

                        # region Logo

                        HtmlGenericControl divLogoFail = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divLogoFailure");
                        divLogoFail.Visible = false;

                        Label lblLogoFail = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblLogoFailure");
                        Label lblLogoFailCont = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblLogoFailureContent");
                        lblLogoFail.Text = string.Empty;
                        lblLogoFailCont.Text = string.Empty;

                        string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"].ToString());
                        string logoTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"]).ToString();
                        int maxSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileLenght"]);

                        HtmlInputFile companyLogo = (HtmlInputFile)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "CompanyLogo");

                        var logo = companyLogo.PostedFile;
                        var logoName = logo.FileName;
                        var logoSize = logo.ContentLength;
                        var logoType = logo.ContentType;

                        string formattedName = "";

                        if (logoSize != 0)
                        {
                            if (logoType != "image/png" && logoType != "image/jpeg" && logoType != "image/jpg")
                            {
                                lblLogoFail.Text = "Error! ";
                                lblLogoFailCont.Text = "Image type must be .png, or .jpg";
                                divLogoFail.Visible = true;
                                divLogoFail.Focus();
                                return;
                            }

                            if (logoSize > maxSize)
                            {
                                lblLogoFail.Text = "Error! ";
                                lblLogoFailCont.Text = "Image size is too large. Max size is " + maxSize + " kb.";
                                divLogoFail.Visible = true;
                                divLogoFail.Focus();
                                return;
                            }

                            if (logoType == "image/png")
                            {
                                formattedName = ImageResize.ChangeFileName(logoName, ".png");
                            }
                            else if (logoType == "image/jpeg")
                            {
                                formattedName = ImageResize.ChangeFileName(logoName, ".jpeg");
                            }
                            else if (logoType == "image/jpg")
                            {
                                formattedName = ImageResize.ChangeFileName(logoName, ".jpg");
                            }

                            serverMapPathTargetFolder = serverMapPathTargetFolder + vSession.User.GuId + "\\";

                            #region Create File Directory

                            if (!Directory.Exists(serverMapPathTargetFolder))
                                Directory.CreateDirectory(serverMapPathTargetFolder);

                            #endregion

                            #region Delete old files in directory if exist

                            DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                            foreach (FileInfo fi in originaldir.GetFiles())
                            {
                                fi.Delete();
                            }

                            #endregion

                            logo.SaveAs(serverMapPathTargetFolder + formattedName);

                            #region Update User

                            vSession.User.CompanyLogo = logoTargetFolder + vSession.User.GuId + "/" + formattedName;

                            vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);

                            #endregion

                            Image imgViewLogo = (Image)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "ImgViewLogo");
                            imgViewLogo.ImageUrl = vSession.User.CompanyLogo;
                        }
                        else
                        {
                            //if (logoSize == 0)
                            //    vSession.User.CompanyLogo = "";

                            if (string.IsNullOrEmpty(vSession.User.CompanyLogo))
                            {
                                lblLogoFail.Text = "Error! ";
                                lblLogoFailCont.Text = "You have to select an image.";
                                divLogoFail.Visible = true;
                                divLogoFail.Focus();
                                return;
                            }
                        }

                        #endregion

                        #region Industry

                        cb1.Items.Clear();
                        cb2.Items.Clear();
                        cb3.Items.Clear();

                        cb1.Items.Clear();
                        cb2.Items.Clear();
                        cb3.Items.Clear();
                        label48.Visible = false;

                        int count = 0;
                        List<ElioIndustries> allIndustries = Sql.GetIndustries(session);
                        foreach (ElioIndustries industry in allIndustries)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserIndustry(vSession.User.Id, industry.Id, session);
                            if (count <= 13)
                            {
                                item.Text = industry.IndustryDescription;
                                item.Value = industry.Id.ToString();

                                cb1.Items.Add(item);
                                count++;
                            }
                            else if (count > 13 && count <= 27)
                            {
                                item.Text = industry.IndustryDescription;
                                item.Value = industry.Id.ToString();

                                cb2.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = industry.IndustryDescription;
                                item.Value = industry.Id.ToString();

                                cb3.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Partner

                        cb4.Items.Clear();
                        cb5.Items.Clear();
                        cb6.Items.Clear();

                        cb4.Items.Clear();
                        cb5.Items.Clear();
                        cb6.Items.Clear();
                        label48.Visible = false;

                        count = 0;
                        List<ElioPartners> allPartners = Sql.GetPartners(session);
                        foreach (ElioPartners partner in allPartners)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserPartner(vSession.User.Id, partner.Id, session);
                            if (count <= 2)
                            {
                                item.Text = partner.PartnerDescription;
                                item.Value = partner.Id.ToString();

                                cb4.Items.Add(item);
                                count++;
                            }
                            else if (count > 2 && count <= 4)
                            {
                                item.Text = partner.PartnerDescription;
                                item.Value = partner.Id.ToString();

                                cb5.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = partner.PartnerDescription;
                                item.Value = partner.Id.ToString();

                                cb6.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Market

                        cb7.Items.Clear();
                        cb8.Items.Clear();

                        cb7.Items.Clear();
                        cb8.Items.Clear();
                        label48.Visible = false;

                        count = 0;
                        List<ElioMarkets> allMarkets = Sql.GetMarkets(session);
                        foreach (ElioMarkets market in allMarkets)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserMarket(vSession.User.Id, market.Id, session);
                            if (count <= 1)
                            {
                                item.Text = market.MarketDescription;
                                item.Value = market.Id.ToString();

                                cb7.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = market.MarketDescription;
                                item.Value = market.Id.ToString();

                                cb8.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Api

                        if (rdBtnUserType.SelectedItem != null)
                        {
                            pnlApi.Visible = rdBtnUserType.SelectedItem.Value == "2" ? false : true;

                            cb9.Items.Clear();
                            cb10.Items.Clear();
                            cb11.Items.Clear();

                            count = 0;
                            List<ElioApies> allApies = Sql.GetApies(session);
                            foreach (ElioApies api in allApies)
                            {
                                ListItem item = new ListItem();

                                item.Selected = Sql.ExistUserApi(vSession.User.Id, api.Id, session);
                                if (count <= 1)
                                {
                                    item.Text = api.ApiDescription;
                                    item.Value = api.Id.ToString();

                                    cb9.Items.Add(item);
                                    count++;
                                }
                                else if (count > 1 && count <= 3)
                                {
                                    item.Text = api.ApiDescription;
                                    item.Value = api.Id.ToString();

                                    cb10.Items.Add(item);
                                    count++;
                                }
                                else
                                {
                                    item.Text = api.ApiDescription;
                                    item.Value = api.Id.ToString();

                                    cb11.Items.Add(item);
                                    count++;
                                }
                            }
                        }

                        #endregion                        
                    }

                    break;

                case 1:

                    if (check)
                    {
                        #region Company Characteristics

                        bool hasSelectedItem = false;

                        List<CheckBoxList> indutryList = new List<CheckBoxList>();
                        indutryList.Add(cb1);
                        indutryList.Add(cb2);
                        indutryList.Add(cb3);

                        hasSelectedItem = GlobalMethods.HasSelectedItemInCheckBoxList(indutryList);
                        if (!hasSelectedItem)
                        {
                            lblIndustryError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "8")).Text;
                            return;
                        }

                        List<CheckBoxList> partnerList = new List<CheckBoxList>();
                        partnerList.Add(cb4);
                        partnerList.Add(cb5);
                        partnerList.Add(cb6);

                        hasSelectedItem = GlobalMethods.HasSelectedItemInCheckBoxList(partnerList);
                        if (!hasSelectedItem)
                        {
                            lblPartnerError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "9")).Text;
                            return;
                        }

                        List<CheckBoxList> marketList = new List<CheckBoxList>();
                        marketList.Add(cb7);
                        marketList.Add(cb8);

                        hasSelectedItem = GlobalMethods.HasSelectedItemInCheckBoxList(marketList);
                        if (!hasSelectedItem)
                        {
                            lblMarketError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "10")).Text;
                            return;
                        }

                        #endregion

                        #region Sub Industries

                        cbSub1.Items.Clear();
                        cbSub2.Items.Clear();
                        cbSub3.Items.Clear();
                        cbSub4.Items.Clear();
                        cbSub5.Items.Clear();
                        cbSub6.Items.Clear();
                        cbSub7.Items.Clear();
                        cbSub8.Items.Clear();
                        cbSub9.Items.Clear();
                        cbSub10.Items.Clear();
                        cbSub11.Items.Clear();
                        cbSub12.Items.Clear();
                        cbSub13.Items.Clear();
                        cbSub14.Items.Clear();
                        cbSub15.Items.Clear();
                        cbSub16.Items.Clear();
                        cbSub17.Items.Clear();
                        cbSub18.Items.Clear();
                        cbSub19.Items.Clear();
                        cbSub20.Items.Clear();
                        cbSub21.Items.Clear();
                        cbSub22.Items.Clear();
                        cbSub23.Items.Clear();
                        cbSub24.Items.Clear();
                        cbSub25.Items.Clear();
                        cbSub26.Items.Clear();
                        cbSub27.Items.Clear();
                        cbSub28.Items.Clear();
                        cbSub29.Items.Clear();
                        cbSub30.Items.Clear();
                        cbSub31.Items.Clear();
                        cbSub32.Items.Clear();
                        cbSub33.Items.Clear();
                        cbSub34.Items.Clear();
                        cbSub35.Items.Clear();
                        cbSub36.Items.Clear();
                        cbSub37.Items.Clear();
                        cbSub38.Items.Clear();
                        cbSub39.Items.Clear();
                        cbSub40.Items.Clear();
                        cbSub41.Items.Clear();
                        cbSub42.Items.Clear();
                        cbSub43.Items.Clear();
                        cbSub44.Items.Clear();
                        cbSub45.Items.Clear();
                        cbSub46.Items.Clear();
                        cbSub47.Items.Clear();

                        int count = 0;

                        #region Group_1

                        List<ElioSubIndustriesGroupItems> salesMarketingGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.SalesMarketing), session);
                        foreach (ElioSubIndustriesGroupItems group in salesMarketingGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 5)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();
                                //item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                                cbSub1.Items.Add(item);
                                count++;
                            }
                            else if (count > 5 && count <= 11)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();
                                //item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                                cbSub2.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();
                                //item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                                cbSub3.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_2

                        count = 0;
                        List<ElioSubIndustriesGroupItems> customerManagementGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.CustomerManagement), session);
                        foreach (ElioSubIndustriesGroupItems group in customerManagementGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 1)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub4.Items.Add(item);
                                count++;
                            }
                            else if (count > 1 && count <= 3)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub5.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub6.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_3

                        count = 0;
                        List<ElioSubIndustriesGroupItems> projectManagementGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.ProjectManagement), session);
                        foreach (ElioSubIndustriesGroupItems group in projectManagementGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 0)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub7.Items.Add(item);
                                count++;
                            }
                            else if (count > 0 && count <= 1)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub8.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub9.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_4

                        count = 0;
                        List<ElioSubIndustriesGroupItems> operationsWorkflowGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.OperationsWorkflow), session);
                        foreach (ElioSubIndustriesGroupItems group in operationsWorkflowGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 6)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub10.Items.Add(item);
                                count++;
                            }
                            else if (count > 6 && count <= 13)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub11.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub12.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_5

                        count = 0;
                        List<ElioSubIndustriesGroupItems> trackingMeasurementGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.TrackingMeasurement), session);
                        foreach (ElioSubIndustriesGroupItems group in trackingMeasurementGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 1)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub13.Items.Add(item);
                                count++;
                            }
                            else if (count > 1 && count <= 3)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub14.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub15.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_6

                        count = 0;
                        List<ElioSubIndustriesGroupItems> accountingFinancialsGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.AccountingFinancials), session);
                        foreach (ElioSubIndustriesGroupItems group in accountingFinancialsGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 1)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub16.Items.Add(item);
                                count++;
                            }
                            else if (count > 1 && count <= 3)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub17.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub18.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_7

                        count = 0;
                        List<ElioSubIndustriesGroupItems> hRGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.HR), session);
                        foreach (ElioSubIndustriesGroupItems group in hRGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 2)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub19.Items.Add(item);
                                count++;
                            }
                            else if (count > 2 && count <= 4)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub20.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub21.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_8

                        count = 0;
                        List<ElioSubIndustriesGroupItems> webMobileSoftwareDevelopmentGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.WebMobileSoftwareDevelopment), session);
                        foreach (ElioSubIndustriesGroupItems group in webMobileSoftwareDevelopmentGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 2)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub22.Items.Add(item);
                                count++;
                            }
                            else if (count > 2 && count <= 5)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub23.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub24.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_9

                        count = 0;
                        List<ElioSubIndustriesGroupItems> iTInfrastructureGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.ITInfrastructure), session);
                        foreach (ElioSubIndustriesGroupItems group in iTInfrastructureGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 3)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub25.Items.Add(item);
                                count++;
                            }
                            else if (count > 3 && count <= 7)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub26.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub27.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_10

                        count = 0;
                        List<ElioSubIndustriesGroupItems> businessUtilitiesGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.BusinessUtilities), session);
                        foreach (ElioSubIndustriesGroupItems group in businessUtilitiesGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 2)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub28.Items.Add(item);
                                count++;
                            }
                            else if (count > 2 && count <= 5)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub29.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub30.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_11

                        count = 0;
                        List<ElioSubIndustriesGroupItems> securityBackupGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.DataSecurityGRC), session);
                        foreach (ElioSubIndustriesGroupItems group in securityBackupGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 4)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub31.Items.Add(item);
                                count++;
                            }
                            else if (count > 4 && count <= 9)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub32.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub33.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_12

                        count = 0;
                        List<ElioSubIndustriesGroupItems> designMultimediaGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.DesignMultimedia), session);
                        foreach (ElioSubIndustriesGroupItems group in designMultimediaGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 1)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub34.Items.Add(item);
                                count++;
                            }
                            else if (count > 1 && count <= 2)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub35.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub36.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_13

                        count = 0;
                        List<ElioSubIndustriesGroupItems> miscellaneousGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.Miscellaneous), session);
                        foreach (ElioSubIndustriesGroupItems group in miscellaneousGroup)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 1)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString();
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub37.Items.Add(item);
                                count++;
                            }
                            else if (count > 1 && count <= 2)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString();
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub38.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_14

                        count = 0;
                        List<ElioSubIndustriesGroupItems> unifiedCommunications = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.UnifiedCommunications), session);
                        foreach (ElioSubIndustriesGroupItems group in unifiedCommunications)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 3)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString();
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub39.Items.Add(item);
                                count++;
                            }
                            else if (count > 3 && count <= 7)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString();
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub40.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString();
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub41.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_15

                        count = 0;
                        List<ElioSubIndustriesGroupItems> cadPlm = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.CADPLM), session);
                        foreach (ElioSubIndustriesGroupItems group in cadPlm)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 2)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString();
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub42.Items.Add(item);
                                count++;
                            }
                            else if (count > 2 && count <= 5)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString();
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub43.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString();
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub44.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #region Group_16

                        count = 0;
                        List<ElioSubIndustriesGroupItems> hrdware = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.Hardware), session);
                        foreach (ElioSubIndustriesGroupItems group in hrdware)
                        {
                            ListItem item = new ListItem();

                            item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                            if (count <= 2)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString();
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub45.Items.Add(item);
                                count++;
                            }
                            else if (count > 2 && count <= 5)
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString();
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub46.Items.Add(item);
                                count++;
                            }
                            else
                            {
                                item.Text = group.Description;
                                item.Value = group.Id.ToString();
                                item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                                cbSub47.Items.Add(item);
                                count++;
                            }
                        }

                        #endregion

                        #endregion
                    }

                    break;

                case 2:

                    if (check)
                    {
                        #region Company Characteristics for Sub Categories

                        bool hasSelectedItem = false;

                        List<CheckBoxList> subIndutryList = new List<CheckBoxList>();
                        subIndutryList.Add(cbSub1);
                        subIndutryList.Add(cbSub2);
                        subIndutryList.Add(cbSub3);
                        subIndutryList.Add(cbSub4);
                        subIndutryList.Add(cbSub5);
                        subIndutryList.Add(cbSub6);
                        subIndutryList.Add(cbSub7);
                        subIndutryList.Add(cbSub8);
                        subIndutryList.Add(cbSub9);
                        subIndutryList.Add(cbSub10);
                        subIndutryList.Add(cbSub11);
                        subIndutryList.Add(cbSub12);
                        subIndutryList.Add(cbSub13);
                        subIndutryList.Add(cbSub14);
                        subIndutryList.Add(cbSub15);
                        subIndutryList.Add(cbSub16);
                        subIndutryList.Add(cbSub17);
                        subIndutryList.Add(cbSub18);
                        subIndutryList.Add(cbSub19);
                        subIndutryList.Add(cbSub20);
                        subIndutryList.Add(cbSub21);
                        subIndutryList.Add(cbSub22);
                        subIndutryList.Add(cbSub23);
                        subIndutryList.Add(cbSub24);
                        subIndutryList.Add(cbSub25);
                        subIndutryList.Add(cbSub26);
                        subIndutryList.Add(cbSub27);
                        subIndutryList.Add(cbSub28);
                        subIndutryList.Add(cbSub29);
                        subIndutryList.Add(cbSub30);
                        subIndutryList.Add(cbSub31);
                        subIndutryList.Add(cbSub32);
                        subIndutryList.Add(cbSub33);
                        subIndutryList.Add(cbSub34);
                        subIndutryList.Add(cbSub35);
                        subIndutryList.Add(cbSub36);
                        subIndutryList.Add(cbSub37);
                        subIndutryList.Add(cbSub38);
                        subIndutryList.Add(cbSub39);
                        subIndutryList.Add(cbSub40);
                        subIndutryList.Add(cbSub41);
                        subIndutryList.Add(cbSub42);
                        subIndutryList.Add(cbSub43);
                        subIndutryList.Add(cbSub44);
                        subIndutryList.Add(cbSub45);
                        subIndutryList.Add(cbSub46);
                        subIndutryList.Add(cbSub47);

                        hasSelectedItem = GlobalMethods.HasSelectedItemInCheckBoxList(subIndutryList);
                        if (!hasSelectedItem)
                        {
                            lblSubIndustryError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "21")).Text;
                            return;
                        }

                        if (!GlobalMethods.IsValidChecked(subIndutryList))
                        {
                            lblSubIndustryError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companycategoriesdata", "label", "3")).Text;
                            return;
                        }

                        #endregion
                    }

                    break;

                case 3:

                    if (check)
                    {
                        #region Overview/Description/Terms

                        if (string.IsNullOrEmpty(tbxOverview.Text))
                        {
                            lblOverViewError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "18")).Text;
                            return;
                        }
                        else
                        {
                            if (tbxOverview.Text.Length <= 400)
                            {
                                lblOverViewError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "25")).Text;
                                return;
                            }
                        }

                        if (string.IsNullOrEmpty(tbxDescription.Text))
                        {
                            lblDescriptionError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "19")).Text;
                            return;
                        }
                        else
                        {
                            if (tbxDescription.Text.Length <= 400)
                            {
                                lblDescriptionError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "26")).Text;
                                return;
                            }
                        }

                        #endregion
                    }

                    break;
            }

            #region Steps Messages

            RpbRegistrationSteps.Items[2].Text = (rdBtnUserType.SelectedItem.Value == "2")
                        ? "Step 3 of 4 - " + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "4")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "9")).Text
                        : "Step 3 of 4 - " + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "4")).Text;

            RpbRegistrationSteps.Items[3].Text = (rdBtnUserType.SelectedItem.Value == "1")
                                            ? "Step 4 of 4 - " + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "8")).Text
                                            : RpbRegistrationSteps.Items[3].Text = "Step 4 of 4 - " + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "7")).Text;

            label37.Text = (rdBtnUserType.SelectedItem.Value == "1")
                            ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "7")).Text
                            : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "8")).Text;

            label11.Text = (rdBtnUserType.SelectedItem.Value == "1")
                            ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "11")).Text
                            : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "12")).Text;

            #endregion

            if (selectedIndex != 3)
            {
                #region Expand next Item

                RpbRegistrationSteps.Items[selectedIndex + 1].Selected = true;
                RpbRegistrationSteps.Items[selectedIndex + 1].Expanded = true;
                RpbRegistrationSteps.Items[selectedIndex + 1].Visible = true;
                RpbRegistrationSteps.Items[selectedIndex].Expanded = false;

                #endregion
            }
            else
            {
                try
                {
                    if (vSession.User != null)
                    {
                        #region Save Data

                        bool isError = CheckRegistrationData();
                        if (!isError)
                        {
                            ElioUsers user = Sql.GetUserById(vSession.User.Id, session);
                            if (user != null)
                            {
                                try
                                {
                                    session.BeginTransaction();

                                    #region Personal Info

                                    user.CompanyType = (rdBtnList.SelectedItem.Value == (Convert.ToInt32(Types.Vendors)).ToString()) ? Types.Vendors.ToString() : EnumHelper.GetDescription(Types.Resellers).ToString();

                                    //if (tbxCompanyEmail.Text != string.Empty)
                                    //{
                                    //    user.OfficialEmail = tbxCompanyEmail.Text;
                                    //}

                                    user.Address = tbxAddress.Text;
                                    user.Phone = tbxCompanyPhone.Text;
                                    user.Country = ddlCountries.SelectedItem.Text;

                                    user.CompanyRegion = Sql.GetRegionByCountryId(Convert.ToInt32(ddlCountries.SelectedValue), session);

                                    user.WebSite = (tbxWebsite.Text.StartsWith("http://") || tbxWebsite.Text.StartsWith("https://")) ? tbxWebsite.Text.Trim() : "http://" + tbxWebsite.Text.Trim();
                                    user.CompanyName = tbxCompanyName.Text;
                                    user.Overview = GlobalMethods.FixStringEntryToParagraphs(tbxOverview.Text);
                                    user.Description = GlobalMethods.FixStringEntryToParagraphs(tbxDescription.Text);
                                    user.LastUpdated = DateTime.Now;

                                    if (user.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty))
                                    {
                                        #region If Has Person And/Or Company Data from ClearBit

                                        if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed) && user.IsPublic == Convert.ToInt32(AccountStatus.Public))
                                        {
                                            #region Set Person And/Or Company Data Not Public/Not Active

                                            try
                                            {
                                                ClearbitSql.SetClearbitPersonPublicActiveStatus(user.Id, (int)AccountPublicStatus.IsNotPublic, (int)ActiveStatus.IsNotActive, session);
                                                ClearbitSql.SetClearbitPersonCompanyPublicActiveStatus(user.Id, (int)AccountPublicStatus.IsNotPublic, (int)ActiveStatus.IsNotActive, session);
                                            }
                                            catch (Exception ex)
                                            {
                                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), string.Format("Error setting Clearbit person/company data not public/not active for user with ID: {0}, at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                            }

                                            #endregion
                                        }

                                        #endregion
                                    }

                                    //user.UserApplicationType = Convert.ToInt32(UserApplicationType.Elioplus);

                                    # endregion

                                    #region Industries

                                    List<CheckBoxList> cbxIndustryList = new List<CheckBoxList>();
                                    cbxIndustryList.Add(cb1);
                                    cbxIndustryList.Add(cb2);
                                    cbxIndustryList.Add(cb3);

                                    GlobalDBMethods.FixUserCategoriesByCheckBoxList(cbxIndustryList, Category.Industry, user.Id, true, session);

                                    #endregion

                                    #region Subindustries

                                    List<CheckBoxList> cbxSubIndustryList = new List<CheckBoxList>();
                                    cbxSubIndustryList.Add(cbSub1);
                                    cbxSubIndustryList.Add(cbSub2);
                                    cbxSubIndustryList.Add(cbSub3);
                                    cbxSubIndustryList.Add(cbSub4);
                                    cbxSubIndustryList.Add(cbSub5);
                                    cbxSubIndustryList.Add(cbSub6);
                                    cbxSubIndustryList.Add(cbSub7);
                                    cbxSubIndustryList.Add(cbSub8);
                                    cbxSubIndustryList.Add(cbSub9);
                                    cbxSubIndustryList.Add(cbSub10);
                                    cbxSubIndustryList.Add(cbSub11);
                                    cbxSubIndustryList.Add(cbSub12);
                                    cbxSubIndustryList.Add(cbSub13);
                                    cbxSubIndustryList.Add(cbSub14);
                                    cbxSubIndustryList.Add(cbSub15);
                                    cbxSubIndustryList.Add(cbSub16);
                                    cbxSubIndustryList.Add(cbSub17);
                                    cbxSubIndustryList.Add(cbSub18);
                                    cbxSubIndustryList.Add(cbSub19);
                                    cbxSubIndustryList.Add(cbSub20);
                                    cbxSubIndustryList.Add(cbSub21);
                                    cbxSubIndustryList.Add(cbSub22);
                                    cbxSubIndustryList.Add(cbSub23);
                                    cbxSubIndustryList.Add(cbSub24);
                                    cbxSubIndustryList.Add(cbSub25);
                                    cbxSubIndustryList.Add(cbSub26);
                                    cbxSubIndustryList.Add(cbSub27);
                                    cbxSubIndustryList.Add(cbSub28);
                                    cbxSubIndustryList.Add(cbSub29);
                                    cbxSubIndustryList.Add(cbSub30);
                                    cbxSubIndustryList.Add(cbSub31);
                                    cbxSubIndustryList.Add(cbSub32);
                                    cbxSubIndustryList.Add(cbSub33);
                                    cbxSubIndustryList.Add(cbSub34);
                                    cbxSubIndustryList.Add(cbSub35);
                                    cbxSubIndustryList.Add(cbSub36);
                                    cbxSubIndustryList.Add(cbSub37);
                                    cbxSubIndustryList.Add(cbSub38);
                                    cbxSubIndustryList.Add(cbSub39);
                                    cbxSubIndustryList.Add(cbSub40);
                                    cbxSubIndustryList.Add(cbSub41);
                                    cbxSubIndustryList.Add(cbSub42);
                                    cbxSubIndustryList.Add(cbSub43);
                                    cbxSubIndustryList.Add(cbSub44);
                                    cbxSubIndustryList.Add(cbSub45);
                                    cbxSubIndustryList.Add(cbSub46);
                                    cbxSubIndustryList.Add(cbSub47);

                                    GlobalDBMethods.FixUserCategoriesByCheckBoxList(cbxSubIndustryList, Category.SubIndustry, user.Id, true, session);

                                    #endregion

                                    #region Partners

                                    List<CheckBoxList> cbxPartnerList = new List<CheckBoxList>();
                                    cbxPartnerList.Add(cb4);
                                    cbxPartnerList.Add(cb5);
                                    cbxPartnerList.Add(cb6);

                                    GlobalDBMethods.FixUserCategoriesByCheckBoxList(cbxPartnerList, Category.Partner, user.Id, true, session);

                                    #endregion

                                    #region Markets

                                    List<CheckBoxList> cbxMarketList = new List<CheckBoxList>();
                                    cbxMarketList.Add(cb7);
                                    cbxMarketList.Add(cb8);

                                    GlobalDBMethods.FixUserCategoriesByCheckBoxList(cbxMarketList, Category.Market, user.Id, true, session);

                                    #endregion

                                    #region Apies

                                    List<CheckBoxList> cbxApiList = new List<CheckBoxList>();
                                    cbxApiList.Add(cb9);
                                    cbxApiList.Add(cb10);
                                    cbxApiList.Add(cb11);

                                    GlobalDBMethods.FixUserCategoriesByCheckBoxList(cbxApiList, Category.Api, user.Id, true, session);

                                    #endregion

                                    #region User Features

                                    ElioUsersFeatures freeFeatures = Sql.GetFeaturesbyUserType(Convert.ToInt32(BillingTypePacket.FreemiumPacketType), session);
                                    if (freeFeatures != null)
                                    {
                                        ElioUserPacketStatus userPackStatus = Sql.GetUserPacketStatusFeatures(user.Id, session);

                                        DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                        if (userPackStatus == null)
                                        {
                                            userPackStatus = new ElioUserPacketStatus();

                                            userPackStatus.UserId = user.Id;
                                            userPackStatus.PackId = freeFeatures.PackId;
                                            userPackStatus.UserBillingType = freeFeatures.UserBillingType;
                                            userPackStatus.AvailableLeadsCount = freeFeatures.TotalLeads;
                                            userPackStatus.AvailableMessagesCount = freeFeatures.TotalMessages;
                                            userPackStatus.AvailableConnectionsCount = freeFeatures.TotalConnections;
                                            userPackStatus.AvailableManagePartnersCount = freeFeatures.TotalManagePartners;
                                            userPackStatus.AvailableLibraryStorageCount = Convert.ToDecimal(freeFeatures.TotalLibraryStorage);
                                            userPackStatus.Sysdate = DateTime.Now;
                                            userPackStatus.LastUpdate = DateTime.Now;
                                            userPackStatus.StartingDate = DateTime.Now;
                                            userPackStatus.ExpirationDate = DateTime.Now.AddMonths(1);

                                            loader.Insert(userPackStatus);
                                        }
                                        else
                                        {
                                            userPackStatus.UserId = user.Id;
                                            userPackStatus.PackId = freeFeatures.PackId;
                                            userPackStatus.UserBillingType = freeFeatures.UserBillingType;
                                            userPackStatus.AvailableLeadsCount = freeFeatures.TotalLeads;
                                            userPackStatus.AvailableMessagesCount = freeFeatures.TotalMessages;
                                            userPackStatus.AvailableConnectionsCount = freeFeatures.TotalConnections;
                                            userPackStatus.AvailableManagePartnersCount = freeFeatures.TotalManagePartners;
                                            userPackStatus.AvailableLibraryStorageCount = Convert.ToDecimal(freeFeatures.TotalLibraryStorage);
                                            userPackStatus.Sysdate = DateTime.Now;
                                            userPackStatus.LastUpdate = DateTime.Now;
                                            userPackStatus.StartingDate = DateTime.Now;
                                            userPackStatus.ExpirationDate = DateTime.Now.AddMonths(1);

                                            loader.Update(userPackStatus);
                                        }
                                    }

                                    #endregion

                                    #region Update User / Session

                                    user.AccountStatus = Convert.ToInt32(AccountStatus.Completed);
                                    user.IsPublic = Convert.ToInt32(AccountStatus.Public);

                                    vSession.User = GlobalDBMethods.UpDateUser(user, session);

                                    #endregion

                                    #region Insert User Email Notifications Settings

                                    GlobalDBMethods.FixUserEmailNotificationsSettingsData(user, session);

                                    #endregion

                                    session.CommitTransaction();
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                                    Response.Redirect(ControlLoader.ErrorPage, false);
                                    return;
                                }

                                #region Redirect to Criteria Selection Page

                                //Response.Redirect(ControlLoader.Criteria(vSession.User), false);
                                Response.Redirect(ControlLoader.Dashboard(vSession.User, "find-new-partners-criteria"), false);

                                #endregion

                                Logger.Info("User {0}, completed successfully his registration from {1} as {2}", vSession.User.Id, vSession.User.Country, vSession.User.CompanyType);

                                #region Send Emails

                                try
                                {
                                    #region Send Invitation Email To Vendor

                                    //string confirmationLink = FileHelper.AddToPhysicalRootPath(Request) + "/free-sign-up?verificationViewID=" + user.GuId + "&type=" + user.UserApplicationType.ToString();
                                    ElioUsers vendor = Sql.GetUserById(VendorID, session);
                                    if (vendor != null)
                                    {
                                        EmailSenderLib.CollaborationInvitationEmailFromChannelPartner(user.UserApplicationType, vendor.Email.Trim(), vendor.CompanyName, user.CompanyLogo, user.CompanyName, Request, "en", session);
                                    }

                                    #endregion

                                    #region Comment out for Accepting Invitation

                                    //if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                    //{
                                    //    if (VendorID > 0)
                                    //    {
                                    //        ElioUserPacketStatus vendorPacketStatus = Sql.GetUserPacketStatusFeatures(VendorID, session);
                                    //        if (vendorPacketStatus != null)
                                    //        {
                                    //            if (vendorPacketStatus.AvailableManagePartnersCount > 0)
                                    //            {
                                    //                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                    //                vendorPacketStatus.AvailableManagePartnersCount--;
                                    //                vendorPacketStatus.LastUpdate = DateTime.Now;

                                    //                ElioCollaborationVendorsResellers collaboration = SqlCollaboration.GetCollaborationByVendorAndResellerId(VendorID, vSession.User.Id, session);
                                    //                if (collaboration != null)
                                    //                {
                                    //                    if (collaboration.InvitationStatus == CollaborateInvitationStatus.Pending.ToString())
                                    //                    {
                                    //                        DataLoader<ElioCollaborationVendorsResellers> colLoader = new DataLoader<ElioCollaborationVendorsResellers>(session);

                                    //                        collaboration.InvitationStatus = CollaborateInvitationStatus.Confirmed.ToString();
                                    //                        collaboration.LastUpdated = DateTime.Now;

                                    //                        ElioCollaborationVendorResellerInvitations invitation = SqlCollaboration.GetVendorResellerInvitationByResellerByVendResIdByStatusAndRecipientEmail(collaboration.Id, vSession.User.Email, CollaborateInvitationStatus.Pending.ToString(), session);
                                    //                        if (invitation != null)
                                    //                        {
                                    //                            if (invitation.InvitationStepDescription == CollaborateInvitationStatus.Pending.ToString())
                                    //                            {
                                    //                                invitation.InvitationStepDescription = CollaborateInvitationStatus.Confirmed.ToString();
                                    //                                invitation.LastUpdated = DateTime.Now;

                                    //                                DataLoader<ElioCollaborationVendorResellerInvitations> venResLoader = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                    //                                venResLoader.Update(invitation);
                                    //                            }
                                    //                        }

                                    //                        colLoader.Update(collaboration);

                                    //                        loader.Update(vendorPacketStatus);
                                    //                    }
                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //}

                                    #endregion

                                    #region Send Email for New FullRegistered User

                                    //EmailSenderLib.SendActivationEmailToFullRegisteredUser(vSession.User, vSession.Lang, session);
                                    EmailSenderLib.SendNotificationEmailForNewFullRegisteredUser(vSession.User, vSession.Lang, session);

                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }

                                #endregion
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region Redirect to Home Page

                        Response.Redirect(ControlLoader.Default(), false);

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                }
            }
        }

        public bool IsValid(RadAsyncUpload logo)
        {
            bool isValid = true;

            if (logo.UploadedFiles.Count == 0)
            {
                isValid = false;
            }

            return isValid;
        }

        #endregion

        #region Buttons

        protected void LnkBtnDennyInvitation_OnClick(object sender, EventArgs args)
        {
            try
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnClose_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields();

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmationPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnDennyInvitation_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ResetFields();

                    int pendingRequestsCount = 0;
                    if (SqlCollaboration.HasInvitationRequestByStatus(vSession.User.Id, vSession.User.AccountStatus, vSession.User.CompanyType, CollaborateInvitationStatus.Pending.ToString(), out pendingRequestsCount, session))
                    {
                        try
                        {
                            session.BeginTransaction();

                            SqlCollaboration.DeleteUserCollaborationAllItems(vSession.User.Id, Types.Resellers, session);

                            session.CommitTransaction();

                            divSuccess.Visible = true;
                            LblSuccessMsg.Text = "Invitation deleted successfully";
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                            divFailure.Visible = true;
                            LblFailureMsg.Text = "Invitation could not be deleted. Please try again later or contact with us";
                        }
                        finally
                        {
                            session.CloseConnection();
                        }
                    }

                    if (vSession.User.UserApplicationType == (int)UserApplicationType.ThirdParty)
                    {
                        try
                        {
                            EmailSenderLib.SendNotificationEmailForNewSimpleRegisteredThirdPartyToElioUser(vSession.User, vSession.Lang, session);
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString() + "User with ID:" + vSession.User.Id.ToString() + " dennied his third party type and tries to registered as Elio company.", ex.StackTrace.ToString());
                        }
                    }

                    vSession.User.UserApplicationType = (int)UserApplicationType.Elioplus;
                    vSession.User.LastUpdated = DateTime.Now;

                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                    loader.Update(vSession.User);

                    Response.Redirect(vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage, false);
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

        protected void BtnNext_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                GoToNextItem(true);
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

        protected void BtnBack_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                int selectedIndex = RpbRegistrationSteps.SelectedItem.Index;

                if (selectedIndex != 0)
                {
                    RpbRegistrationSteps.Items[selectedIndex - 1].Selected = true;
                    RpbRegistrationSteps.Items[selectedIndex - 1].Expanded = true;
                    RpbRegistrationSteps.Items[selectedIndex - 1].Visible = true;
                    RpbRegistrationSteps.Items[selectedIndex].Expanded = false;
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

        protected void BtnClearStep1_OnClick(object sender, EventArgs args)
        {
            try
            {
                TextBox tbxCompanyName = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyName");
                //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
                TextBox tbxWebsite = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxWebsite");
                DropDownList ddlCountries = (DropDownList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "DdlCountries");
                TextBox tbxAddress = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxAddress");
                TextBox tbxCompanyPhone = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyPhone");

                HtmlGenericControl divLogoFail = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divLogoFailure");
                divLogoFail.Visible = false;

                HtmlGenericControl divUserTypeError = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divUserTypeError");
                divUserTypeError.Visible = false;

                Label lblCompanyNameError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCompanyNameError");
                lblCompanyNameError.Text = string.Empty;
                //Label lblOfficialEmailError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblOfficialEmailError");
                //lblOfficialEmailError.Text = string.Empty;
                Label lblWeSiteError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblWeSiteError");
                lblWeSiteError.Text = string.Empty;
                Label lblCountryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCountryError");
                lblCountryError.Text = string.Empty;
                Label lblAddressError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblAddressError");
                lblAddressError.Text = string.Empty;

                HtmlInputFile companyLogo = (HtmlInputFile)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "CompanyLogo");

                tbxCompanyName.Text = string.Empty;
                //tbxCompanyEmail.Text = string.Empty;
                tbxWebsite.Text = string.Empty;
                ddlCountries.SelectedItem.Value = "0";
                tbxAddress.Text = string.Empty;
                tbxCompanyPhone.Text = string.Empty;
                companyLogo.Dispose();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnClearStep2_OnClick(object sender, EventArgs args)
        {
            try
            {
                Label lblIndustryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblIndustryError");
                lblIndustryError.Text = string.Empty;
                Label lblPartnerError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPartnerError");
                lblPartnerError.Text = string.Empty;
                Label lblMarketError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblMarketError");
                lblMarketError.Text = string.Empty;
                Label lblApiError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblApiError");
                lblApiError.Text = string.Empty;

                CheckBoxList cb1 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb1");
                CheckBoxList cb2 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb2");
                CheckBoxList cb3 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb3");

                CheckBoxList cb4 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb4");
                CheckBoxList cb5 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb5");
                CheckBoxList cb6 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb6");

                CheckBoxList cb7 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb7");
                CheckBoxList cb8 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb8");

                CheckBoxList cb9 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb9");
                CheckBoxList cb10 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb10");
                CheckBoxList cb11 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb11");

                List<CheckBoxList> nList = new List<CheckBoxList>();
                nList.Add(cb1);
                nList.Add(cb2);
                nList.Add(cb3);
                nList.Add(cb4);
                nList.Add(cb5);
                nList.Add(cb6);
                nList.Add(cb7);
                nList.Add(cb8);
                nList.Add(cb9);
                nList.Add(cb10);
                nList.Add(cb11);

                foreach (CheckBoxList nItem in nList)
                {
                    foreach (ListItem item in nItem.Items)
                    {
                        item.Selected = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnClearStep3_OnClick(object sender, EventArgs args)
        {
            try
            {
                Label lblSubIndustryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblSubIndustryError");
                lblSubIndustryError.Text = string.Empty;

                CheckBoxList cbSub1 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub1");
                CheckBoxList cbSub2 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub2");
                CheckBoxList cbSub3 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub3");
                CheckBoxList cbSub4 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub4");
                CheckBoxList cbSub5 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub5");
                CheckBoxList cbSub6 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub6");
                CheckBoxList cbSub7 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub7");
                CheckBoxList cbSub8 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub8");
                CheckBoxList cbSub9 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub9");
                CheckBoxList cbSub10 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub10");
                CheckBoxList cbSub11 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub11");
                CheckBoxList cbSub12 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub12");
                CheckBoxList cbSub13 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub13");
                CheckBoxList cbSub14 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub14");
                CheckBoxList cbSub15 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub15");
                CheckBoxList cbSub16 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub16");
                CheckBoxList cbSub17 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub17");
                CheckBoxList cbSub18 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub18");
                CheckBoxList cbSub19 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub19");
                CheckBoxList cbSub20 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub20");
                CheckBoxList cbSub21 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub21");
                CheckBoxList cbSub22 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub22");
                CheckBoxList cbSub23 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub23");
                CheckBoxList cbSub24 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub24");
                CheckBoxList cbSub25 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub25");
                CheckBoxList cbSub26 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub26");
                CheckBoxList cbSub27 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub27");
                CheckBoxList cbSub28 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub28");
                CheckBoxList cbSub29 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub29");
                CheckBoxList cbSub30 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub30");
                CheckBoxList cbSub31 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub31");
                CheckBoxList cbSub32 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub32");
                CheckBoxList cbSub33 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub33");
                CheckBoxList cbSub34 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub34");
                CheckBoxList cbSub35 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub35");
                CheckBoxList cbSub36 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub36");

                List<CheckBoxList> nList = new List<CheckBoxList>();
                nList.Add(cbSub1);
                nList.Add(cbSub2);
                nList.Add(cbSub3);
                nList.Add(cbSub4);
                nList.Add(cbSub5);
                nList.Add(cbSub6);
                nList.Add(cbSub7);
                nList.Add(cbSub8);
                nList.Add(cbSub9);
                nList.Add(cbSub10);
                nList.Add(cbSub11);
                nList.Add(cbSub12);
                nList.Add(cbSub13);
                nList.Add(cbSub14);
                nList.Add(cbSub15);
                nList.Add(cbSub16);
                nList.Add(cbSub17);
                nList.Add(cbSub18);
                nList.Add(cbSub19);
                nList.Add(cbSub20);
                nList.Add(cbSub21);
                nList.Add(cbSub22);
                nList.Add(cbSub23);
                nList.Add(cbSub24);
                nList.Add(cbSub25);
                nList.Add(cbSub26);
                nList.Add(cbSub27);
                nList.Add(cbSub28);
                nList.Add(cbSub29);
                nList.Add(cbSub30);
                nList.Add(cbSub31);
                nList.Add(cbSub32);
                nList.Add(cbSub33);
                nList.Add(cbSub34);
                nList.Add(cbSub35);
                nList.Add(cbSub36);

                foreach (CheckBoxList nItem in nList)
                {
                    foreach (ListItem item in nItem.Items)
                    {
                        item.Selected = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnClearStep4_OnClick(object sender, EventArgs args)
        {
            try
            {
                TextBox tbxOverview = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxOverview");
                TextBox tbxDescription = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxDescription");

                Label lblOverViewError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblOverViewError");
                lblOverViewError.Text = string.Empty;
                Label lblDescriptionError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblDescriptionError");
                lblDescriptionError.Text = string.Empty;

                tbxOverview.Text = string.Empty;
                tbxDescription.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    # region Delete uploaded file

                    DirectoryInfo originaldir = new DirectoryInfo(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"].ToString()) + vSession.User.GuId + "\\");
                    if (originaldir != null)
                    {
                        foreach (FileInfo fi in originaldir.GetFiles())
                        {
                            fi.Delete();
                        }
                    }

                    vSession.User.CompanyLogo = null;
                    vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);

                    # endregion

                    //Response.Redirect(ControlLoader.Default, false);
                    Response.Redirect(ControlLoader.Default(), false);
                }
                else
                    Response.Redirect(ControlLoader.Default(), false);
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

        #region Combo

        protected void RdBtnUserType_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                Panel pnlApi = (Panel)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "PnlApi");
                HtmlControl divProductDemoLink = (HtmlControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divProductDemoLink");
                divProductDemoLink.Visible = false;

                RadioButtonList rdBtnUserType = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");
                pnlApi.Visible = rdBtnUserType.SelectedItem.Value == Convert.ToInt32(Types.Resellers).ToString() ? false : true;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}