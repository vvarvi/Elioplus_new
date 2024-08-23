<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResellersBenefits.ascx.cs" Inherits="WdS.ElioPlus.Controls.Benefits.ResellersBenefits" %>

<div class="how-content txt-color">
    <div class="how-benefits-steps">
        <asp:Image ID="ImgResellersOne" ImageUrl="~/images/icons/reseller_img_1.png" runat="server" />
    </div>
    <div class="how-benefits-step1">
        <h1 style="text-align:left;"><asp:Label ID="LabelResBen24" runat="server" /></h1>
        <div class="row">
            <asp:Label ID="LblResellerBen1" runat="server" />
        </div>
        <div class="row">
            <asp:Label ID="LblResellerBen2" runat="server" />
        </div>                                        
    </div>
    <div class="how-benefits-steps" style="float:right;">
        <asp:Image ID="ImgResellersSecond" ImageUrl="~/images/icons/reseller_img_2.png" runat="server" />
    </div>
    <div class="how-benefits-step2">
        <h1 style="text-align:right;"><asp:Label ID="LabelResBen28" runat="server" /></h1>
        <div class="row">
            <asp:Label ID="LabelResBen21" runat="server" />
        </div>
        <div class="row">
            <asp:Label ID="LabelResBen7" runat="server" />
        </div>                                       
    </div>
    <div class="how-benefits-steps-bottom" style="margin-left:350px;">
        <asp:Image ID="ImgResellersThird" ImageUrl="~/images/icons/reseller_img_3.png" style="margin-top:30px;" runat="server" />
    </div>
    <div class="how-benefits-step3">
        <h1><asp:Label ID="LabelRessBen31" runat="server" /></h1>
        <div class="row">
            <asp:Label ID="LabelResBen23" runat="server" />
        </div>
        <div class="row">
            <asp:Label ID="LabelResBen8" runat="server" />
        </div>
    </div>
</div>