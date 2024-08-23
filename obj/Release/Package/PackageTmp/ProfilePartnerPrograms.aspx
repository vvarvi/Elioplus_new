<%@ Page Title="" Language="C#" MasterPageFile="~/ElioplusMaster.Master" AutoEventWireup="true" CodeBehind="ProfilePartnerPrograms.aspx.cs" Inherits="WdS.ElioPlus.ProfilePartnerPrograms" %>

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
    <div class="sections-wrapper bglight profile-cont">      
        <div class="container">
            <div class="clearfix">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-2 col-sm-2">
                            <p>
                                <asp:Image ID="ImgCompanyLogo" CssClass="img-responsive" runat="server" />                                
                            </p>
                        </div>
                        <div class="col-md-6 col-sm-7">                                                      
                            <h1><asp:Label ID="LblCompanyName" runat="server" /></h1>                            
                            <h2 class="seo-title"><asp:Label ID="LblCompanyType" Font-Bold="true" runat="server" /></h2>
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
                        <div class="col-md-4 col-sm-3">
                            <p class="profile-rate">                     
                                <strong style="float:right; margin-bottom:5px;">                                                    
                                    <asp:Panel ID="PnlRating" runat="server">
                                        <asp:ImageButton ID="r1" OnClick="R1_OnClick" runat="server" />
                                        <telerik:RadToolTip ID="Rttp1" TargetControlID="r1" Position="TopRight" Animation="Fade" runat="server" />
                                        <asp:ImageButton ID="r2" OnClick="R2_OnClick" runat="server" />
                                        <telerik:RadToolTip ID="Rttp2" TargetControlID="r2" Position="TopRight" Animation="Fade"  runat="server" />
                                        <asp:ImageButton ID="r3" OnClick="R3_OnClick" runat="server" />
                                        <telerik:RadToolTip ID="Rttp3" TargetControlID="r3" Position="TopRight" Animation="Fade" runat="server" />
                                        <asp:ImageButton ID="r4" OnClick="R4_OnClick" runat="server" />
                                        <telerik:RadToolTip ID="Rttp4" TargetControlID="r4" Position="TopRight" Animation="Fade" runat="server" />
                                        <asp:ImageButton ID="r5" OnClick="R5_OnClick" runat="server" />
                                        <telerik:RadToolTip ID="Rttp5" TargetControlID="r5" Position="TopRight" Animation="Fade" runat="server" />
                                        <asp:Label ID="LblAverage" Text="({average})" style="margin-left:5px;" runat="server" />                                                        
                                        <asp:Label ID="LblAverageRating" style="margin-left:0px;" runat="server" />    
                                        <asp:Image ID="ImgAverageRating" ImageUrl="/Images/icons/small/info.png" CssClass="pointer" style="margin-top:-3px;" runat="server" />
                                        <telerik:RadToolTip ID="RttpRating" TargetControlID="ImgAverageRating" Position="TopRight" Animation="Fade" runat="server" />
                                        <div id="divRatingNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center;">
                                            <strong><asp:Label ID="LblRatingNotFull" runat="server" /></strong><asp:Label ID="LblRatingNotFullContent" runat="server" />
                                        </div>
                                    </asp:Panel>
                                </strong>
                            </p>
                            <%--<p>
                                <a class="fontwb" href="#"><i class="fa fa-external-link"></i><asp:Label ID="LblSeeDemo" runat="server" /></a>
                            </p>--%>
                            <a id="aAddReview" href="#MdAddReview" role="button" class="btn btn-cta btn-cta-default" data-toggle="modal" style="float:right" runat="server"><i class="fa fa-plus-square-o"></i>
                                <asp:Label ID="LblAddReview" runat="server" />
                            </a>                            
                        </div>
                    </div>
                    <div id="rowMsgNotFull" runat="server" visible="false" class="row message-not-full" >
                        <div id="divMsgNotFull" runat="server" class="alert alert-danger" style="border: thin solid #02a8f3; float:left; background-color:#ffffff; color:#02a8f3;">                            
                            <strong><asp:Label ID="LblMsgNotFull" runat="server" /></strong><asp:Label ID="LblMsgNotFullContent" runat="server" />
                        </div>
                        <div id="divCreateAccount" runat="server">
                            <a id="aCreateAccount" runat="server" class="btn btn-cta btn-cta-primary" style="height:54px; padding-top:15px; font-size:16px;"><asp:Label ID="LblCreateAccount" runat="server" /></a>
                        </div>
                    </div>
                    <div id="rowMsgSaveProfile" runat="server" visible="false" class="row" style="margin-top:5%; margin-left:25%; margin-right:25%; text-align:center;">
                        <div id="divErrorSaveProfile" runat="server" visible="false" class="alert alert-danger">
                            <strong><asp:Label ID="LblErrorSaveProfile" runat="server" /></strong><asp:Label ID="LblErrorSaveProfileContent" runat="server" />
                        </div>
                        <div id="divScsSaveProfile" runat="server" visible="false" class="alert alert-success">
                            <strong><asp:Label ID="LblScsSaveProfile" runat="server" /></strong><asp:Label ID="LblSuccessSaveProfileContent" runat="server" />
                        </div>
                    </div>                    
                    <div class="row">
                        <div class="col-md-6">
                            <div style="padding-right: 30px;">
                                <br />
                                <br />
                                <p class="text-justify">
                                    <strong style="margin-bottom:10px; display:inline-block;"><asp:Label ID="LblOverviewTitle" runat="server" /></strong><br />
                                    <asp:Label ID="LblOverview" runat="server" />
                                </p>
                                <hr />
                                <p class="text-justify">
                                    <strong style="margin-bottom:10px; display:inline-block;"><asp:Label ID="LblDescriptionTitle" runat="server" /></strong><br />
                                    <asp:Label ID="LblDescription" runat="server" />
                                    <div id="divDescriptionNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center;">
                                        <strong><asp:Label ID="LblDescriptionNotFull" runat="server" /></strong><asp:Label ID="LblDescriptionNotFullContent" runat="server" />
                                    </div>
                                </p>
                                <hr />                                
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div style="float:right">
                                <i id="iPdf" runat="server" visible="false" class="fa fa-2x fa-file-pdf-o" style="float:right; margin-left:5px;"></i>
                                <div style="float:right">
                                <a id="aPdf" runat="server" class="btn btn-cta btn-cta-secondary"><asp:Label ID="LblPdfValue" runat="server" /></a>
                                <div id="divPdfNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                    <strong><asp:Label ID="LblPdfNotFull" runat="server" /></strong><asp:Label ID="LblPdfNotFullContent" runat="server" />
                                </div>
                                </div>                                
                            </div>                          
                            <div class="offer-specification">                                
                                <strong><i class="fa fa-list-alt" style="margin-right:10px;"></i><asp:Label ID="LblCompanyInfoTitle" runat="server" /></strong>
                                <br />
                                <br />
                                <table class="table table-bordered">
                                    <tr id="rowIndustries" runat="server">
                                        <td>
                                            <asp:Label ID="LblIndustryTitle" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblIndustries" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="rowSubcategories" runat="server">
                                        <td>
                                            <asp:Label ID="LblSubcategoriesTitle" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblSubcategories" runat="server" />
                                            <div id="divSubcategoriesNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                                <strong><asp:Label ID="LblSubcategoriesNotFull" runat="server" /></strong><asp:Label ID="LblSubcategoriesNotFullContent" runat="server" />
                                            </div>
                                            <div id="divSubcategoriesNotPremium" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                                <strong><asp:Label ID="LblSubcategoriesNotPremium" runat="server" /></strong><asp:Label ID="LblSubcategoriesNotPremiumContent" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="rowMarkets" runat="server">
                                        <td>
                                            <asp:Label ID="LblMarketsTitle" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblMarkets" runat="server" />
                                            <div id="divMarketsNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                                <strong><asp:Label ID="LblMarketsNotFull" runat="server" /></strong><asp:Label ID="LblMarketsNotFullContent" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="rowPrograms" runat="server">
                                        <td>
                                            <asp:Label ID="LblProgramsTitle" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblPrograms" runat="server" />
                                            <div id="divProgramsNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                                <strong><asp:Label ID="LblProgramsNotFull" runat="server" /></strong><asp:Label ID="LblProgramsNotFullContent" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="rowApies" runat="server">
                                        <td>
                                            <asp:Label ID="LblApiTitle" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblApi" runat="server" />
                                            <div id="divApiNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                                <strong><asp:Label ID="LblApiNotFull" runat="server" /></strong><asp:Label ID="LblApiNotFullContent" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblTotalReviewsTitle" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblTotalReviews" runat="server" />
                                            <div id="divTotalReviewsNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                                <strong><asp:Label ID="LblTotalReviewsNotFull" runat="server" /></strong><asp:Label ID="LblTotalReviewsNotFullContent" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="rowWebsite" runat="server">
                                        <td>
                                            <asp:Label ID="LblWebsiteTitle" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblWebsite" runat="server" />
                                            <asp:HyperLink ID="HplnkWebsite" Target="_blank" runat="server" />
                                            <div id="divWebsiteNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                                <strong><asp:Label ID="LblWebsiteNotFull" runat="server" /></strong><asp:Label ID="LblWebsiteNotFullContent" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblAddressTitle" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblAddress" runat="server" />
                                            <div id="divAddressNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                                <strong><asp:Label ID="LblAddressNotFull" runat="server" /></strong><asp:Label ID="LblAddressNotFullContent" runat="server" />
                                            </div>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblPhoneTitle" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblPhone" runat="server" />
                                            <div id="divPhoneNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                                <strong><asp:Label ID="LblPhoneNotFull" runat="server" /></strong><asp:Label ID="LblPhoneNotFullContent" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="rowMashape" runat="server">
                                        <td>
                                            <asp:Label ID="LblMashapeTitle" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HplnkMashape" Target="_blank" runat="server" />
                                            <div id="divMashapeNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                                <strong><asp:Label ID="LblMashapeNotFull" runat="server" /></strong><asp:Label ID="LblMashapeNotFullContent" runat="server" />
                                            </div>
                                        </td>
                                    </tr>                                    
                                </table>
                                <p>
                                    <i class="fa fa-clock-o"></i>
                                    <small>
                                        <asp:Label ID="LblRegDateTitle" runat="server" />
                                        <asp:Label ID="LblRegDate" runat="server" />
                                        <div id="divRegDateNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                            <strong><asp:Label ID="LblRegDateNotFull" runat="server" /></strong><asp:Label ID="LblRegDateNotFullContent" runat="server" />
                                        </div>
                                    </small>
                                </p>
                            </div>
                            <hr />
                            <p>
                                <strong><asp:Label ID="LblProgramReviewsTitle" runat="server" /></strong>                                
                            </p>
                            <hr />
                            <div id="divReviewsNotFull" runat="server" visible="false" class="alert alert-warning" style="margin-top:5px; padding:0px; text-align:center; margin-bottom:1px;">
                                <strong><asp:Label ID="LblReviewsNotFull" runat="server" /></strong><asp:Label ID="LblReviewsNotFullContent" runat="server" />
                            </div>                            
                            <asp:Panel ID="PnlReviews" Visible="false" runat="server">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                    <ContentTemplate>
                                        <div id="divInfoMessage" runat="server" visible="false" class="alert alert-info">
                                            <strong><asp:Label ID="LblInfo" runat="server" /></strong><asp:Label ID="LblInfoMessage" runat="server" />
                                        </div>
                                        <div id="divWarningMessage" runat="server" visible="false" class="alert alert-danger">
                                              <strong><asp:Label ID="LblWarning" runat="server" /></strong><asp:Label ID="LblWarningMessage" runat="server" />
                                        </div>
                                        <div id="divSuccessMessage" runat="server" visible="false" class="alert alert-success">
                                            <strong><asp:Label ID="LblSuccess" runat="server" /></strong><asp:Label ID="LblSuccessMessage" runat="server" />
                                        </div>
                                        <telerik:RadGrid ID="RdgReviews" AllowPaging="true" PageSize="5" CssClass="grd-btm-0" OnItemDataBound="RdgReviews_OnItemDataBound" OnNeedDataSource="RdgReviews_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                            <MasterTableView>
                                                <NoRecordsTemplate>
                                                    <div class="emptyGridHolder">
                                                        <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                    </div>
                                                </NoRecordsTemplate>                                       
                                                <Columns>                                                    
                                                    <telerik:GridBoundColumn DataField="id" UniqueName="id" Display="false" />
                                                    <telerik:GridBoundColumn DataField="visitor_id" UniqueName="visitor_id" Display="false" />
                                                    <telerik:GridTemplateColumn>
                                                        <ItemTemplate>                                                    
                                                            <div style="display:inline-block; width:100%;">
                                                                <div style="display:inline-block; width:25%; float:left;">
                                                                    <div style="display:inline-block; width:100%; padding:5%;">
                                                                        <asp:ImageButton ID="ImgBtnCompanyLogo" CssClass="img-responsive" OnClick="ImgBtnCompanyLogo_OnClick" runat="server" /> 
                                                                        <telerik:RadToolTip ID="RdttpCompanyLogo" TargetControlID="ImgBtnCompanyLogo" Position="TopRight" Animation="Fade" runat="server" />
                                                                    </div>
                                                                    <div style="display:inline-block; width:100%; text-align:center; padding:5%;">
                                                                        <asp:Label ID="LblDateText" Font-Size="14px" Font-Bold="true" runat="server" /><br /> 
                                                                        <asp:Label ID="Lbldate" Font-Size="13px" runat="server" />
                                                                    </div>                                                        
                                                                </div>
                                                                <div style="display:inline-block; width:75%; float:left; padding:2%;">
                                                                    <h4>
                                                                        <asp:Label ID="LblCompanyReview" Font-Size="16px" runat="server" />
                                                                        <asp:ImageButton ID="ImgBtnSetNotPublic" OnClick="ImgBtnSetNotPublic_OnClick" ImageUrl="/images/delete.png" runat="server" />
                                                                    </h4>
                                                                    <div class="line" style="margin-bottom:10px;"></div>
                                                                    <div style="text-align:justify;">
                                                                        <asp:Label ID="LblReview" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <hr />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>                                       
							</asp:Panel>                            
                        </div>
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
