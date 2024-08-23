using System;
using System.Linq;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum EmailNotificationDesctriptions
    {
        NewReview = 1,
        NewInboxMessage = 2,
        EmailToFriends = 3,
        ResentLeads = 4,
        NotFullRegisteredUser = 5,
        ActivationAccount = 6,
        NewFullRegisteredUser = 7,
        NewSimpleRegisteredUser = 8,
        Resetpassword = 9,
        ContactElioplusMessage = 10,
        ErrorNotificationEmail = 11,
        CommunityUpvoteUserPost = 12,
        CommunityCommentUserPost = 13,
        CommunityFollowUserPost = 14,
        CommunityCreateNewPost = 15,

        NewSimpleRegisteredCommunityUser = 16,
        NewFullRegisteredCommunityUser = 17,

        InboxEmail = 18,

        InvitationEmail = 19,

        StripeTrialActivationEmail = 20,

        NewTaskNotificationEmail = 21,
        TaskReminderEmail = 22,

        NewMultiAccountRegisteredUser = 23,

        CollaborationInvitationEmailForExistingConnections = 24,
        CollaborationInvitationEmailForUserPartners = 25,
        AcceptInvitationEmail = 26,
        CollaborationInvitationEmailForNotFullRegisteredUsers = 27,

        NewSimpleRegisteredThirdPartyToElioUser = 28,
        ClaimProfileResetPasswordEmail = 29,
        ContactElioplusDemoRequestMessage = 30,
        NewUploadedFileEmail = 31,
        NewDealRegistrationEmail = 32,
        NewDealRegistrationChangeStatusEmail = 33,
        NewDealRegistrationWonLostEmail = 34,
        NewLeadDistributionEmail = 35,
        NewLeadDistributionWonLostEmail = 36,
        NewPartner2PartnerDealRegistrationEmail = 37,
        ContactElioplusToApproveDemoRequestEmail = 38,
        RejectInvitationEmail = 39,
        NewPartner2PartnerChangeStatusDealRegistrationEmail = 40,
        CollaborationInvitationEmailFromChannelPartners = 41,
        NewVendorPartnerSimpleRegisteredUser = 42,
        NewPRMFullRegisteredUser = 43,
        NewPRMSimpleRegisteredUser = 44,
        NewVendorPartnerFullRegisteredUser = 45,
        NewDealRegistrationCommentEmail = 46,

        AcceptRequestEmail = 47,
        RejectRequestEmail = 48,
        DeletePartnerNotificationEmail = 49,
        NewUploadedCollaborationLibraryFileEmail = 50,
        MarketplaceRequestMessageEmail = 51,
        RFQApprovedRequestEmail = 52,
        RFQApprovedMessageEmail = 53
    }
}