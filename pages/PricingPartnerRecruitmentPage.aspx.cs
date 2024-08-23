using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Web.UI;
using WdS.ElioPlus.Objects;
using Stripe.Checkout;

namespace WdS.ElioPlus.pages
{
    public partial class PricingPartnerRecruitmentPage : System.Web.UI.Page
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
            UpdateStrings();
            SetLinks();

            if (vSession.User == null)
            {
                aGetStartedData.Visible = aStartupData.Visible = aGrowthData.Visible =
                aFreeAuto.Visible = aStartupAuto.Visible = aGrowthAuto.Visible = true;

                aChekoutGrowthData.Visible = aChekoutStartupData.Visible = aChekoutGrowthAuto.Visible = aChekoutStartupAuto.Visible = false;
            }
            else
            {
                if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    aChekoutGrowthData.Visible = aChekoutStartupData.Visible = aChekoutGrowthAuto.Visible = aChekoutStartupAuto.Visible = true;

                    aGetStartedData.Visible = aStartupData.Visible = aGrowthData.Visible =
                    aFreeAuto.Visible = aStartupAuto.Visible = aGrowthAuto.Visible = false;
                }
                else
                {
                    aGetStartedData.Visible = aStartupData.Visible = aGrowthData.Visible =
                    aFreeAuto.Visible = aStartupAuto.Visible = aGrowthAuto.Visible = false;

                    aChekoutGrowthData.Visible = aChekoutStartupData.Visible = aChekoutGrowthAuto.Visible = aChekoutStartupAuto.Visible = false;
                }
            }
        }

        private void SetLinks()
        {
            aEnterpriseData.HRef = aEnterpriseAuto.HRef = ControlLoader.ContactUs;

            if (vSession.User == null)
            {
                aGetStartedData.HRef = aStartupData.HRef = aGrowthData.HRef =
                aFreeAuto.HRef = aStartupAuto.HRef = aGrowthAuto.HRef = ControlLoader.SignUp;
            }
            else
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(aChekoutGrowthData);
                scriptManager.RegisterPostBackControl(aChekoutStartupData);
                scriptManager.RegisterPostBackControl(aChekoutGrowthAuto);
                scriptManager.RegisterPostBackControl(aChekoutStartupAuto);
            }
        }

        private void UpdateStrings()
        {
            if (vSession.User != null)
            {
                LblCkStartupAuto.Text = LblCkGrowthAuto.Text = LblCkStartupData.Text = LblCkGrowthData.Text = "UPGRADE NOW";
            }
            else
            {
                LblStartupAuto.Text = LblGrowthAuto.Text = LblStartupData.Text = LblGrowthData.Text = "GET STARTED NOW";
            }
        }

        #endregion

        #region Buttons

        protected void aChekoutGrowthAuto_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(57, session);

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

        protected void aChekoutStartupAuto_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(56, session);

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

        protected void aChekoutGrowthData_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(59, session);

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

        protected void aChekoutStartupData_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(58, session);

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

        #region Methods

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
    }
}