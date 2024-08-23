<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CancelServiceOrderConfirmationMessageAlert.ascx.cs" Inherits="WdS.ElioPlus.Controls.AlertControls.CancelServiceOrderConfirmationMessageAlert" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function isNumberOnly(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function CloseConfirmationServicePopUp() {
            $('#ConfirmationServiceModal').modal('hide');
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
    <div class="modal-dialog" id="wndConfirmation" style="width: 300px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 id="myModalLabel">
                    <asp:Label ID="LblMessageHeader" Text="Confirmation Message" runat="server" />
                </h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <i aria-hidden="true" class="ki ki-close"></i>
                </button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <form class="form-horizontal col-sm-12">
                        <div id="divInfo" runat="server" style="width: 100%;" class="alert alert-custom alert-light-info fade show">
                            <div class="alert-icon">
                                <i class="flaticon-warning"></i>
                            </div>
                            <div class="alert-text">
                                <strong>
                                    <asp:Label ID="LblMessage" Text="You are going to cancel your Premium Service Plan" runat="server" /><br />
                                    <br />
                                    <asp:Label ID="LblConfirm" Text="Are you sure?" runat="server" />
                                </strong>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="row">
                    <div id="divGeneralSuccess" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                        <div class="alert-icon">
                            <i class="flaticon-warning"></i>
                        </div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblGeneralSuccess" runat="server" /></strong><asp:Label ID="LblCancelSuccess"
                                    runat="server" />
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">
                                    <i class="ki ki-close"></i>
                                </span>
                            </button>
                        </div>
                    </div>
                    <div id="divGeneralFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                        <div class="alert-icon">
                            <i class="flaticon-warning"></i>
                        </div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblGeneralFailure" runat="server" /></strong><asp:Label ID="LblCancelFailure"
                                    runat="server" />
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">
                                    <i class="ki ki-close"></i>
                                </span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divFooter" class="modal-footer" runat="server">
                <asp:Button ID="BtnCancelMessage" Text="No" OnClick="BtnCancelMessage_OnClick" CssClass="btn btn-light-primary" runat="server" />
                <asp:Button ID="BtnProccedMessage" Text="Yes, Cancel Anyway" OnClick="BtnProccedMessage_OnClick" CssClass="btn btn-primary" runat="server" />
            </div>
        </div>
    </div>
</telerik:RadAjaxPanel>
