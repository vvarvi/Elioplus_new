<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardConnections.aspx.cs" Inherits="WdS.ElioPlus.DashboardConnections" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager ID="masterRadAjaxManager" runat="server" RestoreOriginalRenderDelegate="false">
    </telerik:RadAjaxManager>
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel7">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">            
                    <div class="note note-success">
                        <h2><small>
                                <asp:Label ID="LblConnectionsPageInfo" Font-Size="14px" runat="server" />
                                <a id="aGoFullOrReg" runat="server" visible="false" class="alert-link"><asp:Label ID="LblGoFullOrReg" Font-Bold="true" Font-Size="15px" runat="server" /></a>
                            </small>
                        </h2>
                    </div>
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
                                <div class="table-bordered">                        
                                    <table cellspacing="0" width="100%" class="table table-striped table-bordered" style="margin-bottom:0px !important; border:0px solid #fff !important;">
                                        <thead>
                                            <tr style="text-align:center;">
                                                <th style="width:100px; text-align:center;"><asp:Label ID="LblConLogo" runat="server" /></th>
                                                <th style="width:150px; text-align:center;"><asp:Label ID="LblConName" runat="server" /></th>
                                                <th style="width:200px; text-align:center;"><asp:Label ID="LblConEmail" runat="server" /></th>
                                                <th style="width:50px; text-align:center;"><asp:Label ID="LblLinkedin" runat="server" /></th>
                                                <th style="width:180px; text-align:center;"><asp:Label ID="LblConWebsite" runat="server" /></th>                                        
                                                <th style="width:100px; text-align:center;"><asp:Label ID="LblConDateStarted" runat="server" /></th>
                                                <th style="width:180px; text-align:center;"><asp:Label ID="LblConAdd" runat="server" /></th>
                                            </tr>
                                        </thead>
                                        <tbody>                                    
                                            <telerik:RadGrid ID="RdgConnections" AllowPaging="true" PageSize="20" PagerStyle-Position="Bottom" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-HorizontalAlign="Center" OnItemDataBound="RdgConnections_OnItemDataBound" OnNeedDataSource="RdgConnections_OnNeedDataSource" AutoGenerateColumns="false" runat="server" PagerStyle-NextPageImageUrl="~/images/prettyPhoto/light_rounded/btnNext.png" PagerStyle-PrevPageImageUrl="~/images/prettyPhoto/light_rounded/btnPrevious.png" PagerStyle-FirstPageImageUrl="~/assets/global/plugins/datatables/images/back_disabled.png" PagerStyle-LastPageImageUrl="~/assets/global/plugins/datatables/images/forward_disabled.png">
                                                <MasterTableView>                   
                                                    <Columns>                                        
                                                        <telerik:GridBoundColumn DataField="id" Display="false" />
                                                        <telerik:GridBoundColumn DataField="company_id" Display="false" />
                                                        <telerik:GridBoundColumn DataField="company_logo" Display="false" />
                                                        <telerik:GridBoundColumn DataField="user_application_type" Display="false" />
                                                        <telerik:GridBoundColumn DataField="company_name" Display="false" />
                                                        <telerik:GridBoundColumn DataField="company_email" Display="false" />
                                                        <telerik:GridBoundColumn DataField="company_website" Display="false" />
                                                        <telerik:GridBoundColumn DataField="company_type" Display="false" />
                                                        <telerik:GridBoundColumn DataField="linkedin_url" Display="false" />
                                                        <telerik:GridBoundColumn DataField="sysdate" Display="false" /> 
                                                        <telerik:GridBoundColumn DataField="current_period_start" Display="false" />
                                                        <telerik:GridBoundColumn DataField="current_period_end" Display="false" />
                                                        <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <table cellspacing="0" width="100%" class="table table-striped table-bordered" style="margin-bottom:0px !important; border:0px solid #fff !important;"> <%--class="table table-responsive table-striped table-bordered table-hover"--%>
                                                                    <tr>
                                                                        <td style="width:90px;">
                                                                            <a id="aCompanyLogo" runat="server">
                                                                                <asp:Image ID="ImgCompanyLogo" Width="90" Height="40" runat="server" />
                                                                            </a>
                                                                        </td>
                                                                        <td style="width:140px; overflow-wrap:break-word;">
                                                                            <a id="aCompanyName" runat="server">
                                                                                <asp:Label ID="LblCompanyNameContent" Font-Size="10" Width="140" ForeColor="Black" runat="server" />
                                                                            </a>
                                                                        </td>                                                        
                                                                        <td style="width:200px; overflow-wrap:break-word;">
                                                                            <asp:Label ID="LblCompanyEmail" Font-Size="10" ForeColor="Black" Width="200" runat="server" />
                                                                        </td>
                                                                        <td style="width:25px;">
                                                                            <a id="aLinkedin" runat="server">
                                                                                <asp:Image ID="ImgLinkedin" ImageUrl="~/images/icons/linkedin_icon_big.png" Visible="false" runat="server" />
                                                                                <asp:Label ID="LblNoLinkedin" Text="-" ForeColor="Black" Width="25" Visible="false" runat="server" />
                                                                            </a>
                                                                        </td>
                                                                        <td style="width:180px; overflow-wrap:break-word;">
                                                                            <a id="aWebsite" runat="server">
                                                                                <asp:Label ID="LblWebsite" Font-Size="10" ForeColor="Black" Width="180" runat="server" />
                                                                            </a>
                                                                        </td>                                                                                                                        
                                                                        <td style="width:70px;">
                                                                            <asp:Label ID="LblViewGivenDate" Font-Size="10" Width="70" ForeColor="Black" runat="server" />
                                                                        </td>
                                                                        <td style="width:180px; text-align:center;">
                                                                            <a id="BtnAddOpportunity" visible="true" onserverclick="BtnAddOpportunity_OnClick" class="btn btn-circle btn-sm green" runat="server">
                                                                                <asp:Label ID="LblAddOpportunity" Font-Size="10" runat="server" />
                                                                                <i class="fa fa-add"></i>
                                                                            </a>
                                                                            <asp:Image ID="ImgOpportunitySuccess" style="cursor:pointer;" ImageUrl="~/images/icons/ok.png" runat="server" />
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
                        <controls:MessageControl ID="UcConnectionsMessageAlert" runat="server" />
                        <div id="divPartnersInfo" visible="false" runat="server">
                            <asp:Image ID="Image1" ImageUrl="~/images/icons/small/info.png" runat="server" />
                            <asp:Label ID="Label1" Text="Connections with this icon " runat="server" />
                            <asp:Image ID="ImgInfo" ImageUrl="~/images/icons/third_party.png" runat="server" />
                            <asp:Label ID="LblThirdPartyInfo" Text=" are from 3rd party partners that match your criteria" runat="server" />
                        </div>
                    </div>            
                </div>
                <div style="margin-bottom:100px;"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
</asp:Content>
