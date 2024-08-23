using HangfireDashboard.Objects;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Services.HangfireJobs.Enumerators;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Services.HangfireJobs.Get
{
    public static class GetData
    {       
        public static void GetInboxMessages(int isNew, int sent, int isDeleted, string taskName, int totalSentCount)
        {
            DBSession session = new DBSession();

            try
            {
                MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.InboxMessages);
                if (mailMessage != null)
                {
                    try
                    {
                        session.OpenConnection();

                        DataLoader<ElioUsersMessages> loader = new DataLoader<ElioUsersMessages>(session);

                        List<ElioUsersMessages> messages = loader.Load(@"select 
			                                                         sender_user_id,
			                                                        'InboxMessagesJob',
			                                                        um.id,
			                                                        receiver_email, 
			                                                        sender_email,
			                                                        getdate(),
			                                                        getdate(),
			                                                        null,
			                                                        0,
			                                                        NULL,
			                                                        NULL,
			                                                        0,
			                                                        0,
			                                                        '',
			                                                        '',
			                                                        ''
		                                                        from Elio_users_messages um 
		                                                        where um.is_new = @is_new
			                                                        and um.deleted = @is_deleted
			                                                        and um.sender_user_id <> um.receiver_user_id
			                                                        and um.sent = @is_sent
			                                                        and year(um.sysdate) >= 2020
			                                                        and um.id not in
			                                                        (
				                                                        select case_id
				                                                        from Elio_scheduler_notification_emails sne
				                                                        where 1 = 1	
				                                                        and sne.task_name = @task_name)"
                                                                    , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                                    , DatabaseHelper.CreateIntParameter("@is_deleted", isDeleted)
                                                                    , DatabaseHelper.CreateIntParameter("@is_sent", sent)
                                                                    , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                        if (messages.Count > 0)
                        {
                            foreach (ElioUsersMessages inbox in messages)
                            {
                                ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                email.UserId = inbox.SenderUserId;
                                email.TaskName = taskName;
                                email.CaseId = inbox.Id;
                                email.ReceiverEmailAddress = inbox.ReceiverEmail;
                                email.SenderEmailAddress = inbox.SenderEmail;
                                email.Sysdate = DateTime.Now;
                                email.LastUpdated = DateTime.Now;
                                email.RemindDate = DateTime.Now.AddDays(5);
                                email.IsSent = 0;
                                email.DateSent = null;
                                email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(5);
                                email.Count = 0;
                                email.SentLimitCount = totalSentCount;
                                email.EmailTemplate = mailMessage.Body;
                                email.EmailSubject = mailMessage.Subject;
                                email.EmailMessage = "";
                                email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                schedulerLoader.Insert(email);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetInboxMessages", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                    finally
                    {
                        session.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetInboxMessages", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetTeamSubAccounts(int isConfirmed, int isActive, string taskName, int totalSentCount)
        {
            DBSession session = new DBSession();

            try
            {

                try
                {
                    session.OpenConnection();

                    DataLoader<ElioUsersSubAccountsIJUsers> loader = new DataLoader<ElioUsersSubAccountsIJUsers>(session);

                    List<ElioUsersSubAccountsIJUsers> subAccounts = loader.Load(@"select 
			                                                                            user_id, 
                                                                                        company_name,
			                                                                            'TeamSubAccountsJob',
			                                                                            usa.id,
			                                                                            usa.email,
			                                                                            u.email,
			                                                                            getdate(),
			                                                                            getdate(),
			                                                                            '',
			                                                                            0,
			                                                                            NULL,
			                                                                            NULL,
			                                                                            0',
			                                                                            0',
			                                                                            '',
			                                                                            '',
			                                                                            ''
		                                                                            from Elio_users_sub_accounts usa
		                                                                            inner join Elio_users u
			                                                                            on u.id = usa.user_id
		                                                                            where usa.is_confirmed = @is_confirmed
		                                                                            and usa.is_active = @is_active
                                                                                    and usa.sysdate >= '2020-07-01'
		                                                                            and usa.id not in
		                                                                            (
			                                                                            select case_id
			                                                                            from Elio_scheduler_notification_emails sne
			                                                                            where 1 = 1	
			                                                                            and sne.task_name = @task_name
		                                                                            )"
                                                                                , DatabaseHelper.CreateIntParameter("@is_confirmed", isConfirmed)
                                                                                , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                                                , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                    if (subAccounts.Count > 0)
                    {
                        foreach (ElioUsersSubAccountsIJUsers sub in subAccounts)
                        {
                            MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.TeamSubAccounts);
                            if (mailMessage != null)
                            {
                                ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                email.UserId = sub.UserId;
                                email.TaskName = taskName;
                                email.CaseId = sub.Id;
                                email.ReceiverEmailAddress = sub.Email;
                                email.SenderEmailAddress = sub.SenderEmail;
                                email.Sysdate = DateTime.Now;
                                email.LastUpdated = DateTime.Now;
                                email.RemindDate = DateTime.Now;
                                email.IsSent = 0;
                                email.DateSent = null;
                                email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(5);
                                email.Count = 0;
                                email.SentLimitCount = totalSentCount;
                                email.EmailTemplate = mailMessage.Body = mailMessage.Body.Replace("{link}", sub.ConfirmationUrl).Replace("{CompanyName}", sub.CompanyName);
                                email.EmailSubject = mailMessage.Subject.Replace("{CompanyName}", sub.CompanyName);
                                email.EmailMessage = "";
                                email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                schedulerLoader.Insert(email);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetTeamSubAccounts", ex.Message.ToString(), ex.StackTrace.ToString());
                }
                finally
                {
                    session.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetTeamSubAccounts", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetTasksReminder(DateTime remindDate, string taskName, int totalSentCount)
        {
            DBSession session = new DBSession();

            try
            {
                MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.TasksReminder);
                if (mailMessage != null)
                {
                    try
                    {
                        session.OpenConnection();

                        DataLoader<ElioOpportunitiesUserTasksIJUsers> loader = new DataLoader<ElioOpportunitiesUserTasksIJUsers>(session);

                        List<ElioOpportunitiesUserTasksIJUsers> tasks = loader.Load(@"SELECT 
			                                                                                user_id,
			                                                                                'TasksReminderJob',
			                                                                                tas.id,
			                                                                                u.email,			
			                                                                                '',
			                                                                                getdate(),
			                                                                                getdate(),
			                                                                                tas.remind_date,
			                                                                                0,
			                                                                                NULL,
			                                                                                NULL,
			                                                                                0,
			                                                                                0,
			                                                                                '',
			                                                                                '',
			                                                                                ''
		                                                                                FROM Elio_opportunities_users_tasks tas
		                                                                                inner join elio_users u
		                                                                                on u.id = tas.user_id
		                                                                                where 1 = 1
		                                                                                and year(remind_date) = year(@remind_date)
		                                                                                and month(remind_date) = month(@remind_date)
		                                                                                and day(remind_date) = day(@remind_date)
                                                                                        and tas.sysdate >= '2020-07-01'
		                                                                                and tas.id not in
		                                                                                (
			                                                                                select case_id
			                                                                                from Elio_scheduler_notification_emails sne
			                                                                                where 1 = 1	
			                                                                                and sne.task_name = @task_name
		                                                                                )"
                                                                                , DatabaseHelper.CreateDateTimeParameter("@remind_date", remindDate)
                                                                                , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                        if (tasks.Count > 0)
                        {
                            foreach (ElioOpportunitiesUserTasksIJUsers task in tasks)
                            {
                                ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                email.UserId = task.UserId;
                                email.TaskName = taskName;
                                email.CaseId = task.Id;
                                email.ReceiverEmailAddress = task.Email;
                                email.RemindDate = task.RemindDate;
                                email.SenderEmailAddress = "";
                                email.Sysdate = DateTime.Now;
                                email.LastUpdated = DateTime.Now;
                                email.IsSent = 0;
                                email.DateSent = null;
                                email.NextDateSent = null;
                                email.Count = 0;
                                email.SentLimitCount = totalSentCount;
                                email.EmailTemplate = mailMessage.Body.Replace("{TaskSubject}", task.TaskSubject).Replace("{RemindDate}", task.RemindDate.ToShortDateString());
                                email.EmailSubject = mailMessage.Subject;
                                email.EmailMessage = "";
                                email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                schedulerLoader.Insert(email);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetTasksReminder", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                    finally
                    {
                        session.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetTasksReminder", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetPendingInvitations(int isNew, string companyType, string invitationStatus, string taskName, int totalSentCount)
        {
            DBSession session = new DBSession();

            try
            {
                MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.PendingInvitations);
                if (mailMessage != null)
                {
                    try
                    {
                        session.OpenConnection();

                        DataLoader<ElioCollaborationVendorResellerInvitationsIJUsers> loader = new DataLoader<ElioCollaborationVendorResellerInvitationsIJUsers>(session);

                        List<ElioCollaborationVendorResellerInvitationsIJUsers> invitations = loader.Load(@"SELECT cvr_inv.user_id, 
			                                                                                                    'PendingInvitationsJob',
			                                                                                                    cvr_inv.id,			
			                                                                                                    cvr_inv.recipient_email, 
			                                                                                                    u2.email,
                                                                                                                u2.company_name,
                                                                                                                cvr.id as vendor_reseller_id,
                                                                                                                cvr.partner_user_id,
			                                                                                                    getdate(),
			                                                                                                    getdate(),
			                                                                                                    '',
			                                                                                                    0,
			                                                                                                    NULL,
			                                                                                                    NULL,
			                                                                                                    0,
			                                                                                                    0,
			                                                                                                    '',
			                                                                                                    '',
			                                                                                                    ''
		                                                                                                    FROM Elio_collaboration_vendors_resellers cvr
		                                                                                                    inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
			                                                                                                    on cvr_inv.vendor_reseller_id = cvr.id
		                                                                                                    inner join elio_users u 
			                                                                                                    on u.id = cvr.partner_user_id
		                                                                                                    inner join elio_users u2
			                                                                                                    on u2.id = cvr.master_user_id
		                                                                                                    where 1 = 1
		                                                                                                    and is_active = 1
		                                                                                                    and is_new = @is_new
		                                                                                                    and u.company_type = @company_type
		                                                                                                    AND invitation_status = @invitation_status
		                                                                                                    and cvr.master_user_id = cvr_inv.user_id
		                                                                                                    and u2.email <> cvr_inv.recipient_email
                                                                                                            and cvr_inv.send_date >= '2020-07-01'
		                                                                                                    and cvr_inv.id not in
		                                                                                                    (
			                                                                                                    select case_id
			                                                                                                    from Elio_scheduler_notification_emails sne
			                                                                                                    where 1 = 1	
			                                                                                                    and sne.task_name = @task_name
		                                                                                                    )"
                                                                                            , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                                                            , DatabaseHelper.CreateStringParameter("@company_type", companyType)
                                                                                            , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus)
                                                                                            , DatabaseHelper.CreateIntParameter("@scheduler_sent_count", totalSentCount)
                                                                                            , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                        if (invitations.Count > 0)
                        {
                            foreach (ElioCollaborationVendorResellerInvitationsIJUsers invitation in invitations)
                            {
                                ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                ElioUsersSubAccounts subAccount = Sql.GetSubAccountByAssignedPartner(invitation.VendorResellerId, invitation.PartnerUserId, session);
                                if (subAccount != null)
                                {
                                    #region Assign to Sub Account Partner case

                                    string companyName = "";

                                    if (subAccount.AccountStatus == (int)AccountStatus.Completed)
                                    {
                                        companyName = subAccount.LastName + " " + subAccount.FirstName;
                                    }
                                    else
                                        companyName = invitation.CompanyName;

                                    email.UserId = invitation.UserId;
                                    email.TaskName = taskName;
                                    email.CaseId = invitation.Id;
                                    email.ReceiverEmailAddress = invitation.RecipientEmail;
                                    email.SenderEmailAddress = subAccount.Email;
                                    email.Sysdate = DateTime.Now;
                                    email.LastUpdated = DateTime.Now;
                                    email.RemindDate = DateTime.Now;
                                    email.IsSent = 0;
                                    email.DateSent = null;
                                    email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(5);
                                    email.Count = 0;
                                    email.SentLimitCount = totalSentCount;
                                    email.EmailTemplate = mailMessage.Body.Replace("{CompanyName}", companyName);
                                    email.EmailSubject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                                    email.EmailMessage = "";
                                    email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                    DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                    schedulerLoader.Insert(email);

                                    #endregion
                                }
                                else
                                {
                                    #region Regular case

                                    email.UserId = invitation.UserId;
                                    email.TaskName = taskName;
                                    email.CaseId = invitation.Id;
                                    email.ReceiverEmailAddress = invitation.RecipientEmail;
                                    email.SenderEmailAddress = invitation.Email;
                                    email.Sysdate = DateTime.Now;
                                    email.LastUpdated = DateTime.Now;
                                    email.RemindDate = DateTime.Now;
                                    email.IsSent = 0;
                                    email.DateSent = null;
                                    email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(5);
                                    email.Count = 0;
                                    email.SentLimitCount = totalSentCount;
                                    email.EmailTemplate = mailMessage.Body.Replace("{CompanyName}", invitation.CompanyName);
                                    email.EmailSubject = mailMessage.Subject.Replace("{CompanyName}", invitation.CompanyName);
                                    email.EmailMessage = "";
                                    email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                    DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                    schedulerLoader.Insert(email);

                                    #endregion
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetPendingInvitations", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                    finally
                    {
                        session.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetPendingInvitations", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetPendingRequests(int isNew, string companyType, string recipientCompanyType, string invitationStatus, string taskName, int totalSentCount)
        {
            DBSession session = new DBSession();

            try
            {
                MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.PendingInvitations);
                if (mailMessage != null)
                {
                    try
                    {
                        session.OpenConnection();

                        DataLoader<ElioCollaborationVendorResellerInvitationsIJUsers> loader = new DataLoader<ElioCollaborationVendorResellerInvitationsIJUsers>(session);

                        List<ElioCollaborationVendorResellerInvitationsIJUsers> requests = loader.Load(@"SELECT 
			                                                                                    cvr_inv.user_id,
			                                                                                    'PendingRequestsJob',
			                                                                                    cvr_inv.id,
                                                                                                cvr.id as vendor_reseller_id,
                                                                                                u.id as partner_user_id,
			                                                                                    cvr_inv.recipient_email,
			                                                                                    u.email,
                                                                                                u.company_name,
			                                                                                    getdate(),
			                                                                                    getdate(),
			                                                                                    '',
			                                                                                    0,
			                                                                                    NULL,
			                                                                                    NULL,
			                                                                                    0,
			                                                                                    0,
			                                                                                    '',
			                                                                                    '',
			                                                                                    ''
		                                                                                    FROM Elio_collaboration_vendors_resellers cvr
		                                                                                    inner join Elio_collaboration_vendor_reseller_invitations cvr_inv
		                                                                                    on cvr_inv.vendor_reseller_id = cvr.id and cvr_inv.user_id = cvr.partner_user_id
		                                                                                    inner join elio_users u 
		                                                                                    on u.id = cvr.partner_user_id
		                                                                                    inner join elio_users u2
			                                                                                    on u2.id = cvr.master_user_id
		                                                                                    where 1 = 1
		                                                                                    and cvr.master_user_id = u2.id
		                                                                                    and is_active = 1
		                                                                                    and is_new = @is_new
		                                                                                    and u.company_type = @company_type
		                                                                                    and cvr.partner_user_id = cvr_inv.user_id
		                                                                                    and u2.company_type = @recipient_company_type
		                                                                                    and cvr_inv.user_id = u.id
		                                                                                    and u.account_status = 1 
		                                                                                    AND invitation_status = @invitation_status
		                                                                                    and u2.email = cvr_inv.recipient_email
                                                                                            and cvr_inv.send_date >= '2020-07-01'
		                                                                                    and cvr_inv.id not in
		                                                                                    (
			                                                                                    select case_id
			                                                                                    from Elio_scheduler_notification_emails sne
			                                                                                    where 1 = 1	
			                                                                                    and sne.task_name = @task_name
		                                                                                    )"
                                                                                    , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                                                    , DatabaseHelper.CreateStringParameter("@company_type", companyType)
                                                                                    , DatabaseHelper.CreateStringParameter("@recipient_company_type", recipientCompanyType)
                                                                                    , DatabaseHelper.CreateStringParameter("@invitation_status", invitationStatus)
                                                                                    , DatabaseHelper.CreateIntParameter("@scheduler_sent_count", totalSentCount)
                                                                                    , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                        if (requests.Count > 0)
                        {
                            foreach (ElioCollaborationVendorResellerInvitationsIJUsers request in requests)
                            {
                                ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                ElioUsersSubAccounts subAccount = Sql.GetSubAccountByAssignedPartner(request.VendorResellerId, request.PartnerUserId, session);
                                if (subAccount != null)
                                {
                                    #region Assign to Sub Account Partner case

                                    email.UserId = request.UserId;
                                    email.TaskName = taskName;
                                    email.CaseId = request.Id;
                                    email.ReceiverEmailAddress = request.RecipientEmail;
                                    email.SenderEmailAddress = subAccount.Email;
                                    email.Sysdate = DateTime.Now;
                                    email.LastUpdated = DateTime.Now;
                                    email.RemindDate = DateTime.Now;
                                    email.IsSent = 0;
                                    email.DateSent = null;
                                    email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(5);
                                    email.Count = 0;
                                    email.SentLimitCount = totalSentCount;
                                    email.EmailTemplate = mailMessage.Body.Replace("{CompanyName}", request.CompanyName);
                                    email.EmailSubject = mailMessage.Subject;
                                    email.EmailMessage = "";
                                    email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                    DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                    schedulerLoader.Insert(email);

                                    #endregion
                                }
                                else
                                {
                                    #region Regular case

                                    email.UserId = request.UserId;
                                    email.TaskName = taskName;
                                    email.CaseId = request.Id;
                                    email.ReceiverEmailAddress = request.RecipientEmail;
                                    email.SenderEmailAddress = request.Email;
                                    email.Sysdate = DateTime.Now;
                                    email.LastUpdated = DateTime.Now;
                                    email.RemindDate = DateTime.Now;
                                    email.IsSent = 0;
                                    email.DateSent = null;
                                    email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(5);
                                    email.Count = 0;
                                    email.SentLimitCount = totalSentCount;
                                    email.EmailTemplate = mailMessage.Body.Replace("{CompanyName}", request.CompanyName);
                                    email.EmailSubject = mailMessage.Subject;
                                    email.EmailMessage = "";
                                    email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                    DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                    schedulerLoader.Insert(email);

                                    #endregion
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetPendingRequests", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                    finally
                    {
                        session.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetPendingRequests", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetNotConfirmedDeals(int isNew, int isActive, string dealResult, int status, string taskName, int totalSentCount)
        {
            DBSession session = new DBSession();

            try
            {
                MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.NotConfirmedDeals);
                if (mailMessage != null)
                {
                    try
                    {
                        session.OpenConnection();

                        DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

                        string query = @"SELECT
			                                reseller_id,
                                            vendor_id,
			                                'NotConfirmedDealsJob',
			                                rd.id,
                                            rd.collaboration_vendor_reseller_id,
			                                --u.email as 'receiver_email_address', 
			                                --u2.email as 'sender_email_address',
			                                getdate(),
			                                getdate(),
			                                '',
			                                0,
			                                NULL,
			                                NULL,
			                                0,
			                                0,
			                                '',
			                                '',
			                                ''
		                                FROM Elio_registration_deals rd
		                                --inner join elio_users u
			                            --    on u.id = rd.vendor_id
		                                --inner join elio_users u2
			                            --    on u2.id = rd.reseller_id
		                                where 1 = 1
			                                and is_new = @is_new
			                                and is_active = @is_active
			                                and deal_result = @deal_result
			                                and date_viewed is null
			                                and status = @status
			                                and expected_closed_date > getdate()
                                            and created_date >= '2020-07-01'
                                            and rd.vendor_id <> 52356       -- RANDSTAD EXCLUDED
			                                and rd.id not in
			                                (
				                                select case_id
				                                from Elio_scheduler_notification_emails sne
				                                where 1 = 1	
				                                and sne.task_name = @task_name
			                                )";

                        List<ElioRegistrationDeals> deals = loader.Load(query
                                                                                    , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealResult)
                                                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                                                    , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                        if (deals.Count > 0)
                        {
                            foreach (ElioRegistrationDeals deal in deals)
                            {
                                DataLoader<ElioUsers> loaderUser = new DataLoader<ElioUsers>(session);

                                ElioUsersSubAccounts subAccount = Sql.GetSubAccountByAssignedPartner(deal.CollaborationVendorResellerId, deal.ResellerId, session);
                                if (subAccount != null)
                                {
                                    #region Assign to Sub Account Partner case

                                    ElioUsers reseller = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", deal.ResellerId));
                                    if (reseller != null)
                                    {
                                        ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                        email.UserId = deal.ResellerId;
                                        email.TaskName = taskName;
                                        email.CaseId = deal.Id;
                                        email.ReceiverEmailAddress = subAccount.Email;
                                        email.SenderEmailAddress = (reseller != null) ? reseller.Email : "";
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = DateTime.Now.AddDays(2);
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(2);
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{CompanyName}", reseller.CompanyName);
                                        email.EmailSubject = mailMessage.Subject.Replace("{CompanyName}", reseller.CompanyName);
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                        DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                        schedulerLoader.Insert(email);
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region Regular case

                                    ElioUsers reseller = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", deal.ResellerId));
                                    ElioUsers vendor = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", deal.VendorId));

                                    if (vendor != null && reseller != null)
                                    {
                                        ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                        email.UserId = deal.ResellerId;
                                        email.TaskName = taskName;
                                        email.CaseId = deal.Id;
                                        email.ReceiverEmailAddress = vendor.Email;
                                        email.SenderEmailAddress = (reseller != null) ? reseller.Email : "";
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = DateTime.Now.AddDays(2);
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(2);
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{CompanyName}", reseller.CompanyName);
                                        email.EmailSubject = mailMessage.Subject.Replace("{CompanyName}", reseller.CompanyName);
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                        DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                        schedulerLoader.Insert(email);
                                    }

                                    #endregion
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetNotConfirmedDeals", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                    finally
                    {
                        session.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetNotConfirmedDeals", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetExpiringDeals(int isActive, string dealResult, int status, int daysToNotify, string taskName, int totalSentCount)
        {
            DBSession session = new DBSession();

            try
            {
                MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.ExpiringDeals);
                if (mailMessage != null)
                {
                    try
                    {
                        session.OpenConnection();

                        DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

                        List<ElioRegistrationDeals> deals = loader.Load(@"SELECT 
			                                                                reseller_id,
                                                                            rd.company_name,
                                                                            vendor_id,
			                                                                'ExpiringDealsJob',
			                                                                rd.id,
                                                                            rd.collaboration_vendor_reseller_id,
			                                                                --u2.email as 'receiver_email_address', 
			                                                                --u.email as 'sender_email_address',
			                                                                getdate(),
			                                                                getdate(),
                                                                            expected_closed_date,   --DateAdd(day, (@days_to_notify * -1), expected_closed_date) as 'remind_date',
			                                                                0,
			                                                                NULL,
			                                                                NULL,
			                                                                0,
			                                                                0,
			                                                                '',
			                                                                '',
			                                                                ''
		                                                                FROM Elio_registration_deals rd
		                                                                --inner join elio_users u
			                                                            --    on u.id = rd.vendor_id
		                                                                --inner join elio_users u2
			                                                            --    on u2.id = rd.reseller_id
		                                                                where 1 = 1
			                                                                and is_new IN (0,1)
			                                                                and rd.is_public = 1
			                                                                and is_active = @is_active
			                                                                and deal_result = @deal_result
			                                                                and status = @status
			                                                                and expected_closed_date > getdate()			
			                                                                and datediff(day, getdate(), expected_closed_date) = @days_to_notify
                                                                            and created_date >= '2020-07-01'
			                                                                and rd.id not in
			                                                                (
				                                                                select case_id
				                                                                from Elio_scheduler_notification_emails sne
				                                                                where 1 = 1	
				                                                                and sne.task_name = @task_name
			                                                                )"
                                                                    , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                                    , DatabaseHelper.CreateStringParameter("@deal_result", dealResult)
                                                                    , DatabaseHelper.CreateIntParameter("@status", status)
                                                                    , DatabaseHelper.CreateIntParameter("@days_to_notify", daysToNotify)
                                                                    , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                        if (deals.Count > 0)
                        {
                            foreach (ElioRegistrationDeals deal in deals)
                            {
                                DataLoader<ElioUsers> loaderUser = new DataLoader<ElioUsers>(session);

                                ElioUsersSubAccounts subAccount = Sql.GetSubAccountByAssignedPartner(deal.CollaborationVendorResellerId, deal.ResellerId, session);
                                if (subAccount != null)
                                {
                                    #region Assign to Sub Account Partner case

                                    ElioUsers reseller = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", deal.ResellerId));
                                    if (reseller != null)
                                    {
                                        ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                        email.UserId = deal.ResellerId;
                                        email.TaskName = taskName;
                                        email.CaseId = deal.Id;
                                        email.ReceiverEmailAddress = subAccount.Email;
                                        email.SenderEmailAddress = (reseller != null) ? reseller.Email : "";
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = DateTime.Now.AddDays(2);
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(2);
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{CompanyName}", reseller.CompanyName);
                                        email.EmailSubject = mailMessage.Subject.Replace("{CompanyName}", reseller.CompanyName);
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                        DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                        schedulerLoader.Insert(email);
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region Regular case

                                    ElioUsers resellerEmail = loaderUser.LoadSingle(@"Select email from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", deal.ResellerId));
                                    ElioUsers vendorEmail = loaderUser.LoadSingle(@"Select email from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", deal.VendorId));

                                    if (resellerEmail != null)
                                    {
                                        ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                        email.UserId = deal.ResellerId;
                                        email.TaskName = taskName;
                                        email.CaseId = deal.Id;
                                        email.ReceiverEmailAddress = resellerEmail.Email;
                                        email.SenderEmailAddress = (vendorEmail != null) ? vendorEmail.Email : "";
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = deal.ExpectedClosedDate.AddDays(-1 * daysToNotify);
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(15);
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{ClientName}", deal.CompanyName).Replace("{DaysBefore}", daysToNotify.ToString());
                                        email.EmailSubject = mailMessage.Subject.Replace("{ClientName}", deal.CompanyName).Replace("{DaysBefore}", daysToNotify.ToString());
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                        DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                        schedulerLoader.Insert(email);
                                    }

                                    #endregion
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GeExpiringDeals", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                    finally
                    {
                        session.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GeExpiringDeals", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetNewLeads(int isNew, string leadResult, int status, string taskName, int totalSentCount)
        {
            DBSession session = new DBSession();

            try
            {
                MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.NewLeads);
                if (mailMessage != null)
                {
                    try
                    {
                        session.OpenConnection();
                        DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);

                        List<ElioLeadDistributions> leads = loader.Load(@"SELECT
			                                                                reseller_id,
                                                                            ld.company_name,
                                                                            vendor_id,
			                                                                'NewLeadsJob',
			                                                                ld.id,
                                                                            ld.collaboration_vendor_reseller_id,
			                                                                --u2.email, 
			                                                                --u.email as 'sender_email_address',
			                                                                getdate(),
			                                                                getdate(),
			                                                                '',
			                                                                0,
			                                                                NULL,
			                                                                NULL,
			                                                                0,
			                                                                0,
			                                                                '',
			                                                                '',
			                                                                ''
		                                                                FROM Elio_lead_distributions ld
		                                                                --inner join elio_users u
			                                                            --    on u.id = ld.vendor_id
		                                                                --inner join elio_users u2
			                                                            --    on u2.id = ld.reseller_id
		                                                                where 1 = 1
			                                                                and is_new = @is_new
			                                                                and ld.is_public = 1
			                                                                and lead_result = @lead_result
			                                                                and status = @status
                                                                            and created_date >= '2020-07-01'
			                                                                and ld.id not in
			                                                                (
				                                                                select case_id
				                                                                from Elio_scheduler_notification_emails sne
				                                                                where 1 = 1	
				                                                                and sne.task_name = @task_name
			                                                                )"
                                                                , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                                , DatabaseHelper.CreateStringParameter("@lead_result", leadResult)
                                                                , DatabaseHelper.CreateIntParameter("@status", status)
                                                                , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                        if (leads.Count > 0)
                        {
                            foreach (ElioLeadDistributions lead in leads)
                            {
                                DataLoader<ElioUsers> loaderUser = new DataLoader<ElioUsers>(session);

                                ElioUsersSubAccounts subAccount = Sql.GetSubAccountByAssignedPartner(lead.CollaborationVendorResellerId, lead.ResellerId, session);
                                if (subAccount != null)
                                {
                                    #region Assign to Sub Account Partner case

                                    string companyName = "";

                                    if (subAccount.AccountStatus == (int)AccountStatus.Confirmed)
                                    {
                                        companyName = subAccount.LastName + " " + subAccount.FirstName;
                                    }
                                    else
                                    {
                                        ElioUsers vendor = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", lead.VendorId));
                                        if (vendor != null)
                                        {
                                            companyName = vendor.CompanyName;
                                        }
                                    }

                                    ElioUsers reseller = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", lead.ResellerId));
                                    if (reseller != null)
                                    {
                                        ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                        email.UserId = lead.ResellerId;
                                        email.TaskName = taskName;
                                        email.CaseId = lead.Id;
                                        email.ReceiverEmailAddress = reseller.Email;
                                        email.SenderEmailAddress = subAccount.Email;
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = DateTime.Now;
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(2);
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{VendorName}", companyName).Replace("{ClientName}", lead.CompanyName);
                                        email.EmailSubject = mailMessage.Subject.Replace("{VendorName}", companyName);
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                        DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                        schedulerLoader.Insert(email);
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region Regular case

                                    ElioUsers reseller = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", lead.ResellerId));
                                    ElioUsers vendor = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", lead.VendorId));

                                    if (reseller != null)
                                    {
                                        ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                        email.UserId = lead.ResellerId;
                                        email.TaskName = taskName;
                                        email.CaseId = lead.Id;
                                        email.ReceiverEmailAddress = reseller.Email;
                                        email.SenderEmailAddress = (vendor != null) ? vendor.Email : "";
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = DateTime.Now;
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(2);
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{VendorName}", vendor.CompanyName).Replace("{ClientName}", lead.CompanyName);
                                        email.EmailSubject = mailMessage.Subject.Replace("{VendorName}", vendor.CompanyName);
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                        DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                        schedulerLoader.Insert(email);
                                    }

                                    #endregion
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetNewLeads", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                    finally
                    {
                        session.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetNewLeads", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetPendingLeads(string leadResult, int status, string taskName, int totalSentCount)
        {
            DBSession session = new DBSession();

            try
            {
                MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.PendingLeads);
                if (mailMessage != null)
                {
                    try
                    {
                        session.OpenConnection();
                        DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);

                        List<ElioLeadDistributions> leads = loader.Load(@"SELECT
			                                                                reseller_id,
                                                                            vendor_id,
                                                                            ld.company_name,
			                                                                'PendingLeadsJob',
			                                                                ld.id,
                                                                            ld.collaboration_vendor_reseller_id,
			                                                                --u2.email as 'receiver_email_address', 
			                                                                --u.email as 'sender_email_address',
			                                                                getdate(),
			                                                                getdate(),
			                                                                '',
			                                                                0,
			                                                                NULL,
			                                                                NULL,
			                                                                0,
			                                                                0,
			                                                                '',
			                                                                '',
			                                                                ''
		                                                                FROM Elio_lead_distributions ld
		                                                                --inner join elio_users u
			                                                            --    on u.id = ld.vendor_id
		                                                                --inner join elio_users u2
			                                                            --    on u2.id = ld.reseller_id
		                                                                where 1 = 1
			                                                                and is_new IN (0,1)
			                                                                and ld.is_public = 1
			                                                                and lead_result = @lead_result
			                                                                and status = @status
                                                                            and created_date >= '2020-07-01'
			                                                                and ld.id not in
			                                                                (
				                                                                select case_id
				                                                                from Elio_scheduler_notification_emails sne
				                                                                where 1 = 1	
				                                                                and sne.task_name = @task_name
			                                                                )"
                                                                , DatabaseHelper.CreateStringParameter("@lead_result", leadResult)
                                                                , DatabaseHelper.CreateIntParameter("@status", status)
                                                                , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                        if (leads.Count > 0)
                        {
                            foreach (ElioLeadDistributions lead in leads)
                            {
                                DataLoader<ElioUsers> loaderUser = new DataLoader<ElioUsers>(session);

                                ElioUsersSubAccounts subAccount = Sql.GetSubAccountByAssignedPartner(lead.CollaborationVendorResellerId, lead.ResellerId, session);
                                if (subAccount != null)
                                {
                                    #region Assign to Sub Account Partner case

                                    string companyName = "";

                                    if (subAccount.AccountStatus == (int)AccountStatus.Completed)
                                    {
                                        companyName = subAccount.LastName + " " + subAccount.FirstName;
                                    }
                                    else
                                    {
                                        ElioUsers vendor = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", lead.VendorId));
                                        if (vendor != null)
                                        {
                                            companyName = vendor.CompanyName;
                                        }
                                    }

                                    ElioUsers resellerEmail = loaderUser.LoadSingle(@"Select email from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", lead.ResellerId));
                                    if (resellerEmail != null)
                                    {
                                        ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                        email.UserId = lead.ResellerId;
                                        email.TaskName = taskName;
                                        email.CaseId = lead.Id;
                                        email.ReceiverEmailAddress = resellerEmail.Email;
                                        email.SenderEmailAddress = subAccount.Email;
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = DateTime.Now;
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(30);
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{ClientName}", lead.CompanyName).Replace("{VendorName}", companyName);
                                        email.EmailSubject = mailMessage.Subject.Replace("{ClientName}", lead.CompanyName);
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                        DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                        schedulerLoader.Insert(email);
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region Regular case

                                    ElioUsers resellerEmail = loaderUser.LoadSingle(@"Select email from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", lead.ResellerId));
                                    ElioUsers vendor = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", lead.VendorId));

                                    if (resellerEmail != null)
                                    {
                                        ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                        email.UserId = lead.ResellerId;
                                        email.TaskName = taskName;
                                        email.CaseId = lead.Id;
                                        email.ReceiverEmailAddress = resellerEmail.Email;
                                        email.SenderEmailAddress = (vendor != null) ? vendor.Email : "";
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = DateTime.Now;
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(30);
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{ClientName}", lead.CompanyName).Replace("{VendorName}", vendor.CompanyName);
                                        email.EmailSubject = mailMessage.Subject.Replace("{ClientName}", lead.CompanyName);
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                        DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                        schedulerLoader.Insert(email);
                                    }

                                    #endregion
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetPendingLeads", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                    finally
                    {
                        session.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetPendingLeads", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetOnboardingFiles(int isNew, int daysAfter, int totalSentCount, string taskName)
        {
            DBSession session = new DBSession();

            try
            {
                MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.OnboardingFiles);
                if (mailMessage != null)
                {
                    try
                    {
                        session.OpenConnection();

                        DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

                        List<ElioCollaborationVendorsResellers> vendorResellers = loader.Load(@"SELECT
                                                                            cvr.id,
			                                                                cvr.master_user_id,
                                                                            cvr.partner_user_id			                                                                
		                                                                from Elio_collaboration_vendors_resellers cvr
		                                                                inner join elio_users u on u.id = cvr.partner_user_id
		                                                                inner join elio_users u2 on u2.id = cvr.master_user_id
		                                                                where cvr.master_user_id in 
		                                                                (
			                                                                select distinct user_id
			                                                                from Elio_onboarding_users_library_files f
			                                                                where 1 = 1
			                                                                and f.user_id = cvr.master_user_id
			                                                                and f.is_new = @is_new
			                                                                and datediff(day, date_created, getdate()) = @days_after
                                                                            and date_created >= '2020-07-01'
		                                                                )
		                                                                and cvr.invitation_status = 'Confirmed'		
		                                                                and u.company_type = 'Channel Partners'
		                                                                and u.account_status = 1
                                                                        and cvr.id not in
                                                                        (
                                                                        	select case_id
                                                                        	from Elio_scheduler_notification_emails sne
                                                                        	where 1 = 1	
                                                                        	and sne.task_name = @task_name
                                                                        )"
                                                                , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                                , DatabaseHelper.CreateIntParameter("@days_after", daysAfter)
                                                                , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                        if (vendorResellers.Count > 0)
                        {
                            foreach (ElioCollaborationVendorsResellers venRes in vendorResellers)
                            {
                                DataLoader<ElioUsers> loaderUser = new DataLoader<ElioUsers>(session);

                                ElioUsers resellerEmail = loaderUser.LoadSingle(@"Select email from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", venRes.PartnerUserId));
                                ElioUsers vendor = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", venRes.MasterUserId));

                                if (resellerEmail != null)
                                {
                                    ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                    ElioUsersSubAccounts subAccount = Sql.GetSubAccountByAssignedPartner(venRes.Id, venRes.PartnerUserId, session);
                                    if (subAccount != null)
                                    {
                                        #region Assign to Sub Account Partner case

                                        string companyName = "";

                                        if (subAccount.AccountStatus == (int)AccountStatus.Completed)
                                        {
                                            companyName = subAccount.LastName + " " + subAccount.FirstName;
                                        }
                                        else
                                            companyName = (vendor != null) ? vendor.CompanyName : "Vendor company name";

                                        email.UserId = venRes.PartnerUserId;
                                        email.TaskName = taskName;
                                        email.CaseId = venRes.Id;
                                        email.ReceiverEmailAddress = resellerEmail.Email;
                                        email.SenderEmailAddress = subAccount.Email;
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = DateTime.Now;
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = null;
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{VendorName}", companyName);
                                        email.EmailSubject = mailMessage.Subject;
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                        DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                        schedulerLoader.Insert(email);

                                        #endregion
                                    }
                                    else
                                    {
                                        #region Regular case

                                        email.UserId = venRes.PartnerUserId;
                                        email.TaskName = taskName;
                                        email.CaseId = venRes.Id;
                                        email.ReceiverEmailAddress = resellerEmail.Email;
                                        email.SenderEmailAddress = (vendor != null) ? vendor.Email : "";
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = DateTime.Now;
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = null;
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{VendorName}", vendor.CompanyName);
                                        email.EmailSubject = mailMessage.Subject;
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                        DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                        schedulerLoader.Insert(email);

                                        #endregion
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetOnboardingFiles", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                    finally
                    {
                        session.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetOnboardingFiles", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetCollaborationLibraryFiles(bool isForChannelPartner, int isNew, int daysAfter, int totalSentCount, string taskName)
        {
            DBSession session = new DBSession();

            try
            {
                MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.CollaborationLibraryFiles);
                if (mailMessage != null)
                {
                    try
                    {
                        session.OpenConnection();

                        DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);

                        List<ElioCollaborationVendorsResellers> vendorResellers = null;
                        
                        if (isForChannelPartner)
                        {
                            //string strQuery = @"SELECT 
                            //                    cvr.id,
                            //                    cvr.master_user_id,
                            //                    cvr.partner_user_id	                                                               
                            //                    FROM Elio_collaboration_vendors_resellers cvr 
                            //                    inner join Elio_collaboration_users_library_files f
                            //                     on cvr.master_user_id = f.uploaded_by_user_id
                            //                      and cvr. partner_user_id = f.user_id
                            //                    inner join elio_users u on u.id = cvr.partner_user_id
                            //                    inner join elio_users u2 on u2.id = cvr.master_user_id
                            //                    where is_new = 1
                            //                    and mailbox_id = -1
                            //                    and f.user_id <> f.uploaded_by_user_id
                            //                    and u.company_type = 'Channel Partners'
                            //                    and cvr.invitation_status = 'Confirmed'	
                            //                    and datediff(day, date_created, getdate()) = @days_after
                            //                    and date_created >= '2022-01-01'
                            //                    and f.id not in
                            //                    (
                            //                        select case_id
                            //                        from Elio_scheduler_notification_emails sne
                            //                        where 1 = 1	
                            //                        and sne.task_name = @task_name
                            //                    )
                            //                    order by user_id";

                            string strQuery = @"SELECT 
                                                cvr.id,
                                                cvr.master_user_id,
                                                cvr.partner_user_id	                                                               
                                                FROM Elio_collaboration_vendors_resellers cvr 
                                                cross apply
                                                (
	                                                select TOP 1 id,user_id,uploaded_by_user_id
	                                                from Elio_collaboration_users_library_files f
	                                                where cvr.master_user_id = f.uploaded_by_user_id
	                                                and cvr. partner_user_id = f.user_id
	                                                and is_new = 1
	                                                and mailbox_id = -1
	                                                and user_id <> uploaded_by_user_id
	                                                and datediff(day, date_created, getdate()) = @days_after
	                                                and date_created >= '2022-01-01'
	                                                group by user_id, uploaded_by_user_id,id
                                                )fls
                                                inner join elio_users u on u.id = cvr.partner_user_id
                                                inner join elio_users u2 on u2.id = cvr.master_user_id
                                                where 1 = 1
                                                and u.company_type = 'Channel Partners'
                                                and cvr.invitation_status = 'Confirmed'	
                                                and fls.id not in
                                                (
                                                    select case_id
                                                    from Elio_scheduler_notification_emails sne
                                                    where 1 = 1	
                                                    and sne.task_name = @task_name
                                                )
                                                order by user_id";

                            vendorResellers = loader.Load(strQuery
                                                , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                , DatabaseHelper.CreateIntParameter("@days_after", daysAfter)
                                                , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                            if (vendorResellers.Count > 0)
                            {
                                foreach (ElioCollaborationVendorsResellers venRes in vendorResellers)
                                {
                                    DataLoader<ElioUsers> loaderUser = new DataLoader<ElioUsers>(session);

                                    ElioUsers resellerEmail = loaderUser.LoadSingle(@"Select email from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", venRes.PartnerUserId));
                                    ElioUsers vendor = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", venRes.MasterUserId));

                                    if (resellerEmail != null)
                                    {
                                        ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                        ElioUsersSubAccounts subAccount = Sql.GetSubAccountByAssignedPartner(venRes.Id, venRes.PartnerUserId, session);
                                        if (subAccount != null)
                                        {
                                            #region Assign to Sub Account Partner case

                                            string companyName = "";

                                            if (subAccount.AccountStatus == (int)AccountStatus.Completed)
                                            {
                                                companyName = subAccount.LastName + " " + subAccount.FirstName;
                                            }
                                            else
                                                companyName = (vendor != null) ? vendor.CompanyName : "Vendor company name";

                                            email.UserId = venRes.PartnerUserId;
                                            email.TaskName = taskName;
                                            email.CaseId = venRes.Id;
                                            email.ReceiverEmailAddress = resellerEmail.Email;
                                            email.SenderEmailAddress = subAccount.Email;
                                            email.Sysdate = DateTime.Now;
                                            email.LastUpdated = DateTime.Now;
                                            email.RemindDate = DateTime.Now;
                                            email.IsSent = 0;
                                            email.DateSent = null;
                                            email.NextDateSent = null;
                                            email.Count = 0;
                                            email.SentLimitCount = totalSentCount;
                                            email.EmailTemplate = mailMessage.Body.Replace("{VendorName}", companyName);
                                            email.EmailSubject = mailMessage.Subject;
                                            email.EmailMessage = "";
                                            email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                            DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                            schedulerLoader.Insert(email);

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Regular case

                                            email.UserId = venRes.PartnerUserId;
                                            email.TaskName = taskName;
                                            email.CaseId = venRes.Id;
                                            email.ReceiverEmailAddress = resellerEmail.Email;
                                            email.SenderEmailAddress = (vendor != null) ? vendor.Email : "";
                                            email.Sysdate = DateTime.Now;
                                            email.LastUpdated = DateTime.Now;
                                            email.RemindDate = DateTime.Now;
                                            email.IsSent = 0;
                                            email.DateSent = null;
                                            email.NextDateSent = null;
                                            email.Count = 0;
                                            email.SentLimitCount = totalSentCount;
                                            email.EmailTemplate = mailMessage.Body.Replace("{VendorName}", vendor.CompanyName);
                                            email.EmailSubject = mailMessage.Subject;
                                            email.EmailMessage = "";
                                            email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                            DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                            schedulerLoader.Insert(email);

                                            #endregion
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //string strQuery = @"SELECT 
                            //                    cvr.id,
                            //                    cvr.master_user_id,
                            //                    cvr.partner_user_id	
                            //                    FROM Elio_collaboration_vendors_resellers cvr 
                            //                    inner join Elio_collaboration_users_library_files f
                            //                     on cvr.partner_user_id = f.uploaded_by_user_id
                            //                      and cvr.master_user_id = f.user_id
                            //                    inner join elio_users u on u.id = cvr.master_user_id
                            //                    inner join elio_users u2 on u2.id = cvr.partner_user_id
                            //                    where is_new = 1
                            //                    and mailbox_id = -1
                            //                    and f.user_id <> f.uploaded_by_user_id
                            //                    and u.company_type = 'Vendors'
                            //                    and cvr.invitation_status = 'Confirmed'	
                            //                    and datediff(day, date_created, getdate()) = @days_after
                            //                    and date_created >= '2022-01-01'
                            //                    and f.id not in
                            //                    (
                            //                        select case_id
                            //                        from Elio_scheduler_notification_emails sne
                            //                        where 1 = 1	
                            //                        and sne.task_name = @task_name
                            //                    )
                            //                    order by user_id";

                            string strQuery = @"SELECT 
                                                cvr.id,
                                                cvr.master_user_id,
                                                cvr.partner_user_id	
                                                FROM Elio_collaboration_vendors_resellers cvr 
                                                cross apply
                                                (
	                                                select top 1 id,user_id,uploaded_by_user_id
	                                                from Elio_collaboration_users_library_files f
	                                                where f.uploaded_by_user_id = cvr.partner_user_id
	                                                and cvr.master_user_id = f.user_id
	                                                and is_new = 1
	                                                and mailbox_id = -1
	                                                and user_id <> uploaded_by_user_id
	                                                and datediff(day, date_created, getdate()) = @days_after
	                                                and date_created >= '2022-01-01'
	                                                group by user_id, uploaded_by_user_id,id
                                                )fls
                                                inner join elio_users u on u.id = cvr.master_user_id
                                                inner join elio_users u2 on u2.id = cvr.partner_user_id
                                                where 1 = 1
                                                and u.company_type = 'Vendors'
                                                and cvr.invitation_status = 'Confirmed'                                                
                                                and fls.id not in
                                                (
                                                    select case_id
                                                    from Elio_scheduler_notification_emails sne
                                                    where 1 = 1	
                                                    and sne.task_name = @task_name
                                                )
                                                order by user_id";

                            vendorResellers = loader.Load(strQuery

                                                , DatabaseHelper.CreateIntParameter("@is_new", isNew)
                                                , DatabaseHelper.CreateIntParameter("@days_after", daysAfter)
                                                , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                            if (vendorResellers.Count > 0)
                            {
                                foreach (ElioCollaborationVendorsResellers venRes in vendorResellers)
                                {
                                    DataLoader<ElioUsers> loaderUser = new DataLoader<ElioUsers>(session);

                                    ElioUsers reseller = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", venRes.PartnerUserId));
                                    ElioUsers vendor = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", venRes.MasterUserId));

                                    if (vendor != null)
                                    {
                                        ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                        ElioUsersSubAccounts subAccount = Sql.GetSubAccountByAssignedPartner(venRes.Id, venRes.PartnerUserId, session);
                                        if (subAccount != null)
                                        {
                                            #region Assign to Sub Account Partner case

                                            string companyName = "";

                                            if (subAccount.AccountStatus == (int)AccountStatus.Completed)
                                            {
                                                companyName = subAccount.LastName + " " + subAccount.FirstName;
                                            }
                                            else
                                                companyName = (reseller != null) ? reseller.CompanyName : "Reseller company name";

                                            email.UserId = venRes.MasterUserId;
                                            email.TaskName = taskName;
                                            email.CaseId = venRes.Id;
                                            email.ReceiverEmailAddress = vendor.Email;
                                            email.SenderEmailAddress = subAccount.Email;
                                            email.Sysdate = DateTime.Now;
                                            email.LastUpdated = DateTime.Now;
                                            email.RemindDate = DateTime.Now;
                                            email.IsSent = 0;
                                            email.DateSent = null;
                                            email.NextDateSent = null;
                                            email.Count = 0;
                                            email.SentLimitCount = totalSentCount;
                                            email.EmailTemplate = mailMessage.Body.Replace("{VendorName}", reseller.CompanyName);
                                            email.EmailSubject = mailMessage.Subject;
                                            email.EmailMessage = "";
                                            email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                            DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                            schedulerLoader.Insert(email);

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Regular case

                                            email.UserId = venRes.MasterUserId;
                                            email.TaskName = taskName;
                                            email.CaseId = venRes.Id;
                                            email.ReceiverEmailAddress = vendor.Email;
                                            email.SenderEmailAddress = reseller.Email;
                                            email.Sysdate = DateTime.Now;
                                            email.LastUpdated = DateTime.Now;
                                            email.RemindDate = DateTime.Now;
                                            email.IsSent = 0;
                                            email.DateSent = null;
                                            email.NextDateSent = null;
                                            email.Count = 0;
                                            email.SentLimitCount = totalSentCount;
                                            email.EmailTemplate = mailMessage.Body.Replace("{VendorName}", reseller.CompanyName);
                                            email.EmailSubject = mailMessage.Subject;
                                            email.EmailMessage = "";
                                            email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;

                                            DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                            schedulerLoader.Insert(email);

                                            #endregion
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetCollaborationLibraryFilesForType", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                    finally
                    {
                        session.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetOnboardingFiles", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetCollaborationMailboxMessages(bool isForChannelPartner, string taskName, int totalSentCount)
        {
            DBSession session = new DBSession();

            try
            {
                MailMessage mailMessage = TasksLib.GetTemplate(TasksNotificationDescriptions.CollaborationMailboxMessages);
                if (mailMessage != null)
                {
                    try
                    {
                        session.OpenConnection();
                        DataLoader<ElioCollaborationUsersMailbox> loader = new DataLoader<ElioCollaborationUsersMailbox>(session);

                        string strQuery = "";

                        if (isForChannelPartner)
                        {
                            strQuery = @"select distinct 
                                            cum.vendors_resellers_id,
				                            cum.master_user_id,
                                            cum.partner_user_id,
				                            'CollaborationMailboxMessagesJob',
				                            cum.mailbox_id,
				                            --u2.email as 'receiver_email_address',			
				                            --u.email as 'sender_email_address',
				                            getdate(),
				                            getdate(),
				                            NULL,
				                            0,
				                            NULL,
				                            NULL,
				                            0,
				                            0,
				                            '',
				                            '',
				                            ''
                                    from Elio_collaboration_users_mailbox cum
                                    --inner join elio_users u
                                    --  on u.id = cum.master_user_id
                                    --inner join elio_users u2
                                    --    on u2.id = cum.partner_user_id
                                    cross apply
                                    (
                                        select cm.user_id--, count(id) as messages_count
                                        from Elio_collaboration_mailbox cm
                                        where cm.id = cum.mailbox_id
                                        and cm.is_public = 1
                                        and cm.user_id = cum.master_user_id-- from vendor
                                    ) m
                                    where 1 = 1
                                    and cum.is_new = 1
                                    and cum.is_viewed = 0
                                    and cum.date_viewed is null
                                    and cum.is_deleted = 0
                                    and cum.date_deleted is null
                                    and cum.is_public = 1
                                    and cum.sysdate >= '2020-07-01'
                                    and cum.mailbox_id not in
                                    (
                                        select case_id
                                        from Elio_scheduler_notification_emails sne
                                        where 1 = 1
                                        and sne.task_name = @task_name
			                        )";
                        }
                        else
                        {
                            strQuery = @"select distinct 
                                            cum.vendors_resellers_id,
                                            cum.master_user_id,
				                            cum.partner_user_id,
				                            'CollaborationMailboxMessagesJob',
				                            cum.mailbox_id,
				                            --u.email as 'receiver_email_address',			
				                            --u2.email as 'sender_email_address',
				                            getdate(),
				                            getdate(),
				                            NULL,
				                            0,
				                            NULL,
				                            NULL,
				                            0,
				                            0,
				                            '',
				                            '',
				                            ''
                                    from Elio_collaboration_users_mailbox cum
                                    --inner join elio_users u
                                    --  on u.id = cum.master_user_id
                                    --inner join elio_users u2
                                    --    on u2.id = cum.partner_user_id
                                    cross apply
                                    (
                                        select cm.user_id--, count(id) as messages_count
                                        from Elio_collaboration_mailbox cm
                                        where cm.id = cum.mailbox_id
                                        and cm.is_public = 1
                                        and cm.user_id = cum.partner_user_id     -- from channel partner
                                    ) m
                                    where 1 = 1
                                    and cum.is_new = 1
                                    and cum.is_viewed = 0
                                    and cum.date_viewed is null
                                    and cum.is_deleted = 0
                                    and cum.date_deleted is null
                                    and cum.is_public = 1
                                    and cum.sysdate >= '2020-07-01'
                                    and cum.mailbox_id not in
                                    (
                                        select case_id
                                        from Elio_scheduler_notification_emails sne
                                        where 1 = 1
                                        and sne.task_name = @task_name
			                        )";
                        }

                        List<ElioCollaborationUsersMailbox> usersMailbox = loader.Load(strQuery
                                                                                , DatabaseHelper.CreateStringParameter("@task_name", taskName));

                        if (usersMailbox.Count > 0)
                        {
                            foreach (ElioCollaborationUsersMailbox userMailB in usersMailbox)
                            {
                                DataLoader<ElioUsers> loaderUser = new DataLoader<ElioUsers>(session);

                                ElioUsers reseller = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", userMailB.PartnerUserId));
                                ElioUsers vendor = loaderUser.LoadSingle(@"Select email, company_name from elio_users where id = @id", DatabaseHelper.CreateIntParameter("@id", userMailB.MasterUserId));

                                ElioSchedulerNotificationEmails email = new ElioSchedulerNotificationEmails();

                                if (isForChannelPartner)
                                {
                                    if (reseller != null)
                                    {
                                        email.UserId = userMailB.MasterUserId;
                                        email.TaskName = taskName;
                                        email.CaseId = userMailB.MailboxId;
                                        email.ReceiverEmailAddress = reseller.Email;
                                        email.SenderEmailAddress = (vendor != null) ? vendor.Email : "";
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = DateTime.Now;
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(2);
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{CompanyName}", vendor.CompanyName);
                                        email.EmailSubject = mailMessage.Subject.Replace("{CompanyName}", vendor.CompanyName);
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;
                                    }
                                }
                                else
                                {
                                    if (vendor != null)
                                    {
                                        email.UserId = userMailB.PartnerUserId;
                                        email.TaskName = "CollaborationMailboxMessagesJob";
                                        email.CaseId = userMailB.MailboxId;
                                        email.ReceiverEmailAddress = vendor.Email;
                                        email.SenderEmailAddress = (reseller != null) ? reseller.Email : "";
                                        email.Sysdate = DateTime.Now;
                                        email.LastUpdated = DateTime.Now;
                                        email.RemindDate = null;
                                        email.IsSent = 0;
                                        email.DateSent = null;
                                        email.NextDateSent = null;
                                        email.Count = 0;
                                        email.SentLimitCount = totalSentCount;
                                        email.EmailTemplate = mailMessage.Body.Replace("{CompanyName}", reseller.CompanyName);
                                        email.EmailSubject = mailMessage.Subject.Replace("{CompanyName}", reseller.CompanyName);
                                        email.EmailMessage = "";
                                        email.IsActive = (ConfigurationManager.AppSettings["IsSchedulerJobsActivated"] != null) ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSchedulerJobsActivated"].ToString()) : false;
                                    }
                                }

                                DataLoader<ElioSchedulerNotificationEmails> schedulerLoader = new DataLoader<ElioSchedulerNotificationEmails>(session);
                                schedulerLoader.Insert(email);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetCollaborationMailboxMessages", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                    finally
                    {
                        session.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetCollaborationMailboxMessages", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void GetSnitcherLeads()
        {
            DBSession session = new DBSession();

            try
            {
                session.OpenConnection();

                string date = "";

                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString();
                string day = DateTime.Now.Day.ToString();

                date = year + "-" + month + "-" + day;

                //List<ElioSnitcherWebsites> websites = Lib.Services.AnonymousTrackingAPI.SnitcherService.GetWebsitesList(1, session);
                //if (websites != null && websites.Count > 0)
                //{
                //    if (websites[0].WebsiteId != "")
                //    {
                //        List<ElioSnitcherWebsiteLeads> leads = Lib.Services.AnonymousTrackingAPI.SnitcherService.GetWebsiteLeads(websites[0], 1, date, session);
                //    }
                //}
                //else
                //{
                ElioSnitcherWebsites website = Sql.GetSnitcherWebsite("19976", session);
                if (website != null)
                {
                    List<ElioSnitcherWebsiteLeads> leads = Lib.Services.AnonymousTrackingAPI.SnitcherService.GetWebsiteLeads(website, 1, date, session);

                    if (leads != null && leads.Count > 0)
                    {
                        string message = string.Format("{0} leads were inserted to Elio from Snitcher API", leads.Count.ToString());
                        if (date != "")
                            message += " at date " + date;

                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetSnitcherLeads", message);
                    }
                    else
                    {
                        string message = string.Format("No new leads (count: {0}) were inserted to Elio from Snitcher API", 0);
                        if (date != "")
                            message += " at date " + date;

                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetSnitcherLeads", message);
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetSnitcherLeads", ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        public static void GetIpInfoLeads()
        {
            DBSession session = new DBSession();
            
            List<ElioSnitcherWebsiteLeads> infoLeads = new List<ElioSnitcherWebsiteLeads>();

            try
            {
                session.OpenConnection();

                List<ElioAnonymousIpInfo> leadInfos = Sql.GetAnonymousIpInfoByInsertedStatus(0, session);
                foreach (ElioAnonymousIpInfo leadInfo in leadInfos)
                {
                    try
                    {
                        session.BeginTransaction();

                        if (!string.IsNullOrEmpty(leadInfo.CompanyDomain))
                        {
                            ElioAnonymousCompaniesInfo companyInfo = Lib.Services.TheCompaniesAPI.CompaniesServiceAPI.GetCompaniesInfo(leadInfo.CompanyDomain, session);

                            if (companyInfo != null)
                            {
                                ElioSnitcherWebsiteLeads infoLead = Lib.Services.AnonymousTrackingAPI.SnitcherService.GetWebsiteLeadsFromInfo(companyInfo, leadInfo, session);

                                if (infoLead != null)
                                    infoLeads.Add(infoLead);
                            }
                        }

                        leadInfo.IsInserted = 1;

                        DataLoader<ElioAnonymousIpInfo> loader = new DataLoader<ElioAnonymousIpInfo>(session);
                        loader.Update(leadInfo);

                        session.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();
                        Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetIpInfoLeads", ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                string message = string.Format("{0} leads were inserted to Elio from Anonymous Ip Info/The Companies API at {1}", infoLeads.Count.ToString(), DateTime.Now);

                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetIpInfoLeads", message);
            }
            catch (Exception ex)
            {
                Logger.DetailedError("HangfireDashboard.DBModel.DBQueries.GetIpInfoLeads", ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }
    }
}