<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardManagementTierPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardManagementTierPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Modals/AddToTeamForm.ascx" TagName="UcAddToTeam" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--begin::Card-->
                    <div class="card card-custom gutter-b">
                        <div class="card-body">
                            <div class="card card-custom gutter-b example example-compact">
                                <div class="d-flex">
                                    <div class="example example-basic" style="width: 100%;">
                                        <div class="example-preview">
                                            <!--begin: Pic-->
                                            <div class="flex-shrink-0 mr-7 mt-lg-0 mt-3" style="float: left;">
                                                <div class="symbol symbol-50 symbol-lg-120">
                                                    <asp:Image ID="ImgCompanyLogo" runat="server" ImageUrl="/images/no_logo3.jpg" />
                                                </div>
                                                <div class="symbol symbol-50 symbol-lg-120 symbol-primary d-none">
                                                    <span class="font-size-h3 symbol-label font-weight-boldest"></span>
                                                </div>
                                            </div>
                                            <!--end: Pic-->
                                            <!--begin: Info-->
                                            <div class="flex-grow-1">
                                                <!--begin: Title-->
                                                <div class="d-flex align-items-center justify-content-between flex-wrap">
                                                    <div class="mr-3" style="width: 90% !important;">
                                                        <!--begin::Name-->
                                                        <a id="aDealPartnerName" runat="server" class="d-flex align-items-center text-dark text-hover-primary font-size-h5 font-weight-bold mr-3">
                                                            <h3>
                                                                <asp:Label ID="LblDealPartnerName" runat="server" /></h3>
                                                            <i class="flaticon2-correct text-success icon-md ml-2"></i>
                                                        </a>
                                                        <!--end::Name-->
                                                        <!--begin::Contacts-->
                                                        <div class="d-flex flex-wrap my-2">
                                                            <a id="aCountryContent" runat="server" class="text-muted text-hover-primary font-weight-bold mr-lg-8 mr-5 mb-lg-0 mb-2">
                                                                <span class="">
                                                                    <strong>
                                                                        <asp:Label ID="LblCountryPartner" Text="Country:" runat="server" />
                                                                    </strong>
                                                                </span>
                                                                <asp:Label ID="LblCountryContent" Text="" runat="server" />
                                                            </a>
                                                            <a id="aWebsiteContent" runat="server" class="text-muted text-hover-primary font-weight-bold mr-lg-8 mr-5 mb-lg-0 mb-2">
                                                                <span class="">
                                                                    <strong>
                                                                        <asp:Label ID="LblWebsitePartner" Text="Website:" runat="server" />
                                                                    </strong>
                                                                </span>
                                                                <asp:Label ID="LblWebsiteContent" Text="" runat="server" />
                                                            </a>
                                                            <a id="aRegDate" runat="server" class="text-muted text-hover-primary font-weight-bold">
                                                                <span class="">
                                                                    <strong>
                                                                        <asp:Label ID="LblRegDatePartner" Text="Registration Date:" runat="server" />
                                                                    </strong>
                                                                </span>
                                                                <asp:Label ID="LblRegDateContent" Text="" runat="server" />
                                                            </a>
                                                        </div>
                                                        <!--end::Contacts-->
                                                    </div>
                                                    <div class="my-lg-0 my-1">
                                                        <a id="aDeletePartnerAccount" runat="server" onserverclick="aDeletePartnerAccount_ServerClick" class="btn btn-sm btn-danger font-weight-bolder">Delete</a>
                                                    </div>
                                                </div>
                                                <!--end: Title-->
                                                <!--begin: Content-->
                                                <div class="d-flex align-items-center flex-wrap justify-content-between">
                                                    <div class="flex-grow-1 font-weight-bold text-dark-50 py-5 py-lg-2 mr-5" style="width: 100%; margin-bottom: 35px;">
                                                        <h4>
                                                            <asp:Label ID="LblDealPartnerNameType" runat="server" />
                                                        </h4>
                                                        <br />
                                                    </div>
                                                    <div class="d-flex flex-wrap align-items-center py-2">
                                                        <div class="d-flex align-items-center mr-6">
                                                            <div class="mr-6">
                                                                <div class="font-weight-bold mb-2">
                                                                    <strong>
                                                                        <asp:Label ID="Label1" Text="Tier Description:" runat="server" />
                                                                    </strong>
                                                                </div>
                                                                <span class="btn btn-sm btn-text btn-light-primary text-uppercase font-weight-bold">
                                                                    <asp:Label ID="LblTierDescription" Text="Influencer" runat="server" />
                                                                </span>
                                                                <a id="aEditCommon" runat="server" onserverclick="aEditCommon_ServerClick">
                                                                    <i class="flaticon-edit"></i>
                                                                </a>
                                                                <a id="aEdit" visible="false" runat="server" onserverclick="aEdit_ServerClick">
                                                                    <i class="flaticon-edit"></i>
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divTeamMembersArea" visible="false" runat="server" class="d-flex flex-wrap align-items-center py-2">
                                                        <div class="d-flex align-items-center">
                                                            <div class="">
                                                                <div class="font-weight-bold mb-2">
                                                                    <strong>
                                                                        <asp:Label ID="Label14" Text="Your Team Members" runat="server" />
                                                                    </strong>
                                                                </div>
                                                                <div class="font-weight-bold mb-4">
                                                                    <div class="text-center text-center">
                                                                        <div id="divMemberImages" runat="server" visible="false" class="symbol-group symbol-hover justify-content-center">
                                                                            <span class="mr-4">
                                                                                <i class="flaticon-network display-4 text-muted font-weight-bold"></i>
                                                                            </span>
                                                                            <div id="divPic1" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                                <img id="Pic1" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_16.jpg" />
                                                                            </div>
                                                                            <div id="divPic2" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                                <img id="Pic2" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_21.jpg" />
                                                                            </div>
                                                                            <div id="divPic3" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                                <img id="Pic3" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_22.jpg" />
                                                                            </div>
                                                                            <div id="divPic4" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                                <img id="Pic4" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_23.jpg" />
                                                                            </div>
                                                                            <div id="divPic5" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                                <img id="Pic5" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_15.jpg" />
                                                                            </div>
                                                                            <div id="divPic6" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                                <img id="Pic6" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_16.jpg" />
                                                                            </div>
                                                                            <div id="divPic7" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                                <img id="Pic7" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_21.jpg" />
                                                                            </div>
                                                                            <div id="divPic8" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                                <img id="Pic8" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_22.jpg" />
                                                                            </div>
                                                                            <div id="divPic9" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                                <img id="Pic9" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_23.jpg" />
                                                                            </div>
                                                                            <div id="divPic10" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                                <img id="Pic10" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_15.jpg" />
                                                                            </div>
                                                                            <div id="divMorePic" runat="server" visible="false" class="symbol symbol-35 symbol-circle symbol-light-success" data-toggle="tooltip" title="there are more members">
                                                                                <span class="symbol-label font-weight-bold">10+</span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <span class="btn btn-sm btn-text"></span>

                                                                <div class="font-weight-bold mb-2">
                                                                    <strong>
                                                                        <asp:Label ID="Label15" Text="Assigned Partner" runat="server" />
                                                                    </strong>
                                                                </div>
                                                                <div id="divAssignPartner" runat="server" class="symbol-group symbol-hover justify-content-center">
                                                                    <div id="divMyAssignPartner" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                        <img id="ImgMyAssignPartner" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_16.jpg" />
                                                                    </div>
                                                                    <asp:Label ID="LblAssignedPartnerFullName" Text="Not Assigned to Partner" runat="server" />
                                                                </div>
                                                                <div class="font-weight-bold mt-2">
                                                                    <a id="aAssignPartner" onserverclick="aAssignPartner_ServerClick" runat="server" class="btn btn-sm btn-success font-weight-bolder">Account manager</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end: Content-->
                                            </div>
                                            <!--end: Info-->
                                            <div class="separator separator-solid"></div>

                                            <div class="d-flex align-items-center flex-wrap mt-8">
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center flex-lg-fill mr-5 mb-2">
                                                    <span class="mr-4">
                                                        <i class="flaticon-coins display-4 text-muted font-weight-bold"></i>
                                                    </span>
                                                    <div class="d-flex flex-column text-dark-75">
                                                        <span class="font-weight-bolder font-size-sm">Total Revenues</span>
                                                        <span class="font-weight-bolder font-size-h5">
                                                            <span class="text-dark-50 font-weight-bold">
                                                                <asp:Label ID="LblVendorTotalRevenuesCurrencySymbol" runat="server" Text="$" />
                                                            </span>
                                                            <asp:Label ID="LblVendorTotalRevenuesValue" Text="0.00" runat="server" />
                                                        </span>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center flex-lg-fill mr-5 mb-2">
                                                    <span class="mr-4">
                                                        <i class="flaticon-rocket display-4 text-muted font-weight-bold"></i>
                                                    </span>
                                                    <div class="d-flex flex-column text-dark-75">
                                                        <span class="font-weight-bolder font-size-sm">Current Goal</span>
                                                        <span class="font-weight-bolder font-size-h5">
                                                            <span class="text-dark-50 font-weight-bold">
                                                                <asp:Label ID="LblVendorCurrentGoalCurrencySymbol" runat="server" Text="$" />
                                                            </span>
                                                            <asp:Label ID="LblVendorCurrentGoalValue" Text="0.00" runat="server" />
                                                        </span>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        
                        <div class="card-header card-header-tabs-line">
                            <div class="card-title">
                                <h3 class="card-label"></h3>
                            </div>
                            <div class="card-toolbar">
                                <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                    <li id="liActivity" runat="server" class="nav-item">
                                        <a id="aActivity" runat="server" onserverclick="aActivity_ServerClick" class="nav-link active">
                                            <span class="nav-icon"><i class="flaticon2-chat-1"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblActivity" Text="Activity" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                    <li id="liAnalytics" runat="server" class="nav-item">
                                        <a id="aAnalytics" runat="server" onserverclick="aAnalytics_ServerClick" class="nav-link">
                                            <span class="nav-icon"><i class="flaticon2-drop"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblAnalytics" Text="Analytics" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                    <li id="liGoals" runat="server" class="nav-item">
                                        <a id="aGoals" runat="server" onserverclick="aGoals_ServerClick" class="nav-link">
                                            <span class="nav-icon"><i class="flaticon2-gear"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblGoals" Text="Goals" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                    <li id="liTeam" runat="server" class="nav-item">
                                        <a id="aTeam" runat="server" onserverclick="aTeam_ServerClick" class="nav-link">
                                            <span class="nav-icon"><i class="flaticon2-gear"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblTeam" Text="Team" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                    <li id="liReporting" runat="server" class="nav-item">
                                        <a id="aReporting" runat="server" onserverclick="aReporting_ServerClick" class="nav-link">
                                            <span class="nav-icon"><i class="flaticon2-gear"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblReporting" Text="Reporting" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="card-body">
                                                <!--begin::Example-->
                                                <div class="example example-basic">
                                                    <div class="example-preview">
                                                        <!--begin::Timeline-->
                                                        <div class="timeline timeline-2">
                                                            <div class="timeline-bar"></div>
                                                            <div class="timeline-item" style="display: none;">
                                                                <div class="timeline-badge"></div>
                                                                <div class="timeline-content d-flex align-items-center justify-content-between">
                                                                    <span class="mr-3">
                                                                        <a href="#">12 new users registered and pending for activation</a>
                                                                        <span class="label label-light-success font-weight-bolder">8</span>
                                                                    </span>
                                                                    <span class="label label-inline label-light-primary font-weight-bolder">new</span>
                                                                    <span class="label label-inline label-light-danger font-weight-bolder">hot</span></span>
                                                                    <span class="text-muted text-right">3 hrs ago</span>
                                                                </div>
                                                            </div>
                                                            <div class="timeline-item">
                                                                <span class="timeline-badge bg-success"></span>
                                                                <div class="timeline-content d-flex align-items-left justify-content-between">
                                                                    <span class="mr-3">Last login.																
                                                                            <span class=""></span>
                                                                        <span class=""></span></span>
                                                                    <span class="text-muted font-italic text-right">2 hrs ago</span>
                                                                </div>
                                                            </div>
                                                            <div class="timeline-item">
                                                                <span class="timeline-badge bg-success"></span>
                                                                <div class="timeline-content d-flex align-items-left justify-content-between">
                                                                    <span class="mr-3">
                                                                        <span class="label label-light-success font-weight-bolder">3</span>
                                                                        new Registration Deals.																
                                                                            <span class=""></span>
                                                                        <span class=""></span></span>
                                                                    <span class="text-muted font-italic text-right">2 days ago</span>
                                                                </div>
                                                            </div>
                                                            <div class="timeline-item">
                                                                <span class="timeline-badge bg-success"></span>
                                                                <div class="timeline-content d-flex align-items-center justify-content-between">

                                                                    <span class="mr-3">
                                                                        <span class="label label-light-success font-weight-bolder">1</span>
                                                                        new Registration Deals.																
                                                                            <span class=""></span>
                                                                        <span class=""></span></span>
                                                                    <span class="text-muted font-italic text-right">1 days ago</span>
                                                                </div>
                                                            </div>
                                                            <div class="timeline-item">
                                                                <span class="timeline-badge"></span>
                                                                <div class="timeline-content d-flex align-items-center justify-content-between">

                                                                    <span class="mr-3">
                                                                        <span class="label label-light-success font-weight-bolder">1</span>
                                                                        Viewed Document on Onboarding Library.</span>
                                                                    <span class="text-muted text-right">7 days ago</span>
                                                                </div>
                                                            </div>
                                                            <div class="timeline-item">
                                                                <span class="timeline-badge bg-danger"></span>
                                                                <div class="timeline-content d-flex align-items-center justify-content-between">

                                                                    <span class="mr-3">
                                                                        <span class="label label-light-success font-weight-bolder">1</span>
                                                                        new Lead Destribution.																
                                                                            <span class="label label-inline label-danger font-weight-bolder">pending</span></span>
                                                                    <span class="text-muted text-right">3 days ago</span>
                                                                </div>
                                                            </div>
                                                            <div class="timeline-item">
                                                                <span class="timeline-badge bg-warning"></span>
                                                                <div class="timeline-content d-flex align-items-center justify-content-between">

                                                                    <span class="mr-3">
                                                                        <span class="label label-light-success font-weight-bolder">5</span>
                                                                        Closed Leads Destribution.</span>
                                                                    <span class="text-muted font-italic text-right">1 month ago</span>
                                                                </div>
                                                            </div>
                                                            <div class="timeline-item">
                                                                <span class="timeline-badge bg-success"></span>
                                                                <div class="timeline-content d-flex align-items-center justify-content-between">

                                                                    <span class="mr-3">
                                                                        <span class="label label-light-success font-weight-bolder">8</span>
                                                                        Sent messages.</span>
                                                                    <span class="text-muted text-right">this month</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end::Timeline-->
                                                    </div>
                                                </div>
                                                <!--end::Example-->
                                            </div>

                                            <controls:MessageControl ID="UcMessageControlActivity" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane fade" id="tab_1_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_2_3">
                                    <!--begin::Row-->
                                    <div class="row">
                                        <div class="col-xl-4">
                                            <!--begin::Block-->
                                            <div class="bg-light-warning p-8 rounded-xl flex-grow-1">
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon-network"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="LblTotalRevenuesTitle" Text="Total Revenues" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblTotalRevenues" Text="0" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanTotalRevenues" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon-list-1"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="Label10" Text="Deals Won / Lost" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblTotalDealWonLost" Text="0" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanTotalDealWonLost" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon2-percentage"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="Label12" Text="Deals Won %" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblDealWinRate" Text="100/100" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanDealWinRate" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon-coins"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="Label9" Text="Total Deals Size" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblTotalDealSize" Text="0" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanTotalDealSize" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="col-md-8">
                                                        <div class="flex-row-fluid mb-7">
                                                            <span id="spanDealsSizeAverage" runat="server" class="d-block font-weight-bold mb-4">
                                                                <asp:Label ID="LblDealsSizeAverageTitle" Text="Average Deal Size" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-weight-bolder">
                                                                <asp:Label ID="LblDealsSizeAverage" Text="0 Days" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanDealsSizeAverageProgress" runat="server" class="progress-bar bg-warning" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->

                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon-coins"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="Label16" Text="Total Leads Size" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblTotalLeadSize" Text="0" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanTotalLeadSize" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon-interface-5"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="Label7" Text="Leads Won / Lost" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblTotalLeadsWonLost" Text="0" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanTotalLeadsWonLost" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon-interface-5"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="Label18" Text="Leads Closed" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblLeadsWinRate" Text="0" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanLeadsWinRate" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="col-md-8">
                                                        <div class="flex-row-fluid mb-7">
                                                            <span id="spanDealsSizeAverageWonDays" runat="server" class="d-block font-weight-bold mb-4">
                                                                <asp:Label ID="LblOpenOpportunitiesTitle" Text="Open Opportunities" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-weight-bolder">
                                                                <asp:Label ID="LblOpenOpportunities" Text="0 Days" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                            </div>
                                            <!--end::Item-->
                                            <!--end::Block-->
                                        </div>
                                        <div class="col-xl-8">

                                            <telerik:RadHtmlChart runat="server" ID="ChrtTotalStatistics" Height="300px" Width="100%">
                                                <ChartTitle Text="Total Revenues">
                                                </ChartTitle>
                                                <PlotArea>
                                                    <Series>
                                                        <telerik:LineSeries DataFieldY="amount">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#3699FF" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                            <MarkersAppearance MarkersType="Circle" BackgroundColor="#3699FF" />
                                                            <TooltipsAppearance BackgroundColor="#3699FF" Color="White" DataFormatString="{0}" />
                                                        </telerik:LineSeries>
                                                        <telerik:AreaSeries DataFieldY="amount">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#FFF4DE" />
                                                                <Overlay Gradient="SharpBevel" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                            <MarkersAppearance MarkersType="Circle" BackgroundColor="#6993ff" />
                                                            <TooltipsAppearance BackgroundColor="#8950fc" Color="White" />
                                                        </telerik:AreaSeries>
                                                    </Series>
                                                    <XAxis MinValue="0" BaseUnit="Months" DashType="Dash" Color="#d0d0d5" MajorTickType="Outside" MinorTickType="Outside">
                                                        <MajorGridLines Color="#8950fc" Width="0" />
                                                        <MinorGridLines Color="#fff" Width="0" />
                                                        <Items>
                                                            <telerik:AxisItem LabelText="Jan" />
                                                            <telerik:AxisItem LabelText="Febr" />
                                                            <telerik:AxisItem LabelText="Mar" />
                                                            <telerik:AxisItem LabelText="Apr" />
                                                            <telerik:AxisItem LabelText="May" />
                                                            <telerik:AxisItem LabelText="Jun" />
                                                            <telerik:AxisItem LabelText="Jul" />
                                                            <telerik:AxisItem LabelText="Aug" />
                                                            <telerik:AxisItem LabelText="Sept" />
                                                            <telerik:AxisItem LabelText="Oct" />
                                                            <telerik:AxisItem LabelText="Nov" />
                                                            <telerik:AxisItem LabelText="Dec" />
                                                        </Items>
                                                        <TitleAppearance Position="Center" />
                                                    </XAxis>
                                                    <YAxis Color="#d0d0d5" Width="1" DashType="Dot">
                                                        <MajorGridLines Color="#e5eaee" DashType="Dot" Width="1" />
                                                        <MinorGridLines Color="#e5eaee" Width="0" />
                                                        <TitleAppearance Position="Center" Text="" />
                                                    </YAxis>
                                                </PlotArea>
                                                <Legend>
                                                    <Appearance Position="Bottom" BackgroundColor="Transparent" />
                                                </Legend>
                                            </telerik:RadHtmlChart>

                                            <div class="row">
                                                <div class="col-md-10">
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DrpTotalRevenues" AutoPostBack="true" OnSelectedIndexChanged="DrpTotalRevenues_SelectedIndexChanged" Width="100" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>

                                            <telerik:RadHtmlChart runat="server" ID="ChrtStatisticsByMonth" Height="300px" Width="100%">
                                                <ChartTitle Text="">
                                                </ChartTitle>
                                                <PlotArea>
                                                    <Series>
                                                        <telerik:LineSeries DataFieldY="amount">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#3699FF" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                            <MarkersAppearance MarkersType="Circle" BackgroundColor="#3699FF" />
                                                            <TooltipsAppearance BackgroundColor="#3699FF" Color="White" DataFormatString="{0}" />
                                                        </telerik:LineSeries>
                                                        <telerik:AreaSeries DataFieldY="amount">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#FFF4DE" />
                                                                <Overlay Gradient="SharpBevel" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                            <MarkersAppearance MarkersType="Circle" BackgroundColor="#6993ff" />
                                                            <TooltipsAppearance BackgroundColor="#8950fc" Color="White" />
                                                        </telerik:AreaSeries>
                                                    </Series>
                                                    <XAxis MinValue="0" BaseUnit="Months" DashType="Dash" Color="#d0d0d5" MajorTickType="Outside" MinorTickType="Outside">
                                                        <MajorGridLines Color="#8950fc" Width="0" />
                                                        <MinorGridLines Color="#fff" Width="0" />
                                                        <Items>
                                                            <telerik:AxisItem LabelText="Jan" />
                                                            <telerik:AxisItem LabelText="Febr" />
                                                            <telerik:AxisItem LabelText="Mar" />
                                                            <telerik:AxisItem LabelText="Apr" />
                                                            <telerik:AxisItem LabelText="May" />
                                                            <telerik:AxisItem LabelText="Jun" />
                                                            <telerik:AxisItem LabelText="Jul" />
                                                            <telerik:AxisItem LabelText="Aug" />
                                                            <telerik:AxisItem LabelText="Sept" />
                                                            <telerik:AxisItem LabelText="Oct" />
                                                            <telerik:AxisItem LabelText="Nov" />
                                                            <telerik:AxisItem LabelText="Dec" />
                                                        </Items>
                                                        <TitleAppearance Position="Center" />
                                                    </XAxis>
                                                    <YAxis Color="#d0d0d5" Width="1" DashType="Dot">
                                                        <MajorGridLines Color="#e5eaee" DashType="Dot" Width="1" />
                                                        <MinorGridLines Color="#e5eaee" Width="0" />
                                                        <TitleAppearance Position="Center" Text="" />
                                                    </YAxis>
                                                </PlotArea>
                                                <Legend>
                                                    <Appearance Position="Bottom" BackgroundColor="Transparent" />
                                                </Legend>
                                            </telerik:RadHtmlChart>

                                            <div class="row">
                                                <div class="col-md-10">
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DrpRevenuesByMonth" AutoPostBack="true" OnSelectedIndexChanged="DrpRevenuesByMonth_SelectedIndexChanged" Width="100" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 30px;">
                                                <div class="col-4">
                                                </div>
                                                <div class="col-4">
                                                    <a id="aViewStatisticsByPartners" runat="server" class="btn btn-light-warning">
                                                        <asp:Label ID="LblViewStatisticsByPartners" Text="View Statistics by Partner" runat="server" />
                                                    </a>
                                                </div>
                                                <div class="col-4"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end::Row-->
                                    <!--begin::Row-->
                                    <div class="row" style="margin-top: 20px;">
                                        <div id="divDealsChart" runat="server" class="col-xl-6">
                                            <!--begin::Charts Widget 1-->
                                            <div class="card card-custom card-stretch gutter-b">
                                                <!--begin::Header-->
                                                <div class="card-header h-auto border-0">
                                                    <!--begin::Title-->
                                                    <div class="card-title py-5">
                                                        <h3 class="card-label">
                                                            <span class="d-block text-dark font-weight-bolder">Deals Registered/Closed</span>
                                                            <span class="d-block text-muted mt-2 font-size-sm"></span>
                                                        </h3>
                                                    </div>
                                                    <!--end::Title-->
                                                    <!--begin::Toolbar-->
                                                    <div class="card-toolbar">
                                                        <asp:DropDownList ID="DrpRegisteredDeals" AutoPostBack="true" OnSelectedIndexChanged="DrpRegisteredDeals_SelectedIndexChanged" Width="100" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <!--end::Toolbar-->
                                                </div>
                                                <!--end::Header-->
                                                <!--begin::Body-->
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-xl-12">
                                                            <!--begin::Chart-->
                                                            <telerik:RadHtmlChart runat="server" ID="ChrtRegisteredDeals" BackColor="White" Height="300px">
                                                                <PlotArea>
                                                                    <Series>
                                                                        <telerik:ColumnSeries DataFieldY="count" Gap="2" Spacing="0.5" Name="Number of Deals Registered">
                                                                            <TooltipsAppearance Color="White" />
                                                                            <Appearance>
                                                                                <FillStyle BackgroundColor="#1bc5bd" />
                                                                            </Appearance>
                                                                            <LabelsAppearance Visible="false" />
                                                                        </telerik:ColumnSeries>
                                                                        <telerik:ColumnSeries DataFieldY="count_by_status" Gap="2" Spacing="0.5" Name="Number of Deals Closed">
                                                                            <TooltipsAppearance Color="White" />
                                                                            <Appearance>
                                                                                <FillStyle BackgroundColor="#e5eaee" />
                                                                            </Appearance>
                                                                            <LabelsAppearance Visible="false" />
                                                                        </telerik:ColumnSeries>
                                                                    </Series>
                                                                    <XAxis DataLabelsField="month" DashType="Dash" Width="1">
                                                                        <MajorGridLines Color="#e5eaee" Width="0" />
                                                                        <MinorGridLines Color="#fff" Width="0" />
                                                                        <TitleAppearance Position="Center" />
                                                                        <Items>
                                                                            <telerik:AxisItem LabelText="Jan" />
                                                                            <telerik:AxisItem LabelText="Febr" />
                                                                            <telerik:AxisItem LabelText="Mar" />
                                                                            <telerik:AxisItem LabelText="Apr" />
                                                                            <telerik:AxisItem LabelText="May" />
                                                                            <telerik:AxisItem LabelText="Jun" />
                                                                            <telerik:AxisItem LabelText="Jul" />
                                                                            <telerik:AxisItem LabelText="Aug" />
                                                                            <telerik:AxisItem LabelText="Sept" />
                                                                            <telerik:AxisItem LabelText="Oct" />
                                                                            <telerik:AxisItem LabelText="Nov" />
                                                                            <telerik:AxisItem LabelText="Dec" />
                                                                        </Items>
                                                                    </XAxis>
                                                                    <YAxis Step="1" DashType="Dash" Width="0">
                                                                        <MajorGridLines Color="#e5eaee" DashType="Dash" Width="1" />
                                                                        <MinorGridLines Color="#e5eaee" Width="0" />
                                                                        <TitleAppearance Position="Center" Text="" />
                                                                    </YAxis>
                                                                </PlotArea>
                                                                <Legend>
                                                                    <Appearance Position="Bottom" />
                                                                </Legend>
                                                            </telerik:RadHtmlChart>
                                                            <!--end::Chart-->
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Body-->
                                            </div>
                                            <!--end::Charts Widget 1-->
                                        </div>
                                        <div id="divLeadsChart" runat="server" class="col-xl-6">
                                            <!--begin::Charts Widget 2-->
                                            <div class="card card-custom bg-gray-100 card-stretch gutter-b">
                                                <!--begin::Header-->
                                                <div class="card-header h-auto border-0">
                                                    <!--begin::Title-->
                                                    <div class="card-title py-5">
                                                        <h3 class="card-label">
                                                            <span class="d-block text-dark font-weight-bolder">Leads Registered/Closed</span>
                                                            <span class="d-block text-dark-50 mt-2 font-size-sm"></span>
                                                        </h3>
                                                    </div>
                                                    <!--end::Title-->
                                                    <!--begin::Toolbar-->
                                                    <div class="card-toolbar">
                                                        <asp:DropDownList ID="DrpRegisteredLeads" AutoPostBack="true" OnSelectedIndexChanged="DrpRegisteredLeads_SelectedIndexChanged" Width="100" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <!--end::Toolbar-->
                                                </div>
                                                <!--end::Header-->
                                                <!--begin::Body-->
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-xl-12">
                                                            <!--begin::Chart-->
                                                            <telerik:RadHtmlChart runat="server" ID="ChrtRegisteredLeads" Height="300px">
                                                                <ChartTitle Text="">
                                                                </ChartTitle>
                                                                <PlotArea>
                                                                    <Series>
                                                                        <telerik:ColumnSeries DataFieldY="count" Gap="2" Spacing="0.5" Name="Number of Leads Registered">
                                                                            <TooltipsAppearance Color="White" />
                                                                            <LabelsAppearance Visible="false" />
                                                                            <Appearance>
                                                                                <FillStyle BackgroundColor="#ffa800" />
                                                                            </Appearance>
                                                                        </telerik:ColumnSeries>
                                                                        <telerik:ColumnSeries DataFieldY="count_by_status" Gap="2" Spacing="0.5" Name="Number of Leads Closed">
                                                                            <TooltipsAppearance Color="White" />
                                                                            <LabelsAppearance Visible="false" />
                                                                            <Appearance>
                                                                                <FillStyle BackgroundColor="#e5eaee" />
                                                                            </Appearance>
                                                                        </telerik:ColumnSeries>
                                                                    </Series>
                                                                    <XAxis DataLabelsField="month" DashType="Dash" Width="1">
                                                                        <MajorGridLines Color="#fff" Width="0" />
                                                                        <MinorGridLines Color="#fff" Width="0" />
                                                                        <TitleAppearance Position="Center" />
                                                                        <Items>
                                                                            <telerik:AxisItem LabelText="Jan" />
                                                                            <telerik:AxisItem LabelText="Febr" />
                                                                            <telerik:AxisItem LabelText="Mar" />
                                                                            <telerik:AxisItem LabelText="Apr" />
                                                                            <telerik:AxisItem LabelText="May" />
                                                                            <telerik:AxisItem LabelText="Jun" />
                                                                            <telerik:AxisItem LabelText="Jul" />
                                                                            <telerik:AxisItem LabelText="Aug" />
                                                                            <telerik:AxisItem LabelText="Sept" />
                                                                            <telerik:AxisItem LabelText="Oct" />
                                                                            <telerik:AxisItem LabelText="Nov" />
                                                                            <telerik:AxisItem LabelText="Dec" />
                                                                        </Items>
                                                                    </XAxis>
                                                                    <YAxis Step="1" Width="0">
                                                                        <MajorGridLines Color="#e5eaee" DashType="Dash" Width="1" />
                                                                        <MinorGridLines Color="#e5eaee" Width="0" />
                                                                        <TitleAppearance Position="Center" Text="" />
                                                                    </YAxis>
                                                                </PlotArea>
                                                                <Legend>
                                                                    <Appearance Position="Bottom" />
                                                                </Legend>
                                                            </telerik:RadHtmlChart>
                                                            <!--end::Chart-->
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Body-->
                                            </div>
                                            <!--end::Charts Widget 2-->
                                        </div>
                                    </div>
                                    <!--end::Row-->
                                    <!--begin::Row-->
                                    <div class="row">
                                        <div class="col-md-12">
                                            <controls:MessageControl ID="UcMessageControlAnalytics" runat="server" />
                                        </div>
                                    </div>
                                    <!--end::Row-->
                                </div>

                                <div class="tab-pane fade" id="tab_1_5" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_5_3">
                                    <div class="row">
                                        <div class="row text-center mt-8 ml-4">
                                            <h3>
                                                <asp:Label ID="LblGoalsTitle" runat="server" />
                                            </h3>
                                            <div style="margin-top: -5px; margin-left: 10px;">
                                                <asp:DropDownList ID="DrpGoalsYears" AutoPostBack="true" OnSelectedIndexChanged="DrpGoalsYears_SelectedIndexChanged" Width="100" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="separator separator-solid my-7"></div>
                                    <div class="row">
                                        <div class="col-xl-4">
                                            <telerik:RadHtmlChart runat="server" ID="ChrtGoals" BackColor="White" Height="300px">
                                                <PlotArea>
                                                    <Series>
                                                        <telerik:ColumnSeries DataFieldY="sum_goals" Gap="2" Spacing="0.5" Name="Goals Amount">
                                                            <TooltipsAppearance Color="White" />
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#1bc5bd" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                        </telerik:ColumnSeries>
                                                        <telerik:ColumnSeries AxisName="AdditionalAxis" Name="User Goals Amount" DataFieldY="sum_users_goals" Gap="2" Spacing="0.5">
                                                            <TooltipsAppearance Color="White" />
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#e5eaee" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                        </telerik:ColumnSeries>
                                                    </Series>
                                                    <YAxis Color="#1bc5bd" Width="1">
                                                    </YAxis>
                                                    <AdditionalYAxes>
                                                        <telerik:AxisY Name="AdditionalAxis" Color="#e5eaee" Width="1">
                                                        </telerik:AxisY>
                                                    </AdditionalYAxes>
                                                    <XAxis DataLabelsField="description" DashType="Dash" Width="1">
                                                        <AxisCrossingPoints>
                                                            <telerik:AxisCrossingPoint Value="0" />
                                                            <telerik:AxisCrossingPoint Value="4" />
                                                        </AxisCrossingPoints>
                                                        <MajorGridLines Color="#e5eaee" Width="0" />
                                                        <MinorGridLines Color="#fff" Width="0" />
                                                        <TitleAppearance Position="Center" />
                                                    </XAxis>
                                                    <YAxis DashType="Dash" Width="0">
                                                        <MajorGridLines Color="#e5eaee" DashType="Dash" Width="1" />
                                                        <MinorGridLines Color="#e5eaee" Width="0" />
                                                        <TitleAppearance Position="Center" Text="" />
                                                    </YAxis>
                                                </PlotArea>
                                                <Legend>
                                                    <Appearance Position="Bottom" />
                                                </Legend>
                                            </telerik:RadHtmlChart>
                                        </div>
                                        <div class="col-xl-8 mt-30">
                                            <!--begin: Items-->
                                            <div class="d-flex align-items-center flex-wrap">

                                                <!--begin: Item-->
                                                <div class="d-flex align-items-center flex-lg-fill mr-5 my-1">
                                                    <span class="mr-4"></span>
                                                    <div class="d-flex flex-column text-dark-75">
                                                        <span class="font-weight-bolder font-size-sm">
                                                            <asp:Label ID="LblQ1Descr" Text="Q1 2021:" runat="server" />
                                                            <asp:HiddenField ID="HdnQ1" Value="0" runat="server" />
                                                            <asp:Label ID="LblQ1" Text="$150.000" runat="server" />
                                                            <asp:TextBox ID="TbxQ1" MaxLength="10" TextMode="Number" Width="80" runat="server" Visible="false" />
                                                            <div id="divEdit1" visible="false" runat="server">
                                                                <a id="aEdit1" runat="server" onserverclick="aEdit1_ServerClick">
                                                                    <i class="flaticon-edit"></i>
                                                                </a>
                                                            </div>
                                                            <div id="divSave1" visible="false" runat="server">
                                                                <a id="aSave1" runat="server" onserverclick="aSave1_ServerClick">
                                                                    <i class="fa fa-save"></i>
                                                                </a>
                                                                <a id="aCancel1" runat="server" onserverclick="aCancel1_ServerClick">
                                                                    <i class="flaticon-delete"></i>
                                                                </a>
                                                            </div>
                                                        </span>
                                                        <span class="font-weight-bolder font-size-h5">
                                                            <span class="text-dark-50 font-weight-bold"></span>
                                                            <asp:Label ID="Label11" Text="Result:" runat="server" />
                                                            <asp:Label ID="LblQ1R" Text="$150.000" runat="server" />
                                                            <asp:TextBox ID="TbxQ1R" placeholder="Not Set" MaxLength="10" TextMode="Number" Width="94" runat="server" Visible="false" />
                                                            <div id="divEdit1R" visible="false" runat="server">
                                                                <a id="aEdit1R" runat="server" onserverclick="aEdit1R_ServerClick">
                                                                    <i class="flaticon-edit"></i>
                                                                </a>
                                                            </div>
                                                            <div id="divSave1R" visible="false" runat="server">
                                                                <a id="aSave1R" runat="server" onserverclick="aSave1R_ServerClick">
                                                                    <i class="fa fa-save"></i>
                                                                </a>
                                                                <a id="aCancel1R" runat="server" onserverclick="aCancel1R_ServerClick">
                                                                    <i class="flaticon-delete"></i>
                                                                </a>
                                                            </div>
                                                        </span>
                                                    </div>
                                                </div>
                                                <!--end: Item-->
                                                <!--begin: Item-->
                                                <div class="d-flex align-items-center flex-lg-fill mr-5 my-1">
                                                    <span class="mr-4"></span>
                                                    <div class="d-flex flex-column text-dark-75">
                                                        <span class="font-weight-bolder font-size-sm">
                                                            <asp:Label ID="LblQ2Descr" Text="Q1 2021:" runat="server" />
                                                            <asp:HiddenField ID="HdnQ2" Value="0" runat="server" />
                                                            <asp:Label ID="LblQ2" Text="$150.000" runat="server" />
                                                            <asp:TextBox ID="TbxQ2" placeholder="Not Set" MaxLength="10" TextMode="Number" Width="80" runat="server" Visible="false" />
                                                            <div id="divEdit2" visible="false" runat="server">
                                                                <a id="aEdit2" runat="server" onserverclick="aEdit2_ServerClick">
                                                                    <i class="flaticon-edit"></i>
                                                                </a>
                                                            </div>
                                                            <div id="divSave2" visible="false" runat="server">
                                                                <a id="aSave2" runat="server" onserverclick="aSave2_ServerClick">
                                                                    <i class="fa fa-save"></i>
                                                                </a>
                                                                <a id="aCancel2" runat="server" onserverclick="aCancel2_ServerClick">
                                                                    <i class="flaticon-delete"></i>
                                                                </a>
                                                            </div>
                                                        </span>
                                                        <span class="font-weight-bolder font-size-h5">
                                                            <span class="text-dark-50 font-weight-bold"></span>
                                                            <asp:Label ID="Label4" Text="Result:" runat="server" />
                                                            <asp:Label ID="LblQ2R" Text="$150.000" runat="server" />
                                                            <asp:TextBox ID="TbxQ2R" placeholder="Not Set" MaxLength="10" TextMode="Number" Width="94" runat="server" Visible="false" />
                                                            <div id="divEdit2R" visible="false" runat="server">
                                                                <a id="aEdit2R" runat="server" onserverclick="aEdit2R_ServerClick">
                                                                    <i class="flaticon-edit"></i>
                                                                </a>
                                                            </div>
                                                            <div id="divSave2R" visible="false" runat="server">
                                                                <a id="aSave2R" runat="server" onserverclick="aSave2R_ServerClick">
                                                                    <i class="fa fa-save"></i>
                                                                </a>
                                                                <a id="aCancel2R" runat="server" onserverclick="aCancel2R_ServerClick">
                                                                    <i class="flaticon-delete"></i>
                                                                </a>
                                                            </div>
                                                        </span>
                                                    </div>
                                                </div>
                                                <!--end: Item-->
                                                <!--begin: Item-->
                                                <div class="d-flex align-items-center flex-lg-fill mr-5 my-1">
                                                    <span class="mr-4"></span>
                                                    <div class="d-flex flex-column text-dark-75">
                                                        <span class="font-weight-bolder font-size-sm">
                                                            <asp:Label ID="LblQ3Descr" Text="Q1 2021:" runat="server" />
                                                            <asp:HiddenField ID="HdnQ3" Value="0" runat="server" />
                                                            <asp:Label ID="LblQ3" Text="$150.000" runat="server" />
                                                            <asp:TextBox ID="TbxQ3" placeholder="Not Set" MaxLength="10" TextMode="Number" Width="80" runat="server" Visible="false" />
                                                            <div id="divEdit3" visible="false" runat="server">
                                                                <a id="aEdit3" runat="server" onserverclick="aEdit3_ServerClick">
                                                                    <i class="flaticon-edit"></i>
                                                                </a>
                                                            </div>
                                                            <div id="divSave3" visible="false" runat="server">
                                                                <a id="aSave3" runat="server" onserverclick="aSave3_ServerClick">
                                                                    <i class="fa fa-save"></i>
                                                                </a>
                                                                <a id="aCancel3" runat="server" onserverclick="aCancel3_ServerClick">
                                                                    <i class="flaticon-delete"></i>
                                                                </a>
                                                            </div>
                                                        </span>
                                                        <span class="font-weight-bolder font-size-h5">
                                                            <span class="text-dark-50 font-weight-bold"></span>
                                                            <asp:Label ID="Label6" Text="Result:" runat="server" />
                                                            <asp:Label ID="LblQ3R" Text="$150.000" runat="server" />
                                                            <asp:TextBox ID="TbxQ3R" placeholder="Not Set" MaxLength="10" TextMode="Number" Width="94" runat="server" Visible="false" />
                                                            <div id="divEdit3R" visible="false" runat="server">
                                                                <a id="aEdit3R" runat="server" onserverclick="aEdit3R_ServerClick">
                                                                    <i class="flaticon-edit"></i>
                                                                </a>
                                                            </div>
                                                            <div id="divSave3R" visible="false" runat="server">
                                                                <a id="aSave3R" runat="server" onserverclick="aSave3R_ServerClick">
                                                                    <i class="fa fa-save"></i>
                                                                </a>
                                                                <a id="aCancel3R" runat="server" onserverclick="aCancel3R_ServerClick">
                                                                    <i class="flaticon-delete"></i>
                                                                </a>
                                                            </div>
                                                        </span>
                                                    </div>
                                                </div>
                                                <!--end: Item-->
                                                <!--begin: Item-->
                                                <div class="d-flex align-items-center flex-lg-fill mr-5 my-1">
                                                    <span class="mr-4"></span>
                                                    <div class="d-flex flex-column flex-lg-fill">
                                                        <span class="text-dark-75 font-weight-bolder font-size-sm">
                                                            <asp:Label ID="LblQ4Descr" Text="Q1 2021:" runat="server" />
                                                            <asp:HiddenField ID="HdnQ4" Value="0" runat="server" />
                                                            <asp:Label ID="LblQ4" Text="$150.000" runat="server" />
                                                            <asp:TextBox ID="TbxQ4" placeholder="Not Set" MaxLength="10" TextMode="Number" Width="80" runat="server" Visible="false" />
                                                            <div id="divEdit4" visible="false" runat="server">
                                                                <a id="aEdit4" runat="server" onserverclick="aEdit4_ServerClick">
                                                                    <i class="flaticon-edit"></i>
                                                                </a>
                                                            </div>
                                                            <div id="divSave4" visible="false" runat="server">
                                                                <a id="aSave4" runat="server" onserverclick="aSave4_ServerClick">
                                                                    <i class="fa fa-save"></i>
                                                                </a>
                                                                <a id="aCancel4" runat="server" onserverclick="aCancel4_ServerClick">
                                                                    <i class="flaticon-delete"></i>
                                                                </a>
                                                            </div>
                                                        </span>
                                                        <span class="font-weight-bolder font-size-h5">
                                                            <span class="text-dark-50 font-weight-bold"></span>
                                                            <asp:Label ID="Label8" Text="Result:" runat="server" />
                                                            <asp:Label ID="LblQ4R" Text="$150.000" runat="server" />
                                                            <asp:TextBox ID="TbxQ4R" placeholder="Not Set" MaxLength="10" TextMode="Number" Width="94" runat="server" Visible="false" />
                                                            <div id="divEdit4R" visible="false" runat="server">
                                                                <a id="aEdit4R" runat="server" onserverclick="aEdit4R_ServerClick">
                                                                    <i class="flaticon-edit"></i>
                                                                </a>
                                                            </div>
                                                            <div id="divSave4R" visible="false" runat="server">
                                                                <a id="aSave4R" runat="server" onserverclick="aSave4R_ServerClick">
                                                                    <i class="fa fa-save"></i>
                                                                </a>
                                                                <a id="aCancel4R" runat="server" onserverclick="aCancel4R_ServerClick">
                                                                    <i class="flaticon-delete"></i>
                                                                </a>
                                                            </div>
                                                        </span>
                                                    </div>
                                                </div>
                                                <!--end: Item-->

                                            </div>
                                        </div>

                                        <controls:MessageControl ID="UcMessageControlGoals" runat="server" />

                                    </div>
                                </div>

                                <div class="tab-pane fade" id="tab_1_3" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_3_3">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="card-header" style="width: 100%; margin-bottom: 20px;">
                                                        <div class="row" style="width: 100%; display: inline-block;">
                                                            <div style="float: left;">
                                                                <h3 class="card-title" style="margin-bottom: 0px;">
                                                                    <asp:Label ID="LblTitle" Text="Current Team" runat="server" />
                                                                </h3>
                                                            </div>
                                                            <div style="float: right;">
                                                                <div class="col-lg-12 col-xl-12 mt-5 mt-lg-0">
                                                                    <!--begin::Button-->
                                                                    <a id="aAddTeam" runat="server" onserverclick="aAddTeam_ServerClick" role="button" class="btn btn-primary font-weight-bolder">
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
                                                                        <asp:Label ID="Label13" Text="Invite New User" runat="server" />
                                                                    </a>
                                                                    <!--end::Button-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="width: 100%; display: inline-block;">
                                                            <!--begin::Search Form-->
                                                            <div class="mb-7">
                                                                <div class="row align-items-center">
                                                                    <div class="col-lg-6 col-xl-6">
                                                                        <div class="row align-items-center">
                                                                            <div class="col-md-6 my-2 my-md-0">
                                                                                <div class="input-icon">
                                                                                    <asp:TextBox ID="RtbxTeamMemberName" Width="250" placeholder="Name/Email" CssClass="form-control" runat="server" />
                                                                                    <span>
                                                                                        <i class="flaticon2-search-1 text-muted"></i>
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-6 my-2 my-md-0">
                                                                                <div class="d-flex align-items-center">
                                                                                    <asp:DropDownList ID="DrpRoles" Width="250" CssClass="form-control" runat="server" />

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-3 col-xl-4 mt-5 mt-lg-0">
                                                                        <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!--end::Search Form-->
                                                        </div>
                                                    </div>
                                                    <controls:MessageAlertControl ID="UcMessageAlertTop" runat="server" />
                                                    <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                        <asp:Repeater ID="RdgResults" OnLoad="RdgResults_Load" OnItemDataBound="RdgResults_OnItemDataBound" runat="server">
                                                            <ItemTemplate>
                                                                <!--begin::Row-->
                                                                <div class="row">
                                                                    <!--begin::Col-->
                                                                    <div id="div1" runat="server" class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                                        <!--begin::Card-->
                                                                        <div class="card card-custom gutter-b card-stretch">
                                                                            <div id="divRibbonOK1" runat="server" visible="false" class="card-header ribbon ribbon-top ribbon-ver">
                                                                                <div class="ribbon-target bg-primary" style="top: -2px; right: 20px;">
                                                                                    OK!
                                                                                </div>
                                                                            </div>
                                                                            <div id="divRibbon1" runat="server" visible="false" class="card-header card-header-right ribbon ribbon-clip ribbon-left">
                                                                                <div class="ribbon-target" style="top: 12px;">
                                                                                    <span id="spanRibbon1" runat="server" class="ribbon-inner bg-primary"></span>
                                                                                    <asp:Label ID="LblPendingInfo1" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <!--begin::Body-->
                                                                            <div class="card-body pt-4">
                                                                                <!--begin::Toolbar-->
                                                                                <div class="d-flex justify-content-end pt-4">
                                                                                    <div id="divActions1" runat="server" class="dropdown dropdown-inline">
                                                                                        <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                            <i class="ki ki-bold-more-hor"></i>
                                                                                        </a>
                                                                                        <div class="dropdown-menu dropdown-menu-md dropdown-menu-right">
                                                                                            <!--begin::Navigation-->
                                                                                            <ul class="navi navi-hover">
                                                                                                <li class="navi-header font-weight-bold py-4">
                                                                                                    <span class="font-size-lg">Edit profile</span>
                                                                                                    <i class="flaticon2-information icon-md text-muted" data-toggle="tooltip" data-placement="right" title="Click to learn more..."></i>
                                                                                                </li>
                                                                                                <li class="navi-separator mt-3 opacity-70"></li>
                                                                                                <li class="navi-item">
                                                                                                    <a id="aSettings1" runat="server" class="navi-link">
                                                                                                        <span class="navi-icon">
                                                                                                            <i class="flaticon2-gear"></i>
                                                                                                        </span>
                                                                                                        <span class="navi-text">Settings</span>
                                                                                                    </a>
                                                                                                </li>
                                                                                            </ul>
                                                                                            <!--end::Navigation-->
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <!--end::Toolbar-->
                                                                                <!--begin::User-->
                                                                                <div class="d-flex align-items-end mb-7">
                                                                                    <!--begin::Pic-->
                                                                                    <div class="d-flex align-items-center">
                                                                                        <!--begin::Pic-->
                                                                                        <div class="flex-shrink-0 mr-4 mt-lg-0 mt-3">
                                                                                            <div id="divImg1" runat="server" class="symbol symbol-circle symbol-lg-75">
                                                                                                <asp:Image ID="img1" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "personal_image1")%>' AlternateText="Team sub account personal image" />
                                                                                            </div>
                                                                                            <div id="divImgNo1" runat="server" visible="false" class="symbol symbol-lg-75 symbol-circle symbol-primary">
                                                                                                <span class="symbol-label font-size-h3 font-weight-boldest">NO</span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <!--end::Pic-->
                                                                                        <!--begin::Title-->
                                                                                        <div class="d-flex flex-column">
                                                                                            <a id="aName1" runat="server" class="text-dark font-weight-bold text-hover-primary font-size-h4 mb-0">
                                                                                                <asp:Label ID="LblSubAccountName1" runat="server" />
                                                                                            </a>
                                                                                            <a id="aRole1" runat="server" class="text-muted font-weight-bold">
                                                                                                <%# DataBinder.Eval(Container.DataItem, "team_role_name1")%>
                                                                                            </a>
                                                                                        </div>
                                                                                        <!--end::Title-->
                                                                                    </div>
                                                                                    <!--end::Title-->
                                                                                </div>
                                                                                <!--end::User-->
                                                                                <!--begin::Info-->
                                                                                <div class="mb-7">
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Email:</span>
                                                                                        <div id="divEmailTooltip1" runat="server">
                                                                                            <a href="#" class="text-muted text-hover-primary">
                                                                                                <asp:Label ID="LblEmail1" Text='<%# DataBinder.Eval(Container.DataItem, "email1")%>' runat="server" />
                                                                                            </a>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Role:</span>
                                                                                        <span class="text-muted font-weight-bold">
                                                                                            <%# DataBinder.Eval(Container.DataItem, "team_role_name1")%>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Position:</span>
                                                                                        <span class="text-muted font-weight-bold">
                                                                                            <%# DataBinder.Eval(Container.DataItem, "position1")%>
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                                <!--end::Info-->
                                                                                <a id="aSend1" runat="server" role="button" onserverclick="ImgBtnSendEmail1_OnClick" class="btn btn-block btn-sm btn-light-primary font-weight-bolder text-uppercase py-4">Resend Invitation
                                                                                </a>
                                                                                <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                                <asp:HiddenField ID="HdnConfirmationUrl1" Value='<%# Eval("confirmation_url1") %>' runat="server" />
                                                                            </div>
                                                                            <!--end::Body-->
                                                                        </div>
                                                                        <!--end::Card-->
                                                                    </div>
                                                                    <!--end::Col-->
                                                                    <!--begin::Col-->
                                                                    <div id="div2" runat="server" class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                                        <!--begin::Card-->
                                                                        <div class="card card-custom gutter-b card-stretch">
                                                                            <div id="divRibbonOK2" runat="server" visible="false" class="card-header ribbon ribbon-top ribbon-ver">
                                                                                <div class="ribbon-target bg-primary" style="top: -2px; right: 20px;">
                                                                                    OK!
                                                                                </div>
                                                                            </div>
                                                                            <div id="divRibbon2" runat="server" visible="false" class="card-header card-header-right ribbon ribbon-clip ribbon-left">
                                                                                <div class="ribbon-target" style="top: 12px;">
                                                                                    <span id="spanRibbon2" runat="server" class="ribbon-inner bg-primary"></span>
                                                                                    <asp:Label ID="LblPendingInfo2" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <!--begin::Body-->
                                                                            <div class="card-body pt-4">
                                                                                <!--begin::Toolbar-->
                                                                                <div class="d-flex justify-content-end pt-4">
                                                                                    <div id="divActions2" runat="server" class="dropdown dropdown-inline">
                                                                                        <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                            <i class="ki ki-bold-more-hor"></i>
                                                                                        </a>
                                                                                        <div class="dropdown-menu dropdown-menu-md dropdown-menu-right">
                                                                                            <!--begin::Navigation-->
                                                                                            <ul class="navi navi-hover">
                                                                                                <li class="navi-header font-weight-bold py-4">
                                                                                                    <span class="font-size-lg">Edit profile</span>
                                                                                                    <i class="flaticon2-information icon-md text-muted" data-toggle="tooltip" data-placement="right" title="Click to learn more..."></i>
                                                                                                </li>
                                                                                                <li class="navi-separator mt-3 opacity-70"></li>
                                                                                                <li class="navi-item">
                                                                                                    <a id="aSettings2" runat="server" class="navi-link">
                                                                                                        <span class="navi-icon">
                                                                                                            <i class="flaticon2-gear"></i>
                                                                                                        </span>
                                                                                                        <span class="navi-text">Settings</span>
                                                                                                    </a>
                                                                                                </li>
                                                                                            </ul>
                                                                                            <!--end::Navigation-->
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <!--end::Toolbar-->
                                                                                <!--begin::User-->
                                                                                <div class="d-flex align-items-end mb-7">
                                                                                    <!--begin::Pic-->
                                                                                    <div class="d-flex align-items-center">
                                                                                        <!--begin::Pic-->
                                                                                        <div class="flex-shrink-0 mr-4 mt-lg-0 mt-3">
                                                                                            <div id="divImg2" runat="server" class="symbol symbol-circle symbol-lg-75">
                                                                                                <asp:Image ID="img2" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "personal_image2")%>' AlternateText="Team sub account personal image" />
                                                                                            </div>
                                                                                            <div id="divImgNo2" runat="server" visible="false" class="symbol symbol-lg-75 symbol-circle symbol-primary">
                                                                                                <span class="symbol-label font-size-h3 font-weight-boldest">NO</span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <!--end::Pic-->
                                                                                        <!--begin::Title-->
                                                                                        <div class="d-flex flex-column">
                                                                                            <a id="aName2" runat="server" class="text-dark font-weight-bold text-hover-primary font-size-h4 mb-0">
                                                                                                <asp:Label ID="LblSubAccountName2" runat="server" />
                                                                                            </a>
                                                                                            <a id="aRole2" runat="server" class="text-muted font-weight-bold">
                                                                                                <%# DataBinder.Eval(Container.DataItem, "team_role_name2")%>
                                                                                            </a>
                                                                                        </div>
                                                                                        <!--end::Title-->
                                                                                    </div>
                                                                                    <!--end::Title-->
                                                                                </div>
                                                                                <!--end::User-->
                                                                                <!--begin::Info-->
                                                                                <div class="mb-7">
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Email:</span>
                                                                                        <div id="divEmailTooltip2" runat="server">
                                                                                            <a href="#" class="text-muted text-hover-primary">
                                                                                                <asp:Label ID="LblEmail2" Text='<%# DataBinder.Eval(Container.DataItem, "email2")%>' runat="server" />
                                                                                            </a>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Role:</span>
                                                                                        <span class="text-muted font-weight-bold">
                                                                                            <%# DataBinder.Eval(Container.DataItem, "team_role_name2")%>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Position:</span>
                                                                                        <span class="text-muted font-weight-bold">
                                                                                            <%# DataBinder.Eval(Container.DataItem, "position2")%>
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                                <!--end::Info-->
                                                                                <a id="aSend2" runat="server" role="button" onserverclick="ImgBtnSendEmail2_OnClick" class="btn btn-block btn-sm btn-light-primary font-weight-bolder text-uppercase py-4">Resend Invitation
                                                                                </a>
                                                                                <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                                <asp:HiddenField ID="HdnConfirmationUrl2" Value='<%# Eval("confirmation_url2") %>' runat="server" />
                                                                            </div>
                                                                            <!--end::Body-->
                                                                        </div>
                                                                        <!--end::Card-->
                                                                    </div>
                                                                    <!--end::Col-->
                                                                    <!--begin::Col-->
                                                                    <div id="div3" runat="server" class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                                        <!--begin::Card-->
                                                                        <div class="card card-custom gutter-b card-stretch">
                                                                            <div id="divRibbonOK3" runat="server" visible="false" class="card-header ribbon ribbon-top ribbon-ver">
                                                                                <div class="ribbon-target bg-primary" style="top: -2px; right: 20px;">
                                                                                    OK!
                                                                                </div>
                                                                            </div>
                                                                            <div id="divRibbon3" runat="server" visible="false" class="card-header card-header-right ribbon ribbon-clip ribbon-left">
                                                                                <div class="ribbon-target" style="top: 12px;">
                                                                                    <span id="spanRibbon3" runat="server" class="ribbon-inner bg-primary"></span>
                                                                                    <asp:Label ID="LblPendingInfo3" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <!--begin::Body-->
                                                                            <div class="card-body pt-4">
                                                                                <!--begin::Toolbar-->
                                                                                <div class="d-flex justify-content-end pt-4">
                                                                                    <div id="divActions3" runat="server" class="dropdown dropdown-inline">
                                                                                        <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                            <i class="ki ki-bold-more-hor"></i>
                                                                                        </a>
                                                                                        <div class="dropdown-menu dropdown-menu-md dropdown-menu-right">
                                                                                            <!--begin::Navigation-->
                                                                                            <ul class="navi navi-hover">
                                                                                                <li class="navi-header font-weight-bold py-4">
                                                                                                    <span class="font-size-lg">Edit profile</span>
                                                                                                    <i class="flaticon2-information icon-md text-muted" data-toggle="tooltip" data-placement="right" title="Click to learn more..."></i>
                                                                                                </li>
                                                                                                <li class="navi-separator mt-3 opacity-70"></li>
                                                                                                <li class="navi-item">
                                                                                                    <a id="aSettings3" runat="server" class="navi-link">
                                                                                                        <span class="navi-icon">
                                                                                                            <i class="flaticon2-gear"></i>
                                                                                                        </span>
                                                                                                        <span class="navi-text">Settings</span>
                                                                                                    </a>
                                                                                                </li>
                                                                                            </ul>
                                                                                            <!--end::Navigation-->
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <!--end::Toolbar-->
                                                                                <!--begin::User-->
                                                                                <div class="d-flex align-items-end mb-7">
                                                                                    <!--begin::Pic-->
                                                                                    <div class="d-flex align-items-center">
                                                                                        <!--begin::Pic-->
                                                                                        <div class="flex-shrink-0 mr-4 mt-lg-0 mt-3">
                                                                                            <div id="divImg3" runat="server" class="symbol symbol-circle symbol-lg-75">
                                                                                                <asp:Image ID="img3" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "personal_image3")%>' AlternateText="Team sub account personal image" />
                                                                                            </div>
                                                                                            <div id="divImgNo3" runat="server" visible="false" class="symbol symbol-lg-75 symbol-circle symbol-primary">
                                                                                                <span class="symbol-label font-size-h3 font-weight-boldest">NO</span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <!--end::Pic-->
                                                                                        <!--begin::Title-->
                                                                                        <div class="d-flex flex-column">
                                                                                            <a id="aName3" runat="server" class="text-dark font-weight-bold text-hover-primary font-size-h4 mb-0">
                                                                                                <asp:Label ID="LblSubAccountName3" runat="server" />
                                                                                            </a>
                                                                                            <a id="aRole3" runat="server" class="text-muted font-weight-bold">
                                                                                                <%# DataBinder.Eval(Container.DataItem, "team_role_name3")%>
                                                                                            </a>
                                                                                        </div>
                                                                                        <!--end::Title-->
                                                                                    </div>
                                                                                    <!--end::Title-->
                                                                                </div>
                                                                                <!--end::User-->
                                                                                <!--begin::Info-->
                                                                                <div class="mb-7">
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Email:</span>
                                                                                        <div id="divEmailTooltip3" runat="server">
                                                                                            <a href="#" class="text-muted text-hover-primary">
                                                                                                <asp:Label ID="LblEmail3" Text='<%# DataBinder.Eval(Container.DataItem, "email3")%>' runat="server" />
                                                                                            </a>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Role:</span>
                                                                                        <span class="text-muted font-weight-bold">
                                                                                            <%# DataBinder.Eval(Container.DataItem, "team_role_name3")%>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Position:</span>
                                                                                        <span class="text-muted font-weight-bold">
                                                                                            <%# DataBinder.Eval(Container.DataItem, "position3")%>
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                                <!--end::Info-->
                                                                                <a id="aSend3" runat="server" role="button" onserverclick="ImgBtnSendEmail3_OnClick" class="btn btn-block btn-sm btn-light-primary font-weight-bolder text-uppercase py-4">Resend Invitation
                                                                                </a>
                                                                                <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                                <asp:HiddenField ID="HdnConfirmationUrl3" Value='<%# Eval("confirmation_url3") %>' runat="server" />
                                                                            </div>
                                                                            <!--end::Body-->
                                                                        </div>
                                                                        <!--end::Card-->
                                                                    </div>
                                                                    <!--end::Col-->
                                                                    <!--begin::Col-->
                                                                    <div id="div4" runat="server" class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                                        <!--begin::Card-->
                                                                        <div class="card card-custom gutter-b card-stretch">
                                                                            <div id="divRibbonOK4" runat="server" visible="false" class="card-header ribbon ribbon-top ribbon-ver">
                                                                                <div class="ribbon-target bg-primary" style="top: -2px; right: 20px;">
                                                                                    OK!
                                                                                </div>
                                                                            </div>
                                                                            <div id="divRibbon4" runat="server" visible="false" class="card-header card-header-right ribbon ribbon-clip ribbon-left">
                                                                                <div class="ribbon-target" style="top: 12px;">
                                                                                    <span id="spanRibbon4" runat="server" class="ribbon-inner bg-primary"></span>
                                                                                    <asp:Label ID="LblPendingInfo4" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <!--begin::Body-->
                                                                            <div class="card-body pt-4">
                                                                                <!--begin::Toolbar-->
                                                                                <div class="d-flex justify-content-end pt-4">
                                                                                    <div id="divActions4" runat="server" class="dropdown dropdown-inline">
                                                                                        <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                            <i class="ki ki-bold-more-hor"></i>
                                                                                        </a>
                                                                                        <div class="dropdown-menu dropdown-menu-md dropdown-menu-right">
                                                                                            <!--begin::Navigation-->
                                                                                            <ul class="navi navi-hover">
                                                                                                <li class="navi-header font-weight-bold py-4">
                                                                                                    <span class="font-size-lg">Edit profile</span>
                                                                                                    <i class="flaticon2-information icon-md text-muted" data-toggle="tooltip" data-placement="right" title="Click to learn more..."></i>
                                                                                                </li>
                                                                                                <li class="navi-separator mt-3 opacity-70"></li>
                                                                                                <li class="navi-item">
                                                                                                    <a id="aSettings4" runat="server" class="navi-link">
                                                                                                        <span class="navi-icon">
                                                                                                            <i class="flaticon2-gear"></i>
                                                                                                        </span>
                                                                                                        <span class="navi-text">Settings</span>
                                                                                                    </a>
                                                                                                </li>
                                                                                            </ul>
                                                                                            <!--end::Navigation-->
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <!--end::Toolbar-->
                                                                                <!--begin::User-->
                                                                                <div class="d-flex align-items-end mb-7">
                                                                                    <!--begin::Pic-->
                                                                                    <div class="d-flex align-items-center">
                                                                                        <!--begin::Pic-->
                                                                                        <div class="flex-shrink-0 mr-4 mt-lg-0 mt-3">
                                                                                            <div id="divImg4" runat="server" class="symbol symbol-circle symbol-lg-75">
                                                                                                <asp:Image ID="img4" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "personal_image4")%>' AlternateText="Team sub account personal image" />
                                                                                            </div>
                                                                                            <div id="divImgNo4" runat="server" visible="false" class="symbol symbol-lg-75 symbol-circle symbol-primary">
                                                                                                <span class="symbol-label font-size-h3 font-weight-boldest">NO</span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <!--end::Pic-->
                                                                                        <!--begin::Title-->
                                                                                        <div class="d-flex flex-column">
                                                                                            <a id="aName4" runat="server" class="text-dark font-weight-bold text-hover-primary font-size-h4 mb-0">
                                                                                                <asp:Label ID="LblSubAccountName4" runat="server" />
                                                                                            </a>
                                                                                            <a id="aRole4" runat="server" class="text-muted font-weight-bold">
                                                                                                <%# DataBinder.Eval(Container.DataItem, "team_role_name4")%>
                                                                                            </a>
                                                                                        </div>
                                                                                        <!--end::Title-->
                                                                                    </div>
                                                                                    <!--end::Title-->
                                                                                </div>
                                                                                <!--end::User-->
                                                                                <!--begin::Info-->
                                                                                <div class="mb-7">
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Email:</span>
                                                                                        <div id="divEmailTooltip4" runat="server">
                                                                                            <a href="#" class="text-muted text-hover-primary">
                                                                                                <asp:Label ID="LblEmail4" Text='<%# DataBinder.Eval(Container.DataItem, "email4")%>' runat="server" />
                                                                                            </a>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Role:</span>
                                                                                        <span class="text-muted font-weight-bold">
                                                                                            <%# DataBinder.Eval(Container.DataItem, "team_role_name4")%>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                                        <span class="text-dark-75 font-weight-bolder mr-2">Position:</span>
                                                                                        <span class="text-muted font-weight-bold">
                                                                                            <%# DataBinder.Eval(Container.DataItem, "position4")%>
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                                <!--end::Info-->
                                                                                <a id="aSend4" runat="server" role="button" onserverclick="ImgBtnSendEmail4_OnClick" class="btn btn-block btn-sm btn-light-primary font-weight-bolder text-uppercase py-4">Resend Invitation
                                                                                </a>
                                                                                <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                                <asp:HiddenField ID="HdnConfirmationUrl4" Value='<%# Eval("confirmation_url4") %>' runat="server" />
                                                                            </div>
                                                                            <!--end::Body-->
                                                                        </div>
                                                                        <!--end::Card-->
                                                                    </div>
                                                                    <!--end::Col-->
                                                                </div>
                                                                <!--end::Row-->
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                    <controls:MessageAlertControl ID="UcMessageAlertTeam" runat="server" />
                                                </div>
                                            </div>

                                            <controls:MessageControl ID="UcMessageControlTeam" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane fade" id="tab_1_4" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_4_3">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="card card-custom gutter-b example example-compact">
                                                <div class="card-toolbar" style="padding: 15px;">
                                                    <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                                        <li id="liNewReport" runat="server" class="nav-item">
                                                            <a id="aNewReport" runat="server" onserverclick="aNewReport_ServerClick" class="nav-link active">
                                                                <span class="nav-icon"><i class="flaticon2-chat-1"></i></span>
                                                                <span class="nav-text">
                                                                    <asp:Label ID="LblNewReport" Text="New Report" runat="server" />
                                                                </span>
                                                            </a>
                                                        </li>
                                                        <li id="liReportHistory" runat="server" class="nav-item">
                                                            <a id="aReportHistory" runat="server" onserverclick="aReportHistory_ServerClick" class="nav-link">
                                                                <span class="nav-icon"><i class="flaticon2-drop"></i></span>
                                                                <span class="nav-text">
                                                                    <asp:Label ID="LblReportHistory" Text="History" runat="server" /></span>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="card-body">
                                                    <div class="tab-content">
                                                        <div class="tab-pane fade show active" id="tab_2_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                                            <div class="card-header">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <div class="example-tools justify-content-center">
                                                                        <h3>Top 5 customers</h3>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="form-group row">
                                                                    <label class="col-lg-2 col-form-label text-right">Customer Name:</label>
                                                                    <div class="col-lg-3">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxCustomerName1" CssClass="form-control" placeholder="Enter customer name" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-customer"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <label class="col-lg-2 col-form-label text-right">Industry:</label>
                                                                    <div class="col-lg-4">
                                                                        <div class="input-group">
                                                                            <asp:DropDownList ID="DrpIndustry1" EnableViewState="true" Width="220" runat="server" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="la la-bookmark-o"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-2 col-form-label text-right">Customer Name:</label>
                                                                    <div class="col-lg-3">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxCustomerName2" CssClass="form-control" placeholder="Enter customer name" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-customer"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <label class="col-lg-2 col-form-label text-right">Industry:</label>
                                                                    <div class="col-lg-4">
                                                                        <div class="input-group">
                                                                            <asp:DropDownList ID="DrpIndustry2" EnableViewState="true" Width="220" runat="server" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="la la-bookmark-o"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-2 col-form-label text-right">Customer Name:</label>
                                                                    <div class="col-lg-3">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxCustomerName3" CssClass="form-control" placeholder="Enter customer name" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-customer"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <label class="col-lg-2 col-form-label text-right">Industry:</label>
                                                                    <div class="col-lg-4">
                                                                        <div class="input-group">
                                                                            <asp:DropDownList ID="DrpIndustry3" EnableViewState="true" Width="220" runat="server" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="la la-bookmark-o"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-2 col-form-label text-right">Customer Name:</label>
                                                                    <div class="col-lg-3">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxCustomerName4" CssClass="form-control" placeholder="Enter customer name" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-customer"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <label class="col-lg-2 col-form-label text-right">Industry:</label>
                                                                    <div class="col-lg-4">
                                                                        <div class="input-group">
                                                                            <asp:DropDownList ID="DrpIndustry4" EnableViewState="true" Width="220" runat="server" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="la la-bookmark-o"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-2 col-form-label text-right">Customer Name:</label>
                                                                    <div class="col-lg-3">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxCustomerName5" CssClass="form-control" placeholder="Enter customer name" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-customer"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <label class="col-lg-2 col-form-label text-right">Industry:</label>
                                                                    <div class="col-lg-4">
                                                                        <div class="input-group">
                                                                            <asp:DropDownList ID="DrpIndustry5" EnableViewState="true" Width="220" runat="server" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="la la-bookmark-o"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-header">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <div class="example-tools justify-content-center">
                                                                        <h3>Market size</h3>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="form-group row">
                                                                    <%--<label class="col-lg-3 col-form-label text-right">Total Customers:</label>
                                                                    <div class="col-lg-2">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxTotalCust" TextMode="Number" OnTextChanged="TbxMarketShare_TextChanged" AutoPostBack="true" CssClass="form-control" Width="100" runat="server" />
                                                                            <input id="TbxTotalCus1" class="form-control" visible="false" onchange="Avg()" runat="server" type="text" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-users"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                        <span class="form-text text-muted"></span>
                                                                    </div>--%>
                                                                    <div class="col-lg-2"></div>
                                                                    <label class="col-lg-3 col-form-label text-right">Revenue of customers:</label>
                                                                    <div class="col-lg-3">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxTotalReven" TextMode="Number" OnTextChanged="TbxMarketShareSpend_TextChanged" AutoPostBack="true" CssClass="form-control" Width="100" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <asp:Label ID="LblVendorCurrencySymbol" runat="server" />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                        <span class="form-text text-muted"></span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <%--<label class="col-lg-3 col-form-label text-right">Active customers:</label>
                                                                    <div class="col-lg-2">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxActiveCust" TextMode="Number" OnTextChanged="TbxMarketShare_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-user-ok"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                        <span class="form-text text-muted"></span>
                                                                    </div>--%>
                                                                    <div class="col-lg-2"></div>
                                                                    <label class="col-lg-3 col-form-label text-right">Total spend on you:</label>
                                                                    <div class="col-lg-3">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxTotalSpend" TextMode="Number" OnTextChanged="TbxMarketShareSpend_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-coins"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                        <span class="form-text text-muted"></span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <%--<label class="col-lg-3 col-form-label text-right">Market share:</label>
                                                                    <div class="col-lg-2">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxMarketShare" TextMode="Number" ClientIDMode="Static" Enabled="False" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">%</span>
                                                                            </div>
                                                                        </div>
                                                                        <span class="form-text text-muted"></span>
                                                                    </div>--%>
                                                                    <div class="col-lg-2"></div>
                                                                    <label class="col-lg-3 col-form-label text-right">Market share:</label>
                                                                    <div class="col-lg-3">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxMarketShareSpend" Enabled="false" TextMode="Number" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">%</span>
                                                                            </div>
                                                                        </div>
                                                                        <span class="form-text text-muted"></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-header">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <div class="example-tools justify-content-center">
                                                                        <h3>Employee Capacity</h3>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label"></label>

                                                                    <label class="col-lg-2 col-md-4 col-form-label" style="max-width: 14% !important;">Q1</label>

                                                                    <label class="col-lg-2 col-md-4 col-form-label" style="max-width: 14% !important;">Q2</label>

                                                                    <label class="col-lg-2 col-md-4 col-form-label" style="max-width: 14% !important;">Q3</label>

                                                                    <label class="col-lg-2 col-md-4 col-form-label" style="max-width: 14% !important;">Q4</label>

                                                                    <label class="col-lg-2 col-form-label" style="max-width: 14% !important;">Average</label>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <div class="col-lg-3">
                                                                        <label class="col-lg-10 col-form-label mt-1">Admin</label>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxAdminQ1" OnTextChanged="TbxAdminQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxAdminQ2" OnTextChanged="TbxAdminQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxAdminQ3" OnTextChanged="TbxAdminQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxAdminQ4" OnTextChanged="TbxAdminQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxAdminAvg" Enabled="false" Width="100" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <div class="col-lg-3">
                                                                        <label class="col-lg-10 col-form-label mt-1">Business Development</label>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxBusDevQ1" OnTextChanged="TbxBusDevQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxBusDevQ2" OnTextChanged="TbxBusDevQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxBusDevQ3" OnTextChanged="TbxBusDevQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxBusDevQ4" OnTextChanged="TbxBusDevQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxBusDevAvg" Enabled="false" Width="100" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <div class="col-lg-3">
                                                                        <label class="col-lg-10 col-form-label mt-1">Developers</label>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxDevelQ1" OnTextChanged="TbxDevelQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxDevelQ2" OnTextChanged="TbxDevelQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxDevelQ3" OnTextChanged="TbxDevelQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxDevelQ4" OnTextChanged="TbxDevelQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxDevelAvg" Enabled="false" Width="100" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <div class="col-lg-3">
                                                                        <label class="col-lg-10 col-form-label mt-1">Certified Developers</label>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxCerDevQ1" OnTextChanged="TbxCerDevQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxCerDevQ2" OnTextChanged="TbxCerDevQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxCerDevQ3" OnTextChanged="TbxCerDevQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxCerDevQ4" OnTextChanged="TbxCerDevQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <asp:TextBox ID="TbxCerDevAvg" Enabled="false" Width="100" TextMode="Number" placeholder="0" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-header">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <div class="example-tools justify-content-center">
                                                                        <h3>Sales</h3>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label"></label>

                                                                    <label class="col-lg-2 col-md-4 col-form-label" style="max-width: 14% !important;">Q1</label>

                                                                    <label class="col-lg-2 col-md-4 col-form-label" style="max-width: 14% !important;">Q2</label>

                                                                    <label class="col-lg-2 col-md-4 col-form-label" style="max-width: 14% !important;">Q3</label>

                                                                    <label class="col-lg-2 col-md-4 col-form-label" style="max-width: 14% !important;">Q4</label>

                                                                    <label class="col-lg-2 col-form-label" style="max-width: 14% !important;">Total</label>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <div class="col-lg-3">
                                                                        <label class="col-lg-10 col-form-label mt-1">Planned</label>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxPlannedQ1" OnTextChanged="TbxPlannedQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0.00" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <asp:Label ID="LblPlannedCurrencyQ1" runat="server" />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxPlannedQ2" OnTextChanged="TbxPlannedQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0.00" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <asp:Label ID="LblPlannedCurrencyQ2" runat="server" />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxPlannedQ3" OnTextChanged="TbxPlannedQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0.00" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <asp:Label ID="LblPlannedCurrencyQ3" runat="server" />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxPlannedQ4" OnTextChanged="TbxPlannedQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0.00" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <asp:Label ID="LblPlannedCurrencyQ4" runat="server" />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxPlannedTotal" Enabled="false" Width="100" TextMode="Number" placeholder="0.00" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <asp:Label ID="LblPlannedCurrencyTotal" runat="server" />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <div class="col-lg-3">
                                                                        <label class="col-lg-10 col-form-label mt-1">Actual</label>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxActualQ1" OnTextChanged="TbxActualQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0.00" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-coins"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxActualQ2" OnTextChanged="TbxActualQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0.00" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-coins"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxActualQ3" OnTextChanged="TbxActualQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0.00" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-coins"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxActualQ4" OnTextChanged="TbxActualQ_TextChanged" AutoPostBack="true" Width="80" TextMode="Number" placeholder="0.00" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-coins"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-2" style="max-width: 14% !important;">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxActualTotal" Enabled="false" Width="100" TextMode="Number" placeholder="0.00" CssClass="form-control" runat="server" />
                                                                            <div class="input-group-append">
                                                                                <span class="input-group-text">
                                                                                    <i class="flaticon-coins"></i>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-header">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <div class="example-tools justify-content-center">
                                                                        <h3>Joint Marketing Initiatives</h3>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-md-4 col-form-label text-right">Objectives</label>

                                                                    <label class="col-lg-3 col-md-4 col-form-label text-right">Costs</label>

                                                                    <label class="col-lg-2 col-md-3 col-form-label text-right">When</label>

                                                                    <label class="col-lg-3 col-md-4 col-form-label text-right">Activity</label>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxObjective" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxCosts" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-3 col-sm-12">
                                                                        <telerik:RadDateTimePicker ID="RtdDateCreated" TimePopupButton-Visible="false" DateInput-DateFormat="MM/dd/yyyy" Width="140" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxActivity" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                                                                    </div>
                                                                </div>

                                                                <div id="divRow2" runat="server" visible="false" class="form-group row">
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxObjective2" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxCosts2" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-3 col-sm-12">
                                                                        <telerik:RadDateTimePicker ID="RtdDateCreated2" TimePopupButton-Visible="false" DateInput-DateFormat="MM/dd/yyyy" Width="140" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxActivity2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                                                                    </div>
                                                                    <div class="col-lg-1 col-md-2 col-sm-12">
                                                                        <a id="ImgBtnRemove2" onserverclick="ImgBtnRemove2_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                            <i class="la la-remove"></i>
                                                                        </a>
                                                                    </div>

                                                                </div>

                                                                <div id="divRow3" runat="server" visible="false" class="form-group row">
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxObjective3" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxCosts3" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-3 col-sm-12">
                                                                        <telerik:RadDateTimePicker ID="RtdDateCreated3" TimePopupButton-Visible="false" DateInput-DateFormat="MM/dd/yyyy" Width="140" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxActivity3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                                                                    </div>
                                                                    <div class="col-lg-1 col-md-2 col-sm-12">
                                                                        <a id="ImgBtnRemove3" onserverclick="ImgBtnRemove3_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                            <i class="la la-remove"></i>
                                                                        </a>
                                                                    </div>
                                                                </div>

                                                                <div id="divRow4" runat="server" visible="false" class="form-group row">
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxObjective4" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxCosts4" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-3 col-sm-12">
                                                                        <telerik:RadDateTimePicker ID="RtdDateCreated4" TimePopupButton-Visible="false" DateInput-DateFormat="MM/dd/yyyy" Width="140" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxActivity4" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                                                                    </div>
                                                                    <div class="col-lg-1 col-md-2 col-sm-12">
                                                                        <a id="ImgBtnRemove4" onserverclick="ImgBtnRemove4_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                            <i class="la la-remove"></i>
                                                                        </a>
                                                                    </div>
                                                                </div>

                                                                <div id="divRow5" runat="server" visible="false" class="form-group row">
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxObjective5" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxCosts5" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-3 col-sm-12">
                                                                        <telerik:RadDateTimePicker ID="RtdDateCreated5" TimePopupButton-Visible="false" DateInput-DateFormat="MM/dd/yyyy" Width="140" runat="server" />
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                                                        <asp:TextBox ID="TbxActivity5" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                                                                    </div>
                                                                    <div class="col-lg-1 col-md-2 col-sm-12">
                                                                        <a id="ImgBtnRemove5" onserverclick="ImgBtnRemove5_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                            <i class="la la-remove"></i>
                                                                        </a>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <div class="col-lg-3">
                                                                        <a id="aAddNew" runat="server" role="button" onserverclick="aAddNew_ServerClick" data-repeater-create="" class="btn font-weight-bold btn-primary">
                                                                            <i class="la la-plus"></i>
                                                                            <asp:Label ID="LblAddNew" Text="Add" runat="server" />
                                                                        </a>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="card-header">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <div class="example-tools justify-content-center">
                                                                        <h3>Technical support issues</h3>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="form-group row">
                                                                    <div class="col-lg-2">
                                                                        <label class="col-lg-10 col-form-label mt-1">Description</label>
                                                                    </div>
                                                                    <div class="col-lg-10">
                                                                        <asp:TextBox ID="TbxTechSupIssues" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-header">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <div class="example-tools justify-content-center">
                                                                        <h3>General issues</h3>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="form-group row">
                                                                    <div class="col-lg-2">
                                                                        <label class="col-lg-10 col-form-label mt-1">Description</label>
                                                                    </div>
                                                                    <div class="col-lg-10">
                                                                        <asp:TextBox ID="TbxGeneralIssues" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <controls:MessageControl ID="UcMessageReporting" Visible="false" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="card-footer">
                                                                <div class="row">
                                                                    <div class="col-lg-9"></div>
                                                                    <div class="col-lg-3">
                                                                        <asp:Button ID="BtnClearReporting" runat="server" OnClick="BtnClearReporting_Click" Text="Clear" CssClass="btn btn-light-primary mr-2" />
                                                                        <asp:Button ID="BtnSubmitReporting" runat="server" OnClick="BtnSubmitReporting_Click" Text="Submit" CssClass="btn btn-primary" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <controls:MessageControl ID="UcMessageControlReporting" runat="server" />

                                                        </div>

                                                        <div class="tab-pane fade" id="tab_2_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                                            <!--begin: Datatable-->
                                                            <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="">
                                                                            <asp:Label ID="LblPartnerName" runat="server" Text="Partner Name" />
                                                                        </th>
                                                                        <th style="">
                                                                            <asp:Label ID="LblCountry" runat="server" Text="Partner Country" />
                                                                        </th>
                                                                        <th style="">
                                                                            <asp:Label ID="LblDate" runat="server" Text="Date Created" />
                                                                        </th>
                                                                        <th style="">
                                                                            <asp:Label ID="LblFilePdf" runat="server" Text="File" />
                                                                        </th>
                                                                        <th style="width: 200px;">
                                                                            <asp:Label ID="LblActions" Text="Actions" runat="server" />
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <asp:Repeater ID="RdgReporting" OnLoad="RdgReporting_Load" runat="server">
                                                                    <ItemTemplate>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="">
                                                                                    <asp:HiddenField ID="HdnId" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' runat="server" />
                                                                                    <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                                        <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                                            <span class="symbol-label">
                                                                                                <asp:Image ID="ImgLogo" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "company_logo")%>' class="h-50 align-self-center" alt="" />
                                                                                            </span>
                                                                                        </div>
                                                                                        <asp:Label ID="LblCompanyNameContent" Text='<%# DataBinder.Eval(Container.DataItem, "company_name")%>' runat="server" />
                                                                                    </a>
                                                                                </td>
                                                                                <td style="">
                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                        <asp:Label ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "country")%>' runat="server" />
                                                                                    </span>
                                                                                </td>
                                                                                <td style="">
                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                        <asp:Label ID="LblClientName" Text='<%# DataBinder.Eval(Container.DataItem, "insert_date")%>' runat="server" />
                                                                                    </span>
                                                                                </td>
                                                                                <td style="">
                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                        <asp:Image ID="ImgFilePath" runat="server" Width="32" Height="32" ImageUrl="/assets/media/svg/files/pdf.svg" />
                                                                                    </span>
                                                                                </td>
                                                                                <td style="">
                                                                                    <a id="aEditReport" runat="server" title="Edit" onserverclick="aEditReport_ServerClick" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
                                                                                        <span class="svg-icon svg-icon-md svg-icon-primary">
                                                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Write.svg-->
                                                                                            <%-- <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                                        <rect x="0" y="0" width="24" height="24" />
                                                                                                        <path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953)" />
                                                                                                        <path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                                                    </g>
                                                                                                </svg>--%>
                                                                                            <!--end::Svg Icon-->
                                                                                        </span>
                                                                                    </a>
                                                                                    <a id="aView" runat="server" title="View as pdf" onserverclick="aView_ServerClick" visible="false" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
                                                                                        <span class="svg-icon svg-icon-md svg-icon-primary">
                                                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Write.svg-->
                                                                                            <%-- <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                                        <rect x="0" y="0" width="24" height="24" />
                                                                                                        <path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953)" />
                                                                                                        <path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                                                    </g>
                                                                                                </svg>--%>
                                                                                            <!--end::Svg Icon-->
                                                                                        </span>
                                                                                    </a>
                                                                                    <a id="aDelete" onserverclick="BtnDelete_OnClick" title="Delete" runat="server" class="btn btn-icon btn-light-danger btn-hover-danger btn-sm">
                                                                                        <span class="svg-icon svg-icon-md svg-icon-primary">
                                                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/General/Trash.svg-->
                                                                                            <%-- <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                                        <rect x="0" y="0" width="24" height="24" />
                                                                                                        <path d="M6,8 L6,20.5 C6,21.3284271 6.67157288,22 7.5,22 L16.5,22 C17.3284271,22 18,21.3284271 18,20.5 L18,8 L6,8 Z" fill="#000000" fill-rule="nonzero" />
                                                                                                        <path d="M14,4.5 L14,4 C14,3.44771525 13.5522847,3 13,3 L11,3 C10.4477153,3 10,3.44771525 10,4 L10,4.5 L5.5,4.5 C5.22385763,4.5 5,4.72385763 5,5 L5,5.5 C5,5.77614237 5.22385763,6 5.5,6 L18.5,6 C18.7761424,6 19,5.77614237 19,5.5 L19,5 C19,4.72385763 18.7761424,4.5 18.5,4.5 L14,4.5 Z" fill="#000000" opacity="0.3" />
                                                                                                    </g>
                                                                                                </svg>--%>
                                                                                            <!--end::Svg Icon-->
                                                                                        </span>
                                                                                    </a>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                            <!--end: Datatable-->

                                                            <controls:MessageControl ID="UcMessageAlertGrid" runat="server" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end::Card-->
                    <!--end::Dashboard-->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <!-- Invitation form (modal view) -->
    <div id="InvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel6">
            <ContentTemplate>
                <controls:UcAddToTeam ID="UcAddToTeamForm" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--IS3 Edit Tier form (modal view) -->
    <div id="EditTierModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblMessageAlertTitle" Text="Criteria" CssClass="control-label" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row mb-5">
                                <form action="#" class="form-horizontal col-lg-12" role="form">
                                    <div class="form-group row" style="width: 85% !important;">
                                        <label class="col-lg-6 col-form-label text-right">Statutory Compliance:</label>
                                        <div class="col-lg-6 text-right">
                                            <asp:DropDownList ID="Drp1" EnableViewState="true" Width="220" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                    </div>

                                    <div class="form-group row pl-2">
                                        <div class="pt-2 mb-0">
                                            <div class="form-group mb-0 row">
                                                <label class="col-10 col-form-label text-right">
                                                    CIPC Annual Returns
                                                </label>
                                                <div class="col-2 text-right">
                                                    <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                        <label>
                                                            <input id="CbxCrit1" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                            <span></span>
                                                        </label>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="form-group mb-0 row">
                                                <label class="col-10 col-form-label text-right">
                                                    COIDA
                                                </label>
                                                <div class="col-2 text-right">
                                                    <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                        <label>
                                                            <input id="CbxCrit2" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                            <span></span>
                                                        </label>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="form-group mb-0 row">
                                                <label class="col-10 col-form-label text-right">
                                                    Tax and VAT compliance
                                                </label>
                                                <div class="col-2 text-right">
                                                    <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                        <label>
                                                            <input id="CbxCrit3" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                            <span></span>
                                                        </label>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="form-group mb-0 row">
                                                <label class="col-10 col-form-label text-right">
                                                    UIF clearance certificate
                                                </label>
                                                <div class="col-2 text-right">
                                                    <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                        <label>
                                                            <input id="CbxCrit4" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                            <span></span>
                                                        </label>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="form-group mb-0 row">
                                                <label class="col-10 col-form-label text-right">
                                                    Proof of banking
                                                </label>
                                                <div class="col-2 text-right">
                                                    <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                        <label>
                                                            <input id="CbxCrit5" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                            <span></span>
                                                        </label>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row" style="width: 85% !important;">
                                        <label class="col-lg-6 col-form-label text-right">Sales Achievement:</label>
                                        <div class="col-lg-6 text-right">
                                            <asp:DropDownList ID="Drp2" runat="server" Width="220" CssClass="form-control">
                                            </asp:DropDownList>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                    </div>

                                    <div class="form-group row" style="width: 85% !important;">
                                        <label class="col-lg-6 col-form-label text-right">Credit Health:</label>
                                        <div class="col-lg-6 text-right">
                                            <asp:DropDownList ID="Drp3" runat="server" Width="220" CssClass="form-control">
                                            </asp:DropDownList>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                    </div>

                                    <div class="form-group row" style="width: 85% !important;">
                                        <label class="col-lg-6 col-form-label text-right">Customer Satisfaction:</label>
                                        <div class="col-lg-6">
                                            <asp:DropDownList ID="Drp4" runat="server" Width="220" CssClass="bs-select form-control">
                                            </asp:DropDownList>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                    </div>

                                    <div class="form-group row" style="width: 85% !important;">
                                        <label class="col-lg-6 col-form-label text-right">AVEVA Courses:</label>
                                        <div class="col-lg-6">
                                            <asp:DropDownList ID="Drp5" runat="server" Width="220" CssClass="bs-select form-control">
                                            </asp:DropDownList>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                    </div>

                                    <div class="form-group row" style="width: 85% !important;">
                                        <label class="col-lg-6 col-form-label text-right">AVEVA Certified Developers:</label>
                                        <div class="col-lg-6">
                                            <asp:DropDownList ID="Drp6" runat="server" Width="220" CssClass="bs-select form-control">
                                            </asp:DropDownList>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                    </div>

                                    <div class="form-group row" style="width: 85% !important;">
                                        <label class="col-lg-6 col-form-label text-right">AVEVA SI Status:</label>
                                        <div class="col-lg-6">
                                            <asp:DropDownList ID="Drp7" runat="server" Width="220" CssClass="bs-select form-control">
                                            </asp:DropDownList>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                    </div>

                                </form>
                            </div>
                            <div class="row">
                                <div id="divGeneralSuccess" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-questions-circular-button"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblGeneralSuccess" Text="Done!" runat="server" />
                                        </strong>
                                        <asp:Label ID="LblSuccess" Text="Tier was saved successfully" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">
                                                <i class="ki ki-close"></i>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                                <div id="divGeneralFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-questions-circular-button"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblGeneralFailure" Text="Warning!" runat="server" />
                                        </strong>
                                        <asp:Label ID="LblFailure" Text="You must select values for all criteria" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">
                                                <i class="ki ki-close"></i>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-light-primary">Close</button>
                            <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" Text="Submit" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up form Message (modal view) -->
    <div id="PopUpMarketingInitiativesAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="Label2" Text="Information Message" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblMarketingInitiativesAlertMsg" Text="Save these first in order to add more" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnMarketingInitiativesRemove" Text="Proceed" OnClick="BtnMarketingInitiativesRemove_Click" Visible="false" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Confirmation Modal -->
    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblConfTitle" Text="Confirmation" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="LblConfMsg" Text="Are you sure you want to delete this report?" runat="server" />
                                    <asp:HiddenField ID="TbxConfId" Value="0" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="UcPopUpConfirmationMessageAlert" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-light-primary">No</button>
                            <asp:Button ID="BtnConfDelete" OnClick="BtnConfDelete_OnClick" CssClass="btn btn-primary" Text="Yes" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up Message Alert (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblMessageTitle" Text="Warning" CssClass="control-label" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group">
                                    <controls:MessageControl ID="UcMessageAlert" runat="server" />
                                    <asp:HiddenField ID="HdnVendorResellerId" Value="0" runat="server" />
                                    <asp:HiddenField ID="HdnSubAccountId" Value="0" runat="server" />
                                    <asp:HiddenField ID="HdnPartnerId" Value="0" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnDeletePartnerAccountConfirm" Visible="false" OnClick="BtnDeletePartnerAccountConfirm_Click" Text="Yes" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up Assign Partner (modal view) -->
    <div id="PopUpAssignPartner" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="Label5" Text="Assign Partner to Team Member" CssClass="control-label" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group" style="width: 100%;">
                                    <span class="btn btn-sm btn-text" style="padding-bottom: 0px;">
                                        <asp:Label ID="Label3" Text="List of your team members:" runat="server" />
                                    </span>
                                    <span class="btn btn-sm btn-text">
                                        <asp:DropDownList ID="DrpTeamMembers" CssClass="form-control" Width="400" runat="server" />
                                    </span>
                                </div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="UcAssignmentMessageAlert" Visible="false" runat="server" />
                                <asp:HiddenField ID="HdnVendResId" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnSubAcId" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnPartId" Value="0" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <a id="aSaveAssignToMember" class="btn btn-primary" runat="server" onserverclick="aSaveAssignToMember_ServerClick">Save
                            </a>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--Common Edit Tier form (modal view) -->
    <div id="EditTierModalCommon" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel7">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="Label17" Text="Set Tier for your partner" CssClass="control-label" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card-body" style="padding: 2px 2px 2px 2px;">
                                        <div class="col-12" style="margin-bottom: 30px;">
                                            <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                                <li class="nav-item">
                                                    <a id="aSetTier" onserverclick="aSetTier_ServerClick" runat="server" class="nav-link active">                                                        
                                                        <span class="nav-text">
                                                            <asp:Label ID="LblSetTier" Text="Set Tier" runat="server" />
                                                        </span>
                                                    </a>
                                                </li>
                                                <li class="nav-item">
                                                    <a id="aAddAmount" onserverclick="aAddAmount_ServerClick" runat="server" class="nav-link">
                                                        <span class="nav-text">
                                                            <asp:Label ID="LblAddAmount" Text="Add Amount" runat="server" />
                                                        </span>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="tab-content">
                                            <div class="tab-pane fade show active" id="tab_SetTier" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_6">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group" style="width: 100%;">
                                                            <label class="btn btn-sm btn-text">Select Tier:</label>
                                                            <div class="btn btn-sm btn-text">
                                                                <asp:DropDownList ID="DdlTiers" Width="200" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                                <span class="form-text text-muted"></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane fade" id="tab_AddAmount" runat="server" visible="false" role="tabpanel" aria-labelledby="kt_tab_pane_1_6">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group" style="width: 100%;">
                                                            <label class="btn btn-sm btn-text">Add Amount:</label>
                                                            <div class="btn btn-sm btn-text" style="width:70%;">
                                                                <div style="float:left;">
                                                                    <asp:TextBox ID="TbxAddAmount" Width="200" CssClass="form-control" runat="server">
                                                                    </asp:TextBox>
                                                                </div>
                                                                <div style="margin-top:10px;">
                                                                <span class="font-weight-bolder font-size-h5">
                                                                    <span class="text-dark-50 font-weight-bold">
                                                                        <asp:Label ID="LblVendorCurrencySymb" runat="server" Text="$" />
                                                                    </span>
                                                                </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="divSucc" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-questions-circular-button"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="Label19" Text="Done!" runat="server" />
                                        </strong>
                                        <asp:Label ID="LblSucc" Text="Tier was saved successfully!" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">
                                                <i class="ki ki-close"></i>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                                <div id="divFail" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-questions-circular-button"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="Label21" Text="Warning!" runat="server" />
                                        </strong>
                                        <asp:Label ID="LblFail" Text="Tier could not be saved! Please try again." runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">
                                                <i class="ki ki-close"></i>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnSaveTierCommon" OnClick="BtnSaveTierCommon_Click" Text="Save" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">

            function OpenInvitationPopUp() {
                var x = $('#InvitationModal').modal('show');
                console.log(x);
            }
            function CloseInvitationPopUp() {
                var x = $('#InvitationModal').modal('hide');
                console.log(x);
            }

            function BtnCancelMessage_OnClientClick(sender, e) {
                var x = $('#InvitationModal').modal('hide');
            }

            function OpenAssignPartnerPopUp() {
                $('#PopUpAssignPartner').modal('show');
            }

            function CloseAssignPartnerPopUp() {
                $('#PopUpAssignPartner').modal('hide');
            }

            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }

            function OpenMarketingInitiativesPopUp() {
                $('#PopUpMarketingInitiativesAlert').modal('show');
            }

            function CloseMarketingInitiativesPopUp() {
                $('#PopUpMarketingInitiativesAlert').modal('hide');
            }

            function CloseConfirmPopUp() {
                $('#divConfirm').modal('hide');
            }

            function OpenConfirmPopUp() {
                $('#divConfirm').modal('show');
            }

            function UpdateCheckBoxes() {

                var cbx1 = document.getElementById('<%= CbxCrit1.ClientID%>').value;
                var cbx2 = document.getElementById('<%= CbxCrit2.ClientID%>').value;
                var cbx3 = document.getElementById('<%= CbxCrit3.ClientID%>').value;
                var cbx4 = document.getElementById('<%= CbxCrit4.ClientID%>').value;
                var cbx5 = document.getElementById('<%= CbxCrit5.ClientID%>').value;

                if (cbx1 == "0") {
                    cbx1.checked = true;
                    cbx1.value = "1";
                }
                else {
                    cbx1.checked = false;
                    cbx1.value = "0";
                }

                if (cbx2 == "0") {
                    cbx2.checked = true;
                    cbx2.value = "1";
                }
                else {
                    cbx2.checked = false;
                    cbx2.value = "0";
                }

                if (cbx3 == "0") {
                    cbx3.checked = true;
                    cbx3.value = "1";
                }
                else {
                    cbx3.checked = false;
                    cbx3.value = "0";
                }

                if (cbx4 == "0") {
                    cbx4.checked = true;
                    cbx4.value = "1";
                }
                else {
                    cbx4.checked = false;
                    cbx4.value = "0";
                }

                if (cbx5 == "0") {
                    cbx5.checked = true;
                    cbx5.value = "1";
                }
                else {
                    cbx5.checked = false;
                    cbx5.value = "0";
                }
            }

            function SetChecked() {
                //var cbx1 = document.getElementById('<%= CbxCrit1.ClientID%>').value;

                var isChkd1 = '<%=IsCbx1Ckecked%>';
                var isChkd2 = '<%=IsCbx2Ckecked%>';
                var isChkd3 = '<%=IsCbx3Ckecked%>';
                var isChkd4 = '<%=IsCbx4Ckecked%>';
                var isChkd5 = '<%=IsCbx5Ckecked%>';

                if (isChkd1) {
                    document.getElementById("CbxCrit1").checked = true;
                }
                if (isChkd2) {
                    document.getElementById("CbxCrit2").checked = true;
                }
                if (isChkd3) {
                    document.getElementById("CbxCrit3").checked = true;
                }
                if (isChkd4) {
                    document.getElementById("CbxCrit4").checked = true;
                }
                if (isChkd5) {
                    document.getElementById("CbxCrit5").checked = true;
                }
            }

            function OpenEditTierModal() {
                $('#EditTierModal').modal('show');
            }

            function CloseEditTierModal() {
                $('#EditTierModal').modal('hide');
            }

            function OpenEditTierModalCommon() {
                $('#EditTierModalCommon').modal('show');
            }

            function CloseEditTierModalCommon() {
                $('#EditTierModalCommon').modal('hide');
            }

        </script>

    </telerik:RadScriptBlock>

</asp:Content>
