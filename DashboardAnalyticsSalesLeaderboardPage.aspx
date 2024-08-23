<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardAnalyticsSalesLeaderboardPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardAnalyticsSalesLeaderboardPage" %>

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
                                <h3 class="card-label">Sales Leaderboard
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
                                            <span class="nav-icon"><i class="flaticon2-chat-1"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblTitle" Text="Partners Statistics" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <!--begin::Search Form-->
                                    <div class="mb-7">
                                        <div class="row align-items-center">
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
                                            PageSize="10" CssClass="table table-separate table-head-custom table-checkable" OnNeedDataSource="RdgResellers_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                            <MasterTableView Width="100%" Name="Parent">
                                                <NoRecordsTemplate>
                                                    <div class="emptyGridHolder">
                                                        <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                    </div>
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn Display="false" DataField="vendor_reseller_id" UniqueName="vendor_reseller_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="master_user_id" UniqueName="master_user_id" />
                                                    <telerik:GridBoundColumn Display="false" DataField="partner_user_id" UniqueName="partner_user_id" />
                                                    <telerik:GridBoundColumn HeaderText="Company Name" DataField="company_name" UniqueName="company_name" />
                                                    <telerik:GridBoundColumn HeaderText="Deals Count" DataField="deals_count" UniqueName="deals_count" />
                                                    <telerik:GridBoundColumn HeaderText="Deals Amount" DataField="deals_amount" UniqueName="deals_amount" />
                                                    <telerik:GridBoundColumn HeaderText="Leads Count" DataField="leads_count" UniqueName="leads_count" />
                                                    <telerik:GridBoundColumn HeaderText="Leads Amount" DataField="leads_amount" UniqueName="leads_amount" />
                                                    <telerik:GridBoundColumn HeaderText="Deals Won/Lost" DataField="deals_result" UniqueName="deals_result" />
                                                    <telerik:GridBoundColumn HeaderText="Deals Won Percent" DataField="deals_won_perc" UniqueName="deals_won_perc" />
                                                    <telerik:GridBoundColumn HeaderText="Leads Won/Lost" DataField="leads_result" UniqueName="leads_result" />
                                                    <telerik:GridBoundColumn HeaderText="Leads Won Percent" DataField="leads_won_perc" UniqueName="leads_won_perc" />
                                                    <telerik:GridBoundColumn HeaderText="Open Opportunities" DataField="deals_leads_pending" UniqueName="deals_leads_pending" />
                                                    <telerik:GridBoundColumn HeaderText="Deals Avg Size" DataField="deals_average_size" UniqueName="deals_average_size" />
                                                    <telerik:GridBoundColumn HeaderText="Deals Avg Sales Cycle" DataField="deals_average_sales_cycle" UniqueName="deals_average_sales_cycle" />
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <!--end: Datatable-->
                                </div>
                            </div>
                            <controls:MessageControl ID="UcMessageAlert" runat="server" />
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
