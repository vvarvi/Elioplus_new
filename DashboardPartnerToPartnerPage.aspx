<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardPartnerToPartnerPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardPartnerToPartnerPage" %>

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
                                <div class="col-xl-12">
                                    <!--begin::Card-->
                                    <div class="card-body">
                                        <!--begin::Button-->
                                        <a id="aAddNewDeal" runat="server" role="button" class="btn btn-primary font-weight-bolder">
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
                                            <asp:Label ID="LblAddNewDeal" Text="Add New Deal" runat="server" />
                                        </a>
                                        <!--end::Button-->
                                    </div>
                                    <!--end::Card-->
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
                                        <li id="liOpenP2pDeals" runat="server" class="nav-item">
                                            <a id="aOpenP2pDeals" runat="server" onserverclick="aOpenP2pDeals_ServerClick" class="nav-link active">
                                                <span class="nav-icon"><i class="flaticon-buildings"></i></span>
                                                <span class="nav-text">
                                                    <asp:Label ID="LblOpenP2pDeals" Text="Open Deals" runat="server" />
                                                </span>
                                            </a>
                                        </li>
                                        <li id="liMyOpenP2pDeals" runat="server" class="nav-item">
                                            <a id="aMyOpenP2pDeals" runat="server" onserverclick="aMyOpenP2pDeals_ServerClick" class="nav-link">
                                                <span class="nav-icon"><i class="flaticon-clock-2"></i></span>
                                                <span class="nav-text">
                                                    <asp:Label ID="LblMyOpenP2pDeals" Text="My Deals" runat="server" /></span>
                                            </a>
                                        </li>
                                        <li id="liClosedDeals" runat="server" class="nav-item">
                                            <a id="aClosedDeals" runat="server" onserverclick="aClosedDeals_ServerClick" class="nav-link">
                                                <span class="nav-icon"><i class="flaticon-interface-3"></i></span>
                                                <span class="nav-text">
                                                    <asp:Label ID="LblPastDeals" Text="Past Deals" runat="server" />
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
                                                                <asp:TextBox ID="RtbxCompanyNameOpen" Width="250" EmptyMessage="find company by name or email" CssClass="form-control" runat="server" />
                                                                <span>
                                                                    <i class="flaticon2-search-1 text-muted"></i>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Button ID="BtnSearchOpen" OnClick="BtnSearchOpen_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
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
                                        <div class="table table-separate table-head-custom table-checkable" id="kt_datatable1">                                             
                                            <telerik:RadGrid ID="RdgDealsOpen" AllowPaging="true" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowSorting="false" PagerStyle-Position="Bottom"
                                                PageSize="10" CssClass="table table-separate table-head-custom table-checkable"
                                                OnNeedDataSource="RdgDealsOpen_OnNeedDataSource"
                                                OnItemDataBound="RdgDealsOpen_OnItemDataBound" AutoGenerateColumns="false"
                                                runat="server">
                                                <MasterTableView CssClass="table-status">
                                                    <Columns>
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="reseller_id" UniqueName="reseller_id" />
                                                        <telerik:GridBoundColumn HeaderText="Opportunity Name" HeaderStyle-Width="180" DataField="opportunity_name" UniqueName="opportunity_name" />
                                                        <telerik:GridTemplateColumn Display="false" DataField="is_new" UniqueName="is_new">
                                                            <ItemTemplate>
                                                                <div id="divNotification" runat="server" visible="false" class="media-right">
                                                                    <span id="spanNotificationMsg" class="badge bg-red" title="New unread message" runat="server">
                                                                        <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                    </span>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn HeaderText="Deal Value" HeaderStyle-Width="120" DataField="deal_value" UniqueName="deal_value" />
                                                        <telerik:GridTemplateColumn Display="false" HeaderText="Partner Details" UniqueName="company_more_details">
                                                            <ItemTemplate>
                                                                <a id="aMoreDetails" style="text-decoration: underline !important;" runat="server">
                                                                    <asp:Label ID="LblMoreDetails" runat="server" />
                                                                </a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn HeaderStyle-Width="120" HeaderText="Uploaded by" DataField="reseller_name" UniqueName="reseller_name" />
                                                        <telerik:GridBoundColumn Display="false" DataField="country_id" UniqueName="country_id" />
                                                        <telerik:GridBoundColumn HeaderText="Client's location" HeaderStyle-Width="140" DataField="country" UniqueName="country" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Date Created" DataField="date_created" UniqueName="date_created" />
                                                        <telerik:GridBoundColumn Display="false" DataField="last_updated" UniqueName="last_updated" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Status" DataField="status" UniqueName="status" />
                                                        <telerik:GridTemplateColumn HeaderStyle-Width="120" HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblStatus" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn Display="false" DataField="is_active" UniqueName="is_active" />
                                                        <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                        <telerik:GridTemplateColumn HeaderText="Actions" DataField="actions" UniqueName="actions">
                                                            <ItemTemplate>
                                                                <a id="aEdit" runat="server" title="Preview/Edit deal" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
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
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                        <!--end: Datatable-->
                                        <controls:MessageControl ID="UcOpenMessage" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_2_3">
                                        <!--begin::Search Form-->
                                        <div class="mb-7">
                                            <div class="row align-items-center">
                                                <div class="col-lg-9 col-xl-8">
                                                    <div class="row align-items-center">
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="input-icon">
                                                                <asp:TextBox ID="RtbxCompanyNameMyOpen" Width="250" EmptyMessage="find company by name or email" CssClass="form-control" runat="server" />
                                                                <span>
                                                                    <i class="flaticon2-search-1 text-muted"></i>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Button ID="BtnSearchMyOpen" OnClick="BtnSearchMyOpen_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-xl-4 mt-5 mt-lg-0">
                                                </div>
                                            </div>
                                        </div>
                                        <!--end: Search Form-->
                                        <!--begin: Datatable-->
                                        <div class="table table-separate table-head-custom table-checkable" id="kt_datatable2">                                            
                                            <telerik:RadGrid ID="RdgDealsMyOpen" AllowPaging="true" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowSorting="false" PagerStyle-Position="Bottom"
                                                PageSize="10" CssClass="table table-separate table-head-custom table-checkable"
                                                OnNeedDataSource="RdgDealsMyOpen_OnNeedDataSource"
                                                OnItemDataBound="RdgDealsMyOpen_OnItemDataBound" AutoGenerateColumns="false"
                                                runat="server">
                                                <MasterTableView CssClass="table-status">
                                                    <NoRecordsTemplate>
                                                        <div class="emptyGridHolder">
                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                        </div>
                                                    </NoRecordsTemplate>
                                                    <Columns>
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="reseller_id" UniqueName="reseller_id" />
                                                        <telerik:GridBoundColumn HeaderText="Opportunity Name" DataField="opportunity_name" UniqueName="opportunity_name" />
                                                        <telerik:GridBoundColumn HeaderText="Deal Value" DataField="deal_value" UniqueName="deal_value" />
                                                        <telerik:GridTemplateColumn Display="false" HeaderText="Partner Details" UniqueName="company_more_details">
                                                            <ItemTemplate>
                                                                <a id="aMoreDetails" style="text-decoration: underline !important;" runat="server">
                                                                    <asp:Label ID="LblMoreDetails" runat="server" />
                                                                </a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn Display="false" DataField="country_id" UniqueName="country_id" />
                                                        <telerik:GridBoundColumn HeaderText="Client's location" DataField="country_name" UniqueName="country_name" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Date Created" DataField="date_created" UniqueName="date_created" />
                                                        <telerik:GridBoundColumn Display="false" DataField="last_updated" UniqueName="last_updated" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Status" DataField="status" UniqueName="status" />
                                                        <telerik:GridTemplateColumn HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblStatus" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn Display="false" DataField="is_active" UniqueName="is_active" />
                                                        <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                        <telerik:GridTemplateColumn HeaderText="Actions" DataField="actions" UniqueName="actions">
                                                            <ItemTemplate>
                                                                <a id="aEdit" runat="server" title="Preview/Edit deal" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
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
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                        <!--end: Datatable-->
                                        <controls:MessageControl ID="UcMyOpenMessage" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_3" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_3_3">
                                        <!--begin::Search Form-->
                                        <div class="mb-7">
                                            <div class="row align-items-center">
                                                <div class="col-lg-9 col-xl-8">
                                                    <div class="row align-items-center">
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="input-icon">
                                                                <asp:TextBox ID="RtbxCompanyNamePast" Width="250" CssClass="form-control" runat="server" />

                                                                <span>
                                                                    <i class="flaticon2-search-1 text-muted"></i>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="d-flex align-items-center">
                                                                <asp:Button ID="BtnSearchPast" OnClick="BtnSearchPast_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
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
                                        <div class="table table-separate table-head-custom table-checkable" id="kt_datatable3">                                            
                                            <telerik:RadGrid ID="RdgPastDeals" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowPaging="true" AllowSorting="false" PagerStyle-Position="Bottom" CssClass="table table-separate table-head-custom table-checkable" PageSize="10" Width="100%" OnItemDataBound="RdgPastDeals_OnItemDataBound" OnNeedDataSource="RdgPastDeals_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                                <MasterTableView CssClass="table-status">
                                                    <Columns>
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="reseller_id" UniqueName="reseller_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="partner_user_id" UniqueName="partner_user_id" />
                                                        <telerik:GridBoundColumn HeaderText="Opportunity Name" HeaderStyle-Width="220" DataField="opportunity_name" UniqueName="opportunity_name" />
                                                        <telerik:GridBoundColumn HeaderText="Deal Value" HeaderStyle-Width="180" DataField="deal_value" UniqueName="deal_value" />
                                                        <telerik:GridBoundColumn HeaderText="Client's location" HeaderStyle-Width="200" DataField="country_name" UniqueName="country_name" />
                                                        <telerik:GridTemplateColumn HeaderText="Partner Name">
                                                            <ItemTemplate>
                                                                <td style="width: 280px;">
                                                                    <a id="aCompanyLogo" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                        <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                            <span class="symbol-label">
                                                                                <asp:Image ID="ImgCompanyLogo" runat="server" class="h-50 align-self-center" alt="" />
                                                                            </span>
                                                                        </div>
                                                                        <asp:Label ID="LblPartnerName" runat="server" />
                                                                    </a>
                                                                </td>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblStatus" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Status" DataField="status" UniqueName="status" />
                                                        <telerik:GridBoundColumn Display="false" DataField="is_active" UniqueName="is_active" />
                                                        <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                        <telerik:GridTemplateColumn HeaderText="Actions" DataField="actions" UniqueName="actions">
                                                            <ItemTemplate>
                                                                <a id="aEdit" runat="server" title="Preview/Edit deal" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
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
                                                                <a id="aDelete" visible="false" onserverclick="BtnDelete_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm">
                                                                    <span class="svg-icon svg-icon-md svg-icon-primary">
                                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/General/Trash.svg-->
                                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                <rect x="0" y="0" width="24" height="24" />
                                                                                <path d="M6,8 L6,20.5 C6,21.3284271 6.67157288,22 7.5,22 L16.5,22 C17.3284271,22 18,21.3284271 18,20.5 L18,8 L6,8 Z" fill="#000000" fill-rule="nonzero" />
                                                                                <path d="M14,4.5 L14,4 C14,3.44771525 13.5522847,3 13,3 L11,3 C10.4477153,3 10,3.44771525 10,4 L10,4.5 L5.5,4.5 C5.22385763,4.5 5,4.72385763 5,5 L5,5.5 C5,5.77614237 5.22385763,6 5.5,6 L18.5,6 C18.7761424,6 19,5.77614237 19,5.5 L19,5 C19,4.72385763 18.7761424,4.5 18.5,4.5 L14,4.5 Z" fill="#000000" opacity="0.3" />
                                                                            </g>
                                                                        </svg>
                                                                        <!--end::Svg Icon-->
                                                                    </span>
                                                                </a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
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

    <!-- Pop Up Invitation form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="LblFileUploadTitle" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <controls:MessageControl ID="UploadMessageAlert" Visible="false" runat="server" />
                                        <asp:Label ID="LblFileUploadfMsg" Visible="false" CssClass="control-label" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                        </div>
                    </div>
                </div>
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

            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }

            function OpenConfPopUp() {
                $('#divConfirm').modal('show');
            }

            function CloseConfPopUp() {
                $('#divConfirm').modal('hide');
            }
            function OpenConfPopUpVideo() {
                $('#divVideoConfirm').modal('show');
            }

            function CloseConfPopUpVideo() {
                $('#divVideoConfirm').modal('hide');
            }
        </script>

        <!--begin::Page Scripts(used by this page)-->
        <script src="assets/js/pages/custom/contacts/list-datatable.js"></script>
        <!--end::Page Scripts-->

    </telerik:RadScriptBlock>

</asp:Content>
