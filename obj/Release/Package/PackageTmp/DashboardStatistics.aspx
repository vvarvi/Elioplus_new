<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardStatistics.aspx.cs" Inherits="WdS.ElioPlus.DashboardStatistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top: 50px; text-align: center;">
        <h2>
            <asp:Label ID="LblUserRegistrationStatistics" runat="server" />
        </h2>
        <asp:Panel ID="PnlUserRegistrationStatistics" Style="margin-top: 20px; margin-left: 25px;
            padding: 10px; text-align: left;" runat="server">
            <asp:PlaceHolder ID="PhStatistics" runat="server" />
        </asp:Panel>
    </div>
</asp:Content>
