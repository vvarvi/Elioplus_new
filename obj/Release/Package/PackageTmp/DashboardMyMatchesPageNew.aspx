<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardMyMatchesPageNew.aspx.cs" Inherits="WdS.ElioPlus.DashboardMyMatchesPageNew" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">

        <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
            <ContentTemplate>
                <!--begin::Entry-->
                <div class="d-flex flex-column-fluid">
                    <!--begin::Container-->
                    <div class="container">
                        <!--begin::Dashboard-->
                        <div id="fade-class" class="card card-custom gutter-b">
                            <div class="card-body">
                                <asp:Panel ID="PnlNotRegisteredOrNoVerticals" Visible="false" runat="server">
                                    <div class="row">
                                        <h1>
                                            <asp:Label ID="LblNotRegistHeader" runat="server" />
                                            <span class="">
                                                <asp:Label ID="LblNotRegistSubHeader" runat="server" /></span>
                                        </h1>
                                    </div>
                                    <div class="alert alert-custom alert-primary" role="alert" style="margin-top: 30px; text-align: center; width: 100%;">
                                        <asp:Label ID="LblActionNotRegist" runat="server" />
                                        <a id="aFullRegist" runat="server" style="color: #ffffff;" class="alert-link">
                                            <strong>
                                                <asp:Label ID="LblActionLink" Text="  here" runat="server" />
                                            </strong>
                                        </a>
                                    </div>
                                    <div class="row" style="text-align: center; margin-left: 15%;">
                                        <div style="margin-top: 50px; display: inline-block; margin-bottom: 50px;">
                                            <div style="margin-top: 20px;">
                                                <div style="float: left; width: 220px; margin-left: -20px;">
                                                    <asp:Label ID="Label55" runat="server" Text="Your Company"></asp:Label>
                                                </div>
                                                <div style="float: left; width: 250px;">
                                                    <asp:Label ID="Label56" runat="server" Text="Partners"></asp:Label>
                                                </div>
                                                <div style="float: left">
                                                    <asp:Label ID="Label57" runat="server" Text="The results"></asp:Label>
                                                </div>
                                            </div>
                                            <div style="margin-left: 50px; float: left;">
                                                <asp:Image ID="Image10" runat="server" AlternateText="company partners" ImageUrl="/images/img1-nosub.png" />
                                            </div>
                                            <div style="float: left; margin-top: 30px; width: 160px;">
                                                <div>
                                                    <asp:Label ID="Label58" runat="server" Text="Get matched with the best partners" />
                                                </div>
                                                <div style="margin-top: 30px;">
                                                    <asp:Label ID="Label59" runat="server" Text="Find the best terms of partnership" />
                                                </div>
                                                <div style="margin-top: 30px;">
                                                    <asp:Label ID="Label60" runat="server" Text="Increase your revenues" />
                                                </div>
                                            </div>
                                            <div style="float: left; width: 90px; margin-top: 20px;">
                                                <asp:Image ID="Image11" runat="server" AlternateText="revenues increase" ImageUrl="/images/img2-nosub.png" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PnlFindPartners" Visible="false" runat="server">
                                    <div class="col-xl-6">
                                        <!--begin::Card-->
                                        <div class="card card-custom gutter-b example example-compact">
                                            <div class="card-header">
                                                <h3 class="card-title">
                                                    <asp:Label ID="LblVerticalsSelection" runat="server" /></h3>
                                            </div>
                                            <div class="card-body">
                                                <!--begin::Form-->
                                                <form action="#" class="form" role="form">
                                                    <div class="form-group row pl-2">
                                                        <div class="pt-2 mb-0">
                                                            <div class="form-group mb-0 row">
                                                                <label class="col-10 col-form-label text-right">
                                                                    <asp:Label ID="LblCriteria1" Visible="false" runat="server" />
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
                                                                    <asp:Label ID="LblCriteria2" Visible="false" runat="server" />
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
                                                                    <asp:Label ID="LblCriteria3" Visible="false" runat="server" />
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
                                                                    <asp:Label ID="LblCriteria4" Visible="false" runat="server" />
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
                                                                    <asp:Label ID="LblCriteria5" Visible="false" runat="server" />
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
                                                            <div class="form-group mb-0 row">
                                                                <label class="col-10 col-form-label text-right">
                                                                    <asp:Label ID="LblCriteria6" Visible="false" runat="server" />
                                                                </label>
                                                                <div class="col-2 text-right">
                                                                    <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                                        <label>
                                                                            <input id="CbxCrit6" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                                            <span></span>
                                                                        </label>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group mb-0 row">
                                                                <label class="col-10 col-form-label text-right">
                                                                    <asp:Label ID="LblCriteria7" Visible="false" runat="server" />
                                                                </label>
                                                                <div class="col-2 text-right">
                                                                    <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                                        <label>
                                                                            <input id="CbxCrit7" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                                            <span></span>
                                                                        </label>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <asp:HiddenField ID="HdnVert1Ckd" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HdnVert2Ckd" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HdnVert3Ckd" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HdnVert4Ckd" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HdnVert5Ckd" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HdnVert6Ckd" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HdnVert7Ckd" Value="0" runat="server" />

                                                    <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HiddenField2" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HiddenField3" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HiddenField4" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HiddenField5" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HiddenField6" Value="0" runat="server" />
                                                    <asp:HiddenField ID="HiddenField7" Value="0" runat="server" />

                                                    <div class="form-group" style="display: none;">
                                                        <asp:CheckBoxList ID="CbxSubcategories11" OnSelectedIndexChanged="CbxSubcategories_SelectedIndexChanged"
                                                            DataValueField="id" AutoPostBack="true" DataTextField="description" RepeatDirection="Vertical"
                                                            runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </form>
                                                <!--end::Form-->
                                            </div>
                                        </div>
                                        <!--end::Card-->
                                    </div>
                                    <div class="col-xl-6">
                                        <!--begin::Engage Widget 1-->
                                        <div class="card card-custom card-stretch gutter-b">
                                            <div class="card-header" style="background-color: #FFF4DE;">
                                                <h3 class="card-title">
                                                    <asp:Label ID="LblOpportunitiesViewTitle" Text="Opportunities" runat="server" /></h3>
                                            </div>
                                            <div class="card-body d-flex p-0">
                                                <div class="flex-grow-1 p-8 card-rounded bgi-no-repeat d-flex align-items-center" style="background-color: #FFF4DE; background-position: left bottom; background-size: auto 100%; background-image: url(assets/media/svg/humans/custom-2.svg)">
                                                    <div class="row">
                                                        <div class="col-5 col-xl-5" style="margin-top: -55px;">
                                                            <span class="svg-icon svg-icon-3x svg-icon-success">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Group.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <polygon points="0 0 24 0 24 24 0 24"></polygon>
                                                                        <path d="M18,14 C16.3431458,14 15,12.6568542 15,11 C15,9.34314575 16.3431458,8 18,8 C19.6568542,8 21,9.34314575 21,11 C21,12.6568542 19.6568542,14 18,14 Z M9,11 C6.790861,11 5,9.209139 5,7 C5,4.790861 6.790861,3 9,3 C11.209139,3 13,4.790861 13,7 C13,9.209139 11.209139,11 9,11 Z" fill="#000000" fill-rule="nonzero" opacity="0.3"></path>
                                                                        <path d="M17.6011961,15.0006174 C21.0077043,15.0378534 23.7891749,16.7601418 23.9984937,20.4 C24.0069246,20.5466056 23.9984937,21 23.4559499,21 L19.6,21 C19.6,18.7490654 18.8562935,16.6718327 17.6011961,15.0006174 Z M0.00065168429,20.1992055 C0.388258525,15.4265159 4.26191235,13 8.98334134,13 C13.7712164,13 17.7048837,15.2931929 17.9979143,20.2 C18.0095879,20.3954741 17.9979143,21 17.2466999,21 C13.541124,21 8.03472472,21 0.727502227,21 C0.476712155,21 -0.0204617505,20.45918 0.00065168429,20.1992055 Z" fill="#000000" fill-rule="nonzero"></path>
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                            <h4 class="text-dark font-weight-bolder">
                                                                <asp:Label ID="LblUserOpportunities" runat="server" />
                                                            </h4>
                                                            <p class="text-dark-50 my-5 font-size-xl font-weight-bold">
                                                                <asp:Label ID="LblAvaiOpportunities" runat="server" />
                                                            </p>
                                                        </div>
                                                        <div class="col-7 col-xl-7" style="width: 100%;">
                                                            <a id="aBtnGoPremium" runat="server" style="width: 90%;" class="btn btn-primary font-weight-bold py-2 px-6">
                                                                <span class="svg-icon svg-icon-md">
                                                                    <!--begin::Svg Icon | path:assets/media/svg/icons/Design/Flatten.svg-->
                                                                    <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                            <rect x="0" y="0" width="24" height="24"></rect>
                                                                            <circle fill="#000000" cx="9" cy="15" r="6"></circle>
                                                                            <path d="M8.8012943,7.00241953 C9.83837775,5.20768121 11.7781543,4 14,4 C17.3137085,4 20,6.6862915 20,10 C20,12.2218457 18.7923188,14.1616223 16.9975805,15.1987057 C16.9991904,15.1326658 17,15.0664274 17,15 C17,10.581722 13.418278,7 9,7 C8.93357256,7 8.86733422,7.00080962 8.8012943,7.00241953 Z" fill="#000000" opacity="0.3"></path>
                                                                        </g>
                                                                    </svg>
                                                                    <!--end::Svg Icon-->
                                                                </span>
                                                                <asp:Label ID="LblBtnGoPremium" Text="View Plans & Pricing" runat="server" />
                                                            </a>
                                                        </div>
                                                        <div class="col-12 col-xl-12 mt-12" style="width: 100%;">
                                                            <div class="col-12">
                                                                <asp:Label ID="LblOpportInfo" Text="These are the reseller opportunities available based on your verticals selection. If you change the options on your left you will see the number to adjust." runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end::Engage Widget 1-->

                                    </div>
                                    <div class="row">
                                        <controls:MessageControl ID="UcNoMatchesForPremiumUserNotification" Visible="false" runat="server" />
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PnlPricingPlans" runat="server">
                                    <div class="row">
                                        <div class="col-xl-12">
                                            <div class="card card-custom gutter-b">
                                                <div class="flex-grow-1 p-8 card-rounded bgi-no-repeat d-flex align-items-center" style="background-color: #FFF4DE; background-position: left bottom; background-size: auto 100%; background-image: url(/assets/media/svg/humans/custom-2.svg)">
                                                    <div class="row">
                                                        <div class="col-12 col-xl-5"></div>
                                                        <div id="divFree" runat="server" class="col-12 col-xl-7">
                                                            <h4 class="text-danger font-weight-bolder">
                                                                <asp:Label ID="LblTalkUsTitle" runat="server" />
                                                            </h4>
                                                            <p class="text-dark-50 my-5 font-size-xl font-weight-bold">
                                                                Select the plan below that fits your needs and start recruiting new partners. Choose the Partner Recruitment Database plans to source, 
                                locate and get access to partners' contact details or the Partner Recruitment Automate to let our system provide you with partners that exactly fit your needs.
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="card card-custom gutter-b">
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
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="row">
                                    <controls:MessageControl ID="UcOpportunitiesForPremiumVendors" Visible="false" runat="server" />
                                </div>
                            </div>
                            <div class="card-body">
                                <div id="divAlgorithmHolder" class="col-md-12" runat="server">
                                    <div class="col-md-12" style="">
                                        <a id="aCancelRunCriteriaSelection" visible="false" onserverclick="aCancelRunCriteriaSelection_OnClick" runat="server" class="btn btn-outline btn-rounded btn-primary">
                                            <asp:Label ID="LblCancelRunCriteriaSelection" Text="Back to My Matches" runat="server" />
                                        </a>
                                        <a id="aRunAlgBtn" runat="server" visible="false" onserverclick="RunAlgBtn_OnClick" class="btn btn-lg green" style="float: right;">
                                            <asp:Label ID="LblAlgBtnText" runat="server" />
                                            <i class="fa fa-cog"></i>
                                        </a>
                                    </div>
                                    <asp:Panel ID="PnlSelectionProcess" runat="server" Visible="false">
                                        <asp:PlaceHolder ID="PhCriteriaSelection" runat="server" />
                                    </asp:Panel>
                                    <asp:Panel ID="PnlCriteriaSelection" runat="server" Visible="false">
                                        <div class="portlet-body">
                                            <table class="table table-striped table-bordered table-advance table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <i class="fa fa-tasks" style="margin-right: 10px;"></i>
                                                            <asp:Label ID="LblCriteriaName" runat="server" />
                                                        </th>
                                                        <th class="hidden-xs">
                                                            <i class="fa fa-bars" style="margin-right: 10px;"></i>
                                                            <asp:Label ID="LblCriteriaValue" runat="server" />
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="rowVerticals" runat="server" visible="false">
                                                        <td style="min-width: 300px;">
                                                            <asp:Label ID="LblVerticals" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblVerticalsValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowFee" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblFee" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblFeeValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowRevenue" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblRevenue" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblRevenueValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowSupport" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblSupport" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblSupportValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowCompMat" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblCompMat" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCompMatValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowProgMat" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblProgMat" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblProgMatValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowNumPartn" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblNumPartn" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblNumPartnValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowTiers" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblTiers" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblTiersValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowTrain" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblTrain" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblTrainValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowFreeTrain" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblFreeTrain" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblFreeTrainValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowMarkMat" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblMarkMat" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblMarkMatValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowCert" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblCert" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCertValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowLocal" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblLocal" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblLocalValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowMdf" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblMdf" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblMdfValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowPortal" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblPortal" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblPortalValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowCountry" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCountryValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr id="rowPdf" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblPdf" runat="server" />
                                                        </td>
                                                        <td>
                                                            <a id="aPdf" runat="server">
                                                                <asp:Label ID="LblPdfValue" runat="server" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr id="rowCsv" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LblCsv" runat="server" />
                                                        </td>
                                                        <td>
                                                            <a id="aCsv" runat="server">
                                                                <asp:Label ID="LblCsvValue" runat="server" /></a>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <!--begin::Card-->
                                    <div class="card-header flex-wrap py-5" style="width: 100%;">
                                        <div class="card-title">
                                            <h3 class="card-label">
                                                <asp:Label ID="LblConnectionsTitle" runat="server" />
                                            </h3>
                                        </div>
                                        <div class="card-toolbar">
                                            <div class="row">
                                                <div class="col-6">
                                                    <!--begin::Dropdown-->
                                                    <div id="divExportAreaButton" runat="server" class="dropdown dropdown-inline mr-2">
                                                        <button type="button" class="btn btn-light-primary font-weight-bolder dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                            <span class="svg-icon svg-icon-md">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Design/PenAndRuller.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <rect x="0" y="0" width="24" height="24" />
                                                                        <path d="M3,16 L5,16 C5.55228475,16 6,15.5522847 6,15 C6,14.4477153 5.55228475,14 5,14 L3,14 L3,12 L5,12 C5.55228475,12 6,11.5522847 6,11 C6,10.4477153 5.55228475,10 5,10 L3,10 L3,8 L5,8 C5.55228475,8 6,7.55228475 6,7 C6,6.44771525 5.55228475,6 5,6 L3,6 L3,4 C3,3.44771525 3.44771525,3 4,3 L10,3 C10.5522847,3 11,3.44771525 11,4 L11,19 C11,19.5522847 10.5522847,20 10,20 L4,20 C3.44771525,20 3,19.5522847 3,19 L3,16 Z" fill="#000000" opacity="0.3" />
                                                                        <path d="M16,3 L19,3 C20.1045695,3 21,3.8954305 21,5 L21,15.2485298 C21,15.7329761 20.8241635,16.200956 20.5051534,16.565539 L17.8762883,19.5699562 C17.6944473,19.7777745 17.378566,19.7988332 17.1707477,19.6169922 C17.1540423,19.602375 17.1383289,19.5866616 17.1237117,19.5699562 L14.4948466,16.565539 C14.1758365,16.200956 14,15.7329761 14,15.2485298 L14,5 C14,3.8954305 14.8954305,3 16,3 Z" fill="#000000" />
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                            <asp:Label ID="LblExport" Text="Export" runat="server" />
                                                        </button>
                                                        <!--begin::Dropdown Menu-->
                                                        <div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">
                                                            <!--begin::Navigation-->
                                                            <ul class="navi flex-column navi-hover py-2">
                                                                <li class="navi-header font-weight-bolder text-uppercase font-size-sm text-primary pb-2">Export to:</li>

                                                                <li class="navi-item">
                                                                    <a id="aBtnExport" role="button" onserverclick="aBtnExport_ServerClick" runat="server" class="navi-link">
                                                                        <span class="navi-icon">
                                                                            <i class="la la-file-text-o"></i>
                                                                        </span>
                                                                        <span class="navi-text">
                                                                            <asp:Label ID="LblBtnExport" Text="CSV" runat="server" />
                                                                        </span>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                            <!--end::Navigation-->
                                                        </div>
                                                        <!--end::Dropdown Menu-->
                                                    </div>
                                                    <!--end::Dropdown-->
                                                </div>
                                                <div class="col-6 text-right">
                                                    <a id="aShowAlgBtn" runat="server" class="btn btn-success">
                                                        <asp:Label ID="LblShowAlgBtnText" runat="server" />
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="col-6" style="margin-bottom: 30px;">
                                            <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                                <li class="nav-item">
                                                    <a id="aSearchConnection" runat="server" onserverclick="aSearchConnection_ServerClick" class="nav-link active">
                                                        <span class="nav-icon"><i class="flaticon-search"></i></span>
                                                        <span class="nav-text">
                                                            <asp:Label ID="LblSearchConnection" Text="Search Connections" runat="server" />
                                                        </span>
                                                    </a>
                                                </li>
                                                <li class="nav-item">
                                                    <a id="aMyConneciton" runat="server" onserverclick="aMyConneciton_ServerClick" class="nav-link">
                                                        <span class="nav-icon"><i class="flaticon-customer"></i></span>
                                                        <span class="nav-text">
                                                            <asp:Label ID="LblMyConnecitons" Text="My Connections" runat="server" />
                                                        </span>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-3"></div>
                                        <div class="col-3"></div>
                                        <div class="col-12">
                                            <div class="tab-content">
                                                <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                                    <div id="divSearchConnections" style="width: 100%;" runat="server">
                                                        <!--begin::Search Form-->
                                                        <div class="mb-7">
                                                            <div class="row align-items-center">
                                                                <div class="col-lg-10">
                                                                    <div class="row align-items-center">
                                                                        <div class="col-md-4 my-2 my-md-0">
                                                                            <div class="d-flex align-items-center">
                                                                                <div class="col-md-11">
                                                                                    <asp:DropDownList CssClass="form-control" ID="DdlCountries" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:ImageButton ID="ImgBtnAddCountries" OnClick="ImgBtnAddCountries_Click" runat="server" ImageUrl="~/images/icons/add_btn_1.png" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="d-flex align-items-center mt-2">
                                                                                <div class="col-md-11">
                                                                                    <asp:TextBox ID="TbxCountries" ReadOnly="true" CssClass="form-control" runat="server" />
                                                                                    <asp:HiddenField ID="HdnCountries" Value="" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:ImageButton ID="ImgBtnRemoveCountries" OnClick="ImgBtnRemoveCountries_Click" Visible="false" runat="server" ImageUrl="~/images/icons/small/error_1.png" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4 my-2 my-md-0">
                                                                            <div class="d-flex align-items-center">
                                                                                <div class="col-md-11">
                                                                                    <asp:DropDownList ID="DdlVerticals" CssClass="form-control" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:ImageButton ID="ImgBtnAddVerticals" OnClick="ImgBtnAddVerticals_Click" runat="server" ImageUrl="~/images/icons/add_btn_1.png" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="d-flex align-items-center mt-2">
                                                                                <div class="col-md-11">
                                                                                    <asp:TextBox ID="TbxVerticals" ReadOnly="true" CssClass="form-control" runat="server" />
                                                                                    <asp:HiddenField ID="HdnVerticals" Value="" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:ImageButton ID="ImgBtnRemoveVerticals" OnClick="ImgBtnRemoveVerticals_Click" Visible="false" runat="server" ImageUrl="~/images/icons/small/error_1.png" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4 my-2 my-md-0">
                                                                            <div class="d-flex align-items-center">
                                                                                <div class="col-md-11">
                                                                                    <asp:DropDownList ID="DdlProducts" CssClass="form-control" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:ImageButton ID="ImgBtnAddProducts" OnClick="ImgBtnAddProducts_Click" runat="server" ImageUrl="~/images/icons/add_btn_1.png" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="d-flex align-items-center mt-2">
                                                                                <div class="col-md-11">
                                                                                    <asp:TextBox ID="TbxProducts" ReadOnly="true" CssClass="form-control" runat="server" />
                                                                                    <asp:HiddenField ID="HdnProducts" Value="" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:ImageButton ID="ImgBtnRemoveProducts" OnClick="ImgBtnRemoveProducts_Click" Visible="false" runat="server" ImageUrl="~/images/icons/small/error_1.png" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="col-lg-2">
                                                                    <asp:Button ID="BtnConnectionSearch" OnClick="BtnConnectionSearch_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <controls:MessageControl ID="UcSearchAlert" runat="server" />
                                                            </div>
                                                        </div>
                                                        <!--end: Search Form-->
                                                        <controls:MessageControl ID="UcSearchConnectionsMessageAlertTop" runat="server" />
                                                        <div id="divMultyActions" runat="server" visible="false" class="row">
                                                            <div class="col-md-2">
                                                                <div class="d-flex flex-grow-1 align-items-center bg-hover-light p-1 rounded" style="border: 1px solid #3699FF;">
                                                                    <div class="mr-4 flex-shrink-0 text-center">
                                                                        <a id="aBtnAddMultyConn" onserverclick="aBtnAddMultyConn_ServerClick" runat="server">
                                                                            <i class="mr-4 icon-2x text-dark-50 flaticon-user-add"></i>
                                                                            <asp:Label ID="LblAddMultyConn" Text="Add Connections" runat="server" />
                                                                        </a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="d-flex align-items-center">
                                                                    <asp:TextBox ID="TbxSearchCompanyName" CssClass="form-control" placeholder="Company name" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:DropDownList ID="DrpAnnualRange" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Value="0" Text="Select range" Selected="True" />
                                                                    <asp:ListItem Value="1" Text="$0-$1M" />
                                                                    <asp:ListItem Value="2" Text="$1M-$10M" />
                                                                    <asp:ListItem Value="3" Text="$10M-$50M" />
                                                                    <asp:ListItem Value="4" Text="$50M-$100M" />
                                                                    <asp:ListItem Value="5" Text="$100M-$250M" />
                                                                    <asp:ListItem Value="6" Text="$250M-$500M" />
                                                                    <asp:ListItem Value="7" Text="$500M-$1B" />
                                                                    <asp:ListItem Value="8" Text="$1B-$10B" />
                                                                    <asp:ListItem Value="9" Text="$10B+" />
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Button ID="BtnSearchList" OnClick="BtnSearchList_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="d-flex flex-grow-1 align-items-center bg-light p-1 rounded" style="border: 1px solid #B5B5C3;">
                                                                    <div class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <i class="icon-2x text-dark-50 flaticon2-avatar"></i>
                                                                    </div>
                                                                    <div class="text-muted">
                                                                        <asp:Label ID="LblAvailableConnCount" Text="Available connections: {0}" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--begin: Datatable-->
                                                        <div id="navBtns" visible="false" class="col-md-12" style="margin-top: 10px; margin-bottom: 15px;" runat="server">
                                                            <div class="row">
                                                                <div class="col-md-4"></div>
                                                                <div class="col-md-4" style="text-align: center;">
                                                                    <asp:Button ID="lbtnPrev" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="previous" OnClick="lbtnPrev_Click" />

                                                                    <asp:Button ID="lbtnNext" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="next" OnClick="lbtnNext_Click" />
                                                                </div>
                                                                <div class="col-md-4"></div>
                                                            </div>
                                                        </div>

                                                        <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                            <thead>
                                                                <tr>
                                                                    <th style="">
                                                                        <asp:CheckBox AutoPostBack="true" ID="CbxSelectUserAll" OnCheckedChanged="CbxSelectUserAll_CheckedChanged" runat="server" />
                                                                        <asp:Label ID="Label8" runat="server" Text="All" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="Label2" runat="server" Text="Company" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="Label3" runat="server" Text="Country" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="Label5" runat="server" Text="Industry" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="Label4" runat="server" Text="Categories" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="Label6" runat="server" Text="Products" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="Label9" Text="Annual Range" runat="server" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="Label7" Text="Actions" runat="server" />
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <asp:Repeater ID="RdgSearchConnections" OnItemDataBound="RdgSearchConnections_OnItemDataBound" runat="server">
                                                                <ItemTemplate>
                                                                    <tbody style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <div style="float: left; padding: 10px; margin-top: 10px;">
                                                                                    <asp:CheckBox ID="CbxSelectUser" runat="server" />
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                                    <div class="symbol symbol-50 symbol-light mr-2" style="float: left; padding-right: 15px;">
                                                                                        <span class="symbol-label">
                                                                                            <asp:Image ID="ImgCompanyLogo" ToolTip="View company's profile or website" AlternateText="Elioplus partner' s company logo" runat="server" class="h-50 align-self-center" alt="" />
                                                                                        </span>
                                                                                    </div>
                                                                                    <asp:Label ID="LblCompanyName" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>' runat="server" />
                                                                                    <asp:Image ID="ImgInfo" Style="cursor: pointer;" AlternateText="info" ImageUrl="~/images/icons/small/info.png" Width="20" Height="20" runat="server" />
                                                                                    <telerik:RadToolTip ID="RttImgInfo" TargetControlID="ImgInfo" Width="500" Title="Company Overview" AutoCloseDelay="10000" Text='<%# DataBinder.Eval(Container.DataItem, "Overview")%>' HideEvent="ManualClose" Position="TopRight" Animation="Fade" runat="server" />
                                                                                </a>
                                                                            </td>
                                                                            <td>
                                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                    <asp:Label ID="LblCountryR" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' runat="server" />
                                                                                </span>
                                                                            </td>
                                                                            <td>
                                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                    <asp:Label ID="LblIndustry" Text='<%# DataBinder.Eval(Container.DataItem, "Industry")%>' runat="server" />
                                                                                </span>
                                                                            </td>
                                                                            <td>
                                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                    <asp:Label ID="LblCategoryValue" Text='<%# DataBinder.Eval(Container.DataItem, "Categories")%>' runat="server" />                                                                                    
                                                                                    <i id="iMoreCategories" visible="false" runat="server" class="fa fa-chevron-circle-right" style="margin-left: 10px; font-size: 18px;"></i>
                                                                                </span>
                                                                            </td>
                                                                            <td>
                                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                    <asp:Label ID="LblProductsValue" Text='<%# DataBinder.Eval(Container.DataItem, "Products")%>' runat="server" />
                                                                                    <i id="iMoreProducts" runat="server" visible="false" class="fa fa-chevron-circle-right" style="margin-left: 10px; font-size: 18px;"></i>
                                                                                </span>
                                                                            </td>
                                                                            <td>
                                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                    <asp:Label ID="LblAnnualRevenueRange" Text='<%# DataBinder.Eval(Container.DataItem, "AnnualRevenueRange")%>' runat="server" />
                                                                                </span>
                                                                            </td>
                                                                            <td>
                                                                                <div class="row">
                                                                                    <div class="col-md-3 d-flex align-items-stretch">
                                                                                        <div class="d-flex flex-grow-1 align-items-center bg-hover-light p-1 rounded">
                                                                                            <div class="flex-shrink-0 text-center" style="width: 40px;">
                                                                                                <a id="aAdd" class="btn btn-primary" onserverclick="aAdd_ServerClick" runat="server">Add
                                                                                                </a>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                            <asp:HiddenField ID="HdnConnectionId" Value='<%# Eval("ConnectionId") %>' runat="server" />
                                                                        </tr>
                                                                    </tbody>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                        <!--end: Datatable-->
                                                        <div id="navBtnsBottom" visible="false" runat="server" class="col-md-12" style="margin-top: 10px; margin-bottom: 15px;">
                                                            <div class="row">
                                                                <div class="col-md-4"></div>
                                                                <div class="col-md-4" style="text-align: center;">
                                                                    <asp:Button ID="lbtnPrevBottom" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="previous" OnClick="lbtnPrev_Click" />

                                                                    <asp:Button ID="lbtnNextBottom" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="next" OnClick="lbtnNext_Click" />
                                                                </div>
                                                                <div class="col-md-4"></div>
                                                            </div>
                                                        </div>
                                                        <controls:MessageControl ID="UcSearchConnectionsMessageAlert" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" id="tab_1_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                                    <div id="divConnections" style="width: 100%;" runat="server">
                                                        <!--begin::Search Form-->
                                                        <div class="mb-7">
                                                            <div class="row align-items-center">
                                                                <div class="col-lg-10 col-xl-8">
                                                                    <div class="row align-items-center">
                                                                        <div class="col-md-4 my-2 my-md-0">
                                                                            <div class="d-flex align-items-center">
                                                                                <label class="mr-4 mb-0 d-none d-md-block">
                                                                                    Given Date from:
                                                                                </label>
                                                                                <telerik:RadDatePicker ID="RdpConnectionsFrom" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4 my-2 my-md-0">
                                                                            <div class="d-flex align-items-center">
                                                                                <label class="mr-4 mb-0 d-none d-md-block">
                                                                                    Given Date to:
                                                                                </label>
                                                                                <telerik:RadDatePicker ID="RdpConnectionsTo" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4 my-2 my-md-0">
                                                                            <div class="d-flex align-items-center">
                                                                                <asp:TextBox ID="RtbxCompanyName" CssClass="form-control" placeholder="Company name" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2 col-xl-4 mt-5 mt-lg-0">
                                                                    <asp:Button ID="BtnSearch" OnClick="BtnSearch_OnClick" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end: Search Form-->

                                                        <div id="navConBtns" visible="false" class="col-md-12" style="margin-top: 10px; margin-bottom: 15px;" runat="server">
                                                            <div class="row">
                                                                <div class="col-md-4"></div>
                                                                <div class="col-md-4" style="text-align: center;">
                                                                    <asp:Button ID="lbtnConPrev" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="previous" OnClick="lbtnConPrev_Click" />

                                                                    <asp:Button ID="lbtnConNext" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="next" OnClick="lbtnConNext_Click" />
                                                                </div>
                                                                <div class="col-md-4"></div>
                                                            </div>
                                                        </div>

                                                        <!--begin: Datatable-->
                                                        <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                            <thead>
                                                                <tr>
                                                                    <th style="">
                                                                        <asp:Label ID="LblConName" runat="server" Text="Company" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="LblCountry" runat="server" Text="Country" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="LblConEmail" runat="server" Text="Email" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="LblSysdate" runat="server" Text="Given Date" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="LblMore" runat="server" Text="Details" />
                                                                    </th>
                                                                    <th style="">
                                                                        <asp:Label ID="LblActions" Text="Actions" runat="server" />
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <asp:Repeater ID="RdgConnections" OnItemDataBound="RdgConnections_OnItemDataBound" runat="server">
                                                                <ItemTemplate>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 200px;">
                                                                                <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                                    <div class="symbol symbol-50 symbol-light mr-2" style="float: left; padding-right: 15px;">
                                                                                        <span class="symbol-label">
                                                                                            <asp:Image ID="ImgCompanyLogo" runat="server" class="h-50 align-self-center" alt="" />
                                                                                        </span>
                                                                                    </div>
                                                                                    <%# DataBinder.Eval(Container.DataItem, "CompanyName")%>                                                                   
                                                                                </a>
                                                                                <div id="divNotification" runat="server" visible="false" class="text-right">
                                                                                    <span id="spanNotificationMsg" class="label label-lg label-light-danger label-inline" title="New unread message" runat="server">
                                                                                        <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                                    </span>
                                                                                </div>
                                                                            </td>
                                                                            <td style="width: 200px;">
                                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                    <%# DataBinder.Eval(Container.DataItem, "Country")%>
                                                                                </span>
                                                                            </td>
                                                                            <td style="width: 260px;">
                                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                    <%# DataBinder.Eval(Container.DataItem, "CompanyEmail")%>
                                                                                </span>
                                                                            </td>
                                                                            <td style="width: 260px;">
                                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                    <%# DataBinder.Eval(Container.DataItem, "Sysdate")%>
                                                                                </span>
                                                                            </td>
                                                                            <td style="width: 150px;">
                                                                                <a id="aMoreDetails" runat="server">
                                                                                    <asp:Label ID="LblMoreDetails" runat="server" />
                                                                                </a>
                                                                            </td>
                                                                            <td style="width: 150px;">
                                                                                <asp:ImageButton ID="BtnAddOpportunity" runat="server" Style="border-radius: 5px;" ImageUrl="/images/Buttons/add-btn.png" Width="35" Height="35" ToolTip="Add to Opportunities" OnClick="BtnAddOpportunity_OnClick" />
                                                                                <asp:ImageButton ID="BtnDelete" runat="server" Style="border-radius: 5px;" ImageUrl="/images/Buttons/delete-btn.png" Width="35" Height="35" ToolTip="Delete connection" OnClick="BtnDelete_OnClick" />
                                                                            </td>
                                                                            <asp:HiddenField ID="HdnConnectionId" Value='<%# Eval("ConnectionId") %>' runat="server" />
                                                                            <asp:HiddenField ID="HdnConnectionCompanyId" Value='<%# Eval("ConnectionCompanyId") %>' runat="server" />
                                                                            <asp:HiddenField ID="HdnWebsite" Value='<%# Eval("CompanyWebsite") %>' runat="server" />
                                                                            <asp:HiddenField ID="HdnCompanyType" Value='<%# Eval("CompanyType") %>' runat="server" />
                                                                            <asp:HiddenField ID="HdnUserApplicationType" Value='<%# Eval("UserApplicationType") %>' runat="server" />
                                                                        </tr>
                                                                    </tbody>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                        <!--end: Datatable-->
                                                        <div id="navConBtnsBottom" visible="false" class="col-md-12" style="margin-top: 10px; margin-bottom: 15px;" runat="server">
                                                            <div class="row">
                                                                <div class="col-md-4"></div>
                                                                <div class="col-md-4" style="text-align: center;">
                                                                    <asp:Button ID="lbtnConPrevBottom" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="previous" OnClick="lbtnConPrev_Click" />

                                                                    <asp:Button ID="lbtnConNextBottom" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="next" OnClick="lbtnConNext_Click" />
                                                                </div>
                                                                <div class="col-md-4"></div>
                                                            </div>
                                                        </div>

                                                        <controls:MessageControl ID="UcConnectionsMessageAlert" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end::Card-->
                                </div>
                            </div>
                        </div>
                        <!--end::Dashboard-->
                    </div>
                    <!--end::Container-->
                </div>
                <!--end::Entry-->
                <div id="loader" style="display: none; vertical-align: middle; position: fixed; left: 48%; top: 42%; background-color: #ffffff; padding-top: 20px; padding-bottom: 20px; padding-left: 40px; padding-right: 40px; border: 1px solid #0d1f39; border-radius: 5px 5px 5px 5px; -moz-box-shadow: 1px 1px 10px 2px #aaa; -webkit-box-shadow: 1px 1px 10px 2px #aaa; box-shadow: 1px 1px 10px 2px #aaa; z-index: 10000;">
                    <div id="loadermsg" style="background-color: #ffffff; padding: 10px; border-radius: 5px; background-image: url(/Images/loading.gif); background-repeat: no-repeat; background-position: center center;">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
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
                                <asp:Label ID="LblConfMsg" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" data-dismiss="modal" class="btn btn-light-primary">Close</button>
                        <asp:Button ID="BtnConfDelete" OnClick="BtnConfDelete_OnClick" CssClass="btn btn-primary" runat="server" Text="Delete" />
                    </div>
                </div>
            </div>
        </div>

    </telerik:RadAjaxPanel>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script id="SetCheckedVerticals" type="text/javascript">
            function SetCheckedVerticals() {
                var HdnVert1Ckd = document.getElementById('<%= HdnVert1Ckd.ClientID%>');
                if (HdnVert1Ckd.value == "1") {
                    document.getElementById('<%= CbxCrit1.ClientID%>').checked = true;
                }

                var HdnVert2Ckd = document.getElementById('<%= HdnVert2Ckd.ClientID%>');
                if (HdnVert2Ckd.value == "1") {
                    document.getElementById('<%= CbxCrit2.ClientID%>').checked = true;
                }

                var HdnVert3Ckd = document.getElementById('<%= HdnVert3Ckd.ClientID%>');
                if (HdnVert3Ckd.value == "1") {
                    document.getElementById('<%= CbxCrit3.ClientID%>').checked = true;
                }

                var HdnVert4Ckd = document.getElementById('<%= HdnVert4Ckd.ClientID%>');
                if (HdnVert4Ckd.value == "1") {
                    document.getElementById('<%= CbxCrit4.ClientID%>').checked = true;
                }

                var HdnVert5Ckd = document.getElementById('<%= HdnVert5Ckd.ClientID%>');
                if (HdnVert5Ckd.value == "1") {
                    document.getElementById('<%= CbxCrit5.ClientID%>').checked = true;
                }

                var HdnVert6Ckd = document.getElementById('<%= HdnVert6Ckd.ClientID%>');
                if (HdnVert6Ckd.value == "1") {
                    document.getElementById('<%= CbxCrit6.ClientID%>').checked = true;
                }

                var HdnVert7Ckd = document.getElementById('<%= HdnVert7Ckd.ClientID%>');
                if (HdnVert7Ckd.value == "1") {
                    document.getElementById('<%= CbxCrit7.ClientID%>').checked = true;
                }
            }
        </script>

        <script type="text/javascript">

            function show_more_menu(e) {
                if (!confirm(`Go to ${e.target.href} ?`)) e.preventDefault();
            }

            function RapPage_OnRequestStart(sender, args) {
                $('#loader').show();
                document.getElementById("fade-class").style.opacity = "0.5";
                document.getElementById("fade-class").style.pointerEvents = "none";
            }
            function endsWith(s) {
                return this.length >= s.length && this.substr(this.length - s.length) == s;
            }
            function RapPage_OnResponseEnd(sender, args) {
                $('#loader').hide();
                document.getElementById("fade-class").style.opacity = "1";
            }

            function CloseConfirmPopUp() {
                $('#divConfirm').modal('hide');
            }

            function OpenConfirmPopUp() {
                $('#divConfirm').modal('show');
            }

        </script>

    </telerik:RadScriptBlock>

</asp:Content>
