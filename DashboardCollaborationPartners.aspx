<%@ Page Title="" Language="C#" MasterPageFile="~/CollaborationToolMaster.Master" AutoEventWireup="true" CodeBehind="DashboardCollaborationPartners.aspx.cs" Inherits="WdS.ElioPlus.DashboardCollaborationPartners" %>
<%@ MasterType VirtualPath="~/CollaborationToolMaster.Master" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/InvitationMessageForm.ascx" TagName="UcInvitationMessageForm" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/CreateNewInvitationToConnectionsMessage.ascx" TagName="UcCreateNewInvitationToConnectionsMessage" TagPrefix="controls" %>
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
            </div> --%>
            <!-- END PAGE BAR -->
            <!-- BEGIN PAGE TITLE-->
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
                                <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
                                    <ContentTemplate>
                                <!-- END PAGE TITLE-->
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="padding: 10px;" class="form-group">
                                        </div>
                                        <a id="aInvitationSend" runat="server" role="button" class="btn btn-outline btn-rounded btn-primary">
                                            <asp:Label ID="LblInvitationSend" Text="Invite New Collaboration Partners" runat="server" />
                                        </a>
                                        <div class="widget-body" style="padding:0px !important;">
                                            <div class="" style="padding-left:0px !important;">
                                                <i class="fa fa-file-text-o"></i>
                                                <h4>
                                                    <asp:Label ID="LblTitle" Visible="false" runat="server" />
                                                </h4>
                                            </div>
                                            <div class="table table-hover" style="margin-bottom:0px !important;">
                                                <telerik:RadGrid ID="RdgReceivedInvitations" Style="margin: auto; position: relative;"
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
                                                        <%--<DetailTables>
                                                            <telerik:GridTableView DataKeyNames="id" Name="VendorsResellersUsersInvitations"
                                                                Width="100%">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                                    <telerik:GridBoundColumn Display="false" DataField="vendor_reseller_id" UniqueName="vendor_reseller_id" />
                                                                    <telerik:GridTemplateColumn HeaderStyle-Width="10" HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="ImgStepDescription" Style="cursor: pointer;" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn ItemStyle-CssClass="bg-blue" HeaderStyle-Width="80" Display="false"
                                                                        HeaderText="Subject" DataField="inv_subject" UniqueName="inv_subject" />
                                                                    <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Width="100"
                                                                        HeaderText="Subject" HeaderStyle-Font-Size="12" UniqueName="inv_subject">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TbxSubject" BorderStyle="None" Font-Size="12" TextMode="MultiLine"
                                                                                Rows="5" Height="100" Width="100%" ReadOnly="true" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" HeaderText="Content"
                                                                        DataField="inv_content" UniqueName="inv_content" />
                                                                    <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Width="200"
                                                                        HeaderText="Content" HeaderStyle-Font-Size="12" UniqueName="inv_content">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TbxContent" BorderStyle="None" Rows="5" TextMode="MultiLine" Font-Size="12"
                                                                                Width="100%" Height="100" ReadOnly="true" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Date Created" DataField="date_created"
                                                                        HeaderStyle-Font-Size="12" UniqueName="date_created" />
                                                                    <telerik:GridBoundColumn Display="false" DataField="last_updated" HeaderStyle-Font-Size="12"
                                                                        UniqueName="last_updated" />
                                                                    <telerik:GridBoundColumn Display="false" DataField="is_public" HeaderStyle-Font-Size="12"
                                                                        UniqueName="is_public" />
                                                                </Columns>
                                                            </telerik:GridTableView>
                                                        </DetailTables>--%>
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
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="20" HeaderText="Company Logo">
                                                                <ItemTemplate>
                                                                    <ul class="chat-list m-0 media-list">
                                                                        <li class="media">  
                                                                            <a id="aCompanyLogo" href="javascript:;" target="_blank" runat="server">
                                                                                <div class="media-left avatar">
                                                                                    <asp:Image ID="ImgCompanyLogo" CssClass="media-object img-circle" runat="server" />
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
                                                            <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Company Name" DataField="company_name"
                                                                UniqueName="company_name" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Email" DataField="email"
                                                                UniqueName="email" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Country" DataField="country"
                                                                UniqueName="country" />
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="70" HeaderText="Actions">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgBtnDelete" Width="20" OnClick="ImgBtnDeleteReceivedInvitations_OnClick" ImageUrl="~/images/icons/small/delete.png" runat="server" />
                                                                    <a id="aGoFullRegister" visible="false" style="width:20px; display:inline-block;" runat="server">
                                                                        <div>
                                                                            <asp:Image ID="ImgGoFullRegister" AlternateText="full register" ImageUrl="~/images/icons/small/info.png" runat="server" />
                                                                        </div>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>                                                            
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="10" HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblStatus" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
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
                                            <controls:MessageControl ID="UcReceiveMessageAlert" Visible="false" runat="server" />
                                            <div style="margin-bottom: 25px;">
                                            </div>
                                            <div id="divSearchArea" runat="server" style="padding: 10px; padding-bottom:0px !important;" class="form-group">
                                                <table>
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            <asp:Label ID="LblCompanyNameEmail" Text="Name/Email " runat="server" />
                                                        </td>
                                                        <td style="width: 190px;">
                                                            <telerik:RadTextBox ID="RtbxCompanyNameEmail" Skin="MetroTouch" runat="server" />
                                                        </td>
                                                        <td style="width: 70px;">
                                                            <asp:Label ID="LblCountry" Text="Country " runat="server" />
                                                        </td>
                                                            <td style="width: 200px;">
                                                            <telerik:RadDropDownList ID="DdlCountries" Width="180" runat="server">                                            
                                                            </telerik:RadDropDownList>
                                                        </td>
                                                        <td style="width: 60px;">
                                                            <asp:Label ID="LblInvitationStatus" Text="Status " runat="server" />
                                                        </td>
                                                        <td style="width: 170px;">
                                                            <telerik:RadDropDownList ID="DdlInvitationStatus" Width="150" runat="server">
                                                            </telerik:RadDropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="BtnSearch" runat="server" OnClick="BtnSearch_Click" CssClass="btn btn-cta-primary" Text="Search" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="" style="padding-left:0px !important;">
                                                <i class="fa fa-file-text-o"></i>
                                                <h5>
                                                    <asp:Label ID="LblTitle2" runat="server" />
                                                </h5>
                                            </div>
                                            <div class="table table-hover" style="margin-bottom:0px !important;">
                                                <telerik:RadGrid ID="RdgSendInvitations" Style="margin: auto; position: relative;"
                                                    AllowPaging="true" AllowSorting="false" PagerStyle-Position="Bottom"
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
                                                        <%--<DetailTables>
                                                            <telerik:GridTableView DataKeyNames="id" Name="VendorsResellersUsersInvitations"
                                                                Width="100%">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                                    <telerik:GridBoundColumn Display="false" DataField="vendor_reseller_id" UniqueName="vendor_reseller_id" />
                                                                    <telerik:GridTemplateColumn HeaderStyle-Width="10" HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="ImgStepDescription" Style="cursor: pointer;" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn ItemStyle-CssClass="bg-blue" HeaderStyle-Width="80" Display="false"
                                                                        HeaderText="Subject" DataField="inv_subject" UniqueName="inv_subject" />
                                                                    <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Width="100"
                                                                        HeaderText="Subject" HeaderStyle-Font-Size="12" UniqueName="inv_subject">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TbxSubject" BorderStyle="None" Font-Size="12" TextMode="MultiLine"
                                                                                Rows="5" Height="100" Width="100%" ReadOnly="true" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" HeaderText="Content"
                                                                        DataField="inv_content" UniqueName="inv_content" />
                                                                    <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Width="200"
                                                                        HeaderText="Content" HeaderStyle-Font-Size="12" UniqueName="inv_content">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TbxContent" BorderStyle="None" Rows="5" TextMode="MultiLine" Font-Size="12"
                                                                                Width="100%" Height="100" ReadOnly="true" runat="server" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Date Created" DataField="date_created"
                                                                        HeaderStyle-Font-Size="12" UniqueName="date_created" />
                                                                    <telerik:GridBoundColumn Display="false" DataField="last_updated" HeaderStyle-Font-Size="12"
                                                                        UniqueName="last_updated" />
                                                                    <telerik:GridBoundColumn Display="false" DataField="is_public" HeaderStyle-Font-Size="12"
                                                                        UniqueName="is_public" />
                                                                </Columns>
                                                            </telerik:GridTableView>
                                                        </DetailTables>--%>
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
                                                                    <ul class="chat-list m-0 media-list">
                                                                        <li class="media">  
                                                                            <a id="aCompanyLogo" href="javascript:;" target="_blank" runat="server">
                                                                                <div class="media-left avatar">
                                                                                    <asp:Image ID="ImgCompanyLogo" CssClass="media-object img-circle" Style="cursor: pointer;" runat="server" />
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
                                                            <telerik:GridBoundColumn HeaderStyle-Width="70" HeaderText="Company Name" DataField="company_name"
                                                                UniqueName="company_name" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="70" HeaderText="Email" DataField="email"
                                                                UniqueName="email" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="40" HeaderText="Country" DataField="country"
                                                                UniqueName="country" />
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="20" HeaderText="Actions">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgBtnSendEmail" Visible="false" OnClick="ImgBtnSendEmail_OnClick"
                                                                        ImageUrl="~/images/email_notif.png" runat="server" />
                                                                    <a id="aDelete" onserverclick="aDelete_OnClick" runat="server">
                                                                        <asp:Image ID="ImgDelete" AlternateText="delete" ImageUrl="~/images/icons/small/delete.png" runat="server" />
                                                                    </a>
                                                                    <asp:ImageButton ID="ImgBtnDelete" Visible="false" OnClick="ImgBtnDeleteSendInvitations_OnClick" ImageUrl="~/images/icons/small/delete.png"
                                                                        runat="server" />
                                                                    <a id="aCollaborationRoom" visible="false" runat="server">
                                                                        <asp:Image ID="ImgCollaborationRoom" AlternateText="collaboration room" Width="20" Height="20" Style="margin-top: -10px;" ToolTip="Start conversation" ImageUrl="~/images/icons/chat/cht_60.png" runat="server" />
                                                                    </a>
                                                                    <asp:ImageButton ID="ImgBtnCollaborationRoom" Visible="false" OnClick="ImgBtnCollaborationRoom_OnClick" ToolTip="Start conversation" ImageUrl="~/images/icons/chat/cht_60.png" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="30" HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblStatus" runat="server" />
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
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
            <script type="text/javascript">
                function OpenConfPopUp() {
                    $('#divConfirm').modal('show');
                }
            </script>
            <script type="text/javascript">
                function CloseConfPopUp() {
                    $('#divConfirm').modal('hide');
                }
            </script>

            <script type="text/javascript">
                function OpenSendInvitationPopUp() {
                    $('#SendInvitationModal').modal('show');
                }
            </script>
            <script type="text/javascript">
                function CloseSendInvitationPopUp() {
                    $('#SendInvitationModal').modal('hide');
                }
            </script>
        
</asp:Content>
