using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Objects;
using System.Data;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.LoadControls;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus.Controls.Community.Grids
{
    public partial class uc_RdgFollowed : System.Web.UI.UserControl
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

        private void RedirectToUserDetails(GridDataItem item)
        {
            vSession.ElioCompanyDetailsView = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
            if (vSession.ElioCompanyDetailsView != null)
            {
                Response.Redirect(ControlLoader.CommunityUserProfile(vSession.ElioCompanyDetailsView), false);
            }
            else
            {
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "11")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "4")).Text;
                //GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
            }
        }

        #endregion

        #region Buttons

        protected void ImgFPersonal_OnClick(object sender, EventArgs args)
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

        protected void LblFUser_OnClick(object sender, EventArgs args)
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

        protected void ImgBtnUserDetails_OnClick(object sender, EventArgs args)
        {
            try
            {
                ImageButton btn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                RadPanelBar rpbUserDetails = (RadPanelBar)ControlFinder.FindControlRecursive(item, "RpbUserDetails");

                if (!rpbUserDetails.Items[0].Expanded)
                {
                    rpbUserDetails.CssClass = "unhidden";
                    rpbUserDetails.Items[0].Expanded = true;
                }
                else
                {
                    rpbUserDetails.CssClass = "hidden";
                    rpbUserDetails.Items[0].Expanded = false;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Grids

        protected void RdgFollowed_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            vSession.CurrentGridPageIndex = e.NewPageIndex;
        }

        protected void RdgFollowed_ItemCreated(object sender, GridItemEventArgs e)
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

        protected void RdgFollowed_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    HtmlAnchor aFPersonal = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aFPersonal");
                    HtmlAnchor aFUser = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aFUser");

                    Image imgFPersonal = (Image)ControlFinder.FindControlRecursive(item, "ImgFPersonal");
                    ImageButton imgBtnUserDetails = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnUserDetails");
                    Label lblFUser = (Label)ControlFinder.FindControlRecursive(item, "LblFUser");
                    Label lblSocialText = (Label)ControlFinder.FindControlRecursive(item, "LblSocialText");                    
                    Label lblFUserJobText = (Label)ControlFinder.FindControlRecursive(item, "LblFUserJobText");
                    Label lblFSummaryText = (Label)ControlFinder.FindControlRecursive(item, "LblFSummaryText");
                    lblFUserJobText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "label", "3")).Text;
                    lblFSummaryText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "label", "4")).Text; ;

                    HyperLink linkedinLink = (HyperLink)ControlFinder.FindControlRecursive(item, "LinkedinLink");
                    HyperLink twitterLink = (HyperLink)ControlFinder.FindControlRecursive(item, "TwitterLink");

                    Label lblFUserJob = (Label)ControlFinder.FindControlRecursive(item, "LblFUserJob");
                    Label lblFSummary = (Label)ControlFinder.FindControlRecursive(item, "LblFSummary");

                    aFPersonal.HRef = ControlLoader.CommunityUserProfile(vSession.ElioCompanyDetailsView);
                    aFUser.HRef = ControlLoader.CommunityUserProfile(vSession.ElioCompanyDetailsView);

                    ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                    if (user != null)
                    {
                        if (Convert.ToInt32(item["community_status"].Text) == Convert.ToInt32(AccountStatus.Completed))
                        {
                            imgFPersonal.ImageUrl = GlobalMethods.LoadUserPersonalImageDefault(user, true, true);        //(item["personal_image"].Text != "&nbsp;") ? item["personal_image"].Text : "~/images/icons/personal_img_3.png";
                            lblFUser.Text = item["last_name"].Text + " " + item["first_name"].Text;
                            lblFUserJob.Text = GlobalMethods.FixParagraphsView(item["position"].Text);
                            lblFSummary.Text = GlobalMethods.FixParagraphsView(item["community_summary_text"].Text);
                            lblSocialText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "label", "5")).Text;
                            linkedinLink.Visible = (string.IsNullOrEmpty(item["linkedin"].Text) || item["linkedin"].Text == "&nbsp;") ? false : true;
                            linkedinLink.NavigateUrl = item["linkedin"].Text;
                            twitterLink.Visible = (string.IsNullOrEmpty(item["twitter"].Text) || item["twitter"].Text == "&nbsp;") ? false : true;
                            twitterLink.NavigateUrl = item["twitter"].Text;
                            lblSocialText.Visible = (!linkedinLink.Visible && !twitterLink.Visible) ? false : true;
                        }
                        else
                        {
                            imgFPersonal.ImageUrl = GlobalMethods.LoadUserPersonalImageDefault(user, true, true);   //"~/images/icons/personal_img_3.png";
                            lblFUser.Text = item["username"].Text;
                            lblFUserJobText.Visible = false;
                            lblFSummaryText.Visible = false;
                            lblSocialText.Visible = false;
                            imgBtnUserDetails.Visible = false;
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

        protected void RdgFollowed_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                string alert=Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "message", "4")).Text;

                RadTabStrip rtbInfo = (RadTabStrip)ControlFinder.FindControlBackWards(this, "RtbInfo");

                List<ElioUsers> fUsers = new List<ElioUsers>();

                if (vSession.ElioCompanyDetailsView != null)
                {
                    if (rtbInfo.Tabs[3].Selected)
                    {
                        fUsers = Sql.GetUserFollowedUsers(vSession.ElioCompanyDetailsView.Id, session);
                    }
                    else if (rtbInfo.Tabs[4].Selected)
                    {
                        fUsers = Sql.GetUserFollowingUsers(vSession.ElioCompanyDetailsView.Id, session);
                        alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "message", "5")).Text;
                    }

                    if (fUsers.Count > 0)
                    {
                        RdgFollowed.Visible = true;
                        UcMessageAlert.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("community_status");
                        table.Columns.Add("last_name");
                        table.Columns.Add("first_name");
                        table.Columns.Add("personal_image");
                        table.Columns.Add("username");
                        table.Columns.Add("position");
                        table.Columns.Add("community_summary_text");
                        table.Columns.Add("linkedin");
                        table.Columns.Add("twitter");

                        foreach (ElioUsers fUser in fUsers)
                        {
                            table.Rows.Add(fUser.Id, fUser.CommunityStatus, fUser.LastName, fUser.FirstName, fUser.PersonalImage, fUser.Username, GlobalMethods.FixParagraphsView(fUser.Position), GlobalMethods.FixParagraphsView(fUser.CommunitySummaryText), fUser.LinkedInUrl, fUser.TwitterUrl);
                        }

                        RdgFollowed.DataSource = table;
                    }
                    else
                    {
                        RdgFollowed.Visible = false;
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