using System;
using System.Linq;
using System.Web.UI;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.StripePayment;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using Stripe;
using System.Data;
using ServiceStack.Stripe.Types;

namespace WdS.ElioPlus.Controls.Dashboard.AlertControls
{
    public partial class DACancelOrderConfirmationMessageAlert : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public string SubscriptionId
        {
            get
            {
                if (ViewState["SubscriptionId"] != null)
                    return ViewState["SubscriptionId"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["SubscriptionId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixPage();
                    UpdateStrings();
                }
                else
                {
                    Response.Redirect(ControlLoader.Default(), false);
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

        public void Refresh()
        {
            FixPage();
            UpdateStrings();
        }

        private void UpdateStrings()
        {
            bool mustCloseConnection = false;
            if (session.Connection.State == ConnectionState.Closed)
            {
                session.OpenConnection();
                mustCloseConnection = true;
            }

            ElioPackets packet = null;
            if (!string.IsNullOrEmpty(SubscriptionId))
            {
                packet = Sql.GetPacketDescriptionBySubscriptionID(SubscriptionId, session);
            }
            else
            {
                packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
            }

            if (packet != null)
            {
                LblMessage.Text = "You are going to cancel your " + packet.PackDescription + " Plan";
            }
            else
                LblMessage.Text = "You are going to cancel your Premium Plan";

            LblConfirm.Text = "Are you sure?";
            BtnCancelMessage.Text = "No";
            BtnProccedMessage.Text = "Yes, Cancel Anyway";

            if (mustCloseConnection)
                session.CloseConnection();
        }

        private void FixPage()
        {
            divGeneralSuccess.Visible = false;
            divGeneralFailure.Visible = false;

            BtnCancelMessage.Visible = BtnProccedMessage.Visible = true;
        }

        #region Buttons

        protected void BtnProccedMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                    {
                        #region Stripe Subscribed User

                        string stripeUnsubscribeError = string.Empty;
                        string defaultCreditCard = string.Empty;
                        bool cancelSubscriptionSuccess = false;

                        //ElioUsersCreditCards userCard = Sql.GetUserDefaultCreditCard(vSession.User.Id, vSession.User.CustomerStripeId, session);
                        //if (userCard != null)
                        //    defaultCreditCard = userCard.CardStripeId;

                        try
                        {
                            if (!string.IsNullOrEmpty(SubscriptionId))
                            {
                                //successUnsubscription = StripeLib.UnSubscribeCustomer(ref canceledAt, vSession.User.CustomerStripeId, defaultCreditCard, ref stripeUnsubscribeError);
                                Subscription subscription = Lib.Services.StripeAPI.StripeService.CancelSubscriptionNewApi(SubscriptionId);
                                if (subscription != null && subscription.CanceledAt != null)
                                {
                                    cancelSubscriptionSuccess = subscription.Status.ToString().ToLower() == "canceled";
                                }

                                ElioPackets packet = Sql.GetPacketDescriptionBySubscriptionID(SubscriptionId, session);
                                if (packet != null)
                                {
                                    if (cancelSubscriptionSuccess)
                                    {
                                        try
                                        {
                                            session.BeginTransaction();

                                            //string customerCard = subscription.Customer.DefaultSourceId;

                                            vSession.User = PaymentLib.CancelCustomerSubscriptionAndMakeUserFreemium(vSession.User, subscription, false, session);

                                            session.CommitTransaction();
                                        }
                                        catch (Exception ex)
                                        {
                                            session.RollBackTransaction();
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }

                                        RadGrid rdgOrders = (RadGrid)ControlFinder.FindControlBackWards(this, "RdgOrders");
                                        if (rdgOrders != null)
                                        {
                                            rdgOrders.Rebind();

                                            UpdatePanel updatePnl = (UpdatePanel)ControlFinder.FindControlBackWards(this, "UpdatePnl");
                                            updatePnl.Update();
                                        }

                                        divGeneralSuccess.Visible = true;
                                        LblGeneralSuccess.Text = "Done! ";
                                        LblCancelSuccess.Text = "Your " + packet.PackDescription + " plan was canceled successfully!";
                                        BtnProccedMessage.Visible = false;
                                        BtnCancelMessage.Text = "Close";
                                        LblMessage.Text = "Plan canceled";
                                        LblConfirm.Text = "Press Close to continue";
                                    }
                                    else
                                    {
                                        divGeneralFailure.Visible = true;
                                        LblGeneralFailure.Text = "Error! ";
                                        LblCancelFailure.Text = "Your " + packet.PackDescription + " plan could not be canceled. Please try again later or contact with us";

                                        Logger.DetailedError(Request.Url.ToString(), string.Format("User {0} tried to cancel his subscription at {1} but he has no stripe id. Maybe he is a virtual premium customer else ERROR", vSession.User.Id, DateTime.Now.ToString()));
                                    }
                                }
                                else
                                {
                                    divGeneralFailure.Visible = true;
                                    LblGeneralFailure.Text = "Error! ";
                                    LblCancelFailure.Text = "Your plan could not be canceled. Please try again later or contact with us";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }
                        
                        #endregion
                    }
                    else
                    {
                        #region User has no Stripe ID (Maybe he is Virtual Premium User)

                        divGeneralFailure.Visible = true;
                        LblGeneralFailure.Text = "Error! ";
                        LblCancelFailure.Text = "Your premium plan could not be canceled. Please try again later or contact with us";
                        Logger.DetailedError(string.Format(Request.Url.ToString(), "User {0}, tried to unsubscribe unsuccessfully because he has customer stripe Id null or empty!", vSession.User.Id.ToString()));

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

        protected void BtnCancelMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmationPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}