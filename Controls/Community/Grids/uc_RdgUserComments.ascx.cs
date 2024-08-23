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

namespace WdS.ElioPlus.Controls.Community.Grids
{
    public partial class uc_RdgUserComments : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateStrings();

                if (!RdgPostComments.Visible)
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "message", "7")).Text, MessageTypes.Warning, true, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void UpdateStrings()
        {

        }

        private void RedirectToLoginPage()
        {
            GlobalMethods.ClearCriteriaSession(vSession, false);

            Response.Redirect(ControlLoader.CommunityLogin, false);
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

        protected void ImgBtnUser_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton btn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

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

                    ElioCommunityPostsVotes userVote = Sql.GetPostVotesByUserId(vSession.User.Id, Convert.ToInt32(item["post_id"].Text), session);
                    if (userVote == null)
                    {
                        try
                        {
                            session.BeginTransaction();

                            userVote = new ElioCommunityPostsVotes();

                            userVote.UserId = vSession.User.Id;
                            userVote.CommunityPostId = Convert.ToInt32(item["post_id"].Text);

                            DataLoader<ElioCommunityPostsVotes> loader = new DataLoader<ElioCommunityPostsVotes>(session);
                            loader.Insert(userVote);

                            ElioCommunityPosts post = Sql.GetPostByPostId(Convert.ToInt32(item["post_id"].Text), session);
                            if (post != null)
                            {
                                post.TotalVotes++;

                                DataLoader<ElioCommunityPosts> loader2 = new DataLoader<ElioCommunityPosts>(session);
                                loader2.Update(post);

                                session.CommitTransaction();

                                RdgPostComments.Rebind();
                            }
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }
                    }

                    session.OpenConnection();

                    if (vSession.User.Id != Convert.ToInt32(item["creator_user_id"].Text))
                    {
                        #region Send Email

                        EmailSenderLib.SendCommunityNotificationEmail(vSession.User, Convert.ToInt32(item["post_id"].Text), EmailNotificationDesctriptions.CommunityUpvoteUserPost, CommunityEmailNotifications.UpvoteUserPost, vSession.Lang, session);

                        #endregion
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

        #endregion

        #region Grids

        protected void RdgPostComments_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            vSession.CurrentGridPageIndex = e.NewPageIndex;
        }

        protected void RdgPostComments_ItemCreated(object sender, GridItemEventArgs e)
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

        protected void RdgPostComments_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    LinkButton lnkBtnTitle = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnTitle");
                    HyperLink hpLnkUrlTopic = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkUrlTopic");
                    Label lblVotes = (Label)ControlFinder.FindControlRecursive(item, "LblVotes");
                    Label lblUrlTopic = (Label)ControlFinder.FindControlRecursive(item, "LblUrlTopic");
                    Label lblComment = (Label)ControlFinder.FindControlRecursive(item, "LblComment");
                    Label lblSysdate = (Label)ControlFinder.FindControlRecursive(item, "LblSysdate");
                    ImageButton imgBtnUpVote = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnUpVote");
                    ImageButton imgBtnLogo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnLogo");

                    if (string.IsNullOrEmpty(item["topic_url"].Text) || item["topic_url"].Text == "&nbsp;")
                    {
                        hpLnkUrlTopic.Visible = false;
                        lnkBtnTitle.Visible = true;
                        lnkBtnTitle.Text = item["topic"].Text;
                    }
                    else
                    {
                        hpLnkUrlTopic.Visible = true;
                        lnkBtnTitle.Visible = false;
                        hpLnkUrlTopic.Text = item["topic"].Text;
                        hpLnkUrlTopic.NavigateUrl = item["topic_url"].Text;

                        if (!item["topic_url"].Text.Contains("http://") && !item["topic_url"].Text.Contains("https://") && !item["topic_url"].Text.Contains("ftp://"))
                        {
                            item["topic_url"].Text = "http://" + item["topic_url"].Text;
                        }
                        Uri myUri = new Uri(item["topic_url"].Text);
                        lblUrlTopic.Text = myUri.Host;
                        lblUrlTopic.Visible = true;
                    }

                    if (vSession.User != null)
                    {
                        imgBtnUpVote.Enabled = (!Sql.UserHasUpVotePost(vSession.User.Id, Convert.ToInt32(item["post_id"].Text), session)) ? true : false;
                        imgBtnUpVote.ToolTip = (imgBtnUpVote.Enabled) ? "Up Vote" : "You have already Up Vote this post";
                        imgBtnUpVote.ImageUrl = (imgBtnUpVote.Enabled) ? "/images/icons/small/099288-designbump-logo.png" : "/images/icons/small/099288-designbump-logo1.png";
                    }
                    else
                    {
                        imgBtnUpVote.ToolTip = "You must log in to Up Vote";
                        imgBtnUpVote.ImageUrl = "/images/icons/small/099288-designbump-logo1.png";
                    }

                    lblVotes.Text = item["total_votes"].Text;
                    imgBtnLogo.ImageUrl = (item["personal_image"].Text != "&nbsp;") ? item["personal_image"].Text : "~/images/icons/personal_img_3.png";

                    List<ElioCommunityPostsCommentsIJUsers> postComments = Sql.GetPostAllCommentsIJUsers(Convert.ToInt32(item["post_id"].Text), session);

                    foreach (ElioCommunityPostsCommentsIJUsers postComment in postComments)
                    {
                        lblComment.Text = GlobalMethods.FixParagraphsView(postComment.Comment);

                        lblSysdate.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communitycomments", "label", "2")).Text + GlobalMethods.FixDate(postComment.SysDate.ToString());
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

        protected void RdgPostComments_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.ElioCompanyDetailsView != null)
                {
                    List<ElioCommunityPosts> posts = Sql.GetCommunityPostsIJCommentsByCommentUserId(vSession.ElioCompanyDetailsView.Id, session);

                    if (posts.Count > 0)
                    {
                        RdgPostComments.Visible = true;
                        UcMessageAlert.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("post_id");
                        table.Columns.Add("creator_user_id");
                        table.Columns.Add("topic");
                        table.Columns.Add("topic_url");
                        table.Columns.Add("total_votes");

                        foreach (ElioCommunityPosts post in posts)
                        {
                            table.Rows.Add(post.Id, post.CreatorUserId, post.Topic, post.TopicUrl, post.TotalVotes);
                        }

                        RdgPostComments.DataSource = table;
                    }
                    else
                    {
                        RdgPostComments.Visible = false;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "message", "7")).Text, MessageTypes.Warning, true, true, false);
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