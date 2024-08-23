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

namespace WdS.ElioPlus.Controls.Payment
{
    public partial class Stripe_Packets_Ctrl_Old : System.Web.UI.UserControl
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

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        LoadPackets((int)StripePlans.Elio_Startup_Plan);

                        TbxEmail.Text = vSession.User.Email;
                        BtnPayment.Enabled = true;
                        UpdateStrings();

                        divInfo.Visible = false;  //(!string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? false : true;
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

        protected void LoadPackets(int selectedPlan)
        {
            List<ElioPackets> packets = Sql.GetDefaultActivePackets(session);

            DrpStripePlans.Items.Clear();

            //item.Value = "0";
            //item.Text = "Select your Plan";

            //DrpStripePlans.Items.Add(item);

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

            if (vSession.User != null)
                TbxEmail.Text = vSession.User.Email;
            else
                TbxEmail.Text = string.Empty;

            //LblServiceTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(StripePlans.Elio_Premium_Service_Plan), session).ToString() + " $";
        }

        protected void LoadPackets12()
        {
            List<ElioPackets> packets = Sql.GetDefaultActivePackets(session);

            DrpStripePlans.Items.Clear();

            //ListItem item = new ListItem();
            //item.Value = "0";
            //item.Text = "Select your Plan";

            //DrpStripePlans.Items.Add(item);

            foreach (ElioPackets packet in packets)
            {
                ListItem item = new ListItem();
                item.Value = packet.Id.ToString();
                item.Text = packet.PackDescription;

                DrpStripePlans.Items.Add(item);
            }

            //if (PacketId != null)
            //    DrpStripePlans.SelectedValue = PacketId.ToString();

            LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session).ToString() + " $";
        }

        private void UpdateStrings()
        {
            LblTotalCost.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "4")).Text;    //(vSession.User.BillingType == Convert.ToInt32(BillingType.Freemium) && !string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "2")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "payment", "label", "1")).Text;
        }

        private void FixState()
        {
            HasDiscount = Sql.HasUserDiscount(vSession.User.Id, session);
            //PnlPayment.Enabled = !HasDiscount;
            BtnPayment.Enabled = (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? false : true;
        }

        public void ClearFields()
        {
            LblPaymentWarningMsgContent.Text = string.Empty;
            divPaymentWarningMsg.Visible = false;

            LblPaymentSuccessMsgContent.Text = string.Empty;
            divPaymentSuccessMsg.Visible = false;

            if (vSession.User != null)
            {
                TbxEmail.Text = vSession.User.Email;
                BtnPayment.Enabled = (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? false : true;
            }
            else
            {
                TbxEmail.Text = string.Empty;
                BtnPayment.Enabled = false;
            }

            DrpStripePlans.SelectedIndex = -1;
            TbxCardNumber.Text = string.Empty;
            DrpExpMonth.SelectedValue = "0";
            TbxExpYear.Text = string.Empty;
            TbxCVC.Text = string.Empty;

            divDiscount.Visible = false;
            divInfo.Visible = false;
            CbxCouponDiscount.Checked = false;

            LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session).ToString() + " $";
        }

        private bool IsValidData()
        {
            bool isValid = true;

            //if (DrpStripePlans.SelectedValue == "0")
            //{
            //    LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "9")).Text;
            //    isValid = false;
            //    return isValid;
            //}

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

        private bool HasUserDiscountCoupon(int planCouponsId, int selectedPacketId)
        {
            if (vSession.User != null)
            {
                return Sql.HasUserDiscountCouponByPlan(vSession.User.Id, planCouponsId, selectedPacketId, session);
            }
            else
            {
                Response.Redirect(ControlLoader.Login, false);
                return false;
            }
        }

        #endregion

        #region Buttons

        protected void BtnPayment_OnClick(object sender, EventArgs args)
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

                        if (CbxCouponDiscount.Checked)
                        {
                            if (TbxDiscount.Text.Trim() != string.Empty)
                            {
                                if (TbxDiscount.Text.Trim().ToLower() != "saastock2019startup" && TbxDiscount.Text.Trim().ToLower() != "saastock2019growth")
                                {
                                    #region regular coupon case

                                    ElioPacketsStripeCoupons planCoupon = Sql.GetPacketStripeCoupon(TbxDiscount.Text.Trim().ToUpper(), session);
                                    if (planCoupon != null)
                                    {
                                        ElioPackets packet = Sql.GetPacketById(selectedPacketId, session);
                                        if (packet != null)
                                        {
                                            if (packet.stripePlanId == planCoupon.StripePlanId)
                                            {
                                                bool hasCouponError = true;
                                                Coupon stripeCoupon = Lib.Services.StripeAPI.StripeAPIService.GetCouponNewApi(planCoupon.CouponId);
                                                if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                {
                                                    hasCouponError = false;
                                                }
                                                else
                                                {
                                                    stripeCoupon = Lib.Services.StripeAPI.StripeAPIService.CreateCouponNewApi(planCoupon.CouponId, planCoupon.Name, planCoupon.Duration, (long)planCoupon.AmountOff, planCoupon.Currency, planCoupon.DurationInMonths, planCoupon.MaxRedemptions, (long)planCoupon.PercentOff, planCoupon.RedeemBy);
                                                    if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                    {
                                                        hasCouponError = false;
                                                    }
                                                    else
                                                    {
                                                        //cooupon could not be created
                                                        hasCouponError = true;
                                                        divPaymentWarningMsg.Visible = true;
                                                        LblPaymentWarningMsgContent.Text = "Something went wrong with your Coupon. Please try again or contact us";
                                                        Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his coupon ({1}) could not be created in Stripe", vSession.User.Id.ToString(), TbxDiscount.Text.Trim().ToUpper()));
                                                        return;
                                                    }
                                                }

                                                if (!hasCouponError)
                                                {
                                                    if (stripeCoupon.TimesRedeemed < planCoupon.MaxRedemptions)
                                                    {
                                                        if ((stripeCoupon.RedeemBy == null || stripeCoupon.RedeemBy != null && Convert.ToDateTime(stripeCoupon.RedeemBy) >= DateTime.Now))
                                                        {
                                                            divPaymentSuccessMsg.Visible = true;
                                                            LblPaymentSuccessMsgContent.Text = "Coupon found successfully";

                                                            if (BtnPayment.Text == "Proceed")
                                                            {
                                                                #region Step 1: Proceed

                                                                Plan plan = Lib.Services.StripeAPI.StripeAPIService.GetPlanNewApi(packet.stripePlanId);
                                                                if (plan != null && !string.IsNullOrEmpty(plan.Id))
                                                                {
                                                                    decimal totalCost = Convert.ToDecimal(plan.Amount) / 100;       //Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session);
                                                                    LblTotalCostValue.Text = (stripeCoupon.AmountOff == null || stripeCoupon.AmountOff == 0) ? (totalCost - ((stripeCoupon.PercentOff * totalCost) / 100)).ToString() + " $" : (totalCost - Convert.ToDecimal(stripeCoupon.AmountOff) / 100).ToString() + " $";
                                                                    //divInfo.Visible = false;
                                                                    BtnPayment.Text = "Subscribe";

                                                                    return;
                                                                }
                                                                else
                                                                {
                                                                    divPaymentWarningMsg.Visible = true;
                                                                    LblPaymentWarningMsgContent.Text = "Something went wrong with your Plan activation. Please try again or contact us";
                                                                    Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his plan ({1}) could not be found in Stripe for packet amount", vSession.User.Id.ToString(), packet.stripePlanId));

                                                                    return;
                                                                }

                                                                #endregion
                                                            }
                                                            else if (BtnPayment.Text == "Subscribe")
                                                            {
                                                                #region Step 2 Subscribe

                                                                //continue to subscribe in Stripe

                                                                #endregion
                                                            }
                                                        }
                                                        else
                                                        {
                                                            divPaymentWarningMsg.Visible = true;
                                                            LblPaymentWarningMsgContent.Text = "Your Coupon has expired. Please contact us";
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        divPaymentWarningMsg.Visible = true;
                                                        LblPaymentWarningMsgContent.Text = "Your Coupon can not be used any more. Please contact us";
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    divPaymentWarningMsg.Visible = true;
                                                    LblPaymentWarningMsgContent.Text = "Something went wrong with your Coupon. Please try again or contact us";
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                divPaymentWarningMsg.Visible = true;
                                                LblPaymentWarningMsgContent.Text = "Coupon is not valid for selected Plan. Please try again";
                                                return;
                                            }
                                        }
                                        else
                                        {

                                            return;
                                        }
                                    }
                                    else
                                    {
                                        divPaymentWarningMsg.Visible = true;
                                        LblPaymentWarningMsgContent.Text = "Coupon is not valid. Please try again";
                                        Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his coupon could not be found in Elio DB", vSession.User.Id.ToString()));
                                        return;
                                    }

                                    #endregion
                                }
                                else if (TbxDiscount.Text.Trim().ToLower() == "saastock2019startup" || TbxDiscount.Text.Trim().ToLower() == "saastock2019growth")
                                {
                                    #region Saastock 2019 Elioplus Event Discount Coupon

                                    bool hasCouponError = true;

                                    ElioPackets packet = Sql.GetPacketById(selectedPacketId, session);
                                    if (packet != null)
                                    {
                                        ElioPacketsStripeCoupons planCoupon = Sql.GetPacketStripeCouponByStripePlan(TbxDiscount.Text.Trim().ToUpper(), packet.stripePlanId, session);
                                        if (planCoupon != null)
                                        {
                                            Coupon stripeCoupon = Lib.Services.StripeAPI.StripeAPIService.GetCouponNewApi(planCoupon.CouponId);

                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                            {
                                                hasCouponError = false;
                                            }
                                            else
                                            {
                                                stripeCoupon = Lib.Services.StripeAPI.StripeAPIService.CreateCouponNewApi(planCoupon.CouponId, planCoupon.Name, planCoupon.Duration, (long)planCoupon.AmountOff, planCoupon.Currency, planCoupon.DurationInMonths, planCoupon.MaxRedemptions, (long)planCoupon.PercentOff, planCoupon.RedeemBy);
                                                if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                {
                                                    hasCouponError = false;
                                                }
                                                else
                                                {
                                                    //cooupon could not be created
                                                    hasCouponError = true;
                                                    divPaymentWarningMsg.Visible = true;
                                                    LblPaymentWarningMsgContent.Text = "Something went wrong with your Coupon. Please try again or contact us";
                                                    Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his coupon ({1}) could not be created in Stripe", vSession.User.Id.ToString(), TbxDiscount.Text.Trim().ToUpper()));
                                                    return;
                                                }
                                            }

                                            if (!hasCouponError)
                                            {
                                                if (stripeCoupon.TimesRedeemed < planCoupon.MaxRedemptions)
                                                {
                                                    if ((stripeCoupon.RedeemBy == null || (stripeCoupon.RedeemBy != null && Convert.ToDateTime(stripeCoupon.RedeemBy) >= DateTime.Now)))
                                                    {
                                                        divPaymentSuccessMsg.Visible = true;
                                                        LblPaymentSuccessMsgContent.Text = "Coupon found successfully";

                                                        if (BtnPayment.Text == "Proceed")
                                                        {
                                                            #region Step 1: Proceed

                                                            Plan plan = Lib.Services.StripeAPI.StripeAPIService.GetPlanNewApi(packet.stripePlanId);
                                                            if (plan != null && !string.IsNullOrEmpty(plan.Id))
                                                            {
                                                                decimal totalCost = Convert.ToDecimal(plan.Amount) / 100;       //Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session);
                                                                LblTotalCostValue.Text = (stripeCoupon.AmountOff == null || stripeCoupon.AmountOff == 0) ? (totalCost - ((stripeCoupon.PercentOff * totalCost) / 100)).ToString() + " $" : (totalCost - Convert.ToDecimal(stripeCoupon.AmountOff) / 100).ToString() + " $";
                                                                //divInfo.Visible = false;
                                                                BtnPayment.Text = "Subscribe";

                                                                return;
                                                            }
                                                            else
                                                            {
                                                                divPaymentWarningMsg.Visible = true;
                                                                LblPaymentWarningMsgContent.Text = "Something went wrong with your Plan activation. Please try again or contact us";
                                                                Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his plan ({1}) could not be found in Stripe for packet amount", vSession.User.Id.ToString(), packet.stripePlanId));

                                                                return;
                                                            }

                                                            #endregion
                                                        }
                                                        else if (BtnPayment.Text == "Subscribe")
                                                        {
                                                            #region Step 2 Subscribe

                                                            //continue to subscribe in Stripe

                                                            #endregion
                                                        }
                                                    }
                                                    else
                                                    {
                                                        divPaymentWarningMsg.Visible = true;
                                                        LblPaymentWarningMsgContent.Text = "Your Coupon has expired. Please contact us";
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    divPaymentWarningMsg.Visible = true;
                                                    LblPaymentWarningMsgContent.Text = "Your Coupon can not be used any more. Please contact us";
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                divPaymentWarningMsg.Visible = true;
                                                LblPaymentWarningMsgContent.Text = "Something went wrong with your Coupon. Please try again or contact us";
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            divPaymentWarningMsg.Visible = true;
                                            LblPaymentWarningMsgContent.Text = "Coupon is not valid. Please try again";
                                            Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his coupon could not be found in Elio DB", vSession.User.Id.ToString()));
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }

                                    #endregion
                                }
                            }
                        }

                        session.BeginTransaction();

                        ElioUsers user = vSession.User;
                        isSuccess = StripeApi.PaymentMethodNew(out user, vSession.User.Id, selectedPacketId, TbxCardNumber.Text, DrpExpMonth.SelectedItem.Text, TbxExpYear.Text, TbxCVC.Text, TbxDiscount.Text, session);
                        vSession.User = user;

                        session.CommitTransaction();

                        if (!isSuccess)
                        {
                            divPaymentWarningMsg.Visible = true;
                            LblPaymentWarningMsgContent.Text = "Something went wrong! Please try again later or contact us";

                            return;
                        }
                        else
                        {
                            ClearFields();

                            divPaymentSuccessMsg.Visible = true;
                            LblPaymentSuccessMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "5")).Text;
                            BtnPayment.Enabled = false;

                            //Response.Redirect(ControlLoader.Dashboard(vSession.User, "billing"), false);

                            try
                            {
                                //EmailSenderLib.SendStripeTrialActivationEmail(TbxEmail.Text, vSession.Lang, session);
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }
                        }

                        return;

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User

                        BtnPayment.Enabled = false;
                        divPaymentWarningMsg.Visible = true;
                        LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "6")).Text;

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();

                #region Error

                divPaymentWarningMsg.Visible = true;
                LblPaymentWarningMsgContent.Text = "Something went wrong. Please try again later.";
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                if (!string.IsNullOrEmpty(customerResponseId))
                {
                    Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                    try
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        if (!string.IsNullOrEmpty(customerResponseId))
                        {
                            Xamarin.Payments.Stripe.StripeSubscription customerSubscription = payment.Unsubscribe(customerResponseId, false);
                            Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.DeleteCustomer(customerResponseId);
                        }
                    }
                    catch (Xamarin.Payments.Stripe.StripeException exDeleteCustomer)
                    {
                        //errorMessage = exDeleteCustomer.Message;
                        Logger.DetailedError(Request.Url.ToString(), exDeleteCustomer.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                #endregion
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

        protected void CbxCouponDiscount_OnCheckedChanged(object sender, EventArgs args)                         
        {
            try
            {
                divDiscount.Visible = (CbxCouponDiscount.Checked) ? true : false;
                //divInfo.Visible = !divDiscount.Visible;
                TbxDiscount.Text = "";

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

                        ElioPackets packet = Sql.GetPacketById(Convert.ToInt32(DrpStripePlans.SelectedValue), session);
                        if (packet != null)
                        {
                            Plan plan = Lib.Services.StripeAPI.StripeAPIService.GetPlanNewApi(packet.stripePlanId);
                            if (plan != null && !string.IsNullOrEmpty(plan.Id))
                            {
                                LblTotalCostValue.Text = (Convert.ToDecimal(plan.Amount) / 100).ToString() + " $";      //Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session).ToString() + " $";
                            }
                            else
                                LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session).ToString() + " $";
                        }
                        else
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

        protected void CbxService_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                //session.OpenConnection();

                //if (CbxService.Checked)
                //{
                //    LblPaymentWarningMsgContent.Text = string.Empty;
                //    divPaymentWarningMsg.Visible = false;

                //    LblPaymentSuccessMsgContent.Text = string.Empty;
                //    divPaymentSuccessMsg.Visible = false;

                //    decimal planCost = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session);
                //    decimal serviceCost = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(StripePlans.Elio_Premium_Service_Plan), session);
                //    decimal totalCost = planCost + serviceCost;

                //    LblTotalCostValue.Text = totalCost.ToString() + " $";
                //}
                //else
                //{
                //    LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session).ToString() + " $";
                //}
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

                BtnPayment.Text = "Subscribe";
                CbxCouponDiscount.Checked = false;
                TbxDiscount.Text = "";
                divDiscount.Visible = false;
                //if (CbxCouponDiscount.Checked)
                //{
                //    if (TbxDiscount.Text.Trim() != string.Empty)
                //    {
                //        ElioPlanCoupons planCoupon = Sql.GetPlanCoupon(TbxDiscount.Text.Trim().ToUpper(), session);

                //        if (planCoupon != null)
                //        {
                //            if (planCoupon.CouponId == planCoupon.CouponId)
                //            {
                //                Xamarin.Payments.Stripe.StripeCoupon couponDiscount = new Xamarin.Payments.Stripe.StripeCoupon();
                //                couponDiscount.PercentOff = planCoupon.PercentOff;
                //                couponDiscount.Duration = Xamarin.Payments.Stripe.StripeCouponDuration.Repeating;
                //                couponDiscount.ID = planCoupon.CouponId;
                //                couponDiscount.TimesRedeemed = planCoupon.MaxRedemptions;

                //                //couponDiscount = StripeLib.CreateGetCoupon(Xamarin.Payments.Stripe.StripeCouponDuration.Repeating, planCoupon.MaxRedemptions, planCoupon.CouponId, planCoupon.PercentOff, planCoupon.MonthDuration, planCoupon.RedeemBy);

                //                if (couponDiscount != null)
                //                {
                //                    //    if (BtnPayment.Text == "Proceed")
                //                    //    {
                //                    decimal totalCost = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session);
                //                    LblTotalCostValue.Text = (couponDiscount.AmountOff == null || couponDiscount.AmountOff == 0) ? (totalCost - ((couponDiscount.PercentOff * totalCost) / 100)).ToString() + " $" : (totalCost - Convert.ToDecimal(couponDiscount.AmountOff) / 100).ToString() + " $";
                //                    divInfo.Visible = false;
                //                    //BtnPayment.Text = "Subscribe";
                //                    return;
                //                    //}
                //                }
                //            }
                //        }
                //    }
                //}
                //else
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