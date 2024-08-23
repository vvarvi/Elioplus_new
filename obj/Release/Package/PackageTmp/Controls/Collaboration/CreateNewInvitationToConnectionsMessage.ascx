<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateNewInvitationToConnectionsMessage.ascx.cs" Inherits="WdS.ElioPlus.Controls.Collaboration.CreateNewInvitationToConnectionsMessage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        function CloseConnectionsInvitationPopUp() {
            $('#ConnectionsInvitationModal').modal('hide');
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
    <div role="document" class="modal-dialog" id="wndInvitation" style="width: 850px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 id="myModalLabel">
                    <asp:Label ID="LblMessageHeader" Text="Invitation to your connections" runat="server" />
                </h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <i aria-hidden="true" class="ki ki-close"></i>
                </button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <form class="form-horizontal col-sm-12">
                        <asp:Panel ID="PnlMsg" runat="server">
                            <div class="form-group">
                                <asp:TextBox ID="TbxSubject" CssClass="form-control" BorderStyle="None" Style="width: 94%; float: left;" placeholder="Subject" data-placement="top" runat="server" />
                                <asp:Image ID="ImgStatus" AlternateText="status" Visible="false" runat="server" />
                                <asp:ImageButton ID="ImgBtnEdit" OnClick="ImgBtnEdit_OnClick" Visible="false" Style="float: right;" ImageUrl="~/images/edit.png" runat="server" />
                                <asp:ImageButton ID="ImgBtnCancel" OnClick="ImgBtnCancel_OnClick" Visible="false" Style="float: right;" ImageUrl="~/images/cancel.png" runat="server" />
                                <asp:ImageButton ID="ImgBtnSave" OnClick="ImgBtnSave_OnClick" Visible="false" Style="float: right;" ImageUrl="~/images/save.png" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="TbxMsg" Rows="5" CssClass="form-control" placeholder="Message" data-placement="top" TextMode="MultiLine" runat="server" />
                            </div>
                            <div id="divInvitations" class="form-group" style="overflow-y: scroll; height: 220px;" runat="server">

                                <telerik:RadGrid ID="RdgMyInvitations" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="false" PagerStyle-Position="Bottom" PageSize="10" Width="100%" CssClass="rgdd" OnNeedDataSource="RdgMyInvitations_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
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
                                                    <asp:ImageButton ID="ImgBtnDelete" OnClick="ImgBtnDelete_OnClick" ImageUrl="~/images/icons/small/delete.png" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <controls:MessageControl ID="UcMessageAlert" runat="server" />
                        </asp:Panel>
                    </form>
                </div>
                <div class="row" style="margin-top: 25px;">
                    <div id="divGeneralSuccess" runat="server" visible="false" class="alert alert-custom alert-light-success fade show" style="text-align: justify; width: 100%;">
                        <div class="alert-icon"><i class="flaticon-warning"></i></div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblGeneralSuccess" Text="Done!" runat="server" />
                            </strong>
                            <asp:Label ID="LblSuccess" runat="server" />
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                            </button>
                        </div>
                    </div>
                    <div id="divGeneralFailure" runat="server" visible="false" class="alert alert-custom alert-light-danger fade show" style="text-align: justify; width: 100%;">
                        <div class="alert-icon"><i class="flaticon-warning"></i></div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblGeneralFailure" Text="Error!" runat="server" />
                            </strong>
                            <asp:Label ID="LblFailure" runat="server" />
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divFooter" class="modal-footer" runat="server">
                <asp:Button ID="BtnCancelMessage" OnClick="BtnCancelMessage_OnClick" Text="Cancel" CssClass="btn btn-danger" runat="server" />
                <asp:Button ID="BtnClear" OnClick="BtnClear_OnClick" Text="Clear" CssClass="btn btn-light-primary" runat="server" />
                <asp:Button ID="BtnProccedMessage" OnClick="BtnProccedMessage_OnClick" Text="Send Invitation" CssClass="btn btn-primary" runat="server" />
            </div>
        </div>
    </div>
</telerik:RadAjaxPanel>
