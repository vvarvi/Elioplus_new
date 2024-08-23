<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddTask.ascx.cs" Inherits="WdS.ElioPlus.Controls.Modals.AddTask" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
       
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

    <div class="modal-dialog" id="wndInvitation" style="width: 700px;">
        <div class="modal-content">
            <div class="modal-header" style="text-align: center;">
                <h3 id="myModalLabel">
                    <asp:Label ID="LblMessageHeader" Text="Add task to this opportunity!" runat="server" />
                </h3>
            </div>
            <div class="modal-body">
                <div style="height: 250px;">
                    <form class="form-horizontal col-sm-12">                        
                        <asp:Panel ID="PnlMsg" runat="server">                            
                            <div class="form-group">
                                <asp:TextBox ID="TbxSubject" CssClass="form-control" BorderStyle="None" placeholder="Subject" data-placement="top" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="TbxMsg" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Note" data-placement="top" runat="server" />
                            </div>  
                            <div class="form-group">
                                <asp:Label ID="LblRemindDate" Text="Remind Date" runat="server" />
                            </div>
                            <div class="form-group">
                                <telerik:RadDatePicker ID="RdpRemindDate" Skin="MetroTouch" runat="server" />
                            </div>                         
                        </asp:Panel>
                    </form>
                </div>
                <div id="divGeneralSuccess" runat="server" visible="false" class="alert alert-success" style="text-align: justify; margin-top:0px; margin-bottom:-11px;">
                    <strong>
                        <asp:Label ID="LblGeneralSuccess" Text="Done" runat="server" />
                    </strong>
                    <asp:Label ID="LblSuccess" runat="server" />
                </div>
                <div id="divGeneralFailure" runat="server" visible="false" class="alert alert-danger" style="text-align: justify; margin-top:0px; margin-bottom:-11px;">
                    <strong>
                        <asp:Label ID="LblGeneralFailure" Text="Error" runat="server" />
                    </strong>
                    <asp:Label ID="LblFailure" runat="server" />
                </div>
            </div>
            <div id="divFooter" class="modal-footer" runat="server">
                <asp:Button ID="BtnCancelMessage" OnClick="BtnCancelMessage_OnClick" Text="Clear" CssClass="btn btn-success" runat="server" />
                <asp:Button ID="BtnProccedMessage" OnClick="BtnProccedMessage_OnClick" Text="Add Task" CssClass="btn btn-success" runat="server" />
                <asp:Button ID="BtnBack" OnClick="BtnBack_OnClick" Text="Back to Opportunities" CssClass="btn btn-success" runat="server" />
            </div>
        </div>
    </div>
