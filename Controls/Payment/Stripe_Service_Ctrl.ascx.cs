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
using Stripe.Checkout;

namespace WdS.ElioPlus.Controls.Payment
{
    public partial class Stripe_Service_Ctrl : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

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
                        FixPage();

                        int servicePlanId = 2;

                        if (vSession.User.Id == 34817)  //Custom for Applixure only company name
                            servicePlanId = 2;

                        LblTotalCostValue.Text = Sql.GetServiceTotalCost(servicePlanId, session).ToString() + " $";
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
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

        private void FixPage()
        {
            UpdateStrings();

            divPaymentWarningMsg.Visible = false;
            LblPaymentWarningMsgContent.Text = string.Empty;
            divPaymentSuccessMsg.Visible = false;
            LblPaymentSuccessMsgContent.Text = string.Empty;
        }

        private void UpdateStrings()
        {
            LblTotalCost.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "3")).Text;
        }

        #endregion

        #region Buttons

        protected void aCheckout_ServerClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                        {
                            int selectedPacketId = (int)Packets.AccountManagerService;

                            ElioUsers user = vSession.User;

                            if (user.Id == 34817)
                                selectedPacketId = (int)Packets.PremiumService299;
                            else if (user.Id == 35867 || user.Id == 3399)
                                selectedPacketId = (int)Packets.PremiumService;

                            ElioPackets packet = Sql.GetPacketById(selectedPacketId, session);
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

                                Session chSession = null;

                                ElioPacketsStripeCoupons planCoupon = Sql.GetPacketStripeCouponByUserPlanId(user.Id, packet.stripePlanId, session);

                                if (planCoupon != null)
                                    chSession = Lib.Services.StripeAPI.StripeAPIService.CreateCheckoutSessionForPriceAndExistingCustomerWithDiscountApi(packet.stripePlanId, planCoupon.CouponId, user);
                                else
                                    chSession = Lib.Services.StripeAPI.StripeAPIService.CreateCheckoutSessionForPriceAndExistingCustomerApi(packet.stripePlanId, user);

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
                        else
                        {
                            divPaymentWarningMsg.Visible = true;
                            LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "7")).Text;
                        }
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