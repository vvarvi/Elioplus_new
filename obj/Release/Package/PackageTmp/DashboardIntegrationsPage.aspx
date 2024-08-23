<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardIntegrationsPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardIntegrationsPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Modals/AddToTeamForm.ascx" TagName="UcAddToTeam" TagPrefix="controls" %>
<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DANotificationControl.ascx" TagName="NotificationControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>

    <telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">
        <!--begin::Entry-->
        <div class="d-flex flex-column-fluid">
            <!--begin::Container-->
            <div class="container">
                <!--begin::Dashboard-->
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <!--begin::Entry-->
                        <div class="d-flex flex-column-fluid">
                            <!--begin::Container-->
                            <div class="container">

                                <div class="card-body">
                                    <div class="row">
                                        <div class="card-header" style="width: 100%; margin-bottom: 20px;">
                                            <h3 class="card-title" style="margin-bottom: 0px;">
                                                <asp:Label ID="Label1" Text="Integrations" runat="server" />
                                            </h3>
                                        </div>

                                        <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                            <asp:Repeater ID="RdgResults" OnLoad="RdgResults_Load" OnItemDataBound="RdgResults_OnItemDataBound" runat="server">
                                                <ItemTemplate>
                                                    <!--begin::Row-->
                                                    <div class="row">
                                                        <!--begin::Column-->
                                                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6">
                                                            <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                                <!--begin::Body-->
                                                                <div class="card-body text-center pt-4">
                                                                    <!--begin::Toolbar-->
                                                                    <div class="d-flex justify-content-end">
                                                                    </div>
                                                                    <!--end::Toolbar-->
                                                                    <!--begin::User-->
                                                                    <div class="mt-7">
                                                                        <div class="symbol symbol-circle symbol-lg-90">
                                                                            <img id="img1" runat="server" src='<%# DataBinder.Eval(Container.DataItem, "crm_logo1")%>' alt="image" />
                                                                        </div>
                                                                    </div>
                                                                    <!--end::User-->
                                                                    <!--begin::Name-->
                                                                    <div class="my-4">
                                                                        <a href="#" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                                            <%# DataBinder.Eval(Container.DataItem, "crm_name1")%>
                                                                        </a>
                                                                    </div>
                                                                    <!--end::Name-->
                                                                    <!--begin::Label-->
                                                                    <a id="aApiKeyInfo1" runat="server" class="btn btn-text btn-light-warning btn-sm font-weight-bold" style="cursor: pointer;">
                                                                        <asp:Label ID="LblApiKeyInfo1" Text="How do i find my Api Key" runat="server" />
                                                                    </a>
                                                                    <telerik:RadToolTip ID="RttApiKeyInfo1" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "api_key_description1")%>' TargetControlID="aApiKeyInfo1" Width="400" Font-Size="X-Small" AutoCloseDelay="10000" HideEvent="ManualClose" Position="TopRight" Animation="Fade" runat="server" />
                                                                    <!--end::Label-->
                                                                    <!--begin::Buttons-->
                                                                    <div class="mt-9">
                                                                        <asp:Button ID="ImgAddApiKey1" runat="server" Text="Add Your Api Key" OnClick="ImgAddApiKey1_ServerClick" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase" />
                                                                    </div>
                                                                    <!--end::Buttons-->
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsActive1" Value='<%# Eval("is_active1") %>' runat="server" />
                                                                </div>
                                                                <!--end::Body-->
                                                            </div>
                                                        </div>
                                                        <!--end::Column-->
                                                        <!--begin::Column-->
                                                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6">
                                                            <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                                <!--begin::Body-->
                                                                <div class="card-body text-center pt-4">
                                                                    <!--begin::Toolbar-->
                                                                    <div class="d-flex justify-content-end">
                                                                    </div>
                                                                    <!--end::Toolbar-->
                                                                    <!--begin::User-->
                                                                    <div class="mt-7">
                                                                        <div class="symbol symbol-circle symbol-lg-90">
                                                                            <img id="img2" runat="server" src='<%# DataBinder.Eval(Container.DataItem, "crm_logo2")%>' alt="image" />
                                                                        </div>
                                                                    </div>
                                                                    <!--end::User-->
                                                                    <!--begin::Name-->
                                                                    <div class="my-4">
                                                                        <a href="#" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                                            <%# DataBinder.Eval(Container.DataItem, "crm_name2")%>
                                                                        </a>
                                                                    </div>
                                                                    <!--end::Name-->
                                                                    <!--begin::Label-->
                                                                    <a id="aApiKeyInfo2" runat="server" class="btn btn-text btn-light-warning btn-sm font-weight-bold" style="cursor: pointer;">
                                                                        <asp:Label ID="LblApiKeyInfo2" Text="How do i find my Api Key" runat="server" />
                                                                    </a>
                                                                    <telerik:RadToolTip ID="RttApiKeyInfo2" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "api_key_description2")%>' TargetControlID="aApiKeyInfo2" Width="400" Font-Size="X-Small" AutoCloseDelay="10000" HideEvent="ManualClose" Position="TopRight" Animation="Fade" runat="server" />
                                                                    <!--end::Label-->
                                                                    <!--begin::Buttons-->
                                                                    <div class="mt-9">
                                                                        <asp:Button ID="ImgAddApiKey2" runat="server" Text="Add Your Api Key" OnClick="ImgAddApiKey2_ServerClick" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase" />
                                                                    </div>
                                                                    <!--end::Buttons-->
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsActive2" Value='<%# Eval("is_active2") %>' runat="server" />
                                                                </div>
                                                                <!--end::Body-->
                                                            </div>
                                                        </div>
                                                        <!--end::Column-->
                                                        <!--begin::Column-->
                                                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6">
                                                            <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                                <!--begin::Body-->
                                                                <div class="card-body text-center pt-4">
                                                                    <!--begin::Toolbar-->
                                                                    <div class="d-flex justify-content-end">
                                                                    </div>
                                                                    <!--end::Toolbar-->
                                                                    <!--begin::User-->
                                                                    <div class="mt-7">
                                                                        <div class="symbol symbol-circle symbol-lg-90">
                                                                            <img id="img3" runat="server" src='<%# DataBinder.Eval(Container.DataItem, "crm_logo3")%>' alt="image" />
                                                                        </div>
                                                                    </div>
                                                                    <!--end::User-->
                                                                    <!--begin::Name-->
                                                                    <div class="my-4">
                                                                        <a href="#" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                                            <%# DataBinder.Eval(Container.DataItem, "crm_name3")%>
                                                                        </a>
                                                                    </div>
                                                                    <!--end::Name-->
                                                                    <!--begin::Label-->
                                                                    <a id="aApiKeyInfo3" runat="server" class="btn btn-text btn-light-warning btn-sm font-weight-bold" style="cursor: pointer;">
                                                                        <asp:Label ID="LblApiKeyInfo3" Text="How do i find my Api Key" runat="server" />
                                                                    </a>
                                                                    <telerik:RadToolTip ID="RttApiKeyInfo3" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "api_key_description3")%>' TargetControlID="aApiKeyInfo3" Width="400" Font-Size="X-Small" AutoCloseDelay="10000" HideEvent="ManualClose" Position="TopRight" Animation="Fade" runat="server" />
                                                                    <!--end::Label-->
                                                                    <!--begin::Buttons-->
                                                                    <div class="mt-9">
                                                                        <asp:Button ID="ImgAddApiKey3" runat="server" Text="Add Your Api Key" OnClick="ImgAddApiKey3_ServerClick" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase" />
                                                                    </div>
                                                                    <!--end::Buttons-->
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsActive3" Value='<%# Eval("is_active3") %>' runat="server" />
                                                                </div>
                                                                <!--end::Body-->
                                                            </div>
                                                        </div>
                                                        <!--end::Column-->
                                                    </div>
                                                    <!--end::Row-->
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <!--begin::Pagination-->
                                        <div id="divPaginationResults" runat="server" visible="false" class="d-flex justify-content-between align-items-center flex-wrap">
                                            <div class="d-flex flex-wrap mr-3">
                                                <a href="#" class="btn btn-icon btn-sm btn-light-primary mr-2 my-1">
                                                    <i class="ki ki-bold-double-arrow-back icon-xs"></i>
                                                </a>
                                                <a href="#" class="btn btn-icon btn-sm btn-light-primary mr-2 my-1">
                                                    <i class="ki ki-bold-arrow-back icon-xs"></i>
                                                </a>
                                                <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">...</a>
                                                <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">23</a>
                                                <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary active mr-2 my-1">24</a>
                                                <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">25</a>
                                                <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">26</a>
                                                <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">27</a>
                                                <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">28</a>
                                                <a href="#" class="btn btn-icon btn-sm border-0 btn-hover-primary mr-2 my-1">...</a>
                                                <a href="#" class="btn btn-icon btn-sm btn-light-primary mr-2 my-1">
                                                    <i class="ki ki-bold-arrow-next icon-xs"></i>
                                                </a>
                                                <a href="#" class="btn btn-icon btn-sm btn-light-primary mr-2 my-1">
                                                    <i class="ki ki-bold-double-arrow-next icon-xs"></i>
                                                </a>
                                            </div>
                                            <div class="d-flex align-items-center">
                                                <select class="form-control form-control-sm text-primary font-weight-bold mr-4 border-0 bg-light-primary" style="width: 75px;">
                                                    <option value="10">10</option>
                                                    <option value="20">20</option>
                                                    <option value="30">30</option>
                                                    <option value="50">50</option>
                                                    <option value="100">100</option>
                                                </select>
                                                <span class="text-muted">Displaying 10 of 230 records</span>
                                            </div>
                                        </div>
                                        <!--end::Pagination-->
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <controls:MessageControl ID="UcMessageAlert" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="card-header" style="width: 100%; margin-bottom: 20px;">
                                            <h3 class="card-title" style="margin-bottom: 0px;">
                                                <asp:Label ID="LblUserTitle" Text="My Integrations" runat="server" />
                                            </h3>
                                        </div>

                                        <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                            <asp:Repeater ID="RdgUserResults" OnLoad="RdgUserResults_Load" OnItemDataBound="RdgUserResults_ItemDataBound" runat="server">
                                                <ItemTemplate>
                                                    <!--begin::Row-->
                                                    <div class="row">
                                                        <!--begin::Column-->
                                                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6">
                                                            <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                                <!--begin::Body-->
                                                                <div class="card-body text-center pt-4">
                                                                    <!--begin::Toolbar-->
                                                                    <div class="d-flex justify-content-end">
                                                                    </div>
                                                                    <!--end::Toolbar-->
                                                                    <!--begin::User-->
                                                                    <div class="mt-7">
                                                                        <div class="symbol symbol-circle symbol-lg-90">
                                                                            <img id="img1" runat="server" src='<%# DataBinder.Eval(Container.DataItem, "crm_logo1")%>' alt="image" />
                                                                        </div>
                                                                    </div>
                                                                    <!--end::User-->
                                                                    <!--begin::Name-->
                                                                    <div class="my-4">
                                                                        <a href="#" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                                            <%# DataBinder.Eval(Container.DataItem, "crm_name1")%>
                                                                        </a>
                                                                    </div>
                                                                    <!--end::Name-->
                                                                    <!--begin::Buttons-->
                                                                    <div class="mt-9">
                                                                        <asp:Button ID="ImgEditApiKey1" runat="server" Text="Edit Your Api Key" OnClick="ImgEditApiKey1_ServerClick" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase" />
                                                                    </div>
                                                                    <div class="mt-3">
                                                                        <asp:Button ID="ImgBtnRemove1" runat="server" Text="Remove Integration" OnClick="ImgBtnRemove1_ServerClick" class="btn btn-light-danger font-weight-bolder btn-sm py-3 px-6 text-uppercase" />
                                                                    </div>
                                                                    <!--end::Buttons-->
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserApiKey1" Value='<%# Eval("user_api_key1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCrmUserIntegrationsId1" Value='<%# Eval("crm_user_integrations_id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsActive1" Value='<%# Eval("is_active1") %>' runat="server" />
                                                                </div>
                                                                <!--end::Body-->
                                                            </div>
                                                        </div>
                                                        <!--end::Column-->
                                                        <!--begin::Column-->
                                                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6">
                                                            <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                                <!--begin::Body-->
                                                                <div class="card-body text-center pt-4">
                                                                    <!--begin::Toolbar-->
                                                                    <div class="d-flex justify-content-end">
                                                                    </div>
                                                                    <!--end::Toolbar-->
                                                                    <!--begin::User-->
                                                                    <div class="mt-7">
                                                                        <div class="symbol symbol-circle symbol-lg-90">
                                                                            <img id="img2" runat="server" src='<%# DataBinder.Eval(Container.DataItem, "crm_logo2")%>' alt="image" />
                                                                        </div>
                                                                    </div>
                                                                    <!--end::User-->
                                                                    <!--begin::Name-->
                                                                    <div class="my-4">
                                                                        <a href="#" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                                            <%# DataBinder.Eval(Container.DataItem, "crm_name2")%>
                                                                        </a>
                                                                    </div>
                                                                    <!--end::Name-->
                                                                    <!--begin::Buttons-->
                                                                    <div class="mt-9">
                                                                        <asp:Button ID="ImgEditApiKey2" runat="server" Text="Edit Your Api Key" OnClick="ImgEditApiKey2_ServerClick" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase" />
                                                                    </div>
                                                                    <div class="mt-3">
                                                                        <asp:Button ID="ImgBtnRemove2" runat="server" Text="Remove Integration" OnClick="ImgBtnRemove2_ServerClick" class="btn btn-light-danger font-weight-bolder btn-sm py-3 px-6 text-uppercase" />
                                                                    </div>
                                                                    <!--end::Buttons-->
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserApiKey2" Value='<%# Eval("user_api_key2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCrmUserIntegrationsId2" Value='<%# Eval("crm_user_integrations_id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsActive2" Value='<%# Eval("is_active2") %>' runat="server" />
                                                                </div>
                                                                <!--end::Body-->
                                                            </div>
                                                        </div>
                                                        <!--end::Column-->
                                                        <!--begin::Column-->
                                                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6">
                                                            <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                                <!--begin::Body-->
                                                                <div class="card-body text-center pt-4">
                                                                    <!--begin::Toolbar-->
                                                                    <div class="d-flex justify-content-end">
                                                                    </div>
                                                                    <!--end::Toolbar-->
                                                                    <!--begin::User-->
                                                                    <div class="mt-7">
                                                                        <div class="symbol symbol-circle symbol-lg-90">
                                                                            <img id="img3" runat="server" src='<%# DataBinder.Eval(Container.DataItem, "crm_logo3")%>' alt="image" />
                                                                        </div>
                                                                    </div>
                                                                    <!--end::User-->
                                                                    <!--begin::Name-->
                                                                    <div class="my-4">
                                                                        <a href="#" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                                            <%# DataBinder.Eval(Container.DataItem, "crm_name3")%>
                                                                        </a>
                                                                    </div>
                                                                    <!--end::Name-->
                                                                    <!--begin::Buttons-->
                                                                    <div class="mt-9">
                                                                        <asp:Button ID="ImgEditApiKey3" runat="server" Text="Edit Your Api Key" OnClick="ImgEditApiKey1_ServerClick" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase" />
                                                                    </div>
                                                                    <div class="mt-3">
                                                                        <asp:Button ID="ImgBtnRemove3" runat="server" Text="Remove Integration" OnClick="ImgBtnRemove3_ServerClick" class="btn btn-light-danger font-weight-bolder btn-sm py-3 px-6 text-uppercase" />
                                                                    </div>
                                                                    <!--end::Buttons-->
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserApiKey3" Value='<%# Eval("user_api_key3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCrmUserIntegrationsId3" Value='<%# Eval("crm_user_integrations_id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsActive3" Value='<%# Eval("is_active3") %>' runat="server" />
                                                                </div>
                                                                <!--end::Body-->
                                                            </div>
                                                        </div>
                                                        <!--end::Column-->
                                                    </div>
                                                    <!--end::Row-->
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <controls:MessageControl ID="MessageControlUserApiKey" runat="server" />

                                    </div>
                                </div>

                            </div>
                            <!--end::Container-->
                        </div>
                        <!--end::Entry-->
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!--end::Dashboard-->
            </div>
            <!--end::Container-->
        </div>
        <!--end::Entry-->

        <!-- Invitation form (modal view) -->
        <div id="InvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <controls:UcAddToTeam ID="UcAddToTeamForm" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- Leads Service Modal -->
        <div id="LeadsServiceModal" class="modal fade" tabindex="-1" data-width="200">
            <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                <ContentTemplate>
                    <div role="document" class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="Label8" Text="Add Api Key" runat="server" /></h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <i aria-hidden="true" class="ki ki-close"></i>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="divFreemiumArea" visible="false" runat="server">
                                            <div class="form-group">
                                                <controls:MessageControl ID="FreemiumMessageControl" Visible="true" runat="server" />
                                            </div>
                                        </div>
                                        <div id="divPremiumArea" runat="server">
                                            <div id="divApiKeyArea" runat="server">
                                                <div class="form-group">
                                                    <asp:TextBox ID="TbxApiKey" placeholder="Client Key" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:TextBox ID="TbxApiSecretKey" Visible="false" placeholder="Secret Key" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 25px;">
                                    <div id="divCrmErrorMessage" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show">
                                        <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                        <div class="alert-text">
                                            <strong>
                                                <asp:Label ID="LblLeadCrmError" Text="Error! " runat="server" />
                                            </strong>
                                            <asp:Label ID="LblLeadCrmErrorMessage" runat="server" />
                                        </div>
                                        <div class="alert-close">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                            </button>
                                        </div>
                                    </div>

                                    <div id="divCrmSuccessMessage" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show">
                                        <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                        <div class="alert-text">
                                            <strong>
                                                <asp:Label ID="LblLeadCrmSuccess" Text="Done! " runat="server" />
                                            </strong>
                                            <asp:Label ID="LblLeadCrmSuccessMessage" runat="server" />
                                        </div>
                                        <div class="alert-close">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                                <asp:Button ID="BtnSaveApiKey" OnClick="BtnSaveApiKey_Click" runat="server" CssClass="btn btn-primary" Text="Save" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- Remove Crm Confirmation Modal -->
        <div id="CrmRemoveConfirmationModal" class="modal fade" tabindex="-1" data-width="200">
            <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                <ContentTemplate>
                    <div role="document" class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="LblCrmRemove" Text="Remove Integration" runat="server" /></h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <i aria-hidden="true" class="ki ki-close"></i>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="div2" runat="server">
                                            <div class="form-group">
                                                <controls:MessageControl ID="MessageControlCrmRemove" runat="server" />
                                            </div>
                                        </div>
                                        <div id="div3" runat="server">
                                            <div class="form-group">
                                                <div id="div4" runat="server" class="" style="width: 100%; text-align: justify;">

                                                    <asp:Label ID="LblCrmRemoveText" Text="Are you sure you want to completely remove your crm integration?" runat="server" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 25px;">
                                    <div id="divCrmRemoveErrorMessage" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show">
                                        <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                        <div class="alert-text">
                                            <strong>
                                                <asp:Label ID="LblLeadCrmRemoveError" Text="Error! " runat="server" />
                                            </strong>
                                            <asp:Label ID="LblLeadCrmRemoveErrorMessage" runat="server" />
                                        </div>

                                        <div class="alert-close">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                            </button>
                                        </div>
                                    </div>

                                    <div id="divCrmRemoveSuccessMessage" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show">
                                        <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                        <div class="alert-text">
                                            <strong>
                                                <asp:Label ID="LblLeadCrmRemoveSuccess" Text="Done! " runat="server" />
                                            </strong>
                                            <asp:Label ID="LblLeadCrmRemoveSuccessMessage" runat="server" />
                                        </div>
                                        <div class="alert-close">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                            </button>
                                        </div>
                                        <div class="alert-close">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" data-dismiss="modal" class="btn btn-danger">No</button>
                                <asp:Button ID="BtnRemove" OnClick="BtnRemove_Click" Text="Yes" runat="server" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="loader" style="display: none;">
            <div id="loadermsg">
            </div>
        </div>
    </telerik:RadAjaxPanel>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
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

            function CloseInvitationPopUp() {
                var x = $('#InvitationModal').modal('hide');
                console.log(x);
            }

            function BtnCancelMessage_OnClientClick(sender, e) {
                var x = $('#InvitationModal').modal('hide');
            }

            function OpenLeadsServiceModal() {
                $('#LeadsServiceModal').modal('show');
            }

            function CloseLeadsServiceModal() {
                $('#LeadsServiceModal').modal('hide');
            }

            function OpenCrmRemoveConfirmationModal() {
                $('#CrmRemoveConfirmationModal').modal('show');
            }

            function CloseCrmRemoveConfirmationModal() {
                $('#CrmRemoveConfirmationModal').modal('hide');
            }
        </script>
        <style>
            .RadGrid_MetroTouch .rgAltRow {
                background-color: transparent !important;
                height: 365px;
            }

            .RadGrid_MetroTouch .rgRow {
                background-color: transparent !important;
                height: 365px;
            }
        </style>
    </telerik:RadScriptBlock>

</asp:Content>
