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
    public partial class Stripe_Startup_Ctrl : System.Web.UI.UserControl
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
                        LblStartupTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.PremiumStartup), session).ToString() + " $";
                        TbxStartupEmail.Text = vSession.User.Email;

                        UpdateStrings();

                        divStartupInfo.Visible = (!string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? false : true;
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
            LblStartupTotalCost.Text = (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && !string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "2")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "1")).Text;
        }

        private void FixState()
        {
            HasDiscount = Sql.HasUserDiscount(vSession.User.Id, session);
            PnlStartupPayment.Enabled = !HasDiscount;
            BtnStartupPayment.Enabled = (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? true : false;
        }

        public void ClearFields()
        {
            LblStartupPaymentWarningMsgContent.Text = string.Empty;
            divStartupPaymentWarningMsg.Visible = false;

            LblStartupPaymentSuccessMsgContent.Text = string.Empty;
            divStartupPaymentSuccessMsg.Visible = false;

            TbxStartupEmail.Text = vSession.User.Email;
            TbxStartupCardNumber.Text = string.Empty;
            DrpStartupExpMonth.SelectedValue = "0";
            TbxStartupExpYear.Text = string.Empty;
            TbxStartupEmail.Text = string.Empty;
            TbxStartupCVC.Text = string.Empty;

            BtnStartupPayment.Enabled = (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? true : false;

            divStartupDiscount.Visible = false;
            divStartupInfo.Visible = true;
            CbxStartupCouponDiscount.Checked = false;

            LblStartupTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.Premium), session).ToString() + " $";
        }

        private bool IsValidData()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(TbxStartupEmail.Text))
            {
                LblStartupPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "1")).Text;
                isValid = false;
                return isValid;
            }
            else
            {
                if (!Validations.IsEmail(TbxStartupEmail.Text))
                {
                    LblStartupPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "2")).Text;
                    isValid = false;
                    return isValid;
                }
            }

            if (string.IsNullOrEmpty(TbxStartupCardNumber.Text))
            {
                LblStartupPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "3")).Text;
                isValid = false;
                return isValid;
            }

            if (DrpStartupExpMonth.SelectedValue == "0")
            {
                LblStartupPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "4")).Text;
                isValid = false;
                return isValid;
            }

            if (string.IsNullOrEmpty(TbxStartupExpYear.Text))
            {
                LblStartupPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "5")).Text;
                isValid = false;
                return isValid;
            }
            else
            {
                if (Convert.ToInt32(TbxStartupExpYear.Text) < Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2)))
                {
                    LblStartupPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "7")).Text;
                    isValid = false;
                    return isValid;
                }
            }

            if (string.IsNullOrEmpty(TbxStartupCVC.Text))
            {
                LblStartupPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "6")).Text;
                isValid = false;
                return isValid;
            }

            if (CbxStartupCouponDiscount.Checked)
            {
                if (string.IsNullOrEmpty(TbxStartupDiscount.Text))
                {
                    LblStartupPaymentWarningMsgContent.Text = "Please enter your Coupon ID";       //Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "8")).Text;
                    isValid = false;
                    return isValid;
                }
            }

            return isValid;
        }

        #endregion

        #region Buttons

        protected void BtnStartupPayment_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        divStartupPaymentWarningMsg.Visible = false;
                        LblStartupPaymentWarningMsgContent.Text = string.Empty;
                        divStartupPaymentSuccessMsg.Visible = false;
                        LblStartupPaymentSuccessMsgContent.Text = string.Empty;

                        string cardFingerPrint = string.Empty;
                        string cardId = string.Empty;
                        string errorMessage = string.Empty;
                        string customerResponseId = string.Empty;
                        string chargeId = string.Empty;
                        DateTime? startDate = null;
                        DateTime? currentPeriodStart = null;
                        DateTime? currentPeriodEnd = null;
                        string subscriptionStatus = string.Empty;
                        DateTime? trialPeriodStart = null;
                        DateTime? trialPeriodEnd = null;
                        string orderMode = string.Empty;
                        Xamarin.Payments.Stripe.StripeCard card = null;
                        Xamarin.Payments.Stripe.StripeCoupon couponDiscount = null;
                        int packetId = Convert.ToInt32(Packets.Premium);

                        if (!HasDiscount)
                        {
                            if (!IsValidData())
                            {
                                divStartupPaymentWarningMsg.Visible = true;
                                return;
                            }
                            else
                            {
                                divStartupPaymentWarningMsg.Visible = false;
                            }

                            bool isError = false;

                            if (string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                            {
                                if (CbxStartupCouponDiscount.Checked)
                                {
                                    if (TbxStartupDiscount.Text.Trim() != string.Empty)
                                    {
                                        ElioPlanCoupons planCoupon = Sql.GetPlanCoupon(TbxStartupDiscount.Text.Trim().ToUpper(), session);

                                        if (planCoupon != null)
                                        {
                                            couponDiscount = StripeLib.CreateGetCoupon(Xamarin.Payments.Stripe.StripeCouponDuration.Repeating, planCoupon.MaxRedemptions, planCoupon.CouponId, planCoupon.PercentOff, planCoupon.MonthDuration, planCoupon.RedeemBy);

                                            if (couponDiscount != null)
                                            {
                                                if (BtnStartupPayment.Text == "Proceed")
                                                {
                                                    decimal totalCost = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.Premium), session);
                                                    //LblTotalCostValue.Text = (planCoupon.AmountOff == null || planCoupon.AmountOff == 0) ? (totalCost - ((planCoupon.PercentOff * totalCost) / 100)).ToString() : Convert.ToDecimal(planCoupon.AmountOff) + " $";
                                                    LblStartupTotalCostValue.Text = (couponDiscount.AmountOff == null || couponDiscount.AmountOff == 0) ? (totalCost - ((couponDiscount.PercentOff * totalCost) / 100)).ToString() : (totalCost - Convert.ToDecimal(couponDiscount.AmountOff) / 100).ToString() + " $";
                                                    divStartupInfo.Visible = false;
                                                    BtnStartupPayment.Text = "Subscribe";

                                                    return;
                                                }
                                                else if (BtnStartupPayment.Text == "Subscribe")
                                                {
                                                    isError = StripeLib.SubscribeUnRegisteredCustomerWithCoupon(TbxStartupEmail.Text, TbxStartupCardNumber.Text, TbxStartupCVC.Text, Convert.ToInt32(DrpStartupExpMonth.SelectedItem.Text), Convert.ToInt32(TbxStartupExpYear.Text), vSession.User.Id, vSession.User.CompanyName, couponDiscount, planCoupon.PlanId, ref errorMessage, ref cardFingerPrint, ref customerResponseId, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode, ref card);

                                                    if (!isError)
                                                    {
                                                        if (couponDiscount != null)
                                                        {
                                                            ElioUsersPlanCoupons userCoupon = new ElioUsersPlanCoupons();
                                                            userCoupon.UserId = vSession.User.Id;
                                                            userCoupon.PlanCouponsId = planCoupon.Id;
                                                            userCoupon.Sysdate = DateTime.Now;
                                                            userCoupon.LastUpdate = DateTime.Now;

                                                            DataLoader<ElioUsersPlanCoupons> loader = new DataLoader<ElioUsersPlanCoupons>(session);
                                                            loader.Insert(userCoupon);

                                                            packetId = planCoupon.PacketId;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        divStartupPaymentWarningMsg.Visible = true;
                                                        LblStartupPaymentWarningMsgContent.Text = "Something went wrong. Please try again later";
                                                        Logger.DetailedError(string.Format("User {0} tried to register but something went wrong in Stripe", vSession.User.Id.ToString()));
                                                        return;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                divStartupPaymentWarningMsg.Visible = true;
                                                LblStartupPaymentWarningMsgContent.Text = "Something went wrong. Please try again later";
                                                Logger.DetailedError(string.Format("User {0} tried to register but coupon could not be found in Stripe", vSession.User.Id.ToString()));
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            divStartupPaymentWarningMsg.Visible = true;
                                            LblStartupPaymentWarningMsgContent.Text = "Coupon ID is wrong. Please try again";
                                            Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his coupon could not be found", vSession.User.Id.ToString()));
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        divStartupPaymentWarningMsg.Visible = true;
                                        LblStartupPaymentWarningMsgContent.Text = "Please type your Coupon ID in order to get discount";
                                        return;
                                    }
                                }
                                else
                                {
                                    //isError = StripeLib.GetInTrial(TbxEmail.Text, TbxCardNumber.Text, TbxCVC.Text, Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text), vSession.User.Id, vSession.User.CompanyName, ref errorMessage, ref cardFingerPrint, ref customerResponseId, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode);

                                    isError = StripeLib.SubscribeUnRegisteredCustomer(StripePlans.Elio_Premium_Plan.ToString(), TbxStartupEmail.Text, TbxStartupCardNumber.Text, TbxStartupCVC.Text, Convert.ToInt32(DrpStartupExpMonth.SelectedItem.Text), Convert.ToInt32(TbxStartupExpYear.Text), vSession.User.Id, vSession.User.CompanyName, ref errorMessage, ref cardFingerPrint, ref cardId, ref customerResponseId, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode, ref card);
                                }
                            }
                            else
                            {
                                isError = StripeLib.SubscribeRegisteredCustomer(StripePlans.Elio_Startup_Plan.ToString(), TbxStartupCardNumber.Text, TbxStartupCVC.Text, Convert.ToInt32(DrpStartupExpMonth.SelectedItem.Text), Convert.ToInt32(TbxStartupExpYear.Text), vSession.User.CustomerStripeId, ref errorMessage, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode, ref card);
                                customerResponseId = vSession.User.CustomerStripeId;
                            }

                            if (isError)
                            {
                                divStartupPaymentWarningMsg.Visible = true;
                                LblStartupPaymentWarningMsgContent.Text = errorMessage;
                                return;
                            }
                        }

                        if (startDate != null && ((trialPeriodStart != null && trialPeriodEnd != null) || (currentPeriodStart != null && currentPeriodEnd != null)))
                        {
                            try
                            {
                                session.BeginTransaction();

                                vSession.User = PaymentLib.MakeUserPremium(vSession.User, packetId, (couponDiscount != null) ? couponDiscount.PercentOff : null, customerResponseId, cardId, TbxStartupEmail.Text, startDate, trialPeriodStart, trialPeriodEnd, currentPeriodStart, currentPeriodEnd, orderMode, card, session);

                                session.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }

                            vSession.UserHasExpiredOrder = false;

                            divStartupPaymentSuccessMsg.Visible = true;
                            LblStartupPaymentSuccessMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "5")).Text;
                            BtnStartupPayment.Enabled = false;

                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "billing"), false);

                            try
                            {
                                EmailSenderLib.SendStripeTrialActivationEmail(TbxStartupEmail.Text, vSession.Lang, session);
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }

                            ClearFields();
                        }
                    }
                    else
                    {
                        BtnStartupPayment.Enabled = false;
                        divStartupPaymentWarningMsg.Visible = true;
                        LblStartupPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "6")).Text;
                    }
                }
            }
            catch (Exception ex)
            {
                divStartupPaymentWarningMsg.Visible = true;
                LblStartupPaymentWarningMsgContent.Text = "Something went wrong. Please try again later.";
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnCancelStartupPayment_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ClearFields();

                    ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "ClosePaymentPopUp();", true);
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

        protected void CbxStartupCouponDiscount_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                divStartupDiscount.Visible = (CbxStartupCouponDiscount.Checked) ? true : false;
                divStartupInfo.Visible = !divStartupDiscount.Visible;

                if (!CbxStartupCouponDiscount.Checked)
                {
                    LblStartupPaymentWarningMsgContent.Text = string.Empty;
                    divStartupPaymentWarningMsg.Visible = false;

                    LblStartupPaymentSuccessMsgContent.Text = string.Empty;
                    divStartupPaymentSuccessMsg.Visible = false;

                    BtnStartupPayment.Text = "Subscribe";

                    try
                    {
                        session.OpenConnection();

                        LblStartupTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.Premium), session).ToString() + " $";
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
                else
                {
                    BtnStartupPayment.Text = "Proceed";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}