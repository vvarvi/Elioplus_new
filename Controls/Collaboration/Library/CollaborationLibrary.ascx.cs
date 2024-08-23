using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Controls.AlertControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Controls.Collaboration.Library
{
    public partial class CollaborationLibrary : System.Web.UI.UserControl
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    //ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    //RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, out key, session);

                    Key = key;

                    if (isError)
                    {
                        Response.Redirect(errorPage, false);
                        return;
                    }

                    if (!IsPostBack)
                        FixPage();
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
            UpdateStrings();
            SetLinks();
            //LoadFileTypes();
            FillDropLists();
            FixTabsContent(1);

            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
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
            }
            else
            {
                divInvitationToPartners.Visible = false;
            }

            divVendorsUploadFilesArea.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

            UcMessageAlert.Visible = false;
            UploadMessageAlert.Visible = false;
            vSession.VendorsResellersList.Clear();
            vSession.VendorsResellersList = null;
        }

        private void GetCategoriesNewFilesNotifications(string tableName)
        {
            try
            {
                session.OpenConnection();

                int userFiles = vSession.User.Id;

                DataTable newFilesNotifications = session.GetDataTable(@";with a
                                                                            as
                                                                            (
                                                                            SELECT category_id, sum(is_new) as count
                                                                                FROM " + tableName + " " +
                                                                                "where user_id = @user_id " +
                                                                                "and is_public = 1 " +
                                                                                "group by category_id" +
                                                                            ") " +
                                                                            "select category_id, count " + 
                                                                            "from a " +
                                                                            "where count > 0"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userFiles));

                if (newFilesNotifications != null && newFilesNotifications.Rows.Count > 0)
                {
                    foreach (DataRow row in newFilesNotifications.Rows)
                    {
                        if (row["category_id"].ToString() == "1")
                        {
                            UcControl1.IsVisible = true;
                            UcControl1.Text = row["count"].ToString();
                        }
                        else if (row["category_id"].ToString() == "2")
                        {
                            UcControl2.IsVisible = true;
                            UcControl2.Text = row["count"].ToString();
                        }
                        else if (row["category_id"].ToString() == "3")
                        {
                            UcControl3.IsVisible = true;
                            UcControl3.Text = row["count"].ToString();
                        }
                        else if (row["category_id"].ToString() == "4")
                        {
                            UcControl4.IsVisible = true;
                            UcControl4.Text = row["count"].ToString();
                        }
                        else if (row["category_id"].ToString() == "5")
                        {
                            UcControl5.IsVisible = true;
                            UcControl5.Text = row["count"].ToString();
                        }
                        else if (row["category_id"].ToString() == "6")
                        {
                            UcControl6.IsVisible = true;
                            UcControl6.Text = row["count"].ToString();
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

        private void SetLinks()
        {
            //aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
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

        private bool Validate(out string alert)
        {
            alert = "";

            if (TbxFileTitle.Text == string.Empty)
            {
                alert = "Fill file title";

                return false;
            }

            //if (DdlFileType.SelectedItem.Value == "0")
            //{
            //    alert = "Select file type";

            //    return false;
            //}

            if (Ddlcategory.SelectedItem.Value == "0")
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
                    alert = "Your file size is outside the bounds. Please try smaller file size to send or contact with us.";

                    return false;
                }

                #region User Available File Storage Space

                if (!GlobalDBMethods.UserHasAvailableStorage(vSession.User.Id, fileContent.ContentLength, session))
                {
                    alert = " Your available storage space is not enough for this file to be uploaded";
                    return false;
                }

                #endregion
            }
            else
            {
                alert = "Select file to upload!";

                return false;
            }

            # endregion

            return true;
        }

        private void LoadFileTypes()
        {
            //DdlFileType.Items.Clear();

            //ListItem item = new ListItem();

            //item.Value = "0";
            //item.Text = "Select file type";

            //DdlFileType.Items.Add(item);

            //List<ElioOnboardingFileTypes> fileTypes = Sql.GetOnboardingFileTypes(session);

            //foreach (ElioOnboardingFileTypes type in fileTypes)
            //{
            //    //if (type.Id != 4)
            //    //{
            //    item = new ListItem();
            //    item.Text = type.FileType;
            //    item.Value = type.Id.ToString();

            //    DdlFileType.Items.Add(item);
            //    //}
            //}
        }

        private void ResetPanelItems()
        {
            TbxFileTitle.Text = string.Empty;
            divFileUpload.Visible = true;      
        }

        private void FillDropLists()
        {
            List<ElioCollaborationLibraryFilesDefaultCategories> defaultCategories = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategories(session);

            if (defaultCategories.Count > 0)
            {
                Ddlcategory.Items.Clear();

                ListItem item = new ListItem();

                item.Value = "0";
                item.Text = "Choose category";

                Ddlcategory.Items.Add(item);

                foreach (ElioCollaborationLibraryFilesDefaultCategories defaultCategory in defaultCategories)
                {
                    item = new ListItem();

                    item.Value = defaultCategory.Id.ToString();
                    item.Text = defaultCategory.CategoryDescription;

                    Ddlcategory.Items.Add(item);
                }
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

        private void DeleteFile(int id, int categoryId, string fileName)
        {
            if (id > 0 && fileName != "" && categoryId > -1)
            {
                GlobalMethods.ShowMessageControl(UcConfirmationMessageControl, "Are you sure you want to delete this file from your library?", MessageTypes.Warning, true, true, true, true, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            else
            {
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "11")).Text;
                GlobalMethods.ShowMessageAlertControl(UcMessageAlert, alert, MessageTypes.Error, true, true, true, true, false);
            }

            GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");
        }

        private void FixTabsContent(int tab)
        {
            aTabProductUpdates.Attributes["class"] = "navi-link";
            tab_1_1.Attributes["class"] = "tab-pane fade";
            tab_1_1.Visible = false;
            Rdg1.Visible = false;

            aTabMarketingMaterial.Attributes["class"] = "navi-link";
            tab_1_2.Attributes["class"] = "tab-pane fade";
            tab_1_2.Visible = false;
            Rdg2.Visible = false;

            aTabNewCampaign.Attributes["class"] = "navi-link";
            tab_1_3.Attributes["class"] = "tab-pane fade";
            tab_1_3.Visible = false;
            Rdg3.Visible = false;

            tab_1_4.Attributes["class"] = "tab-pane fade";
            aTabNewsLetter.Attributes["class"] = "navi-link";
            tab_1_4.Visible = false;
            Rdg4.Visible = false;

            tab_1_5.Attributes["class"] = "tab-pane fade";
            aTabBanner.Attributes["class"] = "navi-link";
            tab_1_5.Visible = false;
            Rdg5.Visible = false;

            tab_1_6.Attributes["class"] = "tab-pane fade";
            aTabDocumentationPdf.Attributes["class"] = "navi-link";
            tab_1_6.Visible = false;
            Rdg6.Visible = false;

            switch (tab)
            {
                case 1:
                    aTabProductUpdates.Attributes["class"] = "navi-link active";
                    tab_1_1.Attributes["class"] = "tab-pane fade show active";
                    tab_1_1.Visible = true;
                    LoadGridData(Rdg1, 1, UcRgd1);
                    break;

                case 2:
                    aTabMarketingMaterial.Attributes["class"] = "navi-link active";
                    tab_1_2.Attributes["class"] = "tab-pane fade show active";
                    tab_1_2.Visible = true;
                    LoadGridData(Rdg2, 2, UcRgd2);
                    break;

                case 3:
                    aTabNewCampaign.Attributes["class"] = "navi-link active";
                    tab_1_3.Attributes["class"] = "tab-pane fade show active";
                    tab_1_3.Visible = true;
                    LoadGridData(Rdg3, 3, UcRgd3);
                    break;

                case 4:
                    tab_1_4.Attributes["class"] = "tab-pane fade show active";
                    aTabNewsLetter.Attributes["class"] = "navi-link active";
                    tab_1_4.Visible = true;
                    LoadGridData(Rdg4, 4, UcRgd4);
                    break;

                case 5:
                    tab_1_5.Attributes["class"] = "tab-pane fade show active";
                    aTabBanner.Attributes["class"] = "navi-link active";
                    tab_1_5.Visible = true;
                    LoadGridData(Rdg5, 5, UcRgd5);
                    break;

                case 6:
                    tab_1_6.Attributes["class"] = "tab-pane fade show active";
                    aTabDocumentationPdf.Attributes["class"] = "navi-link active";
                    tab_1_6.Visible = true;
                    LoadGridData(Rdg6, 6, UcRgd6);
                    break;
            }

            GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");
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
                        NotificationControl ucNotif1 = (NotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif1");
                        ucNotif1.IsVisible = ucNotif1 != null ? Convert.ToInt32(row["is_new1"]) == 1 : false;

                        HtmlAnchor aDelete1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete1");

                        if (aDelete1 != null)
                            aDelete1.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                        Label lbl1 = (Label)ControlFinder.FindControlRecursive(item, "Lbl1");

                        if (row["file_title1"].ToString().Length >= 45)
                        {
                            lbl1.Text = row["file_title1"].ToString().Substring(0, 45) + "...";
                        }
                        else
                        {
                            lbl1.Text = row["file_title1"].ToString();
                        }
                    }
                    if (div2.Visible)
                    {
                        NotificationControl ucNotif2 = (NotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif2");
                        ucNotif2.IsVisible = ucNotif2 != null ? Convert.ToInt32(row["is_new2"]) == 1 : false;

                        HtmlAnchor aDelete2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete2");

                        if (aDelete2 != null)
                            aDelete2.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                        Label lbl2 = (Label)ControlFinder.FindControlRecursive(item, "Lbl2");

                        if (row["file_title2"].ToString().Length >= 45)
                        {
                            lbl2.Text = row["file_title2"].ToString().Substring(0, 45) + "...";
                        }
                        else
                        {
                            lbl2.Text = row["file_title2"].ToString();
                        }
                    }
                    if (div3.Visible)
                    {
                        NotificationControl ucNotif3 = (NotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif3");
                        ucNotif3.IsVisible = ucNotif3 != null ? Convert.ToInt32(row["is_new3"]) == 1 : false;

                        HtmlAnchor aDelete3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete3");

                        if (aDelete3 != null)
                            aDelete3.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                        Label lbl3 = (Label)ControlFinder.FindControlRecursive(item, "Lbl3");

                        if (row["file_title3"].ToString().Length >= 45)
                        {
                            lbl3.Text = row["file_title3"].ToString().Substring(0, 45) + "...";
                        }
                        else
                        {
                            lbl3.Text = row["file_title3"].ToString();
                        }
                    }
                    if (div4.Visible)
                    {
                        NotificationControl ucNotif4 = (NotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif4");
                        ucNotif4.IsVisible = ucNotif4 != null ? Convert.ToInt32(row["is_new4"]) == 1 : false;

                        HtmlAnchor aDelete4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete4");

                        if (aDelete4 != null)
                            aDelete4.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                        Label lbl4 = (Label)ControlFinder.FindControlRecursive(item, "Lbl4");

                        if (row["file_title4"].ToString().Length >= 45)
                        {
                            lbl4.Text = row["file_title4"].ToString().Substring(0, 45) + "...";
                        }
                        else
                        {
                            lbl4.Text = row["file_title4"].ToString();
                        }
                    }
                }
            }
        }

        private void LoadGridData(Repeater rpt, int categoryID, MessageControl control)
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

                    ElioUsers partner = Sql.GetUserByGuId(Key, session);

                    List<ElioCollaborationUsersLibraryFiles> files = new List<ElioCollaborationUsersLibraryFiles>();

                    if (partner == null)
                    {
                        partner = Sql.GetUserById(vSession.User.Id, session);

                        files = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, categoryID, "0,1", false, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);
                    }
                    else if (partner != null)        //(Key != "")
                    {
                        files = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(partner.Id, vSession.User.Id, categoryID, "0,1", true, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);
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
                                if (!FileExistsNew1(table, files[index].FileName))
                                    files[index].FileType = GetItemImageByType(files[index].FilePath);

                                if (!FileExistsNew2(table, files[index + 1].FileName))
                                    files[index + 1].FileType = GetItemImageByType(files[index + 1].FilePath);

                                if (!FileExistsNew3(table, files[index + 2].FileName))
                                    files[index + 2].FileType = GetItemImageByType(files[index + 2].FilePath);

                                if (!FileExistsNew4(table, files[index + 3].FileName))
                                    files[index + 3].FileType = GetItemImageByType(files[index + 3].FilePath);

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
                                index = index + 4;

                                table.Rows.Add(row);
                            }

                            if (columns == 1)
                            {
                                if (!FileExistsNew1(table, files[index].FileName))
                                    files[index].FileType = GetItemImageByType(files[index].FilePath);

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

                                table.Rows.Add(row);
                            }
                            else if (columns == 2)
                            {
                                if (!FileExistsNew1(table, files[index].FileName))
                                    files[index].FileType = GetItemImageByType(files[index].FilePath);

                                if (!FileExistsNew2(table, files[index + 1].FileName))
                                    files[index + 1].FileType = GetItemImageByType(files[index + 1].FilePath);

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

                                table.Rows.Add(row);
                            }
                            else if (columns == 3)
                            {
                                if (!FileExistsNew1(table, files[index].FileName))
                                    files[index].FileType = GetItemImageByType(files[index].FilePath);

                                if (!FileExistsNew2(table, files[index + 1].FileName))
                                    files[index + 1].FileType = GetItemImageByType(files[index + 1].FilePath);

                                if (!FileExistsNew3(table, files[index + 2].FileName))
                                    files[index + 2].FileType = GetItemImageByType(files[index + 2].FilePath);

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
                            GlobalMethods.ShowMessageControl(control, "There are no files uploaded", MessageTypes.Info, true, true, false, true, false);
                        }
                    }
                    else
                    {
                        rpt.Visible = false;
                        GlobalMethods.ShowMessageControl(control, "There are no files uploaded", MessageTypes.Info, true, true, false, true, false);
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

        private void PreviewFile(int isNew, string url, int id, NotificationControl ucNotification, DBSession session)
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

            GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");

            Response.Write("<script>");
            Response.Write("window.open('" + url + "','_blank')");
            Response.Write("</script>");
        }

        #endregion

        #region Buttons

        protected void BtnUploadFile_OnCick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                string serverMapPathTargetFolder = "";
                string fileName = "";
                bool successUpload = false;
                UcMessageAlert.Visible = false;
                string alert = "";

                if (!Validate(out alert))
                {
                    GlobalMethods.ShowMessageAlertControl(UcMessageAlert, alert, MessageTypes.Error, true, true, true, true, false);
                    LblFileUploadTitle.Text = "File Uploading Warning";
                    //LblFileUploadfMsg.Text = alert;
                    //GlobalMethods.ShowMessageControl(UploadMessageAlert, alert, MessageTypes.Error, true, true, true, true, false);
                    //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                    GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");

                    UpdatePanelContent.Update();

                    return;
                }

                var fileContent = inputFile.PostedFile;

                if (fileContent != null)
                {
                    try
                    {
                        #region updoad file

                        serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + Ddlcategory.SelectedItem.Text + "\\" + vSession.User.GuId + "\\";

                        fileName = fileContent.FileName;

                        successUpload = UpLoadImage.UpLoadFile(serverMapPathTargetFolder, fileContent, out fileName, session);

                        #endregion

                        if (successUpload)
                        {
                            try
                            {
                                #region File Uploading Area

                                session.BeginTransaction();

                                #region User File

                                ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                                libraryFile.CategoryId = Convert.ToInt32(Ddlcategory.SelectedValue);
                                libraryFile.FileTitle = "Library_" + TbxFileTitle.Text.Replace(" ", "_").Trim();
                                libraryFile.FileName = fileName;
                                libraryFile.FilePath = Ddlcategory.SelectedItem.Text + "/" + vSession.User.GuId + "/" + fileName;       //serverMapPathTargetFolder;
                                libraryFile.FileType = fileContent.ContentType;
                                libraryFile.IsPublic = 1;
                                libraryFile.MailboxId = -1;
                                libraryFile.IsNew = 1;
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
                                //blobFile.FilePath = serverMapPathTargetFolder;
                                blobFile.FileSize = fileContent.ContentLength;
                                blobFile.FileType = fileContent.ContentType;
                                blobFile.IsPublic = 1;
                                blobFile.DateCreated = DateTime.Now;
                                blobFile.LastUpdate = DateTime.Now;
                                blobFile.LibraryFilesId = libraryFile.Id;
                                blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolder + "/" + blobFile.FileName);

                                DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                blbLoader.Insert(blobFile);

                                #endregion

                                #region  Update User PacketStatus Available File Storage Count

                                bool success = GlobalDBMethods.ReduceUserFileStorage(vSession.User.Id, Convert.ToDouble(fileContent.ContentLength), session);
                                if (!success)
                                    throw new Exception(string.Format("User {0} tried to upload a file at {1} but he has not enough storage space from his packet (user billing packet type id = {2}), so it could not be saved or uploaded!", vSession.User.Id, DateTime.Now.ToString(), vSession.User.BillingType.ToString()));

                                #endregion

                                session.CommitTransaction();

                                #endregion
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                throw ex;
                            }

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
                                throw ex;
                            }
                        }
                        else
                        {
                            LblFileUploadTitle.Text = "File Uploading Warning";
                            string contentAlert = "File could not be uploaded to your library (maybe it's size was outside the bounds). Please try again or contact with us.";
                            GlobalMethods.ShowMessageAlertControl(UcMessageAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);
                            //GlobalMethods.ShowMessageControl(UploadMessageAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);
                            //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                            GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");

                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        LblFileUploadTitle.Text = "File Uploading Warning";
                        string contentAlert = "File could not be uploaded to your onboarding library";
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);
                        //GlobalMethods.ShowMessageControl(UploadMessageAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);
                        //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                        bool deleted = false;

                        if (successUpload)
                            deleted = UpLoadImage.DeleteFileFromDirectory(serverMapPathTargetFolder, fileName);

                        Logger.DetailedError(Request.Url.ToString(), (deleted) ? string.Format("File not deleted successfully int path {0}", serverMapPathTargetFolder + "\\" + fileName) : "", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (successUpload)
                {
                    string content = "File was successfully uploaded to your library.";
                    LblFileUploadTitle.Text = "File Uploading";

                    GlobalMethods.ShowMessageAlertControl(UcMessageAlert, content, MessageTypes.Success, true, true, true, true, false);
                    //GlobalMethods.ShowMessageControl(UploadMessageAlert, content, MessageTypes.Error, true, true, true, true, false);
                    //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
                else
                {
                    string contentAlert = "File could not be uploaded to your onboarding library";
                    GlobalMethods.ShowMessageAlertControl(UcMessageAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);
                    //GlobalMethods.ShowMessageControl(UploadMessageAlert, contentAlert, MessageTypes.Error, true, true, true, true, false);
                    //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }

                switch (Ddlcategory.SelectedValue)
                {
                    case "1":
                    case "9":
                        FixTabsContent(1);

                        break;

                    case "2":
                    case "10":
                        FixTabsContent(2);

                        break;

                    case "3":
                    case "11":
                        FixTabsContent(3);

                        break;

                    case "4":
                    case "12":
                        FixTabsContent(4);

                        break;

                    case "5":
                    case "13":
                        FixTabsContent(5);

                        break;

                    case "6":
                    case "14":
                        FixTabsContent(6);

                        break;

                    case "7":
                    case "15":
                        FixTabsContent(7);

                        break;

                    case "8":
                    case "16":
                        FixTabsContent(8);

                        break;

                    case "17":
                        FixTabsContent(9);

                        break;
                }

                ResetPanelItems();
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
                        //HiddenField hdnFileTypeId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileTypeId1");
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName1");

                        if (hdnId != null && hdnCategoryId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            //FileTypeIdToDelete = Convert.ToInt32(hdnFileTypeId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile(FileIdToDelete, FileCategoryIdToDelete, FileNameToDelete);
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
                        //HiddenField hdnFileTypeId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileTypeId2");
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName2");

                        if (hdnId != null && hdnCategoryId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            //ileTypeIdToDelete = Convert.ToInt32(hdnFileTypeId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile(FileIdToDelete, FileCategoryIdToDelete, FileNameToDelete);
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
                        //HiddenField hdnFileTypeId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileTypeId3");
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName3");

                        if (hdnId != null && hdnCategoryId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            //FileTypeIdToDelete = Convert.ToInt32(hdnFileTypeId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile(FileIdToDelete, FileCategoryIdToDelete, FileNameToDelete);
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
                        //HiddenField hdnFileTypeId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileTypeId4");
                        HiddenField hdnFileName = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName4");

                        if (hdnId != null && hdnCategoryId != null && hdnFileName != null)
                        {
                            FileIdToDelete = Convert.ToInt32(hdnId.Value);
                            //FileTypeIdToDelete = Convert.ToInt32(hdnFileTypeId.Value);
                            FileCategoryIdToDelete = Convert.ToInt32(hdnCategoryId.Value);
                            FileNameToDelete = hdnFileName.Value;

                            DeleteFile(FileIdToDelete, FileCategoryIdToDelete, FileNameToDelete);
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

                        SqlCollaboration.DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(vSession.User, FileIdToDelete, false, false, session);
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
                        ElioCollaborationLibraryFilesDefaultCategories fCategory = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(FileCategoryIdToDelete, session);
                        if (fCategory != null)
                        {
                            DirectoryInfo filesInDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryFullTargetFolder"].ToString() + fCategory.CategoryDescription + "\\" + vSession.User.GuId + "\\"));

                            foreach (FileInfo logFile in filesInDirectory.GetFiles())
                            {
                                if (logFile.Name == FileNameToDelete)
                                {
                                    logFile.Delete();

                                    FileIdToDelete = -1;
                                    FileNameToDelete = "";

                                    break;
                                }
                            }

                            switch (fCategory.Id.ToString())
                            {
                                case "1":
                                    FixTabsContent(1);

                                    break;

                                case "2":
                                    FixTabsContent(2);

                                    break;

                                case "3":
                                    FixTabsContent(3);

                                    break;

                                case "4":
                                    FixTabsContent(4);

                                    break;

                                case "5":
                                    FixTabsContent(5);

                                    break;

                                case "6":
                                    FixTabsContent(6);

                                    break;

                                case "7":
                                case "15":
                                    FixTabsContent(7);

                                    break;
                            }

                            UpdatePanelContent.Update();
                        }

                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "File was deleted successfully", MessageTypes.Success, true, true, true, true, false);
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

                    GlobalMethods.ShowMessageAlertControl(UcMessageAlert, alert, MessageTypes.Error, true, true, true, true, false);

                    GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");
                }
            }
            catch (Exception ex)
            {
                LblFileUploadTitle.Text = "Delete File Warning";
                string content = "File could not be deleted. Please try again later or contact with us.";
                GlobalMethods.ShowMessageControl(UploadMessageAlert, content, MessageTypes.Error, true, true, true, true, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                GetCategoriesNewFilesNotifications("Elio_collaboration_users_library_files");

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
                        NotificationControl ucNotification = (NotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif1");

                        if (hdnPath != null && hdnId != null && hdnIsNew != null)
                            PreviewFile(Convert.ToInt32(hdnIsNew.Value), hdnPath.Value, Convert.ToInt32(hdnId.Value), ucNotification, session);

                        if (hdnIsNew.Value == "1")
                            hdnIsNew.Value = "0";
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
                        NotificationControl ucNotification = (NotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif2");

                        if (hdnPath != null && hdnId != null && hdnIsNew != null)
                            PreviewFile(Convert.ToInt32(hdnIsNew.Value), hdnPath.Value, Convert.ToInt32(hdnId.Value), ucNotification, session);

                        if (hdnIsNew.Value == "1")
                            hdnIsNew.Value = "0";
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
                        NotificationControl ucNotification = (NotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif3");

                        if (hdnPath != null && hdnId != null && hdnIsNew != null)
                            PreviewFile(Convert.ToInt32(hdnIsNew.Value), hdnPath.Value, Convert.ToInt32(hdnId.Value), ucNotification, session);

                        if (hdnIsNew.Value == "1")
                            hdnIsNew.Value = "0";
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
                        NotificationControl ucNotification = (NotificationControl)ControlFinder.FindControlRecursive(item, "UcNotif4");

                        if (hdnPath != null && hdnId != null && hdnIsNew != null)
                            PreviewFile(Convert.ToInt32(hdnIsNew.Value), hdnPath.Value, Convert.ToInt32(hdnId.Value), ucNotification, session);

                        if (hdnIsNew.Value == "1")
                            hdnIsNew.Value = "0";
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

        # endregion

        #region Grids

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
                LoadGridData(Rdg1, 1, UcRgd1);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {

            }
        }

        protected void Rdg2_ItemDataBound(object sender, RepeaterItemEventArgs args)
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

        protected void Rdg2_Load(object sender, EventArgs e)
        {
            try
            {
                LoadGridData(Rdg2, 2, UcRgd2);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {

            }
        }

        protected void Rdg3_ItemDataBound(object sender, RepeaterItemEventArgs args)
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

        protected void Rdg3_Load(object sender, EventArgs e)
        {
            try
            {
                LoadGridData(Rdg3, 3, UcRgd3);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {

            }
        }

        protected void Rdg4_ItemDataBound(object sender, RepeaterItemEventArgs args)
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

        protected void Rdg4_Load(object sender, EventArgs e)
        {
            try
            {
                LoadGridData(Rdg4, 4, UcRgd4);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {

            }
        }

        protected void Rdg5_ItemDataBound(object sender, RepeaterItemEventArgs args)
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

        protected void Rdg5_Load(object sender, EventArgs e)
        {
            try
            {
                LoadGridData(Rdg5, 5, UcRgd5);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {

            }
        }

        protected void Rdg6_ItemDataBound(object sender, RepeaterItemEventArgs args)
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

        protected void Rdg6_Load(object sender, EventArgs e)
        {
            try
            {
                LoadGridData(Rdg6, 6, UcRgd6);
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

        protected void aProductUpdates_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixTabsContent(1);
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

        protected void aTabMarketingMaterial_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixTabsContent(2);
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

        protected void aTabNewCampaign_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixTabsContent(3);
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

        protected void aTabNewsLetter_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixTabsContent(4);
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

        protected void aTabBanner_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixTabsContent(5);
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

        protected void aTabDocumentationPdf_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixTabsContent(6);
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
            //        //GlobalMethods.ShowMessageControl(UploadMessageAlert, LblFileUploadfMsg.Text, MessageTypes.Error, false, true, false);
            //        //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
            //    }

            //    #endregion
            //}
            //catch (Exception ex)
            //{
            //    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            //}
        }

        #endregion
    }
}