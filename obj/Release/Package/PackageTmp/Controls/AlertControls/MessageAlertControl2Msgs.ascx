<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageAlertControl2Msgs.ascx.cs" Inherits="WdS.ElioPlus.Controls.AlertControls.MessageAlertControl2Msgs" %>

<div id="PnlMessage" runat="server" class="alert alert-custom alert-primary" role="alert" style="text-align: center; width: 100%;">
    <div class="flex items-center gap-5px">
        <img id="Icon" runat="server" src="/assets_out/images/alerts/info-white.svg" alt="Quick Actions Icon" title="Click to toggle the quick actions menu" width="20" height="20" />
        <h3 id="titleMsg" runat="server" class="font-bold text-base text-white">
            <asp:Label ID="LblTitle" EnableViewState="false" runat="server" />
        </h3>
    </div>
    <p id="contentMsg" runat="server" class="text-sm text-white">
        <asp:Label ID="LblMessage" EnableViewState="false" runat="server" />
    </p>
    <p id="contentMsg2" runat="server" class="text-sm text-white">
        <asp:Label ID="LblMessage2" EnableViewState="false" runat="server" />
    </p>
</div>



