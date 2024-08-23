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
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Web.UI.WebControls;

namespace WdS.ElioPlus
{
    public partial class DashboardTasksAddEditPage : System.Web.UI.Page
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
            //divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

            UpdateStrings();
            SetLinks();
        }

        private void SetLinks()
        {
            aBack.HRef = ControlLoader.Dashboard(vSession.User, "opportunities");
        }

        private void UpdateStrings()
        {
            //LblDashboard.Text = "Dashboard";
            //LblDashPage.Text = "Opportunity Tasks";
            //LblElioplusDashboard.Text = "Manage Tasks";
            //LblDashSubTitle.Text = "keep them updated";
            LblAddOppTask.Text = "Add Task";
            LblAddTaskTitle.Text = "Create Task";
            LblAddTaskSubj.Text = "Subject";
            LblAddTaskCont.Text = "Content";
            LblTaskDate.Text = "Task remind date";
            LblConfTitle.Text = "Confirmation";
            LblConfMsg.Text = "Are you sure you want to delete this task?";
            LblEditTaskTitle.Text = "Edit Task";
            LblTaskEditDate.Text = "Remind date";
            LblTaskEditCont.Text = "Content";
            LblTaskEditSubj.Text = "Subject";
        }

        # endregion

        #region Grids

        protected void RdgTasks_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    TextBox tbxSubject = (TextBox)ControlFinder.FindControlRecursive(item, "TbxSubject");
                    tbxSubject.Text = item["task_subject"].Text;

                    TextBox tbxContent = (TextBox)ControlFinder.FindControlRecursive(item, "TbxContent");
                    tbxContent.Text = item["task_content"].Text;
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

        protected void RdgTasks_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (Request.QueryString["opportunityViewID"] != null)
                {
                    int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);
                    if (oppId > 0)
                    {
                        List<ElioOpportunitiesUsersTasks> tasks = Sql.GetUsersTasks(vSession.User.Id, oppId, session);

                        if (tasks.Count > 0)
                        {
                            RdgTasks.Visible = true;
                            divNoResults.Visible = false;

                            DataTable table = new DataTable();

                            table.Columns.Add("id");
                            table.Columns.Add("task_subject");
                            table.Columns.Add("task_content");
                            table.Columns.Add("sysdate");
                            table.Columns.Add("last_updated");
                            table.Columns.Add("remind_date");
                            table.Columns.Add("status");

                            foreach (ElioOpportunitiesUsersTasks task in tasks)
                            {
                                table.Rows.Add(task.Id, task.TaskSubject, task.TaskContent, task.Sysdate.ToString("MM/dd/yyyy"), task.LastUpdated.ToString("MM/dd/yyyy"), task.RemindDate.ToString("MM/dd/yyyy"), task.Status);
                            }

                            RdgTasks.DataSource = table;
                        }
                        else
                        {
                            RdgTasks.Visible = false;
                            divNoResults.Visible = true;
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
                divTaskEditFailure.Visible = false;

                TbxEditTaskId.Text = string.Empty;
                TbxEditTaskSubj.Text = string.Empty;
                TbxTaskEditCont.Text = string.Empty;


                HtmlAnchor aEditBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aEditBtn.NamingContainer;

                TbxEditTaskId.Text = item["id"].Text;
                TbxEditTaskSubj.Text = item["task_subject"].Text;
                TbxTaskEditCont.Text = item["task_content"].Text;
                RdpRemindDateEdit.SelectedDate = Convert.ToDateTime(item["remind_date"].Text);

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenEditPopUp();", true);
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
                        if (string.IsNullOrEmpty(TbxEditTaskSubj.Text) || string.IsNullOrEmpty(TbxTaskEditCont.Text) || RdpRemindDateEdit.SelectedDate == null)
                        {
                            divTaskEditFailure.Visible = true;
                            LblTaskEditFail.Text = "Task must have 'Subject', a 'Content' and a remind date";
                            return;
                        }
                        else if (RdpRemindDateEdit.SelectedDate <= DateTime.Now)
                        {
                            divTaskEditFailure.Visible = true;
                            LblTaskEditFail.Text = "Please select a date later than the current one";
                            return;
                        }
                        else
                        {
                            ElioOpportunitiesUsersTasks task = Sql.GetTaskById(Convert.ToInt32(TbxEditTaskId.Text), session);
                            if (task != null)
                            {
                                task.TaskSubject = TbxEditTaskSubj.Text;
                                task.TaskContent = TbxTaskEditCont.Text;
                                task.RemindDate = Convert.ToDateTime(RdpRemindDateEdit.SelectedDate);
                                task.LastUpdated = DateTime.Now;

                                DataLoader<ElioOpportunitiesUsersTasks> loader = new DataLoader<ElioOpportunitiesUsersTasks>(session);
                                loader.Update(task);

                                int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);

                                ElioOpportunitiesUsers opportunity = Sql.GetOpportunityById(oppId, session);
                                DataLoader<ElioOpportunitiesUsers> oloader = new DataLoader<ElioOpportunitiesUsers>(session);
                                opportunity.LastUpdated = DateTime.Now;
                                oloader.Update(opportunity);

                                TbxEditTaskSubj.Text = string.Empty;
                                TbxTaskEditCont.Text = string.Empty;
                                TbxEditTaskId.Text = string.Empty;
                                RdpRemindDateEdit.SelectedDate = null;

                                divTaskInfo.Visible = false;
                                divAddTaskSucc.Visible = true;
                                LblAddTaskSucc.Text = "Task was updated successfully.";

                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseEditPopUp();", true);

                                #region SCHEDULER SEND TASK REMINDER

                                //try
                                //{
                                //    EmailSenderLib.SendNewTaskNotificationEmail(task, vSession.Lang, session);
                                //}
                                //catch (Exception ex)
                                //{
                                //    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                //}

                                #endregion

                                RdgTasks.Rebind();
                            }
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

        protected void BtnAddTask_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (Request.QueryString["opportunityViewID"] != null)
                    {
                        if (string.IsNullOrEmpty(TbxAddTaskSubj.Text))
                        {
                            divAddTaskFailure.Visible = true;
                            LblAddTaskFail.Text = "Please add Task Subject";
                            return;
                        }

                        if (string.IsNullOrEmpty(TbxAddTaskCont.Text))
                        {
                            divAddTaskFailure.Visible = true;
                            LblAddTaskFail.Text = "Please add Task Content";
                            return;
                        }

                        if (RdpRemindDate.SelectedDate == null)
                        {
                            divAddTaskFailure.Visible = true;
                            LblAddTaskFail.Text = "Plese select Task remind date";
                            return;
                        }
                        else if (RdpRemindDate.SelectedDate <= DateTime.Now)
                        {
                            divAddTaskFailure.Visible = true;
                            LblAddTaskFail.Text = "Please select a remind date later than the current one";
                            return;
                        }
                        else
                        {
                            int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);
                            if (oppId > 0)
                            {
                                ElioOpportunitiesUsersTasks task = new ElioOpportunitiesUsersTasks();

                                task.UserId = vSession.User.Id;
                                task.OpportunityId = oppId;
                                task.TaskSubject = TbxAddTaskSubj.Text;
                                task.TaskContent = TbxAddTaskCont.Text;
                                task.Sysdate = DateTime.Now;
                                task.LastUpdated = DateTime.Now;
                                task.RemindDate = Convert.ToDateTime(RdpRemindDate.SelectedDate);

                                DataLoader<ElioOpportunitiesUsersTasks> loader = new DataLoader<ElioOpportunitiesUsersTasks>(session);
                                loader.Insert(task);

                                ElioOpportunitiesUsers opportunity = Sql.GetOpportunityById(oppId, session);
                                DataLoader<ElioOpportunitiesUsers> oloader = new DataLoader<ElioOpportunitiesUsers>(session);
                                opportunity.LastUpdated = DateTime.Now;
                                oloader.Update(opportunity);

                                #region SCHEDULER SEND TASK REMINDER

                                //try
                                //{
                                //    EmailSenderLib.SendNewTaskNotificationEmail(task, vSession.Lang, session);
                                //}
                                //catch (Exception ex)
                                //{
                                //    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                //}

                                #endregion

                                divTaskInfo.Visible = false;
                                divAddTaskSucc.Visible = true;
                                LblAddTaskSucc.Text = "Task was created and saved successfully!";

                                RdgTasks.Rebind();

                                TbxAddTaskSubj.Text = string.Empty;
                                TbxAddTaskCont.Text = string.Empty;

                                divAddTaskFailure.Visible = false;

                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseTaskPopUp();", true);
                            }
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

        protected void BtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor aDelBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aDelBtn.NamingContainer;
                TbxTaskConfId.Text = item["id"].Text;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmPopUp();", true);
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

                Sql.DeleteTaskById(Convert.ToInt32(TbxTaskConfId.Text), session);

                int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);

                ElioOpportunitiesUsers opportunity = Sql.GetOpportunityById(oppId, session);
                DataLoader<ElioOpportunitiesUsers> oloader = new DataLoader<ElioOpportunitiesUsers>(session);
                opportunity.LastUpdated = DateTime.Now;
                oloader.Update(opportunity);

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);

                divTaskInfo.Visible = false;
                divAddTaskSucc.Visible = true;

                LblAddTaskSucc.Text = "Task was deleted successfully.";

                RdgTasks.Rebind();
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

        protected void BtnDiscardAddTask_OnClick(object sender, EventArgs args)
        {
            try
            {
                divAddTaskFailure.Visible = false;
                LblAddTaskFail.Text = string.Empty;

                TbxAddTaskSubj.Text = string.Empty;
                TbxAddTaskCont.Text = string.Empty;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseTaskPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnDiscardEditTask_OnClick(object sender, EventArgs args)
        {
            try
            {
                divTaskEditFailure.Visible = false;

                TbxEditTaskId.Text = string.Empty;
                TbxEditTaskSubj.Text = string.Empty;
                TbxTaskEditCont.Text = string.Empty;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseEditPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion
    }
}