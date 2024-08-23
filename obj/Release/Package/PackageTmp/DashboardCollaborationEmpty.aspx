<%@ Page Title="" Language="C#" MasterPageFile="~/CollaborationDashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardCollaborationEmpty.aspx.cs" Inherits="WdS.ElioPlus.DashboardCollaborationEmpty" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/MailBox/ChatGrids/uc_RdgMailBoxList.ascx" TagName="UcRdgMailBoxList" TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager ID="masterRadAjaxManager" runat="server" RestoreOriginalRenderDelegate="false">
    </telerik:RadAjaxManager>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
