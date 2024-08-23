<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyData.ascx.cs" Inherits="WdS.ElioPlus.Controls.TermsPrivacy.MyData" %>

<div style="text-align: justify;">
    <p class="paragraph">
        <asp:Label ID="Label10" Text="Claim your company profile or request to remove your data from our platform." runat="server" />
    </p>
    <h4>
        <asp:Label ID="LblIntroduction" Text="Claim Your Company Profile" runat="server" />
    </h4>
    <p class="paragraph">
        <asp:Label ID="Label1" runat="server" />
        <a id="aSearch" runat="server">Search for my Company’s Profile
        </a>
    </p>
    <h4>
        <asp:Label ID="Label11" Text="Request Removal/Deletion from Elioplus (Opt-Out of Sale)" runat="server" />
    </h4>
    <p class="paragraph">
        <asp:Label ID="Label3" runat="server" />
        <a id="aContactUs" runat="server">Request Removal
        </a>
    </p>
</div>
