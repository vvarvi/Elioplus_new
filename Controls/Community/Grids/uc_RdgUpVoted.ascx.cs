using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using System.Data;
using WdS.ElioPlus.Objects;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.EmailNotificationSender;

namespace WdS.ElioPlus.Controls.Community.Grids
{
    public partial class uc_RdgUpVoted : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void RedirectToLoginPage()
        {
            GlobalMethods.ClearCriteriaSession(vSession, false);
            Response.Redirect(ControlLoader.CommunityLogin, false);            
        }

        #endregion

        #region Buttons

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

                            if (vSession.User.Id != post.CreatorUserId)
                            {
                                #region Send Email

                                try
                                {
                                    EmailSenderLib.SendCommunityNotificationEmail(vSession.User, post.Id, EmailNotificationDesctriptions.CommunityUpvoteUserPost, CommunityEmailNotifications.UpvoteUserPost, vSession.Lang, session);
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }

                                #endregion
                            }
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
                session.RollBackTransaction();
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

                ElioCommunityPostsIJUsers selectedPost = Sql.GetCommunityPostsDetailsById(Convert.ToInt32(item["id"].Text), session);
                if (selectedPost != null)
                {
                    vSession.LoadedCommunityPostDetails = selectedPost;

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

                        DataLoader<ElioCommunityPosts> loader = new DataLoader<ElioCommunityPosts>(session);
                        loader.Update(post);

                        //RdgPostsResults.Rebind();
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

        protected void LnkBtnTopic_OnClick(object sender, EventArgs args)
        {
            try
            {
                LinkButton lnkBtn = (LinkButton)sender;
                GridDataItem item = (GridDataItem)lnkBtn.NamingContainer;

                RadPanelBar rpbPost = (RadPanelBar)ControlFinder.FindControlRecursive(item, "RpbPost");

                rpbPost.CssClass = (!rpbPost.Items[0].Expanded) ? "unhidden" : "hidden";
                rpbPost.Items[0].Expanded = (!rpbPost.Items[0].Expanded) ? true : false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Grids

        protected void RdgUpVoted_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            vSession.CurrentGridPageIndex = e.NewPageIndex;
        }

        protected void RdgUpVoted_ItemCreated(object sender, GridItemEventArgs e)
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

        protected void RdgUpVoted_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    if (vSession.User != null)
                    {
                        ImageButton imgBtnSetNotPublic = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSetNotPublic");
                        imgBtnSetNotPublic.Visible = (Sql.IsUserAdministrator(vSession.User.Id, session)) ? true : false;
                        imgBtnSetNotPublic.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "tooltip", "1")).Text;
                    }

                    HyperLink hpLnkUrlTopic = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkUrlTopic");
                    hpLnkUrlTopic.Visible = (string.IsNullOrEmpty(item["topic_url"].Text)) ? false : true;
                    if (hpLnkUrlTopic.Visible)
                    {
                        hpLnkUrlTopic.Text = item["topic_url"].Text;
                        hpLnkUrlTopic.NavigateUrl = item["topic_url"].Text;
                    }

                    LinkButton lnkBtnTopic = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnTopic");
                    RadPanelBar rpbPost = (RadPanelBar)ControlFinder.FindControlRecursive(item, "RpbPost");
                    Label lblUpVotedPostContent = (Label)ControlFinder.FindControlRecursive(rpbPost, "LblUpVotedPostContent");
                    Image imgSeePost = (Image)ControlFinder.FindControlRecursive(item, "ImgSeePost");
                    imgSeePost.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "tooltip", "2")).Text;
                    Label lblLastUpdate = (Label)ControlFinder.FindControlRecursive(item, "LblLastUpdate");
                    Label lblVotes = (Label)ControlFinder.FindControlRecursive(item, "LblVotes");
                    Label lblComments = (Label)ControlFinder.FindControlRecursive(item, "LblComments");

                    lnkBtnTopic.Text = item["topic"].Text;
                    lblUpVotedPostContent.Text = item["post"].Text;
                    lblLastUpdate.Text = "written at " + GlobalMethods.FixDate(item["last_update"].Text);

                    ImageButton imgBtnUpVote = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnUpVote");

                    if (vSession.User != null)
                    {
                        imgBtnUpVote.Enabled = (!Sql.UserHasUpVotePost(vSession.User.Id, Convert.ToInt32(item["id"].Text), session)) ? true : false;
                        imgBtnUpVote.ToolTip = (imgBtnUpVote.Enabled) ? "Up Vote" : "You have already Up Vote this post";
                        imgBtnUpVote.ImageUrl = (imgBtnUpVote.Enabled) ? "~/images/icons/small/up_2.png" : "~/images/icons/small/up_no_2.png";
                    }
                    else
                    {
                        imgBtnUpVote.ToolTip = "You must log in to Up Vote";
                        imgBtnUpVote.ImageUrl = "~/images/icons/small/up_no_2.png";
                    }

                    lblVotes.Text = item["total_votes"].Text;
                    lblComments.Text = item["total_comments"].Text;

                    LinkButton lnkBtnComments = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnComments");
                    lnkBtnComments.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "community", "label", "7")).Text;
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

        protected void RdgUpVoted_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.ElioCompanyDetailsView != null)
                {
                    List<ElioCommunityPosts> userPosts = new List<ElioCommunityPosts>();
                    Sql.GetUserCommunityPostsDetails(vSession.ElioCompanyDetailsView.Id, session);

                    if (userPosts.Count > 0)
                    {
                        RdgUpVoted.Visible = true;
                        UcMessageAlert.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("creator_user_id");
                        table.Columns.Add("topic");
                        table.Columns.Add("topic_url");
                        table.Columns.Add("post");
                        table.Columns.Add("last_update");
                        table.Columns.Add("total_votes");
                        table.Columns.Add("total_comments");

                        foreach (ElioCommunityPosts post in userPosts)
                        {
                            table.Rows.Add(post.Id, post.CreatorUserId, post.Topic, post.TopicUrl, GlobalMethods.FixParagraphsView(post.Post), post.LastUpdate, post.TotalVotes, post.TotalComments);
                        }

                        RdgUpVoted.DataSource = table;
                    }
                    else
                    {
                        RdgUpVoted.Visible = false;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "message", "3")).Text, MessageTypes.Warning, true, true, false);
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