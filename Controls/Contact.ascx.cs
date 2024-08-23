using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.Localization;
using System.Web.UI.WebControls;

namespace WdS.ElioPlus.Controls
{
    public partial class Contact : System.Web.UI.UserControl
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
                
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    LoadData();

                    session.CloseConnection();
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
            LblName.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "contact", "label", "1")).Text;
            LblEmail.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "contact", "label", "2")).Text;
            LblPhone.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "contact", "label", "3")).Text;
            LblSubject.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "contact", "label", "4")).Text;
            LblMessage.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "contact", "label", "5")).Text;

            RttMessage.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "contact", "tooltip", "1")).Text;

            Label lblResetText = (Label)ControlFinder.FindControlRecursive(RbtnReset, "LblResetText");
            lblResetText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "contact", "button", "1")).Text;
            Label lblSendText = (Label)ControlFinder.FindControlRecursive(RbtnSend, "LblSendText");
            lblSendText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "contact", "button", "2")).Text;
        }

        private void LoadData()
        {
            RtbxCompanyName.Text = vSession.User.CompanyName;

            RcbxCompanyEmail.Visible = ((!string.IsNullOrEmpty(vSession.User.OfficialEmail)) && vSession.User.Email != vSession.User.OfficialEmail) ? true : false;
            RtbxCompanyEmail.Visible = (RcbxCompanyEmail.Visible) ? false : true;
            if (RcbxCompanyEmail.Visible)
            {
                RcbxCompanyEmail.Items.Clear();

                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = vSession.User.Email;
                item.Value = "0";
                RcbxCompanyEmail.Items.Add(item);

                RadComboBoxItem item2 = new RadComboBoxItem();
                item2.Text = vSession.User.OfficialEmail;
                item.Value = "1";
                RcbxCompanyEmail.Items.Add(item2);

            }
            else
            {
                RtbxCompanyEmail.Text = vSession.User.Email;
            }
            RtbxCompanyPhone.Text = vSession.User.Phone;
        }

        private bool CheckData()
        {
            LblnameError.Text = string.Empty;
            LblEmailError.Text = string.Empty;
            LblPhoneError.Text = string.Empty;
            LblSubjectError.Text = string.Empty;
            LblMessageError.Text = string.Empty;

            bool isError = false;

            if (string.IsNullOrEmpty(RtbxCompanyName.Text))
            {
                LblnameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "12")).Text;
                return isError = true;
            }

            if (!RcbxCompanyEmail.Visible)
            {
                if (string.IsNullOrEmpty(RtbxCompanyEmail.Text))
                {
                    LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "13")).Text;
                    return isError = true;
                }
                else
                {
                    if (!Validations.IsEmail(RtbxCompanyEmail.Text))
                    {
                        LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "14")).Text;
                        return isError = true;
                    }
                }
            }

            if (string.IsNullOrEmpty(RtbxCompanyPhone.Text))
            {
                LblPhoneError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "15")).Text;
                return isError = true;
            }

            if (string.IsNullOrEmpty(RtbxSubject.Text))
            {
                LblSubjectError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "16")).Text;
                return isError = true;
            }

            if (string.IsNullOrEmpty(RtbxMessage.Text))
            {
                LblMessageError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "17")).Text;
                return isError = true;
            }

            return isError;
        }

        private void ClearMessageData()
        {
            UcMessageAlert.Visible = false;

            if (vSession.User == null)
            {
                RtbxCompanyName.Text = string.Empty;
                RtbxCompanyEmail.Text = string.Empty;
                RtbxCompanyPhone.Text = string.Empty;
            }
            
            RtbxSubject.Text = string.Empty;
            RtbxMessage.Text = string.Empty;

            LblnameError.Text = string.Empty;
            LblEmailError.Text = string.Empty;
            LblPhoneError.Text = string.Empty;
            LblSubjectError.Text = string.Empty;
            LblMessageError.Text = string.Empty;
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

                bool isError = CheckData();

                if (isError) return;

                //EmailNotificationsLib.ContactElioplus(RtbxCompanyName.Text, (RcbxCompanyEmail.Visible) ? RcbxCompanyEmail.SelectedItem.Text : RtbxCompanyEmail.Text, RtbxSubject.Text, RtbxMessage.Text, RtbxCompanyPhone.Text, session);
                EmailSenderLib.ContactElioplus(RtbxCompanyName.Text, (RcbxCompanyEmail.Visible) ? RcbxCompanyEmail.SelectedItem.Text : RtbxCompanyEmail.Text, RtbxSubject.Text, RtbxMessage.Text, RtbxCompanyPhone.Text, vSession.Lang, session);

                ClearMessageData();
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "2")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "19")).Text;
                GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Success, true, true, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                Logger.DetailedError(Request.Url.ToString(), ("An error occured during someone was trying to contact with us at " + DateTime.Now + ". Details: Cpmpany Name:" + RtbxCompanyName.Text + ", Company Email: " + ((RcbxCompanyEmail.Visible) ? RcbxCompanyEmail.SelectedItem.Text : RtbxCompanyEmail.Text + ", Subject: " + RtbxSubject.Text + ", Message: " + RtbxMessage.Text + ", Company Phone: " + RtbxCompanyPhone.Text)).ToString(), ex.StackTrace.ToString());

                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "20")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "4")).Text;
                GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion
    }
}