using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Hangfire.Server;
using Hangfire.SqlServer;
using Hangfire.Storage;
using Microsoft.Owin;
using Org.BouncyCastle.Asn1.Ocsp;
using Owin;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;

[assembly: OwinStartup(typeof(WdS.ElioPlus.Startup))]

namespace WdS.ElioPlus
{
    public class Startup
    {
        DBSession session = new DBSession();

        private BackgroundJobServer _backgroundJobServer;

        [Obsolete]
        public void Configuration(IAppBuilder app)
        {
            try
            {
                session.OpenConnection();

                var options = new DashboardOptions
                {
                    AuthorizationFilters = new[]
                    {
                        new LocalRequestsOnlyAuthorizationFilter()
                    }
                };

                //GlobalConfiguration.Configuration
                ////.UseSqlServerStorage("ConnectionString");
                //.UseSqlServerStorage(@"Server=mongodb://eliousr:QcR2#tPp@10.215.0.1/ElioHangfireStage;");

                var migrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                    BackupStrategy = new CollectionMongoBackupStrategy()
                };

                string conStringMongoDB = (ConfigurationManager.ConnectionStrings["ConnectionStringHangfire"] != null && ConfigurationManager.ConnectionStrings["ConnectionStringHangfire"].ToString() != "") ? ConfigurationManager.ConnectionStrings["ConnectionStringHangfire"].ToString() : "";
                string mongoDBName = (ConfigurationManager.AppSettings["mongoDBNameHangfire"] != null && ConfigurationManager.AppSettings["mongoDBNameHangfire"].ToString() != "") ? ConfigurationManager.AppSettings["mongoDBNameHangfire"].ToString() : "";

                if (conStringMongoDB != "" && mongoDBName != "")
                {
                    GlobalConfiguration.Configuration
                    //.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    //.UseSimpleAssemblyNameTypeSerializer()
                    //.UseRecommendedSerializerSettings()
                    .UseMongoStorage(conStringMongoDB, mongoDBName, new MongoStorageOptions { MigrationOptions = migrationOptions });
                    //.UseMongoStorage("mongodb://eliousr:QcR2#tPp@10.215.0.1", "ElioHangfireStage", new MongoStorageOptions { MigrationOptions = migrationOptions });
                    //.UseSqlServerStorage("ConnectionString")
                    //.UseSqlServerStorage("data source=52.166.73.224;initial catalog=ElioPlus_DB_Hangfire;persist security info=False;user id=elioplu$u$er@dm!n!str@t0r;pwd=t0rn@d0v@g1985;packet size=8192;Connection Lifetime=900;Max Pool Size=200;Connection Timeout=240;Pooling=true; Integrated Security=True;", new SqlServerStorageOptions

                    //.UseSqlServerStorage(@"Server=mongodb://localhost:27017; Database=Elio_DB_Hangfire;");
                    //{                    
                    //    //CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    //    //SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    //    //QueuePollInterval = TimeSpan.Zero,
                    //    //UseRecommendedIsolationLevel = true,
                    //    //UsePageLocksOnDequeue = true,
                    //    //DisableGlobalLocks = true
                    //});

                    Logger.Info("Startup.cs --> Configuration() connected Mongo DB successfully!", "conStringMongoDB:" + conStringMongoDB, "mongoDBName:" + mongoDBName);

                    _backgroundJobServer = new BackgroundJobServer();

                    app.UseHangfireServer();

                    app.UseHangfireDashboard("/hangfire");

                    if (ConfigurationManager.AppSettings["WorkerForGetData"] != null && ConfigurationManager.AppSettings["WorkerForGetData"].ToString() == "true")
                        WorkerForGetData();

                    if (ConfigurationManager.AppSettings["WorkerForUpdateData"] != null && ConfigurationManager.AppSettings["WorkerForUpdateData"].ToString() == "true")
                        WorkerForUpdateData();

                    if (ConfigurationManager.AppSettings["WorkerForSendData"] != null && ConfigurationManager.AppSettings["WorkerForSendData"].ToString() == "true")
                        WorkerForSendData();

                    //RemoveBackgroundJobs();
                    //RemoveRecurringJobs();

                    #region Options TO DELETE

                    //, new SqlServerStorageOptions
                    //{
                    //    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    //    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    //    QueuePollInterval = TimeSpan.Zero,
                    //    UseRecommendedIsolationLevel = true,
                    //    UsePageLocksOnDequeue = true,
                    //    DisableGlobalLocks = true
                    //});

                    //var options = new BackgroundJobServerOptions
                    //{
                    //    ServerName = String.Format(
                    //    "{0}.{1}",
                    //    Environment.MachineName,
                    //    Guid.NewGuid().ToString())
                    //};

                    #endregion
                }
                else
                    Logger.Info("Startup.cs --> Configuration() NOT connected Mongo DB!", "conStringMongoDB:" + conStringMongoDB, "mongoDBName:" + mongoDBName);
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Exception ex ERROR in Startup.cs", ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
                //GC.Collect();
            }
        }

        private async void WorkerForSendData()
        {
            //SEND NOTIFICATION EMAILS
            RecurringJob.AddOrUpdate("SendInboxMessagesNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("InboxMessagesJob", DateTime.Now, 5), Cron.Daily(8, 30));
            RecurringJob.AddOrUpdate("SendTasksNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("TasksReminderJob", DateTime.Now, 0), Cron.Daily(8, 35));
            RecurringJob.AddOrUpdate("SendOnboardingNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("OnboardingFilesJob", DateTime.Now, 0), Cron.Daily(8, 40));
            RecurringJob.AddOrUpdate("SendCollaborationLibraryNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("CollaborationLibraryFilesJob", DateTime.Now, 0), Cron.Daily(8, 45));
            RecurringJob.AddOrUpdate("SendNotConfirmedDealsNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("NotConfirmedDealsJob", DateTime.Now, 2), Cron.Daily(8, 50));
            RecurringJob.AddOrUpdate("SendExpiringDealsNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("ExpiringDealsJob", DateTime.Now, 15), Cron.Daily(8, 55));
            RecurringJob.AddOrUpdate("SendNewLeadsNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("NewLeadsJob", DateTime.Now, 2), Cron.Daily(9, 00));
            RecurringJob.AddOrUpdate("SendPendingLeadsNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("PendingLeadsJob", DateTime.Now, 30), Cron.Daily(9, 05));
            RecurringJob.AddOrUpdate("SendCollaborationMailboxMessagesNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("CollaborationMailboxMessagesJob", DateTime.Now, 2), Cron.Daily(9, 10));
            RecurringJob.AddOrUpdate("SendTeamSubAccountsNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("TeamSubAccountsJob", DateTime.Now, 5), Cron.Daily(9, 15));
            RecurringJob.AddOrUpdate("SendPendingInvitationsNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("PendingInvitationsJob", DateTime.Now, 5), Cron.Daily(9, 20));
            RecurringJob.AddOrUpdate("SendPendingRequestsNotifications", () => Lib.Services.HangfireJobs.Send.SendData.SendNotifications("PendingRequestsJob", DateTime.Now, 5), Cron.Daily(9, 25));
        }

        private void WorkerForUpdateData()
        {
            //Update the DATA before NOTIFICATION EMAILS
            RecurringJob.AddOrUpdate("UpdateInboxMessagesJob", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("InboxMessagesJob", DateTime.Now), Cron.Hourly(5));
            RecurringJob.AddOrUpdate("UpdateTeamSubAccountsJob", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("TeamSubAccountsJob", DateTime.Now), Cron.Hourly(10));
            RecurringJob.AddOrUpdate("UpdateTasksReminderJob", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("TasksReminderJob", DateTime.Now), Cron.Hourly(15));
            RecurringJob.AddOrUpdate("UpdatePendingInvitationsJob", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("PendingInvitationsJob", DateTime.Now), Cron.Hourly(20));
            RecurringJob.AddOrUpdate("UpdatePendingRequestsJob", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("PendingRequestsJob", DateTime.Now), Cron.Hourly(25));
            RecurringJob.AddOrUpdate("UpdateNotConfirmedDealsJob", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("NotConfirmedDealsJob", DateTime.Now), Cron.Hourly(30));
            RecurringJob.AddOrUpdate("UpdateExpiringDealsJob", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("ExpiringDealsJob", DateTime.Now), Cron.Hourly(35));
            RecurringJob.AddOrUpdate("UpdateNewLeadsJob", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("NewLeadsJob", DateTime.Now), Cron.Hourly(40));
            RecurringJob.AddOrUpdate("UpdatePendingLeadsJob", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("PendingLeadsJob", DateTime.Now), Cron.Hourly(45));
            RecurringJob.AddOrUpdate("UpdateCollaborationMailboxMessagesJobForChannelPartners", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("CollaborationMailboxMessagesJobForChannelPartners", DateTime.Now), Cron.Hourly(50));
            RecurringJob.AddOrUpdate("UpdateCollaborationMailboxMessagesJobForVendors", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("CollaborationMailboxMessagesJobForVendors", DateTime.Now), Cron.Hourly(55));
            //Update Expired Deals
            RecurringJob.AddOrUpdate("UpdateExpiredDealsJob", () => Lib.Services.HangfireJobs.Fix.FixData.UpdateRecordsForEachTaskJobs("ExpiredDealsJob", DateTime.Now), Cron.Daily(7, 15));
        }

        private void WorkerForGetData()
        {
            //Get the DATA            
            RecurringJob.AddOrUpdate("GetInboxMessages", () => Lib.Services.HangfireJobs.Get.GetData.GetInboxMessages(1, 1, 0, "InboxMessagesJob", 3), Cron.Hourly(3));
            RecurringJob.AddOrUpdate("GetTeamSubAccounts", () => Lib.Services.HangfireJobs.Get.GetData.GetTeamSubAccounts(0, 1, "TeamSubAccountsJob", 3), Cron.Hourly(6));
            RecurringJob.AddOrUpdate("GetTasksReminder", () => Lib.Services.HangfireJobs.Get.GetData.GetTasksReminder(DateTime.Now, "TasksReminderJob", 1), Cron.Hourly(9));
            RecurringJob.AddOrUpdate("GetPendingInvitations", () => Lib.Services.HangfireJobs.Get.GetData.GetPendingInvitations(1, "Channel Partners", "Pending", "PendingInvitationsJob", 4), Cron.Hourly(12));
            RecurringJob.AddOrUpdate("GetPendingRequests", () => Lib.Services.HangfireJobs.Get.GetData.GetPendingRequests(1, "Channel Partners", "Vendors", "Pending", "PendingRequestsJob", 3), Cron.Hourly(18));
            RecurringJob.AddOrUpdate("GetNotConfirmedDeals", () => Lib.Services.HangfireJobs.Get.GetData.GetNotConfirmedDeals(1, 0, "Pending", 1, "NotConfirmedDealsJob", 2), Cron.Hourly(21));
            RecurringJob.AddOrUpdate("GetExpiringDeals", () => Lib.Services.HangfireJobs.Get.GetData.GetExpiringDeals(1, "Pending", 1, 30, "ExpiringDealsJob", 2), Cron.Hourly(24));
            RecurringJob.AddOrUpdate("GetNewLeads", () => Lib.Services.HangfireJobs.Get.GetData.GetNewLeads(1, "Pending", 1, "NewLeadsJob", 3), Cron.Hourly(27));
            RecurringJob.AddOrUpdate("GetPendingLeads", () => Lib.Services.HangfireJobs.Get.GetData.GetPendingLeads("Pending", 1, "PendingLeadsJob", 3), Cron.Hourly(32));
            RecurringJob.AddOrUpdate("GetOnboardingFiles", () => Lib.Services.HangfireJobs.Get.GetData.GetOnboardingFiles(1, 5, 3, "OnboardingFilesJob"), Cron.Hourly(37));
            RecurringJob.AddOrUpdate("GetCollaborationLibraryFilesForChannelPartners", () => Lib.Services.HangfireJobs.Get.GetData.GetCollaborationLibraryFiles(true, 1, 5, 3, "CollaborationLibraryFilesJob"), Cron.Hourly(43));
            RecurringJob.AddOrUpdate("GetCollaborationLibraryFilesForVendors", () => Lib.Services.HangfireJobs.Get.GetData.GetCollaborationLibraryFiles(false, 1, 5, 3, "CollaborationLibraryFilesJob"), Cron.Hourly(46));
            RecurringJob.AddOrUpdate("GetCollaborationMailboxMessagesForChannelPartners", () => Lib.Services.HangfireJobs.Get.GetData.GetCollaborationMailboxMessages(true, "CollaborationMailboxMessagesJob", 2), Cron.Hourly(49));
            RecurringJob.AddOrUpdate("GetCollaborationMailboxMessagesForVendors", () => Lib.Services.HangfireJobs.Get.GetData.GetCollaborationMailboxMessages(false, "CollaborationMailboxMessagesJob", 2), Cron.Hourly(51));
            RecurringJob.AddOrUpdate("GetIpInfoLeads", () => Lib.Services.HangfireJobs.Get.GetData.GetIpInfoLeads(), Cron.Hourly(54));
            //RecurringJob.AddOrUpdate("GetSnitcherLeads", () => Lib.Services.HangfireJobs.Get.GetData.GetSnitcherLeads(), "0 */ 2 * * *");
        }

        private void RemoveRecurringJobs()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
        }

        private void RemoveBackgroundJobs()
        {
            List<RecurringJobDto> list;
            using (var connection = JobStorage.Current.GetConnection())
            {
                list = connection.GetRecurringJobs();
            }

            List<string> jobsIDs = new List<string> { "1", "2", "3","4","5","6","7","8","9", "10", "11", "12", "13", "14", "15", "16" , "17", "18", "19", "#0", "21", "22", "23"
            , "24","25","26"};

            foreach (string item in jobsIDs)
            {
                var job = list?.FirstOrDefault(j => j.Id == item);  // jobId is the recurring job ID, whatever that is
                if (job != null && !string.IsNullOrEmpty(job.LastJobId))
                {
                    BackgroundJob.Delete(job.LastJobId);
                }
            }
        }
    }
}
