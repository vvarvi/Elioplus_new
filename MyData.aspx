<%@ Page Title="" Language="C#" MasterPageFile="~/ElioplusMaster.Master" AutoEventWireup="true" CodeBehind="MyData.aspx.cs" Inherits="WdS.ElioPlus.MyData" %>

<%@ Register Src="/Controls/TermsPrivacy/MyData.ascx" TagName="MDtControl" TagPrefix="controls" %>

<asp:Content ID="AboutUsHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Manage your company’s and personal data on Elioplus." />
    <meta name="keywords" content="manage data, remove personal data" />
    <script src="js/carousel-slider/jquery-1.12.0.min.js"></script>
</asp:Content>
<asp:Content ID="AboutUsMain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="headline-bg about-headline-bg">
    </div><!--//headline-bg-->
    <!-- ******Video Section****** --> 
    <div class="story-section section section-on-bg">
        <h1 class="title container text-center"><asp:Label ID="LtrMyDataTitle" runat="server" /></h1>
        <div class="story-container container text-center"> 
            <div class="story-container-inner">
                <div class="about">                   
                    <p>
                        <controls:MDtControl id="UcMyData" runat="server" />
                    </p>                    
                </div><!--//about-->               
            </div><!--//story-container--> 
        </div><!--//container-->
    </div><!--//story-video-->
    <div id="cta-section" class="section cta-section text-center home-cta-section">
        <asp:PlaceHolder ID="PhElioplusNutshell" runat="server" />
    </div>
</asp:Content>
