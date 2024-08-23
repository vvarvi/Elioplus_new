<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardDealPartnerDirectoryInviteNew.aspx.cs" Inherits="WdS.ElioPlus.DashboardDealPartnerDirectoryInviteNew" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/CreateNewInvitationToPartnersMessage.ascx" TagName="UcCreateNewInvitationToPartnersMessage" TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- BEGIN PAGE BAR -->
    <%--<div class="page-bar">
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
                        
                </span>
                <br /><asp:Label ID="LblRenewalHead" runat="server" Visible="false" /><asp:Label ID="LblRenewal" Visible="false" runat="server" />
            </div>
        </div>
    </div>--%>
    <!-- END PAGE BAR -->
    <!-- BEGIN PAGE TITLE-->
    <!-- END PAGE TITLE-->
    <div class="main-container">
        <div id="page-container">
            <div id="page-content container-fluid p-0">
                <div class="main-content content-lg">
                    <div class="row">
                        <h3 class="page-title">
                            <asp:Label ID="LblElioplusDashboard" runat="server" />
                            <small>
                                <asp:Label ID="LblDashSubTitle" runat="server" /></small>
                        </h3>
                        <div class="row">                            
                            <div class="col-md-12">
                                <div style="padding: 10px;" class="form-group">
                                </div>
                                <a id="aCancelInvitation" runat="server" role="button" class="btn btn-outline btn-rounded btn-primary">
                                    <asp:Label ID="LblCancelInvitation" Text="Back to Partner Directory" runat="server" />
                                </a>
                                <div style="margin-bottom:20px;"></div>

                                <div class="widget-body" style="padding:0px !important;">                                    
                                    <asp:UpdatePanel runat="server" ID="UpdatePnl" UpdateMode="Conditional">
                                        <ContentTemplate>                                            
                                            
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="portlet light bordered">
                                                        <div class="portlet-title">
                                                            <div class="caption font-dark">
                                                                <i class="icon-settings font-dark"></i>
                                                                <span class="caption-subject bold uppercase">
                                                                    <asp:Label ID="LblConnectionsTitle" runat="server" />
                                                                </span>
                                                            </div>
                                                            <div id="divVendorTools" runat="server" class="tools">                                                                 
                                                                <a id="aInvitationToPartners" runat="server" href="#PartnersInvitationModal" data-toggle="modal" role="button" style="height:30px;" class="btn btn-sm blue">
                                                                    <i class="fa fa-user"></i>
                                                                    <asp:Label ID="LblSendNewInvitation" Text="Invite your partners" runat="server" />
                                                                </a>
                                                                <asp:Button ID="BtnProccedMessage" OnClick="BtnProccedMessage_OnClick" Text="Send Invitation" CssClass="btn btn-sm red" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="portlet-body">
                                                            <div class="row" id="divSearchArea" runat="server" style="margin-bottom:20px !important;">
                                                                <div class="col-md-6 col-sm-12">
                                                                    <div class="dataTables_length" id="sample_1_length">
                                                                        <label>
                                                                            Search by:
                                                                            <telerik:RadDropDownList ID="DdlCountries" AutoPostBack="true" OnSelectedIndexChanged="DdlCountries_SelectedIndexChanged" Width="180" runat="server">
                                                                            </telerik:RadDropDownList>
                                                                        </label>
                                                                    </div>
                                                                </div>                                                               
                                                                <div class="col-md-6 col-sm-12">
                                                                    <div class="dataTables_filter" style="float:right;">
                                                                        <label>
                                                                            Search:
                                                                            <Telerik:RadTextBox ID="RtbxCompanyNameEmail" CssClass="form-control input-sm input-small input-inline" EmptyMessage="Name/Email" runat="server" />
                                                                            <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" CssClass="btn btn-cta-primary" Text="Go" />
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                            </div>                                                           
                                                            <div class="table table-hover">
                                                                <telerik:RadGrid ID="RdgResellers" HeaderStyle-CssClass="table table-striped table-bordered table-hover" Style="margin: auto; position: relative;" AllowPaging="true"
                                                                    AllowSorting="false" PagerStyle-Position="Bottom" PageSize="10" Width="100%"
                                                                    CssClass="rgdd" OnNeedDataSource="RdgResellers_OnNeedDataSource" OnItemDataBound="RdgResellers_OnItemDataBound"
                                                                    AutoGenerateColumns="false" runat="server">
                                                                    <MasterTableView>
                                                                        <NoRecordsTemplate>
                                                                            <div class="emptyGridHolder">
                                                                                <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                            </div>
                                                                        </NoRecordsTemplate>
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn HeaderStyle-Width="10" HeaderText="Select" DataField="select" UniqueName="select">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="CbxSelectUser" runat="server" />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>                                                                            
                                                                            
                                                                            <telerik:GridTemplateColumn HeaderStyle-Width="40" HeaderText="Company Logo">
                                                                                <ItemTemplate>
                                                                                    <ul class="chat-list m-0 media-list">
                                                                                        <li class="media">  
                                                                                            <a id="aCompanyLogo" href="javascript:;" target="_blank" runat="server">
                                                                                                <div class="media-left avatar">
                                                                                                    <asp:Image ID="ImgCompanyLogo" Width="40" Height="40" style="margin-top:20px;cursor: pointer;" CssClass="media-object img-circle" runat="server" />
                                                                                                </div>
                                                                                            </a>
                                                                                        </li>
                                                                                    </ul>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn HeaderStyle-Width="170" HeaderText="Company Name" DataField="company_name" UniqueName="company_name" />
                                                                            
                                                                            <telerik:GridBoundColumn HeaderStyle-Width="150" HeaderText="Email" DataField="email" UniqueName="email" />
                                                                            <telerik:GridBoundColumn HeaderStyle-Width="140" HeaderText="Country" DataField="country" UniqueName="country" />
                                                                            <telerik:GridTemplateColumn Display="false" HeaderStyle-Width="30" HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LblStatus" runat="server" />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn Display="false" DataField="user_application_type" UniqueName="user_application_type" />
                                                                            <telerik:GridBoundColumn Display="false" DataField="company_logo" UniqueName="company_logo" />
                                                                            <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Id" Display="false" DataField="id" UniqueName="id" />

                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                                <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div style="">
                                                            
                                                            <a id="aInvitationToConnections" visible="false" runat="server" href="#ConnectionsInvitationModal"
                                                                data-toggle="modal" role="button" class="btn btn-primary"><i class="ion-plus-round mr-5">
                                                                </i>
                                                                <asp:Label ID="LblInvitationToConnections" Text="Invite your Connections" runat="server" />
                                                            </a>
                                                            <asp:Label ID="LblOrText" runat="server" />
                                        
                                                        </div>
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
    </div>
    <!-- Invitation to Vendor Connections form (modal view) -->
    <div id="ConnectionsInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <%--<controls:UcCreateNewInvitationToConnectionsMessage ID="UcCreateNewInvitationToConnectionsMessage" runat="server" />--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Invitation to Vendor Partners form (modal view) -->
    <div id="PartnersInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <controls:UcCreateNewInvitationToPartnersMessage ID="UcCreateNewInvitationToPartnersMessage" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Pop Up Invitation form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border" style="background-color:#627284;color:#fff;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title"><asp:Label ID="LblInvitationSendTitle" Text="Invitation Send" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblSuccessfullSendfMsg" CssClass="control-label" runat="server" />                                       
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
        <style>
            .RadGrid_MetroTouch .rgHeader:first-child, .RadGrid_MetroTouch th.rgResizeCol:first-child, .RadGrid_MetroTouch .rgFilterRow > td:first-child, .RadGrid_MetroTouch .rgRow > td:first-child, .RadGrid_MetroTouch .rgAltRow > td:first-child
            {
                width:10px;
            }
            .RadGrid_MetroTouch .rgHeader, .RadGrid_MetroTouch th.rgResizeCol, .RadGrid_MetroTouch .rgHeaderWrapper
            {
                width:120px;
            }
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

            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }

            function OpenPartnersInvitationPopUp() {
                $('#PartnersInvitationModal').modal('show');
            }

            function ClosePartnersInvitationPopUp() {
                $('#PartnersInvitationModal').modal('hide');
            }

        </script>
        <script src="/assets/global/plugins/ckeditor/ckeditor.js" type="text/javascript"></script>
    </telerik:RadScriptBlock>

</asp:Content>