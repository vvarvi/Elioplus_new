<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardNewClients.aspx.cs" Inherits="WdS.ElioPlus.DashboardNewClients" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function RapPage_OnRequestStart(sender, args) {
            $('#loader').show();
        }
        function endsWith(s) {
            return this.length >= s.length && this.substr(this.length - s.length) == s;
        }
        function RapPage_OnResponseEnd(sender, args) {
            $('#loader').hide();
        }
    </script>
</telerik:RadScriptBlock>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>

    <telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">
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
            <div class="col-md-12">
                <div style="padding: 10px;" class="form-group">
                </div>               
                <div class="portlet-body">
                    <h2><asp:Label ID="LblTitle" Text="Add a new client" runat="server" /></h2>                
                
                    <controls:messagecontrol id="UcMessageAlert" visible="false" runat="server" />
                    <div style="padding:10px;">
                        <a id="aAddNewClient" runat="server" role="button" class="btn btn-circle purple btn-md">
                            <asp:Label ID="Label3" Text="Create a new company profile" runat="server" />
                        </a>
                    </div> 
                    <div class="form-group">
                        <telerik:RadGrid ID="RdgResults" CssClass="col-lg-10 col-md-9 col-sm-8 col-xs-12" HeaderStyle-CssClass="display-none" BorderStyle="None" AllowPaging="true" PageSize="8" OnPageIndexChanged="RdgResults_PageIndexChanged" OnItemCreated="RdgResults_ItemCreated" OnItemDataBound="RdgResults_OnItemDataBound" OnNeedDataSource="RdgResults_OnNeedDataSource" AutoGenerateColumns="false" runat="server" PagerStyle-Position="Bottom" MasterTableView-CssClass="col-lg-10 col-md-9 col-sm-8 col-xs-12" ItemStyle-CssClass="col-lg-3 col-md-6 nopad-rgt" AlternatingItemStyle-CssClass="col-lg-3 col-md-6 nopad-rgt">
                            <MasterTableView>
                                <NoRecordsTemplate>
                                    <div class="emptyGridHolder">
                                        <asp:Literal ID="LtlNoDataFound" runat="server" />
                                    </div>
                                </NoRecordsTemplate>
                                <Columns>                            
                                    <telerik:GridBoundColumn Display="false" DataField="client_id" UniqueName="client_id" />
                                    <telerik:GridBoundColumn Display="false" DataField="supervisor_id" UniqueName="supervisor_id" />
                                    <telerik:GridBoundColumn Display="false" DataField="company_name" UniqueName="company_name" />
                                    <telerik:GridBoundColumn Display="false" DataField="company_logo" UniqueName="company_logo" />
                                    <telerik:GridBoundColumn Display="false" DataField="email" UniqueName="email" />
                                    <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                    <telerik:GridTemplateColumn>
                                        <ItemTemplate>
                                            <div class="elio-res" style="text-align:center; display:inline-block; height:200px;">                                                
                                                <!-- logo -->
                                                <div class="pic text-center">
                                                    <asp:ImageButton ID="ImgBtnAccountLogo" Width="150" Height="150" OnClick="ImgBtnAccountLogo_OnClick" ImageUrl="~/images/no_logo.jpg" runat="server" />
                                                </div>
                                                <!-- / logo -->   
                                                <div>
                                                    <asp:Label ID="LblProfileStatus" Text="Public Profile: " runat="server" />
                                                    <asp:Label ID="LblProfileStatusValue" runat="server" />
                                                    <telerik:RadComboBox ID="RcbxPublic" Visible="false" Width="73" runat="server">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Value="1" Text="Yes" runat="server" />
                                                            <telerik:RadComboBoxItem Value="0" Text="No" runat="server" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </div>                                             
                                                <div class="pic text-center">
                                                    <asp:ImageButton ID="ImgBtnEdit" OnClick="ImgBtnEdit_OnClick" ImageUrl="~/images/edit.png" runat="server" />
                                                    <telerik:RadToolTip ID="RadToolTip2" TargetControlID="ImgBtnEdit" Position="BottomRight" Animation="Fade" Text="Edit Account" runat="server" />
                                                    <asp:ImageButton ID="ImgBtnSaveChanges" Visible="false" OnClick="ImgBtnSaveChanges_OnClick" ImageUrl="~/Images/save.png" runat="server" />
                                                    <telerik:RadToolTip ID="RadToolTip3" TargetControlID="ImgBtnSaveChanges" Position="BottomRight" Animation="Fade" Text="Save Changes" runat="server" />
                                                    <asp:ImageButton ID="ImgBtnCancel" Visible="false" OnClick="ImgBtnCancel_OnClick" ImageUrl="~/Images/cancel.png" runat="server" />                                                
                                                    <telerik:RadToolTip ID="RttMessage" TargetControlID="ImgBtnCancel" Position="BottomRight" Animation="Fade" Text="Cancel Edit Account" runat="server" />
                                                    <asp:ImageButton ID="ImgBtnLoginAsCompany" OnClick="ImgBtnLoginAsCompany_OnClick" ImageUrl="~/Images/admin.png" runat="server" />  
                                                    <telerik:RadToolTip ID="RttLoginMessage" TargetControlID="ImgBtnLoginAsCompany" Position="BottomRight" Animation="Fade" Text="Login As Supervisor" runat="server" />
                                                    <asp:ImageButton ID="ImgBtnDeleteCompany" OnClick="ImgBtnDeleteCompany_OnClick" ImageUrl="~/Images/icons/small/delete.png" runat="server" />  
                                                    <telerik:RadToolTip ID="RadToolTip1" TargetControlID="ImgBtnDeleteCompany" Position="BottomRight" Animation="Fade" Text="Delete Client" runat="server" />
                                                </div>
                                            </div>                                                                       
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>                                             
                    </div>      
                </div>           
            </div>        
        </div>
       
        <div id="loader" style="display:none;">
            <div id="loadermsg">
            </div>
        </div>
    </telerik:RadAjaxPanel>

    <!-- Payment form (modal view) -->
    <div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <controls:UcStripe id="UcStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
