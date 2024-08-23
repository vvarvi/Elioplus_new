using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus
{
    public partial class ElioplusEmpty : System.Web.UI.MasterPage
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        private string CookieName
        {
            get
            {
                return "lgn";
            }
        }

        private string VendorName
        {
            get
            {
                object value = ViewState["VendorName"];
                return value != null ? value.ToString() : "";
            }
            set
            {
                ViewState["VendorName"] = Regex.Replace(value, @"[^A-Za-z0-9]+", "_").Trim().ToLower();
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();
                                
                if (!IsPostBack)
                {
                    UpdateStrings();
                    SetLinks();
                    PageTitle();
                }

                if (vSession.User == null)
                {
                    #region Login by cookies

                    if (Request.Browser.Cookies)
                    {
                        HttpCookie loginCookie = Request.Cookies[CookieName];

                        if (loginCookie != null)
                        {
                            if (loginCookie.Value.Length % 16 == 0)
                            {
                                List<string> usernameencr = Encrypter.SplitStringBySize(loginCookie.Value, loginCookie.Value.Length / 2);

                                string responseError = string.Empty;

                                ElioUsers user = Sql.GetUserByUsernameAndPassword(Encrypter.DecryptStringAes(usernameencr[0], Encrypter.SharedSecret), Encrypter.DecryptStringAes(usernameencr[1], Encrypter.SharedSecret), Encrypter.DecryptStringAes(usernameencr[0], Encrypter.SharedSecret), HttpContext.Current.Session.SessionID, out responseError, vSession.Lang, out string subAccountEmailLogin, out int loggedInRoleID, out bool isAdminRole, session);
                                if (user != null)
                                {
                                    vSession.SubAccountEmailLogin = subAccountEmailLogin;
                                    vSession.LoggedInSubAccountRoleID = loggedInRoleID;
                                    vSession.IsAdminRole = isAdminRole;

                                    if (user.AccountStatus != Convert.ToInt32(AccountStatus.Blocked))
                                    {
                                        //iUserFull.Visible = (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? true : false;
                                        //iUserNotFull.Visible = (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? false : true;

                                        //liMainMenuUser.Visible = liMainMenuStatus.Visible = liMainMenuNotifications.Visible = liMainMenuMessages.Visible = (user == null) ? false : true;

                                        vSession.User = user;
                                    }
                                    else
                                    {
                                        ////LblUserBlocked.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "17")).Text;
                                    }
                                }
                                else
                                {
                                    Session.Clear();
                                    loginCookie.Expires = DateTime.Now.AddYears(-30);
                                    Response.Cookies.Add(loginCookie);
                                    Response.Redirect(vSession.Page = ControlLoader.Default(), false);
                                }
                            }
                        }
                    }

                    #endregion
                }

                if (!IsPostBack)
                {
                    
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(session, ex, Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #region Methods

        protected void SetUserDataForCustomersAPI()
        {
            if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
            {

            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["UseCustomersAPI"]))
            {
                if (ConfigurationManager.AppSettings["UseCustomersAPI"].ToString() == "true")
                {
                    List<ElioUsers> users = Sql.GetPublicFullRegisteredUsersJoinCustomersAPI(AccountPublicStatus.IsPublic, AccountStatus.Completed, session);
                    if (users.Count > 0 && vSession.LoggedInUsersCount <= 100)
                    {
                        Response.Redirect(ControlLoader.Login, false);
                    }
                }
            }
        }

        private void PageTitle()
        {
            string metaDescription = "";
            string metaKeywords = "";

            if (PgTitle.InnerText == "")
                PgTitle.InnerText = GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.ElioCompanyDetailsView, out metaDescription, out metaKeywords, session);
        }

        private void SetLinks()
        {

            if (vSession.User != null)
            {
                VendorName = SqlCollaboration.GetVendorNameByResellerUserId(vSession.User.Id, session);
                if (VendorName != "")
                {
                    //aMainMenuUserLogout.HRef = ControlLoader.LogoutPartner.Replace("{CompanyName}", VendorName);
                }
                else
                {
                    //aMainMenuUserLogout.HRef = ControlLoader.Login;
                }

            }
        }

        private void UpdateStrings()
        {
            //ImgLogo.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "tooltip", "1")).Text;
            //ImgLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "alternate", "1")).Text;
            //Label1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "1")).Text;
            //Label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "2")).Text;
            //Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "3")).Text;
            //Label4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "4")).Text;
            //Label5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "5")).Text;
            //Label6.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "6")).Text;
            //Label7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "7")).Text;
            //Label8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "8")).Text;
            //Label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "9")).Text;
            //Label10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "10")).Text;
            //Label11.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "11")).Text;
            //Label12.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "12")).Text;
            //Label13.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "13")).Text;
            //Label14.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "14")).Text;
            //Label15.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "15")).Text;
            //Label16.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "16")).Text;
            //Label17.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "17")).Text;
            //Label18.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "18")).Text;
            //Label19.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "19")).Text;
            //Label20.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "20")).Text;
            //Label21.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "21")).Text;
            //Label22.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "22")).Text;
            //Label23.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "23")).Text;
            //Label24.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "24")).Text;
            //Label25.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "25")).Text;
            //Label26.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "26")).Text;
            //Label27.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "27")).Text;
            //Label28.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "28")).Text;
            //Label29.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "29")).Text;
            //Label30.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "30")).Text;
            //Label31.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "31")).Text;
            //Label32.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "32")).Text;
            //Label33.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "33")).Text;
            //Label34.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "34")).Text;
            //Label35.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "35")).Text;
            //Label36.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "36")).Text;
            //Label37.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "37")).Text;
        }

        #endregion

        #region Buttons

        #endregion
    }
}