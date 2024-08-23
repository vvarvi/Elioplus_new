using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.HangfireJobs.Enumerators
{
    public enum TasksNotificationDescriptions
    {
        InboxMessages = 1,
        TeamSubAccounts = 2,
        TasksReminder = 3,
        PendingInvitations = 4,
        PendingRequests = 5,
        NotConfirmedDeals = 6,
        ExpiringDeals = 7,
        NewLeads = 8,
        PendingLeads = 9,
        OnboardingFiles = 10,
        CollaborationMailboxMessages = 11,
        ExpiredDeals = 12,
        CollaborationLibraryFiles = 13
    }
}