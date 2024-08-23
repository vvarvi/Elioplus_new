using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using System.Text.RegularExpressions;

namespace WdS.ElioPlus
{
    public partial class FullRegistrationPartner : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        private string VendorName
        {
            get
            {
                object value = ViewState["VendorName"];
                return value != null ? value.ToString() : "";
            }
            set
            {
                ViewState["VendorName"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        #region Get Company Profile Data from Url

                        ElioUsers vendor = GlobalDBMethods.GetCompanyFromAbsoluteUrl(HttpContext.Current.Request.Url.AbsolutePath, session);
                        if (vendor != null)
                        {
                            ImgElioplusLogo.ImageUrl = vendor.CompanyLogo;
                            aElioplusLogo.HRef = ControlLoader.Profile(vendor);
                            ImgElioplusLogo.ToolTip = vendor.CompanyName + " profile";
                            ImgElioplusLogo.AlternateText = vendor.CompanyName + " logo";
                            //LblSignUpTitle.Text = "Sign up now through " + vendor.CompanyName;
                            VendorName = Regex.Replace(vendor.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                        }
                        else
                            Response.Redirect(ControlLoader.SignUp, false);

                        #endregion

                        SetLinks();
                        UpdateStrings();
                        PageTitle();

                        GlobalMethods.ClearCriteriaSession(vSession, false);
                    }

                    if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
                    {
                        ControlLoader.LoadElioControls(Page, PhFullRegistration, ControlLoader.FullRegistrationPartnerPrm);
                    }
                    else
                    {
                        //Response.Redirect(ControlLoader.Default, false);
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                    }
                }
                else
                {
                    string LoginUrl = (!string.IsNullOrEmpty(VendorName)) ? ControlLoader.LoginPartner.Replace("{CompanyName}", VendorName) : ControlLoader.Login;

                    Response.Redirect(LoginUrl, false);
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

        private void PageTitle()
        {
            string metaDescription = "";
            string metaKeywords = "";

            PgTitle.InnerText = GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.ElioCompanyDetailsView, out metaDescription, out metaKeywords, session);

            //MetaDescription = metaDescription;
            //MetaKeywords = metaKeywords;
        }

        private void SetLinks()
        {
            try
            {
                //aElioplusLogo.HRef = aFooterHome.HRef = ControlLoader.Default;
                //aFooterHowItWorks.HRef = ControlLoader.HowItWorks;
                //aFooterPricing.HRef = ControlLoader.Pricing;
                //aFooterSearch.HRef = ControlLoader.Search;
                //aFooterContact.HRef = ControlLoader.ContactUs;               
                //aFooterBlog.HRef = "https://medium.com/@elioplus.com";
                //aFooterFacebook.HRef = "http://www.facebook.com/elioplus";
                //aFooterTwitter.HRef = "http://www.twitter.com/elioplus";
                //aFooterLinkedin.HRef = "https://www.linkedin.com/company/elio";
                //aFooterGoogle.HRef = "https://plus.google.com/u/0/b/108177376326631142433/108177376326631142433/posts";
                //aFooterFaq.HRef = ControlLoader.Faq;
                //aFooterInfoEmail.HRef = "mailto:info@elioplus.com";
                //aFooterAbout.HRef = aFooterCopyright.HRef = ControlLoader.About;
                //aFooterCommunity.HRef = ControlLoader.CommunityPosts;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void UpdateStrings()
        {
            try
            {
                //ImgElioplusLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "signup", "alternate", "1")).Text;
                //ImgElioplusLogo.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "login", "tooltip", "1")).Text;
                //LtrPrivacyTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "10")).Text;
                //RwndPrivacy.Title = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "10")).Text;
                //BtnClosePrivacy.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "12")).Text;
                //RwndTerms.Title = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "label", "9")).Text;
                //BtnCloseTerms.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "label", "10")).Text;
                //LtrTermsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "literal", "1")).Text;                
                //LblFooterHome.Text = "Home";
                //LblFooterHowItWorks.Text = "How it works";
                //LblFooterPricing.Text = "Pricing";
                //LblFooterSearch.Text = "Search";
                //LblFooterContact.Text = "Contact";
                //LblFooterBlog.Text = "Blog";
                //LblTermsPrivacy.Text = "Terms & Conditions";
                //LblTerms.Text = "Terms & Conditions";
                //LblPrivacy.Text = "Privacy Statement";
                //LblFooterFaq.Text = "Faq";
                //LblSitemap.Text = "Sitemap";
                //LblContactInfo.Text = "Contact Information";
                //LblFooterAbout.Text = "About";
                //LtrInfoEmailText.Text = "Contact us at: ";
                //LtrInfoEmail.Text = "info@elioplus.com";
                //LtrAddressGR.Text = "Address: 33 Saronikou St , 163 45, Ilioupoli, Athens, Greece";
                ////LtrTelGR.Text = "Tel: +30 2177367850";
                //LtrAddressUS.Text = "Address: 3511 Silverside Road, Suite 105, Wilmington, DE 19810";
                //LtrCopyright.Text = "Copyright Elioplus @ 2020. Designed by ";
                //LtrElioplusTeam.Text = "Elioplus Team";
                //LblFooterCommunity.Text = "Community";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
    }
}