<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardPDManagePartnersPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardPDManagePartnersPage" %>

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
                                <div class="row">
                                    <div id="divVendorToolsConfirmed" runat="server" visible="false">
                                        <!--begin::Button-->
                                        <a id="aChannelPartnerInvitationConfirmed" runat="server" class="btn btn-primary font-weight-bolder">
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
                                            <asp:Label ID="LblSendNewInvitationConfirmed" Text="Invite partners" runat="server" />
                                        </a>
                                        <!--end::Button-->
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="col-6" style="margin-bottom: 30px;">
                                <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                    <li class="nav-item">
                                        <a href="#tab_1_1" data-toggle="tab" class="nav-link active">
                                            <span class="nav-icon"><i class="flaticon-customer"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblActiveInvitations" Text="Partner Directory" runat="server" />
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
                                            <div class="col-lg-9 col-xl-9">
                                                <div class="row align-items-center">
                                                    <div class="col-md-6 my-2 my-md-0">
                                                        <div class="d-flex align-items-center">
                                                            <asp:DropDownList ID="DdlCountriesConfirmed" CssClass="form-control" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 my-2 my-md-0">
                                                        <div class="input-icon">
                                                            <asp:TextBox ID="RtbxCompanyNameEmailConfirmed" placeholder="Name/Email" CssClass="form-control" runat="server" />
                                                            <span>
                                                                <i class="flaticon2-search-1 text-muted"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-xl-3 mt-5 mt-lg-0">
                                                <asp:Button ID="BtnSearchConfirmed" OnClick="BtnSearchConfirmed_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                                <!--begin::Dropdown-->
                                                <div id="divExportAreaButton" runat="server" class="dropdown dropdown-inline mr-2">
                                                    <button type="button" class="btn btn-light-warning font-weight-bolder dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
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
                                                    <div class="dropdown-menu dropdown-menu-sm dropdown-menu-right" style="width: 225px;">
                                                        <!--begin::Navigation-->
                                                        <ul class="navi flex-column navi-hover py-2">
                                                            <li class="navi-header font-weight-bolder text-uppercase font-size-sm text-primary pb-2">Export to:</li>

                                                            <li class="navi-item">
                                                                <a id="aBtnExportPdf" role="button" onserverclick="aBtnExportPdf_ServerClick" runat="server" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="la la-file-text-o"></i>
                                                                    </span>
                                                                    <span class="navi-text">
                                                                        <asp:Label ID="LblBtnExportPdf" Text="PDF" runat="server" />
                                                                    </span>
                                                                </a>
                                                                <a id="aBtnExportCriteriaPdf" role="button" onserverclick="aBtnExportCriteriaPdf_ServerClick" runat="server" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="la la-file-text-o"></i>
                                                                    </span>
                                                                    <span class="navi-text">
                                                                        <asp:Label ID="LblBtnExportCriteriaPdf" Text="PDF with criteria weight" runat="server" />
                                                                    </span>
                                                                </a>
                                                            </li>
                                                            <li class="navi-item">
                                                                <a id="aBtnExportCsv" role="button" onserverclick="aBtnExportCsv_ServerClick" runat="server" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="la la-file-text-o"></i>
                                                                    </span>
                                                                    <span class="navi-text">
                                                                        <asp:Label ID="LblBtnExportCsv" Text="CSV" runat="server" />
                                                                    </span>
                                                                </a>
                                                                <a id="aBtnExportCriteriaCsv" role="button" onserverclick="aBtnExportCriteriaCsv_ServerClick" runat="server" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="la la-file-text-o"></i>
                                                                    </span>
                                                                    <span class="navi-text">
                                                                        <asp:Label ID="LblBtnExportCriteriaCsv" Text="CSV with criteria weight" runat="server" />
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
                                        </div>
                                    </div>
                                    <!--end: Search Form-->
                                    <!--begin: Datatable-->
                                    <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                        <telerik:RadGrid ID="RdgSendInvitationsConfirmed" AllowPaging="true" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowSorting="false" PagerStyle-Position="Bottom"
                                            PageSize="10" CssClass="table table-separate table-head-custom table-checkable"
                                            OnNeedDataSource="RdgSendInvitationsConfirmed_OnNeedDataSource"
                                            OnItemDataBound="RdgSendInvitationsConfirmed_OnItemDataBound" AutoGenerateColumns="false"
                                            runat="server">
                                            <MasterTableView Width="100%" DataKeyNames="id" Name="Parent">
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
                                                    <telerik:GridBoundColumn Display="false" DataField="tier_status" UniqueName="tier_status" />
                                                    <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                    <telerik:GridTemplateColumn HeaderText="Company">
                                                        <ItemTemplate>
                                                            <a id="aCompanyLogo" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                <div class="symbol symbol-50 symbol-light mr-2" style="float: left; padding-right: 15px;">
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
                                                    <telerik:GridBoundColumn Display="false" DataField="company_name"
                                                        UniqueName="company_name" />
                                                    <telerik:GridBoundColumn Display="false" DataField="sysdate"
                                                        UniqueName="sysdate" />
                                                    <telerik:GridBoundColumn Display="false" DataField="email"
                                                        UniqueName="email" />
                                                    <telerik:GridBoundColumn HeaderText="Country" DataField="country"
                                                        UniqueName="country" />
                                                    <telerik:GridTemplateColumn HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblStatus" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="LblHdrTierStatusText" Text="Tier Status" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div id="divEdit" style="width: 87px;" runat="server">
                                                                <asp:Label ID="LblTierStatus" runat="server" />
                                                                <a id="aEditTierStatus" visible="false" runat="server" onserverclick="aEditTierStatus_OnClick">
                                                                    <i class="fa fa-edit"></i>
                                                                </a>
                                                                <asp:ImageButton ID="ImgBtnEditTierStatus" Visible="false" ImageUrl="~/Images/edit.png" runat="server" />
                                                            </div>
                                                            <div id="divSave" visible="false" runat="server">
                                                                <asp:DropDownList ID="RcbxTierStatus" Width="85" CssClass="form-control" runat="server" />
                                                                <a id="aSaveTierStatus" runat="server" onserverclick="aSaveTierStatus_OnClick">
                                                                    <i class="fa fa-save"></i>
                                                                </a>
                                                                <a id="aCancelTierStatus" runat="server" onserverclick="aCancelTierStatus_OnClick">
                                                                    <i class="fa fa-trash"></i>
                                                                </a>
                                                                <asp:ImageButton ID="ImgBtnTierSave" Visible="false" ImageUrl="~/Images/save.png" runat="server" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn Display="false" HeaderText="SCORE" DataField="score" UniqueName="score" />
                                                    <telerik:GridTemplateColumn Display="false" HeaderText="Assigned Account Manager" UniqueName="sub_account_id">
                                                        <ItemTemplate>
                                                            <div class="text-center text-center" style="float: left;">
                                                                <div id="divMemberImages" runat="server" class="symbol-group symbol-hover justify-content-center">
                                                                    <div id="divPic1" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip">
                                                                        <img id="Pic1" runat="server" alt="" width="35" height="35" src="/assets/media/users/300_16.jpg" />
                                                                    </div>
                                                                    <asp:Label ID="LblAssignedPartnerFullName" Text="Not Assigned" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div id="divMembersArea" visible="false" runat="server">
                                                                <span class="btn btn-sm btn-text">
                                                                    <asp:DropDownList ID="DrpTeamMembers" Width="240" CssClass="form-control" runat="server" />
                                                                </span>
                                                                <a id="aSaveAssignToMember" title="Assign/UnAssign partner to/from member of your team" runat="server">
                                                                    <i class="fa fa-save"></i>
                                                                </a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="75" HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Manage partner" data-placement="left">
                                                                <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                    <i class="ki ki-bold-more-hor"></i>
                                                                </a>
                                                                <div class="dropdown-menu dropdown-menu-sm dropdown-menu-right" style="width:200px;">
                                                                    <!--begin::Navigation-->
                                                                    <ul class="navi navi-hover">
                                                                        <li class="navi-item">
                                                                            <a id="aMoreDetails" runat="server" class="navi-link">
                                                                                <span class="navi-icon">
                                                                                    <i class="flaticon-profile"></i>
                                                                                </span>
                                                                                <span class="navi-text">
                                                                                    <asp:Label ID="LblMoreDetails" Text="Manage partner" runat="server" />
                                                                                </span>
                                                                            </a>
                                                                        </li>
                                                                        <li class="navi-item">
                                                                            <a id="aAssignPartner" onserverclick="aAssignPartner_ServerClick" runat="server" class="navi-link">
                                                                                <span class="navi-icon">
                                                                                    <i class="flaticon-avatar"></i>
                                                                                </span>
                                                                                <span class="navi-text">
                                                                                    <asp:Label ID="LblAssignPartner" Text="Assign account manager" runat="server" />
                                                                                </span>
                                                                            </a>
                                                                        </li>
                                                                        <li class="navi-item">
                                                                            <a id="aCollaborationRoomConfirmed" onserverclick="ImgBtnCollaborationRoom_OnClick" runat="server" class="navi-link">
                                                                                <span class="navi-icon">
                                                                                    <i class="flaticon-chat-1"></i>
                                                                                </span>
                                                                                <span class="navi-text">
                                                                                    <asp:Label ID="LblCollaborationRoomConfirmed" Text="Chat with partner" runat="server" />
                                                                                </span>
                                                                            </a>
                                                                        </li>
                                                                        <li class="navi-item">
                                                                            <a id="aDeleteConfirmed" onserverclick="aDeleteConfirmed_OnClick" runat="server" class="navi-link">
                                                                                <span class="navi-icon">
                                                                                    <i class="flaticon-delete"></i>
                                                                                </span>
                                                                                <span class="navi-text">Delete partner</span>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                    <!--end::Navigation-->
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <!--end: Datatable-->
                                </div>
                            </div>
                            <controls:MessageControl ID="UcSendMessageAlertConfirmed" runat="server" />
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
    <div id="SendInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="LblInvitationSendTitle" Text="Invitation Send" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblSuccessfullSendfMsg" CssClass="control-label" runat="server" />
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
                                    <asp:HiddenField ID="HdnPartnerUserId" Value="0" runat="server" />
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
    <!-- Pop Up Assign Partner (modal view) -->
    <div id="PopUpAssignPartner" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblMessageTitle" Text="Assign Partner to Team Member" CssClass="control-label" runat="server" /></h4>
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
            function RapPage_OnRequestStart(sender, args) {
                $('#loader').show();
            }
            function endsWith(s) {
                return this.length >= s.length && this.substr(this.length - s.length) == s;
            }
            function RapPage_OnResponseEnd(sender, args) {
                $('#loader').hide();
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
