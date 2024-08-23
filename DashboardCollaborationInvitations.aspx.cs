using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data;

namespace WdS.ElioPlus
{
    public partial class DashboardCollaborationInvitations : System.Web.UI.Page
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
            LblInvitationTitle.Text = "Invitation";
            LblInvitationSubject.Text = "Subject";
            LblInvitationContent.Text = "Invitation Content";
            LblAddOppNote.Text = "Add New Invitation";
            LblConfTitle.Text = "Confirmation";
            //LblDashboard.Text = "Dashboard";
            //LblDashPage.Text = "Collaboration Invitations";
            //LblElioplusDashboard.Text = "Manage your invitations";
            //LblDashSubTitle.Text = "keep them updated...";
            LblEditInvitationTitle.Text = "Edit Invitation";
            LblEditInvitationSubject.Text = "Subject";
            LblEditInvitationContent.Text = "Invitation Content";
            LblConfMsg.Text = "Are you sure you want to delete this invitation?";
        }

        private void SetLinks()
        {
            aBack.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-partners");  
        }
        
        # endregion

        #region Grids

        protected void RdgInvitations_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    TextBox tbxSubject = (TextBox)ControlFinder.FindControlRecursive(item, "TbxSubject");
                    tbxSubject.Text = item["inv_subject"].Text;

                    TextBox tbxContent = (TextBox)ControlFinder.FindControlRecursive(item, "TbxContent");
                    tbxContent.Text = item["inv_content"].Text;
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

        protected void RdgInvitations_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    int isPublic = 1;
                    List<ElioCollaborationUsersInvitations> invitations = SqlCollaboration.GetCollaborationUserAllInvitationsByStatus(vSession.User.Id, isPublic, session);

                    if (invitations.Count > 0)
                    {
                        RdgInvitations.Visible = true;
                        divNoResults.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("inv_subject");
                        table.Columns.Add("inv_content");
                        table.Columns.Add("date_created");
                        table.Columns.Add("last_updated");
                        table.Columns.Add("is_public");

                        foreach (ElioCollaborationUsersInvitations invitation in invitations)
                        {
                            table.Rows.Add(invitation.Id, invitation.InvSubject, invitation.InvContent, invitation.DateCreated.ToString("dd/MM/yyyy"), invitation.LastUpdated.ToString("dd/MM/yyyy"), invitation.IsPublic);
                        }

                        RdgInvitations.DataSource = table;
                    }
                    else
                    {
                        RdgInvitations.Visible = false;
                        divNoResults.Visible = true;
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

        #endregion

        # region Buttons

        protected void BtnDiscardEditInvitation_OnClick(object sender, EventArgs args)
        {
            try
            {
                divEditInvitationFailure.Visible = false;
                LblFailure.Text = string.Empty;
                LblInvitationFailure.Visible = false;

                TbxEditInvitationSubject.Text = string.Empty;
                TbxEditInvitationContent.Text = string.Empty;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseEditPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnDiscardInvitation_OnClick(object sender, EventArgs args)
        {
            try
            {
                divEditInvitationFailure.Visible = false;
                LblFailure.Text = string.Empty;
                LblInvitationFailure.Visible = false;

                TbxSubject.Text = string.Empty;
                TbxMsg.Text = string.Empty;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseInvitationPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                divEditInvitationFailure.Visible = false;

                TbxEditInvitationId.Text = string.Empty;
                TbxEditInvitationSubject.Text = string.Empty;
                TbxEditInvitationContent.Text = string.Empty;

                HtmlAnchor aEditBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aEditBtn.NamingContainer;

                TbxEditInvitationId.Text = item["id"].Text;
                TbxEditInvitationSubject.Text = item["inv_subject"].Text;
                TbxEditInvitationContent.Text = item["inv_content"].Text;

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
                    if (string.IsNullOrEmpty(TbxEditInvitationSubject.Text) || string.IsNullOrEmpty(TbxEditInvitationContent.Text))
                    {
                        divEditInvitationFailure.Visible = true;
                        LblEditInvitationFail.Text = "Invitation must have 'Subject' and 'Content'...";
                        return;
                    }
                    else
                    {
                        ElioCollaborationUsersInvitations invitation = SqlCollaboration.GetUserInvitationById(Convert.ToInt32(TbxEditInvitationId.Text), session);
                        if (invitation != null)
                        {
                            invitation.InvSubject = TbxEditInvitationSubject.Text;
                            invitation.InvContent = TbxEditInvitationContent.Text;
                            invitation.LastUpdated = DateTime.Now;

                            DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                            loader.Update(invitation);

                            TbxEditInvitationSubject.Text = string.Empty;
                            TbxEditInvitationContent.Text = string.Empty;
                            TbxEditInvitationId.Text = string.Empty;

                            divInvitationInfo.Visible = false;
                            divAddInvitationSucc.Visible = true;
                            LblAddInvitationSucc.Text = "Invitation was updated successfully.";

                            RdgInvitations.Rebind();

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseEditPopUp();", true);
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

        protected void BtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor aDelBtn = (HtmlAnchor)sender;                
                GridDataItem item = (GridDataItem)aDelBtn.NamingContainer;
                TbxInvitationConfId.Text = item["id"].Text;
                
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

                SqlCollaboration.DeleteCollaborationUserInvitationById(Convert.ToInt32(TbxInvitationConfId.Text), session);

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);

                divInvitationInfo.Visible = false;
                divAddInvitationSucc.Visible = true;

                LblAddInvitationSucc.Text = "Invitation was deleted successfully.";

                RdgInvitations.Rebind();
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
        
        protected void BtnAddInvitation_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                divInvitationFailure.Visible = false;

                if (vSession.User != null)
                {
                    if (string.IsNullOrEmpty(TbxSubject.Text))
                    {
                        divInvitationFailure.Visible = true;
                        LblFailure.Text = "Please add Subject";
                        return;
                    }

                    if (string.IsNullOrEmpty(TbxMsg.Text))
                    {
                        divInvitationFailure.Visible = true;
                        LblFailure.Text = "Please add Content";
                        return;
                    }

                    ElioCollaborationUsersInvitations invitation = new ElioCollaborationUsersInvitations();

                    invitation.UserId = vSession.User.Id;
                    invitation.InvSubject = TbxSubject.Text;
                    invitation.InvContent = TbxMsg.Text;
                    invitation.DateCreated = DateTime.Now;
                    invitation.LastUpdated = DateTime.Now;
                    invitation.IsPublic = 1;

                    DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                    loader.Insert(invitation);

                    divInvitationInfo.Visible = false;
                    divAddInvitationSucc.Visible = true;
                    LblAddInvitationSucc.Text = "Invitation was created and saved successfully!";

                    RdgInvitations.Rebind();

                    TbxSubject.Text = string.Empty;
                    TbxMsg.Text = string.Empty;

                    divInvitationFailure.Visible = false;

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseInvitationPopUp();", true);
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
        
        # endregion
    }
}