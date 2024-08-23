using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using System.Data;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Net.Mail;
using System.Configuration;

namespace WdS.ElioPlus
{
    public partial class DashboardCollaborationNewPartners : System.Web.UI.Page
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

                    if (!IsPostBack)
                    {
                        ElioUsers user = null;
                        bool isError = false;
                        string errorPage = string.Empty;
                        //string key = string.Empty;
                        RtbxCompanyName.Text = "";

                        RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, session);

                        if (isError)
                        {
                            Response.Redirect(vSession.Page = errorPage, false);
                            return;
                        }

                        FixPage();
                    }

                    //PhDialogContent.Controls.Clear();
                    //PhDialogContent.Controls.Add(UcCreateNewInvitationToPartnersMessage);
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
                UpdateStrings();
                SetLinks();
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

            //LblPricingPlan.Text = vSession.User.BillingType == Convert.ToInt32(BillingType.Premium) ? "You are currently on a premium plan" : "You are currently on a free plan";

            //LblElioplusDashboard.Text = "";
            //LblOrText.Text = " or ";
            //LblDashboard.Text = "Dashboard";
            //LblDashPage.Text = "Collaboration Invite New Partners";
            LblDashSubTitle.Text = "";

            string partners = (vSession.User.CompanyType == Types.Vendors.ToString()) ? EnumHelper.GetDescription(Types.Resellers).ToString() : Types.Vendors.ToString();
            LblConnectionsTitle.Text = "List of your " + partners + " connections";

            divExistingPartners.Visible = true; // (vSession.User.CompanyType == Types.Vendors.ToString()) ? true : false;     //to do about resellers how to or if send invitations to outside partners
        }

        private void SetLinks()
        {
            aCancelInvitation.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-partners");
        }

        private void UpdateStrings()
        {
            //LblCollaborationTitle.Text = "";            
        }

        private string GetApplicationRootPath()
        {
            return Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
        }

        # endregion

        #region Grids

        protected void RdgResellers_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    //ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                    //imgBtnEdit.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "1")).Text;

                    ElioUsers company = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                    if (company != null)
                    {
                        Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");

                        //////string invStatus = Sql.GetCollaborationInvitationStatus(vSession.User.Id, company.Id, session);
                        //////if (invStatus != string.Empty)
                        //////{
                        //////    Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");

                        //////    if (invStatus == CollaborateInvitationStatus.Rejected.ToString())
                        //////    {
                        //////        lblStatus.ToolTip = "The specific Prtner has rejected your Invitation. You can re-send your Invotation if you want.";
                        //////        lblStatus.Text = CollaborateInvitationStatus.Rejected.ToString();
                        //////        lblStatus.CssClass = "label label-outline label-primary";
                        //////    }
                        //////    else if (invStatus == CollaborateInvitationStatus.Pending.ToString())
                        //////    {
                        //////        lblStatus.ToolTip = "The specyfic Partner has not accept your Invitation yet. You can re-send your Invotation if you want.";
                        //////        lblStatus.Text = CollaborateInvitationStatus.Pending.ToString();
                        //////        lblStatus.CssClass = "label label-outline label-purple";
                        //////    }
                                                        
                        //////    lblStatus.ToolTip = invStatus;
                        //////}

                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        //HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                        if (Convert.ToInt32(item["user_application_type"].Text) == Convert.ToInt32(UserApplicationType.Elioplus))
                        {
                            aCompanyLogo.HRef = ControlLoader.Profile(company);

                            imgCompanyLogo.ToolTip = "View company's profile";
                            imgCompanyLogo.ImageUrl = item["company_logo"].Text;
                            imgCompanyLogo.AlternateText = "Company logo";
                        }
                        else
                        {
                            aCompanyLogo.HRef = company.WebSite;

                            //aCompanyLogo.Target = aCompanyName.Target = "_blank";
                            imgCompanyLogo.ToolTip = "View company's site";
                            imgCompanyLogo.ImageUrl = "~/images/icons/partners_th_party_2.png";
                            imgCompanyLogo.AlternateText = "Third party partners logo";
                        }
                    }
                }
                else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "CompanyItems")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);

                    if (user != null)
                    {

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

        protected void RdgResellers_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    List<ElioUsers> users = SqlCollaboration.GetUserCollaborationConnections(vSession.User.Id, vSession.User.CompanyType, RtbxCompanyName.Text, "", session);        //Sql.GetAvailableUsersForCollaborationByType(EnumHelper.GetDescription(Types.Resellers).ToString(), session);

                    if (users.Count > 0)
                    {
                        RdgResellers.Visible = true;
                        UcMessageAlert.Visible = false;
                        divSearchArea.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("company_logo");
                        table.Columns.Add("company_name");
                        table.Columns.Add("user_application_type");
                        table.Columns.Add("email");
                        table.Columns.Add("country");

                        foreach (ElioUsers user in users)
                        {
                            table.Rows.Add(user.Id, user.CompanyLogo, user.CompanyName, user.UserApplicationType, user.Email, user.Country);
                        }

                        RdgResellers.DataSource = table;

                        BtnProccedMessage.Visible = true;
                    }
                    else
                    {
                        RdgResellers.Visible = false;
                        LblConnectionsTitle.Text = string.Empty;
                        BtnProccedMessage.Visible = false;
                        aInvitationToConnections.Visible = false;
                        //aInvitationToPartners.Visible = false;
                        LblOrText.Text = string.Empty;
                        divSearchArea.Visible = false;

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "There are no Connections as possible partners", MessageTypes.Info, true, true, false);
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

        protected void BtnProccedMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    List<ElioUsers> partners = new List<ElioUsers>();

                    bool hasSelectedItem = false;
                    foreach (GridDataItem item in RdgResellers.Items)
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
                        //string alert = "Please select at least one partner in order to send your invitation";
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Warning, true, true, false);
                        LblInvitationSendTitle.Text = "Invitation Send Warning";
                        LblSuccessfullSendfMsg.Text = "Please select at least one partner in order to send your invitation";
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                        return;
                    }
                    else
                    {
                        foreach (ElioUsers partner in partners)
                        {
                            try
                            {
                                session.BeginTransaction();

                                string confirmationLink = FileHelper.AddToPhysicalRootPath(Request);

                                if (partner.UserApplicationType == (int)UserApplicationType.ThirdParty)
                                {
                                    confirmationLink += "/free-sign-up?verificationViewID=" + partner.GuId + "&type=" + partner.UserApplicationType.ToString();
                                }
                                else
                                {
                                    confirmationLink += ControlLoader.Dashboard(partner, "collaboration-partners");
                                }

                                ElioCollaborationUsersInvitations userInvitation = new ElioCollaborationUsersInvitations();

                                MailMessage mailMessage = EmailSenderLib.FillMailMessage(EmailNotificationDesctriptions.CollaborationInvitationEmailForExistingConnections.ToString(), vSession.Lang, true, ConfigurationManager.AppSettings["EmailInvitationsTemplateFileName"].ToString(), session);

                                if (mailMessage != null)
                                {
                                    mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", vSession.User.CompanyName);
                                    mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", vSession.User.CompanyName).Replace("{link}", "<a href=\"" + confirmationLink + "\" target=\"_blank\">here</a>").ToString();

                                    userInvitation.UserId = vSession.User.Id;
                                    userInvitation.InvSubject = mailMessage.Subject;
                                    userInvitation.InvContent = mailMessage.Body;
                                    userInvitation.DateCreated = DateTime.Now;
                                    userInvitation.LastUpdated = DateTime.Now;
                                    userInvitation.IsPublic = 1;

                                    DataLoader<ElioCollaborationUsersInvitations> userInvitationLoader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                                    userInvitationLoader.Insert(userInvitation);
                                }

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
                                    vendorResselerInvitation.UserInvitationId = userInvitation.Id;
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
                                    ElioCollaborationVendorResellerInvitations venResInvitation = SqlCollaboration.GetCollaborationVendorResellerInvitationByVenResIdAndInvId(collaboration.Id, userInvitation.Id, session);
                                    if (venResInvitation == null)
                                    {
                                        ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                                        vendorResselerInvitation.UserId = vSession.User.Id;
                                        vendorResselerInvitation.VendorResellerId = collaboration.Id;
                                        vendorResselerInvitation.UserInvitationId = userInvitation.Id;
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

                                try
                                {
                                    //maybe asynchronously send emails, to do
                                    EmailSenderLib.CollaborationInvitationEmail(mailMessage, partner.Email, vSession.User.CompanyName, confirmationLink, vSession.Lang, session);
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    Logger.DetailedError(string.Format("PAGE SEND INVITATION ACTION ERROR --> DashboardCollaborationNewPartners: User {0}, tried to send invitation to his connection partner {1}, at {2}, but emai with link '{3}' could not ne send", vSession.User.Id.ToString(), partner.Id.ToString(), DateTime.Now.ToString(), confirmationLink));
                                }
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }  
                        }

                        //string alert = "Your invitation to your selected connections was successfully send!";
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Success, true, true, false);

                        LblInvitationSendTitle.Text = "Invitation Send";
                        LblSuccessfullSendfMsg.Text = "Your Invitation was sent successfully";
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                        RdgResellers.Rebind();
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

        protected void ImgBtnEditCompany_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    RdgResellers.Rebind();
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
    }
}