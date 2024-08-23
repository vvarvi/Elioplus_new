using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using static WdS.ElioPlus.PartnerPortalLocator;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.pages
{
    public partial class PartnerPortalLocatorPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        #region Properties

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    FixPage();
                    SetLinks();
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
            UcMessageAlert.Visible = false;
            //aGetStarted.Visible = vSession.User == null;
        }

        private void SetLinks()
        {
            //aGetStarted.HRef = ControlLoader.SignUpPrm;
        }

        public IList<ElioUsersInfo> UserList()
        {
            IList<ElioUsersInfo> list;

            if (ViewState["UserList"] != null)
                list = ViewState["UserList"] as IList<ElioUsersInfo>;
            else
            {
                list = new List<ElioUsersInfo>();

                session.OpenConnection();
                List<ElioUsers> vendors = new List<ElioUsers>();
                vendors = Sql.GetPublicVendors(tbxCompany.Text, session);

                foreach (ElioUsers vendor in vendors)
                {
                    list.Add(new ElioUsersInfo()
                    {
                        Id = vendor.Id,
                        CompanyLogo = vendor.CompanyLogo,
                        CompanyName = vendor.CompanyName,
                        Description = vendor.Description,
                        Country = vendor.Country,
                        City = vendor.City,
                        Address = vendor.Address,
                        WebSite = vendor.WebSite,
                        Profile = ControlLoader.Profile(vendor),
                        Industries = Sql.GetIndustiesDescriptionsIJUserIndustriesAsString(vendor.Id, session),
                        PortalLoginUrl = ControlLoader.LoginPartner.Replace("{CompanyName}", Regex.Replace(vendor.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower())
                    });
                }

                ViewState["UserList"] = list;
            }

            session.CloseConnection();

            if (list == null)
                return new List<ElioUsersInfo>();

            if (list != null && list.Count < 1)
            {
                GlobalMethods.ShowMessageControl(UcMessageAlert, "There are no available results for this name", MessageTypes.Warning, true, true, false, true, false);
            }
            else
                UcMessageAlert.Visible = false;

            if (list != null && list.Count > 10)
                navBtns.Visible = navBtnsBottom.Visible = true;
            else
                navBtns.Visible = navBtnsBottom.Visible = false;

            return list;
        }

        protected void LoadRepeater(Navigation navigation)
        {
            try
            {
                //Create the object of PagedDataSource
                PagedDataSource objPds = new PagedDataSource();

                //Assign our data source to PagedDataSource object
                objPds.DataSource = UserList();

                //Set the allow paging to true
                objPds.AllowPaging = true;

                //Set the number of items you want to show
                objPds.PageSize = 30;

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

                // Disable Prev, Next, First, Last buttons if necessary
                lbtnPrev.Visible = lbtnPrevBottom.Visible = !objPds.IsFirstPage;
                lbtnNext.Visible = lbtnNextBottom.Visible = !objPds.IsLastPage;

                //Assign PagedDataSource to repeater
                Partners.DataSource = objPds;
                Partners.DataBind();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {

            }
        }

        #endregion

        #region Buttons

        protected void lbtnPrev_Click(object sender, EventArgs e)
        {
            LoadRepeater(Navigation.Previous);
        }

        protected void lbtnNext_Click(object sender, EventArgs e)
        {
            LoadRepeater(Navigation.Next);
        }

        protected void BtnSearch_OnClick(object sender, EventArgs e)
        {
            ViewState["UserList"] = null;
            if (tbxCompany.Text.Trim().Length < 2)
            {
                GlobalMethods.ShowMessageControl(UcMessageAlert, "Name is too short! Try at least 2 characters", MessageTypes.Warning, true, true, false, true, false);
               
                navBtns.Visible = navBtnsBottom.Visible = false;
                Partners.DataSource = null;
                Partners.DataBind();
            }
            else
            {
                UcMessageAlert.Visible = false;
                LoadRepeater(Navigation.None);
            }
        }

        #endregion

        protected void Partners_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Reference the Repeater Item.
                    RepeaterItem item = e.Item;

                    string companyId = (item.FindControl("HdnCompanyID") as HiddenField).Value;

                    if (companyId != "" && companyId != "0")
                    {
                        ElioUsers vendor = Sql.GetUserById(Convert.ToInt32(companyId), session);
                        if (vendor != null)
                        {
                            Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                            imgCompanyLogo.AlternateText = vendor.CompanyName + " logo";
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
    }
}