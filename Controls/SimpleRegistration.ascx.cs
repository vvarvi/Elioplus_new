using System;
using System.Linq;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using System.Web;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using System.Web.UI.WebControls;

namespace WdS.ElioPlus.Controls
{
    public partial class SimpleRegistration : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateStrings();

                UcMessageControl.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void UpdateStrings()
        {
            LblHeader.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "label", "3")).Text;
            RtbxUsername.EmptyMessage = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "emptymessage", "1")).Text;
            RtbxPassword.EmptyMessage = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "emptymessage", "2")).Text;
            RtbxRetypePassword.EmptyMessage = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "emptymessage", "2")).Text;
            RtbxEmail.EmptyMessage = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "emptymessage", "4")).Text;
            LnkBtnCreatePassword.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "button", "2")).Text;
            LblAccountText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "label", "5")).Text;
            LnkBtnRegister.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "label", "6")).Text;
            Label lblSaveText = (Label)ControlFinder.FindControlRecursive(RbtnSave, "LblSaveText");
            lblSaveText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "6")).Text;
        }

        private void ClearData()
        {
            UcMessageControl.Visible = false;
            RtbxUsername.Text = string.Empty;
            RtbxPassword.Text = string.Empty;
            RtbxRetypePassword.Text = string.Empty;
            RtbxEmail.Text = string.Empty;
        }

        private string CheckInvalidData()
        {
            string alert = string.Empty;

            if (string.IsNullOrEmpty(RtbxUsername.Text))
            {
                alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "1")).Text;
                return alert;
            }
            else
            {
                if (RtbxUsername.Text.Length < 8)
                {
                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "2")).Text;
                    return alert;
                }
                else
                {
                    if (!Validations.IsUsernameCharsValid(RtbxUsername.Text))
                    {
                        alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "3")).Text;
                        return alert;
                    }

                    if (Sql.ExistUsername(RtbxUsername.Text, session))
                    {
                        alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "4")).Text;
                        return alert;
                    }
                }
            }

            if (string.IsNullOrEmpty(RtbxPassword.Text))
            {
                alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "5")).Text;
                return alert;
            }
            else
            {
                if (RtbxPassword.Text.Length < 8)
                {
                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "6")).Text;
                    return alert;
                }
                if (!Validations.IsPasswordCharsValid(RtbxPassword.Text))
                {
                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "7")).Text;
                    return alert;
                }
            }

            if (string.IsNullOrEmpty(RtbxRetypePassword.Text))
            {
                alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "8")).Text;
                return alert;
            }
            else
            {
                if (RtbxPassword.Text != RtbxRetypePassword.Text)
                {
                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "9")).Text;
                    return alert;
                }
            }

            if (string.IsNullOrEmpty(RtbxEmail.Text))
            {
                alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "10")).Text;
                return alert;
            }
            else
            {
                if (!Validations.IsEmail(RtbxEmail.Text))
                {
                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "11")).Text;
                    return alert;
                }

                if (Sql.ExistEmail(RtbxEmail.Text, session))
                {
                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "12")).Text;
                    return alert;
                }
            }

            return alert;
        }

        #endregion
        
        #region Buttons

        protected void LnkBtnCreatePassword_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
               
                RtbxPassword.Text = WdS.ElioPlus.Lib.Utils.GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);
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

        protected void BtnSubmit_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcMessageControl.Visible = false;

                string alert = CheckInvalidData();

                if (alert != string.Empty)
                {
                    GlobalMethods.ShowMessageControl(UcMessageControl, alert, MessageTypes.Error, false, true, false);
                    return;
                }
                else
                {
                    #region Insert New User

                    //vSession.User = GlobalMethods.InsertSimpleRegistration(RtbxUsername.Text, 
                    //                                                       RtbxPassword.Text, 
                    //                                                       HttpContext.Current.Request.ServerVariables["remote_addr"],
                    //                                                       RtbxEmail.Text,string.Empty, string.Empty, session);

                    ElioUsers user = new ElioUsers();

                    user.Username = RtbxUsername.Text;
                    user.UsernameEncrypted = MD5.Encrypt(user.Username);
                    user.Password = RtbxPassword.Text;
                    user.PasswordEncrypted = MD5.Encrypt(user.Password);
                    user.SysDate = DateTime.Now;
                    user.LastUpdated = DateTime.Now;
                    user.LastLogin = DateTime.Now;
                    user.UserLoginCount = 1;
                    user.Ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
                    user.Email = RtbxEmail.Text;
                    user.GuId = Guid.NewGuid().ToString();
                    user.IsPublic = 1;
                    user.LinkedInUrl = string.Empty;
                    user.TwitterUrl = string.Empty;
                    user.HasBillingDetails = 0;

                    try
                    {
                        user.BillingType = Sql.GetFreemiumBillingTypeId(session);
                    }
                    catch (Exception ex)
                    {
                        user.BillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    user.UserApplicationType = Convert.ToInt32(UserApplicationType.Elioplus);

                    vSession.User = GlobalDBMethods.InsertNewUser(user, session);

                    #endregion

                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "13")).Text;
                    GlobalMethods.ShowMessageControl(UcMessageControl, alert, MessageTypes.Success, false, true, false);

                    #region Redirect

                    Response.Redirect(ControlLoader.SignUp, false);

                    #endregion

                    #region Send Notification Email

                    EmailSenderLib.SendNotificationEmailForNewSimpleRegisteredUser(vSession.User, vSession.Lang, session);

                    #endregion

                    alert = string.Empty;
                }

                ClearData();
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

        protected void BtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect(ControlLoader.Default(), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}