using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Objects;
using Stripe.Checkout;

namespace WdS.ElioPlus.pages
{
    public partial class RecruitmentChannelPartnerPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    FixPage();

                //FixPaymentBtns();
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
               aStartupData.Visible = aGrowthData.Visible =
                aFreeAuto.Visible = aStartupAuto.Visible = aGrowthAuto.Visible = aGetStartedFree.Visible = true;

                aChekoutGrowthData.Visible = aChekoutStartupData.Visible = aChekoutGrowthAuto.Visible = aChekoutStartupAuto.Visible = false;
            }
            else
            {
                aGetStartedFree.Visible = false;

                if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    aChekoutGrowthData.Visible = aChekoutStartupData.Visible = aChekoutGrowthAuto.Visible = aChekoutStartupAuto.Visible = true;

                    aStartupData.Visible = aGrowthData.Visible =
                    aFreeAuto.Visible = aStartupAuto.Visible = aGrowthAuto.Visible = false;
                }
                else
                {
                    aChekoutGrowthData.Visible = aChekoutStartupData.Visible = aChekoutGrowthAuto.Visible = aChekoutStartupAuto.Visible = false;

                    aStartupData.Visible = aGrowthData.Visible =
                    aFreeAuto.Visible = aStartupAuto.Visible = aGrowthAuto.Visible = false;
                }
            }
        }

        private void SetLinks()
        {
            aEnterpriseData.HRef = aEnterpriseAuto.HRef = ControlLoader.ContactUs;

            if (vSession.User == null)
            {
                aGetStartedFreeB.Visible = true;

                aStartupData.HRef = aGrowthData.HRef =
                aFreeAuto.HRef = aStartupAuto.HRef = aGrowthAuto.HRef = aGetStartedFree.HRef = ControlLoader.SignUp;
            }
            else
            {
                aGetStartedFree.Visible = aGetStartedFreeB.Visible = false;

                if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(aChekoutGrowthData);
                    scriptManager.RegisterPostBackControl(aChekoutStartupData);
                    scriptManager.RegisterPostBackControl(aChekoutGrowthAuto);
                    scriptManager.RegisterPostBackControl(aChekoutStartupAuto);
                }
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

            //ImgVendorsFeatures.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "1")).Text;
            //LblVendorsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "29")).Text;
            //ImgResellersFeatures.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "2")).Text;
            //LblResellersTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "30")).Text;
            //ImgApiDevelopersFeatures.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "3")).Text;
            //LblApiDevelopersTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "31")).Text;
            //LtrVendorsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "literal", "1")).Text;
            //LblCollBenefit0Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "60")).Text;
            //LblCollBenefit0Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "61")).Text;
            //LblCollBenefit1Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "62")).Text;
            //LblCollBenefit1Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "63")).Text;
            //LblCollBenefit2Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "64")).Text;
            //LblCollBenefit2Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "65")).Text;
            //LblCollBenefit3Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "66")).Text;
            //LblCollBenefit3Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "67")).Text;
            //LblCollBenefit4Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "68")).Text;
            //LblCollBenefit4Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "69")).Text;
            //LblCollBenefit5Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "70")).Text;
            //LblCollBenefit5Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "71")).Text;
            //LblCollBenefit5ContentReadMore.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "73")).Text;

            //ImgCollBenefit0.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "alternate", "10")).Text;
            //ImgCollBenefit1.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "alternate", "11")).Text;
            //ImgCollBenefit2.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "alternate", "12")).Text;
            //ImgCollBenefit3.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "alternate", "13")).Text;
            //ImgCollBenefit4.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "alternate", "14")).Text;

            //LblHowItWorksCollaborationTool.Text = "How it works - Collaboration Tool";
            //LblFeature2Title1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "32")).Text;
            //LblFeature2Content1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "33")).Text;
            //LblFeature2Title2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "34")).Text;
            //LblFeature2Content2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "35")).Text;
            //LblFeature2Title3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "36")).Text;
            //LblFeature2Content3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "37")).Text;
            //LblCollaborationVBenefits.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "57")).Text;
            //LblCollaborationVendorsBenefitsCnt.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "58")).Text;
            //LblCollaborationVendorsBenefitsCnt2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "72")).Text;
            //LblCollaborationVBenefits2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitment", "label", "59")).Text;

            //LblMoreInfoTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworkscollaboration", "label", "9")).Text;
            //LblInfo1Title.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "1")).Text;
            //LblInfo1Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "2")).Text;
            //LblInfo2Title.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "3")).Text;
            //LblInfo2Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "4")).Text;
            ////LblInfo2Link.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "5")).Text;
            //LblInfo3Title.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "5")).Text;
            //LblInfo3Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "6")).Text;
            ////LblInfo3Link.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "8")).Text;
            //LblInfo4Title.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "7")).Text;
            //LblInfo4Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "8")).Text;

            //LblMoreQuestions.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "51")).Text;
            //LblGetInTouch.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "52")).Text;
            //LblGetStarted.Text = (vSession.User == null) ? "Free Sign Up" : "Upgrade Now";
            //LblMoreThan.Text = "More than ";
            //LblUsersNumber.Text = "120.000";
            //LblUsersAction.Text = " companies are using Elioplus";
            //LblNoWait.Text = "What are you waiting for?";

            //if (vSession.User == null)
            //{
            //    LblGetElioNow.Text = BtnGetElioNow.Text = "Add your Company Free";
            //    aGetElioNow.HRef = ControlLoader.SignUp;
            //}
            //else if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
            //    LblGetElioNow.Text = BtnGetElioNow.Text = "Start a new partnership!";
            //    //aGetElioNow.HRef = ControlLoader.Dashboard(vSession.User);                    
            //}
            //else
            //{
            //    LblGetElioNow.Text = BtnGetElioNow.Text = "Go Premium Now";
            //    aGetElioNow.HRef = ControlLoader.Pricing;
            //}
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
    }
}