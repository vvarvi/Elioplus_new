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
using System.Web.Services;
using System.Configuration;
using System.Net;

namespace WdS.ElioPlus.pages
{
    public partial class MAMLandingPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

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
                    ResetFields();
                    SetLinks();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void UpdateStrings()
        {

        }

        private void ResetFields()
        {
            UcMessageAlert.Visible = false;

            TbxFirstName.Text = "";
            TbxLastName.Text = "";
            TbxBusinessName.Text = "";
            TbxCompanyEmail.Text = "";
            TbxPhone.Text = "";
            TbxMessage.Text = "";
        }

        private void SetLinks()
        {
            //aLogo.HRef = aLogoBottom.HRef = ControlLoader.Default();
        }

        #endregion

        #region Buttons

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                UcMessageAlert.Visible = false;

                if (TbxFirstName.Text == "")
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Please fill your firstname", MessageTypes.Error, true, true, false, true, false);

                    return;
                }

                if (TbxLastName.Text == "")
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Please fill your lastname", MessageTypes.Error, true, true, false, true, false);

                    return;
                }

                if (TbxBusinessName.Text == "")
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Please fill your company name", MessageTypes.Error, true, true, false, true, false);

                    return;
                }

                if (TbxCompanyEmail.Text == "")
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Please fill your email address", MessageTypes.Error, true, true, false, true, false);

                    return;
                }

                if (TbxPhone.Text == "")
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Please fill your phone number", MessageTypes.Error, true, true, false, true, false);

                    return;
                }

                if (TbxMessage.Text == "")
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Please type your message", MessageTypes.Error, true, true, false, true, false);

                    return;
                }

                try
                {
                    EmailSenderLib.MAMContactElioplusRequest(TbxFirstName.Text, TbxLastName.Text, TbxBusinessName.Text, TbxCompanyEmail.Text, TbxPhone.Text, TbxMessage.Text, "", vSession.Lang, session);

                    ResetFields();

                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Your information has been successfully received, someone from our team will reach out concerning the next steps.", MessageTypes.Success, true, true, false, true, false);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControl(UcMessageAlert, "Sorry, something went wrong. Please try again later or contact us.", MessageTypes.Error, true, true, false, true, false);
            }
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFields();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            ResetFields();
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseSelfServiceModal();", true);
        }

        #endregion

        
    }
}