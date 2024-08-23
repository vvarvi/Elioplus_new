<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DAInfoMessageControl.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.AlertControls.DAInfoMessageControl" %>

<asp:Panel ID="PnlMessage" style="text-align:right;" EnableViewState="false" runat="server">    
    <asp:ImageButton ID="ImgBtnIcon" EnableViewState="false" runat="server" />    
    <telerik:RadToolTip ID="RttMessage" TargetControlID="ImgBtnIcon" Position="TopRight" Animation="Fade" runat="server" />       
    <asp:Label ID="LblMessage" style="margin-top:-5px;" EnableViewState="false" runat="server" />
    <asp:LinkButton ID="LnkBtnRegister" Visible="false" OnClick="LnkBtnRegister_OnClick" Text=" Register Now" runat="server" />       
</asp:Panel> 