<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardLeadDistributionAddEditPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardLeadDistributionAddEditPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>

    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="card card-custom gutter-b example example-compact">
                        <div id="divPartnerHeaderInfo" runat="server" class="card-header">
                            <h3 class="card-title">
                                <asp:Label ID="LblPartnerHeaderInfo" Text="Step 1: Select your Partner" runat="server" />
                            </h3>
                            <div class="card-toolbar">
                            </div>
                        </div>
                        <div id="divSelectPartners" runat="server" class="card-body">
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Select Partner:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-map-pin"></i></span></div>
                                        <asp:DropDownList AutoPostBack="true" ID="DrpPartners" OnSelectedIndexChanged="DrpPartners_SelectedIndexChanged" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Select Country:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <div class="input-group-append"><span class="input-group-text"><i class="flaticon-map-location"></i></span></div>
                                        <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="DdlCountries_SelectedIndexChanged" CssClass="form-control" ID="DdlCountries" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                        </div>

                        <div id="divStep2Wizard" visible="false" runat="server">
                            <div class="card-header">
                                <div class="row" style="margin-bottom:-20px;">
                                    <div class="col-lg-6">
                                        <h3 class="card-title" style="font-size:1.275rem;">
                                            <asp:Label ID="LblPersonalInfo" Text="Step 2: Fill in details " runat="server" />
                                        </h3>
                                    </div>
                                    <div class="col-lg-4">
                                        <h3 style="font-size:1.275rem;">
                                            <asp:Label ID="Label11" Text="OR Pull data from CRM" runat="server" />
                                        </h3>
                                    </div>
                                    <div class="col-lg-2" style="margin-top:-15px;">
                                        <asp:Button ID="BtnGetLeads" OnClick="BtnGetLeads_Click" Text="Pull Data" CssClass="btn btn-danger" runat="server" />
                                        <a id="aGoToIntegrations" runat="server" visible="false">
                                            <asp:Label ID="LblGoToIntegrations" CssClass="btn btn-danger" Text="Integration with CRM" runat="server" />                                            
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <form class="form">
                                <div class="card-body">
                                    <div class="form-group row">
                                        <label class="col-lg-2 col-form-label text-right">First Name:</label>
                                        <div class="col-lg-3">
                                            <div class="input-group">
                                                <asp:TextBox ID="TbxFirstName" placeholder="First Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                            </div>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                        <label class="col-lg-2 col-form-label text-right">Last Name:</label>
                                        <div class="col-lg-3">
                                            <div class="input-group">
                                                <asp:TextBox ID="TbxLastName" placeholder="Last Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                            </div>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 col-form-label text-right">Company Name:</label>
                                        <div class="col-lg-3">
                                            <div class="input-group">
                                                <asp:TextBox ID="TbxOrganiz" placeholder="Company Name" CssClass="form-control" MaxLength="95" runat="server" />
                                                <div class="input-group-append"><span class="input-group-text"><i class="la la-map-marker"></i></span></div>
                                            </div>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                        <label class="col-lg-2 col-form-label text-right">Business Email:</label>
                                        <div class="col-lg-3">
                                            <div class="input-group">
                                                <asp:TextBox ID="TbxEmail" placeholder="Business Email" CssClass="form-control" MaxLength="95" runat="server" />
                                                <div class="input-group-append"><span class="input-group-text">@</i></span></div>
                                            </div>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 col-form-label text-right">Business Website:</label>
                                        <div class="col-lg-3">
                                            <div class="input-group">
                                                <asp:TextBox ID="TbxWebsite" placeholder="Business Website" CssClass="form-control" MaxLength="95" runat="server" />
                                                <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                            </div>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                        <label class="col-lg-2 col-form-label text-right">Business Phone:</label>
                                        <div class="col-lg-3">
                                            <div class="input-group">
                                                <asp:TextBox ID="TbxPhone" placeholder="Business Phone" CssClass="form-control" MaxLength="45" runat="server" />
                                                <div class="input-group-append"><span class="input-group-text"><i class="flaticon2-phone"></i></span></div>
                                            </div>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 col-form-label text-right">Comments:</label>
                                        <div class="col-lg-8">
                                            <div class="input-group">
                                                <asp:TextBox ID="TbxComments" placeholder="Type Comments" TextMode="MultiLine" Rows="5" CssClass="form-control form-control-lg form-control" MaxLength="500" runat="server" />
                                                <div class="input-group-append"><span class="input-group-text"><i class="la la-bookmark-o"></i></span></div>
                                            </div>
                                            <span class="form-text text-muted"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div id="divVendorActionsA" visible="false" runat="server">
                                                <asp:Button ID="BtnBackVendor" Visible="false" OnClick="BtnBack_OnClick" Text="Back to Leads" CssClass="btn btn-secondary mr-2" runat="server" />
                                                <asp:Button ID="BtnReject" OnClick="BtnReject_OnClick" Text="Reject" CssClass="btn btn-danger mr-2" runat="server" />
                                                <asp:Button ID="BtnApprove" OnClick="BtnApprove_OnClick" Text="Approve" CssClass="btn btn-success mr-2" runat="server" />
                                            </div>

                                            <div id="divResellerActionsA" runat="server">
                                                <asp:Button ID="BtnBack" Visible="false" OnClick="BtnBack_OnClick" Text="Back to Leads" CssClass="btn btn-secondary mr-2" runat="server" />
                                                <asp:Button ID="BtnClear" OnClick="BtnClear_OnClick" Text="Clear" CssClass="btn btn-light-primary mr-2" runat="server" />
                                                <asp:Button ID="BtnSave" OnClick="BtnSave_OnClick" Text="Send" CssClass="btn btn-success mr-2" runat="server" />
                                                <asp:Button ID="BtnSendToCrm" Visible="false" OnClick="BtnGetLeads_Click" Text="Send lead to CRM" CssClass="btn blue-primary mr-2" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-lg-6"></div>
                                    </div>
                                    <div class="row">
                                        <controls:MessageControl ID="UcMessageAlert" runat="server" />
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <!-- Pop Up Invitation form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblFileUploadTitle" CssClass="control-label" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group" style="width:100%;">
                                    <controls:MessageControl ID="UploadMessageAlert" Visible="false" runat="server" />
                                    <asp:Label ID="LblFileUploadfMsg" Visible="false" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-light-primary">Close</button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Leads Service Modal -->
    <div id="LeadsServiceModal" class="modal fade" tabindex="-1">
        <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="Label8" Text="Get Leads Service" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div id="divFreemiumArea" runat="server">
                                    <div class="form-group">
                                        <controls:MessageControl ID="FreemiumMessageControl" Visible="true" runat="server" />
                                    </div>
                                </div>
                                <div id="divPremiumArea" visible="false" runat="server">
                                    <div class="form-group">
                                        <div id="divCrmListArea" runat="server" visible="false" class="" style="float: left; width: 100%;">
                                            <table>
                                                <tr>
                                                    <td style="width: 140px; height: 50px;">
                                                        <asp:Label ID="LblLeadsText" Text="Get leads from " runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList AutoPostBack="true" Visible="true" ID="DrpUserCrmList" Width="350" CssClass="form-control" OnSelectedIndexChanged="DrpUserCrmList_SelectedIndexChanged" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div id="divSearchLeadInfo" runat="server" visible="false" class="" style="float: left; width: 100%;">
                                            <table>
                                                <tr>
                                                    <td style="width: 140px; height: 50px;">
                                                        <asp:Label ID="LblLeadEmail" Text="Company email" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TbxLeadEmail" Width="350" CssClass="form-control" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 25px;">
                                <div id="divCrmErrorMessage" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblLeadCrmError" Text="Error! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblLeadCrmErrorMessage" runat="server" />
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
                            <button type="button" data-dismiss="modal" class="btn btn-light-primary">Close</button>
                            <asp:Button ID="BtnGetData" OnClick="BtnGetData_Click" runat="server" CssClass="btn btn-primary" Text="Get Data from CRM" />
                            <asp:Button ID="BtnSendData" OnClick="BtnSendData_Click" Visible="false" runat="server" CssClass="btn btn-light-success" Text="Send Data to CRM" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">
            
            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }

            function OpenLeadsServiceModal() {
                $('#LeadsServiceModal').modal('show');
            }

            function CloseLeadsServiceModal() {
                $('#LeadsServiceModal').modal('hide');
            }
        </script>

    </telerik:RadScriptBlock>

</asp:Content>
