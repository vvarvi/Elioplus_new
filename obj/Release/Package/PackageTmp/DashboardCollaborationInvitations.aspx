<%@ Page Title="" Language="C#" MasterPageFile="~/CollaborationToolMaster.Master" AutoEventWireup="true" CodeBehind="DashboardCollaborationInvitations.aspx.cs" Inherits="WdS.ElioPlus.DashboardCollaborationInvitations" %>

<asp:Content ID="DashEditHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="DashEditMain" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">        
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

           function isNumberOnly(evt) {
               var charCode = (evt.which) ? evt.which : event.keyCode
               if (charCode > 31 && (charCode < 48 || charCode > 57))
                   return false;

               return true;
           }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">
        <div class="main-container">
        <div id="page-container">            
            <div id="page-content container-fluid p-0">                
                <div class="main-content content-lg">
                    <div class="row">
                    <%--<div class="page-bar">
                        <ul class="page-breadcrumb">
                            <li>
                                <span><asp:Label ID="LblDashboard" runat="server" /></span><i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span><asp:Label ID="LblDashPage" runat="server" /></span>
                            </li>
                        </ul>
                    </div>   
                    <h3 class="fw-300 mt-0">
                        <asp:Label ID="LblElioplusDashboard" runat="server" />
                        <small>
                            <asp:Label ID="LblDashSubTitle" runat="server" />
                        </small>
                    </h3>--%>
                    <div style="float:left">
                        <a id="aBack" class="btn btn-outline btn-rounded btn-primary" runat="server"> Go to Partners list
                            <i class="m-icon-swapleft m-icon-white"></i>
                        </a>
                    </div>
                    <div class="col-md-12">                        
                        <div class="col-md-3">
                        </div>                   
                        <div class="col-md-6">
                            <a href="#AddInvitation" id="aAddInvitation" data-toggle="modal" class="btn btn-success btn-block" runat="server">
                                <asp:Label ID="LblAddOppNote" runat="server" />
                                <i id="iAddNote" class="fa fa-edit" runat="server"></i>
                            </a>
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                </div>
                                <div id="divAddInvitationSucc" runat="server" visible="false" class="col-md-8 alert alert-success" style="text-align:center; margin-top:30px;">
                                    <strong>
                                        <asp:Label ID="LblAddInvitationErr" Text="Done! " runat="server" />
                                    </strong>
                                    <asp:Label ID="LblAddInvitationSucc" runat="server" />
                                </div>
                                <div id="divInvitationInfo" runat="server" visible="true" class="col-md-8 alert alert-info" style="text-align:center; margin-top:30px;">
                                    <strong>
                                        <asp:Label ID="LblInvitationInfo" Text="Info! " runat="server" />
                                    </strong>
                                    <asp:Label ID="LblInvitationInfoContent" Text="Create and manage your invitations. Click 'Add Invitation' above to create a new one or manage the existing ones below." runat="server" />
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>                        
                            <div class="row">
                                <div class="col-md-12">                            
                                    
                                        <div class="widget-heading">
                                            <div class="widget-title">                                                
                                                    <i class="fa fa-file-text-o"></i>Invitations list                                   
                                            </div>
                                        </div>
                                        <div class="widget-body">
                                            <div class="table table-hover">                                        
                                                <telerik:RadGrid ID="RdgInvitations" Style="margin: auto; position: relative;" HeaderStyle-CssClass="bg-blue" AllowPaging="true" AllowSorting="false" PagerStyle-Position="TopAndBottom" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" CssClass="table table-bordered table-striped table-condensed" OnItemDataBound="RdgInvitations_OnItemDataBound" OnNeedDataSource="RdgInvitations_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                                    <MasterTableView CssClass="table-status">                                                                
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                            <telerik:GridBoundColumn ItemStyle-CssClass="bg-blue" HeaderStyle-Width="80" Display="false" HeaderText="Subject" DataField="inv_subject" UniqueName="inv_subject" />
                                                            <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Width="100" HeaderText="Subject" HeaderStyle-Font-Size="12" UniqueName="inv_subject">
                                                                <ItemTemplate>                                                            
                                                                    <asp:TextBox ID="TbxSubject" BorderStyle="None" Font-Size="12" MaxLength="200" Width="100%" ReadOnly="true" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" HeaderText="Content" DataField="inv_content" UniqueName="inv_content" />
                                                            <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Width="200" HeaderText="Content" HeaderStyle-Font-Size="12" UniqueName="inv_content">
                                                                <ItemTemplate>                                                            
                                                                    <asp:TextBox ID="TbxContent" BorderStyle="None" Font-Size="12" MaxLength="200" Width="100%" ReadOnly="true" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn  HeaderStyle-Width="100" HeaderText="Date Created" DataField="date_created" HeaderStyle-Font-Size="12" UniqueName="date_created" />
                                                            <telerik:GridBoundColumn  HeaderStyle-Width="100" HeaderText="Last Edit Date" DataField="last_updated" HeaderStyle-Font-Size="12" UniqueName="last_updated" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="100" Display="false" HeaderText="Is Public" DataField="is_public" UniqueName="is_public" />
                                                            <telerik:GridTemplateColumn  HeaderStyle-Width="50" HeaderText="Actions" DataField="actions" HeaderStyle-Font-Size="12" UniqueName="actions">
                                                                <ItemTemplate>
                                                                    <a id="aEditNote" onserverclick="BtnEdit_OnClick" title="Edit" runat="server">
                                                                        <i class="glyphicon glyphicon-edit"></i>
                                                                    </a>
                                                                    <a id="aDeleteNote" onserverclick="BtnDelete_OnClick" title="Delete" runat="server">
                                                                        <i class="glyphicon glyphicon-remove-circle"></i>
                                                                    </a>                                                      
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                <div class="col-md-3">
                                                </div>
                                                <div id="divNoResults" runat="server" visible="false" class="col-md-6 alert alert-warning" style="text-align:center; margin-top:30px;">
                                                    <strong>
                                                        <asp:Label ID="LblNoResultsTitle" Text="0 Invitations! " runat="server" />
                                                    </strong>
                                                    <asp:Label ID="LblNoResultsContent" Text="There are no Invitations yet, create your first one!" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                </div>                                       
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
    </telerik:RadAjaxPanel>
    <div id="AddInvitation" class="modal fade" tabindex="-1" data-width="500">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title"><asp:Label ID="LblInvitationTitle" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblInvitationSubject" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxSubject" MaxLength="145" EnableViewState="true" ViewStateMode="Enabled" CssClass="form-control" BorderStyle="None" placeholder="Enter subject here" data-placement="top" runat="server" />                                        
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="LblInvitationContent" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxMsg" MaxLength="3995" TextMode="MultiLine" Rows="10" CssClass="form-control" placeholder="Enter content here" data-placement="top" runat="server" />
                                    </div>
                                </div>                            
                            </div>                    
                        </div>                        
                        <div id="divInvitationFailure" runat="server" visible="false" class="col-md-12 alert alert-danger" style="text-align:center">
                            <strong>
                                <asp:Label ID="LblInvitationFailure" Text="Error! " runat="server" />
                            </strong>
                            <asp:Label ID="LblFailure" runat="server" />
                        </div>                        
                        <div class="modal-footer">   
                            <asp:Button ID="BtnDiscardInvitation" OnClick="BtnDiscardInvitation_OnClick" CssClass="btn btn-black" runat="server" Text="Discard" />
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnAddInvitation" OnClick="BtnAddInvitation_OnClick" CssClass="btn btn-success" runat="server" Text="OK" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="EditInvitation" class="modal fade" tabindex="-1" data-width="500">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title"><asp:Label ID="LblEditInvitationTitle" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblEditInvitationSubject" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxEditInvitationSubject" MaxLength="145" EnableViewState="true" ViewStateMode="Enabled" CssClass="form-control" BorderStyle="None" placeholder="Enter subject here" data-placement="top" runat="server" />
                                        <asp:TextBox ID="TbxEditInvitationId" CssClass="hidden" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="LblEditInvitationContent" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxEditInvitationContent" MaxLength="3995" TextMode="MultiLine" Rows="10" CssClass="form-control" placeholder="Enter content here" data-placement="top" runat="server" />
                                    </div>
                                </div>                            
                            </div>                    
                        </div>                        
                        <div id="divEditInvitationFailure" runat="server" visible="false" class="col-md-12 alert alert-danger" style="text-align:center">
                            <strong>
                                <asp:Label ID="LblEditInvitationError" Text="Error! " runat="server" />
                            </strong>
                            <asp:Label ID="LblEditInvitationFail" runat="server" />
                        </div>                        
                        <div class="modal-footer">
                            <asp:Button ID="BtnDiscardEditInvitation" OnClick="BtnDiscardEditInvitation_OnClick" CssClass="btn btn-black" runat="server" Text="Discard" />
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnEditInvitation" OnClick="BtnConfEdit_OnClick" CssClass="btn btn-success" runat="server" Text="OK" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title"><asp:Label ID="LblConfTitle" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblConfMsg" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxInvitationConfId" CssClass="hidden" runat="server" />
                                    </div>                                    
                                </div>                            
                            </div>                    
                        </div>
                        <div class="modal-footer">                            
                            <button type="button" data-dismiss="modal" class="btn btn-black">Back</button>
                            <asp:Button ID="BtnConfDelete" OnClick="BtnConfDelete_OnClick" CssClass="btn btn-danger" runat="server" Text="Delete" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function CloseInvitationPopUp() {
            $('#AddInvitation').modal('hide');
        }
    </script>
    <script type="text/javascript">
        function OpenNotePopUp() {
            $('#AddInvitation').modal('show');
        }
    </script>
    <script type="text/javascript">
        function CloseConfirmPopUp() {
            $('#divConfirm').modal('hide');
        }
    </script>
    <script type="text/javascript">
        function OpenConfirmPopUp() {
            $('#divConfirm').modal('show');
        }
    </script>
    <script type="text/javascript">
        function CloseEditPopUp() {
            $('#EditInvitation').modal('hide');
        }
    </script>
    <script type="text/javascript">
        function OpenEditPopUp() {
            $('#EditInvitation').modal('show');
        }
    </script>
</asp:Content>
