using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus
{
    public partial class DashboardDealPartnerDirectory : System.Web.UI.Page
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
                divPgToolbar.Visible = divSearchArea.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
                //divResellerTools.Visible = vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();
                divVendorTools.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                UpdateStrings();
                SetLinks();
                LoadCountries();
                LoadInvitationStatus();
            }

            //if (vSession.User.BillingType == Convert.ToInt32(BillingType.Premium))
            //{
            //    LblRenewalHead.Visible = LblRenewal.Visible = true;
            //    LblRenewalHead.Text = "Renewal date: ";

            //    try
            //    {
            //        LblRenewal.Text = Sql.GetUserRenewalDateFromActiveOrder(vSession.User.Id, session).ToString("MM/dd/yyyy");
            //    }
            //    catch (Exception)
            //    {
            //        LblRenewalHead.Visible = LblRenewal.Visible = false;

            //        Logger.Debug(string.Format("User {0} seems to be premium but he has no order in his account", vSession.User.Id.ToString()));
            //    }
            //}
            //else
            //{
            //    LblRenewalHead.Visible = LblRenewal.Visible = false;
            //}

            aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
            aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;
            //LblPricingPlan.Text = vSession.User.BillingType == Convert.ToInt32(BillingType.Premium) ? "You are currently on a premium plan" : "You are currently on a free plan";

            if (aBtnGoPremium.Visible)
            {
                LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
                LblPricingPlan.Visible = false;
            }

            LblElioplusDashboard.Text = "";

            LblDashboard.Text = "Dashboard";
            //////LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "label", "17")).Text;
            //////LblGoFull.Text = "Complete your registration";
            LblDashPage.Text = "Partner Directory";
            LblDashSubTitle.Text = "";
            divSuccess.Visible = false;
            LblSuccessMsg.Text = "";

            divFailure.Visible = false;
            LblFailureMsg.Text = "";
            //divActions.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
            RbtnAccept.Enabled = RbtnDelete.Enabled = RbtnPending.Enabled = RbtnReject.Enabled = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
            if (RdgReceivedInvitations.DataSource == null && RdgSendInvitations.DataSource == null)
            {
                GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "There are no Invitations received from your Partners", MessageTypes.Info, true, true, false);

                GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, "You have sent no Invitations to your Partners yet", MessageTypes.Info, true, true, false);
            }
            else
            {
                if (RdgSendInvitations.Visible && RdgSendInvitations.DataSource != null)
                {
                    if (vSession.CurrentGridPageIndex != 0)
                    {
                        RdgSendInvitations.CurrentPageIndex = vSession.CurrentGridPageIndex;
                    }
                }
            }

            if (vSession.User.CompanyType != EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                bool hasMessages = false;
                bool hasConfirmedInvitationPartners = SqlCollaboration.HasCollaborationPartnersOrMessages(vSession.User.Id, vSession.User.CompanyType, out hasMessages, session);
                if (!hasConfirmedInvitationPartners)
                {                    
                    LblDealPartnerDirectoryTitle.Text = "You do not have any Partners yet. Click the button below to Invite them.";
                }
                else
                {
                    LblDealPartnerDirectoryTitle.Text = "Invite new Partners by clicking the button below.";
                }

                divDealPartnerDirectory.Visible = true;
                divVendorTools.Visible = true;
                //divInvitationToPartners.Visible = true;
                aInvitationToPartners.HRef = ControlLoader.Dashboard(vSession.User, "partner-directory-invitations");
            }
            else
            {
                divVendorTools.Visible = false;
                divDealPartnerDirectory.Visible = false;
                //divInvitationToPartners.Visible = false;
            }

            if (!IsPostBack)
            {
                if (vSession.HasToUpdateNewInvitationRequest)
                    vSession.HasToUpdateNewInvitationRequest = !Sql.SetVendorResellerInvitationAsNotNew(vSession.User.Id, session);
            }
        }

        private void SetLinks()
        {
            aInvitationToPartners.HRef = ControlLoader.Dashboard(vSession.User, "partner-directory-invitations");
            aChannelPartnerInvitation.HRef = ControlLoader.Dashboard(vSession.User, "partner-directory-invitations");
        }

        private void UpdateStrings()
        {
            string partners = (vSession.User.CompanyType == Types.Vendors.ToString()) ? EnumHelper.GetDescription(Types.Resellers).ToString() : Types.Vendors.ToString();

            LblTitle.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Invitations from my " + partners : "Invitations from my " + partners;
            LblTitle2.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Invitations to my " + partners : "Invitations to my " + partners;
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
            UcReceiveMessageAlert.Visible = false;

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
                                            EmailSenderLib.SendEmailToCollaboratePartnerForAcceptInvitation(item["email"].Text, vSession.User.CompanyName, vSession.User.CompanyType, Request, false, vSession.Lang, session);
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }
                                    }
                                    else
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "You can not confirm this invitation because your partner can not manage more partners.Contact with him or with Elioplus team.", MessageTypes.Error, true, true, false);
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
                                        GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "Something went wrong. Please contact with Elioplus team.", MessageTypes.Error, true, true, false);
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
                                        GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "Something went wrong. Please contact with Elioplus team.", MessageTypes.Error, true, true, false);
                                        UpdatePanelContent.Update();
                                        ShowPopUpModalWithText("Warning", "Something went wrong. Please contact with Elioplus team.");
                                        
                                        Logger.DetailedError(string.Format("User {0} tried to do some action at {1} with his invitation and something went wrong.", vSession.User.Id.ToString(), DateTime.Now.ToString()));
                                        return;
                                    }

                                    try
                                    {
                                        EmailSenderLib.SendEmailToCollaboratePartnerForRejectInvitation(item["email"].Text, vSession.User.CompanyName, vSession.User.CompanyType, false, vSession.Lang, session);
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
                                        GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "Something went wrong. Please contact with Elioplus team.", MessageTypes.Error, true, true, false);
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

        private void LoadCountries()
        {            
            DdlCountries.Items.Clear();

            DropDownListItem item = new DropDownListItem();
            item.Value = "0";
            item.Text = "Select Country";

            DdlCountries.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new DropDownListItem();
                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                DdlCountries.Items.Add(item);
            }
        }

        private void LoadInvitationStatus()
        {
            DdlInvitationStatus.Items.Clear();

            DropDownListItem item = new DropDownListItem();
            item.Value = "-2";
            item.Text = "Select Status";

            DdlInvitationStatus.Items.Add(item);

            item = new DropDownListItem();
            item.Value = Convert.ToInt32(CollaborateInvitationStatus.Confirmed).ToString();
            item.Text = CollaborateInvitationStatus.Confirmed.ToString();

            DdlInvitationStatus.Items.Add(item);

            item = new DropDownListItem();
            item.Value = Convert.ToInt32(CollaborateInvitationStatus.Pending).ToString();
            item.Text = CollaborateInvitationStatus.Pending.ToString();

            DdlInvitationStatus.Items.Add(item);

            item = new DropDownListItem();
            item.Value = Convert.ToInt32(CollaborateInvitationStatus.Rejected).ToString();
            item.Text = CollaborateInvitationStatus.Rejected.ToString();

            DdlInvitationStatus.Items.Add(item);

            item = new DropDownListItem();
            item.Value = Convert.ToInt32(CollaborateInvitationStatus.Deleted).ToString();
            item.Text = CollaborateInvitationStatus.Deleted.ToString();

            DdlInvitationStatus.Items.Add(item);
        }

        # endregion

        #region Grids

        protected void RdgReceivedInvitations_OnItemDataBound(object sender, GridItemEventArgs e)
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
                        ImageButton imgNotAvailable = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgNotAvailable");
                        Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
                        ImageButton imgBtnDelete = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnDelete");
                        CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
                        HtmlAnchor aGoFullRegister = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aGoFullRegister");
                        aGoFullRegister.Visible = vSession.User.AccountStatus != Convert.ToInt32(AccountStatus.Public);
                        aGoFullRegister.Title = "Click here to complete your full registration in order to be able to accept or edit this invitation";
                        aGoFullRegister.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
                        cbxSelectUser.Visible = imgBtnDelete.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed) && !aGoFullRegister.Visible;
                        imgNotAvailable.Visible = !cbxSelectUser.Visible;

                        imgBtnDelete.ToolTip = "Delete specific Partner Invitation from your list";

                        HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                        divNotification.Visible = Convert.ToInt32(item["is_new"].Text) == 1 && item["invitation_status"].Text == CollaborateInvitationStatus.Pending.ToString();
                        
                        HtmlAnchor aMoreDetails = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMoreDetails");

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            if (item["invitation_status"].Text == CollaborateInvitationStatus.Rejected.ToString())
                            {
                                //imgStatus.ImageUrl = "~/images/icons/small/rejected_5.png";
                                lblStatus.ToolTip = "The specific Prtner has rejected your Invitation. You can re-send your Invotation if you want.";
                                lblStatus.Text = CollaborateInvitationStatus.Rejected.ToString();
                                lblStatus.CssClass = "label label-outline label-primary";
                                //cbxSelectUser.Enabled = false;
                                //aMoreDetails.Visible = false;
                            }
                            else if (item["invitation_status"].Text == CollaborateInvitationStatus.Pending.ToString())
                            {
                                //imgStatus.ImageUrl = "~/images/icons/small/rejected_1.png";
                                lblStatus.ToolTip = "The specyfic Partner has not accept your Invitation yet. You can re-send your Invotation if you want.";
                                lblStatus.Text = CollaborateInvitationStatus.Pending.ToString();
                                lblStatus.CssClass = "label label-outline label-success";
                                //cbxSelectUser.Enabled = false;
                                //aMoreDetails.Visible = false;
                            }
                            else if (item["invitation_status"].Text == CollaborateInvitationStatus.Confirmed.ToString())
                            {
                                //imgStatus.ImageUrl = "~/images/icons/small/confirmed_1.png";
                                lblStatus.ToolTip = "The specyfic Partner has confirmed your Invitation and is available for you to collaborare with.";
                                lblStatus.Text = CollaborateInvitationStatus.Confirmed.ToString();
                                lblStatus.CssClass = "label label-outline label-success";
                                cbxSelectUser.Enabled = true;
                                //aMoreDetails.Visible = true;
                            }
                        }
                        else
                        {
                            cbxSelectUser.Enabled = true;

                            if (item["invitation_status"].Text == CollaborateInvitationStatus.Rejected.ToString())
                            {
                                //imgStatus.ImageUrl = "~/images/icons/small/rejected_5.png";
                                lblStatus.ToolTip = "You have reject Invitation. You can change it's status if you want.";
                                lblStatus.Text = CollaborateInvitationStatus.Rejected.ToString();
                                lblStatus.CssClass = "label label-outline label-primary";
                                //aMoreDetails.Visible = false;
                            }
                            else if (item["invitation_status"].Text == CollaborateInvitationStatus.Pending.ToString())
                            {
                                //imgStatus.ImageUrl = "~/images/icons/small/rejected_1.png";
                                lblStatus.ToolTip = "You have not accept Invitation yet. You can change it's status if you want.";
                                lblStatus.Text = CollaborateInvitationStatus.Pending.ToString();
                                lblStatus.CssClass = "label label-outline label-success";
                                //aMoreDetails.Visible = false;
                            }
                            else if (item["invitation_status"].Text == CollaborateInvitationStatus.Confirmed.ToString())
                            {
                                //imgStatus.ImageUrl = "~/images/icons/small/confirmed_1.png";
                                lblStatus.ToolTip = "You have accepted Invitation and is available for you to collaborare with.";
                                lblStatus.Text = CollaborateInvitationStatus.Confirmed.ToString();
                                lblStatus.CssClass = "label label-outline label-primary";
                                //aMoreDetails.Visible = true;
                            }
                        }

                        aMoreDetails.Visible = true;

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
                                                
                        aMoreDetails.HRef = ControlLoader.PersonProfile(company);
                        aMoreDetails.Target = "_blank";

                        Label lblMoreDetails = (Label)ControlFinder.FindControlRecursive(item, "LblMoreDetails");
                        lblMoreDetails.Text = "Manage Account";
                    }

                    #endregion
                }
                else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "VendorsResellersUsersInvitations")
                {
                    #region Vendors-Resellers Invitations

                    GridDataItem item = (GridDataItem)e.Item;

                    if (item["vendor_reseller_id"].Text != "&nbsp;" && item["vendor_reseller_id"].Text != "")
                    {
                        int invitationId = Convert.ToInt32(item["id"].Text);
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

        protected void RdgReceivedInvitations_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    List<ElioCollaborationVendorsResellersIJUsers> partners = new List<ElioCollaborationVendorsResellersIJUsers>();

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        partners = SqlCollaboration.GetVendorCollaborationInvitationsReceivedFromChannelPartners(vSession.User.Id, RtbxCompanyNameEmail.Text, DdlInvitationStatus.SelectedItem.Text, DdlCountries.SelectedItem.Text, session);
                    }
                    else
                    {
                        //partners = SqlCollaboration.GetCollaborationInvitationsReceivedByMaster(vSession.User.Id, session);
                        partners = SqlCollaboration.GetChannelPartnerCollaborationInvitationsReceivedFromVendors(vSession.User.Id, RtbxCompanyNameEmail.Text, DdlInvitationStatus.SelectedItem.Text, DdlCountries.SelectedItem.Text, session);
                    }

                    if (partners.Count > 0)
                    {
                        RdgReceivedInvitations.Visible = true;
                        UcReceiveMessageAlert.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("master_user_id");
                        table.Columns.Add("invitation_status");
                        table.Columns.Add("partner_user_id");
                        table.Columns.Add("company_name");
                        table.Columns.Add("email");
                        table.Columns.Add("country");
                        table.Columns.Add("is_new");

                        foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                        {
                            table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, partner.Country, partner.IsNew);
                        }

                        RdgReceivedInvitations.DataSource = table;

                        divActions.Visible = true;
                        //LblTitle.Visible = true;
                    }
                    else
                    {
                        RdgReceivedInvitations.Visible = false;
                        divActions.Visible = false;
                        //LblTitle.Visible = false;
                        //GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "There are no Invitations received from your Partners", MessageTypes.Info, true, true, false);
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

        protected void RdgReceivedInvitations_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                session.OpenConnection();

                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "VendorsResellersUsersInvitations":
                        {
                            int vendorResellerId = Convert.ToInt32(dataItem.GetDataKeyValue("id").ToString());
                            int isPublic = 1;
                            List<ElioCollaborationUsersInvitations> userInvitations = SqlCollaboration.GetCollaborationUserInvitationsByVendorResellerId(vendorResellerId, isPublic, session);
                            if (userInvitations.Count > 0)
                            {
                                DataTable table = new DataTable();

                                table.Columns.Add("id");
                                table.Columns.Add("vendor_reseller_id");
                                table.Columns.Add("user_id");
                                table.Columns.Add("inv_subject");
                                table.Columns.Add("inv_content");
                                table.Columns.Add("date_created");
                                table.Columns.Add("last_updated");
                                table.Columns.Add("is_public");

                                foreach (ElioCollaborationUsersInvitations invitation in userInvitations)
                                {
                                    table.Rows.Add(invitation.Id, vendorResellerId, invitation.UserId, invitation.InvSubject, invitation.InvContent, invitation.DateCreated.ToString("dd/MM/yyyy"), invitation.LastUpdated, invitation.IsPublic);
                                }

                                e.DetailTableView.DataSource = table;
                            }
                            else
                            {
                                e.DetailTableView.DataSource = null;
                                e.DetailTableView.Visible = false;
                            }
                        }

                        break;
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

        protected void RdgReceivedInvitations_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //RdgElioUsers.MasterTableView.Items[0].Expanded = true;
                //RdgElioUsers.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            }
        }

        protected void RdgSendInvitations_OnPageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                vSession.CurrentGridPageIndex = e.NewPageIndex;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgSendInvitations_OnItemDataBound(object sender, GridItemEventArgs e)
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
                        ImageButton imgBtnSendEmail = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSendEmail");
                        //ImageButton imgBtnDelete = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnDelete");
                        HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");

                        ImageButton imgBtnCollaborationRoom = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCollaborationRoom");
                        //CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
                        //HtmlAnchor aCollaborationRoom = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCollaborationRoom");

                        //imgBtnDelete.ToolTip = "Delete specific Invitation from your list";
                        aDelete.Title = "Delete specific Invitation from your list";
                        aDelete.HRef = "#divConfirm";

                        HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                        divNotification.Visible = Convert.ToInt32(item["is_new"].Text) == 1 && item["invitation_status"].Text == CollaborateInvitationStatus.Confirmed.ToString();
                        if (Convert.ToInt32(item["is_new"].Text) == 1 && item["invitation_status"].Text == CollaborateInvitationStatus.Confirmed.ToString())
                        {
                            vSession.HasToUpdateNewInvitationRequest = true;
                        }

                        HtmlAnchor aMoreDetails = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMoreDetails");

                        if (item["invitation_status"].Text == CollaborateInvitationStatus.Rejected.ToString())
                        {
                            lblStatus.ToolTip = "The specific Invitation has been rejected. You can re-send your Invitation if you want.";
                            lblStatus.Text = CollaborateInvitationStatus.Rejected.ToString();
                            lblStatus.CssClass = "label label-outline label-primary";

                            imgBtnSendEmail.Visible = true;
                            imgBtnSendEmail.ToolTip = "Re-Send Invitation";
                            //aCollaborationRoom.Visible = false;
                            imgBtnCollaborationRoom.Visible = false;

                            //aMoreDetails.Visible = false;
                        }
                        else if (item["invitation_status"].Text == CollaborateInvitationStatus.Pending.ToString())
                        {
                            lblStatus.ToolTip = "The specific Invitation has not been accepted and is pending. You can re-send your Invitation if you want.";
                            lblStatus.Text = CollaborateInvitationStatus.Pending.ToString();
                            lblStatus.CssClass = "label label-outline label-success";

                            imgBtnSendEmail.Visible = true;
                            imgBtnSendEmail.ToolTip = "Re-Send Invitation";
                            //aCollaborationRoom.Visible = false;
                            imgBtnCollaborationRoom.Visible = false;

                            //aMoreDetails.Visible = false;
                        }
                        else if (item["invitation_status"].Text == CollaborateInvitationStatus.Confirmed.ToString())
                        {
                            lblStatus.ToolTip = "The specific Invitation has been confirmed and is available for you to collaborare with.";
                            lblStatus.Text = CollaborateInvitationStatus.Confirmed.ToString();
                            lblStatus.CssClass = "label label-outline label-primary";

                            imgBtnSendEmail.Visible = false;
                            //aCollaborationRoom.Visible = true;
                            imgBtnCollaborationRoom.Visible = true;

                            //aMoreDetails.Visible = true;
                        }

                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");

                        //if (aCollaborationRoom.Visible)
                        //{
                        //aCollaborationRoom.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-chat-room");                            
                        //}

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

                        aMoreDetails.HRef = ControlLoader.PersonProfile(company);
                        aMoreDetails.Target = "_blank";

                        Label lblMoreDetails = (Label)ControlFinder.FindControlRecursive(item, "LblMoreDetails");
                        lblMoreDetails.Text = "Manage Account";
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

        protected void RdgSendInvitations_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    bool hasCriteria = RtbxCompanyNameEmail.Text.Trim() != "" || DdlInvitationStatus.SelectedItem.Value != "-2" || DdlCountries.SelectedItem.Value != "0";

                    List<ElioCollaborationVendorsResellersIJUsers> partners = new List<ElioCollaborationVendorsResellersIJUsers>();

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        partners = SqlCollaboration.GetVendorCollaborationInvitationsToFromChannelPartners(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, RtbxCompanyNameEmail.Text.Trim(), (DdlInvitationStatus.SelectedItem.Value != "-2") ? DdlInvitationStatus.SelectedItem.Text : "", (DdlCountries.SelectedItem.Value != "0") ? DdlCountries.SelectedItem.Text : "", session);
                    }
                    else
                    {
                        partners = SqlCollaboration.GetChannelPartnerCollaborationInvitationsToFromVendors(vSession.User.Id, RtbxCompanyNameEmail.Text.Trim(), (DdlInvitationStatus.SelectedItem.Value != "-2") ? DdlInvitationStatus.SelectedItem.Text : "", (DdlCountries.SelectedItem.Value != "0") ? DdlCountries.SelectedItem.Text : "", session);
                    }

                    //if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                    //{
                    //    partners = SqlCollaboration.GetCollaborationInvitationsSendToMasterByChannelPartner(vSession.User.Id, RtbxCompanyNameEmail.Text.Trim(), (DdlInvitationStatus.SelectedItem.Value != "-2") ? DdlInvitationStatus.SelectedItem.Text : "", (DdlCountries.SelectedItem.Value != "0") ? DdlCountries.SelectedItem.Text : "", session);
                    //}
                    //else
                    //{
                    //    partners = SqlCollaboration.GetCollaborationInvitationsSendByMaster(vSession.User.Id, RtbxCompanyNameEmail.Text.Trim(), (DdlInvitationStatus.SelectedItem.Value != "-2") ? DdlInvitationStatus.SelectedItem.Text : "", (DdlCountries.SelectedItem.Value != "0") ? DdlCountries.SelectedItem.Text : "", session);
                    //}

                    if (partners.Count > 0)
                    {
                        RdgSendInvitations.Visible = true;
                        UcSendMessageAlert.Visible = false;
                        //divSearchArea.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("master_user_id");
                        table.Columns.Add("invitation_status");
                        table.Columns.Add("partner_user_id");
                        table.Columns.Add("company_name");
                        table.Columns.Add("email");
                        table.Columns.Add("country");
                        table.Columns.Add("is_new");

                        foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                        {
                            table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, partner.Country, partner.IsNew);
                        }

                        RdgSendInvitations.DataSource = table;

                        //LblTitle2.Visible = true;
                    }
                    else
                    {
                        RdgSendInvitations.Visible = false;
                        //LblTitle2.Visible = false;
                        //divSearchArea.Visible = false;

                        GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, (hasCriteria) ? "There are no Invitations send to your Partners with these criteria" : "There are no Invitations send to your Partners", MessageTypes.Info, true, true, false);
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

        protected void RdgSendInvitations_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                session.OpenConnection();

                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "VendorsResellersUsersInvitations":
                        {
                            int vendorResellerId = Convert.ToInt32(dataItem.GetDataKeyValue("id").ToString());
                            int isPublic = 1;
                            List<ElioCollaborationUsersInvitations> userInvitations = SqlCollaboration.GetCollaborationUserInvitationsByVendorResellerId(vendorResellerId, isPublic, session);
                            if (userInvitations.Count > 0)
                            {
                                DataTable table = new DataTable();

                                table.Columns.Add("id");
                                table.Columns.Add("vendor_reseller_id");
                                table.Columns.Add("user_id");
                                table.Columns.Add("inv_subject");
                                table.Columns.Add("inv_content");
                                table.Columns.Add("date_created");
                                table.Columns.Add("last_updated");
                                table.Columns.Add("is_public");

                                foreach (ElioCollaborationUsersInvitations invitation in userInvitations)
                                {
                                    table.Rows.Add(invitation.Id, vendorResellerId, invitation.UserId, invitation.InvSubject, invitation.InvContent, invitation.DateCreated.ToString("dd/MM/yyyy"), invitation.LastUpdated, invitation.IsPublic);
                                }

                                e.DetailTableView.DataSource = table;
                            }
                            else
                            {
                                e.DetailTableView.DataSource = null;
                                e.DetailTableView.Visible = false;
                            }
                        }

                        break;
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

        protected void RdgSendInvitations_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //RdgElioUsers.MasterTableView.Items[0].Expanded = true;
                //RdgElioUsers.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            }
        }

        #endregion

        # region Buttons

        protected void ImgBtnCollaborationRoom_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton btn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                vSession.VendorsResellersList.Clear();

                ElioCollaborationVendorsResellers vendRes = SqlCollaboration.GetCollaborationVendorResellerById(Convert.ToInt32(item["id"].Text), session);
                if (vendRes != null)
                    GlobalDBMethods.AddRemoveChatReceiversMessages(Mode.SimpleMode, Mode.Any, vSession.VendorsResellersList, vendRes);

                //vSession.VendorsResellersList.Add(vendRes);

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

        protected void RbtnAccept_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixGridStatus(RdgReceivedInvitations, CollaborateInvitationStatus.Confirmed.ToString());
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

        protected void RbtnPending_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixGridStatus(RdgReceivedInvitations, CollaborateInvitationStatus.Pending.ToString());
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

        protected void RbtnReject_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixGridStatus(RdgReceivedInvitations, CollaborateInvitationStatus.Rejected.ToString());
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

                            RdgSendInvitations.Rebind();

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

        protected void aDelete_OnClick(object sender, EventArgs args)
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

        protected void ImgBtnDeleteSendInvitations_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                int vendorResellerId = Convert.ToInt32(item["id"].Text);
                if (vendorResellerId > 0)
                    DeleteInvitationFromGrid(vendorResellerId, RdgSendInvitations);
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

        protected void ImgBtnDeleteReceivedInvitations_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                int vendorResellerId = Convert.ToInt32(item["id"].Text);
                if (vendorResellerId > 0)
                    DeleteInvitationFromGrid(vendorResellerId, RdgReceivedInvitations);
                else
                    ShowPopUpModalWithText("Delete Warning", "Your Invitation could not be deleted. Please try again later or contact with Elioplus support");
                //GlobalMethods.ShowMessageControlDA(UcReceiveMessageAlert, "Your Invitation could not be deleted. Please try again later or contact with Elioplus support", MessageTypes.Error, true, true, false);

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

        protected void ImgBtnSendEmail_OnClick(object sender, EventArgs args)
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

                        RdgSendInvitations.Rebind();

                        //GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "4")).Text.Replace("{companyemail}", item["email"].Text), MessageTypes.Success, true, true, false);

                        ShowPopUpModalWithText("Invitation Re-Send", Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "8")).Text.Replace("{companyemail}", item["email"].Text));
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, "Invitation could not be send. Please try again later or contact with Elioplus support.", MessageTypes.Error, true, true, false);
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
                GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    RdgSendInvitations.Rebind();
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

        protected void DdlCountries_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                if (vSession.User != null)
                    RdgSendInvitations.Rebind();
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
        
        protected void DdlInvitationStatus_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                if (vSession.User != null)
                    RdgSendInvitations.Rebind();
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