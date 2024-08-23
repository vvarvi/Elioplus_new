using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Utils;
using System.Data;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus.Controls
{
    public partial class InboxContent : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        UpdateStrings();
                    }
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
            LblTitle.Text = "Inbox";
            LblActions.Text = "Actions";
            LblMarkAsRead.Text = "Mark as read";
            LblDelete.Text = "Delete";
        }

        # endregion

        # region Buttons

        protected void LnkBtnPreview_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LinkButton lnkBtnPreview = (LinkButton)sender;
                    GridDataItem item = (GridDataItem)lnkBtnPreview.NamingContainer;

                    Sql.UpdateUsersMessageIsNewStatusById(Convert.ToInt32(item["id"].Text), 0, session);

                    RdgMessages.Rebind();

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "messages/" + Convert.ToInt32(item["id"].Text) + "/view"), false);

                    //ElioUsersMessages message = Sql.GetUsersMessageById(Convert.ToInt32(item["id"].Text), session);
                    //if (message != null)
                    //{
                    //    message.IsNew = 0;
                    //    DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
                    //    loader.Update(message);

                    //    RdgMessages.Rebind();

                    //    Response.Redirect(ControlLoader.Dashboard(vSession.User, "messages/" + Convert.ToInt32(item["id"].Text) + "/view"), false);
                    //}
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

        protected void Delete_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (RdgMessages.Items.Count > 0)
                    {
                        foreach (GridDataItem item in RdgMessages.Items)
                        {
                            CheckBox chkBx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxMessage");
                            if (chkBx != null && chkBx.Checked)
                            {
                                Sql.UpdateUsersMessageDeleteStatusById(Convert.ToInt32(item["id"].Text), 1, session);

                                //ElioUsersMessages message = Sql.GetUsersMessageById(msgId, session);

                                //if (message != null)
                                //{
                                //    message.Deleted = 1;
                                //    DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
                                //    loader.Update(message);
                                //}
                            }
                        }

                        RdgMessages.Rebind();
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

        protected void MarkRead_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (RdgMessages.Items.Count > 0)
                    {
                        foreach (GridDataItem item in RdgMessages.Items)
                        {
                            CheckBox chkBx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxMessage");
                            if (chkBx != null && chkBx.Checked)
                            {
                                Sql.UpdateUsersMessageIsNewStatusById(Convert.ToInt32(item["id"].Text), 0, session);

                                //ElioUsersMessages message = Sql.GetUsersMessageById(msgId, session);

                                //if (message != null)
                                //{
                                //    message.IsNew = 0;
                                //    DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
                                //    loader.Update(message);
                                //}
                            }
                        }

                        RdgMessages.Rebind();
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

        # region Grids

        protected void RdgMessages_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ElioUsersMessages message = Sql.GetUsersMessageById(Convert.ToInt32(item["id"].Text), session);
                    if (message != null)
                    {
                        RdgMessages.MasterTableView.GetColumn("id").Display = false;

                        HtmlAnchor viewCompany = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aViewCompany");
                        ElioUsers senderCompany = Sql.GetUserById(message.SenderUserId, session);
                        if (senderCompany != null)
                        {
                            viewCompany.Visible = true;
                            viewCompany.HRef = ControlLoader.Profile(senderCompany);

                            LinkButton lnkBtnSenderName = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnSenderName");
                            lnkBtnSenderName.Text = senderCompany.CompanyName;
                        }
                        else
                            viewCompany.Visible = false;

                        LinkButton lnkBtnMessageSubject = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnMessageSubject");
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
                    int isNew = 1;
                    int deleted = 0;

                    List<ElioUsersMessages> newMessages = Sql.GetUsersMessagesByStatus(vSession.User.Id, isNew, deleted, session);

                    isNew = 0;
                    deleted = 0;

                    List<ElioUsersMessages> oldMessages = Sql.GetUsersMessagesByStatus(vSession.User.Id, isNew, deleted, session);

                    System.Linq.IOrderedEnumerable<ElioUsersMessages> allMessages = newMessages.Union(oldMessages).OrderByDescending(x => x.Sysdate);


                    if (allMessages.Count() > 0)
                    {
                        RdgMessages.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");

                        foreach (ElioUsersMessages message in allMessages)
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