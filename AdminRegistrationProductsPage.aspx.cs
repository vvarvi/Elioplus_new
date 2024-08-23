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
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.StripePayment;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.UI;

namespace WdS.ElioPlus
{
    public partial class AdminRegistrationProductsPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        #region Page ViewState properties

        public string SearchQueryString
        {
            get
            {
                return (this.ViewState["SearchQueryString"] != null) ? ViewState["SearchQueryString"].ToString() : string.Empty;
            }

            set { this.ViewState["SearchQueryString"] = value; }
        }

        public string ProductDescription
        {
            get
            {
                return (this.ViewState["ProductDescription"] != null) ? ViewState["ProductDescription"].ToString() : string.Empty;
            }

            set { this.ViewState["ProductDescription"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session))
                {
                   
                    if (!IsPostBack)
                    {
                        FixPage();
                    }
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
            UcMessage.Visible = false;
            UcMessageAlert.Visible = false;
            TbxProductDescription.Text = "";
            TbxIsPublic.Text = "1";
        }

        private void LoadData()
        {
            string orderBy = "";

            if (DrpSortList.SelectedValue == "0")
            {
                orderBy = "";
            }
            else if (DrpSortList.SelectedValue == "1")
            {
                orderBy = " description asc ";
            }
            else if (DrpSortList.SelectedValue == "2")
            {
                orderBy = " description desc ";
            }
            else if (DrpSortList.SelectedValue == "3")
            {
                orderBy = " sysdate asc ";
            }
            else if (DrpSortList.SelectedValue == "4")
            {
                orderBy = " sysdate desc ";
            }

            DataTable table = Sql.GetRegistrationProductsTbl(TbxDescriptionSearch.Text, orderBy, session);

            if (table != null && table.Rows.Count > 0)
            {
                UcMessageAlert.Visible = false;
                divData.Visible = true;
                RdgRegProducts.Visible = true;

                RdgRegProducts.DataSource = table;
                //RdgRegProducts.Rebind();
            }
            else
            {
                //divData.Visible = false;
                RdgRegProducts.Visible = false;
                string alert = "There are no data";
                GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Info, true, true, false, true, false);
            }
        }

        private void FixFields(GridDataItem item, bool isEditMode)
        {
            if (item != null)
            {
                Label lblDescription = (Label)ControlFinder.FindControlRecursive(item, "LblDescription");
                Label lblIsPublic = (Label)ControlFinder.FindControlRecursive(item, "LblIsPublic");

                TextBox tbxDescription = (TextBox)ControlFinder.FindControlRecursive(item, "TbxDescription");
                TextBox tbxIsPublic = (TextBox)ControlFinder.FindControlRecursive(item, "TbxIsPublic");

                ImageButton imgBtnEditProduct = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEditProduct");
                ImageButton imgBtnCancelProduct = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancelProduct");
                ImageButton imgBtnUpdateProduct = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnUpdateProduct");

                lblDescription.Visible = !isEditMode;
                lblIsPublic.Visible = !isEditMode;
                imgBtnEditProduct.Visible = !isEditMode;

                tbxDescription.Visible = isEditMode;
                tbxIsPublic.Visible = isEditMode;

                imgBtnCancelProduct.Visible = isEditMode;
                imgBtnUpdateProduct.Visible = isEditMode;
            }
        }

        #endregion

        #region Grids

        protected void RdgRegProducts_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent Items

                    GridDataItem item = (GridDataItem)e.Item;

                    ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                    if (user != null)
                    {
                        
                    }

                    #endregion
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

        protected void RdgRegProducts_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LoadData();
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

        protected void RbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                bool inserted = true;

                if (string.IsNullOrEmpty(TbxProductDescription.Text))
                {
                    GlobalMethods.ShowMessageControlDA(UcMessage, "Please fill product description!", MessageTypes.Warning, true, true, false, false, false);
                    return;
                }
                else
                {
                    if (Validations.ContainsSpecialCharForRegProducts(TbxProductDescription.Text))
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessage, "Product description contains invalid characters!", MessageTypes.Warning, true, true, false, false, false);
                        return;
                    }
                }

                if (string.IsNullOrEmpty(TbxIsPublic.Text.Trim()))
                {
                    GlobalMethods.ShowMessageControlDA(UcMessage, "Please fill product public/not public status!", MessageTypes.Warning, true, true, false, false, false);
                    return;
                }
                else
                {
                    if (TbxIsPublic.Text.Trim() != "0" && TbxIsPublic.Text.Trim() != "1")
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessage, "Public status must have 0 or 1 for value!", MessageTypes.Warning, true, true, false, false, false);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(TbxProductDescription.Text))
                    inserted = GlobalDBMethods.InsertProduct(TbxProductDescription.Text.Trim(), Convert.ToInt32(TbxIsPublic.Text), session);

                if (!inserted)
                {
                    GlobalMethods.ShowMessageControlDA(UcMessage, "Registration product was not inserted!", MessageTypes.Warning, true, true, false, false, false);
                    return;
                }
                else
                {
                    FixPage();

                    GlobalMethods.ShowMessageControlDA(UcMessage, "New registration product inserted successfully!", MessageTypes.Success, true, true, false, false, false);
                    RdgRegProducts.Rebind();
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

        protected void RbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                FixPage();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                RdgRegProducts.Rebind();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnEditProduct_Click(object sender, System.Web.UI.ImageClickEventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    ImageButton imgBtn = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                    FixFields(item, true);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnCancelProduct_Click(object sender, System.Web.UI.ImageClickEventArgs args)
        {
            try
            {
                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                FixFields(item, false);

                BtnDeleteConfirm.Visible = true;
                BtnProceedMerge.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnUpdateProduct_Click(object sender, System.Web.UI.ImageClickEventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                BtnProceedMerge.Visible = false;

                if (item != null)
                {
                    Label lblDescription = (Label)ControlFinder.FindControlRecursive(item, "LblDescription");
                    TextBox tbxDescription = (TextBox)ControlFinder.FindControlRecursive(item, "TbxDescription");

                    if (tbxDescription.Text != "")
                    {
                        if (tbxDescription.Text == lblDescription.Text)
                        {
                            FixFields(item, false);

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "No change to Product description was made!", MessageTypes.Success, true, true, false, false, false);
                            return;
                        }

                        DataTable table = session.GetDataTable(@"select COUNT(id) as count 
                                                        from Elio_registration_products
                                                        where id != @id AND description = @description"
                                            , DatabaseHelper.CreateIntParameter("@id", Convert.ToInt32(item["id"].Text))
                                            , DatabaseHelper.CreateStringParameter("@description", tbxDescription.Text));

                        bool existDescription = Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
                        if (existDescription)
                        {
                            if (Convert.ToInt32(item["id"].Text) > 0)
                            {
                                HdnId.Value = item["id"].Text;
                                ProductDescription = tbxDescription.Text;
                                // exist same description to other ID
                                LblConfMsg.Text = "Product description could not be updated because the same description exist to other " + table.Rows[0]["count"].ToString() + " product/s. If you want to merge all them with this, press Merge!";
                                BtnDeleteConfirm.Visible = false;
                                BtnProceedMerge.Visible = true;
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfDeleteUserPopUp();", true);

                                return;
                            }
                        }

                        int row = session.ExecuteQuery(@"Update Elio_registration_products SET description = @description WHERE id = @id"
                                        , DatabaseHelper.CreateIntParameter("@id", Convert.ToInt32(item["id"].Text))
                                        , DatabaseHelper.CreateStringParameter("@description", tbxDescription.Text.Trim()));

                        if (row > 0)
                        {
                            lblDescription.Text = tbxDescription.Text;
                        }
                        else
                        {
                            // error no update
                            LblConfMsg.Text = "Product description could not be updated";
                            BtnDeleteConfirm.Visible = false;
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfDeleteUserPopUp();", true);

                            return;
                        }
                    }
                    else
                    {
                        LblConfMsg.Text = "Please fill Product description";
                        BtnDeleteConfirm.Visible = false;
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfDeleteUserPopUp();", true);

                        return;
                    }

                    FixFields(item, false);

                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Product updated successfully!", MessageTypes.Success, true, true, false, false, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Warning, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void ImgBtnDeleteProduct_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                if (vSession.User != null)
                {
                    DataTable table = session.GetDataTable(@"select COUNT(id) as count 
                                                        from Elio_users_registration_products
                                                        where reg_products_id = @reg_products_id"
                                                , DatabaseHelper.CreateIntParameter("@reg_products_id", Convert.ToInt32(item["id"].Text)));

                    bool existToUsers = Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
                    if (existToUsers)
                    {
                        LblConfMsg.Text = "This product belong to " + Convert.ToInt32(table.Rows[0]["count"]) + " user/s. Are you sure to delete it anyway?";
                    }
                    else
                    {
                        LblConfMsg.Text = "This product belong to no user. Are you sure to delete it?";
                    }

                    BtnDeleteConfirm.Visible = true;
                    BtnProceedMerge.Visible = false;

                    if (item != null)
                    {
                        HdnId.Value = "0";

                        if (Convert.ToInt32(item["id"].Text) > 0)
                        {
                            HdnId.Value = item["id"].Text;

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfDeleteUserPopUp();", true);
                        }
                        else
                        {
                            //error no id found

                            LblConfMsg.Text = "Product could not be found for delete";
                            BtnDeleteConfirm.Visible = false;
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfDeleteUserPopUp();", true);
                        }
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

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            UcMessageAlert.Visible = false;
            HdnId.Value = "0";
            ProductDescription = "";

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfDeleteUserPopUp();", true);
        }

        protected void BtnDeleteConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;

                if (vSession.User != null)
                {
                    if (!string.IsNullOrEmpty(HdnId.Value) && Convert.ToInt32(HdnId.Value) > 0)
                    {
                        int row = Sql.DeleteUserRegistrationProductAndProduct(Convert.ToInt32(HdnId.Value), session);
                        if (row > 0)
                        {
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfDeleteUserPopUp();", true);
                            HdnId.Value = "0";

                            RdgRegProducts.Rebind();

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Product deleted successfully and from any user who had it!", MessageTypes.Success, true, true, false);
                        }
                        else
                        {
                            #region Product not deleted

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Product could not be deleted! Please try again later.", MessageTypes.Warning, true, true, false);

                            #endregion
                        }
                    }
                    else
                    {
                        #region Product could not be find for delete

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Product could not be found for delete", MessageTypes.Warning, true, true, false);

                        #endregion
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Something went wrong. Please contact Admin.", MessageTypes.Error, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnProceedMerge_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;

                if (ProductDescription != "" && Convert.ToInt32(HdnId.Value) > 0)
                {
                    DataTable table = session.GetDataTable(@"SELECT *
                                                        FROM Elio_registration_products
                                                        WHERE id != @id AND description = @description"
                                            , DatabaseHelper.CreateIntParameter("@id", Convert.ToInt32(HdnId.Value))
                                            , DatabaseHelper.CreateStringParameter("@description", ProductDescription));

                    if (table != null && table.Rows.Count > 0)
                    {
                        session.BeginTransaction();

                        foreach (DataRow row in table.Rows)
                        {
                            if (Convert.ToInt32(row["id"]) > 0)
                            {
                                int ok = session.ExecuteQuery(@"UPDATE Elio_users_registration_products
                                                                    SET reg_products_id = " + Convert.ToInt32(HdnId.Value) + " " +
                                                                "WHERE reg_products_id = @reg_products_id"
                                                            , DatabaseHelper.CreateIntParameter("@reg_products_id", Convert.ToInt32(row["id"])));

                                if (ok < 0)
                                {
                                    throw new Exception("Something went wrong. Please try again later!");
                                }
                                else
                                {
                                    int del = session.ExecuteQuery(@"DELETE FROM Elio_registration_products
                                                                    WHERE id = @id"
                                                            , DatabaseHelper.CreateIntParameter("@id", Convert.ToInt32(row["id"])));

                                    if (del < 0)
                                    {
                                        throw new Exception("Something went wrong. Please try again later!");
                                    }
                                }
                            }
                        }

                        session.CommitTransaction();

                        int upd = session.ExecuteQuery(@"UPDATE Elio_registration_products
                                                            SET description = @description
                                                        WHERE id = @id"
                                                , DatabaseHelper.CreateIntParameter("@id", Convert.ToInt32(HdnId.Value))
                                                , DatabaseHelper.CreateStringParameter("@description", ProductDescription));

                        if (upd < 0)
                        {
                            throw new Exception("Something went wrong. Please try again later!");
                        }
                        else
                        {
                            UcMessageAlert.Visible = false;
                            HdnId.Value = "0";
                            ProductDescription = "";

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfDeleteUserPopUp();", true);

                            RdgRegProducts.Rebind();

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Product updated and merged successfully!", MessageTypes.Success, true, true, false, true, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Warning, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion

        #region DropDownLists

        protected void DrpSortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RdgRegProducts.Rebind();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}