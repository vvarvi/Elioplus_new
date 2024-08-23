<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardTrainingLibraryPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardTrainingLibraryPage" %>

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
                                    <div id="divVendorsList" visible="false" runat="server" class="row">
                                        <div class="col-lg-2" style="top: 15px; text-align: center;">
                                            <asp:Label ID="Label2" runat="server" Text="Selected Vendor:" />
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:DropDownList AutoPostBack="true" Width="300" ID="DrpVendors" OnSelectedIndexChanged="DrpVendors_SelectedIndexChanged" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-4"></div>
                                    </div>
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
                                                                <asp:Image ID="ImgCompanyLogo1" Width="150" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "overview_image_name1") %>' runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!--end::User-->
                                                    <!--begin::Name-->
                                                    <div class="my-4">
                                                        <a id="aCompanyName1" runat="server" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                            <asp:Label ID="LblCompanyName1" Text='<%# DataBinder.Eval(Container.DataItem, "course_description1") %>' runat="server" />
                                                        </a>
                                                    </div>
                                                    <!--end::Name-->
                                                    <div class="my-4">
                                                    </div>
                                                    <!--begin::Buttons-->
                                                    <div class="mt-9">
                                                        <a id="aBtnChoose1" runat="server" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase">view course</a>
                                                    </div>
                                                    <!--end::Buttons-->
                                                </div>
                                                <!--end::Body-->
                                                <asp:HiddenField ID="id1" Value='<%# DataBinder.Eval(Container.DataItem, "id1") %>' runat="server" />
                                                <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("overview_image_name1") %>' runat="server" />
                                                <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("overview_image_path1") %>' runat="server" />
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
                                                                <asp:Image ID="ImgCompanyLogo2" Width="150" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "overview_image_name2") %>' runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!--end::User-->
                                                    <!--begin::Name-->
                                                    <div class="my-4">
                                                        <a id="aCompanyName2" runat="server" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                            <asp:Label ID="LblCompanyName2" Text='<%# DataBinder.Eval(Container.DataItem, "course_description2") %>' runat="server" />
                                                        </a>
                                                    </div>
                                                    <!--end::Name-->
                                                    <div class="my-4">
                                                    </div>
                                                    <!--begin::Buttons-->
                                                    <div class="mt-9">
                                                        <a id="aBtnChoose2" runat="server" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase">view course</a>
                                                    </div>
                                                    <!--end::Buttons-->
                                                </div>
                                                <!--end::Body-->
                                                <asp:HiddenField ID="id2" Value='<%# DataBinder.Eval(Container.DataItem, "id2") %>' runat="server" />
                                                <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("overview_image_name2") %>' runat="server" />
                                                <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("overview_image_path2") %>' runat="server" />
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
                                                                <asp:Image ID="ImgCompanyLogo3" Width="150" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "overview_image_name3") %>' runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!--end::User-->
                                                    <!--begin::Name-->
                                                    <div class="my-4">
                                                        <a id="aCompanyName3" runat="server" class="text-dark font-weight-bold text-hover-primary font-size-h4">
                                                            <asp:Label ID="LblCompanyName3" Text='<%# DataBinder.Eval(Container.DataItem, "course_description3") %>' runat="server" />
                                                        </a>
                                                    </div>
                                                    <!--end::Name-->
                                                    <div class="my-4">
                                                    </div>
                                                    <!--begin::Buttons-->
                                                    <div class="mt-9">
                                                        <a id="aBtnChoose3" runat="server" class="btn btn-light-primary font-weight-bolder btn-sm py-3 px-6 text-uppercase">view course</a>
                                                    </div>
                                                    <!--end::Buttons-->
                                                </div>
                                                <!--end::Body-->
                                                <asp:HiddenField ID="id3" Value='<%# DataBinder.Eval(Container.DataItem, "id3") %>' runat="server" />
                                                <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("overview_image_name3") %>' runat="server" />
                                                <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("overview_image_path3") %>' runat="server" />
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

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <!--begin::Page Scripts(used by this page)-->

        <script type="text/javascript">

        </script>
        <!--end::Page Scripts-->

    </telerik:RadScriptBlock>

</asp:Content>
