<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelPartnerLocator.aspx.cs" Inherits="WdS.ElioPlus.ChannelPartnerLocator" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->  
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->  
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->  
<head id="head" runat="server">
    <title id="PgTitle" title="" runat="server"></title>
    <!-- Meta -->
    <meta name="description" content="" />
    <meta name="keywords" content="" />
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
   
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("assets_main/global/plugins/font-awesome/css/font-awesome.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("assets_main/global/plugins/simple-line-icons/simple-line-icons.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("assets_main/global/plugins/bootstrap/css/bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("assets_main/global/plugins/uniform/css/uniform.default.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("assets_main/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css") %>" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="<%= Page.ResolveUrl("assets_main/global/plugins/select2/css/select2.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("assets_main/global/plugins/select2/css/select2-bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="<%= Page.ResolveUrl("assets_main/global/css/components.min.css") %>" rel="stylesheet" id="style_components" type="text/css" />
    <link href="<%= Page.ResolveUrl("assets_main/global/css/plugins.min.css") %>" rel="stylesheet" type="text/css" />
   
    <link href="<%= Page.ResolveUrl("assets_main/pages/css/login-2.min.css") %>" rel="stylesheet" type="text/css" />

    <script src="/js/carousel-slider/jquery-1.12.0.min.js"></script>
    <link rel="stylesheet" href="/assets_main/css/styles.css" />

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
       
</head>
<body class="locator" style="">
    <form id="locatorForm" class="register-form" runat="server">
        <telerik:RadScriptManager ID="signUpRadScriptManager" runat="server">
        </telerik:RadScriptManager>
   
        <main role="main" id="mainForm">
            <div class="head-block">
                <div class="container">
                    <div class="row">
                        <div class="col-xs-20 col-md-14 col-lg-12">
                            <h1>
                                <asp:Label ID="LblVendorPartnerLocatorTitle" runat="server" />
                            </h1>
                            <div class="selection-block">
                                <div class="form-group has-feedback input-block">
                                    <asp:TextBox ID="tbxCompanyOrAddress" CssClass="form-control search-input" placeholder="Search by company name or address..." 
                                         EnableViewState="true" runat="server" />                                
                                    <a id="aBtnSearch" onserverclick="aBtnSearch_OnClick" runat="server">
                                        <ul>
                                            <i class="glyphicon glyphicon-search form-control-feedback"></i>
                                        </ul>
                                    </a>
                                </div>
                                <div class="dropdown">                                
                                    <asp:DropDownList ID="DdlCountries" runat="server" OnSelectedIndexChanged="DdlCountries_SelectedIndexChanged" CssClass="ddl-selection" AutoPostBack="true" EnableViewState="true" />
                                </div>
                            </div>                    
                        </div>
                    </div>
                </div>
            </div>
            <div class="content-block">
                <div class="container">
                    <div class="row">
                        <div class="col-xs-22">
                            <div class="table-content">
                                <asp:Repeater ID="Partners" runat="server">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HdnPartnerID" runat="server" Value='<%# Eval("Id") %>' Visible="false" />
                                        <div class="item">
                                            <div class="logo-td">                                                                                
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Logo") %>' />
                                                <a id="aCompanyLogo" visible="false" href="javascript:;" target="_blank" runat="server" style="">
                                                </a>
                                            </div>
                                            <div class="name-td">
                                                <p class="company-name">                                                
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    <a id="aCompanyName" visible="false" href="javascript:;" target="_blank" runat="server" style="">
                                                    </a>
                                                </p>
                                                <p class="address">
                                                    <i class="fa fa-map-marker"></i>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                </p>
                                            </div>
                                            <div class="contact-td">
                                                <p class="site">
                                                    <i class="fa fa-globe"></i>
                                                    <a id="A1" href='<%# Eval("Website") %>' target="_blank" runat="server">website</a>
                                                </p>
                                                <p class="phone">
                                                    <i class="fa fa-phone"></i>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                </p>
                                            </div>
                                            <div class="info-td">
                                                <p class="status">
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                </p>
                                                <p class="cert">
                                                
                                                </p>
                                            </div>
                                        </div>                                    
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
                        </div>
                    </div>
                </div>            
            </div>
        </main>
    </form>
    
    <!-- BEGIN CORE PLUGINS -->
    <script src="<%= Page.ResolveUrl("assets_main/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("assets_main/global/plugins/bootstrap/js/bootstrap.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("assets_main/global/plugins/js.cookie.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("assets_main/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("assets_main/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("assets_main/global/plugins/jquery.blockui.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("assets_main/global/plugins/uniform/jquery.uniform.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("assets_main/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js") %>" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="<%= Page.ResolveUrl("assets_main/global/plugins/jquery-validation/js/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("assets_main/global/plugins/jquery-validation/js/additional-methods.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("assets_main/global/plugins/select2/js/select2.full.min.js") %>" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <script src="<%= Page.ResolveUrl("assets_main/global/scripts/app.min.js") %>" type="text/javascript"></script>
    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="<%= Page.ResolveUrl("assets_main/pages/scripts/login.min.js") %>" type="text/javascript"></script>
           
</body>
</html> 


