<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardMyMatchesCriteriaSelectionPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardMyMatchesCriteriaSelectionPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
    <!--begin::Page Custom Styles(used by this page)-->
    <link href="/assets/css/pages/wizard/wizard-1.css" rel="stylesheet" type="text/css" />
    <!--end::Page Custom Styles-->
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <!--begin::Subheader-->
    <div class="subheader py-2 py-lg-4 subheader-transparent" id="kt_subheader">
        <div class="container d-flex align-items-center justify-content-between flex-wrap flex-sm-nowrap">
            <!--begin::Details-->
            <div class="d-flex align-items-center flex-wrap mr-2">
                <!--begin::Title-->
                <h5 class="text-dark font-weight-bold mt-2 mb-2 mr-5">Criteria selection of matching process</h5>
                <!--end::Title-->
                <!--begin::Separator-->
                <div class="subheader-separator subheader-separator-ver mt-2 mb-2 mr-5 bg-gray-200"></div>
                <!--end::Separator-->
                <!--begin::Search Form-->
                <div class="d-flex align-items-center" id="kt_subheader_search">
                    <span class="text-dark-50 font-weight-bold" id="kt_subheader_total">Enter your criteria and submit</span>
                </div>
                <!--end::Search Form-->
            </div>
            <!--end::Details-->
            <!--begin::Toolbar-->
            <div class="d-flex align-items-center">
                <!--begin::Button-->
                <a id="aBack" runat="server" class="btn btn-primary font-weight-bold">
                    <asp:Label ID="LblBack" runat="server" />
                </a>
                <!--end::Button-->
            </div>
            <!--end::Toolbar-->
        </div>
    </div>
    <!--end::Subheader-->

    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                <ContentTemplate>
                    <!--begin::Card-->
                    <div class="card card-custom">
                        <div class="card-body p-0">
                            <div class="wizard wizard-1" id="kt_projects_add" data-wizard-state="step-first" data-wizard-clickable="true">
                                <div class="kt-grid__item">
                                    <!--begin::Wizard Nav-->
                                    <div class="wizard-nav border-bottom">
                                        <div class="wizard-steps p-8 p-lg-10">
                                            <div id="divStateStep1" runat="server" class="wizard-step" data-wizard-type="step" data-wizard-state="current">
                                                <a id="aStepOne" runat="server" onserverclick="aStepOne_ServerClick" class="wizard-label">
                                                    <span class="svg-icon svg-icon-4x wizard-icon">
                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Chat-check.svg-->
                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                <rect x="0" y="0" width="24" height="24" />
                                                                <path d="M4.875,20.75 C4.63541667,20.75 4.39583333,20.6541667 4.20416667,20.4625 L2.2875,18.5458333 C1.90416667,18.1625 1.90416667,17.5875 2.2875,17.2041667 C2.67083333,16.8208333 3.29375,16.8208333 3.62916667,17.2041667 L4.875,18.45 L8.0375,15.2875 C8.42083333,14.9041667 8.99583333,14.9041667 9.37916667,15.2875 C9.7625,15.6708333 9.7625,16.2458333 9.37916667,16.6291667 L5.54583333,20.4625 C5.35416667,20.6541667 5.11458333,20.75 4.875,20.75 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                <path d="M2,11.8650466 L2,6 C2,4.34314575 3.34314575,3 5,3 L19,3 C20.6568542,3 22,4.34314575 22,6 L22,15 C22,15.0032706 21.9999948,15.0065399 21.9999843,15.009808 L22.0249378,15 L22.0249378,19.5857864 C22.0249378,20.1380712 21.5772226,20.5857864 21.0249378,20.5857864 C20.7597213,20.5857864 20.5053674,20.4804296 20.317831,20.2928932 L18.0249378,18 L12.9835977,18 C12.7263047,14.0909841 9.47412135,11 5.5,11 C4.23590829,11 3.04485894,11.3127315 2,11.8650466 Z M6,7 C5.44771525,7 5,7.44771525 5,8 C5,8.55228475 5.44771525,9 6,9 L15,9 C15.5522847,9 16,8.55228475 16,8 C16,7.44771525 15.5522847,7 15,7 L6,7 Z" fill="#000000" />
                                                            </g>
                                                        </svg>
                                                        <!--end::Svg Icon-->
                                                    </span>
                                                    <h3 class="wizard-title">
                                                        <asp:Label ID="LblBasicCriteria" Text="Basic criteria" runat="server" />
                                                    </h3>
                                                </a>
                                                <span class="svg-icon svg-icon-xl wizard-arrow">
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
                                            </div>
                                            <div id="divStateStep2" runat="server" class="wizard-step" data-wizard-type="step">
                                                <a id="aStepTwo" runat="server" onserverclick="aStepTwo_ServerClick" class="wizard-label">
                                                    <span class="svg-icon svg-icon-4x wizard-icon">
                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/Devices/Display1.svg-->
                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                <rect x="0" y="0" width="24" height="24" />
                                                                <path d="M11,20 L11,17 C11,16.4477153 11.4477153,16 12,16 C12.5522847,16 13,16.4477153 13,17 L13,20 L15.5,20 C15.7761424,20 16,20.2238576 16,20.5 C16,20.7761424 15.7761424,21 15.5,21 L8.5,21 C8.22385763,21 8,20.7761424 8,20.5 C8,20.2238576 8.22385763,20 8.5,20 L11,20 Z" fill="#000000" opacity="0.3" />
                                                                <path d="M3,5 L21,5 C21.5522847,5 22,5.44771525 22,6 L22,16 C22,16.5522847 21.5522847,17 21,17 L3,17 C2.44771525,17 2,16.5522847 2,16 L2,6 C2,5.44771525 2.44771525,5 3,5 Z M4.5,8 C4.22385763,8 4,8.22385763 4,8.5 C4,8.77614237 4.22385763,9 4.5,9 L13.5,9 C13.7761424,9 14,8.77614237 14,8.5 C14,8.22385763 13.7761424,8 13.5,8 L4.5,8 Z M4.5,10 C4.22385763,10 4,10.2238576 4,10.5 C4,10.7761424 4.22385763,11 4.5,11 L7.5,11 C7.77614237,11 8,10.7761424 8,10.5 C8,10.2238576 7.77614237,10 7.5,10 L4.5,10 Z" fill="#000000" />
                                                            </g>
                                                        </svg>
                                                        <!--end::Svg Icon-->
                                                    </span>
                                                    <h3 class="wizard-title">
                                                        <asp:Label ID="LblOptionalCriteria" Text="Optional criteria" runat="server" />
                                                    </h3>
                                                </a>
                                                <span class="svg-icon svg-icon-xl wizard-arrow">
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
                                            </div>
                                            <div id="divStateStep3" runat="server" class="wizard-step" data-wizard-type="step">
                                                <a id="aStepThree" runat="server" onserverclick="aStepThree_ServerClick" class="wizard-label">
                                                    <span class="svg-icon svg-icon-4x wizard-icon">
                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/Home/Globe.svg-->
                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                <rect x="0" y="0" width="24" height="24" />
                                                                <path d="M13,18.9450712 L13,20 L14,20 C15.1045695,20 16,20.8954305 16,22 L8,22 C8,20.8954305 8.8954305,20 10,20 L11,20 L11,18.9448245 C9.02872877,18.7261967 7.20827378,17.866394 5.79372555,16.5182701 L4.73856106,17.6741866 C4.36621808,18.0820826 3.73370941,18.110904 3.32581341,17.7385611 C2.9179174,17.3662181 2.88909597,16.7337094 3.26143894,16.3258134 L5.04940685,14.367122 C5.46150313,13.9156769 6.17860937,13.9363085 6.56406875,14.4106998 C7.88623094,16.037907 9.86320756,17 12,17 C15.8659932,17 19,13.8659932 19,10 C19,7.73468744 17.9175842,5.65198725 16.1214335,4.34123851 C15.6753081,4.01567657 15.5775721,3.39010038 15.903134,2.94397499 C16.228696,2.49784959 16.8542722,2.4001136 17.3003976,2.72567554 C19.6071362,4.40902808 21,7.08906798 21,10 C21,14.6325537 17.4999505,18.4476269 13,18.9450712 Z" fill="#000000" fill-rule="nonzero" />
                                                                <circle fill="#000000" opacity="0.3" cx="12" cy="10" r="6" />
                                                            </g>
                                                        </svg>
                                                        <!--end::Svg Icon-->
                                                    </span>
                                                    <h3 class="wizard-title">
                                                        <asp:Label ID="LblOptionalCriteria2" Text="Optional criteria" runat="server" />
                                                    </h3>
                                                </a>
                                                <span class="svg-icon svg-icon-xl wizard-arrow">
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
                                            </div>
                                            <div id="divStateStep4" runat="server" class="wizard-step" data-wizard-type="step">
                                                <a id="aStepFour" runat="server" onserverclick="aStepFour_ServerClick" class="wizard-label">
                                                    <span class="svg-icon svg-icon-4x wizard-icon">
                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/General/Notification2.svg-->
                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                <rect x="0" y="0" width="24" height="24" />
                                                                <path d="M13.2070325,4 C13.0721672,4.47683179 13,4.97998812 13,5.5 C13,8.53756612 15.4624339,11 18.5,11 C19.0200119,11 19.5231682,10.9278328 20,10.7929675 L20,17 C20,18.6568542 18.6568542,20 17,20 L7,20 C5.34314575,20 4,18.6568542 4,17 L4,7 C4,5.34314575 5.34314575,4 7,4 L13.2070325,4 Z" fill="#000000" />
                                                                <circle fill="#000000" opacity="0.3" cx="18.5" cy="5.5" r="2.5" />
                                                            </g>
                                                        </svg>
                                                        <!--end::Svg Icon-->
                                                    </span>
                                                    <h3 class="wizard-title">
                                                        <asp:Label ID="LblSummary" Text="Summary" runat="server" />
                                                    </h3>
                                                </a>
                                                <span class="svg-icon svg-icon-xl wizard-arrow last">
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
                                            </div>
                                        </div>
                                    </div>
                                    <!--end::Wizard Nav-->
                                </div>
                                <div class="row justify-content-center my-10 px-8 my-lg-15 px-lg-10">
                                    <div class="col-xl-12 col-xxl-10">
                                        <!--begin::Form Wizard-->
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <form class="form" id="kt_projects_add_form">
                                                    <!--begin::Step 1-->
                                                    <div id="divStep1" runat="server" class="pb-5" visible="true">
                                                        <h3 class="mb-10 font-weight-bold text-dark">Basic criteria</h3>

                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <div class="form-group row pl-2">
                                                                    <div class="pt-2 mb-0">
                                                                        <div class="form-group mb-0 row">
                                                                            <label class="col-10 col-form-label text-right">
                                                                                <asp:Label ID="LblCriteria1" runat="server" />
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
                                                                                <asp:Label ID="LblCriteria2" runat="server" />
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
                                                                                <asp:Label ID="LblCriteria3" runat="server" />
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
                                                                                <asp:Label ID="LblCriteria4" runat="server" />
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
                                                                                <asp:Label ID="LblCriteria5" runat="server" />
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
                                                                                <asp:Label ID="LblCriteria6" runat="server" />
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
                                                                                <asp:Label ID="LblCriteria7" runat="server" />
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
                                                                <span class="help-block">
                                                                    <asp:Label ID="LblVerticalsHelp" Text="Choose the products you'd like to find resellers for" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="row mt-10">
                                                            <div class="col-xl-12">
                                                                <div class="form-group row">
                                                                    <label class="col-xl-3 col-lg-3 col-form-label">
                                                                        <asp:Label ID="LblSetUpFee" Text="Set up fee *" runat="server" />
                                                                    </label>
                                                                    <div class="col-lg-6 col-xl-6">
                                                                        <asp:DropDownList ID="DrpSetUpFee" runat="server" CssClass="form-control" />
                                                                        <span class="form-text text-muted">
                                                                            <asp:Label ID="LblSetUpFeeHelp" Text="Do you require a set up fee payment from your partners?" runat="server" />
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <div class="form-group row">
                                                                    <label class="col-xl-3 col-lg-3 col-form-label">
                                                                        <asp:Label ID="LblRevenue" Text="Revenue model *" runat="server" />
                                                                    </label>
                                                                    <div class="col-lg-6 col-xl-6">
                                                                        <asp:DropDownList ID="DrpRevenue" runat="server" CssClass="form-control" />
                                                                        <span class="form-text text-muted">
                                                                            <asp:Label ID="LblRevenueHelp" Text="What type of revenue model are you using?" runat="server" />
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <div class="form-group form-group-last row">
                                                                    <div class="pt-2 mb-0">
                                                                        <div id="divIndifferent" runat="server" visible="false" class="form-group mb-0 row">
                                                                            <label class="col-10 col-form-label text-right">
                                                                                <asp:Label ID="LblIndifferent" Text="Indifferent" runat="server" />
                                                                            </label>
                                                                            <div class="col-2 text-right">
                                                                                <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                                                    <label>
                                                                                        <input id="CbxIndifferent" onclick="UpdateCheckBoxSupport()" value="0" runat="server" type="checkbox" name="select">
                                                                                        <span></span>
                                                                                    </label>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group mb-0 row">
                                                                            <label class="col-10 col-form-label text-right">
                                                                                <asp:Label ID="LblDedicated" Text="Dedicated" runat="server" />
                                                                            </label>
                                                                            <div class="col-2 text-right">
                                                                                <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                                                    <label>
                                                                                        <input id="CbxDedicated" onclick="UpdateCheckBoxSupport()" value="0" runat="server" type="checkbox" name="select">
                                                                                        <span></span>
                                                                                    </label>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group mb-0 row">
                                                                            <label class="col-10 col-form-label text-right">
                                                                                <asp:Label ID="LblPhone" Text="Phone" runat="server" />
                                                                            </label>
                                                                            <div class="col-2 text-right">
                                                                                <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                                                    <label>
                                                                                        <input id="CbxPhone" onclick="UpdateCheckBoxSupport()" value="0" runat="server" type="checkbox" name="select">
                                                                                        <span></span>
                                                                                    </label>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group mb-0 row">
                                                                            <label class="col-10 col-form-label text-right">
                                                                                <asp:Label ID="LblMail" Text="Mail" runat="server" />
                                                                            </label>
                                                                            <div class="col-2 text-right">
                                                                                <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                                                    <label>
                                                                                        <input id="CbxMail" onclick="UpdateCheckBoxSupport()" value="0" runat="server" type="checkbox" name="select">
                                                                                        <span></span>
                                                                                    </label>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <span class="form-text text-muted">
                                                                    <asp:Label ID="LblSupportHelp" Text="What type of support will you offer to your partners?" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>

                                                        <asp:HiddenField ID="HdnVert1Ckd" Value="0" runat="server" />
                                                        <asp:HiddenField ID="HdnVert2Ckd" Value="0" runat="server" />
                                                        <asp:HiddenField ID="HdnVert3Ckd" Value="0" runat="server" />
                                                        <asp:HiddenField ID="HdnVert4Ckd" Value="0" runat="server" />
                                                        <asp:HiddenField ID="HdnVert5Ckd" Value="0" runat="server" />
                                                        <asp:HiddenField ID="HdnVert6Ckd" Value="0" runat="server" />
                                                        <asp:HiddenField ID="HdnVert7Ckd" Value="0" runat="server" />

                                                    </div>
                                                    <!--end::Step 1-->
                                                    <!--begin::Step 2-->
                                                    <div id="divStep2" runat="server" visible="false" class="pb-5">
                                                        <h3 class="mb-10 font-weight-bold text-dark">Optional criteria</h3>
                                                        <div class="row">
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="LblTraining" Text="Training" runat="server" />
                                                                    </label>
                                                                    <asp:DropDownList ID="DrpTraining" runat="server" CssClass="form-control" />
                                                                    <span class="form-text text-muted">
                                                                        <asp:Label ID="LblTrainingHelp" Text="Do you offer training for your new partners?" runat="server" /></span>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="LblFreeTraining" Text=">Free training" runat="server" />
                                                                    </label>
                                                                    <asp:DropDownList ID="DrpFreeTraining" runat="server" CssClass="form-control" />
                                                                    <span class="form-text text-muted">
                                                                        <asp:Label ID="LblFreeTrainingHelp" Text="Do you offer free or paid training?" runat="server" /></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="LblProgMaturity" Text="Partner program maturity (years)" runat="server" />
                                                                    </label>
                                                                    <asp:DropDownList ID="DrpProgMaturity" runat="server" CssClass="form-control" />
                                                                    <span class="form-text text-muted">
                                                                        <asp:Label ID="LblProgMatHelp" Text="How long have you been offering your partner program?" runat="server" /></span>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="LblCompMaturity" Text="Company maturity (years)" runat="server" />
                                                                    </label>
                                                                    <asp:DropDownList ID="DrpCompMaturity" runat="server" CssClass="form-control" />
                                                                    <span class="form-text text-muted">
                                                                        <asp:Label ID="LblCompMatHelp" Text="How long is your company in operation?" runat="server" />
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="LblMarkMaterial" Text="Marketing material" runat="server" />
                                                                    </label>
                                                                    <asp:DropDownList ID="DrpMarkMater" runat="server" CssClass="form-control" />
                                                                    <span class="form-text text-muted">
                                                                        <asp:Label ID="LblMarkMaterialHelp" Text="Do you distribute marketing material to your partners?" runat="server" />
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="LblLocalization" Text="Localization" runat="server" />
                                                                    </label>
                                                                    <asp:DropDownList ID="DrpLocaliz" runat="server" CssClass="form-control" />
                                                                    <span class="form-text text-muted">
                                                                        <asp:Label ID="LblLocalizationHelp" Text="Do you offer localized version of your product to your target markets?" runat="server" /></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end::Step 2-->
                                                    <!--begin::Step 3-->
                                                    <div id="divStep3" runat="server" visible="false" class="pb-5">
                                                        <h3 class="mb-10 font-weight-bold text-dark">Optional criteria</h3>
                                                        <div class="row">
                                                            <controls:MessageControl ID="UcFilesInfo" runat="server" Visible="false" />
                                                        </div>
                                                        <div id="divUpload" runat="server" class="row mt-4">
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label2" Text="Upload a partner program brochure on your profile" runat="server" />
                                                                </div>
                                                                <div class="form-group row" id="divPdfFile" runat="server">
                                                                    <label class="col-xl-2 col-lg-2 col-form-label">
                                                                        <asp:Label ID="LblPdfTitle" runat="server" />
                                                                    </label>
                                                                    <div class="col-lg-10 col-xl-10">
                                                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                            <div class="input-group" style="">
                                                                                <div class="form-control uneditable-input input-fixed input-medium" data-trigger="fileinput" style="display: inline-table;">
                                                                                    <span class="fileinput-filename">
                                                                                        <asp:Label ID="LblExistingPdf" runat="server" />
                                                                                    </span>
                                                                                    <telerik:RadToolTip ID="RttpPdf" TargetControlID="LblExistingPdf" Position="TopCenter" runat="server" />
                                                                                </div>
                                                                                <span class="input-group-addon btn">
                                                                                    <telerik:RadAsyncUpload RenderMode="Auto" runat="server" MaxFileInputsCount="1" HideFileInput="true" AllowedFileExtensions=".pdf,application/pdf" EnableCustomValidation="true" ID="pdfFile" MultipleFileSelection="Disabled"
                                                                                        OnFileUploaded="UploadPdf_FileUploaded">
                                                                                    </telerik:RadAsyncUpload>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label1" Text="Upload the domains of your current partners to be excluded from the targeting data" runat="server" />
                                                                </div>
                                                                <div class="form-group row" id="divCsvFile" runat="server">
                                                                    <label class="col-xl-2 col-lg-2 col-form-label">
                                                                        <asp:Label ID="LblCsvTitle" runat="server" />
                                                                    </label>
                                                                    <div class="col-lg-10 col-xl-10">
                                                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                            <div class="input-group" style="">
                                                                                <div class="form-control uneditable-input input-fixed input-medium" data-trigger="fileinput" style="display: inline-table;">
                                                                                    <span class="fileinput-filename">
                                                                                        <asp:Label ID="LblExistingCsv" runat="server" />
                                                                                    </span>
                                                                                    <telerik:RadToolTip ID="RttpCsv" TargetControlID="LblExistingCsv" Position="TopCenter" runat="server" />
                                                                                </div>
                                                                                <span class="input-group-addon btn">
                                                                                    <telerik:RadAsyncUpload RenderMode="Auto" runat="server" MaxFileInputsCount="1" HideFileInput="true" AllowedFileExtensions=".csv,application/csv" EnableCustomValidation="true" ID="csvFile" MultipleFileSelection="Disabled"
                                                                                        OnFileUploaded="UploadCsv_FileUploaded">
                                                                                    </telerik:RadAsyncUpload>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="LblMDF" Text="Marketing development funds" runat="server" /></label>
                                                                    <asp:DropDownList ID="DrpMdf" runat="server" CssClass="form-control" />
                                                                    <span class="form-text text-muted">
                                                                        <asp:Label ID="LblMDFHelp" Text="Do you offer MDFs to your partners?" runat="server" /></span>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="LblCertifRequired" Text="Certification required" runat="server" /></label>
                                                                    <asp:DropDownList ID="DrpCertReq" runat="server" CssClass="form-control" />
                                                                    <span class="form-text text-muted">
                                                                        <asp:Label ID="LblCertifRequiredHelp" Text="Do your new partners need certification?" runat="server" /></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="LblPortal" Text="Partner portal/resources" runat="server" /></label>
                                                                    <asp:DropDownList ID="DrpPortal" runat="server" CssClass="form-control" />
                                                                    <span class="form-text text-muted">
                                                                        <asp:Label ID="LblPortalHelp" Text="Do you have in place a partner portal or any other type of resources for your partners?" runat="server" /></span>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="LblNumPartners" Text="Number of partners" runat="server" /></label>
                                                                    <asp:DropDownList ID="DrpNumPartners" runat="server" CssClass="form-control" />
                                                                    <span class="form-text text-muted">
                                                                        <asp:Label ID="LblNumPartnHelp" Text="How many partners do you currently have?" runat="server" /></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="LblProgTiers" Text="Partner program tiers" runat="server" /></label>
                                                                    <asp:DropDownList ID="DrpProgTiers" runat="server" CssClass="form-control" />
                                                                    <span class="form-text text-muted">
                                                                        <asp:Label ID="LblProgTiersHelp" Text="How many tiers you offer?" runat="server" /></span>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="divCountries" runat="server">
                                                            <div class="col-xl-12">
                                                                <div class="form-group form-group-last row">
                                                                    <label class="col-xl-2 col-lg-2 col-form-label">
                                                                        <asp:Label ID="LblCountries" Text="Country / Region" runat="server" />
                                                                    </label>
                                                                    <div class="col-lg-10 col-xl-9">
                                                                        <div class="form-group">
                                                                            <div class="checkbox-inline">
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesSelAll" runat="server" value="0" type="checkbox" />Select All
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesAsiaPacif" runat="server" value="0" type="checkbox" />Asia-Pacific
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesAfrica" runat="server" value="0" type="checkbox" />Africa
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesEurope" runat="server" value="0" type="checkbox" />Europe
                                                                            <span></span>
                                                                                </label>
                                                                            </div>
                                                                            <div class="checkbox-inline">
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesMidEast" runat="server" value="0" type="checkbox" />Middle East
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesNortAmer" runat="server" value="0" type="checkbox" />North America
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesSoutAmer" runat="server" value="0" type="checkbox" />South America
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesArgen" runat="server" value="0" type="checkbox" />Argentina
                                                                            <span></span>
                                                                                </label>
                                                                            </div>
                                                                            <div class="checkbox-inline">
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesAustr" runat="server" value="0" type="checkbox" />Australia
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesBraz" runat="server" value="0" type="checkbox" />Brazil
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesCanada" runat="server" value="0" type="checkbox" />Canada
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesFrance" runat="server" value="0" type="checkbox" />France
                                                                            <span></span>
                                                                                </label>
                                                                            </div>
                                                                            <div class="checkbox-inline">
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesGermany" runat="server" value="0" type="checkbox" />Germany
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesIndia" runat="server" value="0" type="checkbox" />India
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesMexico" runat="server" value="0" type="checkbox" />Mexico
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesPakistan" runat="server" value="0" type="checkbox" />Pakistan
                                                                            <span></span>
                                                                                </label>
                                                                            </div>
                                                                            <div class="checkbox-inline">
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesSpain" runat="server" value="0" type="checkbox" />Spain
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesUnKing" runat="server" value="0" type="checkbox" />United Kingdom
                                                                            <span></span>
                                                                                </label>
                                                                                <label class="checkbox" style="width: 20%;">
                                                                                    <input id="CbxCountriesUnStat" runat="server" value="0" type="checkbox" />United States
                                                                            <span></span>
                                                                                </label>
                                                                            </div>

                                                                        </div>
                                                                        <span class="help-block">
                                                                            <asp:Label ID="LblCountriesHelp" Text="" runat="server" />
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end::Step 3-->
                                                    <!--begin::Step 4-->
                                                    <div id="divStep4" runat="server" visible="false" class="pb-5">
                                                        <h4 class="mb-10 font-weight-bold">
                                                            <asp:Label ID="LblOverview" Text="Overview of your selection" runat="server" />
                                                        </h4>
                                                        <div class="col-12">
                                                            <h6 class="font-weight-bold mb-3">Step 1:</h6>
                                                            <table class="w-100">
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewVerticals" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewVerticalsValue" runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewFee" runat="server" />
                                                                    </td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewFeeValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewRevenue" runat="server" />
                                                                    </td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewRevenueValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewSupport" runat="server" />
                                                                    </td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewSupportValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <div class="separator separator-dashed my-5"></div>
                                                        <div class="col-12">
                                                            <h6 class="font-weight-bold mb-3">Step 2:</h6>
                                                            <table class="w-100">
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewTraining" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewTrainingValue" runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewFreeTraining" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewFreeTrainingValue" runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewCompMaturity" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewCompMaturityValue" runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblProgramMaturity" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblProgramMaturityValue" runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewMarkMaterial" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewMarkMaterialValue" runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewLocalization" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewLocalizationValue" runat="server" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div class="separator separator-dashed my-5"></div>
                                                        <div class="col-12">
                                                            <h6 class="font-weight-bold mb-3">Step 3:</h6>
                                                            <table class="w-100">
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewMDF" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewMDFValue" runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewCertification" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewCertificationValue" runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewPortal" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewPortalValue" runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewNumPartners" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewNumPartnersValue" runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewTiers" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewTiersValue" runat="server" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="font-weight-bold text-muted">
                                                                        <asp:Label ID="LblOverviewCountries" runat="server" /></td>
                                                                    <td class="font-weight-bold text-right">
                                                                        <asp:Label ID="LblOverviewCountriesValue" runat="server" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <!--end::Step 4-->
                                                    <div class="row" style="width: 100%;">
                                                        <controls:MessageControl ID="UcMessageControl" runat="server" Visible="false" />
                                                    </div>
                                                    <!--begin::Actions-->
                                                    <div class="d-flex justify-content-between border-top mt-5 pt-10">
                                                        <div class="mr-2">
                                                            <a id="aPrevious" runat="server" onserverclick="aPrevious_ServerClick" class="btn btn-light-primary font-weight-bold text-uppercase px-9 py-4">Previous</a>
                                                        </div>
                                                        <div>
                                                            <a id="aSubmit" runat="server" onserverclick="aSubmit_ServerClick" visible="false" class="btn btn-success font-weight-bold text-uppercase px-9 py-4">
                                                                <asp:Label ID="LblSubmit" Text="Submit" runat="server" />
                                                            </a>
                                                            <a id="aNext" runat="server" onserverclick="aNext_ServerClick" class="btn btn-primary font-weight-bold text-uppercase px-9 py-4">Next Step</a>
                                                        </div>
                                                    </div>
                                                    <!--end::Actions-->
                                                </form>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="aSubmit" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <!--end::Form Wizard-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end::Card-->
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <!--begin::Page Scripts(used by this page)-->

        <!--end::Page Scripts-->

        <script id="UpdateCheckBoxes" type="text/javascript">
            function UpdateCheckBoxes() {

                var CbxCrit1 = document.getElementById('<%= CbxCrit1.ClientID%>');
                if (CbxCrit1 != null) {
                    var HdnVert1Ckd = document.getElementById('<%= HdnVert1Ckd.ClientID%>');
                    if (HdnVert1Ckd != null) {
                        if (CbxCrit1.checked) {
                            HdnVert1Ckd.value = "1";
                        }
                        else {
                            HdnVert1Ckd.value = "0";
                        }
                    }
                }
                var CbxCrit2 = document.getElementById('<%= CbxCrit2.ClientID%>');
                if (CbxCrit2 != null) {
                    var HdnVert2Ckd = document.getElementById('<%= HdnVert2Ckd.ClientID%>');
                    if (HdnVert2Ckd != null) {
                        if (CbxCrit2.checked) {
                            HdnVert2Ckd.value = "1";
                        }
                        else {
                            HdnVert2Ckd.value = "0";
                        }
                    }
                }

                var CbxCrit3 = document.getElementById('<%= CbxCrit3.ClientID%>');
                if (CbxCrit3 != null) {
                    var HdnVert3Ckd = document.getElementById('<%= HdnVert3Ckd.ClientID%>');
                    if (HdnVert3Ckd != null) {
                        if (CbxCrit3.checked) {
                            HdnVert3Ckd.value = "1";
                        }
                        else {
                            HdnVert3Ckd.value = "0";
                        }
                    }
                }

                var CbxCrit4 = document.getElementById('<%= CbxCrit4.ClientID%>');
                if (CbxCrit4 != null) {
                    var HdnVert4Ckd = document.getElementById('<%= HdnVert4Ckd.ClientID%>');
                    if (HdnVert4Ckd != null) {
                        if (CbxCrit4.checked) {
                            HdnVert4Ckd.value = "1";
                        }
                        else {
                            HdnVert4Ckd.value = "0";
                        }
                    }
                }

                var CbxCrit5 = document.getElementById('<%= CbxCrit5.ClientID%>');
                if (CbxCrit5 != null) {
                    var HdnVert5Ckd = document.getElementById('<%= HdnVert5Ckd.ClientID%>');
                    if (HdnVert5Ckd != null) {
                        if (CbxCrit5.checked) {
                            HdnVert5Ckd.value = "1";
                        }
                        else {
                            HdnVert5Ckd.value = "0";
                        }
                    }
                }

                var CbxCrit6 = document.getElementById('<%= CbxCrit6.ClientID%>');
                if (CbxCrit6 != null) {
                    var HdnVert6Ckd = document.getElementById('<%= HdnVert6Ckd.ClientID%>');
                    if (HdnVert6Ckd != null) {
                        if (CbxCrit6.checked) {
                            HdnVert6Ckd.value = "1";
                        }
                        else {
                            HdnVert6Ckd.value = "0";
                        }
                    }
                }

                var CbxCrit7 = document.getElementById('<%= CbxCrit7.ClientID%>');
                if (CbxCrit7 != null) {
                    var HdnVert7Ckd = document.getElementById('<%= HdnVert7Ckd.ClientID%>');
                    if (HdnVert7Ckd != null) {
                        if (CbxCrit7.checked) {
                            HdnVert7Ckd.value = "1";
                        }
                        else {
                            HdnVert7Ckd.value = "0";
                        }
                    }
                }
            }
        </script>

        <script id="SetCheckedVerticals" type="text/javascript">
            function SetCheckedVerticals() {
                var HdnVert1Ckd = document.getElementById('<%= HdnVert1Ckd.ClientID%>');
                if (HdnVert1Ckd.value == "1") {
                    document.getElementById("CbxCrit1").checked = true;
                }
                var HdnVert2Ckd = document.getElementById('<%= HdnVert2Ckd.ClientID%>');
                if (HdnVert2Ckd.value == "1") {
                    document.getElementById("CbxCrit2").checked = true;
                }
                var HdnVert3Ckd = document.getElementById('<%= HdnVert3Ckd.ClientID%>');
                if (HdnVert3Ckd.value == "1") {
                    document.getElementById("CbxCrit3").checked = true;
                }
                var HdnVert4Ckd = document.getElementById('<%= HdnVert4Ckd.ClientID%>');
                if (HdnVert4Ckd.value == "1") {
                    document.getElementById("CbxCrit4").checked = true;
                }
                var HdnVert5Ckd = document.getElementById('<%= HdnVert5Ckd.ClientID%>');
                if (HdnVert5Ckd.value == "1") {
                    document.getElementById("CbxCrit5").checked = true;
                }
                var HdnVert6Ckd = document.getElementById('<%= HdnVert6Ckd.ClientID%>');
                if (HdnVert6Ckd.value == "1") {
                    document.getElementById("CbxCrit6").checked = true;
                }
                var HdnVert7Ckd = document.getElementById('<%= HdnVert7Ckd.ClientID%>');
                if (HdnVert7Ckd.value == "1") {
                    document.getElementById("CbxCrit7").checked = true;
                }
            }
        </script>

        <script id="SetCheckedSupport" type="text/javascript">

            function SetCheckedSupport() {
                var x1 = document.getElementById('<%= CbxIndifferent.ClientID%>');
                if (x1.value == "1") {
                    document.getElementById("CbxIndifferent").checked = true;
                }
                else {
                    document.getElementById("CbxIndifferent").checked = false;
                }

                var x2 = document.getElementById('<%= CbxDedicated.ClientID%>');
                if (x2.value == "1") {
                    document.getElementById("CbxDedicated").checked = true;
                }
                else {
                    document.getElementById("CbxDedicated").checked = false;
                }

                var x3 = document.getElementById('<%= CbxPhone.ClientID%>');
                if (x3.value == "1") {
                    document.getElementById("CbxPhone").checked = true;
                }
                else {
                    document.getElementById("CbxPhone").checked = false;
                }

                var x4 = document.getElementById('<%= CbxMail.ClientID%>');
                if (x4.value == "1") {
                    document.getElementById("CbxMail").checked = true;
                }
                else {
                    document.getElementById("CbxMail").checked = false;
                }
            }
        </script>

        <script id="SetCheckedCountries" type="text/javascript">
            function SetCheckedCountries() {
                var x1 = document.getElementById('<%= CbxCountriesSelAll.ClientID%>');
                if (x1.value == "1") {
                    document.getElementById("CbxCountriesSelAll").checked = true;
                }
                var x2 = document.getElementById('<%= CbxCountriesAsiaPacif.ClientID%>');
                if (x2.value == "1") {
                    document.getElementById("CbxCountriesAsiaPacif").checked = true;
                }
                var x3 = document.getElementById('<%= CbxCountriesAfrica.ClientID%>');
                if (x3.value == "1") {
                    document.getElementById("CbxCountriesAfrica").checked = true;
                }
                var x4 = document.getElementById('<%= CbxCountriesEurope.ClientID%>');
                if (x4.value == "1") {
                    document.getElementById("CbxCountriesEurope").checked = true;
                }
                var x5 = document.getElementById('<%= CbxCountriesMidEast.ClientID%>');
                if (x5.value == "1") {
                    document.getElementById("CbxCountriesMidEast").checked = true;
                }
                var x6 = document.getElementById('<%= CbxCountriesNortAmer.ClientID%>');
                if (x6.value == "1") {
                    document.getElementById("CbxCountriesNortAmer").checked = true;
                }
                var x7 = document.getElementById('<%= CbxCountriesSoutAmer.ClientID%>');
                if (x7.value == "1") {
                    document.getElementById("CbxCountriesSoutAmer").checked = true;
                }
                var x8 = document.getElementById('<%= CbxCountriesArgen.ClientID%>');
                if (x8.value == "1") {
                    document.getElementById("CbxCountriesArgen").checked = true;
                }
                var x9 = document.getElementById('<%= CbxCountriesAustr.ClientID%>');
                if (x9.value == "1") {
                    document.getElementById("CbxCountriesAustr").checked = true;
                }
                var x10 = document.getElementById('<%= CbxCountriesBraz.ClientID%>');
                if (x10.value == "1") {
                    document.getElementById("CbxCountriesBraz").checked = true;
                }
                var x11 = document.getElementById('<%= CbxCountriesCanada.ClientID%>');
                if (x11.value == "1") {
                    document.getElementById("CbxCountriesCanada").checked = true;
                }
                var x12 = document.getElementById('<%= CbxCountriesFrance.ClientID%>');
                if (x12.value == "1") {
                    document.getElementById("CbxCountriesFrance").checked = true;
                }
                var x13 = document.getElementById('<%= CbxCountriesGermany.ClientID%>');
                if (x13.value == "1") {
                    document.getElementById("CbxCountriesGermany").checked = true;
                }
                var x14 = document.getElementById('<%= CbxCountriesIndia.ClientID%>');
                if (x14.value == "1") {
                    document.getElementById("CbxCountriesIndia").checked = true;
                }
                var x15 = document.getElementById('<%= CbxCountriesMexico.ClientID%>');
                if (x15.value == "1") {
                    document.getElementById("CbxCountriesMexico").checked = true;
                }
                var x16 = document.getElementById('<%= CbxCountriesPakistan.ClientID%>');
                if (x16.value == "1") {
                    document.getElementById("CbxCountriesPakistan").checked = true;
                }
                var x17 = document.getElementById('<%= CbxCountriesSpain.ClientID%>');
                if (x17.value == "1") {
                    document.getElementById("CbxCountriesSpain").checked = true;
                }
                var x18 = document.getElementById('<%= CbxCountriesUnKing.ClientID%>');
                if (x18.value == "1") {
                    document.getElementById("CbxCountriesUnKing").checked = true;
                }
                var x19 = document.getElementById('<%= CbxCountriesUnStat.ClientID%>');
                if (x19.value == "1") {
                    document.getElementById("CbxCountriesUnStat").checked = true;
                }
            }
        </script>

        <style type="text/css">
            div.RadUpload .ruBrowse {
                height: 35px !important;
                width: 80px !important;
                vertical-align: middle !important;
            }

            div.RadUpload .ruFileWrap.ruStyled {
            }
        </style>
    </telerik:RadScriptBlock>

</asp:Content>
