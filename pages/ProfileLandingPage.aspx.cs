using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Data;
using WdS.ElioPlus.Objects;
using System.Web;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI;
using System.IO;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using System.Net.Mail;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Windows.Interop;
using DocumentFormat.OpenXml.Spreadsheet;

namespace WdS.ElioPlus.pages
{
    public partial class ProfileLandingPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        public bool IsTechnologyType
        {
            get
            {
                if (ViewState["IsTechnologyType"] != null)
                    return Convert.ToBoolean(ViewState["IsTechnologyType"].ToString());
                else
                    return false;
            }
            set
            {
                ViewState["IsTechnologyType"] = value;
            }
        }

        public enum Navigation
        {
            None,
            First,
            Next,
            Previous,
            Last,
            Pager,
            Sorting
        }

        public int NowViewing
        {
            get
            {
                object obj = ViewState["_NowViewing"];
                if (obj == null)
                    return 0;
                else
                    return (int)obj;
            }
            set
            {
                this.ViewState["_NowViewing"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                //scriptManager.RegisterPostBackControl(aClaimProfile);
                //scriptManager.RegisterPostBackControl(aGetAQuote);

                ElioUsers user = null;
                bool isError = false;
                string errorPage = string.Empty;
                string key = string.Empty;
                
                RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, session);

                if (isError)
                {
                    if (user != null && (user.AccountStatus == (int)AccountStatus.NotCompleted || user.IsPublic == Convert.ToInt32(AccountStatus.NotPublic) && user.CompanyType == EnumHelper.GetDescription(Types.Resellers)))
                    {
                        bool hasPersonData = ClearbitSql.ExistsClearbitPerson(user.Id, session);
                        bool hasCompanyData = ClearbitSql.ExistsClearbitCompany(user.Id, session);

                        if ((!hasPersonData || !hasCompanyData) && user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString() && user.AccountStatus == (int)AccountStatus.NotCompleted)
                        {
                            bool success = ClearBit.FindCombinedPersonCompanyByEmail_v2(user, user.Email, session);
                            if (!success)
                            {
                                Response.Redirect(errorPage, false);
                                return;
                            }
                        }
                        else
                        {
                            Response.Redirect(errorPage, false);
                            return;
                        }
                    }
                    else
                    {
                        Response.Redirect(errorPage, false);
                        return;
                    }
                }

                vSession.ElioCompanyDetailsView = user;

                if (!IsPostBack)
                {
                    UpdateStrings();

                    try
                    {
                        GlobalDBMethods.AddCompanyViews(vSession.User, vSession.ElioCompanyDetailsView, vSession.Lang, session);
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                FixPage();
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

        private void PageTitle()
        {
            string metaDescription = "";
            string metaKeywords = "";

            string pgTitle = GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.ElioCompanyDetailsView, out metaDescription, out metaKeywords, session);

            HtmlControl metaHeadDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaHeadKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaHeadDescription.Attributes["content"] = metaDescription;
            metaHeadKeywords.Attributes["content"] = metaKeywords;
        }

        private void UpdateStrings()
        {
            LblCompanyType.Text = vSession.ElioCompanyDetailsView.CompanyType;
            //ImgCompanyLogo.Alt = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "literal", "1")).Text;
            //LblOverviewTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "3")).Text;
            //LblDescriptionTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "4")).Text;
            //LblCompanyInfoTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "6")).Text;
            //LblMessageHeader.Text = "Send Message";
            //BtnMessageSend.Text = "Send it";
            //BtnMessageCancel.Text = "Cancel";
            //BtnCloseModal.Text = BtnCloseReview.Text = "X";
            //LblReviewHeader.Text = "Add Review";
            //BtnAddReview.Text = "Submit Review";
            //BtnCancelReview.Text = "Cancel";
            //LblWarningReview.Text = "Error! ";
            //LblSuccessReview.Text = "Done! ";
            //LblWarningMsg.Text = "Error! ";
            //LblSuccessMsg.Text = "Done! ";

            LblClaimMessageHeader.Text = "Claim Profile Message Form";
            //LblClaimWarningMsg.Text = "Error! ";
            //LblClaimSuccessMsg.Text = "Done!";
            //BtnCancelMessage.Text = "Close";
            //BtnCloseClaimMsg.Text = "X";
            BtnSendClaim.Text = "Send it";
            BtnCancelClaimMsg.Text = "Cancel";
            LblClaimMessageHeader.Text = "Claim Profile Message Form";
            //BtnCloseMessage.Text = "X";
            //LblMessageGoFull.Text = "Create a full profile for free!";
            //LblGoFullTitle.Text = "Complete your registration in order to have access to this feature! ";

            //BtnCancelMessage.Text = "Close";
            //LblClaimMessageHeader.Text = "Claim Profile Message Form";
            //LblClaimWarningMsg.Text = "Error! ";
            //LblClaimSuccessMsg.Text = "Done!";
            //BtnCancelMessage.Text = "Close";
            //BtnCloseClaimMsg.Text = "X";
            //BtnSendClaim.Text = "Send it";
            //BtnCancelClaimMsg.Text = "Cancel";
            //LblClaimMessageHeader.Text = "Claim Profile Message Form";
        }

        private void FixButtons()
        {
            if (vSession.ElioCompanyDetailsView != null)
            {
                //aAddToMyMatches.Visible = false;  //vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();
                //aSuccess.Visible = false;
            }
            else
                Response.Redirect(ControlLoader.Search, false);

            //if (vSession.User != null)
            //{
            //    if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
            //    {
            //        if (vSession.User.CompanyType == Types.Vendors.ToString())
            //        {
            //            if (vSession.ElioCompanyDetailsView.CompanyType != Types.Vendors.ToString())
            //            {
            //                bool isAlreadyConnection = Sql.IsConnection(vSession.User.Id, vSession.ElioCompanyDetailsView.Id, session);
            //                if (!isAlreadyConnection)
            //                {
            //                    aSuccess.Visible = false;
            //                }
            //                else
            //                {
            //                    aAddToMyMatches.Visible = false;
            //                    aSuccess.Visible = true;
            //                }

            //                if (aAddToMyMatches.Visible)
            //                {
            //                    if (vSession.User.BillingType == (int)BillingTypePacket.FreemiumPacketType)
            //                    {
            //                        aAddToMyMatches.HRef = "https://calendly.com/elioplus";
            //                    }
            //                    else
            //                    {
            //                        aAddToMyMatches.ServerClick += aAddToMyMatches_ServerClick;
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            aAddToMyMatches.Visible = false;
            //            aSuccess.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        aAddToMyMatches.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
            //        aSuccess.Visible = false;
            //    }
            //}
            //else
            //{
            //    aAddToMyMatches.ServerClick += aAddToMyMatches_ServerClick;
            //    aSuccess.Visible = false;
            //}
        }

        private void FixPage()
        {
            PageTitle();
            FixButtons();
            SetLinks();

            aClaimProfile.Visible = (vSession.ElioCompanyDetailsView.UserApplicationType == (int)UserApplicationType.ThirdParty && !ClearbitSql.IsPersonProfileClaimed(vSession.ElioCompanyDetailsView.Id, session));
            if (aClaimProfile.Visible)
            {
                aClaimProfile.HRef = "/" + vSession.ElioCompanyDetailsView.Id + ControlLoader.ClaimProfile;
                aClaimProfile.Target = "_blank";
            }

            LoadDetails();

            if (!IsPostBack)
            {
                if (vSession.User != null)
                    LoadRequestDemoData();
            }

            if (vSession.ElioCompanyDetailsView != null)
            {
                if (vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers) && vSession.ElioCompanyDetailsView.AccountStatus == (int)AccountStatus.Completed)
                {
                    divDescriptionArea.Visible = false;
                }
            }

            if (vSession.ElioCompanyDetailsView.BillingType == (int)BillingTypePacket.FreemiumPacketType)
                LoadRepeater(Navigation.None);
        }

        protected void LoadRepeater(Navigation navigation)
        {
            bool mustClose = false;

            if (session.Connection.State == ConnectionState.Closed)
            {
                session.OpenConnection();
                mustClose = true;
            }

            divWarningMsg.Visible = false;

            List<ElioUsersSearchInfo> users = Sql.GetUsersWithSameSubIndustriesGroupItemsByCompanyTypeDataTable(vSession.ElioCompanyDetailsView, session);

            if (users.Count > 0)
            {
                divSimilarResults.Visible = true;

                //Create the object of PagedDataSource
                PagedDataSource objPds = new PagedDataSource();

                //Assign our data source to PagedDataSource object
                objPds.DataSource = users;

                //Set the allow paging to true
                objPds.AllowPaging = true;

                //Set the number of items you want to show
                objPds.PageSize = 100;

                //Based on navigation manage the NowViewing
                switch (navigation)
                {
                    case Navigation.Next:       //Increment NowViewing by 1
                        NowViewing++;
                        break;
                    case Navigation.Previous:   //Decrement NowViewing by 1
                        NowViewing--;
                        break;
                    default:                    //Default NowViewing set to 0
                        NowViewing = 0;
                        break;
                }

                //Set the current page index
                objPds.CurrentPageIndex = NowViewing;

                //Assign PagedDataSource to repeater
                RdgResults.DataSource = objPds;
                RdgResults.DataBind();

                LblSimilarCompanies.Text = (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) ? "Similar Vendors you might also be interest in:" : "Similar Channel Partners you might also be interest in:";

                #region Custom Ads Acronis

                if (vSession.ElioCompanyDetailsView.CompanyRegion == "Asia Pacific" || vSession.ElioCompanyDetailsView.CompanyRegion == "Africa" || vSession.ElioCompanyDetailsView.CompanyRegion == "Middle East" || vSession.ElioCompanyDetailsView.CompanyRegion == "")
                {
                    divAcronisAds.Visible = divAcronisAds2.Visible = true;

                    LblAdv.Text = "Interested in advertising on our platform? Reach out to us here:";
                    //RttpAcronisAds.Text = RttpAcronisAds2.Text = "Interested in advertising on our platform? Reach out to us here:<b><a href=\"https://elioplus.com/contact-us\" target=\"_blank\"> Contact Us</a></b>";
                }
                else
                {
                    divSimilarResults.Attributes["class"] = "grid-cols-1 lg:grid-cols-company gap-30px lg:gap-50px";
                }

                #endregion
            }
            else
            {
                RdgResults.DataSource = null;
                RdgResults.DataBind();

                NowViewing = 0;
            }

            if (mustClose)
                session.CloseConnection();
        }

        private void SetLinks()
        {

        }

        private void LoadRequestDemoData()
        {
            if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
            {
                if (!string.IsNullOrEmpty(vSession.User.FirstName))
                    TbxFirstName.Text = vSession.User.FirstName;

                if (!string.IsNullOrEmpty(vSession.User.LastName))
                    TbxLastName.Text = vSession.User.LastName;

                if (!string.IsNullOrEmpty(vSession.User.Email))
                    TbxCompanyEmail.Text = vSession.User.Email;

                if (!string.IsNullOrEmpty(vSession.User.CompanyName))
                    TbxBusinessName.Text = vSession.User.CompanyName;
            }
            else
            {
                if (!string.IsNullOrEmpty(vSession.User.Email))
                    TbxCompanyEmail.Text = vSession.User.Email;
            }
        }

        private void LoadIndustries()
        {
            PhIndustries1.Controls.Clear();
            PhIndustries2.Controls.Clear();
            PhIndustries3.Controls.Clear();

            List<ElioIndustries> userIndustries = Sql.GetUsersIndustries(vSession.ElioCompanyDetailsView.Id, session);

            divIndustries.Visible = userIndustries.Count > 0;

            if (userIndustries.Count > 0)
            {
                int count = 0;
                foreach (ElioIndustries industry in userIndustries)
                {
                    if (count == 3) count = 0;

                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.Attributes["class"] = "w-full flex gap-10px items-center";

                    HtmlImage img = new HtmlImage();
                    img.Src = "/assets_out/images/pricing/check.svg";

                    Label lblInd = new Label();
                    lblInd.ID = "lblInd_" + industry.Id.ToString();
                    lblInd.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                    lblInd.Text = industry.IndustryDescription;

                    div1.Controls.Clear();
                    div1.Controls.Add(img);
                    div1.Controls.Add(lblInd);

                    if (count == 0)
                        PhIndustries1.Controls.Add(div1);
                    else if (count == 1)
                        PhIndustries2.Controls.Add(div1);
                    else if (count == 2)
                        PhIndustries3.Controls.Add(div1);

                    count++;
                }
            }
        }

        private void LoadMarkets()
        {
            PhMarkets1.Controls.Clear();
            PhMarkets2.Controls.Clear();
            PhMarkets3.Controls.Clear();

            List<ElioMarkets> userMarkets = Sql.GetUsersMarkets(vSession.ElioCompanyDetailsView.Id, session);

            divMarkets.Visible = userMarkets.Count > 0;

            if (userMarkets.Count > 0)
            {
                int count = 0;
                foreach (ElioMarkets market in userMarkets)
                {
                    if (count == 3) count = 0;

                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.Attributes["class"] = "w-full flex gap-10px items-center";

                    HtmlImage img = new HtmlImage();
                    img.Src = "/assets_out/images/pricing/check.svg";

                    Label lblInt = new Label();
                    lblInt.ID = "lblInt_" + market.Id.ToString();
                    lblInt.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                    lblInt.Text = market.MarketDescription;

                    div1.Controls.Clear();
                    div1.Controls.Add(img);
                    div1.Controls.Add(lblInt);

                    if (count == 0)
                        PhMarkets1.Controls.Add(div1);
                    else if (count == 1)
                        PhMarkets2.Controls.Add(div1);
                    else if (count == 2)
                        PhMarkets3.Controls.Add(div1);

                    count++;
                }
            }
        }

        private string GetProgram(string userPartnerProgram)
        {
            if (userPartnerProgram == "White Label")
            {
                return "/white-label-partner-programs";
            }
            else if (userPartnerProgram == "Reseller")
            {
                return "/saas-partner-programs";
            }
            else if (userPartnerProgram == "Managed Service Provider")
            {
                return "/msps-partner-programs";
            }
            else if (userPartnerProgram == "System Integrator")
            {
                return "/systems-integrators-partner-programs";
            }
            else
                return "";
        }

        private void LoadProgramsForVendors()
        {
            divVendorsPartnerPrograms.Visible = true;
            divResellersPartnerPrograms.Visible = false;

            PhPartPrV1.Controls.Clear();
            PhPartPrV2.Controls.Clear();
            PhPartPrV3.Controls.Clear();

            List<ElioPartners> userPrograms = Sql.GetUsersPartners(vSession.ElioCompanyDetailsView.Id, session);
            if (userPrograms.Count > 0)
            {
                int count = 0;
                foreach (ElioPartners program in userPrograms)
                {
                    if (count == 3) count = 0;

                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.Attributes["class"] = "w-full flex gap-10px items-center";

                    HtmlGenericControl div = new HtmlGenericControl();
                    div.Attributes["class"] = "w-[15px] h-[15px] rounded-full bg-blue";

                    HtmlAnchor aPartPrV = new HtmlAnchor();
                    aPartPrV.ID = "aPartPrV_" + program.Id.ToString();
                    aPartPrV.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                    aPartPrV.HRef = GetProgram(program.PartnerDescription);

                    Label lblPartPrV = new Label();
                    lblPartPrV.ID = "lblPartPrV_" + program.Id.ToString();
                    lblPartPrV.Text = program.PartnerDescription;

                    aPartPrV.Controls.Clear();
                    aPartPrV.Controls.Add(lblPartPrV);

                    div1.Controls.Clear();
                    div1.Controls.Add(div);
                    div1.Controls.Add(aPartPrV);

                    if (count == 0)
                        PhPartPrV1.Controls.Add(div1);
                    else if (count == 1)
                        PhPartPrV2.Controls.Add(div1);
                    else if (count == 2)
                        PhPartPrV3.Controls.Add(div1);

                    count++;
                }
            }
            else
            {
                divVendorsPartnerPrograms.Visible = false;
            }
        }

        private void LoadProgramsForResellers()
        {
            divResellersPartnerPrograms.Visible = true;
            divVendorsPartnerPrograms.Visible = false;

            PhPartPrR1.Controls.Clear();
            PhPartPrR2.Controls.Clear();
            PhPartPrR3.Controls.Clear();

            List<ElioPartners> userPrograms = Sql.GetUsersPartners(vSession.ElioCompanyDetailsView.Id, session);

            //LblPartnerProgramTitle.Visible = userPrograms.Count > 0;

            if (userPrograms.Count > 0)
            {
                int count = 0;
                foreach (ElioPartners program in userPrograms)
                {
                    if (count == 3) count = 0;

                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.Attributes["class"] = "w-full flex gap-10px items-center";

                    HtmlImage img = new HtmlImage();
                    img.Src = "/assets_out/images/pricing/check.svg";

                    Label lblPartPrR = new Label();
                    lblPartPrR.ID = "lblPartPrR_" + program.Id.ToString();
                    lblPartPrR.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                    lblPartPrR.Text = program.PartnerDescription;

                    div1.Controls.Clear();
                    div1.Controls.Add(img);
                    div1.Controls.Add(lblPartPrR);

                    if (count == 0)
                        PhPartPrR1.Controls.Add(div1);
                    else if (count == 1)
                        PhPartPrR2.Controls.Add(div1);
                    else if (count == 2)
                        PhPartPrR3.Controls.Add(div1);

                    count++;
                }
            }
            else
            {
                divResellersPartnerPrograms.Visible = false;
                divVendorsPartnerPrograms.Visible = false;
            }
        }

        private void LoadVendorIntegrations()
        {
            divChannelPartnersProducts.Visible = false;
            divVendorsIntegrations.Visible = true;

            PhVendorsIntegrations1.Controls.Clear();
            PhVendorsIntegrations2.Controls.Clear();
            PhVendorsIntegrations3.Controls.Clear();

            List<ElioRegistrationIntegrations> items = Sql.GetUserRegistrationIntegrations(vSession.ElioCompanyDetailsView.Id, session);
            
            if (items.Count > 0)
            {
                int count = 0;
                foreach (ElioRegistrationIntegrations item in items)
                {
                    if (count == 3) count = 0;

                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.Attributes["class"] = "w-full flex gap-10px items-center";

                    HtmlImage img = new HtmlImage();
                    img.Src = "/assets_out/images/pricing/check.svg";

                    Label lblInt = new Label();
                    lblInt.ID = "lblInt_" + item.Id.ToString();
                    lblInt.Text = item.Description;

                    div1.Controls.Clear();
                    div1.Controls.Add(img);
                    div1.Controls.Add(lblInt);

                    if (count == 0)
                        PhVendorsIntegrations1.Controls.Add(div1);
                    else if (count == 1)
                        PhVendorsIntegrations2.Controls.Add(div1);
                    else if (count == 2)
                        PhVendorsIntegrations3.Controls.Add(div1);

                    count++;
                }
            }
            else
            {
                divVendorsIntegrations.Visible = false;
            }
        }

        private void LoadChannelPartnerProductsInPlaceHolder()
        {
            divChannelPartnersProducts.Visible = true;
            divVendorsIntegrations.Visible = false;

            string productUrl = "/profile/channel-partners/";
            List<ElioRegistrationProducts> userProducts = Sql.GetRegistrationProductsDescriptionByUserId(vSession.ElioCompanyDetailsView.Id, session);

            if (userProducts.Count > 0)
            {
                bool hasCity = false;
                string cityURL = "/";
                if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
                {
                    hasCity = true;
                    divCityProduct.Visible = true;
                    //cityURL = vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();

                    if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.CompanyRegion) && !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Country) && !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
                    {
                        if (vSession.ElioCompanyDetailsView.Country == "India" || vSession.ElioCompanyDetailsView.Country == "United States" || vSession.ElioCompanyDetailsView.Country == "United Kingdom" || vSession.ElioCompanyDetailsView.Country == "Australia")
                        {
                            string state = Sql.GetStateByRegionCountryCityTbl(vSession.ElioCompanyDetailsView.CompanyRegion, vSession.ElioCompanyDetailsView.Country, vSession.ElioCompanyDetailsView.City, session);
                            if (state != "")
                                cityURL += vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + state.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
                            else
                                cityURL += vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
                        }
                        else
                            cityURL += vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
                    }
                    else
                        cityURL += vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
                }

                PhProductsCityContent1.Controls.Clear();
                PhProductsCityContent2.Controls.Clear();
                PhProductsCityContent3.Controls.Clear();

                PhProductsContent1.Controls.Clear();
                PhProductsContent2.Controls.Clear();
                PhProductsContent3.Controls.Clear();

                PhProductsCityContentTrans1.Controls.Clear();
                PhProductsCityContentTrans2.Controls.Clear();
                PhProductsCityContentTrans3.Controls.Clear();

                PhProductsCountryContentTrans1.Controls.Clear();
                PhProductsCountryContentTrans2.Controls.Clear();
                PhProductsCountryContentTrans3.Controls.Clear();

                int count = 0;
                foreach (ElioRegistrationProducts product in userProducts)
                {
                    if (count == 3) count = 0;

                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.Attributes["class"] = "w-full flex gap-10px items-center";

                    HtmlGenericControl div = new HtmlGenericControl();
                    div.Attributes["class"] = "w-[15px] h-[15px] rounded-full bg-blue";

                    HtmlAnchor aProd = new HtmlAnchor();
                    aProd.ID = "aProd_" + product.Id.ToString();
                    aProd.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                    aProd.HRef = productUrl + product.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                    Label lblProd = new Label();
                    lblProd.ID = "lblProd_" + product.Id.ToString();
                    lblProd.Text = product.Description;

                    aProd.Controls.Clear();
                    aProd.Controls.Add(lblProd);

                    div1.Controls.Clear();
                    div1.Controls.Add(div);
                    div1.Controls.Add(aProd);

                    if (hasCity)
                    {
                        LblCityProductTitle.Text = "Products in city: " + vSession.ElioCompanyDetailsView.City;

                        HtmlGenericControl divC1 = new HtmlGenericControl();
                        divC1.Attributes["class"] = "w-full flex gap-10px items-center";

                        HtmlGenericControl divC = new HtmlGenericControl();
                        divC.Attributes["class"] = "w-[15px] h-[15px] rounded-full bg-blue";

                        HtmlAnchor aProdCity = new HtmlAnchor();
                        aProdCity.ID = "aProdCity_" + product.Id.ToString();
                        aProdCity.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                        aProdCity.HRef = cityURL + "/channel-partners/" + product.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                        Label lblProdCity = new Label();
                        lblProdCity.ID = "lblCity_" + product.Id.ToString();
                        lblProdCity.Text = product.Description + " in " + vSession.ElioCompanyDetailsView.City;

                        aProdCity.Controls.Clear();
                        aProdCity.Controls.Add(lblProdCity);

                        divC1.Controls.Clear();
                        divC1.Controls.Add(divC);
                        divC1.Controls.Add(aProdCity);

                        if (count == 0)
                        {
                            PhProductsCityContent1.Controls.Add(divC1);
                            LoadCityProductTrans(product, false, PhProductsCityContentTrans1);
                        }
                        else if (count == 1)
                        {
                            PhProductsCityContent2.Controls.Add(divC1);
                            LoadCityProductTrans(product, false, PhProductsCityContentTrans2);
                        }
                        else if (count == 2)
                        {
                            PhProductsCityContent3.Controls.Add(divC1);
                            LoadCityProductTrans(product, false, PhProductsCityContentTrans3);
                        }
                    }

                    if (count == 0)
                    {
                        PhProductsContent1.Controls.Add(div1);
                        LoadCountryProductTrans(product, true, PhProductsCountryContentTrans1);
                    }
                    else if (count == 1)
                    {
                        PhProductsContent2.Controls.Add(div1);
                        LoadCountryProductTrans(product, true, PhProductsCountryContentTrans2);
                    }
                    else if (count == 2)
                    {
                        PhProductsContent3.Controls.Add(div1);
                        LoadCountryProductTrans(product, true, PhProductsCountryContentTrans3);
                    }

                    count++;
                }
            }
            else
            {
                divChannelPartnersProducts.Visible = false;
            }
        }

        private void LoadUserVerticalsInPlaceHolder()
        {
            bool existCountry = true;
            string urlLink = "/";

            if (vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers))
            {
                if (existCountry)
                    urlLink += vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/channel-partners/";
                else
                    urlLink += "profile/channel-partners/";
            }

            bool hasPartnerProgram = false;
            if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
            {
                hasPartnerProgram = Sql.HasUsersPartnerProgram(vSession.ElioCompanyDetailsView.Id, "White Label", session);
            }

            //if (vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers))
            //{
            List<ElioSubIndustriesGroupItems> userVarticals = Sql.GetUserSubIndustriesGroupItems(vSession.ElioCompanyDetailsView.Id, session);

            if (userVarticals.Count > 0)
            {
                bool hasCity = false;
                string cityURL = "/";
                if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
                {
                    hasCity = true;
                    divCityCategory.Visible = true;
                    LblSimilarCityCompanyTitle.Text = string.Format("Find similar companies in {0}: ", vSession.ElioCompanyDetailsView.City);

                    if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.CompanyRegion) && !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Country) && !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
                    {
                        if (vSession.ElioCompanyDetailsView.Country == "India" || vSession.ElioCompanyDetailsView.Country == "United States" || vSession.ElioCompanyDetailsView.Country == "United Kingdom" || vSession.ElioCompanyDetailsView.Country == "Australia")
                        {
                            string state = Sql.GetStateByRegionCountryCityTbl(vSession.ElioCompanyDetailsView.CompanyRegion, vSession.ElioCompanyDetailsView.Country, vSession.ElioCompanyDetailsView.City, session);
                            if (state != "")
                                cityURL += vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + state.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
                            else
                                cityURL += vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
                        }
                        else
                            cityURL += vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
                    }
                    else
                        cityURL += vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
                }

                PhVerticalsContent1.Controls.Clear();
                PhVerticalsContent2.Controls.Clear();
                PhVerticalsContent3.Controls.Clear();

                PhVerticalsCityContent1.Controls.Clear();
                PhVerticalsCityContent2.Controls.Clear();
                PhVerticalsCityContent3.Controls.Clear();

                PhVerticalsCityContentTrans1.Controls.Clear();
                PhVerticalsCityContentTrans2.Controls.Clear();
                PhVerticalsCityContentTrans3.Controls.Clear();

                PhVerticalsCountryContentTrans1.Controls.Clear();
                PhVerticalsCountryContentTrans2.Controls.Clear();
                PhVerticalsCountryContentTrans3.Controls.Clear();

                int count = 0;
                foreach (ElioSubIndustriesGroupItems userVartical in userVarticals)
                {
                    if (count == 3) count = 0;

                    string verticalURL = userVartical.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.Attributes["class"] = "w-full flex gap-10px items-center";

                    HtmlGenericControl div = new HtmlGenericControl();
                    div.Attributes["class"] = "w-[15px] h-[15px] rounded-full bg-blue";

                    HtmlAnchor aVert = new HtmlAnchor();
                    aVert.ID = "aVert_" + userVartical.Id.ToString();
                    aVert.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                    aVert.HRef = (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) ? ControlLoader.SubIndustryProfiles("vendors", verticalURL) : urlLink + verticalURL;

                    Label lblVert = new Label();
                    lblVert.ID = "lblVert_" + userVartical.Id.ToString();
                    lblVert.Text = userVartical.Description;

                    aVert.Controls.Clear();
                    aVert.Controls.Add(lblVert);

                    div1.Controls.Clear();
                    div1.Controls.Add(div);
                    div1.Controls.Add(aVert);

                    if (count == 0)
                    {
                        PhVerticalsContent1.Controls.Add(div1);
                    }
                    else if (count == 1)
                    {
                        PhVerticalsContent2.Controls.Add(div1);
                    }
                    else if (count == 2)
                    {
                        PhVerticalsContent3.Controls.Add(div1);
                    }

                    if (hasCity)
                    {
                        HtmlGenericControl divC1 = new HtmlGenericControl();
                        divC1.Attributes["class"] = "w-full flex gap-10px items-center";

                        HtmlGenericControl divC = new HtmlGenericControl();
                        divC.Attributes["class"] = "w-[15px] h-[15px] rounded-full bg-blue";

                        HtmlAnchor aVertCity = new HtmlAnchor();
                        aVertCity.ID = "aVertCity_" + userVartical.Id.ToString();
                        aVertCity.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                        aVertCity.HRef = (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) ? ControlLoader.SubIndustryProfiles("vendors", cityURL + "/" + verticalURL) : cityURL + "/channel-partners/" + verticalURL;

                        Label lblVertCity = new Label();
                        lblVertCity.ID = "lblVertCity_" + userVartical.Id.ToString();
                        lblVertCity.Text = userVartical.Description + " in " + vSession.ElioCompanyDetailsView.City;

                        aVertCity.Controls.Clear();
                        aVertCity.Controls.Add(lblVertCity);

                        divC1.Controls.Clear();
                        divC1.Controls.Add(divC);
                        divC1.Controls.Add(aVertCity);

                        if (count == 0)
                        {
                            PhVerticalsCityContent1.Controls.Add(divC1);
                            LoadCityVerticalTrans(userVartical, false, PhVerticalsCityContentTrans1);
                        }
                        else if (count == 1)
                        {
                            PhVerticalsCityContent2.Controls.Add(divC1);
                            LoadCityVerticalTrans(userVartical, false, PhVerticalsCityContentTrans2);
                        }
                        else if (count == 2)
                        {
                            PhVerticalsCityContent3.Controls.Add(divC1);
                            LoadCityVerticalTrans(userVartical, false, PhVerticalsCityContentTrans3);
                        }
                    }

                    if (hasPartnerProgram)
                    {
                        HtmlAnchor aTag = new HtmlAnchor();
                        aTag.ID = "aTag_" + userVartical.Id.ToString();
                        aTag.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                        aTag.HRef = "white-label/vendors/" + verticalURL;

                        Label lblTag = new Label();
                        lblTag.ID = "lblTag_" + userVartical.Id.ToString();
                        lblTag.Text = "White Label " + userVartical.Description;

                        aTag.Controls.Clear();
                        aTag.Controls.Add(lblTag);

                        //PhTagsContent.Controls.Add(aTag);
                    }

                    if (count == 0)
                    {
                        LoadCountryVerticalTrans(userVartical, existCountry, PhVerticalsCountryContentTrans1);
                    }
                    else if (count == 1)
                    {
                        LoadCountryVerticalTrans(userVartical, existCountry, PhVerticalsCountryContentTrans2);
                    }
                    else if (count == 2)
                    {
                        LoadCountryVerticalTrans(userVartical, existCountry, PhVerticalsCountryContentTrans3);
                    }

                    count++;
                }
            }
            else
            {
                divCategories.Visible = false;
            }
            //}
            //else
            //{
            //    divCategories.Visible = false;
            //}
        }

        private void LoadCityVerticalTrans(ElioSubIndustriesGroupItems category, bool existCountry, PlaceHolder phVerticalsCityContentTrans)
        {
            bool hasCity = false;
            string urlLink = "";
            string cityURL = "";
            bool hasTrans = false;

            if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
            {
                hasCity = true;
                cityURL = vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
            }

            if (hasCity)
            {
                if (existCountry)
                    urlLink = vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/channel-partners/";
                else
                    urlLink = "profile/channel-partners/";

                string verticalURL = category.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                //divCountryCityTransArea.Visible = true;
                divCityVerticalsTrans.Visible = true;
                LblSimilarCityCompanyTitleTrans.Text = "Categories in: " + vSession.ElioCompanyDetailsView.City;

                if (vSession.ElioCompanyDetailsView.Country.ToLower() == "france" || vSession.ElioCompanyDetailsView.Country.ToLower() == "canada")
                {
                    hasTrans = true;
                    cityURL = "fr/" + cityURL;
                    LblSimilarCityCompanyTitleTrans.Text = "Trouvez des entreprises similaires à " + vSession.ElioCompanyDetailsView.City;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "austria")
                {
                    hasTrans = true;
                    cityURL = "at/" + cityURL;
                    LblSimilarCityCompanyTitleTrans.Text = "Ähnliche Unternehmen in " + vSession.ElioCompanyDetailsView.City + " finden: ";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "germany")
                {
                    hasTrans = true;
                    cityURL = "de/" + cityURL;
                    LblSimilarCityCompanyTitleTrans.Text = "Ähnliche Unternehmen in " + vSession.ElioCompanyDetailsView.City + " finden: ";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "brazil" || vSession.ElioCompanyDetailsView.Country.ToLower() == "portugal")
                {
                    hasTrans = true;
                    cityURL = "pt/" + cityURL;
                    LblSimilarCityCompanyTitleTrans.Text = "Encontre empresas similares em " + vSession.ElioCompanyDetailsView.City;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "italy")
                {
                    hasTrans = true;
                    cityURL = "it/" + cityURL;
                    LblSimilarCityCompanyTitleTrans.Text = "Trova aziende simili a " + vSession.ElioCompanyDetailsView.City;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "argentina" || vSession.ElioCompanyDetailsView.Country.ToLower() == "bolivia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "chile" || vSession.ElioCompanyDetailsView.Country.ToLower() == "colombia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "costa rica" ||
                    vSession.ElioCompanyDetailsView.Country.ToLower() == "dominican republic" || vSession.ElioCompanyDetailsView.Country.ToLower() == "ecuador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "el salvador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "guatemala" || vSession.ElioCompanyDetailsView.Country.ToLower() == "honduras" ||
                    vSession.ElioCompanyDetailsView.Country.ToLower() == "mexico" || vSession.ElioCompanyDetailsView.Country.ToLower() == "panama" || vSession.ElioCompanyDetailsView.Country.ToLower() == "paraguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "peru" || vSession.ElioCompanyDetailsView.Country.ToLower() == "puerto rico" ||
                     vSession.ElioCompanyDetailsView.Country.ToLower() == "uruguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "venezuela")
                {
                    hasTrans = true;
                    cityURL = "la/" + cityURL;
                    LblSimilarCityCompanyTitleTrans.Text = "Encuentre empresas similares en " + vSession.ElioCompanyDetailsView.City;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "spain")
                {
                    hasTrans = true;
                    cityURL = "es/" + cityURL;
                    LblSimilarCityCompanyTitleTrans.Text = "Encuentra empresas similares en " + vSession.ElioCompanyDetailsView.City;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "netherlands")
                {
                    hasTrans = true;
                    cityURL = "nl/" + cityURL;
                    LblSimilarCityCompanyTitleTrans.Text = "Zoek vergelijkbare bedrijven in " + vSession.ElioCompanyDetailsView.City;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "poland")
                {
                    hasTrans = true;
                    cityURL = "pl/" + cityURL;
                    LblSimilarCityCompanyTitleTrans.Text = "Znajdź podobne firmy w " + vSession.ElioCompanyDetailsView.City;
                }

                if (hasTrans)
                {
                    if (!cityURL.StartsWith("/"))
                        cityURL = "/" + cityURL;

                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.Attributes["class"] = "w-full flex gap-10px items-center";

                    HtmlGenericControl div = new HtmlGenericControl();
                    div.Attributes["class"] = "w-[15px] h-[15px] rounded-full bg-blue";

                    HtmlAnchor aVerticalsCity = new HtmlAnchor();
                    aVerticalsCity.ID = "aVerticalsCity_" + category.Id.ToString();
                    aVerticalsCity.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                    aVerticalsCity.HRef = cityURL + "/channel-partners/" + verticalURL;

                    Label lblVerticalsCity = new Label();
                    lblVerticalsCity.ID = "lblVerticalsCity_" + category.Id.ToString();
                    lblVerticalsCity.Text = category.Description + " in " + vSession.ElioCompanyDetailsView.City;

                    aVerticalsCity.Controls.Clear();
                    aVerticalsCity.Controls.Add(lblVerticalsCity);

                    div1.Controls.Clear();
                    div1.Controls.Add(div);
                    div1.Controls.Add(aVerticalsCity);

                    phVerticalsCityContentTrans.Controls.Add(div1);
                }
                else
                    divCityVerticalsTrans.Visible = false;
            }
        }

        private void LoadCityProductTrans(ElioRegistrationProducts category, bool existCountry, PlaceHolder phProductsCityContentTrans)
        {
            bool hasCity = false;
            string urlLink = "";
            string cityURL = "";
            bool hasTrans = false;
            if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
            {
                hasCity = true;

                cityURL = vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
            }

            if (hasCity)
            {
                if (existCountry)
                    urlLink = vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/channel-partners/";
                else
                    urlLink = "profile/channel-partners/";

                string verticalURL = category.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                divCityProductTrans.Visible = true;
                LblCityProductTransTitle.Text = "Products in city: " + vSession.ElioCompanyDetailsView.City + " (Translated pages)";

                if (vSession.ElioCompanyDetailsView.Country.ToLower() == "france" || vSession.ElioCompanyDetailsView.Country.ToLower() == "canada")
                {
                    hasTrans = true;
                    cityURL = "fr/" + cityURL;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "austria")
                {
                    hasTrans = true;
                    cityURL = "at/" + cityURL;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "germany")
                {
                    hasTrans = true;
                    cityURL = "de/" + cityURL;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "brazil" || vSession.ElioCompanyDetailsView.Country.ToLower() == "portugal")
                {
                    hasTrans = true;
                    cityURL = "pt/" + cityURL;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "italy")
                {
                    hasTrans = true;
                    cityURL = "it/" + cityURL;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "argentina" || vSession.ElioCompanyDetailsView.Country.ToLower() == "bolivia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "chile" || vSession.ElioCompanyDetailsView.Country.ToLower() == "colombia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "costa rica" ||
                    vSession.ElioCompanyDetailsView.Country.ToLower() == "dominican republic" || vSession.ElioCompanyDetailsView.Country.ToLower() == "ecuador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "el salvador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "guatemala" || vSession.ElioCompanyDetailsView.Country.ToLower() == "honduras" ||
                    vSession.ElioCompanyDetailsView.Country.ToLower() == "mexico" || vSession.ElioCompanyDetailsView.Country.ToLower() == "panama" || vSession.ElioCompanyDetailsView.Country.ToLower() == "paraguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "peru" || vSession.ElioCompanyDetailsView.Country.ToLower() == "puerto rico" ||
                     vSession.ElioCompanyDetailsView.Country.ToLower() == "uruguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "venezuela")
                {
                    hasTrans = true;
                    cityURL = "la/" + cityURL;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "spain")
                {
                    hasTrans = true;
                    cityURL = "es/" + cityURL;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "netherlands")
                {
                    hasTrans = true;
                    cityURL = "nl/" + cityURL;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "poland")
                {
                    hasTrans = true;
                    cityURL = "pl/" + cityURL;
                }

                if (hasTrans)
                {
                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.Attributes["class"] = "w-full flex gap-10px items-center";

                    HtmlGenericControl div = new HtmlGenericControl();
                    div.Attributes["class"] = "w-[15px] h-[15px] rounded-full bg-blue";

                    HtmlAnchor aProductsCity = new HtmlAnchor();
                    aProductsCity.ID = "aProductsCity_" + category.Id.ToString();
                    aProductsCity.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                    aProductsCity.HRef = (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) ? ControlLoader.SubIndustryProfiles("vendors", cityURL + "/" + verticalURL) : cityURL + "/channel-partners/" + verticalURL;

                    if (!aProductsCity.HRef.StartsWith("/"))
                        aProductsCity.HRef = "/" + aProductsCity.HRef;

                    Label lblProductsCity = new Label();
                    lblProductsCity.ID = "lblProductsCity_" + category.Id.ToString();
                    lblProductsCity.Text = category.Description + " in " + vSession.ElioCompanyDetailsView.City;

                    aProductsCity.Controls.Clear();
                    aProductsCity.Controls.Add(lblProductsCity);

                    div1.Controls.Clear();
                    div1.Controls.Add(div);
                    div1.Controls.Add(aProductsCity);

                    phProductsCityContentTrans.Controls.Add(div1);
                }
                else
                    divCityProductTrans.Visible = false;
            }
        }

        private void LoadCountryVerticalTrans(ElioSubIndustriesGroupItems category, bool existCountry, PlaceHolder phVerticalsCountryContentTrans)
        {
            bool hasCity = false;
            string urlLink = "";
            string cityURL = "";
            bool hasTrans = false;

            if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
            {
                hasCity = true;
            }

            if (hasCity)
            {
                if (existCountry)
                    urlLink = vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/channel-partners/";
                else
                    urlLink = "profile/channel-partners/";

                string verticalURL = category.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                divCountryVerticalsTrans.Visible = true;
                LblSimilarCountryCompanyTitleTrans.Text = "Categories in: " + vSession.ElioCompanyDetailsView.Country;

                if (vSession.ElioCompanyDetailsView.Country.ToLower() == "france" || vSession.ElioCompanyDetailsView.Country.ToLower() == "canada")
                {
                    hasTrans = true;
                    cityURL = "fr/";
                    LblSimilarCountryCompanyTitleTrans.Text = "Trouvez des entreprises similaires en " + vSession.ElioCompanyDetailsView.Country;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "austria")
                {
                    hasTrans = true;
                    cityURL = "at/";
                    LblSimilarCountryCompanyTitleTrans.Text = "Ähnliche Unternehmen in Österreich finden: ";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "germany")
                {
                    hasTrans = true;
                    cityURL = "de/";
                    LblSimilarCountryCompanyTitleTrans.Text = "Ähnliche Unternehmen in Deutschland finden: ";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "brazil" || vSession.ElioCompanyDetailsView.Country.ToLower() == "portugal")
                {
                    hasTrans = true;
                    cityURL = "pt/";
                    LblSimilarCountryCompanyTitleTrans.Text = "Encontre empresas semelhantes em " + vSession.ElioCompanyDetailsView.Country;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "italy")
                {
                    hasTrans = true;
                    cityURL = "it/";
                    LblSimilarCountryCompanyTitleTrans.Text = "Trova aziende simili in Italia: ";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "argentina" || vSession.ElioCompanyDetailsView.Country.ToLower() == "bolivia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "chile" || vSession.ElioCompanyDetailsView.Country.ToLower() == "colombia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "costa rica" ||
                    vSession.ElioCompanyDetailsView.Country.ToLower() == "dominican republic" || vSession.ElioCompanyDetailsView.Country.ToLower() == "ecuador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "el salvador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "guatemala" || vSession.ElioCompanyDetailsView.Country.ToLower() == "honduras" ||
                    vSession.ElioCompanyDetailsView.Country.ToLower() == "mexico" || vSession.ElioCompanyDetailsView.Country.ToLower() == "panama" || vSession.ElioCompanyDetailsView.Country.ToLower() == "paraguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "peru" || vSession.ElioCompanyDetailsView.Country.ToLower() == "puerto rico" ||
                     vSession.ElioCompanyDetailsView.Country.ToLower() == "uruguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "venezuela")
                {
                    hasTrans = true;
                    cityURL = "la/";
                    LblSimilarCountryCompanyTitleTrans.Text = "Buscar empresas similares en " + vSession.ElioCompanyDetailsView.Country;
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "spain")
                {
                    hasTrans = true;
                    cityURL = "es/";
                    LblSimilarCountryCompanyTitleTrans.Text = "Encuentra empresas similares en España: ";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "netherlands")
                {
                    hasTrans = true;
                    cityURL = "nl/";
                    LblSimilarCountryCompanyTitleTrans.Text = "Zoek vergelijkbare bedrijven in Nederland: ";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "poland")
                {
                    hasTrans = true;
                    cityURL = "pl/";
                    LblSimilarCountryCompanyTitleTrans.Text = "Znajdź podobne firmy w Polsce: ";
                }

                if (hasTrans)
                {
                    if (!cityURL.StartsWith("/"))
                        cityURL = "/" + cityURL;

                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.Attributes["class"] = "w-full flex gap-10px items-center";

                    HtmlGenericControl div = new HtmlGenericControl();
                    div.Attributes["class"] = "w-[15px] h-[15px] rounded-full bg-blue";

                    HtmlAnchor aVerticalsCountry = new HtmlAnchor();
                    aVerticalsCountry.ID = "aVerticalsCountry_" + category.Id.ToString();
                    aVerticalsCountry.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                    aVerticalsCountry.HRef = cityURL + urlLink + verticalURL;

                    Label lblVerticalsCountry = new Label();
                    lblVerticalsCountry.ID = "lblVerticalsCountry_" + category.Id.ToString();
                    lblVerticalsCountry.Text = category.Description + " in " + vSession.ElioCompanyDetailsView.Country;

                    aVerticalsCountry.Controls.Clear();
                    aVerticalsCountry.Controls.Add(lblVerticalsCountry);

                    div1.Controls.Clear();
                    div1.Controls.Add(div);
                    div1.Controls.Add(aVerticalsCountry);

                    phVerticalsCountryContentTrans.Controls.Add(div1);
                }
                else
                    divCountryVerticalsTrans.Visible = false;
            }
        }

        private void LoadCountryProductTrans(ElioRegistrationProducts category, bool existCountry, PlaceHolder phProductsCountryContentTrans)
        {
            bool hasCity = false;
            string urlLink = "";
            string cityURL = "";
            bool hasTrans = false;

            if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
            {
                hasCity = true;
            }

            if (hasCity)
            {
                if (existCountry)
                    urlLink = vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/channel-partners/";
                else
                    urlLink = "profile/channel-partners/";

                string verticalURL = category.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                divCountryProductTrans.Visible = true;
                LblCountryProductTransTitle.Text = "Products in country: " + vSession.ElioCompanyDetailsView.Country + " (Translated pages)";

                if (vSession.ElioCompanyDetailsView.Country.ToLower() == "france" || vSession.ElioCompanyDetailsView.Country.ToLower() == "canada")
                {
                    hasTrans = true;
                    cityURL = "fr/";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "austria")
                {
                    hasTrans = true;
                    cityURL = "at/";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "germany")
                {
                    hasTrans = true;
                    cityURL = "de/";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "brazil" || vSession.ElioCompanyDetailsView.Country.ToLower() == "portugal")
                {
                    hasTrans = true;
                    cityURL = "pt/";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "italy")
                {
                    hasTrans = true;
                    cityURL = "it/";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "argentina" || vSession.ElioCompanyDetailsView.Country.ToLower() == "bolivia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "chile" || vSession.ElioCompanyDetailsView.Country.ToLower() == "colombia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "costa rica" ||
                    vSession.ElioCompanyDetailsView.Country.ToLower() == "dominican republic" || vSession.ElioCompanyDetailsView.Country.ToLower() == "ecuador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "el salvador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "guatemala" || vSession.ElioCompanyDetailsView.Country.ToLower() == "honduras" ||
                    vSession.ElioCompanyDetailsView.Country.ToLower() == "mexico" || vSession.ElioCompanyDetailsView.Country.ToLower() == "panama" || vSession.ElioCompanyDetailsView.Country.ToLower() == "paraguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "peru" || vSession.ElioCompanyDetailsView.Country.ToLower() == "puerto rico" ||
                     vSession.ElioCompanyDetailsView.Country.ToLower() == "uruguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "venezuela")
                {
                    hasTrans = true;
                    cityURL = "la/";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "spain")
                {
                    hasTrans = true;
                    cityURL = "es/";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "netherlands")
                {
                    hasTrans = true;
                    cityURL = "nl/";
                }
                else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "poland")
                {
                    hasTrans = true;
                    cityURL = "pl/";
                }

                if (hasTrans)
                {
                    if (!cityURL.StartsWith("/"))
                        cityURL = "/" + cityURL;

                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.Attributes["class"] = "w-full flex gap-10px items-center";

                    HtmlGenericControl div = new HtmlGenericControl();
                    div.Attributes["class"] = "w-[15px] h-[15px] rounded-full bg-blue";

                    HtmlAnchor aProductsCountry = new HtmlAnchor();
                    aProductsCountry.ID = "aProductsCountry_" + category.Id.ToString();
                    aProductsCountry.Attributes["class"] = "text-base lg:text-lg hover:text-blue font-semibold";
                    aProductsCountry.HRef = cityURL + urlLink + verticalURL;

                    Label lblProductsCountry = new Label();
                    lblProductsCountry.ID = "lblProductsCountry_" + category.Id.ToString();
                    lblProductsCountry.Text = category.Description + " in " + vSession.ElioCompanyDetailsView.Country;

                    aProductsCountry.Controls.Clear();
                    aProductsCountry.Controls.Add(lblProductsCountry);

                    div1.Controls.Clear();
                    div1.Controls.Add(div);
                    div1.Controls.Add(aProductsCountry);

                    phProductsCountryContentTrans.Controls.Add(div1);
                }
                else
                    divCountryProductTrans.Visible = false;
            }
        }

        private void ResetLabels()
        {
            LblAddress.Text = string.Empty;
            LblPhone.Text = string.Empty;
            LblDescription.Text = string.Empty;
            LblOverview.Text = string.Empty;
            LblCompanyName.Text = string.Empty;
            //LblCompanyType.Text = string.Empty;
        }

        private void LoadCompanyDetailsViewData()
        {
            if (vSession.ElioCompanyDetailsView != null)
            {
                divFeaturedProfile.Visible = vSession.ElioCompanyDetailsView.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType);

                LblCompanyName.Text = vSession.ElioCompanyDetailsView.CompanyName;
                ImgCompanyLogo.Src = (vSession.ElioCompanyDetailsView.CompanyLogo != "") ? vSession.ElioCompanyDetailsView.CompanyLogo : "/Images/no_logo.jpg";
                ImgCompanyLogo.Alt = vSession.ElioCompanyDetailsView.CompanyName + " in Elioplus";
                LblCompanyCountryText.Text = vSession.ElioCompanyDetailsView.Country;

                if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
                {
                    divCity.Visible = false;
                }
                else
                {
                    LblCompanyCityText.Text = !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City) ? vSession.ElioCompanyDetailsView.City : "-";
                }

                if (string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Overview) || vSession.ElioCompanyDetailsView.Overview == "&nbsp;")
                {
                    string[] countries = new string[] { "Afghanistan", "Australia", "Bahrain", "Bangladesh", "Bahamas", "Barbados", "Cyprus", "India", "Ireland", "Jamaica", "New Zealand", "Saudi Arabia", "Singapore", "South Africa", "United Arab Emirates", "United Kingdom", "United States" };

                    if (countries.Contains(vSession.ElioCompanyDetailsView.Country))
                    {
                        LblOverview.Text = "View the solutions, services and product portfolio of " + vSession.ElioCompanyDetailsView.CompanyName;
                    }
                    else
                    {
                        LblOverview.Text = "Oups, we are sorry but there are no description data for this company.";
                    }
                }
                else
                {
                    LblOverview.Text = GlobalMethods.FixParagraphsView(vSession.ElioCompanyDetailsView.Overview);
                }

                if (string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Description) || vSession.ElioCompanyDetailsView.Description == "&nbsp;")
                {
                    string[] countries = new string[] { "Afghanistan", "Australia", "Bahrain", "Bangladesh", "Bahamas", "Barbados", "Cyprus", "India", "Ireland", "Jamaica", "New Zealand", "Saudi Arabia", "Singapore", "South Africa", "United Arab Emirates", "United Kingdom", "United States" };

                    if (countries.Contains(vSession.ElioCompanyDetailsView.Country))
                    {
                        LblDescription.Text = "View the solutions, services and product portfolio of " + vSession.ElioCompanyDetailsView.CompanyName;
                    }
                    else
                    {
                        LblDescription.Text = "Oups, we are sorry but there are no description data for this company.";
                    }
                }
                else
                {
                    LblDescription.Text = GlobalMethods.FixParagraphsView(vSession.ElioCompanyDetailsView.Description);
                }

                //if (string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Overview) && !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Description))
                //    LblOverview.Text = LblOverview.Text;

                //RttClaimProfileInfo.Text = "This listing was automatically created using public available data from third party sources. This listing is not currently maintained by, endorsed by or affiliated with {CompanyName}".Replace("{CompanyName}", vSession.ElioCompanyDetailsView.CompanyName);
            }
        }

        private void LoadDetails()
        {
            if (session.Connection.State == ConnectionState.Closed)
                session.OpenConnection();

            ResetLabels();

            LoadCompanyDetailsViewData();

            bool hasPhone = false;
            string alert = "";

            if (vSession.User == null)
                alert = "Login to your account for full access";
            else
                if (vSession.User.AccountStatus == (int)AccountStatus.NotCompleted)
                alert = "Complete your registration for full access";

            if (alert != "")
            {

            }

            if ((vSession.User == null) || (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)))
            {
                #region Logged In / Full Registered User

                //divDescriptionNotFull.Visible = false;

                //divAddressNotFull.Visible = false;
                //divPhoneNotFull.Visible = false;

                LblAddress.Text = vSession.ElioCompanyDetailsView.Address;
                //LblPhone.Text = (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Phone)) ? vSession.ElioCompanyDetailsView.Phone : "-";
                LblEmail.Text = vSession.ElioCompanyDetailsView.Email;
                aEmailTo.HRef = "mailto:" + vSession.ElioCompanyDetailsView.Email;

                LblPhone.Text = GlobalDBMethods.ShowUserPhone(vSession.ElioCompanyDetailsView, session, out hasPhone);

                #endregion
            }
            else
            {
                #region Not Logged In User / Not Full Registered User

                //divDescriptionNotFull.Visible = true;

                LblAddress.Visible = false;
                LblPhone.Visible = false;

                //divAddressNotFull.Visible = true;
                //divPhoneNotFull.Visible = true;

                //LblAddressNotFull.Text = alert;
                //LblPhoneNotFull.Text = alert;

                #endregion
            }

            LoadIndustries();

            LoadMarkets();

            LoadUserVerticalsInPlaceHolder();

            if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
            {
                LoadProgramsForVendors();
                LoadVendorIntegrations();

                //divPdfNotFull.Visible = false;

                if ((vSession.User == null) || (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)))
                {
                    #region Logged In / Full Registered User

                    ElioUsersFiles userPdf = Sql.GetUserPdfFile(vSession.ElioCompanyDetailsView.Id, session);

                    if (userPdf != null)
                    {
                        //    try
                        //    {
                        //        if (!userPdf.FilePath.Contains("??????"))
                        //        {
                        //            bool fileExists = File.Exists(Server.MapPath(userPdf.FilePath));
                        //            if (fileExists)
                        //            {
                        //                aPdf.Visible = true;
                        //                iPdf.Visible = true;
                        //                string[] pdfArray = userPdf.FilePath.Split('/');
                        //                string pdfName = pdfArray[4];
                        //                LblPdfValue.Text = "Partner Program brochure ";
                        //                aPdf.HRef = userPdf.FilePath;
                        //                aPdf.Target = "_blank";
                        //            }
                        //            else
                        //            {
                        //                aPdf.Visible = false;
                        //                iPdf.Visible = false;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            aPdf.Visible = false;
                        //            iPdf.Visible = false;
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        aPdf.Visible = false;
                        //        iPdf.Visible = false;
                        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        //    }
                    }
                    else
                    {
                        //    aPdf.Visible = false;
                        //    iPdf.Visible = false;
                    }

                    #endregion
                }
                else
                {
                    #region Not Logged In User / Not Full Registered User

                    //iPdf.Visible = true;
                    //aPdf.Visible = false;
                    //divPdfNotFull.Visible = true;

                    //string msgAlert = (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? "Complete your registration to view partner program brochure" : "Login to your account to view partner program brochure";

                    //LblPdfNotFull.Text = msgAlert;

                    #endregion
                }

            }
            else if (vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                LoadProgramsForResellers();
                LoadChannelPartnerProductsInPlaceHolder();

                //aPdf.Visible = false;
                //iPdf.Visible = false;

                if (vSession.ElioCompanyDetailsView != null && vSession.ElioCompanyDetailsView.BillingType != (int)BillingTypePacket.FreemiumPacketType)
                {
                    aWebsite.Visible = true;
                    aWebsite.HRef = vSession.ElioCompanyDetailsView.WebSite;
                    aWebsite.Target = "_blank";

                    aContact.Visible = true;
                    aContact.HRef = "/" + vSession.ElioCompanyDetailsView.Id + ControlLoader.MessageQuote;
                    aContact.Target = "_blank";
                }
            }
            else
            {
                //aPdf.Visible = false;
                //iPdf.Visible = false;
            }
        }

        private void ClearMessageData(bool isClaimMode)
        {
            if (isClaimMode)
            {
                TbxClaimMessageEmail.Text = string.Empty;

                LblClaimSuccessMsg.Text = string.Empty;
                LblClaimWarningMsg.Text = string.Empty;
                divClaimWarningMsg.Visible = false;
                divClaimSuccessMsg.Visible = false;
            }
        }

        private void Rate(int r)
        {
            if (vSession.User != null && vSession.ElioCompanyDetailsView.Id != 0)
            {
                ElioUserPartnerProgramRating rating = new ElioUserPartnerProgramRating();

                rating.Rate = r;
                rating.CompanyId = vSession.ElioCompanyDetailsView.Id;
                rating.VisitorId = vSession.User.Id;
                rating.Sysdate = DateTime.Now;

                DataLoader<ElioUserPartnerProgramRating> loader = new DataLoader<ElioUserPartnerProgramRating>(session);
                loader.Insert(rating);

                //BindRatingControl();
            }
            else
            {
                Response.Redirect(ControlLoader.Search, false);
            }
        }

        private bool CheckData(bool isClaimMode)
        {
            bool isError = false;

            divClaimWarningMsg.Visible = false;
            divClaimSuccessMsg.Visible = false;

            if (isClaimMode)
            {
                if (TbxClaimMessageEmail.Visible)
                {
                    if (string.IsNullOrEmpty(TbxClaimMessageEmail.Text))
                    {
                        LblClaimWarningMsgContent.Text = "Please enter an email address!";
                        divClaimWarningMsg.Visible = true;
                        return isError = true;
                    }
                    else
                    {
                        if (!Validations.IsEmail(TbxClaimMessageEmail.Text))
                        {
                            LblClaimWarningMsgContent.Text = "Please enter a valid email address!";
                            divClaimWarningMsg.Visible = true;
                            return isError = true;
                        }
                    }
                }
            }

            return isError;
        }

        private void ResetRFPsFields()
        {
            divDemoWarningMsg.Visible = false;
            divDemoSuccessMsg.Visible = false;

            divStepOne.Visible = true;
            divStepTwo.Visible = false;
            BtnBack.Visible = false;
            BtnProceed.Text = "Next";

            TbxFirstName.Text = "";
            TbxCompanyEmail.Text = "";
            TbxLastName.Text = "";
            TbxBusinessName.Text = "";
            TbxCity.Text = "";
            DdlCountries.SelectedIndex = -1;
            TbxProduct.Text = "";
            TbxNumberUnits.Text = "";
            TbxMessage.Text = "";

            HdnLeadId.Value = "0";
        }

        private void LoadCountries()
        {
            DdlCountries.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Select Country";

            DdlCountries.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new ListItem();
                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                DdlCountries.Items.Add(item);
            }
        }

        # endregion

        # region Buttons

        protected void aRFPsForm_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ResetRFPsFields();
                LoadCountries();

                if (vSession.ElioCompanyDetailsView.Id > 0)
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenRFPsPopUp();", true);
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

        protected void BtnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divDemoSuccessMsg.Visible = divDemoWarningMsg.Visible = false;

                if (vSession.ElioCompanyDetailsView.Id > 0)
                {
                    if (divStepOne.Visible)
                    {
                        if (TbxProduct.Text == "")
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please fill your product/technology";
                            return;
                        }

                        if (TbxNumberUnits.Text == "")
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please fill number of users/units";
                            return;
                        }
                        else
                        {
                            if (!Validations.IsNumber(TbxNumberUnits.Text))
                            {
                                divDemoWarningMsg.Visible = true;
                                LblDemoWarningMsg.Text = "Error! ";
                                LblDemoWarningMsgContent.Text = "Please fill only numbers for users/units";
                                return;
                            }
                        }

                        if (TbxMessage.Text == "")
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please fill a message";
                            return;
                        }

                        divStepOne.Visible = false;
                        divStepTwo.Visible = true;

                        BtnProceed.Text = "Save";
                        BtnBack.Visible = true;
                    }
                    else
                    {
                        if (TbxFirstName.Text == "")
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please fill your first name";
                            return;
                        }

                        if (TbxLastName.Text == "")
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please fill your last name";
                            return;
                        }

                        if (TbxCompanyEmail.Text == "")
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please fill your company email";
                            return;
                        }
                        else
                        {
                            if (!Validations.IsEmail(TbxCompanyEmail.Text))
                            {
                                divDemoWarningMsg.Visible = true;
                                LblDemoWarningMsg.Text = "Error! ";
                                LblDemoWarningMsgContent.Text = "Please fill a valid company email";
                                return;
                            }
                        }

                        if (TbxBusinessName.Text == "")
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please fill your company name";
                            return;
                        }

                        if (DdlCountries.SelectedValue == "0" || DdlCountries.SelectedValue == "")
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please select your country";
                            return;
                        }

                        if (TbxPhoneNumber.Text == "")
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please fill your phone number";
                            return;
                        }

                        if (TbxCity.Text == "")
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please fill your city";
                            return;
                        }

                        ElioSnitcherWebsiteLeads lead = null;

                        DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);

                        if (HdnLeadId.Value == "0")
                        {
                            lead = new ElioSnitcherWebsiteLeads();

                            lead.WebsiteId = "19976";
                            lead.ElioSnitcherWebsiteId = 2;
                            lead.SessionReferrer = "https://www.elioplus.com";
                            lead.SessionOperatingSystem = "";
                            lead.SessionBrowser = "";
                            lead.SessionDeviceType = "";
                            lead.SessionCampaign = "";
                            lead.SessionStart = DateTime.Now;
                            lead.SessionDuration = 0;
                            lead.SessionTotalPageviews = 1;

                            string number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8);
                            int count = 1;
                            while (Sql.GetSnitcherLeadIDByLeadId(number, session) != "" && count < 10)
                            {
                                number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8);
                                count++;
                            }

                            if (count >= 10)
                            {
                                throw new Exception("ERROR -> string number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8) could not created");
                            }

                            lead.LeadId = number;
                            lead.LeadLastSeen = DateTime.Now;
                            lead.LeadFirstName = TbxFirstName.Text;
                            lead.LeadLastName = TbxLastName.Text;
                            lead.LeadCompanyName = TbxBusinessName.Text;
                            lead.LeadCompanyLogo = "";
                            lead.LeadCompanyWebsite = "";
                            lead.LeadCountry = DdlCountries.SelectedItem.Text;
                            lead.LeadCity = TbxCity.Text;
                            lead.LeadCompanyAddress = "";
                            lead.LeadCompanyFounded = "0";
                            lead.LeadCompanySize = TbxNumberUnits.Text;
                            lead.LeadCompanyIndustry = "";
                            lead.LeadCompanyPhone = "";
                            lead.LeadCompanyEmail = TbxCompanyEmail.Text;
                            lead.LeadCompanyContacts = "";
                            lead.LeadLinkedinHandle = "";
                            lead.LeadFacebookHandle = "";
                            lead.LeadYoutubeHandle = "";
                            lead.LeadInstagramHandle = "";
                            lead.LeadTwitterHandle = "";
                            lead.LeadPinterestHandle = "";
                            lead.LeadAngellistHandle = "";
                            lead.Message = TbxMessage.Text;
                            lead.IsApiLead = (int)ApiLeadCategory.isRFQMessage;
                            lead.IsApproved = 0;
                            lead.IsPublic = 1;
                            lead.IsConfirmed = 0;
                            lead.Sysdate = DateTime.Now;
                            lead.LastUpdate = DateTime.Now;
                            lead.RfpMessageCompanyIdIsFor = vSession.ElioCompanyDetailsView.Id.ToString();

                            loader.Insert(lead);

                            HdnLeadId.Value = lead.Id.ToString();

                            List<string> products = TbxProduct.Text.Trim().Split(',').ToList();
                            if (products.Count > 0)
                            {
                                foreach (string product in products)
                                {
                                    if (product != "")
                                    {
                                        ElioSnitcherLeadsPageviews pageView = new ElioSnitcherLeadsPageviews();

                                        pageView.LeadId = lead.LeadId;
                                        pageView.ElioWebsiteLeadsId = lead.Id;
                                        pageView.Url = product.Trim();
                                        pageView.Product = product.Trim();
                                        pageView.TimeSpent = 1;
                                        pageView.ActionTime = DateTime.Now;
                                        pageView.Sysdate = DateTime.Now;
                                        pageView.LastUpdate = DateTime.Now;

                                        DataLoader<ElioSnitcherLeadsPageviews> loaderView = new DataLoader<ElioSnitcherLeadsPageviews>(session);
                                        loaderView.Insert(pageView);
                                    }
                                }
                            }
                        }
                        else
                        {
                            lead = Sql.GetSnitcherWebsiteLeadById(Convert.ToInt32(HdnLeadId.Value), session);
                            if (lead != null)
                            {
                                lead.LeadFirstName = TbxFirstName.Text;
                                lead.LeadLastName = TbxLastName.Text;
                                lead.LeadCompanyName = TbxBusinessName.Text;
                                lead.LeadCountry = DdlCountries.SelectedItem.Text;
                                lead.LeadCity = TbxCity.Text;
                                lead.LeadCompanySize = TbxNumberUnits.Text;
                                lead.LeadCompanyEmail = TbxCompanyEmail.Text;
                                lead.Message = TbxMessage.Text;
                                lead.LastUpdate = DateTime.Now;
                                lead.IsConfirmed = 0;
                                lead.RfpMessageCompanyIdIsFor = vSession.ElioCompanyDetailsView.Id.ToString();

                                loader.Update(lead);

                                List<string> products = TbxProduct.Text.Trim().Split(',').ToList();
                                if (products.Count > 0)
                                {
                                    foreach (string product in products)
                                    {
                                        if (product != "")
                                        {
                                            DataLoader<ElioSnitcherLeadsPageviews> loaderView = new DataLoader<ElioSnitcherLeadsPageviews>(session);

                                            ElioSnitcherLeadsPageviews pageView = Sql.GetSnitcherLeadPageViewByLeadIdAndUrlOrProduct(lead.LeadId, product, product, session);
                                            if (pageView == null)
                                            {
                                                pageView = new ElioSnitcherLeadsPageviews();

                                                pageView.LeadId = lead.LeadId;
                                                pageView.ElioWebsiteLeadsId = lead.Id;
                                                pageView.Url = product.Trim();
                                                pageView.Product = product.Trim();
                                                pageView.TimeSpent = 1;
                                                pageView.ActionTime = DateTime.Now;
                                                pageView.Sysdate = DateTime.Now;
                                                pageView.LastUpdate = DateTime.Now;

                                                loaderView.Insert(pageView);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        divDemoSuccessMsg.Visible = true;
                        LblDemoSuccessMsg.Text = "Done! ";
                        LblDemoSuccessMsgContent.Text = "Your request for a quote saved successfully.";
                    }
                }
                else
                {
                    throw new Exception("RFPCompanyID is 0!");
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                divDemoWarningMsg.Visible = true;
                LblDemoWarningMsg.Text = "Error! ";
                LblDemoWarningMsgContent.Text = "Something went wrong! Please try again later.";
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                divStepOne.Visible = true;
                divStepTwo.Visible = false;

                divDemoSuccessMsg.Visible = divDemoWarningMsg.Visible = false;

                BtnBack.Visible = false;
                BtnProceed.Text = "Next";
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

        protected void BtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    //if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                    //{
                    //    session.BeginTransaction();

                    //    ElioUsersFavorites favorite = new ElioUsersFavorites();
                    //    DataLoader<ElioUsersFavorites> loader = new DataLoader<ElioUsersFavorites>(session);

                    //    favorite.UserId = vSession.User.Id;
                    //    favorite.CompanyId = vSession.ElioCompanyDetailsView.Id;
                    //    favorite.SysDate = DateTime.Now;
                    //    favorite.IsDeleted = 0;
                    //    favorite.LastUpDated = DateTime.Now;

                    //    loader.Insert(favorite);

                    //    aSaveProfile.Visible = false;

                    //    session.CommitTransaction();

                    //    rowMsgSaveProfile.Visible = true;
                    //    divErrorSaveProfile.Visible = false;
                    //    divScsSaveProfile.Visible = true;
                    //    LblScsSaveProfile.Text = "Done! ";
                    //    LblSuccessSaveProfileContent.Text = "You have successfully saved this company's profile";
                    //}
                    //else
                    //{
                    //    rowMsgSaveProfile.Visible = true;
                    //    divErrorSaveProfile.Visible = true;
                    //    divScsSaveProfile.Visible = false;
                    //    LblErrorSaveProfile.Text = "Error! ";
                    //    LblErrorSaveProfileContent.Text = "Something wrong happened, try again or contact us.";
                    //}
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
                }
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

        protected void ImgBtnCompanyLogo_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                vSession.ElioCompanyDetailsView = null;

                if (vSession.User != null)
                {
                    ImageButton imgBtnCompanyLogo = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtnCompanyLogo.NamingContainer;

                    if (!Sql.IsUserAdministrator(vSession.User.Id, session))
                    {
                        ElioUsers visitorCompany = Sql.GetUserById(Convert.ToInt32(item["visitor_id"].Text), session);
                        if (visitorCompany != null)
                        {
                            GlobalDBMethods.AddCompanyViews(vSession.User, visitorCompany, vSession.Lang, session);
                        }
                    }

                    vSession.ElioCompanyDetailsView = Sql.GetUserById(Convert.ToInt32(item["visitor_id"].Text), session);
                    if (vSession.ElioCompanyDetailsView != null)
                    {
                        if (vSession.User.Id != Convert.ToInt32(item["visitor_id"].Text) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                        {
                            if (!Sql.IsUserAdministrator(vSession.User.Id, session))
                            {
                                #region Send Lead email info

                                //EmailNotificationsLib.SendNotificationEmailToCompanyForResentLeads(Convert.ToInt32(item["visitor_id"].Text), session);
                                EmailSenderLib.SendNotificationEmailToCompanyForResentLeads(Convert.ToInt32(item["visitor_id"].Text), false, vSession.Lang, session);

                                #endregion
                            }
                        }

                        Response.Redirect(ControlLoader.Profile(vSession.ElioCompanyDetailsView), false);
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Login, false);
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

        protected void ImgBtnSetNotPublic_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ImageButton imgBtnSetNotPublic = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtnSetNotPublic.NamingContainer;

                    ElioUserProgramReview review = Sql.GetProgramsReviewById(Convert.ToInt32(item["id"].Text), session);
                    if (review != null)
                    {
                        review.IsPublic = 0;
                        review.LastUpdate = DateTime.Now;
                        review.UpdateByUserId = vSession.User.Id;

                        DataLoader<ElioUserProgramReview> loader = new DataLoader<ElioUserProgramReview>(session);
                        loader.Update(review);

                        //LblTotalReviews.Text = Sql.GetCompanyTotalReviews(review.CompanyId, session).ToString();

                        //RdgReviews.Rebind();
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

        protected void R1_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                Rate(1);
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

        protected void R2_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                Rate(2);
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

        protected void R3_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                Rate(3);
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

        protected void R4_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                Rate(4);
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

        protected void R5_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                Rate(5);
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

        protected void BtnSendClaim_OnClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divClaimSuccessMsg.Visible = false;
                divClaimWarningMsg.Visible = false;

                if (vSession.ElioCompanyDetailsView != null)
                {
                    bool isError = CheckData(true);

                    if (isError) return;

                    MailAddress companyMail = new MailAddress(vSession.ElioCompanyDetailsView.Email.Trim().ToLower());
                    MailAddress inputMail = new MailAddress(TbxClaimMessageEmail.Text.Trim().ToLower());
                    string companyHost = companyMail.Host;
                    string inputHost = inputMail.Host;

                    if (companyHost == inputHost)
                    {
                        try
                        {
                            EmailSenderLib.ClaimProfileResetPasswordEmail(vSession.ElioCompanyDetailsView.Password, vSession.ElioCompanyDetailsView.Email, vSession.Lang, session);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        isError = true;
                        divClaimSuccessMsg.Visible = false;
                        divClaimWarningMsg.Visible = true;
                        LblClaimWarningMsg.Text = "Warning! ";
                        LblClaimWarningMsgContent.Text = "We 're sorry but this email does not match to this profile's email. Please contact us.";

                        return;
                    }

                    if (!isError)
                    {
                        ClearMessageData(true);

                        divClaimSuccessMsg.Visible = true;
                        LblClaimSuccessMsgContent.Text = "Thank you! You have received an email with your password and account instructions. You can log in to it.";
                    }
                    else
                    {
                        ClearMessageData(true);

                        //divWarningMsg.Visible = true;
                        //LblWarningMsgContent.Text = "We are sorry but your message could not be sent because an unkown error occured. Please try again later to contact us!";
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Search, false);
                }
            }
            catch (Exception ex)
            {
                ClearMessageData(true);

                //divWarningMsg.Visible = true;
                //LblWarningMsgContent.Text = "We are sorry but your message could not be sent because an unkown error occured. Please try again later to contact us!";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnCancelClaimMsg_OnClick(object sender, EventArgs args)
        {
            try
            {
                ClearMessageData(true);

                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseClaimProfilePopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnCancelRvw_OnClick(object sender, EventArgs args)
        {
            try
            {
                ClearMessageData(false);

                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseReviewPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aSendMessage_ServerClick(object sender, EventArgs e)
        {
            try
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenMessagePopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aAddReview_ServerClick(object sender, EventArgs e)
        {
            try
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenReviewPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aPartnerPortalLogin_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string url = "/" + Regex.Replace(vSession.ElioCompanyDetailsView.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-free-sign-up";
                Response.Redirect(url, false);

                vSession.User = null;
                Session.Clear();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aClaimProfile_ServerClick(object sender, EventArgs e)
        {
            try
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenClaimProfilePopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnCloseAlertPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseAlertPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion

        #region Grids

        protected void RdgResults_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = e.Item;
                    if (item != null)
                    {
                        ElioUsersSearchInfo row = (ElioUsersSearchInfo)e.Item.DataItem;
                        if (row != null)
                        {
                            //string companyId = (item.FindControl("HdnCompanyID") as HiddenField).Value;
                            //int UserID = Convert.ToInt32(companyId);
                            string Translation = "";

                            if (row.BillingType > 1)
                            {
                                HtmlGenericControl featured = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divFeatured");
                                featured.Visible = true;

                                HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                                HtmlAnchor aContact = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aContact");

                                if (!string.IsNullOrEmpty(row.WebSite))
                                {
                                    aWebsite.Visible = true;
                                }

                                aContact.Visible = true;
                                aContact.HRef = "/" + row.Id + ControlLoader.MessageQuote;
                                aContact.Target = "_blank";
                            }
                            else
                            {
                                HtmlAnchor aRFPsForm = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aRFPsForm");
                                Label lblRFPsForm = (Label)ControlFinder.FindControlRecursive(item, "LblRFPsForm");

                                aRFPsForm.Visible = true;
                                aRFPsForm.HRef = (Translation == "") ? "/" + row.Id + ControlLoader.RequestQuote : "/" + Translation + "/" + row.Id + ControlLoader.RequestQuote;

                                if (Translation != "")
                                {
                                    switch (Translation)
                                    {
                                        case "es":
                                        case "la":

                                            lblRFPsForm.Text = "Solicitud de Cotización";

                                            break;

                                        case "pt":

                                            lblRFPsForm.Text = "Solicitação de Cotação";

                                            break;

                                        case "de":
                                        case "at":

                                            lblRFPsForm.Text = "Angebot anfordern";

                                            break;

                                        case "pl":

                                            lblRFPsForm.Text = "Poproś o wycenę";

                                            break;

                                        case "it":

                                            lblRFPsForm.Text = "Richiedi Preventivo";

                                            break;

                                        case "nl":

                                            lblRFPsForm.Text = "Uw offerte aanvragen";

                                            break;

                                        case "fr":

                                            lblRFPsForm.Text = "Obtenir un devis";

                                            break;
                                    }
                                }
                                else
                                    lblRFPsForm.Text = "Get a Quote";
                            }

                            bool hasCompanyData = false;
                            ElioUsersPersonCompanies company = null;

                            string urlLink = "";
                            bool existCountry = true;

                            if (row.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                            {
                                if (row.UserApplicationType == (int)UserApplicationType.ThirdParty)        // && userIndustries.Count == 0)
                                {
                                    hasCompanyData = ClearbitSql.ExistsClearbitCompany(row.Id, session);
                                    if (hasCompanyData)
                                        company = ClearbitSql.GetPersonCompanyByUserId(row.Id, session);

                                    if (hasCompanyData && company != null)
                                    {
                                        if (string.IsNullOrEmpty(row.CompanyLogo))
                                            if (!string.IsNullOrEmpty(company.Logo))
                                            {
                                                HtmlImage imgCompanyLogo = (HtmlImage)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                                                imgCompanyLogo.Src = company.Logo;
                                            }
                                    }
                                }

                                urlLink = (existCountry) ? row.CompanyRegion.Replace(" ", "-").ToLower() + "/" + row.Country.Replace(" ", "-").ToLower() + "/channel-partners/" : "profile/channel-partners/";

                                urlLink = urlLink.StartsWith("/") ? urlLink : "/" + urlLink;

                                bool hasMSPs = Sql.HasUserMSPsProgram(row.Id, 7, session);

                                if (hasMSPs)
                                {
                                    HtmlGenericControl divMSPs = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divMSPs");
                                    HtmlAnchor aMSPs = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMSPs");

                                    divMSPs.Visible = true;

                                    string lnkMSP = "/" + row.CompanyRegion.Replace(" ", "-").ToLower() + "/" + row.Country.Replace(" ", "-").ToLower() + "/";

                                    if (!string.IsNullOrEmpty(row.State))
                                        lnkMSP += row.State.Replace(" ", "-").ToLower() + "/";

                                    if (!string.IsNullOrEmpty(row.City))
                                        lnkMSP += row.City.Replace(" ", "-").ToLower() + "/";

                                    aMSPs.HRef = lnkMSP + "channel-partners/managed-service-providers";
                                }
                            }
                            else
                            {
                                urlLink = "/partner-programs/vendors/";
                            }

                            if (!urlLink.StartsWith("/"))
                                urlLink = "/" + urlLink;

                            List<ElioSubIndustriesGroupItems> userVarticals = Sql.GetUserSubIndustriesGroupItems(row.Id, session);

                            if (userVarticals.Count > 0)
                            {
                                int count = 0;
                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert1");
                                    Label LblVert1 = (Label)ControlFinder.FindControlRecursive(item, "LblVert1");
                                    aVert1.Visible = true;
                                    aVert1.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert1.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert2");
                                    Label LblVert2 = (Label)ControlFinder.FindControlRecursive(item, "LblVert2");
                                    aVert2.Visible = true;
                                    aVert2.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert2.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert3");
                                    Label LblVert3 = (Label)ControlFinder.FindControlRecursive(item, "LblVert3");
                                    aVert3.Visible = true;
                                    aVert3.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert3.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert4");
                                    Label LblVert4 = (Label)ControlFinder.FindControlRecursive(item, "LblVert4");
                                    aVert4.Visible = true;
                                    aVert4.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert4.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert5 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert5");
                                    Label LblVert5 = (Label)ControlFinder.FindControlRecursive(item, "LblVert5");
                                    aVert5.Visible = true;
                                    aVert5.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert5.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert6 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert6");
                                    Label LblVert6 = (Label)ControlFinder.FindControlRecursive(item, "LblVert6");
                                    aVert6.Visible = true;
                                    aVert6.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert6.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert7 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert7");
                                    Label LblVert7 = (Label)ControlFinder.FindControlRecursive(item, "LblVert7");
                                    aVert7.Visible = true;
                                    aVert7.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert7.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert8 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert8");
                                    Label LblVert8 = (Label)ControlFinder.FindControlRecursive(item, "LblVert8");
                                    aVert8.Visible = true;
                                    aVert8.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert8.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert9 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert9");
                                    Label LblVert9 = (Label)ControlFinder.FindControlRecursive(item, "LblVert9");
                                    aVert9.Visible = true;
                                    aVert9.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert9.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert10 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert10");
                                    Label LblVert10 = (Label)ControlFinder.FindControlRecursive(item, "LblVert10");
                                    aVert10.Visible = true;
                                    aVert10.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert10.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert11 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert11");
                                    Label LblVert11 = (Label)ControlFinder.FindControlRecursive(item, "LblVert11");
                                    aVert11.Visible = true;
                                    aVert11.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert11.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert12 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert12");
                                    Label LblVert12 = (Label)ControlFinder.FindControlRecursive(item, "LblVert12");
                                    aVert12.Visible = true;
                                    aVert12.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert12.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert13 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert13");
                                    Label LblVert13 = (Label)ControlFinder.FindControlRecursive(item, "LblVert13");
                                    aVert13.Visible = true;
                                    aVert13.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert13.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert14 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert14");
                                    Label LblVert14 = (Label)ControlFinder.FindControlRecursive(item, "LblVert14");
                                    aVert14.Visible = true;
                                    aVert14.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert14.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert15 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert15");
                                    Label LblVert15 = (Label)ControlFinder.FindControlRecursive(item, "LblVert15");
                                    aVert15.Visible = true;
                                    aVert15.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert15.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (userVarticals.Count > 14)
                                {
                                    HtmlAnchor aVertMore = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVertMore");
                                    Label lblVertMore = (Label)ControlFinder.FindControlRecursive(item, "LblVertMore");
                                    Label LblVertMoreNum = (Label)ControlFinder.FindControlRecursive(item, "LblVertMoreNum");
                                    aVertMore.Visible = userVarticals.Count > 15;
                                    aVertMore.HRef = "";

                                    if (aVertMore.Visible)
                                    {
                                        lblVertMore.Text = "more";
                                        LblVertMoreNum.Text = "+" + (userVarticals.Count - 15).ToString() + " ";

                                        aVertMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithVerticalsRestDescriptions(userVarticals);
                                    }
                                }
                            }
                            else
                            {
                                HtmlGenericControl divCategoriesArea = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divCategoriesArea");
                                divCategoriesArea.Visible = false;
                            }

                            if (row.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                            {
                                if (!string.IsNullOrEmpty(row.City))
                                {
                                    HtmlAnchor aAllCityChannelPartners = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aAllCityChannelPartners");
                                    aAllCityChannelPartners.Attributes["class"] = "text-xs lg:text-xs py-5px px-10px bg-gray rounded-20px text-blue";

                                    aAllCityChannelPartners.HRef = "/" + row.CompanyRegion.Replace(" ", "-").ToLower() + "/" + row.Country.Replace(" ", "-").ToLower() + "/" + row.City.Replace(" ", "-").ToLower() + "/all-channel-partners/";

                                    if (row.Country != "United States" && row.Country != "United Kingdom" && row.Country != "Australia" && row.Country != "India" && row.City != "")
                                    {
                                        string link = "/" + row.CompanyRegion.ToLower().Replace(" ", "-").Replace("&", "and") + "/";

                                        if (row.Country == "France")
                                            link += "fr/";
                                        else if (row.Country == "Canada")
                                            link += "ca/";
                                        else if (row.Country == "Spain")
                                            link += "es/";
                                        else if (row.Country == "Germany")
                                            link += "de/";
                                        else if (row.Country == "Portugal" || row.Country == "Brazil")
                                            link += "pt/";
                                        else if (row.Country == "Austria")
                                            link += "at/";
                                        else if (row.Country == "Italy")
                                            link += "it/";
                                        else if (row.Country == "Argentina" || row.Country == "Bolivia" || row.Country == "Chile"
                                            || row.Country == "Colombia" || row.Country == "Costa Rica" || row.Country == "Dominican Republic"
                                            || row.Country == "Ecuador" || row.Country == "El Salvador" || row.Country == "Guatemala"
                                            || row.Country == "Honduras" || row.Country == "Mexico" || row.Country == "Panama"
                                            || row.Country == "Paraguay" || row.Country == "Peru" || row.Country == "Puerto Rico"
                                            || row.Country == "Uruguay" || row.Country == "Venezuela")
                                            link += "la/";
                                        else if (row.Country == "Netherlands")
                                            link += "nl/";
                                        else if (row.Country == "Poland")
                                            link += "pl/";

                                        link += row.Country.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + row.City.ToLower().Replace(" ", "-").Replace("&", "and") + "/all-channel-partners";
                                        aAllCityChannelPartners.HRef = link;
                                    }
                                }
                                else
                                {
                                    HtmlGenericControl divAllCity = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divAllCity");
                                    divAllCity.Visible = false;
                                }
                            }
                            else
                            {
                                HtmlGenericControl divAllCity = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divAllCity");
                                divAllCity.Visible = false;
                            }

                            string productUrl = "";

                            if (row.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                productUrl = (existCountry) ? row.CompanyRegion.Replace(" ", "-").ToLower() + "/" + row.Country.Replace(" ", "-").ToLower() + "/channel-partners/" : "profile/channel-partners/";
                            else
                                productUrl = "/partner-programs/vendors/";

                            if (!productUrl.StartsWith("/"))
                                productUrl = "/" + productUrl;

                            productUrl = productUrl.StartsWith("/") ? productUrl : "/" + productUrl;

                            List<ElioRegistrationProducts> userProducts = Sql.GetRegistrationProductsDescriptionByUserId(row.Id, session);

                            if (userProducts.Count > 0)
                            {
                                int count = 0;

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr1");
                                    Label LblPartPr1 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr1");
                                    aPartPr1.Visible = true;
                                    aPartPr1.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr1.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr2");
                                    Label LblPartPr2 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr2");
                                    aPartPr2.Visible = true;
                                    aPartPr2.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr2.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr3");
                                    Label LblPartPr3 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr3");
                                    aPartPr3.Visible = true;
                                    aPartPr3.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr3.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr4");
                                    Label LblPartPr4 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr4");
                                    aPartPr4.Visible = true;
                                    aPartPr4.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr4.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr5 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr5");
                                    Label LblPartPr5 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr5");
                                    aPartPr5.Visible = true;
                                    aPartPr5.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr5.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr6 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr6");
                                    Label LblPartPr6 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr6");
                                    aPartPr6.Visible = true;
                                    aPartPr6.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr6.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr7 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr7");
                                    Label LblPartPr7 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr7");
                                    aPartPr7.Visible = true;
                                    aPartPr7.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr7.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr8 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr8");
                                    Label LblPartPr8 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr8");
                                    aPartPr8.Visible = true;
                                    aPartPr8.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr8.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr9 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr9");
                                    Label LblPartPr9 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr9");
                                    aPartPr9.Visible = true;
                                    aPartPr9.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr9.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr10 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr10");
                                    Label LblPartPr10 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr10");
                                    aPartPr10.Visible = true;
                                    aPartPr10.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr10.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr11 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr11");
                                    Label LblPartPr11 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr11");
                                    aPartPr11.Visible = true;
                                    aPartPr11.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr11.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr12 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr12");
                                    Label LblPartPr12 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr12");
                                    aPartPr12.Visible = true;
                                    aPartPr12.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr12.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr13 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr13");
                                    Label LblPartPr13 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr13");
                                    aPartPr13.Visible = true;
                                    aPartPr13.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr13.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr14 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr14");
                                    Label LblPartPr14 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr14");
                                    aPartPr14.Visible = true;
                                    aPartPr14.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr14.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr15 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr15");
                                    Label LblPartPr15 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr15");
                                    aPartPr15.Visible = true;
                                    aPartPr15.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr15.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (userProducts.Count > 14)
                                {
                                    HtmlAnchor aPartPrMore = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPrMore");
                                    Label lblPartPrMore = (Label)ControlFinder.FindControlRecursive(item, "LblPartPrMore");
                                    Label lblPartPrMoreNum = (Label)ControlFinder.FindControlRecursive(item, "LblPartPrMoreNum");
                                    aPartPrMore.Visible = userProducts.Count > 15;
                                    aPartPrMore.HRef = "";

                                    if (aPartPrMore.Visible)
                                    {
                                        lblPartPrMore.Text = "more";
                                        lblPartPrMoreNum.Text = "+" + (userProducts.Count - 15).ToString() + " ";

                                        HtmlImage iMoreProducts = (HtmlImage)ControlFinder.FindControlRecursive(item, "iMoreProducts");
                                        aPartPrMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithRegistrationProductsRestDescriptions(userProducts);
                                    }
                                }
                            }
                            else
                            {
                                HtmlGenericControl divProductsArea = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divProductsArea");
                                divProductsArea.Visible = false;
                            }

                            if (string.IsNullOrEmpty(row.Overview) || row.Overview == "&nbsp;")
                            {
                                if (company != null)
                                {
                                    if (!string.IsNullOrEmpty(company.Description))
                                    {
                                        if (company.Description.Length > 1000)
                                        {
                                            row.Overview = GlobalMethods.FixParagraphsView(company.Description.Substring(0, 1000) + " ...");
                                        }
                                        else
                                        {
                                            row.Overview = GlobalMethods.FixParagraphsView(company.Description);
                                        }
                                    }
                                    else
                                    {
                                        if (row.Country != "")
                                        {
                                            row.Overview = GlobalDBMethods.GetOverviewForCompanyByCountry(row.Country, row.CompanyName, session);
                                        }
                                        else
                                            row.Overview = "View the solutions, services and product portfolio of " + row.CompanyName;
                                    }
                                }
                                else
                                    row.Overview = "View the solutions, services and product portfolio of the company";
                            }
                            else
                            {
                                if (row.Overview.Length < 35)
                                {
                                    //user.Overview = (row["Overview"].ToString().EndsWith(".")) ? row["Overview"].ToString() : row["Overview"].ToString() + ". ";
                                    row.Overview = row.Overview + Environment.NewLine + "Check our profile for more details.";
                                }
                                else
                                    row.Overview = GlobalMethods.FixParagraphsView(row.Overview);
                            }

                            if (row.Overview != "")
                            {
                                if ((row.Overview.Contains("<br/>") || row.Overview.Contains("<br>")))
                                {
                                    row.Overview = row.Overview.Replace("<br/><br/><br/><br/>", "").Replace("<br/><br/><br/>", "").Replace("<br/><br/>", "").Replace("<br/>", "");
                                    row.Overview = row.Overview.Replace("<br><br><br><br>", "").Replace("<br><br><br>", "").Replace("<br><br>", "").Replace("<br>", "");
                                }

                                Label lblOverview = (Label)ControlFinder.FindControlRecursive(item, "LblOverview");
                                lblOverview.Text = row.Overview;
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

        protected void RdgResults_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                divWarningMsg.Visible = false;

                session.OpenConnection();

                if (vSession.ElioCompanyDetailsView != null)
                {
                    List<ElioUsersSearchInfo> resultsTbl = Sql.GetUsersWithSameSubIndustriesGroupItemsByCompanyTypeDataTable(vSession.ElioCompanyDetailsView, session);

                    if (resultsTbl != null && resultsTbl.Count > 0)
                    {
                        RdgResults.Visible = true;
                        divSimilarResults.Visible = true;

                        RdgResults.DataSource = resultsTbl;
                    }
                    else
                    {
                        RdgResults.Visible = false;
                        divSimilarResults.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Search, false);

                    //divWarningMsg.Visible = true;
                    //LblWarningMsg.Text = "Information!";
                    //LblWarningMsgContent.Text = "No companies with similar criteria as the above profile were found!";
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