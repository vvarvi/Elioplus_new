<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopularVendor.ascx.cs" Inherits="WdS.ElioPlus.Controls.Categories.Vendors.PopularVendor" %>
<div class="header-css">
    <h2><asp:Label ID="LblPopularVendor" Text="Most Popular Vendor" runat="server" /></h2>
</div>
<div class="offer_box" style="border-radius: 5px;">
    <div class="offer_text">
        <div class="offer_image" style="width: 100px;">
            <asp:ImageButton ID="ImgBtnVendorLogo" OnClick="LnkBtnDetails_OnClick" Width="100" runat="server" />
        </div>
        <div style="margin-top: 70px;">
        </div>
        <h3><asp:Label ID="Label1" Text="Description" runat="server" /></h3>
        <div class="line">
        </div>
        <div class="offer_price">
            <div class="row">
                <asp:Label ID="Label3" CssClass="bold" Text="Company Name: " runat="server" />
                <asp:Label ID="LblCompanyName" runat="server" />
            </div>
            <div class="row">
                <asp:Label ID="Label2" CssClass="bold" Text="Reg. Date: " runat="server" />
                <asp:Label ID="LblRegdate" runat="server" />
            </div>
            <div class="row">
                <asp:Label ID="Label4" CssClass="bold" Text="Views: " runat="server" />
                <asp:Label ID="LblViews" runat="server" />
            </div>           
        </div>
        <div class="offer_descr">
            <div class="row">
                <telerik:RadTextBox ID="RtbxSmallDescription" Width="380" Height="90" TextMode="MultiLine" style="text-align:justify;" MaxLength="400" ReadOnly="true" runat="server" />
                <%--<asp:Literal ID="LtrSmallDescription" runat="server" />--%>
            </div>
            <div style="margin-top: 20px;">
            </div>
            <div class="link_more" style="float: right;">
                <asp:LinkButton ID="LnkBtnDetails" OnClick="LnkBtnDetails_OnClick" Text="View more ..." runat="server" />
            </div>
        </div>
    </div>
</div>
