<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Stripe_CreditCard_Ctrl.ascx.cs" Inherits="WdS.ElioPlus.Controls.Payment.Stripe_CreditCard_Ctrl" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function isNumberOnly(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function CloseCCNumberPopUp() {
            $('#PaymentModal').modal('hide');
        }

        function RapPage_OnRequestStart(sender, args) {
            $('#loader').show();
            document.getElementById('<%= BtnCCUpdate.ClientID %>').disabled = true;
        }
        function endsWith(s) {
            return this.length >= s.length && this.substr(this.length - s.length) == s;
        }
        function RapPage_OnResponseEnd(sender, args) {
            $('#loader').hide();
        }
        function disable() {
            document.getElementById('<%= BtnCCUpdate.ClientID %>').disabled = true;
        }
        function Enable() {
            $("#BtnCCUpdate").attr('disabled', false);
        }
        function Disable() {
            $("#BtnCCUpdate").attr('disabled', true);
        }
    </script>
</telerik:RadScriptBlock>

<telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">
    <div class="modal-dialog" id="wndCCNumber" style="width: 300px;">
        <div class="modal-content" style="display: inline-block;">
            <div class="modal-header">
                <asp:Button ID="BtnCCUpdateClose" OnClick="BtnCCUpdateCancel_OnClick" Text="X" CssClass="close" aria-hidden="true" runat="server" />
                <h3 id="myModalLabel">
                    <asp:Label ID="LblMessageHeader" runat="server" />
                </h3>
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
                    <form class="form-horizontal col-sm-12">
                        <asp:Panel ID="PnlCCNumberUpdate" runat="server">
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
                        </asp:Panel>
                    </form>
                </div>
                <div class="row">
                    <div id="divCCWarningMsg" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                        <div class="alert-icon">
                            <i class="flaticon-questions-circular-button"></i>
                        </div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblCCWarningMsg" runat="server" />
                            </strong>
                            <asp:Label ID="LblCCWarningMsgContent" runat="server" />
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">
                                    <i class="ki ki-close"></i>
                                </span>
                            </button>
                        </div>
                    </div>
                    <div id="divCCSuccessMsg" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                        <div class="alert-icon">
                            <i class="flaticon-questions-circular-button"></i>
                        </div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblCCSuccessMsg" runat="server" />
                            </strong>
                            <asp:Label ID="LblCCSuccessMsgContent" runat="server" />
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
                <asp:Button ID="BtnCCUpdateCancel" Text="Cancel" CssClass="btn btn-light-primary" aria-hidden="true" runat="server" />
                &nbsp;
                <asp:Button ID="BtnCCUpdate" Text="Subscribe" OnClick="BtnCCUpdate_OnClick" OnClientClick="disable()" CssClass="btn btn-primary" runat="server" />
            </div>
        </div>
    </div>

    <div id="loader" style="display: none;">
        <div id="loadermsg"></div>
    </div>
</telerik:RadAjaxPanel>
