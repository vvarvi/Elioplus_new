<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage_Synergies.aspx.cs" Inherits="WdS.ElioPlus.LandingPage_Synergies" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Build synergies to reach more customers</title>
    <meta name="description" content="Synergies by Elioplus" />
    <link href="css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <!-- content begins -->
        <div class="content" style="width:100%">
            <div class="banner">
                <div class="logo">               
                    <a id="aLogo" href="https://www.elioplus.com" runat="server">
                        <img src="/images/lnd-elio-lg.png" alt="Elioplus logo" />
                    </a>               
                </div>
                <div class="banner-title">
                    <asp:Label ID="LblBannerTitle" Text="Build synergies to reach more customers" runat="server" />
                    <div class="banner-text">
                        <asp:Label ID="LblBannerText" Text="Create marketing partnerships (like cross-promotions, content-collaboration, partner retargeting etc.) 
                                                        with other companies to leverage each other's userbase, reach more customers for free and grow your business." runat="server" />
                    </div>
                </div>
                <div class="banner-entry">
                    <asp:Label ID="LblEmailText" Text="Get early access" runat="server" />
                    <div style="margin-top:20px;">
                        <asp:TextBox ID="TbxEmail" MaxLength="30" Font-Size="12" Width="300" Height="30" CssClass="input" runat="server" />
                        <asp:Button ID="BtnSendEmail" OnClick="BtnSendEmail_OnClick" ForeColor="White" Font-Size="12" Width="100" Height="35" CssClass="input btnn-input" Text="I want in" runat="server" />
                    </div>
                    <div class="row">
                        <asp:Label ID="LblError" CssClass="error" runat="server" />
                    </div>
                </div>
            </div>
            <div class="main"> 
                <div>
                    <h1>
                        <asp:Label ID="Label1" Text="How it works" runat="server" />
                    </h1>
                </div>
                <div class="main-wrapper" style="margin-bottom:20px;">
                    <div class="level">
                        <div class="img-wrapper">
                            <img src="/images/LandingPages/Lnd1/img_1.png" alt="" />
                        </div>                       
                        <div class="row-wrapper">
                            <h2>
                                <asp:Label ID="Label2" Text="Join" runat="server" />
                            </h2>
                            <div class="txt-wrapper">
                                <asp:Label ID="Label3" Text="Create your profile and add the partnership types that you are interested in, like cross-promotions, 
                                                        content collaboration, guest posts, social media etc. and give an overview of your company." runat="server" />
                            </div>
                        </div>                        
                    </div> 
                    <div class="level">
                        <div class="img-wrapper">
                             <h2>
                                <asp:Label ID="Label4" Text="Partner" runat="server" />
                            </h2>
                            <div class="txt-wrapper">
                                <asp:Label ID="Label5" Text="Discover prospective partners based on the type of partnership, the vertical and even the location if you want 
                                                        to reach a specific market. Reach out directly or we can match you with potential partners based on your preferences." runat="server" />
                            </div>
                        </div>                       
                        <div class="row-wrapper">
                            <img src="/images/LandingPages/Lnd1/img_2.png" alt="" />                           
                        </div>                        
                    </div>   
                    <div class="level" style="padding:20px; margin-top:50px;  display:inline-block;">
                        <div class="img-wrapper">
                            <img src="/images/LandingPages/Lnd1/img_3.png" alt="" />                             
                        </div>                       
                        <div class="row-wrapper">
                            <h2>
                                <asp:Label ID="Label6" Text="Grow" runat="server" />
                            </h2>
                            <div class="txt-wrapper">
                                <asp:Label ID="Label7" Text="Once you’ve found your perfect partners, you can start making wonderful things together. You can collaborate and 
                                                        communicate with them directly from your dashboard and track your reach in your joined efforts." runat="server" />
                            </div>
                        </div>                        
                    </div>   
                </div>
                <div>
                    <h1>
                        <asp:Label ID="Label8" Text="Why it works" runat="server" />
                    </h1>
                </div>
                <div class="main-wrapper" style="width:40%">
                    <div style="float:left; display:inline-block; margin-bottom:30px;">
                        <div style="float:left; width:50px; display:inline-block;">
                            <img src="/images/LandingPages/Lnd1/Icon_1.png" alt="" />
                        </div>
                        <div style="float:left; margin-left:20px; width:80%; text-align:left;">
                            <asp:Label ID="Label9" Text="Reach new customers or users" Font-Bold="true" runat="server" />
                            <br />
                            <asp:Label ID="Label10" Text="Reach new, higly-interested propsect clients or even users with zero cost." runat="server" />
                        </div>                        
                    </div>
                    <div style="float:left; display:inline-block; margin-bottom:30px;">
                        <div style="float:left; width:50px; display:inline-block;">
                            <img src="/images/LandingPages/Lnd1/Icon_2.png" alt="" />
                        </div>
                        <div style="float:left; margin-left:20px; width:80%; text-align:left;">
                            <asp:Label ID="Label11" Text="Increase brand awareness" Font-Bold="true" runat="server" />
                            <br />
                            <asp:Label ID="Label12" Text="Create a strong brand and increase your brand awareness by collaborating with complementary companies in your
                                                        industry or simply by partnering with other well-known businesses." runat="server" />
                        </div>                        
                    </div>
                    <div style="float:left; display:inline-block; margin-bottom:20px;">
                        <div style="float:left; width:50px; display:inline-block;">
                            <img src="/images/LandingPages/Lnd1/Icon_3.png" alt="" />
                        </div>
                        <div style="float:left; margin-left:20px; width:80%; text-align:left;">
                            <asp:Label ID="Label13" Text="100% Privacy Safe" Font-Bold="true" runat="server" />
                            <br />
                            <asp:Label ID="Label14" Text="We don't share or sell your data. You work directly with your new partners to run any promotions and you can
                                                        end any partnership at anytime." runat="server" />
                        </div>                        
                    </div>
                </div>               
            </div>
            <div class="footer">
                <asp:Label ID="LblFooterTxt" Text="This project is built by the Elioplus team" runat="server" />
            </div>
        </div>
    <!-- content ends -->
    </form>
</body>
</html>
