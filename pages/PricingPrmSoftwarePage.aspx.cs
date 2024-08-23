using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using static ServiceStack.Diagnostics.Events;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.SalesforceDC;
using Stripe.Checkout;

namespace WdS.ElioPlus.pages
{
    public partial class PricingPrmSoftwarePage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                if (!IsPostBack)
                {
                    SetLinks();
                    FixButtons();
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

        private void UpdateStrings(string pageLang)
        {

        }

        private void FixButtons()
        {
            if (vSession.User == null)
                aGetStartedForFree.Visible = aGetStarted.Visible = aStartup.Visible = aGrowth.Visible = true;
            else
            {
                aGetStartedForFree.Visible = aGetStarted.Visible = false;

                if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                     aStartup.Visible = aGrowth.Visible = false;

                    aChekoutStartup.Visible = aChekoutGrowth.Visible = true;
                }
                else
                {
                    aStartup.Visible = aGrowth.Visible = false;
                    aChekoutStartup.Visible = aChekoutGrowth.Visible = false;
                }
            }
        }

        private void SetLinks()
        {
            
        }

        private void CheckOut(int packId, DBSession session)
        {
            ElioUsers user = vSession.User;

            ElioPackets packet = Sql.GetPacketById(packId, session);
            if (packet != null)
            {
                bool setEpiredSess = Sql.SetUSerCheckoutSessionsExpired(user.Id, session);
                if (!setEpiredSess)
                {
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
        }

        #endregion

        #region Buttons

        protected void aChekoutGrowth_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(9, session);

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User


                        #endregion
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

        protected void aChekoutStartup_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(8, session);

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User


                        #endregion
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

        #endregion
    }
}