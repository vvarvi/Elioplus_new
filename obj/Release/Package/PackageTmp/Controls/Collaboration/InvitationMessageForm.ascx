<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvitationMessageForm.ascx.cs" Inherits="WdS.ElioPlus.Controls.Collaboration.InvitationMessageForm" %>

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
    <div class="modal-dialog" id="wndInvitation" style="width: 700px;">
        <div class="modal-content">
            <div class="modal-header" style="text-align: center;">
                <h3 id="myModalLabel">
                    <asp:Label ID="LblMessageHeader" Text="Invite your partners!" runat="server" />
                </h3>
            </div>
            <div class="modal-body">
                <div style="height: 400px;">
                    <form class="form-horizontal col-sm-12">                        
                        <asp:Panel ID="PnlMsg" runat="server">                            
                            <div class="form-group">
                                <asp:TextBox ID="TbxSubject" CssClass="form-control" BorderStyle="None" placeholder="Subject" data-placement="top" data-trigger="manual" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="TbxMsg" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Message" data-placement="top" data-trigger="manual" runat="server" />
                            </div>                            
                            <div class="form-group">
                                <asp:TextBox ID="TbxEmail" BorderStyle="None" CssClass="form-control" Width="300" placeholder="Email" data-placement="top" data-trigger="manual" runat="server" />
                            </div>
                            <%--<div class="form-group">
                                <asp:CheckBox ID="CbxSaveTemplate" Text="Save message" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="BtnExistingUser" Text="Invite Existing User" CssClass="btn btn-success" runat="server" />
                            </div>--%>
                            <div class="form-group" style="overflow-y:scroll; height:180px;">
                                <telerik:RadGrid ID="RdgUsers" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="false" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" CssClass="rgdd" OnNeedDataSource="RdgUsers_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                    <MasterTableView>
                                        <NoRecordsTemplate>
                                            <div class="emptyGridHolder">
                                                <asp:Literal ID="LtlNoDataFound" runat="server" />
                                            </div>
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="20" DataField="select" UniqueName="select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CbxSelectUser" AutoPostBack="true" OnCheckedChanged="CbxSelectUser_OnCheckedChanged" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Id" Display="false" DataField="id" UniqueName="id" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Comapany Name" DataField="company_name" UniqueName="company_name" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Email" DataField="email" UniqueName="email" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <asp:CheckBoxList ID="CbxExistingUsers" Visible="false" AutoPostBack="true" OnTextChanged="CbxExistingUsers_OnTextChanged" runat="server" />
                            </div>                           
                        </asp:Panel>
                    </form>
                </div>
                <div id="divGeneralSuccess" runat="server" visible="false" class="alert alert-success"
                    style="text-align: justify;">
                    <strong>
                        <asp:Label ID="LblGeneralSuccess" runat="server" />
                    </strong>
                    <asp:Label ID="LblSuccess" runat="server" />
                </div>
                <div id="divGeneralFailure" runat="server" visible="false" class="alert alert-danger"
                    style="text-align: justify;">
                    <strong>
                        <asp:Label ID="LblGeneralFailure" runat="server" />
                    </strong>
                    <asp:Label ID="LblFailure" runat="server" />
                </div>
            </div>
            <div id="divFooter" class="modal-footer" runat="server">
                <asp:Button ID="BtnCancelMessage" OnClick="BtnCancelMessage_OnClick" Text="Cancel" CssClass="btn btn-success" runat="server" />
                <asp:Button ID="BtnProccedMessage" OnClick="BtnProccedMessage_OnClick" Text="Send" CssClass="btn btn-success" runat="server" />
            </div>
        </div>
    </div>
</telerik:RadAjaxPanel>
