using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using System.Data;
using WdS.ElioPlus.Objects;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Text.RegularExpressions;

namespace WdS.ElioPlus
{
    public partial class DashboardCollaborationCreateNewInvitations : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public int InvitationId
        {
            get
            {
                return (this.ViewState["InvitationId"] != null) ? Convert.ToInt32(ViewState["InvitationId"]) : -1;
            }

            set { this.ViewState["InvitationId"] = value; }
        }

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

                            if (TbxMsg.Value == string.Empty)
                            {
                                divGeneralFailure.Visible = true;
                                LblFailure.Text = "Please add Content";
                                return;
                            }

                            ElioCollaborationUsersInvitations invitation = null;

                            DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);

                            if (InvitationId != -1)
                            {
                                invitation = SqlCollaboration.GetUserInvitationById(Convert.ToInt32(ViewState["InvitationId"]), session);
                                if (invitation != null)
                                {
                                    invitation.InvSubject = TbxSubject.Text;
                                    invitation.InvContent = TbxMsg.Value;
                                    invitation.LastUpdated = DateTime.Now;

                                    loader.Update(invitation);
                                }
                                else
                                {
                                    invitation = new ElioCollaborationUsersInvitations();

                                    invitation.UserId = vSession.User.Id;
                                    invitation.InvSubject = TbxSubject.Text;
                                    invitation.InvContent = TbxMsg.Value;
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
                                invitation.InvContent = TbxMsg.Value;
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

                                    int collaborationId = SqlCollaboration.GetCollaborationId(vSession.User.Id, partner.Id, session);

                                    if (collaborationId < 0)
                                    {
                                        ElioCollaborationVendorsResellers newCollaboration = new ElioCollaborationVendorsResellers();

                                        newCollaboration.MasterUserId = vSession.User.Id;
                                        newCollaboration.PartnerUserId = partner.Id;
                                        newCollaboration.IsActive = 1;
                                        newCollaboration.InvitationStatus = CollaborateInvitationStatus.Pending.ToString();
                                        newCollaboration.Sysdate = DateTime.Now;
                                        newCollaboration.LastUpdated = DateTime.Now;

                                        DataLoader<ElioCollaborationVendorsResellers> loader2 = new DataLoader<ElioCollaborationVendorsResellers>(session);
                                        loader2.Insert(newCollaboration);

                                        ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                                        vendorResselerInvitation.UserId = vSession.User.Id;
                                        vendorResselerInvitation.VendorResellerId = newCollaboration.Id;
                                        vendorResselerInvitation.UserInvitationId = invitation.Id;
                                        vendorResselerInvitation.RecipientEmail = partner.Email;
                                        vendorResselerInvitation.SendDate = DateTime.Now;
                                        vendorResselerInvitation.LastUpdated = DateTime.Now;

                                        DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                        loader3.Insert(vendorResselerInvitation);
                                    }
                                    else
                                    {
                                        ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                                        vendorResselerInvitation.UserId = vSession.User.Id;
                                        vendorResselerInvitation.VendorResellerId = collaborationId;
                                        vendorResselerInvitation.UserInvitationId = invitation.Id;
                                        vendorResselerInvitation.RecipientEmail = partner.Email;
                                        vendorResselerInvitation.SendDate = DateTime.Now;
                                        vendorResselerInvitation.LastUpdated = DateTime.Now;

                                        DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                        loader3.Insert(vendorResselerInvitation);
                                    }

                                    session.CommitTransaction();

                                    try
                                    {
                                        //maybe asynchronously send emails, to do
                                        //string confirmationLink = "https://www.elioplus.com/free-sign-up?verificationViewID=" + partner.GuId + "&type=" + partner.UserApplicationType.ToString();
                                        string partnerPortalSignUpLink = FileHelper.AddToPhysicalRootPath(Request) + ControlLoader.SignUpPartner.Replace("{CompanyName}", Regex.Replace(vSession.User.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower()) + "?verificationViewID=" + partner.GuId + "&type=" + partner.UserApplicationType.ToString();
                                        EmailSenderLib.CollaborationInvitationEmail(partner.UserApplicationType, partner.Email, vSession.User.CompanyName, vSession.User.CompanyLogo, partner.CompanyName, partnerPortalSignUpLink, vSession.Lang, session);
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }

                            divGeneralSuccess.Visible = true;
                            LblGeneralSuccess.Text = "Done! ";
                            LblSuccess.Text = "Your invitation to selected partners was successfully send!";

                            TbxSubject.Text = string.Empty;
                            TbxMsg.Value = string.Empty;

                            RdgMyInvitations.Rebind();

                            rdgResellers.Rebind();
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
                TbxMsg.Value = string.Empty;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseInvitationPopUp();", true);
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
                TbxMsg.Value = string.Empty;

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

                ViewState["InvitationId"] = Convert.ToInt32(item["id"].Text);

                TbxSubject.Text = item["inv_subject"].Text;
                TbxMsg.Value = item["inv_content"].Text;

                ResetFields();
                ImgBtnSave.Visible = true;
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

                ResetFields();

                if (ViewState["InvitationId"] != null)
                {
                    if (TbxSubject.Text == string.Empty)
                    {
                        divGeneralFailure.Visible = true;
                        LblFailure.Text = "Please add Subject";

                        ImgBtnSave.Visible = true;

                        return;
                    }

                    if (TbxMsg.Value == string.Empty)
                    {
                        divGeneralFailure.Visible = true;
                        LblFailure.Text = "Please add Content";

                        ImgBtnSave.Visible = true;

                        return;
                    }

                    ElioCollaborationUsersInvitations invitation = SqlCollaboration.GetUserInvitationById(Convert.ToInt32(ViewState["InvitationId"]), session);
                    if (invitation != null)
                    {
                        invitation.InvSubject = TbxSubject.Text;
                        invitation.InvContent = TbxMsg.Value;
                        invitation.LastUpdated = DateTime.Now;

                        DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                        loader.Update(invitation);

                        RdgMyInvitations.Rebind();

                        ImgBtnSave.Visible = true;
                        ImgStatus.Visible = true;
                        ImgStatus.ImageUrl = "~/images/icons/small/success.png";

                        divGeneralSuccess.Visible = true;
                        LblGeneralSuccess.Text = "Done! ";
                        LblSuccess.Text = "Your invitation was updated successfully!";
                    }
                    else
                    {
                        divGeneralFailure.Visible = true;
                        LblFailure.Text = "Your invitation could not updated. Please try again later!";
                        ImgStatus.ImageUrl = "~/images/icons/small/rejected_1.png";
                    }
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