using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Objects;
using Telerik.Web.UI;
using System.Data;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.IO;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI;

namespace WdS.ElioPlus
{
    public partial class PersonProfile : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
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

                vSession.ElioCompanyDetailsView = user;

                HtmlGenericControl header = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "header");
                if (header != null)
                {
                    header.Attributes["class"] = "header headbg-img navbar-fixed-top";
                    header.Attributes["style"] = "display:none;";
                }

                HtmlGenericControl footer = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "footer");
                if (footer != null)
                    footer.Attributes["style"] = "display:none;";

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

        # region Methods

        private void LoadPersonCompanyData()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            string[] pathElements = path.Split('/');

            int number;
            int userId = (int.TryParse(pathElements[2], out number)) ? number : 0;

            ElioUsers user = Sql.GetUserById(userId, session);

            if (user != null)
            {
                bool hasPersonData = ClearbitSql.ExistsClearbitPerson(user.Id, session);
                bool hasCompanyData = ClearbitSql.ExistsClearbitCompany(user.Id, session);

                if ((!hasPersonData || !hasCompanyData) && user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString() && user.AccountStatus == (int)AccountStatus.NotCompleted)
                {
                    bool success = ClearBit.FindCombinedPersonCompanyByEmail_v2(user, user.Email, session);
                    if (success)
                        LoadPersonCompanyData();                    
                }

                #region Elioplus User Details

                ImgPersonLogo.ImageUrl = (!string.IsNullOrEmpty(user.PersonalImage) && File.Exists(user.PersonalImage)) ? user.PersonalImage : "~/Images/icons/personal_img_2.png";
                LblCompanyName.Text = user.CompanyName;
                LblPhoneContent.Text = (user.Phone.Length <= 4) ? "-" : user.Phone;
                LblPersonTitleTitle.Text = (!string.IsNullOrEmpty(user.Position)) ? user.Position : "-";
                LblEmailContent.Text = user.Email;
                int webSiteLength = "http://".Length;
                aWebsiteContent.HRef = LblWebsiteContent.Text = (!string.IsNullOrEmpty(user.WebSite)) ? (user.WebSite.StartsWith("http://")) ? "https://" + user.WebSite.Substring(webSiteLength, user.WebSite.Length - webSiteLength) : user.WebSite : "-";
                aWebsiteContent.Target = "_blank";
                LblAddressContent.Text = (!string.IsNullOrEmpty(user.Address)) ? user.Address : "-";
                LblCountryContent.Text = user.Country;
                LblTimeZoneContent.Text = "-";
                LblGivenNameContent.Text = (!string.IsNullOrEmpty(user.FirstName)) ? user.FirstName : "-";
                LblFamilyNameContent.Text = (!string.IsNullOrEmpty(user.LastName)) ? user.LastName : "-";
                if (string.IsNullOrEmpty(user.FirstName) && string.IsNullOrEmpty(user.LastName))
                {
                    LblFamilyNameContent.Text = "Personal ";
                    LblGivenNameContent.Text = "Details";
                }

                LblBioContent.Text = (!string.IsNullOrEmpty(user.Description)) ? user.Description : (!string.IsNullOrEmpty(user.Overview)) ? user.Overview : "-";
                LblSeniorityContent.Text = "-";

                aTwitterHandleContent.Visible = false;

                if (!string.IsNullOrEmpty(user.LinkedInUrl))
                {
                    aLinkedinHandleContent.Visible = true;
                    aLinkedinHandleContent.HRef = (!user.LinkedInUrl.StartsWith("https://www.linkedin.com/")) ? "https://www.linkedin.com/" + user.LinkedInUrl : user.LinkedInUrl;
                }
                else
                    aLinkedinHandleContent.Visible = false;

                aAboutMeHandleContent.Visible = false;

                aFacebookHandleContent.Visible = false;
                aGoogleMailHandleContent.Visible = false;
                LblCompanyTypeContent.Text = "Channel Partners";   //user.CompanyType;
                LblPositionContent.Text = (!string.IsNullOrEmpty(user.Position)) ? user.Position : "-";
                ImgCompanyLogo.ImageUrl = (!string.IsNullOrEmpty(user.CompanyLogo) && File.Exists(user.CompanyLogo)) ? user.CompanyLogo : "~/Images/no_logo1.png";
                aCompanyDomainContent.HRef = LblCompanyDomainContent.Text = (!string.IsNullOrEmpty(user.WebSite)) ? (!user.WebSite.StartsWith("https://") && !user.WebSite.StartsWith("www.") && !user.WebSite.StartsWith("https://www.") && !user.WebSite.StartsWith("http://") && !user.WebSite.StartsWith("http://www.")) ? "https://www." + user.WebSite : user.WebSite : "-";
                aCompanyDomainContent.Target = "_blank";
                LblSectorContent.Text = "-";
                LblIndustryGroupContent.Text = "-";

                List<ElioIndustries> userIdustries = Sql.GetUsersIndustries(user.Id, session);
                if (userIdustries.Count > 0)
                {
                    foreach (ElioIndustries industry in userIdustries)
                    {
                        LblIndustryContent.Text += industry.IndustryDescription + ", ";
                    }
                    LblIndustryContent.Text = (userIdustries.Count > 0) ? LblIndustryContent.Text.Substring(0, LblIndustryContent.Text.Length - 2) : "-";
                }
                else
                    LblIndustryContent.Text = "-";

                List<ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers> profileSubcategories = Sql.GetUserSubcategoriesById(user.Id, session);
                if (profileSubcategories.Count > 0)
                {
                    foreach (ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers subcategory in profileSubcategories)
                    {
                        LblSubIndustryContent.Text += subcategory.DescriptionSubcategory + ", ";
                    }
                    LblSubIndustryContent.Text = (profileSubcategories.Count > 0) ? LblSubIndustryContent.Text.Substring(0, LblSubIndustryContent.Text.Length - 2) : "-";
                }
                else
                    LblSubIndustryContent.Text = "-";

                LblCompanyDescriptionContent.Text = (!string.IsNullOrEmpty(user.Description)) ? user.Description : "-";
                LblCompanyFoundedYearContent.Text = "-";
                LblCompanyLocationContent.Text = (!string.IsNullOrEmpty(user.Address)) ? user.Address : "-";
                LblCompanyFundAmountContent.Text = "-";
                LblCompanyEmployeesNumberContent.Text = "-";
                LblCompanyEmployeesRangeContent.Text = "-";
                LblCompanyAnnualRevenueContent.Text = "-";
                LblCompanyAnnualRangeContent.Text = "-";

                aCompanyGoogleMailHandleContent.Visible = false;
                aCompanyFacebookHandleContent.Visible = false;
                aCompanyTwitterSiteContent.Visible = false;
                aCompanyAboutMeHandleContent.Visible = false;

                if (!string.IsNullOrEmpty(user.LinkedInUrl))
                {
                    aCompanyLinkedinHandleContent.Visible = true;
                    aCompanyLinkedinHandleContent.HRef = (!user.LinkedInUrl.StartsWith("https://www.linkedin.com/")) ? "https://www.linkedin.com/" + user.LinkedInUrl : user.LinkedInUrl;
                }
                else
                    aCompanyLinkedinHandleContent.Visible = false;

                #endregion

                #region Clearbit Person/Company Details

                //ElioUsersPersonIJPersonCompanies personCompany = Sql.GetPersonCompaniesByUserId(user.Id, session);
                ElioUsersPerson person = ClearbitSql.GetPersonByUserId(user.Id, session);

                if (person != null)
                {
                    #region Person Details

                    //LblRegistrationDateContent.Text = person.DateInserted.ToString();
                    //LblElioplusDashboard.Text = user.CompanyName;
                    ImgPersonLogo.ImageUrl = (!string.IsNullOrEmpty(user.PersonalImage) && File.Exists(user.PersonalImage)) ? user.PersonalImage : (!string.IsNullOrEmpty(person.Avatar)) ? person.Avatar : "~/Images/icons/personal_img_2.png";

                    LblCompanyName.Text = user.CompanyName;
                    LblPhoneContent.Text = (user.Phone.Length <= 4) ? "-" : user.Phone;
                    //LblPersonTitle.Text = "Person title";
                    LblPersonTitleTitle.Text = person.Title;
                    LblEmailContent.Text = user.Email;
                    webSiteLength = "http://".Length;
                    aWebsiteContent.HRef = LblWebsiteContent.Text = (!string.IsNullOrEmpty(user.WebSite)) ? (user.WebSite.StartsWith("http://")) ? "https://" + user.WebSite.Substring(webSiteLength, user.WebSite.Length - webSiteLength) : user.WebSite : "-";
                    aWebsiteContent.Target = "_blank";
                    LblAddressContent.Text = (!string.IsNullOrEmpty(user.Address)) ? user.Address : person.Location;
                    LblCountryContent.Text = user.Country;
                    //LblAccountStatusContent.Text = (user.AccountStatus == (int)AccountStatus.Completed) ? "Completed" : "Not Completed";
                    LblTimeZoneContent.Text = (!string.IsNullOrEmpty(person.TimeZone)) ? person.TimeZone : "-";
                    LblGivenNameContent.Text = (!string.IsNullOrEmpty(user.FirstName)) ? user.FirstName : person.GivenName;
                    LblFamilyNameContent.Text = (!string.IsNullOrEmpty(user.LastName)) ? user.LastName : person.FamilyName;
                    LblBioContent.Text = person.Bio;
                    LblSeniorityContent.Text = person.Seniority;

                    if (!string.IsNullOrEmpty(person.TwitterHandle))
                    {
                        aTwitterHandleContent.Visible = true;
                        aTwitterHandleContent.HRef = (!person.TwitterHandle.StartsWith("https://www.twitter.com/")) ? "https://www.twitter.com/" + person.TwitterHandle : person.TwitterHandle;
                    }
                    else
                        aTwitterHandleContent.Visible = false;

                    if (!string.IsNullOrEmpty(person.LinkedinHandle))
                    {
                        aLinkedinHandleContent.Visible = true;
                        aLinkedinHandleContent.HRef = (!person.LinkedinHandle.StartsWith("https://www.linkedin.com/")) ? "https://www.linkedin.com/" + person.LinkedinHandle : person.LinkedinHandle;
                    }
                    else
                        aLinkedinHandleContent.Visible = false;

                    if (!string.IsNullOrEmpty(person.AboutMeHandle))
                    {
                        aAboutMeHandleContent.Visible = true;
                        aAboutMeHandleContent.HRef = (!person.AboutMeHandle.StartsWith("https://about.me/")) ? "https://about.me/" + person.AboutMeHandle : person.AboutMeHandle;
                    }
                    else
                        aAboutMeHandleContent.Visible = false;

                    aFacebookHandleContent.Visible = false;
                    aGoogleMailHandleContent.Visible = false;
                    //LblOverviewContent.Text = user.Overview;
                    //LblDescriptionContent.Text = user.Description;
                    LblCompanyTypeContent.Text = "Channel Partners";   //user.CompanyType;
                    LblPositionContent.Text = (!string.IsNullOrEmpty(user.Position)) ? user.Position : "-";
                    //LblCommunitySummaryContent.Text = user.CommunitySummaryText;

                    #endregion

                    ElioUsersPersonCompanies company = ClearbitSql.GetPersonCompanyByPersonIdUserId(person.Id, user.Id, session);
                    if (company != null)
                    {
                        #region Company Details

                        ImgCompanyLogo.ImageUrl = (!string.IsNullOrEmpty(company.Logo)) ? company.Logo : "~/Images/no_logo1.png";
                        if (string.IsNullOrEmpty(ImgPersonLogo.ImageUrl) && !string.IsNullOrEmpty(company.Logo))
                            ImgPersonLogo.ImageUrl = company.Logo;

                        aCompanyDomainContent.HRef = LblCompanyDomainContent.Text = (!string.IsNullOrEmpty(company.Domain)) ? (!company.Domain.StartsWith("https://") && !company.Domain.StartsWith("www.") && !company.Domain.StartsWith("https://www.") && !company.Domain.StartsWith("http://") && !company.Domain.StartsWith("http://www.")) ? "https://www." + company.Domain : company.Domain : "-";
                        aCompanyDomainContent.Target = "_blank";
                        LblSectorContent.Text = company.Sector;
                        LblIndustryGroupContent.Text = company.IndustryGroup;
                        LblIndustryContent.Text = company.Industry;
                        LblSubIndustryContent.Text = company.SubIndustry;

                        LblCompanyDescriptionContent.Text = company.Description;
                        LblCompanyFoundedYearContent.Text = (!string.IsNullOrEmpty(company.FoundedYear.ToString()) && (int)company.FoundedYear != 0) ? company.FoundedYear.ToString() : "-";
                        LblCompanyLocationContent.Text = (!string.IsNullOrEmpty(company.Location)) ? company.Location : "-";
                        LblCompanyFundAmountContent.Text = (!string.IsNullOrEmpty(company.FundAmount.ToString()) && (int)company.FundAmount != 0) ? company.FundAmount.ToString() : "-";
                        LblCompanyEmployeesNumberContent.Text = (!string.IsNullOrEmpty(company.EmployeesNumber.ToString())) ? company.EmployeesNumber.ToString() : "-";
                        LblCompanyEmployeesRangeContent.Text = (!string.IsNullOrEmpty(company.EmployeesRange.ToString())) ? company.EmployeesRange.ToString() : "-";
                        LblCompanyAnnualRevenueContent.Text = (!string.IsNullOrEmpty(company.AnnualRevenue.ToString()) && (int)company.AnnualRevenue != 0) ? company.AnnualRevenue.ToString() : "-";
                        LblCompanyAnnualRangeContent.Text = company.AnnualRevenueRange.ToString();

                        //LblCompanyFacebookLikesContent.Text = (!string.IsNullOrEmpty(company.FacebookLikes.ToString())) ? company.FacebookLikes.ToString() : "-";
                        //LblCompanyTwitterBioContent.Text = (!string.IsNullOrEmpty(company.TwitterBio.ToString())) ? company.TwitterBio.ToString() : "-";
                        //LblCompanyTwitterFollowersContent.Text = (!string.IsNullOrEmpty(company.TwitterFollowers.ToString())) ? company.TwitterFollowers.ToString() : "-";
                        //LblCompanyTwitterFollowingContent.Text = (!string.IsNullOrEmpty(company.TwitterFollowing.ToString())) ? company.TwitterFollowing.ToString() : "-";
                        //LblCompanyTwitterLocationContent.Text = (!string.IsNullOrEmpty(company.TwitterLocation.ToString())) ? company.TwitterLocation : "-";

                        //ImgCompanyTwitterAvatar.ImageUrl = company.TwitterAvatar;

                        //aCompanyLinkedinHandleContent.Visible = false;
                        aCompanyGoogleMailHandleContent.Visible = false;
                        if (!string.IsNullOrEmpty(company.FacebookHandle))
                        {
                            aCompanyFacebookHandleContent.Visible = true;
                            aCompanyFacebookHandleContent.HRef = (!company.FacebookHandle.StartsWith("https://www.facebook.com/")) ? "https://www.facebook.com/" + company.FacebookHandle : company.FacebookHandle;
                        }
                        else
                            aCompanyFacebookHandleContent.Visible = false;

                        //if (!string.IsNullOrEmpty(company.TwitterSite))
                        //{
                        //    aCompanyTwitterSiteContent.Visible = true;
                        //    aCompanyTwitterSiteContent.HRef = company.TwitterSite;
                        //}
                        //else
                        //{
                        //    aCompanyTwitterSiteContent.Visible = false;
                        //}

                        if (!string.IsNullOrEmpty(company.TwitterHandle))
                        {
                            aCompanyTwitterSiteContent.Visible = true;
                            aCompanyTwitterSiteContent.HRef = (!company.TwitterHandle.StartsWith("https://www.twitter.com/")) ? "https://www.twitter.com/" + company.TwitterHandle : company.TwitterHandle;
                        }
                        else
                            aCompanyTwitterSiteContent.Visible = false;

                        if (!string.IsNullOrEmpty(company.CrunchbaseHandle))
                        {
                            aCompanyAboutMeHandleContent.Visible = true;
                            aCompanyAboutMeHandleContent.HRef = (!company.CrunchbaseHandle.StartsWith("https://www.crunchbase.com/")) ? "https://www.crunchbase.com/" + company.CrunchbaseHandle : company.CrunchbaseHandle;
                        }
                        else
                            aCompanyAboutMeHandleContent.Visible = false;

                        if (!string.IsNullOrEmpty(company.LinkedinHandle))
                        {
                            aCompanyLinkedinHandleContent.Visible = true;
                            aCompanyLinkedinHandleContent.HRef = (!company.LinkedinHandle.StartsWith("https://www.linkedin.com/")) ? "https://www.linkedin.com/" + company.LinkedinHandle : company.LinkedinHandle;
                        }
                        else
                            aCompanyLinkedinHandleContent.Visible = false;


                        string phones = "";

                        List<ElioUsersPersonCompanyPhoneNumbers> companyPhones = ClearbitSql.GetPersonCompanyPhones(company.Id, user.Id, session);

                        if (companyPhones.Count > 0)
                        {
                            int phoneIndex = 1;
                            foreach (ElioUsersPersonCompanyPhoneNumbers phone in companyPhones)
                            {
                                if (phoneIndex < companyPhones.Count)
                                    phones += phone.PhoneNumber + ", <br/>";
                                else
                                    phones += phone.PhoneNumber;

                                phoneIndex++;
                            }

                            //phones = phones.Substring(0, phones.Length - 2);
                        }
                        else
                            phones = "-";

                        LblCompanyPhonesContent.Text = phones;
                        //string industries = "";

                        string tagnames = ClearbitSql.GetPersonCompanyTagsAsString(company.Id, session);
                        if (tagnames != "")
                            LblCompanyTagsContent.Text = tagnames;
                        else
                            LblCompanyTagsContent.Text = "-";

                        //List<ElioUsersPersonCompanyTags> tags = ClearbitSql.GetPersonCompanyTags(company.Id, session);

                        //if (tags.Count > 0)
                        //{
                        //    int TagIndex = 1;
                        //    foreach (ElioUsersPersonCompanyTags tag in tags)
                        //    {
                        //        if (TagIndex < tags.Count)
                        //            industries += tag.TagName + ", ";

                        //        else
                        //            industries += tag.TagName;

                        //        TagIndex++;
                        //    }

                        //    //industries = industries.Substring(0, industries.Length - 2);
                        //}
                        //else
                        //    industries = "-";

                        //LblCompanyTagsContent.Text = industries;

                        #endregion
                    }
                }

                #endregion

                #region Add Company Views

                if (vSession.User != null)
                {
                    try
                    {
                        GlobalDBMethods.AddCompanyViews(vSession.User, user, vSession.Lang, session);
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                #endregion
            }
            else
                Response.Redirect(ControlLoader.PageDash404, false);
        }

        private void UpdateStrings()
        {
            ImgCompanyLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "literal", "1")).Text;
            
            LblSaveProfile.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "2")).Text;
            LblSendMessage.Text = "Send message";
            
            LblMessageHeader.Text = "Send Message";
            
            BtnMessageCancel.Text = "Cancel";
            
            BtnAddReview.Text = "Submit Review";
            BtnCancelReview.Text = "Cancel";
            LblWarningReview.Text = "Error! ";
            LblSuccessReview.Text = "Done! ";
            LblWarningMsg.Text = "Error! ";
            LblSuccessMsg.Text = "Done! ";
            
            BtnCloseMessage.Text = "X";
            LblMessageGoFull.Text = "Create a full profile for free!";
            LblGoFullTitle.Text = "Complete your registration in order to have access to this feature! ";
            //LblGoFullContent.Text = "You have to be full registered in order to use any action buttons in this page or see all the details of this company profile. Click on the 'Sign Up Now' button and go through a free, simple and fast registration!";
            BtnCancelMessage.Text = "Close";
        }

        private void FixPage()
        {
            if (!IsPostBack)
                UpdateStrings();

            LoadPersonCompanyData();
        } 

        private void LoadData()
        {
            if (vSession.User != null)
            {
                TbxMessageName.Text = vSession.User.CompanyName;

                divComEmail.Visible = ((!string.IsNullOrEmpty(vSession.User.OfficialEmail)) && vSession.User.Email != vSession.User.OfficialEmail) ? true : false;
                divRegEmail.Visible = (divComEmail.Visible) ? false : true;
                if (divComEmail.Visible)
                {
                    DdlCompanyMessageEmail.Items.Clear();

                    ListItem li = new ListItem();
                    li.Text = vSession.User.Email;
                    li.Value = "0";
                    DdlCompanyMessageEmail.Items.Add(li);

                    ListItem li2 = new ListItem();
                    li2.Text = vSession.User.OfficialEmail;
                    li2.Value = "1";
                    DdlCompanyMessageEmail.Items.Add(li2);

                    DdlCompanyMessageEmail.DataBind();
                }
                else
                {
                    TbxMessageEmail.Text = vSession.User.Email;
                }

                TbxMessagePhone.Text = vSession.User.Phone;
            }
        }

        private void ClearMessageData()
        {
            TbxReviewContent.Text = string.Empty;
            TbxMessageContent.Text = string.Empty;
            TbxMessageEmail.Text = string.Empty;
            TbxMessageName.Text = string.Empty;
            TbxMessagePhone.Text = string.Empty;
            TbxMessageSubject.Text = string.Empty;

            LblSuccessReviewMessage.Text = string.Empty;
            LblWarningReviewMessage.Text = string.Empty;
            divWarningReview.Visible = false;
            divSuccessReview.Visible = false;            
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

                BindRatingControl();
            }
            else
            {
                Response.Redirect(ControlLoader.Search, false);
            }            
        }

        protected void BindRatingControl()
        {            
            int count = Sql.GetCompanyCountRatings(vSession.ElioCompanyDetailsView.Id, session);

            if (count > 0)
            {
                int total = Sql.GetCompanyTotalRatings(vSession.ElioCompanyDetailsView.Id, session);
                
            }
            else
            {

            }
        }
                
        private bool CheckData()
        {
            bool isError = false;

            divWarningMsg.Visible = false;
            divSuccessMsg.Visible = false;

            if (string.IsNullOrEmpty(TbxMessageName.Text))
            {
                LblWarningMsgContent.Text = "Please enter a name!";
                divWarningMsg.Visible = true;
                return isError = true;
            }

            if (TbxMessageEmail.Visible)
            {
                if (string.IsNullOrEmpty(TbxMessageEmail.Text))
                {
                    LblWarningMsgContent.Text = "Please enter an email address!";
                    divWarningMsg.Visible = true;
                    return isError = true;
                }
                else
                {
                    if (!Validations.IsEmail(TbxMessageEmail.Text))
                    {
                        LblWarningMsgContent.Text = "Please enter a valid email address!";
                        divWarningMsg.Visible = true;
                        return isError = true;
                    }
                }
            }

            //if (string.IsNullOrEmpty(TbxMessagePhone.Text))
            //{
            //    LblWarningMsgContent.Text = "Please enter your phone number!";
            //    divWarningMsg.Visible = true;
            //    return isError = true;
            //}

            if (string.IsNullOrEmpty(TbxMessageSubject.Text))
            {
                LblWarningMsgContent.Text = "Please enter a subject!";
                divWarningMsg.Visible = true;
                return isError = true;
            }

            if (string.IsNullOrEmpty(TbxMessageContent.Text))
            {
                LblWarningMsgContent.Text = "Please enter a message!";
                divWarningMsg.Visible = true;
                return isError = true;
            }
            
            return isError;
        }

        # endregion

        # region Buttons

        protected void ViewProductDemo_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.ElioCompanyDetailsView != null)
                {
                    if (vSession.User == null || (vSession.User != null && vSession.User.Id != vSession.ElioCompanyDetailsView.Id))
                    {
                        ElioUsersProductDemoViews demoViews = Sql.GetUserProductDemoViews(vSession.ElioCompanyDetailsView.Id, session);
                        DataLoader<ElioUsersProductDemoViews> loader = new DataLoader<ElioUsersProductDemoViews>(session);

                        if (demoViews == null)
                        {
                            demoViews = new ElioUsersProductDemoViews();

                            demoViews.UserId = vSession.ElioCompanyDetailsView.Id;

                            if (vSession.User != null)
                                demoViews.VisitorCompanyId = vSession.User.Id;

                            demoViews.Sysdate = DateTime.Now;
                            demoViews.LastUpdated = DateTime.Now;
                            demoViews.Count++;
                            
                            loader.Insert(demoViews);
                        }
                        else
                        {
                            demoViews.UserId = vSession.ElioCompanyDetailsView.Id;

                            if (vSession.User != null)
                                demoViews.VisitorCompanyId = vSession.User.Id;

                            demoViews.Sysdate = DateTime.Now;
                            demoViews.LastUpdated = DateTime.Now;
                            demoViews.Count++;

                            loader.Update(demoViews);
                        }

                        Response.Redirect(aViewProductDemo.HRef, false);
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

        protected void BtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                    {
                        session.BeginTransaction();

                        ElioUsersFavorites favorite = new ElioUsersFavorites();
                        DataLoader<ElioUsersFavorites> loader = new DataLoader<ElioUsersFavorites>(session);

                        favorite.UserId = vSession.User.Id;
                        favorite.CompanyId = vSession.ElioCompanyDetailsView.Id;
                        favorite.SysDate = DateTime.Now;
                        favorite.IsDeleted = 0;
                        favorite.LastUpDated = DateTime.Now;

                        loader.Insert(favorite);

                        //aSaveProfile.Visible = false;

                        session.CommitTransaction();

                    }
                    else
                    {
                        
                    }
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

        protected void BtnSaveReview_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    divSuccessReview.Visible = false;
                    LblSuccessReviewMessage.Text = string.Empty;
                    divWarningReview.Visible = false;
                    LblSuccessReviewMessage.Text = string.Empty;

                    if (string.IsNullOrEmpty(TbxReviewContent.Text))
                    {
                        divWarningReview.Visible = true;
                        LblWarningReviewMessage.Text = "Please add a review!";
                        
                        return;
                    }

                    #region Save Review

                    ElioUserProgramReview newReview = new ElioUserProgramReview();

                    newReview.VisitorId = vSession.User.Id;
                    newReview.CompanyId = vSession.ElioCompanyDetailsView.Id;
                    newReview.Review = GlobalMethods.FixStringEntryToParagraphs(TbxReviewContent.Text);
                    newReview.SysDate = DateTime.Now;
                    newReview.LastUpdate = DateTime.Now;
                    newReview.IsPublic = 1;
                    newReview.UpdateByUserId = vSession.User.Id;

                    DataLoader<ElioUserProgramReview> loader = new DataLoader<ElioUserProgramReview>(session);
                    loader.Insert(newReview);

                    #endregion

                    if (!Sql.IsUserAdministrator(vSession.User.Id, session))
                    {
                        //EmailNotificationsLib.SendNotificationEmailToCompanyForNewReview(vSession.User, vSession.ElioCompanyDetailsView.Email, vSession.Lang, session);
                        EmailSenderLib.SendNotificationEmailToCompanyForNewReview(vSession.User, vSession.ElioCompanyDetailsView.Email, vSession.Lang, session);
                    }

                    
                    ClearMessageData();
                    divSuccessReview.Visible = true;
                    LblSuccessReviewMessage.Text = "Your review was saved successfully!";
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
                }
            }
            catch (Exception ex)
            {
                divWarningReview.Visible = true;
                LblSuccessReviewMessage.Text = "Your review could not be saved. Try again!";

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

        protected void BtnSend_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (vSession.ElioCompanyDetailsView != null)
                    {
                        bool isError = CheckData();

                        if (isError) return;

                        ElioUserPacketStatus packetStatusFeatures = Sql.GetUserPacketStatusFeatures(vSession.User.Id, session);

                        if (packetStatusFeatures != null)
                        {
                            if (packetStatusFeatures.AvailableMessagesCount > 0)
                            {
                                ElioUsersMessages message = new ElioUsersMessages();

                                try
                                {
                                    session.BeginTransaction();

                                    message = GlobalDBMethods.InsertCompanyMessage(vSession.User.Id, TbxMessageEmail.Text, vSession.ElioCompanyDetailsView.Id, vSession.ElioCompanyDetailsView.Email, vSession.ElioCompanyDetailsView.OfficialEmail, TbxMessageSubject.Text, TbxMessageContent.Text, session);

                                    List<string> emails = new List<string>();
                                    emails.Add(vSession.ElioCompanyDetailsView.Email);
                                    if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.OfficialEmail))
                                    {
                                        emails.Add(vSession.ElioCompanyDetailsView.OfficialEmail);
                                    }

                                    //EmailNotificationsLib.SendNotificationEmailToCompanyForNewInboxMessage(vSession.User, emails, TbxMessageSubject.Text, session);
                                    EmailSenderLib.SendNotificationEmailToCompanyForNewInboxMessage(vSession.User, emails, TbxMessageSubject.Text, TbxMessageContent.Text, false, vSession.Lang, session);

                                    GlobalDBMethods.FixUserEmailAndPacketStatusFeatutes(message, packetStatusFeatures, session);

                                    ClearMessageData();

                                    divSuccessMsg.Visible = true;
                                    LblSuccessMsgContent.Text = "Your message was successfully sent!";

                                    session.CommitTransaction();
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();

                                    divWarningMsg.Visible = true;
                                    LblWarningMsgContent.Text = "Your message could not be sent to " + vSession.ElioCompanyDetailsView.CompanyName + ". Please try again later or contact us!";

                                    Logger.DetailedError("An error occured during compose new email from company " + vSession.User.CompanyName + " to company " + vSession.ElioCompanyDetailsView.CompanyName + " at " + DateTime.Now);
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }
                            else
                            {
                                divWarningMsg.Visible = true;
                                LblWarningMsgContent.Text = "You have no messages left. They will be available again after the monthly subscription renewal!";
                            }
                        }
                    }
                    else
                    {
                        divWarningMsg.Visible = true;
                        LblWarningMsgContent.Text = "Your message could not be sent to " + vSession.ElioCompanyDetailsView.CompanyName + ". Please try again later or contact us!";
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
                }
            }
            catch (Exception ex)
            {
                ClearMessageData();

                divWarningMsg.Visible = true;
                LblWarningMsgContent.Text = "We are sorry but your message could not be sent because an unkown error occured. Please try again later or contact us!";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnCancelMsg_OnClick(object sender, EventArgs args)
        {
            try
            {
                LblWarningMsgContent.Text = string.Empty;
                divWarningMsg.Visible = false;

                LblSuccessMsgContent.Text = string.Empty;
                divSuccessMsg.Visible = false;

                TbxMessageSubject.Text = string.Empty;
                TbxMessageContent.Text = string.Empty;
                
                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseMessagePopUp();", true);
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
                LblWarningReviewMessage.Text = string.Empty;
                divWarningReview.Visible = false;

                LblSuccessReviewMessage.Text = string.Empty;
                divSuccessReview.Visible = false;

                TbxReviewContent.Text = string.Empty;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseReviewPopUp();", true);                
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion

        #region Grids

        protected void RdgReviews_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ElioUserProgramReviewIJUsers review = Sql.GetUserReviewById(Convert.ToInt32(item["id"].Text), session);
                    if (review != null)
                    {
                        ImageButton imgBtnCompanyLogo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCompanyLogo");
                        imgBtnCompanyLogo.ImageUrl = (review.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? "/images/no_logo.jpg" : review.ComapnyLogo;

                        Label lbldate = (Label)ControlFinder.FindControlRecursive(item, "Lbldate");
                        lbldate.Text = review.SysDate.ToString("F");

                        Label lblReview = (Label)ControlFinder.FindControlRecursive(item, "LblReview");
                        lblReview.Text = GlobalMethods.FixParagraphsView(review.Review);

                        ImageButton imgBtnSetNotPublic = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSetNotPublic");
                        imgBtnSetNotPublic.Visible = (vSession.User != null) ? Sql.IsUserAdministrator(vSession.User.Id, session) : false;
                        imgBtnSetNotPublic.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "9")).Text;

                        RadToolTip rdttpCompanyLogo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RdttpCompanyLogo");
                        rdttpCompanyLogo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "8")).Text;

                        Label lblDateText = (Label)ControlFinder.FindControlRecursive(item, "LblDateText");
                        lblDateText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "label", "3")).Text;

                        Label lblCompanyReview = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyReview");
                        lblCompanyReview.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "label", "4")).Text;
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

        protected void RdgReviews_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.ElioCompanyDetailsView != null)
                {
                    List<ElioUserProgramReview> reviews = Sql.GetUserIdAllPublicReviews(vSession.ElioCompanyDetailsView.Id, session);

                    if (reviews.Count > 0)
                    {
                        

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("visitor_id");
                        foreach (ElioUserProgramReview review in reviews)
                        {
                            table.Rows.Add(review.Id, review.VisitorId);
                        }

                       
                    }
                    else
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

        #endregion

        protected void BtnClosePage_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
    }
}