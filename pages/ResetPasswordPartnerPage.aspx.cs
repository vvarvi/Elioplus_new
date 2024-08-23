using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Objects;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;

namespace WdS.ElioPlus.pages
{
    public partial class ResetPasswordPartnerPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        private string CaptchaKey
        {
            get
            {
                object value = ViewState["CaptchaKey"];
                return value != null ? value.ToString() : "";
            }
            set
            {
                ViewState["CaptchaKey"] = value;
            }
        }

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

        public ResetPasswordPartnerPage()
        {
            _captchaGenerator = new CaptchaGenerator();
        }

        private Random random = new Random();

        CaptchaGenerator _captchaGenerator;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    //Response.Redirect(ControlLoader.Default, false);
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        #region Get Company Profile Data from Url

                        ElioUsers vendor = GlobalDBMethods.GetCompanyFromAbsoluteUrl(HttpContext.Current.Request.Url.AbsolutePath, session);
                        if (vendor != null)
                        {
                            VendorName = vendor.CompanyName;
                            ImgElioplusLogo.ImageUrl = vendor.CompanyLogo;
                            aElioplusLogo.HRef = ControlLoader.Profile(vendor);
                            ImgElioplusLogo.ToolTip = vendor.CompanyName + " profile";
                            ImgElioplusLogo.AlternateText = vendor.CompanyName + " logo";
                            //LblTitle.Text = "Sign up now through " + vendor.CompanyName;                            
                        }
                        else
                            Response.Redirect(ControlLoader.SignUp, false);

                        #endregion

                        UpdateStrings();
                        SetLinks();
                        PageTitle();

                        GlobalMethods.ClearCriteriaSession(vSession, false);

                        UcMessageControlAlert.Visible = false;

                        divContact.Visible = true;
                        //LblInfo.Visible = true;
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

        private void PageTitle()
        {
            HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            pgTitle.InnerText = "Reset your password";
            metaDescription.Attributes["content"] = (!string.IsNullOrEmpty(VendorName)) ? "Reset your password to login to the " + VendorName + " partner portal" : "Reset your password to login to the vendor's partner portal";
            metaKeywords.Attributes["content"] = (!string.IsNullOrEmpty(VendorName)) ? VendorName + " password reset" : "Vendor's partner portal password reset";
        }

        private void SetLinks()
        {

        }

        private void UpdateStrings()
        {
            //LblTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "forgotpassword", "label", "2")).Text;
            //LblInfo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "forgotpassword", "label", "9")).Text;
            //LblLogin.Text = "Back to Log In"; // Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "forgotpassword", "label", "7")).Text;
            //BtnSend.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "forgotpassword", "label", "5")).Text;
        }

        private void GenerateRandomCode()
        {
            string s = "";
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, this.random.Next(10).ToString());

            string encryptedCode = _captchaGenerator.EncryptImageKey(s);
            System.Diagnostics.Debug.WriteLine(encryptedCode);
            CaptchaKey = encryptedCode;
            //captcha.ImageUrl = ResolveUrl("~/JpegImage.aspx?s=" + Server.UrlEncode(encryptedCode));
        }

        private bool CheckData()
        {
            UcMessageControlAlert.Visible = false;
            bool isError = false;

            if (string.IsNullOrEmpty(TbxEmail.Text))
            {
                string warningMessage = "Please enter your email";
                //divWarningMessage.Visible = true;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, warningMessage, MessageTypes.Error, true, true, true, true, false);

                return isError = true;
            }
            else
            {
                if (!Validations.IsEmail(TbxEmail.Text))
                {
                    string warningMessage = "This email is not valid";
                    //divWarningMessage.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, warningMessage, MessageTypes.Error, true, true, true, true, false);

                    return isError = true;
                }
            }

            return isError;
        }

        private void ClearMessageData()
        {
            TbxEmail.Text = string.Empty;
            UcMessageControlAlert.Visible = false;
        }

        #endregion

        #region Buttons

        protected void BtnSend_OnClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divContact.Visible = true;
                //LblInfo.Visible = true;

                UcMessageControlAlert.Visible = false;
                bool isError = CheckData();

                if (isError) return;

                ElioUsers user = Sql.GetUserByEmail(TbxEmail.Text, session);

                if (user != null)
                {
                    string newPassword = GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);

                    #region Send Email

                    EmailSenderLib.SendResetPasswordEmail(newPassword, TbxEmail.Text, vSession.Lang, session);

                    #endregion

                    if (newPassword != string.Empty)
                    {
                        #region Update new password

                        user.Password = newPassword;
                        user.PasswordEncrypted = MD5.Encrypt(user.Password);
                        user.LastUpdated = DateTime.Now;

                        user = GlobalDBMethods.UpDateUser(user, session);

                        #endregion

                        #region Update Sub-Account Password too if has

                        ElioUsersSubAccounts subAccount = Sql.GetSubAccountByEmail(TbxEmail.Text, session);
                        if (subAccount != null)
                        {
                            subAccount.Password = newPassword;
                            subAccount.PasswordEncrypted = MD5.Encrypt(newPassword);
                            subAccount.LastUpdated = DateTime.Now;

                            GlobalDBMethods.UpDateSubUser(subAccount, session);
                        }

                        #endregion
                    }
                    else
                    {
                        isError = true;
                        string warningMessage = "We ase sorry but something wrong happened. Please try again later or contact us.";
                        //divWarningMessage.Visible = true;
                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, warningMessage, MessageTypes.Error, true, true, true, true, false);

                        return;
                    }
                }
                else
                {
                    ElioUsersSubAccounts subUser = Sql.GetSubAccountByEmail(TbxEmail.Text, session);
                    if (subUser != null)
                    {
                        string newPassword = GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);

                        #region Send Email

                        EmailSenderLib.SendResetPasswordEmail(newPassword, TbxEmail.Text, vSession.Lang, session);

                        #endregion

                        if (newPassword != string.Empty)
                        {
                            #region Update new password

                            subUser.Password = newPassword;
                            subUser.PasswordEncrypted = MD5.Encrypt(subUser.Password);
                            subUser.LastUpdated = DateTime.Now;

                            GlobalDBMethods.UpDateSubUser(subUser, session);

                            #endregion
                        }
                        else
                        {
                            isError = true;
                            string warningMessage = "We ase sorry but something wrong happened. Please try again later or contact us.";
                            //divWarningMessage.Visible = true;
                            GlobalMethods.ShowMessageControl(UcMessageControlAlert, warningMessage, MessageTypes.Error, true, true, true, true, false);

                            return;
                        }
                    }
                    else
                    {
                        isError = true;
                        string warningMessage = "This email does not belong to a user of Elioplus. Please try again or contact us.";
                        //divWarningMessage.Visible = true;
                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, warningMessage, MessageTypes.Error, true, true, true, true, false);

                        return;
                    }
                }

                ClearMessageData();

                UcMessageControlAlert.Visible = false;
                divContact.Visible = false;
                //LblInfo.Visible = false;

                string successMessage = "Done! You will receive a new password in your email inbox!";
                //divSuccessMessage.Visible = true;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, successMessage, MessageTypes.Success, true, true, true, true, false);
            }
            catch (Exception ex)
            {
                string warningMessage = (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false") ? ex.Message : "We ase sorry but something wrong happened. Please try again later or contact us.";
                //divWarningMessage.Visible = true;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, warningMessage, MessageTypes.Error, true, true, true, true, false);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "User Email who request new password: " + TbxEmail.Text);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion
    }
}