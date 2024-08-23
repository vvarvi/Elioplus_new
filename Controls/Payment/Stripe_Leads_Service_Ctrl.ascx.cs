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
using Stripe.Checkout;

namespace WdS.ElioPlus.Controls.Payment
{
    public partial class Stripe_Leads_Service_Ctrl : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        public bool HasDiscount { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(BtnCheckout);

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
                            //TbxEmail.Text = vSession.User.Email;
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
                    //TbxEmail.Text = string.Empty;
                    //BtnPayment.Enabled = false;
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
        }

        private bool IsValidData()
        {
            bool isValid = true;

            //if (string.IsNullOrEmpty(TbxEmail.Text))
            //{
            //    LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "1")).Text;
            //    isValid = false;
            //    return isValid;
            //}
            //else
            //{
            //    if (Sql.IsBlackListedDomain(TbxEmail.Text, session) || Sql.IsBlackListedEmail(TbxEmail.Text, session))
            //    {
            //        LblPaymentWarningMsgContent.Text = "Access denied";
            //        isValid = false;
            //        BtnPayment.Visible = false;
            //        return isValid;
            //    }

            //    if (!Validations.IsEmail(TbxEmail.Text))
            //    {
            //        LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "2")).Text;
            //        isValid = false;
            //        return isValid;
            //    }
            //}

            //if (string.IsNullOrEmpty(TbxCardNumber.Text))
            //{
            //    LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "3")).Text;
            //    isValid = false;
            //    return isValid;
            //}

            //if (DrpExpMonth.SelectedValue == "0")
            //{
            //    LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "4")).Text;
            //    isValid = false;
            //    return isValid;
            //}

            //if (string.IsNullOrEmpty(TbxExpYear.Text))
            //{
            //    LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "5")).Text;
            //    isValid = false;
            //    return isValid;
            //}
            //else
            //{
            //    if (Convert.ToInt32(TbxExpYear.Text) < Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2)))
            //    {
            //        LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "7")).Text;
            //        isValid = false;
            //        return isValid;
            //    }
            //}

            //if (string.IsNullOrEmpty(TbxCVC.Text))
            //{
            //    LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "6")).Text;
            //    isValid = false;
            //    return isValid;
            //}

            return isValid;
        }

        #endregion

        #region Buttons

        protected void aCheckout_ServerClick(object sender, EventArgs e)
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

                        if (string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                        {
                            DataLoader<ElioSnitcherUserCountryProducts> productsLoader = new DataLoader<ElioSnitcherUserCountryProducts>(session);

                            ElioSnitcherUserCountryProducts userInfo = productsLoader.LoadSingle(@"Select * from Elio_snitcher_user_country_products where user_id = @user_id and is_active = 1"
                                                                                , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id));

                            if (userInfo != null && userInfo.IsActive == 1)
                            {
                                ElioUsers user = vSession.User;

                                vSession.User = user;

                                ElioPackets packet = Sql.GetPacketById(userInfo.PackId, session);
                                if (packet != null)
                                {
                                    bool setEpiredSess = Sql.SetUSerCheckoutSessionsExpired(user.Id, session);
                                    if (!setEpiredSess)
                                    {
                                        divPaymentWarningMsg.Visible = true;
                                        LblPaymentWarningMsgContent.Text = "Sorry, you can not proceed to Checkout. Please try again later.";

                                        Logger.DetailedError(Request.Url.ToString(), string.Format("User {0} could not set expired his checkout sessions on Elio", user.Id), string.Format("User {0} could not proceed to payment on Stripe at {1}", user.Id, DateTime.Now));
                                        return;
                                    }

                                    Session chSession = Lib.Services.StripeAPI.StripeAPIService.CreateCheckoutSessionForPriceAndCustomerApi(packet.stripePlanId, user, false);
                                    if (chSession != null)
                                    {
                                        StripeUsersCheckoutSessions uSession = new StripeUsersCheckoutSessions();

                                        uSession.UserId = user.Id;
                                        uSession.StripePlanId = packet.stripePlanId;
                                        uSession.CheckoutSessionId = chSession.Id;
                                        uSession.CheckoutUrl = chSession.Url;
                                        uSession.DateCreated = DateTime.Now;
                                        uSession.LastUpdate = DateTime.Now;

                                        DataLoader<StripeUsersCheckoutSessions> loader = new DataLoader<StripeUsersCheckoutSessions>(session);
                                        loader.Insert(uSession);

                                        Response.Redirect(chSession.Url, false);
                                    }
                                }
                            }
                        }
                        else
                        {
                            divPaymentSuccessMsg.Visible = false;
                            divPaymentWarningMsg.Visible = true;
                            LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "7")).Text;
                            return;
                        }

                        return;
                    }
                    else
                    {
                        divPaymentSuccessMsg.Visible = false;
                        divPaymentWarningMsg.Visible = true;
                        LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "7")).Text;
                    }
                }
            }
            catch (Exception ex)
            {
                divPaymentSuccessMsg.Visible = false;
                divPaymentWarningMsg.Visible = true;
                LblPaymentWarningMsgContent.Text = "Something went wrong. Please try again later or contact us.";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

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

                        if (string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                        {
                            DataLoader<ElioSnitcherUserCountryProducts> productsLoader = new DataLoader<ElioSnitcherUserCountryProducts>(session);

                            ElioSnitcherUserCountryProducts userInfo = productsLoader.LoadSingle(@"Select * from Elio_snitcher_user_country_products where user_id = @user_id and is_active = 1"
                                                                                , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id));

                            ElioUsers user = vSession.User;

                            if (userInfo != null && userInfo.IsActive == 1)
                            {
                                bool setEpiredSess = Sql.SetUSerCheckoutSessionsExpired(user.Id, session);
                                if (!setEpiredSess)
                                {
                                    divPaymentWarningMsg.Visible = true;
                                    LblPaymentWarningMsgContent.Text = "Sorry, you can not proceed to Checkout. Please try again later.";

                                    Logger.DetailedError(Request.Url.ToString(), string.Format("User {0} could not set expired his checkout sessions on Elio", user.Id), string.Format("User {0} could not proceed to payment on Stripe at {1}", user.Id, DateTime.Now));
                                    return;
                                }

                                ElioPackets packet = Sql.GetPacketById(userInfo.PackId, session);
                                if (packet != null)
                                {
                                    Session chSession = Lib.Services.StripeAPI.StripeAPIService.CreateCheckoutSessionForPriceAndCustomerApi(packet.stripePlanId, user, false);
                                    if (chSession != null)
                                    {
                                        StripeUsersCheckoutSessions uSession = new StripeUsersCheckoutSessions();

                                        uSession.UserId = user.Id;
                                        uSession.StripePlanId = packet.stripePlanId;
                                        uSession.CheckoutSessionId = chSession.Id;
                                        uSession.CheckoutUrl = chSession.Url;
                                        uSession.DateCreated = DateTime.Now;
                                        uSession.LastUpdate = DateTime.Now;

                                        DataLoader<StripeUsersCheckoutSessions> loader = new DataLoader<StripeUsersCheckoutSessions>(session);
                                        loader.Insert(uSession);

                                        Response.Redirect(chSession.Url, false);
                                    }
                                }

                                ClearFields(false);
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
                        divPaymentWarningMsg.Visible = true;
                        LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "7")).Text;
                    }
                }
            }
            catch (Exception ex)
            {
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