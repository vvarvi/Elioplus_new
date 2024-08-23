<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardPartnersCommissionsFeesPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardPartnersCommissionsFeesPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
                <ContentTemplate>                   
                    <div class="card card-custom">

                        <div class="card-header flex-wrap py-5">
                            <div class="card-title">
                                <h3 class="card-label">
                                    <asp:Label ID="LblHeaderInfo" Text="International and country-specific fees" runat="server" />
                                </h3>
                            </div>
                            <div class="card-toolbar">
                                <div class="example-preview">
                                    <div class="dropdown dropdown-inline mr-10">
                                        <button type="button" class="btn btn-light-primary btn-icon btn-sm" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <i class="ki ki-bold-more-hor"></i>
                                        </button>
                                        <div class="dropdown-menu dropdown-menu-sm" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 33px, 0px);" x-placement="bottom-start">
                                            <a id="aCommissionBillingDetails" runat="server" class="dropdown-item">Edit Billing Details</a>
                                            <a id="aCommissionFeesTerms" runat="server" class="dropdown-item">Fees & Terms</a>
                                            <a id="aCommissionPayments" runat="server" class="dropdown-item">Payments</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="col-6" style="margin-bottom: 30px;">
                                <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                    <li class="nav-item">
                                        <a href="#tab_1_1" data-toggle="tab" class="nav-link active">
                                            <span class="nav-icon"><i class="flaticon-customer"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblCommissionsTransfers" Text="Fees by country" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                </ul>
                            </div>

                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    
                                    <!--begin: Datatable-->
                                    <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                        <telerik:RadGrid ID="RdgCommisions" AllowPaging="false" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowSorting="true" PagerStyle-Position="Bottom"
                                            PageSize="10" Width="100%" CssClass="table table-separate table-head-custom table-checkable"
                                            OnNeedDataSource="RdgCommisions_OnNeedDataSource"
                                            AutoGenerateColumns="false"
                                            runat="server">
                                            <MasterTableView Width="100%" Name="Parent">
                                                <NoRecordsTemplate>
                                                    <div class="emptyGridHolder">
                                                        <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                    </div>
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                    <telerik:GridBoundColumn HeaderText="Country" DataField="name" UniqueName="name" />
                                                    <telerik:GridBoundColumn HeaderText="Onboarding & verification" DataField="currency_id" UniqueName="currency_id" />
                                                    <telerik:GridBoundColumn HeaderText="Cross-border transfers" DataField="transfers_per_cent" UniqueName="transfers_per_cent" />
                                                    <telerik:GridBoundColumn HeaderText="Payouts" DataField="payouts" UniqueName="payouts" />
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <!--end: Datatable-->
                                </div>
                            </div>
                            <controls:MessageControl ID="UcSendMessageAlertConfirmed" runat="server" />
                        </div>
                    </div>
                    <!--end::Card-->
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <!-- Invitation form (modal view) -->
    <div id="SendInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="LblInvitationSendTitle" Text="Invitation Send" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblSuccessfullSendfMsg" CssClass="control-label" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Confirmation Delete Partner form (modal view) -->
    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblConfTitle" Text="Confirmation" CssClass="control-label" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="LblConfMsg" Text="Are you sure you want to delete this partner from your list?" CssClass="control-label" runat="server" />
                                    <asp:HiddenField ID="HdnVendorResellerCollaborationId" Value="0" runat="server" />
                                    <asp:HiddenField ID="HdnPartnerUserId" Value="0" runat="server" />
                                </div>
                            </div>
                            <div class="row" style="margin-top: 25px;">
                                <div id="divFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblFailure" Text="Error! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblFailureMsg" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">
                                                <i class="ki ki-close"></i>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                                <div id="divSuccess" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblSuccess" Text="Done! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblSuccessMsg" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">
                                                <i class="ki ki-close"></i>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnDelete" Text="Delete" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up Assign Partner (modal view) -->
    <div id="PopUpAssignPartner" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblMessageTitle" Text="Assign Partner to Team Member" CssClass="control-label" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group" style="width: 100%;">
                                    <span class="btn btn-sm btn-text" style="padding-bottom: 0px;">
                                        <asp:Label ID="Label3" Text="List of your team members:" runat="server" />
                                    </span>
                                    <span class="btn btn-sm btn-text">
                                        <asp:DropDownList ID="DrpTeamMembers" CssClass="form-control" Width="400" runat="server" />
                                    </span>
                                </div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="UcAssignmentMessageAlert" Visible="false" runat="server" />
                                <asp:HiddenField ID="HdnVendResId" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnSubAcId" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnPartId" Value="0" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <a id="aSaveAssignToMember" class="btn btn-primary" runat="server">Save
                            </a>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <style>
            .RadGrid_MetroTouch .rgAltRow {
                background-color: transparent !important;
            }

            .RadGrid_MetroTouch .rgRow {
                background-color: transparent !important;
            }
        </style>

        <!--begin::Page Scripts(used by this page)-->
        <script src="assets/js/pages/custom/contacts/list-datatable.js"></script>
        <!--end::Page Scripts-->

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

            function OpenAssignPartnerPopUp() {
                $('#PopUpAssignPartner').modal('show');
            }

            function CloseAssignPartnerPopUp() {
                $('#PopUpAssignPartner').modal('hide');
            }

            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }

            function OpenConfPopUp() {
                $('#divConfirm').modal('show');
            }

            function CloseConfPopUp() {
                $('#divConfirm').modal('hide');
            }

            function OpenSendInvitationPopUp() {
                $('#SendInvitationModal').modal('show');
            }

            function CloseSendInvitationPopUp() {
                $('#SendInvitationModal').modal('hide');
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
