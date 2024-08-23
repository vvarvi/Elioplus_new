using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data;

namespace WdS.ElioPlus
{
    public partial class DashboardOpportunitiesView : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    //if (!IsPostBack)
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

        # region Methods

        private void FixPage()
        {
            if (!IsPostBack)
            {
                //divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                UpdateStrings();
                SetLinks();
                FillDdlClosedSubStatusSort();
            }

            LoadOpportunitiesStatus();
            LoadOpportunities("0", string.Empty);
        }

        private void LoadOpportunitiesStatus()
        {
            List<ElioOpportunitiesStatus> oppStatus = Sql.GetOpportunityPublicStatus(session);
            ElioOpportunitiesUsersStatusCustom customDescriptionStatus = null;

            if (oppStatus.Count > 0 && oppStatus.Count == 4)
            {
                customDescriptionStatus = Sql.GetUserOpportunityCurtomByOpportunityStatusId(vSession.User.Id, oppStatus[0].Id, session);
                LblStatusOne.Text = (customDescriptionStatus != null) ? customDescriptionStatus.OpportunityCustomDescription : oppStatus[0].StepDescription;

                customDescriptionStatus = Sql.GetUserOpportunityCurtomByOpportunityStatusId(vSession.User.Id, oppStatus[1].Id, session);
                LblStatusTwo.Text = (customDescriptionStatus != null) ? customDescriptionStatus.OpportunityCustomDescription : oppStatus[1].StepDescription;

                customDescriptionStatus = Sql.GetUserOpportunityCurtomByOpportunityStatusId(vSession.User.Id, oppStatus[2].Id, session);
                LblStatusThree.Text = (customDescriptionStatus != null) ? customDescriptionStatus.OpportunityCustomDescription : oppStatus[2].StepDescription;

                customDescriptionStatus = Sql.GetUserOpportunityCurtomByOpportunityStatusId(vSession.User.Id, oppStatus[3].Id, session);
                LblStatusFour.Text = (customDescriptionStatus != null) ? customDescriptionStatus.OpportunityCustomDescription : oppStatus[3].StepDescription;

                LblClosedSubStatusText.Text = LblStatusFour.Text + " step with status ";
            }
        }

        private void SetLinks()
        {
            aAddOpportunity.HRef = ControlLoader.Dashboard(vSession.User, "opportunities-add-edit");
        }

        private void UpdateStrings()
        {
            LblDashboard.Text = "Dashboard";
            LblDashPage.Text = "Opportunities";

            LblConfMsg.Text = string.Empty;
            TbxOpportConfId.Text = string.Empty;
            BtnSave.Text = string.Empty;
            TbxOpportAction.Text = string.Empty;
        }

        private List<ElioOpportunitiesSubCategoriesStatus> LoadOpportunitySubCategoriesStatus(int opportunityStep, DBSession session)
        {
            return Sql.GetOpportunitySubCategoriesPublicStatus(opportunityStep, session);
        }

        private void LoadOpportunities(string type, string companyName)
        {
            List<ElioOpportunitiesUsersIJUsersIJStatus> opportunities = new List<ElioOpportunitiesUsersIJUsersIJStatus>();

            if (type == "0")
            {
                opportunities = Sql.GetUsersOpportunitiesByName(vSession.User.Id, companyName, session);
            }
            else if (type == "1")
            {
                opportunities = Sql.GetUsersOpportunitiesWithOpenTasksByName(vSession.User.Id, companyName, session);
            }

            # region Initialize items

            RlbContacts.Items.Clear();
            RlbMeeting.Items.Clear();
            RlbProposal.Items.Clear();
            RlbClosed.Items.Clear();

            int contactNo = 0;
            int meetingNo = 0;
            int proposalNo = 0;
            int closedNo = 0;

            //int dtContRowIndex = 0;
            //int dtMeetRowIndex = 0;
            //int dtPropRowIndex = 0;
            //int dtClosRowIndex = 0;

            DataTable dtCont = new DataTable();

            dtCont.Columns.Add("opportId");
            dtCont.Columns.Add("opportOrganName");
            dtCont.Columns.Add("opportNotes", typeof(int));
            dtCont.Columns.Add("opportTasks", typeof(int));
            dtCont.Columns.Add("opportDate", typeof(DateTime));

            DataTable dtMeet = new DataTable();

            dtMeet.Columns.Add("opportId");
            dtMeet.Columns.Add("opportOrganName");
            dtMeet.Columns.Add("opportNotes", typeof(int));
            dtMeet.Columns.Add("opportTasks", typeof(int));
            dtMeet.Columns.Add("opportDate", typeof(DateTime));

            DataTable dtProp = new DataTable();

            dtProp.Columns.Add("opportId");
            dtProp.Columns.Add("opportOrganName");
            dtProp.Columns.Add("opportNotes", typeof(int));
            dtProp.Columns.Add("opportTasks", typeof(int));
            dtProp.Columns.Add("opportDate", typeof(DateTime));

            DataTable dtClos = new DataTable();

            dtClos.Columns.Add("opportId");
            dtClos.Columns.Add("opportOrganName");
            dtClos.Columns.Add("opportNotes", typeof(int));
            dtClos.Columns.Add("opportTasks", typeof(int));
            dtClos.Columns.Add("opportDate", typeof(DateTime));

            # endregion

            # region Fill and Sort

            foreach (ElioOpportunitiesUsersIJUsersIJStatus opportunity in opportunities)
            {
                int notesCount = Sql.GetUserCountOfNotes(opportunity.Id, session);
                int tasksCount = Sql.GetUserOpportunityCountOfTasks(vSession.User.Id, opportunity.Id, session);

                if (opportunity.StepDescription == OpportunityStep.Contact.ToString())
                {
                    dtCont.Rows.Add(opportunity.Id, opportunity.OrganizationName, notesCount, tasksCount, opportunity.SysDate);

                    //AddListContent(RlbContacts, dtCont.Rows[dtContRowIndex]);
                    //contactNo++;
                    //dtContRowIndex++;
                }
                else if (opportunity.StepDescription == OpportunityStep.Meeting.ToString())
                {
                    dtMeet.Rows.Add(opportunity.Id, opportunity.OrganizationName, notesCount, tasksCount, opportunity.SysDate);

                    //AddListContent(RlbMeeting, dtMeet.Rows[dtContRowIndex]);
                    //meetingNo++;
                    //dtMeetRowIndex++;
                }
                else if (opportunity.StepDescription == OpportunityStep.Proposal.ToString())
                {
                    dtProp.Rows.Add(opportunity.Id, opportunity.OrganizationName, notesCount, tasksCount, opportunity.SysDate);

                    //AddListContent(RlbProposal, dtProp.Rows[dtContRowIndex]);
                    //proposalNo++;
                    //dtPropRowIndex++;
                }
                else if (opportunity.StepDescription == OpportunityStep.Closed.ToString())
                {
                    if (DdlClosedSubStatusSort.SelectedItem.Value != "0")
                    {
                        bool exists = Sql.HasUserOpportunitySpecificSubCategoryStatus(vSession.User.Id, opportunity.Id, null, Convert.ToInt32(DdlClosedSubStatusSort.SelectedItem.Value), session);

                        if (exists)
                            dtClos.Rows.Add(opportunity.Id, opportunity.OrganizationName, notesCount, tasksCount, opportunity.SysDate);
                    }
                    else
                    {

                        dtClos.Rows.Add(opportunity.Id, opportunity.OrganizationName, notesCount, tasksCount, opportunity.SysDate);
                    }
                    //AddListContent(RlbClosed, dtClos.Rows[dtContRowIndex]);
                    //closedNo++;
                    //dtClosRowIndex++;
                }
            }

            DataTable sortDtCont = new DataTable();
            DataTable sortDtMeet = new DataTable();
            DataTable sortDtProp = new DataTable();
            DataTable sortDtClos = new DataTable();


            if (dtCont.Rows.Count > 0)
            {
                #region Status 1

                DataView dvCont = dtCont.DefaultView;

                if (DdlContactSort.SelectedItem.Value == "0")
                {
                    dvCont.Sort = "opportDate desc";
                }
                else if (DdlContactSort.SelectedItem.Value == "1")
                {
                    dvCont.Sort = "opportOrganName asc";
                }
                else if (DdlContactSort.SelectedItem.Value == "2")
                {
                    dvCont.Sort = "opportNotes desc";
                }
                else if (DdlContactSort.SelectedItem.Value == "3")
                {
                    dvCont.Sort = "opportTasks desc";
                }

                sortDtCont = dvCont.ToTable();

                #endregion
            }

            if (dtMeet.Rows.Count > 0)
            {
                #region Status 2

                DataView dvMeet = dtMeet.DefaultView;

                if (DdlMeetingSort.SelectedItem.Value == "0")
                {
                    dvMeet.Sort = "opportDate desc";
                }
                else if (DdlMeetingSort.SelectedItem.Value == "1")
                {
                    dvMeet.Sort = "opportOrganName asc";
                }
                else if (DdlMeetingSort.SelectedItem.Value == "2")
                {
                    dvMeet.Sort = "opportNotes desc";
                }
                else if (DdlMeetingSort.SelectedItem.Value == "3")
                {
                    dvMeet.Sort = "opportTasks desc";
                }

                sortDtMeet = dvMeet.ToTable();

                #endregion
            }

            if (dtProp.Rows.Count > 0)
            {
                #region Status 3

                DataView dvProp = dtProp.DefaultView;

                if (DdlProposalSort.SelectedItem.Value == "0")
                {
                    dvProp.Sort = "opportDate desc";
                }
                else if (DdlProposalSort.SelectedItem.Value == "1")
                {
                    dvProp.Sort = "opportOrganName asc";
                }
                else if (DdlProposalSort.SelectedItem.Value == "2")
                {
                    dvProp.Sort = "opportNotes desc";
                }
                else if (DdlProposalSort.SelectedItem.Value == "3")
                {
                    dvProp.Sort = "opportTasks desc";
                }

                sortDtProp = dvProp.ToTable();

                #endregion
            }

            if (dtClos.Rows.Count > 0)
            {
                #region Status 4

                DataView dvClos = dtClos.DefaultView;

                if (DdlClosedSort.SelectedItem.Value == "0")
                {
                    dvClos.Sort = "opportDate desc";
                }
                else if (DdlClosedSort.SelectedItem.Value == "1")
                {
                    dvClos.Sort = "opportOrganName asc";
                }
                else if (DdlClosedSort.SelectedItem.Value == "2")
                {
                    dvClos.Sort = "opportNotes desc";
                }
                else if (DdlClosedSort.SelectedItem.Value == "3")
                {
                    dvClos.Sort = "opportTasks desc";
                }

                sortDtClos = dvClos.ToTable();

                #endregion
            }

            # endregion

            # region Create Content

            foreach (DataRow row in sortDtCont.Rows)
            {
                #region Status One

                RadListBoxItem item = new RadListBoxItem();

                item.CssClass = "itemClass";

                Image img1 = new Image();
                img1.ImageUrl = "/images/sidebar_inline_toggler_icon_grey.jpg";
                img1.CssClass = "imgClass";
                item.Controls.Add(img1);

                Label opportunityId = new Label() { Text = row["opportId"].ToString() };
                opportunityId.Visible = false;
                item.Controls.Add(opportunityId);

                Label companyname = new Label() { Text = row["opportOrganName"].ToString() };
                companyname.CssClass = "nameClass";
                item.Controls.Add(companyname);

                string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(row["opportId"].ToString()), session);
                Session[sessionId] = row["opportId"].ToString();
                //string guid = (!Sql.IsUserAdministrator(vSession.User.Id, session)) ? Guid.NewGuid().ToString() : row["opportId"].ToString();
                //Session[guid] = row["opportId"].ToString();
                
                int notesCount = Convert.ToInt32(row["opportNotes"]);
                HyperLink notes = new HyperLink() { Text = "<b>" + notesCount + "</b> Notes" };
                notes.CssClass = "noteClass";
                notes.NavigateUrl = ControlLoader.Dashboard(vSession.User, "opportunities-view-notes") + "?opportunityViewID=" + sessionId + "&btn=true";
                item.Controls.Add(notes);

                List<ElioOpportunitiesNotes> oppNotes = Sql.GetUsersOpportunityNotesByOpportunityId(Convert.ToInt32(row["opportId"].ToString()), session);

                if (oppNotes.Count > 0)
                {
                    notes.ToolTip = GlobalMethods.FillToolTipWithStrings(oppNotes);
                }

                int tasksCount = Convert.ToInt32(row["opportTasks"]);
                HyperLink tasks = new HyperLink() { Text = "<b>" + tasksCount + "</b> Tasks" };
                tasks.CssClass = "taskClass";
                tasks.NavigateUrl = ControlLoader.Dashboard(vSession.User, "opportunities-view-tasks") + "?opportunityViewID=" + sessionId;
                item.Controls.Add(tasks);

                HtmlAnchor aEditOpportunity = new HtmlAnchor();
                aEditOpportunity.HRef = ControlLoader.Dashboard(vSession.User, "opportunities-add-edit") + "?opportunityViewID=" + sessionId;

                Image imgEditOpportunity = new Image();
                imgEditOpportunity.ImageUrl = "/images/settings.png";
                imgEditOpportunity.CssClass = "settClass";
                aEditOpportunity.Controls.Add(imgEditOpportunity);

                Label lblEditOpportunity = new Label();
                lblEditOpportunity.Text = "View / Edit";
                lblEditOpportunity.Width = Unit.Pixel(76);
                lblEditOpportunity.CssClass = "settClass";
                lblEditOpportunity.Style.Add("margin-right", "10px");
                lblEditOpportunity.Style.Add("margin-top", "-7px");
                aEditOpportunity.Controls.Add(lblEditOpportunity);

                item.Controls.Add(aEditOpportunity);

                contactNo++;
                RlbContacts.Items.Add(item);

                HtmlAnchor ancRight = (HtmlAnchor)item.FindControl("aMoveRight");
                HtmlAnchor ancDelete = (HtmlAnchor)item.FindControl("aDelete");
                ancRight.Title = "Move to " + LblStatusTwo.Text + "";
                ancDelete.Title = "Delete";
                ancRight.Attributes["aria-label"] = ancDelete.Attributes["aria-label"] = row["opportId"].ToString();

                RlbContacts.DataBind();
                item.Style.Add("border-color", "whitesmoke");
                item.Style.Add("background-color", "whitesmoke");

                #endregion
            }

            foreach (DataRow row in sortDtMeet.Rows)
            {
                #region Status Two

                RadListBoxItem item = new RadListBoxItem();

                item.CssClass = "itemClass";

                Image img1 = new Image();
                img1.ImageUrl = "/images/sidebar_inline_toggler_icon_grey.jpg";
                img1.CssClass = "imgClass";
                item.Controls.Add(img1);

                Label opportunityId = new Label() { Text = row["opportId"].ToString() };
                opportunityId.Visible = false;
                item.Controls.Add(opportunityId);

                Label companyname = new Label() { Text = row["opportOrganName"].ToString() };
                companyname.CssClass = "nameClass";
                item.Controls.Add(companyname);

                string guid = Guid.NewGuid().ToString();
                Session[guid] = row["opportId"].ToString();

                int notesCount = Convert.ToInt32(row["opportNotes"]);
                HyperLink notes = new HyperLink() { Text = "<b>" + notesCount + "</b> Notes" };
                notes.CssClass = "noteClass";
                notes.NavigateUrl = ControlLoader.Dashboard(vSession.User, "opportunities-view-notes") + "?opportunityViewID=" + guid + "&btn=true";
                item.Controls.Add(notes);

                List<ElioOpportunitiesNotes> oppNotes = Sql.GetUsersOpportunityNotesByOpportunityId(Convert.ToInt32(row["opportId"].ToString()), session);

                if (oppNotes.Count > 0)
                {
                    notes.ToolTip = GlobalMethods.FillToolTipWithStrings(oppNotes);
                }

                int tasksCount = Convert.ToInt32(row["opportTasks"]);
                HyperLink tasks = new HyperLink() { Text = "<b>" + tasksCount + "</b> Tasks" };
                tasks.CssClass = "taskClass";
                tasks.NavigateUrl = ControlLoader.Dashboard(vSession.User, "opportunities-view-tasks") + "?opportunityViewID=" + guid;
                item.Controls.Add(tasks);

                HtmlAnchor aEditOpportunity = new HtmlAnchor();
                aEditOpportunity.HRef = ControlLoader.Dashboard(vSession.User, "opportunities-add-edit") + "?opportunityViewID=" + guid;

                Image imgEditOpportunity = new Image();
                imgEditOpportunity.ImageUrl = "/images/settings.png";
                imgEditOpportunity.CssClass = "settClass";
                aEditOpportunity.Controls.Add(imgEditOpportunity);

                Label lblEditOpportunity = new Label();
                lblEditOpportunity.Text = "View / Edit";
                lblEditOpportunity.Width = Unit.Pixel(76);
                lblEditOpportunity.CssClass = "settClass";
                lblEditOpportunity.Style.Add("margin-right", "10px");
                lblEditOpportunity.Style.Add("margin-top", "-7px");
                aEditOpportunity.Controls.Add(lblEditOpportunity);

                item.Controls.Add(aEditOpportunity);

                meetingNo++;
                RlbMeeting.Items.Add(item);

                HtmlAnchor ancRight = (HtmlAnchor)item.FindControl("aMoveRight");
                HtmlAnchor ancLeft = (HtmlAnchor)item.FindControl("aMoveLeft");
                HtmlAnchor ancDelete = (HtmlAnchor)item.FindControl("aDelete");
                ancRight.Title = "Move to " + LblStatusThree.Text + "";
                ancLeft.Title = "Move to " + LblStatusOne.Text + "";
                ancDelete.Title = "Delete";
                ancRight.Attributes["aria-label"] = ancLeft.Attributes["aria-label"] = ancDelete.Attributes["aria-label"] = row["opportId"].ToString();

                RlbMeeting.DataBind();
                item.Style.Add("border-color", "whitesmoke");
                item.Style.Add("background-color", "whitesmoke");

                #endregion
            }

            foreach (DataRow row in sortDtProp.Rows)
            {
                #region Status Three

                RadListBoxItem item = new RadListBoxItem();

                item.CssClass = "itemClass";

                Image img1 = new Image();
                img1.ImageUrl = "/images/sidebar_inline_toggler_icon_grey.jpg";
                img1.CssClass = "imgClass";
                item.Controls.Add(img1);

                Label opportunityId = new Label() { Text = row["opportId"].ToString() };
                opportunityId.Visible = false;
                item.Controls.Add(opportunityId);

                Label companyname = new Label() { Text = row["opportOrganName"].ToString() };
                companyname.CssClass = "nameClass";
                item.Controls.Add(companyname);

                string guid = Guid.NewGuid().ToString();
                Session[guid] = row["opportId"].ToString();

                int notesCount = Convert.ToInt32(row["opportNotes"]);
                HyperLink notes = new HyperLink() { Text = "<b>" + notesCount + "</b> Notes" };
                notes.CssClass = "noteClass";
                notes.NavigateUrl = ControlLoader.Dashboard(vSession.User, "opportunities-view-notes") + "?opportunityViewID=" + guid + "&btn=true";
                item.Controls.Add(notes);

                List<ElioOpportunitiesNotes> oppNotes = Sql.GetUsersOpportunityNotesByOpportunityId(Convert.ToInt32(row["opportId"].ToString()), session);

                if (oppNotes.Count > 0)
                {
                    notes.ToolTip = GlobalMethods.FillToolTipWithStrings(oppNotes);
                }

                int tasksCount = Convert.ToInt32(row["opportTasks"]);
                HyperLink tasks = new HyperLink() { Text = "<b>" + tasksCount + "</b> Tasks" };
                tasks.CssClass = "taskClass";
                tasks.NavigateUrl = ControlLoader.Dashboard(vSession.User, "opportunities-view-tasks") + "?opportunityViewID=" + guid;
                item.Controls.Add(tasks);

                HtmlAnchor aEditOpportunity = new HtmlAnchor();
                aEditOpportunity.HRef = ControlLoader.Dashboard(vSession.User, "opportunities-add-edit") + "?opportunityViewID=" + guid;

                Image imgEditOpportunity = new Image();
                imgEditOpportunity.ImageUrl = "/images/settings.png";
                imgEditOpportunity.CssClass = "settClass";
                aEditOpportunity.Controls.Add(imgEditOpportunity);

                Label lblEditOpportunity = new Label();
                lblEditOpportunity.Text = "View / Edit";
                lblEditOpportunity.Width = Unit.Pixel(76);
                lblEditOpportunity.CssClass = "settClass";
                lblEditOpportunity.Style.Add("margin-right", "10px");
                lblEditOpportunity.Style.Add("margin-top", "-7px");
                aEditOpportunity.Controls.Add(lblEditOpportunity);

                item.Controls.Add(aEditOpportunity);

                proposalNo++;
                RlbProposal.Items.Add(item);

                HtmlAnchor ancRight = (HtmlAnchor)item.FindControl("aMoveRight");
                HtmlAnchor ancLeft = (HtmlAnchor)item.FindControl("aMoveLeft");
                HtmlAnchor ancDelete = (HtmlAnchor)item.FindControl("aDelete");
                ancRight.Title = "Move to " + LblStatusFour.Text + "";
                ancLeft.Title = "Move to " + LblStatusTwo.Text + "";
                ancDelete.Title = "Delete";
                ancRight.Attributes["aria-label"] = ancLeft.Attributes["aria-label"] = ancDelete.Attributes["aria-label"] = row["opportId"].ToString();

                RlbProposal.DataBind();
                item.Style.Add("border-color", "whitesmoke");
                item.Style.Add("background-color", "whitesmoke");

                #endregion
            }

            int itemsCount = 0;
            foreach (DataRow row in sortDtClos.Rows)
            {
                itemsCount++;

                #region Status Four

                RadListBoxItem item = new RadListBoxItem();

                item.CssClass = "itemClass";
                item.Value = row["opportId"].ToString();
                item.ToolTip = Convert.ToInt32(OpportunityStep.Closed).ToString();

                Image img1 = new Image();
                img1.ImageUrl = "/images/sidebar_inline_toggler_icon_grey.jpg";
                img1.CssClass = "imgClass";
                item.Controls.Add(img1);

                Label opportunityId = new Label() { Text = row["opportId"].ToString() };
                opportunityId.Visible = false;
                item.Controls.Add(opportunityId);

                Label companyname = new Label() { Text = row["opportOrganName"].ToString() };
                companyname.CssClass = "nameClass";
                item.Controls.Add(companyname);

                #region List with sub categories status of this step

                List<ElioOpportunitiesSubCategoriesStatus> subStatus = LoadOpportunitySubCategoriesStatus(Convert.ToInt32(OpportunityStep.Closed), session);    //Sql.GetOpportunitySubCategoriesPublicStatus(Convert.ToInt32(OpportunityStep.Closed), session);
                if (subStatus.Count > 0)
                {
                    Label status = new Label() { Text = "Status " };
                    status.CssClass = "stClass";
                    item.Controls.Add(status);

                    RadComboBox combo = new RadComboBox();
                    combo.Items.Clear();

                    combo.ID = "combo_" + item.Value;   //Need an ID to find the control for save button

                    RadComboBoxItem cItem = new RadComboBoxItem();
                    cItem.Text = "Closed";
                    cItem.Value = "0";
                    combo.Items.Add(cItem);

                    foreach (ElioOpportunitiesSubCategoriesStatus subCategory in subStatus)
                    {
                        cItem = new RadComboBoxItem();
                        cItem.Text = subCategory.SubStepDescription;
                        cItem.Value = subCategory.Id.ToString();

                        combo.Items.Add(cItem);
                    }

                    ElioOpportunitiesUsersSubCategoriesStatus opportunitySubCategory = Sql.GetUserOpportunitySubCategoryStatus(vSession.User.Id, Convert.ToInt32(item.Value), null, session);
                    combo.FindItemByValue((opportunitySubCategory != null) ? opportunitySubCategory.SubCategoriesStatusId.ToString() : "0").Selected = true;

                    combo.Width = 115;
                    combo.Style.Add("float", "right");
                    combo.Style.Add("margin-top", "-38px");
                    combo.Style.Add("margin-right", "20px");

                    item.Controls.Add(combo);

                    ImageButton imgBtnSaveSub = new ImageButton();
                    imgBtnSaveSub.ImageUrl = "/images/save.png";
                    imgBtnSaveSub.CssClass = "saveClass";
                    imgBtnSaveSub.Click += new System.Web.UI.ImageClickEventHandler(ImgBtnSaveSub_Click);
                    item.Controls.Add(imgBtnSaveSub);
                }

                #endregion

                string guid = Guid.NewGuid().ToString();
                Session[guid] = row["opportId"].ToString();

                int notesCount = Convert.ToInt32(row["opportNotes"]);
                HyperLink notes = new HyperLink() { Text = "<b>" + notesCount + "</b> Notes" };
                notes.CssClass = "noteClass";
                notes.NavigateUrl = ControlLoader.Dashboard(vSession.User, "opportunities-view-notes") + "?opportunityViewID=" + guid + "&btn=true";
                item.Controls.Add(notes);

                List<ElioOpportunitiesNotes> oppNotes = Sql.GetUsersOpportunityNotesByOpportunityId(Convert.ToInt32(row["opportId"].ToString()), session);

                if (oppNotes.Count > 0)
                {
                    notes.ToolTip = GlobalMethods.FillToolTipWithStrings(oppNotes);
                }

                int tasksCount = Convert.ToInt32(row["opportTasks"]);
                HyperLink tasks = new HyperLink() { Text = "<b>" + tasksCount + "</b> Tasks" };
                tasks.CssClass = "taskClass";
                tasks.NavigateUrl = ControlLoader.Dashboard(vSession.User, "opportunities-view-tasks") + "?opportunityViewID=" + guid;
                item.Controls.Add(tasks);

                HtmlAnchor aEditOpportunity = new HtmlAnchor();
                aEditOpportunity.HRef = ControlLoader.Dashboard(vSession.User, "opportunities-add-edit") + "?opportunityViewID=" + guid;

                Image imgEditOpportunity = new Image();
                imgEditOpportunity.ImageUrl = "/images/settings.png";
                imgEditOpportunity.CssClass = "settClass";
                aEditOpportunity.Controls.Add(imgEditOpportunity);

                Label lblEditOpportunity = new Label();
                lblEditOpportunity.Text = "View / Edit";
                lblEditOpportunity.Width = Unit.Pixel(76);
                lblEditOpportunity.CssClass = "settClass";
                lblEditOpportunity.Style.Add("margin-right", "10px");
                lblEditOpportunity.Style.Add("margin-top", "-7px");
                aEditOpportunity.Controls.Add(lblEditOpportunity);

                item.Controls.Add(aEditOpportunity);

                closedNo++;

                RlbClosed.Items.Add(item);

                HtmlAnchor ancLeft = (HtmlAnchor)item.FindControl("aMoveLeft");
                HtmlAnchor ancDelete = (HtmlAnchor)item.FindControl("aDelete");
                ancLeft.Title = "Move to " + LblStatusThree.Text + "";
                ancDelete.Title = "Delete";
                ancLeft.Attributes["aria-label"] = ancDelete.Attributes["aria-label"] = row["opportId"].ToString();

                RlbClosed.DataBind();
                item.Style.Add("border-color", "whitesmoke");
                item.Style.Add("background-color", "whitesmoke");

                #endregion
            }

            # endregion

            LblAllContacts.Text = contactNo.ToString();
            LblAllMeeting.Text = meetingNo.ToString();
            LblAllProposal.Text = proposalNo.ToString();
            LblAllClosed.Text = closedNo.ToString();

            RlbContacts.Visible = (contactNo > 0) ? true : false;
            RlbMeeting.Visible = (meetingNo > 0) ? true : false;
            RlbProposal.Visible = (proposalNo > 0) ? true : false;
            RlbClosed.Visible = (closedNo > 0) ? true : false;
        }

        private void AddListContent(RadListBox rlb, DataRow row)
        {
            #region Add Content to ListBox

            RadListBoxItem item = new RadListBoxItem();

            item.CssClass = "itemClass";

            Image img1 = new Image();
            img1.ImageUrl = "/images/sidebar_inline_toggler_icon_grey.jpg";
            img1.CssClass = "imgClass";
            item.Controls.Add(img1);

            Label opportunityId = new Label() { Text = row["opportId"].ToString() };
            opportunityId.Visible = false;
            item.Controls.Add(opportunityId);

            Label companyname = new Label() { Text = row["opportOrganName"].ToString() };
            companyname.CssClass = "nameClass";
            item.Controls.Add(companyname);

            string guid = Guid.NewGuid().ToString();
            Session[guid] = row["opportId"].ToString();

            int dtContNotesCount = Convert.ToInt32(row["opportNotes"]);
            HyperLink notes = new HyperLink() { Text = "<b>" + dtContNotesCount + "</b> Notes" };
            notes.CssClass = "noteClass";
            notes.NavigateUrl = ControlLoader.Dashboard(vSession.User, "opportunities-view-notes") + "?opportunityViewID=" + guid + "&btn=true";
            item.Controls.Add(notes);

            List<ElioOpportunitiesNotes> oppNotes = Sql.GetUsersOpportunityNotesByOpportunityId(Convert.ToInt32(row["opportId"].ToString()), session);

            if (oppNotes.Count > 0)
            {
                notes.ToolTip = GlobalMethods.FillToolTipWithStrings(oppNotes);
            }

            int dtContTasksCount = Convert.ToInt32(row["opportTasks"]);
            HyperLink tasks = new HyperLink() { Text = "<b>" + dtContTasksCount + "</b> Tasks" };
            tasks.CssClass = "taskClass";
            tasks.NavigateUrl = ControlLoader.Dashboard(vSession.User, "opportunities-view-tasks") + "?opportunityViewID=" + guid;
            item.Controls.Add(tasks);

            HtmlAnchor aEditOpportunity = new HtmlAnchor();
            aEditOpportunity.HRef = ControlLoader.Dashboard(vSession.User, "opportunities-add-edit") + "?opportunityViewID=" + guid;

            Image imgEditOpportunity = new Image();
            imgEditOpportunity.ImageUrl = "/images/settings.png";
            imgEditOpportunity.CssClass = "settClass";
            aEditOpportunity.Controls.Add(imgEditOpportunity);

            Label lblEditOpportunity = new Label();
            lblEditOpportunity.Text = "View / Edit";
            lblEditOpportunity.Width = Unit.Pixel(76);
            lblEditOpportunity.CssClass = "settClass";
            lblEditOpportunity.Style.Add("margin-right", "10px");
            lblEditOpportunity.Style.Add("margin-top", "-7px");
            aEditOpportunity.Controls.Add(lblEditOpportunity);

            item.Controls.Add(aEditOpportunity);

            //contactNo++;
            rlb.Items.Add(item);

            HtmlAnchor ancRight = (HtmlAnchor)item.FindControl("aMoveRight");
            HtmlAnchor ancDelete = (HtmlAnchor)item.FindControl("aDelete");
            ancRight.Title = "Move to " + LblStatusTwo.Text + "";
            ancDelete.Title = "Delete";
            ancRight.Attributes["aria-label"] = ancDelete.Attributes["aria-label"] = row["opportId"].ToString();

            rlb.DataBind();
            item.Style.Add("border-color", "whitesmoke");
            item.Style.Add("background-color", "whitesmoke");

            #endregion
        }

        private void FixOpportunityStatusDescription(int originalStatusId, string newDescription)
        {
            ElioOpportunitiesUsersStatusCustom customStatus = new ElioOpportunitiesUsersStatusCustom();

            DataLoader<ElioOpportunitiesUsersStatusCustom> loader = new DataLoader<ElioOpportunitiesUsersStatusCustom>(session);

            //int statusId = Convert.ToInt32(OpportunityStep.Contact);
            customStatus = Sql.GetUserCustomOpportunityStatus(vSession.User.Id, originalStatusId, session);    //opportunitiesStatus[0].Id = Contact

            if (customStatus == null)
            {
                customStatus = new ElioOpportunitiesUsersStatusCustom();

                customStatus.UserId = vSession.User.Id;
                customStatus.OpportunityStatusId = originalStatusId;
                customStatus.OpportunityCustomDescription = newDescription;
                customStatus.Sysdate = DateTime.Now;
                customStatus.LastUpdated = DateTime.Now;
                customStatus.IsPublic = 1;

                loader.Insert(customStatus);
            }
            else
            {
                customStatus.UserId = vSession.User.Id;
                customStatus.OpportunityCustomDescription = newDescription;
                customStatus.LastUpdated = DateTime.Now;
                customStatus.IsPublic = 1;

                loader.Update(customStatus);
            }
        }

        private void FillDdlClosedSubStatusSort()
        {
            List<ElioOpportunitiesSubCategoriesStatus> subCategories = LoadOpportunitySubCategoriesStatus(Convert.ToInt32(OpportunityStep.Closed), session);

            if (subCategories.Count > 0)
            {
                DdlClosedSubStatusSort.Items.Clear();

                ListItem lstItem = new ListItem();
                lstItem.Text = "All/Closed";
                lstItem.Value = "0";
                DdlClosedSubStatusSort.Items.Add(lstItem);

                foreach (ElioOpportunitiesSubCategoriesStatus subStatus in subCategories)
                {
                    lstItem = new ListItem();
                    lstItem.Text = subStatus.SubStepDescription;
                    lstItem.Value = subStatus.Id.ToString();

                    DdlClosedSubStatusSort.Items.Add(lstItem);
                }
            }
        }

        # endregion

        # region Buttons

        protected void DdlClosedSubStatusSort_OnSelectedIndexChanged(object sender, EventArgs args)
        {

        }

        protected void ImgBtnSaveSub_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ImageButton imgBtn = (ImageButton)sender;
                    RadListBoxItem item = (RadListBoxItem)imgBtn.NamingContainer;
                    RadComboBox combo = (RadComboBox)ControlFinder.FindControlRecursive(item, "combo_" + item.Value);

                    if (combo != null)
                    {
                        int selectedSubCategoryId = Convert.ToInt32(combo.SelectedValue);
                        ElioOpportunitiesUsersSubCategoriesStatus subCategory = null;
                        DataLoader<ElioOpportunitiesUsersSubCategoriesStatus> loader = new DataLoader<ElioOpportunitiesUsersSubCategoriesStatus>(session);

                        int opportunityId = int.Parse(item.Value);
                        int opportunityStatusId = int.Parse(item.ToolTip);

                        if (selectedSubCategoryId > 0)
                        {
                            subCategory = Sql.GetUserOpportunitySubCategoryStatus(vSession.User.Id, opportunityId, opportunityStatusId, session);

                            if (subCategory == null)
                            {
                                #region Insert Sub Category Status

                                subCategory = new ElioOpportunitiesUsersSubCategoriesStatus();

                                subCategory.UserId = vSession.User.Id;
                                subCategory.OpportunityId = opportunityId;
                                subCategory.OpportunityStatusId = opportunityStatusId;
                                subCategory.SubCategoriesStatusId = selectedSubCategoryId;
                                subCategory.Sysdate = DateTime.Now;
                                subCategory.LastUpdated = DateTime.Now;

                                loader.Insert(subCategory);

                                #endregion
                            }
                            else
                            {
                                #region Update Sub Category Status

                                subCategory.SubCategoriesStatusId = selectedSubCategoryId;
                                subCategory.LastUpdated = DateTime.Now;

                                loader.Update(subCategory);

                                #endregion
                            }
                        }
                        else
                        {
                            #region Delete Sub Category Status

                            subCategory = Sql.GetUserOpportunitySubCategoryStatus(vSession.User.Id, opportunityId, opportunityStatusId, session);

                            if (subCategory != null)
                                loader.Delete(subCategory);

                            #endregion
                        }
                    }
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

        protected void DeleteStatusOne_OnCick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ElioOpportunitiesStatus status = Sql.GetOpportunityStatusByStatusId(Convert.ToInt32(OpportunityStep.Contact), session);
                    if (status != null)
                    {
                        Sql.DeleteUserOpportunityCustomByStatusId(Convert.ToInt32(OpportunityStep.Contact), vSession.User.Id, session);

                        LblStatusOne.Text = status.StepDescription;
                        LblStatusOne.Visible = true;
                        TbxStatusOne.Visible = false;
                        DeleteStatusOne.Visible = false;

                        iEditStatusOne.Attributes.Add("class", "fa fa-edit");
                    }
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

        protected void DeleteStatusTwo_OnCick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ElioOpportunitiesStatus status = Sql.GetOpportunityStatusByStatusId(Convert.ToInt32(OpportunityStep.Meeting), session);
                    if (status != null)
                    {
                        Sql.DeleteUserOpportunityCustomByStatusId(Convert.ToInt32(OpportunityStep.Meeting), vSession.User.Id, session);

                        LblStatusTwo.Text = status.StepDescription;
                        LblStatusTwo.Visible = true;
                        TbxStatusTwo.Visible = false;
                        DeleteStatusTwo.Visible = false;

                        iEditStatusTwo.Attributes.Add("class", "fa fa-edit");
                    }
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

        protected void DeleteStatusThree_OnCick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ElioOpportunitiesStatus status = Sql.GetOpportunityStatusByStatusId(Convert.ToInt32(OpportunityStep.Proposal), session);
                    if (status != null)
                    {
                        Sql.DeleteUserOpportunityCustomByStatusId(Convert.ToInt32(OpportunityStep.Proposal), vSession.User.Id, session);

                        LblStatusThree.Text = status.StepDescription;
                        LblStatusThree.Visible = true;
                        TbxStatusThree.Visible = false;
                        DeleteStatusThree.Visible = false;

                        iEditStatusThree.Attributes.Add("class", "fa fa-edit");
                    }
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

        protected void DeleteStatusFour_OnCick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ElioOpportunitiesStatus status = Sql.GetOpportunityStatusByStatusId(Convert.ToInt32(OpportunityStep.Closed), session);
                    if (status != null)
                    {
                        Sql.DeleteUserOpportunityCustomByStatusId(Convert.ToInt32(OpportunityStep.Closed), vSession.User.Id, session);

                        LblStatusFour.Text = status.StepDescription;
                        LblStatusFour.Visible = true;
                        TbxStatusFour.Visible = false;
                        DeleteStatusFour.Visible = false;

                        iEditStatusFour.Attributes.Add("class", "fa fa-edit");

                        LblClosedSubStatusText.Text = LblStatusFour.Text + " step with status ";
                    }
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

        protected void EditStatusOne_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (LblStatusOne.Visible)
                    {
                        LblStatusOne.Visible = false;
                        TbxStatusOne.Visible = true;
                        TbxStatusOne.Text = LblStatusOne.Text;

                        iEditStatusOne.Attributes.Add("class", "fa fa-save");
                        DeleteStatusOne.Visible = true;
                    }
                    else
                    {

                        if (TbxStatusOne.Text != string.Empty)
                        {
                            session.OpenConnection();

                            List<ElioOpportunitiesStatus> opportunitiesStatus = Sql.GetOpportunityPublicStatus(session);
                            if (opportunitiesStatus.Count > 0)
                            {
                                FixOpportunityStatusDescription(opportunitiesStatus[0].Id, TbxStatusOne.Text);
                                LblStatusOne.Text = TbxStatusOne.Text;
                            }

                            session.CloseConnection();
                        }

                        LblStatusOne.Visible = true;
                        TbxStatusOne.Visible = false;
                        DeleteStatusOne.Visible = false;

                        iEditStatusOne.Attributes.Add("class", "fa fa-edit");
                    }
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
        }

        protected void EditStatusTwo_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (LblStatusTwo.Visible)
                    {
                        LblStatusTwo.Visible = false;
                        TbxStatusTwo.Visible = true;
                        TbxStatusTwo.Text = LblStatusTwo.Text;

                        iEditStatusTwo.Attributes.Add("class", "fa fa-save");
                        DeleteStatusTwo.Visible = true;
                    }
                    else
                    {

                        if (TbxStatusTwo.Text != string.Empty)
                        {
                            session.OpenConnection();

                            List<ElioOpportunitiesStatus> opportunitiesStatus = Sql.GetOpportunityPublicStatus(session);
                            if (opportunitiesStatus.Count > 0)
                            {
                                FixOpportunityStatusDescription(opportunitiesStatus[1].Id, TbxStatusTwo.Text);
                                LblStatusTwo.Text = TbxStatusTwo.Text;
                            }

                            session.CloseConnection();
                        }

                        LblStatusTwo.Visible = true;
                        TbxStatusTwo.Visible = false;

                        iEditStatusTwo.Attributes.Add("class", "fa fa-edit");
                        DeleteStatusTwo.Visible = false;
                    }
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
        }

        protected void EditStatusThree_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (LblStatusThree.Visible)
                    {
                        LblStatusThree.Visible = false;
                        TbxStatusThree.Visible = true;
                        TbxStatusThree.Text = LblStatusThree.Text;

                        iEditStatusThree.Attributes.Add("class", "fa fa-save");
                        DeleteStatusThree.Visible = true;
                    }
                    else
                    {

                        if (TbxStatusThree.Text != string.Empty)
                        {
                            session.OpenConnection();

                            List<ElioOpportunitiesStatus> opportunitiesStatus = Sql.GetOpportunityPublicStatus(session);
                            if (opportunitiesStatus.Count > 0)
                            {
                                FixOpportunityStatusDescription(opportunitiesStatus[2].Id, TbxStatusThree.Text);
                                LblStatusThree.Text = TbxStatusThree.Text;
                            }

                            session.CloseConnection();
                        }

                        LblStatusThree.Visible = true;
                        TbxStatusThree.Visible = false;

                        iEditStatusThree.Attributes.Add("class", "fa fa-edit");
                        DeleteStatusThree.Visible = false;
                    }
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
        }

        protected void EditStatusFour_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (LblStatusFour.Visible)
                    {
                        LblStatusFour.Visible = false;
                        TbxStatusFour.Visible = true;
                        TbxStatusFour.Text = LblStatusFour.Text;

                        iEditStatusFour.Attributes.Add("class", "fa fa-save");
                        DeleteStatusFour.Visible = true;
                    }
                    else
                    {

                        if (TbxStatusFour.Text != string.Empty)
                        {
                            session.OpenConnection();

                            List<ElioOpportunitiesStatus> opportunitiesStatus = Sql.GetOpportunityPublicStatus(session);
                            if (opportunitiesStatus.Count > 0)
                            {
                                FixOpportunityStatusDescription(opportunitiesStatus[3].Id, TbxStatusFour.Text);
                                LblStatusFour.Text = TbxStatusFour.Text;
                            }

                            session.CloseConnection();
                        }

                        LblStatusFour.Visible = true;
                        TbxStatusFour.Visible = false;

                        iEditStatusFour.Attributes.Add("class", "fa fa-edit");
                        DeleteStatusFour.Visible = false;
                    }

                    LblClosedSubStatusText.Text = LblStatusFour.Text + " step with status ";
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
        }

        protected void BtntSort_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LoadOpportunities(DdlOpportKinds.SelectedItem.Value, TbxSearch.Text);
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

        protected void BtnConfirm_OnClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LblConfMsg.Text = string.Empty;
                    TbxOpportConfId.Text = string.Empty;
                    BtnSave.Text = string.Empty;
                    TbxOpportAction.Text = string.Empty;

                    HtmlAnchor popupConfirm = (HtmlAnchor)sender;
                    
                    if (!string.IsNullOrEmpty(popupConfirm.Attributes["aria-label"].ToString()))
                    {
                        ElioOpportunitiesUsers opportunity = Sql.GetOpportunityById(Convert.ToInt32(popupConfirm.Attributes["aria-label"].ToString()), session);

                        if (opportunity != null)
                        {
                            TbxOpportConfId.Text = popupConfirm.Attributes["aria-label"].ToString();

                            if (popupConfirm.ID == "aMoveLeft")
                            {
                                BtnSave.Text = "Move";
                                TbxOpportAction.Text = "Left";

                                if (opportunity.StatusId == 2)
                                {
                                    LblConfMsg.Text = "Move this opportunity to " + LblStatusOne.Text + "?";    //Contact
                                }
                                else if (opportunity.StatusId == 3)
                                {
                                    LblConfMsg.Text = "Move this opportunity to " + LblStatusTwo.Text + "?";    //Meeting
                                }
                                else if (opportunity.StatusId == 4)
                                {
                                    LblConfMsg.Text = "Move this opportunity to " + LblStatusThree.Text + "?";  //Proposal
                                }
                            }
                            else if (popupConfirm.ID == "aMoveRight")
                            {
                                BtnSave.Text = "Move";
                                TbxOpportAction.Text = "Right";

                                if (opportunity.StatusId == 1)
                                {
                                    LblConfMsg.Text = "Move this opportunity to " + LblStatusTwo.Text + "?";    //Meeting
                                }
                                else if (opportunity.StatusId == 2)
                                {
                                    LblConfMsg.Text = "Move this opportunity to " + LblStatusThree.Text + "?";  //Proposal
                                }
                                else if (opportunity.StatusId == 3)
                                {
                                    LblConfMsg.Text = "Move this opportunity to " + LblStatusFour.Text + "?";  //Closed?";
                                }
                            }
                            else if (popupConfirm.ID == "aDelete")
                            {
                                BtnSave.Text = "Delete";
                                TbxOpportAction.Text = "Delete";
                                LblConfMsg.Text = "Delete this opportunity permanently?";
                            }

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
                            LoadOpportunities(DdlOpportKinds.SelectedItem.Value, TbxSearch.Text);
                        }
                        else
                        {
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                    }
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

        protected void BtnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!string.IsNullOrEmpty(TbxOpportConfId.Text) && !string.IsNullOrEmpty(TbxOpportAction.Text))
                    {
                        ElioOpportunitiesUsers opportunity = Sql.GetOpportunityById(Convert.ToInt32(TbxOpportConfId.Text), session);
                        DataLoader<ElioOpportunitiesUsers> loader = new DataLoader<ElioOpportunitiesUsers>(session);

                        if (TbxOpportAction.Text == "Right")
                        {
                            opportunity.StatusId++;

                            #region To Delete

                            //if (opportunity.StatusId == 1)
                            //{
                            //    opportunity.StatusId = 2;
                            //}
                            //else if (opportunity.StatusId == 2)
                            //{
                            //    opportunity.StatusId = 3;
                            //}
                            //else if (opportunity.StatusId == 3)
                            //{
                            //    opportunity.StatusId = 4;
                            //}

                            #endregion

                            opportunity.LastUpdated = DateTime.Now;

                            loader.Update(opportunity);

                            Sql.DeleteOpportunityUserSubCategoriesStatusByOpportunityId(opportunity.Id, session);
                        }
                        else if (TbxOpportAction.Text == "Left")
                        {
                            opportunity.StatusId--;

                            #region To Delete

                            //if (opportunity.StatusId == 4)
                            //{
                            //    opportunity.StatusId = 3;
                            //}
                            //else if (opportunity.StatusId == 3)
                            //{
                            //    opportunity.StatusId = 2;
                            //}
                            //else if (opportunity.StatusId == 2)
                            //{
                            //    opportunity.StatusId = 1;
                            //}

                            #endregion

                            opportunity.LastUpdated = DateTime.Now;

                            loader.Update(opportunity);

                            Sql.DeleteOpportunityUserSubCategoriesStatusByOpportunityId(opportunity.Id, session);
                        }
                        else
                        {
                            if (TbxOpportAction.Text == "Delete")
                            {
                                try
                                {
                                    session.BeginTransaction();

                                    Sql.DeleteOpportunityById(Convert.ToInt32(TbxOpportConfId.Text), session);

                                    session.CommitTransaction();

                                    #region To Delete

                                    //Sql.DeleteAllUserOpportunitiesNotes(Convert.ToInt32(TbxOpportConfId.Text), session);
                                    //Sql.DeleteAllUserOpportunitiesTasks(Convert.ToInt32(TbxOpportConfId.Text), session);
                                    //Sql.DeleteUserOpportunity(Convert.ToInt32(TbxOpportConfId.Text), session);

                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }
                        }

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "opportunities"), false);
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                    }
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

        protected void BtnClosedSubStatusSort_OnClick(object sender, EventArgs args)
        {
            try
            {
                //LoadOpportunities(DdlOpportKinds.SelectedItem.Value, TbxSearch.Text);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion
    }
}