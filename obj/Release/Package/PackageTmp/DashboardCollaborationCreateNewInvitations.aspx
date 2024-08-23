<%@ Page Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardCollaborationCreateNewInvitations.aspx.cs" Inherits="WdS.ElioPlus.DashboardCollaborationCreateNewInvitations" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        function CloseInvitationPopUp() {
            $('#InvitationModal').modal('hide');
        }
        function disable() {
            var x = document.getElementById('<%= BtnProccedMessage.ClientID %>').disabled = true;

            console.log(x);
        }

        function BtnProccedMessage_OnClientClick(sender, e) {
            var y = $("#BtnProccedMessage.ClientID").click();
            var x = document.getElementById('<%= BtnProccedMessage.ClientID %>').disabled = true;

            console.log(y);
        }

    </script>
</telerik:RadScriptBlock>

<telerik:RadAjaxPanel ID="RapPage" runat="server" RestoreOriginalRenderDelegate="false">
    <script src="/assets/global/plugins/ckeditor/ckeditor.js" type="text/javascript"></script>
    <div class="modal-dialog" id="wndInvitation" style="width: 700px;">
        <div class="modal-content">
            <div class="modal-header" style="text-align: center;">
                <h3 id="myModalLabel">
                    <asp:Label ID="LblMessageHeader" Text="Invitation to partners" runat="server" />
                </h3>
            </div>
            <div class="modal-body">
                <div style="">
                    <form class="form-horizontal col-sm-12">                        
                        <asp:Panel ID="PnlMsg" runat="server">
                            <div class="form-group">
                                <asp:TextBox ID="TbxSubject" CssClass="form-control" style="width:93%; float:left;" placeholder="Subject" data-placement="top" runat="server" />
                                <asp:ImageButton ID="ImgBtnSave" OnClick="ImgBtnSave_OnClick" Visible="false" style="float:right;" ImageUrl="~/images/save.png" runat="server" />
                                <asp:Image ID="ImgStatus" AlternateText="status" Visible="false" runat="server" />
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                <textarea ID="TbxMsg" rows="5" class="ckeditor form-control" placeholder="Message" data-placement="top" name="editor1" runat="server"></textarea>
                                
                                </div>
                            </div>
                            <div id="divInvitations" class="form-group" style="overflow-y:scroll; height:220px;" runat="server">
                                <telerik:RadGrid ID="RdgMyInvitations" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="false" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" CssClass="rgdd" OnNeedDataSource="RdgMyInvitations_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                    <MasterTableView>
                                        <NoRecordsTemplate>
                                            <div class="emptyGridHolder">
                                                <asp:Literal ID="LtlNoDataFound" runat="server" />
                                            </div>
                                        </NoRecordsTemplate>
                                        <Columns>                                            
                                            <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" DataField="id" UniqueName="id" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" DataField="user_id" UniqueName="user_id" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="120" HeaderText="Subject" DataField="inv_subject" UniqueName="inv_subject" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="300" HeaderText="Message" DataField="inv_content" UniqueName="inv_content" />
                                            <telerik:GridTemplateColumn HeaderStyle-Width="10" HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnLoad" OnClick="ImgBtnLoad_OnClick" ImageUrl="~/Images/icons/small/load_to_edit.png" runat="server" />
                                                    <asp:ImageButton ID="ImgBtnDelete" OnClick="ImgBtnDelete_OnClick"  ImageUrl="~/images/icons/small/delete.png" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>                                
                            </div>
                            <controls:messagecontrol id="UcMessageAlert" visible="false" runat="server" /> 
                        </asp:Panel>
                    </form>
                </div>
                <div id="divGeneralSuccess" runat="server" visible="false" class="alert alert-success" style="text-align: justify; margin-top:0px; margin-bottom:-11px;">
                    <strong>
                        <asp:Label ID="LblGeneralSuccess" Text="Done!" runat="server" />
                    </strong>
                    <asp:Label ID="LblSuccess" runat="server" />
                </div>
                <div id="divGeneralFailure" runat="server" visible="false" class="alert alert-danger" style="text-align: justify; margin-top:0px; margin-bottom:-11px;">
                    <strong>
                        <asp:Label ID="LblGeneralFailure" Text="Error!" runat="server" />
                    </strong>
                    <asp:Label ID="LblFailure" runat="server" />
                </div>
            </div>
            <div id="divFooter" class="modal-footer" runat="server">
                <asp:Button ID="BtnCancelMessage" OnClick="BtnCancelMessage_OnClick" Text="Cancel" CssClass="btn btn-success" runat="server" />
                <asp:Button ID="BtnClear" OnClick="BtnClear_OnClick" Text="Clear" CssClass="btn btn-error" runat="server" />
                <asp:Button ID="BtnProccedMessage" OnClick="BtnProccedMessage_OnClick" Text="Send Invitation" CssClass="btn btn-success" runat="server" />
            </div>
        </div>
    </div>
</telerik:RadAjaxPanel>
