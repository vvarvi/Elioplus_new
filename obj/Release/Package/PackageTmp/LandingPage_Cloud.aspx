<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage_Cloud.aspx.cs" Inherits="WdS.ElioPlus.LandingPage_Cloud" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Market - elio</title>
    <link href="css/styles.css" rel="stylesheet" type="text/css" />
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/com_style.css" type="text/css"/>
    <link rel="stylesheet" href="/com_main_style.css" type="text/css"/>
    
    <script src="/js/libs/modernizr.min.js" type="text/javascript"></script>
    <script src="/js/libs/respond.min.js" type="text/javascript"></script>
    <script src="/js/libs/jquery.min.js" type="text/javascript"></script>
    <script src="/js/jquery.easing.min.js" type="text/javascript"></script>
    <script src="/js/general.js" type="text/javascript"></script>
    <script src="/js/hoverIntent.js" type="text/javascript"></script>
    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-38815992-1', 'auto');
        ga('send', 'pageview');

        adroll_adv_id = "37GQS7SFQVDKREWXV47SXL";
        adroll_pix_id = "M23SWPXKLZFRPJI2UEAPQU";
        (function () {
            var oldonload = window.onload;
            window.onload = function () {
                __adroll_loaded = true;
                var scr = document.createElement("script");
                var host = (("https:" == document.location.protocol) ? "https://s.adroll.com" : "http://a.adroll.com");
                scr.setAttribute('async', 'true');
                scr.type = "text/javascript";
                scr.src = host + "/j/roundtrip.js";
                ((document.getElementsByTagName('head') || [null])[0] ||
                document.getElementsByTagName('script')[0].parentNode).appendChild(scr);
                if (oldonload) { oldonload() }
            };
        } ());
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <!-- content begins -->
    <div class="content">
        <div class="">
            <div class="banner-title3">
                <asp:Label ID="LblBannerTitle" Text="Grow your business" runat="server" />
                <div class="banner-text2">
                    <asp:Label ID="LblBannerText" Text="Partner with companies in the Cloud, Data & Security industry and accelerate your growth. Start now, it's free!"
                        runat="server" />
                </div>
            </div>
        </div>
        <div class="main">
            <div class="" style="display:inline-block;">
                <div style="display: inline-block;padding:20px; float:left;">
                    <img src="images/LandingPages/Lnd3/middle-l.png" alt="" />                                       
                        <%--<a id="aMainMenuSignUp" runat="server">
                            <img src="images/LandingPages/Lnd3/middle-r.png" alt="" /> 
                        </a> --%>                   
                        <div class="landing_contetn_right">                            
                            <asp:PlaceHolder ID="PhLoginControls" runat="server" />
                        </div>
                </div>              
            </div>
            <div>
                <h2>
                    <asp:Label ID="Label1" Text="How it works" runat="server" />
                </h2>
            </div>
            <div class="">
                <div class="" style="font-size:18px;line-height:23px; text-align:center;display:inline-block;">
                    <div class="col-md-4" style="width: 230px;display: inline-block; float:left; font-size: 15px;">
                        <img src="/images/LandingPages/Lnd3/img_1.png" alt=""/>
                        <br />
                        <h3 class="text"><asp:Label ID="LblFeature1Title1" Text="Join" runat="server" /></h3>
                        <p>
                            <asp:Label ID="LblFeature1Content1" Text="Start with simply adding a few details about your company so potential partners can find out about your business and partner program. "
                                runat="server" />
                        </p>
                    </div>
                    <div class="col-md-4" style="width: 230px; display: inline-block; float:left;font-size: 15px;">
                        <img src="/images/LandingPages/Lnd3/img_2.png" alt=""/>
                        <br />
                        <h3 class="text"><asp:Label ID="Label3" Text="Receive new proposals" runat="server" /></h3>
                        <p>
                            <asp:Label ID="Label4" Text="We match you with new partners automatically based on your needs. After you provide some details about your needs we use an algorithm that analyses these criteria in order to connect you with the most relevant partners."
                                runat="server" />
                        </p>
                    </div>
                    <div class="col-md-4" style="width: 230px;position: relative; display: inline-block;font-size: 15px;">
                        <img src="/images/LandingPages/Lnd3/img_3.png" alt=""/>
                        <br />
                        <h3 class="text"><asp:Label ID="Label5" Text="Accererate Growth" runat="server" /></h3>
                        <p>
                            <asp:Label ID="Label6" Text="Collaborate with your new partners directly from our platform. You will have a central hub where you can find marketing material and campaigns, product updates etc. that will enable you to reach new customers. You can add as man vendors as you like, it's always free."
                                runat="server" />
                        </p>
                    </div>
                </div>
            </div>
            <div>
                <h2>
                    <asp:Label ID="Label2" Text="Some of our featured companies:" runat="server" />
                </h2>
            </div>
            <div>
                <img src="images/LandingPages/Lnd3/f_companies.png" alt=""/>
            </div>
        </div>
        <div class="" style="text-align:center; padding:20px;">
            <asp:Button ID="BtnJoinNow" Text="Join Now Free" OnClick="BtnJoinNow_OnClick" CssClass="btn-input" style="border-radius:10px; border:1px solid #454545; background-color:#454545; height:70px; width:200px; color:#ffffff; font-size:20px; cursor:pointer; text-align:center;" runat="server" />
        </div>
    </div>
    <!-- content ends -->
    </form>
</body>
</html>
