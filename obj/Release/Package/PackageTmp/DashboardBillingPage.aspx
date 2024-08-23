<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardBillingPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardBillingPage" %>

<%@ Register Src="/Controls/Payment/Stripe_Service_Ctrl.ascx" TagName="UcServiceStripe" TagPrefix="controls" %>
<%@ Register Src="/Controls/Payment/Stripe_CreditCard_Ctrl.ascx" TagName="UcStripeCreditCard" TagPrefix="controls" %>
<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DACancelOrderConfirmationMessageAlert.ascx" TagName="UcConfirmationMessageAlert" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DACancelServiceOrderConfirmationMessageAlert.ascx" TagName="UcConfirmationServiceMessageAlert" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <asp:UpdatePanel runat="server" ID="UpdatePnl" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="card card-custom gutter-b">
                        <div class="card-header" style="border-top: 1px solid #ECF0F3; border-bottom: 0px #FFFFFF !important;">
                            <div class="card-title">
                                <span class="card-icon">
                                    <i class="flaticon2-chart text-primary"></i>
                                </span>
                                <h3 class="card-label">
                                    <asp:Label ID="LblElioplusDashboard" runat="server" />
                                </h3>
                            </div>
                            <div class="card-toolbar">
                                <asp:Label ID="LblDashSubTitle" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div id="divPricingPrmPlan" runat="server" visible="false" class="card card-custom gutter-b">
                        <div class="card-header" style="border-top: 1px solid #ECF0F3; border-bottom: 0px #FFFFFF !important;">
                            <div class="card-title">
                                <span class="card-icon">
                                    <i class="flaticon2-box-1 text-success"></i>
                                </span>
                                <h3 class="card-label">PRM Software Plans</h3>
                                <a href="/prm-software-pricing">Compare plans & pricing
                                </a>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row my-10">
                                <!--begin: Pricing-->
                                <div class="col-md-4">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                        
                                        <span class="font-size-h1 d-block font-weight-boldest text-dark-75 py-2">298
												
                                            <sup class="font-size-h3 font-weight-normal pl-1">$</sup></span>
                                        <h4 class="font-size-h3 d-block font-weight-bold mb-7 text-dark">Start Up</h4>
                                        <p class="mb-15 d-flex flex-column">
                                            <span>Manage up to 100 partners</span>
                                        </p>
                                        <div class="d-flex justify-content-center">
                                            <a id="aChekoutStartup" onserverclick="aChekoutStartup_ServerClick" runat="server" class="btn btn-success text-uppercase font-weight-bolder px-15 py-3">Upgrade Now</a>
                                        </div>
                                    </div>
                                </div>
                                <!--end: Pricing-->
                                <!--begin: Pricing-->
                                <div class="col-md-4 border-x-0 border-x-md border-y border-y-md-0">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                        
                                        <span class="font-size-h1 d-block font-weight-boldest text-dark-75 py-2">498
												
                                            <sup class="font-size-h3 font-weight-normal pl-1">$</sup></span>
                                        <h4 class="font-size-h3 d-block font-weight-bold mb-7 text-dark">Growth</h4>
                                        <p class="mb-15 d-flex flex-column">
                                            <span>Manage up to 250 partners</span>
                                        </p>
                                        <div class="d-flex justify-content-center">
                                            <a id="aChekoutGrowth" onserverclick="aChekoutGrowth_ServerClick" runat="server" class="btn btn-danger text-uppercase font-weight-bolder px-15 py-3">Upgrade Now</a>
                                        </div>
                                    </div>
                                </div>
                                <!--end: Pricing-->
                                <!--begin: Pricing-->
                                <div class="col-md-4">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                        
                                        <span class="font-size-h1 d-block font-weight-boldest text-dark-75 py-2">1200+
												
                                            <sup class="font-size-h3 font-weight-normal pl-1">$</sup></span>
                                        <h4 class="font-size-h3 d-block font-weight-bold mb-7 text-dark">Enterprise</h4>
                                        <p class="mb-15 d-flex flex-column">
                                            <span>Custom partners management</span>
                                        </p>
                                        <div class="d-flex justify-content-center">
                                            <a href="/contact-us" class="btn btn-warning text-uppercase font-weight-bolder px-15 py-3">Contact Us</a>
                                        </div>
                                    </div>
                                </div>
                                <!--end: Pricing-->
                            </div>
                        </div>
                    </div>

                    <div id="divPricingPartnerRecruitmentPlan" runat="server" visible="false" class="card card-custom gutter-b">
                        <div class="card-header" style="border-top: 1px solid #ECF0F3; border-bottom: 0px #FFFFFF !important;">
                            <div class="card-title">
                                <span class="card-icon">
                                    <i class="flaticon2-chart text-primary"></i>
                                </span>
                                <h3 class="card-label">Partner Recruitment Plans (Database)</h3>
                                <a href="/partner-recruitment-pricing">Compare plans & pricing
                                </a>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row my-10">
                                <!-- begin: Pricing-->
                                <div class="col-md-4">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                       
                                        <!--begin::Content-->
                                        <h4 class="font-size-h3 mb-10">Start Up Plan</h4>
                                        <div class="d-flex flex-column line-height-xl pb-10">
                                            <span>100 Channel Partner Profiles</span>
                                        </div>
                                        <span class="font-size-h1 d-block font-weight-boldest text-dark">238
															
                                                        <sup class="font-size-h3 font-weight-normal pl-1">$</sup></span>
                                        <div class="mt-7">
                                            <a id="aChekoutStartupData" onserverclick="aChekoutStartupData_ServerClick" runat="server" class="btn btn-primary text-uppercase font-weight-bolder px-15 py-3">Upgrade Now</a>
                                        </div>
                                        <!--end::Content-->
                                    </div>
                                </div>
                                <!-- end: Pricing-->
                                <!-- begin: Pricing-->
                                <div class="col-md-4 border-x-0 border-x-md border-y border-y-md-0">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                        
                                        <!--begin::Content-->
                                        <h4 class="font-size-h3 mb-10">Growth Plan</h4>
                                        <div class="d-flex flex-column line-height-xl mb-10">
                                            <span>200 Channel Partner Profiles</span>
                                        </div>
                                        <span class="font-size-h1 d-block font-weight-boldest text-dark">398
															
                                                        <sup class="font-size-h3 font-weight-normal pl-1">$</sup></span>
                                        <div class="mt-7">
                                            <a id="aChekoutGrowthData" onserverclick="aChekoutGrowthData_ServerClick" runat="server" class="btn btn-primary text-uppercase font-weight-bolder px-15 py-3">Upgrade Now</a>
                                        </div>
                                        <!--end::Content-->
                                    </div>
                                </div>
                                <!-- end: Pricing-->
                                <!-- begin: Pricing-->
                                <div class="col-md-4">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                       
                                        <!--begin::Content-->
                                        <h4 class="font-size-h3 mb-10">Enterprise</h4>
                                        <div class="d-flex flex-column line-height-xl mb-10">
                                            <span>Custom No. of Profiles</span>
                                        </div>
                                        <span class="font-size-h1 d-block font-weight-boldest text-dark">900+
															
                                                        <sup class="font-size-h3 font-weight-normal pl-1">$</sup></span>
                                        <div class="mt-7">
                                            <a href="/contact-us" class="btn btn-primary text-uppercase font-weight-bolder px-15 py-3">Contact Us</a>
                                        </div>
                                        <!--end::Content-->
                                    </div>
                                </div>
                                <!-- end: Pricing-->
                            </div>
                            <div class="row my-10">
                                <strong>* 3 months minimum commitment is required on all Partner Recruitment Database plans. After the 3-month period your plan will renew automatically or you can cancel at anytime.
                                </strong>
                            </div>
                        </div>

                        <div class="card-header" style="border-top: 1px solid #ECF0F3; border-bottom: 0px #FFFFFF !important;">
                            <div class="card-title">
                                <span class="card-icon">
                                    <i class="flaticon2-chart text-primary"></i>
                                </span>
                                <h3 class="card-label">Partner Recruitment Plans (Automation)</h3>
                                <a href="/partner-recruitment-pricing">Compare plans & pricing
                                </a>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row my-10">
                                <!-- begin: Pricing-->
                                <div class="col-md-4">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                       
                                        <!--begin::Content-->
                                        <h4 class="font-size-h3 mb-10">Start Up Plan</h4>
                                        <div class="d-flex flex-column line-height-xl mb-10">
                                            <span>70 Matches</span>
                                        </div>
                                        <span class="font-size-h1 d-block font-weight-boldest text-dark">298
												
                                                    <sup class="font-size-h3 font-weight-normal pl-1">$</sup></span>
                                        <div class="mt-7">
                                            <a id="aChekoutStartupAuto" runat="server" onserverclick="aChekoutStartupAuto_ServerClick" class="btn btn-primary text-uppercase font-weight-bolder px-15 py-3">Upgrade Now</a>
                                        </div>
                                        <!--end::Icon-->
                                    </div>
                                </div>
                                <!-- end: Pricing-->
                                <!-- begin: Pricing-->
                                <div class="col-md-4 border-x-0 border-x-md border-y border-y-md-0">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                        
                                        <!--begin::Content-->
                                        <h4 class="font-size-h3 mb-10">Growth Plan</h4>
                                        <div class="d-flex flex-column line-height-xl mb-10">
                                            <span>150 Matches</span>
                                        </div>
                                        <span class="font-size-h1 d-block font-weight-boldest text-dark">498
												
                                                    <sup class="font-size-h3 font-weight-normal pl-1">$</sup></span>
                                        <div class="mt-7">
                                            <a id="aChekoutGrowthAuto" runat="server" onserverclick="aChekoutGrowthAuto_ServerClick" class="btn btn-primary text-uppercase font-weight-bolder px-15 py-3">Upgrade Now</a>
                                        </div>
                                        <!--end::Content-->
                                    </div>
                                </div>
                                <!-- end: Pricing-->
                                <!-- begin: Pricing-->
                                <div class="col-md-4">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                       
                                        <!--end::Content-->
                                        <h4 class="font-size-h3 mb-10">Enterprise</h4>
                                        <div class="d-flex flex-column line-height-xl mb-10">
                                            <span>Custom No. of Matches</span>
                                        </div>
                                        <span class="font-size-h1 d-block font-weight-boldest text-dark">1200+
												
                                                    <sup class="font-size-h3 font-weight-normal pl-1">$</sup></span>
                                        <div class="mt-7">
                                            <a href="/contact-us" class="btn btn-primary text-uppercase font-weight-bolder px-15 py-3">Contact Us</a>
                                        </div>
                                        <!--end::Content-->
                                    </div>
                                </div>
                                <!-- end: Pricing-->
                            </div>
                        </div>
                    </div>

                    <div id="divServicePlan" runat="server" visible="false" class="card card-custom gutter-b">
                        <div class="card-header" style="border-top: 1px solid #ECF0F3; border-bottom: 0px #FFFFFF !important;">
                            <div class="card-title">
                                <span class="card-icon">
                                    <i class="flaticon2-box-1 text-success"></i>
                                </span>
                                <h3 class="card-label">Service Plan
                                </h3>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <!--begin: Pricing-->
                                <div class="col-md-12">
                                    <div class="px-5 text-center">
                                        Add an
                    <b>
                        <asp:Label Style="color: #1BC5BD; font-size: large;" ID="LblManagedText" Text=" Account Manager " runat="server" /></b>
                                        to your account that will follow up with all your connections and send you qualified leads 
        that are ready for Demo or Meeting. We’ll follow up with every connection you receive multiple times until we get a response in 
        order to increase the chances of partnership. You’ll be able to watch the progress of your account via your Prospect Pipeline 
        where you’ll find every task and prospect note at any time, giving you a complete view of your leads.
                                    </div>
                                </div>
                            </div>
                            <div class="row my-10">
                                <!--begin: Pricing-->
                                <div class="col-md-4">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                    </div>
                                </div>
                                <!--end: Pricing-->
                                <!--begin: Pricing-->
                                <div class="col-md-4 border-x-0 border-x-md border-y border-y-md-0">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                        
                                        <span class="font-size-h1 d-block font-weight-boldest text-dark-75 py-2">
                                            <asp:Label ID="LblServiceCost" Text="299" runat="server" />
                                            <sup class="font-size-h3 font-weight-normal pl-1">$</sup>
                                        </span>
                                        <h4 class="font-size-h3 mb-10">
                                            <asp:Label ID="LblElioService" runat="server" />
                                        </h4>
                                        <div class="d-flex justify-content-center">
                                            <a id="aGetElioService" runat="server" onserverclick="aGetElioService_ServerClick" class="btn btn-success text-uppercase font-weight-bolder px-15 py-3">
                                                <asp:Label ID="LblGetElioService" runat="server" />
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <!--end: Pricing-->
                                <!--begin: Pricing-->
                                <div class="col-md-4">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                    </div>
                                </div>
                                <!--end: Pricing-->
                            </div>
                            <div class="row my-10">
                                <strong>* 3 months minimum commitment is required on Account Manager Service plan. After the 3-month period your plan will renew automatically or you can cancel at anytime.
                                </strong>
                            </div>
                        </div>
                    </div>

                    <div id="divIntentSignalsPlan" runat="server" visible="false" class="card card-custom gutter-b">
                        <div class="card-header" style="border-top: 1px solid #ECF0F3; border-bottom: 0px #FFFFFF !important;">
                            <div class="card-title">
                                <span class="card-icon">
                                    <i class="flaticon2-box-1 text-success"></i>
                                </span>
                                <h3 class="card-label">Intent Signal Plans</h3>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row my-10">
                                <!--begin: Pricing-->
                                <div class="col-md-4">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                    </div>
                                </div>
                                <!--end: Pricing-->
                                <!--begin: Pricing-->
                                <div class="col-md-4 border-x-0 border-x-md border-y border-y-md-0">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                        <div class="d-flex flex-center position-relative mb-25">
                                            <span class="svg svg-fill-primary opacity-4 position-absolute">
                                                <svg width="175" height="200">
                                                    <polyline points="87,0 174,50 174,150 87,200 0,150 0,50 87,0" />
                                                </svg>
                                            </span>
                                            <span class="svg-icon svg-icon-5x svg-icon-success">
                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Shopping/Box3.svg-->
                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                        <rect x="0" y="0" width="24" height="24" />
                                                        <path d="M20.4061385,6.73606154 C20.7672665,6.89656288 21,7.25468437 21,7.64987309 L21,16.4115967 C21,16.7747638 20.8031081,17.1093844 20.4856429,17.2857539 L12.4856429,21.7301984 C12.1836204,21.8979887 11.8163796,21.8979887 11.5143571,21.7301984 L3.51435707,17.2857539 C3.19689188,17.1093844 3,16.7747638 3,16.4115967 L3,7.64987309 C3,7.25468437 3.23273352,6.89656288 3.59386153,6.73606154 L11.5938615,3.18050598 C11.8524269,3.06558805 12.1475731,3.06558805 12.4061385,3.18050598 L20.4061385,6.73606154 Z" fill="#000000" opacity="0.3" />
                                                        <polygon fill="#000000" points="14.9671522 4.22441676 7.5999999 8.31727912 7.5999999 12.9056825 9.5999999 13.9056825 9.5999999 9.49408582 17.25507 5.24126912" />
                                                    </g>
                                                </svg>
                                                <!--end::Svg Icon-->
                                            </span>
                                        </div>
                                        <span class="font-size-h1 d-block font-weight-boldest text-dark-75 py-2">Custom Pricing
					
                        <sup class="font-size-h3 font-weight-normal pl-1">$</sup></span>
                                        <h4 class="font-size-h6 d-block font-weight-bold mb-7 text-dark-50">Premium</h4>
                                        <p class="mb-15 d-flex flex-column">
                                            <span>Get a Featured company profile</span>
                                            <span>Access RFQs</span>
                                            <span>Unlimited High Intent Leads</span>
                                            <span>Exclusive Customer Requests</span>
                                            <span>Worlwide or Local Targeting</span>
                                            <span>Enreach Your Leads (Coming Soon)</span>
                                        </p>
                                        <div class="d-flex justify-content-center">
                                            <a href="/contact-us" class="btn btn-success text-uppercase font-weight-bolder px-15 py-3">Contact Sales</a>
                                        </div>
                                    </div>
                                </div>
                                <!--end: Pricing-->
                                <!--begin: Pricing-->
                                <div class="col-md-4">
                                    <div class="pt-30 pt-md-25 pb-15 px-5 text-center">
                                    </div>
                                </div>
                                <!--end: Pricing-->
                            </div>
                        </div>
                    </div>

                    <div class="card card-custom" style="margin-top: 25px;">
                        <div class="card-header card-header-tabs-line">
                            <div class="card-title">
                                <h3 class="card-label"></h3>
                            </div>
                            <div class="card-toolbar">
                                <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                    <li id="liBillingHistory" runat="server" class="nav-item">
                                        <a id="aBillingHistory" role="button" onserverclick="aBillingHistory_ServerClick" runat="server" class="nav-link active">
                                            <span class="nav-icon"><i class="flaticon2-chat-1"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblBillingHistory" Text="Transactions History" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                    <li id="liEditBillingAccount" runat="server" class="nav-item">
                                        <a id="aEditBillingAccount" role="button" onserverclick="aEditBillingAccount_ServerClick" runat="server" class="nav-link">
                                            <span class="nav-icon"><i class="flaticon2-drop"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblBillingInfo" Text="Billing Info" runat="server" /></span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <div class="row">
                                        <!--begin::Search Form-->
                                        <div id="divSearch" runat="server" visible="false" class="mb-7">
                                            <div class="row align-items-center">
                                                <div class="col-lg-9 col-xl-8">
                                                    <div class="row align-items-center">
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="input-icon">
                                                                <asp:TextBox ID="TbxInvoices" Width="250" placeholder="Invoice number" CssClass="form-control" runat="server" />

                                                                <span>
                                                                    <i class="flaticon2-search-1 text-muted"></i>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-xl-4 mt-5 mt-lg-0">
                                                </div>
                                            </div>
                                        </div>
                                        <!--end::Search Form-->
                                        <!--begin: Datatable-->
                                        <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                            <thead>
                                                <tr>
                                                    <th style="">
                                                        <asp:Label ID="Label12" runat="server" Text="Plan Status" />
                                                    </th>
                                                    <th style="width: 150px;">
                                                        <asp:Label ID="Label13" runat="server" Text="Plan" />
                                                    </th>
                                                    <th style="">
                                                        <asp:Label ID="Label14" runat="server" Text="Date Created" />
                                                    </th>
                                                    <th style="">
                                                        <asp:Label ID="Label15" runat="server" Text="Total Amount" />
                                                    </th>
                                                    <th style="">
                                                        <asp:Label ID="Label16" runat="server" Text="Invoice" />
                                                    </th>
                                                    <th style="">
                                                        <asp:Label ID="LblAdded" runat="server" Text="Cancel Plan" />
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="LblIsActive" runat="server" Text="Activate Plan" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <telerik:RadGrid ID="RdgOrders" ShowHeader="false" AllowPaging="true" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowSorting="false" PagerStyle-Position="Bottom"
                                                PageSize="20" OnNeedDataSource="RdgOrders_OnNeedDataSource" Width="100%"
                                                OnItemDataBound="RdgOrders_OnItemDataBound" AutoGenerateColumns="false"
                                                runat="server">
                                                <MasterTableView Width="100%" DataKeyNames="id" Name="Parent">
                                                    <NoRecordsTemplate>
                                                        <div class="emptyGridHolder">
                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                        </div>
                                                    </NoRecordsTemplate>
                                                    <Columns>
                                                        <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="user_subscription_id" UniqueName="user_subscription_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="user_id" UniqueName="user_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="invoice_id" UniqueName="invoice_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="charge_id" UniqueName="charge_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="is_closed" UniqueName="is_closed" />
                                                        <telerik:GridBoundColumn Display="false" DataField="sub_total_amount" UniqueName="sub_total_amount" />
                                                        <telerik:GridBoundColumn Display="false" DataField="number" UniqueName="number" />
                                                        <telerik:GridBoundColumn Display="false" DataField="period_start" UniqueName="period_start" />
                                                        <telerik:GridBoundColumn Display="false" DataField="period_end" UniqueName="period_end" />
                                                        <telerik:GridBoundColumn Display="false" DataField="is_paid" UniqueName="is_paid" />
                                                        <telerik:GridBoundColumn DataField="status" UniqueName="status" />
                                                        <telerik:GridBoundColumn DataField="plan_id" UniqueName="plan_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="coupon_id" UniqueName="coupon_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="plan_nickname" UniqueName="plan_nickname" />
                                                        <telerik:GridBoundColumn Display="false" DataField="subscription_id" UniqueName="subscription_id" />
                                                        <telerik:GridBoundColumn DataField="date" UniqueName="date" />
                                                        <telerik:GridBoundColumn DataField="total_amount" UniqueName="total_amount" />
                                                        <telerik:GridBoundColumn DataField="invoice_pdf" UniqueName="invoice_pdf" />
                                                        <telerik:GridBoundColumn DataField="cancel" />
                                                        <telerik:GridBoundColumn DataField="activate" />
                                                        <telerik:GridTemplateColumn Display="false">
                                                            <ItemTemplate>
                                                                <tbody>
                                                                    <tr style="">
                                                                        <td style="width: 125px;">
                                                                            <span class="">
                                                                                <asp:Label ID="LblStatus" runat="server" /></span>
                                                                        </td>
                                                                        <td style="width: 225px;">
                                                                            <asp:Label ID="LblPlan" runat="server" />
                                                                        </td>
                                                                        <td style="width: 215px;">
                                                                            <asp:Label ID="LblDateCreated" runat="server" />
                                                                        </td>
                                                                        <td style="width: 185px;">
                                                                            <asp:Label ID="LblPrice" runat="server" />
                                                                        </td>
                                                                        <td style="width: 120px;">
                                                                            <div>
                                                                                <a id="aInvoiceUrl" runat="server">
                                                                                    <asp:Label ID="LblInvoiceUrl" runat="server" />
                                                                                </a>
                                                                                <asp:LinkButton Visible="false" ID="LnkBtnInvoiceExport" Text="invoices" runat="server" OnClick="LnkBtnInvoiceExport_Click" />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <a id="BtnCancelPlan" onserverclick="BtnCancelPlan_ServerClick" class="btn btn-circle btn-sm purple" runat="server">
                                                                                <asp:Label ID="LblBtnCancelPlan" runat="server" />
                                                                                <i class="fa fa-times"></i>
                                                                            </a>
                                                                        </td>
                                                                        <td style="">
                                                                            <a id="BtnActivatePlan" visible="false" onserverclick="PaymentCanceledModal_OnClick" class="btn btn-circle btn-sm purple" runat="server">
                                                                                <asp:Label ID="LblBtnActivatePlan" runat="server" />
                                                                                <i class="fa fa-check"></i>
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </table>
                                        <!--end: Datatable-->
                                    </div>
                                    <div class="row">
                                        <controls:MessageControl ID="MessageAlertHistory" runat="server" />
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="tab_1_2" runat="server" visible="false" role="tabpanel" aria-labelledby="kt_tab_pane_2_3">
                                    <div class="row">
                                        <div class="col-3">
                                            <ul class="navi navi-border">
                                                <li class="navi-item">
                                                    <a id="aElioBillingDetails" runat="server" role="button" onserverclick="aElioBillingDetails_ServerClick" class="navi-link active">
                                                        <span class="navi-icon"><i class="flaticon2-analytics"></i></span>
                                                        <span class="navi-text">
                                                            <asp:Label ID="LblElioBillingDetails" runat="server" />
                                                        </span>
                                                    </a>
                                                </li>
                                                <li class="navi-item">
                                                    <a id="aStripeCreditCardDetails" runat="server" role="button" onserverclick="aStripeCreditCardDetails_ServerClick" class="navi-link">
                                                        <span class="navi-icon"><i class="flaticon2-pie-chart-2"></i></span>
                                                        <span class="navi-text">
                                                            <asp:Label ID="LblStripeCreditCardDetails" runat="server" /></span>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-9">
                                            <div class="tab-pane fade show active" id="tab_2_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_2_3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel12">
                                                    <ContentTemplate>
                                                        <div class="card-body">
                                                            <h3 class="font-size-lg text-dark font-weight-bold mb-6">1. Billing Info:</h3>
                                                            <div class="mb-15">
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Company Name:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxBillingCompanyName" CssClass="form-control" placeholder="Enter company name" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your company name</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Company Billing Address:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxBillingCompanyAddress" CssClass="form-control" placeholder="Enter company billing address (street, city, country-state, country)" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your company billing address (street, city, country-state, country)</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Company Post Code:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxBillingCompanyPostCode" CssClass="form-control" placeholder="Enter company post code" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your company post code</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Company Vat Number:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxBillingCompanyVatNumber" CssClass="form-control" placeholder="Enter company vat number" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your company company vat number</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <controls:MessageControl ID="UcBillingMessageControl" runat="server" />
                                                        </div>
                                                        <div class="card-footer">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <asp:Button ID="BtnSaveBillingDetails" OnClick="BtnSaveBillingDetails_OnClick" CssClass="btn btn-success mr-2" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="tab-pane fade" id="tab_2_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_2_3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel14">
                                                    <ContentTemplate>
                                                        <div class="card-body">
                                                            <h3 class="font-size-lg text-dark font-weight-bold mb-6">2. Credit Card Info:</h3>
                                                            <div class="mb-15">
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Credit Card Number:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxCCNumber" CssClass="form-control" placeholder="Enter Credit Card Number" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your Credit Card Number</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">CVC:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxCvcNumber" CssClass="form-control" placeholder="Enter CVC" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your CVC</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Expiration Month:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:DropDownList ID="DrpExpMonth" CssClass="form-control" placeholder="month" data-placement="top" data-trigger="manual" runat="server">
                                                                            <asp:ListItem Text="MM" Selected="True" Value="0" />
                                                                            <asp:ListItem Text="01" Value="1" />
                                                                            <asp:ListItem Text="02" Value="2" />
                                                                            <asp:ListItem Text="03" Value="3" />
                                                                            <asp:ListItem Text="04" Value="4" />
                                                                            <asp:ListItem Text="05" Value="5" />
                                                                            <asp:ListItem Text="06" Value="6" />
                                                                            <asp:ListItem Text="07" Value="7" />
                                                                            <asp:ListItem Text="08" Value="8" />
                                                                            <asp:ListItem Text="09" Value="9" />
                                                                            <asp:ListItem Text="10" Value="10" />
                                                                            <asp:ListItem Text="11" Value="11" />
                                                                            <asp:ListItem Text="12" Value="12" />
                                                                        </asp:DropDownList>
                                                                        <span class="form-text text-muted">Please enter your credit card expiration month</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Expiration Year:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxExpYear" CssClass="form-control" placeholder="YY" MaxLength="2" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your credit card expiration year(last 2 digits)</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <controls:MessageControl ID="UcCreditcardMessageControl" runat="server" />
                                                        </div>
                                                        <div class="card-footer">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <asp:Button ID="BtnSaveCreditCardDetails" Visible="false" OnClick="BtnSaveCreditCardDetails_OnClick" CssClass="btn btn-success mr-2" runat="server" />
                                                                    <asp:Button ID="BtnAddNewCard" OnClick="BtnAddNewCard_OnClick" CssClass="btn btn-primary mr-2" runat="server" />
                                                                    <asp:Button ID="BtnCancelAddNewCard" OnClick="BtnCancelAddNewCard_OnClick" CssClass="btn btn-secondary" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <!-- Payment Service form (modal view) -->
    <div id="PaymentServiceModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <controls:UcServiceStripe ID="UcServiceStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Confirmation form (modal view) -->
    <div id="ConfirmationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <controls:UcConfirmationMessageAlert ID="UcConfirmationMessageAlert" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Confirmation service form (modal view) -->
    <div id="ConfirmationServiceModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <controls:UcConfirmationServiceMessageAlert ID="UcConfirmationServiceMessageAlert" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Stripe Payment Modal -->
    <div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <controls:UcStripe ID="UcStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Change Credit Card form (modal view) -->
    <div id="CCNumberUpdateModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel6">
            <ContentTemplate>
                <controls:UcStripeCreditCard ID="UcStripeCreditCard" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <style>
            .RadGrid_MetroTouch .rgAltRow {
                background-color: transparent !important;
            }

            .RadGrid_MetroTouch .rgRow {
                background-color: transparent !important;
            }
        </style>

        <script type="text/javascript">

            function isNumberOnly(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }

            function CloseServicePaymentModal() {
                $('#PaymentServiceModal').modal('hide');
            }

            function OpenServicePaymentModal() {
                $('#PaymentServiceModal').modal('show');
            }

            function ClosePaymentModal() {
                $('#PaymentModal').modal('hide');
            }

            function OpenPaymentModal() {
                $('#PaymentModal').modal('show');
            }

            function CloseCCNumberUpdateModal() {
                $('#CCNumberUpdateModal').modal('hide');
            }

            function OpenCCNumberUpdateModal() {
                $('#CCNumberUpdateModal').modal('show');
            }

            function CloseConfirmationModal() {
                $('#ConfirmationModal').modal('hide');
            }

            function OpenConfirmationModal() {
                $('#ConfirmationModal').modal('show');
            }
            function CloseServiceConfirmationModal() {
                $('#ConfirmationServiceModal').modal('hide');
            }

            function OpenServiceConfirmationModal() {
                $('#ConfirmationServiceModal').modal('show');
            }
        </script>

    </telerik:RadScriptBlock>

</asp:Content>
