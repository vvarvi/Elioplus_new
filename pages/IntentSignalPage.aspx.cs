using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web.UI;

namespace WdS.ElioPlus.pages
{
    public partial class IntentSignalPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        public int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] != null)
                    return Convert.ToInt32(ViewState["PageIndex"].ToString());
                else
                    return 1;
            }
            set
            {
                if (value > 1)
                    ViewState["PageIndex"] = value;
                else
                    ViewState["PageIndex"] = 1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                //scriptManager.RegisterPostBackControl(aLoadLess);
                //scriptManager.RegisterPostBackControl(aLoadMore);

                if (!IsPostBack)
                {
                    PageIndex = 1;
                    FixPage();                    
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

        private void UpdateStrings()
        {
            Label1.Text = "Get access to ";
            LblLeadsCount.Text = Sql.GetSnitcherRFPsLeadsCount(38679, "-1", 1, session).ToString();
            Label2.Text = " more active leads on our platform today";
        }

        private void FixPage()
        {
            SetLinks();
            UpdateStrings();

            aLoadLess.Visible = false;  // PageIndex > 1;
        }

        private void SetLinks()
        {
            
        }

        private void LoadData(DBSession session)
        {
            aLoadLess.Visible = false;  // PageIndex > 1;

            DataTable table = Sql.GetIntentSignalsAndRFQsDataTop30(PageIndex, "", "", session);

            if (table != null && table.Rows.Count > 0)
            {
                aLoadMore.Visible = false;
                RdgIntentData.Visible = true;
                UcMessageDataAlert.Visible = false;

                RdgIntentData.DataSource = table;
                RdgIntentData.DataBind();
            }
            else
            {
                RdgIntentData.DataSource = null;
                RdgIntentData.Visible = false;
                aLoadMore.Visible = false;

                GlobalMethods.ShowMessageControl(UcMessageDataAlert, "There are no data", MessageTypes.Info, true, true, false, false, false);

            }
        }

        #endregion

        #region Grids

        protected void RdgIntentData_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;
                        if (row != null)
                        {
                            Label lblProducts = (Label)ControlFinder.FindControlRecursive(item, "LblProducts");

                            string itemProducts = row["product"].ToString();

                            if (itemProducts != "" && itemProducts.EndsWith(","))
                                itemProducts = itemProducts.Substring(0, itemProducts.Length - 1);

                            string[] products = itemProducts.Split(',');

                            if (products[0] != "")
                                lblProducts.Text = products[0].TrimEnd(',');

                            if (products.Length > 2)
                            {
                                Image imgInfo = (Image)ControlFinder.FindControlRecursive(item, "ImgInfo");
                                RadToolTip rttImgInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttImgInfo");

                                imgInfo.Visible = true;
                                rttImgInfo.Text = itemProducts.TrimEnd(',');

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgIntentData_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                    LoadData(session);
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

        protected void aLoadNext_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();
                ++PageIndex;
                LoadData(session);
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

        protected void aLoadLess_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();
                --PageIndex;
                LoadData(session);
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