<%@ Page Title="" Language="C#" MasterPageFile="~/ElioplusMaster.Master" AutoEventWireup="true" CodeBehind="HowItWorksCollaborationPage.aspx.cs" Inherits="WdS.ElioPlus.HowItWorksCollaborationPage" %>

<%@ Register Src="/Controls/Payment/Stripe_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Elioplus matches automatically software and SaaS vendors with resellers according to their partner program structure and reseller’s partnership needs" />
    <meta name="keywords" content="partner program, service providers, software distributors, API developers, VARs, SaaS vendors, SaaS Resellers, Software partners, business partners, Cloud providers" />
    <script src="js/carousel-slider/jquery-1.12.0.min.js"></script>
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="headline-bg-how-it-works-tool feature-headline-bg mobilefeature">
    </div>
    <!--//headline-bg-->
    <!-- ******Video Section****** -->
    <div class="features-video section section-on-bg">
    </div>
    <!--//feature-video-->
    <!-- ******Features Section****** -->
    <div class="features-tabbed section" style="padding-top: 92px !important; padding-bottom:0px !important;">
        <div class="container">
            <div class="row">
                <div class="text-center col-md-9 col-sm-10 col-xs-12 col-md-offset-2 col-sm-offset-1 col-xs-offset-0">
                    <!-- Nav tabs -->
                    <div class="row">
                        <div class="col-md-10 col-sm-12 col-xs-12 col-md-offset-1 col-sm-offset-1 col-xs-offset-0 clearfix"
                            style="display: none;">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="active" style="width: 315px;"><a href="#feature-1" role="tab" data-toggle="tab">
                                    <asp:Image ID="ImgHowItWorks" Visible="false" ImageUrl="/assets/images/features/vendors_img_2.png"
                                        runat="server" /><br />
                                    <span class="hidden-sm hidden-xs">
                                        <asp:Label Visible="false" ID="LblHowItWorksCollaborationTool" runat="server" /></span>
                                </a></li>
                                <li class="" style="width: 315px; display: none;"><a href="#feature-2" role="tab"
                                    data-toggle="tab">
                                    <asp:Image ID="ImgVendorsFeatures" ImageUrl="/assets/images/features/vendors_img_3.png"
                                        runat="server" /><br />
                                    <span class="hidden-sm hidden-xs">
                                        <asp:Label ID="LblVendorsTitle" runat="server" /></span> </a></li>
                                <li style="width: 315px; display: none;"><a href="#feature-3" role="tab" data-toggle="tab">
                                    <asp:Image ID="ImgResellersFeatures" ImageUrl="/assets/images/features/join-renew-icon-2x.png"
                                        runat="server" /><br />
                                    <span class="hidden-sm hidden-xs">
                                        <asp:Label ID="LblResellersTitle" runat="server" /></span> </a></li>
                                <li style="display: none;"><a href="#feature-4" role="tab" data-toggle="tab">
                                    <asp:Image ID="ImgApiDevelopersFeatures" ImageUrl="/assets/images/features/vendors_img_1.png"
                                        runat="server" /><br />
                                    <span class="hidden-sm hidden-xs">
                                        <asp:Label ID="LblApiDevelopersTitle" runat="server" /></span> </a></li>
                            </ul>
                            <!--//nav-tabs-->
                        </div>
                    </div>
                    <!-- Tab panes -->
                    <div class="row">
                        <div class="tab-content  clearfix">
                            <!--//tab-pane-->
                            <div class="tab-pane fade in active" id="feature-1">
                                <div class="steps section" style="display: none;">
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image ID="ImgVendorFeature1" runat="server" ImageUrl="/assets/images/home/vendors-sing-up and-join.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature2Title1" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature2Content1" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image ID="ImgVendorFeature2" runat="server" ImageUrl="/assets/images/home/vendors-tell-us-more.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature2Title2" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature2Content2" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image ID="ImgVendorFeature3" runat="server" ImageUrl="/assets/images/home/vendors-connect-and-grow.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature2Title3" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature2Content3" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                </div>
                                <div class="col-md-12" style="text-align: center; margin-top: -120px; color:#fff; margin-bottom: 0px;">
                                    <h1>
                                        <asp:Label ID="LblCollaborationVBenefits" runat="server" /></h1>
                                    <p>
                                        <asp:Label ID="LblCollaborationVendorsBenefitsCnt" runat="server" />
                                    </p>
                                    <p>
                                        <a href="/free-sign-up" id="aGetStarted" class="btn btn-cta btn-cta-primary" runat="server">
                                            <span id="LblHomeAddAccount"><asp:Label ID="LblGetStarted" runat="server" /></span>
                                        </a>                    
                                    </p>
                                </div>
                                <div class="col-md-12" style="text-align: center; margin-bottom: 30px; margin-top:70px;">
                                    <h3>
                                        <asp:Label ID="LblCollaborationVBenefits2" runat="server" /></h3>
                                </div>
                                <h3 class="title sr-only">
                                    <asp:Literal ID="LtrVendorsTitle" runat="server" /></h3>
                                <div class="desc" style="display: inline-block; text-align: justify;margin-top: 20px;">
                                    <div style="float: left;">
                                        <asp:Image ID="ImgCollBenefit0" ImageUrl="/assets/images/features/collaborationTool/Manage_Partners.png"
                                            runat="server" />
                                    </div>
                                    <div style="float: right; width: 450px; margin-top: 20px; margin-left: 20px;">
                                        <h3>
                                            <asp:Label ID="LblCollBenefit0Header" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblCollBenefit0Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="desc" style="display: inline-block; text-align: justify;margin-top: 20px;">
                                    <div style="float: right;">
                                        <asp:Image ID="ImgCollBenefit1" ImageUrl="/assets/images/features/collaborationTool/Existing_Partners.png"
                                            runat="server" />
                                    </div>
                                    <div style="float: left; width: 450px; margin-top: 20px; margin-right: 20px;">
                                        <h3>
                                            <asp:Label ID="LblCollBenefit1Header" runat="server" />
                                        </h3>
                                        <p>
                                            <asp:Label ID="LblCollBenefit1Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="desc" style="display: inline-block; text-align: justify;margin-top: 20px;">
                                    <div style="float: left;">
                                        <asp:Image ID="ImgCollBenefit2" ImageUrl="/assets/images/features/collaborationTool/Content_Library.png" runat="server" />
                                    </div>
                                    <div style="float: right; width: 450px; margin-top: 20px; margin-left: 40px;">
                                        <h3>
                                            <asp:Label ID="LblCollBenefit2Header" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblCollBenefit2Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="desc" style="display: inline-block; text-align: justify;margin-top: 20px;">
                                    <div style="float: right;">
                                        <asp:Image ID="ImgCollBenefit3" ImageUrl="/assets/images/features/collaborationTool/Analytics.png"
                                            runat="server" />
                                    </div>
                                    <div style="float: left; width: 450px; margin-top: 0px; margin-right: 20px;">
                                        <h3>
                                            <asp:Label ID="LblCollBenefit3Header" runat="server" />
                                        </h3>
                                        <p>
                                            <asp:Label ID="LblCollBenefit3Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="desc" style="display: inline-block; text-align: justify;">
                                    <div style="float: left;margin-top: 30px;">
                                        <asp:Image ID="ImgCollBenefit4" ImageUrl="/assets/images/features/collaborationTool/Partner_to_Partner.png"
                                            runat="server" />
                                    </div>
                                    <div style="float: right; width: 450px; margin-top: 20px; margin-left: 20px;">
                                        <h3>
                                            <asp:Label ID="LblCollBenefit4Header" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblCollBenefit4Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <!--//desc-->
                                
                            </div>
                            <!--//tab-pane-->
                        </div>
                    </div>
                </div>
                <!--//tab-content-->
                <!-- / row -->
            </div>
            <!--//col-md-x-->
        </div>
        <!--//row-->
    </div>
    <div class="faq section has-bg-color" style="padding-top:50px !important;">
        <div class="container">
            <h2 class="title text-center">
                <asp:Label ID="LblMoreInfoTitle" runat="server" /></h2>
            <div class="row">
                <div class="col-md-8 col-sm-10 col-xs-12 col-md-offset-2 col-sm-offset-1 col-xs-offset-0">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-parent="#accordion" data-toggle="collapse" class="panel-toggle" href="#faq1">
                                    <i class="fa fa-plus-square"></i>
                                    <asp:Label ID="LblInfo1Title" runat="server" />
                                </a>
                            </h4>
                        </div>
                        <div class="panel-collapse collapse" id="faq1">
                            <div class="panel-body" style="padding-top:10px !important;">
                                <asp:Label ID="LblInfo1Content" runat="server" />
                            </div>
                        </div>
                    </div>
                    <!--//panel-->
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-parent="#accordion" data-toggle="collapse" class="panel-toggle" href="#faq2">
                                    <i class="fa fa-plus-square"></i>
                                    <asp:Label ID="LblInfo2Title" runat="server" />
                                </a>
                            </h4>
                        </div>
                        <div class="panel-collapse collapse" id="faq2">
                            <div class="panel-body" style="padding-top:10px !important;">
                                <asp:Label ID="LblInfo2Content" runat="server" />
                                <a id="aInfo2" runat="server">
                                    <asp:Label ID="LblInfo2Link" runat="server" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <!--//panel-->
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-parent="#accordion" data-toggle="collapse" class="panel-toggle" href="#faq3">
                                    <i class="fa fa-plus-square"></i>
                                    <asp:Label ID="LblInfo3Title" runat="server" />
                                </a>
                            </h4>
                        </div>
                        <div class="panel-collapse collapse" id="faq3">
                            <div class="panel-body" style="padding-top:10px !important;">
                                <asp:Label ID="LblInfo3Content" runat="server" />
                                <a id="aInfo3" runat="server">
                                    <asp:Label ID="LblInfo3Link" runat="server" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <!--//panel-->
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-parent="#accordion" data-toggle="collapse" class="panel-toggle" href="#faq4">
                                    <i class="fa fa-plus-square"></i>
                                    <asp:Label ID="LblInfo4Title" runat="server" />
                                </a>
                            </h4>
                        </div>
                        <div class="panel-collapse collapse" id="faq4">
                            <div class="panel-body" style="padding-top:10px !important;">
                                <asp:Label ID="LblInfo4Content" runat="server" />
                            </div>
                        </div>
                    </div>
                    <!--//panel-->
                </div>
            </div>
            <!--//row-->
            <div class="contact-lead text-center">
                <h4 class="title">
                    <asp:Label ID="LblMoreQuestions" runat="server" /></h4>
                <a id="aGetInTouch" runat="server" class="btn btn-cta btn-cta-secondary">
                    <asp:Label ID="LblGetInTouch" runat="server" /></a>
            </div>
        </div>
        <!--//container-->
    </div>
    <!--//container-->    
    <div id="cta-section" class="section cta-section text-center pricing-cta-section">
        <div>
            <h2 class="title">
                <asp:Label ID="LblMoreThan" runat="server" /><span class="counting"><asp:Label ID="LblUsersNumber"
                    runat="server" /></span><asp:Label ID="LblUsersAction" runat="server" /></h2>
            <p class="intro">
                <asp:Label ID="LblNoWait" runat="server" /></p>
            <p>
                <a id="aGetElioNow" runat="server" visible="false" href="#PaymentModal" role="button"
                    data-toggle="modal" class="btn btn-cta btn-cta-primary">
                    <asp:Label ID="LblGetElioNow" runat="server" /></a>
                <asp:Button ID="BtnGetElioNow" Visible="false" OnClick="BtnSearchGoPremium_OnClick"
                    CssClass="btn btn-lg btn-cta-primary" runat="server" />
            </p>
        </div>
        <!--//container-->
    </div>
    <!-- Payment form (modal view) -->
    <div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <controls:UcStripe ID="UcStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
