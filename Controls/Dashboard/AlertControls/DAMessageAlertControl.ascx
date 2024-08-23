<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DAMessageAlertControl.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.AlertControls.DAMessageAlertControl" %>

<div id="PnlMessage" runat="server" class="alert alert-custom alert-primary" role="alert" style="text-align: center; width: 100%;">
    <div class="alert-icon">
        <i id="Icon" runat="server" class="flaticon-warning"></i>
    </div>
    <div class="alert-text">
        <asp:Label ID="LblMessage" EnableViewState="false" runat="server" />
    </div>
    <div id="divClose" runat="server" class="alert-close">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true"><i class="ki ki-close"></i></span>
        </button>
    </div>
</div>
<div id="PnlRegisterMsg" runat="server" visible="false" class="alert alert-custom alert-white" role="alert">
    <div class="alert-icon"><i class="flaticon-warning"></i></div>
    <div class="alert-text">
        <asp:LinkButton ID="LnkBtnRegister" OnClick="LnkBtnRegister_OnClick" Text=" Register Now" runat="server" />
    </div>
</div>



