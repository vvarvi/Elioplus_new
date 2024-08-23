using System;
using System.Linq;
using System.Web.UI;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.EmailNotificationSender;

namespace WdS.ElioPlus.Controls.Modals
{
    public partial class AddTask : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public int OpportunityId
        {
            get
            {
                return ViewState["OpportunityId"] == null ? 0 : (int)ViewState["OpportunityId"];
            }
            set
            {
                ViewState["OpportunityId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixPage();
                }
                else
                {
                    Response.Redirect(ControlLoader.Default(), false);
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

        #region Methods

        private void FixPage()
        {
            ResetFields();
        }

        private void ResetFields()
        {
            divGeneralSuccess.Visible = false;
            divGeneralFailure.Visible = false;

            LblFailure.Text = string.Empty;
            LblSuccess.Text = string.Empty;
        }

        #endregion

        #region Buttons

        protected void BtnProccedMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ResetFields();

                if (vSession.User != null)
                {
                    if (OpportunityId > 0 || Request.QueryString["opportunityViewID"] != null)
                    {
                        if (TbxSubject.Text == string.Empty)
                        {
                            divGeneralFailure.Visible = true;
                            LblFailure.Text = "Please add Subject";
                            return;
                        }

                        if (TbxMsg.Text == string.Empty)
                        {
                            divGeneralFailure.Visible = true;
                            LblFailure.Text = "Please add Content";
                            return;
                        }

                        if (RdpRemindDate.SelectedDate == null)
                        {
                            divGeneralFailure.Visible = true;
                            LblFailure.Text = "Please select remind date";
                            return;
                        }
                        else
                        {
                            if (RdpRemindDate.SelectedDate <= DateTime.Now)
                            {
                                divGeneralFailure.Visible = true;
                                LblFailure.Text = "Please select date after today";
                                return;
                            }
                        }

                        ElioOpportunitiesUsersTasks task = new ElioOpportunitiesUsersTasks();

                        task.UserId = vSession.User.Id;
                        task.OpportunityId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);
                        task.TaskSubject = TbxSubject.Text;
                        task.TaskContent = TbxMsg.Text;
                        task.Sysdate = DateTime.Now;
                        task.LastUpdated = DateTime.Now;
                        task.RemindDate = Convert.ToDateTime(RdpRemindDate.SelectedDate);

                        DataLoader<ElioOpportunitiesUsersTasks> loader = new DataLoader<ElioOpportunitiesUsersTasks>(session);
                        loader.Insert(task);

                        divGeneralSuccess.Visible = true;
                        LblGeneralSuccess.Text = "Done! ";
                        LblSuccess.Text = "Your task was added successfully!";

                        RadGrid rdgTasks = (RadGrid)ControlFinder.FindControlBackWards(this, "RdgTasks");
                        if (rdgTasks != null)
                            rdgTasks.Rebind();

                        TbxSubject.Text = string.Empty;
                        TbxMsg.Text = string.Empty;
                        RdpRemindDate.SelectedDate = null;

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
                    }
                    else
                    {
                        divGeneralFailure.Visible = true;
                        LblFailure.Text = "You must select a contact in order to attach a note on it";
                        return;
                    }
                }
                else
                {
                    Response.Redirect(vSession.Page, false);
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

        protected void BtnCancelMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields();

                TbxSubject.Text = string.Empty;
                TbxMsg.Text = string.Empty;

                RadGrid rdgTasks = (RadGrid)ControlFinder.FindControlBackWards(this, "RdgTasks");
                if (rdgTasks != null)
                    rdgTasks.Rebind();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnBack_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "opportunities"), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}