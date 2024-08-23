using System;
using System.Linq;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DB;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.Utils;
using System.Web;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.CurrencyConverter;
using WdS.ElioPlus.Lib.Analytics;
using System.Globalization;
using System.IO;
using System.Threading;
using WdS.ElioPlus.Controls.AlertControls;
using WdS.ElioPlus.Controls.Dashboard.AlertControls;
using Stripe.Checkout;

namespace WdS.ElioPlus.Lib.DBQueries
{
    public class GlobalDBMethods
    {
        public static bool UpdateUserTrainingPathDirectoryAndMoveFiles(HttpServerUtility Server, int userId, int categoryId, string oldDirectoryDescription, string newDirectoryDescription, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategoriesCourses> loader = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);

            List<ElioUsersTrainingCategoriesCourses> courses = loader.Load(@"SELECT *
                                                                            FROM Elio_users_training_categories_courses
                                                                            where category_id = @category_id"
                                                            , DatabaseHelper.CreateIntParameter("@category_id", categoryId));

            foreach (ElioUsersTrainingCategoriesCourses course in courses)
            {
                course.OverviewImagePath = course.OverviewImagePath.Replace(oldDirectoryDescription, newDirectoryDescription);

                course.LastUpDated = DateTime.Now;

                loader.Update(course);

                string userGuid = course.OverviewImagePath.Split('/')[2].ToString();

                string serverMapPathTargetFolderFrom = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["TrainingLibraryTargetFolder"].ToString()) + userGuid + "\\" + oldDirectoryDescription + "\\";
                string serverMapPathTargetFolderTo = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["TrainingLibraryTargetFolder"].ToString()) + userGuid + "\\" + newDirectoryDescription + "\\";

                if (Directory.Exists(serverMapPathTargetFolderFrom))
                    Directory.Move(serverMapPathTargetFolderFrom, serverMapPathTargetFolderTo);
            }

            DataLoader<ElioUsersTrainingCoursesChapters> loaderChpt = new DataLoader<ElioUsersTrainingCoursesChapters>(session);

            List<ElioUsersTrainingCoursesChapters> chapters = loaderChpt.Load(@"SELECT *
                                                                            FROM Elio_users_training_courses_chapters
                                                                            where category_id = @category_id"
                                                            , DatabaseHelper.CreateIntParameter("@category_id", categoryId));

            foreach (ElioUsersTrainingCoursesChapters chapter in chapters)
            {
                chapter.ChapterFilePath = chapter.ChapterFilePath.Replace(oldDirectoryDescription, newDirectoryDescription);

                chapter.LastUpDated = DateTime.Now;

                loaderChpt.Update(chapter);
            }

            return true;
        }

        public static string CheckOut(int packId, ElioUsers user, DBSession session)
        {
            string paymentUrl = "";

            if (user != null)
            {
                ElioPackets packet = Sql.GetPacketById(packId, session);
                if (packet != null)
                {
                    bool setEpiredSess = Sql.SetUSerCheckoutSessionsExpired(user.Id, session);
                    if (!setEpiredSess)
                    {
                        throw new Exception(string.Format("User {0} could not proceed to payment on Stripe at {1}", user.Id, DateTime.Now));
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

                        paymentUrl = chSession.Url;
                    }
                }
            }

            return paymentUrl;
        }

        public static void ShowMessageDA(DAMessageControl ucMessage1, DAMessageControl ucMessage2, MessageTypes msgType, string content)
        {
            if (ucMessage1 != null)
                GlobalMethods.ShowMessageControlDA(ucMessage1, content, msgType, true, true, false, false, false);
            else
                ucMessage1.Visible = false;

            if (ucMessage2 != null)
                GlobalMethods.ShowMessageControlDA(ucMessage2, content, msgType, true, true, false, false, false);
            else
                ucMessage2.Visible = false;
        }

        public static void ShowMessage(MessageControl ucMessage1, MessageControl ucMessage2, MessageTypes msgType, string content)
        {
            if (ucMessage1 != null)
                GlobalMethods.ShowMessageControl(ucMessage1, content, msgType, true, true, false, false, false);
            else
                ucMessage1.Visible = false;

            if (ucMessage2 != null)
                GlobalMethods.ShowMessageControl(ucMessage2, content, msgType, true, true, false, false, false);
            else
                ucMessage2.Visible = false;
        }

        public static bool AddUserConnectionsInstantlyDA(int userId, int connectionId, bool insertConnection, DAMessageControl UcMessageAlert1, DAMessageControl UcMessageAlert2, DBSession session)
        {
            ElioUsers user = Sql.GetUserById(userId, session);

            if (user != null)
            {
                if (user.CompanyType == Types.Vendors.ToString())
                {
                    #region Vendor

                    if (insertConnection)
                    {
                        #region Add Connection to Vendor

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            #region Only Not Free User

                            bool isAlreadyConnection = Sql.IsConnection(user.Id, connectionId, session);
                            if (!isAlreadyConnection)
                            {
                                ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                                if (vendorPacketFeatures != null)
                                {
                                    if (vendorPacketFeatures.ExpirationDate <= DateTime.Now)
                                    {
                                        #region Packet Status Features need update

                                        ElioUsersSubscriptions userSubscription = Sql.GetUserSubscription(user.Id, user.CustomerStripeId, session);
                                        if (userSubscription != null)
                                        {
                                            int packId = Sql.GetPacketIdBySubscriptionID(userSubscription.SubscriptionId, session);
                                            if (packId > 0)
                                            {
                                                if (packId == (int)Packets.PremiumDiscountNoTrial || packId == (int)Packets.PremiumDiscountTrial || packId == (int)Packets.PremiumtNoTrial25 || packId == (int)Packets.PremiumDiscount20 || packId == (int)Packets.Premium_No_Trial)
                                                    packId = (int)Packets.Premium;

                                                ElioPackets packet = Sql.GetPacketById(packId, session);
                                                if (packet != null && packet.Id != (int)Packets.PremiumService)
                                                {
                                                    List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(packet.Id, session);
                                                    if (items.Count > 0)
                                                    {
                                                        #region Get Packet Features Items

                                                        int totalLeads = 0;
                                                        int totalMessages = 0;
                                                        int totalConnections = 0;
                                                        int totalManagePartners = 0;
                                                        int totalLibraryStorage = 0;

                                                        for (int i = 0; i < items.Count; i++)
                                                        {
                                                            if (items[i].ItemDescription == "Leads")
                                                            {
                                                                totalLeads = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "Messages")
                                                            {
                                                                totalMessages = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "Connections")
                                                            {
                                                                totalConnections = items[i].FreeItemsNo;        // (order.Mode == OrderMode.Trialing.ToString()) ? items[i].FreeItemsTrialNo : items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "ManagePartners")
                                                            {
                                                                totalManagePartners = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "LibraryStorage")
                                                            {
                                                                totalLibraryStorage = items[i].FreeItemsNo;
                                                            }
                                                        }

                                                        #endregion

                                                        #region Get User Already Supplied Leads/Messages/Connections for Current Period

                                                        int totalUserLeads = Sql.GetUserLeadsCountByMonthRange(user, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);                            //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserMessages = Sql.GetUserSendMessagesCountByMonthRange(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);               //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserConnections = Sql.GetUserViewableConnectionsForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);      //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserInvitations = Sql.GetUserInvitationsForCurrentPeriod(user.Id, CollaborateInvitationStatus.Confirmed.ToString(), userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);
                                                        int totalUserFilesSize = Sql.GetUserLibraryFilesStorageForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);

                                                        double totalUserFileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(totalUserFilesSize), "GB");

                                                        #endregion

                                                        #region Insert / Update Packet Status Features

                                                        ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);

                                                        DataLoader<ElioUserPacketStatus> loader4 = new DataLoader<ElioUserPacketStatus>(session);

                                                        if (packetFeatures == null)
                                                        {
                                                            packetFeatures = new ElioUserPacketStatus();

                                                            packetFeatures.UserId = user.Id;
                                                            packetFeatures.PackId = items[0].Id;
                                                            packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);
                                                            packetFeatures.AvailableLeadsCount = totalLeads;
                                                            packetFeatures.AvailableMessagesCount = totalMessages;
                                                            packetFeatures.AvailableConnectionsCount = totalConnections;
                                                            packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                                                            packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                                                            packetFeatures.Sysdate = DateTime.Now;
                                                            packetFeatures.LastUpdate = DateTime.Now;
                                                            packetFeatures.StartingDate = Convert.ToDateTime(userSubscription.CurrentPeriodStart);       //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                                                            packetFeatures.ExpirationDate = Convert.ToDateTime(userSubscription.CurrentPeriodEnd);    //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                                                            loader4.Insert(packetFeatures);
                                                        }
                                                        else
                                                        {
                                                            packetFeatures.PackId = items[0].Id;
                                                            packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);
                                                            packetFeatures.AvailableLeadsCount = totalLeads;
                                                            packetFeatures.AvailableMessagesCount = totalMessages;
                                                            packetFeatures.AvailableConnectionsCount = totalConnections;
                                                            packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                                                            packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                                                            packetFeatures.LastUpdate = DateTime.Now;
                                                            packetFeatures.StartingDate = Convert.ToDateTime(userSubscription.CurrentPeriodStart);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                                                            packetFeatures.ExpirationDate = Convert.ToDateTime(userSubscription.CurrentPeriodEnd);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                                                            loader4.Update(packetFeatures);
                                                        }

                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }

                                    vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);

                                    if (vendorPacketFeatures.AvailableConnectionsCount > 0)
                                    {
                                        #region Vendor Side

                                        bool hasNoSubscription = false;
                                        string ids = (ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"] != null && ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"] != "") ? ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"].ToString() : "";

                                        if (ids != "")
                                        {
                                            string[] customers = ids.Split(',').ToArray();
                                            foreach (string userID in customers)
                                            {
                                                if (Convert.ToInt32(userID) == userId)
                                                {
                                                    hasNoSubscription = true;
                                                    break;
                                                }
                                            }
                                        }

                                        if (!hasNoSubscription)
                                        {
                                            #region Normal Stripe Case

                                            #region Get User Subscription

                                            ElioUsersSubscriptions sub = Sql.GetUserSubscription(user.Id, user.CustomerStripeId, session);
                                            //ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);

                                            //if (order == null)
                                            //{
                                            //    order = Sql.HasUserOrderByServicePacketStatusUse(user.Id, Convert.ToInt32(Packets.PremiumService), Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                            //}

                                            #endregion

                                            if (sub != null)
                                            {
                                                if (sub.Status.ToLower() != "active")   //custom for now
                                                    Logger.Debug("GlobalDBMethods.cs --> AddUserConnectionsInstantly()", string.Format("GlobalDBMethods.cs --> MESSAGE: Admin added connection ID:{0}, to user {1} at {2}, but his subscription status is {3}", connectionId, user.Id, DateTime.Now.ToString(), sub.SubscriptionId), "Connection added successfully but subscription need to be updated");

                                                DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                                #region Add Vendor Connection

                                                ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                                vendorConnection.UserId = userId;
                                                vendorConnection.ConnectionId = connectionId;
                                                vendorConnection.SysDate = DateTime.Now;
                                                vendorConnection.LastUpdated = DateTime.Now;
                                                vendorConnection.CanBeViewed = 1;
                                                vendorConnection.CurrentPeriodStart = Convert.ToDateTime(sub.CurrentPeriodStart);
                                                vendorConnection.CurrentPeriodEnd = Convert.ToDateTime(sub.CurrentPeriodEnd);
                                                vendorConnection.Status = true;
                                                vendorConnection.IsNew = 1;

                                                loader.Insert(vendorConnection);

                                                #endregion

                                                #region Add Reseller Connection

                                                ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                                resellerConnection.UserId = connectionId;
                                                resellerConnection.ConnectionId = userId;
                                                resellerConnection.SysDate = DateTime.Now;
                                                resellerConnection.LastUpdated = DateTime.Now;
                                                resellerConnection.CanBeViewed = 1;
                                                resellerConnection.CurrentPeriodStart = vendorConnection.CurrentPeriodStart;
                                                resellerConnection.CurrentPeriodEnd = vendorConnection.CurrentPeriodEnd;
                                                resellerConnection.Status = true;
                                                resellerConnection.IsNew = 1;

                                                loader.Insert(resellerConnection);

                                                #endregion

                                                #region Update Vendor Available Connections Counter

                                                vendorPacketFeatures.AvailableConnectionsCount--;
                                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                                DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                                loader1.Update(vendorPacketFeatures);

                                                #endregion

                                                #region Show Success Message of Currrent Period

                                                //item["current_period_start"].Text = vendorConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                                //item["current_period_end"].Text = vendorConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                                //imgStatus.Visible = true;
                                                //imgStatus.ImageUrl = "~/images/icons/small/success.png";
                                                //imgStatus.AlternateText = "elio account success";

                                                #endregion
                                            }
                                            else
                                            {
                                                ShowMessageDA(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "No subscription found for this user");
                                                //GlobalMethods.ShowMessageControl(UcMessageAlert1, "No subscription found for this user", MessageTypes.Error, true, true, false, false, false);
                                                return false;
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Excluded from Stripe Customers

                                            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                            #region Add Vendor Connection

                                            ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                            vendorConnection.UserId = userId;
                                            vendorConnection.ConnectionId = connectionId;
                                            vendorConnection.SysDate = DateTime.Now;
                                            vendorConnection.LastUpdated = DateTime.Now;
                                            vendorConnection.CanBeViewed = 1;
                                            vendorConnection.CurrentPeriodStart = Convert.ToDateTime(vendorPacketFeatures.StartingDate);
                                            vendorConnection.CurrentPeriodEnd = Convert.ToDateTime(vendorPacketFeatures.ExpirationDate);
                                            vendorConnection.Status = true;
                                            vendorConnection.IsNew = 1;

                                            loader.Insert(vendorConnection);

                                            #endregion

                                            #region Add Reseller Connection

                                            ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                            resellerConnection.UserId = connectionId;
                                            resellerConnection.ConnectionId = userId;
                                            resellerConnection.SysDate = DateTime.Now;
                                            resellerConnection.LastUpdated = DateTime.Now;
                                            resellerConnection.CanBeViewed = 1;
                                            resellerConnection.CurrentPeriodStart = vendorConnection.CurrentPeriodStart;
                                            resellerConnection.CurrentPeriodEnd = vendorConnection.CurrentPeriodEnd;
                                            resellerConnection.Status = true;
                                            resellerConnection.IsNew = 1;

                                            loader.Insert(resellerConnection);

                                            #endregion

                                            #region Update Vendor Available Connections Counter

                                            vendorPacketFeatures.AvailableConnectionsCount--;
                                            vendorPacketFeatures.LastUpdate = DateTime.Now;

                                            DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                            loader1.Update(vendorPacketFeatures);

                                            #endregion

                                            #region Show Success Message of Currrent Period

                                            //item["current_period_start"].Text = vendorConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                            //item["current_period_end"].Text = vendorConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                            //imgStatus.Visible = true;
                                            //imgStatus.ImageUrl = "~/images/icons/small/success.png";
                                            //imgStatus.AlternateText = "elio account success";

                                            #endregion

                                            #endregion
                                        }

                                        #endregion

                                        #region Reseller Side

                                        ElioUsers reseller = Sql.GetUserById(connectionId, session);
                                        if (reseller != null)
                                        {
                                            if (reseller.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                                            {
                                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(reseller.Id, session);
                                                if (resellerPacketFeatures != null)
                                                {
                                                    if (resellerPacketFeatures.AvailableConnectionsCount > 0)
                                                    {
                                                        #region Update Reseller Available Connections Counter

                                                        resellerPacketFeatures.AvailableConnectionsCount--;
                                                        resellerPacketFeatures.LastUpdate = DateTime.Now;

                                                        DataLoader<ElioUserPacketStatus> loader2 = new DataLoader<ElioUserPacketStatus>(session);
                                                        loader2.Update(resellerPacketFeatures);

                                                        #endregion
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        ShowMessageDA(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "You have no available connections to add to this user");
                                        //GlobalMethods.ShowMessageControl(UcMessageAlert1, "You have no available connections to add to this user", MessageTypes.Error, true, true, false, false, false);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                ShowMessageDA(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "This connection belongs already to this user");
                                //GlobalMethods.ShowMessageControl(UcMessageAlert1, "This connection belongs already to this user", MessageTypes.Error, true, true, false, false, false);
                                return false;
                            }

                            return true;

                            #endregion
                        }
                        else
                        {
                            #region Free User

                            ShowMessageDA(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "You are not allowed to add connection to Fremium user");
                            //GlobalMethods.ShowMessageControl(UcMessageAlert1, "You are not allowed to add connection to Fremium user", MessageTypes.Error, true, true, false, false, false);
                            return false;

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        #region Delete Specific Connection

                        #region Vendor Delete Connection

                        //ElioUsersConnections vendorConnection = Sql.GetConnection(userId, connectionId, session);

                        //if (vendorConnection != null)
                        //{
                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                            if (vendorPacketFeatures != null)
                            {
                                #region Increase Vendor Available Connections Counter

                                vendorPacketFeatures.AvailableConnectionsCount++;
                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(vendorPacketFeatures);

                                #endregion
                            }
                        }

                        Sql.DeleteConnection(userId, connectionId, session);
                        //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                        //loader1.Delete(vendorConnection);
                        //}

                        #endregion

                        #region Reseller Delete Connection

                        //ElioUsersConnections resellerConnection = Sql.GetConnection(connectionId, userId, session);

                        //if (resellerConnection != null)
                        //{
                        ElioUsers connectionUser = Sql.GetUserById(connectionId, session);

                        if (connectionUser != null)
                        {
                            if (connectionUser.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                            {
                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(connectionId, session);
                                if (resellerPacketFeatures != null)
                                {
                                    #region Increase Reseller Available Connections Counter

                                    resellerPacketFeatures.AvailableConnectionsCount++;
                                    resellerPacketFeatures.LastUpdate = DateTime.Now;

                                    DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                    loader.Update(resellerPacketFeatures);

                                    #endregion
                                }
                            }
                        }

                        Sql.DeleteConnection(connectionId, userId, session);
                        //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                        //loader1.Delete(resellerConnection);
                        //}

                        #endregion

                        #endregion

                        #region Show Message for Delete Connection

                        //item["current_period_start"].Text = "-";
                        //item["current_period_end"].Text = "-";
                        //imgStatus.Visible = true;
                        //imgStatus.ImageUrl = "~/images/icons/small/error.png";
                        //imgStatus.AlternateText = "elioplus account error";

                        #endregion

                        return false;
                    }

                    #endregion
                }
                else if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                {
                    #region reseller

                    if (insertConnection)
                    {
                        #region Add Connection to Reseller

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            #region Only Not Free User

                            bool isAlreadyConnection = Sql.IsConnection(connectionId, user.Id, session);
                            if (!isAlreadyConnection)
                            {
                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);
                                if (resellerPacketFeatures != null)
                                {
                                    if (resellerPacketFeatures.AvailableConnectionsCount > 0)
                                    {
                                        #region Reseller Side

                                        ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                        if (order != null)
                                        {
                                            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                            #region Add Reseller Connection

                                            ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                            resellerConnection.UserId = connectionId;
                                            resellerConnection.ConnectionId = userId;
                                            resellerConnection.SysDate = DateTime.Now;
                                            resellerConnection.LastUpdated = DateTime.Now;
                                            resellerConnection.CanBeViewed = 1;
                                            resellerConnection.CurrentPeriodStart = Convert.ToDateTime(order.CurrentPeriodStart);
                                            resellerConnection.CurrentPeriodEnd = (order.Mode == OrderMode.Active.ToString()) ? Convert.ToDateTime(order.CurrentPeriodEnd) : Convert.ToDateTime(order.CurrentPeriodEnd).AddMonths(1);

                                            resellerConnection.Status = true;
                                            resellerConnection.IsNew = 1;

                                            loader.Insert(resellerConnection);

                                            #endregion

                                            #region Add Vendor Connection

                                            ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                            vendorConnection.UserId = userId;
                                            vendorConnection.ConnectionId = connectionId;
                                            vendorConnection.SysDate = DateTime.Now;
                                            vendorConnection.LastUpdated = DateTime.Now;
                                            vendorConnection.CanBeViewed = 1;
                                            vendorConnection.CurrentPeriodStart = resellerConnection.CurrentPeriodStart;
                                            vendorConnection.CurrentPeriodEnd = resellerConnection.CurrentPeriodEnd;
                                            vendorConnection.Status = true;
                                            vendorConnection.IsNew = 1;

                                            loader.Insert(vendorConnection);

                                            #endregion

                                            #region Update Reseller Available Connections Counter

                                            resellerPacketFeatures.AvailableConnectionsCount--;
                                            resellerPacketFeatures.LastUpdate = DateTime.Now;

                                            DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                            loader1.Update(resellerPacketFeatures);

                                            #endregion

                                            #region Show Success Message of Currrent Period

                                            //item["current_period_start"].Text = resellerConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                            //item["current_period_end"].Text = resellerConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                            //imgStatus.Visible = true;
                                            //imgStatus.ImageUrl = "~/images/icons/small/success.png";

                                            #endregion
                                        }

                                        #endregion

                                        #region Vendor Side

                                        ElioUsers vendor = Sql.GetUserById(userId, session);
                                        if (vendor != null)
                                        {
                                            if (vendor.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                                            {
                                                ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(vendor.Id, session);
                                                if (vendorPacketFeatures != null)
                                                {
                                                    if (vendorPacketFeatures.AvailableConnectionsCount > 0)
                                                    {
                                                        vendorPacketFeatures.AvailableConnectionsCount--;
                                                        vendorPacketFeatures.LastUpdate = DateTime.Now;

                                                        DataLoader<ElioUserPacketStatus> loader2 = new DataLoader<ElioUserPacketStatus>(session);
                                                        loader2.Update(vendorPacketFeatures);
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        ShowMessageDA(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "You have no available connections to add to this user");
                                        //GlobalMethods.ShowMessageControl(UcMessageAlert1, "You have no available connections to add to this user", MessageTypes.Error, true, true, false, false, false);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                ShowMessageDA(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "This connection belongs already to this user");
                                //GlobalMethods.ShowMessageControl(UcMessageAlert1, "This connection belongs already to this user", MessageTypes.Error, true, true, false, false, false);
                                return false;
                            }

                            return true;

                            #endregion
                        }
                        else
                        {
                            #region Free User

                            ShowMessageDA(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "You are not allowed to add connection to Fremium user");
                            //GlobalMethods.ShowMessageControl(UcMessageAlert1, "You are not allowed to add connection to Fremium user", MessageTypes.Error, true, true, false, false, false);
                            return false;

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        #region Delete Specific Connection

                        #region Reseller Delete Connection

                        //ElioUsersConnections resellerConnection = Sql.GetConnection(connectionId, userId, session);

                        //if (resellerConnection != null)
                        //{
                        ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(connectionId, session);
                        if (resellerPacketFeatures != null)
                        {
                            #region Increase Reseller Available Connections Counter

                            if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                            {
                                //    ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                //    if (order != null)
                                //    {
                                //if (connection.CurrentPeriodStart >= order.CurrentPeriodStart && connection.CurrentPeriodEnd <= order.CurrentPeriodEnd)
                                //{
                                resellerPacketFeatures.AvailableConnectionsCount++;
                                resellerPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(resellerPacketFeatures);
                                //}
                                //}
                            }

                            #endregion
                        }

                        Sql.DeleteConnection(connectionId, userId, session);

                        //item["current_period_start"].Text = "-";
                        //item["current_period_end"].Text = "-";
                        //imgStatus.Visible = true;
                        //imgStatus.ImageUrl = "~/images/icons/small/error.png";

                        #endregion

                        #region Vendor Delete Connection

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                            if (vendorPacketFeatures != null)
                            {
                                #region Increase Vendor Available Connections Counter

                                vendorPacketFeatures.AvailableConnectionsCount++;
                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(vendorPacketFeatures);

                                #endregion
                            }
                        }

                        Sql.DeleteConnection(userId, connectionId, session);

                        #endregion

                        #endregion

                        #region Show Message for Delete Connection

                        //item["current_period_start"].Text = "-";
                        //item["current_period_end"].Text = "-";
                        //imgStatus.Visible = true;
                        //imgStatus.ImageUrl = "~/images/icons/small/error.png";
                        //imgStatus.AlternateText = "elioplus account error";

                        #endregion

                        return false;
                    }

                    #endregion
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static bool AddUserConnectionsInstantly(int userId, int connectionId, bool insertConnection, MessageControl UcMessageAlert1, MessageControl UcMessageAlert2, DBSession session)
        {
            ElioUsers user = Sql.GetUserById(userId, session);

            if (user != null)
            {
                if (user.CompanyType == Types.Vendors.ToString())
                {
                    #region Vendor

                    if (insertConnection)
                    {
                        #region Add Connection to Vendor

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            #region Only Not Free User

                            bool isAlreadyConnection = Sql.IsConnection(user.Id, connectionId, session);
                            if (!isAlreadyConnection)
                            {
                                ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                                if (vendorPacketFeatures != null)
                                {
                                    if (vendorPacketFeatures.ExpirationDate <= DateTime.Now)
                                    {
                                        #region Packet Status Features need update

                                        ElioUsersSubscriptions userSubscription = Sql.GetUserSubscription(user.Id, user.CustomerStripeId, session);
                                        if (userSubscription != null)
                                        {
                                            int packId = Sql.GetPacketIdBySubscriptionID(userSubscription.SubscriptionId, session);
                                            if (packId > 0)
                                            {
                                                if (packId == (int)Packets.PremiumDiscountNoTrial || packId == (int)Packets.PremiumDiscountTrial || packId == (int)Packets.PremiumtNoTrial25 || packId == (int)Packets.PremiumDiscount20 || packId == (int)Packets.Premium_No_Trial)
                                                    packId = (int)Packets.Premium;

                                                ElioPackets packet = Sql.GetPacketById(packId, session);
                                                if (packet != null && packet.Id != (int)Packets.PremiumService)
                                                {
                                                    List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(packet.Id, session);
                                                    if (items.Count > 0)
                                                    {
                                                        #region Get Packet Features Items

                                                        int totalLeads = 0;
                                                        int totalMessages = 0;
                                                        int totalConnections = 0;
                                                        int totalManagePartners = 0;
                                                        int totalLibraryStorage = 0;

                                                        for (int i = 0; i < items.Count; i++)
                                                        {
                                                            if (items[i].ItemDescription == "Leads")
                                                            {
                                                                totalLeads = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "Messages")
                                                            {
                                                                totalMessages = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "Connections")
                                                            {
                                                                totalConnections = items[i].FreeItemsNo;        // (order.Mode == OrderMode.Trialing.ToString()) ? items[i].FreeItemsTrialNo : items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "ManagePartners")
                                                            {
                                                                totalManagePartners = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "LibraryStorage")
                                                            {
                                                                totalLibraryStorage = items[i].FreeItemsNo;
                                                            }
                                                        }

                                                        #endregion

                                                        #region Get User Already Supplied Leads/Messages/Connections for Current Period

                                                        int totalUserLeads = Sql.GetUserLeadsCountByMonthRange(user, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);                            //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserMessages = Sql.GetUserSendMessagesCountByMonthRange(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);               //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserConnections = Sql.GetUserViewableConnectionsForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);      //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserInvitations = Sql.GetUserInvitationsForCurrentPeriod(user.Id, CollaborateInvitationStatus.Confirmed.ToString(), userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);
                                                        int totalUserFilesSize = Sql.GetUserLibraryFilesStorageForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);

                                                        double totalUserFileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(totalUserFilesSize), "GB");

                                                        #endregion

                                                        #region Insert / Update Packet Status Features

                                                        ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);

                                                        DataLoader<ElioUserPacketStatus> loader4 = new DataLoader<ElioUserPacketStatus>(session);

                                                        if (packetFeatures == null)
                                                        {
                                                            packetFeatures = new ElioUserPacketStatus();

                                                            packetFeatures.UserId = user.Id;
                                                            packetFeatures.PackId = items[0].Id;
                                                            packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);
                                                            packetFeatures.AvailableLeadsCount = totalLeads;
                                                            packetFeatures.AvailableMessagesCount = totalMessages;
                                                            packetFeatures.AvailableConnectionsCount = totalConnections;
                                                            packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                                                            packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                                                            packetFeatures.Sysdate = DateTime.Now;
                                                            packetFeatures.LastUpdate = DateTime.Now;
                                                            packetFeatures.StartingDate = Convert.ToDateTime(userSubscription.CurrentPeriodStart);       //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                                                            packetFeatures.ExpirationDate = Convert.ToDateTime(userSubscription.CurrentPeriodEnd);    //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                                                            loader4.Insert(packetFeatures);
                                                        }
                                                        else
                                                        {
                                                            packetFeatures.PackId = items[0].Id;
                                                            packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);
                                                            packetFeatures.AvailableLeadsCount = totalLeads;
                                                            packetFeatures.AvailableMessagesCount = totalMessages;
                                                            packetFeatures.AvailableConnectionsCount = totalConnections;
                                                            packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                                                            packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                                                            packetFeatures.LastUpdate = DateTime.Now;
                                                            packetFeatures.StartingDate = Convert.ToDateTime(userSubscription.CurrentPeriodStart);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                                                            packetFeatures.ExpirationDate = Convert.ToDateTime(userSubscription.CurrentPeriodEnd);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                                                            loader4.Update(packetFeatures);
                                                        }

                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }

                                    vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);

                                    if (vendorPacketFeatures.AvailableConnectionsCount > 0)
                                    {
                                        #region Vendor Side

                                        bool hasNoSubscription = false;
                                        string ids = (ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"] != null && ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"] != "") ? ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"].ToString() : "";

                                        if (ids != "")
                                        {
                                            string[] customers = ids.Split(',').ToArray();
                                            foreach (string userID in customers)
                                            {
                                                if (Convert.ToInt32(userID) == userId)
                                                {
                                                    hasNoSubscription = true;
                                                    break;
                                                }
                                            }
                                        }

                                        if (!hasNoSubscription)
                                        {
                                            #region Normal Stripe Case

                                            #region Get User Subscription

                                            ElioUsersSubscriptions sub = Sql.GetUserSubscription(user.Id, user.CustomerStripeId, session);
                                            //ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);

                                            //if (order == null)
                                            //{
                                            //    order = Sql.HasUserOrderByServicePacketStatusUse(user.Id, Convert.ToInt32(Packets.PremiumService), Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                            //}

                                            #endregion

                                            if (sub != null)
                                            {
                                                if (sub.Status.ToLower() != "active")   //custom for now
                                                    Logger.Debug("GlobalDBMethods.cs --> AddUserConnectionsInstantly()", string.Format("GlobalDBMethods.cs --> MESSAGE: Admin added connection ID:{0}, to user {1} at {2}, but his subscription status is {3}", connectionId, user.Id, DateTime.Now.ToString(), sub.SubscriptionId), "Connection added successfully but subscription need to be updated");

                                                DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                                #region Add Vendor Connection

                                                ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                                vendorConnection.UserId = userId;
                                                vendorConnection.ConnectionId = connectionId;
                                                vendorConnection.SysDate = DateTime.Now;
                                                vendorConnection.LastUpdated = DateTime.Now;
                                                vendorConnection.CanBeViewed = 1;
                                                vendorConnection.CurrentPeriodStart = Convert.ToDateTime(sub.CurrentPeriodStart);
                                                vendorConnection.CurrentPeriodEnd = Convert.ToDateTime(sub.CurrentPeriodEnd);
                                                vendorConnection.Status = true;
                                                vendorConnection.IsNew = 1;

                                                loader.Insert(vendorConnection);

                                                #endregion

                                                #region Add Reseller Connection

                                                ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                                resellerConnection.UserId = connectionId;
                                                resellerConnection.ConnectionId = userId;
                                                resellerConnection.SysDate = DateTime.Now;
                                                resellerConnection.LastUpdated = DateTime.Now;
                                                resellerConnection.CanBeViewed = 1;
                                                resellerConnection.CurrentPeriodStart = vendorConnection.CurrentPeriodStart;
                                                resellerConnection.CurrentPeriodEnd = vendorConnection.CurrentPeriodEnd;
                                                resellerConnection.Status = true;
                                                resellerConnection.IsNew = 1;

                                                loader.Insert(resellerConnection);

                                                #endregion

                                                #region Update Vendor Available Connections Counter

                                                vendorPacketFeatures.AvailableConnectionsCount--;
                                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                                DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                                loader1.Update(vendorPacketFeatures);

                                                #endregion

                                                #region Show Success Message of Currrent Period

                                                //item["current_period_start"].Text = vendorConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                                //item["current_period_end"].Text = vendorConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                                //imgStatus.Visible = true;
                                                //imgStatus.ImageUrl = "~/images/icons/small/success.png";
                                                //imgStatus.AlternateText = "elio account success";

                                                #endregion
                                            }
                                            else
                                            {
                                                ShowMessage(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "No subscription found for this user");
                                                //GlobalMethods.ShowMessageControl(UcMessageAlert1, "No subscription found for this user", MessageTypes.Error, true, true, false, false, false);
                                                return false;
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Excluded from Stripe Customers

                                            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                            #region Add Vendor Connection

                                            ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                            vendorConnection.UserId = userId;
                                            vendorConnection.ConnectionId = connectionId;
                                            vendorConnection.SysDate = DateTime.Now;
                                            vendorConnection.LastUpdated = DateTime.Now;
                                            vendorConnection.CanBeViewed = 1;
                                            vendorConnection.CurrentPeriodStart = Convert.ToDateTime(vendorPacketFeatures.StartingDate);
                                            vendorConnection.CurrentPeriodEnd = Convert.ToDateTime(vendorPacketFeatures.ExpirationDate);
                                            vendorConnection.Status = true;
                                            vendorConnection.IsNew = 1;

                                            loader.Insert(vendorConnection);

                                            #endregion

                                            #region Add Reseller Connection

                                            ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                            resellerConnection.UserId = connectionId;
                                            resellerConnection.ConnectionId = userId;
                                            resellerConnection.SysDate = DateTime.Now;
                                            resellerConnection.LastUpdated = DateTime.Now;
                                            resellerConnection.CanBeViewed = 1;
                                            resellerConnection.CurrentPeriodStart = vendorConnection.CurrentPeriodStart;
                                            resellerConnection.CurrentPeriodEnd = vendorConnection.CurrentPeriodEnd;
                                            resellerConnection.Status = true;
                                            resellerConnection.IsNew = 1;

                                            loader.Insert(resellerConnection);

                                            #endregion

                                            #region Update Vendor Available Connections Counter

                                            vendorPacketFeatures.AvailableConnectionsCount--;
                                            vendorPacketFeatures.LastUpdate = DateTime.Now;

                                            DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                            loader1.Update(vendorPacketFeatures);

                                            #endregion

                                            #region Show Success Message of Currrent Period

                                            //item["current_period_start"].Text = vendorConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                            //item["current_period_end"].Text = vendorConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                            //imgStatus.Visible = true;
                                            //imgStatus.ImageUrl = "~/images/icons/small/success.png";
                                            //imgStatus.AlternateText = "elio account success";

                                            #endregion

                                            #endregion
                                        }

                                        #endregion

                                        #region Reseller Side

                                        ElioUsers reseller = Sql.GetUserById(connectionId, session);
                                        if (reseller != null)
                                        {
                                            if (reseller.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                                            {
                                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(reseller.Id, session);
                                                if (resellerPacketFeatures != null)
                                                {
                                                    if (resellerPacketFeatures.AvailableConnectionsCount > 0)
                                                    {
                                                        #region Update Reseller Available Connections Counter

                                                        resellerPacketFeatures.AvailableConnectionsCount--;
                                                        resellerPacketFeatures.LastUpdate = DateTime.Now;

                                                        DataLoader<ElioUserPacketStatus> loader2 = new DataLoader<ElioUserPacketStatus>(session);
                                                        loader2.Update(resellerPacketFeatures);

                                                        #endregion
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        ShowMessage(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "You have no available connections to add to this user");
                                        //GlobalMethods.ShowMessageControl(UcMessageAlert1, "You have no available connections to add to this user", MessageTypes.Error, true, true, false, false, false);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                ShowMessage(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "This connection belongs already to this user");
                                //GlobalMethods.ShowMessageControl(UcMessageAlert1, "This connection belongs already to this user", MessageTypes.Error, true, true, false, false, false);
                                return false;
                            }

                            return true;

                            #endregion
                        }
                        else
                        {
                            #region Free User

                            ShowMessage(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "You are not allowed to add connection to Fremium user");
                            //GlobalMethods.ShowMessageControl(UcMessageAlert1, "You are not allowed to add connection to Fremium user", MessageTypes.Error, true, true, false, false, false);
                            return false;

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        #region Delete Specific Connection

                        #region Vendor Delete Connection

                        //ElioUsersConnections vendorConnection = Sql.GetConnection(userId, connectionId, session);

                        //if (vendorConnection != null)
                        //{
                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                            if (vendorPacketFeatures != null)
                            {
                                #region Increase Vendor Available Connections Counter

                                vendorPacketFeatures.AvailableConnectionsCount++;
                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(vendorPacketFeatures);

                                #endregion
                            }
                        }

                        Sql.DeleteConnection(userId, connectionId, session);
                        //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                        //loader1.Delete(vendorConnection);
                        //}

                        #endregion

                        #region Reseller Delete Connection

                        //ElioUsersConnections resellerConnection = Sql.GetConnection(connectionId, userId, session);

                        //if (resellerConnection != null)
                        //{
                        ElioUsers connectionUser = Sql.GetUserById(connectionId, session);

                        if (connectionUser != null)
                        {
                            if (connectionUser.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                            {
                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(connectionId, session);
                                if (resellerPacketFeatures != null)
                                {
                                    #region Increase Reseller Available Connections Counter

                                    resellerPacketFeatures.AvailableConnectionsCount++;
                                    resellerPacketFeatures.LastUpdate = DateTime.Now;

                                    DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                    loader.Update(resellerPacketFeatures);

                                    #endregion
                                }
                            }
                        }

                        Sql.DeleteConnection(connectionId, userId, session);
                        //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                        //loader1.Delete(resellerConnection);
                        //}

                        #endregion

                        #endregion

                        #region Show Message for Delete Connection

                        //item["current_period_start"].Text = "-";
                        //item["current_period_end"].Text = "-";
                        //imgStatus.Visible = true;
                        //imgStatus.ImageUrl = "~/images/icons/small/error.png";
                        //imgStatus.AlternateText = "elioplus account error";

                        #endregion

                        return false;
                    }

                    #endregion
                }
                else if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                {
                    #region reseller

                    if (insertConnection)
                    {
                        #region Add Connection to Reseller

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            #region Only Not Free User

                            bool isAlreadyConnection = Sql.IsConnection(connectionId, user.Id, session);
                            if (!isAlreadyConnection)
                            {
                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);
                                if (resellerPacketFeatures != null)
                                {
                                    if (resellerPacketFeatures.AvailableConnectionsCount > 0)
                                    {
                                        #region Reseller Side

                                        ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                        if (order != null)
                                        {
                                            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                            #region Add Reseller Connection

                                            ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                            resellerConnection.UserId = connectionId;
                                            resellerConnection.ConnectionId = userId;
                                            resellerConnection.SysDate = DateTime.Now;
                                            resellerConnection.LastUpdated = DateTime.Now;
                                            resellerConnection.CanBeViewed = 1;
                                            resellerConnection.CurrentPeriodStart = Convert.ToDateTime(order.CurrentPeriodStart);
                                            resellerConnection.CurrentPeriodEnd = (order.Mode == OrderMode.Active.ToString()) ? Convert.ToDateTime(order.CurrentPeriodEnd) : Convert.ToDateTime(order.CurrentPeriodEnd).AddMonths(1);

                                            resellerConnection.Status = true;
                                            resellerConnection.IsNew = 1;

                                            loader.Insert(resellerConnection);

                                            #endregion

                                            #region Add Vendor Connection

                                            ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                            vendorConnection.UserId = userId;
                                            vendorConnection.ConnectionId = connectionId;
                                            vendorConnection.SysDate = DateTime.Now;
                                            vendorConnection.LastUpdated = DateTime.Now;
                                            vendorConnection.CanBeViewed = 1;
                                            vendorConnection.CurrentPeriodStart = resellerConnection.CurrentPeriodStart;
                                            vendorConnection.CurrentPeriodEnd = resellerConnection.CurrentPeriodEnd;
                                            vendorConnection.Status = true;
                                            vendorConnection.IsNew = 1;

                                            loader.Insert(vendorConnection);

                                            #endregion

                                            #region Update Reseller Available Connections Counter

                                            resellerPacketFeatures.AvailableConnectionsCount--;
                                            resellerPacketFeatures.LastUpdate = DateTime.Now;

                                            DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                            loader1.Update(resellerPacketFeatures);

                                            #endregion

                                            #region Show Success Message of Currrent Period

                                            //item["current_period_start"].Text = resellerConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                            //item["current_period_end"].Text = resellerConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                            //imgStatus.Visible = true;
                                            //imgStatus.ImageUrl = "~/images/icons/small/success.png";

                                            #endregion
                                        }

                                        #endregion

                                        #region Vendor Side

                                        ElioUsers vendor = Sql.GetUserById(userId, session);
                                        if (vendor != null)
                                        {
                                            if (vendor.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                                            {
                                                ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(vendor.Id, session);
                                                if (vendorPacketFeatures != null)
                                                {
                                                    if (vendorPacketFeatures.AvailableConnectionsCount > 0)
                                                    {
                                                        vendorPacketFeatures.AvailableConnectionsCount--;
                                                        vendorPacketFeatures.LastUpdate = DateTime.Now;

                                                        DataLoader<ElioUserPacketStatus> loader2 = new DataLoader<ElioUserPacketStatus>(session);
                                                        loader2.Update(vendorPacketFeatures);
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        ShowMessage(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "You have no available connections to add to this user");
                                        //GlobalMethods.ShowMessageControl(UcMessageAlert1, "You have no available connections to add to this user", MessageTypes.Error, true, true, false, false, false);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                ShowMessage(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "This connection belongs already to this user");
                                //GlobalMethods.ShowMessageControl(UcMessageAlert1, "This connection belongs already to this user", MessageTypes.Error, true, true, false, false, false);
                                return false;
                            }

                            return true;

                            #endregion
                        }
                        else
                        {
                            #region Free User

                            ShowMessage(UcMessageAlert1, UcMessageAlert2, MessageTypes.Error, "You are not allowed to add connection to Fremium user");
                            //GlobalMethods.ShowMessageControl(UcMessageAlert1, "You are not allowed to add connection to Fremium user", MessageTypes.Error, true, true, false, false, false);
                            return false;

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        #region Delete Specific Connection

                        #region Reseller Delete Connection

                        //ElioUsersConnections resellerConnection = Sql.GetConnection(connectionId, userId, session);

                        //if (resellerConnection != null)
                        //{
                        ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(connectionId, session);
                        if (resellerPacketFeatures != null)
                        {
                            #region Increase Reseller Available Connections Counter

                            if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                            {
                                //    ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                //    if (order != null)
                                //    {
                                //if (connection.CurrentPeriodStart >= order.CurrentPeriodStart && connection.CurrentPeriodEnd <= order.CurrentPeriodEnd)
                                //{
                                resellerPacketFeatures.AvailableConnectionsCount++;
                                resellerPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(resellerPacketFeatures);
                                //}
                                //}
                            }

                            #endregion
                        }

                        Sql.DeleteConnection(connectionId, userId, session);

                        //item["current_period_start"].Text = "-";
                        //item["current_period_end"].Text = "-";
                        //imgStatus.Visible = true;
                        //imgStatus.ImageUrl = "~/images/icons/small/error.png";

                        #endregion

                        #region Vendor Delete Connection

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                            if (vendorPacketFeatures != null)
                            {
                                #region Increase Vendor Available Connections Counter

                                vendorPacketFeatures.AvailableConnectionsCount++;
                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(vendorPacketFeatures);

                                #endregion
                            }
                        }

                        Sql.DeleteConnection(userId, connectionId, session);

                        #endregion

                        #endregion

                        #region Show Message for Delete Connection

                        //item["current_period_start"].Text = "-";
                        //item["current_period_end"].Text = "-";
                        //imgStatus.Visible = true;
                        //imgStatus.ImageUrl = "~/images/icons/small/error.png";
                        //imgStatus.AlternateText = "elioplus account error";

                        #endregion

                        return false;
                    }

                    #endregion
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static bool SetSnitcherScriptNew2(string country, string category, DBSession session)
        {
            DataLoader<ElioSnitcherUserCountryProducts> loader = new DataLoader<ElioSnitcherUserCountryProducts>(session);

            string strQuery = @"SELECT ucp.country,ucp.products
                                FROM Elio_snitcher_user_country_products ucp 
                                WHERE ucp.user_id IN
                                (" + ConfigurationManager.AppSettings["SnitcherCountryProductsUsers"].ToString() + ") ";

            if (country == "" && category != "")
            {
                strQuery += @" AND country = '' ";

                DataTable table = session.GetDataTable(strQuery);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string[] prdctsWords = row["products"].ToString().Split(',');

                        if (prdctsWords.Length > 0)
                        {
                            foreach (string prd in prdctsWords)
                            {
                                if (prd.ToLower() == category.ToLower())
                                    return true;
                            }
                        }
                    }
                }
            }
            else if (country != "" && category != "")
            {
                strQuery += @" AND country != ''";

                DataTable table = session.GetDataTable(strQuery);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string[] countrWords = row["country"].ToString().Split(',');

                        if (countrWords.Length > 0)
                        {
                            foreach (string countr in countrWords)
                            {
                                if (countr == country)
                                {
                                    string[] prdctsWords = row["products"].ToString().Split(',');
                                    if (prdctsWords.Length > 0)
                                    {
                                        foreach (string prd in prdctsWords)
                                        {
                                            if (prd.ToLower() == category.ToLower())
                                                return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool SetSnitcherScriptNew(string country, string category, DBSession session)
        {
            DataLoader<ElioSnitcherUserCountryProducts> loader = new DataLoader<ElioSnitcherUserCountryProducts>(session);

            string strQuery = @"SELECT ucp.country,ucp.products
                                FROM Elio_snitcher_user_country_products ucp 
                                WHERE ucp.user_id IN
                                (" + ConfigurationManager.AppSettings["SnitcherCountryProductsUsers"].ToString() + ") ";

            if (country == "" && category != "")
            {
                strQuery += @" AND country = '' ";

                List<ElioSnitcherUserCountryProducts> products = loader.Load(strQuery);

                foreach (ElioSnitcherUserCountryProducts cPrd in products)
                {
                    string[] prdctsWords = cPrd.Products.Split(',');

                    if (prdctsWords.Length > 0)
                    {
                        foreach (string prd in prdctsWords)
                        {
                            if (prd.ToLower() == category.ToLower())
                                return true;
                        }
                    }
                }
            }
            else if (country != "" && category != "")
            {
                strQuery += @" AND country != ''";

                List<ElioSnitcherUserCountryProducts> products = loader.Load(strQuery);

                foreach (ElioSnitcherUserCountryProducts cPrd in products)
                {
                    string[] countrWords = cPrd.Country.Split(',');

                    if (countrWords.Length > 0)
                    {
                        foreach (string countr in countrWords)
                        {
                            if (countr == country)
                            {
                                string[] prdctsWords = cPrd.Products.Split(',');
                                if (prdctsWords.Length > 0)
                                {
                                    foreach (string prd in prdctsWords)
                                    {
                                        if (prd.ToLower() == category.ToLower())
                                            return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool SetSnitcherScript(string country, string category, DBSession session)
        {
            string strQuery = @"SELECT ucp.country,ucp.products
                                FROM Elio_snitcher_user_country_products ucp 
                                WHERE ucp.user_id IN
                                (" + ConfigurationManager.AppSettings["SnitcherCountryProductsUsers"].ToString() + ") ";

            if (country == "")
            {
                strQuery += @" AND country = '' ";
            }
            else
            {
                #region Old Way to delete

                //strQuery += @" AND (";

                strQuery += FixFasterQueryStringBy(country, "country");

                //string[] segsWords = country.Split(' ');
                //if (segsWords.Length > 1)
                //{
                //    foreach (var word in segsWords)
                //    {
                //        if (word != "")
                //        {
                //            strQuery += "country LIKE '%" + word + "%' ";
                //            strQuery += " OR ";
                //        }
                //    }

                //    if (strQuery.EndsWith(" OR "))
                //        strQuery = strQuery.Substring(0, strQuery.Length - 5);
                //}
                //else
                //    strQuery += @" AND country LIKE '%" + country + "%' ";

                //strQuery += ") ";

                #endregion
            }

            if (category != "")
            {
                #region Old Way to delete

                //strQuery += @" AND (";

                strQuery += FixFasterQueryStringBy(category, "products");

                //string[] segsWords = category.Split(' ');
                //if (segsWords.Length > 1)
                //{
                //    foreach (var word in segsWords)
                //    {
                //        if (word != "")
                //        {
                //            strQuery += "products LIKE '%" + word + "%' ";
                //            strQuery += " OR ";
                //        }
                //    }

                //    if (strQuery.EndsWith(" OR "))
                //        strQuery = strQuery.Substring(0, strQuery.Length - 5);
                //}
                //else
                //    strQuery += @" AND products LIKE '%" + category + "%' ";

                //strQuery += ") ";

                //strQuery += @" AND products LIKE '%" + category + "%' ";

                #endregion

                DataLoader<ElioSnitcherUserCountryProducts> loader = new DataLoader<ElioSnitcherUserCountryProducts>(session);

                List<ElioSnitcherUserCountryProducts> products = loader.Load(strQuery);

                foreach (ElioSnitcherUserCountryProducts cPrd in products)
                {
                    if (country == "")
                    {
                        string[] prdctsWords = cPrd.Products.Split(',');
                        if (prdctsWords.Length > 0)
                        {
                            foreach (string prd in prdctsWords)
                            {
                                if (prd.ToLower() == category.ToLower())
                                    return true;
                            }
                        }
                    }
                    else
                    {
                        string[] countrWords = cPrd.Country.Split(',');
                        if (countrWords.Length > 0)
                        {
                            foreach (string countr in countrWords)
                            {
                                if (countr == country)
                                {
                                    string[] prdctsWords = cPrd.Products.Split(',');
                                    if (prdctsWords.Length > 0)
                                    {
                                        foreach (string prd in prdctsWords)
                                        {
                                            if (prd.ToLower() == category.ToLower())
                                                return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //Dictionary<List<string>, List<string>> countryProds = new Dictionary<List<string>, List<string>>();
            }

            return false;
        }

        private static string FixFasterQueryStringBy(string searcWord, string replaceByWord)
        {
            //string strQuery = "";
            //string[] segsWords = searcWord.Split(' ');
            //if (segsWords.Length > 0)
            //{
            string strQuery = @" AND {replaceWord} LIKE '%" + searcWord.Replace("'", "").Replace("'d", "") + "%' ";

            //foreach (var word in segsWords)
            //{
            //    if (word != "")
            //    {
            //        strQuery += "{replaceWord} LIKE '%" + word + "%' ";
            //        strQuery += " OR ";
            //    }
            //}

            //if (strQuery.EndsWith(" OR "))
            //    strQuery = strQuery.Substring(0, strQuery.Length - 4);
            //}
            //else
            //    strQuery += @" AND {replaceWord} LIKE '%" + searcWord + "%' ";

            return strQuery.Replace("{replaceWord}", replaceByWord);
        }

        public static string GetOverviewForCompanyByCountry(string countryName, string companyName, DBSession session)
        {
            bool existInCountriesVIEW_EN = Sql.ExistCountryInCountriesVIEW_EN(countryName, session);

            if (existInCountriesVIEW_EN)
            {
                return "View the solutions, services and product portfolio of " + companyName;
            }
            else
            {
                bool existInCountriesVIEW_ES = Sql.ExistCountryInCountriesVIEW_ES(countryName, session);

                if (existInCountriesVIEW_ES)
                {
                    return "Vea las soluciones, los servicios y la cartera de productos de " + companyName;
                }
                else
                {
                    bool existInCountriesVIEW_FR = Sql.ExistCountryInCountriesVIEW_FR(countryName, session);

                    if (existInCountriesVIEW_FR)
                    {
                        return "Découvrez les solutions, les services et le portefeuille de produits de " + companyName;
                    }
                    else
                    {
                        bool existInCountriesVIEW_PT = Sql.ExistCountryInCountriesVIEW_PT(countryName, session);

                        if (existInCountriesVIEW_PT)
                        {
                            return "Veja as soluções, serviços e portfólio de produtos da " + companyName;
                        }
                        else
                        {
                            bool existInCountriesVIEW_DE = Sql.ExistCountryInCountriesVIEW_DE(countryName, session);

                            if (existInCountriesVIEW_DE)
                            {
                                return "Sehen Sie sich die Lösungen, Services und das Produktportfolio von " + companyName;
                            }
                            else
                            {
                                bool existInCountriesVIEW_IT = Sql.ExistCountryInCountriesVIEW_IT(countryName, session);

                                if (existInCountriesVIEW_IT)
                                {
                                    return "Visualizza le soluzioni, i servizi e il portafoglio prodotti di " + companyName;
                                }
                                else
                                {
                                    switch (countryName)
                                    {
                                        case "Sweden":
                                            return "Se " + companyName + " lösningar, tjänster och produktportfölj.";

                                        case "Turkey":
                                            return companyName + " un çözümlerini, hizmetlerini ve ürün portföyünü görüntüleyin.";

                                        case "Denmark":
                                            return "Se " + companyName + " løsninger, tjenester og produktportefølje.";

                                        case "Czech Republic":
                                            return "Prohlédněte si řešení, služby a portfolio produktů společnosti " + companyName;

                                        case "Croatia":
                                            return "Pogledajte rješenja, usluge i portfelj proizvoda tvrtke " + companyName;

                                        case "Greece":
                                            return "Δες τις υπηρεσίες, προϊόντα και συνεργάτες της εταιρείας " + companyName;

                                        case "Hungary":
                                            return "Tekintse meg a " + companyName + " megoldásait, szolgáltatásait és termékportfólióját.";

                                        case "Netherlands":
                                            return "Bekijk de oplossingen, diensten en het productportfolio van " + companyName;

                                        case "Poland":
                                            return "Zobacz rozwiązania, usługi i portfolio produktów firmy " + companyName;

                                        case "Finland":
                                            return "Tutustu " + companyName + " ratkaisuihin, palveluihin ja tuotevalikoimaan.";

                                        default:
                                            return "View the solutions, services and product portfolio of " + companyName;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static bool IsCorrectPage(ref string validUrl, DBSession session)
        {
            if (session.Connection.State == ConnectionState.Closed)
                session.OpenConnection();

            validUrl = ControlLoader.SearchForChannelPartners;
            string[] paths = HttpContext.Current.Request.Url.AbsolutePath.TrimEnd('/').Split('/').ToArray();

            Uri path = HttpContext.Current.Request.Url;
            var pathSegs = path.Segments;
            string redPath = "/";
            string segs = pathSegs[1].TrimEnd('/').TrimEnd('-');

            foreach (string item in paths)
            {
                if (!string.IsNullOrEmpty(item) && !item.Contains("profile") && !item.Contains("channel-partners"))
                {
                    if (item.Contains("OR") || item.Contains("XOR") || item.Contains("SYSDATE") || item.Contains("SLEEP") || item.Contains("|") || item.Contains("'") || item.Contains("PIPE") || item.Contains("CHR") || item.Contains("SELECT") || item.Contains("OR") || item.Contains("(") || item.Contains(")") || item.Contains("--") || item.Contains("SLEEP") || item.Contains("FROM"))
                    {
                        return false;
                    }
                }
                else if (item.Contains("'%7C%7C") || item.Contains("%")
                    || item.Contains("'") || item.Contains("OR")
                    || item.Contains("XOR") || item.Contains("SYSDATE")
                    || item.Contains("SLEEP") || item.Contains("|") || item.Contains("'")
                    || item.Contains("PIPE") || item.Contains("CHR") || item.Contains("SELECT")
                    || item.Contains("OR") || item.Contains("(") || item.Contains(")")
                    || item.Contains("--") || item.Contains("SLEEP") || item.Contains("FROM"))
                {
                    return false;
                }
            }
            
            if (segs.Contains("asia-pasific"))
            {
                validUrl = HttpContext.Current.Request.Url.ToString().Replace("asia-pasific", "asia-pacific");
                return false;
            }
            else if (pathSegs.Length == 4 && !pathSegs[1].Contains("profile/"))
            {
                string[] regions = { "africa", "asia-pacific", "europe", "middle-east", "north-america", "south-america" };

                if (!regions.Contains(segs))
                {
                    string region = Sql.GetRegionByCountryTbl(segs.Replace("-", " ").Replace("and", "&"), session);
                    if (region == "")
                    {
                        string regionCountryUrl = Sql.GetRegionCountryByCityTbl(segs.Replace("-", " ").Replace("and", "&"), session);
                        if (regionCountryUrl != "")
                        {
                            redPath += regionCountryUrl + "/" + pathSegs[1].Replace("-/", "/") + pathSegs[2].Replace("-/", "/") + pathSegs[3].Replace("-/", "/");

                            validUrl = redPath;
                            return false;
                        }
                        else
                        {
                            validUrl = ControlLoader.SearchForChannelPartners;
                            return false;
                        }
                    }
                    else
                    {
                        redPath += region.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + pathSegs[1].Replace("-/", "/") + pathSegs[2].Replace("-/", "/") + pathSegs[3].Replace("-/", "/");

                        validUrl = redPath;
                        return false;
                    }
                }
            }
            else if (pathSegs.Length == 5 && !pathSegs[1].Replace("-/", "/").Contains("profile/") && pathSegs[1].TrimEnd('/').Length >= 2)
            {
                if (paths[2].Contains("|") || paths[2].Contains("'") || paths[2].Contains("PIPE") || paths[2].Contains("CHR") || paths[2].Contains("SELECT") || paths[2].Contains("OR") || paths[2].Contains("(") || paths[2].Contains(")") || paths[2].Contains("--") || paths[2].Contains("SLEEP") || paths[2].Contains("FROM"))
                {
                    return false;
                }

                if (segs.Length == 2)
                {
                    string countryCity = pathSegs[2].TrimEnd('/').TrimEnd('-');
                    string region = Sql.GetRegionByCountryTbl(countryCity.Replace("-", " ").Replace("and", "&"), session);
                    if (region == "")
                    {
                        string regionCountryUrl = Sql.GetRegionTransCountryByCityTbl(countryCity.Replace("-", " ").Replace("and", "&"), segs.ToLower(), session);
                        if (regionCountryUrl != "")
                        {
                            redPath += regionCountryUrl + "/" + pathSegs[2].Replace("-/", "/") + pathSegs[3].Replace("-/", "/") + pathSegs[4].Replace("-/", "/");

                            validUrl = redPath;
                            return false;
                        }
                        else
                        {
                            validUrl = ControlLoader.SearchForChannelPartners;
                            return false;
                        }
                    }
                    else
                    {
                        redPath += region.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + pathSegs[1].Replace("-/", "/") + pathSegs[2].Replace("-/", "/") + pathSegs[3].Replace("-/", "/") + pathSegs[4].Replace("-/", "/");

                        validUrl = redPath;
                        return false;
                    }
                }
            }
            else if (paths.Length == 6 && !pathSegs[1].Contains("profile/") && paths[4] == "channel-partners")
            {
                if (paths[2].Length == 2 && (paths[3].Contains("|") || paths[3].Contains("'") || paths[3].Contains("PIPE") || paths[3].Contains("CHR") || paths[3].Contains("SELECT") || paths[3].Contains("OR") || paths[3].Contains("(") || paths[3].Contains(")") || paths[3].Contains("--") || paths[3].Contains("SLEEP") || paths[3].Contains("FROM")))
                {
                    return false;
                }

                string[] regions = { "africa", "asia-pacific", "europe", "middle-east", "north-america", "south-america" };

                if (regions.Contains(segs))
                {
                    string country = "";
                    string city = "";
                    string region = "";

                    string[] segsWords = paths[2].Split('-');

                    if (segsWords.Length == 1)
                    {
                        foreach (var word in segsWords)
                        {
                            if (word != "")
                                country += char.ToUpper(word[0]) + word.Substring(1);
                        }
                    }
                    else if (segsWords.Length > 1)
                    {
                        foreach (var word in segsWords)
                        {
                            if (word != "")
                                country += char.ToUpper(word[0]) + word.Substring(1) + " ";
                        }
                    }

                    if (country.EndsWith(" "))
                        country = country.TrimEnd();

                    segsWords = paths[3].Split('-');

                    if (segsWords.Length == 1)
                    {
                        foreach (var word in segsWords)
                        {
                            if (word != "")
                                city += char.ToUpper(word[0]) + word.Substring(1);
                        }
                    }
                    else if (segsWords.Length > 1)
                    {
                        foreach (var word in segsWords)
                        {
                            if (word != "")
                                city += char.ToUpper(word[0]) + word.Substring(1) + " ";
                        }
                    }

                    if (city.EndsWith(" "))
                        city = city.TrimEnd();

                    segsWords = paths[1].Split('-');

                    if (segsWords.Length == 1)
                    {
                        foreach (var word in segsWords)
                        {
                            if (word != "")
                                region += char.ToUpper(word[0]) + word.Substring(1);
                        }
                    }
                    else if (segsWords.Length > 1)
                    {
                        foreach (var word in segsWords)
                        {
                            if (word != "")
                                region += char.ToUpper(word[0]) + word.Substring(1) + " ";
                        }
                    }

                    if (region.EndsWith(" "))
                        region = region.TrimEnd();

                    if (region != "" && country != "" && city != "")
                    {
                        string state = Sql.GetStateByRegionCountryCityTbl(region, country, city, session);
                        if (state != "" && state != city)
                        {
                            redPath += pathSegs[1] + pathSegs[2] + state.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + pathSegs[3] + pathSegs[4] + pathSegs[5];

                            validUrl = redPath;
                            return false;
                        }
                    }
                    else if (region == "" && country != "" && city != "")
                    {
                        string state = Sql.GetStateByCountryCityTbl(country, city, session);
                        if (state != "" && state != city)
                        {
                            redPath += pathSegs[1] + pathSegs[2] + state.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + pathSegs[3] + pathSegs[4] + pathSegs[5];

                            validUrl = redPath;
                            return false;
                        }
                    }
                }
            }
            else if (paths.Length == 3)
            {
                if (paths[2] == "channel-partners")
                    return true;
            }
            else if (paths.Length == 2 && paths[paths.Length - 1].TrimEnd('/') == "partnerships")
            {
                return true;
            }
            else if (paths.Length == 4 && pathSegs[1].Contains("profile/"))
            {
                return true;
            }
            else if (paths.Length <= 6 && paths[paths.Length - 1].TrimEnd('/') == "channel-partners")
            {
                return true;
            }
            else if (paths.Length <= 7 && paths[paths.Length - 2].TrimEnd('/') == "channel-partners")
            {
                return true;
            }
            else
                return false;

            return true;
        }

        public static bool InsertProductsToUser(ElioUsers user, string integrationProducts, DBSession session)
        {
            if (user != null && user.Id > 0)
            {
                if (integrationProducts != "")
                {
                    if (integrationProducts.EndsWith(", "))
                        integrationProducts = integrationProducts.Substring(0, integrationProducts.Length - 2);

                    string[] list = integrationProducts.Split(',').ToArray();
                    if (list.Length > 0)
                    {
                        foreach (string item in list)
                        {
                            string itemDescription = item;
                            if (itemDescription != "")
                            {
                                if (itemDescription.StartsWith(" "))
                                    itemDescription = itemDescription.Substring(1);

                                if (itemDescription.EndsWith(" "))
                                    itemDescription = itemDescription.TrimEnd(' ');

                                if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                {
                                    ElioRegistrationProducts product = Sql.GetRegistrationProductsIDByDescription(itemDescription, session);
                                    if (product == null)
                                    {
                                        product = new ElioRegistrationProducts();
                                        product.Description = itemDescription;
                                        product.IsPublic = 1;
                                        product.Sysdate = DateTime.Now;

                                        DataLoader<ElioRegistrationProducts> insertLoader = new DataLoader<ElioRegistrationProducts>(session);
                                        insertLoader.Insert(product);
                                    }

                                    if (product != null)
                                    {
                                        bool exist = Sql.ExistUserRegistrationProduct(user.Id, product.Id, session);
                                        if (!exist)
                                        {
                                            ElioUsersRegistrationProducts userProducts = new ElioUsersRegistrationProducts();
                                            userProducts.UserId = user.Id;
                                            userProducts.RegProductsId = product.Id;

                                            DataLoader<ElioUsersRegistrationProducts> loader = new DataLoader<ElioUsersRegistrationProducts>(session);
                                            loader.Insert(userProducts);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

            return true;
        }

        public static bool InsertProducts(string integrationProducts, DBSession session)
        {
            bool inserted = false;

            if (integrationProducts != "")
            {
                if (integrationProducts.EndsWith(", "))
                    integrationProducts = integrationProducts.Substring(0, integrationProducts.Length - 2);

                string[] list = integrationProducts.Split(',').ToArray();
                if (list.Length > 0)
                {
                    foreach (string item in list)
                    {
                        string itemDescription = item;
                        if (itemDescription != "")
                        {
                            if (itemDescription.StartsWith(" "))
                                itemDescription = itemDescription.Substring(1);

                            if (itemDescription.EndsWith(" "))
                                itemDescription = itemDescription.TrimEnd(' ');

                            if (itemDescription.EndsWith(","))
                                itemDescription = itemDescription.TrimEnd(',');

                            ElioRegistrationProducts product = Sql.GetRegistrationProductsIDByDescription(itemDescription, session);
                            if (product == null)
                            {
                                product = new ElioRegistrationProducts();
                                product.Description = itemDescription;
                                product.IsPublic = 1;
                                product.Sysdate = DateTime.Now;

                                DataLoader<ElioRegistrationProducts> insertLoader = new DataLoader<ElioRegistrationProducts>(session);
                                insertLoader.Insert(product);

                                inserted = true;
                            }
                        }
                    }
                }
                else
                    return false;
            }
            else
                return false;

            return inserted;
        }

        public static bool InsertProduct(string product, int isPublic, DBSession session)
        {
            bool inserted = false;

            if (product != "")
            {
                if (product.EndsWith(", "))
                    product = product.Substring(0, product.Length - 2);

                if (product.StartsWith(" "))
                    product = product.TrimStart(' ');

                if (product.EndsWith(" "))
                    product = product.TrimEnd(' ');

                if (product.EndsWith(","))
                    product = product.TrimEnd(',');

                ElioRegistrationProducts regProduct = Sql.GetRegistrationProductsIDByDescription(product, session);
                if (regProduct == null)
                {
                    regProduct = new ElioRegistrationProducts();
                    regProduct.Description = product;
                    regProduct.IsPublic = isPublic;
                    regProduct.Sysdate = DateTime.Now;

                    DataLoader<ElioRegistrationProducts> insertLoader = new DataLoader<ElioRegistrationProducts>(session);
                    insertLoader.Insert(regProduct);

                    inserted = true;
                }
                else
                    return inserted;
            }
            else
                return inserted;

            return inserted;
        }

        public static bool UpdateUserCollaborationLibraryFilesPathDirectoryAndMoveFiles(HttpServerUtility Server, int userId, int libraryFileCategoryId, string oldDirectoryDescription, string newDirectoryDescription, bool isOnboardingLibrary, DBSession session)
        {
            if (!isOnboardingLibrary)
            {
                DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);

                List<ElioCollaborationUsersLibraryFiles> files = loader.Load(@"SELECT *
                                                                            FROM Elio_collaboration_users_library_files
                                                                            where category_id = @category_id"
                                                                , DatabaseHelper.CreateIntParameter("@category_id", libraryFileCategoryId));

                foreach (ElioCollaborationUsersLibraryFiles file in files)
                {
                    file.FilePath = file.FilePath.Replace(oldDirectoryDescription, newDirectoryDescription);

                    if (!string.IsNullOrEmpty(file.PreviewFilePath))
                        file.PreviewFilePath = file.PreviewFilePath.Replace(oldDirectoryDescription, newDirectoryDescription);

                    file.LastUpdate = DateTime.Now;

                    loader.Update(file);

                    string userGuid = file.FilePath.Split('/')[0].ToString();

                    string serverMapPathTargetFolderFrom = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + userGuid + "\\" + oldDirectoryDescription + "\\";
                    string serverMapPathTargetFolderTo = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + userGuid + "\\" + newDirectoryDescription + "\\";

                    if (Directory.Exists(serverMapPathTargetFolderFrom))
                        Directory.Move(serverMapPathTargetFolderFrom, serverMapPathTargetFolderTo);
                }
            }
            else
            {
                DataLoader<ElioOnboardingUsersLibraryFiles> loader = new DataLoader<ElioOnboardingUsersLibraryFiles>(session);

                List<ElioOnboardingUsersLibraryFiles> files = loader.Load(@"SELECT *
                                                                            FROM Elio_onboarding_users_library_files
                                                                            where category_id = @category_id"
                                                                , DatabaseHelper.CreateIntParameter("@category_id", libraryFileCategoryId));

                foreach (ElioOnboardingUsersLibraryFiles file in files)
                {
                    file.FilePath = file.FilePath.Replace(oldDirectoryDescription, newDirectoryDescription);

                    if (!string.IsNullOrEmpty(file.PreviewFilePath))
                        file.PreviewFilePath = file.PreviewFilePath.Replace(oldDirectoryDescription, newDirectoryDescription);

                    file.LastUpdate = DateTime.Now;

                    loader.Update(file);

                    string userGuid = file.FilePath.Split('/')[0].ToString();

                    string serverMapPathTargetFolderFrom = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["OnboardingLibraryTargetFolder"].ToString()) + userGuid + "\\" + oldDirectoryDescription + "\\";
                    string serverMapPathTargetFolderTo = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["OnboardingLibraryTargetFolder"].ToString()) + userGuid + "\\" + newDirectoryDescription + "\\";

                    if (Directory.Exists(serverMapPathTargetFolderFrom))
                        Directory.Move(serverMapPathTargetFolderFrom, serverMapPathTargetFolderTo);
                }
            }

            return true;
        }

        public static string ConvertVendorDealsSumAmountToVendorCurrency(int vendorId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int? year, out string averageSizeAmount, DBSession session)
        {
            decimal SumAmount = 0;
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            string vendorCurrencySymbol = "";

            NumberStyles style;
            CultureInfo provider;
            string cultInfoL = "";
            string cultInfoU = "";

            style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;

            DataLoader<ElioRegistrationDeals> loaderDeals = new DataLoader<ElioRegistrationDeals>(session);

            string query = @"select * 
                            from  Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                      and rd.vendor_id = @vendor_id
                      and rd.is_active = 1 ";

            if (year != null)
                query += "and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            " and usa.is_active = 1 ";
            }

            List<ElioRegistrationDeals> deals = loaderDeals.Load(query
                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            if (deals.Count > 0)
            {
                foreach (ElioRegistrationDeals deal in deals)
                {
                    //SumAmount = deal.Amount;

                    if (string.IsNullOrEmpty(deal.CurId))
                    {
                        string partnerCountry = "";

                        ElioUsers dealPartner = Sql.GetUserById(deal.ResellerId, session);
                        if (dealPartner != null)
                            partnerCountry = dealPartner.Country;

                        if (partnerCountry != "")
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                            if (countryCurrency != null)
                            {
                                if (deal != null)
                                {
                                    deal.CurId = countryCurrency.CurId;

                                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);
                                    loader.Update(deal);

                                    resellerCurrencyID = countryCurrency.CurrencyId;
                                }
                            }
                        }
                    }
                    else
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                        if (countryCurrency != null)
                        {
                            resellerCurrencyID = countryCurrency.CurrencyId;
                        }
                    }

                    ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(deal.VendorId, session);
                    if (vendorCurrency != null)
                    {
                        vendorCurrencyID = vendorCurrency.CurrencyId;
                        vendorCurrencySymbol = vendorCurrency.CurrencySymbol;

                        cultInfoL = vendorCurrency.CurId.ToLower();
                        cultInfoU = vendorCurrency.CurId.ToUpper();
                    }
                    else
                    {
                        ElioUsers vendor = Sql.GetUserById(deal.VendorId, session);
                        if (vendor != null)
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                            if (countryCurrency != null)
                            {
                                vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                                vendorCurrencyID = countryCurrency.CurrencyId;

                                cultInfoL = countryCurrency.CurId.ToLower();
                                cultInfoU = countryCurrency.CurId.ToUpper();
                            }
                        }
                    }

                    if (vendorCurrencyID != "" && resellerCurrencyID != "")
                    {
                        if (vendorCurrencyID != resellerCurrencyID)
                        {
                            double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount)), resellerCurrencyID, vendorCurrencyID);
                            if (convertedAmount > 0)
                            {
                                SumAmount += Convert.ToDecimal(convertedAmount);
                            }
                            //else
                            //    SumAmount += Convert.ToDecimal(convertedAmount);
                        }
                        else
                            SumAmount += Convert.ToDecimal(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));
                    }
                    else
                        SumAmount += Convert.ToDecimal(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));
                }

                provider = new CultureInfo(cultInfoL + "-" + cultInfoU);

                decimal avg = SumAmount / deals.Count;
                decimal avgNumber = Decimal.Parse(avg.ToString(), style, provider);

                averageSizeAmount = vendorCurrencySymbol + " " + GlobalMethods.FixNumberPattern(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", avgNumber));
            }
            else
            {
                averageSizeAmount = vendorCurrencySymbol + " 0";

                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;

                    cultInfoL = vendorCurrency.CurId.ToLower();
                    cultInfoU = vendorCurrency.CurId.ToUpper();
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(vendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                            cultInfoL = countryCurrency.CurId.ToLower();
                            cultInfoU = countryCurrency.CurId.ToUpper();
                        }
                    }
                }
            }

            provider = new CultureInfo(cultInfoL + "-" + cultInfoU);

            SumAmount = Decimal.Parse(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", SumAmount), style, provider);

            return vendorCurrencySymbol + " " + GlobalMethods.FixNumberPattern(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", SumAmount));
        }

        public static string ConvertVendorDealsSumAmountToVendorCurrencyByChannelPartner(int vendorId, int resellerId, int loggedInRoleId, string subAccountEmail, bool isAdminRole, int? year, out string averageSizeAmount, DBSession session)
        {
            decimal SumAmount = 0;
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            string vendorCurrencySymbol = "";
            
            NumberStyles style;
            CultureInfo provider;
            string cultInfoL = "";
            string cultInfoU = "";

            style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                        
            DataLoader<ElioRegistrationDeals> loaderDeals = new DataLoader<ElioRegistrationDeals>(session);

            string query = @"select * 
                            from  Elio_registration_deals rd ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" where rd.is_public = 1
                      and rd.vendor_id = @vendor_id
                      and rd.reseller_id = @reseller_id
                      and rd.is_active = 1 ";

            if (year != null)
                query += "and (year(rd.created_date) >= " + year + " and year(rd.created_date) <= " + year + ") ";

            if (loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            " and usa.is_active = 1 ";
            }

            List<ElioRegistrationDeals> deals = loaderDeals.Load(query
                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                    , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            if (deals.Count > 0)
            {
                foreach (ElioRegistrationDeals deal in deals)
                {
                    //SumAmount = deal.Amount;

                    if (string.IsNullOrEmpty(deal.CurId))
                    {
                        string partnerCountry = "";

                        ElioUsers dealPartner = Sql.GetUserById(deal.ResellerId, session);
                        if (dealPartner != null)
                            partnerCountry = dealPartner.Country;

                        if (partnerCountry != "")
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                            if (countryCurrency != null)
                            {
                                if (deal != null)
                                {
                                    deal.CurId = countryCurrency.CurId;

                                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);
                                    loader.Update(deal);

                                    resellerCurrencyID = countryCurrency.CurrencyId;
                                }
                            }
                        }
                    }
                    else
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                        if (countryCurrency != null)
                        {
                            resellerCurrencyID = countryCurrency.CurrencyId;
                        }
                    }

                    ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(deal.VendorId, session);
                    if (vendorCurrency != null)
                    {
                        vendorCurrencyID = vendorCurrency.CurrencyId;
                        vendorCurrencySymbol = vendorCurrency.CurrencySymbol;

                        cultInfoL = vendorCurrency.CurId.ToLower();
                        cultInfoU = vendorCurrency.CurId.ToUpper();
                    }
                    else
                    {
                        ElioUsers vendor = Sql.GetUserById(deal.VendorId, session);
                        if (vendor != null)
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                            if (countryCurrency != null)
                            {
                                vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                                vendorCurrencyID = countryCurrency.CurrencyId;

                                cultInfoL = countryCurrency.CurId.ToLower();
                                cultInfoU = countryCurrency.CurId.ToUpper();
                            }
                        }
                    }

                    if (vendorCurrencyID != "" && resellerCurrencyID != "")
                    {
                        if (vendorCurrencyID != resellerCurrencyID)
                        {
                            double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount)), resellerCurrencyID, vendorCurrencyID);
                            if (convertedAmount > 0)
                            {
                                SumAmount += Convert.ToDecimal(convertedAmount);
                            }
                            //else
                            //    SumAmount += Convert.ToDecimal(convertedAmount);
                        }
                        else
                            SumAmount += Convert.ToDecimal(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));
                    }
                    else
                        SumAmount += Convert.ToDecimal(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));
                }

                provider = new CultureInfo(cultInfoL + "-" + cultInfoU);

                decimal avg = SumAmount / deals.Count;
                decimal avgNumber = Decimal.Parse(avg.ToString(), style, provider);

                averageSizeAmount = vendorCurrencySymbol + " " + GlobalMethods.FixNumberPattern(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", avgNumber));
            }
            else
            {
                averageSizeAmount = vendorCurrencySymbol + " 0";

                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;

                    cultInfoL = vendorCurrency.CurId.ToLower();
                    cultInfoU = vendorCurrency.CurId.ToUpper();
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(vendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                            cultInfoL = countryCurrency.CurId.ToLower();
                            cultInfoU = countryCurrency.CurId.ToUpper();
                        }
                    }
                }
            }

            provider = new CultureInfo(cultInfoL + "-" + cultInfoU);

            //SumAmount = Decimal.Parse(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", SumAmount), style, provider);

            return vendorCurrencySymbol + " " + GlobalMethods.FixNumberPattern(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", SumAmount));
        }

        public static string ConvertDealsSumAmountToVendorCurrency(int vendorId, List<ElioRegistrationDeals> deals, out string average, DBSession session)
        {
            double SumAmount = 0;
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            string vendorCurrencySymbol = "";

            if (deals.Count > 0)
            {
                foreach (ElioRegistrationDeals deal in deals)
                {
                    //SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));

                    if (string.IsNullOrEmpty(deal.CurId))
                    {
                        string partnerCountry = "";

                        ElioUsers dealPartner = Sql.GetUserById(deal.ResellerId, session);
                        if (dealPartner != null)
                            partnerCountry = dealPartner.Country;

                        if (partnerCountry != "")
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                            if (countryCurrency != null)
                            {
                                if (deal != null)
                                {
                                    deal.CurId = countryCurrency.CurId;

                                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);
                                    loader.Update(deal);

                                    resellerCurrencyID = countryCurrency.CurrencyId;
                                }
                            }
                        }
                    }
                    else
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                        if (countryCurrency != null)
                        {
                            resellerCurrencyID = countryCurrency.CurrencyId;
                        }
                    }

                    ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(deal.VendorId, session);
                    if (vendorCurrency != null)
                    {
                        vendorCurrencyID = vendorCurrency.CurrencyId;
                        vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                    }
                    else
                    {
                        ElioUsers vendor = Sql.GetUserById(deal.VendorId, session);
                        if (vendor != null)
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                            if (countryCurrency != null)
                            {
                                vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                                vendorCurrencyID = countryCurrency.CurrencyId;
                            }
                        }
                    }

                    if (vendorCurrencyID != "" && resellerCurrencyID != "")
                    {
                        if (vendorCurrencyID != resellerCurrencyID)
                        {
                            double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount)), resellerCurrencyID, vendorCurrencyID);
                            if (convertedAmount > 0)
                            {
                                SumAmount += convertedAmount;
                            }
                            //else
                            //    SumAmount += convertedAmount;
                        }
                        else
                            SumAmount += Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));
                    }
                    else
                        SumAmount += Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));
                }

                double avg = SumAmount / deals.Count;
                average = vendorCurrencySymbol + " " + GlobalMethods.FixNumberPattern(avg.ToString("0.00"));
            }
            else
            {
                average = vendorCurrencySymbol + " 0";

                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(vendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;
                        }
                    }
                }
            }

            return vendorCurrencySymbol + " " + GlobalMethods.FixNumberPattern(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", SumAmount));
        }

        public static double GetDealsSumAmountToVendorCurrency(int vendorId, List<ElioRegistrationDeals> deals, out string vendorCurrencySymbol, DBSession session)
        {
            double SumAmount = 0;
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            vendorCurrencySymbol = "";

            if (deals.Count > 0)
            {
                foreach (ElioRegistrationDeals deal in deals)
                {
                    //SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));

                    if (string.IsNullOrEmpty(deal.CurId))
                    {
                        string partnerCountry = "";

                        ElioUsers dealPartner = Sql.GetUserById(deal.ResellerId, session);
                        if (dealPartner != null)
                            partnerCountry = dealPartner.Country;

                        if (partnerCountry != "")
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                            if (countryCurrency != null)
                            {
                                if (deal != null)
                                {
                                    deal.CurId = countryCurrency.CurId;

                                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);
                                    loader.Update(deal);

                                    resellerCurrencyID = countryCurrency.CurrencyId;
                                }
                            }
                        }
                    }
                    else
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                        if (countryCurrency != null)
                        {
                            resellerCurrencyID = countryCurrency.CurrencyId;
                        }
                    }

                    ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(deal.VendorId, session);
                    if (vendorCurrency != null)
                    {
                        vendorCurrencyID = vendorCurrency.CurrencyId;
                        vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                    }
                    else
                    {
                        ElioUsers vendor = Sql.GetUserById(deal.VendorId, session);
                        if (vendor != null)
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                            if (countryCurrency != null)
                            {
                                vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                                vendorCurrencyID = countryCurrency.CurrencyId;
                            }
                        }
                    }

                    if (vendorCurrencyID != "" && resellerCurrencyID != "")
                    {
                        if (vendorCurrencyID != resellerCurrencyID)
                        {
                            double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount)), resellerCurrencyID, vendorCurrencyID);
                            if (convertedAmount > 0)
                            {
                                SumAmount += convertedAmount;
                            }
                            //else
                            //    SumAmount += convertedAmount;
                        }
                        else
                            SumAmount += Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));
                    }
                    else
                        SumAmount += Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount));
                }
            }

            return Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", SumAmount));
        }

        public static string ConvertLeadsSumAmountToVendorCurrency(int vendorId, List<ElioLeadDistributions> leads, out string average, DBSession session)
        {
            double SumAmount = 0;
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            string vendorCurrencySymbol = "";

            if (leads.Count > 0)
            {
                foreach (ElioLeadDistributions lead in leads)
                {
                    //SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));

                    if (string.IsNullOrEmpty(lead.CurId))
                    {
                        string partnerCountry = "";

                        ElioUsers dealPartner = Sql.GetUserById(lead.ResellerId, session);
                        if (dealPartner != null)
                            partnerCountry = dealPartner.Country;

                        if (partnerCountry != "")
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                            if (countryCurrency != null)
                            {
                                if (lead != null)
                                {
                                    lead.CurId = countryCurrency.CurId;

                                    DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);
                                    loader.Update(lead);

                                    resellerCurrencyID = countryCurrency.CurrencyId;
                                }
                            }
                        }
                    }
                    else
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(lead.CurId, session);
                        if (countryCurrency != null)
                        {
                            resellerCurrencyID = countryCurrency.CurrencyId;
                        }
                    }

                    ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(lead.VendorId, session);
                    if (vendorCurrency != null)
                    {
                        vendorCurrencyID = vendorCurrency.CurrencyId;
                        vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                    }
                    else
                    {
                        ElioUsers vendor = Sql.GetUserById(lead.VendorId, session);
                        if (vendor != null)
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                            if (countryCurrency != null)
                            {
                                vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                                vendorCurrencyID = countryCurrency.CurrencyId;
                            }
                        }
                    }

                    if (vendorCurrencyID != "" && resellerCurrencyID != "")
                    {
                        if (vendorCurrencyID != resellerCurrencyID)
                        {
                            double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount)), resellerCurrencyID, vendorCurrencyID);
                            if (convertedAmount > 0)
                            {
                                SumAmount += convertedAmount;
                            }
                            else
                                SumAmount += Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));
                        }
                        else
                        {
                            SumAmount += Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));
                        }
                    }
                    else
                        SumAmount += Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));
                }

                double avg = SumAmount / leads.Count;
                average = vendorCurrencySymbol + " " + GlobalMethods.FixNumberPattern(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", avg));
            }
            else
            {
                average = vendorCurrencySymbol + " 0";

                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(vendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;
                        }
                    }
                }
            }

            return vendorCurrencySymbol + " " + GlobalMethods.FixNumberPattern(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", SumAmount));
        }

        public static double GetLeadsSumAmountToVendorCurrency(int vendorId, List<ElioLeadDistributions> leads, out string vendorCurrencySymbol, DBSession session)
        {
            double SumAmount = 0;
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            vendorCurrencySymbol = "";

            if (leads.Count > 0)
            {
                foreach (ElioLeadDistributions lead in leads)
                {
                    //SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));

                    if (string.IsNullOrEmpty(lead.CurId))
                    {
                        string partnerCountry = "";

                        ElioUsers dealPartner = Sql.GetUserById(lead.ResellerId, session);
                        if (dealPartner != null)
                            partnerCountry = dealPartner.Country;

                        if (partnerCountry != "")
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                            if (countryCurrency != null)
                            {
                                if (lead != null)
                                {
                                    lead.CurId = countryCurrency.CurId;

                                    DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);
                                    loader.Update(lead);

                                    resellerCurrencyID = countryCurrency.CurrencyId;
                                }
                            }
                        }
                    }
                    else
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(lead.CurId, session);
                        if (countryCurrency != null)
                        {
                            resellerCurrencyID = countryCurrency.CurrencyId;
                        }
                    }

                    ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(lead.VendorId, session);
                    if (vendorCurrency != null)
                    {
                        vendorCurrencyID = vendorCurrency.CurrencyId;
                        vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                    }
                    else
                    {
                        ElioUsers vendor = Sql.GetUserById(lead.VendorId, session);
                        if (vendor != null)
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                            if (countryCurrency != null)
                            {
                                vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                                vendorCurrencyID = countryCurrency.CurrencyId;
                            }
                        }
                    }

                    if (vendorCurrencyID != "" && resellerCurrencyID != "")
                    {
                        if (vendorCurrencyID != resellerCurrencyID)
                        {
                            double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount)), resellerCurrencyID, vendorCurrencyID);
                            if (convertedAmount > 0)
                            {
                                SumAmount += convertedAmount;
                            }
                            else
                                SumAmount += Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));
                        }
                        else
                        {
                            SumAmount += Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));
                        }
                    }
                    else
                        SumAmount += Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));
                }
            }

            return Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", SumAmount));
        }

        public static string ConvertLeadSumAmountToVendorCurrency(int vendorId, ElioLeadDistributions lead, out string average, DBSession session)
        {
            double SumAmount = 0;
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            string vendorCurrencySymbol = "";

            if (lead != null)
            {
                //SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));

                if (string.IsNullOrEmpty(lead.CurId))
                {
                    string partnerCountry = "";

                    ElioUsers dealPartner = Sql.GetUserById(lead.ResellerId, session);
                    if (dealPartner != null)
                        partnerCountry = dealPartner.Country;

                    if (partnerCountry != "")
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                        if (countryCurrency != null)
                        {
                            if (lead != null)
                            {
                                lead.CurId = countryCurrency.CurId;

                                DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);
                                loader.Update(lead);

                                resellerCurrencyID = countryCurrency.CurrencyId;
                            }
                        }
                    }
                }
                else
                {
                    ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(lead.CurId, session);
                    if (countryCurrency != null)
                    {
                        resellerCurrencyID = countryCurrency.CurrencyId;
                    }
                }

                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(lead.VendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencyID = vendorCurrency.CurrencyId;
                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(lead.VendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                            vendorCurrencyID = countryCurrency.CurrencyId;
                        }
                    }
                }

                if (vendorCurrencyID != "" && resellerCurrencyID != "")
                {
                    if (vendorCurrencyID != resellerCurrencyID)
                    {
                        double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount)), resellerCurrencyID, vendorCurrencyID);
                        if (convertedAmount > 0)
                        {
                            SumAmount = convertedAmount;
                        }
                        else
                            SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));
                    }
                    else
                        SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));
                }
                else
                    SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));

                average = vendorCurrencySymbol + " " + GlobalMethods.FixNumberPattern(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", SumAmount));
            }
            else
            {
                average = vendorCurrencySymbol + " 0";

                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(vendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            vendorCurrencySymbol = countryCurrency.CurrencySymbol;
                        }
                    }
                }
            }

            return vendorCurrencySymbol + " " + GlobalMethods.FixNumberPattern(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", SumAmount));
        }

		public static double ConvertLeadSumAmountToVendorCurrencyNoSymbol(int vendorId, ElioLeadDistributions lead, DBSession session)
        {
            double SumAmount = 0;
            string vendorCurrencyID = "";
            string resellerCurrencyID = "";
            //string vendorCurrencySymbol = "";

            if (lead != null)
            {
                //SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));

                if (string.IsNullOrEmpty(lead.CurId))
                {
                    string partnerCountry = "";

                    ElioUsers dealPartner = Sql.GetUserById(lead.ResellerId, session);
                    if (dealPartner != null)
                        partnerCountry = dealPartner.Country;

                    if (partnerCountry != "")
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                        if (countryCurrency != null)
                        {
                            if (lead != null)
                            {
                                lead.CurId = countryCurrency.CurId;

                                DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);
                                loader.Update(lead);

                                resellerCurrencyID = countryCurrency.CurrencyId;
                            }
                        }
                    }
                }
                else
                {
                    ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCurId(lead.CurId, session);
                    if (countryCurrency != null)
                    {
                        resellerCurrencyID = countryCurrency.CurrencyId;
                    }
                }

                ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(lead.VendorId, session);
                if (vendorCurrency != null)
                {
                    vendorCurrencyID = vendorCurrency.CurrencyId;
                    //vendorCurrencySymbol = vendorCurrency.CurrencySymbol;
                }
                else
                {
                    ElioUsers vendor = Sql.GetUserById(lead.VendorId, session);
                    if (vendor != null)
                    {
                        ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vendor.Country, session);
                        if (countryCurrency != null)
                        {
                            //vendorCurrencySymbol = countryCurrency.CurrencySymbol;

                            vendorCurrencyID = countryCurrency.CurrencyId;
                        }
                    }
                }

                if (vendorCurrencyID != "" && resellerCurrencyID != "")
                {
                    if (vendorCurrencyID != resellerCurrencyID)
                    {
                        double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount)), resellerCurrencyID, vendorCurrencyID);
                        if (convertedAmount > 0)
                        {
                            SumAmount = convertedAmount;
                        }
                        else
                            SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));
                    }
                    else
                        SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));
                }
                else
                    SumAmount = Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount));
            }
            
            return SumAmount;
        }
		
        public static bool SetResellerTierStatus(int resellerId, int vendorId, int colVendResId, int isSetByVendor, bool isVendor, out string errorMsg, out string tierStatus, DBSession session)
        {
            errorMsg = "";
            bool success = false;
            string vendorCurrencySymbol = "";
            tierStatus = "";

            bool hasTiers = Sql.ExistTiersByUser(vendorId, session);
            if (hasTiers)
            {
                bool canUpdateTier = SqlCollaboration.CanSetTierStatus(vendorId, resellerId, session);

                if (canUpdateTier || isVendor)
                {
                    double totalRevenues = StatsDB.GetVendorTotalRevenuesAmount(colVendResId, vendorId, resellerId, out vendorCurrencySymbol, out tierStatus, session);
                    if (totalRevenues > 0 && tierStatus != "")
                    {
                        success = SqlCollaboration.UpdatePartnerTierStatus(vendorId, resellerId, tierStatus, isSetByVendor, session);
                        if (!success)
                        {
                            errorMsg = "Could not find vendor and reseller collaboration! Tier status was not updated.";
                        }
                    }
                }
            }

            return success;
        }

        public static string AddNewCollaborationUserRegistrationToPRM(ElioUsers vendor, string newUserEmail, string username, string password, HttpRequest request, DBSession session)
        {
            string message = "";

            if (vendor != null)
            {
                int userApplicationType = (int)UserApplicationType.NotRegistered;
                bool isFullRegistered = false;

                bool hasAlreadyGetInvitation = SqlCollaboration.HasInvitationRequestThisEmailBySpecificUser(vendor.Id, newUserEmail, session);
                if (hasAlreadyGetInvitation)
                {
                    message = "You have already send invitation to this email " + newUserEmail;
                    return message;
                }

                bool isRegisteredEmail = Sql.IsRegisteredEmail(newUserEmail, out userApplicationType, out isFullRegistered, session);
                if (!isRegisteredEmail)
                {
                    ElioCollaborationUsersInvitations defaultInvitation = SqlCollaboration.GetUserInvitationById(1, session);

                    if (defaultInvitation == null)
                    {
                        message = "No invitation to send was found";
                    }
                    else
                    {
                        #region Add User Invitation

                        ElioCollaborationUsersInvitations invitation = new ElioCollaborationUsersInvitations();

                        invitation.UserId = vendor.Id;
                        invitation.InvSubject = defaultInvitation.InvSubject;
                        invitation.InvContent = defaultInvitation.InvContent;
                        invitation.DateCreated = DateTime.Now;
                        invitation.LastUpdated = DateTime.Now;
                        invitation.IsPublic = 1;

                        DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                        loader.Insert(invitation);

                        #endregion

                        #region Add Collaboration Partners Emails

                        int collaborationId = SqlCollaboration.GetCollaborationIdFromPartnerInvitation(vendor.Id, newUserEmail, session);

                        ElioUsers collaborationUser = new ElioUsers();

                        if (collaborationId < 0)
                        {
                            #region Insert New Collaboration User

                            collaborationUser.CompanyType = EnumHelper.GetDescription(Types.Resellers).ToString();
                            collaborationUser.CompanyName = "Collaboration Company Name";
                            collaborationUser.Email = newUserEmail.Trim();
                            collaborationUser.GuId = Guid.NewGuid().ToString();
                            collaborationUser.CompanyLogo = "~/Images/CollaborationTool/collabor_1.png";
                            collaborationUser.PersonalImage = string.Empty;
                            collaborationUser.WebSite = string.Empty;
                            collaborationUser.Address = "Collaboration Company Address";
                            collaborationUser.Country = "Collaboration Company Country";
                            collaborationUser.Phone = string.Empty;
                            collaborationUser.LinkedInUrl = string.Empty;
                            collaborationUser.TwitterUrl = string.Empty;
                            collaborationUser.Ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
                            collaborationUser.SysDate = DateTime.Now;
                            collaborationUser.LastUpdated = DateTime.Now;
                            collaborationUser.UserApplicationType = Convert.ToInt32(UserApplicationType.Collaboration);
                            collaborationUser.Overview = string.Empty;
                            collaborationUser.Description = string.Empty;
                            collaborationUser.MashapeUsername = string.Empty;
                            collaborationUser.AccountStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                            collaborationUser.IsPublic = 0;
                            collaborationUser.CommunityStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                            collaborationUser.CommunityProfileCreated = DateTime.Now;
                            collaborationUser.CommunityProfileLastUpdated = DateTime.Now;
                            collaborationUser.HasBillingDetails = 0;
                            collaborationUser.BillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
                            collaborationUser.Username = username.Trim();
                            collaborationUser.UsernameEncrypted = MD5.Encrypt(collaborationUser.Username);
                            collaborationUser.Password = GeneratePasswordLib.CreateRandomStringWithMax11Chars(10);
                            collaborationUser.PasswordEncrypted = MD5.Encrypt(collaborationUser.Password);

                            collaborationUser = GlobalDBMethods.InsertNewUser(collaborationUser, session);

                            #endregion

                            #region Insert User Email Notifications Settings

                            GlobalDBMethods.FixUserEmailNotificationsSettingsData(collaborationUser, session);

                            #endregion

                            #region Insert Collaboration Vendor/Reseller

                            ElioCollaborationVendorsResellers newCollaboration = new ElioCollaborationVendorsResellers();

                            int masterUserId = vendor.Id;
                            int partnerUserId = collaborationUser.Id;

                            newCollaboration.MasterUserId = masterUserId;
                            newCollaboration.PartnerUserId = partnerUserId;
                            newCollaboration.IsActive = 1;
                            newCollaboration.InvitationStatus = CollaborateInvitationStatus.Pending.ToString();
                            newCollaboration.Sysdate = DateTime.Now;
                            newCollaboration.LastUpdated = DateTime.Now;

                            DataLoader<ElioCollaborationVendorsResellers> loader2 = new DataLoader<ElioCollaborationVendorsResellers>(session);
                            loader2.Insert(newCollaboration);

                            #endregion

                            #region Insert V/R Invitation

                            ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                            vendorResselerInvitation.UserId = vendor.Id;
                            vendorResselerInvitation.VendorResellerId = newCollaboration.Id;
                            vendorResselerInvitation.UserInvitationId = invitation.Id;
                            vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                            vendorResselerInvitation.RecipientEmail = newUserEmail;
                            vendorResselerInvitation.SendDate = DateTime.Now;
                            vendorResselerInvitation.LastUpdated = DateTime.Now;

                            DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                            loader3.Insert(vendorResselerInvitation);

                            #endregion
                        }
                        else
                        {
                            #region Insert V/R Invitation

                            ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                            vendorResselerInvitation.UserId = vendor.Id;
                            vendorResselerInvitation.VendorResellerId = collaborationId;
                            vendorResselerInvitation.UserInvitationId = invitation.Id;
                            vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                            vendorResselerInvitation.RecipientEmail = newUserEmail;
                            vendorResselerInvitation.SendDate = DateTime.Now;
                            vendorResselerInvitation.LastUpdated = DateTime.Now;

                            DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                            loader3.Insert(vendorResselerInvitation);

                            #endregion
                        }

                        try
                        {
                            #region Send Invitation Email

                            //maybe asynchronously send emails, to do
                            //string confirmationLink = FileHelper.AddToPhysicalRootPath(request) + "/free-sign-up?verificationViewID=" + collaborationUser.GuId + "&type=" + collaborationUser.UserApplicationType.ToString();
                            string confirmationLink = FileHelper.AddToPhysicalRootPath(request) + ControlLoader.SignUpPartner.Replace("{CompanyName}", Regex.Replace(vendor.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower()) + "?verificationViewID=" + collaborationUser.GuId + "&type=" + collaborationUser.UserApplicationType.ToString();
                            EmailSenderLib.CollaborationInvitationEmail(collaborationUser.UserApplicationType, newUserEmail.Trim(), vendor.CompanyName, vendor.CompanyLogo, collaborationUser.CompanyName, confirmationLink, "en", session);
                            //EmailSenderLib.CollaborationInvitationEmailWithUserText(collaborationUser.UserApplicationType, partnerEmail.Trim(), vSession.User.CompanyName, confirmationLink, TbxSubject.Text, TbxMsg.Text, vSession.Lang, session);

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion

                        message = "Success registration";
                    }
                }
            }
            else
            {
                message = "Vendor could not be found by url";
            }

            return message;
        }

        public static bool AddNewCollaborationUserSignUpToPRM(ElioUsers vendor, ElioUsers newChannelPartnerUser, HttpRequest request, DBSession session)
        {
            string message = "";

            if (vendor != null)
            {
                int userApplicationType = (int)UserApplicationType.NotRegistered;
                bool isFullRegistered = false;

                bool hasAlreadyGetInvitation = SqlCollaboration.HasInvitationRequestThisEmailBySpecificUser(vendor.Id, newChannelPartnerUser.Email, session);
                if (hasAlreadyGetInvitation)
                {
                    message = "You have already send invitation to this email " + newChannelPartnerUser.Email;
                    return false;
                }

                bool isRegisteredEmail = Sql.IsRegisteredEmail(newChannelPartnerUser.Email, out userApplicationType, out isFullRegistered, session);
                if (isRegisteredEmail)
                {
                    ElioCollaborationUsersInvitations defaultInvitation = SqlCollaboration.GetUserInvitationById(1, session);

                    if (defaultInvitation == null)
                    {
                        message = "No invitation to send was found";
                        return false;
                    }
                    else
                    {
                        #region Add User Invitation

                        ElioCollaborationUsersInvitations invitation = new ElioCollaborationUsersInvitations();

                        invitation.UserId = newChannelPartnerUser.Id;
                        invitation.InvSubject = defaultInvitation.InvSubject;
                        invitation.InvContent = defaultInvitation.InvContent;
                        invitation.DateCreated = DateTime.Now;
                        invitation.LastUpdated = DateTime.Now;
                        invitation.IsPublic = 1;

                        DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                        loader.Insert(invitation);

                        #endregion

                        #region Add Collaboration Partners Emails

                        int collaborationId = SqlCollaboration.GetCollaborationIdFromChannelPartnerInvitation(newChannelPartnerUser.Id, newChannelPartnerUser.Email, session);

                        if (collaborationId < 0)
                        {
                            #region Update As Collaboration User

                            newChannelPartnerUser.CompanyType = EnumHelper.GetDescription(Types.Resellers).ToString();
                            newChannelPartnerUser.CompanyLogo = "~/Images/CollaborationTool/collabor_1.png";
                            newChannelPartnerUser.UserApplicationType = Convert.ToInt32(UserApplicationType.Collaboration);

                            newChannelPartnerUser = GlobalDBMethods.UpDateUser(newChannelPartnerUser, session);

                            #endregion

                            #region Insert Collaboration Vendor/Reseller

                            ElioCollaborationVendorsResellers newCollaboration = new ElioCollaborationVendorsResellers();

                            int masterUserId = vendor.Id;
                            int partnerUserId = newChannelPartnerUser.Id;

                            newCollaboration.MasterUserId = masterUserId;
                            newCollaboration.PartnerUserId = partnerUserId;
                            newCollaboration.IsActive = 1;
                            newCollaboration.InvitationStatus = CollaborateInvitationStatus.Pending.ToString();
                            newCollaboration.Sysdate = DateTime.Now;
                            newCollaboration.LastUpdated = DateTime.Now;

                            DataLoader<ElioCollaborationVendorsResellers> loader2 = new DataLoader<ElioCollaborationVendorsResellers>(session);
                            loader2.Insert(newCollaboration);

                            #endregion

                            #region Insert V/R Invitation

                            ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                            vendorResselerInvitation.UserId = newChannelPartnerUser.Id;
                            vendorResselerInvitation.VendorResellerId = newCollaboration.Id;
                            vendorResselerInvitation.UserInvitationId = invitation.Id;
                            vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                            vendorResselerInvitation.RecipientEmail = newChannelPartnerUser.Email;
                            vendorResselerInvitation.SendDate = DateTime.Now;
                            vendorResselerInvitation.LastUpdated = DateTime.Now;

                            DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                            loader3.Insert(vendorResselerInvitation);

                            #endregion
                        }
                        else
                        {
                            #region Insert V/R Invitation If Not Already Exist

                            bool existInvitation = SqlCollaboration.HasInvitationRequestThisEmailBySpecificUser(vendor.Id, newChannelPartnerUser.Email, session);

                            if (!existInvitation)
                            {
                                ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                                vendorResselerInvitation.UserId = vendor.Id;
                                vendorResselerInvitation.VendorResellerId = collaborationId;
                                vendorResselerInvitation.UserInvitationId = invitation.Id;
                                vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                                vendorResselerInvitation.RecipientEmail = newChannelPartnerUser.Email;
                                vendorResselerInvitation.SendDate = DateTime.Now;
                                vendorResselerInvitation.LastUpdated = DateTime.Now;

                                DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                loader3.Insert(vendorResselerInvitation);
                            }

                            #endregion
                        }

                        try
                        {
                            #region Send Invitation Email to Reseller

                            //maybe asynchronously send emails, to do
                            //string confirmationLink = FileHelper.AddToPhysicalRootPath(request) + "/free-sign-up?verificationViewID=" + newChannelPartnerUser.GuId + "&type=" + newChannelPartnerUser.UserApplicationType.ToString();

                            //EmailSenderLib.CollaborationInvitationEmail(newChannelPartnerUser.UserApplicationType, newChannelPartnerUser.Email.Trim(), vendor.CompanyName, newChannelPartnerUser.CompanyName, confirmationLink, "en", session);
                            //EmailSenderLib.CollaborationInvitationEmailWithUserText(collaborationUser.UserApplicationType, partnerEmail.Trim(), vSession.User.CompanyName, confirmationLink, TbxSubject.Text, TbxMsg.Text, vSession.Lang, session);

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion

                        message = "Success registration";
                        return true;
                    }
                }
                else
                    return false;
            }
            else
            {
                message = "Vendor could not be found by url";
                return false;
            }
        }

        public static ElioUsers GetCompanyFromAbsoluteUrl(string path, DBSession session)
        {
            //string path = HttpContext.Current.Request.Url.AbsolutePath;
            string[] originalPathElements = path.Split('/');

            if (originalPathElements.Length > 0)
            {
                string vendorName = originalPathElements[originalPathElements.Length - 2];
                if (vendorName != "")
                {
                    ElioUsers vendor = Sql.GetVendorByCompanyNameLike(vendorName, session);
                    if (vendor == null)
                    {
                        vendorName = vendorName.Replace("-", " ");
                        vendor = Sql.GetVendorByCompanyNameLike(vendorName, session);
                        if (vendor == null)
                        {
                            vendorName = vendorName.Replace("%20", " ");
                            vendor = Sql.GetVendorByCompanyNameLike(vendorName, session);
                            if (vendor == null)
                            {
                                vendorName = vendorName.Replace("_", " ");
                                vendor = Sql.GetVendorByCompanyNameLike(vendorName, session);
                                if (vendor == null)
                                {
                                    vendorName = vendorName.Replace("-", " ");
                                    vendor = Sql.GetVendorByCompanyNameLike(vendorName, session);
                                    if (vendor == null)
                                    {
                                        vendorName = vendorName.Replace("inc", "").Trim();
                                        vendor = Sql.GetVendorByCompanyNameLike(vendorName, session);
                                        if (vendor == null)
                                        {
                                            vendorName = vendorName.Replace("ltd", "").Trim();
                                            vendor = Sql.GetVendorByCompanyNameLike(vendorName, session);
                                            if (vendor == null)
                                            {
                                                string[] vendorNameArray = vendorName.Split('-').ToArray();
                                                if (vendorNameArray.Length > 0)
                                                {
                                                    vendorName = vendorNameArray[0];
                                                    vendor = Sql.GetVendorByCompanyNameLike(vendorName, session);
                                                    if (vendor != null)
                                                        return vendor;
                                                    else
                                                        return null;
                                                }
                                                else
                                                {
                                                    vendorName = Regex.Replace(originalPathElements[originalPathElements.Length - 2], @"[^A-Za-z0-9]+", "-");
                                                    vendor = Sql.GetVendorByCompanyNameLike(vendorName, session);
                                                    if (vendor != null)
                                                        return vendor;
                                                    else
                                                        return null;
                                                }
                                            }
                                            else
                                                return vendor;
                                        }
                                        else
                                            return vendor;
                                    }
                                    else
                                        return vendor;
                                }
                                else
                                    return vendor;
                            }
                            else
                                return vendor;
                        }
                        else
                            return vendor;
                    }
                    else
                        return vendor;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public static void FixUserEmailNotificationsSettingsData(ElioUsers user, DBSession session)
        {
            List<ElioEmailNotifications> notifications = Sql.GetElioEmailNotifications(session);

            foreach (ElioEmailNotifications notification in notifications)
            {
                if (!Sql.ExistUserEmailNotificationsSettingsById(user.Id, notification.Id, session))
                {
                    ElioUserEmailNotificationsSettings newNotification = new ElioUserEmailNotificationsSettings();

                    newNotification.UserId = user.Id;
                    newNotification.EmaiNotificationsId = notification.Id;

                    DataLoader<ElioUserEmailNotificationsSettings> loader = new DataLoader<ElioUserEmailNotificationsSettings>(session);
                    loader.Insert(newNotification);
                }
            }
        }

        public static void SaveDealMonthDurationSetting(int vendorId, int monthDefaultDurationSetting, DBSession session)
        {
            if (monthDefaultDurationSetting != 0)
            {
                if (!Sql.HasDealMonthDurationSettings(vendorId, session))
                {
                    Sql.InsertUserDealMonthDuration(vendorId, monthDefaultDurationSetting, session);
                }
                else
                {
                    Sql.UpdateUserDealMonthDurationSettings(vendorId, monthDefaultDurationSetting, session);
                }
            }
            else
            {
                Sql.DeleteUserDealMonthDuration(vendorId, session);
            }
        }

        public static void FixUserOpenDealsStatus(ElioUsers user, DBSession session)
        {
            try
            {
                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                session.BeginTransaction();

                Sql.SetUserDealsAsExpiredAndLost(user, session);

                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError("DashboardDealRegistration.aspx --> GlobalDBMethods --> FixUserOpenDealsStatus", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public void FixOpenDealsStatus(ElioUsers user, DealStatus status, DealActivityStatus isActive, DBSession session)
        {
            List<ElioRegistrationDeals> deals = Sql.GetUserDealsByStatusAndActivity(user, (int)status, (int)isActive, "", -1, "", session);
            if (deals.Count > 0)
            {
                foreach (ElioRegistrationDeals deal in deals)
                {
                    if (deal.ExpectedClosedDate < DateTime.Now)
                    {
                        deal.Status = Convert.ToInt32(DealStatus.Expired);

                        DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);
                        loader.Update(deal);
                    }
                }
            }
        }

        public static bool AddUserCategoryFilesStorage(ElioUsers user, int categoryId, bool isOnboardingFile, DBSession session)
        {
            if (!isOnboardingFile)
            {
                List<ElioCollaborationBlobFiles> blobFiles = SqlCollaboration.GetTableBlobFilesSizeByCategoryId(user.Id, categoryId, session);
                if (blobFiles.Count > 0)
                {
                    foreach (ElioCollaborationBlobFiles blobFile in blobFiles)
                    {
                        bool deleted = SqlCollaboration.DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(user, blobFile.LibraryFilesId, false, isOnboardingFile, session);

                        if (!deleted)
                            return deleted;

                        #region Old way

                        //decimal fileSize = Convert.ToDecimal(blobFile.FileSize);
                        //double fileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(fileSize), "GB");

                        //ElioUserPacketStatus userPacketStatus = Sql.GetUserPacketStatusFeatures(user.Id, session);
                        //if (userPacketStatus != null)
                        //{
                        //    ElioUsersFeatures uFeatures = Sql.GetFeaturesbyUserType(user.BillingType, session);
                        //    if (uFeatures != null)
                        //    {
                        //        DataLoader<ElioUserPacketStatus> packetStatusLoader = new DataLoader<ElioUserPacketStatus>(session);

                        //        if (uFeatures.TotalLibraryStorage > userPacketStatus.AvailableLibraryStorageCount && Convert.ToDecimal(fileSizeGB) <= uFeatures.TotalLibraryStorage - userPacketStatus.AvailableLibraryStorageCount)
                        //            userPacketStatus.AvailableLibraryStorageCount += Convert.ToDecimal(fileSizeGB);
                        //        else
                        //            userPacketStatus.AvailableLibraryStorageCount = uFeatures.TotalLibraryStorage;

                        //        userPacketStatus.LastUpdate = DateTime.Now;
                        //        packetStatusLoader.Update(userPacketStatus);
                        //    }
                        //}
                        //else
                        //    return false;

                        #endregion
                    }
                }
                else
                {
                    List<ElioCollaborationUsersLibraryFiles> files = SqlCollaboration.GetCollaborationUserLibraryFilesByCategoryId(user.Id, categoryId, session);
                    if (files.Count > 0)
                    {
                        foreach (ElioCollaborationUsersLibraryFiles file in files)
                        {
                            bool deleted = SqlCollaboration.DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(user, file.Id, false, isOnboardingFile, session);

                            if (!deleted)
                                return deleted;
                        }
                    }
                }
            }
            else
            {
                List<ElioOnboardingBlobFiles> blobFiles = SqlCollaboration.GetTableOnboardingBlobFilesSizeByCategoryId(user.Id, categoryId, session);
                if (blobFiles.Count > 0)
                {
                    foreach (ElioOnboardingBlobFiles blobFile in blobFiles)
                    {
                        bool deleted = SqlCollaboration.DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(user, blobFile.LibraryFilesId, false, isOnboardingFile, session);

                        if (!deleted)
                            return deleted;
                    }
                }
                else
                {
                    List<ElioOnboardingUsersLibraryFiles> files = SqlCollaboration.GetOnboardingUserLibraryFilesByCategoryId(user.Id, categoryId, session);
                    if (files.Count > 0)
                    {
                        foreach (ElioOnboardingUsersLibraryFiles file in files)
                        {
                            bool deleted = SqlCollaboration.DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(user, file.Id, false, isOnboardingFile, session);

                            if (!deleted)
                                return deleted;
                        }
                    }
                }
            }

            return true;
        }

        public static bool FixUserPacketStatusStorageByFilesSizeGB(ElioUsers user, double fileSizeGB, DBSession session)
        {
            if (user != null)
            {
                ElioUserPacketStatus userPacketStatus = Sql.GetUserPacketStatusFeatures(user.Id, session);
                if (userPacketStatus != null)
                {
                    ElioUsersFeatures uFeatures = Sql.GetFeaturesbyUserType(user.BillingType, session);
                    if (uFeatures != null)
                    {
                        DataLoader<ElioUserPacketStatus> packetStatusLoader = new DataLoader<ElioUserPacketStatus>(session);

                        if (uFeatures.TotalLibraryStorage > userPacketStatus.AvailableLibraryStorageCount && Convert.ToDecimal(fileSizeGB) <= uFeatures.TotalLibraryStorage - userPacketStatus.AvailableLibraryStorageCount)
                            userPacketStatus.AvailableLibraryStorageCount += Convert.ToDecimal(fileSizeGB);
                        else
                            userPacketStatus.AvailableLibraryStorageCount = uFeatures.TotalLibraryStorage;

                        userPacketStatus.LastUpdate = DateTime.Now;
                        packetStatusLoader.Update(userPacketStatus);
                    }
                }
                else
                    return false;
            }

            return true;
        }

        public static bool AddUserFileStorage(ElioUsers user, int fileId, bool isCollaborationBlobFile, DBSession session)
        {
            decimal fileSize = 0;
            double fileSizeGB = 0;

            if (isCollaborationBlobFile)
            {
                ElioCollaborationBlobFiles blobFile = SqlCollaboration.GetCollaborationBlobFileByFileId(fileId, session);
                if (blobFile != null)
                {
                    fileSize = Convert.ToDecimal(blobFile.FileSize);
                    fileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(fileSize), "GB");
                }
                else
                    return false;
            }
            else
            {
                ElioOnboardingBlobFiles blobFile = SqlCollaboration.GetOnboardingBlobFileByFileId(fileId, session);
                if (blobFile != null)
                {
                    fileSize = Convert.ToDecimal(blobFile.FileSize);
                    fileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(fileSize), "GB");
                }
                else
                    return false;
            }

            if (fileSizeGB > 0)
            {
                return FixUserPacketStatusStorageByFilesSizeGB(user, fileSizeGB, session);
            }
            else
                return false;
        }

        public static bool AddUserPreviewFileStorage(ElioUsers user, int fileId, bool isCollaborationBlobFile, DBSession session)
        {
            decimal fileSize = 0;
            double fileSizeGB = 0;

            if (isCollaborationBlobFile)
            {
                ElioCollaborationBlobPreviewFiles blobPreviewFile = SqlCollaboration.GetCollaborationBlobPreviewFileByFileId(fileId, session);
                if (blobPreviewFile != null)
                {
                    fileSize = Convert.ToDecimal(blobPreviewFile.FileSize);
                    fileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(fileSize), "GB");
                }
                else
                    return false;
            }
            else
            {
                ElioOnboardingBlobPreviewFiles blobPreviewFile = SqlCollaboration.GetOnboardingBlobPreviewFileByFileId(fileId, session);
                if (blobPreviewFile != null)
                {
                    fileSize = Convert.ToDecimal(blobPreviewFile.FileSize);
                    fileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(fileSize), "GB");
                }
                else
                    return false;
            }

            if (fileSizeGB > 0)
            {
                return FixUserPacketStatusStorageByFilesSizeGB(user, fileSizeGB, session);
            }
            else
                return false;
        }

        public static bool ReduceUserFileStorage(int userId, double fileSize, DBSession session)
        {
            ElioUserPacketStatus userPacketStatus = Sql.GetUserPacketStatusFeatures(userId, session);
            if (userPacketStatus != null)
            {
                double fileSizeGB = GlobalMethods.ConvertSize(fileSize, "GB");
                if (fileSizeGB > 0)
                {
                    if (userPacketStatus.AvailableLibraryStorageCount > 0 && userPacketStatus.AvailableLibraryStorageCount >= Convert.ToDecimal(fileSizeGB))
                    {
                        userPacketStatus.AvailableLibraryStorageCount = userPacketStatus.AvailableLibraryStorageCount - Convert.ToDecimal(fileSizeGB);
                        userPacketStatus.LastUpdate = DateTime.Now;

                        DataLoader<ElioUserPacketStatus> packetStatusLoader = new DataLoader<ElioUserPacketStatus>(session);
                        packetStatusLoader.Update(userPacketStatus);
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

            return true;
        }

        public static List<ElioUsers> GetHomePageClientsProfiles(string ids, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load(@"Select * from Elio_users Where id IN (" + ids + ") order by id");
        }

        public static string ShowUserPhone(ElioUsers user, DBSession session, out bool hasPhone)
        {
            if (session.Connection.State == ConnectionState.Closed)
                session.OpenConnection();

            hasPhone = false;
            if (!string.IsNullOrEmpty(user.Country))
            {
                ElioCountries country = Sql.GetCountryByCountryName(user.Country, session);
                if (country != null)
                {
                    if (user.Phone.Length > country.Prefix.Length)
                    {
                        hasPhone = (user.Phone.Length - country.Prefix.Length > 0) || !(user.Phone.StartsWith(country.Prefix));

                        if (!string.IsNullOrEmpty(user.Phone))
                        {
                            if (!string.IsNullOrEmpty(country.Prefix))
                            {
                                if (user.Phone.StartsWith(country.Prefix))
                                    return "+ " + country.Prefix + " - " + user.Phone.Substring(country.Prefix.Length, user.Phone.Length - country.Prefix.Length);

                                else
                                    return "+ " + country.Prefix + " - " + user.Phone;
                            }
                            else
                                return "+ " + user.Phone;
                        }
                        else
                            if (!string.IsNullOrEmpty(country.Prefix))
                                return "+ " + country.Prefix + " -";
                            else
                                return "-";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(country.Prefix))
                            return "+ " + country.Prefix + " -";
                        else
                            return "-";
                    }
                }
                else
                    return "-";
            }
            else
                return "-";
        }

        public static bool UserHasAvailableStorage(int userId, decimal fileContentLength, DBSession session)
        {
            double fileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(fileContentLength), "GB");

            ElioUserPacketStatus userPacketStatus = Sql.GetUserPacketStatusFeatures(userId, session);

            return userPacketStatus != null && userPacketStatus.AvailableLibraryStorageCount > 0 && userPacketStatus.AvailableLibraryStorageCount >= Convert.ToDecimal(fileSizeGB);
        }

        public static decimal GetUserUsedStorageSpacePercent(int userId, ref decimal totalPacketItemValue, ref decimal availablePacketItemValue, ref decimal usedSpace, DBSession session)
        {
            ElioUsers user = Sql.GetUserById(userId, session);
            if (user != null)
            {
                ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(user.BillingType, session);
                if (packet != null)
                {
                    List<ElioPacketsIJFeaturesItems> packetFeatureItems = Sql.GetPacketFeaturesItems(packet.Id, session);

                    foreach (ElioPacketsIJFeaturesItems item in packetFeatureItems)
                    {
                        if (item.ItemDescription == "LibraryStorage")
                        {
                            totalPacketItemValue = item.FreeItemsNo;
                            break;
                        }
                    }

                    ElioUserPacketStatus userPacketStatus = Sql.GetUserPacketStatusFeatures(user.Id, session);
                    if (userPacketStatus != null)
                    {
                        availablePacketItemValue = userPacketStatus.AvailableLibraryStorageCount;

                        decimal usedSpacePercent = ((totalPacketItemValue - availablePacketItemValue) * 100) / totalPacketItemValue;

                        usedSpace = totalPacketItemValue - availablePacketItemValue;
                        return usedSpacePercent;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        public static decimal GetUserUsedStorageSpacePercent2(int userId, string packetFeature, ref decimal totalPacketItemValue, ref decimal availablePacketItemValue, ref decimal usedSpace, ref decimal freeSpacePercent, DBSession session)
        {
            ElioUsers user = Sql.GetUserById(userId, session);
            if (user != null)
            {
                DataTable table = session.GetDataTable(@"SELECT *
                                                    FROM Elio_packet_features_items pfi
                                                    INNER JOIN Elio_packet_features pf 
                                                        ON pf.id = pfi.feature_id
                                                    INNER JOIN Elio_packets p 
                                                        ON p.id = pfi.pack_id 
                                                    INNER JOIN Elio_users_billing_type_packets btp 
                                                        ON p.id = btp.pack_id 
                                                    INNER JOIN Elio_users_billing_type bt 
                                                        ON bt.id = btp.billing_type_id
                                                    INNER JOIN Elio_user_packet_status ups 
                                                        ON ups.pack_id = p.id
                                                    WHERE 1 = 1
                                                    AND p.is_active = 1
                                                    and pf.item_description = @item_description
                                                    AND btp.id = @billingPacketTypeId
                                                    and ups.user_id = @user_id"
                                                        , DatabaseHelper.CreateStringParameter("@item_description", packetFeature)
                                                        , DatabaseHelper.CreateIntParameter("@billingPacketTypeId", user.BillingType)
                                                        , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                if (table.Rows.Count > 0)
                {
                    totalPacketItemValue = Convert.ToDecimal(table.Rows[0]["free_items_no"]);
                    availablePacketItemValue = Convert.ToDecimal(table.Rows[0]["available_libraryStorage_count"]);

                    decimal usedSpacePercent = ((totalPacketItemValue - availablePacketItemValue) * 100) / totalPacketItemValue;

                    usedSpace = totalPacketItemValue - availablePacketItemValue;
                    freeSpacePercent = 100 - usedSpacePercent;
                    return usedSpacePercent;
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        public static bool GetGroupMembersForMessages(int groupId, int isActive, int isPublic, string companyType, int senderUserId, out List<ElioUsers> groupPartners, DBSession session)
        {
            groupPartners = new List<ElioUsers>();

            if (groupId != -1)
            {
                ElioUsers user = new ElioUsers();

                List<ElioCollaborationUsersGroupMembersIJUsers> members = SqlCollaboration.GetUserGroupMembersIJUsersByGroupId(groupId, isActive, isPublic, null, session);

                if (members.Count > 0)
                {
                    if (companyType == Types.Vendors.ToString())
                    {
                        foreach (ElioCollaborationUsersGroupMembersIJUsers member in members)
                        {
                            if (senderUserId != member.GroupRetailorId)
                            {
                                user = Sql.GetUserById(member.GroupRetailorId, session);
                                if (user != null)
                                    groupPartners.Add(user);
                            }
                        }
                    }
                    else if (companyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                    {
                        bool masterInserted = false;
                        foreach (ElioCollaborationUsersGroupMembersIJUsers member in members)
                        {
                            if (!masterInserted)
                            {
                                user = Sql.GetUserById(member.CreatorUserId, session);
                                if (user != null)
                                    groupPartners.Add(user);

                                masterInserted = true;
                            }

                            if (senderUserId != member.GroupRetailorId)
                            {
                                user = Sql.GetUserById(member.GroupRetailorId, session);
                                if (user != null)
                                    groupPartners.Add(user);
                            }
                        }
                    }
                }
            }

            return (groupPartners.Count > 0) ? true : false;
        }

        public static string LoadUserIndustriesCommaDelimetered(int userId, DBSession session)
        {
            string strIndustries = "-";

            if (userId != 0)
            {
                List<ElioIndustries> industries = Sql.GetUsersIndustries(userId, session);
                foreach (ElioIndustries industry in industries)
                {
                    strIndustries += industry.IndustryDescription + ", ";
                }
                strIndustries = (industries.Count > 0) ? strIndustries.Substring(0, strIndustries.Length - 2) : "-";
            }

            return strIndustries;
        }

        public static string LoadUserMarketsCommaDelimitered(int userId, DBSession session)
        {
            string strMarkets = "-";

            if (userId != 0)
            {
                List<ElioMarkets> userMarkets = Sql.GetUsersMarkets(userId, session);
                foreach (ElioMarkets userMarket in userMarkets)
                {
                    strMarkets += userMarket.MarketDescription + ", ";
                }
                strMarkets = (userMarkets.Count > 0) ? strMarkets.Substring(0, strMarkets.Length - 2) : "-";
            }

            return strMarkets;
        }

        public static string LoadUserPartnersCommaDelimitered(int userId, DBSession session)
        {
            string strPrograms = "-";

            if (userId != 0)
            {
                List<ElioPartners> userPartners = Sql.GetUsersPartners(userId, session);
                foreach (ElioPartners userPartner in userPartners)
                {
                    strPrograms += userPartner.PartnerDescription + ", ";
                }
                strPrograms = (userPartners.Count > 0) ? strPrograms.Substring(0, strPrograms.Length - 2) : "-";
            }

            return strPrograms;
        }

        public static string LoadUserSubCategoriesCommaDelimitered(int userId, DBSession session)
        {
            string strSubcategories = "-";

            if (userId != 0)
            {
                List<ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers> profileSubcategories = Sql.GetUserSubcategoriesById(userId, session);
                foreach (ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers subcategory in profileSubcategories)
                {
                    strSubcategories += subcategory.DescriptionSubcategory + ", ";
                }
                strSubcategories = (profileSubcategories.Count > 0) ? strSubcategories.Substring(0, strSubcategories.Length - 2) : "-";
            }

            return strSubcategories;
        }

        public static string LoadUserApiesCommaDelimitered(int userId, DBSession session)
        {
            string strApi = "-";

            if (userId != 0)
            {
                List<ElioApies> userApies = Sql.GetUsersApies(userId, session);
                foreach (ElioApies userApi in userApies)
                {
                    strApi += userApi.ApiDescription + ", ";
                }
                strApi = (userApies.Count > 0) ? strApi.Substring(0, strApi.Length - 2) : "-";
            }

            return strApi;
        }

        public static List<ElioCollaborationVendorsResellers> AddRemoveChatReceiversMessages(Mode conversationMode, Mode actionMode, List<ElioCollaborationVendorsResellers> vendorsResellersList, ElioCollaborationVendorsResellers collaborationParnter)
        {
            if (conversationMode == Mode.SimpleMode && actionMode == Mode.Any)
            {
                if (vendorsResellersList.Count > 0)
                {
                    if (collaborationParnter.Id == vendorsResellersList[0].Id)
                    {
                        vendorsResellersList.Clear();
                    }
                    else
                    {
                        vendorsResellersList.Clear();
                        vendorsResellersList.Add(collaborationParnter);

                    }
                }
                else
                    vendorsResellersList.Add(collaborationParnter);
            }
            else if (conversationMode == Mode.GroupMode)
            {
                bool existInList = false;

                if (actionMode == Mode.Add)
                {
                    if (vendorsResellersList.Count > 0)
                    {
                        foreach (ElioCollaborationVendorsResellers partner in vendorsResellersList)
                        {
                            if (partner.Id == collaborationParnter.Id)
                            {
                                existInList = true;
                                break;
                            }
                        }

                        if (!existInList)
                            vendorsResellersList.Add(collaborationParnter);
                    }
                    else
                        vendorsResellersList.Add(collaborationParnter);
                }
                else if (actionMode == Mode.Remove)
                {
                    foreach (ElioCollaborationVendorsResellers partner in vendorsResellersList)
                    {
                        if (partner.Id == collaborationParnter.Id)
                        {
                            vendorsResellersList.Remove(partner);
                            break;
                        }
                    }
                }
            }

            return vendorsResellersList;
        }

        public static bool HasAccessRights(int userId, DBSession session)
        {
            bool hasAccess = false;

            hasAccess = Sql.IsUserAdministrator(userId, session);

            return hasAccess;
        }

        public static string FixUrlByUserRole(int userId, int objectId, DBSession session)
        {
            string sessionGuid = (!Sql.IsUserAdministrator(userId, session)) ? Guid.NewGuid().ToString() : objectId.ToString();

            return sessionGuid;
        }

        public static string FixUrlByUserRole(int userId, string objectId, DBSession session)
        {
            string sessionGuid = (!Sql.IsUserAdministrator(userId, session)) ? Guid.NewGuid().ToString() : objectId;

            return sessionGuid;
        }

        public static List<ElioUsers> GetUsersSearchResultsNew(string categoryId, string category, string industryId, string verticalId, string programId, string marketId, string apiId, string country, string countryId, string city, string companyName, ElioSession vSession, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            
            GlobalMethods.ClearCriteriaSession(vSession, false);
            string strQuery = "";

            if (category == Types.Vendors.ToString())
            {
                strQuery += @"Select distinct(Elio_users.id) as id,company_name, replace(company_logo, '~/', '/') as company_logo
                            , website
                            , country
                            , city
                            , billing_type
                            , company_type
                            , overview
                            , Elio_users.sysdate
                            from Elio_users
                            left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id=Elio_users.id ";

                vSession.CategoryViewState = categoryId;

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                if (countryId != "0")
                {
                    vSession.CountryViewState = countryId;
                }

                strQuery += "where is_public=1 and company_type='" + category + "' and account_status=1 ";
                strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : "";
                strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : "";
                strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : "";
                strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : "";
                strQuery += (countryId != "0") ? "and country='" + country + "' " : "";
                strQuery += (city != "") ? "and city='" + city + "' " : "";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : "";
                strQuery += " GROUP BY Elio_users.id, billing_type, website, city, company_type,company_logo,country,company_name,overview,Elio_users.sysdate " +
                    "ORDER BY billing_type DESC, Elio_users.sysdate DESC";

                return loader.Load(strQuery);
            }
            else
            {
                strQuery += @"Select distinct(Elio_users.id),company_name, replace(company_logo, '~/', '/') as company_logo
                            , website
                            , country
                            , city
                            , billing_type
                            , company_type
                            , overview
                            , Elio_users.sysdate
                            from Elio_users " + Environment.NewLine;

                vSession.CategoryViewState = categoryId;

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                if (countryId != "0")
                {
                    vSession.CountryViewState = countryId;
                }

                strQuery += "where is_public=1 and company_type='" + category + "' and account_status=1 ";
                strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : "";
                strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : "";
                strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : "";
                strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : "";
                strQuery += (countryId != "0") ? "and country='" + country + "' " : "";
                strQuery += (city != "") ? "and city='" + city + "' " : "";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : "";
                strQuery += " GROUP BY Elio_users.id, billing_type, website, city, company_type,company_logo,country,company_name,overview, Elio_users.sysdate " +
                            " ORDER BY billing_type desc, Elio_users.sysdate DESC";

                return loader.Load(strQuery);
            }
        }

        public static string GetSearchResultsWithPagingNew(string categoryId, string category, string industryId, string verticalId, string programId, string marketId, string apiId, string country, string countryId, string city, string companyName, ElioSession vSession, DBSession session)
        {
            GlobalMethods.ClearCriteriaSession(vSession, false);
            string strQuery = "";

            if (category == Types.Vendors.ToString())
            {
                strQuery += @"Select distinct(Elio_users.id),company_name, company_logo
                            , isnull(round(avg(cast(rate as float)),2),'0') as r
                            , website
                            ,country
                            , city
                            , billing_type
                            , company_type
                            , overview
                            from Elio_users
                            left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id=Elio_users.id ";

                vSession.CategoryViewState = categoryId;

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                if (countryId != "0")
                {
                    vSession.CountryViewState = countryId;
                }

                strQuery += "where is_public=1 and company_type='" + category + "' and account_status=1 ";
                strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : "";
                strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : "";
                strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : "";
                strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : "";
                strQuery += (countryId != "0") ? "and country='" + country + "' " : "";
                strQuery += (city != "") ? "and city='" + city + "' " : "";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : "";
                strQuery += " GROUP BY Elio_users.id, billing_type, website, city, company_type,company_logo,country,company_name,overview " +
                    "ORDER BY billing_type DESC,r desc";
            }
            else
            {
                strQuery = @";WITH MyDataSet
                                 AS
                                 (";

                strQuery += @"Select distinct(Elio_users.id)
                            , ROW_NUMBER() over (order by billing_type desc) as row_index
                            --, isnull(round(avg(cast(rate as float)),2),'0') as r
                            , address
                            , city
                            , billing_type 
                            , company_type
                            , sysdate
                            from Elio_users
                            --left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id=Elio_users.id " + Environment.NewLine;

                vSession.CategoryViewState = categoryId;

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                if (countryId != "0")
                {
                    vSession.CountryViewState = countryId;
                }

                strQuery += "where is_public=1 and company_type='" + category + "' and account_status=1 ";
                strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : "";
                strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : "";
                strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : "";
                strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : "";
                strQuery += (countryId != "0") ? "and country='" + country + "' " : "";
                strQuery += (city != "") ? "and city='" + city + "' " : "";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : "";
                strQuery += " GROUP BY Elio_users.id, billing_type, address, city, company_type, sysdate";

                strQuery += @") ";

                //            strQuery += @",total_views
                //                            as
                //                            (";

                //            strQuery += @"Select company_id,sum(views) as totalViews 
                //	                      from Elio_companies_views nolock	
                //	                      GROUP BY company_id";

                //            strQuery += ")";


                strQuery += @" select id
                                ,row_index
                                --,r
                                ,address
                                ,city
                                ,billing_type
                                ,company_type
                                --,isnull(totalViews,0) as totalViews 
                            from MyDataSet
                            --left join total_views on total_views.company_id = MyDataSet.id " +
                                " ORDER BY sysdate DESC " +
                                " --, r desc";
            }

            return strQuery;
        }

        public static string GetSearchResultsWithPaging(int fromPageIndex, int toPageIndex, string categoryId, string category, string industryId, string verticalId, string programId, string marketId, string apiId, string country, string countryId, string city, string companyName, ElioSession vSession, DBSession session)
        {
            //GlobalMethods.ClearCriteriaSession(vSession, false);
            string strQuery = "";

            if (fromPageIndex > 1)
                fromPageIndex = fromPageIndex + 1;

            if (category == Types.Vendors.ToString())
            {
                strQuery = @";WITH MyDataSet
                                 AS
                                 (";

                strQuery += @"Select distinct(Elio_users.id)
                            , ROW_NUMBER() over (order by billing_type desc) as row_index
                            , isnull(round(avg(cast(rate as float)),2),'0') as r
                            , address
                            , city
                            , billing_type
                            , company_type
                            from Elio_users
                            left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id=Elio_users.id ";

                vSession.CategoryViewState = categoryId;

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                if (countryId != "0")
                {
                    vSession.CountryViewState = countryId;
                }

                strQuery += "where is_public=1 and company_type='" + category + "' and account_status=1 ";
                strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : "";
                strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : "";
                strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : "";
                strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : "";
                strQuery += (countryId != "0") ? "and country='" + country + "' " : "";
                strQuery += (city != "") ? "and city='" + city + "' " : "";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : "";
                strQuery += " GROUP BY Elio_users.id, billing_type, address, city, company_type";

                strQuery += @") ";

                //            strQuery += @",total_views
                //                            as
                //                            (";

                //            strQuery += @"Select company_id,sum(views) as totalViews 
                //	                      from Elio_companies_views nolock	
                //	                      GROUP BY company_id";

                //            strQuery += ")";


                strQuery += @" select id
                                ,row_index
                                ,r
                                ,address
                                ,city
                                ,billing_type
                                ,company_type
                                --,isnull(totalViews,0) as totalViews 
                            from MyDataSet
                            --left join total_views on total_views.company_id = MyDataSet.id
                            where row_index between " + fromPageIndex + " and " + toPageIndex + " " +
                                " ORDER BY billing_type DESC " +
                                " , r desc";
            }
            else
            {
                strQuery = @";WITH MyDataSet
                                 AS
                                 (";

                strQuery += @"Select distinct(Elio_users.id)
                            , ROW_NUMBER() over (order by billing_type desc) as row_index
                            --, isnull(round(avg(cast(rate as float)),2),'0') as r
                            , address
                            , city
                            , billing_type 
                            , company_type
                            , sysdate
                            from Elio_users
                            --left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id=Elio_users.id " + Environment.NewLine;

                vSession.CategoryViewState = categoryId;

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                if (countryId != "0")
                {
                    vSession.CountryViewState = countryId;
                }

                strQuery += "where is_public=1 and company_type='" + category + "' and account_status=1 ";
                strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : "";
                strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : "";
                strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : "";
                strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : "";
                strQuery += (countryId != "0") ? "and country='" + country + "' " : "";
                strQuery += (city != "") ? "and city='" + city + "' " : "";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : "";
                strQuery += " GROUP BY Elio_users.id, billing_type, address, city, company_type, sysdate";
                
                strQuery += @") ";

                //            strQuery += @",total_views
                //                            as
                //                            (";

                //            strQuery += @"Select company_id,sum(views) as totalViews 
                //	                      from Elio_companies_views nolock	
                //	                      GROUP BY company_id";

                //            strQuery += ")";


                strQuery += @" select id
                                ,row_index
                                --,r
                                ,address
                                ,city
                                ,billing_type
                                ,company_type
                                --,isnull(totalViews,0) as totalViews 
                            from MyDataSet
                            --left join total_views on total_views.company_id = MyDataSet.id
                            where row_index between " + fromPageIndex + " and " + toPageIndex + " " +
                                " ORDER BY sysdate DESC " +
                                " --, r desc";
            }

            return strQuery;
        }

        public static string GetSearchResults(string categoryId, string category, string industryId, string verticalId, string programId, string marketId, string apiId, string country, string countryId, string companyName, ElioSession vSession, DBSession session)
        {
            GlobalMethods.ClearCriteriaSession(vSession, false);

            string strQuery = @"Select distinct(Elio_users.id), isnull(round(avg(cast(rate as float)),2),'0') as r, billing_type from Elio_users
                                left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id=Elio_users.id ";

            vSession.CategoryViewState = categoryId;

            if (industryId != "0")
            {
                strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id ";
                vSession.IndustryViewState = industryId;
            }
            if (verticalId != "0")
            {
                strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                vSession.VerticalViewState = verticalId;
            }
            if (programId != "0")
            {
                strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id ";
                vSession.PartnerViewState = programId;
            }
            if (marketId != "0")
            {
                strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id ";
                vSession.MarketViewState = marketId;
            }

            if (apiId != "0")
            {
                strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id ";
                vSession.ApiViewState = apiId;
            }

            if (companyName != string.Empty)
            {
                vSession.CompanyNameViewState = companyName;
            }

            if (countryId != "0")
            {
                vSession.CountryViewState = countryId;
            }

            strQuery += "where is_public=1 and company_type='" + category + "' and account_status=1 ";
            strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
            strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : "";
            strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : "";
            strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : "";
            strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : "";
            strQuery += (countryId != "0") ? "and country='" + country + "' " : "";
            strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : "";
            strQuery += " GROUP BY Elio_users.id, billing_type ORDER BY billing_type DESC, r DESC";

            return strQuery;
        }

        public static string GetSearchResults_old(string categoryId, string category, string industryId, string verticalId, string programId, string marketId, string apiId, string country, string countryId, string companyName, ElioSession vSession, DBSession session)
        {
            GlobalMethods.ClearCriteriaSession(vSession, false);

            string strQuery = @"Select distinct(Elio_users.id), isnull(round(avg(cast(rate as float)),2),'0') as r, company_name, billing_type from Elio_users
                                left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id=Elio_users.id ";

            vSession.CategoryViewState = categoryId;

            if (industryId != "0")
            {
                strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id ";
                vSession.IndustryViewState = industryId;
            }
            if (verticalId != "0")
            {
                strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                vSession.VerticalViewState = verticalId;
            }
            if (programId != "0")
            {
                strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id ";
                vSession.PartnerViewState = programId;
            }
            if (marketId != "0")
            {
                strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id ";
                vSession.MarketViewState = marketId;
            }

            if (apiId != "0")
            {
                strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id ";
                vSession.ApiViewState = apiId;
            }

            if (companyName != string.Empty)
            {
                vSession.CompanyNameViewState = companyName;
            }

            if (countryId != "0")
            {
                vSession.CountryViewState = countryId;
            }

            strQuery += "where is_public=1 and company_type='" + category + "' and account_status=1 ";
            strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
            strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : "";
            strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : "";
            strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : "";
            strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : "";
            strQuery += (countryId != "0") ? "and country='" + country + "' " : "";
            strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : "";
            strQuery += " GROUP BY Elio_users.id, company_name, billing_type ORDER BY billing_type DESC, r desc";

            return strQuery;
        }

        public static string GetSEOSearchResults(string categoryId, string industryId, string verticalId, string programId, string marketId, string apiId, string subIndustryGroupId, string country, string countryId, string companyName, ElioSession vSession, DBSession session)
        {
            GlobalMethods.ClearCriteriaSession(vSession, false);

            string strQuery = @"Select distinct(Elio_users.id), round(avg(cast(rate as float)),2) as r, company_name, billing_type from Elio_users 
                                left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id=Elio_users.id ";

            vSession.CategoryViewState = categoryId;

            if (industryId != "0")
            {
                strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id ";
                vSession.IndustryViewState = industryId;
            }
            if (verticalId != "0" || subIndustryGroupId != "0")
            {
                strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                vSession.VerticalViewState = verticalId;
            }           
            if (programId != "0")
            {
                strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id ";
                vSession.PartnerViewState = programId;
            }
            if (marketId != "0")
            {
                strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id ";
                vSession.MarketViewState = marketId;
            }
            if (apiId != "0")
            {
                strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id ";
                vSession.ApiViewState = apiId;
            }

            if (companyName != string.Empty)
            {
                vSession.CompanyNameViewState = companyName;
            }

            if (countryId != "0")
            {
                vSession.CountryViewState = countryId;
            }

            strQuery += "where is_public=1 and account_status=1 ";
            strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : " ";
            strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_id=" + Convert.ToInt32(verticalId) + " " : " ";
            if (subIndustryGroupId != "0")
                strQuery += "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(subIndustryGroupId) + " ";

            strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : " ";
            strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : " ";
            strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : " ";
            strQuery += (countryId != "0") ? "and country='" + country + "' " : " ";
            strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : " ";
            strQuery += " GROUP BY Elio_users.id, company_name, billing_type ORDER BY billing_type DESC, r desc";

            return strQuery;
        }

        public static List<ElioUsers> GetSEOSearchResultsNew(string companyType, string industryId, string verticalId, string category, string programId, string marketId, string apiId, string region, string country, string state, string city, string companyName, string orderBy, ElioSession vSession, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            GlobalMethods.ClearCriteriaSession(vSession, false);

            if (companyType.ToLower().StartsWith("vendors"))
                companyType = Types.Vendors.ToString();
            else
                companyType = EnumHelper.GetDescription(Types.Resellers).ToString();

            string strQuery = "";

            if (companyType == Types.Vendors.ToString())
            {
                strQuery += @"Select distinct(u.id) as id,company_name, replace(u.company_logo, '~/', '/') as company_logo
                            , website
                            , country
                            , city
                            , billing_type
                            , company_type
                            , overview
                            , u.sysdate
                            from Elio_users u ";

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id = u.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on u.id=Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=u.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=u.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=u.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                strQuery += "where u.is_public=1 and company_type='" + companyType + "' and account_status=1 ";
                strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : "";
                strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : " ";
                strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : " ";
                strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : " ";
                strQuery += (city != "") ? "and city = '" + city + "' " : " ";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : " ";
                strQuery += " GROUP BY u.id, billing_type, website, city, company_type,company_logo,country,company_name,overview, u.sysdate";

                if (orderBy != "")
                    strQuery += " ORDER BY " + orderBy;
                else
                    strQuery += " ORDER BY billing_type desc";
            }
            else
            {
                strQuery += @"Select distinct(u.id)
                            ,u.company_name
                            ,replace(u.company_logo, '~/', '/') as company_logo
                            ,u.user_application_type
                            --,u.company_region
                            ,u.country
                            --,u.address
                            ,case when u.city != '' then city else '-' end as  city
                            ,u.webSite
                            ,case when u.billing_type > 1 then 'true' else 'false' end as billing_type
                            ,u.company_type
                            ,case when u.overview !='' then 
								case 
									when LEN(u.overview) > 1000 then SUBSTRING(u.overview, 0, 1000) + ' ...'
								else 
									overview 
								end 
							else
								''
								end
							as overview
                            ,'/profiles/channel-partners/' + CAST(u.id as nvarchar(50)) + '/' + LOWER(REPLACE(TRIM(u.company_name), ' ', '-')) as profiles
                            --, u.sysdate
                            from Elio_users u " + Environment.NewLine;

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id = u.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on u.id = Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                else
                {
                    if (category != "")
                        strQuery += @"inner join Elio_users_registration_products urp
                                        on urp.user_id = u.id
                                    inner join Elio_registration_products rp
                                        on rp.id = urp.reg_products_id " + Environment.NewLine;
                }

                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id = u.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id = u.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id = u.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                strQuery += "where u.is_public=1 and company_type='" + companyType + "' and account_status=1 ";
                strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : (category != "") ? " and rp.description = '" + category + "' " : " ";
                strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : " ";
                strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : " ";
                strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : " ";
                strQuery += (region != "") ? "and company_region = '" + region + "' " : " ";
                strQuery += (country != "") ? "and country='" + country + "' " : " ";
                strQuery += (state != "") ? "and state = '" + state + "' " : " ";
                strQuery += (city != "") ? "and city = '" + city + "' " : " ";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : " ";
                strQuery += " GROUP BY u.id, u.company_name, u.company_logo, u.user_application_type, u.billing_type, u.country, u.city, u.webSite, u.company_type, u.overview";

                if (orderBy != "")
                    strQuery += " ORDER BY " + orderBy;
                else
                    strQuery += " ORDER BY billing_type desc, u.id desc";
            }

            return loader.Load(strQuery);
        }

        public static List<ElioUsersSearchInfo> GetSEOSearchResultsNewCP(string companyType, string industryId, string verticalId, string category, string programId, string marketId, string apiId, string region, string country, string state, string city, string companyName, string orderBy, ElioSession vSession, DBSession session)
        {
            DataLoader<ElioUsersSearchInfo> loader = new DataLoader<ElioUsersSearchInfo>(session);

            //GlobalMethods.ClearCriteriaSession(vSession, false);

            //if (fromPageIndex > 1)
            //    fromPageIndex = fromPageIndex + 1;

            if (companyType.ToLower().StartsWith("vendors"))
                companyType = Types.Vendors.ToString();
            else
                companyType = EnumHelper.GetDescription(Types.Resellers).ToString();

            string strQuery = "";

            if (companyType == Types.Vendors.ToString())
            {
                strQuery += @"Select distinct(u.id) as id,company_name
                            , case when isnull(u.company_logo, '') != '' then replace(u.company_logo, '~/', '/') 
                              else '/images/no_logo_company.png' end as company_logo
                            , country
                            , billing_type
                            , company_type
                            , company_region
                            , city
                            , case when u.overview !='' then 
                              case 
                              when LEN(u.overview) > 1000 then SUBSTRING(u.overview, 0, 1000) + ' ...'
                              else 
                              overview 
                              end 
                              else
                              ''
                              end
                              as overview
                            ,'/profiles/vendors/' + CAST(u.id as nvarchar(50)) + '/' + LOWER(REPLACE(TRIM(u.company_name), ' ', '-')) as profiles
                            ,'/' + LOWER(REPLACE(TRIM(u.company_name), ' ', '-')) + '/partner-free-sign-up' as partner_portal                            
                            , u.sysdate
                            from Elio_users u ";

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id = u.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on u.id=Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=u.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=u.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=u.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                strQuery += "where u.is_public=1 and company_type='" + companyType + "' and account_status=1 ";
                strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : "";
                strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : " ";
                strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : " ";
                strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : " ";
                strQuery += (city != "") ? "and city = '" + city + "' " : " ";
                strQuery += (country != "") ? "and country = '" + country + "' " : " ";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : " ";
                strQuery += " GROUP BY u.id, billing_type, company_type,company_logo,country,company_name,overview,company_region,city,u.sysdate";

                if (orderBy != "")
                    strQuery += " ORDER BY " + orderBy;
                else
                    strQuery += " ORDER BY billing_type desc, id desc";
            }
            else
            {
                //strQuery = @";WITH MyDataSet
                //                 AS
                //                 ( " + Environment.NewLine;

                strQuery += @"Select distinct(u.id)
                            --,ROW_NUMBER() over (order by billing_type desc) as row_index
                            ,u.company_name
                            ,case when u.company_logo != '' then u.company_logo 
                              else '/images/no_logo_company.png' end as company_logo
                            ,u.user_application_type
                            ,u.company_region
                            ,u.country
                            ,case when u.city != '' then city else '-' end as  city
                            ,u.webSite
                            ,u.billing_type
                            ,u.company_type
                            ,case when u.overview !='' then 
								case 
									when LEN(u.overview) > 1000 then SUBSTRING(u.overview, 0, 1000) + ' ...'
								else 
									overview 
								end 
							else
								''
								end
							as overview
                            ,'/profiles/channel-partners/' + CAST(u.id as nvarchar(50)) + '/' + LOWER(REPLACE(TRIM(u.company_name), ' ', '-')) as profiles
                            , u.sysdate
                            from Elio_users u " + Environment.NewLine;

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id = u.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on u.id = Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                else
                {
                    if (category != "")
                        strQuery += @"inner join Elio_users_registration_products urp
                                        on urp.user_id = u.id
                                    inner join Elio_registration_products rp
                                        on rp.id = urp.reg_products_id " + Environment.NewLine;
                }

                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id = u.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id = u.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id = u.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                strQuery += "where u.is_public = 1 and company_type = '" + companyType + "' and account_status = 1 ";
                strQuery += (industryId != "0") ? "and industry_id = " + Convert.ToInt32(industryId) + " " : " ";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id = " + Convert.ToInt32(verticalId) + " " : (category != "") ? " and rp.description = '" + category + "' " : " ";
                strQuery += (programId != "0") ? "and partner_id = " + Convert.ToInt32(programId) + " " : " ";
                strQuery += (marketId != "0") ? "and market_id = " + Convert.ToInt32(marketId) + " " : " ";
                strQuery += (apiId != "0") ? "and api_id = " + Convert.ToInt32(apiId) + " " : " ";
                strQuery += (region != "") ? "and (u.company_region != '' and u.company_region = '" + region + "') " : " ";
                strQuery += (country != "") ? "and (u.country != '' and u.country ='" + country + "') " : " ";
                strQuery += (state != "") ? "and (u.state != '' and u.state = '" + state + "') " : " ";
                strQuery += (city != "") ? "and (u.city != '' and u.city = '" + city + "') " : " ";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : " ";
                strQuery += " GROUP BY u.id, u.company_name, u.company_logo, u.user_application_type, u.billing_type, u.company_region, u.country, u.city, u.webSite, u.company_type, u.overview, u.sysdate";

                //strQuery += @") ";

                //strQuery += @" select *
                //            from MyDataSet
                //            where row_index between " + fromPageIndex + " and " + toPageIndex + " ";

                if (orderBy != "")
                    strQuery += " ORDER BY " + orderBy;
                else
                    strQuery += " ORDER BY billing_type desc, id desc";
            }

            return loader.Load(strQuery);
        }

        public static string GetSEOSearchResults(int fromPageIndex, int toPageIndex, string companyType, string industryId, string verticalId, string category, string programId, string marketId, string apiId, string region, string country, string state, string city, string companyName, string orderBy, ElioSession vSession, DBSession session)
        {
            GlobalMethods.ClearCriteriaSession(vSession, false);

            if (fromPageIndex > 1)
                fromPageIndex = fromPageIndex + 1;

            if (companyType.ToLower().StartsWith("vendors"))
                companyType = Types.Vendors.ToString();
            else
                companyType = EnumHelper.GetDescription(Types.Resellers).ToString();

            string strQuery = "";

            if (companyType == Types.Vendors.ToString())
            {
                strQuery = @";WITH MyDataSet
                                 AS
                                 (";

                strQuery += @"Select distinct(u.id)
                            , ROW_NUMBER() over (order by billing_type desc) as row_index
                            --, isnull(round(avg(cast(rate as float)),2),'0') as r
                            , company_name
                            , address
                            , city
                            , billing_type 
                            , company_type
                            , u.sysdate
                            from Elio_users u ";
                            //--left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id = u.id
                            //inner join Elio_users_registration_products urp
                            //on urp.user_id = u.id
                            //inner join Elio_registration_products rp
                            //on rp.id = urp.reg_products_id ";

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id = u.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on u.id=Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=u.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=u.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=u.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                strQuery += "where u.is_public=1 and company_type='" + companyType + "' and account_status=1 ";
                strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : "";
                strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : " ";
                strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : " ";
                strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : " ";
                strQuery += (city != "") ? "and city = '" + city + "' " : " ";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : " ";
                strQuery += " GROUP BY u.id, company_name, billing_type, address, city, company_type, u.sysdate";

                strQuery += @") ";

                //            strQuery += @",total_views
                //                            as
                //                            (";

                //            strQuery += @"Select company_id,sum(views) as totalViews 
                //	                      from Elio_companies_views nolock	
                //	                      GROUP BY company_id";

                //            strQuery += ")";


                strQuery += @" select id
                                ,row_index
                                --,r
                                ,address
                                ,city
                                ,billing_type
                                ,company_type
                                --,isnull(totalViews,0) as totalViews 
                            from MyDataSet
                            --left join total_views on total_views.company_id = MyDataSet.id
                            where row_index between " + fromPageIndex + " and " + toPageIndex + " ";

                if (orderBy != "")
                    strQuery += " ORDER BY " + orderBy;
            }
            else
            {
                strQuery = @";WITH MyDataSet
                                 AS
                                 (";

                strQuery += @"Select distinct(u.id)
                            , ROW_NUMBER() over (order by billing_type desc, user_application_type asc) as row_index
                            , company_name
                            , company_logo
                            , user_application_type
                            , address
                            , company_region
                            , country
                            , city
                            , webSite
                            , billing_type 
                            , company_type
                            , overview
                            , u.sysdate
                            from Elio_users u " + Environment.NewLine;

                if (industryId != "0")
                {
                    strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id = u.id ";
                    vSession.IndustryViewState = industryId;
                }
                if (verticalId != "0")
                {
                    strQuery += "inner join Elio_users_sub_industries_group_items on u.id = Elio_users_sub_industries_group_items.user_id ";
                    vSession.VerticalViewState = verticalId;
                }
                else
                {
                    if (category != "")
                        strQuery += @"inner join Elio_users_registration_products urp
                                        on urp.user_id = u.id
                                    inner join Elio_registration_products rp
                                        on rp.id = urp.reg_products_id " + Environment.NewLine;
                }

                if (programId != "0")
                {
                    strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id = u.id ";
                    vSession.PartnerViewState = programId;
                }
                if (marketId != "0")
                {
                    strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id = u.id ";
                    vSession.MarketViewState = marketId;
                }

                if (apiId != "0")
                {
                    strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id = u.id ";
                    vSession.ApiViewState = apiId;
                }

                if (companyName != string.Empty)
                {
                    vSession.CompanyNameViewState = companyName;
                }

                //if (country != "")
                //{
                //    strQuery += "inner join Elio_countries on Elio_countries.country_name = u.country ";
                //}

                strQuery += "where u.is_public=1 and company_type='" + companyType + "' and account_status=1 ";
                strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : "";
                strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : (category != "") ? " and rp.description = '" + category + "' " : " ";
                strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : " ";
                strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : " ";
                strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : " ";
                strQuery += (region != "") ? "and company_region = '" + region + "' " : " ";
                strQuery += (country != "") ? "and country='" + country + "' " : " ";
                strQuery += (state != "") ? "and state = '" + state + "' " : " ";
                strQuery += (city != "") ? "and city = '" + city + "' " : " ";
                strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : " ";
                strQuery += " GROUP BY u.id, company_name, company_logo, user_application_type, billing_type, company_region, country, address, city, webSite, company_type, overview, u.description, u.sysdate";

                strQuery += @") ";

                strQuery += @" select id
                            ,row_index
                            ,company_name
                            ,company_logo
                            ,user_application_type
                            ,company_region
                            ,country
                            ,address
                            ,case when city != '' then city else '-' end as  city
                            ,webSite
                            ,billing_type
                            ,company_type
                            ,case when overview !='' then 
								case 
									when LEN(overview) > 250 then SUBSTRING(overview, 1, 250) 
								else 
									overview 
								end 
							else
								''
								end
							as overview
                            ,'/profiles/channel-partners/' + CAST(id as nvarchar(50)) + '/' + LOWER(REPLACE(TRIM(company_name), ' ', '-')) + '/' as profiles
                            from MyDataSet
                            --left join total_views on total_views.company_id = MyDataSet.id
                            where row_index between " + fromPageIndex + " and " + toPageIndex + " ";

                if (orderBy != "")
                    strQuery += " ORDER BY " + orderBy;
                //else
                //    strQuery += " ORDER BY sysdate DESC ";
            }

            return strQuery;
        }

        public static string GetSEOSearchResultsByTechnologies(int fromPageIndex, int toPageIndex, string companyType, string technolgyDescription, string categoryId, string industryId, string verticalId, string programId, string marketId, string apiId, string subIndustryGroupId, string country, string countryId, string city, string companyName, out int resultsCount, out string disclaimerText, ElioSession vSession, DBSession session)
        {
            resultsCount = 0;
            vSession.TechnologyCategory = "";
            disclaimerText = "";

            GlobalMethods.ClearCriteriaSession(vSession, false);

            if (fromPageIndex > 1)
                fromPageIndex = fromPageIndex + 1;

            if (companyType.ToLower().StartsWith("vendors"))
                companyType = Types.Vendors.ToString();
            else
                companyType = EnumHelper.GetDescription(Types.Resellers).ToString();

            #region fix technology name

            if (technolgyDescription.StartsWith("'"))
                technolgyDescription = technolgyDescription.Substring(1, technolgyDescription.Length);
            if (technolgyDescription.EndsWith("'"))
                technolgyDescription = technolgyDescription.Substring(0, technolgyDescription.Length - 1);

            technolgyDescription = technolgyDescription.Replace("_", " ").Replace("'", "-");

            technolgyDescription = GlobalMethods.CapitalizeFirstLetter(technolgyDescription);

            #endregion

            string strQuery = @";WITH MyDataSet
                                 AS
                                 (";

            strQuery += @"Select ROW_NUMBER() over (order by billing_type desc) as row_index
                        , Elio_users.id
                        --, round(avg(cast(rate as float)),2) as r
                        , company_name
                        , address
                        ,city
                        , billing_type 
                        , company_type
                        , Elio_users.sysdate
                        from Elio_users 
                        --left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id=Elio_users.id 
                        inner join Elio_users_registration_products urp
                            on urp.user_id = Elio_users.id
                        inner join Elio_registration_products rp
                            on rp.id = urp.reg_products_id " + Environment.NewLine;

            vSession.CategoryViewState = categoryId;

            if (industryId != "0")
            {
                strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id ";
                vSession.IndustryViewState = industryId;
            }
            if (verticalId != "0" || subIndustryGroupId != "0")
            {
                strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                vSession.VerticalViewState = verticalId;
            }
            if (programId != "0")
            {
                strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id ";
                vSession.PartnerViewState = programId;
            }
            if (marketId != "0")
            {
                strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id ";
                vSession.MarketViewState = marketId;
            }
            if (apiId != "0")
            {
                strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id ";
                vSession.ApiViewState = apiId;
            }

            if (companyName != string.Empty)
            {
                vSession.CompanyNameViewState = companyName;
            }

            if (country != "")
            {
                strQuery += "inner join Elio_countries on Elio_countries.country_name = Elio_users.country ";

                if (countryId != "0")
                    vSession.CountryViewState = countryId;
            }

            strQuery += "where 1 = 1 and Elio_users.is_public = 1 ";     //Elio_users.is_public=1 and account_status=1 ";
            strQuery += "and company_type like '" + companyType + "' ";
            strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : " ";
            strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : " ";
            if (subIndustryGroupId != "0")
                strQuery += "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(subIndustryGroupId) + " ";

            strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : " ";
            strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : " ";
            strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : " ";
            strQuery += (country != "") ? "and country='" + country + "' " : " ";
            strQuery += (city != "") ? "and city='" + city + "' " : " ";
            strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : " ";
            strQuery += (technolgyDescription != "") ? " and rp.description = '" + technolgyDescription + "'" : "";

            #region to delete
            /*
            strQuery += "and Elio_users.id in (";

            switch (technolgyDescription)
            {
                #region technologies

                case "abbyy":

                    strQuery += @"31605,5289,5413,5456,5746,6064,6772,6859,11409,7491,7935,8017,10003,10336,13223,13224,13225,13226,13227,13279,13303,13304,
                                    13339,13364,13365,13367,13393,13509,13739,14285,14374,14844,15246,17942,18518,18901,5064,19644,11647,20271,25915,9946,
                                    9987,13274,29245,16321,9184,15071,21251";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official ABBYY resellers list nor is affiliated with or in any way associated with ABBYY.";
                    break;

                case "accusoft":

                    strQuery += @"16839,2514,10982";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 3;
                    disclaimerText = "This list is not an official Accusoft resellers list nor is affiliated with or in any way associated with Accusoft Corporation.";
                    break;

                case "adobe":

                    strQuery += @"2504,2697,2873,3164,3222,3264,3412,8533,9842,10655,10713,5324,11165,11709,12063,12120,13472,13634,13670,13762,13907,3601,
                                    14059,2791,6611,22036,22673,4548,16044,31605,31609,6801,19493,24847,3087,4321,4941,5023,5065,5107,6545,8489,8643,17139,
                                    21180,21732";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 46;
                    disclaimerText = "This list is not an official Adobe resellers list nor is affiliated with or in any way associated with Adobe.";
                    break;

                case "alfresco":

                    strQuery += @"2873,3572,4035,5605,6402,13412,13845,14450,32781,9366,4391,5223,5292,5663,5916,7933,8107,8868,8958,10294,10333,10380,
                                    10491,10584,10999,11790,11962,12101,13364,17941,19384,19643,19722,15457,20024,20459,6556,13697,21127,21129,21184,
                                    11192,21465,15396,21570,15524,21733,21734";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 48;
                    disclaimerText = "This list is not an official Alfresco resellers list nor is affiliated with or in any way associated with Alfresco Software, Inc.";
                    break;

                case "docscorp":

                    strQuery += @"4432,6100,6102,6103,6134,6352,6456,6457,7331,9145,9378,9379,10782,15075,15246,15248,15330,15401,15889,13271,24792,26330,
                                    7353,6454";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 24;
                    disclaimerText = "This list is not an official DocsCorp resellers list nor is affiliated with or in any way associated with DocsCorp.";
                    break;

                case "docusign":

                    strQuery += @"3515,3755,2625,2641,2658,4736,24450,26971,5017,29705,6438,5827,4348,4533,5051,5412,5916,6456,7331,8107,8108,8110,8146,
                                    8148,8236,8283,8284,8446,8497,8498,8545,8546,8547,8708,9144,10185,15246,18903,4895,19643,19645,20271,12180,22046,8963,
                                    4501,8640,8693,8694,24829";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official DocuSign resellers list nor is affiliated with or in any way associated with DocuSign.";
                    break;

                case "docuware":

                    strQuery += @"14242,4389,4879,5497,6770,7718,8063,8599,8600,8601,9111,9292,9293,9743,13224,13226,13480,13481,13508,13509,13510,13539,
                                    13540,13541,13542,13580,13612,13644,13645,13647,13677,13679,13700,13701,13702,13737,13738,13739,13740,13767,13768,13792,
                                    13817,13878,13879,13880,13917,13952,13954,13987";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official DocuWare resellers list nor is affiliated with or in any way associated with DocuWare Corporation.";
                    break;

                case "ephesoft":

                    strQuery += @"2596,4035,5605,4548,4535,5663,5916,8868,11875,15457,20024,21184,21570,15524,21784,22349,15241,8267,5057,21114,23122,16228";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 22;
                    disclaimerText = "This list is not an official Ephesoft resellers list nor is affiliated with or in any way associated with Ephesoft Inc.";
                    break;

                case "hotdocs":

                    strQuery += @"6102,7331,9337,17142,17234,17235,12180,14934";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 8;
                    disclaimerText = "This list is not an official HotDocs resellers list nor is affiliated with or in any way associated with AbacusNext International Ltd.";
                    break;

                case "hubdoc":

                    strQuery += @"33058,33245,33430,33499,33810,33892,34354,34518,34609,35429,35602,35673,37168,37489,37897,38257,38524";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 17;
                    disclaimerText = "This list is not an official Hubdoc resellers list nor is affiliated with or in any way associated with Hubdoc Inc.";
                    break;

                case "imanage":

                    strQuery += @"7216,22442,38083,38158,6102,6103,6350,6456,6457,6458,6571,7213,7331,7332,9199,9246,9247,9294,9551,9552,6145,9913,10185,10782,
                                    15074,15075,15127,15402,6110,12180,24792,26330,34857,20649,7495,2673,6342,21054";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 38;
                    disclaimerText = "This list is not an official iManage resellers list nor is affiliated with or in any way associated with iManage.";
                    break;

                case "kofax":

                    strQuery += @"2421,7103,13412,4548,16044,9380,2625,15456,5138,5289,5292,5548,5744,5851,5916,5964,6458,6772,6773,7658,7935,8017,8236,5038,
                                    8707,9112,9648,10003,10294,10678,10831,11090,11334,11759,11790,12160,13225,13700,13738,13739,13917,15246,10773,19573,10536,
                                    19644,20367";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official Kofax resellers list nor is affiliated with or in any way associated with Kofax Inc.";
                    break;

                case "m-files":

                    strQuery += @"19496,7333,4473,5055,8865,10294,13227,13304,13701,17413,17414,8733,17415,17762,17764,17765,17871,17941,17942,17943,18036,
                                    18037,18038,18039,18042,18045,18047,18515,18516,18517,18518,18620,15322,21887,22144,34766,19202,19080,16144,27771,18129,
                                    16523,32805";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 43;
                    disclaimerText = "This list is not an official M-Files resellers list nor is affiliated with or in any way associated with M-Files Inc.";
                    break;

                case "netdocuments":

                    strQuery += @"38158,6100,6103,7216,9145,9199,9294,9337,9379,9552,9913,12100,15248,15330,15889,15890,17235,12180,8118";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 19;
                    disclaimerText = "This list is not an official NetDocuments resellers list nor is affiliated with or in any way associated with NetDocuments Software, Inc.";
                    break;

                case "opentext":

                    strQuery += @"2873,6900,7925,9272,9894,4448,11385,11638,12177,25608,13298,9380,33193,7333,16713,5180,5967,6103,6134,6243,6244,6517,6631,
                                    6632,8345,8769,8867,8957,8958,9112,9147,9199,9294,9378,9551,9648,9913,9914,10000,10002,10046,10047,10048,10090,10091,
                                    10133,10135,10185,10186,10336";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official OpenText resellers list nor is affiliated with or in any way associated with Open Text Corporation.";
                    break;

                case "pandadoc":

                    strQuery += @"26611,15188,13502,10444,13657,36862,7223";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 7;
                    disclaimerText = "This list is not an official PandaDoc resellers list nor is affiliated with or in any way associated with PandaDoc Inc.";
                    break;

                case "ricoh":

                    strQuery += @"5496,6770,10675,14543,16587,32584,7454,4980";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 8;
                    disclaimerText = "This list is not an official Ricoh resellers list nor is affiliated with or in any way associated with Ricoh.";
                    break;

                case "worldox":

                    strQuery += @"4433,4434,6100,6101";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 4;
                    disclaimerText = "This list is not an official Worldox resellers list nor is affiliated with or in any way associated with Worldox.";
                    break;

                case "xerox":

                    strQuery += @"2731,5148,30328,4477,4760,5703,5704,5744,5745,5746,5803,5804,5851,5914,5966,6064,6065,6572,6690,8547,9292,10609,11790,5518,
                                    12188,13581,13740,13915,15531,8858,8633,32584,38382,4099,6043,5522,17013,33402,8819";

                    vSession.TechnologyCategory = "Document Management";
                    resultsCount = 39;
                    disclaimerText = "This list is not an official Xerox resellers list nor is affiliated with or in any way associated with Xerox Corporation.";
                    break;

                case "avalara":

                    strQuery += @"4825,18686,20415,20152,20231,22424,8833,16521,3295,16481,18827,7252,7409,33058,38257,23353,18826,19227,19578,20461,21830,7516";

                    vSession.TechnologyCategory = "Accounting";
                    resultsCount = 23;
                    disclaimerText = "This list is not an official Avalara resellers list nor is affiliated with or in any way associated with Avalara Inc.";
                    break;

                case "blackline":

                    strQuery += @"3666,10586,11932,15723,8186,6159,6819,23063,30057,13453,15896,14334,6882,13486,15857,20219,35392,8271";

                    vSession.TechnologyCategory = "Accounting";
                    resultsCount = 18;
                    disclaimerText = "This list is not an official BlackLine resellers list nor is affiliated with or in any way associated with BlackLine Inc.";
                    break;

                case "quickbooks":

                    strQuery += @"8488,10885,22802,10845,26615,28534,24403,29975,32116,32583,33447,34391,36701,37372,37694,37950,13817,15850,31871,10384,8632,
                                    23648,24342,25106,13827,8214,16866,21307,22253,22424,22425,22426,22427,7004,23034,23036,3397,23033,34697,34700,34806,34892,
                                    34950,34953,35049,35050,35053,35159,35428,35523";

                    vSession.TechnologyCategory = "Accounting";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official QuickBooks resellers list nor is affiliated with or in any way associated with Intuit Limited.";
                    break;

                case "xero":

                    strQuery += @"2707,3832,6731,9028,9238,11168,10885,16893,17040,17139,17140,25904,26034,27802,27893,21089,28776,15815,24403,17693,19084,
                                    23648,23948,19568,19620,13827,17545,33174,33176,33177,33178,33179,33242,33243,33244,33245,33246,33337,33338,33339,33340,
                                    33499,33501,33598,33599,33600,33601,33602,33603,33729";

                    vSession.TechnologyCategory = "Accounting";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Xero resellers list nor is affiliated with or in any way associated with Xero Limited.";
                    break;

                case "intuit":

                    strQuery += @"16189,16893,10609,9463,3906,6946,3397,6219,13733,7566,7567,7625,7679,7006,32829,33497,33598,35774,36075,36671,37045,5278";

                    vSession.TechnologyCategory = "Accounting";
                    resultsCount = 22;
                    disclaimerText = "This list is not an official Intuit resellers list nor is affiliated with or in any way associated with Intuit Inc.";
                    break;

                case "aerohive":

                    strQuery += @"2275,8049,9636,10771,10899,11638,11709,11779,11838,11862,12150,13206,13207,5131,13352,13472,13812,20740,5784,21880,14947,28822,
                                    28963,20665,32007,36550,37178,7773,30050,9744,11001,13737,16666,19595,31721,37227,7341,7769,6153,8019,8435,23467,23823,26163,
                                    16649,5613,19179,24324,35393";

                    vSession.TechnologyCategory = "Hardware";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official Aerohive resellers list nor is affiliated with or in any way associated with Extreme Networks.";
                    break;

                case "ciena":

                    strQuery += @"2750,9532,10274,11455,31033,16984,31746";

                    vSession.TechnologyCategory = "Hardware";
                    resultsCount = 7;
                    disclaimerText = "This list is not an official Ciena resellers list nor is affiliated with or in any way associated with Ciena Corporation.";
                    break;

                case "cisco":

                    strQuery += @"2246,2750,2873,2937,2939,3011,3061,3092,3164,3222,3387,3594,3754,3788,3854,3956,4686,5607,5647,5902,6203,6753,7017,7018,7019,7104,
                                    7753,8008,8009,8142,8178,8179,8224,8263,8322,8471,8472,8532,8636,8800,8935,8936,8981,8982,9082,9085,9086,9126,9129,9130";

                    vSession.TechnologyCategory = "Hardware";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Cisco resellers list nor is affiliated with or in any way associated with Cisco.";
                    break;

                case "d-link":

                    strQuery += @"11709,3456,16969,5141,32808,32881,31743,19559,10655,30298,30304,30690,31140,4272";

                    vSession.TechnologyCategory = "Hardware";
                    resultsCount = 14;
                    disclaimerText = "This list is not an official D-Link resellers list nor is affiliated with or in any way associated with D-Link.";
                    break;

                case "extreme-networks":

                    strQuery += @"2329,6552,9983,13412,21519,11464,38653,2469,9347,9393,3394,5190,8324,11455,29743,11813,12150,13247,13351,13414,13499,13635,
                                    13871,28770,14450,36553,19619,5925,25844,14071";

                    vSession.TechnologyCategory = "Hardware";
                    resultsCount = 30;
                    disclaimerText = "This list is not an official Extreme Networks resellers list nor is affiliated with or in any way associated with Extreme Networks.";
                    break;

                case "meraki":

                    strQuery += @"2328,13331,13413,5754,18867,19864,19869,19977,20174,20310,20315,35455,3305,6658,8339,8755,8944,15802,16187,18681,11259,3335,30051,
                                    30988,31959,34545,36838,18394,37627,34998,7545,4851";

                    vSession.TechnologyCategory = "Hardware";
                    resultsCount = 32;
                    disclaimerText = "This list is not an official Meraki resellers list nor is affiliated with or in any way associated with Cisco.";
                    break;

                case "ruckus-networks":

                    strQuery += @"6754,8009,8142,10233,11132,11421,11455,13389,13497,13786,29302,6044,19975,20403,20503,35970,36625,6197,7941,27143,11181,6445,
                                    34987,13737,15942,23717,15429,5445,6190,24879,2673,4099,5637,29168,17325,17326,19348,29167,32234,16738,27278,35394,24877,17985,13714";

                    vSession.TechnologyCategory = "Hardware";
                    resultsCount = 45;
                    disclaimerText = "This list is not an official Ruckus Networks resellers list nor is affiliated with or in any way associated with CommScope.";
                    break;

                case "tintri":

                    strQuery += @"2287,2448,5124,7205,8009,8934,9178,9356,13632,13948,33545,37367,25769,16476,19758,8302,5577,8711,6913";

                    vSession.TechnologyCategory = "Hardware";
                    resultsCount = 19;
                    disclaimerText = "This list is not an official Tintri resellers list nor is affiliated with or in any way associated with DDN.";
                    break;

                case "yealink":

                    strQuery += @"18799,21514,8404,35496,30289,21631,22591,23660";

                    vSession.TechnologyCategory = "Hardware";
                    resultsCount = 8;
                    disclaimerText = "This list is not an official Yealink resellers list nor is affiliated with or in any way associated with Yealink Inc.";
                    break;

                case "appian":

                    strQuery += @"2873,7103,6828,8912,6817,6906,19594,6936,6830,26539,12156,7244,8385,30972,34556,6937,27098,6831,19795,20894,21361,22315,7242,
                                    7354,7400,7401,7243,6829";

                    vSession.TechnologyCategory = "Business Process Management";
                    resultsCount = 28;
                    disclaimerText = "This list is not an official Appian resellers list nor is affiliated with or in any way associated with Appian.";
                    break;

                case "bonitasoft":

                    strQuery += @"3431,11519,11401,10536,27145,5710,11319,4701,21120,11279,19389,13421,14949,28611,11280,21675,6789";

                    vSession.TechnologyCategory = "Business Process Management";
                    resultsCount = 17;
                    disclaimerText = "This list is not an official Bonitasoft resellers list nor is affiliated with or in any way associated with Bonitasoft, S.A.";
                    break;

                case "onbase":

                    strQuery += @"3853,25608,5138,5292,6244,7332,10610,11497,13305,13738,13954,14636,11194,22686,23901,24125,26696,13274,29043,29245,7187,37480,18687,
                                    30800,9235,14192,19080,2556,3296,19799,19896,13392,21944,22418";

                    vSession.TechnologyCategory = "Business Process Management";
                    resultsCount = 34;
                    disclaimerText = "This list is not an official Onbase resellers list nor is affiliated with or in any way associated with Hyland Software, Inc.";
                    break;

                case "software-ag":

                    strQuery += @"2560,2771,3292,4368,32131,32133,32136,32224,32226,32227,11714,20896,34659,6181,31810,20706,11474,28393,11222,8976,2528,14838,14969,
                                    23120,23358,17268,14590";

                    vSession.TechnologyCategory = "Business Process Management";
                    resultsCount = 27;
                    disclaimerText = "This list is not an official Software AG resellers list nor is affiliated with or in any way associated with Software AG.";
                    break;

                case "apptus":

                    strQuery += @"2641,4501,8640,28458,11330,19347,2620";

                    vSession.TechnologyCategory = "eCommerce";
                    resultsCount = 7;
                    disclaimerText = "This list is not an official Apptus resellers list nor is affiliated with or in any way associated with Apptus.";
                    break;

                case "magento":

                    strQuery += @"2685,3453,4033,4035,4266,20752,29696,38154,3330,21290,24551,3618,35100,23721,8569,20151,23034,13592,8876,32978,35158,35341,35507,
                                    35595,37152,37155,37156,37157,16481,37237,37241,37243,37576,37650,37651,37726,37810,38114,38115,38189,38396,38397,38403,38733,38786,
                                    38790,38794";

                    vSession.TechnologyCategory = "eCommerce";
                    resultsCount = 47;
                    disclaimerText = "This list is not an official Magento resellers list nor is affiliated with or in any way associated with Adobe.";
                    break;

                case "shopify":

                    strQuery += @"2695,6497,23532,25616,26319,26611,27055,27056,27802,21089,29979,33938,36915,4020,25972,3318,3034,36801,37155,37157,37235,37237,37241,
                                    37575,37579,37580,37643,37651,37652,37726,37727,38111,38114,38185,38186,38189,38247,38396,38397,38505,38594,38596,38661,38733,38786,
                                    38787,38790,38794,38880";

                    vSession.TechnologyCategory = "eCommerce";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official Shopify resellers list nor is affiliated with or in any way associated with Shopify.";
                    break;

                case "bigcommerce":

                    strQuery += @"8827,3099,3034,36965,37135,37151,37152,37155,37156,37157,37235,37236,37241,37243,37571,37575,37576,37579,37580,37648,37649,37650,37651,
                                    37652,37726,37727,37728,37731,37733,37806,37809,37810,37815,38113,38114,38115,38116,38118,38185,38186,38189,38247,38321,38396,38505,
                                    38508,38588,38596";

                    vSession.TechnologyCategory = "eCommerce";
                    resultsCount = 48;
                    disclaimerText = "This list is not an official BigCommerce resellers list nor is affiliated with or in any way associated with BigCommerce.";
                    break;

                case "servicem8":

                    strQuery += @"21089,24403,19620,17544,20831,20832,20889,20891,20892,21085,21086,21087,21088,21090,21091,21092,21094,21253,21348,21432,21433,
                                    21434,21436,21437,21544,21645,21647,21854,21856,20941";

                    vSession.TechnologyCategory = "eCommerce";
                    resultsCount = 30;
                    disclaimerText = "This list is not an official ServiceM8 resellers list nor is affiliated with or in any way associated with ServiceM8.";
                    break;

                case "autodesk":

                    strQuery += @"10713,13670,3601,9677,6124,30987,33109,5412,6688,8147,11761,14154,25016,38989,35989,19202,23425,23505,23506,23508,28706,8251,37978,
                                    33899,33605,5395,24651,25551,35222,14478,14163,27412,28029,36985,37051";

                    vSession.TechnologyCategory = "CAD";
                    resultsCount = 35;
                    disclaimerText = "This list is not an official Autodesk resellers list nor is affiliated with or in any way associated with Autodesk Inc.";
                    break;

                case "solidworks":

                    strQuery += @"25075,14154,27965,27933";

                    vSession.TechnologyCategory = "CAD";
                    resultsCount = 4;
                    disclaimerText = "This list is not an official Solidworks resellers list nor is affiliated with or in any way associated with Dassault Systèmes.";
                    break;

                case "constant-contact":

                    strQuery += @"7044,7130,26690,3906,19849,21036,14769,22170,4775,16445,4261,13589,4108,4346,4602,5248,8765";

                    vSession.TechnologyCategory = "Email Marketing";
                    resultsCount = 17;
                    disclaimerText = "This list is not an official Constant Contact resellers list nor is affiliated with or in any way associated with Constant Contact.";
                    break;

                case "mailchimp":

                    strQuery += @"3151,14304,16133,24450,25697,11304,27683,34905,3778,14205,14769,3099,38118,26150,22078,38347,20793,22107,16445,28008,11940,4224,19339,
                                    5711,20688,4602,4178,3769";

                    vSession.TechnologyCategory = "Email Marketing";
                    resultsCount = 28;
                    disclaimerText = "This list is not an official Mailchimp resellers list nor is affiliated with or in any way associated with Mailchimp.";
                    break;

                case "aws":

                    strQuery += @"2632,3092,3164,3222,3431,3453,3717,3852,3854,3855,3926,3928,4104,4300,4608,4836,4960,5261,5263,5528,5605,5692,5791,5902,6094,6402,
                                    6450,6675,6755,6902,7821,8178,8378,9082,9398,9440,9590,9636,9985,10315,10657,11317,11421,12005,12059,12062,12146,12147,13266";

                    vSession.TechnologyCategory = "Cloud Storage";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official AWS resellers list nor is affiliated with or in any way associated with Amazon Web Services, Inc.";
                    break;

                case "box":

                    strQuery += @"3486,29694,2659,2854,3305,4227,4771,8276,15527,26971,19881,17640,4533,5916,6100,6243,7887,8283,8958,10335,11875,21127,33957,38384,
                                    19422,3906,35864";

                    vSession.TechnologyCategory = "Cloud Storage";
                    resultsCount = 27;
                    disclaimerText = "This list is not an official Box resellers list nor is affiliated with or in any way associated with Box.";
                    break;

                case "ca":

                    strQuery += @"2938,3119,17165,18247,2586,2873,3011,3957,4685,5083,5446,5647,7018,8852,9318,9399,10772,11569,11778,13209,13386,13633,8242,
                                    13785,15647,22136,25538,25539,31166,9380";

                    vSession.TechnologyCategory = "Cloud Storage";
                    resultsCount = 30;
                    disclaimerText = "This list is not an official CA Technologies resellers list nor is affiliated with or in any way associated with Broadcom.";
                    break;

                case "citrix":

                    strQuery += @"2246,2255,2263,2296,2461,2750,3011,3062,3164,3222,3387,3754,3853,3854,3901,6201,7018,7202,7968,8142,8178,8224,8324,8378,8423,8424,
                                    8471,8532,8935,8937,9017,9018,9086,9126,9179,9233,9273,9316,9318,9357,9398,9399,9497,9587,9590,9636,9841,9982,10076,10077";

                    vSession.TechnologyCategory = "Cloud Storage";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Citrix resellers list nor is affiliated with or in any way associated with Citrix Systems, Inc.";
                    break;

                case "dropbox":

                    strQuery += @"11709,2659,3476,3832,4771,5023,6928,7045,7609,7773,8276,9323,11430,15024,16000,17043,23531,25838,25904,25951,29041,30254,30936,31620,
                                    31705,32122,32575,32963,33839,34120,34126,34382,35103,35382,35383,30113,36448,17640,19084,37695,5944,5181";

                    vSession.TechnologyCategory = "Cloud Storage";
                    resultsCount = 42;
                    disclaimerText = "This list is not an official Dropbox resellers list nor is affiliated with or in any way associated with Dropbox, Inc.";
                    break;

                case "google-cloud":

                    strQuery += @"2448,3164,3453,3852,4034,4871,9896,10936,2791,22797,5754,29138,32010,35809,37366,2900,3942,6197,6198,16893,5719,6084,6079,4892,26041,
                                    6069,5854,26500,4531,29690,5439,30543,30638,30934,31376,33108,33198,33295,27687,33759";

                    vSession.TechnologyCategory = "Cloud Storage";
                    resultsCount = 40;
                    disclaimerText = "This list is not an official Google Cloud resellers list nor is affiliated with or in any way associated with Google.";
                    break;

                case "netapp":

                    strQuery += @"2328,2440,2750,2751,3061,3222,3646,3854,5166,6552,6755,7709,8142,8179,8262,8888,8892,8934,8936,9019,9082,9086,9179,9232,9233,9274,
                                    9397,9441,9442,9443,9497,9532,9533,9534,9535,9983,10234,10273,4004,10578,10657,10813,11165,11485,29743,11747,11837,11865,12038";

                    vSession.TechnologyCategory = "Cloud Storage";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official NetApp resellers list nor is affiliated with or in any way associated with NetApp.";
                    break;

                case "nutanix":

                    strQuery += @"2751,7753,8142,8936,9018,9082,9086,9087,9126,9357,9440,9495,9983,4448,10714,10813,10900,10936,11316,11421,11673,11782,11813,12038,
                                    13353,13601,8242,13761,13812,13981,14063,14101,14104,15316,20740,16803,22037,20960,20402,30043,32404,37367,37776,38369,4281,23163,
                                    36558,27280,8155,14497";

                    vSession.TechnologyCategory = "Cloud Storage";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Nutanix resellers list nor is affiliated with or in any way associated with Nutanix.";
                    break;

                case "rackspace":

                    strQuery += @"2596,2632,2668,5528,6675,3057,3501,3802,4670,6598,10885,19439,24638,6917,16767,20222,38604,9566,36081,33583,4271,5536,22115,22410,
                                    21702,4346,4260";

                    vSession.TechnologyCategory = "Cloud Storage";
                    resultsCount = 27;
                    disclaimerText = "This list is not an official Rackspace resellers list nor is affiliated with or in any way associated with Rackspace.";
                    break;

                case "vmware":

                    strQuery += @"2328,34282,2441,2523,2535,3011,3061,3062,3092,3469,3717,3754,3852,3853,3854,3900,4300,5166,5264,5903,6162,6203,6343,6402,6553,6753,
                                    6900,7018,7753,7822,7876,8049,8142,8179,8224,8262,8532,8800,8888,8889,8892,8936,8937,8982,9017,9018,9019,9086,9126,9127";

                    vSession.TechnologyCategory = "Cloud Storage";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Vmware resellers list nor is affiliated with or in any way associated with VMware, Inc.";
                    break;

                case "bamboohr":

                    strQuery += @"15188,20760";

                    vSession.TechnologyCategory = "HR Software";
                    resultsCount = 2;
                    disclaimerText = "This list is not an official BambooHR resellers list nor is affiliated with or in any way associated with Bamboo HR LLC.";
                    break;

                case "gusto":

                    strQuery += @"32680,32682,32751,33058,33176,33178,33179,33243,33245,33337,33426,33430,33499,33599,33806,33810,33892,33894,34008,34082,34192,
                                    34354,34356,34430,34518,34609,34696,34953,35429,35673,36279,36282,36505,37489,37658,38257";

                    vSession.TechnologyCategory = "HR Software";
                    resultsCount = 36;
                    disclaimerText = "This list is not an official Gusto resellers list nor is affiliated with or in any way associated with Gusto.";
                    break;

                case "kronos":

                    strQuery += @"32796,36056,22363,31098,14076,20931,23597,22370,5771,3949,4702,22070,4329,22264,22364,4821,4902,3873,4085,2381";

                    vSession.TechnologyCategory = "HR Software";
                    resultsCount = 20;
                    disclaimerText = "This list is not an official Kronos resellers list nor is affiliated with or in any way associated with Kronos Incorporated.";
                    break;

                case "pegasus":

                    strQuery += @"13893,27886,19695,3278,37942,5455,19694,3277,14080,5307,14170,14207,3236,14120,14121,14252,3240,3359,19580,19581,3269,3268,3149";

                    vSession.TechnologyCategory = "HR Software";
                    resultsCount = 23;
                    disclaimerText = "This list is not an official Pegasus resellers list nor is affiliated with or in any way associated with Pegasus Software.";
                    break;

                case "successfactors":

                    strQuery += @"2641,12102,10252,22714,7062,23481,14573,23082,24184,34041,23478,23403,11226,28506,21278,23742,23476,23486,14126,19315,22264,2558,
                                    23737,23738,23740,23741,23744,23747,2620,15054";

                    vSession.TechnologyCategory = "HR Software";
                    resultsCount = 30;
                    disclaimerText = "This list is not an official Successfactors resellers list nor is affiliated with or in any way associated with SAP.";
                    break;

                case "tsheets":

                    strQuery += @"19778,22425,6946,7409,33242,34609,35429,36150,36503,37169,37658,37899,20682,8803";

                    vSession.TechnologyCategory = "HR Software";
                    resultsCount = 14;
                    disclaimerText = "This list is not an official Tsheets resellers list nor is affiliated with or in any way associated with Intuit Inc.";
                    break;

                case "workday":

                    strQuery += @"8283,10252,11544,13363,10663,32796,38257,36056,3779,15228,28505,9132,22628,23084,15227,5771,4779,22264,23198,23199,23200,23201";

                    vSession.TechnologyCategory = "HR Software";
                    resultsCount = 22;
                    disclaimerText = "This list is not an official Workday resellers list nor is affiliated with or in any way associated with Workday, Inc.";
                    break;

                case "cloudera":

                    strQuery += @"2536,2569,2588,2597,2616,3411,3431,3450,3452,3453,3625,3646,3851,3901,3928,4001,4035,4105,4137,4305,4565,4717,4834,5261,5262,5263,
                                    5330,5336,5487,5530,5572,5606,5839,5902,5903,5958,6343,6401,6607,6675,7105,9442,10589,11711,13634,13785,13870,15452,20911,20912";

                    vSession.TechnologyCategory = "Big Data";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Cloudera resellers list nor is affiliated with or in any way associated with Cloudera, Inc.";
                    break;

                case "hortonworks":

                    strQuery += @"2569,2597,2652,3010,3092,3189,3221,3412,3431,3450,3541,3593,3625,3754,3855,3900,3901,3928,4001,4033,4034,4067,4137,4264,4565,4648,5005,
                                    5214,5261,5262,5330,5487,5528,5530,5572,5840,5902,6450,6607,6675,6968,9317,10589,15238,15452,5170,20911,20912,10907,25825";

                    vSession.TechnologyCategory = "Big Data";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Hortonworks resellers list nor is affiliated with or in any way associated with Cloudera, Inc.";
                    break;

                case "informatica":

                    strQuery += @"2561,2571,2588,2771,3388,3625,3645,3680,4001,4104,4139,4186,4265,4303,4373,4418,4464,4752,4753,4834,4835,4836,4912,4960,5043,5734,
                                    5957,6343,6346,6400,7103,7158,10275,12145,12176,12177,13469,13903,15520,15592,15712,20911,20988,3860,21178,11955,11501,25901,25942,
                                    16534";

                    vSession.TechnologyCategory = "Big Data";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Informatica resellers list nor is affiliated with or in any way associated with Informatica.";
                    break;

                case "mapr":

                    strQuery += @"3092,3431,3450,3541,3928,4137,4139,5005,5214,5262,5330,5487,5572,5790,5902,6450,6675,9126,15452,25825,18369,18370,34766,8871,
                                    4598,15465,16419,19816,9032,12186,13684,32996,13547,23409,15445,4697";

                    vSession.TechnologyCategory = "Big Data";
                    resultsCount = 36;
                    disclaimerText = "This list is not an official MapR resellers list nor is affiliated with or in any way associated with MapR Technologies, Inc.";
                    break;

                case "talend":

                    strQuery += @"2392,2588,2632,2685,2978,3337,3388,3411,3453,3487,4035,4139,4420,5262,5530,5605,5694,5957,6346,6902,6968,12145,12178,15520,5170,9622,
                                    27972,33006,38153,7237,10297,10785,4556,4599,11645,9763,14637,5102,13684,16554,11689,13346,25253,13654,10967,13287,15946,5217,23206";

                    vSession.TechnologyCategory = "Big Data";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official Talend resellers list nor is affiliated with or in any way associated with Talend.";
                    break;

                case "enterprisedb":

                    strQuery += @"6345,6401,6402,6403,6448,6450,6451,6554,6676,6755,6756,6850,6900,6901,6968,34659,6717,6486";

                    vSession.TechnologyCategory = "Databases";
                    resultsCount = 18;
                    disclaimerText = "This list is not an official EnterpriseDB resellers list nor is affiliated with or in any way associated with EnterpriseDB Corporation.";
                    break;

                case "oracle":

                    strQuery += @"24873,2392,2397,2422,2504,2523,2525,2560,2571,2616,2668,3010,3011,3093,3094,3165,3292,3337,3387,3412,3469,3485,3571,3646,3717,3754,
                                    3756,3855,3928,3956,3957,4033,4067,4185,4186,4230,4303,4305,4373,4418,4515,4563,4717,4750,4752,4753,4799,4836,5043,5216";

                    vSession.TechnologyCategory = "Databases";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Oracle resellers list nor is affiliated with or in any way associated with Oracle.";
                    break;

                case "snowflake":

                    strQuery += @"5902,13469,27972,12186,18694,31456,35662,37642,16829,24707,12154";

                    vSession.TechnologyCategory = "Databases";
                    resultsCount = 11;
                    disclaimerText = "This list is not an official Snowflake resellers list nor is affiliated with or in any way associated with Snowflake Inc.";
                    break;

                case "teradata":

                    strQuery += @"2525,3388,3680,4564,5214,5216,5261,5262,5336,5734,5957,6346,6400,12176,12177,3965,15712,15713,20911,15860,25942,26853,4531,25651,
                                    8871,8915,4598,4501,11931,13513,13705,13772,17508,19816,13476,16119,16283,4310,16638,3296,14118";

                    vSession.TechnologyCategory = "Databases";
                    resultsCount = 41;
                    disclaimerText = "This list is not an official Teradata resellers list nor is affiliated with or in any way associated with Teradata.";
                    break;

                case "erwin":

                    strQuery += @"4556,28594,18548,17299,29936,18696,7473";

                    vSession.TechnologyCategory = "Data Management";
                    resultsCount = 7;
                    disclaimerText = "This list is not an official Erwin resellers list nor is affiliated with or in any way associated with erwin, Inc.";
                    break;

                case "esri":

                    strQuery += @"2698,3855,4914,9272,4711,13414,14018,20988,7934,10955,34997,37480,10837,14251,26061,38384,2556,29490,29560,29755,29760,29946,29949,
                                    30028,30217,30371,30911,33422,34075,34076,34174,34258,34262,34264,34266,34349,34424,34425,34516,36265,36268,36360,36362,36370,36423";

                    vSession.TechnologyCategory = "GIS";
                    resultsCount = 45;
                    disclaimerText = "This list is not an official Esri resellers list nor is affiliated with or in any way associated with Esri.";
                    break;

                case "hootsuite":

                    strQuery += @"2625,4051,7044,24551,27896,34043,15002,16291,16082,2415,5619,3918";

                    vSession.TechnologyCategory = "Social Media Management";
                    resultsCount = 12;
                    disclaimerText = "This list is not an official Hootsuite resellers list nor is affiliated with or in any way associated with Hootsuite Inc.";
                    break;

                case "lightspeed":

                    strQuery += @"8472,3257,25913,28195,36243,3545,8819,21051,3128,4465,8279,8194,9008,3521,3544,3574,3100,3127";

                    vSession.TechnologyCategory = "POS";
                    resultsCount = 18;
                    disclaimerText = "This list is not an official Lightspeed resellers list nor is affiliated with or in any way associated with Lightspeed.";
                    break;

                case "ncr":

                    strQuery += @"20914,5062,10536,6000,24813,4516,21256,14150,3960,13443,22364,24107,6667,4380,4722,4964,5800,5911,4192,4339,4694,5221,3556,9957,
                                    10089,7171,5035,3507";

                    vSession.TechnologyCategory = "POS";
                    resultsCount = 28;
                    disclaimerText = "This list is not an official NCR resellers list nor is affiliated with or in any way associated with NCR Corporation.";
                    break;

                case "vend":

                    strQuery += @"4736,22802,28641,28776,24403,15188,8214,33314,22716,3545,33426,33810,34008,34430,20682,20733,20891,20892,21090,21436,
                                    21647,21857,4042,20626,22107,30750,20532,8819,10331,7715,7714";

                    vSession.TechnologyCategory = "POS";
                    resultsCount = 31;
                    disclaimerText = "This list is not an official Vend resellers list nor is affiliated with or in any way associated with Vend™ Limited.";
                    break;

                case "kounta":

                    strQuery += @"27802,28641,28776,3545,34892,4379,20733,20975,21545,21647,4042,20532,8819,3128,11940,21543";

                    vSession.TechnologyCategory = "POS";
                    resultsCount = 16;
                    disclaimerText = "This list is not an official Kounta resellers list nor is affiliated with or in any way associated with Kounta.";
                    break;

                case "livetiles":

                    strQuery += @"3755,27700,33842,8769,14153,11446,26645";

                    vSession.TechnologyCategory = "Collaboration";
                    resultsCount = 7;
                    disclaimerText = "This list is not an official LiveTiles resellers list nor is affiliated with or in any way associated with LiveTiles Limited.";
                    break;

                case "matrix42":

                    strQuery += @"3062,11838,6259,2911,7863";

                    vSession.TechnologyCategory = "Collaboration";
                    resultsCount = 5;
                    disclaimerText = "This list is not an official Matrix42 resellers list nor is affiliated with or in any way associated with Matrix42 AG.";
                    break;

                case "powell-365":

                    strQuery += @"6438,6445,6493";

                    vSession.TechnologyCategory = "Collaboration";
                    resultsCount = 3;
                    disclaimerText = "This list is not an official Powell 365 resellers list nor is affiliated with or in any way associated with Powell Software.";
                    break;

                case "3cx":

                    strQuery += @"6162,7823,10233,11189,18799,29696,18272,18284,18321,17841,18471,18473,7608,17037,17868,20363,23457,25313,27332,29394,30328,
                                    11146,30994,22759,33299,33380,33839,31522,37452,37458,38378,13954,20226,36356,21380,10631,5683,6594,6605,6329,7890,6043,
                                    7939,14122,15605,3240,17640";

                    vSession.TechnologyCategory = "VoIP";
                    resultsCount = 47;
                    disclaimerText = "This list is not an official 3CX resellers list nor is affiliated with or in any way associated with 3CX.";
                    break;

                case "asterisk":

                    strQuery += @"8487,17590,23530,29104,37456,6644,5639,6488,8150,29991,16738,17638,19267,35325,10646,19693,21072,22590,22984";

                    vSession.TechnologyCategory = "VoIP";
                    resultsCount = 19;
                    disclaimerText = "This list is not an official Asterisk resellers list nor is affiliated with or in any way associated with Sangoma.";
                    break;

                case "audiocodes":

                    strQuery += @"11812,32949,6688,9744,9477,16557,5829,9431,16477,32229,19178,33024,23325,10321,6046";

                    vSession.TechnologyCategory = "VoIP";
                    resultsCount = 15;
                    disclaimerText = "This list is not an official AudioCodes resellers list nor is affiliated with or in any way associated with AudioCodes Limited.";
                    break;

                case "avaya":

                    strQuery += @"3291,3852,9944,29743,11837,11864,13330,13470,13570,13762,13871,22440,30777,31085,19497,21695,21700,21931,3328,7943,26500,
                                    27333,33380,36707,7936,11375,16748,14450,14158,14447,19270,36063,37954,3176,3236,14809,19099,6643,7224,32508,27085,37567,
                                    28654,21413";

                    vSession.TechnologyCategory = "VoIP";
                    resultsCount = 44;
                    disclaimerText = "This list is not an official Avaya resellers list nor is affiliated with or in any way associated with Avaya Inc.";
                    break;

                case "brocade":

                    strQuery += @"2329,2750,2937,2954,3291,3594,5124,5166,5214,6508,6553,6900,7104,8178,8224,8472,8533,9019,9127,9130,9232,9398,9443,9495,9533,
                                    9534,9983,10274,10899,11132,11189,11711,11782,12008,12036,6640,12149,3743,13499,8242,13671,13786,13948,14101,6557,14104,
                                    15184,15238,15394";

                    vSession.TechnologyCategory = "VoIP";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official Brocade resellers list nor is affiliated with or in any way associated with Broadcom.";
                    break;

                case "centurylink":

                    strQuery += @"9357,16519,6112,7223,19178,32791,15434,32983";

                    vSession.TechnologyCategory = "VoIP";
                    resultsCount = 8;
                    disclaimerText = "This list is not an official CenturyLink resellers list nor is affiliated with or in any way associated with CenturyLink.";
                    break;

                case "ringcentral":

                    strQuery += @"2957,3776,4585,9186,15845,16893,25168,25694,8480,5445,15474,14021,21629,6409,24158,18454,9069,10866,16967,35103,38371,9566";

                    vSession.TechnologyCategory = "VoIP";
                    resultsCount = 22;
                    disclaimerText = "This list is not an official RingCentral resellers list nor is affiliated with or in any way associated with RingCentral, Inc.";
                    break;

                case "twilio":

                    strQuery += @"34755,13622,27084";

                    vSession.TechnologyCategory = "VoIP";
                    resultsCount = 3;
                    disclaimerText = "This list is not an official Twilio resellers list nor is affiliated with or in any way associated with Twilio Inc.";
                    break;

                case "unify":

                    strQuery += @"21519,30153,14242,20403,21330,21630,19619,21524,4477,33130,3193,30179,37566,37567,22390,17205,22391,23093,31190,23333,23335,
                                    32322,17207,22784,31336,29746,19494,19614,21241,21243,21244,21332,21336,21414,21415";

                    vSession.TechnologyCategory = "VoIP";
                    resultsCount = 35;
                    disclaimerText = "This list is not an official Unify resellers list nor is affiliated with or in any way associated with Atos SE.";
                    break;

                case "g-suite":

                    strQuery += @"2435,2500,2546,2564,2563,2437,2516,2548,2606,2625,2448,2518,2640,2695,2707,2449,2519,2520,2737,10061,2806,2465,2466,2740,
                                    2807,2900,2488,2989,3018,3020,3041,2679,18409,32089,2489,2490,2694,3042,3086,3153,2547,3180,2512,3281,3305,3330,2581,
                                    2988,3328,3361,2593,2732,3058,3363,2594,3181,3206,3434,3475,2607,3362,3454,3455,3476,3501,31611,31612,31613,31618,31619";

                    vSession.TechnologyCategory = "Email";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official G Suite resellers list nor is affiliated with or in any way associated with Google.";
                    break;

                case "office-365":

                    strQuery += @"18733,18730,18463,20147,8029,11259,26974,26897,20157,20914,7059,26064,23841,23843,27699,6494,6445,14425,32414,
                                    32413,32412,32410,32407,32406,32298,32213,32122,32121,32120,32119,32118,32117,32021,32020,32019,32018,32017,
                                    32016,32015,27327,31961,31960,29201,31959,31958,31956,31860,31859,31858,31715,31706,31620,31617,31616,31615,
                                    10059,31489,31488,7782,31487,31486,31485,30609,31482,31380,31379,31378,23976,10211,27592";

                    vSession.TechnologyCategory = "Email";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Office 365 resellers list nor is affiliated with or in any way associated with Microsoft.";
                    break;

                case "kerio-connect":

                    strQuery += @"7923,9985,5023,8489,17869,24747,27802,23132,8807,31710,22144,7530,20717,20874,20955,21229,21230,21512,21514,21708,
                                    21709,10632,7635,9347,31824,4274,20138,22358,8529,10810,2304323046,20728,20729,20730,23853,6641,22022,21940,22025,
                                    22205,7891,6153,22616,22871,10881,10621,10623,9174,9560,8879,10390,8972,9008,10840,6155,9024,9026,7939,8977,7662,2797";

                    vSession.TechnologyCategory = "Email";
                    resultsCount = 63;
                    disclaimerText = "This list is not an official Kerio Connect resellers list nor is affiliated with or in any way associated with GFI.";
                    break;

                case "zimbra":

                    strQuery += @"6448,6753,6901,11385,11486,22037,4898,6232,7232,9324,11463,23532,6154,25690,25907,8753,30158,6917,30407,30784,14071,
                                    14547,18623,5057,4978,4934,5058,5602,5441,8334,5440,6329,7765,8949,21516,23206,20893,5398,5444,29902,8510,30298,5622,
                                    6033,5027,5079,5081,17637,5639,5640,5436,6378,5442,6486,6488,5445,8966,5400,5641,5401,5397,5437,6430,6487,5720,6645,
                                    6398,6640,6108,5863";

                    vSession.TechnologyCategory = "Email";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Zimbra resellers list nor is affiliated with or in any way associated with Synacor, Inc.";
                    break;

                case "mimecast":

                    strQuery += @"2847,2938,3754,11240,6491,20778,29099,29102,6286,30777,32402,19873,20780,5378,2581,2594,3637,10667,26774,27333,8433,
                                    29150,29235,30988,31610,6132,7216,7333,15075,15402,6110,15805,15942,24792,18694,29795,31798,14904,17369,15047,795,
                                    18163,17020,10744,20660,20958,20963,21699,17460,18571,3648";

                    vSession.TechnologyCategory = "Email";
                    resultsCount = 51;
                    disclaimerText = "This list is not an official Mimecast resellers list nor is affiliated with or in any way associated with Mimecast.";
                    break;

                case "icewarp":

                    strQuery += @"7504,7831,21514,7938,7937,7647,7832,7890,7892,7937,7034,7546,7939,4494";

                    vSession.TechnologyCategory = "Email";
                    resultsCount = 14;
                    disclaimerText = "This list is not an official IceWarp resellers list nor is affiliated with or in any way associated with IceWarp.";
                    break;

                case "sharepoint":

                    strQuery += @"8800,9315,9801,9944,9982,10316,10592,11240,11275,11569,11745,11812,12120,13386,13727,7929,22801,29100,30781,31230,
                                    31703,7226,7494,5378,5300,6198,6274,6544,6658,6872,6928,7130,14274,7232,7448,8069,8337,8721,9238,9594,9642,9726,
                                    9728,9950,10282,10721,10944,11325,11430,15764,17037,17868,24450,9263,6154,26032,27582,10703,29240,6469,5875,8753,
                                    6269,6536,30490,30641,30785,10755,20296,31172";

                    vSession.TechnologyCategory = "Microsoft";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official SharePoint resellers list nor is affiliated with or in any way associated with Microsoft.";
                    break;

                case "dynamics":

                    strQuery += @"2258,3223,5447,6673,7929,25415,13893,3573,19776,14452,14505,21234,3515,3624,3957,4033,4067,4419,5734,6201,6293,8179,
                                    8324,9017,9233,10769,11070,3153,3305,3501,3911,5300,6657,8112,7928,3192,2824,6005,6405,2895,3394,5651,7973,13557,
                                    13655,27352,27354,27355,27356,27413,27151,27153,27154,26238,25090,25186,25189,24441,19652,3327";

                    vSession.TechnologyCategory = "Microsoft";
                    resultsCount = 60;
                    disclaimerText = "This list is not an official Dynamics 365 resellers list nor is affiliated with or in any way associated with Microsoft.";
                    break;

                case "yammer":

                    strQuery += @"29671,4467,6104,23347,23713,15764,3412,5607,6607,8178,8179,8636,8691,9982,4004,11812,3257,4890";

                    vSession.TechnologyCategory = "Microsoft";
                    resultsCount = 18;
                    disclaimerText = "This list is not an official Yammer resellers list nor is affiliated with or in any way associated with Microsoft.";
                    break;

                case "azure":

                    strQuery += @"2753,3375,3452,3624,3680,3717,3755,3957,4033,4034,4067,4606,5447,5734,5791,5840,5900,5903,6201,6293,6401,7104,7821,7823,
                                    8142,8178,8179,8224,8691,8800,8937,8982,9017,9179,9233,9440,9982,10076,10077,10233,10316,10713,10769,5324,11070,11164,
                                    11187,11275,11672,2836,3019,3057,3501,3637,3911,4018,4049,4985,5024,5300,6274,6440,6497,6544,7609,9901,9950,10239,14303,2824";

                    vSession.TechnologyCategory = "Microsoft";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Azure resellers list nor is affiliated with or in any way associated with Microsoft.";
                    break;

                case "power-bi":

                    strQuery += @"2384,3119,3120,3122,3189,3468,3513,3541,3680,4033,4264,4464,4686,4753,4960,5607,5734,5900,5957,6201,6343,8691,9233,10769,
                                    11316,3501,7608,7943,5271,11142,9931,13228,7653,8606,6131,4421,11501,3192,2824,6097,4755,6972,9151,6405,6406,8548,2895,
                                    3167,4872,7108,8011,5218,3694,3979,4036,5402,7106,10291,5780,3066";

                    vSession.TechnologyCategory = "Microsoft";
                    resultsCount = 60;
                    disclaimerText = "This list is not an official Power BI resellers list nor is affiliated with or in any way associated with Microsoft.";
                    break;

                case "skype":

                    strQuery += @"6345,7878,8179,10233,11187,11317,11709,11812,7609,24089,32582,8150,17619,18378,17482,22976,24070,29412,31010,31635,9844,
                                    3204,24302,27260,9662,20729,17481,19361,9393,21631,22190,22748,23102,23346,3883";

                    vSession.TechnologyCategory = "Microsoft";
                    resultsCount = 36;
                    disclaimerText = "This list is not an official Skype for Business resellers list nor is affiliated with or in any way associated with Microsoft.";
                    break;

                case "check-point":

                    strQuery += @"2341,8007,8008,8049,8141,8179,8746,9895,10577,11711,13248,5784,5969,29135,29136,29443,2246,2256,2328,2632,2938,3164,3291,
                                    3852,5647,8101,8102,8262,8322,8381,8425,8426,8531,9019,9128,9129,10079,10590,10712,10770,10772,11021,11129,11458,11485,
                                    11519,11569,29743,11605,11777,11780,11813,11861,11862,11948,11978,12009,12010,12034,12035,12036,12038,12062,12063,4656,
                                    12087,12090,12120,12121,12122";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Check Point resellers list nor is affiliated with or in any way associated with Check Point Software Technologies, Ltd.";
                    break;

                case "mcafee":

                    strQuery += @"2393,2461,2750,2938,3291,6025,7539,8008,8264,9128,10079,10817,11241,11482,11485,11523,11633,11746,11813,11836,11948,12034,
                                    4656,13248,13329,13353,13388,13811,13843,13947,13980,16803,22035,25466,29304,18396,6286,31081,31703,32204,32402,32704,
                                    32761,32845,32848,20780,20661,2594,4049,6273,6392,15762,25621,31374,25016,14331,31179,16786,16991,5743,7530,28992,29121,
                                    15167,19270,6156,5939,23702,28221,17259";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official McAfee resellers list nor is affiliated with or in any way associated with McAfee, LLC.";
                    break;

                case "imperva":

                    strQuery += @"2275,2321,2939,3032,6203,6292,7649,8423,9589,10076,10590,10712,11021,11362,11421,11455,11523,11781,11980,4656,12090,12122,
                                    12148,12150,13209,13210,13248,13245,13246,13270,13327,13352,13353,13354,13388,13412,13413,13414,13433,13436,13437,13468,
                                    13469,13470,13497,13501,13530,13568,13570,13571,13572,13573,13600,13603,13605,13630,13631,13632,13634,13635,13668,13670,
                                    8242,13731,13759,13761,13785,13787,13810,13811";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Imperva resellers list nor is affiliated with or in any way associated with Imperva, Inc.";
                    break;

                case "sophos":

                    strQuery += @"2245,2322,2523,2938,3061,3062,6162,6345,7424,7823,7923,7970,8263,8378,8424,8425,8473,8532,8636,9129,9177,9273,9895,10459,
                                    10590,10592,10817,5324,11072,11188,11240,11313,11385,11486,11604,11633,11710,11777,11780,11839,13206,13385,13388,13600,
                                    13605,13727,14063,15113,22036,22037,22674,28072,29442,29443,29694,18272,30780,30984,32402,32570,32845,4090,5025,6197,
                                    6198,6544,7392,7982,8755,10365";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Sophos resellers list nor is affiliated with or in any way associated with Sophos Group plc.";
                    break;

                case "juniper-networks":

                    strQuery += @"2256,2263,2461,2632,2750,2938,2954,3164,3291,3594,3788,3819,3852,5124,6203,8009,8102,8179,8471,8636,8936,9128,9532,9841,
                                    10273,10577,10578,10590,10714,5324,10935,11131,11132,11455,11456,11486,29743,11709,11710,11779,12006,12060,12088,12118,
                                    13209,13210,13270,13330,13352,13386,3743,13470,13530,13631,13635,13761,13786,13840,13841,13871,13872,13873,13874,13946,
                                    14017,14018,14101,22921,16206,28770";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Juniper Networks resellers list nor is affiliated with or in any way associated with Juniper Networks, Inc.";
                    break;

                case "a10":

                    strQuery += @"2274,2275,2329,2341,2954,6162,6553,8179,8426,8474,8691,9895,11978,12006,12062,12148,12150,13389,13470,13472,13498,13501,
                                    13761,13810,13812,13842,13979,14059,16741,5784,20987,21881,32950,8604,24750,4344,18642,19596,25876,32030,36355,22522,
                                    17301,8304,32434,33567,33780";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 47;
                    disclaimerText = "This list is not an official A10 resellers list nor is affiliated with or in any way associated with A10, Inc.";
                    break;

                case "alienvault":

                    strQuery += @"10358,10860,11073,13327,13572,13600,13603,15391,16786,16923,5973,31422,15495,31530,18790,2356,2341,2939,10770,13388,20960,
                                    32765,18434,8040,15167,16028";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 26;
                    disclaimerText = "This list is not an official AlienVault resellers list nor is affiliated with or in any way associated with AT&T Business.";
                    break;

                case "anomali":

                    strQuery += @"11276,11712,13268,13979,29135,29235,33704";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 7;
                    disclaimerText = "This list is not an official Anomali resellers list nor is affiliated with or in any way associated with Anomali.";
                    break;

                case "barracuda-networks":

                    strQuery += @"2300,2322,2504,2938,2939,6555,7923,8049,8263,8379,8425,8533,9398,10036,7216,10315,10899,11070,11072,11129,11313,11709,11838,
                                    11862,12120,13206,13208,13414,13573,13670,13671,13810,13872,13982,3601,15236,15239,15710,22231,27886,28862,28967,29136,29304,
                                    29443,29512,20665,30865,30925";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official Barracuda Networks resellers list nor is affiliated with or in any way associated with Barracuda Networks, Inc.";
                    break;

                case "beyondtrust":

                    strQuery += @"7422,8423,9130,10076,11313,11422,11712,4711,13671,13979,5969,28967,36831,16035,17794,32030,7442,7293,33478,29745,29816,20976,35327,15362";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 24;
                    disclaimerText = "This list is not an official BeyondTrust resellers list nor is affiliated with or in any way associated with BeyondTrust Corporation.";
                    break;

                case "bitdefender":

                    strQuery += @"7877,8224,8690,11569,12010,13727,30865,33545,5025,10326,17140,28636,7338,29035,29041,30933,30988,29679,34389,35385,11791,13952,
                                    16666,21786,34095,5743,28884,18843,5922,24134";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 30;
                    disclaimerText = "This list is not an official Bitdefender resellers list nor is affiliated with or in any way associated with Bitdefender.";
                    break;

                case "bitsight":

                    strQuery += @"10659,13268,13785,13979,37992,19936,33704";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 7;
                    disclaimerText = "This list is not an official BitSight resellers list nor is affiliated with or in any way associated with BitSight Technologies.";
                    break;

                case "blue-coat":

                    strQuery += @"2246,2247,2256,2274,2302,2341,2939,8008,8009,8049,8141,8426,8474,8531,8635,8746,9129,9232,9590,10076,11188,11455,11711,
                                    11837,11839,11861,11948,12035,12063,4656,12087,12122,12147,13248,13268,13350,13433,13497,13501,13603,13670,13671,13761,
                                    13810,13841,13872,13874,13903,14017,14019";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Blue Coat resellers list nor is affiliated with or in any way associated with Symantec.";
                    break;

                case "boldon-james":

                    strQuery += @"30983,35634,35490,17210";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 4;
                    disclaimerText = "This list is not an official Boldon James resellers list nor is affiliated with or in any way associated with Boldon James Ltd.";
                    break;

                case "bomgar":

                    strQuery += @"8381,29149,25777,16026,16028,16430,16111,16153,16525,16527,16274,23823,16025,16619,4934,16112,16523,16154,16027";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 19;
                    disclaimerText = "This list is not an official Bomgar resellers list nor is affiliated with or in any way associated with BeyondTrust.";
                    break;

                case "carbon-black":

                    strQuery += @"3032,8141,8531,9636,9671,9895,29533,11072,11712,12008,13501,13568,13947,14017,14019,20987,21960,28429,36772,5024,15999,4344,
                                    18551,19596,20225,25876,34054,17167,29174";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 29;
                    disclaimerText = "This list is not an official Carbon Black resellers list nor is affiliated with or in any way associated with Carbon Black, Inc.";
                    break;

                case "centrify":

                    strQuery += @"2750,8802,10315,11360,11861,13904,15708,3687,30925,16209,37615,6544,16906,33376,25876,23890,17251,17372,17254,17167,15286";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 21;
                    disclaimerText = "This list is not an official Centrify resellers list nor is affiliated with or in any way associated with Centrify Corporation.";
                    break;

                case "checkmarx":

                    strQuery += @"8531,21880,4155,6584,17167,37173,32738,33704,5999";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 9;
                    disclaimerText = "This list is not an official Checkmarx resellers list nor is affiliated with or in any way associated with Checkmarx Ltd.";
                    break;

                case "cloudflare":

                    strQuery += @"2625,9238,19439,22681,25010,32115,33299,16614,27590,32432,6082,3769";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 12;
                    disclaimerText = "This list is not an official Cloudflare resellers list nor is affiliated with or in any way associated with Cloudflare, Inc.";
                    break;

                case "crowdstrike":

                    strQuery += @"2250,7422,10315,13245,13327,13530,13632,13843,13904,13947,29135,30984,15927,18553,19597,25876,28213,15167,20229";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 19;
                    disclaimerText = "This list is not an official CrowdStrike resellers list nor is affiliated with or in any way associated with CrowdStrike Holdings, Inc.";
                    break;

                case "cyber-essentials":

                    strQuery += @"3656,28583,28635,28863,29512,32762,32846,30049,32319,3460";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 10;
                    disclaimerText = "This list is not an official Cyber Essentials resellers list nor is affiliated with or in any way associated with Cyber Essentials.";
                    break;

                case "cyberark":

                    strQuery += @"2274,2873,8264,8691,9129,10315,10770,11605,11636,11711,12034,12038,13244,13248,13247,13351,13437,13501,13634,13731,13759,13763,
                                    13843,13872,16741,20987,3758,31164,35969,36397,36772,37178,5024,15999,33299,37867,15250,17794,17973,18701,19596,7530,25777,
                                    37828,37992,16619,36084,18446,15920,16908";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official CyberArk resellers list nor is affiliated with or in any way associated with CyberArk Software Ltd.";
                    break;

                case "f5-networks":

                    strQuery += @"2264,2273,2385,2773,2939,3032,3222,3291,3788,6552,7649,8008,8049,8141,8262,8381,8423,8691,9082,9085,9086,9127,9128,9232,9534,9588
                                    ,9636,9983,10120,10590,10591,10714,10715,11485,11519,29743,11604,11779,11780,11836,11840,11862,11865,11948,5451,11978,11980,
                                    12007,12008,12009";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official F5 Networks resellers list nor is affiliated with or in any way associated with F5 Networks, Inc.";
                    break;

                case "fortinet":

                    strQuery += @"2245,2247,2287,2322,2329,3291,3387,3852,5264,6203,6753,7018,8008,8049,8178,8224,8325,8471,8472,8747,8854,9083,9128,9129,9273,
                                    9357,9982,10577,10578,10591,10715,10770,10772,10860,5324,10936,11129,11132,11313,11317,11485,11519,29743,11708,11710,11744,
                                    11747,11779,11813,11838";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Fortinet resellers list nor is affiliated with or in any way associated with Fortinet, Inc.";
                    break;

                case "f-secure":

                    strQuery += @"10120,10655,11099,25418,10062,2300,7018,7424,7924,8007,8179,8262,8378,8888,10771,5324,11073,11746,12122,6640,3658,36829,7129,
                                    20992,31086,6976,14542,18901,7125,37907,29177,29420,34335,31916,5699";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 35;
                    disclaimerText = "This list is not an official F-Secure resellers list nor is affiliated with or in any way associated with F-Secure.";
                    break;

                case "eset":

                    strQuery += @"2938,8263,9177,11385,11746,11777,13412,13572,13670,13727,13907,14059,27886,29439,3336,31700,31852,32205,32402,32481,32482,32847,
                                    3086,4860,8192,9725,11136,11391,18898,25075,25172,27973,8433,29041,29784,30048,30258,8807,30609,31959,32118,33198,34389,34455,
                                    36448,36907,10504,27917,16767,5136";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official ESET resellers list nor is affiliated with or in any way associated with ESET, spol. s r.o.";
                    break;

                case "gemalto":

                    strQuery += @"2250,3819,7424,7820,7877,8009,8049,8471,9129,10591,11132,11605,11744,11779,11861,12008,12010,12038,13527,13573,13787,13945,14101,
                                    15708,22440,22538,3687,28632,28822,28862,28966,34830,15824,36891,33299,5704,10675,10877,11375,28987,17794,7530,38028,6643,16527";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 45;
                    disclaimerText = "This list is not an official Gemalto resellers list nor is affiliated with or in any way associated with Gemalto NV.";
                    break;

                case "kaspersky":

                    strQuery += @"2356,2938,7753,8007,8008,8263,8325,8424,9019,9128,9177,9804,10712,10715,10817,10860,11132,11313,11315,11671,11709,11744,11746,11811,
                                    12087,13331,13569,13572,13906,13907,13980,5784,20914,22226,29102,18396,31351,31779,31852,31855,32285,32481,32845,32846,33191,19496,
                                    3658,35631";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 48;
                    disclaimerText = "This list is not an official Kaspersky resellers list nor is affiliated with or in any way associated with AO Kaspersky Lab.";
                    break;

                case "logrythm":

                    strQuery += @"4301,8380,10412,4656,13436,13573,13601,13631,13668,13731,13874,13945,3687,29441,4201,18552,18954,35685,38047,4072,29794,34941,4366,
                                    8607,9105";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 25;
                    disclaimerText = "This list is not an official LogRhythm resellers list nor is affiliated with or in any way associated with LogRhythm, Inc.";
                    break;

                case "malwarebytes":

                    strQuery += @"7969,8425,9637,10358,13436,21880,29516,33545,7501,24542,15402,5141,18169,33739,10631,6431,6328,33794,7474,26737,18568,18383,19071,
                                    19354,19356,9978,9116";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 27;
                    disclaimerText = "This list is not an official Malwarebytes resellers list nor is affiliated with or in any way associated with Malwarebytes.";
                    break;

                case "observeit":

                    strQuery += @"2374,8855,11072,29027,32402,25426,4281,27889,31257,6234,16154,4072,15222,10528,7662";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 15;
                    disclaimerText = "This list is not an official ObserveIT resellers list nor is affiliated with or in any way associated with ObserveIT.";
                    break;

                case "okta":

                    strQuery += @"2250,6755,11240,11360,11422,12150,13210,13527,13945,13946,15708,22542,22677,6491,29512,32402,2489,2957,25833,26225,33957,12075,15250,
                                    18694,26071,31275,35685,15228,16908,35525,35681,35683,35866,16619,17254,35680,35682";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 37;
                    disclaimerText = "This list is not an official Okta resellers list nor is affiliated with or in any way associated with Okta, Inc.";
                    break;

                case "palo-alto-networks":

                    strQuery += @"2263,2448,2441,2937,2938,2939,2954,3032,3165,3222,3387,5124,5647,7877,8102,8179,8635,9082,9086,9129,9232,9316,9356,9534,9985,10591,
                                    10771,10860,11131,11132,11187,11456,11744,11779,11780,11836,11837,11864,11865,11977,12006,12034,12035,12060,12118,12150,13206,13210";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 48;
                    disclaimerText = "This list is not an official Palo Alto Networks resellers list nor is affiliated with or in any way associated with Palo Alto Networks, Inc.";
                    break;

                case "rapid7":

                    strQuery += @"2245,2300,2322,6901,7925,8423,8747,8854,10459,11455,11484,4656,13243,13497,13601,13787,13947,29516,31081,6801,11760,16786,16923,4201,
                                    18552,19594,21302,20303,8040,3576,35502,37992,15430,16984,29175,29177,29908,32738,33795,34245";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 40;
                    disclaimerText = "This list is not an official Rapid7 resellers list nor is affiliated with or in any way associated with Rapid7.";
                    break;

                case "red-hat":

                    strQuery += @"2255,2504,2523,2536,2588,3222,3452,3754,3818,3900,3926,4649,5330,6402,6448,6450,6451,6508,6555,6675,6753,6754,6900,6902,6968,7018,7202,
                                    7821,9086,9232,9318,4004,10589,11421,11486,11604,12146,13269,13811,7917,14058,6557,15181,15184,15234,15236,15239,15316,15647,20912";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Red Hat resellers list nor is affiliated with or in any way associated with Red Hat, Inc.";
                    break;

                case "rsa":

                    strQuery += @"34282,2535,2937,2938,3387,3819,6025,8009,8141,8381,8423,8426,9671,10318,11420,6017,11422,11456,11458,11482,11484,11521,11522,11523,
                                    11569,11602,29743,11605,11606,11633,11635,11638,11668,11669,11671,11708,11710,11744,11778,11780,11781,11810,11811,11813,11836,11837,
                                    4655,11839";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 48;
                    disclaimerText = "This list is not an official RSA resellers list nor is affiliated with or in any way associated with RSA Security LLC.";
                    break;

                case "skybox-security":

                    strQuery += @"2252,2773,7424,8264,11813,11948,13945,29441,31702,36772,36833,14849,16210,29908,33153,16804,16524,7476,4624";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 19;
                    disclaimerText = "This list is not an official Skybox Security resellers list nor is affiliated with or in any way associated with Skybox Security, Inc.";
                    break;

                case "splunk":

                    strQuery += @"2258,2448,2441,2750,2937,2938,3032,3222,3788,3851,3852,3854,3901,3926,4717,5572,5840,8380,8381,8635,8637,8802,8852,8936,9086,9359,
                                    9671,10318,10411,10456,11360,11482,11671,11744,11864,11948,12007,12009,12038,13244,13266,13270,13353,13415,13437,13472,13763,13785,
                                    13812,13813";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Splunk resellers list nor is affiliated with or in any way associated with Splunk Inc.";
                    break;

                case "symantec":

                    strQuery += @"2251,2254,2258,34282,2523,3011,3164,5446,6025,6203,6343,6345,6555,7018,7202,7822,8008,8379,8380,8472,8691,9083,9232,9274,9397,9399,
                                    9400,9442,9443,10233,10318,4004,10936,6017,11485,11523,29743,11708,11746,11747,11810,11840,11864,11948,4656,13245,13269,13331,13386,
                                    3743";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official Symantec resellers list nor is affiliated with or in any way associated with Broadcom.";
                    break;

                case "tenable":

                    strQuery += @"3852,8264,8380,9086,9671,10358,10715,11072,11129,11484,11671,11744,11836,11948,13248,13433,13436,13471,13573,13605,13630,13981,5969,
                                    21960,25466,18573,32401,36692,15805,16991,4155,18434,8040,29121,15167,38097,29534,34241";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 38;
                    disclaimerText = "This list is not an official Tenable resellers list nor is affiliated with or in any way associated with Tenable.";
                    break;

                case "thycotic":

                    strQuery += @"9985,10459,10590,21881,14947,25177,16666,15250,29121,6182,37827,15821,15363,22742,17460,8415,7219";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 17;
                    disclaimerText = "This list is not an official Thycotic resellers list nor is affiliated with or in any way associated with Thycotic.";
                    break;

                case "forcepoint":

                    strQuery += @"6025,8049,8264,8381,10860,6017,11458,11485,11605,11780,11813,11861,11862,5451,11978,12009,12010,12060,12090,12118,12120,6640,13270,
                                    13354,13389,13433,13436,13437,13498,13527,13600,13841,13843,13906,13909,20987,6063,21960,21519,28823,28967,29027,29102,29304,23419,
                                    30984,31780,32848";

                    vSession.TechnologyCategory = "Data Security";
                    resultsCount = 48;
                    disclaimerText = "This list is not an official Forcepoint resellers list nor is affiliated with or in any way associated with Forcepoint.";
                    break;

                case "salesforce":

                    strQuery += @"2504,3293,3315,3339,3375,3412,3680,4034,4717,4836,5957,6240,6673,7103,7821,11976,15594,7929,8548,16536,2468,2499,2548,2641,
                                    2679,2694,2708,2776,3246,3281,3306,3433,3525,3614,3615,3638,3666,3701,3776,3833,3834,3911,4354,5147,6392,6498,6656,6732,
                                    6871,7609,7984,8066,8112,8114,8277,8391,8755,9059,16893,10857,5619,18759,20268,20269,11452,20620,21125,21180,24450,24744";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Salesforce resellers list nor is affiliated with or in any way associated with Salesforce.com, Inc.";
                    break;

                case "sugarcrm":
                case "sugar-crm":

                    strQuery += @"2685,6902,6903,4548,27886,3281,4446,7940,9238,9809,15527,17233,11452,10585,15524,24046,12047,14549,16194,8053,23904,26256,
                                    9925,19777,6218,4367,16487,16554,16484,3778,16552,6186,16553,5505,17112,6115,18937,13502,13594,5203,17670,13658,13575,
                                    13800,18504,19076,3906,19406,20231,14261,20931,16062,6187,22521,5109,3944,6219,5151,25784,25785,8301,9865,29002,29530,
                                    30111,13733,19698,6262,3945,18637";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official SugarCRM resellers list nor is affiliated with or in any way associated with SugarCRM.";
                    break;

                case "zoho":

                    strQuery += @"22228,2468,2774,3018,3020,3180,3282,3566,3803,3832,3890,3987,3989,4089,4090,4127,4165,4225,4226,4587,4670,6656,6658,6873,8066,
                                    8068,8112,8901,9238,9325,9991,10598,16132,17047,18757,20269,6084,23532,15257,9307,25907,25950,26033,26510,26611,27055,21951,
                                    3618,27247,27248,19623,19881,19571,10808,28693,29233,29237,29450,28670,29982,25028,25030,29681,30609,31709,31715,2650,17693,
                                    9379,9341";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Zoho resellers list nor is affiliated with or in any way associated with Zoho.";
                    break;

                case "sage":

                    strQuery += @"3680,10713,25466,14206,16206,29696,18593,21234,3455,3525,8112,8753,3278,5051,5455,6100,7166,9606,9743,14068,14407,14492,14976,
                                    15026,15461,15942,16002,16004,16417,16418,16667,5064,20271,21129,18238,31624,9917,13774,14035,14119,7710,16007,18157,18828,
                                    18905,19227,7619,19449,19920,20030,20132,26514,26872,28374,27903,6996,19203,7561,19694,9387,20094,20177,20413,20507,20786,
                                    21532,21533,21534,21535,21536";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Sage resellers list nor is affiliated with or in any way associated with Sage Group plc.";
                    break;

                case "vtiger":

                    strQuery += @"3330,10039,14792,15257,27395,5085,8334,9925,25341,25342,10010,14467,25346,5228,25345,13559,15343,15473,27066,21484,25429,11110";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 22;
                    disclaimerText = "This list is not an official Vtiger resellers list nor is affiliated with or in any way associated with Vtiger.";
                    break;

                case "vte":

                    strQuery += @"6197,6232,6084,5981,11935,5669,5761,5762,5930,6083,6148,5821,5623,5624,5670";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 15;
                    disclaimerText = "This list is not an official VTENEXT resellers list nor is affiliated with or in any way associated with VTENEXT SRL.";
                    break;

                case "suitecrm":

                    strQuery += @"17413,6556,10138,17773,25784,3835,19792,18721,18722,25780,25782,27070,21484,22246,17664,18723,5171";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 17;
                    disclaimerText = "This list is not an official SuiteCRM resellers list nor is affiliated with or in any way associated with SalesAgility.";
                    break;

                case "hubspot":

                    strQuery += @"2658,3087,3306,3615,5303,18027,11452,24551,25471,10705,30791,15521,16145,17553,3588,17202,17294,17331,3952,19408,8608,19956,
                                    20392,10444,22963,22969,17191,17663,17616,17340,17343,17342,17459,15002,27194,25256,17282,17456,17457,17458,17534,17537,
                                    21155,14205,14611,14649,14650,14651,14652,14699,14700,14701,14702,14703,14805,23537,26146,26147,26148,26149,26150,26151,
                                    26235,26236,26337,22246,22247,22249,22251,22356";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Hubspot resellers list nor is affiliated with or in any way associated with HubSpot, Inc.";
                    break;

                case "act":

                    strQuery += @"17693,20942,20794,2631,14169,3461,5310,13716,3504,20943,20944,20945,19849,20946,20948,20949,20950,20951,21036,21038,21039,
                                    21040,21149,21150,21151,21152,21153,21154,21215,21216,21224,21225,21226,21227,21304,21305,21306,21308,21309,21310,21311,
                                    21312,21313,21314,21502,21505,21607,21608,21614,19586,23142,20947,16269,20923,20924,20927,21009,21010,21011,21191,25766,
                                    25767,25850,20791,20792,20793,20958";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 67;
                    disclaimerText = "This list is not an official Act! resellers list nor is affiliated with or in any way associated with Swiftpage ACT! LLC.";
                    break;

                case "bpm-online":

                    strQuery += @"6830,10660,19795,25780,22241,20887,21401,26163,23948,13531,16489,17287,25889,25890,25972,25973,25974,26071,26073,13533,25971,
                                    26265,26162,26066,13440,25886,25968,13443,26067,26068,26165,25885,25853,25955,13576,13607,13534,11452,13502,17670,21151,
                                    21404,22521,21559,21457,21455,21561,29955,21121,21362,21453,21456,21120,21363,21365,21562,21564,21560";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 58;
                    disclaimerText = "This list is not an official Bpm'online resellers list nor is affiliated with or in any way associated with Bpm'online.";
                    break;

                case "1crm":

                    strQuery += @"20826,16489,13592,13593,7115,5983,3420,25658,3472,13440,795";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 11;
                    disclaimerText = "This list is not an official 1CRM resellers list nor is affiliated with or in any way associated with 1CRM Systems Corp.";
                    break;

                case "bitrix24":

                    strQuery += @"3699,29956,26253,19546,7956,18230,18174,18175,18177,18180,18229,18361,18130,18131,18178,18133,3779,18176,18129,32062,26799,4057";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 22;
                    disclaimerText = "This list is not an official Bitrix24 resellers list nor is affiliated with or in any way associated with Bitrix, Inc.";
                    break;

                case "zendesk":

                    strQuery += @"2740,3775,4942,14305,18026,19439,22926,15257,25831,16906,23535,29234,31618,34456,20157,12075,16082,17335,13502,20392,10444,
                                    23890,10399,22646,10644,21918,10068,19882,20684,10022,10515,10640,24757,24758,24759,25630,10070,10347,10752,10925,30383,
                                    10699,26997,3800,35525";

                    vSession.TechnologyCategory = "CRM";
                    resultsCount = 45;
                    disclaimerText = "This list is not an official Zendesk resellers list nor is affiliated with or in any way associated with Zendesk Inc.";
                    break;

                case "sap-business-one":

                    strQuery += @"32240,32238,32149,32148,32051,32050,32049,31983,31982,31981,31903,31902,31901,31900,31751,31749,31658,31657,31656,31521,31442,
                                    31440,31438,31334,31332,31132,31131,31130,31129,31018,31016,30897,30818,30747,30746,30675,30674,30673,30585,30527,30526,30525,
                                    30447,30446,30444,30364,30363,30292,30291,30200,30086,30085,30084,30009,30008,29923,29922,29920,29741,29740,29641,29640,29638,
                                    29540,29473,29471,29355,29354,29353,29270";

                    vSession.TechnologyCategory = "ERP";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official SAP Business One resellers list nor is affiliated with or in any way associated with SAP SE.";
                    break;

                case "infor":

                    strQuery += @"2258,2261,3165,9587,15236,15238,15709,13893,8382,3573,14249,3020,3666,4856,11727,9706,13822,13883,14112,15718,6938,16831,13454,
                                    13889,13924,13994,14078,14251,8011,8182,13486,13547,13652,13774,13775,13854,13886,13888,13922,11251,13925,13958,13960,13961,
                                    13992,13993,6159,14032,14033,14034,14035,14074,14075,14076,14115,14116,14117,14118,14119,14162,14163,14166,14200,14202,14203,
                                    14248,14250,15537,6882,18157";

                    vSession.TechnologyCategory = "ERP";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Infor resellers list nor is affiliated with or in any way associated with Infor.";
                    break;

                case "odoo":

                    strQuery += @"2774,4019,6440,8641,16132,17233,15257,24043,24740,11144,26320,10009,29234,29239,23137,30160,10585,11001,17413,6556,29709,23891,
                                    26540,29527,18564,23206,8569,10010,13892,22646,22649,4092,5109,4020,4093,8779,9251,9300,7956,8457,9865,8507,9422,8878,8876,
                                    28847,28892,28893,28894,29000,29001,29002,29003,8209,8208,29061,29063,8455,8456,8663,9826,29529,29530,29532,29906,29907,29995,
                                    29996,29997,30111";

                    vSession.TechnologyCategory = "ERP";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Odoo resellers list nor is affiliated with or in any way associated with Odoo.";
                    break;

                case "epicor":

                    strQuery += @"3592,13873,18518,11647,6000,3552,21642,22750,26719,5552,3550,3478,21506,3587,3611,3620,3610,3784,8833,31732,6883,11785,
                                    5117,19314,14255,14503,3785,2966,11918,22698,22699,22700,23387,25763,25764,25765,26983,20333,7592,9507,3613,3643,3551,2537";

                    vSession.TechnologyCategory = "ERP";
                    resultsCount = 44;
                    disclaimerText = "This list is not an official Epicor resellers list nor is affiliated with or in any way associated with Epicor Software Corporation.";
                    break;

                case "tally":

                    strQuery += @"3330,27794,9341,23142,31731,9349,27918,10257";

                    vSession.TechnologyCategory = "ERP";
                    resultsCount = 8;
                    disclaimerText = "This list is not an official Tally resellers list nor is affiliated with or in any way associated with Tally Solutions Private Limited.";
                    break;

                case "acumatica":

                    strQuery += @"27055,19917,7561,20885,21536,21683,21684,22200,22393,22749,22866,22998,18933,16480,3446,26719,23788,26999,27003,27093,16866,20231,
                                    22652,22653,22659,22660,8299,22412,32863,35335,2825,36858,37151,25970";

                    vSession.TechnologyCategory = "ERP";
                    resultsCount = 34;
                    disclaimerText = "This list is not an official Acumatica resellers list nor is affiliated with or in any way associated with Acumatica, Inc.";
                    break;

                case "jd-edwards":

                    strQuery += @"3571,7016,28581,11933,6295,3296,20386,7532,35595,23187,8921";

                    vSession.TechnologyCategory = "ERP";
                    resultsCount = 11;
                    disclaimerText = "This list is not an official JD Edwards resellers list nor is affiliated with or in any way associated with Oracle Corporation.";
                    break;

                case "moodle":

                    strQuery += @"26320,5223,5085,22050,25343,14006,17249,21139,14045,19128,16760,14472,16392,14483,14714,31887,17258,16469,14043,14081,17652,
                                    14082,21142,21210,19234,15295,15580,13939,15979,16843,15294,15292,14466,14467,13931,13938,13973,13999,14858,13933,17250,
                                    14002,14041,14007,16769,16394";

                    vSession.TechnologyCategory = "LMS";
                    resultsCount = 46;
                    disclaimerText = "This list is not an official Moodle resellers list nor is affiliated with or in any way associated with Moodle Org.";
                    break;

                case "articulate":

                    strQuery += @"15826,14562,15001,17170,16065,14362,19128,16846,15264,16849,16847,18633,18533,16851,16760,16845,16392,14360,31195,14469,14516,
                                    16844,14858,14997,16852";

                    vSession.TechnologyCategory = "LMS";
                    resultsCount = 25;
                    disclaimerText = "This list is not an official Articulate resellers list nor is affiliated with or in any way associated with Articulate Global, Inc.";
                    break;

                case "totara":

                    strQuery += @"14006,13929,13965,13970,15260,13930,16684,16469,16623,13934,16626,13939,15979,16840,13938,13973,13997,13999,13933,16693,14002,
                                    14004,14001,13968,13974,14462,13966,14003,14000,16394";

                    vSession.TechnologyCategory = "LMS";
                    resultsCount = 30;
                    disclaimerText = "This list is not an official Totara resellers list nor is affiliated with or in any way associated with Totara Learning Solutions.";
                    break;

                case "docebo":

                    strQuery += @"16847,31195,31887,17089,21142";

                    vSession.TechnologyCategory = "LMS";
                    resultsCount = 5;
                    disclaimerText = "This list is not an official Docebo resellers list nor is affiliated with or in any way associated with Docebo S.p.A.";
                    break;

                case "e-front":

                    strQuery += @"17061,21142,14709";

                    vSession.TechnologyCategory = "LMS";
                    resultsCount = 3;
                    disclaimerText = "This list is not an official eFront resellers list nor is affiliated with or in any way associated with Epignosis.";
                    break;

                case "tableau":

                    strQuery += @"2259,2260,24873,2505,2569,2570,2571,2596,2597,2616,2632,2633,2651,2668,2685,2686,2697,2719,2753,2772,2953,2979,3010,3093,3189,
                                    3315,3411,3452,3468,3469,3470,3540,3680,3755,3853,3855,3928,4001,4034,4104,4265,4420,4606,4717,4960,5734,5902,5957,6293,6343,
                                    6900,7422,9442,12145,12176,13469,5170,20911,20912,3860,5271,6616,11955,11501,6611,8548,16329,25742,25942,16438";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Tableau resellers list nor is affiliated with or in any way associated with Tableau Software.";
                    break;

                case "sas":

                    strQuery += @"2373,2374,16047,2372,34282,24873,2384,2391,2392,2397,2398,2399,2400,2412,2421,2422,2440,2441,2523,2588,2978,3220,3485,3646,3680,
                                    3855,4186,4231,5900,5901,5957,7103,11840,12145,12176,15238,15595,20912,20986,11955,16044,25942,16127,16128,4038,16125,25651,
                                    6603,9513,9555,9650,10094,11142,11338,4556,11544,3441,20026,9409,10671,6905,10636,10697,10967,11300,11858,11911,12078,13516,16130";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official SAS resellers list nor is affiliated with or in any way associated with SAS Institute.";
                    break;

                case "collibra":

                    strQuery += @"3263,4184,4185,4186,4373,4418,4419,4420,5902,15594,15930,16037,30752,31462,31537,33232,9105,17786,15934,15931,16038,16537,5008";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 23;
                    disclaimerText = "This list is not an official Collibra resellers list nor is affiliated with or in any way associated with Collibra.";
                    break;

                case "microstrategy":

                    strQuery += @"2560,2571,2698,3337,3432,3680,3853,3957,4139,4464,4960,5336,5734,10658,12176,12177,15592,15595,20751,3860,5532,11955,10907,11501,
                                    25901,14783,9963,10497,10499,10680,6130,11142,4872,11616,13285,13371,13398,13450,13511,13544,13705,16321,13476,12099,6906,21572,
                                    5267,14599";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 48;
                    disclaimerText = "This list is not an official MicroStrategy resellers list nor is affiliated with or in any way associated with MicroStrategy Incorporated.";
                    break;

                case "new-relic":

                    strQuery += @"2668,5791,9901,13365,8829,6015,16430,32348,6181,6080,3307,35232,24704,11394,22668,16214,16115";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 17;
                    disclaimerText = "This list is not an official New Relic resellers list nor is affiliated with or in any way associated with New Relic, Inc.";
                    break;

                case "nuance":

                    strQuery += @"3853,9357,25608,5499,5703,5704,5803,5851,6244,6572,9379,11539,13612,13701,14282,6110,21887,10236,9446,6643,7724,22390,24229,3354,9758";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 25;
                    disclaimerText = "This list is not an official Nuance resellers list nor is affiliated with or in any way associated with Nuance Communications, Inc.";
                    break;

                case "olap":

                    strQuery += @"5336,7372,8052,16516";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 4;
                    disclaimerText = "This list is not an official OLAP resellers list nor is affiliated with or in any way associated with OLAP.";
                    break;

                case "pentaho":

                    strQuery += @"2597,3121,3264,3432,3452,3680,4139,4565,6676,6850,12145,17905,33006,6556,21129,24046,5085,8960,10050,10138,10190,4556,11645,
                                    12133,13307,13544,13920,9032,20826,12099,15602,11769,8268,11856,8267,13229,13287,19389,9031,22351,24128,37633,5652,5109,5151,
                                    7956,3368";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 47;
                    disclaimerText = "This list is not an official Pentaho resellers list nor is affiliated with or in any way associated with Hitachi Data Systems.";
                    break;

                case "sisense":

                    strQuery += @"25426,9963,12099,5905,25630";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 5;
                    disclaimerText = "This list is not an official Sisense resellers list nor is affiliated with or in any way associated with Sisense Inc.";
                    break;

                case "tibco":

                    strQuery += @"2257,2261,2569,2873,2978,3292,4067,5694,7103,12176,7163,6656,32519,5454,13697,10616,6130,4368,4501,11546,15702,17453,10697,
                                    10967,16144,16145,20626,20703,20704,20706,20921,20686,2717,21000,21001,21130,21131,21186,2552,33393,14106,16901,31462";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 43;
                    disclaimerText = "This list is not an official TIBCO resellers list nor is affiliated with or in any way associated with TIBCO Software Inc.";
                    break;

                case "qpr":

                    strQuery += @"10150,5403,9972,9973,9974,9976,10065,10105,10151,10214,10215,10216,10217,10218,10260,10261,10263,10311,10394,10396,10639,13852,31875";

                    vSession.TechnologyCategory = "Perfromance Management";
                    resultsCount = 23;
                    disclaimerText = "This list is not an official QPR resellers list nor is affiliated with or in any way associated with QPR Software.";
                    break;

                case "qlik":

                    strQuery += @"5546,24873,2401,2569,2652,2753,2978,3165,3293,3315,3413,3453,3485,3486,3487,3680,3853,3957,4033,4186,4231,4717,4960,5571,5692,
                                    5733,5899,5900,5901,5957,6402,8424,10769,11316,12145,12176,14059,15315,15860,3860,11955,6059,11501,25415,16329,25608,25609,
                                    25610,13893,14206,25685,25686,25738,25739,25740,25741,25742,25826,25827,25828,25830,25899,25900,25942,16438,4038,2765,3573,24703,28579";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 70;
                    disclaimerText = "This list is not an official Qlik resellers list nor is affiliated with or in any way associated with QlikTech International AB.";
                    break;

                case "jedox":

                    strQuery += @"8052,13746,11650,11688,11689,8183,11729,11769,8268,11795,11796,8011,8383,8182,8382,12108,3724,8267,12197,13228,13229,8227,13230,
                                    13231,13232,8226,13309,13310,4565,13346,13373,13374,13421,13452,13453,13487,13548,13549,13550,13586,8326,7737,13654,13684,13708,
                                    15601,11616,25484,25485,27986,35986,2686,5170,17345,7933,23170";

                    vSession.TechnologyCategory = "Perfromance Management";
                    resultsCount = 56;
                    disclaimerText = "This list is not an official Jedox resellers list nor is affiliated with or in any way associated with Jedox AG.";
                    break;

                case "board":

                    strQuery += @"2719,3165,3514,3853,4606,5005,15593,25743,8382,10497,13285,13994,14695,10697,9388,11103,11689,11823,11825,13228,15537,22240,16638,20075,21248,6083,13445,6819,21441";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 29;
                    disclaimerText = "This list is not an official Board resellers list nor is affiliated with or in any way associated with Board International.";
                    break;

                case "birst":

                    strQuery += @"2569,2587,2632,2978,8980,12145,3860,27241,11252,8108,13544,11285,22147,10697,10967,11251,14763,22353,3765,27816,8983";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 21;
                    disclaimerText = "This list is not an official Birst resellers list nor is affiliated with or in any way associated with Birst, Inc.";
                    break;

                case "xlcubed":

                    strQuery += @"3119,11518,11446,11475,11476,11507,11547,11548,11586,11587,11649,3066,11650";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 13;
                    disclaimerText = "This list is not an official XLCubed resellers list nor is affiliated with or in any way associated with XLCubed.";
                    break;

                case "alteryx":

                    strQuery += @"2597,2632,2651,2652,2669,2719,2953,2979,3411,3470,3486,3487,4001,4420,4606,5694,13469,20912,6616,25415,16329,2765,3634,28581,
                                    9651,18687,17877,7106,13684,9931,11796,13289,5845,18199,19391,19450,19527,2700,28879,32024,32222,35912,28594,14208,3368,
                                    29485,30626,30627,30752,32348,32350,5905,6904,9241,35598";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 55;
                    disclaimerText = "This list is not an official Alteryx resellers list nor is affiliated with or in any way associated with Alteryx.";
                    break;

                case "yellowfin":

                    strQuery += @"3541,4033,5005,5084,5336,5271,10907,35457,9409,11650,16032,24708,3465,11083,17280,8523";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 16;
                    disclaimerText = "This list is not an official Yellowfin resellers list nor is affiliated with or in any way associated with Yellowfin.";
                    break;

                case "anaplan":

                    strQuery += @"5122,25741,16993,4038,14694,14450,8108,9465,14421,13994,14554,14695,15082,15083,6097,11049,12048,13375,13773,14333,14334,14382,
                                    14417,14419,14449,14451,14498,14499,14552,14553,14642,14762,14763,14764,14801,14802,14803,14935,14936,14984,14985,15031,15033,
                                    16081,17150,20369,3977,33949,35986";

                    vSession.TechnologyCategory = "Perfromance Management";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official Anaplan resellers list nor is affiliated with or in any way associated with Anaplan, Inc.";
                    break;

                case "adaptive-insights":

                    strQuery += @"2260,5957,5271,2594,6871,10497,14421,7206,13420,13287,13375,15857,15897,16755,18826,18827,18828,18905,18906,18907,18996,18997,18998,
                                    19100,19101,19102,19122,19123,19226,19227,19302,19389,19390,19391,19448,19449,19450,19527,19528,19529,19577,19578,19727,19818,
                                    19819,19820,19917,19918,19919,19920,20028,20029,20030,20133,20219,20368,20369,20370,20461,20463,20464,20549,20550,23063,2486";

                    vSession.TechnologyCategory = "Perfromance Management";
                    resultsCount = 65;
                    disclaimerText = "This list is not an official Adaptive Insights resellers list nor is affiliated with or in any way associated with Adaptive Insights.";
                    break;

                case "cxo-software":

                    strQuery += @"12077,14554,15663,16516,16755,18998,27898";

                    vSession.TechnologyCategory = "Performance Management";
                    resultsCount = 7;
                    disclaimerText = "This list is not an official CXO Software resellers list nor is affiliated with or in any way associated with insightsoftware Inc.";
                    break;

                case "longview":

                    strQuery += @"6905,10848,15469,15539,15726,15771,15773,16006,16008";

                    vSession.TechnologyCategory = "Performance Management";
                    resultsCount = 9;
                    disclaimerText = "This list is not an official Longview resellers list nor is affiliated with or in any way associated with Longview.";
                    break;

                case "arcplan":

                    strQuery += @"3165,3485,11344,15602,15724,8011,14335,15337,15406,15467,15537,15601,15725,15772,16005,3167,5489";

                    vSession.TechnologyCategory = "Business Intelligence";
                    resultsCount = 17;
                    disclaimerText = "This list is not an official Arcplan resellers list nor is affiliated with or in any way associated with Arcplan.";
                    break;

                case "avepoint":

                    strQuery += @"36834,29705,6230,33193,36470,2946,17382,15456,5370,6351,8957,8963,13911,8119,6493,21610,38133,5616,5225,8451,10982,31933,8607,
                                    5828,26835,22625,17790";

                    vSession.TechnologyCategory = "Cloud Management";
                    resultsCount = 27;
                    disclaimerText = "This list is not an official AvePoint resellers list nor is affiliated with or in any way associated with AvePoint, Inc.";
                    break;

                case "bittitan":

                    strQuery += @"2988,33113,17382,6493,8064";

                    vSession.TechnologyCategory = "Cloud Management";
                    resultsCount = 5;
                    disclaimerText = "This list is not an official BitTitan resellers list nor is affiliated with or in any way associated with BitTitan, Inc.";
                    break;

                case "bmc-software":

                    strQuery += @"3291,4067,5900,6553,8637,9841,10656,13469,13785,15115,15315,15394,34766,8860,4640,19597,27175,24165,6700,8352,6259,10644,36320,
                                    19936,21000,10695,28393,16525,16527,14902,14901,16212,13518,16153,5737,2911,19892,3905";

                    vSession.TechnologyCategory = "Cloud Management";
                    resultsCount = 38;
                    disclaimerText = "This list is not an official BMC Software resellers list nor is affiliated with or in any way associated with BMC Software, Inc.";
                    break;

                case "ibm":

                    strQuery += @"2253,2255,2329,37500,24873,2392,2397,2401,2461,2504,2523,2560,2561,2569,2586,2587,2588,2616,2719,2750,2751,2773,2873,2889,2890,2954,
                                    3010,3061,3122,3164,3165,3191,3220,3222,3223,3224,3292,3315,3337,3375,3388,3412,3468,3485,3486,3625,3646,3679,3680,3853";

                    vSession.TechnologyCategory = "Cloud Management";
                    resultsCount = 50;
                    disclaimerText = "This list is not an official IBM resellers list nor is affiliated with or in any way associated with IBM.";
                    break;

                case "layer2":

                    strQuery += @"5378,38154,5883,15869,8076,6421,5141,5293,8119,3980,5227,8118,8404,8451,6104,5153,2895,8556";

                    vSession.TechnologyCategory = "Cloud Management";
                    resultsCount = 18;
                    disclaimerText = "This list is not an official Layer2 resellers list nor is affiliated with or in any way associated with Layer 2 GmbH.";
                    break;

                case "manageengine":

                    strQuery += @"7104,8101,6286,38646,27677,9963,20826,18954,8040,36257,6146,6234,23176,21585,9923,36348,36084,15012,32886,12110,20724";

                    vSession.TechnologyCategory = "Cloud Management";
                    resultsCount = 21;
                    disclaimerText = "This list is not an official ManageEngine resellers list nor is affiliated with or in any way associated with Zoho Corp.";
                    break;

                case "riverbed":

                    strQuery += @"2264,2288,2535,2750,2954,3061,3900,8009,8179,8381,8800,8888,9232,9317,9532,9533,9983,10274,4448,10658,10935,11864,12035,12121,
                                    13330,3743,13415,13471,13497,13499,5435,13530,13810,13840,13841,13845,7917,13874,13909,14104,15113,15238,6063,18573,30981,
                                    20962,14331,14547,16862";

                    vSession.TechnologyCategory = "Cloud Management";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official Riverbed resellers list nor is affiliated with or in any way associated with Riverbed Technology.";
                    break;

                case "solarwinds":

                    strQuery += @"2938,6345,6555,7753,8852,11605,11747,12059,13497,5435,20740,22542,7049,15332,6286,19493,35455,38646,10667,15525,11297,27592,29679,
                                    14069,14495,14982,18641,8040,31494,6016,13715,3871,21611,21005,5276,6182,38047,29327,17325,22973,24074";

                    vSession.TechnologyCategory = "Cloud Management";
                    resultsCount = 41;
                    disclaimerText = "This list is not an official SolarWinds resellers list nor is affiliated with or in any way associated with SolarWinds Worldwide, LLC.";
                    break;

                case "suse":

                    strQuery += @"3011,3164,6555,6756,6850,6901,10234,10592,11458,15238,15518,16206,5921,37617,12100,37867,16272,6645,6717,7384,6425,6644,5442,6486,8336";

                    vSession.TechnologyCategory = "Cloud Management";
                    resultsCount = 25;
                    disclaimerText = "This list is not an official SuSe resellers list nor is affiliated with or in any way associated with SuSe.";
                    break;

                case "acquia":

                    strQuery += @"4035,3087,34214,9960,11473,16637,20893,20013,16548,3076,21114";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 11;
                    disclaimerText = "This list is not an official Acquia resellers list nor is affiliated with or in any way associated with Acquia, Inc.";
                    break;

                case "atlassian":

                    strQuery += @"3572,7821,22673,3615,25613,33759,8601,11900,20367,6874,13991,3287,3906,3935,2838,7893,34598,16158,22360,3309,2788,38476,23820,
                                    10848,11474,18199,28591,17608,16525,19072,3307,19604,3286,16558,3260,16159,2842,26997,8949,2843,16473,3308,19549,16114,3936,
                                    3800,21830";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 47;
                    disclaimerText = "This list is not an official Atlassian resellers list nor is affiliated with or in any way associated with Atlassian.";
                    break;

                case "chef":

                    strQuery += @"3852,5606,11782,8216,32348,36014,34156";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 7;
                    disclaimerText = "This list is not an official Chef resellers list nor is affiliated with or in any way associated with Chef.";
                    break;

                case "docker":

                    strQuery += @"3467,3852,5606,5903,6403,6676,6756,9232,10589,25536,25539,24574,5854,34116,35280,9960,20702,16637,8216,29522,15344,11319,
                                    32348,34075,6717,6029,6112,10640,7223,10637,32871,17253,34229,14949,34156";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 35;
                    disclaimerText = "This list is not an official Docker resellers list nor is affiliated with or in any way associated with Docker Inc.";
                    break;

                case "drupal":

                    strQuery += @"3375,5605,4531,34214,4530,19722,24046,15138,15199,8779,36965,37650,20000,11470,20002,20893,20001,20003,20004,20005,20102,20103,
                                    20105,20013,4543,11769,9641,13949,3836";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 29;
                    disclaimerText = "This list is not an official Drupal resellers list nor is affiliated with or in any way associated with Drupal.";
                    break;

                case "filemaker":

                    strQuery += @"19222,25913,25029,13791,8655,6249,13733,10319,13875,22879,16087,20138,6153,25324,23759,13876,10818,13764,13765,13766,13789,
                                    13790,13877,13913,21648,21649,21650,21651,21652,22872,22873,22874,22876,22880,22881,22883,22884";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 37;
                    disclaimerText = "This list is not an official FileMaker resellers list nor is affiliated with or in any way associated with Claris International Inc.";
                    break;

                case "kony":

                    strQuery += @"3221,4033,5083,2625,3638,7973,6181,14485,16441,4009,4010";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 11;
                    disclaimerText = "This list is not an official Kony resellers list nor is affiliated with or in any way associated with Kony, Inc.";
                    break;

                case "optimizely":

                    strQuery += @"27241,6512,22147,2862,2864,32168,26243,2942,3102,3038,11536,4361";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 12;
                    disclaimerText = "This list is not an official Optimizely resellers list nor is affiliated with or in any way associated with Optimizely.";
                    break;

                case "teamviewer":

                    strQuery += @"11791,34095,26576,34201,33505,33506,33511,33604,33736,33737,34009,34094,35222,33434,6167,36673,33320,6331";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 18;
                    disclaimerText = "This list is not an official TeamViewer resellers list nor is affiliated with or in any way associated with TeamViewer.";
                    break;

                case "tempo":

                    strQuery += @"3935,2788,38208,2843,22760,3259,16330,16335";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 8;
                    disclaimerText = "This list is not an official Tempo resellers list nor is affiliated with or in any way associated with Tempo.";
                    break;

                case "wordpress":

                    strQuery += @"2685,3404,4444,16186,3472,11332,11926,4394";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 8;
                    disclaimerText = "This list is not an official WordPress resellers list nor is affiliated with or in any way associated with WordPress.";
                    break;

                case "workflowmax":

                    strQuery += @"37552,15188,19778,19570,19620,33243,34695,36150,4043,20682,20972,21086,21433,21434,21436,21856,21857,24516,4042,20796,22401,22399";

                    vSession.TechnologyCategory = "Developer Tools";
                    resultsCount = 22;
                    disclaimerText = "This list is not an official WorkflowMax resellers list nor is affiliated with or in any way associated with WorkflowMax.";
                    break;

                case "acronis":

                    strQuery += @"3852,6555,8533,9441,7216,5324,16803,22226,8072,18272,32482,32570,37093,38646,3455,4860,7609,10667,17591,11006,14273,27975,11113,
                                    29149,23132,30988,9971,33200,34391,35706,20295,15806,16895,18344,25016,7611,14846,18416,16839,33737,19626,5876,7382,8273,
                                    5684,6919,14505";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 47;
                    disclaimerText = "This list is not an official Acronis resellers list nor is affiliated with or in any way associated with Acronis International GmbH.";
                    break;

                case "altaro":

                    strQuery += @"6162,10459,29097,29884,19500,35631,36834,3455,18898,21089,30048,18394,18463,22144,16269,8119,34193,6328,5973,30191,19199,19559,24845";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 23;
                    disclaimerText = "This list is not an official Altaro resellers list nor is affiliated with or in any way associated with Altaro.";
                    break;

                case "arcserve":

                    strQuery += @"2504,2908,3011,6555,7104,8691,10234,11456,11569,11951,13243,13385,15392,23705,36342,33552,36467,7623,6110,18901,18902,27271,
                                    36356,6317,33736,20335,28987,18699,28886,34480,26996,939,36985,17068,32882,32988";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 36;
                    disclaimerText = "This list is not an official Arcserve resellers list nor is affiliated with or in any way associated with Arcserve.";
                    break;

                case "backblaze":

                    strQuery += @"25913,28085,28195,37701,4387,9008,2797";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 7;
                    disclaimerText = "This list is not an official Backblaze resellers list nor is affiliated with or in any way associated with Backblaze.";
                    break;

                case "backupassist":

                    strQuery += @"2249,7823,6044,16417,5683,6120,4553,20657,5722";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 9;
                    disclaimerText = "This list is not an official BackupAssist resellers list nor is affiliated with or in any way associated with BackupAssist.";
                    break;

                case "backupify":

                    strQuery += @"2489,2546,2547,2581,2594,2641,2854,2957,2973,4050,4986,9539,10323,16281,22233,9264,26033,11940";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 18;
                    disclaimerText = "This list is not an official Backupify resellers list nor is affiliated with or in any way associated with Backupify.";
                    break;

                case "carbonite":

                    strQuery += @"11709,33009,38365,4127,9069,30540,30784,34638,37555,37695,38570,38710,5371,12100,13917,14886,21039,22657,4741,7724";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 20;
                    disclaimerText = "This list is not an official Carbonite resellers list nor is affiliated with or in any way associated with Carbonite, Inc.";
                    break;

                case "commvault":

                    strQuery += @"34282,2461,2751,2937,3061,3222,3754,6096,6900,7104,7205,7753,8691,8888,8889,8892,8934,8935,8981,9017,9019,9082,9083,9126,9127,
                                    9130,9178,9232,9273,9274,9316,9317,9398,9400,9440,9441,9494,9495,9532,9533,9535,9587,9588,9589,10234,10769,11189,11485";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 48;
                    disclaimerText = "This list is not an official CommVault resellers list nor is affiliated with or in any way associated with CommVault.";
                    break;

                case "datto":

                    strQuery += @"8178,8473,9842,11709,19873,35564,3257,9239,10667,11196,16187,16965,17140,17590,18031,21180,23252,24638,20492,30869,30936,32575,
                                    33117,31522,36704,37452,30563,38371,38491,5181,6101,10335,10609,12100,15402,15805,15942,34998,16437,37279,38028,38653,15071,
                                    13715,26639,21611";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 46;
                    disclaimerText = "This list is not an official Datto resellers list nor is affiliated with or in any way associated with Datto, Inc.";
                    break;

                case "evault":

                    strQuery += @"6508,7262,5378,15824,22446,6270,7216,17020";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 8;
                    disclaimerText = "This list is not an official eVault resellers list nor is affiliated with or in any way associated with Carbonite.";
                    break;

                case "spanning":

                    strQuery += @"2489,2988,3041,3151,3832,3888,4017,4088,4354,6656,7393,9059,9069,9135,10866,18898,10392,29150,31618,11999,2892";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 21;
                    disclaimerText = "This list is not an official Spanning resellers list nor is affiliated with or in any way associated with Spanning Cloud Apps, LLC.";
                    break;

                case "veeam":

                    strQuery += @"2448,2586,2751,2937,2938,3011,3062,5264,5903,6162,6203,7018,7102,7753,7822,8142,8224,8472,8532,8690,8800,8888,9018,9019,9087,9127,
                                    9273,9274,9400,9440,9497,9589,10077,10233,10411,10577,10592,10655,10658,10769,10813,10814,11070,11165,11317,11420,11456,11672,11713";

                    vSession.TechnologyCategory = "Backup & Restore";
                    resultsCount = 49;
                    disclaimerText = "This list is not an official Veeam resellers list nor is affiliated with or in any way associated with Veeam.";
                    break;                

                case "act-on":

                    strQuery += @"3431,3433,14205,10399,25505,4094,17197,15527,15524,6218,17112,13502,13658,17717,19076,3906,19846,17677,20370";

                    vSession.TechnologyCategory = "Marketing Automation";
                    resultsCount = 19;
                    disclaimerText = "This list is not an official Act-On resellers list nor is affiliated with or in any way associated with Act-On Software, Inc.";
                    break;

                case "activecampaign":

                    strQuery += @"17287,20486,26150,20887,36175,19037,19802,26980,26981,20420,20509,20511,11110,19803,19804,19805,19806,19801";

                    vSession.TechnologyCategory = "Marketing Automation";
                    resultsCount = 18;
                    disclaimerText = "This list is not an official ActiveCampaign resellers list nor is affiliated with or in any way associated with ActiveCampaign.";
                    break;

                case "clickdimensions":

                    strQuery += @"14452,14450,35638,21075,3503,19849,13688,14384,14387,14388,14389,14426,14454,14455,14852,7162,38614,6005,2873,6096,3573,6438,26064,
                                    7623,6678,8895,3028,3616";

                    vSession.TechnologyCategory = "Marketing Automation";
                    resultsCount = 28;
                    disclaimerText = "This list is not an official ClickDimensions resellers list nor is affiliated with or in any way associated with ClickDimensions.";
                    break;

                case "marketo":

                    strQuery += @"4501,3861,16522,13659,2864,32168,32348,25256,26147,20013,36297,16555,29985,15084,3038,4233,24704,18721,2892,17197,17674,17680,16457,9368";

                    vSession.TechnologyCategory = "Marketing Automation";
                    resultsCount = 24;
                    disclaimerText = "This list is not an official Marketo resellers list nor is affiliated with or in any way associated with Adobe.";
                    break;

                #endregion

                #region default

                default:

                    strQuery += "0";
                    vSession.TechnologyCategory = "";
                    resultsCount = 0;
                    disclaimerText = "";

                    break;

                #endregion
            }
            */
            #endregion

            //strQuery += ") ";

            strQuery += " GROUP BY Elio_users.id, company_name, billing_type, address, city, company_type, Elio_users.sysdate";
            //strQuery += " GROUP BY Elio_users.id, company_name, billing_type ORDER BY billing_type DESC, r desc";

            strQuery += @") ";

            //            strQuery += @",total_views
            //                            as
            //                            (";

            //            strQuery += @"Select company_id,sum(views) as totalViews 
            //	                      from Elio_companies_views nolock	
            //	                      GROUP BY company_id";

            //            strQuery += ")";

            string strCount = strQuery + @" select count(id) as count
                            from MyDataSet";

            DataTable tblCount = session.GetDataTable(strCount);
            if (tblCount.Rows.Count > 0)
            {
                resultsCount = Convert.ToInt32(tblCount.Rows[0]["count"]);
            }

            strQuery += @" select id,row_index
                            --,r,billing_type
                            --,isnull(totalViews,0) as totalViews 
                            ,address
                            ,city
                            ,billing_type
                            ,company_type
                            from MyDataSet  
                            --left join total_views on total_views.company_id = MyDataSet.id
                           where row_index between " + fromPageIndex + " and " + toPageIndex + " " +
                            " ORDER BY sysdate DESC";
                      
            //select * 
            //from MyDataSet
            //where row_index between " + fromPageIndex + " and " + toPageIndex + " " +
            //" ORDER BY billing_type DESC, r desc";

            return strQuery;
        }

        public static int GetSearchResultsCount(string companyType, string industryId, string verticalId, string category, string programId, string marketId, string apiId, string region, string country, string state, string city, string companyName, ElioSession vSession, DBSession session)
        {
            if (companyType.ToLower().StartsWith("vendors"))
                companyType = Types.Vendors.ToString();
            else
                companyType = EnumHelper.GetDescription(Types.Resellers).ToString();

            string strQuery = @";WITH MyDataSet
                                 AS
                                 (";

            strQuery += @"Select COUNT(Elio_users.id) as id
                        from Elio_users ";

            if (industryId != "0")
            {
                strQuery += "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id ";
                vSession.IndustryViewState = industryId;
            }

            if (verticalId != "0")
            {
                strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                vSession.VerticalViewState = verticalId;
            }
            else
            {
                if (category != "")
                    strQuery += @"inner join Elio_users_registration_products urp
                                        on urp.user_id = Elio_users.id
                                    inner join Elio_registration_products rp
                                        on rp.id = urp.reg_products_id " + Environment.NewLine;
            }

            if (programId != "0")
            {
                strQuery += "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id ";
                vSession.PartnerViewState = programId;
            }

            if (marketId != "0")
            {
                strQuery += "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id ";
                vSession.MarketViewState = marketId;
            }

            if (apiId != "0")
            {
                strQuery += "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id ";
                vSession.ApiViewState = apiId;
            }

            if (companyName != string.Empty)
            {
                vSession.CompanyNameViewState = companyName;
            }

            strQuery += "where Elio_users.is_public=1 and account_status=1 ";
            strQuery += "and company_type like '" + companyType + "' ";
            strQuery += (industryId != "0") ? "and industry_id=" + Convert.ToInt32(industryId) + " " : " ";
            strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : (category != "") ? " and rp.description = '" + category + "' " : " ";
            strQuery += (programId != "0") ? "and partner_id=" + Convert.ToInt32(programId) + " " : " ";
            strQuery += (marketId != "0") ? "and market_id=" + Convert.ToInt32(marketId) + " " : " ";
            strQuery += (apiId != "0") ? "and api_id=" + Convert.ToInt32(apiId) + " " : " ";
            strQuery += (region != "") ? "and company_region = '" + region + "' " : " ";
            strQuery += (country != "") ? "and country='" + country + "' " : " ";
            strQuery += (state != "") ? "and state = '" + state + "' " : " ";
            strQuery += (city != "") ? "and city = '" + city + "' " : " ";
            strQuery += (companyName != string.Empty) ? "and lower (company_name) like '" + companyName + "%' " : " ";
            strQuery += " GROUP BY Elio_users.id";  //, company_name, billing_type";
            //strQuery += " GROUP BY Elio_users.id, company_name, billing_type ORDER BY billing_type DESC, r desc";

            strQuery += @")

                        select count(id) as count
                        from MyDataSet";

            DataTable tbl = session.GetDataTable(strQuery);

            return Convert.ToInt32(tbl.Rows[0]["count"]);
        }

        public static string GetSEOSearchResultsCount(string companyType, string verticalId, string country, string city, ElioSession vSession, DBSession session)
        {
            GlobalMethods.ClearCriteriaSession(vSession, false);

            string strQuery = @"Select COUNT(distinct(Elio_users.id)) as count from Elio_users ";

            if (verticalId != "0")
            {
                strQuery += "inner join Elio_users_sub_industries_group_items on elio_users.id=Elio_users_sub_industries_group_items.user_id ";
                vSession.VerticalViewState = verticalId;
            }
            if (country != "")
            {
                strQuery += "inner join Elio_countries on Elio_countries.country_name = Elio_users.country ";
            }
 
            strQuery += "where Elio_users.is_public=1 and account_status=1 ";            
            strQuery += (verticalId != "0") ? "and Elio_users_sub_industries_group_items.sub_industry_group_item_id=" + Convert.ToInt32(verticalId) + " " : " ";
            strQuery += (country != "") ? "and country='" + country + "' " : " ";
            strQuery += (city != "") ? "and city = '" + city + "' " : " ";

            if (companyType != "")
            {
                if (companyType.StartsWith("vendors"))
                    companyType = "Vendors";
                else
                    companyType = "Channel Partners";

                strQuery += "and company_type = '" + companyType + "' ";
            }

            return strQuery;
        }

        public static bool AllowPaymentProccess(ElioUsers user, bool isRegistration, ref bool showBtn, ref bool showModal, DBSession session)
        {
            if (ConfigurationManager.AppSettings["ActivatePayment"].ToString() == "true")
            {
                if (user == null)
                {
                    showBtn = true;
                    showModal = false;

                    return true;
                }
                else
                {
                    if (!isRegistration)
                    {
                        if (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
                        {
                            showBtn = false;
                            showModal = false;

                            return false;
                        }
                        else
                        {
                            if (user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                            {
                                showModal = true;
                                showBtn = false;

                                return true;
                            }
                            else
                            {
                                showBtn = false;
                                showModal = false;

                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            showModal = true;
                            showBtn = false;

                            return true;
                        }
                        else
                        {
                            showBtn = false;
                            showModal = false;

                            return false;
                        }
                    }
                }

                //if ((user != null) && (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) && (user.BillingType == Convert.ToInt32(BillingType.Freemium))) //|| (user.BillingType == Convert.ToInt32(BillingType.Premium) && (Sql.HasExpiredOrder(user.Id, session) || Sql.UserHasActiveOrder(user.Id, session)))))
                //{
                //    showModal = true;
                //    showBtn = false;
                //}
                //else
                //{
                //    showModal = false;
                //    showBtn = true;
                //}

                //return true;
            }
            else
            {
                showBtn = false;
                showModal = false;

                return false;
            }
        }

        public static void FixUserCriteriaSelection(int userId, int criteriaId, string criteriaValue, bool selectedItem, bool hasMultiValues, ref string selectedValues, DBSession session)
        {
            DataLoader<ElioUsersCriteria> criteriaLoader = new DataLoader<ElioUsersCriteria>(session);

            ElioUsersCriteria criteria = new ElioUsersCriteria();

            if (hasMultiValues)
            {
                criteria = Sql.GetUserAlgorithmCriteriaValue(userId, criteriaId, criteriaValue, session);
            }
            else
            {
                criteria = Sql.GetUserCriteriaByCriteriaId(userId, criteriaId, session);
            }

            if (selectedItem)
            {
                if (criteria == null)
                {
                    criteria = new ElioUsersCriteria();

                    criteria.UserId = userId;
                    criteria.CriteriaId = criteriaId;
                    criteria.CriteriaValue = criteriaValue;
                    criteria.SysDate = DateTime.Now;
                    criteria.LastUpdated = DateTime.Now;
                    criteriaLoader.Insert(criteria);
                }
                else
                {
                    criteria.CriteriaValue = criteriaValue;
                    criteria.LastUpdated = DateTime.Now;

                    criteriaLoader.Update(criteria);
                }

                selectedValues = criteria.CriteriaValue;
            }
            else
            {
                if (criteria != null)
                {
                    if (hasMultiValues)
                    {
                        Sql.DeleteUserCriteriaValue(userId, criteriaId, criteriaValue, session);
                    }
                    else
                    {
                        Sql.DeleteUserCriteria(userId, criteriaId, session);
                    }
                }
            }
        }

        public static bool ShowControlByUser(ElioUsers user, DBSession session)
        {
            return ((user != null) && (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) || Sql.IsUserAdministrator(user.Id, session))) ? true : false;
        }

        public static bool ShowPremiumBtn(ElioUsers user, DBSession session)
        {
            return ((user != null) && (user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))) ? true : false;
        }

        public static string ReturnSearchTopResultsQueryString(ElioUsers user, bool isForDashBoardView,  DBSession session)
        {
            string topResults = "";

            ElioUsersFeatures userFeatures = new ElioUsersFeatures();

            if (user == null || user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                userFeatures = Sql.GetFeaturesbyUserType(Convert.ToInt32(BillingTypePacket.FreemiumPacketType), session);
                if (userFeatures != null)
                {
                    topResults = (userFeatures.HasSearchLimit == 1) ? (isForDashBoardView) ? " TOP " + userFeatures.TotalSearchResults * 3 + " " : " TOP " + userFeatures.TotalSearchResults + " " : "";
                }
            }

            return topResults;
        }

        public static int TopResultsQueryString(ElioUsers user, bool isForDashBoardView, DBSession session)
        {
            int topResults = 0;

            ElioUsersFeatures userFeatures = new ElioUsersFeatures();

            if (user != null && Sql.IsUserAdministrator(user.Id, session))
            {
                return topResults;
            }
            else if (user == null || user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                userFeatures = Sql.GetFeaturesbyUserType(Convert.ToInt32(BillingTypePacket.FreemiumPacketType), session);
                if (userFeatures != null)
                {
                    topResults = (userFeatures.HasSearchLimit == 1) ? (isForDashBoardView) ? userFeatures.TotalSearchResults * 3 : userFeatures.TotalSearchResults : 0;
                }
            }

            return topResults;
        }

        public static string FindPrefixByCountryName(string countryName, DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);

            ElioCountries country = loader.LoadSingle("Select * from Elio_countries where country_name=@country_name", DatabaseHelper.CreateStringParameter("@country_name", countryName));

            return (country != null) ? country.Prefix : string.Empty;
        }

        public static ElioUsersMessages InsertCompanyMessage(int senderId, string senderEmail, int receiverId, string receiverEmail, string receiverOfficialEmail, string subject, string companyMessage, DBSession session)
        {
            ElioUsersMessages message = new ElioUsersMessages();

            message.SenderUserId = senderId;
            message.SenderEmail = senderEmail;
            message.ReceiverUserId = receiverId;
            message.ReceiverEmail = receiverEmail;
            message.ReceiverOfficialEmail = receiverOfficialEmail;
            message.Subject = subject;
            message.Message = companyMessage;
            message.Sysdate = DateTime.Now;
            message.IsNew = 1;
            message.Deleted = 0;
            message.Sent = 0;
            message.LastUpdated = DateTime.Now;

            DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
            loader.Insert(message);

            return message;
        }

        public static void FixUserEmailAndPacketStatusFeatutes(ElioUsersMessages message, DBSession session)
        {
            if (message != null)
            {
                #region Update as Sent Email

                message.Sent = 1;
                message.LastUpdated = DateTime.Now;

                DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
                loader.Update(message);

                #endregion

                #region Fix Company Packet Status For Messages

                ElioUserPacketStatus packet = Sql.GetUserPacketStatusFeatures(message.SenderUserId, session);
                if (packet != null)
                {
                    if (packet.AvailableMessagesCount > 0)
                    {
                        packet.AvailableMessagesCount--;
                        packet.LastUpdate = DateTime.Now;

                        DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                        loader1.Update(packet);
                    }
                    else
                    {
                        Logger.Error("User {0} with no available messages was able to send email at {1} but he could not.", packet.UserId.ToString(), DateTime.Now.ToString());
                    }
                }
                else
                {
                    Logger.Error("!!! ERROR !!! User {0} has no packet status features at {1}.", packet.UserId.ToString(), DateTime.Now.ToString());
                }

                #endregion
            }
        }

        public static void FixUserEmailAndPacketStatusFeatutes(ElioUsersMessages message, ElioUserPacketStatus senderPacketStatusFeatures, DBSession session)
        {
            if (message != null)
            {
                #region Update as Sent Email

                message.Sent = 1;
                message.LastUpdated = DateTime.Now;

                DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
                loader.Update(message);

                #endregion

                #region Fix Company Packet Status For Messages

                if (senderPacketStatusFeatures != null)
                {
                    if (senderPacketStatusFeatures.AvailableMessagesCount > 0)
                    {
                        senderPacketStatusFeatures.AvailableMessagesCount--;
                        senderPacketStatusFeatures.LastUpdate = DateTime.Now;

                        DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                        loader1.Update(senderPacketStatusFeatures);
                    }
                    else
                    {
                        Logger.Error("User {0} with no available messages was able to send email at {1} but he could not.", senderPacketStatusFeatures.UserId.ToString(), DateTime.Now.ToString());
                    }
                }
                else
                {
                    Logger.Error("!!! ERROR !!! User {0} has no packet status features at {1}.", senderPacketStatusFeatures.UserId.ToString(), DateTime.Now.ToString());
                }

                #endregion
            }
        }

        public static void FixUserLeadsAndPacketStatusFeatutes(ElioCompaniesViewsCompanies companiesViewsCompany, ElioUsers viewedUser, ElioUsers interestedUser, int safeModeIndex, DBSession session)
        {
            if (safeModeIndex <= 1)
            {
                safeModeIndex++;
                bool leadHasValueForViewedCompany = false;

                if (viewedUser.CompanyType == Types.Vendors.ToString())
                {
                    if (interestedUser.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                    {
                        leadHasValueForViewedCompany = true;
                    }
                }
                else if (viewedUser.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                {
                    if (interestedUser.CompanyType == Types.Vendors.ToString())
                    {
                        leadHasValueForViewedCompany = true;
                    }
                }

                if (leadHasValueForViewedCompany)
                {
                    #region Fix Company Packet Status For Leads

                    ElioUserPacketStatus packet = Sql.GetUserPacketStatusFeatures(viewedUser.Id, session);
                    if (packet != null)
                    {
                        packet.AvailableLeadsCount--;
                        packet.LastUpdate = DateTime.Now;

                        DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                        loader1.Update(packet);

                        if (packet.AvailableLeadsCount < 0)
                        {
                            companiesViewsCompany.CanBeViewed = 0;
                            companiesViewsCompany.LastUpdate = DateTime.Now;

                            DataLoader<ElioCompaniesViewsCompanies> loader = new DataLoader<ElioCompaniesViewsCompanies>(session);
                            loader.Update(companiesViewsCompany);
                        }
                    }
                    else
                    {
                        #region User Features

                        ElioUsersFeatures freeFeatures = Sql.GetFeaturesbyUserType(Convert.ToInt32(BillingTypePacket.FreemiumPacketType), session);
                        if (freeFeatures != null)
                        {
                            ElioUserPacketStatus userPackStatus = Sql.GetUserPacketStatusFeatures(viewedUser.Id, session);
                            if (userPackStatus == null)
                            {
                                userPackStatus = new ElioUserPacketStatus();

                                userPackStatus.UserId = viewedUser.Id;
                                userPackStatus.PackId = freeFeatures.PackId;
                                userPackStatus.UserBillingType = freeFeatures.UserBillingType;
                                userPackStatus.AvailableLeadsCount = freeFeatures.TotalLeads;
                                userPackStatus.AvailableMessagesCount = freeFeatures.TotalMessages;
                                userPackStatus.AvailableConnectionsCount = freeFeatures.TotalConnections;
                                userPackStatus.AvailableManagePartnersCount = freeFeatures.TotalManagePartners;
                                userPackStatus.AvailableLibraryStorageCount = Convert.ToDecimal(freeFeatures.TotalLibraryStorage);
                                userPackStatus.Sysdate = DateTime.Now;
                                userPackStatus.LastUpdate = DateTime.Now;
                                userPackStatus.StartingDate = DateTime.Now;
                                userPackStatus.ExpirationDate = DateTime.Now.AddMonths(1);

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
                                loader.Insert(userPackStatus);

                                FixUserLeadsAndPacketStatusFeatutes(companiesViewsCompany, viewedUser, interestedUser, safeModeIndex, session);
                            }
                        }

                        #endregion

                        //Logger.Error("!!! ERROR !!! User {0} has no packet status features at {1}.", viewedUser.Id.ToString(), DateTime.Now.ToString());
                    }

                    #endregion
                }
            }
            else
            {
                Logger.Error("!!! ERROR !!! User {0} has no packet status features at {1}. safeIndex count = {2}", viewedUser.Id.ToString(), DateTime.Now.ToString(), safeModeIndex.ToString());
            }
        }

        public static void AddCompanyViews(ElioUsers sessionUser, ElioUsers viewedCompany, string lang, DBSession session)
        {
            bool allowToInsert = true;
            if (sessionUser != null)
            {
                allowToInsert = Sql.IsUserAdministrator(sessionUser.Id, session);
            }
            else
            {
                allowToInsert = false;
            }

            if (!allowToInsert)
            {
                if ((sessionUser != null && sessionUser.Id != viewedCompany.Id) || sessionUser == null)
                {
                    ElioCompaniesViews views = Sql.GetCompanyViewsByCompanyId(viewedCompany.Id, session);
                    DataLoader<ElioCompaniesViews> loader = new DataLoader<ElioCompaniesViews>(session);

                    if (views == null)
                    {
                        views = new ElioCompaniesViews();
                        views.CompanyId = viewedCompany.Id;
                        views.Date = DateTime.Now;
                        views.Views++;

                        loader.Insert(views);
                    }
                    else
                    {
                        views.Views++;

                        loader.Update(views);
                    }

                    if (sessionUser != null)
                    {
                        if (sessionUser.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                        {
                            #region Save specific company

                            DataLoader<ElioCompaniesViewsCompanies> loader1 = new DataLoader<ElioCompaniesViewsCompanies>(session);

                            ElioCompaniesViewsCompanies companiesViewsCompany = Sql.GetCompanyViewsCompany(viewedCompany.Id, sessionUser.Id, session);
                            if (companiesViewsCompany == null)
                            {
                                companiesViewsCompany = new ElioCompaniesViewsCompanies();

                                companiesViewsCompany.CompanyViewsCompanyId = views.CompanyId;
                                companiesViewsCompany.InterestedCompanyId = sessionUser.Id;
                                companiesViewsCompany.SysDate = DateTime.Now;
                                companiesViewsCompany.LastUpdate = DateTime.Now;
                                companiesViewsCompany.IsNew = 1;
                                companiesViewsCompany.IsDeleted = 0;
                                companiesViewsCompany.CanBeViewed = 1;

                                loader1.Insert(companiesViewsCompany);

                                FixUserLeadsAndPacketStatusFeatutes(companiesViewsCompany, viewedCompany, sessionUser, 0, session);

                                if (viewedCompany.UserApplicationType != (int)UserApplicationType.ThirdParty)
                                {
                                    #region Send Lead email info

                                    try
                                    {
                                        //EmailNotificationsLib.SendNotificationEmailToCompanyForResentLeads(companiesViewsCompany.CompanyViewsCompantId, session);
                                        //EmailSenderLib.SendNotificationEmailToCompanyForResentLeads(companiesViewsCompany.CompanyViewsCompanyId, lang, session);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }

                                    #endregion
                                }
                            }
                            else
                            {
                                companiesViewsCompany.LastUpdate = DateTime.Now;
                                companiesViewsCompany.IsNew = 1;
                                companiesViewsCompany.IsDeleted = 0;

                                loader1.Update(companiesViewsCompany);
                            }

                            #endregion
                        }

                        #region Delete views over 30 Days (Not working any more)

                        //List<ElioCompaniesViews> totalViews = Sql.GetCountCompanyViewsByCompanyId(viewedCompany.Id, session);
                        //if (totalViews.Count > 30)
                        //{
                        //    int rowsToDelete = totalViews.Count - 30;
                        //    for (int i = 0; i < rowsToDelete; i++)
                        //    {
                        //        loader.Delete(totalViews[i]);
                        //    }
                        //}

                        #endregion
                    }
                }
            }
        }

        public static bool FixUserCategoriesByCheckBoxList(List<CheckBoxList> cbxList, Category category, int userId, bool insertOnly, DBSession session)
        {
            bool hasSelectedItem = false;

            switch (category)
            {
                case Enums.Category.Industry:

                    #region Industry

                    DataLoader<ElioUsersIndustries> loader = new DataLoader<ElioUsersIndustries>(session);

                    if (!insertOnly)
                    {
                        foreach (CheckBoxList list in cbxList)
                        {
                            foreach (ListItem item in list.Items)
                            {
                                bool exist = false;

                                exist = Sql.ExistUserIndustry(userId, Convert.ToInt32(item.Value), session);
                                if (item.Selected)
                                {
                                    hasSelectedItem = true;
                                    if (!exist)
                                    {
                                        ElioUsersIndustries newIndustry = new ElioUsersIndustries();
                                        newIndustry.UserId = userId;
                                        newIndustry.IndustryId = Convert.ToInt32(item.Value);

                                        loader.Insert(newIndustry);
                                    }
                                }
                                else
                                {
                                    if (exist)
                                    {
                                        //Delete user Industry
                                        Sql.DeleteUserIndustry(userId, Convert.ToInt32(item.Value), session);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (CheckBoxList list in cbxList)
                        {
                            foreach (ListItem item in list.Items)
                            {
                                if (item.Selected)
                                {
                                    ElioUsersIndustries newIndustry = new ElioUsersIndustries();
                                    newIndustry.UserId = userId;
                                    newIndustry.IndustryId = Convert.ToInt32(item.Value);

                                    loader.Insert(newIndustry);
                                }
                            }
                        }
                    }

                    #endregion

                    break;

                case Category.SubIndustry:

                    #region SubIndustry

                    DataLoader<ElioUsersSubIndustriesGroupItems> loader01 = new DataLoader<ElioUsersSubIndustriesGroupItems>(session);

                    if (!insertOnly)
                    {
                        foreach (CheckBoxList list in cbxList)
                        {
                            foreach (ListItem item in list.Items)
                            {
                                bool exist = false;

                                int groupId = Convert.ToInt32(item.Value.Split('~')[0]);
                                int subIndustryId = Convert.ToInt32(item.Value.Split('~')[1]);

                                exist = Sql.ExistUserSubIndustryGroupItem(userId, groupId, session);
                                if (item.Selected)
                                {
                                    hasSelectedItem = true;
                                    if (!exist)
                                    {
                                        ElioUsersSubIndustriesGroupItems newSubIndustryGroupItem = new ElioUsersSubIndustriesGroupItems();
                                        newSubIndustryGroupItem.UserId = userId;
                                        newSubIndustryGroupItem.SubIndustryGroupItemId = groupId;
                                        newSubIndustryGroupItem.SubIndustryGroupId = subIndustryId;

                                        loader01.Insert(newSubIndustryGroupItem);
                                    }
                                }
                                else
                                {
                                    if (exist)
                                    {
                                        //Delete user Sub Industry
                                        Sql.DeleteUserSubIndustryGroupItem(userId, groupId, session);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (CheckBoxList list in cbxList)
                        {
                            foreach (ListItem item in list.Items)
                            {
                                if (item.Selected)
                                {
                                    hasSelectedItem = true;

                                    ElioUsersSubIndustriesGroupItems newSubIndustryGroupItem = new ElioUsersSubIndustriesGroupItems();
                                    newSubIndustryGroupItem.UserId = userId;
                                    newSubIndustryGroupItem.SubIndustryGroupItemId = Convert.ToInt32(item.Value.Split('~')[0]);
                                    newSubIndustryGroupItem.SubIndustryGroupId = Convert.ToInt32(item.Value.Split('~')[1]);

                                    loader01.Insert(newSubIndustryGroupItem);                               
                                }
                            }
                        }
                    }

                    #endregion

                    break;

                case Category.Partner:

                    #region Partner

                    DataLoader<ElioUsersPartners> loader1 = new DataLoader<ElioUsersPartners>(session);

                    if (!insertOnly)
                    {
                        foreach (CheckBoxList list in cbxList)
                        {
                            foreach (ListItem item in list.Items)
                            {
                                bool exist = false;

                                exist = Sql.ExistUserPartner(userId, Convert.ToInt32(item.Value), session);
                                if (item.Selected)
                                {
                                    hasSelectedItem = true;
                                    if (!exist)
                                    {
                                        ElioUsersPartners newPartner = new ElioUsersPartners();
                                        newPartner.UserId = userId;
                                        newPartner.PartnerId = Convert.ToInt32(item.Value);

                                        loader1.Insert(newPartner);
                                    }
                                }
                                else
                                {
                                    if (exist)
                                    {
                                        //Delete user Partner
                                        Sql.DeleteUserPartnerProgramByPartnerID(userId, Convert.ToInt32(item.Value), session);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (CheckBoxList list in cbxList)
                        {
                            foreach (ListItem item in list.Items)
                            {
                                if (item.Selected)
                                {
                                    ElioUsersPartners newPartner = new ElioUsersPartners();
                                    newPartner.UserId = userId;
                                    newPartner.PartnerId = Convert.ToInt32(item.Value);

                                    loader1.Insert(newPartner);
                                }
                            }
                        }
                    }

                    #endregion

                    break;

                case Category.Market:

                    #region Market

                    DataLoader<ElioUsersMarkets> loader2 = new DataLoader<ElioUsersMarkets>(session);

                    if (!insertOnly)
                    {
                        foreach (CheckBoxList list in cbxList)
                        {
                            foreach (ListItem item in list.Items)
                            {
                                bool exist = false;

                                exist = Sql.ExistUserMarket(userId, Convert.ToInt32(item.Value), session);
                                if (item.Selected)
                                {
                                    hasSelectedItem = true;
                                    if (!exist)
                                    {
                                        ElioUsersMarkets newMarket = new ElioUsersMarkets();
                                        newMarket.UserId = userId;
                                        newMarket.MarketId = Convert.ToInt32(item.Value);

                                        loader2.Insert(newMarket);
                                    }
                                }
                                else
                                {
                                    if (exist)
                                    {
                                        //Delete user Market
                                        Sql.DeleteUserMarket(userId, Convert.ToInt32(item.Value), session);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (CheckBoxList list in cbxList)
                        {
                            foreach (ListItem item in list.Items)
                            {
                                if (item.Selected)
                                {
                                    ElioUsersMarkets newMarket = new ElioUsersMarkets();
                                    newMarket.UserId = userId;
                                    newMarket.MarketId = Convert.ToInt32(item.Value);

                                    loader2.Insert(newMarket);
                                }
                            }
                        }
                    }

                    #endregion

                    break;

                case Category.Api:

                    #region Api

                    DataLoader<ElioUsersApies> loader3 = new DataLoader<ElioUsersApies>(session);

                    if (!insertOnly)
                    {
                        foreach (CheckBoxList list in cbxList)
                        {
                            foreach (ListItem item in list.Items)
                            {
                                bool exist = false;

                                exist = Sql.ExistUserApi(userId, Convert.ToInt32(item.Value), session);
                                if (item.Selected)
                                {
                                    hasSelectedItem = true;
                                    if (!exist)
                                    {
                                        ElioUsersApies newApi = new ElioUsersApies();
                                        newApi.UserId = userId;
                                        newApi.ApiId = Convert.ToInt32(item.Value);

                                        loader3.Insert(newApi);
                                    }
                                }
                                else
                                {
                                    if (exist)
                                    {
                                        //Delete user Api
                                        Sql.DeleteUserApi(userId, Convert.ToInt32(item.Value), session);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (CheckBoxList list in cbxList)
                        {
                            foreach (ListItem item in list.Items)
                            {
                                if (item.Selected)
                                {
                                    ElioUsersApies newApi = new ElioUsersApies();
                                    newApi.UserId = userId;
                                    newApi.ApiId = Convert.ToInt32(item.Value);

                                    loader3.Insert(newApi);
                                }
                            }
                        }
                    }

                    #endregion

                    break;
            }

            return hasSelectedItem;
        }

        public static void ClearChekckBoxLists(List<CheckBoxList> cbxList)
        {
            #region SubIndustry

            foreach (CheckBoxList list in cbxList)
            {
                foreach (ListItem item in list.Items)
                {
                    item.Selected = false;
                }
            }

            #endregion
        }

        public static bool LoadUserSubCategories(List<CheckBoxList> cbxList, Category category, int userId, DBSession session)
        {
            bool hasSelectedItem = false;

            switch (category)
            {
                case Category.Industry:                    

                    break;

                case Category.SubIndustry:

                    #region SubIndustry

                    foreach (CheckBoxList list in cbxList)
                    {
                        foreach (ListItem item in list.Items)
                        {
                            item.Selected = Sql.ExistUserSubIndustryGroupItem(userId, Convert.ToInt32(item.Value.Split('~')[0]), session);
                        }
                    }

                    #endregion

                    break;

                case Category.Partner:                    

                    break;

                case Category.Market:                    

                    break;

                case Category.Api:
                    
                    break;
            }

            return hasSelectedItem;
        }

        public static ElioUsers InsertNewUserWithCredentials(string username, string password, string email, int userRegisterType, DBSession session)
        {
            ElioUsers user = new ElioUsers();

            user.Username = username;
            user.UsernameEncrypted = MD5.Encrypt(user.Username);
            user.Password = password;
            user.PasswordEncrypted = MD5.Encrypt(user.Password);
            user.SysDate = DateTime.Now;
            user.LastUpdated = DateTime.Now;
            user.LastLogin = DateTime.Now;
            user.UserLoginCount = 1;
            user.Ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
            user.Email = email;
            user.GuId = Guid.NewGuid().ToString();
            user.IsPublic = 1;
            user.CommunityStatus = Convert.ToInt32(AccountStatus.NotCompleted);
            user.CommunityProfileCreated = DateTime.Now;
            user.CommunityProfileLastUpdated = DateTime.Now;
            user.LinkedInUrl = string.Empty;
            user.TwitterUrl = string.Empty;
            user.HasBillingDetails = 0;
            user.BillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
            user.UserApplicationType = Convert.ToInt32(UserApplicationType.Elioplus);
            user.UserRegisterType = userRegisterType;
            //////user.CurrentSessionId = HttpContext.Current.Session.SessionID;

            return InsertNewUser(user, session);
        }

        public static ElioUsers UpdateNewUserWithCredentials(ElioUsers thirdPartyUser, string username, string password, string email, int userRegisterType, DBSession session)
        {
            ElioUsers user = new ElioUsers();

            thirdPartyUser.Username = username;
            thirdPartyUser.UsernameEncrypted = MD5.Encrypt(user.Username);
            thirdPartyUser.Password = password;
            thirdPartyUser.PasswordEncrypted = MD5.Encrypt(user.Password);
            //thirdPartyUser.SysDate = DateTime.Now;
            thirdPartyUser.LastUpdated = DateTime.Now;
            thirdPartyUser.LastLogin = DateTime.Now;
            //thirdPartyUser.UserLoginCount = 1;
            thirdPartyUser.Ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
            thirdPartyUser.Email = email;
            //thirdPartyUser.GuId = Guid.NewGuid().ToString();
            thirdPartyUser.IsPublic = 1;
            thirdPartyUser.CommunityStatus = Convert.ToInt32(AccountStatus.NotCompleted);
            //thirdPartyUser.CommunityProfileCreated = DateTime.Now;
            thirdPartyUser.CommunityProfileLastUpdated = DateTime.Now;
            //thirdPartyUser.LinkedInUrl = string.Empty;
            //thirdPartyUser.TwitterUrl = string.Empty;
            thirdPartyUser.HasBillingDetails = 0;
            thirdPartyUser.BillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
            thirdPartyUser.UserApplicationType = Convert.ToInt32(UserApplicationType.Elioplus);
            thirdPartyUser.UserRegisterType = userRegisterType;
            thirdPartyUser.AccountStatus = Convert.ToInt32(AccountStatus.NotCompleted);
            //////user.CurrentSessionId = HttpContext.Current.Session.SessionID;

            return UpDateUser(user, session);
        }

        public static ElioUsersDeleted InsertDeletedUser(ElioUsersDeleted user, DBSession session)
        {
            DataLoader<ElioUsersDeleted> loader = new DataLoader<ElioUsersDeleted>(session);
            loader.Insert(user);

            return user;
        }

        public static ElioUsers DeleteUser(ElioUsers user, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            loader.Delete(user);

            return user;
        }

        public static ElioUsers InsertNewUser(ElioUsers user, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            loader.Insert(user);

            return user;
        }

        public static ElioUsers UpDateUser(ElioUsers user, DBSession session)
        {
            user.LastUpdated = DateTime.Now;
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            loader.Update(user);

            return user;
        }

        public static ElioUsersSubAccounts UpDateSubUser(ElioUsersSubAccounts subUser, DBSession session)
        {
            DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);
            loader.Update(subUser);

            return subUser;
        }

        public static ElioUsersPerson InsertPerson(ElioUsersPerson person, DBSession session)
        {
            DataLoader<ElioUsersPerson> loader = new DataLoader<ElioUsersPerson>(session);
            loader.Insert(person);

            return person;
        }

        public static ElioUsersPerson UpDatePerson(ElioUsersPerson person, DBSession session)
        {
            person.LastUpdate = DateTime.Now;
            DataLoader<ElioUsersPerson> loader = new DataLoader<ElioUsersPerson>(session);
            loader.Update(person);

            return person;
        }

        public static ElioUsersPersonCompanies InsertCompany(ElioUsersPersonCompanies company, DBSession session)
        {
            DataLoader<ElioUsersPersonCompanies> loader = new DataLoader<ElioUsersPersonCompanies>(session);
            loader.Insert(company);

            return company;
        }

        public static ElioUsersPersonCompanies UpDateCompany(ElioUsersPersonCompanies company, DBSession session)
        {
            company.LastUpdate = DateTime.Now;
            DataLoader<ElioUsersPersonCompanies> loader = new DataLoader<ElioUsersPersonCompanies>(session);
            loader.Update(company);

            return company;
        }

        public static Dictionary<List<ElioCommunityPostsCommentsIJUsers>, List<ElioCommunityPostsCommentsIJUsers>> FixCommentsOrderList(List<ElioCommunityPostsCommentsIJUsers> comments)
        {
            Dictionary<List<ElioCommunityPostsCommentsIJUsers>, List<ElioCommunityPostsCommentsIJUsers>> dict = new Dictionary<List<ElioCommunityPostsCommentsIJUsers>, List<ElioCommunityPostsCommentsIJUsers>>();

            foreach (ElioCommunityPostsCommentsIJUsers comment in comments)
            {

            }

            return dict;
        }

        public static int CheckUserByLinkedinAccount(string strLinkId,
                                                 string strLinkFirstName,
                                                 string strLinkLastName,
                                                 string strLinkHeadline,
                                                 string strLinkPicture,
                                                 string strLinkEmail,
                                                 string strLinkSummary,
                                                 string strLinkProfileUrl,
                                                 DBSession session,
                                                 ref int userId)
        {
            int result = 0;
            userId = 0;

            ElioUsers user = Sql.GetUserByEmail(strLinkEmail, session);
            if (user != null)
            {
                #region Login User

                if (user.AccountStatus != Convert.ToInt32(AccountStatus.Blocked))
                {
                    #region User Account Status Not Blocked

                    if (!Sql.IsUserAdministrator(user.Id, session))
                    {
                        #region Keep Login Statistics

                        user.UserLoginCount += 1;

                        Sql.InsertUserLoginStatistics(user.Id, session);

                        #endregion
                    }

                    #region Update User Last Login

                    user.LastLogin = DateTime.Now;
                    user.UserLoginCount++;

                    user = UpDateUser(user, session);

                    #endregion

                    result = 1;
                    userId = user.Id;

                    #endregion
                }
                else
                {
                    #region User Account Status Blocked

                    result = -1;

                    #endregion
                }

                #endregion
            }
            else
            {
                ElioUsersSubAccounts subAccount = Sql.GetSubAccountByEmail(strLinkEmail, session);
                if (subAccount != null)
                {
                    #region Sub Account User

                    subAccount.LinkedinId = strLinkId;
                    subAccount.Password = strLinkId;
                    subAccount.PasswordEncrypted = MD5.Encrypt(strLinkId);
                    subAccount.FirstName = strLinkFirstName;
                    subAccount.LastName = strLinkLastName;
                    subAccount.Email = strLinkEmail;
                    subAccount.PersonalImage = (strLinkPicture != "undefined") ? strLinkPicture : null;
                    subAccount.LinkedinUrl = strLinkProfileUrl;
                    subAccount.CommunitySummaryText = (strLinkSummary == "undefined") ? strLinkHeadline : strLinkSummary;
                    subAccount.Sysdate = DateTime.Now;
                    subAccount.LastUpdated = DateTime.Now;
                    subAccount.CommunityProfileCreated = DateTime.Now;
                    subAccount.CommunityProfileLastUpdated = DateTime.Now;
                    subAccount.LastLogin = DateTime.Now;
                    subAccount.TwitterUrl = string.Empty;
                    subAccount.CommunityStatus = Convert.ToInt32(AccountStatus.Completed);
                    subAccount.AccountStatus = Convert.ToInt32(AccountStatus.Completed);
                    subAccount.IsConfirmed = 1;

                    DataLoader<ElioUsersSubAccounts> loader1 = new DataLoader<ElioUsersSubAccounts>(session);
                    loader1.Update(subAccount);

                    #endregion
                }
                else
                {
                    bool isBlackListed = Sql.IsBlackListedDomain(strLinkEmail, session);
                    if (isBlackListed)
                    {
                        #region User Email is Black Listed

                        result = -1;

                        #endregion
                    }
                    else
                    {
                        #region Register User

                        #region Insert New User

                        DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                        user = new ElioUsers();

                        user.LinkedinId = strLinkId;
                        user.Username = strLinkEmail;
                        user.UsernameEncrypted = MD5.Encrypt(strLinkEmail);
                        user.Password = strLinkId;
                        user.PasswordEncrypted = MD5.Encrypt(strLinkId);
                        user.FirstName = strLinkFirstName;
                        user.LastName = strLinkLastName;
                        user.Email = strLinkEmail;
                        user.PersonalImage = (strLinkPicture != "undefined") ? strLinkPicture : null;
                        user.LinkedInUrl = (strLinkProfileUrl != null) ? strLinkProfileUrl : "";
                        user.CommunitySummaryText = (strLinkSummary == "undefined") ? strLinkHeadline : strLinkSummary;
                        user.Ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
                        user.SysDate = DateTime.Now;
                        user.LastUpdated = DateTime.Now;
                        user.GuId = Guid.NewGuid().ToString();
                        user.IsPublic = 1;
                        user.CommunityProfileCreated = DateTime.Now;
                        user.CommunityProfileLastUpdated = DateTime.Now;
                        user.LastLogin = DateTime.Now;
                        user.TwitterUrl = string.Empty;
                        user.BillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
                        user.UserApplicationType = Convert.ToInt32(UserApplicationType.Elioplus);

                        user.CommunityStatus = Convert.ToInt32(AccountStatus.Completed);
                        user.AccountStatus = Convert.ToInt32(AccountStatus.NotCompleted);

                        loader.Insert(user);

                        #endregion

                        #region Insert user community notification emails settings

                        //InsertUserCommunityNotificationEmailSettings(user.Id, session);

                        #endregion

                        result = 2;

                        userId = user.Id;

                        #endregion
                    }
                }
            }

            return result;
        }

        public static void InsertUserCommunityNotificationEmailSettings(int userId, DBSession session)
        {
            List<ElioCommunityEmailNotifications> notifEmails = Sql.GetCommunityEmailNotifications(session);
            foreach (ElioCommunityEmailNotifications email in notifEmails)
            {
                if (!Sql.ExistCommunityUserEmailNotification(userId, email.Id, session))
                {
                    ElioCommunityUserEmailNotifications newEmailNotification = new ElioCommunityUserEmailNotifications();
                    newEmailNotification.CommunityEmailNotificationsId = email.Id;
                    newEmailNotification.UserId = userId;

                    DataLoader<ElioCommunityUserEmailNotifications> loader2 = new DataLoader<ElioCommunityUserEmailNotifications>(session);
                    loader2.Insert(newEmailNotification);
                }
            }
        }

        //public static void ResetUserPacketFeaturesByMonth(ElioUsers user, DBSession session)
        //{
        //    if (user.BillingType == Convert.ToInt32(BillingType.Freemium) && user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
        //    {
        //        ElioUserPacketStatus packetStatusFeatures = Sql.GetUserExpiredPacketStatusFeatures(user.Id, session);
        //        if (packetStatusFeatures != null)
        //        {
        //            ElioUsersFeatures userFeatures = Sql.GetFeaturesbyUserType(Convert.ToInt32(BillingType.Freemium), session);
        //            if (userFeatures != null)
        //            {
        //                packetStatusFeatures.StartingDate = (DateTime.Now.Day < user.SysDate.Day) ? Convert.ToDateTime(DateTime.Now.AddMonths(-1).Month + "/" + user.SysDate.Day + "/" + DateTime.Now.Year) : Convert.ToDateTime(DateTime.Now.Month + "/" + user.SysDate.Day + "/" + DateTime.Now.Year);

        //                int daysOfCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        //                int daysOfNextMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month);

        //                int expirationDayOfCurrentMonth = (daysOfCurrentMonth < user.SysDate.Day) ? daysOfCurrentMonth : user.SysDate.Day;
        //                int expirationDayOfNextMonth = (daysOfNextMonth < user.SysDate.Day) ? daysOfNextMonth : user.SysDate.Day;

        //                packetStatusFeatures.ExpirationDate = (DateTime.Now.Day < user.SysDate.Day) ? Convert.ToDateTime(DateTime.Now.Month + "/" + expirationDayOfCurrentMonth + "/" + DateTime.Now.Year) : Convert.ToDateTime(DateTime.Now.AddMonths(1).Month + "/" + expirationDayOfNextMonth + "/" + DateTime.Now.Year);

        //                int availableLeads = userFeatures.TotalLeads - Sql.GetUserLeadsCountByMonthRange(user, packetStatusFeatures.StartingDate, packetStatusFeatures.ExpirationDate, session);
        //                packetStatusFeatures.AvailableLeadsCount = (availableLeads > 0) ? availableLeads : -availableLeads;

        //                int availableMessages = userFeatures.TotalMessages - Sql.GetUserSendMessagesCountByMonthRange(user.Id, packetStatusFeatures.StartingDate, packetStatusFeatures.ExpirationDate, session);
        //                packetStatusFeatures.AvailableMessagesCount = (availableLeads > 0) ? availableMessages : -availableMessages;

        //                packetStatusFeatures.AvailableConnectionsCount = 0;
        //                packetStatusFeatures.LastUpdate = DateTime.Now;

        //                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
        //                loader.Update(packetStatusFeatures);

        //            }
        //        }
        //    }
        //    else if (user.BillingType == Convert.ToInt32(BillingType.Premium))
        //    {

        //    }
        //}

        public static bool FixPremiumUserOrderAndFeatures(ElioUsers user, DBSession session)
        {
            bool hasExpiredOrder = false;

            if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                hasExpiredOrder = Sql.UserOrderHasExpired(user.Id, session);
                if (hasExpiredOrder)
                {
                    ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Closed), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                    if (order != null)
                    {
                        order.IsReadyToUse = Convert.ToInt32(OrderStatus.NotReadyToUse);
                        order.OrderStatus = Convert.ToInt32(OrderStatus.Expired);
                        order.LastUpdate = DateTime.Now;

                        DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
                        loader.Update(order);

                        ElioUserPacketStatus packetStatus = Sql.GetUserPacketStatusFeatures(user.Id, session);
                        if (packetStatus != null)
                        {
                            packetStatus.AvailableLeadsCount = 0;
                            packetStatus.AvailableMessagesCount = 0;
                            packetStatus.AvailableConnectionsCount = 0;
                            packetStatus.LastUpdate = DateTime.Now;

                            DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                            loader1.Update(packetStatus);
                        }
                    }
                }
            }

            return hasExpiredOrder;
        }

        public static void FixPremiumUserAvailableManagePartnersAndPacketStatusFeatutes(ElioUsers user, DBSession session)
        {
            ElioUserPacketStatus packetStatus = Sql.GetUserPacketStatusFeatures(user.Id, session);
            
            if (packetStatus != null)
            {
                if (packetStatus.AvailableManagePartnersCount > 0)
                {
                    packetStatus.AvailableManagePartnersCount--;
                    packetStatus.LastUpdate = DateTime.Now;


                }
            }
        }

        public static void ResetThirdPartyUserVerticals(int userId, DBSession session)
        {
            session.ExecuteQuery(@"DELETE FROM Elio_users_sub_industries_group_items WHERE user_id = @user_id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }
    }
}