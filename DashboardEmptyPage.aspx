<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardEmptyPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardEmptyPage" %>

<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <div class="row">
                <div class="col-xl-6">
                    <!--begin::Card-->
                    
                    <!--end::Card-->
                </div>
                <div class="col-xl-6">
                    <!--begin::Card-->
                    
                    <!--end::Card-->
                </div>
            </div>
            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <!--begin::Page Scripts(used by this page)-->

        <!--end::Page Scripts-->

    </telerik:RadScriptBlock>

</asp:Content>
