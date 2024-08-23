<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardTeamPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardTeamPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Modals/AddToTeamForm.ascx" TagName="UcAddToTeam" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>

    <telerik:RadAjaxPanel ID="RapPage" runat="server" RestoreOriginalRenderDelegate="false">
        <!--begin::Entry-->
        <div class="d-flex flex-column-fluid">
            <!--begin::Container-->
            <div class="container">
                <!--begin::Dashboard-->
                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="card-body">
                            <div class="row">
                                <div class="card-header" style="width: 100%; margin-bottom: 20px;">
                                    <div class="row" style="width: 100%;display:inline-block;">
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
                                                    <asp:Label ID="Label3" Text="Invite New User" runat="server" />
                                                </a>
                                                <!--end::Button-->
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="width: 100%;display:inline-block;">
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
                                                            <div class="d-flex justify-content-end">
                                                                <div class="dropdown dropdown-inline">
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
                                                            <div class="d-flex justify-content-end">
                                                                <div class="dropdown dropdown-inline">
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
                                                            <div class="d-flex justify-content-end">
                                                                <div class="dropdown dropdown-inline">
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
                                                            <div class="d-flex justify-content-end">
                                                                <div class="dropdown dropdown-inline">
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
                                    <!--begin::Pagination-->
                                    <div id="divPagination" runat="server" visible="false" class="d-flex justify-content-between align-items-center flex-wrap">
                                        <div class="d-flex flex-wrap mr-3">
                                            <a href="#" class="btn btn-icon btn-sm btn-light-primary mr-2 my-1">
                                                <i class="ki ki-bold-double-arrow-back icon-xs"></i>
                                            </a>
                                            <a href="#" class="btn btn-icon btn-sm btn-light-primary mr-2 my-1">
                                                <i class="ki ki-bold-arrow-back icon-xs"></i>
                                            </a>
                                            <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">...</a>
                                            <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">23</a>
                                            <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary active mr-2 my-1">24</a>
                                            <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">25</a>
                                            <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">26</a>
                                            <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">27</a>
                                            <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">28</a>
                                            <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">...</a>
                                            <a href="#" class="btn btn-icon btn-sm btn-light-primary mr-2 my-1">
                                                <i class="ki ki-bold-arrow-next icon-xs"></i>
                                            </a>
                                            <a href="#" class="btn btn-icon btn-sm btn-light-primary mr-2 my-1">
                                                <i class="ki ki-bold-double-arrow-next icon-xs"></i>
                                            </a>
                                        </div>
                                        <div class="d-flex align-items-center">
                                            <select class="form-control form-control-sm text-primary font-weight-bold mr-4 border-0 bg-light-primary" style="width: 75px;">
                                                <option value="10">10</option>
                                                <option value="20">20</option>
                                                <option value="30">30</option>
                                                <option value="50">50</option>
                                                <option value="100">100</option>
                                            </select>
                                            <span class="text-muted">Displaying 10 of 230 records</span>
                                        </div>
                                    </div>
                                    <!--end::Pagination-->
                                </div>
                                <controls:MessageAlertControl ID="UcMessageAlert" runat="server" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!--end::Dashboard-->
            </div>
            <!--end::Container-->
        </div>
        <!--end::Entry-->

        <!-- Invitation form (modal view) -->
        <div id="InvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <controls:UcAddToTeam ID="UcAddToTeamForm" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </telerik:RadAjaxPanel>

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

        </script>
        <style>
            .RadGrid_MetroTouch .rgAltRow {
                background-color: transparent !important;
                height: 365px;
            }

            .RadGrid_MetroTouch .rgRow {
                background-color: transparent !important;
                height: 365px;
            }
        </style>

    </telerik:RadScriptBlock>

</asp:Content>
