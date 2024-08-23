using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.Localization;
using System.Data;
using WdS.ElioPlus.Objects;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.IO;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus.Controls.Community.Grids
{
    public partial class uc_RdgPosts : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                if (!IsPostBack)
                {
                    UpdateStrings();
                }

                if (!RdgPostsResults.Visible)
                {
                    FixMessageAlertByCase();
                }
                else
                {
                    if (vSession.CurrentGridPageIndex != 0)
                    {
                        RdgPostsResults.CurrentPageIndex = vSession.CurrentGridPageIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void FixMessageAlertByCase()
        {
            string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "message", "5")).Text;

            //if (vSession.Page == ControlLoader.CommunityUserProfile(vSession.ElioCompanyDetailsView))
            if (vSession.ElioCompanyDetailsView != null)
            {
                RadTabStrip rtbInfo = (RadTabStrip)ControlFinder.FindControlBackWards(this, "RtbInfo");

                if (rtbInfo.Tabs[0].Selected)
                {
                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "message", "1")).Text;
                }
                else if (rtbInfo.Tabs[2].Selected)
                {
                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "message", "3")).Text;
                }
            }

            GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Warning, true, true, false);
        }

        private void UpdateStrings()
        {
            
        }

        private void RedirectToLoginPage()
        {
            GlobalMethods.ClearCriteriaSession(vSession, false);            
            Response.Redirect(vSession.Page=ControlLoader.CommunityLogin, false);
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
                GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
            }
        }
       
        #endregion

        #region Buttons

        protected void LnkBtnSummary_OnClick(object sender, EventArgs args)
        {
            try
            {
                LinkButton lnkBtn = (LinkButton)sender;
                GridDataItem item = (GridDataItem)lnkBtn.NamingContainer;

                RadPanelBar rpbPost = (RadPanelBar)ControlFinder.FindControlRecursive(item, "RpbPost");

                if (!rpbPost.Items[0].Expanded)
                {
                    rpbPost.CssClass = "unhidden";
                    rpbPost.Items[0].Expanded = true;
                }
                else
                {
                    rpbPost.CssClass = "hidden";
                    rpbPost.Items[0].Expanded = false;
                    GlobalMethods.ClearCriteriaSession(vSession, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void LnkBtnUser_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LinkButton lnkBtn = (LinkButton)sender;
                GridDataItem item = (GridDataItem)lnkBtn.NamingContainer;

                RedirectToUserDetails(item);
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

        protected void ImgBtnUser_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                //SeeUserDetails(item);

                RedirectToUserDetails(item);
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

        protected void ImgBtnUpVote_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                if (vSession.User != null)
                {
                    #region Allow User To Up Vote Post

                    ElioCommunityPostsVotes userVote = Sql.GetPostVotesByUserId(vSession.User.Id, Convert.ToInt32(item["id"].Text), session);
                    if (userVote == null)
                    {
                        try
                        {
                            #region Allow User To Up Vote Post

                            session.BeginTransaction();

                            userVote = new ElioCommunityPostsVotes();

                            userVote.UserId = vSession.User.Id;
                            userVote.CommunityPostId = Convert.ToInt32(item["id"].Text);

                            DataLoader<ElioCommunityPostsVotes> loader = new DataLoader<ElioCommunityPostsVotes>(session);
                            loader.Insert(userVote);

                            ElioCommunityPosts post = Sql.GetPostByPostId(Convert.ToInt32(item["id"].Text), session);
                            if (post != null)
                            {
                                post.TotalVotes++;

                                DataLoader<ElioCommunityPosts> loader2 = new DataLoader<ElioCommunityPosts>(session);
                                loader2.Update(post);

                                session.CommitTransaction();

                                RdgPostsResults.Rebind();

                                //post.CreatorUserId

                                if (vSession.User.Id != post.CreatorUserId)
                                {
                                    #region Send Email

                                    try
                                    {
                                        session.OpenConnection();

                                        EmailSenderLib.SendCommunityNotificationEmail(vSession.User, post.Id, EmailNotificationDesctriptions.CommunityUpvoteUserPost, CommunityEmailNotifications.UpvoteUserPost, vSession.Lang, session);
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    }

                                    #endregion
                                }
                            }

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }
                    }

                    #endregion
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

        protected void LnkBtnComments_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LinkButton lnkBtn = (LinkButton)sender;
                GridDataItem item = (GridDataItem)lnkBtn.NamingContainer;

                vSession.LoadedCommunityPostDetails = null;

                vSession.LoadedCommunityPostDetails = Sql.GetCommunityPostsDetailsById(Convert.ToInt32(item["id"].Text), session);
                if (vSession.LoadedCommunityPostDetails != null)
                {
                    Response.Redirect(ControlLoader.CommunityComments(vSession.LoadedCommunityPostDetails.Id, vSession.LoadedCommunityPostDetails.Topic), false);
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

        protected void ImgBtnSetNotPublic_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ImageButton imgBtnSetNotPublic = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtnSetNotPublic.NamingContainer;

                    ElioCommunityPosts post = Sql.GetCommunityPostsById(Convert.ToInt32(item["id"].Text), session);
                    if (post != null)
                    {
                        post.IsPublic = 0;
                        post.LastUpdate = DateTime.Now;

                        DataLoader<ElioCommunityPosts> loader = new DataLoader<ElioCommunityPosts>(session);
                        loader.Update(post);

                        RdgPostsResults.Rebind();
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

        protected void ImgBtnMustRead_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session))
                {
                    ImageButton imgBtnMustRead = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtnMustRead.NamingContainer;

                    ElioCommunityPosts post = Sql.GetCommunityPostsById(Convert.ToInt32(item["id"].Text), session);
                    if (post != null)
                    {
                        post.MustRead = (post.MustRead == 0) ? 1 : 0;
                        post.LastUpdate = DateTime.Now;

                        DataLoader<ElioCommunityPosts> loader = new DataLoader<ElioCommunityPosts>(session);
                        loader.Update(post);

                        RdgPostsResults.Rebind();
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.CommunityLogin, false);
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

        #region Grids

        protected void RdgPostsResults_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            vSession.CurrentGridPageIndex = e.NewPageIndex;
        }

        protected void RdgPostsResults_ItemCreated(object sender, GridItemEventArgs e)
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

        protected void RdgPostsResults_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ImageButton imgBtnMustRead = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnMustRead");

                    if (vSession.User != null)
                    {
                        bool isAdministrator = Sql.IsUserAdministrator(vSession.User.Id, session);

                        ImageButton imgBtnSetNotPublic = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSetNotPublic");
                        imgBtnSetNotPublic.Visible = (isAdministrator) ? true : false;
                        imgBtnSetNotPublic.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "tooltip", "1")).Text;

                        imgBtnMustRead.Visible = isAdministrator;
                        imgBtnMustRead.ToolTip = (item["must_read"].Text == "0") ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "tooltip", "8")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "tooltip", "9")).Text;
                        imgBtnMustRead.ImageUrl = (item["must_read"].Text == "0") ? "/images/icons/small/flag_1.png" : "/images/icons/small/unflag_1.png";

                        if (!isAdministrator && item["must_read"].Text == "1")
                        {
                            imgBtnMustRead.Visible = true;
                            imgBtnMustRead.ImageUrl = "/images/icons/small/flag_2.png";
                            imgBtnMustRead.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "tooltip", "10")).Text;
                        }
                        else if (!isAdministrator && item["must_read"].Text == "0")
                        {
                            imgBtnMustRead.Visible = false;
                        }
                    }
                    else
                    {
                        if (item["must_read"].Text == "1")
                        {
                            imgBtnMustRead.Visible = true;
                            imgBtnMustRead.ImageUrl = "/images/icons/small/flag_2.png";
                            imgBtnMustRead.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "tooltip", "10")).Text;
                        }
                        else
                        {
                            imgBtnMustRead.Visible = false;
                        }
                    }

                    HtmlAnchor aTitle = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aTitle");
                    Label lblTitle = (Label)ControlFinder.FindControlRecursive(aTitle, "LblTitle");
                    HtmlAnchor aUrlTopic = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aUrlTopic");
                    Label lblaUrlTopic = (Label)ControlFinder.FindControlRecursive(aUrlTopic, "LblaUrlTopic");

                    if (string.IsNullOrEmpty(item["topic_url"].Text) || item["topic_url"].Text == "&nbsp;")
                    {
                        aUrlTopic.Visible = false;
                        aTitle.Visible = true;
                        lblTitle.Text = item["topic"].Text;
                    }
                    else
                    {
                        aUrlTopic.Visible = true;
                        aTitle.Visible = false;
                        lblaUrlTopic.Text = item["topic"].Text;
                        aUrlTopic.HRef = item["topic_url"].Text;

                        Label lblUrlTopic = (Label)ControlFinder.FindControlRecursive(item, "LblUrlTopic");
                        //lblUrlTopic.Text = item["topic_url"].Text.Replace("https://", string.Empty).Replace("http://", string.Empty);
                        if (!item["topic_url"].Text.Contains("http://") && !item["topic_url"].Text.Contains("https://") && !item["topic_url"].Text.Contains("ftp://"))
                        {
                            item["topic_url"].Text = "http://" + item["topic_url"].Text;
                        }
                        Uri myUri = new Uri(item["topic_url"].Text);
                        lblUrlTopic.Text = myUri.Host;
                        lblUrlTopic.Visible = true;
                    }

                    RadPanelBar rpbPost = (RadPanelBar)ControlFinder.FindControlRecursive(item, "RpbPost");
                    Label lblPostContent = (Label)ControlFinder.FindControlRecursive(rpbPost, "LblPostContent");
                    Image imgSeePost = (Image)ControlFinder.FindControlRecursive(item, "ImgSeePost");
                    imgSeePost.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "tooltip", "2")).Text;
                    Label lblLastUpdate = (Label)ControlFinder.FindControlRecursive(item, "LblLastUpdate");
                    Label lblVotes = (Label)ControlFinder.FindControlRecursive(item, "LblVotes");
                    Label lblComments = (Label)ControlFinder.FindControlRecursive(item, "LblComments");
                    HtmlAnchor aUsername = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aUsername");
                    Label lblUsername = (Label)ControlFinder.FindControlRecursive(aUsername, "LblUsername");
                    Label lblUser = (Label)ControlFinder.FindControlRecursive(item, "LblUser");
                    ImageButton imgBtnLogo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnLogo");

                    HtmlAnchor aComments = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aComments");
                    vSession.LoadedCommunityPostDetails = Sql.GetCommunityPostsDetailsById(Convert.ToInt32(item["id"].Text), session);
                    if (vSession.LoadedCommunityPostDetails != null)
                    {
                        aComments.HRef = ControlLoader.CommunityComments(vSession.LoadedCommunityPostDetails.Id, vSession.LoadedCommunityPostDetails.Topic);
                    }
                    
                    Label lblaComments = (Label)ControlFinder.FindControlRecursive(aComments, "LblaComments");

                    if (Request.RawUrl.Contains("/community/members") && vSession.ElioCompanyDetailsView.Id == Convert.ToInt32(item["creator_user_id"].Text))
                    {
                        imgBtnLogo.Visible = false;
                    }
                    else
                    {
                        imgBtnLogo.Visible = true;
                    }
                    //imgBtnLogo.Visible = (Request.RawUrl.Contains("/community/members")) ? false : true;
                    //if (item["personal_image"].Text != "&nbsp;" && !string.IsNullOrEmpty(item["personal_image"].Text) && !item["personal_image"].Text.StartsWith("https://media.licdn"))
                    //{
                    //    if (File.Exists(item["personal_image"].Text))
                    //        imgBtnLogo.ImageUrl = item["personal_image"].Text;
                    //    else
                    //        imgBtnLogo.ImageUrl = "/images/icons/personal_img_3.png";
                    //}
                    //else
                    //    imgBtnLogo.ImageUrl = "/images/icons/personal_img_3.png";

                    ElioUsers userPost = Sql.GetUserById(vSession.LoadedCommunityPostDetails.CreatorUserId, session);
                    if (userPost != null)
                        imgBtnLogo.ImageUrl = GlobalMethods.LoadUserPersonalImageDefault(userPost, true, true);
                    else
                        imgBtnLogo.ImageUrl = (item["personal_image"].Text != "&nbsp;") ? item["personal_image"].Text : "/images/icons/personal_img_3.png";
                    
                    imgBtnLogo.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "tooltip", "3")).Text;

                    if (Request.RawUrl.Contains("/community/members") && vSession.ElioCompanyDetailsView.Id == Convert.ToInt32(item["creator_user_id"].Text))
                    {
                        lblUser.Visible = false;
                    }
                    else
                    {
                        lblUser.Visible = true;
                    }

                    if (Request.RawUrl.Contains("/community/members") && vSession.ElioCompanyDetailsView.Id == Convert.ToInt32(item["creator_user_id"].Text))
                        aUsername.Visible = false;
                    else
                        aUsername.Visible = true;

                    //lblUser.Visible = (Request.RawUrl.Contains("/community/members")) ? false : true;
                    //lknBtnUsername.Visible = (Request.RawUrl.Contains("/community/members")) ? false : true;

                    lblPostContent.Text = GlobalMethods.FixParagraphsView(item["post"].Text);
                    lblLastUpdate.Text = "written at " + GlobalMethods.FixDate(item["sysdate"].Text);
                    lblUser.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "label", "6")).Text;
                    lblUsername.Text = (item["first_name"].Text != "&nbsp;" && item["last_name"].Text != "&nbsp;") ? item["first_name"].Text + " " + item["last_name"].Text : item["username"].Text;
                    lblUsername.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "tooltip", "3")).Text;

                    ImageButton imgBtnUpVote = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnUpVote");

                    if (vSession.User != null)
                    {
                        imgBtnUpVote.Enabled = (!Sql.UserHasUpVotePost(vSession.User.Id, Convert.ToInt32(item["id"].Text), session)) ? true : false;
                        imgBtnUpVote.ToolTip = (imgBtnUpVote.Enabled) ? "Up Vote" : "You have already Up Vote this post";
                        imgBtnUpVote.ImageUrl = (imgBtnUpVote.Enabled) ? "/images/icons/small/099288-designbump-logo.png" : "/images/icons/small/099288-designbump-logo1.png";
                    }
                    else
                    {
                        imgBtnUpVote.ToolTip = "You must log in to Up Vote";
                        imgBtnUpVote.ImageUrl = "/images/icons/small/099288-designbump-logo1.png";
                    }

                    lblVotes.Text = item["total_votes"].Text;
                    lblComments.Text = item["total_comments"].Text;
                    //lblCommentsCount.Text = lblComments.Text;

                    vSession.LoadedCommunityPostDetails = Sql.GetCommunityPostsDetailsById(Convert.ToInt32(item["id"].Text), session);
                    if (vSession.LoadedCommunityPostDetails != null)
                    {
                        aTitle.HRef = ControlLoader.CommunityComments(vSession.LoadedCommunityPostDetails.Id, vSession.LoadedCommunityPostDetails.Topic);
                    }

                    lblaComments.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "label", "7")).Text;

                    LinkButton lnkBtnSummary = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnSummary");
                    lnkBtnSummary.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "label", "10")).Text;

                    #region User Details

                    //RadPanelBar rpbUserDetails = (RadPanelBar)ControlFinder.FindControlRecursive(item, "RpbUserDetails");

                    //Label lblPersonalImage = (Label)ControlFinder.FindControlRecursive(rpbUserDetails, "LblPersonalImage");
                    //Label lblLastNameText = (Label)ControlFinder.FindControlRecursive(rpbUserDetails, "LblLastNameText");
                    //Label lblLastName = (Label)ControlFinder.FindControlRecursive(rpbUserDetails, "LblLastName");
                    //Label lblFirstNameText = (Label)ControlFinder.FindControlRecursive(rpbUserDetails, "LblFirstNameText");
                    //Label lblFirstName = (Label)ControlFinder.FindControlRecursive(rpbUserDetails, "LblFirstName");
                    //Label lblJobPositionText = (Label)ControlFinder.FindControlRecursive(rpbUserDetails, "LblJobPositionText");
                    //Label lblJobPosition = (Label)ControlFinder.FindControlRecursive(rpbUserDetails, "LblJobPosition");
                    //Label lblSummaryText = (Label)ControlFinder.FindControlRecursive(rpbUserDetails, "LblSummaryText");
                    //Label lblSummary = (Label)ControlFinder.FindControlRecursive(rpbUserDetails, "LblSummary");

                    //Image imgPersonalImage = (Image)ControlFinder.FindControlRecursive(item, "ImgPersonalImage");

                    //lblPersonalImage.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "1")).Text;
                    //lblLastNameText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "2")).Text;
                    //lblFirstNameText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "3")).Text;
                    //lblJobPositionText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "4")).Text;
                    //lblSummaryText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "5")).Text;

                    //imgPersonalImage.ImageUrl = (item["personal_image"].Text == "&nbsp;") ? "~/images/personal_img_3.png" : item["personal_image"].Text;
                    //lblLastName.Text = item["last_name"].Text;
                    //lblFirstName.Text = item["first_name"].Text;
                    //lblJobPosition.Text = item["position"].Text;
                    //lblSummary.Text = item["community_summary_text"].Text;

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

        protected void RdgPostsResults_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "message", "5")).Text;

                if (vSession.CommunityPostsStrQueryAppend != null || vSession.ElioCompanyDetailsView != null)
                {
                    List<ElioCommunityPostsIJUsers> posts = new List<ElioCommunityPostsIJUsers>();

                    //if(vSession.Page != ControlLoader.CommunityUserProfile(vSession.ElioCompanyDetailsView))
                    if (!Request.RawUrl.Contains("/community/members"))
                    {
                        posts = Sql.GetAllCommunityPostsDetailsOrderBy(vSession.CommunityPostsStrQueryAppend, session);
                    }
                    else
                    {
                        RadTabStrip rtbInfo = (RadTabStrip)ControlFinder.FindControlBackWards(this, "RtbInfo");

                        if (rtbInfo.Tabs[0].Selected)
                        {
                            if (vSession.ElioCompanyDetailsView != null)
                            {
                                posts = Sql.GetUserCommunityPostsDetails(vSession.ElioCompanyDetailsView.Id, session);
                            }

                            alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "message", "1")).Text;
                        }
                        else if (rtbInfo.Tabs[2].Selected)
                        {
                            posts = Sql.GetUserUpVotedCommunityPostsDetails(vSession.ElioCompanyDetailsView.Id, session);
                            alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "message", "3")).Text;
                        }
                    }

                    if (posts.Count > 0)
                    {
                        RdgPostsResults.Visible = true;
                        UcMessageAlert.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("creator_user_id");
                        table.Columns.Add("topic");
                        table.Columns.Add("topic_url");
                        table.Columns.Add("post");
                        table.Columns.Add("sysdate");
                        table.Columns.Add("must_read");
                        table.Columns.Add("total_votes");
                        table.Columns.Add("total_comments");
                        table.Columns.Add("username");
                        table.Columns.Add("last_name");
                        table.Columns.Add("first_name");
                        table.Columns.Add("personal_image");
                        table.Columns.Add("position");
                        table.Columns.Add("community_summary_text");

                        foreach (ElioCommunityPostsIJUsers post in posts)
                        {
                            table.Rows.Add(post.Id, post.CreatorUserId, post.Topic, post.TopicUrl, GlobalMethods.FixParagraphsView(post.Post), post.SysDate, post.MustRead, post.TotalVotes, post.TotalComments, post.Username, post.LastName, post.FirstName, post.PersonalImage, GlobalMethods.FixParagraphsView(post.Position), GlobalMethods.FixParagraphsView(post.CommunitySummaryText));
                        }

                        RdgPostsResults.DataSource = table;
                    }
                    else
                    {
                        RdgPostsResults.Visible = false;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Warning, true, true, false);
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.CommunityPosts, false);
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