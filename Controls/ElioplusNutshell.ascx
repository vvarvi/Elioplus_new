<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ElioplusNutshell.ascx.cs" Inherits="WdS.ElioPlus.Controls.ElioplusNutshell" %>

<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<div class="container">
    <h2 class="title"><asp:Label ID="LblElioplusNutsell" runat="server" /></h2>
    <p class="intro">
        <asp:Label ID="LblNutshell" runat="server" />
        <a id="aLearnMore" runat="server"><asp:Label ID="LblMore" runat="server" /></a>
    </p>
    <p>
        <a id="aGetElioNow" runat="server" visible="true" class="btn btn-cta btn-cta-primary"><asp:Label ID="LblGetElioNow" runat="server" /></a>
        <asp:Button ID="BtnGetElioNow" Visible="false" OnClick="BtnSearchGoPremium_OnClick"	CssClass="btn btn-lg btn-cta-primary" runat="server" />
    </p>
</div>

<!--//container-->
<!-- Payment form (modal view) -->
<div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <controls:UcStripe id="UcStripe" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>