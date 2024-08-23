<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardAnalyticsGeneralPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardAnalyticsGeneralPage" %>

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
                    <!--begin::Row-->
                    <div class="row">
                        <div class="col-xl-12">
                            <!--begin::Charts Widget 6-->
                            <div class="card card-custom card-stretch gutter-b">
                                <!--begin::Header-->
                                <div class="card-header h-auto border-0">
                                    <div class="card-title py-5">
                                        <h3 class="card-label">
                                            <span class="d-block text-dark font-weight-bolder">Total Statistics</span>
                                            <span class="d-block text-muted mt-2 font-size-sm"></span>
                                        </h3>
                                    </div>
                                    <div class="card-toolbar">
                                    </div>
                                </div>
                                <!--end::Header-->
                                <!--begin::Body-->
                                <div class="card-body">
                                    <div class="row">
                                        <asp:Repeater ID="RptTiers" runat="server" OnLoad="RptTiers_Load" OnItemDataBound="RptTiers_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="col-xl-3">
                                                    <!--begin::Tiles Widget 11-->
                                                    <div class="card card-custom gutter-b" style="height: 150px">
                                                        <div id="div0" runat="server" class="card-body">
                                                            <span class="svg-icon svg-icon-3x svg-icon-primary">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Group.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <polygon points="0 0 24 0 24 24 0 24"></polygon>
                                                                        <path d="M18,14 C16.3431458,14 15,12.6568542 15,11 C15,9.34314575 16.3431458,8 18,8 C19.6568542,8 21,9.34314575 21,11 C21,12.6568542 19.6568542,14 18,14 Z M9,11 C6.790861,11 5,9.209139 5,7 C5,4.790861 6.790861,3 9,3 C11.209139,3 13,4.790861 13,7 C13,9.209139 11.209139,11 9,11 Z" fill="#000000" fill-rule="nonzero" opacity="0.3"></path>
                                                                        <path d="M17.6011961,15.0006174 C21.0077043,15.0378534 23.7891749,16.7601418 23.9984937,20.4 C24.0069246,20.5466056 23.9984937,21 23.4559499,21 L19.6,21 C19.6,18.7490654 18.8562935,16.6718327 17.6011961,15.0006174 Z M0.00065168429,20.1992055 C0.388258525,15.4265159 4.26191235,13 8.98334134,13 C13.7712164,13 17.7048837,15.2931929 17.9979143,20.2 C18.0095879,20.3954741 17.9979143,21 17.2466999,21 C13.541124,21 8.03472472,21 0.727502227,21 C0.476712155,21 -0.0204617505,20.45918 0.00065168429,20.1992055 Z" fill="#000000" fill-rule="nonzero"></path>
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                            <div class="text-dark font-weight-bolder font-size-h2 mt-3">
                                                                <asp:Label ID="LblTotalPartnersCount1" Text="0" runat="server" />
                                                            </div>
                                                            <a href="#" class="text-muted text-hover-primary font-weight-bold font-size-lg mt-1">
                                                                <asp:Label ID="LblTotalPartnersText1" Text="Total Confirmed Partners" runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!--end::Tiles Widget 11-->
                                                </div>
                                                <div class="col-xl-3">
                                                    <!--begin::Tiles Widget 12-->
                                                    <div class="card card-custom gutter-b" style="height: 150px">
                                                        <div id="div1" runat="server" visible="false" class="card-body">
                                                            <span class="svg-icon svg-icon-3x svg-icon-primary">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Group.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <polygon points="0 0 24 0 24 24 0 24"></polygon>
                                                                        <path d="M18,14 C16.3431458,14 15,12.6568542 15,11 C15,9.34314575 16.3431458,8 18,8 C19.6568542,8 21,9.34314575 21,11 C21,12.6568542 19.6568542,14 18,14 Z M9,11 C6.790861,11 5,9.209139 5,7 C5,4.790861 6.790861,3 9,3 C11.209139,3 13,4.790861 13,7 C13,9.209139 11.209139,11 9,11 Z" fill="#000000" fill-rule="nonzero" opacity="0.3"></path>
                                                                        <path d="M17.6011961,15.0006174 C21.0077043,15.0378534 23.7891749,16.7601418 23.9984937,20.4 C24.0069246,20.5466056 23.9984937,21 23.4559499,21 L19.6,21 C19.6,18.7490654 18.8562935,16.6718327 17.6011961,15.0006174 Z M0.00065168429,20.1992055 C0.388258525,15.4265159 4.26191235,13 8.98334134,13 C13.7712164,13 17.7048837,15.2931929 17.9979143,20.2 C18.0095879,20.3954741 17.9979143,21 17.2466999,21 C13.541124,21 8.03472472,21 0.727502227,21 C0.476712155,21 -0.0204617505,20.45918 0.00065168429,20.1992055 Z" fill="#000000" fill-rule="nonzero"></path>
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                            <div class="text-dark font-weight-bolder font-size-h2 mt-3">
                                                                <asp:Label ID="LblTotalPartnersCount2" Text="790" runat="server" />
                                                            </div>
                                                            <a href="#" class="text-muted text-hover-primary font-weight-bold font-size-lg mt-1">
                                                                <asp:Label ID="LblTotalPartnersText2" Text="Silver Partners" runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!--end::Tiles Widget 12-->
                                                </div>
                                                <div class="col-xl-3">
                                                    <!--begin::Tiles Widget 11-->
                                                    <div class="card card-custom gutter-b" style="height: 150px">
                                                        <div id="div2" runat="server" visible="false" class="card-body">
                                                            <span class="svg-icon svg-icon-3x svg-icon-primary">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Group.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <polygon points="0 0 24 0 24 24 0 24"></polygon>
                                                                        <path d="M18,14 C16.3431458,14 15,12.6568542 15,11 C15,9.34314575 16.3431458,8 18,8 C19.6568542,8 21,9.34314575 21,11 C21,12.6568542 19.6568542,14 18,14 Z M9,11 C6.790861,11 5,9.209139 5,7 C5,4.790861 6.790861,3 9,3 C11.209139,3 13,4.790861 13,7 C13,9.209139 11.209139,11 9,11 Z" fill="#000000" fill-rule="nonzero" opacity="0.3"></path>
                                                                        <path d="M17.6011961,15.0006174 C21.0077043,15.0378534 23.7891749,16.7601418 23.9984937,20.4 C24.0069246,20.5466056 23.9984937,21 23.4559499,21 L19.6,21 C19.6,18.7490654 18.8562935,16.6718327 17.6011961,15.0006174 Z M0.00065168429,20.1992055 C0.388258525,15.4265159 4.26191235,13 8.98334134,13 C13.7712164,13 17.7048837,15.2931929 17.9979143,20.2 C18.0095879,20.3954741 17.9979143,21 17.2466999,21 C13.541124,21 8.03472472,21 0.727502227,21 C0.476712155,21 -0.0204617505,20.45918 0.00065168429,20.1992055 Z" fill="#000000" fill-rule="nonzero"></path>
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                            <div class="text-dark font-weight-bolder font-size-h2 mt-3">
                                                                <asp:Label ID="LblTotalPartnersCount3" Text="0" runat="server" />
                                                            </div>
                                                            <a href="#" class="text-muted text-hover-primary font-weight-bold font-size-lg mt-1">
                                                                <asp:Label ID="LblTotalPartnersText3" Text="Gold Partners" runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!--end::Tiles Widget 11-->
                                                </div>
                                                <div class="col-xl-3">
                                                    <!--begin::Tiles Widget 12-->
                                                    <div class="card card-custom gutter-b" style="height: 150px">
                                                        <div id="div3" runat="server" visible="false" class="card-body">
                                                            <span class="svg-icon svg-icon-3x svg-icon-primary">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Group.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <polygon points="0 0 24 0 24 24 0 24"></polygon>
                                                                        <path d="M18,14 C16.3431458,14 15,12.6568542 15,11 C15,9.34314575 16.3431458,8 18,8 C19.6568542,8 21,9.34314575 21,11 C21,12.6568542 19.6568542,14 18,14 Z M9,11 C6.790861,11 5,9.209139 5,7 C5,4.790861 6.790861,3 9,3 C11.209139,3 13,4.790861 13,7 C13,9.209139 11.209139,11 9,11 Z" fill="#000000" fill-rule="nonzero" opacity="0.3"></path>
                                                                        <path d="M17.6011961,15.0006174 C21.0077043,15.0378534 23.7891749,16.7601418 23.9984937,20.4 C24.0069246,20.5466056 23.9984937,21 23.4559499,21 L19.6,21 C19.6,18.7490654 18.8562935,16.6718327 17.6011961,15.0006174 Z M0.00065168429,20.1992055 C0.388258525,15.4265159 4.26191235,13 8.98334134,13 C13.7712164,13 17.7048837,15.2931929 17.9979143,20.2 C18.0095879,20.3954741 17.9979143,21 17.2466999,21 C13.541124,21 8.03472472,21 0.727502227,21 C0.476712155,21 -0.0204617505,20.45918 0.00065168429,20.1992055 Z" fill="#000000" fill-rule="nonzero"></path>
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                            <div class="text-dark font-weight-bolder font-size-h2 mt-3">
                                                                <asp:Label ID="LblTotalPartnersCount4" Text="0" runat="server" />
                                                            </div>
                                                            <a href="#" class="text-muted text-hover-primary font-weight-bold font-size-lg mt-1">
                                                                <asp:Label ID="LblTotalPartnersText4" Text="Platinum Partners" runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!--end::Tiles Widget 12-->
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="row">
                                        <div class="col-4 d-flex flex-column">
                                            <!--begin::Block-->
                                            <div class="bg-light-warning p-8 rounded-xl flex-grow-1">
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon-network"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="Label2" Text="Total Partners" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblTotalPartners" Text="0" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanTotalPartners" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon-coins"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="Label6" Text="Total Deals Size" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblTotalDealSize" Text="0" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanTotalDealSize" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon-list-1"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="Label1" Text="Total Deals Registered" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblTotalDealRegistered" Text="0" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanTotalDealRegistered" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon-interface-5"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="Label18" Text="Leads Closed" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblLeadsClosedWon" Text="0" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanLeadsClosedWon" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="symbol symbol-circle symbol-white symbol-30 flex-shrink-0 mr-3">
                                                        <div class="symbol-label">
                                                            <i class="flaticon2-percentage"></i>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="flex-row-fluid mb-7">
                                                            <span class="font-size-sm font-weight-bold">
                                                                <asp:Label ID="Label10" Text="Win Rate" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-size-sm text-muted">
                                                                <asp:Label ID="LblLeadWinRate" Text="0/100" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanLeadWinRate" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="col-md-8">
                                                        <div class="flex-row-fluid mb-7">
                                                            <span id="spanDealsSizeAverage" runat="server" class="d-block font-weight-bold mb-4">
                                                                <asp:Label ID="LblDealsSizeAverageTitle" Text="Average Deal Size" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-weight-bolder">
                                                                <asp:Label ID="LblDealsSizeAverage" Text="0 Days" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanDealsSizeAverageProgress" runat="server" class="progress-bar bg-warning" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="col-md-8">
                                                        <div class="flex-row-fluid mb-7">
                                                            <span id="spanDealsSizeAverageWonDays" runat="server" class="d-block font-weight-bold mb-4">
                                                                <asp:Label ID="LblDealsSizeAverageWonDaysTitle" Text="Average Sales Cycle" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-weight-bolder">
                                                                <asp:Label ID="LblDealsSizeAverageWonDays" Text="0 Days" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanDealsSizeAverageWonDaysProgress" runat="server" class="progress-bar bg-success" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                                <!--begin::Item-->
                                                <div class="d-flex align-items-center mb-5">
                                                    <div class="col-md-8">
                                                        <div class="flex-row-fluid mb-7">
                                                            <span id="spanDealsWinRate" runat="server" class="d-block font-weight-bold mb-4">
                                                                <asp:Label ID="LblDealsWinRateTitle" Text="Partner Win Rate" runat="server" />
                                                            </span>
                                                            <span class="ml-3 font-weight-bolder">
                                                                <asp:Label ID="LblDealsWinRateCount" Text="0" runat="server" />
                                                            </span>
                                                            <div class="d-flex align-items-center pt-2">
                                                                <div class="progress progress-xs mt-2 mb-2 w-100">
                                                                    <div id="spanDealsWinRateProgress" runat="server" class="progress-bar bg-primary" role="progressbar" style="width: 78%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end::Item-->
                                            </div>
                                            <!--end::Item-->
                                            <!--end::Block-->
                                        </div>
                                        <div class="col-8">

                                            <telerik:RadHtmlChart runat="server" ID="ChrtTotalStatistics" Height="300px" Width="100%">
                                                <ChartTitle Text="Total Revenues">
                                                </ChartTitle>
                                                <PlotArea>
                                                    <Series>
                                                        <telerik:LineSeries DataFieldY="amount">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#3699FF" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                            <MarkersAppearance MarkersType="Circle" BackgroundColor="#3699FF" />
                                                            <TooltipsAppearance BackgroundColor="#3699FF" Color="White" DataFormatString="{0}" />
                                                        </telerik:LineSeries>
                                                        <telerik:AreaSeries DataFieldY="amount">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#FFF4DE" />
                                                                <Overlay Gradient="SharpBevel" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                            <MarkersAppearance MarkersType="Circle" BackgroundColor="#6993ff" />
                                                            <TooltipsAppearance BackgroundColor="#8950fc" Color="White" />
                                                        </telerik:AreaSeries>
                                                    </Series>
                                                    <XAxis MinValue="0" BaseUnit="Months" DashType="Dash" Color="#d0d0d5" MajorTickType="Outside" MinorTickType="Outside">
                                                        <MajorGridLines Color="#8950fc" Width="0" />
                                                        <MinorGridLines Color="#fff" Width="0" />
                                                        <Items>
                                                            <telerik:AxisItem LabelText="Jan" />
                                                            <telerik:AxisItem LabelText="Febr" />
                                                            <telerik:AxisItem LabelText="Mar" />
                                                            <telerik:AxisItem LabelText="Apr" />
                                                            <telerik:AxisItem LabelText="May" />
                                                            <telerik:AxisItem LabelText="Jun" />
                                                            <telerik:AxisItem LabelText="Jul" />
                                                            <telerik:AxisItem LabelText="Aug" />
                                                            <telerik:AxisItem LabelText="Sept" />
                                                            <telerik:AxisItem LabelText="Oct" />
                                                            <telerik:AxisItem LabelText="Nov" />
                                                            <telerik:AxisItem LabelText="Dec" />
                                                        </Items>
                                                        <TitleAppearance Position="Center" />
                                                    </XAxis>
                                                    <YAxis Color="#d0d0d5" Width="1" DashType="Dot">
                                                        <MajorGridLines Color="#e5eaee" DashType="Dot" Width="1" />
                                                        <MinorGridLines Color="#e5eaee" Width="0" />
                                                        <TitleAppearance Position="Center" Text="" />
                                                    </YAxis>
                                                </PlotArea>
                                                <Legend>
                                                    <Appearance Position="Bottom" BackgroundColor="Transparent" />
                                                </Legend>
                                            </telerik:RadHtmlChart>

                                            <div class="row">
                                                <div class="col-md-10">
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DrpTotalRevenues" AutoPostBack="true" OnSelectedIndexChanged="DrpTotalRevenues_SelectedIndexChanged" Width="100" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>

                                            <telerik:RadHtmlChart runat="server" ID="ChrtStatisticsByMonth" Height="300px" Width="100%">
                                                <ChartTitle Text="">
                                                </ChartTitle>
                                                <PlotArea>
                                                    <Series>
                                                        <telerik:LineSeries DataFieldY="amount">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#3699FF" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                            <MarkersAppearance MarkersType="Circle" BackgroundColor="#3699FF" />
                                                            <TooltipsAppearance BackgroundColor="#3699FF" Color="White" DataFormatString="{0}" />
                                                        </telerik:LineSeries>
                                                        <telerik:AreaSeries DataFieldY="amount">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#FFF4DE" />
                                                                <Overlay Gradient="SharpBevel" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                            <MarkersAppearance MarkersType="Circle" BackgroundColor="#6993ff" />
                                                            <TooltipsAppearance BackgroundColor="#8950fc" Color="White" />
                                                        </telerik:AreaSeries>
                                                    </Series>
                                                    <XAxis MinValue="0" BaseUnit="Months" DashType="Dash" Color="#d0d0d5" MajorTickType="Outside" MinorTickType="Outside">
                                                        <MajorGridLines Color="#8950fc" Width="0" />
                                                        <MinorGridLines Color="#fff" Width="0" />
                                                        <Items>
                                                            <telerik:AxisItem LabelText="Jan" />
                                                            <telerik:AxisItem LabelText="Febr" />
                                                            <telerik:AxisItem LabelText="Mar" />
                                                            <telerik:AxisItem LabelText="Apr" />
                                                            <telerik:AxisItem LabelText="May" />
                                                            <telerik:AxisItem LabelText="Jun" />
                                                            <telerik:AxisItem LabelText="Jul" />
                                                            <telerik:AxisItem LabelText="Aug" />
                                                            <telerik:AxisItem LabelText="Sept" />
                                                            <telerik:AxisItem LabelText="Oct" />
                                                            <telerik:AxisItem LabelText="Nov" />
                                                            <telerik:AxisItem LabelText="Dec" />
                                                        </Items>
                                                        <TitleAppearance Position="Center" />
                                                    </XAxis>
                                                    <YAxis Color="#d0d0d5" Width="1" DashType="Dot">
                                                        <MajorGridLines Color="#e5eaee" DashType="Dot" Width="1" />
                                                        <MinorGridLines Color="#e5eaee" Width="0" />
                                                        <TitleAppearance Position="Center" Text="" />
                                                    </YAxis>
                                                </PlotArea>
                                                <Legend>
                                                    <Appearance Position="Bottom" BackgroundColor="Transparent" />
                                                </Legend>
                                            </telerik:RadHtmlChart>

                                            <div class="row">
                                                <div class="col-md-10">
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DrpRevenuesByMonth" AutoPostBack="true" OnSelectedIndexChanged="DrpRevenuesByMonth_SelectedIndexChanged" Width="100" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 30px;">
                                                <div class="col-4">
                                                </div>
                                                <div class="col-4">
                                                    <a id="aViewStatisticsByPartners" runat="server" class="btn btn-light-warning">
                                                        <asp:Label ID="LblViewStatisticsByPartners" Text="View Statistics by Partner" runat="server" />
                                                    </a>
                                                </div>
                                                <div class="col-4"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Charts Widget 6-->
                        </div>
                    </div>
                    <!--end::Row-->

                    <!--begin::Row-->
                    <div class="row">
                        <div class="col-xl-8">
                            <div class="card card-custom card-stretch gutter-b">
                                <div class="card-header flex-wrap py-5">
                                    <div class="card-title">
                                        <h3 class="card-label">
                                            <asp:Label ID="LblMostActive" Text="Most Active Partners" runat="server" />
                                        </h3>
                                    </div>
                                    <div class="card-toolbar">
                                        <!--begin::Button-->
                                        <a id="aChannelPartnerViewAll" runat="server" class="btn btn-primary font-weight-bolder">
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
                                            <asp:Label ID="LblChannelPartnerViewAll" Text="View All" runat="server" />
                                        </a>
                                        <!--end::Button-->
                                    </div>
                                </div>
                                <div class="card-body">
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
                                            <MasterTableView Name="Parent">
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
                                    <controls:MessageControl ID="UcSendMessageAlertConfirmed" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4">
                            <div class="card card-custom card-stretch gutter-b">
                                <div class="card-header flex-wrap py-5">
                                    <div class="card-title">
                                        <h3 class="card-label">
                                            <asp:Label ID="Label3" Text="Partners/Country" runat="server" />
                                        </h3>
                                    </div>
                                    <div class="card-toolbar">
                                    </div>
                                </div>
                                <div class="card-body">
                                    <!--begin: Datatable-->
                                    <table class="table table-separate table-head-custom table-checkable" id="kt_datatable1">
                                        <thead>
                                            <tr style="text-align: center;">
                                                <td>Country
                                                </td>
                                                <td>Partners
                                                </td>
                                            </tr>
                                        </thead>
                                        <asp:Repeater ID="RptPartnerCountries" OnLoad="RptPartnerCountries_Load" runat="server">
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr style="">
                                                        <td style="">
                                                            <div class="symbol symbol-50 symbol-light mr-2 ml-8" style="padding-right: 15px; float: left;">
                                                                <span class="symbol-label">
                                                                    <asp:Image ID="ImgCountryLogo" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "country_flag")%>' runat="server" alt="" />
                                                                </span>
                                                            </div>
                                                            <div class="mt-5">
                                                                <asp:Label ID="LblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "country_name")%>' runat="server" />
                                                            </div>
                                                        </td>
                                                        <td style="text-align: center; vertical-align: middle;">
                                                            <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                <asp:Label ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "partners")%>' runat="server" />
                                                            </span>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <!--end: Datatable-->
                                    <controls:MessageControl ID="UcPartnerCountriesMessageAlert" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end::Row-->

                    <!--begin::Row-->
                    <div class="row">
                        <div id="divDealsChart" runat="server" class="col-xl-6">
                            <!--begin::Charts Widget 1-->
                            <div class="card card-custom card-stretch gutter-b">
                                <!--begin::Header-->
                                <div class="card-header h-auto border-0">
                                    <!--begin::Title-->
                                    <div class="card-title py-5">
                                        <h3 class="card-label">
                                            <span class="d-block text-dark font-weight-bolder">Deals Registered/Closed</span>
                                            <span class="d-block text-muted mt-2 font-size-sm"></span>
                                        </h3>
                                    </div>
                                    <!--end::Title-->
                                    <!--begin::Toolbar-->
                                    <div class="card-toolbar">
                                        <asp:DropDownList ID="DrpRegisteredDeals" AutoPostBack="true" OnSelectedIndexChanged="DrpRegisteredDeals_SelectedIndexChanged" Width="100" CssClass="form-control" runat="server" />
                                    </div>
                                    <!--end::Toolbar-->
                                </div>
                                <!--end::Header-->
                                <!--begin::Body-->
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-xl-12">
                                            <!--begin::Chart-->
                                            <telerik:RadHtmlChart runat="server" ID="ChrtRegisteredDeals" BackColor="White" Height="300px">
                                                <PlotArea>
                                                    <Series>
                                                        <telerik:ColumnSeries DataFieldY="count" Gap="2" Spacing="0.5" Name="Number of Deals Registered">
                                                            <TooltipsAppearance Color="White" />
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#1bc5bd" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                        </telerik:ColumnSeries>
                                                        <telerik:ColumnSeries DataFieldY="count_by_status" Gap="2" Spacing="0.5" Name="Number of Deals Closed">
                                                            <TooltipsAppearance Color="White" />
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#e5eaee" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                        </telerik:ColumnSeries>
                                                    </Series>
                                                    <XAxis DataLabelsField="month" DashType="Dash" Width="1">
                                                        <MajorGridLines Color="#e5eaee" Width="0" />
                                                        <MinorGridLines Color="#fff" Width="0" />
                                                        <TitleAppearance Position="Center" />
                                                        <Items>
                                                            <telerik:AxisItem LabelText="Jan" />
                                                            <telerik:AxisItem LabelText="Febr" />
                                                            <telerik:AxisItem LabelText="Mar" />
                                                            <telerik:AxisItem LabelText="Apr" />
                                                            <telerik:AxisItem LabelText="May" />
                                                            <telerik:AxisItem LabelText="Jun" />
                                                            <telerik:AxisItem LabelText="Jul" />
                                                            <telerik:AxisItem LabelText="Aug" />
                                                            <telerik:AxisItem LabelText="Sept" />
                                                            <telerik:AxisItem LabelText="Oct" />
                                                            <telerik:AxisItem LabelText="Nov" />
                                                            <telerik:AxisItem LabelText="Dec" />
                                                        </Items>
                                                    </XAxis>
                                                    <YAxis Step="1" DashType="Dash" Width="0">
                                                        <MajorGridLines Color="#e5eaee" DashType="Dash" Width="1" />
                                                        <MinorGridLines Color="#e5eaee" Width="0" />
                                                        <TitleAppearance Position="Center" Text="" />
                                                    </YAxis>
                                                </PlotArea>
                                                <Legend>
                                                    <Appearance Position="Bottom" />
                                                </Legend>
                                            </telerik:RadHtmlChart>
                                            <!--end::Chart-->
                                        </div>
                                    </div>
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Charts Widget 1-->
                        </div>
                        <div id="divLeadsChart" runat="server" class="col-xl-6">
                            <!--begin::Charts Widget 2-->
                            <div class="card card-custom bg-gray-100 card-stretch gutter-b">
                                <!--begin::Header-->
                                <div class="card-header h-auto border-0">
                                    <!--begin::Title-->
                                    <div class="card-title py-5">
                                        <h3 class="card-label">
                                            <span class="d-block text-dark font-weight-bolder">Leads Registered/Closed</span>
                                            <span class="d-block text-dark-50 mt-2 font-size-sm"></span>
                                        </h3>
                                    </div>
                                    <!--end::Title-->
                                    <!--begin::Toolbar-->
                                    <div class="card-toolbar">
                                        <asp:DropDownList ID="DrpRegisteredLeads" AutoPostBack="true" OnSelectedIndexChanged="DrpRegisteredLeads_SelectedIndexChanged" Width="100" CssClass="form-control" runat="server" />
                                    </div>
                                    <!--end::Toolbar-->
                                </div>
                                <!--end::Header-->
                                <!--begin::Body-->
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-xl-12">
                                            <!--begin::Chart-->
                                            <telerik:RadHtmlChart runat="server" ID="ChrtRegisteredLeads" Height="300px">
                                                <ChartTitle Text="">
                                                </ChartTitle>
                                                <PlotArea>
                                                    <Series>
                                                        <telerik:ColumnSeries DataFieldY="count" Gap="2" Spacing="0.5" Name="Number of Leads Registered">
                                                            <TooltipsAppearance Color="White" />
                                                            <LabelsAppearance Visible="false" />
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#ffa800" />
                                                            </Appearance>
                                                        </telerik:ColumnSeries>
                                                        <telerik:ColumnSeries DataFieldY="count_by_status" Gap="2" Spacing="0.5" Name="Number of Leads Closed">
                                                            <TooltipsAppearance Color="White" />
                                                            <LabelsAppearance Visible="false" />
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#e5eaee" />
                                                            </Appearance>
                                                        </telerik:ColumnSeries>
                                                    </Series>
                                                    <XAxis DataLabelsField="month" DashType="Dash" Width="1">
                                                        <MajorGridLines Color="#fff" Width="0" />
                                                        <MinorGridLines Color="#fff" Width="0" />
                                                        <TitleAppearance Position="Center" />
                                                        <Items>
                                                            <telerik:AxisItem LabelText="Jan" />
                                                            <telerik:AxisItem LabelText="Febr" />
                                                            <telerik:AxisItem LabelText="Mar" />
                                                            <telerik:AxisItem LabelText="Apr" />
                                                            <telerik:AxisItem LabelText="May" />
                                                            <telerik:AxisItem LabelText="Jun" />
                                                            <telerik:AxisItem LabelText="Jul" />
                                                            <telerik:AxisItem LabelText="Aug" />
                                                            <telerik:AxisItem LabelText="Sept" />
                                                            <telerik:AxisItem LabelText="Oct" />
                                                            <telerik:AxisItem LabelText="Nov" />
                                                            <telerik:AxisItem LabelText="Dec" />
                                                        </Items>
                                                    </XAxis>
                                                    <YAxis Step="1" Width="0">
                                                        <MajorGridLines Color="#e5eaee" DashType="Dash" Width="1" />
                                                        <MinorGridLines Color="#e5eaee" Width="0" />
                                                        <TitleAppearance Position="Center" Text="" />
                                                    </YAxis>
                                                </PlotArea>
                                                <Legend>
                                                    <Appearance Position="Bottom" />
                                                </Legend>
                                            </telerik:RadHtmlChart>
                                            <!--end::Chart-->
                                        </div>
                                    </div>
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Charts Widget 2-->
                        </div>
                    </div>
                    <!--end::Row-->

                    <!--begin::Row-->
                    <div class="row">
                        <div class="col-xl-12">
                            <!--begin::Charts Widget 6-->
                            <div class="card card-custom card-stretch gutter-b">
                                <!--begin::Header-->
                                <div class="card-header h-auto border-0">
                                    <div class="card-title py-5">
                                        <h3 class="card-label">
                                            <span class="d-block text-dark font-weight-bolder">
                                                <asp:Label ID="LblForecastingTitle" Text="Forecasting" runat="server" />
                                            </span>
                                            <span class="d-block text-muted mt-2 font-size-sm"></span>
                                        </h3>
                                    </div>
                                    <div class="card-toolbar">
                                    </div>
                                </div>
                                <!--end::Header-->
                                <!--begin::Body-->
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-12">
                                            <telerik:RadHtmlChart runat="server" ID="ChrtForecasting" Height="400px" Width="100%">
                                                <ChartTitle Text="">
                                                </ChartTitle>
                                                <PlotArea>
                                                    <Series>
                                                        <telerik:LineSeries DataFieldY="count" Name="Future expected deals based on your partner Deal Registrations">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#6993ff" />
                                                            </Appearance>
                                                            <LabelsAppearance Visible="false" />
                                                            <MarkersAppearance MarkersType="Circle" BackgroundColor="#5472d2" />
                                                            <TooltipsAppearance BackgroundColor="#6993ff" Color="White" DataFormatString="{0} deals" />
                                                        </telerik:LineSeries>
                                                    </Series>
                                                    <XAxis DataLabelsField="amount" DashType="Dash" Width="1">
                                                        <MajorGridLines Color="#fff" Width="0" />
                                                        <MinorGridLines Color="#fff" Width="0" />
                                                    </XAxis>
                                                    <YAxis Step="1" Width="0">
                                                        <MajorGridLines Color="#e5eaee" DashType="Dash" Width="1" />
                                                        <MinorGridLines Color="#e5eaee" Width="0" />
                                                        <TitleAppearance Position="Center" Text="" />
                                                    </YAxis>
                                                </PlotArea>
                                                <Legend>
                                                    <Appearance Position="Top" />
                                                </Legend>
                                            </telerik:RadHtmlChart>
                                        </div>
                                    </div>
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Charts Widget 6-->
                        </div>
                    </div>
                    <!--end::Row-->

                    <!--begin::Row-->
                    <div id="divChartForecasting" runat="server" visible="false" class="row">
                        <div class="col-xl-12">
                            <!--begin::Charts Widget 6-->
                            <div class="card card-custom card-stretch gutter-b">
                                <!--begin::Header-->
                                <div class="card-header h-auto border-0">
                                    <div class="card-title py-5">
                                        <h3 class="card-label">
                                            <span class="d-block text-dark font-weight-bolder">Forecasting</span>
                                            <span class="d-block text-muted mt-2 font-size-sm"></span>
                                        </h3>
                                    </div>
                                    <div class="card-toolbar">
                                        <span class="mr-5 d-flex align-items-center font-weight-bold">
                                            <i class="label label-dot label-xl label-primary mr-2"></i>Sales</span>
                                        <span class="d-flex align-items-center font-weight-bold">
                                            <i class="label label-dot label-xl label-info mr-2"></i>Authors</span>
                                    </div>
                                </div>
                                <!--end::Header-->
                                <!--begin::Body-->
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-12">
                                            <telerik:RadHtmlChart Visible="false" runat="server" ID="RdChrtForeCasting" Height="400px" Width="100%">
                                            </telerik:RadHtmlChart>
                                        </div>
                                    </div>
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Charts Widget 6-->
                        </div>
                    </div>
                    <!--end::Row-->
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

        <!--begin::Page Scripts(used by this page)-->
        <script src="/assets/js/pages/widgets.js"></script>
        <!--end::Page Scripts-->

    </telerik:RadScriptBlock>

</asp:Content>
