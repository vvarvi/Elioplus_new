using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus.Lib.Services.HangfireJobs.Fix
{
	public class FixData
	{
		public static void UpdateRecordsForEachTaskJobs(string taskName, DateTime remindDate)
		{
			if (taskName != "ExpiredDealsJob")
				BackgroundJob.Enqueue(() => GetData(taskName, remindDate));
			else
				BackgroundJob.Enqueue(() => UpdateData(taskName));
		}

		public static int GetData(string taskName, DateTime remindDate)
		{
			DBSession session = new DBSession();

			try
			{
				session.OpenConnection();

				string strQuery = @"UPDATE [dbo].[Elio_scheduler_notification_emails] 
                                        SET  remind_date = NULL
		                                    ,next_date_sent = NULL
                                    WHERE case_id in
                                    (
                                        SELECT 
		                                    sne.case_id
	                                    FROM [dbo].[Elio_scheduler_notification_emails] sne ";

				switch (taskName)
				{
					case "InboxMessagesJob":

						strQuery += @"inner join [dbo].[Elio_users_messages] um
                                          on um.id = sne.case_id
                                      where task_name = @task_name 
                                      and um.is_new = 0";

						break;

					case "TasksReminderJob":

						strQuery += @"inner join [dbo].[Elio_users_messages] um
                                          on um.id = sne.case_id
                                      where task_name = @task_name 
									  and sent = 1 ";

						break;

					case "TeamSubAccountsJob":

						strQuery += @"inner join [dbo].[Elio_users_sub_accounts] usa
		                                  on usa.id = sne.case_id
	                                  where task_name = @task_name
	                                  and usa.is_confirmed = 1
	                                  and usa.is_active = 1 ";

						break;

					case "PendingInvitationsJob":

						strQuery += @"inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
		                                    on cvr_inv.id = sne.case_id
	                                    inner join Elio_collaboration_vendors_resellers cvr
		                                    on cvr_inv.vendor_reseller_id = cvr.id
	                                    inner join elio_users u 
		                                    on u.id = cvr.partner_user_id
	                                    inner join elio_users u2
		                                    on u2.id = cvr.master_user_id
	                                    where 1 = 1
	                                    and cvr.is_active = 1
	                                    and cvr_inv.is_new IN (0,1)
	                                    and u.company_type = 'Channel Partners'
	                                    AND cvr.invitation_status = 'Confirmed'
	                                    and cvr.master_user_id = cvr_inv.user_id
	                                    and u2.email <> cvr_inv.recipient_email
	                                    and task_name = @task_name ";

						break;

					case "PendingRequestsJob":

						strQuery += @"inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
		                                    on cvr_inv.id = sne.case_id 
	                                    inner join Elio_collaboration_vendors_resellers cvr
		                                    on cvr_inv.vendor_reseller_id = cvr.id and cvr_inv.user_id = cvr.partner_user_id
	                                    inner join elio_users u 
		                                    on u.id = cvr.partner_user_id
	                                    inner join elio_users u2
		                                    on u2.id = cvr.master_user_id
	                                    where 1 = 1
	                                    and cvr.master_user_id = u2.id
	                                    and cvr.is_active = 1
	                                    and cvr_inv.is_new = 0
	                                    and u.company_type = 'Channel Partners'
	                                    and cvr.partner_user_id = cvr_inv.user_id
	                                    and u2.company_type = 'Vendors'
	                                    and cvr_inv.user_id = u.id
	                                    and u.account_status = 1 
	                                    AND invitation_status = 'Confirmed'
	                                    and u2.email = cvr_inv.recipient_email
	                                    and sne.task_name = @task_name ";

						break;

					case "NotConfirmedDealsJob":

						strQuery += @"inner join Elio_registration_deals rd
		                                    on rd.id = sne.case_id
	                                    where 1 = 1
	                                    and is_new = 0
	                                    and rd.is_active = 1
	                                    and deal_result <> 'Pending'
	                                    and date_viewed is not null
	                                    and status <> 1
	                                    --and expected_closed_date > getdate()
	                                    and sne.task_name = @task_name ";

						break;

					case "ExpiringDealsJob":

						strQuery += @"inner join Elio_registration_deals rd
		                                    on rd.id = sne.case_id
	                                    where 1 = 1
	                                    and is_new IN (0,1)
	                                    and rd.is_public = 1
	                                    and rd.is_active = 1
	                                    and deal_result <> 'Pending'
	                                    and status <> 1
	                                    and expected_closed_date > getdate()			
	                                    and datediff(day, getdate(), expected_closed_date) = 30
	                                    and task_name = @task_name ";

						break;

					case "NewLeadsJob":

						strQuery += @"inner join Elio_lead_distributions ld
											on ld.id = sne.case_id
										where 1 = 1
										and is_new = 0
										and ld.is_public = 1
										and lead_result <> 'Pending'
										and status <> 1
										and task_name = @task_name ";

						break;

					case "PendingLeadsJob":

						strQuery += @"inner join Elio_lead_distributions ld
											on ld.id = sne.case_id
										where 1 = 1
										and is_new IN (0)
										and ld.is_public = 1
										and lead_result <> 'Pending'
										and status <> 1
										and task_name = @task_name ";

						break;

					case "OnboardingFilesJob":

						strQuery += @"inner join Elio_collaboration_vendors_resellers cvr on cvr.id = sne.case_id
										inner join elio_users u on u.id = cvr.partner_user_id
										inner join elio_users u2 on u2.id = cvr.master_user_id
										where cvr.master_user_id in 
										(
											select distinct user_id
											from Elio_onboarding_users_library_files f
											where 1 = 1
											and f.user_id = cvr.master_user_id
											and f.is_new = 0
											and datediff(day, date_created, getdate()) = 5
										)
										and cvr.invitation_status = 'Confirmed'		
										and u.company_type = 'Channel Partners'
										and u.account_status = 1
										and task_name = @task_name ";

						break;

					case "CollaborationLibraryFilesJobForChannelPartners":

						strQuery += @"inner join Elio_collaboration_vendors_resellers cvr on cvr.id = sne.case_id
										inner join elio_users u on u.id = cvr.partner_user_id
										inner join elio_users u2 on u2.id = cvr.master_user_id
										where cvr.master_user_id in 
										(
											select distinct user_id
											from Elio_collaboration_users_library_files f
											where 1 = 1
											and f.uploaded_by_user_id = cvr.master_user_id
											and f.is_new = 0
											and datediff(day, date_created, getdate()) = 5
										)
										and cvr.invitation_status = 'Confirmed'		
										and u.company_type = 'Channel Partners'
										and u.account_status = 1
										and task_name = @task_name ";

						break;

					case "CollaborationLibraryFilesJobForVendors":

						strQuery += @"inner join Elio_collaboration_vendors_resellers cvr on cvr.id = sne.case_id
										inner join elio_users u on u.id = cvr.master_user_id
										inner join elio_users u2 on u2.id = cvr.partner_user_id
										where cvr.master_user_id in 
										(
											select distinct user_id
											from Elio_collaboration_users_library_files f
											where 1 = 1
											and f.uploaded_by_user_id = cvr.partner_user_id
											and f.is_new = 0
											and datediff(day, date_created, getdate()) = 5
										)
										and cvr.invitation_status = 'Confirmed'		
										and u.company_type = 'Vendors'
										and u.account_status = 1
										and task_name = @task_name ";

						break;

					case "CollaborationMailboxMessagesJobForChannelPartners":

						strQuery += @"inner join Elio_collaboration_users_mailbox cum on cum.mailbox_id = sne.case_id
										cross apply
										(
											select cm.user_id--, count(id) as messages_count
											from Elio_collaboration_mailbox cm
											where cm.id = cum.mailbox_id
											and cm.is_public = 1
											and cm.user_id = cum.master_user_id-- from vendor
										) m
										where 1 = 1
										and cum.is_new = 0
										and cum.is_viewed = 1
										and cum.date_viewed is not null
										and cum.is_deleted = 0
										and cum.date_deleted is null
										and cum.is_public = 1
										and task_name = @task_name ";

						break;

					case "CollaborationMailboxMessagesJobForVendors":

						strQuery += @"inner join Elio_collaboration_users_mailbox cum on cum.mailbox_id = sne.case_id
										cross apply
										(
											select cm.user_id--, count(id) as messages_count
											from Elio_collaboration_mailbox cm
											where cm.id = cum.mailbox_id
											and cm.is_public = 1
											and cm.user_id = cum.partner_user_id     -- from channel partner
										) m
										where 1 = 1
										and cum.is_new = 0
										and cum.is_viewed = 1
										and cum.date_viewed is not null
										and cum.is_deleted = 0
										and cum.date_deleted is null
										and cum.is_public = 1
										and task_name = @task_name ";

						break;
				}

				if (taskName == "TasksReminderJob")
				{
					strQuery += @"  and count = sent_limit_count) ";
				}
				else
				{
					strQuery += @"  and count < sent_limit_count 
                                and 
									(
										(
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
										)
									)
                                )";
				}

				return session.ExecuteQuery(strQuery
									, DatabaseHelper.CreateStringParameter("@task_name", taskName)
									, DatabaseHelper.CreateDateTimeParameter("@remind_date", remindDate));
			}
			catch (Exception ex)
			{
				Logger.DetailedError("HangfireDashboard.EmailSender.Data.GetData", ex.Message.ToString(), ex.StackTrace.ToString());
				return -1;
			}
			finally
			{
				session.CloseConnection();
			}
		}

		public static int UpdateData(string taskName)
		{
			DBSession session = new DBSession();

			try
			{
				session.OpenConnection();

				if (taskName == "ExpiredDealsJob")
					return session.ExecuteQuery(@"update Elio_registration_deals 
													SET status = -3,
														is_new = 0,
														date_viewed = expected_closed_date
													WHERE 1 = 1
													AND DATEADD(MONTH, month_duration, created_date) < GETDATE()
													AND status = 1
													AND is_active = 1
													AND deal_result = 'Pending'
													AND month_duration <> 0"
											, DatabaseHelper.CreateStringParameter("@task_name", taskName));
				else
					return -1;
			}
			catch (Exception ex)
			{
				Logger.DetailedError("HangfireDashboard.EmailSender.Data.GetData", ex.Message.ToString(), ex.StackTrace.ToString());
				return -1;
			}
			finally
			{
				session.CloseConnection();
			}
		}
	}
}