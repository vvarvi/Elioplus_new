<%@ Page Title="" Language="C#" MasterPageFile="~/ElioplusMaster.Master" AutoEventWireup="true" CodeBehind="Calculator.aspx.cs" Inherits="WdS.ElioPlus.Calculator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12" style="text-align:center; margin-top:80px;">
        <h1><asp:Label ID="Label2" runat="server" Text="Calculate your ROI" /></h1>
    </div>
    <div class="col-md-12" style="margin-top:10px; height:120px; background:white; text-align:center; padding-right:10%; padding-left:10%;">
        <h3><asp:Label ID="Label1" Font-Size="12" runat="server" Text="Add on the left field your average annual revenues from your channel partners and then
                                                    drag to select how many months you plan to use our service. On the right part you will
                                                    be able to view your total earnings from the new channel partners added to your distribution
                                                    network by our platfrom." />
        </h3>
    </div>
    <div class="" style="padding-left:auto; padding-right:auto; text-align:center;">
        <iframe src="https://jscalc.io/embed/xyp81fzqdrLuu8I3?autofocus=1" width="1100px" height="500" frameborder="0" marginheight="0" marginwidth="0" style="border: 1px solid rgba(0,0,0,0.12); margin-left:auto; margin-right:auto; margin-top:-45px;"></iframe>
    </div>
</asp:Content>
