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
using System.Web;
using System.IO;
using WdS.ElioPlus.Lib.ImagesHelper;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Text.RegularExpressions;

namespace WdS.ElioPlus.Controls.Collaboration
{
    public partial class CreateNewInvitationToPartnersMessage : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        #region Page ViewState properties

        public int InvitationId1
        {
            get
            {
                return (this.ViewState["InvitationId"] != null) ? Convert.ToInt32(ViewState["InvitationId"]) : -1;
            }

            set { this.ViewState["InvitationId"] = value; }
        }

        public string InvitationSbj1
        {
            get
            {
                return (this.ViewState["InvitationSbj"] != null) ? ViewState["InvitationSbj"].ToString() : null;
            }

            set { this.ViewState["InvitationSbj"] = value; }
        }

        public string InvitationMsg1
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
                    if (!IsPostBack)
                    {
                        UpdateStrings();
                        FixPage();

                        RdgMyInvitations.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect(vSession.Page = ControlLoader.Default(), false);
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
            //ImgBtnSave.ToolTip = "Save changes of this Invitation";
        }

        private void FixPage()
        {
            ResetFields();
            //int isDefault = 1;
            //BtnLoadDefaultTemplate.Visible = SqlCollaboration.ExistDefaultCollaborationUserInvitationById(isDefault, session);
        }

        private void ResetFields()
        {
            divGeneralSuccess.Visible = false;
            divGeneralFailure.Visible = false;
            divGeneralInfo.Visible = false;

            LblFailure.Text = string.Empty;
            LblSuccess.Text = string.Empty;
            LblInfo.Text = string.Empty;
            LblGeneralFailure.Text = string.Empty;
            LblGeneralSuccess.Text = string.Empty;
            LblGeneralInfo.Text = string.Empty;

            HdnInvitationId.Value = "0";
            //ImgStatus.Visible = false;
            //ImgBtnSave.Visible = false;
        }

        #endregion

        #region Buttons

        protected void BtnProccedMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                #region Reset Fields & Lists

                ResetFields();
                List<string> validCollaborationPartnersEmails = new List<string>();
                List<ElioUsers> thirdPartyOrCollabUsers = new List<ElioUsers>();
                List<ElioUsers> notFullRegisteredUsers = new List<ElioUsers>();
                List<ElioUsers> fullRegisteredUsers = new List<ElioUsers>();

                #endregion

                if (vSession.User != null)
                {
                    #region Validation

                    #region to delete

                    //if (divCustomInvitation.Visible)
                    //{
                    //if (TbxSubject.Text == string.Empty)
                    //{
                    //    divGeneralFailure.Visible = true;
                    //    LblFailure.Text = "Please add Subject";
                    //    return;
                    //}

                    //if (TbxMsg.Text == string.Empty)
                    //{
                    //    divGeneralFailure.Visible = true;
                    //    LblFailure.Text = "Please add Content";
                    //    return;
                    //}
                    //}
                    //else
                    //{
                    //    ElioCollaborationUsersInvitations defaultInvitation = SqlCollaboration.GetUserInvitationById(1, session);

                    //    if (defaultInvitation != null)
                    //    {
                    //        HdnInvitationId.Value = defaultInvitation.Id.ToString();
                    //        TbxSubject.Text = defaultInvitation.InvSubject.Replace("{CompanyName}", vSession.User.CompanyName);
                    //        TbxMsg.Text = defaultInvitation.InvContent.Replace("{CompanyName}", vSession.User.CompanyName);
                    //    }
                    //}

                    #endregion

                    if (TbxPartnerEmailAddress.Text == string.Empty)
                    {
                        divGeneralFailure.Visible = true;
                        LblFailure.Text = "Please enter Partner email address";
                        return;
                    }
                    else
                    {
                        #region Email Validations

                        List<string> emails = new List<string>();

                        emails = TbxPartnerEmailAddress.Text.Trim().Split(',').ToList();

                        if (emails.Count > 25)
                        {
                            divGeneralFailure.Visible = true;
                            LblFailure.Text = "You can add up to 25 email addresses";
                            return;
                        }
                        else
                        {
                            foreach (string email in emails)
                            {
                                if (email != "")
                                {
                                    if (!Validations.IsValidEmail(email))
                                    {
                                        divGeneralFailure.Visible = true;
                                        LblFailure.Text = "Please enter a valid email address";
                                        return;
                                    }
                                    else
                                    {
                                        if (vSession.User.Email == email || (!string.IsNullOrEmpty(vSession.User.OfficialEmail) && vSession.User.OfficialEmail == email))
                                        {
                                            divGeneralFailure.Visible = true;
                                            LblFailure.Text = "This email/official address belongs to you. Plase try another email";
                                            return;
                                        }

                                        int userApplicationType = (int)UserApplicationType.NotRegistered;
                                        bool isFullRegistered = false;

                                        bool hasAlreadyGetInvitation = SqlCollaboration.HasInvitationRequestThisEmailBySpecificUser(vSession.User.Id, email, session);
                                        if (hasAlreadyGetInvitation)
                                        {
                                            divGeneralFailure.Visible = true;
                                            LblFailure.Text = string.Format("You have already send invitation to this email ({0})", email);
                                            return;
                                        }

                                        bool isRegisteredEmail = Sql.IsRegisteredEmail(email, out userApplicationType, out isFullRegistered, session);

                                        if (!isRegisteredEmail)
                                        {
                                            validCollaborationPartnersEmails.Add(email);
                                        }
                                        else
                                        {
                                            if (isFullRegistered)
                                            {
                                                if (userApplicationType == (int)UserApplicationType.Elioplus)
                                                {
                                                    ElioUsers fullRegisteredUser = Sql.GetUserByEmail(email, session);

                                                    if (fullRegisteredUser != null)
                                                    {
                                                        if (fullRegisteredUser.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                                            fullRegisteredUsers.Add(fullRegisteredUser);
                                                    }
                                                    //divGeneralFailure.Visible = true;
                                                    //LblFailure.Text = string.Format("Email {0} is allready registered. Try another or contact with Elioplus support team", email);
                                                    //return;
                                                }
                                            }
                                            else
                                            {
                                                if (userApplicationType == (int)UserApplicationType.Elioplus)
                                                {
                                                    ElioUsers notFullRegisteredUser = Sql.GetUserByEmail(email, session);

                                                    if (notFullRegisteredUser != null)
                                                    {
                                                        notFullRegisteredUsers.Add(notFullRegisteredUser);
                                                    }
                                                }
                                                else        //if (userApplicationType == (int)UserApplicationType.ThirdParty)
                                                {
                                                    ElioUsers thirdPartyOrCollabUser = Sql.GetUserByEmail(email, session);

                                                    if (thirdPartyOrCollabUser != null)
                                                    {
                                                        thirdPartyOrCollabUsers.Add(thirdPartyOrCollabUser);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                    }

                    #endregion

                    if (validCollaborationPartnersEmails.Count == 0 && thirdPartyOrCollabUsers.Count == 0 && notFullRegisteredUsers.Count == 0 && fullRegisteredUsers.Count == 0)
                    {
                        divGeneralInfo.Visible = true;
                        LblGeneralInfo.Text = "Info! ";
                        LblInfo.Text = "No invitation will be send";

                        return;
                    }
                    else
                    {
                        ElioCollaborationUsersInvitations defaultInvitation = SqlCollaboration.GetUserInvitationById(1, session);

                        if (defaultInvitation != null)
                        {
                            HdnInvitationId.Value = defaultInvitation.Id.ToString();
                            TbxSubject.Text = defaultInvitation.InvSubject.Replace("{CompanyName}", vSession.User.CompanyName);
                            TbxMsg.Text = defaultInvitation.InvContent.Replace("{CompanyName}", vSession.User.CompanyName);
                        }
                        else
                        {
                            divGeneralFailure.Visible = true;
                            LblGeneralFailure.Text = "Error! ";
                            LblFailure.Text = "We are sorry. Something went wrong and your invitation could not be delivered. Please try again later or contact with Elioplus support team.";

                            Logger.DetailedError(Request.Url.ToString(), "PAGE ERROR: CreateNewInvitationToPartnersMessage.ascx", string.Format("User {0} tried to send invitation email at {1}, but default collaboration Invitation could not be loaded, so email could not be sent.", vSession.User.Id.ToString(), DateTime.Now.ToString()));
                            return;
                        }
                    }

                    try
                    {
                        session.BeginTransaction();

                        #region to delete

                        //ElioCollaborationUsersInvitations invitation = null;

                        //DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);

                        //if (HdnInvitationId.Value != "0")
                        //{
                        //    invitation = SqlCollaboration.GetUserInvitationById(Convert.ToInt32(HdnInvitationId.Value), session);
                        //    if (invitation != null)
                        //    {
                        //        invitation.InvSubject = TbxSubject.Text;
                        //        invitation.InvContent = TbxMsg.Text;
                        //        invitation.LastUpdated = DateTime.Now;

                        //        loader.Update(invitation);
                        //    }
                        //    else
                        //    {
                        //        invitation = new ElioCollaborationUsersInvitations();

                        //        invitation.UserId = vSession.User.Id;
                        //        invitation.InvSubject = TbxSubject.Text;
                        //        invitation.InvContent = TbxMsg.Text;
                        //        invitation.DateCreated = DateTime.Now;
                        //        invitation.LastUpdated = DateTime.Now;
                        //        invitation.IsPublic = 1;

                        //        loader.Insert(invitation);
                        //    }
                        //}
                        //else
                        //{

                        #endregion

                        #region Add User Invitation

                        ElioCollaborationUsersInvitations invitation = new ElioCollaborationUsersInvitations();

                        invitation.UserId = vSession.User.Id;
                        invitation.InvSubject = TbxSubject.Text;
                        invitation.InvContent = TbxMsg.Text;
                        invitation.DateCreated = DateTime.Now;
                        invitation.LastUpdated = DateTime.Now;
                        invitation.IsPublic = 1;

                        DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                        loader.Insert(invitation);

                        #endregion

                        foreach (string partnerEmail in validCollaborationPartnersEmails)
                        {
                            if (vSession.User.CompanyType == Types.Vendors.ToString())
                            {
                                #region Add Collaboration Partners Emails

                                int collaborationId = SqlCollaboration.GetCollaborationIdFromPartnerInvitation(vSession.User.Id, partnerEmail, session);

                                ElioUsers collaborationUser = new ElioUsers();
                                ElioUsers collabUserFromClearbit = new ElioUsers();

                                if (collaborationId < 0)
                                {
                                    #region Get Data from Clearbit

                                    if (partnerEmail != "")
                                    {
                                        collabUserFromClearbit = Lib.Services.EnrichmentAPI.ClearBit.GetCombinedPersonCompanyByEmail_v2(collabUserFromClearbit);
                                    }

                                    #endregion

                                    #region Insert New Collaboration User

                                    collaborationUser.CompanyType = (vSession.User.CompanyType == Types.Vendors.ToString()) ? EnumHelper.GetDescription(Types.Resellers).ToString() : EnumHelper.GetDescription(Types.Resellers).ToString();
                                    collaborationUser.CompanyName = (collabUserFromClearbit != null && !string.IsNullOrEmpty(collabUserFromClearbit.CompanyName)) ? collabUserFromClearbit.CompanyName : "Add Your Company Name";
                                    collaborationUser.Email = partnerEmail.Trim();
                                    collaborationUser.GuId = Guid.NewGuid().ToString();
                                    collaborationUser.CompanyLogo = (collabUserFromClearbit != null && !string.IsNullOrEmpty(collabUserFromClearbit.CompanyLogo)) ? collabUserFromClearbit.CompanyLogo : "~/Images/CollaborationTool/collabor_1.png";
                                    collaborationUser.PersonalImage = (collabUserFromClearbit != null && !string.IsNullOrEmpty(collabUserFromClearbit.PersonalImage)) ? collabUserFromClearbit.PersonalImage : string.Empty;
                                    collaborationUser.WebSite = (collabUserFromClearbit != null && !string.IsNullOrEmpty(collabUserFromClearbit.WebSite)) ? collabUserFromClearbit.WebSite : string.Empty;
                                    collaborationUser.Address = (collabUserFromClearbit != null && !string.IsNullOrEmpty(collabUserFromClearbit.Address)) ? collabUserFromClearbit.Address : "Add Your Company Address";
                                    collaborationUser.Country = (collabUserFromClearbit != null && !string.IsNullOrEmpty(collabUserFromClearbit.Country)) ? collabUserFromClearbit.Country : "Add Your Company Country";
                                    collaborationUser.Phone = (collabUserFromClearbit != null && !string.IsNullOrEmpty(collabUserFromClearbit.Phone)) ? collabUserFromClearbit.Phone : string.Empty;
                                    collaborationUser.LinkedInUrl = (collabUserFromClearbit != null && !string.IsNullOrEmpty(collabUserFromClearbit.LinkedInUrl)) ? collabUserFromClearbit.LinkedInUrl : string.Empty;
                                    collaborationUser.TwitterUrl = (collabUserFromClearbit != null && !string.IsNullOrEmpty(collabUserFromClearbit.TwitterUrl)) ? collabUserFromClearbit.TwitterUrl : string.Empty;
                                    collaborationUser.Ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
                                    collaborationUser.SysDate = DateTime.Now;
                                    collaborationUser.LastUpdated = DateTime.Now;
                                    collaborationUser.UserApplicationType = Convert.ToInt32(UserApplicationType.Collaboration);
                                    collaborationUser.Overview = (collabUserFromClearbit != null && !string.IsNullOrEmpty(collabUserFromClearbit.Overview)) ? collabUserFromClearbit.Overview : string.Empty;
                                    collaborationUser.Description = (collabUserFromClearbit != null && !string.IsNullOrEmpty(collabUserFromClearbit.Description)) ? collabUserFromClearbit.Description : string.Empty;
                                    collaborationUser.MashapeUsername = string.Empty;
                                    collaborationUser.AccountStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                                    collaborationUser.IsPublic = 0;
                                    collaborationUser.CommunityStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                                    collaborationUser.CommunityProfileCreated = DateTime.Now;
                                    collaborationUser.CommunityProfileLastUpdated = DateTime.Now;
                                    collaborationUser.HasBillingDetails = 0;
                                    collaborationUser.BillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
                                    collaborationUser.Username = partnerEmail.Trim();
                                    collaborationUser.UsernameEncrypted = MD5.Encrypt(collaborationUser.Username);
                                    collaborationUser.Password = GeneratePasswordLib.CreateRandomStringWithMax11Chars(10);
                                    collaborationUser.PasswordEncrypted = MD5.Encrypt(collaborationUser.Password);

                                    collaborationUser = GlobalDBMethods.InsertNewUser(collaborationUser, session);

                                    #endregion

                                    #region Insert User Email Notifications Settings

                                    GlobalDBMethods.FixUserEmailNotificationsSettingsData(collaborationUser, session);

                                    #endregion

                                    #region Insert Collaboration Vendor/Reseller

                                    ElioCollaborationVendorsResellers newCollaboration = new ElioCollaborationVendorsResellers();

                                    int masterUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : collaborationUser.Id;
                                    int partnerUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? collaborationUser.Id : vSession.User.Id;

                                    newCollaboration.MasterUserId = masterUserId;
                                    newCollaboration.PartnerUserId = partnerUserId;
                                    newCollaboration.IsActive = 1;
                                    newCollaboration.InvitationStatus = CollaborateInvitationStatus.Pending.ToString();
                                    newCollaboration.Sysdate = DateTime.Now;
                                    newCollaboration.LastUpdated = DateTime.Now;

                                    DataLoader<ElioCollaborationVendorsResellers> loader2 = new DataLoader<ElioCollaborationVendorsResellers>(session);
                                    loader2.Insert(newCollaboration);

                                    #endregion

                                    #region Insert V/R Invitation

                                    ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                                    vendorResselerInvitation.UserId = vSession.User.Id;
                                    vendorResselerInvitation.VendorResellerId = newCollaboration.Id;
                                    vendorResselerInvitation.UserInvitationId = invitation.Id;
                                    vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                                    vendorResselerInvitation.RecipientEmail = partnerEmail;
                                    vendorResselerInvitation.SendDate = DateTime.Now;
                                    vendorResselerInvitation.LastUpdated = DateTime.Now;

                                    DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                    loader3.Insert(vendorResselerInvitation);

                                    #endregion
                                }
                                else
                                {
                                    #region Insert V/R Invitation

                                    bool existInvitation = SqlCollaboration.HasInvitationRequestThisEmailBySpecificUser(vSession.User.Id, collaborationUser.Email, session);

                                    if (!existInvitation)
                                    {
                                        ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                                        vendorResselerInvitation.UserId = vSession.User.Id;
                                        vendorResselerInvitation.VendorResellerId = collaborationId;
                                        vendorResselerInvitation.UserInvitationId = invitation.Id;
                                        vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                                        vendorResselerInvitation.RecipientEmail = partnerEmail;
                                        vendorResselerInvitation.SendDate = DateTime.Now;
                                        vendorResselerInvitation.LastUpdated = DateTime.Now;

                                        DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                        loader3.Insert(vendorResselerInvitation);
                                    }

                                    #endregion
                                }

                                try
                                {
                                    #region Send Invitation Email

                                    //maybe asynchronously send emails, to do

                                    #region Previous Link Collaboration Email

                                    //string confirmationLink = FileHelper.AddToPhysicalRootPath(Request) + "/free-sign-up?verificationViewID=" + collaborationUser.GuId + "&type=" + collaborationUser.UserApplicationType.ToString();
                                    //EmailSenderLib.CollaborationInvitationEmail(collaborationUser.UserApplicationType, partnerEmail.Trim(), vSession.User.CompanyName, collaborationUser.CompanyName, confirmationLink, vSession.Lang, session);

                                    #endregion

                                    #region New Link Collaboration Email (Partner Portal Sign Up Page)

                                    string partnerPortalSignUpLink = FileHelper.AddToPhysicalRootPath(Request) + ControlLoader.SignUpPartner.Replace("{CompanyName}", Regex.Replace(vSession.User.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower()) + "?verificationViewID=" + collaborationUser.GuId + "&type=" + collaborationUser.UserApplicationType.ToString();
                                    EmailSenderLib.CollaborationInvitationEmail(collaborationUser.UserApplicationType, partnerEmail.Trim(), vSession.User.CompanyName, vSession.User.CompanyLogo, collaborationUser.CompanyName, partnerPortalSignUpLink, vSession.Lang, session);

                                    #endregion

                                    ////EmailSenderLib.CollaborationInvitationEmailWithUserText(collaborationUser.UserApplicationType, partnerEmail.Trim(), vSession.User.CompanyName, confirmationLink, TbxSubject.Text, TbxMsg.Text, vSession.Lang, session);

                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    throw ex;
                                }

                                #endregion
                            }
                        }

                        foreach (ElioUsers thirdPartyOrCollabUsr in thirdPartyOrCollabUsers)
                        {
                            #region Add Collaboration Third Party Partners Emails

                            int collaborationId = SqlCollaboration.GetCollaborationIdFromPartnerInvitation(vSession.User.Id, thirdPartyOrCollabUsr.Email.Trim(), session);

                            if (collaborationId < 0)
                            {
                                ElioCollaborationVendorsResellers newCollaboration = new ElioCollaborationVendorsResellers();

                                int masterUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : thirdPartyOrCollabUsr.Id;
                                int partnerUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? thirdPartyOrCollabUsr.Id : vSession.User.Id;

                                newCollaboration.MasterUserId = masterUserId;
                                newCollaboration.PartnerUserId = partnerUserId;
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
                                vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                                vendorResselerInvitation.RecipientEmail = thirdPartyOrCollabUsr.Email;
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
                                vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                                vendorResselerInvitation.RecipientEmail = thirdPartyOrCollabUsr.Email.Trim();
                                vendorResselerInvitation.SendDate = DateTime.Now;
                                vendorResselerInvitation.LastUpdated = DateTime.Now;

                                DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                loader3.Insert(vendorResselerInvitation);
                            }

                            try
                            {
                                //maybe asynchronously send emails, to do

                                #region Previous Link Collaboration Email

                                //string confirmationLink = FileHelper.AddToPhysicalRootPath(Request) + "/free-sign-up?verificationViewID=" + thirdPartyUsr.GuId + "&type=" + thirdPartyUsr.UserApplicationType.ToString();
                                //EmailSenderLib.CollaborationInvitationEmailWithUserText(thirdPartyUsr.UserApplicationType, thirdPartyUsr.Email.Trim(), vSession.User.CompanyName, confirmationLink, "", "", vSession.Lang, session);

                                #endregion

                                #region New Link Collaboration Email (Partner Portal Sign Up Page)

                                string partnerPortalSignUpLink = FileHelper.AddToPhysicalRootPath(Request) + ControlLoader.SignUpPartner.Replace("{CompanyName}", Regex.Replace(vSession.User.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower()) + "?verificationViewID=" + thirdPartyOrCollabUsr.GuId + "&type=" + thirdPartyOrCollabUsr.UserApplicationType.ToString();
                                EmailSenderLib.CollaborationInvitationEmailWithUserText(thirdPartyOrCollabUsr.UserApplicationType, thirdPartyOrCollabUsr.Email.Trim(), vSession.User.CompanyName, partnerPortalSignUpLink, "", "", vSession.Lang, session);

                                #endregion
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                throw ex;
                            }

                            #endregion
                        }

                        foreach (ElioUsers notFullRegisteredUser in notFullRegisteredUsers)
                        {
                            #region Add Collaboration Not Full Registered Partners Emails

                            int collaborationId = SqlCollaboration.GetCollaborationIdFromPartnerInvitation(vSession.User.Id, notFullRegisteredUser.Email, session);

                            if (collaborationId < 0)
                            {
                                ElioCollaborationVendorsResellers newCollaboration = new ElioCollaborationVendorsResellers();

                                int masterUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : notFullRegisteredUser.Id;
                                int partnerUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? notFullRegisteredUser.Id : vSession.User.Id;

                                newCollaboration.MasterUserId = masterUserId;
                                newCollaboration.PartnerUserId = partnerUserId;
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
                                vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                                vendorResselerInvitation.RecipientEmail = notFullRegisteredUser.Email;
                                vendorResselerInvitation.SendDate = DateTime.Now;
                                vendorResselerInvitation.LastUpdated = DateTime.Now;

                                DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                loader3.Insert(vendorResselerInvitation);
                            }

                            try
                            {
                                EmailSenderLib.SendCollaborationInvitationEmailForNotFullRegisteredUsers(vSession.User.CompanyName, notFullRegisteredUser.Email, vSession.LoggedInSubAccountRoleID > 0, vSession.Lang, session);
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                throw ex;
                            }

                            #endregion
                        }

                        foreach (ElioUsers fullRegisteredUser in fullRegisteredUsers)
                        {
                            #region Add Collaboration Full Registered Partners Emails

                            int collaborationId = SqlCollaboration.GetCollaborationIdFromPartnerInvitation(vSession.User.Id, fullRegisteredUser.Email, session);

                            if (collaborationId < 0)
                            {
                                ElioCollaborationVendorsResellers newCollaboration = new ElioCollaborationVendorsResellers();

                                int masterUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : fullRegisteredUser.Id;
                                int partnerUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? fullRegisteredUser.Id : vSession.User.Id;

                                newCollaboration.MasterUserId = masterUserId;
                                newCollaboration.PartnerUserId = partnerUserId;
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
                                vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                                vendorResselerInvitation.RecipientEmail = fullRegisteredUser.Email;
                                vendorResselerInvitation.SendDate = DateTime.Now;
                                vendorResselerInvitation.LastUpdated = DateTime.Now;

                                DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                loader3.Insert(vendorResselerInvitation);
                            }

                            try
                            {
                                //EmailSenderLib.CollaborationInvitationEmail(vSession.User.CompanyName, notFullRegisteredUser.Email, vSession.Lang, session);

                                //string partnerPortalSignUpLink = FileHelper.AddToPhysicalRootPath(Request) + ControlLoader.SignUpPartner.Replace("{CompanyName}", Regex.Replace(vSession.User.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower());
                                EmailSenderLib.CollaborationInvitationEmail(fullRegisteredUser.UserApplicationType, fullRegisteredUser.Email.Trim(), vSession.User.CompanyName, vSession.User.CompanyLogo, fullRegisteredUser.CompanyName, "", vSession.Lang, session);
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                throw ex;
                            }

                            #endregion
                        }

                        session.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();
                        //Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        throw ex;

                        #region Failure Procedure

                        //divGeneralSuccess.Visible = false;
                        //divGeneralFailure.Visible = true;
                        //LblGeneralFailure.Text = "Error! ";
                        //LblFailure.Text = "We are sorry. Something went wrong and your invitation could not be delivered. Please try again later or contact with Elioplus support team.";

                        #endregion
                    }

                    #region Successfull Procedure

                    divGeneralFailure.Visible = false;
                    divGeneralSuccess.Visible = true;
                    LblGeneralSuccess.Text = "Done! ";
                    LblSuccess.Text = "Successfully sent";

                    TbxSubject.Text = string.Empty;
                    TbxMsg.Text = string.Empty;
                    TbxPartnerEmailAddress.Text = string.Empty;
                    HdnInvitationId.Value = "0";

                    RadGrid rdgResellers = (RadGrid)ControlFinder.FindControlBackWards(this, "RdgResellers");
                    if (rdgResellers != null)
                    {
                        rdgResellers.Rebind();
                        UpdatePanel updatePnl = (UpdatePanel)ControlFinder.FindControlBackWards(this, "UpdatePnl");
                        updatePnl.Update();
                    }

                    #endregion
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                #region Failure Procedure

                divGeneralSuccess.Visible = false;
                divGeneralFailure.Visible = true;
                LblGeneralFailure.Text = "Error! ";
                LblFailure.Text = "We are sorry. Something went wrong and your invitation could not be delivered. Please try again later or contact with Elioplus support team.";

                #endregion

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        private void FixCollaborationUser(bool isNewRegistration, List<string> validCollaborationPartnersEmails, ElioUsers thirdPartyUser)
        {
            ElioCollaborationUsersInvitations invitation = null;

            DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);

            if (HdnInvitationId.Value != "0")
            {
                invitation = SqlCollaboration.GetUserInvitationById(Convert.ToInt32(HdnInvitationId.Value), session);
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

            if (isNewRegistration)
            {
                try
                {
                    session.BeginTransaction();

                    foreach (string partnerEmail in validCollaborationPartnersEmails)
                    {
                        int collaborationId = SqlCollaboration.GetCollaborationIdFromPartnerInvitation(vSession.User.Id, partnerEmail, session);

                        ElioUsers collaborationUser = new ElioUsers();

                        if (collaborationId < 0)
                        {
                            collaborationUser.CompanyType = EnumHelper.GetDescription(Types.Resellers).ToString();
                            collaborationUser.CompanyName = "Collaboration Company Name";
                            collaborationUser.Email = partnerEmail;
                            collaborationUser.GuId = Guid.NewGuid().ToString();
                            collaborationUser.CompanyLogo = "~/Images/CollaborationTool/collabor_1.png";
                            collaborationUser.PersonalImage = string.Empty;
                            collaborationUser.WebSite = string.Empty;
                            collaborationUser.Address = "Collaboration Company Address";
                            collaborationUser.Country = "Collaboration Company Country";
                            collaborationUser.Phone = string.Empty;
                            collaborationUser.LinkedInUrl = string.Empty;
                            collaborationUser.TwitterUrl = string.Empty;
                            collaborationUser.Ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
                            collaborationUser.SysDate = DateTime.Now;
                            collaborationUser.LastUpdated = DateTime.Now;
                            collaborationUser.UserApplicationType = Convert.ToInt32(UserApplicationType.Collaboration);
                            collaborationUser.Overview = string.Empty;
                            collaborationUser.Description = string.Empty;
                            collaborationUser.MashapeUsername = string.Empty;
                            collaborationUser.AccountStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                            collaborationUser.IsPublic = 0;
                            collaborationUser.CommunityStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                            collaborationUser.CommunityProfileCreated = DateTime.Now;
                            collaborationUser.CommunityProfileLastUpdated = DateTime.Now;
                            collaborationUser.HasBillingDetails = 0;
                            collaborationUser.BillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
                            collaborationUser.Username = partnerEmail;
                            collaborationUser.UsernameEncrypted = MD5.Encrypt(collaborationUser.Username);
                            collaborationUser.Password = GeneratePasswordLib.CreateRandomStringWithMax11Chars(10);
                            collaborationUser.PasswordEncrypted = MD5.Encrypt(collaborationUser.Password);

                            collaborationUser = GlobalDBMethods.InsertNewUser(collaborationUser, session);

                            #region Insert User Email Notifications Settings

                            GlobalDBMethods.FixUserEmailNotificationsSettingsData(collaborationUser, session);

                            #endregion

                            ElioCollaborationVendorsResellers newCollaboration = new ElioCollaborationVendorsResellers();

                            int masterUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : collaborationUser.Id;
                            int partnerUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? collaborationUser.Id : vSession.User.Id;

                            newCollaboration.MasterUserId = masterUserId;
                            newCollaboration.PartnerUserId = partnerUserId;
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
                            vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                            vendorResselerInvitation.RecipientEmail = partnerEmail;
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
                            vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                            vendorResselerInvitation.RecipientEmail = partnerEmail;
                            vendorResselerInvitation.SendDate = DateTime.Now;
                            vendorResselerInvitation.LastUpdated = DateTime.Now;

                            DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                            loader3.Insert(vendorResselerInvitation);
                        }

                        session.CommitTransaction();

                        try
                        {
                            //maybe asynchronously send emails, to do
                            string confirmationLink = FileHelper.AddToPhysicalRootPath(Request) + "/free-sign-up?verificationViewID=" + collaborationUser.GuId + "&type=" + collaborationUser.UserApplicationType.ToString();

                            EmailSenderLib.CollaborationInvitationEmail(collaborationUser.UserApplicationType, partnerEmail, vSession.User.CompanyName, vSession.User.CompanyLogo, collaborationUser.CompanyName, confirmationLink, vSession.Lang, session);
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                }
            }
            else
            {
                try
                {
                    session.BeginTransaction();

                    int collaborationId = SqlCollaboration.GetCollaborationIdFromPartnerInvitation(vSession.User.Id, thirdPartyUser.Email, session);

                    if (collaborationId < 0)
                    {
                        ElioCollaborationVendorsResellers newCollaboration = new ElioCollaborationVendorsResellers();

                        int masterUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : thirdPartyUser.Id;
                        int partnerUserId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? thirdPartyUser.Id : vSession.User.Id;

                        newCollaboration.MasterUserId = masterUserId;
                        newCollaboration.PartnerUserId = partnerUserId;
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
                        vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                        vendorResselerInvitation.RecipientEmail = thirdPartyUser.Email;
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
                        vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                        vendorResselerInvitation.RecipientEmail = thirdPartyUser.Email;
                        vendorResselerInvitation.SendDate = DateTime.Now;
                        vendorResselerInvitation.LastUpdated = DateTime.Now;

                        DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                        loader3.Insert(vendorResselerInvitation);
                    }

                    session.CommitTransaction();

                    try
                    {
                        //maybe asynchronously send emails, to do
                        string confirmationLink = FileHelper.AddToPhysicalRootPath(Request) + "/free-sign-up?verificationViewID=" + thirdPartyUser.GuId + "&type=" + thirdPartyUser.UserApplicationType.ToString();

                        EmailSenderLib.CollaborationInvitationEmail(thirdPartyUser.UserApplicationType, thirdPartyUser.Email, vSession.User.CompanyName, vSession.User.CompanyLogo, thirdPartyUser.CompanyName, confirmationLink, vSession.Lang, session);
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
        }

        protected void BtnCancelMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields();

                TbxSubject.Text = string.Empty;
                TbxMsg.Text = string.Empty;
                TbxPartnerEmailAddress.Text = string.Empty;

                TbxSubject.ReadOnly = false;
                TbxMsg.ReadOnly = false;

                divCustomInvitation.Visible = false;
                BtnLoadDefaultTemplate.Text = "Create custom invitation";
                //ImgBtnCancel.Visible = false;
                //ImgBtnEdit.Visible = false;

                //InvitationSbj = null;
                //InvitationMsg = null;
                //ViewState["InvitationId"] = null;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "ClosePartnersInvitationPopUp();", true);
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
                TbxPartnerEmailAddress.Text = string.Empty;

                //ImgBtnEdit.Visible = false;
                //ImgBtnCancel.Visible = false;
                
                //TbxSubject.ReadOnly = false;
                //TbxMsg.ReadOnly = false;

                //InvitationSbj = null;
                //InvitationMsg = null;
                //ViewState["InvitationId"] = null;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnLoadDefaultTemplate_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ResetFields();

                    TbxSubject.Text = string.Empty;
                    HdnInvitationId.Value = "0";

                    ElioCollaborationUsersInvitations defaultInvitation = SqlCollaboration.GetUserInvitationById(1, session);

                    if (defaultInvitation != null)
                    {
                        HdnInvitationId.Value = defaultInvitation.Id.ToString();
                        TbxSubject.Text = defaultInvitation.InvSubject.Replace("{CompanyName}", vSession.User.CompanyName);
                        TbxMsg.Text = defaultInvitation.InvContent.Replace("{CompanyName}", vSession.User.CompanyName);
                    }

                    divCustomInvitation.Visible = !divCustomInvitation.Visible;
                    BtnLoadDefaultTemplate.Text = (divCustomInvitation.Visible) ? "Cancel custom invitation" : "Create custom invitation";
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

                //ImgBtnEdit.Visible = false;
                //ImgBtnCancel.Visible = false;

                //InvitationSbj = null;
                //InvitationMsg = null;
                //ViewState["InvitationId"] = null;

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

                //if (InvitationId != -1)
                //{
                //    if (TbxSubject.Text == string.Empty)
                //    {
                //        divGeneralFailure.Visible = true;
                //        LblFailure.Text = "Please add Subject";

                //        return;
                //    }

                //    if (TbxMsg.Text == string.Empty)
                //    {
                //        divGeneralFailure.Visible = true;
                //        LblFailure.Text = "Please add Content";

                //        return;
                //    }

                //    ResetFields();

                //    if (TbxSubject.Text == InvitationSbj && TbxMsg.Text == InvitationMsg)
                //    {
                //        ElioCollaborationUsersInvitations invitation = SqlCollaboration.GetUserInvitationById(InvitationId, session);
                //        if (invitation != null)
                //        {
                //            invitation.LastUpdated = DateTime.Now;

                //            DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                //            loader.Update(invitation);
                            
                //            LblSuccess.Text = "Your invitation was updated successfully!";
                //        }
                //        else
                //        {
                //            divGeneralFailure.Visible = true;
                //            LblFailure.Text = "Your invitation could not updated. Please try again later!";
                //            ImgStatus.ImageUrl = "~/images/icons/small/rejected_1.png";

                //            return;
                //        }
                //    }
                //    else
                //    {
                //        ElioCollaborationUsersInvitations invitation = new ElioCollaborationUsersInvitations();

                //        invitation.InvSubject = InvitationSbj = TbxSubject.Text;
                //        invitation.InvContent = InvitationMsg = TbxMsg.Text;
                //        invitation.IsPublic = 1;
                //        invitation.UserId = vSession.User.Id;
                //        invitation.DateCreated = DateTime.Now;
                //        invitation.LastUpdated = DateTime.Now;

                //        DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                //        loader.Insert(invitation);

                //        InvitationId = invitation.Id;
    
                //        LblSuccess.Text = "Your invitation was inserted successfully!";
                //    }

                //    RdgMyInvitations.Rebind();

                //    ImgBtnSave.Visible = false;
                //    ImgBtnCancel.Visible = false;
                //    ImgBtnEdit.Visible = true;

                //    TbxSubject.ReadOnly = true;
                //    TbxMsg.ReadOnly = true;

                //    ImgStatus.Visible = true;
                //    ImgStatus.ImageUrl = "~/images/icons/small/success.png";

                //    divGeneralSuccess.Visible = true;
                //    LblGeneralSuccess.Text = "Done! ";
                //}
                //else
                //{
                //    divGeneralFailure.Visible = true;
                //    LblFailure.Text = "Your invitation could not updated. Please try again later!";
                //    ImgStatus.ImageUrl = "~/images/icons/small/rejected_2.png";
                //}
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

                //if (InvitationId != -1)
                //{
                //    TbxSubject.ReadOnly = false;
                //    TbxMsg.ReadOnly = false;

                //    ImgBtnEdit.Visible = false;
                //    ImgBtnSave.Visible = true;
                //    ImgBtnCancel.Visible = true;
                //}
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

                //if (InvitationId != -1)
                //{
                //    TbxSubject.Text = InvitationSbj;
                //    TbxMsg.Text = InvitationMsg;

                //    TbxSubject.ReadOnly = true;
                //    TbxMsg.ReadOnly = true;

                //    ImgBtnSave.Visible = false;
                //    ImgBtnCancel.Visible = false;
                //    ImgBtnEdit.Visible = true;
                //}
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

        protected void BtnImportCsv_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    TbxPartnerEmailAddress.Text = "";
                    divGeneralSuccess.Visible = false;
                    divGeneralFailure.Visible = false;

                    if (ConfigurationManager.AppSettings["EmailsFromFileTargetFolder"] != null)
                    {
                        string inputFileTargetPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["EmailsFromFileTargetFolder"].ToString() + "\\" + vSession.User.GuId + "\\");
                        string referError = "";
                        string emailsFromFile = GlobalMethods.ImportEmailsFromCSV(inputFile, inputFileTargetPath, session, out referError);

                        if (referError == "" && emailsFromFile != "" && emailsFromFile.Length > 0)
                        {
                            TbxPartnerEmailAddress.Text = emailsFromFile;
                        }
                        else
                        {
                            divGeneralFailure.Visible = true;
                            LblFailure.Text = referError;
                        }
                    }
                    else
                    {
                        divGeneralFailure.Visible = true;
                        LblFailure.Text = "We're sorry but file can not be uploaded.Please try again later";

                        Logger.DetailedError(Request.Url.ToString(), "ERROR --> CreateNewInvitationToPartnersMessage.aspx", string.Format("User {0} tried to upload an read emails from .csv file but target folder(EmailsFromCSVTargetFileFolder) could not be found at {1}", vSession.User.Id.ToString(), DateTime.Now));
                    }

                    UpdatePanel1.Update();

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenPartnersInvitationPopUp();", true);
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

                        //RdgMyInvitations.DataSource = table;
                    }
                    else
                    {
                        RdgMyInvitations.Visible = false;

                        divInvitations.Visible = false;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, "You have no Invitations yet", MessageTypes.Info, true, true, false);
                    }
                }
                else
                {
                    Response.Redirect(vSession.Page = ControlLoader.Login, false);
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