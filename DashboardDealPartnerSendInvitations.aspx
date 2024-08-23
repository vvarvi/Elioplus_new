<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardDealPartnerSendInvitations.aspx.cs" Inherits="WdS.ElioPlus.DashboardDealPartnerSendInvitations" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/CreateNewInvitationToPartnersMessage.ascx" TagName="UcCreateNewInvitationToPartnersMessage" TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
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
                        <div class="row" style="text-align:center">
                            <div class="col-md-12">
                                <h1>
                                    <strong>
                                        <asp:Label ID="LblTitleInvite" runat="server" />
                                    </strong>
                                </h1>
                            </div>
                        </div>
                        <div class="row" style="text-align:center">                            
                            <div class="col-md-12">
                                <div style="padding: 10px;" class="form-group">
                                </div>
                                <a id="aCancelInvitation" visible="false" runat="server" role="button" class="btn btn-outline btn-rounded btn-primary">
                                    <asp:Label ID="LblCancelInvitation" Text="Back to Partner Directory" runat="server" />
                                </a>
                                <h5>
                                    <strong>
                                        <asp:Label ID="LblTitleInvite2" runat="server" />
                                    </strong>
                                </h5>
                                <div style="margin-bottom:20px;"></div>
                                <a id="aInvitationToPartners" runat="server" href="#PartnersInvitationModal" data-toggle="modal" role="button" style="height:30px;" class="btn btn-success">
                                    <asp:Label ID="LblSendNewInvitation" Text="Send Invitations" runat="server" />
                                </a>
                                <div style="margin-bottom:50px;"></div>
                                <h1>
                                    <strong>
                                        <asp:Label ID="LblTitleInvite3" runat="server" />
                                    </strong>
                                </h1>
                                <div style="margin-bottom:50px;"></div>
                                <h5>
                                    <strong>
                                        <asp:Label ID="LblTitleInvite4" runat="server" />
                                    </strong>
                                </h5>                                
                                <asp:Label ID="LblTitleInvite5" runat="server" />
                                <div style="margin-bottom:50px;"></div>
                                <div class="widget-body" style="padding:0px !important;">                                    
                                    <asp:UpdatePanel runat="server" ID="UpdatePnl" UpdateMode="Conditional">
                                        <ContentTemplate>                                            
                                            <div class="row" style="margin-bottom:20px;">
                                                <div class="col-md-1"></div>
                                                <div class="col-md-10">
                                                    <div class="dataTables_filter" style="float:left;">
                                                        <label>
                                                            Search:
                                                            <Telerik:RadTextBox ID="RtbxCompanyNameEmail" CssClass="form-control input-sm input-small input-inline" EmptyMessage="Name/Email" runat="server" />
                                                            <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" CssClass="btn btn-cta-primary" Text="Go" />
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-md-1"></div>
                                            </div>
                                            <div class="row">                                                
                                                <div class="col-md-1"></div>                                                
                                                <div class="col-md-10">
                                                    <div class="portlet light bordered">
                                                       
                                                        <div class="portlet-body">
                                                                                                                  
                                                            <div class="table table-hover">
                                                                <telerik:RadGrid ID="RdgResellers" HeaderStyle-CssClass="table table-striped table-bordered table-hover" Style="margin: auto; position: relative;" AllowPaging="true"
                                                                    AllowSorting="false" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" HeaderStyle-ForeColor="#e7505a" HeaderStyle-Font-Bold="true" PagerStyle-Position="Bottom" PageSize="10" Width="100%"
                                                                    CssClass="rgdd" OnNeedDataSource="RdgResellers_OnNeedDataSource" OnItemDataBound="RdgResellers_OnItemDataBound"
                                                                    AutoGenerateColumns="false" runat="server">
                                                                    <MasterTableView>
                                                                        <NoRecordsTemplate>
                                                                            <div class="emptyGridHolder">
                                                                                <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                            </div>
                                                                        </NoRecordsTemplate>
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn Display="false" HeaderStyle-Width="10" HeaderText="Select" DataField="select" UniqueName="select">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="CbxSelectUser" runat="server" />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>                                                                            
                                                                            
                                                                            <telerik:GridTemplateColumn HeaderStyle-Width="120" HeaderText="Company">
                                                                                <ItemTemplate>
                                                                                    <ul class="chat-list m-0 media-list">
                                                                                        <li class="media" style="margin-bottom:0;">  
                                                                                            <a id="aCompanyLogo" href="javascript:;" target="_blank" runat="server" style="float:left;">
                                                                                                <div class="media-left avatar">
                                                                                                    <asp:Image ID="ImgCompanyLogo" Width="40" Height="40" style="margin-top:20px;cursor: pointer;" CssClass="media-object img-circle" runat="server" />
                                                                                                </div>
                                                                                                <div class="media-right" style="line-height:4;">
                                                                                                    <asp:Label ID="LblCompanyName" runat="server" />
                                                                                                </div>
                                                                                                <div id="divNotification" runat="server" visible="false" class="media-right">
                                                                                                    <span id="spanNotificationMsg" class="badge bg-red" style="background-color:#ed7177;" title="New unread message" runat="server">
                                                                                                        <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                                                    </span>
                                                                                                </div>
                                                                                            </a>
                                                                                        </li>
                                                                                    </ul>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn Display="false" HeaderStyle-Width="170" HeaderText="Company Name" DataField="company_name" UniqueName="company_name" />
                                                                            <telerik:GridBoundColumn HeaderStyle-Width="170" HeaderText="Website" DataField="website" UniqueName="website" />
                                                                            <telerik:GridBoundColumn Display="false" HeaderStyle-Width="150" HeaderText="Email" DataField="email" UniqueName="email" />
                                                                            <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Country" DataField="country" UniqueName="country" />
                                                                            <telerik:GridTemplateColumn Display="false" HeaderStyle-Width="50" HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LblStatus" runat="server" />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn Display="false" DataField="user_application_type" UniqueName="user_application_type" />
                                                                            <telerik:GridBoundColumn Display="false" DataField="company_logo" UniqueName="company_logo" />
                                                                            <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Id" Display="false" DataField="id" UniqueName="id" />
                                                                            <telerik:GridTemplateColumn HeaderStyle-Width="50" HeaderText="Actions">
                                                                                <ItemTemplate>
                                                                                    <div class="dropdown">
                                                                                        <li id="liUserOptions" runat="server" class="btn btn-sm btn-clean btn-icon btn-icon-md">
                                                                                            <a class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                                                                                <asp:Image ID="ImgUserPhoto" CssClass="img-circle" ImageUrl="~/images/icons/settings-vert.png" runat="server" />  
                                                                                                <span class="username username-hide-on-mobile"></span>
                                                                                            </a>
                                                                                            <ul class="dropdown-menu dropdown-menu-default" style="background-color:#fff !important; min-width:120px !important; text-align: center;">
                                                                                                <li>
                                                                                                    <a id="aSendInvitation" onserverclick="aSendInvitation_OnClick" runat="server">
                                                                                                        <i class="fa fa-send"></i>
                                                                                                        <asp:Label ID="LblaSendInvitation" Text="Send Invitation" runat="server" />
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
                                                <div class="col-md-1"></div>
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

        <!--begin::Page Scripts(used by this page)-->
		<script src="assets/plugins/custom/uppy/uppy.bundle.js"></script>
		<script src="assets/js/pages/crud/file-upload/uppy.js"></script>
		<!--end::Page Scripts-->
    </telerik:RadScriptBlock>

</asp:Content>