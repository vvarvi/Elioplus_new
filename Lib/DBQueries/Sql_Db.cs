using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DB;
using System.Data;
using WdS.ElioPlus.Lib.Enums;

namespace WdS.ElioPlus.Lib.DBQueries
{
    public class Sql_Db
    {
        public static ElioCommunityPostsComments GetCommentById(int id, DBSession session)
        {
            DataLoader<ElioCommunityPostsComments> loader = new DataLoader<ElioCommunityPostsComments>(session);

            return loader.LoadSingle("Select * from Elio_community_posts_comments where id=?", DatabaseHelper.CreateIntParameter("id", id));
        }

        public static int GetMaxDepth(int postId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select top(1) Elio_community_posts_comments.depth as depth from Elio_community_posts_comments where community_post_id=? order by depth desc"
                                                   , DatabaseHelper.CreateIntParameter("community_post_id", postId));

            return (!string.IsNullOrEmpty(table.Rows[0]["depth"].ToString())) ? Convert.ToInt32(table.Rows[0]["depth"]) : 0;
        }

        public static ElioCommunityPostsComments GetPostCommentById(int id, DBSession session)
        {
            DataLoader<ElioCommunityPostsComments> loader = new DataLoader<ElioCommunityPostsComments>(session);

            return loader.LoadSingle("Select * from Elio_community_posts_comments where id=@id", DatabaseHelper.CreateIntParameter("id", id));
        }

        public static List<ElioCommunityPostsCommentsIJUsers> GetPostAllCommentsIJUsers(int postId, DBSession session)
        {
            DataLoader<ElioCommunityPostsCommentsIJUsers> loader = new DataLoader<ElioCommunityPostsCommentsIJUsers>(session);

            return loader.Load("Select * from Elio_community_posts_comments " +
                               "inner join Elio_users on Elio_users.id=Elio_community_posts_comments.user_id " +
                               "where Elio_community_posts_comments.community_post_id=? and Elio_community_posts_comments.is_public=1 order by Elio_community_posts_comments.id desc"
                               , DatabaseHelper.CreateIntParameter("community_post_id", postId));
        }       

        public static ElioCommunityPosts GetPostByPostId(int postId, DBSession session)
        {
            DataLoader<ElioCommunityPosts> loader = new DataLoader<ElioCommunityPosts>(session);

            return loader.LoadSingle("Select * from Elio_community_posts where id=?"
                                     , DatabaseHelper.CreateIntParameter("id", postId));
        }

        public static ElioCommunityPostsVotes GetPostVotesByUserId(int userId, int postId, DBSession session)
        {
            DataLoader<ElioCommunityPostsVotes> loader = new DataLoader<ElioCommunityPostsVotes>(session);

            return loader.LoadSingle("Select * from Elio_community_posts_votes where user_id=? and community_post_id=?"
                                     , DatabaseHelper.CreateIntParameter("user_id", userId)
                                     , DatabaseHelper.CreateIntParameter("community_post_id", postId));
        }

        public static bool UserHasUpVotePost(int userId, int postId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_community_posts_votes where user_id=? and community_post_id=?"
                                                   , DatabaseHelper.CreateIntParameter("user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("community_post_id", postId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static List<ElioCommunityPostsIJUsers> GetAllCommunityPostsDetailsOrderBy(string orderBy, DBSession session)
        {
            DataLoader<ElioCommunityPostsIJUsers> loader = new DataLoader<ElioCommunityPostsIJUsers>(session);

            return loader.Load("Select * from Elio_community_posts " +
                               "inner join Elio_users on Elio_users.id=Elio_community_posts.creator_user_id " +
                               "where Elio_community_posts.is_public=1 " + orderBy);
        }

        public static List<ElioCommunityPostsIJUsers> GetAllCommunityPostsDetails(DBSession session)
        {
            DataLoader<ElioCommunityPostsIJUsers> loader = new DataLoader<ElioCommunityPostsIJUsers>(session);

            return loader.Load("Select * from Elio_community_posts " +
                               "inner join Elio_users on Elio_users.id=Elio_community_posts.creator_user_id " +
                               "where Elio_community_posts.is_public=1 order by total_votes desc");
        }

        public static List<ElioCommunityPostsIJUsers> GetUserCommunityPostsDetails(int creatorId, DBSession session)
        {
            DataLoader<ElioCommunityPostsIJUsers> loader = new DataLoader<ElioCommunityPostsIJUsers>(session);

            return loader.Load("Select * from Elio_community_posts " +
                               "inner join Elio_users on Elio_users.id=Elio_community_posts.creator_user_id " +
                               "where creator_user_id=? and Elio_community_posts.is_public=1 order by total_votes desc"
                               , DatabaseHelper.CreateIntParameter("creator_user_id", creatorId));
        }

        public static List<ElioCommunityPosts> GetCommunityPostsIJCommentsByCommentUserId(int userId, DBSession session)
        {
            DataLoader<ElioCommunityPosts> loader = new DataLoader<ElioCommunityPosts>(session);

            return loader.Load("SELECT distinct Elio_community_posts.id, creator_user_id, topic, topic_url, total_votes FROM Elio_community_posts_comments " +
                               "inner join Elio_community_posts on Elio_community_posts.id=Elio_community_posts_comments.community_post_id " +
                               "where user_id=? and Elio_community_posts.is_public=1"
                               , DatabaseHelper.CreateIntParameter("user_id", userId));
        }

        public static List<ElioCommunityPostsIJUsers> GetUserUpVotedCommunityPostsDetails(int userId, DBSession session)
        {
            DataLoader<ElioCommunityPostsIJUsers> loader = new DataLoader<ElioCommunityPostsIJUsers>(session);

            return loader.Load("Select * from Elio_community_posts " +
                               "inner join Elio_users on Elio_users.id=Elio_community_posts.creator_user_id " +
                               "inner join Elio_community_posts_votes on Elio_community_posts_votes.community_post_id=Elio_community_posts.id " +
                               "where user_id=? and Elio_community_posts.is_public=1 order by total_votes desc"
                               , DatabaseHelper.CreateIntParameter("user_id", userId));
        }

        public static string GetEmailByUserId(int userId, DBSession session)
        {
            DataTable table=session.GetDataTable("Select email as email from Elio_users " +
                                                 "where id=?"
                                                 , DatabaseHelper.CreateIntParameter("id", userId));

            return table.Rows[0]["email"].ToString();
        }

        public static List<ElioUsers> GetUserFollowedUsers(int userId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load("Select * from Elio_users " +
                               "inner join Elio_community_user_profiles_followers on Elio_community_user_profiles_followers.follower_user_id=Elio_users.id " +
                               "where user_id=?"
                               , DatabaseHelper.CreateIntParameter("user_id", userId));
        }

        public static List<ElioUsers> GetUserFollowingUsers(int followingUserid, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

            return loader.Load("Select * from Elio_users " +
                               "inner join Elio_community_user_profiles_followers on Elio_community_user_profiles_followers.user_id=Elio_users.id " +
                               "where follower_user_id=?"
                               , DatabaseHelper.CreateIntParameter("follower_user_id", followingUserid));
        }

        public static List<ElioCommunityPostsComments> GetUserPostCommentsGetUserPostCommentsIJPosts(int userId, DBSession session)
        {
            DataLoader<ElioCommunityPostsComments> loader = new DataLoader<ElioCommunityPostsComments>(session);

            return loader.Load("Select * from Elio_community_posts_comments " +
                               "inner join Elio_community_posts on Elio_community_posts.id=Elio_community_posts_comments.community_post_id " +
                               "where user_id=? and Elio_community_posts_comments.is_public=1 and Elio_community_posts.is_public=1 ", DatabaseHelper.CreateIntParameter("user_id", userId));
        }

        public static ElioCommunityPostsIJUsers GetCommunityPostsDetailsById(int postId, DBSession session)
        {
            DataLoader<ElioCommunityPostsIJUsers> loader = new DataLoader<ElioCommunityPostsIJUsers>(session);

            return loader.LoadSingle("Select * from Elio_community_posts " +
                               "inner join Elio_users on Elio_users.id=Elio_community_posts.creator_user_id " +
                               "where Elio_community_posts.is_public=1 and Elio_community_posts.id=?"
                               , DatabaseHelper.CreateIntParameter("id", postId));
        }

        public static ElioCommunityPosts GetCommunityPostsById(int id, DBSession session)
        {
            DataLoader<ElioCommunityPosts> loader = new DataLoader<ElioCommunityPosts>(session);

            return loader.LoadSingle("Select * from Elio_community_posts where id=?", DatabaseHelper.CreateIntParameter("id", id));
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

        public static List<ElioRoles> GetUserRoles(int userId, DBSession session)
        {
            DataLoader<ElioRoles> loader = new DataLoader<ElioRoles>(session);

            return loader.Load("Select role_description from Elio_roles " +
                               "inner join Elio_users_roles on Elio_users_roles.elio_role_id=Elio_roles.id " +
                               "inner join Elio_users on Elio_users.id=Elio_users_roles.user_id " +
                               "where Elio_users.id=?"
                                                      , DatabaseHelper.CreateIntParameter("@id", userId));
        }

        public static bool IsUserAdministrator(int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(Elio_users.id) as count from Elio_users with (nolock) " +
                                                   "inner join Elio_users_roles on Elio_users_roles.user_id=Elio_users.id " +
                                                   "inner join Elio_roles on Elio_roles.role_description='Administrator' and Elio_users.id=@id"
                                                   , DatabaseHelper.CreateIntParameter("@id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool HasFollowedPost(int userId, int postId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_community_posts_followers " +
                                                   "where follower_user_id=? and community_post_id=?"
                                                   , DatabaseHelper.CreateIntParameter("@follower_user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@community_post_id", postId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool IsFollowingUser(int followerUserId, int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_community_user_profiles_followers " +
                                                   "where follower_user_id=? and user_id=?"
                                                   , DatabaseHelper.CreateIntParameter("@follower_user_id", followerUserId)
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static bool AllowCompanyToRate(int visitorId, int reviewCompanyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_user_partner_program_rating where company_id=? and visitor_id=?"
                                                    , DatabaseHelper.CreateIntParameter("@company_id", reviewCompanyId)
                                                    , DatabaseHelper.CreateIntParameter("@visitor_id", visitorId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? true : false;
        }

        public static bool SendCommunityNotificationEmailByEmailId(int userId, int communityEmailNotificationsId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_community_user_email_notifications " +
                                                   "where user_id=? and community_email_notifications_id=?"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateIntParameter("@community_email_notifications_id", communityEmailNotificationsId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 0 ? true : false;
        }

        public static List<ElioUserPartnerProgramRating> GetCompanyRating(int companyId, DBSession session)
        {
            DataLoader<ElioUserPartnerProgramRating> loader = new DataLoader<ElioUserPartnerProgramRating>(session);

            return loader.Load("Select Rate from Elio_user_partner_program_rating where company_id=?"
                                , DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static decimal GetCompanyAverageRating(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select sum(Rate) as sum, COUNT(id) as count from Elio_user_partner_program_rating where company_id=?"
                                  , DatabaseHelper.CreateIntParameter("@company_id", companyId));

            if (table.Rows[0]["sum"] != null && Convert.ToInt32(table.Rows[0]["count"]) != 0)
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
            DataTable table = session.GetDataTable("Select sum(Rate) as sum from Elio_user_partner_program_rating where company_id=?"
                                                   , DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return (!string.IsNullOrEmpty(table.Rows[0]["sum"].ToString())) ? Convert.ToInt32(table.Rows[0]["sum"]) : 0;
        }

        public static int GetCompanyCountRatings(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_user_partner_program_rating where company_id=?"
                                  , DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return (!string.IsNullOrEmpty(table.Rows[0]["count"].ToString())) ? Convert.ToInt32(table.Rows[0]["count"]) : 0;
        }

        public static ElioUserPartnerProgramRating GetCompanySumRating(int companyId, DBSession session)
        {
            DataLoader<ElioUserPartnerProgramRating> loader = new DataLoader<ElioUserPartnerProgramRating>(session);

            return loader.LoadSingle("Select sum(Rate) from Elio_user_partner_program_rating where company_id=?"
                                     , DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static List<ElioUsersNotificationEmails> UserNotificationEmailCount(int userId, DBSession session)
        {
            DataLoader<ElioUsersNotificationEmails> loader = new DataLoader<ElioUsersNotificationEmails>(session);

            return loader.Load("select notification_email_date from Elio_users_notification_emails where user_id=?"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static void UpDateUser(ElioUsers user, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            loader.Update(user);
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
            session.GetDataTable("Delete from Elio_users_apies where user_id=? and api_id=?", DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                                                            , DatabaseHelper.CreateIntParameter("@api_id", apiId));

        }

        public static void DeleteUserIndustry(int userId, int industryId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_users_industries where user_id=? and industry_id=?", DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                                                            , DatabaseHelper.CreateIntParameter("@industry_id", industryId));

        }

        public static void DeleteUserPartner(int userId, int partnerId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_users_partners where user_id=? and partner_id=?", DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                                                            , DatabaseHelper.CreateIntParameter("@partner_id", partnerId));

        }

        public static void DeleteUserMarket(int userId, int marketId, DBSession session)
        {
            session.GetDataTable("Delete from Elio_users_markets where user_id=? and market_id=?", DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                                                            , DatabaseHelper.CreateIntParameter("@market_id", marketId));

        }

        public static List<Tbl1> GetTbl(DBSession session)
        {
            DataLoader<Tbl1> loader = new DataLoader<Tbl1>(session);
            return loader.Load("Select * from tbl1");
        }


        public static bool ExistUserFavoriteCompany(int userId, int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_favorites where user_id=? and company_id=?", DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                                                                                                        , DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistCompanyViewCompany(int companyViewsCompanyId, int interestedCompanyId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_companies_views_companies where company_views_company_id=? and interested_company_id=?"
                                                                                                                                        , DatabaseHelper.CreateIntParameter("@company_views_company_id", companyViewsCompanyId)
                                                                                                                                        , DatabaseHelper.CreateIntParameter("@interested_company_id", interestedCompanyId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static ElioCompanyDetails GetCompanyDetailsByCompanyId(int companyId, DBSession session)
        {
            DataLoader<ElioCompanyDetails> loader = new DataLoader<ElioCompanyDetails>(session);
            return loader.LoadSingle("Select * from Elio_users with (nolock) " +
                                     "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id " +
                                     "inner join Elio_industries on Elio_industries.id=Elio_users_industries.industry_id " +
                                     "inner join Elio_users_partners on Elio_users_partners.user_id=Elio_users.id " +
                                     "inner join Elio_partners on Elio_partners.id=Elio_users_partners.partner_id " +
                                     "inner join Elio_users_markets on Elio_users_markets.user_id=Elio_users.id " +
                                     "inner join Elio_markets on Elio_markets.id=Elio_users_markets.market_id " +
                                     "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id " +
                                     "inner join Elio_apies on Elio_apies.id=Elio_users_apies.api_id " +
                                     "inner join Elio_companies_views on Elio_companies_views.company_id=Elio_users.id " +
                                     "where Elio_users.id=? ", DatabaseHelper.CreateIntParameter("@id", companyId));
        }

        public static int GetCompanyTotalViews11(int companyId, DBSession session)
        {
            int totalViews = 0;
            DataLoader<ElioCompaniesViews> loader = new DataLoader<ElioCompaniesViews>(session);
            List<ElioCompaniesViews> views = loader.Load("Select sum(views) as totalViews from Elio_companies_views where company_id=?", DatabaseHelper.CreateIntParameter("@company_id", companyId));
            foreach (ElioCompaniesViews view in views)
            {
                totalViews += view.Views;
            }

            return totalViews;
        }

        public static int GetCompanyTotalViews(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select sum(views) as totalViews from Elio_companies_views where company_id=?", DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return (!string.IsNullOrEmpty(table.Rows[0]["totalViews"].ToString())) ? Convert.ToInt32(table.Rows[0]["totalViews"]) : 0;
        }

        public static int GetTotalRegisteredCompaniesByType(string companyType, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as totalCompanies from Elio_users with (nolock) where company_type=?", DatabaseHelper.CreateStringParameter("@company_type", companyType));

            return (!string.IsNullOrEmpty(table.Rows[0]["totalCompanies"].ToString())) ? Convert.ToInt32(table.Rows[0]["totalCompanies"]) : 0;
        }

        public static int GetTotalCompaniesByAccountStatus(int accountStatus, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as totalCompanies from Elio_users with (nolock) where account_status=?", DatabaseHelper.CreateIntParameter("@account_status", accountStatus));

            return (!string.IsNullOrEmpty(table.Rows[0]["totalCompanies"].ToString())) ? Convert.ToInt32(table.Rows[0]["totalCompanies"]) : 0;
        }

        public static int GetCompanyTotalReviews(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as totalReviews from Elio_user_program_review where company_id=? and is_public=1", DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return (!string.IsNullOrEmpty(table.Rows[0]["totalReviews"].ToString())) ? Convert.ToInt32(table.Rows[0]["totalReviews"]) : 0;
        }

        public static bool AllowUserToVote(int visitorId, int reviewId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select SUM(votes) as votes from Elio_user_program_review_votes where Elio_user_program_review_visitor_id=? and Elio_user_program_review_id=?"
                                                    , DatabaseHelper.CreateIntParameter("@Elio_user_program_review_visitor_id", visitorId)
                                                    , DatabaseHelper.CreateIntParameter("@Elio_user_program_review_id", reviewId));

            return (!string.IsNullOrEmpty(table.Rows[0]["votes"].ToString())) ? Convert.ToInt32(table.Rows[0]["votes"]) == 0 ? true : false : true;
        }

        public static int GetReviewTotalVotes(int reviewId, DBSession session)
        {
            DataTable table = session.GetDataTable("SELECT SUM(votes) as votes " +
                                                   "FROM Elio_user_program_review_votes " +
                                                   "where Elio_user_program_review_id=?"
                                                    , DatabaseHelper.CreateIntParameter("@Elio_user_program_review_id", reviewId));

            return Convert.ToInt32(table.Rows[0]["votes"]);
        }

        public static int GetCompanyTotalRating(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("SELECT SUM(votes) as totalVotes " +
                                                   "FROM Elio_user_program_review " +
                                                   "inner join Elio_user_program_review_votes on Elio_user_program_review_votes.Elio_user_program_review_id=Elio_user_program_review.id " +
                                                   "where company_id=?"
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
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_companies_views_companies where company_views_company_id=? and is_new=1"
                                                   , DatabaseHelper.CreateIntParameter("@company_views_company_id", companyId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static int GetCompanySavedProfiles(int companyId, DBSession session)
        {
            DataTable table = session.GetDataTable("Select count(id) as count from Elio_users_favorites where company_id=? ", DatabaseHelper.CreateIntParameter("@company_id", companyId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static List<ElioUsersIJIndustries> GetAllUsersByCompanyType(string type, DBSession session)
        {
            DataLoader<ElioUsersIJIndustries> loader = new DataLoader<ElioUsersIJIndustries>(session);
            return loader.Load("Select elio_users.id, company_name, overview,country, company_logo, views,industry_description from elio_users with (nolock) " +
                                "inner join Elio_users_industries on Elio_users_industries.user_id=Elio_users.id " +
                                "inner join Elio_industries on Elio_industries.id=Elio_users_industries.industry_id " +
                                "where company_type=?", DatabaseHelper.CreateStringParameter("@company_type", type));
        }

        public static List<ElioUsers> GetAllCompletedUsers(int userId, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select * from Elio_users with (nolock) where id<>? and account_status=1 order by company_name asc", DatabaseHelper.CreateIntParameter("id", userId));
        }

        public static List<ElioUsers> GetAllUsers(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select * from Elio_users with (nolock) where account_status<>0 order by company_name asc");
        }

        public static List<ElioUsers> GetAllUsersId(DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.Load("Select id, last_login,user_login_count,sysdate from Elio_users with (nolock)");
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
            return loader.Load("Select id, company_logo, country, company_name from Elio_users with (nolock) " +
                               " where features_no>0 order by features_no desc");
        }

        public static List<string> GetIndustiesDescriptionsIJUserIndustries(int userId, DBSession session)
        {
            List<string> vendorIndustries = new List<string>();

            DataLoader<ElioIndustriesIJIndustries> loader = new DataLoader<ElioIndustriesIJIndustries>(session);
            List<ElioIndustriesIJIndustries> industries= loader.Load("Select industry_description from Elio_industries " +
                                     "inner join Elio_users_industries on Elio_users_industries.industry_id=Elio_industries.id " +
                                     "where user_id=@user_id", DatabaseHelper.CreateIntParameter("user_id", userId));

            foreach (ElioIndustriesIJIndustries industry in industries)
            {
                vendorIndustries.Add(industry.IndustryDescription);
            }

            return vendorIndustries;
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
                                "where elio_users.id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsersIJApies GetUsersIJApiesIJViewsByCompanyId(int id, DBSession session)
        {
            DataLoader<ElioUsersIJApies> loader = new DataLoader<ElioUsersIJApies>(session);
            return loader.LoadSingle("Select top 1 elio_users.id, company_name, overview,country, company_logo, views,api_description from elio_users with (nolock) " +
                                "inner join Elio_users_apies on Elio_users_apies.user_id=Elio_users.id " +
                                "inner join Elio_apies on Elio_apies.id=Elio_users_apies.api_id " +
                                "left join Elio_companies_views on Elio_companies_views.company_id=elio_users.id " +
                                "where elio_users.id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetUserById(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select * from elio_users with (nolock) where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetCompanynameAndIdAndEmailAndOfficialEmailByName(string companyName, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select id, company_name, email, official_email from elio_users with (nolock) where company_name=?", DatabaseHelper.CreateStringParameter("@company_name", companyName));
        }

        public static ElioUsers GetUserForRoutingById(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select id, company_name, company_type, username from elio_users with (nolock) where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetCompanyForDetailsViewById(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select id, company_name, country, company_type from elio_users with (nolock) where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetCompanyEmailAndCompanyName(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select email, company_name from elio_users with (nolock) where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetCompanyEmailAndOfficialEmail(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select email, official_email from elio_users with (nolock) where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetCompanynameAndIdAndEmailAndOfficialEmailById(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select id, company_name, email, official_email from elio_users with (nolock) where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }


        public static string GetCompanyEmail(int id, DBSession session)
        {
            DataTable table = session.GetDataTable("Select email from elio_users with (nolock) where id=?", DatabaseHelper.CreateIntParameter("@id", id));

            return table.Rows[0]["email"].ToString();
        }

        public static ElioUsers GetUserEmails(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select email, official_email from elio_users with (nolock) where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetUserWebSite(int id, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select website from elio_users with (nolock) where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUsers GetUserByEmail(string email, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select * from elio_users with (nolock) where email=?", DatabaseHelper.CreateStringParameter("@email", email));
        }

        public static ElioUserEmailNotifications GetEmailNotificationByDescription(string description, DBSession session)
        {
            DataLoader<ElioUserEmailNotifications> loader = new DataLoader<ElioUserEmailNotifications>(session);
            return loader.LoadSingle("Select * from Elio_user_email_notifications where description=?", DatabaseHelper.CreateStringParameter("@description", description));
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

        public static ElioUsers GetUserByUsernameAndPassword(string username, string password, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            ElioUsers user = new ElioUsers();

            user = loader.LoadSingle("Select * from elio_users with (nolock) " +
                                    "where username=@username and password=@password "
                                                                       , DatabaseHelper.CreateStringParameter("@username", username)
                                                                       , DatabaseHelper.CreateStringParameter("@password", password));
            if (user == null)
            {
                user = loader.LoadSingle("Select * from elio_users with (nolock) " +
                                         "where (username_encrypted=@username_encrypted and password_encrypted=@password_encrypted) "
                                                                       , DatabaseHelper.CreateStringParameter("@username_encrypted", username)
                                                                       , DatabaseHelper.CreateStringParameter("@password_encrypted", password));
            }

            return user;
        }
       
        public static ElioUsers GetUserByUsernameEncryptedAndPasswordEncrypted(string usernameEncrypted, string passwordEncrypted, DBSession session)
        {
            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
            return loader.LoadSingle("Select * from elio_users with (nolock) where username_encrypted=? and password_encrypted=?", DatabaseHelper.CreateStringParameter("@username_encrypted", usernameEncrypted)
                                                                                               , DatabaseHelper.CreateStringParameter("@password_encrypted", passwordEncrypted));
        }

        public static ElioCompaniesViews GetCompanyViewsByCompanyId(int companyId, DBSession session)
        {
            DataLoader<ElioCompaniesViews> loader = new DataLoader<ElioCompaniesViews>(session);
            return loader.LoadSingle("Select * from Elio_companies_views where company_id=? and (date>=? and date<=?) order by date asc", DatabaseHelper.CreateIntParameter("@company_id", companyId)
                                                                                                      , DatabaseHelper.CreateDateTimeParameter("@date", DateTime.Now.ToString("yyyy-MM-dd"))
                                                                                                      , DatabaseHelper.CreateDateTimeParameter("@date", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));
        }

        public static List<ElioCompaniesViewsCompaniesIJCompanies> GetCompanyViewsCompaniesByCompanyId(int companyId, DBSession session)
        {
            DataLoader<ElioCompaniesViewsCompaniesIJCompanies> loader = new DataLoader<ElioCompaniesViewsCompaniesIJCompanies>(session);
            return loader.Load("SELECT * FROM Elio_companies_views_companies " +
                               "inner join Elio_users on Elio_users.id=Elio_companies_views_companies.interested_company_id " +
                               "where company_views_company_id=? order by Elio_companies_views_companies.sysdate desc", DatabaseHelper.CreateIntParameter("@company_views_company_id", companyId));
        }

        public static ElioCompaniesViewsCompanies GetCompanyViewsCompany(int companyViesCompanyId, int interestedCompanyId, DBSession session)
        {
            DataLoader<ElioCompaniesViewsCompanies> loader = new DataLoader<ElioCompaniesViewsCompanies>(session);
            return loader.LoadSingle("Select * from Elio_companies_views_companies where company_views_company_id=? and interested_company_id=?", DatabaseHelper.CreateIntParameter("@company_views_company_id", companyViesCompanyId)
                                                                                                                             , DatabaseHelper.CreateIntParameter("@interested_company_id", interestedCompanyId));
        }

        public static ElioCompaniesViewsCompanies GetCompanyViewsCompanyById(int id, DBSession session)
        {
            DataLoader<ElioCompaniesViewsCompanies> loader = new DataLoader<ElioCompaniesViewsCompanies>(session);
            return loader.LoadSingle("Select * from Elio_companies_views_companies where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioCompaniesViews> GetCountCompanyViewsByCompanyId(int companyId, DBSession session)
        {
            DataLoader<ElioCompaniesViews> loader = new DataLoader<ElioCompaniesViews>(session);
            return loader.Load("Select * from Elio_companies_views where company_id=? order by date asc", DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static DataTable GetCompanyViewsByCompanyIdForChart(int companyId, DBSession session)
        {
            return session.GetDataTable("Select * from Elio_companies_views where company_id=? order by date", DatabaseHelper.CreateIntParameter("@company_id", companyId));
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

        public static List<ElioUserTypes> GetUserTypes(DBSession session)
        {
            DataLoader<ElioUserTypes> loader = new DataLoader<ElioUserTypes>(session);
            return loader.Load("Select * from elio_user_types");
        }

        public static List<ElioIndustries> GetIndustries(DBSession session)
        {
            DataLoader<ElioIndustries> loader = new DataLoader<ElioIndustries>(session);
            return loader.Load("Select * from elio_industries where is_public=1");
        }

        public static ElioIndustries GetIndustryById(int industryId, DBSession session)
        {
            DataLoader<ElioIndustries> loader = new DataLoader<ElioIndustries>(session);
            return loader.LoadSingle("Select * from elio_industries where id=?"
                                     , DatabaseHelper.CreateIntParameter("id",industryId));
        }

        public static List<ElioIndustries> GetUsersIndustries(int userId, DBSession session)
        {
            DataLoader<ElioIndustries> loader = new DataLoader<ElioIndustries>(session);
            return loader.Load("Select * from elio_industries inner join elio_users_industries on elio_users_industries.industry_id=elio_industries.id " +
                               "where user_id=?", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioMarkets> GetUsersMarkets(int userId, DBSession session)
        {
            DataLoader<ElioMarkets> loader = new DataLoader<ElioMarkets>(session);
            return loader.Load("Select * from elio_markets inner join elio_users_markets on elio_users_markets.market_id=elio_markets.id " +
                               "where user_id=?", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersMessages> GetUsersMessagesByStatus(int userId, int isNew, int isDeleted, DBSession session)
        {
            DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
            return loader.Load("Select * from elio_users_messages where receiver_user_id=? and is_new=? and deleted=? order by sysdate desc", DatabaseHelper.CreateIntParameter("@receiver_user_id", userId)
                                                                                                                      , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                                                                                      , DatabaseHelper.CreateIntParameter("@deleted", isDeleted));
        }

        public static List<ElioUsersMessages> GetUsersDeletedMessagesByStatus(int userId, int isDeleted, DBSession session)
        {
            DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
            return loader.Load("Select * from elio_users_messages where receiver_user_id=? and deleted=? order by sysdate desc", DatabaseHelper.CreateIntParameter("@receiver_user_id", userId)
                                                                                                         , DatabaseHelper.CreateIntParameter("@deleted", isDeleted));
        }

        public static ElioUsersMessages GetUsersMessageById(int id, DBSession session)
        {
            DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);
            return loader.LoadSingle("Select * from elio_users_messages where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioCompaniesViewsCompanies GetUsersRecentLeadById(int id, DBSession session)
        {
            DataLoader<ElioCompaniesViewsCompanies> loader = new DataLoader<ElioCompaniesViewsCompanies>(session);
            return loader.LoadSingle("Select * from Elio_companies_views_companies where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioUsersFavorites> GetUserFavorites(int userId, DBSession session)
        {
            DataLoader<ElioUsersFavorites> loader = new DataLoader<ElioUsersFavorites>(session);
            return loader.Load("Select * from elio_users_favorites where user_id=?", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersFavoritesIJUsers> GetUserFavoritesIJUsers(int userId, DBSession session)
        {
            DataLoader<ElioUsersFavoritesIJUsers> loader = new DataLoader<ElioUsersFavoritesIJUsers>(session);
            return loader.Load("Select * from elio_users_favorites " +
                               "inner join elio_users on elio_users.id=Elio_users_favorites.company_id " +
                               "where user_id=? order by elio_users_favorites.sysdate desc"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersFavorites> GetUserFavouritedCompanies(int companyId, DBSession session)
        {
            DataLoader<ElioUsersFavorites> loader = new DataLoader<ElioUsersFavorites>(session);
            return loader.Load("Select * from elio_users_favorites where company_id=?", DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static List<ElioUsersFavoritesIJUsers> GetUserFavouritedCompaniesIJUsers(int companyId, DBSession session)
        {
            DataLoader < ElioUsersFavoritesIJUsers > loader = new DataLoader<ElioUsersFavoritesIJUsers>(session);
            return loader.Load("Select * from elio_users_favorites " +
                               "inner join elio_users on elio_users.id=Elio_users_favorites.user_id " +
                               "where company_id=? order by elio_users_favorites.sysdate desc"
                                                   , DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static ElioUserPartnerProgramRating GetUserRatingsById(int id, DBSession session)
        {
            DataLoader<ElioUserPartnerProgramRating> loader = new DataLoader<ElioUserPartnerProgramRating>(session);
            return loader.LoadSingle("Select * from Elio_user_partner_program_rating " +
                                     "where id=?"
                                                , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static List<ElioUserPartnerProgramRatingIJUsers> GetUserRatingsIJUsers(int companyId, DBSession session)
        {
            DataLoader<ElioUserPartnerProgramRatingIJUsers> loader = new DataLoader<ElioUserPartnerProgramRatingIJUsers>(session);
            return loader.Load("Select * from Elio_user_partner_program_rating " +
                               "inner join elio_users on elio_users.id=Elio_user_partner_program_rating.visitor_id " +
                               "where company_id=? order by Elio_user_partner_program_rating.sysdate desc"
                                                   , DatabaseHelper.CreateIntParameter("@company_id", companyId));
        }

        public static ElioUserProgramReviewVotes GetVisitorProgramsReviewVotesByReviewId(int reviewId, int visitorId, DBSession session)
        {
            DataLoader<ElioUserProgramReviewVotes> loader = new DataLoader<ElioUserProgramReviewVotes>(session);
            return loader.LoadSingle("Select * from Elio_user_program_review_votes where Elio_user_program_review_id=? and Elio_user_program_review_visitor_id=?"
                                                                , DatabaseHelper.CreateIntParameter("@Elio_user_program_review_id", reviewId)
                                                                , DatabaseHelper.CreateIntParameter("@Elio_user_program_review_visitor_id", visitorId));
        }

        public static List<ElioUserProgramReview> GetUserIdAllPublicReviews(int companyViewId, DBSession session)
        {
            DataLoader<ElioUserProgramReview> loader = new DataLoader<ElioUserProgramReview>(session);
            return loader.Load("Select id,visitor_id from Elio_user_program_review " +
                               "where company_id=? and is_public=1 order by id desc"
                                                                                    , DatabaseHelper.CreateIntParameter("@company_id", companyViewId));
        }

        public static ElioUserProgramReview GetProgramsReviewById(int id, DBSession session)
        {
            DataLoader<ElioUserProgramReview> loader = new DataLoader<ElioUserProgramReview>(session);
            return loader.LoadSingle("Select * from Elio_user_program_review " +
                               "where id=?"
                                           , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioUserProgramReviewIJUsers GetUserReviewById(int reviewId, DBSession session)
        {
            DataLoader<ElioUserProgramReviewIJUsers> loader = new DataLoader<ElioUserProgramReviewIJUsers>(session);
            return loader.LoadSingle("Select Elio_user_program_review.id,username,company_name,company_logo,Elio_user_program_review.sysdate,review,Elio_user_program_review.is_public,account_status from Elio_user_program_review " +
                                     "left join Elio_users on Elio_users.id=Elio_user_program_review.visitor_id " +
                //"left join Elio_user_program_review_votes on Elio_user_program_review_votes.Elio_user_program_review_id=Elio_user_program_review.id " +
                                     "where Elio_user_program_review.id=?"
                                                                         , DatabaseHelper.CreateIntParameter("@id", reviewId));
        }

        public static ElioUsersFavorites GetUsersFavoritesById(int id, DBSession session)
        {
            DataLoader<ElioUsersFavorites> loader = new DataLoader<ElioUsersFavorites>(session);
            return loader.LoadSingle("Select * from elio_users_favorites where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioCompaniesViewsCompanies GetCompaniesViewsCompaniesById(int id, DBSession session)
        {
            DataLoader<ElioCompaniesViewsCompanies> loader = new DataLoader<ElioCompaniesViewsCompanies>(session);
            return loader.LoadSingle("Select * from Elio_companies_views_companies where id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static ElioCompaniesViewsCompaniesIJCompanies GetCompaniesViewsCompaniesByInterestedCompanyId(int interestedCompanyId, DBSession session)
        {
            DataLoader<ElioCompaniesViewsCompaniesIJCompanies> loader = new DataLoader<ElioCompaniesViewsCompaniesIJCompanies>(session);
            return loader.LoadSingle("Select * from Elio_companies_views_companies where interested_company_id=?", DatabaseHelper.CreateIntParameter("@interested_company_id", interestedCompanyId));
        }

        public static List<ElioPartners> GetUsersPartners(int userId, DBSession session)
        {
            DataLoader<ElioPartners> loader = new DataLoader<ElioPartners>(session);
            return loader.Load("Select * from elio_partners inner join elio_users_partners on elio_users_partners.partner_id=elio_partners.id " +
                               "where user_id=?", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioApies> GetUsersApies(int userId, DBSession session)
        {
            DataLoader<ElioApies> loader = new DataLoader<ElioApies>(session);
            return loader.Load("Select * from elio_apies inner join elio_users_apies on elio_users_apies.api_id=elio_apies.id " +
                               "where user_id=?", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioPartners> GetPartners(DBSession session)
        {
            DataLoader<ElioPartners> loader = new DataLoader<ElioPartners>(session);
            return loader.Load("Select * from elio_partners where is_public=1");
        }

        public static List<ElioMarkets> GetMarkets(DBSession session)
        {
            DataLoader<ElioMarkets> loader = new DataLoader<ElioMarkets>(session);
            return loader.Load("Select * from elio_markets where is_public=1");
        }

        public static List<ElioApies> GetApies(DBSession session)
        {
            DataLoader<ElioApies> loader = new DataLoader<ElioApies>(session);
            return loader.Load("Select * from elio_apies where is_public=1");
        }

        public static List<ElioCountries> GetCountries(DBSession session)
        {
            DataLoader<ElioCountries> loader = new DataLoader<ElioCountries>(session);
            return loader.Load("Select * from elio_countries order by country_name asc");
        }

        public static bool ExistEmail(string email, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where email=?", DatabaseHelper.CreateStringParameter("@email", email));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistEmailToOtherUser(string email, int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where email=? and id<>?"
                                                   , DatabaseHelper.CreateStringParameter("@email", email)
                                                   , DatabaseHelper.CreateIntParameter("id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUsername(string username, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where username=?", DatabaseHelper.CreateStringParameter("@username", username));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUsernameToOtherUser(string username, int userId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users with (nolock) where username=? and id<>?"
                                                   , DatabaseHelper.CreateStringParameter("@username", username)
                                                   , DatabaseHelper.CreateIntParameter("id",userId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserIndustry(int userId, int industryId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_industries where user_id=? and industry_id=?"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@industry_id", industryId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static void DeleteCommunityUserEmailNotification(int id, DBSession session)
        {
            session.GetDataTable("Delete from Elio_community_user_email_notifications where community_email_notifications_id=?", DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static bool ExistCommunityUserEmailNotification(int userId, int emailNotificationId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_community_user_email_notifications where user_id=? and community_email_notifications_id=?"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@community_email_notifications_id", emailNotificationId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserPartner(int userId, int partnerId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_partners where user_id=? and partner_id=?"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@partner_id", partnerId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserMarket(int userId, int marketId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_markets where user_id=? and market_id=?"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@market_id", marketId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool ExistUserApi(int userId, int apiId, DBSession session)
        {
            DataTable table = session.GetDataTable("select Count(id) as count from Elio_users_apies where user_id=? and api_id=?"
                                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                    , DatabaseHelper.CreateIntParameter("@api_id", apiId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }
    }
}