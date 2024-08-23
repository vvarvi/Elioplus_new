<%@ Page Title="" Language="C#" MasterPageFile="~/CollaborationToolMaster.Master" AutoEventWireup="true" CodeBehind="DashboardCollaborationNewPartners.aspx.cs" Inherits="WdS.ElioPlus.DashboardCollaborationNewPartners" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/CreateNewInvitationToPartnersMessage.ascx" TagName="UcCreateNewInvitationToPartnersMessage" TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
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
                                    <asp:Label ID="LblCancelInvitation" Text="Back to Partners" runat="server" />
                                </a>
                                <div class="widget-body" style="padding:0px !important;">                                    
                                    <asp:UpdatePanel runat="server" ID="UpdatePnl" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="divExistingPartners" class="col-md-2" style="width:20%; padding-left:0px !important;" runat="server">
                                                <div class="widget-heading" style="padding-left:0px !important;">
                                                    <i class="fa fa-file-text-o"></i>
                                                    <asp:Label ID="Label1" Text="Your Existing Partners" runat="server" />
                                                </div>
                                                <div class="">
                                                    <a id="aInvitationToPartners" runat="server" href="#PartnersInvitationModal" data-toggle="modal"
                                                        role="button" class="btn btn-primary"><i class="ion-plus-round mr-5">
                                                        </i>
                                                        <asp:Label ID="LblInvitationToPartners" Text="Invite your Partners" runat="server" />
                                                    </a>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-10" style="width:80%;">
                                                <div id="divSearchArea" runat="server" style="padding:10px 10px 10px 0;" class="form-group">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 300px;">
                                                                <telerik:RadTextBox ID="RtbxCompanyName" EmptyMessage="company name or email" Width="280" Skin="MetroTouch" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" CssClass="btn btn-cta-primary" Text="Search" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="widget-heading" style="padding-left:0px !important;">
                                                    <i class="fa fa-file-text-o"></i>
                                                    <asp:Label ID="LblConnectionsTitle" Text="Your Connections" runat="server" />
                                                </div>
                                            <div class="table table-hover">
                                                <telerik:RadGrid ID="RdgResellers" Style="margin: auto; position: relative;" AllowPaging="true"
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
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="10" HeaderText="Select" DataField="select"
                                                                UniqueName="select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CbxSelectUser" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>                                                            
                                                            <telerik:GridBoundColumn Display="false" DataField="company_logo" UniqueName="company_logo" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Id" Display="false" DataField="id"
                                                                UniqueName="id" />
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="50" HeaderText="Company Logo">
                                                                <ItemTemplate>
                                                                    <ul class="chat-list m-0 media-list">
                                                                        <li class="media">  
                                                                            <a id="aCompanyLogo" href="javascript:;" target="_blank" runat="server">
                                                                                <div class="media-left avatar">
                                                                                    <asp:Image ID="ImgCompanyLogo" CssClass="media-object img-circle" runat="server" />
                                                                                </div>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="150" HeaderText="Company Name" DataField="company_name"
                                                                UniqueName="company_name" />
                                                            <telerik:GridBoundColumn Display="false" DataField="user_application_type" UniqueName="user_application_type" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Email" DataField="email"
                                                                UniqueName="email" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="40" HeaderText="Country" DataField="country"
                                                                UniqueName="country" />
                                                            <telerik:GridTemplateColumn Display="false" HeaderStyle-Width="10" HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblStatus" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
                                            </div>
                                            <div style="">
                                                <asp:Button ID="BtnProccedMessage" OnClick="BtnProccedMessage_OnClick" Text="Send Invitation" CssClass="btn btn-primary" runat="server" />
                                                <a id="aInvitationToConnections" visible="false" runat="server" href="#ConnectionsInvitationModal"
                                                    data-toggle="modal" role="button" class="btn btn-primary"><i class="ion-plus-round mr-5">
                                                    </i>
                                                    <asp:Label ID="LblInvitationToConnections" Text="Invite your Connections" runat="server" />
                                                </a>
                                                <asp:Label ID="LblOrText" runat="server" />
                                        
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

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">

            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }
        </script>
        <script src="/assets/global/plugins/ckeditor/ckeditor.js" type="text/javascript"></script>
    </telerik:RadScriptBlock>

</asp:Content>