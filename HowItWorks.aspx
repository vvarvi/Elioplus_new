<%@ Page Title="" Language="C#" MasterPageFile="~/ElioplusMaster.Master" AutoEventWireup="true" CodeBehind="HowItWorks.aspx.cs" Inherits="WdS.ElioPlus.HowItWorks" %>

<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Elioplus matches automatically software and SaaS vendors with resellers according to their partner program structure and reseller’s partnership needs" />
    <meta name="keywords" content="partner program, service providers, software distributors, API developers, VARs, SaaS vendors, SaaS Resellers, Software partners, business partners, Cloud providers" />
    <script src="js/carousel-slider/jquery-1.12.0.min.js"></script>
</asp:Content>
<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="headline-bg feature-headline-bg mobilefeature">
    </div>
    <!--//headline-bg-->
    <!-- ******Video Section****** -->
    <div class="features-video section section-on-bg">
    </div>
    <!--//feature-video-->
    <!-- ******Features Section****** -->
    <div class="features-tabbed section" style="padding-top: 92px !important">
        <div class="container">
            <div class="row">
                <div class="text-center col-md-9 col-sm-10 col-xs-12 col-md-offset-2 col-sm-offset-1 col-xs-offset-0">
                    <!-- Nav tabs -->
                    <div class="row">
                        <div class="col-md-10 col-sm-12 col-xs-12 col-md-offset-1 col-sm-offset-1 col-xs-offset-0 clearfix">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="" style="width: 340px; display: none;">
                                    <a href="#feature-1" role="tab" data-toggle="tab">
                                        <asp:Image ID="ImgHowItWorks" ImageUrl="/assets/images/features/vendors_img_2.png" AlternateText="Vendors" runat="server" /><br />
                                        <span class="hidden-sm hidden-xs">
                                            <asp:Label ID="LblHowItWorks" runat="server" /></span>
                                    </a>
                                </li>
                                <li class="active" style="width: 340px;">
                                    <a href="#feature-2" role="tab" data-toggle="tab">
                                        <asp:Image ID="ImgVendorsFeatures" ImageUrl="/assets/images/features/vendors_img_3.png" AlternateText="Channel Partners" runat="server" /><br />
                                        <span class="hidden-sm hidden-xs">
                                            <asp:Label ID="LblVendorsTitle" runat="server" /></span>
                                    </a>
                                </li>
                                <li style="width: 340px;">
                                    <a href="#feature-3" role="tab" data-toggle="tab">
                                        <asp:Image ID="ImgResellersFeatures" ImageUrl="/assets/images/features/join-renew-icon-2x.png" AlternateText="Join Renew" runat="server" /><br />
                                        <span class="hidden-sm hidden-xs">
                                            <asp:Label ID="LblResellersTitle" runat="server" /></span>
                                    </a>
                                </li>
                                <li style="display: none;">
                                    <a href="#feature-4" role="tab" data-toggle="tab">
                                        <asp:Image ID="ImgApiDevelopersFeatures" ImageUrl="/assets/images/features/vendors_img_1.png" AlternateText="Api Developers Features" runat="server" /><br />
                                        <span class="hidden-sm hidden-xs">
                                            <asp:Label ID="LblApiDevelopersTitle" runat="server" /></span>
                                    </a>
                                </li>
                            </ul>
                            <!--//nav-tabs-->
                        </div>
                    </div>
                    <!-- Tab panes -->
                    <div class="row">
                        <div class="tab-content  clearfix">
                            <div class="tab-pane fade in" id="feature-1">
                                <div class="steps section">
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <br />
                                        <asp:Image ID="ImgFeature1Search" runat="server" AlternateText="Feature 1 Search" ImageUrl="/assets/images/home/reseller-vendor-search.png" />
                                        <br />
                                        <h3 class="text">
                                            <asp:Label ID="LblFeature1Title1" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature1Content1" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <br />
                                        <asp:Image ID="ImgFeature1Discover" AlternateText="Discover" runat="server" ImageUrl="/assets/images/home/discover-opportunities.png" />
                                        <br />
                                        <h3 class="text">
                                            <asp:Label ID="LblFeature1Title2" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature1Content2" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <br />
                                        <asp:Image ID="ImgFeature1Connect" AlternateText="Connect" runat="server" ImageUrl="/assets/images/home/connect-with-partners.png" />
                                        <br />
                                        <h3 class="text">
                                            <asp:Label ID="LblFeature1Title3" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature1Content3" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                </div>
                            </div>
                            <!--//tab-pane-->
                            <div class="tab-pane fade in active" id="feature-2">
                                <div class="col-md-12">
                                    <div class="hidden">
                                        <iframe src="https://www.youtube.com/embed/57adi2j2hlw" width="100%" height="500px" frameborder="0" allowfullscreen style="margin-top: 50px; margin-bottom: 50px;"></iframe>
                                    </div>
                                </div>
                                <div class="steps section" style="display: none;">
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image ID="ImgVendorFeature1" runat="server" AlternateText="Sign Up and Join" ImageUrl="/assets/images/home/vendors-sing-up and-join.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature2Title1" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature2Content1" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image ID="ImgVendorFeature2" AlternateText="Feature 2" runat="server" ImageUrl="/assets/images/home/vendors-tell-us-more.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature2Title2" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature2Content2" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image ID="ImgVendorFeature3" AlternateText="Vendors Connect and Grow" runat="server" ImageUrl="/assets/images/home/vendors-connect-and-grow.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature2Title3" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature2Content3" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                </div>
                                <div class="col-md-12" style="text-align: center; margin-bottom: 0px;">
                                    <h1>
                                        <asp:Label ID="LblVendorsBenefits" runat="server" /></h1>
                                </div>
                                <h3 class="title sr-only">
                                    <asp:Literal ID="LtrVendorsTitle" runat="server" /></h3>
                                <div class="desc" style="display: inline-block; text-align: justify;">
                                    <div style="float: left;">
                                        <asp:Image ID="ImgVendorBenefit0" AlternateText="Sign up" Style="margin-top: 30px;" ImageUrl="/assets/images/features/vendors/Sign_Up.png" runat="server" />
                                    </div>
                                    <div style="float: right; width: 450px; margin-top: 40px; margin-left: 20px;">
                                        <h3>
                                            <asp:Label ID="LblVendorBenefit0Header" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblVendorBenefit0Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="desc" style="display: inline-block; text-align: justify;">
                                    <div style="float: right;">
                                        <asp:Image ID="ImgVendorBenefit1" AlternateText="Promote Partner Program" ImageUrl="/assets/images/features/vendors/Promote_Partner_Program.png" runat="server" />
                                    </div>
                                    <div style="float: left; width: 450px; margin-top: 40px; margin-right: 20px;">
                                        <h3>
                                            <asp:Label ID="LblVendorBenefit1Header" runat="server" />
                                        </h3>
                                        <p>
                                            <asp:Label ID="LblVendorBenefit1Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="desc" style="display: inline-block; text-align: justify;">
                                    <div style="float: left;">
                                        <asp:Image ID="ImgVendorBenefit2" AlternateText="Get Matched" ImageUrl="/assets/images/features/vendors/Get_Matched.png" runat="server" />
                                    </div>
                                    <div style="float: right; width: 450px; margin-top: 40px; margin-left: 20px;">
                                        <h3>
                                            <asp:Label ID="LblVendorBenefit2Header" runat="server" />
                                        </h3>
                                        <p>
                                            <asp:Label ID="LblVendorBenefit2Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="desc" style="display: inline-block; margin-top: 20px; text-align: justify;">
                                    <div style="float: right; margin-top: 20px;">
                                        <asp:Image ID="ImgVendorBenefit3" AlternateText="Opportunities Pipeline" ImageUrl="/assets/images/features/vendors/Opportunities_Pipeline.png" runat="server" />
                                    </div>
                                    <div style="float: left; width: 450px; margin-top: 20px; margin-right: 20px;">
                                        <h3>
                                            <asp:Label ID="LblVendorBenefit3Header" runat="server" />
                                        </h3>
                                        <p>
                                            <asp:Label ID="LblVendorBenefit3Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="desc" style="display: inline-block; text-align: justify;">
                                    <div style="float: left;">
                                        <asp:Image ID="ImgVendorBenefit4" AlternateText="Collaborate" ImageUrl="/assets/images/features/vendors/Collaborate.png" runat="server" />
                                    </div>
                                    <div style="float: right; width: 450px; margin-top: 20px; margin-left: 20px;">
                                        <h3>
                                            <asp:Label ID="LblVendorBenefit4Header" runat="server" />
                                        </h3>
                                        <p>
                                            <asp:Label ID="LblVendorBenefit4Content" runat="server" />
                                            <a id="aLblVendorBenefit4ContentLearnMore" runat="server">
                                                <asp:Label ID="LblVendorBenefit4ContentLearnMore" runat="server" />
                                            </a>
                                        </p>
                                    </div>
                                </div>
                                <!--//desc-->
                                <!-- ******Features Section****** -->
                                <%--<div class="features-tabbed section">        
                                    <h2 class="title text-center">Product Features</h2>
                                    <div class="row">
                                        <div class=" text-center col-md-8 col-sm-10 col-xs-12 col-md-offset-2 col-sm-offset-1 col-xs-offset-0">
                                            <!-- Nav tabs -->
                                            <ul class="nav nav-tabs text-center" role="tablist">
                                                <li class="active"><a href="#feature-1" role="tab" data-toggle="tab"><i class="fa fa-cloud-upload"></i><br /><span class="hidden-sm hidden-xs">Feature One</span></a></li>
                                                <li><a href="#feature-2" role="tab" data-toggle="tab"><i class="fa fa-tachometer"></i><br /><span class="hidden-sm hidden-xs">Feature Two</span></a></li>
                                                <li><a href="#feature-3" role="tab" data-toggle="tab"><i class="fa fa-photo"></i><br /><span class="hidden-sm hidden-xs">Feature Three</span></a></li>
                                                <li class="last"><a href="#feature-4" role="tab" data-toggle="tab"><i class="fa fa-users"></i><br /><span class="hidden-sm hidden-xs">Feature Four</span></a></li>
                                            </ul><!--//nav-tabs-->                    
                                            <!-- Tab panes -->
                                            <div class="tab-content">
                                                <div class="tab-pane fade in active" id="Div1">
                                                    <h3 class="title sr-only">Feature One</h3>                                    
                                                    <figure class="figure text-center">
                                                        <img class="img-responsive" src="assets/images/features/screenshot-1.png" alt="Feature 1" />
                                                        <figcaption class="figure-caption">(Screenshot source: Coral - App &amp; website startup kit)</figcaption>
                                                    </figure>
                                                    <div class="desc text-left">
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc congue leo mauris, a fringilla nisi posuere tincidunt. Aliquam elementum lorem eget sollicitudin suscipit.</p>
                                                        <p>Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet.</p>
                                                        <ul class="list-unstyled">
                                                            <li><i class="fa fa-star"></i>Lorem ipsum dolor sit amet.</li>
                                                            <li><i class="fa fa-star"></i>Aliquam tincidunt mauris eu risus.</li>
                                                            <li><i class="fa fa-star"></i>Ultricies eget vel aliquam libero.</li>
                                                            <li><i class="fa fa-star"></i>Maecenas nec odio.</li>
                                                        </ul>                                
                                                        <p>Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. </p>
                                                    </div><!--//desc-->
                                                </div><!--//tab-pane-->
                                                <div class="tab-pane" id="Div2">
                                                    <h3 class="title sr-only">Feature Two</h3>                                    
                                                    <figure class="figure text-center">
                                                        <img class="img-responsive" src="assets/images/features/screenshot-2.png" alt="" />
                                                        <figcaption class="figure-caption">(Screenshot source: Coral - App &amp; website startup kit)</figcaption>
                                                    </figure>
                                                    <div class="desc text-left">
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc congue leo mauris, a fringilla nisi posuere tincidunt. Aliquam elementum lorem eget sollicitudin suscipit.</p>
                                                        <p>
                                                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis id nulla at libero ultricies tempus. Duis porta justo quam, ut ultrices felis posuere sit amet. Sed imperdiet bibendum est, sit amet sagittis ante sagittis eu. <a href="#">Integer fringilla</a> a purus sit amet laoreet. Ut consequat volutpat sapien sed lobortis. Nullam laoreet vitae justo nec dignissim. 
                                                        </p>
                                                        <blockquote>
                                                            <p>Viverra magna pellentesque in magnis gravida sit augue felis vehicula vestibulum semper penatibus justo ornare semper Gravida felis platea arcu mus non. Montes at posuere. Natoque.</p>
                                                        </blockquote>                                
                                                    </div><!--//desc-->
                                                </div><!--//tab-pane-->
                                                <div class="tab-pane" id="Div3">
                                                    <h3 class="title sr-only">Feature Three</h3>
                                                    <figure class="figure text-center">
                                                        <img class="img-responsive" src="assets/images/features/screenshot-3.png" alt="Screenshot source: Coral - App &amp; website startup kit" />
                                                        <figcaption class="figure-caption">(Screenshot source: Coral - App &amp; website startup kit)</figcaption>
                                                    </figure>
                                                    <div class="desc text-left">
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc congue leo mauris, a fringilla nisi posuere tincidunt. Aliquam elementum lorem eget sollicitudin suscipit.</p>
                                
                                                        <p>Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. </p>
                                
                                                        <div class="table-responsive">  
                                                            <table class="table table-striped">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Nullam consequat</th>
                                                                        <th>Commodo metus</th>
                                                                        <th>Dapibus porta</th>
                                                                        <th>Sed porta</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Faucibus purus</td>
                                                                        <td>Aliquam sit</td>
                                                                        <td>Sed porta leo</td>
                                                                        <td>Duis ut ornare dui</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Condimentum</td>
                                                                        <td>Curabitur tempus</td>
                                                                        <td>Fusce vehicula</td>
                                                                        <td>Nasceturs</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Neque a condimentum</td>
                                                                        <td>Cum sociis natoque</td>
                                                                        <td>Penatibus magnis</td>
                                                                        <td>Curabitur</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div><!--//table-responsive-->
                                                    </div><!--//desc-->                                    
                                                </div><!--//tab-pane-->
                                                <div class="tab-pane" id="feature-4">
                                                    <h3 class="title sr-only">Feature Four</h3>                                    
                                                    <figure class="figure text-center">
                                                        <img class="img-responsive" src="assets/images/features/screenshot-4.png" alt="Feature 4" />
                                                        <figcaption class="figure-caption">(Screenshot source: Coral - App &amp; website startup kit)</figcaption>
                                                    </figure>
                                                    <div class="desc text-left">
                                                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc congue leo mauris, a fringilla nisi posuere tincidunt. Aliquam elementum lorem eget sollicitudin suscipit.</p>
                                                        <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu</p>
                                                        <p class="box">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis id nulla at libero ultricies tempus. Duis porta justo quam, ut ultrices felis posuere sit amet. Sed imperdiet bibendum est, sit amet sagittis ante sagittis eu. Ut consequat volutpat sapien sed lobortis. Nullam laoreet vitae justo nec dignissim.
                                                        </p>
                                                    </div><!--//desc-->
                                                </div><!--//tab-pane-->
                                            </div><!--//tab-content-->
                                        </div><!--//col-md-x-->
                                    </div><!--//row-->        
                                </div>--%><!--//features-tabbed-->
                            </div>
                            <!--//tab-pane-->
                            <div class="tab-pane fade in" id="feature-3">
                                <div class="col-md-12">
                                    <div class="hidden">
                                        <iframe src="https://www.youtube.com/embed/wPdSo3Z2Ocg" width="100%" height="500px" frameborder="0" allowfullscreen style="margin-top: 50px; margin-bottom: 50px;"></iframe>
                                    </div>
                                </div>
                                <div class="steps section" style="display: none;">
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image AlternateText="Resellers sign up" ID="ImgResellerFeature1" runat="server" ImageUrl="/assets/images/home/resellers-sign-up-and-join.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature3Title1" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature3Content1" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image ID="ImgResellerFeature2" AlternateText="Resellers Specify" runat="server" ImageUrl="/assets/images/home/resellers-specify.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature3Title2" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature3Content2" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image ID="ImgResellerFeature3" AlternateText="Resellers Receive and determine" runat="server" ImageUrl="/assets/images/home/resellers-receive-and-determine.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature3Title3" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature3Content3" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                </div>
                                <div class="col-md-12" style="text-align: center;">
                                    <h1>
                                        <asp:Label ID="LblResellersBenefits" runat="server" /></h1>
                                </div>
                                <h3 class="title sr-only">
                                    <asp:Literal ID="LtrResellersTitle" runat="server" /></h3>
                                <div class="desc" style="display: inline-block; text-align: justify;">
                                    <div style="float: left;">
                                        <asp:Image ID="ImgResellerBenefit1" AlternateText="Sign up" Style="margin-top: 30px;" ImageUrl="/assets/images/features/resellers/Sign_Up.png" runat="server" />
                                    </div>
                                    <div style="float: right; width: 450px; margin-top: 20px; margin-left: 20px;">
                                        <h3>
                                            <asp:Label ID="LblResellerBenefit1Header" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblResellerBenefit1Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="desc" style="display: inline-block; text-align: justify;">
                                    <div style="float: right; margin-top: 20px;">
                                        <asp:Image ID="ImgResellerBenefit2" AlternateText="Reseller benefit 2" ImageUrl="/assets/images/features/resellers/Partner_With_New_Vendors.png" runat="server" />
                                    </div>
                                    <div style="float: left; width: 450px; margin-top: 20px; margin-right: 20px;">
                                        <h3>
                                            <asp:Label ID="LblResellerBenefit2Header" runat="server" />
                                        </h3>
                                        <p>
                                            <asp:Label ID="LblResellerBenefit2Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="desc" style="display: inline-block; text-align: justify;">
                                    <div style="float: left;">
                                        <asp:Image ID="ImgResellerBenefit3" AlternateText="Resellerbenefit 3" ImageUrl="/assets/images/features/resellers/Manage_Vendors.png" runat="server" />
                                    </div>
                                    <div style="float: right; width: 450px; margin-top: 30px; margin-left: 20px;">
                                        <h3>
                                            <asp:Label ID="LblResellerBenefit3Header" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblResellerBenefit3Content" runat="server" />
                                            <a id="aLblResellerBenefit3ContentLearnMore" runat="server">
                                                <asp:Label ID="LblResellerBenefit3ContentLearnMore" runat="server" />
                                            </a>
                                        </p>
                                    </div>
                                </div>
                                <div class="desc" style="display: inline-block; text-align: justify;">
                                    <div style="float: right; margin-top: 20px;">
                                        <asp:Image ID="ImgResellerBenefit4" AlternateText="Benefit 4" ImageUrl="/assets/images/features/resellers/Sales_Incentives.png" runat="server" />
                                    </div>
                                    <div style="float: left; width: 450px; margin-top: 20px; margin-right: 20px;">
                                        <h3>
                                            <asp:Label ID="LblResellerBenefit4Header" runat="server" />
                                        </h3>
                                        <p>
                                            <asp:Label ID="LblResellerBenefit4Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <!--//desc-->
                            </div>
                            <!--//tab-pane-->
                            <div class="tab-pane fade in" id="feature-4" style="display: none;">
                                <div class="steps section">
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image ID="ImgAPIDeveloperFeature1" AlternateText="Api Developer feature 1" runat="server" ImageUrl="/assets/images/home/developers-tell-us-about-you.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature4Title1" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature4Content1" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image ID="ImgAPIDeveloperFeature2" AlternateText="Api developer feature 2" runat="server" ImageUrl="/assets/images/home/developers-start-building.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature4Title2" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature4Content2" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                    <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                                        <asp:Image ID="ImgAPIDeveloperFeature3" AlternateText="Api developer feature 3" runat="server" ImageUrl="/assets/images/home/developers-community.png" />
                                        <h3 class="text" style="margin-top: -10px;">
                                            <asp:Label ID="LblFeature4Title3" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblFeature4Content3" runat="server" />
                                        </p>
                                    </div>
                                    <!--//step-->
                                </div>
                                <div class="col-md-12" style="margin-top: 50px; text-align: center; margin-bottom: 20px;">
                                    <h3>
                                        <asp:Label ID="LblDevelopersBenefits" runat="server" /></h3>
                                </div>
                                <h3 class="title sr-only">
                                    <asp:Literal ID="LtrApiDevelopersTitle" runat="server" /></h3>
                                <div class="col-md-12">
                                    <div class="desc text-left">
                                        <br />
                                        <h3>
                                            <asp:Label ID="LblApiDeveloperBenefit1Header" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblApiDeveloperBenefit1Content" runat="server" />
                                        </p>
                                        <br />
                                        <h3>
                                            <asp:Label ID="LblApiDeveloperBenefit2Header" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblApiDeveloperBenefit2Content" runat="server" />
                                        </p>
                                        <br />
                                        <h3>
                                            <asp:Label ID="LblApiDeveloperBenefit3Header" runat="server" /></h3>
                                        <p>
                                            <asp:Label ID="LblApiDeveloperBenefit3Content" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <!--//desc-->
                            </div>
                            <!--//tab-pane-->
                        </div>
                        <!--//tab-content-->
                    </div>
                    <!-- / row -->
                </div>
                <!--//col-md-x-->
            </div>
            <!--//row-->
        </div>
        <!--//container-->
    </div>
    <!--//features-tabbed-->
    <%--<div class="steps section">
        <div class="container">
            <h2 class="title text-center">3 Simple Steps to Get you started with Velocity</h2>
            <div class="row">
                 <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                     <h3 class="title"><span class="number">1</span><br /><span class="text">Sign up</span></h3>
                     <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.</p>
                 </div><!--//step-->
                 <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                     <h3 class="title"><span class="number">2</span><br /><span class="text">Choose your lorem ipsum</span></h3>
                     <p>Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.</p>
                 </div><!--//step-->
                 <div class="step text-center col-md-4 col-sm-4 col-xs-12">
                     <h3 class="title"><span class="number">3</span><br /><span class="text">Start building ipsum</span></h3>
                     <p>Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet.</p>
                 </div><!--//step-->
            </div><!--//row-->
            
            <div class="text-center"><a class="btn btn-cta btn-cta-primary" href="signup.html">Get Started - It's Free</a></div>
            
        </div><!--//container-->        
    </div>
    <div style="padding-bottom:100px"></div>--%>
    <div id="cta-section" class="section cta-section text-center pricing-cta-section">
        <div>
            <h2 class="title">
                <asp:Label ID="LblMoreThan" runat="server" /><span class="counting"><asp:Label ID="LblUsersNumber" runat="server" /></span><asp:Label ID="LblUsersAction" runat="server" /></h2>
            <p class="intro">
                <asp:Label ID="LblNoWait" runat="server" />
            </p>
            <p>
                <a id="aGetElioNow" runat="server" visible="false" href="#PaymentModal" role="button" data-toggle="modal" class="btn btn-cta btn-cta-primary">
                    <asp:Label ID="LblGetElioNow" runat="server" /></a>
                <asp:Button ID="BtnGetElioNow" Visible="false" OnClick="BtnSearchGoPremium_OnClick" CssClass="btn btn-lg btn-cta-primary" runat="server" />
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
