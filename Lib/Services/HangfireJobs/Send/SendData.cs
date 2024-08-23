using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Services.HangfireJobs.Send
{
    public class SendData
    {
        public static void SendNotifications(string taskName, DateTime remindDate, int runAfterDays)
        {
            DBSession session = new DBSession();

            try
            {
                session.OpenConnection();

                DataLoader<ElioSchedulerNotificationEmails> loader = new DataLoader<ElioSchedulerNotificationEmails>(session);

                List<ElioSchedulerNotificationEmails> emails = loader.Load(@"SELECT *
                                                                                FROM [dbo].[Elio_scheduler_notification_emails]
                                                                                where task_name = @task_name
                                                                                and is_active = 1
                                                                                and count < sent_limit_count 
                                                                                and (next_date_sent is not null and next_date_sent >= getdate())
                                                                                and 
	                                                                            ((
		                                                                            remind_date is not null 
		                                                                            and year(remind_date) = year(@remind_date)
		                                                                            and month(remind_date) = month(@remind_date)
		                                                                            and day(remind_date) = day(@remind_date)
	                                                                            )
                                                                                or
	                                                                            (
		                                                                            next_date_sent is not null 
		                                                                            and year(next_date_sent) = year(@remind_date)
		                                                                            and month(next_date_sent) = month(@remind_date)
		                                                                            and day(next_date_sent) = day(@remind_date)
	                                                                            ))"
                                                                    , DatabaseHelper.CreateStringParameter("@task_name", taskName)
                                                                    , DatabaseHelper.CreateDateTimeParameter("@remind_date", remindDate));

                if (emails.Count > 0)
                {
                    TasksLib.SendSchedulerEmails(emails, runAfterDays, loader);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.EmailSender.SendOnboardingNotifications", ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }
    }
}