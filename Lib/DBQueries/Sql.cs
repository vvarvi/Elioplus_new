using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DB;
using System.Data;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Localization;
using System.Configuration;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus.Lib.DBQueries
{
    public class Sql
    {
        public static List<ElioAnonymousIpInfo> GetAnonymousIpInfoByInsertedStatus(int isInserted, DBSession session)
        {
            DataLoader<ElioAnonymousIpInfo> loader = new DataLoader<ElioAnonymousIpInfo>(session);

            return loader.Load(@"SELECT *
                                  FROM Elio_anonymous_ip_info
                                where is_inserted = @is_inserted
                                and sysdate >= '2024-07-18'
                                and url != ''
                                and company_domain != ''
                                order by sysdate"
                            , DatabaseHelper.CreateIntParameter("@is_inserted", isInserted));
        }

        public static bool HasPartnerTraningCourse(int partnerId, int courseId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(Id) as count
                                                    FROM Elio_partners_training_courses
                                                    where partner_id = @partner_id 
                                                    and course_id = @course_id"
                                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId)
                                            , DatabaseHelper.CreateIntParameter("@course_id", courseId));

            return table != null && table.Rows.Count >= 0 ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static List<ElioUsersTrainingGroupMembers> GetElioUserTrainingGroupMembersByGroupId(int groupId, DBSession session)
        {
            DataLoader<ElioUsersTrainingGroupMembers> loader = new DataLoader<ElioUsersTrainingGroupMembers>(session);

            return loader.Load(@"Select *
                                FROM Elio_users_training_group_members
                                where id = @id
                                ORDER BY reseller_id"
                            , DatabaseHelper.CreateIntParameter("@id", groupId));
        }

        public static bool ExistIpAnonymousIpInfo(string ip, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_anonymous_ip_info with (nolock) where ip_address = @ip_address"
                                                   , DatabaseHelper.CreateStringParameter("@ip_address", ip));

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static ElioAnonymousIpInfo GetAnonymousIpInfoByIp(string ip, DBSession session)
        {
            DataLoader<ElioAnonymousIpInfo> loader = new DataLoader<ElioAnonymousIpInfo>(session);

            return loader.LoadSingle("select TOP 1 * from Elio_anonymous_ip_info with (nolock) where ip_address = @ip_address order by id desc"
                                                   , DatabaseHelper.CreateStringParameter("@ip_address", ip));
        }

        public static bool ExistDomainAnonymousCompaniesInfo(string domain, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_anonymous_companies_info with (nolock) where domain = @domain"
                                                   , DatabaseHelper.CreateStringParameter("@domain", domain));

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static ElioAnonymousCompaniesInfo GetAnonymousCompaniesInfoByDomain(string domain, DBSession session)
        {
            DataLoader<ElioAnonymousCompaniesInfo> loader = new DataLoader<ElioAnonymousCompaniesInfo>(session);

            return loader.LoadSingle("select TOP 1 * from Elio_anonymous_companies_info with (nolock) where domain = @domain"
                                                   , DatabaseHelper.CreateStringParameter("@domain", domain));
        }

        public static bool UpdateUserTrainingCategoryDescription(int userId, int categoryId, string description, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_users_training_categories 
                                    SET category_description = @category_description,
                                        last_updated = getdate()
                                WHERE user_id = @user_id AND id = @id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@id", categoryId)
                                , DatabaseHelper.CreateStringParameter("@category_description", description));

            return true;
        }

        public static ElioUsersTrainingCategoryCoursePermissions GetElioUserTrainingCoursePermissionsByCourseId(int userId, int courseId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategoryCoursePermissions> loader = new DataLoader<ElioUsersTrainingCategoryCoursePermissions>(session);

            return loader.LoadSingle(@"Select *
                                        FROM Elio_users_training_category_course_permissions
                                        where user_id = @user_id 
                                        and course_id = @course_id
                                        and is_public = 1"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateIntParameter("@course_id", courseId));
        }
        public static int GetTrainingCategoryLastId(DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT TOP 1 id
                                                      FROM Elio_users_training_categories
                                                      order by id desc");

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0][0]) : 0;
        }

        public static int GetTrainingCourseLastId(DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT TOP 1 id
                                                      FROM Elio_users_training_categories_courses
                                                      order by id desc");

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0][0]) : 0;
        }

        public static int GetTrainingChapterLastId(DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT TOP 1 id
                                                      FROM Elio_users_training_courses_chapters
                                                      order by id desc");

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0][0]) : 0;
        }

        public static ElioUsersTrainingCategories GetElioUserTrainingCategoryById(int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategories> loader = new DataLoader<ElioUsersTrainingCategories>(session);

            return loader.LoadSingle(@"Select *
                                        FROM Elio_users_training_categories
                                        where user_id = @user_id 
                                        and Id = @Id
                                        and is_public = 1"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateIntParameter("@Id", categoryId));
        }

        public static string GetElioUserTrainingCategoryDescriptionByCategoryId(int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategories> loader = new DataLoader<ElioUsersTrainingCategories>(session);

            DataTable table = session.GetDataTable(@"Select category_description
                                        FROM Elio_users_training_categories
                                        where user_id = @user_id 
                                        and Id = @Id"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateIntParameter("@Id", categoryId));

            return (table != null && table.Rows.Count > 0) ? table.Rows[0]["category_description"].ToString() : "";
        }

        public static bool ExistUserTrainingCategory(int userId, string categoryName, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count
                                                        from Elio_users_training_categories 
                                                        where user_id = @user_id 
                                                        and category_description = @category_description"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateStringParameter("@category_description", categoryName));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static ElioUsersTrainingCategories GetElioUserTrainingCategoryByName(int userId, string categoryName, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategories> loader = new DataLoader<ElioUsersTrainingCategories>(session);

            return loader.LoadSingle(@"Select *
                                        FROM Elio_users_training_categories
                                        where user_id = @user_id 
                                        and category_description = @category_description
                                        and is_public = 1"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateStringParameter("@category_description", categoryName));
        }

        public static List<ElioUsersTrainingCategories> GetElioUserTrainingCategories(int userId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategories> loader = new DataLoader<ElioUsersTrainingCategories>(session);

            return loader.Load(@"Select *
                                FROM Elio_users_training_categories
                                where user_id = @user_id
                                and is_public = 1"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersTrainingCoursesChapters> GetElioUserTrainingChaptersByCourseId(int courseId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCoursesChapters> loader = new DataLoader<ElioUsersTrainingCoursesChapters>(session);

            return loader.Load(@"Select *
                                FROM Elio_users_training_courses_chapters
                                where course_id = @course_id
                                and is_public = 1"
                            , DatabaseHelper.CreateIntParameter("@course_id", courseId));
        }

        public static DataTable GetElioUserTrainingCategoriesTbl(int userId, DBSession session)
        {
            return session.GetDataTable(@"SELECT *  
                                         FROM Elio_users_training_categories 
                                         WHERE user_id = @user_id
                                         AND is_public = 1
                                         ORDER BY category_description"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersTrainingCategoriesCourses> GetElioUserTrainingCategoryCourses(int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategoriesCourses> loader = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);

            return loader.Load(@"Select *
                                    FROM Elio_users_training_categories_courses
                                    where user_id = @user_id 
                                    and category_id = @category_id
                                    and is_public = 1
                                order by course_description"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateIntParameter("@category_id", categoryId));
        }

        public static List<ElioUsersTrainingCategoriesCourses> GetElioPartnerTrainingCategoryCourses(int vendorId, int partnerId, int categoryId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategoriesCourses> loader = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);

            return loader.Load(@"Select uc.*
                                    FROM Elio_users_training_categories_courses uc
                                    inner join Elio_partners_training_courses pc
	                                    on uc.user_id = pc.vendor_id
	                                    and uc.id = pc.course_id
                                    where uc.user_id = @user_id 
                                    and pc.partner_id = @partner_id
                                    and uc.category_id = @category_id
                                    and pc.is_public = 1
                                order by course_description"
                            , DatabaseHelper.CreateIntParameter("@user_id", vendorId)
                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId)
                            , DatabaseHelper.CreateIntParameter("@category_id", categoryId));
        }

        public static List<ElioUsersTrainingCategoriesCourses> GetElioUserTrainingCategoryCoursesAll(int userId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategoriesCourses> loader = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);

            return loader.Load(@"Select *
                                    FROM Elio_users_training_categories_courses
                                    where user_id = @user_id
                                    and is_public = 1
                                order by course_description"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersTrainingCategoriesCourses> GetElioPartnerTrainingCategoryCoursesAll(int vendorId, int partnerId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategoriesCourses> loader = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);

            return loader.Load(@"Select uc.*
                                FROM Elio_users_training_categories_courses uc
                                inner join Elio_partners_training_courses pc
	                                on uc.user_id = pc.vendor_id
	                                and uc.id = pc.course_id
                                where uc.user_id = @vendorId
                                and pc.partner_id = @partner_id
                                and pc.is_public = 1
                                order by course_description"
                            , DatabaseHelper.CreateIntParameter("@vendorId", vendorId)
                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId));
        }

        public static ElioUsersTrainingCategoriesCourses GetTrainingCategoryCourseByCourseId(int courseId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategoriesCourses> loader = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);

            return loader.LoadSingle(@"Select *
                                    FROM Elio_users_training_categories_courses
                                    where id = @id
                                    and is_public = 1
                                order by course_description"
                            , DatabaseHelper.CreateIntParameter("@id", courseId));
        }

        public static ElioUsersTrainingCategoriesCourses GetPartnerTrainingCategoryCourseByCourseId(int partnerId, int courseId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategoriesCourses> loader = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);

            return loader.LoadSingle(@"Select uc.id
                                          ,uc.user_id
                                          ,uc.category_id
                                          ,uc.course_description
                                          ,uc.overview_text
                                          ,uc.overview_image_path
                                          ,uc.overview_image_type
                                          ,uc.overview_image_name
	                                      ,pc.partner_id
	                                      ,pc.is_public
	                                      ,pc.is_new
	                                      ,pc.sysdate
	                                      --,pc.date_viewed
	                                      ,pc.last_updated
                                    FROM Elio_users_training_categories_courses uc
                                    inner join Elio_partners_training_courses pc
	                                    on uc.user_id = pc.vendor_id
	                                    and uc.id = pc.course_id
	                                    and uc.category_id = pc.category_id
                                    where 1 = 1
                                    and pc.partner_id = @partner_id
                                    and pc.is_public = 1
                                    and pc.course_id = @course_id
                                    order by course_description"
                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId)
                            , DatabaseHelper.CreateIntParameter("@course_id", courseId));
        }


        public static List<ElioUsersTrainingCoursesChapters> GetElioUserTrainingCourseChapters(int userId, int categoryId, int courseId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCoursesChapters> loader = new DataLoader<ElioUsersTrainingCoursesChapters>(session);

            return loader.Load(@"Select *
                                    FROM Elio_users_training_courses_chapters
                                    where user_id = @user_id 
                                    and category_id = @category_id
                                    and course_id = @course_id
                                    and is_public = 1"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateIntParameter("@category_id", categoryId)
                            , DatabaseHelper.CreateIntParameter("@course_id", courseId));
        }

        public static List<ElioUsersTrainingCoursesChapters> GetPartnerTrainingCourseChapters(int partnerId, int categoryId, int courseId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCoursesChapters> loader = new DataLoader<ElioUsersTrainingCoursesChapters>(session);

            return loader.Load(@"SELECT cc.id, cc.user_id,cc.category_id,cc.course_id,cc.chapter_title,cc.chapter_text,cc.chapter_file_path,cc.chapter_file_name,cc.chapter_link
		                                ,tc.partner_id,tc.is_public,tc.is_viewed,tc.sysdate,tc.date_viewed,tc.last_updated
                                FROM Elio_users_training_courses_chapters cc
                                INNER JOIN Elio_partners_training_chapters tc
	                            ON cc.course_id = tc.course_id
		                            AND cc.id = tc.chapter_id
		                            AND cc.user_id = tc.vendor_id
                                where 1 = 1
                                and tc.partner_id = @partner_id
                                and cc.category_id = @category_id
                                and tc.course_id = @course_id
                                and tc.is_public = 1"
                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId)
                            , DatabaseHelper.CreateIntParameter("@category_id", categoryId)
                            , DatabaseHelper.CreateIntParameter("@course_id", courseId));
        }

        public static ElioUsersTrainingCoursesChapters GetTrainingNextChapter(int partnerId, int courseId, int chapterId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCoursesChapters> loader = new DataLoader<ElioUsersTrainingCoursesChapters>(session);

            return loader.LoadSingle(@"Select TOP 1 cc.id, cc.user_id,cc.category_id,cc.course_id,cc.chapter_title,cc.chapter_text,cc.chapter_file_path,cc.chapter_file_name,cc.chapter_link
		                                ,tc.partner_id,tc.is_public,tc.is_viewed,tc.sysdate,tc.date_viewed,tc.last_updated
                                      FROM Elio_users_training_courses_chapters cc
                                      INNER JOIN Elio_partners_training_chapters tc
	                                    ON cc.course_id = tc.course_id
		                                    AND cc.id = tc.chapter_id
		                                    AND cc.user_id = tc.vendor_id
                                            where tc.course_id = @course_id
                                            and tc.partner_id = @partner_id
                                            and tc.is_public = 1
                                            and tc.chapter_id > @chapter_id
                                            order by tc.chapter_id"
                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId)
                            , DatabaseHelper.CreateIntParameter("@chapter_id", chapterId)
                            , DatabaseHelper.CreateIntParameter("@course_id", courseId));
        }

        public static void SetTrainingChapterViewed(int parterId, int courseId, int chapterId, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_partners_training_chapters
                                        SET is_viewed = 1, date_viewed = getdate(), last_updated = getdate()
                                    where course_id = @course_id
                                    and partner_id = @partner_id
                                    and is_public = 1
                                    and chapter_id = @chapter_id"
                            , DatabaseHelper.CreateIntParameter("@chapter_id", chapterId)
                            , DatabaseHelper.CreateIntParameter("@partner_id", parterId)
                            , DatabaseHelper.CreateIntParameter("@course_id", courseId));
        }

        public static void SetTrainingCourseNotNew(int partnerId, int courseId, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_partners_training_courses
                                        SET is_new = 0, last_updated = getdate()
                                    where course_id = @course_id
                                    and partner_id = @partner_id"
                            , DatabaseHelper.CreateIntParameter("@course_id", courseId)
                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId));
        }

        public static void SetTrainingChaptersViewedByCourse(int partnerId, int courseId, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_partners_training_chapters
                                        SET is_viewed = 1, date_viewed = getdate(), last_updated = getdate()
                                    where course_id = @course_id
                                    and partner_id = @partner_id
                                    and is_public = 1
                                    and is_viewed = 0"
                            , DatabaseHelper.CreateIntParameter("@course_id", courseId)
                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId));
        }

        public static bool IsTrainingChapterLast(int partnerId, int courseId, int chapterId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(id) as count
                                    FROM Elio_partners_training_chapters
                                    where course_id = @course_id
                                    and is_public = 1
                                    and partner_id = @partner_id
                                    and chapter_id > @chapter_id
                                    group by id
                                    order by id"
                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId)
                            , DatabaseHelper.CreateIntParameter("@chapter_id", chapterId)
                            , DatabaseHelper.CreateIntParameter("@course_id", courseId));

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) == 0 : true;
        }

        public static ElioUsersTrainingCoursesChapters GetTrainingPreviousChapter(int partnerId, int courseId, int chapterId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCoursesChapters> loader = new DataLoader<ElioUsersTrainingCoursesChapters>(session);

            return loader.LoadSingle(@"Select TOP 1 cc.id, cc.user_id,cc.category_id,cc.course_id,cc.chapter_title,cc.chapter_text,cc.chapter_file_path,cc.chapter_file_name,cc.chapter_link
		                                ,tc.partner_id,tc.is_public,tc.is_viewed,tc.sysdate,tc.date_viewed,tc.last_updated
                                      FROM Elio_users_training_courses_chapters cc
                                      INNER JOIN Elio_partners_training_chapters tc
	                                    ON cc.course_id = tc.course_id
		                                    AND cc.id = tc.chapter_id
		                                    AND cc.user_id = tc.vendor_id
                                            where tc.course_id = @course_id
                                            and tc.partner_id = @partner_id
                                            and tc.is_public = 1
                                    and chapter_id < @chapter_id
                                    and partner_id = @partner_id
                                    order by id desc"
                            , DatabaseHelper.CreateIntParameter("@chapter_id", chapterId)
                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId)
                            , DatabaseHelper.CreateIntParameter("@course_id", courseId));
        }

        public static ElioUsersTrainingCoursesChapters GetTrainingChapterById(int chapterId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCoursesChapters> loader = new DataLoader<ElioUsersTrainingCoursesChapters>(session);

            return loader.LoadSingle(@"Select TOP 1 *
                                    FROM Elio_users_training_courses_chapters
                                    where id = @id"
                            , DatabaseHelper.CreateIntParameter("@id", chapterId));
        }

        public static List<ElioUsersTrainingCoursesChaptersIJCourses> GetElioUserTrainingCategoryChaptersIJCourses(int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCoursesChaptersIJCourses> loader = new DataLoader<ElioUsersTrainingCoursesChaptersIJCourses>(session);

            return loader.Load(@"SELECT ch.id
                                  ,ch.user_id
                                  ,ch.category_id
                                  ,ch.course_id
	                              ,c.course_description
                                  ,ch.chapter_title
                                  ,ch.chapter_text
                                  ,ch.chapter_file_path
                                  ,ch.chapter_file_name
                                  ,ch.chapter_link
                              FROM Elio_users_training_courses_chapters ch
                              inner join Elio_users_training_categories_courses c
	                            on ch.course_id = c.id
                            where ch.user_id = @user_id
                            and ch.category_id = @category_id
                            order by ch.course_id"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateIntParameter("@category_id", categoryId));
        }

        public static ElioUsersTrainingCategoriesCourses GetElioUserTrainingCategoryCourseByName(int userId, int categoryId, string courseName, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategoriesCourses> loader = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);

            return loader.LoadSingle(@"Select *
                                    FROM Elio_users_training_categories_courses
                                    where user_id = @user_id 
                                    and category_id = @category_id
                                    and course_description = @course_description"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateIntParameter("@category_id", categoryId)
                            , DatabaseHelper.CreateStringParameter("@course_description", courseName));
        }

        public static ElioUsersTrainingCategoriesCourses GetTrainingCourseById(int courseId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCategoriesCourses> loader = new DataLoader<ElioUsersTrainingCategoriesCourses>(session);

            return loader.LoadSingle(@"Select *
                                    FROM Elio_users_training_categories_courses
                                    where id = @id"
                            , DatabaseHelper.CreateIntParameter("@id", courseId));
        }

        public static ElioUsersTrainingCoursesChapters GetTrainingChpaterById(int chapterId, DBSession session)
        {
            DataLoader<ElioUsersTrainingCoursesChapters> loader = new DataLoader<ElioUsersTrainingCoursesChapters>(session);

            return loader.LoadSingle(@"Select *
                                    FROM Elio_users_training_courses_chapters
                                    where id = @id"
                            , DatabaseHelper.CreateIntParameter("@id", chapterId));
        }

        public static bool HasUserTraningChaptersByCategory(int userId, int categoryId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(Id) as count
                                                    FROM Elio_users_training_courses_chapters
                                                    where user_id = @user_id 
                                                    and category_id = @category_id
                                                    and is_public = 1"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@category_id", categoryId));

            return table != null && table.Rows.Count >= 0 ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static bool HasUserTraningChaptersByCourse(int userId, int courseId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(Id) as count
                                                    FROM Elio_users_training_courses_chapters
                                                    where user_id = @user_id 
                                                    and course_id = @course_id
                                                    and is_public = 1"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@course_id", courseId));

            return table != null && table.Rows.Count >= 0 ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static ElioSchedulerNotificationEmails GetElioSchedulerNotificationEmailById(int id, DBSession session)
        {
            DataLoader<ElioSchedulerNotificationEmails> loader = new DataLoader<ElioSchedulerNotificationEmails>(session);

            return loader.LoadSingle(@"Select *
                                        FROM Elio_scheduler_notification_emails WITH (NOLOCK)
                                        where is_active = 1 
                                        and id = @id"
                            , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static bool SetUSerCheckoutSessionsExpired(int userId, DBSession session)
        {
            int rows = session.ExecuteQuery(@"UPDATE Stripe_users_checkout_sessions
                                                        SET is_expired = 1
                                                        , last_update = getdate()
                                                        where user_id = @user_id 
                                                        and is_expired = 0"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return rows >= 0 ? true : false;
        }

        public static StripeUsersCheckoutSessions GetUserStripeCheckoutSession(int userId, int isExpired, DBSession session)
        {
            DataLoader<StripeUsersCheckoutSessions> loader = new DataLoader<StripeUsersCheckoutSessions>(session);
            return loader.LoadSingle(@"Select TOP 1 * from Stripe_users_checkout_sessions with (nolock) 
                where user_id = @user_id 
                and is_expired = @is_expired
                order by date_created desc"
                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                , DatabaseHelper.CreateIntParameter("@is_expired", isExpired));
        }

        public static StripeUsersCheckoutSessions GetUserStripeCheckoutSessionLast(int userId, DBSession session)
        {
            DataLoader<StripeUsersCheckoutSessions> loader = new DataLoader<StripeUsersCheckoutSessions>(session);
            return loader.LoadSingle(@"Select TOP 1 * from Stripe_users_checkout_sessions with (nolock) 
                where user_id = @user_id 
                order by date_created desc"
                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetUserFeedsByCaseTable(ElioUsers user, int caseId, DBSession session)
        {
            DataTable table = null;

            if (user.CompanyType == Types.Vendors.ToString())
            {
                switch (caseId)
                {
                    case 0: //ALL

                        table = GetUserFeedsTable(user, session);

                        break;

                    case 2: //NEW DEALS

                        table = session.GetDataTable(@"SELECT 'Deal Registration' as title, 'A new deal has been added by ' + u.company_name + ' at ' + cast(cast(rd.created_date as date) as nvarchar(50)) + '. View the details Accept or Reject the deal.' as content,rd.created_date
, 'flaticon-notepad' as icon
FROM Elio_registration_deals rd
inner join elio_users u
	on rd.reseller_id = u.id
WHERE 1 = 1
AND rd.is_public = 1
AND is_active = 1
AND vendor_id = @user_id
order by rd.created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 3: //CLOSED LEADS

                        table = session.GetDataTable(@"SELECT 'Lead Distribution' as title, 'A lead has been ' + lead_result + ' by ' + u.company_name + ' at ' + cast(cast(ld.created_date as date) as nvarchar(50)) + '. Go to Lead Distribution tab to view the details.' as content,ld.created_date
, 'flaticon2-digital-marketing' as icon
FROM Elio_lead_distributions ld
inner join elio_users u
	on ld.reseller_id = u.id
WHERE 1 = 1
AND ld.is_public = 1
AND status = 2 
AND lead_result = 'Won'
AND vendor_id = @user_id
order by ld.created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 4: //COLL MESSAGES

                        table = session.GetDataTable(@"SELECT 'Collaboration Messages' as title, 'You have received new messages. Got to Collaborate tab to view and respond.' as content, date_created as created_date
, 'flaticon-chat' as icon
FROM Elio_collaboration_users_mailbox cum
INNER JOIN Elio_collaboration_mailbox cm
	ON cm.id = cum.mailbox_id
WHERE 1 = 1                              
AND cm.is_public = 1
AND cum.master_user_id = @user_id 
AND is_new = 1 
AND is_viewed = 0 
AND cm.user_id != @user_id 
AND cum.is_deleted = 0 
AND cum.is_public = 1
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 5: //NEW FILES

                        table = session.GetDataTable(@"SELECT 'Library Assets' as title, 'You have received new files sent by ' + u.company_name + '. Go to your Library to view the files. ' as content, date_created as created_date
, 'flaticon-download-1' as icon
FROM Elio_collaboration_users_library_files lf
inner join elio_users u
	on lf.uploaded_by_user_id = u.id
WHERE user_id = @user_id 
AND uploaded_by_user_id <> @user_id 
AND is_new = 1
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 6: //PENDING REQUESTS

                        table = session.GetDataTable(@"SELECT 'New Partner Requests' as title, 'There is a new partner request by ' + u.company_name + ' to join your partner portal. Visit the Partner Directory tab to Accept or Delete the request.' as content, date_created as created_date
, 'flaticon-users-1' as icon
FROM Elio_collaboration_vendors_resellers cvr
inner join elio_users u
	on cvr.partner_user_id = u.id
inner join Elio_collaboration_vendor_reseller_invitations 
on Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id = cvr.id 
inner join Elio_collaboration_users_invitations
	on Elio_collaboration_users_invitations.id = Elio_collaboration_vendor_reseller_invitations.user_invitation_id
WHERE 1 = 1 
    AND invitation_status = 'Pending' 
    AND cvr.is_active = 1 
    AND Elio_collaboration_users_invitations.is_public = 1
	AND Elio_collaboration_vendor_reseller_invitations.user_id <> @user_id
	AND master_user_id = @user_id
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 7: //CONFIRMED REQUESTS

                        table = session.GetDataTable(@"SELECT 'New Partners Joined' as title, u.company_name + ' has accepted your invitation and joined the partner portal. Go to your Partner Directory to view more details.' as content, cvr.last_updated as created_date
, 'flaticon-user-add' as icon
FROM Elio_collaboration_vendors_resellers cvr
inner join elio_users u
	on cvr.partner_user_id = u.id
inner join Elio_collaboration_vendor_reseller_invitations 
on Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id = cvr.id 
inner join Elio_collaboration_users_invitations
	on Elio_collaboration_users_invitations.id = Elio_collaboration_vendor_reseller_invitations.user_invitation_id
WHERE 1 = 1 
    AND invitation_status = 'Confirmed' 
    AND cvr.is_active = 1 
    AND Elio_collaboration_users_invitations.is_public = 1
	AND Elio_collaboration_vendor_reseller_invitations.user_id = @user_id
	AND master_user_id = @user_id
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 8: //NEW INBOX MESSAGES

                        table = session.GetDataTable(@"SELECT 'Messages' as title, 'You have received a new message, view it on your Messages tab.' as content, cum.last_updated as created_date
, 'flaticon-multimedia' as icon
FROM Elio_collaboration_users_mailbox  cum
INNER JOIN Elio_collaboration_mailbox cm
	ON cm.id = cum.mailbox_id
WHERE 1 = 1                              
AND cm.is_public = 1
AND cum.master_user_id = @user_id
AND is_new = 1
AND is_viewed = 0 
AND cm.user_id != @user_id 
AND cum.is_deleted = 0 
AND cum.is_public = 1
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 9: //PENDING COMMISSIONS

                        table = session.GetDataTable(@"SELECT 'Commissions' as title, 'A new payout of ' + cast(replace(amount, '.00', '') as nvarchar(10)) + ' ' + cc.currency_symbol + ' has been scheduled to be transferred in ' + convert(varchar, dateadd(day, 30, ld.last_update), 23) + ' to your partner, ' + u.company_name + '.' as content, ld.last_update as created_date
, 'flaticon-notepad' as icon
FROM Elio_lead_distributions ld 
INNER join Elio_collaboration_vendors_resellers cvr
on cvr.master_user_id = ld.vendor_id
AND cvr.partner_user_id = ld.reseller_id
and cvr.is_active = 1
INNER JOIN Elio_currencies_countries cc
ON cc.cur_id = ld.cur_id
left joiN Elio_tier_management_users_settings tmus
on tmus.user_id = ld.vendor_id
and tmus.description = cvr.tier_status
INNER JOIN elio_users u 
ON u.id = ld.reseller_id
WHERE 1 = 1
AND ld.is_public = 1
AND ld.status = 2
AND lead_result = 'Won'
AND payment_status = 0 
AND vendor_id = @user_id
and dateadd(day, 30, ld.last_update) > getdate()
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 10: //PAYOUTS

                        table = session.GetDataTable(@"SELECT 'Payouts' as title, 'A payout of ' + cast(replace(amount, '.00', '') as nvarchar(10)) + ' ' + cc.currency_symbol + ' has been successfully transferred at ' + convert(varchar, ld.last_update, 23) + ' to your partner, ' + u.company_name + '. Visit your Commissions tab to view more details.' as content, ld.last_update as created_date
, 'flaticon-notepad' as icon
FROM Elio_lead_distributions ld 
INNER join Elio_collaboration_vendors_resellers cvr
on cvr.master_user_id = ld.vendor_id
AND cvr.partner_user_id = ld.reseller_id
and cvr.is_active = 1
INNER JOIN Elio_currencies_countries cc
ON cc.cur_id = ld.cur_id
left joiN Elio_tier_management_users_settings tmus
on tmus.user_id = ld.vendor_id
and tmus.description = cvr.tier_status
INNER JOIN elio_users u 
ON u.id = ld.reseller_id
WHERE 1 = 1
AND ld.is_public = 1
AND ld.status = 2
AND lead_result = 'Won'
AND payment_status = 1 
AND vendor_id = @user_id
and dateadd(day, 30, ld.last_update) <= getdate()
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 11: //POSTS

                        table = session.GetDataTable(@"SELECT 'New Post by ' + u.company_name as title, post_message as content,date_created as created_date
, 'flaticon-notepad' as icon
FROM Elio_users_posts p
inner join elio_users u
		on u.id = p.user_id
WHERE 1 = 1
AND p.is_public = 1
AND p.user_id = @user_id
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    default:

                        table = GetUserFeedsTable(user, session);

                        break;
                }
            }
            else
            {
                switch (caseId)
                {
                    case 0: //ALL

                        table = GetUserFeedsTable(user, session);

                        break;

                    case 2: //APPROVED/REJECTED DEALS

                        table = session.GetDataTable(@"SELECT 'Deal Registration' as title, 'A deal you have submitted has been Accepted/Rejects by ' + u2.company_name + ' at ' + cast(cast(rd.last_update as date) as nvarchar(50)) + '. Go to your Deal Registration tab to manage the deal.' as content,rd.last_update as created_date
, 'flaticon-notepad' as icon
FROM Elio_registration_deals rd
inner join elio_users u
	on rd.reseller_id = u.id
	and u.company_type = 'Channel Partners'
inner join elio_users u2
	on rd.vendor_id = u2.id
	and u2.company_type = 'Vendors'
WHERE 1 = 1
AND rd.is_public = 1
AND is_active IN (1,-1)
AND status = 2 
AND deal_result = 'Pending'
AND is_new = 0
AND reseller_id = @user_id
order by rd.last_update desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 3: //NEW LEADS

                        table = session.GetDataTable(@"SELECT 'Lead Distribution' as title, 'You have received a new lead by ' + u.company_name + ' at ' + cast(cast(ld.created_date as date) as nvarchar(50)) + '. Visit the Lead Distribution tab to view the details.' as content,ld.created_date
, 'flaticon2-digital-marketing' as icon
FROM Elio_lead_distributions ld
inner join elio_users u
	on ld.vendor_id = u.id
WHERE 1 = 1
AND ld.is_public = 1
AND status = 1 
AND lead_result = 'Pending'
AND is_new = 1
AND reseller_id = @user_id
order by ld.created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 4: //COLL MESSAGES

                        table = session.GetDataTable(@"SELECT 'Collaboration Messages' as title, 'You have received new messages. Got to Collaborate tab to view and respond.' as content, date_created as created_date
, 'flaticon-chat' as icon
FROM Elio_collaboration_mailbox cm
INNER JOIN Elio_collaboration_users_mailbox cum
ON cm.id =  cum.mailbox_id
inner join Elio_collaboration_vendors_resellers cvr
	on cvr.partner_user_id = cum.partner_user_id
	and cvr.master_user_id = cum.master_user_id
	and cvr.invitation_status = 'Confirmed'
where 1 = 1 
AND cum.partner_user_id = @user_id
AND is_viewed = 0
AND cm.user_id != @user_id 
AND cum.is_deleted = 0 
AND cum.is_public = 1
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 5: //NEW FILES

                        table = session.GetDataTable(@"SELECT 'Library Assets' as title, 'You have received new files sent by ' + u.company_name + '. Go to your Library to view the files. ' as content, date_created as created_date
, 'flaticon-download-1' as icon
FROM Elio_collaboration_users_library_files lf
inner join elio_users u
	on lf.uploaded_by_user_id = u.id
WHERE user_id = @user_id 
AND uploaded_by_user_id <> @user_id 
AND is_new = 1
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 6: //PENDING REQUESTS

                        table = session.GetDataTable(@"SELECT 'New Partner Requests' as title, 'There is a new partner request by ' + u.company_name + ' to join your partner portal. Visit the Partner Directory tab to Accept or Delete the request.' as content, date_created as created_date
, 'flaticon-users-1' as icon
FROM Elio_collaboration_vendors_resellers cvr
inner join elio_users u
	on cvr.partner_user_id = u.id
inner join Elio_collaboration_vendor_reseller_invitations 
on Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id = cvr.id 
inner join Elio_collaboration_users_invitations
	on Elio_collaboration_users_invitations.id = Elio_collaboration_vendor_reseller_invitations.user_invitation_id
WHERE 1 = 1 
    AND invitation_status = 'Pending' 
    AND cvr.is_active = 1 
    AND Elio_collaboration_users_invitations.is_public = 1
	AND Elio_collaboration_vendor_reseller_invitations.user_id <> @user_id
	AND master_user_id = @user_id
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 7: //CONFIRMED REQUESTS

                        table = session.GetDataTable(@"SELECT 'New Partners Joined' as title, u.company_name + ' has accepted your invitation and joined the partner portal. Go to your Partner Directory to view more details.' as content, cvr.last_updated as created_date
, 'flaticon-user-add' as icon
FROM Elio_collaboration_vendors_resellers cvr
inner join elio_users u
	on cvr.partner_user_id = u.id
inner join Elio_collaboration_vendor_reseller_invitations 
on Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id = cvr.id 
inner join Elio_collaboration_users_invitations
	on Elio_collaboration_users_invitations.id = Elio_collaboration_vendor_reseller_invitations.user_invitation_id
WHERE 1 = 1 
    AND invitation_status = 'Confirmed' 
    AND cvr.is_active = 1 
    AND Elio_collaboration_users_invitations.is_public = 1
	AND Elio_collaboration_vendor_reseller_invitations.user_id = @user_id
	AND master_user_id = @user_id
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 8: //NEW INBOX MESSAGES

                        table = session.GetDataTable(@"SELECT 'Messages' as title, 'You have received a new message, view it on your Messages tab.' as content, cum.last_updated as created_date
, 'flaticon-multimedia' as icon
FROM Elio_collaboration_users_mailbox  cum
INNER JOIN Elio_collaboration_mailbox cm
	ON cm.id = cum.mailbox_id
WHERE 1 = 1                              
AND cm.is_public = 1
AND cum.master_user_id = @user_id
AND is_new = 1
AND is_viewed = 0 
AND cm.user_id != @user_id 
AND cum.is_deleted = 0 
AND cum.is_public = 1
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 9: //PENDING COMMISSIONS

                        table = session.GetDataTable(@"SELECT 'Commissions' as title, 'A new payout of ' + cast(replace(amount, '.00', '') as nvarchar(10)) + ' ' + cc.currency_symbol + ' has been scheduled to be transferred in ' + convert(varchar, dateadd(day, 30, ld.last_update), 23) + ' to your partner, ' + u.company_name + '.' as content, ld.last_update as created_date
, 'flaticon-notepad' as icon
FROM Elio_lead_distributions ld 
INNER join Elio_collaboration_vendors_resellers cvr
on cvr.master_user_id = ld.vendor_id
AND cvr.partner_user_id = ld.reseller_id
and cvr.is_active = 1
INNER JOIN Elio_currencies_countries cc
ON cc.cur_id = ld.cur_id
left joiN Elio_tier_management_users_settings tmus
on tmus.user_id = ld.vendor_id
and tmus.description = cvr.tier_status
INNER JOIN elio_users u 
ON u.id = ld.reseller_id
WHERE 1 = 1
AND ld.is_public = 1
AND ld.status = 2
AND lead_result = 'Won'
AND payment_status = 0 
AND vendor_id = @user_id
and dateadd(day, 30, ld.last_update) > getdate()
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 10: //PAYOUTS

                        table = session.GetDataTable(@"SELECT 'Payouts' as title, 'A payout of ' + cast(replace(amount, '.00', '') as nvarchar(10)) + ' ' + cc.currency_symbol + ' has been successfully transferred at ' + convert(varchar, ld.last_update, 23) + ' to your partner, ' + u.company_name + '. Visit your Commissions tab to view more details.' as content, ld.last_update as created_date
, 'flaticon-notepad' as icon
FROM Elio_lead_distributions ld 
INNER join Elio_collaboration_vendors_resellers cvr
on cvr.master_user_id = ld.vendor_id
AND cvr.partner_user_id = ld.reseller_id
and cvr.is_active = 1
INNER JOIN Elio_currencies_countries cc
ON cc.cur_id = ld.cur_id
left joiN Elio_tier_management_users_settings tmus
on tmus.user_id = ld.vendor_id
and tmus.description = cvr.tier_status
INNER JOIN elio_users u 
ON u.id = ld.reseller_id
WHERE 1 = 1
AND ld.is_public = 1
AND ld.status = 2
AND lead_result = 'Won'
AND payment_status = 1 
AND vendor_id = @user_id
and dateadd(day, 30, ld.last_update) <= getdate()
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 11: //POSTS

                        table = session.GetDataTable(@"    SELECT 'New Post by ' + u.company_name as title, post_message as content,date_created as created_date
	, 'flaticon-notepad' as icon
    FROM Elio_users_posts p
	inner join elio_users u
		on u.id = p.user_id
	inner join Elio_collaboration_vendors_resellers cvr
		on cvr.master_user_id = u.id
		and cvr.master_user_id = p.user_id
		and cvr.partner_user_id = @user_id
		and cvr.invitation_status = 'Confirmed'
    WHERE 1 = 1
    AND p.is_public = 1
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    case 12:    //ONBOARDING FILES

                        table = session.GetDataTable(@"SELECT 'Onboarding Resources' as title, u.company_name +' has uploaded new material at ' + cast(cast(ulf.date_created as date) as nvarchar(50)) + '. View all documents on your Onboarding Resources tab.' as content,ulf.date_created AS created_date
	, 'flaticon-chat' as icon
	FROM Elio_onboarding_users_library_files ulf
	inner join elio_users u
		on ulf.user_id = u.id
	where user_id in
	(
		select master_user_id 
		from Elio_collaboration_vendors_resellers cvr
		where invitation_status = 'Confirmed'
		and partner_user_id = @user_id
		and is_active = 1
	)
	and ulf.is_new = 1
	and ulf.is_public = 1
order by created_date desc"
                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id));

                        break;

                    default:

                        table = GetUserFeedsTable(user, session);

                        break;
                }
            }

            return table;
        }

        public static DataTable GetUserFeedsTable(ElioUsers user, DBSession session)
        {
            if (user.CompanyType == Types.Vendors.ToString())
            {
                return session.GetDataTable(@"
;with new_deals
as
(
SELECT 'Deal Registration' as title, 'A new deal has been added by ' + u.company_name + ' at ' + cast(cast(rd.created_date as date) as nvarchar(50)) + '. View the details Accept or Reject the deal.' as content,rd.created_date
, 'flaticon-notepad' as icon
FROM Elio_registration_deals rd
inner join elio_users u
	on rd.reseller_id = u.id
WHERE 1 = 1
AND rd.is_public = 1
AND is_active = 1
--AND status = 1 
--AND deal_result = 'Pending'
--AND is_new = 1
AND vendor_id = @user_id
)

,close_deals
as
(
SELECT 'Deal Registration' as title, 'A deal has been ' + deal_result + ' by ' + u.company_name + ' at ' + cast(cast(rd.created_date as date) as nvarchar(50)) + '. Go to Deal Registration tab to view the details.' as content,rd.created_date
, 'flaticon-notepad' as icon
FROM Elio_registration_deals rd
inner join elio_users u
	on rd.reseller_id = u.id
WHERE 1 = 1
AND rd.is_public = 1
AND is_active = 1
AND status = 2 
--AND deal_result = 'Pending'
--AND is_new = 1
AND vendor_id = @user_id
)

,close_leads
as
(
SELECT 'Lead Distribution' as title, 'A lead has been ' + lead_result + ' by ' + u.company_name + ' at ' + cast(cast(ld.created_date as date) as nvarchar(50)) + '. Go to Lead Distribution tab to view the details.' as content,ld.created_date
, 'flaticon2-digital-marketing' as icon
FROM Elio_lead_distributions ld
inner join elio_users u
	on ld.reseller_id = u.id
WHERE 1 = 1
AND ld.is_public = 1
AND status = 2 
AND lead_result = 'Won'
--AND is_new = 1
AND vendor_id = @user_id
)

, col_messages
as
(
SELECT 'Collaboration Messages' as title, 'You have received new messages. Got to Collaborate tab to view and respond.' as content, date_created as created_date
, 'flaticon-chat' as icon
    FROM Elio_collaboration_users_mailbox cum
    INNER JOIN Elio_collaboration_mailbox cm
	    ON cm.id = cum.mailbox_id
    WHERE 1 = 1                              
    AND cm.is_public = 1
	AND cum.master_user_id = @user_id
	AND is_new = 1 
AND is_viewed = 0 
AND cm.user_id != @user_id 
AND cum.is_deleted = 0 
AND cum.is_public = 1
)

, new_files
as
(
SELECT 
'Library Assets' as title, 'You have received new files sent by ' + u.company_name + '. Go to your Library to view the files. ' as content, date_created as created_date
, 'flaticon-download-1' as icon	 
FROM Elio_collaboration_users_library_files lf
inner join elio_users u
	on lf.uploaded_by_user_id = u.id
WHERE user_id = @user_id 
AND uploaded_by_user_id <> @user_id 
AND is_new = 1
)
, pending_request
as
(
SELECT 
'New Partner Requests' as title, 'There is a new partner request by ' + u.company_name + ' to join your partner portal. Visit the Partner Directory tab to Accept or Delete the request.' as content, date_created as created_date
, 'flaticon-users-1' as icon
FROM Elio_collaboration_vendors_resellers cvr
inner join elio_users u
	on cvr.partner_user_id = u.id
inner join Elio_collaboration_vendor_reseller_invitations 
on Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id = cvr.id 
inner join Elio_collaboration_users_invitations
	on Elio_collaboration_users_invitations.id = Elio_collaboration_vendor_reseller_invitations.user_invitation_id
WHERE 1 = 1 
    AND invitation_status = 'Pending' 
    AND cvr.is_active = 1 
    AND Elio_collaboration_users_invitations.is_public = 1
	AND Elio_collaboration_vendor_reseller_invitations.user_id <> @user_id
	AND master_user_id = @user_id
)

, confirmed_request
as
(
SELECT 
'New Partners Joined' as title, u.company_name + ' has accepted your invitation and joined the partner portal. Go to your Partner Directory to view more details.' as content, cvr.last_updated as created_date
, 'flaticon-user-add' as icon
FROM Elio_collaboration_vendors_resellers cvr
inner join elio_users u
	on cvr.partner_user_id = u.id
inner join Elio_collaboration_vendor_reseller_invitations 
on Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id = cvr.id 
inner join Elio_collaboration_users_invitations
	on Elio_collaboration_users_invitations.id = Elio_collaboration_vendor_reseller_invitations.user_invitation_id
WHERE 1 = 1 
    AND invitation_status = 'Confirmed' 
    AND cvr.is_active = 1 
    AND Elio_collaboration_users_invitations.is_public = 1
	AND Elio_collaboration_vendor_reseller_invitations.user_id = @user_id
	AND master_user_id = @user_id
)

, new_inbox_msgs
as
(
SELECT 
'Messages' as title, 'You have received a new message, view it on your Messages tab.' as content, cum.last_updated as created_date
, 'flaticon-multimedia' as icon                           
FROM Elio_collaboration_users_mailbox  cum
INNER JOIN Elio_collaboration_mailbox cm
	ON cm.id = cum.mailbox_id
WHERE 1 = 1                              
AND cm.is_public = 1
AND cum.master_user_id = @user_id
AND is_new = 1
AND is_viewed = 0 
AND cm.user_id != @user_id 
AND cum.is_deleted = 0 
AND cum.is_public = 1
)


, pending_commissions
as
(
SELECT 
'Commissions' as title, 'A new payout of ' + cast(replace(amount, '.00', '') as nvarchar(10)) + ' ' + cc.currency_symbol + ' has been scheduled to be transferred in ' + convert(varchar, dateadd(day, 30, ld.last_update), 23) + ' to your partner, ' + u.company_name + '.' as content, ld.last_update as created_date
, 'flaticon-notepad' as icon 
FROM Elio_lead_distributions ld 
INNER join Elio_collaboration_vendors_resellers cvr
on cvr.master_user_id = ld.vendor_id
AND cvr.partner_user_id = ld.reseller_id
and cvr.is_active = 1
INNER JOIN Elio_currencies_countries cc
ON cc.cur_id = ld.cur_id
left joiN Elio_tier_management_users_settings tmus
on tmus.user_id = ld.vendor_id
and tmus.description = cvr.tier_status
INNER JOIN elio_users u 
ON u.id = ld.reseller_id
WHERE 1 = 1
AND ld.is_public = 1
AND ld.status = 2
AND lead_result = 'Won'
AND payment_status = 0 
AND vendor_id = @user_id
and dateadd(day, 30, ld.last_update) > getdate()
)

, payouts
as
(
SELECT 
'Payouts' as title, 'A payout of ' + cast(replace(amount, '.00', '') as nvarchar(10)) + ' ' + cc.currency_symbol + ' has been successfully transferred at ' + convert(varchar, ld.last_update, 23) + ' to your partner, ' + u.company_name + '. Visit your Commissions tab to view more details.' as content, ld.last_update as created_date
, 'flaticon-notepad' as icon
FROM Elio_lead_distributions ld 
INNER join Elio_collaboration_vendors_resellers cvr
on cvr.master_user_id = ld.vendor_id
AND cvr.partner_user_id = ld.reseller_id
and cvr.is_active = 1
INNER JOIN Elio_currencies_countries cc
ON cc.cur_id = ld.cur_id
left joiN Elio_tier_management_users_settings tmus
on tmus.user_id = ld.vendor_id
and tmus.description = cvr.tier_status
INNER JOIN elio_users u 
ON u.id = ld.reseller_id
WHERE 1 = 1
AND ld.is_public = 1
AND ld.status = 2
AND lead_result = 'Won'
AND payment_status = 1 
AND vendor_id = @user_id
and dateadd(day, 30, ld.last_update) <= getdate()
)

, new_posts
as
(
    SELECT 'New Post by ' + u.company_name as title, post_message as content,date_created as created_date
    , 'flaticon-notepad' as icon
    FROM Elio_users_posts p
	inner join elio_users u
		on u.id = p.user_id
    WHERE 1 = 1
    AND p.is_public = 1
    AND p.user_id = @user_id
)

select * from new_deals
where created_date > '2022-01-01'
union
select * from close_deals
where created_date > '2022-01-01'
union
select * from close_leads
where created_date > '2022-01-01'
union
select * from col_messages
where created_date > '2022-01-01'
union
select * from new_files
where created_date > '2022-01-01'
union
select * from pending_request
where created_date > '2022-01-01'
union
select * from confirmed_request
where created_date > '2021-01-01'
union
select * from new_inbox_msgs
where created_date > '2022-01-01'
union
select * from pending_commissions
where created_date > '2022-01-01'
union
select * from payouts
where created_date > '2022-01-01'
union
select * from new_posts
where created_date > '2022-01-01'

order by created_date desc"
                    , DatabaseHelper.CreateIntParameter("@user_id", user.Id));
            }
            else
            {
                return session.GetDataTable(@";with new_deals
as
(
SELECT 'Deal Registration' as title, 'A deal you have submitted has been Accepted/Rejects by ' + u2.company_name + ' at ' + cast(cast(rd.last_update as date) as nvarchar(50)) + '. Go to your Deal Registration tab to manage the deal.' as content,rd.last_update as created_date
, 'flaticon-notepad' as icon
FROM Elio_registration_deals rd
inner join elio_users u
	on rd.reseller_id = u.id
	and u.company_type = 'Channel Partners'
inner join elio_users u2
	on rd.vendor_id = u2.id
	and u2.company_type = 'Vendors'
WHERE 1 = 1
AND rd.is_public = 1
AND is_active IN (1,-1)
AND status = 2 
AND deal_result = 'Pending'
AND is_new = 0
AND reseller_id = @user_id
)

,close_leads
as
(
SELECT 'Lead Distribution' as title, 'You have received a new lead by ' + u.company_name + ' at ' + cast(cast(ld.created_date as date) as nvarchar(50)) + '. Visit the Lead Distribution tab to view the details.' as content,ld.created_date
, 'flaticon2-digital-marketing' as icon
FROM Elio_lead_distributions ld
inner join elio_users u
	on ld.vendor_id = u.id
WHERE 1 = 1
AND ld.is_public = 1
AND status = 1 
AND lead_result = 'Pending'
AND is_new = 1
AND reseller_id = @user_id
)

, new_posts
as
(
    SELECT 'New Post by ' + u.company_name as title, post_message as content,date_created as created_date
	, 'flaticon-notepad' as icon
    FROM Elio_users_posts p
	inner join elio_users u
		on u.id = p.user_id
	inner join Elio_collaboration_vendors_resellers cvr
		on cvr.master_user_id = u.id
		and cvr.master_user_id = p.user_id
		and cvr.partner_user_id = @user_id
		and cvr.invitation_status = 'Confirmed'
    WHERE 1 = 1
    AND p.is_public = 1
)

, col_messages
as
(
SELECT 'Collaboration Messages' as title, 'You have received new messages. Got to Collaborate tab to view and respond.' as content, date_created as created_date
, 'flaticon-chat' as icon
FROM Elio_collaboration_mailbox cm
INNER JOIN Elio_collaboration_users_mailbox cum
ON cm.id =  cum.mailbox_id
inner join Elio_collaboration_vendors_resellers cvr
	on cvr.partner_user_id = cum.partner_user_id
	and cvr.master_user_id = cum.master_user_id
	and cvr.invitation_status = 'Confirmed'
where 1 = 1 
AND cum.partner_user_id = @user_id
AND is_viewed = 0
AND cm.user_id != @user_id 
AND cum.is_deleted = 0 
AND cum.is_public = 1
)

,new_onboarding
as
(
SELECT 'Onboarding Resources' as title, u.company_name +' has uploaded new material at ' + cast(cast(ulf.date_created as date) as nvarchar(50)) + '. View all documents on your Onboarding Resources tab.' as content,ulf.date_created AS created_date
	, 'flaticon-chat' as icon
	FROM Elio_onboarding_users_library_files ulf
	inner join elio_users u
		on ulf.user_id = u.id
	where user_id in
	(
		select master_user_id 
		from Elio_collaboration_vendors_resellers cvr
		where invitation_status = 'Confirmed'
		and partner_user_id = @user_id
		and is_active = 1
	)
	and ulf.is_new = 1
	and ulf.is_public = 1
)

--, pending_request
--as
--(
--SELECT *
--FROM Elio_collaboration_vendors_resellers cvr
--inner join elio_users u
--	on cvr.master_user_id = u.id
----inner join Elio_collaboration_vendor_reseller_invitations 
----on Elio_collaboration_vendor_reseller_invitations.vendor_reseller_id = Elio_collaboration_vendors_resellers.id 
----inner join Elio_collaboration_users_invitations
----	on Elio_collaboration_users_invitations.id = Elio_collaboration_vendor_reseller_invitations.user_invitation_id                                
--WHERE 1 = 1 
--    AND invitation_status = 'Pending' 
-- --   AND Elio_collaboration_vendors_resellers.is_active = 1 
-- --   AND Elio_collaboration_users_invitations.is_public = 1
--	--AND Elio_collaboration_vendor_reseller_invitations.user_id <> 1
--	--AND master_user_id = 1
--	AND partner_user_id = @user_id
--)

select * from new_deals
where created_date > '2022-01-01'
union
select * from close_leads
where created_date > '2022-01-01'
union
select * from new_posts
where created_date > '2022-01-01'
union
select * from col_messages
where created_date > '2022-01-01'
--union
--select * from new_onboarding
--where created_date > '2022-01-01'
order by created_date desc"
                    , DatabaseHelper.CreateIntParameter("@user_id", user.Id));
            }
        }

        public static bool ExistCountryInCountriesVIEW_EN(string country, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                      FROM Elio_countries_no_overview_VIEW_EN
                                                      where country_name = @country_name"
                                                , DatabaseHelper.CreateStringParameter("@country_name", country));

            return Convert.ToInt32(table.Rows[0]["count"]) == 1;
        }

        public static bool ExistCountryInCountriesVIEW_ES(string country, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                      FROM Elio_countries_no_overview_VIEW_ES
                                                      where country_name = @country_name"
                                                , DatabaseHelper.CreateStringParameter("@country_name", country));

            return Convert.ToInt32(table.Rows[0]["count"]) == 1;
        }

        public static bool ExistCountryInCountriesVIEW_FR(string country, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                      FROM Elio_countries_no_overview_VIEW_FR
                                                      where country_name = @country_name"
                                                , DatabaseHelper.CreateStringParameter("@country_name", country));

            return Convert.ToInt32(table.Rows[0]["count"]) == 1;
        }

        public static bool ExistCountryInCountriesVIEW_PT(string country, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                      FROM Elio_countries_no_overview_VIEW_PT
                                                      where country_name = @country_name"
                                                , DatabaseHelper.CreateStringParameter("@country_name", country));

            return Convert.ToInt32(table.Rows[0]["count"]) == 1;
        }

        public static bool ExistCountryInCountriesVIEW_DE(string country, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                      FROM Elio_countries_no_overview_VIEW_DE
                                                      where country_name = @country_name"
                                                , DatabaseHelper.CreateStringParameter("@country_name", country));

            return Convert.ToInt32(table.Rows[0]["count"]) == 1;
        }

        public static bool ExistCountryInCountriesVIEW_IT(string country, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                      FROM Elio_countries_no_overview_VIEW_IT
                                                      where country_name = @country_name"
                                                , DatabaseHelper.CreateStringParameter("@country_name", country));

            return Convert.ToInt32(table.Rows[0]["count"]) == 1;
        }

        public static bool ExistCountryInCountries(string country, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                      FROM Elio_countries
                                                      where country_name = @country_name"
                                                , DatabaseHelper.CreateStringParameter("@country_name", country));

            return Convert.ToInt32(table.Rows[0]["count"]) == 1;
        }

        public static void InsertProductToUserBySubIndustryGroupItemsIDs(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select count(id) as count 
                                                        FROM Elio_users_sub_industries_group_items
                                                        where user_id = @user_id
                                                        and sub_industry_group_item_id in
                                                        ( 75,76,77,78,79,89,93,94,121,122,123,124,125 )"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            if (table != null && table.Rows.Count > 0)
            {
                if (Convert.ToInt32(table.Rows[0]["count"]) > 0)
                {
                    DataTable tableProd = session.GetDataTable(@"select count(id) as count 
                                                        FROM Elio_users_registration_products
                                                        where user_id = @user_id
                                                        and reg_products_id = 5359"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

                    if (tableProd != null && Convert.ToInt32(tableProd.Rows[0]["count"]) == 0)
                    {
                        DataLoader<ElioUsersRegistrationProducts> loader = new DataLoader<ElioUsersRegistrationProducts>(session);

                        ElioUsersRegistrationProducts uProduct = new ElioUsersRegistrationProducts();

                        uProduct.UserId = userId;
                        uProduct.RegProductsId = 5359;

                        loader.Insert(uProduct);

                        //session.ExecuteQuery("insert into Elio_users_registration_products values(" + userId + ", 5359)");
                    }
                }
            }
        }

        public static bool AllowAddHistoricalLeadForPartner(int vendorId, int resellerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count from Elio_lead_distributions with (nolock) 
                                                        where 1 = 1
                                                        and vendor_id = @vendor_id
                                                        and reseller_id = @reseller_id
                                                        and (is_public = 0 and is_historical_reason = 1)"
                                                   , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                   , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            if (ConfigurationManager.AppSettings["MaxLeadsHistoricalCount"] != "")
                return Convert.ToInt32(table.Rows[0]["count"]) <= Convert.ToInt32(ConfigurationManager.AppSettings["MaxLeadsHistoricalCount"]) ? true : false;
            else
                return Convert.ToInt32(table.Rows[0]["count"]) <= 10 ? true : false;
        }

        public static string GetUserGUIDByIdTbl(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT guid FROM Elio_users WHERE id = @id"
                                                , DatabaseHelper.CreateIntParameter("@id", userId));

            string key = (table.Rows.Count > 0) ? table.Rows[0]["guid"].ToString() : "";

            return key;
        }

        public static int GetSnitcherRFPsLeadsCountForChannelPartners(string leadLastSeen, int resultCase, DBSession session)
        {
            DataTable table = null;

            if (leadLastSeen == "-1")
            {
                table = session.GetDataTable(@"Select count(id) as count from Elio_snitcher_website_leads where is_public = 1 and is_approved = 1");

                return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) : 0;
            }
            else
            {
                string daysBefore = "DATEADD(day, " + (-1 * Convert.ToInt32(leadLastSeen)).ToString() + ", getdate())";

                table = session.GetDataTable(@"Select count(id) as count
                                                    from Elio_snitcher_website_leads 
                                                    where is_public = 1 
                                                    and is_approved = 1 
                                                    and lead_last_seen >= " + daysBefore + " ");

                return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) : 0;
            }
        }

        public static int GetSnitcherRFPsLeadsCount(int userId, string leadLastSeen, int resultCase, DBSession session)
        {
            int count = 0;

            if (userId == 38679)
            {
                DataTable table = null;

                if (leadLastSeen == "-1")
                {
                    table = session.GetDataTable(@"Select count(id) as count from Elio_snitcher_website_leads where is_public = 1 and is_approved = 1");

                    return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) : 0;
                }
                else
                {
                    string daysBefore = "DATEADD(day, " + (-1 * Convert.ToInt32(leadLastSeen)).ToString() + ", getdate())";

                    table = session.GetDataTable(@"Select count(id) as count
                                                    from Elio_snitcher_website_leads 
                                                    where is_public = 1 
                                                    and is_approved = 1 
                                                    and lead_last_seen >= " + daysBefore + " ");

                    return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) : 0;
                }
            }
            else
            {
                DataLoader<ElioSnitcherUserCountryProducts> productsLoader = new DataLoader<ElioSnitcherUserCountryProducts>(session);

                ElioSnitcherUserCountryProducts userInfo = productsLoader.LoadSingle(@"Select * from Elio_snitcher_user_country_products where user_id = @user_id and is_active = 1"
                                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));

                if (userInfo != null && userInfo.IsActive == 0)
                    return 0;

                if (userInfo == null)
                {
                    DataTable table = null;

                    if (leadLastSeen == "-1")
                    {
                        table = session.GetDataTable(@"Select count(id) as count from Elio_snitcher_website_leads where is_public = 1 and is_approved = 1");

                        return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) : 0;
                    }
                    else
                    {
                        string daysBefore = "DATEADD(day, " + (-1 * Convert.ToInt32(leadLastSeen)).ToString() + ", getdate())";

                        table = session.GetDataTable(@"Select count(id) as count
                                                    from Elio_snitcher_website_leads 
                                                    where is_public = 1 
                                                    and is_approved = 1 
                                                    and lead_last_seen >= " + daysBefore + " ");

                        return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) : 0;
                    }
                }
                else
                {
                    bool isWorldWide = false;
                    string countries = "";
                    string[] userCountries = userInfo.Country.ToLower().Split(',').ToArray();

                    if (userCountries.Length == 1 && userCountries[0].Trim() == "")
                        isWorldWide = true;
                    else
                    {
                        if (userCountries.Length > 0)
                        {
                            foreach (string countryName in userCountries)
                            {
                                countries += "'" + countryName + "',";
                            }

                            if (countries.Length > 0 && countries.EndsWith(","))
                                countries = countries.Substring(0, countries.Length - 1);
                        }
                        else
                            isWorldWide = true;
                    }

                    DataLoader<ElioSnitcherLeadsPageviews> loader = new DataLoader<ElioSnitcherLeadsPageviews>(session);
                    List<ElioSnitcherLeadsPageviews> pageViews = new List<ElioSnitcherLeadsPageviews>();

                    string query = "";

                    if (leadLastSeen == "-1")
                    {
                        if (resultCase == 1)
                        {
                            if (!isWorldWide)
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id
                                            where is_public = 1 and is_approved = 1 and lead_country IN (" + countries + ")";
                            }
                            else
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id
                                            where is_public = 1 and is_approved = 1";
                            }

                            pageViews = loader.Load(query);
                        }
                        else if (resultCase == 2)
                        {
                            if (!isWorldWide)
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id 
                                            where is_public = 1 and is_approved = 1 and is_api_lead = 1 and lead_country IN (" + countries + ")";
                            }
                            else
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id 
                                            where is_public = 1 and is_approved = 1 and is_api_lead = 1";
                            }

                            pageViews = loader.Load(query);
                        }
                        else
                        {
                            if (!isWorldWide)
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id 
                                            where is_public = 1 and is_approved = 1 and is_api_lead = 0 and lead_country IN (" + countries + ")";
                            }
                            else
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id 
                                            where is_public = 1 and is_approved = 1 and is_api_lead = 0";
                            }

                            pageViews = loader.Load(query);
                        }
                    }
                    else
                    {
                        string daysBefore = "DATEADD(day, " + (-1 * Convert.ToInt32(leadLastSeen)).ToString() + ", getdate())";

                        if (resultCase == 1)
                        {
                            if (!isWorldWide)
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id 
                                            where is_public = 1 
                                            and is_approved = 1 
                                            and lead_country IN (" + countries + ") " +
                                            "and lead_last_seen >= " + daysBefore + " " +
                                            "order by lead_last_seen desc";
                            }
                            else
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id 
                                            where is_public = 1 
                                            and is_approved = 1 
                                            and lead_last_seen >= " + daysBefore + " " +
                                            "order by lead_last_seen desc";
                            }

                            pageViews = loader.Load(query);
                        }
                        else if (resultCase == 2)
                        {
                            if (!isWorldWide)
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id
                                            where is_public = 1 
                                            and is_approved = 1 
                                            and is_api_lead = 1 
                                            and lead_country IN (" + countries + ") " +
                                            "and lead_last_seen >= " + daysBefore + " " +
                                            "order by lead_last_seen desc";
                            }
                            else
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id
                                            where is_public = 1 
                                            and is_approved = 1 
                                            and is_api_lead = 1 
                                            and lead_last_seen >= " + daysBefore + " " +
                                            "order by lead_last_seen desc";
                            }

                            pageViews = loader.Load(query);
                        }
                        else
                        {
                            if (!isWorldWide)
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id
                                            where is_public = 1 
                                            and is_approved = 1 
                                            and is_api_lead = 0 
                                            and lead_country IN (" + countries + ") " +
                                            "and lead_last_seen >= " + daysBefore + " " +
                                            "order by lead_last_seen desc";
                            }
                            else
                            {
                                query = @"Select pv.lead_id,pv.product 
                                            from Elio_snitcher_website_leads l
                                            inner join Elio_snitcher_leads_pageviews pv
	                                            on pv.lead_id = l.lead_id
                                            where is_public = 1 
                                            and is_approved = 1 
                                            and is_api_lead = 0 
                                            and lead_last_seen >= " + daysBefore + " " +
                                            "order by lead_last_seen desc";
                            }

                            pageViews = loader.Load(query);
                        }
                    }

                    if (pageViews.Count > 0)
                    {
                        foreach (ElioSnitcherLeadsPageviews pageView in pageViews)
                        {
                            string[] userProducts = userInfo.Products.ToLower().Split(',').ToArray();

                            if (userProducts.Contains(pageView.Product.Trim().ToLower()))
                            {
                                count++;
                            }
                        }
                    }
                    else
                        return count;
                }

                return count;
            }
        }

        public static string GetSnitcherLeadIDByLeadId(string leadId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select lead_id 
                                                    from Elio_snitcher_website_leads where lead_id = @lead_id"
                                            , DatabaseHelper.CreateStringParameter("@lead_id", leadId));

            return (table != null && table.Rows.Count > 0) ? table.Rows[0]["lead_id"].ToString() : "";
        }

        public static DataTable GetRFPsRequestsTbl(int isApproved, int isApiLead, DBSession session)
        {
            string query = "";

            if (isApproved == 1)
            {
                query = @"SELECT TOP 20 r.[id]
                                ,r.[lead_first_name]
	                            ,r.[lead_last_name]
	                            ,r.[lead_company_name]
	                            ,r.[lead_company_email]
                                ,r.[lead_country]
	                            ,r.[lead_company_size]
                                --,p.url as product
                                ,r.[message]
	                            ,r.[sysdate]
	                            ,r.[is_approved] 
                                ,r.is_confirmed
                                ,isnull(u.company_name, '-') as rfp_company_name
                            FROM Elio_snitcher_website_leads r
                            left join elio_users u
                                on u.id = r.rfp_message_company_id_is_for
                            WHERE 1 = 1 and r.is_public = 1 and r.is_approved = @is_approved and is_api_lead = @is_api_lead
                                order by r.is_approved, r.sysdate desc, r.id";
            }
            else
            {
                query = @"SELECT r.[id]
                                ,r.[lead_first_name]
	                            ,r.[lead_last_name]
	                            ,r.[lead_company_name]
	                            ,r.[lead_company_email]
                                ,r.[lead_country]
	                            ,r.[lead_company_size]
                                --,p.url as product
                                ,r.[message]
	                            ,r.[sysdate]
	                            ,r.[is_approved] 
                                ,r.is_confirmed
                                ,isnull(u.company_name, '-') as rfp_company_name
                            FROM Elio_snitcher_website_leads r
                            left join elio_users u
                                on u.id = r.rfp_message_company_id_is_for
                            WHERE 1 = 1 and r.is_public = 1 and r.is_approved = @is_approved and is_api_lead = @is_api_lead
                                order by r.is_approved, r.sysdate desc, r.id";
            }

            return session.GetDataTable(query
                                , DatabaseHelper.CreateIntParameter("@is_approved", isApproved)
                                , DatabaseHelper.CreateIntParameter("@is_api_lead", isApiLead));
        }

        public static DataTable GetSnitcherLeadContactsTbl(string leadId, DBSession session)
        {
            if (leadId != "")
            {
                return session.GetDataTable(@"Select id,lead_id,lead_company_contacts from Elio_snitcher_website_leads where lead_id = @lead_id order by lead_last_seen desc"
                                    , DatabaseHelper.CreateStringParameter("@lead_id", leadId));
            }
            else
            {
                return session.GetDataTable(@"Select top 1 id,lead_id,lead_company_contacts from Elio_snitcher_website_leads order by lead_last_seen desc");
            }
        }

        public static DataTable GetSnitcherLeadsForReseller(int resultCase, string leadLastSeen, int userId, bool isExport, DBSession session)
        {
            DataLoader<ElioSnitcherUserCountryProducts> productsLoader = new DataLoader<ElioSnitcherUserCountryProducts>(session);

            ElioSnitcherUserCountryProducts userInfo = productsLoader.LoadSingle(@"Select * from Elio_snitcher_user_country_products where user_id = @user_id and is_active = 1"
                                                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            if (userInfo == null || userInfo != null && userInfo.IsActive == 0)
                return null;
            else
            {
                bool isWorldWide = false;
                string countries = "";
                string[] userCountries = userInfo.Country.ToLower().Split(',').ToArray();

                if (userCountries.Length == 1 && userCountries[0].Trim() == "")
                    isWorldWide = true;
                else
                {
                    if (userCountries.Length > 0)
                    {
                        foreach (string countryName in userCountries)
                        {
                            if (countryName != "")
                                countries += "'" + countryName + "',";
                        }

                        if (countries.Length > 0 && countries.EndsWith(","))
                            countries = countries.Substring(0, countries.Length - 1);
                        else
                            isWorldWide = true;
                    }
                    else
                        isWorldWide = true;
                }

                DataLoader<ElioSnitcherLeadsPageviews> loader = new DataLoader<ElioSnitcherLeadsPageviews>(session);
                List<ElioSnitcherLeadsPageviews> pageViews = new List<ElioSnitcherLeadsPageviews>();

                string query = "";

                if (leadLastSeen == "-1")
                {
                    if (resultCase == 1)
                    {
                        if (!isWorldWide)
                        {
                            query = @"Select pv.lead_id,pv.product 
                                    from Elio_snitcher_website_leads l
                                    inner join Elio_snitcher_leads_pageviews pv
	                                    on pv.lead_id = l.lead_id
                                    where is_public = 1 and is_approved = 1 and lead_country IN (" + countries + ") order by pv.lead_id desc";
                        }
                        else
                        {
                            query = @"Select pv.lead_id,pv.product 
                                    from Elio_snitcher_website_leads l
                                    inner join Elio_snitcher_leads_pageviews pv
	                                    on pv.lead_id = l.lead_id
                                    where is_public = 1 and is_approved = 1 order by pv.lead_id desc";
                        }

                        pageViews = loader.Load(query);
                    }
                    else if (resultCase == 2)
                    {
                        if (!isWorldWide)
                        {
                            query = @"Select pv.lead_id,pv.product 
                                                from Elio_snitcher_website_leads l
                                                inner join Elio_snitcher_leads_pageviews pv
	                                                on pv.lead_id = l.lead_id 
                                                where is_public = 1 and is_approved = 1 and is_api_lead = 1 and lead_country IN (" + countries + ") order by pv.lead_id desc";
                        }
                        else
                        {
                            query = @"Select pv.lead_id,pv.product 
                                                from Elio_snitcher_website_leads l
                                                inner join Elio_snitcher_leads_pageviews pv
	                                                on pv.lead_id = l.lead_id 
                                                where is_public = 1 and is_approved = 1 and is_api_lead = 1 order by pv.lead_id desc";
                        }

                        pageViews = loader.Load(query);
                    }
                    else if (resultCase == 3)
                    {
                        if (!isWorldWide)
                        {
                            query = @"Select pv.lead_id,pv.product 
                                                from Elio_snitcher_website_leads l
                                                inner join Elio_snitcher_leads_pageviews pv
	                                                on pv.lead_id = l.lead_id 
                                                where is_public = 1 and is_approved = 1 and is_api_lead = 0 and lead_country IN (" + countries + ") order by pv.lead_id desc";
                        }
                        else
                        {
                            query = @"Select pv.lead_id,pv.product
                                                from Elio_snitcher_website_leads l
                                                inner join Elio_snitcher_leads_pageviews pv
	                                                on pv.lead_id = l.lead_id 
                                                where is_public = 1 and is_approved = 1 and is_api_lead = 0 order by pv.lead_id desc";
                        }

                        pageViews = loader.Load(query);
                    }
                    else
                    {
                        if (!isWorldWide)
                        {
                            query = @"Select pv.lead_id,pv.product 
                                                from Elio_snitcher_website_leads l
                                                inner join Elio_snitcher_leads_pageviews pv
	                                                on pv.lead_id = l.lead_id 
                                                where is_public = 1 
                                                and rfp_message_company_id_is_for = " + userId + " " +
                                                "and is_approved = 1 " +
                                                "and is_api_lead = 2 " +
                                                "--and lead_country IN (" + countries + ") " +
                                                "order by pv.lead_id desc";
                        }
                        else
                        {
                            query = @"Select pv.lead_id,pv.product
                                                from Elio_snitcher_website_leads l
                                                inner join Elio_snitcher_leads_pageviews pv
	                                                on pv.lead_id = l.lead_id 
                                                where is_public = 1 
                                                and rfp_message_company_id_is_for = " + userId + " " +
                                                "and is_approved = 1 " +
                                                "and is_api_lead = 2 " +
                                                "order by pv.lead_id desc";
                        }

                        pageViews = loader.Load(query);
                    }
                }
                else
                {
                    string daysBefore = "DATEADD(day, " + (-1 * Convert.ToInt32(leadLastSeen)).ToString() + ", getdate())";

                    if (resultCase == 1)
                    {
                        if (!isWorldWide)
                        {
                            query = @"Select pv.lead_id,pv.product
                                        from Elio_snitcher_website_leads l
                                        inner join Elio_snitcher_leads_pageviews pv
	                                        on pv.lead_id = l.lead_id 
                                        where is_public = 1 
                                        and is_approved = 1 
                                        and lead_country IN (" + countries + ") " +
                                        "and lead_last_seen >= " + daysBefore + " " +
                                        "order by product,pv.lead_id desc";
                        }
                        else
                        {
                            query = @"Select pv.lead_id,pv.product
                                        from Elio_snitcher_website_leads l
                                        inner join Elio_snitcher_leads_pageviews pv
	                                        on pv.lead_id = l.lead_id 
                                        where is_public = 1 
                                        and is_approved = 1 
                                        and lead_last_seen >= " + daysBefore + " " +
                                        "order by product,pv.lead_id desc";
                        }

                        pageViews = loader.Load(query
                                                    , DatabaseHelper.CreateStringParameter("@lead_country", userInfo.Country));
                    }
                    else if (resultCase == 2)
                    {
                        if (!isWorldWide)
                        {
                            query = @"Select pv.lead_id,pv.product 
                                        from Elio_snitcher_website_leads l
                                        inner join Elio_snitcher_leads_pageviews pv
	                                        on pv.lead_id = l.lead_id
                                        where is_public = 1 
                                        and is_approved = 1 
                                        and is_api_lead = 1 
                                        and lead_country IN (" + countries + ") " +
                                        "and lead_last_seen >= " + daysBefore + " " +
                                        "order by product,pv.lead_id desc";
                        }
                        else
                        {
                            query = @"Select pv.lead_id,pv.product
                                        from Elio_snitcher_website_leads l
                                        inner join Elio_snitcher_leads_pageviews pv
	                                        on pv.lead_id = l.lead_id
                                        where is_public = 1 
                                        and is_approved = 1 
                                        and is_api_lead = 1 
                                        and lead_last_seen >= " + daysBefore + " " +
                                        "order by product,pv.lead_id desc";
                        }

                        pageViews = loader.Load(query
                                                    , DatabaseHelper.CreateStringParameter("@lead_country", userInfo.Country));
                    }
                    else if (resultCase == 3)
                    {
                        if (!isWorldWide)
                        {
                            query = @"Select pv.lead_id,pv.product
                                        from Elio_snitcher_website_leads l
                                        inner join Elio_snitcher_leads_pageviews pv
	                                        on pv.lead_id = l.lead_id
                                        where is_public = 1 
                                        and is_approved = 1 
                                        and is_api_lead = 0 
                                        and lead_country IN (" + countries + ") " +
                                        "and lead_last_seen >= " + daysBefore + " " +
                                        "order by product,pv.lead_id desc";
                        }
                        else
                        {
                            query = @"Select pv.lead_id,pv.product
                                        from Elio_snitcher_website_leads l
                                        inner join Elio_snitcher_leads_pageviews pv
	                                        on pv.lead_id = l.lead_id
                                        where is_public = 1 
                                        and is_approved = 1 
                                        and is_api_lead = 0 
                                        and lead_last_seen >= " + daysBefore + " " +
                                        "order by product,pv.lead_id desc";
                        }

                        pageViews = loader.Load(query
                                                    , DatabaseHelper.CreateStringParameter("@lead_country", userInfo.Country));
                    }
                    else
                    {
                        if (!isWorldWide)
                        {
                            query = @"Select pv.lead_id,pv.product
                                        from Elio_snitcher_website_leads l
                                        inner join Elio_snitcher_leads_pageviews pv
	                                        on pv.lead_id = l.lead_id
                                        where is_public = 1 
                                        and rfp_message_company_id_is_for = " + userId + " " +
                                        "and is_approved = 1 " +
                                        "and is_api_lead = 2 " +
                                        "--and lead_country IN (" + countries + ") " +
                                        "--and lead_last_seen >= " + daysBefore + " " +
                                        "order by product,pv.lead_id desc";
                        }
                        else
                        {
                            query = @"Select pv.lead_id,pv.product
                                        from Elio_snitcher_website_leads l
                                        inner join Elio_snitcher_leads_pageviews pv
	                                        on pv.lead_id = l.lead_id
                                        where is_public = 1 
                                        and rfp_message_company_id_is_for = " + userId + " " +
                                        "and is_approved = 1 " +
                                        "and is_api_lead = 2 " +
                                        "--and lead_last_seen >= " + daysBefore + " " +
                                        "order by product,pv.lead_id desc";
                        }

                        pageViews = loader.Load(query
                                                    , DatabaseHelper.CreateStringParameter("@lead_country", userInfo.Country));
                    }
                }

                if (pageViews.Count > 0)
                {
                    string finalLeadIDs = "";
                    string[] userProducts = userInfo.Products.ToLower().Split(',').ToArray();

                    if (resultCase == 4)
                    {
                        foreach (ElioSnitcherLeadsPageviews pageView in pageViews)
                        {
                            finalLeadIDs += pageView.LeadId + ",";
                        }
                    }
                    else
                    {
                        foreach (ElioSnitcherLeadsPageviews pageView in pageViews)
                        {
                            if (userProducts.Contains(pageView.Product.ToLower().Trim()))
                            {
                                finalLeadIDs += pageView.LeadId + ",";
                            }
                        }
                    }

                    if (finalLeadIDs != "")
                    {
                        if (finalLeadIDs.EndsWith(","))
                            finalLeadIDs = finalLeadIDs.Substring(0, finalLeadIDs.Length - 1);

                        if (leadLastSeen == "-1")
                        {
                            if (!isExport)
                            {
                                //if (resultCase == 1)
                                //    return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo from Elio_snitcher_website_leads where lead_id in (" + finalLeadIDs + ") order by id desc");
                                //else if (resultCase == 2)
                                //    return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo from Elio_snitcher_website_leads where lead_id in (" + finalLeadIDs + ") order by id desc");
                                //else

                                return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo from Elio_snitcher_website_leads where lead_id in (" + finalLeadIDs + ") order by Elio_snitcher_website_leads.lead_last_seen desc");
                            }
                            else
                            {
                                //if (resultCase == 1)
                                //    return session.GetDataTable(@"Select [session_referrer]
                                //                                      ,[session_operating_system]
                                //                                      ,[session_browser]
                                //                                      ,[session_device_type]      
                                //                                      ,[session_start]
                                //                                      ,[session_duration]
                                //                                      ,[session_total_pageviews]      
                                //                                      ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                //                                      ,[lead_company_name]     
                                //                                      ,[lead_company_website]
                                //                                      ,[lead_country]
                                //                                      ,[lead_city]
                                //                                      ,[lead_company_address]
                                //                                      ,[lead_company_founded]
                                //                                      ,[lead_company_size]
                                //                                      ,[lead_company_industry]
                                //                                      ,[lead_company_phone]
                                //                                      ,[lead_company_email]
                                //                                      ,[lead_company_contacts]
                                //                                      ,[lead_linkedin_handle]
                                //                                      ,[lead_facebook_handle]
                                //                                      ,[lead_youtube_handle]
                                //                                      ,[lead_instagram_handle]
                                //                                      ,[lead_twitter_handle]
                                //                                      ,[lead_pinterest_handle]
                                //                                      ,[lead_angellist_handle]
                                //                                      ,[message] 
                                //                                      ,trim(',' from pp.product) as products
                                //                                from Elio_snitcher_website_leads l
                                //                                cross apply
                                //                             (
                                //                              select pv.product + ',' 
                                //                              from Elio_snitcher_leads_pageviews pv
                                //                              where pv.lead_id = l.lead_id 
                                //                              for xml path('')
                                //                             )pp(product)
                                //                                where lead_id in (" + finalLeadIDs + ") order by l.id desc");
                                //else if (resultCase == 2)
                                //    return session.GetDataTable(@"Select [session_referrer]
                                //                                      ,[session_operating_system]
                                //                                      ,[session_browser]
                                //                                      ,[session_device_type]      
                                //                                      ,[session_start]
                                //                                      ,[session_duration]
                                //                                      ,[session_total_pageviews]      
                                //                                      ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                //                                      ,[lead_company_name]     
                                //                                      ,[lead_company_website]
                                //                                      ,[lead_country]
                                //                                      ,[lead_city]
                                //                                      ,[lead_company_address]
                                //                                      ,[lead_company_founded]
                                //                                      ,[lead_company_size]
                                //                                      ,[lead_company_industry]
                                //                                      ,[lead_company_phone]
                                //                                      ,[lead_company_email]
                                //                                      ,[lead_company_contacts]
                                //                                      ,[lead_linkedin_handle]
                                //                                      ,[lead_facebook_handle]
                                //                                      ,[lead_youtube_handle]
                                //                                      ,[lead_instagram_handle]
                                //                                      ,[lead_twitter_handle]
                                //                                      ,[lead_pinterest_handle]
                                //                                      ,[lead_angellist_handle]
                                //                                      ,[message] 
                                //                                      ,trim(',' from pp.product) as products
                                //                                from Elio_snitcher_website_leads l 
                                //                                cross apply
                                //                             (
                                //                              select pv.product + ',' 
                                //                              from Elio_snitcher_leads_pageviews pv
                                //                              where pv.lead_id = l.lead_id 
                                //                              for xml path('')
                                //                             )pp(product)
                                //                                where lead_id in (" + finalLeadIDs + ") order by l.id desc");
                                //else
                                return session.GetDataTable(@"Select [session_referrer]
                                                                      ,[session_operating_system]
                                                                      ,[session_browser]
                                                                      ,[session_device_type]      
                                                                      ,[session_start]
                                                                      ,[session_duration]
                                                                      ,[session_total_pageviews]      
                                                                      ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                                                      ,[lead_company_name]     
                                                                      ,[lead_company_website]
                                                                      ,[lead_country]
                                                                      ,[lead_city]
                                                                      ,[lead_company_address]
                                                                      ,[lead_company_founded]
                                                                      ,[lead_company_size]
                                                                      ,[lead_company_industry]
                                                                      ,[lead_company_phone]
                                                                      ,[lead_company_email]
                                                                      ,[lead_company_contacts]
                                                                      ,[lead_linkedin_handle]
                                                                      ,[lead_facebook_handle]
                                                                      ,[lead_youtube_handle]
                                                                      ,[lead_instagram_handle]
                                                                      ,[lead_twitter_handle]
                                                                      ,[lead_pinterest_handle]
                                                                      ,[lead_angellist_handle]
                                                                      ,[message] 
                                                                      ,trim(',' from pp.product) as products
                                                                from Elio_snitcher_website_leads l 
                                                                cross apply
	                                                            (
		                                                            select pv.product + ',' 
		                                                            from Elio_snitcher_leads_pageviews pv
		                                                            where pv.lead_id = l.lead_id 
		                                                            for xml path('')
	                                                            )pp(product)
                                                                where lead_id in (" + finalLeadIDs + ") order by l.id desc");
                            }
                        }
                        else
                        {
                            if (isExport)
                            {
                                string daysBefore = "DATEADD(day, " + (-1 * Convert.ToInt32(leadLastSeen)).ToString() + ", getdate())";

                                //if (resultCase == 1)
                                //{
                                //    return session.GetDataTable(@"Select [session_referrer]
                                //                                      ,[session_operating_system]
                                //                                      ,[session_browser]
                                //                                      ,[session_device_type]      
                                //                                      ,[session_start]
                                //                                      ,[session_duration]
                                //                                      ,[session_total_pageviews]      
                                //                                      ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                //                                      ,[lead_company_name]     
                                //                                      ,[lead_company_website]
                                //                                      ,[lead_country]
                                //                                      ,[lead_city]
                                //                                      ,[lead_company_address]
                                //                                      ,[lead_company_founded]
                                //                                      ,[lead_company_size]
                                //                                      ,[lead_company_industry]
                                //                                      ,[lead_company_phone]
                                //                                      ,[lead_company_email]
                                //                                      ,[lead_company_contacts]
                                //                                      ,[lead_linkedin_handle]
                                //                                      ,[lead_facebook_handle]
                                //                                      ,[lead_youtube_handle]
                                //                                      ,[lead_instagram_handle]
                                //                                      ,[lead_twitter_handle]
                                //                                      ,[lead_pinterest_handle]
                                //                                      ,[lead_angellist_handle]
                                //                                      ,[message] 
                                //                                      ,trim(',' from pp.product) as products
                                //                from Elio_snitcher_website_leads l
                                //                cross apply
                                //             (
                                //              select pv.product + ',' 
                                //              from Elio_snitcher_leads_pageviews pv
                                //              where pv.lead_id = l.lead_id 
                                //              for xml path('')
                                //             )pp(product)
                                //                where lead_id in (" + finalLeadIDs + ") " +
                                //                "and lead_last_seen >= " + daysBefore + " " +
                                //                "order by l.id desc");
                                //}
                                //else if (resultCase == 2)
                                //{
                                //    return session.GetDataTable(@"Select [session_referrer]
                                //                                      ,[session_operating_system]
                                //                                      ,[session_browser]
                                //                                      ,[session_device_type]      
                                //                                      ,[session_start]
                                //                                      ,[session_duration]
                                //                                      ,[session_total_pageviews]      
                                //                                      ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                //                                      ,[lead_company_name]     
                                //                                      ,[lead_company_website]
                                //                                      ,[lead_country]
                                //                                      ,[lead_city]
                                //                                      ,[lead_company_address]
                                //                                      ,[lead_company_founded]
                                //                                      ,[lead_company_size]
                                //                                      ,[lead_company_industry]
                                //                                      ,[lead_company_phone]
                                //                                      ,[lead_company_email]
                                //                                      ,[lead_company_contacts]
                                //                                      ,[lead_linkedin_handle]
                                //                                      ,[lead_facebook_handle]
                                //                                      ,[lead_youtube_handle]
                                //                                      ,[lead_instagram_handle]
                                //                                      ,[lead_twitter_handle]
                                //                                      ,[lead_pinterest_handle]
                                //                                      ,[lead_angellist_handle]
                                //                                      ,[message] 
                                //                                      ,trim(',' from pp.product) as products
                                //                from Elio_snitcher_website_leads l
                                //                cross apply
                                //             (
                                //              select pv.product + ',' 
                                //              from Elio_snitcher_leads_pageviews pv
                                //              where pv.lead_id = l.lead_id 
                                //              for xml path('')
                                //             )pp(product)
                                //                where lead_id in (" + finalLeadIDs + ") " +
                                //                "and lead_last_seen >= " + daysBefore + " " +
                                //                "order by l.id desc");
                                //}
                                //else
                                //{
                                return session.GetDataTable(@"Select [session_referrer]
                                                                      ,[session_operating_system]
                                                                      ,[session_browser]
                                                                      ,[session_device_type]      
                                                                      ,[session_start]
                                                                      ,[session_duration]
                                                                      ,[session_total_pageviews]      
                                                                      ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                                                      ,[lead_company_name]     
                                                                      ,[lead_company_website]
                                                                      ,[lead_country]
                                                                      ,[lead_city]
                                                                      ,[lead_company_address]
                                                                      ,[lead_company_founded]
                                                                      ,[lead_company_size]
                                                                      ,[lead_company_industry]
                                                                      ,[lead_company_phone]
                                                                      ,[lead_company_email]
                                                                      ,[lead_company_contacts]
                                                                      ,[lead_linkedin_handle]
                                                                      ,[lead_facebook_handle]
                                                                      ,[lead_youtube_handle]
                                                                      ,[lead_instagram_handle]
                                                                      ,[lead_twitter_handle]
                                                                      ,[lead_pinterest_handle]
                                                                      ,[lead_angellist_handle]
                                                                      ,[message]
                                                                      ,trim(',' from pp.product) as products
                                                from Elio_snitcher_website_leads l
                                                cross apply
	                                            (
		                                            select pv.product + ',' 
		                                            from Elio_snitcher_leads_pageviews pv
		                                            where pv.lead_id = l.lead_id 
		                                            for xml path('')
	                                            )pp(product)
                                                where lead_id in (" + finalLeadIDs + ") " +
                                            "and lead_last_seen >= " + daysBefore + " " +
                                            "order by l.id desc");
                                //}
                            }
                            else
                            {
                                string daysBefore = "DATEADD(day, " + (-1 * Convert.ToInt32(leadLastSeen)).ToString() + ", getdate())";

                                //if (resultCase == 1)
                                //{
                                //    return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo 
                                //                from Elio_snitcher_website_leads 
                                //                where lead_id in (" + finalLeadIDs + ") " +
                                //                "and lead_last_seen >= " + daysBefore + " " +
                                //                "order by id desc");
                                //}
                                //else if (resultCase == 2)
                                //{
                                //    return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo 
                                //                from Elio_snitcher_website_leads 
                                //                where lead_id in (" + finalLeadIDs + ") " +
                                //                "and lead_last_seen >= " + daysBefore + " " +
                                //                "order by id desc");
                                //}
                                //else
                                //{
                                return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo 
                                                from Elio_snitcher_website_leads 
                                                where lead_id in (" + finalLeadIDs + ") " +
                                            "and lead_last_seen >= " + daysBefore + " " +
                                            "order by Elio_snitcher_website_leads.lead_last_seen desc");
                                //}
                            }
                        }
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
        }

        public static DataTable GetSnitcherLeadForResellerByLeadId(string leadId, DBSession session)
        {
            return session.GetDataTable(@"Select [session_referrer]
                                            ,[session_operating_system]
                                            ,[session_browser]
                                            ,[session_device_type]      
                                            ,[session_start]
                                            ,[session_duration]
                                            ,[session_total_pageviews]      
                                            ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                            ,[lead_company_name]     
                                            ,[lead_company_website]
                                            ,[lead_country]
                                            ,[lead_city]
                                            ,[lead_company_address]
                                            ,[lead_company_founded]
                                            ,[lead_company_size]
                                            ,[lead_company_industry]
                                            ,[lead_company_phone]
                                            ,[lead_company_email]
                                            ,[lead_company_contacts]
                                            ,[lead_linkedin_handle]
                                            ,[lead_facebook_handle]
                                            ,[lead_youtube_handle]
                                            ,[lead_instagram_handle]
                                            ,[lead_twitter_handle]
                                            ,[lead_pinterest_handle]
                                            ,[lead_angellist_handle]
                                            ,[message] 
                                            ,vi.products
                                            from Elio_snitcher_website_leads l
                                            cross apply
                                            (
	                                            select cast(product + ',' as nvarchar(500))
	                                            from Elio_snitcher_leads_pageviews pv
	                                            where pv.lead_id = l.lead_id
	                                            for xml path('')
	
                                            )vi(products)
                                            where l.lead_id = @lead_id"
                                    , DatabaseHelper.CreateStringParameter("@lead_id", leadId));
        }

        public static DataTable GetSnitcherLeads(int resultCase, string leadLastSeen, int userId, DBSession session)
        {
            DataLoader<ElioSnitcherUserCountryProducts> productsLoader = new DataLoader<ElioSnitcherUserCountryProducts>(session);

            ElioSnitcherUserCountryProducts userInfo = productsLoader.LoadSingle(@"Select * from Elio_snitcher_user_country_products where user_id = @user_id and is_active = 1"
                                                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            if (userInfo == null || (userInfo != null && userInfo.IsActive == 0))
                return null;
            else
            {
                DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);
                List<ElioSnitcherWebsiteLeads> leads = new List<ElioSnitcherWebsiteLeads>();

                if (leadLastSeen == "-1")
                {
                    if (resultCase == 1)
                        leads = loader.Load(@"Select id,lead_id,lead_company_address from Elio_snitcher_website_leads where is_public = 1 and is_approved = 1 order by lead_last_seen desc");
                    else if (resultCase == 2)
                        leads = loader.Load(@"Select id,lead_id,lead_company_address from Elio_snitcher_website_leads where is_public = 1 and is_approved = 1 and is_api_lead = 1 order by lead_last_seen desc");
                    else
                        leads = loader.Load(@"Select id,lead_id,lead_company_address from Elio_snitcher_website_leads where is_public = 1 and is_approved = 1 and is_api_lead = 0 order by lead_last_seen desc");
                }
                else
                {
                    string daysBefore = "DATEADD(day, " + (-1 * Convert.ToInt32(leadLastSeen)).ToString() + ", getdate())";

                    if (resultCase == 1)
                    {
                        leads = loader.Load(@"Select id,lead_id,lead_company_address 
                                                from Elio_snitcher_website_leads 
                                                where is_public = 1 
                                                and is_approved = 1 
                                                and lead_last_seen >= " + daysBefore + " " +
                                                    "order by lead_last_seen desc");
                    }
                    else if (resultCase == 2)
                    {
                        leads = loader.Load(@"Select id,lead_id,lead_company_address 
                                                from Elio_snitcher_website_leads 
                                                where is_public = 1 
                                                and is_approved = 1 
                                                and is_api_lead = 1 
                                                and lead_last_seen >= " + daysBefore + " " +
                                                    "order by lead_last_seen desc");
                    }
                    else
                    {
                        leads = loader.Load(@"Select id,lead_id,lead_company_address 
                                                from Elio_snitcher_website_leads 
                                                where is_public = 1 
                                                and is_approved = 1 
                                                and is_api_lead = 0 
                                                and lead_last_seen >= " + daysBefore + " " +
                                                    "order by lead_last_seen desc");
                    }
                }

                if (leads.Count > 0)
                {
                    List<string> ids = new List<string>();
                    string leadsID = "";

                    foreach (ElioSnitcherWebsiteLeads lead in leads)
                    {
                        string[] address = lead.LeadCompanyAddress.Split(',').ToArray();
                        if (address.Length > 0)
                        {
                            string leadCountry = address[address.Length - 1];
                            if (leadCountry.Length > 0)
                            {
                                if (leadCountry.Contains(userInfo.Country))
                                {
                                    ids.Add(lead.LeadId);
                                    leadsID += lead.LeadId + ",";
                                }
                            }
                        }
                    }

                    if (ids.Count > 0 || leadsID != "")
                    {
                        if (leadsID.EndsWith(","))
                            leadsID = leadsID.Substring(0, leadsID.Length - 1);

                        DataLoader<ElioSnitcherLeadsPageviews> viewsLoader = new DataLoader<ElioSnitcherLeadsPageviews>(session);

                        List<ElioSnitcherLeadsPageviews> pageViews = viewsLoader.Load(@"Select lead_id,url from Elio_snitcher_leads_pageviews where lead_id in (" + leadsID + ")");

                        if (pageViews.Count > 0)
                        {
                            string finalLeadIDs = "";

                            foreach (ElioSnitcherLeadsPageviews page in pageViews)
                            {
                                string[] parameters = page.Url.Split('?');
                                if (parameters.Length > 0)
                                {
                                    string[] path = parameters[0].Split('/').ToArray();
                                    if (path.Length > 0)
                                    {
                                        if (path.Length > 3 && path[path.Length - 1] != "")
                                        {
                                            string product = path[path.Length - 1];
                                            if (product.Length > 0)
                                            {
                                                //string[] userProducts = { "Collaboration", "Conferencing", "Unified Communications", "Unified Messaging", "Video Conferencing", "VoIP", "Mobility", "Team Collaboration", "Cloud Storage", "Virtualization", "Rackspace", "RapidScale", "EvolveIP", "RingCentral", "Cisco", "Webex", "Microsoft Teams", "Masergy", "Yealink", "LogMeIn", "CenturyLink", "sas", "sap" };
                                                string[] userProducts = userInfo.Products.ToLower().Split(',').ToArray();

                                                if (userProducts.Contains(product.ToLower()))
                                                {
                                                    finalLeadIDs += page.LeadId + ",";
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (finalLeadIDs != "")
                            {
                                if (finalLeadIDs.EndsWith(","))
                                    finalLeadIDs = finalLeadIDs.Substring(0, finalLeadIDs.Length - 1);

                                if (leadLastSeen == "-1")
                                {
                                    if (resultCase == 1)
                                        return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo from Elio_snitcher_website_leads where lead_id in (" + finalLeadIDs + ") order by lead_last_seen desc");
                                    else if (resultCase == 2)
                                        return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo from Elio_snitcher_website_leads where lead_id in (" + finalLeadIDs + ") order by lead_last_seen desc");
                                    else
                                        return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo from Elio_snitcher_website_leads where lead_id in (" + finalLeadIDs + ") order by lead_last_seen desc");
                                }
                                else
                                {
                                    string daysBefore = "DATEADD(day, " + (-1 * Convert.ToInt32(leadLastSeen)).ToString() + ", getdate())";

                                    if (resultCase == 1)
                                    {
                                        return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo 
                                                from Elio_snitcher_website_leads 
                                                where lead_id in (" + finalLeadIDs + ") " +
                                                    "and lead_last_seen >= " + daysBefore + " " +
                                                    "order by lead_last_seen desc");
                                    }
                                    else if (resultCase == 2)
                                    {
                                        return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo 
                                                from Elio_snitcher_website_leads 
                                                where lead_id in (" + finalLeadIDs + ") " +
                                                    "and lead_last_seen >= " + daysBefore + " " +
                                                    "order by lead_last_seen desc");
                                    }
                                    else
                                    {
                                        return session.GetDataTable(@"Select id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo 
                                                from Elio_snitcher_website_leads 
                                                where lead_id in (" + finalLeadIDs + ") " +
                                                    "and lead_last_seen >= " + daysBefore + " " +
                                                    "order by lead_last_seen desc");
                                    }
                                }
                            }
                            else
                                return null;
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
        }

        public static DataTable GetSnitcherLeadsTbl(int resultCase, string leadLastSeen, bool isExport, DBSession session)
        {
            if (leadLastSeen == "-1")
            {
                if (!isExport)
                {
                    if (resultCase == 1)
                        return session.GetDataTable(@"Select top 400 id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo from Elio_snitcher_website_leads where is_public = 1 and is_approved = 1 and lead_last_seen >= DATEADD(day, (-1 * 7), getdate()) order by id desc, lead_id desc");
                    else if (resultCase == 2)
                        return session.GetDataTable(@"Select top 400 id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo from Elio_snitcher_website_leads where is_public = 1 and is_approved = 1 and is_api_lead = 1 and lead_last_seen >= DATEADD(day, (-1 * 7), getdate()) order by id desc, lead_id desc");
                    else
                        return session.GetDataTable(@"Select top 400 id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo from Elio_snitcher_website_leads where is_public = 1 and is_approved = 1 and is_api_lead = 0 and lead_last_seen >= DATEADD(day, (-1 * 30), getdate()) order by id desc, lead_id desc");
                }
                else
                {
                    if (resultCase == 1)
                        return session.GetDataTable(@"Select [session_referrer]
                                                                      ,[session_operating_system]
                                                                      ,[session_browser]
                                                                      ,[session_device_type]      
                                                                      ,[session_start]
                                                                      ,[session_duration]
                                                                      ,[session_total_pageviews]      
                                                                      ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                                                      ,[lead_company_name]     
                                                                      ,[lead_company_website]
                                                                      ,[lead_country]
                                                                      ,[lead_city]
                                                                      ,[lead_company_address]
                                                                      ,[lead_company_founded]
                                                                      ,[lead_company_size]
                                                                      ,[lead_company_industry]
                                                                      ,[lead_company_phone]
                                                                      ,[lead_company_email]
                                                                      ,[lead_company_contacts]
                                                                      ,[lead_linkedin_handle]
                                                                      ,[lead_facebook_handle]
                                                                      ,[lead_youtube_handle]
                                                                      ,[lead_instagram_handle]
                                                                      ,[lead_twitter_handle]
                                                                      ,[lead_pinterest_handle]
                                                                      ,[lead_angellist_handle]
                                                                      ,[message] 
                                                                      ,trim(',' from pp.product) as products
                                                            from Elio_snitcher_website_leads l
                                                            cross apply
	                                                        (
		                                                        select pv.product + ',' 
		                                                        from Elio_snitcher_leads_pageviews pv
		                                                        where pv.lead_id = l.lead_id 
		                                                        for xml path('')
	                                                        )pp(product)
                                                            where is_public = 1 and is_approved = 1 order by l.id desc");
                    else if (resultCase == 2)
                        return session.GetDataTable(@"Select [session_referrer]
                                                                      ,[session_operating_system]
                                                                      ,[session_browser]
                                                                      ,[session_device_type]      
                                                                      ,[session_start]
                                                                      ,[session_duration]
                                                                      ,[session_total_pageviews]      
                                                                      ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                                                      ,[lead_company_name]     
                                                                      ,[lead_company_website]
                                                                      ,[lead_country]
                                                                      ,[lead_city]
                                                                      ,[lead_company_address]
                                                                      ,[lead_company_founded]
                                                                      ,[lead_company_size]
                                                                      ,[lead_company_industry]
                                                                      ,[lead_company_phone]
                                                                      ,[lead_company_email]
                                                                      ,[lead_company_contacts]
                                                                      ,[lead_linkedin_handle]
                                                                      ,[lead_facebook_handle]
                                                                      ,[lead_youtube_handle]
                                                                      ,[lead_instagram_handle]
                                                                      ,[lead_twitter_handle]
                                                                      ,[lead_pinterest_handle]
                                                                      ,[lead_angellist_handle]
                                                                      ,[message] 
                                                                      ,trim(',' from pp.product) as products
                                                            from Elio_snitcher_website_leads l
                                                            cross apply
	                                                        (
		                                                        select pv.product + ',' 
		                                                        from Elio_snitcher_leads_pageviews pv
		                                                        where pv.lead_id = l.lead_id 
		                                                        for xml path('')
	                                                        )pp(product) 
                                                            where is_public = 1 and is_approved = 1 and is_api_lead = 1 order by l.id desc");
                    else if (resultCase == 3)
                        return session.GetDataTable(@"Select [session_referrer]
                                                                      ,[session_operating_system]
                                                                      ,[session_browser]
                                                                      ,[session_device_type]      
                                                                      ,[session_start]
                                                                      ,[session_duration]
                                                                      ,[session_total_pageviews]      
                                                                      ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                                                      ,[lead_company_name]     
                                                                      ,[lead_company_website]
                                                                      ,[lead_country]
                                                                      ,[lead_city]
                                                                      ,[lead_company_address]
                                                                      ,[lead_company_founded]
                                                                      ,[lead_company_size]
                                                                      ,[lead_company_industry]
                                                                      ,[lead_company_phone]
                                                                      ,[lead_company_email]
                                                                      ,[lead_company_contacts]
                                                                      ,[lead_linkedin_handle]
                                                                      ,[lead_facebook_handle]
                                                                      ,[lead_youtube_handle]
                                                                      ,[lead_instagram_handle]
                                                                      ,[lead_twitter_handle]
                                                                      ,[lead_pinterest_handle]
                                                                      ,[lead_angellist_handle]
                                                                      ,[message] 
                                                                      ,trim(',' from pp.product) as products
                                                            from Elio_snitcher_website_leads l
                                                            cross apply
	                                                        (
		                                                        select pv.product + ',' 
		                                                        from Elio_snitcher_leads_pageviews pv
		                                                        where pv.lead_id = l.lead_id 
		                                                        for xml path('')
	                                                        )pp(product) 
                                                            where is_public = 1 and is_approved = 1 and is_api_lead = 0 order by l.id desc");
                    else
                        return session.GetDataTable(@"Select [session_referrer]
                                                                      ,[session_operating_system]
                                                                      ,[session_browser]
                                                                      ,[session_device_type]      
                                                                      ,[session_start]
                                                                      ,[session_duration]
                                                                      ,[session_total_pageviews]      
                                                                      ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                                                      ,[lead_company_name]     
                                                                      ,[lead_company_website]
                                                                      ,[lead_country]
                                                                      ,[lead_city]
                                                                      ,[lead_company_address]
                                                                      ,[lead_company_founded]
                                                                      ,[lead_company_size]
                                                                      ,[lead_company_industry]
                                                                      ,[lead_company_phone]
                                                                      ,[lead_company_email]
                                                                      ,[lead_company_contacts]
                                                                      ,[lead_linkedin_handle]
                                                                      ,[lead_facebook_handle]
                                                                      ,[lead_youtube_handle]
                                                                      ,[lead_instagram_handle]
                                                                      ,[lead_twitter_handle]
                                                                      ,[lead_pinterest_handle]
                                                                      ,[lead_angellist_handle]
                                                                      ,[message] 
                                                                      ,trim(',' from pp.product) as products
                                                            from Elio_snitcher_website_leads l
                                                            cross apply
	                                                        (
		                                                        select pv.product + ',' 
		                                                        from Elio_snitcher_leads_pageviews pv
		                                                        where pv.lead_id = l.lead_id 
		                                                        for xml path('')
	                                                        )pp(product) 
                                                            where is_public = 1 and is_approved = 1 and is_api_lead = 2 order by l.id desc");
                }
            }
            else
            {
                string daysBefore = "DATEADD(day, " + (-1 * Convert.ToInt32(leadLastSeen)).ToString() + ", getdate())";

                if (isExport)
                {
                    if (resultCase == 1)
                    {
                        return session.GetDataTable(@"Select [session_referrer]
                                                        ,[session_operating_system]
                                                        ,[session_browser]
                                                        ,[session_device_type]      
                                                        ,[session_start]
                                                        ,[session_duration]
                                                        ,[session_total_pageviews]      
                                                        ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                                        ,[lead_company_name]     
                                                        ,[lead_company_website]
                                                        ,[lead_country]
                                                        ,[lead_city]
                                                        ,[lead_company_address]
                                                        ,[lead_company_founded]
                                                        ,[lead_company_size]
                                                        ,[lead_company_industry]
                                                        ,[lead_company_phone]
                                                        ,[lead_company_email]
                                                        ,[lead_company_contacts]
                                                        ,[lead_linkedin_handle]
                                                        ,[lead_facebook_handle]
                                                        ,[lead_youtube_handle]
                                                        ,[lead_instagram_handle]
                                                        ,[lead_twitter_handle]
                                                        ,[lead_pinterest_handle]
                                                        ,[lead_angellist_handle]
                                                        ,[message]
                                                        ,trim(',' from pp.product) as products
                                                from Elio_snitcher_website_leads l
                                                cross apply
	                                            (
		                                            select pv.product + ',' 
		                                            from Elio_snitcher_leads_pageviews pv
		                                            where pv.lead_id = l.lead_id 
		                                            for xml path('')
	                                            )pp(product) 
                                                where is_public = 1 
                                                and is_approved = 1 
                                                and lead_last_seen >= " + daysBefore + " " +
                                                "order by l.id desc");
                    }
                    else if (resultCase == 2)
                    {
                        return session.GetDataTable(@"Select [session_referrer]
                                                        ,[session_operating_system]
                                                        ,[session_browser]
                                                        ,[session_device_type]      
                                                        ,[session_start]
                                                        ,[session_duration]
                                                        ,[session_total_pageviews]      
                                                        ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                                        ,[lead_company_name]     
                                                        ,[lead_company_website]
                                                        ,[lead_country]
                                                        ,[lead_city]
                                                        ,[lead_company_address]
                                                        ,[lead_company_founded]
                                                        ,[lead_company_size]
                                                        ,[lead_company_industry]
                                                        ,[lead_company_phone]
                                                        ,[lead_company_email]
                                                        ,[lead_company_contacts]
                                                        ,[lead_linkedin_handle]
                                                        ,[lead_facebook_handle]
                                                        ,[lead_youtube_handle]
                                                        ,[lead_instagram_handle]
                                                        ,[lead_twitter_handle]
                                                        ,[lead_pinterest_handle]
                                                        ,[lead_angellist_handle]
                                                        ,[message]
                                                        ,trim(',' from pp.product) as products
                                                from Elio_snitcher_website_leads l
                                                cross apply
	                                            (
		                                            select pv.product + ',' 
		                                            from Elio_snitcher_leads_pageviews pv
		                                            where pv.lead_id = l.lead_id 
		                                            for xml path('')
	                                            )pp(product)
                                                where is_public = 1 
                                                and is_approved = 1 
                                                and is_api_lead = 1 
                                                and lead_last_seen >= " + daysBefore + " " +
                                                    "order by l.id desc");
                    }
                    else if (resultCase == 3)
                    {
                        return session.GetDataTable(@"Select [session_referrer]
                                                        ,[session_operating_system]
                                                        ,[session_browser]
                                                        ,[session_device_type]      
                                                        ,[session_start]
                                                        ,[session_duration]
                                                        ,[session_total_pageviews]      
                                                        ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                                        ,[lead_company_name]     
                                                        ,[lead_company_website]
                                                        ,[lead_country]
                                                        ,[lead_city]
                                                        ,[lead_company_address]
                                                        ,[lead_company_founded]
                                                        ,[lead_company_size]
                                                        ,[lead_company_industry]
                                                        ,[lead_company_phone]
                                                        ,[lead_company_email]
                                                        ,[lead_company_contacts]
                                                        ,[lead_linkedin_handle]
                                                        ,[lead_facebook_handle]
                                                        ,[lead_youtube_handle]
                                                        ,[lead_instagram_handle]
                                                        ,[lead_twitter_handle]
                                                        ,[lead_pinterest_handle]
                                                        ,[lead_angellist_handle]
                                                        ,[message]
                                                        ,trim(',' from pp.product) as products
                                                from Elio_snitcher_website_leads l
                                                cross apply
	                                            (
		                                            select pv.product + ',' 
		                                            from Elio_snitcher_leads_pageviews pv
		                                            where pv.lead_id = l.lead_id 
		                                            for xml path('')
	                                            )pp(product)
                                                where is_public = 1 
                                                and is_approved = 1 
                                                and is_api_lead = 0 
                                                and lead_last_seen >= " + daysBefore + " " +
                                                    "order by l.id desc");
                    }
                    else
                    {
                        return session.GetDataTable(@"Select [session_referrer]
                                                        ,[session_operating_system]
                                                        ,[session_browser]
                                                        ,[session_device_type]      
                                                        ,[session_start]
                                                        ,[session_duration]
                                                        ,[session_total_pageviews]      
                                                        ,CONVERT(varchar,lead_last_seen,6) as lead_last_seen    
                                                        ,[lead_company_name]     
                                                        ,[lead_company_website]
                                                        ,[lead_country]
                                                        ,[lead_city]
                                                        ,[lead_company_address]
                                                        ,[lead_company_founded]
                                                        ,[lead_company_size]
                                                        ,[lead_company_industry]
                                                        ,[lead_company_phone]
                                                        ,[lead_company_email]
                                                        ,[lead_company_contacts]
                                                        ,[lead_linkedin_handle]
                                                        ,[lead_facebook_handle]
                                                        ,[lead_youtube_handle]
                                                        ,[lead_instagram_handle]
                                                        ,[lead_twitter_handle]
                                                        ,[lead_pinterest_handle]
                                                        ,[lead_angellist_handle]
                                                        ,[message]
                                                        ,trim(',' from pp.product) as products
                                                from Elio_snitcher_website_leads l
                                                cross apply
	                                            (
		                                            select pv.product + ',' 
		                                            from Elio_snitcher_leads_pageviews pv
		                                            where pv.lead_id = l.lead_id 
		                                            for xml path('')
	                                            )pp(product)
                                                where is_public = 1 
                                                and is_approved = 1 
                                                and is_api_lead = 2 
                                                and lead_last_seen >= " + daysBefore + " " +
                                                    "order by l.id desc");
                    }
                }
                else
                {
                    if (resultCase == 1)
                    {
                        return session.GetDataTable(@"Select top 400 id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo 
                                                from Elio_snitcher_website_leads 
                                                where is_public = 1 
                                                and is_approved = 1 
                                                and lead_last_seen >= " + daysBefore + " " +
                                                    "order by id desc");
                    }
                    else if (resultCase == 2)
                    {
                        return session.GetDataTable(@"Select top 400 id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo 
                                                from Elio_snitcher_website_leads 
                                                where is_public = 1 
                                                and is_approved = 1 
                                                and is_api_lead = 1 
                                                and lead_last_seen >= " + daysBefore + " " +
                                                    "order by id desc");
                    }
                    else if (resultCase == 3)
                    {
                        return session.GetDataTable(@"Select top 400 id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo 
                                                from Elio_snitcher_website_leads 
                                                where is_public = 1 
                                                and is_approved = 1 
                                                and is_api_lead = 0 
                                                and lead_last_seen >= " + daysBefore + " " +
                                                    "order by id desc");
                    }
                    else
                    {
                        return session.GetDataTable(@"Select top 400 id,lead_id,CONVERT(varchar,lead_last_seen,6) as lead_last_seen,lead_company_name,lead_company_logo 
                                                from Elio_snitcher_website_leads 
                                                where is_public = 1 
                                                and is_approved = 1 
                                                and is_api_lead = 2 
                                                and lead_last_seen >= " + daysBefore + " " +
                                                    "order by id desc");
                    }
                }
            }
        }

        public static DataTable GetSnitcherLeadPageViewsTbl(string leadId, out string lastLeadId, DBSession session)
        {
            lastLeadId = "";

            if (leadId != "")
            {
                return session.GetDataTable(@"Select * from Elio_snitcher_leads_pageviews 
                                            where lead_id = @lead_id
                                            order by action_time,year(action_time),month(action_time),day(action_time)"
                                            , DatabaseHelper.CreateStringParameter("@lead_id", leadId));
            }
            else
            {
                DataTable tbl = session.GetDataTable(@"Select top 1 lead_id from Elio_snitcher_website_leads order by lead_last_seen desc");
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    lastLeadId = tbl.Rows[0]["lead_id"].ToString();
                    if (lastLeadId != "")
                    {
                        return GetSnitcherLeadPageViewsTbl(lastLeadId, out lastLeadId, session);
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
        }

        public static ElioSnitcherLeadsPageviews GetSnitcherLeadPageViewByLeadIdAndUrlOrProduct(string leadId, string url, string product, DBSession session)
        {
            DataLoader<ElioSnitcherLeadsPageviews> loader = new DataLoader<ElioSnitcherLeadsPageviews>(session);

            return loader.LoadSingle(@"Select top 1 * from Elio_snitcher_leads_pageviews where lead_id = @lead_id and (url = @url or product = @product)"
                                    , DatabaseHelper.CreateStringParameter("@lead_id", leadId)
                                    , DatabaseHelper.CreateStringParameter("@url", url)
                                    , DatabaseHelper.CreateStringParameter("@product", product));
        }

        public static ElioSnitcherLeadsPageviews GetSnitcherLeadPageViewByLeadInfoIdAndUrlOrProduct(string leadId, string url, string product, DBSession session)
        {
            DataLoader<ElioSnitcherLeadsPageviews> loader = new DataLoader<ElioSnitcherLeadsPageviews>(session);

            return loader.LoadSingle(@"Select top 1 * from Elio_snitcher_leads_pageviews where lead_id = @lead_id and (url = @url or product = @product)"
                                    , DatabaseHelper.CreateStringParameter("@lead_id", leadId)
                                    , DatabaseHelper.CreateStringParameter("@url", url)
                                    , DatabaseHelper.CreateStringParameter("@product", product));
        }

        public static ElioSnitcherLeadsPageviews GetSnitcherLeadPageViewByLeadId(string leadId, DBSession session)
        {
            DataLoader<ElioSnitcherLeadsPageviews> loader = new DataLoader<ElioSnitcherLeadsPageviews>(session);

            return loader.LoadSingle(@"Select * from Elio_snitcher_leads_pageviews where lead_id = @lead_id"
                                    , DatabaseHelper.CreateStringParameter("@lead_id", leadId));
        }

        public static List<ElioSnitcherLeadsPageviews> GetSnitcherLeadPageViewsByElioWebsiteLeadsId(int elioSnitcherLeadId, DBSession session)
        {
            DataLoader<ElioSnitcherLeadsPageviews> loader = new DataLoader<ElioSnitcherLeadsPageviews>(session);

            return loader.Load(@"Select * from Elio_snitcher_leads_pageviews where elio_website_leads_id = @elio_website_leads_id"
                                    , DatabaseHelper.CreateIntParameter("@elio_website_leads_id", elioSnitcherLeadId));
        }

        public static ElioSnitcherWebsiteLeads GetSnitcherWebsiteLeadByWebsiteIdLeadId(string websiteId, string leadId, DBSession session)
        {
            DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);

            return loader.LoadSingle(@"Select * from Elio_snitcher_website_leads 
                                        where website_id = @website_id
                                        and lead_id = @lead_id
                                        order by lead_last_seen desc"
                                    , DatabaseHelper.CreateStringParameter("@website_id", websiteId)
                                    , DatabaseHelper.CreateStringParameter("@lead_id", leadId));
        }

        public static ElioSnitcherWebsiteLeads GetSnitcherWebsiteLeadByWebsiteIdAndCompanyName(string companyName, DBSession session)
        {
            DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);

            return loader.LoadSingle(@"Select TOP 1 * from Elio_snitcher_website_leads 
                                        where 1 = 1
                                        --and website_id = @website_id
                                        and lead_company_name = @lead_company_name
                                        and is_api_lead = 1
                                        order by lead_last_seen desc"
                                    //, DatabaseHelper.CreateStringParameter("@website_id", websiteId)
                                    , DatabaseHelper.CreateStringParameter("@lead_company_name", companyName));
        }

        public static ElioSnitcherWebsiteLeads GetSnitcherWebsiteLeadByLeadId(string leadId, DBSession session)
        {
            DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);

            if (leadId != "")
            {
                return loader.LoadSingle(@"Select * from Elio_snitcher_website_leads 
                                        where lead_id = @lead_id
                                        and is_approved = 1
                                        and is_public = 1
                                        order by lead_last_seen desc"
                                        , DatabaseHelper.CreateStringParameter("@lead_id", leadId));
            }
            else
            {
                return loader.LoadSingle(@"Select top 1 * from Elio_snitcher_website_leads 
                                            where is_approved = 1
                                            and is_public = 1
                                            order by lead_last_seen desc");
            }
        }

        public static ElioSnitcherWebsiteLeads GetSnitcherWebsiteLeadById(int id, DBSession session)
        {
            DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);

            return loader.LoadSingle(@"Select * from Elio_snitcher_website_leads 
                                        where id = @id"
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioSnitcherWebsites GetSnitcherWebsite(string websiteId, DBSession session)
        {
            DataLoader<ElioSnitcherWebsites> loader = new DataLoader<ElioSnitcherWebsites>(session);

            return loader.LoadSingle(@"Select * from Elio_snitcher_websites where website_id = @website_id"
                                    , DatabaseHelper.CreateStringParameter("@website_id", websiteId));
        }

        public static int GetCollaborationMemberSubAccountIdByPartnerAndVendorResellerId(int partnerId, int vendResId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT sub_account_id
                                                    FROM Elio_collaboration_vendors_members_resellers
                                                    where partner_user_id = @partner_user_id
                                                    and vendor_reseller_id = @vendor_reseller_id"
                                                , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerId)
                                                , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendResId));

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["sub_account_id"]) : -1;
        }

        public static int GetCollaborationMemberSubAccountIdByVendorResellerId(int vendResId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT sub_account_id
                                                    FROM Elio_collaboration_vendors_members_resellers
                                                    where vendor_reseller_id = @vendor_reseller_id"
                                                , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendResId));

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["sub_account_id"]) : -1;
        }

        public static ElioCollaborationVendorsMembersResellers GetCollaborationMemberByVendorResellerId(int vendResId, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsMembersResellers> loader = new DataLoader<ElioCollaborationVendorsMembersResellers>(session);

            return loader.LoadSingle(@"SELECT *
                                        FROM Elio_collaboration_vendors_members_resellers
                                        where vendor_reseller_id = @vendor_reseller_id"
                                , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendResId));
        }

        public static bool IsPartnerAssigned(int assignByUserId, int resellerId, int subAccountId, bool? isSame, DBSession session)
        {
            DataTable table = null;

            if (isSame != null)
            {
                string query = @"SELECT count(id) as count
                                FROM Elio_collaboration_vendors_members_resellers
                                where partner_user_id = @partner_user_id
                                and assign_by_user_id = @assign_by_user_id ";

                query += (Convert.ToBoolean(isSame)) ? " and sub_account_id = @sub_account_id " : " and sub_account_id <> @sub_account_id ";

                table = session.GetDataTable(query
                                            , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId)
                                            , DatabaseHelper.CreateIntParameter("@assign_by_user_id", assignByUserId)
                                            , DatabaseHelper.CreateIntParameter("@sub_account_id", subAccountId));
            }
            else
            {
                string query = @"SELECT count(id) as count
                                FROM Elio_collaboration_vendors_members_resellers
                                where partner_user_id = @partner_user_id
                                and assign_by_user_id = @assign_by_user_id ";

                table = session.GetDataTable(query
                                            , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId)
                                            , DatabaseHelper.CreateIntParameter("@assign_by_user_id", assignByUserId));
            }

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static int GetNewCommentsCountByDealId(int dealId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(rdc.deal_id) as comments 
                                                        from Elio_registration_deals_comments rdc
                                                        inner join Elio_registration_comments rc
	                                                        on rc.id = rdc.comment_id
                                                        where rdc.deal_id = @deal_id
                                                        and rc.is_new = 1
                                                        group by rdc.deal_id"
                                                    , DatabaseHelper.CreateIntParameter("@deal_id", dealId));

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["comments"]) : 0;
        }

        public static DataTable GetDealCommentsTbl(int dealId, DBSession session)
        {
            return session.GetDataTable(@"SELECT rg.last_name + ' ' + rg.first_name as deal_company_name,
                                                rdc.deal_id as deal_id
                                                ,u2.id as vendor_id,u2.company_name as owner_company_name
                                                ,u.email as writter_email,u.company_name,u.company_logo,u.company_type
                                                ,case 
	                                                when isnull(u.first_name, '') = ''  or isnull(u.last_name, '') = '' 
	                                                then u.company_name 
	                                                else u.first_name + ' ' + u.last_name 
	                                                end as person_name
                                                ,c.id as comment_id,c.description,c.user_id,c.is_new,c.is_public, DATEDIFF(day,c.sysdate,getdate()) as days_past, convert(char(5), c.sysdate, 108) written_time,c.sysdate,c.last_update
                                                ,cuf.file_name,cuf.file_path,cuf.is_new as is_file_new
                                            FROM Elio_registration_comments c
                                            inner join Elio_registration_deals_comments rdc
	                                            on rdc.comment_id = c.id
                                            left join Elio_registration_comments_users_files cuf
	                                            on cuf.comment_id = c.id and cuf.is_public = 1
                                            inner join elio_users u
	                                            on u.id = c.user_id
                                            inner join Elio_registration_deals rg
	                                            on rg.id = rdc.deal_id
                                            inner join elio_users u2
	                                            on u2.id = rg.vendor_id
                                            where rdc.deal_id = @deal_id
                                            and u.account_status = 1
                                            order by c.sysdate desc"
                            , DatabaseHelper.CreateIntParameter("@deal_id", dealId));
        }

        public static ElioCurrenciesCountries GetCurrencyCountryByValues(string alpha3, string curId, string currencyId, string currencyName, string name, DBSession session)
        {
            DataLoader<ElioCurrenciesCountries> loader = new DataLoader<ElioCurrenciesCountries>(session);

            return loader.LoadSingle("Select * from Elio_currencies_countries with (nolock) " +
                "where currency_id = @currency_id " +
                "and alpha3 = @alpha3 " +
                "and cur_id = @cur_id " +
                "and currency_name = @currency_name " +
                "and name = @name"
                , DatabaseHelper.CreateStringParameter("@currency_id", currencyId)
                , DatabaseHelper.CreateStringParameter("@alpha3", alpha3)
                , DatabaseHelper.CreateStringParameter("@cur_id", curId)
                , DatabaseHelper.CreateStringParameter("@currency_name", currencyName)
                , DatabaseHelper.CreateStringParameter("@name", name));
        }

        public static ElioCurrenciesCountries GetCurrencyCountryByCurId(string curId, DBSession session)
        {
            DataLoader<ElioCurrenciesCountries> loader = new DataLoader<ElioCurrenciesCountries>(session);

            return loader.LoadSingle("Select * from Elio_currencies_countries with (nolock) where cur_id = @cur_id"
                , DatabaseHelper.CreateStringParameter("@cur_id", curId));
        }

        public static ElioCurrenciesCountries GetCurrencyCountryByCountryName(string country, DBSession session)
        {
            DataLoader<ElioCurrenciesCountries> loader = new DataLoader<ElioCurrenciesCountries>(session);

            return loader.LoadSingle("Select TOP 1 * from Elio_currencies_countries with (nolock) where name like '" + country + "%'"
                , DatabaseHelper.CreateStringParameter("@name", country));
        }

        public static ElioUsersCurrency GetUserCurrency(int userId, DBSession session)
        {
            DataLoader<ElioUsersCurrency> loader = new DataLoader<ElioUsersCurrency>(session);

            return loader.LoadSingle("Select * from Elio_users_currency with (nolock) where user_id = @user_id"
                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static string GetUserCurID(int userId, DBSession session)
        {
            DataLoader<ElioUsersCurrency> loader = new DataLoader<ElioUsersCurrency>(session);

            ElioUsersCurrency userCurrency = loader.LoadSingle("Select cur_id from Elio_users_currency with (nolock) where user_id = @user_id"
                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return userCurrency != null ? userCurrency.CurId : "";
        }

        //public static string GetUserCurrencyCountryCurId(int userId, DBSession session)
        //{
        //    DataLoader<ElioCurrenciesCountries> loader = new DataLoader<ElioCurrenciesCountries>(session);

        //    ElioCurrenciesCountries countryCurrency = loader.LoadSingle(@"Select cc.* 
        //                                                                  from Elio_currencies_countries cc
        //                                                                  inner
        //                                                                  join Elio_users_currency uc
        //                                                                    on uc.cur_id = cc.cur_id
        //                                                                  where user_id = @user_id"
        //                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));

        //    if (countryCurrency != null)
        //        return countryCurrency.CurrencyId;
        //    else
        //        return "";
        //}

        public static ElioCurrenciesCountries GetCurrencyCountriesIJUserCurrencyByUserID(int userId, DBSession session)
        {
            DataLoader<ElioCurrenciesCountries> loader = new DataLoader<ElioCurrenciesCountries>(session);

            return loader.LoadSingle(@"SELECT cc.*
                                        FROM Elio_currencies_countries cc
                                        inner join Elio_users_currency uc
                                        on uc.cur_id = cc.cur_id
                                        where user_id = @user_id"
                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static bool HasUserCurrency(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(id) as count 
                                                        from Elio_users_currency with (nolock) 
                                                        where user_id = @user_id"
                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static List<ElioCurrenciesCountries> GetCurrenciesCountries(DBSession session)
        {
            DataLoader<ElioCurrenciesCountries> loader = new DataLoader<ElioCurrenciesCountries>(session);

            return loader.Load("Select * from Elio_currencies_countries with (nolock) where is_public = 1 order by cur_id");
        }

		public static DataTable GetCurrenciesCountriesForTransfers(DBSession session)
        {
            return session.GetDataTable("Select id, currency_id, name, transfers_per_cent, payouts from Elio_currencies_countries with (nolock) where isnull(transfers_per_cent, '') != '' order by name");
        }
		
        public static List<ElioCurrencies> GetCurrencies(DBSession session)
        {
            DataLoader<ElioCurrencies> loader = new DataLoader<ElioCurrencies>(session);
            return loader.Load("Select * from Elio_currencies with (nolock) where is_public = 1 order by id");
        }

        public static ElioCurrencies GetCurrencyById(string currencyId, DBSession session)
        {
            DataLoader<ElioCurrencies> loader = new DataLoader<ElioCurrencies>(session);
            return loader.LoadSingle("Select * from Elio_currencies with (nolock) where currency_id = @currency_id");
        }

        public static DataTable GetIntentSignalsData(int pageIndex, DBSession session)
        {
            int nextPage = 0;
            int startPage = 0;
            int endPage = 0;

            if (pageIndex > 0)
            {
                nextPage = pageIndex + 1;
                startPage = (pageIndex - 1) * 30;
                endPage = (nextPage - 1) * 30;
            }

            string query = @";with data
                                as
                                (
                                Select ROW_NUMBER() over (order by date_insert desc) as row_index,* from Elio_intent_signals_data with (nolock)
                                where is_public = 1
                                --order by date_insert desc
                                )

                                select * 
                                from data ";

            if (pageIndex > 0)
                query += @"where row_index between " + startPage + " and " + endPage + "";

            return session.GetDataTable(query);
        }

        public static DataTable GetIntentSignalsAndRFQsData(int pageIndex, string country, string product, DBSession session)
        {
            int nextPage = 0;
            int startPage = 0;
            int endPage = 0;

            if (pageIndex > 0)
            {
                nextPage = pageIndex + 1;
                startPage = (pageIndex - 1) * 30;
                endPage = (nextPage - 1) * 30;
            }

            string query = @";with data
                            as
                            (
	                            Select ROW_NUMBER() over (order by lead_last_seen desc) as row_index, 
	                            case when is_api_lead = 1 then 'Lead' else 'RFQ' end  as 'type',
	                            lead_country as 'country', lead_company_industry as 'industry',lead_company_name as company_name,
                                case when isnull(lead_company_logo, '') = '' then '/images/no_logo_company.png' else lead_company_logo end as company_logo,
                                case when lead_company_size != '' then replace(lead_company_size, ' employees', '') 
	                            else '1' end as 'users_count',
                                isnull(pp.product, '') as 'product',
                                lead_last_seen
	                            from Elio_snitcher_website_leads l
	                            cross apply
	                            (
		                            select pv.product + ','
		                            from Elio_snitcher_leads_pageviews pv
		                            where pv.lead_id = l.lead_id ";

            if (product != "")
                query += @" and pv.product = '" + product + "' ";

            query += @" for xml path('')
	                    )pp(product)
	                    where is_public = 1 
	                    and is_approved = 1
	                    and isnull(pp.product, '') != '' ";

            if (country != "")
                query += @" and lead_country = '" + country + "' ";

            query += ") ";

            query += @" select data.row_index,data.type,data.country,data.industry,data.company_name, data.company_logo,data.users_count,data.product  
                        from data ";

            if (pageIndex > 0)
                query += @"where row_index between " + startPage + " and " + endPage + " " +
                          "order by lead_last_seen desc";

            return session.GetDataTable(query);
        }

        public static DataTable GetIntentSignalsAndRFQsDataTop30(int pageIndex, string country, string product, DBSession session)
        {
            //int nextPage = 0;
            //int startPage = 0;
            //int endPage = 0;

            //if (pageIndex > 0)
            //{
            //    nextPage = pageIndex + 1;
            //    startPage = (pageIndex - 1) * 30;
            //    endPage = (nextPage - 1) * 30;
            //}

            string query = @"Select top 30 --ROW_NUMBER() over (order by lead_last_seen desc) as row_index, 
	                            case when is_api_lead = 1 then 'Lead' else 'RFQ' end  as 'type',
	                            lead_country as 'country', lead_company_industry as 'industry',lead_company_name as company_name,
                                case when isnull(lead_company_logo, '') = '' then '/images/no_logo_company.png' else lead_company_logo end as company_logo,
                                case when lead_company_size != '' then replace(lead_company_size, ' employees', '') 
	                            else '1' end as 'users_count',
                                isnull(pp.product, '') as 'product',
                                lead_last_seen
	                            from Elio_snitcher_website_leads l
	                            cross apply
	                            (
		                            select top 1 pv.product
		                            from Elio_snitcher_leads_pageviews pv
		                            where pv.lead_id = l.lead_id ";

            if (product != "")
                query += @" and pv.product = '" + product + "' ";

            query += @")pp(product)
	                    where is_public = 1 
	                    and is_approved = 1
	                    and pp.product != '' ";

            if (country != "")
                query += @" and lead_country = '" + country + "' ";

            //query += ") ";

            //query += @" select data.row_index,data.type,data.country,data.industry,data.company_name, data.company_logo,data.users_count,data.product  
            //            from data ";

            //if (pageIndex > 0)
            //    query += @"where row_index between " + startPage + " and " + endPage + " " +
            
            query += "order by lead_last_seen desc";

            return session.GetDataTable(query);
        }

        public static DataTable GetPartnerTierManagementReportingTbl(int userId, bool isForExport, DBSession session)
        {
            if (isForExport)
            {
                return session.GetDataTable(@"Select u.company_logo, u.company_name, u.country,rep.*
                                            from Elio_tier_management_reporting_users_values_custom rep
                                            inner join elio_users u
	                                            on u.id = rep.user_id
                                            where rep.is_public = 1 
                                            and rep.user_id = @user_id
                                            and rep.is_active = 1
                                            order by rep.user_id,rep.insert_date"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
            }
            else
            {
                return session.GetDataTable(@"Select u.company_logo, u.company_name, u.country,rep.id,rep.insert_date
                                            from Elio_tier_management_reporting_users_values_custom rep
                                            inner join elio_users u
	                                            on u.id = rep.user_id
                                            where rep.is_public = 1 
                                            and rep.user_id = @user_id
                                            and rep.is_active = 1
                                            order by rep.user_id,rep.insert_date"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
            }
        }

        public static ElioTierManagementReportingUsersValuesCustom GetPartnerTierManagementReportById(int userId, int reportId, DBSession session)
        {
            DataLoader<ElioTierManagementReportingUsersValuesCustom> loader = new DataLoader<ElioTierManagementReportingUsersValuesCustom>(session);

            return loader.LoadSingle(@"Select *
                                            from Elio_tier_management_reporting_users_values_custom 
                                            where 1 = 1 
                                            and user_id = @user_id 
                                            and id = @id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@id", reportId));
        }

        public static void DeleteUserTierManagementReportById(int id, int userId, DBSession session)
        {
            session.ExecuteQuery(@"DELETE from Elio_tier_management_reporting_users_values_custom where id=@id and user_id = @user_id"
                                 , DatabaseHelper.CreateIntParameter("@id", id)
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static string GetTierManagementIndustryDescriptionByid(int industryId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select id, industry_description 
                                                        from Elio_tier_management_industries_custom 
                                                        where id = @id"
                                                , DatabaseHelper.CreateIntParameter("@id", industryId));

            return (table.Rows.Count > 0) ? table.Rows[0]["industry_description"].ToString() : "";
        }

        public static DataTable GetTierManagementIndustriesTbl(DBSession session)
        {
            return session.GetDataTable(@"Select id, industry_description from Elio_tier_management_industries_custom where is_public=1 order by industry_description");
        }

        public static List<ElioTierManagementCustom> GetTierManagementCustomSettings(int userId, DBSession session)
        {
            DataLoader<ElioTierManagementCustom> loader = new DataLoader<ElioTierManagementCustom>(session);

            return loader.Load(@"SELECT * FROM  Elio_tier_management_custom
                                 WHERE is_active = 1 and user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetTierManagementCustomSettingsTbl(int userId, DBSession session)
        {
            return session.GetDataTable(@"SELECT * FROM  Elio_tier_management_custom
                                         WHERE is_active = 1 and user_id = @user_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioTierManagementGoalsCustom GetCurrentTierManagementGoal(DBSession session)
        {
            DataLoader<ElioTierManagementGoalsCustom> loader = new DataLoader<ElioTierManagementGoalsCustom>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_tier_management_goals_custom 
                                        where getdate() between date_from and date_to");
        }

        public static List<ElioTierManagementUsersGoals> GetVendorTierManagementGoalsByPartner(int vendorId, int partnerId, string year, DBSession session)
        {
            DataLoader<ElioTierManagementUsersGoals> loader = new DataLoader<ElioTierManagementUsersGoals>(session);

            return loader.Load(@"SELECT * 
                                    FROM Elio_tier_management_users_goals
                                    where vendor_id = @vendor_id
                                    and partner_id = @partner_id
                                    and year = @year"
                            , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId)
                            , DatabaseHelper.CreateStringParameter("@year", year));
        }

        public static string GetVendorTierManagementCurrentGoalForPartner(int vendorId, int resellerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT goal_value
                                    FROM Elio_tier_management_users_goals_value ugv
                                    INNER JOIN Elio_tier_management_users_goals ug
	                                    on ug.id = ugv.goal_id
                                    where vendor_id = @vendor_id
                                    and partner_id = @partner_id
                                    and year = year(getdate())
                                    and (month(date_from) <= month(getdate()) and month(date_to) > month(getdate()))"
                                , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                , DatabaseHelper.CreateIntParameter("@partner_id", resellerId));

            return table.Rows.Count > 0 ? table.Rows[0]["goal_value"].ToString() : "0";
        }

        public static ElioTierManagementUsersGoalsValue GetUserTierManagementGoalValues(int goalId, DBSession session)
        {
            DataLoader<ElioTierManagementUsersGoalsValue> loader = new DataLoader<ElioTierManagementUsersGoalsValue>(session);

            return loader.LoadSingle(@"SELECT *
                                          FROM Elio_tier_management_users_goals_value  
                                        where goal_id = @goal_id"
                                , DatabaseHelper.CreateIntParameter("@goal_id", goalId));
        }

        public static ElioTierManagementUsersGoalsValue GetUserTierManagementGoalValuesByGoalId(int goalId, DBSession session)
        {
            DataLoader<ElioTierManagementUsersGoalsValue> loader = new DataLoader<ElioTierManagementUsersGoalsValue>(session);

            return loader.LoadSingle(@"SELECT *
                                          FROM Elio_tier_management_users_goals_value  
                                        where goal_id = @goal_id"
                                , DatabaseHelper.CreateIntParameter("@goal_id", goalId));
        }

        public static List<ElioTierManagementGoalsCustom> GetVendorTierManagementGoals(DBSession session)
        {
            DataLoader<ElioTierManagementGoalsCustom> loader = new DataLoader<ElioTierManagementGoalsCustom>(session);

            return loader.Load(@"SELECT * FROM Elio_tier_management_goals_custom");
        }

        public static ElioTierManagementGoalsUsersValueCustom GetUserTierManagementGoalValues(int partnerId, int goalId, DBSession session)
        {
            DataLoader<ElioTierManagementGoalsUsersValueCustom> loader = new DataLoader<ElioTierManagementGoalsUsersValueCustom>(session);

            return loader.LoadSingle(@"SELECT *
                                          FROM Elio_tier_management_goals_users_value_custom  
                                        where user_id = @user_id
                                        and goal_id = @goal_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", partnerId)
                                , DatabaseHelper.CreateIntParameter("@goal_id", goalId));
        }

        public static ElioTierManagementGoalsUsersValueCustom GetUserTierManagementGoalValuesByID(int partnerId, int id, DBSession session)
        {
            DataLoader<ElioTierManagementGoalsUsersValueCustom> loader = new DataLoader<ElioTierManagementGoalsUsersValueCustom>(session);

            return loader.LoadSingle(@"SELECT *
                                          FROM Elio_tier_management_goals_users_value_custom  
                                        where user_id = @user_id
                                        and id = @id"
                                , DatabaseHelper.CreateIntParameter("@user_id", partnerId)
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static bool HasUserTierManagementCriteria(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                      FROM Elio_tier_management_users_criteria_scores_custom
                                                      where user_id = @user_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool HasUserTierManagementGoalsByPartnerYear(int vendorId, int partnerId, string year, DBSession session)
        {
            string strQuery = @"SELECT count(id) as count
                                FROM Elio_tier_management_users_goals
                                where vendor_id = @vendor_id
                                and partner_id = @partner_id 
                                and year = @year";
           
            DataTable table = session.GetDataTable(strQuery
                                        , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                        , DatabaseHelper.CreateIntParameter("@partner_id", partnerId)
                                        , DatabaseHelper.CreateStringParameter("@year", year));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static int UpdateUserTierManagementCriteriaScores(int userId, int oldValue, int newValue, DBSession session)
        {
            return session.ExecuteQuery(@"update Elio_tier_management_users_criteria_scores_custom
                                                        set criteria_values_id = @newValue
                                                        ,update_date = getdate()
                                                        ,update_user = @update_user
                                                        where user_id = @user_id
                                                        and criteria_values_id = @oldValue"
                                        , DatabaseHelper.CreateIntParameter("@newValue", newValue)
                                        , DatabaseHelper.CreateIntParameter("@oldValue", oldValue)
                                        , DatabaseHelper.CreateIntParameter("@update_user", userId)
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static int DeleteUserTierManagementCriteriaScores(int userId, int criteriaValueId, DBSession session)
        {
            return session.ExecuteQuery(@"Delete from Elio_tier_management_users_criteria_scores_custom                                                       
                                            where user_id = @user_id
                                            and criteria_values_id = @criteria_values_id"
                                        , DatabaseHelper.CreateIntParameter("@criteria_values_id", criteriaValueId)
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataSet GetTierManagementCriteriaValues(DBSession session)
        {
            DataTable tbl = new DataTable();
            DataSet ds = new DataSet();

            for (int i = 1; i < 8; i++)
            {
                if (i > 1)
                {
                    tbl = session.GetDataTable(@"SELECT id,criteria_id,value,weight 
                                                    FROM Elio_tier_management_criteria_values_custom
                                                    where criteria_id = @criteria_id"
                                    , DatabaseHelper.CreateIntParameter("criteria_id", i));
                }
                else
                {
                    tbl = session.GetDataTable(@"SELECT id,criteria_id,value,weight 
                                                    FROM Elio_tier_management_criteria_values_custom
                                                    where criteria_id = @criteria_id and id <= 5"
                                    , DatabaseHelper.CreateIntParameter("criteria_id", i));
                }

                if (tbl.Rows.Count > 0)
                    ds.Tables.Add(tbl);
            }

            return ds;
        }

        public static int GetUserTierCriteriaValueID(int userId, int criteriaId, DBSession session)
        {
            DataTable table = null;
            if (criteriaId == 1)
            {
                table = session.GetDataTable(@"SELECT sc.criteria_values_id as id
                                                    FROM Elio_tier_management_users_criteria_scores_custom sc
                                                    inner join Elio_tier_management_criteria_values_custom val
	                                                on val.id = sc.criteria_values_id
                                                    where user_id = @user_id
	                                                and val.criteria_id = @criteria_id
	                                                and val.id <= 5
	                                                and is_active = 1"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@criteria_id", criteriaId));
            }
            else
            {
                table = session.GetDataTable(@"SELECT sc.criteria_values_id as id
                                                    FROM Elio_tier_management_users_criteria_scores_custom sc
                                                    inner join Elio_tier_management_criteria_values_custom val
	                                                on val.id = sc.criteria_values_id
                                                    where user_id = @user_id
	                                                and val.criteria_id = @criteria_id
	                                                and is_active = 1"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@criteria_id", criteriaId));
            }

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : 0;
        }

        public static int GetUserTierCriteriaValueIDChecked(int userId, int criteriaId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT sc.criteria_values_id as id
                                                    FROM Elio_tier_management_users_criteria_scores_custom sc
                                                    inner join Elio_tier_management_criteria_values_custom val
	                                                on val.id = sc.criteria_values_id
                                                    where user_id = @user_id
	                                                and val.criteria_id = 1
	                                                and val.id = @criteria_id
	                                                and is_active = 1"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@criteria_id", criteriaId));

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : 0;
        }

        public static string GetUserTierCriteriaValueWeightAverage(int userId, string criteriaIds, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT sum(cast([weight] as int))/7 as avg
                                                      FROM Elio_tier_management_criteria_values_custom
                                                      where id in (" + criteriaIds + ")");

            return table != null && table.Rows.Count > 0 ? table.Rows[0]["avg"].ToString() : "0";
        }

        public static void UpdateUserCollaborationTierStatus(int vendorId, int resellerId, string tierStatus, DBSession session)
        {
            session.ExecuteQuery(@"update Elio_collaboration_vendors_resellers
                                        set tier_status = @tier_status,
                                        last_updated = getdate()
                                    where master_user_id = @master_user_id
                                    and partner_user_id = @partner_user_id"
                , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId)
                , DatabaseHelper.CreateStringParameter("@tier_status", tierStatus));
        }

        public static string GetUserTierDescription(int userId, DBSession session)
        {
            string strQuery = "declare @val int ";

            strQuery += @"; with weight
                            as
                            (
	                            SELECT sum(cast([weight] as int))/7 as avg
	                            FROM Elio_tier_management_criteria_values_custom
	                            where id in 
	                            (
	                            select criteria_values_id
	                            from Elio_tier_management_users_criteria_scores_custom
	                            where user_id = @user_id
	                            )
                            )

	                        select @val = avg from weight

	                        if(@val >= 50)
	                        begin

		                        select 
			                        description
		                        from Elio_tier_management_custom
		                        where from_value <= @val and to_value >= @val

	                        end
	                        else
	                        begin

		                        select description
		                        FROM Elio_tier_management_custom
		                        where id = 1

	                        end";
            DataTable table = session.GetDataTable(strQuery
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return table != null && table.Rows.Count > 0 ? table.Rows[0]["description"].ToString() : "";
        }
        public static bool ExistTierUserPermissionByFormId(int userId, int tierId, int form, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count from Elio_tier_management_users_permissions with (nolock) 
                                                        where user_id = @user_id
                                                        and form_id = @form_id
                                                        and tier_id = @tier_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@form_id", form)
                                                    , DatabaseHelper.CreateIntParameter("@tier_id", tierId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }
        public static bool ExistTierUserById(int userId, int tierId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count from Elio_tier_management_users_settings with (nolock) 
                                                        where id = @id
                                                        and user_id = @user_id"
                                                   , DatabaseHelper.CreateIntParameter("@id", tierId)
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool DeleteTierUserSettingsById(int userId, int tierId, DBSession session)
        {
            int row = session.ExecuteQuery(@"Delete from Elio_tier_management_users_settings 
                                                      where id = @id
                                                      and user_id = @user_id"
                                                   , DatabaseHelper.CreateIntParameter("@id", tierId)
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return row > -1 ? true : false;
        }

        public static bool DeleteTierUserPermissionsForTierId(int userId, int tierId, DBSession session)
        {
            int row = session.ExecuteQuery(@"Delete from Elio_tier_management_users_permissions 
                                                      where tier_id = @tier_id
                                                      and user_id = @user_id"
                                                   , DatabaseHelper.CreateIntParameter("@tier_id", tierId)
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return row > -1 ? true : false;
        }

        public static bool DeleteUserCurrencyByCurId(int userId, int curId, DBSession session)
        {
            int row = session.ExecuteQuery(@"DELETE from Elio_users_currency where cur_id = @cur_id and user_id = @user_id"
                                                   , DatabaseHelper.CreateIntParameter("@cur_id", curId)
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return row > -1 ? true : false;
        }

        public static bool DeleteUserCurrency(int userId, DBSession session)
        {
            int row = session.ExecuteQuery(@"DELETE from Elio_users_currency where user_id = @user_id"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return row > -1 ? true : false;
        }

        public static bool DeleteTierUserFormPermissionsByTierId(int userId, int formId, int tierId, DBSession session)
        {
            int row = session.ExecuteQuery(@"Delete from Elio_tier_management_users_permissions 
                                                      where tier_id = @tier_id
                                                      and user_id = @user_id
                                                      and form_id = @form_id"
                                                   , DatabaseHelper.CreateIntParameter("@tier_id", tierId)
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@form_id", formId));

            return row > -1 ? true : false;
        }

        public static bool ExistTierByName(string tierDescription, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_tier_management_users_settings with (nolock) where description = @description"
                                                   , DatabaseHelper.CreateStringParameter("@description", tierDescription));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistTiersByUser(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count from Elio_tier_management_users_settings with (nolock) 
                                                        where user_id = @user_id
                                                        and is_active = 1
                                                        and is_public = 1"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static List<ElioTierManagementForms> GetTierManagementForms(DBSession session)
        {
            DataLoader<ElioTierManagementForms> loader = new DataLoader<ElioTierManagementForms>(session);

            return loader.Load(@"SELECT * FROM Elio_tier_management_forms WHERE is_active = 1");
        }

        public static List<int> GetTierManagementFormsIdsArray(DBSession session)
        {
            List<int> ids = new List<int>();
            DataTable table = session.GetDataTable(@"SELECT id FROM Elio_tier_management_forms WHERE is_active = 1");

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (Convert.ToInt32(table.Rows[i]["id"]) > 0)
                        ids.Add(Convert.ToInt32(table.Rows[i]["id"]));
                }
            }

            return ids;
        }

        public static List<ElioTierManagementUsersSettings> GetTierManagementUserSettings(int userId, DBSession session)
        {
            DataLoader<ElioTierManagementUsersSettings> loader = new DataLoader<ElioTierManagementUsersSettings>(session);

            return loader.Load(@"SELECT * FROM Elio_tier_management_users_settings 
                                    WHERE is_active = 1 and is_public = 1 and user_id = @user_id
                                    order by from_volume"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static string GetTierDescriptionById(int userId, int tierId, DBSession session)
        {
            DataLoader<ElioTierManagementUsersSettings> loader = new DataLoader<ElioTierManagementUsersSettings>(session);

            DataTable table = session.GetDataTable(@"SELECT top 1 description FROM Elio_tier_management_users_settings 
                                    WHERE user_id = @user_id and id = @id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@id", tierId));

            return (table.Rows.Count > 0) ? table.Rows[0]["description"].ToString() : "";
        }

        public static string GetTrainingGroupNameById(int userId, int groupId, DBSession session)
        {
            DataLoader<ElioTierManagementUsersSettings> loader = new DataLoader<ElioTierManagementUsersSettings>(session);

            DataTable table = session.GetDataTable(@"SELECT top 1 training_group_name FROM Elio_users_training_groups 
                                    WHERE user_id = @user_id and id = @id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@id", groupId));

            return (table.Rows.Count > 0) ? table.Rows[0]["training_group_name"].ToString() : "";
        }

        public static DataTable GetTierManagementUserSettingsTbl(int userId, DBSession session)
        {
            return session.GetDataTable(@"SELECT * FROM Elio_tier_management_users_settings 
                                            WHERE is_active = 1 and is_public = 1 and user_id = @user_id
                                            order by from_volume"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static bool HasTierManagementUserSettings(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count FROM Elio_tier_management_users_settings 
                                    WHERE is_active = 1 and is_public = 1 and user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static int GetTierManagementUserSettingsPeriodId(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT top 1 period_id FROM Elio_tier_management_users_settings 
                                    WHERE is_active = 1 and is_public = 1 and user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["period_id"]) : 0;
        }

        public static List<ElioTierManagementUsersSettings> GetTierManagementUserSettingsByVolumeAsc(int userId, DBSession session)
        {
            DataLoader<ElioTierManagementUsersSettings> loader = new DataLoader<ElioTierManagementUsersSettings>(session);

            return loader.Load(@"SELECT * FROM Elio_tier_management_users_settings 
                                    WHERE is_active = 1 and is_public = 1 and user_id = @user_id
                                    order by from_volume asc"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioTierManagementUsersSettings GetTierManagementUserSettingsById(int userId, int id, DBSession session)
        {
            DataLoader<ElioTierManagementUsersSettings> loader = new DataLoader<ElioTierManagementUsersSettings>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_tier_management_users_settings 
                                    WHERE id = @id and user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@id", id)
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static int GetTierManagementUserSettingsByDescription(int userId, string description, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select id as tier_id from Elio_tier_management_users_settings with (nolock) 
                                                        where user_id = @user_id
                                                        and description = @description"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@description", description));

            return table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["tier_id"]) : 0;
        }

        public static bool ExistTierManagementUserSettingsPermissionByTierDescription(int userId, string description, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select COUNT(id) as count from Elio_tier_management_users_settings with (nolock) 
                                                        where user_id = @user_id
                                                        and description = @description"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@description", description));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool HasTierManagementUserFormPermission(int userId, int formId, string description, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count
                                                        from Elio_tier_management_users_permissions
                                                        where user_id = @user_id
                                                        and form_id = @form_id
                                                        and is_active = 1
                                                        and tier_id in
                                                        (
	                                                        select id 
	                                                        from Elio_tier_management_users_settings with (nolock) 
	                                                        where user_id = @user_id
	                                                        and description = @description
                                                        )"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@form_id", formId)
                                                   , DatabaseHelper.CreateStringParameter("@description", description));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static ElioTierManagementUsersPermissions GetTierManagementUserPermissionsByTierId(int tierId, int userId, DBSession session)
        {
            DataLoader<ElioTierManagementUsersPermissions> loader = new DataLoader<ElioTierManagementUsersPermissions>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_tier_management_users_permissions 
                                    WHERE tier_id = @tier_id and user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@tier_id", tierId)
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static bool ExistUserRegistrationIntegrationDescription(int userId, string description, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select COUNT(uri.id) as count 
                                                        from Elio_users_registration_integrations uri with (nolock)
                                                        inner join Elio_registration_integrations ri on uri.reg_integrations_id = ri.id 
                                                        where user_id = @user_id AND ri.description = @description"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateStringParameter("@description", description));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserRegistrationIntegration(int userId, int integrationId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select COUNT(id) as count 
                                                        from Elio_users_registration_integrations
                                                        where user_id = @user_id AND reg_integrations_id = @reg_integrations_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@reg_integrations_id", integrationId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserRegistrationProductsDescription(int userId, string description, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select COUNT(urp.id) as count 
                                                        from Elio_users_registration_products urp with (nolock)
                                                        inner join Elio_registration_products rp on urp.reg_products_id = rp.id 
                                                        where user_id = @user_id AND rp.description = @description"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateStringParameter("@description", description));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserRegistrationProduct(int userId, int productId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select COUNT(id) as count 
                                                        from Elio_users_registration_products
                                                        where user_id = @user_id AND reg_products_id = @reg_products_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@reg_products_id", productId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static List<ElioUsersRegistrationProducts> GetUserRegistrationProductsByProductId(int Id, DBSession session)
        {
            DataLoader<ElioUsersRegistrationProducts> loader = new DataLoader<ElioUsersRegistrationProducts>(session);

            return loader.Load(@"SELECT * FROM Elio_users_registration_products
                                        where id = @id"
                                    , DatabaseHelper.CreateIntParameter("@id", Id));
        }

        public static int DeleteUserRegistrationProductAndProduct(int productId, DBSession session)
        {
            int row = session.ExecuteQuery(@"DELETE FROM Elio_users_registration_products
                                                        where reg_products_id = @reg_products_id"
                                        , DatabaseHelper.CreateIntParameter("@reg_products_id", productId));

            if (row >= 0)
            {
                row = session.ExecuteQuery(@"DELETE FROM Elio_registration_products
                                                        where id = @id"
                                        , DatabaseHelper.CreateIntParameter("@id", productId));
            }

            return row;
        }

        public static ElioRegistrationIntegrations GetRegistrationIntegrationsIDByDescription(string description, DBSession session)
        {
            DataLoader<ElioRegistrationIntegrations> loader = new DataLoader<ElioRegistrationIntegrations>(session);

            return loader.LoadSingle(@"SELECT TOP 1 id, description FROM Elio_registration_integrations WHERE description = @description"
                                    , DatabaseHelper.CreateStringParameter("@description", description));
        }

        public static List<ElioRegistrationIntegrations> GetUserRegistrationIntegrations(int userId, DBSession session)
        {
            DataLoader<ElioRegistrationIntegrations> loader = new DataLoader<ElioRegistrationIntegrations>(session);

            return loader.Load(@"SELECT ri.* FROM Elio_registration_integrations ri
                                        inner join Elio_users_registration_integrations uri
	                                        on ri.id = uri.reg_integrations_id
                                        where user_id = @user_id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioRegistrationProducts GetRegistrationProductsIDByDescription(string description, DBSession session)
        {
            DataLoader<ElioRegistrationProducts> loader = new DataLoader<ElioRegistrationProducts>(session);

            return loader.LoadSingle(@"SELECT TOP 1 id, description FROM Elio_registration_products WHERE description = @description"
                                    , DatabaseHelper.CreateStringParameter("@description", description));
        }

        public static List<ElioRegistrationProducts> GetRegistrationProductsDescriptionByUserId(int userId, DBSession session)
        {
            DataLoader<ElioRegistrationProducts> loader = new DataLoader<ElioRegistrationProducts>(session);

            return loader.Load(@"SELECT rp.id,rp.description
                                  FROM Elio_registration_products rp
                                  inner join Elio_users_registration_products urp
	                                on urp.reg_products_id = rp.id
                                where user_id = @user_id"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetRegistrationIntegrationsTable(DBSession session)
        {
            return session.GetDataTable(@"SELECT [id], [description] FROM [Elio_registration_integrations] WHERE [is_public] = 1 order by description");
        }

        public static List<ElioRegistrationProducts> GetRegistrationProducts(DBSession session)
        {
            DataLoader<ElioRegistrationProducts> loader = new DataLoader<ElioRegistrationProducts>(session);

            return loader.Load(@"SELECT [id], [description] FROM [Elio_registration_products] WHERE [is_public] = 1 order by description");
        }

        public static List<ElioRegistrationProducts> GetRegistrationProductsOverId(int id, DBSession session)
        {
            DataLoader<ElioRegistrationProducts> loader = new DataLoader<ElioRegistrationProducts>(session);

            return loader.Load(@"SELECT [id], [description] FROM [Elio_registration_products] WHERE [is_public] = 1 and id > @id order by id"
                        , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static DataTable GetRegistrationProductsTable(DBSession session)
        {
            return session.GetDataTable(@"SELECT [id], [description] FROM [Elio_registration_products] order by id");
        }

        public static DataTable GetRegistrationProductsTable(int fromRow, int toRow, DBSession session)
        {
            string query = @";with a
                            as
                            (
                            SELECT ROW_NUMBER() over(order by id) as rows,[id]
                                    ,[description] + ' partners' as description
                                    ,'/profile/channel-partners/'+ LOWER(REPLACE(REPLACE(description, ' ', '_'), '&', 'and')) as link
                                FROM [Elioplus_DB].[dbo].[Elio_registration_products]
                                where is_public = 1
                            )

                            select * from a
                            where rows between " + fromRow + " and " + toRow + "" +
                            "order by description";

            return session.GetDataTable(query);
        }

        public static DataSet GetRegistrationProductsDataSet(DBSession session)
        {
            DataSet ds = new DataSet();

            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                          FROM [Elioplus_DB].[dbo].[Elio_registration_products]
                                                          where is_public = 1");

            if (table.Rows.Count > 0)
            {
                int allRows = Convert.ToInt32(table.Rows[0]["count"]);

                if (allRows > 0)
                {
                    int rowsA = allRows / 4;
                    int rowsB = rowsA + 4 + rowsA;
                    int rowsC = rowsB + 4 + rowsA;
                    int rowsD = rowsC + 4;

                    DataTable tbl1 = GetRegistrationProductsTable(1, rowsA, session);
                    DataTable tbl2 = GetRegistrationProductsTable(rowsA + 1, rowsB, session);
                    DataTable tbl3 = GetRegistrationProductsTable(rowsB + 1, rowsC, session);
                    DataTable tbl4 = GetRegistrationProductsTable(rowsC + 1, allRows - 1, session);
                                        
                    ds.Tables.Add(tbl1);
                    ds.Tables.Add(tbl2);
                    ds.Tables.Add(tbl3);
                    ds.Tables.Add(tbl4);
                }
            }

            return ds;
        }

        public static DataTable GetRegistrationProductsTbl(string descriptionLike, string orderBy, DBSession session)
        {
            string strQuery = @"SELECT [id], [description], [is_public], [sysdate] FROM [Elio_registration_products] WHERE 1 = 1 ";

            if (descriptionLike != "")
                strQuery += " AND description like '" + descriptionLike + "%' ";

            if (orderBy != "")
                strQuery += " order by " + orderBy;
            else
                strQuery += " order by id";

            return session.GetDataTable(strQuery);
        }

        public static bool ExistCrmUserLead(int crmIntegrationId, int leadId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS count FROM Elio_crm_user_leads 
                                                     WHERE crm_integration_id = @crm_integration_id 
                                                     AND lead_id = @lead_id"
                                                     , DatabaseHelper.CreateIntParameter("@crm_integration_id", crmIntegrationId)
                                                     , DatabaseHelper.CreateIntParameter("@lead_id", leadId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool ExistCrmUserDeal(int crmIntegrationId, int dealId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS count FROM Elio_crm_user_deals 
                                                     WHERE crm_integration_id = @crm_integration_id 
                                                     AND deal_id = @deal_id"
                                                     , DatabaseHelper.CreateIntParameter("@crm_integration_id", crmIntegrationId)
                                                     , DatabaseHelper.CreateIntParameter("@deal_id", dealId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static List<ElioCrmIntegrations> GetPublicCrmIntegrations(DBSession session)
        {
            DataLoader<ElioCrmIntegrations> loader = new DataLoader<ElioCrmIntegrations>(session);
            return loader.Load("Select * from Elio_crm_integrations where is_public = 1 order by crm_name asc");
        }

        public static DataTable GetAllCrmIntegrationsTbl(DBSession session)
        {
            return session.GetDataTable("Select * from Elio_crm_integrations where is_public = 1 order by id");
        }

        public static DataTable GetAllCrmUserIntegrationsTbl(int userId, DBSession session)
        {
            return session.GetDataTable(@"Select ci.*,cui.id as crm_user_integrations_id, cui.user_api_key,cui.user_id 
                                            from Elio_crm_user_integrations  cui
                                            inner join Elio_crm_integrations ci
	                                            on ci.id = cui.crm_integration_id
                                            where user_id = @user_id
                                            order by cui.id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void DeleteCrmUserIntegration(int userId, int crmId, DBSession session)
        {
            session.GetDataTable(@"Delete from Elio_crm_user_integrations 
                                    where user_id = @user_id
                                    and crm_integration_id = @crm_integration_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@crm_integration_id", crmId));
        }

        public static List<ElioCrmIntegrations> GetAllCrmIntegrations(DBSession session)
        {
            DataLoader<ElioCrmIntegrations> loader = new DataLoader<ElioCrmIntegrations>(session);
            return loader.Load("Select * from Elio_crm_integrations order by id");
        }

        public static ElioCrmUserIntegrations GetUserCrmIntegrationByCrmID(int userId, int crmId, DBSession session)
        {
            DataLoader<ElioCrmUserIntegrations> loader = new DataLoader<ElioCrmUserIntegrations>(session);
            return loader.LoadSingle(@"Select * from Elio_crm_user_integrations 
                                       where crm_integration_id = @crm_integration_id 
                                       and user_id = @user_id"
                                     , DatabaseHelper.CreateIntParameter("@crm_integration_id", crmId)
                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioCrmUserIntegrations GetUserCrmIntegration(int userId, DBSession session)
        {
            DataLoader<ElioCrmUserIntegrations> loader = new DataLoader<ElioCrmUserIntegrations>(session);
            return loader.LoadSingle(@"Select top 1 * from Elio_crm_user_integrations where user_id = @user_id"
                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioCrmUserIntegrations> GetUserCrmIntegrations(int userId, DBSession session)
        {
            DataLoader<ElioCrmUserIntegrations> loader = new DataLoader<ElioCrmUserIntegrations>(session);
            return loader.Load(@"Select * from Elio_crm_user_integrations where user_id = @user_id"
                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetPublicCrmIntegrationsTable(DBSession session)
        {
            return session.GetDataTable(@"Select * from Elio_crm_integrations where is_public=1 order by crm_name asc");
        }

        public static bool HasUserCrmIntegrationByCrmID(int userId, int crmId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select count(*) as count 
                                                      from Elio_crm_user_integrations 
                                                      where crm_integration_id = @crm_integration_id 
                                                      and user_id = @user_id"
                                                     , DatabaseHelper.CreateIntParameter("@crm_integration_id", crmId)
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool HasUserCrmIntegrationOrRegisterKey(int userId, out string apiKey, DBSession session)
        {
            apiKey = "";
            DataTable table = session.GetDataTable(@"select isnull(user_api_key, '') as user_api_key
                                                      from Elio_crm_user_integrations 
                                                      where user_id = @user_id"
                                                  , DatabaseHelper.CreateIntParameter("@user_id", userId));
            if (table.Rows.Count == 0)
                return false;
            else if (table.Rows.Count == 1)
            {
                apiKey = table.Rows[0]["user_api_key"].ToString();
                return true;
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["user_api_key"].ToString() != "")
                        apiKey = row["user_api_key"].ToString();
                }

                apiKey = apiKey == "" ? "0" : apiKey;
                return true;
            }
        }

        public static bool HasUserCrmApiKey(int userId, int crmId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select ISNULL(user_api_key,'')
                                                      from Elio_crm_user_integrations 
                                                      where crm_integration_id = @crm_integration_id 
                                                      and user_id = @user_id"
                                                     , DatabaseHelper.CreateIntParameter("@crm_integration_id", crmId)
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (table.Rows.Count > 0 && !string.IsNullOrEmpty(table.Rows[0]["user_api_key"].ToString())) ? true : false;
        }

        public static string GetUserCrmApiKey(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select ISNULL(user_api_key,'')
                                                      from Elio_crm_user_integrations 
                                                      where user_id = @user_id"
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (table.Rows.Count > 0) ? table.Rows[0]["user_api_key"].ToString() : "";
        }

        public static bool HasEmailSettingsAccess(string email, int emailNotificationId, bool isSubAccount, DBSession session)
        {
            DataTable table = new DataTable();
            bool isPublic = false;

            DataTable isPublicTbl = session.GetDataTable(@"select is_public
	                                                        from Elio_email_notifications en
	                                                        where en.id = @email_notification_id"
                                                , DatabaseHelper.CreateIntParameter("@email_notification_id", emailNotificationId));

            if (isPublicTbl != null && isPublicTbl.Rows.Count > 0)
                isPublic = Convert.ToInt32(isPublicTbl.Rows[0]["is_public"]) == 1;
            else
                return false;

            if (isPublic)
            {
                if (!isSubAccount)
                {
                    table = session.GetDataTable(@"select count(en.id) as count 
                                                      from elio_users u
                                                      inner join Elio_user_email_notifications_settings ens
	                                                    on u.id = ens.user_id
                                                      inner join Elio_email_notifications en
	                                                    on en.id = ens.email_notifications_id
                                                      where u.email = @email
                                                      and en.id = @email_notification_id"
                                                             , DatabaseHelper.CreateStringParameter("@email", email)
                                                             , DatabaseHelper.CreateIntParameter("@email_notification_id", emailNotificationId));
                }
                else
                {
                    table = session.GetDataTable(@"select count(en.id) as count 
                                                      from elio_users u
                                                      inner join Elio_user_email_notifications_settings ens
	                                                    on u.id = ens.user_id
                                                      inner join Elio_users_sub_accounts usa
	                                                    on usa.user_id = u.id
                                                      inner join Elio_email_notifications en
	                                                    on en.id = ens.email_notifications_id
                                                      where usa.email = @email
                                                      and en.id = @email_notification_id"
                                                             , DatabaseHelper.CreateStringParameter("@email", email)
                                                             , DatabaseHelper.CreateIntParameter("@email_notification_id", emailNotificationId));
                }

                return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
            }
            else
                return true;
            
        }

        public static ElioUsersDemoRequests GetDemoRequestById(int id, DBSession session)
        {
            DataLoader<ElioUsersDemoRequests> loader = new DataLoader<ElioUsersDemoRequests>(session);
            return loader.LoadSingle(@"SELECT * FROM Elio_users_demo_requests
                                       WHERE id = @id"
                                      , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioUsersDemoRequests> GetUsersDemoRequests(DBSession session)
        {
            DataLoader<ElioUsersDemoRequests> loader = new DataLoader<ElioUsersDemoRequests>(session);
            return loader.Load(@"SELECT * FROM Elio_users_demo_requests r
                                       inner join Elio_users u
	                                        on r.request_for_user_id = u.id
                                       order by r.sysdate");
        }

        public static List<ElioUsersDemoRequestsIJUsers> GetUsersDemoRequestsIJUsers(int isApproved, DBSession session)
        {
            DataLoader<ElioUsersDemoRequestsIJUsers> loader = new DataLoader<ElioUsersDemoRequestsIJUsers>(session);
            return loader.Load(@"SELECT r.[id]
                                  ,r.[request_for_user_id]
                                  ,r.[first_name]
                                  ,r.[last_name]
                                  ,r.[company_name]
                                  ,r.[company_email]
                                  ,r.[company_size]
                                  ,r.[sysdate]
                                  ,r.[is_approved]
                                  ,r.[date_approved]
                                  ,u.company_name as demo_company_name, u.email as demo_company_email FROM Elio_users_demo_requests r
                                   inner join Elio_users u
	                                  on r.request_for_user_id = u.id
                                  WHERE 1 = 1 and r.is_approved = @is_approved
                                   order by r.is_approved, r.sysdate desc, u.id"
                                , DatabaseHelper.CreateIntParameter("@is_approved", isApproved));
        }

        public static DataTable GetUsersDemoRequestsIJUsersTbl(int isApproved, DBSession session)
        {
            return session.GetDataTable(@"SELECT r.[id]
                                  ,r.[request_for_user_id]
                                  ,r.[first_name]
                                  ,r.[last_name]
                                  ,r.[company_name]
                                  ,r.[company_email]
                                  ,r.[company_size]
                                  ,r.[sysdate]
                                  ,r.[is_approved]
                                  ,r.[date_approved]
                                  ,u.company_name as demo_company_name, u.email as demo_company_email FROM Elio_users_demo_requests r
                                   inner join Elio_users u
	                                  on r.request_for_user_id = u.id
                                  WHERE 1 = 1 and r.is_approved = @is_approved
                                   order by r.is_approved, r.sysdate desc, u.id"
                                , DatabaseHelper.CreateIntParameter("@is_approved", isApproved));
        }

        public static DataTable GetUsersDemoRequestsDt(DBSession session)
        {
            return session.GetDataTable(@"SELECT * FROM Elio_users_demo_requests r
                                           inner join Elio_users u
	                                            on r.request_for_user_id = u.id
                                           order by r.sysdate");
        }

        public static bool HasDemoRequestByEmail(string email, int requested_for_user_id, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_users_demo_requests 
                                                     WHERE company_email = @company_email
                                                     AND request_for_user_id = @request_for_user_id"
                                                     , DatabaseHelper.CreateStringParameter("@company_email", email)
                                                     , DatabaseHelper.CreateIntParameter("@request_for_user_id", requested_for_user_id));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool HasDemoRequestByStatus(int isApproved, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_users_demo_requests 
                                                     WHERE is_approved = @is_approved"
                                                     , DatabaseHelper.CreateIntParameter("@is_approved", isApproved));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool HasRFPsRequestByStatus(int isApproved, int isApiLead, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_snitcher_website_leads 
                                                     WHERE is_approved = @is_approved and is_api_lead = @is_api_lead"
                                                     , DatabaseHelper.CreateIntParameter("@is_approved", isApproved)
                                                     , DatabaseHelper.CreateIntParameter("@is_api_lead", isApiLead));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool HasRFPsMessagesByStatus(int isApproved, int isApiLead, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_snitcher_website_leads 
                                                     WHERE is_approved = @is_approved and is_api_lead = @is_api_lead"
                                                     , DatabaseHelper.CreateIntParameter("@is_approved", isApproved)
                                                     , DatabaseHelper.CreateIntParameter("@is_api_lead", isApiLead));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static ElioPackets GetPacketDescriptionBySubscriptionID(int userId, string subscriptionId, DBSession session)
        {
            DataLoader<ElioPackets> loader = new DataLoader<ElioPackets>(session);
            return loader.LoadSingle(@"SELECT * FROM Elio_users_subscriptions WITH (NOLOCK)
                                 WHERE user_id = @user_id AND subscription_id = @subscription_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateStringParameter("@subscription_id", subscriptionId));
        }

        public static bool HasActivePacketSubscription(int userId, string customerId, DBSession session)
        {
            //            DataTable table = session.GetDataTable(@"SELECT COUNT(s.id) as count
            //                                                        FROM Elio_users_subscriptions s
            //                                                        inner join Elio_packets p
            //	                                                        ON s.plan_id = p.stripe_plan_id
            //                                                        where p.id <> 6
            //                                                        and s.user_id = @user_id 
            //                                                        and s.current_period_end > GETDATE()
            //                                                        and s.status = 'active'"
            //                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));

            //            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;

            //DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);

            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                        FROM Elio_users_subscriptions s
                                                        where s.plan_id not in
                                                        (
	                                                        Select p.stripe_plan_id
	                                                        from Elio_packets p
	                                                        where p.id in (6,21,22)
                                                        )
                                                        and user_id = @user_id 
                                                        and customer_id = @customer_id
                                                        and s.current_period_end > GETDATE()
                                                        and s.plan_id <> ''
                                                        and s.status = 'active'
                                                        group by id,created_at
                                                        order by created_at desc"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateStringParameter("@customer_id", customerId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;

            //if (sub == null)
            //{
            //    //ElioUsers user = GetUserById(userId, session);
            //    //if (user != null)
            //    //{
            //    //    StripeApi.FixCustomerSubscriptionInvoices(user, session);
            //    //    HasActivePacketSubscription(userId, customerId, session);
            //    return true;
            //    //}
            //    //else
            //    //    return false;
            //}
            //else
            //    return true;
        }

        public static bool HasActiveServiceSubscription(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(s.id) as count
                                                        FROM Elio_users_subscriptions s
                                                        where s.plan_id in
                                                        (
	                                                        Select p.stripe_plan_id
	                                                        from Elio_packets p
	                                                        where p.id in (6,22,23)
                                                        )
                                                        and s.user_id = @user_id 
                                                        and s.current_period_end > GETDATE()
                                                        and s.plan_id <> ''
                                                        and s.status = 'active'"
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool ExistUserSubscription(int userId, string subscriptionId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_users_subscriptions WITH (NOLOCK)
                                                    WHERE user_id = @user_id AND subscription_id = @subscription_id"
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                     , DatabaseHelper.CreateStringParameter("@subscription_id", subscriptionId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool ExistCustomerSubscription(string customerId, string subscriptionId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_users_subscriptions WITH (NOLOCK)
                                                    WHERE customer_id = @customer_id AND subscription_id = @subscription_id"
                                                     , DatabaseHelper.CreateStringParameter("@customer_id", customerId)
                                                     , DatabaseHelper.CreateStringParameter("@subscription_id", subscriptionId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool ExistInvoice(string invoiceId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_users_subscriptions_invoices WITH (NOLOCK)
                                                    WHERE invoice_id = @invoice_id"
                                                     , DatabaseHelper.CreateStringParameter("@invoice_id", invoiceId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static ElioUsersSubscriptionsInvoices GetInvoiceByInvoiceID(string invoiceId, DBSession session)
        {
            DataLoader<ElioUsersSubscriptionsInvoices> loader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);

            return loader.LoadSingle(@"SELECT *
                                        FROM Elio_users_subscriptions_invoices WITH (NOLOCK)
                                    WHERE invoice_id = @invoice_id"
                                        , DatabaseHelper.CreateStringParameter("@invoice_id", invoiceId));
        }

		public static List<ElioUsersSubscriptions> GetCustomerExpiredSubscription(int userId, string customerId, DBSession session)
        {
            DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);

            List<ElioUsersSubscriptions> subs = loader.Load(@"SELECT *
                                                                FROM Elio_users_subscriptions s
                                                                where plan_id not in
                                                                (
	                                                                Select p.stripe_plan_id
	                                                                From Elio_packets p
	                                                                where p.id in (6)
                                                                )
                                                                and user_id = @user_id and customer_id = @customer_id 
                                                                and isnull(s.plan_id, '') <> ''
                                                                and s.status = 'active'
                                                                order by created_at desc"
                                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                            , DatabaseHelper.CreateStringParameter("@customer_id", customerId));

            return subs;
        }

        public static bool CustomerNeedsPlanSubscriptionUpdate(int userId, string customerId, DBSession session)
        {
            bool needUpdate = false;

            DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);

            List<ElioUsersSubscriptions> subs = loader.Load(@"SELECT *
                                                                FROM Elio_users_subscriptions s
                                                                where plan_id not in
                                                                (
	                                                                Select p.stripe_plan_id
	                                                                From Elio_packets p
	                                                                where p.id in (6)
                                                                )
                                                                and user_id = @user_id and customer_id = @customer_id 
                                                                and isnull(s.plan_id, '') <> '' 
                                                                order by created_at desc"
                                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                            , DatabaseHelper.CreateStringParameter("@customer_id", customerId));

            if (subs.Count == 0)
                needUpdate = true;
            else
            {
                foreach (ElioUsersSubscriptions sub in subs)
                {
                    if (sub.CurrentPeriodEnd < DateTime.Now)
                    {
                        needUpdate = true;
                        break;
                    }
                }
            }

            return needUpdate;
        }
        public static bool CustomerNeedsServiceSubscriptionUpdate(int userId, string customerId, DBSession session)
        {
            DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);

            ElioUsersSubscriptions sub = loader.LoadSingle(@"SELECT *
                                                                FROM Elio_users_subscriptions s
                                                                where plan_id in
                                                                (
	                                                                Select p.stripe_plan_id
	                                                                From Elio_packets p
	                                                                where p.id = 6
                                                                )
                                                                and user_id = @user_id and customer_id = @customer_id"
                                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                            , DatabaseHelper.CreateStringParameter("@customer_id", customerId));

            if (sub == null)
                return false;
            else
                if (sub.CurrentPeriodEnd < DateTime.Now)
                return true;
            else
                return false;
        }

        public static bool HasUserActiveSubscription(int userId, string customerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                      FROM Elio_users_subscriptions
                                                      where user_id = @user_id and customer_id = @customer_id and status = 'active'"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateStringParameter("@customer_id", customerId));

            return (Convert.ToInt32(table.Rows[0]["count"]) == 1) ? true : false;
        }

        public static ElioUsersSubscriptions GetUserSubscription(int userId, string customerId, DBSession session)
        {
            DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
            return loader.LoadSingle(@"SELECT top 1 * FROM Elio_users_subscriptions WITH (NOLOCK)
                                        where user_id = @user_id and customer_id = @customer_id and plan_id not in ('plan_EB9wRnxfsR9ugT', 'plan_GlVkKQLyTruFj8')
                                        order by created_at desc"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateStringParameter("@customer_id", customerId));
        }

        public static ElioUsersSubscriptions GetSubscriptionBySubID(string subscriptionId, DBSession session)
        {
            DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
            return loader.LoadSingle(@"SELECT * FROM Elio_users_subscriptions WITH (NOLOCK)
                                 WHERE subscription_id = @subscription_id"
                                , DatabaseHelper.CreateStringParameter("@subscription_id", subscriptionId));
        }

        public static ElioUsersSubscriptions GetUserSubscriptionBySubID(int userId, string subscriptionId, DBSession session)
        {
            DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
            return loader.LoadSingle(@"SELECT * FROM Elio_users_subscriptions WITH (NOLOCK)
                                 WHERE user_id = @user_id AND subscription_id = @subscription_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateStringParameter("@subscription_id", subscriptionId));
        }

        public static List<ElioUsersSubscriptionsInvoices> GetUserSubscriptionInvoices(int userId, DBSession session)
        {
            DataLoader<ElioUsersSubscriptionsInvoices> loader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
            return loader.Load(@"SELECT * FROM Elio_users_subscriptions_invoices WITH (NOLOCK)
                                 WHERE user_id = @user_id 
                                 ORDER BY date desc"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetUserSubscriptionInvoicesDataTable(int userId, DBSession session)
        {
            return session.GetDataTable(@"SELECT usi.*, us.status, us.plan_id, us.plan_nickname, us.coupon_id FROM Elio_users_subscriptions_invoices usi
                                            LEFT JOIN Elio_users_subscriptions us
	                                            ON usi.user_subscription_id = us.id AND usi.subscription_id = us.subscription_id
                                            WHERE usi.user_id = @user_id 
                                            ORDER BY date desc"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        //        public static bool IsLastInvoice(int userSubscriptionId, string invoiceId, DBSession session)
        //        {
        //            DataTable table = session.GetDataTable(@"select top 1 invoice_id
        //                                            from Elio_users_subscriptions_invoices usi
        //                                            where user_subscription_id = @user_subscription_id
        //                                            order by date desc"
        //                                        , DatabaseHelper.CreateIntParameter("@user_subscription_id", userSubscriptionId));

        //            if (table.Rows.Count > 0)
        //            {
        //                return (table.Rows[0]["invoice_id"].ToString() == invoiceId) ? true : false;
        //            }
        //            else
        //                return false;
        //        }

        public static bool IsPlanSubscriptionLastInvoice(int userSubscriptionId, string invoiceId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"
                                                    with s
                                                    as
                                                    (
	                                                    select s.id
	                                                    from Elio_users_subscriptions s
	                                                    where s.plan_id not in
	                                                    (
		                                                    select p.stripe_plan_id
		                                                    from Elio_packets p 
		                                                    where p.id = 6  --Service Plan
	                                                    )
                                                    )

                                                    select top 1 invoice_id, s.id
                                                    from Elio_users_subscriptions_invoices usi
                                                    inner join s
	                                                    on s.id = usi.user_subscription_id
                                                    where s.id = @id
                                                    order by date desc"
                                        , DatabaseHelper.CreateIntParameter("@id", userSubscriptionId));

            if (table.Rows.Count > 0)
            {
                return (table.Rows[0]["invoice_id"].ToString() == invoiceId) ? true : false;
            }
            else
                return false;
        }

        public static bool IsPlanSubscriptionLastInvoice(string subscriptionId, string invoiceId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"
                                                    with s
                                                    as
                                                    (
	                                                    select s.id, s.subscription_id
	                                                    from Elio_users_subscriptions s
	                                                    where s.plan_id not in
	                                                    (
		                                                    select p.stripe_plan_id
		                                                    from Elio_packets p 
		                                                    where p.id = 6  --Service Plan
	                                                    )
                                                    )

                                                    select top 1 invoice_id, s.subscription_id
                                                    from Elio_users_subscriptions_invoices usi
                                                    inner join s
	                                                    on s.id = usi.user_subscription_id
                                                    where s.subscription_id = @subscription_id
                                                    order by date desc"
                                        , DatabaseHelper.CreateStringParameter("@subscription_id", subscriptionId));

            if (table.Rows.Count > 0)
            {
                return (table.Rows[0]["invoice_id"].ToString() == invoiceId) ? true : false;
            }
            else
                return false;
        }

        public static bool CanCancelSubscriptionByCouponRedeemByDate(string couponId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"
                                                    SELECT redeem_by
                                                    FROM Elio_packets_stripe_coupons
                                                    where coupon_id = @coupon_id"
                                                    , DatabaseHelper.CreateStringParameter("@coupon_id", couponId));

            if (table.Rows.Count > 0)
            {
                return Convert.ToDateTime(table.Rows[0]["redeem_by"]) < DateTime.Now ? true : false;
            }
            else
                return true;
        }

        public static bool IsServiceSubscriptionLastInvoice(string subscriptionId, string invoiceId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"
                                                    with s
                                                    as
                                                    (
	                                                    select s.id, s.subscription_id
	                                                    from Elio_users_subscriptions s
	                                                    where s.plan_id in
	                                                    (
		                                                    select p.stripe_plan_id
		                                                    from Elio_packets p 
		                                                    where p.id = 6  --Service Plan
	                                                    )
                                                    )

                                                    select top 1 invoice_id, s.subscription_id
                                                    from Elio_users_subscriptions_invoices usi
                                                    inner join s
	                                                    on s.id = usi.user_subscription_id
                                                    where s.subscription_id = @subscription_id
                                                    order by date desc"
                                        , DatabaseHelper.CreateStringParameter("@subscription_id", subscriptionId));

            if (table.Rows.Count > 0)
            {
                return (table.Rows[0]["invoice_id"].ToString() == invoiceId) ? true : false;
            }
            else
                return false;
        }

        public static string GetPlanDescriptionBySubscriptionID(string subscriptionId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT pack_description
                                                        FROM Elio_packets p
                                                        INNER JOIN Elio_users_subscriptions us
	                                                        ON p.stripe_plan_id = us.plan_id
                                                        WHERE us.subscription_id = @subscription_id"
                                                    , DatabaseHelper.CreateStringParameter("@subscription_id", subscriptionId));

            return (table.Rows.Count > 0) ? table.Rows[0]["pack_description"].ToString() : "";
        }

        public static int GetPacketIdBySubscriptionID(string subscriptionId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT top 1 p.id
                                                        FROM Elio_packets p
                                                        INNER JOIN Elio_users_subscriptions us
	                                                        ON p.stripe_plan_id = us.plan_id
                                                        WHERE us.subscription_id = @subscription_id
                                                        order by p.id"
                                                    , DatabaseHelper.CreateStringParameter("@subscription_id", subscriptionId));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["id"]) : 0;
        }

        public static int GetPacketIdBySubscriptionPlanNickName(string planNickName, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT p.id
                                                        FROM Elio_packets p
                                                        where p.pack_description = @pack_description"
                                                    , DatabaseHelper.CreateStringParameter("@pack_description", planNickName));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["id"]) : 0;
        }

        public static bool HasVendorNewOnboardingFilesByResellerId(int partnerId, out int count, DBSession session)
        {
            count = 0;

            string strQuery = @"SELECT count(id) as count 
                                FROM Elio_onboarding_users_library_files
                                where user_id in
                                (
	                                select master_user_id 
	                                from Elio_collaboration_vendors_resellers cvr
	                                where invitation_status = 'Confirmed'
	                                and partner_user_id = @partner_user_id
	                                and is_active = 1
                                )
                                and is_new = 1
                                and is_public = 1";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerId));

            if (Convert.ToInt32(table.Rows[0]["count"]) > 0)
            {
                count = Convert.ToInt32(table.Rows[0]["count"]);
                return true;
            }

            return false;
        }

        public static bool HasNewLeadDistribution(ElioUsers user, int status, string result, int isNew, out int count, DBSession session)
        {
            count = 0;

            string strQuery = @"
                                SELECT COUNT(*) as COUNT
                                FROM Elio_lead_distributions
                                where 1 = 1
                                and status = @status
                                and is_public = 1
                                and lead_result = @lead_result
                                and is_new = @is_new";

            strQuery += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @user_id" : " AND reseller_id = @user_id";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateStringParameter("lead_result", result)
                                                    , DatabaseHelper.CreateIntParameter("@is_new", isNew));

            if (Convert.ToInt32(table.Rows[0]["COUNT"]) > 0)
            {
                count = Convert.ToInt32(table.Rows[0]["COUNT"]);
                return true;
            }

            return false;
        }

        public static bool HasNewLeadDistributionByStatus(ElioUsers user, int status, int isVewed, out int count, DBSession session)
        {
            count = 0;

            string strQuery = @"
                                SELECT COUNT(*) as COUNT
                                FROM Elio_lead_distributions
                                where 1 = 1
                                and status = @status
                                and is_vewed_by_vendor = @is_vewed_by_vendor
                                and is_public = 1";

            strQuery += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @user_id" : " AND reseller_id = @user_id";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateIntParameter("@is_vewed_by_vendor", isVewed));

            if (Convert.ToInt32(table.Rows[0]["COUNT"]) > 0)
            {
                count = Convert.ToInt32(table.Rows[0]["COUNT"]);
                return true;
            }

            return false;
        }

        public static bool HasNewDealRegistration(ElioUsers user, int isActive, int status, string result, int isNew, out int count, DBSession session)
        {
            count = 0;

            string strQuery = @"
                                SELECT COUNT(*) as COUNT
                                FROM Elio_registration_deals rd                                
                                WHERE 1 = 1
                                AND is_public = 1
                                AND is_active = @is_active 
                                AND status = @status 
                                AND deal_result = @deal_result
                                AND is_new = @is_new";

            strQuery += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @user_id" : " AND reseller_id = @user_id";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateStringParameter("deal_result", result)
                                                    , DatabaseHelper.CreateIntParameter("@is_new", isNew));

            if (Convert.ToInt32(table.Rows[0]["COUNT"]) > 0)
            {
                count = Convert.ToInt32(table.Rows[0]["COUNT"]);
                return true;
            }

            return false;
        }

        public static bool HasNewDealRegistrationRandStad(ElioUsers user, int isActive, int status, string result, int isNew, out int count, DBSession session)
        {
            count = 0;

            string strQuery = @"
                                SELECT COUNT(*) as COUNT
                                FROM Elio_registration_deals rd                                
                                WHERE 1 = 1
                                AND is_public = 1
                                AND is_active = @is_active 
                                AND status = @status 
                                AND deal_result = @deal_result
                                AND is_new = @is_new";

            if (isActive == (int)DealActivityStatus.NotConfirmed)
                strQuery += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @user_id AND created_by_user_id != vendor_id" : " AND reseller_id = @user_id AND created_by_user_id != reseller_id";
            else if (isActive == (int)DealActivityStatus.Approved)
                strQuery += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @user_id AND created_by_user_id = vendor_id" : " AND reseller_id = @user_id AND created_by_user_id = reseller_id";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                    , DatabaseHelper.CreateStringParameter("deal_result", result)
                                                    , DatabaseHelper.CreateIntParameter("@is_new", isNew));

            if (Convert.ToInt32(table.Rows[0]["COUNT"]) > 0)
            {
                count = Convert.ToInt32(table.Rows[0]["COUNT"]);
                return true;
            }

            return false;
        }

        public static bool HasNewRegistrationDeal(ElioUsers user, int isNew, out int count, DBSession session)
        {
            count = 0;

            string strQuery = @"
                                SELECT COUNT(*) as COUNT
                                FROM Elio_registration_deals rd                                
                                WHERE 1 = 1
                                AND is_public = 1
                                AND is_new = @is_new";

            strQuery += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @user_id" : " AND reseller_id = @user_id";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                                    , DatabaseHelper.CreateIntParameter("@is_new", isNew));

            if (Convert.ToInt32(table.Rows[0]["COUNT"]) > 0)
            {
                count = Convert.ToInt32(table.Rows[0]["COUNT"]);
                return true;
            }

            return false;
        }

        public static bool HasNewP2PDealRegistration(int partnerUserId, int isActive, int status, out int count, DBSession session)
        {
            count = 0;

            string strQuery = @"
                                SELECT COUNT(*) as COUNT
                                FROM Elio_partner_to_partner_deals 
                                WHERE 1 = 1
                                AND is_public = 1
                                AND is_active = @is_active 
                                AND status = @status 
                                AND partner_user_id = @partner_user_id";

            DataTable table = session.GetDataTable(strQuery
                                                    , DatabaseHelper.CreateIntParameter("@partner_user_id", partnerUserId)
                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                    , DatabaseHelper.CreateIntParameter("@status", status));

            if (Convert.ToInt32(table.Rows[0]["COUNT"]) > 0)
            {
                count = Convert.ToInt32(table.Rows[0]["COUNT"]);
                return true;
            }

            return false;
        }

        public static bool ExistDealForVendor(int vendorId, int resellerId, string domain, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_registration_deals 
                                                     WHERE vendor_id = @vendor_id
                                                     AND domain = @domain
                                                     AND is_public = 1
                                                     AND reseller_id <> @reseller_id"
                                                     , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                     , DatabaseHelper.CreateStringParameter("@domain", domain)
                                                     , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool ExistDealToVendorCRM(int vendorId, int resellerId, string email, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_crm_deal_contacts_365 
                                                     WHERE vendor_id = @vendor_id
                                                     AND deal_email = @deal_email
                                                     AND is_public = 1
                                                     AND reseller_id <> @reseller_id"
                                                     , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                     , DatabaseHelper.CreateStringParameter("@deal_email", email)
                                                     , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool ExistDealByReseller(int vendorId, int resellerId, string domain, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_registration_deals 
                                                     WHERE vendor_id = @vendor_id
                                                     AND domain = @domain
                                                     AND is_public = 1
                                                     AND reseller_id = @reseller_id"
                                                     , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                     , DatabaseHelper.CreateStringParameter("@domain", domain)
                                                     , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static bool ExistDomainToOtherDealByReseller(int dealId, int resellerId, string domain, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as count
                                                     FROM Elio_registration_deals 
                                                     WHERE id != @id
                                                     AND domain = @domain
                                                     AND is_public = 1
                                                     AND reseller_id = @reseller_id"
                                                     , DatabaseHelper.CreateIntParameter("@id", dealId)
                                                     , DatabaseHelper.CreateStringParameter("@domain", domain)
                                                     , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static ElioRegistrationDeals GetDealById(int id, DBSession session)
        {
            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_registration_deals 
                                       WHERE id = @id"
                                    , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioRegistrationDeals> GetDeals(int collaborationId, int vendorId, int resellerId, DBSession session)
        {
            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

            return loader.Load(@"SELECT * FROM Elio_registration_deals 
                                       WHERE collaboration_vendor_reseller_id = @collaboration_vendor_reseller_id
                                       AND vendor_id = @vendor_id
                                       AND reseller_id = @reseller_id
                                       ORDER BY created_date DESC"
                                    , DatabaseHelper.CreateIntParameter("@collaboration_vendor_reseller_id", collaborationId)
                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                    , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));
        }

        public static void SetUserDealsAsExpiredAndLost(ElioUsers user, DBSession session)
        {
            //            string query = @"update Elio_registration_deals 
            //                                SET status = -3
            //                             WHERE 1 = 1
            //                             AND status = 1                             
            //                             AND expected_closed_date < GETDATE()";

            //            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            //            session.ExecuteQuery(query
            //                                , DatabaseHelper.CreateIntParameter("@userId", user.Id));

            //            string query2 = @"update Elio_registration_deals 
            //                                SET deal_result = 'Lost'
            //                             WHERE 1 = 1
            //                             AND status = -3";

            //            query2 += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            //            session.ExecuteQuery(query2
            //                                , DatabaseHelper.CreateIntParameter("@userId", user.Id));

            string query3 = @"update Elio_registration_deals 
                                SET status = -3,
                                    is_new = 0,
                                    date_viewed = expected_closed_date
                             WHERE 1 = 1
                             AND DATEADD(MONTH, month_duration, created_date) < GETDATE()
                             AND status = 1
                             AND is_active = 1
                             AND deal_result = 'Pending'
                             AND month_duration <> 0";

            query3 += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            session.ExecuteQuery(query3
                                , DatabaseHelper.CreateIntParameter("@userId", user.Id));
        }

        public static List<ElioRegistrationDealsIJUsers> GetUserDealsIJUsersByStatusAndActivityList(ElioUsers user, int loggedInRoleId, string subAccountEmail, bool isAdminRole, string statusList, string activeList, string dealResult, int collaborationId, string companyNameEmail, DBSession session)
        {
            DataLoader<ElioRegistrationDealsIJUsers> loader = new DataLoader<ElioRegistrationDealsIJUsers>(session);

            string query = @"SELECT rd.*, u.company_name as partner_name, u.country as partner_location FROM Elio_registration_deals rd ";

            query += @" INNER JOIN elio_users u 
                                ON u.id = rd.reseller_id ";

            if (user.CompanyType == Types.Vendors.ToString() && loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";

            query += @" WHERE 1 = 1 
                    AND rd.is_active IN (" + activeList + ") ";

            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            if (user.CompanyType == Types.Vendors.ToString() && loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            if (collaborationId != -1)
                query += " AND collaboration_vendor_reseller_id = @collaboration_vendor_reseller_id ";

            if (dealResult != "" && dealResult != ((int)DealActivityStatus.Rejected).ToString())
                query += "AND status IN (" + statusList + ") AND deal_result = '" + dealResult + "' ";
            else
                query += " AND status IN (" + statusList + ") ";

            if (!string.IsNullOrEmpty(companyNameEmail))
            {
                if (user.CompanyType == Types.Vendors.ToString())
                    query += " AND (u.company_name like '" + companyNameEmail + "%' OR rd.email like '" + companyNameEmail + "%') ";
                else
                    query += " AND (rd.company_name like '" + companyNameEmail + "%' OR rd.email like '" + companyNameEmail + "%') ";
            }

            query += " ORDER BY rd.created_date DESC, rd.deal_result DESC";

            if (collaborationId != -1)
            {
                return loader.Load(query
                                    , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                    , DatabaseHelper.CreateIntParameter("@collaboration_vendor_reseller_id", collaborationId));
            }
            else
            {
                return loader.Load(query
                                        , DatabaseHelper.CreateIntParameter("@userId", user.Id));
            }
        }

        public static List<ElioRegistrationDealsIJUsers> GetUserDealsIJUsersByStatusAndActivity(ElioUsers user, int loggedInRoletId, string subAccountEmail, bool isAdminRole, int status, int isActive, string dealResult, int collaborationId, string companyNameEmail, DBSession session)
        {
            DataLoader<ElioRegistrationDealsIJUsers> loader = new DataLoader<ElioRegistrationDealsIJUsers>(session);

            string query = @"SELECT rd.*, u.company_name as partner_name, u.country as partner_location 
                             FROM Elio_registration_deals rd 
                             INNER JOIN Elio_users u ON rd.reseller_id = u.id ";

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = rd.reseller_id
		                            and cvmr.vendor_reseller_id = rd.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = rd.vendor_id ";
            }

            query += @" WHERE 1 = 1
                        AND rd.status = @status 
                        AND rd.is_active = @is_active ";

            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            if (loggedInRoletId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoletId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            if (collaborationId != -1)
                query += " AND collaboration_vendor_reseller_id = @collaboration_vendor_reseller_id ";

            if (dealResult != "")
                query += " AND deal_result = '" + dealResult + "' ";

            if (!string.IsNullOrEmpty(companyNameEmail))
            {
                query += " AND (rd.company_name like '" + companyNameEmail + "%' OR rd.email like '" + companyNameEmail + "%') ";
            }

            query += " ORDER BY rd.created_date DESC, rd.deal_result DESC";

            if (collaborationId != -1)
            {
                return loader.Load(query
                                    , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                    , DatabaseHelper.CreateIntParameter("@collaboration_vendor_reseller_id", collaborationId));
            }
            else
            {
                return loader.Load(query
                                        , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                        , DatabaseHelper.CreateIntParameter("@status", status)
                                        , DatabaseHelper.CreateIntParameter("@is_active", isActive));
            }
        }

        public static List<ElioRegistrationDeals> GetUserDealsByStatusAndActivity(ElioUsers user, int status, int isActive, string dealResult, int collaborationId, string companyNameEmail, DBSession session)
        {
            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

            string query = @"SELECT * FROM Elio_registration_deals                             
                                WHERE 1 = 1
                                AND status = @status 
                                AND is_active = @is_active ";

            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            if (collaborationId != -1)
                query += " AND collaboration_vendor_reseller_id = @collaboration_vendor_reseller_id ";

            if (dealResult != "")
                query += " AND deal_result = '" + dealResult + "' ";

            if (!string.IsNullOrEmpty(companyNameEmail))
            {
                query += " AND (company_name like '" + companyNameEmail + "%' OR email like '" + companyNameEmail + "%') ";
            }

            query += " ORDER BY created_date DESC, deal_result DESC";

            if (collaborationId != -1)
            {
                return loader.Load(query
                                    , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                    , DatabaseHelper.CreateIntParameter("@collaboration_vendor_reseller_id", collaborationId));
            }
            else
            {
                return loader.Load(query
                                        , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                        , DatabaseHelper.CreateIntParameter("@status", status)
                                        , DatabaseHelper.CreateIntParameter("@is_active", isActive));
            }
        }

        public static List<ElioRegistrationDeals> GetUserDealsByActiveStatus(ElioUsers user, int isActive, int collaborationId, DBSession session)
        {
            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

            string query = @"SELECT * FROM Elio_registration_deals 
                                WHERE 1 = 1
                                AND is_active = @is_active ";

            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            if (collaborationId != -1)
                query += " AND collaboration_vendor_reseller_id = @collaboration_vendor_reseller_id ";

            query += " ORDER BY created_date DESC, deal_result DESC";
            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                , DatabaseHelper.CreateIntParameter("@collaboration_vendor_reseller_id", collaborationId)
                                , DatabaseHelper.CreateIntParameter("@is_active", isActive));
        }

        public static List<ElioRegistrationDealsIJUsers> GetUserDealsIJUsersByActiveStatus(ElioUsers user, int isActive, int collaborationId, string companyNameEmail, DBSession session)
        {
            DataLoader<ElioRegistrationDealsIJUsers> loader = new DataLoader<ElioRegistrationDealsIJUsers>(session);

            string query = @"SELECT rd.*, u.company_name as partner_name, u.country as partner_location FROM Elio_registration_deals rd 
                             INNER JOIN Elio_users u ON rd.reseller_id = u.id
                             WHERE 1 = 1
                             AND is_active = @is_active ";

            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            if (collaborationId != -1)
                query += " AND collaboration_vendor_reseller_id = @collaboration_vendor_reseller_id ";

            if (!string.IsNullOrEmpty(companyNameEmail))
            {
                if (user.CompanyType == Types.Vendors.ToString())
                    query += " AND (u.company_name like '" + companyNameEmail + "%' OR rd.email like '" + companyNameEmail + "%') ";
                else
                    query += " AND (rd.company_name like '" + companyNameEmail + "%' OR rd.email like '" + companyNameEmail + "%') ";
            }

            query += " ORDER BY rd.created_date DESC, rd.deal_result DESC";
            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                , DatabaseHelper.CreateIntParameter("@collaboration_vendor_reseller_id", collaborationId)
                                , DatabaseHelper.CreateIntParameter("@is_active", isActive));
        }

        public static bool ExistUserLeadByContactEmail(int userId, string email, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS count FROM Elio_lead_distributions 
                                                     WHERE vendor_id = @vendor_id 
                                                     AND email = @email"
                                                     , DatabaseHelper.CreateIntParameter("@vendor_id", userId)
                                                     , DatabaseHelper.CreateStringParameter("@email", email));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool ExistVendorLeadToResellerByEmail(int vendorId, int resellerId, string email, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS count FROM Elio_lead_distributions 
                                                     WHERE vendor_id = @vendor_id 
                                                     AND reseller_id = @reseller_id
                                                     AND email = @email"
                                                     , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                     , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId)
                                                     , DatabaseHelper.CreateStringParameter("@email", email));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static decimal GetResellerDealsAndLeadsWonSumAmount(int vendorId, int resellerId, DBSession session)
        {
            DataTable tblDealsAmount = session.GetDataTable(@"SELECT sum(amount) as deals_sum_amount
                                                              FROM Elio_registration_deals
                                                              where reseller_id = @reseller_id
                                                              and status = 2
                                                              and deal_result = 'Won'
                                                              and is_active = 1
                                                              and vendor_id = @vendor_id"
                                                             , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                             , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            decimal dealsSumAmount = tblDealsAmount.Rows.Count > 0 && tblDealsAmount.Rows[0]["deals_sum_amount"].ToString() != "" ? Convert.ToDecimal(tblDealsAmount.Rows[0]["deals_sum_amount"]) : 0;

            DataTable tblLeadsAmount = session.GetDataTable(@"SELECT sum(amount) as leads_sum_amount
                                                            FROM [Elioplus_DB].[dbo].[Elio_lead_distributions]
                                                            where reseller_id = @reseller_id
                                                            and status = 2
                                                            and lead_result = 'Won'
                                                            and is_public = 1
                                                            and vendor_id = @vendor_id"
                                                            , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                            , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            decimal leadsSumAmount = tblLeadsAmount.Rows.Count > 0 && tblLeadsAmount.Rows[0]["leads_sum_amount"].ToString() != "" ? Convert.ToDecimal(tblLeadsAmount.Rows[0]["leads_sum_amount"]) : 0;

            return dealsSumAmount + leadsSumAmount;
        }

        public static decimal GetResellerTotalDealsAndLeadsWonSumAmount(int vendorId, int resellerId, DBSession session)
        {
            DataTable tblTotalAmount = session.GetDataTable(@"; with deals
                                                                as
                                                                (
                                                                SELECT isnull(sum(amount), 0) as deals_sum_amount
                                                                FROM Elio_registration_deals
                                                                where reseller_id = @reseller_id
                                                                and status = 2
                                                                and deal_result = 'Won'
                                                                and is_active = 1
                                                                and vendor_id = @vendor_id
                                                                )
                                                                ,leads
                                                                as
                                                                (
                                                                SELECT isnull(sum(amount), 0) as leads_sum_amount
                                                                FROM [Elioplus_DB].[dbo].[Elio_lead_distributions]
                                                                where reseller_id = @reseller_id
                                                                and status = 2
                                                                and lead_result = 'Won'
                                                                and is_public = 1
                                                                and vendor_id = @vendor_id
                                                                )

                                                                select isnull(sum(deals_sum_amount + leads_sum_amount), 0) as total_sum from deals,leads"
                                                             , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                                             , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId));

            return tblTotalAmount.Rows.Count > 0 && tblTotalAmount.Rows[0]["total_sum"].ToString() != "" ? Convert.ToDecimal(tblTotalAmount.Rows[0]["total_sum"]) : 0;
        }

        public static List<ElioRegistrationDeals> GetUserDealsByResultStatus(ElioUsers user, string resultStatus, DBSession session)
        {
            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

            string query = @"SELECT * FROM Elio_registration_deals 
                                WHERE 1 = 1
                                AND deal_result = @deal_result ";

            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            query += " ORDER BY created_date DESC, deal_result DESC";

            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                , DatabaseHelper.CreateStringParameter("@deal_result", resultStatus));
        }

        public static List<ElioRegistrationDeals> GetDealsBy(int collaborationId, int vendorId, int resellerId, string result, int status, int? year, DBSession session)
        {
            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

            string query = @"SELECT * FROM Elio_registration_deals 
                                WHERE is_public = 1
                                and collaboration_vendor_reseller_id = @collaboration_vendor_reseller_id
                                AND vendor_id = @vendor_id
                                AND reseller_id = @reseller_id
                                AND deal_result = @deal_result
                                AND status = @status ";

            if (year != null)
                query += "and (year(created_date) >= " + year + " and year(created_date) <= " + year + ") ";

            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@collaboration_vendor_reseller_id", collaborationId)
                                , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                                , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId)
                                , DatabaseHelper.CreateStringParameter("@deal_result", result)
                                , DatabaseHelper.CreateIntParameter("@status", status));
        }

        public static List<ElioLeadDistributions> GetLeadsBy(int collaborationId, int vendorId, int resellerId, string result, int status, int? year, DBSession session)
        {
            DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);

            string query = @"SELECT * FROM Elio_lead_distributions
                            where (is_public = 1 or (is_public = 0 and is_historical_reason = 1))
                            and collaboration_vendor_reseller_id = @collaboration_vendor_reseller_id
                            and vendor_id = @vendor_id
                            and reseller_id = @reseller_id
                            and lead_result = @lead_result
                            and status = @status ";

            if (year != null)
                query += "and (year(created_date) >= " + year + " and year(created_date) <= " + year + ") ";

            return loader.Load(query
                , DatabaseHelper.CreateIntParameter("@collaboration_vendor_reseller_id", collaborationId)
                , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId)
                , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId)
                , DatabaseHelper.CreateStringParameter("@lead_result", result)
                , DatabaseHelper.CreateIntParameter("@status", status));
        }

        public static List<ElioLeadDistributions> GetUserLeadsByStatusAndResult(ElioUsers user,int loggedInRoleId, string subAccountEmail, bool isAdminRole, int collaborationId, int status, string result, string companyNameEmail, DBSession session)
        {
            DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);

            string query = @"SELECT * FROM Elio_lead_distributions ld ";

            if (!string.IsNullOrEmpty(companyNameEmail) && user.CompanyType == Types.Vendors.ToString())
            {
                query += @" INNER JOIN elio_users u 
                                ON u.id = ld.reseller_id ";
            }

            if (user.CompanyType == Types.Vendors.ToString() && loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
            {
                query += @" inner join Elio_collaboration_vendors_members_resellers cvmr 
					            on cvmr.partner_user_id = ld.reseller_id
		                            and cvmr.vendor_reseller_id = ld.collaboration_vendor_reseller_id
				            inner join Elio_users_sub_accounts usa 
					            on usa.id = cvmr.sub_account_id 
		                            and usa.user_id = ld.vendor_id ";
            }

            query += @" WHERE 1 = 1
                        AND ld.is_public = 1
                        AND ld.status = @status ";

            if (result != "")
                query += " AND lead_result = @lead_result";

            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            if (user.CompanyType == Types.Vendors.ToString() && loggedInRoleId > 0 && !isAdminRole && subAccountEmail != "")
                query += @" and usa.is_confirmed = 1  
                            and usa.team_role_id = " + loggedInRoleId + " and usa.email = '" + subAccountEmail + "' " +
                            "and usa.is_active = 1 ";

            if (collaborationId != -1)
                query += " AND collaboration_vendor_reseller_id = @collaboration_vendor_reseller_id ";

            if (!string.IsNullOrEmpty(companyNameEmail))
            {
                if (user.CompanyType == Types.Vendors.ToString())
                    query += " AND (u.company_name like '" + companyNameEmail + "%' OR ld.email like '" + companyNameEmail + "%') ";
                else
                    query += " AND (ld.company_name like '" + companyNameEmail + "%' OR ld.email like '" + companyNameEmail + "%') ";
            }

            query += " ORDER BY created_date DESC, lead_result DESC";
            return loader.Load(query
                                , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                , DatabaseHelper.CreateIntParameter("@collaboration_vendor_reseller_id", collaborationId)
                                , DatabaseHelper.CreateIntParameter("@status", status)
                                , DatabaseHelper.CreateStringParameter("@lead_result", result));
        }

        public static ElioLeadDistributions GetLeadDistributionById(int id, DBSession session)
        {
            DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_lead_distributions 
                                       WHERE id = @id"
                                    , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioRegistrationDeals> GetUserDealsByStatus11(ElioUsers user, int status, int collaborationId, DBSession session)
        {
            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

            string query = @"SELECT * FROM Elio_registration_deals 
                            
                                WHERE 1 = 1
                                AND status = @status ";

            query += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @userId " : " AND reseller_id = @userId ";

            if (collaborationId != -1)
                query += " AND collaboration_vendor_reseller_id = @collaboration_vendor_reseller_id ";

            query += " ORDER BY created_date DESC, deal_result DESC";

            if (collaborationId != -1)
            {
                return loader.Load(query
                                    , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                    , DatabaseHelper.CreateIntParameter("@collaboration_vendor_reseller_id", collaborationId));
            }
            else
            {
                return loader.Load(query
                                        , DatabaseHelper.CreateIntParameter("@userId", user.Id)
                                        , DatabaseHelper.CreateIntParameter("@status", status));
            }
        }

        public static ElioRegistrationDealsVendorSettings GetVendorDealMonthSettings(int vendorId, DBSession session)
        {
            DataLoader<ElioRegistrationDealsVendorSettings> loader = new DataLoader<ElioRegistrationDealsVendorSettings>(session);
            return loader.LoadSingle(@"SELECT * FROM Elio_registration_deals_vendor_settings WHERE vendor_id = @vendor_id"
                                , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));
        }

        public static void InsertUserDealMonthDuration(int vendorId, int duration, DBSession session)
        {
            DataLoader<ElioRegistrationDealsVendorSettings> loader = new DataLoader<ElioRegistrationDealsVendorSettings>(session);

            ElioRegistrationDealsVendorSettings setting = new ElioRegistrationDealsVendorSettings();

            setting.VendorId = vendorId;
            setting.DealDurationSetting = duration;
            setting.Sysdate = DateTime.Now;
            setting.LastUpdate = DateTime.Now;

            loader.Insert(setting);
        }

        public static bool HasDealMonthDurationSettings(int vendorId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select COUNT(id) AS count
                                                    FROM Elio_registration_deals_vendor_settings 
                                                    WHERE vendor_id = @vendor_id"
                                                , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static void UpdateUserDealMonthDurationSettings(int vendorId, int duration, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_registration_deals_vendor_settings 
                                        SET deal_duration_setting = @deal_duration_setting,
                                            last_update = GETDATE()
                                   WHERE vendor_id = @vendor_id"
                                , DatabaseHelper.CreateIntParameter("@deal_duration_setting", duration)
                                , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));
        }

        public static void DeleteUserDealMonthDuration(int vendorId, DBSession session)
        {
            session.ExecuteQuery(@"DELETE FROM Elio_registration_deals_vendor_settings WHERE vendor_id = @vendor_id"
                                    , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));
        }

        public static bool SendNotificationEmailToUser(string userEmail, int notificationEmailId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(*) as count
                                                    FROM Elio_user_email_notifications_settings uens
                                                    inner join elio_users u
	                                                    on u.id = uens.user_id
                                                    inner join Elio_email_notifications en
	                                                    on en.id = uens.email_notifications_id	
                                                    where u.email = @email
                                                    and uens.email_notifications_id = @email_notifications_id
                                                    and en.is_public = 1"
                                                    , DatabaseHelper.CreateStringParameter("@email", userEmail)
                                                    , DatabaseHelper.CreateIntParameter("@email_notifications_id", notificationEmailId));

            if (Convert.ToInt32(table.Rows[0]["count"]) == 0)
            {
                table = session.GetDataTable(@"SELECT count(*) as count
                                                    FROM Elio_email_notifications en	
                                                    where 1 = 1
                                                    and en.id = @id
                                                    and en.is_public = 0"
                                                    , DatabaseHelper.CreateIntParameter("@id", notificationEmailId));
            }

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistPage(string page, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT Count(id) as count FROM Elio_pages where page_link = @page_link"
                                                   , DatabaseHelper.CreateStringParameter("@page_link", page));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static List<ElioPages> GetAllPages(DBSession session)
        {
            DataLoader<ElioPages> loader = new DataLoader<ElioPages>(session);
            return loader.Load(@"SELECT * FROM Elio_pages WHERE is_public = 1");
        }

        public static List<ElioSearchCategories> GetAllCategoriesForSearch(DBSession session)
        {
            DataLoader<ElioSearchCategories> loader = new DataLoader<ElioSearchCategories>(session);
            return loader.Load(@"SELECT * FROM Elio_search_categories WHERE is_public = 1");
        }

        public static ElioSearchCategories GetCategoryForSearchById(int id, DBSession session)
        {
            DataLoader<ElioSearchCategories> loader = new DataLoader<ElioSearchCategories>(session);
            return loader.LoadSingle(@"SELECT * FROM Elio_search_categories WHERE 1 = 1 AND id = @id"
                                     , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioSearchCategories GetCategoryForSearchByDescription(string categoryDescription, DBSession session)
        {
            DataLoader<ElioSearchCategories> loader = new DataLoader<ElioSearchCategories>(session);
            return loader.LoadSingle(@"SELECT * FROM Elio_search_categories WHERE 1 = 1 AND search_category = @search_category"
                                     , DatabaseHelper.CreateStringParameter("@search_category", categoryDescription));
        }

        public static void SaveInDB(int userId, string page, string errorMessage, string stackTrace, DBSession session)
        {
            DataLoader<ElioUsersFavorites> loader = new DataLoader<ElioUsersFavorites>(session);
            loader.Load("Select * from elio_users_favorites where user_id=@user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void DeleteUserOnboardingLibraryVideoFileById(int id, DBSession session)
        {
            session.ExecuteQuery(@"DELETE  
                                   FROM Elio_onboarding_users_video_library_files 
                                   WHERE 1 = 1 
                                   AND id = @id"
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static bool DeleteOrUpdateUserOnboardingLibraryFileAndBlobById(ElioUsers user, int fileId, bool isPreviewFile, DBSession session)
        {
            bool resetStorage = false;
            if (!isPreviewFile)
            {
                resetStorage = GlobalDBMethods.AddUserFileStorage(user, fileId, false, session);
                if (resetStorage)
                {
                    session.ExecuteQuery(@"DELETE  
                                       FROM Elio_onboarding_users_library_files 
                                       WHERE 1 = 1 
                                            AND id = @id"
                                        , DatabaseHelper.CreateIntParameter("@id", fileId));

                    session.ExecuteQuery(@"DELETE  
                                       FROM Elio_onboarding_blob_files 
                                       WHERE 1 = 1 
                                            AND library_files_id = @library_files_id"
                                        , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));

                    session.ExecuteQuery(@"DELETE  
                                            FROM Elio_onboarding_blob_preview_files
                                            WHERE library_files_id = @library_files_id"
                                    , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
                }
            }
            else
            {
                resetStorage = GlobalDBMethods.AddUserPreviewFileStorage(user, fileId, false, session);
                if (resetStorage)
                {
                    session.ExecuteQuery(@"UPDATE Elio_onboarding_users_library_files
                                              SET preview_file_path = ''
                                            WHERE id = @id"
                                        , DatabaseHelper.CreateIntParameter("@id", fileId));

                    session.ExecuteQuery(@"DELETE  
                                            FROM Elio_onboarding_blob_preview_files
                                            WHERE library_files_id = @library_files_id"
                                        , DatabaseHelper.CreateIntParameter("@library_files_id", fileId));
                }
            }

            return resetStorage;
        }

        public static List<ElioOnboardingFileTypes> GetOnboardingFileTypes(DBSession session)
        {
            DataLoader<ElioOnboardingFileTypes> loader = new DataLoader<ElioOnboardingFileTypes>(session);

            return loader.Load(@"SELECT * FROM Elio_onboarding_file_types WHERE is_public = 1");
        }

        public static ElioOnboardingFileTypes GetOnboardingPublicFileTypeById(int id, DBSession session)
        {
            DataLoader<ElioOnboardingFileTypes> loader = new DataLoader<ElioOnboardingFileTypes>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_onboarding_file_types 
                                       WHERE is_public = 1
                                       AND id = @id"
                                       , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioOnboardingUsersLibraryFiles GetOnboardingUserLibraryFileById(int id, DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFiles> loader = new DataLoader<ElioOnboardingUsersLibraryFiles>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_onboarding_users_library_files
                                 where id = @id"
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioCollaborationUsersLibraryFiles> GetAllPublicCollaborationUserLibraryFiles(DBSession session)
        {
            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);

            return loader.Load(@"SELECT * FROM Elio_collaboration_users_library_files
                                 where is_public = 1 order by user_id, category_id");
        }

        public static List<ElioOnboardingUsersLibraryFiles> GetAllPublicOnboardingUserLibraryFiles(DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFiles> loader = new DataLoader<ElioOnboardingUsersLibraryFiles>(session);

            return loader.Load(@"SELECT [id]
                                  ,[user_id]
                                  ,[file_name]
                                  ,[file_path]
                                FROM Elio_onboarding_users_library_files
                                 where is_public = 1 and user_id <> 1 order by user_id, file_type");
        }

        public static List<ElioOnboardingUsersLibraryFiles> GetOnboardingUserLibraryFileByCategoryId(int userId, int categoryId, DateTime? from, DateTime? to, DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFiles> loader = new DataLoader<ElioOnboardingUsersLibraryFiles>(session);

            string strQuery = @"
                            SELECT *  
                            FROM Elio_onboarding_users_library_files
                            where 1 = 1
                                AND user_id = @user_id and category_id = @category_id ";

            if (from != null)
            {
                strQuery += " and date_created >= '" + from.Value.Year + "-" + from.Value.Month + "-" + from.Value.Day + "'";
            }

            if (to != null)
            {
                strQuery += " and date_created <= '" + to.Value.Year + "-" + to.Value.Month + "-" + to.Value.Day + "'";
            }

            return loader.Load(strQuery
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@category_id", categoryId));
        }

        public static List<ElioOnboardingUsersLibraryFiles> GetOnboardingUserLibraryFileByAllCategories(int userId, DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFiles> loader = new DataLoader<ElioOnboardingUsersLibraryFiles>(session);

            return loader.Load(@"SELECT * FROM Elio_onboarding_users_library_files
                                 where user_id = @user_id order by category_id, file_type_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsers> GetPossibleMatchUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load("select distinct (user_id) from Elio_users_algorithm_subcategories " +
                                "inner join Elio_users on Elio_users.id=Elio_users_algorithm_subcategories.user_id " +
                                "where subcategory_id in( " +

                                                "select subcategory_id from Elio_users_algorithm_subcategories " +
                                                "group by subcategory_id " +
                                                "HAVING  count (subcategory_id)>1)");
        }

        public static int GetUserSendMessagesCountByMonthRange(int userId, DateTime? fromDate, DateTime? toDate, DBSession session)
        {
            string dateFrom = Convert.ToDateTime(fromDate).Year + "-" + Convert.ToDateTime(fromDate).Month + "-" + Convert.ToDateTime(fromDate).Day;
            string dateTo = Convert.ToDateTime(toDate).Year + "-" + Convert.ToDateTime(toDate).Month + "-" + Convert.ToDateTime(toDate).Day;

            DataTable table = session.GetDataTable(@"SELECT COUNT(id) AS messages_count FROM Elio_users_messages 
                                                     WHERE sender_user_id=@sender_user_id 
                                                     AND sent = 1 
                                                     AND (sysdate>='" + dateFrom + "' and sysdate<='" + dateTo + "')"
                                                     , DatabaseHelper.CreateIntParameter("@sender_user_id", userId));

            return Convert.ToInt32(table.Rows[0]["messages_count"]);
        }

        public static int GetUserLeadsCountByMonthRange(ElioUsers user, DateTime? fromDate, DateTime? toDate, DBSession session)
        {
            string companyTypesIn = "";
            if (user.CompanyType == Types.Vendors.ToString())
            {
                companyTypesIn = "" + EnumHelper.GetDescription(Types.Resellers).ToString() + "";
            }
            else if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                companyTypesIn = "" + Types.Vendors.ToString() + "";
            }

            string dateFrom = Convert.ToDateTime(fromDate).Year + "-" + Convert.ToDateTime(fromDate).Month + "-" + Convert.ToDateTime(fromDate).Day;
            string dateTo = Convert.ToDateTime(toDate).Year + "-" + Convert.ToDateTime(toDate).Month + "-" + Convert.ToDateTime(toDate).Day;

            DataTable table = session.GetDataTable(@"SELECT COUNT(Elio_companies_views_companies.id) as views_count FROM Elio_companies_views_companies 
                                                     INNER JOIN Elio_users on Elio_companies_views_companies.interested_company_id=Elio_users.id 
                                                     WHERE company_views_company_id=@company_views_company_id 
                                                     AND company_type IN ('" + companyTypesIn + "') AND (last_update >= '" + dateFrom + "' AND last_update <= '" + dateTo + "')"
                                                     , DatabaseHelper.CreateIntParameter("@company_views_company_id", user.Id));

            return Convert.ToInt32(table.Rows[0]["views_count"]);
        }

        public static ElioUsersFeatures GetFeaturesbyUserType(int billingType, DBSession session)
        {
            DataLoader<ElioUsersFeatures> loader = new DataLoader<ElioUsersFeatures>(session);
            return loader.LoadSingle("Select * from Elio_users_features where user_billing_type = @user_billing_type", DatabaseHelper.CreateIntParameter("@user_billing_type", billingType));
        }

        public static List<ElioUserPacketStatus> GetAllUserPacketStatusFeatures(DBSession session)
        {
            DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
            return loader.Load("Select * from Elio_user_packet_status with (nolock)");
        }

        public static ElioUserPacketStatus GetUserPacketStatusFeatures(int userId, DBSession session)
        {
            DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
            return loader.LoadSingle(@"Select * from Elio_user_packet_status with (nolock) 
                                       where user_id = @user_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static int GetUserPacketStatusAvailableConnectionsCount(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select available_connections_count from Elio_user_packet_status with (nolock) 
                                                        where user_id = @user_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["available_connections_count"]) : 0;
        }

        public static void DeleteUserPacketStatusFeatures(int userId, DBSession session)
        {
            session.ExecuteQuery(@"Delete from Elio_user_packet_status where user_id = @user_id"
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUserPacketStatus GetUserPacketStatusFeaturesByDate(int userId, DateTime expirationDate, DBSession session)
        {
            DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
            return loader.LoadSingle(@"SELECT * FROM Elio_user_packet_status with (nolock)
                                       WHERE user_id=@user_id 
                                       AND expiration_date <= @expiration_date"
                                       , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                       , DatabaseHelper.CreateDateTimeParameter("@expiration_date", expirationDate));
        }

        public static ElioUserPacketStatusIJBillingOrder GetUserPacketStatusFeaturesInnerJoinOrder(int userId, int orderStatus, int isReadyToUse, DBSession session)
        {
            DataLoader<ElioUserPacketStatusIJBillingOrder> loader = new DataLoader<ElioUserPacketStatusIJBillingOrder>(session);
            return loader.LoadSingle(@"Select * from Elio_user_packet_status with (nolock)
                                       inner join Elio_billing_user_orders 
                                       on Elio_billing_user_orders.user_id=Elio_user_packet_status.user_id
                                       where Elio_user_packet_status.user_id=@user_id
                                       and order_status=@order_status and is_ready_to_use=@is_ready_to_use"
                                       , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                       , DatabaseHelper.CreateIntParameter("@order_status", isReadyToUse)
                                       , DatabaseHelper.CreateIntParameter("@is_ready_to_use", isReadyToUse));
        }

        public static DateTime GetSubscriptionPlanRenewalDate(string customerId, DBSession session)
        {
            //            DataTable table = session.GetDataTable(@"SELECT top 1 current_period_end
            //                                                        FROM Elio_users_subscriptions s
            //                                                        inner join Elio_packets p
            //	                                                        ON s.plan_id = p.stripe_plan_id
            //                                                        where customer_id = @customer_id  
            //                                                        and p.id <> 6"
            //                                                    , DatabaseHelper.CreateStringParameter("@customer_id", customerId));

            DataTable table = session.GetDataTable(@"SELECT current_period_end
                                                        FROM Elio_users_subscriptions s
                                                        where s.plan_id not in
                                                        (
	                                                        Select p.stripe_plan_id
	                                                        from Elio_packets p
	                                                        where p.id = 6
                                                        )
                                                        and customer_id = @customer_id
                                                        and s.status = 'active'"
                                                    , DatabaseHelper.CreateStringParameter("@customer_id", customerId));

            if (table.Rows.Count > 0)
            {
                return Convert.ToDateTime(table.Rows[0]["current_period_end"]);
            }
            else
            {
                //                table = session.GetDataTable(@"SELECT top 1 current_period_end
                //                                                FROM Elio_users_subscriptions s
                //                                                inner join Elio_packets p
                //	                                                ON s.plan_id = p.stripe_plan_old_code
                //                                                where customer_id = @customer_id  
                //                                                and p.id <> 6"
                //                                            , DatabaseHelper.CreateStringParameter("@customer_id", customerId));

                table = session.GetDataTable(@"SELECT current_period_end
                                                FROM Elio_users_subscriptions s
                                                where s.plan_id not in
                                                (
	                                                Select p.stripe_plan_old_code
	                                                from Elio_packets p
	                                                where p.stripe_plan_old_code IN ('Elio_Premium_Service_Plan')
                                                )
                                                and customer_id = @customer_id
                                                and s.status = 'active'"
                                            , DatabaseHelper.CreateStringParameter("@customer_id", customerId));


                if (table.Rows.Count > 0)
                    return Convert.ToDateTime(table.Rows[0]["current_period_end"]);
                else
                    return DateTime.Now;
            }
        }

        public static string GetSubscriptionServiceRenewalDate(string customerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT top 1 current_period_end
                                                        FROM Elio_users_subscriptions s
                                                        inner join Elio_packets p
	                                                        ON s.plan_id = p.stripe_plan_id
                                                        where customer_id = @customer_id  
                                                        and p.id = 6"
                                                    , DatabaseHelper.CreateStringParameter("@customer_id", customerId));

            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["current_period_end"].ToString();
            }
            else
            {
                table = session.GetDataTable(@"SELECT top 1 current_period_end
                                                FROM Elio_users_subscriptions s
                                                inner join Elio_packets p
	                                                ON s.plan_id = p.stripe_plan_id
                                                where customer_id = @customer_id  
                                                and p.id = 6"
                                            , DatabaseHelper.CreateStringParameter("@customer_id", customerId));

                if (table.Rows.Count > 0)
                    return table.Rows[0]["current_period_end"].ToString();
                else
                    return "";
            }
        }

        public static DateTime GetUserRenewalDateFromActiveOrder1(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select mode, current_period_end from Elio_billing_user_orders with (nolock) " +
                                                   "where user_id=@user_id " +
                                                   "and order_status in(1) order by sysdate asc"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            if (table.Rows.Count > 0)
            {
                return (table.Rows[0]["mode"]).ToString() == OrderMode.Trialing.ToString() ? Convert.ToDateTime(table.Rows[0]["current_period_end"]).AddMonths(1) : Convert.ToDateTime(table.Rows[0]["current_period_end"]);
            }
            else
                return DateTime.Now;
        }

        public static ElioUserPacketStatus GetUserExpiredPacketStatusFeatures(int userId, DBSession session)
        {
            DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
            return loader.LoadSingle("Select * from Elio_user_packet_status with (nolock) " +
                                     "where user_id=@user_id " +
                                     "and expiration_date<'" + DateTime.Now.ToString("yyyy-MM-dd") + "'"
                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static bool UserHasActiveOrderByPacketAndType(int userId, int orderType, int packId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count from Elio_billing_user_orders WITH (NOLOCK) 
                                                    WHERE user_id=@user_id 
                                                    AND pack_id=@pack_id 
                                                    AND order_status=1
                                                    AND is_ready_to_use=1 
                                                    AND order_type=@order_type"
                                                   , DatabaseHelper.CreateIntParameter("user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@pack_id", packId)
                                                   , DatabaseHelper.CreateIntParameter("@order_type", orderType));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool UserHasCancelOrderByPacketAndType(int userId, int orderType, int packId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count from Elio_billing_user_orders WITH (NOLOCK) 
                                                    WHERE user_id=@user_id 
                                                    AND pack_id=@pack_id 
                                                    AND order_status=-3
                                                    AND is_ready_to_use=0 
                                                    AND order_type=@order_type"
                                                   , DatabaseHelper.CreateIntParameter("user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@pack_id", packId)
                                                   , DatabaseHelper.CreateIntParameter("@order_type", orderType));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static ElioBillingUserOrders GetUserActiveOrderByPacketAndType(int userId, int orderType, int packId, DBSession session)
        {
            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);

            return loader.LoadSingle(@"SELECT * from Elio_billing_user_orders WITH (NOLOCK) 
                                                    WHERE user_id=@user_id 
                                                    AND pack_id=@pack_id 
                                                    AND order_status=1 
                                                    AND is_ready_to_use=1 
                                                    AND order_type=@order_type"
                                                   , DatabaseHelper.CreateIntParameter("user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@pack_id", packId)
                                                   , DatabaseHelper.CreateIntParameter("@order_type", orderType));

        }

        public static bool UserOrderHasExpired(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_billing_user_orders with (nolock) " +
                                                   "where user_id=@user_id " +
                                                   "and order_status=-1 " +
                                                   "and is_ready_to_use=1 " +
                                                   "and expiration_date<'" + DateTime.Now.ToString() + "'"
                                                   , DatabaseHelper.CreateIntParameter("user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        //public static bool HasExpiredOrder(int userId, DBSession session)
        //{
        //    DataTable table = session.GetDataTable("Select count(id) as count from Elio_billing_user_orders with (nolock) " +
        //                                           "where user_id=@user_id " +
        //                                           "and order_status=-2 " +
        //                                           "and is_ready_to_use=0 " +
        //                                           "and expiration_date<'" + DateTime.Now.ToString() + "'"
        //                                           , DatabaseHelper.CreateIntParameter("user_id", userId));

        //    return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        //}

        public static bool HasUserDiscount(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_discount_users with (nolock) " +
                                                   "where user_id=@user_id " +
                                                   "and expiration_date<'" + DateTime.Now.AddDays(1).AddMonths(1).ToString() + "'"
                                                   , DatabaseHelper.CreateIntParameter("user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool HasUserDiscountCouponByPlan(int userId, int planCouponsId, int packetId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select COUNT(id) as count
                                                      from Elio_users_plan_coupons_discount upcd
                                                      where user_id = @user_id
                                                      and plan_coupons_id = @plan_coupons_id
                                                      and parent_pack_id = @parent_pack_id"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@plan_coupons_id", planCouponsId)
                                                   , DatabaseHelper.CreateIntParameter("@parent_pack_id", packetId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static ElioUsersPlanCouponsDiscount GetUserPlanCouponsDiscount(int userId, int planCouponsId, DBSession session)
        {
            DataLoader<ElioUsersPlanCouponsDiscount> loader = new DataLoader<ElioUsersPlanCouponsDiscount>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_users_plan_coupons_discount upcd
                                       WHERE user_id = @user_id
                                       AND plan_coupons_id = @plan_coupons_id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateIntParameter("@plan_coupons_id", planCouponsId));
        }

        public static bool UserHasPendingOrder(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_billing_user_orders with (nolock) " +
                                                   "where user_id=@user_id " +
                                                   "and is_ready_to_use=0 " +
                                                   "and order_status=-1"
                                                   , DatabaseHelper.CreateIntParameter("user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool UserHasNotPaidOrder(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_billing_user_orders with (nolock) where user_id=@user_id and order_status=2"
                                                   , DatabaseHelper.CreateIntParameter("user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool UserHasSpecificOrderWithPurchase(int userId, int orderStatus, int isReadyToUse, int isPaid, DBSession session)
        {
            DataTable table = session.GetDataTable("select count(Elio_billing_user_orders.id) as count from Elio_billing_user_orders " +
                                                   "inner join Elio_billing_purchases on Elio_billing_purchases.order_id=Elio_billing_user_orders.id " +
                                                   "where " +
                                                   "Elio_billing_user_orders.user_id=@user_id " +
                                                   "and order_status=@order_status " +
                                                   "and is_ready_to_use=@is_ready_to_use " +
                                                   "and is_paid=@is_paid"

                                                        , DatabaseHelper.CreateIntParameter("user_id", userId)
                                                        , DatabaseHelper.CreateIntParameter("@order_status", orderStatus)
                                                        , DatabaseHelper.CreateIntParameter("@is_ready_to_use", isReadyToUse)
                                                        , DatabaseHelper.CreateIntParameter("@is_paid", isPaid));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        //public static ElioBillingPurchases GetPurchaseByOrderIdAndPaidStatusAndType(int orderId, int isPaid, int purchaseType, DBSession session)
        //{
        //    DataLoader<ElioBillingPurchases> loader = new DataLoader<ElioBillingPurchases>(session);
        //    return loader.LoadSingle("Select * from Elio_billing_purchases with (nolock) " +
        //                             "where order_id=@order_id " +
        //                             "and is_paid=@is_paid " +
        //                             "and purchase_type=@purchase_type"
        //                             , DatabaseHelper.CreateIntParameter("@order_id", orderId)
        //                             , DatabaseHelper.CreateIntParameter("@is_paid", isPaid)
        //                             , DatabaseHelper.CreateIntParameter("@purchase_type", purchaseType));
        //}

        //public static List<ElioBillingUserOrdersIJPurchase> GetUserPurchases(int userId, DBSession session)
        //{
        //    DataLoader<ElioBillingUserOrdersIJPurchase> loader = new DataLoader<ElioBillingUserOrdersIJPurchase>(session);
        //    return loader.Load("Select * from Elio_billing_purchases with (nolock) " +
        //                       "inner join Elio_billing_user_orders on Elio_billing_user_orders.id=Elio_billing_purchases.order_id " +
        //                       "where Elio_billing_purchases.user_id=@user_id " +
        //                       "order by Elio_billing_purchases.sysdate asc"
        //                             , DatabaseHelper.CreateIntParameter("@user_id", userId));
        //}

        public static ElioBillingUserOrders GetUserOrderByStatus(int userId, int status, DBSession session)
        {
            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
            return loader.LoadSingle("Select * from Elio_billing_user_orders with (nolock) " +
                                     "where user_id=@user_id " +
                                     "and order_status=@order_status"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@order_status", status));
        }

        public static bool HasUserOrderByStatus(int userId, int orderStatus, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select COUNT(*) as count from Elio_billing_user_orders buo
                                                        inner join dbo.Elio_billing_user_orders_payments buop on buo.id = buop.order_id
                                                      where buo.user_id=@user_id 
                                                      and buo.order_status=@order_status"
                                                  , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                  , DatabaseHelper.CreateIntParameter("@order_status", orderStatus));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool GetUserLastOrderByStatus(int userId, int orderStatus, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select COUNT(*) as count from Elio_billing_user_orders
                                                    where id in(
	                                                    select top 1 id from Elio_billing_user_orders
	                                                    where user_id=@user_id  
	                                                    order by sysdate desc)
                                                    and order_status=@order_status"
                                                  , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                  , DatabaseHelper.CreateIntParameter("@order_status", orderStatus));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool HasOrderByMode(int userId, string orderMode, DBSession sessio)
        {
            DataTable table = sessio.GetDataTable("Select COUNT(*) as count from Elio_billing_user_orders with (nolock) " +
                                                  "where user_id=@user_id " +
                                                  "and mode=@mode"
                                                  , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                  , DatabaseHelper.CreateStringParameter("@mode", orderMode));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static List<ElioBillingUserOrdersPayments> GetUserOrdersPayments(int userId, DBSession session)
        {
            DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
            return loader.Load(@"SELECT * FROM Elio_billing_user_orders_payments WITH (NOLOCK)
                                 WHERE user_id = @user_id 
                                 ORDER BY date_created desc"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioBillingUserOrdersPayments> GetUserPaymentsByOrderId(int orderId, DBSession session)
        {
            DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
            return loader.Load(@"SELECT * FROM Elio_billing_user_orders_payments WITH (NOLOCK)
                                 WHERE order_id = @order_id
                                 ORDER BY current_period_start desc"
                                , DatabaseHelper.CreateIntParameter("@order_id", orderId));
        }

        public static ElioBillingUserOrdersPayments GetUserLastPaymentsByOrderId(int orderId, DBSession session)
        {
            DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
            return loader.LoadSingle(@"SELECT TOP 1 * FROM Elio_billing_user_orders_payments WITH (NOLOCK)
                                 WHERE order_id = @order_id
                                 ORDER BY current_period_start desc"
                                , DatabaseHelper.CreateIntParameter("@order_id", orderId));
        }

        public static bool IsCurrentUserPacketPayment(int userId, int paymentId, DBSession session)
        {
            DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
            ElioBillingUserOrdersPayments lastPayment = loader.LoadSingle(@"SELECT TOP 1 * FROM Elio_billing_user_orders_payments WITH (NOLOCK)
                                 WHERE 1 = 1 AND pack_id <> 6
                                 AND user_id = @user_id
                                 ORDER BY current_period_start desc"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (lastPayment != null && lastPayment.Id == paymentId) ? true : false;
        }

        public static bool IsLastServicePayment(int paymentId, DBSession session)
        {
            DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
            ElioBillingUserOrdersPayments lastServicePayment = loader.LoadSingle(@"SELECT TOP 1 * 
                                                                            FROM Elio_billing_user_orders_payments uop
                                                                            INNER JOIN Elio_billing_user_orders buo
	                                                                            ON uop.order_id = buo.id
                                                                            WHERE 1 = 1
                                                                            AND buo.pack_id = 6
                                                                            ORDER BY uop.current_period_start desc");

            return (lastServicePayment != null && lastServicePayment.Id == paymentId) ? true : false;
        }

        public static ElioBillingUserOrdersPayments GetUserPaymentsByOrderIdAndDateRange(int orderId, DateTime currentStart, DateTime currentEnd, DBSession session)
        {
            DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
            return loader.LoadSingle(@"SELECT TOP 1 * FROM Elio_billing_user_orders_payments WITH (NOLOCK)
                                 WHERE order_id = @order_id
                                 AND current_period_start >= @current_period_start and current_period_end <= @current_period_end
                                 ORDER BY current_period_start desc"
                                , DatabaseHelper.CreateIntParameter("@order_id", orderId)
                                , DatabaseHelper.CreateDateTimeParameter("@current_period_start", currentStart)
                                , DatabaseHelper.CreateDateTimeParameter("@current_period_end", currentEnd));
        }

        public static ElioBillingUserOrdersPayments GetUserOrdersPaymentById(int userId, int paymentId, DBSession session)
        {
            DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
            return loader.LoadSingle(@"SELECT * FROM Elio_billing_user_orders_payments WITH (NOLOCK)
                                 WHERE user_id = @user_id 
                                 and id = @id
                                 ORDER BY date_created desc"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@id", paymentId));
        }

        public static ElioBillingUserOrdersPayments GetBillingPaymentById(int paymentId, DBSession session)
        {
            DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
            return loader.LoadSingle(@"SELECT * FROM Elio_billing_user_orders_payments WITH (NOLOCK)
                                 WHERE 1 = 1
                                 and id = @id
                                 ORDER BY date_created desc"
                                , DatabaseHelper.CreateIntParameter("@id", paymentId));
        }

        public static List<ElioBillingUserOrders> GetUserOrders(int userId, DBSession session)
        {
            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
            return loader.Load(@"SELECT * FROM Elio_billing_user_orders WITH (NOLOCK)
                                 WHERE user_id = @user_id 
                                 ORDER BY id ASC"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioBillingUserOrders GetOrderById(int id, DBSession session)
        {
            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
            return loader.LoadSingle(@"SELECT * FROM Elio_billing_user_orders WITH (NOLOCK)
                                        WHERE id = @id"
                                     , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static bool IsUserLastOrder(int userId, int orderId, DBSession session)
        {
            DataTable table = session.GetDataTable(@";WITH maxOrderId
                                                    AS
                                                    (
	                                                    SELECT max(id) AS maxID FROM Elio_billing_user_orders WITH (NOLOCK)
	                                                    WHERE 1 = 1 
	                                                    AND user_id = 2308
	                                                    AND order_type = 1
	                                                    AND order_status = -3
                                                    )

                                                    SELECT 
	                                                    CASE WHEN ISNULL(maxID, 0) > @order_id THEN 0 ELSE 1 END AS isLastOrder
                                                    FROM maxOrderId"
                                                    , DatabaseHelper.CreateIntParameter("@order_id", orderId));

            return ((table.Rows.Count > 0) && (Convert.ToInt32(table.Rows[0]["isLastOrder"]) == 1)) ? true : false;
        }

        public static ElioBillingUserOrders GetUserLastActiveOrder(int userId, DBSession session)
        {
            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
            return loader.LoadSingle(@"Select * from Elio_billing_user_orders with (nolock) 
                                       where user_id = @user_id "
                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioBillingUserOrders GetUserOrderByStatusAndUse(int userId, int status, int readyToUse, DBSession session)
        {
            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
            return loader.LoadSingle("Select * from Elio_billing_user_orders with (nolock) " +
                                     "where user_id=@user_id " +
                                     "and is_ready_to_use=@is_ready_to_use " +
                                     "and order_status=@order_status"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@is_ready_to_use", readyToUse)
                                        , DatabaseHelper.CreateIntParameter("@order_status", status));
        }

        public static ElioBillingUserOrders HasUserOrderByPacketStatusUse(ElioUsers user, int status, int readyToUse, DBSession session)
        {
            ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(user.BillingType, session);
            if (packet != null)
            {
                DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
                return loader.LoadSingle(@"Select * from Elio_billing_user_orders with (nolock) 
                                       where user_id=@user_id 
                                       AND pack_id=@pack_id 
                                       AND order_status=@order_status 
                                       AND is_ready_to_use=@is_ready_to_use"
                                            , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                            , DatabaseHelper.CreateIntParameter("@pack_id", packet.Id)
                                            , DatabaseHelper.CreateIntParameter("@order_status", status)
                                            , DatabaseHelper.CreateIntParameter("@is_ready_to_use", readyToUse));
            }
            else
                return null;
        }

        public static ElioBillingUserOrders HasUserOrderByServicePacketStatusUse(int userId, int packId, int status, int readyToUse, DBSession session)
        {
            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
            return loader.LoadSingle(@"Select * from Elio_billing_user_orders with (nolock) 
                                       where user_id=@user_id 
                                       AND pack_id=@pack_id 
                                       AND order_status=@order_status 
                                       AND is_ready_to_use=@is_ready_to_use"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@pack_id", packId)
                                        , DatabaseHelper.CreateIntParameter("@order_status", status)
                                        , DatabaseHelper.CreateIntParameter("@is_ready_to_use", readyToUse));
        }

        public static ElioBillingUserOrders GetUserPendingOrderToBeDeleted(int userId, string orderStatus, int isPaid, int isReadyToUse, DBSession session)
        {
            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
            return loader.LoadSingle("select * from Elio_billing_user_orders  " +
                                        "inner join Elio_billing_purchases on Elio_billing_purchases.order_id=Elio_billing_user_orders.id " +
                                        "where Elio_billing_user_orders.user_id=@user_id " +
                                        "and order_status in(" + orderStatus + ") " +
                                        "and is_paid=@is_paid " +
                                        "and is_ready_to_use=@is_ready_to_use"

                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@is_paid", isPaid)
                                            , DatabaseHelper.CreateIntParameter("@is_ready_to_use", isReadyToUse));
        }

        public static ElioUsersBillingAccount GetUserAccountByUserId(int userId, DBSession session)
        {
            DataLoader<ElioUsersBillingAccount> loader = new DataLoader<ElioUsersBillingAccount>(session);
            return loader.LoadSingle("Select * from Elio_users_billing_account with (nolock) where user_id=@user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsersCreditCards GetUserDefaultCreditCard(int userId, string customerStripeId, DBSession session)
        {
            DataLoader<ElioUsersCreditCards> loader = new DataLoader<ElioUsersCreditCards>(session);
            return loader.LoadSingle(@"Select * from Elio_users_credit_cards with (nolock) 
                                       where user_id=@user_id
                                       and customer_stripe_id = @customer_stripe_id 
                                       and is_default = 1"
                                       , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                       , DatabaseHelper.CreateStringParameter("@customer_stripe_id", customerStripeId));
        }

        public static ElioUsersCreditCards GetUserDefaultCreditCardByUserId(int userId, DBSession session)
        {
            DataLoader<ElioUsersCreditCards> loader = new DataLoader<ElioUsersCreditCards>(session);
            return loader.LoadSingle(@"Select * from Elio_users_credit_cards with (nolock) 
                                       where user_id=@user_id 
                                       and is_default = 1"
                                       , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioPackets> GetActivePackets(DBSession session)
        {
            DataLoader<ElioPackets> loader = new DataLoader<ElioPackets>(session);

            return loader.Load(@"Select * from Elio_packets where is_active = 1");
        }

        public static List<ElioPackets> GetDefaultActivePackets(DBSession session)
        {
            DataLoader<ElioPackets> loader = new DataLoader<ElioPackets>(session);

            return loader.Load(@"Select * from Elio_packets where is_active = 1 AND is_default = 1");
        }

        public static ElioPackets GetPacketByStripePlanId(string planId, DBSession session)
        {
            DataLoader<ElioPackets> loader = new DataLoader<ElioPackets>(session);

            return loader.LoadSingle(@"Select TOP 1 * from Elio_packets 
                                        where is_active = 1
                                        and product_id <> 0
                                        and stripe_plan_id = @stripe_plan_id"
                        , DatabaseHelper.CreateStringParameter("@stripe_plan_id", planId));
        }

        public static ElioPackets GetPacketById(int packId, DBSession session)
        {
            DataLoader<ElioPackets> loader = new DataLoader<ElioPackets>(session);

            return loader.LoadSingle(@"Select * from Elio_packets where id=@id", DatabaseHelper.CreateIntParameter("@id", packId));
        }

        public static ElioPackets GetPacketByStripePlanOldCode(string planOldCode, DBSession session)
        {
            DataLoader<ElioPackets> loader = new DataLoader<ElioPackets>(session);

            return loader.LoadSingle(@"Select * from Elio_packets where stripe_plan_old_code= @stripe_plan_old_code", DatabaseHelper.CreateStringParameter("@stripe_plan_old_code", planOldCode));
        }

        public static bool ExistsPacketById(int packId, DBSession session)
        {
            DataTable table = new DataTable();

            table = session.GetDataTable(@"Select COUNT(id) AS count from Elio_packets where id = @id", DatabaseHelper.CreateIntParameter("@id", packId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static List<ElioPackets> GetFeaturesByPackId(int packId, DBSession session)
        {
            DataLoader<ElioPackets> loader = new DataLoader<ElioPackets>(session);

            return loader.Load(@"select Elio_packets.id, pack_description, feature_id, free_items_no, item_cost_with_no_vat, item_cost_with_vat, vat, item_description 
                                from Elio_packets 
                                inner join Elio_packet_features_items on Elio_packet_features_items.pack_id=Elio_packets.id 
                                inner join Elio_packet_features on Elio_packet_features.id = Elio_packet_features_items.feature_id 
                                where Elio_packets.is_active=1 
                                and Elio_packet_features.is_active = 1 
                                and is_public = 1 
                                and Elio_packets.id = @id"
                                                       , DatabaseHelper.CreateIntParameter("@id", packId));
        }

        public static List<ElioPacketsIJFeaturesItems> GetPacketFeaturesItems(int packId, DBSession session)
        {
            DataLoader<ElioPacketsIJFeaturesItems> loader = new DataLoader<ElioPacketsIJFeaturesItems>(session);

            return loader.Load(@"select Elio_packets.id, pack_description, feature_id, free_items_no, free_items_trial_no, item_cost_with_no_vat, item_cost_with_vat, vat, item_description 
                                 from Elio_packets 
                                 inner join Elio_packet_features_items on Elio_packet_features_items.pack_id=Elio_packets.id 
                                 inner join Elio_packet_features on Elio_packet_features.id = Elio_packet_features_items.feature_id 
                                 where Elio_packets.id = @id 
                                 and Elio_packets.is_active=1 
                                 and Elio_packet_features.is_active = 1 
                                 and is_public = 1"
                                , DatabaseHelper.CreateIntParameter("@id", packId));
        }

        public static int GetUserViewableConnectionsForCurrentPeriod(int userId, DateTime? currentPeriodStart, DateTime? currentPeriodEnd, DBSession session)
        {
            string periodStart = Convert.ToDateTime(currentPeriodStart).Year + "-" + Convert.ToDateTime(currentPeriodStart).Month + "-" + Convert.ToDateTime(currentPeriodStart).Day;
            string periodEnd = Convert.ToDateTime(currentPeriodEnd).Year + "-" + Convert.ToDateTime(currentPeriodEnd).Month + "-" + Convert.ToDateTime(currentPeriodEnd).Day;

            DataTable table = session.GetDataTable(@"Select count(id) as total_connections_count from Elio_users_connections 
                                                   where user_id=@user_id 
                                                   and can_be_viewed=1 
                                                   and (sysdate>='" + periodStart + "' and sysdate<='" + periodEnd + "')"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["total_connections_count"]);
        }

        public static int GetUserInvitationsForCurrentPeriod(int userId, string invitationStepDescription, DateTime? currentPeriodStart, DateTime? currentPeriodEnd, DBSession session)
        {
            string periodStart = Convert.ToDateTime(currentPeriodStart).Year + "-" + Convert.ToDateTime(currentPeriodStart).Month + "-" + Convert.ToDateTime(currentPeriodStart).Day;
            string periodEnd = Convert.ToDateTime(currentPeriodEnd).Year + "-" + Convert.ToDateTime(currentPeriodEnd).Month + "-" + Convert.ToDateTime(currentPeriodEnd).AddDays(1).Day;

            DataTable table = session.GetDataTable(@"SELECT COUNT(id) as total_invitations_count
                                                      FROM Elio_collaboration_vendor_reseller_invitations
                                                      where user_id = @user_id
                                                      and invitation_step_description = @invitation_step_description
                                                      and (send_date >= '" + periodStart + "' and send_date <= '" + periodEnd + "')"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateStringParameter("@invitation_step_description", invitationStepDescription));

            return Convert.ToInt32(table.Rows[0]["total_invitations_count"]);
        }

        public static int GetUserLibraryFilesStorageForCurrentPeriod(int userId, DateTime? currentPeriodStart, DateTime? currentPeriodEnd, DBSession session)
        {
            string periodStart = Convert.ToDateTime(currentPeriodStart).Year + "-" + Convert.ToDateTime(currentPeriodStart).Month + "-" + Convert.ToDateTime(currentPeriodStart).Day;
            string periodEnd = Convert.ToDateTime(currentPeriodEnd).Year + "-" + Convert.ToDateTime(currentPeriodEnd).Month + "-" + Convert.ToDateTime(currentPeriodEnd).AddDays(1).Day;

            DataTable table = session.GetDataTable(@"SELECT SUM(cast(file_size as int)) AS total_size
                                                      FROM Elio_collaboration_users_library_files ulf
                                                      inner join Elio_collaboration_blob_files blf
	                                                    on blf.library_files_id = ulf.id  
                                                      where (uploaded_by_user_id = @uploaded_by_user_id and user_id <> @user_id) 
                                                      and (ulf.date_created >='" + periodStart + "' and ulf.date_created <='" + periodEnd + "')"
                                                    , DatabaseHelper.CreateIntParameter("@uploaded_by_user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (!string.IsNullOrEmpty(table.Rows[0]["total_size"].ToString())) ? Convert.ToInt32(table.Rows[0]["total_size"]) : 0;
        }

        public static decimal GetPacketTotalCostWithVat(int packId, DBSession session)
        {
            DataLoader<ElioPacketFeaturesItems> loader = new DataLoader<ElioPacketFeaturesItems>(session);

            List<ElioPacketFeaturesItems> items = loader.Load(@"SELECT * FROM Elio_packet_features_items where pack_id=@pack_id", DatabaseHelper.CreateIntParameter("pack_id", packId));

            decimal cost = 0;

            foreach (ElioPacketFeaturesItems item in items)
            {
                cost += item.ItemCostWithVat * item.FreeItemsNo;
            }

            return cost;
        }

        //public static ElioUsersFeatures GetUserFeaturesByBillingType(int billingTypeId, DBSession session)
        //{
        //    DataLoader<ElioUsersFeatures> loader = new DataLoader<ElioUsersFeatures>(session);

        //    return loader.LoadSingle("SELECT * FROM Elio_users_features where user_billing_type=@user_billing_type", DatabaseHelper.CreateIntParameter("user_billing_type", billingTypeId));
        //}

        public static int GetPacketTotalCostAndFeatures(int packId, ref int? totalLeads, ref int? totalMessages, ref int? totalConnections, ref int? totalCollaborationPartners, ref int? totalFileStorage, DBSession session)
        {
            DataLoader<ElioPacketFeaturesItems> loader = new DataLoader<ElioPacketFeaturesItems>(session);

            List<ElioPacketFeaturesItems> items = loader.Load(@"SELECT * FROM Elio_packet_features_items where pack_id=@pack_id", DatabaseHelper.CreateIntParameter("pack_id", packId));

            decimal cost = 0;

            foreach (ElioPacketFeaturesItems item in items)
            {
                cost += item.ItemCostWithVat * item.FreeItemsNo;
            }

            if (items.Count > 0)
            {
                totalLeads = items[0].FreeItemsNo;
                totalMessages = items[1].FreeItemsNo;
                totalConnections = items[2].FreeItemsNo;

                if (items.Count > 3)
                {
                    totalCollaborationPartners = items[3].FreeItemsNo;
                    totalFileStorage = items[4].FreeItemsNo;
                }
            }

            return (int)cost;
        }

        public static List<ElioPacketFeaturesItems> GetAllPublicPacketTotalCostAndFeatures(DBSession session)
        {
            DataLoader<ElioPacketFeaturesItems> loader = new DataLoader<ElioPacketFeaturesItems>(session);

            return loader.Load(@"SELECT * FROM Elio_packet_features_items where is_public = 1");
        }

        public static List<ElioPacketFeaturesItems> GetPublicPacketTotalCostAndFeaturesById(int packId, DBSession session)
        {
            DataLoader<ElioPacketFeaturesItems> loader = new DataLoader<ElioPacketFeaturesItems>(session);

            return loader.Load(@"SELECT * FROM Elio_packet_features_items where is_public = 1 and pack_id = @pack_id"
                                , DatabaseHelper.CreateIntParameter("@pack_id", packId));
        }

        public static decimal GetPacketTotalCostWithNoVat(int packId, DBSession session)
        {
            DataLoader<ElioPacketFeaturesItems> loader = new DataLoader<ElioPacketFeaturesItems>(session);

            List<ElioPacketFeaturesItems> items = loader.Load(@"SELECT * FROM Elio_packet_features_items where pack_id=@pack_id", DatabaseHelper.CreateIntParameter("@pack_id", packId));

            decimal cost = 0;

            foreach (ElioPacketFeaturesItems item in items)
            {
                cost += item.ItemCostWithNoVat * item.FreeItemsNo;
            }

            return cost;
        }

        public static List<ElioUsers> GetFollowersEmailsForNewPostCreated(int communityEmailNotificationsId, int userId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load(@"select distinct(email) from Elio_users 
                                     inner join Elio_community_user_email_notifications on Elio_community_user_email_notifications.user_id=Elio_users.id 
                                     inner join Elio_community_email_notifications on Elio_community_email_notifications.id=Elio_community_user_email_notifications.community_email_notifications_id 
                                     inner join Elio_community_user_profiles_followers on Elio_community_user_profiles_followers.follower_user_id=Elio_users.id 
                                 where 
                                     Elio_community_user_email_notifications.community_email_notifications_id=@community_email_notifications_id 
                                     and Elio_community_user_profiles_followers.user_id=@user_id 
                                     and Elio_community_email_notifications.is_public=1"
                                                                     , DatabaseHelper.CreateIntParameter("community_email_notifications_id", communityEmailNotificationsId)
                                                                     , DatabaseHelper.CreateIntParameter("user_id", userId));
        }

        public static List<string> GetFollowersEmails(int communityEmailNotificationsId, int followerUserId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            List<ElioUsers> users = loader.Load(@"select distinct(email) from Elio_users 
                                    inner join Elio_community_user_email_notifications on Elio_community_user_email_notifications.user_id=Elio_users.id 
                                    inner join Elio_community_email_notifications on Elio_community_email_notifications.id=Elio_community_user_email_notifications.community_email_notifications_id 
                                    inner join Elio_community_user_profiles_followers on Elio_community_user_profiles_followers.user_id=Elio_users.id 
                               where 
                                    Elio_community_user_email_notifications.community_email_notifications_id=@community_email_notifications_id 
                                    and Elio_community_user_profiles_followers.follower_user_id=@follower_user_id 
                                    and Elio_community_email_notifications.is_public=1"
                                    , DatabaseHelper.CreateIntParameter("community_email_notifications_id", communityEmailNotificationsId)
                                    , DatabaseHelper.CreateIntParameter("follower_user_id", followerUserId));

            List<string> emails = new List<string>();
            foreach (ElioUsers email in users)
            {
                emails.Add(email.Email);
            }

            return emails;
        }

        public static ElioUsers GetCommentedOrUpvotedPostUserEmail(int postId, int communityEmailNotificationsId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.LoadSingle(@"select distinct(email) from Elio_users 
                                           inner join Elio_community_posts on Elio_community_posts.creator_user_id = Elio_users.id
                                           inner join Elio_community_user_email_notifications on Elio_community_user_email_notifications.user_id=Elio_users.id 
                                           inner join Elio_community_email_notifications on Elio_community_email_notifications.id=Elio_community_user_email_notifications.community_email_notifications_id 
                                       where Elio_community_user_email_notifications.community_email_notifications_id=@community_email_notifications_id 
                                           and Elio_community_email_notifications.is_public=1 
                                           and Elio_community_posts.id=@id"
                                     , DatabaseHelper.CreateIntParameter("@community_email_notifications_id", communityEmailNotificationsId)
                                     , DatabaseHelper.CreateIntParameter("@id", postId));
        }

        public static ElioUsers GetFollowingUserEmail(int userId, int communityEmailNotificationsId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.LoadSingle(@"select distinct(email) from Elio_users 
                                            inner join Elio_community_user_email_notifications on Elio_community_user_email_notifications.user_id=Elio_users.id 
                                       where community_email_notifications_id=@community_email_notifications_id
                                            and Elio_users.id=@id"
                                     , DatabaseHelper.CreateIntParameter("@community_email_notifications_id", communityEmailNotificationsId)
                                     , DatabaseHelper.CreateIntParameter("@id", userId));
        }

        public static ElioCommunityPostsComments GetCommentById(int id, DBSession session)
        {
            DataLoader<ElioCommunityPostsComments> loader = new DataLoader<ElioCommunityPostsComments>(session);

            return loader.LoadSingle(@"Select * from Elio_community_posts_comments where id=@id", DatabaseHelper.CreateIntParameter("id", id));
        }

        public static int GetMaxDepth(int postId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select top(1) Elio_community_posts_comments.depth as depth from Elio_community_posts_comments where community_post_id=@community_post_id order by depth desc"
                                                   , DatabaseHelper.CreateIntParameter("community_post_id", postId));

            return (!string.IsNullOrEmpty(table.Rows[0]["depth"].ToString())) ? Convert.ToInt32(table.Rows[0]["depth"]) : 0;
        }

        public static ElioCommunityPostsComments GetPostCommentById(int id, DBSession session)
        {
            DataLoader<ElioCommunityPostsComments> loader = new DataLoader<ElioCommunityPostsComments>(session);

            return loader.LoadSingle(@"Select * from Elio_community_posts_comments where id=@id", DatabaseHelper.CreateIntParameter("id", id));
        }

        public static List<ElioCommunityPostsCommentsIJUsers> GetPostAllCommentsIJUsers(int postId, DBSession session)
        {
            DataLoader<ElioCommunityPostsCommentsIJUsers> loader = new DataLoader<ElioCommunityPostsCommentsIJUsers>(session);

            return loader.Load(@"Select * from Elio_community_posts_comments 
                                 inner join Elio_users on Elio_users.id=Elio_community_posts_comments.user_id 
                                 where Elio_community_posts_comments.community_post_id=@community_post_id 
                                 and Elio_community_posts_comments.is_public=1 
                                 order by Elio_community_posts_comments.id desc"
                               , DatabaseHelper.CreateIntParameter("community_post_id", postId));
        }

        public static ElioCommunityPosts GetPostByPostId(int postId, DBSession session)
        {
            DataLoader<ElioCommunityPosts> loader = new DataLoader<ElioCommunityPosts>(session);

            return loader.LoadSingle(@"Select * from Elio_community_posts where id=@id"
                                     , DatabaseHelper.CreateIntParameter("id", postId));
        }

        public static ElioCommunityPostsVotes GetPostVotesByUserId(int userId, int postId, DBSession session)
        {
            DataLoader<ElioCommunityPostsVotes> loader = new DataLoader<ElioCommunityPostsVotes>(session);

            return loader.LoadSingle(@"Select * from Elio_community_posts_votes where user_id=@user_id and community_post_id=@community_post_id"
                                     , DatabaseHelper.CreateIntParameter("user_id", userId)
                                     , DatabaseHelper.CreateIntParameter("community_post_id", postId));
        }

        public static bool UserHasUpVotePost(int userId, int postId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(id) as count from Elio_community_posts_votes where user_id=@user_id and community_post_id=@community_post_id"
                                                   , DatabaseHelper.CreateIntParameter("user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("community_post_id", postId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static List<ElioCommunityPostsIJUsers> GetAllCommunityPostsDetailsOrderBy(string orderBy, DBSession session)
        {
            DataLoader<ElioCommunityPostsIJUsers> loader = new DataLoader<ElioCommunityPostsIJUsers>(session);

            return loader.Load(@"Select * from Elio_community_posts 
                               inner join Elio_users on Elio_users.id=Elio_community_posts.creator_user_id 
                               where Elio_community_posts.is_public=1 " + orderBy);
        }

        public static List<ElioCommunityPostsIJUsers> GetAllCommunityPostsDetails(DBSession session)
        {
            DataLoader<ElioCommunityPostsIJUsers> loader = new DataLoader<ElioCommunityPostsIJUsers>(session);

            return loader.Load(@"Select * from Elio_community_posts 
                               inner join Elio_users on Elio_users.id=Elio_community_posts.creator_user_id 
                               where Elio_community_posts.is_public=1 order by total_votes desc");
        }

        public static List<ElioCommunityPostsIJUsers> GetUserCommunityPostsDetails(int creatorId, DBSession session)
        {
            DataLoader<ElioCommunityPostsIJUsers> loader = new DataLoader<ElioCommunityPostsIJUsers>(session);

            return loader.Load(@"Select * from Elio_community_posts 
                               inner join Elio_users on Elio_users.id=Elio_community_posts.creator_user_id 
                               where creator_user_id=@creator_user_id and Elio_community_posts.is_public=1 order by total_votes desc"
                               , DatabaseHelper.CreateIntParameter("creator_user_id", creatorId));
        }

        public static List<ElioCommunityPosts> GetCommunityPostsIJCommentsByCommentUserId(int userId, DBSession session)
        {
            DataLoader<ElioCommunityPosts> loader = new DataLoader<ElioCommunityPosts>(session);

            return loader.Load(@"SELECT distinct Elio_community_posts.id, creator_user_id, topic, topic_url, total_votes FROM Elio_community_posts_comments 
                               inner join Elio_community_posts on Elio_community_posts.id=Elio_community_posts_comments.community_post_id 
                               where user_id=@user_id and Elio_community_posts.is_public=1"
                               , DatabaseHelper.CreateIntParameter("user_id", userId));
        }

        public static List<ElioCommunityPostsIJUsers> GetUserUpVotedCommunityPostsDetails(int userId, DBSession session)
        {
            DataLoader<ElioCommunityPostsIJUsers> loader = new DataLoader<ElioCommunityPostsIJUsers>(session);

            return loader.Load(@"Select * from Elio_community_posts 
                               inner join Elio_users on Elio_users.id=Elio_community_posts.creator_user_id 
                               inner join Elio_community_posts_votes on Elio_community_posts_votes.community_post_id=Elio_community_posts.id 
                               where user_id=@user_id and Elio_community_posts.is_public=1 order by total_votes desc"
                               , DatabaseHelper.CreateIntParameter("user_id", userId));
        }

        public static string GetEmailByUserId(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select email as email from Elio_users 
                                                   where id = @id"
                                                 , DatabaseHelper.CreateIntParameter("id", userId));

            return table.Rows.Count > 0 ? table.Rows[0]["email"].ToString() : "";
        }

        public static List<ElioUsers> GetUserFollowedUsers(int userId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load(@"Select * from Elio_users 
                                inner join Elio_community_user_profiles_followers on Elio_community_user_profiles_followers.follower_user_id=Elio_users.id 
                                where user_id=@user_id"
                               , DatabaseHelper.CreateIntParameter("user_id", userId));
        }

        public static List<ElioUsers> GetUserFollowingUsers(int followingUserid, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load(@"Select * from Elio_users 
                               inner join Elio_community_user_profiles_followers on Elio_community_user_profiles_followers.user_id=Elio_users.id 
                               where follower_user_id=@follower_user_id"
                               , DatabaseHelper.CreateIntParameter("follower_user_id", followingUserid));
        }

        public static List<ElioCommunityPostsComments> GetUserPostCommentsGetUserPostCommentsIJPosts(int userId, DBSession session)
        {
            DataLoader<ElioCommunityPostsComments> loader = new DataLoader<ElioCommunityPostsComments>(session);

            return loader.Load("Select * from Elio_community_posts_comments " +
                               "inner join Elio_community_posts on Elio_community_posts.id=Elio_community_posts_comments.community_post_id " +
                               "where user_id=@user_id and Elio_community_posts_comments.is_public=1 and Elio_community_posts.is_public=1 ", DatabaseHelper.CreateIntParameter("user_id", userId));
        }

        public static ElioCommunityPostsIJUsers GetCommunityPostsDetailsById(int postId, DBSession session)
        {
            DataLoader<ElioCommunityPostsIJUsers> loader = new DataLoader<ElioCommunityPostsIJUsers>(session);

            return loader.LoadSingle("Select * from Elio_community_posts " +
                               "inner join Elio_users on Elio_users.id=Elio_community_posts.creator_user_id " +
                               "where Elio_community_posts.is_public=1 and Elio_community_posts.id=@id"
                               , DatabaseHelper.CreateIntParameter("id", postId));
        }

        public static ElioCommunityPosts GetCommunityPostsById(int id, DBSession session)
        {
            DataLoader<ElioCommunityPosts> loader = new DataLoader<ElioCommunityPosts>(session);

            return loader.LoadSingle("Select * from Elio_community_posts where id=@id", DatabaseHelper.CreateIntParameter("id", id));
        }

        public static List<ElioCommunityPosts> GetAllPublicCommunityPosts(DBSession session)
        {
            DataLoader<ElioCommunityPosts> loader = new DataLoader<ElioCommunityPosts>(session);

            return loader.Load("Select id from Elio_community_posts where is_public=1");
        }

        public static List<ElioCommunityEmailNotifications> GetCommunityEmailNotifications(DBSession session)
        {
            DataLoader<ElioCommunityEmailNotifications> loader = new DataLoader<ElioCommunityEmailNotifications>(session);

            return loader.Load("Select * from Elio_community_email_notifications where is_public=1");
        }

        public static List<ElioEmailNotifications> GetElioEmailNotifications(DBSession session)
        {
            DataLoader<ElioEmailNotifications> loader = new DataLoader<ElioEmailNotifications>(session);

            return loader.Load("Select * from Elio_email_notifications where is_public=1");
        }

        public static ElioEmailNotifications GetElioEmailNotificationByDescription(string email, DBSession session)
        {
            DataLoader<ElioEmailNotifications> loader = new DataLoader<ElioEmailNotifications>(session);

            return loader.LoadSingle("Select * from Elio_email_notifications where description = @description"
                                , DatabaseHelper.CreateStringParameter("@description", email));
        }

        public static List<ElioRoles> GetUserRoles(int userId, DBSession session)
        {
            DataLoader<ElioRoles> loader = new DataLoader<ElioRoles>(session);

            return loader.Load("Select role_description from Elio_roles " +
                               "inner join Elio_users_roles on Elio_users_roles.elio_role_id=Elio_roles.id " +
                               "inner join Elio_users on Elio_users.id=Elio_users_roles.user_id " +
                               "where Elio_users.id=@id"
                                                      , DatabaseHelper.CreateIntParameter("@id", userId));
        }

        public static List<ElioRoles> GetAllRoles(DBSession session)
        {
            DataLoader<ElioRoles> loader = new DataLoader<ElioRoles>(session);
            return loader.Load("Select * from Elio_roles");
        }

        public static bool IsUserAdministrator(int userId, DBSession session)
        {
            return IsUserAdministrator(userId, UserRoles.Administrator.ToString(), session);

            //DataTable table = session.GetDataTable(@"Select count(Elio_users.id) as count from Elio_users with (nolock) 
            //                                         inner join Elio_users_roles on Elio_users_roles.user_id=Elio_users.id 
            //                                         inner join Elio_roles on Elio_roles.role_description='Administrator' and Elio_users.id=@id"
            //                                         , DatabaseHelper.CreateIntParameter("@id", userId));

            //            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool IsUserAdministrator(int userId, string roleDescription, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(id) as count from Elio_users_roles with (nolock) 
                                                     where Elio_users_roles.user_id=@user_id 
                                                     and Elio_users_roles.elio_role_id=1"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool HasRoleByDescription(int userId, string roleDescription, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(Elio_users.id) as count from Elio_users with (nolock) 
                                                     inner join Elio_users_roles on Elio_users_roles.user_id=Elio_users.id 
                                                     inner join Elio_roles on Elio_roles.id=Elio_users_roles.elio_role_id 
                                                     where Elio_users.id=@id
                                                     and role_description=@role_description"

                                                   , DatabaseHelper.CreateIntParameter("@id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@role_description", roleDescription));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool HasFollowedPost(int userId, int postId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_community_posts_followers " +
                                                   "where follower_user_id=@follower_user_id and community_post_id=@community_post_id"
                                                   , DatabaseHelper.CreateIntParameter("@follower_user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@community_post_id", postId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool IsFollowingUser(int followerUserId, int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(id) as count from Elio_community_user_profiles_followers 
                                                     where follower_user_id=@follower_user_id and user_id=@user_id"
                                                   , DatabaseHelper.CreateIntParameter("@follower_user_id", followerUserId)
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool AllowCompanyToRate(int visitorId, int reviewCompanyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_user_partner_program_rating where company_id=@company_id and visitor_id=@visitor_id"
                                                    , DatabaseHelper.CreateIntParameter("@company_id", reviewCompanyId)
                                                    , DatabaseHelper.CreateIntParameter("@visitor_id", visitorId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? true : false;
        }

        public static bool SendCommunityNotificationEmailByEmailId(int userId, int communityEmailNotificationsId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_community_user_email_notifications " +
                                                   "where user_id=@user_id and community_email_notifications_id=@community_email_notifications_id"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@community_email_notifications_id", communityEmailNotificationsId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static List<ElioUserPartnerProgramRating> GetCompanyRating(int companyId, DBSession session)
        {
            DataLoader<ElioUserPartnerProgramRating> loader = new DataLoader<ElioUserPartnerProgramRating>(session);

            return loader.Load("Select Rate from Elio_user_partner_program_rating where company_id=@company_id"
                                , DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static decimal GetCompanyAverageRating(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select isnull(sum(Rate),0) as sum, COUNT(id) as count from Elio_user_partner_program_rating where company_id=@company_id"
                                  , DatabaseHelper.CreateIntParameter("@company_id", companyId));

            if (table.Rows[0]["sum"] != null && Convert.ToInt32(table.Rows[0]["sum"]) > 0 && Convert.ToInt32(table.Rows[0]["count"]) > 0)
            {
                return Convert.ToDecimal(table.Rows[0]["sum"]) / Convert.ToInt32(table.Rows[0]["count"]);
            }
            else
            {
                return 0;
            }
        }

        public static int GetCompanyTotalRatings(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select sum(Rate) as sum from Elio_user_partner_program_rating where company_id=@company_id"
                                                   , DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return (!string.IsNullOrEmpty(table.Rows[0]["sum"].ToString())) ? Convert.ToInt32(table.Rows[0]["sum"]) : 0;
        }

        public static int GetCompanyCountRatings(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_user_partner_program_rating where company_id=@company_id"
                                  , DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return (!string.IsNullOrEmpty(table.Rows[0]["count"].ToString())) ? Convert.ToInt32(table.Rows[0]["count"]) : 0;
        }

        public static ElioUserPartnerProgramRating GetCompanySumRating(int companyId, DBSession session)
        {
            DataLoader<ElioUserPartnerProgramRating> loader = new DataLoader<ElioUserPartnerProgramRating>(session);

            return loader.LoadSingle("Select sum(Rate) from Elio_user_partner_program_rating where company_id=@company_id"
                                     , DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static List<ElioUsersNotificationEmails> UserNotificationEmailCount(int userId, DBSession session)
        {
            DataLoader<ElioUsersNotificationEmails> loader = new DataLoader<ElioUsersNotificationEmails>(session);

            return loader.Load("select notification_email_date from Elio_users_notification_emails where user_id=@user_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void InsertUserLoginStatistics(int userId, DBSession session)
        {
            DataLoader<ElioUsersLoginStatistics> loader = new DataLoader<ElioUsersLoginStatistics>(session);

            ElioUsersLoginStatistics userStatistics = new ElioUsersLoginStatistics();

            userStatistics.UserId = userId;
            userStatistics.Ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
            userStatistics.Datein = DateTime.Now;
            userStatistics.SessionId = HttpContext.Current.Session.SessionID;

            loader.Insert(userStatistics);
        }

        public static void InsertUserNotificationEmailsStatistics(int userId, int adminUserId, DBSession session)
        {
            DataLoader<ElioUsersNotificationEmails> loader = new DataLoader<ElioUsersNotificationEmails>(session);

            ElioUsersNotificationEmails userEmailStatistics = new ElioUsersNotificationEmails();

            userEmailStatistics.UserId = userId;
            userEmailStatistics.NotificationEmailDate = DateTime.Now;
            userEmailStatistics.SendByUser = adminUserId;

            loader.Insert(userEmailStatistics);
        }

        public static void DeleteUserApi(int userId, int apiId, DBSession session)
        {
            session.ExecuteQuery("Delete from Elio_users_apies where user_id=@user_id and api_id=@api_id", DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                                                            , DatabaseHelper.CreateIntParameter("@api_id", apiId));

        }

        public static void DeleteUserIndustry(int userId, int industryId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_users_industries where user_id=@user_id and industry_id=@industry_id", DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                                                            , DatabaseHelper.CreateIntParameter("@industry_id", industryId));

        }

        public static void DeleteUserSubIndustryGroupItem(int userId, int subIndustryGroupItemId, DBSession session)
        {
            session.GetDataTable(@"Delete from Elio_users_sub_industries_group_items 
                                 where user_id = @user_id 
                                 and sub_industry_group_item_id = @sub_industry_group_item_id"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateIntParameter("@sub_industry_group_item_id", subIndustryGroupItemId));

        }

        public static void DeleteUserSubIndustryGroupItemsAll(int userId, DBSession session)
        {
            session.GetDataTable(@"Delete from Elio_users_sub_industries_group_items 
                                 where user_id = @user_id"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void DeleteUserPartnerProgramByPartnerID(int userId, int partnerId, DBSession session)
        {
            session.GetDataTable(@"Delete from Elio_users_partners
                                    where user_id = @user_id
                                    and partner_id = @partner_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@partner_id", partnerId));
        }

        public static void DeleteUserMarket(int userId, int marketId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_users_markets where user_id=@user_id and market_id=@market_id", DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                                                            , DatabaseHelper.CreateIntParameter("@market_id", marketId));

        }

        public static bool ExistUserFavoriteCompanyByIsDeletedStatus(int userId, int companyId, int isDeleted, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_favorites " +
                                                   "where user_id=@user_id and company_id=@company_id"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@company_id", companyId)
                                                   , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistCompanyViewCompany(int companyViewsCompanyId, int interestedCompanyId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_companies_views_companies where company_views_company_id=@company_views_company_id and interested_company_id=@interested_company_id"
                                                                                                                                        , DatabaseHelper.CreateIntParameter("@company_views_company_id", companyViewsCompanyId)
                                                                                                                                        , DatabaseHelper.CreateIntParameter("@interested_company_id", interestedCompanyId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static int GetCompanyTotalViews11(int companyId, DBSession session)
        {
            int totalViews = 0;
            DataLoader<ElioCompaniesViews> loader = new DataLoader<ElioCompaniesViews>(session);
            List<ElioCompaniesViews> views = loader.Load("Select sum(views) as totalViews from Elio_companies_views where company_id=@company_id", DatabaseHelper.CreateIntParameter("@company_id", companyId));
            foreach (ElioCompaniesViews view in views)
            {
                totalViews += view.Views;
            }

            return totalViews;
        }

        public static int GetCompanyTotalViews(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select sum(views) as totalViews from Elio_companies_views where company_id=@company_id", DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return (!string.IsNullOrEmpty(table.Rows[0]["totalViews"].ToString())) ? Convert.ToInt32(table.Rows[0]["totalViews"]) : 0;
        }

        public static int GetTotalRegisteredCompaniesByType(string companyType, int userApplicationType, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as totalCompanies from Elio_users with (nolock) where company_type=@company_type and user_application_type=@user_application_type"
                                                   , DatabaseHelper.CreateStringParameter("@company_type", companyType)
                                                   , DatabaseHelper.CreateIntParameter("@user_application_type", userApplicationType));

            return (!string.IsNullOrEmpty(table.Rows[0]["totalCompanies"].ToString())) ? Convert.ToInt32(table.Rows[0]["totalCompanies"]) : 0;
        }

        public static int GetTotalCompaniesByAccountStatus(int accountStatus, int userApplicationType, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as totalCompanies from Elio_users with (nolock) where account_status=@account_status and user_application_type=@user_application_type"
                                                   , DatabaseHelper.CreateIntParameter("@account_status", accountStatus)
                                                   , DatabaseHelper.CreateIntParameter("@user_application_type", userApplicationType));

            return (!string.IsNullOrEmpty(table.Rows[0]["totalCompanies"].ToString())) ? Convert.ToInt32(table.Rows[0]["totalCompanies"]) : 0;
        }

        public static int GetCompanyTotalReviews(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as totalReviews from Elio_user_program_review where company_id=@company_id and is_public=1", DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return (!string.IsNullOrEmpty(table.Rows[0]["totalReviews"].ToString())) ? Convert.ToInt32(table.Rows[0]["totalReviews"]) : 0;
        }

        public static bool AllowUserToVote(int visitorId, int reviewId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select SUM(votes) as votes from Elio_user_program_review_votes where Elio_user_program_review_visitor_id=@Elio_user_program_review_visitor_id and Elio_user_program_review_id=@Elio_user_program_review_id"
                                                    , DatabaseHelper.CreateIntParameter("@Elio_user_program_review_visitor_id", visitorId)
                                                    , DatabaseHelper.CreateIntParameter("@Elio_user_program_review_id", reviewId));

            return (!string.IsNullOrEmpty(table.Rows[0]["votes"].ToString())) ? Convert.ToInt32(table.Rows[0]["votes"]) == 0 ? true : false : true;
        }

        public static int GetReviewTotalVotes(int reviewId, DBSession session)
        {
            DataTable table = session.GetDataTable("SELECT SUM(votes) as votes " +
                                                   "FROM Elio_user_program_review_votes " +
                                                   "where Elio_user_program_review_id=@Elio_user_program_review_id"
                                                    , DatabaseHelper.CreateIntParameter("@Elio_user_program_review_id", reviewId));

            return Convert.ToInt32(table.Rows[0]["votes"]);
        }

        public static int GetCompanyTotalRating(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("SELECT SUM(votes) as totalVotes " +
                                                   "FROM Elio_user_program_review " +
                                                   "inner join Elio_user_program_review_votes on Elio_user_program_review_votes.Elio_user_program_review_id=Elio_user_program_review.id " +
                                                   "where company_id=@company_id"
                                                                        , DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return (!string.IsNullOrEmpty(table.Rows[0]["totalVotes"].ToString())) ? Convert.ToInt32(table.Rows[0]["totalVotes"]) : 0;
        }

        public static int GetCompanyMessages(int companyId, int isNewMessage, int isDeleted, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_users_messages where is_new=@is_new and deleted=@deleted and receiver_user_id=@receiver_user_id "
                                                    , DatabaseHelper.CreateIntParameter("@is_new", isNewMessage)
                                                    , DatabaseHelper.CreateIntParameter("@deleted", isDeleted)
                                                    , DatabaseHelper.CreateIntParameter("@receiver_user_id", companyId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetCompanyRecentLeads(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_companies_views_companies where company_views_company_id=@company_views_company_id and is_new=1"
                                                   , DatabaseHelper.CreateIntParameter("@company_views_company_id", companyId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetCompanySavedProfiles(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_users_favorites where company_id=@company_id ", DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static List<ElioUsersIJIndustries> GetAllUsersByCompanyType(string type, DBSession session)
        {
            DataLoader<ElioUsersIJIndustries> loader = new DataLoader<ElioUsersIJIndustries>(session);
            return loader.Load("Select elio_users.id, company_name, overview,country, company_logo, views,industry_description from elio_users with (nolock) " +
                                "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id " +
                                "inner join Elio_industries on Elio_industries.id=Elio_users_industries.industry_id " +
                                "where company_type=@company_type", DatabaseHelper.CreateStringParameter("@company_type", type));
        }

        public static List<ElioUsers> GetAllCompletedUsers(ElioUsers user, DBSession session)
        {
            string topResults = GlobalDBMethods.ReturnSearchTopResultsQueryString(user, true, session);

            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select " + topResults + " * from Elio_users with (nolock) " +
                               "left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id=Elio_users.id " +
                               "where Elio_users.id<>@id and account_status=1 and is_public=1 ORDER BY rate desc", DatabaseHelper.CreateIntParameter("id", user.Id));
        }

        public static List<ElioUsers> GetAllFullRegisteredUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select * from Elio_users with (nolock) where account_status<>0 order by company_name asc");
        }

        public static List<ElioUsers> GetAllVendorsUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select * from Elio_users with (nolock) where account_status<>0 and company_type = 'Vendors' and user_application_type <> 2 order by company_name asc");
        }

        public static List<ElioUsers> GetAllVendorsUsersDDL(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select id, company_name from Elio_users with (nolock) where account_status<>0 and company_type = 'Vendors' and user_application_type <> 2 order by company_name asc");
        }

        public static List<ElioUsers> GetAllResellersUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select * from Elio_users with (nolock) where account_status<>0 and company_type = 'Channel Partners' and user_application_type <> 2 order by company_name asc");
        }

        public static List<ElioUsers> GetAllThirdPartyUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select * from Elio_users with (nolock) where account_status<>0 and company_type = 'Channel Partners' and user_application_type = 2 order by company_name asc");
        }

        public static List<ElioUsers> GetFullRegisteredUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select * from Elio_users with (nolock) where account_status=1");
        }

        public static List<ElioUsers> GetFullRegisteredPublicUsers(int id,DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"Select id,company_name,company_type from Elio_users with (nolock) 
                                    where account_status = 1 
                                    and is_public = 1
                                    and isnull(company_name, '') != ''
                                    and id > @id
                                    order by id"
                              , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioUsers> GetAllUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select * from Elio_users with (nolock)");
        }

        public static List<ElioUsers> GetAllUsersId(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select id, last_login,user_login_count,sysdate from Elio_users with (nolock) where user_application_type = 1 order by id");
        }

        public static List<ElioUsers> GetAllUsersByApplicationType(int applicationType, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select * from Elio_users with (nolock) " +
                               "where user_application_type=@user_application_type " +
                               "order by id"
                               , DatabaseHelper.CreateIntParameter("@user_application_type", applicationType));
        }

        public static List<ElioUsers> GetAllUsersByApplicationTypeAndStatus(int applicationType, int isPublic, int accountStatus, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"Select * from Elio_users with (nolock)
                                   where 1 = 1
                                    and user_application_type = @user_application_type 
                                    and is_public = @is_public 
                                    and account_status = @account_status
                                   order by id"
                               , DatabaseHelper.CreateIntParameter("@user_application_type", applicationType)
                               , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                               , DatabaseHelper.CreateIntParameter("@account_status", accountStatus));
        }

        public static List<ElioUsers> GetTopUsersByApplicationTypeAndStatus(int applicationType, int topAmount, int isPublic, int accountStatus, DBSession session)
        {
            string query = @"Select TOP " + topAmount + " * from Elio_users u with (nolock) " +
                            "where 1 = 1 " +
                            "and u.id not in " +
                            "( " +
                            "    Select up.user_id " +
                            "    from Elio_users_person up " +
                            "    inner join Elio_users_person_companies upc " +
                            "        ON up.user_id = upc.user_id " +
                            "            and up.id = upc.elio_person_id " +
                            "    where 1 = 1 " +
                            "    and up.is_public = 1 " +
                            "    and up.is_active = 1 " +
                            ") " +
                            "and u.user_application_type = @user_application_type " +
                            "and u.is_public = @is_public " +
                            "and u.account_status = @account_status " +
                            "order by u.id desc";

            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(query
                               , DatabaseHelper.CreateIntParameter("@user_application_type", applicationType)
                               , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                               , DatabaseHelper.CreateIntParameter("@account_status", accountStatus));
        }

        public static List<ElioUsersThirdParty> GetAll3pUsersId(DBSession session)
        {
            DataLoader<ElioUsersThirdParty> loader = new DataLoader<ElioUsersThirdParty>(session);
            return loader.Load("Select * from Elio_users_third_party with (nolock) order by id");
        }

        public static List<ElioUsers> GetFeatueredUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select id, company_logo, country from Elio_users with (nolock) " +
                               "where features_no>0 order by features_no desc");
        }
        public static List<ElioUsersIJIndustries> GetFeatuerUsers(DBSession session)
        {
            DataLoader<ElioUsersIJIndustries> loader = new DataLoader<ElioUsersIJIndustries>(session);
            return loader.Load("Select top 5 Elio_users.id, company_logo, country, industry_description, company_name from Elio_users with (nolock) " +
                               "left join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id " +
                               "left join Elio_industries on Elio_industries.id=Elio_users_industries.industry_id" +
                               " where features_no>0 order by features_no desc");
        }

        public static List<ElioUsers> GetFeaturedUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select id, company_logo, country, company_name, company_type from Elio_users with (nolock) " +
                               " where features_no>0 order by features_no desc");
        }

        public static List<string> GetIndustiesDescriptionsIJUserIndustries(int userId, DBSession session)
        {
            List<string> vendorIndustries = new List<string>();

            DataLoader<ElioIndustriesIJIndustries> loader = new DataLoader<ElioIndustriesIJIndustries>(session);
            List<ElioIndustriesIJIndustries> industries = loader.Load(@"Select industry_description from Elio_industries 
                                                                         inner join Elio_users_industries on Elio_users_industries.industry_id=Elio_industries.id 
                                                                         where user_id = @user_id"
                                                                    , DatabaseHelper.CreateIntParameter("user_id", userId));

            foreach (ElioIndustriesIJIndustries industry in industries)
            {
                vendorIndustries.Add(industry.IndustryDescription);
            }

            return vendorIndustries;
        }

        public static string GetIndustiesDescriptionsIJUserIndustriesAsString(int userId, DBSession session)
        {
            DataTable table = new DataTable();
            table = session.GetDataTable(@"Select industry_description + ', ' 
                                            from Elio_industries 
                                            inner join Elio_users_industries on Elio_users_industries.industry_id=Elio_industries.id 
                                            where user_id = @user_id
                                            for xml path('')"
                                    , DatabaseHelper.CreateIntParameter("user_id", userId));

            return table.Rows.Count > 0 ? (table.Rows[0][0].ToString().Length >= 2) ? table.Rows[0][0].ToString().Substring(0, table.Rows[0][0].ToString().Length - 2) : table.Rows[0][0].ToString() : "";
        }

        public static string GetVerticalsDescriptionsIJUserVerticalsAsString(int userId, DBSession session)
        {
            DataTable table = new DataTable();
            table = session.GetDataTable(@"Select description + ',' 
                                            from Elio_sub_industries_group_items sigi
                                            inner join Elio_users_sub_industries_group_items usigi on usigi.sub_industry_group_item_id = sigi.id 
                                            where user_id = @user_id
                                            for xml path('')"
                                    , DatabaseHelper.CreateIntParameter("user_id", userId));

            return table.Rows.Count > 0 ? (table.Rows[0][0].ToString().Length > 1) ? table.Rows[0][0].ToString().Substring(0, table.Rows[0][0].ToString().Length - 1) : table.Rows[0][0].ToString() : "";
        }

        public static string GetProductsDescriptionsIJUserProductsAsString(int userId, DBSession session)
        {
            DataTable table = new DataTable();
            table = session.GetDataTable(@"Select description + ',' 
                                            from Elio_registration_products rp
                                            inner join Elio_users_registration_products urp on urp.reg_products_id = rp.id 
                                            where user_id = @user_id
                                            for xml path('')"
                                    , DatabaseHelper.CreateIntParameter("user_id", userId));

            return table.Rows.Count > 0 ? (table.Rows[0][0].ToString().Length > 1) ? table.Rows[0][0].ToString().Substring(0, table.Rows[0][0].ToString().Length - 1) : table.Rows[0][0].ToString() : "";
        }

        public static List<string> GetApiesDescriptionsIJUserApies(int userId, DBSession session)
        {
            List<string> userApies = new List<string>();

            DataLoader<ElioApiesIJApies> loader = new DataLoader<ElioApiesIJApies>(session);
            List<ElioApiesIJApies> apies = loader.Load("Select api_description from Elio_apies " +
                                     "inner join Elio_users_apies on Elio_users_apies.api_id=Elio_apies.id " +
                                     "where user_id=@user_id", DatabaseHelper.CreateIntParameter("user_id", userId));

            foreach (ElioApiesIJApies api in apies)
            {
                userApies.Add(api.ApiDescription);
            }

            return userApies;
        }

        public static ElioUsersIJIndustriesIJApies GetUsersIJIndustriesIJApiesIJViewsByCompanyId(int id, DBSession session)
        {
            DataLoader<ElioUsersIJIndustriesIJApies> loader = new DataLoader<ElioUsersIJIndustriesIJApies>(session);
            return loader.LoadSingle("Select top 1 elio_users.id, company_name, overview,country, company_logo, views,industry_description, api_description from elio_users with (nolock) " +
                                "left join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id " +
                                "left join Elio_industries on Elio_industries.id=Elio_users_industries.industry_id " +
                                "left join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id " +
                                "left join Elio_apies on Elio_apies.id=Elio_users_apies.api_id " +
                                "left join Elio_companies_views on Elio_companies_views.company_id=elio_users.id " +
                                "where elio_users.id=@id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsersIJApies GetUsersIJApiesIJViewsByCompanyId(int id, DBSession session)
        {
            DataLoader<ElioUsersIJApies> loader = new DataLoader<ElioUsersIJApies>(session);
            return loader.LoadSingle("Select top 1 elio_users.id, company_name, overview,country, company_logo, views,api_description from elio_users with (nolock) " +
                                "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id " +
                                "inner join Elio_apies on Elio_apies.id=Elio_users_apies.api_id " +
                                "left join Elio_companies_views on Elio_companies_views.company_id=elio_users.id " +
                                "where elio_users.id=@id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioUsers> GetUsersByIds(string ids, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"Select distinct
                                    id, 
                                    company_name, 
                                    user_application_type, 
                                    country, 
                                    email, 
                                    website
                                from elio_users with (nolock) where id IN (" + ids + ")");
        }

        public static List<ElioUsers> GetUsersByCompaniesName(string companiesName, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"Select distinct
                                    id, 
                                    company_name, 
                                    user_application_type, 
                                    country, 
                                    email, 
                                    website
                                from elio_users with (nolock) where company_name IN (" + companiesName + ") and isnull(company_name, '') != ''");
        }

        public static List<ElioUsers> GetUserPartnersIdsGuidsByCompaniesNamesByCompanyType(int userId, string companyType, string companiesName, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            if (companyType == Types.Vendors.ToString())
            {
                return loader.Load(@"Select 
                                    distinct u.id, u.company_name, u.guid
                                from elio_users u
                                inner join Elio_collaboration_vendors_resellers cvr
	                                on u.id = cvr.partner_user_id
                                where company_name IN (" + companiesName + ") " +
                                    "and account_status = 1 " +
                                    "and company_type = 'Channel Partners' " +
                                    "and cvr.invitation_status = 'Confirmed' " +
                                    "and cvr.is_active = 1 " +
                                    "and cvr.master_user_id = @master_user_id"
                                , DatabaseHelper.CreateIntParameter("@master_user_id", userId));
            }
            else
            {
                return loader.Load(@"Select 
                                    distinct u.id, u.company_name, u.guid
                                from elio_users u
                                inner join Elio_collaboration_vendors_resellers cvr
	                                on u.id = cvr.master_user_id
                                where company_name IN (" + companiesName + ") " +
                                    "and account_status = 1 " +
                                    "and company_type = 'Vendors' " +
                                    "and cvr.invitation_status = 'Confirmed' " +
                                    "and cvr.is_active = 1 " +
                                    "and cvr.partner_user_id = @partner_user_id"
                                , DatabaseHelper.CreateIntParameter("@partner_user_id", userId));
            }
        }

        public static List<string> GetUserPartnersEmailsByCompaniesNamesByCompanyType(int userId, string companyType, string companiesName, DBSession session)
        {
            List<string> partnersEmails = new List<string>();
            List<ElioUsers> users = null;

            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            if (companyType == Types.Vendors.ToString())
            {
                users = loader.Load(@"Select 
                                        distinct u.email
                                    from elio_users u
                                    inner join Elio_collaboration_vendors_resellers cvr
	                                    on u.id = cvr.partner_user_id
                                    where company_name IN (" + companiesName + ") " +
                                    "and account_status = 1 " +
                                    "and company_type = 'Channel Partners' " +
                                    "and cvr.invitation_status = 'Confirmed' " +
                                    "and cvr.is_active = 1 " +
                                    "and cvr.master_user_id = @master_user_id"
                                , DatabaseHelper.CreateIntParameter("@master_user_id", userId));
            }
            else
            {
                users = loader.Load(@"Select 
                                        distinct u.email
                                    from elio_users u
                                    inner join Elio_collaboration_vendors_resellers cvr
	                                    on u.id = cvr.master_user_id
                                    where company_name IN (" + companiesName + ") " +
                                    "and account_status = 1 " +
                                    "and company_type = 'Vendors' " +
                                    "and cvr.invitation_status = 'Confirmed' " +
                                    "and cvr.is_active = 1 " +
                                    "and cvr.partner_user_id = @partner_user_id"
                                , DatabaseHelper.CreateIntParameter("@partner_user_id", userId));
            }

            foreach (ElioUsers user in users)
            {
                if (user.Email != "")
                    partnersEmails.Add(user.Email);
            }

            return partnersEmails;
        }

        public static List<ElioCollaborationVendorsResellers> GetElioCollaborationVendorsResellersIdsByCompaniesNames(int vendorId, string companiesName, DBSession session)
        {
            DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);
            return loader.Load(@"Select 
                                    cvr.id,cvr.partner_user_id
                                from Elio_collaboration_vendors_resellers cvr
                                inner join elio_users u
	                                on u.id = cvr.partner_user_id
                                where company_name IN (" + companiesName + ") and account_status = 1 and company_type = 'Channel Partners' and master_user_id = @master_user_id and cvr.invitation_status = 'Confirmed'"
                        , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId));
        }

        public static int GetCollaborationIDByVendorResellerId(int vendorId, int resellerId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select
                                                        cvr.id,cvr.partner_user_id
                                                    from Elio_collaboration_vendors_resellers cvr
                                                    where master_user_id = @master_user_id and partner_user_id = @partner_user_id"
                                            , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                            , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["id"]) : 0;
        }

        public static List<ElioUsers> GetUsersByCompaniesEmail(string companiesEmail, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"Select distinct
                                    id, 
                                    company_name, 
                                    user_application_type, 
                                    country, 
                                    email, 
                                    website from elio_users with (nolock) where email IN (" + companiesEmail + ") and isnull(email, '') != ''");
        }

        public static ElioUsersDeleted GetUserDeletedById(int userId, DBSession session)
        {
            DataLoader<ElioUsersDeleted> loader = new DataLoader<ElioUsersDeleted>(session);
            return loader.LoadSingle(@"Select * from elio_users_deleted with (nolock) where user_id = @user_id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsers GetUserByDealId(int dealId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle(@"select u.* 
                                        from elio_users u
                                        inner join Elio_registration_deals d
                                            on u.id = d.reseller_id
                                        where d.id = @dealId", DatabaseHelper.CreateIntParameter("@dealId", dealId));
        }

        public static DataTable GetDealResellerEmailAndDealCompanyNameByDealId(int dealId, string companyType, DBSession session)
        {
            string query = @"select u.email,d.company_name
                            from elio_users u
                            inner join Elio_registration_deals d ";


            query += (companyType == Types.Vendors.ToString()) ? " on u.id = d.reseller_id " : " on u.id = d.vendor_id ";

            query += " where d.id = @dealId";

            DataTable table = session.GetDataTable(query
                                , DatabaseHelper.CreateIntParameter("@dealId", dealId));

            return (table != null && table.Rows.Count > 0) ? table : null;
        }

        public static ElioUsers GetUserById(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select * from elio_users with (nolock) where id = @id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetCollaborationPartnerUserById(int partnerId, int vendorId, string companyType, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            string strQuery = @"Select u.* 
                                from elio_users u
                                inner join Elio_collaboration_vendors_resellers cvr ";

            strQuery += (companyType == Types.Vendors.ToString()) ? " on cvr.partner_user_id = u.id " : " on cvr.master_user_id = u.id ";

            strQuery += @" where cvr.is_active = 1 AND invitation_status = 'Confirmed' ";

            strQuery += (companyType == Types.Vendors.ToString()) ? " and u.id = @partner_id and cvr.master_user_id = @vendor_id " : " and u.id = @vendor_id and cvr.partner_user_id = @partner_id ";

            return loader.LoadSingle(strQuery
                                , DatabaseHelper.CreateIntParameter("@partner_id", partnerId)
                                , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));
        }

        public static bool IsActiveCollaborationUser(int partnerId, int vendorId, DBSession session)
        {
            string strQuery = @"Select count(id) as count
                                from Elio_collaboration_vendors_resellers 
                                where is_active = 1 AND invitation_status = 'Confirmed' 
                                and partner_user_id = @partner_id and master_user_id = @vendor_id ";

            DataTable table = session.GetDataTable(strQuery
                                , DatabaseHelper.CreateIntParameter("@partner_id", partnerId)
                                , DatabaseHelper.CreateIntParameter("@vendor_id", vendorId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static ElioUsers GetUserByCustomerId(string customerId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select * from elio_users with (nolock) where customer_id = @customer_id", DatabaseHelper.CreateStringParameter("@customer_id", customerId));
        }

        public static DataTable GetPersonByUserIdAsDataTable(int userId, DBSession session)
        {
            return session.GetDataTable(@"Select given_name as 'FIRST NAME', family_name AS 'LAST NAME', location AS 'PERSON LOCATION', bio AS 'PERSON BIO', title AS 'PERSON TITLE', role AS 'PERSON ROLE', seniority AS 'SENIORITY' 
                                            from Elio_users_person with (nolock) where user_id = @user_id"
                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetPersonCompanyByUserIdAsDataTable(int userId, DBSession session)
        {
            return session.GetDataTable(@"Select domain as 'COMPANY DOMAIN', sector AS 'SECTOR', industry AS 'COMPANY INDUSTRY', sub_industry AS 'COMPANY SUB INDUSTRIES', description AS 'COMPANY DESCRIPTION', founded_year AS 'COMPANY FOUNDED YEAR', fund_amount AS 'COMPANY FUND AMOUNT', employees_number AS 'COMPANY EMPLOYEES NUMBER', employees_range AS 'COMPANY EMPLOYEES RANGE', annual_revenue AS 'ESTIMATED REVENUE', annual_revenue_range AS 'ESTIMATED REVENUE RANGE'
                                            from Elio_users_person_companies with (nolock) where user_id = @user_id"
                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetUserByIdAsDataTable(int id, DBSession session)
        {
            return session.GetDataTable(@"SELECT
	                                       [company_name] AS 'COMPANY NAME'
                                          ,[sysdate] as 'DATE INSERTED'     
                                          ,[phone] AS 'COMPANY PHONE'
                                          ,[address] AS 'COMPANY ADDRESS'
                                          ,[country] AS 'COUNTRY'
                                          ,[website] AS 'WEBSITE'
                                          ,[email] AS 'EMAIL'
                                          ,CASE WHEN [account_status] = 1 THEN 'Full Registered' ELSE 'Not Full Registered' END AS 'ACCOUNT REGISTRATION STATUS'
                                          ,[overview] AS 'COMPANY OVERVIEW'
                                          ,[description] AS 'COMPANY DESCRIPTION'      
                                          ,[company_type] AS 'COMPANY TYPE'
                                          ,[official_email] AS 'OFFICIAL EMAIL'
                                          ,CASE WHEN [is_public] = 1 THEN 'Public Profile' ELSE 'Not Public Profile' END AS 'COMPANY PROFILE'
                                          ,[last_name] AS 'PERSONAL LAST NAME'
                                          ,[first_name] AS 'PERSONAL FIRST NAME'
                                          ,CASE WHEN [community_status] = 1 THEN 'Community Completed Profile' ELSE 'Community Not Completed Profile' END AS 'COMMUNITY PROFILE STATUS' 
                                          ,[position] AS 'PERSON POSITION'
                                          ,[community_summary_text] AS 'COMMUNITY DESCRIPTION'
                                          ,[linkedin_url] AS 'LINKEDIN URL'
                                          ,[twitter_url] AS 'TWITTER URL'
                                          ,CASE WHEN [billing_type] = 1 THEN 'Freemium Packet user' ELSE
                                          CASE WHEN [billing_type] = 2 THEN 'Premium Packet user' ELSE
                                          CASE WHEN [billing_type] = 3 THEN 'Startup Packet user' ELSE
                                          CASE WHEN [billing_type] = 4 THEN 'Growth Packet user' ELSE 'Enterprise Packet user' END END END END AS 'BILLING ACCOUNT TYPE'
                                          ,[vendor_product_demo_link] AS 'Vendor Product Demo Link'
                                      FROM elio_users
                                      where id = @id"
                                        , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static DataTable GetUsersPartnersAsDataTable(int userId, DBSession session)
        {
            return session.GetDataTable(@"Select partner_description as 'PRTNER PROGRAMM DESCRIPTION' from elio_partners inner join elio_users_partners 
                                            on elio_users_partners.partner_id=elio_partners.id
                                            where user_id=@user_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetUsersApiesAsDataTable(int userId, DBSession session)
        {
            return session.GetDataTable(@"Select api_description as 'API DESCRIPTION' from elio_apies inner join elio_users_apies on elio_users_apies.api_id=elio_apies.id " +
                               "where user_id=@user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetUsersIndustriesAsDataTable(int userId, DBSession session)
        {
            return session.GetDataTable(@"Select industry_description as 'INDUSTRY DESCRIPTION' from elio_industries 
                                         inner join elio_users_industries 
                                             on elio_users_industries.industry_id=elio_industries.id 
                                         where user_id=@user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetUsersMarketsAsDataTable(int userId, DBSession session)
        {
            return session.GetDataTable(@"Select market_description as 'MARKET DESCRIPTION' from elio_markets 
                                         inner join elio_users_markets 
                                            on elio_users_markets.market_id=elio_markets.id 
                                         where user_id=@user_id"
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioSubcategoriesIJSubcategoriesGroups> GetUserSubcategoriesAndGroupsAsDataTable(int userId, DBSession session)
        {
            DataLoader<ElioSubcategoriesIJSubcategoriesGroups> loader = new DataLoader<ElioSubcategoriesIJSubcategoriesGroups>(session);
            return loader.Load("select user_id, description, group_description from Elio_users_sub_industries_group_items inner join " +
                                "Elio_sub_industries_group_items on Elio_users_sub_industries_group_items.sub_industry_group_item_id = Elio_sub_industries_group_items.id " +
                                "inner join Elio_sub_industries_group on Elio_sub_industries_group_items.sub_industies_group_id = Elio_sub_industries_group.id " +
                                "where user_id=@user_id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static bool IsUserDeleted(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from elio_users_deleted with (nolock) where user_id = @user_id"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static ElioUsers GetNotPublicUserById(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select * from elio_users with (nolock) where id=@id and is_public = 0", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsersIJCountries GetUserIJCountryById(int id, DBSession session)
        {
            DataLoader<ElioUsersIJCountries> loader = new DataLoader<ElioUsersIJCountries>(session);
            return loader.LoadSingle(@"Select elio_users.*, Elio_countries.id AS country_id, country_name, capital, prefix, iso2, iso3, region
                                       from elio_users with (nolock) 
                                       Inner Join Elio_countries
                                            On Elio_countries.country_name = elio_users.country
                                       where elio_users.id = @id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetUserByGuId(string guId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select * from elio_users with (nolock) where guid = @guid", DatabaseHelper.CreateStringParameter("@guid", guId));
        }

        public static ElioUsers GetCompanynameAndIdAndEmailAndOfficialEmailByName(string companyName, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select TOP 1 id, company_name, email, official_email from elio_users with (nolock) where company_name=@company_name", DatabaseHelper.CreateStringParameter("@company_name", companyName));
        }

        public static ElioUsers GetCompanyEmailAndCompanyName(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select email, company_name from elio_users with (nolock) where id=@id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetCompanyEmailAndOfficialEmail(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select email, official_email from elio_users with (nolock) where id=@id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetCompanynameAndIdAndEmailAndOfficialEmailById(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select id, company_name, email, official_email from elio_users with (nolock) where id=@id", DatabaseHelper.CreateIntParameter("@id", id));
        }


        public static string GetCompanyEmail(int id, DBSession session)
        {
            DataTable table = session.GetDataTable("Select email from elio_users with (nolock) where id=@id", DatabaseHelper.CreateIntParameter("@id", id));

            return table.Rows[0]["email"].ToString();
        }

        public static ElioUsers GetUserEmails(int userId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select email, official_email from elio_users with (nolock) where id=@id", DatabaseHelper.CreateIntParameter("@id", userId));
        }

        public static List<ElioUserEmails> GetUserMoreEmails(int userId, DBSession session)
        {
            DataLoader<ElioUserEmails> loader = new DataLoader<ElioUserEmails>(session);
            return loader.Load("Select * from elio_user_emails with (nolock) where user_id=@user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUserEmails GetUserMoreEmailById(int id, DBSession session)
        {
            DataLoader<ElioUserEmails> loader = new DataLoader<ElioUserEmails>(session);
            return loader.LoadSingle("Select * from elio_user_emails with (nolock) where id=@id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetUserWebSite(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select website from elio_users with (nolock) where id=@id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetUserByEmail(string email, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select * from elio_users with (nolock) where email = @email", DatabaseHelper.CreateStringParameter("@email", email));
        }

        public static ElioUsers GetUserByEmailCustomTop1(string email, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select top 1 * from elio_users with (nolock) where email = @email", DatabaseHelper.CreateStringParameter("@email", email));
        }

        public static ElioUsers GetUserByEmailTop1(string email, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select top 1 email, id from elio_users with (nolock) where email = @email", DatabaseHelper.CreateStringParameter("@email", email));
        }

        public static ElioUsers GetUserByCompanyName(string companyName, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select * from elio_users with (nolock) where company_name = @company_name", DatabaseHelper.CreateStringParameter("@company_name", companyName));
        }

        public static ElioUsers GetVendorByCompanyNameLike(string companyName, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select TOP 1 id, company_name, company_logo " +
                "from elio_users with (nolock) " +
                "where company_name like '" + companyName + "%'" +
                "and company_type = 'Vendors'");
        }

        public static ElioUsers GetUserByCompanyNameRegexForURLSearch(string companyName, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle(@"Select 
                                        LOWER(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(company_name,'@[^A-Za-z0-9]+','-'),',','.'),' ','-'),'.',''),' ',''),'''',''),'@',''),'""',''),'/','-'),'\','-'),'---','-'),'--','-')),*
                                        from elio_users with (nolock) where company_name = @company_name"
                                    , DatabaseHelper.CreateStringParameter("@company_name", companyName));
        }

        public static List<ElioUsers> GetUsersByCompanyType(string companyType, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"Select 
                                    id, 
                                    company_name, 
                                    user_application_type, 
                                    country, 
                                    email, 
                                    website
                                 from elio_users with (nolock) 
                                 where company_type=@company_type 
                                 and ((is_public = 1 and account_status=1) 
                                 or (is_public=0 and account_status=0 and user_application_type=2))
                                 order by id asc"
                                 , DatabaseHelper.CreateStringParameter("@company_type", companyType));
        }

        public static List<ElioUsers> GetUsersExceptUserActiveConnectionsByCompanyType(int userId, string companyType, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"Select * from elio_users with (nolock) 
                                 where company_type=@company_type 
                                 and ((is_public = 1 and account_status=1) 
                                 or (is_public=0 and account_status=0 and user_application_type=2))
                                 and id not in
                                     (
	                                     select connection_id 
	                                     from Elio_users_connections
	                                     where user_id = @user_id and status = 1 and can_be_viewed = 1
                                     )
                                 order by id asc"
                                 , DatabaseHelper.CreateStringParameter("@company_type", companyType)
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsers> GetVendorsConnectionsByUserId(int userId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"SELECT * FROM elio_users
                                 inner join Elio_users_connections
                                    on elio_users.id = Elio_users_connections.connection_id
                                 where user_id=@user_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsers FindUserType(int userId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle(@"Select id, company_type from elio_users with (nolock) 
                                      where id=@id and is_public = 1 and account_status=1                                 
                                      order by id asc"
                                      , DatabaseHelper.CreateIntParameter("@id", userId));
        }

        public static bool IsUsersSpecificType(int userId, string companyType, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from elio_users with (nolock) where id=@id and company_type=@company_type"
                               , DatabaseHelper.CreateIntParameter("@id", userId)
                               , DatabaseHelper.CreateStringParameter("@company_type", companyType));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static ElioUserEmailNotifications GetEmailNotificationByDescription(string description, DBSession session)
        {
            DataLoader<ElioUserEmailNotifications> loader = new DataLoader<ElioUserEmailNotifications>(session);
            return loader.LoadSingle("Select * from Elio_user_email_notifications where description=@description", DatabaseHelper.CreateStringParameter("@description", description));
        }

        public static List<ElioUserEmailNotifications> GetAllEmailNotifications(DBSession session)
        {
            DataLoader<ElioUserEmailNotifications> loader = new DataLoader<ElioUserEmailNotifications>(session);
            return loader.Load("Select * from Elio_user_email_notifications where id=17");
        }

        public static List<ElioUserEmailNotifications> GetAllLiveEmailNotifications(DBLiveSession lvSession)
        {
            DataLiveLoader<ElioUserEmailNotifications> loader = new DataLiveLoader<ElioUserEmailNotifications>(lvSession);
            return loader.Load("Select * from Elio_user_email_notifications where id=17");
        }

        public static List<ElioUsers> GetFeaturedCompanies(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select id, company_logo from elio_users with (nolock) where id in (66,18,24,52,59,61)");
        }

        public static ElioUsersIJViews GetPopularVendor(DBSession session)
        {
            DataLoader<ElioUsersIJViews> loader = new DataLoader<ElioUsersIJViews>(session);
            return loader.LoadSingle("select top 1 * from Elio_users with (nolock) inner join Elio_companies_views on Elio_companies_views.company_id=Elio_users.id " +
                                     "where company_type='Vendors' order by views desc");
        }

        public static bool IsAdminRole(int userId, int roleId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(id) as count from Elio_permissions_users_roles with (nolock) 
                                                    where role_name = 'Admin'
                                                    and user_id = @user_id
                                                    and id = @id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@id", roleId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static ElioUsers GetUserByUsernameAndPassword(string username, string password, string email, string sessionId, out string errorMessage, string sessionLang, out string subAccountEmailLogin, out int loggedInRoleId, out bool isAdminRole, DBSession session)
        {
            errorMessage = string.Empty;
            subAccountEmailLogin = "";
            loggedInRoleId = 0;
            isAdminRole = false;

            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            ElioUsers user = null;

            user = loader.LoadSingle(@"Select * from elio_users with (nolock) 
                                    where ((username = @username and password = @password) 
                                    or (email = @email and password = @password) 
                                    or (username_encrypted = @username_encrypted and password_encrypted = @password_encrypted))
                                    and (elio_users.email not in 
                                    (
	                                    select email
	                                    from Elio_users_sub_accounts
	                                    where Elio_users_sub_accounts.email = elio_users.email
                                    ))"
                                                                       , DatabaseHelper.CreateStringParameter("@username", username)
                                                                       , DatabaseHelper.CreateStringParameter("@password", password)
                                                                       , DatabaseHelper.CreateStringParameter("@email", email)
                                                                       , DatabaseHelper.CreateStringParameter("@username_encrypted", username)
                                                                       , DatabaseHelper.CreateStringParameter("@password_encrypted", password));

            if (user != null)
            {
                if (user.AccountStatus == Convert.ToInt32(AccountStatus.Blocked))
                {
                    errorMessage = Localizer.GetTranslationResults(sessionLang, new XElementInfo("content", "login", "label", "13")).Text;
                    return null;
                }
                else if (user.AccountStatus == Convert.ToInt32(AccountStatus.Deleted))
                {
                    errorMessage = Localizer.GetTranslationResults(sessionLang, new XElementInfo("content", "login", "label", "15")).Text;
                    return null;
                }
                else
                {
                    user.LastLogin = DateTime.Now;
                    user.UserLoginCount = user.UserLoginCount + 1;

                    loader.Update(user);
                }
            }
            else
            {
                DataLoader<ElioUsers> loader2 = new DataLoader<ElioUsers>(session);
                user = null;

                user = loader.LoadSingle(@"Select * from elio_users with (nolock) 
                                    where ((username = @username and password = @password) 
                                    or (email = @email and password = @password) 
                                    or (username_encrypted = @username_encrypted and password_encrypted = @password_encrypted))"
                                                                       , DatabaseHelper.CreateStringParameter("@username", username)
                                                                       , DatabaseHelper.CreateStringParameter("@password", password)
                                                                       , DatabaseHelper.CreateStringParameter("@email", email)
                                                                       , DatabaseHelper.CreateStringParameter("@username_encrypted", username)
                                                                       , DatabaseHelper.CreateStringParameter("@password_encrypted", password));

                if (user != null)
                {
                    DataLoader<ElioUsersSubAccounts> loader1 = new DataLoader<ElioUsersSubAccounts>(session);

                    ElioUsersSubAccounts subAccount = loader1.LoadSingle(@"Select * from Elio_users_sub_accounts 
                                                                            where email = @email and (password = @password or password_encrypted = @password)"
                                                                        , DatabaseHelper.CreateStringParameter("@email", user.Email)
                                                                        , DatabaseHelper.CreateStringParameter("@password", password));

                    if (subAccount != null)
                    {
                        if (subAccount.IsConfirmed == Convert.ToInt32(AccountStatus.Confirmed))
                        {
                            if (subAccount.IsActive == Convert.ToInt32(AccountStatus.Active))
                            {
                                if (subAccount.AccountStatus == Convert.ToInt32(AccountStatus.Blocked))
                                {
                                    errorMessage = Localizer.GetTranslationResults(sessionLang, new XElementInfo("content", "login", "label", "16")).Text;
                                    return null;
                                }
                                else if (subAccount.AccountStatus == Convert.ToInt32(AccountStatus.Deleted))
                                {
                                    errorMessage = Localizer.GetTranslationResults(sessionLang, new XElementInfo("content", "login", "label", "17")).Text;
                                    return null;
                                }
                                else
                                {
                                    subAccount.LastLogin = DateTime.Now;
                                    subAccount.UserLoginCount = subAccount.UserLoginCount + 1;

                                    loader1.Update(subAccount);

                                    user = GetUserById(subAccount.UserId, session);

                                    subAccountEmailLogin = subAccount.Email;
                                    loggedInRoleId = subAccount.TeamRoleId;
                                    isAdminRole = IsAdminRole(user.Id, subAccount.TeamRoleId, session);
                                }
                            }
                            else
                            {
                                errorMessage = Localizer.GetTranslationResults(sessionLang, new XElementInfo("content", "login", "label", "18")).Text;
                                return null;
                            }
                        }
                        else
                        {
                            errorMessage = Localizer.GetTranslationResults(sessionLang, new XElementInfo("content", "login", "label", "19")).Text;
                            return null;
                        }
                    }
                }
                else
                {
                    if (user == null)
                    {
                        DataLoader<ElioUsersSubAccounts> loader3 = new DataLoader<ElioUsersSubAccounts>(session);

                        ElioUsersSubAccounts subAccount = loader3.LoadSingle(@"Select * from Elio_users_sub_accounts 
                                                                            where email = @email and (password = @password or password_encrypted = @password)"
                                                                        , DatabaseHelper.CreateStringParameter("@email", username)
                                                                        , DatabaseHelper.CreateStringParameter("@password", password));

                        if (subAccount != null)
                        {
                            if (subAccount.IsConfirmed == Convert.ToInt32(AccountStatus.Confirmed))
                            {
                                if (subAccount.IsActive == Convert.ToInt32(AccountStatus.Active))
                                {
                                    if (subAccount.AccountStatus == Convert.ToInt32(AccountStatus.Blocked))
                                    {
                                        errorMessage = Localizer.GetTranslationResults(sessionLang, new XElementInfo("content", "login", "label", "16")).Text;
                                        return null;
                                    }
                                    else if (subAccount.AccountStatus == Convert.ToInt32(AccountStatus.Deleted))
                                    {
                                        errorMessage = Localizer.GetTranslationResults(sessionLang, new XElementInfo("content", "login", "label", "17")).Text;
                                        return null;
                                    }
                                    else
                                    {
                                        subAccount.LastLogin = DateTime.Now;
                                        subAccount.UserLoginCount = subAccount.UserLoginCount + 1;

                                        loader3.Update(subAccount);

                                        user = GetUserById(subAccount.UserId, session);
                                        subAccountEmailLogin = subAccount.Email;
                                        loggedInRoleId = subAccount.TeamRoleId;
                                        isAdminRole = IsAdminRole(user.Id, subAccount.TeamRoleId, session);
                                    }
                                }
                                else
                                {
                                    errorMessage = Localizer.GetTranslationResults(sessionLang, new XElementInfo("content", "login", "label", "18")).Text;
                                    return null;
                                }
                            }
                            else
                            {
                                errorMessage = Localizer.GetTranslationResults(sessionLang, new XElementInfo("content", "login", "label", "19")).Text;
                                return null;
                            }
                        }
                    }
                }
            }

            if (user != null)
            {
                if (!string.IsNullOrEmpty(sessionId))
                {
                    //////user.CurrentSessionId = sessionId + user.Id;
                    loader.Update(user);
                }
            }

            return user;
        }

        public static ElioUsers GetUserByUsernameEncryptedAndPasswordEncrypted(string usernameEncrypted, string passwordEncrypted, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select * from elio_users with (nolock) where username_encrypted=@username_encrypted and password_encrypted=@password_encrypted", DatabaseHelper.CreateStringParameter("@username_encrypted", usernameEncrypted)
                                                                                               , DatabaseHelper.CreateStringParameter("@password_encrypted", passwordEncrypted));
        }

        public static ElioCompaniesViews GetCompanyViewsByCompanyId(int companyId, DBSession session)
        {
            DataLoader<ElioCompaniesViews> loader = new DataLoader<ElioCompaniesViews>(session);

            return loader.LoadSingle("Select TOP 1 * from Elio_companies_views where company_id=@company_id and (date>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and date<='" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "') order by date asc"
                                     , DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static List<ElioCompaniesViewsCompaniesIJCompanies> GetValuedCompanyViewsCompanies(ElioUsers user, int isDeleted, int canBeViewed, string companyTypesIn, bool showValuedLeadsOnly, DBSession session)
        {
            List<ElioCompaniesViewsCompaniesIJCompanies> companyLeads = new List<ElioCompaniesViewsCompaniesIJCompanies>();

            if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                DataLoader<ElioUsersFeatures> loader0 = new DataLoader<ElioUsersFeatures>(session);
                ElioUsersFeatures userFeatures = loader0.LoadSingle("Select * from Elio_users_features where user_billing_type=@user_billing_type"
                                                                  , DatabaseHelper.CreateIntParameter("@user_billing_type", user.BillingType));

                if (userFeatures != null)
                {
                    ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);
                    if (packetFeatures != null)
                    {
                        DataLoader<ElioCompaniesViewsCompaniesIJCompanies> loader = new DataLoader<ElioCompaniesViewsCompaniesIJCompanies>(session);

                        string strTop = "";

                        if (showValuedLeadsOnly)
                        {
                            int topResults = (packetFeatures.AvailableLeadsCount >= 0) ? userFeatures.TotalLeads - packetFeatures.AvailableLeadsCount : userFeatures.TotalLeads;

                            strTop = "TOP " + topResults.ToString();
                        }

                        companyLeads = loader.Load(@"SELECT " + strTop + " * FROM Elio_companies_views_companies " +
                                                   "inner join Elio_users on Elio_users.id=Elio_companies_views_companies.interested_company_id " +
                                                   "where company_views_company_id=@company_views_company_id " +
                                                   "and is_deleted=@is_deleted " +
                                                   "and company_type in('" + companyTypesIn + "') " +
                                                   "and can_be_viewed = @can_be_viewed " +
                                                   "order by Elio_companies_views_companies.sysdate desc"
                                                   , DatabaseHelper.CreateIntParameter("@company_views_company_id", user.Id)
                                                   , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted)
                                                   , DatabaseHelper.CreateIntParameter("@can_be_viewed", canBeViewed));
                    }
                }
            }

            return companyLeads;
        }

        public static List<ElioCompaniesViewsCompaniesIJCompanies> GetValuedCompanyViewsCompaniesByDateRange(ElioUsers user, int isDeleted, int canBeViewed, string companyTypesIn, bool showValuedLeadsOnly, string fromDate, string toDate, DBSession session)
        {
            List<ElioCompaniesViewsCompaniesIJCompanies> companyLeads = new List<ElioCompaniesViewsCompaniesIJCompanies>();

            if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                DataLoader<ElioUsersFeatures> loader0 = new DataLoader<ElioUsersFeatures>(session);
                ElioUsersFeatures userFeatures = loader0.LoadSingle("Select * from Elio_users_features where user_billing_type=@user_billing_type"
                                                                  , DatabaseHelper.CreateIntParameter("@user_billing_type", user.BillingType));

                if (userFeatures != null)
                {
                    ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);
                    if (packetFeatures != null)
                    {
                        DataLoader<ElioCompaniesViewsCompaniesIJCompanies> loader = new DataLoader<ElioCompaniesViewsCompaniesIJCompanies>(session);

                        string strTop = "";

                        if (showValuedLeadsOnly)
                        {
                            int topResults = (packetFeatures.AvailableLeadsCount >= 0) ? userFeatures.TotalLeads - packetFeatures.AvailableLeadsCount : userFeatures.TotalLeads;

                            strTop = "TOP " + topResults.ToString();
                        }

                        string strQuery = "SELECT " + strTop + " * FROM Elio_companies_views_companies " +
                                                   "left join Elio_users on Elio_users.id=Elio_companies_views_companies.interested_company_id " +
                                                   "where company_views_company_id=@company_views_company_id " +
                                                   "and is_deleted=@is_deleted " +
                                                   "and company_type in('" + companyTypesIn + "') " +
                                                   "and can_be_viewed =@can_be_viewed ";

                        if (fromDate != string.Empty && toDate != string.Empty)
                        {
                            strQuery += " and (Elio_companies_views_companies.sysdate>='" + fromDate + "' and Elio_companies_views_companies.sysdate<='" + toDate + "')";
                        }
                        else
                        {
                            if (fromDate != string.Empty)
                            {
                                strQuery += " and Elio_companies_views_companies.sysdate>='" + fromDate + "'";
                            }
                            else if (toDate != string.Empty)
                            {
                                strQuery += " and Elio_companies_views_companies.sysdate<='" + toDate + "'";
                            }
                        }

                        strQuery += " order by Elio_companies_views_companies.sysdate desc";

                        companyLeads = loader.Load(strQuery, DatabaseHelper.CreateIntParameter("@company_views_company_id", user.Id)
                                                           , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted)
                                                           , DatabaseHelper.CreateIntParameter("@can_be_viewed", canBeViewed));
                    }
                }
            }

            return companyLeads;
        }

        public static int GetCompanyRecentLeadsForCurrentMonthByIsNewIsDeletedStatus(ElioUsers user, int isNew, int isDeleted, DBSession session)
        {
            int companyLeadsCount = 0;

            if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                DataLoader<ElioUsersFeatures> loader0 = new DataLoader<ElioUsersFeatures>(session);
                ElioUsersFeatures userFeatures = loader0.LoadSingle("Select * from Elio_users_features where user_billing_type=@user_billing_type"
                                                                  , DatabaseHelper.CreateIntParameter("@user_billing_type", user.BillingType));

                if (userFeatures != null)
                {
                    ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);
                    if (packetFeatures != null)
                    {
                        string companyTypesIn = "";
                        if (user.CompanyType == Types.Vendors.ToString())
                        {
                            companyTypesIn = "" + EnumHelper.GetDescription(Types.Resellers).ToString() + "";
                        }
                        else if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                        {
                            companyTypesIn = "" + Types.Vendors.ToString() + "";
                        }

                        if (packetFeatures.StartingDate != null && packetFeatures.ExpirationDate != null)
                        {
                            string fromYear = Convert.ToDateTime(packetFeatures.StartingDate).Year.ToString();
                            string fromMonth = Convert.ToDateTime(packetFeatures.StartingDate).Month.ToString();
                            string fromDay = Convert.ToDateTime(packetFeatures.StartingDate).Day.ToString();
                            string toYear = Convert.ToDateTime(packetFeatures.ExpirationDate).Year.ToString();
                            string toMonth = Convert.ToDateTime(packetFeatures.ExpirationDate).Month.ToString();
                            string toDay = Convert.ToDateTime(packetFeatures.ExpirationDate).Day.ToString();

                            string fromDate = fromYear + "-" + fromMonth + "-" + fromDay;
                            string toDate = toYear + "-" + toMonth + "-" + toDay;

                            DataTable table = session.GetDataTable("SELECT COUNT(Elio_companies_views_companies.id) as views_count FROM Elio_companies_views_companies " +
                                                                   "inner join Elio_users on Elio_companies_views_companies.interested_company_id=Elio_users.id " +
                                                                   "where company_views_company_id=@company_views_company_id " +
                                                                   "and is_new =@is_new " +
                                                                   "and is_deleted=@is_deleted " +
                                                                   "and company_type in('" + companyTypesIn + "') " +
                                                                   "and (Elio_companies_views_companies.last_update>='" + fromDate + "' and Elio_companies_views_companies.last_update<='" + toDate + "')"
                                                                   , DatabaseHelper.CreateIntParameter("@company_views_company_id", user.Id)
                                                                   , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                                   , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted));

                            companyLeadsCount = Convert.ToInt32(table.Rows[0]["views_count"]);
                        }
                    }
                }
            }

            return companyLeadsCount;
        }

        public static ElioCompaniesViewsCompanies GetCompanyViewsCompany(int viewedCompanyId, int interestedCompanyId, DBSession session)
        {
            DataLoader<ElioCompaniesViewsCompanies> loader = new DataLoader<ElioCompaniesViewsCompanies>(session);
            return loader.LoadSingle("Select * from Elio_companies_views_companies where company_views_company_id=@company_views_company_id and interested_company_id=@interested_company_id", DatabaseHelper.CreateIntParameter("@company_views_company_id", viewedCompanyId)
                                                                                                                             , DatabaseHelper.CreateIntParameter("@interested_company_id", interestedCompanyId));
        }

        public static List<ElioCompaniesViews> GetCountCompanyViewsByCompanyId(int companyId, DBSession session)
        {
            DataLoader<ElioCompaniesViews> loader = new DataLoader<ElioCompaniesViews>(session);
            return loader.Load("Select * from Elio_companies_views where company_id=@company_id order by date asc", DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static DataTable GetCompanyViewsByCompanyIdForChart(int companyId, DBSession session)
        {
            return session.GetDataTable("Select * from Elio_companies_views where company_id=@company_id order by date", DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static DataTable GetCompanyRegistrationForChartPerYear(DBSession session)
        {
            return session.GetDataTable("select datepart(YEAR, sysdate) as year, count(id) as count " +
                                        "from elio_users with (nolock) " +
                                        "group by datepart(YEAR, sysdate)");
        }

        public static DataTable GetCompanyRegistrationForChartPerMonth(DBSession session)
        {
            return session.GetDataTable("select datepart(YEAR, sysdate) as year, datepart(MONTH, sysdate) as month, count(id) as count " +
                                        "from elio_users with (nolock) " +
                                        "group by datepart(YEAR, sysdate), datepart(MONTH, sysdate) order by year asc");
        }

        public static DataTable GetCompanyRegistrationForChartPerDay(DBSession session)
        {
            return session.GetDataTable("select datepart(YEAR, sysdate) as year, datepart(MONTH, sysdate) as month, datepart(DAY, sysdate) as day, count(id) as count " +
                                        "from elio_users with (nolock) " +
                                        "group by datepart(YEAR, sysdate), datepart(MONTH, sysdate), datepart(DAY, sysdate)");
        }

        public static List<ElioUserTypes> GetUserPublicTypes(DBSession session)
        {
            DataLoader<ElioUserTypes> loader = new DataLoader<ElioUserTypes>(session);
            return loader.Load("Select * from elio_user_types where is_public=1");
        }

        public static List<ElioUserTypes> GetUserAllTypes(DBSession session)
        {
            DataLoader<ElioUserTypes> loader = new DataLoader<ElioUserTypes>(session);
            return loader.Load("Select * from elio_user_types");
        }

        public static List<ElioIndustries> GetIndustries(DBSession session)
        {
            DataLoader<ElioIndustries> loader = new DataLoader<ElioIndustries>(session);
            return loader.Load("Select * from elio_industries where is_public=1");
        }

        public static ElioIndustries GetIndustryByDescription(string description, DBSession session)
        {
            DataLoader<ElioIndustries> loader = new DataLoader<ElioIndustries>(session);
            return loader.LoadSingle("Select * from elio_industries where is_public=1 and industry_description = @industry_description", DatabaseHelper.CreateStringParameter("@industry_description", description));
        }

        public static ElioIndustries GetIndustryById(int id, DBSession session)
        {
            DataLoader<ElioIndustries> loader = new DataLoader<ElioIndustries>(session);
            return loader.LoadSingle("Select * from elio_industries where is_public=1 and id = @id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioSubIndustriesGroupItems> GetAllVerticals(DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.Load("Select * from Elio_sub_industries_group_items where is_public=1 order by description asc");
        }

        public static List<ElioSubIndustriesGroupItems> GetVerticalsOverId(int id, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.Load(@"Select * 
                                    from Elio_sub_industries_group_items 
                                    where is_public = 1 
                                    and id > @id 
                                    order by id asc"
                            , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static DataTable GetVerticalsWithUsersCountForSearchResults(bool withCount, string companyType, DBSession session)
        {
            if (companyType == "channel-partners" || companyType == EnumHelper.GetDescription(Types.Resellers))
            {
                if (withCount)
                    return session.GetDataTable(@"Select 
                                            sigi.id,sigi.sub_industies_group_id,
                                            sigi.description + ' (' + cast(isnull(us.count, 0) as nvarchar(50)) + ')' as description,
                                            LOWER('/profile/channel-partners/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                            from Elio_sub_industries_group_items sigi
                                            cross apply
                                            (
	                                            select COUNT(distinct(u.id)) as count
	                                            from Elio_users_sub_industries_group_items usigi
	                                            inner join Elio_users u
		                                            on u.id = usigi.user_id
	                                            where usigi.sub_industry_group_item_id = sigi.id
	                                            and u.is_public = 1 and u.account_status = 1
	                                            and u.company_type = 'Channel Partners'
                                            ) us

                                            where sigi.is_public = 1 
                                            order by sigi.description asc");
                else
                    return session.GetDataTable(@"Select distinct
                                            sigi.id,sigi.sub_industies_group_id,
                                            sigi.description,
                                            LOWER('/profile/channel-partners/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                            from Elio_sub_industries_group_items sigi
                                            inner join Elio_users_sub_industries_group_items usigi
                                                on usigi.sub_industry_group_item_id = sigi.id
                                            inner join Elio_users u
                                                on u.id = usigi.user_id
	                                        where 1 = 1
	                                        and u.is_public = 1 and u.account_status = 1
	                                        and u.company_type = 'Channel Partners'
                                            and sigi.is_public = 1 
                                            order by sigi.description asc");
            }
            else if (companyType == Types.Vendors.ToString() || companyType == "vendors")
            {
                if (withCount)
                    return session.GetDataTable(@"Select sigi.id,sigi.sub_industies_group_id,
                                            sigi.description + ' (' + cast(isnull(us.count, 0) as nvarchar(50)) + ')' as description,
                                            LOWER('/partner-programs/vendors/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                            from Elio_sub_industries_group_items sigi
                                            cross apply
                                            (
	                                            select COUNT(distinct(u.id)) as count
	                                            from Elio_users_sub_industries_group_items usigi
	                                            inner join Elio_users u
		                                            on u.id = usigi.user_id
	                                            where usigi.sub_industry_group_item_id = sigi.id
	                                            and u.is_public = 1 and u.account_status = 1
	                                            and u.company_type = 'Vendors'
                                            ) us
                                            where sigi.is_public = 1 
                                            order by sigi.description asc");
                else
                    return session.GetDataTable(@"Select distinct sigi.id,sigi.sub_industies_group_id,
                                            sigi.description,
                                            LOWER('/partner-programs/vendors/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                            from Elio_sub_industries_group_items sigi
                                            inner join Elio_users_sub_industries_group_items usigi
	                                            on usigi.sub_industry_group_item_id = sigi.id
                                            inner join Elio_users u
                                                on u.id = usigi.user_id
                                            where 1 = 1
	                                            and u.is_public = 1 and u.account_status = 1
	                                            and u.company_type = 'Vendors'
                                                and sigi.is_public = 1 
                                            order by sigi.description asc");
            }
            else
                return null;
        }

        public static DataTable GetVerticalsForSearchResultsByLetter(string companyType, string startLetter, DBSession session)
        {
            if (companyType == "channel-partners" || companyType == EnumHelper.GetDescription(Types.Resellers))
            {
                string strQuery = @"Select distinct
                                sigi.id,sigi.sub_industies_group_id,
                                sigi.description,
                                LOWER('/profile/channel-partners/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                from Elio_sub_industries_group_items sigi
                                inner join Elio_users_sub_industries_group_items usigi
                                    on usigi.sub_industry_group_item_id = sigi.id
                                inner join Elio_users u
                                    on u.id = usigi.user_id
	                            where 1 = 1
	                            and u.is_public = 1 and u.account_status = 1
	                            and u.company_type = 'Channel Partners'
                                and sigi.is_public = 1 ";

                strQuery += (startLetter == "A") ? " and (sigi.description like '" + startLetter + "%' OR sigi.description like '3%') " : " and sigi.description like '" + startLetter + "%' ";

                strQuery += " order by sigi.description asc";

                return session.GetDataTable(strQuery);
            }
            else if (companyType == Types.Vendors.ToString() || companyType == "vendors")
            {
                string strQuery = @"Select distinct sigi.id,sigi.sub_industies_group_id,
                                    sigi.description,
                                    LOWER('/partner-programs/vendors/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                    from Elio_sub_industries_group_items sigi
                                    inner join Elio_users_sub_industries_group_items usigi
	                                    on usigi.sub_industry_group_item_id = sigi.id
                                    inner join Elio_users u
                                        on u.id = usigi.user_id
                                    where 1 = 1
	                                    and u.is_public = 1 and u.account_status = 1
	                                    and u.company_type = 'Vendors'
                                        and sigi.is_public = 1 ";

                strQuery += (startLetter == "A") ? " and (sigi.description like '" + startLetter + "%' OR sigi.description like '3%') " : " and sigi.description like '" + startLetter + "%' ";

                strQuery += " order by sigi.description asc";

                return session.GetDataTable(strQuery);
            }
            else
                return null;
        }

        public static DataTable GetVerticalsForSearchResultsOrderByLetter(string companyType, DBSession session)
        {
            if (companyType == "channel-partners" || companyType == EnumHelper.GetDescription(Types.Resellers))
            {
                string strQuery = @"Select distinct
                                    sigi.description,
                                    LOWER('/profile/channel-partners/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                    from Elio_sub_industries_group_items sigi
                                    inner join Elio_users_sub_industries_group_items usigi
                                    on usigi.sub_industry_group_item_id = sigi.id
                                    inner join Elio_users u
                                    on u.id = usigi.user_id
                                    where 1 = 1
                                    and u.is_public = 1 
                                    and u.account_status = 1
                                    and u.company_type = 'Channel Partners'
                                    and sigi.is_public = 1   
                                    order by sigi.description";

                return session.GetDataTable(strQuery);
            }
            else if (companyType == Types.Vendors.ToString() || companyType == "vendors")
            {
                string strQuery = @"Select distinct sigi.description,
                                    LOWER('/partner-programs/vendors/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                    from Elio_sub_industries_group_items sigi
                                    inner join Elio_users_sub_industries_group_items usigi
	                                    on usigi.sub_industry_group_item_id = sigi.id
                                    inner join Elio_users u
                                        on u.id = usigi.user_id
                                    where 1 = 1
	                                    and u.is_public = 1 and u.account_status = 1
	                                    and u.company_type = 'Vendors'
                                        and sigi.is_public = 1 ";

                //strQuery += (startLetter == "A") ? " and (sigi.description like '" + startLetter + "%' OR sigi.description like '3%') " : " and sigi.description like '" + startLetter + "%' ";

                //strQuery += " order by sigi.description asc";

                return session.GetDataTable(strQuery);
            }
            else
                return null;
        }

        public static DataTable GetVerticalsWithUsersCountForSearchResultsWithNoTrans(bool withCount, string companyType, string region, string country, string state, string city, DBSession session)
        {
            if (companyType == "channel-partners" || companyType == EnumHelper.GetDescription(Types.Resellers))
            {
                string strQuery = "";

                if (withCount)
                    strQuery = @"Select
                                    sigi.id,sigi.sub_industies_group_id,
                                    sigi.description + ' (' + cast(isnull(us.count, 0) as nvarchar(50)) + ')' as description ";
                else
                    strQuery = @"Select distinct
                                    sigi.id,sigi.sub_industies_group_id,
                                    sigi.description, ";

                strQuery += @" LOWER('/" + region.ToLower().Replace(" ", "-") + "/";

                if (country != "")
                {
                //    if (country == "France")
                //        strQuery += "fr/";
                //    else if (country == "Canada")
                //        strQuery += "ca/";
                //    else if (country == "Spain")
                //        strQuery += "es/";
                //    else if (country == "Germany")
                //        strQuery += "de/";
                //    else if (country == "Portugal" || country == "Brazil")
                //        strQuery += "pt/";
                //    else if (country == "Austria")
                //        strQuery += "at/";
                //    else if (country == "Italy")
                //        strQuery += "it/";
                //    else if (country == "Argentina" || country == "Bolivia" || country == "Chile"
                //        || country == "Colombia" || country == "Costa Rica" || country == "Dominican Republic"
                //        || country == "Ecuador" || country == "El Salvador" || country == "Guatemala"
                //        || country == "Honduras" || country == "Mexico" || country == "Panama"
                //        || country == "Paraguay" || country == "Peru" || country == "Puerto Rico"
                //        || country == "Uruguay" || country == "Venezuela")
                //        strQuery += "la/";
                //    else if (country == "Netherlands")
                //        strQuery += "nl/";
                //    else if (country == "Poland")
                //        strQuery += "pl/";

                    strQuery += country.ToLower().Replace(" ", "-") + "/";
                }

                if (state != "")
                {
                    strQuery += state.ToLower().Replace(" ", "-") + "/";
                }

                if (city != "")
                {
                    strQuery += city.ToLower().Replace(" ", "-") + "/";
                }

                strQuery += @"channel-partners/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link 
                            from Elio_sub_industries_group_items sigi";

                if (withCount)
                {
                    strQuery += @" cross apply 
                                (
                                select COUNT(distinct(u.id)) as count
                                from Elio_users_sub_industries_group_items usigi
                                inner join Elio_users u
                                    on u.id = usigi.user_id
                                where usigi.sub_industry_group_item_id = sigi.id 
                                and u.is_public = 1 and u.account_status = 1
                                and u.company_type = 'Channel Partners' ";

                    if (region != "")
                    {
                        strQuery += " and u.company_region = '" + region + "' ";
                    }

                    if (country != "")
                    {
                        strQuery += " and u.country = '" + country + "' ";
                    }

                    if (state != "")
                    {
                        strQuery += " and u.state = '" + state + "' ";
                    }

                    if (city != "")
                    {
                        strQuery += " and u.city = '" + city + "' ";
                    }

                    strQuery += @" having COUNT(u.id) > 0 
                            ) us 
                            where sigi.is_public = 1
                            order by sigi.description asc";
                }
                else
                {
                    strQuery += @" inner join Elio_users_sub_industries_group_items usigi
                                        on usigi.sub_industry_group_item_id = sigi.id
                                    inner join Elio_users u
                                        on u.id = usigi.user_id
                                    where usigi.sub_industry_group_item_id = sigi.id 
                                    and u.is_public = 1 and u.account_status = 1
                                    and u.company_type = 'Channel Partners' ";

                    if (region != "")
                    {
                        strQuery += " and u.company_region = '" + region + "' ";
                    }

                    if (country != "")
                    {
                        strQuery += " and u.country = '" + country + "' ";
                    }

                    if (state != "")
                    {
                        strQuery += " and u.state = '" + state + "' ";
                    }

                    if (city != "")
                    {
                        strQuery += " and u.city = '" + city + "' ";
                    }
                }

                strQuery += @" and sigi.is_public = 1
                            order by sigi.description asc";

                return session.GetDataTable(strQuery);
            }
            else if (companyType == Types.Vendors.ToString() || companyType == "vendors")
            {
                if (withCount)
                    return session.GetDataTable(@"Select sigi.id,sigi.sub_industies_group_id,
                                            sigi.description + ' (' + cast(isnull(us.count, 0) as nvarchar(50)) + ')' as description,
                                            LOWER('/partner-programs/vendors/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                            from Elio_sub_industries_group_items sigi
                                            cross apply
                                            (
	                                            select COUNT(distinct(u.id)) as count
	                                            from Elio_users_sub_industries_group_items usigi
	                                            inner join Elio_users u
		                                            on u.id = usigi.user_id
	                                            where usigi.sub_industry_group_item_id = sigi.id
	                                            and u.is_public = 1 and u.account_status = 1
	                                            and u.company_type = 'Vendors'
                                            ) us
                                            where sigi.is_public = 1 
                                            order by sigi.description asc");
                else
                    return session.GetDataTable(@"Select distinct sigi.id,sigi.sub_industies_group_id,
                                            sigi.description,
                                            LOWER('/partner-programs/vendors/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                            from Elio_sub_industries_group_items sigi
                                            inner join Elio_users_sub_industries_group_items usigi
	                                            on usigi.sub_industry_group_item_id = sigi.id
                                            inner join Elio_users u
                                                on u.id = usigi.user_id
                                            where 1 = 1
	                                            and u.is_public = 1 and u.account_status = 1
	                                            and u.company_type = 'Vendors'
                                                and sigi.is_public = 1 
                                            order by sigi.description asc");
            }
            else
                return null;
        }

        public static int GetNumberOfCompaniesInCityArea(string companyType, string region, string country, string state, string city, DBSession session)
        {
            string strQuery = @"select count(usigi.id) as count
                                from Elio_users_sub_industries_group_items usigi
                                inner join Elio_users u
	                                on u.id = usigi.user_id
                                where u.is_public = 1 and u.account_status = 1
                                and u.company_type = '" + companyType + "' ";

            if (region != "")
            {
                strQuery += " and u.company_region = '" + region + "' ";
            }

            if (country != "")
            {
                strQuery += " and u.country = '" + country + "' ";
            }

            if (state != "")
            {
                strQuery += " and u.state = '" + state + "' ";
            }

            if (city != "")
            {
                strQuery += " and u.city = '" + city + "' ";
            }

            DataTable table = session.GetDataTable(strQuery);

            if (table != null && table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["count"]);
            else
                return 0;
        }

        public static DataTable GetVerticalsForSearchResultsByLetter(string companyType, string startLetter, string region, string country, string state, string city, DBSession session)
        {
            if (companyType == "channel-partners" || companyType == EnumHelper.GetDescription(Types.Resellers))
            {
                string strQuery = "";

                strQuery = @"Select distinct
                                    sigi.id,sigi.sub_industies_group_id,
                                    sigi.description, ";

                strQuery += @" LOWER('/" + region.ToLower().Replace(" ", "-") + "/";

                if (country != "")
                {
                    if (country == "France")
                        strQuery += "fr/";
                    else if (country == "Canada")
                        strQuery += "ca/";
                    else if (country == "Spain")
                        strQuery += "es/";
                    else if (country == "Germany")
                        strQuery += "de/";
                    else if (country == "Portugal" || country == "Brazil")
                        strQuery += "pt/";
                    else if (country == "Austria")
                        strQuery += "at/";
                    else if (country == "Italy")
                        strQuery += "it/";
                    else if (country == "Argentina" || country == "Bolivia" || country == "Chile"
                        || country == "Colombia" || country == "Costa Rica" || country == "Dominican Republic"
                        || country == "Ecuador" || country == "El Salvador" || country == "Guatemala"
                        || country == "Honduras" || country == "Mexico" || country == "Panama"
                        || country == "Paraguay" || country == "Peru" || country == "Puerto Rico"
                        || country == "Uruguay" || country == "Venezuela")
                        strQuery += "la/";
                    else if (country == "Netherlands")
                        strQuery += "nl/";
                    else if (country == "Poland")
                        strQuery += "pl/";

                    strQuery += country.ToLower().Replace(" ", "-") + "/";
                }

                if (state != "")
                {
                    strQuery += state.ToLower().Replace(" ", "-") + "/";
                }

                if (city != "")
                {
                    strQuery += city.ToLower().Replace(" ", "-") + "/";
                }

                strQuery += @"channel-partners/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link 
                            from Elio_sub_industries_group_items sigi";


                strQuery += @" inner join Elio_users_sub_industries_group_items usigi
                                        on usigi.sub_industry_group_item_id = sigi.id
                                    inner join Elio_users u
                                        on u.id = usigi.user_id
                                    where usigi.sub_industry_group_item_id = sigi.id 
                                    and u.is_public = 1 and u.account_status = 1
                                    and u.company_type = 'Channel Partners' ";

                if (region != "")
                {
                    strQuery += " and u.company_region = '" + region + "' ";
                }

                if (country != "")
                {
                    strQuery += " and u.country = '" + country + "' ";
                }

                if (state != "")
                {
                    strQuery += " and u.state = '" + state + "' ";
                }

                if (city != "")
                {
                    strQuery += " and u.city = '" + city + "' ";
                }

                strQuery += (startLetter == "A") ? " and (sigi.description like '" + startLetter + "%' OR sigi.description like '3%') " : " and sigi.description like '" + startLetter + "%' ";

                strQuery += @" and sigi.is_public = 1
                            order by sigi.description asc";

                return session.GetDataTable(strQuery);
            }
            else if (companyType == Types.Vendors.ToString() || companyType == "vendors")
            {
                string strQuery = @"Select distinct sigi.id,sigi.sub_industies_group_id,
                                    sigi.description,
                                    LOWER('/partner-programs/vendors/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                    from Elio_sub_industries_group_items sigi
                                    inner join Elio_users_sub_industries_group_items usigi
	                                    on usigi.sub_industry_group_item_id = sigi.id
                                    inner join Elio_users u
                                        on u.id = usigi.user_id
                                    where 1 = 1
	                                    and u.is_public = 1 and u.account_status = 1
	                                    and u.company_type = 'Vendors'
                                        and sigi.is_public = 1 ";

                strQuery += (startLetter == "A") ? " and (sigi.description like '" + startLetter + "%' OR sigi.description like '3%') " : " and sigi.description like '" + startLetter + "%' ";

                strQuery += " order by sigi.description asc";

                return session.GetDataTable(strQuery);
            }
            else
                return null;
        }

        public static DataTable GetVerticalsWithUsersCountForSearchResults(bool withCount, string companyType, string region, string country, string state, string city, DBSession session)
        {
            if (companyType == "channel-partners" || companyType == EnumHelper.GetDescription(Types.Resellers))
            {
                string strQuery = "";

                if (withCount)
                    strQuery = @"Select
                                    sigi.id,sigi.sub_industies_group_id,
                                    sigi.description + ' (' + cast(isnull(us.count, 0) as nvarchar(50)) + ')' as description ";
                else
                    strQuery = @"Select distinct
                                    sigi.id,sigi.sub_industies_group_id,
                                    sigi.description, ";

                strQuery += @" LOWER('/" + region.ToLower().Replace(" ", "-") + "/";

                if (country != "")
                {
                    if (country == "France")
                        strQuery += "fr/";
                    else if (country == "Canada")
                        strQuery += "ca/";
                    else if (country == "Spain")
                        strQuery += "es/";
                    else if (country == "Germany")
                        strQuery += "de/";
                    else if (country == "Portugal" || country == "Brazil")
                        strQuery += "pt/";
                    else if (country == "Austria")
                        strQuery += "at/";
                    else if (country == "Italy")
                        strQuery += "it/";
                    else if (country == "Argentina" || country == "Bolivia" || country == "Chile"
                        || country == "Colombia" || country == "Costa Rica" || country == "Dominican Republic"
                        || country == "Ecuador" || country == "El Salvador" || country == "Guatemala"
                        || country == "Honduras" || country == "Mexico" || country == "Panama"
                        || country == "Paraguay" || country == "Peru" || country == "Puerto Rico"
                        || country == "Uruguay" || country == "Venezuela")
                        strQuery += "la/";
                    else if (country == "Netherlands")
                        strQuery += "nl/";
                    else if (country == "Poland")
                        strQuery += "pl/";

                    strQuery += country.ToLower().Replace(" ", "-") + "/";
                }

                if (state != "")
                {
                    strQuery += state.ToLower().Replace(" ", "-") + "/";
                }

                if (city != "")
                {
                    strQuery += city.ToLower().Replace(" ", "-") + "/";
                }

                strQuery += @"channel-partners/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link 
                            from Elio_sub_industries_group_items sigi";

                if (withCount)
                {
                    strQuery += @" cross apply 
                                (
                                select COUNT(distinct(u.id)) as count
                                from Elio_users_sub_industries_group_items usigi
                                inner join Elio_users u
                                    on u.id = usigi.user_id
                                where usigi.sub_industry_group_item_id = sigi.id 
                                and u.is_public = 1 and u.account_status = 1
                                and u.company_type = 'Channel Partners' ";

                    if (region != "")
                    {
                        strQuery += " and u.company_region = '" + region + "' ";
                    }

                    if (country != "")
                    {
                        strQuery += " and u.country = '" + country + "' ";
                    }

                    if (state != "")
                    {
                        strQuery += " and u.state = '" + state + "' ";
                    }

                    if (city != "")
                    {
                        strQuery += " and u.city = '" + city + "' ";
                    }

                    strQuery += @" having COUNT(u.id) > 0 
                            ) us 
                            where sigi.is_public = 1
                            order by sigi.description asc";
                }
                else
                {
                    strQuery += @" inner join Elio_users_sub_industries_group_items usigi
                                        on usigi.sub_industry_group_item_id = sigi.id
                                    inner join Elio_users u
                                        on u.id = usigi.user_id
                                    where usigi.sub_industry_group_item_id = sigi.id 
                                    and u.is_public = 1 and u.account_status = 1
                                    and u.company_type = 'Channel Partners' ";

                    if (region != "")
                    {
                        strQuery += " and u.company_region = '" + region + "' ";
                    }

                    if (country != "")
                    {
                        strQuery += " and u.country = '" + country + "' ";
                    }

                    if (state != "")
                    {
                        strQuery += " and u.state = '" + state + "' ";
                    }

                    if (city != "")
                    {
                        strQuery += " and u.city = '" + city + "' ";
                    }
                }

                strQuery += @" and sigi.is_public = 1
                            order by sigi.description asc";

                return session.GetDataTable(strQuery);
            }
            else if (companyType == Types.Vendors.ToString() || companyType == "vendors")
            {
                if (withCount)
                    return session.GetDataTable(@"Select sigi.id,sigi.sub_industies_group_id,
                                            sigi.description + ' (' + cast(isnull(us.count, 0) as nvarchar(50)) + ')' as description,
                                            LOWER('/partner-programs/vendors/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                            from Elio_sub_industries_group_items sigi
                                            cross apply
                                            (
	                                            select COUNT(distinct(u.id)) as count
	                                            from Elio_users_sub_industries_group_items usigi
	                                            inner join Elio_users u
		                                            on u.id = usigi.user_id
	                                            where usigi.sub_industry_group_item_id = sigi.id
	                                            and u.is_public = 1 and u.account_status = 1
	                                            and u.company_type = 'Vendors'
                                            ) us
                                            where sigi.is_public = 1 
                                            order by sigi.description asc");
                else
                    return session.GetDataTable(@"Select distinct sigi.id,sigi.sub_industies_group_id,
                                            sigi.description,
                                            LOWER('/partner-programs/vendors/' + REPLACE(REPLACE(sigi.description, '&', 'and'), ' ', '_')) as link
                                            from Elio_sub_industries_group_items sigi
                                            inner join Elio_users_sub_industries_group_items usigi
	                                            on usigi.sub_industry_group_item_id = sigi.id
                                            inner join Elio_users u
                                                on u.id = usigi.user_id
                                            where 1 = 1
	                                            and u.is_public = 1 and u.account_status = 1
	                                            and u.company_type = 'Vendors'
                                                and sigi.is_public = 1 
                                            order by sigi.description asc");
            }
            else
                return null;
        }

        public static DataTable GetTechnologiesWithUsersCountByRegionForSearchResults(string companyType, string region, string country, DBSession session)
        {
            if (companyType == "channel-partners" || companyType == EnumHelper.GetDescription(Types.Resellers))
            {
                string strQuery = @"Select 
                                    rp.id,
                                    rp.description + ' Resellers (' + cast(isnull(us.count, 0) as nvarchar(50)) + ')' as description, ";

                strQuery += @" LOWER('/" + region.ToLower().Replace(" ", "-") + "/";

                if (country != "")
                {
                    if (country == "France")
                        strQuery += "fr/";
                    else if (country == "Canada")
                        strQuery += "ca/";
                    else if (country == "Spain")
                        strQuery += "es/";
                    else if (country == "Germany")
                        strQuery += "de/";
                    else if (country == "Portugal" || country == "Brazil")
                        strQuery += "pt/";
                    else if (country == "Austria")
                        strQuery += "at/";
                    else if (country == "Italy")
                        strQuery += "it/";
                    else if (country == "Argentina" || country == "Bolivia" || country == "Chile"
                        || country == "Colombia" || country == "Costa Rica" || country == "Dominican Republic"
                        || country == "Ecuador" || country == "El Salvador" || country == "Guatemala"
                        || country == "Honduras" || country == "Mexico" || country == "Panama"
                        || country == "Paraguay" || country == "Peru" || country == "Puerto Rico"
                        || country == "Uruguay" || country == "Venezuela")
                        strQuery += "la/";
                    else if (country == "Netherlands")
                        strQuery += "nl/";
                    else if (country == "Poland")
                        strQuery += "pl/";

                    strQuery += country.ToLower().Replace(" ", "-") + "/";
                }

                strQuery += @"channel-partners/' + REPLACE(REPLACE(rp.description, '&', 'and'), ' ', '_')) as link 
                            from Elio_registration_products rp 
                            cross apply 
                            ( 
                                select COUNT(distinct(u.id)) as count 
                                from Elio_users_registration_products urp 
                                inner join Elio_users u 
                                    on u.id = urp.user_id 
                                where urp.reg_products_id = rp.id 
                                and u.is_public = 1 and u.account_status = 1 
                                and u.company_type = 'Channel Partners' 
                                and u.company_region = @region ";

                if (country != "")
                {
                    strQuery += " and u.country = '" + country + "' ";
                }

                strQuery += @" having COUNT(u.id) > 0 
                            ) us
                            where rp.is_public = 1
                            order by rp.description";

                return session.GetDataTable(strQuery
                                    , DatabaseHelper.CreateStringParameter("@region", region));
            }
            else
                return null;
        }

        public static DataTable GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(bool withCount, bool isTop50, string companyType, string region, string country, string state, string city, DBSession session)
        {
            if (companyType == "channel-partners" || companyType == EnumHelper.GetDescription(Types.Resellers))
            {
                string strQuery = "";

                if (withCount)
                    strQuery = @"Select top 50 percent 
                                    rp.id,";
                else
                    strQuery = @"Select distinct top 50 percent 
                                    rp.id,";

                if (withCount)
                    strQuery += " rp.description + ' Resellers (' + cast(isnull(us.count, 0) as nvarchar(50)) + ')' as description, ";
                else
                    strQuery += " rp.description + ' Resellers' as description, ";

                strQuery += @" LOWER('/" + region.ToLower().Replace(" ", "-") + "/";

                if (country != "")
                {
                    //if (country == "France")
                    //    strQuery += "fr/";
                    //else if (country == "Canada")
                    //    strQuery += "ca/";
                    //else if (country == "Spain")
                    //    strQuery += "es/";
                    //else if (country == "Germany")
                    //    strQuery += "de/";
                    //else if (country == "Portugal" || country == "Brazil")
                    //    strQuery += "pt/";
                    //else if (country == "Austria")
                    //    strQuery += "at/";
                    //else if (country == "Italy")
                    //    strQuery += "it/";
                    //else if (country == "Argentina" || country == "Bolivia" || country == "Chile"
                    //    || country == "Colombia" || country == "Costa Rica" || country == "Dominican Republic"
                    //    || country == "Ecuador" || country == "El Salvador" || country == "Guatemala"
                    //    || country == "Honduras" || country == "Mexico" || country == "Panama"
                    //    || country == "Paraguay" || country == "Peru" || country == "Puerto Rico"
                    //    || country == "Uruguay" || country == "Venezuela")
                    //    strQuery += "la/";
                    //else if (country == "Netherlands")
                    //    strQuery += "nl/";
                    //else if (country == "Poland")
                    //    strQuery += "pl/";

                    strQuery += country.ToLower().Replace(" ", "-") + "/";
                }

                if (state != "")
                {
                    strQuery += state.ToLower().Replace(" ", "-") + "/";
                }

                if (city != "")
                {
                    strQuery += city.ToLower().Replace(" ", "-") + "/";
                }

                strQuery += @"channel-partners/' + REPLACE(REPLACE(rp.description, '&', 'and'), ' ', '_')) as link 
                            from Elio_registration_products rp ";

                if (withCount)
                {
                    strQuery += @" cross apply
                                    (
                                    select COUNT(distinct(u.id)) as count
                                    from Elio_users_registration_products urp
                                    inner join Elio_users u
                                        on u.id = urp.user_id
                                    where urp.reg_products_id = rp.id
                                    and u.is_public = 1 and u.account_status = 1
                                    and u.company_type = 'Channel Partners'
                                    and u.company_region = @region ";

                    if (country != "")
                    {
                        strQuery += " and u.country = '" + country + "' ";
                    }

                    if (state != "")
                    {
                        strQuery += " and u.state = '" + state + "' ";
                    }

                    if (city != "")
                    {
                        strQuery += " and u.city = '" + city + "' ";
                    }

                    strQuery += @" having COUNT(u.id) > 0 
                            ) us
                            where rp.is_public = 1 ";
                }
                else
                {
                    strQuery += @" inner join Elio_users_registration_products urp 
	                                on rp.id = urp.reg_products_id
                                    inner join Elio_users u 
                                        on u.id = urp.user_id 
                                    where 1 = 1
                                    and u.is_public = 1 and u.account_status = 1
                                    and u.company_type = 'Channel Partners'
                                    and u.company_region = @region ";

                    if (country != "")
                    {
                        strQuery += " and u.country = '" + country + "' ";
                    }

                    if (state != "")
                    {
                        strQuery += " and u.state = '" + state + "' ";
                    }

                    if (city != "")
                    {
                        strQuery += " and u.city = '" + city + "' ";
                    }

                    strQuery += " and rp.is_public = 1 ";
                }

                strQuery += (isTop50) ? " order by rp.id asc" : " order by rp.id desc";

                return session.GetDataTable(strQuery
                                    , DatabaseHelper.CreateStringParameter("@region", region));
            }
            else
                return null;
        }

        public static int GetRegistrationProductsCount(DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                          FROM [Elioplus_DB].[dbo].[Elio_registration_products]
                                                          where is_public = 1");

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) : 9000;
        }

        public static DataTable GetRegistrationProductsTableByLetter(string startLetter, DBSession session)
        {
            string query = @"SELECT [id]
                                    ,[description] + ' partners' as description
                                    ,'/profile/channel-partners/'+ LOWER(REPLACE(REPLACE(description, ' ', '_'), '&', 'and')) as link
                                FROM [Elioplus_DB].[dbo].[Elio_registration_products]
                                where is_public = 1 ";
            query += (startLetter == "A") ? " and (description like '" + startLetter + "%' OR description like '3%' OR description like '1%') " : " and description like '" + startLetter + "%' ";

            query += " order by description asc";

            return session.GetDataTable(query);
        }

        public static DataTable GetTechnologiesForSearchResultsByLetter(string companyType, string startLetter, string region, string country, string state, string city, DBSession session)
        {
            if (companyType == "channel-partners" || companyType == EnumHelper.GetDescription(Types.Resellers))
            {
                string strQuery = "";

                strQuery = @"Select distinct rp.id, rp.description, ";

                strQuery += @" LOWER('/" + region.ToLower().Replace(" ", "-") + "/";

                if (country != "")
                {
                    if (country == "France")
                        strQuery += "fr/";
                    else if (country == "Canada")
                        strQuery += "ca/";
                    else if (country == "Spain")
                        strQuery += "es/";
                    else if (country == "Germany")
                        strQuery += "de/";
                    else if (country == "Portugal" || country == "Brazil")
                        strQuery += "pt/";
                    else if (country == "Austria")
                        strQuery += "at/";
                    else if (country == "Italy")
                        strQuery += "it/";
                    else if (country == "Argentina" || country == "Bolivia" || country == "Chile"
                        || country == "Colombia" || country == "Costa Rica" || country == "Dominican Republic"
                        || country == "Ecuador" || country == "El Salvador" || country == "Guatemala"
                        || country == "Honduras" || country == "Mexico" || country == "Panama"
                        || country == "Paraguay" || country == "Peru" || country == "Puerto Rico"
                        || country == "Uruguay" || country == "Venezuela")
                        strQuery += "la/";
                    else if (country == "Netherlands")
                        strQuery += "nl/";
                    else if (country == "Poland")
                        strQuery += "pl/";

                    strQuery += country.ToLower().Replace(" ", "-") + "/";
                }

                if (state != "")
                {
                    strQuery += state.ToLower().Replace(" ", "-") + "/";
                }

                if (city != "")
                {
                    strQuery += city.ToLower().Replace(" ", "-") + "/";
                }

                strQuery += @"channel-partners/' + REPLACE(REPLACE(rp.description, '&', 'and'), ' ', '_')) as link 
                            from Elio_registration_products rp ";


                strQuery += @" inner join Elio_users_registration_products urp 
	                                on rp.id = urp.reg_products_id
                                    inner join Elio_users u 
                                        on u.id = urp.user_id 
                                    where 1 = 1
                                    and u.is_public = 1 and u.account_status = 1
                                    and u.company_type = 'Channel Partners'
                                    and u.company_region = @region ";

                if (country != "")
                {
                    strQuery += " and u.country = '" + country + "' ";
                }

                if (state != "")
                {
                    strQuery += " and u.state = '" + state + "' ";
                }

                if (city != "")
                {
                    strQuery += " and u.city = '" + city + "' ";
                }

                strQuery += " and rp.is_public = 1 ";

                strQuery += (startLetter == "A") ? " and (rp.description like '" + startLetter + "%' OR rp.description like '3%' OR rp.description like '1%') " : " and rp.description like '" + startLetter + "%' ";

                strQuery += " order by rp.description asc";

                return session.GetDataTable(strQuery
                                    , DatabaseHelper.CreateStringParameter("@region", region));
            }
            else
                return null;
        }

        public static DataTable GetTechnologiesWithUsersCountForSearchResultsByTop(bool withCount, bool isTop50, string companyType, string region, string country, string state, string city, DBSession session)
        {
            if (companyType == "channel-partners" || companyType == EnumHelper.GetDescription(Types.Resellers))
            {
                string strQuery = "";

                if (withCount)
                    strQuery = @"Select top 50 percent 
                                    rp.id,";
                else
                    strQuery = @"Select distinct top 50 percent 
                                    rp.id,";

                if (withCount)
                    strQuery += " rp.description + ' Resellers (' + cast(isnull(us.count, 0) as nvarchar(50)) + ')' as description, ";
                else
                    strQuery += " rp.description + ' Resellers' as description, ";

                strQuery += @" LOWER('/" + region.ToLower().Replace(" ", "-") + "/";

                if (country != "")
                {
                    if (country == "France")
                        strQuery += "fr/";
                    else if (country == "Canada")
                        strQuery += "ca/";
                    else if (country == "Spain")
                        strQuery += "es/";
                    else if (country == "Germany")
                        strQuery += "de/";
                    else if (country == "Portugal" || country == "Brazil")
                        strQuery += "pt/";
                    else if (country == "Austria")
                        strQuery += "at/";
                    else if (country == "Italy")
                        strQuery += "it/";
                    else if (country == "Argentina" || country == "Bolivia" || country == "Chile"
                        || country == "Colombia" || country == "Costa Rica" || country == "Dominican Republic"
                        || country == "Ecuador" || country == "El Salvador" || country == "Guatemala"
                        || country == "Honduras" || country == "Mexico" || country == "Panama"
                        || country == "Paraguay" || country == "Peru" || country == "Puerto Rico"
                        || country == "Uruguay" || country == "Venezuela")
                        strQuery += "la/";
                    else if (country == "Netherlands")
                        strQuery += "nl/";
                    else if (country == "Poland")
                        strQuery += "pl/";

                    strQuery += country.ToLower().Replace(" ", "-") + "/";
                }

                if (state != "")
                {
                    strQuery += state.ToLower().Replace(" ", "-") + "/";
                }

                if (city != "")
                {
                    strQuery += city.ToLower().Replace(" ", "-") + "/";
                }

                strQuery += @"channel-partners/' + REPLACE(REPLACE(rp.description, '&', 'and'), ' ', '_')) as link 
                            from Elio_registration_products rp ";

                if (withCount)
                {
                    strQuery += @" cross apply
                                    (
                                    select COUNT(distinct(u.id)) as count
                                    from Elio_users_registration_products urp
                                    inner join Elio_users u
                                        on u.id = urp.user_id
                                    where urp.reg_products_id = rp.id
                                    and u.is_public = 1 and u.account_status = 1
                                    and u.company_type = 'Channel Partners'
                                    and u.company_region = @region ";

                    if (country != "")
                    {
                        strQuery += " and u.country = '" + country + "' ";
                    }

                    if (state != "")
                    {
                        strQuery += " and u.state = '" + state + "' ";
                    }

                    if (city != "")
                    {
                        strQuery += " and u.city = '" + city + "' ";
                    }

                    strQuery += @" having COUNT(u.id) > 0 
                            ) us
                            where rp.is_public = 1 ";
                }
                else
                {
                    strQuery += @" inner join Elio_users_registration_products urp 
	                                on rp.id = urp.reg_products_id
                                    inner join Elio_users u 
                                        on u.id = urp.user_id 
                                    where 1 = 1
                                    and u.is_public = 1 and u.account_status = 1
                                    and u.company_type = 'Channel Partners'
                                    and u.company_region = @region ";

                    if (country != "")
                    {
                        strQuery += " and u.country = '" + country + "' ";
                    }

                    if (state != "")
                    {
                        strQuery += " and u.state = '" + state + "' ";
                    }

                    if (city != "")
                    {
                        strQuery += " and u.city = '" + city + "' ";
                    }

                    strQuery += " and rp.is_public = 1 ";
                }

                strQuery += (isTop50) ? " order by rp.id asc" : " order by rp.id desc";

                return session.GetDataTable(strQuery
                                    , DatabaseHelper.CreateStringParameter("@region", region));
            }
            else
                return null;
        }

        public static List<ElioSubIndustriesGroup> GetAllVerticalCategories(DBSession session)
        {
            DataLoader<ElioSubIndustriesGroup> loader = new DataLoader<ElioSubIndustriesGroup>(session);
            return loader.Load("Select * from Elio_sub_industries_group where is_public=1 order by group_description asc");
        }

        public static List<ElioSubIndustriesGroup> GetSubIndustriesGroup(DBSession session)
        {
            DataLoader<ElioSubIndustriesGroup> loader = new DataLoader<ElioSubIndustriesGroup>(session);
            return loader.Load("Select * from Elio_sub_industries_group where is_public=1 order by id");
        }

        public static List<ElioSubIndustriesGroup> GetSubIndustriesGroupExceptHardware(DBSession session)
        {
            DataLoader<ElioSubIndustriesGroup> loader = new DataLoader<ElioSubIndustriesGroup>(session);
            return loader.Load("Select * from Elio_sub_industries_group where is_public=1 and id <> 16 order by id");
        }

        public static List<ElioSubIndustriesGroup> GetVerticalCategoriesOverId(int id, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroup> loader = new DataLoader<ElioSubIndustriesGroup>(session);
            return loader.Load("Select * from Elio_sub_industries_group where is_public = 1 and id > @id order by id asc"
                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioSubIndustriesGroup GetVerticalById(int subIndustryGroupId, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroup> loader = new DataLoader<ElioSubIndustriesGroup>(session);
            return loader.LoadSingle(@"Select * from Elio_sub_industries_group where id = @id"
                                      , DatabaseHelper.CreateIntParameter("@id", subIndustryGroupId));
        }

        public static int GetVerticalIdByDescription(string vertical, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select id from Elio_sub_industries_group_items 
                                                    where description = @description"
                                      , DatabaseHelper.CreateStringParameter("@description", vertical));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["id"].ToString()) : -1;
        }

        public static List<ElioSubIndustriesGroupItems> GetSubIndustriesGroupItemsByIndustryGroupId(int subIndustryGroupId, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.Load(@"Select * from elio_sub_industries_group_items 
                                where sub_industies_group_id = @sub_industies_group_id 
                                and is_public = 1 
                                order by description asc"
                                , DatabaseHelper.CreateIntParameter("@sub_industies_group_id", subIndustryGroupId));
        }

        public static DataSet GetSubIndustriesGroupItemssForAllIndustryGroupsByLinkPrefix(string urlPrefix, DBSession session)
        {
            DataSet ds = new DataSet();
            DataLoader<ElioSubIndustriesGroup> loader = new DataLoader<ElioSubIndustriesGroup>(session);
            List<ElioSubIndustriesGroup> groups = GetSubIndustriesGroupExceptHardware(session);
            foreach (ElioSubIndustriesGroup group in groups)
            {
                DataTable tbl = session.GetDataTable("SELECT sig.id, sg.id as sigi_id,sg.description " +
                    ", '" + urlPrefix + "' + lower(replace(replace(sg.description, '&', 'and'), ' ', '_')) as link " +
                    "FROM Elio_sub_industries_group sig " +
                    "cross apply " +
                    "( " +
                    "select * " +
                    "from Elio_sub_industries_group_items sigi " +
                    "where sigi.sub_industies_group_id = sig.id " +
                    ")sg " +
                    "where sig.id = @sub_industies_group_id " +
                    "order by sig.id,sg.description"

                , DatabaseHelper.CreateIntParameter("@sub_industies_group_id", group.Id));

                ds.Tables.Add(tbl);
            }

            return ds;
        }


        public static List<ElioSubIndustriesGroupItems> GetSubIndustriesGroupItemsByIndustryGroupIdOrderById(int subIndustryGroupId, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.Load(@"Select * from elio_sub_industries_group_items 
                                where sub_industies_group_id = @sub_industies_group_id 
                                and is_public = 1 
                                order by id asc"
                                , DatabaseHelper.CreateIntParameter("@sub_industies_group_id", subIndustryGroupId));
        }

        public static ElioSubIndustriesGroupItems GetSubIndustriesGroupItemByVerticalDescription(string verticalDescription, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.LoadSingle(@"Select top 1 * from elio_sub_industries_group_items 
                                        where description like '" + verticalDescription + "'");
        }

        public static List<ElioSubIndustriesGroupItems> GetSubIndustriesGroupItemsByRegion(string region, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.Load(@"select es.sub_industies_group_id, es.description, count(es.sub_industies_group_id) as num 
                                    from Elio_sub_industries_group_items es  
                                    inner join Elio_users_sub_industries_group_items eus
	                                    on es.id = eus.sub_industry_group_item_id
                                    inner join elio_users eu
	                                    on eu.id = eus.user_id
                                    where eu.company_type = 'Channel Partners' 
                                    and eu.is_public = 1
                                    and eu.company_region = @region
                                    group by es.sub_industies_group_id, es.description
                                    order by es.description"
                                , DatabaseHelper.CreateStringParameter("@region", region));
        }

        public static DataTable GetSubIndustriesGroupItemsByRegionWithLink(string region, DBSession session)
        {
            return session.GetDataTable(@"select es.sub_industies_group_id, es.description, REPLACE(REPLACE(Lower(@region), ' ', '-'), '&', 'and') + '/channel-partners/' + REPLACE(REPLACE(Lower(es.description), ' ', '_'), '&', 'and') as link
                                            from Elio_sub_industries_group_items es  
                                            inner join Elio_users_sub_industries_group_items eus
	                                            on es.id = eus.sub_industry_group_item_id
                                            inner join elio_users eu
	                                            on eu.id = eus.user_id
                                            where eu.company_type = 'Channel Partners' 
                                            and eu.is_public = 1
                                            and eu.company_region = @region
                                            group by es.sub_industies_group_id, es.description
                                            order by es.description"
                                        , DatabaseHelper.CreateStringParameter("@region", region));
        }

        public static List<ElioSubIndustriesGroupItems> GetSubIndustriesGroupItemsByCountry(string country, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.Load(@"select es.sub_industies_group_id, es.description, count(es.sub_industies_group_id) as num 
                                    from Elio_sub_industries_group_items es  
                                    inner join Elio_users_sub_industries_group_items eus
	                                    on es.id = eus.sub_industry_group_item_id
                                    inner join elio_users eu
	                                    on eu.id = eus.user_id
                                    where eu.company_type = 'Channel Partners' 
                                    and eu.is_public = 1
                                    and eu.country = @country
                                    group by es.sub_industies_group_id, es.description
                                    order by es.description"
                                , DatabaseHelper.CreateStringParameter("@country", country));
        }

        public static ElioSubIndustriesGroupIJGroupItems GetSubIndustriesGroupItemsByIndustryGroupIdAndVerticalId(int itemId, int subIndustryGroupId, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupIJGroupItems> loader = new DataLoader<ElioSubIndustriesGroupIJGroupItems>(session);
            return loader.LoadSingle(@"Select sigi.id, sigi.description, sigi.opportunities, sigi.sub_industies_group_id, sig.group_description from Elio_sub_industries_group sig
                                    inner join elio_sub_industries_group_items sigi 
	                                    on sig.id = sigi.sub_industies_group_id
                                    where sigi.sub_industies_group_id = @sub_industies_group_id and sigi.id = @id
                                    order by sig.group_description, sigi.description"
                                , DatabaseHelper.CreateIntParameter("@sub_industies_group_id", subIndustryGroupId)
                                , DatabaseHelper.CreateIntParameter("@id", itemId));
        }

        public static List<ElioSubIndustriesGroupItems> GetUserSubIndustriesGroupItemsIJAlgorithmSubIndustries(int userId, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.Load(@"select subcategory_id, user_id, description 
                                 from Elio_sub_industries_group_items 
                                 inner join Elio_users_algorithm_subcategories 
                                    on Elio_users_algorithm_subcategories.subcategory_id = Elio_sub_industries_group_items.id 
                                 where user_id = @user_id"
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioSubIndustriesGroupItems> GetUserSubIndustriesGroupItems(int userId, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.Load(@"SELECT  sigi.id, sigi.description
                                FROM Elio_sub_industries_group_items sigi
                                inner join Elio_users_sub_industries_group_items usigi
	                                on usigi.sub_industry_group_item_id = sigi.id
                                where user_id = @user_id"
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioIndustries> GetUsersIndustries(int userId, DBSession session)
        {
            DataLoader<ElioIndustries> loader = new DataLoader<ElioIndustries>(session);
            return loader.Load(@"Select * from elio_industries 
                                 inner join elio_users_industries 
                                     on elio_users_industries.industry_id=elio_industries.id 
                                 where user_id=@user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioMarkets> GetUsersMarkets(int userId, DBSession session)
        {
            DataLoader<ElioMarkets> loader = new DataLoader<ElioMarkets>(session);
            return loader.Load(@"Select * from elio_markets 
                                 inner join elio_users_markets 
                                    on elio_users_markets.market_id=elio_markets.id 
                                 where user_id=@user_id"
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersMessages> GetUsersMessagesByStatus(int userId, int isNew, int isDeleted, DBSession session)
        {
            DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
            return loader.Load(@"Select * from elio_users_messages 
                                where receiver_user_id=@receiver_user_id 
                                and is_new=@is_new 
                                and deleted=@deleted 
                                order by sysdate desc"
                                , DatabaseHelper.CreateIntParameter("@receiver_user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                , DatabaseHelper.CreateIntParameter("@deleted", isDeleted));
        }

        public static List<ElioUsersMessages> GetUsersSentMessagesByStatus(int userId, int isNew, int isDeleted, DBSession session)
        {
            DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
            return loader.Load(@"Select * from elio_users_messages 
                                 where sender_user_id=@sender_user_id 
                                 and is_new=@is_new 
                                 and deleted = @deleted 
                                 order by sysdate desc"
                                 , DatabaseHelper.CreateIntParameter("@sender_user_id", userId)
                                 , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                 , DatabaseHelper.CreateIntParameter("@deleted", isDeleted));
        }

        public static List<ElioUsersMessages> GetUsersDeletedMessagesByStatus(int userId, int isDeleted, DBSession session)
        {
            DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
            return loader.Load("Select * from elio_users_messages where receiver_user_id=@receiver_user_id and deleted=@deleted order by sysdate desc", DatabaseHelper.CreateIntParameter("@receiver_user_id", userId)
                                                                                                         , DatabaseHelper.CreateIntParameter("@deleted", isDeleted));
        }

        public static List<ElioUsersMessages> GetUsersSentDeletedMessagesByStatus(int userId, int isDeleted, DBSession session)
        {
            DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
            return loader.Load("Select * from elio_users_messages where sender_user_id=@sender_user_id and deleted=@deleted order by sysdate desc", DatabaseHelper.CreateIntParameter("@sender_user_id", userId)
                                                                                                         , DatabaseHelper.CreateIntParameter("@deleted", isDeleted));
        }

        public static ElioUsersMessages GetUsersMessageById(int messageId, DBSession session)
        {
            DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
            return loader.LoadSingle("Select * from elio_users_messages where id=@id", DatabaseHelper.CreateIntParameter("@id", messageId));
        }

        public static void UpdateUsersMessageDeleteStatusById(int messageId, int deleted, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE elio_users_messages 
                                   SET deleted = @deleted,
                                       last_updated = GETDATE() 
                                   WHERE id=@id"
                                , DatabaseHelper.CreateIntParameter("@deleted", deleted)
                                , DatabaseHelper.CreateIntParameter("@id", messageId));
        }

        public static void UpdateUsersMessageIsNewStatusById(int messageId, int isNew, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE elio_users_messages 
                                   SET is_new = @is_new,
                                       last_updated = GETDATE() 
                                   WHERE id=@id"
                                , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                , DatabaseHelper.CreateIntParameter("@id", messageId));
        }

        public static void DeleteUserMessageById(int messageId, DBSession session)
        {
            session.ExecuteQuery(@"DELETE FROM elio_users_messages WHERE id=@id", DatabaseHelper.CreateIntParameter("@id", messageId));
        }

        public static ElioCompaniesViewsCompanies GetCompaniesViewsCompaniesById(int id, DBSession session)
        {
            DataLoader<ElioCompaniesViewsCompanies> loader = new DataLoader<ElioCompaniesViewsCompanies>(session);
            return loader.LoadSingle("Select * from Elio_companies_views_companies where id=@id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static void UpdateCompaniesViewsCompaniesIsNewStatusById(int id, int isNew, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_companies_views_companies
                                        SET is_new = @is_new 
                                   WHERE id=@id"
                                , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static void DeleteCompaniesViewsCompaniesById(int id, int isDeleted, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_companies_views_companies
                                        SET is_deleted = @is_deleted 
                                   WHERE id=@id"
                                , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted)
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioUsersFavorites> GetUserFavorites(int userId, DBSession session)
        {
            DataLoader<ElioUsersFavorites> loader = new DataLoader<ElioUsersFavorites>(session);
            return loader.Load("Select * from elio_users_favorites where user_id=@user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersFavoritesIJUsers> GetUserFavoritesIJUsersByIsDeletedStatus(int userId, int isDeleted, DBSession session)
        {
            DataLoader<ElioUsersFavoritesIJUsers> loader = new DataLoader<ElioUsersFavoritesIJUsers>(session);
            return loader.Load("Select * from elio_users_favorites " +
                               "inner join elio_users on elio_users.id=Elio_users_favorites.company_id " +
                               "where user_id=@user_id " +
                               "and is_deleted=@is_deleted " +
                               "order by elio_users_favorites.sysdate desc"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted));
        }

        public static List<ElioUsersFavorites> GetUserFavouritedCompanies(int companyId, DBSession session)
        {
            DataLoader<ElioUsersFavorites> loader = new DataLoader<ElioUsersFavorites>(session);
            return loader.Load("Select * from elio_users_favorites where company_id=@company_id", DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static List<ElioUsersFavoritesIJUsers> GetUserFavouritedCompaniesIJUsersByIsDeletedStatus(int companyId, int isDeleted, DBSession session)
        {
            DataLoader<ElioUsersFavoritesIJUsers> loader = new DataLoader<ElioUsersFavoritesIJUsers>(session);
            return loader.Load("Select * from elio_users_favorites " +
                               "inner join elio_users on elio_users.id=Elio_users_favorites.user_id " +
                               "where company_id=@company_id " +
                               "and is_deleted=@is_deleted " +
                               "order by elio_users_favorites.sysdate desc"
                               , DatabaseHelper.CreateIntParameter("@company_id", companyId)
                               , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted));
        }

        public static ElioUserPartnerProgramRating GetUserRatingsById(int id, DBSession session)
        {
            DataLoader<ElioUserPartnerProgramRating> loader = new DataLoader<ElioUserPartnerProgramRating>(session);
            return loader.LoadSingle("Select * from Elio_user_partner_program_rating " +
                                     "where id=@id"
                                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioUserPartnerProgramRatingIJUsers> GetUserRatingsIJUsers(int companyId, DBSession session)
        {
            DataLoader<ElioUserPartnerProgramRatingIJUsers> loader = new DataLoader<ElioUserPartnerProgramRatingIJUsers>(session);
            return loader.Load("Select * from Elio_user_partner_program_rating " +
                               "inner join elio_users on elio_users.id=Elio_user_partner_program_rating.visitor_id " +
                               "where company_id=@company_id order by Elio_user_partner_program_rating.sysdate desc"
                                                   , DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static ElioUserProgramReviewVotes GetVisitorProgramsReviewVotesByReviewId(int reviewId, int visitorId, DBSession session)
        {
            DataLoader<ElioUserProgramReviewVotes> loader = new DataLoader<ElioUserProgramReviewVotes>(session);
            return loader.LoadSingle("Select * from Elio_user_program_review_votes where Elio_user_program_review_id=@ and Elio_user_program_review_visitor_id=@Elio_user_program_review_visitor_id"
                                                                , DatabaseHelper.CreateIntParameter("@Elio_user_program_review_id", reviewId)
                                                                , DatabaseHelper.CreateIntParameter("@Elio_user_program_review_visitor_id", visitorId));
        }

        public static List<ElioUserProgramReview> GetUserIdAllPublicReviews(int companyViewId, DBSession session)
        {
            DataLoader<ElioUserProgramReview> loader = new DataLoader<ElioUserProgramReview>(session);
            return loader.Load(@"Select id,visitor_id from Elio_user_program_review 
                                 where company_id=@company_id and is_public=1 order by id desc"
                                , DatabaseHelper.CreateIntParameter("@company_id", companyViewId));
        }

        public static DataTable GetUserIdAllPublicReviewsAsTable(int companyViewId, DBSession session)
        {
            return session.GetDataTable(@"Select id,visitor_id from Elio_user_program_review
                                          where company_id=@company_id and is_public=1 order by id desc"
                                        , DatabaseHelper.CreateIntParameter("@company_id", companyViewId));
        }

        public static ElioUserProgramReview GetProgramsReviewById(int id, DBSession session)
        {
            DataLoader<ElioUserProgramReview> loader = new DataLoader<ElioUserProgramReview>(session);
            return loader.LoadSingle("Select * from Elio_user_program_review " +
                                     "where id=@id"
                                           , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUserProgramReviewIJUsers GetUserReviewById(int reviewId, DBSession session)
        {
            DataLoader<ElioUserProgramReviewIJUsers> loader = new DataLoader<ElioUserProgramReviewIJUsers>(session);
            return loader.LoadSingle("Select Elio_user_program_review.id,username,company_name,company_logo,Elio_user_program_review.sysdate,review,Elio_user_program_review.is_public,account_status from Elio_user_program_review " +
                                     "left join Elio_users on Elio_users.id=Elio_user_program_review.visitor_id " +
                                     "where Elio_user_program_review.id=@id"
                                                                         , DatabaseHelper.CreateIntParameter("@id", reviewId));
        }

        public static ElioUsersFavorites GetUsersNotDeletedFavoritesById(int id, DBSession session)
        {
            DataLoader<ElioUsersFavorites> loader = new DataLoader<ElioUsersFavorites>(session);
            return loader.LoadSingle("Select * from elio_users_favorites where id=@id and is_deleted=0", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static void UpdateUsersFavoritesDeleteStatusById(int id, int isDeleted, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE elio_users_favorites 
                                        SET is_deleted = @is_deleted
                                   WHERE id = @id"
                                , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted)
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioCompaniesViewsCompaniesIJCompanies GetCompaniesViewsCompaniesByInterestedCompanyId(int interestedCompanyId, DBSession session)
        {
            DataLoader<ElioCompaniesViewsCompaniesIJCompanies> loader = new DataLoader<ElioCompaniesViewsCompaniesIJCompanies>(session);
            return loader.LoadSingle("Select * from Elio_companies_views_companies where interested_company_id=@interested_company_id", DatabaseHelper.CreateIntParameter("@interested_company_id", interestedCompanyId));
        }

        public static List<ElioPartners> GetUsersPartners(int userId, DBSession session)
        {
            DataLoader<ElioPartners> loader = new DataLoader<ElioPartners>(session);
            return loader.Load("Select * from elio_partners inner join elio_users_partners on elio_users_partners.partner_id=elio_partners.id " +
                               "where user_id=@user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsersPartners GetUsersPartnerProgramsById(int userId, int programId, DBSession session)
        {
            DataLoader<ElioUsersPartners> loader = new DataLoader<ElioUsersPartners>(session);
            return loader.LoadSingle(@"Select * from elio_users_partners 
                                        where user_id = @user_id
                                        and partner_id = @partner_id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateIntParameter("@partner_id", programId));
        }

        public static bool HasUsersPartnerProgram(int userId, string partnerProgram, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(p.id) as count 
                                                    from elio_partners p
                                                    inner join elio_users_partners up
                                                        on up.partner_id = p.id
                                                    where user_id = @user_id
                                                    and p.partner_description = @partner_description"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateStringParameter("@partner_description", partnerProgram));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool HasUsersPartnerProgramByID(int userId, int partnerProgramId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(p.id) as count 
                                                    from elio_users_partners up
                                                    where user_id = @user_id
                                                    and partner_id = @partner_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@partner_id", partnerProgramId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static void UpdateUserPartnerProgram(int vendorId, int resellerId, string tierStatus, DBSession session)
        {
            session.ExecuteQuery(@"update Elio_collaboration_vendors_resellers
                                        set tier_status = @tier_status,
                                        last_updated = getdate()
                                    where master_user_id = @master_user_id
                                    and partner_user_id = @partner_user_id"
                , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId)
                , DatabaseHelper.CreateStringParameter("@tier_status", tierStatus));
        }

        public static string GetUsersPartnerProgramsDescription(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select CONVERT(nvarchar(50),p.partner_description) + ', ' 
                                                     from elio_partners p
                                                     inner join elio_users_partners up 
                                                        on up.partner_id=p.id 
                                                     where user_id = @user_id
                                                     for xml path('')"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (table.Rows.Count > 0) ? table.Rows[0][0].ToString() : "";
        }

        public static ElioUsersIndustries GetUsersIndustriesByIndustryId(int userId, int industryId, DBSession session)
        {
            DataLoader<ElioUsersIndustries> loader = new DataLoader<ElioUsersIndustries>(session);
            return loader.LoadSingle(@"Select * from Elio_users_industries 
                                        where user_id = @user_id
                                        and industry_id = @industry_id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateIntParameter("@industry_id", industryId));
        }

        public static ElioUsersMarkets GetUsersMarketsByMarketId(int userId, int marketId, DBSession session)
        {
            DataLoader<ElioUsersMarkets> loader = new DataLoader<ElioUsersMarkets>(session);
            return loader.LoadSingle(@"Select * from Elio_users_markets 
                                        where user_id = @user_id
                                        and market_id = @market_id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateIntParameter("@market_id", marketId));
        }

        public static ElioUsersApies GetUsersApiesByApiId(int userId, int apiId, DBSession session)
        {
            DataLoader<ElioUsersApies> loader = new DataLoader<ElioUsersApies>(session);
            return loader.LoadSingle(@"Select * from Elio_users_apies 
                                        where user_id = @user_id
                                        and api_id = @api_id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateIntParameter("@api_id", apiId));
        }

        public static string GetUsersSubIndustriesGroupDescription(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select distinct replace(Convert(nvarchar(50),sig.group_description), '&', 'and') + ', '
                                                     from Elio_sub_industries_group sig
                                                     inner join Elio_sub_industries_group_items sigi
	                                                     on sig.id = sigi.sub_industies_group_id
                                                     inner join Elio_users_sub_industries_group_items usigi
	                                                     on usigi.sub_industry_group_item_id = sigi.id
                                                     where user_id = @user_id
                                                     for xml Path('')"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (table.Rows.Count > 0) ? table.Rows[0][0].ToString() : "";
        }

        public static List<ElioApies> GetUsersApies(int userId, DBSession session)
        {
            DataLoader<ElioApies> loader = new DataLoader<ElioApies>(session);
            return loader.Load("Select * from elio_apies inner join elio_users_apies on elio_users_apies.api_id=elio_apies.id " +
                               "where user_id=@user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioPartners> GetPartners(DBSession session)
        {
            DataLoader<ElioPartners> loader = new DataLoader<ElioPartners>(session);
            return loader.Load("Select * from elio_partners where is_public=1");
        }

        public static ElioPartners GetPartnerById(int partnerId, DBSession session)
        {
            DataLoader<ElioPartners> loader = new DataLoader<ElioPartners>(session);
            return loader.LoadSingle(@"Select * from elio_partners where id = @id"
                                     , DatabaseHelper.CreateIntParameter("@id", partnerId));
        }

        public static ElioPartners GetPartnerByDescription(string partnerDescription, DBSession session)
        {
            DataLoader<ElioPartners> loader = new DataLoader<ElioPartners>(session);
            return loader.LoadSingle(@"Select top 1 * from elio_partners where partner_description like '" + partnerDescription + "%'");
        }

        public static List<ElioMarkets> GetMarkets(DBSession session)
        {
            DataLoader<ElioMarkets> loader = new DataLoader<ElioMarkets>(session);
            return loader.Load("Select * from elio_markets where is_public=1");
        }

        public static ElioMarkets GetMarketById(int id, DBSession session)
        {
            DataLoader<ElioMarkets> loader = new DataLoader<ElioMarkets>(session);
            return loader.LoadSingle("Select * from elio_markets where is_public=1 and id = @id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioApies> GetApies(DBSession session)
        {
            DataLoader<ElioApies> loader = new DataLoader<ElioApies>(session);
            return loader.Load("Select * from elio_apies where is_public=1");
        }

        public static ElioApies GetApiById(int apiId, DBSession session)
        {
            DataLoader<ElioApies> loader = new DataLoader<ElioApies>(session);
            return loader.LoadSingle(@"Select * from elio_apies where id = @id"
                                     , DatabaseHelper.CreateIntParameter("@id", apiId));
        }

        public static List<ElioCountries> GetPublicCountries(DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);
            return loader.Load("Select * from elio_countries where is_public=1 order by country_name asc");
        }

        public static List<ElioCountries> GetCountriesByCategoryName(bool isVertical,  string category, DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);

            string strQuery = @"
                                ;with a
                                as
                                (
                                Select distinct country_name
                                from Elio_countries c
                                inner join Elio_users u
	                                on u.country = c.country_name ";

            if (isVertical)
            {
                strQuery += @" inner join Elio_users_sub_industries_group_items usigi
                                    on u.id = usigi.user_id
                                inner join Elio_sub_industries_group_items cat
                                    on cat.id = usigi.sub_industry_group_item_id ";
            }
            else
            {
                strQuery += @" inner join Elio_users_registration_products urp
                                    on urp.user_id = u.id
                                inner join Elio_registration_products cat
                                    on cat.id = urp.reg_products_id ";
            }

            strQuery += @" where u.is_public = 1 
                            and company_type = 'Channel Partners'
                            and account_status = 1 ";

            strQuery += @" and cat.description = @description
                            )
                            select ROW_NUMBER() over(order by country_name) as id,country_name
                            from a
                            order by country_name";

            return loader.Load(strQuery
                            , DatabaseHelper.CreateStringParameter("@description", category));
        }

        public static List<ElioCountries> GetCountriesByCategoryNameNew(bool isVertical, string category, DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);

            string strQuery = @"Select distinct c.Id, country_name
                                from Elio_countries c
                                inner join Elio_users u
	                                on u.country = c.country_name ";

            if (isVertical)
            {
                strQuery += @" inner join Elio_users_sub_industries_group_items usigi
                                    on u.id = usigi.user_id
                                inner join Elio_sub_industries_group_items cat
                                    on cat.id = usigi.sub_industry_group_item_id 
                                    and cat.description = @description";
            }
            else
            {
                strQuery += @" inner join Elio_users_registration_products urp
                                    on urp.user_id = u.id
                                inner join Elio_registration_products cat
                                    on cat.id = urp.reg_products_id 
                                    and cat.description = @description";
            }

            strQuery += @" where u.is_public = 1 
                            and company_type = 'Channel Partners'
                            and account_status = 1 
                            order by country_name";           

            return loader.Load(strQuery
                            , DatabaseHelper.CreateStringParameter("@description", category));
        }

        public static DataTable GetCitiesByCategoryNameTbl(string category, string country, bool isVertical, DBSession session)
        {
            string strQuery = @";with a
                                as
                                (
                                Select distinct city
                                from Elio_countries c
                                inner join Elio_users u
                                on u.country = c.country_name ";

            if (isVertical)
            {
                strQuery += @" inner join Elio_users_sub_industries_group_items usigi
	                                on u.id = usigi.user_id 
                                inner join Elio_sub_industries_group_items cat
	                                on cat.id = usigi.sub_industry_group_item_id ";
            }
            else
            {
                strQuery += @" inner join Elio_users_registration_products urp
                                    on urp.user_id = u.id
                                inner join Elio_registration_products cat
                                    on cat.id = urp.reg_products_id ";
            }

            strQuery += @" where u.is_public = 1 
                        and company_type = 'Channel Partners' 
                        and account_status = 1 
                        and isnull(city, '') != ''
                        and cat.description = @description ";

            strQuery += country != "" ? " and c.country_name = '" + country + "' " : "";

            strQuery += @" )
                            select ROW_NUMBER() over(order by city) as id,city
                            from a
                            order by city";

            return session.GetDataTable(strQuery
                            , DatabaseHelper.CreateStringParameter("@description", category));
        }

        public static List<ElioCities> GetCitiesByCategoryName(string category, string country, string state, bool isVertical, DBSession session)
        {
            DataLoader<ElioCities> loader = new DataLoader<ElioCities>(session);

            string strQuery = @";with a
                                as
                                (
                                Select distinct city
                                from Elio_countries c
                                inner join Elio_users u
                                on u.country = c.country_name ";

            if (isVertical)
            {
                strQuery += @" inner join Elio_users_sub_industries_group_items usigi
	                                on u.id = usigi.user_id 
                                inner join Elio_sub_industries_group_items cat
	                                on cat.id = usigi.sub_industry_group_item_id ";
            }
            else
            {
                strQuery += @" inner join Elio_users_registration_products urp
                                    on urp.user_id = u.id
                                inner join Elio_registration_products cat
                                    on cat.id = urp.reg_products_id ";
            }

            strQuery += @" where u.is_public = 1 
                        and company_type = 'Channel Partners' 
                        and account_status = 1 
                        and isnull(city, '') != ''
                        and cat.description = @description ";

            strQuery += country != "" ? " and c.country_name = '" + country + "' " : "";

            strQuery += state != "" ? " and state = '" + state + "' " : "";

            strQuery += @" )
                            select ROW_NUMBER() over(order by city) as id,city
                            from a
                            order by city";

            return loader.Load(strQuery
                            , DatabaseHelper.CreateStringParameter("@description", category));
        }

        public static string GetRegionCountryByCityTbl(string city, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select top 1 LOWER(REPLACE(REPLACE(c.region, ' ', '-'), '&', 'and') + '/' + REPLACE(REPLACE(c.country_name, ' ', '-'), '&', 'and')) as url
                                                        from Elio_countries c
                                                        inner join Elio_users u
                                                        on u.country = c.country_name  
                                                        and u.company_region = c.region
                                                        where city = @city
                                                        and isnull(c.region, '') != ''
                                                        and isnull(c.country_name, '') != ''"
                                                , DatabaseHelper.CreateStringParameter("@city", city));

            return (table.Rows.Count > 0) ? table.Rows[0]["url"].ToString() : "";
        }

        public static string GetRegionTransCountryByCityTbl(string city, string trans, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select top 1 LOWER(REPLACE(REPLACE(c.region, ' ', '-'), '&', 'and') + '/" + trans + "/' + REPLACE(REPLACE(c.country_name, ' ', '-'), '&', 'and')) as url " +
                                                        "from Elio_countries c " +
                                                        "inner join Elio_users u " +
                                                        "on u.country = c.country_name " +
                                                        "and u.company_region = c.region " +
                                                        "where city = @city " +
                                                        "and isnull(c.region, '') != '' " +
                                                        "and isnull(c.country_name, '') != ''"
                                                , DatabaseHelper.CreateStringParameter("@city", city));

            return (table.Rows.Count > 0) ? table.Rows[0]["url"].ToString() : "";
        }

        public static string GetRegionByCountryTbl(string country, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select region
                                                        from Elio_countries c
                                                        where country_name = @country_name"
                                                , DatabaseHelper.CreateStringParameter("@country_name", country));

            return (table.Rows.Count > 0) ? table.Rows[0][0].ToString() : "";
        }

        public static string GetStateByCountryCityTbl(string country, string city, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select top 1 state
                                                        from elio_users
                                                        where country = @country
                                                        and city = @city
                                                        and isnull(state, '') != ''"
                                                , DatabaseHelper.CreateStringParameter("@country", country)
                                                , DatabaseHelper.CreateStringParameter("@city", city));

            return (table.Rows.Count > 0) ? table.Rows[0][0].ToString() : "";
        }

        public static string GetStateByRegionCountryCityTbl(string region, string country, string city, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select top 1 state
                                                        from elio_users
                                                        where country = @country
                                                        and city = @city
                                                        and company_region = @company_region
                                                        and isnull(state, '') != ''"
                                                , DatabaseHelper.CreateStringParameter("@country", country)
                                                , DatabaseHelper.CreateStringParameter("@city", city)
                                                , DatabaseHelper.CreateStringParameter("@company_region", region));

            return (table.Rows.Count > 0) ? table.Rows[0][0].ToString() : "";
        }

        public static List<ElioCountries> GetPublicCountriesOverId(int id, DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);
            return loader.Load("Select * from elio_countries where is_public = 1 and id > @id order by id asc"
                            , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static string GetRegionByCountryId(int countryId, DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);
            ElioCountries country = loader.LoadSingle("Select region from elio_countries where id = @id"
                            , DatabaseHelper.CreateIntParameter("@id", countryId));

            return (country != null) ? country.Region : "";
        }

        public static DataTable GetRegionsFromCountries(DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT distinct region, + REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and') + '/channel-partners' as link
                                                      FROM Elio_countries
                                                      where is_public = 1
                                                      order by region");

            return (table.Rows.Count > 0) ? table : null;
        }

        public static string GetRegionByCountry(string countryName, DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);
            ElioCountries country = loader.LoadSingle("Select region from Elio_countries where country_name = @country_name"
                                            , DatabaseHelper.CreateStringParameter("@country_name", countryName));

            return (country != null) ? country.Region : "";
        }

        public static List<ElioUsers> GetPublicUsersByType(string companyType, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"Select * 
                                    from elio_users 
                                    where is_public=1 
                                    and account_status = 1 
                                    and company_type = @company_type 
                                    order by id asc"
                                , DatabaseHelper.CreateStringParameter("@company_type", companyType));
        }

        public static List<ElioUsers> GetPublicUsersByTypeAndRowNumber(string companyType, int from, int to, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@";with a
                                    as
                                    (
                                        Select ROW_NUMBER() over (order by id) as row_index,u.id as user_id 
                                        from elio_users u
                                        where is_public=1 
                                        and account_status = 1 
                                        and company_type = @company_type
                                    )

                                    select user_id as id
                                    from a
                                    where row_index between " + from + " and " + to + ""
                                , DatabaseHelper.CreateStringParameter("@company_type", companyType));
        }

        public static List<ElioUsers> GetPublicElioUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select * from elio_users where is_public=1 and account_status = 1 and user_application_type = 1 order by id asc");
        }

        public static List<ElioCountries> GetRegionsByCountryId(int id, DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);
            return loader.Load("Select * from elio_countries where id = @id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioCountries GetCountryById(int id, DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);
            return loader.LoadSingle("Select * from elio_countries where id = @id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static DataTable GetCountriesByRegionTbl(string region, DBSession session)
        {
            return session.GetDataTable(@"SELECT distinct id
    ,                                           country_name, '/' + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/' + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(country_name), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/channel-partners' as link
                                            FROM Elio_countries
                                            where is_public = 1
                                            and region = @region
                                            order by country_name"
                                        , DatabaseHelper.CreateStringParameter("@region", region));
        }

        public static DataTable GetCitiesByRegionCountryTbl(string region, string country, DBSession session)
        {
            string strQuery = @"select distinct city,";

            strQuery += "LOWER('/' + replace(replace(@region, ' ', '-'), '&', 'and') + '/";

            //if (country == "France")
            //    strQuery += "fr/";
            //else if (country == "Canada")
            //    strQuery += "ca/";
            //else if (country == "Spain")
            //    strQuery += "es/";
            //else if (country == "Germany")
            //    strQuery += "de/";
            //else if (country == "Portugal" || country == "Brazil")
            //    strQuery += "pt/";
            //else if (country == "Austria")
            //    strQuery += "at/";
            //else if (country == "Italy")
            //    strQuery += "it/";
            //else if (country == "Argentina" || country == "Bolivia" || country == "Chile"
            //    || country == "Colombia" || country == "Costa Rica" || country == "Dominican Republic"
            //    || country == "Ecuador" || country == "El Salvador" || country == "Guatemala"
            //    || country == "Honduras" || country == "Mexico" || country == "Panama"
            //    || country == "Paraguay" || country == "Peru" || country == "Puerto Rico"
            //    || country == "Uruguay" || country == "Venezuela")
            //    strQuery += "la/";
            //else if (country == "Netherlands")
            //    strQuery += "nl/";
            //else if (country == "Poland")
            //    strQuery += "pl/";

            strQuery += @"' + replace(replace(@country, ' ', '-'), '&', 'and') + '/' + replace(replace(RTRIM(city), ' ', '-'), '&', 'and') + '/channel-partners') as link
                        from elio_users u
                        where country = @country
                        and company_region = @region
                        and company_type = 'Channel Partners'
                        and u.is_public = 1 
                        and u.account_status = 1
                        and isnull(city, '') != ''
                        order by city";

            return session.GetDataTable(strQuery
                                        , DatabaseHelper.CreateStringParameter("@country", country)
                                        , DatabaseHelper.CreateStringParameter("@region", region));

            /*@"select distinct city,
                LOWER('/' + replace(replace(@region, ' ', '-'), '&', 'and') + '/' + replace(replace(@country, ' ', '-'), '&', 'and') + '/' + replace(replace(city, ' ', '-'), '&', 'and') + '/channel-partners') as link
                from elio_users u
                where country = @country
                and company_region = @region
                and company_type = 'Channel Partners'
                and u.is_public = 1 
                and u.account_status = 1
                order by city"
            */
        }

        public static DataTable GetCitiesByRegionCountryStateTbl(string country, string state, DBSession session)
        {
            string strQuery = @"select distinct city,
                                LOWER('/' + replace(replace(RTRIM(company_region), ' ', '-'), '&', 'and') + '/' + replace(replace(RTRIM(country), ' ', '-'), '&', 'and') + '/' + replace(replace(RTRIM(state), ' ', '-'), '&', 'and') + '/' + replace(replace(RTRIM(city), ' ', '-'), '&', 'and') + '/channel-partners') as link
                                from elio_users u
                                where country = @country
                                and company_type = 'Channel Partners'
                                and u.is_public = 1 
                                and u.account_status = 1
                                and isnull(city, '') != ''
                                and state = @state
                                order by city";

            return session.GetDataTable(strQuery
                                        , DatabaseHelper.CreateStringParameter("@country", country)
                                        , DatabaseHelper.CreateStringParameter("@state", state));
        }

        public static DataTable GetStatesByCountryTbl(string country, DBSession session)
        {
            string strQuery = @"select distinct state,
                                LOWER('/' + replace(replace(RTRIM(company_region), ' ', '-'), '&', 'and') + '/' + replace(replace(RTRIM(country), ' ', '-'), '&', 'and') + '/' + replace(replace(RTRIM(state), ' ', '-'), '&', 'and') + '/channel-partners') as link
                                from elio_users u
                                where country = @country
                                and company_type = 'Channel Partners'
                                and u.is_public = 1 
                                and u.account_status = 1
                                and isnull(state, '') != ''
                                order by state";

            return session.GetDataTable(strQuery
                                        , DatabaseHelper.CreateStringParameter("@country", country));
        }

        public static List<ElioCountries> GetCountriesByRegion(string region, DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);
            return loader.Load("Select * from elio_countries where region = @region", DatabaseHelper.CreateStringParameter("@region", region));
        }

        public static DataTable GetDtPublicCountries(DBSession session)
        {
            return session.GetDataTable("Select * from elio_countries where is_public=1 order by country_name asc");
        }

        public static bool ExistEmail(string email, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where email = @email", DatabaseHelper.CreateStringParameter("@email", email));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool IsBlackListedDomain(string email, DBSession session)
        {
            string[] emailParts = email.Split('@').ToArray();
            if (emailParts.Length > 1)
            {
                string domain = emailParts[1];
                if (domain != "")
                {
                    DataTable table = session.GetDataTable(@"select Count(id) as count 
                                                            from Elio_blacklisted_domains with (nolock) 
                                                            where domain = @domain
                                                            and is_black_listed = 1"
                                                    , DatabaseHelper.CreateStringParameter("@domain", domain));

                    return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static bool IsBlackListedEmail(string email, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count 
                                                            from Elio_blacklisted_emails with (nolock) 
                                                            where email = @email
                                                            and is_black_listed = 1"
                                            , DatabaseHelper.CreateStringParameter("@email", email));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool IsRegisteredEmail(string email, out int userApplicationType, out bool isFullRegistered, DBSession session)
        {
            userApplicationType = (int)UserApplicationType.NotRegistered;
            isFullRegistered = false;

            //DataTable table = session.GetDataTable(@"SELECT Count(id) as count FROM Elio_users WITH (nolock) WHERE email = @email", DatabaseHelper.CreateStringParameter("@email", email));
            DataTable table = session.GetDataTable(@"
                                                    SELECT 
	                                                    cast
		                                                    (
			                                                    case when account_status = 0 then 0 else 1 end as bit
		                                                    ) as full_registered
		                                                    , email 
                                                            , user_application_type
                                                    FROM Elio_users
                                                    WHERE email = @email
                                                    group by  account_status, email, user_application_type
                                                    "
                                                    , DatabaseHelper.CreateStringParameter("@email", email));

            if (table.Rows.Count > 0)
            {
                userApplicationType = Convert.ToInt32(table.Rows[0]["user_application_type"]);
                isFullRegistered = (Convert.ToInt32(table.Rows[0]["full_registered"]) == 0) ? false : true;
                return true;
            }

            return false;
        }

        public static bool ExistEmailToSubAccount(int userId, string email, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count from Elio_users_sub_accounts with (nolock) 
                                                    where user_id = @user_id
                                                    and email = @email"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateStringParameter("@email", email));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistEmailToOtherSubAccount(int userId, string email, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count from Elio_users_sub_accounts with (nolock) 
                                                    where user_id <> @user_id
                                                    and email = @email"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateStringParameter("@email", email));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool HasSubAccount(string email, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count from Elio_users_sub_accounts with (nolock) 
                                                    where email = @email"
                                                    , DatabaseHelper.CreateStringParameter("@email", email));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool HasSubAccountsPending(int userId, int isConfirmed, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count from Elio_users_sub_accounts with (nolock) 
                                                    where user_id = @user_id
                                                    and account_status = 0 
                                                    and is_confirmed = @is_confirmed
                                                    and is_active = 1"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                , DatabaseHelper.CreateIntParameter("@is_confirmed", isConfirmed));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool IsEmailRegistered(string email, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where (email=@email or official_email=@official_email)"
                                                   , DatabaseHelper.CreateStringParameter("@email", email)
                                                   , DatabaseHelper.CreateStringParameter("@official_email", email));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool IsUserEmailRegistered(int userId, string email, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_user_emails with (nolock) where user_id=@user_id and email=@email"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@email", email));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool IsDomainRegistered(string website, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where website like '%" + website.Trim() + "'"
                                                   , DatabaseHelper.CreateStringParameter("@website", website));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserEmailToOtherUser(int otherUserId, string email, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_user_emails with (nolock) where email=@email and user_id<>@user_id"
                                                   , DatabaseHelper.CreateStringParameter("@email", email)
                                                   , DatabaseHelper.CreateIntParameter("user_id", otherUserId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistEmailToOtherUser(string email, int otherUserId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where email=@email and id<>@id"
                                                   , DatabaseHelper.CreateStringParameter("@email", email)
                                                   , DatabaseHelper.CreateIntParameter("id", otherUserId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistCompanyName(string companyName, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where company_name=@company_name"
                                                   , DatabaseHelper.CreateStringParameter("@company_name", companyName));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool IsCompanyNameRegisteredByCountry(string companyName, string country, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where company_name = @company_name and country = @country"
                                                   , DatabaseHelper.CreateStringParameter("@company_name", companyName)
                                                   , DatabaseHelper.CreateStringParameter("@country", country));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistEmailToOtherBillingAccount(string email, int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_billing_account with (nolock) where billing_email=@billing_email and user_id<>@user_id"
                                                   , DatabaseHelper.CreateStringParameter("@billing_email", email)
                                                   , DatabaseHelper.CreateIntParameter("user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUsername(string username, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where username=@username", DatabaseHelper.CreateStringParameter("@username", username));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUsernameToOtherUser(string username, int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where username=@username and id<>@id"
                                                   , DatabaseHelper.CreateStringParameter("@username", username)
                                                   , DatabaseHelper.CreateIntParameter("id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserIndustry(int userId, int industryId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_industries where user_id=@user_id and industry_id=@industry_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@industry_id", industryId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserSubIndustryGroupItem(int userId, int subIndustryGroupItemId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_sub_industries_group_items where user_id=@user_id and sub_industry_group_item_id=@sub_industry_group_item_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@sub_industry_group_item_id", subIndustryGroupItemId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static void DeleteCommunityUserEmailNotification(int id, int userId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_community_user_email_notifications " +
                                 "where community_email_notifications_id=@community_email_notifications_id " +
                                 "and user_id=@user_id ", DatabaseHelper.CreateIntParameter("@community_email_notifications_id", id)
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static bool ExistCommunityUserEmailNotification(int userId, int emailNotificationId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_community_user_email_notifications where user_id=@user_id and community_email_notifications_id=@community_email_notifications_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@community_email_notifications_id", emailNotificationId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserEmailNotificationsSettingsById(int userId, int emailNotificationId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count 
                                                    from Elio_user_email_notifications_settings 
                                                    where user_id = @user_id 
                                                    and email_notifications_id = @email_notifications_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@email_notifications_id", emailNotificationId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserEmailNotificationsSettingsByUser(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count 
                                                    from Elio_user_email_notifications_settings 
                                                    where user_id = @user_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static void DeleteUserEmailNotificationSettings(int id, int userId, DBSession session)
        {
            session.GetDataTable(@"Delete from Elio_user_email_notifications_settings
                                     where email_notifications_id = @email_notifications_id
                                     and user_id = @user_id"
                                 , DatabaseHelper.CreateIntParameter("@email_notifications_id", id)
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void DeleteUserEmailNotificationSettingsAll(int userId, DBSession session)
        {
            session.GetDataTable(@"Delete from Elio_user_email_notifications_settings
                                     where user_id = @user_id"
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUserEmailNotificationsSettings> GetUserEmailNotificationSettings(int userId, DBSession session)
        {
            DataLoader<ElioUserEmailNotificationsSettings> loader = new DataLoader<ElioUserEmailNotificationsSettings>(session);

            return loader.Load(@"Select * from Elio_user_email_notifications_settings
                                     where user_id = @user_id"
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static bool ExistUserPartner(int userId, int partnerId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_partners where user_id=@user_id and partner_id=@partner_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@partner_id", partnerId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserMarket(int userId, int marketId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_markets where user_id=@user_id and market_id=@market_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@market_id", marketId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserApi(int userId, int apiId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_apies where user_id=@user_id and api_id=@api_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@api_id", apiId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }




        public static List<ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers> GetUserSubcategoriesById(int userId, DBSession session)
        {
            DataLoader<ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers> loader = new DataLoader<ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers>(session);
            return loader.Load(@"select 
	                                A.id, 
	                                A.user_id, 
	                                A.sub_industry_group_item_id, 
	                                B.sub_industies_group_id, 
	                                B.description,
	                                C.company_name, 
	                                C.email, 
	                                C.official_email, 
	                                C.website, 
	                                C.country 
                                from Elio_users_sub_industries_group_items as A 
                                inner join Elio_sub_industries_group_items as B 
	                                on A.sub_industry_group_item_id = B.id 
                                inner join elio_users as C 
	                                on A.user_id = C.id
                                where A.user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersSubIndustriesGroupItems> GetUsersBySubcategoryId(string subcategoriesId, DBSession session)
        {
            DataLoader<ElioUsersSubIndustriesGroupItems> loader = new DataLoader<ElioUsersSubIndustriesGroupItems>(session);
            return loader.Load("select distinct user_id from Elio_users_sub_industries_group_items where sub_industry_group_item_id in (" + subcategoriesId + ")");
        }

        public static List<ElioUsersSubIndustriesGroupItems> GetUserProfileSubcategoriesId(int userId, DBSession session)
        {
            DataLoader<ElioUsersSubIndustriesGroupItems> loader = new DataLoader<ElioUsersSubIndustriesGroupItems>(session);
            return loader.Load("select * from Elio_users_sub_industries_group_items where user_id=@user_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsers> GetOpportunitiesByCompanyType(string usersId, string companyType, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("select * from elio_users where id in (" + usersId + ") and company_type=@company_type"
                                                        , DatabaseHelper.CreateStringParameter("@company_type", companyType));
        }

        public static List<ElioUsers> GetPublicFullRegisteredUsersJoinCustomersAPI(AccountPublicStatus publicStatus, AccountStatus status, DBSession session)
        {
            string top = "20";
            if (ConfigurationManager.AppSettings["top"].ToString() != null)
                top = ConfigurationManager.AppSettings["top"].ToString();

            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"select top " + top + "  * from elio_users eu where 1 = 1 and is_public = 1 and account_status = 1 and id not in (select user_id from Elio_users_customers_custom eucc where exist_in_customers = 1) and id <=" + top
                                , DatabaseHelper.CreateIntParameter("@is_public", (int)publicStatus)
                                , DatabaseHelper.CreateIntParameter("@account_status", (int)status));
        }

        public static int GetPossibleOpportunitiesForUserBySubCategoriesIdAndCompanyType(string subCategoriesId, string companyType, DBSession session)
        {
            DataTable table = session.GetDataTable("select COUNT( distinct user_id) as count " +
                                                   "from Elio_users_sub_industries_group_items " +
                                                   "inner join Elio_users on Elio_users.id=Elio_users_sub_industries_group_items.user_id " +
                                                   "where sub_industry_group_item_id in (" + subCategoriesId + ") " +
                                                   "and company_type=@company_type"
                                                   , DatabaseHelper.CreateStringParameter("@company_type", companyType));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetUserPossibleMachesByCompanyType(int userId, string companyType, DBSession session)
        {
            DataTable table = session.GetDataTable("select count(distinct user_id) as count " +
                                                    "from Elio_users_sub_industries_group_items " +
                                                    "inner join Elio_users on Elio_users.id=Elio_users_sub_industries_group_items.user_id " +
                                                    "where sub_industry_group_item_id in( " +
                                                                                            "select sub_industry_group_item_id " +
                                                                                            "from Elio_users_sub_industries_group_items " +
                                                                                            "where user_id=@user_id) " +
                                                                                            "and company_type=@company_type"
                                                                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                                                            , DatabaseHelper.CreateStringParameter("@company_type", companyType));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static bool HasUserVerticals(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("select COUNT (id) as count " +
                                                    "from Elio_users_sub_industries_group_items " +
                                                    "where user_id=@user_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static List<ElioUsersAlgorithmSubcategories> GetUserAlgorithmSubcategoriesById(int userId, DBSession session)
        {
            DataLoader<ElioUsersAlgorithmSubcategories> loader = new DataLoader<ElioUsersAlgorithmSubcategories>(session);
            return loader.Load("select * from Elio_users_algorithm_subcategories where user_id=@user_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<int> GetUserAlgorithmSubcategoriesId(int userId, DBSession session)
        {
            List<int> subCategoriesId = new List<int>();

            DataLoader<ElioUsersAlgorithmSubcategories> loader = new DataLoader<ElioUsersAlgorithmSubcategories>(session);
            List<ElioUsersAlgorithmSubcategories> subCategories = loader.Load("select * from Elio_users_algorithm_subcategories where user_id=@user_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));

            foreach (ElioUsersAlgorithmSubcategories subCategory in subCategories)
            {
                subCategoriesId.Add(subCategory.SubcategoryId);
            }

            return subCategoriesId;
        }

        public static List<ElioUsersAlgorithmSubcategories> GetUniqueUsersAlgorithmSubcategories(DBSession session)
        {
            DataLoader<ElioUsersAlgorithmSubcategories> loader = new DataLoader<ElioUsersAlgorithmSubcategories>(session);
            return loader.Load("select distinct(user_id) from Elio_users_algorithm_subcategories");
        }

        public static List<ElioSubcategoriesIJSubcategoriesGroups> GetUserSubIndustriesGroupItemsBySubIndustryGroupId(int userId, int subIndustryGroupId, DBSession session)
        {
            DataLoader<ElioSubcategoriesIJSubcategoriesGroups> loader = new DataLoader<ElioSubcategoriesIJSubcategoriesGroups>(session);
            return loader.Load(@"select user_id, description, sub_industry_group_item_id, sub_industry_group_id, group_description from Elio_users_sub_industries_group_items inner join 
                                    Elio_sub_industries_group_items on Elio_users_sub_industries_group_items.sub_industry_group_item_id = Elio_sub_industries_group_items.id 
                                    inner join Elio_sub_industries_group on Elio_sub_industries_group_items.sub_industies_group_id = Elio_sub_industries_group.id
                                    where user_id = @user_id
                                    and sub_industry_group_id = @sub_industry_group_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@sub_industry_group_id", subIndustryGroupId));
        }

        public static List<ElioSubcategoriesIJSubcategoriesGroups> GetUserSubIndustryGroups(int userId, int subIndustryGroupId, DBSession session)
        {
            DataLoader<ElioSubcategoriesIJSubcategoriesGroups> loader = new DataLoader<ElioSubcategoriesIJSubcategoriesGroups>(session);
            return loader.Load(@"select user_id, description, sub_industry_group_item_id, sub_industry_group_id, group_description from Elio_users_sub_industries_group_items inner join 
                                    Elio_sub_industries_group_items on Elio_users_sub_industries_group_items.sub_industry_group_item_id = Elio_sub_industries_group_items.id 
                                    inner join Elio_sub_industries_group on Elio_sub_industries_group_items.sub_industies_group_id = Elio_sub_industries_group.id
                                    where user_id = @user_id
                                    and sub_industry_group_id = @sub_industry_group_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@sub_industry_group_id", subIndustryGroupId));
        }

        public static ElioSubIndustriesGroupItems GetSubcategoryById(int subcategoryId, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.LoadSingle("select * from Elio_sub_industries_group_items where id=@subcategory_id"
                                                        , DatabaseHelper.CreateIntParameter("@subcategory_id", subcategoryId));
        }

        public static List<ElioSubIndustriesGroupItems> GetGroupItemsBySubIndustryGroupId(int subIndustryGroupId, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.Load("select * from Elio_sub_industries_group_items where sub_industies_group_id=@sub_industies_group_id"
                                                        , DatabaseHelper.CreateIntParameter("@sub_industies_group_id", subIndustryGroupId));
        }

        public static bool ExistUserSubIndustriesGroupItemId(int userId, int subIndustryGroupItemId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select count(id) as count 
                                                        from Elio_users_sub_industries_group_items 
                                                        where user_id = @user_id 
                                                        and sub_industry_group_item_id = @sub_industry_group_item_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                        , DatabaseHelper.CreateIntParameter("@sub_industry_group_item_id", subIndustryGroupItemId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static int ExistUserSubIndustriesGroupItemIdByDescription(int userId, string subIndustryGroupItemDescription, out bool exist, DBSession session)
        {
            exist = false;
            int subIndustryGroupItemId = 0;

            DataTable table = session.GetDataTable(@"select id
	                                                    from Elio_sub_industries_group_items
	                                                    where 1 = 1
	                                                    and description = @description"
                                        , DatabaseHelper.CreateStringParameter("@description", subIndustryGroupItemDescription));

            if (table.Rows.Count > 0 && Convert.ToInt32(table.Rows[0]["id"]) > 0)
            {
                subIndustryGroupItemId = Convert.ToInt32(table.Rows[0]["id"]);

                DataTable table2 = session.GetDataTable(@"select count(id) as count 
                                                        from Elio_users_sub_industries_group_items
                                                        where user_id = @user_id
                                                        and sub_industry_group_item_id = @sub_industry_group_item_id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@sub_industry_group_item_id", subIndustryGroupItemId));

                exist = (table2.Rows.Count > 0 && Convert.ToInt32(table2.Rows[0]["count"]) > 0) ? true : false;
            }

            return subIndustryGroupItemId;
        }

        public static void FixUserSubIndustriesGroupItemIdByDescription(int userId, string subIndustryGroupItemDescription, int subIndustryGroupId, string insert, DBSession session)
        {
            int subIndustriesGroupItemId = 0;

            DataTable table = session.GetDataTable(@"select id
	                                                    from Elio_sub_industries_group_items
	                                                    where 1 = 1
	                                                    and description = @description"
                                        , DatabaseHelper.CreateStringParameter("@description", subIndustryGroupItemDescription));

            if (table.Rows.Count > 0 && Convert.ToInt32(table.Rows[0]["id"]) > 0)
            {
                subIndustriesGroupItemId = Convert.ToInt32(table.Rows[0]["id"]);

                bool exist = ExistUserSubIndustriesGroupItemId(userId, subIndustriesGroupItemId, session);

                if (exist)
                {
                    if (insert == "0")
                    {
                        DeleteUserSubIndustryGroupItem(userId, subIndustriesGroupItemId, session);
                    }
                }
                else
                {
                    if (insert == "1")
                    {
                        ElioUsersSubIndustriesGroupItems newVertical = new ElioUsersSubIndustriesGroupItems();

                        newVertical.UserId = userId;
                        newVertical.SubIndustryGroupId = subIndustryGroupId;
                        newVertical.SubIndustryGroupItemId = subIndustriesGroupItemId;

                        DataLoader<ElioUsersSubIndustriesGroupItems> loader = new DataLoader<ElioUsersSubIndustriesGroupItems>(session);
                        loader.Insert(newVertical);
                    }
                }
            }
        }

        public static List<ElioUsers> GetPremiumVendorsWithSameSubIndustriesGroupItems(int resellerId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"select distinct u.id, u.company_logo, u.company_name, u.website, u.email, u.first_name, u.last_name
                                    from dbo.Elio_users_sub_industries_group_items usigi1
                                    inner join elio_users u
	                                    on u.id = usigi1.user_id
                                    where usigi1.sub_industry_group_item_id
                                    in 
                                    (
	                                    select usigi.sub_industry_group_item_id 
	                                    from Elio_users_sub_industries_group_items usigi
	                                    where usigi.user_id = @user_id
                                    )
                                    and u.company_type = 'Vendors'
                                    and u.billing_type > 1"
                                , DatabaseHelper.CreateIntParameter("@user_id", resellerId));
        }

        public static List<ElioUsers> GetUsersWithSameSubIndustriesGroupItemsByCompanyType(int userId, string companyType, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"select distinct u.id, u.company_logo, u.company_name, u.website, u.email, u.first_name, u.last_name
                                    from dbo.Elio_users_sub_industries_group_items usigi1
                                    inner join elio_users u
	                                    on u.id = usigi1.user_id
                                    where usigi1.sub_industry_group_item_id
                                    in 
                                    (
	                                    select usigi.sub_industry_group_item_id 
	                                    from Elio_users_sub_industries_group_items usigi
	                                    where usigi.user_id = @user_id
                                    )
                                    and u.company_type = @company_type"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateStringParameter("@company_type", companyType));
        }

        public static List<ElioUsersSearchInfo> GetUsersWithSameSubIndustriesGroupItemsByCompanyTypeDataTable(ElioUsers user, DBSession session)
        {
            DataLoader<ElioUsersSearchInfo> loader = new DataLoader<ElioUsersSearchInfo>(session);

            string strQuery = "";
            int topUsers = 20;
            string excludeIds = user.Id.ToString();

            List<ElioUsersSearchInfo> users = null;
            List<ElioUsersSearchInfo> usersTotal = new List<ElioUsersSearchInfo>();

            if (user.CompanyType == Types.Vendors.ToString())
            {
                for (int i = 0; i < 4; i++)
                {
                    strQuery = @"Select distinct top " + topUsers + " (u.id) as id,u.company_name " +
                            ", case when isnull(u.company_logo, '') != '' then replace(u.company_logo, '~/', '/')" +
                            " else '/images/no_logo_company.png' end as company_logo " +
                            ", u.country" +
                            ", u.billing_type " +
                            ", u.company_type " +
                            ", u.company_region " +
                            ", u.city " +
                            ", case when u.overview !='' then " +
                            "case " +
                            "when LEN(u.overview) > 1000 then SUBSTRING(u.overview, 0, 1000) + ' ...' " +
                            "else " +
                            "u.overview " +
                            "end " +
                            "else " +
                            "'' end as overview " +
                            ",'/profiles/vendors/' + CAST(u.id as nvarchar(50)) + '/' + LOWER(REPLACE(TRIM(u.company_name), ' ', '-')) as profiles " +
                            ",'/' + LOWER(REPLACE(TRIM(u.company_name), ' ', '-')) + '/partner-free-sign-up' as partner_portal " +
                            ", u.sysdate " +
                            @" from dbo.Elio_users_sub_industries_group_items usigi1 
                            inner join elio_users u 
                            on u.id = usigi1.user_id 
                            inner join elio_users u1 
                            on u1.id = @user_id 
                            where usigi1.sub_industry_group_item_id 
                            in 
                            ( 
                            select usigi.sub_industry_group_item_id 
                            from Elio_users_sub_industries_group_items usigi 
                            where usigi.user_id = @user_id 
                            ) 
                            and u.company_type = @company_type 
                            and u.is_public = 1 
                            and u.id NOT IN (" + excludeIds + ") ";

                    if (i <= 2)
                        strQuery += (!string.IsNullOrEmpty(user.CompanyRegion)) ? "and u.company_region = u1.company_region " : "";

                    if (i <= 1)
                        strQuery += (!string.IsNullOrEmpty(user.Country)) ? "and u.country = u1.country " : "";

                    if (i == 0)
                        strQuery += (!string.IsNullOrEmpty(user.City)) ? "and u.city = u1.city " : "";

                    strQuery += "GROUP BY u.id, u.billing_type, u.company_type,u.company_logo,u.country,u.company_name,u.overview,u.company_region,u.city,u.sysdate " +
                                "ORDER BY u.billing_type desc, u.company_region, u.country, u.city, u.id desc";

                    users = loader.Load(strQuery
                                         , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                         , DatabaseHelper.CreateStringParameter("@company_type", user.CompanyType));

                    if (i == 0 && users.Count >= 10)
                        return users;

                    if (users.Count > 0 && topUsers > 0)
                    {
                        foreach (ElioUsersSearchInfo item in users)
                        {
                            usersTotal.Add(item);
                            excludeIds += "," + item.Id.ToString();
                        }

                        topUsers = 20 - usersTotal.Count;
                    }

                    if (topUsers <= 0 || usersTotal.Count > 10)
                        break;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    strQuery = @"Select distinct top " + topUsers + " (u.id) " +
                        ",u.company_name " +
                        ", case when isnull(u.company_logo, '') != '' then replace(u.company_logo, '~/', '/') " +
                        "else '/images/no_logo_company.png' end as company_logo " +
                        ",u.user_application_type " +
                        ",u.company_region " +
                        ",u.country " +
                        ",u.city " +
                        ",case when u.city != '' then u.city else '-' end as  city " +
                        ",u.webSite " +
                        ",u.billing_type " +
                        ",u.company_type " +
                        ",case when u.overview !='' then " +
                        "case when LEN(u.overview) > 1000 then SUBSTRING(u.overview, 0, 1000) + ' ...' " +
                        "else u.overview end else '' end as overview " +
                        @",'/profiles/channel-partners/' + CAST(u.id as nvarchar(50)) + '/' + LOWER(REPLACE(TRIM(u.company_name), ' ', '-')) as profiles 
                        , u.sysdate 
                        from dbo.Elio_users_sub_industries_group_items usigi1 
                        inner join elio_users u 
                        on u.id = usigi1.user_id 
                        inner join elio_users u1 
                        on u1.id = @user_id 
                        where usigi1.sub_industry_group_item_id 
                        in 
                        ( 
                        select usigi.sub_industry_group_item_id 
                        from Elio_users_sub_industries_group_items usigi 
                        where usigi.user_id = @user_id 
                        ) 
                        and u.company_type = @company_type 
                        and u.is_public = 1 
                        and u.id NOT IN (" + excludeIds + ") ";

                    if (i <= 2)
                        strQuery += (!string.IsNullOrEmpty(user.CompanyRegion)) ? "and (u.company_region != '' and u.company_region = '" + user.CompanyRegion + "')" : "";

                    if (i <= 1)
                        strQuery += (!string.IsNullOrEmpty(user.Country)) ? "and (u.country != '' and u.country = '" + user.Country + "')" : "";

                    if (i == 0)
                        strQuery += (!string.IsNullOrEmpty(user.City)) ? "and (u.city != '' and u.city = '" + user.City + "')" : "";

                    strQuery += "GROUP BY u.id, u.company_name, u.company_logo, u.user_application_type, u.billing_type, u.company_region, u.country, u.city, u.webSite, u.company_type, u.overview, u.sysdate " +
                                "ORDER BY billing_type desc, u.company_region, u.country, u.city, u.id desc";

                    users = loader.Load(strQuery
                                    , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                                    , DatabaseHelper.CreateStringParameter("@company_type", user.CompanyType));

                    if (i == 0 && users.Count >= 10)
                        return users;

                    if (users.Count > 0 && topUsers > 0)
                    {
                        foreach (ElioUsersSearchInfo item in users)
                        {
                            usersTotal.Add(item);
                            excludeIds += "," + item.Id.ToString();
                        }

                        topUsers = 20 - usersTotal.Count;
                    }

                    if (topUsers <= 0 || usersTotal.Count > 10)
                        break;
                }
            }

            return usersTotal;
        }

        public static DataTable GetUsersWithSpecificSubIndustriesGroupItemsByCompanyTypeDataTable(int userId, string companyType, DBSession session)
        {           
            return session.GetDataTable(@"select distinct top 20 u.id, NEWID()
                                            from dbo.Elio_users_sub_industries_group_items usigi1
                                            inner join elio_users u
	                                            on u.id = usigi1.user_id
                                            where usigi1.sub_industry_group_item_id
                                            in 
                                            (
	                                            select usigi.sub_industry_group_item_id 
	                                            from Elio_users_sub_industries_group_items usigi
	                                            where usigi.user_id = @user_id
                                                and usigi.sub_industry_group_item_id in (62,75,76,77,79,122)    --Virtualization,Data Security,Vulnerability Management,Firewall,Backup & Restore,Application Security
                                            )
                                            and u.company_type = @company_type
                                            and u.is_public = 1
                                            and u.id not in (" + userId + ") " +
                                            "ORDER BY NEWID()"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateStringParameter("@company_type", companyType));
        }

        public static DataTable GetUsersWithSameProductsByCompanyTypeDataTable(int userId, string companyType, DBSession session)
        {
            return session.GetDataTable(@"select distinct top 20 u.id, NEWID()
                                            from Elio_users_registration_products urp1
                                            inner join elio_users u
	                                            on u.id = urp1.user_id
                                            where urp1.reg_products_id
                                            in 
                                            (
	                                            select urp.reg_products_id 
	                                            from Elio_users_registration_products urp
	                                            where urp.user_id = @user_id
                                            )
                                            and u.company_type = @company_type
                                            and u.is_public = 1
                                            and u.id not in (" + userId + ") " +
                                            "ORDER BY NEWID()"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateStringParameter("@company_type", companyType));
        }

        public static bool HasUserCriteria(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("select COUNT(id) as count from Elio_users_criteria where user_id=@userId"
                                                        , DatabaseHelper.CreateIntParameter("@userId", userId));

            return (Convert.ToInt32(table.Rows[0]["count"]) == 0) ? false : true;
        }

        public static bool ExistUserAlgorithmSubcategory(int userId, int subcategoryId, DBSession session)
        {
            DataTable table = session.GetDataTable("select COUNT(id) as count from Elio_users_algorithm_subcategories where user_id=@user_id and subcategory_id=@subcategory_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@subcategory_id", subcategoryId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistMatch(int userId, int matchId, DBSession session)
        {
            DataTable table = session.GetDataTable("select COUNT(id) as count from Elio_algorithm_matches where user_id=@user_id and match_id=@match_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@match_id", matchId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserAlgorithmCriteria(int userId, int criteriaId, DBSession session)
        {
            DataTable table = session.GetDataTable("select COUNT(id) as count from Elio_users_criteria where user_id=@user_id and criteria_id=@criteria_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@criteria_id", criteriaId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserAlgorithmCriteriaValue(int userId, int criteriaId, string criteriaValue, DBSession session)
        {
            DataTable table = session.GetDataTable("select COUNT(id) as count from Elio_users_criteria where user_id=@user_id and criteria_id=@criteria_id and criteria_value=@criteria_value"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@criteria_id", criteriaId)
                                                    , DatabaseHelper.CreateStringParameter("@criteria_value", criteriaValue));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool HasUserMSPsProgram(int userId, int programId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select COUNT(id) as count
                                                        FROM Elio_users_partners
                                                        where partner_id = @partner_id
                                                        and user_id = @user_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@partner_id", programId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }


        public static ElioUsersCriteria GetUserAlgorithmCriteriaValue(int userId, int criteriaId, string criteriaValue, DBSession session)
        {
            DataLoader<ElioUsersCriteria> loader = new DataLoader<ElioUsersCriteria>(session);
            return loader.LoadSingle("Select * from Elio_users_criteria where user_id=@user_id and criteria_id=@criteria_id and criteria_value=@criteria_value"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@criteria_id", criteriaId)
                                            , DatabaseHelper.CreateStringParameter("@criteria_value", criteriaValue));
        }

        public static void DeleteUserAlgorithmSubcategory(int userId, int subCategoryId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_users_algorithm_subcategories where user_id=@user_id and subcategory_id=@subcategory_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                        , DatabaseHelper.CreateIntParameter("@subcategory_id", subCategoryId));

        }

        public static void DeleteUserAlgorithmSubcategories(int userId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_users_algorithm_subcategories where user_id = @user_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));

        }

        public static void DeleteMatch(int userId, int matchId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_algorithm_matches where user_id=@user_id and match_id=@match_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                        , DatabaseHelper.CreateIntParameter("@match_id", matchId));

        }

        public static void DeleteUserCriteriaValue(int userId, int criteriaId, string criteriaValue, DBSession session)
        {
            session.GetDataTable("Delete from Elio_users_criteria where user_id=@user_id and criteria_id=@criteria_id and criteria_value=@criteria_value"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@criteria_id", criteriaId)
                                                    , DatabaseHelper.CreateStringParameter("@criteria_value", criteriaValue));

        }

        public static void DeleteUserCriteriaById(int userId, int criteriaId, DBSession session)
        {
            session.GetDataTable(@"Delete from Elio_users_criteria
                                   where user_id = @user_id
                                   and criteria_id = @criteria_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@criteria_id", criteriaId));
        }

        public static void DeleteUserCriteria(int userId, int criteriaId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_users_criteria where user_id=@user_id and criteria_id=@criteria_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@criteria_id", criteriaId));
        }

        public static ElioUsersCriteria GetUserCriteriaByCriteriaId(int userId, int criteriaId, DBSession session)
        {
            DataLoader<ElioUsersCriteria> loader = new DataLoader<ElioUsersCriteria>(session);
            return loader.LoadSingle("Select * from Elio_users_criteria where user_id=@user_id and criteria_id=@criteria_id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@criteria_id", criteriaId));
        }

        public static List<ElioUsersCriteria> GetUserCriteriaByCritId(int userId, int criteriaId, DBSession session)
        {
            DataLoader<ElioUsersCriteria> loader = new DataLoader<ElioUsersCriteria>(session);
            return loader.Load("Select * from Elio_users_criteria where user_id=@user_id and criteria_id=@criteria_id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@criteria_id", criteriaId));
        }

        public static List<ElioUsersCriteria> GetUserCriteria(int userId, DBSession session)
        {
            DataLoader<ElioUsersCriteria> loader = new DataLoader<ElioUsersCriteria>(session);
            return loader.Load(@"Select * from Elio_users_criteria where user_id = @user_id order by criteria_id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsersAlgorithmSubcategories GetUserSubcaBySubcatId(int userId, int subcategoryId, DBSession session)
        {
            DataLoader<ElioUsersAlgorithmSubcategories> loader = new DataLoader<ElioUsersAlgorithmSubcategories>(session);
            return loader.LoadSingle("Select * from Elio_users_algorithm_subcategories where user_id=@user_id and subcategory_id=@subcategory_id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@subcategory_id", subcategoryId));
        }

        public static ElioCountries GetRegionByCountryName(string countryName, DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);
            return loader.LoadSingle("Select region from Elio_countries where country_name = @country_name"
                                            , DatabaseHelper.CreateStringParameter("@country_name", countryName));
        }

        public static ElioCountries GetCountryByCountryName(string countryName, DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);
            return loader.LoadSingle("Select * from Elio_countries where country_name = @country_name"
                                            , DatabaseHelper.CreateStringParameter("@country_name", countryName));
        }

        public static bool ExistCountryByNameLike(string countryName, DBSession session)
        {
            string query = @"declare @country nvarchar(150) " + Environment.NewLine;
            query += "set @country = '" + countryName + "%' " + Environment.NewLine;

            query += @"Select count(id) as count from Elio_countries 
                        where country_name like @country
                        or country_name like REPLACE(@country, '-', ' ')";

            DataTable table = session.GetDataTable(query);

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static bool ExistCityInUsersByCityNameLike(string cityName, DBSession session)
        {
            string query = @"declare @city nvarchar(150) " + Environment.NewLine;
            query += "set @city = '" + cityName + "%' " + Environment.NewLine;

            query += @"Select count(id) as count from elio_users 
                        where city like @city
                        or city like REPLACE(@city, '-', ' ')";

            DataTable table = session.GetDataTable(query);

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static List<ElioAlgorithmMatches> GetUniqueMatches(DBSession session)
        {
            DataLoader<ElioAlgorithmMatches> loader = new DataLoader<ElioAlgorithmMatches>(session);
            return loader.Load("select distinct(user_id) from Elio_algorithm_matches");
        }

        public static List<ElioAlgorithmMatches> GetUniqueMatchesById(int userId, DBSession session)
        {
            DataLoader<ElioAlgorithmMatches> loader = new DataLoader<ElioAlgorithmMatches>(session);
            return loader.Load("select match_id from Elio_algorithm_matches where user_id=@user_id"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioAlgSubcatIJGroupItems> GetSubcatDescriptionByUserId(int userId, DBSession session)
        {
            DataLoader<ElioAlgSubcatIJGroupItems> loader = new DataLoader<ElioAlgSubcatIJGroupItems>(session);
            return loader.Load("select * from Elio_users_algorithm_subcategories as A inner join Elio_sub_industries_group_items as B on A.subcategory_id = B.id where user_id=@user_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioSubIndustriesGroupItems> GetUserSubIndustries(int userId, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroupItems> loader = new DataLoader<ElioSubIndustriesGroupItems>(session);
            return loader.Load(@"SELECT sigi.id, usigi.user_id, description, opportunities, sigi.sub_industies_group_id 
                                 FROM Elio_sub_industries_group_items  sigi
                                 INNER JOIN Elio_users_sub_industries_group_items usigi
                                    ON usigi.sub_industry_group_item_id = sigi.id 
                                 WHERE usigi.user_id = @user_id
                                 ORDER BY sigi.sub_industies_group_id,sigi.id"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersSubIndustriesGroupItems> GetUserSubIndustriesGroupItemsID(int userId, DBSession session)
        {
            DataLoader<ElioUsersSubIndustriesGroupItems> loader = new DataLoader<ElioUsersSubIndustriesGroupItems>(session);
            return loader.Load(@"SELECT id
                                 FROM Elio_users_sub_industries_group_items 
                                 WHERE user_id = @user_id"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsersFiles GetUserPdfFile(int userId, DBSession session)
        {
            DataLoader<ElioUsersFiles> loader = new DataLoader<ElioUsersFiles>(session);
            return loader.LoadSingle("Select * from Elio_users_files where user_id=@user_id and file_path like '%.pdf' and is_public=1"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersFiles> GetUserFiles(int userId, DBSession session)
        {
            DataLoader<ElioUsersFiles> loader = new DataLoader<ElioUsersFiles>(session);
            return loader.Load("Select * from Elio_users_files where user_id=@user_id and is_public=1"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsersFiles GetUserCsvFile(int userId, DBSession session)
        {
            DataLoader<ElioUsersFiles> loader = new DataLoader<ElioUsersFiles>(session);
            return loader.LoadSingle("Select * from Elio_users_files where user_id=@user_id and file_path like '%.csv' and is_public=1"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioAlgorithmMatches GetMatchByUserAndMatchId(int userId, int matchId, DBSession session)
        {
            DataLoader<ElioAlgorithmMatches> loader = new DataLoader<ElioAlgorithmMatches>(session);
            return loader.LoadSingle("Select * from Elio_algorithm_matches where user_id=@user_id and match_id=@match_id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@match_id", matchId));
        }

        public static bool ExistConnection(int userId, int connectionId, DBSession session)
        {
            DataTable table = session.GetDataTable("select COUNT(id) as count from Elio_users_connections where user_id=@user_id and connection_id=@connection_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@connection_id", connectionId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static List<ElioUsersConnections> GetUserConnections(int userId, DBSession session)
        {
            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
            return loader.Load("select * from Elio_users_connections where user_id=@user_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersConnections> GetConnectionsByReseller(int resellerId, DBSession session)
        {
            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
            return loader.Load("select * from Elio_users_connections where connection_id = @connection_id"
                                                        , DatabaseHelper.CreateIntParameter("@connection_id", resellerId));
        }

        public static bool SetUserConnectionsAsViewed(int userId, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_users_connections 
                                 SET is_new = 0,
                                     last_updated = GETDATE()
                                 WHERE user_id = @user_id AND is_new = 1"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return true;
        }

        public static bool SetVendorResellerInvitationAsNotNew(int userId, DBSession session)
        {
            session.ExecuteQuery(@"UPDATE Elio_collaboration_vendor_reseller_invitations 
                                 SET is_new = 0,
                                     last_updated = GETDATE()
                                 WHERE user_id = @user_id AND is_new = 1 and invitation_step_description = 'Confirmed'"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return true;
        }

        public static bool HasUserConnections(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("select COUNT(id) AS count from Elio_users_connections where user_id = @user_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static List<ElioUsersConnections> GetUserConnectionsByPeriod(int userId, DateTime currentPeriodStart, DateTime currentPeriodEnd, DBSession session)
        {
            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
            return loader.Load("select * from Elio_users_connections where user_id=@user_id " +
                               "(and current_period_start>=@current_period_start and current_period_end<=@current_period_end)"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateDateTimeParameter("@current_period_end", currentPeriodStart)
                               , DatabaseHelper.CreateDateTimeParameter("@current_period_end", currentPeriodEnd));
        }

        public static ElioUsersConnections GetUserConnection(int id, DBSession session)
        {
            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
            return loader.LoadSingle("select * from Elio_users_connections where id=@id"
                                                        , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsersConnections GetConnection(int userId, int connectionId, DBSession session)
        {
            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
            return loader.LoadSingle(@"select 
                                        id, 
                                        user_id, 
                                        connection_id, 
                                        current_period_start, 
                                        current_period_end, 
                                        status, 
                                        is_new 
                                       from Elio_users_connections 
                                       where user_id = @user_id 
                                       and connection_id = @connection_id"
                                     , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                     , DatabaseHelper.CreateIntParameter("@connection_id", connectionId));
        }

        public static void DeleteConnection(int userId, int connectionId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_users_connections where user_id = @user_id and connection_id = @connection_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                        , DatabaseHelper.CreateIntParameter("@connection_id", connectionId));
        }

        public static void DeleteConnectionByUser(int userId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_users_connections where user_id = @user_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static bool HasUserConnectionsByTimeRange(int userId, DateTime? fromDate, DateTime? toDate, DBSession session)
        {
            DataTable table = session.GetDataTable("select COUNT(id) as count from Elio_users_connections where user_id=@user_id " +
                                                   "and (sysdate>='" + fromDate + "' and last_updated<='" + toDate + "')"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (Convert.ToInt32(table.Rows[0]["count"]) == 0) ? false : true;
        }

        public static bool IsConnection(int userId, int connectionId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select COUNT(id) as count from Elio_users_connections 
                                                     where user_id = @user_id 
                                                     and connection_id = @connection_id"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@connection_id", connectionId));

            return (Convert.ToInt32(table.Rows[0]["count"]) == 0) ? false : true;
        }

        public static List<ElioUsersConnectionsIJUsersIJPersonIJCompanies> GetUserConnectionsDetailsIJUsersIJPersonsIJCompaniesByOrder(int userId, int canBeViewed, DateTime currentPeriodStart, DateTime currentPeriodEnd, DBSession session)
        {
            DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies> loader = new DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies>(session);
            return loader.Load(@"select uc.id as as connection_id, 
                                    uc.user_id as connection_user_id, 
                                    uc.connection_id as connection_company_id,
                                    uc.sysdate,  
                                    uc.current_period_start, 
                                    uc.current_period_end, 
                                    uc.can_be_viewed,
                                    u.company_name, 
                                    u.email, 
                                    u.official_email, 
                                    u.website, 
                                    u.company_logo, 
                                    u.billing_type, 
                                    u.user_application_type, 
                                    u.country 
                                from Elio_users_connections uc
                                inner join Elio_users u on u.id = uc.connection_id
                                left join Elio_users_person up
	                                    on up.user_id = uc.connection_id
                                left join Elio_users_person_companies upc
	                                on upc.elio_person_id = up.id
		                                and up.clearbit_person_id = upc.clearbit_person_id
		                                and up.user_id = upc.user_id 
                                        and upc.user_id = uc.connection_id
                                where uc.user_id = @user_id 
                                and can_be_viewed = @can_be_viewed 
                                and (current_period_start>=@current_period_start and current_period_end<=@current_period_end)"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@can_be_viewed", canBeViewed)
                                , DatabaseHelper.CreateDateTimeParameter("@current_period_start", currentPeriodStart)
                                , DatabaseHelper.CreateDateTimeParameter("@current_period_end", currentPeriodEnd));
        }

        public static DataTable GetUserConnectionsDetailsIJUsersIJPersonsIJCompaniesTable(int userId, int canBeViewed, DBSession session)
        {
            return session.GetDataTable(@"select uc.id as connection_id, uc.user_id as connection_user_id, uc.connection_id as connection_company_id,
                                    uc.sysdate,  uc.current_period_start, uc.current_period_end, uc.can_be_viewed, uc.is_new,
                                    Elio_users.company_name, Elio_users.email as company_email, Elio_users.official_email, Elio_users.website as company_website,
                                    Elio_users.company_logo, Elio_users.billing_type, Elio_users.user_application_type, 
                                    Elio_users.country, Elio_users.linkedin_url, Elio_users.company_type, Elio_users.is_public,
                                    up.avatar as avatar,
                                    upc.logo as logo
                                    from Elio_users_connections uc
                                    inner join Elio_users on Elio_users.id = uc.connection_id
                                    left join Elio_users_person up
	                                    on up.user_id = connection_id
                                    left join Elio_users_person_companies upc
	                                    on upc.elio_person_id = up.id
		                                    and up.clearbit_person_id = upc.clearbit_person_id
		                                    and up.user_id = upc.user_id and upc.user_id = uc.connection_id
                                    where uc.user_id = @user_id
                                    and can_be_viewed = @can_be_viewed
                                    order by uc.sysdate desc"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@can_be_viewed", canBeViewed));
        }

        public static List<ElioUsersConnectionsIJUsersIJPersonIJCompanies> GetUserConnectionsDetailsIJUsersIJPersonsIJCompanies(int userId, int canBeViewed, DBSession session)
        {
            DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies> loader = new DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies>(session);
            return loader.Load(@"select uc.id, uc.user_id, uc.connection_id,
                                    uc.sysdate,  uc.current_period_start, uc.current_period_end, uc.can_be_viewed, uc.is_new,
                                    Elio_users.company_name, Elio_users.email, Elio_users.official_email, Elio_users.website,
                                    Elio_users.company_logo, Elio_users.billing_type, Elio_users.user_application_type, 
                                    Elio_users.country, Elio_users.linkedin_url,
                                    up.avatar as avatar,
                                    upc.logo as logo
                                    from Elio_users_connections uc
                                    inner join Elio_users on Elio_users.id = uc.connection_id
                                    left join Elio_users_person up
	                                    on up.user_id = connection_id
                                    left join Elio_users_person_companies upc
	                                    on upc.elio_person_id = up.id
		                                    and up.clearbit_person_id = upc.clearbit_person_id
		                                    and up.user_id = upc.user_id and upc.user_id = uc.connection_id
                                    where uc.user_id = @user_id
                                    and can_be_viewed = @can_be_viewed
                                    order by uc.sysdate desc"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@can_be_viewed", canBeViewed));
        }

        public static List<ElioUsersConnectionsIJUsersIJPersonIJCompanies> GetResellerConnectionsDetailsIJUsersIJPersonsIJCompanies(int connectionUserId, int canBeViewed, DBSession session)
        {
            DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies> loader = new DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies>(session);
            return loader.Load(@"select uc.id, uc.user_id, uc.connection_id,
                                    uc.sysdate,  uc.current_period_start, uc.current_period_end, uc.can_be_viewed, uc.is_new,
                                    Elio_users.company_name, Elio_users.email, Elio_users.official_email, Elio_users.website,
                                    Elio_users.company_logo, Elio_users.billing_type, Elio_users.user_application_type, 
                                    Elio_users.country, Elio_users.linkedin_url,
                                    up.avatar as avatar,
                                    upc.logo as logo
                                    from Elio_users_connections uc
                                    inner join Elio_users on Elio_users.id = uc.connection_id
                                    left join Elio_users_person up
	                                    on up.user_id = connection_id
                                    left join Elio_users_person_companies upc
	                                    on upc.elio_person_id = up.id
		                                    and up.clearbit_person_id = upc.clearbit_person_id
		                                    and up.user_id = upc.user_id and upc.user_id = uc.connection_id
                                where uc.connection_id = @connection_id
                                and can_be_viewed = @can_be_viewed"
                                , DatabaseHelper.CreateIntParameter("@connection_id", connectionUserId)
                                , DatabaseHelper.CreateIntParameter("@can_be_viewed", canBeViewed));
        }

        public static List<ElioUsersConnectionsIJUsersIJPersonIJCompanies> GetResellerConnectionsDetailsIJUsersIJPersonsIJCompaniesByOrder(int connectionUserId, int canBeViewed, DateTime currentPeriodStart, DateTime currentPeriodEnd, DBSession session)
        {
            DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies> loader = new DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies>(session);
            return loader.Load(@"select uc.id, uc.user_id, uc.connection_id,
                                uc.sysdate,  uc.current_period_start, uc.current_period_end, uc.can_be_viewed,
                                u.company_name, u.email, u.official_email, u.website,
                                u.company_logo, u.billing_type, u.user_application_type, u.country 
                                from Elio_users_connections uc
                                inner join Elio_users u on u.id = uc.user_id 
                                left join Elio_users_person up
	                                    on up.user_id = uc.connection_id
                                left join Elio_users_person_companies upc
	                                on upc.elio_person_id = up.id
		                                and up.clearbit_person_id = upc.clearbit_person_id
		                                and up.user_id = upc.user_id and upc.user_id = uc.connection_id
                                where uc.connection_id = @connection_id
                                and can_be_viewed = @can_be_viewed
                                and (current_period_start>=@current_period_start and current_period_end<=@current_period_end)"
                                , DatabaseHelper.CreateIntParameter("@connection_id", connectionUserId)
                                , DatabaseHelper.CreateIntParameter("@can_be_viewed", canBeViewed)
                                , DatabaseHelper.CreateDateTimeParameter("@current_period_start", currentPeriodStart)
                                , DatabaseHelper.CreateDateTimeParameter("@current_period_end", currentPeriodEnd));
        }

        public static List<ElioUsersConnections> GetUserConnectionsDetails(int userId, int canBeViewd, string currentPeriodStart, string currentPeriodEnd, DBSession session)
        {
            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
            return loader.Load("select * from Elio_users_connections " +
                                "where user_id=@user_id " +
                                "and can_be_viewed=@can_be_viewed " +
                                "and (current_period_start>=@current_period_start and current_period_end<=@current_period_end)"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@can_be_viewed", canBeViewd)
                                , DatabaseHelper.CreateStringParameter("@current_period_start", currentPeriodStart)
                                , DatabaseHelper.CreateStringParameter("@current_period_end", currentPeriodEnd));
        }

        public static List<ElioSubIndustriesGroup> GetUserSubIndustryGroups(int userId, DBSession session)
        {
            DataLoader<ElioSubIndustriesGroup> loader = new DataLoader<ElioSubIndustriesGroup>(session);
            return loader.Load(@"select distinct sig.* 
                                    from dbo.Elio_sub_industries_group sig
                                    inner join dbo.Elio_sub_industries_group_items sigi
	                                    on sigi.sub_industies_group_id = sig.id
                                    inner join Elio_users_sub_industries_group_items usigi
	                                    on usigi.sub_industry_group_id = sig.id
                                    where user_id = @user_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioSubcategoriesIJSubcategoriesGroups> GetUserSubcategoriesAndGroups(int userId, DBSession session)
        {
            DataLoader<ElioSubcategoriesIJSubcategoriesGroups> loader = new DataLoader<ElioSubcategoriesIJSubcategoriesGroups>(session);
            return loader.Load("select user_id, sub_industry_group_item_id, sub_industry_group_id, description, group_description from Elio_users_sub_industries_group_items inner join " +
                                "Elio_sub_industries_group_items on Elio_users_sub_industries_group_items.sub_industry_group_item_id = Elio_sub_industries_group_items.id " +
                                "inner join Elio_sub_industries_group on Elio_sub_industries_group_items.sub_industies_group_id = Elio_sub_industries_group.id " +
                                "where user_id=@user_id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersFavoritesIJUsers> GetUserFavouritesIJUsersByIsDeletedStatus(int userId, int isDeleted, DBSession session)
        {
            DataLoader<ElioUsersFavoritesIJUsers> loader = new DataLoader<ElioUsersFavoritesIJUsers>(session);
            return loader.Load("Select * from elio_users_favorites " +
                               "inner join elio_users on elio_users.id=Elio_users_favorites.company_id " +
                               "where user_id=@user_id " +
                               "and is_deleted=@is_deleted " +
                               "order by elio_users_favorites.sysdate desc"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted));
        }

        public static int GetPublicOpportunitiesSum(DBSession session)
        {
            DataTable table = session.GetDataTable("Select sum(opportunities) as total_public_opportunities from elio_sub_industries_group_items where is_public=1");

            return Convert.ToInt32(table.Rows[0]["total_public_opportunities"]);
        }

        public static int GetUserOpportunitiesSum(string idsList, DBSession session)
        {
            DataTable table = session.GetDataTable("Select sum(opportunities) as total_public_opportunities from elio_sub_industries_group_items where id in (" + idsList + ")");

            return Convert.ToInt32(table.Rows[0]["total_public_opportunities"]);
        }

        public static ElioOpportunitiesUsersStatusCustom GetUserCustomOpportunityStatus(int userId, int statusId, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersStatusCustom> loader = new DataLoader<ElioOpportunitiesUsersStatusCustom>(session);

            return loader.LoadSingle(@"Select * From Elio_opportunities_users_status_custom 
                                       Where user_id=@user_id and opportunity_status_id=@opportunity_status_id"
                                       , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                       , DatabaseHelper.CreateIntParameter("@opportunity_status_id", statusId));
        }

        public static int GetAllPublicVendorsAndResellers(DBSession session)
        {
            DataTable table = session.GetDataTable("select count(id) as count " +
                                                    "from Elio_users " +
                                                    "where company_type in ('Vendors', 'Channel Partners') and is_public=1");

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetAllPublicVendors(DBSession session)
        {
            DataTable table = session.GetDataTable("select count(id) as count " +
                                                    "from Elio_users " +
                                                    "where company_type in ('Vendors') and is_public=1");

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static List<ElioUsers> GetPublicVendors(string company, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("select id, company_name, country, city, address, description, overview, website, company_logo from Elio_users " +
                                "where company_type in ('Vendors') " +
                                "and is_public = 1 " +
                                "and company_name like '%" + company + "%'" +
                                "order by company_name asc");
        }

        public static List<ElioCollaborationUsersInvitations> GetAllUserInvitations(int userId, DBSession session)
        {
            DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
            return loader.Load("Select * from Elio_collaboration_users_invitations with (nolock) " +
                               "where user_id=@user_id " +
                               "order by date_created desc"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioTeamRoles> GetTeamPublicRoles(DBSession session)
        {
            DataLoader<ElioTeamRoles> loader = new DataLoader<ElioTeamRoles>(session);

            return loader.Load(@"Select * from Elio_team_roles where is_public=1");
        }

        public static ElioUsersSubAccounts GetSubAccountByGuid(string guid, DBSession session)
        {
            DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);

            return loader.LoadSingle(@"Select * from Elio_users_sub_accounts where guid=@guid", DatabaseHelper.CreateStringParameter("@guid", guid));
        }

        public static ElioUsersSubAccounts GetSubAccountById(int id, DBSession session)
        {
            DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);

            return loader.LoadSingle(@"Select * from Elio_users_sub_accounts where id=@id", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsersSubAccounts GetSubAccountByAssignedPartner(int vendorResellerId, int resellerId, DBSession session)
        {
            DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);

            return loader.LoadSingle(@"Select usa.* 
                                      from Elio_users_sub_accounts usa
                                      inner join Elio_collaboration_vendors_members_resellers cvmr
	                                    on cvmr.sub_account_id = usa.id  
                                      where cvmr.vendor_reseller_id = @vendor_reseller_id
                                      and partner_user_id = @partner_user_id"
                                , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", vendorResellerId)
                                , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));
        }

        public static ElioUsersSubAccounts GetSubAccountByUserId(int userId, DBSession session)
        {
            DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);

            return loader.LoadSingle(@"Select * from Elio_users_sub_accounts where user_id = @user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersSubAccounts> GetUserSubAccounts(int userId, DBSession session)
        {
            DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);

            return loader.Load(@"Select * from Elio_users_sub_accounts where user_id=@user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersSubAccounts> GetUserSubAccountsByRoleId(int userId, int parentRoleId, DBSession session)
        {
            DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);

            return loader.Load(@"Select * from Elio_users_sub_accounts where user_id = @user_id and team_role_id = @team_role_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@team_role_id", parentRoleId));
        }

        public static ElioUsersSubAccounts GetSubAccountByEmail(string email, DBSession session)
        {
            DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);

            return loader.LoadSingle(@"Select * from Elio_users_sub_accounts where email = @email", DatabaseHelper.CreateStringParameter("@email", email));
        }

        public static ElioUsersSubAccounts GetMasterUserSubAccountByEmailAndUserId(string email, int masterUserId, DBSession session)
        {
            DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);

            return loader.LoadSingle(@"Select * from Elio_users_sub_accounts where email = @email and user_id = @user_id"
                                , DatabaseHelper.CreateStringParameter("@email", email)
                                , DatabaseHelper.CreateIntParameter("@user_id", masterUserId));
        }

        //public static ElioUsersSubAccountsIJRolesIJUsers GetSubAccountIJRolesIJUsersById(int id, DBSession session)
        //{
        //    DataLoader<ElioUsersSubAccountsIJRolesIJUsers> loader = new DataLoader<ElioUsersSubAccountsIJRolesIJUsers>(session);

        //    return loader.LoadSingle(@"Select * from Elio_users_sub_accounts 
        //                               inner join Elio_team_roles on Elio_team_roles.id=Elio_users_sub_accounts.team_role_id
        //                               inner join Elio_users on Elio_users.id=Elio_users_sub_accounts.user_id
        //                               where Elio_users_sub_accounts.id=@id"
        //                               , DatabaseHelper.CreateIntParameter("@id", id));
        //}

        //public static List<ElioUsersSubAccountsIJRolesIJUsers> GetUserSubAccountsIJRolesIJUsers(int userId, int selectTop, DBSession session)
        //{
        //    DataLoader<ElioUsersSubAccountsIJRolesIJUsers> loader = new DataLoader<ElioUsersSubAccountsIJRolesIJUsers>(session);

        //    string strQuery = "Select ";

        //    if (selectTop > 0)
        //        strQuery += " top " + selectTop + " ";

        //    strQuery += @" * from Elio_users_sub_accounts 
        //                    inner join Elio_permissions_users_roles on Elio_permissions_users_roles.id = Elio_users_sub_accounts.team_role_id
        //                    inner join Elio_users on Elio_users.id = Elio_users_sub_accounts.user_id
        //                    where user_id = @user_id";

        //    return loader.Load(strQuery
        //                         , DatabaseHelper.CreateIntParameter("@user_id", userId));
        //}

        public static List<ElioUsersSubAccounts> GetSubAccounts(DBSession session)
        {
            DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);

            return loader.Load(@"Select * from Elio_users_sub_accounts");
        }

        public static List<ElioOpportunitiesStatus> GetOpportunityPublicStatus(DBSession session)
        {
            DataLoader<ElioOpportunitiesStatus> loader = new DataLoader<ElioOpportunitiesStatus>(session);
            return loader.Load(@"Select * from Elio_opportunities_status with (nolock) where is_public=1");
        }

        public static List<ElioOpportunitiesSubCategoriesStatus> GetOpportunitySubCategoriesPublicStatus(int oppStatusId, DBSession session)
        {
            DataLoader<ElioOpportunitiesSubCategoriesStatus> loader = new DataLoader<ElioOpportunitiesSubCategoriesStatus>(session);
            return loader.Load(@"Select * from Elio_opportunities_sub_categories_status with (nolock) where is_public=1 and opportunity_status_id=@opportunity_status_id"
                                , DatabaseHelper.CreateIntParameter("@opportunity_status_id", oppStatusId));
        }

        public static ElioOpportunitiesStatus GetOpportunityStatusByStatusId(int statusId, DBSession session)
        {
            DataLoader<ElioOpportunitiesStatus> loader = new DataLoader<ElioOpportunitiesStatus>(session);
            return loader.LoadSingle(@"Select * from Elio_opportunities_status with (nolock) where id=@id"
                                       , DatabaseHelper.CreateIntParameter("@id", statusId));
        }

        public static ElioOpportunitiesUsersStatusCustom GetUserOpportunityCurtomByOpportunityStatusId(int userId, int opportunityStatus, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersStatusCustom> loader = new DataLoader<ElioOpportunitiesUsersStatusCustom>(session);

            return loader.LoadSingle(@"SELECT *
                                      FROM Elio_opportunities_users_status_custom 
                                      WHERE opportunity_status_id = @opportunity_status_id 
                                      AND user_id=@user_id"
                                      , DatabaseHelper.CreateIntParameter("@opportunity_status_id", opportunityStatus)
                                      , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioOpportunitiesUsersSubCategoriesStatus GetUserOpportunitySubCategoryStatus(int userId, int opportunityId, int? opportunityStatusId, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersSubCategoriesStatus> loader = new DataLoader<ElioOpportunitiesUsersSubCategoriesStatus>(session);

            string strQuery = @"SELECT * 
                               FROM Elio_opportunities_users_sub_categories_status 
                               WHERE 1 = 1 ";

            strQuery += (opportunityStatusId != null) ? "AND opportunity_status_id =" + opportunityStatusId : " ";
            strQuery += "AND user_id = @user_id AND opportunity_id = @opportunity_id";

            return loader.LoadSingle(strQuery
                                     , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                     , DatabaseHelper.CreateIntParameter("@opportunity_id", opportunityId));
        }

        public static bool HasUserOpportunitySpecificSubCategoryStatus(int userId, int opportunityId, int? opportunityStatusId, int subCategoriesStatusId, DBSession session)
        {
            DataTable table = new DataTable();

            string strQuery = @"SELECT COUNT(id) AS count  
                               FROM Elio_opportunities_users_sub_categories_status 
                               WHERE 1 = 1 ";

            strQuery += (opportunityStatusId != null) ? "AND opportunity_status_id =" + opportunityStatusId : " ";
            strQuery += "AND user_id = @user_id AND opportunity_id = @opportunity_id AND sub_categories_status_id = @sub_categories_status_id";

            table = session.GetDataTable(strQuery
                                     , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                     , DatabaseHelper.CreateIntParameter("@opportunity_id", opportunityId)
                                     , DatabaseHelper.CreateIntParameter("@sub_categories_status_id", subCategoriesStatusId));

            return (Convert.ToInt32(table.Rows[0]["count"]) > 0) ? true : false;
        }

        public static List<ElioOpportunitiesUsers> GetUsersOpportunities(int userId, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsers> loader = new DataLoader<ElioOpportunitiesUsers>(session);
            return loader.Load("Select * from Elio_opportunities_users with (nolock) " +
                               "where user_id=@user_id"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void DeleteAllUserOpportunitiesNotes(int opportunityUserId, DBSession session)
        {
            session.ExecuteQuery("Delete from Elio_opportunities_notes where opportunity_user_id=@opportunity_user_id", DatabaseHelper.CreateIntParameter("@opportunity_user_id", opportunityUserId));
        }

        public static void DeleteAllUserOpportunitiesTasks(int opportunityId, DBSession session)
        {
            session.ExecuteQuery("Delete from Elio_opportunities_users_tasks where opportunity_id=@opportunity_id", DatabaseHelper.CreateIntParameter("@opportunity_id", opportunityId));
        }

        public static void DeleteUserOpportunity(int opportunityId, DBSession session)
        {
            session.ExecuteQuery("Delete from Elio_opportunities_users where id=@id", DatabaseHelper.CreateIntParameter("@id", opportunityId));
        }

        public static void DeleteUserOpportunityCustomByStatusId(int statusId, int userId, DBSession session)
        {
            session.ExecuteQuery(@"DELETE 
                                   FROM Elio_opportunities_users_status_custom 
                                   WHERE opportunity_status_id = @opportunity_status_id 
                                   AND user_id = @user_id"
                                   , DatabaseHelper.CreateIntParameter("@opportunity_status_id", statusId)
                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void DeleteOpportunityById(int opportunityId, DBSession session)
        {
            session.ExecuteQuery(@"DELETE 
                                   FROM Elio_opportunities_notes 
                                   WHERE opportunity_user_id = @opportunity_user_id"
                                   , DatabaseHelper.CreateIntParameter("@opportunity_user_id", opportunityId));

            session.ExecuteQuery(@"DELETE 
                                   FROM Elio_opportunities_users_tasks 
                                   WHERE opportunity_id = @opportunity_id"
                                   , DatabaseHelper.CreateIntParameter("@opportunity_id", opportunityId));

            session.ExecuteQuery(@"DELETE 
                                   FROM Elio_opportunities_users 
                                   WHERE id = @id"
                                   , DatabaseHelper.CreateIntParameter("@id", opportunityId));

            session.ExecuteQuery(@"DELETE 
                                   FROM Elio_opportunities_users_sub_categories_status 
                                   WHERE opportunity_id = @opportunity_id"
                                   , DatabaseHelper.CreateIntParameter("@opportunity_id", opportunityId));
        }

        public static void DeleteOpportunityUserSubCategoriesStatusByOpportunityId(int opportunityId, DBSession session)
        {
            session.ExecuteQuery(@"DELETE 
                                   FROM Elio_opportunities_users_sub_categories_status 
                                   WHERE opportunity_id = @opportunity_id"
                                   , DatabaseHelper.CreateIntParameter("@opportunity_id", opportunityId));
        }

        public static int GetUserCountOfNotes(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_opportunities_notes with (nolock) " +
                               "where opportunity_user_id=@opportunity_user_id"
                               , DatabaseHelper.CreateIntParameter("@opportunity_user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static string GetUserCountry(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select country from Elio_users with (nolock) where id = @id"
                                                    , DatabaseHelper.CreateIntParameter("@id", userId));

            return (table != null && table.Rows.Count > 0) ? table.Rows[0]["country"].ToString() : "";
        }

        public static List<ElioOpportunitiesUserTasksIJUsers> GetUserTotalTasks(int userId, DBSession session)
        {
            DataLoader<ElioOpportunitiesUserTasksIJUsers> loader = new DataLoader<ElioOpportunitiesUserTasksIJUsers>(session);
            return loader.Load(@"Select * from Elio_opportunities_users_tasks with (nolock) 
                                 INNER JOIN elio_opportunities_users ON Elio_opportunities_users_tasks.opportunity_id=elio_opportunities_users.id
                                where Elio_opportunities_users_tasks.user_id=@user_id order by remind_date asc"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetUserTotalTasksTbl(int userId, bool isOverdue, DBSession session)
        {
            if (isOverdue)
            {
                return session.GetDataTable(@"Select t.id,opportunity_id,organization_name,task_subject,task_content,t.sysdate,t.last_updated,remind_date,status 
                                            from Elio_opportunities_users_tasks  t 
                                            INNER JOIN elio_opportunities_users op
	                                            ON t.opportunity_id=op.id
                                            where t.user_id = @user_id 
                                            and remind_date < getdate()
                                            order by remind_date asc"
                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));
            }
            else
            {
                return session.GetDataTable(@"Select t.id,opportunity_id,organization_name,task_subject,task_content,t.sysdate,t.last_updated,remind_date,status 
                                            from Elio_opportunities_users_tasks  t 
                                            INNER JOIN elio_opportunities_users op
	                                            ON t.opportunity_id=op.id
                                            where t.user_id = @user_id 
                                            and remind_date >= getdate()
                                            order by remind_date asc"
                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));
            }
        }

        public static ElioOpportunitiesUsersTasks GetTaskById(int id, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersTasks> loader = new DataLoader<ElioOpportunitiesUsersTasks>(session);
            return loader.LoadSingle(@"Select * from Elio_opportunities_users_tasks with (nolock) 
                                 where id=@id"
                               , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static int GetUserOpportunityCountOfTasks(int userId, int opportunityId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_opportunities_users_tasks with (nolock) " +
                               "where user_id=@user_id and opportunity_id=@opportunity_id"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateIntParameter("@opportunity_id", opportunityId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static ElioOpportunitiesNotes GetUsersOpportunityNoteById(int id, DBSession session)
        {
            DataLoader<ElioOpportunitiesNotes> loader = new DataLoader<ElioOpportunitiesNotes>(session);
            return loader.LoadSingle("Select * from Elio_opportunities_notes with (nolock) " +
                               "where id=@id"
                               , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioOpportunitiesUsersIJUsersIJStatus> GeAlltUsersOpportunitiesWithStatusDescription(int userId, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersIJUsersIJStatus> loader = new DataLoader<ElioOpportunitiesUsersIJUsersIJStatus>(session);
            return loader.Load(@"SELECT * FROM Elio_opportunities_users WITH (NOLOCK) 
                                 INNER JOIN Elio_opportunities_status 
                                 ON Elio_opportunities_users.status_id=Elio_opportunities_status.id 
                                 WHERE user_id=@user_id 
                                 ORDER BY Elio_opportunities_status.step_description"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioOpportunitiesUsersIJUsersIJStatus> GetUsersOpportunitiesWithDescriptionByStatus(int userId, int statusId, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersIJUsersIJStatus> loader = new DataLoader<ElioOpportunitiesUsersIJUsersIJStatus>(session);
            return loader.Load(@"Select * from Elio_opportunities_users with (nolock) 
                                 INNER JOIN Elio_opportunities_status ON Elio_opportunities_users.status_id=Elio_opportunities_status.id 
                                 where user_id=@user_id and status_id=@status_id
                                 order by Elio_opportunities_status.step_description desc"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateIntParameter("@status_id", statusId));
        }

        public static List<ElioOpportunitiesUsersIJUsersIJStatus> GetUsersOpportunitiesWithDescription(int userId, int statusId, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersIJUsersIJStatus> loader = new DataLoader<ElioOpportunitiesUsersIJUsersIJStatus>(session);
            return loader.Load(@"Select * from Elio_opportunities_users with (nolock) 
                                 INNER JOIN Elio_opportunities_status ON Elio_opportunities_users.status_id=Elio_opportunities_status.id 
                                 where user_id=@user_id and status_id=@status_id
                                 order by Elio_opportunities_status.step_description desc"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateIntParameter("@status_id", statusId));
        }

        public static List<ElioOpportunitiesNotes> GetUsersOpportunityNotesByOpportunityId(int opportunityUserId, DBSession session)
        {
            DataLoader<ElioOpportunitiesNotes> loader = new DataLoader<ElioOpportunitiesNotes>(session);
            return loader.Load(@"Select * from Elio_opportunities_notes with (nolock) 
                                 where opportunity_user_id=@opportunity_user_id order by sysdate desc"
                               , DatabaseHelper.CreateIntParameter("@opportunity_user_id", opportunityUserId));
        }

        public static DataTable GetUsersOpportunityNotesByOpportunityIdTbl(int opportunityUserId, DBSession session)
        {
            return session.GetDataTable(@"Select id,note_subject,note_content,sysdate,last_updated,is_public 
                                            from Elio_opportunities_notes with (nolock) 
                                            where opportunity_user_id = @opportunity_user_id order by sysdate desc"
                               , DatabaseHelper.CreateIntParameter("@opportunity_user_id", opportunityUserId));
        }

        public static List<ElioOpportunitiesUsersTasks> GetUsersTasks(int userId, int opportunityId, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersTasks> loader = new DataLoader<ElioOpportunitiesUsersTasks>(session);
            return loader.Load(@"Select * from Elio_opportunities_users_tasks with (nolock) 
                                 where user_id=@user_id and opportunity_id=@opportunity_id order by sysdate desc"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateIntParameter("@opportunity_id", opportunityId));
        }

        public static DataTable GetUsersTasksTbl(int userId, int opportunityId, DBSession session)
        {
            return session.GetDataTable(@"Select id,opportunity_id,task_subject,task_content,sysdate,last_updated,remind_date,status from Elio_opportunities_users_tasks with (nolock) 
                                 where user_id = @user_id and opportunity_id=@opportunity_id order by sysdate desc"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateIntParameter("@opportunity_id", opportunityId));
        }

        public static List<ElioOpportunitiesUsersIJUsersIJStatus> GetUsersOpportunitiesByName(int userId, string companyName, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersIJUsersIJStatus> loader = new DataLoader<ElioOpportunitiesUsersIJUsersIJStatus>(session);
            return loader.Load(@"Select Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id, 
	                                Elio_opportunities_status.step_description, 
	                                organization_name ,
	                                Elio_opportunities_users.last_updated
                                from Elio_opportunities_users with (nolock) 
	                                INNER JOIN Elio_opportunities_status ON Elio_opportunities_users.status_id=Elio_opportunities_status.id 
                                where Elio_opportunities_users.user_id=@user_id and organization_name like '%' + @organization_name + '%'
                                group by 
	                                Elio_opportunities_status.step_description, 
	                                organization_name,
	                                Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id,
	                                Elio_opportunities_users.last_updated
                                order by Elio_opportunities_users.last_updated DESC,
	                                Elio_opportunities_status.step_description, 
	                                organization_name,
	                                Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateStringParameter("@organization_name", companyName));
        }

        public static List<ElioOpportunitiesUsersIJUsersIJStatus> GetUsersOpportunitiesByNameAndStatus(int userId, string statusDescription, string companyName, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersIJUsersIJStatus> loader = new DataLoader<ElioOpportunitiesUsersIJUsersIJStatus>(session);
            return loader.Load(@"Select Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id, 
	                                Elio_opportunities_status.step_description, 
	                                organization_name ,
	                                Elio_opportunities_users.last_updated,
                                    u.id as organization_user_id,
                                    u.company_logo
                                from Elio_opportunities_users with (nolock) 
	                                INNER JOIN Elio_opportunities_status 
                                        ON Elio_opportunities_users.status_id=Elio_opportunities_status.id 
                                    LEFT JOIN Elio_users u
                                        ON u.email = Elio_opportunities_users.email
                                where Elio_opportunities_users.user_id=@user_id and organization_name like '' + @organization_name + '%'
                                    AND step_description = @step_description 
                                group by 
	                                Elio_opportunities_status.step_description, 
	                                organization_name,
	                                Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id,
	                                Elio_opportunities_users.last_updated,
                                    u.id,
                                    u.company_logo
                                order by Elio_opportunities_users.last_updated DESC,
	                                Elio_opportunities_status.step_description, 
	                                organization_name,
	                                Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id,
                                    u.id,
                                    u.company_logo"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateStringParameter("@organization_name", companyName)
                               , DatabaseHelper.CreateStringParameter("@step_description", statusDescription));
        }

        public static List<ElioOpportunitiesUsersIJUsersIJStatus> GetUsersOpportunitiesWithOpenTasksByName(int userId, string name, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersIJUsersIJStatus> loader = new DataLoader<ElioOpportunitiesUsersIJUsersIJStatus>(session);
            return loader.Load(@"Select Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id, 
	                                Elio_opportunities_status.step_description, 
	                                organization_name ,
	                                Elio_opportunities_users.last_updated
                                from Elio_opportunities_users with (nolock) 
	                                INNER JOIN Elio_opportunities_status ON Elio_opportunities_users.status_id=Elio_opportunities_status.id
                                    INNER JOIN Elio_opportunities_users_tasks ON Elio_opportunities_users.id=Elio_opportunities_users_tasks.opportunity_id 
                                where Elio_opportunities_users.user_id=@user_id and remind_date >= getdate() and organization_name like '%' + @organization_name + '%' 
                                group by 
	                                Elio_opportunities_status.step_description, 
	                                organization_name,
	                                Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id,
	                                Elio_opportunities_users.last_updated,
                                    Elio_opportunities_users_tasks.opportunity_id
                                order by Elio_opportunities_users.last_updated DESC,
	                                Elio_opportunities_status.step_description, 
	                                organization_name,
	                                Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id,
                                    Elio_opportunities_users_tasks.opportunity_id"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateStringParameter("@organization_name", name));
        }

        public static List<ElioOpportunitiesUsersIJUsersIJStatus> GetUsersOpportunitiesWithOpenTasksByStepByName(int userId, string statusDescription, string name, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsersIJUsersIJStatus> loader = new DataLoader<ElioOpportunitiesUsersIJUsersIJStatus>(session);
            return loader.Load(@"Select Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id, 
	                                Elio_opportunities_status.step_description, 
	                                organization_name ,
	                                Elio_opportunities_users.last_updated,
                                    u.id as organization_user_id,
                                    u.company_logo
                                from Elio_opportunities_users with (nolock) 
	                                INNER JOIN Elio_opportunities_status ON Elio_opportunities_users.status_id=Elio_opportunities_status.id
                                    INNER JOIN Elio_opportunities_users_tasks ON Elio_opportunities_users.id=Elio_opportunities_users_tasks.opportunity_id 
                                    inner join Elio_users u ON u.email = Elio_opportunities_users.email
                                where Elio_opportunities_users.user_id=@user_id and remind_date >= getdate() and organization_name like '' + @organization_name + '%' 
                                    AND step_description = @step_description
                                group by 
	                                Elio_opportunities_status.step_description, 
	                                organization_name,
	                                Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id,
	                                Elio_opportunities_users.last_updated,
                                    Elio_opportunities_users_tasks.opportunity_id,
                                    u.id,
                                    u.company_logo
                                order by Elio_opportunities_users.last_updated DESC,
	                                Elio_opportunities_status.step_description, 
	                                organization_name,
	                                Elio_opportunities_users.id,
	                                Elio_opportunities_users.user_id,
                                    Elio_opportunities_users_tasks.opportunity_id,
                                    u.id,
                                    u.company_logo"
                               , DatabaseHelper.CreateIntParameter("@user_id", userId)
                               , DatabaseHelper.CreateStringParameter("@organization_name", name)
                               , DatabaseHelper.CreateStringParameter("@step_description", statusDescription));
        }

        public static List<ElioOpportunitiesSubCategoriesStatus> GetUserOpportunitiesByStatusAndSubCategoryStatus(int userId, int subStatus, int opportunityStatusId, DBSession session)
        {
            DataLoader<ElioOpportunitiesSubCategoriesStatus> loader = new DataLoader<ElioOpportunitiesSubCategoriesStatus>(session);

            return loader.Load(@"SELECT *
                                 FROM Elio_opportunities_users_sub_categories_status
                                 WHERE user_id=@user_id 
                                 AND sub_categories_status_id=@sub_categories_status_id 
                                 AND opportunity_status_id=@opportunity_status_id"
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                 , DatabaseHelper.CreateIntParameter("@sub_categories_status_id", subStatus)
                                 , DatabaseHelper.CreateIntParameter("@opportunity_status_id", opportunityStatusId));
        }

        public static ElioOpportunitiesUsers GetOpportunityById(int id, DBSession session)
        {
            DataLoader<ElioOpportunitiesUsers> loader = new DataLoader<ElioOpportunitiesUsers>(session);
            return loader.LoadSingle(@"Select * from Elio_opportunities_users with (nolock) 
                                       where id=@id"
                                       , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static bool IsUserOpportunity(int userId, string companyName, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(id) as count from Elio_opportunities_users with (nolock) 
                                       where user_id=@user_id and organization_name=@organization_name"
                                       , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                       , DatabaseHelper.CreateStringParameter("@organization_name", companyName));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static void DeleteOpportunityNoteById(int id, DBSession session)
        {
            session.ExecuteQuery(@"DELETE from Elio_opportunities_notes where id=@id"
                                 , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static void DeleteTaskById(int id, DBSession session)
        {
            session.ExecuteQuery(@"DELETE from Elio_opportunities_users_tasks where id=@id"
                                 , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static bool IsUserOpportunityByEmailOrName(int userId, string companyName, string email, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select count(id) as count from Elio_opportunities_users with (nolock) 
                                       where user_id=@user_id and 
                                       (organization_name=@organization_name or email=@email)"
                                       , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                       , DatabaseHelper.CreateStringParameter("@organization_name", companyName)
                                       , DatabaseHelper.CreateStringParameter("@email", email));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static void DeleteSubAccountById(int id, DBSession session)
        {
            session.ExecuteQuery(@"Delete from Elio_users_sub_accounts where id=@id"
                                 , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioPacketsStripeCoupons GetPacketStripeCouponByUserPlanId(int userId, string striepPlantId, DBSession session)
        {
            DataLoader<ElioPacketsStripeCoupons> loader = new DataLoader<ElioPacketsStripeCoupons>(session);
            return loader.LoadSingle(@"Select TOP 1 * from Elio_packets_stripe_coupons with (nolock) 
                                        where user_id = @user_id
                                        and stripe_plan_id = @stripe_plan_id
                                        order by date_created desc"
                                       , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                       , DatabaseHelper.CreateStringParameter("@stripe_plan_id", striepPlantId));
        }

        public static ElioPacketsStripeCoupons GetPacketStripeCoupon(string couponId, DBSession session)
        {
            DataLoader<ElioPacketsStripeCoupons> loader = new DataLoader<ElioPacketsStripeCoupons>(session);
            return loader.LoadSingle(@"Select * from Elio_packets_stripe_coupons with (nolock) 
                                       where coupon_id = @coupon_id"
                                       , DatabaseHelper.CreateStringParameter("@coupon_id", couponId));
        }

        public static ElioPacketsStripeCoupons GetPacketStripeCouponByStripePlan(string couponId, string stripePlanId, DBSession session)
        {
            DataLoader<ElioPacketsStripeCoupons> loader = new DataLoader<ElioPacketsStripeCoupons>(session);
            return loader.LoadSingle(@"Select * from Elio_packets_stripe_coupons with (nolock) 
                                       where coupon_id = @coupon_id
                                       and stripe_plan_id = @stripe_plan_id"
                                       , DatabaseHelper.CreateStringParameter("@coupon_id", couponId)
                                       , DatabaseHelper.CreateStringParameter("@stripe_plan_id", stripePlanId));
        }

        public static ElioPlanCoupons GetPlanCoupon(string couponId, DBSession session)
        {
            DataLoader<ElioPlanCoupons> loader = new DataLoader<ElioPlanCoupons>(session);
            return loader.LoadSingle(@"Select * from Elio_plan_coupons with (nolock) 
                                       where coupon_id=@coupon_id"
                                       , DatabaseHelper.CreateStringParameter("@coupon_id", couponId));
        }

        public static ElioPacketsStripeCoupons GetPlanStripeCoupon(string couponId, DBSession session)
        {
            DataLoader<ElioPacketsStripeCoupons> loader = new DataLoader<ElioPacketsStripeCoupons>(session);
            return loader.LoadSingle(@"Select * from Elio_packets_stripe_coupons with (nolock) 
                                       where coupon_id = @coupon_id"
                                       , DatabaseHelper.CreateStringParameter("@coupon_id", couponId));
        }

        public static List<ElioUsersMultiAccounts> GetUserClientAccounts(int supervisorId, DBSession session)
        {
            DataLoader<ElioUsersMultiAccounts> loader = new DataLoader<ElioUsersMultiAccounts>(session);

            return loader.Load(@"Select * from Elio_users_multi_accounts with (nolock)
                                 where supervisor_id=@supervisor_id"
                                 , DatabaseHelper.CreateIntParameter("@supervisor_id", supervisorId));
        }

        public static List<ElioUsersMultiAccountsIJUsers> GetSupervisorUserClientAccounts(int supervisorId, DBSession session)
        {
            DataLoader<ElioUsersMultiAccountsIJUsers> loader = new DataLoader<ElioUsersMultiAccountsIJUsers>(session);

            return loader.Load(@"Select * from Elio_users with (nolock)
                                inner join Elio_users_multi_accounts 
                                on Elio_users_multi_accounts.client_id=Elio_users.id
                                where supervisor_id=@supervisor_id"
                                , DatabaseHelper.CreateIntParameter("@supervisor_id", supervisorId));
        }

        public static List<ElioOnboardingUsersLibraryFiles> GetUserOnboardingFiles(int userId, DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFiles> loader = new DataLoader<ElioOnboardingUsersLibraryFiles>(session);

            return loader.Load(@"SELECT * FROM Elio_onboarding_users_library_files
                                 where user_id = @user_id
                                 and uploaded_by_user_id = @uploaded_by_user_id
                                 and is_public = 1"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@uploaded_by_user_id", userId));
        }

        public static ElioOnboardingFileTypes GetOnboardingFileTypeById(int id, DBSession session)
        {
            DataLoader<ElioOnboardingFileTypes> loader = new DataLoader<ElioOnboardingFileTypes>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_onboarding_file_types
                                 where id = @id"
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioOnboardingFileTypes GetOnboardingFileTypeByExtension(string extension, DBSession session)
        {
            DataLoader<ElioOnboardingFileTypes> loader = new DataLoader<ElioOnboardingFileTypes>(session);

            return loader.LoadSingle(@"SELECT TOP 1 * FROM Elio_onboarding_file_types
                                 where file_format_extensions like '%" + extension + "%'");
        }

        public static List<ElioOnboardingUsersLibraryFilesIJFileTypesIJCategories> GetUserOnboardingFilesIJTypesIJCategories(int userId, int categoryId, DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFilesIJFileTypesIJCategories> loader = new DataLoader<ElioOnboardingUsersLibraryFilesIJFileTypesIJCategories>(session);

            return loader.Load(@"SELECT  ulf.id, ulf.user_id, ulf.file_type_id, ulf.category_id, ulf.file_title, ulf.file_name, ulf.file_path, ulf.file_type, ulf.date_created, ulf.is_public, ulf.is_new,
                                 ft.file_format_extensions, lfc.category_description
                                 FROM Elio_onboarding_users_library_files ulf
                                 INNER JOIN Elio_onboarding_file_types ft 
                                    ON ft.id = ulf.file_type_id
                                 INNER JOIN Elio_onboarding_library_files_default_categories lfc
	                                ON lfc.id = ulf.category_id
                                 WHERE user_id = @user_id
                                 AND uploaded_by_user_id = @uploaded_by_user_id
                                 AND ulf.category_id = @category_id
                                 AND ulf.is_public = 1
                                 ORDER BY ulf.file_type"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@uploaded_by_user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@category_id", categoryId));
        }

        public static List<ElioOnboardingLibraryFilesDefaultCategories> GetOnboardingLibraryDefaultFilesCategories(DBSession session)
        {
            DataLoader<ElioOnboardingLibraryFilesDefaultCategories> loader = new DataLoader<ElioOnboardingLibraryFilesDefaultCategories>(session);
            return loader.Load(@"SELECT *  
                                 FROM Elio_onboarding_library_files_default_categories 
                                 WHERE is_default = 1
                                 AND is_public=1");
        }

        public static List<ElioOnboardingLibraryFilesDefaultCategories> GetOnboardingLibraryCustomFilesCategories(string categoryIDs, DBSession session)
        {
            DataLoader<ElioOnboardingLibraryFilesDefaultCategories> loader = new DataLoader<ElioOnboardingLibraryFilesDefaultCategories>(session);
            return loader.Load(@"SELECT *  
                                 FROM Elio_onboarding_library_files_default_categories 
                                 WHERE is_default = 0
                                 AND is_public = 0
                                 AND id IN (" + categoryIDs + ")");
        }

        public static ElioOnboardingLibraryFilesDefaultCategories GetOnboardingLibraryDefaultFilesCategoryById(int categoryId, DBSession session)
        {
            DataLoader<ElioOnboardingLibraryFilesDefaultCategories> loader = new DataLoader<ElioOnboardingLibraryFilesDefaultCategories>(session);
            return loader.LoadSingle(@"SELECT *  
                                         FROM Elio_onboarding_library_files_default_categories 
                                         WHERE id = @id"
                                        , DatabaseHelper.CreateIntParameter("@id", categoryId));
        }

        public static ElioOnboardingUsersLibraryFilesCategories GetOnboardingUserLibraryFilesCategoriesById(int categoryId, int userId, DBSession session)
        {
            DataLoader<ElioOnboardingUsersLibraryFilesCategories> loader = new DataLoader<ElioOnboardingUsersLibraryFilesCategories>(session);
            return loader.LoadSingle(@"SELECT *  
                                         FROM Elio_onboarding_users_library_files_categories 
                                         WHERE id = @id and user_id = @user_id"
                                        , DatabaseHelper.CreateIntParameter("@id", categoryId)
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioOnboardingUsersVideoLibraryFiles> GetUserOnboardingVideoFiles(int userId, DBSession session)
        {
            DataLoader<ElioOnboardingUsersVideoLibraryFiles> loader = new DataLoader<ElioOnboardingUsersVideoLibraryFiles>(session);

            return loader.Load(@"select * from dbo.Elio_onboarding_users_video_library_files
                                 WHERE user_id = @user_id
                                 AND is_public = 1
                                 ORDER BY date_created desc"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioOnboardingUsersVideoLibraryFiles GetUserOnboardingVideoFileById(int fileId, DBSession session)
        {
            DataLoader<ElioOnboardingUsersVideoLibraryFiles> loader = new DataLoader<ElioOnboardingUsersVideoLibraryFiles>(session);

            return loader.LoadSingle(@"SELECT * FROM dbo.Elio_onboarding_users_video_library_files
                                         WHERE 1 = 1 
                                         AND id = @id 
                                         ORDER BY date_created desc"
                                , DatabaseHelper.CreateIntParameter("@id", fileId));
        }

        public static void DeleteUserById(int userId, DBSession session)
        {
            session.ExecuteQuery(@"Delete 
                                    from Elio_users 
                                    where id=@id"
                                               , DatabaseHelper.CreateIntParameter("@id", userId));

        }

        public static void DeleteWholeUserCredentials(int userId, DBSession session)
        {
            session.ExecuteQuery(@"DELETE 
                                   from Elio_users_sub_industries_group_items 
                                   where user_id=@user_id"
                                              , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_users_sub_accounts
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_users_plan_coupons
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_users_partners
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_users_notification_emails
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_users_multi_accounts
                                    where client_id=@client_id"
                                             , DatabaseHelper.CreateIntParameter("@client_id", userId));


            session.ExecuteQuery(@"DELETE 
                                    from Elio_users_markets
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_users_industries
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_users_criteria
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_users_connections
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_users_apies
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_users_algorithm_subcategories
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_user_packet_status
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_user_emails
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_discount_users
                                    where user_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from Elio_collaboration_vendors_resellers
                                    where vendor_id=@user_id"
                                             , DatabaseHelper.CreateIntParameter("@user_id", userId));

            session.ExecuteQuery(@"DELETE 
                                    from elio_users
                                    where id=@id"
                                             , DatabaseHelper.CreateIntParameter("@id", userId));
        }

        public static string GetCollaborationInvitationStatus(int vendorId, int resellerId, DBSession session)
        {
            string invStatus = string.Empty;

            DataTable table = session.GetDataTable(@"SELECT invitation_status 
                                                     FROM Elio_collaboration_vendors_resellers 
                                                     WHERE master_user_id=@master_user_id 
                                                     AND partner_user_id=@partner_user_id"
                                                     , DatabaseHelper.CreateIntParameter("@master_user_id", vendorId)
                                                     , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId));

            if (table.Rows.Count > 0)
            {
                invStatus = table.Rows[0]["invitation_status"].ToString();
            }

            return invStatus;
        }

        public static ElioServices GetServiceByPlanId(string planId, DBSession session)
        {
            DataLoader<ElioServices> loader = new DataLoader<ElioServices>(session);
            return loader.LoadSingle("Select * from elio_services where plan_id=@plan_id", DatabaseHelper.CreateStringParameter("@id", planId));
        }

        public static ElioServicesFeatures GetServiceFeaturesByServiceId(int serviceId, DBSession session)
        {
            DataLoader<ElioServicesFeatures> loader = new DataLoader<ElioServicesFeatures>(session);
            return loader.LoadSingle("Select * from elio_services_features where service_id=@service_id"
                                     , DatabaseHelper.CreateIntParameter("@service_id", serviceId));
        }

        public static decimal GetServiceTotalCost(int serviceId, DBSession session)
        {
            DataLoader<ElioServicesFeatures> loader = new DataLoader<ElioServicesFeatures>(session);

            ElioServicesFeatures serviceFeatures = loader.LoadSingle("SELECT * FROM Elio_services_features where service_id=@service_id", DatabaseHelper.CreateIntParameter("service_id", serviceId));

            decimal cost = 0;

            cost = (serviceFeatures != null) ? serviceFeatures.Cost : 0;

            return cost;
        }

        public static bool HasUserActiveService(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as COUNT 
                                                     FROM Elio_users_services 
                                                     WHERE user_id=@user_id and is_active=1"
                                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return (Convert.ToInt32(table.Rows[0]["COUNT"]) > 0) ? true : false;
        }

        public static List<ElioUsersServices> GetUserServices(int userId, DBSession session)
        {
            DataLoader<ElioUsersServices> loader = new DataLoader<ElioUsersServices>(session);

            return loader.Load(@"SELECT * FROM elio_users_services WHERE user_id=@user_id"
                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsersStripeId GetUserStripeServiceByUserId(int userId, DBSession session)
        {
            DataLoader<ElioUsersStripeId> loader = new DataLoader<ElioUsersStripeId>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_users_stripe_id WHERE user_id=@user_id"
                                     , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioFinancialIncome GetElioFinancialIncomeFlowById(int id, DBSession session)
        {
            DataLoader<ElioFinancialIncome> loader = new DataLoader<ElioFinancialIncome>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_financial_income WHERE id=@id"
                                     , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioFinancialIncome> GetAllPublicElioFinancialIncome(DBSession session)
        {
            DataLoader<ElioFinancialIncome> loader = new DataLoader<ElioFinancialIncome>(session);

            return loader.Load(@"SELECT * FROM Elio_financial_income where is_public=1 ORDER BY datein DESC");
        }

        public static void SetElioFinancialIncomesNotPublic(int id, int lastEditUserId, DBSession session)
        {

            session.ExecuteQuery(@"UPDATE Elio_financial_income 
                                   SET 
                                        is_public=@is_public, 
                                        last_updated=@last_updated, 
                                        last_edit_user_id=@last_edit_user_id 
                                   WHERE id=@id"
                                   , DatabaseHelper.CreateIntParameter("@is_public", 0)
                                   , DatabaseHelper.CreateDateTimeParameter("@last_updated", DateTime.Now)
                                   , DatabaseHelper.CreateIntParameter("@last_edit_user_id", lastEditUserId)
                                   , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioFinancialIncome UpdateElioFinancialFlowById(int id, decimal incomeAmount,
                                                                       string adminName, int adminId,
                                                                       string incomeSource, string organization,
                                                                       string comments,
                                                                       DateTime datein, DateTime lastUpdated,
                                                                       decimal vat, decimal incomeVatAmount,
                                                                       decimal incomeAmountWithNoVat,
                                                                       int isPublic,
                                                                       int lastEditUserId,
                                                                       DBSession session)
        {
            DataLoader<ElioFinancialIncome> loader = new DataLoader<ElioFinancialIncome>(session);

            return loader.LoadSingle(@"UPDATE Elio_financial_income 
                                       SET
                                            income_amount=@income_amount, 
                                            admin_name=@admin_name,
                                            admin_id=@admin_id, 
                                            income_source=@income_source, 
                                            organization=@organization, 
                                            comments=@comments, 
                                            datein=@datein, 
                                            last_updated=@last_updated, 
                                            vat=@vat, 
                                            vat_amount=@vat_amount, 
                                            income_amount_with_no_vat=@income_amount_with_no_vat,
                                            is_public=@is_public,
                                            last_edit_user_id=@last_edit_user_id
                                      WHERE id=@id"
                                     , DatabaseHelper.CreateDecimalParameter("@income_amount", incomeAmount)
                                     , DatabaseHelper.CreateStringParameter("@admin_name", adminName)
                                     , DatabaseHelper.CreateIntParameter("@admin_id", adminId)
                                     , DatabaseHelper.CreateStringParameter("@income_source", incomeSource)
                                     , DatabaseHelper.CreateStringParameter("@organization", organization)
                                     , DatabaseHelper.CreateStringParameter("@comments", comments)
                                     , DatabaseHelper.CreateDateTimeParameter("@datein", datein)
                                     , DatabaseHelper.CreateDateTimeParameter("@last_updated", lastUpdated)
                                     , DatabaseHelper.CreateDecimalParameter("@vat", vat)
                                     , DatabaseHelper.CreateDecimalParameter("@vat_amount", incomeVatAmount)
                                     , DatabaseHelper.CreateDecimalParameter("@income_amount_with_no_vat", incomeAmountWithNoVat)
                                     , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                                     , DatabaseHelper.CreateIntParameter("@last_edit_user_id", lastEditUserId)
                                     , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static void GetFinancialIncomePublicAmounts(ref double totalIncome, ref double totalVatAmount, ref double totalClearAmount, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT 
                                                        SUM(income_amount) AS TotalIncome, 
                                                        SUM(vat_amount) AS TotalVatAmount, 
                                                        SUM(income_amount_with_no_vat) AS TotalClearAmount 
                                                     FROM Elio_financial_income where is_public=1");

            if (table.Rows.Count > 0)
            {
                totalIncome = Convert.ToDouble(table.Rows[0]["TotalIncome"]);
                totalVatAmount = Convert.ToDouble(table.Rows[0]["TotalVatAmount"]);
                totalClearAmount = Convert.ToDouble(table.Rows[0]["TotalClearAmount"]);
            }
            else
            {
                totalIncome = 0.000;
                totalVatAmount = 0.00;
                totalClearAmount = 0.00;
            }
        }

        public static ElioFinancialExpenses GetElioFinancialExpensesFlowById(int id, DBSession session)
        {
            DataLoader<ElioFinancialExpenses> loader = new DataLoader<ElioFinancialExpenses>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_financial_expenses WHERE id=@id"
                                     , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioFinancialExpenses> GetAllPublicElioFinancialExpenses(DBSession session)
        {
            DataLoader<ElioFinancialExpenses> loader = new DataLoader<ElioFinancialExpenses>(session);

            return loader.Load(@"SELECT * FROM Elio_financial_expenses where is_public=1 ORDER BY datein DESC");
        }

        public static void SetElioFinancialExpensesNotPublic(int id, int lastEditUserId, DBSession session)
        {

            session.ExecuteQuery(@"UPDATE Elio_financial_expenses 
                                   SET 
                                        is_public=@is_public, 
                                        last_updated=@last_updated, 
                                        last_edit_user_id=@last_edit_user_id 
                                   WHERE id=@id"
                                   , DatabaseHelper.CreateIntParameter("@is_public", 0)
                                   , DatabaseHelper.CreateDateTimeParameter("@last_updated", DateTime.Now)
                                   , DatabaseHelper.CreateIntParameter("@last_edit_user_id", lastEditUserId)
                                   , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioFinancialExpenses UpdateElioFinancialExpensesFlowById(int id, decimal expensesAmount,
                                                                       string adminName, int adminId,
                                                                       string expensesReason, int userId,
                                                                       string organization, string comments,
                                                                       DateTime datein, DateTime lastUpdated,
                                                                       decimal vat, decimal vatAmount,
                                                                       decimal expensesAmountWithNoVat,
                                                                       int isPublic,
                                                                       int lastEditUserId,
                                                                       DBSession session)
        {
            DataLoader<ElioFinancialExpenses> loader = new DataLoader<ElioFinancialExpenses>(session);

            return loader.LoadSingle(@"UPDATE Elio_financial_expenses 
                                       SET
                                            expenses_amount=@expenses_amount, 
                                            admin_name=@admin_name,
                                            admin_id=@admin_id, 
                                            expenses_reason=@expenses_reason,
                                            user_id=@user_id, 
                                            organization=@organization, 
                                            comments=@comments, 
                                            datein=@datein, 
                                            last_updated=@last_updated, 
                                            vat=@vat, 
                                            vat_amount=@vat_amount, 
                                            expenses_amount_with_no_vat=@expenses_amount_with_no_vat,
                                            is_public=@is_public,
                                            last_edit_user_id=@last_edit_user_id
                                      WHERE id=@id"
                                     , DatabaseHelper.CreateDecimalParameter("@expenses_amount", expensesAmount)
                                     , DatabaseHelper.CreateStringParameter("@admin_name", adminName)
                                     , DatabaseHelper.CreateIntParameter("@admin_id", adminId)
                                     , DatabaseHelper.CreateStringParameter("@expenses_reason", expensesReason)
                                     , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                     , DatabaseHelper.CreateStringParameter("@organization", organization)
                                     , DatabaseHelper.CreateStringParameter("@comments", comments)
                                     , DatabaseHelper.CreateDateTimeParameter("@datein", datein)
                                     , DatabaseHelper.CreateDateTimeParameter("@last_updated", lastUpdated)
                                     , DatabaseHelper.CreateDecimalParameter("@vat", vat)
                                     , DatabaseHelper.CreateDecimalParameter("@vat_amount", vatAmount)
                                     , DatabaseHelper.CreateDecimalParameter("@expenses_amount_with_no_vat", expensesAmountWithNoVat)
                                     , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                                     , DatabaseHelper.CreateIntParameter("@last_edit_user_id", lastEditUserId)
                                     , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static void GetFinancialExpensesPublicAmounts(ref double totalExpenses, ref double totalExpensesVatAmount, ref double totalClearExpensesAmount, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT 
                                                        SUM(expenses_amount) AS TotalExpenses, 
                                                        SUM(vat_amount) AS TotalExpensesVatAmount, 
                                                        SUM(expenses_amount_with_no_vat) AS TotalClearExpensesAmount 
                                                     FROM Elio_financial_expenses where is_public=1");

            if (table.Rows.Count > 0)
            {
                totalExpenses = Convert.ToDouble(table.Rows[0]["TotalExpenses"]);
                totalExpensesVatAmount = Convert.ToDouble(table.Rows[0]["TotalExpensesVatAmount"]);
                totalClearExpensesAmount = Convert.ToDouble(table.Rows[0]["TotalClearExpensesAmount"]);
            }
            else
            {
                totalExpenses = 0.00;
                totalExpensesVatAmount = 0.00;
                totalClearExpensesAmount = 0.00;
            }
        }

        public static ElioUsersProductDemoViews GetUserProductDemoViews(int userId, DBSession session)
        {
            DataLoader<ElioUsersProductDemoViews> loader = new DataLoader<ElioUsersProductDemoViews>(session);

            return loader.LoadSingle(@"Select * from Elio_users_product_demo_views 
                                       where user_id = @user_id"
                                       , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersBillingType> GetAllActiveUsersBillingType(DBSession session)
        {
            DataLoader<ElioUsersBillingType> loader = new DataLoader<ElioUsersBillingType>(session);

            return loader.Load(@"Select * From Elio_users_billing_type Where is_active = 1");
        }

        public static ElioUsersBillingType GetUsersBillingTypeById(int id, DBSession session)
        {
            DataLoader<ElioUsersBillingType> loader = new DataLoader<ElioUsersBillingType>(session);

            return loader.LoadSingle(@"Select * From Elio_users_billing_type Where id = @id and is_active = 1"
                                       , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsersBillingType GetUsersBillingTypeByDescription(string typeDescription, DBSession session)
        {
            DataLoader<ElioUsersBillingType> loader = new DataLoader<ElioUsersBillingType>(session);

            return loader.LoadSingle(@"Select * From Elio_users_billing_type Where type_description = @type_description and is_active = 1"
                                       , DatabaseHelper.CreateStringParameter("@type_description", typeDescription));
        }

        public static List<ElioUsersBillingTypeIJTypePacketsIJPackets> GetAllUsersBillingTypeIJPacketsTypeIJPackets(DBSession session)
        {
            DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets> loader = new DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets>(session);

            return loader.Load(@"Select bt.id, bt.type_description, bt.is_active, btp.id AS billing_type_id, btp.pack_id, p.pack_description, p.sysdate, p.is_default, p.vat
                                 From Elio_users_billing_type bt
                                 INNER JOIN Elio_users_billing_type_packets btp 
                                    ON bt.id = btp.billing_type_id 
                                 INNER JOIN Elio_packets p 
                                    ON p.id = btp.pack_id");
        }

        public static int GetFreemiumBillingTypeId(DBSession session)
        {
            int typeId = 1;

            DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets> loader = new DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets>(session);

            ElioUsersBillingTypeIJTypePacketsIJPackets billingType = loader.LoadSingle(@"Select bt.id, bt.type_description, bt.is_active, btp.id AS billing_type_id, btp.pack_id, p.pack_description, p.sysdate, p.is_default, p.vat
                                                                                         From Elio_users_billing_type bt
                                                                                         INNER JOIN Elio_users_billing_type_packets btp 
                                                                                            ON bt.id = btp.billing_type_id 
                                                                                         INNER JOIN Elio_packets p 
                                                                                            ON p.id = btp.pack_id 
                                                                                         WHERE 1 = 1
                                                                                         AND p.is_active = 1
                                                                                         AND type_description = 'Freemium'");
            if (billingType != null)
                typeId = billingType.BillingTypeId;
            else
                return typeId;

            return typeId;
        }

        public static List<ElioUsersBillingTypeIJTypePacketsIJPackets> GetAllUsersBillingTypeByTypeDescription(string typeDescription, DBSession session)
        {
            DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets> loader = new DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets>(session);

            return loader.Load(@"Select bt.id, bt.type_description, bt.is_active, btp.id AS billing_type_id, btp.pack_id, p.pack_description, p.sysdate, p.is_default, p.vat
                                 From Elio_users_billing_type bt
                                 INNER JOIN Elio_users_billing_type_packets btp 
                                    ON bt.id = btp.billing_type_id 
                                 INNER JOIN Elio_packets p 
                                    ON p.id = btp.pack_id 
                                 WHERE 1 = 1
                                 AND p.is_active = 1
                                 AND type_description = @type_description"
                                 , DatabaseHelper.CreateStringParameter("@type_description", typeDescription));
        }

        public static ElioUsersBillingTypeIJTypePacketsIJPackets GetUserBillingTypeByPacket(string packet, DBSession session)
        {
            DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets> loader = new DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets>(session);

            return loader.LoadSingle(@"Select bt.id, bt.type_description, bt.is_active, btp.id AS billing_type_id, btp.pack_id, p.pack_description, p.sysdate, p.is_default, p.vat
                                 From Elio_users_billing_type bt
                                 INNER JOIN Elio_users_billing_type_packets btp 
                                    ON bt.id = btp.billing_type_id 
                                 INNER JOIN Elio_packets p 
                                    ON p.id = btp.pack_id 
                                 WHERE 1 = 1
                                 AND p.is_active = 1
                                 AND p.pack_description = @pack_description"
                                 , DatabaseHelper.CreateStringParameter("@pack_description", packet));
        }

        public static int GetPremiumBillingTypeIdByPacketId(int packetId, DBSession session)
        {
            int typeId = 2;

            DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets> loader = new DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets>(session);

            ElioUsersBillingTypeIJTypePacketsIJPackets billingType = loader.LoadSingle(@"Select bt.id, bt.type_description, bt.is_active, btp.id AS billing_type_id, btp.pack_id, p.pack_description, p.sysdate, p.is_default, p.vat
                                                                                         From Elio_users_billing_type bt
                                                                                         INNER JOIN Elio_users_billing_type_packets btp 
                                                                                            ON bt.id = btp.billing_type_id 
                                                                                         INNER JOIN Elio_packets p 
                                                                                            ON p.id = btp.pack_id 
                                                                                         WHERE 1 = 1
                                                                                         AND p.is_active = 1
                                                                                         AND type_description = 'Premium'
                                                                                         AND p.id = @id"
                                                                                         , DatabaseHelper.CreateIntParameter("@id", packetId));
            if (billingType != null)
                typeId = billingType.BillingTypeId;
            else
                return typeId;

            return typeId;
        }

        public static int GetPremiumBillingTypeIdByStripePlanId(string planId, DBSession session)
        {
            int typeId = 2;

            DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets> loader = new DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets>(session);

            ElioUsersBillingTypeIJTypePacketsIJPackets billingType = loader.LoadSingle(@"Select bt.id, bt.type_description, bt.is_active, btp.id AS billing_type_id, btp.pack_id, p.pack_description, p.sysdate, p.is_default, p.vat
                                                                                         From Elio_users_billing_type bt
                                                                                         INNER JOIN Elio_users_billing_type_packets btp 
                                                                                            ON bt.id = btp.billing_type_id 
                                                                                         INNER JOIN Elio_packets p 
                                                                                            ON p.id = btp.pack_id 
                                                                                         WHERE 1 = 1
                                                                                         AND p.is_active = 1
                                                                                         AND type_description = 'Premium'
                                                                                         AND p.stripe_plan_id = @stripe_plan_id"
                                                                                         , DatabaseHelper.CreateStringParameter("@stripe_plan_id", planId));
            if (billingType != null)
                typeId = billingType.BillingTypeId;
            else
                return typeId;

            return typeId;
        }

        public static int GetPremiumBillingTypeIdByPacketDescription(string packet, DBSession session)
        {
            int typeId = 2;

            DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets> loader = new DataLoader<ElioUsersBillingTypeIJTypePacketsIJPackets>(session);

            ElioUsersBillingTypeIJTypePacketsIJPackets billingType = loader.LoadSingle(@"Select bt.id, bt.type_description, bt.is_active, btp.id AS billing_type_id, btp.pack_id, p.pack_description, p.sysdate, p.is_default, p.vat
                                                                                         From Elio_users_billing_type bt
                                                                                         INNER JOIN Elio_users_billing_type_packets btp 
                                                                                            ON bt.id = btp.billing_type_id 
                                                                                         INNER JOIN Elio_packets p 
                                                                                            ON p.id = btp.pack_id 
                                                                                         WHERE 1 = 1
                                                                                         AND p.is_active = 1
                                                                                         AND type_description = 'Premium'
                                                                                         AND p.pack_description = @pack_description"
                                                                                         , DatabaseHelper.CreateStringParameter("@pack_description", packet));
            if (billingType != null)
                typeId = billingType.BillingTypeId;
            else
                return typeId;

            return typeId;
        }

        public static List<ElioPackets> GetPacketsByUserBillingTypeDescription(string billingTypeDescription, DBSession session)
        {
            DataLoader<ElioPackets> loader = new DataLoader<ElioPackets>(session);

            return loader.Load(@"SELECT *
                                        FROM Elio_packets p 
                                        INNER JOIN Elio_users_billing_type_packets btp ON p.id = btp.pack_id 
                                        INNER JOIN Elio_users_billing_type bt ON bt.id = btp.billing_type_id
                                        WHERE 1 = 1
                                        AND p.is_active = 1
                                        AND type_description = @type_description"
                                        , DatabaseHelper.CreateStringParameter("@type_description", billingTypeDescription));
        }

        public static ElioPackets GetPacketDescriptionBySubscriptionID(string subscriptionId, DBSession session)
        {
            DataLoader<ElioPackets> loader = new DataLoader<ElioPackets>(session);
            return loader.LoadSingle(@"SELECT top 1 p.id, p.pack_description
                                        FROM Elio_packets p 
                                        INNER JOIN Elio_users_subscriptions us
                                            ON p.stripe_plan_id = us.plan_id    
                                        WHERE us.subscription_id = @subscription_id
                                        order by p.id"
                                , DatabaseHelper.CreateStringParameter("@subscription_id", subscriptionId));
        }

        public static ElioPackets GetPacketByUserBillingTypePacketId(int userBillingTypePacketId, DBSession session)
        {
            DataLoader<ElioPackets> loader = new DataLoader<ElioPackets>(session);

            return loader.LoadSingle(@"SELECT *
                                        FROM Elio_packets p 
                                        INNER JOIN Elio_users_billing_type_packets btp ON p.id = btp.pack_id 
                                        INNER JOIN Elio_users_billing_type bt ON bt.id = btp.billing_type_id
                                        WHERE 1 = 1
                                        AND p.is_active = 1
                                        AND btp.id = @id"
                                        , DatabaseHelper.CreateIntParameter("@id", userBillingTypePacketId));
        }

        public static List<ElioUsers> GetBillingUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load(@"select u.* 
                                    from dbo.Elio_users u
                                    inner join Elio_billing_user_orders buo on u.id = buo.user_id
                                    where is_public=1 
                                    and account_status = 1 
                                    and user_application_type = 1 
                                    and billing_type > 1
                                    and customer_stripe_id <> '' 
                                    and buo.is_paid = 1
                                    and buo.is_ready_to_use = 1
                                    and buo.order_status = 1
                                    and buo.order_type = 1 
                                    and u.id <> 2226
                                    order by u.id asc");
        }

        public static List<ElioBillingUserOrdersPayments> GetUserPaymentsByOrderId(int userId, int orderId, DBSession session)
        {
            DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);

            return loader.Load(@"Select * from Elio_billing_user_orders_payments
                                 where user_id = @user_id
                                 and order_id = @order_id
                                 order by current_period_start desc"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@order_id", orderId));
        }

        public static ElioUsers GetP2pDealUserByDealId(int p2pDealId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.LoadSingle(@"Select * from Elio_users u
                                        Inner join Elio_partner_to_partner_deals p2pd
                                            ON u.id = p2pd.reseller_id
                                        Where p2pd.id = @id"
                                     , DatabaseHelper.CreateIntParameter("@id", p2pDealId));
        }

        public static List<ElioPartnerToPartnerDeals> GetUserPartnerToPartnerDeals(int userId, int status, string opportunityName, DBSession session)
        {
            DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);

            string strQuery = @"Select * from Elio_partner_to_partner_deals
                                 where reseller_id = @reseller_id
                                 and is_public = 1
                                 and status = @status ";

            if (!string.IsNullOrEmpty(opportunityName))
                strQuery += " and opportunity_name like '" + opportunityName + "%' ";

            strQuery += " order by date_created desc";

            return loader.Load(strQuery
                                , DatabaseHelper.CreateIntParameter("@reseller_id", userId)
                                , DatabaseHelper.CreateIntParameter("@status", status));
        }

        public static DataTable GetUserPartnerToPartnerDealsIJCountriesTbl(int userId, int status, string opportunityName, DBSession session)
        {
            string strQuery = @"Select 
                                    c.country_name                                    
                                    ,p2pd.*
                                from Elio_partner_to_partner_deals p2pd
                                inner join Elio_countries c
	                                on c.id = p2pd.country_id
                                where p2pd.reseller_id = @reseller_id
                                    and p2pd.is_public = 1
                                    and p2pd.status = @status ";

            if (!string.IsNullOrEmpty(opportunityName))
                strQuery += " and opportunity_name like '" + opportunityName + "%' ";

            strQuery += " order by date_created desc";

            return session.GetDataTable(strQuery
                                    , DatabaseHelper.CreateIntParameter("@reseller_id", userId)
                                    , DatabaseHelper.CreateIntParameter("@status", status));
        }

        public static ElioPartnerToPartnerDeals GetPartnerToPartnerDealById(int id, DBSession session)
        {
            DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);

            return loader.LoadSingle(@"Select * from Elio_partner_to_partner_deals
                                 where id = @id"
                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static bool HasP2PDeals(int resellerId, int status, DBSession session)
        {
            return session.GetDataTable(@"SELECT * FROM Elio_partner_to_partner_deals
                                          WHERE reseller_id = @reseller_id
                                          AND is_public = 1
                                          AND status = @status"
                                        , DatabaseHelper.CreateIntParameter("@reseller_id", resellerId)
                                        , DatabaseHelper.CreateIntParameter("@status", status)).Rows.Count > 0;
        }

        public static List<ElioPartnerToPartnerDeals> GetP2PDeals(ElioUsers user, int status, DBSession session)
        {
            DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);

            string strQuery = @"SELECT * FROM Elio_partner_to_partner_deals
                                WHERE is_public = 1
                                AND status = @status ";

            strQuery += (user.CompanyType == Types.Vendors.ToString()) ? " AND vendor_id = @user_id" : " AND reseller_id = @user_id";

            return loader.Load(strQuery
                        , DatabaseHelper.CreateIntParameter("@user_id", user.Id)
                        , DatabaseHelper.CreateIntParameter("@status", status));
        }

        //public static List<ElioPartnerToPartnerDeals> GetP2PDealsFromOtherResellers(int resellerId, int status, DBSession session)
        //{
        //      DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);
        //
        //      return loader.Load(@"SELECT * FROM Elio_partner_to_partner_deals p2p
        //                           INNER JOIN Elio_collaboration_vendors_resellers cvr
        //                              ON cvr.master_user_id = p2p.vendor_id
        //                           WHERE p2p.is_public = 1
        //                           AND status = @status 
        //                           AND cvr.partner_user_id = @partner_user_id"
        //                       , DatabaseHelper.CreateIntParameter("@partner_user_id", resellerId)
        //                       , DatabaseHelper.CreateIntParameter("@status", status));
        //}

        public static List<ElioPartnerToPartnerSubIndustries> GetP2PSubIndustries(int userId, int p2pId, DBSession session)
        {
            DataLoader<ElioPartnerToPartnerSubIndustries> loader = new DataLoader<ElioPartnerToPartnerSubIndustries>(session);

            return loader.Load(@"SELECT * FROM Elio_partner_to_partner_sub_industries                                     
                                    WHERE p2p_id = @p2p_id
                                    AND user_id = @user_id"
                        , DatabaseHelper.CreateIntParameter("@p2p_id", p2pId)
                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioPartnerToPartnerSubIndustriesIJSubIndustriesGroupItemIJSubIndustriesGroup> GetP2PSubIndustriesIJSubIndustriesGroupItemIJSubIndustriesGroup(int userId, int p2pId, DBSession session)
        {
            DataLoader<ElioPartnerToPartnerSubIndustriesIJSubIndustriesGroupItemIJSubIndustriesGroup> loader = new DataLoader<ElioPartnerToPartnerSubIndustriesIJSubIndustriesGroupItemIJSubIndustriesGroup>(session);

            return loader.Load(@"SELECT p2p_si.id, user_id, p2p_id, sub_industry_group_item_id, p2p_si.sysdate, p2p_si.last_updated, 
                                    sigi.description, sigi.is_public, sigi.opportunities,
                                    sig.id as group_id, sig.group_description
                                    FROM Elio_partner_to_partner_sub_industries p2p_si
                                    inner join Elio_sub_industries_group_items sigi
	                                    on sigi.id = p2p_si.sub_industry_group_item_id
                                    inner join Elio_sub_industries_group sig
	                                    on sig.id = sigi.sub_industies_group_id
                                    WHERE p2p_id = @p2p_id
                                    AND user_id = @user_id"
                        , DatabaseHelper.CreateIntParameter("@p2p_id", p2pId)
                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static bool ExistP2PSubIndustryByUser(int userId, int p2pId, int subIndustryGroupItemId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT COUNT(*) AS count FROM Elio_partner_to_partner_sub_industries
                                          WHERE user_id = @user_id
                                          AND p2p_id = @p2p_id
                                          AND sub_industry_group_item_id = @sub_industry_group_item_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@p2p_id", p2pId)
                                        , DatabaseHelper.CreateIntParameter("@sub_industry_group_item_id", subIndustryGroupItemId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static void DeleteP2PSubIndustryByUser(int userId, int p2pId, int subIndustryGroupItemId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"DELETE FROM Elio_partner_to_partner_sub_industries
                                          WHERE user_id = @user_id
                                          AND p2p_id = @p2p_id
                                          AND sub_industry_group_item_id = @sub_industry_group_item_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@p2p_id", p2pId)
                                        , DatabaseHelper.CreateIntParameter("@sub_industry_group_item_id", subIndustryGroupItemId));
        }

        public static List<ElioPartnerToPartnerDeals> GetP2pDealsByCriteria(int resellerId, int status, DBSession session)
        {
            DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);

            List<ElioPartnerToPartnerDeals> p2pDeals = GetOtherResellersP2pDeals(resellerId, status, session);

            if (p2pDeals.Count == 0)
            {
                p2pDeals = GetOtherResellersP2pDealsOfSameVendors(resellerId, status, session);
                if (p2pDeals.Count == 0)
                {
                    p2pDeals = GetOtherResellersP2pDealsBySubIndustriesAndLocation(resellerId, status, "", session);
                    if (p2pDeals.Count == 0)
                        return null;
                }
            }

            return p2pDeals;
        }

        public static List<ElioPartnerToPartnerDeals> GetOtherResellersP2pDeals(int resellerId, int status, DBSession session)
        {
            DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);

            return loader.Load(@"Select distinct p2p.*
                                        --partner_user_id, p2p_sub.id as p2p_sub_id, p2p_sub.sub_industry_group_item_id, p2p.id as p2p_id, c2.country_name as p2p_country, p2p.country_id as p2p_country_id, p2p.opportunity_name, u.country as user_country, c.id as user_country_id
                                from Elio_partner_to_partner_deals p2p2
                                inner join Elio_collaboration_vendors_resellers cvr
	                                on p2p2.reseller_id = cvr.partner_user_id
                                inner join Elio_users u 
	                                on cvr.partner_user_id = u.id
                                inner join Elio_countries c
	                                on c.country_name = u.country
                                inner join Elio_partner_to_partner_deals p2p 
	                                on p2p.reseller_id = u.id
                                inner join Elio_countries c2
	                                on c2.id = p2p.country_id
                                inner join Elio_partner_to_partner_sub_industries p2p_sub
	                                on p2p_sub.p2p_id = p2p.id
                                inner join Elio_users_sub_industries_group_items usigi
	                                on usigi.user_id = p2p_sub.user_id and usigi.sub_industry_group_item_id = p2p_sub.sub_industry_group_item_id

                                Where master_user_id in 
                                (
	                                Select master_user_id from Elio_collaboration_vendors_resellers cvr2
                                    Where partner_user_id = @user_id
                                )
                                and p2p.partner_user_id <> @user_id
                                and p2p.country_id = c.id
                                and p2p.status = @status"
                            , DatabaseHelper.CreateIntParameter("@user_id", resellerId)
                            , DatabaseHelper.CreateIntParameter("@status", status));
        }

        public static List<ElioPartnerToPartnerDeals> GetOtherResellersP2pDealsOfSameVendors(int resellerId, int status, DBSession session)
        {
            DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);

            return loader.Load(@"Select p2p.*
                                    from Elio_partner_to_partner_deals p2p
                                    inner join Elio_collaboration_vendors_resellers cvr
	                                    on p2p.reseller_id = cvr.partner_user_id
                                    Where master_user_id in 
                                    (
	                                    Select master_user_id from Elio_collaboration_vendors_resellers cvr2
                                        Where partner_user_id = @user_id 
                                    )
                                    and p2p.partner_user_id <> @user_id
                                    and p2p.status = @status"
                            , DatabaseHelper.CreateIntParameter("@user_id", resellerId)
                            , DatabaseHelper.CreateIntParameter("@status", status));
        }

        public static List<ElioPartnerToPartnerDeals> GetOtherResellersP2pDealsBySubIndustriesAndLocation(int resellerId, int status, string opportunityName, DBSession session)
        {
            DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);

            string strQuery = @"Select distinct p2p.*
                                from Elio_partner_to_partner_deals p2p
                                inner join Elio_users u 
	                                on p2p.reseller_id = u.id
                                inner join Elio_countries c
	                                on c.country_name = u.country
                                inner join Elio_countries c2
	                                on c2.id = p2p.country_id and c2.country_name = u.country
                                inner join Elio_partner_to_partner_sub_industries p2p_sub
	                                on p2p_sub.p2p_id = p2p.id
                                inner join Elio_users_sub_industries_group_items usigi
	                                on usigi.user_id = p2p_sub.user_id 
	                                and usigi.sub_industry_group_item_id = p2p_sub.sub_industry_group_item_id
                                inner join Elio_users_sub_industries_group_items usigi2
                                    on usigi2.user_id = @user_id
                                    and usigi2.sub_industry_group_item_id = p2p_sub.sub_industry_group_item_id
                                where p2p.reseller_id <> @user_id
                                and p2p.status = @status";

            if (!string.IsNullOrEmpty(opportunityName))
                strQuery += " and opportunity_name like '" + opportunityName + "%'";

            return loader.Load(strQuery
                            , DatabaseHelper.CreateIntParameter("@user_id", resellerId)
                            , DatabaseHelper.CreateIntParameter("@status", status));
        }

        public static DataTable GetOtherResellersP2pDealsBySubIndustriesAndLocationTbl(int resellerId, int status, string opportunityName, DBSession session)
        {
            string strQuery = @"Select distinct p2p.*
                                from Elio_partner_to_partner_deals p2p
                                inner join Elio_users u 
	                                on p2p.reseller_id = u.id
                                inner join Elio_countries c
	                                on c.country_name = u.country
                                inner join Elio_countries c2
	                                on c2.id = p2p.country_id and c2.country_name = u.country
                                inner join Elio_partner_to_partner_sub_industries p2p_sub
	                                on p2p_sub.p2p_id = p2p.id
                                inner join Elio_users_sub_industries_group_items usigi
	                                on usigi.user_id = p2p_sub.user_id 
	                                and usigi.sub_industry_group_item_id = p2p_sub.sub_industry_group_item_id
                                inner join Elio_users_sub_industries_group_items usigi2
                                    on usigi2.user_id = @user_id
                                    and usigi2.sub_industry_group_item_id = p2p_sub.sub_industry_group_item_id
                                where p2p.reseller_id <> @user_id
                                and p2p.status = @status";

            if (!string.IsNullOrEmpty(opportunityName))
                strQuery += " and opportunity_name like '" + opportunityName + "%'";

            return session.GetDataTable(strQuery
                                        , DatabaseHelper.CreateIntParameter("@user_id", resellerId)
                                        , DatabaseHelper.CreateIntParameter("@status", status));
        }

        public static List<ElioPartnerToPartnerDeals> GetOtherResellersP2pDealsByCases(int resellerId, int status, string opportunityName, DBSession session)
        {
            DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);

            string strQuery = @";with vendors as 
                                (
	                                Select master_user_id from Elio_collaboration_vendors_resellers cvr
	                                Where cvr.partner_user_id = @user_id
                                )
                                --select * from vendors v

                                , resellers as
                                (
	                                Select partner_user_id
	                                from Elio_collaboration_vendors_resellers cvr
	                                inner join vendors v on v.master_user_id = cvr.master_user_id
	                                where partner_user_id not in (@user_id)
                                )

                                --select * from resellers

                                , p2p_by_countries as 
                                (
	                                select p2pd.*, c.country_name as p2p_country, u.country as user_country
	                                from Elio_partner_to_partner_deals p2pd
	                                inner join resellers r on r.partner_user_id = p2pd.reseller_id
	                                inner join Elio_countries c on c.id = p2pd.country_id
	                                inner join Elio_users u on u.id = p2pd.reseller_id
	                                --inner join vendors v on v.master_user_id = u.id
	                                where p2pd.is_active = 1 and p2pd.is_public = 1	
	                                and u.country = c.country_name
                                    and u.id = @user_id
                                )

                                --select * from p2p_by_countries

                                , p2p_sub_ind as 
                                (
	                                select p2p_c.id as p2p_id, p2p_c.p2p_country, p2p_sub.sub_industry_group_item_id, p2p_c.reseller_id 
	                                from p2p_by_countries p2p_c
	                                inner join dbo.Elio_partner_to_partner_sub_industries p2p_sub 
		                                on p2p_sub.p2p_id = p2p_c.id
                                )

                                --select * from p2p_sub_ind

                                , reseller_sub_ind as 
                                (
                                    select usigi.sub_industry_group_item_id, usigi.user_id
	                                from dbo.Elio_users_sub_industries_group_items usigi
	                                where usigi.user_id = @user_id

	                                --select p2p_sub_ind.p2p_id, p2p_sub_ind.reseller_id, p2p_sub_ind.sub_industry_group_item_id 
	                                --from p2p_sub_ind
	                                --inner join dbo.Elio_users_sub_industries_group_items usigi
		                            --    on usigi.sub_industry_group_item_id = p2p_sub_ind.sub_industry_group_item_id
			                        --        and usigi.user_id = p2p_sub_ind.reseller_id
                                )

                                , p2p as 
                                (
                                    select distinct (p2si.p2p_id), p2si.p2p_country
                                    from p2p_sub_ind p2si
	                                inner join reseller_sub_ind rsi
		                                on p2si.sub_industry_group_item_id = rsi.sub_industry_group_item_id

                                    --select distinct (rsi.p2p_id) from reseller_sub_ind rsi
                                    --inner join p2p_sub_ind p2si
	                                --on p2si.sub_industry_group_item_id = rsi.sub_industry_group_item_id
		                            --    and p2si.reseller_id = rsi.reseller_id
                                )

                                select *
                                from p2p
                                inner join Elio_partner_to_partner_deals p2pd
                                    on p2p.p2p_id = p2pd.id ";

            if (!string.IsNullOrEmpty(opportunityName))
                strQuery += " where opportunity_name like '" + opportunityName + "%' ";

            strQuery += " order by p2pd.date_created desc";

            List<ElioPartnerToPartnerDeals> p2pDeals = loader.Load(strQuery
                            , DatabaseHelper.CreateIntParameter("@user_id", resellerId)
                            , DatabaseHelper.CreateIntParameter("@status", status));

            if (p2pDeals.Count == 0)
            {
                return GetOtherResellersP2pDealsBySubIndustriesAndLocation(resellerId, status, opportunityName, session);
            }
            else
                return p2pDeals;
        }

        public static DataTable GetOtherResellersP2pDealsByCasesTbl(int resellerId, int status, string opportunityName, DBSession session)
        {
            DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);

            string strQuery = @";with vendors as 
                                                                        (
	                                                                        Select master_user_id from Elio_collaboration_vendors_resellers cvr
	                                                                        Where cvr.partner_user_id = @user_id
                                                                        )
                                                                        --select * from vendors v

                                                                        , resellers as
                                                                        (
	                                                                        Select partner_user_id
	                                                                        from Elio_collaboration_vendors_resellers cvr
	                                                                        inner join vendors v on v.master_user_id = cvr.master_user_id
	                                                                        where partner_user_id not in (@user_id)
                                                                        )

                                                                        --select * from resellers

                                                                        , p2p_by_countries as 
                                                                        (
	                                                                        select p2pd.*, c.country_name as p2p_country, u.country as user_country
	                                                                        from Elio_partner_to_partner_deals p2pd
	                                                                        inner join resellers r on r.partner_user_id = p2pd.reseller_id
	                                                                        inner join Elio_countries c on c.id = p2pd.country_id
	                                                                        inner join Elio_users u on u.id = p2pd.reseller_id
	                                                                        --inner join vendors v on v.master_user_id = u.id
	                                                                        where p2pd.is_active = 1 and p2pd.is_public = 1	
	                                                                        and u.country = c.country_name
                                                                            and u.id = @user_id
                                                                        )

                                                                        --select * from p2p_by_countries

                                                                        , p2p_sub_ind as 
                                                                        (
	                                                                        select p2p_c.id as p2p_id, p2p_c.p2p_country, p2p_sub.sub_industry_group_item_id, p2p_c.reseller_id 
	                                                                        from p2p_by_countries p2p_c
	                                                                        inner join dbo.Elio_partner_to_partner_sub_industries p2p_sub 
		                                                                        on p2p_sub.p2p_id = p2p_c.id
                                                                        )

                                                                        --select * from p2p_sub_ind

                                                                        , reseller_sub_ind as 
                                                                        (
                                                                            select usigi.sub_industry_group_item_id, usigi.user_id
	                                                                        from dbo.Elio_users_sub_industries_group_items usigi
	                                                                        where usigi.user_id = @user_id

	                                                                        --select p2p_sub_ind.p2p_id, p2p_sub_ind.reseller_id, p2p_sub_ind.sub_industry_group_item_id 
	                                                                        --from p2p_sub_ind
	                                                                        --inner join dbo.Elio_users_sub_industries_group_items usigi
		                                                                    --    on usigi.sub_industry_group_item_id = p2p_sub_ind.sub_industry_group_item_id
			                                                                --        and usigi.user_id = p2p_sub_ind.reseller_id
                                                                        )

                                                                        , p2p as 
                                                                        (
                                                                            select distinct (p2si.p2p_id), p2si.p2p_country
                                                                            from p2p_sub_ind p2si
	                                                                        inner join reseller_sub_ind rsi
		                                                                        on p2si.sub_industry_group_item_id = rsi.sub_industry_group_item_id

                                                                            --select distinct (rsi.p2p_id) from reseller_sub_ind rsi
                                                                            --inner join p2p_sub_ind p2si
	                                                                        --on p2si.sub_industry_group_item_id = rsi.sub_industry_group_item_id
		                                                                    --    and p2si.reseller_id = rsi.reseller_id
                                                                        )

                                                                        select 
                                                                             p2pd.id,p2pd.reseller_id,p2pd.opportunity_name,p2pd.deal_value
                                                                            ,u.company_name,p2pd.country_id,p2p.p2p_country as country
                                                                            ,p2pd.date_created,p2pd.last_updated
                                                                            ,p2pd.status,p2pd.is_active,p2pd.is_public
                                                                        from p2p
                                                                        inner join Elio_partner_to_partner_deals p2pd
                                                                            on p2p.p2p_id = p2pd.id
                                                                        left join Elio_users u
	                                                                        on u.id = p2pd.reseller_id ";

            if (!string.IsNullOrEmpty(opportunityName))
                strQuery += " where opportunity_name like '" + opportunityName + "%' ";

            strQuery += " order by p2pd.date_created desc";

            DataTable table = session.GetDataTable(strQuery
                            , DatabaseHelper.CreateIntParameter("@user_id", resellerId)
                            , DatabaseHelper.CreateIntParameter("@status", status));

            if (table.Rows.Count == 0)
            {
                return GetOtherResellersP2pDealsBySubIndustriesAndLocationTbl(resellerId, status, opportunityName, session);
            }
            else
                return table;
        }
    }
}