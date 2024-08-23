<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardDealRegistration.aspx.cs" Inherits="WdS.ElioPlus.DashboardDealRegistration" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAInfoMessageControl.ascx" TagName="InfoMessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>

    <!-- BEGIN PAGE BAR -->
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <span><asp:Label ID="LblDashboard" runat="server" /></span>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <span><asp:Label ID="LblDashPage" runat="server" /></span>
            </li>
        </ul>
        <div class="page-toolbar" id="divPgToolbar" runat="server">
            <div class="clearfix">
                <asp:Label ID="LblPricingPlan" runat="server" />
                <span style="margin-left:10px;">
                    <a id="aBtnGoPremium" runat="server" href="#PaymentModal" role="button" data-toggle="modal" class="btn btn-circle green-light btn-md" visible="false">
                        <asp:Label ID="LblBtnGoPremium" runat="server" />
                    </a>
                    <a id="aBtnGoFull" runat="server" class="btn btn-circle green-light btn-md" visible="false">
                        <asp:Label ID="LblGoFull" runat="server" />
                    </a>
                </span>
                <br /><asp:Label ID="LblRenewalHead" runat="server" Visible="false" /><asp:Label ID="LblRenewal" Visible="false" runat="server" />
            </div>
        </div>
    </div>    
    <!-- END PAGE BAR -->
    <!-- BEGIN PAGE TITLE-->
    <h3 class="page-title"><asp:Label ID="LblElioplusDashboard" runat="server" />
        <small><asp:Label ID="LblDashSubTitle" runat="server" /></small>
    </h3>
    <!-- END PAGE TITLE--> 
    <div class="row">
        <div class="col-md-6" style="float:right;">
            <div class="">
                <controls:InfoMessageControl ID="InfoMessageControl" runat="server" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div style="padding: 10px;" class="form-group">                
                <div id="divDealRegistrationInvitationToPartners" style="" class="note note-success" runat="server">                                        
                    <p>
                        <asp:Label ID="LblDealRegistrationTitle" runat="server" />
                    </p> 
                    <div id="divInvitationToPartners" visible="false" style="padding:10px 0 0px 0;" runat="server">
                        <a id="aInvitationToPartners" runat="server" style="" class="btn btn-primary">
                            <i class="fa fa-user"></i>
                            <asp:Label ID="LblInvitationToPartners" Text="Invite partners" runat="server" />
                        </a>                       
                    </div>
                </div>
            </div>
            <div class="portlet-body form">
                <form action="#" class="horizontal-form">          
                    <div class="form-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
                            <ContentTemplate>                                
                                <div id="divVendorsList" visible="false" style="height:70px; display:inline-block;text-align:center;width:100%;" runat="server">
                                    <div class="col-md-6">                                            
                                        <div class="form-group">
                                            <asp:Label ID="LblOppStatus" CssClass="control-label" runat="server" />                                                    
                                            <telerik:RadDropDownList AutoPostBack="true" ID="DrpPartners" Width="400" OnSelectedIndexChanged="DrpPartners_SelectedIndexChanged" runat="server">
                                            </telerik:RadDropDownList>                                            
                                        </div>
                                        <asp:Label ID="LblSelectPlan" runat="server" />
                                        <asp:Label ID="LblStatusHelp" CssClass="help-block" runat="server" />
                                    </div>
                                    <div id="divVendorSettings" visible="false" style="height:70px; display:inline-block;text-align:center;" runat="server" class="col-md-6">                                            
                                        <div class="form-group" style=" height:36px;">
                                            <asp:Label ID="LblVendorDurationSetting" Visible="false" CssClass="control-label" runat="server" />
                                            <div class="input-group" style="float:left;">
                                                <asp:TextBox ID="TbxVendorDurationSetting"  Width="400" placeholder="select default deal month duration" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                                                <telerik:RadDropDownList ID="DrpVendorDurationSetting" Width="400" Visible="false" runat="server">
                                                </telerik:RadDropDownList>
                                                <span id="spanInputGroup" runat="server" class="input-group-btn" style="float:left;">
                                                    <asp:Button ID="BtnVendorDurationSetting" OnClick="BtnVendorDurationSetting_Click" Text="Edit" CssClass="btn btn-success" type="button" runat="server" />
                                                    <asp:Button ID="BtnCancelVendorDurationSetting" Visible="false" OnClick="BtnCancelVendorDurationSetting_Click" Text="Cancel" CssClass="btn btn-red" type="button" runat="server" />
                                                </span>
                                            </div>                                            
                                        </div>
                                        <asp:Label ID="LblVendorDurationSettingHelp" CssClass="help-block" runat="server" />
                                    </div>
                                    <div class="col-md-6">                                            
                                        <div class="form-group">
                                            <div class="btn-group btn-group btn-group btn-group-justified" style="text-align:center;">
                                                <asp:Button ID="BtnAddNewDeal" OnClick="BtnAddNewDeal_Click" Text="Add New Deal" Visible="false" Width="200" CssClass="btn btn-circle green-light btn-md" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="divDealSuccessBottom" runat="server" visible="false" class="col-md-8 alert alert-success" style="text-align:center; margin-bottom:0px !important;width:100%;">
                                    <strong>
                                        <asp:Label ID="LblDealSuccBottom" Text="Done! " runat="server" />
                                    </strong>
                                    <asp:Label ID="LblDealSuccContBottom" runat="server" />
                                </div>
                                <div id="divDealErrorBottom" runat="server" visible="false" class="col-md-8 alert alert-warning" style="text-align:center;margin-bottom:0px !important; width:100%;">
                                    <strong>
                                        <asp:Label ID="LblDealErrorBottom" Text="Done! " runat="server" />
                                    </strong>
                                    <asp:Label ID="LblDealErrorContBottom" runat="server" />
                                </div>

                                <div style="margin-top:61px;"></div>
                                   
                                <div id="divDealsResults" runat="server" style="height:100% !important;">
                                    <div class="tabbable-full-width" style="height:100% !important;">
                                        <ul class="nav nav-tabs">
                                            <li id="liPendingDeals" runat="server" class="active">
                                                <a id="aPendingDeals" runat="server" onserverclick="aPendingDeals_ServerClick"><asp:Label ID="LblPendingDeals" Text="New Deals" runat="server" /></a>
                                            </li>                                                
                                            <li id="liOpenDeals" runat="server">
                                                <a id="aOpenDeals" runat="server" onserverclick="aOpenDeals_ServerClick"><asp:Label ID="LblOpenDeals" Text="Open Deals" runat="server" /></a>
                                            </li>
                                            <li id="liClosedDeals" runat="server">
                                                <a id="aClosedDeals" runat="server" onserverclick="aClosedDeals_ServerClick"><asp:Label ID="LblClosedDeals" Text="Past Deals" runat="server" /></a>
                                            </li>
                                        </ul>
                                        <div class="tab-content" style="height:100% !important;">
                                            <!--tab_1_1-->
                                            <div class="tab-pane active" id="tab_1_1" runat="server">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h3>
                                                            <asp:Label ID="LblPendingDealsTitle" Text="List of pending deals" runat="server" />
                                                        </h3>
                                                        <telerik:RadGrid ID="RdgDealsPending" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" HeaderStyle-ForeColor="#e7505a" HeaderStyle-Font-Bold="true" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="false" PagerStyle-Position="Bottom" CssClass="table table-bordered table-striped table-condensed" PageSize="10" Width="100%" OnItemDataBound="RdgDealsPending_OnItemDataBound" OnNeedDataSource="RdgDealsPending_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                                            <MasterTableView CssClass="table-status">                                                                        
                                                                <Columns>
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Vendor Reseller Id" DataField="collaboration_vendor_reseller_id" UniqueName="collaboration_vendor_reseller_id" />
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="vendor_id" UniqueName="vendor_id" />
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="reseller_id" UniqueName="reseller_id" />
                                                                    <telerik:GridTemplateColumn HeaderText="Partner Name" DataField="partner_name" UniqueName="partner_name">
                                                                        <ItemTemplate>
                                                                            <div class="media-left">
                                                                                <a id="aCompanyName" runat="server" style="text-decoration:underline !important; cursor:pointer;">
                                                                                    <asp:Label ID="LblCompanyNameContent" Font-Size="10" ForeColor="Blue" runat="server" />
                                                                                </a>
                                                                            </div>
                                                                            <div id="divNotification" runat="server" visible="false" class="media-right">
                                                                                <span id="spanNotificationMsg" class="badge bg-red" style="background-color:#ed7177;" title="New pending deal" runat="server">
                                                                                    <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                                </span>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Partner Location" DataField="partner_location" UniqueName="partner_location" />
                                                                    <telerik:GridTemplateColumn Visible="false" HeaderText="Partner Details" UniqueName="company_more_details">
                                                                        <ItemTemplate>
                                                                            <a id="aMoreDetails" visible="false" style="text-decoration:underline !important;" runat="server">
                                                                                <asp:Label ID="LblMoreDetails" Font-Size="10" ForeColor="Blue" runat="server" />
                                                                            </a>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Client" DataField="company_name" UniqueName="company_name" />
                                                                    <telerik:GridTemplateColumn HeaderText="Client">
                                                                        <ItemTemplate>
                                                                            <div class="media-left">
                                                                                <asp:Label ID="LblClientName" runat="server" />
                                                                            </div>
                                                                            <div id="divClientNotification" runat="server" visible="false" class="media-right">
                                                                                <span id="spanClientNotificationMsg" class="badge bg-red" style="background-color:#ed7177;" title="New open deal" runat="server">
                                                                                    <asp:Label ID="LblClientNotificationMsg" Text="new!" runat="server" />
                                                                                </span>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Client Email" DataField="email" UniqueName="email" />
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Client Website" DataField="website" UniqueName="website" />
                                                                    <telerik:GridTemplateColumn HeaderText="Client Website">
                                                                        <ItemTemplate>
                                                                            <a id="aWebsite" runat="server" style="text-decoration:underline !important; cursor:pointer;">
                                                                                <asp:Label ID="LblWebsite" Font-Size="10" ForeColor="Blue" runat="server" />
                                                                            </a>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Added On" DataField="sysdate" UniqueName="sysdate" />
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Status" DataField="status" UniqueName="status" />
                                                                    <telerik:GridBoundColumn Display="false" DataField="deal_result" UniqueName="deal_result" />
                                                                    <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                                    <telerik:GridTemplateColumn Display="false" HeaderStyle-Width="80" HeaderText="Deal Result">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblResultStatus" style="width:80px !important;" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn Display="false" DataField="is_active" UniqueName="is_active" />
                                                                    <telerik:GridTemplateColumn Display="false" HeaderStyle-Width="10" HeaderText="Deal">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblActiveStatus" style="width:115px !important;" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Actions" HeaderStyle-Width="60" HeaderStyle-Font-Size="12" DataField="actions" UniqueName="actions">
                                                                        <ItemTemplate>
                                                                            <div class="dropdown">
                                                                                <li id="liUserOptions" runat="server" class="btn btn-sm btn-clean btn-icon btn-icon-md">
                                                                                    <a class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                                                                        <asp:Image ID="ImgUserPhoto" CssClass="img-circle" ImageUrl="~/images/icons/settings-vert.png" runat="server" />  
                                                                                        <span class="username username-hide-on-mobile"></span>
                                                                                    </a>
                                                                                    <ul class="dropdown-menu dropdown-menu-default" style="background-color:#fff !important; min-width:120px !important; text-align: center;">
                                                                                        <li>
                                                                                            <a id="aEdit" runat="server">
                                                                                                <i class="fa fa-edit"></i>
                                                                                                <asp:Label ID="LblEdit" ToolTip="Preview/Edit deal" Text="View" runat="server" />
                                                                                            </a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a id="aDeleteTask" visible="false" onserverclick="BtnDelete_OnClick" runat="server">
                                                                                                <i class="icon-close font-lg font-green-soft"></i>
                                                                                            </a>
                                                                                        </li>
                                                                                    </ul>
                                                                                </li>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>

                                                        <div id="divPendingNoResults" runat="server" visible="false" class="col-md-12 alert alert-warning" style="text-align:center; margin-top:0px; margin-bottom:40px;">
                                                            <strong>
                                                                <asp:Label ID="LblPendingNoResultsTitle" Text="0 Deals! " runat="server" />
                                                            </strong>
                                                            <asp:Label ID="LblPendingNoResultsContent" runat="server" />                                        
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!--tab_1_2-->
                                            <div class="tab-pane" id="tab_1_2" runat="server">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div id="divSearchOpen" runat="server" visible="false" style="padding:0;" class="form-group">
                                                            <div style="padding-bottom:5px;">
                                                                <asp:Label ID="LblSearchTitleOpen" Text="Advanced search" runat="server" />
                                                            </div>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 260px;">
                                                                        <telerik:RadTextBox ID="RtbxCompanyNameOpen" EmptyMessage="find company by name or email" Width="250" Skin="MetroTouch" runat="server" />
                                                                    </td>
                                                                    <td style="width: 160px;">
                                                                        <telerik:RadDropDownList ID="DdlDealResultOpen" Width="150" runat="server">
                                                                        </telerik:RadDropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="BtnSearchOpen" OnClick="BtnSearchOpen_Click" runat="server" CssClass="btn btn-cta-primary" Text="Search" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <h3>
                                                            <asp:Label ID="LblOpenDealsTitle" Text="List of open(approved) deals" runat="server" />
                                                        </h3>
                                                        <telerik:RadGrid ID="RdgDealsOpen" Style="margin: auto; position: relative;" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" HeaderStyle-ForeColor="#e7505a" HeaderStyle-Font-Bold="true" AllowPaging="true" AllowSorting="false" PagerStyle-Position="Bottom" CssClass="table table-bordered table-striped table-condensed" PageSize="10" Width="100%" OnItemDataBound="RdgDealsOpen_OnItemDataBound" OnNeedDataSource="RdgDealsOpen_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                                            <MasterTableView CssClass="table-status">                                                                        
                                                                <Columns>
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Vendor Reseller Id" DataField="collaboration_vendor_reseller_id" UniqueName="collaboration_vendor_reseller_id" />
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="vendor_id" UniqueName="vendor_id" />
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="reseller_id" UniqueName="reseller_id" />
                                                                    <telerik:GridTemplateColumn HeaderText="Partner Name" DataField="partner_name" UniqueName="partner_name">
                                                                        <ItemTemplate>
                                                                            <div class="media-left">
                                                                                <a id="aCompanyName" runat="server" style="text-decoration:underline !important; cursor:pointer;">
                                                                                    <asp:Label ID="LblCompanyNameContent" Font-Size="10" ForeColor="Blue" runat="server" />
                                                                                </a>
                                                                            </div>
                                                                            <div id="divNotification" runat="server" visible="false" class="media-right">
                                                                                <span id="spanNotificationMsg" class="badge bg-red" style="background-color:#ed7177;" title="New open deal" runat="server">
                                                                                    <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                                </span>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Partner Location" DataField="partner_location" UniqueName="partner_location" />
                                                                    <telerik:GridTemplateColumn Visible="false" HeaderText="Partner Details" UniqueName="company_more_details">
                                                                        <ItemTemplate>
                                                                            <a id="aMoreDetails" visible="false" style="text-decoration:underline !important;" runat="server">
                                                                                <asp:Label ID="LblMoreDetails" Font-Size="10" ForeColor="Blue" runat="server" />
                                                                            </a>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Client" DataField="company_name" UniqueName="company_name" />
                                                                    <telerik:GridTemplateColumn HeaderText="Client">
                                                                        <ItemTemplate>
                                                                            <div class="media-left">
                                                                                <asp:Label ID="LblClientName" runat="server" />
                                                                            </div>
                                                                            <div id="divClientNotification" runat="server" visible="false" class="media-right">
                                                                                <span id="spanClientNotificationMsg" class="badge bg-red" style="background-color:#ed7177;" title="New open deal" runat="server">
                                                                                    <asp:Label ID="LblClientNotificationMsg" Text="new!" runat="server" />
                                                                                </span>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Client Email" DataField="email" UniqueName="email" />                                                
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Client Website" DataField="website" UniqueName="website" />
                                                                    <telerik:GridTemplateColumn HeaderText="Client Website">
                                                                        <ItemTemplate>
                                                                            <a id="aWebsite" runat="server" style="text-decoration:underline !important; cursor:pointer;">
                                                                                <asp:Label ID="LblWebsite" Font-Size="10" ForeColor="Blue" runat="server" />
                                                                            </a>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Expiring Date" DataField="month_duration" UniqueName="month_duration" />                                              
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Status" DataField="status" UniqueName="status" />
                                                                    <telerik:GridBoundColumn Display="false" DataField="deal_result" UniqueName="deal_result" />
                                                                    <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                                    <telerik:GridTemplateColumn HeaderStyle-Width="80" HeaderText="Deal Result">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblResultStatus" style="width:80px !important;" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn Display="false" DataField="is_active" UniqueName="is_active" />
                                                                    <telerik:GridTemplateColumn Display="false" HeaderStyle-Width="10" HeaderText="Deal">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblActiveStatus" style="width:115px !important;" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Actions" HeaderStyle-Width="60" HeaderStyle-Font-Size="12" DataField="actions" UniqueName="actions">
                                                                        <ItemTemplate>
                                                                            <div class="dropdown">
                                                                                <li id="liUserOptions" runat="server" class="btn btn-sm btn-clean btn-icon btn-icon-md">
                                                                                    <a class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                                                                        <asp:Image ID="ImgUserPhoto" CssClass="img-circle" ImageUrl="~/images/icons/settings-vert.png" runat="server" />  
                                                                                        <span class="username username-hide-on-mobile"></span>
                                                                                    </a>
                                                                                    <ul class="dropdown-menu dropdown-menu-default" style="background-color:#fff !important; min-width:120px !important; text-align: center;">
                                                                                        <li>
                                                                                            <a id="aEdit" runat="server">
                                                                                                <i class="fa fa-edit"></i>
                                                                                                <asp:Label ID="LblEdit" ToolTip="Preview/Edit deal" Text="View" runat="server" />
                                                                                            </a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a id="aDeleteTask" visible="false" onserverclick="BtnDelete_OnClick" runat="server">
                                                                                                <i class="icon-close font-lg font-green-soft"></i>
                                                                                            </a>
                                                                                        </li>
                                                                                    </ul>
                                                                                </li>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>    
                                                                                                                
                                                        <div id="divOpenNoResults" runat="server" visible="false" class="col-md-12 alert alert-warning" style="text-align:center; margin-top:0px; margin-bottom:40px;">
                                                        <strong>
                                                            <asp:Label ID="LblOpenNoResultsTitle" Text="0 Deals! " runat="server" />
                                                        </strong>
                                                        <asp:Label ID="LblOpenNoResultsContent" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!--tab_1_3-->
                                            <div class="tab-pane" id="tab_1_3" runat="server">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div id="divPastDeals" runat="server" style="padding:0;" class="form-group">
                                                            <table>
                                                                <tr> 
                                                                    <td style="width: 260px;">
                                                                        <telerik:RadTextBox ID="RtbxCompanyNamePast" EmptyMessage="find by client name or email" Width="250" Skin="MetroTouch" runat="server" />
                                                                    </td>                                                                        
                                                                    <td style="width: 160px;">
                                                                        <telerik:RadDropDownList ID="DdlDealResultPast" Width="150" runat="server">
                                                                        </telerik:RadDropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="BtnSearchPast" OnClick="BtnSearchPast_Click" runat="server" CssClass="btn btn-cta-primary" Text="Search" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <h3>
                                                            <asp:Label ID="LblClosedDealsTitle" Text="List of past deals" runat="server" />
                                                        </h3>
                                                        <telerik:RadGrid ID="RdgPastDeals" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" HeaderStyle-ForeColor="#e7505a" HeaderStyle-Font-Bold="true" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="false" PagerStyle-Position="Bottom" CssClass="table table-bordered table-striped table-condensed" PageSize="10" Width="100%" OnItemDataBound="RdgPastDeals_OnItemDataBound" OnNeedDataSource="RdgPastDeals_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                                            <MasterTableView CssClass="table-status">                                                                        
                                                                <Columns>
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Vendor Reseller Id" DataField="collaboration_vendor_reseller_id" UniqueName="collaboration_vendor_reseller_id" />
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="vendor_id" UniqueName="vendor_id" />
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="reseller_id" UniqueName="reseller_id" />
                                                                    <telerik:GridTemplateColumn HeaderText="Partner Name" DataField="partner_name" UniqueName="partner_name">
                                                                        <ItemTemplate>
                                                                            <div class="media-left">
                                                                                <a id="aCompanyName" runat="server" style="text-decoration:underline !important; cursor:pointer;">
                                                                                    <asp:Label ID="LblCompanyNameContent" Font-Size="10" ForeColor="Blue" runat="server" />
                                                                                </a>
                                                                            </div>
                                                                            <div id="divNotification" runat="server" visible="false" class="media-right">
                                                                                <span id="spanNotificationMsg" class="badge bg-red" style="background-color:#ed7177;" title="New pending deal" runat="server">
                                                                                    <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                                </span>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Partner Location" DataField="partner_location" UniqueName="partner_location" />
                                                                    <telerik:GridTemplateColumn Visible="false" HeaderText="Partner Details" UniqueName="company_more_details">
                                                                        <ItemTemplate>
                                                                            <a id="aMoreDetails" visible="false" style="text-decoration:underline !important;" runat="server">
                                                                                <asp:Label ID="LblMoreDetails" Font-Size="10" ForeColor="Blue" runat="server" />
                                                                            </a>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Client" DataField="company_name" UniqueName="company_name" />
                                                                    <telerik:GridBoundColumn HeaderText="Client Email" DataField="email" UniqueName="email" />                                                
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Client Website" DataField="website" UniqueName="website" />
                                                                    <telerik:GridTemplateColumn HeaderText="Client Website">
                                                                        <ItemTemplate>
                                                                            <a id="aWebsite" runat="server" style="text-decoration:underline !important; cursor:pointer;">
                                                                                <asp:Label ID="LblWebsite" Font-Size="10" ForeColor="Blue" runat="server" />
                                                                            </a>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Expiring Date" DataField="month_duration" UniqueName="month_duration" />                                              
                                                                    <telerik:GridBoundColumn Display="false" HeaderText="Status" DataField="status" UniqueName="status" />
                                                                    <telerik:GridBoundColumn Display="false" DataField="deal_result" UniqueName="deal_result" />
                                                                    <telerik:GridTemplateColumn HeaderStyle-Width="80" HeaderText="Deal Result">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblResultStatus" style="width:80px !important;" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn Display="false" DataField="is_active" UniqueName="is_active" />
                                                                    <telerik:GridTemplateColumn HeaderStyle-Width="80" HeaderText="Deal">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblActiveStatus" style="width:115px !important;" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Actions" HeaderStyle-Width="60" HeaderStyle-Font-Size="12" DataField="actions" UniqueName="actions">
                                                                        <ItemTemplate>
                                                                            <div class="dropdown">
                                                                                <li id="liUserOptions" runat="server" class="btn btn-sm btn-clean btn-icon btn-icon-md">
                                                                                    <a class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                                                                        <asp:Image ID="ImgUserPhoto" CssClass="img-circle" ImageUrl="~/images/icons/settings-vert.png" runat="server" />  
                                                                                        <span class="username username-hide-on-mobile"></span>
                                                                                    </a>
                                                                                    <ul class="dropdown-menu dropdown-menu-default" style="background-color:#fff !important; min-width:120px !important; text-align: center;">
                                                                                        <li>
                                                                                            <a id="aEdit" runat="server">
                                                                                                <i class="fa fa-edit"></i>
                                                                                                <asp:Label ID="LblEdit" ToolTip="Preview/Edit deal" Text="View" runat="server" />
                                                                                            </a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a id="aDeleteTask" visible="false" onserverclick="BtnDelete_OnClick" runat="server">
                                                                                                <i class="icon-close font-lg font-green-soft"></i>
                                                                                            </a>
                                                                                        </li>
                                                                                    </ul>
                                                                                </li>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>

                                                        <div id="divClosedNoResults" runat="server" visible="false" class="col-md-12 alert alert-warning" style="text-align:center; margin-top:0px; margin-bottom:40px;">
                                                        <strong>
                                                            <asp:Label ID="LblClosedNoResultsTitle" Text="0 Deals! " runat="server" />
                                                        </strong>
                                                        <asp:Label ID="LblClosedNoResultsContent" runat="server" />
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
                </form>
            </div>   
        </div>        
    </div>
       
    <div id="loader" style="display:none;">
        <div id="loadermsg">
        </div>
    </div>
    
    <!-- Payment form (modal view) -->
    <div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <controls:UcStripe id="UcStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up Invitation form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title"><asp:Label ID="LblFileUploadTitle" CssClass="control-label" runat="server" /></h4>                            
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <controls:MessageControl ID="UploadMessageAlert" Visible="false" runat="server" />
                                        <asp:Label ID="LblFileUploadfMsg" Visible="false" CssClass="control-label" runat="server" />                                       
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
    
    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
        <style>           
            .RadGrid_MetroTouch .rgAltRow
            {
                background-color:transparent !important;
            }
            .RadGrid_MetroTouch .rgRow
            {
                background-color:transparent !important;
            }
        </style>

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
        </script>        
    </telerik:RadScriptBlock>

</asp:Content>
