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

namespace WdS.ElioPlus
{
    public partial class FullRegistration : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        SetLinks();
                        UpdateStrings();
                        PageTitle();

                        GlobalMethods.ClearCriteriaSession(vSession, false);
                    }

                    if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
                    {
                        ControlLoader.LoadElioControls(Page, PhFullRegistration, ControlLoader.FullRegistration);
                    }
                    else
                    {
                        //Response.Redirect(ControlLoader.Default, false);
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
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
                aElioplusLogo.HRef = aFooterHome.HRef = ControlLoader.Default();
                aFooterHowItWorks.HRef = ControlLoader.HowItWorks;
                aFooterPricing.HRef = ControlLoader.Pricing;
                aFooterSearch.HRef = ControlLoader.Search;
                aFooterContact.HRef = ControlLoader.ContactUs;               
                aFooterBlog.HRef = "https://medium.com/@elioplus.com";
                aFooterFacebook.HRef = "http://www.facebook.com/elioplus";
                aFooterTwitter.HRef = "http://www.twitter.com/elioplus";
                aFooterLinkedin.HRef = "https://www.linkedin.com/company/elio";
                aFooterGoogle.HRef = "https://plus.google.com/u/0/b/108177376326631142433/108177376326631142433/posts";
                aFooterFaq.HRef = ControlLoader.Faq;
                aFooterInfoEmail.HRef = "mailto:info@elioplus.com";
                aFooterAbout.HRef = aFooterCopyright.HRef = ControlLoader.About;
                aFooterCommunity.HRef = ControlLoader.CommunityPosts;
                aFooterTerms.HRef = ControlLoader.Terms;
                aFooterPrivacy.HRef = ControlLoader.Privacy;
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
                ImgElioplusLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "signup", "alternate", "1")).Text;
                ImgElioplusLogo.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "login", "tooltip", "1")).Text;
                //LtrPrivacyTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "10")).Text;
                //RwndPrivacy.Title = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "10")).Text;
                //BtnClosePrivacy.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "12")).Text;
                //RwndTerms.Title = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "label", "9")).Text;
                //BtnCloseTerms.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "label", "10")).Text;
                //LtrTermsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "literal", "1")).Text;                
                LblFooterHome.Text = "Home";
                LblFooterHowItWorks.Text = "How it works";
                LblFooterPricing.Text = "Pricing";
                LblFooterSearch.Text = "Search";
                LblFooterContact.Text = "Contact";
                LblFooterBlog.Text = "Blog";
                LblTermsPrivacy.Text = "Terms & Conditions";
                LblTerms.Text = "Terms & Conditions";
                LblPrivacy.Text = "Privacy Statement";
                LblFooterFaq.Text = "Faq";
                LblSitemap.Text = "Sitemap";
                LblContactInfo.Text = "Contact Information";
                LblFooterAbout.Text = "About";
                LtrInfoEmailText.Text = "Contact us at: ";
                LtrInfoEmail.Text = "info@elioplus.com";
                LtrAddressGR.Text = "Address: 33 Saronikou St , 163 45, Ilioupoli, Athens, Greece";
                //LtrTelGR.Text = "Tel: +30 2177367850";
                LtrAddressUS.Text = "Address: 108 West 13th Street, Suite 105 Wilmington, Delaware 19801";
                LtrCopyright.Text = "Copyright Elioplus @ 2020. Designed by ";
                LtrElioplusTeam.Text = "Elioplus Team";
                LblFooterCommunity.Text = "Community";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
    }
}