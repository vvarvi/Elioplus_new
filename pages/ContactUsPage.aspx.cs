using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Controls;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Net;
using System.Web.Services;
using System.Configuration;
using System.Collections.Generic;
using System.Net.Mail;

namespace WdS.ElioPlus.pages
{
    public partial class ContactUsPage : System.Web.UI.Page
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

        public ContactUsPage()
        {
            _captchaGenerator = new CaptchaGenerator();
        }

        private Random random = new Random();

        CaptchaGenerator _captchaGenerator;

        protected static string ReCaptcha_Key = ConfigurationManager.AppSettings["ReCaptcha_Key"].ToString();
        protected static string ReCaptcha_Secret = ConfigurationManager.AppSettings["ReCaptcha_Secret"].ToString();

        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    UpdateStrings();
                    //GenerateRandomCode();
                    //ImageCode.Text = "";
                    UcMessageAlert.Visible = false;

                    if (vSession.User != null)
                        LoadData();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void SetLinks()
        {
            aFaq.HRef = ControlLoader.Faq;
        }

        private void UpdateStrings()
        {
            //LblTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "contact", "label", "2")).Text;
            //LblTitleInfo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "contact", "label", "16")).Text;
            //BtnSend.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "contact", "label", "17")).Text;
            //BtnBack.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "contact", "label", "19")).Text;
            //LblContactInfo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "contact", "label", "4")).Text;
            //LblContactText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "contact", "label", "13")).Text;
            //LblElioplusEmail.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "contact", "label", "18")).Text;
            //LblFacebook.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "contact", "label", "7")).Text;
            ////LblContactPhone.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "contact", "label", "15")).Text;
            //LblAddress.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "contact", "label", "14")).Text;
            //LblFind.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "contact", "label", "12")).Text;
            //LblWarning.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "8")).Text;
            //LblSuccess.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "label", "4")).Text;
        }

        private void LoadData()
        {
            TbxName.Text = vSession.User.CompanyName;
            TbxEmail.Text = vSession.User.Email;
            TbxPhone.Text = vSession.User.Phone;
        }

        private bool CheckData()
        {
            UcMessageAlert.Visible = false;

            bool isError = false;

            if (BtnSend.Text == "START NEW MESSAGE")
            {
                Response.Redirect(ControlLoader.ContactUs, false);
                return isError = true;
            }

            if (string.IsNullOrEmpty(TbxEmail.Text))
            {
                GlobalMethods.ShowMessageControl(UcMessageAlert, "Please enter an email address", MessageTypes.Error, true, true, false, false, false);

                return isError = true;
            }
            else
            {
                if (Sql.IsBlackListedDomain(TbxEmail.Text, session) || Sql.IsBlackListedEmail(TbxEmail.Text, session))
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Access denied", MessageTypes.Error, true, true, false, false, false);

                    return isError = true;
                }

                if (!Validations.IsEmail(TbxEmail.Text))
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Please enter a valid email address", MessageTypes.Error, true, true, false, false, false);

                    return isError = true;
                }

                if (GlobalMethods.IsForbiddenDomain(TbxEmail.Text.Trim()))
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Please use a valid business email address", MessageTypes.Error, true, true, false, false, false);

                    return isError = true;
                }
            }
                

            if (string.IsNullOrEmpty(TbxName.Text))
            {
                GlobalMethods.ShowMessageControl(UcMessageAlert, "Please enter a name", MessageTypes.Error, true, true, false, false, false);

                return isError = true;
            }

            //if (string.IsNullOrEmpty(TbxPhone.Text))
            //{
            //    LblWarningMessage.Text = "Please enter a phone number";
            //    divWarningMessage.Visible = true;
            //    return isError = true;
            //}

            if (string.IsNullOrEmpty(TbxSubject.Text))
            {
                GlobalMethods.ShowMessageControl(UcMessageAlert, "Please enter a subject", MessageTypes.Error, true, true, false, false, false);

                return isError = true;
            }

            if (string.IsNullOrEmpty(TbxMessage.Text))
            {
                GlobalMethods.ShowMessageControl(UcMessageAlert, "Please enter your message", MessageTypes.Error, true, true, false, false, false);

                return isError = true;
            }

            return isError;
        }

        private void ClearMessageData()
        {
            if (vSession.User == null)
            {
                TbxName.Text = string.Empty;
                TbxEmail.Text = string.Empty;
                TbxPhone.Text = string.Empty;
            }

            TbxSubject.Text = string.Empty;
            TbxMessage.Text = string.Empty;

            UcMessageAlert.Visible = false;
        }

        private void GenerateRandomCode()
        {
            //string s = "";
            //for (int i = 0; i < 6; i++)
            //    s = String.Concat(s, this.random.Next(10).ToString());

            //string encryptedCode = _captchaGenerator.EncryptImageKey(s);
            //System.Diagnostics.Debug.WriteLine(encryptedCode);
            //CaptchaKey = encryptedCode;
            //captcha.ImageUrl = ResolveUrl("~/JpegImage.aspx?s=" + Server.UrlEncode(encryptedCode));
        }

        #endregion

        #region Buttons

        protected void BtnSend_OnClick(object sender, EventArgs e)
        {
            try
            {
                //divContact.Visible = true;
                //divCongratulations.Visible = false;

                session.OpenConnection();

                bool isError = CheckData();

                if (isError) return;

                try
                {
                    EmailSenderLib.ContactElioplus(TbxName.Text, TbxEmail.Text, TbxSubject.Text, TbxMessage.Text, TbxPhone.Text, vSession.Lang, session);
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    throw ex;
                }

                ClearMessageData();

                //divContact.Visible = false;
                //divCongratulations.Visible = true;
                GlobalMethods.ShowMessageControl(UcMessageAlert, "Your message was successfully sent.", MessageTypes.Success, true, true, false, false, false);

                BtnSend.Text = "START NEW MESSAGE";
                //ImageCode.Text = "";
                //BtnSend.Enabled = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ("An error occured during someone was trying to contact with us at " + DateTime.Now + ". Details: Cpmpany Name:" + TbxName.Text + ", Company Email: " + TbxEmail.Text + ", Subject: " + TbxSubject.Text + ", Message: " + TbxMessage.Text + ", Company Phone: " + TbxPhone.Text).ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControl(UcMessageAlert, "We are sorry, an unknown error occured and your message could not be sent. Please try again later.", MessageTypes.Error, true, true, false, false, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }
        
        #endregion
    }
}