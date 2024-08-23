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
    public partial class Stripe_Ctrl : System.Web.UI.UserControl
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
                        LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.Premium), session).ToString() + " $";
                        TbxEmail.Text = vSession.User.Email;

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
            LblTotalCost.Text = (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && !string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "2")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "1")).Text;
        }

        private void FixState()
        {
            HasDiscount = Sql.HasUserDiscount(vSession.User.Id, session);
            PnlPayment.Enabled = !HasDiscount;
            BtnPayment.Enabled = (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? true : false;
        }

        public void ClearFields()
        {
            LblPaymentWarningMsgContent.Text = string.Empty;
            divPaymentWarningMsg.Visible = false;

            LblPaymentSuccessMsgContent.Text = string.Empty;
            divPaymentSuccessMsg.Visible = false;

            TbxEmail.Text = vSession.User.Email;
            TbxCardNumber.Text = string.Empty;
            DrpExpMonth.SelectedValue = "0";
            TbxExpYear.Text = string.Empty;
            TbxEmail.Text = string.Empty;
            TbxCVC.Text = string.Empty;

            BtnPayment.Enabled = (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? true : false;

            divDiscount.Visible = false;
            divInfo.Visible = true;
            CbxCouponDiscount.Checked = false;

            LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.Premium), session).ToString() + " $";
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

            if (CbxCouponDiscount.Checked)
            {
                if (string.IsNullOrEmpty(TbxDiscount.Text))
                {
                    LblPaymentWarningMsgContent.Text = "Please enter your Coupon ID";       //Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "8")).Text;
                    isValid = false;
                    return isValid;
                }
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
                                divPaymentWarningMsg.Visible = true;
                                return;
                            }
                            else
                            {
                                divPaymentWarningMsg.Visible = false;
                            }

                            bool isError = false;

                            if (string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                            {
                                if (CbxCouponDiscount.Checked)
                                {
                                    if (TbxDiscount.Text.Trim() != string.Empty)
                                    {
                                        ElioPlanCoupons planCoupon = Sql.GetPlanCoupon(TbxDiscount.Text.Trim().ToUpper(), session);

                                        if (planCoupon != null)
                                        {
                                            couponDiscount = StripeLib.CreateGetCoupon(Xamarin.Payments.Stripe.StripeCouponDuration.Repeating, planCoupon.MaxRedemptions, planCoupon.CouponId, planCoupon.PercentOff, planCoupon.MonthDuration, planCoupon.RedeemBy);

                                            if (couponDiscount != null)
                                            {
                                                if (BtnPayment.Text == "Proceed")
                                                {
                                                    decimal totalCost = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.Premium), session);
                                                    //LblTotalCostValue.Text = (planCoupon.AmountOff == null || planCoupon.AmountOff == 0) ? (totalCost - ((planCoupon.PercentOff * totalCost) / 100)).ToString() : Convert.ToDecimal(planCoupon.AmountOff) + " $";
                                                    LblTotalCostValue.Text = (couponDiscount.AmountOff == null || couponDiscount.AmountOff == 0) ? (totalCost - ((couponDiscount.PercentOff * totalCost) / 100)).ToString() : (totalCost - Convert.ToDecimal(couponDiscount.AmountOff) / 100).ToString() + " $";
                                                    divInfo.Visible = false;
                                                    BtnPayment.Text = "Subscribe";

                                                    return;
                                                }
                                                else if (BtnPayment.Text == "Subscribe")
                                                {
                                                    isError = StripeLib.SubscribeUnRegisteredCustomerWithCoupon(TbxEmail.Text, TbxCardNumber.Text, TbxCVC.Text, Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text), vSession.User.Id, vSession.User.CompanyName, couponDiscount, planCoupon.PlanId, ref errorMessage, ref cardFingerPrint, ref customerResponseId, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode, ref card);

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
                                                        divPaymentWarningMsg.Visible = true;
                                                        LblPaymentWarningMsgContent.Text = "Something went wrong. Please try again later";
                                                        Logger.DetailedError(string.Format("User {0} tried to register but something went wrong in Stripe", vSession.User.Id.ToString()));
                                                        return;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                divPaymentWarningMsg.Visible = true;
                                                LblPaymentWarningMsgContent.Text = "Something went wrong. Please try again later";
                                                Logger.DetailedError(string.Format("User {0} tried to register but coupon could not be found in Stripe", vSession.User.Id.ToString()));
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            divPaymentWarningMsg.Visible = true;
                                            LblPaymentWarningMsgContent.Text = "Coupon ID is wrong. Please try again";
                                            Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his coupon could not be found", vSession.User.Id.ToString()));
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        divPaymentWarningMsg.Visible = true;
                                        LblPaymentWarningMsgContent.Text = "Please type your Coupon ID in order to get discount";
                                        return;
                                    }
                                }
                                else
                                {
                                    //isError = StripeLib.GetInTrial(TbxEmail.Text, TbxCardNumber.Text, TbxCVC.Text, Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text), vSession.User.Id, vSession.User.CompanyName, ref errorMessage, ref cardFingerPrint, ref customerResponseId, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode);

                                    isError = StripeLib.SubscribeUnRegisteredCustomer(StripePlans.Elio_Premium_Plan.ToString(), TbxEmail.Text, TbxCardNumber.Text, TbxCVC.Text, Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text), vSession.User.Id, vSession.User.CompanyName, ref errorMessage, ref cardFingerPrint, ref cardId, ref customerResponseId, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode, ref card);
                                }
                            }
                            else
                            {
                                isError = StripeLib.SubscribeRegisteredCustomer(StripePlans.Elio_Premium_Plan.ToString(), TbxCardNumber.Text, TbxCVC.Text, Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text), vSession.User.CustomerStripeId, ref errorMessage, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode, ref card);
                                customerResponseId = vSession.User.CustomerStripeId;
                            }

                            if (isError)
                            {
                                divPaymentWarningMsg.Visible = true;
                                LblPaymentWarningMsgContent.Text = errorMessage;
                                return;
                            }
                        }

                        if (startDate != null && ((trialPeriodStart != null && trialPeriodEnd != null) || (currentPeriodStart != null && currentPeriodEnd != null)))
                        {
                            try
                            {
                                session.BeginTransaction();

                                vSession.User = PaymentLib.MakeUserPremium(vSession.User, packetId, (couponDiscount != null) ? couponDiscount.PercentOff : null, customerResponseId, cardId, TbxEmail.Text, startDate, trialPeriodStart, trialPeriodEnd, currentPeriodStart, currentPeriodEnd, orderMode, card, session);

                                session.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }

                            vSession.UserHasExpiredOrder = false;

                            divPaymentSuccessMsg.Visible = true;
                            LblPaymentSuccessMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "5")).Text;
                            BtnPayment.Enabled = false;

                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "billing"), false);

                            try
                            {
                                EmailSenderLib.SendStripeTrialActivationEmail(TbxEmail.Text, vSession.Lang, session);
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
                        BtnPayment.Enabled = false;
                        divPaymentWarningMsg.Visible = true;
                        LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "6")).Text;
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

        protected void CbxCouponDiscount_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                divDiscount.Visible = (CbxCouponDiscount.Checked) ? true : false;
                divInfo.Visible = !divDiscount.Visible;

                if (!CbxCouponDiscount.Checked)
                {
                    LblPaymentWarningMsgContent.Text = string.Empty;
                    divPaymentWarningMsg.Visible = false;

                    LblPaymentSuccessMsgContent.Text = string.Empty;
                    divPaymentSuccessMsg.Visible = false;

                    BtnPayment.Text = "Subscribe";

                    try
                    {
                        session.OpenConnection();

                        LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.Premium), session).ToString() + " $";
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
                    BtnPayment.Text = "Proceed";
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