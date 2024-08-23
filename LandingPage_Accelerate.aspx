<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage_Accelerate.aspx.cs" Inherits="WdS.ElioPlus.LandingPage_Accelerate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Market Template - elio</title>
    <link href="css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <!-- content begins -->
        <div class="content">
            <div class="banner2">
                <div class="logo">               
                    <a id="aLogo" href="https://www.elioplus.com" runat="server">
                        <img src="/assets/images/logo-elioplus.png" alt="Elioplus logo  "/>
                    </a>               
                </div>
                <div class="banner-title2">
                    <asp:Label ID="LblBannerTitle" Text="Accelerate your Growth" runat="server" />
                    <div class="banner-text2">
                        <asp:Label ID="LblBannerText" Text="Find out in which markets you should enter next, how many resellers you need to cover these markets, build your partner program, recruit and finally manage your partners all under one solution. Take a look below to learn how it works and join." runat="server" />
                    </div>
                </div>
                <div class="banner-entry2">
                    <asp:Label ID="LblEmailText" Text="Get early access" runat="server" />
                    <div style="margin-top:20px;">
                        <asp:TextBox ID="TbxEmail" MaxLength="30" Width="300" Height="30" CssClass="input" runat="server" />
                        <asp:Button ID="BtnSendEmail" OnClick="BtnSendEmail_OnClick" Width="100" Height="35" CssClass="input" Text="Let me in" runat="server" />
                    </div>
                    <div class="row">
                        <asp:Label ID="LblError" CssClass="error" runat="server" />
                    </div>
                </div>
            </div>
            <div class="main">                
                <div class="main-wrapper" style="">
                    <div class="level">
                        <div class="img-wrapper">
                            <img src="/images/LandingPages/Lnd2/img_1.jpg" alt="" />
                        </div>                       
                        <div class="row-wrapper">
                            <h2>
                                <asp:Label ID="Label2" Text="Choose your vertical" runat="server" />
                            </h2>
                            <div class="txt-wrapper">
                                <asp:Label ID="Label3" Text="Select from a variety of verticals to retrive data about the market. You can select multiple verticals based on the products you offer." runat="server" />
                            </div>
                        </div>                        
                    </div> 
                    <div class="level">
                        <div class="img-wrapper">
                            <h2>
                                <asp:Label ID="Label4" Text="Discover your best options" runat="server" />
                            </h2>
                            <div class="txt-wrapper">
                                <asp:Label ID="Label5" Text="We deliver you data about the major geographic markets in order to decide what markets to penetrate first. You will be able to see the size of each market and what is the ideal number of resellers on each market in order to have and optimized channel and cover the total number of customers." runat="server" />
                            </div>
                        </div>                       
                        <div class="row-wrapper">
                            <img src="/images/LandingPages/Lnd2/map.png" alt="" />                           
                        </div>                        
                    </div>   
                    <div class="level" style="padding:20px; margin-top:50px;  display:inline-block;">
                        <div class="img-wrapper">
                            <img src="/images/LandingPages/Lnd2/img_2.png" alt="" />                             
                        </div>                       
                        <div class="row-wrapper">
                            <h2>
                                <asp:Label ID="Label6" Text="Build your partner program" runat="server" />
                            </h2>
                            <div class="txt-wrapper">
                                <asp:Label ID="Label7" Text="If you haven't created a service map for your partner program don't worry, we got you covered. We have create partner program templates using best practices in order to have proper material for your partners. Just add your data and you are set." runat="server" />
                            </div>
                        </div>                        
                    </div>
                    <div class="level">
                        <div class="img-wrapper">
                             <h2>
                                <asp:Label ID="Label1" Text="Recruit new partners" runat="server" />
                            </h2>
                            <div class="txt-wrapper">
                                <asp:Label ID="Label8" Text="Based on your needs we connect you with potential partners that are looking for your solution in the geographic markets that you want to penetrate." runat="server" />
                                <a href="https://www.elioplus.com" target="_blank">Learn more about how that works here.</a>
                            </div>
                        </div>                       
                        <div class="row-wrapper">
                            <img src="/images/LandingPages/Lnd2/business_intelligence.png" alt="" />
                        </div>                        
                    </div>
                    <div class="level" style="padding:20px; margin-top:50px;  display:inline-block;">
                        <div class="img-wrapper">
                            <img src="/images/LandingPages/Lnd2/img_4.png" alt="" />
                        </div>                       
                        <div class="row-wrapper">
                            <h2>
                                <asp:Label ID="Label9" Text="Manage your Partners and Grow" runat="server" />
                            </h2>
                            <div class="txt-wrapper">
                                <asp:Label ID="Label10" Text="Manage and enable your channel partners from your dashboard. No matter how big your network becomes you will be able to collaborate and communicate with your partners with ease and grow your indirect sales." runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="footer">
                <div style="margin-top:20px;">
                    <asp:TextBox ID="TbxEmail2" MaxLength="30" Width="300" Height="30" CssClass="input" runat="server" />
                    <asp:Button ID="BtnSendEmail2" OnClick="BtnSendEmail2_OnClick" Width="100" Height="35" CssClass="input" Text="Let me in" runat="server" />
                </div>
                <div class="row">
                    <asp:Label ID="LblError2" CssClass="error" runat="server" />
                </div>
            </div>
        </div>
    <!-- content ends -->
    </form>
</body>
</html>
