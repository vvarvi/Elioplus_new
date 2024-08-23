<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/EmptyMaster.Master" CodeBehind="TaskReminderSenderPage.aspx.cs" Inherits="WdS.ElioPlus.TaskReminderSenderPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="HomeHeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Elioplus is a business development platform that helps Software and SaaS companies that offer a partner program to find new Software and SaaS resellers" />
    <meta name="keywords" content="channel partners, partner program, software resellers, software vendors, reseller partner program, value added resellers, white label software, SaaS" />
</asp:Content>
<asp:Content ID="HomeMainContent" ContentPlaceHolderID="MainContent" runat="server">  
    <controls:MessageControl ID="UcMessageControl" Visible="false" runat="server" />
   <asp:Button ID="BtnElioHome" Text="Go to Elio" OnClick="BtnElioHome_OnClick" runat="server" />
</asp:Content>