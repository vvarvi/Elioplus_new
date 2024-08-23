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
    public partial class DashboardCollaborationLibraryPageNew : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public const int CustomUSER = 39132;

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

        public int GroupId
        {
            get
            {
                if (ViewState["GroupId"] != null)
                    return Convert.ToInt32(ViewState["GroupId"]);
                else
                    return -1;
            }
            set
            {
                ViewState["GroupId"] = value;
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
                        Key = key;

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

                    #region To Delete

                    //Stream s = e.File.InputStream;

                    //Bitmap bitmapImage = ResizeImage(RadAsyncUpload1.UploadedFiles[0].InputStream);
                    //System.IO.MemoryStream stream = new System.IO.MemoryStream();
                    //bitmapImage.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    ////RadBinaryImage1.DataValue = stream.ToArray();

                    //foreach (UploadedFile file in RadAsyncUpload1.UploadedFiles)
                    //{
                    //    UploadedFileInfo uploadedFileInfo = new UploadedFileInfo(file);

                    //    List<Telerik.Web.UI.UploadedFileInfo> UploadedFiles = new List<UploadedFileInfo>();
                    //    UploadedFiles.Add(uploadedFileInfo);
                    //}

                    //UploadedFile f = RadAsyncUpload1.UploadedFiles[0];
                    //byte[] fileData = new byte[f.InputStream.Length];
                    //f.InputStream.Read(fileData, 0, (int)f.InputStream.Length);

                    #endregion

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
            FixTextAreasView(0);
            FillPartnersList();

            BtnUploadFile.Visible = BtnSendFile.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardPartnerOnboardingPage", Actions.Upload, session);

            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                //bool hasConfirmedInvitationPartners = SqlCollaboration.HasCollaborationPartnersOrMessages(vSession.User.Id, vSession.User.CompanyType, out hasMessages, session);
                //if (!hasConfirmedInvitationPartners)
                //{
                //    aInvitationToPartners.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-new-partners");
                //    divInvitationToPartners.Visible = true;
                //}
                //else
                //{
                //    divInvitationToPartners.Visible = false;
                //}

                divNaviItem.Visible = false;
                divVendorGroupsArea.Visible = false;
            }
            else
            {
                LoadStepOne();
                divNaviItem.Visible = true;
                divVendorGroupsArea.Visible = true;

                //divInvitationToPartners.Visible = false;

                FixUserLibraryGroups();
            }

            tab_1_1.Visible = true;
            tab_1_1.Attributes["class"] = "tab-pane fade show active";
            aIndividualPartners.Attributes["class"] = "nav-link active";

            tab_1_2.Visible = tab_1_3.Visible = false;
            tab_1_2.Attributes["class"] = tab_1_3.Attributes["class"] = "tab-pane fade";
            aGroupPartners.Attributes["class"] = aTiersPartners.Attributes["class"] = "nav-link";

            aRemoveReceivers.Visible = TbxReceiversPartners.Text != "";

            UcMessageAlert.Visible = false;
            MessageControlGroupMembers.Visible = false;
            UploadMessageAlert.Visible = false;
            vSession.VendorsResellersList.Clear();
            vSession.VendorsResellersList = null;
            GroupId = -1;
            IsEditMode = false;
            IsSendFiles = true;
            LibraryFileId = -1;
            LibraryFileCategoryId = -1;
            LibraryPreviewFileName = "";
            LibraryFileName = "";

            aSendFiles.Attributes["class"] = "nav-link active";
            aReceivedFiles.Attributes["class"] = "nav-link";
        }

        public Bitmap ResizeImage(Stream stream)
        {
            System.Drawing.Image originalImage = Bitmap.FromStream(stream);

            int height = 331;
            int width = 495;

            Bitmap scaledImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, width, height);
                g.DrawString("My photo from ", new Font("Tahoma", 18), Brushes.White, new PointF(0, 0));
                return scaledImage;
            }
        }

        private void GetCollaborationVendors()
        {
            List<ElioUsers> vendors = SqlCollaboration.GetCollaborationVendorsByResellerUserId(vSession.User.Id, CollaborateInvitationStatus.Confirmed.ToString(), session);
            if (vendors.Count > 0)
            {
                divVendorsList.Visible = true;

                DrpVendors.Items.Clear();
                DdlPartnersListSearch.Items.Clear();

                foreach (ElioUsers vendor in vendors)
                {
                    ListItem item = new ListItem();
                    item.Value = vendor.Id.ToString();
                    item.Text = vendor.CompanyName;

                    DrpVendors.Items.Add(item);
                    DdlPartnersListSearch.Items.Add(item);

                    if (vendor.Id == CustomUSER)
                        IsCustomVendor = true;
                }

                DrpVendors.Items.FindByValue(vendors[0].Id.ToString()).Selected = true;
                DrpVendors.SelectedItem.Value = vendors[0].Id.ToString();
                DrpVendors.SelectedItem.Text = vendors[0].CompanyName;

                DrpVendors.Enabled = (vendors.Count == 1) ? false : true;

                DdlPartnersListSearch.Items.FindByValue(vendors[0].Id.ToString()).Selected = true;
                DdlPartnersListSearch.SelectedItem.Value = vendors[0].Id.ToString();
                DdlPartnersListSearch.SelectedItem.Text = vendors[0].CompanyName;

                DdlPartnersListSearch.Enabled = (vendors.Count == 1) ? false : true;

                Key = Sql.GetUserGUIDByIdTbl(Convert.ToInt32(DrpVendors.SelectedValue), session);
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

        private void FixTextAreasView(int caseOrder)
        {
            if (caseOrder == 0)
            {
                TbxReceiversPartners.Rows = 1;
                TbxLibraryGroupsName.Rows = 1;
                TbxTiersPartners.Rows = 1;
            }
            else if (caseOrder == 1)
            {
                if (TbxReceiversPartners.Text.Length > 75)
                {
                    int rows = TbxReceiversPartners.Text.Length / 75;
                    int extra = TbxReceiversPartners.Text.Length % 75;

                    TbxReceiversPartners.Rows = (extra == 0) ? rows : rows + 1;
                    TbxReceiversPartners.TextMode = (TbxReceiversPartners.Rows > 1) ? TextBoxMode.MultiLine : TextBoxMode.SingleLine;
                }
                else
                    TbxReceiversPartners.Rows = 1;
            }
            else if (caseOrder == 2)
            {
                if (TbxLibraryGroupsName.Text.Length > 33)
                {
                    int rows = TbxLibraryGroupsName.Text.Length / 33;
                    int extra = TbxLibraryGroupsName.Text.Length % 33;

                    TbxLibraryGroupsName.Rows = (extra == 0) ? rows : rows + 1;
                    TbxLibraryGroupsName.TextMode = (TbxLibraryGroupsName.Rows > 1) ? TextBoxMode.MultiLine : TextBoxMode.SingleLine;
                }
                else
                    TbxLibraryGroupsName.Rows = 1;
            }
            else if (caseOrder == 3)
            {
                if (TbxTiersPartners.Text.Length > 33)
                {
                    int rows = TbxTiersPartners.Text.Length / 33;
                    int extra = TbxTiersPartners.Text.Length % 33;

                    TbxTiersPartners.Rows = (extra == 0) ? rows : rows + 1;
                    TbxTiersPartners.TextMode = (TbxTiersPartners.Rows > 1) ? TextBoxMode.MultiLine : TextBoxMode.SingleLine;
                }
                else
                    TbxTiersPartners.Rows = 1;
            }
        }

        private void GetCategoriesFilesCountNotificationsByCategoryId(RepeaterItem item, int categoryId)
        {
            DataTable newFilesNotifications = new DataTable();

            ElioUsers partner = Sql.GetUserByGuId(Key, session);

            string strQuery = @"
                                SELECT category_id, count(id) as count  
                                FROM Elio_collaboration_users_library_files
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
                if (DrpVendors.SelectedValue != "")
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
                                                    , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id)
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

                if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                {
                    #region Each User Available File Storage Space

                    List<string> partnersList = TbxReceiversPartners.Text.Trim().TrimEnd(';').Split(';').ToList();

                    if (partnersList.Count == 0)
                        return false;

                    string companyNames = "";
                    int count = 0;

                    foreach (string receiver in partnersList)
                    {
                        count++;
                        string partner = receiver.Trim().TrimEnd(' ').TrimStart(' ');

                        if (count < partnersList.Count)
                        {
                            if (partner.Trim() != "")
                            {
                                companyNames += "'" + partner + "',";
                            }
                        }
                        else
                        {
                            if (partner.Trim() != "")
                            {
                                companyNames += "'" + partner + "'";
                            }
                        }
                    }

                    List<ElioUsers> partners = Sql.GetUserPartnersIdsGuidsByCompaniesNamesByCompanyType(vSession.User.Id, vSession.User.CompanyType, companyNames, session);
                    foreach (ElioUsers partner in partners)
                    {
                        if (!GlobalDBMethods.UserHasAvailableStorage(partner.Id, fileContent.ContentLength, session))
                        {
                            alert = string.Format("Your vendor, {0},  has not enough available storage space to send him file {1}.", partner.CompanyName, fileContent.FileName);
                            return false;
                        }
                    }

                    #endregion
                }
                else
                {
                    if (!GlobalDBMethods.UserHasAvailableStorage(vSession.User.Id, fileContent.ContentLength, session))
                    {
                        alert = string.Format("Your available storage space is not enough to send file {0}", fileContent.FileName);
                        return false;
                    }
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

                if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                {
                    #region Each User Available File Storage Space

                    List<string> partnersList = TbxReceiversPartners.Text.Trim().TrimEnd(';').Split(';').ToList();

                    if (partnersList.Count == 0)
                        return false;

                    string companyNames = "";
                    int count = 0;

                    foreach (string receiver in partnersList)
                    {
                        count++;
                        string partner = receiver.Trim().TrimEnd(' ').TrimStart(' ');

                        if (count < partnersList.Count)
                        {
                            if (partner.Trim() != "")
                            {
                                companyNames += "'" + partner + "',";
                            }
                        }
                        else
                        {
                            if (partner.Trim() != "")
                            {
                                companyNames += "'" + partner + "'";
                            }
                        }
                    }

                    List<ElioUsers> partners = Sql.GetUserPartnersIdsGuidsByCompaniesNamesByCompanyType(vSession.User.Id, vSession.User.CompanyType, companyNames, session);
                    foreach (ElioUsers partner in partners)
                    {
                        if (!GlobalDBMethods.UserHasAvailableStorage(partner.Id, fileContent.ContentLength, session))
                        {
                            alert = string.Format("Your vendor, {0},  has not enough available storage space to send him file {1}.", partner.CompanyName, fileContent.FileName);
                            return false;
                        }
                    }

                    #endregion
                }
                else
                {
                    if (!GlobalDBMethods.UserHasAvailableStorage(vSession.User.Id, fileContent.ContentLength, session))
                    {
                        alert = string.Format("Your available storage space is not enough to send file {0}", fileContent.FileName);
                        return false;
                    }
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

        private void ResetPanelItems()
        {
            UcReceiversAndUploadFile.Visible = false;
            MessageControlCategories.Visible = false;
            MessageControlGroupMembers.Visible = false;

            TbxReceiversPartners.Text = "";

            aRemoveReceivers.Visible = TbxReceiversPartners.Text != "";

            FixTextAreasView(0);

            //if (Ddlcategory.SelectedValue != "0")
            //{
            //    Ddlcategory.SelectedIndex = -1;
            //    Ddlcategory.SelectedValue = "0";
            //    Ddlcategory.SelectedItem.Text = "Choose category";

            //    Ddlcategory2.SelectedIndex = Ddlcategory3.SelectedIndex = Ddlcategory4.SelectedIndex = Ddlcategory5.SelectedIndex = Ddlcategory6.SelectedIndex = Ddlcategory7.SelectedIndex = Ddlcategory8.SelectedIndex = Ddlcategory9.SelectedIndex = Ddlcategory10.SelectedIndex = -1;
            //    Ddlcategory2.SelectedValue = Ddlcategory3.SelectedValue = Ddlcategory4.SelectedValue = Ddlcategory5.SelectedValue = Ddlcategory6.SelectedValue = Ddlcategory7.SelectedValue = Ddlcategory8.SelectedValue = Ddlcategory9.SelectedValue = Ddlcategory10.SelectedValue = "0";
            //    Ddlcategory2.SelectedItem.Text = Ddlcategory3.SelectedItem.Text = Ddlcategory4.SelectedItem.Text = Ddlcategory5.SelectedItem.Text = Ddlcategory6.SelectedItem.Text = Ddlcategory7.SelectedItem.Text = Ddlcategory8.SelectedItem.Text = Ddlcategory9.SelectedItem.Text = Ddlcategory10.SelectedItem.Text = "Choose category";
            //}

            //DdlPartnersList.SelectedIndex = -1;
            ////DdlPartnersList.SelectedValue = "0";
            ////DdlPartnersList.SelectedItem.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Select partner" : "Select vendor";

            //DdlPartnersListSearch.SelectedIndex = -1;
            ////DdlPartnersListSearch.SelectedValue = "0";
            ////DdlPartnersListSearch.SelectedItem.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Select partner" : "Select vendor";

            TbxReceiversPartners.CssClass = DdlPartnersList.CssClass = TbxLibraryGroupsName.CssClass = TbxTiersPartners.CssClass = "form-control";

            divFileUpload.Visible = true;
            tr2.Visible = tr3.Visible = tr4.Visible = tr5.Visible = tr6.Visible = tr7.Visible = tr8.Visible = tr9.Visible = tr10.Visible = false;

            aIndividualPartners.Attributes["class"] = "nav-link active";
            tab_1_1.Attributes["class"] = "tab-pane fade show active";
            tab_1_1.Visible = true;

            aGroupPartners.Attributes["class"] = "nav-link";
            tab_1_2.Attributes["class"] = "tab-pane fade";
            tab_1_2.Visible = false;

            aTiersPartners.Attributes["class"] = "nav-link";
            tab_1_3.Attributes["class"] = "tab-pane fade";
            tab_1_3.Visible = false;
        }

        private void FixTabMenu()
        {
            MessageControlCategories.Visible = false;

            DataTable table = null;

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                table = SqlCollaboration.GetCollaborationUserLibraryPublicFilesCategoriesTbl(vSession.User.Id, session);

                if (table == null || (table != null && table.Rows.Count == 0))
                {
                    List<ElioCollaborationLibraryFilesDefaultCategories> defaultCategories = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategories(session);
                    if (defaultCategories.Count > 0)
                    {
                        foreach (ElioCollaborationLibraryFilesDefaultCategories category in defaultCategories)
                        {
                            ElioCollaborationUsersLibraryFilesCategories item = new ElioCollaborationUsersLibraryFilesCategories();

                            item.UserId = vSession.User.Id;
                            item.CategoryDescription = category.CategoryDescription;
                            item.DateCreated = DateTime.Now;
                            item.LastUpdate = DateTime.Now;
                            item.IsPublic = 1;
                            item.IsDefault = 1;

                            DataLoader<ElioCollaborationUsersLibraryFilesCategories> loader = new DataLoader<ElioCollaborationUsersLibraryFilesCategories>(session);
                            loader.Insert(item);
                        }
                    }

                    table = SqlCollaboration.GetCollaborationUserLibraryPublicFilesCategoriesTbl(vSession.User.Id, session);
                }
            }
            else
            {
                if (DrpVendors.SelectedValue != "")
                    table = SqlCollaboration.GetCollaborationUserLibraryPublicFilesCategoriesTbl(Convert.ToInt32(DrpVendors.SelectedValue), session);
            }

            if (table != null && table.Rows.Count > 0)
            {
                RdgCategories.Visible = true;
                BtnSendFile.Visible = true;

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

        private void FixUserLibraryGroups()
        {
            try
            {
                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                DataTable table = SqlCollaboration.GetCollaborationUserLibraryGroupsTbl(vSession.User.Id, session);

                if (table != null && table.Rows.Count > 0)
                {
                    MessageControlGroupMembers.Visible = false;
                    RptGroups.Visible = true;

                    RptGroups.DataSource = table;
                    RptGroups.DataBind();
                }
                else
                {
                    RptGroups.Visible = false;
                    GlobalMethods.ShowMessageControlDA(MessageControlGroupMembers, "No groups", MessageTypes.Info, true, true, true, false, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void FillDropLists()
        {
            MessageControlNoCategories.Visible = false;

            List<ElioCollaborationUsersLibraryFilesCategories> userCategories = null;

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                userCategories = SqlCollaboration.GetCollaborationUserLibraryPublicFilesCategories(vSession.User.Id, session);

                if (userCategories.Count == 0)
                {
                    List<ElioCollaborationLibraryFilesDefaultCategories> defaultCategories = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategories(session);
                    if (defaultCategories.Count > 0)
                    {
                        foreach (ElioCollaborationLibraryFilesDefaultCategories category in defaultCategories)
                        {
                            ElioCollaborationUsersLibraryFilesCategories item = new ElioCollaborationUsersLibraryFilesCategories();

                            item.UserId = vSession.User.Id;
                            item.CategoryDescription = category.CategoryDescription;
                            item.DateCreated = DateTime.Now;
                            item.LastUpdate = DateTime.Now;
                            item.IsPublic = 1;
                            item.IsDefault = 1;

                            DataLoader<ElioCollaborationUsersLibraryFilesCategories> loader = new DataLoader<ElioCollaborationUsersLibraryFilesCategories>(session);
                            loader.Insert(item);
                        }
                    }

                    userCategories = SqlCollaboration.GetCollaborationUserLibraryPublicFilesCategories(vSession.User.Id, session);
                }
            }
            else
            {
                if (DrpVendors.SelectedValue != "")
                    userCategories = SqlCollaboration.GetCollaborationUserLibraryPublicFilesCategories(Convert.ToInt32(DrpVendors.SelectedValue), session);
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

                foreach (ElioCollaborationUsersLibraryFilesCategories userCategory in userCategories)
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
                GlobalMethods.ShowMessageControlDA(MessageControlNoCategories, "Sorry, selected vendor has no library files categories yet.", MessageTypes.Error, true, true, true, false, false);
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

        private void DeleteFile()
        {
            if (FileIdToDelete > 0 && FileCategoryIdToDelete > 0 && FileNameToDelete != "")
            {
                BtnDeleteFile.Visible = true;
                BtnDeletePreviewFile.Visible = false;
                BtnDeleteCategory.Visible = false;
                BtnDeleteGroup.Visible = false;

                GlobalMethods.ShowMessageControlDA(UcConfirmationMessageControl, "If you proceed, this file saved in this category will be deleted. Do you want to delete this file?", MessageTypes.Error, true, true, false, false, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            else
            {
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "11")).Text;
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false, false, false);
            }
        }

        private void DeletePreviewFile()
        {
            if (LibraryFileId > 0 && LibraryFileCategoryId > 0 && LibraryPreviewFileName != "")
            {
                BtnDeletePreviewFile.Visible = true;
                BtnDeleteFile.Visible = false;
                BtnDeleteCategory.Visible = false;
                BtnDeleteGroup.Visible = false;

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
                BtnDeleteGroup.Visible = false;

                GlobalMethods.ShowMessageControlDA(UcConfirmationMessageControl, "If you proceed, all the files saved in this category will be deleted. Do you want to delete this category?", MessageTypes.Error, true, true, false, false, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            else
            {
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "19")).Text;
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false, false, false);
            }
        }

        private void DeleteGroup(int groupId)
        {
            if (groupId > -1)
            {
                BtnDeleteGroup.Visible = true;
                BtnDeleteFile.Visible = false;
                BtnDeletePreviewFile.Visible = false;
                BtnDeleteCategory.Visible = false;

                GlobalMethods.ShowMessageControlDA(UcConfirmationMessageControl, "If you proceed, all the files you have sent and received from this group will be deleted. Do you want to delete this group?", MessageTypes.Error, true, true, false, false, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            else
            {
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "20")).Text;
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false, false, false);
            }
        }

        //private void FixTabsContent(int categoryId)
        //{
        //    LoadGridData(Rdg1, categoryId, UcRgd1);
        //}

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

                    ElioUsers partner = Sql.GetUserByGuId(Key, session);

                    List<ElioCollaborationUsersLibraryFiles> files = new List<ElioCollaborationUsersLibraryFiles>();

                    if (partner == null)
                    {
                        partner = Sql.GetUserById(vSession.User.Id, session);

                        files = SqlCollaboration.GetLibraryFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, categoryID, "0,1", false, IsSendFiles, RdtDateFromSearch.SelectedDate != null ? RdtDateFromSearch.SelectedDate : null, RdtDateToSearch.SelectedDate != null ? RdtDateToSearch.SelectedDate : null, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);
                    }
                    else if (partner != null)        //(Key != "")
                    {
                        files = SqlCollaboration.GetLibraryFilesSendByUserSendToUserAndUsers(vSession.User.Id, partner.Id, categoryID, "0,1", true, IsSendFiles, RdtDateFromSearch.SelectedDate != null ? RdtDateFromSearch.SelectedDate : null, RdtDateToSearch.SelectedDate != null ? RdtDateToSearch.SelectedDate : null, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);

                        DdlPartnersListSearch.SelectedValue = partner.Id.ToString();
                        DdlPartnersListSearch.SelectedItem.Text = partner.CompanyName;
                    }

                    if (partner != null)
                    {
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
                                    files[index].FileType = "/CollaborationLibrary/" + files[index].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 1].PreviewFilePath))
                                {
                                    if (!FileExistsNew2(table, files[index + 1].FileName))
                                        files[index + 1].FileType = GetItemImageByType(files[index + 1].FilePath);
                                }
                                else
                                {
                                    files[index + 1].FileType = "/CollaborationLibrary/" + files[index + 1].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 2].PreviewFilePath))
                                {
                                    if (!FileExistsNew3(table, files[index + 2].FileName))
                                        files[index + 2].FileType = GetItemImageByType(files[index + 2].FilePath);
                                }
                                else
                                {
                                    files[index + 2].FileType = "/CollaborationLibrary/" + files[index + 2].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 3].PreviewFilePath))
                                {
                                    if (!FileExistsNew4(table, files[index + 3].FileName))
                                        files[index + 3].FileType = GetItemImageByType(files[index + 3].FilePath);
                                }
                                else
                                {
                                    files[index + 3].FileType = "/CollaborationLibrary/" + files[index + 3].PreviewFilePath;
                                }

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = files[index + 1].Id.ToString();
                                row["id3"] = files[index + 2].Id.ToString();
                                row["id4"] = files[index + 3].Id.ToString();
                                row["file_type_id1"] = "files[index].FileTypeId.ToString()";
                                row["file_type_id2"] = "files[index + 1].FileTypeId.ToString()";
                                row["file_type_id3"] = "files[index + 2].FileTypeId.ToString()";
                                row["file_type_id4"] = "files[index + 3].FileTypeId.ToString()";
                                row["file_title1"] = files[index].FileTitle;
                                row["file_title2"] = files[index + 1].FileTitle;
                                row["file_title3"] = files[index + 2].FileTitle;
                                row["file_title4"] = files[index + 3].FileTitle;
                                row["file_name1"] = files[index].FileName;
                                row["file_name2"] = files[index + 1].FileName;
                                row["file_name3"] = files[index + 2].FileName;
                                row["file_name4"] = files[index + 3].FileName;
                                row["file_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index].FilePath;
                                row["file_path2"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath;
                                row["file_path3"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 2].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 2].FilePath;
                                row["file_path4"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 3].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 3].FilePath;
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
                                    files[index].FileType = "/CollaborationLibrary/" + files[index].PreviewFilePath;
                                }

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = "0";
                                row["id3"] = "0";
                                row["id4"] = "0";
                                row["file_type_id1"] = "files[index].FileTypeId.ToString()";
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
                                row["file_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index].FilePath;
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
                                    files[index].FileType = "/CollaborationLibrary/" + files[index].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 1].PreviewFilePath))
                                {
                                    if (!FileExistsNew2(table, files[index + 1].FileName))
                                        files[index + 1].FileType = GetItemImageByType(files[index + 1].FilePath);
                                }
                                else
                                {
                                    files[index + 1].FileType = "/CollaborationLibrary/" + files[index + 1].PreviewFilePath;
                                }

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = files[index + 1].Id.ToString();
                                row["id3"] = "0";
                                row["id4"] = "0";
                                row["file_type_id1"] = "files[index].FileTypeId.ToString()";
                                row["file_type_id2"] = "files[index + 1].FileTypeId.ToString()";
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
                                row["file_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index].FilePath;
                                row["file_path2"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath;
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
                                    files[index].FileType = "/CollaborationLibrary/" + files[index].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 1].PreviewFilePath))
                                {
                                    if (!FileExistsNew2(table, files[index + 1].FileName))
                                        files[index + 1].FileType = GetItemImageByType(files[index + 1].FilePath);
                                }
                                else
                                {
                                    files[index + 1].FileType = "/CollaborationLibrary/" + files[index + 1].PreviewFilePath;
                                }

                                if (string.IsNullOrEmpty(files[index + 2].PreviewFilePath))
                                {
                                    if (!FileExistsNew3(table, files[index + 2].FileName))
                                        files[index + 2].FileType = GetItemImageByType(files[index + 2].FilePath);
                                }
                                else
                                {
                                    files[index + 2].FileType = "/CollaborationLibrary/" + files[index + 2].PreviewFilePath;
                                }

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = files[index + 1].Id.ToString();
                                row["id3"] = files[index + 2].Id.ToString();
                                row["id4"] = "0";
                                row["file_type_id1"] = "files[index].FileTypeId.ToString()";
                                row["file_type_id2"] = "files[index + 1].FileTypeId.ToString()";
                                row["file_type_id3"] = "files[index + 2].FileTypeId.ToString()";
                                row["file_type_id4"] = "0";
                                row["file_title1"] = files[index].FileTitle;
                                row["file_title2"] = files[index + 1].FileTitle;
                                row["file_title3"] = files[index + 2].FileTitle;
                                row["file_title4"] = "-";
                                row["file_name1"] = files[index].FileName;
                                row["file_name2"] = files[index + 1].FileName;
                                row["file_name3"] = files[index + 2].FileName;
                                row["file_name4"] = "-";
                                row["file_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index].FilePath;
                                row["file_path2"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 1].FilePath;
                                row["file_path3"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 2].FilePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + files[index + 2].FilePath;
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
                    session.ExecuteQuery(@"Update Elio_collaboration_users_library_files
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

        private bool UploadFile(HttpPostedFile fileContent, DropDownList drpList)
        {
            bool successUpload = false;

            string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + drpList.SelectedItem.Text + "\\";
            string extension = Path.GetExtension(fileContent.FileName);
            string fileName = fileContent.FileName;

            if (fileContent != null)
            {
                try
                {
                    List<ElioUsers> partners = new List<ElioUsers>();

                    if (TbxReceiversPartners.Text == "")
                        return false;

                    List<string> partnersList = TbxReceiversPartners.Text.Trim().TrimEnd(';').Split(';').ToList();

                    if (partnersList.Count == 0)
                        return false;

                    string companyNames = "";
                    int count = 0;

                    foreach (string receiver in partnersList)
                    {
                        count++;
                        string partner = receiver.Trim().TrimEnd(' ').TrimStart(' ');

                        if (count < partnersList.Count)
                        {
                            if (partner.Trim() != "")
                            {
                                companyNames += "'" + partner + "',";
                            }
                        }
                        else
                        {
                            if (partner.Trim() != "")
                            {
                                companyNames += "'" + partner + "'";
                            }
                        }
                    }

                    partners = Sql.GetUserPartnersIdsGuidsByCompaniesNamesByCompanyType(vSession.User.Id, vSession.User.CompanyType, companyNames, session);

                    if (partners.Count == 0)
                        return false;

                    #region updoad file to my directory                 

                    successUpload = UpLoadImage.UpLoadFile(serverMapPathTargetFolderMine, fileContent, out fileName, session);
                    
                    #endregion

                    if (successUpload)
                    {
                        try
                        {
                            #region File Saving Area

                            session.BeginTransaction();

                            #region User File

                            ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                            libraryFile.CategoryId = Convert.ToInt32(drpList.SelectedValue);
                            libraryFile.FileName = (fileName != "") ? fileName : "Library_" + DateTime.Now.ToShortDateString().Replace("/", "_").Replace("-", "_");
                            libraryFile.FileTitle = libraryFile.FileName.Replace(extension, "");       //(TbxFileTitle.Text != "") ? "Library_" + TbxFileTitle.Text.Replace(" ", "_").Trim() : (fileName.Length <= 150) ? fileName : fileName.Substring(0, 149);
                            libraryFile.FilePath = vSession.User.GuId + "/" + drpList.SelectedItem.Text + "/" + fileName;       //serverMapPathTargetFolder;
                            libraryFile.FileType = fileContent.ContentType;
                            libraryFile.IsPublic = 1;
                            libraryFile.MailboxId = -1;
                            libraryFile.IsNew = 1;

                            //ElioUsers partner = null;

                            //try
                            //{
                            //    if (Key != "")
                            //        partner = Sql.GetUserByGuId(Key, session);
                            //}
                            //catch (Exception ex)
                            //{
                            //    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            //}

                            libraryFile.UserId = vSession.User.Id;
                            libraryFile.UploadedByUserId = vSession.User.Id;
                            libraryFile.DateCreated = DateTime.Now;
                            libraryFile.LastUpdate = DateTime.Now;

                            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                            loader.Insert(libraryFile);

                            #endregion

                            #region Blob File

                            ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                            blobFile.FileName = fileName;
                            //blobFile.CategoryDescription = Ddlcategory.SelectedItem.Text;
                            //blobFile.FilePath = serverMapPathTargetFolderMine;
                            blobFile.FileSize = fileContent.ContentLength;
                            blobFile.FileType = fileContent.ContentType;
                            blobFile.IsPublic = 1;
                            blobFile.DateCreated = DateTime.Now;
                            blobFile.LastUpdate = DateTime.Now;
                            blobFile.LibraryFilesId = libraryFile.Id;
                            blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderMine + "/" + blobFile.FileName);

                            DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                            blbLoader.Insert(blobFile);

                            #endregion

                            if (vSession.User.CompanyType == Types.Vendors.ToString())
                            {
                                #region  Update User PacketStatus Available File Storage Count

                                bool success = GlobalDBMethods.ReduceUserFileStorage(vSession.User.Id, Convert.ToDouble(fileContent.ContentLength), session);
                                if (!success)
                                    throw new Exception(string.Format("User {0} tried to upload a file at {1} but he has not enough storage space from his packet (user billing packet type id = {2}), so it could not be saved or uploaded!", vSession.User.Id, DateTime.Now.ToString(), vSession.User.BillingType.ToString()));

                                #endregion
                            }

                            foreach (ElioUsers partner in partners)
                            {
                                string serverMapPathTargetFolderPartner = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + partner.GuId + "\\" + drpList.SelectedItem.Text + "\\";

                                #region updoad file to partner directory               

                                successUpload = UpLoadImage.UpLoadFile(serverMapPathTargetFolderPartner, fileContent, out fileName, session);

                                #endregion

                                if (successUpload)
                                {
                                    #region User File

                                    libraryFile = new ElioCollaborationUsersLibraryFiles();

                                    libraryFile.CategoryId = Convert.ToInt32(drpList.SelectedValue);
                                    libraryFile.FileName = (fileName != "") ? fileName : "Library_" + DateTime.Now.ToShortDateString().Replace("/", "_").Replace("-", "_");
                                    libraryFile.FileTitle = libraryFile.FileName.Replace(extension, "");       //(TbxFileTitle.Text != "") ? "Library_" + TbxFileTitle.Text.Replace(" ", "_").Trim() : (fileName.Length <= 150) ? fileName : fileName.Substring(0, 149);
                                    libraryFile.FilePath = partner.GuId + "/" + drpList.SelectedItem.Text + "/" + fileName;       //serverMapPathTargetFolder;
                                    libraryFile.FileType = fileContent.ContentType;
                                    libraryFile.IsPublic = 1;
                                    libraryFile.MailboxId = -1;
                                    libraryFile.IsNew = 1;

                                    libraryFile.UserId = partner.Id;
                                    libraryFile.UploadedByUserId = vSession.User.Id;
                                    libraryFile.DateCreated = DateTime.Now;
                                    libraryFile.LastUpdate = DateTime.Now;

                                    loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                    loader.Insert(libraryFile);

                                    #endregion

                                    #region Blob File

                                    blobFile = new ElioCollaborationBlobFiles();

                                    blobFile.FileName = fileName;
                                    //blobFile.CategoryDescription = Ddlcategory.SelectedItem.Text;
                                    //blobFile.FilePath = serverMapPathTargetFolderPartner;
                                    blobFile.FileSize = fileContent.ContentLength;
                                    blobFile.FileType = fileContent.ContentType;
                                    blobFile.IsPublic = 1;
                                    blobFile.DateCreated = DateTime.Now;
                                    blobFile.LastUpdate = DateTime.Now;
                                    blobFile.LibraryFilesId = libraryFile.Id;
                                    blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderPartner + "/" + blobFile.FileName);

                                    blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                    blbLoader.Insert(blobFile);

                                    #endregion

                                    if (partner.CompanyType == Types.Vendors.ToString())
                                    {
                                        #region  Update User PacketStatus Available File Storage Count for Partner NOT

                                        bool success = GlobalDBMethods.ReduceUserFileStorage(partner.Id, Convert.ToDouble(fileContent.ContentLength), session);
                                        if (!success)
                                        {
                                            //not enough storage for this partner
                                            Logger.DetailedError(Request.Url.ToString(), "DANGER!!! Νot enough storage for user uploading files", string.Format("User {0} tried to upload a file at {1} but he has not enough storage space from his packet (user billing packet type id = {2}), so it could not be saved or uploaded!", vSession.User.Id, DateTime.Now.ToString(), vSession.User.BillingType.ToString()));
                                            throw new Exception(string.Format("User {0} tried to upload a file at {1} but he has not enough storage space from his packet (user billing packet type id = {2}), so it could not be saved or uploaded!", vSession.User.Id, DateTime.Now.ToString(), vSession.User.BillingType.ToString()));
                                        }

                                        #endregion
                                    }
                                }
                                else
                                {
                                    LblFileUploadTitle.Text = "File Uploading Warning";
                                    string contentAlert = "File could not be uploaded to your partner' s library (maybe it's size was outside the bounds). Please try again or contact with us.";
                                    GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);

                                    //GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");
                                    FixTabMenu();

                                    return false;
                                }
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
                        LblFileUploadTitle.Text = "File Uploading Warning";
                        string contentAlert = "File could not be uploaded to your library (maybe it's size was outside the bounds). Please try again or contact with us.";
                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);

                        //GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");
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
                        deleted = UpLoadImage.DeleteFileFromDirectory(serverMapPathTargetFolderMine, fileName);

                    Logger.DetailedError(Request.Url.ToString(), (deleted) ? string.Format("File not deleted successfully int path {0}", serverMapPathTargetFolderMine + "\\" + fileName) : "", ex.Message.ToString(), ex.StackTrace.ToString());

                    return false;
                }
            }

            return true;
        }

        private bool UploadLibraryFile(FileUploadedEventArgs fileContent, string fileType, DropDownList drpList)
        {
            bool successUpload = false;

            string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + drpList.SelectedItem.Text + "\\";
            string extension = fileContent.File.GetExtension();
            string fileName = fileContent.File.FileName;

            if (fileContent != null)
            {
                try
                {
                    #region Find Partners

                    List<ElioUsers> partners = new List<ElioUsers>();

                    if (TbxReceiversPartners.Text == "")
                        return false;

                    List<string> partnersList = TbxReceiversPartners.Text.Trim().TrimEnd(';').Split(';').ToList();

                    if (partnersList.Count == 0)
                        return false;

                    string companyNames = "";
                    int count = 0;

                    foreach (string receiver in partnersList)
                    {
                        count++;
                        string partner = receiver.Trim().TrimEnd(' ').TrimStart(' ');

                        if (count < partnersList.Count)
                        {
                            if (partner.Trim() != "")
                            {
                                companyNames += "'" + partner + "',";
                            }
                        }
                        else
                        {
                            if (partner.Trim() != "")
                            {
                                companyNames += "'" + partner + "'";
                            }
                        }
                    }

                    partners = Sql.GetUserPartnersIdsGuidsByCompaniesNamesByCompanyType(vSession.User.Id, vSession.User.CompanyType, companyNames, session);

                    if (partners.Count == 0)
                        return false;

                    #endregion

                    #region updoad file to my directory                 

                    //successUpload = UpLoadImage.UpLoadFile(serverMapPathTargetFolderMine, fileContent, out fileName, session);
                    successUpload = UpLoadImage.UpLoadLibraryFile(serverMapPathTargetFolderMine, fileContent, out fileName, session);

                    #endregion

                    if (successUpload)
                    {
                        try
                        {
                            #region File Saving Area

                            session.BeginTransaction();

                            #region User File

                            ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                            libraryFile.CategoryId = Convert.ToInt32(drpList.SelectedValue);
                            libraryFile.FileName = (fileName != "") ? fileName : "Library_" + DateTime.Now.ToShortDateString().Replace("/", "_").Replace("-", "_");
                            libraryFile.FileTitle = libraryFile.FileName.Replace(extension, "");       //(TbxFileTitle.Text != "") ? "Library_" + TbxFileTitle.Text.Replace(" ", "_").Trim() : (fileName.Length <= 150) ? fileName : fileName.Substring(0, 149);
                            libraryFile.FilePath = vSession.User.GuId + "/" + drpList.SelectedItem.Text + "/" + fileName;       //serverMapPathTargetFolder;
                            libraryFile.FileType = fileType;
                            libraryFile.IsPublic = 1;
                            libraryFile.MailboxId = -1;
                            libraryFile.IsNew = 1;

                            //ElioUsers partner = null;

                            //try
                            //{
                            //    if (Key != "")
                            //        partner = Sql.GetUserByGuId(Key, session);
                            //}
                            //catch (Exception ex)
                            //{
                            //    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            //}

                            libraryFile.UserId = vSession.User.Id;
                            libraryFile.UploadedByUserId = vSession.User.Id;
                            libraryFile.DateCreated = DateTime.Now;
                            libraryFile.LastUpdate = DateTime.Now;

                            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                            loader.Insert(libraryFile);

                            #endregion

                            #region Blob File

                            ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                            blobFile.FileName = fileName;
                            //blobFile.CategoryDescription = Ddlcategory.SelectedItem.Text;
                            //blobFile.FilePath = serverMapPathTargetFolderMine;
                            blobFile.FileSize = Convert.ToInt32(fileContent.File.ContentLength);
                            blobFile.FileType = fileType;
                            blobFile.IsPublic = 1;
                            blobFile.DateCreated = DateTime.Now;
                            blobFile.LastUpdate = DateTime.Now;
                            blobFile.LibraryFilesId = libraryFile.Id;
                            blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderMine + "/" + blobFile.FileName);

                            DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
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

                            foreach (ElioUsers partner in partners)
                            {
                                string serverMapPathTargetFolderPartner = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + partner.GuId + "\\" + drpList.SelectedItem.Text + "\\";

                                #region updoad file to partner directory               

                                //successUpload = UpLoadImage.UpLoadLibraryFile(serverMapPathTargetFolderPartner, fileContent, out fileName, session);
                                successUpload = UpLoadImage.MoveFileToFolder(serverMapPathTargetFolderMine, serverMapPathTargetFolderPartner, fileName);

                                #endregion

                                if (successUpload)
                                {
                                    #region User File

                                    libraryFile = new ElioCollaborationUsersLibraryFiles();

                                    libraryFile.CategoryId = Convert.ToInt32(drpList.SelectedValue);
                                    libraryFile.FileName = (fileName != "") ? fileName : "Library_" + DateTime.Now.ToShortDateString().Replace("/", "_").Replace("-", "_");
                                    libraryFile.FileTitle = libraryFile.FileName.Replace(extension, "");       //(TbxFileTitle.Text != "") ? "Library_" + TbxFileTitle.Text.Replace(" ", "_").Trim() : (fileName.Length <= 150) ? fileName : fileName.Substring(0, 149);
                                    libraryFile.FilePath = partner.GuId + "/" + drpList.SelectedItem.Text + "/" + fileName;       //serverMapPathTargetFolder;
                                    libraryFile.FileType = fileType;
                                    libraryFile.IsPublic = 1;
                                    libraryFile.MailboxId = -1;
                                    libraryFile.IsNew = 1;

                                    libraryFile.UserId = partner.Id;
                                    libraryFile.UploadedByUserId = vSession.User.Id;
                                    libraryFile.DateCreated = DateTime.Now;
                                    libraryFile.LastUpdate = DateTime.Now;

                                    loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                    loader.Insert(libraryFile);

                                    #endregion

                                    #region Blob File

                                    blobFile = new ElioCollaborationBlobFiles();

                                    blobFile.FileName = fileName;
                                    //blobFile.CategoryDescription = Ddlcategory.SelectedItem.Text;
                                    //blobFile.FilePath = serverMapPathTargetFolderPartner;
                                    blobFile.FileSize = Convert.ToInt32(fileContent.File.ContentLength);
                                    blobFile.FileType = fileType;
                                    blobFile.IsPublic = 1;
                                    blobFile.DateCreated = DateTime.Now;
                                    blobFile.LastUpdate = DateTime.Now;
                                    blobFile.LibraryFilesId = libraryFile.Id;
                                    blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderPartner + "/" + blobFile.FileName);

                                    blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                    blbLoader.Insert(blobFile);

                                    #endregion

                                    if (partner.CompanyType == Types.Vendors.ToString())
                                    {
                                        #region  Update User PacketStatus Available File Storage Count for Partner NOT

                                        bool success = GlobalDBMethods.ReduceUserFileStorage(partner.Id, Convert.ToDouble(fileContent.File.ContentLength), session);
                                        if (!success)
                                        {
                                            //not enough storage for this partner
                                            Logger.DetailedError(Request.Url.ToString(), "DANGER!!! Νot enough storage for user uploading files", string.Format("User {0} tried to upload a file at {1} but he has not enough storage space from his packet (user billing packet type id = {2}), so it could not be saved or uploaded!", vSession.User.Id, DateTime.Now.ToString(), vSession.User.BillingType.ToString()));
                                            throw new Exception(string.Format("User {0} tried to upload a file at {1} but he has not enough storage space from his packet (user billing packet type id = {2}), so it could not be saved or uploaded!", vSession.User.Id, DateTime.Now.ToString(), vSession.User.BillingType.ToString()));
                                        }

                                        #endregion
                                    }
                                }
                                else
                                {
                                    LblFileUploadTitle.Text = "File Uploading Warning";
                                    string contentAlert = "File could not be uploaded to your partner' s library (maybe it's size was outside the bounds). Please try again or contact with us.";
                                    GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);

                                    //UpLoadImage.DeleteFileFromDirectory(serverMapPathTargetFolderPartner, fileName);

                                    //GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");
                                    FixTabMenu();

                                    return false;
                                }
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
                        LblFileUploadTitle.Text = "File Uploading Warning";
                        string contentAlert = "File could not be uploaded to your library (maybe it's size was outside the bounds). Please try again or contact with us.";
                        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);

                        //GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");
                        FixTabMenu();

                        return false;
                    }
                }
                catch (Exception ex)
                {
                    LblFileUploadTitle.Text = "File Uploading Warning";
                    string contentAlert = "File could not be uploaded to your onboarding library";
                    GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);

                    bool deleted = false;

                    if (successUpload)
                        deleted = UpLoadImage.DeleteFileFromDirectory(serverMapPathTargetFolderMine, fileName);

                    Logger.DetailedError(Request.Url.ToString(), (deleted) ? string.Format("File not deleted successfully int path {0}", serverMapPathTargetFolderMine + "\\" + fileName) : "", ex.Message.ToString(), ex.StackTrace.ToString());

                    return false;
                }
            }

            return true;
        }

        private bool UploadPreviewFile(HttpPostedFile fileContent)
        {
            bool successUpload = false;

            ElioCollaborationUsersLibraryFiles libraryFile = SqlCollaboration.GetCollaborationUserLibraryFileById(LibraryFileId, session);
            if (libraryFile != null)
            {
                string[] filePaths = libraryFile.FilePath.Split('/').ToArray();
                if (filePaths.Length > 0 && filePaths.Length > 2)
                {
                    string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + filePaths[0] + "\\" + filePaths[1] + "\\PreviewFiles\\";
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

                                    DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                    loader.Update(libraryFile);

                                    #endregion

                                    #region Blob Preview File

                                    ElioCollaborationBlobPreviewFiles blobPreviewFile = new ElioCollaborationBlobPreviewFiles();

                                    blobPreviewFile.FileName = fileName;
                                    //blobPreviewFile.FilePath = serverMapPathTargetFolder;
                                    blobPreviewFile.FileSize = fileContent.ContentLength;
                                    blobPreviewFile.FileType = fileContent.ContentType;
                                    blobPreviewFile.IsPublic = 1;
                                    blobPreviewFile.DateCreated = DateTime.Now;
                                    blobPreviewFile.LastUpdate = DateTime.Now;
                                    blobPreviewFile.LibraryFilesId = libraryFile.Id;
                                    blobPreviewFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolder + "/" + blobPreviewFile.FileName);

                                    DataLoader<ElioCollaborationBlobPreviewFiles> blbLoader = new DataLoader<ElioCollaborationBlobPreviewFiles>(session);
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

        private void FillGroups()
        {
            if (session.Connection.State == ConnectionState.Closed)
                session.OpenConnection();

            List<ElioCollaborationUsersLibraryGroups> groups = SqlCollaboration.GetCollaborationUserLibraryGroups(vSession.User.Id, session);

            if (groups.Count > 0)
            {
                divLibraryGroup.Visible = true;
                divNoLibraryGroupExists.Visible = false;

                TbxLibraryGroupsName.Text = "";
                TbxLibraryGroupsName.CssClass = "form-control";
                aBtnAddGroupMembers.Visible = false;
                DdlLibraryGroups.Items.Clear();

                ListItem item = new ListItem();
                item.Value = "0";
                item.Text = "Select group";

                DdlLibraryGroups.Items.Add(item);

                foreach (ElioCollaborationUsersLibraryGroups group in groups)
                {
                    item = new ListItem();
                    item.Value = group.Id.ToString();
                    item.Text = group.GroupDescription;

                    DdlLibraryGroups.Items.Add(item);
                }

                DdlLibraryGroups.Items.FindByValue("0").Selected = true;

                FixTextAreasView(2);
            }
            else
            {
                divNoLibraryGroupExists.Visible = true;
                divLibraryGroup.Visible = false;
            }
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

            aBtnAdd.Visible = false;
            DdlPartnersList.Items.Clear();
            DdlPartnersListSearch.Items.Clear();

            if (partners.Count > 0)
            {
                if (partners.Count > 1)
                {
                    ListItem item = new ListItem();
                    item.Value = "0";
                    item.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Select partner" : "Select vendor";

                    DdlPartnersList.Items.Add(item);
                    DdlPartnersListSearch.Items.Add(item);
                }

                foreach (ElioUsers partner in partners)
                {
                    ListItem item = new ListItem();
                    item.Value = partner.Id.ToString();
                    item.Text = partner.CompanyName;

                    DdlPartnersList.Items.Add(item);
                    DdlPartnersListSearch.Items.Add(item);
                }
            }

            if (partners.Count == 1)
            {
                DdlPartnersList.Visible = false;
                TbxReceiversPartners.Text = partners[0].CompanyName;
                TbxReceiversPartners.Enabled = false;

                DdlPartnersListSearch.Visible = true;
                DdlPartnersListSearch.Enabled = false;
                DdlPartnersListSearch.Items.FindByValue(partners[0].Id.ToString()).Selected = true;
                DdlPartnersListSearch.SelectedValue = partners[0].Id.ToString();
                DdlPartnersListSearch.SelectedItem.Text = partners[0].CompanyName;
            }

            if (partners.Count == 0)
            {
                GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, "Sorry, you have no partners to send files.", MessageTypes.Error, true, true, true, true, false);
                aBtnLoadNextStep.Visible = false;
            }

            FixTextAreasView(1);
        }

        private void GetTiersByUser()
        {
            try
            {
                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                List<ElioTierManagementUsersSettings> tiers = Sql.GetTierManagementUserSettings(vSession.User.Id, session);

                if (tiers.Count > 0)
                {
                    divTiers.Visible = true;
                    divNoTiersExists.Visible = false;

                    TbxTiersPartners.Text = "";
                    TbxTiersPartners.CssClass = "form-control";
                    aBtnAddTiersMembers.Visible = false;
                    DdlTiers.Items.Clear();

                    ListItem item = new ListItem();
                    item.Value = "0";
                    item.Text = "Select tier";

                    DdlTiers.Items.Add(item);

                    foreach (ElioTierManagementUsersSettings tier in tiers)
                    {
                        item = new ListItem();
                        item.Value = tier.Id.ToString();
                        item.Text = tier.Description;

                        DdlTiers.Items.Add(item);
                    }

                    DdlTiers.Items.FindByValue("0").Selected = true;

                    FixTextAreasView(3);
                }
                else
                {
                    divNoTiersExists.Visible = true;
                    divTiers.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
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

        private void GetPartnersList(string orderByClause, GlobalMethods.SearchCriteria criteria, int libraryGroupId)
        {
            if (session.Connection.State == ConnectionState.Closed)
                session.OpenConnection();

            if (vSession.User != null)
            {
                int isPublic = 1;
                int isDeleted = 0;
                //int isNew = 1;
                //int isViewed = 0;

                List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), isDeleted, isPublic, vSession.User.CompanyType, criteria, orderByClause, libraryGroupId, session);

                if (partners.Count > 0)
                {
                    //RptConnectionList.Visible = true;
                    RptRetailorsList.Visible = true;
                    RptEditRetailorsList.Visible = true;
                    PnlCreateLibraryGroup.Enabled = true;

                    UcMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("master_user_id");
                    table.Columns.Add("invitation_status");
                    table.Columns.Add("partner_user_id");
                    table.Columns.Add("company_name");
                    table.Columns.Add("email");
                    table.Columns.Add("country");

                    foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                    {
                        //int totalNewNotViewedMsgs = SqlCollaboration.GetUserCountMailBoxMessagesByNewAndViewStatusId(partner.Id, partner.MasterUserId, partner.PartnerUserId, vSession.User.Id, isNew, isViewed, isDeleted, isPublic, session);

                        table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, partner.Country);
                    }

                    RptRetailorsList.DataSource = table;
                    RptRetailorsList.DataBind();

                    //RptEditRetailorsList.DataSource = table;
                    //RptEditRetailorsList.DataBind();
                }
                else
                {
                    //RptConnectionList.Visible = false;
                    RptRetailorsList.Visible = false;
                    //RptEditRetailorsList.Visible = false;

                    string alert = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "You have no Resellers yet" : "You have no Vendors yet";
                    //GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Info, true, true, false, false, false);
                    GlobalMethods.ShowMessageControlDA(MessageControlGroupMembers, alert, MessageTypes.Info, true, true, false, false, false);
                    GlobalMethods.ShowMessageControlDA(MessageControlEditRetailors, alert, MessageTypes.Info, true, true, false, false, false);
                    GlobalMethods.ShowMessageControlDA(MessageControlCreateLibraryGroup, alert, MessageTypes.Info, true, true, false, false, false);

                    //ImgBtnSendMsg.Enabled = false;
                    PnlCreateLibraryGroup.Enabled = false;
                }
            }
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
                    #region Reset Repeater

                    foreach (RepeaterItem item in RptRetailorsList.Items)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
                        if (cbx != null && rptCheckedItem != null)
                        {
                            if (item.ClientID != rptCheckedItem.ClientID)
                                cbx.Checked = false;
                        }
                    }

                    #endregion
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

        private void GetPartnersListForEditRetailorsSearch(string orderByClause, GlobalMethods.SearchCriteria criteria)
        {
            if (vSession.User != null)
            {
                int isPublic = 1;
                int isDeleted = 0;
                int isNew = 1;
                int isViewed = 0;

                List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), isDeleted, isPublic, vSession.User.CompanyType, criteria, orderByClause, 0, session);

                if (partners.Count > 0)
                {
                    RptEditRetailorsList.Visible = true;
                    UcMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("master_user_id");
                    table.Columns.Add("invitation_status");
                    table.Columns.Add("partner_user_id");
                    table.Columns.Add("company_name");
                    table.Columns.Add("email");
                    table.Columns.Add("country");
                    table.Columns.Add("region");
                    table.Columns.Add("msgs_count");

                    foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                    {
                        int totalNewNotViewedMsgs = SqlCollaboration.GetUserCountMailBoxMessagesByNewAndViewStatusId(partner.Id, partner.MasterUserId, partner.PartnerUserId, vSession.User.Id, isNew, isViewed, isDeleted, isPublic, session);

                        table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, partner.Country, partner.Region, totalNewNotViewedMsgs);
                    }

                    RptEditRetailorsList.DataSource = table;
                    RptEditRetailorsList.DataBind();
                }
                else
                {
                    RptEditRetailorsList.Visible = false;

                    string alert = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "You have no Resellers results" : "You have no Vendors results";
                    GlobalMethods.ShowMessageControlDA(MessageControlEditRetailors, alert, MessageTypes.Info, true, true, false);
                }
            }
        }

        private void GetPartnersListForRetailorsSearch(string orderByClause, GlobalMethods.SearchCriteria criteria)
        {
            if (vSession.User != null)
            {
                int isPublic = 1;
                int isDeleted = 0;
                int isNew = 1;
                int isViewed = 0;

                List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), isDeleted, isPublic, vSession.User.CompanyType, criteria, orderByClause, 0, session);

                if (partners.Count > 0)
                {
                    RptRetailorsList.Visible = true;
                    UcMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("master_user_id");
                    table.Columns.Add("invitation_status");
                    table.Columns.Add("partner_user_id");
                    table.Columns.Add("company_name");
                    table.Columns.Add("email");
                    table.Columns.Add("country");
                    table.Columns.Add("region");
                    table.Columns.Add("msgs_count");

                    foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                    {
                        int totalNewNotViewedMsgs = SqlCollaboration.GetUserCountMailBoxMessagesByNewAndViewStatusId(partner.Id, partner.MasterUserId, partner.PartnerUserId, vSession.User.Id, isNew, isViewed, isDeleted, isPublic, session);

                        table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, partner.Country, partner.Region, totalNewNotViewedMsgs);
                    }

                    RptRetailorsList.DataSource = table;
                    RptRetailorsList.DataBind();
                }
                else
                {
                    RptRetailorsList.Visible = false;

                    string alert = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "You have no Resellers results" : "You have no Vendors results";
                    GlobalMethods.ShowMessageControlDA(MessageControlGroupMembers, alert, MessageTypes.Info, true, true, false);
                }
            }
        }

        private void ResetGroupItems()
        {
            TbxLibraryGroupName.Text = "";

            foreach (RepeaterItem item in RptRetailorsList.Items)
            {
                CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                if (cbx.Checked)
                {
                    cbx.Checked = false;
                }

                if (item.ItemType == ListItemType.Header)
                {
                    CheckBox cbxAll = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectAll");
                    if (cbxAll != null)
                        cbxAll.Checked = false;
                }
            }

            UpdatePanel2.Update();
        }

        private void LoadStepOne()
        {
            LblSelectReceiversAndUploadFiles.Text = "Step 1: Select partners";

            UcReceiversAndUploadFile.Visible = false;
            divVendorsUploadFilesAreaStepOne.Visible = true;
            aBtnLoadNextStep.Visible = true;
            aBtnLoadNextStep.Enabled = true;

            divVendorsUploadFilesAreaStepTwo.Visible = false;
            divVendorsUploadFilesAreaStepThree.Visible = false;
            aBtnLoadPreviousStep.Visible = false;
            BtnUploadFile.Visible = false;
        }

        private void LoadStepTwo()
        {
            LblSelectReceiversAndUploadFiles.Text = "Step 2: Send files to your partners";

            UcReceiversAndUploadFile.Visible = false;
            divVendorsUploadFilesAreaStepOne.Visible = false;
            divVendorsUploadFilesAreaStepThree.Visible = false;
            BtnUploadFile.Visible = Ddlcategory.SelectedItem.Value != "0";
            aBtnLoadNextStep.Visible = false;

            divVendorsUploadFilesAreaStepTwo.Visible = true;
            aBtnLoadPreviousStep.Visible = true;
        }

        private void LoadStepThree()
        {
            LblSelectReceiversAndUploadFiles.Text = "Step 3: Summary";

            UcReceiversAndUploadFile.Visible = false;
            divVendorsUploadFilesAreaStepOne.Visible = false;
            divVendorsUploadFilesAreaStepTwo.Visible = false;
            aBtnLoadNextStep.Visible = false;

            divVendorsUploadFilesAreaStepThree.Visible = true;
            aBtnLoadPreviousStep.Visible = true;
            BtnUploadFile.Visible = true;
        }

        private bool ExistInReceivers(string inputReceivers)
        {
            if (inputReceivers != "")
            {
                List<string> inputPartnersList = inputReceivers.Trim().TrimEnd(';').Split(';').ToList();

                foreach (string inputReceiver in inputPartnersList)
                {
                    if (TbxReceiversPartners.Text != "")
                    {
                        List<string> partnersList = TbxReceiversPartners.Text.Trim().TrimEnd(';').Split(';').ToList();

                        foreach (string receiver in partnersList)
                        {
                            if (receiver.TrimEnd(' ').TrimStart(' ').Trim() == inputReceiver.TrimEnd(' ').TrimStart(' ').Trim())
                                return true;
                        }
                    }
                }
            }

            return false;
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

                LoadStepOne();
                FillDropLists();
                //FillPartnersList();

                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    liGroupPartners.Visible = liTiersPartners.Visible = true;

                    FillGroups();
                    GetTiersByUser();
                }
                else
                    liGroupPartners.Visible = liTiersPartners.Visible = false;

                ResetPanelItems();
                FixCategoriesItems();
                FillPartnersList();

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
                        bool existCategoryDescription = SqlCollaboration.ExistLibraryCategoryDescription(vSession.User.Id, TbxAddNewCategory.Text.Trim(), false, session);
                        if (existCategoryDescription)
                        {
                            GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Sorry, this category description already exists for you! Please try different description", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                        else
                        {
                            DataLoader<ElioCollaborationUsersLibraryFilesCategories> loader = new DataLoader<ElioCollaborationUsersLibraryFilesCategories>(session);

                            ElioCollaborationUsersLibraryFilesCategories category = new ElioCollaborationUsersLibraryFilesCategories();

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

                                        bool success = SqlCollaboration.UpdateUserCollaborationLibraryFileCategoryDescription(vSession.User.Id, Convert.ToInt32(hdnId.Value), tbxEditCategory.Text.Trim(), false, session);
                                        if (!success)
                                        {
                                            //GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Sorry, this category description already exists for you! Please try different description", MessageTypes.Error, true, true, true, true, false);
                                            GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Sorry, category description could not be updated! Please try again later or contact us.", MessageTypes.Error, true, true, true, true, false);
                                            return;
                                        }

                                        #endregion

                                        #region Rename file path of files to new cateogry description / Rename Folder Name

                                        bool successUpdateTransfer = GlobalDBMethods.UpdateUserCollaborationLibraryFilesPathDirectoryAndMoveFiles(Server, vSession.User.Id, Convert.ToInt32(hdnId.Value), lblDescription.Text, tbxEditCategory.Text, false, session);
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

                if (TbxReceiversPartners.Text == string.Empty)
                {
                    GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Please select at least one receiver", MessageTypes.Error, true, true, true, true, false);
                    return;
                }

                if (UploadedFilesCount == 0)
                {
                    string contentAlert = "No File has been selected for upload to your collaboration library. Please select at least one.";
                    GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                    return;
                }
                else
                {
                    string content = UploadedFilesCount == 1 ? UploadedFilesCount + " File successfully sent to your partners." : UploadedFilesCount + " Files successfully sent to your partners.";

                    GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, content, MessageTypes.Success, true, true, true, true, false);

                    try
                    {
                        //if (vSession.User.CompanyType == Types.Vendors.ToString())
                        //{
                        List<string> partnersList = TbxReceiversPartners.Text.Trim().TrimEnd(';').Split(';').ToList();

                        string companyNames = "";
                        int count = 0;

                        foreach (string receiver in partnersList)
                        {
                            count++;
                            string partner = receiver.Trim().TrimEnd(' ').TrimStart(' ');

                            if (count < partnersList.Count)
                            {
                                if (partner.Trim() != "")
                                {
                                    companyNames += "'" + partner + "',";
                                }
                            }
                            else
                            {
                                if (partner.Trim() != "")
                                {
                                    companyNames += "'" + partner + "'";
                                }
                            }
                        }

                        List<string> emails = Sql.GetUserPartnersEmailsByCompaniesNamesByCompanyType(vSession.User.Id, vSession.User.CompanyType, companyNames, session);

                        //List<string> emails = SqlCollaboration.GetResellersEmailByVendorUserId(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, CollaborateInvitationStatus.Confirmed.ToString(), session);

                        if (emails.Count > 0)
                            EmailSenderLib.SendNewUploadedCollaborationLibraryFileEmail(vSession.User.CompanyName, emails, false, vSession.Lang, session);
                        else
                            Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, uploaded new collaboration file at {1}, but no collaboration partner email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardPartnerOnboarding.aspx --> ERROR sending notification Email");
                        //}
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

                return;

                #region Old Way - To Delete

                //bool successUpload = false;
                //string alert = "";
                //int uploadedFilesCount = 0;

                //if (divFileUpload.Visible)
                //{
                //    UploadedFile e = RadAsyncUpload1.UploadedFiles[0];
                //    string fName = RadAsyncUpload1.UploadedFiles[0].FileName;
                //    string fNameWithNoExt = RadAsyncUpload1.UploadedFiles[0].GetNameWithoutExtension();

                //    if (!ValidateFile(Ddlcategory, e, out alert))
                //    {
                //        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, alert, MessageTypes.Error, true, true, true, true, false);
                //        return;
                //    }

                //    string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + Ddlcategory.SelectedItem.Text + "\\";

                //    if (e != null && e.ContentLength > 0)
                //    {
                //        if (File.Exists(serverMapPathTargetFolderMine + "\\" + fName))
                //        {
                //            successUpload = true;
                //            uploadedFilesCount++;
                //        }

                //        if (!successUpload && UploadedFilesCount == 0)
                //        {
                //            string contentAlert = "File " + fNameWithNoExt + " could not be uploaded to your collaboration library";
                //            GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                //            return;
                //        }
                //    }
                //}

                //if (divFileUpload2.Visible)
                //{
                //    UploadedFile e = RadAsyncUpload2.UploadedFiles[0];
                //    string fName = RadAsyncUpload2.UploadedFiles[0].FileName;
                //    string fNameWithNoExt = RadAsyncUpload2.UploadedFiles[0].GetNameWithoutExtension();

                //    if (!ValidateFile(Ddlcategory2, e, out alert) && uploadedFilesCount == 0)
                //    {
                //        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, alert, MessageTypes.Error, true, true, true, true, false);
                //        return;
                //    }

                //    string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + Ddlcategory2.SelectedItem.Text + "\\";

                //    if (e != null && e.ContentLength > 0)
                //    {
                //        if (File.Exists(serverMapPathTargetFolderMine + "\\" + fName))
                //        {
                //            successUpload = true;
                //            uploadedFilesCount++;
                //        }

                //        if (!successUpload && uploadedFilesCount == 0)
                //        {
                //            string contentAlert = "File " + fNameWithNoExt + " could not be uploaded to your collaboration library";
                //            GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                //            return;
                //        }
                //    }
                //}

                //if (divFileUpload3.Visible)
                //{
                //    UploadedFile e = RadAsyncUpload3.UploadedFiles[0];
                //    string fName = RadAsyncUpload3.UploadedFiles[0].FileName;
                //    string fNameWithNoExt = RadAsyncUpload3.UploadedFiles[0].GetNameWithoutExtension();

                //    if (!ValidateFile(Ddlcategory3, e, out alert) && uploadedFilesCount == 0)
                //    {
                //        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, alert, MessageTypes.Error, true, true, true, true, false);
                //        return;
                //    }

                //    string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + Ddlcategory3.SelectedItem.Text + "\\";

                //    if (e != null && e.ContentLength > 0)
                //    {
                //        if (File.Exists(serverMapPathTargetFolderMine + "\\" + fName))
                //        {
                //            successUpload = true;
                //            uploadedFilesCount++;
                //        }

                //        if (!successUpload && uploadedFilesCount == 0)
                //        {
                //            string contentAlert = "File " + fNameWithNoExt + " could not be uploaded to your collaboration library";
                //            GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                //            return;
                //        }
                //    }
                //}

                //if (divFileUpload4.Visible)
                //{
                //    UploadedFile e = RadAsyncUpload4.UploadedFiles[0];
                //    string fName = RadAsyncUpload4.UploadedFiles[0].FileName;
                //    string fNameWithNoExt = RadAsyncUpload4.UploadedFiles[0].GetNameWithoutExtension();

                //    if (!ValidateFile(Ddlcategory4, e, out alert) && uploadedFilesCount == 0)
                //    {
                //        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, alert, MessageTypes.Error, true, true, true, true, false);
                //        return;
                //    }

                //    string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + Ddlcategory4.SelectedItem.Text + "\\";

                //    if (e.InputStream != null && e.ContentLength > 0)
                //    {
                //        if (File.Exists(serverMapPathTargetFolderMine + "\\" + fName))
                //        {
                //            successUpload = true;
                //            uploadedFilesCount++;
                //        }

                //        if (!successUpload && uploadedFilesCount == 0)
                //        {
                //            string contentAlert = "File " + fNameWithNoExt + " could not be uploaded to your collaboration library";
                //            GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                //            return;
                //        }
                //    }
                //}

                //if (divFileUpload5.Visible)
                //{
                //    UploadedFile e = RadAsyncUpload5.UploadedFiles[0];
                //    string fName = RadAsyncUpload5.UploadedFiles[0].FileName;
                //    string fNameWithNoExt = RadAsyncUpload5.UploadedFiles[0].GetNameWithoutExtension();

                //    if (!ValidateFile(Ddlcategory5, e, out alert) && uploadedFilesCount == 0)
                //    {
                //        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, alert, MessageTypes.Error, true, true, true, true, false);
                //        return;
                //    }

                //    string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + Ddlcategory5.SelectedItem.Text + "\\";

                //    if (e != null && e.ContentLength > 0)
                //    {
                //        if (File.Exists(serverMapPathTargetFolderMine + "\\" + fName))
                //        {
                //            successUpload = true;
                //            uploadedFilesCount++;
                //        }

                //        if (!successUpload && uploadedFilesCount == 0)
                //        {
                //            string contentAlert = "File " + fNameWithNoExt + " could not be uploaded to your collaboration library";
                //            GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                //            return;
                //        }
                //    }
                //}

                //if (divFileUpload6.Visible)
                //{
                //    UploadedFile e = RadAsyncUpload6.UploadedFiles[0];
                //    string fName = RadAsyncUpload6.UploadedFiles[0].FileName;
                //    string fNameWithNoExt = RadAsyncUpload6.UploadedFiles[0].GetNameWithoutExtension();

                //    if (!ValidateFile(Ddlcategory6, e, out alert) && uploadedFilesCount == 0)
                //    {
                //        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, alert, MessageTypes.Error, true, true, true, true, false);
                //        return;
                //    }

                //    string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + Ddlcategory6.SelectedItem.Text + "\\";

                //    if (e != null && e.ContentLength > 0)
                //    {
                //        if (File.Exists(serverMapPathTargetFolderMine + "\\" + fName))
                //        {
                //            successUpload = true;
                //            uploadedFilesCount++;
                //        }

                //        if (!successUpload && uploadedFilesCount == 0)
                //        {
                //            string contentAlert = "File " + fNameWithNoExt + " could not be uploaded to your collaboration library";
                //            GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                //            return;
                //        }
                //    }
                //}

                //if (divFileUpload7.Visible)
                //{
                //    UploadedFile e = RadAsyncUpload7.UploadedFiles[0];
                //    string fName = RadAsyncUpload7.UploadedFiles[0].FileName;
                //    string fNameWithNoExt = RadAsyncUpload7.UploadedFiles[0].GetNameWithoutExtension();

                //    if (!ValidateFile(Ddlcategory7, e, out alert) && uploadedFilesCount == 0)
                //    {
                //        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, alert, MessageTypes.Error, true, true, true, true, false);
                //        return;
                //    }

                //    string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + Ddlcategory7.SelectedItem.Text + "\\";

                //    if (e != null && e.ContentLength > 0)
                //    {
                //        if (File.Exists(serverMapPathTargetFolderMine + "\\" + fName))
                //        {
                //            successUpload = true;
                //            uploadedFilesCount++;
                //        }

                //        if (!successUpload && uploadedFilesCount == 0)
                //        {
                //            string contentAlert = "File " + fNameWithNoExt + " could not be uploaded to your collaboration library";
                //            GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                //            return;
                //        }
                //    }
                //}

                //if (divFileUpload8.Visible)
                //{
                //    UploadedFile e = RadAsyncUpload8.UploadedFiles[0];
                //    string fName = RadAsyncUpload8.UploadedFiles[0].FileName;
                //    string fNameWithNoExt = RadAsyncUpload8.UploadedFiles[0].GetNameWithoutExtension();

                //    if (!ValidateFile(Ddlcategory8, e, out alert) && uploadedFilesCount == 0)
                //    {
                //        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, alert, MessageTypes.Error, true, true, true, true, false);
                //        return;
                //    }

                //    string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + Ddlcategory8.SelectedItem.Text + "\\";

                //    if (e != null && e.ContentLength > 0)
                //    {
                //        if (File.Exists(serverMapPathTargetFolderMine + "\\" + fName))
                //        {
                //            successUpload = true;
                //            uploadedFilesCount++;
                //        }

                //        if (!successUpload && uploadedFilesCount == 0)
                //        {
                //            string contentAlert = "File " + fNameWithNoExt + " could not be uploaded to your collaboration library";
                //            GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                //            return;
                //        }
                //    }
                //}

                //if (divFileUpload9.Visible)
                //{
                //    UploadedFile e = RadAsyncUpload9.UploadedFiles[0];
                //    string fName = RadAsyncUpload9.UploadedFiles[0].FileName;
                //    string fNameWithNoExt = RadAsyncUpload9.UploadedFiles[0].GetNameWithoutExtension();

                //    if (!ValidateFile(Ddlcategory9, e, out alert) && uploadedFilesCount == 0)
                //    {
                //        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, alert, MessageTypes.Error, true, true, true, true, false);
                //        return;
                //    }

                //    string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + Ddlcategory9.SelectedItem.Text + "\\";

                //    if (e != null && e.ContentLength > 0)
                //    {
                //        if (File.Exists(serverMapPathTargetFolderMine + "\\" + fName))
                //        {
                //            successUpload = true;
                //            uploadedFilesCount++;
                //        }

                //        if (!successUpload && uploadedFilesCount == 0)
                //        {
                //            string contentAlert = "File " + fNameWithNoExt + " could not be uploaded to your collaboration library";
                //            GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                //            return;
                //        }
                //    }
                //}

                //if (divFileUpload10.Visible)
                //{
                //    UploadedFile e = RadAsyncUpload10.UploadedFiles[0];
                //    string fName = RadAsyncUpload10.UploadedFiles[0].FileName;
                //    string fNameWithNoExt = RadAsyncUpload10.UploadedFiles[0].GetNameWithoutExtension();

                //    if (!ValidateFile(Ddlcategory10, e, out alert) && uploadedFilesCount == 0)
                //    {
                //        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, alert, MessageTypes.Error, true, true, true, true, false);
                //        return;
                //    }

                //    string serverMapPathTargetFolderMine = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + Ddlcategory10.SelectedItem.Text + "\\";

                //    if (e != null && e.ContentLength > 0)
                //    {
                //        if (File.Exists(serverMapPathTargetFolderMine + "\\" + fName))
                //        {
                //            successUpload = true;
                //            uploadedFilesCount++;
                //        }

                //        if (!successUpload && uploadedFilesCount == 0)
                //        {
                //            string contentAlert = "File " + fNameWithNoExt + " could not be uploaded to your collaboration library";
                //            GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                //            return;
                //        }
                //    }
                //}

                //if (successUpload && uploadedFilesCount > 0)
                //{
                //    string content = "Files successfully sent to your partners.";

                //    GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, content, MessageTypes.Success, true, true, true, true, false);

                //    try
                //    {
                //        if (vSession.User.CompanyType == Types.Vendors.ToString())
                //        {
                //            List<string> partnersList = TbxReceiversPartners.Text.Trim().TrimEnd(';').Split(';').ToList();

                //            string companyNames = "";
                //            int count = 0;

                //            foreach (string receiver in partnersList)
                //            {
                //                count++;
                //                string partner = receiver.Trim().TrimEnd(' ').TrimStart(' ');

                //                if (count < partnersList.Count)
                //                {
                //                    if (partner.Trim() != "")
                //                    {
                //                        companyNames += "'" + partner + "',";
                //                    }
                //                }
                //                else
                //                {
                //                    if (partner.Trim() != "")
                //                    {
                //                        companyNames += "'" + partner + "'";
                //                    }
                //                }
                //            }

                //            List<string> emails = Sql.GetUserPartnersEmailsByCompaniesNamesByCompanyType(vSession.User.Id, vSession.User.CompanyType, companyNames, session);

                //            //List<string> emails = SqlCollaboration.GetResellersEmailByVendorUserId(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, CollaborateInvitationStatus.Confirmed.ToString(), session);

                //            if (emails.Count > 0)
                //                EmailSenderLib.SendNewUploadedFileEmail(vSession.User.CompanyName, emails, false, vSession.Lang, session);
                //            else
                //                Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, uploaded new collaboration file at {1}, but no collaboration partner email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardPartnerOnboarding.aspx --> ERROR sending notification Email");
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                //    }
                //}
                //else
                //{
                //    string contentAlert = "No File could be uploaded to your collaboration library or send to your parnter/s";
                //    GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, contentAlert, MessageTypes.Error, true, true, true, true, false);
                //}

                //FixCategoriesItems();
                //CategoryIdView = Convert.ToInt32(Ddlcategory.SelectedValue);
                //LoadGridData(Rdg1, CategoryIdView, UcRgd1);
                //UpdatePanelContent.Update();

                #endregion
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
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName1");

                        if (hdnId != null && hdnCategoryId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile();

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
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName2");

                        if (hdnId != null && hdnCategoryId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile();

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
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName3");

                        if (hdnId != null && hdnCategoryId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile();

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
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName4");

                        if (hdnId != null && hdnCategoryId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile();

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

        protected void BtnDeleteFile_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;
                bool deleted = false;

                if (FileIdToDelete > 0 && !string.IsNullOrEmpty(FileNameToDelete))
                {
                    try
                    {
                        session.BeginTransaction();

                        deleted = SqlCollaboration.DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(vSession.User, FileIdToDelete, false, false, session);
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
                        ElioCollaborationUsersLibraryFilesCategories userCategory = SqlCollaboration.GetCollaborationUserLibraryPublicFilesCategoriesById(vSession.User.Id, FileCategoryIdToDelete, session);
                        if (userCategory != null)
                        {
                            CategoryIdView = userCategory.Id;
                            DirectoryInfo filesInDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryFullTargetFolder"].ToString() + vSession.User.GuId + "\\" + userCategory.CategoryDescription + "\\"));

                            string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + vSession.User.GuId + "\\" + userCategory.CategoryDescription + "\\";

                            try
                            {
                                if (Directory.Exists(serverMapPathTargetFolder))
                                {
                                    foreach (FileInfo logFile in filesInDirectory.GetFiles())
                                    {
                                        if (logFile.Name == FileNameToDelete)
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

                            FileIdToDelete = -1;
                            FileNameToDelete = "";
                            FileCategoryIdToDelete = -1;
                        }

                        FixTabMenu();
                        LoadGridData(Rdg1, CategoryIdView, UcRgd1);

                        UpdatePanelContent.Update();
                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "File was deleted successfully", MessageTypes.Success, true, true, true, true, false);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "11")).Text;
                    GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, true, true, false);

                    FixTabMenu();
                }
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                string content = "File could not be deleted. Please try again later or contact with us.";
                GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, content, MessageTypes.Error, true, true, true, true, false);

                FixTabMenu();

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
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

                        deleted = SqlCollaboration.DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(vSession.User, LibraryFileId, true, false, session);
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

        protected void aBtnAdd_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    TbxReceiversPartners.CssClass = "form-control";
                    DdlPartnersList.CssClass = "form-control";

                    if (RcbxCollaborationPartners.Visible)
                    {
                        if (RcbxCollaborationPartners.Text != "")
                            TbxReceiversPartners.Text += RcbxCollaborationPartners.Text + "; ";

                        RcbxCollaborationPartners.Entries.Clear();
                    }
                    else
                    {
                        if (DdlPartnersList.SelectedValue != "0")
                        {
                            bool exists = ExistInReceivers(DdlPartnersList.SelectedItem.Text);
                            if (!exists)
                            {
                                TbxReceiversPartners.Text += DdlPartnersList.SelectedItem.Text + "; ";
                                FixTextAreasView(1);
                            }
                            else
                            {
                                TbxReceiversPartners.CssClass = "form-control border-danger";
                                DdlPartnersList.CssClass = "form-control border-danger";
                                return;
                            }
                        }
                    }

                    aRemoveReceivers.Visible = TbxReceiversPartners.Text != "";
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aBtnAddGroupMembers_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    TbxReceiversPartners.CssClass = "form-control";
                    TbxLibraryGroupsName.CssClass = "form-control";

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        if (TbxLibraryGroupsName.Text != "")
                        {
                            if (TbxReceiversPartners.Text != "")
                            {
                                if (!TbxReceiversPartners.Text.EndsWith("; "))
                                {
                                    TbxReceiversPartners.Text += "; ";
                                }
                            }

                            bool exists = ExistInReceivers(TbxLibraryGroupsName.Text);
                            if (!exists)
                            {
                                TbxReceiversPartners.Text += TbxLibraryGroupsName.Text + "; ";
                                FixTextAreasView(1);
                            }
                            else
                            {
                                TbxReceiversPartners.CssClass = "form-control border-danger";
                                TbxLibraryGroupsName.CssClass = "form-control border-danger";
                                return;
                            }
                        }

                        FillGroups();
                    }

                    aRemoveReceivers.Visible = TbxReceiversPartners.Text != "";
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

        protected void aBtnAddTiersMembers_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    TbxReceiversPartners.CssClass = "form-control";
                    TbxTiersPartners.CssClass = "form-control";

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        if (TbxTiersPartners.Text != "")
                        {
                            if (TbxReceiversPartners.Text != "")
                            {
                                if (!TbxReceiversPartners.Text.EndsWith("; "))
                                {
                                    TbxReceiversPartners.Text += "; ";
                                }
                            }

                            bool exists = ExistInReceivers(TbxTiersPartners.Text);
                            if (!exists)
                            {
                                TbxReceiversPartners.Text += TbxTiersPartners.Text + "; ";
                                FixTextAreasView(1);
                            }
                            else
                            {
                                TbxReceiversPartners.CssClass = "form-control border-danger";
                                TbxTiersPartners.CssClass = "form-control border-danger";
                                return;
                            }
                        }

                        GetTiersByUser();
                    }

                    aRemoveReceivers.Visible = TbxReceiversPartners.Text != "";
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

        protected void ImgBtnRemove_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //LblAllCollaborationPartners.Text = "";
                //ImgBtnRemove.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor aEditBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aEditBtn.NamingContainer;

                HdnGroupId.Value = item["id"].Text;
                GroupId = Convert.ToInt32(item["id"].Text);
                TbxEditRetailorName.Text = item["collaboration_group_name"].Text;

                IsEditMode = true;

                GetPartnersList("id", null, Convert.ToInt32(HdnGroupId.Value));

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenEditPopUp();", true);
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

        protected void BtnEditRetailor_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                divEditRetailorFailure.Visible = false;
                divEditRetailorSuccess.Visible = false;

                LblEditRetailorFailureMsg.Text = string.Empty;
                LblEditRetailorSuccessMsg.Text = string.Empty;

                if (vSession.User != null)
                {
                    if (TbxEditRetailorName.Text == string.Empty)
                    {
                        divEditRetailorFailure.Visible = true;
                        LblEditRetailorFailureMsg.Text = "Add your retailor's group name";
                        return;
                    }

                    List<int> vendResIDs = new List<int>();
                    vendResIDs = HasSelectedItem(RptEditRetailorsList);

                    if (vendResIDs.Count <= 1)
                    {
                        divEditRetailorFailure.Visible = true;
                        LblEditRetailorFailureMsg.Text = "Select at least two for your retailor's group";
                        return;
                    }

                    if (GroupId == -1)
                    {
                        divEditRetailorFailure.Visible = true;
                        LblEditRetailorFailureMsg.Text = "Retailor's group could not be updated.";
                        return;
                    }

                    SqlCollaboration.UpdateCollaborationUserGroupByGroupId(GroupId, vSession.User.Id, TbxEditRetailorName.Text, 1, 1, session);

                    foreach (RepeaterItem item in RptEditRetailorsList.Items)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                        var vendorResellerId = cbx.InputAttributes["id"];
                        HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");

                        if (vendorResellerId != null)
                        {
                            if (cbx.Checked)
                            {
                                if (!SqlCollaboration.ExistCollaborationUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session))
                                {
                                    ElioCollaborationUsersGroupMembers member = new ElioCollaborationUsersGroupMembers();

                                    member.CreatorUserId = vSession.User.Id;
                                    member.GroupRetailorId = Convert.ToInt32(hdnPartnerUserId.Value);
                                    member.CollaborationGroupId = GroupId;
                                    member.DateCreated = DateTime.Now;
                                    member.LastUpdate = DateTime.Now;
                                    member.IsActive = 1;
                                    member.IsPublic = 1;

                                    DataLoader<ElioCollaborationUsersGroupMembers> mLoader = new DataLoader<ElioCollaborationUsersGroupMembers>(session);
                                    mLoader.Insert(member);
                                }
                            }
                            else
                            {
                                if (SqlCollaboration.ExistCollaborationUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session))
                                {
                                    SqlCollaboration.DeleteCollaborationUserGroupRetailorByGroupId(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), session);
                                }
                            }
                        }
                        else
                        {
                            divEditRetailorFailure.Visible = true;
                            LblEditRetailorFailureMsg.Text = "Something went wrong. Retailor's group could not be updated.";
                            return;
                        }
                    }

                    divEditRetailorSuccess.Visible = true;
                    LblEditRetailorSuccessMsg.Text = "You updated your group successfully.";

                    FixUserLibraryGroups();

                    UpdatePanelContent.Update();
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

        protected void ImgBtnDeleteGroup_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                ImageButton btn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                HdnGroupId.Value = item["id"].Text;
                GroupId = Convert.ToInt32(item["id"].Text);

                if (GroupId > 0)
                {
                    SqlCollaboration.DeleteGroupById(GroupId, session);
                }

                session.CommitTransaction();

                FixUserLibraryGroups();
                //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenEditPopUp();", true);

                //RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                //if (rdgMailBox != null)
                //    rdgMailBox.Rebind();
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

        protected void BtnCreate_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                MessageControlUp.Visible = false;

                if (vSession.User != null)
                {
                    #region Validations

                    if (TbxLibraryGroupName.Text == string.Empty)
                    {
                        GlobalMethods.ShowMessageControlDA(MessageControlUp, "Please, add group description", MessageTypes.Error, true, true, true, true, false);
                        return;
                    }
                    else
                    {
                        if (!IsEditMode)
                        {
                            bool exist = SqlCollaboration.ExistLibraryGroupDescription(vSession.User.Id, TbxLibraryGroupName.Text.Trim(), session);
                            if (exist)
                            {
                                GlobalMethods.ShowMessageControlDA(MessageControlUp, "Sorry, this group name already exists by you!", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }
                        }
                    }

                    List<int> vendResIDs = HasSelectedItem(RptRetailorsList);

                    if (vendResIDs.Count <= 1)
                    {
                        GlobalMethods.ShowMessageControlDA(MessageControlUp, "Please, you must select at least two in order to create a group", MessageTypes.Error, true, true, true, true, false);
                        return;
                    }

                    #endregion

                    try
                    {
                        if (!IsEditMode)
                        {
                            #region Insert new Group

                            session.BeginTransaction();

                            ElioCollaborationUsersLibraryGroups group = new ElioCollaborationUsersLibraryGroups();
                            group.UserId = vSession.User.Id;
                            group.GroupDescription = TbxLibraryGroupName.Text.Trim();
                            group.Sysdate = DateTime.Now;
                            group.LastUpdated = DateTime.Now;
                            group.IsActive = 1;
                            group.IsPublic = 1;

                            DataLoader<ElioCollaborationUsersLibraryGroups> loader = new DataLoader<ElioCollaborationUsersLibraryGroups>(session);
                            loader.Insert(group);

                            foreach (int vendResId in vendResIDs)
                            {
                                ElioCollaborationUsersLibraryGroupMembers member = new ElioCollaborationUsersLibraryGroupMembers();

                                member.UserId = vSession.User.Id;
                                member.VendorResellerId = vendResId;
                                member.LibraryGroupId = group.Id;
                                member.Sysdate = DateTime.Now;
                                member.LastUpdated = DateTime.Now;
                                member.IsPublic = 1;

                                DataLoader<ElioCollaborationUsersLibraryGroupMembers> loaderMem = new DataLoader<ElioCollaborationUsersLibraryGroupMembers>(session);
                                loaderMem.Insert(member);
                            }

                            session.CommitTransaction();

                            GroupId = group.Id;

                            #endregion
                        }
                        else
                        {
                            #region Update Group

                            if (GroupId > 0)
                            {
                                vendResIDs.Clear();

                                session.BeginTransaction();

                                bool exist = SqlCollaboration.ExistLibraryGroupDescriptionToOtherGroupId(vSession.User.Id, GroupId, TbxLibraryGroupName.Text.Trim(), session);
                                if (exist)
                                {
                                    GlobalMethods.ShowMessageControlDA(MessageControlUp, "Sorry, this name already exists to other group of yours!", MessageTypes.Error, true, true, true, true, false);
                                    return;
                                }
                                else
                                {
                                    session.ExecuteQuery(@"UPDATE Elio_collaboration_users_library_groups
                                                    SET group_description = @group_description
                                                    WHERE id = @id
                                                    AND user_id = @user_id"
                                                        , DatabaseHelper.CreateStringParameter("@group_description", TbxLibraryGroupName.Text.Trim())
                                                        , DatabaseHelper.CreateIntParameter("@id", GroupId)
                                                        , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id));
                                }

                                foreach (RepeaterItem item in RptRetailorsList.Items)
                                {
                                    HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "id");

                                    CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                                    if (hdnId.Value != "")
                                    {
                                        bool existMember = SqlCollaboration.IsLibraryGroupMember(GroupId, Convert.ToInt32(hdnId.Value), session);
                                        if (cbxSelectUser.Checked)
                                        {
                                            if (!existMember)
                                                vendResIDs.Add(Convert.ToInt32(hdnId.Value));
                                        }
                                        else
                                        {
                                            if (existMember)
                                                session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_library_group_members 
                                                        WHERE user_id = @user_id 
                                                        AND vendor_reseller_id = @vendor_reseller_id 
                                                        AND library_group_id = @library_group_id"
                                                                    , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id)
                                                                    , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", Convert.ToInt32(hdnId.Value))
                                                                    , DatabaseHelper.CreateIntParameter("@library_group_id", GroupId));
                                        }
                                    }
                                }

                                foreach (int vendResId in vendResIDs)
                                {
                                    ElioCollaborationUsersLibraryGroupMembers member = new ElioCollaborationUsersLibraryGroupMembers();

                                    member.UserId = vSession.User.Id;
                                    member.VendorResellerId = vendResId;
                                    member.LibraryGroupId = GroupId;
                                    member.Sysdate = DateTime.Now;
                                    member.LastUpdated = DateTime.Now;
                                    member.IsPublic = 1;

                                    DataLoader<ElioCollaborationUsersLibraryGroupMembers> loaderMem = new DataLoader<ElioCollaborationUsersLibraryGroupMembers>(session);
                                    loaderMem.Insert(member);
                                }

                                session.CommitTransaction();
                            }
                            else
                                throw new Exception(string.Format("No Group Id while update from user {0}", vSession.User.Id.ToString()));

                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();
                        GlobalMethods.ShowMessageControlDA(MessageControlUp, "Sorry, something went wrong! Please try again later or contact us", MessageTypes.Error, true, true, true, true, false);
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        throw ex;
                    }

                    //ResetGroupItems();

                    GlobalMethods.ShowMessageControlDA(MessageControlUp, (!IsEditMode) ? "Group created successfully" : "Group updated successfully", MessageTypes.Success, true, true, true, true, false);

                    FixUserLibraryGroups();
                    IsEditMode = true;

                    UpdatePanel2.Update();
                    UpdatePanelContent.Update();
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

        protected void aEditGroup_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    GroupId = -1;
                    IsEditMode = false;

                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnGroupId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnGroupId");
                        Label lblGroupDescription = (Label)ControlFinder.FindControlRecursive(item, "LblGroupDescription");

                        MessageControlUp.Visible = false;

                        ResetGroupItems();

                        GroupId = Convert.ToInt32(hdnGroupId.Value);
                        TbxLibraryGroupName.Text = lblGroupDescription.Text;
                        IsEditMode = true;

                        GetPartnersList("id", null, 0);
                        RptRetailorsList.DataBind();
                        BtnCreate.Text = (!IsEditMode) ? "Create" : "Update";

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenGroupPopUp();", true);
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

        protected void aGroupDescription_ServerClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRetailorSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                //string whereLikeMode = "";
                //string searchLikeClause = "";
                //string programsLikeClause = "";
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                //whereLikeMode = "Name";
                criteria.CompanyName = TbxRetailorSearch.Text.Trim();

                GetPartnersListForRetailorsSearch("id", criteria);
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

        protected void ImgBtnEditRetailorSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                //string whereLikeMode = "";
                //string searchLikeClause = "";
                //string programsLikeClause = "";
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();
                //whereLikeMode = "Name";
                criteria.CompanyName = TbxEditRetailorsSearch.Text.Trim();

                GetPartnersListForEditRetailorsSearch("id", criteria);
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

        protected void LnkBtnRetailorAdvancedSearch_OnClck(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                MessageControlUp.Visible = false;

                if (divRetailorAdvancedSearch.Visible)
                {
                    divRetailorAdvancedSearch.Visible = false;
                    TbxRetailorSearch.Visible = true;
                    TbxRetailorAdvancedSearch.Visible = false;
                    TbxRetailorSearch.Text = "";
                }
                else
                {
                    DdlRetailorCountries.Items.Clear();
                    ListItem item = new ListItem();
                    item.Text = "Select country";
                    item.Value = "0";
                    DdlRetailorCountries.Items.Add(item);

                    List<ElioCountries> countries = Sql.GetPublicCountries(session);
                    foreach (ElioCountries country in countries)
                    {
                        item = new ListItem();

                        item.Text = country.CountryName;
                        item.Value = country.Id.ToString();

                        DdlRetailorCountries.Items.Add(item);
                    }

                    DdlRetailorRegions.Items.Clear();
                    item = new ListItem();
                    item.Text = "Select region";
                    item.Value = "0";
                    DdlRetailorRegions.Items.Add(item);

                    List<ElioCountries> regions = countries.GroupBy(x => x.Region).Select(y => y.First()).ToList();
                    foreach (ElioCountries region in regions)
                    {
                        item = new ListItem();

                        item.Text = region.Region;
                        item.Value = region.Id.ToString();

                        DdlRetailorRegions.Items.Add(item);
                    }

                    divRetailorAdvancedSearch.Visible = true;
                    TbxRetailorSearch.Visible = false;
                    TbxRetailorAdvancedSearch.Visible = true;
                    TbxRetailorAdvancedSearch.Text = "";

                    CbxRetailorSearchList.ClearSelection();

                    List<ElioPartners> allPartners = Sql.GetPartners(session);
                    int cbxCount = 1;
                    for (int i = 0; i < allPartners.Count; i++)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(this, "Rcbx" + cbxCount.ToString());
                        cbx.Text = allPartners[i].PartnerDescription.Replace("(VAR)", "").Replace("(Developers)", "");
                        cbx.Checked = false;

                        cbxCount++;
                    }
                }

                GetPartnersListForRetailorsSearch("id", null);
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

        protected void aBtnCreateGroup_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                GroupId = -1;
                IsEditMode = false;

                MessageControlUp.Visible = false;

                ResetGroupItems();

                GetPartnersList("id", null, 0);
                BtnCreate.Text = (!IsEditMode) ? "Create" : "Update";

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenGroupPopUp();", true);
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

        protected void aBtnLoadNextStep_ServerClick(object sender, EventArgs e)
        {
            try
            {
                UcReceiversAndUploadFile.Visible = false;
                TbxReceiversPartners.CssClass = DdlPartnersList.CssClass = TbxLibraryGroupsName.CssClass = TbxTiersPartners.CssClass = "form-control";

                if (divVendorsUploadFilesAreaStepOne.Visible)
                {
                    if (TbxReceiversPartners.Text.Trim() == "")
                    {
                        GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, "Please select at least one receiver", MessageTypes.Error, true, true, true, true, false);
                        return;
                    }
                    else
                    {
                        //PhSummaryPartners.Controls.Clear();
                        //LblSummaryReceivers.Text = "";

                        //List<string> partnersList = TbxReceiversPartners.Text.Trim().TrimEnd(';').Split(';').ToList();

                        //foreach (string receiver in partnersList)
                        //{
                        //    string partner = receiver.Trim().TrimEnd(' ').TrimStart(' ');

                        //    if (partner.Trim() != "")
                        //    {
                        //        Label lbl = new Label();
                        //        lbl.ID = Guid.NewGuid().ToString();
                        //        lbl.Text = partner + Environment.NewLine;

                        //        PhSummaryPartners.Controls.Add(lbl);

                        //        LblSummaryReceivers.Text += partner + Environment.NewLine;
                        //    }
                        //}

                        LoadStepTwo();
                    }
                }
                else if (divVendorsUploadFilesAreaStepTwo.Visible)
                {
                    //string alert = "";

                    //if (!Validate(out alert))
                    //{
                    //    GlobalMethods.ShowMessageControlDA(UcReceiversAndUploadFile, alert, MessageTypes.Error, true, true, true, true, false);
                    //    return;
                    //}

                    LoadStepThree();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aBtnLoadPreviousStep_ServerClick(object sender, EventArgs e)
        {
            try
            {
                UcReceiversAndUploadFile.Visible = false;
                TbxReceiversPartners.CssClass = DdlPartnersList.CssClass = TbxLibraryGroupsName.CssClass = TbxTiersPartners.CssClass = "form-control";

                if (divVendorsUploadFilesAreaStepTwo.Visible)
                {
                    LoadStepOne();
                }
                else if (divVendorsUploadFilesAreaStepThree.Visible)
                {
                    LoadStepTwo();
                }
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

                        deleted = SqlCollaboration.DeleteUserCollaborationLibraryCategoryFilesAndBlobById(vSession.User, FileCategoryIdToDelete, true, false, session);
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
                        ElioCollaborationUsersLibraryFilesCategories userCategory = SqlCollaboration.GetCollaborationUserLibraryPublicFilesCategoriesById(vSession.User.Id, FileCategoryIdToDelete, session);
                        if (userCategory != null)
                        {
                            DirectoryInfo filesInDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryFullTargetFolder"].ToString() + vSession.User.GuId + "\\" + userCategory.CategoryDescription + "\\"));

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

        protected void aRemoveReceivers_ServerClick(object sender, EventArgs e)
        {
            try
            {
                TbxReceiversPartners.Text = "";
                TbxReceiversPartners.CssClass = DdlPartnersList.CssClass = TbxLibraryGroupsName.CssClass = TbxTiersPartners.CssClass = "form-control";

                aRemoveReceivers.Visible = TbxReceiversPartners.Text != "";

                FixTextAreasView(0);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDeleteGroup_ServerClick(object sender, EventArgs e)
        {
            try
            {
                divSuccess.Visible = divFailure.Visible = false;

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnGroupId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnGroupId");

                        if (hdnGroupId != null)
                        {
                            GroupId = Convert.ToInt32(hdnGroupId.Value);
                            DeleteGroup(Convert.ToInt32(hdnGroupId.Value));

                            //GetCategoriesNewFilesNotificationsByCategoryId(item, "Elio_collaboration_users_library_files", Convert.ToInt32(hdnId.Value));
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

        protected void BtnDeleteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = UcMessageAlert.Visible = false;
                divSuccess.Visible = divFailure.Visible = false;

                if (vSession.User != null)
                {
                    if (GroupId > 0)
                    {
                        try
                        {
                            session.BeginTransaction();

                            session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_library_group_members WHERE library_group_id = @library_group_id AND user_id = @user_id"
                                                , DatabaseHelper.CreateIntParameter("@library_group_id", GroupId)
                                                , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id));

                            session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_library_groups WHERE id = @id AND user_id = @user_id"
                                                , DatabaseHelper.CreateIntParameter("@id", GroupId)
                                                , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id));

                            session.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();

                            string content = "Group could not be deleted! Please try again later or contact us.";
                            GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, content, MessageTypes.Error, true, true, true, true, false);
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfPopUp();", true);

                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                        FixUserLibraryGroups();

                        GroupId = -1;
                        IsEditMode = false;

                        //divSuccess.Visible = true;
                        //LblSuccessMsg.Text = "Group was deleted successfully";
                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Group was deleted successfully", MessageTypes.Success, true, true, true, true, false);

                        UpdatePanelContent.Update();
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);
                        //divFailure.Visible = true;
                        //LblFailureMsg.Text = "Group could not be deleted! Please try again later or contact us.";
                        GlobalMethods.ShowMessageAlertControlDA(UcMessageAlert, "Group could not be deleted! Please try again later or contact us.", MessageTypes.Error, true, true, true, true, false);
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                divFailure.Visible = true;
                LblFailureMsg.Text = "Group could not be deleted! Please try again later or contact us.";
                //string content = "Group could not be deleted! Please try again later or contact us.";
                //GlobalMethods.ShowMessageControlDA(MessageControlUp, content, MessageTypes.Error, true, true, true, true, false);
                //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfPopUp();", true);

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
                session.OpenConnection();

                if (DdlPartnersListSearch.SelectedValue != "0")
                {
                    Key = Sql.GetUserGUIDByIdTbl(Convert.ToInt32(DdlPartnersListSearch.SelectedValue), session);
                }
                else
                {
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

                    Key = key;
                }

                FixCategoriesItems();
                LoadGridData(Rdg1, CategoryIdView, UcRgd1);
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

        protected void RptRetailorsList_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        //ElioCollaborationVendorsResellersIJUsers partner = (ElioCollaborationVendorsResellersIJUsers)args.Item.DataItem;

                        CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                        System.Web.UI.WebControls.Image imgCompanyLogo = (System.Web.UI.WebControls.Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");

                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "id");
                        //HiddenField hdnCollaborationGroupId = (HiddenField)ControlFinder.FindControlRecursive(item, "collaboration_group_id");
                        HiddenField hdnMasterUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "master_user_id");
                        HiddenField hdnInvitationStatus = (HiddenField)ControlFinder.FindControlRecursive(item, "invitation_status");
                        HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");
                        HiddenField hdnEmail = (HiddenField)ControlFinder.FindControlRecursive(item, "email");

                        hdnId.Value = row["id"].ToString();
                        cbxSelectUser.InputAttributes.Add("id", hdnId.Value);

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            if (GroupId > 0)
                            {
                                cbxSelectUser.Checked = SqlCollaboration.IsLibraryGroupMember(GroupId, Convert.ToInt32(hdnId.Value), session);
                            }
                        }

                        //hdnCollaborationGroupId.Value = row["collaboration_group_id"].ToString();
                        //cbxSelectUser.InputAttributes.Add("collaboration_group_id", hdnCollaborationGroupId.Value);

                        hdnMasterUserId.Value = (vSession.User.CompanyType == Types.Vendors.ToString()) ? row["master_user_id"].ToString() : row["partner_user_id"].ToString();
                        cbxSelectUser.InputAttributes.Add("master_user_id", hdnMasterUserId.Value);

                        hdnPartnerUserId.Value = (vSession.User.CompanyType == Types.Vendors.ToString()) ? row["partner_user_id"].ToString() : row["master_user_id"].ToString();
                        cbxSelectUser.InputAttributes.Add("partner_user_id", hdnPartnerUserId.Value);

                        //hdnMasterUserId.Value = row["master_user_id"].ToString();
                        //cbxSelectUser.InputAttributes.Add("master_user_id", hdnMasterUserId.Value);

                        //hdnPartnerUserId.Value = row["partner_user_id"].ToString();
                        //cbxSelectUser.InputAttributes.Add("partner_user_id", hdnPartnerUserId.Value);

                        hdnInvitationStatus.Value = row["invitation_status"].ToString();
                        hdnEmail.Value = row["email"].ToString();

                        Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                        Label lblCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCountry");

                        lblCompanyName.Text = row["company_name"].ToString();
                        lblCountry.Text = row["country"].ToString();

                        //int partnerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(hdnPartnerUserId.Value) : Convert.ToInt32(hdnMasterUserId.Value);
                        ElioUsers company = Sql.GetUserById(Convert.ToInt32(hdnPartnerUserId.Value), session);

                        if (company != null)
                        {
                            aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.Profile(company);
                            aCompanyLogo.Target = aCompanyName.Target = "_blanck";
                            imgCompanyLogo.ImageUrl = company.CompanyLogo;
                            imgCompanyLogo.ToolTip = "View company's profile";
                            imgCompanyLogo.AlternateText = "Company logo";
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

        protected void RdgRetailorsGroups_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent

                    GridDataItem item = (GridDataItem)e.Item;

                    //int partnerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(item["partner_user_id"].Text) : Convert.ToInt32(item["master_user_id"].Text);
                    //ElioUsers company = Sql.GetUserById(Convert.ToInt32(item["partner_user_id"].Text), session);
                    HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                    ImageButton imgBtnDeleteGroup = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnDeleteGroup");
                    aEdit.Visible = imgBtnDeleteGroup.Visible = (vSession.User.CompanyType == Types.Vendors.ToString()) ? true : false;

                    Label lblGroupMessagesCount = (Label)ControlFinder.FindControlRecursive(item, "LblGroupMessagesCount");
                    HtmlControl spanGroupMessagesCount = (HtmlControl)ControlFinder.FindControlRecursive(item, "spanGroupMessagesCount");
                    if (item["group_msgs_count"].Text != "" && Convert.ToInt32(item["group_msgs_count"].Text) > 0)
                    {
                        lblGroupMessagesCount.Text = item["group_msgs_count"].Text;
                        spanGroupMessagesCount.Visible = true;
                    }
                    else
                    {
                        spanGroupMessagesCount.Visible = false;
                        lblGroupMessagesCount.Text = "";
                    }

                    #endregion
                }
                else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "UsersGroupRetailors")
                {
                    #region Vendors-Resellers

                    GridDataItem item = (GridDataItem)e.Item;

                    HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                    //HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                    ImageButton imgCompanyLogo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");

                    Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                    //Label lblCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCountry");
                    //CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
                    //lblCountry.Text = item["country"].Text;
                    //string retailorEmail = item["email"].Text;      // (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(item["partner_user_id"].Text) : Convert.ToInt32(item["master_user_id"].Text);
                    ElioUsers company = Sql.GetUserByEmail(item["email"].Text, session);

                    if (company != null)
                    {
                        lblCompanyName.Text = company.CompanyName;
                        aCompanyLogo.HRef = ControlLoader.Profile(company);
                        aCompanyLogo.Target = "_blank";
                        imgCompanyLogo.ImageUrl = company.CompanyLogo;
                        imgCompanyLogo.ToolTip = "View company's profile";
                        imgCompanyLogo.AlternateText = "Company logo";
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

        protected void RdgRetailorsGroups_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                session.OpenConnection();

                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "UsersGroupRetailors":
                        {
                            int groupId = Convert.ToInt32(dataItem.GetDataKeyValue("id").ToString());

                            //string orderByClause = "Elio_collaboration_vendors_resellers.id";

                            //List<ElioCollaborationVendorsResellersIJUsers> partners = new List<ElioCollaborationVendorsResellersIJUsers>();

                            GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                            //criteria.CompanyName = TbxSearch.Text;
                            //if (divAdvancedSearch.Visible)
                            //{
                            //    if (TbxAdvancedSearch.Text != "")
                            //        criteria.CompanyName = TbxAdvancedSearch.Text;

                            //    if (DdlCountries.SelectedValue != "0")
                            //    {
                            //        criteria.Country = DdlCountries.SelectedItem.Text;
                            //    }

                            //    if (DdlRegions.SelectedValue != "0")
                            //    {
                            //        criteria.Region = DdlRegions.SelectedItem.Text;
                            //    }

                            //    if (Cbx1.Checked)
                            //        criteria.PartnerPrograms.Add(Cbx1.Text);                     //criteria[3] = "'" + Cbx1.Text + "',";
                            //    if (Cbx2.Checked)
                            //        criteria.PartnerPrograms.Add(Cbx2.Text);                     //criteria[3] += "'" + Cbx2.Text + "',";
                            //    if (Cbx3.Checked)
                            //        criteria.PartnerPrograms.Add(Cbx3.Text);                     //criteria[3] += "'" + Cbx3.Text + "',";
                            //    if (Cbx4.Checked)
                            //        criteria.PartnerPrograms.Add(Cbx4.Text);                     //criteria[3] += "'" + Cbx4.Text + "',";
                            //    if (Cbx5.Checked)
                            //        criteria.PartnerPrograms.Add(Cbx5.Text);                     //criteria[3] += "'" + Cbx5.Text + "',";
                            //    if (Cbx6.Checked)
                            //        criteria.PartnerPrograms.Add(Cbx6.Text);                     //criteria[3] += "'" + Cbx6.Text + "',";
                            //    if (Cbx7.Checked)
                            //        criteria.PartnerPrograms.Add(Cbx7.Text);                     //criteria[3] += "'" + Cbx7.Text + "',";
                            //}

                            ////partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupIdAndCriteria(2, groupId, vSession.User.Id, CollaborateInvitationStatus.Confirmed.ToString(), vSession.User.CompanyType, criteria, orderByClause, session);

                            //int isActive = 1;
                            //int isPublic = 1;
                            //List<ElioCollaborationUsersGroupMembersIJUsers> members = SqlCollaboration.GetUserGroupMembersIJUsersByGroupId(groupId, isActive, isPublic, criteria, session);

                            //if (members.Count > 0)
                            //{
                            //    RptConnectionList.Visible = true;
                            //    UcMessageAlert.Visible = false;

                            //    DataTable table = new DataTable();

                            //    table.Columns.Add("id");
                            //    table.Columns.Add("creator_user_id");
                            //    table.Columns.Add("group_retailor_id");
                            //    table.Columns.Add("collaboration_group_id");
                            //    table.Columns.Add("company_name");
                            //    table.Columns.Add("email");
                            //    table.Columns.Add("company_logo");
                            //    table.Columns.Add("country");
                            //    table.Columns.Add("region");
                            //    table.Columns.Add("date_created");
                            //    table.Columns.Add("last_update");

                            //    if (vSession.User.CompanyType == Types.Vendors.ToString())
                            //    {
                            //        foreach (ElioCollaborationUsersGroupMembersIJUsers member in members)
                            //        {
                            //            table.Rows.Add(member.Id, member.CreatorUserId, member.GroupRetailorId, member.CollaborationGroupId, member.CompanyName, member.Email
                            //                , member.CompanyLogo, member.Country, member.Region, member.DateCreated, member.LastUpdate);
                            //        }
                            //    }
                            //    else
                            //    {
                            //        bool masterInserted = false;
                            //        foreach (ElioCollaborationUsersGroupMembersIJUsers member in members)
                            //        {
                            //            if (!masterInserted)
                            //            {
                            //                ElioUsersIJCountries groupMaster = Sql.GetUserIJCountryById(member.CreatorUserId, session);
                            //                if (groupMaster != null)
                            //                {
                            //                    table.Rows.Add(member.Id, member.CreatorUserId, member.GroupRetailorId, member.CollaborationGroupId, groupMaster.CompanyName, groupMaster.Email
                            //                    , groupMaster.CompanyLogo, groupMaster.Country, groupMaster.Region, member.DateCreated, member.LastUpdate);

                            //                    masterInserted = true;
                            //                }
                            //            }

                            //            if (vSession.User.Id != member.GroupRetailorId)
                            //            {
                            //                table.Rows.Add(member.Id, member.CreatorUserId, member.GroupRetailorId, member.CollaborationGroupId, member.CompanyName, member.Email
                            //                , member.CompanyLogo, member.Country, member.Region, member.DateCreated, member.LastUpdate);
                            //            }
                            //        }
                            //    }

                            //    e.DetailTableView.DataSource = table;
                            //}
                            //else
                            //{
                            //    e.DetailTableView.DataSource = null;
                            //    e.DetailTableView.Visible = false;

                            //    RdgRetailorsGroups.Visible = false;
                            //    //PnlCreateRetailorGroups.Enabled = false;
                            //    GlobalMethods.ShowMessageControlDA(MessageControlRetailors, "There are no Group members matching these criteria", MessageTypes.Info, true, true, false);
                            //}

                            #region to delete

                            //if (partners.Count > 0)
                            //{
                            //    RptConnectionList.Visible = true;
                            //    UcMessageAlert.Visible = false;

                            //    DataTable table = new DataTable();

                            //    table.Columns.Add("id");
                            //    table.Columns.Add("master_user_id");
                            //    table.Columns.Add("invitation_status");
                            //    table.Columns.Add("partner_user_id");
                            //    table.Columns.Add("company_name");
                            //    table.Columns.Add("email");
                            //    table.Columns.Add("company_logo");
                            //    table.Columns.Add("country");

                            //    foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                            //    {
                            //        int partnerId = partner.PartnerUserId;      //(vSession.User.CompanyType == Types.Vendors.ToString()) ? partner.PartnerUserId : partner.PartnerUserId;
                            //        ElioUsers company = Sql.GetUserById(partnerId, session);

                            //        if (company != null)
                            //        {
                            //            table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, company.CompanyLogo, company.Country);
                            //        }
                            //    }

                            //    e.DetailTableView.DataSource = table;
                            //}
                            //else
                            //{
                            //    e.DetailTableView.DataSource = null;
                            //    e.DetailTableView.Visible = false;
                            //}

                            #endregion
                        }

                        break;
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

        protected void RdgRetailorsGroups_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //RdgElioUsers.MasterTableView.Items[0].Expanded = true;
                //RdgElioUsers.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            }
        }

        #endregion

        #region Tabs

        protected void aIndividualPartners_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    aIndividualPartners.Attributes["class"] = "nav-link active";
                    tab_1_1.Attributes["class"] = "tab-pane fade show active";
                    tab_1_1.Visible = true;

                    aGroupPartners.Attributes["class"] = "nav-link";
                    tab_1_2.Attributes["class"] = "tab-pane fade";
                    tab_1_2.Visible = false;

                    aTiersPartners.Attributes["class"] = "nav-link";
                    tab_1_3.Attributes["class"] = "tab-pane fade";
                    tab_1_3.Visible = false;
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aGroupPartners_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FillGroups();

                    aGroupPartners.Attributes["class"] = "nav-link active";
                    tab_1_2.Attributes["class"] = "tab-pane fade show active";
                    tab_1_2.Visible = true;

                    aIndividualPartners.Attributes["class"] = "nav-link";
                    tab_1_1.Attributes["class"] = "tab-pane fade";
                    tab_1_1.Visible = false;

                    aTiersPartners.Attributes["class"] = "nav-link";
                    tab_1_3.Attributes["class"] = "tab-pane fade";
                    tab_1_3.Visible = false;
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

        protected void aTiersPartners_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    GetTiersByUser();

                    aTiersPartners.Attributes["class"] = "nav-link active";
                    tab_1_3.Attributes["class"] = "tab-pane fade show active";
                    tab_1_3.Visible = true;

                    aIndividualPartners.Attributes["class"] = "nav-link";
                    tab_1_1.Attributes["class"] = "tab-pane fade";
                    tab_1_1.Visible = false;

                    aGroupPartners.Attributes["class"] = "nav-link";
                    tab_1_2.Attributes["class"] = "tab-pane fade";
                    tab_1_2.Visible = false;
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

        protected void aSendFiles_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    aSendFiles.Attributes["class"] = "nav-link active";
                    aReceivedFiles.Attributes["class"] = "nav-link";

                    IsSendFiles = true;
                    FixCategoriesItems();
                    LoadGridData(Rdg1, CategoryIdView, UcRgd1);
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

        protected void aReceivedFiles_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    aReceivedFiles.Attributes["class"] = "nav-link active";
                    aSendFiles.Attributes["class"] = "nav-link";

                    IsSendFiles = false;
                    FixCategoriesItems();
                    LoadGridData(Rdg1, CategoryIdView, UcRgd1);
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

        #region DropDownLists

        protected void DdlFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    divVideoUpload.Visible = (DdlFileType.SelectedItem.Text == "Video");
            //    //divFileUpload.Visible = !divVideoUpload.Visible;
            //    TbxVideoLink.Text = "";
            //    //LblUploadFile.Text = (divVideoUpload.Visible) ? "Save video" : "Upload file";
            //    TbxFileTitle.Text = (DdlFileType.SelectedValue == "0") ? string.Empty : TbxFileTitle.Text;

            //    #region to delete

            //    if (divVideoUpload.Visible)
            //    {
            //        //TbxVideoLink.Enabled = !divVideoUpload.Visible;
            //        //BtnUploadFile.Enabled = !divVideoUpload.Visible;

            //        //LblFileUploadTitle.Text = "Video File Uploading Warning";
            //        //LblFileUploadfMsg.Text = "Temporary not available!";
            //        //GlobalMethods.ShowMessageControlDA(UploadMessageAlert, LblFileUploadfMsg.Text, MessageTypes.Error, false, true, false);
            //        //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
            //    }

            //    #endregion
            //}
            //catch (Exception ex)
            //{
            //    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            //}
        }

        protected void DdlLibraryGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                TbxLibraryGroupsName.Text = "";
                //TbxLibraryGroupsName.CssClass = "form-control";
                TbxReceiversPartners.CssClass = DdlPartnersList.CssClass = TbxLibraryGroupsName.CssClass = TbxTiersPartners.CssClass = "form-control";

                if (DdlLibraryGroups.SelectedValue == "0")
                {
                    TbxLibraryGroupsName.Text = "";
                    aBtnAddGroupMembers.Visible = false;
                    FixTextAreasView(2);
                }
                else
                {
                    DataTable groupPartners = SqlCollaboration.GetLibraryGroupUserMembersByGroupIdTbl(vSession.User.Id, Convert.ToInt32(DdlLibraryGroups.SelectedValue), vSession.User.CompanyType, session);

                    if (groupPartners != null && groupPartners.Rows.Count > 0)
                    {
                        TbxLibraryGroupsName.Text = "";
                        aBtnAddGroupMembers.Visible = true;

                        foreach (DataRow row in groupPartners.Rows)
                        {
                            TbxLibraryGroupsName.Text += row["company_name"] + "; ";
                        }

                        if (TbxLibraryGroupsName.Text.EndsWith("; "))
                        {
                            TbxLibraryGroupsName.Text = TbxLibraryGroupsName.Text.Substring(0, TbxLibraryGroupsName.Text.Length - 2);
                            FixTextAreasView(2);
                        }
                    }
                    else
                        TbxLibraryGroupsName.CssClass = "form-control border-danger";
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

        protected void DdlTiers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                TbxTiersPartners.Text = "";
                //TbxTiersPartners.CssClass = "form-control";
                TbxReceiversPartners.CssClass = DdlPartnersList.CssClass = TbxLibraryGroupsName.CssClass = TbxTiersPartners.CssClass = "form-control";

                if (DdlTiers.SelectedValue == "0")
                {
                    TbxTiersPartners.Text = "";
                    aBtnAddTiersMembers.Visible = false;
                    FixTextAreasView(3);
                }
                else
                {
                    DataTable trierPartners = SqlCollaboration.GetUserPartnersByTierDescriptionTbl(vSession.User.Id, DdlTiers.SelectedItem.Text, vSession.User.CompanyType, session);

                    if (trierPartners != null && trierPartners.Rows.Count > 0)
                    {
                        TbxTiersPartners.Text = "";
                        aBtnAddTiersMembers.Visible = true;

                        foreach (DataRow row in trierPartners.Rows)
                        {
                            TbxTiersPartners.Text += row["company_name"] + "; ";
                        }

                        if (TbxTiersPartners.Text.EndsWith("; "))
                        {
                            TbxTiersPartners.Text = TbxTiersPartners.Text.Substring(0, TbxTiersPartners.Text.Length - 2);
                            FixTextAreasView(3);
                        }
                    }
                    else
                    {
                        TbxTiersPartners.CssClass = "form-control border-danger";
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

        protected void DdlRetailorCountries_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                vSession.VendorsResellersList.Clear();
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                if (divRetailorAdvancedSearch.Visible)
                {
                    if (TbxRetailorAdvancedSearch.Text != "")
                        criteria.CompanyName = TbxRetailorAdvancedSearch.Text;

                    if (DdlRetailorCountries.SelectedValue != "0")
                    {
                        criteria.Country = DdlRetailorCountries.SelectedItem.Text;
                    }

                    if (DdlRetailorRegions.SelectedValue != "0")
                    {
                        criteria.Region = DdlRetailorRegions.SelectedItem.Text;
                    }

                    if (Rcbx1.Checked)
                        criteria.PartnerPrograms.Add(Rcbx1.Text);
                    if (Rcbx2.Checked)
                        criteria.PartnerPrograms.Add(Rcbx2.Text);
                    if (Rcbx3.Checked)
                        criteria.PartnerPrograms.Add(Rcbx3.Text);
                    if (Rcbx4.Checked)
                        criteria.PartnerPrograms.Add(Rcbx4.Text);
                    if (Rcbx5.Checked)
                        criteria.PartnerPrograms.Add(Rcbx5.Text);
                    if (Rcbx6.Checked)
                        criteria.PartnerPrograms.Add(Rcbx6.Text);
                    if (Rcbx7.Checked)
                        criteria.PartnerPrograms.Add(Rcbx7.Text);
                }

                GetPartnersListForRetailorsSearch("id", criteria);
                //GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", criteria);

                //RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                //if (rdgMailBox != null)
                //{
                //    rdgMailBox.Rebind();
                //}

                //ShowSelectedChatPartnersName(0);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlRetailorRegions_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioCountries> countries = null;
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                DdlRetailorCountries.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "Select country";
                item.Value = "0";
                DdlRetailorCountries.Items.Add(item);

                if (TbxRetailorAdvancedSearch.Text != "")
                    criteria.CompanyName = TbxRetailorAdvancedSearch.Text;

                if (DdlRetailorRegions.SelectedItem.Value != "0")
                {
                    countries = Sql.GetCountriesByRegion(DdlRetailorRegions.SelectedItem.Text, session);
                }
                else
                {
                    //countries = Sql.GetPublicCountries(session);

                    //item = new ListItem();
                    //item.Text = "Select country";
                    //item.Value = "0";
                    //DdlCountries.Items.Add(item);
                }

                foreach (ElioCountries country in countries)
                {
                    item = new ListItem();

                    item.Text = country.CountryName;
                    item.Value = country.Id.ToString();

                    DdlRetailorCountries.Items.Add(item);
                }

                DdlRetailorCountries.DataBind();

                if (DdlRetailorCountries.SelectedItem.Value != "0")
                    criteria.Country = DdlRetailorCountries.SelectedItem.Text;

                if (DdlRetailorRegions.SelectedItem.Value != "0")
                    criteria.Region = DdlRetailorRegions.SelectedItem.Text;

                GetPartnersListForRetailorsSearch("id", criteria);

                //RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                //if (rdgMailBox != null)
                //{
                //    rdgMailBox.Rebind();
                //}
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlEditRetailorCountries_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                vSession.VendorsResellersList.Clear();
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                if (divEditRetailorAdvancedSearch.Visible)
                {
                    if (TbxEditRetailorsAdvancedSearch.Text != "")
                        criteria.CompanyName = TbxEditRetailorsAdvancedSearch.Text;

                    if (DdlEditRetailorCountries.SelectedValue != "0")
                    {
                        criteria.Country = DdlEditRetailorCountries.SelectedItem.Text;
                    }

                    if (DdlEditRetailorRegions.SelectedValue != "0")
                    {
                        criteria.Region = DdlEditRetailorRegions.SelectedItem.Text;
                    }

                    if (EdRcbx1.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx1.Text);
                    if (EdRcbx2.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx2.Text);
                    if (EdRcbx3.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx3.Text);
                    if (EdRcbx4.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx4.Text);
                    if (EdRcbx5.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx5.Text);
                    if (EdRcbx6.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx6.Text);
                    if (EdRcbx7.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx7.Text);
                }

                GetPartnersListForEditRetailorsSearch("id", criteria);

                //RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                //if (rdgMailBox != null)
                //{
                //    rdgMailBox.Rebind();
                //}

                //ShowSelectedChatPartnersName(0);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlEditRetailorRegions_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioCountries> countries = null;
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                DdlEditRetailorCountries.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "Select country";
                item.Value = "0";
                DdlEditRetailorCountries.Items.Add(item);

                if (DdlEditRetailorRegions.SelectedItem.Value != "0")
                {
                    countries = Sql.GetCountriesByRegion(DdlEditRetailorRegions.SelectedItem.Text, session);
                }
                else
                {
                    countries = Sql.GetPublicCountries(session);

                    item = new ListItem();
                    item.Text = "Select country";
                    item.Value = "0";
                    DdlEditRetailorCountries.Items.Add(item);
                }

                foreach (ElioCountries country in countries)
                {
                    item = new ListItem();

                    item.Text = country.CountryName;
                    item.Value = country.Id.ToString();

                    DdlEditRetailorCountries.Items.Add(item);
                }

                DdlEditRetailorCountries.DataBind();

                if (TbxEditRetailorsAdvancedSearch.Text != "")
                    criteria.CompanyName = TbxEditRetailorsAdvancedSearch.Text;

                if (DdlEditRetailorCountries.SelectedItem.Value != "0")
                    criteria.Country = DdlEditRetailorCountries.SelectedItem.Text;

                if (DdlEditRetailorRegions.SelectedItem.Value != "0")
                    criteria.Region = DdlEditRetailorRegions.SelectedItem.Text;

                GetPartnersListForEditRetailorsSearch("id", criteria);

                //RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                //if (rdgMailBox != null)
                //{
                //    rdgMailBox.Rebind();
                //}
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TbxReceiversPartners.CssClass = DdlPartnersList.CssClass = TbxLibraryGroupsName.CssClass = TbxTiersPartners.CssClass = "form-control";

                BtnUploadFile.Visible = Ddlcategory.SelectedValue != "0";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlPartnersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TbxReceiversPartners.CssClass = DdlPartnersList.CssClass = TbxLibraryGroupsName.CssClass = TbxTiersPartners.CssClass = "form-control";

                aBtnAdd.Visible = DdlPartnersList.SelectedValue != "0";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DrpVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (DrpVendors.SelectedValue != "" && DrpVendors.SelectedValue != "0")
                {
                    Key = Sql.GetUserGUIDByIdTbl(Convert.ToInt32(DrpVendors.SelectedValue), session);

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

        protected void DdlPartnersListSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (DdlPartnersListSearch.SelectedValue != "0")
                {
                    Key = Sql.GetUserGUIDByIdTbl(Convert.ToInt32(DdlPartnersListSearch.SelectedValue), session);
                }
                else
                {
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

                    Key = key;
                }

                LoadGridData(Rdg1, CategoryIdView, UcRgd1);
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

        protected void CbxSelectAll_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                CheckBox cbxAll = (CheckBox)sender;

                var item = ((CheckBox)sender).Parent as RepeaterItem;
                if (item != null)
                {
                    foreach (RepeaterItem rptItem in RptRetailorsList.Items)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(rptItem, "CbxSelectUser");
                        if (cbx != null)
                        {
                            cbx.Checked = cbxAll.Checked;
                        }
                        else
                        {
                            Logger.DetailedError(string.Format("User with ID {0} tried to select All retailors at {1}, but checkBox could not be found", vSession.User.Id.ToString(), DateTime.Now.ToString()));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void CbxSelectGroupUsers_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                CheckBox cbx = (CheckBox)sender;

                GridDataItem item = (GridDataItem)cbx.NamingContainer;

                if (item != null)
                {
                    RestoreCheckBoxes(null, null, RptGroups, null);

                    HtmlControl divSimpleMailBox = (HtmlControl)ControlFinder.FindControlRecursive(this, "divSimpleMailBox");
                    divSimpleMailBox.Visible = false;

                    HtmlControl divGroupMailBox = (HtmlControl)ControlFinder.FindControlRecursive(this, "divGroupMailBox");
                    divGroupMailBox.Visible = true;

                    var groupId = Convert.ToInt32(item["id"].Text);
                    List<ElioUsers> groupUsers = new List<ElioUsers>();
                    int isActive = 1;
                    int isPublic = 1;

                    bool hasMembers = GlobalDBMethods.GetGroupMembersForMessages((int)groupId, isActive, isPublic, vSession.User.CompanyType, vSession.User.Id, out groupUsers, session);

                    if (hasMembers)
                    {
                        RadGrid rdgGroupMailBox = null;

                        //rdgGroupMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgGroupMailBox");
                        if (rdgGroupMailBox != null)
                        {
                            if (cbx.Checked)
                            {
                                //UcRdgMailBoxList.CollaborationGroupId = groupId;

                                Label lblGroupMessagesCount = (Label)ControlFinder.FindControlRecursive(item, "LblGroupMessagesCount");
                                HtmlControl spanGroupMessagesCount = (HtmlControl)ControlFinder.FindControlRecursive(item, "spanGroupMessagesCount");
                                lblGroupMessagesCount.Text = "";
                                spanGroupMessagesCount.Visible = false;
                            }
                            else
                            {
                                //UcRdgMailBoxList.CollaborationGroupId = -1;
                            }

                            //bool changeMailCountStatus = false;
                            //SetMailsAsViewed(out changeMailCountStatus);

                            //if (changeMailCountStatus)
                            //{
                            //    ShowNotificationsMessages();
                            //}

                            rdgGroupMailBox.Visible = cbx.Checked;
                            Control ucMailBoxListMessageAlert = null;

                            //ucMailBoxListMessageAlert = (Control)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "UcMailBoxListMessageAlert");
                            if (ucMailBoxListMessageAlert != null)
                                ucMailBoxListMessageAlert.Visible = false;

                            if (cbx.Checked)
                            {
                                rdgGroupMailBox.Rebind();

                                //int simpleMsgCount = 0;
                                //int groupMsgCount = 0;
                                //int totalNewMsgCount = 0;
                                //this.Master.ShowMasterNotifications(out simpleMsgCount, out groupMsgCount, out totalNewMsgCount);

                                //UpdatePanel updatePanel1 = (UpdatePanel)ControlFinder.FindControlRecursive(this.Master, "UpdatePanel1");
                                //if (updatePanel1 != null)
                                //    updatePanel1.Update();

                                if (hasMembers)
                                    ShowNotificationsMessages();
                            }

                            if (session.Connection.State == ConnectionState.Closed)
                                session.OpenConnection();

                            ShowSelectedChatPartnersName((cbx.Checked) ? groupId : -1);

                            UpdatePanel1.Update();
                        }
                    }
                    //else
                    //    divGroupImages.Visible = false;
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

        #region TextBoxes

        protected void TbxRetailorSearch_OnTextChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();
                criteria.CompanyName = TbxRetailorSearch.Text;

                if (divRetailorAdvancedSearch.Visible)
                {
                    if (TbxRetailorAdvancedSearch.Text != "")
                        criteria.CompanyName = TbxRetailorAdvancedSearch.Text;

                    if (DdlRetailorCountries.SelectedItem.Value != "0")
                        criteria.Country = DdlRetailorCountries.SelectedItem.Text;

                    if (DdlRetailorRegions.SelectedItem.Value != "0")
                        criteria.Region = DdlRetailorRegions.SelectedItem.Text;

                    if (Rcbx1.Checked)
                        criteria.PartnerPrograms.Add(Rcbx1.Text);
                    if (Rcbx2.Checked)
                        criteria.PartnerPrograms.Add(Rcbx2.Text);
                    if (Rcbx3.Checked)
                        criteria.PartnerPrograms.Add(Rcbx3.Text);
                    if (Rcbx4.Checked)
                        criteria.PartnerPrograms.Add(Rcbx4.Text);
                    if (Rcbx5.Checked)
                        criteria.PartnerPrograms.Add(Rcbx5.Text);
                    if (Rcbx6.Checked)
                        criteria.PartnerPrograms.Add(Rcbx6.Text);
                    if (Rcbx7.Checked)
                        criteria.PartnerPrograms.Add(Rcbx7.Text);
                }

                GetPartnersListForRetailorsSearch("id", criteria);

                //RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                //if (rdgMailBox != null)
                //{
                //    rdgMailBox.Rebind();
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

        protected void TbxEditRetailorsSearch_OnTextChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                criteria.CompanyName = TbxEditRetailorsSearch.Text.Trim();

                if (divEditRetailorAdvancedSearch.Visible)
                {
                    if (TbxEditRetailorsAdvancedSearch.Text != "")
                        criteria.CompanyName = TbxEditRetailorsAdvancedSearch.Text;

                    if (DdlEditRetailorCountries.SelectedItem.Value != "0")
                        criteria.Country = DdlEditRetailorCountries.SelectedItem.Text;

                    if (DdlEditRetailorRegions.SelectedItem.Value != "0")
                        criteria.Region = DdlEditRetailorRegions.SelectedItem.Text;

                    if (EdRcbx1.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx1.Text);
                    if (EdRcbx2.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx2.Text);
                    if (EdRcbx3.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx3.Text);
                    if (EdRcbx4.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx4.Text);
                    if (EdRcbx5.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx5.Text);
                    if (EdRcbx6.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx6.Text);
                    if (EdRcbx7.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx7.Text);
                }

                GetPartnersListForEditRetailorsSearch("id", criteria);

                //RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                //if (rdgMailBox != null)
                //{
                //    rdgMailBox.Rebind();
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

        #endregion
    }
}