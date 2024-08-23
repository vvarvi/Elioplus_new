<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThankYouPage.aspx.cs" Inherits="WdS.ElioPlus.ThankYouPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="PgTitle" runat="server"></title>

    <!-- Global CSS -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap/css/bootstrap.min.css" />
    <!-- Plugins CSS -->
    <link rel="stylesheet" href="/assets/plugins/font-awesome/css/font-awesome.css" />
    <link rel="stylesheet" href="/assets/plugins/flexslider/flexslider.css" />
    <!-- Theme CSS -->
    <link href="/assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/icheck/skins/all.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/bootstrap-wysihtml5/bootstrap-wysihtml5.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/bootstrap-markdown/css/bootstrap-markdown.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/bootstrap-summernote/summernote.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/css/components.css" rel="stylesheet" />

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
        }());
    </script>
</head>
<body class="login-page access-page has-full-screen-bg">
    <form id="form1" runat="server">
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="row">
                    <div id="divVendors" runat="server" class="text-center col-md-9 col-sm-9 col-xs-12 col-md-offset-2">
                        <div class="row" style="margin-top: 50px;">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" style="text-align: center;">
                                <asp:Image ID="ImgCompetitiveSocialMedia" ImageUrl="/images/Competitive_Social_Media_Benchmarking.png" Width="300px" runat="server" />
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                    </div>
                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-12">
                                        <div style="text-align: center; width: 280px;">
                                            <h3>
                                                <asp:Label ID="LblSuccessRegistration" runat="server" Text="YOU HAVE SUCCESSFULLY SIGNED UP!" />
                                            </h3>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div style="text-align: center; width: 100%;">
                                        <p>
                                            <asp:Label ID="LblSuccessRegistrationContent" runat="server" Text="You can access your account to manage potential partnerships and grow your partner network" />
                                        </p>
                                        <a id="aBtnDashboard" runat="server" style="width: 250px;" onserverclick="BtnDashboard_OnClick" class="btn btn-danger">
                                            <asp:Label ID="LblBtnDashboard" Text="Go to your dashboard" runat="server" />
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="text-align: center; background-color: #1d58a4; color: #fff; min-height: 550px; margin-top: 50px; padding-bottom: 20px;">
                            <div class="col-md-12 col-sm-12">
                                <div style="margin-top: 50px;">
                                    <h3>
                                        <asp:Label ID="LblVendorsTitle" runat="server" Text="Launch you Partner Portal" />
                                    </h3>
                                    <p>
                                        <asp:Label ID="LblContent" runat="server" Text="Launch your partner portal to manage your channel partners, automate the deal registration process and increase your partner sales." />
                                        <a id="aContactUs" style="color:#fff;text-decoration:underline;" runat="server">Contact us</a>
                                        <br />
                                        <asp:Label ID="LblContent2" runat="server" Text="<b>What’s included?</b> Partner directory, onboarding, deal registration, lead distribution, CRM integrations, analytics, partner locator and many more features." />
                                    </p>
                                    <div class="row">
                                        <div id="divVendorsArea" runat="server">
                                            <div class="text-center col-md-1 col-xs-12" style=""></div>
                                            <div class="text-center col-md-5 col-xs-12">
                                                <div style="background-color: #fff; height: 300px; width: 380px; text-align: center; color: #000;">
                                                    <div style="padding-top: 20px;">
                                                        <img src="/images/Thank-you-Partner-With-New-Vendors.png" height="220px;" alt="Partner With New Vendors" />
                                                    </div>
                                                    <div style="margin-top: 20px;">
                                                        <strong>
                                                            <asp:Label ID="Label2" runat="server" Text="Partner Relationship Manager" /></strong>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="text-center col-md-5 col-xs-12">
                                                <div style="background-color: #fff; height: 300px; width: 380px; text-align: center; color: #000; padding-top: 20px;">
                                                    <div style="padding-top: 10px;">
                                                        <strong>
                                                            <asp:Label ID="Label1" runat="server" Text="Want to learn more about Elioplus' PRM software?" /></strong>
                                                    </div>
                                                    <div style="padding-top: 10px;">
                                                        <img src="/images/Thank-you-New-Vendors-image-2.png" alt="New Vendors Registration" />
                                                        <div style="margin-top: 10px;">
                                                            <a id="aBookDemo" target="_blank" runat="server" style="border: 2px solid #39b4ef;" class="btn btn-cta btn-cta-secondary">
                                                                <asp:Label ID="LblBookDemo" Text="BOOK A MEETING" runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="text-center col-md-1 col-xs-12" style=""></div>
                                        </div>
                                        <div id="divChannelPartnersArea" runat="server" visible="false">
                                            <div class="text-center col-md-2 col-xs-12" style=""></div>
                                            <div class="text-center col-md-8 col-xs-12">
                                                <div style="background-color: #fff; text-align: center; color: #000;">
                                                    <div style="padding-top: 20px;">
                                                        <img src="/images/Leads-thank-you-page.png" alt="Leads" />
                                                    </div>
                                                    <div style="margin-top: 20px;">                                                        
                                                    </div>
                                                </div>
                                            </div>                                            
                                            <div class="text-center col-md-2 col-xs-12" style=""></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divChannelPartners" visible="false" runat="server" class="text-center col-md-9 col-sm-9 col-xs-12 col-md-offset-2">
                    </div>
                </div>
                <div class="row">
                    <div class="text-center col-md-9 col-sm-10 col-xs-12 col-md-offset-2 col-sm-offset-1 col-xs-offset-0">
                        <div style="float: right; margin-top: 40px; font-family: sans-serif; margin-bottom: 20px;">
                            <asp:Label ID="LblCopyright" runat="server" />
                            <a id="aCopyRight" runat="server">
                                <asp:Label ID="LblCopyrightElioplus" runat="server" />
                            </a>
                            <asp:Label ID="LblCopyright2" runat="server" />
                        </div>
                    </div>
                </div>
                <div id="divOldThankYouArea" runat="server" visible="false" style="margin-top: 5%; margin-left: 15%; margin-right: 15%;">
                    <div>
                        <asp:ImageButton ID="ImgBtnLogo" OnClick="ImgBtnLogo_OnClick" ImageUrl="/images/logo_blue_letters.png" Style="cursor: pointer;" runat="server" />
                    </div>
                    <div style="border-bottom-style: ridge; border-bottom-width: medium; border-bottom-color: #CCCCCC; margin-top: 10px;"></div>
                    <div style="font-size: 35px; font-weight: bold; text-align: center; margin-top: 25px; font-family: Calibri;">
                        <asp:Label ID="LblSuccessRegister" runat="server" />
                        <asp:Image ID="Image1" ImageUrl="/images/thank-page-check.png" runat="server" ImageAlign="AbsBottom" />
                    </div>
                    <div style="font-family: Calibri; font-size: 25px; font-weight: bold; text-align: center; margin-top: 40px; color: Grey;">
                        <asp:Label ID="LblFindNewPartners" runat="server" />
                    </div>
                    <div style="font-family: Calibri; text-align: center; font-size: 30px; font-weight: bold; margin-top: 60px;">
                        <asp:Label ID="LblRecruitNewPartners" runat="server" />
                    </div>

                </div>
            </div>
        </div>
    </form>
    <!--ClickMeter.com code for conversion: Full sign up -->
    <script type='text/javascript'>
        var ClickMeter_conversion_id = '82C8837C909343C0816152753ACC39FD';
        var ClickMeter_conversion_value = '0.00';
        var ClickMeter_conversion_commission = '0.00';
        var ClickMeter_conversion_commission_percentage = '0.00';
        var ClickMeter_conversion_parameter = 'empty';
    </script>
    <script type='text/javascript' id='cmconvscript' src='//s3.amazonaws.com/scripts-clickmeter-com/js/conversion.js'></script>
    <noscript>
        <img height='0' width='0' alt='' src='//www.clickmeter.com/conversion.aspx?id=82C8837C909343C0816152753ACC39FD&val=0.00&param=empty&com=0.00&comperc=0.00' />
    </noscript>
</body>
</html>
