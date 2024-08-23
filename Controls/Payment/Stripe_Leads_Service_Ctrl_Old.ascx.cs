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
using WdS.ElioPlus.Lib.Services.StripeAPI;
using System.Configuration;
using System.Collections.Generic;

namespace WdS.ElioPlus.Controls.Payment
{
    public partial class Stripe_Leads_Service_Ctrl_Old : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        public bool HasDiscount { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        DataLoader<ElioSnitcherUserCountryProducts> productsLoader = new DataLoader<ElioSnitcherUserCountryProducts>(session);

                        ElioSnitcherUserCountryProducts userInfo = productsLoader.LoadSingle(@"Select * from Elio_snitcher_user_country_products where user_id = @user_id and is_active = 1"
                                                                            , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id));

                        if (userInfo != null && userInfo.IsActive == 1)
                        {
                            LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(userInfo.PackId, session).ToString() + " $";
                            TbxEmail.Text = vSession.User.Email;
                        }
                        else
                        {
                            LblTotalCostValue.Text = Sql.GetServiceTotalCost(24, session).ToString() + " $";
                        }

                        ClearFields(true);
                        UpdateStrings();
                    }                  
                }
                else
                {
                    TbxEmail.Text = string.Empty;
                    BtnPayment.Enabled = false;
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

        private void UpdateStrings()
        {
            bool isOneTimeStripeUser = false;

            if (ConfigurationManager.AppSettings["IsLeadOneTimeStripeCustomer"] != null)
            {
                List<string> userLeadsIds = ConfigurationManager.AppSettings["IsLeadOneTimeStripeCustomer"].ToString().Split(',').ToList();

                if (userLeadsIds.Count > 0)
                {
                    if (userLeadsIds.Contains(vSession.User.Id.ToString()))
                        isOneTimeStripeUser = true;
                }
            }

            LblTotalCost.Text = (isOneTimeStripeUser) ? "Your service plan cost is " : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "3")).Text;
        }

        public void ClearFields(bool isFullReset)
        {
            if (isFullReset)
            {
                LblPaymentWarningMsgContent.Text = string.Empty;
                divPaymentWarningMsg.Visible = false;
                
                LblPaymentSuccessMsgContent.Text = string.Empty;
                divPaymentSuccessMsg.Visible = false;
            }

            TbxEmail.Text = (vSession.User == null) ? string.Empty : vSession.User.Email;
            TbxCardNumber.Text = string.Empty;
            DrpExpMonth.SelectedValue = "0";
            TbxExpYear.Text = string.Empty;
            TbxCVC.Text = string.Empty;            
        }

        private bool IsValidData()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(TbxEmail.Text))
            {
                LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "1")).Text;
                isValid = false;
                return isValid;
            }
            else
            {
                if (Sql.IsBlackListedDomain(TbxEmail.Text, session) || Sql.IsBlackListedEmail(TbxEmail.Text, session))
                {
                    LblPaymentWarningMsgContent.Text = "Access denied";
                    isValid = false;
                    BtnPayment.Visible = false;
                    return isValid;
                }

                if (!Validations.IsEmail(TbxEmail.Text))
                {
                    LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "2")).Text;
                    isValid = false;
                    return isValid;
                }
            }

            if (string.IsNullOrEmpty(TbxCardNumber.Text))
            {
                LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "3")).Text;
                isValid = false;
                return isValid;
            }

            if (DrpExpMonth.SelectedValue == "0")
            {
                LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "4")).Text;
                isValid = false;
                return isValid;
            }

            if (string.IsNullOrEmpty(TbxExpYear.Text))
            {
                LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "5")).Text;
                isValid = false;
                return isValid;
            }
            else
            {
                if (Convert.ToInt32(TbxExpYear.Text) < Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2)))
                {
                    LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "7")).Text;
                    isValid = false;
                    return isValid;
                }
            }

            if (string.IsNullOrEmpty(TbxCVC.Text))
            {
                LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "6")).Text;
                isValid = false;
                return isValid;
            }

            return isValid;
        }

        #endregion

        #region Buttons

        protected void BtnPayment_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        divPaymentWarningMsg.Visible = false;
                        LblPaymentWarningMsgContent.Text = string.Empty;
                        divPaymentSuccessMsg.Visible = false;
                        LblPaymentSuccessMsgContent.Text = string.Empty;

                        if (!IsValidData())
                        {
                            divPaymentWarningMsg.Visible = true;
                            return;
                        }
                        else
                        {
                            divPaymentWarningMsg.Visible = false;
                        }

                        bool isSuccess = false;

                        if (string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                        {
                            DataLoader<ElioSnitcherUserCountryProducts> productsLoader = new DataLoader<ElioSnitcherUserCountryProducts>(session);

                            ElioSnitcherUserCountryProducts userInfo = productsLoader.LoadSingle(@"Select * from Elio_snitcher_user_country_products where user_id = @user_id and is_active = 1"
                                                                                , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id));

                            if (userInfo != null && userInfo.IsActive == 1)
                            {
                                session.BeginTransaction();

                                ElioUsers user = vSession.User;

                                isSuccess = StripeApi.PaymentMethodNew(out user, vSession.User.Id, userInfo.PackId, TbxCardNumber.Text, DrpExpMonth.SelectedItem.Text, TbxExpYear.Text, TbxCVC.Text, "", session);
                                
                                vSession.User = user;

                                session.CommitTransaction();

                                if (!isSuccess)
                                {
                                    divPaymentWarningMsg.Visible = true;
                                    LblPaymentWarningMsgContent.Text = "Something went wrong with your service subscription. Please try again later or contact us.";
                                    Logger.DetailedError(Request.Url.ToString(), "Stripe_Leads_Service_Ctrl.aspx --> ERROR -->", "User " + vSession.User.Id + " tried to pay leads service but something went wrong in Stripe at " + DateTime.Now.ToString());

                                    return;
                                }

                                ClearFields(false);

                                divPaymentSuccessMsg.Visible = true;
                                LblPaymentSuccessMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "5")).Text;
                                BtnPayment.Enabled = false;

                                try
                                {
                                    //EmailSenderLib.SendStripeTrialActivationEmail(TbxEmail.Text, vSession.Lang, session);
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }
                        }
                        else
                        {
                            divPaymentWarningMsg.Visible = true;
                            LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "7")).Text;
                            return;
                        }

                        return;
                    }
                    else
                    {
                        BtnPayment.Enabled = false;
                        divPaymentWarningMsg.Visible = true;
                        LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "7")).Text;
                    }
                }
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();

                divPaymentWarningMsg.Visible = true;
                LblPaymentWarningMsgContent.Text = "Something went wrong. Please try again later.";
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();                
            }
        }

        protected void BtnCancelPayment_OnClick(object sender, EventArgs args)
        {
            try
            {
                ClearFields(true);

                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "ClosePaymentServicePopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}