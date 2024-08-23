using System;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Data;

namespace WdS.ElioPlus
{
    public partial class ChannelPartnerLocator : System.Web.UI.Page
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
                value = HttpContext.Current.Request.Url.Segments[1].TrimEnd('/');
                ViewState["VendorName"] = value;  //Regex.Replace(value, @"[^A-Za-z0-9]+", "_").Trim().ToLower();
            }
        }

        private int VendorID
        {
            get
            {
                object value = ViewState["VendorID"];
                return value != null ? Convert.ToInt32(value.ToString()) : -1;
            }
            set
            {
                ViewState["VendorID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadCountries();
                    LoadRepeater();

                    PageTitle();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void UpdateStrings()
        {
            //LblVendorPartnerLocatorTitle.Text = (!string.IsNullOrEmpty(VendorName)) ? VendorName + " Partner Locator" : "Partner Locator";
        }

        private void PageTitle()
        {
            PgTitle.Text = (!string.IsNullOrEmpty(VendorName)) ? VendorName + " partner locator" : "Partner Locator";
            MetaDescription = (!string.IsNullOrEmpty(VendorName)) ? "Find your " + VendorName + " channel partners by their country from partner locator" : "Find your channel partners from partner locator by theis country";
            MetaKeywords = (!string.IsNullOrEmpty(VendorName)) ? VendorName + " partner locator, channel partners country" : "Vendor's partner locator, channel partners country";
        }

        private void LoadCountries()
        {
            try
            {
                session.OpenConnection();

                if (VendorID == -1)
                    GetVendorFromUrl();

                if (VendorID > 0)
                {
                    DdlCountries.Items.Clear();


                    DdlCountries.DataSource = SqlCollaboration.GetVendorCollaborationPartnersLocatorCountriesTable(VendorID, session);
                    DdlCountries.DataTextField = "country_name";
                    DdlCountries.DataValueField = "id";

                    DdlCountries.DataBind();

                    DdlCountries.Items.Add(new ListItem("-- Select country --", "0"));

                    DdlCountries.Items.FindByValue("0").Selected = true;
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

        private void GetVendorFromUrl()
        {
            ElioUsers vendor = GlobalDBMethods.GetCompanyFromAbsoluteUrl(HttpContext.Current.Request.Url.AbsolutePath, session);
            if (vendor != null)
            {
                VendorID = vendor.Id;
                VendorName = vendor.CompanyName;
                LblVendorPartnerLocatorTitle.Text = vendor.CompanyName + " Partner Locator";
            }
            else
                Response.Redirect(ControlLoader.Default(), false);
            
            //Uri path = HttpContext.Current.Request.Url;
            //if (path != null)
            //{
            //    var pathSegs = path.Segments;
            //    if (pathSegs != null && pathSegs.Length > 2)
            //    {
            //        vendorId = Convert.ToInt32(pathSegs[2].TrimEnd('/'));
            //    }
            //}
        }

        protected void LoadRepeater()
        {
            try
            {
                session.OpenConnection();

                if (VendorID == -1)
                    GetVendorFromUrl();

                if (VendorID > 0)
                {
                    DataTable table = SqlCollaboration.GetVendorCollaborationPartnersLocatorTable(VendorID, tbxCompanyOrAddress.Text, (DdlCountries.SelectedItem.Value != "0") ? DdlCountries.SelectedItem.Text : "", session);

                    if (table != null && table.Rows.Count > 0)
                    {
                        UcMessageAlert.Visible = false;
                        Partners.Visible = true;
                        Partners.DataSource = table;
                        Partners.DataBind();
                    }
                    else
                    {
                        Partners.DataSource = null;
                        Partners.Visible = false;

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, (DdlCountries.SelectedItem.Value != "0") ? "You have no partners in " + DdlCountries.SelectedItem.Text : "There are no partners", Lib.Enums.MessageTypes.Warning, true, true, false);
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

        protected void aBtnSearch_OnClick(object sender, EventArgs e)
        {
            try
            {
                LoadRepeater();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadRepeater();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Partners_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                RepeaterItem item = (RepeaterItem)e.Item;

                //HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                //HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                //HiddenField hdnPartnerID = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnPartnerID");
                //if (hdnPartnerID != null)
                //{
                //    int partnerId = Convert.ToInt32(hdnPartnerID.Value);
                //    if (partnerId > 0)
                //    {
                //        ElioUsers partner = Sql.GetUserById(partnerId, session);
                //        if (partner != null)
                //        {
                //            if (aCompanyLogo != null && aCompanyName != null)
                //                aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.Profile(partner);
                //        }
                //    }
                //}
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