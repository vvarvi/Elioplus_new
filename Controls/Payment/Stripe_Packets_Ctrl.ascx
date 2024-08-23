<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Stripe_Packets_Ctrl.ascx.cs" Inherits="WdS.ElioPlus.Controls.Payment.Stripe_Packets_Ctrl" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function isNumberOnly(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function OpenPaymentModal() {
            $('#PaymentPacketsModal').modal('show');
        }
        function ClosePaymentModal() {
            $('#PaymentModal').modal('hide');
        }

        function RapPage_OnRequestStart(sender, args) {
            $('#loader').show();
            document.getElementById('<%= BtnCheckout.ClientID %>').disabled = true;
        }
        function endsWith(s) {
            return this.length >= s.length && this.substr(this.length - s.length) == s;
        }
        function RapPage_OnResponseEnd(sender, args) {
            $('#loader').hide();
        }
        function disable() {
            document.getElementById('<%= BtnCheckout.ClientID %>').disabled = true;
        }
    </script>
</telerik:RadScriptBlock>

<telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">
    <div class="modal-dialog" id="wndPayment" style="width: 300px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 id="myModalLabel">
                    <asp:Label ID="LblMessageHeader" Text="Select your plan" runat="server" />
                </h4>
                <asp:Button ID="BtnCancelPayment" OnClick="BtnCancelPayment_OnClick" Text="X" CssClass="ki ki-close" aria-hidden="true" runat="server" />
            </div>
            <div class="modal-body">
                <div class="row">                    
                    <div class="form-group">
                        <asp:DropDownList AutoPostBack="true" ID="DrpStripePlans" Width="280" OnSelectedIndexChanged="DrpStripePlans_OnSelectedIndexChanged" CssClass="form-control" placeholder="Select plan" data-placement="top" data-trigger="manual" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <div style="font-size: 13px; color: #3699FF; display: inline-block; padding: 10px 0 10px 0; float: left;">
                            <asp:Label ID="LblTotalCost" runat="server" />
                        </div>
                    </div>
                    <div style="font-weight: bold; font-size: 15px; float: right; background-color: #3699FF; color: #fff; padding: 5px; border: 1px solid #3699FF; border-radius: 5px; width: 90px; height: 35px; margin-left: 25px; text-align: center; vertical-align: middle;">
                        <asp:Label ID="LblTotalCostValue" runat="server" />
                    </div>                    
                </div>
                <div class="row">
                    <div id="divPaymentWarningMsg" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                        <div class="alert-icon">
                            <i class="flaticon-questions-circular-button"></i>
                        </div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblPaymentWarningMsg" runat="server" />
                            </strong>
                            <asp:Label ID="LblPaymentWarningMsgContent" runat="server" />
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">
                                    <i class="ki ki-close"></i>
                                </span>
                            </button>
                        </div>
                    </div>
                    <div id="divPaymentSuccessMsg" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                        <div class="alert-icon">
                            <i class="flaticon-questions-circular-button"></i>
                        </div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblPaymentSuccessMsg" runat="server" />
                            </strong>
                            <asp:Label ID="LblPaymentSuccessMsgContent" runat="server" />
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
            <div class="modal-footer">
                <asp:Button ID="BtnBottomCancelPayment" OnClick="BtnCancelPayment_OnClick" Text="Cancel" CssClass="btn btn-light-primary" aria-hidden="true" runat="server" />
                <a id="BtnCheckout" runat="server" onserverclick="aCheckout_ServerClick" class="btn btn-primary">
                    Checkout
                </a>
            </div>
        </div>
    </div>
</telerik:RadAjaxPanel>
