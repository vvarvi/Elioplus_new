using System;
using System.Linq;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Web.UI;
using System.Collections.Generic;
using WdS.ElioPlus.Lib.Enums;
using System.IO;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace WdS.ElioPlus.Controls
{
    public partial class FullRegistrationPrm : System.Web.UI.UserControl
    {
        public Types UserType
        {
            get
            {
                if (ViewState["UserType"] != null)
                    return (Types)ViewState["UserType"];
                else
                    return Types.Resellers;
            }
            set
            {
                ViewState["UserType"] = value;
            }
        }

        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HtmlAnchor aStartupModal = (HtmlAnchor)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "aStartupModal");
                HtmlAnchor aGrowthPaymentModal = (HtmlAnchor)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "aGrowthPaymentModal");

                if (aStartupModal != null && aGrowthPaymentModal != null)
                {
                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(aStartupModal);
                    scriptManager.RegisterPostBackControl(aGrowthPaymentModal);
                }

                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        LoadCountries();
                        UpdateStrings();

                        FixPage();
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

        #region Methods

        private void FixPaymentBtns()
        {
            HtmlAnchor aStartupModal = (HtmlAnchor)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "aStartupModal");
            HtmlAnchor aGrowthPaymentModal = (HtmlAnchor)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "aGrowthPaymentModal");
            HtmlAnchor aGoPremium = (HtmlAnchor)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "aGoPremium");

            Button btnGoPremium = (Button)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "BtnGoPremium");
            Button btnStartUpGoPremium = (Button)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "BtnStartUpGoPremium");
            Button btnGrowthGoPremium = (Button)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "BtnGrowthGoPremium");

            bool showBtn = false;
            bool showModal = false;

            bool allowPayment = GlobalDBMethods.AllowPaymentProccess(vSession.User, true, ref showBtn, ref showModal, session);

            if (allowPayment)
            {
                btnStartUpGoPremium.Visible = btnGrowthGoPremium.Visible = btnGoPremium.Visible = showBtn;
                aStartupModal.Visible = aGrowthPaymentModal.Visible = aGoPremium.Visible = showModal;
            }
            else
            {
                btnStartUpGoPremium.Visible = btnGrowthGoPremium.Visible = btnGoPremium.Visible = false;
                aStartupModal.Visible = aGrowthPaymentModal.Visible = aGoPremium.Visible = false;
            }
        }

        private void LoadPacketFeatures()
        {
            #region Find Controls

            //Label LblFreeLeads = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblFreeLeads");
            Label LblFreeMessages = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblFreeMessages");
            Label LblFreeManagePartners = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblFreeManagePartners");
            Label LblFreeLibraryStorage = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblFreeLibraryStorage");
            //Label LblPremiumStartupLeads = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumStartupLeads");
            Label LblPremiumStartupMessages = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumStartupMessages");
            Label LblPremiumStartupConnections = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumStartupConnections");
            Label LblPremiumStartupManagePartners = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumStartupManagePartners");
            Label LblPremiumStartupLibraryStorage = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumStartupLibraryStorage");

            //Label LblPremiumGrowthLeads = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumGrowthLeads");
            Label LblPremiumGrowthMessages = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumGrowthMessages");
            Label LblPremiumGrowthConnections = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumGrowthConnections");
            Label LblPremiumGrowthManagePartners = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumGrowthManagePartners");
            Label LblPremiumGrowthLibraryStorage = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumGrowthLibraryStorage");

            Label LblPremiumStartupPrice = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumStartupPrice");
            Label LblPremiumGrowthPrice = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPremiumGrowthPrice");
            Label LblFreePrice = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblFreePrice");
            Label LblFreeConnections = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblFreeConnections");

            #endregion

            List<ElioPacketFeaturesItems> packetsFeaturesItems = Sql.GetAllPublicPacketTotalCostAndFeatures(session);

            decimal freeCost = 0;
            decimal premiumCost = 0;
            decimal premiumStartupCost = 0;
            decimal premiumGrowthCost = 0;

            foreach (ElioPacketFeaturesItems item in packetsFeaturesItems)
            {
                if (item.PackId == (int)Packets.Freemium)
                {
                    freeCost += item.ItemCostWithVat * item.FreeItemsNo;

                    //if (item.FeatureId == 1)
                    //    LblFreeLeads.Text = item.FreeItemsNo.ToString();
                    if (item.FeatureId == 2)
                        LblFreeMessages.Text = item.FreeItemsNo.ToString();
                    else if (item.FeatureId == 4)
                        LblFreeManagePartners.Text = item.FreeItemsNo.ToString();
                    else if (item.FeatureId == 5)
                        LblFreeLibraryStorage.Text = item.FreeItemsNo.ToString();
                }
                else if (item.PackId == (int)Packets.Premium)
                {
                    premiumCost += item.ItemCostWithVat * item.FreeItemsNo;

                    //if (item.FeatureId == 1)
                    //LblPremiumLeads.Text = item.FreeItemsNo.ToString();
                    //else if (item.FeatureId == 2)
                    //LblPremiumMessages.Text = item.FreeItemsNo.ToString();
                    //else if (item.FeatureId == 3)
                    //LblPremiumConnections.Text = item.FreeItemsNo.ToString();
                    if (item.FeatureId == 4)
                        LblFreeManagePartners.Text = item.FreeItemsNo.ToString();
                    else if (item.FeatureId == 5)
                        LblFreeLibraryStorage.Text = item.FreeItemsNo.ToString();
                }
                else if (item.PackId == (int)Packets.PremiumStartup)
                {
                    premiumStartupCost += item.ItemCostWithVat * item.FreeItemsNo;

                    //if (item.FeatureId == 1)
                    //    LblPremiumStartupLeads.Text = item.FreeItemsNo.ToString();
                    if (item.FeatureId == 2)
                        LblPremiumStartupMessages.Text = item.FreeItemsNo.ToString();
                    else if (item.FeatureId == 3)
                        LblPremiumStartupConnections.Text = item.FreeItemsNo.ToString();
                    else if (item.FeatureId == 4)
                        LblPremiumStartupManagePartners.Text = item.FreeItemsNo.ToString();
                    else if (item.FeatureId == 5)
                        LblPremiumStartupLibraryStorage.Text = item.FreeItemsNo.ToString();
                }
                else if (item.PackId == (int)Packets.PremiumGrowth)
                {
                    premiumGrowthCost += item.ItemCostWithVat * item.FreeItemsNo;

                    //if (item.FeatureId == 1)
                    //    LblPremiumGrowthLeads.Text = item.FreeItemsNo.ToString();
                    if (item.FeatureId == 2)
                        LblPremiumGrowthMessages.Text = item.FreeItemsNo.ToString();
                    else if (item.FeatureId == 3)
                        LblPremiumGrowthConnections.Text = item.FreeItemsNo.ToString();
                    else if (item.FeatureId == 4)
                        LblPremiumGrowthManagePartners.Text = item.FreeItemsNo.ToString();
                    else if (item.FeatureId == 5)
                        LblPremiumGrowthLibraryStorage.Text = item.FreeItemsNo.ToString();
                }
            }

            int? totalLeads = 0;
            int? totalMessages = 0;
            int? totalConnections = 0;
            int? totalManagePartners = 0;
            int? totalLibraryStorage = 0;

            #region Premium Packet

            decimal totalCost = Sql.GetPacketTotalCostAndFeatures(Convert.ToInt32(Packets.Premium), ref totalLeads, ref totalMessages, ref totalConnections, ref totalManagePartners, ref totalLibraryStorage, session);

            //LblOldPrice.Text = "$ 249.00 / month";

            //LblOldPriceInfo.Text = " (Limited spots available)";

            //LblPremiumPrice.Text = "$ " + totalCost.ToString() + " / month";

            //LblPremiumLeads.Text = "up to {count} leads".Replace("{count}", totalLeads.ToString());

            //LblPremiumMessages.Text = "{count} messages".Replace("{count}", totalMessages.ToString());

            //LblPremiumConnections.Text = "{count} matches".Replace("{count}", totalConnections.ToString());

            //LblPremiumManagePartners.Text = "{count} partners to manage".Replace("{count}", totalManagePartners.ToString());

            //LblPremiumLibraryStorage.Text = "{count} GB library storage".Replace("{count}", totalLibraryStorage.ToString());

            #endregion

            #region Start Up Packet

            totalCost = Sql.GetPacketTotalCostAndFeatures(Convert.ToInt32(Packets.PremiumStartup), ref totalLeads, ref totalMessages, ref totalConnections, ref totalManagePartners, ref totalLibraryStorage, session);

            LblPremiumStartupPrice.Text = totalCost.ToString();

            //LblPremiumStartupPriceInfo.Text = "";

            //LblPremiumStartupLeads.Text = "{count} leads".Replace("{count}", totalLeads.ToString());

            LblPremiumStartupMessages.Text = "{count} messages".Replace("{count}", totalMessages.ToString());

            LblPremiumStartupConnections.Text = "{count} matches".Replace("{count}", totalConnections.ToString());

            LblPremiumStartupManagePartners.Text = "manage {count} partners".Replace("{count}", totalManagePartners.ToString());

            LblPremiumStartupLibraryStorage.Text = "{count} GB library storage".Replace("{count}", totalLibraryStorage.ToString());

            #endregion

            #region Growth Packet

            totalCost = Sql.GetPacketTotalCostAndFeatures(Convert.ToInt32(Packets.PremiumGrowth), ref totalLeads, ref totalMessages, ref totalConnections, ref totalManagePartners, ref totalLibraryStorage, session);

            LblPremiumGrowthPrice.Text = totalCost.ToString();

            //LblPremiumGrowthPriceInfo.Text = "";

            //LblPremiumGrowthLeads.Text = "{count} leads".Replace("{count}", totalLeads.ToString());

            LblPremiumGrowthMessages.Text = "{count} messages".Replace("{count}", totalMessages.ToString());

            LblPremiumGrowthConnections.Text = "{count} matches".Replace("{count}", totalConnections.ToString());

            LblPremiumGrowthManagePartners.Text = "manage {count} partners".Replace("{count}", totalManagePartners.ToString());

            LblPremiumGrowthLibraryStorage.Text = "{count} GB library storage".Replace("{count}", totalLibraryStorage.ToString());

            #endregion

            #region Enterprise Packet

            //totalCost = Sql.GetPacketTotalCostAndFeatures(Convert.ToInt32(Packets.PremiumEnterprise), ref totalLeads, ref totalMessages, ref totalConnections, ref totalManagePartners, ref totalLibraryStorage, session);

            //LblPremiumEnterprisePrice.Text = totalCost.ToString();

            //LblPremiumEnterpriseLeads.Text = "over {count} leads".Replace("{count}", totalLeads.ToString());

            //LblPremiumEnterpriseMessages.Text = "{count} messages".Replace("{count}", totalMessages.ToString());

            //LblPremiumEnterpriseConnections.Text = "custom";    // connections"; //"over {count} connections".Replace("{count}", totalConnections.ToString());

            //LblPremiumEnterpriseManagePartners.Text = "custom"; // partners to manage";  //"over {count} partners to manage".Replace("{count}", totalManagePartners.ToString());

            //LblPremiumEnterpriseLibraryStorage.Text = "custom";      //"over {count} GB library storage".Replace("{count}", totalLibraryStorage.ToString());


            #endregion

            #region Free Packet

            totalCost = Sql.GetPacketTotalCostAndFeatures(Convert.ToInt32(Packets.Freemium), ref totalLeads, ref totalMessages, ref totalConnections, ref totalManagePartners, ref totalLibraryStorage, session);

            LblFreePrice.Text = totalCost.ToString();

            //LblFreeLeads.Text = "{count} leads".Replace("{count}", totalLeads.ToString());

            LblFreeMessages.Text = "{count} messages".Replace("{count}", totalMessages.ToString());

            LblFreeConnections.Text = "{count} matches".Replace("{count}", totalConnections.ToString());

            LblFreeManagePartners.Text = "manage {count} partners".Replace("{count}", totalManagePartners.ToString());

            LblFreeLibraryStorage.Text = "{count} GB library storage".Replace("{count}", totalLibraryStorage.ToString());

            #endregion
        }

        private void PreLoadCompanyData()
        {
            if (vSession.User != null)
            {
                if (vSession.User.Email != "" && vSession.User.AccountStatus == (int)AccountStatus.NotCompleted)
                {
                    #region Get Data from Clearbit

                    vSession.User = Lib.Services.EnrichmentAPI.ClearBit.GetCombinedPersonCompanyByEmail_v2(vSession.User);

                    #endregion

                    #region Find Controls

                    TextBox tbxCompanyName = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyName");
                    //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
                    TextBox tbxWebsite = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxWebsite");
                    DropDownList ddlCountries = (DropDownList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "DdlCountries");
                    TextBox tbxAddress = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxAddress");
                    TextBox tbxState = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxState");
                    TextBox tbxCompanyPhone = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyPhone");

                    #endregion

                    #region Company Info

                    tbxCompanyName.Text = vSession.User.CompanyName;
                    //tbxCompanyEmail.Text = vSession.User.Email;
                    tbxWebsite.Text = vSession.User.WebSite;
                    if (!string.IsNullOrEmpty(vSession.User.Country))
                    {
                        ElioCountries country = Sql.GetCountryByCountryName(vSession.User.Country, session);
                        if (country != null)
                        {
                            ddlCountries.SelectedItem.Value = country.Id.ToString();
                            ddlCountries.SelectedItem.Text = country.CountryName;
                        }
                        else
                        {
                            if (vSession.User.Country == "Add Your Company Country")
                                ddlCountries.SelectedItem.Value = "0";

                            ddlCountries.SelectedItem.Text = vSession.User.Country;
                        }
                    }

                    tbxAddress.Text = vSession.User.Address;
                    tbxState.Text = vSession.User.State;
                    tbxCompanyPhone.Text = vSession.User.Phone;

                    #endregion

                    # region Logo

                    HtmlGenericControl divLogoFail = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divLogoFailure");
                    divLogoFail.Visible = false;

                    Label lblLogoFail = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblLogoFailure");
                    Label lblLogoFailCont = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblLogoFailureContent");
                    lblLogoFail.Text = string.Empty;
                    lblLogoFailCont.Text = string.Empty;

                    string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"].ToString());
                    string logoTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"]).ToString();
                    int maxSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileLenght"]);

                    Image imgViewLogo = (Image)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "ImgViewLogo");
                    HtmlInputFile companyLogo = (HtmlInputFile)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "CompanyLogo");

                    var logo = companyLogo.PostedFile;
                    if (logo != null)
                    {
                        var logoName = logo.FileName;
                        var logoSize = logo.ContentLength;
                        var logoType = logo.ContentType;

                        string formattedName = "";

                        if (logoSize != 0)
                        {
                            if (logoType != "image/png" && logoType != "image/jpeg" && logoType != "image/jpg")
                            {
                                lblLogoFail.Text = "Error! ";
                                lblLogoFailCont.Text = "Image type must be .png, or .jpg";
                                divLogoFail.Visible = true;
                                divLogoFail.Focus();
                                return;
                            }

                            if (logoSize > maxSize)
                            {
                                lblLogoFail.Text = "Error! ";
                                lblLogoFailCont.Text = "Image size is too large. Max size is " + maxSize + " kb.";
                                divLogoFail.Visible = true;
                                divLogoFail.Focus();
                                return;
                            }

                            if (logoType == "image/png")
                            {
                                formattedName = ImageResize.ChangeFileName(logoName, ".png");
                            }
                            else if (logoType == "image/jpeg")
                            {
                                formattedName = ImageResize.ChangeFileName(logoName, ".jpeg");
                            }
                            else if (logoType == "image/jpg")
                            {
                                formattedName = ImageResize.ChangeFileName(logoName, ".jpg");
                            }

                            serverMapPathTargetFolder = serverMapPathTargetFolder + vSession.User.GuId + "\\";

                            #region Create File Directory

                            if (!Directory.Exists(serverMapPathTargetFolder))
                                Directory.CreateDirectory(serverMapPathTargetFolder);

                            #endregion

                            #region Delete old files in directory if exist

                            DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                            foreach (FileInfo fi in originaldir.GetFiles())
                            {
                                fi.Delete();
                            }

                            #endregion

                            //logo.SaveAs(serverMapPathTargetFolder + formattedName);

                            #region Update User

                            //vSession.User.CompanyLogo = logoTargetFolder + vSession.User.GuId + "/" + formattedName;

                            //GlobalDBMethods.UpDateUser(vSession.User, session);

                            #endregion

                            imgViewLogo.ImageUrl = vSession.User.CompanyLogo;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(vSession.User.CompanyLogo))
                            {
                                lblLogoFail.Text = "Error! ";
                                lblLogoFailCont.Text = "You have to select an image.";
                                divLogoFail.Visible = true;
                                divLogoFail.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        imgViewLogo.ImageUrl = vSession.User.CompanyLogo;
                    }

                    #endregion
                }
            }
        }

        private void FixDataForCollaborationOrThirdPartyUserRegistration()
        {
            RadioButtonList rdBtnUserType = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");
            //Label lblTypeInfo = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblTypeInfo");
            HtmlAnchor aDennyInvitation = (HtmlAnchor)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "aDennyInvitation");

            if (SqlCollaboration.ExistUserInCollaboration(vSession.User.Id, session))
            {
                int pendingRequestsCount = 0;
                if (vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.Collaboration) || vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty) || SqlCollaboration.HasInvitationRequestByStatus(vSession.User.Id, vSession.User.AccountStatus, vSession.User.CompanyType, CollaborateInvitationStatus.Pending.ToString(), out pendingRequestsCount, session))
                {
                    bool isMasterVendorResellerUser = SqlCollaboration.IsMasterVendorResellerUser(vSession.User.Id, session);

                    if (isMasterVendorResellerUser)
                    {
                        rdBtnUserType.Enabled = false;
                        rdBtnUserType.SelectedValue = "1";

                        //lblTypeInfo.Text = "You are invited to register as a " + Types.Vendors.ToString() + " company type";

                        UserType = Types.Vendors;
                    }

                    //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
                    //tbxCompanyEmail.Enabled = false;
                    //tbxCompanyEmail.Text = vSession.User.Email;
                }

                LblInvitationMsg.Text = "Are you sure you want to delete collaboration Invitation?";
            }
            else
            {
                if (vSession.User.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty))
                {
                    //lblTypeInfo.Text = "You are suggested to register as a Channel Partner company type";

                    aDennyInvitation.Visible = false;
                    //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
                    //tbxCompanyEmail.Enabled = false;
                    //tbxCompanyEmail.Text = vSession.User.Email;

                    LblInvitationMsg.Text = "Are you sure you want to delete suggestion as Channel Partner registration?";

                    rdBtnUserType.Enabled = true;
                    rdBtnUserType.SelectedValue = "2";

                    UserType = Types.Resellers;

                    #region Delete third party user all sub industries group items

                    try
                    {
                        GlobalDBMethods.ResetThirdPartyUserVerticals(vSession.User.Id, session);
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), string.Format("Reset third party user {0} sub industries group items failed at{1}, failed to do at registration step.", vSession.User.Id.ToString(), DateTime.Now));
                    }

                    #endregion
                }
            }
        }

        protected void FixPage()
        {
            FixDataForCollaborationOrThirdPartyUserRegistration();

            ResetFields();

            //PreLoadCompanyData();

            #region Set Default Selected as Vendor

            RadioButtonList rdBtnUserType = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");
            rdBtnUserType.SelectedItem.Value = "1";

            #endregion
        }

        private void ResetFields()
        {
            divSuccess.Visible = false;
            divFailure.Visible = false;
            LblFailureMsg.Text = "";
            LblSuccessMsg.Text = "";
        }

        protected void RpbRegistrationSteps_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            int selectedIndex = RpbRegistrationSteps.SelectedItem.Index;
            if (selectedIndex == 1)
            {

            }
        }

        private void UpdateStrings()
        {
            RpbRegistrationSteps.Items[0].Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "10")).Text;
            RpbRegistrationSteps.Items[1].Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "11")).Text;

            //Label lbl10 = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "Lbl10");
            //lbl10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "1")).Text;
            //Label label9 = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "Label9");
            //label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "2")).Text;
            //Label label2 = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "Label2");
            //label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "5")).Text;
            //Label label3 = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "Label3");
            //label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "label", "6")).Text;
            Label lblSelectImg = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblSelectImg");
            lblSelectImg.Text = "Select image";
            Label lblChangeImg = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblChangeImg");
            lblChangeImg.Text = "Change image";

            Image imgViewLogo = (Image)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "ImgViewLogo");
            imgViewLogo.ImageUrl = vSession.User.CompanyLogo != null ? vSession.User.CompanyLogo : "/images/no_logo3.jpg";

            Label lblGoPremium = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblGoPremium");
            Label lblStartUpGoPremium = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblStartUpGoPremium");
            Label lblGrowthGoPremium = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblGrowthGoPremium");
            Button btnStartUpGoPremium = (Button)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "BtnStartUpGoPremium");
            Button btnGrowthGoPremium = (Button)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "BtnGrowthGoPremium");
            Button btnGoPremium = (Button)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "BtnGoPremium");

            Label lblSignUp = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblSignUp");
            Label lblSignUpStartUp = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblSignUpStartUp");
            Label lblSignUpGrowth = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblSignUpGrowth");

            lblGoPremium.Text = btnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "27")).Text;
            lblStartUpGoPremium.Text = btnStartUpGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "28")).Text;
            lblGrowthGoPremium.Text = btnGrowthGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "29")).Text;
        }

        private void LoadCountries()
        {
            DropDownList ddlCountries = (DropDownList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "DdlCountries");
            ddlCountries.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Select Country";

            ddlCountries.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new ListItem();
                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                ddlCountries.Items.Add(item);
            }
        }

        private bool CheckRegistrationData()
        {
            bool isError = false;

            #region Find Controls

            RadioButtonList rdBtnList = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");
            TextBox tbxCompanyName = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyName");
            //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
            TextBox tbxWebsite = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxWebsite");
            DropDownList ddlCountries = (DropDownList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "DdlCountries");
            TextBox tbxAddress = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxAddress");
            TextBox tbxState = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxState");
            TextBox tbxCompanyPhone = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyPhone");

            HtmlGenericControl divLogoFail = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divLogoFailure");
            divLogoFail.Visible = false;

            Label lblLogoFail = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblLogoFailure");
            Label lblLogoFailCont = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblLogoFailureContent");
            lblLogoFail.Text = string.Empty;
            lblLogoFailCont.Text = string.Empty;

            Label lblCompanyNameError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCompanyNameError");
            lblCompanyNameError.Text = string.Empty;
            //Label lblOfficialEmailError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblOfficialEmailError");
            //lblOfficialEmailError.Text = string.Empty;
            Label lblWeSiteError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblWeSiteError");
            lblWeSiteError.Text = string.Empty;
            Label lblCountryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCountryError");
            lblCountryError.Text = string.Empty;
            Label lblAddressError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblAddressError");
            lblAddressError.Text = string.Empty;

            HtmlGenericControl divUserTypeError = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divUserTypeError");
            divUserTypeError.Visible = false;

            Label lblUserTypeError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblUserTypeError");
            lblUserTypeError.Text = string.Empty;

            Label lblUserTypeErrorContent = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblUserTypeErrorContent");
            lblUserTypeErrorContent.Text = string.Empty;

            #endregion

            #region Step 2

            if (rdBtnList.SelectedItem == null)
            {
                divUserTypeError.Visible = true;
                lblUserTypeError.Text = "Error! ";
                lblUserTypeErrorContent.Text = "You have to select your company type.";
                divUserTypeError.Focus();
                isError = true;
            }

            if (string.IsNullOrEmpty(tbxCompanyName.Text))
            {
                lblCompanyNameError.Text = "Please add company name";
                tbxCompanyName.Focus();
                isError = true;
            }
            else
            {
                if (Validations.ContainsSpecialChar(tbxCompanyName.Text))
                {
                    lblCompanyNameError.Text = "Company name contains special characters. Please add valid company name";
                    tbxCompanyName.Focus();
                    isError = true;
                }

                if (vSession.User.UserApplicationType != (int)UserApplicationType.ThirdParty)
                {
                    if (ddlCountries.SelectedItem.Text != "")
                    {
                        if (Sql.IsCompanyNameRegisteredByCountry(tbxCompanyName.Text, ddlCountries.SelectedItem.Text, session))
                        {
                            lblCompanyNameError.Text = "User with this company name is already registered";
                            tbxCompanyName.Focus();
                            isError = true;
                        }
                    }
                }
            }

            //if (!string.IsNullOrEmpty(tbxCompanyEmail.Text))
            //{
            //    if (!Validations.IsEmail(tbxCompanyEmail.Text))
            //    {
            //        lblOfficialEmailError.Text = "Please enter a valid email";
            //        tbxCompanyEmail.Focus();
            //        isError = true;
            //    }

            //    if (Sql.ExistEmailToOtherUser(tbxCompanyEmail.Text, vSession.User.Id, session))
            //    {
            //        lblOfficialEmailError.Text = "The official email belongs to another user";
            //        tbxCompanyEmail.Focus();
            //        return true;
            //    }
            //}

            if (string.IsNullOrEmpty(tbxWebsite.Text))
            {
                lblWeSiteError.Text = "Please add website";
                tbxWebsite.Focus();
                isError = true;
            }
            else
            {
                if (tbxWebsite.Text.Split(' ').Length > 1)
                {
                    lblWeSiteError.Text = "Please add valid website";
                    tbxWebsite.Focus();
                    isError = true;
                }
            }

            if (ddlCountries.SelectedItem.Value == "0" && ddlCountries.SelectedItem.Text == "")
            {
                lblCountryError.Text = "Please select country";
                ddlCountries.Focus();
                isError = true;
            }
            else
            {
                if (vSession.User.UserApplicationType != (int)UserApplicationType.ThirdParty && vSession.User.UserApplicationType != (int)UserApplicationType.Collaboration)
                {
                    if (Sql.IsCompanyNameRegisteredByCountry(tbxCompanyName.Text, ddlCountries.SelectedItem.Text, session))
                    {
                        lblCompanyNameError.Text = "User with this company name is already registered in this country";
                        tbxCompanyName.Focus();
                        isError = true;
                    }
                }
            }

            if (string.IsNullOrEmpty(tbxAddress.Text))
            {
                lblAddressError.Text = "Please add address";
                tbxAddress.Focus();
                isError = true;
            }

            if (string.IsNullOrEmpty(vSession.User.CompanyLogo))
            {
                divLogoFail.Visible = true;
                lblLogoFail.Text = "Error! ";
                lblLogoFailCont.Text = "You have to submit a logo for your company";
                divLogoFail.Focus();
                isError = true;
            }

            if (isError)
            {
                RpbRegistrationSteps.Items[0].Expanded = true;
                return isError;
            }

            #endregion

            return isError;
        }

        private void GoToNextItem(bool check)
        {
            if (vSession.User != null)
            {
                int selectedIndex = RpbRegistrationSteps.SelectedItem.Index;

                #region Find Controls

                RadioButtonList rdBtnList = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");
                TextBox tbxCompanyName = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyName");
                //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
                TextBox tbxWebsite = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxWebsite");
                DropDownList ddlCountries = (DropDownList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "DdlCountries");
                TextBox tbxAddress = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxAddress");
                TextBox tbxState = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxState");
                TextBox tbxCompanyPhone = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyPhone");

                HtmlGenericControl divUserTypeError = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divUserTypeError");
                divUserTypeError.Visible = false;

                Label lblUserTypeError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblUserTypeError");
                lblUserTypeError.Text = string.Empty;

                Label lblUserTypeErrorContent = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblUserTypeErrorContent");
                lblUserTypeErrorContent.Text = string.Empty;

                Label lblCompanyNameError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCompanyNameError");
                lblCompanyNameError.Text = string.Empty;
                //Label lblOfficialEmailError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblOfficialEmailError");
                //lblOfficialEmailError.Text = string.Empty;
                Label lblWeSiteError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblWeSiteError");
                lblWeSiteError.Text = string.Empty;
                Label lblCountryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCountryError");
                lblCountryError.Text = string.Empty;
                Label lblAddressError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblAddressError");
                lblAddressError.Text = string.Empty;

                RadioButtonList rdBtnUserType = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");

                HtmlAnchor aStartupModal = (HtmlAnchor)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "aStartupModal");
                HtmlAnchor aGrowthPaymentModal = (HtmlAnchor)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "aGrowthPaymentModal");
                HtmlAnchor aGoPremium = (HtmlAnchor)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "aGoPremium");

                #endregion

                switch (selectedIndex)
                {
                    case 0:

                        if (check)
                        {
                            //FixDataForCollaborationOrThirdPartyUserRegistration();

                            #region Company Info

                            if (rdBtnList.SelectedItem == null)
                            {
                                divUserTypeError.Visible = true;
                                lblUserTypeError.Text = "Error! ";
                                lblUserTypeErrorContent.Text = "You have to select your company type.";
                                return;
                            }

                            if (string.IsNullOrEmpty(tbxCompanyName.Text))
                            {
                                lblCompanyNameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "2")).Text;
                                return;
                            }
                            else
                            {
                                if (Validations.ContainsSpecialChar(tbxCompanyName.Text))
                                {
                                    lblCompanyNameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "24")).Text;
                                    return;
                                }

                                if (vSession.User.UserApplicationType != (int)UserApplicationType.ThirdParty)
                                {
                                    if (ddlCountries.SelectedItem.Text != "")
                                    {
                                        if (Sql.IsCompanyNameRegisteredByCountry(tbxCompanyName.Text, ddlCountries.SelectedItem.Text, session))
                                        {
                                            lblCompanyNameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "23")).Text;
                                            return;
                                        }
                                    }
                                }
                            }

                            //if (!string.IsNullOrEmpty(tbxCompanyEmail.Text))
                            //{
                            //    if (!Validations.IsEmail(tbxCompanyEmail.Text))
                            //    {
                            //        lblOfficialEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "3")).Text;
                            //        return;
                            //    }
                            //}

                            if (string.IsNullOrEmpty(tbxWebsite.Text))
                            {
                                lblWeSiteError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "4")).Text;
                                return;
                            }
                            else
                            {
                                if (tbxWebsite.Text.Split(' ').Length > 1)
                                {
                                    lblWeSiteError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "27")).Text;
                                    return;
                                }
                            }

                            if (ddlCountries.SelectedItem.Text == "" || (ddlCountries.SelectedItem.Value == "0") || (ddlCountries.SelectedItem.Text != "" && ddlCountries.SelectedItem.Text == "Add Your Company Country"))
                            {
                                lblCountryError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "5")).Text;
                                return;
                            }
                            else
                            {
                                if (vSession.User.UserApplicationType != (int)UserApplicationType.ThirdParty)
                                {
                                    if (Sql.IsCompanyNameRegisteredByCountry(tbxCompanyName.Text, ddlCountries.SelectedItem.Text, session))
                                    {
                                        lblCompanyNameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "23")).Text;
                                        return;
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(tbxAddress.Text))
                            {
                                lblAddressError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "6")).Text;
                                return;
                            }

                            #endregion

                            # region Logo

                            HtmlGenericControl divLogoFail = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divLogoFailure");
                            divLogoFail.Visible = false;

                            Label lblLogoFail = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblLogoFailure");
                            Label lblLogoFailCont = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblLogoFailureContent");
                            lblLogoFail.Text = string.Empty;
                            lblLogoFailCont.Text = string.Empty;

                            string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"].ToString());
                            string logoTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"]).ToString();
                            int maxSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileLenght"]);

                            HtmlInputFile companyLogo = (HtmlInputFile)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "CompanyLogo");

                            var logo = companyLogo.PostedFile;
                            if (logo != null)
                            {
                                var logoName = logo.FileName;
                                var logoSize = logo.ContentLength;
                                var logoType = logo.ContentType;

                                string formattedName = "";

                                if (logoSize != 0)
                                {
                                    if (logoType != "image/png" && logoType != "image/jpeg" && logoType != "image/jpg")
                                    {
                                        lblLogoFail.Text = "Error! ";
                                        lblLogoFailCont.Text = "Image type must be .png, or .jpg";
                                        divLogoFail.Visible = true;
                                        divLogoFail.Focus();
                                        return;
                                    }

                                    if (logoSize > maxSize)
                                    {
                                        lblLogoFail.Text = "Error! ";
                                        lblLogoFailCont.Text = "Image size is too large. Max size is " + maxSize + " kb.";
                                        divLogoFail.Visible = true;
                                        divLogoFail.Focus();
                                        return;
                                    }

                                    if (logoType == "image/png")
                                    {
                                        formattedName = ImageResize.ChangeFileName(logoName, ".png");
                                    }
                                    else if (logoType == "image/jpeg")
                                    {
                                        formattedName = ImageResize.ChangeFileName(logoName, ".jpeg");
                                    }
                                    else if (logoType == "image/jpg")
                                    {
                                        formattedName = ImageResize.ChangeFileName(logoName, ".jpg");
                                    }

                                    serverMapPathTargetFolder = serverMapPathTargetFolder + vSession.User.GuId + "\\";

                                    #region Create File Directory

                                    if (!Directory.Exists(serverMapPathTargetFolder))
                                        Directory.CreateDirectory(serverMapPathTargetFolder);

                                    #endregion

                                    #region Delete old files in directory if exist

                                    DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                                    foreach (FileInfo fi in originaldir.GetFiles())
                                    {
                                        fi.Delete();
                                    }

                                    #endregion

                                    logo.SaveAs(serverMapPathTargetFolder + formattedName);

                                    #region Update User

                                    vSession.User.CompanyLogo = logoTargetFolder + vSession.User.GuId + "/" + formattedName;

                                    vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);

                                    #endregion

                                    Image imgViewLogo = (Image)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "ImgViewLogo");
                                    imgViewLogo.ImageUrl = vSession.User.CompanyLogo;
                                }
                                else
                                {
                                    //if (logoSize == 0)
                                    //    vSession.User.CompanyLogo = "";

                                    if (string.IsNullOrEmpty(vSession.User.CompanyLogo))
                                    {
                                        lblLogoFail.Text = "Error! ";
                                        lblLogoFailCont.Text = "You have to select an image.";
                                        divLogoFail.Visible = true;
                                        divLogoFail.Focus();
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                lblLogoFail.Text = "Error! ";
                                lblLogoFailCont.Text = "You have to select an image.";
                                divLogoFail.Visible = true;
                                divLogoFail.Focus();
                                return;
                            }

                            #endregion

                            if (rdBtnList.SelectedItem.Value == (Convert.ToInt32(Types.Vendors)).ToString())
                            {
                                LoadPacketFeatures();

                                FixPaymentBtns();

                                #region Update Button Strings

                                Label lblGoPremium = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblGoPremium");
                                Label lblStartUpGoPremium = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblStartUpGoPremium");
                                Label lblGrowthGoPremium = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblGrowthGoPremium");
                                Button btnStartUpGoPremium = (Button)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "BtnStartUpGoPremium");
                                Button btnGrowthGoPremium = (Button)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "BtnGrowthGoPremium");
                                Button btnGoPremium = (Button)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "BtnGoPremium");

                                lblGoPremium.Text = btnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "27")).Text;
                                lblStartUpGoPremium.Text = btnStartUpGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "28")).Text;
                                lblGrowthGoPremium.Text = btnGrowthGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "29")).Text;

                                #endregion
                            }
                        }

                        break;
                }

                #region Steps Messages

                //RpbRegistrationSteps.Items[2].Text = (rdBtnUserType.SelectedItem.Value == "2")
                //            ? "Step 3 of 4 - " + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "4")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "9")).Text
                //            : "Step 3 of 4 - " + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "4")).Text;

                //RpbRegistrationSteps.Items[3].Text = (rdBtnUserType.SelectedItem.Value == "1")
                //                                ? "Step 4 of 4 - " + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "8")).Text
                //                                : RpbRegistrationSteps.Items[3].Text = "Step 4 of 4 - " + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "item", "7")).Text;

                #endregion

                if (selectedIndex != 1 && rdBtnList.SelectedItem.Value == (Convert.ToInt32(Types.Vendors)).ToString())
                {
                    #region Expand next Item

                    RpbRegistrationSteps.Items[selectedIndex + 1].Selected = true;
                    RpbRegistrationSteps.Items[selectedIndex + 1].Expanded = true;
                    RpbRegistrationSteps.Items[selectedIndex + 1].Visible = true;
                    RpbRegistrationSteps.Items[selectedIndex].Expanded = false;

                    #endregion
                }
                else
                {
                    try
                    {
                        if (vSession.User != null)
                        {
                            #region Save Data

                            bool isError = CheckRegistrationData();
                            if (!isError)
                            {
                                ElioUsers user = Sql.GetUserById(vSession.User.Id, session);
                                if (user != null)
                                {
                                    if (user.AccountStatus == (int)AccountStatus.NotCompleted)
                                    {
                                        try
                                        {
                                            session.BeginTransaction();

                                            #region Personal Info

                                            user.CompanyType = (rdBtnList.SelectedItem.Value == (Convert.ToInt32(Types.Vendors)).ToString()) ? Types.Vendors.ToString() : EnumHelper.GetDescription(Types.Resellers).ToString();

                                            //if (tbxCompanyEmail.Text != string.Empty)
                                            //{
                                            //    user.OfficialEmail = tbxCompanyEmail.Text;
                                            //}

                                            user.Address = tbxAddress.Text;
                                            user.State = tbxState.Text;
                                            user.Phone = tbxCompanyPhone.Text;
                                            user.Country = ddlCountries.SelectedItem.Text;

                                            user.CompanyRegion = Sql.GetRegionByCountryId(Convert.ToInt32(ddlCountries.SelectedValue), session);

                                            user.WebSite = (tbxWebsite.Text.StartsWith("http://") || tbxWebsite.Text.StartsWith("https://")) ? tbxWebsite.Text.Trim() : "http://" + tbxWebsite.Text.Trim();
                                            user.CompanyName = tbxCompanyName.Text;
                                            user.Overview = ""; // GlobalMethods.FixStringEntryToParagraphs(tbxOverview.Text);
                                            user.Description = "";  // GlobalMethods.FixStringEntryToParagraphs(tbxDescription.Text);
                                            user.LastUpdated = DateTime.Now;
                                            user.UserApplicationType = Convert.ToInt32(UserApplicationType.Elioplus);

                                            # endregion

                                            #region Update User / Session

                                            user.AccountStatus = Convert.ToInt32(AccountStatus.Completed);
                                            user.IsPublic = Convert.ToInt32(AccountStatus.NotPublic);

                                            vSession.User = GlobalDBMethods.UpDateUser(user, session);

                                            #endregion

                                            #region Insert User Email Notifications Settings

                                            GlobalDBMethods.FixUserEmailNotificationsSettingsData(user, session);

                                            #endregion

                                            session.CommitTransaction();
                                        }
                                        catch (Exception ex)
                                        {
                                            session.RollBackTransaction();
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                                            Response.Redirect(ControlLoader.ErrorPage, false);
                                            return;
                                        }

                                        if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                        {
                                            #region Redirect to Dashboard Home Page

                                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                                            //Response.Redirect(ControlLoader.Dashboard(vSession.User, "partners-invitations"), false);

                                            #endregion
                                        }

                                        Logger.Info("User {0}, completed successfully his registration from {1} as {2}", vSession.User.Id, vSession.User.Country, vSession.User.CompanyType);

                                        #region Send Emails

                                        try
                                        {
                                            //EmailSenderLib.SendActivationEmailToFullRegisteredUser(vSession.User, vSession.Lang, session);
                                            EmailSenderLib.SendNotificationEmailForNewPRMFullRegisteredUser(vSession.User, vSession.Lang, session);
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }

                                        #endregion
                                    }
                                }
                                else
                                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "partners-invitations"), false);
                            }

                            #endregion
                        }
                        else
                        {
                            #region Redirect to Home Page

                            Response.Redirect(ControlLoader.Login, false);

                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }
            }
            else
                Response.Redirect(ControlLoader.Login, false);
        }

        #endregion

        #region Buttons

        protected void BtnSearchGoPremium_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User == null)
                {
                    Response.Redirect(ControlLoader.Login, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void PaymentFreemiumModal_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    #region User Features

                    ElioUsersFeatures freeFeatures = Sql.GetFeaturesbyUserType(Convert.ToInt32(BillingTypePacket.FreemiumPacketType), session);
                    if (freeFeatures != null)
                    {
                        ElioUserPacketStatus userPackStatus = Sql.GetUserPacketStatusFeatures(vSession.User.Id, session);

                        DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                        if (userPackStatus == null)
                        {
                            userPackStatus = new ElioUserPacketStatus();

                            userPackStatus.UserId = vSession.User.Id;
                            userPackStatus.PackId = freeFeatures.PackId;
                            userPackStatus.UserBillingType = freeFeatures.UserBillingType;
                            userPackStatus.AvailableLeadsCount = freeFeatures.TotalLeads;
                            userPackStatus.AvailableMessagesCount = freeFeatures.TotalMessages;
                            userPackStatus.AvailableConnectionsCount = freeFeatures.TotalConnections;
                            userPackStatus.AvailableManagePartnersCount = freeFeatures.TotalManagePartners;
                            userPackStatus.AvailableLibraryStorageCount = Convert.ToDecimal(freeFeatures.TotalLibraryStorage);
                            userPackStatus.Sysdate = DateTime.Now;
                            userPackStatus.LastUpdate = DateTime.Now;
                            userPackStatus.StartingDate = DateTime.Now;
                            userPackStatus.ExpirationDate = DateTime.Now.AddMonths(1);

                            loader.Insert(userPackStatus);
                        }
                        else
                        {
                            userPackStatus.UserId = vSession.User.Id;
                            userPackStatus.PackId = freeFeatures.PackId;
                            userPackStatus.UserBillingType = freeFeatures.UserBillingType;
                            userPackStatus.AvailableLeadsCount = freeFeatures.TotalLeads;
                            userPackStatus.AvailableMessagesCount = freeFeatures.TotalMessages;
                            userPackStatus.AvailableConnectionsCount = freeFeatures.TotalConnections;
                            userPackStatus.AvailableManagePartnersCount = freeFeatures.TotalManagePartners;
                            userPackStatus.AvailableLibraryStorageCount = Convert.ToDecimal(freeFeatures.TotalLibraryStorage);
                            userPackStatus.Sysdate = DateTime.Now;
                            userPackStatus.LastUpdate = DateTime.Now;
                            userPackStatus.StartingDate = DateTime.Now;
                            userPackStatus.ExpirationDate = DateTime.Now.AddMonths(1);

                            loader.Update(userPackStatus);
                        }
                    }

                    #endregion

                    GoToNextItem(true);

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "partners-invitations"), false);
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

        protected void PaymentStartupModal_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        int selectedPacket = (int)StripePlans.Elio_Startup_Plan;

                        string redUrl = GlobalDBMethods.CheckOut(selectedPacket, vSession.User, session);

                        if (redUrl != null)
                        {
                            Response.Redirect(redUrl, false);
                        }
                        else
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                    }
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
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

        protected void PaymentGrowthModal_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        int selectedPacket = (int)StripePlans.Elio_Growth_Plan;

                        string redUrl = GlobalDBMethods.CheckOut(selectedPacket, vSession.User, session);

                        if (redUrl != null)
                        {
                            Response.Redirect(redUrl, false);
                        }
                        else
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                    }
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
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

        protected void LnkBtnDennyInvitation_OnClick(object sender, EventArgs args)
        {
            try
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnClose_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields();

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmationPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnDennyInvitation_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ResetFields();

                    int pendingRequestsCount = 0;
                    if (SqlCollaboration.HasInvitationRequestByStatus(vSession.User.Id, vSession.User.AccountStatus, vSession.User.CompanyType, CollaborateInvitationStatus.Pending.ToString(), out pendingRequestsCount, session))
                    {
                        try
                        {
                            session.BeginTransaction();

                            SqlCollaboration.DeleteUserCollaborationAllItems(vSession.User.Id, Types.Resellers, session);

                            session.CommitTransaction();

                            divSuccess.Visible = true;
                            LblSuccessMsg.Text = "Invitation deleted successfully";
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                            divFailure.Visible = true;
                            LblFailureMsg.Text = "Invitation could not be deleted. Please try again later or contact with us";
                        }
                        finally
                        {
                            session.CloseConnection();
                        }
                    }

                    if (vSession.User.UserApplicationType == (int)UserApplicationType.ThirdParty)
                    {
                        try
                        {
                            EmailSenderLib.SendNotificationEmailForNewSimpleRegisteredThirdPartyToElioUser(vSession.User, vSession.Lang, session);
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString() + "User with ID:" + vSession.User.Id.ToString() + " dennied his third party type and tries to registered as Elio company.", ex.StackTrace.ToString());
                        }
                    }

                    vSession.User.UserApplicationType = (int)UserApplicationType.Elioplus;
                    vSession.User.LastUpdated = DateTime.Now;

                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                    loader.Update(vSession.User);

                    Response.Redirect(vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage, false);
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

        protected void BtnNext_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                GoToNextItem(true);
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
                session.OpenConnection();

                int selectedIndex = RpbRegistrationSteps.SelectedItem.Index;

                if (selectedIndex != 0)
                {
                    RpbRegistrationSteps.Items[selectedIndex - 1].Selected = true;
                    RpbRegistrationSteps.Items[selectedIndex - 1].Expanded = true;
                    RpbRegistrationSteps.Items[selectedIndex - 1].Visible = true;
                    RpbRegistrationSteps.Items[selectedIndex].Expanded = false;
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

        protected void BtnClearStep1_OnClick(object sender, EventArgs args)
        {
            try
            {
                TextBox tbxCompanyName = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyName");
                //TextBox tbxCompanyEmail = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyEmail");
                TextBox tbxWebsite = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxWebsite");
                DropDownList ddlCountries = (DropDownList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "DdlCountries");
                TextBox tbxAddress = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxAddress");
                TextBox tbxState = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxState");
                TextBox tbxCompanyPhone = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxCompanyPhone");

                HtmlGenericControl divLogoFail = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divLogoFailure");
                divLogoFail.Visible = false;

                HtmlGenericControl divUserTypeError = (HtmlGenericControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divUserTypeError");
                divUserTypeError.Visible = false;

                Label lblCompanyNameError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCompanyNameError");
                lblCompanyNameError.Text = string.Empty;
                //Label lblOfficialEmailError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblOfficialEmailError");
                //lblOfficialEmailError.Text = string.Empty;
                Label lblWeSiteError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblWeSiteError");
                lblWeSiteError.Text = string.Empty;
                Label lblCountryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblCountryError");
                lblCountryError.Text = string.Empty;
                Label lblAddressError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblAddressError");
                lblAddressError.Text = string.Empty;

                HtmlInputFile companyLogo = (HtmlInputFile)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "CompanyLogo");

                tbxCompanyName.Text = string.Empty;
                //tbxCompanyEmail.Text = string.Empty;
                tbxWebsite.Text = string.Empty;
                ddlCountries.SelectedItem.Value = "0";
                tbxAddress.Text = string.Empty;
                tbxState.Text = string.Empty;
                tbxCompanyPhone.Text = string.Empty;
                companyLogo.Dispose();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnClearStep2_OnClick(object sender, EventArgs args)
        {
            try
            {
                Label lblIndustryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblIndustryError");
                lblIndustryError.Text = string.Empty;
                Label lblPartnerError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblPartnerError");
                lblPartnerError.Text = string.Empty;
                Label lblMarketError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblMarketError");
                lblMarketError.Text = string.Empty;
                Label lblApiError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblApiError");
                lblApiError.Text = string.Empty;

                CheckBoxList cb1 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb1");
                CheckBoxList cb2 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb2");
                CheckBoxList cb3 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb3");

                CheckBoxList cb4 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb4");
                CheckBoxList cb5 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb5");
                CheckBoxList cb6 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb6");

                CheckBoxList cb7 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb7");
                CheckBoxList cb8 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb8");

                CheckBoxList cb9 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb9");
                CheckBoxList cb10 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb10");
                CheckBoxList cb11 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cb11");

                List<CheckBoxList> nList = new List<CheckBoxList>();
                nList.Add(cb1);
                nList.Add(cb2);
                nList.Add(cb3);
                nList.Add(cb4);
                nList.Add(cb5);
                nList.Add(cb6);
                nList.Add(cb7);
                nList.Add(cb8);
                nList.Add(cb9);
                nList.Add(cb10);
                nList.Add(cb11);

                foreach (CheckBoxList nItem in nList)
                {
                    foreach (ListItem item in nItem.Items)
                    {
                        item.Selected = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnClearStep3_OnClick(object sender, EventArgs args)
        {
            try
            {
                Label lblSubIndustryError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblSubIndustryError");
                lblSubIndustryError.Text = string.Empty;

                CheckBoxList cbSub1 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub1");
                CheckBoxList cbSub2 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub2");
                CheckBoxList cbSub3 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub3");
                CheckBoxList cbSub4 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub4");
                CheckBoxList cbSub5 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub5");
                CheckBoxList cbSub6 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub6");
                CheckBoxList cbSub7 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub7");
                CheckBoxList cbSub8 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub8");
                CheckBoxList cbSub9 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub9");
                CheckBoxList cbSub10 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub10");
                CheckBoxList cbSub11 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub11");
                CheckBoxList cbSub12 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub12");
                CheckBoxList cbSub13 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub13");
                CheckBoxList cbSub14 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub14");
                CheckBoxList cbSub15 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub15");
                CheckBoxList cbSub16 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub16");
                CheckBoxList cbSub17 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub17");
                CheckBoxList cbSub18 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub18");
                CheckBoxList cbSub19 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub19");
                CheckBoxList cbSub20 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub20");
                CheckBoxList cbSub21 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub21");
                CheckBoxList cbSub22 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub22");
                CheckBoxList cbSub23 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub23");
                CheckBoxList cbSub24 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub24");
                CheckBoxList cbSub25 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub25");
                CheckBoxList cbSub26 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub26");
                CheckBoxList cbSub27 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub27");
                CheckBoxList cbSub28 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub28");
                CheckBoxList cbSub29 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub29");
                CheckBoxList cbSub30 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub30");
                CheckBoxList cbSub31 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub31");
                CheckBoxList cbSub32 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub32");
                CheckBoxList cbSub33 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub33");
                CheckBoxList cbSub34 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub34");
                CheckBoxList cbSub35 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub35");
                CheckBoxList cbSub36 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub36");

                List<CheckBoxList> nList = new List<CheckBoxList>();
                nList.Add(cbSub1);
                nList.Add(cbSub2);
                nList.Add(cbSub3);
                nList.Add(cbSub4);
                nList.Add(cbSub5);
                nList.Add(cbSub6);
                nList.Add(cbSub7);
                nList.Add(cbSub8);
                nList.Add(cbSub9);
                nList.Add(cbSub10);
                nList.Add(cbSub11);
                nList.Add(cbSub12);
                nList.Add(cbSub13);
                nList.Add(cbSub14);
                nList.Add(cbSub15);
                nList.Add(cbSub16);
                nList.Add(cbSub17);
                nList.Add(cbSub18);
                nList.Add(cbSub19);
                nList.Add(cbSub20);
                nList.Add(cbSub21);
                nList.Add(cbSub22);
                nList.Add(cbSub23);
                nList.Add(cbSub24);
                nList.Add(cbSub25);
                nList.Add(cbSub26);
                nList.Add(cbSub27);
                nList.Add(cbSub28);
                nList.Add(cbSub29);
                nList.Add(cbSub30);
                nList.Add(cbSub31);
                nList.Add(cbSub32);
                nList.Add(cbSub33);
                nList.Add(cbSub34);
                nList.Add(cbSub35);
                nList.Add(cbSub36);

                foreach (CheckBoxList nItem in nList)
                {
                    foreach (ListItem item in nItem.Items)
                    {
                        item.Selected = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnClearStep4_OnClick(object sender, EventArgs args)
        {
            try
            {
                TextBox tbxOverview = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxOverview");
                TextBox tbxDescription = (TextBox)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "TbxDescription");

                Label lblOverViewError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblOverViewError");
                lblOverViewError.Text = string.Empty;
                Label lblDescriptionError = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblDescriptionError");
                lblDescriptionError.Text = string.Empty;

                tbxOverview.Text = string.Empty;
                tbxDescription.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    # region Delete uploaded file

                    DirectoryInfo originaldir = new DirectoryInfo(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"].ToString()) + vSession.User.GuId + "\\");
                    if (originaldir != null)
                    {
                        foreach (FileInfo fi in originaldir.GetFiles())
                        {
                            fi.Delete();
                        }
                    }

                    vSession.User.CompanyLogo = null;
                    vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);

                    # endregion
                }

                //Response.Redirect(ControlLoader.Default, false);
                Response.Redirect(ControlLoader.Default(), false);
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

        #region Combo

        protected void RdBtnUserType_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                HtmlControl divProductDemoLink = (HtmlControl)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "divProductDemoLink");
                divProductDemoLink.Visible = false;

                //RadioButtonList rdBtnUserType = (RadioButtonList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "RdBtnUserType");
                //pnlApi.Visible = rdBtnUserType.SelectedItem.Value == Convert.ToInt32(Types.Resellers).ToString() ? false : true;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DrpStripePlans_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                DropDownList drpStripePlans = (DropDownList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "DrpStripePlans");
                Label lblTotalCostValue = (Label)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "LblTotalCostValue");

                lblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(drpStripePlans.SelectedValue), session).ToString() + " $";
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