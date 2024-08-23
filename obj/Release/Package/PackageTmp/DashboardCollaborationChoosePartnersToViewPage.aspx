<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardCollaborationChoosePartnersToViewPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardCollaborationChoosePartnersToViewPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DANotificationControl.ascx" TagName="NotificationControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--begin::Dashboard-->
                    <!--begin::Head-->
                    <div class="card card-custom gutter-b">
                        <!--begin::Body-->
                        <div class="card-body align-items-center justify-content-between flex-wrap py-3" style="padding: 2rem 1.75rem !important;">
                            <!--begin::Info-->
                            <div class="d-flex align-items-center mr-2 py-2">
                                <!--begin::Navigation-->
                                <div class="col-lg-12">
                                    <!--begin::Navi-->

                                    <div id="divVendorGroupsArea" visible="false" runat="server" class="row align-items-center">
                                        <div class="col-lg-8">
                                            <div class="row align-items-center">
                                                <div class="col-md-6">
                                                    <div class="d-flex align-items-center">
                                                        <asp:DropDownList ID="DdlLibraryGroups" AutoPostBack="true" OnSelectedIndexChanged="DdlLibraryGroups_SelectedIndexChanged" Width="500" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="input-icon">
                                                        <asp:TextBox ID="TbxLibraryGroupName" placeholder="Group Name" Width="400" CssClass="form-control" runat="server" />
                                                        <span>
                                                            <i class="flaticon2-search-1 text-muted"></i>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <a id="aBtnCreateGroup" runat="server" onserverclick="BtnCreateGroup_Click" class="btn btn-light-primary px-6 font-weight-bold">
                                                <asp:Label ID="LblCreateGroup" Text="Create new Group" runat="server" />
                                            </a>
                                            <a id="aBtnCancelCreateGroup" runat="server" onserverclick="aBtnCancelCreateGroup_Click" class="btn btn-light-warning px-6 font-weight-bold">Cancel
                                            </a>
                                            <a id="aBtnDeleteGroup" runat="server" onserverclick="BtnDeleteGroup_Click" class="btn btn-danger px-6 font-weight-bold">Delete
                                            </a>
                                        </div>
                                    </div>
                                    <!--end::Navi-->
                                </div>
                                <!--end::Navigation-->
                            </div>
                            <!--end::Info-->
                        </div>
                        <!--end::Body-->
                    </div>
                    <!--end::Head-->
                    <div class="">
                        <controls:MessageControl ID="MessageControlUp" runat="server" />
                    </div>
                    <!--begin::Entry-->
                    <div class="d-flex flex-column-fluid">
                        <!--begin::Container-->
                        <div class="container">
                            <asp:Repeater ID="Rdg1" OnLoad="Rdg1_Load" OnItemDataBound="Rdg1_OnItemDataBound" runat="server">
                                <ItemTemplate>
                                    <!--begin::Row-->
                                    <div class="row">
                                        <!--begin::Column-->
                                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6">
                                            <!--begin::Card-->
                                            <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                <!--begin::Body-->
                                                <div class="card-body text-center pt-4">
                                                    <!--begin::Toolbar-->
                                                    <div id="divActions1" runat="server" visible="false" class="d-flex justify-content-end">
                                                    </div>
                                                    <!--end::Toolbar-->
                                                    <!--begin::User-->
                                                    <div class="mt-7" style="height: 154px;">
                                                        <div class="symbol symbol-circle symbol-lg-90">
                                                            <a id="aCompanyLogo1" runat="server" style="">
                                                                <asp:Image ID="ImgCompanyLogo1" Width="150" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "company_logo1") %>' runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!--end::User-->
                                                    <!--begin::Name-->
                                                    <div class="my-4">
                                                        <a id="aCompanyName1" runat="server" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                            <asp:Label ID="LblCompanyName1" Text='<%# DataBinder.Eval(Container.DataItem, "company_name1") %>' runat="server" />
                                                        </a>
                                                    </div>
                                                    <!--end::Name-->
                                                    <div class="my-4">
                                                        <asp:CheckBox ID="Cbx1" Visible="false" runat="server" />
                                                    </div>
                                                    <!--begin::Label-->
                                                    <span id="spanNotificationNewReceivedFiles1" runat="server" visible="true" style="" title="1 New received file" class="btn btn-text btn-light-warning btn-sm font-weight-bold">
                                                        <asp:Label ID="LblNotificationNewReceivedFiles1" runat="server" />
                                                    </span>

                                                    <!--end::Label-->
                                                    <!--begin::Buttons-->
                                                    <div class="mt-9">
                                                        <a id="aBtnChoose1" runat="server" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase">view partner's files</a>
                                                    </div>
                                                    <!--end::Buttons-->
                                                </div>
                                                <!--end::Body-->
                                                <asp:HiddenField ID="id1" Value='<%# DataBinder.Eval(Container.DataItem, "id1") %>' runat="server" />
                                                <asp:HiddenField ID="master_user_id1" runat="server" />
                                                <asp:HiddenField ID="invitation_status1" runat="server" />
                                                <asp:HiddenField ID="partner_user_id1" runat="server" />
                                                <asp:HiddenField ID="email1" runat="server" />
                                            </div>
                                            <!--end::Card-->
                                        </div>
                                        <!--end::Column-->
                                        <!--begin::Column-->
                                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6">
                                            <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                <!--begin::Body-->
                                                <div class="card-body text-center pt-4">
                                                    <!--begin::Toolbar-->
                                                    <div id="divActions2" runat="server" visible="false" class="d-flex justify-content-end">
                                                    </div>
                                                    <!--end::Toolbar-->
                                                    <!--begin::User-->
                                                    <div class="mt-7" style="height: 154px;">
                                                        <div class="symbol symbol-circle symbol-lg-90">
                                                            <a id="aCompanyLogo2" runat="server" style="">
                                                                <asp:Image ID="ImgCompanyLogo2" Width="150" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "company_logo2") %>' runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!--end::User-->
                                                    <!--begin::Name-->
                                                    <div class="my-4">
                                                        <a id="aCompanyName2" runat="server" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                            <asp:Label ID="LblCompanyName2" Text='<%# DataBinder.Eval(Container.DataItem, "company_name2") %>' runat="server" />
                                                        </a>
                                                    </div>
                                                    <!--end::Name-->
                                                    <div class="my-4">
                                                        <asp:CheckBox ID="Cbx2" Visible="false" runat="server" />
                                                    </div>
                                                    <!--begin::Label-->
                                                    <span id="spanNotificationNewReceivedFiles2" visible="true" runat="server" style="" title="1 New received file" class="btn btn-text btn-light-warning btn-sm font-weight-bold">
                                                        <asp:Label ID="LblNotificationNewReceivedFiles2" runat="server" />
                                                    </span>

                                                    <!--end::Label-->
                                                    <!--begin::Buttons-->
                                                    <div class="mt-9">
                                                        <a id="aBtnChoose2" runat="server" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase">view partner's files</a>
                                                    </div>
                                                    <!--end::Buttons-->
                                                </div>
                                                <!--end::Body-->
                                                <asp:HiddenField ID="id2" Value='<%# DataBinder.Eval(Container.DataItem, "id2") %>' runat="server" />
                                                <asp:HiddenField ID="master_user_id2" runat="server" />
                                                <asp:HiddenField ID="invitation_status2" runat="server" />
                                                <asp:HiddenField ID="partner_user_id2" runat="server" />
                                                <asp:HiddenField ID="email2" runat="server" />
                                            </div>
                                        </div>
                                        <!--end::Column-->
                                        <!--begin::Column-->
                                        <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6">
                                            <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                <!--begin::Body-->
                                                <div class="card-body text-center pt-4">
                                                    <!--begin::Toolbar-->
                                                    <div id="divActions3" runat="server" visible="false" class="d-flex justify-content-end">
                                                    </div>
                                                    <!--end::Toolbar-->
                                                    <!--begin::User-->
                                                    <div class="mt-7" style="height: 154px;">
                                                        <div class="symbol symbol-circle symbol-lg-90">
                                                            <a id="aCompanyLogo3" runat="server" style="">
                                                                <asp:Image ID="ImgCompanyLogo3" Width="150" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "company_logo3") %>' runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!--end::User-->
                                                    <!--begin::Name-->
                                                    <div class="my-4">
                                                        <a id="aCompanyName3" runat="server" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                            <asp:Label ID="LblCompanyName3" Text='<%# DataBinder.Eval(Container.DataItem, "company_name3") %>' runat="server" />
                                                        </a>
                                                    </div>
                                                    <!--end::Name-->
                                                    <div class="my-4">
                                                        <asp:CheckBox ID="Cbx3" Visible="false" runat="server" />
                                                    </div>
                                                    <!--begin::Label-->
                                                    <span id="spanNotificationNewReceivedFiles3" visible="true" runat="server" style="" title="1 New received file" class="btn btn-text btn-light-warning btn-sm font-weight-bold">
                                                        <asp:Label ID="LblNotificationNewReceivedFiles3" runat="server" />
                                                    </span>

                                                    <!--end::Label-->
                                                    <!--begin::Buttons-->
                                                    <div class="mt-9">
                                                        <a id="aBtnChoose3" runat="server" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase">view partner's files</a>
                                                    </div>
                                                    <!--end::Buttons-->
                                                </div>
                                                <!--end::Body-->
                                                <asp:HiddenField ID="id3" Value='<%# DataBinder.Eval(Container.DataItem, "id3") %>' runat="server" />
                                                <asp:HiddenField ID="master_user_id3" runat="server" />
                                                <asp:HiddenField ID="invitation_status3" runat="server" />
                                                <asp:HiddenField ID="partner_user_id3" runat="server" />
                                                <asp:HiddenField ID="email3" runat="server" />
                                            </div>
                                        </div>
                                        <!--end::Column-->
                                    </div>
                                    <!--end::Row-->
                                </ItemTemplate>
                            </asp:Repeater>
                            <controls:MessageControl ID="UcRgd1" runat="server" />

                            <!--begin::Pagination-->
                            <div id="divPagination" runat="server" visible="false" class="d-flex justify-content-between align-items-center flex-wrap">
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
                        <!--end::Container-->
                    </div>
                    <!--end::Entry-->                    
                    <!--end::Dashboard-->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <!-- Confirmation Delete Partner form (modal view) -->
    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
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
                                <controls:MessageControl ID="UcConfirmationMessageControl" runat="server" />
                            </div>
                            <div class="row" style="margin-top: 25px;">
                                <div id="divSuccess" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblSuccess" Text="Done!" runat="server" />
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
                                <div id="divFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-questions-circular-button"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblFailure" Text="Error!" runat="server" />
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
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-light-primary">Close</button>
                            <asp:Button ID="BtnDeleteConfirm" OnClick="BtnDeleteConfirm_OnClick" Text="Delete" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <!--begin::Page Scripts(used by this page)-->

        <script type="text/javascript">

            function OpenConfPopUp() {
                $('#divConfirm').modal('show');
            }

            function CloseConfPopUp() {
                $('#divConfirm').modal('hide');
            }

        </script>
        <!--end::Page Scripts-->

    </telerik:RadScriptBlock>

</asp:Content>
