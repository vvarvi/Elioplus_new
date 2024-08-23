<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeatureCompanies.ascx.cs" Inherits="WdS.ElioPlus.Controls.FeatureCompanies" %>

<telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>

<div class="container" style="width:100%;">
	<div class="header-css" align="center">
        <h2><asp:Label ID="LblPartners" runat="server" ForeColor="White" /></h2>
    </div>
    <div align="center">
	<ul>
        <li>
            <asp:Image ID="Img1" AlternateText="logo1" runat="server" Width="150px" Height="100px" />
            <asp:HiddenField ID="HdnCompany1" runat="server" />
        </li>
        <li>
            <asp:Image ID="Img2" AlternateText="logo2" runat="server" Width="150px" Height="100px" />
            <asp:HiddenField ID="HdnCompany2" runat="server" />
        </li>
        <li>
            <asp:Image ID="Img3" AlternateText="logo3" runat="server" Width="150px" Height="100px" />
            <asp:HiddenField ID="HdnCompany3" runat="server" />
        </li>
        <li>
            <asp:Image ID="Img4" AlternateText="logo4" runat="server" Width="150px" Height="100px" />
            <asp:HiddenField ID="HdnCompany4" runat="server" />
        </li>
        <li>
            <asp:Image ID="Img5" AlternateText="logo5" runat="server" Width="150px" Height="100px" />
            <asp:HiddenField ID="HdnCompany5" runat="server" />
        </li>
        <li>
            <asp:Image ID="Img6" AlternateText="logo6" runat="server" Width="150px" Height="100px" />
            <asp:HiddenField ID="HdnCompany6" runat="server" />
        </li>
	</ul>	            
    </div>
</div>