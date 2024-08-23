<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcHowItWorks.ascx.cs" Inherits="WdS.ElioPlus.Controls.UcHowItWorks" %>

<div class="row">
    <h1 style="text-align:center; margin-bottom:50px;"><asp:Label ID="LblHowItWorks" runat="server" /></h1>
    <div class="col-md-4" style="text-align:center;margin-bottom:80px;">                        
        <asp:Image ID="Image1" CssClass="hmpg-img-steps" AlternateText="Elioplus how it works" ImageUrl="/images/hiw_icon_1.png" runat="server" />
        <h3><asp:Label ID="LblCreateProfile" runat="server" /></h3>
        <asp:Label ID="LblCreateProfileContent" runat="server" />
    </div>
    <div class="col-md-4" style="text-align:center;margin-bottom:80px;">                        
        <asp:Image ID="Image3" CssClass="hmpg-img-steps" AlternateText="Elioplus how it works" ImageUrl="/images/hiw_icon_3.png" runat="server" />
        <h3><asp:Label ID="LblGetMatched" runat="server" /></h3>
        <asp:Label ID="LblGetMatchedContent" runat="server" />
    </div>
    <div class="col-md-4" style="text-align:center;margin-bottom:80px;">                        
        <asp:Image ID="Image2" CssClass="hmpg-img-steps" AlternateText="Elioplus how it works" ImageUrl="/images/hiw_icon_2.png" runat="server" />
        <h3><asp:Label ID="LblPartnerCollaboration" runat="server" /></h3>
        <asp:Label ID="LblPartnerCollaborationContent" runat="server" />
    </div> 
    <div class="text-center"><a id="aBuildCV" runat="server" class="btn btn-elio"><asp:Label ID="LblHowItWorksBuildCV" runat="server" /></a></div>                   
</div>