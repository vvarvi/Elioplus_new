<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardDealPartnerDirectory.aspx.cs" Inherits="WdS.ElioPlus.DashboardDealPartnerDirectory" %>

<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/InvitationMessageForm.ascx" TagName="UcInvitationMessageForm" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/CreateNewInvitationToConnectionsMessage.ascx" TagName="UcCreateNewInvitationToConnectionsMessage" TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="/assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    
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
        <div class="col-md-12">
            
            <div style="padding: 10px;" class="form-group">
                <div id="divDealPartnerDirectory" visible="false" style="" class="note note-success" runat="server">
                    <p>
                        <asp:Label ID="LblDealPartnerDirectoryTitle" runat="server" />
                    </p>
                    <div id="divInvitationToPartners" visible="false" style="padding:10px 0 0px 0;" runat="server">
                        <a id="aInvitationToPartners" runat="server" role="button" class="btn btn-outline btn-rounded btn-primary">
                            <i class="ion-plus-round mr-5"></i>
                            <asp:Label ID="LblInvitationSend" Text="Invite New Collaboration Partners" runat="server" />
                        </a>
                    </div>
                </div>
            </div>

            <div class="portlet-body form">
                <form action="#" class="horizontal-form">               
                    <div class="form-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="portlet light bordered">
                                            <div class="portlet-title">
                                                <div class="caption font-dark">
                                                    <i class="icon-settings font-dark"></i>
                                                    <span class="caption-subject bold uppercase">
                                                        <asp:Label ID="LblTitle" runat="server" />
                                                    </span>
                                                </div>
                                                <div id="divResellerTools" runat="server" visible="false" class="tools"> 
                                                    <a id="aVendorInvitation" runat="server" style="height:30px;" class="btn btn-sm blue">
                                                        <i class="fa fa-user"></i>
                                                        <asp:Label ID="LblNewReceivedInvitations" Text="Send to vendor" runat="server" />
                                                    </a>
                                                </div>
                                            </div>
                                            <div class="portlet-body">
                                                <div class="table table-hover" style="margin-bottom:0px !important;">
                                                    <telerik:RadGrid ID="RdgReceivedInvitations" HeaderStyle-CssClass="table table-striped table-bordered table-hover" Style="margin: auto; position: relative;"
                                                        AllowPaging="true" AllowSorting="false" PagerStyle-Position="Bottom"
                                                        PageSize="10" Width="100%" CssClass="rgdd" 
                                                        OnPreRender="RdgReceivedInvitations_PreRender" OnNeedDataSource="RdgReceivedInvitations_OnNeedDataSource"
                                                        OnItemDataBound="RdgReceivedInvitations_OnItemDataBound" AutoGenerateColumns="false"
                                                        runat="server">         <%--OnDetailTableDataBind="RdgReceivedInvitations_DetailTableDataBind"--%>
                                                        <MasterTableView Width="100%" DataKeyNames="id" Name="Parent" AllowMultiColumnSorting="true">
                                                            <NoRecordsTemplate>
                                                                <div class="emptyGridHolder">
                                                                    <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                </div>
                                                            </NoRecordsTemplate>                                                            
                                                            <Columns>
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="10" HeaderText="Select" DataField="select"
                                                                    UniqueName="select">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CbxSelectUser" runat="server" />
                                                                        <asp:ImageButton ID="ImgNotAvailable" Visible="false" ImageUrl="~/images/cancel.png" runat="server" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                                <telerik:GridBoundColumn Display="false" DataField="master_user_id" UniqueName="master_user_id" />
                                                                <telerik:GridBoundColumn Display="false" DataField="partner_user_id" UniqueName="partner_user_id" />
                                                                <telerik:GridBoundColumn Display="false" DataField="invitation_status" UniqueName="invitation_status" />
                                                                <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="120" HeaderText="Company Logo">
                                                                    <ItemTemplate>
                                                                        <ul class="chat-list m-0 media-list" style="text-align:center;">
                                                                            <li class="media">  
                                                                                <a id="aCompanyLogo" href="javascript:;" target="_blank" runat="server">
                                                                                    <div class="media-left avatar">
                                                                                        <asp:Image ID="ImgCompanyLogo" Width="40" Height="40" style="margin-top:20px;" CssClass="media-object img-circle" runat="server" />
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
                                                                <telerik:GridBoundColumn HeaderStyle-Width="150" HeaderText="Company Name" DataField="company_name"
                                                                    UniqueName="company_name" />
                                                                <telerik:GridBoundColumn HeaderStyle-Width="150" HeaderText="Email" DataField="email"
                                                                    UniqueName="email" />
                                                                <telerik:GridBoundColumn HeaderStyle-Width="120" HeaderText="Country" DataField="country"
                                                                    UniqueName="country" />                                                                                                           
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="100" HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LblStatus" runat="server" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="More Details" HeaderStyle-Width="120" UniqueName="company_more_details">
                                                                    <ItemTemplate>
                                                                        <a id="aMoreDetails" class="btn btn-sm blue" runat="server">
                                                                            <asp:Label ID="LblMoreDetails" Text="Manage Account" runat="server" />
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="50" HeaderText="Actions">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgBtnDelete" Width="20" OnClick="ImgBtnDeleteReceivedInvitations_OnClick" ImageUrl="~/images/icons/small/delete.png" runat="server" />
                                                                        <a id="aGoFullRegister" visible="false" style="width:20px; display:inline-block;" runat="server">
                                                                            <div>
                                                                                <asp:Image ID="ImgGoFullRegister" ImageUrl="~/images/icons/small/info.png" runat="server" />
                                                                            </div>
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn> 
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                    <controls:MessageControl ID="UcReceiveMessageAlert" Visible="false" runat="server" />
                                                </div>
                                                <div id="divActions" class="" style="text-align: center;
                                                    margin-bottom: 30px; margin-top: 30px; padding:5px;" runat="server">
                                                    <asp:Button ID="RbtnAccept" OnClick="RbtnAccept_OnClick" Text="Accept" Width="180px"
                                                        CssClass="btn btn-primary" runat="server" />
                                                    <asp:Button ID="RbtnPending" OnClick="RbtnPending_OnClick" Text="Pending" Width="180px"
                                                        CssClass="btn btn-success" runat="server" />
                                                    <asp:Button ID="RbtnReject" OnClick="RbtnReject_OnClick" Text="Reject" Width="180px"
                                                        CssClass="btn btn-error" runat="server" />
                                                    <asp:Button ID="RbtnDelete" Visible="false" OnClick="BtnDelete_OnClick" Text="Delete" Width="180px"
                                                        CssClass="btn btn-danger" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div style="margin-bottom: 15px;" />
                                
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="portlet light bordered">
                                            <div class="portlet-title">
                                                <div class="caption font-dark">
                                                    <i class="icon-settings font-dark"></i>
                                                    <span class="caption-subject bold uppercase">
                                                        <asp:Label ID="LblTitle2" runat="server" />
                                                    </span>
                                                </div>
                                                <div id="divVendorTools" runat="server" visible="false" class="tools"> 
                                                    <a id="aChannelPartnerInvitation" runat="server" style="height:30px;" class="btn btn-sm blue">
                                                        <i class="fa fa-user"></i>
                                                        <asp:Label ID="LblSendNewInvitation" Text="Invite partners" runat="server" />
                                                    </a>
                                                </div>
                                            </div>
                                            <div class="portlet-body">
                                                <div class="row" id="divSearchArea" runat="server" style="margin-bottom:20px !important;">
                                                    <div class="col-md-4 col-sm-12">
                                                        <div class="dataTables_length" id="sample_1_length">
                                                            <label>
                                                                Search by:
                                                                <telerik:RadDropDownList ID="DdlCountries" AutoPostBack="true" OnSelectedIndexChanged="DdlCountries_SelectedIndexChanged" Width="180" runat="server">
                                                                </telerik:RadDropDownList>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 col-sm-12">
                                                        <div class="dataTables_length" id="Div1">
                                                            <label>
                                                                Sort by:
                                                                <telerik:RadDropDownList AutoPostBack="true" OnSelectedIndexChanged="DdlInvitationStatus_SelectedIndexChanged" ID="DdlInvitationStatus" Width="150" runat="server">
                                                                </telerik:RadDropDownList>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 col-sm-12">
                                                        <div class="dataTables_filter" style="float:right;">
                                                            <label>
                                                                Search:
                                                                <Telerik:RadTextBox ID="RtbxCompanyNameEmail" CssClass="form-control input-sm input-small input-inline" EmptyMessage="Name/Email" runat="server" />
                                                                <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" CssClass="btn btn-cta-primary" Text="Go" />
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="table table-hover" style="margin-bottom:10px !important;">
                                                    <telerik:RadGrid ID="RdgSendInvitations" HeaderStyle-CssClass="table table-striped table-bordered table-hover" Style="margin: auto; position: relative;"
                                                        AllowPaging="true" AllowSorting="true" Font-Size="5" PagerStyle-Position="Bottom"
                                                            PageSize="10" Width="100%" CssClass="rgdd"  
                                                        OnPreRender="RdgSendInvitations_PreRender" OnNeedDataSource="RdgSendInvitations_OnNeedDataSource"
                                                        OnItemDataBound="RdgSendInvitations_OnItemDataBound" AutoGenerateColumns="false"
                                                        runat="server">
                                                        <MasterTableView Width="100%" DataKeyNames="id" Name="Parent" AllowMultiColumnSorting="true">
                                                            <NoRecordsTemplate>
                                                                <div class="emptyGridHolder">
                                                                    <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                </div>
                                                            </NoRecordsTemplate>                                                            
                                                            <Columns>
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="10" Visible="false" HeaderText="Select"
                                                                    DataField="select" UniqueName="select">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CbxSelectUser" runat="server" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                                <telerik:GridBoundColumn Display="false" DataField="master_user_id" UniqueName="master_user_id" />
                                                                <telerik:GridBoundColumn Display="false" DataField="partner_user_id" UniqueName="partner_user_id" />
                                                                <telerik:GridBoundColumn Display="false" DataField="invitation_status" UniqueName="invitation_status" />          
                                                                <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="40" HeaderText="Company Logo">
                                                                    <ItemTemplate>
                                                                        <ul class="chat-list m-0 media-list" style="text-align:center;">
                                                                            <li class="media">  
                                                                                <a id="aCompanyLogo" href="javascript:;" target="_blank" runat="server">
                                                                                    <div class="media-left avatar">
                                                                                        <asp:Image ID="ImgCompanyLogo" Width="40" Height="40" style="margin-top:20px;cursor: pointer;" CssClass="media-object img-circle" runat="server" />
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
                                                                <telerik:GridBoundColumn HeaderStyle-Width="60" HeaderText="Company Name" DataField="company_name"
                                                                    UniqueName="company_name" />
                                                                <telerik:GridBoundColumn HeaderStyle-Width="70" HeaderText="Email" DataField="email"
                                                                    UniqueName="email" />
                                                                <telerik:GridBoundColumn HeaderStyle-Width="40" HeaderText="Country" DataField="country"
                                                                    UniqueName="country" />                                                
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="30" HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LblStatus" runat="server" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="More Details" HeaderStyle-Width="50" UniqueName="company_more_details">
                                                                    <ItemTemplate>
                                                                        <a id="aMoreDetails" class="btn btn-sm blue" runat="server">
                                                                            <asp:Label ID="LblMoreDetails" Text="Manage Account" runat="server" />
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>                                                                
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="20" HeaderText="Actions">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgBtnSendEmail" Visible="false" OnClick="ImgBtnSendEmail_OnClick"
                                                                            ImageUrl="~/images/email_notif.png" runat="server" />
                                                                        <a id="aDelete" onserverclick="aDelete_OnClick" runat="server">
                                                                            <asp:Image ID="ImgDelete" ImageUrl="~/images/icons/small/delete.png" runat="server" />
                                                                        </a>
                                                                        <asp:ImageButton ID="ImgBtnDelete" Visible="false" OnClick="ImgBtnDeleteSendInvitations_OnClick" ImageUrl="~/images/icons/small/delete.png"
                                                                            runat="server" />
                                                                        <a id="aCollaborationRoom" visible="false" runat="server">
                                                                            <asp:Image ID="ImgCollaborationRoom" Width="20" Height="20" Style="margin-top: -10px;" ToolTip="Start conversation" ImageUrl="~/images/icons/chat/cht_60.png" runat="server" />
                                                                        </a>
                                                                        <asp:ImageButton ID="ImgBtnCollaborationRoom" Visible="false" OnClick="ImgBtnCollaborationRoom_OnClick" ToolTip="Start conversation" ImageUrl="~/images/icons/chat/cht_60.png" runat="server" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                    <controls:MessageControl ID="UcSendMessageAlert" Visible="false" runat="server" />
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
    
    <!-- Invitation form (modal view) -->
    <div id="SendInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
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
    <!-- Invitation form (modal view) -->
    <div id="CreateInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <controls:UcCreateNewInvitationToConnectionsMessage ID="UcCreateNewInvitationToConnectionsMessage"
                    runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Confirmation Delete Partner form (modal view) -->
    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title"><asp:Label ID="LblConfTitle" Text="Confirmation" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblConfMsg" Text="Are you sure you want to delete this partner from your list?" CssClass="control-label" runat="server" />
                                        <asp:HiddenField ID="HdnVendorResellerCollaborationId" Value="0" runat="server" />                           
                                    </div>                                    
                                </div>                            
                            </div>                    
                        </div>
                        <div id="divFailure" runat="server" visible="false" class="col-md-12 alert alert-danger" style="text-align:center">
                            <strong>
                                <asp:Label ID="LblFailure" Text="Error! " runat="server" />
                            </strong>
                            <asp:Label ID="LblFailureMsg" runat="server" />
                        </div>
                        <div id="divSuccess" runat="server" visible="false" class="col-md-12 alert alert-success" style="text-align:center;">
                            <strong>
                                <asp:Label ID="LblSuccess" Text="Done! " runat="server" />
                            </strong>
                            <asp:Label ID="LblSuccessMsg" runat="server" />
                        </div> 
                        <div class="modal-footer">                            
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnDelete" OnClick="BtnDelete_OnClick" Text="Delete" CssClass="btn red-sunglo" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Payment form (modal view) -->
    <div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <controls:UcStripe id="UcStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">

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
            function RapPage_OnRequestStart(sender, args) {
                $('#loader').show();
            }
            function endsWith(s) {
                return this.length >= s.length && this.substr(this.length - s.length) == s;
            }
            function RapPage_OnResponseEnd(sender, args) {
                $('#loader').hide();
            }
            
            function OpenConfPopUp() {
                $('#divConfirm').modal('show');
            }

            function CloseConfPopUp() {
                $('#divConfirm').modal('hide');
            }

            function OpenSendInvitationPopUp() {
                $('#SendInvitationModal').modal('show');
            }

            function CloseSendInvitationPopUp() {
                $('#SendInvitationModal').modal('hide');
            }
        </script>
        
        <script src="/assets/global/scripts/datatable.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
        
        <script src="/assets/pages/scripts/table-datatables-colreorder.min.js" type="text/javascript"></script>
            
    </telerik:RadScriptBlock>

</asp:Content>
