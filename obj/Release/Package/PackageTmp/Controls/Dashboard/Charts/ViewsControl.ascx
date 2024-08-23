<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewsControl.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.Charts.ViewsControl" %>

<%@ Register Assembly="Libero.FusionCharts" Namespace="Libero.FusionCharts.Control" TagPrefix="fcl" %>
<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<div style="padding-left:210px; padding-top:150px;">
    <h2><asp:label ID="Label2" style="margin-left:90px; padding:5px;" runat="server" /></h2>
</div>
<controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />                              
<fcl:FChart ID = "ViewsChart" runat = "server" Width = "950" Height = "300"/>