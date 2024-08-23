<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardFindRecommendedChannelPartners.aspx.cs" Inherits="WdS.ElioPlus.DashboardFindRecommendedChannelPartners" %>

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
                  
                </asp:Panel>

                <asp:Panel ID="PnlFindPartners" runat="server">
                    <div class="row">
                        <%--<div class="col-md-12">
                            <div class="note note-success">
                                <asp:Label ID="LblFindPartners" runat="server" />
                            </div>
                        </div>--%>            
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
               
                <div class="col-md-12">                   
                    <div class="note note-success" style="display:none;">
                        <h2>
                            <small>
                                <asp:Label ID="LblConnectionsPageInfo" Visible="false" Font-Size="14px" runat="server" />
                                <a id="aGoFullOrReg" runat="server" visible="false" class="alert-link"><asp:Label ID="LblGoFullOrReg" Font-Bold="true" Font-Size="15px" runat="server" /></a>
                            </small>
                        </h2>
                    </div>
                    <div id="divConnectionsTableHolder" runat="server">                        
                        <div class="portlet box">
                            <div class="portlet-title">
                               
                            </div>
                            <div class="portlet-body">
                                <div id="divConnections" runat="server">                                    
                                    <asp:ImageButton ID="ImgBtnExport" Visible="false" Width="50" Height="50" OnClick="ImgBtnExport_Click" ToolTip="Export my matches to .csv file" ImageUrl="~/images/icons/csv_export_1.png" runat="server" />                                    
                                    <div class="">  
                                        <telerik:RadGrid ID="RdgConnections" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" HeaderStyle-ForeColor="#e7505a" HeaderStyle-Font-Bold="true" CssClass="col-lg-12 col-md-12 col-sm-12 col-xs-12" MasterTableView-CssClass="col-lg-12 col-md-12 col-sm-12 col-xs-12" ItemStyle-CssClass="col-lg-4 col-md-12 nopad-rgt" AlternatingItemStyle-CssClass="col-lg-4 col-md-12 nopad-rgt" ShowHeader="false" OnNeedDataSource="RdgConnections_OnNeedDataSource" OnItemDataBound="RdgConnections_OnItemDataBound" AllowPaging="true" PageSize="20" PagerStyle-Position="Bottom" AutoGenerateColumns="false" runat="server">
                                            <MasterTableView>                   
                                                <Columns>                                        
                                                    <telerik:GridBoundColumn DataField="id" UniqueName="id" Display="false" />
                                                    <telerik:GridBoundColumn DataField="company_name" UniqueName="company_name" Display="false" />
                                                    <telerik:GridBoundColumn DataField="company_logo" UniqueName="company_logo" Display="false" />
                                                    <telerik:GridBoundColumn DataField="website" UniqueName="website" Display="false" />
                                                    <telerik:GridBoundColumn DataField="email" UniqueName="email" Display="false" />
                                                    <telerik:GridBoundColumn DataField="first_name" UniqueName="first_name" Display="false" />
                                                    <telerik:GridBoundColumn DataField="last_name" UniqueName="last_name" Display="false" />
                                                    <telerik:GridTemplateColumn>
                                                        <ItemTemplate>
                                                            <div class="profile-sidebar">
                                                                <!-- PORTLET MAIN -->
                                                                <div class="portlet light profile-sidebar-portlet">
                                                                    <!-- SIDEBAR USERPIC -->
                                                                    <div class="profile-userpic">
                                                                        <a id="aCompanyLogo" runat="server" role="button">
                                                                            <asp:Image ID="ImgCompanyLogo" Height="80" CssClass="img-responsive" runat="server" />
                                                                        </a>
                                                                    </div>
                                                                    <!-- END SIDEBAR USERPIC -->
                                                                    <!-- SIDEBAR USER TITLE -->
                                                                    <div class="profile-usertitle" style="margin-top:10px;">
                                                                        <div class="profile-usertitle-name">
                                                                            <a id="aCompanyName" runat="server" style="text-decoration-line:none;" role="button">
                                                                                <asp:Label ID="LblCompanyName" Text="Marcus Doe" runat="server" />
                                                                            </a>
                                                                        </div>                                                                        
                                                                    </div>
                                                                    <!-- END SIDEBAR USER TITLE -->
                                                                    <!-- SIDEBAR BUTTONS -->
                                                                    <div class="profile-userbuttons">
                                                                        <a id="aWebsite" runat="server" href="#" target="_blank" class="btn btn-circle green btn-sm" role="button">
                                                                            <asp:Label ID="LblWebsite" Text="Website" runat="server" />
                                                                        </a>
                                                                         <a id="aViewProfile" href="#" class="btn btn-circle green btn-sm" role="button" runat="server">                                                                            
                                                                            <asp:Label ID="LblViewProfile" Text="View Profile" runat="server" />
                                                                        </a>
                                                                        
                                                                    </div>
                                                                    <!-- END SIDEBAR BUTTONS -->
                                                                    <!-- SIDEBAR MENU -->
                                                                    <div class="profile-userbuttons">
                                                                        
                                                                               <a id="aBookDemo" runat="server" onserverclick="aBookDemo_ServerClick" class="btn btn-circle red btn-sm" role="button">
                                                                                    <asp:Label ID="LblBookDemo" Text="Book a demo" runat="server" />
                                                                                </a>
                                                                            
                                                                    </div>
                                                                    <!-- END MENU -->
                                                                </div>
                                                                <!-- END PORTLET MAIN -->                               
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>                                                    
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>  
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

    <!-- Book Demo Modal -->
    <div id="BookDemoModal" class="modal fade" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel6">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="BtnCancelDemo" OnClick="BtnCancelDemo_Click" Text="X" CssClass="close" aria-hidden="true" runat="server" />               
                            <h3>
                                <div style="text-align:center;">
                                    <asp:Label ID="LblBookTitle" Text="Book a Demo" runat="server" />
                                </div>
                            </h3>
                        </div>
                        <div class="modal-body">
                            <form class="form-horizontal col-sm-12">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="LblFirstName" runat="server" />
                                            <asp:TextBox ID="TbxFirstName" CssClass="form-control" Width="275" MaxLength="100" placeholder="First Name *" data-placement="top" data-trigger="manual" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="LblCompanyEmail" runat="server" />
                                            <asp:TextBox ID="TbxCompanyEmail" CssClass="form-control" Width="275" MaxLength="100" placeholder="Company Email *" data-placement="top" data-trigger="manual" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="LblBusinessSize" runat="server" />
                                            <asp:DropDownList Width="275" ID="DrpBusinessSize" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>                                
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="LblLastName" runat="server" />
                                            <asp:TextBox ID="TbxLastName" MaxLength="20" CssClass="form-control" Width="275" placeholder="Last Name *" data-placement="top" data-trigger="manual" runat="server" />
                                        </div>                                    
                                        <div class="form-group">
                                            <asp:Label ID="LblBusinessName" runat="server" />
                                            <asp:TextBox ID="TbxBusinessName" MaxLength="20" CssClass="form-control" Width="275" placeholder="Business Name *" data-placement="top" data-trigger="manual" runat="server" />
                                        </div>                                        
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer" style="display:inline-block; float:right;width:100% !important;">
                            <asp:Button ID="BtnRequestDemo" OnClick="BtnRequestDemo_Click" Width="100%" Text="Request a demo" CssClass="btn btn-success" runat="server" /> 
                        </div>
                        <div class="modal-footer" style="display:inline-block;width:100% !important;text-align:justify;">
                            <div id="divDemoWarningMsg" runat="server" visible="false" class="alert alert-danger">
                                <strong>
                                    <asp:Label ID="LblDemoWarningMsg" runat="server" />
                                </strong>
                                <asp:Label ID="LblDemoWarningMsgContent" runat="server" />
                            </div>
                            <div id="divDemoSuccessMsg" runat="server" visible="false" class="alert alert-success">
                                <strong>
                                    <asp:Label ID="LblDemoSuccessMsg" runat="server" />
                                </strong>
                                <asp:Label ID="LblDemoSuccessMsgContent" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Payment form (modal view) -->
    <div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <controls:UcStripe id="UcStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

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

    <style>
        .RadGrid_MetroTouch .rgAltRow
        {
            background-color:transparent !important;
            height:334px;
            margin-top:0px;
        }   
        .RadGrid_MetroTouch .rgRow
        {
            background-color:transparent !important;
            height:334px;
            margin-top:0px;
        }        
    </style>

    <script type="text/javascript">
        function CloseConfirmPopUp() {
            $('#divConfirm').modal('hide');
        }
        function OpenConfirmPopUp() {
            $('#divConfirm').modal('show');
        }

        function CloseBookDemoModal() {
            $('#BookDemoModal').modal('hide');
        }

        function OpenBookDemoModal() {
            $('#BookDemoModal').modal('show');
        }
    </script>
    
</asp:Content>
