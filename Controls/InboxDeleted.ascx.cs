using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using System.Data;
using WdS.ElioPlus.Lib.Utils;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus.Controls
{
    public partial class InboxDeleted : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    UpdateStrings();
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
        }

        # region Methods

        private void UpdateStrings()
        {
            LblTitle.Text = "Deleted";
            LblActions.Text = "Actions";
            LblDelete.Text = "Delete";
            LblRestore.Text = "Restore";
        }

        # endregion

        # region Buttons

        protected void LnkBtnPreview_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LinkButton lnkBtnPreview = (LinkButton)sender;
                GridDataItem item = (GridDataItem)lnkBtnPreview.NamingContainer;

                ElioUsersMessages message = Sql.GetUsersMessageById(Convert.ToInt32(item["id"].Text), session);
                if (message != null)
                {
                    message.IsNew = 0;
                    DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
                    loader.Update(message);

                    RdgMessages.Rebind();

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "messages/" + Convert.ToInt32(item["id"].Text) + "/view"), false);
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

        protected void Delete_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (RdgMessages.Items.Count > 0)
                {
                    foreach (GridDataItem item in RdgMessages.Items)
                    {
                        CheckBox chkBx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxMessage");
                        if (chkBx != null && chkBx.Checked)
                        {
                            Sql.DeleteUserMessageById(Convert.ToInt32(item["id"].Text), session);                           
                        }
                    }

                    RdgMessages.Rebind();
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

        protected void Restore_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (RdgMessages.Items.Count > 0)
                {
                    foreach (GridDataItem item in RdgMessages.Items)
                    {
                        CheckBox chkBx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxMessage");
                        if (chkBx != null && chkBx.Checked)
                        {
                            ElioUsersMessages message = Sql.GetUsersMessageById(Convert.ToInt32(item["id"].Text), session);

                            if (message != null)
                            {
                                message.Deleted = 0;
                                DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
                                loader.Update(message);
                            }
                        }
                    }

                    RdgMessages.Rebind();
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

        # endregion

        # region Grids

        protected void RdgMessages_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    
                    ElioUsersMessages message = Sql.GetUsersMessageById(Convert.ToInt32(item["id"].Text), session);
                    if (message != null)
                    {
                        RdgMessages.MasterTableView.GetColumn("id").Display = false;

                        HtmlAnchor viewCompany = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aViewCompany");
                        LinkButton lnkBtnSenderName = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnSenderName");
                        LinkButton lnkBtnMessageSubject = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnMessageSubject");
                        scriptManager.RegisterPostBackControl(lnkBtnSenderName);
                        scriptManager.RegisterPostBackControl(lnkBtnMessageSubject);

                        ElioUsers senderCompany = Sql.GetUserById(message.SenderUserId, session);

                        if (senderCompany != null)
                        {
                            viewCompany.HRef = ControlLoader.Profile(senderCompany);                            
                            lnkBtnSenderName.Text = senderCompany.CompanyName;
                        }
                        else
                        {
                            viewCompany.HRef = "#";
                            lnkBtnSenderName.Text = "Sender company name not available";
                        }

                        lnkBtnMessageSubject.Text = message.Subject;

                        Label lblDateReceived = (Label)ControlFinder.FindControlRecursive(item, "LblDateReceived");
                        lblDateReceived.Text = message.LastUpdated.ToString("MM/dd/yyyy");

                        HtmlTableRow messageRow = (HtmlTableRow)ControlFinder.FindControlRecursive(item, "rowMessage");
                        if (message.IsNew == 1)
                        {
                            messageRow.Attributes["class"] = "unread";
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

        protected void RdgMessages_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    int deleted = 1;

                    List<ElioUsersMessages> deletedMessages = Sql.GetUsersDeletedMessagesByStatus(vSession.User.Id, deleted, session);

                    List<ElioUsersMessages> deletedSentMessages = Sql.GetUsersSentDeletedMessagesByStatus(vSession.User.Id, deleted, session);

                    System.Linq.IOrderedEnumerable<ElioUsersMessages> allDeletedMessages = deletedMessages.Union(deletedSentMessages).OrderByDescending(x => x.Sysdate);

                    if (allDeletedMessages.Count() > 0)
                    {
                        RdgMessages.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");

                        foreach (ElioUsersMessages message in allDeletedMessages)
                        {
                            table.Rows.Add(message.Id);
                        }

                        RdgMessages.DataSource = table;
                    }
                    else
                    {
                        RdgMessages.Visible = false;
                        LblInfo.Text = "Info! ";
                        LblInfoContent.Text = "No messages yet.";
                        divInfo.Visible = true;
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

        # endregion
    }
}