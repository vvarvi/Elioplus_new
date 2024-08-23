using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.StripePayment;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Objects;
using Stripe;
using ServiceStack.Stripe.Types;

namespace WdS.ElioPlus.Controls.AlertControls
{
    public partial class CancelServiceOrderConfirmationMessageAlert : System.Web.UI.UserControl
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
                if (vSession.User != null)
                {
                    FixPage();
                    //UpdateStrings();
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
        }

        private void FixPage()
        {
            divGeneralSuccess.Visible = false;
            divGeneralFailure.Visible = false;
        }

        public void Refresh()
        {
            FixPage();
            //UpdateStrings();
        }

        #region Buttons

        protected void BtnProccedMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId))  //  && Sql.UserHasActiveOrderByPacketAndType(vSession.User.Id, Convert.ToInt32(OrderType.ServiceNewOrder), Convert.ToInt32(Packets.PremiumService), session))
                    {
                        bool cancelSubscriptionSuccess = false;

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

                                            vSession.User = PaymentLib.CancelCustomerSubscriptionAndMakeUserFreemium(vSession.User, subscription, true, session);

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

                                        return;
                                    }
                                }
                                else
                                {
                                    divGeneralFailure.Visible = true;
                                    LblGeneralFailure.Text = "Error! ";
                                    LblCancelFailure.Text = "Your plan could not be canceled. Please try again later or contact with us";

                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        #region Stripe Old Way

                        //ElioUsersStripeId userService = Sql.GetUserStripeServiceByUserId(vSession.User.Id, session);
                        //if (userService != null)
                        //{
                        //    ElioUsersCreditCards userCard = Sql.GetUserDefaultCreditCard(vSession.User.Id, userService.StripeServiceCustomerId, session);

                        //    if (userCard != null)
                        //    {
                        //        DateTime cancelAt = DateTime.Now;
                        //        successUnsubscription = PaymentLib.CancelUserService(vSession.User, userCard, session, out cancelAt);

                        //        Logger.Info(string.Format("User {0} canceled his premium service plan at {1} that expires at {2}. Don't forget to cancel from Stripe when it's time.", vSession.User.Id.ToString(), DateTime.Now.ToString(), cancelAt.ToString()));

                        //        if (successUnsubscription)
                        //        {
                        //            RadGrid rdgOrders = (RadGrid)ControlFinder.FindControlBackWards(this, "RdgOrders");
                        //            if (rdgOrders != null)
                        //            {
                        //                rdgOrders.Rebind();

                        //                UpdatePanel updatePnl = (UpdatePanel)ControlFinder.FindControlBackWards(this, "UpdatePnl");
                        //                updatePnl.Update();
                        //            }

                        //            divGeneralSuccess.Visible = true;
                        //            LblGeneralSuccess.Text = "Done! ";
                        //            LblCancelSuccess.Text = "Your service plan was canceled successfully!";
                        //            BtnProccedMessage.Visible = false;
                        //            BtnCancelMessage.Text = "Close";
                        //            LblMessage.Text = "Your Service Plan canceled successfully";
                        //            LblConfirm.Text = "Press Close to continue";
                        //        }
                        //        else
                        //        {
                        //            divGeneralFailure.Visible = true;
                        //            LblGeneralFailure.Text = "Error! ";
                        //            LblCancelFailure.Text = "Your service plan could not be canceled. Please try again later or contact with us";
                        //        }
                        //    }
                        //    else
                        //    {
                        //        divGeneralFailure.Visible = true;
                        //        LblGeneralFailure.Text = "Error! ";
                        //        LblCancelFailure.Text = "Your service plan could not be canceled. Please try again later or contact with us";
                        //        Logger.DetailedError(string.Format(Request.Url.ToString(), "User {0}, tried to unsubscribe unsuccessfully because he's card did not find!", vSession.User.Id.ToString()));
                        //    }
                        //}
                        //else
                        //{
                        //    divGeneralFailure.Visible = true;
                        //    LblGeneralFailure.Text = "Error! ";
                        //    LblCancelFailure.Text = "Your service plan could not be canceled. Please try again later or contact with us";
                        //    Logger.DetailedError(string.Format(Request.Url.ToString(), "User {0}, tried to unsubscribe unsuccessfully because he did not find stripe service customer Id!", vSession.User.Id.ToString()));
                        //}

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
                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmationServicePopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}