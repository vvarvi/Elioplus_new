using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus
{
    public partial class ChannelPartnerLocatorWithMaster : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadCountries();
                    LoadRepeater();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void LoadCountries()
        {
            try
            {
                session.OpenConnection();

                int vendorId = GetVendorFromUrl();

                if (vendorId > 0)
                {
                    DdlCountries.Items.Clear();


                    DdlCountries.DataSource = SqlCollaboration.GetVendorCollaborationPartnersLocatorCountriesTable(vendorId, session);
                    DdlCountries.DataTextField = "country_name";
                    DdlCountries.DataValueField = "id";

                    DdlCountries.DataBind();

                    DdlCountries.Items.Add(new ListItem("-- Select --", "0"));

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

        private int GetVendorFromUrl()
        {
            int vendorId = -1;

            Uri path = HttpContext.Current.Request.Url;
            if (path != null)
            {
                var pathSegs = path.Segments;
                if (pathSegs != null && pathSegs.Length > 2)
                {
                    vendorId = Convert.ToInt32(pathSegs[2].TrimEnd('/'));
                }
            }

            return vendorId;
        }

        protected void LoadRepeater()
        {
            try
            {
                session.OpenConnection();

                int vendorId = GetVendorFromUrl();

                if (vendorId > 0)
                {
                    DataTable table = SqlCollaboration.GetVendorCollaborationPartnersLocatorTable(vendorId, tbxCompanyOrAddress.Text, (DdlCountries.SelectedItem.Value != "0") ? DdlCountries.SelectedItem.Text : "", session);

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

                        GlobalMethods.ShowMessageControl(UcMessageAlert, (DdlCountries.SelectedItem.Value != "0") ? "You have no partners in " + DdlCountries.SelectedItem.Text : "There are no partners", Lib.Enums.MessageTypes.Warning, true, true, false);
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