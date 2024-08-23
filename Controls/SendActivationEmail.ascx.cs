using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using System.Configuration;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Controls.AlertControls;

namespace WdS.ElioPlus.Controls
{
    public partial class SendActivationEmail : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    UpdateStrings();
                }

                UcMessageAlert.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void UpdateStrings()
        {
            LblEmail.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "label", "1")).Text;
            
            Label lblResetText = (Label)ControlFinder.FindControlRecursive(RbtnReset, "LblResetText");
            lblResetText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "button", "1")).Text;
            Label lblSendText = (Label)ControlFinder.FindControlRecursive(RbtnSend, "LblSendText");
            lblSendText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "button", "2")).Text;

            RttMessage.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "tooltip", "1")).Text;
        }

        private bool CheckData()
        {
            UcMessageAlert.Visible = false;
            LblEmailError.Text = string.Empty;

            bool isError = false;

            if (string.IsNullOrEmpty(RtbxCompanyEmail.Text))
            {
                LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "label", "2")).Text;
                return isError = true;
            }
            else
            {
                if (!Validations.IsEmail(RtbxCompanyEmail.Text))
                {
                    LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "label", "3")).Text;
                    return isError = true;
                }
                //else
                //{
                //    if (!Sql.ExistEmail(RtbxCompanyEmail.Text, session))
                //    {
                //        LblEmailError.Text = "The email address you entered does not belong to any user";
                //        return isError = true;
                //    }
                //}
            }
           
            return isError;
        }

        private void ClearMessageData()
        {
            RtbxCompanyEmail.Text = string.Empty;
            LblEmailError.Text = string.Empty;
            UcMessageAlert.Visible = false;
        }

        #endregion

        #region Buttons

        protected void RbtnReset_OnClick(object sender, EventArgs args)
        {
            try
            {
                ClearMessageData();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RbtnSend_OnClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                string alert = string.Empty;

                bool isError = CheckData();

                if (isError) return;

                ElioUsers user = Sql.GetUserByEmail(RtbxCompanyEmail.Text, session);
                if (user != null)
                {
                    string newPassword = GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);

                    #region Send Email

                    EmailSenderLib.SendResetPasswordEmail(newPassword, RtbxCompanyEmail.Text, vSession.Lang, session);

                    #endregion

                    if (newPassword != string.Empty)
                    {
                        #region Update new password

                        user.Password = newPassword;
                        user.PasswordEncrypted = MD5.Encrypt(user.Password);
                        user.LastUpdated = DateTime.Now;

                        user = GlobalDBMethods.UpDateUser(user, session);

                        #endregion
                    }
                    else
                    {
                        isError = true;
                        alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "message", "1")).Text;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
                        return;
                    }
                }
                else
                {
                    ElioUsersSubAccounts subUser = Sql.GetSubAccountByEmail(RtbxCompanyEmail.Text, session);
                    if (subUser != null)
                    {
                        string newPassword = GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);

                        #region Send Email

                        EmailSenderLib.SendResetPasswordEmail(newPassword, RtbxCompanyEmail.Text, vSession.Lang, session);

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
                            alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "message", "1")).Text;
                            GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
                            return;
                        }
                    }
                    else
                    {
                        isError = true;
                        alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "message", "3")).Text;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
                        return;
                    }
                }

                ClearMessageData();

                alert = (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true") ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "message", "2")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "message", "4")).Text;
                GlobalMethods.ShowMessageControl(UcMessageAlert, alert, (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true") ? MessageTypes.Info : MessageTypes.Success, true, true, false);
            }
            catch (Exception ex)
            {
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "sendactivationemail", "message", "5")).Text;
                GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Error, true, true, false);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "User Email who request new password: " + RtbxCompanyEmail.Text);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion
    }
}