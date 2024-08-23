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
using WdS.ElioPlus.Controls.AlertControls;

namespace WdS.ElioPlus.pages
{
    public partial class ReferralSoftwarePage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    FixPage();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void FixPage()
        {
            UcMessageAlertEmailTop.Visible = false;
            //TbxEmailAddressTop.Text = string.Empty;

            aSignUp.Visible = aSignUpB.Visible = aSignUpC.Visible = vSession.User == null;

            SetLinks();
        }

        private void SetLinks()
        {
            aSignUp.HRef = aSignUpB.HRef = aSignUpC.HRef = ControlLoader.SignUp;
        }

        private void SendEmail(string emailAddress, MessageAlertControl control)
        {
            control.Visible = true;
            control.HasClose = true;
            control.ShowImg = true;
            control.ShowLnkBtnRgstr = false;
            control.IsLong = true;

            if (emailAddress == "")
            {
                control.MessageType = MessageTypes.Error;
                control.Message = "Fill your email address";
                //UpdatePanel6.Update();
                return;
            }
            else
            {
                if (!Validations.IsValidEmail(emailAddress))
                {
                    control.MessageType = MessageTypes.Error;
                    control.Message = "Your email address is not valid";
                    //UpdatePanel6.Update();
                    return;
                }
            }

            EmailSenderLib.ContactElioplus("", emailAddress, "Referral Software", string.Format("A user send his email: {0} address from referral software page at {1} in order to get in touch with him. Please do.", emailAddress, DateTime.Now.ToShortDateString()), "", vSession.Lang, session);

            control.MessageType = MessageTypes.Success;
            control.Message = "Your email address was successfully sent";
            //UpdatePanel6.Update();
        }

        #endregion

        protected void aSubmitTop_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlertEmailTop.Visible = false;

                //SendEmail(TbxEmailAddressTop.Text, UcMessageAlertEmailTop);

                //TbxEmailAddressTop.Text = string.Empty;
            }
            catch (Exception ex)
            {
                UcMessageAlertEmailTop.Visible = true;
                UcMessageAlertEmailTop.HasClose = true;
                UcMessageAlertEmailTop.ShowImg = true;
                UcMessageAlertEmailTop.ShowLnkBtnRgstr = false;
                UcMessageAlertEmailTop.IsLong = true;
                UcMessageAlertEmailTop.MessageType = MessageTypes.Error;
                UcMessageAlertEmailTop.Message = "Your email could not be sent. Please try again later or contact us.";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }
    }
}