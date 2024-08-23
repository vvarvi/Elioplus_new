using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus
{
    public partial class DashboardFavourites : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        string companyTypesIn = "" + Types.Vendors.ToString() + "','" + EnumHelper.GetDescription(Types.Resellers).ToString() + "";

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

        #region Methods

        private void FixPage()
        {
            if (!IsPostBack)
            {
                divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                UpdateStrings();
                SetLinks();

                if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    LblRenewalHead.Visible = LblRenewal.Visible = true;                    
                    LblRenewalHead.Text = "Renewal date: ";

                    ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
                    if (packet != null)
                    {
                        LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
                    }

                    try
                    {
                        LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");
                    }
                    catch (Exception)
                    {
                        LblRenewalHead.Visible = LblRenewal.Visible = false;

                        Logger.Debug(string.Format("User {0} seems to be premium but he has no order in his account", vSession.User.Id.ToString()));
                    }
                }
                else
                {
                    LblRenewalHead.Visible = LblRenewal.Visible = false;
                    LblPricingPlan.Text = "You are currently on a free plan";
                }

                aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
                aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

                LblElioplusDashboard.Text = !string.IsNullOrEmpty(vSession.User.CompanyName) ? vSession.User.CompanyName + " dashboard" : (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName + " dashboard" : vSession.User.Username + " dashboard";

                LblDashboard.Text = "Dashboard";

                if (aBtnGoPremium.Visible)
                {
                    LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
                    LblPricingPlan.Visible = false;
                }

                LblGoFull.Text = "Complete your registration";
                LblDashPage.Text = "Favourites";
                LblDashSubTitle.Text = "your favourite saved companies";
                
                LblAdditInfo.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;
                LblAdditInfo.Text = "It is available only for full registered users. It's free! ";
                aActionLink1.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;
                aActionLink1.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
                LblActionInfo.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;
                LblActionInfo.Text = " Go full now!";
                
                LblAdditInfo2.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;
                LblAdditInfo2.Text = "It is available only for full registered users. It's free! ";
                aActionLink2.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;
                aActionLink2.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
                LblActionInfo2.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;
                LblActionInfo2.Text = " Go full now!";
            }

            
        }

        private void SetLinks()
        {
            aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
            //aBtnGoPremium.HRef = ControlLoader.Dashboard(vSession.User, "billing");
        }

        private void UpdateStrings()
        {            
            LblFavouritedPageInfo.Text = "In the list below you will find those companies that have saved your company profile to their favourites. " +
                                            "Go and visit their profile, some of them could be your potential future business partners!";
            LblFavouritedTitle.Text = "Favourited company profiles. Take a closer look!";
            LblFavouritesPageInfo.Text = "The list below is the one with your favourite company profiles. Always keep your list updated!";
            LblFavouritesTitle.Text = "Your favourite company profiles!";            
        }

        #endregion

        #region Grids

        protected void RdgUserFavouritedCompanies_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    session.OpenConnection();

                    ElioUsers company = Sql.GetUserById(Convert.ToInt32(item["user_id"].Text), session);

                    if (company != null)
                    {
                        HtmlAnchor companyLogoLink = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        companyLogoLink.HRef = company != null ? ControlLoader.Profile(company) : ControlLoader.Default();
                        companyLogoLink.Target = "_blank";

                        Image companyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                        companyLogo.ImageUrl = !string.IsNullOrEmpty(company.CompanyLogo) ? company.CompanyLogo : !string.IsNullOrEmpty(company.PersonalImage) ? company.PersonalImage : "/assets/layouts/layout/img/avatar.png";

                        HtmlAnchor companyNameLink = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                        companyNameLink.HRef = company != null ? ControlLoader.Profile(company) : ControlLoader.Default();
                        companyNameLink.Target = "_blank";

                        Label companyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyNameContent");
                        companyName.Text = !string.IsNullOrEmpty(company.CompanyName) ? company.CompanyName : (!string.IsNullOrEmpty(company.FirstName) && !string.IsNullOrEmpty(company.LastName)) ? company.FirstName + " " + company.LastName : company.Username;

                        Label companyType = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyTypeContent");
                        companyType.Text = !string.IsNullOrEmpty(company.CompanyType) ? company.CompanyType : "-";
                    }
                    else
                        Logger.DetailedError(Request.Url.ToString(), "DashboardFavorites --> RdgUserFavouritedCompanies_OnItemDataBound --> ElioUsers company = Sql.GetUserById(Convert.ToInt32(item[\"user_id\"].Text), session) --> company was NULL");

                    ElioUsersFavorites favourite = Sql.GetUsersNotDeletedFavoritesById(Convert.ToInt32(item["id"].Text), session);

                    if (favourite != null)
                    {
                        Label companyViewDate = (Label)ControlFinder.FindControlRecursive(item, "LblViewDateContent");
                        companyViewDate.Text = !string.IsNullOrEmpty(favourite.LastUpDated.ToString()) ? favourite.LastUpDated.ToString("MM/dd/yyyy") : "-";
                    }
                    else
                        Logger.DetailedError(Request.Url.ToString(), "DashboardFavorites --> RdgUserFavouritedCompanies_OnItemDataBound --> ElioUsersFavorites favourite = Sql.GetUsersNotDeletedFavoritesById(Convert.ToInt32(item[\"id\"].Text), session) --> favourite was NULL");

                    //Label lblBtnDelete = (Label)ControlFinder.FindControlRecursive(item, "LblBtnDelete");
                    //lblBtnDelete.Text = "Delete";
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

        protected void RdgUserFavouritedCompanies_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcFavouritedMessageAlert.Visible = false;

                if (vSession.User != null)
                {
                    int isDeleted = 0;
                    List<ElioUsersFavoritesIJUsers> favouritedCompanies = Sql.GetUserFavouritedCompaniesIJUsersByIsDeletedStatus(vSession.User.Id, isDeleted, session);

                    if (favouritedCompanies.Count > 0)
                    {
                        RdgUserFavouritedCompanies.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("user_id");

                        foreach (ElioUsersFavoritesIJUsers favouritedCompany in favouritedCompanies)
                        {
                            table.Rows.Add(favouritedCompany.Id, favouritedCompany.UserId);
                        }

                        RdgUserFavouritedCompanies.DataSource = table;
                    }
                    else
                    {
                        RdgUserFavouritedCompanies.Visible = false;

                        GlobalMethods.ShowMessageControlDA(UcFavouritedMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "16")).Text, MessageTypes.Info, true, true, false);
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

        protected void RdgFavourites_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    session.OpenConnection();

                    ElioUsers company = Sql.GetUserById(Convert.ToInt32(item["company_id"].Text), session);

                    if (company != null)
                    {
                        HtmlAnchor companyLogoLink = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        companyLogoLink.HRef = company != null ? ControlLoader.Profile(company) : ControlLoader.Default();
                        companyLogoLink.Target = "_blank";

                        Image companyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                        companyLogo.ImageUrl = !string.IsNullOrEmpty(company.CompanyLogo) ? company.CompanyLogo : !string.IsNullOrEmpty(company.PersonalImage) ? company.PersonalImage : "/assets/layouts/layout/img/avatar.png";

                        HtmlAnchor companyNameLink = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                        companyNameLink.HRef = company != null ? ControlLoader.Profile(company) : ControlLoader.Default();
                        companyNameLink.Target = "_blank";

                        Label companyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyNameContent");
                        companyName.Text = !string.IsNullOrEmpty(company.CompanyName) ? company.CompanyName : (!string.IsNullOrEmpty(company.FirstName) && !string.IsNullOrEmpty(company.LastName)) ? company.FirstName + " " + company.LastName : company.Username;

                        Label companyType = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyTypeContent");
                        companyType.Text = !string.IsNullOrEmpty(company.CompanyType) ? company.CompanyType : "-";
                    }
                    else
                        Logger.DetailedError(Request.Url.ToString(), "DashboardFavorites --> RdgFavourites_OnItemDataBound --> ElioUsers company = Sql.GetUserById(Convert.ToInt32(item[\"user_id\"].Text), session) --> company was NULL");

                    ElioUsersFavorites favourite = Sql.GetUsersNotDeletedFavoritesById(Convert.ToInt32(item["id"].Text), session);

                    if (favourite != null)
                    {
                        Label companyViewDate = (Label)ControlFinder.FindControlRecursive(item, "LblViewDateContent");
                        companyViewDate.Text = !string.IsNullOrEmpty(favourite.LastUpDated.ToString()) ? favourite.LastUpDated.ToString("MM/dd/yyyy") : "-";
                    }
                    else
                        Logger.DetailedError(Request.Url.ToString(), "DashboardFavorites --> RdgFavourites_OnItemDataBound --> ElioUsersFavorites favourite = Sql.GetUsersNotDeletedFavoritesById(Convert.ToInt32(item[\"id\"].Text), session) --> favourite was NULL");

                    Label lblBtnDelete = (Label)ControlFinder.FindControlRecursive(item, "LblBtnDelete");
                    lblBtnDelete.Text = "Delete";
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

        protected void RdgFavourites_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcFavouritesMessageAlert.Visible = false;

                if (vSession.User != null)
                {
                    int isDeleted = 0;
                    List<ElioUsersFavoritesIJUsers> favourites = Sql.GetUserFavouritesIJUsersByIsDeletedStatus(vSession.User.Id, isDeleted, session);

                    if (favourites.Count > 0)
                    {
                        RdgFavourites.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("company_id");

                        foreach (ElioUsersFavoritesIJUsers favourite in favourites)
                        {
                            table.Rows.Add(favourite.Id, favourite.CompanyId);
                        }

                        RdgFavourites.DataSource = table;
                    }
                    else
                    {
                        RdgFavourites.Visible = false;

                        GlobalMethods.ShowMessageControlDA(UcFavouritesMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "15")).Text, MessageTypes.Info, true, true, false);
                    }
                }
                else
                {
                    Response.Redirect(vSession.Page, false);
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

        #region Buttons

        protected void BtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    HtmlAnchor btnDelete = (HtmlAnchor)sender;
                    GridDataItem item = (GridDataItem)btnDelete.NamingContainer;

                    Sql.UpdateUsersFavoritesDeleteStatusById(Convert.ToInt32(item["id"].Text), 1, session);

                    RdgFavourites.Rebind();

                    //ElioUsersFavorites favourite = Sql.GetUsersNotDeletedFavoritesById(Convert.ToInt32(item["id"].Text), session);

                    //if (favourite != null)
                    //{
                    //    DataLoader<ElioUsersFavorites> loader = new DataLoader<ElioUsersFavorites>(session);
                    //    favourite.IsDeleted = 1;
                    //    loader.Update(favourite);

                    //    RdgFavourites.Rebind();
                    //}
                }
                else
                {
                    Response.Redirect(ControlLoader.Default());
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