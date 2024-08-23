<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardMessages.aspx.cs" Inherits="WdS.ElioPlus.DashboardMessages" %>

<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

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
    <div class="inbox">
        <div class="row">
            <div class="col-md-2">
                <div class="inbox-sidebar">
                    <a id="aCompose" runat="server" onserverclick="Compose_OnClick" data-title="Compose" class="btn red compose-btn btn-block">
                        <i class="fa fa-edit"></i> Compose 
                    </a>
                    <ul class="inbox-nav">
                        <li id="liInbox" runat="server" class="active">
                            <a id="aInbox" runat="server" data-type="inbox" data-title="Inbox"><asp:Label ID="LblInbox" runat="server" />
                                <span class="badge badge-success" id="spanNumberNewMessages" runat="server" visible="false">
                                    <asp:Label ID="LblNumberNewMessages" runat="server" />
                                </span>
                            </a>
                        </li>                        
                        <li id="liSent" runat="server">
                            <a id="aSent" runat="server" data-type="sent" data-title="Sent"><asp:Label ID="LblSent" runat="server" /></a>
                        </li>                        
                        <li class="divider"></li>
                        <li id="liTrash" runat="server">
                            <a id="aTrash" runat="server" data-title="Trash"><asp:Label ID="LblTrash" runat="server" /></a>
                        </li>                        
                    </ul>                    
                </div>
            </div>
            <div class="col-md-10">
                <div id="divWarningMessage" runat="server" visible="false" class="alert alert-warning alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
                    <strong><asp:Label ID="LblWarning" runat="server" /></strong><asp:Label ID="LblWarningContent" runat="server" />
                    <a id="aFullRegist" runat="server" class="alert-link"><asp:Label ID="LblActionLink" runat="server" /></a>
                </div>
                <div class="inbox-body">                    
                    <div class="inbox-content">                        
                        <asp:PlaceHolder ID="PhInboxContent" runat="server" />
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
