<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardInfoLogs.aspx.cs" Inherits="WdS.ElioPlus.DashboardInfoLogs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top: 50px; text-align: center;">
        <h2>
            <asp:Label ID="LblViewInfoLogFiles" runat="server" />
        </h2>
        <asp:Panel ID="PnlViewInfoLogFiles" Style="margin-top: 20px; margin-left: 25px; padding: 10px;
            text-align: left; min-height: 700px;" runat="server">
            <asp:PlaceHolder ID="PhViewInfoLogFiles" runat="server" />
        </asp:Panel>
    </div>
</asp:Content>
