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
    public partial class DashboardOpportunitiesViewTasksPage : System.Web.UI.Page
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
            LblAddOppTask.Text = "Add Task";
            LblConfTitle.Text = "Confirmation";
            //LblDashboard.Text = "Dashboard";
            //LblDashPage.Text = "Opportunity Notes";
            //LblElioplusDashboard.Text = "Manage notes";
            //LblDashSubTitle.Text = "keep them updated...";
            LblEditTaskTitle.Text = "Edit Task";
            //LblEditNoteSubject.Text = "Subject";
            //LblEditNoteContent.Text = "Content";
            LblConfMsg.Text = "Are you sure you want to delete this task?";
        }

        private void SetLinks()
        {
            //aBack.HRef = ControlLoader.Dashboard(vSession.User, "opportunities");
        }

        private void Bind(int oppId)
        {
            DataTable table = Sql.GetUsersTasksTbl(vSession.User.Id, oppId, session);

            if (table.Rows.Count > 0)
            {
                RdgTasks.Visible = true;
                UcMessageAlertControlGrid.Visible = false;

                RdgTasks.DataSource = table;
                RdgTasks.DataBind();
            }
            else
            {
                RdgTasks.Visible = false;
                GlobalMethods.ShowMessageControlDA(UcMessageAlertControlGrid, "There are no Task yet, create your first one!", MessageTypes.Info, true, true, true, false, false);
            }
        }

        #endregion

        #region Grids

        protected void RdgTask_OnItemDataBound(object sender, RepeaterItemEventArgs args)
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
                                lblSubject.Text = row["task_subject"].ToString();

                                Label lblContent = (Label)ControlFinder.FindControlRecursive(item, "LblContent");
                                lblContent.Text = row["task_content"].ToString();
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

        protected void RdgTasks_Load(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (Request.QueryString["opportunityViewID"] != null)
                {
                    int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);
                    if (oppId > 0)
                    {
                        Bind(oppId);
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
                    divGeneralSuccess.Visible = divEditTaskFailure.Visible = false;

                    TbxEditTaskId.Value = "0";
                    TbxEditTaskSubject.Text = string.Empty;
                    TbxEditTaskContent.Text = string.Empty;
                    RdpRemindDateEdit.SelectedDate = null;

                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");
                            Label lblSubject = (Label)ControlFinder.FindControlRecursive(item, "LblSubject");
                            Label lblContent = (Label)ControlFinder.FindControlRecursive(item, "LblContent");
                            Label lblRemindDate = (Label)ControlFinder.FindControlRecursive(item, "LblRemindDate");

                            if (hdnId != null && lblSubject != null && lblContent != null)
                            {
                                TbxEditTaskId.Value = hdnId.Value;
                                TbxEditTaskSubject.Text = lblSubject.Text;
                                TbxEditTaskContent.Text = lblContent.Text;
                                RdpRemindDateEdit.SelectedDate = Convert.ToDateTime(lblRemindDate.Text);

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
        
        protected void BtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    divGeneralSuccess.Visible = divEditTaskFailure.Visible = UcPopUpConfirmationMessageAlert.Visible = false;

                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");

                            if (hdnId != null)
                            {
                                TbxTaskConfId.Value = hdnId.Value;

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

                UcPopUpConfirmationMessageAlert.Visible = divGeneralSuccess.Visible = divEditTaskFailure.Visible = false;

                Sql.DeleteTaskById(Convert.ToInt32(TbxTaskConfId.Value), session);

                int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);

                ElioOpportunitiesUsers opportunity = Sql.GetOpportunityById(oppId, session);
                DataLoader<ElioOpportunitiesUsers> oloader = new DataLoader<ElioOpportunitiesUsers>(session);
                opportunity.LastUpdated = DateTime.Now;
                oloader.Update(opportunity);

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);

                Bind(oppId);

                GlobalMethods.ShowMessageControlDA(UcMessageAlertControlGrid, "Task was deleted successfully.", MessageTypes.Success, true, true, true, true, false);
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

        protected void BtnAddEditTask_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    divEditTaskFailure.Visible = divGeneralSuccess.Visible = UcPopUpConfirmationMessageAlert.Visible = UcMessageAlertControlGrid.Visible = false;

                    if (Request.QueryString["opportunityViewID"] != null)
                    {
                        if (string.IsNullOrEmpty(TbxEditTaskSubject.Text))
                        {
                            divEditTaskFailure.Visible = true;
                            LblEditTaskFail.Text = "Please add Subject";
                            return;
                        }

                        if (string.IsNullOrEmpty(TbxEditTaskContent.Text))
                        {
                            divEditTaskFailure.Visible = true;
                            LblEditTaskFail.Text = "Please add Content";
                            return;
                        }

                        if (RdpRemindDateEdit.SelectedDate == null)
                        {
                            divEditTaskFailure.Visible = true;
                            LblEditTaskFail.Text = "Please select a remind date";
                            return;
                        }
                        else
                        {
                            if (RdpRemindDateEdit.SelectedDate <= DateTime.Now)
                            {
                                divEditTaskFailure.Visible = true;
                                LblEditTaskFail.Text = "Please select a date later than the current one";
                                return;
                            }
                        }

                        string alert = "";

                        int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);
                        if (oppId > 0)
                        {
                            DataLoader<ElioOpportunitiesUsersTasks> loader = new DataLoader<ElioOpportunitiesUsersTasks>(session);
                            ElioOpportunitiesUsersTasks task = null;

                            if (TbxEditTaskId.Value == "0")
                            {
                                #region Insert new task

                                task = new ElioOpportunitiesUsersTasks();

                                task.UserId = vSession.User.Id;
                                task.OpportunityId = oppId;
                                task.TaskSubject = TbxEditTaskSubject.Text;
                                task.TaskContent = TbxEditTaskContent.Text;
                                task.Sysdate = DateTime.Now;
                                task.LastUpdated = DateTime.Now;
                                task.RemindDate = Convert.ToDateTime(RdpRemindDateEdit.SelectedDate);

                                loader.Insert(task);

                                TbxEditTaskId.Value = task.Id.ToString();

                                alert = "Task was created successfully!";

                                #endregion
                            }
                            else
                            {
                                #region update task

                                task = Sql.GetTaskById(Convert.ToInt32(TbxEditTaskId.Value), session);
                                if (task != null)
                                {
                                    task.TaskSubject = TbxEditTaskSubject.Text;
                                    task.TaskContent = TbxEditTaskContent.Text;
                                    task.RemindDate = Convert.ToDateTime(RdpRemindDateEdit.SelectedDate);
                                    task.LastUpdated = DateTime.Now;

                                    loader.Update(task);

                                    alert = "Task was updated successfully!";
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

        protected void BtnDiscardEditTask_OnClick(object sender, EventArgs args)
        {
            try
            {
                divEditTaskFailure.Visible = divGeneralSuccess.Visible = UcMessageAlertControlGrid.Visible = false;

                TbxEditTaskId.Value = "0";
                TbxEditTaskSubject.Text = string.Empty;
                TbxEditTaskContent.Text = string.Empty;
                RdpRemindDateEdit.SelectedDate = null;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseEditPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aAddTask_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    divEditTaskFailure.Visible = divGeneralSuccess.Visible = UcMessageAlertControlGrid.Visible = false;

                    if (Request.QueryString["opportunityViewID"] != null)
                    {
                        TbxEditTaskId.Value = "0";
                        TbxEditTaskSubject.Text = TbxEditTaskContent.Text = "";
                        RdpRemindDateEdit.SelectedDate = null;

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