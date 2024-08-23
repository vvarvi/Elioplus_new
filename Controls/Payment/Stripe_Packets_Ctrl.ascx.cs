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
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using WdS.ElioPlus.Lib.Services.StripeAPI;
using Stripe;
using ServiceStack.Stripe.Types;
using Stripe.Checkout;
using Org.BouncyCastle.Bcpg;

namespace WdS.ElioPlus.Controls.Payment
{
    public partial class Stripe_Packets_Ctrl : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        public bool HasDiscount { get; set; }

        public int PacketId { get; set; }

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
                        LoadPackets((int)StripePlans.Elio_Startup_Plan);

                        UpdateStrings();
                    }
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

        protected void LoadPackets(int selectedPlan)
        {
            List<ElioPackets> packets = Sql.GetDefaultActivePackets(session);

            DrpStripePlans.Items.Clear();

            foreach (ElioPackets packet in packets)
            {
                if (packet.PackDescription == "Enterprise" || packet.PackDescription == "Premium")
                    continue;

                ListItem item = new ListItem();
                item.Value = packet.Id.ToString();
                item.Text = packet.PackDescription;

                DrpStripePlans.Items.Add(item);
            }

            if (Sql.ExistsPacketById(selectedPlan, session))
                DrpStripePlans.SelectedValue = selectedPlan.ToString();

            LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(selectedPlan), session).ToString() + " $";
        }

        private void UpdateStrings()
        {
            LblTotalCost.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "4")).Text;    //(vSession.User.BillingType == Convert.ToInt32(BillingType.Freemium) && !string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "2")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "1")).Text;
        }

        public void ClearFields()
        {
            LblPaymentWarningMsgContent.Text = string.Empty;
            divPaymentWarningMsg.Visible = false;

            LblPaymentSuccessMsgContent.Text = string.Empty;
            divPaymentSuccessMsg.Visible = false;
                       
            DrpStripePlans.SelectedIndex = -1;            
            
            LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session).ToString() + " $";
        }

        #endregion

        #region Buttons

        protected void aCheckout_ServerClick(object sender, EventArgs e)
        {
            string customerResponseId = string.Empty;

            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {                        
                        #region Freemium User

                        divPaymentWarningMsg.Visible = false;
                        LblPaymentWarningMsgContent.Text = string.Empty;
                        divPaymentSuccessMsg.Visible = false;
                        LblPaymentSuccessMsgContent.Text = string.Empty;

                        int selectedPacketId = Convert.ToInt32(DrpStripePlans.SelectedValue);

                        ElioUsers user = vSession.User;

                        ElioPackets packet = Sql.GetPacketById(Convert.ToInt32(DrpStripePlans.SelectedValue), session);
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
                                chSession = Lib.Services.StripeAPI.StripeAPIService.CreateCheckoutSessionForPriceAndCustomerWithDiscountApi(packet.stripePlanId, planCoupon.CouponId, user);
                            else
                                chSession = Lib.Services.StripeAPI.StripeAPIService.CreateCheckoutSessionForPriceAndCustomerApi(packet.stripePlanId, user, false);

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

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User

                        //BtnCheckout.Enabled = false;
                        divPaymentSuccessMsg.Visible = false;
                        divPaymentWarningMsg.Visible = true;
                        LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "6")).Text;

                        #endregion
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

        protected void BtnCancelPayment_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ClearFields();

                    ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "ClosePaymentModal();", true);
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

        #region DropDownList

        protected void DrpStripePlans_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session).ToString() + " $";
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