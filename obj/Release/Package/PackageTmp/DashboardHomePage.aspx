<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardHomePage.aspx.cs" Inherits="WdS.ElioPlus.DashboardHomePage" %>

<%@ Register Src="~/DashboardArea/Components/Grid/DealsPendingGridControl.ascx" TagName="UcDealsPendingGridControl" TagPrefix="controls" %>
<%@ Register Src="~/DashboardArea/Components/Grid/PartnersGridControl.ascx" TagName="UcPartnersGridControl" TagPrefix="controls" %>
<%@ Register Src="~/DashboardArea/Components/Grid/TeamsGridControl.ascx" TagName="UcTeamsGridControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
    <!--begin::Page Custom Styles(used by this page)-->
    <link href="/assets/plugins/custom/kanban/kanban.bundle.css" rel="stylesheet" type="text/css" />
    <!--end::Page Custom Styles-->
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xl-8">
                            <!--begin::Row-->
                            <div class="row">
                                <div class="col-xl-12">
                                    <!--begin::Card-->
                                    <div class="card card-custom gutter-b">
                                        <div class="card-header">
                                            <div class="card-title">
                                                <h3 class="card-label">Notifications</h3>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <!--begin::Example-->
                                            <div class="example example-basic">
                                                <div class="example-preview">
                                                    <div class="timeline timeline-5">
                                                        <div id="divUserRegistrationNotificationsArea" visible="false" runat="server" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-rocket"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-danger"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="LblUserRegistrationNotificationsTitle" Text="" runat="server" />
                                                                </h3>
                                                                <p>
                                                                    <asp:Label ID="LblUserRegistrationNotificationsContent" Text="" runat="server" />
                                                                    <a id="aFaqPageLink" runat="server">
                                                                        <asp:Label ID="LblFaqPage" Text="" runat="server" />
                                                                    </a>
                                                                    <asp:Label ID="LblUserRegistrationNotificationsContent2" Text="" runat="server" />
                                                                </p>
                                                                <p>
                                                                    <asp:Label ID="LblUserRegistrationNotificationsContent3" Text="" runat="server" />
                                                                </p>
                                                                <p>
                                                                    <asp:Label ID="LblUserRegistrationNotificationsContent4" Text="" runat="server" />
                                                                    <a id="aLearnMoreLink" runat="server">
                                                                        <span class="label label-lg label-light-primary label-inline">
                                                                            <asp:Label ID="LblLearnMoreLink" Text="" runat="server" />
                                                                            <i class="fa fa-share ml-2"></i>
                                                                        </span>
                                                                    </a>
                                                                </p>
                                                            </div>
                                                        </div>
                                                        <div id="divNoExistCollaborationNotificationsArea" runat="server" visible="false" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-danger"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="LblNotificationTitle" Text="Notification" Visible="true" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblNotific" Visible="false" runat="server" />
                                                                <a id="aInvitationToPartners" runat="server" visible="false">
                                                                    <span class="label label-lg label-light-primary label-inline">
                                                                        <asp:Label ID="LblInvitationToPartners" Text="Invite your technology partners" runat="server" />
                                                                        <i class="fa fa-share ml-2"></i>
                                                                    </span>
                                                                </a>
                                                            </div>
                                                        </div>
                                                        <div id="DealPartnerDirectoryNotif" visible="false" runat="server" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-success"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="Label11" Text="Invitation Requests Notification" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblDealPartnerDirectoryInvitationNotifications" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div id="divCompleteRegistrationNotifications" runat="server" visible="false" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-warning"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="LblCompleteRegistrationPageInfo" Visible="true" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblGoFullRegister" runat="server" />
                                                                <a id="aGoFullRegister" runat="server">
                                                                    <span class="label label-lg label-light-primary label-inline">
                                                                        <asp:Label ID="LblGoFullRegisterLink" runat="server" />
                                                                        <i class="fa fa-share ml-2"></i>
                                                                    </span>
                                                                </a>
                                                            </div>
                                                        </div>
                                                        <div id="divInvitationNotifications" visible="false" runat="server" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-danger"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="LblCollaborationNotificationTitle" Text="Collaboration Notifications" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblCollaborationNotificationContent" Text="You have pending request for collaboration. To accept the invitation " runat="server" />
                                                                <a id="aCollaborationInvitationPartners" visible="false" runat="server">
                                                                    <span class="label label-lg label-light-primary label-inline">click here
                                                            <i class="fa fa-share ml-2"></i>
                                                                    </span>
                                                                </a>
                                                            </div>
                                                        </div>
                                                        <div id="divChatMessagesNotifications" visible="false" runat="server" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-danger"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="Label2" Text="Collaboration Chat Messages Notification" Visible="true" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblTotalMsgsCount" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div id="divNewFilesNotifications" visible="false" runat="server" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-success"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="Label4" Text="New Library Files Received Notification" Visible="true" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblNewFilesNotifications" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div id="divResellerNewUploadedOnboardingFilesNotification" visible="false" runat="server" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-success"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="Label8" Text="New Uploaded Files Notification" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblNewOnboardingFilesNotifications" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div id="divDealRegistrationNotApprovedNotification" visible="false" runat="server" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-danger"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="Label1" Text="Not Confirmed Deal Registration Notifications" Visible="true" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblDealRegistrationNotApprovedNotification" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div id="divDealRegistrationApprovedNotification" runat="server" visible="false" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-danger"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="Label5" Text="Approved Deal Registration Notifications" Visible="true" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblDealRegistrationApprovedNotification1" runat="server" />
                                                                <asp:Label ID="LblDealRegistrationApprovedNotification2" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div id="divResellersP2PDealRegistrationNotification" visible="false" runat="server" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-success"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="Label10" Text="Partner 2 Partner Deal Registration Notifications" Visible="true" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblResellersP2PDealRegistrationNotification1" runat="server" />
                                                                <asp:Label ID="LblResellersP2PDealRegistrationNotification2" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div id="divVendorsLeadDistributionNotification" runat="server" visible="false" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-warning"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="Label7" Text="Lead Distribution Notifications" Visible="true" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblVendorsLeadDistributionNotification" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div id="divResellersLeadDistributionNotification" runat="server" visible="false" class="timeline-item align-items-start">
                                                            <div class="timeline-label">
                                                                <i class="icon-2x text-dark-50 flaticon2-notification"></i>
                                                            </div>
                                                            <div class="timeline-badge">
                                                                <i class="fa fa-genderless text-warning"></i>
                                                            </div>
                                                            <div class="timeline-content">
                                                                <h3>
                                                                    <asp:Label ID="Label3" Text="Lead Distribution Notifications" Visible="true" runat="server" />
                                                                </h3>
                                                                <asp:Label ID="LblResellersLeadDistributionNotification1" runat="server" />
                                                                <asp:Label ID="LblResellersLeadDistributionNotification2" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end::Example-->
                                        </div>
                                    </div>
                                    <!--end::Card-->
                                </div>
                            </div>
                            <!--end::Row-->
                            <!--begin::Row-->
                            <div id="divNewPartners" runat="server" class="row">
                                <div class="col-xl-12">
                                    <!--begin::Base Table Widget 1-->
                                    <div class="card card-custom card-stretch gutter-b">
                                        <!--begin::Header-->
                                        <div class="card-header border-0 pt-5">
                                            <h3 class="card-title align-items-start flex-column">
                                                <span class="card-label font-weight-bolder text-dark">New Partners</span>
                                            </h3>

                                        </div>
                                        <!--end::Header-->
                                        <!--begin::Body-->
                                        <div class="card-body pt-2 pb-0">
                                            <!--begin::Table-->
                                            <controls:UcPartnersGridControl ID="UcPartnersGridControl" runat="server" />
                                            <!--end::Table-->
                                        </div>
                                    </div>
                                    <!--end::Base Table Widget 1-->
                                </div>
                            </div>
                            <!--end::Row-->
                            <!--begin::Row-->
                            <div class="row">
                                <div class="col-xl-12">
                                    <!--begin::Base Table Widget 1-->
                                    <div class="card card-custom card-stretch gutter-b">
                                        <!--begin::Header-->
                                        <div class="card-header border-0 pt-5">
                                            <h3 class="card-title align-items-start flex-column">
                                                <span class="card-label font-weight-bolder text-dark">New Deals Registered</span>
                                                <span class="text-muted mt-3 font-weight-bold font-size-sm"></span>
                                            </h3>
                                        </div>
                                        <!--end::Header-->
                                        <!--begin::Body-->
                                        <div class="card-body pt-2 pb-0">
                                            <!--begin::Table-->
                                            <controls:UcDealsPendingGridControl ID="UcDealsPendingGridControl" runat="server" />
                                            <!--end::Table-->
                                        </div>
                                    </div>
                                    <!--end::Base Table Widget 1-->
                                </div>
                            </div>
                            <!--end::Row-->
                        </div>
                        <div class="col-xl-4">
                            <div class="col-xl-12" style="height: 100%;">
                                <!--begin::Aside Secondary-->
                                <div class="sidebar sidebar-left d-flex flex-row-auto flex-column" id="kt_sidebar" style="background-color: #F5EBDF; border-radius: 0.50rem;">
                                    <!--begin::Aside Secondary Header-->
                                    <div class="sidebar-header flex-column-auto pt-9 pb-5 px-5 px-lg-10">
                                        <!--begin::Toolbar-->
                                        <div id="divUpgrade" runat="server" class="d-flex">
                                            <!--begin::Button-->
                                            <a id="aBtnGoPremium" style="width: 100%;" runat="server" class="btn btn-success font-weight-bolder">
                                                <span class="svg-icon svg-icon-md">
                                                    <!--begin::Svg Icon | path:assets/media/svg/icons/Design/Flatten.svg-->
                                                    <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                            <rect x="0" y="0" width="24" height="24" />
                                                            <circle fill="#000000" cx="9" cy="15" r="6" />
                                                            <path d="M8.8012943,7.00241953 C9.83837775,5.20768121 11.7781543,4 14,4 C17.3137085,4 20,6.6862915 20,10 C20,12.2218457 18.7923188,14.1616223 16.9975805,15.1987057 C16.9991904,15.1326658 17,15.0664274 17,15 C17,10.581722 13.418278,7 9,7 C8.93357256,7 8.86733422,7.00080962 8.8012943,7.00241953 Z" fill="#000000" opacity="0.3" />
                                                        </g>
                                                    </svg>
                                                    <!--end::Svg Icon-->
                                                </span>
                                                <asp:Label ID="LblBtnGoPremium" Text="View Plans & Pricing" runat="server" />
                                            </a>
                                            <!--end::Button-->
                                        </div>
                                        <!--end::Toolbar-->
                                    </div>
                                    <!--end::Aside Secondary Header-->
                                    <!--begin::Aside Secondary Content-->
                                    <div class="sidebar-content flex-column-fluid pb-10 pt-9 px-5 px-lg-10">
                                        <!--begin::Engage Widget 8-->
                                        <div id="divBookADemo" runat="server" class="card card-custom card-stretch gutter-b">
                                            <div class="card-body p-0 d-flex">
                                                <div class="d-flex align-items-start justify-content-start flex-grow-1 p-8 card-rounded flex-grow-1 position-relative">
                                                    <div class="d-flex flex-column align-items-start flex-grow-1 h-100">
                                                        <div class="p-1 flex-grow-1">
                                                            <h4 class="text-warning font-weight-bolder">
                                                                <asp:Label ID="LblTalkUsTitle" Text="Talk to Us" runat="server" />
                                                            </h4>
                                                            <p class="text-dark-50 font-weight-bold mt-3">
                                                                <asp:Label ID="LblScheduleContent" Text="Let’s schedule a call" runat="server" />
                                                            </p>
                                                        </div>
                                                        <a id="aScheduleDemo" runat="server" class="btn btn-link btn-link-warning font-weight-bold">
                                                            <span class="svg-icon svg-icon-lg svg-icon-warning">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Navigation/Arrow-right.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <polygon points="0 0 24 0 24 24 0 24" />
                                                                        <rect fill="#000000" opacity="0.3" transform="translate(12.000000, 12.000000) rotate(-90.000000) translate(-12.000000, -12.000000)" x="11" y="5" width="2" height="14" rx="1" />
                                                                        <path d="M9.70710318,15.7071045 C9.31657888,16.0976288 8.68341391,16.0976288 8.29288961,15.7071045 C7.90236532,15.3165802 7.90236532,14.6834152 8.29288961,14.2928909 L14.2928896,8.29289093 C14.6714686,7.914312 15.281055,7.90106637 15.675721,8.26284357 L21.675721,13.7628436 C22.08284,14.136036 22.1103429,14.7686034 21.7371505,15.1757223 C21.3639581,15.5828413 20.7313908,15.6103443 20.3242718,15.2371519 L15.0300721,10.3841355 L9.70710318,15.7071045 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.999999, 11.999997) scale(1, -1) rotate(90.000000) translate(-14.999999, -11.999997)" />
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                            <asp:Label ID="LblScheduleDemo" Text="Book a Demo" runat="server" />
                                                        </a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end::Engage Widget 8-->
                                        <!--begin::Tiles Widget 8-->
                                        <div id="divPartnerManagement" runat="server" class="card card-custom gutter-b card-stretch">
                                            <!--begin::Header-->
                                            <div class="card-header border-0 pt-5">
                                                <div class="card-title">
                                                    <div class="card-label">
                                                        <div class="font-weight-bolder">Partner Management</div>
                                                    </div>
                                                </div>
                                                <div class="card-toolbar">
                                                    <span id="spanNotificationPrmPlan" class="label label-lg label-light-primary label-inline" style="height:45px !important;" title="New unread message" runat="server">
                                                        <asp:Label ID="LblNotificationPrmPlan" Text="Free Plan" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                            <!--end::Header-->
                                            <!--begin::Body-->
                                            <div class="card-body d-flex flex-column p-0">
                                                <!--begin::Items-->
                                                <div class="flex-grow-1 card-spacer">
                                                    <!--begin::Item-->
                                                    <div class="d-flex align-items-center justify-content-between mb-10">
                                                        <div class="d-flex align-items-center mr-2">
                                                            <div class="symbol symbol-40 symbol-light-primary mr-3 flex-shrink-0">
                                                                <div class="flaticon-users">
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <div class="flex-row-fluid mb-7">
                                                                    <span class="font-size-sm font-weight-bold">
                                                                        <asp:Label ID="LblManagePartners" Text="Remaining partners to manage" runat="server" />
                                                                    </span>
                                                                    <span class="ml-3 font-size-sm text-muted">
                                                                        <asp:Label ID="LblUserManagePartnersValue" Text="0/0" runat="server" />
                                                                    </span>
                                                                    <div class="d-flex align-items-center pt-2">
                                                                        <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                            <div id="spanManagePartnersProgress" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <!--end::Item-->
                                                    <!--begin::Item-->
                                                    <div class="d-flex align-items-center justify-content-between mb-10">
                                                        <div class="d-flex align-items-center mr-2">
                                                            <div class="symbol symbol-40 symbol-light-warning mr-3 flex-shrink-0">
                                                                <div class="flaticon-interface-3">
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <div class="flex-row-fluid mb-7">
                                                                    <span class="font-size-sm font-weight-bold">
                                                                        <asp:Label ID="LblLibraryStorage" Text="Remaining storage" runat="server" />
                                                                    </span>
                                                                    <span class="ml-3 font-size-sm text-muted">
                                                                        <asp:Label ID="LblUserLibraryStorageValue" Text="0/0" runat="server" />
                                                                    </span>
                                                                    <div class="d-flex align-items-center pt-2">
                                                                        <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                            <div id="spanLibraryStorageProgress" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <!--end::Item-->
                                                </div>
                                                <!--end::Items-->
                                            </div>
                                            <!--end::Body-->
                                        </div>
                                        <!--end::Tiles Widget 8-->
                                        <!--begin::Tiles Widget 8-->
                                        <div id="divPartnerReqruitment" runat="server" class="card card-custom gutter-b card-stretch">
                                            <!--begin::Header-->
                                            <div class="card-header border-0 pt-5">
                                                <div class="card-title">
                                                    <div class="card-label">
                                                        <div class="font-weight-bolder">Partner Recruitment</div>
                                                    </div>
                                                </div>
                                                <div class="card-toolbar">
                                                    <span id="spanNotificationReqruitmentPlan" class="label label-lg label-light-primary label-inline" style="height:45px !important;" title="New unread message" runat="server">
                                                        <asp:Label ID="LblNotificationReqruitmentPlan" Text="Free Plan" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                            <!--end::Header-->
                                            <!--begin::Body-->
                                            <div class="card-body d-flex flex-column p-0">
                                                <!--begin::Items-->
                                                <div class="flex-grow-1 card-spacer">
                                                    <!--begin::Item-->
                                                    <div class="d-flex align-items-center justify-content-between mb-10">
                                                        <div class="d-flex align-items-center mr-2">
                                                            <div class="symbol symbol-40 symbol-light-primary mr-3 flex-shrink-0">
                                                                <div class="flaticon-customer">
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <div class="flex-row-fluid mb-7">
                                                                    <span class="font-size-sm font-weight-bold">
                                                                        <asp:Label ID="LblConnectionsRemaining" Text="Remaining Matches" runat="server" />
                                                                    </span>
                                                                    <span class="ml-3 font-size-sm text-muted">
                                                                        <asp:Label ID="LblUserConnectionsValue" Text="0/0" runat="server" />
                                                                    </span>
                                                                    <div class="d-flex align-items-center pt-2">
                                                                        <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                            <div id="spanConnectionsProgress" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <!--end::Item-->
                                                    <!--begin::Item-->
                                                    <div class="d-flex align-items-center justify-content-between mb-10">
                                                        <div class="d-flex align-items-center mr-2">
                                                            <div class="symbol symbol-40 symbol-light-warning mr-3 flex-shrink-0">
                                                                <div class="flaticon-paper-plane-1">
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <div class="flex-row-fluid mb-7">
                                                                    <span class="font-size-sm font-weight-bold">
                                                                        <asp:Label ID="LblMessagesRemaining" Text="Remaining Messages" runat="server" />
                                                                    </span>
                                                                    <span class="ml-3 font-size-sm text-muted">
                                                                        <asp:Label ID="LblUserMessagesValue" Text="0/0" runat="server" />
                                                                    </span>
                                                                    <div class="d-flex align-items-center pt-2">
                                                                        <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                            <div id="spanMessagesProgress" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <!--end::Item-->
                                                </div>
                                                <!--end::Items-->
                                            </div>
                                            <!--end::Body-->
                                        </div>
                                        <!--end::Tiles Widget 8-->
                                        <!--begin::List Widget 3-->
                                        <div id="divPartnerships" runat="server" class="card card-custom card-stretch gutter-b">
                                            <!--begin::Header-->
                                            <div class="card-header border-0">
                                                <h3 class="card-title font-weight-bolder text-dark">Partnerships</h3>
                                                <div class="card-toolbar">
                                                </div>
                                            </div>
                                            <!--end::Header-->
                                            <!--begin::Body-->
                                            <div class="card-body pt-2">
                                                <!--begin::Item-->
                                                <asp:Repeater ID="RptVendors" OnLoad="RptVendors_Load" OnItemDataBound="RptVendors_ItemDataBound" runat="server">
                                                    <ItemTemplate>
                                                        <main class="kanban-drag" style="min-height: 0px;">
                                                            <div class="kanban-item">
                                                                <div class="card-body pt-1" style="padding: 1.5rem;">
                                                                    <!--begin::User-->
                                                                    <div class="d-flex align-items-end mb-3">
                                                                        <!--begin::Pic-->
                                                                        <div class="d-flex align-items-center">
                                                                            <!--begin::Symbol-->
                                                                            <div id="divImg" runat="server" class="symbol symbol-light-primary mr-5">
                                                                                <span class="symbol-label">
                                                                                    <asp:Image ID="img" runat="server" Width="35" Height="35" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "company_logo")%>' AlternateText="" />
                                                                                </span>
                                                                            </div>
                                                                            <div id="divImgNo" runat="server" visible="false" class="symbol symbol-light-primary mr-5 symbol-light-primary">
                                                                                <span class="symbol-label font-size-h3 font-weight-boldest">NO</span>
                                                                            </div>
                                                                            <!--end::Symbol-->
                                                                            <!--begin::Title-->
                                                                            <div class="d-flex flex-column align-items-start">
                                                                                <span class="text-dark-50 font-weight-bold mb-1">
                                                                                    <asp:Label ID="LblCompanyName" Text='<%# DataBinder.Eval(Container.DataItem, "company_name")%>' runat="server" />
                                                                                </span>
                                                                            </div>
                                                                            <!--end::Title-->
                                                                        </div>
                                                                        <!--end::Title-->
                                                                    </div>
                                                                    <!--end::User-->
                                                                    <!--begin::Info-->
                                                                    <div class="mb-2 mt-1">
                                                                        <div class="d-flex justify-content-between align-items-center">
                                                                            <span class="font-weight-bold mr-2" style="font-size: 13px;">Invitation status:
                                                                            </span>
                                                                            <asp:Label ID="LblInvitationStatus" CssClass="label label-lg label-light-success label-inline mb-2" Text='<%# DataBinder.Eval(Container.DataItem, "invitation_status")%>' runat="server" />
                                                                        </div>
                                                                        <div class="d-flex justify-content-between align-items-center">
                                                                            <span class="font-weight-bold mr-2" style="font-size: 13px;">Tier status:
                                                                            </span>
                                                                            <span class="text-muted font-weight-bold">
                                                                                <asp:Label ID="LblTierStatus" CssClass="label label-lg label-light-primary label-inline" Text='<%# DataBinder.Eval(Container.DataItem, "tier_status")%>' runat="server" />
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                    <!--end::Info-->
                                                                </div>
                                                            </div>
                                                        </main>
                                                        <footer></footer>
                                                        <asp:HiddenField ID="HdnCollaborationId" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <controls:MessageControl ID="UcMessage1" runat="server" />
                                                <!--end::Item-->
                                            </div>
                                            <!--end::Body-->
                                        </div>
                                        <!--end::List Widget 3-->
                                        <!--begin::Tiles Widget 8-->
                                        <div id="divChannelPartnerGoals" runat="server" class="card card-custom gutter-b card-stretch">
                                            <!--begin::Header-->
                                            <div class="card-header border-0 pt-5">
                                                <div class="card-title">
                                                    <div class="card-label">
                                                        <div class="font-weight-bolder">Goals</div>
                                                    </div>
                                                </div>
                                                <div class="card-toolbar">
                                                </div>
                                            </div>
                                            <!--end::Header-->
                                            <!--begin::Body-->
                                            <div class="card-body d-flex flex-column p-0">
                                                <!--begin::Items-->
                                                <div class="flex-grow-1 card-spacer">
                                                    <!--begin::Item-->
                                                    <div class="d-flex align-items-center justify-content-between mb-10">
                                                        <div class="d-flex align-items-center mr-2">
                                                            <div class="symbol symbol-40 symbol-light-primary mr-3 flex-shrink-0">
                                                                <div class="flaticon-customer">
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <div class="flex-row-fluid mb-7">
                                                                    <span class="font-size-sm font-weight-bold">
                                                                        <asp:Label ID="LblGoalQuarter" Text="Current Quarter Progress" runat="server" />
                                                                    </span>
                                                                    <span class="ml-3 font-size-sm text-muted">
                                                                        <asp:Label ID="LblGoalQuarterTextValue" Text="67/100" runat="server" />
                                                                    </span>
                                                                    <div class="d-flex align-items-center pt-2">
                                                                        <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                            <div id="spanGoalQuarterValue" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 67%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <!--end::Item-->
                                                </div>
                                                <!--end::Items-->
                                            </div>
                                            <!--end::Body-->
                                        </div>
                                        <!--end::Tiles Widget 8-->
                                        <!--begin::List Widget 11-->
                                        <div id="divQuickLinks" runat="server" class="card card-custom card-stretch gutter-b">
                                            <!--begin::Header-->
                                            <div class="card-header border-0">
                                                <h3 class="card-title font-weight-bolder text-dark">Quick Links</h3>
                                                <div class="card-toolbar">
                                                </div>
                                            </div>
                                            <!--end::Header-->
                                            <!--begin::Body-->
                                            <div class="card-body pt-0">
                                                <!--begin::Item-->
                                                <div id="divDeepCrawlPartnerBillingTextArea" runat="server" visible="false">
                                                    <p>
                                                        <strong>
                                                            <asp:Label ID="LblDeepCrawlPartnerBillingTitle" Text="DeepCrawl Partner Billing" runat="server" />
                                                        </strong>
                                                    </p>
                                                    <p>
                                                        <asp:Label ID="LblDeepCrawlPartnerBillingText" Text="Use the link below to manage DeepCrawl subscriptions for your clients" runat="server" />
                                                    </p>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div id="divDeepCrawlPartnerBilling" runat="server" visible="false" class="d-flex align-items-center mb-9 bg-primary rounded p-5">
                                                    <!--begin::Icon-->
                                                    <span class="svg-icon svg-icon-warning mr-5">
                                                        <span class="flaticon-browser"></span>
                                                    </span>
                                                    <!--end::Icon-->
                                                    <!--begin::Title-->
                                                    <div class="d-flex flex-column flex-grow-1 mr-2">
                                                        <a id="aDeepCrawlPartnerBilling" runat="server" class="font-weight-bold text-dark-75 text-hover-primary font-size-lg mb-1">
                                                            <asp:Label ID="Label14" Text="Partner Billing Page" runat="server" />
                                                            <span class="svg-icon svg-icon-lg svg-icon-success">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Navigation/Arrow-right.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <polygon points="0 0 24 0 24 24 0 24"></polygon>
                                                                        <rect fill="#000000" opacity="0.3" transform="translate(12.000000, 12.000000) rotate(-90.000000) translate(-12.000000, -12.000000)" x="11" y="5" width="2" height="14" rx="1"></rect>
                                                                        <path d="M9.70710318,15.7071045 C9.31657888,16.0976288 8.68341391,16.0976288 8.29288961,15.7071045 C7.90236532,15.3165802 7.90236532,14.6834152 8.29288961,14.2928909 L14.2928896,8.29289093 C14.6714686,7.914312 15.281055,7.90106637 15.675721,8.26284357 L21.675721,13.7628436 C22.08284,14.136036 22.1103429,14.7686034 21.7371505,15.1757223 C21.3639581,15.5828413 20.7313908,15.6103443 20.3242718,15.2371519 L15.0300721,10.3841355 L9.70710318,15.7071045 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.999999, 11.999997) scale(1, -1) rotate(90.000000) translate(-14.999999, -11.999997)"></path>
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                        </a>

                                                    </div>
                                                    <!--end::Title-->
                                                    <!--begin::Lable-->

                                                    <!--end::Lable-->
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div id="divPartnerPortal" runat="server" class="d-flex align-items-center mb-9 bg-primary bg-hover-light-primary rounded p-5">
                                                    <!--begin::Icon-->
                                                    <span class="svg-icon svg-icon-warning mr-5">
                                                        <span class="flaticon-browser"></span>
                                                    </span>
                                                    <!--end::Icon-->
                                                    <!--begin::Title-->
                                                    <div class="d-flex flex-column flex-grow-1 mr-2">
                                                        <a id="aPartnerPortalLogin" onserverclick="aPartnerPortalLogin_ServerClick" runat="server" class="font-weight-bold text-75 text-white text-hover-dark font-size-lg mb-1">
                                                            <asp:Label ID="LblSignUpText" Text="Portal Log In" runat="server" />
                                                            <span class="svg-icon svg-icon-lg svg-icon-white">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Navigation/Arrow-right.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <polygon points="0 0 24 0 24 24 0 24"></polygon>
                                                                        <rect fill="#000000" opacity="0.3" transform="translate(12.000000, 12.000000) rotate(-90.000000) translate(-12.000000, -12.000000)" x="11" y="5" width="2" height="14" rx="1"></rect>
                                                                        <path d="M9.70710318,15.7071045 C9.31657888,16.0976288 8.68341391,16.0976288 8.29288961,15.7071045 C7.90236532,15.3165802 7.90236532,14.6834152 8.29288961,14.2928909 L14.2928896,8.29289093 C14.6714686,7.914312 15.281055,7.90106637 15.675721,8.26284357 L21.675721,13.7628436 C22.08284,14.136036 22.1103429,14.7686034 21.7371505,15.1757223 C21.3639581,15.5828413 20.7313908,15.6103443 20.3242718,15.2371519 L15.0300721,10.3841355 L9.70710318,15.7071045 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.999999, 11.999997) scale(1, -1) rotate(90.000000) translate(-14.999999, -11.999997)"></path>
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                        </a>

                                                    </div>
                                                    <!--end::Title-->
                                                    <!--begin::Lable-->

                                                    <!--end::Lable-->
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div id="divPartnerLocator" runat="server" class="d-flex align-items-center bg-primary bg-hover-light-primary rounded p-5 mb-9">
                                                    <!--begin::Icon-->
                                                    <span class="svg-icon svg-icon-success mr-5">
                                                        <span class="flaticon2-position"></span>
                                                    </span>
                                                    <!--end::Icon-->
                                                    <!--begin::Title-->
                                                    <div class="d-flex flex-column flex-grow-1 mr-2">
                                                        <a id="aLocator" onserverclick="aLocator_ServerClick" runat="server" class="font-weight-bold text-75 text-white text-hover-dark font-size-lg mb-1">
                                                            <asp:Label ID="LblLocatorBtn" Text="Partner Locator" runat="server" />

                                                            <span class="svg-icon svg-icon-lg svg-icon-white">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Navigation/Arrow-right.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <polygon points="0 0 24 0 24 24 0 24"></polygon>
                                                                        <rect fill="#000000" opacity="0.3" transform="translate(12.000000, 12.000000) rotate(-90.000000) translate(-12.000000, -12.000000)" x="11" y="5" width="2" height="14" rx="1"></rect>
                                                                        <path d="M9.70710318,15.7071045 C9.31657888,16.0976288 8.68341391,16.0976288 8.29288961,15.7071045 C7.90236532,15.3165802 7.90236532,14.6834152 8.29288961,14.2928909 L14.2928896,8.29289093 C14.6714686,7.914312 15.281055,7.90106637 15.675721,8.26284357 L21.675721,13.7628436 C22.08284,14.136036 22.1103429,14.7686034 21.7371505,15.1757223 C21.3639581,15.5828413 20.7313908,15.6103443 20.3242718,15.2371519 L15.0300721,10.3841355 L9.70710318,15.7071045 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.999999, 11.999997) scale(1, -1) rotate(90.000000) translate(-14.999999, -11.999997)"></path>
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span></a>
                                                    </div>
                                                    <!--end::Title-->
                                                    <!--begin::Lable-->

                                                    <!--end::Lable-->
                                                </div>
                                                <!--end::Item-->
                                            </div>
                                            <!--end::Body-->
                                        </div>
                                        <!--end::List Widget 11-->
                                        <!--begin::List Widget 3-->
                                        <div class="card card-custom card-stretch gutter-b">
                                            <!--begin::Header-->
                                            <div class="card-header border-0">
                                                <h3 class="card-title font-weight-bolder text-dark">Team</h3>
                                                <div class="card-toolbar">
                                                    <a id="aAddTeam" runat="server" class="btn btn-primary btn-sm font-weight-bolder">Add
                                                    </a>
                                                </div>
                                            </div>
                                            <!--end::Header-->
                                            <!--begin::Body-->
                                            <div class="card-body pt-2">
                                                <!--begin::Item-->
                                                <controls:UcTeamsGridControl ID="ucTeamsGridControl" runat="server" />
                                                <!--end::Item-->
                                            </div>
                                            <!--end::Body-->
                                        </div>
                                        <!--end::List Widget 3-->
                                    </div>
                                    <!--end::Aside Secondary Content-->
                                </div>
                                <!--end::Aside Secondary-->
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="aPartnerPortalLogin" />
                    <asp:PostBackTrigger ControlID="aLocator" />
                </Triggers>
            </asp:UpdatePanel>
            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->
    
    <!-- Payment form (modal view) -->
    <div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <controls:UcStripe ID="UcStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Self Service Modal -->
    <div id="ModalSelfService" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel8">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="Label6" Text="Self Service Plan" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group">
                                    <div id="divMessageAlertConfirmation" runat="server">
                                        <asp:Label ID="LblOfferContent" Text="Redeem now our exclusive <b>66% discount</b> offer and unlock all the data from resellers of your choice. Subscribe below" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-light-primary">Close</button>
                            <a id="aSelfServicePlan" class="btn btn-primary" runat="server">
                                <asp:Label ID="LblSelfServicePlan" Text="Subscribe" runat="server" />
                            </a>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">

            function OpenSelfServiceModal() {
                $('#ModalSelfService').modal('show');
            }

        </script>
    </telerik:RadScriptBlock>
    
    <!--begin::Page Scripts(used by this page)-->
    <script src="/assets/plugins/custom/kanban/kanban.bundle.js"></script>
    <script src="/assets/js/pages/features/miscellaneous/kanban-board.js"></script>
    <!--end::Page Scripts-->

</asp:Content>
