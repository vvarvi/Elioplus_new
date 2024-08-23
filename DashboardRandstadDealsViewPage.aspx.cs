using System;
using System.Collections.Generic;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.Models;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.CurrencyConverter;
using System.Globalization;
using System.Threading;

namespace WdS.ElioPlus
{
    public partial class DashboardRandstadDealsViewPage : System.Web.UI.Page
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
                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        if (vSession.User.Id != GlobalMethods.GetRandstadCustomerID())
                        {
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration-view"), false);
                            return;
                        }
                    }
                    else
                    {
                        bool isCustomPartner = SqlCollaboration.IsPartnerOfCustomVendor(GlobalMethods.GetRandstadCustomerID(), vSession.User.Id, session);
                        if (!isCustomPartner)
                        {
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration-view"), false);
                            return;
                        }
                    }

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

                    if (!IsPostBack)
                    {
                        if (Request.QueryString["dealViewID"] != null)
                        {
                            int dealId = Convert.ToInt32(Session[Request.QueryString["dealViewID"]]);
                            if (dealId > 0)
                            {
                                LoadDealResultStatus();

                                GetSelectedDealData(dealId);

                                DdlDealStatus.Enabled = false;
                                divVendorsList.Visible = false;

                                if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                                {
                                    divCrmService.Visible = false;
                                    divPartnerHeaderInfo.Visible = false;
                                    //BtnSendToCrm.Visible = RBtnSendToCrm.Visible = true;
                                }
                                //else
                                //{
                                //if (Sql.IsUserAdministrator(vSession.User.Id, session))
                                //    BtnSendToCrmVendor.Visible = RBtnSendToCrmVendor.Visible = true;
                                //}

                                UpdatePanelContent.Update();
                            }
                            else
                                Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
                        }
                        else
                        {
                            if (Request.QueryString["dealVendorViewID"] == null)
                            {
                                Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
                            }
                            else
                            {
                                TbxMonthDuration.Visible = true;
                                //DdlMonthDuration.Visible = false;
                                DdlDealStatus.Text = DealStatus.Open.ToString();
                                DdlDealStatus.Enabled = false;

                                int vendorId = Convert.ToInt32(Session[Request.QueryString["dealVendorViewID"]]);
                                if (vendorId == 0 && vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                {
                                    divVendorsList.Visible = true;
                                    divSelectedPartnerHeader.Visible = false;

                                    GetCollaborationUsers();

                                    AllowEdit(false);
                                    divCrmService.Visible = false;
                                }
                                else if (vendorId > 0)
                                {
                                    bool successLoaded = LoadPartnerData(vendorId);
                                    if (!successLoaded)
                                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);

                                    if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                                    {
                                        //divCrmService.Visible = true;
                                        divFreemiumArea.Visible = false;
                                        divPremiumArea.Visible = true;
                                    }
                                }
                                else
                                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
                            }
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

        #region Methods

        private void LoadCurrency1()
        {
            GlobalMethods.GetDrpCurrencies(DrpCurrency);

            if (DrpCurrency.Items.Count > 0)
            {
                divCurrencyArea.Visible = true;

                DrpCurrency.DataBind();
            }
            else
                divCurrencyArea.Visible = true;
        }

        private void LoadCurrency(string selectedCurID)
        {
            DrpCurrency.Items.Clear();
            //string defaultUserCurId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Sql.GetUserCurrencyID(vSession.User.Id, session) : "";

            if (selectedCurID == "0")
            {
                if (Request.QueryString["dealViewID"] != null)
                {
                    int dealId = Convert.ToInt32(Session[Request.QueryString["dealViewID"].ToString()]);

                    if (dealId > 0)
                    {
                        ElioRegistrationDeals deal = Sql.GetDealById(dealId, session);
                        if (deal != null)
                        {
                            if (!string.IsNullOrEmpty(deal.CurId))
                                selectedCurID = deal.CurId;
                            else
                            {
                                ElioUsers partner = Sql.GetUserById(deal.ResellerId, session);
                                if (partner != null)
                                {
                                    ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partner.Country, session);
                                    if (countryCurrency != null)
                                    {
                                        selectedCurID = countryCurrency.CurId;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            List<ElioCurrenciesCountries> currCountries = Sql.GetCurrenciesCountries(session);
            if (currCountries.Count > 0)
            {
                #region Get from DB

                ListItem itm = new ListItem();

                itm.Value = "NO";
                itm.Text = "Select currency";

                DrpCurrency.Items.Add(itm);

                foreach (ElioCurrenciesCountries currency in currCountries)
                {
                    itm = new ListItem();

                    itm.Value = currency.CurId;
                    itm.Text = currency.CurId + "(" + currency.CurrencySymbol + "-" + currency.CurrencyId + ")";

                    if (selectedCurID != "0" && selectedCurID == currency.CurId)
                        itm.Selected = true;
                    //else if (currency.Name == vSession.User.Country)
                    //    itm.Selected = true;

                    DrpCurrency.Items.Add(itm);
                }

                DrpCurrency.DataBind();

                #endregion
            }
            else
            {
                #region Get from API

                try
                {
                    List<Country> countryCurr = Lib.Services.CurrencyConverterAPI.CurrencyConverter.ConverterLib.GetAllCountries();
                    if (countryCurr != null && countryCurr.Count > 0)
                    {
                        countryCurr.OrderBy(x => x.Id).ToList();

                        ListItem itm = new ListItem();

                        itm.Value = "NO";
                        itm.Text = "Select currency";
                        DrpCurrency.Items.Add(itm);

                        foreach (Country currency in countryCurr)
                        {
                            itm = new ListItem();

                            itm.Value = currency.Id;
                            itm.Text = currency.Id + "(" + currency.CurrencySymbol + "-" + currency.CurrencyId + ")";

                            //if (currency.Name == vSession.User.Country)
                            //    itm.Selected = true;

                            DrpCurrency.Items.Add(itm);

                            #region Insert / Update Elio Currencies Countries

                            try
                            {
                                DataLoader<ElioCurrenciesCountries> loader = new DataLoader<ElioCurrenciesCountries>(session);

                                ElioCurrenciesCountries curCountry = Sql.GetCurrencyCountryByValues(currency.Alpha3, currency.Id, currency.CurrencyId, currency.CurrencyName, currency.Name, session);
                                if (curCountry == null)
                                {
                                    curCountry = new ElioCurrenciesCountries();

                                    curCountry.Alpha3 = currency.Alpha3;
                                    curCountry.CurId = currency.Id;
                                    curCountry.CurrencyId = currency.CurrencyId;
                                    curCountry.CurrencyName = currency.CurrencyName;
                                    curCountry.CurrencySymbol = currency.CurrencySymbol;
                                    curCountry.Name = currency.Name;
                                    curCountry.Sysdate = DateTime.Now;
                                    curCountry.LastUpdate = DateTime.Now;
                                    curCountry.IsPublic = 1;

                                    loader.Insert(curCountry);
                                }
                                else
                                {
                                    curCountry.Alpha3 = currency.Alpha3;
                                    curCountry.CurId = currency.Id;
                                    curCountry.CurrencyId = currency.CurrencyId;
                                    curCountry.CurrencyName = currency.CurrencyName;
                                    curCountry.CurrencySymbol = currency.CurrencySymbol;
                                    curCountry.Name = currency.Name;
                                    curCountry.LastUpdate = DateTime.Now;

                                    loader.Update(curCountry);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }

                            #endregion
                        }

                        DrpCurrency.DataBind();
                    }
                    else
                    {
                        divCurrencyArea.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                }

                #endregion
            }
        }

        private void FixPage()
        {
            if (!IsPostBack)
            {
                //divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                UpdateStrings();
                SetLinks();
                
                #region Top Packet Button

                //if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                //{
                //    LblRenewalHead.Visible = LblRenewal.Visible = true;
                //    LblRenewalHead.Text = "Renewal date: ";

                //    try
                //    {
                //        LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");
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

                //aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingType.Freemium) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
                //aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

                #endregion

                //divResellerActionsA.Visible = vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();
                //RBtnCheckCrmLead.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                ResetFields();

                GlobalDBMethods.FixUserOpenDealsStatus(vSession.User, session);
            }
        }

        private void AllowEdit(bool allow)
        {
            RdpExpectedClosedDate.Enabled = DdlDealResult.Enabled = allow;
            DdlIsActive.Enabled = false;
            DdlForeCasting.Enabled = allow;

            if (vSession.User.CompanyType == Types.Vendors.ToString() && Request.QueryString["dealViewID"] != null)
            {
                RdpExpectedClosedDate.Enabled = false;
                DdlDealStatus.Enabled = DdlDealResult.Enabled = false;
                DdlIsActive.Enabled = false;
            }
            else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString() && Convert.ToInt32(Session[Request.QueryString["dealVendorViewID"]]) != 0)
            {
                DdlIsActive.Enabled = false;
            }
        }

        private void SetLinks()
        {
            //aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
        }

        private void UpdateStrings()
        {
            //if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
            //    ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
            //    if (packet != null)
            //    {
            //        LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
            //    }
            //}
            //else
            //{
            //    LblPricingPlan.Text = "You are currently on a free plan";
            //}

            //LblElioplusDashboard.Text = "";

            //LblDashboard.Text = "Dashboard";

            //aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;

            //if (aBtnGoPremium.Visible)
            //{
            //    LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
            //    LblPricingPlan.Visible = false;
            //}

            //LblGoFull.Text = "Complete your registration";
            //LblDashPage.Text = "Deal Registration";
            //LblDashSubTitle.Text = "";
            LblSelectPlan.Text = "You have to select a Vendor to add deal registration to";
            LblStatus.Text = "Open/Expired Status: ";
            LblIsActive.Text = "Approved/Rejected Status: ";
            LblMonthDuration.Text = "Month Duration: ";
            LblForeCasting.Text = "Forecasting: ";
            LblDealResult.Text = "Deal Stages: ";
            LblFirstName.Text = "First Name: ";
            //LblFNameHelp.Text = "Fill in your customer first name";
            LblLastName.Text = "Last Name: ";
            //LblLNameHelp.Text = "Fill in your customer's last name";
            LblOrganiz.Text = "Organisation Name: ";
            //LblOrganizHelp.Text = "Fill in your customer's organisation/company name";
            LblAddress.Text = "Location / Address: ";
            //LblAddressHelp.Text = "Fill in your customer's location/address of the company";
            LblEmail.Text = "Business Email: ";
            //LblEmailHelp.Text = "Fill in your customer's business email";
            LblWebsite.Text = "Website: ";
            //LblWebsiteHelp.Text = "Fill in your customer's website";
            LblOpportunityDescription.Text = "Opportunity Description: ";
            //LblOpportunityDescriptionHelp.Text = "Fill in your customer's description";
            LblPhone.Text = "Phone: ";
            //LblPhoneHelp.Text = "Fill in your customer's phone";
            LblProduct.Text = "Product: ";
            //LblProductHelp.Text = "Fill in your customer's product";
            LblAmount.Text = "Amount";
            //LblAmountHelp.Text = "Fill in your deal's amount";
            LblExpectedClosedDate.Text = "Expected closed date of deal: ";
            //LblExpectedClosedDateHelp.Text = "Fill in your customer's deal expected closed date";
        }

        private void ShowUploadMessages(string content, string title, MessageTypes type)
        {
            //LblFileUploadTitle.Text = title;
            //LblFileUploadfMsg.Text = content;
            //GlobalMethods.ShowMessageControlDA(UploadMessageAlert, content, type, false, true, false);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
        }

        private void ResetPanelItems()
        {

        }

        private void ResetFields()
        {
            //HdnDealRegistrationId.Value = "0";
            //HdnCollaborationId.Value = "0";
            //HdnResellerId.Value = "0";
            //HdnVendorId.Value = "0";

            //TbxUserId.Text = string.Empty;
            TbxFirstName.Text = string.Empty;
            TbxLastName.Text = string.Empty;
            TbxOrganiz.Text = string.Empty;
            TbxAddress.Text = string.Empty;
            TbxOpportunityDescription.Text = string.Empty;
            TbxEmail.Text = string.Empty;
            TbxPhone.Text = string.Empty;
            TbxWebsite.Text = string.Empty;
            TbxProduct.Text = string.Empty;
            TbxAmount.Text = string.Empty;
            TbxMonthDuration.Text = string.Empty;
            RdpExpectedClosedDate.Text = string.Empty;
            DdlForeCasting.Text = "Unknow";

            //divOpportError.Visible = divOpportErrorBottom.Visible = false;
            //divOpportSuccess.Visible = divOpportSuccessBottom.Visible = false;
        }

        private void GetCollaborationUsers()
        {
            List<ElioUsers> users = SqlCollaboration.GetCollaborationUsersByUserType(vSession.User, CollaborateInvitationStatus.Confirmed.ToString(), session);

            if (users.Count > 0)
            {
                divVendorsList.Visible = true;

                DrpPartners.Items.Clear();

                ListItem item = new ListItem();
                item.Value = "0";
                item.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Select Reseller" : "Select Vendor";

                DrpPartners.Items.Add(item);

                foreach (ElioUsers user in users)
                {
                    item = new ListItem();
                    item.Value = user.Id.ToString();
                    item.Text = user.CompanyName;

                    DrpPartners.Items.Add(item);
                }
            }
            else
            {
                divVendorsList.Visible = false;
            }
        }

        private void LoadDealResultStatus()
        {
            DdlDealResultEdit.Items.Clear();

            ListItem item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Pending).ToString();
            item.Text = RandstadDealResultStatus.Pending.ToString();
            DdlDealResultEdit.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Qualified).ToString();
            item.Text = RandstadDealResultStatus.Qualified.ToString();
            DdlDealResultEdit.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Solution_Developed).ToString();
            item.Text = RandstadDealResultStatus.Solution_Developed.ToString();
            DdlDealResultEdit.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Proposal_Presented).ToString();
            item.Text = RandstadDealResultStatus.Proposal_Presented.ToString();
            DdlDealResultEdit.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Negotiation_and_Close).ToString();
            item.Text = RandstadDealResultStatus.Negotiation_and_Close.ToString();
            DdlDealResultEdit.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Awarded_Contract_Pending).ToString();
            item.Text = RandstadDealResultStatus.Awarded_Contract_Pending.ToString();
            DdlDealResultEdit.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Won).ToString();
            item.Text = RandstadDealResultStatus.Won.ToString();
            DdlDealResultEdit.Items.Add(item);

            item = new ListItem();
            item.Value = Convert.ToInt32(DealResultStatus.Lost).ToString();
            item.Text = DealResultStatus.Lost.ToString();
            DdlDealResultEdit.Items.Add(item);

            DdlDealResultEdit.SelectedItem.Value = Convert.ToInt32(RandstadDealResultStatus.Pending).ToString();
            DdlDealResultEdit.SelectedItem.Text = RandstadDealResultStatus.Pending.ToString();
        }

        private bool LoadPartnerData(int partnerId)
        {
            ElioUsers partner = Sql.GetUserById(partnerId, session);
            if (partner != null)
            {
                LblDealPartnerName.Text = partner.CompanyName;
                LblDealPartnerNameType.Text = partner.CompanyType;
                //LblPhoneContent.Text = (!string.IsNullOrEmpty(partner.Phone)) ? partner.Phone : "-";
                LblAddressContent.Text = partner.Address;
                aWebsiteContent.HRef = partner.WebSite;
                aWebsiteContent.Target = "_blank";
                aWebsiteContent.HRef = partner.WebSite;
                aWebsiteContent.Target = "_blank";
                LblWebsiteContent.Text = partner.WebSite;
                aEmailContent.HRef = "mailto:" + partner.Email;
                LblEmailContent.Text = partner.Email;
                //aMoreDetails.HRef = ControlLoader.PersonProfile(partner);
                //aComposeMessage.HRef = ControlLoader.Dashboard(vSession.User, "messages/compose");
                //aMoreDetails.Target = "_blank";
                //LblMoreDetailsContent.Text = "view profile";
                ImgCompanyLogo.ImageUrl = partner.CompanyLogo;
                divSelectedPartnerHeader.Visible = true;

                ElioRegistrationDealsVendorSettings monthSettings = Sql.GetVendorDealMonthSettings(partnerId, session);

                if (monthSettings != null)
                {
                    //DdlMonthDuration.SelectedIndex = -1;

                    //DdlMonthDuration.FindItemByValue(monthSettings.DealDurationSetting.ToString()).Selected = true;
                    TbxMonthDuration.Text = monthSettings.DealDurationSetting.ToString();
                }
                else
                {
                    TbxMonthDuration.Text = "6";
                }
            }

            return partner != null;
        }

        private void GetSelectedDealData(int dealId)
        {
            ElioRegistrationDeals deal = Sql.GetDealById(dealId, session);

            if (deal != null)
            {
                if (deal.IsActive == (int)DealActivityStatus.Deleted)
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
                else
                {
                    if (deal.Status == (int)DealStatus.Open)
                        DdlDealStatus.Text = DealStatus.Open.ToString();
                    else if (deal.Status == (int)DealStatus.Closed)
                        DdlDealStatus.Text = DealStatus.Closed.ToString();
                    else if (deal.Status == (int)DealStatus.Expired)
                        DdlDealStatus.Text = DealStatus.Expired.ToString();
                    else if (deal.Status == (int)DealStatus.InProgress)
                        DdlDealStatus.Text = DealStatus.InProgress.ToString();

                    if (deal.DealResult == RandstadDealResultStatus.Won.ToString())
                    {
                        DdlDealResult.Text = RandstadDealResultStatus.Won.ToString();
                        DdlDealResultEdit.SelectedValue = ((int)RandstadDealResultStatus.Won).ToString();
                        DdlDealResultEdit.Text = RandstadDealResultStatus.Won.ToString();
                    }
                    else if (deal.DealResult == RandstadDealResultStatus.Pending.ToString()) 
                    { 
                        DdlDealResult.Text = RandstadDealResultStatus.Pending.ToString();
                        DdlDealResultEdit.SelectedValue = ((int)RandstadDealResultStatus.Pending).ToString();
                        DdlDealResultEdit.Text = RandstadDealResultStatus.Pending.ToString();
                    }
                    else if (deal.DealResult == RandstadDealResultStatus.Lost.ToString()) 
                    { 
                        DdlDealResult.Text = RandstadDealResultStatus.Lost.ToString();
                        DdlDealResultEdit.SelectedValue = ((int)RandstadDealResultStatus.Lost).ToString();
                        DdlDealResultEdit.Text = RandstadDealResultStatus.Lost.ToString();
                    }
                    else if (deal.DealResult == RandstadDealResultStatus.Qualified.ToString()) 
                    { 
                        DdlDealResult.Text = RandstadDealResultStatus.Qualified.ToString();
                        DdlDealResultEdit.SelectedValue = ((int)RandstadDealResultStatus.Qualified).ToString();
                        DdlDealResultEdit.Text = RandstadDealResultStatus.Qualified.ToString();
                    }
                    else if (deal.DealResult == RandstadDealResultStatus.Solution_Developed.ToString()) 
                    { 
                        DdlDealResult.Text = RandstadDealResultStatus.Solution_Developed.ToString();
                        DdlDealResultEdit.SelectedValue = ((int)RandstadDealResultStatus.Won).ToString();
                        DdlDealResultEdit.Text = RandstadDealResultStatus.Won.ToString();
                    }
                    else if (deal.DealResult == RandstadDealResultStatus.Proposal_Presented.ToString()) 
                    { 
                        DdlDealResult.Text = RandstadDealResultStatus.Proposal_Presented.ToString();
                        DdlDealResultEdit.SelectedValue = ((int)RandstadDealResultStatus.Proposal_Presented).ToString();
                        DdlDealResultEdit.Text = RandstadDealResultStatus.Proposal_Presented.ToString();
                    }
                    else if (deal.DealResult == RandstadDealResultStatus.Negotiation_and_Close.ToString()) 
                    { 
                        DdlDealResult.Text = RandstadDealResultStatus.Negotiation_and_Close.ToString();
                        DdlDealResultEdit.SelectedValue = ((int)RandstadDealResultStatus.Negotiation_and_Close).ToString();
                        DdlDealResultEdit.Text = RandstadDealResultStatus.Negotiation_and_Close.ToString();
                    }
                    else if (deal.DealResult == RandstadDealResultStatus.Awarded_Contract_Pending.ToString()) 
                    { 
                        DdlDealResult.Text = RandstadDealResultStatus.Awarded_Contract_Pending.ToString();
                        DdlDealResultEdit.SelectedValue = ((int)RandstadDealResultStatus.Awarded_Contract_Pending).ToString();
                        DdlDealResultEdit.Text = RandstadDealResultStatus.Awarded_Contract_Pending.ToString();
                    }

                    if (deal.IsActive == (int)DealActivityStatus.NotConfirmed)
                        DdlIsActive.Text = DealActivityStatus.NotConfirmed.ToString();
                    else if (deal.IsActive == (int)DealActivityStatus.Approved)
                        DdlIsActive.Text = DealActivityStatus.Approved.ToString();
                    else if (deal.IsActive == (int)DealActivityStatus.Rejected)
                        DdlIsActive.Text = DealActivityStatus.Rejected.ToString();
                    
                    //RBtnCheckCrmLead.Visible = deal.IsActive == (int)DealActivityStatus.NotConfirmed && vSession.User.CompanyType == Types.Vendors.ToString();

                    if (deal.IsActive != (int)DealActivityStatus.NotConfirmed)
                    {
                        //RBtnSendToCrm.Visible = vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();
                        //RBtnSendToCrmVendor.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                        //RBtnSendToCrm.Visible = true;
                        RBtnSave.Visible = false;
                        RBtnReject.Visible = RBtnApprove.Visible = false;
                        //RBtnApprove.Visible = deal.IsActive == (int)DealActivityStatus.Rejected;
                        //RBtnReject.Visible = deal.IsActive == (int)DealActivityStatus.Approved;
                    }

                    if (deal.IsActive == (int)DealActivityStatus.NotConfirmed)
                    {
                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            BtnDelete.Visible = deal.CreatedByUserId == deal.VendorId && (deal.IsActive == (int)DealActivityStatus.Rejected && deal.Status == (int)DealStatus.Closed && deal.DealResult == RandstadDealResultStatus.Lost.ToString());
                        }
                        else
                        {
                            BtnDelete.Visible = deal.CreatedByUserId == deal.ResellerId && (deal.IsActive == (int)DealActivityStatus.Rejected && deal.Status == (int)DealStatus.Closed && deal.DealResult == RandstadDealResultStatus.Lost.ToString());
                        }

                        RBtnSendToCrm.Visible = false;

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            RBtnApprove.Visible = RBtnReject.Visible = (deal.VendorId == vSession.User.Id && deal.CreatedByUserId != deal.VendorId);
                        }
                        else
                        {
                            RBtnApprove.Visible = RBtnReject.Visible = (deal.ResellerId == vSession.User.Id && deal.CreatedByUserId != deal.ResellerId);
                        }
                    }

                    if (deal.Status == (int)DealStatus.Open)
                    {
                        if (deal.DealResult != RandstadDealResultStatus.Won.ToString() && deal.DealResult != RandstadDealResultStatus.Lost.ToString())
                        {
                            if (deal.IsActive == (int)DealActivityStatus.Approved)
                            {
                                AllowEdit(false);
                                
                                RBtnSave.Visible = DdlDealResultEdit.Visible = true;
                                DdlDealResult.Visible = false;
                            }
                            else
                            {
                                RBtnSave.Visible = DdlDealResultEdit.Visible = false;
                                DdlDealResult.Visible = true;
                                DdlDealResult.Text = deal.DealResult;
                            }
                        }
                        else
                        {
                            RBtnSave.Visible = DdlDealResultEdit.Visible = false;
                            DdlDealResult.Visible = true;
                            DdlDealResult.Text = deal.DealResult;
                        }
                    }
                    else
                    {
                        RBtnSave.Visible = DdlDealResultEdit.Visible = false;
                        DdlDealResult.Visible = true;
                        DdlDealResult.Text = deal.DealResult;
                    }

                    if (deal.MonthDuration != 0)
                    {
                        TbxMonthDuration.Text = deal.MonthDuration.ToString();
                        //DdlMonthDuration.FindItemByValue(deal.MonthDuration.ToString()).Selected = true;
                    }
                    else
                    {
                        //if (deal.IsActive == 0)
                        //{
                        ElioRegistrationDealsVendorSettings monthSettings = Sql.GetVendorDealMonthSettings(deal.VendorId, session);
                        if (monthSettings != null)
                        {
                            if (TbxMonthDuration.Text != monthSettings.DealDurationSetting.ToString())        //(DdlMonthDuration.SelectedItem.Value != monthSettings.DealDurationSetting.ToString())
                                TbxMonthDuration.Text = "";                                                   //DdlMonthDuration.SelectedIndex = -1;

                            //DdlMonthDuration.FindItemByValue(monthSettings.DealDurationSetting.ToString()).Selected = true;
                            TbxMonthDuration.Text = monthSettings.DealDurationSetting.ToString();
                        }
                        //}
                    }

                    TbxMonthDuration.Visible = true;

                    TbxLastName.Text = deal.LastName;
                    TbxFirstName.Text = deal.FirstName;
                    TbxOrganiz.Text = deal.CompanyName;
                    TbxAddress.Text = deal.Address;
                    TbxEmail.Text = deal.Email;
                    TbxWebsite.Text = aWebsite.HRef = deal.Website;
                    aWebsite.Target = "_blank";
                    TbxPhone.Text = deal.Phone;
                    TbxProduct.Text = deal.Product;
                    TbxAmount.Text = string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount);

                    #region Currency

                    if (string.IsNullOrEmpty(deal.CurId))
                    {
                        LoadCurrency("NO");

                        divSelectedCurrencyArea.Visible = true;
                        divCurrencyArea.Visible = false;

                        string partnerCountry = "";

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            ElioUsers dealPartner = Sql.GetUserById(deal.ResellerId, session);
                            if (dealPartner != null)
                                partnerCountry = dealPartner.Country;
                        }
                        else
                            partnerCountry = vSession.User.Country;

                        if (partnerCountry != "")
                        {
                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(partnerCountry, session);
                            if (countryCurrency != null)
                            {
                                deal = Sql.GetDealById(dealId, session);
                                if (deal != null)
                                {
                                    deal.CurId = countryCurrency.CurId;

                                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);
                                    loader.Update(deal);

                                    TbxCurrency.Text = countryCurrency.CurId + "(" + countryCurrency.CurrencySymbol + "-" + countryCurrency.CurrencyId + ")";
                                }

                                if (DrpCurrency.Items.Count > 1)
                                {
                                    DrpCurrency.SelectedValue = countryCurrency.CurId;
                                    DrpCurrency.SelectedItem.Text = countryCurrency.CurId + "(" + countryCurrency.CurrencySymbol + "-" + countryCurrency.CurrencyId + ")";
                                }
                            }
                        }
                    }
                    else
                    {
                        divSelectedCurrencyArea.Visible = true;
                        divCurrencyArea.Visible = false;

                        ElioCurrenciesCountries currency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                        if (currency != null)
                        {
                            TbxCurrency.Text = currency.CurId + "(" + currency.CurrencySymbol + "-" + currency.CurrencyId + ")";
                        }
                    }

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vSession.User.Id, session);

                        if (vendorCurrency != null)
                        {
                            string vendorCurrencyID = vendorCurrency.CurrencyId;

                            ElioCurrenciesCountries currency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                            if (currency != null)
                            {
                                string resellerCurrencyID = currency.CurrencyId;

                                if (vendorCurrencyID != "" && resellerCurrencyID != "")
                                {
                                    if (vendorCurrencyID != resellerCurrencyID)
                                    {
                                        double newAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", TbxAmount.Text)), resellerCurrencyID, vendorCurrencyID);
                                        if (newAmount > 0)
                                        {
                                            TbxAmount.Text = string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", newAmount);       //newAmount.ToString("0.00");
                                            TbxCurrency.Text = vendorCurrency.CurId + "(" + vendorCurrency.CurrencySymbol + "-" + vendorCurrency.CurrencyId + ")";
                                        }
                                    }
                                    else
                                        TbxAmount.Text = string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount);
                                }
                            }
                        }
                        else
                        {
                            LoadCurrency("0");

                            divSelectedCurrencyArea.Visible = false;
                            divCurrencyArea.Visible = true;
                        }
                    }

                    #endregion

                    TbxOpportunityDescription.Text = deal.Description;
                    RdpExpectedClosedDate.Text = deal.ExpectedClosedDate.ToString();

                    if (!string.IsNullOrEmpty(deal.ForecastingPercent))
                    {
                        DdlForeCasting.Text = deal.ForecastingPercent.ToString() + " %";
                        divForeCasting.Attributes["style=width:"] = deal.ForecastingPercent.ToString() + "%;";
                        divForeCasting.Attributes["aria-valunow"] = deal.ForecastingPercent.ToString();
                        divForeCasting.Attributes["aria-valuemax"] = "100";
                        divForeCasting.Attributes["aria-valuemin"] = "0";
                    }
                    else
                    {
                        divForeCasting.Attributes["style=width:"] = "0%;";
                        divForeCasting.Attributes["aria-valuenow"] = "0";
                        DdlForeCasting.Text = "Unknow";
                    }

                    int partnerId = vSession.User.CompanyType == Types.Vendors.ToString() ? deal.ResellerId : deal.VendorId;

                    bool successLoaded = LoadPartnerData(partnerId);
                    if (!successLoaded)
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);

                    UpdatePanelContent.Update();
                }
            }
            else
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
        }

        private void LoadAPIs()
        {
            DataTable crms = Sql.GetAllCrmUserIntegrationsTbl(vSession.User.Id, session);           //GetPublicCrmIntegrationsTable(session);

            if (crms.Rows.Count > 0)
            {
                DrpUserCrmList.Items.Clear();

                DataRow row = crms.NewRow();
                row["id"] = "0";
                row["crm_name"] = "Select CRM";
                row["crm_description"] = "";
                row["date_created"] = DateTime.Now;
                row["is_public"] = "1";

                crms.Rows.Add(row);
                crms.DefaultView.Sort = "id";

                DrpUserCrmList.DataValueField = "id";
                DrpUserCrmList.DataTextField = "crm_name";
                DrpUserCrmList.DataSource = crms;

                DrpUserCrmList.DataBind();
            }
            else
            {
                divFreemiumArea.Visible = true;
                divPremiumArea.Visible = false;
                ShowPopUpModalWithText("Warning", "This service is not available yet", MessageTypes.Info);
                //GlobalMethods.ShowMessageControlDA(FreemiumMessageControl, "This service is not available yet", MessageTypes.Info, true, true, false);
            }
        }

        private void ShowPopUpModalWithText(string title, string content, MessageTypes type)
        {
            LblMessageTitle.Text = title;
            GlobalMethods.ShowMessageControlDA(UcMessageControl, content, type, true, true, true, false, false);

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
        }

        #endregion

        #region Grids

        #endregion

        #region Buttons

        protected void BtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    int dealId = -1;
                    DealActionMode mode = DealActionMode.UPDATE;

                    if (Request.QueryString["dealViewID"] != null)
                        dealId = Convert.ToInt32(Session[Request.QueryString["dealViewID"].ToString()]);
                    else
                    {
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
                        return;
                    }

                    if (dealId == -1)
                    {
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
                        return;
                    }

                    #region Check Fields

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        #region Vendor validation check

                        if (DdlIsActive.Text == "")
                        {
                            ShowPopUpModalWithText("Warning", "Please select if deal is Approved or Rejected", MessageTypes.Warning);
                            //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Please select if deal is Approved or Rejected", MessageTypes.Warning, true, true, true, true, false);

                            return;
                        }
                        else
                        {
                            if (DdlIsActive.Text == "1" && TbxMonthDuration.Text == "")       //&& DdlMonthDuration.SelectedValue == "0"
                            {
                                ShowPopUpModalWithText("Warning", "Please select Deal's duration", MessageTypes.Warning);
                                //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Please select Deal's duration", MessageTypes.Warning, true, true, true, true, false);

                                return;
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        if (DdlDealResultEdit.SelectedItem.Text == RandstadDealResultStatus.Won.ToString() || DdlDealResultEdit.SelectedItem.Text == RandstadDealResultStatus.Lost.ToString())
                        {
                            ShowPopUpModalWithText("Warning", "You are not authorized to select this Deal's result stage. Please select another!", MessageTypes.Warning);
                            //GlobalMethods.ShowMessageControlDA(UcMessageControl, "You are not authorized to select this Deal's result stage. Please select another!", MessageTypes.Warning, true, true, true, true, false);

                            return;
                        }
                    }

                    #endregion

                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

                    if (mode == DealActionMode.UPDATE)
                    {
                        #region Update Deal

                        ElioRegistrationDeals deal = Sql.GetDealById(dealId, session);
                        if (deal != null)
                        {
                            if (DdlForeCasting.Text != "Unknow")
                                deal.ForecastingPercent = DdlForeCasting.Text.Replace(" %", "");
                            else
                                deal.ForecastingPercent = null;

                            deal.DealResult = DdlDealResultEdit.SelectedItem.Text;

                            if (deal.DealResult == RandstadDealResultStatus.Won.ToString() || deal.DealResult == RandstadDealResultStatus.Lost.ToString())
                            {
                                deal.Status = Convert.ToInt32(DealStatus.Closed);

                                DdlDealResult.Text = deal.DealResult;

                                DdlDealResult.Visible = true;
                                DdlDealResultEdit.Visible = false;

                                DdlDealStatus.Text = DealStatus.Closed.ToString();
                                RBtnSave.Visible = false;
                            }

                            deal.LastUpdate = DateTime.Now;
                            deal.MonthDuration = Convert.ToInt32(TbxMonthDuration.Text);

                            if (deal.CreatedByUserId == vSession.User.Id)
                            {
                                int isNew = 1;
                                if (deal.IsActive != (int)DealActivityStatus.NotConfirmed && deal.IsNew == isNew)
                                {
                                    #region Set New Deal as Viewed

                                    deal.IsNew = 0;
                                    deal.DateViewed = DateTime.Now;

                                    #endregion
                                }
                            }

                            if (vSession.User.CompanyType == Types.Vendors.ToString() && vSession.User.Id == GlobalMethods.GetRandstadCustomerID())
                            {
                                if (deal.DealResult == RandstadDealResultStatus.Won.ToString() || deal.DealResult == RandstadDealResultStatus.Lost.ToString())
                                {
                                    try
                                    {
                                        ElioUsers partner = Sql.GetUserById(deal.ResellerId, session);
                                        if (partner != null)
                                            EmailSenderLib.SendNewDealRegistrationWonLostEmail(partner.Email, vSession.User.CompanyName, DdlDealResult.Text, deal.CompanyName, vSession.LoggedInSubAccountRoleID > 0, vSession.Lang, session);
                                        else
                                            Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, changed new registration deal's status to {1}, at {2}, but no partner email was found to send notification email to", vSession.User.Id.ToString(), DdlDealResultEdit.Text, DateTime.Now.ToString()), "DashboardRandstadDealsViewPage.aspx --> ERROR sending notification Email");
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        throw ex;
                                    }
                                }
                            }

                            loader.Update(deal);

                            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers) && deal.ResellerId == vSession.User.Id)
                            {
                                if (deal.Status == (int)DealStatus.Closed && deal.DealResult == RandstadDealResultStatus.Won.ToString())
                                {
                                    #region set tier management

                                    try
                                    {
                                        string errorMsg = "";
                                        string tierStatus = "";

                                        bool successSet = GlobalDBMethods.SetResellerTierStatus(deal.ResellerId, deal.VendorId, deal.CollaborationVendorResellerId, 0, false, out errorMsg, out tierStatus, session);
                                        if (!successSet && errorMsg != "")
                                        {
                                            //error
                                            Exception ex = new Exception("GlobalDBMethods.SetResellerTierStatus ERROR because " + errorMsg);
                                            throw ex;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationView.aspx --> ERROR SETTING TIER MANAGEMENT", string.Format("Reseller with ID {0}, closed his lead with ID {1} for his vendor with ID {2}, but tier management could not be updated, at {3}", vSession.User.Id, deal.Id, deal.VendorId, DateTime.Now.ToString()), ex.Message.ToString(), ex.StackTrace.ToString());
                                    }

                                    #endregion
                                }
                            }

                            ShowPopUpModalWithText("Success", "Deal status was changed successfully!", MessageTypes.Success);
                            //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal status was changed successfully!", MessageTypes.Success, true, true, true, true, false);
                        }

                        #endregion
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

        protected void BtnBack_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnReject_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (Request.QueryString["dealViewID"] != null)
                    {
                        ElioRegistrationDeals deal = Sql.GetDealById(Convert.ToInt32(Session[Request.QueryString["dealViewID"].ToString()]), session);
                        if (deal != null)
                        {
                            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

                            deal.IsActive = Convert.ToInt32(DealActivityStatus.Rejected);
                            deal.ChangeStatusDate = DateTime.Now;
                            deal.Status = Convert.ToInt32(DealStatus.Closed);
                            deal.DealResult = DealResultStatus.Lost.ToString();
                            deal.MonthDuration = Convert.ToInt32(TbxMonthDuration.Text);       //Convert.ToInt32(DdlMonthDuration.SelectedItem.Value);
                            deal.LastUpdate = DateTime.Now;

                            loader.Update(deal);

                            ShowPopUpModalWithText("Success", "Deal " + DealActivityStatus.Rejected.ToString() + " successfully!", MessageTypes.Success);
                            //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal " + DealActivityStatus.Rejected.ToString() + " successfully!", MessageTypes.Success, true, true, true, true, false);
                            
                            DdlIsActive.Text = DealActivityStatus.Rejected.ToString();      //deal.IsActive.ToString();

                            RBtnReject.Visible = false;
                            RBtnApprove.Visible = true;

                            try
                            {
                                ElioUsers partner = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Sql.GetUserById(deal.ResellerId, session) : Sql.GetUserById(deal.VendorId, session);

                                if (partner != null)
                                    EmailSenderLib.SendNewDealRegistrationAcceptRejectEmail(partner.Email, vSession.User.CompanyName, partner.CompanyName, DealActivityStatus.Rejected.ToString(), deal.CompanyName, vSession.LoggedInSubAccountRoleID > 0, vSession.Lang, session);
                                else
                                    Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, approved new registration deal at {1}, but no partner email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardRandstadDealsAddEdit.aspx --> ERROR sending notification Email");
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }
                        }
                        else
                        {
                            ShowPopUpModalWithText("Warning", "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error);
                            //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error, true, true, true, true, false);

                            return;
                        }
                    }
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
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

        protected void BtnApprove_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (Request.QueryString["dealViewID"] != null)
                    {
                        ElioRegistrationDeals deal = Sql.GetDealById(Convert.ToInt32(Session[Request.QueryString["dealViewID"].ToString()]), session);
                        if (deal != null)
                        {
                            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

                            deal.IsActive = Convert.ToInt32(DealActivityStatus.Approved);
                            deal.ChangeStatusDate = DateTime.Now;
                            deal.MonthDuration = Convert.ToInt32(TbxMonthDuration.Text);           //Convert.ToInt32(DdlMonthDuration.SelectedItem.Value);
                            deal.LastUpdate = DateTime.Now;

                            loader.Update(deal);

                            ShowPopUpModalWithText("Success", "Deal " + DealActivityStatus.Approved.ToString() + " successfully!", MessageTypes.Success);
                            //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal " + DealActivityStatus.Approved.ToString() + " successfully!", MessageTypes.Success, true, true, true, true, false);
                            
                            DdlIsActive.Text = DealActivityStatus.Approved.ToString();       //deal.IsActive.ToString();

                            RBtnApprove.Visible = false;
                            RBtnReject.Visible = false;

                            try
                            {
                                ElioUsers partner = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Sql.GetUserById(deal.ResellerId, session) : Sql.GetUserById(deal.VendorId, session);

                                if (partner != null)
                                    EmailSenderLib.SendNewDealRegistrationAcceptRejectEmail(partner.Email, vSession.User.CompanyName, partner.CompanyName, DealActivityStatus.Approved.ToString(), deal.CompanyName, vSession.LoggedInSubAccountRoleID > 0, vSession.Lang, session);
                                else
                                    Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, approved new registration deal at {1}, but no partner email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardRandstadDealsAddEdit.aspx --> ERROR sending notification Email");
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }
                        }
                        else
                        {
                            ShowPopUpModalWithText("Warning", "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error);
                            //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error, true, true, true, true, false);

                            return;
                        }
                    }
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
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

        protected void BtnGetLeads_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    #region Fix Data

                    LoadAPIs();
                    divCrmErrorMessage.Visible = false;
                    LblLeadCrmErrorMessage.Text = "";
                    DrpUserCrmList.SelectedIndex = -1;
                    //divApiKeyArea.Visible = false;
                    divSearchLeadInfo.Visible = false;

                    #endregion

                    if (DrpUserCrmList.Items.Count == 0)
                    {
                        #region No Integrations

                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "integrations"), false);
                        return;

                        #endregion
                    }
                    else
                    {
                        if (DrpUserCrmList.Items.Count == 2)
                        {
                            ElioCrmUserIntegrations integration = Sql.GetUserCrmIntegration(vSession.User.Id, session);

                            if (integration == null || (integration != null && string.IsNullOrEmpty(integration.UserApiKey)))
                            {
                                #region Go Add Integration (API KEY)

                                Response.Redirect(ControlLoader.Dashboard(vSession.User, "integrations"), false);
                                return;

                                #endregion
                            }
                            else
                            {
                                if (Request.QueryString["dealViewID"] == null)
                                {
                                    #region New Deal

                                    #region Show search by email area

                                    divFreemiumArea.Visible = false;
                                    divPremiumArea.Visible = true;
                                    divCrmListArea.Visible = false;
                                    divSearchLeadInfo.Visible = true;
                                    TbxLeadEmail.Text = "";

                                    UpdatePanel8.Update();

                                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenLeadsServiceModal();", true);

                                    #endregion

                                    #endregion
                                }
                                else
                                {
                                    #region View - Old Deal

                                    int leadId = Convert.ToInt32(Session[Request.QueryString["dealViewID"]]);
                                    if (leadId > 0)
                                    {
                                        ElioRegistrationDeals deal = Sql.GetDealById(leadId, session);

                                        if (deal != null)
                                        {
                                            #region Send New Lead To CRM

                                            string crmResponse = Lib.Services.CRMs.HubspotAPI.HubspotService.CreateOrUpdateContactLead(integration.UserApiKey, deal, null);

                                            if (crmResponse != System.Net.HttpStatusCode.OK.ToString())
                                            {
                                                if (crmResponse == System.Net.HttpStatusCode.Conflict.ToString().ToUpper())
                                                {
                                                    #region Lead exists in CRM

                                                    ShowPopUpModalWithText("Warning", "Deal already exists in your CRM.", MessageTypes.Info);
                                                    //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal already exists in your CRM.", MessageTypes.Info, true, true, true, true, false);
                                                    //divOpportError.Visible = divOpportErrorBottom.Visible = true;
                                                    //LblOpportErrorCont.Text = LblOpportErrorContBottom.Text = "Deal already exists in your CRM.";

                                                    return;

                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region Bad response

                                                    ShowPopUpModalWithText("Warning", "Deal could not be send to CRM. Please try again later", MessageTypes.Error);
                                                    //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                                                    //divOpportError.Visible = divOpportErrorBottom.Visible = true;
                                                    //LblOpportErrorCont.Text = LblOpportErrorContBottom.Text = "Deal could not be send to CRM. Please try again later";

                                                    Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR creating new lead to CRM with response: " + crmResponse, string.Format("deal with ID: {0} could not be created to CRM by user: {1}, at {2} with bad response: {3}", deal.Id, vSession.User.Id, DateTime.Now.ToString(), crmResponse));

                                                    #endregion
                                                }
                                            }
                                            else
                                            {
                                                #region Save connection Elio-deal with Crm-deal

                                                try
                                                {
                                                    bool exists = Sql.ExistCrmUserDeal(integration.CrmIntegrationId, deal.Id, session);
                                                    if (!exists)
                                                    {
                                                        ElioCrmUserDeals crmDeal = new ElioCrmUserDeals();

                                                        crmDeal.CrmIntegrationId = integration.CrmIntegrationId;
                                                        crmDeal.CrmDeadId = "";
                                                        crmDeal.DealId = deal.Id;
                                                        crmDeal.DateInsert = DateTime.Now;
                                                        crmDeal.LastUpdate = DateTime.Now;
                                                        crmDeal.IsActive = 1;

                                                        DataLoader<ElioCrmUserDeals> loader = new DataLoader<ElioCrmUserDeals>(session);
                                                        loader.Insert(crmDeal);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                                }

                                                #endregion

                                                #region Success lead create/update to CRM

                                                ShowPopUpModalWithText("Success", "Deal was sent/updated to your CRM successfully", MessageTypes.Success);
                                                //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal was sent/updated to your CRM successfully", MessageTypes.Success, true, true, true, true, false);
                                                //divOpportSuccess.Visible = divOpportSuccessBottom.Visible = true;
                                                //LblOpportSuccCont.Text = LblOpportSuccContBottom.Text = "Deal was sent/updated to your CRM successfully";

                                                #endregion
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Lead not found By ID

                                            ShowPopUpModalWithText("Warning", "Deal could not be send to CRM. Please try again later", MessageTypes.Error);
                                            //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                                            //divOpportError.Visible = divOpportErrorBottom.Visible = true;
                                            //LblOpportErrorCont.Text = LblOpportErrorContBottom.Text = "Deal could not be send to CRM. Please try again later";

                                            Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending lead to CRM", string.Format("deal with ID: {0} could not be send to CRM by user: {1}, at {2}", deal.Id, vSession.User.Id, DateTime.Now.ToString()));

                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        #region No Lead found by Session URL

                                        ShowPopUpModalWithText("Warning", "Deal could not be send to CRM. Please try again later", MessageTypes.Error);
                                        //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                                        //divOpportError.Visible = divOpportErrorBottom.Visible = true;
                                        //LblOpportErrorCont.Text = LblOpportErrorContBottom.Text = "Deal could not be send to CRM. Please try again later";

                                        Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending lead to CRM because it could not be found by session url", string.Format("deal could not be not be found by url and  not sent to CRM by user: {0}, at {1}", vSession.User.Id, DateTime.Now.ToString()));

                                        #endregion
                                    }

                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            #region More than one integrations

                            divFreemiumArea.Visible = false;
                            divPremiumArea.Visible = true;

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenLeadsServiceModal();", true);

                            #endregion
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

        protected void BtnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divCrmErrorMessage.Visible = false;
                LblLeadCrmErrorMessage.Text = "";

                ElioCrmUserIntegrations userCrmIntegration = null;

                if (divCrmListArea.Visible)
                {
                    if (DrpUserCrmList.SelectedItem.Value != "0")
                    {
                        userCrmIntegration = Sql.GetUserCrmIntegrationByCrmID(vSession.User.Id, Convert.ToInt32(DrpUserCrmList.SelectedItem.Value), session);
                    }
                    else
                    {
                        divCrmErrorMessage.Visible = true;
                        LblLeadCrmErrorMessage.Text = "Please select CRM";
                        return;
                    }
                }
                else
                {
                    userCrmIntegration = Sql.GetUserCrmIntegration(vSession.User.Id, session);
                }

                if (userCrmIntegration == null)
                {
                    divCrmErrorMessage.Visible = true;
                    LblLeadCrmErrorMessage.Text = "Sorry, something went wrong. Please try again later";
                    return;
                }

                if (TbxLeadEmail.Text.Trim() == "")
                {
                    divCrmErrorMessage.Visible = true;
                    LblLeadCrmErrorMessage.Text = "Please fill deal's email address";
                    return;
                }
                else
                {
                    if (TbxLeadEmail.Text.Trim() != "" && !Validations.IsEmail(TbxLeadEmail.Text.Trim()))
                    {
                        divCrmErrorMessage.Visible = true;
                        LblLeadCrmErrorMessage.Text = "Please add a valid email address";
                        return;
                    }
                }

                ElioRegistrationDeals lead = Lib.Services.CRMs.HubspotAPI.HubspotService.GetDealDetailByEmail(vSession.User.Id, userCrmIntegration.UserApiKey, TbxLeadEmail.Text, session);
                if (lead != null)
                {
                    TbxFirstName.Text = lead.FirstName;
                    TbxLastName.Text = lead.LastName;
                    TbxOrganiz.Text = lead.CompanyName;
                    TbxEmail.Text = lead.Email;
                    TbxWebsite.Text = lead.Website;
                    TbxPhone.Text = lead.Phone;
                    TbxOpportunityDescription.Text = lead.Description;

                    lead.ResellerId = Convert.ToInt32(DrpPartners.SelectedItem.Value);

                    TbxLeadEmail.Text = "";

                    UpdatePanel8.Update();
                    UpdatePanelContent.Update();

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseLeadsServiceModal();", true);
                }
                else
                {
                    divCrmErrorMessage.Visible = true;
                    LblLeadCrmErrorMessage.Text = "Sorry, no lead was found with this email";
                    return;
                }
            }
            catch (Exception ex)
            {
                divCrmErrorMessage.Visible = true;
                LblLeadCrmErrorMessage.Text = "Sorry, no lead was found";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnSendData_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                #region Check Data

                if (DrpUserCrmList.SelectedItem.Value == "0")
                {
                    divCrmErrorMessage.Visible = true;
                    LblLeadCrmErrorMessage.Text = "Please select CRM";
                    return;
                }

                //if (TbxApiKey.Text.Trim() == "")
                //{
                //    divCrmErrorMessage.Visible = true;
                //    LblLeadCrmErrorMessage.Text = "Please add selected CRM api key";
                //    return;
                //}

                #endregion

                if (Request.QueryString["dealViewID"] != null)
                {
                    int leadId = Convert.ToInt32(Session[Request.QueryString["dealViewID"]]);
                    if (leadId > 0)
                    {
                        ElioRegistrationDeals deal = Sql.GetDealById(leadId, session);

                        if (deal != null)
                        {
                            #region Send New Lead To CRM

                            ElioCrmUserIntegrations integration = Sql.GetUserCrmIntegrationByCrmID(vSession.User.Id, Convert.ToInt32(DrpUserCrmList.SelectedItem.Value), session);
                            if (integration != null)
                            {
                                string crmResponse = Lib.Services.CRMs.HubspotAPI.HubspotService.CreateOrUpdateContactLead(integration.UserApiKey, deal, null);

                                if (crmResponse != System.Net.HttpStatusCode.OK.ToString())
                                {
                                    if (crmResponse == System.Net.HttpStatusCode.Conflict.ToString().ToUpper())
                                    {
                                        #region Lead exists in CRM

                                        ShowPopUpModalWithText("Warning", "Deal already exists in " + DrpUserCrmList.SelectedItem.Text + " CRM.", MessageTypes.Info);
                                        //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal already exists in " + DrpUserCrmList.SelectedItem.Text + " CRM.", MessageTypes.Info, true, true, true, true, false);
                                        //divOpportError.Visible = divOpportErrorBottom.Visible = true;
                                        //LblOpportErrorCont.Text = LblOpportErrorContBottom.Text = "Deal already exists in " + DrpUserCrmList.SelectedItem.Text + " CRM.";

                                        #endregion
                                    }
                                    else
                                    {
                                        #region Bad response

                                        ShowPopUpModalWithText("Warning", "Deal could not be send to CRM. Please try again later", MessageTypes.Error);
                                        //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                                        //divOpportError.Visible = divOpportErrorBottom.Visible = true;
                                        //LblOpportErrorCont.Text = LblOpportErrorContBottom.Text = "Deal could not be send to CRM. Please try again later";

                                        Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR creating new lead to CRM with response: " + crmResponse, string.Format("deal with ID: {0} could not be created to CRM by user: {1}, at {2} with bad response: {3}", deal.Id, vSession.User.Id, DateTime.Now.ToString(), crmResponse));

                                        #endregion
                                    }
                                }
                                else
                                {
                                    #region Save connection Elio-deal with Crm-deal

                                    try
                                    {
                                        bool exists = Sql.ExistCrmUserDeal(Convert.ToInt32(DrpUserCrmList.SelectedItem.Value), deal.Id, session);
                                        if (!exists)
                                        {
                                            ElioCrmUserDeals crmDeal = new ElioCrmUserDeals();

                                            crmDeal.CrmIntegrationId = Convert.ToInt32(DrpUserCrmList.SelectedItem.Value);
                                            crmDeal.CrmDeadId = "";
                                            crmDeal.DealId = deal.Id;
                                            crmDeal.DateInsert = DateTime.Now;
                                            crmDeal.LastUpdate = DateTime.Now;
                                            crmDeal.IsActive = 1;

                                            DataLoader<ElioCrmUserDeals> loader = new DataLoader<ElioCrmUserDeals>(session);
                                            loader.Insert(crmDeal);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    }

                                    #endregion

                                    #region Success lead create/update to CRM

                                    ShowPopUpModalWithText("Success", "Deal was sent/updated to selected CRM successfully", MessageTypes.Success);
                                    //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal was sent/updated to selected CRM successfully", MessageTypes.Success, true, true, true, true, false);
                                    //divOpportSuccess.Visible = divOpportSuccessBottom.Visible = true;
                                    //LblOpportSuccCont.Text = LblOpportSuccContBottom.Text = "Deal was sent/updated to selected CRM successfully";

                                    #endregion
                                }
                            }
                            else
                            {

                            }

                            #endregion
                        }
                        else
                        {
                            #region Lead not found By ID

                            ShowPopUpModalWithText("Warning", "Deal could not be send to CRM. Please try again later", MessageTypes.Error);
                            //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                            //divOpportError.Visible = divOpportErrorBottom.Visible = true;
                            //LblOpportErrorCont.Text = LblOpportErrorContBottom.Text = "Deal could not be send to CRM. Please try again later";

                            Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending lead to CRM", string.Format("deal with ID: {0} could not be send to CRM by user: {1}, at {2}", deal.Id, vSession.User.Id, DateTime.Now.ToString()));

                            #endregion
                        }
                    }
                    else
                    {
                        #region No Lead found by Session URL

                        ShowPopUpModalWithText("Warning", "Deal could not be send to CRM. Please try again later", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                        //divOpportError.Visible = divOpportErrorBottom.Visible = true;
                        //LblOpportErrorCont.Text = LblOpportErrorContBottom.Text = "Deal could not be send to CRM. Please try again later";

                        Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending lead to CRM because it could not be found by session url", string.Format("deal could not be not be found by url and  not sent to CRM by user: {0}, at {1}", vSession.User.Id, DateTime.Now.ToString()));

                        #endregion
                    }
                }
                else
                {
                    #region No Lead found by URL

                    //divOpportError.Visible = divOpportErrorBottom.Visible = true;
                    //LblOpportErrorCont.Text = LblOpportErrorContBottom.Text = "Deal could not be send to CRM. Please try again later";
                    ShowPopUpModalWithText("Warning", "Deal could not be send to CRM. Please try again later", MessageTypes.Error);
                    //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);

                    Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending deal to CRM because it could not be found by url", string.Format("deal could not be not be found by url and  not sent to CRM by user: {0}, at {1}", vSession.User.Id, DateTime.Now.ToString()));

                    #endregion
                }

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseLeadsServiceModal();", true);

                UpdatePanelContent.Update();
            }
            catch (Exception ex)
            {
                divCrmErrorMessage.Visible = true;
                LblLeadCrmErrorMessage.Text = "Sorry, deal could not be send to " + DrpUserCrmList.SelectedItem.Text + " CRM";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnCheckCrmLead_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ElioCrmUserIntegrations integration = Sql.GetUserCrmIntegration(vSession.User.Id, session);

                    if (integration != null)
                    {
                        bool exist = Lib.Services.CRMs.HubspotAPI.HubspotService.SearchContactByEmai(integration.UserApiKey, TbxEmail.Text);

                        if (exist)
                        {
                            ShowPopUpModalWithText("Warning", "This deal already exists in your CRM", MessageTypes.Info);
                            //GlobalMethods.ShowMessageControlDA(UcMessageControl, "This deal already exists in your CRM", MessageTypes.Info, true, true, true, true, false);
                        }
                        else
                        {
                            ShowPopUpModalWithText("Warning", "This deal does not exists in your CRM", MessageTypes.Warning);
                            //GlobalMethods.ShowMessageControlDA(UcMessageControl, "This deal does not exists in your CRM", MessageTypes.Warning, true, true, true, true, false);
                        }
                    }
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "integrations"), false);
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

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    UcPopUpConfirmationMessageAlert.Visible = false;

                    if (Request.QueryString["dealViewID"] != null)
                    {
                        int dealId = Convert.ToInt32(Session[Request.QueryString["dealViewID"]]);
                        if (dealId > 0)
                        {
                            TbxId.Value = dealId.ToString();

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmPopUp();", true);
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
        }

        protected void BtnConfDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    UcPopUpConfirmationMessageAlert.Visible = false;

                    if (Request.QueryString["dealViewID"] != null)
                    {
                        int dealId = Convert.ToInt32(TbxId.Value);

                        if (dealId == Convert.ToInt32(Session[Request.QueryString["dealViewID"]]))
                        {
                            ElioRegistrationDeals deal = Sql.GetDealById(dealId, session);
                            if (deal != null)
                            {
                                try
                                {
                                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

                                    deal.IsActive = (int)DealActivityStatus.Deleted;
                                    //deal.Status = (int)DealStatus.Deleted;
                                    deal.LastUpdate = DateTime.Now;

                                    loader.Update(deal);
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    throw ex;
                                }

                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);

                                TbxId.Value = "0";
                                BtnDelete.Visible = false;

                                ShowPopUpModalWithText("Success", "Deal was deleted successfully.", MessageTypes.Success);
                                //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Deal was deleted successfully.", MessageTypes.Success, true, true, true, true, false);

                                UpdatePanelContent.Update();
                            }
                            else
                                Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
                        }
                        else
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
                    }
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                ShowPopUpModalWithText("Warning", "Deal could not be deleted.", MessageTypes.Error);
                //GlobalMethods.ShowMessageControlDA(UcPopUpConfirmationMessageAlert, "Deal could not be deleted.", MessageTypes.Error, true, true, true, true, false);
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        # endregion

        #region Dropdown Lists

        protected void DrpPartners_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LoadAPIs();

                    if (DrpPartners.SelectedItem.Value == "0")
                    {
                        divSelectedPartnerHeader.Visible = false;
                        ResetFields();
                        AllowEdit(false);
                        divCrmService.Visible = false;
                        //divVendorsArea.Visible = false;
                    }
                    else
                    {
                        int vendorId = Convert.ToInt32(DrpPartners.SelectedItem.Value);
                        if (vendorId > 0)
                        {
                            AllowEdit(vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString());
                            DdlDealResult.Enabled = false;

                            bool successLoaded = LoadPartnerData(vendorId);
                            if (!successLoaded)
                                Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);

                            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers) && Request.QueryString["dealViewID"] == null)
                            {
                                //divCrmService.Visible = true;
                                divFreemiumArea.Visible = false;
                                divPremiumArea.Visible = true;
                            }
                            else
                            {
                                divCrmService.Visible = false;
                            }
                        }
                        else
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
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

        protected void DrpUserCrmList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    TbxLeadEmail.Text = "";
                    //CbxSaveApiKey.Checked = false;
                    divCrmErrorMessage.Visible = false;
                    LblLeadCrmErrorMessage.Text = "";

                    if (DrpUserCrmList.SelectedItem.Value != "0")
                    {
                        //divApiKeyArea.Visible = true;

                        if (Request.QueryString["dealVendorViewID"] == null && Request.QueryString["dealViewID"] != null)
                        {
                            divSearchLeadInfo.Visible = false;
                            BtnSendData.Visible = true;
                            BtnGetData.Visible = false;
                        }
                        else if (Request.QueryString["dealVendorViewID"] != null && Request.QueryString["dealViewID"] == null)
                        {
                            divSearchLeadInfo.Visible = true;
                            BtnSendData.Visible = false;
                            BtnGetData.Visible = true;
                        }

                        ElioCrmUserIntegrations userCrm = Sql.GetUserCrmIntegrationByCrmID(vSession.User.Id, Convert.ToInt32(DrpUserCrmList.SelectedItem.Value), session);
                        if (userCrm != null && string.IsNullOrEmpty(userCrm.UserApiKey))
                        {
                            #region Go Add Integration (API KEY)

                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "integrations"), false);
                            return;

                            #endregion
                        }
                    }
                    else
                    {
                        divSearchLeadInfo.Visible = BtnSendData.Visible = false;
                        BtnGetData.Visible = true;
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

        protected void DrpCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                string vendorCurrencyID = "";
                string resellerCurrencyID = "";

                if (TbxAmount.Text != "")
                {
                    if (DrpCurrency.SelectedValue != "NO")
                    {
                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            ElioCurrenciesCountries selectedCurrency = Sql.GetCurrencyCountryByCurId(DrpCurrency.SelectedValue, session);
                            if (selectedCurrency != null)
                            {
                                vendorCurrencyID = selectedCurrency.CurrencyId;

                                int dealId = Convert.ToInt32(Session[Request.QueryString["dealViewID"]]);
                                if (dealId > 0)
                                {
                                    ElioRegistrationDeals deal = Sql.GetDealById(dealId, session);

                                    if (deal != null && deal.CurId != "")
                                    {
                                        ElioCurrenciesCountries currency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                                        if (currency != null)
                                        {
                                            resellerCurrencyID = currency.CurrencyId;
                                        }

                                        if (vendorCurrencyID != "" && resellerCurrencyID != "")
                                        {
                                            if (vendorCurrencyID == resellerCurrencyID)
                                                TbxAmount.Text = string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", deal.Amount);
                                            else
                                            {
                                                double convertedAmount = ConverterLib.Convert(Convert.ToDouble(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", TbxAmount.Text)), resellerCurrencyID, vendorCurrencyID);
                                                if (convertedAmount > 0)
                                                {
                                                    TbxAmount.Text = string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", convertedAmount);   //convertedAmount.ToString();
                                                }
                                                else
                                                {
                                                    throw new Exception("Currency conversion failed. Please try again later or contact us.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Currency conversion failed. Please try again later or contact us.");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("Currency conversion failed. Please try again later or contact us.");
                                    }
                                }
                                else
                                {
                                    throw new Exception("Currency conversion failed. Please try again later or contact us.");
                                }
                            }
                            else
                            {
                                throw new Exception("Currency conversion failed. Please try again later or contact us.");
                            }
                        }
                    }
                    else
                    {
                        ShowPopUpModalWithText("Warning", "Please select currency to convert the amount or reload the deal.", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Please select currency to convert the amount or reload the deal.", MessageTypes.Error, true, true, true, true, false);
                        return;
                    }
                }
                else
                {
                    ShowPopUpModalWithText("Warning", "Please fill deals amount first in order to get the  currency", MessageTypes.Info);
                    //GlobalMethods.ShowMessageControlDA(UcMessageControl, "Please fill deals amount first in order to get the  currency", MessageTypes.Info, true, true, true, true, false);
                    return;
                }
            }
            catch (Exception ex)
            {
                ShowPopUpModalWithText("Warning", ex.Message, MessageTypes.Info);
                //GlobalMethods.ShowMessageControlDA(UcMessageControl, ex.Message, MessageTypes.Info, true, true, true, true, false);

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