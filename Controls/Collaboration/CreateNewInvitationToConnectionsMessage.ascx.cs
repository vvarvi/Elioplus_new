using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Data;
using WdS.ElioPlus.Lib.Localization;

namespace WdS.ElioPlus.Controls.Collaboration
{
    public partial class CreateNewInvitationToConnectionsMessage : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        #region Page ViewState properties

        public int InvitationId
        {
            get
            {
                return (this.ViewState["InvitationId"] != null) ? Convert.ToInt32(ViewState["InvitationId"]) : -1;
            }

            set { this.ViewState["InvitationId"] = value; }
        }

        public string InvitationSbj
        {
            get
            {
                return (this.ViewState["InvitationSbj"] != null) ? ViewState["InvitationSbj"].ToString() : null;
            }

            set { this.ViewState["InvitationSbj"] = value; }
        }

        public string InvitationMsg
        {
            get
            {
                return (this.ViewState["InvitationMsg"] != null) ? ViewState["InvitationMsg"].ToString() : null;
            }

            set { this.ViewState["InvitationMsg"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    UpdateStrings();
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

        private void UpdateStrings()
        {
            ImgBtnSave.ToolTip = "Save changes of this Invitation";
        }

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

            ImgStatus.Visible = false;
            ImgBtnSave.Visible = false;
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
                    RadGrid rdgResellers = (RadGrid)ControlFinder.FindControlBackWards(this, "RdgResellers");
                    if (rdgResellers != null)
                    {
                        List<ElioUsers> partners = new List<ElioUsers>();

                        bool hasSelectedItem = false;
                        foreach (GridDataItem item in rdgResellers.Items)
                        {
                            CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
                            if (cbx != null)
                            {
                                if (cbx.Checked)
                                {
                                    hasSelectedItem = true;
                                    ElioUsers partner = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                                    if (partner != null)
                                    {
                                        partners.Add(partner);
                                    }
                                }
                            }
                        }

                        if (!hasSelectedItem)
                        {
                            divGeneralFailure.Visible = true;
                            LblFailure.Text = "Please select at least one partner in order to send your invitation";
                            return;
                        }
                        else
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

                            ElioCollaborationUsersInvitations invitation = null;

                            DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);

                            if (InvitationId != -1)
                            {
                                invitation = SqlCollaboration.GetUserInvitationById(InvitationId, session);
                                if (invitation != null)
                                {
                                    invitation.InvSubject = TbxSubject.Text;
                                    invitation.InvContent = TbxMsg.Text;
                                    invitation.LastUpdated = DateTime.Now;

                                    loader.Update(invitation);
                                }
                                else
                                {
                                    invitation = new ElioCollaborationUsersInvitations();

                                    invitation.UserId = vSession.User.Id;
                                    invitation.InvSubject = TbxSubject.Text;
                                    invitation.InvContent = TbxMsg.Text;
                                    invitation.DateCreated = DateTime.Now;
                                    invitation.LastUpdated = DateTime.Now;
                                    invitation.IsPublic = 1;

                                    loader.Insert(invitation);
                                }
                            }
                            else
                            {
                                invitation = new ElioCollaborationUsersInvitations();

                                invitation.UserId = vSession.User.Id;
                                invitation.InvSubject = TbxSubject.Text;
                                invitation.InvContent = TbxMsg.Text;
                                invitation.DateCreated = DateTime.Now;
                                invitation.LastUpdated = DateTime.Now;
                                invitation.IsPublic = 1;

                                loader.Insert(invitation);
                            }

                            foreach (ElioUsers partner in partners)
                            {
                                try
                                {
                                    session.BeginTransaction();

                                    //bool existCollaboration = SqlCollaboration.ExistCollaboration(vSession.User.Id, partner.Id, session);
                                    //int collaborationId = SqlCollaboration.GetCollaborationId(vSession.User.Id, partner.Id, session);
                                    int masterUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : partner.Id;
                                    int partnerUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? partner.Id : vSession.User.Id;

                                    ElioCollaborationVendorsResellers collaboration = SqlCollaboration.GetCollaborationByVendorAndResellerId(masterUserId, partnerUserId, session);
                                    if (collaboration == null)
                                    {
                                        collaboration = new ElioCollaborationVendorsResellers();

                                        collaboration.MasterUserId = masterUserId;
                                        collaboration.PartnerUserId = partnerUserId;
                                        collaboration.IsActive = 1;
                                        collaboration.InvitationStatus = CollaborateInvitationStatus.Pending.ToString();
                                        collaboration.Sysdate = DateTime.Now;
                                        collaboration.LastUpdated = DateTime.Now;

                                        DataLoader<ElioCollaborationVendorsResellers> loader2 = new DataLoader<ElioCollaborationVendorsResellers>(session);
                                        loader2.Insert(collaboration);

                                        ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                                        vendorResselerInvitation.UserId = vSession.User.Id;
                                        vendorResselerInvitation.VendorResellerId = collaboration.Id;
                                        vendorResselerInvitation.UserInvitationId = invitation.Id;
                                        vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                                        vendorResselerInvitation.RecipientEmail = partner.Email;
                                        vendorResselerInvitation.SendDate = DateTime.Now;
                                        vendorResselerInvitation.LastUpdated = DateTime.Now;
                                        vendorResselerInvitation.LastUpdated = DateTime.Now;

                                        DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                        loader3.Insert(vendorResselerInvitation);
                                    }
                                    else
                                    {
                                        ElioCollaborationVendorResellerInvitations venResInvitation = SqlCollaboration.GetCollaborationVendorResellerInvitationByVenResIdAndInvId(collaboration.Id, invitation.Id, session);
                                        if (venResInvitation == null)
                                        {
                                            ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                                            vendorResselerInvitation.UserId = vSession.User.Id;
                                            vendorResselerInvitation.VendorResellerId = collaboration.Id;
                                            vendorResselerInvitation.UserInvitationId = invitation.Id;
                                            vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                                            vendorResselerInvitation.RecipientEmail = partner.Email;
                                            vendorResselerInvitation.SendDate = DateTime.Now;
                                            vendorResselerInvitation.LastUpdated = DateTime.Now;
                                            vendorResselerInvitation.LastUpdated = DateTime.Now;

                                            DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                            loader3.Insert(vendorResselerInvitation);
                                        }

                                        collaboration.InvitationStatus = CollaborateInvitationStatus.Pending.ToString();
                                        collaboration.LastUpdated = DateTime.Now;

                                        DataLoader<ElioCollaborationVendorsResellers> colLoader = new DataLoader<ElioCollaborationVendorsResellers>(session);
                                        colLoader.Update(collaboration);
                                    }

                                    session.CommitTransaction();

                                    ////try
                                    ////{
                                    ////    //maybe asynchronously send emails, to do
                                    ////    EmailSenderLib.CollaborationInvitationEmail(partner.Email, vSession.User.CompanyName, "confirmationLink", vSession.Lang, session);
                                    ////}
                                    ////catch (Exception ex)
                                    ////{
                                    ////    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    ////}
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }

                            divGeneralSuccess.Visible = true;
                            LblGeneralSuccess.Text = "Done! ";
                            LblSuccess.Text = "Successfully sent";

                            TbxSubject.Text = string.Empty;
                            TbxMsg.Text = string.Empty;

                            TbxSubject.ReadOnly = false;
                            TbxMsg.ReadOnly = false;

                            InvitationMsg = null;
                            InvitationSbj = null;
                            ViewState["InvitationId"] = null;

                            RdgMyInvitations.Rebind();

                            rdgResellers.Rebind();

                            UpdatePanel updatePnl = (UpdatePanel)ControlFinder.FindControlBackWards(this, "UpdatePnl");
                            updatePnl.Update();
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

        protected void BtnCancelMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields();

                TbxSubject.Text = string.Empty;
                TbxMsg.Text = string.Empty;

                TbxSubject.ReadOnly = false;
                TbxMsg.ReadOnly = false;

                ImgBtnCancel.Visible = false;
                ImgBtnEdit.Visible = false;

                InvitationSbj = null;
                InvitationMsg = null;
                ViewState["InvitationId"] = null;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConnectionsInvitationPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnClear_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields();

                TbxSubject.Text = string.Empty;
                TbxMsg.Text = string.Empty;

                ImgBtnEdit.Visible = false;
                ImgBtnCancel.Visible = false;

                TbxSubject.ReadOnly = false;
                TbxMsg.ReadOnly = false;

                InvitationSbj = null;
                InvitationMsg = null;
                ViewState["InvitationId"] = null;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnLoad_OnClick(object sender, EventArgs args)
        {
            try
            {
                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                TbxSubject.Text = string.Empty;
                TbxSubject.Text = string.Empty;

                InvitationId = Convert.ToInt32(item["id"].Text);

                TbxSubject.Text = InvitationSbj = item["inv_subject"].Text;
                TbxMsg.Text = InvitationMsg = item["inv_content"].Text;

                TbxSubject.ReadOnly = true;
                TbxMsg.ReadOnly = true;

                ResetFields();

                ImgBtnEdit.Visible = true;
                ImgBtnCancel.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                SqlCollaboration.DeleteCollaborationUserInvitationById(Convert.ToInt32(item["id"].Text), session);

                RdgMyInvitations.Rebind();

                ResetFields();

                TbxSubject.Text = string.Empty;
                TbxMsg.Text = string.Empty;

                TbxSubject.ReadOnly = false;
                TbxMsg.ReadOnly = false;

                ImgBtnEdit.Visible = false;
                ImgBtnCancel.Visible = false;

                InvitationSbj = null;
                InvitationMsg = null;
                ViewState["InvitationId"] = null;

                divGeneralSuccess.Visible = true;
                LblGeneralSuccess.Text = "Done! ";
                LblSuccess.Text = "Your invitation deleted!";
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

        protected void ImgBtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (InvitationId != -1)
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

                    ResetFields();

                    if (TbxSubject.Text == InvitationSbj && TbxMsg.Text == InvitationMsg)
                    {
                        ElioCollaborationUsersInvitations invitation = SqlCollaboration.GetUserInvitationById(InvitationId, session);
                        if (invitation != null)
                        {
                            invitation.LastUpdated = DateTime.Now;

                            DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                            loader.Update(invitation);
                            
                            LblSuccess.Text = "Your invitation was updated successfully!";
                        }
                        else
                        {
                            divGeneralFailure.Visible = true;
                            LblFailure.Text = "Your invitation could not updated. Please try again later!";
                            ImgStatus.ImageUrl = "~/images/icons/small/rejected_1.png";

                            return;
                        }
                    }
                    else
                    {
                        ElioCollaborationUsersInvitations invitation = new ElioCollaborationUsersInvitations();

                        invitation.InvSubject = InvitationSbj = TbxSubject.Text;
                        invitation.InvContent = InvitationMsg = TbxMsg.Text;
                        invitation.IsPublic = 1;
                        invitation.UserId = vSession.User.Id;
                        invitation.DateCreated = DateTime.Now;
                        invitation.LastUpdated = DateTime.Now;

                        DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                        loader.Insert(invitation);

                        InvitationId = invitation.Id;
    
                        LblSuccess.Text = "Your invitation was inserted successfully!";
                    }

                    RdgMyInvitations.Rebind();

                    ImgBtnSave.Visible = false;
                    ImgBtnCancel.Visible = false;
                    ImgBtnEdit.Visible = true;

                    TbxSubject.ReadOnly = true;
                    TbxMsg.ReadOnly = true;

                    ImgStatus.Visible = true;
                    ImgStatus.ImageUrl = "~/images/icons/small/success.png";

                    divGeneralSuccess.Visible = true;
                    LblGeneralSuccess.Text = "Done! ";
                }
                else
                {
                    divGeneralFailure.Visible = true;
                    LblFailure.Text = "Your invitation could not updated. Please try again later!";
                    ImgStatus.ImageUrl = "~/images/icons/small/rejected_2.png";
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

        protected void ImgBtnEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ResetFields();

                if (InvitationId != -1)
                {
                    TbxSubject.ReadOnly = false;
                    TbxMsg.ReadOnly = false;

                    ImgBtnEdit.Visible = false;
                    ImgBtnSave.Visible = true;
                    ImgBtnCancel.Visible = true;
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

        protected void ImgBtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ResetFields();

                if (InvitationId != -1)
                {
                    TbxSubject.Text = InvitationSbj;
                    TbxMsg.Text = InvitationMsg;

                    TbxSubject.ReadOnly = true;
                    TbxMsg.ReadOnly = true;

                    ImgBtnSave.Visible = false;
                    ImgBtnCancel.Visible = false;
                    ImgBtnEdit.Visible = true;
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

        #region Grids

        protected void RdgMyInvitations_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ImageButton imgBtnLoad = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnLoad");
                    imgBtnLoad.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "1")).Text;
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

        protected void RdgMyInvitations_OnNeedDataSource(object sender, EventArgs args)
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
                        RdgMyInvitations.Visible = true;
                        UcMessageAlert.Visible = false;
                        divInvitations.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("user_id");
                        table.Columns.Add("inv_subject");
                        table.Columns.Add("inv_content");
                        table.Columns.Add("date_created");
                        table.Columns.Add("last_updated");

                        foreach (ElioCollaborationUsersInvitations invitation in invitations)
                        {
                            table.Rows.Add(invitation.Id, invitation.UserId, invitation.InvSubject, invitation.InvContent, invitation.DateCreated.ToString("dd/MM/yyyy"), invitation.LastUpdated.ToString("dd/MM/yyyy"));
                        }

                        RdgMyInvitations.DataSource = table;
                    }
                    else
                    {
                        RdgMyInvitations.Visible = false;

                        divInvitations.Visible = false;
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "You have no Invitations yet", MessageTypes.Info, true, true, false);
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
    }
}