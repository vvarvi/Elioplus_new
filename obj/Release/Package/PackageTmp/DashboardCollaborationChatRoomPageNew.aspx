<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardCollaborationChatRoomPageNew.aspx.cs" Inherits="WdS.ElioPlus.DashboardCollaborationChatRoomPageNew" %>

<%@ MasterType VirtualPath="~/MasterDashboard.Master" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/MailBox/ChatGrids/uc_RdgMailBoxList.ascx" TagName="UcRdgMailBoxList" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/UserControls/SelectLibraryFiles.ascx" TagName="UcSelectLibraryFiles" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/Library/LibraryViewSendControl.ascx" TagName="UcCollaborationLibrary" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager ID="masterRadAjaxManager" runat="server" RestoreOriginalRenderDelegate="false">
    </telerik:RadAjaxManager>

    <!--begin::Entry-->
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="d-flex flex-column-fluid">
                <!--begin::Container-->
                <div class="container">
                    <!--begin::Dashboard-->

                    <!--begin::Chat-->
                    <div class="d-flex flex-row">
                        <!--begin::Aside-->
                        <div class="flex-row-auto offcanvas-mobile w-350px w-xl-400px" id="kt_chat_aside" style="min-height: 600px; margin-bottom: 25px;">
                            <!--begin::Card-->
                            <div class="card card-custom card-stretch" style="min-height: 500px; margin-bottom: 20px;">
                                <!--begin::Body-->
                                <div class="card-body">
                                    <div class="navi navi-hover navi-active navi-link-rounded navi-bold navi-icon-center navi-light-icon">

                                        <!--begin:Search-->
                                        <div class="input-group input-group-solid">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <span class="svg-icon svg-icon-lg">
                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/General/Search.svg-->
                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                <rect x="0" y="0" width="24" height="24" />
                                                                <path d="M14.2928932,16.7071068 C13.9023689,16.3165825 13.9023689,15.6834175 14.2928932,15.2928932 C14.6834175,14.9023689 15.3165825,14.9023689 15.7071068,15.2928932 L19.7071068,19.2928932 C20.0976311,19.6834175 20.0976311,20.3165825 19.7071068,20.7071068 C19.3165825,21.0976311 18.6834175,21.0976311 18.2928932,20.7071068 L14.2928932,16.7071068 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                <path d="M11,16 C13.7614237,16 16,13.7614237 16,11 C16,8.23857625 13.7614237,6 11,6 C8.23857625,6 6,8.23857625 6,11 C6,13.7614237 8.23857625,16 11,16 Z M11,18 C7.13400675,18 4,14.8659932 4,11 C4,7.13400675 7.13400675,4 11,4 C14.8659932,4 18,7.13400675 18,11 C18,14.8659932 14.8659932,18 11,18 Z" fill="#000000" fill-rule="nonzero" />
                                                            </g>
                                                        </svg>
                                                        <!--end::Svg Icon-->
                                                    </span>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="TbxSearch" AutoPostBack="true" OnTextChanged="TbxSearch_OnTextChanged" CssClass="form-control py-4 h-auto" placeholder="Email" runat="server" />
                                            <asp:TextBox ID="TbxAdvancedSearch" AutoPostBack="true" Visible="false" OnTextChanged="TbxSearch_OnTextChanged" CssClass="form-control py-4 h-auto" placeholder="Search by Country/Region/Type" runat="server" />
                                        </div>
                                        <!--end:Search-->
                                        <div style="float: right; padding-left: 10px; display: inline-block; width: 100%; font-size: 12px; padding: 5px; color: #2b3643 !important;">
                                            <asp:LinkButton ID="LnkBtnAdvancedSearch" OnClick="LnkBtnAdvancedSearch_OnClck" Style="color: #2b3643 !important; text-decoration: underline;" Text="Advanced search" runat="server" />
                                        </div>
                                        <div id="divAdvancedSearch" visible="false" style="height: 175px; font-size: 12px;" runat="server">
                                            <table>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:DropDownList AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DdlCountries_OnSelectedIndexChanged" ID="DdlCountries" runat="server" />
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:DropDownList AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DdlRegions_OnSelectedIndexChanged" ID="DdlRegions" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:CheckBoxList Visible="false" ID="CbxSearchList" runat="server">
                                                <asp:ListItem Text="By country" Value="Country" />
                                                <asp:ListItem Text="By region" Value="Region" />
                                                <asp:ListItem Text="By name & partner program" Value="Type" />
                                            </asp:CheckBoxList>
                                            <div style="height: 70px; font-size: 10px; margin-left: 10px; line-height: 1px; color: #2b3643;">
                                                <table>
                                                    <tr>
                                                        <td style="width: 105px;">
                                                            <asp:CheckBox AutoPostBack="true" ID="Cbx1" OnCheckedChanged="Cbx1_OnCheckedChanged" runat="server" />
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:CheckBox AutoPostBack="true" ID="Cbx2" OnCheckedChanged="Cbx1_OnCheckedChanged" runat="server" />
                                                        </td>
                                                        <td style="width: 130px;">
                                                            <asp:CheckBox AutoPostBack="true" ID="Cbx3" OnCheckedChanged="Cbx1_OnCheckedChanged" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox AutoPostBack="true" ID="Cbx4" OnCheckedChanged="Cbx1_OnCheckedChanged" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox AutoPostBack="true" ID="Cbx5" OnCheckedChanged="Cbx1_OnCheckedChanged" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox AutoPostBack="true" ID="Cbx6" OnCheckedChanged="Cbx1_OnCheckedChanged" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox AutoPostBack="true" ID="Cbx7" OnCheckedChanged="Cbx1_OnCheckedChanged" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>

                                        <div class="text-center">
                                        </div>

                                        <!--begin:Users-->
                                        <div id="divTabConnections" runat="server" class="navi-item my-2 mb-6">
                                            <asp:Repeater ID="RptConnectionList" OnItemDataBound="RptConnectionList_OnItemDataBound" runat="server">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="id" runat="server" />
                                                    <asp:HiddenField ID="master_user_id" runat="server" />
                                                    <asp:HiddenField ID="invitation_status" runat="server" />
                                                    <asp:HiddenField ID="partner_user_id" runat="server" />
                                                    <asp:HiddenField ID="email" runat="server" />
                                                    <!--begin:User-->
                                                    <div id="divMediaBody" runat="server" class="d-flex align-items-center justify-content-between mb-5">
                                                        <div class="d-flex align-items-center">
                                                            <div class="symbol symbol-circle symbol-50 mr-3">
                                                                <asp:ImageButton ID="ImgBtnCompanyLogo" Width="34" Height="34" OnClick="ImgBtnCompanyLogo_OnClick" ImageUrl="/assets/media/users/300_12.jpg" runat="server" />
                                                                <span id="spanPartnerStatus" class="label label-sm label-success" runat="server"></span>
                                                            </div>
                                                            <div class="d-flex flex-column">
                                                                <a id="BtnCompanyName" runat="server" onserverclick="BtnCompanyName_OnClick" class="text-dark-75 text-hover-primary font-weight-bold font-size-lg">
                                                                    <asp:Label ID="LblCompanyName" Text="Matt Pears" runat="server" />
                                                                </a>
                                                                <span class="text-muted font-weight-bold font-size-sm">
                                                                    <asp:Label ID="LblCountry" Text="Head of Development" runat="server" />
                                                                    <asp:Label ID="LblRegion" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="d-flex flex-column align-items-end">
                                                            <span class="text-muted font-weight-bold font-size-sm">
                                                                <asp:Label ID="LblMessageSendTime" Text="" runat="server" />
                                                            </span>
                                                            <span id="spanMessagesCountNotification" class="label label-sm label-success" title="New unread message" runat="server">
                                                                <asp:Label ID="LblMessagesCount" Text="4" runat="server" />
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <!--end:User-->
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />

                                        </div>
                                        <!--end:Users-->

                                        <div id="divTabGroups" visible="false" runat="server" class="navi-item my-2 mb-6">
                                            <h6 class="text-uppercase pl-20 pr-20">
                                                <asp:Label ID="LblTabGroups" Text="CREATE GROUPS" Visible="false" runat="server" />
                                            </h6>
                                            <asp:Panel ID="PnlCreateRetailorGroups" Style="padding: 5px; text-align: center;" runat="server">
                                                <a id="aCreateRetailor" onserverclick="aCreateRetailor_ServerClick" class="btn btn-primary px-6 font-weight-bold flaticon-user-add" runat="server">
                                                    <asp:Label ID="LblAddGroup" Text="Create new group" runat="server" />
                                                </a>
                                            </asp:Panel>

                                            <asp:Repeater ID="RdgRetailorsGroups" OnLoad="RdgRetailorsGroups_OnNeedDataSource" OnItemDataBound="RdgRetailorsGroups_OnItemDataBound" runat="server">
                                                <ItemTemplate>
                                                    <!--begin:Item-->
                                                    <div id="divNaviItem" runat="server" class="navi-item my-2">
                                                        <span class="navi-icon mr-4" style="float: left; margin-top: 10px;">
                                                            <!--begin::Dropdown Menu-->
                                                            <div id="divMenuActions" runat="server" visible="false" class="dropdown dropdown-inline">
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
                                                                            <a id="aEditGroup" onserverclick="aEditGroup_ServerClick" role="button" runat="server" title="Edit group" class="navi-link">
                                                                                <span class="navi-icon">
                                                                                    <i id="iEdit" runat="server" class="flaticon2-edit"></i>
                                                                                </span>
                                                                                Edit
                                                                            </a>
                                                                        </li>
                                                                        <li class="navi-item">
                                                                            <a id="aDeleteGroup" onserverclick="aDeleteGroup_ServerClick" runat="server" title="Delete group" class="navi-link">
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
                                                        <asp:CheckBox AutoPostBack="true" Visible="false" ID="CbxSelectGroupUsers" runat="server" />
                                                        <a id="aGroupDescription" onserverclick="aGroupDescription_ServerClick" role="button" runat="server" class="navi-link">
                                                            <span id="spanGroupDescriptionView" runat="server" class="navi-text font-weight-bolder font-size-lg" style="width: 200px; display: inline-block;">
                                                                <asp:Label ID="LblGroupDescription" Text='<%# DataBinder.Eval(Container.DataItem, "collaboration_group_name")%>' runat="server" />
                                                            </span>
                                                        </a>
                                                        <span id="spanGroupMessagesCount" class="badge bg-danger" style="margin-top: 0px;" title="New unread group message" visible="false" runat="server">
                                                            <asp:Label ID="LblGroupMessagesCount" Text="" runat="server" />
                                                        </span>
                                                        <asp:HiddenField ID="HdnGroupId" Value='<%# Eval("id") %>' runat="server" />
                                                        <asp:HiddenField ID="Hdnuser_id" Value='<%# Eval("user_id") %>' runat="server" />
                                                        <asp:HiddenField ID="Hdngroup_msgs_count" Value='<%# Eval("group_msgs_count") %>' runat="server" />
                                                        <asp:HiddenField ID="HdnDate_created" Value='<%# Eval("date_created") %>' runat="server" />
                                                        <asp:HiddenField ID="HdnLast_update" Value='<%# Eval("last_update") %>' runat="server" />
                                                        <asp:HiddenField ID="HdnIsActive" Value='<%# Eval("is_active") %>' runat="server" />
                                                        <asp:HiddenField ID="HdnIsPublic" Value='<%# Eval("is_public") %>' runat="server" />
                                                    </div>
                                                    <!--end:Item-->
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <controls:MessageControl ID="MessageControlRetailors" Visible="false" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Card-->
                        </div>
                        <!--end::Aside-->
                        <!--begin::Content-->
                        <div class="flex-row-fluid ml-lg-8" id="kt_chat_content">
                            <!--begin::Card-->
                            <div class="card card-custom">
                                <!--begin::Header-->
                                <div class="card-header align-items-center px-4 py-3">
                                    <div class="text-left flex-grow-1">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <p>
                                                    <asp:Label ID="LblSelectedChatTitle" Text="Select partner to chat with" runat="server" />
                                                </p>
                                                <h6 class="fw-300 fs-24 mt-5 mb-5 lh-1">
                                                    <a id="aChatCompanyLogo" visible="false" runat="server">
                                                        <asp:Image ID="ImgChatCompanyLogo" Visible="false" Width="35" Height="35" CssClass="img-circle chat-avatar" runat="server" />
                                                    </a>
                                                    <asp:Label ID="LblSelectedConnectionToChat" runat="server" />
                                                </h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="text-center text-center">
                                        <div id="divGroupImages" runat="server" visible="false" class="symbol-group symbol-hover justify-content-center">
                                            <div id="divPic1" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip" title="Ana Fox">
                                                <img id="Pic1" runat="server" alt="Pic" width="35" height="35" src="/assets/media/users/300_16.jpg" />
                                            </div>
                                            <div id="divPic2" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip" title="Nich Nilson">
                                                <img id="Pic2" runat="server" alt="Pic" width="35" height="35" src="/assets/media/users/300_21.jpg" />
                                            </div>
                                            <div id="divPic3" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip" title="James Stone">
                                                <img id="Pic3" runat="server" alt="Pic" width="35" height="35" src="/assets/media/users/300_22.jpg" />
                                            </div>
                                            <div id="divPic4" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip" title="Micheal Bold">
                                                <img id="Pic4" runat="server" alt="Pic" width="35" height="35" src="/assets/media/users/300_23.jpg" />
                                            </div>
                                            <div id="divPic5" runat="server" visible="false" class="symbol symbol-35 symbol-circle" data-toggle="tooltip" title="Sean Cooper">
                                                <img id="Pic5" runat="server" alt="Pic" width="35" height="35" src="/assets/media/users/300_15.jpg" />
                                            </div>
                                            <div id="divMorePic" runat="server" visible="false" class="symbol symbol-35 symbol-circle symbol-light-success" data-toggle="tooltip" title="Invite someone">
                                                <span class="symbol-label font-weight-bold">5+</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="text-right flex-grow-1">
                                        <!--begin::Dropdown Menu-->
                                        <div class="dropdown dropdown-inline">
                                            <button type="button" class="btn btn-clean btn-sm btn-icon btn-icon-md" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <span class="svg-icon svg-icon-lg">
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
                                            <div class="dropdown-menu p-0 m-0 dropdown-menu-right dropdown-menu-md">
                                                <!--begin::Navigation-->
                                                <ul class="navi navi-hover py-5">
                                                    <li class="navi-item">
                                                        <a id="BtnViewConnections" runat="server" onserverclick="BtnViewConnections_OnClick" title="Connections" class="navi-link">
                                                            <span class="navi-icon">
                                                                <i class="flaticon2-drop"></i>
                                                            </span>
                                                            <span class="navi-text">My Partners</span>
                                                            <span class="navi-link-badge">
                                                                <span id="spanConnectionsMsgCount" runat="server" visible="false" title="New unread message" class="label label-light-primary label-inline font-weight-bold">
                                                                    <asp:Label ID="LblConnectionsMsgCount" runat="server" />
                                                                </span>
                                                            </span>
                                                        </a>
                                                    </li>
                                                    <li class="navi-item">
                                                        <a id="BtnCreateGroups" runat="server" onserverclick="BtnCreateGroups_OnClick" title="Groups" class="navi-link">
                                                            <span class="navi-icon">
                                                                <i class="flaticon2-rocket-1"></i>
                                                            </span>
                                                            <span class="navi-text">My Groups</span>
                                                            <span class="navi-link-badge">
                                                                <span id="spanGroupMsgCount" runat="server" title="New unread group message" visible="false" class="label label-light-primary label-inline font-weight-bold">
                                                                    <asp:Label ID="LblGroupMsgCount" runat="server" />
                                                                </span>
                                                            </span>
                                                        </a>
                                                    </li>
                                                    <li class="navi-separator my-3"></li>
                                                </ul>
                                                <!--end::Navigation-->
                                            </div>
                                        </div>
                                        <!--end::Dropdown Menu-->
                                    </div>
                                </div>
                                <!--end::Header-->
                                <!--begin::Body-->
                                <div class="card-body">
                                    <!--begin::Scroll-->
                                    <div class="scroll scroll-pull" data-mobile-height="350">
                                        <!--begin::Messages-->
                                        <div class="messages">
                                            <controls:UcRdgMailBoxList Visible="true" ID="UcRdgMailBoxList" runat="server" />
                                        </div>
                                        <!--end::Messages-->
                                    </div>
                                    <!--end::Scroll-->
                                </div>
                                <!--end::Body-->
                                <!--begin::Footer-->
                                <div class="card-footer align-items-center">
                                    <!--begin::Compose-->
                                    <asp:TextBox ID="RtbxNewMessage" TextMode="MultiLine" Rows="2" type="text" Width="100%" aria-describedby="inputSendMessage" placeholder="Type a message" CssClass="form-control border-0 p-0l" runat="server" />
                                    <div class="d-flex align-items-center justify-content-between mt-5">
                                        <div class="mr-3">
                                            <a id="aBtnAttachFile" runat="server" onserverclick="BtnAttachFile_OnClick" title="Attach file to send" class="btn btn-clean btn-icon btn-md mr-1">
                                                <i class="flaticon2-photograph icon-lg"></i>
                                            </a>
                                            <a id="aLibraryFile" visible="false" title="Select file from my library" runat="server" onserverclick="aLibraryFile_ServerClick" class="btn btn-clean btn-icon btn-md">
                                                <i class="flaticon2-photo-camera icon-lg"></i>
                                            </a>
                                        </div>
                                        <div>
                                            <asp:Button ID="BtnAttachFile" Text="Attach File" Visible="false" OnClick="BtnAttachFile_OnClick" CssClass="btn btn-success btn-md text-uppercase font-weight-bold chat-send py-2 px-6" runat="server" />
                                            <a id="ImgBtnSendMsg" onserverclick="ImgBtnSendMsg_OnClick" class="btn btn-primary px-6 font-weight-bold flaticon2-telegram-logo" runat="server">Send
                                            </a>
                                        </div>
                                    </div>
                                    <!--begin::Compose-->

                                    <div id="divLibrarySelectedFile" visible="false" style="margin-top: 20px;" runat="server">
                                        <div class="form-group" style="">
                                            <div class="row">
                                                <div class="col-sm-12" style="">
                                                    <div class="form-group">
                                                        <strong>
                                                            <asp:Label ID="LblLibraryFileTitle" Text="Attached file from Library: " runat="server" />
                                                        </strong>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12" style="">
                                                    <div class="form-group">
                                                        <asp:ImageButton ID="ImgBtnDeleteLibraryFile" ToolTip="Remove attached file" ImageUrl="~/images/icons/small/delete.png" OnClick="ImgBtnDeleteLibraryFile_Click" runat="server" />
                                                        <asp:Label ID="LblLibrarySelectedFile" Text="" runat="server" />
                                                        <asp:HiddenField ID="HdnCategoryId" Value="0" runat="server" />
                                                        <asp:HiddenField ID="HdnFileId" Value="0" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divUpload" visible="false" style="margin-top: 20px;" runat="server">
                                        <div class="form-group" style="">
                                            <div class="row">
                                                <div id="divCategoryListArea" runat="server" visible="false" class="col-sm-6" style="display: none;">
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="DdlCategory" Visible="false" Width="250" Height="30" runat="server" />
                                                    </div>
                                                    <div class="form-group">
                                                        <a href="#UploadFile" visible="false" id="aSelectFrLibrary" data-toggle="modal" class="btn btn-primary" runat="server">
                                                            <i id="iSelectFrLibrary" class="ion-plus-round mr-5" runat="server"></i>
                                                            <asp:Label ID="LblSelectFrLibrary" Text="Select from library" runat="server" />
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12" style="float: left;">
                                                    <telerik:RadAsyncUpload RenderMode="Lightweight" Visible="false" CssClass="photo-upload" runat="server" ID="RadAsyncUpload1" MaxFileInputsCount="1" />
                                                    <input type="file" name="uploadFile" id="inputFile" data-buttonname="btn-black btn-outline" data-iconname="ion-image mr-5" class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .doc, .png, .jpg, .jpeg, .rar, .zip, .tar, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html" runat="server" />
                                                </div>
                                                <div id="divLibraryFiles" visible="false" class="col-md-6" style="float: right; margin-top: 20px; height: 100px;" runat="server">
                                                    <asp:Button ID="BtnSelectFromLibrary" Text="Select from library" CssClass="btn btn-raised btn-success upload" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12 mt-5" style="display: inline-block; text-align: center;">
                                        <controls:MessageControl ID="UcChatMessageAlert" Visible="false" runat="server" />
                                    </div>
                                </div>
                                <!--end::Footer-->
                            </div>
                            <!--end::Card-->
                        </div>
                        <!--end::Content-->
                    </div>
                    <!--end::Chat-->

                    <!--end::Dashboard-->
                </div>
                <!--end::Container-->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--end::Entry-->

    <!-- Pop Up Create Retailor form Message (modal view) -->
    <div id="divCreateRetailor" class="modal fade" tabindex="-1">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content" style="width: 600px;">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblRetailorTitle" Text="Add Resellers to Group" CssClass="control-label" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblGroupName" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxGroupName" MaxLength="145" EnableViewState="true" ViewStateMode="Enabled" CssClass="form-control" BorderStyle="None" placeholder="Enter group name here" data-placement="top" runat="server" />
                                    </div>
                                    <div class="form-group">
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
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx1" OnCheckedChanged="Rcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                            <td style="width: 80px;">
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx2" OnCheckedChanged="Rcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                            <td style="width: 110px;">
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx3" OnCheckedChanged="Rcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx4" OnCheckedChanged="Rcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx5" OnCheckedChanged="Rcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx6" OnCheckedChanged="Rcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="Rcbx7" OnCheckedChanged="Rcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Label ID="LblRetailorsList" CssClass="control-label" runat="server" />
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
                                                                                <asp:Label ID="LblSelect" Text="Select All" runat="server" />
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
                                                                            <td style="width: 50px; display: none;">
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
                                                                                <asp:HiddenField ID="id" Value='<%# Eval("id") %>' runat="server" />
                                                                                <%--<asp:HiddenField ID="collaboration_group_id" runat="server" /> --%>
                                                                                <asp:HiddenField ID="master_user_id" Value='<%# Eval("master_user_id") %>' runat="server" />
                                                                                <asp:HiddenField ID="invitation_status" Value='<%# Eval("invitation_status") %>' runat="server" />
                                                                                <asp:HiddenField ID="partner_user_id" Value='<%# Eval("partner_user_id") %>' runat="server" />
                                                                                <asp:HiddenField ID="email" Value='<%# Eval("email") %>' runat="server" />
                                                                            </td>
                                                                            <td style="width: 90px;">
                                                                                <asp:CheckBox AutoPostBack="false" ID="CbxSelectUser" runat="server" />
                                                                            </td>
                                                                            <td style="width: 90px;">
                                                                                <a id="aCompanyLogo" href="javascript:;" runat="server">
                                                                                    <div class="media-left avatar">
                                                                                        <asp:ImageButton ID="ImgBtnCompanyLogo" Width="34" Height="34" class="media-object img-circle" runat="server" />
                                                                                    </div>
                                                                                </a>
                                                                            </td>
                                                                            <td style="width: 300px;">
                                                                                <a id="aCompanyName" runat="server">
                                                                                    <asp:Label ID="LblCompanyName" Text='<%# Eval("company_name") %>' runat="server" />
                                                                                </a>
                                                                            </td>
                                                                            <td style="width: 100px;">
                                                                                <asp:Label ID="LblCountry" Text='<%# Eval("country") %>' runat="server" />
                                                                                <asp:Label ID="LblRegion" Text='<%# Eval("region") %>' runat="server" />
                                                                            </td>
                                                                            <td>
                                                                                <span id="spanMessagesCountNotification" visible="false" class="badge bg-danger" title="New unread message" runat="server">
                                                                                    <asp:Label ID="LblMessagesCount" Text='<%# Eval("msgs_count") %>' runat="server" />
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </li>
                                                            </ul>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <controls:MessageControl ID="MessageControlCreateRetailors" Visible="false" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="divRetailorFailure" runat="server" visible="false" class="alert alert-custom alert-light-danger fade show" style="text-align: justify; width: 100%;">
                                    <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblRetailorFailure" Text="Error! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblRetailorFailureMsg" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                        </button>
                                    </div>
                                </div>
                                <div id="divRetailorSuccess" runat="server" visible="false" class="alert alert-custom alert-light-success fade show" style="text-align: justify; width: 100%;">
                                    <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblRetailorSuccess" Text="Done! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblRetailorSuccessMsg" runat="server" />
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
                            <asp:Button ID="BtnCreate" OnClick="BtnCreate_OnClick" CssClass="btn btn-primary" runat="server" Text="Create" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up Edit Retailor form Message (modal view) -->
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
                                                <asp:LinkButton ID="LnkBtnEditRetailorAdvancedSearch" OnClick="LnkBtnEditRetailorAdvancedSearch_OnClck" Style="color: #2b3643 !important; text-decoration: underline;" Text="Advanced search" runat="server" />
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
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx1" OnCheckedChanged="EdRcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                            <td style="width: 80px;">
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx2" OnCheckedChanged="EdRcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                            <td style="width: 110px;">
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx3" OnCheckedChanged="EdRcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx4" OnCheckedChanged="EdRcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx5" OnCheckedChanged="EdRcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx6" OnCheckedChanged="EdRcbx1_OnCheckedChanged" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox AutoPostBack="true" ID="EdRcbx7" OnCheckedChanged="EdRcbx1_OnCheckedChanged" runat="server" />
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
                                                    <asp:Repeater ID="RptEditRetailorsList" OnItemDataBound="RptEditRetailorsList_OnItemDataBound" runat="server">
                                                        <HeaderTemplate>
                                                            <ul class="chat-list m-0 media-list">
                                                                <li class="media">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 90px; height: 50px;">
                                                                                <asp:CheckBox AutoPostBack="true" ID="CbxSelectAll" OnCheckedChanged="CbxEditSelectAll_OnCheckedChanged" runat="server" />
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
    <!-- Pop Up Upload File form Message (modal view) -->
    <div id="UploadFile" class="modal fade" tabindex="-1" data-width="800">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblUploadFileTitle" Text="Choose file to send" CssClass="control-label" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" CssClass="control-label" runat="server" />
                                        <div class="widget-heading">
                                            <asp:Label ID="LblFileLibrary" runat="server" />
                                        </div>
                                        <telerik:RadGrid ID="RpLibraryFiles" Visible="false" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="false" HeaderStyle-BackColor="#02a8f3" ShowHeader="true" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" CssClass="rgdd" OnDetailTableDataBind="RpLibraryFiles_DetailTableDataBind" OnPreRender="RpLibraryFiles_PreRender" OnItemDataBound="RpLibraryFiles_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                            <MasterTableView Width="100%" DataKeyNames="id" Name="Parent" AllowMultiColumnSorting="true">
                                                <NoRecordsTemplate>
                                                    <div class="emptyGridHolder">
                                                        <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                    </div>
                                                </NoRecordsTemplate>
                                                <DetailTables>
                                                    <telerik:GridTableView DataKeyNames="id" Name="UserLibraryFiles" HeaderStyle-BackColor="#2b3643" ShowHeader="false" HeaderStyle-ForeColor="#ffffff" Width="100%" AllowPaging="true" PageSize="7">
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" DataField="id" UniqueName="id" />
                                                            <telerik:GridBoundColumn Display="false" DataField="user_id" UniqueName="user_id" />
                                                            <telerik:GridBoundColumn Display="false" DataField="uploaded_by_user_id" UniqueName="uploaded_by_user_id" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" DataField="user_guid" UniqueName="user_guid" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50" Display="false" DataField="file_path" UniqueName="file_path" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="100" Display="false" DataField="selected_category" UniqueName="selected_category" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="100" Display="false" DataField="company_name" UniqueName="company_name" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="100" Display="false" DataField="file_type" UniqueName="file_type" />
                                                            <telerik:GridBoundColumn Display="false" HeaderStyle-Width="70" DataField="fileName" UniqueName="fileName" />
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="50" DataField="fileName_Temp" UniqueName="fileName_Temp">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox AutoPostBack="true" Visible="true" ID="CbxSelectFile" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="50" DataField="fileName_Temp" UniqueName="fileName_Temp">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HpLnkPreViewLogFiles" Target="_blank" Style="text-decoration: underline;" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="50" DataField="from_to" UniqueName="from_to">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="ImgFromTo" AlternateText="send" ImageUrl="~/images/icons/small/info.png" Style="cursor: pointer;" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="80" Display="false" DataField="fileSize" UniqueName="fileSize" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="100" Display="false" DataField="date" UniqueName="date" />
                                                            <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                            <telerik:GridBoundColumn Display="false" DataField="collaboration_group_id" UniqueName="collaboration_group_id" />
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="80" DataField="actions" UniqueName="actions">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgBtnPreViewLogFiles" OnClick="ImgBtnPreViewLogFiles_OnClick" ImageUrl="~/Images/preview.png" ToolTip="Set as not new" runat="server" />
                                                                    <asp:ImageButton ID="ImgBtnDeleteLogFiles" OnClick="ImgBtnDeleteLogFiles_OnClick" ToolTip="Delete log file" ImageUrl="~/Images/icons/small/delete.png" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </telerik:GridTableView>
                                                </DetailTables>
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="10" DataField="select" UniqueName="select">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgBtnFolder" ImageUrl="/images/icons/small/all_notes_1.png" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                    <telerik:GridBoundColumn DataField="category_description" HeaderText="Category" UniqueName="category_description" />
                                                    <telerik:GridBoundColumn HeaderText="Files in category count" DataField="total_files_count" UniqueName="total_files_count" />
                                                    <telerik:GridBoundColumn Display="false" DataField="partner_total_files_count" UniqueName="partner_total_files_count" />
                                                    <telerik:GridBoundColumn Display="false" HeaderText="New files count" DataField="new_files_count" UniqueName="new_files_count" />
                                                    <telerik:GridBoundColumn Display="false" DataField="sysdate" UniqueName="sysdate" />
                                                    <telerik:GridBoundColumn Display="false" DataField="is_default" UniqueName="is_default" />
                                                    <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                        <controls:MessageControl ID="UcMessageAlertLibraryControl" Visible="false" runat="server" />
                                        <controls:UcSelectLibraryFiles ID="UcSelectLibraryFiles" runat="server" />
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div id="divUploadFailure" runat="server" visible="false" class="alert alert-custom alert-light-danger fade show" style="text-align: justify; width: 100%;">
                                    <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblUploadFailureError" Text="Error! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblUploadFailureMsg" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                        </button>
                                    </div>
                                </div>
                                <div id="divUploadSuccess" runat="server" visible="false" class="alert alert-custom alert-light-success fade show" style="text-align: justify; width: 100%;">
                                    <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblUploadSuccessDone" Text="Done! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblUploadSuccessMsg" runat="server" />
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
                            <asp:Button ID="BtnUpload" OnClick="BtnUpload_OnClick" CssClass="btn btn-success" runat="server" Text="Select" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgBtnSendMsg" />
                <asp:PostBackTrigger ControlID="BtnAttachFile" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up Invitation form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="LblSendTitle" Text="Invitation Send" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblSendfMsg" CssClass="control-label" runat="server" />
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
        <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Always">
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
                                <div id="divConfirmationMessage" runat="server" style="width: 100%;" class="alert alert-custom alert-light-warning fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">                                        
                                        <asp:Label ID="LblConfirmationMessage" runat="server" />
                                    </div>                                    
                                </div>
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
                                        <i class="flaticon-warning"></i>
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
                            <asp:Button ID="BtnDeleteFile" Visible="false" Text="Delete" CssClass="btn btn-primary" runat="server" />
                            <asp:Button ID="BtnDeletePreviewFile" Visible="false" Text="Delete" CssClass="btn btn-primary" runat="server" />
                            <asp:Button ID="BtnDeleteCategory" Visible="false" Text="Delete" CssClass="btn btn-primary" runat="server" />
                            <asp:Button ID="BtnDeleteGroup" Visible="false" OnClick="BtnDeleteGroup_Click" Text="Delete" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up Library send file to Message (modal view) -->
    <div id="PopUpLibrary" class="modal fade" tabindex="-1" role="dialog" style="" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel6">
            <ContentTemplate>
                <div role="document" class="modal-dialog" style="">
                    <div class="modal-content" style="width: 900px !important;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="Label1" Text="My Library Files" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <controls:UcCollaborationLibrary ID="UcCollaborationLibrary" runat="server" />
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

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <!--begin::Page Scripts(used by this page)-->
        <script src="/assets/js/pages/custom/chat/chat.js"></script>
        <!--end::Page Scripts-->

        <script type="text/javascript">

            var objDiv = document.getElementById("myBoxList");

            objDiv.scrollTop = objDiv.scrollHeight;

        </script>
        <style type="text/css">
            #bottom {
                height: 2px;
            }

            .btn-warning.upload:hover {
                color: #737373 !important;
                background-color: #ffae04 !important;
                border-color: #ffae04 !important;
            }
        </style>

        <script type="text/javascript">

            //<![CDATA[
            Sys.Application.add_load(function () {

                window.upload = $find("<%=RadAsyncUpload1.ClientID %>");

                demo.initialize();
            });

            //]]>

            (function () {
                var $;
                var countriesCombo;
                var citiesCombo;
                var upload;
                var demo = window.demo = window.demo || {};

                demo.initialize = function () {
                    $ = $telerik.$;

                    upload = window.upload;
                };

            })();

            function CloseCreateRetailorPopUp() {
                $('#divCreateRetailor').modal('hide');
            }

            function OpenCreateRetailorPopUp() {
                $('#divCreateRetailor').modal('show');
            }
            function CloseEditPopUp() {
                $('#EditRetailor').modal('hide');
            }

            function OpenEditPopUp() {
                $('#EditRetailor').modal('show');
            }
            function CloseUploadPopUp() {
                $('#UploadFile').modal('hide');
            }

            function OpenUploadPopUp() {
                $('#UploadFile').modal('show');
            }

            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }

            function OpenLibraryPopUp() {
                $('#PopUpLibrary').modal('show');
            }

            function CloseLibraryPopUp() {
                $('#PopUpLibrary').modal('hide');
            }

            function OpenConfPopUp() {
                $('#divConfirm').modal('show');
            }

            function CloseConfPopUp() {
                $('#divConfirm').modal('hide');
            }

        </script>

    </telerik:RadScriptBlock>

</asp:Content>
