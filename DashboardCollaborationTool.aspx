<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardCollaborationTool.aspx.cs" Inherits="WdS.ElioPlus.DashboardCollaborationTool" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/InvitationMessageForm.ascx" TagName="UcInvitationMessageForm" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/CreateNewInvitationToConnectionsMessage.ascx" TagName="UcCreateNewInvitationToConnectionsMessage" TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
                </div>
                <a id="aInvitationSend" runat="server" href="#SendInvitationModal" data-toggle="modal" role="button" class="btn btn-circle purple btn-md">
                    <asp:Label ID="Label1" Text="Invite Partners" runat="server" />
                </a>               
                <div class="portlet-body">
                    <h2><asp:Label ID="LblTitle" Text="Partners" runat="server" /></h2>                
                
                    <h2><asp:Label ID="Label2" Text="Sent Invitations" runat="server" /></h2> 
                    <telerik:RadGrid ID="RdgMyInvitations" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="false" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="TopAndBottom" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" CssClass="rgdd" OnItemDataBound="RdgMyInvitations_OnItemDataBound" OnNeedDataSource="RdgMyInvitations_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                        <MasterTableView>
                            <NoRecordsTemplate>
                                <div class="emptyGridHolder">
                                    <asp:Literal ID="LtlNoDataFound" runat="server" />
                                </div>
                            </NoRecordsTemplate>
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Id" Display="false" DataField="id" UniqueName="id" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Subject" DataField="inv_subject" UniqueName="inv_subject" />
                                <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Message" DataField="inv_content" UniqueName="inv_content" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Created Date" DataField="date_created" UniqueName="date_created" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="60" DataField="actions" UniqueName="actions">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgBtnEdit" ImageUrl="~/Images/edit.png" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <controls:messagecontrol id="UcMessageAlert" visible="false" runat="server" />
                    <div style="padding:10px;">
                        <a id="aCreateNew" runat="server" href="#CreateInvitationModal" data-toggle="modal" role="button" class="btn btn-circle purple btn-md">
                            <asp:Label ID="Label3" Text="Create New Invitation" runat="server" />
                        </a>
                    </div>     
                </div>           
            </div>        
        </div>

        <!-- Invitation form (modal view) -->
        <div id="SendInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <controls:UcInvitationMessageForm ID="UcInvitationMessageForm" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

         <!-- Invitation form (modal view) -->
        <div id="CreateInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>
                    <controls:UcCreateNewInvitationToConnectionsMessage ID="UcCreateNewInvitationToConnectionsMessage" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
</asp:Content>
