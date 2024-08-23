<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardOpportunitiesPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardOpportunitiesPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
    <!--begin::Page Custom Styles(used by this page)-->
    <link href="/assets/plugins/custom/kanban/kanban.bundle.css" rel="stylesheet" type="text/css" />
    <!--end::Page Custom Styles-->
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <!--begin::Entry-->
            <div class="d-flex flex-column-fluid">
                <!--begin::Container-->
                <div class="container">
                    <!--begin::Dashboard-->

                    <!--begin::Card-->
                    <div class="card card-custom gutter-b">
                        <div class="card-header">
                            <div class="card-title">
                                <h3 class="card-label">
                                    <asp:Label ID="LblDashPage" Text="Opportunities" runat="server" />
                                </h3>
                            </div>
                            <div class="card-title text-right mr-7">
                                <a id="aAddOpportunity" runat="server" class="btn btn-primary font-weight-bold">
                                    <asp:Label ID="Label3" Text="Add New Opportunity" runat="server" />
                                </a>
                            </div>
                        </div>
                        <div class="card-body">

                            <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />

                            <div class="card-header flex-wrap border-0 pt-6 pb-0">

                                <div class="card-toolbar">
                                </div>
                            </div>
                            <div class="mb-7">
                                <div class="row align-items-center">
                                    <div class="col-lg-9 col-xl-8">
                                        <div class="row align-items-center">
                                            <div class="col-md-6 my-4 my-md-0">
                                                <div class="d-flex align-items-center">
                                                    <asp:DropDownList ID="DdlOpportKinds" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0" Text="All opportunities" Selected="True" />
                                                        <asp:ListItem Value="1" Text="Opportunities with open tasks" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6 my-4 my-md-0">
                                                <div class="d-flex align-items-center">
                                                    <asp:TextBox ID="TbxSearch" placeholder="Organization Name" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-xl-4 mt-5 mt-lg-0">
                                        <asp:Button ID="BtnSearch" OnClick="BtntSort_OnClick" CssClass="btn btn-primary" runat="server" Text="Search" />
                                    </div>
                                </div>
                            </div>

                            <div id="kt_kanban_1">
                                <div class="kanban-container">
                                    <div data-id="_inprocess" data-order="1" class="kanban-board" style="width: 262px; margin-left: 0px; margin-right: 0px;">
                                        <header class="kanban-board-header primary">
                                            <div class="kanban-title-board">
                                                <div class="mt-step-title">
                                                    <a id="EditStatusOne" style="float: left;" onserverclick="EditStatusOne_OnClick" runat="server">
                                                        <i id="iEditStatusOne" class="flaticon-edit" title="Change opportunity status description" runat="server"></i>
                                                    </a>
                                                    <a id="DeleteStatusOne" style="float: left;" onserverclick="DeleteStatusOne_OnCick" visible="false" runat="server">
                                                        <i id="iDeleteStatusOne" class="flaticon-delete" title="Delete custom and use default" runat="server"></i>
                                                    </a>
                                                    <div style="float: left; margin-left: 5px;">
                                                        <asp:Label ID="LblStatusOne" Text="Contact" runat="server" />
                                                    </div>
                                                    <div style="float: left; margin-left: 5px; margin-top: -7px;">
                                                        <asp:TextBox ID="TbxStatusOne" Visible="false" CssClass="form-control" Width="95" runat="server" />
                                                    </div>
                                                    <span style="margin-left: 15px;">
                                                        <asp:Label ID="LblAllContacts" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                        </header>
                                        <div class="row ml-2 mt-2">
                                            <asp:DropDownList ID="DdlContactSort" CssClass="form-control" Width="195" runat="server">
                                                <asp:ListItem Value="0" Text="Order by date" Selected="True" />
                                                <asp:ListItem Value="1" Text="Order by title" />
                                                <asp:ListItem Value="2" Text="Order by notes" />
                                                <asp:ListItem Value="3" Text="Order by tasks" />
                                            </asp:DropDownList>
                                            <asp:Button ID="BtnContactSort" OnClick="BtntContactSort_OnClick" CssClass="btn btn-light-primary" runat="server" Text="Sort" />
                                        </div>
                                        <asp:Repeater ID="RptContacts" OnLoad="RptContacts_Load" OnItemDataBound="RptContacts_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <main class="kanban-drag" style="min-height: 0px;">
                                                    <div class="kanban-item">
                                                        <div class="card-body pt-4" style="padding: 1.5rem;">
                                                            <!--begin::User-->
                                                            <div class="d-flex align-items-end mb-7">
                                                                <!--begin::Pic-->
                                                                <div class="d-flex align-items-center">
                                                                    <!--begin::Symbol-->
                                                                    <div id="divImg" runat="server" class="symbol symbol-light-primary mr-3">
                                                                        <span class="symbol-label">
                                                                            <asp:Image ID="img" runat="server" Width="45" Height="45" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "companyLogo")%>' AlternateText="" />
                                                                        </span>
                                                                    </div>
                                                                    <div id="divImgNo" runat="server" visible="false" class="symbol symbol-light-primary mr-3 symbol-light-primary">
                                                                        <span class="symbol-label font-size-h3 font-weight-boldest">NO</span>
                                                                    </div>
                                                                    <!--end::Symbol-->
                                                                    <!--begin::Title-->
                                                                    <div class="d-flex flex-column align-items-start">
                                                                        <span class="text-dark-50 font-weight-bold mb-1">
                                                                            <asp:Label ID="LblOrganName" Text='<%# DataBinder.Eval(Container.DataItem, "opportOrganName")%>' runat="server" />
                                                                        </span>
                                                                    </div>
                                                                    <!--end::Title-->
                                                                </div>
                                                                <!--end::Title-->
                                                            </div>
                                                            <!--end::User-->
                                                            <!--begin::Info-->
                                                            <div class="mb-10 mt-12">
                                                                <div class="d-flex justify-content-between align-items-center">
                                                                    <span class="label label-inline label font-weight-bold mr-2 mb-4" style="font-size: 13px;">Notes:</span>
                                                                    <a id="aNotes" runat="server" class="text-muted text-hover-light-primary">
                                                                        <asp:Label ID="LblNotes" CssClass="label label-lg label-primary label-inline mb-4" Text='<%# DataBinder.Eval(Container.DataItem, "opportNotes")%>' runat="server" />
                                                                    </a>
                                                                </div>
                                                                <div class="d-flex justify-content-between align-items-center">
                                                                    <span class="label label-inline label font-weight-bold mr-2" style="font-size: 13px;">Tasks:</span>
                                                                    <a id="aTasks" runat="server" class="text-muted text-hover-primary">
                                                                        <span class="text-muted font-weight-bold">
                                                                            <asp:Label ID="LblTasks" CssClass="label label-lg label-primary label-inline" Text='<%# DataBinder.Eval(Container.DataItem, "opportTasks")%>' runat="server" />
                                                                        </span>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                            <!--end::Info-->
                                                            <!--begin::Toolbar-->
                                                            <div class="d-flex justify-content-end">
                                                                <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Settings" data-placement="right">
                                                                    <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                        <i class="ki ki-bold-more-hor"></i>
                                                                    </a>
                                                                    <div class="dropdown-menu dropdown-menu-md dropdown-menu-left">
                                                                        <!--begin::Navigation-->
                                                                        <ul class="navi navi-hover">
                                                                            <li class="navi-header font-weight-bold py-4">
                                                                                <span class="font-size-lg">Actions</span>
                                                                                <i class="flaticon2-information icon-md text-muted" data-toggle="tooltip" data-placement="right" title="Click to learn more..."></i>
                                                                            </li>
                                                                            <li class="navi-separator mt-3 opacity-70"></li>
                                                                            <li class="navi-item">
                                                                                <a id="aDelete" onserverclick="BtnConfirm_OnClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-more-v4"></i></span>
                                                                                    Delete
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aMoveRight" onserverclick="BtnConfirm_OnClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-more-v4"></i>
                                                                                    </span>
                                                                                    Move next
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aEditOpportunity" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-edit-1"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">
                                                                                        <asp:Label ID="LblEditOpportunity" Text="View/Edit" CssClass="" runat="server" />
                                                                                    </span>
                                                                                </a>
                                                                            </li>
                                                                        </ul>
                                                                        <!--end::Navigation-->
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!--end::Toolbar-->
                                                        </div>
                                                    </div>
                                                </main>
                                                <footer></footer>
                                                <asp:HiddenField ID="HdnOpportunityId" Value='<%# DataBinder.Eval(Container.DataItem, "opportId")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcMessage1" runat="server" />
                                    </div>
                                    <div data-id="_working" data-order="2" class="kanban-board" style="width: 262px; margin-left: 0px; margin-right: 0px;">
                                        <header class="kanban-board-header primary">
                                            <div class="kanban-title-board">
                                                <div class="mt-step-title">
                                                    <a id="EditStatusTwo" style="float: left;" onserverclick="EditStatusTwo_OnClick" runat="server">
                                                        <i id="iEditStatusTwo" class="flaticon-edit" title="Change opportunity status description" runat="server"></i>
                                                    </a>
                                                    <a id="DeleteStatusTwo" style="float: left;" onserverclick="DeleteStatusTwo_OnCick" visible="false" runat="server">
                                                        <i id="iDeleteStatusTwo" class="flaticon-delete" title="Delete custom and use default" runat="server"></i>
                                                    </a>
                                                    <div style="float: left; margin-left: 5px;">
                                                        <asp:Label ID="LblStatusTwo" Text="Meeting" runat="server" />
                                                    </div>
                                                    <div style="float: left; margin-left: 5px; margin-top: -7px;">
                                                        <asp:TextBox ID="TbxStatusTwo" Visible="false" CssClass="form-control" Width="95" runat="server" />
                                                    </div>
                                                    <span style="margin-left: 15px;">
                                                        <asp:Label ID="LblAllMeeting" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                        </header>
                                        <div class="row ml-2 mt-2">
                                            <asp:DropDownList ID="DdlMeetingSort" CssClass="form-control" Width="195" runat="server">
                                                <asp:ListItem Value="0" Text="Order by date" Selected="True" />
                                                <asp:ListItem Value="1" Text="Order by title" />
                                                <asp:ListItem Value="2" Text="Order by notes" />
                                                <asp:ListItem Value="3" Text="Order by tasks" />
                                            </asp:DropDownList>
                                            <asp:Button ID="BtnMeetingSort" OnClick="BtntMeetingSort_OnClick" CssClass="btn btn-light-primary" runat="server" Text="Sort" />
                                        </div>
                                        <asp:Repeater ID="RptMeeting" OnLoad="RptMeeting_Load" OnItemDataBound="RptMeeting_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <main class="kanban-drag" style="min-height: 0px;">
                                                    <div class="kanban-item">
                                                        <div class="card-body pt-4" style="padding: 1.5rem;">
                                                            <!--begin::User-->
                                                            <div class="d-flex align-items-end mb-7">
                                                                <!--begin::Pic-->
                                                                <div class="d-flex align-items-center">
                                                                    <!--begin::Symbol-->
                                                                    <div id="divImg" runat="server" class="symbol symbol-light-primary mr-3">
                                                                        <span class="symbol-label">
                                                                            <asp:Image ID="img" runat="server" Width="38" Height="38" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "companyLogo")%>' AlternateText="" />
                                                                        </span>
                                                                    </div>
                                                                    <div id="divImgNo" runat="server" visible="false" class="symbol symbol-light-primary mr-3 symbol-light-primary">
                                                                        <span class="symbol-label font-size-h3 font-weight-boldest">NO</span>
                                                                    </div>
                                                                    <!--end::Symbol-->
                                                                    <!--begin::Title-->
                                                                    <div class="d-flex flex-column align-items-start">
                                                                        <span class="text-dark-50 font-weight-bold mb-1">
                                                                            <asp:Label ID="LblOrganName" Text='<%# DataBinder.Eval(Container.DataItem, "opportOrganName")%>' runat="server" />
                                                                        </span>
                                                                    </div>
                                                                    <!--end::Title-->
                                                                </div>
                                                                <!--end::Title-->
                                                            </div>
                                                            <!--end::User-->
                                                            <!--begin::Info-->
                                                            <div class="mb-10 mt-12">
                                                                <div class="d-flex justify-content-between align-items-center">
                                                                    <span class="label label-inline label font-weight-bold mr-2 mb-4" style="font-size: 13px;">Notes:</span>
                                                                    <a id="aNotes" runat="server" class="text-muted text-hover-light-danger">
                                                                        <asp:Label ID="LblNotes" CssClass="label label-lg label-primary label-inline mb-4" Text='<%# DataBinder.Eval(Container.DataItem, "opportNotes")%>' runat="server" />
                                                                    </a>
                                                                </div>
                                                                <div class="d-flex justify-content-between align-items-center">
                                                                    <span class="label label-inline label font-weight-bold mr-2" style="font-size: 13px;">Tasks:</span>
                                                                    <a id="aTasks" runat="server" class="text-muted text-hover-light-primary">
                                                                        <span class="text-muted font-weight-bold">
                                                                            <asp:Label ID="LblTasks" CssClass="label label-lg label-primary label-inline" Text='<%# DataBinder.Eval(Container.DataItem, "opportTasks")%>' runat="server" />
                                                                        </span>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                            <!--end::Info-->
                                                            <!--begin::Toolbar-->
                                                            <div class="d-flex justify-content-end">
                                                                <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Settings" data-placement="right">
                                                                    <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                        <i class="ki ki-bold-more-hor"></i>
                                                                    </a>
                                                                    <div class="dropdown-menu dropdown-menu-md dropdown-menu-left">
                                                                        <!--begin::Navigation-->
                                                                        <ul class="navi navi-hover">
                                                                            <li class="navi-header font-weight-bold py-4">
                                                                                <span class="font-size-lg">Actions</span>
                                                                                <i class="flaticon2-information icon-md text-muted" data-toggle="tooltip" data-placement="right" title="Click to learn more..."></i>
                                                                            </li>
                                                                            <li class="navi-separator mt-3 opacity-70"></li>
                                                                            <li class="navi-item">
                                                                                <a id="aMoveLeft" onserverclick="BtnConfirm_OnClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-more-v4"></i>
                                                                                    </span>
                                                                                    Move back
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aDelete" onserverclick="BtnConfirm_OnClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete"></i>
                                                                                    </span>Delete
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aMoveRight" onserverclick="BtnConfirm_OnClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-more-v4"></i>
                                                                                    </span>Move next
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aEditOpportunity" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-edit-1"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">
                                                                                        <asp:Label ID="LblEditOpportunity" Text="View/Edit" CssClass="" runat="server" />
                                                                                    </span>
                                                                                </a>
                                                                            </li>
                                                                        </ul>
                                                                        <!--end::Navigation-->
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!--end::Toolbar-->
                                                        </div>
                                                    </div>
                                                </main>
                                                <footer></footer>
                                                <asp:HiddenField ID="HdnOpportunityId" Value='<%# DataBinder.Eval(Container.DataItem, "opportId")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcMessage2" runat="server" />
                                    </div>
                                    <div data-id="_done" data-order="3" class="kanban-board" style="width: 262px; margin-left: 0px; margin-right: 0px;">
                                        <header class="kanban-board-header primary">
                                            <div class="kanban-title-board">
                                                <div class="mt-step-title">
                                                    <a id="EditStatusThree" style="float: left;" onserverclick="EditStatusThree_OnClick" runat="server">
                                                        <i id="iEditStatusThree" class="flaticon-edit" title="Change opportunity status description" runat="server"></i>
                                                    </a>
                                                    <a id="DeleteStatusThree" style="float: left;" onserverclick="DeleteStatusThree_OnCick" visible="false" runat="server">
                                                        <i id="iDeleteStatusThree" class="flaticon-delete" title="Delete custom and use default" runat="server"></i>
                                                    </a>
                                                    <div style="float: left; margin-left: 5px;">
                                                        <asp:Label ID="LblStatusThree" Text="Proposal" runat="server" />
                                                    </div>
                                                    <div style="float: left; margin-left: 5px; margin-top: -7px;">
                                                        <asp:TextBox ID="TbxStatusThree" Visible="false" CssClass="form-control" Width="95" runat="server" />
                                                    </div>
                                                    <span style="margin-left: 15px;">
                                                        <asp:Label ID="LblAllProposal" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                        </header>
                                        <div class="row ml-2 mt-2">
                                            <asp:DropDownList ID="DdlProposalSort" CssClass="form-control" Width="195" runat="server">
                                                <asp:ListItem Value="0" Text="Order by date" Selected="True" />
                                                <asp:ListItem Value="1" Text="Order by title" />
                                                <asp:ListItem Value="2" Text="Order by notes" />
                                                <asp:ListItem Value="3" Text="Order by tasks" />
                                            </asp:DropDownList>
                                            <asp:Button ID="BtnProposalSort" OnClick="BtntProposalSort_OnClick" CssClass="btn btn-light-primary" runat="server" Text="Sort" />
                                        </div>
                                        <asp:Repeater ID="RptProposal" OnLoad="RptProposal_Load" OnItemDataBound="RptProposal_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <main class="kanban-drag" style="min-height: 0px;">
                                                    <div class="kanban-item">
                                                        <div class="card-body pt-4" style="padding: 1.5rem;">
                                                            <!--begin::User-->
                                                            <div class="d-flex align-items-end mb-7">
                                                                <!--begin::Pic-->
                                                                <div class="d-flex align-items-center">
                                                                    <!--begin::Symbol-->
                                                                    <div id="divImg" runat="server" class="symbol symbol-light-primary mr-3">
                                                                        <span class="symbol-label">
                                                                            <asp:Image ID="img" runat="server" Width="38" Height="38" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "companyLogo")%>' AlternateText="" />
                                                                        </span>
                                                                    </div>
                                                                    <div id="divImgNo" runat="server" visible="false" class="symbol symbol-light-primary mr-3 symbol-light-success">
                                                                        <span class="symbol-label font-size-h3 font-weight-boldest">NO</span>
                                                                    </div>
                                                                    <!--end::Symbol-->
                                                                    <!--begin::Title-->
                                                                    <div class="d-flex flex-column align-items-start">
                                                                        <span class="text-dark-50 font-weight-bold mb-1">
                                                                            <asp:Label ID="LblOrganName" Text='<%# DataBinder.Eval(Container.DataItem, "opportOrganName")%>' runat="server" />
                                                                        </span>
                                                                    </div>
                                                                    <!--end::Title-->
                                                                </div>
                                                                <!--end::Title-->
                                                            </div>
                                                            <!--end::User-->
                                                            <!--begin::Info-->
                                                            <div class="mb-10 mt-12">
                                                                <div class="d-flex justify-content-between align-items-center">
                                                                    <span class="label label-inline label font-weight-bold mr-2 mb-4" style="font-size: 13px;">Notes:</span>
                                                                    <a id="aNotes" runat="server" class="text-muted text-hover-light-primary">
                                                                        <asp:Label ID="LblNotes" CssClass="label label-lg label-primary label-inline mb-4" Text='<%# DataBinder.Eval(Container.DataItem, "opportNotes")%>' runat="server" />
                                                                    </a>
                                                                </div>
                                                                <div class="d-flex justify-content-between align-items-center">
                                                                    <span class="label label-inline label font-weight-bold mr-2" style="font-size: 13px;">Tasks:</span>
                                                                    <a id="aTasks" runat="server" class="text-muted text-hover-light-primary">
                                                                        <span class="text-muted font-weight-bold">
                                                                            <asp:Label ID="LblTasks" CssClass="label label-lg label-primary label-inline" Text='<%# DataBinder.Eval(Container.DataItem, "opportTasks")%>' runat="server" />
                                                                        </span>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                            <!--end::Info-->
                                                            <!--begin::Toolbar-->
                                                            <div class="d-flex justify-content-end">
                                                                <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Settings" data-placement="right">
                                                                    <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                        <i class="ki ki-bold-more-hor"></i>
                                                                    </a>
                                                                    <div class="dropdown-menu dropdown-menu-md dropdown-menu-left">
                                                                        <!--begin::Navigation-->
                                                                        <ul class="navi navi-hover">
                                                                            <li class="navi-header font-weight-bold py-4">
                                                                                <span class="font-size-lg">Actions</span>
                                                                                <i class="flaticon2-information icon-md text-muted" data-toggle="tooltip" data-placement="right" title="Click to learn more..."></i>
                                                                            </li>
                                                                            <li class="navi-separator mt-3 opacity-70"></li>
                                                                            <li class="navi-item">
                                                                                <a id="aMoveLeft" onserverclick="BtnConfirm_OnClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-more-v4"></i>
                                                                                    </span>Move back
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aDelete" onserverclick="BtnConfirm_OnClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete"></i>
                                                                                    </span>Delete
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aMoveRight" onserverclick="BtnConfirm_OnClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-more-v4"></i>
                                                                                    </span>Move next
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aEditOpportunity" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-edit-1"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">
                                                                                        <asp:Label ID="LblEditOpportunity" Text="View/Edit" CssClass="" runat="server" />
                                                                                    </span>
                                                                                </a>
                                                                            </li>
                                                                        </ul>
                                                                        <!--end::Navigation-->
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!--end::Toolbar-->
                                                        </div>
                                                    </div>
                                                </main>
                                                <footer></footer>
                                                <asp:HiddenField ID="HdnOpportunityId" Value='<%# DataBinder.Eval(Container.DataItem, "opportId")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcMessage3" runat="server" />
                                    </div>
                                    <div data-id="_done4" data-order="4" class="kanban-board" style="width: 262px; margin-left: 0px; margin-right: 0px;">
                                        <header class="kanban-board-header primary">
                                            <div class="kanban-title-board">
                                                <div class="mt-step-title">
                                                    <a id="EditStatusFour" style="float: left;" onserverclick="EditStatusFour_OnClick" runat="server">
                                                        <i id="iEditStatusFour" class="flaticon-edit" title="Change opportunity status description" runat="server"></i>
                                                    </a>
                                                    <a id="DeleteStatusFour" style="float: left;" onserverclick="DeleteStatusFour_OnCick" visible="false" runat="server">
                                                        <i id="iDeleteStatusFour" class="flaticon-delete" title="Delete custom and use default" runat="server"></i>
                                                    </a>
                                                    <div style="float: left; margin-left: 5px;">
                                                        <asp:Label ID="LblStatusFour" Text="Closed" runat="server" />
                                                    </div>
                                                    <div style="float: left; margin-left: 5px; margin-top: -7px;">
                                                        <asp:TextBox ID="TbxStatusFour" Visible="false" CssClass="form-control" Width="100" runat="server" />
                                                    </div>
                                                    <span style="margin-left: 15px;">
                                                        <asp:Label ID="LblAllClosed" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                        </header>
                                        <div class="row ml-2 mt-2">
                                            <asp:DropDownList ID="DdlClosedSort" CssClass="form-control" Width="195" runat="server">
                                                <asp:ListItem Value="0" Text="Order by date" Selected="True" />
                                                <asp:ListItem Value="1" Text="Order by title" />
                                                <asp:ListItem Value="2" Text="Order by notes" />
                                                <asp:ListItem Value="3" Text="Order by tasks" />
                                            </asp:DropDownList>
                                            <asp:Button ID="BtnClosedSort" OnClick="BtntClosedSort_OnClick" CssClass="btn btn-light-primary" runat="server" Text="Sort" />
                                        </div>
                                        <div class="row ml-5 mt-10">
                                            <asp:Label ID="LblClosedSubStatusText" runat="server" />
                                            <asp:DropDownList AutoPostBack="true" Width="227" ID="DdlClosedSubStatusSort" CssClass="form-control" OnSelectedIndexChanged="DdlClosedSubStatusSort_OnSelectedIndexChanged" runat="server" />
                                            <asp:Button ID="BtnClosedSubStatusSort" Visible="false" OnClick="BtnClosedSubStatusSort_OnClick" CssClass="btn btn-success" runat="server" Text="Sort" />
                                        </div>
                                        <asp:Repeater ID="RptClosed" OnLoad="RptClosed_Load" OnItemDataBound="RptClosed_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <main class="kanban-drag" style="min-height: 0px;">
                                                    <div class="kanban-item">
                                                        <div class="card-body pt-4" style="padding: 1.5rem;">
                                                            <!--begin::User-->
                                                            <div class="d-flex align-items-end mb-7">
                                                                <!--begin::Pic-->
                                                                <div class="d-flex align-items-center">
                                                                    <!--begin::Symbol-->
                                                                    <div id="divImg" runat="server" class="symbol symbol-light-primary mr-3">
                                                                        <span class="symbol-label">
                                                                            <asp:Image ID="img" runat="server" Width="38" Height="38" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "companyLogo")%>' AlternateText="" />
                                                                        </span>
                                                                    </div>
                                                                    <div id="divImgNo" runat="server" visible="false" class="symbol symbol-light-primary mr-3 symbol-light-primary">
                                                                        <span class="symbol-label font-size-h3 font-weight-boldest">NO</span>
                                                                    </div>
                                                                    <!--end::Symbol-->
                                                                    <!--begin::Title-->
                                                                    <div class="d-flex flex-column align-items-start">
                                                                        <span class="text-dark-50 font-weight-bold mb-1">
                                                                            <asp:Label ID="LblOrganName" Text='<%# DataBinder.Eval(Container.DataItem, "opportOrganName")%>' runat="server" />
                                                                        </span>
                                                                    </div>
                                                                    <!--end::Title-->
                                                                </div>
                                                                <!--end::Title-->
                                                            </div>
                                                            <!--end::User-->
                                                            <!--begin::Info-->
                                                            <div class="mb-10 mt-12">
                                                                <div class="d-flex justify-content-between align-items-center">
                                                                    <span class="label label-inline label font-weight-bold mr-2 mb-4" style="font-size: 13px;">Notes:</span>
                                                                    <a id="aNotes" runat="server" class="text-muted text-hover-light-primary">
                                                                        <asp:Label ID="LblNotes" CssClass="label label-lg label-primary label-inline mb-4" Text='<%# DataBinder.Eval(Container.DataItem, "opportNotes")%>' runat="server" />
                                                                    </a>
                                                                </div>
                                                                <div class="d-flex justify-content-between align-items-center">
                                                                    <span class="label label-inline label font-weight-bold mr-2" style="font-size: 13px;">Tasks:</span>
                                                                    <a id="aTasks" runat="server" class="text-muted text-hover-light-primary">
                                                                        <span class="text-muted font-weight-bold">
                                                                            <asp:Label ID="LblTasks" CssClass="label label-lg label-primary label-inline" Text='<%# DataBinder.Eval(Container.DataItem, "opportTasks")%>' runat="server" />
                                                                        </span>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                            <div class="mb-5 mt-5">
                                                                <div class="row d-flex justify-content-between align-items-center">
                                                                    <span class="text-dark-75 font-weight-bolder mr-2 mb-4">
                                                                        <asp:Label ID="LblSubStatus" runat="server" />
                                                                    </span>
                                                                </div>
                                                                <div class="row d-flex justify-content-between align-items-center">
                                                                    <asp:DropDownList ID="RcbxSubStatus" Width="110" CssClass="form-control" runat="server" />
                                                                    <asp:ImageButton ID="ImgBtnSaveSub" OnClick="ImgBtnSaveSub_OnClick" runat="server" />
                                                                </div>
                                                            </div>
                                                            <!--end::Info-->
                                                            <!--begin::Toolbar-->
                                                            <div class="d-flex justify-content-end">
                                                                <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Settings" data-placement="right">
                                                                    <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                        <i class="ki ki-bold-more-hor"></i>
                                                                    </a>
                                                                    <div class="dropdown-menu dropdown-menu-md dropdown-menu-left">
                                                                        <!--begin::Navigation-->
                                                                        <ul class="navi navi-hover">
                                                                            <li class="navi-header font-weight-bold py-4">
                                                                                <span class="font-size-lg">Actions</span>
                                                                                <i class="flaticon2-information icon-md text-muted" data-toggle="tooltip" data-placement="right" title="Click to learn more..."></i>
                                                                            </li>
                                                                            <li class="navi-separator mt-3 opacity-70"></li>
                                                                            <li class="navi-item">
                                                                                <a id="aDelete" onserverclick="BtnConfirm_OnClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-delete"></i>
                                                                                    </span>Delete
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aMoveLeft" onserverclick="BtnConfirm_OnClick" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-more-v4"></i>
                                                                                    </span>Move back
                                                                                </a>
                                                                            </li>
                                                                            <li class="navi-item">
                                                                                <a id="aEditOpportunity" runat="server" class="navi-link">
                                                                                    <span class="navi-icon">
                                                                                        <i class="flaticon-edit-1"></i>
                                                                                    </span>
                                                                                    <span class="navi-text">
                                                                                        <asp:Label ID="LblEditOpportunity" Text="View/Edit" CssClass="" runat="server" />
                                                                                    </span>
                                                                                </a>
                                                                            </li>
                                                                        </ul>
                                                                        <!--end::Navigation-->
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!--end::Toolbar-->
                                                        </div>
                                                    </div>
                                                </main>
                                                <footer></footer>
                                                <asp:HiddenField ID="HdnOpportunityId" Value='<%# DataBinder.Eval(Container.DataItem, "opportId")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcMessage4" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end::Card-->

                    <!--end::Dashboard-->
                </div>
                <!--end::Container-->
            </div>
            <!--end::Entry-->
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div class="modal-dialog">
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
                                <div class="form-group">
                                    <asp:Label ID="LblConfMsg" Text="Delete this opportunity permanently?" runat="server" />                                    
                                    <asp:Label ID="TbxOpportAction" Text="" Visible="false" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-light-primary">Back</button>
                            <asp:Button ID="BtnBack" Text="Back" Visible="false" OnClick="BtnBack_OnClick" CssClass="btn btn-light-danger" runat="server" />
                            <asp:Button ID="BtnSave" OnClick="BtnSave_OnClick" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">

            function CloseConfirmPopUp() {                
                $('#divConfirm').modal('hide');
            }

            function OpenConfirmPopUp() {
                $('#divConfirm').modal('show');
            }
        </script>

        <!--begin::Page Scripts(used by this page)-->
        <script src="/assets/plugins/custom/kanban/kanban.bundle.js"></script>
        <script src="/assets/js/pages/features/miscellaneous/kanban-board.js"></script>
        <!--end::Page Scripts-->

    </telerik:RadScriptBlock>

</asp:Content>
