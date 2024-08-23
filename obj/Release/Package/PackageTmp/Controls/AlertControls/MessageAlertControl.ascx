<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageAlertControl.ascx.cs" Inherits="WdS.ElioPlus.Controls.AlertControls.MessageAlertControl" %>

<div id="PnlMessage" runat="server" class="card bg-blue text-white gap-15px" style="width: 100%;">
    <div class="flex items-center gap-5px">
        <img id="Icon" runat="server" src="/assets_out/images/alerts/info-white.svg" alt="Quick Actions Icon" title="Click to toggle the quick actions menu" width="20" height="20" />
        <h3 id="titleMsg" runat="server" class="font-bold text-base text-white">
            <asp:Label ID="LblTitle" EnableViewState="false" runat="server" />
        </h3>
    </div>
    <p id="contentMsg" runat="server" class="text-sm text-white">
        <asp:Label ID="LblMessage" EnableViewState="false" runat="server" />
    </p>
</div>



