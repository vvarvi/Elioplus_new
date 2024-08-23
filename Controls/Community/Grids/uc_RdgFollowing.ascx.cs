using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Objects;
using System.Data;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus.Controls.Community.Grids
{
    public partial class uc_RdgFollowing : System.Web.UI.UserControl
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

        #region Grids

        protected void RdgFollowing_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            vSession.CurrentGridPageIndex = e.NewPageIndex;
        }

        protected void RdgFollowing_ItemCreated(object sender, GridItemEventArgs e)
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

        protected void RdgFollowing_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    Image imgFPersonal = (Image)ControlFinder.FindControlRecursive(item, "ImgFPersonal");

                    Label lblFUser = (Label)ControlFinder.FindControlRecursive(item, "LblFUser");

                    Label lblFUserJobText = (Label)ControlFinder.FindControlRecursive(item, "LblFUserJobText");
                    Label lblFSummaryText = (Label)ControlFinder.FindControlRecursive(item, "LblFSummaryText");
                    lblFUserJobText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "label", "3")).Text;
                    lblFSummaryText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "label", "4")).Text; ;

                    Label lblFUserJob = (Label)ControlFinder.FindControlRecursive(item, "LblFUserJob");
                    Label lblFSummary = (Label)ControlFinder.FindControlRecursive(item, "LblFSummary");

                    ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                    if (user != null)
                    {
                        if (Convert.ToInt32(item["community_status"].Text) == Convert.ToInt32(AccountStatus.Completed))
                        {
                            imgFPersonal.ImageUrl = GlobalMethods.LoadUserPersonalImageDefault(user, true, true);   //item["personal_image"].Text;
                            lblFUser.Text = item["last_name"].Text + " " + item["first_name"].Text;
                            lblFUserJob.Text = GlobalMethods.FixParagraphsView(item["position"].Text);
                            lblFSummary.Text = GlobalMethods.FixParagraphsView(item["community_summary_text"].Text);
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

        protected void RdgFollowing_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.ElioCompanyDetailsView != null)
                {
                    List<ElioUsers> fUsers = Sql.GetUserFollowingUsers(vSession.ElioCompanyDetailsView.Id, session);

                    if (fUsers.Count > 0)
                    {
                        RdgFollowing.Visible = true;
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

                        foreach (ElioUsers fUser in fUsers)
                        {
                            table.Rows.Add(fUser.Id, fUser.CommunityStatus, fUser.LastName, fUser.FirstName, fUser.PersonalImage, fUser.Username, GlobalMethods.FixParagraphsView(fUser.Position), GlobalMethods.FixParagraphsView(fUser.CommunitySummaryText));
                        }

                        RdgFollowing.DataSource = table;
                    }
                    else
                    {
                        RdgFollowing.Visible = false;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "communityuserpersonalinfo", "message", "5")).Text, MessageTypes.Warning, true, true, false);
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