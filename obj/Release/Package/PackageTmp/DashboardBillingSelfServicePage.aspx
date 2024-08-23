<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardBillingSelfServicePage.aspx.cs" Inherits="WdS.ElioPlus.DashboardBillingSelfServicePage" %>

<%@ Register Src="/Controls/Payment/Stripe_Service_Ctrl.ascx" TagName="UcServiceStripe" TagPrefix="controls" %>
<%@ Register Src="/Controls/Payment/Stripe_CreditCard_Ctrl.ascx" TagName="UcStripeCreditCard" TagPrefix="controls" %>
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
            <asp:UpdatePanel runat="server" ID="UpdatePnl" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="card card-custom">
                        <div class="card-header">
                            <div class="card-title">
                                <span class="card-icon">
                                    <i class="flaticon2-chart text-primary"></i>
                                </span>
                                <h3 class="card-label">
                                    <asp:Label ID="LblElioplusDashboard" runat="server" />
                                </h3>
                            </div>
                            <div class="card-toolbar">
                                <asp:Label ID="LblDashSubTitle" runat="server" />
                            </div>
                        </div>
                        <div class="card-body">
                            <div id="divServicePlan" runat="server" visible="false" class="row justify-content-center text-center my-0 my-md-25">
                                <div class="card-header">
                                    <div class="card-title">
                                        <h3 class="card-label">
                                            <asp:Label ID="LblElioService" runat="server" /></h3>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <!--begin::Example-->
                                    <div class="example example-basic">
                                        <div class="example-preview">
                                            <div style="padding: 10px;" class="form-group">
                                                <asp:Label ID="LblManagedText" runat="server" />
                                                <div style="padding-top: 20px; text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="LblServiceCost" runat="server" />
                                                </div>
                                                <div style="padding: 3px; text-align: center; font-size: 11px;">
                                                    <asp:Label ID="LblCommitment" Text="" runat="server" />
                                                </div>
                                                <div style="padding: 10px;">
                                                    <a id="aGetElioService" runat="server" role="button" data-toggle="modal" class="btn green purple-button sbold uppercase">
                                                        <asp:Label ID="LblGetElioService" runat="server" />
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divPricingPlan" runat="server" visible="false" class="row justify-content-center text-center my-0 my-md-25">
                                <!-- begin: Pricing-->
                                <div class="col-md-4 col-xxl-3 bg-white rounded-right shadow-sm">
                                    
                                </div>
                                <!-- end: Pricing-->
                                <!-- begin: Pricing-->
                                <div class="col-md-4 col-xxl-3 bg-primary my-md-n15 rounded shadow-sm">
                                    <div class="pt-25 pt-md-37 pb-25 pb-md-10 py-md-28 px-4">
                                        <h4 class="text-white mb-15">
                                            <asp:Label ID="LblGrowthUsers" Text="Self Service" runat="server" />
                                        </h4>
                                        <span class="px-7 py-3 bg-white d-inline-flex flex-center rounded-lg mb-15 bg-white">
                                            <span class="pr-2 text-primary opacity-70">$</span>
                                            <span class="pr-2 font-size-h1 font-weight-bold text-primary">
                                                <asp:Label ID="LblPremiumGrowthPrice" runat="server" />
                                            </span>
                                            <span class="text-primary opacity-70">/&#160;&#160;per month</span>
                                        </span>
                                        <br />
                                        <h4 class="font-size-h6 d-block d-block font-weight-bold mb-7 text-white">
                                            <asp:Label ID="LblPremiumGrowthConnections" runat="server" />
                                        </h4>
                                        <h4 class="font-size-h6 d-block d-block font-weight-bold mb-7 text-white">
                                            <asp:Label ID="LblPremiumGrowthManagePartners" runat="server" />
                                        </h4>

                                        <h4 class="font-size-h6 d-block d-block font-weight-bold mb-7 text-white">
                                            <asp:Label ID="LblPremiumGrowthMessages" runat="server" />
                                        </h4>

                                        <h4 class="font-size-h6 d-block d-block font-weight-bold mb-7 text-white">
                                            <asp:Label ID="LblPremiumGrowthLibraryStorage" runat="server" />
                                        </h4>
                                        <a id="aGrowthSignUp" runat="server" class="btn grey-salsa btn-outline price-button sbold uppercase">
                                            <asp:Label ID="LblSignUpGrowth" runat="server" />
                                        </a><a id="aGrowthPaymentModal" onserverclick="PaymentGrowthModal_OnClick" visible="false" runat="server" class="btn btn-white text-uppercase font-weight-bolder px-15 py-3">
                                            <asp:Label ID="LblGrowthGoPremium" runat="server" />
                                        </a>
                                        <asp:Button ID="BtnGrowthGoPremium" Visible="false" OnClick="BtnSearchGoPremium_OnClick" CssClass="btn btn-white text-uppercase font-weight-bolder px-15 py-3" runat="server" />
                                    </div>
                                </div>
                                <!-- end: Pricing-->
                                <!-- begin: Pricing-->
                                <div class="col-md-4 col-xxl-3 bg-white rounded-right shadow-sm">
                                    
                                </div>
                                <!-- end: Pricing-->
                            </div>
                        </div>
                    </div>
                    <div class="card card-custom">
                        <div class="card-header card-header-tabs-line">
                            <div class="card-title">
                                <h3 class="card-label"></h3>
                            </div>
                            <div class="card-toolbar">
                                <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                    <li id="liBillingHistory" runat="server" class="nav-item">
                                        <a id="aBillingHistory" role="button" onserverclick="aBillingHistory_ServerClick" runat="server" class="nav-link active">
                                            <span class="nav-icon"><i class="flaticon2-chat-1"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblBillingHistory" Text="Transactions History" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                    <li id="liEditBillingAccount" runat="server" class="nav-item">
                                        <a id="aEditBillingAccount" role="button" onserverclick="aEditBillingAccount_ServerClick" runat="server" class="nav-link">
                                            <span class="nav-icon"><i class="flaticon2-drop"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblBillingInfo" Text="Billing Info" runat="server" /></span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <!--begin::Search Form-->
                                    <div id="divSearch" runat="server" visible="false" class="mb-7">
                                        <div class="row align-items-center">
                                            <div class="col-lg-9 col-xl-8">
                                                <div class="row align-items-center">
                                                    <div class="col-md-6 my-2 my-md-0">
                                                        <div class="input-icon">
                                                            <asp:TextBox ID="TbxInvoices" Width="250" placeholder="Invoice number" CssClass="form-control" runat="server" />

                                                            <span>
                                                                <i class="flaticon2-search-1 text-muted"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 my-2 my-md-0">
                                                        <div class="d-flex align-items-center">
                                                            <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
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
                                        <telerik:RadGrid ID="RdgOrders" Width="100%" CssClass="table table-separate table-head-custom table-checkable" HeaderStyle-HorizontalAlign="Center" BorderStyle="None" AllowPaging="false" PagerStyle-Position="Bottom" OnNeedDataSource="RdgOrders_OnNeedDataSource" OnItemDataBound="RdgOrders_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                            <MasterTableView>
                                                <Columns>
                                                    <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="user_subscription_id" UniqueName="user_subscription_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="user_id" UniqueName="user_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="invoice_id" UniqueName="invoice_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="charge_id" UniqueName="charge_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="is_closed" UniqueName="is_closed" />
                                                    <telerik:GridBoundColumn Display="false" DataField="sub_total_amount" UniqueName="sub_total_amount" />
                                                    <telerik:GridBoundColumn Display="false" DataField="number" UniqueName="number" />
                                                    <telerik:GridBoundColumn Display="false" DataField="period_start" UniqueName="period_start" />
                                                    <telerik:GridBoundColumn Display="false" DataField="period_end" UniqueName="period_end" />
                                                    <telerik:GridBoundColumn Display="false" DataField="is_paid" UniqueName="is_paid" />
                                                    <telerik:GridBoundColumn DataField="status" UniqueName="status" />
                                                    <telerik:GridBoundColumn DataField="plan_id" UniqueName="plan_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="coupon_id" UniqueName="coupon_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="plan_nickname" UniqueName="plan_nickname" />
                                                    <telerik:GridBoundColumn Display="false" DataField="subscription_id" UniqueName="subscription_id" />
                                                    <telerik:GridBoundColumn DataField="date" UniqueName="date" />
                                                    <telerik:GridBoundColumn DataField="total_amount" UniqueName="total_amount" />
                                                    <telerik:GridBoundColumn DataField="invoice_pdf" UniqueName="invoice_pdf" />
                                                    <telerik:GridBoundColumn DataField="cancel" />
                                                    <telerik:GridBoundColumn DataField="activate" />
                                                    <telerik:GridTemplateColumn Display="false">
                                                        <ItemTemplate>
                                                            <tbody>
                                                                <tr style="text-align: center;">
                                                                    <td>
                                                                        <span class="">
                                                                            <asp:Label ID="LblStatus" runat="server" /></span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblPlan" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblDateCreated" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblPrice" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <a id="aInvoiceUrl" runat="server">
                                                                                <asp:Label ID="LblInvoiceUrl" runat="server" />
                                                                            </a>
                                                                            <asp:LinkButton Visible="false" ID="LnkBtnInvoiceExport" Text="invoices" runat="server" OnClick="LnkBtnInvoiceExport_Click" />
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <a id="BtnCancelPlan" onserverclick="BtnCancelPlan_ServerClick" class="btn btn-circle btn-sm purple" runat="server">
                                                                            <asp:Label ID="LblBtnCancelPlan" runat="server" />
                                                                            <i class="fa fa-times"></i>
                                                                        </a>
                                                                    </td>
                                                                    <td style="">
                                                                        <a id="BtnActivatePlan" visible="false" onserverclick="PaymentCanceledModal_OnClick" class="btn btn-circle btn-sm purple" runat="server">
                                                                            <asp:Label ID="LblBtnActivatePlan" runat="server" />
                                                                            <i class="fa fa-check"></i>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </table>
                                    <!--end: Datatable-->
                                    <controls:MessageControl ID="MessageAlertHistory" runat="server" />
                                </div>
                                <div class="tab-pane fade" id="tab_1_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_2_3">
                                    <div class="row">
                                        <div class="col-3">
                                            <ul class="navi navi-border">
                                                <li class="navi-item">
                                                    <a id="aElioBillingDetails" runat="server" role="button" onserverclick="aElioBillingDetails_ServerClick" class="navi-link active">
                                                        <span class="navi-icon"><i class="flaticon2-analytics"></i></span>
                                                        <span class="navi-text">
                                                            <asp:Label ID="LblElioBillingDetails" runat="server" />
                                                        </span>
                                                    </a>
                                                </li>
                                                <li class="navi-item">
                                                    <a id="aStripeCreditCardDetails" runat="server" role="button" onserverclick="aStripeCreditCardDetails_ServerClick" class="navi-link">
                                                        <span class="navi-icon"><i class="flaticon2-pie-chart-2"></i></span>
                                                        <span class="navi-text">
                                                            <asp:Label ID="LblStripeCreditCardDetails" runat="server" /></span>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-9">
                                            <div class="tab-pane fade show active" id="tab_2_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_2_3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel12">
                                                    <ContentTemplate>
                                                        <div class="card-body">
                                                            <h3 class="font-size-lg text-dark font-weight-bold mb-6">1. Billing Info:</h3>
                                                            <div class="mb-15">
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Company Name:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxBillingCompanyName" CssClass="form-control" placeholder="Enter company name" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your company name</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Company Billing Address:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxBillingCompanyAddress" CssClass="form-control" placeholder="Enter company billing address (street, city, country-state, country)" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your company billing address (street, city, country-state, country)</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Company Post Code:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxBillingCompanyPostCode" CssClass="form-control" placeholder="Enter company post code" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your company post code</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Company Vat Number:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxBillingCompanyVatNumber" CssClass="form-control" placeholder="Enter company vat number" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your company company vat number</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <controls:MessageControl ID="UcBillingMessageControl" runat="server" />
                                                        </div>
                                                        <div class="card-footer">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <asp:Button ID="BtnSaveBillingDetails" OnClick="BtnSaveBillingDetails_OnClick" CssClass="btn btn-success mr-2" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="tab-pane fade" id="tab_2_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_2_3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel14">
                                                    <ContentTemplate>
                                                        <div class="card-body">
                                                            <h3 class="font-size-lg text-dark font-weight-bold mb-6">2. Credit Card Info:</h3>
                                                            <div class="mb-15">
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Credit Card Number:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxCCNumber" CssClass="form-control" placeholder="Enter Credit Card Number" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your Credit Card Number</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">CVC:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxCvcNumber" CssClass="form-control" placeholder="Enter CVC" MaxLength="45" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your CVC</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Expiration Month:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:DropDownList ID="DrpExpMonth" CssClass="form-control" placeholder="month" data-placement="top" data-trigger="manual" runat="server">
                                                                            <asp:ListItem Text="MM" Selected="True" Value="0" />
                                                                            <asp:ListItem Text="01" Value="1" />
                                                                            <asp:ListItem Text="02" Value="2" />
                                                                            <asp:ListItem Text="03" Value="3" />
                                                                            <asp:ListItem Text="04" Value="4" />
                                                                            <asp:ListItem Text="05" Value="5" />
                                                                            <asp:ListItem Text="06" Value="6" />
                                                                            <asp:ListItem Text="07" Value="7" />
                                                                            <asp:ListItem Text="08" Value="8" />
                                                                            <asp:ListItem Text="09" Value="9" />
                                                                            <asp:ListItem Text="10" Value="10" />
                                                                            <asp:ListItem Text="11" Value="11" />
                                                                            <asp:ListItem Text="12" Value="12" />
                                                                        </asp:DropDownList>
                                                                        <span class="form-text text-muted">Please enter your credit card expiration month</span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-lg-3 col-form-label text-right">Expiration Year:</label>
                                                                    <div class="col-lg-6">
                                                                        <asp:TextBox ID="TbxExpYear" CssClass="form-control" placeholder="year" MaxLength="2" runat="server" />
                                                                        <span class="form-text text-muted">Please enter your credit card expiration year(last 2 digits)</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <controls:MessageControl ID="UcCreditcardMessageControl" runat="server" />
                                                        </div>
                                                        <div class="card-footer">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <asp:Button ID="BtnSaveCreditCardDetails" Visible="false" OnClick="BtnSaveCreditCardDetails_OnClick" CssClass="btn btn-success mr-2" runat="server" />
                                                                    <asp:Button ID="BtnAddNewCard" OnClick="BtnAddNewCard_OnClick" CssClass="btn btn-primary mr-2" runat="server" />
                                                                    <asp:Button ID="BtnCancelAddNewCard" OnClick="BtnCancelAddNewCard_OnClick" CssClass="btn btn-secondary" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <!-- Payment Service form (modal view) -->
    <div id="PaymentServiceModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <controls:UcServiceStripe ID="UcServiceStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Confirmation form (modal view) -->
    <div id="ConfirmationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <controls:UcConfirmationMessageAlert ID="UcConfirmationMessageAlert" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Confirmation service form (modal view) -->
    <div id="ConfirmationServiceModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <controls:UcConfirmationServiceMessageAlert ID="UcConfirmationServiceMessageAlert" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Stripe Payment Modal -->
    <div id="PaymentPacketsModal" class="modal fade" tabindex="-1" data-width="500">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div class="modal-dialog" id="wndPayment" style="width: 300px;">
                    <div class="modal-content" style="display: inline-block;">
                        <div class="modal-header">
                            <h4 id="myModalLabel">
                                <asp:Label ID="LblMessageHeader" Text="Your plan" runat="server" />
                            </h4>
                            <asp:Button ID="BtnCancelPayment" OnClick="BtnCancelPayment_OnClick" Text="X" CssClass="ki ki-close" aria-hidden="true" runat="server" />
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group">
                                    <asp:TextBox ID="TbxStripePlan" Enabled="false" CssClass="form-control" Width="280" MaxLength="100" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                                <div class="form-group">
                                    <div style="font-size: 13px; color: #3699FF; display: inline-block; padding: 10px 0 10px 0; float: left;">
                                        <asp:Label ID="LblTotalCost" Text="Your service plan cost is" runat="server" />
                                    </div>
                                </div>
                                <div style="font-weight: bold; font-size: 15px; float: right; background-color: #3699FF; color: #fff; padding: 5px; border: 1px solid #3699FF; border-radius: 5px; width: 90px; height: 35px; margin-left: 25px; text-align: center; vertical-align: middle;">
                                    <asp:Label ID="LblTotalCostValue" runat="server" />
                                </div>
                                <form class="form-horizontal col-sm-12">
                                    <asp:Panel ID="PnlPayment" runat="server">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="flaticon2-email"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="TbxEmail" CssClass="form-control" MaxLength="100" placeholder="Email" data-placement="top" data-trigger="manual" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="flaticon-price-tag"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="TbxCardNumber" onkeypress="return isNumberOnly(event);" MaxLength="20" CssClass="form-control" name="creditcard" placeholder="Enter card number" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="DrpExpMonth1" CssClass="form-control" placeholder="MM" data-placement="top" data-trigger="manual" runat="server">
                                                    <asp:ListItem Text="MM" Selected="True" Value="0" />
                                                    <asp:ListItem Text="01" Value="1" />
                                                    <asp:ListItem Text="02" Value="2" />
                                                    <asp:ListItem Text="03" Value="3" />
                                                    <asp:ListItem Text="04" Value="4" />
                                                    <asp:ListItem Text="05" Value="5" />
                                                    <asp:ListItem Text="06" Value="6" />
                                                    <asp:ListItem Text="07" Value="7" />
                                                    <asp:ListItem Text="08" Value="8" />
                                                    <asp:ListItem Text="09" Value="9" />
                                                    <asp:ListItem Text="10" Value="10" />
                                                    <asp:ListItem Text="11" Value="11" />
                                                    <asp:ListItem Text="12" Value="12" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="TbxExpYear1" onkeypress="return isNumberOnly(event);" MaxLength="2" CssClass="form-control" placeholder="YY" data-placement="top" data-trigger="manual" runat="server" />
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="TbxCVC" onkeypress="return isNumberOnly(event);" MaxLength="5" CssClass="form-control" placeholder="CVC" data-placement="top" data-trigger="manual" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:CheckBox ID="CbxCouponDiscount" AutoPostBack="true" OnCheckedChanged="CbxCouponDiscount_OnCheckedChanged" Text="I have discount coupon" runat="server" />
                                        </div>
                                        <div id="divDiscount" class="form-group" runat="server" visible="false">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="flaticon2-browser"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="TbxDiscount" CssClass="form-control" MaxLength="100" placeholder="Coupon ID" data-placement="top" data-trigger="manual" runat="server" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </form>
                            </div>
                            <div class="row">
                                <div id="divPaymentWarningMsg" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-questions-circular-button"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblPaymentWarningMsg" runat="server" />
                                        </strong>
                                        <asp:Label ID="LblPaymentWarningMsgContent" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">
                                                <i class="ki ki-close"></i>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                                <div id="divPaymentSuccessMsg" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-questions-circular-button"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblPaymentSuccessMsg" runat="server" />
                                        </strong>
                                        <asp:Label ID="LblPaymentSuccessMsgContent" runat="server" />
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
                            <asp:Button ID="BtnBottomCancelPayment" OnClick="BtnCancelPayment_OnClick" Text="Cancel" CssClass="btn btn-light-primary" aria-hidden="true" runat="server" />
                            &nbsp;
                            <asp:Button ID="BtnPayment" Text="Subscribe" OnClick="BtnPayment_OnClick" OnClientClick="disable()" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Change Credit Card form (modal view) -->
    <div id="CCNumberUpdateModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel6">
            <ContentTemplate>
                <controls:UcStripeCreditCard ID="UcStripeCreditCard" runat="server" />
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

            function isNumberOnly(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }

            function CloseServicePaymentModal() {
                $('#PaymentServiceModal').modal('hide');
            }

            function OpenServicePaymentModal() {
                $('#PaymentServiceModal').modal('show');
            }

            function ClosePaymentModal() {
                $('#PaymentPacketsModal').modal('hide');
            }

            function OpenPaymentModal() {
                $('#PaymentPacketsModal').modal('show');
            }

            function CloseCCNumberUpdateModal() {
                $('#CCNumberUpdateModal').modal('hide');
            }

            function OpenCCNumberUpdateModal() {
                $('#CCNumberUpdateModal').modal('show');
            }

            function CloseConfirmationModal() {
                $('#ConfirmationModal').modal('hide');
            }

            function OpenConfirmationModal() {
                $('#ConfirmationModal').modal('show');
            }
            function CloseServiceConfirmationModal() {
                $('#ConfirmationServiceModal').modal('hide');
            }

            function OpenServiceConfirmationModal() {
                $('#ConfirmationServiceModal').modal('show');
            }
        </script>

    </telerik:RadScriptBlock>

</asp:Content>
