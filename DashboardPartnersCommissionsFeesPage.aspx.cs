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
using Stripe;

namespace WdS.ElioPlus
{
    public partial class DashboardPartnersCommissionsFeesPage : System.Web.UI.Page
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
                UcSendMessageAlertConfirmed.Visible = false;

                UpdateStrings();
                SetLinks();
            }

            divSuccess.Visible = false;
            LblSuccessMsg.Text = "";

            divFailure.Visible = false;
            LblFailureMsg.Text = "";

            if (vSession.User.CompanyType != EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                
            }
            else
            {
                
            }
        }

        private void SetLinks()
        {
            aCommissionBillingDetails.HRef = ControlLoader.Dashboard(vSession.User, "partner-commissions-billing");
            aCommissionFeesTerms.HRef = ControlLoader.Dashboard(vSession.User, "partner-commissions-fees");
            aCommissionPayments.HRef= ControlLoader.Dashboard(vSession.User, "partner-commissions-payments");
        }

        private void UpdateStrings()
        {
            string partners = (vSession.User.CompanyType == Types.Vendors.ToString()) ? EnumHelper.GetDescription(Types.Resellers).ToString() : Types.Vendors.ToString();
        }


        private void ShowPopUpModalWithText(string title, string content)
        {
            LblInvitationSendTitle.Text = title;
            LblSuccessfullSendfMsg.Text = content;

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenSendInvitationPopUp();", true);
        }

        private void ShowPopUpModalAlert(string title, string content, MessageTypes type)
        {
            LblMessageTitle.Text = title;
            GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, content, type, true, true, true, false, false);

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAssignPartnerPopUp();", true);
        }

        # endregion

        #region Grids

        protected void RdgCommisions_OnItemDataBound(object sender, GridItemEventArgs e)
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

                        Label lblMoreDetails = (Label)ControlFinder.FindControlRecursive(item, "LblMoreDetails");
                        lblMoreDetails.Text = "Manage partner";

                        HtmlAnchor aAssignPartner = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aAssignPartner");

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            
                        }
                        else
                            aAssignPartner.Visible = false;
                    }

                    #endregion
                }
                else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "VendorsResellersUsersInvitations")
                {
                    
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

        protected void RdgCommisions_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable table = Sql.GetCurrenciesCountriesForTransfers(session);

                    if (table != null && table.Rows.Count > 0)
                    {
                        RdgCommisions.Visible = true;
                        UcSendMessageAlertConfirmed.Visible = false;

                        RdgCommisions.DataSource = table;
                    }
                    else
                    {
                        RdgCommisions.Visible = false;
                        GlobalMethods.ShowMessageControlDA(UcSendMessageAlertConfirmed, "There are data about fees per country.", MessageTypes.Info, true, true, false, false, false);
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

        # endregion

        #region DropDownLists


        #endregion

        
    }
}