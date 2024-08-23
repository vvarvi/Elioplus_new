<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiAccountsFullRegistration.aspx.cs" Inherits="WdS.ElioPlus.MultiAccountsFullRegistration" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<head>
    <title id="PgTitle" runat="server"></title>
    <!-- Meta -->
    <meta name="description" content="Complete your free registration on Elioplus as a software or SaaS vendor, reseller or API developer and find new partners by creating your own channel" />
    <meta name="keywords" content="partnership opportunities, submit your partner program, find new partners, choose software products, receive leads, find software companies" />
    <meta charset="utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />    
    <meta name="author" content="Elioplus Team" />
    <meta name="robots" content="index, follow" />
    <meta name="revisit-after" content="7 days" />
    <meta name="copyright" content="Elioplus" />
    <meta name="googlebot" content="noodp" />
    <meta name="language" content="English" />
    <meta name="reply-to" content="info@elioplus.com" />
    <meta name="web_author" content="Elioplus Team" />
    <meta name="distribution" content="global" />    
    <link rel="shortcut icon" href="https://elioplus.com/favicon.ico" type="image/x-icon" /> 
    <link href='https://fonts.googleapis.com/css?family=Roboto:400,400italic,500,500italic,700,700italic,900,900italic,300italic,300' rel='stylesheet' type='text/css' /> 
    <link href='https://fonts.googleapis.com/css?family=Roboto+Slab:400,700,300,100' rel='stylesheet' type='text/css' />
    <!-- Global CSS -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap/css/bootstrap.min.css" />   
    <!-- Plugins CSS -->    
    <link rel="stylesheet" href="/assets/plugins/font-awesome/css/font-awesome.css" />
    <link rel="stylesheet" href="/assets/plugins/flexslider/flexslider.css" />
    <!-- Theme CSS -->
    <link id="theme-style" rel="stylesheet" href="/assets/css/styles.css" />
    <link href="/assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/icheck/skins/all.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/bootstrap-wysihtml5/bootstrap-wysihtml5.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/bootstrap-markdown/css/bootstrap-markdown.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/global/plugins/bootstrap-summernote/summernote.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
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
<body class="login-page access-page has-full-screen-bg">
    <form id="signUpForm" runat="server">
    <telerik:RadScriptManager ID="signUpRadScriptManager" runat="server">
    </telerik:RadScriptManager>
    <div class="upper-wrapper">
        <!-- ******HEADER****** -->
        <div class="header">
            <div class="container">
            </div>
            <!--//container-->
        </div>
        <!--//header-->
        <!-- ******Signup Section****** -->
        <div class="signup-section access-section section" style="padding-bottom:40px; padding-top:0px;">
            <div class="container">
                <div class="row">
                    <div class="form-box col-md-8 col-sm-12 col-xs-12 col-md-offset-2 col-sm-offset-0 xs-offset-0">
                        <asp:PlaceHolder ID="PhFullRegistration" runat="server" />
                    </div>
                    <!--//form-box-->
                </div>
                <!--//row-->
            </div>
            <!--//container-->
        </div>
        <!--//signup-section-->
    </div>
    <!--//upper-wrapper-->
    <script type="text/javascript" src="/assets/plugins/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="/assets/plugins/jquery-migrate-1.2.1.min.js"></script>
    <script type="text/javascript" src="/assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/assets/plugins/bootstrap-hover-dropdown.min.js"></script>
    <script type="text/javascript" src="/assets/plugins/back-to-top.js"></script>
    <script type="text/javascript" src="/assets/plugins/jquery-placeholder/jquery.placeholder.js"></script>
    <script type="text/javascript" src="/assets/plugins/FitVids/jquery.fitvids.js"></script>
    <script type="text/javascript" src="/assets/plugins/flexslider/jquery.flexslider-min.js"></script>
    <script type="text/javascript" src="/assets/js/main.js"></script>
    <script type="text/javascript" src="/assets/plugins/jquery.validate.min.js"></script>
    </form>
</body>
</html>
