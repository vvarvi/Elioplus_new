<%@ Page Title="" Language="C#" MasterPageFile="~/ElioplusMaster.Master" AutoEventWireup="true" CodeBehind="PersonProfile.aspx.cs" Inherits="WdS.ElioPlus.PersonProfile" %>

<asp:Content ID="ProfileHeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Elioplus helps Software and SaaS vendors and resellers to find new Software and SaaS partners according to their partner program structure" />
    <meta name="keywords" content="partner program, service providers, channel partners, partner program, business development, partnership opportunities, resell SaaS" />    
    <script type="text/javascript" src="/assets/plugins/jquery-1.11.2.min.js"></script>
</asp:Content>
<asp:Content ID="ProfileMainContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadWindowManager runat="server" ID="ProfileRadWindowManager"></telerik:RadWindowManager>      
    <div class="bg-slider-wrapper">
        <div class="flexslider bg-slider">
            <ul class="slides">
                <li class="slide slide-3"></li>
            </ul>
        </div>
    </div>   
    <!--//bg-slider-wrapper-->
    <div class="sections-wrapper bglight" style="padding-top:30px; padding-bottom:50px; background-color:#fff !important;">      
        <div class="container">
            <div class="clearfix">

                <div class="col-md-12">

                    <div class="row">
                        <div class="col-md-3" style="float:right;">
                            <asp:Button ID="BtnClosePage" OnClick="BtnClosePage_Click" Text="close page" CssClass="btn btn-cta btn-cta-secondary" runat="server" />
                        </div>

                        <div class="col-md-3 col-sm-3" style="width:105px !important;">
                            <p>
                                <asp:Image ID="ImgPersonLogo" Width="70" Height="70" style="border-radius:35px;" CssClass="img-responsive" runat="server" />                                
                            </p>
                        </div>                        
                        <div class="col-md-6 col-sm-7" style="margin-top:-25px;font-size:30px !important;">
                            <div style="float:left; display:inline-block; padding-right:20px;">
                                <h1>
                                    <asp:Label ID="LblFamilyNameContent" style="font-size:30px !important;" runat="server" />
                                    <asp:Label ID="LblGivenNameContent" style="font-size:30px !important;" runat="server" />
                                </h1>
                                <h2 class="seo-title" style="font-size:15px !important; font-weight:normal !important;">
                                    <asp:Label ID="LblPersonTitleTitle" Font-Bold="false" runat="server" />
                                    <asp:Label ID="LblPersonTitle" Visible="false" Font-Bold="false" runat="server" />
                                </h2>
                            </div>
                            <div class="" style="margin-top:18px;">
                                <a id="aFacebookHandleContent" href="https://www.facebook.com" target="_blank" runat="server">
                                    <asp:Image ID="ImgFacebook" Width="30" Height="30" runat="server" ImageUrl="~/images/icons/social/facebook_circle_s.png" />
                                </a>
                                <a id="aTwitterHandleContent" href="https://www.twitter.com" target="_blank" runat="server">
                                    <asp:Image ID="ImgTwitter" Width="30" Height="30" runat="server" ImageUrl="~/images/icons/social/twitter_circle_s.png" />
                                </a>
                                <a id="aLinkedinHandleContent" href="https://www.linkedin.com" target="_blank" runat="server">
                                    <asp:Image ID="ImgLinkedin" Width="30" Height="30" runat="server" ImageUrl="~/images/icons/social/linkedin_circle_s.png" />
                                </a>
                                <a id="aGoogleMailHandleContent" href="https://www.gmail.com" target="_blank" runat="server">
                                    <asp:Image ID="ImgGoogleMail" Width="28" Height="28" runat="server" ImageUrl="~/images/icons/social/gmail_circle_s.png" />
                                </a>
                                <a id="aAboutMeHandleContent" href="https://www.about.me" target="_blank" runat="server">
                                    <asp:Image ID="ImgAboutMe" Width="28" Height="28" style="border-radius:20px;" runat="server" ImageUrl="~/images/icons/social/about_me_circle_s.png" />
                                </a>
                            </div>
                            <a id="aSendMessage" runat="server" visible="false" class="btn btn-cta btn-cta-primary" href="#myModal" role="button" data-toggle="modal">
                                <i style="margin: 0;" class="fa fa-envelope-o"></i><asp:Label ID="LblSendMessage" runat="server" />
                            </a> &nbsp;
                            <a id="aSaveProfile" runat="server" visible="false" onserverclick="BtnSave_OnClick" class="btn btn-cta btn-cta-secondary">
                                <i class="fa fa-bookmark-o"></i><asp:Label ID="LblSaveProfile" runat="server" />
                            </a> &nbsp;
                            <a id="aSaveProfileNotFull" visible="false" runat="server"  class="btn btn-cta btn-cta-secondary" href="#MdGoFull" role="button" data-toggle="modal">
                                <i class="fa fa-bookmark-o"></i><asp:Label ID="LblSaveProfileNotFull" runat="server" />
                            </a> &nbsp;
                            <a id="aViewProductDemo" onserverclick="ViewProductDemo_OnClick" runat="server" visible="false" class="btn btn-cta btn-cta-secondary">
                                <i class="fa fa-bookmark-o"></i><asp:Label ID="LblViewProductDemo" runat="server" />
                            </a> &nbsp;
                            <a id="aViewProductDemoNotFull" visible="false" runat="server" class="btn btn-cta btn-cta-secondary" href="#MdGoFull" role="button" data-toggle="modal">
                                <i class="fa fa-bookmark-o"></i><asp:Label ID="LblViewProductDemoNotFull" runat="server" />
                            </a> &nbsp;                                                        
                        </div>                        
                        <div class="col-md-12 col-sm-12" style="margin-top:-50px !important;">
                            <div class="offer-specification">
                                <table class="table table">
                                    <tr id="Tr2" runat="server">
                                        <td style="background-color:#02a8f3 !important; color:#fff;">
                                            <asp:Label ID="Label1" Text="Personal Details" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table class="table table">
                                    <tr id="Tr1" runat="server">
                                        <td style="width:200px;">
                                            <asp:Label ID="LblEmailTitle" Text="Email" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblEmailContent" runat="server" />
                                        </td>
                                        <td style="width:200px;">
                                            <asp:Label ID="LblPhoneTitle" Text="Phone number" runat="server" />
                                        </td>
                                        <td style="width:200px;">
                                            <div style="display:inline-block; float:left;padding:5px;">
                                                <asp:Image ID="ImgPersonPhone" Width="20" Height="20" runat="server" ImageUrl="~/images/icons/small/phone.png" />
                                            </div>
                                            <asp:Label ID="LblPhoneContent" Font-Bold="true" runat="server" />
                                        </td>                                        
                                    </tr>
                                    <tr id="Tr16" runat="server">
                                        <td style="width:200px;">
                                            <asp:Label ID="LblWebsiteTitle" Text="Website" runat="server" />
                                        </td>
                                        <td>
                                            <a id="aWebsiteContent" runat="server">
                                                <asp:Label ID="LblWebsiteContent" runat="server" />
                                            </a>
                                        </td>
                                        <td style="width:200px;">
                                            <asp:Label ID="LblSeniorityTitle" Text="Seniority" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblSeniorityContent" runat="server" />
                                        </td>
                                        <%--<td style="width:200px;">
                                            <asp:Label ID="LblAccountStatusTitle" Text="Customer status" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblAccountStatusContent" runat="server" />
                                        </td>--%>
                                    </tr>
                                    <tr id="Tr4" runat="server">
                                        <td>
                                            <asp:Label ID="LblBioTitle" Text="Bio" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblBioContent" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblPositionTitle" Text="Position" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblPositionContent" runat="server" />
                                        </td>
                                    </tr>                                    
                                    <tr id="Tr17" runat="server">
                                        <td style="width:200px;">
                                            <asp:Label ID="LblAddressTitle" Text="Address" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblAddressContent" runat="server" />
                                        </td>
                                        <td style="width:200px;">
                                            <asp:Label ID="LblCountryTitle" Text="Country" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCountryContent" runat="server" />
                                        </td>                                        
                                    </tr>
                                    <tr id="Tr3" runat="server">
                                        <td style="width:200px;">
                                            <asp:Label ID="LblTimeZoneTitle" Text="Time zone" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblTimeZoneContent" runat="server" />
                                        </td>
                                        <td>
                                            
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                    <%--<tr id="Tr10" runat="server">
                                        <td>
                                            <asp:Label ID="LblDescriptionTitle" Text="Description" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblDescriptionContent" runat="server" />
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr id="Tr11" runat="server">
                                        <td style="width:200px;">
                                            <asp:Label ID="LblCommunitySummaryTitle" Text="Community summary" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCommunitySummaryContent" runat="server" />
                                        </td> 
                                        <td></td>
                                        <td></td>                                       
                                    </tr>--%>
                                </table>
                                <%--<table class="table table">
                                    <tr id="Tr20" runat="server">
                                        <td style="background-color:#02a8f3 !important; color:#fff;">
                                            <asp:Label ID="Label6" Text="Personal Social Media Details" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table class="table table">
                                    <tr id="Tr9" runat="server">
                                        <td style="width:200px;">
                                            <asp:Label ID="LblLinkedinHandleTitle" Text="Linkedin handle" runat="server" />
                                        </td>
                                        <td>
                                            <a id="aLinkedinHandleContent" target="_blank" runat="server">
                                                <asp:Label ID="LblLinkedinHandleContent" runat="server" />
                                            </a>
                                        </td>
                                        <td style="width:200px;">
                                            <asp:Label ID="LblAboutMeHandleTitle" Text="About me handle" runat="server" />
                                        </td>
                                        <td style="width:300px;">
                                            <a id="aAboutMeHandleContent" target="_blank" runat="server">
                                                <asp:Label ID="LblAboutMeHandleContent" runat="server" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblTwitterHandleTitle" Text="Twitter handle" runat="server" />
                                        </td>
                                        <td>
                                            <a id="aTwitterHandleContent" target="_blank" runat="server">
                                                <asp:Label ID="LblTwitterHandleContent" runat="server" />
                                            </a>
                                        </td>
                                        <td>

                                        </td>
                                        <td></td>
                                    </tr>
                                </table>--%>                                
                            </div>
                        </div> 

                        <div class="col-md-3 col-sm-3" style="width:105px !important; margin-top:50px;">
                            <p>
                                <asp:Image ID="ImgCompanyLogo" Width="70" Height="70" style="border-radius:35px;" CssClass="img-responsive" runat="server" />                                
                            </p>
                        </div>
                        <div class="col-md-6 col-sm-7"> 
                            <div style="float:left; display:inline-block; padding-right:20px; margin-top:25px;">
                                <h1>
                                    <asp:Label ID="LblCompanyName" style="padding-top:50px;font-size:30px !important;" runat="server" />
                                </h1>
                                <h2 class="seo-title" style="font-size:15px !important; font-weight:normal !important;">
                                    <asp:Label ID="LblCompanyTypeContent" runat="server" />
                                    <asp:Label ID="Label5" Visible="false" Font-Bold="false" runat="server" />
                                </h2>
                            </div>
                            <div class="" style="margin-top:50px;">
                                <a id="aCompanyFacebookHandleContent" href="https://www.facebook.com" target="_blank" runat="server">
                                    <asp:Image ID="ImgCompanyFacebook" Width="30" Height="30" runat="server" ImageUrl="~/images/icons/social/facebook_circle_s.png" />
                                </a>
                                <a id="aCompanyTwitterSiteContent" href="https://www.twitter.com" target="_blank" runat="server">
                                    <asp:Image ID="ImgCompanyTwitter" Width="30" Height="30" runat="server" ImageUrl="~/images/icons/social/twitter_circle_s.png" />
                                </a>
                                <a id="aCompanyLinkedinHandleContent" href="https://www.linkedin.com" target="_blank" runat="server">
                                    <asp:Image ID="ImgCompanyLinkedin" Width="30" Height="30" runat="server" ImageUrl="~/images/icons/social/linkedin_circle_s.png" />
                                </a>
                                <a id="aCompanyGoogleMailHandleContent" href="https://www.gmail.com" target="_blank" runat="server">
                                    <asp:Image ID="ImgCompanyGoogleMail" Width="28" Height="28" runat="server" ImageUrl="~/images/icons/social/gmail_circle_s.png" />
                                </a>
                                <a id="aCompanyAboutMeHandleContent" href="https://www.about.me" target="_blank" runat="server">
                                    <asp:Image ID="ImgCompanyAboutMe" Width="28" Height="28" style="border-radius:20px;" runat="server" ImageUrl="~/images/icons/social/about_me_circle_s.png" />
                                </a>
                            </div>
                            <a id="a1" runat="server" visible="false" class="btn btn-cta btn-cta-primary" href="#myModal" role="button" data-toggle="modal">
                                <i style="margin: 0;" class="fa fa-envelope-o"></i><asp:Label ID="Label10" runat="server" />
                            </a> &nbsp;
                            <a id="a2" runat="server" visible="false" onserverclick="BtnSave_OnClick" class="btn btn-cta btn-cta-secondary">
                                <i class="fa fa-bookmark-o"></i><asp:Label ID="Label11" runat="server" />
                            </a> &nbsp;
                            <a id="a3" visible="false" runat="server"  class="btn btn-cta btn-cta-secondary" href="#MdGoFull" role="button" data-toggle="modal">
                                <i class="fa fa-bookmark-o"></i><asp:Label ID="Label12" runat="server" />
                            </a> &nbsp;
                            <a id="a4" onserverclick="ViewProductDemo_OnClick" runat="server" visible="false" class="btn btn-cta btn-cta-secondary">
                                <i class="fa fa-bookmark-o"></i><asp:Label ID="Label13" runat="server" />
                            </a> &nbsp;
                            <a id="a5" visible="false" runat="server" class="btn btn-cta btn-cta-secondary" href="#MdGoFull" role="button" data-toggle="modal">
                                <i class="fa fa-bookmark-o"></i><asp:Label ID="Label14" runat="server" />
                            </a> &nbsp;                                                        
                        </div>
                        <div class="col-md-12 col-sm-12" style="margin-top:-50px !important;">
                            <div class="offer-specification">                                
                                <strong><i class="fa fa-list-alt" style="margin-right:10px; display:none;"></i><asp:Label ID="Label2" runat="server" /></strong>
                                <table class="table table">
                                    <tr id="Tr22" runat="server">
                                        <td style="background-color:#02a8f3 !important; color:#fff;">
                                            <asp:Label ID="Label7" Text="Company Details" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table class="table table">
                                    <tr id="Tr5" runat="server">
                                        <td style="width:200px;">
                                            <asp:Label ID="LblCompanyDescriptionTitle" Text="Company description" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyDescriptionContent" runat="server" />
                                        </td>                                        
                                        <td style="width:200px;">
                                            <asp:Label ID="LblCompanyPhonesTitle" Text="Site phone numbers" runat="server" />                                            
                                        </td>
                                        <td style="width:200px;">
                                            <div style="display:inline-block; float:left;padding:5px;">
                                                <asp:Image ID="ImgPhone" Width="20" Height="20" runat="server" ImageUrl="~/images/icons/small/phone.png" />
                                            </div>
                                            <div style="display:inline-block; float:right;padding:5px; margin-right:20px;">
                                                <asp:Label ID="LblCompanyPhonesContent" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="Tr8" runat="server">
                                        <td>
                                            <asp:Label ID="LblCompanyEmployeesNumberTitle" Text="Employees number" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyEmployeesNumberContent" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyEmployeesRangeTitle" Text="Employees range" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyEmployeesRangeContent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="Tr12" runat="server">
                                        <td>
                                            <asp:Label ID="LblCompanyAnnualRevenueTitle" Text="Estimated revenue" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyAnnualRevenueContent" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyAnnualRangeTitle" Text="Annual range" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyAnnualRangeContent" runat="server" />
                                        </td>
                                    </tr>  
                                    <tr id="Tr6" runat="server">
                                        <td>
                                            <asp:Label ID="LblSectorTitle" Text="Sector" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblSectorContent" runat="server" />
                                        </td>                                    
                                        <td>
                                            <asp:Label ID="LblIndustryGroupTitle" Text="Industry group" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblIndustryGroupContent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="Tr23" runat="server">
                                        <td>
                                            <asp:Label ID="LblIndustryTitle" Text="Industry" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblIndustryContent" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblSubIndustryTitle" Text="Sub industry" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblSubIndustryContent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="Tr7" runat="server">
                                         <td>
                                            <asp:Label ID="LblCompanyDomainTitle" Text="Domain" runat="server" />
                                        </td>
                                        <td>
                                            <a id="aCompanyDomainContent" runat="server">
                                                <asp:Label ID="LblCompanyDomainContent" runat="server" />
                                            </a>
                                        </td>                                        
                                        <td>
                                            <asp:Label ID="LblCompanyFoundedYearTitle" Text="Founded year" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyFoundedYearContent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="Tr24" runat="server">
                                        <td>
                                            <asp:Label ID="LblCompanyLocationTitle" Text="Company location" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyLocationContent" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyFundAmountTitle" Text="Fund amount" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyFundAmountContent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="Tr21" runat="server">
                                        <td>
                                            <asp:Label ID="LblCompanyTagsTitle" Text="Company tags" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyTagsContent" runat="server" />
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table> 
                                <%--<table class="table table">
                                    <tr id="Tr27" runat="server">
                                        <td style="background-color:#02a8f3 !important; color:#fff;">
                                            <asp:Label ID="Label4" Text="Company Social Media Details" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table class="table table">
                                    <tr id="Tr13" runat="server">
                                        <td style="width:200px;">
                                            <asp:Label ID="LblCompanyFacebookHandleTitle" Text="Facebook handle" runat="server" />
                                        </td>
                                        <td>
                                            <a id="aCompanyFacebookHandleContent" target="_blank" runat="server">
                                                <asp:Label ID="LblCompanyFacebookHandleContent" runat="server" />
                                            </a>
                                        </td>
                                        <td style="width:200px;">
                                            <asp:Label ID="LblCompanyFacebookLikesTitle" Text="Facebook likes" runat="server" />
                                        </td>
                                        <td style="width:200px;">
                                            <asp:Label ID="LblCompanyFacebookLikesContent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="Tr25" runat="server">
                                        <td>
                                            <asp:Label ID="LblCompanyTwitterBioTitle" Text="Twitter bio" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyTwitterBioContent" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyTwitterFollowersTitle" Text="Twitter followers" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyTwitterFollowersContent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="Tr14" runat="server">
                                        <td>
                                            <asp:Label ID="LblCompanyTwitterFollowingTitle" Text="Twitter following" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyTwitterFollowingContent" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyTwitterLocationTitle" Text="Twitter location" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCompanyTwitterLocationContent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="Tr15" runat="server">
                                        <td>
                                            <asp:Label ID="LblCompanyTwitterSiteTitle" Text="Twitter site" runat="server" />
                                        </td>
                                        <td>
                                            <a id="aCompanyTwitterSiteContent" target="_blank" runat="server">
                                                <asp:Label ID="LblCompanyTwitterSiteContent" runat="server" />
                                            </a>
                                        </td>                                    
                                        <td>
                                            <asp:Label ID="LblCompanyTwitterAvatarTitle" Text="Twitter avatar" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Image ID="ImgCompanyTwitterAvatar" CssClass="img-responsive" runat="server" />                                            
                                        </td>
                                    </tr>
                                    <tr id="Tr26" runat="server">
                                        <td>
                                            <asp:Label ID="LblCompanyCrunchBaseTitle" Text="Crunch base handle" runat="server" />
                                        </td>
                                        <td>
                                            <a id="aCompanyCrunchBaseContent" target="_blank" runat="server">
                                                <asp:Label ID="LblCompanyCrunchBaseContent" runat="server" />
                                            </a>
                                        </td>
                                        <td>
                                            
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                </table>--%>                           
                            </div>
                        </div>
                                            
                        <%--<p>
                            <i class="fa fa-clock-o"></i>
                            <small>
                                <asp:Label ID="LblRegistrationDateTitle" Text="Registration date:" runat="server" />
                                <asp:Label ID="LblRegistrationDateContent" runat="server" />
                                <div id="div11" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                    <strong><asp:Label ID="Label41" runat="server" /></strong><asp:Label ID="Label42" runat="server" />
                                </div>
                            </small>
                        </p>--%>
                    </div>

                </div>
                <!-- / col-md-12 -->
            </div>
        </div>
    </div>
    <!--//section-wrapper-->    
    <!-- Contact form (modal view) -->
    <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="BtnCloseModal" OnClick="BtnCancelMsg_OnClick" CssClass="close" aria-hidden="true" runat="server" />
                            <h3 id="myModalLabel"><asp:Label ID="LblMessageHeader" runat="server" /></h3>
                        </div>
                        <div class="modal-body">
                            <form class="form-horizontal col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="LblMessageName" runat="server" /><asp:TextBox ID="TbxMessageName" CssClass="form-control" placeholder="Name" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblMessagePhone" runat="server" /><asp:TextBox ID="TbxMessagePhone" CssClass="form-control phone" placeholder="Phone (optional)" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                                <div id="divRegEmail" runat="server" visible="false" class="form-group">
                                    <asp:Label ID="LblMessageEmail" runat="server" /><asp:TextBox ID="TbxMessageEmail" MaxLength="100" CssClass="form-control email" placeholder="E-mail" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                                <div id="divComEmail" runat="server" visible="false" class="form-group">
                                    <asp:Label ID="DdlMessageEmail" runat="server" /><asp:DropDownList ID="DdlCompanyMessageEmail" CssClass="form-control email" placeholder="E-mail" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblMessageSubject" runat="server" /><asp:TextBox ID="TbxMessageSubject" CssClass="form-control" MaxLength="30" placeholder="Subject" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblMessageContent" runat="server" /><asp:TextBox ID="TbxMessageContent" CssClass="form-control" TextMode="MultiLine" MaxLength="2000" Rows="5" placeholder="Enter your message here" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnMessageSend" OnClick="BtnSend_OnClick" CssClass="btn btn-success" runat="server" /> &nbsp;
                            <asp:Button ID="BtnMessageCancel" OnClick="BtnCancelMsg_OnClick" CssClass="btn" aria-hidden="true" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <div id="divWarningMsg" runat="server" visible="false" class="alert alert-danger" style="float:left;">
                                <strong><asp:Label ID="LblWarningMsg" runat="server" /></strong><asp:Label ID="LblWarningMsgContent" runat="server" />
                            </div>
                            <div id="divSuccessMsg" runat="server" visible="false" class="alert alert-success" style="float:left;">
                                <strong><asp:Label ID="LblSuccessMsg" runat="server" /></strong><asp:Label ID="LblSuccessMsgContent" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>    
    <div id="MdAddReview" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="ModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="BtnCloseReview" OnClick="BtnCancelRvw_OnClick" CssClass="close" aria-hidden="true" runat="server" />
                            <h3 id="ModalLabel"><asp:Label ID="LblReviewHeader" runat="server" /></h3>
                        </div>
                        <div class="modal-body" style="display: inline-block;">
                            <div class="form-horizontal col-sm-12" style="width: 280%;">
                                <div class="form-group">
                                    <asp:TextBox ID="TbxReviewContent" CssClass="form-control" TextMode="MultiLine" MaxLength="490" Rows="10" placeholder="Enter your review here" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnAddReview" OnClick="BtnSaveReview_OnClick" CssClass="btn btn-success" runat="server" /> &nbsp;
                            <asp:Button ID="BtnCancelReview" OnClick="BtnCancelRvw_OnClick" CssClass="btn" aria-hidden="true" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <div id="divWarningReview" runat="server" visible="false" class="alert alert-danger" style="">
                                <strong><asp:Label ID="LblWarningReview" runat="server" /></strong><asp:Label ID="LblWarningReviewMessage" runat="server" />
                            </div>
                            <div id="divSuccessReview" runat="server" visible="false" class="alert alert-success" style="">
                                <strong><asp:Label ID="LblSuccessReview" runat="server" /></strong><asp:Label ID="LblSuccessReviewMessage" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="MdGoFull" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="ModalLabel" aria-hidden="true">
    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
        <ContentTemplate>        
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Button ID="BtnCloseMessage" data-dismiss="modal" CssClass="close" aria-hidden="true" runat="server" />
                    <h3 id="MessageLabel"><asp:Label ID="LblMessageGoFull" Font-Size="16" runat="server" /></h3>
                </div>
                <div class="modal-body" style="display: inline-block; margin-bottom:-40px;">
                    <div class="form-horizontal col-sm-12">
                        <div class="form-group">
                            <div id="divMessage" runat="server" class="alert alert-info" style="float:left;">
                                <strong><asp:Label ID="LblGoFullTitle" runat="server" /></strong><asp:Label ID="LblGoFullContent" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnCancelMessage" data-dismiss="modal" CssClass="btn btn-success" runat="server" />                    
                </div>                
            </div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>            
    </div>
    <script type="text/javascript">
        function CloseReviewPopUp() {
            $('#MdAddReview').modal('hide');
        }
    </script>
    <script type="text/javascript">
        function CloseMessagePopUp() {
            $('#myModal').modal('hide');
        }
    </script>    
</asp:Content>
