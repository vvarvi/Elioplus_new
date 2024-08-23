<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardPDPartnersRequestsPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardPDPartnersRequestsPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/CreateNewInvitationToConnectionsMessage.ascx" TagName="UcCreateNewInvitationToConnectionsMessage" TagPrefix="controls" %>

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

                        <div class="card-header flex-wrap py-5">
                            <div class="card-title">
                                <h3 class="card-label">
                                    <asp:Label ID="LblTitle2Confirmed" runat="server" />
                                </h3>
                            </div>
                            <div class="card-toolbar">
                                <div id="divVendorToolsPending" runat="server" visible="false">
                                    <!--begin::Button-->
                                    <a id="aChannelPartnerInvitationPending" runat="server" class="btn btn-primary font-weight-bolder">
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
                                        <asp:Label ID="LblSendNewInvitationPending" Text="Invite partners" runat="server" />
                                    </a>
                                    <!--end::Button-->
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="" style="margin-bottom: 30px;">
                                <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                    <li class="nav-item">
                                        <a id="aSendInvitations" runat="server" onserverclick="aSendInvitations_ServerClick" class="nav-link active">
                                            <span class="nav-icon"><i class="flaticon2-paper-plane"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblSendInvitations" Text="Pending Invitations" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a id="aReceiveInvitations" runat="server" onserverclick="aReceiveInvitations_ServerClick" class="nav-link">
                                            <span class="nav-icon"><i class="flaticon2-chronometer"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblReceiveInvitations" Text="Pending Requests" runat="server" />
                                            </span>
                                            <span id="spanPendingRequests" runat="server" visible="false" class="nav-icon label label-lg label-danger ml-2">
                                                <asp:Label ID="LblPendingRequests" style="color:#fff;" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <!--begin::Search Form-->
                                    <div class="mb-7">
                                        <div id="divSearchAreaConfirmed" runat="server" class="row align-items-center">
                                            <div class="col-lg-10 col-xl-10">
                                                <div class="row align-items-center">
                                                    <div class="col-md-6 my-2 my-md-0">
                                                        <div class="d-flex align-items-center">
                                                            <asp:DropDownList ID="DdlCountriesPending" CssClass="form-control" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 my-2 my-md-0">
                                                        <div class="input-icon">
                                                            <asp:TextBox ID="RtbxCompanyNameEmailPending" placeholder="Name/Email" CssClass="form-control" runat="server" />
                                                            <span>
                                                                <i class="flaticon2-search-1 text-muted"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-xl-2 mt-5 mt-lg-0">
                                                <asp:Button ID="BtnSearchPending" OnClick="BtnSearchPending_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                            </div>
                                        </div>
                                    </div>
                                    <!--end: Search Form-->
                                    <!--begin: Datatable-->
                                    <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                        <telerik:RadGrid ID="RdgSendInvitationsPending" AllowPaging="true" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowSorting="true" PagerStyle-Position="Bottom"
                                            PageSize="10" CssClass="table table-separate table-head-custom table-checkable"
                                            OnNeedDataSource="RdgSendInvitationsPending_OnNeedDataSource"
                                            OnItemDataBound="RdgSendInvitationsPending_OnItemDataBound" AutoGenerateColumns="false"
                                            runat="server">
                                            <MasterTableView Width="100%" DataKeyNames="id" Name="Parent" AllowMultiColumnSorting="true">
                                                <NoRecordsTemplate>
                                                    <div class="emptyGridHolder">
                                                        <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                    </div>
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="10" Visible="false" HeaderText="Select"
                                                        DataField="select" UniqueName="select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CbxSelectUser" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="master_user_id" UniqueName="master_user_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="partner_user_id" UniqueName="partner_user_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="invitation_status" UniqueName="invitation_status" />
                                                    <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="100" HeaderText="Company">
                                                        <ItemTemplate>
                                                            <a id="aCompanyLogo" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                    <span class="symbol-label">
                                                                        <asp:Image ID="ImgCompanyLogo" runat="server" class="h-50 align-self-center" alt="" />
                                                                    </span>
                                                                </div>
                                                                <asp:Label ID="LblCompanyName" Text='<%# DataBinder.Eval(Container.DataItem, "company_name")%>' runat="server" />
                                                            </a>
                                                            <div id="divNotification" runat="server" visible="false" class="text-right" style="display: none;">
                                                                <span id="spanNotificationMsg" class="label label-lg label-light-danger label-inline" title="New unread message" runat="server">
                                                                    <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                </span>
                                                            </div>
                                                            <div id="divSave" visible="false" runat="server">
                                                                <telerik:RadTextBox ID="RtbxCompany" Width="220" runat="server" />
                                                                <a id="aSaveCompany" runat="server" onserverclick="aSaveCompany_OnClick">
                                                                    <i class="fa fa-save"></i>
                                                                </a>
                                                                <a id="aCancelCompany" runat="server" onserverclick="aCancelCompany_OnClick">
                                                                    <i class="fa fa-remove"></i>
                                                                </a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn Display="false" HeaderStyle-Width="60" HeaderText="Company Name" DataField="company_name"
                                                        UniqueName="company_name" />
                                                    <telerik:GridBoundColumn HeaderStyle-Width="70" HeaderText="Email" DataField="email"
                                                        UniqueName="email" />
                                                    <telerik:GridBoundColumn Display="false" HeaderStyle-Width="60" HeaderText="Country" DataField="country"
                                                        UniqueName="country" />
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="50" HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblStatus" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Invitation" HeaderStyle-Width="70" UniqueName="company_more_details">
                                                        <ItemTemplate>
                                                            <a id="aSendEmail" class="label label-lg label-primary label-inline" style="color: #fff;" onserverclick="ImgBtnSendEmailPending_OnClick" runat="server">
                                                                <i class="fa fa-send"></i>
                                                                <asp:Label ID="LbSendEmail" Text="Re-send" runat="server" />
                                                            </a>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn Display="false" HeaderText="More Details" HeaderStyle-Width="50" UniqueName="company_more_details">
                                                        <ItemTemplate>
                                                            <a id="aMoreDetails" class="label label-lg label-primary label-inline" style="color: #fff;" runat="server">
                                                                <asp:Label ID="LblMoreDetails" Text="Manage Account" runat="server" />
                                                            </a>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="40" HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <a id="aDeletePending" onserverclick="aDeletePending_OnClick" runat="server" class="btn btn-icon btn-light-danger btn-hover-danger btn-sm">
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
                                    <controls:MessageControl ID="UcSendMessageAlertPending" runat="server" />
                                </div>
                                <div class="tab-pane fade" id="tab_1_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <!--begin::Search Form-->
                                    <div class="mb-7">
                                        <div id="divSearchAreaPendingRequests" runat="server" class="row align-items-center">
                                            <div class="col-lg-10 col-xl-10">
                                                <div class="row align-items-center">
                                                    <div class="col-md-6 my-2 my-md-0">
                                                        <div class="d-flex align-items-center">
                                                            <asp:DropDownList ID="DdlCountriesPendingRequests" CssClass="form-control" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 my-2 my-md-0">
                                                        <div class="input-icon">
                                                            <asp:TextBox ID="RtbxCompanyNameEmailPendingRequests" placeholder="Name/Email" CssClass="form-control" runat="server" />
                                                            <span>
                                                                <i class="flaticon2-search-1 text-muted"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-xl-2 mt-5 mt-lg-0">
                                                <asp:Button ID="BtnSearchPendingRequests" OnClick="BtnSearchPendingRequests_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                            </div>
                                        </div>
                                    </div>
                                    <!--end: Search Form-->
                                    <!--begin: Datatable-->
                                    <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                        <telerik:RadGrid ID="RdgReceivedInvitations"
                                            AllowPaging="true" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowSorting="true" PagerStyle-Position="Bottom"
                                            PageSize="10" CssClass="table table-separate table-head-custom table-checkable"
                                            OnNeedDataSource="RdgReceivedInvitations_OnNeedDataSource"
                                            OnItemDataBound="RdgReceivedInvitations_OnItemDataBound" AutoGenerateColumns="false"
                                            runat="server">
                                            <MasterTableView Width="100%" DataKeyNames="id" Name="Parent" AllowMultiColumnSorting="true">
                                                <NoRecordsTemplate>
                                                    <div class="emptyGridHolder">
                                                        <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                    </div>
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="50" HeaderText="Select" DataField="select"
                                                        UniqueName="select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CbxSelectUser" runat="server" />
                                                            <asp:ImageButton ID="ImgNotAvailable" Visible="false" ImageUrl="~/images/cancel.png" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="master_user_id" UniqueName="master_user_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="partner_user_id" UniqueName="partner_user_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="invitation_status" UniqueName="invitation_status" />
                                                    <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="120" HeaderText="Company">
                                                        <ItemTemplate>
                                                            <a id="aCompanyLogo" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                    <span class="symbol-label">
                                                                        <asp:Image ID="ImgCompanyLogo" runat="server" class="h-50 align-self-center" alt="" />
                                                                    </span>
                                                                </div>
                                                                <asp:Label ID="LblCompanyName" Text='<%# DataBinder.Eval(Container.DataItem, "company_name")%>' runat="server" />
                                                            </a>
                                                            <div id="divNotification" runat="server" visible="false" class="text-right" style="display: none;">
                                                                <span id="spanNotificationMsg" class="label label-lg label-light-danger label-inline" title="New unread message" runat="server">
                                                                    <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                </span>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn Display="false" HeaderStyle-Width="150" HeaderText="Company Name" DataField="company_name"
                                                        UniqueName="company_name" />
                                                    <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Website" DataField="website"
                                                        UniqueName="website" />
                                                    <telerik:GridBoundColumn HeaderStyle-Width="150" HeaderText="Email" DataField="email"
                                                        UniqueName="email" />
                                                    <telerik:GridBoundColumn HeaderStyle-Width="60" HeaderText="Country" DataField="country"
                                                        UniqueName="country" />
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="50" HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblStatus" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="More Details" HeaderStyle-Width="100" UniqueName="company_more_details">
                                                        <ItemTemplate>
                                                            <a id="aMoreDetails" class="label label-lg label-primary label-inline" style="color: #fff;" runat="server">
                                                                <asp:Label ID="LblMoreDetails" Text="Manage Account" runat="server" />
                                                            </a>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="50" HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <a id="aDeleteReceive" onserverclick="aDeleteReceive_OnClick" runat="server" class="btn btn-icon btn-light-danger btn-hover-danger btn-sm">
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
                                                            <a id="aGoFullRegister" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm">
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
                                    <controls:MessageControl ID="UcReceiveMessageAlert" runat="server" />

                                    <div id="divActions" class="" style="text-align: center; margin-bottom: 30px; margin-top: 30px; padding: 5px;"
                                        runat="server">
                                        <asp:Button ID="RbtnAccept" OnClick="RbtnAccept_OnClick" Text="Accept"
                                            CssClass="btn btn-sm btn-light-success" runat="server" />
                                        <asp:Button ID="RbtnPending" Visible="false" OnClick="RbtnPending_OnClick" Text="Pending"
                                            CssClass="btn btn-sm btn-light-warning" runat="server" />
                                        <asp:Button ID="RbtnReject" Visible="false" OnClick="RbtnReject_OnClick" Text="Reject"
                                            CssClass="btn btn-sm btn-light-danger" runat="server" />
                                        <asp:Button ID="RbtnDelete" Visible="false" OnClick="BtnDelete_OnClick" Text="Delete"
                                            CssClass="btn btn-sm btn-danger" runat="server" />
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

    <!-- Invitation form (modal view) -->
    <div id="SendInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblInvitationSendTitle" Text="Invitation Send" CssClass="control-label" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <controls:MessageControl ID="UcMessageSuccessSend" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Invitation form (modal view) -->
    <div id="CreateInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <controls:UcCreateNewInvitationToConnectionsMessage ID="UcCreateNewInvitationToConnectionsMessage"
                    runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Confirmation Delete Partner form (modal view) -->
    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblConfTitle" Text="Confirmation" CssClass="control-label" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="LblConfMsg" Text="Are you sure you want to delete this partner from your list?" CssClass="control-label" runat="server" />
                                    <asp:HiddenField ID="HdnVendorResellerCollaborationId" Value="0" runat="server" />
                                </div>
                            </div>
                            <div class="row" style="margin-top: 25px;">
                                <div id="divFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblFailure" Text="Error! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblFailureMsg" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">
                                                <i class="ki ki-close"></i>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                                <div id="divSuccess" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblSuccess" Text="Done! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblSuccessMsg" runat="server" />
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
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnDelete" OnClick="BtnDelete_OnClick" Text="Delete" CssClass="btn btn-primary" runat="server" />
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

        <!--begin::Page Scripts(used by this page)-->
        <script src="assets/js/pages/custom/contacts/list-datatable.js"></script>
        <!--end::Page Scripts-->

        <script type="text/javascript">
            function OpenConfPopUp() {
                $('#divConfirm').modal('show');
            }

            function CloseConfPopUp() {
                $('#divConfirm').modal('hide');
            }

            function OpenSendInvitationPopUp() {
                $('#SendInvitationModal').modal('show');
            }

            function CloseSendInvitationPopUp() {
                $('#SendInvitationModal').modal('hide');
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
