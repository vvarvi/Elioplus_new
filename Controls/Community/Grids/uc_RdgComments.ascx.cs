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

namespace WdS.ElioPlus.Controls.Community.Grids
{
    public partial class uc_RdgComments : System.Web.UI.UserControl
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
                    GlobalMethods.ShowMessageControl(UcMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communitycomments", "message", "1")).Text, MessageTypes.Warning, true, true, false);
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
            vSession.ElioCompanyDetailsView = Sql.GetUserById(Convert.ToInt32(item["user_id"].Text), session);
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

        protected void LnkBtnUser_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LinkButton btn = (LinkButton)sender;
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

        protected void LknBtnUsername_OnClick(object sender, EventArgs args)
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

        protected void ImgBtnSetNotPublic_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ImageButton imgBtnSetNotPublic = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtnSetNotPublic.NamingContainer;

                    ElioCommunityPostsComments comment = Sql.GetPostCommentById(Convert.ToInt32(item["id"].Text), session);
                    if (comment != null)
                    {
                        comment.IsPublic = 0;

                        DataLoader<ElioCommunityPostsComments> loader = new DataLoader<ElioCommunityPostsComments>(session);
                        loader.Update(comment);

                        ElioCommunityPosts post = Sql.GetCommunityPostsById(vSession.LoadedCommunityPostDetails.Id, session);
                        if (post != null)
                        {
                            post.TotalComments--;

                            DataLoader<ElioCommunityPosts> loader2 = new DataLoader<ElioCommunityPosts>(session);
                            loader2.Update(post);

                            vSession.LoadedCommunityPostDetails = Sql.GetCommunityPostsDetailsById(vSession.LoadedCommunityPostDetails.Id, session);

                            //LblPostComments.Text = vSession.LoadedCommunityPostDetails.TotalComments.ToString();
                        }

                        RdgPostComments.Rebind();
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

        protected void ImgBtnReply_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ImageButton imgBtnReply = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtnReply.NamingContainer;

                    Panel pnlReply = (Panel)ControlFinder.FindControlRecursive(item, "PnlReply");
                    pnlReply.Visible = true;

                    Panel pnlAddNewComment = (Panel)ControlFinder.FindControlBackWards(this, "PnlAddNewComment");
                    pnlAddNewComment.Visible = false;
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

        protected void RbtnAddComment_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                RadButton btn = (RadButton)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                Label lblNewCommentError = (Label)ControlFinder.FindControlRecursive(item, "LblNewCommentError");
                lblNewCommentError.Text = string.Empty;

                RadTextBox rtbxNewComment = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxNewComment");
                if (string.IsNullOrEmpty(rtbxNewComment.Text))
                {
                    lblNewCommentError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communitycomments", "label", "10")).Text;
                    return;
                }

                try
                {
                    session.BeginTransaction();

                    ElioCommunityPostsComments parentComment = Sql.GetCommentById(Convert.ToInt32(item["id"].Text), session);

                    if (parentComment != null)
                    {
                        ElioCommunityPostsComments newComment = new ElioCommunityPostsComments();

                        newComment.UserId = vSession.User.Id;
                        newComment.SysDate = DateTime.Now;
                        newComment.Comment = rtbxNewComment.Text;
                        newComment.CommunityPostId = vSession.LoadedCommunityPostDetails.Id;
                        newComment.ReplyToCommentId = Convert.ToInt32(item["id"].Text);
                        newComment.IsPublic = 1;
                        newComment.ReplyToCommentId = parentComment.Id;
                        newComment.Depth = parentComment.Depth + 1;

                        DataLoader<ElioCommunityPostsComments> loader = new DataLoader<ElioCommunityPostsComments>(session);

                        loader.Insert(newComment);

                        ElioCommunityPosts post = Sql.GetPostByPostId(vSession.LoadedCommunityPostDetails.Id, session);
                        if (post != null)
                        {
                            post.TotalComments++;

                            DataLoader<ElioCommunityPosts> loader2 = new DataLoader<ElioCommunityPosts>(session);
                            loader2.Update(post);
                        }

                        session.CommitTransaction();

                        RdgPostComments.Rebind();

                        Panel pnlAddNewComment = (Panel)ControlFinder.FindControlBackWards(this, "PnlAddNewComment");
                        pnlAddNewComment.Visible = true;

                        rtbxNewComment.Text = string.Empty;                       
                    }
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
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

        protected void RbtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                RadButton btn = (RadButton)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                Panel pnlReply = (Panel)ControlFinder.FindControlRecursive(item, "PnlReply");
                pnlReply.Visible = false;

                Label lblNewCommentError = (Label)ControlFinder.FindControlRecursive(item, "LblNewCommentError");
                lblNewCommentError.Text = string.Empty;

                Panel pnlAddNewComment = (Panel)ControlFinder.FindControlBackWards(this, "PnlAddNewComment");
                pnlAddNewComment.Visible = true;
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

                    RadButton rbtnAddComment = (RadButton)ControlFinder.FindControlRecursive(item, "RbtnAddComment");
                    Label lblAddCommentText = (Label)ControlFinder.FindControlRecursive(rbtnAddComment, "LblAddCommentText");
                    lblAddCommentText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communitycomments", "button", "1")).Text;

                    RadButton rbtnCancel = (RadButton)ControlFinder.FindControlRecursive(item, "RbtnCancel");
                    Label lblCancelText = (Label)ControlFinder.FindControlRecursive(rbtnCancel, "LblCancelText");
                    lblCancelText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "7")).Text;

                    if (vSession.User != null)
                    {
                        ImageButton imgBtnSetNotPublic = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSetNotPublic");
                        imgBtnSetNotPublic.Visible = (Sql.IsUserAdministrator(vSession.User.Id, session)) ? true : false;
                        imgBtnSetNotPublic.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communitycomments", "tooltip", "1")).Text;   
                    }

                    ImageButton imgBtnReply = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnReply");
                    imgBtnReply.ToolTip = (vSession.User != null) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communitycomments", "tooltip", "2")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communitycomments", "tooltip", "3")).Text;

                    ImageButton imgBtnLogo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnLogo");
                    imgBtnLogo.ImageUrl = (item["personal_image"].Text != "&nbsp;") ? item["personal_image"].Text : "~/images/icons/personal_img_3.png";

                    Label lblComment = (Label)ControlFinder.FindControlRecursive(item, "LblComment");
                    Label lblSysdate = (Label)ControlFinder.FindControlRecursive(item, "LblSysdate");
                    LinkButton lknBtnUsername = (LinkButton)ControlFinder.FindControlRecursive(item, "LknBtnUsername");

                    lblComment.Text = item["comment"].Text;

                    lknBtnUsername.Text = item["username"].Text;
                    lblSysdate.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communitycomments", "label", "2")).Text + GlobalMethods.FixDate(item["sysdate"].Text);

                    Panel pnlComment = (Panel)ControlFinder.FindControlRecursive(item, "PnlComment");
                    pnlComment.CssClass += "-" + item["depth"].Text;
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

                if (vSession.LoadedCommunityPostDetails != null)
                {
                    List<ElioCommunityPostsCommentsIJUsers> comments = Sql.GetPostAllCommentsIJUsers(vSession.LoadedCommunityPostDetails.Id, session);

                    if (comments.Count > 0)
                    {
                        int depth = Sql.GetMaxDepth(vSession.LoadedCommunityPostDetails.Id, session);
                        int x = -1;
                        int y = -1;
                        for (int i = 1; i < depth + 1; i++)
                        {
                            foreach (ElioCommunityPostsCommentsIJUsers reply in comments.ToList())
                            {
                                if (reply.ReplyToCommentId != 0 && reply.Depth == i)
                                {
                                    x = comments.IndexOf(reply);
                                    y = reply.ReplyToCommentId;
                                    int z = -1;

                                    foreach (ElioCommunityPostsCommentsIJUsers comment in comments)
                                    {
                                        if (comment.Id == y)
                                        {
                                            z = comments.IndexOf(comment);
                                            break;
                                        }
                                    }

                                    comments.Insert(z + 1, reply);
                                    comments.RemoveAt(x);
                                }
                            }
                        }
                    }

                    if (comments.Count > 0)
                    {
                        RdgPostComments.Visible = true;
                        UcMessageAlert.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("user_id");
                        table.Columns.Add("comment");
                        table.Columns.Add("sysdate");
                        table.Columns.Add("username");
                        table.Columns.Add("last_name");
                        table.Columns.Add("first_name");
                        table.Columns.Add("personal_image");
                        table.Columns.Add("reply_to_comment_id");
                        table.Columns.Add("depth");

                        foreach (ElioCommunityPostsCommentsIJUsers comment in comments)
                        {
                            table.Rows.Add(comment.Id, comment.UserId, GlobalMethods.FixParagraphsView(comment.Comment), comment.SysDate, comment.Username, comment.LastName, comment.FirstName, comment.PersonalImage, comment.ReplyToCommentId, comment.Depth);
                        }

                        RdgPostComments.DataSource = table;
                    }
                    else
                    {
                        RdgPostComments.Visible = false;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communitycomments", "message", "1")).Text, MessageTypes.Warning, true, true, false);
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