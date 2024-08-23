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

namespace WdS.ElioPlus
{
    public partial class ProfilePartnerPrograms : System.Web.UI.Page
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
                header.Attributes["class"] = "header headbg-img navbar-fixed-top";

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

        # region Methods

        private void UpdateStrings()
        {
            ImgCompanyLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "literal", "1")).Text;
            LblAddReview.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "1")).Text;
            LblSaveProfile.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "2")).Text;
            LblSendMessage.Text = "Send message";
            LblOverviewTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "3")).Text;
            LblDescriptionTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "4")).Text;
            //LblProgramReviewsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "5")).Text;
            LblCompanyInfoTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "6")).Text;
            LblIndustryTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "7")).Text;
            LblSubcategoriesTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "8")).Text;
            LblMarketsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "9")).Text;
            LblProgramsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "10")).Text;
            LblApiTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "11")).Text;
            LblWebsiteTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "12")).Text;
            LblAddressTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "13")).Text;
            LblPhoneTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "14")).Text;
            LblRegDateTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "15")).Text;
            LblTotalReviewsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "18")).Text;
            LblMashapeTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "19")).Text;
            LblViewProductDemo.Text = LblViewProductDemoNotFull.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "profile", "label", "20")).Text;

            LblInfo.Text = "Info! ";
            LblWarning.Text = "Warning! ";
            LblSuccess.Text = "Done! ";
            LblMessageHeader.Text = "Send Message";
            BtnMessageSend.Text = "Send it";
            BtnMessageCancel.Text = "Cancel";
            BtnCloseModal.Text = BtnCloseReview.Text = "X";
            LblReviewHeader.Text = "Add Review";
            BtnAddReview.Text = "Submit Review";
            BtnCancelReview.Text = "Cancel";
            LblWarningReview.Text = "Error! ";
            LblSuccessReview.Text = "Done! ";
            LblWarningMsg.Text = "Error! ";
            LblSuccessMsg.Text = "Done! ";
            Rttp1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "1")).Text;
            Rttp2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "2")).Text;
            Rttp3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "3")).Text;
            Rttp4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "4")).Text;
            Rttp5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "5")).Text;
            BtnCloseMessage.Text = "X";
            LblMessageGoFull.Text = "Create a full profile for free!";
            LblGoFullTitle.Text = "Complete your registration in order to have access to this feature! ";
            //LblGoFullContent.Text = "You have to be full registered in order to use any action buttons in this page or see all the details of this company profile. Click on the 'Sign Up Now' button and go through a free, simple and fast registration!";
            BtnCancelMessage.Text = "Close";
        }

        private void FixPage()
        {
            RdgReviews.ShowHeader = false;
            divInfoMessage.Visible = false;
            divSuccessMessage.Visible = false;
            divWarningMessage.Visible = false;
            rowMsgSaveProfile.Visible = false;

            if (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                int isDeleted = 0;
                aSaveProfile.Visible = (vSession.User != null) ? (!Sql.ExistUserFavoriteCompanyByIsDeletedStatus(vSession.User.Id, vSession.ElioCompanyDetailsView.Id, isDeleted, session) && vSession.User.Id != vSession.ElioCompanyDetailsView.Id) ? true : false : false;
                aSendMessage.Visible = (vSession.User != null) ? vSession.User.Id != vSession.ElioCompanyDetailsView.Id ? true : false : false;
                aSaveProfileNotFull.Visible = false;

                rowMsgNotFull.Visible = false;
                divReviewsNotFull.Visible = false;
                PnlReviews.Visible = true;
            }
            else
            {
                if (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
                {
                    aAddReview.Visible = true;
                    aSaveProfile.Visible = false;
                    aSaveProfileNotFull.Visible = true;

                    aSendMessage.Visible = true;

                    aAddReview.HRef = "#MdGoFull";
                    aSendMessage.HRef = "#MdGoFull";
                    LblSaveProfileNotFull.Text = "Save Profile";

                    PnlReviews.Visible = false;
                    divReviewsNotFull.Visible = true;
                    LblReviewsNotFull.Text = "Complete your registration for full access";

                    rowMsgNotFull.Visible = true;
                    LblMsgNotFull.Text = "Sign up and view the entire profile";
                    LblCreateAccount.Text = "Sign Up Free";
                    aCreateAccount.HRef = vSession.User == null ? ControlLoader.SignUp : ControlLoader.FullRegistrationPage;
                }
                else if (vSession.User == null)
                {
                    PnlReviews.Visible = true;
                    rowMsgNotFull.Visible = false;
                    divReviewsNotFull.Visible = false;
                    aAddReview.Visible = false;
                    aSaveProfile.Visible = false;
                    aSaveProfileNotFull.Visible = false;
                    aSendMessage.Visible = false;
                    LblCreateAccount.Text = "Sign Up Free";
                    aCreateAccount.HRef = vSession.User == null ? ControlLoader.SignUp : ControlLoader.FullRegistrationPage;
                }
            }

            LoadDetails();
        }

        private void ShowHideProfileDetails()
        {
            if ((vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) || (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session)))
            {
                #region Logged In / Full Registered User

                int isDeleted = 0;
                aSaveProfile.Visible = (!Sql.ExistUserFavoriteCompanyByIsDeletedStatus(vSession.User.Id, vSession.ElioCompanyDetailsView.Id, isDeleted, session) && vSession.User.Id != vSession.ElioCompanyDetailsView.Id) ? true : false;
                aSendMessage.Visible = vSession.User.Id != vSession.ElioCompanyDetailsView.Id ? true : false;
                aSaveProfileNotFull.Visible = false;

                rowMsgNotFull.Visible = false;
                divReviewsNotFull.Visible = false;
                PnlReviews.Visible = true;

                divDescriptionNotFull.Visible = false;
                divRegDateNotFull.Visible = false;
                divRatingNotFull.Visible = false;

                LblDescription.Text = GlobalMethods.FixParagraphsView(vSession.ElioCompanyDetailsView.Description);
                LblRegDate.Text = vSession.ElioCompanyDetailsView.SysDate.ToString("dd/MM/yyyy");

                BindRatingControl();
                LoadData();

                divMarketsNotFull.Visible = false;
                divProgramsNotFull.Visible = false;

                LblMarkets.Visible = true;
                LblPrograms.Visible = true;

                if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) || (Sql.IsUserAdministrator(vSession.User.Id, session)))
                {
                    divSubcategoriesNotFull.Visible = false;
                    divSubcategoriesNotPremium.Visible = false;

                    LblSubcategories.Visible = true;
                }
                else
                {
                    divSubcategoriesNotFull.Visible = false;
                    divSubcategoriesNotPremium.Visible = true;

                    LblSubcategories.Visible = false;

                    LblSubcategoriesNotPremium.Text = "Premium account feature";
                }

                if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
                {
                    divPdfNotFull.Visible = false;

                    divWebsiteNotFull.Visible = false;

                    HplnkWebsite.NavigateUrl = vSession.ElioCompanyDetailsView.WebSite;
                    HplnkWebsite.Text = vSession.ElioCompanyDetailsView.WebSite;
                    LblWebsite.Visible = false;

                    ElioUsersFiles userPdf = Sql.GetUserPdfFile(vSession.ElioCompanyDetailsView.Id, session);

                    if (userPdf != null)
                    {
                        bool fileExists = File.Exists(Server.MapPath(userPdf.FilePath));
                        if (fileExists)
                        {
                            aPdf.Visible = true;
                            iPdf.Visible = true;
                            string[] pdfArray = userPdf.FilePath.Split('/');
                            string pdfName = pdfArray[4];
                            LblPdfValue.Text = "Partner Program brochure ";
                            aPdf.HRef = userPdf.FilePath;
                            aPdf.Target = "_blank";
                        }
                        else
                        {
                            aPdf.Visible = false;
                            iPdf.Visible = false;
                        }
                    }
                    else
                    {
                        aPdf.Visible = false;
                        iPdf.Visible = false;
                    }
                }

                if ((!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.MashapeUsername) && vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()))
                {
                    divMashapeNotFull.Visible = false;

                    HplnkMashape.Text = (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.MashapeUsername) && vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) ? "https://www.mashape.com/" + vSession.ElioCompanyDetailsView.MashapeUsername : "-";
                    HplnkMashape.NavigateUrl = "https://www.mashape.com/?utm_source=myelio&utm_medium=website&utm_campaign=partners/" + vSession.ElioCompanyDetailsView.MashapeUsername;
                }
                else
                {
                    rowMashape.Attributes["class"] = "hidden";
                    HplnkMashape.Visible = false;
                    LblMashapeTitle.Visible = false;
                }

                divTotalReviewsNotFull.Visible = false;
                divAddressNotFull.Visible = false;
                divPhoneNotFull.Visible = false;

                LblTotalReviews.Text = Sql.GetCompanyTotalReviews(vSession.ElioCompanyDetailsView.Id, session).ToString();
                LblAddress.Text = vSession.ElioCompanyDetailsView.Address;

                bool hasPhone = false;
                LblPhone.Text = GlobalDBMethods.ShowUserPhone(vSession.ElioCompanyDetailsView, session, out hasPhone);

                #endregion
            }
            else
            {
                #region Not Logged In User / Not Full Registered User

                aAddReview.Visible = true;
                aSaveProfile.Visible = false;
                aSaveProfileNotFull.Visible = true;
                aSendMessage.Visible = true;

                aAddReview.HRef = "#MdGoFull";
                aSendMessage.HRef = "#MdGoFull";
                LblSaveProfileNotFull.Text = "Save Profile";

                PnlReviews.Visible = false;
                divReviewsNotFull.Visible = true;
                LblReviewsNotFull.Text = "Complete your registration for full access";

                rowMsgNotFull.Visible = true;
                LblMsgNotFull.Text = "Sign up and view the entire profile";
                LblCreateAccount.Text = "Sign Up Free";
                aCreateAccount.HRef = vSession.User == null ? ControlLoader.SignUp : ControlLoader.FullRegistrationPage;

                LblDescription.Visible = false;
                divDescriptionNotFull.Visible = true;
                LblDescriptionNotFull.Text = "Complete your registration for full access";

                LblRegDate.Visible = false;
                divRegDateNotFull.Visible = true;
                LblRegDateNotFull.Text = "Complete your registration for full access";

                decimal avg = Sql.GetCompanyAverageRating(vSession.ElioCompanyDetailsView.Id, session);
                int count = Sql.GetCompanyCountRatings(vSession.ElioCompanyDetailsView.Id, session);
                int average = Convert.ToInt32(avg);

                LblAverage.Text = "(" + avg.ToString("0.00") + ")";
                LblAverageRating.Text = count.ToString() + " user(s) have rated";

                switch (average)
                {
                    case 0:
                        r1.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                        break;

                    case 1:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r2.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        break;

                    case 2:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                        break;

                    case 3:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        break;

                    case 4:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                        break;

                    case 5:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        break;
                }

                ImgAverageRating.Visible = false;
                r1.Enabled = r2.Enabled = r3.Enabled = r4.Enabled = r5.Enabled = false;

                divRatingNotFull.Visible = true;

                LblRatingNotFull.Text = "Complete your registration for full access";

                LblMarkets.Visible = false;
                LblPrograms.Visible = false;
                LblSubcategories.Visible = false;

                divMarketsNotFull.Visible = true;
                divProgramsNotFull.Visible = true;
                divSubcategoriesNotPremium.Visible = false;
                divSubcategoriesNotFull.Visible = true;

                LblMarketsNotFull.Text = "Complete your registration for full access";
                LblProgramsNotFull.Text = "Complete your registration for full access";
                LblSubcategoriesNotFull.Text = "Complete your registration for full access";

                if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
                {
                    divPdfNotFull.Visible = false;

                    HplnkWebsite.Visible = false;
                    LblWebsite.Visible = false;
                    divWebsiteNotFull.Visible = true;
                    LblWebsiteNotFull.Text = "Complete your registration for full access";

                    iPdf.Visible = false;
                    aPdf.Visible = false;
                    divPdfNotFull.Visible = true;
                    LblPdfNotFull.Text = "Complete your registration to view partner program brochure";
                }

                if ((!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.MashapeUsername) && vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()))
                {
                    HplnkMashape.Visible = false;

                    divMashapeNotFull.Visible = true;

                    LblMashapeNotFull.Text = "Complete your registration for full access";
                }
                else
                {
                    rowMashape.Attributes["class"] = "hidden";
                    HplnkMashape.Visible = false;
                    LblMashapeTitle.Visible = false;
                }

                LblTotalReviews.Visible = false;
                LblAddress.Visible = false;
                LblPhone.Visible = false;

                divTotalReviewsNotFull.Visible = true;
                divAddressNotFull.Visible = true;
                divPhoneNotFull.Visible = true;

                LblTotalReviewsNotFull.Text = "Complete your registration for full access";
                LblAddressNotFull.Text = "Complete your registration for full access";
                LblPhoneNotFull.Text = "Complete your registration for full access";

                #endregion
            }
        }

        private void LoadIndustries()
        {
            List<ElioIndustries> industries = Sql.GetUsersIndustries(vSession.ElioCompanyDetailsView.Id, session);
            foreach (ElioIndustries industry in industries)
            {
                LblIndustries.Text += industry.IndustryDescription + ", ";
            }
            LblIndustries.Text = (industries.Count > 0) ? LblIndustries.Text.Substring(0, LblIndustries.Text.Length - 2) : "-";
        }

        private void LoadMarkets()
        {
            List<ElioMarkets> userMarkets = Sql.GetUsersMarkets(vSession.ElioCompanyDetailsView.Id, session);
            foreach (ElioMarkets userMarket in userMarkets)
            {
                LblMarkets.Text += userMarket.MarketDescription + ", ";
            }
            LblMarkets.Text = (userMarkets.Count > 0) ? LblMarkets.Text.Substring(0, LblMarkets.Text.Length - 2) : "-";
        }

        private void LoadPartners()
        {
            List<ElioPartners> userPartners = Sql.GetUsersPartners(vSession.ElioCompanyDetailsView.Id, session);
            foreach (ElioPartners userPartner in userPartners)
            {
                LblPrograms.Text += userPartner.PartnerDescription + ", ";
            }
            LblPrograms.Text = (userPartners.Count > 0) ? LblPrograms.Text.Substring(0, LblPrograms.Text.Length - 2) : "-";
        }

        private void LoadSubCategories()
        {
            List<ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers> profileSubcategories = Sql.GetUserSubcategoriesById(vSession.ElioCompanyDetailsView.Id, session);
            foreach (ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers subcategory in profileSubcategories)
            {
                LblSubcategories.Text += subcategory.DescriptionSubcategory + ", ";
            }
            LblSubcategories.Text = (profileSubcategories.Count > 0) ? LblSubcategories.Text.Substring(0, LblSubcategories.Text.Length - 2) : "-";
        }

        private void LoadApies()
        {
            List<ElioApies> userApies = Sql.GetUsersApies(vSession.ElioCompanyDetailsView.Id, session);
            foreach (ElioApies userApi in userApies)
            {
                LblApi.Text += userApi.ApiDescription + ", ";
            }
            LblApi.Text = (userApies.Count > 0) ? LblApi.Text.Substring(0, LblApi.Text.Length - 2) : "-";
        }

        private void ResetLabels()
        {
            LblIndustries.Text = string.Empty;
            LblSubcategories.Text = string.Empty;
            LblMarkets.Text = string.Empty;
            LblPrograms.Text = string.Empty;
            LblApi.Text = string.Empty;
            LblTotalReviews.Text = string.Empty;
            LblWebsite.Text = string.Empty;
            LblAddress.Text = string.Empty;
            LblPhone.Text = string.Empty;
            LblRegDate.Text = string.Empty;
            LblDescription.Text = string.Empty;
            LblOverview.Text = string.Empty;
            LblCompanyName.Text = string.Empty;
            LblCompanyType.Text = string.Empty;
        }

        private void LoadCompanyDetailsViewData()
        {
            if (vSession.ElioCompanyDetailsView != null)
            {
                LblCompanyName.Text = vSession.ElioCompanyDetailsView.CompanyName;
                ImgCompanyLogo.ImageUrl = vSession.ElioCompanyDetailsView.CompanyLogo;
                ImgCompanyLogo.AlternateText = vSession.ElioCompanyDetailsView.CompanyName + " in Elioplus";
                LblCompanyType.Text = vSession.ElioCompanyDetailsView.CompanyType;
                LblOverview.Text = GlobalMethods.FixParagraphsView(vSession.ElioCompanyDetailsView.Overview);
                LblProgramReviewsTitle.Text = vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString() ? "Partner program reviews" : "Company reviews";

                if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.VendorProductDemoLink))
                {
                    aViewProductDemo.Visible = true;
                    aViewProductDemo.HRef = vSession.ElioCompanyDetailsView.VendorProductDemoLink;
                    aViewProductDemo.Target = "_blank";
                    aViewProductDemoNotFull.Visible = false;
                }
                else
                {
                    aViewProductDemo.Visible = false;
                    aViewProductDemoNotFull.Visible = false;
                }
            }
        }

        private void LoadDetails1()
        {
            ResetLabels();

            LoadCompanyDetailsViewData();

            if ((vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) || (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session)))
            {
                #region Logged In / Full Registered User

                divDescriptionNotFull.Visible = false;
                divRegDateNotFull.Visible = false;
                divRatingNotFull.Visible = false;

                LblDescription.Text = GlobalMethods.FixParagraphsView(vSession.ElioCompanyDetailsView.Description);
                LblRegDate.Text = vSession.ElioCompanyDetailsView.SysDate.ToString("dd/MM/yyyy");

                BindRatingControl();
                LoadData();

                divMarketsNotFull.Visible = false;
                divProgramsNotFull.Visible = false;

                LblMarkets.Visible = true;
                LblPrograms.Visible = true;

                if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) || (Sql.IsUserAdministrator(vSession.User.Id, session)))
                {
                    divSubcategoriesNotFull.Visible = false;
                    divSubcategoriesNotPremium.Visible = false;

                    LblSubcategories.Visible = true;
                }
                else
                {
                    divSubcategoriesNotFull.Visible = false;
                    divSubcategoriesNotPremium.Visible = true;

                    LblSubcategories.Visible = false;

                    LblSubcategoriesNotPremium.Text = "Premium account feature";
                }

                #endregion
            }
            else
            {
                #region Not Logged In User / Not Full Registered User

                LblDescription.Visible = false;
                divDescriptionNotFull.Visible = true;
                LblDescriptionNotFull.Text = "Complete your registration for full access";

                LblRegDate.Visible = false;
                divRegDateNotFull.Visible = true;
                LblRegDateNotFull.Text = "Complete your registration for full access";

                decimal avg = Sql.GetCompanyAverageRating(vSession.ElioCompanyDetailsView.Id, session);
                int count = Sql.GetCompanyCountRatings(vSession.ElioCompanyDetailsView.Id, session);
                int average = Convert.ToInt32(avg);

                LblAverage.Text = "(" + avg.ToString("0.00") + ")";
                LblAverageRating.Text = count.ToString() + " user(s) have rated";

                switch (average)
                {
                    case 0:
                        r1.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                        break;

                    case 1:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r2.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        break;

                    case 2:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                        break;

                    case 3:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        break;

                    case 4:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                        break;

                    case 5:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        break;
                }

                ImgAverageRating.Visible = false;
                r1.Enabled = r2.Enabled = r3.Enabled = r4.Enabled = r5.Enabled = false;

                divRatingNotFull.Visible = true;

                LblRatingNotFull.Text = "Complete your registration for full access";

                #endregion
            }

            LoadIndustries();

            LoadMarkets();

            LoadPartners();

            LoadSubCategories();

            if (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                divMarketsNotFull.Visible = false;
                divProgramsNotFull.Visible = false;

                LblMarkets.Visible = true;
                LblPrograms.Visible = true;

                if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) || (Sql.IsUserAdministrator(vSession.User.Id, session)))
                {
                    divSubcategoriesNotFull.Visible = false;
                    divSubcategoriesNotPremium.Visible = false;

                    LblSubcategories.Visible = true;
                }
                else
                {
                    divSubcategoriesNotFull.Visible = false;
                    divSubcategoriesNotPremium.Visible = true;

                    LblSubcategories.Visible = false;

                    LblSubcategoriesNotPremium.Text = "Premium account feature";
                }
            }
            else
            {
                LblMarkets.Visible = false;
                LblPrograms.Visible = false;
                LblSubcategories.Visible = false;

                divMarketsNotFull.Visible = true;
                divProgramsNotFull.Visible = true;
                divSubcategoriesNotPremium.Visible = false;
                divSubcategoriesNotFull.Visible = true;

                LblMarketsNotFull.Visible = true;
                LblProgramsNotFull.Visible = true;
                LblSubcategoriesNotFull.Visible = true;

                LblMarketsNotFull.Text = "Complete your registration for full access";
                LblProgramsNotFull.Text = "Complete your registration for full access";
                LblSubcategoriesNotFull.Text = "Complete your registration for full access";
            }

            if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
            {
                LoadApies();

                if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
                {
                    if (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                    {
                        divApiNotFull.Visible = false;

                        LblApi.Visible = true;
                    }
                    else
                    {
                        LblApi.Visible = false;

                        divApiNotFull.Visible = true;
                        LblApiNotFull.Visible = true;

                        LblApiNotFull.Text = "Complete your registration for full access";
                    }
                }
            }

            if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
            {
                divPdfNotFull.Visible = false;                

                if (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                {
                    divWebsiteNotFull.Visible = false;

                    HplnkWebsite.NavigateUrl = vSession.ElioCompanyDetailsView.WebSite;
                    HplnkWebsite.Text = vSession.ElioCompanyDetailsView.WebSite;
                    LblWebsite.Visible = false;                                      
                    
                    ElioUsersFiles userPdf = Sql.GetUserPdfFile(vSession.ElioCompanyDetailsView.Id, session);

                    if (userPdf != null)
                    {
                        bool fileExists = File.Exists(Server.MapPath(userPdf.FilePath));
                        if (fileExists)
                        {
                            aPdf.Visible = true;
                            iPdf.Visible = true;
                            string[] pdfArray = userPdf.FilePath.Split('/');
                            string pdfName = pdfArray[4];
                            LblPdfValue.Text = "Partner Program brochure ";
                            aPdf.HRef = userPdf.FilePath;
                            aPdf.Target = "_blank";
                        }
                        else
                        {
                            aPdf.Visible = false;
                            iPdf.Visible = false;
                        }
                    }
                    else
                    {
                        aPdf.Visible = false;
                        iPdf.Visible = false;
                    }
                }
                else
                {
                    HplnkWebsite.Visible = false;
                    LblWebsite.Visible = false;
                    divWebsiteNotFull.Visible = true;
                    LblWebsiteNotFull.Text = "Complete your registration for full access";

                    iPdf.Visible = false;
                    aPdf.Visible = false;
                    divPdfNotFull.Visible = true;
                    LblPdfNotFull.Text = "Complete your registration to view partner program brochure";
                }
            }
            else if (vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                rowWebsite.Attributes["class"] = "hidden";
                rowApies.Attributes["class"] = "hidden";
                iPdf.Visible = false; 
                aPdf.Visible = false;
            }
            else
            {
                aPdf.Visible = false;
                iPdf.Visible = false;
            }

            if ((!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.MashapeUsername) && vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()))
            {
                if (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                {
                    divMashapeNotFull.Visible = false;

                    HplnkMashape.Text = (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.MashapeUsername) && vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) ? "https://www.mashape.com/" + vSession.ElioCompanyDetailsView.MashapeUsername : "-";
                    HplnkMashape.NavigateUrl = "https://www.mashape.com/?utm_source=myelio&utm_medium=website&utm_campaign=partners/" + vSession.ElioCompanyDetailsView.MashapeUsername;
                }
                else
                {
                    HplnkMashape.Visible = false;

                    divMashapeNotFull.Visible = true;

                    LblMashapeNotFull.Text = "Complete your registration for full access";
                }
            }
            else
            {
                rowMashape.Attributes["class"] = "hidden";
                HplnkMashape.Visible = false;
                LblMashapeTitle.Visible = false;
            }

            if (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                divTotalReviewsNotFull.Visible = false;
                divAddressNotFull.Visible = false;
                divPhoneNotFull.Visible = false;

                LblTotalReviews.Text = Sql.GetCompanyTotalReviews(vSession.ElioCompanyDetailsView.Id, session).ToString();
                LblAddress.Text = vSession.ElioCompanyDetailsView.Address;

                bool hasPhone = false;
                LblPhone.Text = GlobalDBMethods.ShowUserPhone(vSession.ElioCompanyDetailsView, session, out hasPhone);
            }
            else
            {
                LblTotalReviews.Visible = false;
                LblAddress.Visible = false;
                LblPhone.Visible = false;

                divTotalReviewsNotFull.Visible = true;
                divAddressNotFull.Visible = true;
                divPhoneNotFull.Visible = true;

                LblTotalReviewsNotFull.Text = "Complete your registration for full access";
                LblAddressNotFull.Text = "Complete your registration for full access";
                LblPhoneNotFull.Text = "Complete your registration for full access";
            }
        }

        private void LoadDetails()
        {
            ResetLabels();

            LoadCompanyDetailsViewData();

            if ((vSession.User == null) && (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) || (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) || (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session)))
            {
                #region Logged In / Full Registered User

                divDescriptionNotFull.Visible = false;
                divRegDateNotFull.Visible = false;
                divRatingNotFull.Visible = false;

                LblDescription.Text = GlobalMethods.FixParagraphsView(vSession.ElioCompanyDetailsView.Description);
                LblRegDate.Text = vSession.ElioCompanyDetailsView.SysDate.ToString("dd/MM/yyyy");

                BindRatingControl();
                LoadData();

                divMarketsNotFull.Visible = false;
                divProgramsNotFull.Visible = false;

                LblMarkets.Visible = true;
                LblPrograms.Visible = true;

                divMarketsNotFull.Visible = false;
                divProgramsNotFull.Visible = false;

                LblMarkets.Visible = true;
                LblPrograms.Visible = true;

                divTotalReviewsNotFull.Visible = false;
                divAddressNotFull.Visible = false;
                divPhoneNotFull.Visible = false;

                LblTotalReviews.Text = Sql.GetCompanyTotalReviews(vSession.ElioCompanyDetailsView.Id, session).ToString();
                LblAddress.Text = vSession.ElioCompanyDetailsView.Address;

                bool hasPhone = false;
                LblPhone.Text = GlobalDBMethods.ShowUserPhone(vSession.ElioCompanyDetailsView, session, out hasPhone);

                if (vSession.User != null && vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) || (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session)))
                {
                    divSubcategoriesNotFull.Visible = false;
                    divSubcategoriesNotPremium.Visible = false;
                    LblSubcategories.Visible = true;

                    divSubcategoriesNotFull.Visible = false;
                    divSubcategoriesNotPremium.Visible = false;
                    LblSubcategories.Visible = true;
                }
                else
                {
                    divSubcategoriesNotFull.Visible = false;
                    divSubcategoriesNotPremium.Visible = true;
                    LblSubcategories.Visible = false;
                    LblSubcategoriesNotPremium.Text = "Premium account feature";

                    divSubcategoriesNotFull.Visible = false;
                    divSubcategoriesNotPremium.Visible = true;
                    LblSubcategories.Visible = false;
                    LblSubcategoriesNotPremium.Text = "Premium account feature";
                }

                #endregion
            }
            else
            {
                #region Not Logged In User / Not Full Registered User

                LblDescription.Visible = false;
                divDescriptionNotFull.Visible = true;
                LblDescriptionNotFull.Text = "Complete your registration for full access";

                LblRegDate.Visible = false;
                divRegDateNotFull.Visible = true;
                LblRegDateNotFull.Text = "Complete your registration for full access";

                decimal avg = Sql.GetCompanyAverageRating(vSession.ElioCompanyDetailsView.Id, session);
                int count = Sql.GetCompanyCountRatings(vSession.ElioCompanyDetailsView.Id, session);
                int average = Convert.ToInt32(avg);

                LblAverage.Text = "(" + avg.ToString("0.00") + ")";
                LblAverageRating.Text = count.ToString() + " user(s) have rated";

                switch (average)
                {
                    case 0:
                        r1.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                        break;

                    case 1:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r2.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        break;

                    case 2:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                        break;

                    case 3:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                        break;

                    case 4:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                        break;

                    case 5:
                        r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r4.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                        r5.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                        break;
                }

                ImgAverageRating.Visible = false;
                r1.Enabled = r2.Enabled = r3.Enabled = r4.Enabled = r5.Enabled = false;

                divRatingNotFull.Visible = true;

                LblRatingNotFull.Text = "Complete your registration for full access";

                LblMarkets.Visible = false;
                LblPrograms.Visible = false;
                LblSubcategories.Visible = false;

                divMarketsNotFull.Visible = true;
                divProgramsNotFull.Visible = true;
                divSubcategoriesNotPremium.Visible = false;
                divSubcategoriesNotFull.Visible = true;

                LblMarketsNotFull.Visible = true;
                LblProgramsNotFull.Visible = true;
                LblSubcategoriesNotFull.Visible = true;

                LblMarketsNotFull.Text = "Complete your registration for full access";
                LblProgramsNotFull.Text = "Complete your registration for full access";
                LblSubcategoriesNotFull.Text = "Complete your registration for full access";

                LblTotalReviews.Visible = false;
                LblAddress.Visible = false;
                LblPhone.Visible = false;

                divTotalReviewsNotFull.Visible = true;
                divAddressNotFull.Visible = true;
                divPhoneNotFull.Visible = true;

                LblTotalReviewsNotFull.Text = "Complete your registration for full access";
                LblAddressNotFull.Text = "Complete your registration for full access";
                LblPhoneNotFull.Text = "Complete your registration for full access";

                #endregion
            }

            LoadIndustries();

            LoadMarkets();

            LoadPartners();

            LoadSubCategories();

            if (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString())
            {
                LoadApies();

                divPdfNotFull.Visible = false;

                if ((vSession.User == null) && (vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) || vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                {
                    #region Logged In / Full Registered User

                    divApiNotFull.Visible = false;

                    LblApi.Visible = true;

                    divWebsiteNotFull.Visible = false;

                    HplnkWebsite.NavigateUrl = vSession.ElioCompanyDetailsView.WebSite;
                    HplnkWebsite.Text = vSession.ElioCompanyDetailsView.WebSite;
                    LblWebsite.Visible = false;

                    ElioUsersFiles userPdf = Sql.GetUserPdfFile(vSession.ElioCompanyDetailsView.Id, session);

                    if (userPdf != null)
                    {
                        bool fileExists = File.Exists(Server.MapPath(userPdf.FilePath));
                        if (fileExists)
                        {
                            aPdf.Visible = true;
                            iPdf.Visible = true;
                            string[] pdfArray = userPdf.FilePath.Split('/');
                            string pdfName = pdfArray[4];
                            LblPdfValue.Text = "Partner Program brochure ";
                            aPdf.HRef = userPdf.FilePath;
                            aPdf.Target = "_blank";
                        }
                        else
                        {
                            aPdf.Visible = false;
                            iPdf.Visible = false;
                        }
                    }
                    else
                    {
                        aPdf.Visible = false;
                        iPdf.Visible = false;
                    }

                    if (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.MashapeUsername))
                    {
                        divMashapeNotFull.Visible = false;

                        HplnkMashape.Text = (!string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.MashapeUsername) && vSession.ElioCompanyDetailsView.CompanyType == Types.Vendors.ToString()) ? "https://www.mashape.com/" + vSession.ElioCompanyDetailsView.MashapeUsername : "-";
                        HplnkMashape.NavigateUrl = "https://www.mashape.com/?utm_source=myelio&utm_medium=website&utm_campaign=partners/" + vSession.ElioCompanyDetailsView.MashapeUsername;
                    }
                    else
                    {
                        HplnkMashape.Visible = false;

                        divMashapeNotFull.Visible = true;

                        LblMashapeNotFull.Text = "Complete your registration for full access";
                    }

                    #endregion
                }
                else
                {
                    #region Not Logged In User / Not Full Registered User

                    LblApi.Visible = false;

                    divApiNotFull.Visible = true;
                    LblApiNotFull.Visible = true;

                    LblApiNotFull.Text = "Complete your registration for full access";

                    HplnkWebsite.Visible = false;
                    LblWebsite.Visible = false;
                    divWebsiteNotFull.Visible = true;
                    LblWebsiteNotFull.Text = "Complete your registration for full access";

                    iPdf.Visible = false;
                    aPdf.Visible = false;
                    divPdfNotFull.Visible = true;
                    LblPdfNotFull.Text = "Complete your registration to view partner program brochure";

                    #endregion
                }
            }
            else if (vSession.ElioCompanyDetailsView.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                rowWebsite.Attributes["class"] = "hidden";
                rowApies.Attributes["class"] = "hidden";
                iPdf.Visible = false;
                aPdf.Visible = false;
            }
            else
            {
                aPdf.Visible = false;
                iPdf.Visible = false;
            }

            if ((string.IsNullOrEmpty(vSession.ElioCompanyDetailsView.MashapeUsername) || vSession.ElioCompanyDetailsView.CompanyType != Types.Vendors.ToString()))
            {
                if ((vSession.User == null) || (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)))
                {
                    rowMashape.Attributes["class"] = "hidden";
                    HplnkMashape.Visible = false;
                    LblMashapeTitle.Visible = false;
                }
            }

            rowMashape.Attributes["class"] = "hidden";
            HplnkMashape.Visible = false;
            LblMashapeTitle.Visible = false;
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
                FixRatingImages(total / count);
                LblAverage.Visible = true;
                decimal avrg = Convert.ToDecimal(total) / Convert.ToDecimal(count);
                LblAverage.Text = "({average})";
                LblAverage.Text = LblAverage.Text.Replace("({average})", "(" + avrg.ToString("0.00") + ")");
                LblAverageRating.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "message", "2")).Text.Replace("{count}", count.ToString());
            }
            else
            {
                LblAverage.Visible = false;
                FixRatingImages(0);
                LblAverageRating.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "message", "2")).Text.Replace("{count}", count.ToString());
            }            
        }

        private void FixRatingImages(int average)
        {
            if (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                if (vSession.User.Id != vSession.ElioCompanyDetailsView.Id)
                {
                    PnlRating.Enabled = Sql.AllowCompanyToRate(vSession.User.Id, vSession.ElioCompanyDetailsView.Id, session);
                    RttpRating.Text = (PnlRating.Enabled) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "6")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "10")).Text;
                }
                else
                {
                    PnlRating.Enabled = false;
                    RttpRating.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "11")).Text;
                }
            }
            else
            {
                PnlRating.Enabled = false;
                RttpRating.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "details", "tooltip", "12")).Text;
            }

            switch (average)
            {
                case 0:
                    r1.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    r2.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                    break;

                case 1:
                    r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                    r2.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    break;

                case 2:
                    r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                    r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                    r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                    break;

                case 3:
                    r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                    r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                    r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                    r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                    break;

                case 4:
                    r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                    r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                    r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                    r4.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                    r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                    break;

                case 5:
                    r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                    r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                    r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                    r4.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                    r5.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                    break;
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

                if (vSession.User == null || (vSession.User != null && vSession.User.Id != vSession.ElioCompanyDetailsView.Id))
                {
                    if (vSession.ElioCompanyDetailsView != null)
                    {
                        ElioUsersProductDemoViews demoViews = Sql.GetUserProductDemoViews(vSession.ElioCompanyDetailsView.Id, session);

                        if (demoViews == null)
                        {
                            demoViews = new ElioUsersProductDemoViews();

                            demoViews.UserId = vSession.ElioCompanyDetailsView.Id;

                            if (vSession.User != null)
                                demoViews.VisitorCompanyId = vSession.User.Id;

                            demoViews.Sysdate = DateTime.Now;
                            demoViews.LastUpdated = DateTime.Now;
                            demoViews.Count++;

                            DataLoader<ElioUsersProductDemoViews> loader = new DataLoader<ElioUsersProductDemoViews>(session);
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

                            DataLoader<ElioUsersProductDemoViews> loader = new DataLoader<ElioUsersProductDemoViews>(session);
                            loader.Update(demoViews);
                        }

                        aViewProductDemo.Target = "_blank";
                        Response.Redirect(aViewProductDemo.HRef, false);
                    }
                    else
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

                        aSaveProfile.Visible = false;

                        session.CommitTransaction();

                        rowMsgSaveProfile.Visible = true;
                        divErrorSaveProfile.Visible = false;
                        divScsSaveProfile.Visible = true;
                        LblScsSaveProfile.Text = "Done! ";
                        LblSuccessSaveProfileContent.Text = "You have successfully saved this company's profile";
                    }
                    else
                    {
                        rowMsgSaveProfile.Visible = true;
                        divErrorSaveProfile.Visible = true;
                        divScsSaveProfile.Visible = false;
                        LblErrorSaveProfile.Text = "Error! ";
                        LblErrorSaveProfileContent.Text = "Something wrong happened, try again or contact us.";
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
                        RdgReviews.Rebind();
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

                    LblTotalReviews.Text = Sql.GetCompanyTotalReviews(vSession.ElioCompanyDetailsView.Id, session).ToString();

                    if (!Sql.IsUserAdministrator(vSession.User.Id, session))
                    {
                        //EmailNotificationsLib.SendNotificationEmailToCompanyForNewReview(vSession.User, vSession.ElioCompanyDetailsView.Email, vSession.Lang, session);
                        EmailSenderLib.SendNotificationEmailToCompanyForNewReview(vSession.User, vSession.ElioCompanyDetailsView.Email, vSession.Lang, session);
                    }

                    RdgReviews.Rebind();

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

                        LblTotalReviews.Text = Sql.GetCompanyTotalReviews(review.CompanyId, session).ToString();

                        RdgReviews.Rebind();
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
                        RdgReviews.Visible = true;
                        divSuccessMessage.Visible = false;
                        divWarningMessage.Visible = false;
                        divInfoMessage.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("visitor_id");
                        foreach (ElioUserProgramReview review in reviews)
                        {
                            table.Rows.Add(review.Id, review.VisitorId);
                        }

                        RdgReviews.DataSource = table;

                        RdgReviews.ShowHeader = false;
                    }
                    else
                    {
                        RdgReviews.Visible = false;

                        divSuccessMessage.Visible = false;
                        divWarningMessage.Visible = false;
                        divInfoMessage.Visible = true;
                        LblInfoMessage.Text = "There are no reviews for this user yet. Be the first!";
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
    }
}