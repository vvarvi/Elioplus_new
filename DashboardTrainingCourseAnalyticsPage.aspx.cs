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
using Telerik.Web.UI;
using System.Web.UI;
using WdS.ElioPlus.Controls.Dashboard.AlertControls;

namespace WdS.ElioPlus
{
    public partial class DashboardTrainingCourseAnalyticsPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        #region Page properties

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

        public string CourseId
        {
            get
            {
                if (ViewState["CourseId"] == null)
                {
                    string courseId = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;

                        if (pathSegs.Length == 6)
                        {
                            courseId = pathSegs[pathSegs.Length - 2].TrimEnd('/');
                            
                            ViewState["CourseId"] = courseId;
                        }
                        else
                            ViewState["CourseId"] = "";
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["CourseId"].ToString();
                }
                else
                    return ViewState["CourseId"].ToString();
            }
            set
            {
                ViewState["CourseId"] = value;
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
            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                string courseId = CourseId;
                if (courseId != "")
                {
                    ElioUsersTrainingCategoriesCourses course = Sql.GetTrainingCourseById(Convert.ToInt32(courseId), session);
                    if (course != null)
                    {
                        LblCourse.Text = "Analytics view for Course: " + course.CourseDescription;
                    }
                }
            }

            UpdateStrings();
            SetLinks();
        }

        private void SetLinks()
        {
            aLibrary.HRef = ControlLoader.Dashboard(vSession.User, "partner-training-analytics");
        }

        private void UpdateStrings()
        {

        }

        #endregion

        #region Buttons

        #endregion

        private DataTable RetrieveChaptersByCourseId(int courseId, DBSession session)
        {
            DataTable table = null;

            List<ElioUsersTrainingCoursesChapters> chapters = Sql.GetElioUserTrainingChaptersByCourseId(courseId, session);

            if (chapters.Count > 0)
            {
                table = new DataTable();

                table.Columns.Add("id");
                table.Columns.Add("chapter_id");
                table.Columns.Add("chapter_title");
                table.Columns.Add("chapter_file_name");
                table.Columns.Add("chapter_file_path");
                table.Columns.Add("chapter_link");
                table.Columns.Add("sysdate");
                table.Columns.Add("is_viewed");
                table.Columns.Add("date_viewed");

                foreach (ElioUsersTrainingCoursesChapters chapter in chapters)
                {
                    table.Rows.Add(chapter.CourserId, chapter.Id, chapter.ChapterTitle, "/TrainingLibrary/" + chapter.ChapterFileName, "/TrainingLibrary/" + chapter.ChapterFilePath, chapter.ChapterLink, chapter.SysDate, chapter.IsViewed, chapter.DateViewed);
                }
            }

            return table;
        }

        #region Grids

        protected void RdgElioUsers_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent Items

                    GridDataItem item = (GridDataItem)e.Item;

                    ElioUsersTrainingCategoriesCourses course = Sql.GetTrainingCourseById(Convert.ToInt32(item["id"].Text), session);
                    if (course != null)
                    {
                        Image imgPreview = (Image)ControlFinder.FindControlRecursive(item, "ImgPreview");
                        imgPreview.ImageUrl = "/TrainingLibrary/" + course.OverviewImagePath;

                        item["sysdate"].Text = Convert.ToDateTime(course.SysDate).Day + "/" + Convert.ToDateTime(course.SysDate).Month + "/" + Convert.ToDateTime(course.SysDate).Year;
                    }

                    #endregion
                }
                else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "CompanyItems")
                {
                    #region Resellers

                    GridDataItem item = (GridDataItem)e.Item;
                    int courseId = Convert.ToInt32(item["id"].Text);

                    //bool isVendor = Sql.IsUsersSpecificType(userId, Types.Vendors.ToString(), session);
                    ElioUsersTrainingCategoriesCourses course = Sql.GetTrainingCourseById(courseId, session);

                    if (item["chapter_id"].Text != "&nbsp;")
                    {
                        int chapterId = Convert.ToInt32(item["chapter_id"].Text);

                        ElioUsersTrainingCoursesChapters chapter = Sql.GetTrainingChpaterById(chapterId, session);

                        if (chapter != null)
                        {
                            HtmlAnchor aChapterFilePath = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aChapterFilePath");
                            aChapterFilePath.HRef = System.Configuration.ConfigurationManager.AppSettings["ViewTrainingLibraryPath"] + "/" + chapter.ChapterFilePath;

                            Label lblChapterFileName = (Label)ControlFinder.FindControlRecursive(item, "LblChapterFileName");
                            lblChapterFileName.Text = chapter.ChapterFileName;

                            HtmlAnchor aChapterLink = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aChapterLink");
                            aChapterLink.HRef = chapter.ChapterLink;

                            item["is_viewed"].Text = chapter.IsViewed == 1 ? "Yes" : "No";

                            item["date_viewed"].Text = chapter.DateViewed != null ? Convert.ToDateTime(chapter.DateViewed).Day + "/" + Convert.ToDateTime(chapter.DateViewed).Month + "/" + Convert.ToDateTime(chapter.DateViewed).Year : "-";
                            item["sysdate"].Text = Convert.ToDateTime(chapter.SysDate).Day + "/" + Convert.ToDateTime(chapter.SysDate).Month + "/" + Convert.ToDateTime(chapter.SysDate).Year;
                        }
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

        protected void RdgElioUsers_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioUsersTrainingCategoriesCourses> courses = Sql.GetElioUserTrainingCategoryCoursesAll(vSession.User.Id, session);

                if (courses != null && courses.Count > 0)
                {
                    RdgElioUsers.Visible = true;
                    UcMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("overview_image_path");
                    table.Columns.Add("course_description");
                    table.Columns.Add("sysdate");
                    table.Columns.Add("category_id");
                    table.Columns.Add("category_description");
                    table.Columns.Add("view_count");

                    foreach (ElioUsersTrainingCategoriesCourses course in courses)
                    {
                        table.Rows.Add(course.Id, "/TrainingLibrary/" + course.OverviewImagePath, course.CourseDescription, course.SysDate, course.CategoryId, Sql.GetElioUserTrainingCategoryDescriptionByCategoryId(course.UserId, course.CategoryId, session), 0);
                    }

                    RdgElioUsers.DataSource = table;
                }
                else
                {
                    RdgElioUsers.Visible = false;

                    string alert = "You have no courses";
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Info, true, true, false, false, false);
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

        protected void RdgElioUsers_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                session.OpenConnection();

                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "CompanyItems":
                        {
                            int courseId = Convert.ToInt32(dataItem.GetDataKeyValue("id").ToString());

                            if (courseId > 0)
                            {
                                e.DetailTableView.DataSource = RetrieveChaptersByCourseId(courseId, session);
                            }
                            else
                            {
                                e.DetailTableView.DataSource = null;
                                e.DetailTableView.Visible = false;
                            }
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

        protected void RdgElioUsers_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //RdgElioUsers.MasterTableView.Items[0].Expanded = true;
                //RdgElioUsers.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            }
        }

        #endregion

        #region Tabs

        #endregion

        #region DropDown Lists

        #endregion

        protected void aViewPartners_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                if (item["id"].Text != "")
                {
                    string url = "dashboard/" + item["id"].Text + "/course-training-analytics";
                    Response.Redirect(url, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
    }
}