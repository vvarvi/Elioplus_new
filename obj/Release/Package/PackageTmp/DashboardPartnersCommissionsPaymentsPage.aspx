<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardPartnersCommissionsPaymentsPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardPartnersCommissionsPaymentsPage" %>

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
                    <div class="card card-custom">

                        <div class="card-header flex-wrap py-5">
                            <div class="card-title">
                                <h3 class="card-label">
                                    <asp:Label ID="LblHeaderInfo" Text="Commissions Dashboard" runat="server" />
                                </h3>
                            </div>
                            <div class="card-toolbar">
                                <div class="example-preview">
                                    <div class="dropdown dropdown-inline mr-10">
                                        <button type="button" class="btn btn-light-primary btn-icon btn-sm" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <i class="ki ki-bold-more-hor"></i>
                                        </button>
                                        <div class="dropdown-menu dropdown-menu-sm" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 33px, 0px);" x-placement="bottom-start">
                                            <a id="aCommissionBillingDetails" runat="server" class="dropdown-item">Edit Billing Details</a>
                                            <a id="aCommissionFeesTerms" runat="server" class="dropdown-item">Fees & Terms</a>
                                            <a id="aCommissionPayments" runat="server" class="dropdown-item">Payments</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="col-6" style="margin-bottom: 30px;">
                                <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                    <li class="nav-item">
                                        <a id="aPendingPayments" runat="server" onserverclick="aPendingPayments_ServerClick" class="nav-link active">
                                            <span class="nav-icon"><i class="flaticon-customer"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblPendingPayments" Text="Pending Payments" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a id="aPayments" runat="server" onserverclick="aPayments_ServerClick" class="nav-link">
                                            <span class="nav-icon"><i class="flaticon-customer"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblPayments" Text="Paid Payments" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a id="aPastPayments" runat="server" onserverclick="aPastPayments_ServerClick" class="nav-link">
                                            <span class="nav-icon"><i class="flaticon-customer"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblPastPayments" Text="Upcoming Payments" runat="server" />
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
                                        <!--begin::Search Form-->
                                        <div class="mb-7">
                                            <div id="divSearchAreaPending" visible="false" runat="server" class="row align-items-center">
                                                <div class="col-lg-9 col-xl-9">
                                                    <div class="row align-items-center">
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="d-flex align-items-center">
                                                                <div class="input-group-append"><span class="input-group-text"><i class="la la-map-pin"></i></span></div>
                                                                <asp:DropDownList ID="DrpPartnersPending" CssClass="form-control" runat="server">
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
                                                <div class="col-lg-3 col-xl-3 mt-5 mt-lg-0">
                                                    <asp:Button ID="BtnSearchPending" OnClick="BtnSearchPending_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                                </div>
                                            </div>
                                        </div>
                                        <!--end: Search Form-->
                                        <!--begin: Datatable-->
                                        <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                            <telerik:RadGrid ID="RdgCommisionsPending" AllowPaging="true" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowSorting="false" PagerStyle-Position="Bottom"
                                                PageSize="10" Width="100%" CssClass="table table-separate table-head-custom table-checkable"
                                                OnNeedDataSource="RdgCommisionsPending_OnNeedDataSource" OnItemDataBound="RdgCommisionsPending_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                                <MasterTableView Width="100%">
                                                    <Columns>
                                                        <telerik:GridBoundColumn Display="false" DataField="vendor_id" UniqueName="vendor_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="reseller_id" UniqueName="reseller_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                        <telerik:GridTemplateColumn HeaderText="Partner Name" DataField="partner_name" UniqueName="partner_name">
                                                            <ItemTemplate>
                                                                <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                    <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                        <span class="symbol-label">
                                                                            <asp:Image ID="ImgLogo" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "company_logo")%>' class="h-50 align-self-center" alt="" />
                                                                        </span>
                                                                    </div>
                                                                    <asp:Label ID="LblCompanyNameContent" Text='<%# DataBinder.Eval(Container.DataItem, "partner_name")%>' runat="server" />
                                                                </a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn HeaderText="Client" DataField="client" UniqueName="client" />
                                                        <telerik:GridBoundColumn HeaderText="Client Email" DataField="client_email" UniqueName="client_email" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Amount" DataField="amount" UniqueName="amount" />
                                                        <telerik:GridTemplateColumn HeaderText="Amount" DataField="amount" UniqueName="amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "amount")%>' runat="server" />
                                                                <asp:TextBox ID="TbxAmount" Text='<%# DataBinder.Eval(Container.DataItem, "amount")%>' runat="server" Width="70" Visible="false" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn HeaderText="Tier" DataField="tier_status" UniqueName="tier_status" />
                                                        <telerik:GridBoundColumn HeaderText="Commission" DataField="tier_commission" UniqueName="tier_commission" />
                                                        <telerik:GridBoundColumn HeaderText="Location" DataField="location" UniqueName="location" />
                                                        <telerik:GridBoundColumn HeaderText="Currency" DataField="currency" UniqueName="currency" />
                                                        <telerik:GridBoundColumn Display="false" DataField="last_update" UniqueName="last_update" />
                                                        <telerik:GridBoundColumn HeaderText="Payment Date" DataField="payment_date" UniqueName="payment_date" />
                                                        <telerik:GridTemplateColumn HeaderStyle-Width="100" HeaderText="Status" DataField="payment_status" UniqueName="payment_status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblStatus" CssClass="label label-lg label-light-success label-inline" Text='<%# DataBinder.Eval(Container.DataItem, "payment_status")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderStyle-Width="75" HeaderText="Actions" DataField="actions" UniqueName="actions">
                                                            <ItemTemplate>
                                                                <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Manage payments" data-placement="left">
                                                                    <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                        <i class="ki ki-bold-more-hor"></i>
                                                                    </a>
                                                                    <div class="dropdown-menu dropdown-menu-sm dropdown-menu-right" style="width: 200px;">
                                                                        <!--begin::Navigation-->
                                                                        <ul class="navi navi-hover">
                                                                            <li class="navi-item">
                                                                                <a id="aEditAmount" onserverclick="aEditAmountPending_ServerClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-avatar"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">
                                                                                        <asp:Label ID="LblEditAmount" Text="Edit amount" runat="server" />
                                                                                    </span>
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aCancelEditAmount" visible="false" onserverclick="aCancelEditAmountPending_ServerClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-avatar"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">
                                                                                        <asp:Label ID="LblCancelEditAmount" Text="Cancel edit amount" runat="server" />
                                                                                    </span>
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aPaymentLink" visible="false" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-chat-1"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">
                                                                                        <asp:Label ID="LblPaymentLink" Text="Open Payment link" runat="server" />
                                                                                    </span>
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aPayNow" visible="false" onserverclick="aPayNow_ServerClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">Pay Now</span>
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aDelete" onserverclick="aDeleteConfirmed_OnClick" visible="false" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">Cancel Payment</span>
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
                                        <controls:MessageControl ID="UcSendMessageAlertPending" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_2" runat="server" visible="false" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <!--begin::Search Form-->
                                        <div class="mb-7">
                                            <div id="divSearchAreaConfirmed" visible="false" runat="server" class="row align-items-center">
                                                <div class="col-lg-9 col-xl-9">
                                                    <div class="row align-items-center">
                                                        <div class="col-md-6 my-2 my-md-0">
                                                            <div class="d-flex align-items-center">
                                                                <div class="input-group-append"><span class="input-group-text"><i class="la la-map-pin"></i></span></div>
                                                                <asp:DropDownList ID="DrpPartners" CssClass="form-control" runat="server">
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
                                                </div>
                                            </div>
                                        </div>
                                        <!--end: Search Form-->
                                        <!--begin: Datatable-->
                                        <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                            <telerik:RadGrid ID="RdgCommisions" AllowPaging="true" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowSorting="false" PagerStyle-Position="Bottom"
                                                PageSize="10" Width="100%" CssClass="table table-separate table-head-custom table-checkable"
                                                OnNeedDataSource="RdgCommisions_OnNeedDataSource" OnItemDataBound="RdgCommisions_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                                <MasterTableView Width="100%">
                                                    <Columns>
                                                        <telerik:GridBoundColumn Display="false" DataField="vendor_id" UniqueName="vendor_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="reseller_id" UniqueName="reseller_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                        <telerik:GridTemplateColumn HeaderText="Partner Name" DataField="partner_name" UniqueName="partner_name">
                                                            <ItemTemplate>
                                                                <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                    <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                        <span class="symbol-label">
                                                                            <asp:Image ID="ImgLogo" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "company_logo")%>' class="h-50 align-self-center" alt="" />
                                                                        </span>
                                                                    </div>
                                                                    <asp:Label ID="LblCompanyNameContent" Text='<%# DataBinder.Eval(Container.DataItem, "partner_name")%>' runat="server" />
                                                                </a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn HeaderText="Client" DataField="client" UniqueName="client" />
                                                        <telerik:GridBoundColumn HeaderText="Client Email" DataField="client_email" UniqueName="client_email" />
                                                        <telerik:GridBoundColumn HeaderText="Amount" DataField="amount" UniqueName="amount" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Tier Status" DataField="tier_status" UniqueName="tier_status" />
                                                        <telerik:GridBoundColumn HeaderText="Commission" DataField="tier_commission" UniqueName="tier_commission" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Location" DataField="location" UniqueName="location" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Currency" DataField="currency" UniqueName="currency" />
                                                        <telerik:GridBoundColumn HeaderText="Charge Id" DataField="charge_id" UniqueName="charge_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="last_update" UniqueName="last_update" />
                                                        <telerik:GridBoundColumn HeaderText="Payment Date" DataField="payment_date" UniqueName="payment_date" />
                                                        <telerik:GridTemplateColumn HeaderStyle-Width="100" HeaderText="Status" DataField="payment_status" UniqueName="payment_status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblStatus" CssClass="label label-lg label-light-success label-inline" Text='<%# DataBinder.Eval(Container.DataItem, "payment_status")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>                                                        
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                        <!--end: Datatable-->
                                        <controls:MessageControl ID="UcSendMessageAlert" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_3" runat="server" visible="false" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <!--begin::Search Form-->
                                        <div class="mb-7">
                                        </div>
                                        <!--end: Search Form-->
                                        <!--begin: Datatable-->
                                        <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                            <telerik:RadGrid ID="RdgCommisionsPast" AllowPaging="true" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowSorting="false" PagerStyle-Position="Bottom"
                                                PageSize="10" Width="100%" CssClass="table table-separate table-head-custom table-checkable"
                                                OnNeedDataSource="RdgCommisionsPast_OnNeedDataSource" OnItemDataBound="RdgCommisionsPast_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                                <MasterTableView Width="100%">
                                                    <Columns>
                                                        <telerik:GridBoundColumn Display="false" DataField="vendor_id" UniqueName="vendor_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="reseller_id" UniqueName="reseller_id" />
                                                        <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                        <telerik:GridTemplateColumn HeaderText="Partner Name" DataField="partner_name" UniqueName="partner_name">
                                                            <ItemTemplate>
                                                                <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                    <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                                        <span class="symbol-label">
                                                                            <asp:Image ID="ImgLogo" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "company_logo")%>' class="h-50 align-self-center" alt="" />
                                                                        </span>
                                                                    </div>
                                                                    <asp:Label ID="LblCompanyNameContent" Text='<%# DataBinder.Eval(Container.DataItem, "partner_name")%>' runat="server" />
                                                                </a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn HeaderText="Client" DataField="client" UniqueName="client" />
                                                        <telerik:GridBoundColumn HeaderText="Client Email" DataField="client_email" UniqueName="client_email" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Amount" DataField="amount" UniqueName="amount" />
                                                        <telerik:GridTemplateColumn HeaderText="Amount" DataField="amount" UniqueName="amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "amount")%>' runat="server" />
                                                                <asp:TextBox ID="TbxAmount" Text='<%# DataBinder.Eval(Container.DataItem, "amount")%>' runat="server" Width="70" Visible="false" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn HeaderText="Tier" DataField="tier_status" UniqueName="tier_status" />
                                                        <telerik:GridBoundColumn HeaderText="Commission" DataField="tier_commission" UniqueName="tier_commission" />
                                                        <telerik:GridBoundColumn HeaderText="Location" DataField="location" UniqueName="location" />
                                                        <telerik:GridBoundColumn HeaderText="Currency" DataField="currency" UniqueName="currency" />
                                                        <telerik:GridBoundColumn Display="false" DataField="last_update" UniqueName="last_update" />
                                                        <telerik:GridBoundColumn HeaderText="Payment Date" DataField="payment_date" UniqueName="payment_date" />
                                                        <telerik:GridTemplateColumn HeaderStyle-Width="100" HeaderText="Status" DataField="payment_status" UniqueName="payment_status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblStatus" CssClass="label label-lg label-light-success label-inline" Text='<%# DataBinder.Eval(Container.DataItem, "payment_status")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderStyle-Width="75" HeaderText="Actions" DataField="actions" UniqueName="actions">
                                                            <ItemTemplate>
                                                                <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Manage payments" data-placement="left">
                                                                    <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                        <i class="ki ki-bold-more-hor"></i>
                                                                    </a>
                                                                    <div class="dropdown-menu dropdown-menu-sm dropdown-menu-right" style="width: 200px;">
                                                                        <!--begin::Navigation-->
                                                                        <ul class="navi navi-hover">
                                                                            <li class="navi-item">
                                                                                <a id="aEditAmount" onserverclick="aEditAmountPast_ServerClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-avatar"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">
                                                                                        <asp:Label ID="LblEditAmount" Text="Edit amount" runat="server" />
                                                                                    </span>
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aCancelEditAmount" visible="false" onserverclick="aCancelEditAmountPast_ServerClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-avatar"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">
                                                                                        <asp:Label ID="LblCancelEditAmount" Text="Cancel edit amount" runat="server" />
                                                                                    </span>
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aPaymentLink" visible="false" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-chat-1"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">
                                                                                        <asp:Label ID="LblPaymentLink" Text="Open Payment link" runat="server" />
                                                                                    </span>
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aPayNow" visible="false" onserverclick="aPayNow_ServerClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">Pay Now</span>
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aDelete" onserverclick="aDeleteConfirmed_OnClick" visible="false" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">Cancel Payment</span>
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
                                        <controls:MessageControl ID="UcSendMessageAlertPast" runat="server" />
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
                                    <asp:Label ID="LblConfMsg" Text="Are you sure you want to cancel this payment?" CssClass="control-label" runat="server" />
                                    <asp:HiddenField ID="HdnId" Value="0" runat="server" />
                                    <asp:HiddenField ID="HdnStripeAccountId" Value="" runat="server" />
                                    <asp:HiddenField ID="HdnStripeCustomerId" Value="" runat="server" />
                                    <asp:HiddenField ID="HdnAmount" Value="0" runat="server" />
                                    <asp:HiddenField ID="HdnCurrency" Value="" runat="server" />
                                    <asp:HiddenField ID="HdnCommissionFee" Value="0" runat="server" />
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
                            <asp:Button ID="BtnDelete" OnClick="BtnDelete_OnClick" Text="Cancel" CssClass="btn btn-primary" runat="server" />
                            <asp:Button ID="BtnProceedPayment" Visible="false" OnClick="BtnProceedPayment_Click" Text="Proceed Payment" CssClass="btn btn-success" runat="server" />
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
