using System;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Localization;

namespace WdS.ElioPlus
{
    public partial class CollaborationToolMaster : System.Web.UI.MasterPage
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        private string CookieName
        {
            get
            {
                return "lgn";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        UpdateStrings();                        
                    }

                    PageTitle();
                    FixPage();
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
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

        private void PageTitle()
        {
            string metaDescription = "";
            string metaKeywords = "";

            PgTitle.Text = GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.User, out metaDescription, out metaKeywords, session);
        }

        private void UpdateStrings()
        {
            LblMenuDashboard.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "1")).Text;
            LblMenuChatDashboard.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "2")).Text;
            LblPartnersDashboard.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "3")).Text;
            LblMenuCollaborationLibrary.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "4")).Text;
            //LblDataVolume.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "5")).Text;
            //LblLibraryUsedSpace.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "6")).Text;
            //LblLibraryFreeSpace.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "13")).Text;
            LblUserProfile.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "7")).Text;
            LblUserDashboard.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "8")).Text;
            LblUserEditProfile.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "9")).Text;
            LblUserLogout.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "10")).Text;

            //RttInfo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "tooltip", "1")).Text;
            //RttFreeInfo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "tooltip", "2")).Text;
            //LblMenuCollaborationInvitation.Text = "My Invitations";
            //LblMenuCollaborationPartners.Text = "My Invitations";

            LblMenuCollaborationChoosePartners.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "11")).Text;
            LblMenuCollaborationMyFiles.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "collaborationtoolmaster", "label", "12")).Text;
        }

        private void FixPage()
        {
            aMainMenuUserProfile.Visible = (vSession.User.IsPublic == 1) ? true : false;
            aMainMenuUserProfile.HRef = aMyProfile.HRef = ControlLoader.Profile(vSession.User);
            aMainMenuUserDashboard.HRef = ControlLoader.Dashboard(vSession.User, "home");
            aMainMenuUserEditProfile.HRef = aUserProfileSettings.HRef = ControlLoader.Dashboard(vSession.User, "edit-company-profile");
            aDashboardElio.HRef = ControlLoader.Dashboard(vSession.User, "home");
            aDashboardChat.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-chat-room");
            aInbox.HRef = ControlLoader.Dashboard(vSession.User, "messages/inbox");
            //aMenuCollaborationInvitation.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-invitations");
            aMenuCollaborationPartners.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-partners");

            aMenuCollaborationChoosePartners.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-choose-partners");
            aMenuCollaborationMyFiles.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-library/" + Guid.NewGuid().ToString());

            LblCompanyName.Text = LblUserLoggedIn.Text = vSession.User.CompanyName;
            ImgCompanyLogo.ImageUrl = ImgUserLoggedIn.ImageUrl = vSession.User.CompanyLogo;
            ImgCompanyLogo.AlternateText = vSession.User.CompanyName + "logo";
            aCompanyLogo.HRef = ControlLoader.Profile(vSession.User);

            aLogoHome.HRef = ControlLoader.Default();

            int simpleMsgCount = 0;
            int groupMsgCount = 0;
            int totalNewMessagesCount = 0;

            ShowMasterNotifications(out simpleMsgCount, out groupMsgCount, out totalNewMessagesCount);

            //GetUserStorageInfo();
        }

        public void ShowMasterNotifications(out int simpleMsgCount, out int groupMsgCount, out int totalNewMessagesCount)
        {
            simpleMsgCount = 0;
            groupMsgCount = 0;
            int isNew = 1;
            int isViewed = 0;
            int isDeleted = 0;
            int isPublic = 1;

            totalNewMessagesCount = SqlCollaboration.GetUserTotalSimpleNewUnreadMailBoxMessagesNotification(vSession.User.Id, vSession.User.CompanyType, isNew, isViewed, isDeleted, isPublic, out simpleMsgCount, out groupMsgCount, session);
            if (totalNewMessagesCount > 0)
            {
                spanNotificationChatMsg.Visible = true;
                LblNotificationChatMsgCount.Text = totalNewMessagesCount.ToString();
            }
            else
            {
                spanNotificationChatMsg.Visible = false;
                LblNotificationChatMsgCount.Text = "";
            }

            int newReceivedFiles = SqlCollaboration.GetUserNewFilesReceived(vSession.User.Id, session);

            if (newReceivedFiles > 0)
            {
                spanNotificationNewReceivedFiles.Visible = true;
                LblNotificationNewReceivedFiles.Text = newReceivedFiles.ToString();
            }
            else
            {
                spanNotificationNewReceivedFiles.Visible = false;
                LblNotificationNewReceivedFiles.Text = "";
            }

            LblNotificationPendingRequestCount.Text = "0";
            int pendingRequestsCount = 0;
            spanNotificationPendingRequest.Visible = SqlCollaboration.HasInvitationRequestByStatus(vSession.User.Id, vSession.User.AccountStatus, vSession.User.CompanyType, CollaborateInvitationStatus.Pending.ToString(), out pendingRequestsCount, session);
            LblNotificationPendingRequestCount.Text = (spanNotificationPendingRequest.Visible) ? pendingRequestsCount.ToString() : "0";
        }

        private void GetUserStorageInfo()
        {
            //decimal totalPacketItemValue = 0;
            //decimal availablePacketItemValue = 0;
            //decimal usedSpace = 0;
            //decimal freeSpacePercent = 0;

            //divTransitionProgress.Attributes["data-transitiongoal"] = GlobalDBMethods.GetUserUsedStorageSpacePercent2(vSession.User.Id, "LibraryStorage", ref totalPacketItemValue, ref availablePacketItemValue, ref usedSpace, ref freeSpacePercent, session).ToString();
            //if (usedSpace.ToString().EndsWith("0"))
            //{
            //    LblUsedSpace.Text = usedSpace.ToString().Substring(0, usedSpace.ToString().Length - 1);
            //    if (LblUsedSpace.Text.EndsWith("0"))
            //        LblUsedSpace.Text = LblUsedSpace.Text.Substring(0, LblUsedSpace.Text.Length - 2);

            //    LblUsedSpace.Text += " GB";
            //}
            //else
            //    LblUsedSpace.Text = usedSpace.ToString() + " GB";

            //if (availablePacketItemValue < 2 && vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
            //    ImgInfo.Visible = true;
            //    divTransitionProgress.Attributes["class"] = "progress-bar progress-bar-danger";
            //    spanUsedSpace.Attributes["class"] = "pull-right label label-outline label-danger";
            //}
            //else
            //    ImgInfo.Visible = false;

            //divFreeTransitionProgress.Attributes["data-transitiongoal"] = freeSpacePercent.ToString();
            //if (availablePacketItemValue.ToString().EndsWith("0"))
            //{
            //    LblFreeSpace.Text = availablePacketItemValue.ToString().Substring(0, availablePacketItemValue.ToString().Length - 1);
            //    if (LblFreeSpace.Text.EndsWith("0"))
            //        LblFreeSpace.Text = LblFreeSpace.Text.Substring(0, LblFreeSpace.Text.Length - 2);

            //    LblFreeSpace.Text += " GB";
            //}
            //else
            //    LblFreeSpace.Text = availablePacketItemValue.ToString() + " GB";

            //if (usedSpace > totalPacketItemValue - 2 && vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
            //    ImgFreeInfo.Visible = true;
            //    divFreeTransitionProgress.Attributes["class"] = "progress-bar progress-bar-danger";
            //    spanFreeSpace.Attributes["class"] = "pull-right label label-outline label-danger";
            //}
            //else
            //    ImgFreeInfo.Visible = false;
        }

        #endregion

        #region Buttons

        protected void Logout_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                    Logger.Info("User {0}, logged out", vSession.User.Id);

                //////Global.RemoveFromSessionsList(vSession.User.CurrentSessionId);

                Session.Clear();
                vSession.SubAccountEmailLogin = "";

                if (Request.Browser.Cookies)
                {
                    HttpCookie loginCookie = Request.Cookies[CookieName];
                    if (loginCookie != null)
                    {
                        loginCookie.Expires = DateTime.Now.AddYears(-30);
                        Response.Cookies.Add(loginCookie);
                    }
                }

                Response.Redirect(vSession.Page = ControlLoader.Default(), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}