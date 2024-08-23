<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Stripe_Packets_Ctrl_Old.ascx.cs" Inherits="WdS.ElioPlus.Controls.Payment.Stripe_Packets_Ctrl_Old" %>

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
            document.getElementById('<%= BtnPayment.ClientID %>').disabled = true;
        }
        function endsWith(s) {
            return this.length >= s.length && this.substr(this.length - s.length) == s;
        }
        function RapPage_OnResponseEnd(sender, args) {
            $('#loader').hide();
        }
        function disable() {
            document.getElementById('<%= BtnPayment.ClientID %>').disabled = true;
        }
        function Enable() {
            $("#BtnPayment").attr('disabled', false);
        }
        function Disable() {
            $("#BtnPayment").attr('disabled', true);
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
                    <div id="divInfo" visible="false" runat="server" style="width: 100%;" class="alert alert-custom alert-light-info fade show" role="alert">
                        <div class="alert-icon">
                            <i class="flaticon-questions-circular-button"></i>
                        </div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblMessage" Text="You will not be charged during your 14-day trial. Cancel anytime." runat="server" /></strong>
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">
                                    <i class="ki ki-close"></i>
                                </span>
                            </button>
                        </div>
                    </div>
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
                    <form class="form-horizontal col-sm-12">
                        <asp:Panel ID="PnlPayment" runat="server">
                            <div class="form-group">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="flaticon2-email"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="TbxEmail" CssClass="form-control" MaxLength="100" placeholder="Email" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="flaticon-price-tag"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="TbxCardNumber" onkeypress="return isNumberOnly(event);" MaxLength="20" CssClass="form-control" name="creditcard" placeholder="Enter card number" runat="server" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-4">
                                    <asp:DropDownList ID="DrpExpMonth" CssClass="form-control" placeholder="MM" data-placement="top" data-trigger="manual" runat="server">
                                        <asp:ListItem Text="MM" Selected="True" Value="0" />
                                        <asp:ListItem Text="01" Value="1" />
                                        <asp:ListItem Text="02" Value="2" />
                                        <asp:ListItem Text="03" Value="3" />
                                        <asp:ListItem Text="04" Value="4" />
                                        <asp:ListItem Text="05" Value="5" />
                                        <asp:ListItem Text="06" Value="6" />
                                        <asp:ListItem Text="07" Value="7" />
                                        <asp:ListItem Text="08" Value="8" />
                                        <asp:ListItem Text="09" Value="9" />
                                        <asp:ListItem Text="10" Value="10" />
                                        <asp:ListItem Text="11" Value="11" />
                                        <asp:ListItem Text="12" Value="12" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="TbxExpYear" onkeypress="return isNumberOnly(event);" MaxLength="2" CssClass="form-control" placeholder="YY" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="TbxCVC" onkeypress="return isNumberOnly(event);" MaxLength="5" CssClass="form-control" placeholder="CVC" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:CheckBox ID="CbxCouponDiscount" AutoPostBack="true" OnCheckedChanged="CbxCouponDiscount_OnCheckedChanged" Text="I have discount coupon" runat="server" />
                            </div>
                            <div id="divDiscount" class="form-group" runat="server" visible="false">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="flaticon2-browser"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="TbxDiscount" CssClass="form-control" MaxLength="100" placeholder="Coupon ID" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                            </div>
                        </asp:Panel>
                    </form>
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
                <asp:Button ID="BtnPayment" Text="Subscribe" OnClick="BtnPayment_OnClick" OnClientClick="disable()" CssClass="btn btn-primary" runat="server" />
            </div>
        </div>
    </div>
</telerik:RadAjaxPanel>
