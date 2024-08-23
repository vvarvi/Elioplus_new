using System;
using System.Collections.Generic;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Controls.Dashboard.Charts
{
    public partial class BackUpLiveDB : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();
        private DBLiveSession lvSession = new DBLiveSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void HandleBothConnections(bool open)
        {
            if (open)
            {
                session.OpenConnection();
                lvSession.OpenConnection();

                session.BeginTransaction();
            }
            else
            {
                session.CloseConnection();
                lvSession.CloseConnection();

                session.CommitTransaction();
            }
        }

        #region Buttons

        protected void Elio_apies_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                lvSession.OpenConnection();

                DataLiveLoader<ElioApies> loader = new DataLiveLoader<ElioApies>(lvSession);

                List<ElioApies> apies = loader.Load("Select * from Elio_apies");
                
                session.BeginTransaction();

                foreach (ElioApies api in apies)
                {
                    ElioApies ap = new ElioApies();

                    ap.ApiDescription = api.ApiDescription;
                    ap.IsPublic = api.IsPublic;

                    DataLoader<ElioApies> loader1 = new DataLoader<ElioApies>(session);
                    loader1.Insert(ap);
                }

                session.CommitTransaction();
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

        protected void Elio_community_email_notifications_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                
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

        protected void Elio_community_posts_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                
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

        protected void Elio_community_posts_votes_OnClick(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Elio_community_user_email_notifications_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_community_user_profiles_followers_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_companies_views_OnClick(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Elio_companies_views_companies_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_countries_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_industries_OnClick(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Elio_markets_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_partners_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_roles_OnClick(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Elio_sub_industries_group_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_sub_industries_group_items_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_user_email_notifications_OnClick(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Elio_user_partner_program_rating_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_user_program_review_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_user_program_review_votes_OnClick(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Elio_user_types_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void elio_users_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_users_apies_OnClick(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Elio_users_favorites_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_users_industries_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_users_login_statistics_OnClick(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Elio_users_markets_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_users_messages_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_users_notification_emails_OnClick(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Elio_users_partners_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_users_roles_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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

        protected void Elio_users_sub_industries_group_items_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();


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