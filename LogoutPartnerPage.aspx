<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogoutPartnerPage.aspx.cs" Inherits="WdS.ElioPlus.LogoutPartnerPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<head id="head" runat="server">
    <title id="PgTitle" runat="server"></title>
    <!-- Meta -->
    <meta name="description" content="Logout from your Vendor's partner portal. Login and start working together and collaborate with your Vendor" />
    <meta name="keywords" content="Vendor partner portal, logout page" />
    <meta charset="utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
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

    <!--begin::Fonts-->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
    <!--end::Fonts-->
    <!--begin::Page Custom Styles(used by this page)-->
    <link href="assets/css/pages/users/login-6.css" rel="stylesheet" type="text/css" />
    <!--end::Page Custom Styles-->
    <!--begin::Global Theme Styles(used by all pages)-->
    <link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/custom/prismjs/prismjs.bundle.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
    <!--end::Global Theme Styles-->
    <!--begin::Layout Themes(used by all pages)-->

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <script type="text/javascript" src="https://platform.linkedin.com/in.js">
        api_key: 86r4i8sx9gbse1
        authorize: true
        onLoad: onLinkedInLoad
        scope: r_basicprofile r_emailaddress
    </script>

    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-38815992-1"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-38815992-1');
    </script>

</head>

<!--begin::Body-->
<body id="kt_body" class="header-fixed header-mobile-fixed subheader-enabled page-loading">
    <!--begin::Main-->
    <div class="d-flex flex-column flex-root">
        <!--begin::Login-->
        <div class="login login-6 login-signin-on d-flex flex-column flex-lg-row flex-row-fluid bg-white" id="kt_login">
            <!--begin::Aside-->
            <div class="login-aside order-2 order-lg-1 d-flex flex-column-fluid flex-lg-row-auto bgi-size-cover bgi-no-repeat p-7 p-lg-10">
                <!--begin: Aside Container-->
                <div class="d-flex flex-row-fluid flex-column justify-content-between">
                    <!--begin::Aside body-->
                    <div class="d-flex flex-column-fluid flex-column flex-center mt-5 mt-lg-0">
                        <a id="aElioplusLogo" runat="server" class="mb-15 text-center">
                            <asp:Image ID="ImgElioplusLogo" CssClass="max-h-75px" runat="server" ImageUrl="/assets_main/images/logo-elioplus.png" />
                        </a>
                        <!--begin::Signin-->
                        <div class="login-form login-signin">
                            <div class="text-center mb-10 mb-lg-20">
                                <h2 class="font-weight-bold">
                                    <asp:Label ID="LblTitle" runat="server" />
                                </h2>
                                <p class="text-muted font-weight-bold">Enter your username and password</p>
                            </div>
                            <!--begin::Form-->
                            <form class="form" id="loginForm" runat="server" novalidate="novalidate">
                                <telerik:RadScriptManager ID="loginRadScriptManager" runat="server">
                                </telerik:RadScriptManager>

                                <controls:MessageControl ID="UcMessageControlAlert" runat="server" />

                                <div class="form-group py-3 m-0">
                                    <asp:TextBox ID="TbxUsername" class="form-control h-auto border-0 px-0 placeholder-dark-75" placeholder="Username or Email" runat="server" required="true" />
                                </div>
                                <div class="form-group py-3 border-top m-0">
                                    <asp:TextBox ID="TbxPassword" class="form-control h-auto border-0 px-0 placeholder-dark-75" TextMode="Password" placeholder="Password" runat="server" required="true" />
                                </div>
                                <div class="form-group d-flex flex-wrap justify-content-between align-items-center mt-3">
                                    <label class="checkbox checkbox-outline m-0 text-muted">
                                        <input id="CbxRemember" runat="server" type="checkbox" name="remember" />Remember me
									
                                        <span></span>
                                    </label>
                                    <a id="forgetPassword" runat="server" onserverclick="forgetPassword_ServerClick" class="text-muted text-hover-primary">Forgot Password ?
                                    </a>
                                </div>
                                <div class="form-group d-flex flex-wrap justify-content-between align-items-center mt-2">
                                    <div class="my-3 mr-2">
                                        <span class="text-muted mr-2">Don't have an account?</span>
                                        <a id="aCreateAccount" runat="server" class="font-weight-bold">Signup</a>
                                    </div>
                                    <a id="signInBtn" runat="server" onserverclick="signInBtn_ServerClick" class="btn btn-primary font-weight-bold px-9 py-4 my-3">Sign In</a>
                                </div>
                            </form>
                            <!--end::Form-->
                        </div>
                        <!--end::Signin-->
                        <!--begin::Form-->
                        <form id="forgotForm" runat="server" visible="false" class="form" novalidate="novalidate">

                            <controls:MessageControl ID="UcMessageControlForgotAlert" runat="server" />

                            <!--begin::Forgot-->
                            <div class="login-form login-forgot-on">
                                <div class="text-center mb-10 mb-lg-20">
                                    <h3 class="">Forgotten Password ?</h3>
                                    <p class="text-muted font-weight-bold">Enter your email to reset your password</p>
                                </div>
                                <div class="form-group py-3 border-bottom mb-10">
                                    <asp:TextBox ID="TbxEmail" CssClass="form-control h-auto border-0 px-0 placeholder-dark-75" type="text" autocomplete="off" name="email" placeholder="Email" runat="server" />
                                </div>
                                <div class="form-group d-flex flex-wrap flex-center">
                                    <a id="submitBtn" runat="server" onserverclick="submitBtn_ServerClick" class="btn btn-primary font-weight-bold px-9 py-4 my-3 mx-2">Submit</a>
                                    <a id="backBtn" runat="server" onserverclick="backBtn_ServerClick" class="btn btn-light-primary font-weight-bold px-9 py-4 my-3 mx-2">Back</a>
                                </div>
                            </div>
                            <!--end::Forgot-->
                        </form>
                        <!--end::Form-->
                    </div>
                    <!--end::Aside body-->
                    <!--begin: Aside footer for desktop-->
                    <div class="d-flex flex-column-auto justify-content-between mt-15">
                        <div class="text-dark-50 font-weight-bold order-2 order-sm-1 my-2">© 2024 Elioplus</div>
                        <div class="d-flex order-1 order-sm-2 my-2">
                            <a id="aTerms" runat="server" class="text-muted text-hover-primary">Terms</a>
                            <a id="aPrivacy" runat="server" class="text-muted text-hover-primary">Privacy</a>
                            <a id="aContact" runat="server" class="text-muted text-hover-primary ml-4">Contact</a>
                        </div>
                    </div>
                    <!--end: Aside footer for desktop-->
                </div>
                <!--end: Aside Container-->
            </div>
            <!--begin::Aside-->
            <!--begin::Content-->
            <div class="order-1 order-lg-2 flex-column-auto flex-lg-row-fluid d-flex flex-column p-7" style="background-image: url(/assets_main/pages/img/login/bg1.jpg);">
            </div>
            <!--end::Content-->
        </div>
        <!--end::Login-->
    </div>
    <!--end::Main-->
    <script>var HOST_URL = "https://keenthemes.com/metronic/tools/preview";</script>
    <!--begin::Global Config(global config for global JS scripts)-->
    <script>var KTAppSettings = { "breakpoints": { "sm": 576, "md": 768, "lg": 992, "xl": 1200, "xxl": 1200 }, "colors": { "theme": { "base": { "white": "#ffffff", "primary": "#6993FF", "secondary": "#E5EAEE", "success": "#1BC5BD", "info": "#8950FC", "warning": "#FFA800", "danger": "#F64E60", "light": "#F3F6F9", "dark": "#212121" }, "light": { "white": "#ffffff", "primary": "#E1E9FF", "secondary": "#ECF0F3", "success": "#C9F7F5", "info": "#EEE5FF", "warning": "#FFF4DE", "danger": "#FFE2E5", "light": "#F3F6F9", "dark": "#D6D6E0" }, "inverse": { "white": "#ffffff", "primary": "#ffffff", "secondary": "#212121", "success": "#ffffff", "info": "#ffffff", "warning": "#ffffff", "danger": "#ffffff", "light": "#464E5F", "dark": "#ffffff" } }, "gray": { "gray-100": "#F3F6F9", "gray-200": "#ECF0F3", "gray-300": "#E5EAEE", "gray-400": "#D6D6E0", "gray-500": "#B5B5C3", "gray-600": "#80808F", "gray-700": "#464E5F", "gray-800": "#1B283F", "gray-900": "#212121" } }, "font-family": "Poppins" };</script>
    <!--end::Global Config-->
    <!--begin::Global Theme Bundle(used by all pages)-->
    <script src="/assets/plugins/global/plugins.bundle.js"></script>
    <script src="/assets/plugins/custom/prismjs/prismjs.bundle.js"></script>
    <script src="/assets/js/scripts.bundle.js"></script>
    <!--end::Global Theme Bundle-->
    <!--begin::Page Scripts(used by this page)-->
    <script src="/assets/js/pages/custom/login/login.js"></script>
    <!--end::Page Scripts-->
</body>
<!--end::Body-->
</html>


