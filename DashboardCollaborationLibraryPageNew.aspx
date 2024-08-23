<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardCollaborationLibraryPageNew.aspx.cs" Inherits="WdS.ElioPlus.DashboardCollaborationLibraryPageNew" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DANotificationControl.ascx" TagName="NotificationControl" TagPrefix="controls" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

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
                    <!--begin::Todo Files-->
                    <div class="d-flex flex-row">
                        <!--begin::Aside-->
                        <div class="flex-row-auto offcanvas-mobile w-200px w-xxl-275px" id="kt_todo_aside" style="min-height: 660px !important; display: inline-table;">
                            <!--begin::Card-->
                            <div class="card card-custom card-stretch" style="width: 290px;">
                                <!--begin::Body-->
                                <div class="card-body px-5">
                                    <!--begin:Nav-->
                                    <div class="navi navi-hover navi-active navi-link-rounded navi-bold navi-icon-center navi-light-icon">
                                        <!--begin:Item-->
                                        <div class="navi-item my-2 mb-6" style="text-align: center;">
                                            <a id="BtnSendFile" onserverclick="aSendFile_ServerClick" class="btn btn-primary px-6 font-weight-bold flaticon2-telegram-logo" runat="server">Send New Files
                                            </a>
                                        </div>
                                        <!--end:Item-->

                                        <!--begin:Item-->
                                        <div class="navi-item my-2 mb-6">
                                            <a href="#" class="navi-link active">
                                                <span class="navi-icon mr-4">
                                                    <span class="svg-icon svg-icon-lg">
                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Mail-heart.svg-->
                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                <rect x="0" y="0" width="24" height="24" />
                                                                <path d="M6,2 L18,2 C18.5522847,2 19,2.44771525 19,3 L19,13 C19,13.5522847 18.5522847,14 18,14 L6,14 C5.44771525,14 5,13.5522847 5,13 L5,3 C5,2.44771525 5.44771525,2 6,2 Z M13.8,4 C13.1562,4 12.4033,4.72985286 12,5.2 C11.5967,4.72985286 10.8438,4 10.2,4 C9.0604,4 8.4,4.88887193 8.4,6.02016349 C8.4,7.27338783 9.6,8.6 12,10 C14.4,8.6 15.6,7.3 15.6,6.1 C15.6,4.96870845 14.9396,4 13.8,4 Z" fill="#000000" opacity="0.3" />
                                                                <path d="M3.79274528,6.57253826 L12,12.5 L20.2072547,6.57253826 C20.4311176,6.4108595 20.7436609,6.46126971 20.9053396,6.68513259 C20.9668779,6.77033951 21,6.87277228 21,6.97787787 L21,17 C21,18.1045695 20.1045695,19 19,19 L5,19 C3.8954305,19 3,18.1045695 3,17 L3,6.97787787 C3,6.70173549 3.22385763,6.47787787 3.5,6.47787787 C3.60510559,6.47787787 3.70753836,6.51099993 3.79274528,6.57253826 Z" fill="#000000" />
                                                            </g>
                                                        </svg>
                                                        <!--end::Svg Icon-->
                                                    </span>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">Categories</span>
                                                <controls:NotificationControl ID="UcControl" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                        <asp:Repeater ID="RdgCategories" runat="server">
                                            <ItemTemplate>
                                                <!--begin:Item-->
                                                <div id="divNaviItem" runat="server" class="navi-item my-2">
                                                    <span class="navi-icon mr-4" style="float: left; margin-top: 10px;">
                                                        <!--begin::Dropdown Menu-->
                                                        <div id="divCategoryActionsArea" runat="server" class="dropdown dropdown-inline">
                                                            <button type="button" class="flaticon-more" style="background-color: #fff; border: 0px solid #fff;" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                <span class="svg-icon svg-icon-lg" style="display: none;">
                                                                    <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Add-user.svg-->
                                                                    <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                            <polygon points="0 0 24 0 24 24 0 24" />
                                                                            <path d="M18,8 L16,8 C15.4477153,8 15,7.55228475 15,7 C15,6.44771525 15.4477153,6 16,6 L18,6 L18,4 C18,3.44771525 18.4477153,3 19,3 C19.5522847,3 20,3.44771525 20,4 L20,6 L22,6 C22.5522847,6 23,6.44771525 23,7 C23,7.55228475 22.5522847,8 22,8 L20,8 L20,10 C20,10.5522847 19.5522847,11 19,11 C18.4477153,11 18,10.5522847 18,10 L18,8 Z M9,11 C6.790861,11 5,9.209139 5,7 C5,4.790861 6.790861,3 9,3 C11.209139,3 13,4.790861 13,7 C13,9.209139 11.209139,11 9,11 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                            <path d="M0.00065168429,20.1992055 C0.388258525,15.4265159 4.26191235,13 8.98334134,13 C13.7712164,13 17.7048837,15.2931929 17.9979143,20.2 C18.0095879,20.3954741 17.9979143,21 17.2466999,21 C13.541124,21 8.03472472,21 0.727502227,21 C0.476712155,21 -0.0204617505,20.45918 0.00065168429,20.1992055 Z" fill="#000000" fill-rule="nonzero" />
                                                                        </g>
                                                                    </svg>
                                                                    <!--end::Svg Icon-->
                                                                </span>
                                                            </button>
                                                            <div class="dropdown-menu p-0 m-0 dropdown-menu-left dropdown-menu-md">
                                                                <!--begin::Navigation-->
                                                                <ul class="navi navi-hover py-5">
                                                                    <li class="navi-item">
                                                                        <a id="aEdit" onserverclick="aEdit_ServerClick" role="button" runat="server" title="Edit category" class="navi-link">
                                                                            <span class="navi-icon">
                                                                                <i id="iEdit" runat="server" class="flaticon2-edit"></i>
                                                                            </span>
                                                                            <span class="">
                                                                                <asp:Label ID="LblEditSave" Text="Edit" runat="server" />
                                                                            </span>
                                                                        </a>
                                                                    </li>
                                                                    <li class="navi-item">
                                                                        <a id="aDelete" onserverclick="aDelete_ServerClick" runat="server" title="Delete category" class="navi-link">
                                                                            <span class="navi-icon">
                                                                                <i class="flaticon-delete"></i>
                                                                            </span>
                                                                            Delete
                                                                        </a>
                                                                    </li>
                                                                    <li class="navi-separator my-3"></li>
                                                                </ul>
                                                                <!--end::Navigation-->
                                                            </div>
                                                        </div>
                                                        <!--end::Dropdown Menu-->

                                                    </span>
                                                    <a id="aTabCategory" onserverclick="aTabCategory_ServerClick" role="button" runat="server" class="navi-link">
                                                        <span id="spanCategoryAreaView" runat="server" class="navi-text font-weight-bolder font-size-lg" style="width: 200px; display: inline-block;">
                                                            <asp:Label ID="LblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "category_description")%>' runat="server" />
                                                        </span>
                                                    </a>
                                                    <span id="spanCategoryAreaEdit" runat="server" visible="false" class="navi-text font-weight-bolder font-size-lg" style="width: 200px; display: inline-block;">
                                                        <asp:TextBox ID="TbxEditCategory" Text='<%# DataBinder.Eval(Container.DataItem, "category_description")%>' Width="180" CssClass="form-control" runat="server" />
                                                    </span>
                                                    <controls:NotificationControl ID="UcControlNewFilesCount" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                                    <asp:HiddenField ID="HdnId" Value='<%# Eval("id") %>' runat="server" />
                                                </div>
                                                <!--end:Item-->
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <controls:MessageControl ID="MessageControlCategories" Visible="false" runat="server" />

                                        <!--begin:Item-->
                                        <div id="divNaviItem" runat="server" class="navi-item my-2" style="display: inline-block; margin-bottom: 10px;">
                                            <a id="aTabCategory" runat="server" style="background-color: #fff;" class="navi-link">
                                                <span id="spanTabCategoryAreaView" runat="server" class="navi-text font-weight-bolder font-size-lg" style="text-align: center; display: inline-block; color: #3699FF; margin-top: -10px; padding-bottom: 5px;">
                                                    <asp:Label ID="LblTabCategoryDescriptionAdd" Text="Add new category" runat="server" />
                                                </span>
                                                <span class="navi-icon ml-4" style="margin-top: 10px;">
                                                    <a id="aAdd" onserverclick="aAdd_ServerClick" role="button" runat="server">
                                                        <i class="flaticon2-add" style="margin-top: 10px;"></i></a>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg" style="width: 240px; float: left; display: inline-block; margin-bottom: 10px; padding-right: 10px; margin-top: -10px;">
                                                    <asp:TextBox ID="TbxAddNewCategory" CssClass="form-control" runat="server" />
                                                </span>
                                                <controls:NotificationControl ID="UcControlNewFilesCount" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                            </a>
                                            <asp:HiddenField ID="HdnId" Value='<%# Eval("id") %>' runat="server" />
                                        </div>
                                        <!--end:Item-->
                                    </div>
                                    <!--end:Nav-->
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Card-->

                            <!--begin::Card-->
                            <div id="divVendorGroupsArea" runat="server" visible="false" class="card card-custom card-stretch" style="width: 290px; margin-top: 25px">
                                <!--begin::Body-->
                                <div class="card-body px-5">
                                    <!--begin:Nav-->
                                    <div class="navi navi-hover navi-active navi-link-rounded navi-bold navi-icon-center navi-light-icon">
                                        <!--begin:Item-->
                                        <div class="navi-item my-2 mb-6" style="text-align: center;">
                                            <asp:Panel ID="PnlCreateLibraryGroup" Style="padding: 5px; text-align: center;" runat="server">
                                                <a id="aBtnCreateGroup" runat="server" onserverclick="aBtnCreateGroup_ServerClick" class="btn btn-primary px-6 font-weight-bold flaticon-user-add">
                                                    <asp:Label ID="LblCreateGroup" Text="Create New Group" runat="server" />
                                                </a>
                                            </asp:Panel>
                                        </div>
                                        <!--end:Item-->

                                        <!--begin:Item-->
                                        <div class="navi-item my-2 mb-6">
                                            <a href="#" class="navi-link active">
                                                <span class="navi-icon mr-4">
                                                    <span class="flaticon2-group"></span>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">Groups</span>
                                                <controls:NotificationControl ID="UcGroupsControl" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                        <asp:Repeater ID="RptGroups" runat="server">
                                            <ItemTemplate>
                                                <!--begin:Item-->
                                                <div id="divNaviItem" runat="server" class="navi-item my-2">
                                                    <span class="navi-icon mr-4" style="float: left; margin-top: 10px;">
                                                        <!--begin::Dropdown Menu-->
                                                        <div class="dropdown dropdown-inline">
                                                            <button type="button" class="flaticon-more" style="background-color: #fff; border: 0px solid #fff;" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                <span class="svg-icon svg-icon-lg" style="display: none;">
                                                                    <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Add-user.svg-->
                                                                    <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                            <polygon points="0 0 24 0 24 24 0 24" />
                                                                            <path d="M18,8 L16,8 C15.4477153,8 15,7.55228475 15,7 C15,6.44771525 15.4477153,6 16,6 L18,6 L18,4 C18,3.44771525 18.4477153,3 19,3 C19.5522847,3 20,3.44771525 20,4 L20,6 L22,6 C22.5522847,6 23,6.44771525 23,7 C23,7.55228475 22.5522847,8 22,8 L20,8 L20,10 C20,10.5522847 19.5522847,11 19,11 C18.4477153,11 18,10.5522847 18,10 L18,8 Z M9,11 C6.790861,11 5,9.209139 5,7 C5,4.790861 6.790861,3 9,3 C11.209139,3 13,4.790861 13,7 C13,9.209139 11.209139,11 9,11 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                            <path d="M0.00065168429,20.1992055 C0.388258525,15.4265159 4.26191235,13 8.98334134,13 C13.7712164,13 17.7048837,15.2931929 17.9979143,20.2 C18.0095879,20.3954741 17.9979143,21 17.2466999,21 C13.541124,21 8.03472472,21 0.727502227,21 C0.476712155,21 -0.0204617505,20.45918 0.00065168429,20.1992055 Z" fill="#000000" fill-rule="nonzero" />
                                                                        </g>
                                                                    </svg>
                                                                    <!--end::Svg Icon-->
                                                                </span>
                                                            </button>
                                                            <div class="dropdown-menu p-0 m-0 dropdown-menu-left dropdown-menu-md">
                                                                <!--begin::Navigation-->
                                                                <ul class="navi navi-hover py-5">
                                                                    <li class="navi-item">
                                                                        <a id="aEditGroup" onserverclick="aEditGroup_ServerClick" role="button" runat="server" title="Edit category" class="navi-link">
                                                                            <span class="navi-icon">
                                                                                <i id="iEdit" runat="server" class="flaticon2-edit"></i>
                                                                            </span>
                                                                            Edit
                                                                        </a>
                                                                    </li>
                                                                    <li class="navi-item">
                                                                        <a id="aDeleteGroup" onserverclick="aDeleteGroup_ServerClick" runat="server" title="Delete category" class="navi-link">
                                                                            <span class="navi-icon">
                                                                                <i class="flaticon-delete"></i>
                                                                            </span>
                                                                            Delete
                                                                        </a>
                                                                    </li>
                                                                    <li class="navi-separator my-3"></li>
                                                                </ul>
                                                                <!--end::Navigation-->
                                                            </div>
                                                        </div>
                                                        <!--end::Dropdown Menu-->

                                                    </span>
                                                    <a id="aGroupDescription" role="button" runat="server" class="navi-link">
                                                        <span id="spanGroupDescriptionView" runat="server" class="navi-text font-weight-bolder font-size-lg" style="width: 200px; display: inline-block;">
                                                            <asp:Label ID="LblGroupDescription" Text='<%# DataBinder.Eval(Container.DataItem, "group_description")%>' runat="server" />
                                                        </span>
                                                    </a>
                                                    <controls:NotificationControl ID="UcGroupMembersCount" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                                    <asp:HiddenField ID="HdnGroupId" Value='<%# Eval("id") %>' runat="server" />
                                                </div>
                                                <!--end:Item-->
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <controls:MessageControl ID="MessageControlGroupMembers" Visible="false" runat="server" />

                                    </div>
                                    <!--end:Nav-->
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Card-->
                        </div>
                        <!--end::Aside-->
                        <!--begin::List-->
                        <div class="flex-row-fluid d-flex flex-column ml-lg-8">

                            <div id="divVendorsList" visible="false" runat="server" class="card card-custom card-stretch" style="margin-bottom: 10px; height: 55px;">
                                <div class="d-flex align-items-center mr-2 py-2">
                                    <div class="col-lg-12">
                                        <div class="row">
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
                                </div>
                            </div>
                            <div class="card card-custom card-stretch" style="margin-bottom: 25px; height: 130px;">
                                <!--begin::Info-->
                                <div class="d-flex align-items-center mr-2 py-2">
                                    <!--begin::Navigation-->
                                    <div class="col-lg-12">
                                        <div class="col-lg-6">
                                            <!--begin::Navi-->
                                            <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                                <li class="nav-item">
                                                    <a id="aSendFiles" onserverclick="aSendFiles_OnClick" runat="server" class="nav-link active">
                                                        <span class="nav-icon"><i class="flaticon-upload-1"></i></span>
                                                        <span class="nav-text">
                                                            <asp:Label ID="LblSendFiles" Text="Files Sent" runat="server" />
                                                        </span>
                                                    </a>
                                                </li>
                                                <li class="nav-item">
                                                    <a id="aReceivedFiles" onserverclick="aReceivedFiles_OnClick" runat="server" class="nav-link">
                                                        <span class="nav-icon"><i class="flaticon-download-1"></i></span>
                                                        <span class="nav-text">
                                                            <asp:Label ID="LblReceivedFiles" Text="Files Received" runat="server" />
                                                        </span>
                                                    </a>
                                                </li>
                                            </ul>
                                            <!--end::Navi-->
                                        </div>
                                        <div class="col-lg-6"></div>
                                    </div>
                                    <!--end::Navigation-->
                                </div>
                                <!--end::Info-->
                                <!--begin::Info-->
                                <div class="d-flex align-items-center mr-2 py-2" style="margin-top: 15px;">
                                    <!--begin::Navigation-->
                                    <div class="col-lg-12">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <asp:Label ID="LblFromSearch" runat="server" Text="From:" />
                                                <telerik:RadDatePicker ID="RdtDateFromSearch" Width="180" DateInput-DateFormat="yyyy/MM/dd" runat="server" />
                                            </div>
                                            <div class="col-lg-3">
                                                <asp:Label ID="LblToSearch" runat="server" Text="To:" />
                                                <telerik:RadDatePicker ID="RdtDateToSearch" Width="180" DateInput-DateFormat="yyyy/MM/dd" runat="server" />
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="DdlPartnersListSearch" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-lg-2">
                                                <a id="aSearch" runat="server" onserverclick="aSearch_ServerClick" class="btn btn-light-primary">Search</a>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end::Navigation-->
                                </div>
                                <!--end::Info-->
                            </div>

                            <controls:MessageAlertControl ID="UcMessageAlert" runat="server" />
                            <div class="tab-content">                               
                                <div class="tab-pane fade show active" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <asp:Repeater ID="Rdg1" OnLoad="Rdg1_Load" OnItemDataBound="Rdg1_ItemDataBound" runat="server">
                                        <ItemTemplate>
                                            <!--begin::Row-->
                                            <div class="row">
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div class="dropdown dropdown-inline">
                                                                    <button type="button" class="flaticon-more" style="background-color: #fff; border: 0px solid #fff;" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                        <span class="svg-icon svg-icon-md svg-icon-primary" style="display: none;">
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
                                                                    </button>
                                                                    <div class="dropdown-menu p-0 m-0 dropdown-menu-left dropdown-menu-md" style="width: 220px;">
                                                                        <!--begin::Navigation-->
                                                                        <ul class="navi navi-hover py-5">
                                                                            <li class="navi-item">
                                                                                <a id="aDelete1" onserverclick="aDelete1_OnClick" runat="server" title="Delete file" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete"></i>
                                                                                    </span>
                                                                                    Delete
                                                                                </a>
                                                                            </li>
                                                                            <li id="liUploadPreviewImg1" runat="server" visible="false" class="navi-item">
                                                                                <a id="aUploadPreviewImg1" onserverclick="aUploadPreviewImg1_ServerClick" runat="server" title="Upload preview icon" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-upload"></i>
                                                                                    </span>
                                                                                    Upload preview icon
                                                                                </a>
                                                                            </li>
                                                                            <li id="liDeletePreviewImg1" runat="server" visible="false" class="navi-item">
                                                                                <a id="aDeletePreviewImg1" onserverclick="aDeletePreviewImg1_ServerClick" runat="server" title="Delete preview icon" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete-1"></i>
                                                                                    </span>
                                                                                    Delete preview icon
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-separator my-3"></li>
                                                                        </ul>
                                                                        <!--end::Navigation-->
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                    <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a1" onserverclick="HpLnkFile1_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' runat="server" />
                                                                </a>

                                                                <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnPreviewFilePath1" Value='<%# Eval("preview_file_path1") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div class="dropdown dropdown-inline">
                                                                    <button type="button" class="flaticon-more" style="background-color: #fff; border: 0px solid #fff;" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                        <span class="svg-icon svg-icon-md svg-icon-primary" style="display: none;">
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
                                                                    </button>
                                                                    <div class="dropdown-menu p-0 m-0 dropdown-menu-left dropdown-menu-md" style="width: 220px;">
                                                                        <!--begin::Navigation-->
                                                                        <ul class="navi navi-hover py-5">
                                                                            <li class="navi-item">
                                                                                <a id="aDelete2" onserverclick="aDelete2_OnClick" runat="server" title="Delete file" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete"></i>
                                                                                    </span>
                                                                                    Delete
                                                                                </a>
                                                                            </li>
                                                                            <li id="liUploadPreviewImg2" runat="server" visible="false" class="navi-item">
                                                                                <a id="aUploadPreviewImg2" onserverclick="aUploadPreviewImg2_ServerClick" runat="server" title="Upload preview icon" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-upload"></i>
                                                                                    </span>
                                                                                    Upload preview icon
                                                                                </a>
                                                                            </li>
                                                                            <li id="liDeletePreviewImg2" runat="server" visible="false" class="navi-item">
                                                                                <a id="aDeletePreviewImg2" onserverclick="aDeletePreviewImg2_ServerClick" runat="server" title="Delete preview icon" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete-1"></i>
                                                                                    </span>
                                                                                    Delete preview icon
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-separator my-3"></li>
                                                                        </ul>
                                                                        <!--end::Navigation-->
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                    <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' runat="server" />
                                                                </a>
                                                                <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnPreviewFilePath2" Value='<%# Eval("preview_file_path2") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div class="dropdown dropdown-inline">
                                                                    <button type="button" class="flaticon-more" style="background-color: #fff; border: 0px solid #fff;" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                        <span class="svg-icon svg-icon-md svg-icon-primary" style="display: none;">
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
                                                                    </button>
                                                                    <div class="dropdown-menu p-0 m-0 dropdown-menu-left dropdown-menu-md" style="width: 220px;">
                                                                        <!--begin::Navigation-->
                                                                        <ul class="navi navi-hover py-5">
                                                                            <li class="navi-item">
                                                                                <a id="aDelete3" onserverclick="aDelete3_OnClick" runat="server" title="Delete file" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete"></i>
                                                                                    </span>
                                                                                    Delete
                                                                                </a>
                                                                            </li>
                                                                            <li id="liUploadPreviewImg3" runat="server" visible="false" class="navi-item">
                                                                                <a id="aUploadPreviewImg3" onserverclick="aUploadPreviewImg3_ServerClick" runat="server" title="Upload preview icon" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-upload"></i>
                                                                                    </span>
                                                                                    Upload preview icon
                                                                                </a>
                                                                            </li>
                                                                            <li id="liDeletePreviewImg3" runat="server" visible="false" class="navi-item">
                                                                                <a id="aDeletePreviewImg3" onserverclick="aDeletePreviewImg3_ServerClick" runat="server" title="Delete preview icon" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete-1"></i>
                                                                                    </span>
                                                                                    Delete preview icon
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-separator my-3"></li>
                                                                        </ul>
                                                                        <!--end::Navigation-->
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                    <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' runat="server" />
                                                                </a>
                                                                <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnPreviewFilePath3" Value='<%# Eval("preview_file_path3") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div class="dropdown dropdown-inline">
                                                                    <button type="button" class="flaticon-more" style="background-color: #fff; border: 0px solid #fff;" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                        <span class="svg-icon svg-icon-md svg-icon-primary" style="display: none;">
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
                                                                    </button>
                                                                    <div class="dropdown-menu p-0 m-0 dropdown-menu-left dropdown-menu-md" style="width: 220px;">
                                                                        <!--begin::Navigation-->
                                                                        <ul class="navi navi-hover py-5">
                                                                            <li class="navi-item">
                                                                                <a id="aDelete4" onserverclick="aDelete4_OnClick" runat="server" title="Delete file" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete"></i>
                                                                                    </span>
                                                                                    Delete
                                                                                </a>
                                                                            </li>
                                                                            <li id="liUploadPreviewImg4" runat="server" visible="false" class="navi-item">
                                                                                <a id="aUploadPreviewImg4" onserverclick="aUploadPreviewImg4_ServerClick" runat="server" title="Upload preview icon" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-upload"></i>
                                                                                    </span>
                                                                                    Upload preview icon
                                                                                </a>
                                                                            </li>
                                                                            <li id="liDeletePreviewImg4" runat="server" visible="false" class="navi-item">
                                                                                <a id="aDeletePreviewImg4" onserverclick="aDeletePreviewImg4_ServerClick" runat="server" title="Delete preview icon" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete-1"></i>
                                                                                    </span>
                                                                                    Delete preview icon
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-separator my-3"></li>
                                                                        </ul>
                                                                        <!--end::Navigation-->
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                    <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' runat="server" />
                                                                </a>
                                                                <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnPreviewFilePath4" Value='<%# Eval("preview_file_path4") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                            </div>
                                            <!--end::Row-->
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <controls:MessageControl ID="UcRgd1" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end::List-->
                    </div>
                    <!--end::Todo Files-->
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <!-- Pop Up Invitation form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblFileUploadTitle" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <controls:MessageControl ID="UploadMessageAlert" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-light-primary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Confirmation Delete Partner form (modal view) -->
    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
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
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnDeleteFile" OnClick="BtnDeleteFile_OnClick" Visible="false" Text="Delete" CssClass="btn btn-primary" runat="server" />
                            <asp:Button ID="BtnDeletePreviewFile" OnClick="BtnDeletePreviewFile_OnClick" Visible="false" Text="Delete" CssClass="btn btn-primary" runat="server" />
                            <asp:Button ID="BtnDeleteCategory" OnClick="BtnDeleteCategory_Click" Visible="false" Text="Delete" CssClass="btn btn-primary" runat="server" />
                            <asp:Button ID="BtnDeleteGroup" OnClick="BtnDeleteGroup_Click" Visible="false" Text="Delete" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up Create Library Group form (modal view) -->
    <div id="divCreateLibraryGroup" class="modal fade" tabindex="-1">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content" style="width: 600px;">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblLibraryGroupTitle" Text="Create New Group" CssClass="control-label" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="TbxLibraryGroupName" placeholder="Group Name" CssClass="form-control" runat="server" />
                                    </div>
                                    <div id="divSearchLibraryGroups" runat="server" visible="false" class="form-group">
                                        <div class="pl-20 pr-0">
                                            <div class="form-group has-feedback">
                                                <asp:TextBox ID="TbxRetailorSearch" AutoPostBack="true" OnTextChanged="TbxRetailorSearch_OnTextChanged" CssClass="form-control rounded" Width="250" placeholder="Search by Name" runat="server" />
                                                <asp:TextBox ID="TbxRetailorAdvancedSearch" Visible="false" AutoPostBack="true" OnTextChanged="TbxRetailorSearch_OnTextChanged" CssClass="form-control rounded" Width="250" placeholder="Search by Name" runat="server" />
                                                <div style="float: right; margin-top: -30px; margin-right: 25px;">
                                                    <asp:ImageButton ID="ImgBtnRetailorSearch" Visible="false" runat="server" OnClick="ImgBtnRetailorSearch_OnClick" ImageUrl="images/preview.png" />
                                                </div>
                                            </div>
                                            <div style="padding-left: 10px; display: inline-block; width: 100%; font-size: 12px; padding: 5px; color: #2b3643 !important;">
                                                <asp:LinkButton ID="LnkBtnRetailorAdvancedSearch" OnClick="LnkBtnRetailorAdvancedSearch_OnClck" Style="color: #2b3643 !important; text-decoration: underline;" Text="Advanced search" runat="server" />
                                            </div>
                                            <div id="divRetailorAdvancedSearch" visible="false" style="height: 135px; font-size: 12px;" runat="server">
                                                <table>
                                                    <tr>
                                                        <td style="padding: 5px;">
                                                            <asp:DropDownList AutoPostBack="true" Width="200" OnSelectedIndexChanged="DdlRetailorCountries_OnSelectedIndexChanged" ID="DdlRetailorCountries" runat="server" />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 5px;">
                                                            <asp:DropDownList AutoPostBack="true" Width="120" OnSelectedIndexChanged="DdlRetailorRegions_OnSelectedIndexChanged" ID="DdlRetailorRegions" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:CheckBoxList Visible="false" ID="CbxRetailorSearchList" runat="server">
                                                    <asp:ListItem Text="By country" Value="Country" />
                                                    <asp:ListItem Text="By region" Value="Region" />
                                                    <asp:ListItem Text="By name & partner program" Value="Type" />
                                                </asp:CheckBoxList>
                                                <div style="height: 70px; font-size: 10px; margin-left: 10px; line-height: 1px; color: #2b3643;">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 90px;">
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx1" runat="server" />
                                                            </td>
                                                            <td style="width: 80px;">
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx2" runat="server" />
                                                            </td>
                                                            <td style="width: 110px;">
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx3" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx4" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx5" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx6" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx7" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div style="overflow-y: scroll; min-height: 250px;">
                                        <div class="portlet-body todo-project-list-content">
                                            <div class="todo-project-list">
                                                <asp:Repeater ID="RptRetailorsList" OnItemDataBound="RptRetailorsList_OnItemDataBound" runat="server">
                                                    <HeaderTemplate>
                                                        <ul class="chat-list m-0 media-list">
                                                            <li class="media">
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 90px; height: 50px;">
                                                                            <asp:CheckBox AutoPostBack="true" ID="CbxSelectAll" OnCheckedChanged="CbxSelectAll_OnCheckedChanged" runat="server" />
                                                                            <asp:Label ID="LblSelect" Text="All" runat="server" />
                                                                        </td>
                                                                        <td style="width: 105px; height: 50px;">
                                                                            <asp:Label ID="LblCompanyLogoHeader" Text="Logo" runat="server" />
                                                                        </td>
                                                                        <td style="min-width: 250px;">
                                                                            <asp:Label ID="LblCompanyNameHeader" Text="Name" runat="server" />
                                                                        </td>
                                                                        <td style="width: 100px;">
                                                                            <asp:Label ID="LblCompanyCountryHeader" Text="Country" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </li>
                                                        </ul>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <ul class="chat-list m-0 media-list">
                                                            <li class="media">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:HiddenField ID="id" runat="server" />
                                                                            <asp:HiddenField ID="master_user_id" runat="server" />
                                                                            <asp:HiddenField ID="invitation_status" runat="server" />
                                                                            <asp:HiddenField ID="partner_user_id" runat="server" />
                                                                            <asp:HiddenField ID="email" runat="server" />
                                                                        </td>
                                                                        <td style="width: 90px;">
                                                                            <asp:CheckBox AutoPostBack="false" ID="CbxSelectUser" runat="server" />
                                                                        </td>
                                                                        <td style="width: 90px;">
                                                                            <a id="aCompanyLogo" href="javascript:;" runat="server">
                                                                                <div class="media-left avatar">
                                                                                    <asp:Image ID="ImgCompanyLogo" Width="34" Height="34" class="media-object img-circle" runat="server" />
                                                                                </div>
                                                                            </a>
                                                                        </td>
                                                                        <td style="width: 300px;">
                                                                            <a id="aCompanyName" runat="server">
                                                                                <asp:Label ID="LblCompanyName" runat="server" />
                                                                            </a>
                                                                        </td>
                                                                        <td style="width: 100px;">
                                                                            <asp:Label ID="LblCountry" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </li>
                                                        </ul>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <controls:MessageControl ID="MessageControlCreateLibraryGroup" Visible="false" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="MessageControlUp" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnCreate" OnClick="BtnCreate_OnClick" CssClass="btn btn-primary" runat="server" Text="Create" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up Edit Library Group form (modal view) -->
    <div id="EditRetailor" class="modal fade" tabindex="-1" data-width="500">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblEditRetailorTitle" Text="Add/Remove Retailors to Group" CssClass="control-label" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblEditRetailorName" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxEditRetailorName" MaxLength="145" EnableViewState="true" ViewStateMode="Enabled" CssClass="form-control" BorderStyle="None" placeholder="Enter group name here" data-placement="top" runat="server" />
                                        <asp:HiddenField ID="HdnGroupId" Value="-1" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <div class="pl-20 pr-0">
                                            <div class="form-group has-feedback">
                                                <asp:TextBox ID="TbxEditRetailorsSearch" AutoPostBack="true" OnTextChanged="TbxEditRetailorsSearch_OnTextChanged" CssClass="form-control rounded" Width="250" placeholder="Search by Name" runat="server" />
                                                <asp:TextBox ID="TbxEditRetailorsAdvancedSearch" Visible="false" AutoPostBack="true" OnTextChanged="TbxEditRetailorsSearch_OnTextChanged" CssClass="form-control rounded" Width="250" placeholder="Search by Country/Region/Type" runat="server" />
                                                <div style="float: right; margin-top: -30px; margin-right: 25px;">
                                                    <asp:ImageButton ID="ImgBtnEditRetailorSearch" Visible="false" runat="server" OnClick="ImgBtnEditRetailorSearch_OnClick" ImageUrl="images/preview.png" />
                                                </div>
                                            </div>
                                            <div style="padding-left: 10px; display: inline-block; width: 100%; font-size: 12px; padding: 5px; color: #2b3643;">
                                                <asp:LinkButton ID="LnkBtnEditRetailorAdvancedSearch" Style="color: #2b3643 !important; text-decoration: underline;" Text="Advanced search" runat="server" />
                                            </div>
                                            <div id="divEditRetailorAdvancedSearch" visible="false" style="height: 135px; font-size: 12px;" runat="server">
                                                <table>
                                                    <tr>
                                                        <td style="padding: 5px;">
                                                            <asp:DropDownList AutoPostBack="true" Width="200" OnSelectedIndexChanged="DdlEditRetailorCountries_OnSelectedIndexChanged" ID="DdlEditRetailorCountries" runat="server" />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 5px;">
                                                            <asp:DropDownList AutoPostBack="true" Width="120" OnSelectedIndexChanged="DdlEditRetailorRegions_OnSelectedIndexChanged" ID="DdlEditRetailorRegions" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:CheckBoxList Visible="false" ID="CbxEditRetailorAdvancedSearchList" runat="server">
                                                    <asp:ListItem Text="By country" Value="Country" />
                                                    <asp:ListItem Text="By region" Value="Region" />
                                                    <asp:ListItem Text="By name & partner program" Value="Type" />
                                                </asp:CheckBoxList>
                                                <div style="height: 70px; font-size: 10px; margin-left: 10px; line-height: 1px; color: #2b3643;">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 90px;">
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx1" runat="server" />
                                                            </td>
                                                            <td style="width: 80px;">
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx2" runat="server" />
                                                            </td>
                                                            <td style="width: 110px;">
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx3" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx4" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx5" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx6" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx7" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Label ID="LblEditRetailorList" CssClass="control-label" runat="server" />
                                        <div style="overflow-y: scroll; min-height: 250px;">
                                            <div class="portlet-body todo-project-list-content">
                                                <div class="todo-project-list">
                                                    <asp:Repeater ID="RptEditRetailorsList" runat="server">
                                                        <HeaderTemplate>
                                                            <ul class="chat-list m-0 media-list">
                                                                <li class="media">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 90px; height: 50px;">
                                                                                <asp:CheckBox AutoPostBack="true" ID="CbxSelectAll" runat="server" />
                                                                                <asp:Label ID="LblSelect" Text="Select" runat="server" />
                                                                            </td>
                                                                            <td style="width: 60px; height: 50px;">
                                                                                <asp:Label ID="LblCompanyLogoHeader" Text="Logo" runat="server" />
                                                                            </td>
                                                                            <td style="min-width: 220px;">
                                                                                <asp:Label ID="LblCompanyNameHeader" Text="Name" runat="server" />
                                                                            </td>
                                                                            <td style="width: 100px;">
                                                                                <asp:Label ID="LblCompanyCountryHeader" Text="Country" runat="server" />
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                <asp:Label ID="LblCompanyNewMessagesHeader" Visible="false" Text="New" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </li>
                                                            </ul>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <ul class="chat-list m-0 media-list">
                                                                <li class="media">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:HiddenField ID="id" runat="server" />
                                                                                <%--<asp:HiddenField ID="collaboration_group_id" runat="server" />--%>
                                                                                <asp:HiddenField ID="master_user_id" runat="server" />
                                                                                <asp:HiddenField ID="invitation_status" runat="server" />
                                                                                <asp:HiddenField ID="partner_user_id" runat="server" />
                                                                                <asp:HiddenField ID="email" runat="server" />
                                                                            </td>
                                                                            <td style="width: 90px;">
                                                                                <asp:CheckBox AutoPostBack="false" ID="CbxSelectUser" runat="server" />
                                                                            </td>
                                                                            <td style="width: 70px;">
                                                                                <a id="aCompanyLogo" href="javascript:;" runat="server">
                                                                                    <div class="media-left avatar">
                                                                                        <asp:ImageButton ID="ImgBtnCompanyLogo" CssClass="media-object img-circle" runat="server" />
                                                                                    </div>
                                                                                </a>
                                                                            </td>
                                                                            <td style="width: 240px;">
                                                                                <a id="aCompanyName" runat="server">
                                                                                    <asp:Label ID="LblCompanyName" runat="server" />
                                                                                </a>
                                                                            </td>
                                                                            <td style="width: 100px;">
                                                                                <asp:Label ID="LblCountry" runat="server" />
                                                                                <asp:Label ID="LblRegion" runat="server" />
                                                                            </td>
                                                                            <td>
                                                                                <span id="spanMessagesCountNotification" visible="false" class="badge bg-danger" title="New unread message" runat="server">
                                                                                    <asp:Label ID="LblMessagesCount" runat="server" />
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </li>
                                                            </ul>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <controls:MessageControl ID="MessageControlEditRetailors" Visible="false" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="divEditRetailorFailure" runat="server" visible="false" class="alert alert-custom alert-light-danger fade show" style="text-align: justify; width: 100%;">
                                    <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblEditRetailorFailure" Text="Error! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblEditRetailorFailureMsg" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                        </button>
                                    </div>
                                </div>
                                <div id="divEditRetailorSuccess" runat="server" visible="false" class="alert alert-custom alert-light-success fade show" style="text-align: justify; width: 100%;">
                                    <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblEditRetailorSuccess" Text="Done! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblEditRetailorSuccessMsg" runat="server" />
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
                            <asp:Button ID="BtnEditRetailor" OnClick="BtnEditRetailor_OnClick" CssClass="btn btn-primary" runat="server" Text="Add/Remove" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Upload/Send file form (modal view) -->
    <div id="divUploadSendFile" class="modal fade" tabindex="-1">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content" style="display: inline-table; width: 650px;">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblSelectReceiversAndUploadFiles" Text="Send files to your partners" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="divVendorsUploadFilesAreaStepOne" runat="server" class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-body" style="padding: 2px 2px 2px 2px;">
                                                    <div class="col-12" style="margin-bottom: 30px;">
                                                        <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                                            <li class="nav-item">
                                                                <a id="aIndividualPartners" onserverclick="aIndividualPartners_OnClick" runat="server" class="nav-link active">
                                                                    <span class="nav-icon"><i class="flaticon-avatar"></i></span>
                                                                    <span class="nav-text">
                                                                        <asp:Label ID="LblIndividualPartners" Text="Individual Partners" runat="server" />
                                                                    </span>
                                                                </a>
                                                            </li>
                                                            <li id="liGroupPartners" runat="server" class="nav-item">
                                                                <a id="aGroupPartners" onserverclick="aGroupPartners_OnClick" runat="server" class="nav-link">
                                                                    <span class="nav-icon"><i class="flaticon2-group"></i></span>
                                                                    <span class="nav-text">
                                                                        <asp:Label ID="LblGroupPartners" Text="Group Partners" runat="server" />
                                                                    </span>
                                                                </a>
                                                            </li>
                                                            <li id="liTiersPartners" runat="server" class="nav-item">
                                                                <a id="aTiersPartners" onserverclick="aTiersPartners_OnClick" runat="server" class="nav-link">
                                                                    <span class="nav-icon"><i class="flaticon-medal"></i></span>
                                                                    <span class="nav-text">
                                                                        <asp:Label ID="LblTiersPartners" Text="Tiers" runat="server" />
                                                                    </span>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="tab-content">
                                                        <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                                            <div class="row">
                                                                <div class="col-md-10">
                                                                    <asp:DropDownList ID="DdlPartnersList" AutoPostBack="true" OnSelectedIndexChanged="DdlPartnersList_SelectedIndexChanged" CssClass="form-control" runat="server" />
                                                                    <telerik:RadAutoCompleteBox Visible="false" RenderMode="Auto" Style="border-width: 0px" CssClass="form-control" runat="server" ID="RcbxCollaborationPartners"
                                                                        Delimiter=" " Width="300" DataSourceID="AllCollaborationPartners" AllowCustomEntry="true" DataTextField="company_name" DataValueField="id"
                                                                        Filter="StartsWith" TextSettings-SelectionMode="Single" InputType="Text" DropDownHeight="400"
                                                                        DropDownWidth="400" EmptyMessage="Start typing the name of your partner">
                                                                        <DropDownItemTemplate>
                                                                            <table cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td align="left" style="padding-left: 0px;">
                                                                                        <%# DataBinder.Eval(Container.DataItem, "company_name")%>                                                               
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </DropDownItemTemplate>
                                                                    </telerik:RadAutoCompleteBox>
                                                                    <asp:SqlDataSource ID="AllCollaborationPartners" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                                        ProviderName="System.Data.SqlClient" SelectCommand="SELECT u.id,u.company_name,u.country,u.email
                                                                                                                ,cvr.id as collaboration_vend_res_id,cvr.tier_status
                                                                                                                FROM Elio_collaboration_vendors_resellers cvr
                                                                                                                inner join elio_users u
	                                                                                                                on u.id = cvr.partner_user_id
                                                                                                                where cvr.master_user_id = 1
                                                                                                                and cvr.invitation_status = 'Confirmed'
                                                                                                                and cvr.is_active = 1
                                                                                                                and u.company_type = 'Channel Partners'
                                                                                                                order by u.company_name"></asp:SqlDataSource>

                                                                </div>
                                                                <div class="col-md-2">
                                                                    <a id="aBtnAdd" onserverclick="aBtnAdd_ServerClick" class="btn btn-primary" runat="server">Add
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="tab-pane fade" id="tab_1_2" runat="server" visible="false" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                                            <div id="divLibraryGroup" runat="server">
                                                                <div class="row">
                                                                    <div class="col-md-5">
                                                                        <asp:DropDownList ID="DdlLibraryGroups" AutoPostBack="true" OnSelectedIndexChanged="DdlLibraryGroups_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:TextBox ID="TbxLibraryGroupsName" Enabled="false" placeholder="Group Members" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <a id="aBtnAddGroupMembers" onserverclick="aBtnAddGroupMembers_ServerClick" class="btn btn-primary" runat="server">Add
                                                                        </a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="divNoLibraryGroupExists" runat="server" visible="false">
                                                                <div class="row">
                                                                    <controls:MessageAlertControl ID="UcNoLibraryGroupExistsAlert" Message="You have no Library Groups. To create click the button below!" MessageType="Info" IsLightColor="true" IsLong="true" HasClose="false" ShowImg="true" ShowLnkBtnRgstr="false" Visible="true" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="tab-pane fade" id="tab_1_3" runat="server" visible="false" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                                            <div id="divTiers" runat="server">
                                                                <div class="row">
                                                                    <div class="col-md-5">
                                                                        <asp:DropDownList ID="DdlTiers" AutoPostBack="true" OnSelectedIndexChanged="DdlTiers_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:TextBox ID="TbxTiersPartners" Enabled="false" placeholder="Tier Members" CssClass="form-control border-danger" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <a id="aBtnAddTiersMembers" onserverclick="aBtnAddTiersMembers_ServerClick" class="btn btn-primary" runat="server">Add
                                                                        </a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="divNoTiersExists" visible="false" runat="server">
                                                                <div class="row">
                                                                    <controls:MessageAlertControl ID="UcNoTiersExistsAlert" Message="You have no Tiers yet." MessageType="Info" IsLightColor="true" IsLong="true" HasClose="false" ShowImg="true" ShowLnkBtnRgstr="false" Visible="true" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 25px;">
                                                        <div class="col-md-10">
                                                            <asp:TextBox ID="TbxReceiversPartners" Enabled="false" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-md-2" style="padding-top: 10px;">
                                                            <a id="aRemoveReceivers" visible="false" onserverclick="aRemoveReceivers_ServerClick" runat="server">
                                                                <span class="nav-icon"><i class="flaticon-delete"></i></span>
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divVendorsUploadFilesAreaStepTwo" visible="false" runat="server" class="col-md-12">
                                        <div style="margin-top: 10px;">
                                            <table>
                                                <tr id="tr1" runat="server">
                                                    <td style="width: 220px;">
                                                        <asp:DropDownList ID="Ddlcategory" Width="200" AutoPostBack="true" OnSelectedIndexChanged="Ddlcategory_SelectedIndexChanged" CssClass="form-control" runat="server" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnAddLibraryFile" class="icon text-dark-50 flaticon-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                    </td>
                                                    <td style="width: 20px;"></td>
                                                    <td>
                                                        <div id="divFileUpload" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                            <div class="">
                                                                <span>
                                                                    <input type="file" name="uploadFile" visible="false" id="inputFile" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                        data-iconname="ion-image mr-5"
                                                                        class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                        runat="server" />
                                                                </span>
                                                            </div>

                                                            <telerik:RadAsyncUpload RenderMode="Lightweight" CssClass="" runat="server" ID="RadAsyncUpload1" OnClientFileUploaded="onClientFileUploaded"
                                                                MaxFileInputsCount="1" />
                                                        </div>

                                                    </td>
                                                </tr>
                                                <tr id="tr2" runat="server" visible="false">
                                                    <td style="width: 220px;">
                                                        <asp:DropDownList ID="Ddlcategory2" Width="200" CssClass="form-control" runat="server" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnAddLibraryFile2" class="icon text-dark-50 flaticon-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnDeleteLibraryFile2" class="icon text-dark-50 flaticon-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                    </td>
                                                    <td>
                                                        <div id="divFileUpload2" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                            <div class="">
                                                                <span>
                                                                    <input type="file" name="uploadFile" id="inputFile2" visible="false" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                        data-iconname="ion-image mr-5"
                                                                        class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                        runat="server" />
                                                                </span>
                                                            </div>

                                                            <telerik:RadAsyncUpload RenderMode="Lightweight" CssClass="" runat="server" ID="RadAsyncUpload2" OnClientFileUploaded="onClientFileUploaded"
                                                                MaxFileInputsCount="1" />

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="tr3" runat="server" visible="false">
                                                    <td style="width: 220px;">
                                                        <asp:DropDownList ID="Ddlcategory3" Width="200" CssClass="form-control" runat="server" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnAddLibraryFile3" class="icon text-dark-50 flaticon-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnDeleteLibraryFile3" class="icon text-dark-50 flaticon-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                    </td>
                                                    <td>
                                                        <div id="divFileUpload3" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                            <div class="">
                                                                <span>
                                                                    <input type="file" name="uploadFile" id="inputFile3" visible="false" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                        data-iconname="ion-image mr-5"
                                                                        class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                        runat="server" />
                                                                </span>
                                                            </div>

                                                            <telerik:RadAsyncUpload RenderMode="Lightweight" CssClass="" runat="server" ID="RadAsyncUpload3" OnClientFileUploaded="onClientFileUploaded"
                                                                MaxFileInputsCount="1" />

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="tr4" runat="server" visible="false">
                                                    <td style="width: 220px;">
                                                        <asp:DropDownList ID="Ddlcategory4" Width="200" CssClass="form-control" runat="server" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnAddLibraryFile4" class="icon text-dark-50 flaticon-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnDeleteLibraryFile4" class="icon text-dark-50 flaticon-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                    </td>
                                                    <td>
                                                        <div id="divFileUpload4" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                            <div class="">
                                                                <span>
                                                                    <input type="file" name="uploadFile" id="inputFile4" visible="false" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                        data-iconname="ion-image mr-5"
                                                                        class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                        runat="server" />
                                                                </span>
                                                            </div>

                                                            <telerik:RadAsyncUpload RenderMode="Lightweight" CssClass="" runat="server" ID="RadAsyncUpload4" OnClientFileUploaded="onClientFileUploaded"
                                                                MaxFileInputsCount="1" />

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="tr5" runat="server" visible="false">
                                                    <td style="width: 220px;">
                                                        <asp:DropDownList ID="Ddlcategory5" Width="200" CssClass="form-control" runat="server" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnAddLibraryFile5" class="icon text-dark-50 flaticon-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnDeleteLibraryFile5" class="icon text-dark-50 flaticon-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                    </td>
                                                    <td>
                                                        <div id="divFileUpload5" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                            <div class="">
                                                                <span>
                                                                    <input type="file" name="uploadFile" id="inputFile5" visible="false" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                        data-iconname="ion-image mr-5"
                                                                        class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                        runat="server" />
                                                                </span>
                                                            </div>

                                                            <telerik:RadAsyncUpload RenderMode="Lightweight" CssClass="" runat="server" ID="RadAsyncUpload5" OnClientFileUploaded="onClientFileUploaded"
                                                                MaxFileInputsCount="1"/>

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="tr6" runat="server" visible="false">
                                                    <td style="width: 220px;">
                                                        <asp:DropDownList ID="Ddlcategory6" Width="200" CssClass="form-control" runat="server" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnAddLibraryFile6" class="icon text-dark-50 flaticon-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnDeleteLibraryFile6" class="icon text-dark-50 flaticon-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                    </td>
                                                    <td>
                                                        <div id="divFileUpload6" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                            <div class="">
                                                                <span>
                                                                    <input type="file" name="uploadFile" id="inputFile6" visible="false" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                        data-iconname="ion-image mr-5"
                                                                        class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                        runat="server" />
                                                                </span>
                                                            </div>

                                                            <telerik:RadAsyncUpload RenderMode="Lightweight" CssClass="" runat="server" ID="RadAsyncUpload6" OnClientFileUploaded="onClientFileUploaded"
                                                                MaxFileInputsCount="1" />

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="tr7" runat="server" visible="false">
                                                    <td style="width: 220px;">
                                                        <asp:DropDownList ID="Ddlcategory7" Width="200" CssClass="form-control" runat="server" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnAddLibraryFile7" class="icon text-dark-50 flaticon-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnDeleteLibraryFile7" class="icon text-dark-50 flaticon-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                    </td>
                                                    <td>
                                                        <div id="divFileUpload7" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                            <div class="">
                                                                <span>
                                                                    <input type="file" name="uploadFile" id="inputFile7" visible="false" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                        data-iconname="ion-image mr-5"
                                                                        class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                        runat="server" />
                                                                </span>
                                                            </div>

                                                            <telerik:RadAsyncUpload RenderMode="Lightweight" CssClass="" runat="server" ID="RadAsyncUpload7" OnClientFileUploaded="onClientFileUploaded"
                                                                MaxFileInputsCount="1" />

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="tr8" runat="server" visible="false">
                                                    <td style="width: 220px;">
                                                        <asp:DropDownList ID="Ddlcategory8" Width="200" CssClass="form-control" runat="server" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnAddLibraryFile8" class="icon text-dark-50 flaticon-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnDeleteLibraryFile8" class="icon text-dark-50 flaticon-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                    </td>
                                                    <td>
                                                        <div id="divFileUpload8" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                            <div class="">
                                                                <span>
                                                                    <input type="file" name="uploadFile" id="inputFile8" visible="false" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                        data-iconname="ion-image mr-5"
                                                                        class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                        runat="server" />
                                                                </span>
                                                            </div>

                                                            <telerik:RadAsyncUpload RenderMode="Lightweight" CssClass="" runat="server" ID="RadAsyncUpload8" OnClientFileUploaded="onClientFileUploaded"
                                                                MaxFileInputsCount="1" />

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="tr9" runat="server" visible="false">
                                                    <td style="width: 220px;">
                                                        <asp:DropDownList ID="Ddlcategory9" Width="200" CssClass="form-control" runat="server" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnAddLibraryFile9" class="icon text-dark-50 flaticon-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnDeleteLibraryFile9" class="icon text-dark-50 flaticon-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                    </td>
                                                    <td>
                                                        <div id="divFileUpload9" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                            <div class="">
                                                                <span>
                                                                    <input type="file" name="uploadFile" id="inputFile9" visible="false" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                        data-iconname="ion-image mr-5"
                                                                        class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                        runat="server" />
                                                                </span>
                                                            </div>

                                                            <telerik:RadAsyncUpload RenderMode="Lightweight" CssClass="" runat="server" ID="RadAsyncUpload9" OnClientFileUploaded="onClientFileUploaded"
                                                                MaxFileInputsCount="1" />

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="tr10" runat="server" visible="false">
                                                    <td style="width: 220px;">
                                                        <asp:DropDownList ID="Ddlcategory10" Width="200" CssClass="form-control" runat="server" />
                                                    </td>
                                                    <td style="width: 20px;"></td>
                                                    <td style="width: 20px;">
                                                        <a id="ImgBtnDeleteLibraryFile10" class="icon text-dark-50 flaticon-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                    </td>
                                                    <td>
                                                        <div id="divFileUpload10" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                            <div class="">
                                                                <span>
                                                                    <input type="file" name="uploadFile" id="inputFile10" visible="false" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                        data-iconname="ion-image mr-5"
                                                                        class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                        runat="server" />
                                                                </span>
                                                            </div>

                                                            <telerik:RadAsyncUpload RenderMode="Lightweight" CssClass="" runat="server" ID="RadAsyncUpload10" OnClientFileUploaded="onClientFileUploaded"
                                                                MaxFileInputsCount="1" />

                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="row" style="margin-top: 25px;">
                                            <controls:MessageControl ID="MessageControlNoCategories" runat="server" />
                                        </div>
                                    </div>

                                    <div id="divVendorsUploadFilesAreaStepThree" visible="false" runat="server" class="col-md-12">
                                        <asp:Label ID="LblSummaryPartners" runat="server" Text="The above partners are going to receive these files:" /><br />
                                        <br />
                                        <strong>
                                            <span>
                                                <asp:PlaceHolder ID="PhSummaryPartners" runat="server" />
                                                <asp:Label ID="LblSummaryReceivers" runat="server" />
                                            </span>
                                        </strong>
                                        <br />
                                        <br />
                                        <asp:Label ID="LblProceed" Text="If you agree, click below the button Proceed" runat="server" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row">
                                <controls:MessageControl ID="UcReceiversAndUploadFile" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <a id="aBtnLoadPreviousStep" role="button" onserverclick="aBtnLoadPreviousStep_ServerClick" visible="false" runat="server" title="previous step" class="btn btn-light-primary">Previous
                            </a>
                            <asp:Button ID="aBtnLoadNextStep" OnClick="aBtnLoadNextStep_ServerClick" Width="110" runat="server" Text="Next" class="btn btn-primary" />
                            <a id="BtnUploadFile" class="btn btn-primary flaticon-paper-plane-1" onserverclick="BtnUploadFile_OnCick" onclientclick="updatePictureAndInfo(); return false;" visible="false" runat="server">Send Files
                            </a>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Upload Preview file form (modal view) -->
    <div id="divUploadPreviewFile" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel7">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="Label1" Text="Preview file image" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Always">
                                    <ContentTemplate>
                                        <div class="col-md-12">
                                            <div id="divPreviewFileUpload" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                <div class="">
                                                    <span>
                                                        <input type="file" name="uploadFile" id="inputPreviewFile" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                            data-iconname="ion-image mr-5"
                                                            class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                            runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="aBtnUploadPreviewFile" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="row" style="margin-top: 25px;">
                                <controls:MessageControl ID="UcPreviewFileUploadAlert" runat="server" />
                            </div>
                            <div class="row" style="font-style: italic;">
                                <asp:Label ID="LblPreviewImgTip" runat="server" Text="* Best image dimensions 150x150 pixels and max image size 20kb" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-light-primary" data-dismiss="modal">Close</button>
                            <a id="aBtnUploadPreviewFile" role="button" onserverclick="aBtnUploadPreviewFile_OnCick" runat="server" title="Upload/Save file" class="btn btn-primary">Upload
                            </a>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">
            function showButton() {
                document.getElementById('<%=tr2.ClientID%>').style.visibility = 'visible';
            }
        </script>

        <script type="text/javascript">
            //<![CDATA[
            Sys.Application.add_load(function () {
                window.upload = $find("<%=RadAsyncUpload1.ClientID %>");
                window.upload = $find("<%=RadAsyncUpload2.ClientID %>");
                window.upload = $find("<%=RadAsyncUpload3.ClientID %>");
                window.upload = $find("<%=RadAsyncUpload4.ClientID %>");
                window.upload = $find("<%=RadAsyncUpload5.ClientID %>");
                window.upload = $find("<%=RadAsyncUpload6.ClientID %>");
                window.upload = $find("<%=RadAsyncUpload7.ClientID %>");
                window.upload = $find("<%=RadAsyncUpload8.ClientID %>");
                window.upload = $find("<%=RadAsyncUpload9.ClientID %>");
                window.upload = $find("<%=RadAsyncUpload10.ClientID %>");
                demo.initialize();
            });
            //]]>

            (function () {
                var $;

                var upload;
                var demo = window.demo = window.demo || {};

                demo.initialize = function () {
                    $ = $telerik.$;
                    upload = window.upload;
                };

                window.onClientFileUploaded = function (sender, args) {
                    //BtnUploadFile.enable();
                };

                window.updatePictureAndInfo = function () {
                    if (upload.getUploadedFiles().length > 0) {
                        __doPostBack('BtnUploadFile', 'BtnUploadFileArgs');
                    }
                    else {
                        alert("Please select file/category");
                    }
                };
            })();


        </script>
        <style>
            .RadUpload_MetroTouch .ruSelectWrap .ruFakeInput {
                border-color: #e0e0e0;
                color: #333;
                background-color: #fff;
                height: 30px;
            }

            .RadUpload_MetroTouch .ruSelectWrap .ruButton {
                border-color: #e0e0e0;
                color: #000;
                background-color: #f9f9f9;
                height: 30px;
            }
        </style>
        <script type="text/javascript">

            function CloseGroupPopUp() {
                $('#divCreateLibraryGroup').modal('hide');
            }

            function OpenGroupPopUp() {
                $('#divCreateLibraryGroup').modal('show');
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

            function OpenConfPopUpVideo() {
                $('#divVideoConfirm').modal('show');
            }

            function CloseConfPopUpVideo() {
                $('#divVideoConfirm').modal('hide');
            }

            function OpenUploadSendFilePopUp() {
                $('#divUploadSendFile').modal('show');
            }

            function CloseUploadSendFilePopUp() {
                $('#divUploadSendFile').modal('hide');
            }

            function OpenUploadPreviewFilePopUp() {
                $('#divUploadPreviewFile').modal('show');
            }

            function CloseUploadPreviewFilePopUp() {
                $('#divUploadPreviewFile').modal('hide');
            }
        </script>

    </telerik:RadScriptBlock>

</asp:Content>
