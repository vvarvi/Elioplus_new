<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardLeads.aspx.cs" Inherits="WdS.ElioPlus.DashboardLeads" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="DashLeadsHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashLeadsMain" ContentPlaceHolderID="MainContent" runat="server">
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
                        <h2>
                            <small>
                                <asp:Label ID="LblPageInfo" Font-Size="14px" runat="server" />                        
                            </small>
                            <a id="aActionLink1" runat="server">
                                <strong><asp:Label ID="LblActionInfo" Font-Size="15px" runat="server" /></strong>
                            </a>
                        </h2>
                    </div>
                    <div style="padding: 10px;" class="form-group">
                        <table>
                            <tr>
                                <td style="width: 160px;">
                                    <asp:DropDownList ID="DdlCompanyType" OnTextChanged="DdlCompanyType_OnTextChanged" AutoPostBack="false" class="btn-group btn yellow" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 90px;">
                                    <asp:Label ID="LbldateFrom" Text="Date from: " runat="server" />
                                </td>
                                <td style="width: 190px;">
                                    <telerik:RadDatePicker ID="RdpConnectionsFrom" Skin="MetroTouch" runat="server" />
                                </td>
                                <td style="width: 90px;">
                                    <asp:Label ID="LblDateTo" Text="Date to: " runat="server" />
                                </td>
                                <td style="width: 190px;">
                                    <telerik:RadDatePicker ID="RdpConnectionsTo" Skin="MetroTouch" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnSearch" OnClick="BtnSearch_OnClick" role="button" runat="server" CssClass="btn btn-cta-primary" Text="Search" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="page-title">
                
                    </div>            
                    <div class="portlet box yellow">
                        <div class="portlet-title">
                            <div class="caption"><h3><i class="fa fa-user-plus" style="margin-right:10px;"></i><asp:Label ID="LblTableTitle" Font-Size="18px" runat="server" /></h3></div>
                            <div class="tools">
                                <a class="collapse"></a>                        
                                <a class="reload"> </a>                        
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="table-bordered">
                                <table  cellspacing="0" width="100%" class="table table-striped table-bordered" style="margin-bottom:0px !important; border:0px solid #fff !important;">
                                    <thead>
                                        <tr style="text-align:center;">
                                            <th style="width:150px; text-align:center;"><asp:Label ID="LblComLogo" Text="Logo" runat="server" /></th>
                                            <th style="width:320px; text-align:center;"><asp:Label ID="LblComName" Text="Company" runat="server" /></th>
                                            <th style="width:150px; text-align:center;"><asp:Label ID="LblComType" Text="Type" runat="server" /></th>
                                            <th style="width:150px; text-align:center;"><asp:Label ID="LblDate" Text="Date" runat="server" /></th>
                                            <th style="width:130px; text-align:center;"><asp:Label ID="LblStatus" Text="Status" runat="server" /></th>                                        
                                            <th style="width:120px; text-align:center;"><asp:Label ID="LblActions" Text="Actions" runat="server" /></th>                                    
                                        </tr>
                                    </thead>
                                    <tbody>                            
                                        <telerik:RadGrid ID="RgdPreviewLeads" AllowPaging="true" PageSize="10" PagerStyle-Position="Bottom" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-HorizontalAlign="Center" PagerStyle-NextPageImageUrl="~/images/prettyPhoto/light_rounded/btnNext.png" PagerStyle-PrevPageImageUrl="~/images/prettyPhoto/light_rounded/btnPrevious.png" PagerStyle-FirstPageImageUrl="~/assets/global/plugins/datatables/images/back_disabled.png" PagerStyle-LastPageImageUrl="~/assets/global/plugins/datatables/images/forward_disabled.png" OnItemDataBound="RgdPreviewLeads_OnItemDataBound" OnNeedDataSource="RgdPreviewLeads_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                            <MasterTableView>                                    
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="id" Display="false" />
                                                    <telerik:GridBoundColumn DataField="interested_company_id" Display="false" />
                                                    <telerik:GridBoundColumn DataField="company_logo" HeaderText="Logo" Display="false" />
                                                    <telerik:GridBoundColumn DataField="company_name" HeaderText="Name" Display="false" />
                                                    <telerik:GridBoundColumn DataField="company_type" HeaderText="Type" Display="false" />
                                                    <telerik:GridBoundColumn DataField="last_update" HeaderText="View date" Display="false" />
                                                    <telerik:GridBoundColumn DataField="is_new" HeaderText="Status" Display="false" />
                                                    <telerik:GridBoundColumn DataField="delete" HeaderText="Delete" Display="false" />
                                                    <telerik:GridTemplateColumn DataField="actions" UniqueName="actions">
                                                        <ItemTemplate>
                                                            <table cellspacing="0" width="100%" class="table table-striped table-bordered" style="margin-bottom:0px !important; border:0px solid #fff !important;">
                                                                <tr>
                                                                    <td style="width:95px;">
                                                                        <a id="aCompanyLogo" runat="server">
                                                                            <asp:Image ID="ImgCompanyLogo" Width="90" Height="40" runat="server" />
                                                                        </a>
                                                                    </td>
                                                                    <td style="width:210px; overflow-wrap:break-word;">
                                                                        <a id="aCompanyName" runat="server">
                                                                            <asp:Label ID="LblCompanyNameContent" Width="200" runat="server" />
                                                                        </a>
                                                                    </td>
                                                                    <td style="width:100px;">
                                                                        <asp:Label ID="LblCompanyTypeContent" Width="90" runat="server" />
                                                                    </td>
                                                                    <td style="width:90px;">
                                                                        <asp:Label ID="LblViewDateContent" Width="80" runat="server" />
                                                                    </td>
                                                                    <td style="width:60px;">
                                                                        <a id="BtnNew" onserverclick="BtnNew_OnClick" class="btn btn-circle btn-sm yellow" runat="server">
                                                                            <asp:Label ID="LblBtnNew" runat="server" />
                                                                            <i class="fa fa-search"></i>
                                                                        </a>                                                            
                                                                    </td>
                                                                    <td style="width:80px;">
                                                                        <a id="BtnDelete" onserverclick="BtnDelete_OnClick" class="btn btn-circle btn-sm purple" runat="server">
                                                                            <asp:Label ID="LblBtnDelete" runat="server" />
                                                                            <i class="fa fa-times"></i>
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
                                    <controls:MessageControl ID="UcMessageAlert" runat="server" />
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
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

</asp:Content>
