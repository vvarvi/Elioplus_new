<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardRandstadDealsPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardRandstadDealsPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAInfoMessageControl.ascx" TagName="InfoMessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

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
                    <div class="card card-custom">
                        <div class="card-header flex-wrap border-0 pt-6 pb-0">
                            <div class="card-title">
                                <h3 class="card-label">Deals</h3>
                            </div>

                        </div>
                        <div class="card card-custom gutter-b">
                            <div class="row">

                                <div class="col-xl-6">
                                    <!--begin::Card-->
                                    <div id="divVendorsList" class="card-body" visible="false" runat="server">
                                        <div class="row">
                                            <asp:DropDownList AutoPostBack="true" ID="DrpPartners" CssClass="form-control" Width="400" OnSelectedIndexChanged="DrpPartners_SelectedIndexChanged" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="row">
                                            <asp:Label ID="LblSelectPlan" runat="server" />
                                        </div>
                                    </div>
                                    <!--end::Card-->
                                </div>
                                <div class="col-xl-6">
                                    <div id="divVendorSettings" class="card-body" visible="false" style="" runat="server">
                                        <div class="row">
                                            <div class="col-xl-8">
                                                <asp:TextBox ID="TbxVendorDurationSetting" Width="333" Enabled="false" placeholder="-- select default deal month duration --" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                                                <asp:DropDownList ID="DrpVendorDurationSetting" CssClass="form-contol" Height="38" Width="333" Visible="false" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-xl-4">
                                                <asp:Button ID="BtnVendorDurationSetting" OnClick="BtnVendorDurationSetting_Click" Text="Edit" CssClass="btn btn-light-success" type="button" runat="server" />
                                                <asp:Button ID="BtnCancelVendorDurationSetting" Visible="false" OnClick="BtnCancelVendorDurationSetting_Click" Text="Cancel" CssClass="btn btn-light-danger" type="button" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <asp:Label ID="LblVendorDurationSettingHelp" CssClass="help-block" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xl-12">
                                    <!--begin::Card-->
                                    <div class="card-body">
                                        <!--begin::Button-->
                                        <a id="BtnAddNewDeal" runat="server" onserverclick="BtnAddNewDeal_Click" role="button" class="btn btn-primary font-weight-bolder">
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
                                            </span>Add New Deal</a>
                                        <!--end::Button-->
                                    </div>
                                    <!--end::Card-->
                                </div>

                                <div id="divDealSuccessBottom" runat="server" visible="false" class="alert alert-custom alert-notice alert-light-success fade show mb-8" role="alert">
                                    <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblDealSuccBottom" Text="Done! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblDealSuccContBottom" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                        </button>
                                    </div>
                                </div>

                                <div id="divDealErrorBottom" runat="server" visible="false" class="alert alert-custom alert-notice alert-light-danger fade show mb-8" role="alert">
                                    <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblDealErrorBottom" Text="Done! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblDealErrorContBottom" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div class="card-header card-header-tabs-line">
                                <div class="card-title">
                                    <h3 class="card-label">
                                        <span class="text-muted pt-2 font-size-sm d-block"></span>
                                    </h3>
                                </div>
                                <div class="col-xl-12"></div>
                                <div class="card-toolbar">
                                    <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                        <li id="liPendingDeals" runat="server" class="nav-item">
                                            <a id="aPendingDeals" runat="server" onserverclick="aPendingDeals_ServerClick" class="nav-link active">
                                                <span class="nav-icon"><i class="flaticon-buildings"></i></span>
                                                <span class="nav-text">
                                                    <asp:Label ID="LblPendingDeals" Text="New Deals" runat="server" />
                                                </span>
                                            </a>
                                        </li>
                                        <li id="liOpenDeals" runat="server" class="nav-item">
                                            <a id="aOpenDeals" runat="server" onserverclick="aOpenDeals_ServerClick" class="nav-link">
                                                <span class="nav-icon"><i class="flaticon-clock-2"></i></span>
                                                <span class="nav-text">
                                                    <asp:Label ID="LblOpenDeals" Text="Open Deals" runat="server" /></span>
                                            </a>
                                        </li>
                                        <li id="liClosedDeals" runat="server" class="nav-item">
                                            <a id="aClosedDeals" runat="server" onserverclick="aClosedDeals_ServerClick" class="nav-link">
                                                <span class="nav-icon"><i class="flaticon-interface-3"></i></span>
                                                <span class="nav-text">
                                                    <asp:Label ID="LblClosedDeals" Text="Past Deals" runat="server" />
                                                </span>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="tab-content">
                                    <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <!--begin::Search Form-->
                                        <div class="mb-7">
                                            <div class="row align-items-center">
                                                <div class="col-lg-9 col-xl-8">
                                                    <div class="row align-items-center">
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="input-icon">
                                                                <asp:TextBox ID="TbxPending" Width="250" placeholder="Name/Email" CssClass="form-control" runat="server" />

                                                                <span>
                                                                    <i class="flaticon2-search-1 text-muted"></i>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Button ID="BtnSearchPending" OnClick="BtnSearchPending_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
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
                                                    <th style="width: 200px;">
                                                        <asp:Label ID="Label12" runat="server" Text="Partner Name" />
                                                    </th>
                                                    <th id="tdCountry" runat="server" style="width: 240px;">
                                                        <asp:Label ID="Label13" runat="server" Text="Partner Location" />
                                                    </th>
                                                    <th style="width: 190px;">
                                                        <asp:Label ID="Label14" runat="server" Text="Client" />
                                                    </th>
                                                    <th id="tdClientEmail" runat="server" style="width: 180px;">
                                                        <asp:Label ID="Label15" runat="server" Text="Client Email" />
                                                    </th>
                                                    <th id="tdClientWebsite" runat="server" style="">
                                                        <asp:Label ID="Label16" runat="server" Text="Client Website" />
                                                    </th>
                                                    <th id="tdResultStstus" runat="server">
                                                        <asp:Label ID="LblResultStstus" runat="server" Text="Stage" />
                                                    </th>
                                                    <th style="width: 100px;">
                                                        <asp:Label ID="LblAdded" runat="server" Text="Added" />
                                                    </th>
                                                    <th id="tdActions" runat="server" style="width: 50px;">
                                                        <asp:Label ID="Label17" Text="Actions" runat="server" />
                                                    </th>
                                                    <th id="tdIsActive" class="hidden" runat="server" style="display: none;">
                                                        <asp:Label ID="LblIsActive" runat="server" Text="Deal" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <asp:Repeater ID="RdgDealsPending" OnItemDataBound="RdgDealsPending_OnItemDataBound" OnLoad="RdgDealsPending_OnNeedDataSource" runat="server">
                                                <ItemTemplate>
                                                    <tbody>
                                                        <tr>
                                                            <td style="">
                                                                <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                    <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                        <span class="symbol-label">
                                                                            <asp:Image ID="ImgLogo" runat="server" class="h-50 align-self-center" alt="" />
                                                                        </span>
                                                                    </div>
                                                                    <asp:Label ID="LblCompanyNameContent" runat="server" />
                                                                </a>
                                                                <div id="divNotification" runat="server" visible="false" class="text-left">
                                                                    <span id="spanNotificationMsg" class="label label-lg label-danger label-inline" style="color: #fff;" title="New unread message" runat="server">
                                                                        <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                    </span>
                                                                    <asp:HiddenField ID="HdnIsNew" Value='<%# DataBinder.Eval(Container.DataItem, "is_new")%>' runat="server" />
                                                                </div>
                                                                <div id="divCommentNotification" style="margin-top:-20px;" runat="server" visible="false" class="text-right">
                                                                    <span id="spanCommentNotification" class="label label-lg label-light-success" title="New unread comments" runat="server">

                                                                        <asp:Label ID="LblCommentNotificationCount" Text="" runat="server" />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="tdbCountry" runat="server" style="">
                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                    <asp:Label ID="LblCountry" runat="server" />
                                                                </span>
                                                            </td>
                                                            <td style="">
                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                    <asp:Label ID="LblClientName" Text='<%# DataBinder.Eval(Container.DataItem, "company_name")%>' runat="server" />
                                                                </span>
                                                                <div id="divClientNotification" runat="server" visible="false" class="media-right">
                                                                    <span id="spanClientNotificationMsg" class="label label-lg label-danger label-inline" style="color: #fff;" title="New open deal" runat="server">
                                                                        <asp:Label ID="LblClientNotificationMsg" Text="new!" runat="server" />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="tdbClientEmail" runat="server" style="">
                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                    <asp:Label ID="LblClientEmail" Text='<%# DataBinder.Eval(Container.DataItem, "email")%>' runat="server" />
                                                                </span>
                                                            </td>
                                                            <td id="tdbWebsite" runat="server" style="">
                                                                <a id="aWebsite" style="text-decoration: underline !important;" runat="server">
                                                                    <asp:Label ID="LblWebsite" Text='<%# DataBinder.Eval(Container.DataItem, "website")%>' runat="server" />
                                                                </a>
                                                            </td>
                                                            <td id="tdbResultStatus" runat="server" class="">
                                                                <span class="label label-lg label-light-primary label-inline">
                                                                    <asp:Label ID="LblResultStatus" Text='<%# DataBinder.Eval(Container.DataItem, "deal_result")%>' runat="server" />
                                                                </span>
                                                            </td>
                                                            <td style="">
                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                    <asp:Label ID="Label2" Text='<%# DataBinder.Eval(Container.DataItem, "sysdate")%>' runat="server" />
                                                                </span>
                                                            </td>
                                                            <td id="tdbActiveStatus" runat="server" class="text-right" style="display: none;">
                                                                <span class="label label-lg label-light-primary label-inline">
                                                                    <asp:Label ID="LblActiveStatus" Text='<%# DataBinder.Eval(Container.DataItem, "is_active")%>' runat="server" />
                                                                </span>
                                                            </td>
                                                            <td id="tdbActions" runat="server" style="width: 110px;">
                                                                <a id="aEdit" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
                                                                    <span class="svg-icon svg-icon-md svg-icon-primary">
                                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Write.svg-->
                                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                <rect x="0" y="0" width="24" height="24" />
                                                                                <path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953)" />
                                                                                <path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                            </g>
                                                                        </svg>
                                                                        <!--end::Svg Icon-->
                                                                    </span>
                                                                </a>
                                                                <a id="aDelete" runat="server" visible="false" onserverclick="BtnDelete_OnClick" title="Delete" class="btn btn-icon btn-light-danger btn-hover-danger btn-sm">
                                                                    <%--<span class="svg-icon svg-icon-md svg-icon-primary">
                                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/General/Trash.svg-->
                                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                <rect x="0" y="0" width="24" height="24" />
                                                                                <path d="M6,8 L6,20.5 C6,21.3284271 6.67157288,22 7.5,22 L16.5,22 C17.3284271,22 18,21.3284271 18,20.5 L18,8 L6,8 Z" fill="#000000" fill-rule="nonzero" />
                                                                                <path d="M14,4.5 L14,4 C14,3.44771525 13.5522847,3 13,3 L11,3 C10.4477153,3 10,3.44771525 10,4 L10,4.5 L5.5,4.5 C5.22385763,4.5 5,4.72385763 5,5 L5,5.5 C5,5.77614237 5.22385763,6 5.5,6 L18.5,6 C18.7761424,6 19,5.77614237 19,5.5 L19,5 C19,4.72385763 18.7761424,4.5 18.5,4.5 L14,4.5 Z" fill="#000000" opacity="0.3" />
                                                                            </g>
                                                                        </svg>
                                                                        <!--end::Svg Icon-->
                                                                    </span>--%>
                                                                </a>
                                                            </td>
                                                        </tr>
                                                        <asp:HiddenField ID="HdnId" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' runat="server" />
                                                    </tbody>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                        <!--end: Datatable-->
                                        <controls:MessageControl ID="UcPendingMessage" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_2_3">
                                        <!--begin::Search Form-->
                                        <div class="mb-7">
                                            <div class="row align-items-center">
                                                <div class="col-lg-9 col-xl-8">
                                                    <div class="row align-items-center">
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="input-icon">
                                                                <asp:TextBox ID="RtbxCompanyNameOpen" Width="250" placeholder="Name/Email" CssClass="form-control" runat="server" />
                                                                <span>
                                                                    <i class="flaticon2-search-1 text-muted"></i>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="d-flex align-items-center">
                                                                <asp:DropDownList ID="DdlDealResultOpen" Width="250" CssClass="form-control" runat="server" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-xl-4 mt-5 mt-lg-0">
                                                    <asp:Button ID="BtnSearchOpen" OnClick="BtnSearchOpen_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                                </div>
                                            </div>
                                        </div>
                                        <!--end: Search Form-->
                                        <!--begin: Datatable-->
                                        <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                            <thead>
                                                <tr>
                                                    <th style="width: 200px;">
                                                        <asp:Label ID="LblConName" runat="server" Text="Partner Name" />
                                                    </th>
                                                    <th style="width: 200px;">
                                                        <asp:Label ID="LblCountryHead" runat="server" Text="Partner Location" />
                                                    </th>
                                                    <th style="width: 100px;">
                                                        <asp:Label ID="LblClient" runat="server" Text="Client" />
                                                    </th>
                                                    <th style="width: 180px;">
                                                        <asp:Label ID="LblClientEmail" runat="server" Text="Client Email" />
                                                    </th>
                                                    <th style="width: 180px;">
                                                        <asp:Label ID="LblClientWebsite" runat="server" Text="Client Website" />
                                                    </th>
                                                    <th style="width: 100px;">
                                                        <asp:Label ID="Label4" runat="server" Text="Stage" />
                                                    </th>
                                                    <th style="width: 50px;">
                                                        <asp:Label ID="LblActions" Text="Actions" runat="server" />
                                                    </th>
                                                    <th style="width: 100px; display: none;" class="hidden">
                                                        <asp:Label ID="Label3" runat="server" Text="Deal" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <asp:Repeater ID="RdgDealsOpen" OnItemDataBound="RdgDealsOpen_OnItemDataBound" OnLoad="RdgDealsOpen_OnNeedDataSource" runat="server">
                                                <ItemTemplate>
                                                    <tbody>
                                                        <tr>
                                                            <td style="">
                                                                <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                    <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                        <span class="symbol-label">
                                                                            <asp:Image ID="ImgLogo" runat="server" class="h-50 align-self-center" alt="" />
                                                                        </span>
                                                                    </div>
                                                                    <asp:Label ID="LblCompanyNameContent" runat="server" />
                                                                </a>
                                                                <div id="divNotification" runat="server" visible="false" class="text-left">
                                                                    <span id="spanNotificationMsg" class="label label-lg label-danger label-inline" style="color: #fff;" title="New unread message" runat="server">
                                                                        <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                    </span>
                                                                    <asp:HiddenField ID="HdnIsNew" Value='<%# DataBinder.Eval(Container.DataItem, "is_new")%>' runat="server" />
                                                                </div>
                                                                <div id="divCommentNotification" style="margin-top:-20px;" runat="server" visible="false" class="text-right">
                                                                    <span id="spanCommentNotification" class="label label-lg label-light-success" title="New unread comments" runat="server">

                                                                        <asp:Label ID="LblCommentNotificationCount" Text="" runat="server" />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td style="">
                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                    <asp:Label ID="LblCountry" runat="server" />
                                                                </span>
                                                            </td>
                                                            <td style="">
                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                    <asp:Label ID="LblClientName" Text='<%# DataBinder.Eval(Container.DataItem, "company_name")%>' runat="server" />
                                                                </span>
                                                                <div id="divClientNotification" runat="server" visible="false" class="media-right">
                                                                    <span id="spanClientNotificationMsg" class="label label-lg label-danger label-inline" style="color: #fff;" title="New open deal" runat="server">
                                                                        <asp:Label ID="LblClientNotificationMsg" Text="new!" runat="server" />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td style="">
                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                    <asp:Label ID="LblClientEmail" Text='<%# DataBinder.Eval(Container.DataItem, "email")%>' runat="server" />
                                                                </span>
                                                            </td>
                                                            <td style="">
                                                                <a id="aWebsite" runat="server">
                                                                    <asp:Label ID="LblWebsite" Text='<%# DataBinder.Eval(Container.DataItem, "website")%>' runat="server" />
                                                                </a>
                                                            </td>
                                                            <td class="">
                                                                <span id="spanResultStatus" runat="server" class="label label-lg label-light-primary label-inline">
                                                                    <asp:Label ID="LblResultStatus" Text='<%# DataBinder.Eval(Container.DataItem, "deal_result")%>' runat="server" />
                                                                </span>
                                                            </td>
                                                            <td class="hidden" style="display: none;">
                                                                <span id="spanActiveStatus" runat="server" class="label label-lg label-light-primary label-inline">
                                                                    <asp:Label ID="LblActiveStatus" Text='<%# DataBinder.Eval(Container.DataItem, "is_active")%>' runat="server" />
                                                                </span>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <a id="aEdit" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
                                                                    <span class="svg-icon svg-icon-md svg-icon-primary">
                                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Write.svg-->
                                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                <rect x="0" y="0" width="24" height="24" />
                                                                                <path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953)" />
                                                                                <path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                            </g>
                                                                        </svg>
                                                                        <!--end::Svg Icon-->
                                                                    </span>
                                                                </a>
                                                                <a id="aDelete" runat="server" visible="false" onserverclick="BtnDelete_OnClick" title="Delete" class="btn btn-icon btn-light-danger btn-hover-danger btn-sm">
                                                                    <%--<span class="svg-icon svg-icon-md svg-icon-primary">
                                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/General/Trash.svg-->
                                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                <rect x="0" y="0" width="24" height="24" />
                                                                                <path d="M6,8 L6,20.5 C6,21.3284271 6.67157288,22 7.5,22 L16.5,22 C17.3284271,22 18,21.3284271 18,20.5 L18,8 L6,8 Z" fill="#000000" fill-rule="nonzero" />
                                                                                <path d="M14,4.5 L14,4 C14,3.44771525 13.5522847,3 13,3 L11,3 C10.4477153,3 10,3.44771525 10,4 L10,4.5 L5.5,4.5 C5.22385763,4.5 5,4.72385763 5,5 L5,5.5 C5,5.77614237 5.22385763,6 5.5,6 L18.5,6 C18.7761424,6 19,5.77614237 19,5.5 L19,5 C19,4.72385763 18.7761424,4.5 18.5,4.5 L14,4.5 Z" fill="#000000" opacity="0.3" />
                                                                            </g>
                                                                        </svg>
                                                                        <!--end::Svg Icon-->
                                                                    </span>--%>
                                                                </a>
                                                            </td>
                                                        </tr>
                                                        <asp:HiddenField ID="HdnId" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' runat="server" />
                                                    </tbody>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                        <!--end: Datatable-->
                                        <controls:MessageControl ID="UcOpenMessage" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_3" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_3_3">
                                        <!--begin::Search Form-->
                                        <div class="mb-7">
                                            <div class="row align-items-center">
                                                <div class="col-lg-9 col-xl-8">
                                                    <div class="row align-items-center">
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="input-icon">
                                                                <asp:TextBox ID="RtbxCompanyNamePast" Width="250" placeholder="Name/Email" CssClass="form-control" runat="server" />

                                                                <span>
                                                                    <i class="flaticon2-search-1 text-muted"></i>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="d-flex align-items-center">
                                                                <asp:DropDownList ID="DdlDealResultPast" CssClass="form-control" Width="250" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-xl-4 mt-5 mt-lg-0">
                                                    <asp:Button ID="BtnSearchPast" OnClick="BtnSearchPast_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                                </div>
                                            </div>
                                        </div>
                                        <!--end::Search Form-->
                                        <!--begin: Datatable-->
                                        <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                            <thead>
                                                <tr>
                                                    <th style="width: 200px;">
                                                        <asp:Label ID="Label5" runat="server" Text="Partner Name" />
                                                    </th>
                                                    <th style="width: 200px;">
                                                        <asp:Label ID="Label6" runat="server" Text="Partner Location" />
                                                    </th>
                                                    <th style="width: 100px;">
                                                        <asp:Label ID="Label7" runat="server" Text="Client" />
                                                    </th>
                                                    <th style="width: 180px;">
                                                        <asp:Label ID="Label8" runat="server" Text="Client Email" />
                                                    </th>
                                                    <th style="width: 180px;">
                                                        <asp:Label ID="Label9" runat="server" Text="Client Website" />
                                                    </th>
                                                    <th style="width: 100px;">
                                                        <asp:Label ID="Label11" runat="server" Text="Stage" />
                                                    </th>
                                                    <th style="width: 50px;">
                                                        <asp:Label ID="Label18" Text="Actions" runat="server" />
                                                    </th>
                                                    <th style="width: 100px; display: none;" class="hidden">
                                                        <asp:Label ID="Label10" runat="server" Text="Deal" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <asp:Repeater ID="RdgPastDeals" OnItemDataBound="RdgPastDeals_OnItemDataBound" OnLoad="RdgPastDeals_OnNeedDataSource" runat="server">
                                                <ItemTemplate>
                                                    <tbody>
                                                        <tr>
                                                            <td style="">
                                                                <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                    <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                        <span class="symbol-label">
                                                                            <asp:Image ID="ImgLogo" runat="server" class="h-50 align-self-center" alt="" />
                                                                        </span>
                                                                    </div>
                                                                    <asp:Label ID="LblCompanyNameContent" runat="server" />
                                                                </a>
                                                                <div id="divNotification" runat="server" visible="false" class="text-left">
                                                                    <span id="spanNotificationMsg" class="label label-lg label-danger label-inline" style="color: #fff;" title="New unread message" runat="server">
                                                                        <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                    </span>
                                                                </div>
                                                                <div id="divCommentNotification" style="margin-top:-20px;" runat="server" visible="false" class="text-right">
                                                                    <span id="spanCommentNotification" class="label label-lg label-light-success" title="New unread comments" runat="server">

                                                                        <asp:Label ID="LblCommentNotificationCount" Text="" runat="server" />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td style="">
                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                    <asp:Label ID="LblCountry" runat="server" />
                                                                </span>
                                                            </td>
                                                            <td style="">
                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                    <asp:Label ID="LblClientName" Text='<%# DataBinder.Eval(Container.DataItem, "company_name")%>' runat="server" />
                                                                </span>
                                                            </td>
                                                            <td style="">
                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                    <asp:Label ID="LblClientEmail" Text='<%# DataBinder.Eval(Container.DataItem, "email")%>' runat="server" />
                                                                </span>
                                                            </td>
                                                            <td style="">
                                                                <a id="aWebsite" style="text-decoration: underline !important;" runat="server">
                                                                    <asp:Label ID="LblWebsite" Text='<%# DataBinder.Eval(Container.DataItem, "website")%>' runat="server" />
                                                                </a>
                                                            </td>
                                                            <td class="">
                                                                <span id="spanResultStatus" runat="server" class="label label-lg label-light-primary label-inline">
                                                                    <asp:Label ID="LblResultStatus" Text='<%# DataBinder.Eval(Container.DataItem, "deal_result")%>' runat="server" />
                                                                </span>
                                                            </td>
                                                            <td class="hidden" style="display: none;">
                                                                <span id="spanActiveStatus" runat="server" class="label label-lg label-light-primary label-inline">
                                                                    <asp:Label ID="LblActiveStatus" Text='<%# DataBinder.Eval(Container.DataItem, "is_active")%>' runat="server" />
                                                                </span>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <a id="aEdit" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
                                                                    <span class="svg-icon svg-icon-md svg-icon-primary">
                                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Write.svg-->
                                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                <rect x="0" y="0" width="24" height="24" />
                                                                                <path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953)" />
                                                                                <path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                            </g>
                                                                        </svg>
                                                                        <!--end::Svg Icon-->
                                                                    </span>
                                                                </a>
                                                                <a id="aDelete" runat="server" visible="false" onserverclick="BtnDelete_OnClick" title="Delete" class="btn btn-icon btn-light-danger btn-hover-danger btn-sm">
                                                                    <%--<span class="svg-icon svg-icon-md svg-icon-primary">
                                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/General/Trash.svg-->
                                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                <rect x="0" y="0" width="24" height="24" />
                                                                                <path d="M6,8 L6,20.5 C6,21.3284271 6.67157288,22 7.5,22 L16.5,22 C17.3284271,22 18,21.3284271 18,20.5 L18,8 L6,8 Z" fill="#000000" fill-rule="nonzero" />
                                                                                <path d="M14,4.5 L14,4 C14,3.44771525 13.5522847,3 13,3 L11,3 C10.4477153,3 10,3.44771525 10,4 L10,4.5 L5.5,4.5 C5.22385763,4.5 5,4.72385763 5,5 L5,5.5 C5,5.77614237 5.22385763,6 5.5,6 L18.5,6 C18.7761424,6 19,5.77614237 19,5.5 L19,5 C19,4.72385763 18.7761424,4.5 18.5,4.5 L14,4.5 Z" fill="#000000" opacity="0.3" />
                                                                            </g>
                                                                        </svg>
                                                                        <!--end::Svg Icon-->
                                                                    </span>--%>
                                                                </a>
                                                            </td>
                                                        </tr>
                                                        <asp:HiddenField ID="HdnId" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' runat="server" />
                                                    </tbody>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                        <!--end: Datatable-->
                                        <controls:MessageControl ID="UcPastMessage" Visible="false" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <controls:MessageControl ID="UcConnectionsMessageAlert" runat="server" />
                    </div>
                    <!--end::Card-->
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

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
                                    <asp:Label ID="LblConfMsg" Text="Are you sure you want to delete this deal?" runat="server" />
                                    <asp:HiddenField ID="TbxId" Value="0" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="UcPopUpConfirmationMessageAlert" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">No</button>
                            <asp:Button ID="BtnConfDelete" OnClick="BtnConfDelete_OnClick" CssClass="btn btn-primary" Text="Yes" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">            

            function CloseConfirmPopUp() {
                $('#divConfirm').modal('hide');
            }

            function OpenConfirmPopUp() {
                $('#divConfirm').modal('show');
            }

        </script>

    </telerik:RadScriptBlock>

</asp:Content>
