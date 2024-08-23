<%@ Page Title="" Language="C#" MasterPageFile="~/ElioplusMaster.Master" AutoEventWireup="true" CodeBehind="ProfilesPartnerTypes.aspx.cs" Inherits="WdS.ElioPlus.ProfilesPartnerTypes" %>

<asp:Content ID="SearchHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="SearchMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <div class="bg-slider-wrapper">
                <div class="flexslider bg-slider">
                    <ul class="slides">
                        <li class="slide slide-3"></li>
                    </ul>
                </div>
            </div>
            <!--//bg-slider-wrapper bglight-->
            <div class="sections-wrapper results-cont" style="line-height: 1.5 !important; font-family: 'Open Sans', sans-serif; font-size: 14px;">
                <div class="container-fluid elio-results">
                    <div class="row">
                        <div style="">

                            <asp:Panel ID="PnlResults" Visible="false" runat="server">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="display: inline-block; text-align: center; font-family: 'Open Sans', sans-serif; font-size: 14px;">
                                    <div id="divSuccessMsg" runat="server" visible="false" class="alert alert-success">
                                        <strong>
                                            <asp:Label ID="LblSuccessMsg" runat="server" /></strong><asp:Label ID="LblSuccessMsgContent" runat="server" />

                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12" style="text-align: justify;">
                                        <h1>
                                            <asp:Label ID="LblResultsTitle" runat="server" />
                                        </h1>
                                        <div style="margin-top: 20px;"></div>
                                        <p>
                                            <asp:Label ID="LblResultsContent1" runat="server" />
                                        </p>
                                        <p>
                                            <asp:Label ID="LblResultsContent2" runat="server" />
                                        </p>
                                        <p>
                                            <asp:Label ID="LblResultsContent3" runat="server" />
                                        </p>
                                    </div>
                                    <div style="margin-bottom: 20px;"></div>
                                    <div style="margin-bottom: 50px">
                                        <asp:Label ID="LblGetStartedList" Text="Join this List" runat="server" /><br />
                                        <br />
                                        <div style="margin-bottom: 5px;"></div>
                                        <a id="aGetStarted" class="btn btn-lg btn-cta-primary" style="font-size: 15px" runat="server">Sign up for free!</a>
                                    </div>
                                </div>
                                <div class="col-lg-12" style="width: 100%; text-align: center;">
                                    <div style="margin-top: 30px;" id="divWarningMsg" runat="server" visible="false" class="alert alert-warning">
                                        <strong>
                                            <asp:Label ID="LblWarningMsg" runat="server" /></strong><asp:Label ID="LblWarningMsgContent" runat="server" />
                                    </div>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 30px;">
                                    <div id="content" class="site-content content-wrapper page-content">
                                        <div class="page type-page hentry">
                                            <div class="page-content-body">
                                                <section class="find-work">
                                                    <div class="container" style="width: 100% !important;">
                                                        <div class="section-content">
                                                            <telerik:RadGrid ID="RdgResults" Style="padding-bottom: 0px !important;" CssClass="col-lg-12 col-md-12 col-sm-12 col-xs-12" AllowPaging="false" OnPageIndexChanged="RdgResults_PageIndexChanged" OnItemCreated="RdgResults_ItemCreated" OnItemDataBound="RdgResults_OnItemDataBound" OnNeedDataSource="RdgResults_OnNeedDataSource" AutoGenerateColumns="false" runat="server" PagerStyle-Position="Bottom">
                                                                <MasterTableView>
                                                                    <NoRecordsTemplate>
                                                                        <div class="emptyGridHolder">
                                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                        </div>
                                                                    </NoRecordsTemplate>
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                                        <telerik:GridTemplateColumn>
                                                                            <ItemTemplate>
                                                                                <div class="job-item">

                                                                                    <div class="company-info" style="margin-top: 0px;">
                                                                                        <a id="aCompanyLogo" runat="server">
                                                                                            <asp:Image ID="ImgCompanyLogo" CssClass="img-responsive" runat="server" />
                                                                                        </a>
                                                                                    </div>

                                                                                    <div class="job-info">
                                                                                        <h3>
                                                                                            <a id="aCompanyName" runat="server">
                                                                                                <asp:Label ID="LblCompany" runat="server" />
                                                                                            </a>
                                                                                        </h3>
                                                                                        <p>
                                                                                            <asp:Label ID="LblOverview" runat="server" />
                                                                                        </p>
                                                                                        <ul>
                                                                                            <li class="location">
                                                                                                <i class="fa fa-map-marker" aria-hidden="true"></i>
                                                                                                <asp:Label ID="LblCompanyAddress" runat="server" />
                                                                                            </li>
                                                                                            <li class="rate">
                                                                                                <i class="fa fa-usd" aria-hidden="true"></i>
                                                                                                <asp:Label ID="LblViews" runat="server" />
                                                                                                <asp:Label ID="LblViewsValue" runat="server" />
                                                                                            </li>
                                                                                            <li class="contract">
                                                                                                <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                                                                                <asp:Label ID="LblIndustry" Visible="false" runat="server" />
                                                                                                <asp:Label ID="LblIndustryValue" runat="server" />
                                                                                                <i id="iMoreIndustries" runat="server" visible="false" class="fa fa-chevron-circle-right" style="float: right; font-size: 18px;"></i>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>
                                                                                    <div class="applicants-info">
                                                                                        <p>
                                                                                            <asp:Panel ID="PnlRating" runat="server">
                                                                                                <asp:Image ID="r1" runat="server" />
                                                                                                <asp:Image ID="r2" runat="server" />
                                                                                                <asp:Image ID="r3" runat="server" />
                                                                                                <asp:Image ID="r4" runat="server" />
                                                                                                <asp:Image ID="r5" runat="server" />
                                                                                                <asp:Label ID="LblTotalRatingValue" runat="server" />
                                                                                            </asp:Panel>
                                                                                        </p>
                                                                                    </div>
                                                                                    <div class="apply-info">
                                                                                        <a id="aViewProfile" runat="server" class="btn btn-elio">
                                                                                            <asp:Label ID="LblViewProfile" runat="server" />
                                                                                        </a>
                                                                                    </div>
                                                                                    <div class="elio-res">
                                                                                        <div id="divFeatured" runat="server" visible="false" class="ribbon">
                                                                                            <div class="text">
                                                                                                <asp:Label ID="LblFeature" runat="server" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="footer-col links col-md-5 col-sm-4 col-xs-12"></div>
                                                                <div class="footer-col links col-md-2 col-sm-4 col-xs-12">
                                                                    <div style="">
                                                                        <div style="width: 100px;">
                                                                            <a id="aPreviousPage" visible="false" runat="server">
                                                                                <div style="margin-left: 0px;">
                                                                                    <asp:Image ID="ImgPrevious" ImageUrl="~/images/previous.png" runat="server" />
                                                                                </div>
                                                                                <div style="width: 70px; margin-left: 30px; margin-top: -20px;">
                                                                                    <asp:Label ID="LblPreviousPage" Text="Previous" runat="server" />
                                                                                </div>
                                                                            </a>
                                                                        </div>
                                                                        <div style="margin-left: 120px;">
                                                                            <a id="aNextPage" runat="server">
                                                                                <div style="width: 70px;">
                                                                                    <asp:Label ID="LblNextPage" Text="Next" runat="server" />
                                                                                </div>
                                                                                <div style="margin-left: 45px; margin-top: -20px;">
                                                                                    <asp:Image ID="ImgNext" ImageUrl="~/images/next.png" runat="server" />
                                                                                </div>
                                                                            </a>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="footer-col links col-md-5 col-sm-4 col-xs-12"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </section>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="display: inline-block; padding-bottom: 0px; margin-bottom: 70px;">
                                    <div class="row">
                                        <h4 class="title" style="padding: 15px;">
                                            <asp:Label ID="LblFooterRelatedCompanies" Text="Related Categories" runat="server" /></h4>
                                        <div style="margin-bottom: 10px;"></div>
                                        <div class="footer-col links col-md-3 col-sm-4 col-xs-12">
                                            <div class="footer-col-inner">
                                                <ul class="list-unstyled">
                                                    <li><a id="aFooterCompany1" runat="server">
                                                        <asp:Label ID="LblFooterCompany1" Style="color: #02a8f3;" Text="company 1 link" runat="server" /></a></li>
                                                    <li><a id="aFooterCompany2" runat="server">
                                                        <asp:Label ID="LblFooterCompany2" Style="color: #02a8f3;" Text="company 2 link" runat="server" /></a></li>
                                                </ul>
                                            </div>
                                            <!--//footer-col-inner-->
                                        </div>
                                        <!--//foooter-col-->
                                        <div class="footer-col links col-md-3 col-sm-4 col-xs-12">
                                            <div class="footer-col-inner">
                                                <ul class="list-unstyled">
                                                    <li><a id="aFooterCompany3" runat="server">
                                                        <asp:Label ID="LblFooterCompany3" Style="color: #02a8f3;" Text="company 3 link" runat="server" /></a></li>
                                                    <li><a id="aFooterCompany4" runat="server">
                                                        <asp:Label ID="LblFooterCompany4" Style="color: #02a8f3;" Text="company 4 link" runat="server" /></a></li>
                                                </ul>
                                            </div>
                                            <!--//footer-col-inner-->
                                        </div>
                                        <!--//foooter-col-->
                                        <div class="footer-col links col-md-3 col-sm-4 col-xs-12">
                                            <div class="footer-col-inner">
                                                <ul class="list-unstyled">
                                                    <li><a id="aFooterCompany5" runat="server">
                                                        <asp:Label ID="LblFooterCompany5" Style="color: #02a8f3;" Text="company 5 link" runat="server" /></a></li>
                                                    <li><a id="aFooterCompany6" runat="server">
                                                        <asp:Label ID="LblFooterCompany6" Style="color: #02a8f3;" Text="company 6 link" runat="server" /></a></li>
                                                </ul>
                                            </div>
                                            <!--//footer-col-inner-->
                                        </div>
                                        <!--//foooter-col-->
                                        <div class="footer-col links col-md-3 col-sm-4 col-xs-12">
                                            <div class="footer-col-inner">
                                                <ul class="list-unstyled">
                                                    <li><a id="aFooterCompany7" runat="server">
                                                        <asp:Label ID="LblFooterCompany7" Style="color: #02a8f3;" Text="company 7 link" runat="server" /></a></li>
                                                    <li><a id="aFooterCompany8" runat="server">
                                                        <asp:Label ID="LblFooterCompany8" Style="color: #02a8f3;" Text="company 8 link" runat="server" /></a></li>
                                                </ul>
                                            </div>
                                            <!--//footer-col-inner-->
                                        </div>
                                    </div>
                                    <div id="divGoPremium" runat="server" visible="false" style="float: right; margin-top: 5px; margin-left: 4%; margin-bottom: 2%">
                                        <a id="aGoPremium" visible="false" runat="server" role="button" class="btn btn-lg btn-cta-secondary">
                                            <asp:Label ID="LblGoPremium" Font-Size="12" runat="server" /></a>
                                        <asp:Button ID="BtnGoPremium" Visible="false" CssClass="btn btn-lg btn-cta-secondary" runat="server" />
                                    </div>
                                    <div id="divInfoMsg" runat="server" visible="false" class="alert alert-info" style="float: right;">
                                        <strong>
                                            <asp:Label ID="LblInfoMsg" Font-Size="11" runat="server" /></strong><asp:Label ID="LblInfoMsgContent" Font-Size="11" runat="server" />
                                    </div>
                                    <div id="divDangerMsg" runat="server" visible="false" class="alert alert-danger">
                                        <strong>
                                            <asp:Label ID="LblDangerMsg" runat="server" /></strong><asp:Label ID="LblDangerMsgContent" runat="server" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <!-- / col -->
                    </div>
                    <!-- / row -->
                </div>
            </div>
            <!--//section-wrapper-->

        </ContentTemplate>
    </asp:UpdatePanel>

    <style>
        .ul li a:hover {
            color: #808080;
        }
    </style>

</asp:Content>
