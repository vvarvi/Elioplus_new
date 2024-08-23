using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Roles;
using WdS.ElioPlus.Lib.Roles.EnumsRoles;

namespace WdS.ElioPlus
{
    public partial class DashboardPDManagePartnersPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);                    
                    scriptManager.RegisterPostBackControl(aBtnExportPdf);
                    scriptManager.RegisterPostBackControl(aBtnExportCsv);
                    scriptManager.RegisterPostBackControl(aBtnExportCriteriaPdf);
                    scriptManager.RegisterPostBackControl(aBtnExportCriteriaCsv);

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, session);

                    if (isError)
                    {
                        Response.Redirect(vSession.Page = errorPage, false);
                        return;
                    }

                    bool hasRight = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardPDManagePartnersPage", Actions.View, session);
                    if (!hasRight)
                    {
                        Response.Redirect(ControlLoader.PageDash405, false);
                        return;
                    }

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

        # region Methods

        private void FixPage()
        {
            if (!IsPostBack)
            {
                divVendorToolsConfirmed.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
                aBtnExportPdf.Visible = aBtnExportCsv.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
                UcSendMessageAlertConfirmed.Visible = UcCreateNewInvitationToConnectionsMessage.Visible = false;

                UpdateStrings();
                SetLinks();
                LoadCountriesConfirmed();

                if (vSession.HasToUpdateNewInvitationRequest)
                    vSession.HasToUpdateNewInvitationRequest = !Sql.SetVendorResellerInvitationAsNotNew(vSession.User.Id, session);
            }

            divSuccess.Visible = false;
            LblSuccessMsg.Text = "";

            divFailure.Visible = false;
            LblFailureMsg.Text = "";

            if (vSession.User.CompanyType == Types.Vendors.ToString())
                RdgSendInvitationsConfirmed.MasterTableView.GetColumn("sub_account_id").Display = SqlRoles.HasSubAccountMembersByUserId(vSession.User.Id, 1, 1, session);

            if (vSession.User.CompanyType != EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                divVendorToolsConfirmed.Visible = true;
            }
            else
            {
                divVendorToolsConfirmed.Visible = false;
            }
        }

        private void SetLinks()
        {
            //aInvitationToPartners.HRef = ControlLoader.Dashboard(vSession.User, "partners-invitations");
            aChannelPartnerInvitationConfirmed.HRef = ControlLoader.Dashboard(vSession.User, "partners-invitations");
        }

        private void UpdateStrings()
        {
            string partners = (vSession.User.CompanyType == Types.Vendors.ToString()) ? EnumHelper.GetDescription(Types.Resellers).ToString() : Types.Vendors.ToString();

            //LblTitle.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Invitations from my " + partners : "Invitations from my " + partners;
            LblTitle2Confirmed.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Invitations to my " + partners : "Invitations to my " + partners;
        }

        private bool CanAddRemovePartnersToManageFromUserPacketStatus(int userToUpdate, bool decreaseCount)
        {
            bool successfullUpdated = false;

            ElioUserPacketStatus userPacketStatus = Sql.GetUserPacketStatusFeatures(userToUpdate, session);
            if (userPacketStatus != null)
            {
                if (decreaseCount)
                {
                    if (userPacketStatus.AvailableManagePartnersCount > 0)
                    {
                        userPacketStatus.AvailableManagePartnersCount--;
                        userPacketStatus.LastUpdate = DateTime.Now;

                        successfullUpdated = true;
                    }
                    else
                    {
                        successfullUpdated = false;
                    }
                }
                else
                {
                    ElioUsers user = Sql.GetUserById(userToUpdate, session);
                    if (user != null)
                    {
                        ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(user.BillingType, session);
                        if (packet != null)
                        {
                            List<ElioPacketsIJFeaturesItems> packetFeatures = Sql.GetPacketFeaturesItems(packet.Id, session);
                            if (packetFeatures[3].FreeItemsNo > userPacketStatus.AvailableManagePartnersCount)
                            {
                                userPacketStatus.AvailableManagePartnersCount++;
                                userPacketStatus.LastUpdate = DateTime.Now;

                                successfullUpdated = true;
                            }
                            else
                                successfullUpdated = true;
                        }
                        else
                            successfullUpdated = false;
                    }
                    else
                        successfullUpdated = false;
                }

                DataLoader<ElioUserPacketStatus> packetStatusLoader = new DataLoader<ElioUserPacketStatus>(session);
                packetStatusLoader.Update(userPacketStatus);
            }
            else
                successfullUpdated = false;

            return successfullUpdated;
        }

        private void FixGridStatus(RadGrid rdg, string actionMode)
        {
            bool hasSelectedItem = false;
            //UcReceiveMessageAlert.Visible = false;

            foreach (GridDataItem item in rdg.Items)
            {
                CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
                if (cbx != null)
                {
                    if (cbx.Checked)
                    {
                        hasSelectedItem = true;

                        int vendorResellerId = Convert.ToInt32(item["id"].Text);

                        ElioCollaborationVendorsResellers vendorReseller = SqlCollaboration.GetCollaborationVendorResellerById(vendorResellerId, session);

                        DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

                        bool canAddOrRemovePartnersCount = false;

                        if (vendorReseller != null)
                        {
                            int userToUpdate = (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString()) ? vendorReseller.MasterUserId : vendorReseller.PartnerUserId;

                            switch (actionMode)
                            {
                                case "Confirmed":

                                    if (vendorReseller.InvitationStatus == CollaborateInvitationStatus.Pending.ToString())
                                    {
                                        #region  Update User PacketStatus Available Manage Partners Count

                                        canAddOrRemovePartnersCount = CanAddRemovePartnersToManageFromUserPacketStatus(userToUpdate, true);

                                        #endregion
                                    }
                                    else if (vendorReseller.InvitationStatus == CollaborateInvitationStatus.Confirmed.ToString())
                                        canAddOrRemovePartnersCount = true;

                                    if (canAddOrRemovePartnersCount)
                                    {
                                        vendorReseller.InvitationStatus = CollaborateInvitationStatus.Confirmed.ToString();
                                        vendorReseller.LastUpdated = DateTime.Now;
                                        loader.Update(vendorReseller);

                                        try
                                        {
                                            EmailSenderLib.SendEmailToCollaboratePartnerForAcceptInvitation(item["email"].Text, vSession.User.CompanyName, vSession.User.CompanyType, Request, vSession.LoggedInSubAccountRoleID > 0, vSession.Lang, session);
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }
                                    }
                                    else
                                    {
                                        //GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "You can not confirm this invitation because your partner can not manage more partners.Contact with him or with Elioplus team.", MessageTypes.Error, true, true, false);
                                        UpdatePanelContent.Update();
                                        ShowPopUpModalWithText("Warning", "You can not confirm this invitation because your partner can not manage more partners.");
                                        return;
                                    }

                                    break;

                                case "Pending":

                                    if (vendorReseller.InvitationStatus == CollaborateInvitationStatus.Confirmed.ToString())
                                    {
                                        #region  Update User PacketStatus Available Manage Partners Count

                                        canAddOrRemovePartnersCount = CanAddRemovePartnersToManageFromUserPacketStatus(userToUpdate, false);

                                        #endregion
                                    }
                                    else if (vendorReseller.InvitationStatus == CollaborateInvitationStatus.Pending.ToString())
                                        canAddOrRemovePartnersCount = true;

                                    if (canAddOrRemovePartnersCount)
                                    {
                                        vendorReseller.InvitationStatus = CollaborateInvitationStatus.Pending.ToString();
                                        vendorReseller.LastUpdated = DateTime.Now;
                                        loader.Update(vendorReseller);
                                    }
                                    else
                                    {
                                        //GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "Something went wrong. Please contact with Elioplus team.", MessageTypes.Error, true, true, false);
                                        UpdatePanelContent.Update();
                                        ShowPopUpModalWithText("Warning", "Something went wrong. Please contact with Elioplus team.");

                                        Logger.DetailedError(string.Format("User {0} tried to do some action at {1} with his invitation and something went wrong.", vSession.User.Id.ToString(), DateTime.Now.ToString()));

                                        return;
                                    }

                                    break;

                                case "Rejected":

                                    if (vendorReseller.InvitationStatus == CollaborateInvitationStatus.Confirmed.ToString())
                                    {
                                        #region  Update User PacketStatus Available Manage Partners Count

                                        canAddOrRemovePartnersCount = CanAddRemovePartnersToManageFromUserPacketStatus(userToUpdate, false);

                                        #endregion
                                    }
                                    else if (vendorReseller.InvitationStatus == CollaborateInvitationStatus.Rejected.ToString())
                                        canAddOrRemovePartnersCount = true;

                                    if (canAddOrRemovePartnersCount)
                                    {
                                        vendorReseller.InvitationStatus = CollaborateInvitationStatus.Rejected.ToString();
                                        vendorReseller.LastUpdated = DateTime.Now;
                                        loader.Update(vendorReseller);
                                    }
                                    else
                                    {
                                        //GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "Something went wrong. Please contact with Elioplus team.", MessageTypes.Error, true, true, false);
                                        UpdatePanelContent.Update();
                                        ShowPopUpModalWithText("Warning", "Something went wrong. Please contact with Elioplus team.");

                                        Logger.DetailedError(string.Format("User {0} tried to do some action at {1} with his invitation and something went wrong.", vSession.User.Id.ToString(), DateTime.Now.ToString()));
                                        return;
                                    }

                                    try
                                    {
                                        EmailSenderLib.SendEmailToCollaboratePartnerForRejectInvitation(item["email"].Text, vSession.User.CompanyName, vSession.User.CompanyType, vSession.LoggedInSubAccountRoleID > 0, vSession.Lang, session);
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    }

                                    break;

                                case "Deleted":

                                    if (vendorReseller.InvitationStatus == CollaborateInvitationStatus.Confirmed.ToString())
                                    {
                                        #region  Update User PacketStatus Available Manage Partners Count

                                        canAddOrRemovePartnersCount = CanAddRemovePartnersToManageFromUserPacketStatus(userToUpdate, false);

                                        #endregion
                                    }
                                    else
                                        canAddOrRemovePartnersCount = true;

                                    if (canAddOrRemovePartnersCount)
                                        SqlCollaboration.DeleteCollaborationById(vendorResellerId, session);
                                    else
                                    {
                                        //GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "Something went wrong. Please contact with Elioplus team.", MessageTypes.Error, true, true, false);
                                        UpdatePanelContent.Update();
                                        ShowPopUpModalWithText("Warning", "Something went wrong. Please contact with Elioplus team.");

                                        Logger.DetailedError(string.Format("User {0} tried to do some action at {1} with his invitation and something went wrong.", vSession.User.Id.ToString(), DateTime.Now.ToString()));
                                        return;
                                    }

                                    break;
                            }

                            if (actionMode != "Deleted")
                            {
                                List<ElioCollaborationVendorResellerInvitations> venResInvitations = SqlCollaboration.GetCollaborationVendorResellerInvitationsByVenResId(vendorResellerId, session);
                                foreach (ElioCollaborationVendorResellerInvitations invitation in venResInvitations)
                                {
                                    invitation.InvitationStepDescription = actionMode;
                                    invitation.LastUpdated = DateTime.Now;

                                    DataLoader<ElioCollaborationVendorResellerInvitations> invLoader = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                    invLoader.Update(invitation);
                                }
                            }
                        }
                        else
                        {
                            //error
                        }

                        //int simpleMsgCount = 0;
                        //int groupMsgCount = 0;
                        //int totalNewMsgCount = 0;
                        //this.Master.ShowMasterNotifications(out simpleMsgCount, out groupMsgCount, out totalNewMsgCount);

                        UpdatePanel updatePanelContent = (UpdatePanel)ControlFinder.FindControlRecursive(this.Master, "UpdatePanelContent");
                        if (updatePanelContent != null)
                            updatePanelContent.Update();

                    }
                }
            }

            if (hasSelectedItem)
            {
                rdg.Rebind();
                //GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "Successfully updated", MessageTypes.Success, true, true, false);

                UpdatePanelContent.Update();
                ShowPopUpModalWithText("Confirmation", "Successfully updated");
            }
            else
                ShowPopUpModalWithText("Warning", "Please select at least one");
            //GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "Please select at least one", MessageTypes.Warning, true, true, false);
        }

        private void DeleteInvitationFromGrid(int vendorResellerId, RadGrid rdg)
        {
            SqlCollaboration.DeleteCollaborationById(vendorResellerId, session);

            rdg.Rebind();

            ShowPopUpModalWithText("Successfull Delete", "You deleted this partner from your list successfully.");
        }

        private void ShowPopUpModalWithText(string title, string content)
        {
            LblInvitationSendTitle.Text = title;
            LblSuccessfullSendfMsg.Text = content;

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenSendInvitationPopUp();", true);
        }

        private void LoadCountriesConfirmed()
        {
            DdlCountriesConfirmed.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Select Country";

            DdlCountriesConfirmed.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new ListItem();
                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                DdlCountriesConfirmed.Items.Add(item);
            }
        }

        private void LoadInvitationStatus()
        {
            //DdlInvitationStatus.Items.Clear();

            //DropDownListItem item = new DropDownListItem();
            //item.Value = "-2";
            //item.Text = "Select Status";

            //DdlInvitationStatus.Items.Add(item);

            //item = new DropDownListItem();
            //item.Value = Convert.ToInt32(CollaborateInvitationStatus.Confirmed).ToString();
            //item.Text = CollaborateInvitationStatus.Confirmed.ToString();

            //DdlInvitationStatus.Items.Add(item);

            //item = new DropDownListItem();
            //item.Value = Convert.ToInt32(CollaborateInvitationStatus.Pending).ToString();
            //item.Text = CollaborateInvitationStatus.Pending.ToString();

            //DdlInvitationStatus.Items.Add(item);

            //item = new DropDownListItem();
            //item.Value = Convert.ToInt32(CollaborateInvitationStatus.Rejected).ToString();
            //item.Text = CollaborateInvitationStatus.Rejected.ToString();

            //DdlInvitationStatus.Items.Add(item);

            //item = new DropDownListItem();
            //item.Value = Convert.ToInt32(CollaborateInvitationStatus.Deleted).ToString();
            //item.Text = CollaborateInvitationStatus.Deleted.ToString();

            //DdlInvitationStatus.Items.Add(item);
        }

        private void SetDealLinkUrl(GridDataItem item, int partnerId)
        {
            string url = "";

            string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, partnerId, session);
            Session[sessionId] = partnerId.ToString();

            url = ControlLoader.Dashboard(vSession.User, "management-tier") + "?partnerViewID=" + sessionId;

            HtmlAnchor aMoreDetails = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMoreDetails");
            aMoreDetails.HRef = ControlLoader.Dashboard(vSession.User, "management-tier") + "?partnerViewID=" + sessionId;
        }

        private void LoadAssignedPartner(GridDataItem item)
        {
            int subAccountId = Sql.GetCollaborationMemberSubAccountIdByVendorResellerId(Convert.ToInt32(item["id"].Text), session);
            if (subAccountId > 0)
            {
                DataTable assignedPartnerTbl = session.GetDataTable(@"select pur.id as role_id,
                                                                        case
                                                                            when
                                                                                usa.account_status = 1
                                                                            then
                                                                                usa.last_name + ' ' + usa.first_name + ' - (' + pur.role_name + ')'
                                                                            else
                                                                                usa.email + ' - (' + pur.role_name + ')'
                                                                            end
                                                                        as sub_fullname
                                                                        ,pur.role_name
                                                                        ,usa.last_name,usa.first_name
                                                                        ,usa.id as sub_account_id,usa.email
                                                                        ,usa.position
                                                                        ,u.company_logo
                                                                        from Elio_users_sub_accounts usa
                                                                        inner join Elio_permissions_users_roles pur on pur.id = usa.team_role_id and pur.user_id = usa.user_id
                                                                        inner join Elio_users u on u.id = usa.user_id
                                                                        where usa.user_id = @user_id
                                                                        and usa.is_active = 1
                                                                        and is_confirmed = 1
                                                                        and usa.id = @sub_account_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id)
                                                        , DatabaseHelper.CreateIntParameter("@sub_account_id", subAccountId));

                Label lblAssignedPartnerFullName = (Label)ControlFinder.FindControlRecursive(item, "LblAssignedPartnerFullName");

                if (assignedPartnerTbl.Rows.Count > 0)
                {                    
                    HtmlGenericControl divPic1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divPic1");
                    HtmlImage pic1 = (HtmlImage)ControlFinder.FindControlRecursive(item, "Pic1");
                    
                    divPic1.Visible = true;
                    pic1.Src = assignedPartnerTbl.Rows[0]["company_logo"].ToString();
                    lblAssignedPartnerFullName.Text = assignedPartnerTbl.Rows[0]["sub_fullname"].ToString();
                }
                else
                {
                    lblAssignedPartnerFullName.Text = "Not Assigned";
                }
            }
        }

        private void LoadTeamMembersTable(GridDataItem item)
        {
            HdnPartId.Value = item["partner_user_id"].Text;
            HdnVendResId.Value = item["id"].Text;

            DrpTeamMembers.Items.Clear();

            DataTable membersTbl = SqlRoles.GetUserSubAccountsMembersFullNameAndRoleView(vSession.User.Id, 0, 1, 1, session);

            if (membersTbl.Rows.Count > 0)
            {
                DrpTeamMembers.Enabled = true;
                aSaveAssignToMember.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardPDManagePartnersPage", Actions.Assign, session);

                DataRow row = membersTbl.NewRow();
                row["sub_account_id"] = "0";
                row["sub_fullname"] = "Select member to assign partner";
                membersTbl.Rows.Add(row);

                EnumerableRowCollection<DataRow> members = from member in membersTbl.AsEnumerable()
                                                           orderby member.Field<int>("sub_account_id")
                                                           select member;

                DataView dv = members.AsDataView();

                DrpTeamMembers.DataSource = dv;
                DrpTeamMembers.DataValueField = "sub_account_id";
                DrpTeamMembers.DataTextField = "sub_fullname";

                DrpTeamMembers.DataBind();

                int subAccountId = Sql.GetCollaborationMemberSubAccountIdByVendorResellerId(Convert.ToInt32(item["id"].Text), session);
                if (subAccountId > 0)
                {
                    DrpTeamMembers.Items.FindByValue(subAccountId.ToString()).Selected = true;
                    HdnSubAcId.Value = subAccountId.ToString();
                }
            }
            else
            {
                DataRow row = membersTbl.NewRow();
                row["sub_account_id"] = "0";
                row["sub_fullname"] = "You have no active member to assign partner";
                membersTbl.Rows.Add(row);

                DrpTeamMembers.DataSource = membersTbl;
                DrpTeamMembers.DataValueField = "sub_account_id";
                DrpTeamMembers.DataTextField = "sub_fullname";

                DrpTeamMembers.DataBind();

                DrpTeamMembers.Enabled = false;
                aSaveAssignToMember.Visible = false;
            }
        }

        private void ShowPopUpModalAlert(string title, string content, MessageTypes type)
        {
            LblMessageTitle.Text = title;
            GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, content, type, true, true, true, false, false);

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAssignPartnerPopUp();", true);
        }

        private void AssignPartnerToMember(int collaborationId, int partnerId, int subAccountId)
        {
            DataLoader<ElioCollaborationVendorsMembersResellers> loader = new DataLoader<ElioCollaborationVendorsMembersResellers>(session);

            ElioCollaborationVendorsMembersResellers assignee = Sql.GetCollaborationMemberByVendorResellerId(collaborationId, session);

            if (assignee == null)
            {
                assignee = new ElioCollaborationVendorsMembersResellers();

                assignee.AssignByUserId = vSession.User.Id;
                assignee.SubAccountId = subAccountId;
                assignee.PartnerUserId = partnerId;
                assignee.VendorResellerId = collaborationId;
                assignee.Sysdate = DateTime.Now;
                assignee.LastUpdate = DateTime.Now;

                loader.Insert(assignee);

                GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, "Partner assigned to member successfully!", MessageTypes.Success, true, true, true, false, false);
            }
            else
            {
                if (subAccountId > 0)
                {
                    assignee.AssignByUserId = vSession.User.Id;
                    assignee.SubAccountId = subAccountId;
                    assignee.PartnerUserId = partnerId;
                    assignee.VendorResellerId = collaborationId;
                    assignee.LastUpdate = DateTime.Now;

                    loader.Update(assignee);

                    GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, "Partner re-assigned to member successfully!", MessageTypes.Success, true, true, true, false, false);
                }
                else
                {
                    loader.Delete(assignee);

                    GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, "Partner un-assigned from member successfully!", MessageTypes.Success, true, true, true, false, false);
                }
            }

            RdgSendInvitationsConfirmed.Rebind();
            UpdatePanelContent.Update();
        }

        # endregion

        #region Grids

        protected void RdgSendInvitationsConfirmed_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent

                    GridDataItem item = (GridDataItem)e.Item;

                    int partnerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(item["partner_user_id"].Text) : Convert.ToInt32(item["master_user_id"].Text);
                    ElioUsers company = Sql.GetUserById(partnerId, session);
                    if (company != null)
                    {
                        Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                        Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
                        Label lblTierStatus = (Label)ControlFinder.FindControlRecursive(item, "LblTierStatus");
                        Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                        HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDeleteConfirmed");
                        HtmlAnchor aCollaborationRoom = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCollaborationRoomConfirmed");
                        HtmlAnchor aEditTierStatus = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEditTierStatus");
                        //RadComboBox rcbxTierStatus = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxTierStatus");

                        lblCompanyName.Text = item["company_name"].Text;

                        aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                        aDelete.Title = "Delete specific Invitation from your list";
                        aDelete.HRef = "#divConfirm";

                        lblTierStatus.Text = (!string.IsNullOrEmpty(item["tier_status"].Text) && item["tier_status"].Text != "&nbsp;") ? item["tier_status"].Text : "Select";
                        //aEditTierStatus.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && Sql.HasTierManagementUserSettings(vSession.User.Id, session);

                        if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers) && lblTierStatus.Text == "Select")
                        {
                            lblTierStatus.Text = "Not Selected";
                        }

                        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                        scriptManager.RegisterPostBackControl(aCollaborationRoom);

                        //aCollaborationRoom.HRef = ControlLoader.Dashboard(company, "collaboration-chat-room");

                        HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                        divNotification.Visible = Convert.ToInt32(item["is_new"].Text) == 1 && item["invitation_status"].Text == CollaborateInvitationStatus.Confirmed.ToString();
                        if (Convert.ToInt32(item["is_new"].Text) == 1 && item["invitation_status"].Text == CollaborateInvitationStatus.Confirmed.ToString())
                        {
                            vSession.HasToUpdateNewInvitationRequest = true;
                        }

                        HtmlAnchor aMoreDetails = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMoreDetails");

                        if (item["invitation_status"].Text == CollaborateInvitationStatus.Confirmed.ToString())
                        {
                            lblStatus.ToolTip = "The specific Invitation has been confirmed and is available for you to collaborare with.";
                            lblStatus.Text = "Active";  // CollaborateInvitationStatus.Confirmed.ToString();
                            lblStatus.CssClass = "label label-lg label-light-success label-inline";

                            aCollaborationRoom.Visible = true;
                        }

                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");

                        if (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus))
                        {
                            aCompanyLogo.HRef = ControlLoader.Profile(company);

                            imgCompanyLogo.ToolTip = "View company's profile";
                            imgCompanyLogo.ImageUrl = company.CompanyLogo;
                            imgCompanyLogo.AlternateText = "Company logo";
                        }
                        else if (company.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty))
                        {
                            aCompanyLogo.HRef = company.WebSite;

                            aCompanyLogo.Target = "_blank";
                            imgCompanyLogo.ToolTip = "View company's site";
                            imgCompanyLogo.ImageUrl = "/images/icons/partners_th_party_2.png";
                            imgCompanyLogo.AlternateText = "Third party partners logo";
                        }
                        else if (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Collaboration))
                        {
                            if (company.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                            {
                                aCompanyLogo.HRef = company.WebSite;

                                aCompanyLogo.Target = "_blank";
                                imgCompanyLogo.ToolTip = "View company's site";
                            }

                            imgCompanyLogo.ImageUrl = company.CompanyLogo;
                            imgCompanyLogo.AlternateText = "Potential collaboration company's logo";
                        }

                        aMoreDetails.Visible = true;

                        SetDealLinkUrl(item, company.Id);

                        Label lblMoreDetails = (Label)ControlFinder.FindControlRecursive(item, "LblMoreDetails");
                        lblMoreDetails.Text = "Manage partner";

                        HtmlAnchor aAssignPartner = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aAssignPartner");

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            bool hasTeamMembers = SqlRoles.HasSubAccountMembersByUserId(vSession.User.Id, 1, 1, session);
                            if (hasTeamMembers)
                            {
                                LoadAssignedPartner(item);                //LoadTeamMembersTable(item);
                                aAssignPartner.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardPDManagePartnersPage", Actions.Assign, session);
                            }
                        }
                        else
                            aAssignPartner.Visible = false;
                    }

                    #endregion
                }
                else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "VendorsResellersUsersInvitations")
                {
                    #region Vendors-Resellers Invitations

                    GridDataItem item = (GridDataItem)e.Item;

                    int invitationId = Convert.ToInt32(item["id"].Text);

                    if (item["vendor_reseller_id"].Text != "&nbsp;" && item["vendor_reseller_id"].Text != "")
                    {
                        int vendorResellerId = Convert.ToInt32(item["vendor_reseller_id"].Text);

                        Image imgStepDescription = (Image)ControlFinder.FindControlRecursive(item, "ImgStepDescription");

                        ElioCollaborationVendorResellerInvitations venResInvitation = SqlCollaboration.GetCollaborationVendorResellerInvitationByVenResIdAndInvId(vendorResellerId, invitationId, session);
                        if (venResInvitation != null)
                        {
                            if (venResInvitation.InvitationStepDescription == CollaborateInvitationStepDescription.Rejected.ToString())
                            {
                                imgStepDescription.ImageUrl = "~/images/icons/small/rejected_5.png";
                                imgStepDescription.ToolTip = "You have reject this Invitation. You can change it's status if you want.";
                            }
                            else if (venResInvitation.InvitationStepDescription == CollaborateInvitationStepDescription.Pending.ToString())
                            {
                                imgStepDescription.ImageUrl = "~/images/icons/small/rejected_1.png";
                                imgStepDescription.ToolTip = "You have not accept this Invitation yet. You can change it's status if you want.";
                            }
                            else if (venResInvitation.InvitationStepDescription == CollaborateInvitationStepDescription.Confirmed.ToString())
                            {
                                imgStepDescription.ImageUrl = "~/images/icons/small/confirmed_1.png";
                                imgStepDescription.ToolTip = "You have accepted this Vendor's Invitation and is available for you to collaborare with.";
                            }
                        }

                        TextBox tbxSubject = (TextBox)ControlFinder.FindControlRecursive(item, "TbxSubject");
                        tbxSubject.Text = item["inv_subject"].Text;

                        TextBox tbxContent = (TextBox)ControlFinder.FindControlRecursive(item, "TbxContent");
                        tbxContent.Text = item["inv_content"].Text;

                    }

                    #endregion
                }

                if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                {
                    if (e.Item is GridHeaderItem)
                    {
                        GridHeaderItem headerItem = (GridHeaderItem)e.Item;
                        if (headerItem != null)
                        {
                            Label lblHdrTierStatusText = (Label)headerItem.FindControl("LblHdrTierStatusText");
                            if (lblHdrTierStatusText != null)
                            {
                                lblHdrTierStatusText.Text = "Your Tier Status";
                            }
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

        protected void RdgSendInvitationsConfirmed_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    bool hasCriteria = RtbxCompanyNameEmailConfirmed.Text.Trim() != "" || DdlCountriesConfirmed.SelectedItem.Value != "0";

                    List<ElioCollaborationVendorsResellersIJUsers> partners = new List<ElioCollaborationVendorsResellersIJUsers>();

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        partners = SqlCollaboration.GetVendorCollaborationInvitationsToFromChannelPartners(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, RtbxCompanyNameEmailConfirmed.Text.Trim(), CollaborateInvitationStatus.Confirmed.ToString(), (DdlCountriesConfirmed.SelectedItem.Value != "0") ? DdlCountriesConfirmed.SelectedItem.Text : "", session);
                    }
                    else
                    {
                        partners = SqlCollaboration.GetChannelPartnerCollaborationInvitationsToFromVendors(vSession.User.Id, RtbxCompanyNameEmailConfirmed.Text.Trim(), CollaborateInvitationStatus.Confirmed.ToString(), (DdlCountriesConfirmed.SelectedItem.Value != "0") ? DdlCountriesConfirmed.SelectedItem.Text : "", session);
                    }

                    if (partners.Count > 0)
                    {
                        RdgSendInvitationsConfirmed.Visible = true;
                        UcSendMessageAlertConfirmed.Visible = false;
                        //divSearchArea.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("master_user_id");
                        table.Columns.Add("invitation_status");
                        table.Columns.Add("tier_status");
                        table.Columns.Add("score");
                        table.Columns.Add("partner_user_id");
                        table.Columns.Add("company_name");
                        table.Columns.Add("sysdate");
                        table.Columns.Add("email");
                        table.Columns.Add("country");
                        table.Columns.Add("is_new");

                        foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                        {
                            table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.TierStatus, partner.Score, partner.PartnerUserId, partner.CompanyName, partner.Sysdate.ToShortDateString(), partner.Email, partner.Country, partner.IsNew);
                        }

                        RdgSendInvitationsConfirmed.DataSource = table;

                        divExportAreaButton.Visible = true;
                    }
                    else
                    {
                        RdgSendInvitationsConfirmed.Visible = false;
                        //LblTitle2.Visible = false;
                        divExportAreaButton.Visible = false;

                        GlobalMethods.ShowMessageControlDA(UcSendMessageAlertConfirmed, (hasCriteria) ? "There are no Confirmed Invitations send to your Partners with these criteria" : "There are no Confirmed Invitations send to your Partners", MessageTypes.Info, true, true, false);
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

        protected void aAssignPartner_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor aBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aBtn.NamingContainer;

                UcAssignmentMessageAlert.Visible = false;
                HdnVendResId.Value = "0";
                HdnSubAcId.Value = "0";
                HdnPartId.Value = "0";

                LoadTeamMembersTable(item);

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAssignPartnerPopUp();", true);
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

        protected void aSaveAssignToMember_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcAssignmentMessageAlert.Visible = false;

                if (HdnPartId.Value != "0" && HdnVendResId.Value != "0")
                {
                    if (DrpTeamMembers.SelectedValue == "0")
                    {
                        bool isAssigned = Sql.IsPartnerAssigned(vSession.User.Id, Convert.ToInt32(HdnPartId.Value), 0, null, session);
                        if (isAssigned)
                        {
                            //Un-Assign Partner
                            AssignPartnerToMember(Convert.ToInt32(HdnVendResId.Value), Convert.ToInt32(HdnPartId.Value), 0);
                            return;
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, "Please select member to assign this partner!", MessageTypes.Error, true, true, true, false, false);
                            return;
                        }
                    }
                    else
                    {
                        bool isAssignedToOther = Sql.IsPartnerAssigned(vSession.User.Id, Convert.ToInt32(HdnPartId.Value), Convert.ToInt32(DrpTeamMembers.SelectedValue), false, session);
                        if (isAssignedToOther)
                        {
                            //Re Assign Partner
                            AssignPartnerToMember(Convert.ToInt32(HdnVendResId.Value), Convert.ToInt32(HdnPartId.Value), Convert.ToInt32(DrpTeamMembers.SelectedValue));
                            return;
                        }

                        bool isAssignedToMember = Sql.IsPartnerAssigned(vSession.User.Id, Convert.ToInt32(HdnPartId.Value), Convert.ToInt32(DrpTeamMembers.SelectedValue), true, session);
                        if (isAssignedToMember)
                        {
                            GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, "This partner is already assigned to this member!", MessageTypes.Info, true, true, true, false, false);
                            return;
                        }
                        else
                        {
                            //Assign Partner
                            AssignPartnerToMember(Convert.ToInt32(HdnVendResId.Value), Convert.ToInt32(HdnPartId.Value), Convert.ToInt32(DrpTeamMembers.SelectedValue));
                        }
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, "Partner could not be assigned! Please try again later or contact us!", MessageTypes.Error, true, true, true, false, false);
                    return;
                }
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, "Partner could not be assigned! Please try again later or contact us!", MessageTypes.Error, true, true, true, false, false);
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void aBtnExportPdf_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    string url = "download-csv?case=PartnersReportData&type=pdf";

                    if (DdlCountriesConfirmed.SelectedItem.Value != "0")
                    {
                        url += "&country=" + DdlCountriesConfirmed.SelectedItem.Text.Replace(" ","-").ToLower();
                    }

                    if (RtbxCompanyNameEmailConfirmed.Text != "")
                    {
                        url += "&partnername=" + RtbxCompanyNameEmailConfirmed.Text.Replace(" ", "_").Replace("&", "%").ToLower();
                    }

                    Response.Redirect(url, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aBtnExportCriteriaPdf_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    string url = "download-csv?case=PartnersReportData&type=pdf&mode=a";

                    if (DdlCountriesConfirmed.SelectedItem.Value != "0")
                    {
                        url += "&country=" + DdlCountriesConfirmed.SelectedItem.Text.Replace(" ", "-").ToLower();
                    }

                    if (RtbxCompanyNameEmailConfirmed.Text != "")
                    {
                        url += "&partnername=" + RtbxCompanyNameEmailConfirmed.Text.Replace(" ", "_").Replace("&", "%").ToLower();
                    }

                    Response.Redirect(url, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aBtnExportCsv_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    string url = "download-csv?case=PartnersReportData&type=csv";

                    if (DdlCountriesConfirmed.SelectedItem.Value != "0")
                    {
                        url += "&country=" + DdlCountriesConfirmed.SelectedItem.Text.Replace(" ", "-").ToLower();
                    }

                    if (RtbxCompanyNameEmailConfirmed.Text != "")
                    {
                        url += "&partnername=" + RtbxCompanyNameEmailConfirmed.Text.Replace(" ", "_").Replace("&", "%").ToLower();
                    }

                    Response.Redirect(url, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aBtnExportCriteriaCsv_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    string url = "download-csv?case=PartnersReportData&type=csv&mode=a";

                    if (DdlCountriesConfirmed.SelectedItem.Value != "0")
                    {
                        url += "&country=" + DdlCountriesConfirmed.SelectedItem.Text.Replace(" ", "-").ToLower();
                    }

                    if (RtbxCompanyNameEmailConfirmed.Text != "")
                    {
                        url += "&partnername=" + RtbxCompanyNameEmailConfirmed.Text.Replace(" ", "_").Replace("&", "%").ToLower();
                    }

                    Response.Redirect(url, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aEditTierStatus_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        HtmlAnchor imgBtn = (HtmlAnchor)sender;
                        GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                        if (item != null)
                        {
                            HtmlGenericControl divEdit = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divEdit");
                            HtmlGenericControl divSave = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divSave");
                            Label lblTierStatus = (Label)ControlFinder.FindControlRecursive(item, "LblTierStatus");
                            DropDownList rcbxTierStatus = (DropDownList)ControlFinder.FindControlRecursive(item, "RcbxTierStatus");

                            rcbxTierStatus.Items.Clear();

                            //DataTable tblTierStatus = Sql.GetCollaborationUserTierStatusTable(vSession.User.Id, session);
                            //List<ElioTierManagementUsersSettings> tierSettings = Sql.GetTierManagementUserSettings(vSession.User.Id, session);       //SqlCollaboration.GetCollaborationUserTierStatus(vSession.User.Id, session);

                            if (vSession.User.Id != 41078)
                            {
                                List<ElioTierManagementUsersSettings> tierSettings = Sql.GetTierManagementUserSettings(vSession.User.Id, session);       //SqlCollaboration.GetCollaborationUserTierStatus(vSession.User.Id, session);

                                if (tierSettings.Count > 0)
                                {
                                    ListItem rcbxItem = new ListItem();

                                    rcbxItem.Value = "0";
                                    rcbxItem.Text = "Select";
                                    rcbxTierStatus.Items.Add(rcbxItem);

                                    foreach (ElioTierManagementUsersSettings tier in tierSettings)
                                    {
                                        rcbxItem = new ListItem();

                                        rcbxItem.Value = tier.Id.ToString();
                                        rcbxItem.Text = tier.Description;
                                        rcbxTierStatus.Items.Add(rcbxItem);
                                    }

                                    rcbxTierStatus.DataBind();
                                }
                            }
                            else
                            {
                                List<ElioTierManagementCustom> tierSettingsCustom = Sql.GetTierManagementCustomSettings(vSession.User.Id, session);       //SqlCollaboration.GetCollaborationUserTierStatus(vSession.User.Id, session);

                                if (tierSettingsCustom.Count > 0)
                                {
                                    ListItem rcbxItem = new ListItem();

                                    rcbxItem.Value = "0";
                                    rcbxItem.Text = "Select";
                                    rcbxTierStatus.Items.Add(rcbxItem);

                                    foreach (ElioTierManagementCustom tier in tierSettingsCustom)
                                    {
                                        rcbxItem = new ListItem();

                                        rcbxItem.Value = tier.Id.ToString();
                                        rcbxItem.Text = tier.Description;
                                        rcbxTierStatus.Items.Add(rcbxItem);
                                    }

                                    rcbxTierStatus.DataBind();
                                }
                            }

                            //else
                            //{
                            ////rcbxTierStatus.DataSource = SqlCollaboration.GetCollaborationDefaultTierStatusTable(session);
                            ////rcbxTierStatus.DataTextField = "status_description";
                            ////rcbxTierStatus.DataValueField = "id";

                            //List<ElioCollaborationTierDefaultStatus> defaultTierStatus = SqlCollaboration.GetCollaborationDefaultTierStatus(session);

                            //if (defaultTierStatus.Count > 0)
                            //{
                            //    RadComboBoxItem rcbxItem = new RadComboBoxItem();

                            //    rcbxItem.Value = "0";
                            //    rcbxItem.Text = "Select";
                            //    rcbxTierStatus.Items.Add(rcbxItem);

                            //    foreach (ElioCollaborationTierDefaultStatus status in defaultTierStatus)
                            //    {
                            //        rcbxItem = new RadComboBoxItem();

                            //        rcbxItem.Value = status.Id.ToString();
                            //        rcbxItem.Text = status.StatusDescription;
                            //        rcbxTierStatus.Items.Add(rcbxItem);
                            //    }

                            //    rcbxTierStatus.DataBind();
                            //}
                            //}

                            string selectedTierStatus = item["tier_status"].Text;
                            if (!string.IsNullOrEmpty(selectedTierStatus) && selectedTierStatus != "&nbsp;")
                            {
                                rcbxTierStatus.SelectedItem.Text = selectedTierStatus;
                                //rcbxTierStatus.FindItemByText(selectedTierStatus).Selected = true;
                            }

                            divEdit.Visible = false;
                            divSave.Visible = true;
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

        protected void aSaveTierStatus_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                    if (item != null)
                    {
                        HtmlGenericControl divEdit = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divEdit");
                        HtmlGenericControl divSave = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divSave");
                        Label lblTierStatus = (Label)ControlFinder.FindControlRecursive(item, "LblTierStatus");
                        DropDownList rcbxTierStatus = (DropDownList)ControlFinder.FindControlRecursive(item, "RcbxTierStatus");

                        string selectedTierStatus = (rcbxTierStatus.SelectedItem.Value != "0") ? rcbxTierStatus.SelectedItem.Text : "";
                        int collaborationVendorResellerId = Convert.ToInt32(item["id"].Text);

                        bool success = SqlCollaboration.UpdateCollaborationPartnerTierStatus(collaborationVendorResellerId, vSession.User.Id, selectedTierStatus, session);

                        if (success)
                        {
                            //success updated
                            RdgSendInvitationsConfirmed.Rebind();
                        }
                        else
                        {
                            //problem
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlertConfirmed, "Sorry, something went wrong and tier status could not be saved", MessageTypes.Error, true, true, false);
                            return;
                        }

                        divEdit.Visible = true;
                        divSave.Visible = false;
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

        protected void aCancelTierStatus_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                    if (item != null)
                    {
                        HtmlGenericControl divEdit = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divEdit");
                        HtmlGenericControl divSave = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divSave");
                        Label lblTierStatus = (Label)ControlFinder.FindControlRecursive(item, "LblTierStatus");
                        DropDownList rcbxTierStatus = (DropDownList)ControlFinder.FindControlRecursive(item, "RcbxTierStatus");

                        rcbxTierStatus.Items.Clear();

                        lblTierStatus.Text = (!string.IsNullOrEmpty(item["tier_status"].Text) && item["tier_status"].Text != "&nbsp;") ? item["tier_status"].Text : "Select";

                        divEdit.Visible = true;
                        divSave.Visible = false;
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

        protected void ImgBtnCollaborationRoom_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                vSession.VendorsResellersList.Clear();

                ElioCollaborationVendorsResellers vendRes = SqlCollaboration.GetCollaborationVendorResellerById(Convert.ToInt32(item["id"].Text), session);
                if (vendRes != null)
                    GlobalDBMethods.AddRemoveChatReceiversMessages(Mode.SimpleMode, Mode.Any, vSession.VendorsResellersList, vendRes);

                Response.Redirect(ControlLoader.Dashboard(vSession.User, "collaboration-chat-room"), false);
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
                session.OpenConnection();

                if (vSession.User != null)
                {
                    divSuccess.Visible = divFailure.Visible = false;

                    if (HdnVendorResellerCollaborationId.Value != "0")
                    {
                        try
                        {
                            session.BeginTransaction();

                            SqlCollaboration.DeleteCollaborationById(Convert.ToInt32(HdnVendorResellerCollaborationId.Value), session);

                            HdnVendorResellerCollaborationId.Value = "0";

                            if (HdnPartnerUserId.Value != "0")
                            {
                                session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_library_files  
                                                        WHERE user_id = @user_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", Convert.ToInt32(HdnPartnerUserId.Value)));                                
                            }

                            divSuccess.Visible = true;
                            LblSuccessMsg.Text = "You deleted your partner successfully";

                            session.CommitTransaction();

                            RdgSendInvitationsConfirmed.Rebind();

                            UpdatePanelContent.Update();

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfPopUp();", true);

                            if (vSession.User.CompanyType == Types.Vendors.ToString())
                            {
                                try
                                {
                                    string resellerEmail = Sql.GetEmailByUserId(Convert.ToInt32(HdnPartnerUserId.Value), session);
                                    
                                    if (resellerEmail != "")
                                        EmailSenderLib.SendDeletePartnerNotificationEmail(resellerEmail, vSession.User.CompanyName, Request, vSession.Lang, session);
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }

                            HdnPartnerUserId.Value = "0";
                            divFailure.Visible = false;
                            LblFailureMsg.Text = "";
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }
                    }
                    else
                    {
                        divFailure.Visible = true;
                        LblFailureMsg.Text = "Your partner could not be deleted. Please try again later";
                        Logger.DetailedError(string.Format("PAGE DELETE ACTION ERROR--> DashboardCollaborationPartners: User {0}, tried to delete one of his partner at {1}, but vendor-reseller-id could not be found", vSession.User.Id.ToString(), DateTime.Now.ToString()));
                        return;
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

        protected void aDeleteConfirmed_OnClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                divSuccess.Visible = divFailure.Visible = false;

                HdnVendorResellerCollaborationId.Value = item["id"].Text;
                HdnPartnerUserId.Value = item["partner_user_id"].Text;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSearchConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    RdgSendInvitationsConfirmed.Rebind();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion

        #region Old Buttons to Delete

        protected void ImgBtnDeleteSendInvitationsConfirmed_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                int vendorResellerId = Convert.ToInt32(item["id"].Text);
                if (vendorResellerId > 0)
                    DeleteInvitationFromGrid(vendorResellerId, RdgSendInvitationsConfirmed);
                else
                    ShowPopUpModalWithText("Delete Warning", "Your Invitation could not be deleted. Please try again later or contact with Elioplus support");
                //GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, "Your Invitation could not be deleted. Please try again later or contact with Elioplus support", MessageTypes.Error, true, true, false);

                session.CommitTransaction();

                UpdatePanelContent.Update();
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void ImgBtnSendEmailConfirmed_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ImageButton imgBtn = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                    ElioUsers partnerCompany = Sql.GetUserById(Convert.ToInt32(item["partner_user_id"].Text), session);
                    if (partnerCompany != null)
                    {
                        try
                        {
                            ElioCollaborationUsersInvitations userInvitation = SqlCollaboration.GetCollaborationUserInvitationsByVendorResellerIdForReSend(vSession.User.Id, Convert.ToInt32(item["id"].Text), session);
                            if (userInvitation != null)
                            {
                                string confirmationLink = FileHelper.AddToPhysicalRootPath(Request);
                                bool sendLink = true;

                                if (partnerCompany.UserApplicationType == Convert.ToInt32(UserApplicationType.Collaboration) || partnerCompany.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty))
                                {
                                    confirmationLink += ControlLoader.SignUpPartner.Replace("{CompanyName}", Regex.Replace(vSession.User.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower()) + "?verificationViewID=" + partnerCompany.GuId + "&type=" + partnerCompany.UserApplicationType.ToString();
                                    //confirmationLink += "/free-sign-up?verificationViewID=" + partnerCompany.GuId + "&type=" + partnerCompany.UserApplicationType;
                                }
                                else
                                {
                                    if (partnerCompany.AccountStatus == (int)AccountStatus.Completed && partnerCompany.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus))
                                    {
                                        confirmationLink += ControlLoader.Dashboard(partnerCompany, "collaboration-partners");
                                    }
                                    else
                                    {
                                        EmailSenderLib.CollaborationInvitationEmailForNotFullRegisteredUsersWithUserText(partnerCompany.UserApplicationType, item["email"].Text, vSession.User.CompanyName, userInvitation.InvSubject, userInvitation.InvContent, vSession.Lang, session);
                                        sendLink = false;
                                    }
                                }

                                if (confirmationLink != "")
                                {
                                    if (partnerCompany.UserApplicationType == Convert.ToInt32(UserApplicationType.Collaboration))
                                    {
                                        EmailSenderLib.CollaborationInvitationEmail(partnerCompany.UserApplicationType, partnerCompany.Email, vSession.User.CompanyName, vSession.User.CompanyLogo, partnerCompany.CompanyName, confirmationLink, vSession.Lang, session);
                                    }
                                    else
                                        EmailSenderLib.CollaborationInvitationEmailWithUserText(partnerCompany.UserApplicationType, item["email"].Text, vSession.User.CompanyName, confirmationLink, userInvitation.InvSubject, userInvitation.InvContent, vSession.Lang, session);
                                }
                                else
                                    if (sendLink)
                                    Logger.DetailedError(string.Format("Confirmation Link was empty so CollaborationInvitationEmail could not be send by user {0}, at {1}, from page {2}", vSession.User.Id, DateTime.Now.ToString(), "DashboardCollaborationPartners.aspx"));
                            }
                            else
                            {
                                ShowPopUpModalWithText("Invitation Re-Send Warning", "Invitation Re-Send could not be send. Please try again later or contact with us.");

                                Logger.DetailedError(string.Format("Invitation Re-Send could not be found to be send by user {0}, at {1}, from page {2}", vSession.User.Id, DateTime.Now.ToString(), "DashboardCollaborationPartners.aspx"));
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        RdgSendInvitationsConfirmed.Rebind();

                        //GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "4")).Text.Replace("{companyemail}", item["email"].Text), MessageTypes.Success, true, true, false);

                        ShowPopUpModalWithText("Invitation Re-Send", Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "8")).Text.Replace("{companyemail}", item["email"].Text));
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcSendMessageAlertConfirmed, "Invitation could not be send. Please try again later or contact with Elioplus support.", MessageTypes.Error, true, true, false);
                        Logger.DetailedError(string.Format("User {0} tried to re-send invitation to partner {1} at {2} from collaboration tool, but system failed to find partner Id", vSession.User.Id, item["partner_user_id"].Text, DateTime.Now.ToString()));
                    }

                    UpdatePanelContent.Update();
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                GlobalMethods.ShowMessageControlDA(UcSendMessageAlertConfirmed, ex.Message.ToString(), MessageTypes.Error, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion

        #region DropDownLists

        protected void DdlCountriesConfirmed_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                    RdgSendInvitationsConfirmed.Rebind();
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        
    }
}