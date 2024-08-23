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
using System.Collections.Generic;
using System.Web.UI;

namespace WdS.ElioPlus.pages
{
    public partial class ProfileVendorsPage : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                //ElioUsers user = null;
                //bool isError = false;
                //string errorPage = string.Empty;
                //string key = string.Empty;
                //aSuccess.Attributes.Add("style", "font-family:'Open Sans', sans-serif");

                //RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, session);

                //if (isError)
                //{
                //    if (user != null && (user.AccountStatus == (int)AccountStatus.NotCompleted || user.IsPublic == Convert.ToInt32(AccountStatus.NotPublic) && user.CompanyType == EnumHelper.GetDescription(Types.Resellers)))
                //    {
                //        bool hasPersonData = ClearbitSql.ExistsClearbitPerson(user.Id, session);
                //        bool hasCompanyData = ClearbitSql.ExistsClearbitCompany(user.Id, session);

                //        if ((!hasPersonData || !hasCompanyData) && user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString() && user.AccountStatus == (int)AccountStatus.NotCompleted)
                //        {
                //            bool success = ClearBit.FindCombinedPersonCompanyByEmail_v2(user, user.Email, session);
                //            if (!success)
                //            {
                //                Response.Redirect(errorPage, false);
                //                return;
                //            }
                //        }
                //        else
                //        {
                //            Response.Redirect(errorPage, false);
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        Response.Redirect(errorPage, false);
                //        return;
                //    }
                //}

                //vSession.ElioCompanyDetailsView = user;

                //if (!IsPostBack)
                //{
                //    UpdateStrings();

                //    try
                //    {
                //        GlobalDBMethods.AddCompanyViews(vSession.User, vSession.ElioCompanyDetailsView, vSession.Lang, session);
                //    }
                //    catch (Exception ex)
                //    {
                //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                //    }
                //}

                //FixPage();
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

        //#region Methods

        //private void PageTitle()
        //{
        //    string metaDescription = "";
        //    string metaKeywords = "";

        //    string pgTitle = GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.ElioCompanyDetailsView, out metaDescription, out metaKeywords, session);

        //    HtmlControl metaHeadDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
        //    HtmlControl metaHeadKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

        //    metaHeadDescription.Attributes["content"] = metaDescription;
        //    metaHeadKeywords.Attributes["content"] = metaKeywords;
        //}

        //private void UpdateStrings()
        //{
        //    ImgCompanyLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "literal", "1")).Text;
        //    //LblAddReview.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "1")).Text;
        //    //LblSaveProfile.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "2")).Text;
        //    //LblSendMessage.Text = "Send message";
        //    LblOverviewTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "3")).Text;
        //    LblDescriptionTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "4")).Text;
        //    LblCompanyInfoTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "6")).Text;
        //    //LblViewProductDemo.Text = LblViewProductDemoNotFull.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "20")).Text;

        //    //LblInfo.Text = "Info! ";
        //    //LblWarning.Text = "Warning! ";
        //    //LblSuccess.Text = "Done! ";
        //    LblMessageHeader.Text = "Send Message";
        //    BtnMessageSend.Text = "Send it";
        //    BtnMessageCancel.Text = "Cancel";
        //    BtnCloseModal.Text = BtnCloseReview.Text = "X";
        //    LblReviewHeader.Text = "Add Review";
        //    BtnAddReview.Text = "Submit Review";
        //    BtnCancelReview.Text = "Cancel";
        //    LblWarningReview.Text = "Error! ";
        //    LblSuccessReview.Text = "Done! ";
        //    LblWarningMsg.Text = "Error! ";
        //    LblSuccessMsg.Text = "Done! ";

        //    LblClaimMessageHeader.Text = "Claim Profile Message Form";
        //    LblClaimWarningMsg.Text = "Error! ";
        //    LblClaimSuccessMsg.Text = "Done!";
        //    BtnCancelMessage.Text = "Close";
        //    BtnCloseClaimMsg.Text = "X";
        //    BtnSendClaim.Text = "Send it";
        //    BtnCancelClaimMsg.Text = "Cancel";
        //    LblClaimMessageHeader.Text = "Claim Profile Message Form";
        //    //LblClaimProfile.Text = "Claim this profile";

        //    //Rttp1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "1")).Text;
        //    //Rttp2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "2")).Text;
        //    //Rttp3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "3")).Text;
        //    //Rttp4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "4")).Text;
        //    //Rttp5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "5")).Text;
        //    BtnCloseMessage.Text = "X";
        //    LblMessageGoFull.Text = "Create a full profile for free!";
        //    LblGoFullTitle.Text = "Complete your registration in order to have access to this feature! ";

        //    BtnCancelMessage.Text = "Close";
        //    LblClaimMessageHeader.Text = "Claim Profile Message Form";
        //    LblClaimWarningMsg.Text = "Error! ";
        //    LblClaimSuccessMsg.Text = "Done!";
        //    BtnCancelMessage.Text = "Close";
        //    BtnCloseClaimMsg.Text = "X";
        //    BtnSendClaim.Text = "Send it";
        //    BtnCancelClaimMsg.Text = "Cancel";
        //    LblClaimMessageHeader.Text = "Claim Profile Message Form";
        //    //LblClaimProfile.Text = "Claim this profile";

        //    #region Industries

        //    LblIndAdvMark.Text = "Advertising/Marketing";
        //    LblIndCommun.Text = "Communications";
        //    LblIndConsWeb.Text = "Consumer Web";
        //    LblIndDigMed.Text = "Digital Media";
        //    LblIndEcom.Text = "E-Commerce";
        //    LblIndEduc.Text = "eLearning";
        //    LblIndEnter.Text = "Enterprise";
        //    LblIndEntGam.Text = "Entertainment/Games";
        //    LblIndHard.Text = "Hardware";
        //    LblIndMob.Text = "Mobile";
        //    LblIndNetHos.Text = "Network/Hosting";
        //    LblIndSocMed.Text = "Social Media";
        //    LblIndSoft.Text = "Software";
        //    LblInd14.Text = "Aerospace and Defense";
        //    LblInd15.Text = " Automotive";
        //    LblInd16.Text = " Banking";
        //    LblInd17.Text = " Chemicals & Life Sciences";
        //    LblInd18.Text = " Distribution";
        //    LblInd19.Text = " Electronics";
        //    LblInd20.Text = " Energy";
        //    LblInd21.Text = " Engineering & Construction";
        //    LblInd22.Text = " Equipment Service and Rental";
        //    LblInd23.Text = " Fashion";
        //    LblInd24.Text = " Financial Services and Insurance";
        //    LblInd25.Text = " Food and Beverage/CPG";
        //    LblInd26.Text = " General Industry";
        //    LblInd27.Text = " Healthcare";
        //    LblInd28.Text = " High Tech & Communications";
        //    LblInd29.Text = " Hospitality";
        //    LblInd30.Text = " Legal Services";
        //    LblInd31.Text = " Logistics and 3PL";
        //    LblInd32.Text = " Manufacturing";
        //    LblInd33.Text = " Media";
        //    LblInd34.Text = " Oil & Gas";
        //    LblInd35.Text = " Professional Services";
        //    LblInd36.Text = " Public Sector";
        //    LblInd37.Text = " Real Estate";
        //    LblInd38.Text = " Retail";
        //    LblInd39.Text = " Shipping";
        //    LblInd40.Text = " Telecommunications";
        //    LblInd41.Text = " Travel & Transportation";
        //    LblInd42.Text = " Utilities";

        //    #endregion

        //    #region Partner Program

        //    LblProgWhiteL.Text = "White Label";
        //    LblProgResel.Text = "Reseller";
        //    LblProgVAR.Text = "Value Added Reseller (VAR)";
        //    LblProgDistr.Text = "Distributor";
        //    LblProgAPIprg.Text = "API Program (Developers)";
        //    LblProgSysInteg.Text = "System Integrator";
        //    LblProgServProv.Text = "Managed Service Provider";

        //    #endregion

        //    #region Market

        //    LblMarkConsum.Text = "Consumers (B2C)";
        //    LblMarkSOHO.Text = "Small office - home office: SOHO (B2B)";
        //    LblMarkSmallMid.Text = "Small & mid-sized businesses (B2B)";
        //    LblMarkEnter.Text = "Enterprise (B2B)";

        //    #endregion

        //    #region Api

        //    #endregion

        //    #region Categories

        //    Lbl1_1.Text = "Email Marketing";
        //    Lbl1_2.Text = "Campaign Management";
        //    Lbl1_3.Text = "Marketing Automation";
        //    Lbl1_4.Text = "Content Marketing";
        //    Lbl1_5.Text = "SEO & SEM";
        //    Lbl1_6.Text = "Social Media Marketing";
        //    Lbl1_7.Text = "Affiliate Marketing";
        //    Lbl1_8.Text = "Surveys & Forms";
        //    Lbl1_9.Text = "Ad Serving";
        //    Lbl1_10.Text = "Event Management";
        //    Lbl1_11.Text = "Sales Process Management";
        //    Lbl1_12.Text = "Quotes & Orders";
        //    Lbl1_13.Text = "Document Generation";
        //    Lbl1_14.Text = "Sales Intelligence";
        //    Lbl1_15.Text = "Engagement Tools";
        //    Lbl1_16.Text = "POS";
        //    Lbl1_17.Text = "E-Signature";
        //    Lbl1_18.Text = "ECM";
        //    Lbl2_1.Text = "CRM";
        //    Lbl2_2.Text = "Help Desk";
        //    Lbl2_3.Text = "Live Chat";
        //    Lbl2_4.Text = "Feedback Management";
        //    Lbl2_5.Text = "Gamification & Loyalty";
        //    Lbl2_6.Text = "Chatbot";
        //    Lbl3_1.Text = "Project Management Tools";
        //    Lbl3_2.Text = "Chat & Web Conference";
        //    Lbl3_3.Text = "Knowledge Management";
        //    Lbl3_4.Text = "File Sharing Software";
        //    Lbl4_1.Text = "Business Process Management";
        //    Lbl4_2.Text = "Digital Asset Management";
        //    Lbl4_3.Text = "ERP";
        //    Lbl4_4.Text = "Inventory Management";
        //    Lbl4_5.Text = "Shipping & Tracking";
        //    Lbl4_6.Text = "Supply Chain Management";
        //    Lbl4_7.Text = "Warehouse Management";
        //    Lbl4_8.Text = "Supply Chain Execution";
        //    Lbl4_9.Text = "Track Management";
        //    Lbl4_10.Text = "Workflow Management";
        //    Lbl4_11.Text = "Enterprise Asset Management";
        //    Lbl4_12.Text = "Facility Management";
        //    Lbl4_13.Text = "Asset Lifecycle Management";
        //    Lbl4_14.Text = "CMMS";
        //    Lbl4_15.Text = "Fleet Management";
        //    Lbl4_16.Text = "Change Management";
        //    Lbl4_17.Text = "Procurement";
        //    Lbl5_1.Text = "Analytics Software";
        //    Lbl5_2.Text = "Business Intelligence";
        //    Lbl5_3.Text = "Data Visualization";
        //    Lbl5_4.Text = "Competitive Intelligence";
        //    Lbl5_5.Text = "Location Intelligence";
        //    Lbl6_1.Text = "Accounting";
        //    Lbl6_2.Text = "Payment Processing";
        //    Lbl6_3.Text = "Time & Expenses";
        //    Lbl6_4.Text = "Billing & Invoicing";
        //    Lbl6_5.Text = "Budgeting";
        //    Lbl7_1.Text = "Applicant Tracking";
        //    Lbl7_2.Text = "HR Administration";
        //    Lbl7_3.Text = "Payroll";
        //    Lbl7_4.Text = "Performance Management";
        //    Lbl7_5.Text = "Recruiting";
        //    Lbl7_6.Text = "Learning Management System";
        //    Lbl7_7.Text = "Time & Expense";
        //    Lbl8_1.Text = "API Tools";
        //    Lbl8_2.Text = "Bug Trackers";
        //    Lbl8_3.Text = "Development Tools";
        //    Lbl8_4.Text = "eCommerce";
        //    Lbl8_5.Text = "Frameworks & Libraries";
        //    Lbl8_6.Text = "Mobile Development";
        //    Lbl8_7.Text = "Optimization";
        //    Lbl8_8.Text = "Usability Testing";
        //    Lbl8_9.Text = "Websites";
        //    Lbl9_1.Text = "Cloud Integration (iPaaS)";
        //    Lbl9_2.Text = "Cloud Management";
        //    Lbl9_3.Text = "Cloud Storage";
        //    Lbl9_4.Text = "Remote Access";
        //    Lbl9_5.Text = "Virtualization";
        //    Lbl9_6.Text = "Web Hosting";
        //    Lbl9_7.Text = "Web Monitoring";
        //    Lbl9_8.Text = "VOIP";
        //    Lbl9_9.Text = "Big Data";
        //    Lbl9_10.Text = "Data Warehousing";
        //    Lbl9_11.Text = "Databases";
        //    Lbl9_12.Text = "Data Integration";
        //    Lbl9_13.Text = "Data Management";
        //    Lbl10_1.Text = "Calendar & Scheduling";
        //    Lbl10_2.Text = "Email";
        //    Lbl10_3.Text = "Note Taking";
        //    Lbl10_4.Text = "Password Management";
        //    Lbl10_5.Text = "Presentations";
        //    Lbl10_6.Text = "Productivity Suites";
        //    Lbl10_7.Text = "Spreadsheets";
        //    Lbl10_8.Text = "Task Management";
        //    Lbl10_9.Text = "Time Management";
        //    Lbl11_1.Text = "Data Security";
        //    Lbl11_2.Text = "Vulnerability Management";
        //    Lbl11_3.Text = "Firewall";
        //    Lbl11_4.Text = "Mobile Data Security";
        //    Lbl11_5.Text = "Backup & Restore";
        //    Lbl11_6.Text = "Data Masking";
        //    Lbl11_7.Text = "Identity Management";
        //    Lbl11_8.Text = "Risk Management";
        //    Lbl11_9.Text = "Penetration Testing";
        //    Lbl11_10.Text = "Application Security";
        //    Lbl11_11.Text = "Governance, Risk & Compliance (GRC)";
        //    Lbl11_12.Text = "Compliance";
        //    Lbl11_13.Text = "Fraud Prevention";
        //    Lbl12_1.Text = "Graphic Design";
        //    Lbl12_2.Text = "Infographics";
        //    Lbl12_3.Text = "Video Editing";
        //    Lbl13_1.Text = "eLearning";
        //    Lbl13_2.Text = "Healthcare";
        //    Lbl13_3.Text = "Simulation Software";
        //    Lbl14_1.Text = "Mobility";
        //    Lbl14_2.Text = "Collaboration";
        //    Lbl14_3.Text = "Conferencing";
        //    Lbl14_4.Text = "Unified Messaging";
        //    Lbl14_5.Text = "Unified Communications";
        //    Lbl14_6.Text = "Team Collaboration";
        //    Lbl14_7.Text = "Video Conferencing";
        //    Lbl15_1.Text = "General-Purpose CAD";
        //    Lbl15_2.Text = "CAM";
        //    Lbl15_3.Text = "PLM";
        //    Lbl15_4.Text = "PDM (Product Data Management)";
        //    Lbl15_5.Text = "BIM";
        //    Lbl15_6.Text = "3D Architecture";
        //    Lbl15_7.Text = "3D CAD";

        //    #endregion

        //    #region Technologies

        //    Label01.Text = "G Suite";
        //    Label02.Text = "Office 365";
        //    Label03.Text = "Kerio";
        //    Label04.Text = "Zimbra";
        //    Label05.Text = "Mimecast";
        //    Label06.Text = "Icewarp";
        //    Label07.Text = "Office 365";
        //    Label08.Text = "Sharepoint";
        //    Label09.Text = "Dynamics";
        //    Label010.Text = "Yammer";
        //    Label011.Text = "Azure";
        //    Label012.Text = "PowerBI";
        //    Label013.Text = "Skype";
        //    Label014.Text = "Check Point";
        //    Label015.Text = "McAfee";
        //    Label016.Text = "Imperva";
        //    Label017.Text = "Sophos";
        //    Label018.Text = "Juniper Networks";
        //    Label019.Text = "Salesforce";
        //    Label020.Text = "Dynamics";
        //    Label021.Text = "SugarCRM";
        //    Label022.Text = "Zoho";
        //    Label023.Text = "Sage";
        //    Label024.Text = "Vtiger";
        //    Label025.Text = "VTE";
        //    Label026.Text = "SuiteCRM";
        //    Label027.Text = "Hubspot";
        //    Label028.Text = "ACT";
        //    Label029.Text = "BPM'Online";
        //    Label030.Text = "SAP Business One";
        //    Label031.Text = "Infor";
        //    Label032.Text = "Odoo";
        //    Label033.Text = "Epicor";
        //    Label034.Text = "Tally";
        //    Label035.Text = "Moodle";
        //    Label036.Text = "Articulate";
        //    Label037.Text = "Totara";
        //    Label038.Text = "Docebo";
        //    Label039.Text = "eFront";

        //    #endregion
        //}

        //private void FixButtons()
        //{
        //    if (vSession.ElioCompanyDetailsView != null)
        //    {
        //        aAddToMyMatches.Visible = false;  //vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();
        //        aSuccess.Visible = false;
        //    }
        //    else
        //        Response.Redirect(ControlLoader.Search, false);

        //    if (vSession.User != null)
        //    {
        //        if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
        //        {
        //            if (vSession.User.CompanyType == Types.Vendors.ToString())
        //            {
        //                if (vSession.ElioCompanyDetailsView.CompanyType != Types.Vendors.ToString())
        //                {
        //                    bool isAlreadyConnection = Sql.IsConnection(vSession.User.Id, vSession.ElioCompanyDetailsView.Id, session);
        //                    if (!isAlreadyConnection)
        //                    {
        //                        //aAddToMyMatches.Visible = true;
        //                        aSuccess.Visible = false;
        //                    }
        //                    else
        //                    {
        //                        aAddToMyMatches.Visible = false;
        //                        aSuccess.Visible = true;
        //                    }

        //                    if (aAddToMyMatches.Visible)
        //                    {
        //                        if (vSession.User.BillingType == (int)BillingTypePacket.FreemiumPacketType)
        //                        {
        //                            aAddToMyMatches.HRef = "https://calendly.com/elioplus";
        //                        }
        //                        else
        //                        {
        //                            aAddToMyMatches.ServerClick += aAddToMyMatches_ServerClick;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                aAddToMyMatches.Visible = false;
        //                aSuccess.Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            aAddToMyMatches.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
        //            aSuccess.Visible = false;
        //        }
        //    }
        //    else
        //    {
        //        aAddToMyMatches.ServerClick += aAddToMyMatches_ServerClick;
        //        //aAddToMyMatches.Visible = true;
        //        aSuccess.Visible = false;
        //    }
        //}

        //private void FixPage()
        //{
        //    PageTitle();
        //    FixButtons();
        //    SetLinks();

        //    aClaimProfile.Visible = (vSession.ElioCompanyDetailsView.UserApplicationType == (int)UserApplicationType.ThirdParty && !ClearbitSql.IsPersonProfileClaimed(vSession.ElioCompanyDetailsView.Id, session));

        //    LoadDetails();

        //    if (!IsPostBack)
        //    {
        //        if (vSession.User != null)
        //            LoadRequestDemoData();

        //        FixHeaderButtons();
        //    }

        //    if (vSession.ElioCompanyDetailsView != null)
        //    {
        //        if (vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers) && vSession.ElioCompanyDetailsView.AccountStatus == (int)AccountStatus.Completed)
        //        {
        //            divDescriptionArea.Visible = false;
        //        }
        //    }
        //}

        //private void FixHeaderButtons()
        //{
        //    try
        //    {
        //        DataTable table = session.GetDataTable(@"with ind
        //                                                as
        //                                                (
        //                                                Select case when count(ui.id) > 0 then 'true' else 'false' end as industries_count
        //                                                from elio_industries i
        //                                                inner join elio_users_industries ui
        //                                                on ui.industry_id=i.id 
        //                                                where user_id = @user_id
        //                                                )

        //                                                ,mar
        //                                                as
        //                                                (
        //                                                Select case when count(um.id) > 0 then 'true' else 'false' end as markets_count 
        //                                                from elio_markets m
        //                                                inner join elio_users_markets um
        //                                                on um.market_id=m.id 
        //                                                where user_id = @user_id
        //                                                )

        //                                                ,par
        //                                                as
        //                                                (
        //                                                Select case when count(up.id) > 0 then 'true' else 'false' end as partners_count  
        //                                                from elio_partners p
        //                                                inner join elio_users_partners up
        //                                                on up.partner_id=p.id 
        //                                                where user_id = @user_id
        //                                                )

        //                                                ,pr
        //                                                as
        //                                                (
        //                                                SELECT case when count(rp.id) > 0 then 'true' else 'false' end as products_count 
        //                                                FROM Elio_registration_products rp
        //                                                inner join Elio_users_registration_products urp
        //                                                on urp.reg_products_id = rp.id
        //                                                where user_id = @user_id
        //                                                )

        //                                                ,cat
        //                                                as
        //                                                (
        //                                                SELECT case when count(usigi.id) > 0 then 'true' else 'false' end as categories_count 
        //                                                FROM Elio_sub_industries_group_items sigi
        //                                                inner join Elio_users_sub_industries_group_items usigi
        //                                                on usigi.sub_industry_group_item_id = sigi.id
        //                                                where user_id = @user_id
        //                                                )

        //                                                select * 
        //                                                from ind,mar,par,pr,cat"
        //                            , DatabaseHelper.CreateIntParameter("@user_id", vSession.ElioCompanyDetailsView.Id));

        //        if (table != null && table.Rows.Count > 0)
        //        {
        //            int count = 2;

        //            divIndustriesSection.Visible = industries.Visible = Convert.ToBoolean(table.Rows[0]["industries_count"]);
        //            if (divIndustriesSection.Visible)
        //                count++;

        //            divMarketsSection.Visible = markets.Visible = Convert.ToBoolean(table.Rows[0]["markets_count"]);
        //            if (divMarketsSection.Visible)
        //                count++;

        //            divProgramsSection.Visible = programs.Visible = Convert.ToBoolean(table.Rows[0]["partners_count"]);
        //            if (divProgramsSection.Visible)
        //                count++;

        //            divProductsIntegrationsSection.Visible = products.Visible = Convert.ToBoolean(table.Rows[0]["products_count"]);
        //            if (divProductsIntegrationsSection.Visible)
        //                count++;

        //            divCategoriesSection.Visible = categories.Visible = Convert.ToBoolean(table.Rows[0]["categories_count"]) && vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers);
        //            if (divCategoriesSection.Visible)
        //                count++;

        //            if (count == 2)
        //            {
        //                divOverviewSection.Attributes["class"] = "col-md-6";
        //                divContactSection.Attributes["class"] = "col-md-6";
        //            }
        //            else if (count == 3)
        //            {
        //                divOverviewSection.Attributes["class"] = "col-md-4";
        //                divContactSection.Attributes["class"] = "col-md-4";

        //                if (divIndustriesSection.Visible)
        //                    divIndustriesSection.Attributes["class"] = "col-md-4";

        //                if (divMarketsSection.Visible)
        //                    divMarketsSection.Attributes["class"] = "col-md-4";

        //                if (divProgramsSection.Visible)
        //                    divProgramsSection.Attributes["class"] = "col-md-4";

        //                if (divProductsIntegrationsSection.Visible)
        //                    divProductsIntegrationsSection.Attributes["class"] = "col-md-4";

        //                if (divCategoriesSection.Visible)
        //                    divCategoriesSection.Attributes["class"] = "col-md-4";
        //            }
        //            else if (count == 4)
        //            {
        //                divOverviewSection.Attributes["class"] = "col-md-3";
        //                divContactSection.Attributes["class"] = "col-md-3";

        //                if (divIndustriesSection.Visible)
        //                    divIndustriesSection.Attributes["class"] = "col-md-3";

        //                if (divMarketsSection.Visible)
        //                    divMarketsSection.Attributes["class"] = "col-md-3";

        //                if (divProgramsSection.Visible)
        //                    divProgramsSection.Attributes["class"] = "col-md-3";

        //                if (divProductsIntegrationsSection.Visible)
        //                    divProductsIntegrationsSection.Attributes["class"] = "col-md-3";

        //                if (divCategoriesSection.Visible)
        //                    divCategoriesSection.Attributes["class"] = "col-md-3";
        //            }
        //            else
        //            {
        //                divOverviewSection.Attributes["class"] = "col-md-2";
        //                divContactSection.Attributes["class"] = "col-md-2";

        //                if (divIndustriesSection.Visible)
        //                    divIndustriesSection.Attributes["class"] = "col-md-2";

        //                if (divMarketsSection.Visible)
        //                    divMarketsSection.Attributes["class"] = "col-md-2";

        //                if (divProgramsSection.Visible)
        //                    divProgramsSection.Attributes["class"] = "col-md-2";

        //                if (divProductsIntegrationsSection.Visible)
        //                    divProductsIntegrationsSection.Attributes["class"] = "col-md-2";

        //                if (divCategoriesSection.Visible)
        //                    divCategoriesSection.Attributes["class"] = "col-md-2";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //private void SetLinks()
        //{
        //    if (vSession.User != null)
        //    {
        //        //if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
        //        //aPartnerPortalLogin.HRef = "/" + Regex.Replace(vSession.ElioCompanyDetailsView.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-login";
        //        //else
        //        //aPartnerPortalLogin.HRef = "/" + Regex.Replace(vSession.ElioCompanyDetailsView.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-free-sign-up";
        //    }
        //    //else
        //    //aPartnerPortalLogin.HRef = "/" + Regex.Replace(vSession.ElioCompanyDetailsView.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-free-sign-up";
        //}

        //private void LoadRequestDemoData()
        //{
        //    if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
        //    {
        //        if (!string.IsNullOrEmpty(vSession.User.FirstName))
        //            TbxFirstName.Text = vSession.User.FirstName;

        //        if (!string.IsNullOrEmpty(vSession.User.LastName))
        //            TbxLastName.Text = vSession.User.LastName;

        //        if (!string.IsNullOrEmpty(vSession.User.Email))
        //            TbxCompanyEmail.Text = vSession.User.Email;

        //        if (!string.IsNullOrEmpty(vSession.User.CompanyName))
        //            TbxBusinessName.Text = vSession.User.CompanyName;
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(vSession.User.Email))
        //            TbxCompanyEmail.Text = vSession.User.Email;
        //    }
        //}

        //private void LoadIndustries()
        //{
        //    HdnIndAdvMark.Value = "0";
        //    HdnIndCommun.Value = "0";
        //    HdnIndConsWeb.Value = "0";
        //    HdnIndDigMed.Value = "0";
        //    HdnIndEcom.Value = "0";
        //    HdnIndEduc.Value = "0";
        //    HdnIndEnter.Value = "0";
        //    HdnIndEntGam.Value = "0";
        //    HdnIndHard.Value = "0";
        //    HdnIndMob.Value = "0";
        //    HdnIndNetHos.Value = "0";
        //    HdnIndSocMed.Value = "0";
        //    HdnIndSoft.Value = "0";
        //    HdnInd14.Value = "0";
        //    HdnInd15.Value = "0";
        //    HdnInd16.Value = "0";
        //    HdnInd17.Value = "0";
        //    HdnInd18.Value = "0";
        //    HdnInd19.Value = "0";
        //    HdnInd20.Value = "0";
        //    HdnInd21.Value = "0";
        //    HdnInd22.Value = "0";
        //    HdnInd23.Value = "0";
        //    HdnInd24.Value = "0";
        //    HdnInd25.Value = "0";
        //    HdnInd26.Value = "0";

        //    HdnInd27.Value = "0";
        //    HdnInd28.Value = "0";
        //    HdnInd29.Value = "0";
        //    HdnInd30.Value = "0";
        //    HdnInd31.Value = "0";
        //    HdnInd32.Value = "0";
        //    HdnInd33.Value = "0";
        //    HdnInd34.Value = "0";
        //    HdnInd35.Value = "0";
        //    HdnInd36.Value = "0";
        //    HdnInd37.Value = "0";
        //    HdnInd38.Value = "0";
        //    HdnInd39.Value = "0";

        //    HdnInd40.Value = "0";
        //    HdnInd41.Value = "0";
        //    HdnInd42.Value = "0";

        //    List<ElioIndustries> userIndustries = Sql.GetUsersIndustries(vSession.ElioCompanyDetailsView.Id, session);

        //    LblIndustriesTitle.Visible = userIndustries.Count > 0;

        //    if (userIndustries.Count > 0)
        //    {
        //        foreach (ElioIndustries industry in userIndustries)
        //        {
        //            if (industry.IndustryDescription == "Advertising/Marketing")
        //            {
        //                HdnIndAdvMark.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Communications")
        //            {
        //                HdnIndCommun.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Consumer Web")
        //            {
        //                HdnIndConsWeb.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Digital Media")
        //            {
        //                HdnIndDigMed.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "E-Commerce")
        //            {
        //                HdnIndEcom.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "eLearning")
        //            {
        //                HdnIndEduc.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Enterprise")
        //            {
        //                HdnIndEnter.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Entertainment/Games")
        //            {
        //                HdnIndEntGam.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Hardware")
        //            {
        //                HdnIndHard.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Mobile")
        //            {
        //                HdnIndMob.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Network/Hosting")
        //            {
        //                HdnIndNetHos.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Social Media")
        //            {
        //                HdnIndSocMed.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Software")
        //            {
        //                HdnIndSoft.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Aerospace and Defense")
        //            {
        //                HdnInd14.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Automotive")
        //            {
        //                HdnInd15.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Banking")
        //            {
        //                HdnInd16.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Chemicals & Life Sciences")
        //            {
        //                HdnInd17.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Distribution")
        //            {
        //                HdnInd18.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Electronics")
        //            {
        //                HdnInd19.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Energy")
        //            {
        //                HdnInd20.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Engineering & Construction")
        //            {
        //                HdnInd21.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Equipment Service and Rental")
        //            {
        //                HdnInd22.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Fashion")
        //            {
        //                HdnInd23.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Financial Services and Insurance")
        //            {
        //                HdnInd24.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Food and Beverage/CPG")
        //            {
        //                HdnInd25.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "General Industry")
        //            {
        //                HdnInd26.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Healthcare")
        //            {
        //                HdnInd27.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "High Tech & Communications")
        //            {
        //                HdnInd28.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Hospitality")
        //            {
        //                HdnInd29.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Legal Services")
        //            {
        //                HdnInd30.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Logistics and 3PL")
        //            {
        //                HdnInd31.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Manufacturing")
        //            {
        //                HdnInd32.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Media")
        //            {
        //                HdnInd33.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Oil & Gas")
        //            {
        //                HdnInd34.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Professional Services")
        //            {
        //                HdnInd35.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Public Sector")
        //            {
        //                HdnInd36.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Real Estate")
        //            {
        //                HdnInd37.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Retail")
        //            {
        //                HdnInd38.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Shipping")
        //            {
        //                HdnInd39.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Telecommunications")
        //            {
        //                HdnInd40.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Travel & Transportation")
        //            {
        //                HdnInd41.Value = "1";
        //            }
        //            else if (industry.IndustryDescription == "Utilities")
        //            {
        //                HdnInd42.Value = "1";
        //            }
        //        }

        //        ScriptManager.RegisterStartupScript(this, GetType(), "SetIndustries", "SetIndustries();", true);
        //    }
        //    else
        //    {
        //        divIndustriesSection.Visible = false;
        //        industries.Visible = false;
        //        //LblIndustryValue.Text = "-";
        //    }
        //}

        //private void LoadMarkets()
        //{
        //    HdnMarkConsum.Value = "0";
        //    HdnMarkSOHO.Value = "0";
        //    HdnMarkSmallMid.Value = "0";
        //    HdnMarkEnter.Value = "0";

        //    List<ElioMarkets> userMarkets = Sql.GetUsersMarkets(vSession.ElioCompanyDetailsView.Id, session);

        //    LblMarketTitle.Visible = userMarkets.Count > 0;

        //    if (userMarkets.Count > 0)
        //    {
        //        foreach (ElioMarkets market in userMarkets)
        //        {
        //            if (market.MarketDescription == "Consumers (B2C)")
        //            {
        //                HdnMarkConsum.Value = "1";
        //            }
        //            else if (market.MarketDescription == "Small office - home office: SOHO (B2B)")
        //            {
        //                HdnMarkSOHO.Value = "1";
        //            }
        //            else if (market.MarketDescription == "Small & mid-sized businesses (B2B)")
        //            {
        //                HdnMarkSmallMid.Value = "1";
        //            }
        //            else if (market.MarketDescription == "Enterprise (B2B)")
        //            {
        //                HdnMarkEnter.Value = "1";
        //            }
        //        }

        //        ScriptManager.RegisterStartupScript(this, GetType(), "SetMarkets", "SetMarkets();", true);
        //    }
        //    else
        //    {
        //        divMarketsSection.Visible = false;
        //        markets.Visible = false;
        //    }
        //}

        //private string GetProgram(string userPartnerProgram)
        //{
        //    if (userPartnerProgram == "White Label")
        //    {
        //        return "/white-label-partner-programs";
        //    }
        //    else if (userPartnerProgram == "Reseller")
        //    {
        //        return "/saas-partner-programs";
        //    }
        //    else if (userPartnerProgram == "Managed Service Provider")
        //    {
        //        return "/msps-partner-programs";
        //    }
        //    else if (userPartnerProgram == "System Integrator")
        //    {
        //        return "/systems-integrators-partner-programs";
        //    }
        //    else
        //        return "";
        //}

        //private void LoadProgramsForVendors()
        //{
        //    divVendorsPartnerPrograms.Visible = true;
        //    divResellersPartnerProgramCbx.Visible = false;

        //    List<ElioPartners> userPartners = Sql.GetUsersPartners(vSession.ElioCompanyDetailsView.Id, session);
        //    if (userPartners.Count > 0)
        //    {
        //        int count = 0;

        //        if (count < userPartners.Count)
        //        {
        //            aPartPr1.Visible = true;
        //            aPartPr1.HRef = GetProgram(userPartners[count].PartnerDescription);

        //            LblPartPr1.Text = userPartners[count].PartnerDescription;
        //            count++;
        //        }

        //        if (count < userPartners.Count)
        //        {
        //            aPartPr2.Visible = true;
        //            aPartPr2.HRef = GetProgram(userPartners[count].PartnerDescription);

        //            LblPartPr2.Text = userPartners[count].PartnerDescription;
        //            count++;
        //        }

        //        if (count < userPartners.Count)
        //        {
        //            aPartPr3.Visible = true;
        //            aPartPr3.HRef = GetProgram(userPartners[count].PartnerDescription);

        //            LblPartPr3.Text = userPartners[count].PartnerDescription;
        //            count++;
        //        }

        //        if (count < userPartners.Count)
        //        {
        //            aPartPr4.Visible = true;
        //            aPartPr4.HRef = GetProgram(userPartners[count].PartnerDescription);

        //            LblPartPr4.Text = userPartners[count].PartnerDescription;
        //            count++;
        //        }

        //        if (count < userPartners.Count)
        //        {
        //            aPartPr5.Visible = true;
        //            aPartPr5.HRef = GetProgram(userPartners[count].PartnerDescription);

        //            LblPartPr5.Text = userPartners[count].PartnerDescription;
        //            count++;
        //        }

        //        if (count < userPartners.Count)
        //        {
        //            aPartPr6.Visible = true;
        //            aPartPr6.HRef = GetProgram(userPartners[count].PartnerDescription);

        //            LblPartPr6.Text = userPartners[count].PartnerDescription;
        //            count++;
        //        }

        //        if (count < userPartners.Count)
        //        {
        //            aPartPr7.Visible = true;
        //            aPartPr7.HRef = GetProgram(userPartners[count].PartnerDescription);

        //            LblPartPr7.Text = userPartners[count].PartnerDescription;
        //            count++;
        //        }
        //    }
        //    else
        //    {
        //        divProductsIntegrationsSection.Visible = false;
        //        products.Visible = false;
        //        divVendorsPartnerPrograms.Visible = false;
        //    }
        //}

        //private void LoadProgramsForResellers()
        //{
        //    divResellersPartnerProgramCbx.Visible = true;
        //    divVendorsPartnerPrograms.Visible = false;

        //    HdnProgWhiteL.Value = "0";
        //    HdnProgResel.Value = "0";
        //    HdnProgVAR.Value = "0";
        //    HdnProgDistr.Value = "0";
        //    HdnProgAPIprg.Value = "0";
        //    HdnProgSysInteg.Value = "0";
        //    HdnProgServProv.Value = "0";

        //    List<ElioPartners> userPrograms = Sql.GetUsersPartners(vSession.ElioCompanyDetailsView.Id, session);

        //    LblPartnerProgramTitle.Visible = userPrograms.Count > 0;

        //    if (userPrograms.Count > 0)
        //    {
        //        foreach (ElioPartners program in userPrograms)
        //        {
        //            if (program.PartnerDescription == "White Label")
        //            {
        //                HdnProgWhiteL.Value = "1";
        //            }
        //            else if (program.PartnerDescription == "Reseller")
        //            {
        //                HdnProgResel.Value = "1";
        //            }
        //            else if (program.PartnerDescription == "Value Added Reseller (VAR)")
        //            {
        //                HdnProgVAR.Value = "1";
        //            }
        //            else if (program.PartnerDescription == "Distributor")
        //            {
        //                HdnProgDistr.Value = "1";
        //            }
        //            else if (program.PartnerDescription == "API Program (Developers)")
        //            {
        //                HdnProgAPIprg.Value = "1";
        //            }
        //            else if (program.PartnerDescription == "System Integrator")
        //            {
        //                HdnProgSysInteg.Value = "1";
        //            }
        //            else if (program.PartnerDescription == "Managed Service Provider")
        //            {
        //                HdnProgServProv.Value = "1";
        //            }
        //        }

        //        ScriptManager.RegisterStartupScript(this, GetType(), "SetPrograms", "SetPrograms();", true);
        //    }
        //    else
        //    {
        //        divProgramsSection.Visible = false;
        //        programs.Visible = false;
        //    }
        //}

        //private void LoadVendorIntegrations()
        //{
        //    divChannelPartnersProducts.Visible = false;
        //    divVendorsIntegrations.Visible = true;

        //    List<ElioRegistrationIntegrations> userIntegrations = Sql.GetUserRegistrationIntegrations(vSession.ElioCompanyDetailsView.Id, session);
        //    int count = 0;
        //    if (userIntegrations.Count > 0)
        //    {
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt1.Visible = true;
        //            LblInt1.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt2.Visible = true;
        //            LblInt2.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt3.Visible = true;
        //            LblInt3.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt4.Visible = true;
        //            LblInt4.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt5.Visible = true;
        //            LblInt5.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt6.Visible = true;
        //            LblInt6.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt7.Visible = true;
        //            LblInt7.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt8.Visible = true;
        //            LblInt8.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt9.Visible = true;
        //            LblInt9.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt10.Visible = true;
        //            LblInt10.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt11.Visible = true;
        //            LblInt11.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt12.Visible = true;
        //            LblInt12.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt13.Visible = true;
        //            LblInt13.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt14.Visible = true;
        //            LblInt14.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt15.Visible = true;
        //            LblInt15.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt16.Visible = true;
        //            LblInt16.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt17.Visible = true;
        //            LblInt17.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt18.Visible = true;
        //            LblInt18.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt19.Visible = true;
        //            LblInt19.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (count < userIntegrations.Count)
        //        {
        //            divInt20.Visible = true;
        //            LblInt20.Text = userIntegrations[count].Description;
        //            count++;
        //        }
        //        if (userIntegrations.Count > 20)
        //        {
        //            divIntMore.Visible = true;
        //            LblIntMore.Text = "More";
        //            iMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithRegistrationIntegrationsDescriptions(userIntegrations);
        //            iMore.Visible = true;
        //        }
        //    }
        //    else
        //    {
        //        divVendorsIntegrations.Visible = false;
        //        integrations.Visible = false;
        //    }
        //}

        //private void LoadChannelPartnerProductsInPlaceHolder()
        //{
        //    divChannelPartnersProducts.Visible = true;
        //    divVendorsIntegrations.Visible = false;

        //    string productUrl = "profile/channel-partners/";
        //    List<ElioRegistrationProducts> userProducts = Sql.GetRegistrationProductsDescriptionByUserId(vSession.ElioCompanyDetailsView.Id, session);

        //    if (userProducts.Count > 0)
        //    {
        //        bool hasCity = false;
        //        string cityURL = "";
        //        if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
        //        {
        //            hasCity = true;
        //            divCityProduct.Visible = true;
        //            //cityURL = vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();

        //            if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.CompanyRegion) && !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Country) && !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
        //            {
        //                if (vSession.ElioCompanyDetailsView.Country == "India" || vSession.ElioCompanyDetailsView.Country == "United States" || vSession.ElioCompanyDetailsView.Country == "United Kingdom" || vSession.ElioCompanyDetailsView.Country == "Australia")
        //                {
        //                    string state = Sql.GetStateByRegionCountryCityTbl(vSession.ElioCompanyDetailsView.CompanyRegion, vSession.ElioCompanyDetailsView.Country, vSession.ElioCompanyDetailsView.City, session);
        //                    if (state != "")
        //                        cityURL = vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + state.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
        //                    else
        //                        cityURL = vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
        //                }
        //                else
        //                    cityURL = vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
        //            }
        //            else
        //                cityURL = vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
        //        }

        //        PhProductsCityContent.Controls.Clear();
        //        PhProductsContent.Controls.Clear();

        //        PhProductsCityContentTrans.Controls.Clear();
        //        PhProductsCountryContentTrans.Controls.Clear();

        //        foreach (ElioRegistrationProducts product in userProducts)
        //        {
        //            HtmlAnchor aProd = new HtmlAnchor();
        //            aProd.ID = "aProd_" + product.Id.ToString();
        //            aProd.Attributes["class"] = "s-tags md-sortlist-theme";
        //            aProd.HRef = productUrl + product.Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //            Label lblProd = new Label();
        //            lblProd.ID = "lblProd_" + product.Id.ToString();
        //            lblProd.Text = product.Description;

        //            aProd.Controls.Clear();
        //            aProd.Controls.Add(lblProd);

        //            PhProductsContent.Controls.Add(aProd);

        //            if (hasCity)
        //            {
        //                HtmlAnchor aProdCity = new HtmlAnchor();
        //                aProdCity.ID = "aProdCity_" + product.Id.ToString();
        //                aProdCity.Attributes["class"] = "s-tags md-sortlist-theme";
        //                aProdCity.HRef = cityURL + "/channel-partners/" + product.Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                Label lblProdCity = new Label();
        //                lblProdCity.ID = "lblCity_" + product.Id.ToString();
        //                lblProdCity.Text = product.Description + " in " + vSession.ElioCompanyDetailsView.City;

        //                aProdCity.Controls.Clear();
        //                aProdCity.Controls.Add(lblProdCity);

        //                PhProductsCityContent.Controls.Add(aProdCity);

        //                LoadCityProductTrans(product, false);
        //            }

        //            LoadCountryProductTrans(product, true);
        //        }
        //    }
        //    else
        //    {
        //        //aProductsIntegrationsSection.Visible = false;
        //        divChannelPartnersProducts.Visible = false;
        //    }
        //}

        //private void LoadUserVerticalsInPlaceHolder()
        //{
        //    bool existCountry = true;
        //    string urlLink = "";

        //    if (vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers))
        //    {
        //        //List<string> defaultCountries = "Afghanistan,Albania,Algeria,Angola,Armenia,Azerbaijan,Argentina,Australia,Austria,Bahamas,Bahrain,Bangladesh,Barbados,Belarus,Benin,Bermuda,Bolivia,Bosnia and Herzegovina,Botswana,Bulgaria,Belgium,Brazil,Cambodia,Cameroon,Cape Verde,Chad,China, People s Republic of,China, Republic of (Taiwan),Congo,Costa Rica,Cote d Ivoire (Ivory Coast),Croatia,Cyprus,Canada,Chile,Colombia,Czech Republic,Denmark,Dominican Republic,Ecuador,Egypt,El Salvador,Estonia,Ethiopia,Fiji,Finland,France,Gabon,Gambia,Georgia,Ghana,Greece,Guatemala,Germany,Honduras,Hungary,Hong Kong,Iceland,Iran,Iraq,India,Indonesia,Ireland,Israel,Italy,Jamaica,Jordan,Japan,Kazakhstan,Kenya,Korea, South,Kuwait,Kyrgyzstan,Laos,Latvia,Lebanon,Liberia,Libya,Liechtenstein,Lithuania,Luxembourg,Macedonia,Madagascar,Malawi,Maldives,Mali,Malta,Mauritania,Monaco,Mongolia,Montenegro,Morocco,Mozambique,Myanmar (Burma),Malaysia,Mexico,Namibia,Nepal,Nicaragua,Nigeria,Netherlands,New Zealand,Norway,Oman,Pakistan,Panama,Papua New Guinea,Paraguay,Peru,Philippines,Puerto Rico,Poland,Portugal,Qatar,Romania,Rwanda,Russia,San Marino,Saudi Arabia,Senegal,Serbia,Sierra Leone,Slovakia,Slovenia,Somalia,Sri Lanka,Sudan,Suriname,Syria,Singapore,South Africa,Spain,Sweden,Switzerland,Tajikistan,Tanzania,Togo,Trinidad and Tobago,Tunisia,Turkmenistan,Thailand,Turkey,United Arab Emirates,United Kingdom,United States,Uganda,Ukraine,Uruguay,Venezuela,Vietnam,Yemen',Zambia,Zimbabwe".Split(',').ToList();
        //        //if (defaultCountries.Count > 0)
        //        //{
        //        //    foreach (string country in defaultCountries)
        //        //    {
        //        //        if (country != "" && country.Contains(vSession.ElioCompanyDetailsView.Country))
        //        //        {
        //        //            existCountry = true;
        //        //            break;
        //        //        }
        //        //    }
        //        //}

        //        if (existCountry)
        //            urlLink = vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/channel-partners/";
        //        else
        //            urlLink = "profile/channel-partners/";
        //    }

        //    bool hasPartnerProgram = false;
        //    if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
        //    {
        //        hasPartnerProgram = Sql.HasUsersPartnerProgram(vSession.ElioCompanyDetailsView.Id, "White Label", session);
        //        //divVendorsTags.Visible = hasPartnerProgram;
        //    }
        //    else
        //    {
        //        //divVendorsTags.Visible = false;
        //    }

        //    if (vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers))
        //    {
        //        List<ElioSubIndustriesGroupItems> userVarticals = Sql.GetUserSubIndustriesGroupItems(vSession.ElioCompanyDetailsView.Id, session);

        //        if (userVarticals.Count > 0)
        //        {
        //            bool hasCity = false;
        //            string cityURL = "";
        //            if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
        //            {
        //                hasCity = true;
        //                divCityCategory.Visible = true;
        //                LblSimilarCityCompanyTitle.Text = string.Format("Find similar companies in {0}: ", vSession.ElioCompanyDetailsView.City);

        //                if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.CompanyRegion) && !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Country) && !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
        //                {
        //                    if (vSession.ElioCompanyDetailsView.Country == "India" || vSession.ElioCompanyDetailsView.Country == "United States" || vSession.ElioCompanyDetailsView.Country == "United Kingdom" || vSession.ElioCompanyDetailsView.Country == "Australia")
        //                    {
        //                        string state = Sql.GetStateByRegionCountryCityTbl(vSession.ElioCompanyDetailsView.CompanyRegion, vSession.ElioCompanyDetailsView.Country, vSession.ElioCompanyDetailsView.City, session);
        //                        if (state != "")
        //                            cityURL = vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + state.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
        //                        else
        //                            cityURL = vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
        //                    }
        //                    else
        //                        cityURL = vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
        //                }
        //                else
        //                    cityURL = vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
        //            }

        //            PhVerticalsContent.Controls.Clear();
        //            PhVerticalsCityContent.Controls.Clear();
        //            //PhTagsContent.Controls.Clear();

        //            PhVerticalsCityContentTrans.Controls.Clear();
        //            PhVerticalsCountryContentTrans.Controls.Clear();

        //            foreach (ElioSubIndustriesGroupItems userVartical in userVarticals)
        //            {
        //                string verticalURL = userVartical.Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                HtmlAnchor aVert = new HtmlAnchor();
        //                aVert.ID = "aVert_" + userVartical.Id.ToString();
        //                aVert.Attributes["class"] = "s-tags md-sortlist-theme";
        //                aVert.HRef = (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) ? ControlLoader.SubIndustryProfiles("vendors", verticalURL) : urlLink + verticalURL;

        //                Label lblVert = new Label();
        //                lblVert.ID = "lblVert_" + userVartical.Id.ToString();
        //                lblVert.Text = userVartical.Description;

        //                aVert.Controls.Clear();
        //                aVert.Controls.Add(lblVert);

        //                PhVerticalsContent.Controls.Add(aVert);

        //                if (hasCity)
        //                {
        //                    HtmlAnchor aVertCity = new HtmlAnchor();
        //                    aVertCity.ID = "aVertCity_" + userVartical.Id.ToString();
        //                    aVertCity.Attributes["class"] = "s-tags md-sortlist-theme";
        //                    aVertCity.HRef = (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) ? ControlLoader.SubIndustryProfiles("vendors", cityURL + "/" + verticalURL) : cityURL + "/channel-partners/" + verticalURL;

        //                    Label lblVertCity = new Label();
        //                    lblVertCity.ID = "lblVertCity_" + userVartical.Id.ToString();
        //                    lblVertCity.Text = userVartical.Description + " in " + vSession.ElioCompanyDetailsView.City;

        //                    aVertCity.Controls.Clear();
        //                    aVertCity.Controls.Add(lblVertCity);

        //                    PhVerticalsCityContent.Controls.Add(aVertCity);

        //                    LoadCityVerticalTrans(userVartical, false);
        //                }

        //                if (hasPartnerProgram)
        //                {
        //                    HtmlAnchor aTag = new HtmlAnchor();
        //                    aTag.ID = "aTag_" + userVartical.Id.ToString();
        //                    aTag.Attributes["class"] = "s-tags md-sortlist-theme";
        //                    aTag.HRef = "white-label/vendors/" + verticalURL;

        //                    Label lblTag = new Label();
        //                    lblTag.ID = "lblTag_" + userVartical.Id.ToString();
        //                    lblTag.Text = "White Label " + userVartical.Description;

        //                    aTag.Controls.Clear();
        //                    aTag.Controls.Add(lblTag);

        //                    //PhTagsContent.Controls.Add(aTag);
        //                }

        //                LoadCountryVerticalTrans(userVartical, existCountry);
        //            }
        //        }
        //        else
        //        {
        //            divCategoriesSection.Visible = false;
        //            categories.Visible = false;
        //            divCategories.Visible = false;
        //        }
        //    }
        //    else
        //    {
        //        divCategoriesSection.Visible = false;
        //        categories.Visible = false;
        //        divCategories.Visible = false;
        //    }
        //}

        //private void LoadCityVerticalTrans(ElioSubIndustriesGroupItems category, bool existCountry)
        //{
        //    bool hasCity = false;
        //    string urlLink = "";
        //    string cityURL = "";
        //    bool hasTrans = false;

        //    if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
        //    {
        //        hasCity = true;
        //        cityURL = vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
        //    }

        //    if (hasCity)
        //    {
        //        if (existCountry)
        //            urlLink = vSession.ElioCompanyDetailsView.CompanyRegion.Replace(" ", "-").ToLower() + "/" + vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/channel-partners/";
        //        else
        //            urlLink = "profile/channel-partners/";

        //        string verticalURL = category.Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //        divCountryCityTransArea.Visible = true;
        //        divCityVerticalsTrans.Visible = true;

        //        if (vSession.ElioCompanyDetailsView.Country.ToLower() == "france" || vSession.ElioCompanyDetailsView.Country.ToLower() == "canada")
        //        {
        //            hasTrans = true;
        //            cityURL = "fr/" + cityURL;
        //            LblSimilarCityCompanyTitleTrans.Text = "Trouvez des entreprises similaires à " + vSession.ElioCompanyDetailsView.City;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "austria")
        //        {
        //            hasTrans = true;
        //            cityURL = "at/" + cityURL;
        //            LblSimilarCityCompanyTitleTrans.Text = "Ähnliche Unternehmen in " + vSession.ElioCompanyDetailsView.City + " finden: ";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "germany")
        //        {
        //            hasTrans = true;
        //            cityURL = "de/" + cityURL;
        //            LblSimilarCityCompanyTitleTrans.Text = "Ähnliche Unternehmen in " + vSession.ElioCompanyDetailsView.City + " finden: ";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "brazil" || vSession.ElioCompanyDetailsView.Country.ToLower() == "portugal")
        //        {
        //            hasTrans = true;
        //            cityURL = "pt/" + cityURL;
        //            LblSimilarCityCompanyTitleTrans.Text = "Encontre empresas similares em " + vSession.ElioCompanyDetailsView.City;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "italy")
        //        {
        //            hasTrans = true;
        //            cityURL = "it/" + cityURL;
        //            LblSimilarCityCompanyTitleTrans.Text = "Trova aziende simili a " + vSession.ElioCompanyDetailsView.City;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "argentina" || vSession.ElioCompanyDetailsView.Country.ToLower() == "bolivia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "chile" || vSession.ElioCompanyDetailsView.Country.ToLower() == "colombia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "costa rica" ||
        //            vSession.ElioCompanyDetailsView.Country.ToLower() == "dominican republic" || vSession.ElioCompanyDetailsView.Country.ToLower() == "ecuador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "el salvador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "guatemala" || vSession.ElioCompanyDetailsView.Country.ToLower() == "honduras" ||
        //            vSession.ElioCompanyDetailsView.Country.ToLower() == "mexico" || vSession.ElioCompanyDetailsView.Country.ToLower() == "panama" || vSession.ElioCompanyDetailsView.Country.ToLower() == "paraguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "peru" || vSession.ElioCompanyDetailsView.Country.ToLower() == "puerto rico" ||
        //             vSession.ElioCompanyDetailsView.Country.ToLower() == "uruguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "venezuela")
        //        {
        //            hasTrans = true;
        //            cityURL = "la/" + cityURL;
        //            LblSimilarCityCompanyTitleTrans.Text = "Encuentre empresas similares en " + vSession.ElioCompanyDetailsView.City;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "spain")
        //        {
        //            hasTrans = true;
        //            cityURL = "es/" + cityURL;
        //            LblSimilarCityCompanyTitleTrans.Text = "Encuentra empresas similares en " + vSession.ElioCompanyDetailsView.City;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "netherlands")
        //        {
        //            hasTrans = true;
        //            cityURL = "nl/" + cityURL;
        //            LblSimilarCityCompanyTitleTrans.Text = "Zoek vergelijkbare bedrijven in " + vSession.ElioCompanyDetailsView.City;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "poland")
        //        {
        //            hasTrans = true;
        //            cityURL = "pl/" + cityURL;
        //            LblSimilarCityCompanyTitleTrans.Text = "Znajdź podobne firmy w " + vSession.ElioCompanyDetailsView.City;
        //        }

        //        if (hasTrans)
        //        {
        //            HtmlAnchor aVerticalsCity = new HtmlAnchor();
        //            aVerticalsCity.ID = "aVerticalsCity_" + category.Id.ToString();
        //            aVerticalsCity.Attributes["class"] = "s-tags md-sortlist-theme";
        //            aVerticalsCity.HRef = cityURL + "/channel-partners/" + verticalURL;

        //            Label lblVerticalsCity = new Label();
        //            lblVerticalsCity.ID = "lblVerticalsCity_" + category.Id.ToString();
        //            lblVerticalsCity.Text = category.Description + " in " + vSession.ElioCompanyDetailsView.City;

        //            aVerticalsCity.Controls.Clear();
        //            aVerticalsCity.Controls.Add(lblVerticalsCity);

        //            PhVerticalsCityContentTrans.Controls.Add(aVerticalsCity);
        //        }
        //    }
        //}

        //private void LoadCityProductTrans(ElioRegistrationProducts category, bool existCountry)
        //{
        //    bool hasCity = false;
        //    string urlLink = "";
        //    string cityURL = "";
        //    bool hasTrans = false;
        //    if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
        //    {
        //        hasCity = true;

        //        cityURL = vSession.ElioCompanyDetailsView.City.Replace(" ", "-").ToLower();
        //    }

        //    if (hasCity)
        //    {
        //        if (existCountry)
        //            urlLink = vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/channel-partners/";
        //        else
        //            urlLink = "profile/channel-partners/";

        //        string verticalURL = category.Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //        divCityProductTrans.Visible = true;

        //        if (vSession.ElioCompanyDetailsView.Country.ToLower() == "france" || vSession.ElioCompanyDetailsView.Country.ToLower() == "canada")
        //        {
        //            hasTrans = true;
        //            cityURL = "fr/" + cityURL;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "austria")
        //        {
        //            hasTrans = true;
        //            cityURL = "at/" + cityURL;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "germany")
        //        {
        //            hasTrans = true;
        //            cityURL = "de/" + cityURL;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "brazil" || vSession.ElioCompanyDetailsView.Country.ToLower() == "portugal")
        //        {
        //            hasTrans = true;
        //            cityURL = "pt/" + cityURL;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "italy")
        //        {
        //            hasTrans = true;
        //            cityURL = "it/" + cityURL;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "argentina" || vSession.ElioCompanyDetailsView.Country.ToLower() == "bolivia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "chile" || vSession.ElioCompanyDetailsView.Country.ToLower() == "colombia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "costa rica" ||
        //            vSession.ElioCompanyDetailsView.Country.ToLower() == "dominican republic" || vSession.ElioCompanyDetailsView.Country.ToLower() == "ecuador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "el salvador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "guatemala" || vSession.ElioCompanyDetailsView.Country.ToLower() == "honduras" ||
        //            vSession.ElioCompanyDetailsView.Country.ToLower() == "mexico" || vSession.ElioCompanyDetailsView.Country.ToLower() == "panama" || vSession.ElioCompanyDetailsView.Country.ToLower() == "paraguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "peru" || vSession.ElioCompanyDetailsView.Country.ToLower() == "puerto rico" ||
        //             vSession.ElioCompanyDetailsView.Country.ToLower() == "uruguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "venezuela")
        //        {
        //            hasTrans = true;
        //            cityURL = "la/" + cityURL;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "spain")
        //        {
        //            hasTrans = true;
        //            cityURL = "es/" + cityURL;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "netherlands")
        //        {
        //            hasTrans = true;
        //            cityURL = "nl/" + cityURL;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "poland")
        //        {
        //            hasTrans = true;
        //            cityURL = "pl/" + cityURL;
        //        }

        //        if (hasTrans)
        //        {
        //            HtmlAnchor aProductsCity = new HtmlAnchor();
        //            aProductsCity.ID = "aProductsCity_" + category.Id.ToString();
        //            aProductsCity.Attributes["class"] = "s-tags md-sortlist-theme";
        //            aProductsCity.HRef = (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) ? ControlLoader.SubIndustryProfiles("vendors", cityURL + "/" + verticalURL) : cityURL + "/channel-partners/" + verticalURL;

        //            Label lblProductsCity = new Label();
        //            lblProductsCity.ID = "lblProductsCity_" + category.Id.ToString();
        //            lblProductsCity.Text = category.Description + " in " + vSession.ElioCompanyDetailsView.City;

        //            aProductsCity.Controls.Clear();
        //            aProductsCity.Controls.Add(lblProductsCity);

        //            PhProductsCityContentTrans.Controls.Add(aProductsCity);
        //        }
        //    }
        //}

        //private void LoadCountryVerticalTrans(ElioSubIndustriesGroupItems category, bool existCountry)
        //{
        //    bool hasCity = false;
        //    string urlLink = "";
        //    string cityURL = "";
        //    bool hasTrans = false;

        //    if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
        //    {
        //        hasCity = true;
        //    }

        //    if (hasCity)
        //    {
        //        if (existCountry)
        //            urlLink = vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/channel-partners/";
        //        else
        //            urlLink = "profile/channel-partners/";

        //        string verticalURL = category.Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //        divCountryVerticalsTrans.Visible = true;

        //        if (vSession.ElioCompanyDetailsView.Country.ToLower() == "france" || vSession.ElioCompanyDetailsView.Country.ToLower() == "canada")
        //        {
        //            hasTrans = true;
        //            cityURL = "fr/";
        //            LblSimilarCountryCompanyTitleTrans.Text = "Trouvez des entreprises similaires en " + vSession.ElioCompanyDetailsView.Country;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "austria")
        //        {
        //            hasTrans = true;
        //            cityURL = "at/";
        //            LblSimilarCountryCompanyTitleTrans.Text = "Ähnliche Unternehmen in Österreich finden: ";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "germany")
        //        {
        //            hasTrans = true;
        //            cityURL = "de/";
        //            LblSimilarCountryCompanyTitleTrans.Text = "Ähnliche Unternehmen in Deutschland finden: ";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "brazil" || vSession.ElioCompanyDetailsView.Country.ToLower() == "portugal")
        //        {
        //            hasTrans = true;
        //            cityURL = "pt/";
        //            LblSimilarCountryCompanyTitleTrans.Text = "Encontre empresas semelhantes em " + vSession.ElioCompanyDetailsView.Country;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "italy")
        //        {
        //            hasTrans = true;
        //            cityURL = "it/";
        //            LblSimilarCountryCompanyTitleTrans.Text = "Trova aziende simili in Italia: ";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "argentina" || vSession.ElioCompanyDetailsView.Country.ToLower() == "bolivia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "chile" || vSession.ElioCompanyDetailsView.Country.ToLower() == "colombia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "costa rica" ||
        //            vSession.ElioCompanyDetailsView.Country.ToLower() == "dominican republic" || vSession.ElioCompanyDetailsView.Country.ToLower() == "ecuador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "el salvador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "guatemala" || vSession.ElioCompanyDetailsView.Country.ToLower() == "honduras" ||
        //            vSession.ElioCompanyDetailsView.Country.ToLower() == "mexico" || vSession.ElioCompanyDetailsView.Country.ToLower() == "panama" || vSession.ElioCompanyDetailsView.Country.ToLower() == "paraguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "peru" || vSession.ElioCompanyDetailsView.Country.ToLower() == "puerto rico" ||
        //             vSession.ElioCompanyDetailsView.Country.ToLower() == "uruguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "venezuela")
        //        {
        //            hasTrans = true;
        //            cityURL = "la/";
        //            LblSimilarCountryCompanyTitleTrans.Text = "Buscar empresas similares en " + vSession.ElioCompanyDetailsView.Country;
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "spain")
        //        {
        //            hasTrans = true;
        //            cityURL = "es/";
        //            LblSimilarCountryCompanyTitleTrans.Text = "Encuentra empresas similares en España: ";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "netherlands")
        //        {
        //            hasTrans = true;
        //            cityURL = "nl/";
        //            LblSimilarCountryCompanyTitleTrans.Text = "Zoek vergelijkbare bedrijven in Nederland: ";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "poland")
        //        {
        //            hasTrans = true;
        //            cityURL = "pl/";
        //            LblSimilarCountryCompanyTitleTrans.Text = "Znajdź podobne firmy w Polsce: ";
        //        }

        //        if (hasTrans)
        //        {
        //            HtmlAnchor aVerticalsCountry = new HtmlAnchor();
        //            aVerticalsCountry.ID = "aVerticalsCountry_" + category.Id.ToString();
        //            aVerticalsCountry.Attributes["class"] = "s-tags md-sortlist-theme";
        //            aVerticalsCountry.HRef = cityURL + urlLink + verticalURL;

        //            Label lblVerticalsCountry = new Label();
        //            lblVerticalsCountry.ID = "lblVerticalsCountry_" + category.Id.ToString();
        //            lblVerticalsCountry.Text = category.Description + " in " + vSession.ElioCompanyDetailsView.Country;

        //            aVerticalsCountry.Controls.Clear();
        //            aVerticalsCountry.Controls.Add(lblVerticalsCountry);

        //            PhVerticalsCountryContentTrans.Controls.Add(aVerticalsCountry);
        //        }
        //    }
        //}

        //private void LoadCountryProductTrans(ElioRegistrationProducts category, bool existCountry)
        //{
        //    bool hasCity = false;
        //    string urlLink = "";
        //    string cityURL = "";
        //    bool hasTrans = false;

        //    if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City))
        //    {
        //        hasCity = true;
        //    }

        //    if (hasCity)
        //    {
        //        if (existCountry)
        //            urlLink = vSession.ElioCompanyDetailsView.Country.Replace(" ", "-").ToLower() + "/channel-partners/";
        //        else
        //            urlLink = "profile/channel-partners/";

        //        string verticalURL = category.Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //        divCountryProductTrans.Visible = true;

        //        if (vSession.ElioCompanyDetailsView.Country.ToLower() == "france" || vSession.ElioCompanyDetailsView.Country.ToLower() == "canada")
        //        {
        //            hasTrans = true;
        //            cityURL = "fr/";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "austria")
        //        {
        //            hasTrans = true;
        //            cityURL = "at/";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "germany")
        //        {
        //            hasTrans = true;
        //            cityURL = "de/";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "brazil" || vSession.ElioCompanyDetailsView.Country.ToLower() == "portugal")
        //        {
        //            hasTrans = true;
        //            cityURL = "pt/";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "italy")
        //        {
        //            hasTrans = true;
        //            cityURL = "it/";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "argentina" || vSession.ElioCompanyDetailsView.Country.ToLower() == "bolivia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "chile" || vSession.ElioCompanyDetailsView.Country.ToLower() == "colombia" || vSession.ElioCompanyDetailsView.Country.ToLower() == "costa rica" ||
        //            vSession.ElioCompanyDetailsView.Country.ToLower() == "dominican republic" || vSession.ElioCompanyDetailsView.Country.ToLower() == "ecuador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "el salvador" || vSession.ElioCompanyDetailsView.Country.ToLower() == "guatemala" || vSession.ElioCompanyDetailsView.Country.ToLower() == "honduras" ||
        //            vSession.ElioCompanyDetailsView.Country.ToLower() == "mexico" || vSession.ElioCompanyDetailsView.Country.ToLower() == "panama" || vSession.ElioCompanyDetailsView.Country.ToLower() == "paraguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "peru" || vSession.ElioCompanyDetailsView.Country.ToLower() == "puerto rico" ||
        //             vSession.ElioCompanyDetailsView.Country.ToLower() == "uruguay" || vSession.ElioCompanyDetailsView.Country.ToLower() == "venezuela")
        //        {
        //            hasTrans = true;
        //            cityURL = "la/";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "spain")
        //        {
        //            hasTrans = true;
        //            cityURL = "es/";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "netherlands")
        //        {
        //            hasTrans = true;
        //            cityURL = "nl/";
        //        }
        //        else if (vSession.ElioCompanyDetailsView.Country.ToLower() == "poland")
        //        {
        //            hasTrans = true;
        //            cityURL = "pl/";
        //        }

        //        if (hasTrans)
        //        {
        //            HtmlAnchor aProductsCountry = new HtmlAnchor();
        //            aProductsCountry.ID = "aProductsCountry_" + category.Id.ToString();
        //            aProductsCountry.Attributes["class"] = "s-tags md-sortlist-theme";
        //            aProductsCountry.HRef = cityURL + urlLink + verticalURL;

        //            Label lblProductsCountry = new Label();
        //            lblProductsCountry.ID = "lblProductsCountry_" + category.Id.ToString();
        //            lblProductsCountry.Text = category.Description + " in " + vSession.ElioCompanyDetailsView.Country;

        //            aProductsCountry.Controls.Clear();
        //            aProductsCountry.Controls.Add(lblProductsCountry);

        //            PhProductsCountryContentTrans.Controls.Add(aProductsCountry);
        //        }
        //    }
        //}

        //private void ResetLabels()
        //{
        //    LblAddress.Text = string.Empty;
        //    LblPhone.Text = string.Empty;
        //    LblDescription.Text = string.Empty;
        //    LblOverview.Text = string.Empty;
        //    LblCompanyName.Text = string.Empty;
        //    //LblCompanyType.Text = string.Empty;
        //}

        //private void LoadCompanyDetailsViewData()
        //{
        //    if (vSession.ElioCompanyDetailsView != null)
        //    {
        //        LblCompanyName.Text = vSession.ElioCompanyDetailsView.CompanyName;
        //        ImgCompanyLogo.ImageUrl = (vSession.ElioCompanyDetailsView.CompanyLogo != "") ? vSession.ElioCompanyDetailsView.CompanyLogo : "/Images/no_logo.jpg";
        //        ImgCompanyLogo.AlternateText = vSession.ElioCompanyDetailsView.CompanyName + " in Elioplus";

        //        if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
        //        {
        //            LblCompanyCity.Text = "Type: ";
        //            LblCompanyCityText.Text = vSession.ElioCompanyDetailsView.CompanyType;
        //        }
        //        else
        //        {
        //            LblCompanyCity.Text = "City: ";
        //            LblCompanyCityText.Text = !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.City) ? vSession.ElioCompanyDetailsView.City : "-";
        //        }

        //        if (string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Overview) || vSession.ElioCompanyDetailsView.Overview == "&nbsp;")
        //        {
        //            string[] countries = new string[] { "Afghanistan", "Australia", "Bahrain", "Bangladesh", "Bahamas", "Barbados", "Cyprus", "India", "Ireland", "Jamaica", "New Zealand", "Saudi Arabia", "Singapore", "South Africa", "United Arab Emirates", "United Kingdom", "United States" };

        //            if (countries.Contains(vSession.ElioCompanyDetailsView.Country))
        //            {
        //                LblOverview.Text = "View the solutions, services and product portfolio of " + vSession.ElioCompanyDetailsView.CompanyName;
        //            }
        //            else
        //            {
        //                LblOverview.Text = "Oups, we are sorry but there are no description data for this company.";
        //            }
        //        }
        //        else
        //        {
        //            LblOverview.Text = GlobalMethods.FixParagraphsView(vSession.ElioCompanyDetailsView.Overview);
        //        }

        //        if (string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Description) || vSession.ElioCompanyDetailsView.Description == "&nbsp;")
        //        {
        //            string[] countries = new string[] { "Afghanistan", "Australia", "Bahrain", "Bangladesh", "Bahamas", "Barbados", "Cyprus", "India", "Ireland", "Jamaica", "New Zealand", "Saudi Arabia", "Singapore", "South Africa", "United Arab Emirates", "United Kingdom", "United States" };

        //            if (countries.Contains(vSession.ElioCompanyDetailsView.Country))
        //            {
        //                LblDescription.Text = "View the solutions, services and product portfolio of " + vSession.ElioCompanyDetailsView.CompanyName;
        //            }
        //            else
        //            {
        //                LblDescription.Text = "Oups, we are sorry but there are no description data for this company.";
        //            }
        //        }
        //        else
        //        {
        //            LblDescription.Text = GlobalMethods.FixParagraphsView(vSession.ElioCompanyDetailsView.Description);
        //        }

        //        if (string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Overview) && !string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.Description))
        //            LblOverview.Text = LblDescription.Text;

        //        //RttClaimProfileInfo.Text = "This listing was automatically created using public available data from third party sources. This listing is not currently maintained by, endorsed by or affiliated with {CompanyName}".Replace("{CompanyName}", vSession.ElioCompanyDetailsView.CompanyName);
        //    }
        //}

        //private void LoadDetails()
        //{
        //    if (session.Connection.State == ConnectionState.Closed)
        //        session.OpenConnection();

        //    ResetLabels();

        //    LoadCompanyDetailsViewData();

        //    divDescriptionNotFull.Visible = false;
        //    LblDescriptionNotFull.Text = "";
        //    bool hasPhone = false;
        //    string alert = "-";

        //    if (vSession.User == null)
        //        alert = "Login to your account for full access";
        //    else
        //        if (vSession.User.AccountStatus == (int)AccountStatus.NotCompleted)
        //        alert = "Complete your registration for full access";

        //    if ((vSession.User == null) || (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)))
        //    {
        //        #region Logged In / Full Registered User

        //        divDescriptionNotFull.Visible = false;

        //        LoadData();

        //        divAddressNotFull.Visible = false;
        //        divPhoneNotFull.Visible = false;

        //        LblAddress.Text = vSession.ElioCompanyDetailsView.Address;

        //        LblPhone.Text = GlobalDBMethods.ShowUserPhone(vSession.ElioCompanyDetailsView, session, out hasPhone);

        //        #endregion
        //    }
        //    else
        //    {
        //        #region Not Logged In User / Not Full Registered User

        //        divDescriptionNotFull.Visible = true;

        //        //decimal avg = Sql.GetCompanyAverageRating(vSession.ElioCompanyDetailsView.Id, session);
        //        //int count = Sql.GetCompanyCountRatings(vSession.ElioCompanyDetailsView.Id, session);
        //        //int average = Convert.ToInt32(avg);

        //        LblAddress.Visible = false;
        //        LblPhone.Visible = false;

        //        divAddressNotFull.Visible = true;
        //        divPhoneNotFull.Visible = true;

        //        LblAddressNotFull.Text = alert;
        //        LblPhoneNotFull.Text = alert;

        //        #endregion
        //    }

        //    LoadIndustries();

        //    LoadMarkets();

        //    LoadUserVerticalsInPlaceHolder();

        //    if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
        //    {
        //        LoadProgramsForVendors();
        //        LoadVendorIntegrations();

        //        divPdfNotFull.Visible = false;

        //        if ((vSession.User == null) || (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)))
        //        {
        //            #region Logged In / Full Registered User

        //            ElioUsersFiles userPdf = Sql.GetUserPdfFile(vSession.ElioCompanyDetailsView.Id, session);

        //            if (userPdf != null)
        //            {
        //                try
        //                {
        //                    if (!userPdf.FilePath.Contains("??????"))
        //                    {
        //                        bool fileExists = File.Exists(Server.MapPath(userPdf.FilePath));
        //                        if (fileExists)
        //                        {
        //                            aPdf.Visible = true;
        //                            iPdf.Visible = true;
        //                            string[] pdfArray = userPdf.FilePath.Split('/');
        //                            string pdfName = pdfArray[4];
        //                            LblPdfValue.Text = "Partner Program brochure ";
        //                            aPdf.HRef = userPdf.FilePath;
        //                            aPdf.Target = "_blank";
        //                        }
        //                        else
        //                        {
        //                            aPdf.Visible = false;
        //                            iPdf.Visible = false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        aPdf.Visible = false;
        //                        iPdf.Visible = false;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    aPdf.Visible = false;
        //                    iPdf.Visible = false;
        //                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //                }
        //            }
        //            else
        //            {
        //                aPdf.Visible = false;
        //                iPdf.Visible = false;
        //            }

        //            #endregion
        //        }
        //        else
        //        {
        //            #region Not Logged In User / Not Full Registered User

        //            //aWebsite.Visible = false;
        //            //divWebsiteNotFull.Visible = true;
        //            //LblWebsiteNotFull.Text = alert;

        //            iPdf.Visible = true;
        //            aPdf.Visible = false;
        //            divPdfNotFull.Visible = true;

        //            string msgAlert = (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? "Complete your registration to view partner program brochure" : "Login to your account to view partner program brochure";

        //            LblPdfNotFull.Text = msgAlert;

        //            #endregion
        //        }

        //        //PnlResults.Visible = vSession.ElioCompanyDetailsView.BillingType == (int)BillingTypePacket.FreemiumPacketType;
        //    }
        //    else if (vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
        //    {
        //        LoadProgramsForResellers();
        //        LoadChannelPartnerProductsInPlaceHolder();

        //        aPdf.Visible = false;
        //        iPdf.Visible = false;

        //        if (vSession.ElioCompanyDetailsView != null && vSession.ElioCompanyDetailsView.BillingType != (int)BillingTypePacket.FreemiumPacketType)
        //        {
        //            aWebsite.Visible = true;
        //            //divWebsiteNotFull.Visible = false;
        //            aWebsite.HRef = vSession.ElioCompanyDetailsView.WebSite;
        //            //a1.HRef = vSession.ElioCompanyDetailsView.WebSite;
        //            //LblWebsiteContent.Text = "Visit Website";
        //            aWebsite.Target = "_blank";

        //            aContact.Visible = true;
        //        }
        //    }
        //    else
        //    {
        //        aPdf.Visible = false;
        //        iPdf.Visible = false;
        //    }
        //}

        //private void LoadData()
        //{
        //    if (vSession.User != null)
        //    {
        //        TbxMessageName.Text = vSession.User.CompanyName;

        //        divComEmail.Visible = ((!string.IsNullOrEmpty(vSession.User.OfficialEmail)) && vSession.User.Email != vSession.User.OfficialEmail) ? true : false;
        //        divRegEmail.Visible = (divComEmail.Visible) ? false : true;
        //        if (divComEmail.Visible)
        //        {
        //            DdlCompanyMessageEmail.Items.Clear();

        //            ListItem li = new ListItem();
        //            li.Text = vSession.User.Email;
        //            li.Value = "0";
        //            DdlCompanyMessageEmail.Items.Add(li);

        //            ListItem li2 = new ListItem();
        //            li2.Text = vSession.User.OfficialEmail;
        //            li2.Value = "1";
        //            DdlCompanyMessageEmail.Items.Add(li2);

        //            DdlCompanyMessageEmail.DataBind();
        //        }
        //        else
        //        {
        //            TbxMessageEmail.Text = vSession.User.Email;
        //        }

        //        TbxMessagePhone.Text = vSession.User.Phone;
        //    }
        //}

        //private void ClearMessageData(bool isClaimMode)
        //{
        //    if (!isClaimMode)
        //    {
        //        TbxReviewContent.Text = string.Empty;
        //        TbxMessageContent.Text = string.Empty;
        //        TbxMessageEmail.Text = string.Empty;
        //        TbxMessageName.Text = string.Empty;
        //        TbxMessagePhone.Text = string.Empty;
        //        TbxMessageSubject.Text = string.Empty;

        //        LblSuccessReviewMessage.Text = string.Empty;
        //        LblWarningReviewMessage.Text = string.Empty;
        //        divWarningReview.Visible = false;
        //        divSuccessReview.Visible = false;

        //        LblSuccessMsg.Text = string.Empty;
        //        LblWarningMsg.Text = string.Empty;
        //        divWarningMsg.Visible = false;
        //        divSuccessMsg.Visible = false;
        //    }
        //    else
        //    {
        //        TbxClaimMessageEmail.Text = string.Empty;

        //        LblClaimSuccessMsg.Text = string.Empty;
        //        LblClaimWarningMsg.Text = string.Empty;
        //        divClaimWarningMsg.Visible = false;
        //        divClaimSuccessMsg.Visible = false;
        //    }
        //}

        //private void Rate(int r)
        //{
        //    if (vSession.User != null && vSession.ElioCompanyDetailsView.Id != 0)
        //    {
        //        ElioUserPartnerProgramRating rating = new ElioUserPartnerProgramRating();

        //        rating.Rate = r;
        //        rating.CompanyId = vSession.ElioCompanyDetailsView.Id;
        //        rating.VisitorId = vSession.User.Id;
        //        rating.Sysdate = DateTime.Now;

        //        DataLoader<ElioUserPartnerProgramRating> loader = new DataLoader<ElioUserPartnerProgramRating>(session);
        //        loader.Insert(rating);

        //        //BindRatingControl();
        //    }
        //    else
        //    {
        //        Response.Redirect(ControlLoader.Search, false);
        //    }
        //}

        //protected void BindRatingControl()
        //{
        //    //int count = Sql.GetCompanyCountRatings(vSession.ElioCompanyDetailsView.Id, session);

        //    //if (count > 0)
        //    //{
        //    //    int total = Sql.GetCompanyTotalRatings(vSession.ElioCompanyDetailsView.Id, session);
        //    //    FixRatingImages(total / count);
        //    //    LblAverage.Visible = true;
        //    //    decimal avrg = Convert.ToDecimal(total) / Convert.ToDecimal(count);
        //    //    LblAverage.Text = "({average})";
        //    //    LblAverage.Text = LblAverage.Text.Replace("({average})", "(" + avrg.ToString("0.00") + ")");
        //    //    LblAverageRating.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "message", "2")).Text.Replace("{count}", count.ToString());
        //    //}
        //    //else
        //    //{
        //    //    LblAverage.Visible = false;
        //    //    FixRatingImages(0);
        //    //    LblAverageRating.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "message", "2")).Text.Replace("{count}", count.ToString());
        //    //}
        //}

        //private bool CheckData(bool isClaimMode)
        //{
        //    bool isError = false;

        //    divWarningMsg.Visible = false;
        //    divSuccessMsg.Visible = false;

        //    divClaimWarningMsg.Visible = false;
        //    divClaimSuccessMsg.Visible = false;

        //    if (!isClaimMode)
        //    {
        //        if (string.IsNullOrEmpty(TbxMessageName.Text))
        //        {
        //            LblWarningMsgContent.Text = "Please enter a name!";
        //            divWarningMsg.Visible = true;
        //            return isError = true;
        //        }

        //        if (TbxMessageEmail.Visible)
        //        {
        //            if (string.IsNullOrEmpty(TbxMessageEmail.Text))
        //            {
        //                LblWarningMsgContent.Text = "Please enter an email address!";
        //                divWarningMsg.Visible = true;
        //                return isError = true;
        //            }
        //            else
        //            {
        //                if (!Validations.IsEmail(TbxMessageEmail.Text))
        //                {
        //                    LblWarningMsgContent.Text = "Please enter a valid email address!";
        //                    divWarningMsg.Visible = true;
        //                    return isError = true;
        //                }
        //            }
        //        }

        //        if (string.IsNullOrEmpty(TbxMessageSubject.Text))
        //        {
        //            LblWarningMsgContent.Text = "Please enter a subject!";
        //            divWarningMsg.Visible = true;
        //            return isError = true;
        //        }

        //        if (string.IsNullOrEmpty(TbxMessageContent.Text))
        //        {
        //            LblWarningMsgContent.Text = "Please enter a message!";
        //            divWarningMsg.Visible = true;
        //            return isError = true;
        //        }
        //    }
        //    else
        //    {
        //        if (TbxClaimMessageEmail.Visible)
        //        {
        //            if (string.IsNullOrEmpty(TbxClaimMessageEmail.Text))
        //            {
        //                LblClaimWarningMsgContent.Text = "Please enter an email address!";
        //                divClaimWarningMsg.Visible = true;
        //                return isError = true;
        //            }
        //            else
        //            {
        //                if (!Validations.IsEmail(TbxClaimMessageEmail.Text))
        //                {
        //                    LblClaimWarningMsgContent.Text = "Please enter a valid email address!";
        //                    divClaimWarningMsg.Visible = true;
        //                    return isError = true;
        //                }
        //            }
        //        }
        //    }

        //    return isError;
        //}

        //private void ResetRFPsFields()
        //{
        //    divDemoWarningMsg.Visible = false;
        //    divDemoSuccessMsg.Visible = false;

        //    divStepOne.Visible = true;
        //    divStepTwo.Visible = false;
        //    BtnBack.Visible = false;
        //    BtnProceed.Text = "Next";

        //    TbxFirstName.Text = "";
        //    TbxCompanyEmail.Text = "";
        //    TbxLastName.Text = "";
        //    TbxBusinessName.Text = "";
        //    TbxCity.Text = "";
        //    DdlCountries.SelectedIndex = -1;
        //    TbxProduct.Text = "";
        //    TbxNumberUnits.Text = "";
        //    TbxMessage.Text = "";

        //    HdnLeadId.Value = "0";
        //}

        //private void LoadCountries()
        //{
        //    DdlCountries.Items.Clear();

        //    ListItem item = new ListItem();
        //    item.Value = "0";
        //    item.Text = "Select Country";

        //    DdlCountries.Items.Add(item);

        //    List<ElioCountries> countries = Sql.GetPublicCountries(session);
        //    foreach (ElioCountries country in countries)
        //    {
        //        item = new ListItem();
        //        item.Value = country.Id.ToString();
        //        item.Text = country.CountryName;

        //        DdlCountries.Items.Add(item);
        //    }
        //}

        //# endregion

        //# region Buttons

        //protected void aRFPsForm_ServerClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        ResetRFPsFields();
        //        LoadCountries();

        //        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenRFPsPopUp();", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void BtnProceed_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        divDemoSuccessMsg.Visible = divDemoWarningMsg.Visible = false;

        //        if (divStepOne.Visible)
        //        {
        //            if (TbxProduct.Text == "")
        //            {
        //                divDemoWarningMsg.Visible = true;
        //                LblDemoWarningMsg.Text = "Error! ";
        //                LblDemoWarningMsgContent.Text = "Please fill your product/technology";
        //                return;
        //            }

        //            if (TbxNumberUnits.Text == "")
        //            {
        //                divDemoWarningMsg.Visible = true;
        //                LblDemoWarningMsg.Text = "Error! ";
        //                LblDemoWarningMsgContent.Text = "Please fill number of users/units";
        //                return;
        //            }
        //            else
        //            {
        //                if (!Validations.IsNumber(TbxNumberUnits.Text))
        //                {
        //                    divDemoWarningMsg.Visible = true;
        //                    LblDemoWarningMsg.Text = "Error! ";
        //                    LblDemoWarningMsgContent.Text = "Please fill only numbers for users/units";
        //                    return;
        //                }
        //            }

        //            if (TbxMessage.Text == "")
        //            {
        //                divDemoWarningMsg.Visible = true;
        //                LblDemoWarningMsg.Text = "Error! ";
        //                LblDemoWarningMsgContent.Text = "Please fill a message";
        //                return;
        //            }

        //            divStepOne.Visible = false;
        //            divStepTwo.Visible = true;

        //            BtnProceed.Text = "Save";
        //            BtnBack.Visible = true;
        //        }
        //        else
        //        {
        //            if (TbxFirstName.Text == "")
        //            {
        //                divDemoWarningMsg.Visible = true;
        //                LblDemoWarningMsg.Text = "Error! ";
        //                LblDemoWarningMsgContent.Text = "Please fill your first name";
        //                return;
        //            }

        //            if (TbxLastName.Text == "")
        //            {
        //                divDemoWarningMsg.Visible = true;
        //                LblDemoWarningMsg.Text = "Error! ";
        //                LblDemoWarningMsgContent.Text = "Please fill your last name";
        //                return;
        //            }

        //            if (TbxCompanyEmail.Text == "")
        //            {
        //                divDemoWarningMsg.Visible = true;
        //                LblDemoWarningMsg.Text = "Error! ";
        //                LblDemoWarningMsgContent.Text = "Please fill your company email";
        //                return;
        //            }
        //            else
        //            {
        //                if (!Validations.IsEmail(TbxCompanyEmail.Text))
        //                {
        //                    divDemoWarningMsg.Visible = true;
        //                    LblDemoWarningMsg.Text = "Error! ";
        //                    LblDemoWarningMsgContent.Text = "Please fill a valid company email";
        //                    return;
        //                }
        //            }

        //            if (TbxBusinessName.Text == "")
        //            {
        //                divDemoWarningMsg.Visible = true;
        //                LblDemoWarningMsg.Text = "Error! ";
        //                LblDemoWarningMsgContent.Text = "Please fill your company name";
        //                return;
        //            }

        //            if (DdlCountries.SelectedValue == "0" || DdlCountries.SelectedValue == "")
        //            {
        //                divDemoWarningMsg.Visible = true;
        //                LblDemoWarningMsg.Text = "Error! ";
        //                LblDemoWarningMsgContent.Text = "Please select your country";
        //                return;
        //            }

        //            if (TbxPhoneNumber.Text == "")
        //            {
        //                divDemoWarningMsg.Visible = true;
        //                LblDemoWarningMsg.Text = "Error! ";
        //                LblDemoWarningMsgContent.Text = "Please fill your phone number";
        //                return;
        //            }

        //            if (TbxCity.Text == "")
        //            {
        //                divDemoWarningMsg.Visible = true;
        //                LblDemoWarningMsg.Text = "Error! ";
        //                LblDemoWarningMsgContent.Text = "Please fill your city";
        //                return;
        //            }

        //            ElioSnitcherWebsiteLeads lead = null;

        //            DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);

        //            if (HdnLeadId.Value == "0")
        //            {
        //                lead = new ElioSnitcherWebsiteLeads();

        //                lead.WebsiteId = "19976";
        //                lead.ElioSnitcherWebsiteId = 2;
        //                lead.SessionReferrer = "https://www.elioplus.com";
        //                lead.SessionOperatingSystem = "";
        //                lead.SessionBrowser = "";
        //                lead.SessionDeviceType = "";
        //                lead.SessionCampaign = "";
        //                lead.SessionStart = DateTime.Now;
        //                lead.SessionDuration = 0;
        //                lead.SessionTotalPageviews = 1;

        //                string number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8);
        //                int count = 1;
        //                while (Sql.GetSnitcherLeadIDByLeadId(number, session) != "" && count < 10)
        //                {
        //                    number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8);
        //                    count++;
        //                }

        //                if (count >= 10)
        //                {
        //                    throw new Exception("ERROR -> string number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8) could not created");
        //                }

        //                lead.LeadId = number;
        //                lead.LeadLastSeen = DateTime.Now;
        //                lead.LeadFirstName = TbxFirstName.Text;
        //                lead.LeadLastName = TbxLastName.Text;
        //                lead.LeadCompanyName = TbxBusinessName.Text;
        //                lead.LeadCompanyLogo = "";
        //                lead.LeadCompanyWebsite = "";
        //                lead.LeadCountry = DdlCountries.SelectedItem.Text;
        //                lead.LeadCity = TbxCity.Text;
        //                lead.LeadCompanyAddress = "";
        //                lead.LeadCompanyFounded = "0";
        //                lead.LeadCompanySize = TbxNumberUnits.Text;
        //                lead.LeadCompanyIndustry = "";
        //                lead.LeadCompanyPhone = "";
        //                lead.LeadCompanyEmail = TbxCompanyEmail.Text;
        //                lead.LeadCompanyContacts = "";
        //                lead.LeadLinkedinHandle = "";
        //                lead.LeadFacebookHandle = "";
        //                lead.LeadYoutubeHandle = "";
        //                lead.LeadInstagramHandle = "";
        //                lead.LeadTwitterHandle = "";
        //                lead.LeadPinterestHandle = "";
        //                lead.LeadAngellistHandle = "";
        //                lead.Message = TbxMessage.Text;
        //                lead.IsApiLead = (int)ApiLeadCategory.isRFQMessage;
        //                lead.IsApproved = 0;
        //                lead.IsPublic = 1;
        //                lead.IsConfirmed = 0;
        //                lead.Sysdate = DateTime.Now;
        //                lead.LastUpdate = DateTime.Now;

        //                loader.Insert(lead);

        //                HdnLeadId.Value = lead.Id.ToString();

        //                List<string> products = TbxProduct.Text.Trim().Split(',').ToList();
        //                if (products.Count > 0)
        //                {
        //                    foreach (string product in products)
        //                    {
        //                        if (product != "")
        //                        {
        //                            ElioSnitcherLeadsPageviews pageView = new ElioSnitcherLeadsPageviews();

        //                            pageView.LeadId = lead.LeadId;
        //                            pageView.ElioWebsiteLeadsId = lead.Id;
        //                            pageView.Url = product.Trim();
        //                            pageView.Product = product.Trim();
        //                            pageView.TimeSpent = 1;
        //                            pageView.ActionTime = DateTime.Now;
        //                            pageView.Sysdate = DateTime.Now;
        //                            pageView.LastUpdate = DateTime.Now;

        //                            DataLoader<ElioSnitcherLeadsPageviews> loaderView = new DataLoader<ElioSnitcherLeadsPageviews>(session);
        //                            loaderView.Insert(pageView);
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                lead = Sql.GetSnitcherWebsiteLeadById(Convert.ToInt32(HdnLeadId.Value), session);
        //                if (lead != null)
        //                {
        //                    lead.LeadFirstName = TbxFirstName.Text;
        //                    lead.LeadLastName = TbxLastName.Text;
        //                    lead.LeadCompanyName = TbxBusinessName.Text;
        //                    lead.LeadCountry = DdlCountries.SelectedItem.Text;
        //                    lead.LeadCity = TbxCity.Text;
        //                    lead.LeadCompanySize = TbxNumberUnits.Text;
        //                    lead.LeadCompanyEmail = TbxCompanyEmail.Text;
        //                    lead.Message = TbxMessage.Text;
        //                    lead.LastUpdate = DateTime.Now;
        //                    lead.IsConfirmed = 0;

        //                    loader.Update(lead);

        //                    List<string> products = TbxProduct.Text.Trim().Split(',').ToList();
        //                    if (products.Count > 0)
        //                    {
        //                        foreach (string product in products)
        //                        {
        //                            if (product != "")
        //                            {
        //                                DataLoader<ElioSnitcherLeadsPageviews> loaderView = new DataLoader<ElioSnitcherLeadsPageviews>(session);

        //                                ElioSnitcherLeadsPageviews pageView = Sql.GetSnitcherLeadPageViewByLeadIdAndUrlOrProduct(lead.LeadId, product, product, session);
        //                                if (pageView == null)
        //                                {
        //                                    pageView = new ElioSnitcherLeadsPageviews();

        //                                    pageView.LeadId = lead.LeadId;
        //                                    pageView.ElioWebsiteLeadsId = lead.Id;
        //                                    pageView.Url = product.Trim();
        //                                    pageView.Product = product.Trim();
        //                                    pageView.TimeSpent = 1;
        //                                    pageView.ActionTime = DateTime.Now;
        //                                    pageView.Sysdate = DateTime.Now;
        //                                    pageView.LastUpdate = DateTime.Now;

        //                                    loaderView.Insert(pageView);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            divDemoSuccessMsg.Visible = true;
        //            LblDemoSuccessMsg.Text = "Done! ";
        //            LblDemoSuccessMsgContent.Text = "Your request for a quote saved successfully.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

        //        divDemoWarningMsg.Visible = true;
        //        LblDemoWarningMsg.Text = "Error! ";
        //        LblDemoWarningMsgContent.Text = "Something went wrong! Please try again later.";
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void BtnBack_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        divStepOne.Visible = true;
        //        divStepTwo.Visible = false;

        //        divDemoSuccessMsg.Visible = divDemoWarningMsg.Visible = false;

        //        BtnBack.Visible = false;
        //        BtnProceed.Text = "Next";
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void BtnSave_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        if (vSession.User != null)
        //        {
        //            //if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
        //            //{
        //            //    session.BeginTransaction();

        //            //    ElioUsersFavorites favorite = new ElioUsersFavorites();
        //            //    DataLoader<ElioUsersFavorites> loader = new DataLoader<ElioUsersFavorites>(session);

        //            //    favorite.UserId = vSession.User.Id;
        //            //    favorite.CompanyId = vSession.ElioCompanyDetailsView.Id;
        //            //    favorite.SysDate = DateTime.Now;
        //            //    favorite.IsDeleted = 0;
        //            //    favorite.LastUpDated = DateTime.Now;

        //            //    loader.Insert(favorite);

        //            //    aSaveProfile.Visible = false;

        //            //    session.CommitTransaction();

        //            //    rowMsgSaveProfile.Visible = true;
        //            //    divErrorSaveProfile.Visible = false;
        //            //    divScsSaveProfile.Visible = true;
        //            //    LblScsSaveProfile.Text = "Done! ";
        //            //    LblSuccessSaveProfileContent.Text = "You have successfully saved this company's profile";
        //            //}
        //            //else
        //            //{
        //            //    rowMsgSaveProfile.Visible = true;
        //            //    divErrorSaveProfile.Visible = true;
        //            //    divScsSaveProfile.Visible = false;
        //            //    LblErrorSaveProfile.Text = "Error! ";
        //            //    LblErrorSaveProfileContent.Text = "Something wrong happened, try again or contact us.";
        //            //}
        //        }
        //        else
        //        {
        //            Response.Redirect(ControlLoader.Login, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        session.RollBackTransaction();
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void BtnSaveReview_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        if (vSession.User != null)
        //        {
        //            divSuccessReview.Visible = false;
        //            LblSuccessReviewMessage.Text = string.Empty;
        //            divWarningReview.Visible = false;
        //            LblSuccessReviewMessage.Text = string.Empty;

        //            if (string.IsNullOrEmpty(TbxReviewContent.Text))
        //            {
        //                divWarningReview.Visible = true;
        //                LblWarningReviewMessage.Text = "Please add a review!";
        //                //RdgReviews.Rebind();
        //                return;
        //            }

        //            #region Save Review

        //            ElioUserProgramReview newReview = new ElioUserProgramReview();

        //            newReview.VisitorId = vSession.User.Id;
        //            newReview.CompanyId = vSession.ElioCompanyDetailsView.Id;
        //            newReview.Review = GlobalMethods.FixStringEntryToParagraphs(TbxReviewContent.Text);
        //            newReview.SysDate = DateTime.Now;
        //            newReview.LastUpdate = DateTime.Now;
        //            newReview.IsPublic = 1;
        //            newReview.UpdateByUserId = vSession.User.Id;

        //            DataLoader<ElioUserProgramReview> loader = new DataLoader<ElioUserProgramReview>(session);
        //            loader.Insert(newReview);

        //            #endregion

        //            if (!Sql.IsUserAdministrator(vSession.User.Id, session))
        //            {
        //                //EmailNotificationsLib.SendNotificationEmailToCompanyForNewReview(vSession.User, vSession.ElioCompanyDetailsView.Email, vSession.Lang, session);
        //                EmailSenderLib.SendNotificationEmailToCompanyForNewReview(vSession.User, vSession.ElioCompanyDetailsView.Email, vSession.Lang, session);
        //            }

        //            //RdgReviews.Rebind();

        //            ClearMessageData(false);
        //            divSuccessReview.Visible = true;
        //            LblSuccessReviewMessage.Text = "Your review was saved successfully!";
        //        }
        //        else
        //        {
        //            Response.Redirect(ControlLoader.Login, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        divWarningReview.Visible = true;
        //        LblSuccessReviewMessage.Text = "Your review could not be saved. Try again!";

        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void ImgBtnCompanyLogo_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        vSession.ElioCompanyDetailsView = null;

        //        if (vSession.User != null)
        //        {
        //            ImageButton imgBtnCompanyLogo = (ImageButton)sender;
        //            GridDataItem item = (GridDataItem)imgBtnCompanyLogo.NamingContainer;

        //            if (!Sql.IsUserAdministrator(vSession.User.Id, session))
        //            {
        //                ElioUsers visitorCompany = Sql.GetUserById(Convert.ToInt32(item["visitor_id"].Text), session);
        //                if (visitorCompany != null)
        //                {
        //                    GlobalDBMethods.AddCompanyViews(vSession.User, visitorCompany, vSession.Lang, session);
        //                }
        //            }

        //            vSession.ElioCompanyDetailsView = Sql.GetUserById(Convert.ToInt32(item["visitor_id"].Text), session);
        //            if (vSession.ElioCompanyDetailsView != null)
        //            {
        //                if (vSession.User.Id != Convert.ToInt32(item["visitor_id"].Text) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
        //                {
        //                    if (!Sql.IsUserAdministrator(vSession.User.Id, session))
        //                    {
        //                        #region Send Lead email info

        //                        //EmailNotificationsLib.SendNotificationEmailToCompanyForResentLeads(Convert.ToInt32(item["visitor_id"].Text), session);
        //                        EmailSenderLib.SendNotificationEmailToCompanyForResentLeads(Convert.ToInt32(item["visitor_id"].Text), false, vSession.Lang, session);

        //                        #endregion
        //                    }
        //                }

        //                Response.Redirect(ControlLoader.Profile(vSession.ElioCompanyDetailsView), false);
        //            }
        //            else
        //            {
        //                Response.Redirect(ControlLoader.Login, false);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void ImgBtnSetNotPublic_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        if (vSession.User != null)
        //        {
        //            ImageButton imgBtnSetNotPublic = (ImageButton)sender;
        //            GridDataItem item = (GridDataItem)imgBtnSetNotPublic.NamingContainer;

        //            ElioUserProgramReview review = Sql.GetProgramsReviewById(Convert.ToInt32(item["id"].Text), session);
        //            if (review != null)
        //            {
        //                review.IsPublic = 0;
        //                review.LastUpdate = DateTime.Now;
        //                review.UpdateByUserId = vSession.User.Id;

        //                DataLoader<ElioUserProgramReview> loader = new DataLoader<ElioUserProgramReview>(session);
        //                loader.Update(review);

        //                //LblTotalReviews.Text = Sql.GetCompanyTotalReviews(review.CompanyId, session).ToString();

        //                //RdgReviews.Rebind();
        //            }
        //        }
        //        else
        //        {
        //            Response.Redirect(ControlLoader.Login, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void R1_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        Rate(1);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void R2_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        Rate(2);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void R3_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        Rate(3);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void R4_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        Rate(4);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void R5_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        Rate(5);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void BtnSend_OnClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (vSession.User != null)
        //        {
        //            session.OpenConnection();

        //            if (vSession.ElioCompanyDetailsView != null)
        //            {
        //                bool isError = CheckData(false);

        //                if (isError) return;

        //                ElioUserPacketStatus packetStatusFeatures = Sql.GetUserPacketStatusFeatures(vSession.User.Id, session);

        //                if (packetStatusFeatures != null)
        //                {
        //                    if (packetStatusFeatures.AvailableMessagesCount > 0)
        //                    {
        //                        ElioUsersMessages message = new ElioUsersMessages();

        //                        try
        //                        {
        //                            session.BeginTransaction();

        //                            message = GlobalDBMethods.InsertCompanyMessage(vSession.User.Id, TbxMessageEmail.Text, vSession.ElioCompanyDetailsView.Id, vSession.ElioCompanyDetailsView.Email, vSession.ElioCompanyDetailsView.OfficialEmail, TbxMessageSubject.Text, TbxMessageContent.Text, session);

        //                            List<string> emails = new List<string>();
        //                            emails.Add(vSession.ElioCompanyDetailsView.Email);
        //                            if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.OfficialEmail))
        //                            {
        //                                emails.Add(vSession.ElioCompanyDetailsView.OfficialEmail);
        //                            }

        //                            //EmailNotificationsLib.SendNotificationEmailToCompanyForNewInboxMessage(vSession.User, emails, TbxMessageSubject.Text, session);
        //                            EmailSenderLib.SendNotificationEmailToCompanyForNewInboxMessage(vSession.User, emails, TbxMessageSubject.Text, TbxMessageContent.Text, false, vSession.Lang, session);

        //                            GlobalDBMethods.FixUserEmailAndPacketStatusFeatutes(message, packetStatusFeatures, session);

        //                            ClearMessageData(false);

        //                            divSuccessMsg.Visible = true;
        //                            LblSuccessMsgContent.Text = "Your message was successfully sent!";

        //                            session.CommitTransaction();
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            session.RollBackTransaction();

        //                            divWarningMsg.Visible = true;
        //                            LblWarningMsgContent.Text = "Your message could not be sent to " + vSession.ElioCompanyDetailsView.CompanyName + ". Please try again later or contact us!";

        //                            Logger.DetailedError("An error occured during compose new email from company " + vSession.User.CompanyName + " to company " + vSession.ElioCompanyDetailsView.CompanyName + " at " + DateTime.Now);
        //                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //                        }
        //                    }
        //                    else
        //                    {
        //                        divWarningMsg.Visible = true;
        //                        LblWarningMsgContent.Text = "You have no messages left. They will be available again after the monthly subscription renewal!";
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                divWarningMsg.Visible = true;
        //                LblWarningMsgContent.Text = "Your message could not be sent to " + vSession.ElioCompanyDetailsView.CompanyName + ". Please try again later or contact us!";
        //            }
        //        }
        //        else
        //        {
        //            Response.Redirect(ControlLoader.Login, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ClearMessageData(false);

        //        divWarningMsg.Visible = true;
        //        LblWarningMsgContent.Text = "We are sorry but your message could not be sent because an unkown error occured. Please try again later or contact us!";

        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void BtnCancelMsg_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        ClearMessageData(false);

        //        ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseMessagePopUp();", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //protected void BtnSendClaim_OnClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        divClaimSuccessMsg.Visible = false;
        //        divClaimWarningMsg.Visible = false;

        //        if (vSession.ElioCompanyDetailsView != null)
        //        {
        //            bool isError = CheckData(true);

        //            if (isError) return;

        //            MailAddress companyMail = new MailAddress(vSession.ElioCompanyDetailsView.Email.Trim().ToLower());
        //            MailAddress inputMail = new MailAddress(TbxClaimMessageEmail.Text.Trim().ToLower());
        //            string companyHost = companyMail.Host;
        //            string inputHost = inputMail.Host;

        //            if (companyHost == inputHost)
        //            {
        //                try
        //                {
        //                    EmailSenderLib.ClaimProfileResetPasswordEmail(vSession.ElioCompanyDetailsView.Password, vSession.ElioCompanyDetailsView.Email, vSession.Lang, session);
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw ex;
        //                }
        //            }
        //            else
        //            {
        //                isError = true;
        //                divClaimSuccessMsg.Visible = false;
        //                divClaimWarningMsg.Visible = true;
        //                LblClaimWarningMsg.Text = "Warning! ";
        //                LblClaimWarningMsgContent.Text = "We 're sorry but this email does not match to this profile's email. Please contact us.";

        //                return;
        //            }

        //            if (!isError)
        //            {
        //                ClearMessageData(true);

        //                divClaimSuccessMsg.Visible = true;
        //                LblClaimSuccessMsgContent.Text = "Thank you! You have received an email with your password and account instructions. You can log in to it.";
        //            }
        //            else
        //            {
        //                ClearMessageData(true);

        //                divWarningMsg.Visible = true;
        //                LblWarningMsgContent.Text = "We are sorry but your message could not be sent because an unkown error occured. Please try again later to contact us!";
        //            }
        //        }
        //        else
        //        {
        //            Response.Redirect(ControlLoader.Search, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ClearMessageData(true);

        //        divWarningMsg.Visible = true;
        //        LblWarningMsgContent.Text = "We are sorry but your message could not be sent because an unkown error occured. Please try again later to contact us!";

        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void BtnCancelClaimMsg_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        ClearMessageData(true);

        //        ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseClaimProfilePopUp();", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //protected void BtnCancelRvw_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        ClearMessageData(false);

        //        ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseReviewPopUp();", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //protected void aAddToMyMatches_ServerClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        if (vSession.User == null)
        //        {
        //            LblConfirmationAlertMessage.Text = "Please Sign Up in order to use this feature.";
        //            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAlertPopUp();", true);
        //            return;
        //        }
        //        else
        //        {
        //            if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
        //            {
        //                if (vSession.User.CompanyType == Types.Vendors.ToString())
        //                {
        //                    #region Vendor

        //                    if (true)
        //                    {
        //                        #region Add Connection to Vendor

        //                        if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
        //                        {
        //                            #region Only Not Free User

        //                            bool isAlreadyConnection = Sql.IsConnection(vSession.User.Id, vSession.ElioCompanyDetailsView.Id, session);
        //                            if (!isAlreadyConnection)
        //                            {
        //                                ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(vSession.User.Id, session);
        //                                if (vendorPacketFeatures != null)
        //                                {
        //                                    if (vendorPacketFeatures.AvailableConnectionsCount > 0)
        //                                    {
        //                                        #region Vendor Side

        //                                        #region Get User Subscription

        //                                        ElioUsersSubscriptions sub = Sql.GetUserSubscription(vSession.User.Id, vSession.User.CustomerStripeId, session);

        //                                        #endregion

        //                                        if (sub != null)
        //                                        {
        //                                            if (sub.Status.ToLower() != "active")   //custom for now
        //                                                Logger.Debug(Request.Url.ToString(), string.Format("Profile.aspx --> MESSAGE: user {0} added connection ID:{1}, at {2}, but his subscription status is {3}", vSession.User.Id, vSession.ElioCompanyDetailsView.Id, DateTime.Now.ToString(), sub.SubscriptionId), "Connection added successfully but subscription need to be updated");

        //                                            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

        //                                            #region Add Vendor Connection

        //                                            ElioUsersConnections vendorConnection = new ElioUsersConnections();

        //                                            vendorConnection.UserId = vSession.User.Id;
        //                                            vendorConnection.ConnectionId = vSession.ElioCompanyDetailsView.Id;
        //                                            vendorConnection.SysDate = DateTime.Now;
        //                                            vendorConnection.LastUpdated = DateTime.Now;
        //                                            vendorConnection.CanBeViewed = 1;
        //                                            vendorConnection.CurrentPeriodStart = Convert.ToDateTime(sub.CurrentPeriodStart);
        //                                            vendorConnection.CurrentPeriodEnd = Convert.ToDateTime(sub.CurrentPeriodEnd);
        //                                            vendorConnection.Status = true;
        //                                            vendorConnection.IsNew = 1;

        //                                            loader.Insert(vendorConnection);

        //                                            #endregion

        //                                            #region Add Reseller Connection

        //                                            ElioUsersConnections resellerConnection = new ElioUsersConnections();

        //                                            resellerConnection.UserId = vSession.ElioCompanyDetailsView.Id;
        //                                            resellerConnection.ConnectionId = vSession.User.Id;
        //                                            resellerConnection.SysDate = DateTime.Now;
        //                                            resellerConnection.LastUpdated = DateTime.Now;
        //                                            resellerConnection.CanBeViewed = 1;
        //                                            resellerConnection.CurrentPeriodStart = vendorConnection.CurrentPeriodStart;
        //                                            resellerConnection.CurrentPeriodEnd = vendorConnection.CurrentPeriodEnd;
        //                                            resellerConnection.Status = true;
        //                                            resellerConnection.IsNew = 1;

        //                                            loader.Insert(resellerConnection);

        //                                            #endregion

        //                                            #region Update Vendor Available Connections Counter

        //                                            vendorPacketFeatures.AvailableConnectionsCount--;
        //                                            vendorPacketFeatures.LastUpdate = DateTime.Now;

        //                                            DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
        //                                            loader1.Update(vendorPacketFeatures);

        //                                            #endregion

        //                                            #region Show Success Message of Currrent Period

        //                                            aSuccess.Visible = true;
        //                                            aAddToMyMatches.Visible = false;

        //                                            LblConfirmationAlertMessage.Text = "Added to your mathces successfully";
        //                                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAlertPopUp();", true);

        //                                            //GlobalMethods.ShowMessageControl(UcMessageAlert, "Added to your mathces successfully", MessageTypes.Warning, true, true, false);

        //                                            #endregion

        //                                            //}
        //                                            //else
        //                                            //{
        //                                            //    LblConfirmationAlertMessage.Text = "You have no active subscription to add connections to your matches";
        //                                            //    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAlertPopUp();", true);

        //                                            //    //GlobalMethods.ShowMessageControl(UcMessageAlert, "User has no active subscription to add connections", MessageTypes.Warning, true, true, false);
        //                                            //}
        //                                        }
        //                                        else
        //                                        {
        //                                            LblConfirmationAlertMessage.Text = "No subscription found. Please contact with us.";
        //                                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAlertPopUp();", true);

        //                                            //GlobalMethods.ShowMessageControl(UcMessageAlert, "No subscription found for this user", MessageTypes.Warning, true, true, false);
        //                                        }

        //                                        #endregion

        //                                        #region Reseller Side

        //                                        ElioUsers reseller = Sql.GetUserById(vSession.ElioCompanyDetailsView.Id, session);
        //                                        if (reseller != null)
        //                                        {
        //                                            if (reseller.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
        //                                            {
        //                                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(reseller.Id, session);
        //                                                if (resellerPacketFeatures != null)
        //                                                {
        //                                                    if (resellerPacketFeatures.AvailableConnectionsCount > 0)
        //                                                    {
        //                                                        #region Update Reseller Available Connections Counter

        //                                                        resellerPacketFeatures.AvailableConnectionsCount--;
        //                                                        resellerPacketFeatures.LastUpdate = DateTime.Now;

        //                                                        DataLoader<ElioUserPacketStatus> loader2 = new DataLoader<ElioUserPacketStatus>(session);
        //                                                        loader2.Update(resellerPacketFeatures);

        //                                                        #endregion
        //                                                    }
        //                                                }
        //                                            }
        //                                        }

        //                                        #endregion
        //                                    }
        //                                    else
        //                                    {
        //                                        LblConfirmationAlertMessage.Text = "You have no available connections to add this profile. Please contact with us.";
        //                                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAlertPopUp();", true);

        //                                        //GlobalMethods.ShowMessageControl(UcMessageAlert, "You have no available connections to add to this user", MessageTypes.Warning, true, true, false);
        //                                        return;
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                LblConfirmationAlertMessage.Text = "This profile belongs already to your matches";
        //                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAlertPopUp();", true);
        //                                //GlobalMethods.ShowMessageControl(UcMessageAlert, "This connection belongs already to this user", MessageTypes.Warning, true, true, false);

        //                            }

        //                            #endregion
        //                        }
        //                        else
        //                        {
        //                            #region Free User

        //                            LblConfirmationAlertMessage.Text = "You must upgrade to a plan to add connections to your matches";
        //                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAlertPopUp();", true);

        //                            //GlobalMethods.ShowMessageControl(UcMessageAlert, "You are not allowed to add connection to Fremium user", MessageTypes.Warning, true, true, false);

        //                            #endregion
        //                        }

        //                        #endregion
        //                    }
        //                    else
        //                    {
        //                        #region Delete Specific Connection

        //                        #region Vendor Delete Connection

        //                        /* To DELETE
                                 
        //                        if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
        //                        {
        //                            ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(vSession.User.Id, session);
        //                            if (vendorPacketFeatures != null)
        //                            {
        //                                #region Increase Vendor Available Connections Counter

        //                                vendorPacketFeatures.AvailableConnectionsCount++;
        //                                vendorPacketFeatures.LastUpdate = DateTime.Now;

        //                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

        //                                loader.Update(vendorPacketFeatures);

        //                                #endregion
        //                            }
        //                        }

        //                        Sql.DeleteConnection(vSession.User.Id, vSession.ElioCompanyDetailsView.Id, session);

        //                        */

        //                        #endregion

        //                        #region Reseller Delete Connection

        //                        /* To DELETE
                                
        //                        ElioUsers connectionUser = Sql.GetUserById(vSession.ElioCompanyDetailsView.Id, session);

        //                        if (connectionUser != null)
        //                        {
        //                            if (connectionUser.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
        //                            {
        //                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(vSession.ElioCompanyDetailsView.Id, session);
        //                                if (resellerPacketFeatures != null)
        //                                {
        //                                    #region Increase Reseller Available Connections Counter

        //                                    resellerPacketFeatures.AvailableConnectionsCount++;
        //                                    resellerPacketFeatures.LastUpdate = DateTime.Now;

        //                                    DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

        //                                    loader.Update(resellerPacketFeatures);

        //                                    #endregion
        //                                }
        //                            }
        //                        }

        //                        Sql.DeleteConnection(vSession.ElioCompanyDetailsView.Id, vSession.User.Id, session);

        //                        */

        //                        #endregion

        //                        #endregion

        //                        #region Show Message for Delete Connection

        //                        /* To DELETE
                                
        //                        aSuccess.Visible = true;
        //                        aAddToMyMatches.Visible = false;

        //                        LblConfirmationAlertMessage.Text = "Connection deleted from your matches";
        //                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAlertPopUp();", true);

        //                        */

        //                        #endregion
        //                    }

        //                    #endregion
        //                }
        //                else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
        //                {
        //                    LblConfirmationAlertMessage.Text = "This feature is available only for Vendors type of users.";
        //                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAlertPopUp();", true);
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                Response.Redirect(vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage, false);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void aSendMessage_ServerClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenMessagePopUp();", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //protected void aAddReview_ServerClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenReviewPopUp();", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //protected void aPartnerPortalLogin_ServerClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string url = "/" + Regex.Replace(vSession.ElioCompanyDetailsView.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-free-sign-up";
        //        Response.Redirect(url, false);

        //        vSession.User = null;
        //        Session.Clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //protected void aClaimProfile_ServerClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenClaimProfilePopUp();", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //protected void BtnCloseAlertPopUp_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseAlertPopUp();", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //# endregion

        //#region Grids

        //protected void RdgResults_OnItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    try
        //    {
        //        session.OpenConnection();

        //        if (e.Item is GridDataItem)
        //        {
        //            GridDataItem item = (GridDataItem)e.Item;

        //            ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
        //            if (user != null)
        //            {
        //                item["id"].Text = user.Id.ToString();

        //                Label lblFeature = (Label)ControlFinder.FindControlRecursive(item, "LblFeature");
        //                lblFeature.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "10")).Text;

        //                if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
        //                {
        //                    HtmlGenericControl featured = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divFeatured");
        //                    featured.Visible = true;
        //                }

        //                HtmlAnchor companyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
        //                companyLogo.HRef = ControlLoader.Profile(user);

        //                Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
        //                imgCompanyLogo.ImageUrl = (user.CompanyLogo != "") ? user.CompanyLogo : "/Images/no_logo.jpg";
        //                imgCompanyLogo.AlternateText = user.CompanyName + " on Elioplus";

        //                Label lblCompanyCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyCountry");
        //                lblCompanyCountry.Text = user.Country;

        //                Label lblCompany = (Label)ControlFinder.FindControlRecursive(item, "LblCompany");
        //                lblCompany.Text = user.CompanyName;

        //                HtmlAnchor companyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
        //                companyName.HRef = ControlLoader.Profile(user);

        //                bool hasCompanyData = false;
        //                ElioUsersPersonCompanies company = null;

        //                if (user.UserApplicationType == (int)UserApplicationType.ThirdParty)    // && userIndustries.Count == 0)
        //                {
        //                    hasCompanyData = ClearbitSql.ExistsClearbitCompany(user.Id, session);
        //                    if (hasCompanyData)
        //                        company = ClearbitSql.GetPersonCompanyByUserId(user.Id, session);
        //                }

        //                if (hasCompanyData && company != null)
        //                {
        //                    if (string.IsNullOrEmpty(user.CompanyLogo))
        //                        if (!string.IsNullOrEmpty(company.Logo))
        //                            imgCompanyLogo.ImageUrl = company.Logo;
        //                }

        //                Label lblAddressValue = (Label)ControlFinder.FindControlRecursive(item, "LblAddressValue");
        //                lblAddressValue.Text = (user.Address != "&nbsp;" && user.Address != "") ? user.Address : "-";

        //                Label lblOverview = (Label)ControlFinder.FindControlRecursive(item, "LblOverview");
        //                if (user.Overview.Length >= 250)
        //                {
        //                    lblOverview.Text = GlobalMethods.FixParagraphsView(user.Overview.Substring(0, 250) + "...");
        //                }
        //                else
        //                {
        //                    lblOverview.Text = GlobalMethods.FixParagraphsView(user.Overview);

        //                    if (string.IsNullOrEmpty(user.Overview))
        //                    {
        //                        if (hasCompanyData && company != null)
        //                        {
        //                            if (!string.IsNullOrEmpty(company.Description))
        //                            {
        //                                if (company.Description.Length >= 250)
        //                                {
        //                                    lblOverview.Text = GlobalMethods.FixParagraphsView(company.Description.Substring(0, 250) + "...");
        //                                }
        //                                else
        //                                {
        //                                    lblOverview.Text = GlobalMethods.FixParagraphsView(company.Description);
        //                                }
        //                            }
        //                            else
        //                                lblOverview.Text = "Oups, we are sorry but there are no description data for this company.";
        //                        }
        //                        else
        //                            lblOverview.Text = "Oups, we are sorry but there are no description data for this company.";
        //                    }
        //                    else
        //                    {
        //                        if (user.Overview.Length < 35)
        //                        {
        //                            user.Overview = (user.Overview.EndsWith(".")) ? user.Overview : user.Overview + ". ";
        //                            lblOverview.Text = user.Overview + Environment.NewLine + "Check our profile for more details.";
        //                        }
        //                    }
        //                }

        //                if (lblOverview.Text != "")
        //                {
        //                    lblOverview.Text = lblOverview.Text.Replace("<br/><br/><br/><br/>", "").Replace("<br/><br/><br/>", "").Replace("<br/><br/>", "").Replace("<br/>", "");
        //                    lblOverview.Text = lblOverview.Text.Replace("<br><br><br><br>", "").Replace("<br><br><br>", "").Replace("<br><br>", "").Replace("<br>", "");
        //                }

        //                HtmlAnchor viewProfile = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aViewProfile");
        //                viewProfile.HRef = ControlLoader.Profile(user);

        //                HtmlGenericControl divOr = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divOr");
        //                HtmlAnchor aPartnerPortalLogin = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartnerPortalLogin");

        //                Label lblVerticals = (Label)ControlFinder.FindControlRecursive(item, "LblVerticals");
        //                Label lblPartnerPrograms = (Label)ControlFinder.FindControlRecursive(item, "LblPartnerPrograms");

        //                if (user.CompanyType == Types.Vendors.ToString())
        //                {
        //                    #region Vendors

        //                    aPartnerPortalLogin.Visible = divOr.Visible = true;       //(vSession.User == null || (vSession.User != null && vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))) ? true : false;

        //                    if (aPartnerPortalLogin.Visible)
        //                    {
        //                        if (vSession.User != null)
        //                        {
        //                            if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
        //                                aPartnerPortalLogin.HRef = "/" + Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-login";
        //                            else
        //                                aPartnerPortalLogin.HRef = "/" + Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-free-sign-up";
        //                        }
        //                        else
        //                            aPartnerPortalLogin.HRef = "/" + Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-free-sign-up";
        //                    }

        //                    lblVerticals.Text = "Categories &";
        //                    lblPartnerPrograms.Text = "Technologies";

        //                    List<ElioSubIndustriesGroupItems> userVarticals = Sql.GetUserSubIndustriesGroupItems(user.Id, session);

        //                    if (userVarticals.Count > 0)
        //                    {
        //                        int count = 0;
        //                        if (count < userVarticals.Count)
        //                        {
        //                            HtmlAnchor aVert1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert1");
        //                            Label LblVert1 = (Label)ControlFinder.FindControlRecursive(item, "LblVert1");
        //                            aVert1.Visible = true;
        //                            aVert1.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

        //                            LblVert1.Text = userVarticals[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userVarticals.Count)
        //                        {
        //                            HtmlAnchor aVert2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert2");
        //                            Label LblVert2 = (Label)ControlFinder.FindControlRecursive(item, "LblVert2");
        //                            aVert2.Visible = true;
        //                            aVert2.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

        //                            LblVert2.Text = userVarticals[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userVarticals.Count)
        //                        {
        //                            HtmlAnchor aVert3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert3");
        //                            Label LblVert3 = (Label)ControlFinder.FindControlRecursive(item, "LblVert3");
        //                            aVert3.Visible = true;
        //                            aVert3.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

        //                            LblVert3.Text = userVarticals[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userVarticals.Count)
        //                        {
        //                            HtmlAnchor aVert4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert4");
        //                            Label LblVert4 = (Label)ControlFinder.FindControlRecursive(item, "LblVert4");
        //                            aVert4.Visible = true;
        //                            aVert4.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

        //                            LblVert4.Text = userVarticals[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userVarticals.Count)
        //                        {
        //                            HtmlAnchor aVert5 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert5");
        //                            Label LblVert5 = (Label)ControlFinder.FindControlRecursive(item, "LblVert5");
        //                            aVert5.Visible = true;
        //                            aVert5.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

        //                            LblVert5.Text = userVarticals[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userVarticals.Count)
        //                        {
        //                            HtmlAnchor aVert6 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert6");
        //                            Label LblVert6 = (Label)ControlFinder.FindControlRecursive(item, "LblVert6");
        //                            aVert6.Visible = true;
        //                            aVert6.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

        //                            LblVert6.Text = userVarticals[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userVarticals.Count)
        //                        {
        //                            HtmlAnchor aVert7 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert7");
        //                            Label LblVert7 = (Label)ControlFinder.FindControlRecursive(item, "LblVert7");
        //                            aVert7.Visible = true;
        //                            aVert7.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

        //                            LblVert7.Text = userVarticals[count].Description;
        //                            count++;
        //                        }
        //                    }

        //                    List<ElioPartners> userPartners = Sql.GetUsersPartners(user.Id, session);
        //                    if (userPartners.Count > 0)
        //                    {
        //                        int count = 0;

        //                        if (count < userPartners.Count)
        //                        {
        //                            HtmlAnchor aPartPr1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr1");
        //                            Label LblPartPr1 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr1");
        //                            aPartPr1.Visible = true;
        //                            aPartPr1.HRef = GetProgram(userPartners[count].PartnerDescription);

        //                            LblPartPr1.Text = userPartners[count].PartnerDescription;
        //                            count++;
        //                        }

        //                        if (count < userPartners.Count)
        //                        {
        //                            HtmlAnchor aPartPr2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr2");
        //                            Label LblPartPr2 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr2");
        //                            aPartPr2.Visible = true;
        //                            aPartPr2.HRef = GetProgram(userPartners[count].PartnerDescription);

        //                            LblPartPr2.Text = userPartners[count].PartnerDescription;
        //                            count++;
        //                        }

        //                        if (count < userPartners.Count)
        //                        {
        //                            HtmlAnchor aPartPr3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr3");
        //                            Label LblPartPr3 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr3");
        //                            aPartPr3.Visible = true;
        //                            aPartPr3.HRef = GetProgram(userPartners[count].PartnerDescription);

        //                            LblPartPr3.Text = userPartners[count].PartnerDescription;
        //                            count++;
        //                        }

        //                        if (count < userPartners.Count)
        //                        {
        //                            HtmlAnchor aPartPr4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr4");
        //                            Label LblPartPr4 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr4");
        //                            aPartPr4.Visible = true;
        //                            aPartPr4.HRef = GetProgram(userPartners[count].PartnerDescription);

        //                            LblPartPr4.Text = userPartners[count].PartnerDescription;
        //                            count++;
        //                        }

        //                        if (count < userPartners.Count)
        //                        {
        //                            HtmlAnchor aPartPr5 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr5");
        //                            Label LblPartPr5 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr5");
        //                            aPartPr5.Visible = true;
        //                            aPartPr5.HRef = GetProgram(userPartners[count].PartnerDescription);

        //                            LblPartPr5.Text = userPartners[count].PartnerDescription;
        //                            count++;
        //                        }

        //                        if (count < userPartners.Count)
        //                        {
        //                            HtmlAnchor aPartPr6 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr6");
        //                            Label LblPartPr6 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr6");
        //                            aPartPr6.Visible = true;
        //                            aPartPr6.HRef = GetProgram(userPartners[count].PartnerDescription);

        //                            LblPartPr6.Text = userPartners[count].PartnerDescription;
        //                            count++;
        //                        }

        //                        if (count < userPartners.Count)
        //                        {
        //                            HtmlAnchor aPartPr7 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr7");
        //                            Label LblPartPr7 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr7");
        //                            aPartPr7.Visible = true;
        //                            aPartPr7.HRef = GetProgram(userPartners[count].PartnerDescription);

        //                            LblPartPr7.Text = userPartners[count].PartnerDescription;
        //                            count++;
        //                        }
        //                    }

        //                    #endregion
        //                }
        //                else
        //                {
        //                    #region Channel Partners

        //                    aPartnerPortalLogin.Visible = divOr.Visible = false;     //(vSession.User == null || (vSession.User != null && vSession.User.CompanyType == Types.Vendors.ToString())) ? true : false;

        //                    lblVerticals.Text = "Product &";
        //                    lblPartnerPrograms.Text = "Technologies";

        //                    bool existCountry = false;
        //                    List<string> defaultCountries = "Afghanistan,Albania,Algeria,Angola,Armenia,Azerbaijan,Argentina,Australia,Austria,Bahamas,Bahrain,Bangladesh,Barbados,Belarus,Benin,Bermuda,Bolivia,Bosnia and Herzegovina,Botswana,Bulgaria,Belgium,Brazil,Cambodia,Cameroon,Cape Verde,Chad,China, People s Republic of,China, Republic of (Taiwan),Congo,Costa Rica,Cote d Ivoire (Ivory Coast),Croatia,Cyprus,Canada,Chile,Colombia,Czech Republic,Denmark,Dominican Republic,Ecuador,Egypt,El Salvador,Estonia,Ethiopia,Fiji,Finland,France,Gabon,Gambia,Georgia,Ghana,Greece,Guatemala,Germany,Honduras,Hungary,Hong Kong,Iceland,Iran,Iraq,India,Indonesia,Ireland,Israel,Italy,Jamaica,Jordan,Japan,Kazakhstan,Kenya,Korea, South,Kuwait,Kyrgyzstan,Laos,Latvia,Lebanon,Liberia,Libya,Liechtenstein,Lithuania,Luxembourg,Macedonia,Madagascar,Malawi,Maldives,Mali,Malta,Mauritania,Monaco,Mongolia,Montenegro,Morocco,Mozambique,Myanmar (Burma),Malaysia,Mexico,Namibia,Nepal,Nicaragua,Nigeria,Netherlands,New Zealand,Norway,Oman,Pakistan,Panama,Papua New Guinea,Paraguay,Peru,Philippines,Puerto Rico,Poland,Portugal,Qatar,Romania,Rwanda,Russia,San Marino,Saudi Arabia,Senegal,Serbia,Sierra Leone,Slovakia,Slovenia,Somalia,Sri Lanka,Sudan,Suriname,Syria,Singapore,South Africa,Spain,Sweden,Switzerland,Tajikistan,Tanzania,Togo,Trinidad and Tobago,Tunisia,Turkmenistan,Thailand,Turkey,United Arab Emirates,United Kingdom,United States,Uganda,Ukraine,Uruguay,Venezuela,Vietnam,Yemen',Zambia,Zimbabwe".Split(',').ToList();
        //                    if (defaultCountries.Count > 0)
        //                    {
        //                        foreach (string country in defaultCountries)
        //                        {
        //                            if (country != "" && country.Contains(user.Country))
        //                            {
        //                                existCountry = true;
        //                                break;
        //                            }
        //                        }
        //                    }

        //                    if (existCountry)
        //                    {
        //                        string urlLink = user.Country.Replace(" ", "-").ToLower() + "/channel-partners/";

        //                        List<ElioSubIndustriesGroupItems> userVarticals = Sql.GetUserSubIndustriesGroupItems(user.Id, session);

        //                        if (userVarticals.Count > 0)
        //                        {
        //                            int count = 0;
        //                            if (count < userVarticals.Count)
        //                            {
        //                                HtmlAnchor aVert1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert1");
        //                                Label LblVert1 = (Label)ControlFinder.FindControlRecursive(item, "LblVert1");
        //                                aVert1.Visible = true;
        //                                aVert1.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                                LblVert1.Text = userVarticals[count].Description;
        //                                count++;
        //                            }

        //                            if (count < userVarticals.Count)
        //                            {
        //                                HtmlAnchor aVert2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert2");
        //                                Label LblVert2 = (Label)ControlFinder.FindControlRecursive(item, "LblVert2");
        //                                aVert2.Visible = true;
        //                                aVert2.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                                LblVert2.Text = userVarticals[count].Description;
        //                                count++;
        //                            }

        //                            if (count < userVarticals.Count)
        //                            {
        //                                HtmlAnchor aVert3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert3");
        //                                Label LblVert3 = (Label)ControlFinder.FindControlRecursive(item, "LblVert3");
        //                                aVert3.Visible = true;
        //                                aVert3.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                                LblVert3.Text = userVarticals[count].Description;
        //                                count++;
        //                            }

        //                            if (count < userVarticals.Count)
        //                            {
        //                                HtmlAnchor aVert4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert4");
        //                                Label LblVert4 = (Label)ControlFinder.FindControlRecursive(item, "LblVert4");
        //                                aVert4.Visible = true;
        //                                aVert4.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                                LblVert4.Text = userVarticals[count].Description;
        //                                count++;
        //                            }

        //                            if (count < userVarticals.Count)
        //                            {
        //                                HtmlAnchor aVert5 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert5");
        //                                Label LblVert5 = (Label)ControlFinder.FindControlRecursive(item, "LblVert5");
        //                                aVert5.Visible = true;
        //                                aVert5.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                                LblVert5.Text = userVarticals[count].Description;
        //                                count++;
        //                            }

        //                            if (count < userVarticals.Count)
        //                            {
        //                                HtmlAnchor aVert6 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert6");
        //                                Label LblVert6 = (Label)ControlFinder.FindControlRecursive(item, "LblVert6");
        //                                aVert6.Visible = true;
        //                                aVert6.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                                LblVert6.Text = userVarticals[count].Description;
        //                                count++;
        //                            }

        //                            if (count < userVarticals.Count)
        //                            {
        //                                HtmlAnchor aVert7 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert7");
        //                                Label LblVert7 = (Label)ControlFinder.FindControlRecursive(item, "LblVert7");
        //                                aVert7.Visible = true;
        //                                aVert7.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                                LblVert7.Text = userVarticals[count].Description;
        //                                count++;
        //                            }
        //                        }
        //                    }

        //                    string productUrl = "profile/channel-partners/";
        //                    List<ElioRegistrationProducts> userProducts = Sql.GetRegistrationProductsDescriptionByUserId(user.Id, session);

        //                    if (userProducts.Count > 0)
        //                    {
        //                        int count = 0;

        //                        if (count < userProducts.Count)
        //                        {
        //                            HtmlAnchor aPartPr1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr1");
        //                            Label LblPartPr1 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr1");
        //                            aPartPr1.Visible = true;
        //                            aPartPr1.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                            LblPartPr1.Text = userProducts[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userProducts.Count)
        //                        {
        //                            HtmlAnchor aPartPr2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr2");
        //                            Label LblPartPr2 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr2");
        //                            aPartPr2.Visible = true;
        //                            aPartPr2.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                            LblPartPr2.Text = userProducts[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userProducts.Count)
        //                        {
        //                            HtmlAnchor aPartPr3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr3");
        //                            Label LblPartPr3 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr3");
        //                            aPartPr3.Visible = true;
        //                            aPartPr3.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                            LblPartPr3.Text = userProducts[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userProducts.Count)
        //                        {
        //                            HtmlAnchor aPartPr4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr4");
        //                            Label LblPartPr4 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr4");
        //                            aPartPr4.Visible = true;
        //                            aPartPr4.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                            LblPartPr4.Text = userProducts[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userProducts.Count)
        //                        {
        //                            HtmlAnchor aPartPr5 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr5");
        //                            Label LblPartPr5 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr5");
        //                            aPartPr5.Visible = true;
        //                            aPartPr5.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                            LblPartPr5.Text = userProducts[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userProducts.Count)
        //                        {
        //                            HtmlAnchor aPartPr6 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr6");
        //                            Label LblPartPr6 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr6");
        //                            aPartPr6.Visible = true;
        //                            aPartPr6.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                            LblPartPr6.Text = userProducts[count].Description;
        //                            count++;
        //                        }

        //                        if (count < userProducts.Count)
        //                        {
        //                            HtmlAnchor aPartPr7 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr7");
        //                            Label LblPartPr7 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr7");
        //                            aPartPr7.Visible = true;
        //                            aPartPr7.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").ToLower();

        //                            LblPartPr7.Text = userProducts[count].Description;
        //                            count++;
        //                        }

        //                        if (userProducts.Count > 7)
        //                        {
        //                            HtmlAnchor aPartPrMore = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPrMore");
        //                            Label lblPartPrMore = (Label)ControlFinder.FindControlRecursive(item, "LblPartPrMore");
        //                            aPartPrMore.Visible = true;
        //                            aPartPrMore.HRef = "";

        //                            lblPartPrMore.Text = "more";

        //                            HtmlGenericControl iMoreProducts = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "iMoreProducts");
        //                            iMoreProducts.Attributes["title"] = GlobalMethods.FillRadToolTipWithRegistrationProductsDescriptions(userProducts);
        //                            iMoreProducts.Visible = true;
        //                        }
        //                    }

        //                    #endregion
        //                }

        //                HtmlAnchor aITLearnMore = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aITLearnMore");
        //                if (aITLearnMore != null)
        //                    aITLearnMore.HRef = ControlLoader.IntentSignals;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        session.CloseConnection();
        //    }
        //}

        //protected void RdgResults_OnNeedDataSource(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        divWarningMsg.Visible = false;
        //        divSuccessMsg.Visible = false;

        //        session.OpenConnection();

        //        DataTable resultsTbl = null;    // Sql.GetUsersWithSpecificSubIndustriesGroupItemsByCompanyTypeDataTable(vSession.ElioCompanyDetailsView.Id, vSession.ElioCompanyDetailsView.CompanyType, session);

        //        if (resultsTbl != null && resultsTbl.Rows.Count > 0)
        //        {
        //            //RdgResults.Visible = true;
        //            //PnlResults.Visible = true;

        //            //RdgResults.DataSource = resultsTbl;
        //        }
        //        else
        //        {
        //            //RdgResults.Visible = false;
        //            //PnlResults.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //#endregion
    }
}