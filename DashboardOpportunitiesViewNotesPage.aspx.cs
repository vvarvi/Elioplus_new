using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.IO;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI.Entities;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.Services.StripeAPI;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace WdS.ElioPlus
{
    public partial class DashboardOpportunitiesViewNotesPage : System.Web.UI.Page
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
                    if (!IsPostBack)
                    {
                        FixPage();
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

        # region Methods

        private void FixPage()
        {
            UpdateStrings();
            SetLinks();
        }

        private void UpdateStrings()
        {
            //LblNoteTitle.Text = "Note";
            //LblNoteSubject.Text = "Subject";
            //LblNoteContent.Text = "Content";
            LblAddOppNote.Text = "Add Note";
            LblConfTitle.Text = "Confirmation";
            //LblDashboard.Text = "Dashboard";
            //LblDashPage.Text = "Opportunity Notes";
            //LblElioplusDashboard.Text = "Manage notes";
            //LblDashSubTitle.Text = "keep them updated...";
            LblEditNoteTitle.Text = "Edit Note";
            //LblEditNoteSubject.Text = "Subject";
            //LblEditNoteContent.Text = "Content";
            LblConfMsg.Text = "Are you sure you want to delete this note?";
        }

        private void SetLinks()
        {
            //aBack.HRef = ControlLoader.Dashboard(vSession.User, "opportunities");
        }

        private void Bind(int oppId)
        {
            DataTable table = Sql.GetUsersOpportunityNotesByOpportunityIdTbl(oppId, session);

            if (table.Rows.Count > 0)
            {
                RdgNotes.Visible = true;
                UcMessageAlertControlGrid.Visible = false;

                //table.Columns.Add("id");
                //table.Columns.Add("note_subject");
                //table.Columns.Add("note_content");
                //table.Columns.Add("sysdate");
                //table.Columns.Add("last_updated");
                //table.Columns.Add("is_public");

                //foreach (ElioOpportunitiesNotes note in notes)
                //{
                //    table.Rows.Add(note.Id, note.NoteSubject, note.NoteContent, note.Sysdate.ToString("dd/MM/yyyy"), note.LastUpdated.ToString("dd/MM/yyyy"), note.IsPublic);
                //}

                RdgNotes.DataSource = table;
                RdgNotes.DataBind();
            }
            else
            {
                RdgNotes.Visible = false;
                GlobalMethods.ShowMessageControlDA(UcMessageAlertControlGrid, "There are no Notes yet, create your first one!", MessageTypes.Info, true, true, true, false, false);
            }
        }

        #endregion

        #region Grids

        protected void RdgNotes_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        RepeaterItem item = (RepeaterItem)args.Item;
                        if (item != null)
                        {
                            DataRowView row = (DataRowView)item.DataItem;
                            if (row != null)
                            {
                                Label lblSubject = (Label)ControlFinder.FindControlRecursive(item, "LblSubject");
                                lblSubject.Text = row["note_subject"].ToString();

                                Label lblContent = (Label)ControlFinder.FindControlRecursive(item, "LblContent");
                                lblContent.Text = row["note_content"].ToString();
                            }
                        }
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
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

        protected void RdgNotes_Load(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    if (Request.QueryString["opportunityViewID"] != null)
                    {
                        int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);
                        if (oppId > 0)
                        {
                            Bind(oppId);
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

        #endregion

        # region Buttons

        protected void BtnEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    divGeneralSuccess.Visible = divEditNoteFailure.Visible = false;

                    TbxEditNoteId.Value = "0";
                    TbxEditNoteSubject.Text = string.Empty;
                    TbxEditNoteContent.Text = string.Empty;

                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");
                            Label lblSubject = (Label)ControlFinder.FindControlRecursive(item, "LblSubject");
                            Label lblContent = (Label)ControlFinder.FindControlRecursive(item, "LblContent");

                            if (hdnId != null && lblSubject != null && lblContent != null)
                            {
                                TbxEditNoteId.Value = hdnId.Value;
                                TbxEditNoteSubject.Text = lblSubject.Text;
                                TbxEditNoteContent.Text = lblContent.Text;

                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenEditPopUp();", true);
                            }
                        }
                    }
                    else
                        Response.Redirect(ControlLoader.Login, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnConfEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (Request.QueryString["opportunityViewID"] != null)
                    {
                        if (string.IsNullOrEmpty(TbxEditNoteSubject.Text))
                        {
                            divEditNoteFailure.Visible = true;
                            LblEditNoteFail.Text = "Please add Subject";
                            return;
                        }

                        if (string.IsNullOrEmpty(TbxEditNoteContent.Text))
                        {
                            divEditNoteFailure.Visible = true;
                            LblEditNoteFail.Text = "Please add Content";
                            return;
                        }

                        ElioOpportunitiesNotes note = Sql.GetUsersOpportunityNoteById(Convert.ToInt32(TbxEditNoteId.Value), session);
                        if (note != null)
                        {
                            note.NoteSubject = TbxEditNoteSubject.Text;
                            note.NoteContent = TbxEditNoteContent.Text;
                            note.LastUpdated = DateTime.Now;

                            DataLoader<ElioOpportunitiesNotes> loader = new DataLoader<ElioOpportunitiesNotes>(session);
                            loader.Update(note);

                            int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);

                            ElioOpportunitiesUsers opportunity = Sql.GetOpportunityById(oppId, session);
                            DataLoader<ElioOpportunitiesUsers> oloader = new DataLoader<ElioOpportunitiesUsers>(session);
                            opportunity.LastUpdated = DateTime.Now;
                            oloader.Update(opportunity);

                            TbxEditNoteSubject.Text = string.Empty;
                            TbxEditNoteContent.Text = string.Empty;
                            TbxEditNoteId.Value = "0";

                            Bind(oppId);

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseEditPopUp();", true);

                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControlGrid, "Note was updated successfully.", MessageTypes.Success, true, true, true, true, false);
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "opportunities"), false);
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "opportunities"), false);
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

        protected void BtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    divGeneralSuccess.Visible = divEditNoteFailure.Visible = UcPopUpConfirmationMessageAlert.Visible = false;

                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");

                            if (hdnId != null)
                            {
                                TbxNoteConfId.Value = hdnId.Value;

                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmPopUp();", true);
                            }
                        }
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

        protected void BtnConfDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcPopUpConfirmationMessageAlert.Visible = divGeneralSuccess.Visible = divEditNoteFailure.Visible = false;

                Sql.DeleteOpportunityNoteById(Convert.ToInt32(TbxNoteConfId.Value), session);

                int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);

                ElioOpportunitiesUsers opportunity = Sql.GetOpportunityById(oppId, session);
                DataLoader<ElioOpportunitiesUsers> oloader = new DataLoader<ElioOpportunitiesUsers>(session);
                opportunity.LastUpdated = DateTime.Now;
                oloader.Update(opportunity);

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);

                Bind(oppId);

                GlobalMethods.ShowMessageControlDA(UcMessageAlertControlGrid, "Note was deleted successfully.", MessageTypes.Success, true, true, true, true, false);
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

        protected void BtnAddEditNote_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    divEditNoteFailure.Visible = divGeneralSuccess.Visible = UcPopUpConfirmationMessageAlert.Visible = false;

                    if (Request.QueryString["opportunityViewID"] != null)
                    {
                        if (string.IsNullOrEmpty(TbxEditNoteSubject.Text))
                        {
                            divEditNoteFailure.Visible = true;
                            LblEditNoteFail.Text = "Please add Subject";
                            return;
                        }

                        if (string.IsNullOrEmpty(TbxEditNoteContent.Text))
                        {
                            divEditNoteFailure.Visible = true;
                            LblEditNoteFail.Text = "Please add Content";
                            return;
                        }

                        string alert = "";

                        int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);
                        if (oppId > 0)
                        {
                            DataLoader<ElioOpportunitiesNotes> loader = new DataLoader<ElioOpportunitiesNotes>(session);
                            ElioOpportunitiesNotes note = null;

                            if (TbxEditNoteId.Value == "0")
                            {
                                #region Insert new note

                                note = new ElioOpportunitiesNotes();

                                note.UserId = vSession.User.Id;
                                note.OpportunityUserId = oppId;
                                note.NoteSubject = TbxEditNoteSubject.Text;
                                note.NoteContent = TbxEditNoteContent.Text;
                                note.Sysdate = DateTime.Now;
                                note.LastUpdated = DateTime.Now;
                                note.IsPublic = 1;

                                loader.Insert(note);

                                TbxEditNoteId.Value = note.Id.ToString();

                                alert = "Note was created successfully!";

                                #endregion
                            }
                            else
                            {
                                #region update note

                                note = Sql.GetUsersOpportunityNoteById(Convert.ToInt32(TbxEditNoteId.Value), session);
                                if (note != null)
                                {
                                    note.NoteSubject = TbxEditNoteSubject.Text;
                                    note.NoteContent = TbxEditNoteContent.Text;
                                    note.LastUpdated = DateTime.Now;

                                    loader.Update(note);

                                    alert = "Note was updated successfully!";
                                }

                                #endregion
                            }

                            #region update opportunity

                            ElioOpportunitiesUsers opportunity = Sql.GetOpportunityById(oppId, session);
                            DataLoader<ElioOpportunitiesUsers> oloader = new DataLoader<ElioOpportunitiesUsers>(session);
                            opportunity.LastUpdated = DateTime.Now;
                            oloader.Update(opportunity);

                            #endregion

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseEditPopUp();", true);

                            Bind(oppId);

                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControlGrid, alert, MessageTypes.Success, true, true, true, true, false);
                        }
                        else
                        {
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "opportunities"), false);
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "opportunities"), false);
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

        protected void BtnDiscardEditNote_OnClick(object sender, EventArgs args)
        {
            try
            {
                divEditNoteFailure.Visible = divGeneralSuccess.Visible = UcMessageAlertControlGrid.Visible = false;

                TbxEditNoteId.Value = "0";
                TbxEditNoteSubject.Text = string.Empty;
                TbxEditNoteContent.Text = string.Empty;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseEditPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aAddNote_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    divEditNoteFailure.Visible = divGeneralSuccess.Visible = UcMessageAlertControlGrid.Visible = false;

                    if (Request.QueryString["opportunityViewID"] != null)
                    {
                        TbxEditNoteId.Value = "0";
                        TbxEditNoteSubject.Text = TbxEditNoteContent.Text = "";

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "OpenEditPopUp();", true);
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

        #endregion
    }
}