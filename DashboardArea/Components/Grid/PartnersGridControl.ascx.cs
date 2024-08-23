using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Dashboard.Components.Grid
{
    public partial class PartnersGridControl : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public int CollaborationId
        {
            get
            {
                return (ViewState["CollaborationId"] != null) ? Convert.ToInt32(ViewState["CollaborationId"].ToString()) : -1;
            }
            set
            {
                ViewState["CollaborationId"] = value;
            }
        }

        public bool ShowGridColumns
        {
            get
            {
                return (ViewState["ShowGridColumns"] != null) ? Convert.ToBoolean(ViewState["ShowGridColumns"].ToString()) : true;
            }
            set
            {
                ViewState["ShowGridColumns"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (!IsPostBack)
                    {
                        LoadDealResultStatusPast();
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

        #region methods

        public void Bind()
        {
            try
            {
                session.OpenConnection();

                tdActions.Visible = ShowGridColumns;

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
                    UcMessageAlert.Visible = false;
                    //divSearchArea.Visible = true;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("master_user_id");
                    table.Columns.Add("invitation_status");
                    table.Columns.Add("tier_status");
                    table.Columns.Add("partner_user_id");
                    table.Columns.Add("company_name");
                    table.Columns.Add("sysdate");
                    table.Columns.Add("email");
                    table.Columns.Add("country");
                    table.Columns.Add("is_new");

                    foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                    {
                        table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.TierStatus, partner.PartnerUserId, partner.CompanyName, partner.Sysdate, partner.Email, partner.Country, partner.IsNew);
                    }

                    RdgSendInvitationsConfirmed.DataSource = table;
                    RdgSendInvitationsConfirmed.DataBind();
                    //LblTitle2.Visible = true;
                }
                else
                {
                    RdgSendInvitationsConfirmed.Visible = false;
                    //LblTitle2.Visible = false;
                    //divSearchArea.Visible = false;

                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, (hasCriteria) ? "There are no Confirmed Invitations send to your Partners with these criteria" : "There are no Confirmed Invitations send to your Partners", MessageTypes.Info, true, true, false);
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

        private void FixPage()
        {
            if (!IsPostBack)
            {
                divSearchAreaConfirmed.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
                //divVendorToolsConfirmed.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                UpdateStrings();
                SetLinks();
                LoadCountriesConfirmed();

                UcMessageAlert.Visible = false;
            }

            //aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
            //aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

            //if (aBtnGoPremium.Visible)
            //{
            //    LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
            //    LblPricingPlan.Visible = false;
            //}

            //LblElioplusDashboard.Text = "";

            //LblDashboard.Text = "Dashboard";
            //LblDashPage.Text = "Partner Directory";
            //LblDashSubTitle.Text = "";
            divSuccess.Visible = false;
            LblSuccessMsg.Text = "";

            divFailure.Visible = false;
            LblFailureMsg.Text = "";
            //divActions.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
            //RbtnAccept.Enabled = RbtnDelete.Enabled = RbtnPending.Enabled = RbtnReject.Enabled = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
            //if (RdgReceivedInvitations.DataSource == null && RdgSendInvitations.DataSource == null)
            //{
            //    GlobalMethods.ShowMessageControl(UcReceiveMessageAlert, "There are no Invitations received from your Partners", MessageTypes.Info, true, true, false);

            //    GlobalMethods.ShowMessageControl(UcSendMessageAlert, "You have sent no Invitations to your Partners yet", MessageTypes.Info, true, true, false);
            //}
            //else
            //{
            //    if (RdgSendInvitations.Visible && RdgSendInvitations.DataSource != null)
            //    {
            //        if (vSession.CurrentGridPageIndex != 0)
            //        {
            //            RdgSendInvitations.CurrentPageIndex = vSession.CurrentGridPageIndex;
            //        }
            //    }
            //}

            if (vSession.User.CompanyType != EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                bool hasMessages = false;
                bool hasConfirmedInvitationPartners = SqlCollaboration.HasCollaborationPartnersOrMessages(vSession.User.Id, vSession.User.CompanyType, out hasMessages, session);
                //if (!hasConfirmedInvitationPartners)
                //{
                //    LblDealPartnerDirectoryTitle.Text = "You do not have any Partners yet. Click the button below to Invite them.";
                //}
                //else
                //{
                //    LblDealPartnerDirectoryTitle.Text = "Invite new Partners by clicking the button below.";
                //}

                //divDealPartnerDirectory.Visible = true;
                //divVendorToolsConfirmed.Visible = true;
                ////divInvitationToPartners.Visible = true;
                //aInvitationToPartners.HRef = ControlLoader.Dashboard(vSession.User, "partner-directory-invitations");
            }
            else
            {
                //divVendorToolsConfirmed.Visible = false;
                //divDealPartnerDirectory.Visible = false;
                ////divInvitationToPartners.Visible = false;
            }

            if (!IsPostBack)
            {
                if (vSession.HasToUpdateNewInvitationRequest)
                    vSession.HasToUpdateNewInvitationRequest = !Sql.SetVendorResellerInvitationAsNotNew(vSession.User.Id, session);
            }
        }

        private void SetLinks()
        {
            //aInvitationToPartners.HRef = ControlLoader.Dashboard(vSession.User, "partners-invitations");
            //aChannelPartnerInvitationConfirmed.HRef = ControlLoader.Dashboard(vSession.User, "partners-invitations");
        }

        private void UpdateStrings()
        {
            string partners = (vSession.User.CompanyType == Types.Vendors.ToString()) ? EnumHelper.GetDescription(Types.Resellers).ToString() : Types.Vendors.ToString();

            //LblTitle.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Invitations from my " + partners : "Invitations from my " + partners;
            //LblTitle2Confirmed.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Invitations to my " + partners : "Invitations to my " + partners;
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
                                        //GlobalMethods.ShowMessageControl(UcReceiveMessageAlert, "You can not confirm this invitation because your partner can not manage more partners.Contact with him or with Elioplus team.", MessageTypes.Error, true, true, false);
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
                                        //GlobalMethods.ShowMessageControl(UcReceiveMessageAlert, "Something went wrong. Please contact with Elioplus team.", MessageTypes.Error, true, true, false);
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
                                        //GlobalMethods.ShowMessageControl(UcReceiveMessageAlert, "Something went wrong. Please contact with Elioplus team.", MessageTypes.Error, true, true, false);
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
                                        //GlobalMethods.ShowMessageControl(UcReceiveMessageAlert, "Something went wrong. Please contact with Elioplus team.", MessageTypes.Error, true, true, false);
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

                        //UpdatePanel updatePanelContent = (UpdatePanel)ControlFinder.FindControlRecursive(this.Master, "UpdatePanelContent");
                        //if (updatePanelContent != null)
                        UpdatePanelContent.Update();

                    }
                }
            }

            if (hasSelectedItem)
            {
                rdg.Rebind();
                //GlobalMethods.ShowMessageControl(UcReceiveMessageAlert, "Successfully updated", MessageTypes.Success, true, true, false);

                UpdatePanelContent.Update();
                ShowPopUpModalWithText("Confirmation", "Successfully updated");
            }
            else
                ShowPopUpModalWithText("Warning", "Please select at least one");
            //GlobalMethods.ShowMessageControl(UcReceiveMessageAlert, "Please select at least one", MessageTypes.Warning, true, true, false);
        }

        private void DeleteInvitationFromGrid(int vendorResellerId, Repeater rdg)
        {
            SqlCollaboration.DeleteCollaborationById(vendorResellerId, session);

            rdg.DataBind();

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

        private void SetDealLinkUrl(RepeaterItem item, ElioUsers companyDeal)
        {
            if (item != null)
            {
                DataRowView row = (DataRowView)item.DataItem;

                string url = "";

                string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(row["id"].ToString()), session);
                Session[sessionId] = row["id"].ToString();

                url = ControlLoader.Dashboard(vSession.User, "deal-registration-view") + "?dealViewID=" + sessionId;

                HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                aEdit.HRef = ControlLoader.Dashboard(vSession.User, "deal-registration-view") + "?dealViewID=" + sessionId;
            }
        }

        private void LoadDealResultStatusPast()
        {
            //DdlDealResultPast.Items.Clear();

            //DropDownListItem item = new DropDownListItem();

            //item.Value = "0";
            //item.Text = "Deal's result";
            //DdlDealResultPast.Items.Add(item);

            //item = new DropDownListItem();

            //item.Value = Convert.ToInt32(DealResultStatus.Pending).ToString();
            //item.Text = DealResultStatus.Pending.ToString();
            //DdlDealResultPast.Items.Add(item);

            //item = new DropDownListItem();

            //item.Value = Convert.ToInt32(DealResultStatus.Won).ToString();
            //item.Text = DealResultStatus.Won.ToString();
            //DdlDealResultPast.Items.Add(item);

            //item = new DropDownListItem();
            //item.Value = Convert.ToInt32(DealResultStatus.Lost).ToString();
            //item.Text = DealResultStatus.Lost.ToString();
            //DdlDealResultPast.Items.Add(item);

            //item = new DropDownListItem();
            //item.Value = Convert.ToInt32(DealActivityStatus.Rejected).ToString();
            //item.Text = DealActivityStatus.Rejected.ToString();
            //DdlDealResultPast.Items.Add(item);
        }

        # endregion

        #region Grids

        protected void RdgSendInvitationsConfirmed_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        if (vSession.User != null)
                        {
                            #region Parent

                            int partnerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(row["partner_user_id"].ToString()) : Convert.ToInt32(row["master_user_id"].ToString());
                            ElioUsers company = Sql.GetUserById(partnerId, session);
                            if (company != null)
                            {
                                Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                                Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
                                Label lblTierStatus = (Label)ControlFinder.FindControlRecursive(item, "LblTierStatus");
                                Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                                //HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDeleteConfirmed");
                                HtmlAnchor aCollaborationRoom = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCollaborationRoomConfirmed");
                                //HtmlAnchor aEditTierStatus = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEditTierStatus");
                                //RadComboBox rcbxTierStatus = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxTierStatus");

                                lblCompanyName.Text = row["company_name"].ToString();
                                //aDelete.Title = "Delete specific Invitation from your list";
                                //aDelete.HRef = "#divConfirm";

                                lblTierStatus.Text = (!string.IsNullOrEmpty(row["tier_status"].ToString()) && row["tier_status"].ToString() != "&nbsp;") ? row["tier_status"].ToString() : "Select";
                                //aEditTierStatus.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && Sql.HasTierManagementUserSettings(vSession.User.Id, session);

                                if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers) && lblTierStatus.Text == "Select")
                                {
                                    lblTierStatus.Text = "Not Selected";
                                }

                                aCollaborationRoom.HRef = ControlLoader.Dashboard(company, "collaboration-chat-room");

                                HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                                divNotification.Visible = Convert.ToInt32(row["is_new"].ToString()) == 1 && row["invitation_status"].ToString() == CollaborateInvitationStatus.Confirmed.ToString();
                                if (Convert.ToInt32(row["is_new"].ToString()) == 1 && row["invitation_status"].ToString() == CollaborateInvitationStatus.Confirmed.ToString())
                                {
                                    vSession.HasToUpdateNewInvitationRequest = true;
                                }

                                //HtmlAnchor aMoreDetails = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMoreDetails");
                                HtmlGenericControl spanInvStatus = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanInvStatus");
                                HtmlGenericControl divEdit = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divEdit");
                                divEdit.Visible = ShowGridColumns;

                                if (row["invitation_status"].ToString() == CollaborateInvitationStatus.Confirmed.ToString())
                                {
                                    lblStatus.ToolTip = "The specific Invitation has been confirmed and is available for you to collaborare with.";
                                    lblStatus.Text = "Active";  // CollaborateInvitationStatus.Confirmed.ToString();
                                    spanInvStatus.Attributes["class"] = "label label-lg label-light-success label-inline";

                                    aCollaborationRoom.Visible = ShowGridColumns;
                                }

                                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                                if (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus))
                                {
                                    aCompanyName.HRef = ControlLoader.Profile(company);

                                    imgCompanyLogo.ToolTip = "View company's profile";
                                    imgCompanyLogo.ImageUrl = company.CompanyLogo;
                                    imgCompanyLogo.AlternateText = "Company logo";
                                }
                                else if (company.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty))
                                {
                                    aCompanyName.HRef = company.WebSite;

                                    aCompanyName.Target = "_blank";
                                    imgCompanyLogo.ToolTip = "View company's site";
                                    imgCompanyLogo.ImageUrl = "/images/icons/partners_th_party_2.png";
                                    imgCompanyLogo.AlternateText = "Third party partners logo";
                                }
                                else if (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Collaboration))
                                {
                                    if (company.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                                    {
                                        aCompanyName.HRef = company.WebSite;

                                        aCompanyName.Target = "_blank";
                                        imgCompanyLogo.ToolTip = "View company's site";
                                    }

                                    imgCompanyLogo.ImageUrl = company.CompanyLogo;
                                    imgCompanyLogo.AlternateText = "Potential collaboration company's logo";
                                }

                                if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    aCompanyName.HRef = ControlLoader.Dashboard(vSession.User, "manage-partners");

                                //aMoreDetails.Visible = true;

                                //aMoreDetails.HRef = ControlLoader.PersonProfile(company);
                                //aMoreDetails.Target = "_blank";

                                //Label lblMoreDetails = (Label)ControlFinder.FindControlRecursive(item, "LblMoreDetails");
                                //lblMoreDetails.Text = "Manage Account";
                            }

                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgSendInvitationsConfirmed_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    Bind();
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

        #region Buttons

        protected void BtnSearchPast_Click(object sender, EventArgs e)
        {
            try
            {
                //if (vSession.User != null)
                //{
                //    if (DdlDealResultPast.SelectedItem.Value != "0")
                //    {
                //        if (DdlDealResultPast.SelectedItem.Text != DealActivityStatus.Rejected.ToString())
                //            AdvancedSerch = DdlDealResultPast.SelectedItem.Text;
                //        else if (DdlDealResultPast.SelectedItem.Text == DealActivityStatus.Rejected.ToString())
                //            AdvancedSerch = DdlDealResultPast.SelectedItem.Value;
                //    }
                //    else
                //        AdvancedSerch = "";

                //    RdgDealsPending.Rebind();
                //    RdgDealsOpen.Rebind();
                //    RdgPastDeals.Rebind();
                //    //RdgDealsClosed.Rebind();
                //    //RdgDealsExpired.Rebind();
                //    //RdgDealsRejected.Rebind();
                //}
                //else
                //    Response.Redirect(ControlLoader.Login, false);
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
                            RadComboBox rcbxTierStatus = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxTierStatus");

                            rcbxTierStatus.Items.Clear();

                            //DataTable tblTierStatus = Sql.GetCollaborationUserTierStatusTable(vSession.User.Id, session);
                            List<ElioTierManagementUsersSettings> tierSettings = Sql.GetTierManagementUserSettings(vSession.User.Id, session);       //SqlCollaboration.GetCollaborationUserTierStatus(vSession.User.Id, session);

                            if (tierSettings.Count > 0)
                            {
                                //rcbxTierStatus.DataSource = tblTierStatus;
                                //rcbxTierStatus.DataTextField = "status_description";
                                //rcbxTierStatus.DataValueField = "id";

                                RadComboBoxItem rcbxItem = new RadComboBoxItem();

                                rcbxItem.Value = "0";
                                rcbxItem.Text = "Select";
                                rcbxTierStatus.Items.Add(rcbxItem);

                                foreach (ElioTierManagementUsersSettings tier in tierSettings)
                                {
                                    rcbxItem = new RadComboBoxItem();

                                    rcbxItem.Value = tier.Id.ToString();
                                    rcbxItem.Text = tier.Description;
                                    rcbxTierStatus.Items.Add(rcbxItem);
                                }

                                rcbxTierStatus.DataBind();
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
                                rcbxTierStatus.FindItemByText(selectedTierStatus).Selected = true;
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
                        RadComboBox rcbxTierStatus = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxTierStatus");

                        string selectedTierStatus = (rcbxTierStatus.SelectedItem.Value != "0") ? rcbxTierStatus.SelectedItem.Text : "";
                        int collaborationVendorResellerId = Convert.ToInt32(item["id"].Text);

                        bool success = SqlCollaboration.UpdateCollaborationPartnerTierStatus(collaborationVendorResellerId, vSession.User.Id, selectedTierStatus, session);

                        if (success)
                        {
                            //success updated
                            Bind();
                        }
                        else
                        {
                            //problem
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, something went wrong and tier status could not be saved", MessageTypes.Error, true, true, true, true, false);
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
                        RadComboBox rcbxTierStatus = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxTierStatus");

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
                    if (HdnVendorResellerCollaborationId.Value != "0")
                    {
                        try
                        {
                            session.BeginTransaction();

                            SqlCollaboration.DeleteCollaborationById(Convert.ToInt32(HdnVendorResellerCollaborationId.Value), session);

                            HdnVendorResellerCollaborationId.Value = "0";

                            divSuccess.Visible = true;
                            LblSuccessMsg.Text = "You deleted youw partner successfully";

                            divFailure.Visible = false;
                            LblFailureMsg.Text = "";

                            session.CommitTransaction();

                            Bind();

                            UpdatePanelContent.Update();

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfPopUp();", true);
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

                HdnVendorResellerCollaborationId.Value = item["id"].Text;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDeletePending_OnClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                HdnVendorResellerCollaborationId.Value = item["id"].Text;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

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
                //GlobalMethods.ShowMessageControl(UcSendMessageAlert, "Your Invitation could not be deleted. Please try again later or contact with Elioplus support", MessageTypes.Error, true, true, false);

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

                        Bind();

                        //GlobalMethods.ShowMessageControl(UcSendMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "4")).Text.Replace("{companyemail}", item["email"].Text), MessageTypes.Success, true, true, false);

                        ShowPopUpModalWithText("Invitation Re-Send", Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "8")).Text.Replace("{companyemail}", item["email"].Text));
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Invitation could not be send. Please try again later or contact with Elioplus support.", MessageTypes.Error, true, true, true, true, false);
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
                GlobalMethods.ShowMessageControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnSearchConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    Bind();
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

        #region DropDownLists

        protected void DdlCountriesConfirmed_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                    Bind();
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

        protected void DdlInvitationStatus_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    Bind();
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

        protected void DdlTierStatus_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {

                }
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