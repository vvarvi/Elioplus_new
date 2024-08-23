<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardFavourites.aspx.cs" Inherits="WdS.ElioPlus.DashboardFavourites" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="DashFavouritesHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashFavouritesMain" ContentPlaceHolderID="MainContent" runat="server">
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
    <div class="row">
        <div class="row">
            <div class="col-md-12">
                <div class="note note-success">
                    <h2>
                        <small>
                            <asp:Label ID="LblFavouritesPageInfo" Font-Size="14px" runat="server" />
                            <asp:Label ID="LblAdditInfo2" Font-Size="14px" runat="server" />
                        </small>
                    </h2>
                    <a id="aActionLink2" runat="server">
                        <strong><asp:Label ID="LblActionInfo2" Font-Size="14px" runat="server" /></strong>
                    </a>
                </div>            
                <div class="portlet box blue">
                    <div class="portlet-title">
                        <div class="caption"><h3><i class="fa fa-star-o" style="margin-right:10px;"></i><asp:Label ID="LblFavouritesTitle" Font-Size="18px" runat="server" /></h3></div>
                        <div class="tools">
                            <a class="collapse"> </a>                        
                            <a class="reload"> </a>                        
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-responsive">
                            <table style="min-width:680px">
                                <thead>
                                    <tr style="text-align:center;">
                                        <th style="width:160px; text-align:center;"><asp:Label ID="LblComLogo" Text="Logo" runat="server" /></th>
                                        <th style="width:420px; text-align:center;"><asp:Label ID="LblComName" Text="Company" runat="server" /></th>
                                        <th style="width:200px; text-align:center;"><asp:Label ID="LblComType" Text="Type" runat="server" /></th>
                                        <th style="width:140px; text-align:center;"><asp:Label ID="LblDate" Text="Date" runat="server" /></th>
                                        <th style="width:130px; text-align:center;"><asp:Label ID="LblActions" Text="Actions" runat="server" /></th>                                    
                                    </tr>
                                </thead>
                                <tbody>
                                    <telerik:RadGrid ID="RdgFavourites" AllowPaging="true" PageSize="20" PagerStyle-Position="Bottom" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-HorizontalAlign="Center" PagerStyle-NextPageImageUrl="~/images/prettyPhoto/light_rounded/btnNext.png" PagerStyle-PrevPageImageUrl="~/images/prettyPhoto/light_rounded/btnPrevious.png" PagerStyle-FirstPageImageUrl="~/assets/global/plugins/datatables/images/back_disabled.png" PagerStyle-LastPageImageUrl="~/assets/global/plugins/datatables/images/forward_disabled.png" OnItemDataBound="RdgFavourites_OnItemDataBound" OnNeedDataSource="RdgFavourites_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                        <MasterTableView>                                    
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="id" Display="false" />
                                                <telerik:GridBoundColumn DataField="company_id" Display="false" />
                                                <telerik:GridBoundColumn DataField="company_logo" HeaderText="Logo" Display="false" />
                                                <telerik:GridBoundColumn DataField="company_name" HeaderText="Name" Display="false" />
                                                <telerik:GridBoundColumn DataField="company_type" HeaderText="Type" Display="false" />
                                                <telerik:GridBoundColumn DataField="last_update" HeaderText="View date" Display="false" />                                        
                                                <telerik:GridBoundColumn DataField="delete" HeaderText="Delete" Display="false" />
                                                <telerik:GridTemplateColumn DataField="actions" UniqueName="actions">
                                                    <ItemTemplate>
                                                        <table class="table table-responsive table-striped table-bordered table-hover">
                                                            <tr>
                                                                <td style="width:95px;">
                                                                    <a id="aCompanyLogo" runat="server">
                                                                        <asp:Image ID="ImgCompanyLogo" Width="90" Height="40" runat="server" />
                                                                    </a>
                                                                </td>
                                                                <td style="width:260px; overflow-wrap:break-word;">
                                                                    <a id="aCompanyName" runat="server">
                                                                        <asp:Label ID="LblCompanyNameContent" Width="250" runat="server" />
                                                                    </a>
                                                                </td>
                                                                <td style="width:100px;">
                                                                    <asp:Label ID="LblCompanyTypeContent" Width="90" runat="server" />
                                                                </td>
                                                                <td style="width:90px;">
                                                                    <asp:Label ID="LblViewDateContent" Width="80" runat="server" />
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
                                <controls:MessageControl ID="UcFavouritesMessageAlert" Visible="false" runat="server"/>
                            </table>
                        </div>
                    </div>
                </div>            
            </div>        
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="note note-success">
                    <h2>
                        <small>
                            <asp:Label ID="LblFavouritedPageInfo" Font-Size="14px" runat="server" />
                            <asp:Label ID="LblAdditInfo" Font-Size="14px" runat="server" />
                        </small>
                        <a id="aActionLink1" runat="server">
                            <strong><asp:Label ID="LblActionInfo" Font-Size="14px" runat="server" /></strong>
                        </a>
                    </h2>                
                </div>
                <div class="portlet box green">
                    <div class="portlet-title">
                        <div class="caption"><h3><i class="fa fa-star" style="margin-right:10px;"></i><asp:Label ID="LblFavouritedTitle" Font-Size="18px" runat="server" /></h3></div>
                        <div class="tools">
                            <a class="collapse"> </a>                        
                            <a class="reload"> </a>                        
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-responsive">
                            <table style="min-width:690px">
                                <thead>
                                    <tr style="text-align:center;">
                                        <th style="width:170px; text-align:center;"><asp:Label ID="Label1" Text="Logo" runat="server" /></th>
                                        <th style="width:520px; text-align:center;"><asp:Label ID="Label2" Text="Company" runat="server" /></th>
                                        <th style="width:170px; text-align:center;"><asp:Label ID="Label3" Text="Type" runat="server" /></th>
                                        <th style="width:160px; text-align:center;"><asp:Label ID="Label4" Text="Date" runat="server" /></th>                                        
                                    </tr>
                                </thead>
                                <tbody>                           
                                    <telerik:RadGrid ID="RdgUserFavouritedCompanies" OnItemDataBound="RdgUserFavouritedCompanies_OnItemDataBound" OnNeedDataSource="RdgUserFavouritedCompanies_OnNeedDataSource"
                                        AutoGenerateColumns="false" runat="server">
                                        <MasterTableView>                                    
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="id" Display="false" />
                                                <telerik:GridBoundColumn DataField="user_id" Display="false" />
                                                <telerik:GridBoundColumn DataField="company_logo" HeaderText="Logo" Display="false" />
                                                <telerik:GridBoundColumn DataField="company_name" HeaderText="Name" Display="false" />
                                                <telerik:GridBoundColumn DataField="company_type" HeaderText="Type" Display="false" />
                                                <telerik:GridBoundColumn DataField="last_update" HeaderText="Save date" Display="false" />
                                                <telerik:GridTemplateColumn DataField="actions" UniqueName="actions">
                                                    <ItemTemplate>
                                                        <table class="table table-responsive table-striped table-bordered table-hover">
                                                            <tr>
                                                                <td style="width:95px;">
                                                                    <a id="aCompanyLogo" runat="server">
                                                                        <asp:Image ID="ImgCompanyLogo" Width="90" Height="40" runat="server" />
                                                                    </a>
                                                                </td>
                                                                <td style="width:310px; overflow-wrap:break-word;">
                                                                    <a id="aCompanyName" runat="server">
                                                                        <asp:Label ID="LblCompanyNameContent" Width="300" runat="server" />
                                                                    </a>
                                                                </td>
                                                                <td style="width:100px;">
                                                                    <asp:Label ID="LblCompanyTypeContent" Width="90" runat="server" />
                                                                </td>
                                                                <td style="width:100px;">
                                                                    <asp:Label ID="LblViewDateContent" Width="90" runat="server" />
                                                                </td>                                                        
                                                                <%--<td>
                                                                    <a id="BtnDelete" onserverclick="BtnDelete_OnClick" class="btn btn-circle btn-sm purple" runat="server">
                                                                        <asp:Label ID="LblBtnDelete" runat="server" />
                                                                        <i class="fa fa-times"></i>
                                                                    </a>                                                            
                                                                </td>--%>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </tbody>
                                <controls:MessageControl ID="UcFavouritedMessageAlert" Visible="false" runat="server"/>
                            </table>
                        </div>
                    </div>
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

</asp:Content>
