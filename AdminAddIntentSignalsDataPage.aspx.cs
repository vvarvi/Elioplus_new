using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Utils;
using System.Data;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus
{
    public partial class AdminAddIntentSignalsDataPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
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

        # region Methods

        private void FixPage()
        {            
            UpdateStrings();
            LoadCountries();
            
            UcMessageAlert.Visible = false;
        }

        private void UpdateStrings()
        {
            
        }

        private void LoadCountries()
        {
            RcbxCountries.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Select Country";

            RcbxCountries.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new ListItem();

                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                RcbxCountries.Items.Add(item);
            }

            RcbxCountries.SelectedValue = "0";
        }

        private void ResetFields(bool errorsOnly)
        {
            if (!errorsOnly)
            {
                RcbxType.SelectedValue = "0";
                RcbxType.SelectedIndex = -1;

                RcbxCountries.SelectedValue = "0";
                RcbxCountries.SelectedIndex = -1;

                TbxCity.Text = string.Empty;
                TbxProducts.Text = string.Empty;
                TbxUsersCount.Text = string.Empty;
            }

            LblTypeError.Text = string.Empty;            
            LblCountryError.Text = string.Empty;
            LblProductsError.Text = string.Empty;
            LblUsersCountError.Text = string.Empty;
            LblCityError.Text = string.Empty;
        }

        # endregion

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

                        if (vSession.User != null)
                        {
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgIntentData_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable table = Sql.GetIntentSignalsData(0, session);

                    if (table != null && table.Rows.Count > 0)
                    {
                        divData.Visible = true;
                        RdgIntentData.Visible = true;
                        UcMessageAlert.Visible = false;

                        RdgIntentData.DataSource = table;
                        RdgIntentData.DataBind();
                    }
                    else
                    {
                        divData.Visible = false;
                        RdgIntentData.Visible = false;

                        string alert = "There are no data";
                        GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Info, true, true, false, true, false);
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
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

        #region Combo

        #endregion

        #region Buttons

        protected void RbtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                ResetFields(true);

                string alert = string.Empty;

                #region Check Fields
                                
                if (RcbxType.SelectedItem.Value == "0")
                {
                    LblTypeError.Text = "You must select type";
                    return;
                }

                if (RcbxCountries.SelectedItem.Value == "0")
                {
                    LblCountryError.Text = "You must select country";
                    return;
                }

                if (TbxCity.Text == "")
                {
                    LblCityError.Text = "You must add city";
                    return;
                }

                if (TbxProducts.Text == "")
                {
                    LblProductsError.Text = "You must add product/technology";
                    return;
                }

                if (TbxUsersCount.Text == "")
                {
                    LblUsersCountError.Text = "You must add number of users";
                    return;
                }

                #endregion

                ElioIntentSignalsData data = new ElioIntentSignalsData();

                data.Type = RcbxType.SelectedItem.Text;
                data.Country = RcbxCountries.SelectedItem.Text;
                data.City = TbxCity.Text;
                data.Product = TbxProducts.Text;
                data.UsersCount = Convert.ToInt32(TbxUsersCount.Text);
                data.InsertByUserId = vSession.User.Id;
                data.DateInsert = DateTime.Now;
                data.LastUpdate = DateTime.Now;
                data.IsPublic = 1;

                DataLoader<ElioIntentSignalsData> loader = new DataLoader<ElioIntentSignalsData>(session);
                loader.Insert(data);

                session.CommitTransaction();

                ResetFields(false);

                RdgIntentData.DataBind();

                GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Success, true, true, false, true, false);
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

        protected void RbtnClear_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields(false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion
    }
}