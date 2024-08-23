using System;
using System.Linq;
using System.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.StripePayment;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Controls.Payment
{
    public partial class Stripe_CreditCard_Ctrl : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        UpdateStrings();

                        divInfo.Visible = (!string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? false : true;
                    }
                }
                //else
                // Response.Redirect(ControlLoader.Login, false);
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

        private void UpdateStrings()
        {

        }

        private void FixState()
        {
            
        }

        public void ClearFields()
        {
            LblCCWarningMsgContent.Text = string.Empty;
            divCCWarningMsg.Visible = false;

            LblCCSuccessMsgContent.Text = string.Empty;
            divCCSuccessMsg.Visible = false;

            TbxCardNumber.Text = string.Empty;
            DrpExpMonth.SelectedValue = "0";
            TbxExpYear.Text = string.Empty;
            TbxCVC.Text = string.Empty;

            BtnCCUpdate.Enabled = (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? true : false;

            divInfo.Visible = true;
        }

        private bool IsValidData()
        {
            bool isValid = true;
            
            if (string.IsNullOrEmpty(TbxCardNumber.Text))
            {
                LblCCWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "3")).Text;
                isValid = false;
                return isValid;
            }

            if (DrpExpMonth.SelectedValue == "0")
            {
                LblCCWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "4")).Text;
                isValid = false;
                return isValid;
            }

            if (string.IsNullOrEmpty(TbxExpYear.Text))
            {
                LblCCWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "5")).Text;
                isValid = false;
                return isValid;
            }
            else
            {
                if (Convert.ToInt32(TbxExpYear.Text) < Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2)))
                {
                    LblCCWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "7")).Text;
                    isValid = false;
                    return isValid;
                }
            }

            if (string.IsNullOrEmpty(TbxCVC.Text))
            {
                LblCCWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "6")).Text;
                isValid = false;
                return isValid;
            }

            return isValid;
        }

        #endregion

        #region Buttons

        protected void BtnCCUpdate_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        divCCWarningMsg.Visible = false;
                        LblCCWarningMsgContent.Text = string.Empty;
                        divCCSuccessMsg.Visible = false;
                        LblCCSuccessMsgContent.Text = string.Empty;

                        string cardFingerPrint = string.Empty;
                        string cardId = string.Empty;
                        string errorMessage = string.Empty;
                        string customerResponseId = string.Empty;
                        string chargeId = string.Empty;
                        //DateTime? startDate = null;
                        //DateTime? currentPeriodStart = null;
                        //DateTime? currentPeriodEnd = null;
                        string subscriptionStatus = string.Empty;
                        //DateTime? trialPeriodStart = null;
                        //DateTime? trialPeriodEnd = null;
                        string orderMode = string.Empty;
                        //Xamarin.Payments.Stripe.StripeCard card = null;
                        int packetId = Convert.ToInt32(Packets.Premium);

                        if (!IsValidData())
                        {
                            divCCWarningMsg.Visible = true;
                            return;
                        }
                        else
                        {
                            divCCWarningMsg.Visible = false;
                        }

                        bool isError = false;

                        if (isError)
                        {
                            divCCWarningMsg.Visible = true;
                            LblCCWarningMsgContent.Text = errorMessage;
                            return;
                        }
                    }

                    vSession.UserHasExpiredOrder = false;

                    divCCSuccessMsg.Visible = true;
                    LblCCSuccessMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "5")).Text;
                    BtnCCUpdate.Enabled = false;

                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                divCCWarningMsg.Visible = true;
                LblCCWarningMsgContent.Text = "Something went wrong. Please try again later.";
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnCCUpdateCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ClearFields();

                    ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseCCNumberPopUp();", true);
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

        #endregion
    }
}