using System;
using System.Collections.Generic;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Roles;
using WdS.ElioPlus.Lib.Roles.EnumsRoles;
using System.Linq;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.ImagesHelper;
using System.Configuration;
using System.IO;

namespace WdS.ElioPlus
{
    public partial class DashboardTrainingAddNewPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        #region Page properties

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

        public HtmlInputFile FileToUpload
        {
            get
            {
                //HtmlInputFile result = null;

                if (ViewState["FileToUpload"] != null)
                    return (HtmlInputFile)ViewState["FileToUpload"];
                else
                    return null;
            }
            set
            {
                ViewState["FileToUpload"] = value;
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

        public bool ShowPartner
        {
            get
            {
                if (ViewState["ShowPartner"] != null)
                    return (bool)ViewState["ShowPartner"];
                else
                    return false;
            }
            set
            {
                ViewState["ShowPartner"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

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

                    bool hasRight = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTrainingAddNewPage", Actions.Add, session);
                    if (!hasRight)
                    {
                        Response.Redirect(ControlLoader.PageDash405, false);
                        return;
                    }

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
            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {

            }
            else
            {
                //GetTiersByUser(vSession.User.Id);
                //LoadCountries();
            }

            vSession.Category = null;
            UploadMessageAlert.Visible = UcWizardAlertControl.Visible = false;

            UpdateStrings();
            SetLinks();
            LoadStep(1);

            BtnNext.Visible = DrpCategories.Items != null && DrpCategories.Items.Count > 0 && DrpCategories.SelectedValue != "0";

            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {

            }
            else
            {

            }
        }

        private void SetLinks()
        {

        }

        private void UpdateStrings()
        {

        }

        private void LoadCourses()
        {
            if (TxtCategoryName.Text != "")
            {
                ElioUsersTrainingCategories category = Sql.GetElioUserTrainingCategoryByName(vSession.User.Id, TxtCategoryName.Text, session);

                if (category != null)
                {
                    List<ElioUsersTrainingCategoriesCourses> courses = Sql.GetElioUserTrainingCategoryCourses(vSession.User.Id, category.Id, session);

                    CbxUserCoursesList.Items.Clear();

                    if (courses.Count > 0)
                    {
                        foreach (ElioUsersTrainingCategoriesCourses course in courses)
                        {
                            ListItem item = new ListItem();

                            item.Value = course.Id.ToString();
                            item.Text = course.CourseDescription;

                            item.Selected = true;

                            CbxUserCoursesList.Items.Add(item);
                        }
                    }
                }
            }
            else
                LoadStep(1);
        }

        private void LoadCategories()
        {
            List<ElioUsersTrainingCategories> categories = Sql.GetElioUserTrainingCategories(vSession.User.Id, session);

            if (categories.Count > 0)
            {
                BtnNext.Visible = true;
                divExistingCategories.Visible = true;
                //divNewCategory.Visible = true;

                DrpCategories.Items.Clear();

                ListItem item = new ListItem();
                item.Value = "0";
                item.Text = "Select Category";

                DrpCategories.Items.Add(item);

                foreach (ElioUsersTrainingCategories category in categories)
                {
                    item = new ListItem();

                    item.Value = category.Id.ToString();
                    item.Text = category.CategoryDescription;

                    DrpCategories.Items.Add(item);

                    if (vSession.Category != null)
                    {
                        if (vSession.Category.CategoryId > 0)
                        {
                            item.Selected = (vSession.Category.CategoryId == category.Id);

                            if (item.Selected)
                            {
                                divNewCategory.Visible = false;
                                TxtCategoryName.Text = "";
                            }
                        }
                    }
                }
            }
            else
            {
                BtnNext.Visible = false;
                GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "You must have at least one category to add course.", MessageTypes.Info, true, true, true, false, false);
                divExistingCategories.Visible = false;
                //divNewCategory.Visible = true;
            }
        }

        private List<ElioUsersTrainingCategoriesCourses> LoadCoursesRpt()
        {
            if (TxtCategoryName.Text != "")
            {
                ElioUsersTrainingCategories category = Sql.GetElioUserTrainingCategoryByName(vSession.User.Id, TxtCategoryName.Text, session);

                if (category != null)
                {
                    return Sql.GetElioUserTrainingCategoryCourses(vSession.User.Id, category.Id, session);
                }
                else
                    return null;
            }
            else
                return null;
        }

        private List<ElioUsersTrainingCoursesChapters> LoadChaptersRpt()
        {
            if (TxtCategoryName.Text != "")
            {
                ElioUsersTrainingCategories category = Sql.GetElioUserTrainingCategoryByName(vSession.User.Id, TxtCategoryName.Text, session);

                if (category != null)
                {
                    //if (DrpCourses.SelectedValue != "0")
                    //{
                    //    return Sql.GetElioUserTrainingCourseChapters(vSession.User.Id, category.Id, Convert.ToInt32(DrpCourses.SelectedValue), session);

                    //}
                    //else
                    //{
                        return null;
                    //}
                }
                else
                    return null;
            }
            else
                return null;
        }

        private List<ElioUsersTrainingCoursesChaptersIJCourses> LoadChaptersRptOverview()
        {
            if (TxtCategoryName.Text != "")
            {
                ElioUsersTrainingCategories category = Sql.GetElioUserTrainingCategoryByName(vSession.User.Id, TxtCategoryName.Text, session);

                if (category != null)
                {
                    return Sql.GetElioUserTrainingCategoryChaptersIJCourses(vSession.User.Id, category.Id, session);
                }
                else
                    return null;
            }
            else
                return null;
        }

        private void LoadCoursesDrp(DropDownList drp)
        {
            if (vSession.Category != null && vSession.Category.CategoryName != "")
            {
                drp.Items.Clear();

                ListItem item = new ListItem();
                item.Value = "0";
                item.Text = "Select Course";

                drp.Items.Add(item);

                int i = 1;
                foreach (Course itm in vSession.Category.Courses)
                {
                    item = new ListItem();

                    item.Value = itm.CourseId.ToString();
                    item.Text = itm.CourseDescription;

                    drp.Items.Add(item);

                    //if (vSession.Category.Courses.Count == 1)
                    //{
                    //    if (drp.ID == DrpCoursesPerm.ID)
                    //        if (vSession.Category.Courses.Where(n => n.CourseId == int.Parse(item.Value)).FirstOrDefault() != null && vSession.Category.Courses.Where(n => n.CourseId == int.Parse(item.Value)).FirstOrDefault().Permissions.CourseId == int.Parse(item.Value))
                    //            item.Selected = true;
                    //}

                    i++;
                }

                if (vSession.Category.Courses.Where(n => n.CourseId == int.Parse(drp.SelectedValue)).FirstOrDefault() == null)
                    drp.SelectedValue = "0";
            }
            else
                LoadStep(1);
        }

        private void LoadStep(int step)
        {
            UcWizardAlertControl.Visible = false;

            switch (step)
            {
                case 1:

                    divStep1.Visible = true;
                    divStep1.Attributes["data-wizard-state"] = divMStep1.Attributes["data-wizard-state"] = "current";
                    divStep2.Visible = divStep3.Visible = divStep4.Visible = divStep5.Visible = false;
                    divStep2.Attributes.Remove("data-wizard-state");
                    divStep3.Attributes.Remove("data-wizard-state");
                    divStep4.Attributes.Remove("data-wizard-state");
                    divStep5.Attributes.Remove("data-wizard-state");

                    //divMStep2.Attributes["data-wizard-state"] = divMStep3.Attributes["data-wizard-state"] = divMStep4.Attributes["data-wizard-state"] = divMStep5.Attributes["data-wizard-state"] = "pending";

                    divMStep2.Attributes.Remove("data-wizard-state");
                    divMStep3.Attributes.Remove("data-wizard-state");
                    divMStep4.Attributes.Remove("data-wizard-state");
                    divMStep5.Attributes.Remove("data-wizard-state");

                    divMStep2.Attributes.Add("data-wizard-state", "pending");
                    divMStep3.Attributes.Add("data-wizard-state", "pending");
                    divMStep4.Attributes.Add("data-wizard-state", "pending");
                    divMStep5.Attributes.Add("data-wizard-state", "pending");

                    BtnNext.Visible = true;
                    BtnPrevious.Visible = BtnSave.Visible = BtnClear.Visible = false;

                    LoadCategories();

                    //if (divNewCategory.Visible)
                    //{
                    //    if (vSession.Category != null)
                    //    {
                    //        if (vSession.Category.CategoryName != "")
                    //            TxtCategoryName.Text = vSession.Category.CategoryName;
                    //    }
                    //}

                    break;

                case 2:

                    if (vSession.Category != null)
                    {
                        divStep2.Visible = true;
                        divStep2.Attributes["data-wizard-state"] = divMStep2.Attributes["data-wizard-state"] = "current";
                        divStep1.Visible = divStep3.Visible = divStep4.Visible = divStep5.Visible = false;
                        divStep1.Attributes.Remove("data-wizard-state");
                        divStep3.Attributes.Remove("data-wizard-state");
                        divStep4.Attributes.Remove("data-wizard-state");
                        divStep5.Attributes.Remove("data-wizard-state");

                        //divMStep1.Attributes["data-wizard-state"] = divMStep3.Attributes["data-wizard-state"] = divMStep4.Attributes["data-wizard-state"] = divMStep5.Attributes["data-wizard-state"] = "pending";

                        divMStep1.Attributes.Remove("data-wizard-state");
                        divMStep3.Attributes.Remove("data-wizard-state");
                        divMStep4.Attributes.Remove("data-wizard-state");
                        divMStep5.Attributes.Remove("data-wizard-state");

                        divMStep1.Attributes.Add("data-wizard-state", "pending");
                        divMStep3.Attributes.Add("data-wizard-state", "pending");
                        divMStep4.Attributes.Add("data-wizard-state", "pending");
                        divMStep5.Attributes.Add("data-wizard-state", "pending");

                        BtnPrevious.Visible = BtnNext.Visible = true;
                        BtnSave.Visible = false;

                        if (vSession.Category.Courses.Count > 0)
                        {
                            TxtCourseName.Text = vSession.Category.Courses[0].CourseDescription;
                            TxtCourseOverview.Text = vSession.Category.Courses[0].OverviewText;
                            ImgPhotoBckgr.ImageUrl = "TrainingLibrary/" + vSession.Category.Courses[0].OverviewImagePath;

                            //LoadCourses();                    
                            //LoadRepeater(Navigation.None, LoadCoursesRpt(), RdgCourses);
                            //LoadRptCourse(Navigation.None, RdgCourses);

                        }
                    }
                    else
                        LoadStep(1);

                    break;

                case 3:

                    if (vSession.Category != null)
                    {
                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            //if (DdlTiers.SelectedItem == null || (DdlTiers.SelectedValue != null && DdlTiers.SelectedValue == "0"))
                            //{
                            GetTiersByUser(vSession.User.Id);
                            //}

                            LoadCountries();
                        }

                        divStep3.Visible = true;
                        divStep3.Attributes["data-wizard-state"] = divMStep3.Attributes["data-wizard-state"] = "current";
                        divStep2.Visible = divStep1.Visible = divStep4.Visible = divStep5.Visible = false;
                        divStep2.Attributes.Remove("data-wizard-state");
                        divStep1.Attributes.Remove("data-wizard-state");
                        divStep4.Attributes.Remove("data-wizard-state");
                        divStep5.Attributes.Remove("data-wizard-state");

                        //divMStep1.Attributes["data-wizard-state"] = divMStep2.Attributes["data-wizard-state"] = divMStep4.Attributes["data-wizard-state"] = divMStep5.Attributes["data-wizard-state"] = "pending";

                        divMStep1.Attributes.Remove("data-wizard-state");
                        divMStep2.Attributes.Remove("data-wizard-state");
                        divMStep4.Attributes.Remove("data-wizard-state");
                        divMStep5.Attributes.Remove("data-wizard-state");

                        divMStep1.Attributes.Add("data-wizard-state", "pending");
                        divMStep2.Attributes.Add("data-wizard-state", "pending");
                        divMStep4.Attributes.Add("data-wizard-state", "pending");
                        divMStep5.Attributes.Add("data-wizard-state", "pending");

                        BtnPrevious.Visible = BtnNext.Visible = true;
                        BtnSave.Visible = false;

                        //if (DrpGroupParnters.SelectedItem == null || (DrpGroupParnters.SelectedValue != null && DrpGroupParnters.SelectedValue == "0"))
                        LoadGroups();

                        TxtSelectedCountries.Text = "";

                        if (vSession.Category.Courses.Count > 0)
                        {
                            if (vSession.Category.Courses[0] != null && vSession.Category.Courses[0].CourseId > 0)
                            {
                                DdlTiers.SelectedValue = vSession.Category.Courses[0].Permissions.TierId.ToString();
                                DrpGroupParnters.SelectedValue = vSession.Category.Courses[0].Permissions.TrainingGroupId.ToString();
                                if (vSession.Category.Courses[0].Permissions.Countries.Count > 0)
                                {
                                    TxtSelectedCountries.Text = "";
                                    foreach (string country in vSession.Category.Courses[0].Permissions.Countries)
                                    {
                                        TxtSelectedCountries.Text += country + ",";
                                    }

                                    if (TxtSelectedCountries.Text.EndsWith(","))
                                        TxtSelectedCountries.Text = TxtSelectedCountries.Text.Substring(0, TxtSelectedCountries.Text.Length - 1);
                                }
                            }
                        }

                        aDeleteCountry.Visible = TxtSelectedCountries.Text != "";
                    }
                    else
                        LoadStep(1);

                    break;

                case 4:

                    if (vSession.Category != null)
                    {
                        divStep4.Visible = true;
                        divStep4.Attributes["data-wizard-state"] = divMStep4.Attributes["data-wizard-state"] = "current";
                        divStep2.Visible = divStep3.Visible = divStep1.Visible = divStep5.Visible = false;
                        divStep2.Attributes.Remove("data-wizard-state");
                        divStep3.Attributes.Remove("data-wizard-state");
                        divStep1.Attributes.Remove("data-wizard-state");
                        divStep5.Attributes.Remove("data-wizard-state");

                        divMStep1.Attributes["data-wizard-state"] = divMStep2.Attributes["data-wizard-state"] = divMStep3.Attributes["data-wizard-state"] = divMStep5.Attributes["data-wizard-state"] = "pending";

                        BtnPrevious.Visible = BtnNext.Visible = true;
                        BtnSave.Visible = BtnClear.Visible = false;

                        //LoadCoursesDrp(DrpCourses);
                    }
                    else
                        LoadStep(1);

                    break;

                case 5:

                    if (vSession.Category != null)
                    {
                        divStep5.Visible = true;
                        divStep5.Attributes["data-wizard-state"] = divMStep5.Attributes["data-wizard-state"] = "current";
                        divStep2.Visible = divStep3.Visible = divStep4.Visible = divStep1.Visible = false;
                        divStep2.Attributes.Remove("data-wizard-state");
                        divStep3.Attributes.Remove("data-wizard-state");
                        divStep4.Attributes.Remove("data-wizard-state");
                        divStep1.Attributes.Remove("data-wizard-state");

                        divMStep1.Attributes["data-wizard-state"] = divMStep2.Attributes["data-wizard-state"] = divMStep3.Attributes["data-wizard-state"] = divMStep4.Attributes["data-wizard-state"] = "pending";

                        BtnPrevious.Visible = BtnSave.Visible = BtnClear.Visible = true;
                        BtnNext.Visible = false;

                        LblCategoryName.Text = vSession.Category.CategoryName;

                        LoadRptCourse(Navigation.None, RdgCoursesOverview);
                        LoadRptOverviewChapter(Navigation.None, RdgChapterOverview);
                        LoadRptOverviewPermissions(Navigation.None, RdgPermissionsOverview);
                    }
                    else
                        LoadStep(1);

                    break;
            }

            UpdatePanelContent.Update();
        }

        private bool IsValidToContinueStep(int step)
        {
            bool isOk = true;

            switch (step)
            {
                case 1:
                    {
                        //if (divNewCategory.Visible)
                        //{
                        //    if (TxtCategoryName.Text == "")
                        //    {
                        //        GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "You must type new category description.", MessageTypes.Error, true, true, true, true, false);
                        //        isOk = false;
                        //        break;
                        //    }
                        //}
                        //else 
                        //if (divExistingCategories.Visible)
                        //{

                        if (DrpCategories.SelectedValue == "0")
                        {
                            GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "You must select category.", MessageTypes.Error, true, true, true, true, false);
                            isOk = false;
                            break;
                        }
                        //}

                        if (vSession.Category == null)
                            vSession.Category = new TrainingCategory();

                        vSession.Category.CategoryId = (DrpCategories.SelectedValue == "0" || DrpCategories.SelectedValue == "") ? Sql.GetTrainingCategoryLastId(session) + 1 : Convert.ToInt32(DrpCategories.SelectedValue);
                        vSession.Category.CategoryName = (DrpCategories.SelectedValue == "0" || DrpCategories.SelectedValue == "") ? TxtCategoryName.Text : DrpCategories.SelectedItem.Text;

                        //List<ElioUsersTrainingCategoriesCourses> courses = Sql.GetElioUserTrainingCategoryCourses(vSession.User.Id, vSession.Category.CategoryId, session);

                        //vSession.Category.Courses.Clear();

                        //foreach (ElioUsersTrainingCategoriesCourses course in courses)
                        //{
                        //    Course c = new Course();
                        //    c.CourseDescription = course.CourseDescription;
                        //    c.CourseId = course.Id;
                        //    c.OverviewText = course.OverviewText;
                        //    c.OverviewImagePath = course.OverviewImagePath;
                        //    c.OverviewImageName = course.OverviewImageName;

                        //    vSession.Category.Courses.Add(c);

                        //    ElioUsersTrainingCategoryCoursePermissions permission = Sql.GetElioUserTrainingCoursePermissionsByCourseId(course.UserId, course.Id, session);

                        //    if (permission != null)
                        //    {
                        //        vSession.Category.Courses.Where(n => n.CourseId == course.Id).FirstOrDefault().Permissions = new Permissions();

                        //        vSession.Category.Courses.Where(n => n.CourseId == course.Id).FirstOrDefault().Permissions.CourseId = permission.CourseId;
                        //        vSession.Category.Courses.Where(n => n.CourseId == course.Id).FirstOrDefault().Permissions.TierId = permission.TierId;
                        //        vSession.Category.Courses.Where(n => n.CourseId == course.Id).FirstOrDefault().Permissions.TrainingGroupId = permission.TrainingGroupId;
                        //        vSession.Category.Courses.Where(n => n.CourseId == course.Id).FirstOrDefault().Permissions.Countries = permission.Countries.Split(',').ToList();
                        //    }

                        //    List<ElioUsersTrainingCoursesChapters> chapters = Sql.GetElioUserTrainingCourseChapters(course.UserId, course.CategoryId, course.Id, session);

                        //    vSession.Category.Courses.Where(n => n.CourseId == c.CourseId).FirstOrDefault().Chapters.Clear();

                        //    foreach (ElioUsersTrainingCoursesChapters chapter in chapters)
                        //    {
                        //        Chapter chpt = new Chapter();

                        //        chpt.CourseId = c.CourseId;
                        //        chpt.ChapterId = chapter.Id;
                        //        chpt.ChapterTitle = chapter.ChapterTitle;
                        //        chpt.ChapterText = chapter.ChapterText;
                        //        chpt.ChapterLink = chapter.ChapterLink;
                        //        chpt.ChapterFilePath = chapter.ChapterFilePath;
                        //        chpt.ChapterFileName = chapter.ChapterFileName;

                        //        vSession.Category.Courses.Where(n => n.CourseId == c.CourseId).FirstOrDefault().Chapters.Add(chpt);
                        //    }
                        //}
                    }

                    break;

                case 2:
                    {
                        if (vSession.Category.CategoryName != "")
                        {
                            string overviewImageName = "";
                            string overviewImageType = "";

                            if (vSession.Category.Courses.Count > 1)
                            {
                                vSession.Category.Courses.Clear();
                            }

                            if (vSession.Category.Courses.Count == 0)
                            {
                                Course c = new Course();
                                c.CourseId = Sql.GetTrainingCourseLastId(session) + 1;
                                c.CourseDescription = TxtCourseName.Text;
                                c.OverviewText = TxtCourseOverview.Text;
                                c.OverviewImagePath = UploadCoursePreviewFile(out overviewImageName, out overviewImageType);

                                if (c.OverviewImagePath == "" || overviewImageName == "" || overviewImageType == "")
                                {
                                    GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Course file did not uploaded.Please try again!", MessageTypes.Error, true, true, true, true, false);
                                    isOk = false;

                                    return isOk;
                                }

                                c.OverviewImageName = overviewImageName;
                                c.OverviewImageType = overviewImageType;

                                //if (vSession.Category.Courses.Count > 0 && vSession.Category.Courses[0].CourseDescription != TxtCourseName.Text)
                                //    vSession.Category.Courses.Clear();

                                vSession.Category.Courses.Add(c);
                            }
                            else
                            {
                                vSession.Category.Courses[0].CourseDescription = TxtCourseName.Text;
                                vSession.Category.Courses[0].OverviewText = TxtCourseOverview.Text;

                                string overviewImagePath = UploadCoursePreviewFile(out overviewImageName, out overviewImageType);

                                if (overviewImagePath != "" && overviewImageName != "" && overviewImageType != "")
                                {
                                    vSession.Category.Courses[0].OverviewImagePath = overviewImagePath;
                                    vSession.Category.Courses[0].OverviewImageName = overviewImageName;
                                    vSession.Category.Courses[0].OverviewImageType = overviewImageType;
                                }
                            }

                            if (vSession.Category.Courses.Count > 0 && (vSession.Category.Courses[0].OverviewImagePath == "" || vSession.Category.Courses[0].OverviewImageName == "" || vSession.Category.Courses[0].OverviewImageType == ""))
                            {
                                vSession.Category.Courses.Clear();

                                TxtCourseName.Text = TxtCourseOverview.Text = "";
                                ImgPhotoBckgr.ImageUrl = "~/images/no_files_found.png";

                                GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Course file did not uploaded. Please try again!", MessageTypes.Error, true, true, true, true, false);
                                isOk = false;

                                return isOk;
                            }

                            //LoadRptCourse(Navigation.None, RdgCourses);

                            //TxtCourseName.Text = "";
                            //TxtCourseOverview.Text = "";
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Please type course name!", MessageTypes.Error, true, true, true, true, false);
                            isOk = false;
                        }

                        //if (vSession.Category.CategoryName != "")
                        //{
                        //    if (vSession.Category.Courses.Count == 0)
                        //    {
                        //        GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "You must add at least one course to category.", MessageTypes.Error, true, true, true, true, false);
                        //        isOk = false;
                        //    }
                        //}
                    }

                    break;

                case 3:

                    if (DdlTiers.SelectedValue == "0" && (DrpGroupParnters == null || (DrpGroupParnters != null && (DrpGroupParnters.SelectedValue == "0" || DrpGroupParnters.SelectedValue == ""))) && TxtSelectedCountries.Text == "")
                    {
                        //if (vSession.Category.Courses.Where(n => n.CourseId == int.Parse(DrpCoursesPerm.SelectedValue)).FirstOrDefault() != null)
                        //{

                        //}

                        //GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "You must select at least one permission for your training!", MessageTypes.Error, true, true, true, true, false);
                        //isOk = false;
                    }

                    if (vSession.Category.Courses[0].CourseId > 0)
                    {
                        if (DdlTiers.SelectedValue != "0")
                        {
                            vSession.Category.Courses[0].Permissions.TierId = int.Parse(DdlTiers.SelectedValue);
                            vSession.Category.Courses[0].Permissions.TierName = DdlTiers.SelectedItem.Text;
                        }
                        else
                        {
                            vSession.Category.Courses[0].Permissions.TierId = 0;
                            vSession.Category.Courses[0].Permissions.TierName = "";
                        }

                        if (DrpGroupParnters.SelectedValue != "0")
                        {
                            vSession.Category.Courses[0].Permissions.TrainingGroupId = int.Parse(DrpGroupParnters.SelectedValue);
                            vSession.Category.Courses[0].Permissions.TrainingGroupName = DrpGroupParnters.SelectedItem.Text;
                        }
                        else
                        {
                            vSession.Category.Courses[0].Permissions.TrainingGroupId = 0;
                            vSession.Category.Courses[0].Permissions.TrainingGroupName = "";
                        }

                        if (TxtSelectedCountries.Text != "")
                        {
                            vSession.Category.Courses[0].Permissions.Countries = TxtSelectedCountries.Text.TrimEnd(',').Split(',').ToList();
                        }
                        else
                            vSession.Category.Courses[0].Permissions.Countries.Clear();

                        aDeleteCountry.Visible = TxtSelectedCountries.Text != "";
                    }

                    break;

                case 4:

                    if (vSession.Category.CategoryName != "")
                    {
                        foreach (Course item in vSession.Category.Courses)
                        {
                            if (item.Chapters.Count == 0)
                            {
                                GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "You must add at least one chapter for each training course!", MessageTypes.Error, true, true, true, true, false);
                                isOk = false;
                                break;
                            }
                        }
                    }

                    break;

                case 5:


                    break;
            }

            return isOk;
        }

        private void GetTiersByUser(int vendorId)
        {
            try
            {
                List<ElioTierManagementUsersSettings> tiers = Sql.GetTierManagementUserSettings(vendorId, session);

                if (tiers.Count > 0)
                {
                    DdlTiers.Items.Clear();

                    ListItem item = new ListItem();
                    item.Value = "0";
                    item.Text = "All Tiers";

                    DdlTiers.Items.Add(item);

                    foreach (ElioTierManagementUsersSettings tier in tiers)
                    {
                        item = new ListItem();
                        item.Value = tier.Id.ToString();
                        item.Text = tier.Description;

                        DdlTiers.Items.Add(item);

                        if (vSession.Category != null && vSession.Category.Courses[0].CourseId > 0)
                        {
                            if (vSession.Category.Courses[0].Permissions.TierId == int.Parse(item.Value))
                                item.Selected = true;
                        }
                        else
                            LoadStep(1);
                    }

                    if (vSession.Category.Courses[0] == null || (vSession.Category.Courses[0] != null && vSession.Category.Courses[0].CourseId == 0))
                        DrpGroupParnters.SelectedValue = "0";
                }
                else
                {
                    DdlTiers.Visible = false;
                    spanAlert.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void LoadCountries()
        {
            DrpCountries.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "All Countries";

            DrpCountries.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new ListItem();

                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                DrpCountries.Items.Add(item);
            }

            DrpCountries.SelectedValue = "0";
        }

        protected void LoadRptOverviewChapter(Navigation navigation, Repeater rptOvrv)
        {
            try
            {
                PagedDataSource objPds = new PagedDataSource();

                List<Course> courses = vSession.Category.Courses.ToList();
                List<ChapterOvrv> chapters = new List<ChapterOvrv>();

                foreach (Course item in courses)
                {
                    foreach (Chapter chapter in item.Chapters)
                    {
                        ChapterOvrv chapterOvrv = new ChapterOvrv();

                        chapterOvrv.ChapterId = chapter.ChapterId;
                        chapterOvrv.CourseId = chapter.CourseId;
                        chapterOvrv.ChapterLink = chapter.ChapterLink;
                        chapterOvrv.ChapterTitle = chapter.ChapterTitle;
                        chapterOvrv.ChapterText = chapter.ChapterText;
                        chapterOvrv.ChapterFilePath = chapter.ChapterFilePath;
                        chapterOvrv.ChapterFileName = chapter.ChapterFileName;
                        chapterOvrv.CourseDescription = item.CourseDescription;

                        chapters.Add(chapterOvrv);
                    }
                }

                if (chapters.Count > 0)
                {
                    objPds.DataSource = chapters;

                    objPds.AllowPaging = true;

                    objPds.PageSize = 10;

                    switch (navigation)
                    {
                        case Navigation.First:
                            NowViewing = 1;
                            break;

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

                    objPds.CurrentPageIndex = NowViewing;
                    rptOvrv.DataSource = objPds;
                    rptOvrv.DataBind();
                }
                else
                {
                    rptOvrv.DataSource = null;
                    rptOvrv.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void LoadRptOverviewPermissions(Navigation navigation, Repeater rptOvrv)
        {
            try
            {
                if (vSession.Category.Courses != null && vSession.Category.Courses.Count > 0)
                {
                    PagedDataSource objPds = new PagedDataSource();

                    List<Course> courses = vSession.Category.Courses.ToList();
                    List<PermissionsOvrv> perm = new List<PermissionsOvrv>();

                    foreach (Course item in courses)
                    {
                        PermissionsOvrv permOvrv = new PermissionsOvrv();

                        permOvrv.CourseId = item.CourseId;
                        permOvrv.CourseDescription = item.CourseDescription;
                        permOvrv.TierName = (item.Permissions.TierId > 0) ? Sql.GetTierDescriptionById(vSession.User.Id, item.Permissions.TierId, session) : "All Tiers";
                        permOvrv.TierId = item.Permissions.TierId;
                        permOvrv.GroupId = item.Permissions.TrainingGroupId;
                        permOvrv.GroupName = (item.Permissions.TrainingGroupId > 0) ? Sql.GetTrainingGroupNameById(vSession.User.Id, item.Permissions.TrainingGroupId, session) : "All Partners";

                        if (item.Permissions.Countries.Count > 0)
                        {
                            foreach (string country in item.Permissions.Countries)
                            {
                                if (country != "")
                                    permOvrv.Countries += country + ",";
                            }

                            if (!string.IsNullOrEmpty(permOvrv.Countries) && permOvrv.Countries.EndsWith(","))
                                permOvrv.Countries = permOvrv.Countries.Substring(0, permOvrv.Countries.Length - 1);
                            else
                                permOvrv.Countries = "";

                            permOvrv.Countries = permOvrv.Countries != "" ? permOvrv.Countries : "All Countries";
                        }
                        else
                            permOvrv.Countries = "All Countries";

                        perm.Add(permOvrv);
                    }

                    if (perm.Count > 0)
                    {
                        objPds.DataSource = perm;

                        objPds.AllowPaging = true;

                        objPds.PageSize = 10;

                        switch (navigation)
                        {
                            case Navigation.First:
                                NowViewing = 1;
                                break;

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

                        objPds.CurrentPageIndex = NowViewing;
                        rptOvrv.DataSource = objPds;
                        rptOvrv.DataBind();
                    }
                    else
                    {
                        rptOvrv.DataSource = null;
                        rptOvrv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void LoadRepeater(Navigation navigation, List<ElioUsersTrainingCategoriesCourses> results, Repeater rpt)
        {
            try
            {
                //Create the object of PagedDataSource
                PagedDataSource objPds = new PagedDataSource();

                //Assign our data source to PagedDataSource object

                if (results != null && results.Count > 0)
                {
                    objPds.DataSource = results;

                    //Set the allow paging to true
                    objPds.AllowPaging = true;

                    //Set the number of items you want to show
                    objPds.PageSize = 30;

                    //Based on navigation manage the NowViewing
                    switch (navigation)
                    {
                        case Navigation.First:
                            NowViewing = 1;
                            break;

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
                    //lbtnPrev.Visible = lbtnPrevBottom.Visible = !objPds.IsFirstPage;
                    //lbtnNext.Visible = lbtnNextBottom.Visible = !objPds.IsLastPage;

                    //Assign PagedDataSource to repeater
                    rpt.DataSource = objPds;
                    rpt.DataBind();

                    //ShowMessage1(UcSearchConnectionsMessageAlert, MessageTypes.Success, users.Count + " results from your search!");
                }
                else
                {
                    rpt.DataSource = null;
                    rpt.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void LoadRepeaterChapter(Navigation navigation, List<ElioUsersTrainingCoursesChapters> results, Repeater rpt)
        {
            try
            {
                //Create the object of PagedDataSource
                PagedDataSource objPds = new PagedDataSource();

                //Assign our data source to PagedDataSource object

                if (results != null && results.Count > 0)
                {
                    objPds.DataSource = results;

                    //Set the allow paging to true
                    objPds.AllowPaging = true;

                    //Set the number of items you want to show
                    objPds.PageSize = 10;

                    //Based on navigation manage the NowViewing
                    switch (navigation)
                    {
                        case Navigation.First:
                            NowViewing = 1;
                            break;

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

                    objPds.CurrentPageIndex = NowViewing;

                    rpt.DataSource = objPds;
                    rpt.DataBind();
                }
                else
                {
                    rpt.DataSource = null;
                    rpt.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void LoadRepeaterChapterOverview(Navigation navigation, List<ElioUsersTrainingCoursesChaptersIJCourses> results, Repeater rpt)
        {
            try
            {
                //Create the object of PagedDataSource
                PagedDataSource objPds = new PagedDataSource();

                //Assign our data source to PagedDataSource object

                if (results != null && results.Count > 0)
                {
                    objPds.DataSource = results;

                    //Set the allow paging to true
                    objPds.AllowPaging = true;

                    //Set the number of items you want to show
                    objPds.PageSize = 10;

                    //Based on navigation manage the NowViewing
                    switch (navigation)
                    {
                        case Navigation.First:
                            NowViewing = 1;
                            break;

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

                    objPds.CurrentPageIndex = NowViewing;

                    rpt.DataSource = objPds;
                    rpt.DataBind();
                }
                else
                {
                    rpt.DataSource = null;
                    rpt.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected string UploadCoursePreviewFile(out string fileName, out string fileType)
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["TrainingLibraryTargetFolder"].ToString()))
            {
                string logoTargetFolder = ConfigurationManager.AppSettings["TrainingLibraryTargetFolder"].ToString();
                string serverMapPathTargetFolder = Server.MapPath(logoTargetFolder);

                int maxSize = 50000;
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxPreviewFileLenghtTraining"].ToString()))
                    maxSize = int.Parse(ConfigurationManager.AppSettings["MaxPreviewFileLenghtTraining"]);

                var logo = CoursePreview.PostedFile;
                var logoSize = logo.ContentLength;

                if (logo != null && logo.ContentLength > 0)
                {
                    //if (logo.ContentType == "image/png" || logo.ContentType == "image/jpeg" || logo.ContentType == "image/jpg")
                    //{
                    //    if (logoSize < maxSize)
                    //    {
                    string fileExtension = UpLoadImage.GetFilenameExtension(logo.FileName);

                    serverMapPathTargetFolder = serverMapPathTargetFolder + vSession.User.GuId + "\\" + vSession.Category.CategoryName + "\\" + TxtCourseName.Text + "\\";

                    #region Create File Directory

                    if (!Directory.Exists(serverMapPathTargetFolder))
                        Directory.CreateDirectory(serverMapPathTargetFolder);

                    #endregion

                    #region Delete old files in directory if exist

                    DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                    foreach (FileInfo fi in originaldir.GetFiles())
                    {
                        fi.Delete();
                    }

                    #endregion

                    logo.SaveAs(serverMapPathTargetFolder + logo.FileName);

                    fileName = logo.FileName;
                    fileType = logo.ContentType;

                    return vSession.User.GuId + "/" + vSession.Category.CategoryName + "/" + TxtCourseName.Text + "/" + logo.FileName;
                    //    }
                    //    else
                    //    {
                    //        fileName = "";
                    //        fileType = "";

                    //        GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Course preview file is too large. Max size is " + maxSize + " kb", MessageTypes.Error, true, true, true, true, false);
                    //        return "";
                    //    }
                    //}
                    //else
                    //{
                    //    fileName = "";
                    //    fileType = "";

                    //    GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Course preview file is not supported. Image type must be .png, .jpeg or .jpg", MessageTypes.Error, true, true, true, true, false);
                    //    return "";
                    //}
                }
                else
                {
                    fileName = "";
                    fileType = "";

                    GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "You have not selected course preview file or something went wrong. Try again or contact us.", MessageTypes.Error, true, true, true, true, false);
                    return "";
                }
            }
            else
            {

                fileName = "";
                fileType = "";

                Logger.DetailedError(Request.Url.ToString(), string.Format("User {0} tried to upload logo at {1}, but file could not be uploaded because 'AppSettings --> LogoTargetFolder' is missing from config.", vSession.User.Id.ToString(), DateTime.Now.ToString()), "PAGE --> DashboardEditProfile.aspx");
                GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Something went wrong. Please try again later or contact with us.", MessageTypes.Error, true, true, true, true, false);
                return "";
            }
        }

        protected string UploadChapterFile()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["TrainingLibraryTargetFolder"].ToString()))
            {
                string logoTargetFolder = ConfigurationManager.AppSettings["TrainingLibraryTargetFolder"].ToString();
                string serverMapPathTargetFolder = Server.MapPath(logoTargetFolder);

                int maxSize = 500000;
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxFileLenghtTraining"].ToString()))
                    maxSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileLenghtTraining"]);

                var logo = ChapterFile.PostedFile;
                var logoSize = logo.ContentLength;

                if (logo != null && logo.ContentLength > 0)
                {
                    //if (logo.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || logo.ContentType == "application/x-msexcel" || logo.ContentType == "text/csv" || logo.ContentType == "application/pdf" || logo.ContentType == "application/csv" || logo.ContentType == "application/text" || logo.ContentType == "application/vnd.ms-excel")
                    //{
                    //    if (logoSize < maxSize)
                    //    {
                    string fileExtension = UpLoadImage.GetFilenameExtension(logo.FileName);
                    //string formattedName = ImageResize.ChangeFileName(logoName, fileExtension);

                    serverMapPathTargetFolder = serverMapPathTargetFolder + vSession.User.GuId + "\\" + vSession.Category.CategoryName + "\\" + vSession.Category.Courses[0].CourseDescription + "\\" + TxtChaTitle.Text + "\\";

                    #region Create File Directory

                    if (!Directory.Exists(serverMapPathTargetFolder))
                        Directory.CreateDirectory(serverMapPathTargetFolder);

                    #endregion

                    #region Delete old files in directory if exist

                    DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                    foreach (FileInfo fi in originaldir.GetFiles())
                    {
                        fi.Delete();
                    }

                    #endregion

                    logo.SaveAs(serverMapPathTargetFolder + logo.FileName);

                    return vSession.User.GuId + "/" + vSession.Category.CategoryName + "/" + vSession.Category.Courses[0].CourseDescription + "/" + TxtChaTitle.Text + "/" + logo.FileName;
                    //}
                    //else
                    //{
                    //    GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Chapter file could not be uploaded because size is too large. Max size is " + maxSize + " kb.", MessageTypes.Error, true, true, true, true, false);
                    //    return "";
                    //}
                    //}
                    //else
                    //{
                    //    GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Chapter file could not be uploaded because content type is not supported.", MessageTypes.Error, true, true, true, true, false);
                    //    return "";
                    //}
                }
                else
                {
                    //GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "You have not selected chapter file or something went wrong. Try again or contact us.", MessageTypes.Error, true, true, true, true, false);
                    return "";
                }
            }
            else
            {
                Logger.DetailedError(Request.Url.ToString(), string.Format("User {0} tried to upload logo at {1}, but file could not be uploaded because 'AppSettings --> LogoTargetFolder' is missing from config.", vSession.User.Id.ToString(), DateTime.Now.ToString()), "PAGE --> DashboardEditProfile.aspx");
                GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Something went wrong. Please try again later or contact with us.", MessageTypes.Error, true, true, true, true, false);
                return "Error";
            }
        }

        #endregion

        #region Buttons

        protected void BtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (divStep1.Visible)
                    {
                        if (IsValidToContinueStep(1))
                            LoadStep(2);
                    }
                    else if (divStep2.Visible)
                    {
                        if (IsValidToContinueStep(2))
                            LoadStep(3);
                    }
                    else if (divStep3.Visible)
                    {
                        if (IsValidToContinueStep(3))
                            LoadStep(4);
                    }
                    else if (divStep4.Visible)
                    {
                        if (IsValidToContinueStep(4))
                            LoadStep(5);
                    }
                    else
                        BtnNext.Visible = false;
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (BtnSave.Text == "Submit")
                    {
                        if (vSession.Category != null)
                        {
                            if (vSession.Category.CategoryName != "")
                            {
                                session.BeginTransaction();

                                DataLoader<ElioUsersTrainingCategories> loader = new DataLoader<ElioUsersTrainingCategories>(session);

                                ElioUsersTrainingCategories category = Sql.GetElioUserTrainingCategoryById(vSession.User.Id, vSession.Category.CategoryId, session);
                                if (category == null)
                                {
                                    category = new ElioUsersTrainingCategories();

                                    category.UserId = vSession.User.Id;
                                    category.CategoryDescription = vSession.Category.CategoryName;
                                    category.IsPublic = 1;
                                    category.SysDate = DateTime.Now;
                                    category.LastUpDated = DateTime.Now;

                                    loader.Insert(category);

                                    foreach (Course crs in vSession.Category.Courses)
                                    {
                                        ElioUsersTrainingCategoriesCourses course = new ElioUsersTrainingCategoriesCourses();

                                        course.UserId = vSession.User.Id;
                                        course.CategoryId = category.Id;
                                        course.CourseDescription = crs.CourseDescription;
                                        course.OverviewText = crs.OverviewText;
                                        course.OverviewImagePath = crs.OverviewImagePath;
                                        course.OverviewImageType = crs.OverviewImageType;
                                        course.OverviewImageName = crs.OverviewImageName;
                                        course.IsPublic = 1;
                                        course.IsNew = 1;
                                        course.SysDate = DateTime.Now;
                                        course.LastUpDated = DateTime.Now;

                                        DataLoader<ElioUsersTrainingCategoriesCourses> loaderCo = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);
                                        loaderCo.Insert(course);

                                        crs.CourseId = course.Id;

                                        ElioUsersTrainingCategoryCoursePermissions perm = new ElioUsersTrainingCategoryCoursePermissions();

                                        perm.UserId = vSession.User.Id;
                                        perm.CategoryId = category.Id;
                                        perm.CourseId = crs.CourseId;
                                        perm.TrainingGroupId = crs.Permissions.TrainingGroupId;
                                        perm.TierId = crs.Permissions.TierId;

                                        if (crs.Permissions.Countries.Count > 0)
                                        {
                                            foreach (string country in crs.Permissions.Countries)
                                            {
                                                if (country != "")
                                                    perm.Countries += crs.Permissions.Countries + ",";
                                            }

                                            if (perm.Countries.EndsWith(","))
                                                perm.Countries = perm.Countries.Substring(0, perm.Countries.Length - 1);
                                        }
                                        else
                                            perm.Countries = "";

                                        perm.Sysdate = DateTime.Now;
                                        perm.LastUpdate = DateTime.Now;
                                        perm.IsPublic = 1;

                                        DataLoader<ElioUsersTrainingCategoryCoursePermissions> loaderPerm = new DataLoader<ElioUsersTrainingCategoryCoursePermissions>(session);
                                        loaderPerm.Insert(perm);

                                        foreach (Chapter chapter in crs.Chapters)
                                        {
                                            ElioUsersTrainingCoursesChapters chpt = new ElioUsersTrainingCoursesChapters();

                                            chpt.UserId = vSession.User.Id;
                                            chpt.CategoryId = category.Id;
                                            chpt.CourserId = course.Id;
                                            chpt.ChapterTitle = chapter.ChapterTitle;
                                            chpt.ChapterText = chapter.ChapterText;
                                            chpt.ChapterLink = chapter.ChapterLink;
                                            chpt.ChapterFilePath = chapter.ChapterFilePath;
                                            chpt.ChapterFileName = chapter.ChapterFileName;
                                            chpt.IsPublic = 1;
                                            chpt.IsViewed = 0;
                                            chpt.DateViewed = null;
                                            chpt.SysDate = DateTime.Now;
                                            chpt.LastUpDated = DateTime.Now;

                                            DataLoader<ElioUsersTrainingCoursesChapters> loaderCh = new DataLoader<ElioUsersTrainingCoursesChapters>(session);
                                            loaderCh.Insert(chpt);

                                            chapter.ChapterId = chpt.Id;
                                        }

                                        if (perm.TrainingGroupId == 0 && perm.TierId == 0 && perm.Countries == "")
                                        {
                                            #region All Vendor Partners

                                            List<ElioUsers> partners = SqlCollaboration.GetCollaborationPartnersByVendorId(vSession.User.Id, session);
                                            if (partners.Count > 0)
                                            {
                                                foreach (ElioUsers partner in partners)
                                                {
                                                    bool exist = Sql.HasPartnerTraningCourse(partner.Id, course.Id, session);
                                                    if (!exist)
                                                    {
                                                        ElioPartnersTrainingCourses pCourse = new ElioPartnersTrainingCourses();

                                                        pCourse.CategoryId = category.Id;
                                                        pCourse.VendorId = vSession.User.Id;
                                                        pCourse.PartnerId = partner.Id;
                                                        pCourse.CourseId = course.Id;
                                                        pCourse.IsPublic = 1;
                                                        pCourse.IsNew = 1;
                                                        pCourse.Sysdate = DateTime.Now;

                                                        DataLoader<ElioPartnersTrainingCourses> loaderP = new DataLoader<ElioPartnersTrainingCourses>(session);
                                                        loaderP.Insert(pCourse);
                                                    }

                                                    foreach (Chapter chapter in crs.Chapters)
                                                    {
                                                        ElioPartnersTrainingChapters pChapter = new ElioPartnersTrainingChapters();

                                                        pChapter.VendorId = vSession.User.Id;
                                                        pChapter.PartnerId = partner.Id;
                                                        pChapter.CourseId = course.Id;
                                                        pChapter.ChapterId = chapter.ChapterId;
                                                        pChapter.IsPublic = 1;
                                                        pChapter.IsViewed = 0;
                                                        pChapter.Sysdate = DateTime.Now;

                                                        DataLoader<ElioPartnersTrainingChapters> loaderPC = new DataLoader<ElioPartnersTrainingChapters>(session);
                                                        loaderPC.Insert(pChapter);
                                                    }
                                                }
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            List<int> partnerIds = new List<int>();

                                            if (perm.TrainingGroupId > 0)
                                            {
                                                List<ElioUsersTrainingGroupMembers> members = Sql.GetElioUserTrainingGroupMembersByGroupId(perm.TrainingGroupId, session);

                                                foreach (ElioUsersTrainingGroupMembers member in members)
                                                {
                                                    partnerIds.Add(member.ResellerId);
                                                }
                                            }

                                            if (perm.TierId > 0)
                                            {
                                                List<ElioUsers> partners = SqlCollaboration.GetCollaborationPartnersByVendorIdAndTierId(vSession.User.Id, perm.TierId, session);
                                                foreach (ElioUsers p in partners)
                                                {
                                                    bool exist = false;

                                                    if (partnerIds.Count > 0)
                                                    {
                                                        foreach (int item in partnerIds)
                                                        {
                                                            if (item == p.Id)
                                                            {
                                                                exist = true;
                                                                break;
                                                            }
                                                        }
                                                    }

                                                    if (!exist)
                                                        partnerIds.Add(p.Id);
                                                }
                                            }

                                            if (perm.Countries != "")
                                            {
                                                string countries = "'" + perm.Countries.Replace(",", "','") + "'";

                                                List<ElioUsers> countryPartners = SqlCollaboration.GetCollaborationPartnersByVendorIdAndCountries(vSession.User.Id, countries, session);
                                                foreach (ElioUsers cp in countryPartners)
                                                {
                                                    bool exist = false;

                                                    if (partnerIds.Count > 0)
                                                    {
                                                        foreach (int item in partnerIds)
                                                        {
                                                            if (item == cp.Id)
                                                            {
                                                                exist = true;
                                                                break;
                                                            }
                                                        }
                                                    }

                                                    if (!exist)
                                                        partnerIds.Add(cp.Id);
                                                }
                                            }

                                            if (partnerIds.Count > 0)
                                            {
                                                foreach (int partnerId in partnerIds)
                                                {
                                                    bool exist = Sql.HasPartnerTraningCourse(partnerId, course.Id, session);
                                                    if (!exist)
                                                    {
                                                        ElioPartnersTrainingCourses pCourse = new ElioPartnersTrainingCourses();

                                                        pCourse.CategoryId = category.Id;
                                                        pCourse.VendorId = vSession.User.Id;
                                                        pCourse.PartnerId = partnerId;
                                                        pCourse.CourseId = course.Id;
                                                        pCourse.IsPublic = 1;
                                                        pCourse.IsNew = 1;
                                                        pCourse.Sysdate = DateTime.Now;

                                                        DataLoader<ElioPartnersTrainingCourses> loaderP = new DataLoader<ElioPartnersTrainingCourses>(session);
                                                        loaderP.Insert(pCourse);
                                                    }

                                                    foreach (Chapter chapter in crs.Chapters)
                                                    {
                                                        ElioPartnersTrainingChapters pChapter = new ElioPartnersTrainingChapters();

                                                        pChapter.VendorId = vSession.User.Id;
                                                        pChapter.PartnerId = partnerId;
                                                        pChapter.CourseId = course.Id;
                                                        pChapter.ChapterId = chapter.ChapterId;
                                                        pChapter.IsPublic = 1;
                                                        pChapter.IsViewed = 0;
                                                        pChapter.Sysdate = DateTime.Now;

                                                        DataLoader<ElioPartnersTrainingChapters> loaderPC = new DataLoader<ElioPartnersTrainingChapters>(session);
                                                        loaderPC.Insert(pChapter);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    category.LastUpDated = DateTime.Now;

                                    loader.Update(category);

                                    List<ElioUsersTrainingCategoriesCourses> courses = Sql.GetElioUserTrainingCategoryCourses(vSession.User.Id, category.Id, session);

                                    foreach (Course newCrs in vSession.Category.Courses)
                                    {
                                        bool exist = false;
                                        foreach (ElioUsersTrainingCategoriesCourses crs in courses)
                                        {
                                            if (newCrs.CourseId == crs.Id)
                                            {
                                                exist = true;
                                                break;
                                            }
                                        }

                                        if (!exist)
                                        {
                                            ElioUsersTrainingCategoriesCourses course = new ElioUsersTrainingCategoriesCourses();

                                            course.UserId = vSession.User.Id;
                                            course.CategoryId = category.Id;
                                            course.CourseDescription = newCrs.CourseDescription;
                                            course.OverviewText = newCrs.OverviewText;
                                            course.OverviewImagePath = newCrs.OverviewImagePath;
                                            course.OverviewImageType = newCrs.OverviewImageType;
                                            course.OverviewImageName = newCrs.OverviewImageName;
                                            course.IsPublic = 1;
                                            course.IsNew = 1;
                                            course.SysDate = DateTime.Now;
                                            course.LastUpDated = DateTime.Now;

                                            DataLoader<ElioUsersTrainingCategoriesCourses> loaderCo = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);
                                            loaderCo.Insert(course);

                                            newCrs.CourseId = course.Id;

                                            ElioUsersTrainingCategoryCoursePermissions perm = new ElioUsersTrainingCategoryCoursePermissions();

                                            perm.UserId = vSession.User.Id;
                                            perm.CategoryId = category.Id;
                                            perm.CourseId = newCrs.CourseId;
                                            perm.TrainingGroupId = newCrs.Permissions.TrainingGroupId;
                                            perm.TierId = newCrs.Permissions.TierId;

                                            if (newCrs.Permissions.Countries.Count > 0)
                                            {
                                                perm.Countries = "";
                                                foreach (string country in newCrs.Permissions.Countries)
                                                {
                                                    if (country != "")
                                                        perm.Countries += country + ",";
                                                }

                                                if (perm.Countries.EndsWith(","))
                                                    perm.Countries = perm.Countries.Substring(0, perm.Countries.Length - 1);
                                            }
                                            else
                                                perm.Countries = "";

                                            perm.Sysdate = DateTime.Now;
                                            perm.LastUpdate = DateTime.Now;
                                            perm.IsPublic = 1;

                                            DataLoader<ElioUsersTrainingCategoryCoursePermissions> loaderPerm = new DataLoader<ElioUsersTrainingCategoryCoursePermissions>(session);
                                            loaderPerm.Insert(perm);

                                            foreach (Chapter newChpt in newCrs.Chapters)
                                            {
                                                ElioUsersTrainingCoursesChapters chpt = new ElioUsersTrainingCoursesChapters();

                                                chpt.UserId = vSession.User.Id;
                                                chpt.CategoryId = category.Id;
                                                chpt.CourserId = course.Id;
                                                chpt.ChapterTitle = newChpt.ChapterTitle;
                                                chpt.ChapterText = newChpt.ChapterText;
                                                chpt.ChapterLink = newChpt.ChapterLink;
                                                chpt.ChapterFilePath = newChpt.ChapterFilePath;
                                                chpt.ChapterFileName = newChpt.ChapterFileName;
                                                chpt.IsPublic = 1;
                                                chpt.SysDate = DateTime.Now;
                                                chpt.LastUpDated = DateTime.Now;

                                                DataLoader<ElioUsersTrainingCoursesChapters> loaderCh = new DataLoader<ElioUsersTrainingCoursesChapters>(session);
                                                loaderCh.Insert(chpt);

                                                newChpt.ChapterId = chpt.Id;
                                            }

                                            if (perm.TrainingGroupId == 0 && perm.TierId == 0 && perm.Countries == "")
                                            {
                                                #region All Vendor Partners

                                                List<ElioUsers> partners = SqlCollaboration.GetCollaborationPartnersByVendorId(vSession.User.Id, session);
                                                if (partners.Count > 0)
                                                {
                                                    foreach (ElioUsers partner in partners)
                                                    {
                                                        ElioPartnersTrainingCourses pCourse = new ElioPartnersTrainingCourses();

                                                        pCourse.CategoryId = category.Id;
                                                        pCourse.VendorId = vSession.User.Id;
                                                        pCourse.PartnerId = partner.Id;
                                                        pCourse.CourseId = course.Id;
                                                        pCourse.IsPublic = 1;
                                                        pCourse.IsNew = 1;
                                                        pCourse.Sysdate = DateTime.Now;

                                                        DataLoader<ElioPartnersTrainingCourses> loaderP = new DataLoader<ElioPartnersTrainingCourses>(session);
                                                        loaderP.Insert(pCourse);

                                                        foreach (Chapter newChpt in newCrs.Chapters)
                                                        {
                                                            ElioPartnersTrainingChapters pChapter = new ElioPartnersTrainingChapters();

                                                            pChapter.VendorId = vSession.User.Id;
                                                            pChapter.PartnerId = partner.Id;
                                                            pChapter.CourseId = course.Id;
                                                            pChapter.ChapterId = newChpt.ChapterId;
                                                            pChapter.IsPublic = 1;
                                                            pChapter.IsViewed = 0;
                                                            pChapter.Sysdate = DateTime.Now;

                                                            DataLoader<ElioPartnersTrainingChapters> loaderPC = new DataLoader<ElioPartnersTrainingChapters>(session);
                                                            loaderPC.Insert(pChapter);
                                                        }
                                                    }
                                                }

                                                #endregion
                                            }
                                            else
                                            {
                                                List<int> partnerIds = new List<int>();

                                                if (perm.TrainingGroupId > 0)
                                                {
                                                    List<ElioUsersTrainingGroupMembers> members = Sql.GetElioUserTrainingGroupMembersByGroupId(perm.TrainingGroupId, session);

                                                    foreach (ElioUsersTrainingGroupMembers member in members)
                                                    {
                                                        partnerIds.Add(member.ResellerId);
                                                    }
                                                }

                                                if (perm.TierId > 0)
                                                {
                                                    List<ElioUsers> partners = SqlCollaboration.GetCollaborationPartnersByVendorIdAndTierId(vSession.User.Id, perm.TierId, session);
                                                    foreach (ElioUsers p in partners)
                                                    {
                                                        exist = false;

                                                        if (partnerIds.Count > 0)
                                                        {
                                                            foreach (int item in partnerIds)
                                                            {
                                                                if (item == p.Id)
                                                                {
                                                                    exist = true;
                                                                    break;
                                                                }
                                                            }
                                                        }

                                                        if (!exist)
                                                            partnerIds.Add(p.Id);
                                                    }
                                                }

                                                if (perm.Countries != "")
                                                {
                                                    string countries = "'" + perm.Countries.Replace(",", "','") + "'";

                                                    List<ElioUsers> countryPartners = SqlCollaboration.GetCollaborationPartnersByVendorIdAndCountries(vSession.User.Id, countries, session);
                                                    foreach (ElioUsers cp in countryPartners)
                                                    {
                                                        exist = false;

                                                        if (partnerIds.Count > 0)
                                                        {
                                                            foreach (int item in partnerIds)
                                                            {
                                                                if (item == cp.Id)
                                                                {
                                                                    exist = true;
                                                                    break;
                                                                }
                                                            }
                                                        }

                                                        if (!exist)
                                                            partnerIds.Add(cp.Id);
                                                    }
                                                }

                                                if (partnerIds.Count > 0)
                                                {
                                                    foreach (int partnerId in partnerIds)
                                                    {
                                                        exist = Sql.HasPartnerTraningCourse(partnerId, course.Id, session);
                                                        if (!exist)
                                                        {
                                                            ElioPartnersTrainingCourses pCourse = new ElioPartnersTrainingCourses();

                                                            pCourse.CategoryId = category.Id;
                                                            pCourse.VendorId = vSession.User.Id;
                                                            pCourse.PartnerId = partnerId;
                                                            pCourse.CourseId = course.Id;
                                                            pCourse.IsPublic = 1;
                                                            pCourse.IsNew = 1;
                                                            pCourse.Sysdate = DateTime.Now;

                                                            DataLoader<ElioPartnersTrainingCourses> loaderP = new DataLoader<ElioPartnersTrainingCourses>(session);
                                                            loaderP.Insert(pCourse);
                                                        }

                                                        foreach (Chapter chapter in newCrs.Chapters)
                                                        {
                                                            ElioPartnersTrainingChapters pChapter = new ElioPartnersTrainingChapters();

                                                            pChapter.VendorId = vSession.User.Id;
                                                            pChapter.PartnerId = partnerId;
                                                            pChapter.CourseId = course.Id;
                                                            pChapter.ChapterId = chapter.ChapterId;
                                                            pChapter.IsPublic = 1;
                                                            pChapter.IsViewed = 0;
                                                            pChapter.Sysdate = DateTime.Now;

                                                            DataLoader<ElioPartnersTrainingChapters> loaderPC = new DataLoader<ElioPartnersTrainingChapters>(session);
                                                            loaderPC.Insert(pChapter);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bool hasCoursePerm = false;
                                            if (newCrs.Permissions.CourseId > 0 || newCrs.Permissions.TierId > 0 || newCrs.Permissions.TrainingGroupId > 0 || newCrs.Permissions.Countries.Count > 0)
                                            {
                                                hasCoursePerm = true;
                                            }

                                            DataLoader<ElioUsersTrainingCategoryCoursePermissions> loaderPerm = new DataLoader<ElioUsersTrainingCategoryCoursePermissions>(session);

                                            ElioUsersTrainingCategoryCoursePermissions perm = Sql.GetElioUserTrainingCoursePermissionsByCourseId(vSession.User.Id, newCrs.CourseId, session);
                                            if (perm == null && hasCoursePerm)
                                            {
                                                perm = new ElioUsersTrainingCategoryCoursePermissions();

                                                perm.UserId = vSession.User.Id;
                                                perm.CategoryId = category.Id;
                                                perm.CourseId = newCrs.CourseId;
                                                perm.TrainingGroupId = newCrs.Permissions.TrainingGroupId;
                                                perm.TierId = newCrs.Permissions.TierId;

                                                if (newCrs.Permissions.Countries.Count > 0)
                                                {
                                                    perm.Countries = "";
                                                    foreach (string country in newCrs.Permissions.Countries)
                                                    {
                                                        if (country != "")
                                                            perm.Countries += country + ",";
                                                    }

                                                    if (perm.Countries.EndsWith(","))
                                                        perm.Countries = perm.Countries.Substring(0, perm.Countries.Length - 1);
                                                }
                                                else
                                                    perm.Countries = "";

                                                perm.Sysdate = DateTime.Now;
                                                perm.LastUpdate = DateTime.Now;
                                                perm.IsPublic = 1;

                                                loaderPerm.Insert(perm);
                                            }
                                            else
                                            {
                                                if (hasCoursePerm && perm != null)
                                                {
                                                    perm.TrainingGroupId = newCrs.Permissions.TrainingGroupId;
                                                    perm.TierId = newCrs.Permissions.TierId;

                                                    if (newCrs.Permissions.Countries.Count > 0)
                                                    {
                                                        perm.Countries = "";
                                                        foreach (string country in newCrs.Permissions.Countries)
                                                        {
                                                            if (country != "")
                                                                perm.Countries += country + ",";
                                                        }

                                                        if (perm.Countries.EndsWith(","))
                                                            perm.Countries = perm.Countries.Substring(0, perm.Countries.Length - 1);
                                                    }
                                                    else
                                                        perm.Countries = "";

                                                    perm.LastUpdate = DateTime.Now;

                                                    loaderPerm.Update(perm);
                                                }
                                                else if (!hasCoursePerm && perm != null)
                                                {
                                                    loaderPerm.Delete(perm);
                                                }
                                            }
                                        }
                                    }
                                }

                                session.CommitTransaction();

                                PnlStep5.Enabled = false;
                                BtnPrevious.Visible = false;
                                BtnSave.Text = "Set up New";

                                GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Congratulations! Your training category was set up seuccessfully.", MessageTypes.Success, true, true, true, false, false);
                            }
                        }
                        else
                            LoadStep(1);
                    }
                    else
                    {
                        PnlStep5.Enabled = true;
                        BtnSave.Text = "Submit";

                        TxtCategoryName.Text = "";
                        TxtCourseName.Text = "";
                        TxtCourseOverview.Text = "";
                        RdgCourses.DataSource = null;
                        RdgCourses.DataBind();
                        DdlTiers.Items.Clear();
                        DrpGroupParnters.Items.Clear();
                        DrpCountries.Items.Clear();
                        TxtSelectedCountries.Text = "";
                        //DrpCourses.Items.Clear();
                        TxtChaTitle.Text = "";
                        TxtChaContent.Text = "";
                        TxtChaLink.Text = "";
                        RdgChapter.DataSource = null;
                        RdgChapter.DataBind();
                        RdgCoursesOverview.DataSource = null;
                        RdgCoursesOverview.DataBind();
                        RdgChapterOverview.DataSource = null;
                        RdgCoursesOverview.DataBind();
                        vSession.Category = null;

                        LoadStep(1);
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
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

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                PnlStep5.Enabled = true;
                BtnSave.Text = "Submit";

                TxtCategoryName.Text = "";
                TxtCourseName.Text = "";
                TxtCourseOverview.Text = "";
                RdgCourses.DataSource = null;
                RdgCourses.DataBind();
                DdlTiers.Items.Clear();
                DrpGroupParnters.Items.Clear();
                DrpCountries.Items.Clear();
                TxtSelectedCountries.Text = "";
                TxtChaTitle.Text = "";
                TxtChaContent.Text = "";
                TxtChaLink.Text = "";
                RdgChapter.DataSource = null;
                RdgChapter.DataBind();
                RdgCoursesOverview.DataSource = null;
                RdgCoursesOverview.DataBind();
                RdgChapterOverview.DataSource = null;
                RdgCoursesOverview.DataBind();
                vSession.Category = null;

                LoadStep(1);
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

        protected void BtnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (divStep2.Visible)
                    LoadStep(1);
                else if (divStep3.Visible)
                    LoadStep(2);
                else if (divStep4.Visible)
                    LoadStep(3);
                else if (divStep5.Visible)
                    LoadStep(4);
                else if (divStep1.Visible)
                    BtnPrevious.Visible = false;
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

        protected void BtnAddCourse_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (true)
                    {
                        if (vSession.Category != null)
                        {
                            if (TxtCourseName.Text != "")
                            {
                                string overviewImageName = "";
                                string overviewImageType = "";

                                Course c = new Course();
                                c.CourseId = Sql.GetTrainingCourseLastId(session) + 1;
                                c.CourseDescription = TxtCourseName.Text;
                                c.OverviewText = TxtCourseOverview.Text;
                                c.OverviewImagePath = UploadCoursePreviewFile(out overviewImageName, out overviewImageType);

                                if (c.OverviewImagePath == "" || overviewImageName == "" || overviewImageType == "")
                                    return;

                                c.OverviewImageName = overviewImageName;
                                c.OverviewImageType = overviewImageType;
                                
                                vSession.Category.Courses.Add(c);

                                LoadRptCourse(Navigation.None, RdgCourses);

                                TxtCourseName.Text = "";
                                TxtCourseOverview.Text = "";
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Please type course name!", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }
                        }
                        else
                        {
                            LoadStep(1);
                        }
                    }
                    else
                    {
                        //ElioUsersTrainingCategories category = Sql.GetElioUserTrainingCategoryByName(vSession.User.Id, TxtCategoryName.Text, session);

                        //if (category != null)
                        //{
                            //List<ElioUsersTrainingCategoriesCourses> courses = Sql.GetElioUserTrainingCategoryCourses(vSession.User.Id, category.Id, session);

                            //if (courses.Count > 0)
                            //{
                            //foreach (ElioUsersTrainingCategoriesCourses course in courses)
                            //{
                            //    ListItem item = new ListItem();

                            //    item.Value = course.Id.ToString();
                            //    item.Text = course.CourseDescription;

                            //    item.Selected = true;

                            //    CbxUserCoursesList.Items.Add(item);
                            //}
                            //}
                            //else
                            //{

                        //    ElioUsersTrainingCategoriesCourses course = new ElioUsersTrainingCategoriesCourses();

                        //    course.UserId = vSession.User.Id;
                        //    course.CategoryId = category.Id;
                        //    course.CourseDescription = TxtCourseName.Text;
                        //    course.OverviewText = TxtCourseOverview.Text;
                        //    course.OverviewImagePath = "";
                        //    course.IsPublic = 1;
                        //    course.SysDate = DateTime.Now;
                        //    course.LastUpDated = DateTime.Now;

                        //    DataLoader<ElioUsersTrainingCategoriesCourses> loader = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);
                        //    loader.Insert(course);

                        //    ListItem item = new ListItem();

                        //    item.Value = course.Id.ToString();
                        //    item.Text = course.CourseDescription;

                        //    item.Selected = true;

                        //    CbxUserCoursesList.Items.Add(item);
                        //    //}
                        //}

                        //LoadCourses();
                        //LoadRepeater(Navigation.None, LoadCoursesRpt(), RdgChapter);
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

        protected void BtnAddCountry_Click(object sender, EventArgs e)
        {
            try
            {
                if (DrpCountries.SelectedValue != "0")
                {
                    bool insert = true;

                    if (TxtSelectedCountries.Text != "")
                    {
                        string[] selected = TxtSelectedCountries.Text.Split(',').ToArray();
                        foreach (string s in selected)
                        {
                            if (s != "")
                            {
                                string existCountry = s.Trim();
                                if (existCountry != "")
                                {
                                    if (existCountry == DrpCountries.SelectedItem.Text)
                                    {
                                        insert = false;
                                        GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "This country is already selected!", MessageTypes.Error, true, true, true, true, false);

                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (insert)
                        TxtSelectedCountries.Text = (TxtSelectedCountries.Text != "") ? TxtSelectedCountries.Text + ", " + DrpCountries.SelectedItem.Text : DrpCountries.SelectedItem.Text;

                    aDeleteCountry.Visible = TxtSelectedCountries.Text != "";
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "You must select country!", MessageTypes.Error, true, true, true, true, false);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnAddchapter_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcWizardAlertControl.Visible = false;

                if (vSession.User != null)
                {
                    if (vSession.Category != null)
                    {
                        if (vSession.Category.CategoryName != "")
                        {
                            if (vSession.Category.Courses.Count > 0 && vSession.Category.Courses[0].CourseDescription != "")
                            {
                                Chapter chpt = new Chapter();
                                chpt.CourseId = vSession.Category.Courses[0].CourseId;
                                chpt.ChapterId = Sql.GetTrainingChapterLastId(session) + 1;
                                chpt.ChapterTitle = TxtChaTitle.Text;
                                chpt.ChapterText = TxtChaContent.Text;
                                chpt.ChapterLink = TxtChaLink.Text;
                                string fileResp = UploadChapterFile();

                                if (fileResp != "" && fileResp == "Error")
                                    return;

                                chpt.ChapterFilePath = fileResp;

                                if (chpt.ChapterFilePath == "")
                                    chpt.ChapterFileName = "";
                                else
                                    chpt.ChapterFileName = ChapterFile.PostedFile.FileName;

                                vSession.Category.Courses[0].Chapters.Add(chpt);

                                LoadRptChapter(Navigation.None, RdgChapter);

                                TxtChaTitle.Text = "";
                                TxtChaContent.Text = "";
                                TxtChaLink.Text = "";
                            }
                        }
                        else
                        {
                            LoadStep(2);
                        }
                    }
                    else
                    {
                        LoadStep(1);
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

        protected void aCreateGroup_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    divRetailorFailure.Visible = divRetailorSuccess.Visible = false;

                    GroupId = -1;
                    TbxGroupName.Text = "";
                    IsEditMode = false;

                    GetPartnersList("id", null, 0);
                    BtnCreate.Text = (!IsEditMode) ? "Create" : "Update";

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenCreateGroupPopUp();", true);
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

        #endregion

        #region Grids

        #endregion

        #region Tabs

        #endregion

        #region DropDown Lists

        protected void DrpCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                LoadRptChapter(Navigation.None, RdgChapter);

                TxtChaTitle.Text = "";
                TxtChaContent.Text = "";
                TxtChaLink.Text = "";

                //LoadRepeaterChapter(Navigation.None, LoadChaptersRpt(), RdgChapter);
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

        protected void DrpSavedCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        private void GetPartnersList(string orderByClause, GlobalMethods.SearchCriteria criteria, int libraryGroupId)
        {
            if (vSession.User != null)
            {
                int isPublic = 1;
                int isDeleted = 0;
                int isNew = 1;
                int isViewed = 0;

                List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), isDeleted, isPublic, vSession.User.CompanyType, criteria, orderByClause, libraryGroupId, session);

                if (partners.Count > 0)
                {
                    //ImgBtnSendMsg.Visible = true;
                    //RptConnectionList.Visible = true;
                    RptRetailorsList.Visible = true;
                    RptEditRetailorsList.Visible = true;
                    //PnlCreateRetailorGroups.Enabled = true;

                    //UcMessageAlert.Visible = false;

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

                    //RptConnectionList.DataSource = table;
                    //RptConnectionList.DataBind();

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
                    //GlobalMethods.ShowMessageControlDA(MessageControlRetailors, alert, MessageTypes.Info, true, true, false, false, false);
                    GlobalMethods.ShowMessageControlDA(MessageControlEditRetailors, alert, MessageTypes.Info, true, true, false, false, false);
                    GlobalMethods.ShowMessageControlDA(MessageControlCreateRetailors, alert, MessageTypes.Info, true, true, false, false, false);

                    //ImgBtnSendMsg.Visible = false;
                    //PnlCreateRetailorGroups.Enabled = false;
                }
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

                        CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                        ImageButton imgBtnCompanyLogo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCompanyLogo");

                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "id");
                        HiddenField hdnMasterUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "master_user_id");
                        HiddenField hdnInvitationStatus = (HiddenField)ControlFinder.FindControlRecursive(item, "invitation_status");
                        HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");
                        HiddenField hdnEmail = (HiddenField)ControlFinder.FindControlRecursive(item, "email");
                        HiddenField hdnMsgsCount = (HiddenField)ControlFinder.FindControlRecursive(item, "msgs_count");

                        hdnId.Value = row["id"].ToString();
                        cbxSelectUser.InputAttributes.Add("id", hdnId.Value);

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            if (GroupId > 0)
                            {
                                cbxSelectUser.Checked = SqlCollaboration.ExistCollaborationUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session);
                            }
                        }

                        hdnMasterUserId.Value = (vSession.User.CompanyType == Types.Vendors.ToString()) ? row["master_user_id"].ToString() : row["partner_user_id"].ToString();
                        cbxSelectUser.InputAttributes.Add("master_user_id", hdnMasterUserId.Value);

                        hdnPartnerUserId.Value = (vSession.User.CompanyType == Types.Vendors.ToString()) ? row["partner_user_id"].ToString() : row["master_user_id"].ToString();
                        cbxSelectUser.InputAttributes.Add("partner_user_id", hdnPartnerUserId.Value);

                        hdnInvitationStatus.Value = row["invitation_status"].ToString();
                        hdnEmail.Value = row["email"].ToString();

                        Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                        Label lblCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCountry");
                        Label lblMessagesCount = (Label)ControlFinder.FindControlRecursive(item, "LblMessagesCount");

                        lblCompanyName.Text = row["company_name"].ToString();
                        lblCountry.Text = row["country"].ToString();

                        ElioUsers company = Sql.GetUserById(Convert.ToInt32(hdnPartnerUserId.Value), session);

                        if (company != null)
                        {
                            aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.Profile(company);
                            aCompanyLogo.Target = aCompanyName.Target = "_blanck";
                            imgBtnCompanyLogo.ImageUrl = company.CompanyLogo;
                            imgBtnCompanyLogo.ToolTip = "View company's profile";
                            imgBtnCompanyLogo.AlternateText = "Company logo";
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

        protected void RptEditRetailorsList_OnItemDataBound(object sender, RepeaterItemEventArgs args)
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

                        ImageButton imgBtnCompanyLogo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCompanyLogo");

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

                        hdnMasterUserId.Value = (vSession.User.CompanyType == Types.Vendors.ToString()) ? row["master_user_id"].ToString() : row["partner_user_id"].ToString();
                        cbxSelectUser.InputAttributes.Add("master_user_id", hdnMasterUserId.Value);

                        hdnPartnerUserId.Value = (vSession.User.CompanyType == Types.Vendors.ToString()) ? row["partner_user_id"].ToString() : row["master_user_id"].ToString();
                        cbxSelectUser.InputAttributes.Add("partner_user_id", hdnPartnerUserId.Value);

                        hdnInvitationStatus.Value = row["invitation_status"].ToString();
                        hdnEmail.Value = row["email"].ToString();

                        Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                        Label lblCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCountry");
                        //Label lblMessagesCount = (Label)ControlFinder.FindControlRecursive(item, "LblMessagesCount");

                        if (GroupId != -1)
                            cbxSelectUser.Checked = SqlCollaboration.ExistCollaborationUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session);

                        lblCompanyName.Text = row["company_name"].ToString();
                        lblCountry.Text = row["country"].ToString();

                        //HtmlControl spanMessagesCountNotification = (HtmlControl)ControlFinder.FindControlRecursive(item, "spanMessagesCountNotification");

                        //if (row["msgs_count"].ToString() != "0")
                        //{
                        //    spanMessagesCountNotification.Visible = true;
                        //    lblMessagesCount.Text = row["msgs_count"].ToString();
                        //}
                        //else
                        //{
                        //    spanMessagesCountNotification.Visible = false;
                        //    lblMessagesCount.Text = "";
                        //}

                        //int partnerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(hdnPartnerUserId.Value) : Convert.ToInt32(hdnMasterUserId.Value);
                        ElioUsers company = Sql.GetUserById(Convert.ToInt32(hdnPartnerUserId.Value), session);

                        if (company != null)
                        {
                            aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.Profile(company);
                            aCompanyLogo.Target = aCompanyName.Target = aCompanyName.Target = "_blanck";
                            imgBtnCompanyLogo.ImageUrl = company.CompanyLogo;
                            imgBtnCompanyLogo.ToolTip = "View company's profile";
                            imgBtnCompanyLogo.AlternateText = "Company logo";
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

                    if (!HasSelectedItemOld(RptEditRetailorsList))
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

                    LoadGroups();

                    //UpdatePanel2.Update();
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

        private void LoadGroups()
        {
            DrpGroupParnters.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Select Group";

            DrpGroupParnters.Items.Add(item);

            if (vSession.User != null)
            {
                List<ElioUsersTrainingGroups> groups = SqlCollaboration.GetTrainingUserGroups(vSession.User.Id, session);

                if (groups.Count > 0)
                {
                    foreach (ElioUsersTrainingGroups group in groups)
                    {
                        item = new ListItem();

                        item.Value = group.Id.ToString();
                        item.Text = group.TrainingGroupName;

                        DrpGroupParnters.Items.Add(item);

                        if (vSession.Category.Courses[0] != null && vSession.Category.Courses[0].Permissions.TrainingGroupId == int.Parse(item.Value))
                            item.Selected = true;
                    }

                    if (vSession.Category.Courses[0] == null || (vSession.Category.Courses[0] != null && vSession.Category.Courses[0].CourseId == 0))
                        DrpGroupParnters.SelectedValue = "0";
                }
            }
            else
            {
                Response.Redirect(ControlLoader.Login, false);
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
                    //UcMessageAlert.Visible = false;

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
                    //GlobalMethods.ShowMessageControlDA(MessageControlRetailors, alert, MessageTypes.Info, true, true, false);
                }
            }
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
                    //UcMessageAlert.Visible = false;

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

        private bool HasSelectedItemOld(Repeater rpt)
        {
            int count = 0;
            foreach (RepeaterItem item in rpt.Items)
            {
                CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                if (cbx.Checked)
                {
                    count++;
                }

                if (count > 1)
                    return true;
            }

            return false;
        }

        private List<int> HasSelectedItem(Repeater rpt)
        {
            List<int> resellersIDs = new List<int>();

            foreach (RepeaterItem item in rpt.Items)
            {
                CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                if (cbx.Checked)
                {
                    HiddenField hdnPartnerId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");
                    resellersIDs.Add(Convert.ToInt32(hdnPartnerId.Value));
                }
            }

            return resellersIDs;
        }

        protected void BtnCreate_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                divRetailorFailure.Visible = divRetailorSuccess.Visible = false;
                LblRetailorFailureMsg.Text = LblRetailorSuccessMsg.Text = "";

                if (vSession.User != null)
                {
                    #region Validations

                    if (TbxGroupName.Text == string.Empty)
                    {
                        divRetailorFailure.Visible = true;
                        LblRetailorFailureMsg.Text = "Please, add group name";
                        return;
                    }
                    else
                    {
                        if (!IsEditMode)
                        {
                            bool exist = SqlCollaboration.ExistTrainingGroupDescription(vSession.User.Id, TbxGroupName.Text.Trim(), session);
                            if (exist)
                            {
                                divRetailorFailure.Visible = true;
                                LblRetailorFailureMsg.Text = "Sorry, this group name already exists by you.";
                                return;
                            }
                        }
                    }

                    List<int> resellersIDs = HasSelectedItem(RptRetailorsList);

                    if (resellersIDs.Count <= 1)
                    {
                        divRetailorFailure.Visible = true;
                        LblRetailorFailureMsg.Text = "Please, you must select at least two in order to create a group.";
                        return;
                    }

                    #endregion

                    try
                    {
                        if (!IsEditMode)
                        {
                            #region Insert new Group

                            session.BeginTransaction();

                            ElioUsersTrainingGroups group = new ElioUsersTrainingGroups();

                            group.UserId = vSession.User.Id;
                            group.TrainingGroupName = TbxGroupName.Text;
                            group.DateCreated = DateTime.Now;
                            group.LastUpdate = DateTime.Now;
                            group.IsActive = 1;
                            group.IsPublic = 1;

                            DataLoader<ElioUsersTrainingGroups> loader = new DataLoader<ElioUsersTrainingGroups>(session);
                            loader.Insert(group);

                            foreach (int resellerID in resellersIDs)
                            {
                                ElioUsersTrainingGroupMembers member = new ElioUsersTrainingGroupMembers();

                                member.CreatorUserId = vSession.User.Id;
                                member.ResellerId = resellerID;
                                member.TrainingGroupId = group.Id;
                                member.DateCreated = DateTime.Now;
                                member.LastUpdate = DateTime.Now;
                                member.IsActive = 1;
                                member.IsPublic = 1;

                                DataLoader<ElioUsersTrainingGroupMembers> mLoader = new DataLoader<ElioUsersTrainingGroupMembers>(session);
                                mLoader.Insert(member);
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
                                resellersIDs.Clear();

                                session.BeginTransaction();

                                bool exist = SqlCollaboration.ExistTrainingGroupDescriptionToOtherGroupId(vSession.User.Id, GroupId, TbxGroupName.Text.Trim(), session);
                                if (exist)
                                {
                                    divRetailorFailure.Visible = true;
                                    LblRetailorFailureMsg.Text = "Sorry, this name already exists to other group of yours.";
                                    return;
                                }
                                else
                                {
                                    SqlCollaboration.UpdateTrainingUserGroupByGroupId(GroupId, vSession.User.Id, TbxGroupName.Text, 1, 1, session);
                                }

                                foreach (RepeaterItem item in RptRetailorsList.Items)
                                {
                                    HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");

                                    CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                                    if (hdnPartnerUserId.Value != "")
                                    {
                                        bool existMember = SqlCollaboration.ExistTrainingUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session);
                                        if (cbxSelectUser.Checked)
                                        {
                                            if (!existMember)
                                                resellersIDs.Add(Convert.ToInt32(hdnPartnerUserId.Value));
                                        }
                                        else
                                        {
                                            if (existMember)
                                                SqlCollaboration.DeleteTrainingUserGroupRetailorByGroupId(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), session);
                                        }
                                    }
                                }

                                foreach (int resellerID in resellersIDs)
                                {
                                    ElioUsersTrainingGroupMembers member = new ElioUsersTrainingGroupMembers();

                                    member.CreatorUserId = vSession.User.Id;
                                    member.ResellerId = resellerID;
                                    member.TrainingGroupId = GroupId;
                                    member.DateCreated = DateTime.Now;
                                    member.LastUpdate = DateTime.Now;
                                    member.IsActive = 1;
                                    member.IsPublic = 1;

                                    DataLoader<ElioUsersTrainingGroupMembers> mLoader = new DataLoader<ElioUsersTrainingGroupMembers>(session);
                                    mLoader.Insert(member);
                                }

                                session.CommitTransaction();
                            }
                            else
                            {
                                divRetailorFailure.Visible = true;
                                LblRetailorFailureMsg.Text = "Sorry, something went wrong! Please try again later or contact us";

                                Logger.DetailedError(Request.Url.ToString(), string.Format("No Group Id while update from user {0}", vSession.User.Id.ToString()));
                                return;
                            }

                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();

                        divRetailorFailure.Visible = true;
                        LblRetailorFailureMsg.Text = "Sorry, something went wrong! Please try again later or contact us";

                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        throw ex;
                    }

                    divRetailorSuccess.Visible = true;
                    LblRetailorSuccessMsg.Text = (!IsEditMode) ? "Group created successfully" : "Group updated successfully";

                    LoadGroups();

                    IsEditMode = true;

                    //UpdatePanel2.Update();
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

        protected void RdgCourses_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        Course row = (Course)args.Item.DataItem;
                        if (row != null)
                        {
                            if (vSession.User != null)
                            {
                                Image imgPreview = (Image)ControlFinder.FindControlRecursive(item, "ImgPreview");
                                imgPreview.ImageUrl = "/TrainingLibrary/" + row.OverviewImagePath;

                                if (!string.IsNullOrEmpty(row.OverviewText) && row.OverviewText.Length > 20)
                                {
                                    Label lblPreviewText = (Label)ControlFinder.FindControlRecursive(item, "LblPreviewText");
                                    Image imgInfo = (Image)ControlFinder.FindControlRecursive(item, "ImgInfo");
                                    RadToolTip rttImgInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttImgInfo");

                                    lblPreviewText.Text = row.OverviewText.Substring(0, 20);
                                    imgInfo.Visible = true;
                                    rttImgInfo.Text = row.OverviewText;
                                }
                            }
                            else
                            {
                                Response.Redirect(ControlLoader.Login, false);
                            }
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

        protected void LoadRptCourse(Navigation navigation, Repeater rpt)
        {
            try
            {
                if (vSession.Category != null)
                {
                    PagedDataSource objPds = new PagedDataSource();

                    if (vSession.Category.Courses.Count > 0)
                    {
                        aAddCourse.Visible = false;

                        objPds.DataSource = vSession.Category.Courses;

                        objPds.AllowPaging = true;

                        objPds.PageSize = 10;

                        switch (navigation)
                        {
                            case Navigation.First:
                                NowViewing = 1;
                                break;

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

                        objPds.CurrentPageIndex = NowViewing;
                        rpt.DataSource = objPds;
                        rpt.DataBind();
                    }
                    else
                    {
                        //aAddCourse.Visible = true;
                        rpt.DataSource = null;
                        rpt.DataBind();
                    }
                }
                else
                    LoadStep(1);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void LoadRptChapter(Navigation navigation, Repeater rpt)
        {
            try
            {
                PagedDataSource objPds = new PagedDataSource();

                Course selCourse = vSession.Category.Courses[0];
                if (selCourse != null)
                {
                    if (selCourse.Chapters.Count > 0)
                    {
                        objPds.DataSource = selCourse.Chapters;

                        objPds.AllowPaging = true;

                        objPds.PageSize = 10;

                        switch (navigation)
                        {
                            case Navigation.First:
                                NowViewing = 1;
                                break;

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

                        objPds.CurrentPageIndex = NowViewing;
                        rpt.DataSource = objPds;
                        rpt.DataBind();
                    }
                    else
                    {
                        rpt.DataSource = null;
                        rpt.DataBind();
                    }
                }
                else
                {
                    rpt.DataSource = null;
                    rpt.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public class ChapterOvrv
        {
            public int ChapterId { get; set; }

            public int CourseId { get; set; }

            public string CourseDescription { get; set; }

            public string ChapterTitle { get; set; }

            public string ChapterText { get; set; }

            public string ChapterFilePath { get; set; }

            public string ChapterFileName { get; set; }

            public string ChapterLink { get; set; }
        }

        public class PermissionsOvrv
        {
            public int CourseId { get; set; }

            public string CourseDescription { get; set; }

            public string TierName { get; set; }

            public string GroupName { get; set; }

            public int GroupId { get; set; }

            public int TierId { get; set; }

            public string Countries { get; set; }
        }


        protected void RdgChapter_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        Chapter row = (Chapter)args.Item.DataItem;
                        if (row != null)
                        {
                            if (vSession.User != null)
                            {
                                HtmlAnchor aChapterFilePath = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aChapterFilePath");
                                aChapterFilePath.HRef = System.Configuration.ConfigurationManager.AppSettings["ViewTrainingLibraryPath"] + "/" + row.ChapterFilePath;

                                if (!string.IsNullOrEmpty(row.ChapterText) && row.ChapterText.Length > 30)
                                {
                                    Label lblChapterText = (Label)ControlFinder.FindControlRecursive(item, "LblChapterText");
                                    Image imgInfo = (Image)ControlFinder.FindControlRecursive(item, "ImgInfo");
                                    RadToolTip rttImgInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttImgInfo");

                                    lblChapterText.Text = row.ChapterText.Substring(0, 30) + "...";
                                    imgInfo.Visible = true;
                                    rttImgInfo.Text = row.ChapterText;
                                }
                            }
                            else
                            {
                                Response.Redirect(ControlLoader.Login, false);
                            }
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

        protected void RdgCoursesOverview_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        Course row = (Course)args.Item.DataItem;
                        if (row != null)
                        {
                            if (vSession.User != null)
                            {
                                Image imgPreview = (Image)ControlFinder.FindControlRecursive(item, "ImgPreview");
                                imgPreview.ImageUrl = "/TrainingLibrary/" + row.OverviewImagePath;

                                if (!string.IsNullOrEmpty(row.OverviewText) && row.OverviewText.Length > 30)
                                {
                                    Label lblPreviewText = (Label)ControlFinder.FindControlRecursive(item, "LblPreviewText");
                                    Image imgInfo = (Image)ControlFinder.FindControlRecursive(item, "ImgInfo");
                                    RadToolTip rttImgInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttImgInfo");

                                    lblPreviewText.Text = row.OverviewText.Substring(0, 30) + "...";
                                    imgInfo.Visible = true;
                                    rttImgInfo.Text = row.OverviewText;
                                }
                            }
                            else
                            {
                                Response.Redirect(ControlLoader.Login, false);
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

        protected void RdgChapterOverview_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        ChapterOvrv row = (ChapterOvrv)args.Item.DataItem;
                        if (row != null)
                        {
                            if (vSession.User != null)
                            {
                                HtmlAnchor aChapterFilePath = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aChapterFilePath");
                                aChapterFilePath.HRef = System.Configuration.ConfigurationManager.AppSettings["ViewTrainingLibraryPath"] + "/" + row.ChapterFilePath;

                                if (!string.IsNullOrEmpty(row.ChapterText) && row.ChapterText.Length > 30)
                                {
                                    Label lblChapterText = (Label)ControlFinder.FindControlRecursive(item, "LblChapterText");
                                    Image imgInfo = (Image)ControlFinder.FindControlRecursive(item, "ImgInfo");
                                    RadToolTip rttImgInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttImgInfo");

                                    lblChapterText.Text = row.ChapterText.Substring(0, 30) + "...";
                                    imgInfo.Visible = true;
                                    rttImgInfo.Text = row.ChapterText;
                                }
                            }
                            else
                            {
                                Response.Redirect(ControlLoader.Login, false);
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

        protected void DrpCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    BtnNext.Visible = DrpCategories.SelectedValue != "0";
                    //if (DrpCategories.SelectedValue != "0")
                    //{
                    //    divExistingCategories.Visible = true;
                    //    divNewCategory.Visible = false;
                    //}
                    //else
                    //{
                    //    divExistingCategories.Visible = true;
                    //    divNewCategory.Visible = true;
                    //    vSession.Category = null;
                    //}

                    //UpdatePanel1.Update();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDeleteCourse_ServerClick(object sender, EventArgs e)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        if (vSession.Category != null)
                        {
                            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCourseId");

                            if (hdnId != null)
                            {
                                foreach (Chapter chpt in vSession.Category.Courses.Where(n => n.CourseId == int.Parse(hdnId.Value)).FirstOrDefault().Chapters)
                                {
                                    DirectoryInfo filesInChaptersDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["TrainingLibraryFullTargetFolder"].ToString() + "\\" + vSession.User.GuId + "\\" + vSession.Category.CategoryName + "\\" + vSession.Category.Courses.Where(n => n.CourseId == int.Parse(hdnId.Value)).FirstOrDefault().CourseDescription + "\\" + chpt.ChapterTitle + "\\"));

                                    try
                                    {
                                        foreach (FileInfo logFile in filesInChaptersDirectory.GetFiles())
                                        {
                                            logFile.Delete();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    }
                                }

                                foreach (Course crs in vSession.Category.Courses)
                                {
                                    if (crs.CourseId == int.Parse(hdnId.Value))
                                    {
                                        DirectoryInfo filesInCourseDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["TrainingLibraryFullTargetFolder"].ToString() + "\\" + vSession.User.GuId + "\\" + vSession.Category.CategoryName + "\\" + vSession.Category.Courses.Where(n => n.CourseId == int.Parse(hdnId.Value)).FirstOrDefault().CourseDescription + "\\"));

                                        try
                                        {
                                            foreach (FileInfo logFile in filesInCourseDirectory.GetFiles())
                                            {
                                                logFile.Delete();
                                            }

                                            foreach (DirectoryInfo dirInfo in filesInCourseDirectory.GetDirectories())
                                            {
                                                foreach (FileInfo logFile in dirInfo.GetFiles())
                                                {
                                                    logFile.Delete();
                                                }
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }

                                        vSession.Category.Courses.Where(n => n.CourseId == int.Parse(hdnId.Value)).FirstOrDefault().Chapters.Clear();

                                        vSession.Category.Courses.Remove(crs);

                                        LoadRptCourse(Navigation.None, RdgCourses);
                                    }
                                }
                            }
                        }
                        else
                            LoadStep(1);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDeleteChapter_ServerClick(object sender, EventArgs e)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        if (vSession.Category != null)
                        {
                            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnChapterId");

                            if (hdnId != null)
                            {
                                foreach (Chapter chpt in vSession.Category.Courses.Where(n => n.CourseId == int.Parse(hdnId.Value)).FirstOrDefault().Chapters)
                                {
                                    DirectoryInfo filesInChaptersDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["TrainingLibraryFullTargetFolder"].ToString() + "\\" + vSession.User.GuId + "\\" + vSession.Category.CategoryName + "\\" + vSession.Category.Courses.Where(n => n.CourseId == int.Parse(hdnId.Value)).FirstOrDefault().CourseDescription + "\\" + chpt.ChapterTitle + "\\"));

                                    try
                                    {
                                        foreach (FileInfo logFile in filesInChaptersDirectory.GetFiles())
                                        {
                                            logFile.Delete();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    }
                                }

                                foreach (Course crs in vSession.Category.Courses)
                                {
                                    if (crs.CourseId == int.Parse(hdnId.Value))
                                    {
                                        DirectoryInfo filesInCourseDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["TrainingLibraryFullTargetFolder"].ToString() + "\\" + vSession.User.GuId + "\\" + vSession.Category.CategoryName + "\\" + vSession.Category.Courses.Where(n => n.CourseId == int.Parse(hdnId.Value)).FirstOrDefault().CourseDescription + "\\"));

                                        try
                                        {
                                            foreach (FileInfo logFile in filesInCourseDirectory.GetFiles())
                                            {
                                                logFile.Delete();
                                            }

                                            foreach (DirectoryInfo dirInfo in filesInCourseDirectory.GetDirectories())
                                            {
                                                foreach (FileInfo logFile in dirInfo.GetFiles())
                                                {
                                                    logFile.Delete();
                                                }
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }

                                        vSession.Category.Courses.Where(n => n.CourseId == int.Parse(hdnId.Value)).FirstOrDefault().Chapters.Clear();

                                        vSession.Category.Courses.Remove(crs);

                                        LoadRptCourse(Navigation.None, RdgCourses);
                                    }
                                }
                            }
                        }
                        else
                            LoadStep(1);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aEditChapter_ServerClick(object sender, EventArgs e)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnChapterId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnChapterId");
                        HiddenField hdnCourserId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCourserId");
                        Label lblCourseName = (Label)ControlFinder.FindControlRecursive(item, "LblCourseName");
                        Label lblPreviewText = (Label)ControlFinder.FindControlRecursive(item, "LblPreviewText");
                        Image imgPreview = (Image)ControlFinder.FindControlRecursive(item, "ImgPreview");

                        TxtChaTitle.Text = vSession.Category.Courses[0].Chapters.Where(n => n.ChapterId == int.Parse(hdnChapterId.Value)).FirstOrDefault().ChapterTitle;
                        TxtChaContent.Text = vSession.Category.Courses[0].Chapters.Where(n => n.ChapterId == int.Parse(hdnChapterId.Value)).FirstOrDefault().ChapterText;
                        TxtChaLink.Text = vSession.Category.Courses[0].Chapters.Where(n => n.ChapterId == int.Parse(hdnChapterId.Value)).FirstOrDefault().ChapterLink;
                        Image1.ImageUrl = "/TrainingLibrary/" + vSession.Category.Courses[0].Chapters.Where(n => n.ChapterId == int.Parse(hdnChapterId.Value)).FirstOrDefault().ChapterFilePath;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DrpCoursesPerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                //if (vSession.Category != null)
                //{
                //    if (DrpCoursesPerm.SelectedValue != "0")
                //    {
                //        vSession.Category.Courses.Where(n => n.CourseId == int.Parse(DrpCoursesPerm.SelectedValue)).FirstOrDefault().Permissions.CourseId = int.Parse(DrpCoursesPerm.SelectedValue);

                //        if (vSession.Category.Courses.Where(n => n.CourseId == int.Parse(DrpCoursesPerm.SelectedValue)).FirstOrDefault().Permissions.TierId > 0)
                //        {
                //            DdlTiers.SelectedValue = vSession.Category.Courses.Where(n => n.CourseId == int.Parse(DrpCoursesPerm.SelectedValue)).FirstOrDefault().Permissions.TierId.ToString();
                //        }
                //        else
                //            DdlTiers.SelectedValue = "0";

                //        if (vSession.Category.Courses.Where(n => n.CourseId == int.Parse(DrpCoursesPerm.SelectedValue)).FirstOrDefault().Permissions.TrainingGroupId > 0)
                //            DrpGroupParnters.SelectedValue = vSession.Category.Courses.Where(n => n.CourseId == int.Parse(DrpCoursesPerm.SelectedValue)).FirstOrDefault().Permissions.TrainingGroupId.ToString();
                //        else
                //            DrpGroupParnters.SelectedValue = "0";

                //        if (vSession.Category.Courses.Where(n => n.CourseId == int.Parse(DrpCoursesPerm.SelectedValue)).FirstOrDefault().Permissions.Countries.Count > 0)
                //        {
                //            TxtSelectedCountries.Text = "";
                //            foreach (string country in vSession.Category.Courses.Where(n => n.CourseId == int.Parse(DrpCoursesPerm.SelectedValue)).FirstOrDefault().Permissions.Countries)
                //            {
                //                TxtSelectedCountries.Text += country + ",";
                //            }

                //            if (TxtSelectedCountries.Text.EndsWith(","))
                //                TxtSelectedCountries.Text = TxtSelectedCountries.Text.TrimEnd(',');
                //        }
                //        else
                //            TxtSelectedCountries.Text = "";

                //        aDeleteCountry.Visible = TxtSelectedCountries.Text != "";
                //    }
                //    else
                //    {
                //        //vSession.Category.Courses.ForEach(n => n.Permissions.TrainingGroupId = 0);
                //        //vSession.Category.Courses.ForEach(n => n.Permissions.TierId = 0);
                //        //vSession.Category.Courses.ForEach(n => n.Permissions.Countries.Clear());
                //        //vSession.Category.Courses.ForEach((n => n.Permissions.CourseId = 0));

                //        DdlTiers.SelectedValue = "0";
                //        DrpGroupParnters.SelectedValue = "0";
                //        DrpCountries.SelectedIndex = -1;
                //        TxtSelectedCountries.Text = "";
                //        aDeleteCountry.Visible = false;
                //    }
                //}
                //else
                //    LoadStep(1);
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

        protected void RdgPermissionsOverview_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void aAddCoursePermissions_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (DdlTiers.SelectedValue != "0")
                {
                    vSession.Category.Courses[0].Permissions.TierId = int.Parse(DdlTiers.SelectedValue);
                    vSession.Category.Courses[0].Permissions.TierName = DdlTiers.SelectedItem.Text;
                }
                else
                {
                    vSession.Category.Courses[0].Permissions.TierId = 0;
                    vSession.Category.Courses[0].Permissions.TierName = "";
                }
                if (DrpGroupParnters.SelectedValue != "0")
                {
                    vSession.Category.Courses[0].Permissions.TrainingGroupId = int.Parse(DrpGroupParnters.SelectedValue);
                    vSession.Category.Courses[0].Permissions.TrainingGroupName = DrpGroupParnters.SelectedItem.Text;
                }
                else
                {
                    vSession.Category.Courses[0].Permissions.TrainingGroupId = 0;
                    vSession.Category.Courses[0].Permissions.TrainingGroupName = "";
                }
                if (TxtSelectedCountries.Text != "")
                {
                    vSession.Category.Courses[0].Permissions.Countries.Clear();
                    vSession.Category.Courses[0].Permissions.Countries = TxtSelectedCountries.Text.Split(',').ToList();
                }
                else
                    vSession.Category.Courses[0].Permissions.Countries.Clear();

                aDeleteCountry.Visible = TxtSelectedCountries.Text != "";

                GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Permissions added to course with Description " + vSession.Category.Courses[0].CourseDescription, MessageTypes.Success, true, true, true, true, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aDeleteCountry_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (TxtSelectedCountries.Text != "")
                {
                    TxtSelectedCountries.Text = TxtSelectedCountries.Text.TrimEnd(',');
                    string[] cntTxts = TxtSelectedCountries.Text.Split(',').ToArray();
                    //cntTxts.Take(cntTxts.Length - 2);
                    TxtSelectedCountries.Text = "";
                    for (int i = 0; i < cntTxts.Length - 1; i++)
                    {
                        if (cntTxts[i].Trim() != "")
                            TxtSelectedCountries.Text += cntTxts[i].Trim() + ", ";
                    }

                    if (TxtSelectedCountries.Text.EndsWith(", "))
                        TxtSelectedCountries.Text = TxtSelectedCountries.Text.Substring(0, TxtSelectedCountries.Text.Length - 2);
                }

                aDeleteCountry.Visible = TxtSelectedCountries.Text != "";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
    }
}