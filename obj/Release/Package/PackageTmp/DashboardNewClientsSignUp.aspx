<%@ Page Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardNewClientsSignUp.aspx.cs" Inherits="WdS.ElioPlus.DashboardNewClientsSignUp" %>
<%@ MasterType VirtualPath="~/DashboardMaster.master" %>

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
                    <a id="aCancel" runat="server" role="button" class="btn btn-circle purple btn-md">
                        <asp:Label ID="Label3" Text="Cancel registration" runat="server" />
                    </a>
                </div>
                
                <div class="portlet-body">                    
                    <controls:messagecontrol id="UcMessageAlert" visible="false" runat="server" />                    
                    <div class="form-group">
                        
                        <iframe name="myIframe" id="myIframe" width="100%" height="875" runat="server"></iframe>
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
