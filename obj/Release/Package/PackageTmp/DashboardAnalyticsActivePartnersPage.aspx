<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardAnalyticsActivePartnersPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardAnalyticsActivePartnersPage" %>

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
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanelContent">
                <ContentTemplate>
                    <!--begin::Card-->
                    <div class="card card-custom">

                        <div class="card-header flex-wrap py-5">
                            <div class="card-title">
                                <h3 class="card-label">Active Partners
                                </h3>
                            </div>
                            <div class="card-toolbar">
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="" style="margin-bottom: 30px;">
                                <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                    <li class="nav-item">
                                        <a href="#tab_1_1" data-toggle="tab" class="nav-link active">
                                            <span class="nav-icon"><i class="flaticon2-avatar"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblMostActive" Text="All Active Partners" runat="server" />
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
                                            <div class="col-lg-3 col-xl-3">
                                                <div class="row align-items-center">
                                                    <div class="col-md-12 my-2 my-md-0">
                                                        <div class="input-icon">
                                                            <asp:TextBox ID="RtbxCompanyNameEmail" CssClass="form-control" runat="server" />
                                                            <span>
                                                                <i class="flaticon2-search-1 text-muted"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-xl-2">
                                                <div class="row align-items-center">
                                                    <div class="col-md-12 my-2 my-md-0">
                                                        <div class="input-icon">
                                                            <asp:DropDownList ID="DrpYears" Width="125" CssClass="form-control" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-xl-4 mt-5 mt-lg-0">
                                                <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                            </div>
                                            <div class="col-lg-5 col-xl-5">
                                                <div class="row align-items-center">
                                                    <div class="col-md-12 my-2 my-md-0">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end: Search Form-->
                                    <!--begin: Datatable-->
                                    <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                        <telerik:RadGrid ID="RdgResellers" AllowPaging="false" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowSorting="false" PagerStyle-Position="Bottom"
                                            PageSize="10" CssClass="table table-separate table-head-custom table-checkable" OnNeedDataSource="RdgResellers_OnNeedDataSource" OnItemDataBound="RdgResellers_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                            <MasterTableView Width="100%" Name="Parent">
                                                <NoRecordsTemplate>
                                                    <div class="emptyGridHolder">
                                                        <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                    </div>
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn Display="false" DataField="partner_user_id" UniqueName="partner_user_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="company_name" UniqueName="company_name" />
                                                    <telerik:GridBoundColumn Display="false" DataField="user_application_type" UniqueName="user_application_type" />
                                                    <telerik:GridTemplateColumn HeaderText="Company">
                                                        <ItemTemplate>
                                                            <a id="aCompanyLogo" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                <div class="symbol symbol-50 symbol-light mr-12" style="float: left; display: inline-block;">
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
                                                    <telerik:GridBoundColumn Display="false" HeaderStyle-Width="60" HeaderText="Company Name" DataField="company_name"
                                                        UniqueName="company_name" />
                                                    <telerik:GridBoundColumn HeaderText="Login Count" DataField="user_login_count" UniqueName="user_login_count" />
                                                    <telerik:GridBoundColumn HeaderText="Deals Count" DataField="deals_count" UniqueName="deals_count" />
                                                    <telerik:GridTemplateColumn HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <a id="aCollaborationRoomConfirmed" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
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

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <style>
            .RadGrid_MetroTouch .rgAltRow {
                background-color: transparent !important;
            }

            .RadGrid_MetroTouch .rgRow {
                background-color: transparent !important;
            }
        </style>

    </telerik:RadScriptBlock>

</asp:Content>
