<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Stripe_Ctrl.ascx.cs" Inherits="WdS.ElioPlus.Controls.Payment.Stripe_Ctrl" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function isNumberOnly(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }
            function ClosePaymentPopUp() {
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
    <div class="modal-dialog" id="wndPayment" style="width:300px;">
        <div class="modal-content" style="display:inline-block;">
            <div class="modal-header">
                <asp:Button ID="BtnCancelPayment" OnClick="BtnCancelPayment_OnClick" Text="X" CssClass="close" aria-hidden="true" runat="server" />               
                <h3 id="myModalLabel">
                    <asp:Label ID="LblMessageHeader" runat="server" />
                </h3>
            </div>
            <div class="modal-body">
                <div style="">
                    <div id="divInfo" runat="server" class="alert alert-info" style="text-align:center;">
                        <strong><asp:Label ID="LblMessage" Text="You will not be charged during your 14-day trial. Cancel anytime." runat="server" /></strong>
                    </div>
                    <div style="font-size:13px; color:#5cb85c; display:inline-block; padding:10px 0 10px 0; float:left;">
                        <asp:Label ID="LblTotalCost" runat="server" />
                    </div>
                    <div style="font-weight:bold;font-size:15px; float:right; background-color:#5cb85c; color:#fff; padding:5px; border:1px solid #cccccc; border-radius:5px; width:70px; text-align:center; vertical-align:middle;">
                        <asp:Label ID="LblTotalCostValue" runat="server" />
                    </div>            
                    <form class="form-horizontal col-sm-12">
                        <asp:Panel ID="PnlPayment" runat="server">                            
                            <div class="form-group">
                                <asp:Label ID="LblEmail" runat="server" />
                                <asp:TextBox ID="TbxEmail" CssClass="form-control" MaxLength="100" placeholder="Email" data-placement="top" data-trigger="manual" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblCardNumber" runat="server" />
                                <asp:TextBox ID="TbxCardNumber" onkeypress="return isNumberOnly(event);" MaxLength="20" CssClass="form-control" placeholder="Card Number" data-placement="top" data-trigger="manual" runat="server" />
                            </div>
                            <div class="form-group" style="float:left; padding-right:7px;">
                                <asp:Label ID="LblExpMonth" runat="server" />
                                <asp:DropDownList ID="DrpExpMonth" CssClass="form-control" placeholder="MM" data-placement="top" data-trigger="manual" runat="server">
                                    <asp:ListItem Text="MM" Selected="True" Value="0" />
                                    <asp:ListItem Text="01" Value="1" /><asp:ListItem Text="02" Value="2" /><asp:ListItem Text="03" Value="3" />
                                    <asp:ListItem Text="04" Value="4" /><asp:ListItem Text="05" Value="5" /><asp:ListItem Text="06" Value="6" />
                                    <asp:ListItem Text="07" Value="7" /><asp:ListItem Text="08" Value="8" /><asp:ListItem Text="09" Value="9" />
                                    <asp:ListItem Text="10" Value="10" /><asp:ListItem Text="11" Value="11" /><asp:ListItem Text="12" Value="12" />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" style="float:left;">
                                <asp:Label ID="LblExpYear" runat="server" />
                                <asp:TextBox ID="TbxExpYear" Width="70" onkeypress="return isNumberOnly(event);" MaxLength="2" CssClass="form-control" placeholder="YY" data-placement="top" data-trigger="manual" runat="server" />
                            </div>
                            <div class="form-group" style="float:right;">
                                <asp:Label ID="LblCVC" runat="server" /><asp:TextBox ID="TbxCVC" Width="80" onkeypress="return isNumberOnly(event);" MaxLength="5" CssClass="form-control" placeholder="CVC" data-placement="top" data-trigger="manual" runat="server" />
                            </div>
                            <div class="form-group" style="float:left; color:#cccccc; font-weight:normal; font-size:15px;">
                                <asp:CheckBox ID="CbxCouponDiscount" AutoPostBack="true" OnCheckedChanged="CbxCouponDiscount_OnCheckedChanged" Text="I have discount coupon" runat="server" />
                            </div>
                            <div id="divDiscount" class="form-group" style="float:left; color:#cccccc; font-weight:normal; font-size:15px; display:inline-block; width:268px;" runat="server" visible="false">
                                <asp:TextBox ID="TbxDiscount" CssClass="form-control" MaxLength="100" placeholder="Coupon ID" data-placement="top" data-trigger="manual" runat="server" />
                            </div>
                        </asp:Panel>
                    </form>
                </div>
            </div>
            <div class="modal-footer" style="display:inline-block; float:right; width:100% !important;">               
                <asp:Button ID="BtnBottomCancelPayment" OnClick="BtnCancelPayment_OnClick" Text="Cancel" CssClass="btn btn-info" aria-hidden="true" runat="server" />
                    &nbsp;
                <asp:Button ID="BtnPayment" Text="Subscribe" OnClick="BtnPayment_OnClick" OnClientClick="disable()" CssClass="btn btn-success" runat="server" /> 
            </div>
            <div class="modal-footer" style="display:inline-block;width:100% !important;">
                <div id="divPaymentWarningMsg" runat="server" visible="false" class="alert alert-danger">
                    <strong>
                        <asp:Label ID="LblPaymentWarningMsg" runat="server" />
                    </strong>
                    <asp:Label ID="LblPaymentWarningMsgContent" runat="server" />
                </div>
                <div id="divPaymentSuccessMsg" runat="server" visible="false" class="alert alert-success">
                    <strong>
                        <asp:Label ID="LblPaymentSuccessMsg" runat="server" />
                    </strong>
                    <asp:Label ID="LblPaymentSuccessMsgContent" runat="server" />
                </div>
            </div>
        </div>
    </div>

     <div id="loader" style="display:none;">
        <div id="loadermsg"></div>
    </div> 
</telerik:RadAjaxPanel>