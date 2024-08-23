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
using System.IO;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Controls.AlertControls;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;
using System.Configuration;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.Roles;
using WdS.ElioPlus.Lib.Roles.EnumsRoles;
using System.Drawing;
using WdS.ElioPlus.Controls.Dashboard.AlertControls;

namespace WdS.ElioPlus
{
    public partial class DashboardPartnerOnboardingPageNew : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        //public const int CustomUSER = 39132;

        public string FileNameToDelete
        {
            get
            {
                if (ViewState["FileNameToDelete"] != null)
                    return ViewState["FileNameToDelete"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["FileNameToDelete"] = value;
            }
        }

        public bool IsCustomVendor
        {
            get
            {
                if (ViewState["IsCustomVendor"] != null)
                    return (bool)ViewState["IsCustomVendor"];
                else
                    return false;
            }
            set
            {
                ViewState["IsCustomVendor"] = value;
            }
        }

        public int FileIdToDelete
        {
            get
            {
                if (ViewState["FileIdToDelete"] != null)
                    return Convert.ToInt32(ViewState["FileIdToDelete"].ToString());
                else
                    return -1;
            }
            set
            {
                ViewState["FileIdToDelete"] = value;
            }
        }

        public int FileCategoryIdToDelete
        {
            get
            {
                if (ViewState["FileCategoryIdToDelete"] != null)
                    return Convert.ToInt32(ViewState["FileCategoryIdToDelete"].ToString());
                else
                    return -1;
            }
            set
            {
                ViewState["FileCategoryIdToDelete"] = value;
            }
        }

        public int FileTypeIdToDelete
        {
            get
            {
                if (ViewState["FileTypeIdToDelete"] != null)
                    return Convert.ToInt32(ViewState["FileTypeIdToDelete"].ToString());
                else
                    return -1;
            }
            set
            {
                ViewState["FileTypeIdToDelete"] = value;
            }
        }

        public bool IsEditMode
        {
            get
            {
                if (ViewState["IsEditMode"] != null)
                    return (bool)ViewState["IsEditMode"];
                else
                    return false;
            }
            set
            {
                ViewState["IsEditMode"] = value;
            }
        }

        public string Key
        {
            get
            {
                if (ViewState["Key"] != null)
                    return ViewState["Key"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["Key"] = value;
            }
        }

        public int CategoryIdView
        {
            get
            {
                if (ViewState["CategoryIdView"] != null)
                    return Convert.ToInt32(ViewState["CategoryIdView"].ToString());
                else
                    return -1;
            }
            set
            {
                ViewState["CategoryIdView"] = value;
            }
        }

        public bool IsSendFiles
        {
            get
            {
                if (ViewState["IsSendFiles"] != null)
                    return (bool)ViewState["IsSendFiles"];
                else
                    return true;
            }
            set
            {
                ViewState["IsSendFiles"] = value;
            }
        }

        public int LibraryFileId
        {
            get
            {
                if (ViewState["LibraryFileId"] != null)
                    return Convert.ToInt32(ViewState["LibraryFileId"].ToString());
                else
                    return -1;
            }
            set
            {
                ViewState["LibraryFileId"] = value;
            }
        }

        public string LibraryFileName
        {
            get
            {
                if (ViewState["LibraryFileName"] != null)
                    return ViewState["LibraryFileName"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["LibraryFileName"] = value;
            }
        }

        public string LibraryPreviewFileName
        {
            get
            {
                if (ViewState["LibraryPreviewFileName"] != null)
                    return ViewState["LibraryPreviewFileName"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["LibraryPreviewFileName"] = value;
            }
        }

        public int LibraryFileCategoryId
        {
            get
            {
                if (ViewState["LibraryFileCategoryId"] != null)
                    return Convert.ToInt32(ViewState["LibraryFileCategoryId"].ToString());
                else
                    return -1;
            }
            set
            {
                ViewState["LibraryFileCategoryId"] = value;
            }
        }

        public int UploadedFilesCount
        {
            get
            {
                if (ViewState["UploadedFilesCount"] != null)
                    return Convert.ToInt32(ViewState["UploadedFilesCount"].ToString());
                else
                    return 0;
            }
            set
            {
                ViewState["UploadedFilesCount"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    //scriptManager.RegisterPostBackControl(BtnUploadFile);
                                        
                    RadAsyncUpload1.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(RadAsyncUpload1_FileUploaded);
                    RadAsyncUpload2.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(RadAsyncUpload2_FileUploaded);
                    RadAsyncUpload3.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(RadAsyncUpload3_FileUploaded);
                    RadAsyncUpload4.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(RadAsyncUpload4_FileUploaded);
                    RadAsyncUpload5.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(RadAsyncUpload5_FileUploaded);
                    RadAsyncUpload6.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(RadAsyncUpload6_FileUploaded);
                    RadAsyncUpload7.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(RadAsyncUpload7_FileUploaded);
                    RadAsyncUpload8.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(RadAsyncUpload8_FileUploaded);
                    RadAsyncUpload9.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(RadAsyncUpload9_FileUploaded);
                    RadAsyncUpload10.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(RadAsyncUpload10_FileUploaded);

                    RadAsyncUpload1.PostbackTriggers = new string[] { "BtnUploadFile" };
                    RadAsyncUpload2.PostbackTriggers = new string[] { "BtnUploadFile" };
                    RadAsyncUpload3.PostbackTriggers = new string[] { "BtnUploadFile" };
                    RadAsyncUpload4.PostbackTriggers = new string[] { "BtnUploadFile" };
                    RadAsyncUpload5.PostbackTriggers = new string[] { "BtnUploadFile" };
                    RadAsyncUpload6.PostbackTriggers = new string[] { "BtnUploadFile" };
                    RadAsyncUpload7.PostbackTriggers = new string[] { "BtnUploadFile" };
                    RadAsyncUpload8.PostbackTriggers = new string[] { "BtnUploadFile" };
                    RadAsyncUpload9.PostbackTriggers = new string[] { "BtnUploadFile" };
                    RadAsyncUpload10.PostbackTriggers = new string[] { "BtnUploadFile" };

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, out key, session);

                    if (isError)
                    {
                        Response.Redirect(errorPage, false);
                        return;
                    }

                    bool hasRight = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardCollaborationLibraryPage", Actions.View, session);
                    if (!hasRight)
                    {
                        Response.Redirect(ControlLoader.PageDash405, false);
                        return;
                    }

                    if (!IsPostBack)
                    {
                        FixPage();
                        UploadedFilesCount = 0;
                    }

                    FixCategoriesItems();
                    FixCategoryFilesItems();
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

        #region Upload Files Controls

        protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (divFileUpload.Visible && RadAsyncUpload1.UploadedFiles[0].ContentLength > 0)
                {
                    if (Ddlcategory.SelectedValue == "0")
                    {
                        e.IsValid = false;
                        return;
                    }

                    var liItem = new HtmlGenericControl("li");
                    liItem.InnerText = e.File.FileName;
                    
                    bool successUpload = UploadLibraryFile(e, RadAsyncUpload1.UploadedFiles[0].ContentType, Ddlcategory);       //UpLoadImage.UpLoadLibraryFile(serverMapPathTargetFolderMine, e, out fileName, session);
                    e.IsValid = successUpload;

                    if (e.IsValid)
                        UploadedFilesCount++;
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

        protected void RadAsyncUpload2_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (divFileUpload2.Visible && RadAsyncUpload2.UploadedFiles[0].ContentLength > 0)
                {
                    if (Ddlcategory2.SelectedValue == "0")
                    {
                        e.IsValid = false;
                        return;
                    }

                    var liItem = new HtmlGenericControl("li");
                    liItem.InnerText = e.File.FileName;

                    bool successUpload = UploadLibraryFile(e, RadAsyncUpload2.UploadedFiles[0].ContentType, Ddlcategory2);
                    e.IsValid = successUpload;

                    if (e.IsValid)
                        UploadedFilesCount++;
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

        protected void RadAsyncUpload3_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (divFileUpload3.Visible && RadAsyncUpload3.UploadedFiles[0].ContentLength > 0)
                {
                    if (Ddlcategory3.SelectedValue == "0")
                    {
                        e.IsValid = false;
                        return;
                    }

                    var liItem = new HtmlGenericControl("li");
                    liItem.InnerText = e.File.FileName;

                    bool successUpload = UploadLibraryFile(e, RadAsyncUpload3.UploadedFiles[0].ContentType, Ddlcategory3);
                    e.IsValid = successUpload;

                    if (e.IsValid)
                        UploadedFilesCount++;
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

        protected void RadAsyncUpload4_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (divFileUpload4.Visible && RadAsyncUpload4.UploadedFiles[0].ContentLength > 0)
                {
                    if (Ddlcategory4.SelectedValue == "0")
                    {
                        e.IsValid = false;
                        return;
                    }

                    var liItem = new HtmlGenericControl("li");
                    liItem.InnerText = e.File.FileName;

                    bool successUpload = UploadLibraryFile(e, RadAsyncUpload4.UploadedFiles[0].ContentType, Ddlcategory4);
                    e.IsValid = successUpload;

                    if (e.IsValid)
                        UploadedFilesCount++;
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

        protected void RadAsyncUpload5_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (divFileUpload5.Visible && RadAsyncUpload5.UploadedFiles[0].ContentLength > 0)
                {
                    if (Ddlcategory5.SelectedValue == "0")
                    {
                        e.IsValid = false;
                        return;
                    }

                    var liItem = new HtmlGenericControl("li");
                    liItem.InnerText = e.File.FileName;

                    bool successUpload = UploadLibraryFile(e, RadAsyncUpload5.UploadedFiles[0].ContentType, Ddlcategory5);
                    e.IsValid = successUpload;

                    if (e.IsValid)
                        UploadedFilesCount++;
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

        protected void RadAsyncUpload6_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (divFileUpload6.Visible && RadAsyncUpload6.UploadedFiles[0].ContentLength > 0)
                {
                    if (Ddlcategory6.SelectedValue == "0")
                    {
                        e.IsValid = false;
                        return;
                    }

                    var liItem = new HtmlGenericControl("li");
                    liItem.InnerText = e.File.FileName;

                    bool successUpload = UploadLibraryFile(e, RadAsyncUpload6.UploadedFiles[0].ContentType, Ddlcategory6);
                    e.IsValid = successUpload;

                    if (e.IsValid)
                        UploadedFilesCount++;
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

        protected void RadAsyncUpload7_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (divFileUpload7.Visible && RadAsyncUpload7.UploadedFiles[0].ContentLength > 0)
                {
                    if (Ddlcategory7.SelectedValue == "0")
                    {
                        e.IsValid = false;
                        return;
                    }

                    var liItem = new HtmlGenericControl("li");
                    liItem.InnerText = e.File.FileName;

                    bool successUpload = UploadLibraryFile(e, RadAsyncUpload7.UploadedFiles[0].ContentType, Ddlcategory7);
                    e.IsValid = successUpload;

                    if (e.IsValid)
                        UploadedFilesCount++;
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

        protected void RadAsyncUpload8_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (divFileUpload8.Visible && RadAsyncUpload8.UploadedFiles[0].ContentLength > 0)
                {
                    if (Ddlcategory8.SelectedValue == "0")
                    {
                        e.IsValid = false;
                        return;
                    }

                    var liItem = new HtmlGenericControl("li");
                    liItem.InnerText = e.File.FileName;

                    bool successUpload = UploadLibraryFile(e, RadAsyncUpload8.UploadedFiles[0].ContentType, Ddlcategory8);
                    e.IsValid = successUpload;

                    if (e.IsValid)
                        UploadedFilesCount++;
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

        protected void RadAsyncUpload9_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (divFileUpload9.Visible && RadAsyncUpload9.UploadedFiles[0].ContentLength > 0)
                {
                    if (Ddlcategory9.SelectedValue == "0")
                    {
                        e.IsValid = false;
                        return;
                    }

                    var liItem = new HtmlGenericControl("li");
                    liItem.InnerText = e.File.FileName;

                    bool successUpload = UploadLibraryFile(e, RadAsyncUpload9.UploadedFiles[0].ContentType, Ddlcategory9);
                    e.IsValid = successUpload;

                    if (e.IsValid)
                        UploadedFilesCount++;
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

        protected void RadAsyncUpload10_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (divFileUpload10.Visible && RadAsyncUpload10.UploadedFiles[0].ContentLength > 0)
                {
                    if (Ddlcategory10.SelectedValue == "0")
                    {
                        e.IsValid = false;
                        return;
                    }

                    var liItem = new HtmlGenericControl("li");
                    liItem.InnerText = e.File.FileName;

                    bool successUpload = UploadLibraryFile(e, RadAsyncUpload10.UploadedFiles[0].ContentType, Ddlcategory10);
                    e.IsValid = successUpload;

                    if (e.IsValid)
                        UploadedFilesCount++;
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

        #region Methods

        private void FixPage()
        {
            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                divVendorsList.Visible = true;
                GetCollaborationVendors();
            }
            else
            {
                divVendorsList.Visible = false;
            }

            UpdateStrings();
            SetLinks();
            FixTabMenu();
            //FillPartnersList();

            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                BtnSendFile.Visible = BtnUploadFile.Visible = false;
                bool hasMessages = false;

                bool hasConfirmedInvitationPartners = SqlCollaboration.HasCollaborationPartnersOrMessages(vSession.User.Id, vSession.User.CompanyType, out hasMessages, session);
                if (!hasConfirmedInvitationPartners)
                {
                    aInvitationToPartners.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-new-partners");
                    divInvitationToPartners.Visible = true;
                }
                else
                {
                    divInvitationToPartners.Visible = false;
                }

                divNaviItem.Visible = false;
            }
            else
            {
                divNaviItem.Visible = true;
                divInvitationToPartners.Visible = false;

                BtnSendFile.Visible = BtnUploadFile.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardPartnerOnboardingPage", Actions.Upload, session);
            }

            UcMessageAlert.Visible = false;
            UploadMessageAlert.Visible = false;
            vSession.VendorsResellersList.Clear();
            vSession.VendorsResellersList = null;

            IsEditMode = false;
            IsSendFiles = true;
            LibraryFileId = -1;
            LibraryFileCategoryId = -1;
            LibraryPreviewFileName = "";
            LibraryFileName = "";
        }

        private void GetCollaborationVendors()
        {
            List<ElioUsers> vendors = SqlCollaboration.GetCollaborationVendorsByResellerUserId(vSession.User.Id, CollaborateInvitationStatus.Confirmed.ToString(), session);
            if (vendors.Count > 0)
            {
                divVendorsList.Visible = true;

                DrpVendors.Items.Clear();

                foreach (ElioUsers vendor in vendors)
                {
                    ListItem item = new ListItem();
                    item.Value = vendor.Id.ToString();
                    item.Text = vendor.CompanyName;

                    DrpVendors.Items.Add(item);

                    //if (vendor.Id == CustomUSER)
                    //    IsCustomVendor = true;
                }

                DrpVendors.Items.FindByValue(vendors[0].Id.ToString()).Selected = true;
                DrpVendors.SelectedItem.Value = vendors[0].Id.ToString();
                DrpVendors.SelectedItem.Text = vendors[0].CompanyName;

                DrpVendors.Enabled = (vendors.Count == 1) ? false : true;

            }
            else
            {
                divVendorsList.Visible = false;
            }
        }

        private void FixCategoriesItems()
        {
            bool isClose = false;

            try
            {
                if (session.Connection.State == ConnectionState.Closed)
                {
                    session.OpenConnection();
                    isClose = true;
                }

                foreach (RepeaterItem item in RdgCategories.Items)
                {
                    HtmlAnchor aTabCategory = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aTabCategory");
                    HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");

                    if (CategoryIdView == Convert.ToInt32(hdnId.Value))
                        aTabCategory.Attributes["class"] = "navi-link active";
                    else
                        aTabCategory.Attributes["class"] = "navi-link";

                    if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                    {
                        HtmlGenericControl divCategoryActionsArea = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divCategoryActionsArea");
                        divCategoryActionsArea.Visible = false;
                    }

                    GetCategoriesFilesCountNotificationsByCategoryId(item, Convert.ToInt32(hdnId.Value));
                }

                if (isClose)
                    session.CloseConnection();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void FixCategoryFilesItems()
        {
            bool isClose = false;

            try
            {
                if (session.Connection.State == ConnectionState.Closed)
                {
                    session.OpenConnection();
                    isClose = true;
                }

                foreach (RepeaterItem item in Rdg1.Items)
                {
                    HtmlGenericControl div1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div1");
                    HtmlGenericControl div2 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div2");
                    HtmlGenericControl div3 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div3");
                    HtmlGenericControl div4 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div4");

                    HiddenField hdnIsNew1 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsNew1");
                    HiddenField hdnIsNew2 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsNew2");
                    HiddenField hdnIsNew3 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsNew3");
                    HiddenField hdnIsNew4 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsNew4");

                    if (div1.Visible)
                    {
                        DANotificationControl ucNotif1 = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif1");
                        ucNotif1.IsVisible = ucNotif1 != null ? Convert.ToInt32(hdnIsNew1.Value) == 1 : false;
                    }

                    if (div2.Visible)
                    {
                        DANotificationControl ucNotif2 = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif2");
                        ucNotif2.IsVisible = ucNotif2 != null ? Convert.ToInt32(hdnIsNew2.Value) == 1 : false;
                    }

                    if (div3.Visible)
                    {
                        DANotificationControl ucNotif3 = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif3");
                        ucNotif3.IsVisible = ucNotif3 != null ? Convert.ToInt32(hdnIsNew3.Value) == 1 : false;
                    }

                    if (div4.Visible)
                    {
                        DANotificationControl ucNotif4 = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif4");
                        ucNotif4.IsVisible = ucNotif4 != null ? Convert.ToInt32(hdnIsNew4.Value) == 1 : false;
                    }
                }

                if (isClose)
                    session.CloseConnection();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void GetCategoriesFilesCountNotificationsByCategoryId(RepeaterItem item, int categoryId)
        {
            DataTable newFilesNotifications = new DataTable();

            ElioUsers partner = null;   // Sql.GetUserByGuId(Key, session);

            string strQuery = @"
                                SELECT category_id, count(id) as count  
                                FROM Elio_onboarding_users_library_files
                                where 1 = 1
                                AND ";

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                if (IsSendFiles)
                {
                    if (partner == null)
                        strQuery += @"(uploaded_by_user_id = @user_id and user_id = @user_id) ";
                    else
                        strQuery += @"(uploaded_by_user_id = @user_id
                                and user_id = " + partner.Id + ") ";
                }
                else
                {
                    if (partner == null)
                        strQuery += @"(uploaded_by_user_id <> @user_id 
                                and user_id = @user_id) ";
                    else
                        strQuery += @"(uploaded_by_user_id = " + partner.Id + " " +
                                "and user_id = @user_id) ";
                }

                strQuery += @" and " +
                                "(category_id = @category_id " +
                                "AND is_public=1) ";
            }
            else
            {
                if (DrpVendors.SelectedValue != "0")
                {
                    if (IsSendFiles)
                    {
                        strQuery += @"(uploaded_by_user_id = @user_id 
                                and user_id = " + Convert.ToInt32(DrpVendors.SelectedValue) + " ) ";
                    }
                    else
                    {
                        strQuery += @"(uploaded_by_user_id = " + Convert.ToInt32(DrpVendors.SelectedValue) + " " +
                                "and user_id = @user_id ) ";
                    }

                    strQuery += @" and " +
                                "(category_id = @category_id " +
                                "AND is_public=1) ";
                }
            }

            if (RdtDateFromSearch.SelectedDate != null)
            {
                strQuery += " and date_created >= '" + RdtDateFromSearch.SelectedDate.Value.Year + "-" + RdtDateFromSearch.SelectedDate.Value.Month + "-" + RdtDateFromSearch.SelectedDate.Value.Day + "'";
            }

            if (RdtDateToSearch.SelectedDate != null)
            {
                strQuery += " and date_created <= '" + RdtDateToSearch.SelectedDate.Value.Year + "-" + RdtDateToSearch.SelectedDate.Value.Month + "-" + RdtDateToSearch.SelectedDate.Value.Day + "'";
            }

            strQuery += " group by category_id";

            newFilesNotifications = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@user_id", (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : Convert.ToInt32(DrpVendors.SelectedValue))
                                                    , DatabaseHelper.CreateIntParameter("@category_id", categoryId));

            DANotificationControl UcControlNewFilesCount = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcControlNewFilesCount");
            if (UcConfirmationMessageControl != null)
            {
                UcControlNewFilesCount.IsVisible = true;
                UcControlNewFilesCount.Text = (newFilesNotifications.Rows.Count > 0) ? newFilesNotifications.Rows[0]["count"].ToString() : "0";
            }

            UpdatePanelContent.Update();
        }

        private void SetLinks()
        {
            //aCreateLibraryGroup.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-choose-partners");
            //aCreateTiers.HRef = ControlLoader.Dashboard(vSession.User, "tier-management");
        }

        private void UpdateStrings()
        {
            //if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
            //    ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
            //    if (packet != null)
            //    {
            //        LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
            //    }
            //}
            //else
            //{
            //    LblPricingPlan.Text = "You are currently on a free plan";
            //}

            //LblElioplusDashboard.Text = "";

            //LblDashboard.Text = "Dashboard";

            //aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;

            //if (aBtnGoPremium.Visible)
            //{
            //    LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
            //    LblPricingPlan.Visible = false;
            //}

            //LblGoFull.Text = "Complete your registration";
            //LblDashPage.Text = "Partner Onboarding";
            //LblDashSubTitle.Text = "";
        }

        private void FixItemData(RepeaterItem item, bool enableEdit)
        {
            UcMessageAlert.Visible = false;
            //MessageVideoAlert.Visible = false;

            if (item != null)
            {
                ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                ImageButton imgBtnSaveChanges = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSaveChanges");
                ImageButton imgBtnCancel = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancel");

                imgBtnEdit.Visible = !enableEdit;
                imgBtnSaveChanges.Visible = enableEdit;
                imgBtnCancel.Visible = enableEdit;

                Label lblProfileStatusValue = (Label)ControlFinder.FindControlRecursive(item, "LblProfileStatusValue");
                lblProfileStatusValue.Visible = !enableEdit;

                RadComboBox rcbxPublic = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxPublic");
                rcbxPublic.Visible = enableEdit;
            }
        }

        private bool Validate(DropDownList drpList, HtmlInputFile inputFile, out string alert)
        {
            alert = "";

            if (drpList.SelectedItem.Value == "0")
            {
                alert = "Select category";

                return false;
            }

            # region check file

            var fileContent = inputFile.PostedFile;

            if (fileContent != null && fileContent.ContentLength > 0)
            {
                var fileSize = fileContent.ContentLength;
                var fileType = fileContent.ContentType;

                int maxCollaborationFileLenght = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CollaborationMaxFileLenght"]);

                if (fileSize > maxCollaborationFileLenght)
                {
                    alert = "Your file size is outside the bounds. Please try smaller file size or contact us.";

                    return false;
                }

                if (fileContent.FileName == "")
                {
                    alert = "Your file name is empty. Please try naming your file or contact us.";

                    return false;
                }
                else
                {
                    if (fileContent.FileName.Length > 150)
                    {
                        alert = "Your file name characters length is outside the bounds. Please try smaller file name size or contact us.";

                        return false;
                    }

                    bool exist = SqlCollaboration.ExistFileByNameOrTitle(vSession.User.Id, Convert.ToInt32(Ddlcategory.SelectedItem.Value), fileContent.FileName, true, session);
                    if (exist)
                    {
                        alert = "There is another file with the same name in this category.";
                        return false;
                    }
                }

                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    #region Each User Available File Storage Space
                                        
                    if (!GlobalDBMethods.UserHasAvailableStorage(vSession.User.Id, fileContent.ContentLength, session))
                    {
                        alert = string.Format("Your available storage space is not enough to send file {0}", fileContent.FileName);
                        return false;
                    }

                    #endregion
                }
            }
            else
            {
                alert = "Nothing was uploaded because you did not select a file! Please try again.";

                return false;
            }

            #endregion

            #region check file name/title if already exists by this user

            //if (TbxFileTitle.Text != "")
            //{
            //    bool exist = SqlCollaboration.ExistFileByNameOrTitle(vSession.User.Id, Convert.ToInt32(Ddlcategory.SelectedItem.Value), TbxFileTitle.Text.Trim(), false, session);
            //    if (exist)
            //    {
            //        alert = "There is another file with the same title and in this category.";
            //        return false;
            //    }
            //}

            #endregion

            return true;
        }

        private bool ValidateFile(DropDownList drpList, UploadedFile inputFile, out string alert)
        {
            alert = "";

            if (drpList.SelectedItem.Value == "0")
            {
                alert = "Select category";

                return false;
            }

            # region check file

            var fileContent = inputFile;

            if (fileContent != null && fileContent.ContentLength > 0)
            {
                var fileSize = fileContent.ContentLength;
                var fileType = fileContent.ContentType;

                int maxCollaborationFileLenght = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CollaborationMaxFileLenght"]);

                if (fileSize > maxCollaborationFileLenght)
                {
                    alert = "Your file size is outside the bounds. Please try smaller file size or contact us.";

                    return false;
                }

                if (fileContent.FileName == "")
                {
                    alert = "Your file name is empty. Please try naming your file or contact us.";

                    return false;
                }
                else
                {
                    if (fileContent.FileName.Length > 150)
                    {
                        alert = "Your file name characters length is outside the bounds. Please try smaller file name size or contact us.";

                        return false;
                    }

                    bool exist = SqlCollaboration.ExistFileByNameOrTitle(vSession.User.Id, Convert.ToInt32(Ddlcategory.SelectedItem.Value), fileContent.FileName, true, session);
                    if (exist)
                    {
                        alert = "There is another file with the same name in this category.";
                        return false;
                    }
                }

                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    #region Each User Available File Storage Space

                    if (!GlobalDBMethods.UserHasAvailableStorage(vSession.User.Id, fileContent.ContentLength, session))
                    {
                        alert = string.Format("Your available storage space is not enough to send file {0}", fileContent.FileName);
                        return false;
                    }

                    #endregion
                }
            }
            else
            {
                alert = "Nothing was uploaded because you did not select a file! Please try again.";

                return false;
            }

            #endregion

            return true;
        }

        private void ResetPanelItems()
        {
            UcReceiversAndUploadFile.Visible = false;
            MessageControlCategories.Visible = false;

            divFileUpload.Visible = true;
            tr2.Visible = tr3.Visible = tr4.Visible = tr5.Visible = tr6.Visible = tr7.Visible = tr8.Visible = tr9.Visible = tr10.Visible = false;
        }

        private void FixTabMenu()
        {
            MessageControlCategories.Visible = false;

            DataTable table = null;

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                table = SqlCollaboration.GetOnboardingUserLibraryPublicFilesCategoriesTbl(vSession.User.Id, session);

                if (table == null || (table != null && table.Rows.Count == 0))
                {
                    List<ElioOnboardingLibraryFilesDefaultCategories> defaultCategories = SqlCollaboration.GetOnboardingUserLibraryPublicDefaultFilesCategories(session);
                    if (defaultCategories.Count > 0)
                    {
                        foreach (ElioOnboardingLibraryFilesDefaultCategories category in defaultCategories)
                        {
                            ElioOnboardingUsersLibraryFilesCategories item = new ElioOnboardingUsersLibraryFilesCategories();

                            item.UserId = vSession.User.Id;
                            item.CategoryDescription = category.CategoryDescription;
                            item.DateCreated = DateTime.Now;
                            item.LastUpdate = DateTime.Now;
                            item.IsPublic = 1;
                            item.IsDefault = 1;

                            DataLoader<ElioOnboardingUsersLibraryFilesCategories> loader = new DataLoader<ElioOnboardingUsersLibraryFilesCategories>(session);
                            loader.Insert(item);
                        }
                    }

                    table = SqlCollaboration.GetOnboardingUserLibraryPublicFilesCategoriesTbl(vSession.User.Id, session);
                }
            }
            else
            {
                if (DrpVendors.SelectedValue != "")
                    table = SqlCollaboration.GetOnboardingUserLibraryPublicFilesCategoriesTbl(Convert.ToInt32(DrpVendors.SelectedValue), session);
            }

            if (table != null && table.Rows.Count > 0)
            {
                RdgCategories.Visible = true;
                //BtnSendFile.Visible = true;

                RdgCategories.DataSource = table;
                RdgCategories.DataBind();

                CategoryIdView = Convert.ToInt32(table.Rows[0]["id"]);
                FixCategoriesItems();
            }
            else
            {
                RdgCategories.Visible = false;
                BtnSendFile.Visible = false;

                GlobalMethods.ShowMessageControlDA(MessageControlCategories, "No categories", MessageTypes.Info, true, true, true, false, false);
            }
        }

        private void FillDropLists()
        {
            MessageControlNoCategories.Visible = false;

            List<ElioOnboardingUsersLibraryFilesCategories> userCategories = null;

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                userCategories = SqlCollaboration.GetOnboardingUserLibraryPublicFilesCategories(vSession.User.Id, session);

                if (userCategories.Count == 0)
                {
                    List<ElioOnboardingLibraryFilesDefaultCategories> defaultCategories = SqlCollaboration.GetOnboardingUserLibraryPublicDefaultFilesCategories(session);
                    if (defaultCategories.Count > 0)
                    {
                        foreach (ElioOnboardingLibraryFilesDefaultCategories category in defaultCategories)
                        {
                            ElioOnboardingUsersLibraryFilesCategories item = new ElioOnboardingUsersLibraryFilesCategories();

                            item.UserId = vSession.User.Id;
                            item.CategoryDescription = category.CategoryDescription;
                            item.DateCreated = DateTime.Now;
                            item.LastUpdate = DateTime.Now;
                            item.IsPublic = 1;
                            item.IsDefault = 1;

                            DataLoader<ElioOnboardingUsersLibraryFilesCategories> loader = new DataLoader<ElioOnboardingUsersLibraryFilesCategories>(session);
                            loader.Insert(item);
                        }
                    }

                    userCategories = SqlCollaboration.GetOnboardingUserLibraryPublicFilesCategories(vSession.User.Id, session);
                }
            }
            else
            {
                userCategories = SqlCollaboration.GetOnboardingUserLibraryPublicFilesCategories(Convert.ToInt32(DrpVendors.SelectedValue), session);
            }

            if (userCategories.Count > 0)
            {
                Ddlcategory.Items.Clear();
                Ddlcategory2.Items.Clear();
                Ddlcategory3.Items.Clear();
                Ddlcategory4.Items.Clear();
                Ddlcategory5.Items.Clear();
                Ddlcategory6.Items.Clear();
                Ddlcategory7.Items.Clear();
                Ddlcategory8.Items.Clear();
                Ddlcategory9.Items.Clear();
                Ddlcategory10.Items.Clear();

                ListItem item = new ListItem();

                item.Value = "0";
                item.Text = "Choose category";

                Ddlcategory.Items.Add(item);
                Ddlcategory2.Items.Add(item);
                Ddlcategory3.Items.Add(item);
                Ddlcategory4.Items.Add(item);
                Ddlcategory5.Items.Add(item);
                Ddlcategory6.Items.Add(item);
                Ddlcategory7.Items.Add(item);
                Ddlcategory8.Items.Add(item);
                Ddlcategory9.Items.Add(item);
                Ddlcategory10.Items.Add(item);

                foreach (ElioOnboardingUsersLibraryFilesCategories userCategory in userCategories)
                {
                    item = new ListItem();

                    item.Value = userCategory.Id.ToString();
                    item.Text = userCategory.CategoryDescription;

                    Ddlcategory.Items.Add(item);
                    Ddlcategory2.Items.Add(item);
                    Ddlcategory3.Items.Add(item);
                    Ddlcategory4.Items.Add(item);
                    Ddlcategory5.Items.Add(item);
                    Ddlcategory6.Items.Add(item);
                    Ddlcategory7.Items.Add(item);
                    Ddlcategory8.Items.Add(item);
                    Ddlcategory9.Items.Add(item);
                    Ddlcategory10.Items.Add(item);
                }
            }
            else
            {
                GlobalMethods.ShowMessageControlDA(MessageControlNoCategories, "Sorry, selected vendor has no onboarding files categories yet.", MessageTypes.Error, true, true, true, false, false);
            }
        }

        private bool FileExistsNew(DataTable table, string fileName)
        {
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["file_name"].ToString() == fileName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool FileExistsNew1(DataTable table, string fileName)
        {
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["file_name1"].ToString() == fileName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool FileExistsNew2(DataTable table, string fileName)
        {
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["file_name2"].ToString() == fileName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool FileExistsNew3(DataTable table, string fileName)
        {
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["file_name3"].ToString() == fileName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool FileExistsNew4(DataTable table, string fileName)
        {
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["file_name4"].ToString() == fileName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void DeleteFile(int id, int fileTypeId, int categoryId, string fileName)
        {
            if (fileTypeId > 0 && id > 0 && fileName != "" && categoryId > -1)
            {
                BtnDeleteFile.Visible = true;
                BtnDeletePreviewFile.Visible = BtnDeleteCategory.Visible = false;

                GlobalMethods.ShowMessageControlDA(UcConfirmationMessageControl, "Are you sure you want to delete this file from your library?", MessageTypes.Warning, true, true, true, true, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            else
            {
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "11")).Text;
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, true, true, false);
            }

            //GetCategoriesNewFilesNotifications("Elio_onboarding_users_library_files");
        }

        private void DeletePreviewFile()
        {
            if (LibraryFileId > 0 && LibraryFileCategoryId > 0 && LibraryPreviewFileName != "")
            {
                BtnDeletePreviewFile.Visible = true;
                BtnDeleteFile.Visible = false;
                BtnDeleteCategory.Visible = false;
                
                GlobalMethods.ShowMessageControlDA(UcConfirmationMessageControl, "If you proceed, the preview icon will be deleted. Do you want to delete this preview icon?", MessageTypes.Error, true, true, false, false, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            else
            {
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "21")).Text;
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false, false, false);
            }
        }

        private void DeleteCategory(int categoryId)
        {
            if (categoryId > -1)
            {
                BtnDeleteCategory.Visible = true;
                BtnDeleteFile.Visible = false;
                BtnDeletePreviewFile.Visible = false;
                
                GlobalMethods.ShowMessageControlDA(UcConfirmationMessageControl, "If you proceed, all the files saved in this category will be deleted. Do you want to delete this category?", MessageTypes.Error, true, true, false, false, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            else
            {
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "19")).Text;
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false, false, false);
            }
        }

        private string GetItemImageByType(string path)
        {
            string[] filePath = path.Split('/').ToArray();
            if (filePath.Length > 0)
            {
                string fileNameWithExtension = filePath[filePath.Length - 1];
                string[] fileInArray = fileNameWithExtension.Split('.').ToArray();
                if (fileInArray.Length > 0)
                {
                    string extension = fileInArray[fileInArray.Length - 1].ToLower();

                    if (File.Exists(Server.MapPath("/images/OnboardingFileTypes/" + extension + ".jpg")))
                        return "/images/OnboardingFileTypes/" + extension + ".jpg";
                    else if (File.Exists(Server.MapPath("/images/OnboardingFileTypes/" + extension + ".png")))
                        return "/images/OnboardingFileTypes/" + extension + ".png";
                    else
                        return "/images/OnboardingFileTypes/file.png";
                }
                else
                    return "";
            }
            else
                return "";
        }

        private void BuildRepeaterItem(RepeaterItemEventArgs args)
        {
            if (session.Connection.State == ConnectionState.Closed)
                session.OpenConnection();

            if (vSession.User == null)
            {
                Response.Redirect(ControlLoader.Login, false);
                return;
            }
            RepeaterItem item = (RepeaterItem)args.Item;
            if (item != null)
            {
                DataRowView row = (DataRowView)item.DataItem;
                if (row != null)
                {
                    HtmlGenericControl div1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div1");
                    HtmlGenericControl div2 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div2");
                    HtmlGenericControl div3 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div3");
                    HtmlGenericControl div4 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div4");

                    if (row["id1"].ToString() == "0")
                        div1.Visible = false;
                    if (row["id2"].ToString() == "0")
                        div2.Visible = false;
                    if (row["id3"].ToString() == "0")
                        div3.Visible = false;
                    if (row["id4"].ToString() == "0")
                        div4.Visible = false;

                    if (div1.Visible)
                    {
                        DANotificationControl ucNotif1 = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif1");
                        ucNotif1.IsVisible = ucNotif1 != null ? Convert.ToInt32(row["is_new1"]) == 1 : false;

                        HtmlAnchor aDelete1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete1");
                        
                        if (aDelete1 != null)
                            aDelete1.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                        HtmlGenericControl liDeletePreviewImg1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "liDeletePreviewImg1");

                        if (liDeletePreviewImg1 != null)
                            liDeletePreviewImg1.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && !string.IsNullOrEmpty(row["preview_file_path1"].ToString());

                        HtmlGenericControl liUploadPreviewImg1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "liUploadPreviewImg1");

                        if (liUploadPreviewImg1 != null)
                            liUploadPreviewImg1.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && string.IsNullOrEmpty(row["preview_file_path1"].ToString());

                        Label lbl1 = (Label)ControlFinder.FindControlRecursive(item, "Lbl1");

                        if (!string.IsNullOrEmpty(row["file_name1"].ToString()))
                        {
                            if (row["file_name1"].ToString().Length > 25)
                            {
                                lbl1.Text = row["file_name1"].ToString().Substring(0, 25) + "...";
                            }
                            else
                            {
                                lbl1.Text = row["file_name1"].ToString();
                            }
                        }
                        else
                        {
                            if (row["file_title1"].ToString().Length > 25)
                            {
                                lbl1.Text = row["file_title1"].ToString().Substring(0, 25) + "...";
                            }
                            else
                            {
                                lbl1.Text = row["file_title1"].ToString();
                            }
                        }
                    }
                    if (div2.Visible)
                    {
                        DANotificationControl ucNotif2 = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif2");
                        ucNotif2.IsVisible = ucNotif2 != null ? Convert.ToInt32(row["is_new2"]) == 1 : false;

                        HtmlAnchor aDelete2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete2");
                        
                        if (aDelete2 != null)
                            aDelete2.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                        HtmlGenericControl liDeletePreviewImg2 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "liDeletePreviewImg2");

                        if (liDeletePreviewImg2 != null)
                            liDeletePreviewImg2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && !string.IsNullOrEmpty(row["preview_file_path2"].ToString());

                        HtmlGenericControl liUploadPreviewImg2 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "liUploadPreviewImg2");

                        if (liUploadPreviewImg2 != null)
                            liUploadPreviewImg2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && string.IsNullOrEmpty(row["preview_file_path2"].ToString());

                        Label lbl2 = (Label)ControlFinder.FindControlRecursive(item, "Lbl2");

                        if (!string.IsNullOrEmpty(row["file_name2"].ToString()))
                        {
                            if (row["file_name2"].ToString().Length > 25)
                            {
                                lbl2.Text = row["file_name2"].ToString().Substring(0, 25) + "...";
                            }
                            else
                            {
                                lbl2.Text = row["file_name2"].ToString();
                            }
                        }
                        else
                        {
                            if (row["file_title2"].ToString().Length > 25)
                            {
                                lbl2.Text = row["file_title2"].ToString().Substring(0, 25) + "...";
                            }
                            else
                            {
                                lbl2.Text = row["file_title2"].ToString();
                            }
                        }
                    }
                    if (div3.Visible)
                    {
                        DANotificationControl ucNotif3 = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif3");
                        ucNotif3.IsVisible = ucNotif3 != null ? Convert.ToInt32(row["is_new3"]) == 1 : false;

                        HtmlAnchor aDelete3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete3");
                        
                        if (aDelete3 != null)
                            aDelete3.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                        HtmlGenericControl liDeletePreviewImg3 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "liDeletePreviewImg3");

                        if (liDeletePreviewImg3 != null)
                            liDeletePreviewImg3.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && !string.IsNullOrEmpty(row["preview_file_path3"].ToString());

                        HtmlGenericControl liUploadPreviewImg3 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "liUploadPreviewImg3");

                        if (liUploadPreviewImg3 != null)
                            liUploadPreviewImg3.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && string.IsNullOrEmpty(row["preview_file_path3"].ToString());

                        Label lbl3 = (Label)ControlFinder.FindControlRecursive(item, "Lbl3");

                        if (!string.IsNullOrEmpty(row["file_name3"].ToString()))
                        {
                            if (row["file_name3"].ToString().Length > 25)
                            {
                                lbl3.Text = row["file_name3"].ToString().Substring(0, 25) + "...";
                            }
                            else
                            {
                                lbl3.Text = row["file_name3"].ToString();
                            }
                        }
                        else
                        {
                            if (row["file_title3"].ToString().Length > 25)
                            {
                                lbl3.Text = row["file_title3"].ToString().Substring(0, 25) + "...";
                            }
                            else
                            {
                                lbl3.Text = row["file_title3"].ToString();
                            }
                        }
                    }
                    if (div4.Visible)
                    {
                        DANotificationControl ucNotif4 = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif4");
                        ucNotif4.IsVisible = ucNotif4 != null ? Convert.ToInt32(row["is_new4"]) == 1 : false;

                        HtmlAnchor aDelete4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete4");
                        
                        if (aDelete4 != null)
                            aDelete4.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                        HtmlGenericControl liDeletePreviewImg4 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "liDeletePreviewImg4");

                        if (liDeletePreviewImg4 != null)
                            liDeletePreviewImg4.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && !string.IsNullOrEmpty(row["preview_file_path4"].ToString());

                        HtmlGenericControl liUploadPreviewImg4 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "liUploadPreviewImg4");

                        if (liUploadPreviewImg4 != null)
                            liUploadPreviewImg4.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && string.IsNullOrEmpty(row["preview_file_path4"].ToString());

                        Label lbl4 = (Label)ControlFinder.FindControlRecursive(item, "Lbl4");

                        if (!string.IsNullOrEmpty(row["file_name4"].ToString()))
                        {
                            if (row["file_name4"].ToString().Length > 25)
                            {
                                lbl4.Text = row["file_name4"].ToString().Substring(0, 25) + "...";
                            }
                            else
                            {
                                lbl4.Text = row["file_name4"].ToString();
                            }
                        }
                        else
                        {
                            if (row["file_title4"].ToString().Length > 25)
                            {
                                lbl4.Text = row["file_title4"].ToString().Substring(0, 25) + "...";
                            }
                            else
                            {
                                lbl4.Text = row["file_title4"].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void LoadGridData(Repeater rpt, int categoryID, DAMessageControl control)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable table = new DataTable();

                    table.Columns.Add("user_id");
                    table.Columns.Add("category_id");

                    table.Columns.Add("id1");
                    table.Columns.Add("id2");
                    table.Columns.Add("id3");
                    table.Columns.Add("id4");

                    table.Columns.Add("file_type_id1");
                    table.Columns.Add("file_type_id2");
                    table.Columns.Add("file_type_id3");
                    table.Columns.Add("file_type_id4");

                    table.Columns.Add("file_title1");
                    table.Columns.Add("file_title2");
                    table.Columns.Add("file_title3");
                    table.Columns.Add("file_title4");

                    table.Columns.Add("file_name1");
                    table.Columns.Add("file_name2");
                    table.Columns.Add("file_name3");
                    table.Columns.Add("file_name4");

                    table.Columns.Add("file_path1");
                    table.Columns.Add("file_path2");
                    table.Columns.Add("file_path3");
                    table.Columns.Add("file_path4");

                    table.Columns.Add("file_type1");
                    table.Columns.Add("file_type2");
                    table.Columns.Add("file_type3");
                    table.Columns.Add("file_type4");

                    table.Columns.Add("is_new1");
                    table.Columns.Add("is_new2");
                    table.Columns.Add("is_new3");
                    table.Columns.Add("is_new4");

                    table.Columns.Add("preview_file_path1");
                    table.Columns.Add("preview_file_path2");
                    table.Columns.Add("preview_file_path3");
                    table.Columns.Add("preview_file_path4");

                    int userId = -1;

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        userId = vSession.User.Id;
                    }
                    else
                    {
                        if (DrpVendors.SelectedValue != "" && DrpVendors.SelectedValue != "0")
                        {
                            userId = Convert.ToInt32(DrpVendors.SelectedValue);
                        }
                    }

                    if (userId > -1)
                    {
                        List<ElioOnboardingUsersLibraryFiles> files = new List<ElioOnboardingUsersLibraryFiles>();

                        if (categoryID > 0)
                        {
                            files = Sql.GetOnboardingUserLibraryFileByCategoryId(userId, categoryID, RdtDateFromSearch.SelectedDate != null ? RdtDateFromSearch.SelectedDate : null, RdtDateToSearch.SelectedDate != null ? RdtDateToSearch.SelectedDate : null, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);
                        }
                        else
                        {
                            files = Sql.GetOnboardingUserLibraryFileByAllCategories(userId, session);
                        }

                        if (files.Count > 0)
                        {
                            rpt.Visible = true;
                            control.Visible = false;

                            int rows = files.Count / 4;
                            int columns = files.Count % 4;
                            int index = 0;

                            for (int i = 0; i < rows; i++)
                            {
                                if (string.IsNullOrEmpty(files[index].PreviewFilePath))
                                {
                                    if (!FileExistsNew1(table, files[index].FileName))
                                        files[index].FileType = GetItemImageByType(files[index].FilePath);
                                }
                                else
                                {
                                    files[index].FileType = "/OnboardingLibrary/" + files[index].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 1].PreviewFilePath))
                                {
                                    if (!FileExistsNew2(table, files[index + 1].FileName))
                                        files[index + 1].FileType = GetItemImageByType(files[index + 1].FilePath);
                                }
                                else
                                {
                                    files[index + 1].FileType = "/OnboardingLibrary/" + files[index + 1].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 2].PreviewFilePath))
                                {
                                    if (!FileExistsNew3(table, files[index + 2].FileName))
                                        files[index + 2].FileType = GetItemImageByType(files[index + 2].FilePath);
                                }
                                else
                                {
                                    files[index + 2].FileType = "/OnboardingLibrary/" + files[index + 2].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 3].PreviewFilePath))
                                {
                                    if (!FileExistsNew4(table, files[index + 3].FileName))
                                        files[index + 3].FileType = GetItemImageByType(files[index + 3].FilePath);
                                }
                                else
                                {
                                    files[index + 3].FileType = "/OnboardingLibrary/" + files[index + 3].PreviewFilePath;
                                }

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = files[index + 1].Id.ToString();
                                row["id3"] = files[index + 2].Id.ToString();
                                row["id4"] = files[index + 3].Id.ToString();
                                row["file_type_id1"] = files[index].FileTypeId.ToString();
                                row["file_type_id2"] = files[index + 1].FileTypeId.ToString();
                                row["file_type_id3"] = files[index + 2].FileTypeId.ToString();
                                row["file_type_id4"] = files[index + 3].FileTypeId.ToString();
                                row["file_title1"] = files[index].FileTitle;
                                row["file_title2"] = files[index + 1].FileTitle;
                                row["file_title3"] = files[index + 2].FileTitle;
                                row["file_title4"] = files[index + 3].FileTitle;
                                row["file_name1"] = files[index].FileName;
                                row["file_name2"] = files[index + 1].FileName;
                                row["file_name3"] = files[index + 2].FileName;
                                row["file_name4"] = files[index + 3].FileName;
                                row["file_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index].FilePath;
                                row["file_path2"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath;
                                row["file_path3"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 2].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 2].FilePath;
                                row["file_path4"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 3].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 3].FilePath;
                                row["file_type1"] = files[index].FileType;
                                row["file_type2"] = files[index + 1].FileType;
                                row["file_type3"] = files[index + 2].FileType;
                                row["file_type4"] = files[index + 3].FileType;
                                row["is_new1"] = files[index].IsNew.ToString();
                                row["is_new2"] = files[index + 1].IsNew.ToString();
                                row["is_new3"] = files[index + 2].IsNew.ToString();
                                row["is_new4"] = files[index + 3].IsNew.ToString();
                                row["preview_file_path1"] = files[index].PreviewFilePath.ToString();
                                row["preview_file_path2"] = files[index + 1].PreviewFilePath.ToString();
                                row["preview_file_path3"] = files[index + 2].PreviewFilePath.ToString();
                                row["preview_file_path4"] = files[index + 3].PreviewFilePath.ToString();
                                index = index + 4;

                                table.Rows.Add(row);
                            }

                            if (columns == 1)
                            {
                                if (string.IsNullOrEmpty(files[index].PreviewFilePath))
                                {
                                    if (!FileExistsNew1(table, files[index].FileName))
                                        files[index].FileType = GetItemImageByType(files[index].FilePath);
                                }
                                else
                                {
                                    files[index].FileType = "/OnboardingLibrary/" + files[index].PreviewFilePath;
                                }

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = "0";
                                row["id3"] = "0";
                                row["id4"] = "0";
                                row["file_type_id1"] = files[index].FileTypeId.ToString();
                                row["file_type_id2"] = "0";
                                row["file_type_id3"] = "0";
                                row["file_type_id4"] = "0";
                                row["file_title1"] = files[index].FileTitle;
                                row["file_title2"] = "-";
                                row["file_title3"] = "-";
                                row["file_title4"] = "-";
                                row["file_name1"] = files[index].FileName;
                                row["file_name2"] = "-";
                                row["file_name3"] = "-";
                                row["file_name4"] = "-";
                                row["file_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index].FilePath;
                                row["file_path2"] = "#";
                                row["file_path3"] = "#";
                                row["file_path4"] = "#";
                                row["file_type1"] = files[index].FileType;
                                row["file_type2"] = "-";
                                row["file_type3"] = "-";
                                row["file_type4"] = "-";
                                row["is_new1"] = files[index].IsNew.ToString();
                                row["is_new2"] = "0";
                                row["is_new3"] = "0";
                                row["is_new4"] = "0";
                                row["preview_file_path1"] = files[index].PreviewFilePath.ToString();
                                row["preview_file_path2"] = "";
                                row["preview_file_path3"] = "";
                                row["preview_file_path4"] = "";

                                table.Rows.Add(row);
                            }
                            else if (columns == 2)
                            {
                                if (string.IsNullOrEmpty(files[index].PreviewFilePath))
                                {
                                    if (!FileExistsNew1(table, files[index].FileName))
                                        files[index].FileType = GetItemImageByType(files[index].FilePath);
                                }
                                else
                                {
                                    files[index].FileType = "/OnboardingLibrary/" + files[index].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 1].PreviewFilePath))
                                {
                                    if (!FileExistsNew2(table, files[index + 1].FileName))
                                        files[index + 1].FileType = GetItemImageByType(files[index + 1].FilePath);
                                }
                                else
                                {
                                    files[index + 1].FileType = "/OnboardingLibrary/" + files[index + 1].PreviewFilePath;
                                }

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = files[index + 1].Id.ToString();
                                row["id3"] = "0";
                                row["id4"] = "0";
                                row["file_type_id1"] = files[index].FileTypeId.ToString();
                                row["file_type_id2"] = files[index + 1].FileTypeId.ToString();
                                row["file_type_id3"] = "0";
                                row["file_type_id4"] = "0";
                                row["file_title1"] = files[index].FileTitle;
                                row["file_title2"] = files[index + 1].FileTitle;
                                row["file_title3"] = "-";
                                row["file_title4"] = "-";
                                row["file_name1"] = files[index].FileName;
                                row["file_name2"] = files[index + 1].FileName;
                                row["file_name3"] = "-";
                                row["file_name4"] = "-";
                                row["file_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index].FilePath;
                                row["file_path2"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath;
                                row["file_path3"] = "#";
                                row["file_path4"] = "#";
                                row["file_type1"] = files[index].FileType;
                                row["file_type2"] = files[index + 1].FileType;
                                row["file_type3"] = "-";
                                row["file_type4"] = "-";
                                row["is_new1"] = files[index].IsNew.ToString();
                                row["is_new2"] = files[index + 1].IsNew.ToString();
                                row["is_new3"] = "0";
                                row["is_new4"] = "0";
                                row["preview_file_path1"] = files[index].PreviewFilePath.ToString();
                                row["preview_file_path2"] = files[index + 1].PreviewFilePath.ToString();
                                row["preview_file_path3"] = "";
                                row["preview_file_path4"] = "";

                                table.Rows.Add(row);
                            }
                            else if (columns == 3)
                            {
                                if (string.IsNullOrEmpty(files[index].PreviewFilePath))
                                {
                                    if (!FileExistsNew1(table, files[index].FileName))
                                        files[index].FileType = GetItemImageByType(files[index].FilePath);
                                }
                                else
                                {
                                    files[index].FileType = "/OnboardingLibrary/" + files[index].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 1].PreviewFilePath))
                                {
                                    if (!FileExistsNew2(table, files[index + 1].FileName))
                                        files[index + 1].FileType = GetItemImageByType(files[index + 1].FilePath);
                                }
                                else
                                {
                                    files[index + 1].FileType = "/OnboardingLibrary/" + files[index + 1].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 2].PreviewFilePath))
                                {
                                    if (!FileExistsNew3(table, files[index + 2].FileName))
                                        files[index + 2].FileType = GetItemImageByType(files[index + 2].FilePath);
                                }
                                else
                                {
                                    files[index + 2].FileType = "/OnboardingLibrary/" + files[index + 2].PreviewFilePath;
                                }

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = files[index + 1].Id.ToString();
                                row["id3"] = files[index + 2].Id.ToString();
                                row["id4"] = "0";
                                row["file_type_id1"] = files[index].FileTypeId.ToString();
                                row["file_type_id2"] = files[index + 1].FileTypeId.ToString();
                                row["file_type_id3"] = files[index + 2].FileTypeId.ToString();
                                row["file_type_id4"] = "0";
                                row["file_title1"] = files[index].FileTitle;
                                row["file_title2"] = files[index + 1].FileTitle;
                                row["file_title3"] = files[index + 2].FileTitle;
                                row["file_title4"] = "-";
                                row["file_name1"] = files[index].FileName;
                                row["file_name2"] = files[index + 1].FileName;
                                row["file_name3"] = files[index + 2].FileName;
                                row["file_name4"] = "-";
                                row["file_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index].FilePath;
                                row["file_path2"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath;
                                row["file_path3"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 2].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index + 2].FilePath;
                                row["file_path4"] = "#";
                                row["file_type1"] = files[index].FileType;
                                row["file_type2"] = files[index + 1].FileType;
                                row["file_type3"] = files[index + 2].FileType;
                                row["file_type4"] = "-";
                                row["is_new1"] = files[index].IsNew.ToString();
                                row["is_new2"] = files[index + 1].IsNew.ToString();
                                row["is_new3"] = files[index + 2].IsNew.ToString();
                                row["is_new4"] = "0";
                                row["preview_file_path1"] = files[index].PreviewFilePath.ToString();
                                row["preview_file_path2"] = files[index + 1].PreviewFilePath.ToString();
                                row["preview_file_path3"] = files[index + 2].PreviewFilePath.ToString();
                                row["preview_file_path4"] = "";

                                table.Rows.Add(row);
                            }

                            rpt.Visible = true;
                            rpt.DataSource = table;
                            rpt.DataBind();
                        }
                        else
                        {
                            rpt.Visible = false;
                            control.Visible = true;
                            GlobalMethods.ShowMessageControlDA(control, "There are no files uploaded", MessageTypes.Info, true, true, false, true, false);
                        }
                    }
                    else
                    {
                        rpt.Visible = false;
                        GlobalMethods.ShowMessageControlDA(control, "There are no files uploaded", MessageTypes.Info, true, true, false, true, false);
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

        private void PreviewFile(int isNew, string url, int id, DANotificationControl ucNotification, DBSession session)
        {
            if (isNew == 1 && id > 0)
            {
                try
                {
                    session.ExecuteQuery(@"Update Elio_onboarding_users_library_files
                                                    set is_new = 0
                                                    , last_update = getdate()
                                                    where id = @id"
                    , DatabaseHelper.CreateIntParameter("@id", id));
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                }

                if (ucNotification != null)
                    ucNotification.IsVisible = false;
            }

            //StringBuilder sb = new StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.open('");
            //sb.Append(url);
            //sb.Append("');");
            //sb.Append("</script>");
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "script", sb.ToString(), false);

            Response.Write("<script>");
            Response.Write("window.open('" + url + "','_blank')");
            Response.Write("</script>");
        }

        private bool UploadLibraryFile(FileUploadedEventArgs fileContent, string fileType, DropDownList drpList)
        {
            bool successUpload = false;
            string serverMapPathTargetFolder = "";
            string extension = fileContent.File.GetExtension();
            string fileName = fileContent.File.FileName;
            string contentAlert = "";

            if (fileContent != null)
            {
                try
                {
                    ElioOnboardingFileTypes onboardingFileType = Sql.GetOnboardingFileTypeByExtension(extension, session);       //Sql.GetOnboardingFileTypeById(Convert.ToInt32(DdlFileType.SelectedValue), session);
                    if (onboardingFileType == null)
                    {
                        contentAlert = fileName + " with this type of file could not be uploaded to your library. Please try again or contact with us.";
                        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);

                        return false;
                    }

                    serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["OnboardingLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + drpList.SelectedItem.Text + "\\" + onboardingFileType.FileType + "\\";

                    #region updoad file to my directory                 

                    successUpload = UpLoadImage.UpLoadLibraryFile(serverMapPathTargetFolder, fileContent, out fileName, session);

                    #endregion

                    if (successUpload)
                    {
                        try
                        {
                            #region File Saving Area

                            session.BeginTransaction();

                            #region User File

                            ElioOnboardingUsersLibraryFiles onboardingFile = new ElioOnboardingUsersLibraryFiles();

                            onboardingFile.FileTypeId = (onboardingFileType != null) ? onboardingFileType.Id : -1;
                            onboardingFile.CategoryId = Convert.ToInt32(drpList.SelectedValue);
                            onboardingFile.FileName = (fileName != "") ? fileName : "Onboarding_Library_" + DateTime.Now.ToShortDateString().Replace("/", "_").Replace("-", "_");
                            onboardingFile.FileTitle = onboardingFile.FileName.Replace(extension, "");     //TbxFileTitle.Text;
                            onboardingFile.FilePath = vSession.User.GuId + "/" + drpList.SelectedItem.Text + "/" + onboardingFileType.FileType + "/" + fileName;       //serverMapPathTargetFolder
                            onboardingFile.FileType = fileType;
                            onboardingFile.IsPublic = 1;
                            onboardingFile.IsNew = 1;
                            onboardingFile.UserId = vSession.User.Id;
                            onboardingFile.UploadedByUserId = vSession.User.Id;
                            onboardingFile.DateCreated = DateTime.Now;
                            onboardingFile.LastUpdate = DateTime.Now;

                            DataLoader<ElioOnboardingUsersLibraryFiles> loader = new DataLoader<ElioOnboardingUsersLibraryFiles>(session);
                            loader.Insert(onboardingFile);

                            #endregion

                            #region Blob File

                            ElioOnboardingBlobFiles blobFile = new ElioOnboardingBlobFiles();

                            blobFile.FileName = fileName;
                            //blobFile.CategoryDescription = onboardingFileType.FileType;
                            //blobFile.FilePath = serverMapPathTargetFolder;
                            blobFile.FileSize = (int)fileContent.File.ContentLength;
                            blobFile.FileType = fileType;
                            blobFile.IsPublic = 1;
                            blobFile.DateCreated = DateTime.Now;
                            blobFile.LastUpdate = DateTime.Now;
                            blobFile.LibraryFilesId = onboardingFile.Id;
                            blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolder + blobFile.FileName);

                            DataLoader<ElioOnboardingBlobFiles> blbLoader = new DataLoader<ElioOnboardingBlobFiles>(session);
                            blbLoader.Insert(blobFile);

                            #endregion

                            if (vSession.User.CompanyType == Types.Vendors.ToString())
                            {
                                #region  Update User PacketStatus Available File Storage Count

                                bool success = GlobalDBMethods.ReduceUserFileStorage(vSession.User.Id, Convert.ToDouble(fileContent.File.ContentLength), session);
                                if (!success)
                                    throw new Exception(string.Format("User {0} tried to upload a file at {1} but he has not enough storage space from his packet (user billing packet type id = {2}), so it could not be saved or uploaded!", vSession.User.Id, DateTime.Now.ToString(), vSession.User.BillingType.ToString()));

                                #endregion
                            }

                            session.CommitTransaction();

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();

                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            throw ex;
                        }
                    }
                    else
                    {
                        //GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");
                        FixTabMenu();

                        return false;
                    }
                }
                catch (Exception ex)
                {
                    bool deleted = false;

                    if (successUpload)
                        deleted = UpLoadImage.DeleteFileFromDirectory(serverMapPathTargetFolder, fileName);

                    Logger.DetailedError(Request.Url.ToString(), (deleted) ? string.Format("File not deleted successfully int path {0}", serverMapPathTargetFolder + "\\" + fileName) : "", ex.Message.ToString(), ex.StackTrace.ToString());

                    return false;
                }
            }

            return true;
        }

        private bool UploadPreviewFile(HttpPostedFile fileContent)
        {
            bool successUpload = false;

            ElioOnboardingUsersLibraryFiles libraryFile = SqlCollaboration.GetOnboardingUserLibraryFileById(LibraryFileId, session);
            if (libraryFile != null)
            {
                string[] filePaths = libraryFile.FilePath.Split('/').ToArray();
                if (filePaths.Length > 0 && filePaths.Length > 2)
                {
                    string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["OnboardingLibraryTargetFolder"].ToString()) + filePaths[0] + "\\" + filePaths[1] + "\\PreviewFiles\\";
                    string extension = Path.GetExtension(fileContent.FileName);
                    string fileName = fileContent.FileName + "_" + libraryFile.Id;

                    if (fileContent != null)
                    {
                        try
                        {
                            #region updoad preview file to directory                 

                            successUpload = UpLoadImage.UpLoadFile(serverMapPathTargetFolder, fileContent, true, out fileName, session);

                            #endregion

                            if (successUpload)
                            {
                                try
                                {
                                    #region File Updating / Saving Area

                                    session.BeginTransaction();

                                    #region User Preview File

                                    libraryFile.PreviewFilePath = vSession.User.GuId + "/" + filePaths[1] + "/PreviewFiles/" + fileName;
                                    libraryFile.LastUpdate = DateTime.Now;

                                    DataLoader<ElioOnboardingUsersLibraryFiles> loader = new DataLoader<ElioOnboardingUsersLibraryFiles>(session);
                                    loader.Update(libraryFile);

                                    #endregion

                                    #region Blob Preview File

                                    ElioOnboardingBlobPreviewFiles blobPreviewFile = new ElioOnboardingBlobPreviewFiles();

                                    blobPreviewFile.FileName = fileName;
                                    //blobPreviewFile.FilePath = serverMapPathTargetFolder;
                                    blobPreviewFile.FileSize = fileContent.ContentLength;
                                    blobPreviewFile.FileType = fileContent.ContentType;
                                    blobPreviewFile.IsPublic = 1;
                                    blobPreviewFile.DateCreated = DateTime.Now;
                                    blobPreviewFile.LastUpdate = DateTime.Now;
                                    blobPreviewFile.LibraryFilesId = libraryFile.Id;
                                    blobPreviewFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolder + "/" + blobPreviewFile.FileName);

                                    DataLoader<ElioOnboardingBlobPreviewFiles> blbLoader = new DataLoader<ElioOnboardingBlobPreviewFiles>(session);
                                    blbLoader.Insert(blobPreviewFile);

                                    #endregion

                                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    {
                                        #region  Update User PacketStatus Available File Storage Count

                                        bool success = GlobalDBMethods.ReduceUserFileStorage(vSession.User.Id, Convert.ToDouble(fileContent.ContentLength), session);
                                        if (!success)
                                            throw new Exception(string.Format("User {0} tried to upload a preview icon at {1} but he has not enough storage space from his packet (user billing packet type id = {2}), so it could not be saved or uploaded!", vSession.User.Id, DateTime.Now.ToString(), vSession.User.BillingType.ToString()));

                                        #endregion
                                    }

                                    session.CommitTransaction();

                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    return false;
                                }
                            }
                            else
                            {
                                //LblFileUploadTitle.Text = "File Uploading Warning";
                                string contentAlert = "Preview File could not be uploaded to your library (maybe it's size was outside the bounds). Please try again or contact with us.";
                                GlobalMethods.ShowMessageControlDA(UcPreviewFileUploadAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);

                                FixTabMenu();

                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            LblFileUploadTitle.Text = "File Uploading Warning";
                            string contentAlert = "File could not be uploaded to your onboarding library";
                            GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);

                            bool deleted = false;

                            if (successUpload)
                                deleted = UpLoadImage.DeleteFileFromDirectory(serverMapPathTargetFolder, fileName);

                            Logger.DetailedError(Request.Url.ToString(), (deleted) ? string.Format("Preview File not deleted successfully int path {0}", serverMapPathTargetFolder + "\\" + fileName) : "", ex.Message.ToString(), ex.StackTrace.ToString());

                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void BuildCategoriesRepeaterItems(RepeaterItem item)
        {
            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");
            //HtmlAnchor aTabCategory = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aTabCategory");

            //aTabCategory.Attributes["class"] = "navi-link active";

            if (hdnId != null)
                GetCategoriesFilesCountNotificationsByCategoryId(item, Convert.ToInt32(hdnId.Value));
        }

        private void FillPartnersList()
        {
            if (session.Connection.State == ConnectionState.Closed)
                session.OpenConnection();

            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            List<ElioUsers> partners = null;

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                partners = loader.Load(@"SELECT u.id,u.company_name
                                            FROM elio_users u
                                            inner join Elio_collaboration_vendors_resellers cvr
	                                            on u.id = cvr.partner_user_id
                                            where cvr.master_user_id = @master_user_id
                                            and cvr.invitation_status = 'Confirmed'
                                            and cvr.is_active = 1
                                            and u.company_type = 'Channel Partners'
                                            order by u.company_name"
                                    , DatabaseHelper.CreateIntParameter("@master_user_id", vSession.User.Id));
            }
            else
            {
                partners = loader.Load(@"SELECT u.id,u.company_name
                                            FROM elio_users u
                                            inner join Elio_collaboration_vendors_resellers cvr
	                                            on u.id = cvr.master_user_id
                                            where cvr.partner_user_id = @partner_user_id
                                            and cvr.invitation_status = 'Confirmed'
                                            and cvr.is_active = 1
                                            and u.company_type = 'Vendors'
                                            order by u.company_name"
                                    , DatabaseHelper.CreateIntParameter("@partner_user_id", vSession.User.Id));
            }

            if (partners.Count == 0)
            {
                GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, "Sorry, you have no partners to send files.", MessageTypes.Error, true, true, true, true, false);
            }
        }

        private List<int> HasSelectedItem(Repeater rpt)
        {
            List<int> vendResIDs = new List<int>();

            foreach (RepeaterItem item in rpt.Items)
            {
                CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                if (cbx.Checked)
                {
                    HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "id");
                    vendResIDs.Add(Convert.ToInt32(hdnId.Value));
                }
            }

            return vendResIDs;
        }

        private void ShowNotificationsMessages()
        {
            int simpleMsgCount = 0;
            int groupMsgCount = 0;
            int isNew = 1;
            int isViewed = 0;
            int isDeleted = 0;
            int isPublic = 1;

            int totalNewMessagesCount = SqlCollaboration.GetUserTotalSimpleNewUnreadMailBoxMessagesNotification(vSession.User.Id, vSession.User.CompanyType, isNew, isViewed, isDeleted, isPublic, out simpleMsgCount, out groupMsgCount, session);
            if (totalNewMessagesCount > 0)
            {
                if (simpleMsgCount > 0)
                {
                    //spanConnectionsMsgCount.Visible = true;
                    //LblConnectionsMsgCount.Text = simpleMsgCount.ToString();
                }
                else
                {
                    //spanConnectionsMsgCount.Visible = false;
                    //LblConnectionsMsgCount.Text = "";
                }

                if (groupMsgCount > 0)
                {
                    //spanGroupMsgCount.Visible = true;
                    //LblGroupMsgCount.Text = groupMsgCount.ToString();
                }
                else
                {
                    //spanGroupMsgCount.Visible = false;
                    //LblGroupMsgCount.Text = "";
                }
            }
            else
            {
                //spanConnectionsMsgCount.Visible = false;
                //spanGroupMsgCount.Visible = false;
                //LblConnectionsMsgCount.Text = "";
                //LblGroupMsgCount.Text = "";
            }

            //this.Master.ShowMasterNotifications(out simpleMsgCount, out groupMsgCount, out totalNewMessagesCount);
        }

        private void RestoreCheckBoxes(RadGrid rdg, GridDataItem rdgChechedItem, Repeater rpt, RepeaterItem rptCheckedItem)
        {
            try
            {
                if (rdg != null)
                {
                    #region Grid

                    foreach (GridDataItem item in rdg.Items)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectGroupUsers");
                        if (cbx != null && rdgChechedItem != null)
                        {
                            if (item.ClientID != rdgChechedItem.ClientID)
                                cbx.Checked = false;
                        }
                    }

                    #endregion
                }
                else if (rpt != null)
                {
                    
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void ShowSelectedChatPartnersName(int groupId)
        {
            //if (divTabConnections.Visible)
            //{
            //    if (vSession.VendorsResellersList.Count > 0)
            //    {
            //        ElioUsers partner = Sql.GetUserById((vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.VendorsResellersList[0].PartnerUserId : vSession.VendorsResellersList[0].MasterUserId, session);
            //        if (partner != null)
            //        {
            //            LblSelectedChatTitle.Text = "You are chatting with";
            //            LblSelectedConnectionToChat.Text = partner.CompanyName;
            //            aChatCompanyLogo.HRef = ControlLoader.Profile(partner);
            //            aChatCompanyLogo.Target = "_blank";
            //            aChatCompanyLogo.Visible = true;
            //            ImgChatCompanyLogo.ImageUrl = partner.CompanyLogo;
            //            ImgChatCompanyLogo.Visible = true;
            //            ImgChatCompanyLogo.ToolTip = "See " + partner.CompanyName + "'s profile";
            //        }
            //        else
            //        {
            //            ResetChatPartnersData();
            //        }
            //    }
            //    else
            //    {
            //        ResetChatPartnersData();
            //    }
            //}
            //else if (divTabGroups.Visible)
            //{
            //    ResetChatPartnersData();

            //    if (groupId > 0)
            //    {
            //        ElioCollaborationUsersGroups group = SqlCollaboration.GetCollaborationUserGroupById(groupId, session);
            //        if (group != null)
            //        {
            //            LblSelectedChatTitle.Text = "You are chatting with your group";
            //            LblSelectedConnectionToChat.Text = group.CollaborationGroupName;

            //            ShowGroupMembersImages(group.Id);
            //        }
            //        else
            //        {
            //            LblSelectedChatTitle.Text = "Select group to chat with it's members";
            //            LblSelectedConnectionToChat.Text = "";
            //            divGroupImages.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        LblSelectedChatTitle.Text = "Select group to chat with it's members";
            //        LblSelectedConnectionToChat.Text = "";
            //        divGroupImages.Visible = false;
            //    }
            //}
        }        
        
        private bool ValidatePreviewFile(out string alert)
        {
            alert = "";

            # region check file

            var fileContent = inputPreviewFile.PostedFile;

            if (fileContent != null && fileContent.ContentLength > 0)
            {
                var fileSize = fileContent.ContentLength;
                var fileType = fileContent.ContentType;

                int maxCollaborationFileLenght = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CollaborationMaxFileLenght"]);

                if (fileSize > maxCollaborationFileLenght)
                {
                    alert = "Your icon size is outside the bounds. Please try smaller icon size or contact us.";

                    return false;
                }

                if (fileContent.FileName == "")
                {
                    alert = "Your icon file name is empty. Please try naming your file or contact us.";

                    return false;
                }
                else
                {
                    if (fileContent.FileName.Length > 150)
                    {
                        alert = "Your icon name characters length is outside the bounds. Please try smaller icon name size or contact us.";

                        return false;
                    }
                }

                #region User Available File Storage Space

                if (!GlobalDBMethods.UserHasAvailableStorage(vSession.User.Id, fileContent.ContentLength, session))
                {
                    alert = " Your available storage space is not enough for this icon to be uploaded.";
                    return false;
                }

                #endregion
            }
            else
            {
                alert = "Nothing was uploaded because you did not select a file! Please try again.";

                return false;
            }

            #endregion

            return true;
        }

        #endregion

        #region Buttons

        protected void aSendFile_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                FillDropLists();                
                ResetPanelItems();
                FixCategoriesItems();
                //FillPartnersList();

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenUploadSendFilePopUp();", true);
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

        protected void aTabCategory_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    bool isClosed = false;

                    if (session.Connection.State == ConnectionState.Closed)
                    {
                        session.OpenConnection();
                        isClosed = true;
                    }

                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");

                            if (hdnId != null)
                            {
                                CategoryIdView = Convert.ToInt32(hdnId.Value);
                                LoadGridData(Rdg1, CategoryIdView, UcRgd1);

                                imgBtn.Attributes["class"] = "navi-link active";
                            }
                        }
                    }

                    FixCategoriesItems();

                    if (isClosed)
                        session.CloseConnection();

                    UpdatePanelContent.Update();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aAdd_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    UcMessageAlert.Visible = false;

                    if (TbxAddNewCategory.Text != "")
                    {
                        bool existCategoryDescription = SqlCollaboration.ExistLibraryCategoryDescription(vSession.User.Id, TbxAddNewCategory.Text.Trim(), true, session);
                        if (existCategoryDescription)
                        {
                            GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Sorry, this category description already exists for you! Please try different description", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                        else
                        {
                            DataLoader<ElioOnboardingUsersLibraryFilesCategories> loader = new DataLoader<ElioOnboardingUsersLibraryFilesCategories>(session);

                            ElioOnboardingUsersLibraryFilesCategories category = new ElioOnboardingUsersLibraryFilesCategories();

                            category.UserId = vSession.User.Id;
                            category.CategoryDescription = TbxAddNewCategory.Text;
                            category.DateCreated = DateTime.Now;
                            category.LastUpdate = DateTime.Now;
                            category.IsPublic = 1;
                            category.IsDefault = 0;

                            loader.Insert(category);

                            FillDropLists();
                            FixTabMenu();

                            TbxAddNewCategory.Text = "";

                            GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "New Category added successfully", MessageTypes.Success, true, true, true, true, false);
                        }
                    }
                    else
                    {
                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Please add new category description", MessageTypes.Error, true, true, true, true, false);
                        return;
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

        protected void aEdit_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;

                if (vSession.User != null)
                {
                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");
                            TextBox tbxEditCategory = (TextBox)ControlFinder.FindControlRecursive(item, "TbxEditCategory");
                            Label lblDescription = (Label)ControlFinder.FindControlRecursive(item, "LblDescription");
                            HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                            HtmlGenericControl iEdit = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "iEdit");
                            HtmlGenericControl spanCategoryAreaView = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanCategoryAreaView");
                            HtmlGenericControl spanCategoryAreaEdit = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanCategoryAreaEdit");
                            HtmlAnchor aTabCategory = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aTabCategory");
                            Label lblEditSave = (Label)ControlFinder.FindControlRecursive(item, "LblEditSave");

                            if (lblDescription.Visible)
                            {
                                aTabCategory.Visible = false;
                                iEdit.Attributes["class"] = "flaticon2-add";
                                lblEditSave.Text = "Save";
                                spanCategoryAreaEdit.Visible = true;
                                //spanCategoryAreaEdit.Attributes["style"] = "width: 225px; float: right; display: inline-block;margin-top:-10px;";
                            }
                            else
                            {
                                if (tbxEditCategory.Text != "")
                                {
                                    if (lblDescription.Text != tbxEditCategory.Text)
                                    {
                                        #region Update Category Description

                                        bool success = SqlCollaboration.UpdateUserCollaborationLibraryFileCategoryDescription(vSession.User.Id, Convert.ToInt32(hdnId.Value), tbxEditCategory.Text.Trim(), true, session);
                                        if (!success)
                                        {
                                            //GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Sorry, this category description already exists for you! Please try different description", MessageTypes.Error, true, true, true, true, false);
                                            GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Sorry, category description could not be updated! Please try again later or contact us.", MessageTypes.Error, true, true, true, true, false);
                                            return;
                                        }

                                        #endregion

                                        #region Rename file path of files to new cateogry description / Rename Folder Name

                                        bool successUpdateTransfer = GlobalDBMethods.UpdateUserCollaborationLibraryFilesPathDirectoryAndMoveFiles(Server, vSession.User.Id, Convert.ToInt32(hdnId.Value), lblDescription.Text, tbxEditCategory.Text, true, session);
                                        if (!successUpdateTransfer)
                                        {
                                            Logger.DetailedError(Request.Url.ToString(), "DashboardCollaborationLibraryPageNew.cs --> aEdit_ServerClick --> ERROR", string.Format("category description from {0} to {1} could not be updated in files path in DB for user {2} and category ID {3}", lblDescription.Text, tbxEditCategory.Text, vSession.User.Id, hdnId.Value));
                                            //GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Sorry, category description could not be updated in files path!", MessageTypes.Error, true, true, true, true, false);
                                            //return;
                                        }

                                        #endregion

                                        LoadGridData(Rdg1, Convert.ToInt32(hdnId.Value), UcRgd1);

                                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Description updated successfully", MessageTypes.Success, true, true, true, true, false);
                                    }

                                    lblDescription.Text = tbxEditCategory.Text;
                                    spanCategoryAreaEdit.Visible = false;
                                    lblEditSave.Text = "Edit";
                                    iEdit.Attributes["class"] = "flaticon2-edit";
                                    aTabCategory.Visible = true;
                                    //spanCategoryAreaView.Attributes["style"] = "width: 225px; float: right; display: inline-block;";
                                }
                                else
                                {
                                    GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Please add category description", MessageTypes.Error, true, true, true, true, false);
                                    return;
                                }
                            }

                            //GetCategoriesNewFilesNotificationsByCategoryId(item, "Elio_collaboration_users_library_files", Convert.ToInt32(hdnId.Value));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Sorry, something went wrong! Please try again later or contact us.", MessageTypes.Error, true, true, true, true, false);
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnUploadFile_OnCick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                                
                UcMessageAlert.Visible = UcReceiversAndUploadFile.Visible = false;

                if (UploadedFilesCount == 0)
                {
                    string contentAlert = "No File has been selected for upload to your onboarding library. Please select at least one.";
                    GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                    return;
                }
                else
                {
                    string content = UploadedFilesCount == 1 ? UploadedFilesCount + " File successfully uploaded." : UploadedFilesCount + " Files successfully uploaded.";

                    GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, content, MessageTypes.Success, true, true, true, true, false);

                    try
                    {
                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            List<string> emails = SqlCollaboration.GetResellersEmailByVendorUserId(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, CollaborateInvitationStatus.Confirmed.ToString(), session);

                            if (emails.Count > 0)
                                EmailSenderLib.SendNewUploadedFileEmail(vSession.User.CompanyName, emails, false, vSession.Lang, session);
                            else
                                Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, uploaded new onboarding file at {1}, but no collaboration partner email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardPartnerOnboarding.aspx --> ERROR sending notification Email");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                FixCategoriesItems();
                CategoryIdView = Convert.ToInt32(Ddlcategory.SelectedValue);
                LoadGridData(Rdg1, CategoryIdView, UcRgd1);
                UpdatePanelContent.Update();
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, "File could not be uploaded to your onboarding library", MessageTypes.Success, true, true, true, true, false);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void aDelete1_OnClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId1");
                        HiddenField hdnCategoryId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId1");
                        HiddenField hdnFileTypeId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileTypeId1");
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName1");

                        if (hdnId != null && hdnCategoryId != null && hdnFileTypeId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            FileTypeIdToDelete = Convert.ToInt32(hdnFileTypeId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile(FileIdToDelete, FileTypeIdToDelete, FileCategoryIdToDelete, FileNameToDelete);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDelete2_OnClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId2");
                        HiddenField hdnCategoryId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId2");
                        HiddenField hdnFileTypeId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileTypeId2");
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName2");

                        if (hdnId != null && hdnCategoryId != null && hdnFileTypeId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            FileTypeIdToDelete = Convert.ToInt32(hdnFileTypeId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile(FileIdToDelete, FileTypeIdToDelete, FileCategoryIdToDelete, FileNameToDelete);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDelete3_OnClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId3");
                        HiddenField hdnCategoryId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId3");
                        HiddenField hdnFileTypeId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileTypeId3");
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName3");

                        if (hdnId != null && hdnCategoryId != null && hdnFileTypeId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            FileTypeIdToDelete = Convert.ToInt32(hdnFileTypeId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile(FileIdToDelete, FileTypeIdToDelete, FileCategoryIdToDelete, FileNameToDelete);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDelete4_OnClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId4");
                        HiddenField hdnCategoryId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId4");
                        HiddenField hdnFileTypeId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileTypeId4");
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName4");

                        if (hdnId != null && hdnCategoryId != null && hdnFileTypeId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            FileTypeIdToDelete = Convert.ToInt32(hdnFileTypeId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile(FileIdToDelete, FileTypeIdToDelete, FileCategoryIdToDelete, FileNameToDelete);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnDeleteFile_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;
                bool deleted = false;
                if (FileTypeIdToDelete > 0 && FileIdToDelete > 0 && !string.IsNullOrEmpty(FileNameToDelete))
                {
                    ElioOnboardingFileTypes fileType = Sql.GetOnboardingPublicFileTypeById(FileTypeIdToDelete, session);
                    if (fileType != null)
                    {
                        try
                        {
                            session.BeginTransaction();

                            Sql.DeleteOrUpdateUserOnboardingLibraryFileAndBlobById(vSession.User, FileIdToDelete, false, session);
                            deleted = true;
                            session.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            deleted = false;
                        }

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                        if (deleted)
                        {
                            ElioOnboardingUsersLibraryFilesCategories fCategory = Sql.GetOnboardingUserLibraryFilesCategoriesById(FileCategoryIdToDelete, vSession.User.Id, session);
                            if (fCategory != null)
                            {                                
                                DirectoryInfo filesInDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["OnboardingLibraryFullTargetFolder"].ToString() + vSession.User.GuId + "\\" + fCategory.CategoryDescription + "\\" + fileType.FileType + "\\"));

                                try
                                {
                                    foreach (FileInfo logFile in filesInDirectory.GetFiles())
                                    {
                                        if (logFile.Name == FileNameToDelete)
                                        {
                                            logFile.Delete();

                                            FileTypeIdToDelete = -1;
                                            FileIdToDelete = -1;
                                            FileCategoryIdToDelete = -1;
                                            FileNameToDelete = "";

                                            break;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }

                                FixTabMenu();
                                CategoryIdView = fCategory.Id;
                                LoadGridData(Rdg1, CategoryIdView, UcRgd1);
                                UpdatePanelContent.Update();
                            }

                            GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "File was deleted successfully", MessageTypes.Success, true, true, true, true, false);
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "11")).Text;

                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, false, true, true, false);

                        //GetCategoriesNewFilesNotifications("Elio_onboarding_users_library_files");
                    }
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "11")).Text;

                    GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, true, true, false);

                    //GetCategoriesNewFilesNotifications("Elio_onboarding_users_library_files");
                }
            }
            catch (Exception ex)
            {
                LblFileUploadTitle.Text = "Delete File Warning";
                string content = "File could not be deleted. Please try again later or contact with us.";
                GlobalMethods.ShowMessageControlDA(UploadMessageAlert, content, MessageTypes.Error, true, true, true, true, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                //GetCategoriesNewFilesNotifications("Elio_onboarding_users_library_files");

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void aDeletePreviewImg1_ServerClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId1");
                        HiddenField hdnCategoryId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId1");
                        HiddenField hdnPreviewFilePath = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnPreviewFilePath1");

                        if (hdnId != null && hdnCategoryId != null && hdnPreviewFilePath != null)
                        {
                            LibraryFileId = Convert.ToInt32(hdnId.Value);
                            LibraryFileCategoryId = Convert.ToInt32(hdnCategoryId.Value);
                            string[] previewFilePath = hdnPreviewFilePath.Value.Split('/').ToArray();
                            if (previewFilePath.Length > 0)
                                LibraryPreviewFileName = previewFilePath[previewFilePath.Length - 1];

                            DeletePreviewFile();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDeletePreviewImg2_ServerClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId2");
                        HiddenField hdnCategoryId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId2");
                        HiddenField hdnPreviewFilePath = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnPreviewFilePath2");

                        if (hdnId != null && hdnCategoryId != null && hdnPreviewFilePath != null)
                        {
                            LibraryFileId = Convert.ToInt32(hdnId.Value);
                            LibraryFileCategoryId = Convert.ToInt32(hdnCategoryId.Value);
                            string[] previewFilePath = hdnPreviewFilePath.Value.Split('/').ToArray();
                            if (previewFilePath.Length > 0)
                                LibraryPreviewFileName = previewFilePath[previewFilePath.Length - 1];

                            DeletePreviewFile();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDeletePreviewImg3_ServerClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId3");
                        HiddenField hdnCategoryId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId3");
                        HiddenField hdnPreviewFilePath = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnPreviewFilePath3");

                        if (hdnId != null && hdnCategoryId != null && hdnPreviewFilePath != null)
                        {
                            LibraryFileId = Convert.ToInt32(hdnId.Value);
                            LibraryFileCategoryId = Convert.ToInt32(hdnCategoryId.Value);
                            string[] previewFilePath = hdnPreviewFilePath.Value.Split('/').ToArray();
                            if (previewFilePath.Length > 0)
                                LibraryPreviewFileName = previewFilePath[previewFilePath.Length - 1];

                            DeletePreviewFile();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDeletePreviewImg4_ServerClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId4");
                        HiddenField hdnCategoryId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId4");
                        HiddenField hdnPreviewFilePath = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnPreviewFilePath4");

                        if (hdnId != null && hdnCategoryId != null && hdnPreviewFilePath != null)
                        {
                            LibraryFileId = Convert.ToInt32(hdnId.Value);
                            LibraryFileCategoryId = Convert.ToInt32(hdnCategoryId.Value);
                            string[] previewFilePath = hdnPreviewFilePath.Value.Split('/').ToArray();
                            if (previewFilePath.Length > 0)
                                LibraryPreviewFileName = previewFilePath[previewFilePath.Length - 1];

                            DeletePreviewFile();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnDeletePreviewFile_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;
                bool deleted = false;

                if (LibraryFileId > 0 && LibraryFileCategoryId > 0 && LibraryPreviewFileName != "")
                {
                    try
                    {
                        session.BeginTransaction();

                        deleted = SqlCollaboration.DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(vSession.User, LibraryFileId, true, true, session);
                        //deleted = true;
                        session.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        deleted = false;
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                    if (deleted)
                    {
                        ElioCollaborationUsersLibraryFilesCategories userCategory = SqlCollaboration.GetCollaborationUserLibraryPublicFilesCategoriesById(vSession.User.Id, LibraryFileCategoryId, session);
                        if (userCategory != null)
                        {
                            DirectoryInfo filesInDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryFullTargetFolder"].ToString() + vSession.User.GuId + "\\" + userCategory.CategoryDescription + "\\PreviewFiles\\"));

                            string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + userCategory.CategoryDescription + "\\PreviewFiles\\";
                            
                            try
                            {
                                if (Directory.Exists(serverMapPathTargetFolder))
                                {
                                    foreach (FileInfo logFile in filesInDirectory.GetFiles())
                                    {
                                        if (logFile.Name == LibraryPreviewFileName)
                                        {
                                            logFile.Delete();

                                            break;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }

                    CategoryIdView = LibraryFileCategoryId;
                    LoadGridData(Rdg1, CategoryIdView, UcRgd1);

                    LibraryFileId = -1;
                    LibraryFileCategoryId = -1;
                    LibraryPreviewFileName = "";

                    UpdatePanelContent.Update();

                    GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Preview icon was deleted successfully", MessageTypes.Success, true, true, true, true, false);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "21")).Text;
                    GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, true, true, false);

                    FixTabMenu();
                }
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                string content = "Preview icon could not be deleted. Please try again later or contact us.";
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, content, MessageTypes.Error, true, true, true, true, false);

                FixTabMenu();

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void HpLnkFile1_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnIsNew = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsNew1");
                        HiddenField hdnPath = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFilePath1");
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId1");
                        DANotificationControl ucNotification = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif1");

                        if (hdnPath != null && hdnId != null && hdnIsNew != null)
                            PreviewFile(Convert.ToInt32(hdnIsNew.Value), hdnPath.Value, Convert.ToInt32(hdnId.Value), ucNotification, session);

                        if (hdnIsNew.Value == "1")
                            hdnIsNew.Value = "0";

                        //GetCategoriesNewFilesNotificationsByCategoryId(item, "Elio_collaboration_users_library_files", Convert.ToInt32(hdnId.Value));
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

        protected void HpLnkFile2_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnIsNew = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsNew2");
                        HiddenField hdnPath = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFilePath2");
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId2");
                        DANotificationControl ucNotification = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif2");

                        if (hdnPath != null && hdnId != null && hdnIsNew != null)
                            PreviewFile(Convert.ToInt32(hdnIsNew.Value), hdnPath.Value, Convert.ToInt32(hdnId.Value), ucNotification, session);

                        if (hdnIsNew.Value == "1")
                            hdnIsNew.Value = "0";

                        //GetCategoriesNewFilesNotificationsByCategoryId(item, "Elio_collaboration_users_library_files", Convert.ToInt32(hdnId.Value));
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

        protected void HpLnkFile3_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnIsNew = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsNew3");
                        HiddenField hdnPath = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFilePath3");
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId3");
                        DANotificationControl ucNotification = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif3");

                        if (hdnPath != null && hdnId != null && hdnIsNew != null)
                            PreviewFile(Convert.ToInt32(hdnIsNew.Value), hdnPath.Value, Convert.ToInt32(hdnId.Value), ucNotification, session);

                        if (hdnIsNew.Value == "1")
                            hdnIsNew.Value = "0";

                        //GetCategoriesNewFilesNotificationsByCategoryId(item, "Elio_collaboration_users_library_files", Convert.ToInt32(hdnId.Value));
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

        protected void HpLnkFile4_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnIsNew = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsNew4");
                        HiddenField hdnPath = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFilePath4");
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId4");
                        DANotificationControl ucNotification = (DANotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif4");

                        if (hdnPath != null && hdnId != null && hdnIsNew != null)
                            PreviewFile(Convert.ToInt32(hdnIsNew.Value), hdnPath.Value, Convert.ToInt32(hdnId.Value), ucNotification, session);

                        if (hdnIsNew.Value == "1")
                            hdnIsNew.Value = "0";

                        //GetCategoriesNewFilesNotificationsByCategoryId(item, "Elio_collaboration_users_library_files", Convert.ToInt32(hdnId.Value));
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

        protected void ImgBtnAddLibraryFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!tr2.Visible)
                {
                    tr2.Visible = true;
                    return;
                }
                else if (!tr3.Visible)
                {
                    tr3.Visible = true;
                    return;
                }
                else if (!tr4.Visible)
                {
                    tr4.Visible = true;
                    return;
                }
                else if (!tr5.Visible)
                {
                    tr5.Visible = true;
                    return;
                }
                else if (!tr6.Visible)
                {
                    tr6.Visible = true;
                    return;
                }
                else if (!tr7.Visible)
                {
                    tr7.Visible = true;
                    return;
                }
                else if (!tr8.Visible)
                {
                    tr8.Visible = true;
                    return;
                }
                else if (!tr9.Visible)
                {
                    tr9.Visible = true;
                    return;
                }
                else
                    tr10.Visible = true;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnDeleteLibraryFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (tr10.Visible)
                {
                    tr10.Visible = false;
                    return;
                }
                else if (tr9.Visible)
                {
                    tr9.Visible = false;
                    return;
                }
                else if (tr8.Visible)
                {
                    tr8.Visible = false;
                    return;
                }
                else if (tr7.Visible)
                {
                    tr7.Visible = false;
                    return;
                }
                else if (tr6.Visible)
                {
                    tr6.Visible = false;
                    return;
                }
                else if (tr5.Visible)
                {
                    tr5.Visible = false;
                    return;
                }
                else if (tr4.Visible)
                {
                    tr4.Visible = false;
                    return;
                }
                else if (tr3.Visible)
                {
                    tr3.Visible = false;
                    return;
                }
                else
                    tr2.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDelete_ServerClick(object sender, EventArgs e)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");

                        if (hdnId != null)
                        {
                            FileCategoryIdToDelete = Convert.ToInt32(hdnId.Value);
                            DeleteCategory(FileCategoryIdToDelete);

                            //GetCategoriesNewFilesNotificationsByCategoryId(item, "Elio_collaboration_users_library_files", Convert.ToInt32(hdnId.Value));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnDeleteCategory_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;
                bool deleted = false;
                if (FileCategoryIdToDelete > 0)
                {
                    try
                    {
                        session.BeginTransaction();

                        deleted = SqlCollaboration.DeleteUserCollaborationLibraryCategoryFilesAndBlobById(vSession.User, FileCategoryIdToDelete, true, true, session);
                        //deleted = true;
                        session.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        deleted = false;
                        throw ex;
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                    if (deleted)
                    {
                        ElioOnboardingUsersLibraryFilesCategories userCategory = SqlCollaboration.GetOnboardingUserLibraryPublicFilesCategoriesById(vSession.User.Id, FileCategoryIdToDelete, session);
                        if (userCategory != null)
                        {
                            DirectoryInfo filesInDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["OnboardingLibraryFullTargetFolder"].ToString() + vSession.User.GuId + "\\" + userCategory.CategoryDescription + "\\"));

                            foreach (FileInfo logFile in filesInDirectory.GetFiles())
                            {
                                logFile.Delete();
                            }
                        }

                        FileCategoryIdToDelete = -1;
                        CategoryIdView = -1;

                        FixTabMenu();
                        LoadGridData(Rdg1, CategoryIdView, UcRgd1);

                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Category and all its files were deleted successfully", MessageTypes.Success, true, true, true, true, false);

                        UpdatePanelContent.Update();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "19")).Text;
                    GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, true, true, false);

                    FixTabMenu();
                }
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                string content = "Category could not be deleted. Please try again later or contact with us.";
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, content, MessageTypes.Error, true, true, true, true, false);

                FixTabMenu();

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void aBtnUploadPreviewFile_OnCick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                bool successUpload = false;
                UcPreviewFileUploadAlert.Visible = false;
                string alert = "";

                if (LibraryFileId > 0 && LibraryFileCategoryId > 0)
                {
                    if (!ValidatePreviewFile(out alert))
                    {
                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, true, true, false);
                        return;
                    }

                    HttpPostedFile fileContent = null;

                    fileContent = inputPreviewFile.PostedFile;
                    if (fileContent != null)
                    {
                        successUpload = UploadPreviewFile(fileContent);
                    }

                    if (!successUpload)
                    {
                        string contentAlert = "Preview icon " + fileContent.FileName + " could not be uploaded for this file.";
                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);
                        return;
                    }
                    else
                    {
                        string content = "Preview icon was successfully uploaded for this file.";

                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, content, MessageTypes.Success, true, true, true, true, false);
                    }

                    LoadGridData(Rdg1, CategoryIdView, UcRgd1);
                    UpdatePanelContent.Update();
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

        protected void aUploadPreviewImg1_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LibraryFileId = -1;
                LibraryFileCategoryId = -1;

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId1 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId1");
                        HiddenField hdnCategoryId1 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId1");
                        LibraryFileId = Convert.ToInt32(hdnId1.Value);
                        LibraryFileCategoryId = Convert.ToInt32(hdnCategoryId1.Value);

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenUploadPreviewFilePopUp();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aUploadPreviewImg2_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LibraryFileId = -1;
                LibraryFileCategoryId = -1;

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId2 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId2");
                        HiddenField hdnCategoryId2 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId2");
                        LibraryFileId = Convert.ToInt32(hdnId2.Value);
                        LibraryFileCategoryId = Convert.ToInt32(hdnCategoryId2.Value);

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenUploadPreviewFilePopUp();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aUploadPreviewImg3_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LibraryFileId = -1;
                LibraryFileCategoryId = -1;

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId3 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId3");
                        HiddenField hdnCategoryId3 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId3");
                        LibraryFileId = Convert.ToInt32(hdnId3.Value);
                        LibraryFileCategoryId = Convert.ToInt32(hdnCategoryId3.Value);

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenUploadPreviewFilePopUp();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aUploadPreviewImg4_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LibraryFileId = -1;
                LibraryFileCategoryId = -1;

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId4 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId4");
                        HiddenField hdnCategoryId4 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCategoryId4");
                        LibraryFileId = Convert.ToInt32(hdnId4.Value);
                        LibraryFileCategoryId = Convert.ToInt32(hdnCategoryId4.Value);

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenUploadPreviewFilePopUp();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aSearch_ServerClick(object sender, EventArgs e)
        {
            try
            {              
                FixCategoriesItems();
                LoadGridData(Rdg1, CategoryIdView, UcRgd1);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion

        #region Grids

        protected void RdgCategories_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        if (vSession.User != null)
                        {
                            BuildCategoriesRepeaterItems(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgCategories_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                        FixTabMenu();
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

        protected void Rdg1_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        if (vSession.User != null)
                        {
                            BuildRepeaterItem(args);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Rdg1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    LoadGridData(Rdg1, CategoryIdView, UcRgd1);
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

        #region Tabs

        #endregion

        #region DropDownLists

        protected void DrpVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (DrpVendors.SelectedValue != "" && DrpVendors.SelectedValue != "0")
                {
                    FixTabMenu();
                    LoadGridData(Rdg1, CategoryIdView, UcRgd1);
                }
                else
                {
                    Rdg1.Visible = false;
                    GlobalMethods.ShowMessageControlDA(UcRgd1, "You must select a vendor first", MessageTypes.Info, true, true, true, true, false);
                }

                //GetCategoriesNewFilesNotifications("Elio_onboarding_users_library_files");
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

        #region CheckBoxes

        #endregion

        #region TextBoxes

        #endregion
    }
}