<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardLeadDistributionPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardLeadDistributionPage" %>

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
                                <h3 class="card-label">Lead Distribution
										
                            <span class="text-muted pt-2 font-size-sm d-block"></span></h3>
                            </div>
                            <div class="col-xl-12">
                                <div id="divVendorsList" runat="server">
                                    <div class="row">
                                        <div class="col-xl-9">

                                            <div id="divOppStatus" runat="server" visible="false">
                                                <asp:Label ID="LblOppStatus" runat="server" />
                                                <asp:DropDownList AutoPostBack="true" ID="DrpPartners" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:Label ID="LblSelectPlan" Visible="false" runat="server" />
                                            <asp:Label ID="LblStatusHelp" Visible="false" CssClass="help-block" runat="server" />

                                        </div>
                                        <div class="col-xl-3">
                                            <!--begin::Button-->
                                            <a id="aAddNewDeal" runat="server" visible="false" class="btn btn-primary font-weight-bolder">
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
                                                <asp:Label ID="LblAddNewDeal" Text="Add New Lead" runat="server" />
                                            </a>
                                            <!--end::Button-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-toolbar">
                                <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                    <li id="liPendingDeals" runat="server" class="nav-item">
                                        <a id="aPendingDeals" runat="server" onserverclick="aPendingDeals_ServerClick" class="nav-link active">
                                            <span class="nav-icon"><i class="flaticon-buildings"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblPendingDeals" Text="New Leads" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                    <li id="liPastDeals" runat="server" class="nav-item">
                                        <a id="aPastDeals" runat="server" onserverclick="aPastDeals_ServerClick" class="nav-link">
                                            <span class="nav-icon"><i class="flaticon-interface-3"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblPastDeals" Text="Closed Leads" runat="server" /></span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <!--begin: Search Form-->
                                    <!--begin::Search Form-->
                                    <div id="divNewLeads" runat="server" visible="false" class="mb-7">
                                        <div class="row align-items-center">
                                            <div class="col-lg-9 col-xl-8">
                                                <div class="row align-items-center">
                                                    <div class="col-md-4 my-2 my-md-0">
                                                        <div class="input-icon">
                                                            <input type="text" class="form-control" placeholder="Search..." id="kt_datatable_search_query" />
                                                            <span>
                                                                <i class="flaticon2-search-1 text-muted"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 my-2 my-md-0">
                                                        <div class="d-flex align-items-center">
                                                            <label class="mr-3 mb-0 d-none d-md-block">Status:</label>
                                                            <select class="form-control" id="kt_datatable_search_status">
                                                                <option value="">All</option>
                                                                <option value="1">Pending</option>
                                                                <option value="2">Delivered</option>
                                                                <option value="3">Canceled</option>
                                                                <option value="4">Success</option>
                                                                <option value="5">Info</option>
                                                                <option value="6">Danger</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 my-2 my-md-0">
                                                        <div class="d-flex align-items-center">
                                                            <label class="mr-3 mb-0 d-none d-md-block">Type:</label>
                                                            <select class="form-control" id="kt_datatable_search_type">
                                                                <option value="">All</option>
                                                                <option value="1">Online</option>
                                                                <option value="2">Retail</option>
                                                                <option value="3">Direct</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-xl-4 mt-5 mt-lg-0">
                                                <a href="#" class="btn btn-light-primary px-6 font-weight-bold">Search</a>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end::Search Form-->
                                    <!--end: Search Form-->
                                    <!--begin: Datatable-->

                                    <telerik:RadGrid ID="RdgDealsPending" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowPaging="true" AllowSorting="false" PagerStyle-Position="Bottom" CssClass="table table-separate table-head-custom table-checkable" PageSize="10" Width="100%" OnItemDataBound="RdgDealsPending_OnItemDataBound" OnNeedDataSource="RdgDealsPending_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                        <MasterTableView CssClass="table-status">
                                            <Columns>
                                                <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                <telerik:GridBoundColumn Display="false" HeaderText="Vendor Reseller Id" DataField="collaboration_vendor_reseller_id" UniqueName="collaboration_vendor_reseller_id" />
                                                <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="vendor_id" UniqueName="vendor_id" />
                                                <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="reseller_id" UniqueName="reseller_id" />
                                                <telerik:GridTemplateColumn HeaderText="Partner Name" DataField="partner_name" UniqueName="partner_name">
                                                    <ItemTemplate>
                                                        <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                            <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                <span class="symbol-label">
                                                                    <asp:Image ID="ImgLogo" runat="server" class="h-50 align-self-center" alt="" />
                                                                </span>
                                                            </div>
                                                            <asp:Label ID="LblCompanyNameContent" runat="server" />
                                                        </a>
                                                        <div id="divNotification" runat="server" visible="false" class="text-right">
                                                            <span id="spanNotificationMsg" class="label label-lg label-danger label-inline" style="color: #fff;" title="New unread message" runat="server">
                                                                <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                            </span>
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Location" DataField="country" UniqueName="country" />
                                                <telerik:GridTemplateColumn Visible="false" HeaderText="Partner Details" UniqueName="company_more_details">
                                                    <ItemTemplate>
                                                        <a id="aMoreDetails" visible="false" runat="server">
                                                            <asp:Label ID="LblMoreDetails" runat="server" />
                                                        </a>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn Display="false" HeaderText="Client" DataField="company_name" UniqueName="company_name" />
                                                <telerik:GridTemplateColumn HeaderText="Client">
                                                    <ItemTemplate>
                                                        <div class="media-left">
                                                            <asp:Label ID="LblClientName" runat="server" />
                                                        </div>
                                                        <div id="divClientNotification" runat="server" visible="false" class="text-right">
                                                            <span id="spanClientNotificationMsg" class="label label-lg label-danger label-inline" style="color: #fff;" title="New open deal" runat="server">
                                                                <asp:Label ID="LblClientNotificationMsg" Text="new!" runat="server" />
                                                            </span>
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Client Email" DataField="email" UniqueName="email" />
                                                <telerik:GridBoundColumn Display="false" HeaderText="Client Website" DataField="website" UniqueName="website" />
                                                <telerik:GridTemplateColumn HeaderText="Client Website">
                                                    <ItemTemplate>
                                                        <a id="aWebsite" runat="server">
                                                            <asp:Label ID="LblWebsite" runat="server" />
                                                        </a>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Created" HeaderStyle-Width="80" DataField="created_date" UniqueName="created_date" />
                                                <telerik:GridBoundColumn Display="false" HeaderText="Status" DataField="status" UniqueName="status" />
                                                <telerik:GridBoundColumn Display="false" DataField="lead_result" UniqueName="lead_result" />
                                                <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                <telerik:GridBoundColumn Display="false" DataField="is_vewed_by_vendor" UniqueName="is_vewed_by_vendor" />
                                                <telerik:GridTemplateColumn Display="false" HeaderText="Deal Result">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblResultStatus" runat="server" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                <telerik:GridTemplateColumn HeaderText="Actions" DataField="actions" UniqueName="actions">
                                                    <ItemTemplate>
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
                                                        <a id="aDelete" runat="server" onserverclick="BtnDelete_OnClick" title="Delete" class="btn btn-icon btn-light-danger btn-hover-danger btn-sm">
                                                            <span class="svg-icon svg-icon-md svg-icon-primary">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/General/Trash.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <rect x="0" y="0" width="24" height="24"></rect>
                                                                        <path d="M6,8 L6,20.5 C6,21.3284271 6.67157288,22 7.5,22 L16.5,22 C17.3284271,22 18,21.3284271 18,20.5 L18,8 L6,8 Z" fill="#000000" fill-rule="nonzero"></path>
                                                                        <path d="M14,4.5 L14,4 C14,3.44771525 13.5522847,3 13,3 L11,3 C10.4477153,3 10,3.44771525 10,4 L10,4.5 L5.5,4.5 C5.22385763,4.5 5,4.72385763 5,5 L5,5.5 C5,5.77614237 5.22385763,6 5.5,6 L18.5,6 C18.7761424,6 19,5.77614237 19,5.5 L19,5 C19,4.72385763 18.7761424,4.5 18.5,4.5 L14,4.5 Z" fill="#000000" opacity="0.3"></path>
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
                                    <!--end: Datatable-->
                                    <controls:MessageControl ID="UcPendingMessage" runat="server" />
                                </div>

                                <div class="tab-pane fade" id="tab_1_2" runat="server" visible="false" role="tabpanel" aria-labelledby="kt_tab_pane_2_3">
                                    <!--begin: Search Form-->
                                    <!--begin::Search Form-->
                                    <div class="mb-7">
                                        <div class="row align-items-center">
                                            <div class="col-lg-9 col-xl-8">
                                                <div class="row align-items-center">
                                                    <div class="col-md-6 my-2 my-md-0">
                                                        <div class="input-icon">
                                                            <asp:TextBox ID="RtbxCompanyNamePast" placeholder="Name/Email" CssClass="form-control" runat="server" />

                                                            <span>
                                                                <i class="flaticon2-search-1 text-muted"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 my-2 my-md-0">
                                                        <div class="d-flex align-items-center">
                                                            <label class="mr-3 mb-0 d-none d-md-block">Status:</label>
                                                            <asp:DropDownList ID="DdlDealResultPast" CssClass="form-control" runat="server">
                                                            </asp:DropDownList>

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
                                    <!--end: Search Form-->
                                    <!--begin: Datatable-->

                                    <telerik:RadGrid ID="RdgPastDeals" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowPaging="true" AllowSorting="false" PagerStyle-Position="Bottom" CssClass="table table-separate table-head-custom table-checkable" PageSize="10" Width="100%" OnItemDataBound="RdgPastDeals_OnItemDataBound" OnNeedDataSource="RdgPastDeals_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                        <MasterTableView CssClass="table-status">
                                            <Columns>
                                                <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                <telerik:GridBoundColumn Display="false" HeaderText="Vendor Reseller Id" DataField="collaboration_vendor_reseller_id" UniqueName="collaboration_vendor_reseller_id" />
                                                <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="vendor_id" UniqueName="vendor_id" />
                                                <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="reseller_id" UniqueName="reseller_id" />
                                                <telerik:GridTemplateColumn HeaderText="Partner Name" DataField="partner_name" UniqueName="partner_name">
                                                    <ItemTemplate>
                                                        <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                            <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                <span class="symbol-label">
                                                                    <asp:Image ID="ImgLogo" runat="server" class="h-50 align-self-center" alt="" />
                                                                </span>
                                                            </div>
                                                            <asp:Label ID="LblCompanyNameContent" runat="server" />
                                                        </a>
                                                        <div id="divNotification" runat="server" visible="false" class="text-right">
                                                            <span id="spanNotificationMsg" class="label label-lg label-danger label-inline" style="color: #fff;" title="New unread message" runat="server">
                                                                <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                            </span>
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Location" DataField="country" UniqueName="country" />
                                                <telerik:GridTemplateColumn Visible="false" HeaderText="Partner Details" UniqueName="company_more_details">
                                                    <ItemTemplate>
                                                        <a id="aMoreDetails" visible="false" runat="server">
                                                            <asp:Label ID="LblMoreDetails" runat="server" />
                                                        </a>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn Display="false" HeaderText="Client" DataField="company_name" UniqueName="company_name" />
                                                <telerik:GridTemplateColumn HeaderText="Client">
                                                    <ItemTemplate>
                                                        <div class="media-left">
                                                            <asp:Label ID="LblClientName" runat="server" />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Client Email" DataField="email" UniqueName="email" />
                                                <telerik:GridBoundColumn Display="false" HeaderText="Client Website" DataField="website" UniqueName="website" />
                                                <telerik:GridTemplateColumn HeaderText="Client Website">
                                                    <ItemTemplate>
                                                        <a id="aWebsite" runat="server">
                                                            <asp:Label ID="LblWebsite" runat="server" />
                                                        </a>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn Display="false" HeaderText="Created On" DataField="created_date" UniqueName="created_date" />
                                                <telerik:GridBoundColumn Display="false" HeaderText="Status" DataField="status" UniqueName="status" />
                                                <telerik:GridBoundColumn Display="false" DataField="lead_result" UniqueName="lead_result" />
                                                <telerik:GridTemplateColumn HeaderStyle-Width="80" HeaderText="Deal Result">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblResultStatus" runat="server" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                <telerik:GridBoundColumn Display="false" DataField="is_vewed_by_vendor" UniqueName="is_vewed_by_vendor" />
                                                <telerik:GridTemplateColumn HeaderText="Actions" DataField="actions" UniqueName="actions">
                                                    <ItemTemplate>
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
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>

                                    <controls:MessageControl ID="UcPastMessage" runat="server" />
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

    <!-- Leads Service Modal -->
    <div id="LeadsServiceModal" class="modal fade" tabindex="-1" data-width="200">
        <asp:UpdatePanel runat="server" ID="UpdatePanel8">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="Label6" Text="Get Leads Service" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="divFreemiumArea" runat="server">
                                        <div class="form-group">
                                            <controls:MessageControl ID="FreemiumMessageControl" Visible="true" runat="server" />
                                        </div>
                                    </div>
                                    <div id="divPremiumArea" visible="false" runat="server">
                                        <div class="form-group">
                                            <div class="" style="float: left; width: 100%;">
                                                <table>
                                                    <tr>
                                                        <td style="width: 140px; height: 50px;">
                                                            <asp:Label ID="LblLeadsText" Text="Get leads from " CssClass="control-label" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList AutoPostBack="true" Visible="true" ID="DrpGetLeads" CssClass="form-control" Width="415" OnSelectedIndexChanged="DrpGetLeads_SelectedIndexChanged" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div id="divApiKeyArea" runat="server" visible="false" class="" style="float: left; width: 100%;">
                                                <table>
                                                    <tr>
                                                        <td style="width: 140px; height: 50px;">
                                                            <asp:Label ID="LblApiKey" Text="Type your API key" CssClass="control-label" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TbxApiKey" Width="415" CssClass="form-control" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 140px;"></td>
                                                        <td>
                                                            <asp:CheckBox ID="CbxSaveApiKey" Text=" save my API key" runat="server" />
                                                        </td>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnGetData" OnClick="BtnGetData_Click" runat="server" CssClass="btn btn-success" Text="Get Data from CRM" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

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

            function CloseConfirmPopUp() {
                $('#divConfirm').modal('hide');
            }

            function OpenConfirmPopUp() {
                $('#divConfirm').modal('show');
            }

            function OpenConfPopUpVideo() {
                $('#divVideoConfirm').modal('show');
            }

            function CloseConfPopUpVideo() {
                $('#divVideoConfirm').modal('hide');
            }

            function OpenLeadsServiceModal() {
                $('#LeadsServiceModal').modal('show');
            }

            function CloseLeadsServiceModal() {
                $('#LeadsServiceModal').modal('hide');
            }
        </script>
    </telerik:RadScriptBlock>

</asp:Content>
