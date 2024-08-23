using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using WdS.ElioPlus.Objects;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web;

namespace WdS.ElioPlus.Controls.Collaboration.MailBox.ChatGrids
{
    public partial class uc_RdgMailBoxList : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        #region Page ViewState properties

        public List<ElioCollaborationVendorsResellers> VendorsResellersList
        {
            get
            {
                return (this.ViewState["VendorsResellersList"] != null) ? (List<ElioCollaborationVendorsResellers>)ViewState["VendorsResellersList"] : new List<ElioCollaborationVendorsResellers>();
            }

            set { this.ViewState["VendorsResellersList"] = value; }
        }

        public int CollaborationGroupId
        {
            get
            {
                if (ViewState["CollaborationGroupId"] != null)
                    return (int)ViewState["CollaborationGroupId"];
                else
                    return -1;
            }
            set
            {
                ViewState["CollaborationGroupId"] = value;
            }
        }

        public string QueryJoinClause
        {
            get
            {
                if (ViewState["QueryJoinClause"] != null)
                    return ViewState["QueryJoinClause"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["QueryJoinClause"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        UpdateStrings();
                        FixPage();
                        SetLinks();
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void SetLinks()
        {
            aInvitationToPartners.HRef = ControlLoader.Dashboard(vSession.User, "partners-invitations");       //ControlLoader.Dashboard(vSession.User, "collaboration -new-partners");
        }

        private void UpdateStrings()
        {

        }

        private void RedirectToLoginPage()
        {
            GlobalMethods.ClearCriteriaSession(vSession, false);
            Response.Redirect(ControlLoader.Login, false);
        }

        private void RedirectToUserDetails(GridDataItem item)
        {
            vSession.ElioCompanyDetailsView = Sql.GetUserById(Convert.ToInt32(item["creator_user_id"].Text), session);
            if (vSession.ElioCompanyDetailsView != null)
            {
                Response.Redirect(ControlLoader.CommunityUserProfile(vSession.ElioCompanyDetailsView), false);
            }
            else
            {
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "11")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "4")).Text;
                GlobalMethods.ShowMessageControlDA(UcMailBoxListMessageAlert, alert, MessageTypes.Error, true, true, false);
            }
        }

        protected void FixMailBoxMessageStatus(GridDataItem item, Label lblMsgStatus, int isNew, int isViewed, string dateViewed)
        {
            if (isNew == 1 && isViewed == 0)
            {
                lblMsgStatus.Text = " | (unread message)";
            }
            else if (isNew == 1 && isViewed == 1)
            {
                lblMsgStatus.Text = " | (New delivered message)";
            }
            else if (isNew == 0 && isViewed == 1)
            {
                if (!string.IsNullOrEmpty(dateViewed.ToString()) && dateViewed.ToString() != "&nbsp;")
                    lblMsgStatus.Text = " | seen at " + dateViewed.ToString();
                else
                    lblMsgStatus.Text = " | seen";
            }
        }
        
        private void SetUserMailboxAsViewed(int receiverUserId, ElioCollaborationUsersGroupMaibox mailbox)
        {
            if (mailbox.IsNew == 1 && mailbox.IsViewed == 0)
            {
                if (receiverUserId == mailbox.ReceiverUserId)
                {
                    mailbox.IsNew = 0;
                    mailbox.IsViewed = 1;
                    mailbox.DateViewed = DateTime.Now;

                    DataLoader<ElioCollaborationUsersGroupMaibox> loader = new DataLoader<ElioCollaborationUsersGroupMaibox>(session);
                    loader.Update(mailbox);
                }
            }
        }

        private bool ExistMailBox(List<ElioCollaborationUsersMailbox> mails, int mailBoxId)
        {
            foreach (ElioCollaborationUsersMailbox mail in mails)
            {
                if (mail.MailboxId == mailBoxId)
                {
                    return true;
                }
            }

            return false;
        }

        public void FixPage()
        {
            if (vSession.User != null)
            {
                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                bool hasMessages = false;
                bool hasConfirmedInvitationPartners = SqlCollaboration.HasCollaborationPartnersOrMessages(vSession.User.Id, vSession.User.CompanyType, out hasMessages, session);

                if (!hasConfirmedInvitationPartners)
                {
                    divNoCollaborationPartners.Visible = true;
                    divSimpleMailBox.Visible = false;
                    divGroupMailBox.Visible = false;
                }
                else
                {
                    HtmlControl divTabConnections = (HtmlControl)ControlFinder.FindControlBackWards(this, "divTabConnections");
                    HtmlControl divTabGroups = (HtmlControl)ControlFinder.FindControlBackWards(this, "divTabGroups");

                    //string alert = (hasConfirmedInvitationPartners && hasMessages) ? "Click on your Collaboration Partner to continue a discussion" : "Click on your Collaboration Partner to start a discussion";
                    string alert = string.Format("Click on your Collaboration Partner to {0} a discussion", (hasMessages) ? "continue" : "start");    //(hasConfirmedInvitationPartners && hasMessages) ? "Click on your Collaboration Partner to continue a discussion" : "Click on your Collaboration Partner to start a discussion";

                    if (divTabConnections.Visible)
                    {
                        if (RdgMailBox.DataSource == null)
                        {
                            RdgMailBox.Visible = false;
                            if (vSession.VendorsResellersList.Count == 0)
                                GlobalMethods.ShowMessageControlDA(UcMailBoxListMessageAlert, alert, MessageTypes.Info, true, true, true, false, false);
                            else
                            {
                                if (session.Connection.State == ConnectionState.Closed)
                                    session.OpenConnection();

                                ElioUsers partner = Sql.GetUserById(vSession.VendorsResellersList[0].PartnerUserId, session);
                                if (partner != null)
                                    GlobalMethods.ShowMessageControlDA(UcMailBoxListMessageAlert, "You have no chat messages from/to " + partner.CompanyName + " yet. Start a discussion now", MessageTypes.Info, true, true, true, false, false);
                            }
                        }
                    }
                    else if (divTabGroups.Visible)
                    {
                        if (RdgGroupMailBox.DataSource == null)
                        {
                            RdgGroupMailBox.Visible = false;
                            GlobalMethods.ShowMessageControlDA(UcMailBoxListMessageAlert, alert, MessageTypes.Info, true, true, true, false, false);
                        }
                    }
                }
            }
            else
                Response.Redirect(ControlLoader.Login, false);
        }

        //private bool GetGroupMembersForMessages(string groupId, string companyType, int senderUserId, out List<ElioUsers> groupPartners)
        //{
        //    groupPartners = new List<ElioUsers>();

        //    if (groupId != "")
        //    {
        //        ElioUsers user = new ElioUsers();

        //        List<ElioCollaborationUsersGroupMembersIJUsers> members = SqlCollaboration.GetUserGroupMembersIJUsersByGroupId(Convert.ToInt32(groupId), null, session);

        //        if (members.Count > 0)
        //        {
        //            if (companyType == Types.Vendors.ToString())
        //            {
        //                foreach (ElioCollaborationUsersGroupMembersIJUsers member in members)
        //                {
        //                    if (senderUserId != member.GroupRetailorId)
        //                    {
        //                        user = Sql.GetUserById(member.GroupRetailorId, session);
        //                        if (user != null)
        //                            groupPartners.Add(user);
        //                    }
        //                }
        //            }
        //            else if (companyType == EnumHelper.GetDescription(Types.Resellers).ToString())
        //            {
        //                bool masterInserted = false;
        //                foreach (ElioCollaborationUsersGroupMembersIJUsers member in members)
        //                {
        //                    if (!masterInserted)
        //                    {
        //                        user = Sql.GetUserById(member.CreatorUserId, session);
        //                        if (user != null)
        //                            groupPartners.Add(user);

        //                        masterInserted = true;
        //                    }

        //                    if (senderUserId != member.GroupRetailorId)
        //                    {
        //                        user = Sql.GetUserById(member.GroupRetailorId, session);
        //                        if (user != null)
        //                            groupPartners.Add(user);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return (groupPartners.Count > 0) ? true : false;
        //}

        #endregion

        #region Buttons

        protected void ImgBtnSetNotPublic_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ImageButton imgBtnSetNotPublic = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtnSetNotPublic.NamingContainer;

                    SqlCollaboration.DeleteCollaborationUsersMailBoxById(Convert.ToInt32(item["mailbox_id"].Text), session);

                    RdgMailBox.Rebind();
                }
                else
                {
                    #region Go to Login

                    RedirectToLoginPage();

                    #endregion
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

        protected void ImgBtn_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ImageButton imgBtnMustRead = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtnMustRead.NamingContainer;
                                       
                }
                else
                {
                    #region Go to Login

                    RedirectToLoginPage();

                    #endregion
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

        protected void ImgBtnPartnerLogo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ImageButton imgBtnPartnerLogo = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtnPartnerLogo.NamingContainer;


                }
                else
                {
                    #region Go to Login

                    RedirectToLoginPage();

                    #endregion
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

        protected void ImgBtnPreviewLogFiles_OnClick(object sender, EventArgs args)
        {
            try
            {
                //ImageButton imgBtnPreviewLogFiles = (ImageButton)sender;

                HtmlAnchor btn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                //HyperLink hpLnkPreViewLogFiles = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkPreViewLogFiles");
                HtmlAnchor aImgBtnPreviewLogFiles = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aImgBtnPreviewLogFiles");

                string url = aImgBtnPreviewLogFiles.HRef;       //hpLnkPreViewLogFiles.NavigateUrl;

                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Grids

        protected void RdgMailBox_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            vSession.CurrentGridPageIndex = e.NewPageIndex;
        }

        protected void RdgMailBox_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridNoRecordsItem)
                {
                    GridNoRecordsItem item = (GridNoRecordsItem)e.Item;
                    Literal ltlNoDataFound = (Literal)ControlFinder.FindControlRecursive(item, "LtlNoDataFound");
                    ltlNoDataFound.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "grid", "1", "literal", "1")).Text;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
        
        protected void RdgMailBox_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    if (vSession.User != null)
                    {
                        Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                        HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                        Label lblMailBoxContent = (Label)ControlFinder.FindControlRecursive(item, "LblMailBoxContent");                        
                        Label lblLastUpdate = (Label)ControlFinder.FindControlRecursive(item, "LblLastUpdate");
                        Label lblMsgStatus = (Label)ControlFinder.FindControlRecursive(item, "LblMsgStatus");
                        Image imgLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgLogo");
                        PlaceHolder phPartnersLogo = (PlaceHolder)ControlFinder.FindControlRecursive(item, "PhPartnersLogo");
                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        imgLogo.AlternateText = "logo";
                        phPartnersLogo.Controls.Clear();

                        HtmlGenericControl divMessage = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divMessage");
                        HtmlGenericControl divContentMessage = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divContentMessage");

                        if (vSession.User.Id == Convert.ToInt32(item["user_id"].Text))
                        {
                            aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.Profile(vSession.User);
                            aCompanyLogo.Target = aCompanyName.Target = "_blank";

                            lblCompanyName.Text = vSession.User.CompanyName;
                            imgLogo.ToolTip = "See " + vSession.User.CompanyName + "'s profile";
                            imgLogo.AlternateText = "See " + vSession.User.CompanyName + "'s profile";

                            divMessage.Attributes["class"] = "d-flex flex-column mb-5 align-items-start";
                            divContentMessage.Attributes["class"] = "mt-2 rounded p-5 bg-light-success text-dark-50 font-weight-bold font-size-lg text-left max-w-400p";
                        }
                        else
                        {
                            ElioUsers partner = Sql.GetUserById(Convert.ToInt32(item["user_id"].Text), session);
                            if (partner != null)
                            {
                                aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.Profile(partner);
                                aCompanyLogo.Target = aCompanyName.Target = "_blank";

                                lblCompanyName.Text = partner.CompanyName;
                                imgLogo.ToolTip = "See " + partner.CompanyName + "'s profile";
                                imgLogo.AlternateText = "See " + partner.CompanyName + "'s profile";
                            }

                            divMessage.Attributes["class"] = "d-flex flex-column mb-5 align-items-end";
                            divContentMessage.Attributes["class"] = "mt-2 rounded p-5 bg-light-primary text-dark-50 font-weight-bold font-size-lg text-left max-w-400p";
                        }

                        //HyperLink hpLnkPreViewLogFiles = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkPreViewLogFiles");
                        //ImageButton imgBtnPreviewLogFiles = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnPreviewLogFiles");

                        Label hpLnkPreViewLogFiles = (Label)ControlFinder.FindControlRecursive(item, "HpLnkPreViewLogFiles");
                        HtmlAnchor aImgBtnPreviewLogFiles = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aImgBtnPreviewLogFiles");

                        ElioCollaborationUsersLibraryFiles att = SqlCollaboration.GetCollaborationUsersLibraryAttachmentFilesByMailboxIdUserId(Convert.ToInt32(item["mailbox_id"].Text), vSession.User.Id, session);
                        if (att != null)
                        {
                            aImgBtnPreviewLogFiles.Visible = true;
                            hpLnkPreViewLogFiles.Text = att.FileName;
                            //string url = (HttpContext.Current.Request.IsLocal) ? "http://" : "https://";
                            //aImgBtnPreviewLogFiles.HRef= url + HttpContext.Current.Request.Url.Authority + System.Configuration.ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"] + "/" + att.FilePath;
                            aImgBtnPreviewLogFiles.HRef = System.Configuration.ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"] + "/" + att.FilePath;
                            aImgBtnPreviewLogFiles.Target = "_blank";
                            //hpLnkPreViewLogFiles.NavigateUrl = url + HttpContext.Current.Request.Url.Authority + System.Configuration.ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"] + "/" + att.FilePath;      //defaultCategory.CategoryDescription + "/" + vSession.User.GuId + "/" + att.FileName;
                        }
                        else
                        {
                            aImgBtnPreviewLogFiles.Visible = false;
                            hpLnkPreViewLogFiles.Visible = false;

                            //ElioCollaborationUsersLibraryFilesSend attFromLibrary = SqlCollaboration.GetCollaborationUsersFromLibrarySendAttachmentFilesByMailboxIdUserId(Convert.ToInt32(item["mailbox_id"].Text), session);
                            //if (attFromLibrary != null)
                            //{
                            //    att = SqlCollaboration.GetCollaborationUserLibraryFileById(attFromLibrary.FileId, session);
                            //    if (att != null)
                            //    {
                            //        imgBtnPreviewLogFiles.Visible = true;
                            //        hpLnkPreViewLogFiles.Text = att.FileName;
                            //        string url = (HttpContext.Current.Request.IsLocal) ? "http://" : "https://";
                            //        hpLnkPreViewLogFiles.NavigateUrl = url + HttpContext.Current.Request.Url.Authority + System.Configuration.ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"] + "/" + att.FilePath;      //defaultCategory.CategoryDescription + "/" + vSession.User.GuId + "/" + att.FileName;
                            //    }
                            //    else
                            //    {
                            //        imgBtnPreviewLogFiles.Visible = false;
                            //        hpLnkPreViewLogFiles.Visible = false;
                            //    }
                            //}
                            //else
                            //{
                            //    imgBtnPreviewLogFiles.Visible = false;
                            //    hpLnkPreViewLogFiles.Visible = false;
                            //}
                        }

                        #region to delete

                        //////List<ElioUsers> partnersImgsAndNames = SqlCollaboration.GetCollaborationPartnersImagesUrlByMailBoxIdAndUserType(Convert.ToInt32(item["id"].Text), companyType, session);

                        //////int imgIndex = 0;
                        //////int marginLeft = 22;

                        //////foreach (ElioUsers partner in partnersImgsAndNames)
                        //////{
                        //////    imgIndex++;
                        //////    HtmlAnchor aPartnerLogo = new HtmlAnchor();
                        //////    aPartnerLogo.HRef = ControlLoader.Profile(partner);
                        //////    aPartnerLogo.Target = "_blank";

                        //////    Image imgPartnerLogo = new Image();
                        //////    imgPartnerLogo.ID = "imgBtnPartnerLogo_" + item["id"].Text + "_" + imgIndex;
                        //////    imgPartnerLogo.ImageUrl = partner.CompanyLogo;                           
                        //////    imgPartnerLogo.CssClass = "img-circle chat-avatar";
                        //////    imgPartnerLogo.Width = 18;
                        //////    imgPartnerLogo.Height = 18;
                        //////    //imgPartnerLogo.Click += new System.Web.UI.ImageClickEventHandler(ImgBtnPartnerLogo_Click);
                        //////    imgPartnerLogo.ToolTip = "See " + partner.CompanyName + "'s profile";

                        //////    ElioCollaborationMailboxIJUsersMailBox mailbox = SqlCollaboration.GetCollaborationUsersMailBoxByMailboxId(Convert.ToInt32(item["mailbox_id"].Text), partner.Id, session);

                        //////    if (mailbox != null)
                        //////    {
                        //////        if (mailbox.IsNew == 0 && mailbox.IsViewed == 1)
                        //////        {
                        //////            imgPartnerLogo.Attributes["style"] = "margin-top:55px; margin-left:" + marginLeft + "px; border:1px solid #000000";

                        //////            if (!string.IsNullOrEmpty(item["date_viewed"].Text) && item["date_viewed"].Text != "&nbsp;")
                        //////            {
                        //////                imgPartnerLogo.ToolTip = "Partner has read this mesage at " + GlobalMethods.FixDateWithoutHour(Convert.ToDateTime(item["date_viewed"].Text).ToString("dd/MM/yyyy"));
                        //////            }
                        //////            else
                        //////                imgPartnerLogo.ToolTip = "Partner has read this mesage";
                        //////        }
                        //////        else if (mailbox.IsNew == 1 && mailbox.IsViewed == 0)
                        //////        {
                        //////            bool isGroupMail = SqlCollaboration.IsGroupMailBoxByMailboxId(Convert.ToInt32(item["mailbox_id"].Text), session);

                        //////            if (!isGroupMail)
                        //////            {
                        //////                imgPartnerLogo.Attributes["style"] = "margin-top:55px; margin-left:" + marginLeft + "px; border:0px solid #ffffff";
                        //////                imgPartnerLogo.ToolTip = "Partner has not read this mesage yet";
                        //////            }
                        //////            else
                        //////                imgPartnerLogo.Visible = false;
                        //////        }
                        //////        else
                        //////        {
                        //////            imgPartnerLogo.Attributes["style"] = "margin-top:55px; margin-left:" + marginLeft + "px; border:1px solid #000000";
                        //////            imgPartnerLogo.Visible = false;
                        //////        }

                        //////        aPartnerLogo.Controls.Add(imgPartnerLogo);
                        //////        phPartnersLogo.Controls.Add(aPartnerLogo);
                        //////        marginLeft += 22;
                        //////    }
                        //////    //else
                        //////    //{
                        //////    //    if (Convert.ToInt32(item["is_new"].Text) == 0 && Convert.ToInt32(item["is_viewed"].Text) == 1)
                        //////    //    {
                        //////    //        imgPartnerLogo.Attributes["style"] = "margin-top:55px; margin-left:" + marginLeft + "px; border:1px solid #000000";

                        //////    //        DateTime? dateMsgViewed = null;
                        //////    //        if (!string.IsNullOrEmpty(item["date_viewed"].Text) && item["date_viewed"].Text != "&nbsp;")
                        //////    //        {
                        //////    //            dateMsgViewed = Convert.ToDateTime(item["date_viewed"].Text);
                        //////    //            imgPartnerLogo.ToolTip = "Partner has read this mesage at " + GlobalMethods.FixDateWithoutHour(Convert.ToDateTime(dateMsgViewed).ToString("dd/MM/yyyy"));
                        //////    //        }
                        //////    //        else
                        //////    //            imgPartnerLogo.ToolTip = "Partner has read this mesage";
                        //////    //    }
                        //////    //    else if (Convert.ToInt32(item["is_new"].Text) == 1 && Convert.ToInt32(item["is_viewed"].Text) == 0)
                        //////    //    {
                        //////    //        imgPartnerLogo.Attributes["style"] = "margin-top:55px; margin-left:" + marginLeft + "px; border:0px solid #ffffff";
                        //////    //        imgPartnerLogo.ToolTip = "Partner has not read this mesage yet";
                        //////    //    }
                        //////    //    else
                        //////    //    {
                        //////    //        imgPartnerLogo.Attributes["style"] = "margin-top:55px; margin-left:" + marginLeft + "px; border:1px solid #000000";
                        //////    //    }

                        //////    //    aPartnerLogo.Controls.Add(imgPartnerLogo);
                        //////    //    phPartnersLogo.Controls.Add(aPartnerLogo);
                        //////    //    marginLeft += 22;
                        //////    //}
                        //////}

                        #endregion

                        imgLogo.ImageUrl = (item["company_logo"].Text != "&nbsp;") ? item["company_logo"].Text : "/images/icons/personal_img_3.png";

                        lblMailBoxContent.Text = GlobalMethods.FixParagraphsView(item["message_content"].Text, 1);
                        lblLastUpdate.Text = "written at " + GlobalMethods.FixDateWithoutHour(Convert.ToDateTime(item["date_created"].Text).ToString("dd/MM/yyyy"));

                        string dateViewed = (!string.IsNullOrEmpty(item["date_viewed"].Text) && item["date_viewed"].Text.ToString() != "&nbsp;") ? item["date_viewed"].Text.ToString() : "";

                        FixMailBoxMessageStatus(item, lblMsgStatus, Convert.ToInt32(item["is_new"].Text), Convert.ToInt32(item["is_viewed"].Text), dateViewed);
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
                
        protected void Timer_OnTick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                RdgMailBox.Rebind();
                RdgGroupMailBox.Rebind();
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

        protected void RdgMailBox_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                #region to delete

                //if (vSession.VendorsResellersList == null && vSession.VendorsResellersList.Count == 0)
                //{
                //    List<ElioCollaborationVendorsResellers> vendRes = SqlCollaboration.GetUserCollaborationVendorResellersByStatus(vSession.User.Id, vSession.User.CompanyType, CollaborateInvitationStatus.Confirmed.ToString(), session);

                //    vSession.VendorsResellersList = new List<ElioCollaborationVendorsResellers>();

                //    foreach (ElioCollaborationVendorsResellers collaboration in vendRes)
                //    {
                //        vSession.VendorsResellersList.Add(collaboration);
                //    }
                //}

                #endregion

                if (VendorsResellersList != null && vSession.VendorsResellersList.Count > 0)
                {
                    List<ElioCollaborationUsersMailbox> mails = new List<ElioCollaborationUsersMailbox>();

                    foreach (ElioCollaborationVendorsResellers coll in vSession.VendorsResellersList)
                    {
                        List<ElioCollaborationUsersMailbox> collMails = SqlCollaboration.LoadCollaborationUsersMailBoxByVendResId(coll.Id, 0, QueryJoinClause, CollaborationGroupId, session);

                        foreach (ElioCollaborationUsersMailbox collMail in collMails)
                        {
                            if (!string.IsNullOrEmpty(QueryJoinClause) && SqlCollaboration.IsGroupMailBoxByMailboxId(collMail.MailboxId, session))
                            {
                                if (!ExistMailBox(mails, collMail.MailboxId))
                                {
                                    mails.Add(collMail);
                                }
                            }
                            else if (string.IsNullOrEmpty(QueryJoinClause) && !SqlCollaboration.IsGroupMailBoxByMailboxId(collMail.MailboxId, session))
                            {
                                if (!ExistMailBox(mails, collMail.MailboxId))
                                    mails.Add(collMail);
                            }
                        }
                    }

                    mails = mails.OrderBy(x => x.Sysdate).ToList();
                    if (mails.Count > 0)
                    {
                        List<ElioCollaborationMailboxIJUsersMailBox> userMails = new List<ElioCollaborationMailboxIJUsersMailBox>(); //SqlCollaboration.GetUserMailBoxList(vSession.User.Id, vSession.User.CompanyType, session);  //GetUserCollaborationSendMails(vSession.User.Id, session);

                        foreach (ElioCollaborationUsersMailbox mail in mails)
                        {
                            ElioCollaborationMailboxIJUsersMailBox userMail = SqlCollaboration.GetCollaborationUsersMailBoxByMailboxId(mail.MailboxId, mail.PartnerUserId, session);

                            if (userMail != null)
                                userMails.Add(userMail);
                        }

                        if (userMails.Count > 0)
                        {
                            RdgMailBox.Visible = true;
                            UcMailBoxListMessageAlert.Visible = false;

                            DataTable table = new DataTable();

                            table.Columns.Add("id");
                            table.Columns.Add("mailbox_id");
                            table.Columns.Add("user_id");
                            table.Columns.Add("message_content");
                            table.Columns.Add("date_created");
                            table.Columns.Add("date_send");
                            table.Columns.Add("date_received");
                            table.Columns.Add("is_new");
                            table.Columns.Add("is_viewed");
                            table.Columns.Add("date_viewed");
                            table.Columns.Add("total_reply_comments");
                            table.Columns.Add("username");
                            table.Columns.Add("company_logo");
                            table.Columns.Add("company_name");

                            foreach (ElioCollaborationMailboxIJUsersMailBox mailBox in userMails)
                            {
                                #region to delete

                                //bool exist = true;
                                //int? isNew = -1;
                                //int? isViewed = -1;
                                //int? isDeleted = -1;
                                //DateTime? dateViewed = null;

                                //foreach (GridDataItem item in rdgMyResellers.Items)
                                //{
                                //    CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
                                //    if (cbx != null)
                                //    {
                                //        if (cbx.Checked)
                                //        {
                                //            int partnerUserId = Convert.ToInt32(item["partner_user_id"].Text);
                                //            int masterUserId = Convert.ToInt32(item["master_user_id"].Text);

                                //            exist = SqlCollaboration.ShowSelectedPartnersMailsByUserType(vSession.User.Id, mailBox.Id, partnerUserId, masterUserId, vSession.User.CompanyType, out isNew, out isViewed, out isDeleted, out dateViewed, session);
                                //            if (exist)
                                //                break;
                                //        }
                                //    }
                                //}

                                #endregion

                                if (mailBox.IsDeleted == 1 && Sql.IsUserAdministrator(vSession.User.Id, session))
                                {
                                    ElioUsers user = Sql.GetUserById(mailBox.UserId, session);
                                    if (user != null)
                                    {
                                        table.Rows.Add(mailBox.Id, mailBox.MailboxId, mailBox.UserId, mailBox.MessageContent
                                            , mailBox.DateCreated, mailBox.DateSend.ToString("dd/MM/yyyy")
                                            , mailBox.DateReceived.ToString("dd/MM/yyyy"), mailBox.IsNew, mailBox.IsViewed, (mailBox.IsViewed == 1) ? Convert.ToDateTime(mailBox.DateViewed).ToString("F") : "", mailBox.TotalReplyComments, user.Username, user.CompanyLogo, user.CompanyName);
                                    }
                                }
                                else if (mailBox.IsDeleted == 0)
                                {
                                    ElioUsers user = Sql.GetUserById(mailBox.UserId, session);
                                    if (user != null)
                                    {
                                        table.Rows.Add(mailBox.Id, mailBox.MailboxId, mailBox.UserId, mailBox.MessageContent
                                            , mailBox.DateCreated, mailBox.DateSend.ToString("dd/MM/yyyy")
                                            , mailBox.DateReceived.ToString("dd/MM/yyyy"), mailBox.IsNew, mailBox.IsViewed, (mailBox.IsViewed == 1) ? Convert.ToDateTime(mailBox.DateViewed).ToString("F") : "", mailBox.TotalReplyComments, user.Username, user.CompanyLogo, user.CompanyName);
                                    }
                                }
                            }

                            if (table.Rows.Count > 0)
                            {
                                RdgMailBox.DataSource = table;
                            }
                            else
                            {
                                FixPage();
                            }
                        }
                        else
                        {
                            FixPage();
                        }
                    }
                    else
                    {
                        FixPage();
                    }
                }
                else
                {
                    FixPage();
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

        protected void RdgGroupMailBox_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            vSession.CurrentGridPageIndex = e.NewPageIndex;
        }

        protected void RdgGroupMailBox_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridNoRecordsItem)
                {
                    GridNoRecordsItem item = (GridNoRecordsItem)e.Item;
                    Literal ltlNoDataFound = (Literal)ControlFinder.FindControlRecursive(item, "LtlNoDataFound");
                    ltlNoDataFound.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "grid", "1", "literal", "1")).Text;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgGroupMailBox_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    if (vSession.User != null && CollaborationGroupId != -1)
                    {
                        Label lblMailBoxContent = (Label)ControlFinder.FindControlRecursive(item, "LblMailBoxContent");
                        Label lblLastUpdate = (Label)ControlFinder.FindControlRecursive(item, "LblLastUpdate");
                        Label lblMsgStatus = (Label)ControlFinder.FindControlRecursive(item, "LblMsgStatus");
                        Image imgLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgLogo");
                        PlaceHolder phPartnersLogo = (PlaceHolder)ControlFinder.FindControlRecursive(item, "PhPartnersLogo");
                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                        imgLogo.AlternateText = "logo";
                        phPartnersLogo.Controls.Clear();

                        HtmlGenericControl divMessage = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divMessage");
                        HtmlGenericControl divContentMessage = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divContentMessage");

                        if (vSession.User.Id == Convert.ToInt32(item["user_id"].Text))
                        {
                            aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.Profile(vSession.User);

                            imgLogo.ToolTip = "See " + vSession.User.CompanyName + "'s profile";
                            imgLogo.AlternateText = "See " + vSession.User.CompanyName + "'s profile";

                            divMessage.Attributes["class"] = "d-flex flex-column mb-5 align-items-start";
                            divContentMessage.Attributes["class"] = "mt-2 rounded p-5 bg-light-success text-dark-50 font-weight-bold font-size-lg text-left max-w-400p";

                            imgLogo.ImageUrl = vSession.User.CompanyLogo;

                            List<ElioUsers> groupMembersImgsAndNames = new List<ElioUsers>();
                            int isActive = 1;
                            int isPublic = 1;

                            bool hasMembers = GlobalDBMethods.GetGroupMembersForMessages(CollaborationGroupId, isActive, isPublic, vSession.User.CompanyType, vSession.User.Id, out groupMembersImgsAndNames, session);      //SqlCollaboration.GetCollaborationPartnersImagesUrlByMailBoxIdAndUserType(Convert.ToInt32(item["id"].Text), companyType, session);
                            if (hasMembers)
                            {
                                int imgIndex = 0;
                                //int marginTop = 55;
                                int marginLeft = 40;

                                foreach (ElioUsers partner in groupMembersImgsAndNames)
                                {
                                    imgIndex++;
                                    HtmlAnchor aPartnerLogo = new HtmlAnchor();
                                    aPartnerLogo.HRef = ControlLoader.Profile(partner);
                                    aPartnerLogo.Target = "_blank";

                                    Image imgPartnerLogo = new Image();
                                    imgPartnerLogo.ID = "imgBtnPartnerLogo_" + item["id"].Text + "_" + imgIndex;
                                    imgPartnerLogo.ImageUrl = partner.CompanyLogo;
                                    imgPartnerLogo.CssClass = "img-circle chat-avatar";
                                    imgPartnerLogo.Width = 18;
                                    imgPartnerLogo.Height = 18;
                                    imgPartnerLogo.ToolTip = "See " + partner.CompanyName + "'s profile";
                                    imgPartnerLogo.AlternateText = "See " + partner.CompanyName + "'s profile";

                                    ElioCollaborationUsersGroupMaibox mailbox = SqlCollaboration.GetCollaborationUsersGroupMailBoxByMailboxId(Convert.ToInt32(item["id"].Text), partner.Id, 1, session);
                                    if (mailbox != null)
                                    {
                                        if (mailbox.IsNew == 0 && mailbox.IsViewed == 1)
                                        {
                                            imgPartnerLogo.Attributes["style"] = "margin-bottom:0px; margin-left:" + marginLeft + "px; border:1px solid #000000";

                                            if (mailbox.DateViewed != null)
                                            {
                                                imgPartnerLogo.ToolTip = partner.CompanyName + " has read this mesage at " + Convert.ToDateTime(mailbox.DateViewed).ToString("F");  //GlobalMethods.FixDateWithoutHour(Convert.ToDateTime(mailbox.DateViewed).ToString("dd/MM/yyyy"));
                                                imgPartnerLogo.AlternateText = partner.CompanyName + " has read this mesage at " + Convert.ToDateTime(mailbox.DateViewed).ToString("F");  //GlobalMethods.FixDateWithoutHour(Convert.ToDateTime(mailbox.DateViewed).ToString("dd/MM/yyyy"));
                                            }
                                            else
                                            {
                                                imgPartnerLogo.ToolTip = partner.CompanyName + " has read this mesage";
                                                imgPartnerLogo.AlternateText = partner.CompanyName + " has read this mesage";
                                            }

                                            aPartnerLogo.Controls.Add(imgPartnerLogo);
                                            phPartnersLogo.Controls.Add(aPartnerLogo);
                                        }
                                        else if (mailbox.IsNew == 1 && mailbox.IsViewed == 0)
                                        {
                                            imgPartnerLogo.Visible = false;
                                            //imgPartnerLogo.Attributes["style"] = "margin-top:55px; margin-left:" + marginLeft + "px; border:0px solid #ffffff";
                                            //imgPartnerLogo.ToolTip = partner.CompanyName + " has not read this mesage yet";
                                        }
                                        else
                                        {
                                            imgPartnerLogo.Attributes["style"] = "margin-bottom:0px; margin-left:" + marginLeft + "px; border:1px solid #000000";
                                            imgPartnerLogo.Visible = false;
                                        }

                                        marginLeft += 22;

                                        lblMailBoxContent.Text = GlobalMethods.FixParagraphsView(item["message_content"].Text, 1);
                                        lblLastUpdate.Text = "written at " + GlobalMethods.FixDateWithoutHour(Convert.ToDateTime(item["date_created"].Text).ToString("dd/MM/yyyy"));

                                        //DateTime? dateViewed = null;
                                        string dateViewed = (!string.IsNullOrEmpty(item["date_viewed"].Text) && item["date_viewed"].Text.ToString() != "&nbsp;") ? item["date_viewed"].Text.ToString() : "";

                                        FixMailBoxMessageStatus(item, lblMsgStatus, mailbox.IsNew, mailbox.IsViewed, dateViewed);
                                    }
                                }
                            }
                        }
                        else
                        {
                            ElioUsers member = Sql.GetUserById(Convert.ToInt32(item["user_id"].Text), session);
                            if (member != null)
                            {
                                imgLogo.ImageUrl = member.CompanyLogo;
                                aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.Profile(member);
                                imgLogo.ToolTip = "See " + member.CompanyName + "'s profile";
                                imgLogo.AlternateText = "See " + member.CompanyName + "'s profile";

                                divMessage.Attributes["class"] = "d-flex flex-column mb-5 align-items-end";
                                divContentMessage.Attributes["class"] = "mt-2 rounded p-5 bg-light-primary text-dark-50 font-weight-bold font-size-lg text-left max-w-400p";

                                List<ElioUsers> groupMembersImgsAndNames = new List<ElioUsers>();
                                int isActive = 1;
                                int isPublic = 1;

                                bool hasMembers = GlobalDBMethods.GetGroupMembersForMessages(CollaborationGroupId, isActive, isPublic, member.CompanyType, member.Id, out groupMembersImgsAndNames, session);      //SqlCollaboration.GetCollaborationPartnersImagesUrlByMailBoxIdAndUserType(Convert.ToInt32(item["id"].Text), companyType, session);
                                if (hasMembers)
                                {
                                    int imgIndex = 0;
                                    int marginRight = 40;

                                    foreach (ElioUsers partner in groupMembersImgsAndNames)
                                    {
                                        imgIndex++;
                                        HtmlAnchor aPartnerLogo = new HtmlAnchor();
                                        aPartnerLogo.HRef = ControlLoader.Profile(partner);
                                        aPartnerLogo.Target = "_blank";

                                        Image imgPartnerLogo = new Image();
                                        imgPartnerLogo.ID = "imgBtnPartnerLogo_" + item["id"].Text + "_" + imgIndex;
                                        imgPartnerLogo.ImageUrl = partner.CompanyLogo;
                                        imgPartnerLogo.CssClass = "img-circle chat-avatar";
                                        imgPartnerLogo.Width = 18;
                                        imgPartnerLogo.Height = 18;
                                        imgPartnerLogo.ToolTip = "See " + partner.CompanyName + "'s profile";
                                        imgPartnerLogo.AlternateText = "See " + partner.CompanyName + "'s profile";

                                        ElioCollaborationUsersGroupMaibox mailbox = SqlCollaboration.GetCollaborationUsersGroupMailBoxByMailboxId(Convert.ToInt32(item["id"].Text), partner.Id, 1, session);
                                        if (mailbox != null)
                                        {
                                            SetUserMailboxAsViewed(vSession.User.Id, mailbox);

                                            if (mailbox.IsNew == 0 && mailbox.IsViewed == 1)
                                            {
                                                imgPartnerLogo.Attributes["style"] = "margin-bottom:0px; margin-right:" + marginRight + "px; border:1px solid #000000";

                                                if (mailbox.DateViewed != null)
                                                {
                                                    imgPartnerLogo.ToolTip = partner.CompanyName + " has read this mesage at " + GlobalMethods.FixDateWithoutHour(Convert.ToDateTime(mailbox.DateViewed).ToString("dd/MM/yyyy"));
                                                    imgPartnerLogo.AlternateText = partner.CompanyName + " has read this mesage at " + GlobalMethods.FixDateWithoutHour(Convert.ToDateTime(mailbox.DateViewed).ToString("dd/MM/yyyy"));
                                                }
                                                else
                                                {
                                                    imgPartnerLogo.ToolTip = partner.CompanyName + " has read this mesage";
                                                    imgPartnerLogo.AlternateText = partner.CompanyName + " has read this mesage";
                                                }

                                                aPartnerLogo.Controls.Add(imgPartnerLogo);
                                                phPartnersLogo.Controls.Add(aPartnerLogo);
                                            }
                                            else if (mailbox.IsNew == 1 && mailbox.IsViewed == 0)
                                            {
                                                imgPartnerLogo.Visible = false;
                                                //imgPartnerLogo.Attributes["style"] = "margin-top:55px; margin-right:" + marginRight + "px; border:0px solid #ffffff";
                                                //imgPartnerLogo.ToolTip = partner.CompanyName + " has not read this mesage yet";
                                            }
                                            else
                                            {
                                                imgPartnerLogo.Attributes["style"] = "margin-bottom:0px; margin-right:" + marginRight + "px; border:1px solid #000000";
                                                imgPartnerLogo.Visible = false;
                                            }

                                            marginRight += 22;

                                            lblMailBoxContent.Text = GlobalMethods.FixParagraphsView(item["message_content"].Text, 1);
                                            lblLastUpdate.Text = "written at " + GlobalMethods.FixDateWithoutHour(Convert.ToDateTime(item["date_created"].Text).ToString("dd/MM/yyyy"));

                                            //DateTime? dateViewed = null;
                                            string dateViewed = (!string.IsNullOrEmpty(item["date_viewed"].Text) && item["date_viewed"].Text.ToString() != "&nbsp;") ? item["date_viewed"].Text.ToString() : "";

                                            FixMailBoxMessageStatus(item, lblMsgStatus, mailbox.IsNew, mailbox.IsViewed, dateViewed);
                                        }
                                    }
                                }
                            }
                        }

                        //HyperLink hpLnkPreViewLogFiles = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkPreViewLogFiles");
                        //ImageButton imgBtnPreviewLogFiles = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnPreviewLogFiles");

                        Label hpLnkPreViewLogFiles = (Label)ControlFinder.FindControlRecursive(item, "HpLnkPreViewLogFiles");
                        HtmlAnchor aImgBtnPreviewLogFiles = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aImgBtnPreviewLogFiles");

                        ElioCollaborationUsersLibraryFiles att = SqlCollaboration.GetCollaborationUsersLibraryAttachmentFilesByMailboxIdUserId(Convert.ToInt32(item["id"].Text), vSession.User.Id, session);
                        if (att != null)
                        {
                            aImgBtnPreviewLogFiles.Visible = true;
                            hpLnkPreViewLogFiles.Text = att.FileName;
                            //string url = (HttpContext.Current.Request.IsLocal) ? "http://" : "https://";
                            //aImgBtnPreviewLogFiles.HRef= url + HttpContext.Current.Request.Url.Authority + System.Configuration.ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"] + "/" + att.FilePath;
                            aImgBtnPreviewLogFiles.HRef = System.Configuration.ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"] + "/" + att.FilePath;
                            aImgBtnPreviewLogFiles.Target = "_blank";

                            //hpLnkPreViewLogFiles.NavigateUrl = url + HttpContext.Current.Request.Url.Authority + System.Configuration.ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"] + "/" + att.FilePath;      //defaultCategory.CategoryDescription + "/" + vSession.User.GuId + "/" + att.FileName;
                            if (vSession.User.Id == att.UserId)
                                SqlCollaboration.SetCollaborationUsersLibraryFilesAsViewedByMailboxIdAndUserId(att.MailboxId, vSession.User.Id, session);
                        }
                        else
                        {
                            aImgBtnPreviewLogFiles.Visible = false;
                            hpLnkPreViewLogFiles.Visible = false;
                        }
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

        protected void RdgGroupMailBox_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (CollaborationGroupId != -1)
                {
                    List<ElioCollaborationMailbox> groupMails = SqlCollaboration.GetCollaborationMailboxByGroupId(CollaborationGroupId, 1, 0, session);

                    if (groupMails.Count > 0)
                    {
                        RdgGroupMailBox.Visible = true;
                        UcMailBoxListMessageAlert.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("user_id");
                        table.Columns.Add("message_content");
                        table.Columns.Add("date_created");
                        table.Columns.Add("date_send");
                        table.Columns.Add("date_received");
                        table.Columns.Add("date_viewed");
                        table.Columns.Add("total_reply_comments");
                        table.Columns.Add("is_public");

                        foreach (ElioCollaborationMailbox mail in groupMails)
                        {
                            table.Rows.Add(mail.Id, mail.UserId, mail.MessageContent, mail.DateCreated, mail.DateSend, mail.DateReceived, "", mail.TotalReplyComments, mail.IsPublic);
                        }

                        RdgGroupMailBox.DataSource = table;
                    }
                    else
                    {
                        FixPage();
                    }
                }
                else
                {
                    FixPage();
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