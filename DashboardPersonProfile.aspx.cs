using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.IO;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI.Entities;

namespace WdS.ElioPlus
{
    public partial class DashboardPersonProfile : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        private string TutorialIsWatched
        {
            get
            {
                return "trl";
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (HttpContext.Current.Request.Url.AbsolutePath.EndsWith("/"))
                    {
                        Response.Redirect(vSession.Page = HttpContext.Current.Request.Url.AbsolutePath.Substring(0, HttpContext.Current.Request.Url.AbsolutePath.Length - 1), false);
                        return;
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
                }
                else
                {
                    Response.Redirect(vSession.Page = ControlLoader.Default(), false);
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

        # region methods

        private void LoadData()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            string[] pathElements = path.Split('/');

            int number;
            int userId = (int.TryParse(pathElements[2], out number)) ? number : 0;

            ElioUsers user = Sql.GetUserById(userId, session);

            if (user != null)
            {
                //ElioUsersPersonIJPersonCompanies personCompany = Sql.GetPersonCompaniesByUserId(user.Id, session);
                ElioUsersPerson person = ClearbitSql.GetPersonByUserId(user.Id, session);

                if (person != null)
                {
                    LblElioplusDashboard.Text = user.CompanyName;
                    imgCompanyLogo.Src = user.PersonalImage;

                    ElioUsersPersonCompanies company = ClearbitSql.GetPersonCompanyByPersonIdUserId(person.Id, user.Id, session);
                    if (company != null)
                    {
                        List<ElioUsersPersonCompanyPhoneNumbers> companyPhones = ClearbitSql.GetPersonCompanyPhones(company.Id, user.Id, session);

                        if (companyPhones.Count > 0)
                        {
                            string phones = "";
                            foreach (ElioUsersPersonCompanyPhoneNumbers phone in companyPhones)
                            {
                                phones += phone.PhoneNumber + ", ";
                            }

                            phones = phones.Substring(0, phones.Length - 2);
                        }

                        string tagnames = ClearbitSql.GetPersonCompanyTagsAsString(company.Id, session);
                        if (tagnames != "")
                        {
                            string industries = tagnames;
                        }

                        //List<ElioUsersPersonCompanyTags> tags = ClearbitSql.GetPersonCompanyTags(company.Id, session);

                        //if (tags.Count > 0)
                        //{
                        //    string industries = "";
                        //    foreach (ElioUsersPersonCompanyTags tag in tags)
                        //    {
                        //        industries += tag.TagName + ", ";
                        //    }

                        //    industries = industries.Substring(0, industries.Length - 2);
                        //}
                    }
                }
            }
            else
                Response.Redirect(ControlLoader.PageDash404, false);
        }
        
        private void FixPage()
        {
            if (!IsPostBack)
            {
                divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                LoadData();
                UpdateStrings();
                SetLinks();
            }    

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
            
            //LblElioplusDashboard.Text = !string.IsNullOrEmpty(vSession.User.CompanyName) ? vSession.User.CompanyName + " dashboard" : (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName + " dashboard" : vSession.User.Username + " dashboard";
            //LblDashboard.Text = "Dashboard";

            if (aBtnGoPremium.Visible)
            {
                LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
                LblPricingPlan.Visible = false;
            }

            LblGoFull.Text = "Complete your registration";
            //LblDashPage.Text = "Home";
            //LblDashSubTitle.Text = "Account management";
            
            int isDeleted = 0;
            List<ElioUsersFavoritesIJUsers> favouritedCompanies = Sql.GetUserFavouritedCompaniesIJUsersByIsDeletedStatus(vSession.User.Id, isDeleted, session);

            divHomeInfo.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
            {
                LblHomeContent.Text = "Complete your registration from the link in the top right corner to start receiving leads, connections, send messages and much more.";
            }

            DataTable dt = Sql.GetCompanyViewsByCompanyIdForChart(vSession.User.Id, session);
            ArrayList dates = new ArrayList();
            ArrayList views = new ArrayList();
            dt.Columns.Add("datetime");

            foreach (DataRow row in dt.Rows)
            {
                row["datetime"] = string.Format("{0:dd/MM}", row["date"]);
                dates.Add(row["datetime"]);
                views.Add(row["views"]);
            }
            string json = new JavaScriptSerializer().Serialize(dates);
            //HiddenField1.Value = json;

            string json1 = new JavaScriptSerializer().Serialize(views);
            //HiddenField2.Value = json1;

            if ((Request.Browser.Cookies))
            {
                HttpCookie tutCookie = Request.Cookies[TutorialIsWatched];
                if (tutCookie == null)
                {
                    tutCookie = new HttpCookie(TutorialIsWatched);
                    tutCookie.Expires = DateTime.Now.AddDays(30);

                    tutCookie.Value = "1";

                    if (tutCookie.Value == "1")
                    {
                        Response.Cookies.Add(tutCookie);
                        ScriptManager.RegisterStartupScript(this, GetType(), "fireServerButtonEvent", "fireServerButtonEvent();", true);
                    }
                }
            }
        }

        private void SetLinks()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void UpdateStrings()
        {
            LblDashboard.Text = "Company Profile";
        }

        # endregion
    }
}