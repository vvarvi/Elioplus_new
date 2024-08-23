<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardFindPartners.aspx.cs" Inherits="WdS.ElioPlus.DashboardFindPartners" %>

<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>
<%@ Register Src="/Controls/CriteriaSelection.ascx" TagName="UcCriteriaSelection" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- BEGIN PAGE BAR -->
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <span><asp:Label ID="LblDashboard" runat="server" /></span>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <span><asp:Label ID="LblDashPage" runat="server" /></span>
            </li>
        </ul>
        <div class="page-toolbar" id="divPgToolbar" runat="server">
            <div class="clearfix">
                <asp:Label ID="LblPricingPlan" runat="server" />
                <span style="margin-left:10px;">
                    <a id="aBtnGoPremium" runat="server" href="#PaymentModal" role="button" data-toggle="modal" class="btn btn-circle green-light btn-md" visible="false">
                        <asp:Label ID="LblBtnGoPremium" runat="server" />
                    </a>
                    <a id="aBtnGoFull" runat="server" class="btn btn-circle green-light btn-md" visible="false">
                        <asp:Label ID="LblGoFull" runat="server" />
                    </a>
                </span>
                <br /><asp:Label ID="LblRenewalHead" runat="server" Visible="false" /><asp:Label ID="LblRenewal" Visible="false" runat="server" />
            </div>
        </div>
    </div>    
    <!-- END PAGE BAR -->
    <!-- BEGIN PAGE TITLE-->
    <h3 class="page-title"><asp:Label ID="LblElioplusDashboard" runat="server" />
        <small><asp:Label ID="LblDashSubTitle" runat="server" /></small>
    </h3>
    <!-- END PAGE TITLE-->    
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="row">                
                <asp:Panel ID="PnlNotRegisteredOrNoVerticals" Visible="false" runat="server">            
                    <div class="well" style="text-align:center; margin-left:15%; margin-right:15%">
                        <h1>
                            <asp:Label ID="LblNotRegistHeader" runat="server" />
                            <span class=""><asp:Label ID="LblNotRegistSubHeader" runat="server" /></span>
                        </h1>                
                    </div>            
                    <br />            
                    <br />
                    <div class="alert alert-success alert-dismissable col-md-8" style="text-align:center; margin-left:17%; margin-right:10%">
                        <asp:Label ID="LblActionNotRegist" runat="server" />
                        <a id="aFullRegist" runat="server" class="alert-link"><asp:Label ID="LblActionLink" runat="server" /></a>
                    </div>
                    <div class="row" style="text-align:center;">
                        <div style="margin-top: 50px; display: inline-block; margin-bottom: 50px;">                
                            <div style="margin-top: 20px; font-size: 20px;">
                                <div style="float: left; width: 220px; margin-left: -20px;">
                                    <asp:Label ID="Label55" runat="server" Text="Your Company"></asp:Label>
                                </div>
                                <div style="float: left; width: 250px;">
                                    <asp:Label ID="Label56" runat="server" Text="Partners"></asp:Label>
                                </div>
                                <div style="float: left">
                                    <asp:Label ID="Label57" runat="server" Text="The results"></asp:Label>
                                </div>
                            </div>
                            <div style="margin-left: 50px; float: left;">
                                <asp:Image ID="Image10" runat="server" AlternateText="company partners" ImageUrl="/images/img1-nosub.png" />
                            </div>
                            <div style="float: left; margin-top: 30px; font-size: 18px; width: 160px;">
                                <div>
                                    <asp:Label ID="Label58" runat="server" Text="Get matched with the best partners" />
                                </div>
                                <div style="margin-top: 30px;">
                                    <asp:Label ID="Label59" runat="server" Text="Find the best terms of partnership" />
                                </div>
                                <div style="margin-top: 30px;">
                                    <asp:Label ID="Label60" runat="server" Text="Increase your revenues" />
                                </div>
                            </div>
                            <div style="float: left; width: 90px; margin-top: 20px;">
                                <asp:Image ID="Image11" runat="server" AlternateText="revenues increase" ImageUrl="/images/img2-nosub.png" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PnlFindPartners" Visible="false" runat="server">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="portlet green-sharp box">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-cogs"></i><asp:Label ID="LblVerticalsSelection" runat="server" />
                                    </div>                        
                                </div>
                                <div class="portlet-body">            
                                    <asp:CheckBoxList ID="CbxSubcategories" OnSelectedIndexChanged="CbxSubcategories_SelectedIndexChanged"
                                        DataValueField="id" AutoPostBack="true" DataTextField="description" RepeatDirection="Vertical"
                                        runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                        <div id="divStatAvailableOpportunities" runat="server" class="col-md-6">                
                            <div class="dashboard-stat purple col-md-6" style="height:130px; padding-right:25%; margin-left:25%;">
                                <div class="visual">
                                    <i class="fa fa-globe"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <span id="UserOpportunities" runat="server" data-counter="counterup">
                                            <asp:Label ID="LblUserOpportunities" runat="server" />
                                        </span>
                                    </div>
                                    <div class="desc">
                                        <asp:Label ID="LblAvaiOpportunities" runat="server" />
                                        <br />
                                        <asp:Label ID="LblAvailPartners" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOpportunitiesInfo" runat="server" class="col-md-6">
                            <div class="well well-lg col-md-12">
                                <div style="text-align:justify;">
                                    <asp:Label ID="LblOpportInfo" runat="server" />
                                </div>
                            </div>
                        </div>     
                        
                        <div id="divAlgInfo" runat="server" visible="false" class="col-md-12">
                            <div class="note note-info" style="text-align:center; font-size:15px; font-weight:bold; color:#6d6d6d;">                
                                <asp:Label ID="LblAlgInfo" runat="server" />
                            </div>
                        </div>
                        <div id="divNoMatchesForPremiumUserNotification" runat="server" visible="false" class="col-md-12">
                            <div class="note note-info" style="text-align:center; font-size:15px; font-weight:bold; color:#6d6d6d;">                
                                <asp:Label ID="LblNoMatchesForPremiumUserNotification" runat="server" />
                            </div>
                        </div>                
                    </div>
                </asp:Panel>                
                
                <div id="divOpportunitiesForPremiumVendors" visible="false" runat="server" class="col-md-12">
                    <div class="note note-info" style="text-align:center; font-size:15px; font-weight:bold; color:#6d6d6d;">
                        <asp:Label ID="LblOpportunitiesForPremiumVendors" Visible="true" runat="server" />
                    </div>
                </div>
                <div id="divAlgorithmHolder" class="col-md-12" runat="server">
                    <div class="col-md-12" style="margin-bottom:30px;">
                        <a id="aCancelRunCriteriaSelection" visible="false" onserverclick="aCancelRunCriteriaSelection_OnClick" runat="server" class="btn btn-outline btn-rounded btn-primary">
                            <asp:Label ID="LblCancelRunCriteriaSelection" Text="Back to My Matches" runat="server" />
                        </a>
                        <a id="aRunAlgBtn" runat="server" visible="false" onserverclick="RunAlgBtn_OnClick" class="btn btn-lg green" style="float:right;">
                            <asp:Label ID="LblAlgBtnText" runat="server" />
                            <i class="fa fa-cog"></i>
                        </a>
                    </div> 
                    <asp:Panel ID="PnlSelectionProcess" runat="server" Visible="false">
                        <asp:PlaceHolder ID="PhCriteriaSelection" runat="server" />                                    
                    </asp:Panel>
                    <asp:Panel ID="PnlCriteriaSelection" runat="server" Visible="false">
                        <div class="portlet-body">
                            <table class="table table-striped table-bordered table-advance table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <i class="fa fa-tasks" style="margin-right:10px;"></i><asp:Label ID="LblCriteriaName" runat="server" />
                                        </th>
                                        <th class="hidden-xs">
                                            <i class="fa fa-bars" style="margin-right:10px;"></i><asp:Label ID="LblCriteriaValue" runat="server" />
                                        </th>                                                        
                                    </tr>
                                </thead>
                                <tbody>                                                    
                                    <tr id="rowVerticals" runat="server" visible="false">
                                        <td style="min-width:300px;">
                                            <asp:Label ID="LblVerticals" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblVerticalsValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowFee" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblFee" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblFeeValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowRevenue" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblRevenue" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblRevenueValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowSupport" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblSupport" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblSupportValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowCompMat" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblCompMat" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblCompMatValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowProgMat" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblProgMat" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblProgMatValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowNumPartn" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblNumPartn" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblNumPartnValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowTiers" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblTiers" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblTiersValue" runat="server" />
                                        </td>                                                        
                                    </tr>                                                        
                                    <tr id="rowTrain" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblTrain" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblTrainValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowFreeTrain" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblFreeTrain" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblFreeTrainValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowMarkMat" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblMarkMat" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblMarkMatValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowCert" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblCert" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblCertValue" runat="server" />
                                        </td>                                                                                                                    
                                    </tr>
                                    <tr id="rowLocal" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblLocal" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblLocalValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowMdf" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblMdf" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblMdfValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowPortal" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblPortal" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblPortalValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowCountry" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblCountry" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <asp:Label ID="LblCountryValue" runat="server" />
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowPdf" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblPdf" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <a id="aPdf" runat="server"><asp:Label ID="LblPdfValue" runat="server" /></a>
                                        </td>                                                        
                                    </tr>
                                    <tr id="rowCsv" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="LblCsv" runat="server" />
                                        </td>                                                        
                                        <td>
                                            <a id="aCsv" runat="server"><asp:Label ID="LblCsvValue" runat="server" /></a>
                                        </td>                                                        
                                    </tr>                                                       
                                </tbody>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
                
                <div class="col-md-12">
                    <div class="col-md-12" style="margin-bottom:30px;">
                        <a id="aShowAlgBtn" runat="server" onserverclick="aShowAlgBtn_OnClick" class="btn btn-lg green" style="float:right;">
                            <asp:Label ID="LblShowAlgBtnText" runat="server" />
                            <i class="fa fa-cog"></i>
                        </a>
                    </div> 
                        
                    <div class="note note-success" style="display:none;">
                        <h2>
                            <small>
                                <asp:Label ID="LblConnectionsPageInfo" Visible="false" Font-Size="14px" runat="server" />
                                <a id="aGoFullOrReg" runat="server" visible="false" class="alert-link"><asp:Label ID="LblGoFullOrReg" Font-Bold="true" Font-Size="15px" runat="server" /></a>
                            </small>
                        </h2>
                    </div>
                    <div id="divConnectionsTableHolder" runat="server">
                        <div style="padding: 10px;" class="form-group">
                            <table>
                                <tr>
                                    <td style="width: 90px;">
                                        <asp:Label ID="LbldateFrom" Text="Given Date from: " runat="server" />
                                    </td>
                                    <td style="width: 190px;">
                                        <telerik:RadDatePicker ID="RdpConnectionsFrom" Skin="MetroTouch" runat="server" />
                                    </td>
                                    <td style="width: 90px;">
                                        <asp:Label ID="LblDateTo" Text="Given Date to: " runat="server" />
                                    </td>
                                    <td style="width: 190px;">
                                        <telerik:RadDatePicker ID="RdpConnectionsTo" Skin="MetroTouch" runat="server" />
                                    </td>
                                    <td style="width: 90px;">
                                        <asp:Label ID="LblCompanyName" Text="Company name: " runat="server" />
                                    </td>
                                        <td style="width: 190px;">
                                        <telerik:RadTextBox ID="RtbxCompanyName" Skin="MetroTouch" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnSearch" OnClick="BtnSearch_OnClick" runat="server" CssClass="btn btn-cta-primary" Text="Search" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="portlet box green">
                            <div class="portlet-title">
                                <div class="caption"><h3><i class="fa fa-sitemap" style="margin-right:10px;"></i><asp:Label ID="LblConnectionsTitle" Font-Size="18px" runat="server" /></h3></div>
                                <div class="tools">
                                    <a class="collapse"> </a>                        
                                    <a class="reload"> </a>                        
                                </div>
                            </div>
                            <div class="portlet-body">
                                <div id="divConnections" runat="server">                                    
                                    <asp:ImageButton ID="ImgBtnExport" Visible="false" Width="50" Height="50" OnClick="ImgBtnExport_Click" ToolTip="Export my matches to .csv file" ImageUrl="~/images/icons/csv_export_1.png" runat="server" />                                    
                                    <div class="">  
                                        <telerik:RadGrid ID="RdgConnections" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" HeaderStyle-ForeColor="#e7505a" HeaderStyle-Font-Bold="true" AllowPaging="true" PageSize="20" PagerStyle-Position="Bottom" OnItemDataBound="RdgConnections_OnItemDataBound" OnNeedDataSource="RdgConnections_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                            <MasterTableView>                   
                                                <Columns>                                        
                                                    <telerik:GridBoundColumn DataField="connection_id" UniqueName="connection_id" Display="false" />
                                                    <telerik:GridBoundColumn DataField="connection_company_id" UniqueName="connection_company_id" Display="false" />
                                                    <telerik:GridBoundColumn DataField="company_logo" UniqueName="company_logo" Display="false" />
                                                    <telerik:GridBoundColumn DataField="avatar" Display="false" />
                                                    <telerik:GridBoundColumn DataField="logo" Display="false" />
                                                    <telerik:GridTemplateColumn UniqueName="company_logo" Display="false">
                                                        <ItemTemplate>
                                                            <a id="aCompanyLogo" runat="server">
                                                                <asp:Image ID="ImgCompanyLogo" Width="50" Height="50" style="border-radius:35px !important;" runat="server" />
                                                            </a>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="user_application_type" UniqueName="user_application_type" Display="false" />
                                                    <telerik:GridBoundColumn DataField="company_name" UniqueName="company_name" Display="false" />
                                                    <telerik:GridTemplateColumn DataField="company_name" HeaderText="Company Name" UniqueName="company_name">
                                                        <ItemTemplate>
                                                            <div class="media-left">
                                                                <a id="aCompanyName" runat="server" style="text-decoration:underline !important; cursor:pointer;">
                                                                    <asp:Label ID="LblCompanyNameContent" Width="150" Font-Size="10" ForeColor="Blue" runat="server" />
                                                                </a>
                                                            </div>
                                                            <div id="divNotification" runat="server" visible="false" class="media-right">
                                                                <span id="spanNotificationMsg" class="badge bg-red" style="background-color:#ed7177;" title="New unread message" runat="server">
                                                                    <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                                </span>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="country" UniqueName="country" />
                                                    <telerik:GridBoundColumn DataField="company_email" UniqueName="company_email" Display="false" />
                                                    <telerik:GridTemplateColumn DataField="company_email" UniqueName="company_email" HeaderText="Company Email">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCompanyEmail" Font-Size="10" ForeColor="Black" Width="200" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="company_website" UniqueName="company_website" Display="false" />
                                                    <telerik:GridTemplateColumn UniqueName="company_website" Display="false">
                                                        <ItemTemplate>
                                                            <a id="aWebsite" visible="false" runat="server">
                                                                <asp:Label ID="LblWebsite" Font-Size="10" ForeColor="Black" Width="180" runat="server" />
                                                            </a>                                                            
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>                                                    
                                                    <telerik:GridTemplateColumn UniqueName="company_more_details">
                                                        <ItemTemplate>
                                                            <a id="aMoreDetails" style="text-decoration:underline !important;" runat="server">
                                                                <asp:Label ID="LblMoreDetails" Font-Size="10" ForeColor="Blue" Width="200" runat="server" />
                                                            </a>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="linkedin_url" UniqueName="linkedin_url" Display="false" />
                                                    <telerik:GridTemplateColumn UniqueName="linkedin_url" Display="false">
                                                        <ItemTemplate>
                                                            <a id="aLinkedin" visible="false" runat="server">
                                                                <asp:Image ID="ImgLinkedin" AlternateText="linkedin" ImageUrl="~/images/icons/linkedin_icon_big.png" Visible="false" runat="server" />
                                                                <asp:Label ID="LblNoLinkedin" Text="-" ForeColor="Black" Width="25" Visible="false" runat="server" />
                                                            </a>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="company_type" UniqueName="company_type" Display="false" /> 
                                                    <telerik:GridBoundColumn DataField="is_public" UniqueName="is_public" Display="false" /> 
                                                    <telerik:GridBoundColumn DataField="sysdate" UniqueName="sysdate" Display="false" /> 
                                                    <telerik:GridBoundColumn DataField="current_period_start" Display="false" />
                                                    <telerik:GridBoundColumn DataField="current_period_end" Display="false" />
                                                    <telerik:GridBoundColumn DataField="is_new" UniqueName="is_new" HeaderText="Status" Display="false" />
                                                    <telerik:GridTemplateColumn Display="false" UniqueName="is_new">
                                                        <ItemTemplate>
                                                            <a id="BtnNew" onserverclick="BtnNew_OnClick" class="btn btn-circle btn-sm yellow" runat="server">
                                                                <asp:Label ID="LblBtnNew" runat="server" />
                                                                <i class="fa fa-search"></i>
                                                            </a> 
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="" HeaderText="Add Action">
                                                        <ItemTemplate>
                                                            <a id="BtnAddOpportunity" visible="true" onserverclick="BtnAddOpportunity_OnClick" class="btn btn-circle btn-sm green" runat="server">
                                                                <asp:Label ID="LblAddOpportunity" Font-Size="10" runat="server" />
                                                                <i class="fa fa-add"></i>
                                                            </a>
                                                            <asp:Image ID="ImgOpportunitySuccess" style="cursor:pointer;" AlternateText="opportunity success" ImageUrl="~/images/icons/ok.png" runat="server" />
                                                            <asp:Label ID="LblOpportunitySuccess" Font-Size="8" runat="server" />
                                                            <telerik:RadToolTip ID="RttOpportunutySuccess" Visible="false" TargetControlID="ImgOpportunitySuccess" runat="server" />
                                                            
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="" HeaderText="Delete Action">
                                                        <ItemTemplate>
                                                            <a id="BtnDelete" onserverclick="BtnDelete_OnClick" class="btn btn-circle btn-sm white" runat="server">
                                                                <asp:Label ID="LblBtnDelete" Font-Size="10" runat="server" />
                                                                <asp:Image ID="ImgBtnDelete" AlternateText="delete" ImageUrl="/images/icons/small/delete.png" runat="server" />
                                                            </a>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn Display="false">
                                                        <ItemTemplate>
                                                            <div style="margin-bottom:0px !important; border:0px solid #fff !important;"> <%--class="table table-responsive table-striped table-bordered table-hover"--%>
                                                                <asp:Label ID="LblViewGivenDate" Visible="false" Font-Size="10" Width="70" ForeColor="Black" runat="server" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                          
                                        <div id="divTable" visible="false" runat="server">                    
                                            <table class="table table-striped table-bordered" style="width:100%; margin-bottom:0px !important; border:0px solid #fff !important;">
                                                <thead>
                                                    <tr style="text-align:center;">
                                                        <th style="width:80px; text-align:center;"><asp:Label ID="LblConLogo" runat="server" /></th>
                                                        <th style="width:110px; text-align:center;"><asp:Label ID="LblConName" runat="server" /></th>
                                                        <th style="width:150px; text-align:center;"><asp:Label ID="LblConEmail" runat="server" /></th>
                                                        <th style="width:50px; text-align:center;"><asp:Label ID="LblLinkedin" runat="server" /></th>
                                                        <%--<th style="width:0px; text-align:center;"><asp:Label ID="LblConWebsite" Visible="false" runat="server" /></th>--%>                                        
                                                        <th style="width:80px; text-align:center;"><asp:Label ID="LblConDateStarted" runat="server" /></th>
                                                        <th style="width:70px; text-align:center;"><asp:Label ID="LblStatus" runat="server" /></th>
                                                        <th style="width:180px; text-align:center;"><asp:Label ID="LblConAdd" runat="server" /></th>
                                                    </tr>
                                                </thead>
                                                <tbody>                                    
                                                    <telerik:RadGrid ID="RdgConnections1" Visible="false" AllowPaging="true" PageSize="20" PagerStyle-Position="Bottom" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-HorizontalAlign="Center" OnItemDataBound="RdgConnections_OnItemDataBound" OnNeedDataSource="RdgConnections_OnNeedDataSource" AutoGenerateColumns="false" runat="server" PagerStyle-NextPageImageUrl="~/images/prettyPhoto/light_rounded/btnNext.png" PagerStyle-PrevPageImageUrl="~/images/prettyPhoto/light_rounded/btnPrevious.png" PagerStyle-FirstPageImageUrl="~/assets/global/plugins/datatables/images/back_disabled.png" PagerStyle-LastPageImageUrl="~/assets/global/plugins/datatables/images/forward_disabled.png">
                                                        <MasterTableView>                   
                                                            <Columns>                                        
                                                                <telerik:GridBoundColumn DataField="id" Display="false" />
                                                                <telerik:GridBoundColumn DataField="company_id" Display="false" />
                                                                <telerik:GridBoundColumn DataField="company_logo" Display="false" />
                                                                <telerik:GridBoundColumn DataField="user_application_type" Display="false" />
                                                                <telerik:GridBoundColumn DataField="company_name" Display="false" />
                                                                <telerik:GridBoundColumn DataField="company_email" Display="false" />
                                                                <telerik:GridBoundColumn DataField="company_website" Display="false" />
                                                                <telerik:GridBoundColumn DataField="linkedin_url" Display="false" />
                                                                <telerik:GridBoundColumn DataField="sysdate" Display="false" /> 
                                                                <telerik:GridBoundColumn DataField="current_period_start" Display="false" />
                                                                <telerik:GridBoundColumn DataField="current_period_end" Display="false" />
                                                                <telerik:GridBoundColumn DataField="is_new" HeaderText="Status" Display="false" />
                                                                <telerik:GridTemplateColumn>
                                                                    <ItemTemplate>
                                                                        <table class="table table-striped table-bordered" style="margin-bottom:0px !important; border:0px solid #fff !important;"> <%--class="table table-responsive table-striped table-bordered table-hover"--%>
                                                                            <tr>
                                                                                <td style="width:120px;">
                                                                                    <a id="aCompanyLogo" runat="server">
                                                                                        <asp:Image ID="ImgCompanyLogo" Width="90" Height="40" runat="server" />
                                                                                    </a>
                                                                                </td>
                                                                                <td style="width:120px; overflow-wrap:break-word;">
                                                                                    <a id="aCompanyName" runat="server">
                                                                                        <asp:Label ID="LblCompanyNameContent" Font-Size="10" Width="140" ForeColor="Black" runat="server" />
                                                                                    </a>
                                                                                </td>                                                        
                                                                                <td style="width:150px; overflow-wrap:break-word;">
                                                                                    <asp:Label ID="LblCompanyEmail" Font-Size="10" ForeColor="Black" Width="200" runat="server" />
                                                                                </td>                                                                                
                                                                                <td style="width:100px;">
                                                                                    <a id="aLinkedin" runat="server">
                                                                                        <asp:Image ID="ImgLinkedin" AlternateText="linkedin" ImageUrl="~/images/icons/linkedin_icon_big.png" Visible="false" runat="server" />
                                                                                        <asp:Label ID="LblNoLinkedin" Text="-" ForeColor="Black" Width="25" Visible="false" runat="server" />
                                                                                    </a>
                                                                                </td>
                                                                                <td style="width:150px; overflow-wrap:break-word; display:none;">
                                                                                    <a id="aWebsite" runat="server">
                                                                                        <asp:Label ID="LblWebsite" Font-Size="10" ForeColor="Black" Width="180" runat="server" />
                                                                                    </a>
                                                                                </td>                                                                                                                        
                                                                                <td style="width:100px;">
                                                                                    <asp:Label ID="LblViewGivenDate" Font-Size="10" Width="70" ForeColor="Black" runat="server" />
                                                                                </td>
                                                                                <td style="width:40px;">
                                                                                    <a id="BtnNew" onserverclick="BtnNew_OnClick" class="btn btn-circle btn-sm yellow" runat="server">
                                                                                        <asp:Label ID="LblBtnNew" runat="server" />
                                                                                        <i class="fa fa-search"></i>
                                                                                    </a>                                                            
                                                                                </td>
                                                                                <td style="width:140px; text-align:center;">
                                                                                    <a id="BtnAddOpportunity" visible="true" onserverclick="BtnAddOpportunity_OnClick" class="btn btn-circle btn-sm green" runat="server">
                                                                                        <asp:Label ID="LblAddOpportunity" Font-Size="10" runat="server" />
                                                                                        <i class="fa fa-add"></i>
                                                                                    </a>
                                                                                    <asp:Image ID="ImgOpportunitySuccess" style="cursor:pointer;" AlternateText="opportunity success" ImageUrl="~/images/icons/ok.png" runat="server" />
                                                                                    <asp:Label ID="LblOpportunitySuccess" Font-Size="8" Text="Added successfully" runat="server" />
                                                                                    <telerik:RadToolTip ID="RttOpportunutySuccess" Visible="false" TargetControlID="ImgOpportunitySuccess" runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <a id="BtnDelete" onserverclick="BtnDelete_OnClick" class="btn btn-circle btn-sm red" runat="server">
                                                                                        <asp:Label ID="LblBtnDelete" Font-Size="10" runat="server" />
                                                                                        <i class="fa fa-times" title="Delete"></i>
                                                                                    </a>                                                            
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <controls:MessageControl ID="UcConnectionsMessageAlert" runat="server" />
                            <div id="divPartnersInfo" visible="false" runat="server">
                                <asp:Image ID="Image1" AlternateText="info" ImageUrl="~/images/icons/small/info.png" runat="server" />
                                <asp:Label ID="Label1" Text="Connections with this icon " runat="server" />
                                <asp:Image ID="ImgInfo" AlternateText="third party" ImageUrl="~/images/icons/third_party.png" runat="server" />
                                <asp:Label ID="LblThirdPartyInfo" Text=" are from 3rd party partners that match your criteria" runat="server" />
                            </div>
                        </div>            
                    </div>
                    <div style="margin-bottom:100px;"></div>
                </div>
            </div>
            <div style="margin-bottom:100px;"></div>            
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divConfirm" class="modal fade" tabindex="-1" style="position:fixed; top:25%">        
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title"><asp:Label ID="LblConfTitle" CssClass="control-label" runat="server" /></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="LblConfMsg" CssClass="control-label" runat="server" />
                            </div>                                    
                        </div>                            
                    </div>                    
                </div>
                <div class="modal-footer">                            
                    <button type="button" data-dismiss="modal" class="btn dark btn-outline">Back</button>
                    <asp:Button ID="BtnConfDelete" OnClick="BtnConfDelete_OnClick" CssClass="btn red-sunglo" runat="server" Text="Delete" />
                </div>
            </div>
        </div>            
    </div>

    <!-- Payment form (modal view) -->
    <div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <controls:UcStripe id="UcStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">

    <style>
        .RadGrid_MetroTouch .rgHeader:first-child, .RadGrid_MetroTouch th.rgResizeCol:first-child, .RadGrid_MetroTouch .rgFilterRow > td:first-child, .RadGrid_MetroTouch .rgRow > td:first-child, .RadGrid_MetroTouch .rgAltRow > td:first-child
        {
            width:10px;
        }
        .RadGrid_MetroTouch .rgHeader, .RadGrid_MetroTouch th.rgResizeCol, .RadGrid_MetroTouch .rgHeaderWrapper
        {
            width:120px;
        }
        .RadGrid_MetroTouch .rgAltRow
        {
            background-color:transparent !important;
        }
        .RadGrid_MetroTouch .rgRow
        {
            background-color:transparent !important;
        }
    </style>
    <script type="text/javascript">
        function CloseConfirmPopUp() {
            $('#divConfirm').modal('hide');
        }
    </script>
    <script type="text/javascript">
        function OpenConfirmPopUp() {
            $('#divConfirm').modal('show');
        }
    </script>
    <link rel="stylesheet" type="text/css" href="DashboardTemplate/plugins/datatables.net-select-bs/css/select.bootstrap.min.css">
</telerik:RadScriptBlock>
</asp:Content>
