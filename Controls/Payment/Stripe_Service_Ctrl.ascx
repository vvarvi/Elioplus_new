<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Stripe_Service_Ctrl.ascx.cs" Inherits="WdS.ElioPlus.Controls.Payment.Stripe_Service_Ctrl" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function isNumberOnly(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function ClosePaymentServicePopUp() {
            $('#PaymentServiceModal').modal('hide');
        }

        function RapPage_OnRequestStart(sender, args) {

            document.getElementById('<%= BtnCheckout.ClientID %>').disabled = true;
        }
        function endsWith(s) {
            return this.length >= s.length && this.substr(this.length - s.length) == s;
        }
        function RapPage_OnResponseEnd(sender, args) {

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
                    <asp:Label ID="LblMessageHeader" runat="server" />
                </h4>
                <asp:Button ID="BtnCancelPayment" OnClick="BtnCancelPayment_OnClick" Text="X" CssClass="ki ki-close" aria-hidden="true" runat="server" />
            </div>
            <div class="modal-body">
                <div class="row">
                    <div style="font-size: 13px; color: #5cb85c; display: inline-block; padding: 10px 5px 10px 0; float: left;">
                        <asp:Label ID="LblTotalCost" runat="server" />
                    </div>
                    <div style="font-weight: bold; font-size: 15px; float: right; background-color: #5cb85c; color: #fff; padding: 5px; border: 1px solid #cccccc; border-radius: 5px; width: 80px; text-align: center; vertical-align: middle; margin-bottom: 10px;">
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
                &nbsp;
                <a id="BtnCheckout" runat="server" onserverclick="aCheckout_ServerClick" class="btn btn-primary">Checkout
                </a>
            </div>
        </div>
    </div>

</telerik:RadAjaxPanel>
