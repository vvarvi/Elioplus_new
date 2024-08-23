<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardLeadDistributionViewPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardLeadDistributionViewPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <!--begin::Card-->
                    <div class="card card-custom gutter-b">
                        <div class="card-body">
                            <div id="divPartnerHeaderInfo" runat="server">
                                <div class="grow-1">

                                    <div class="d-flex align-items-center justify-content-between flex-wrap">
                                        <div class="mr-3">
                                            <asp:Label ID="LblPartnerHeaderInfo" Text="" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div id="divSelectPartners" visible="false" runat="server">
                                            <div class="col-md-6">
                                                <asp:Label ID="LblPartners" runat="server" />
                                                <asp:Label ID="Label10" Text=" (*)" ForeColor="Red" runat="server" />
                                                <div class="form-group">
                                                    <asp:DropDownList AutoPostBack="true" CssClass="form-control" ID="DrpPartners" Width="400" OnSelectedIndexChanged="DrpPartners_SelectedIndexChanged" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="LblCountries" runat="server" />
                                                <asp:Label ID="Label2" Text=" (*)" ForeColor="Red" runat="server" />
                                                <div class="form-group">
                                                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="DdlCountries_SelectedIndexChanged" ID="DdlCountries" CssClass="form-control" Width="470" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="">
                                <div class="example example-basic" style="width: 100%;">
                                    <div class="example-preview">
                                        <!--begin: Pic-->
                                        <div class="flex-shrink-0 mr-7 mt-lg-0 mt-3" style="float: left;">
                                            <div class="symbol symbol-50 symbol-lg-120">
                                                <asp:Image ID="ImgCompanyLogo" runat="server" ImageUrl="/assets/media/project-logos/3.png" />
                                            </div>
                                            <div class="symbol symbol-50 symbol-lg-120 symbol-primary d-none">
                                                <span class="font-size-h3 symbol-label font-weight-boldest"></span>
                                            </div>
                                        </div>
                                        <!--end: Pic-->
                                        <!--begin: Info-->
                                        <div class="flex-grow-1">
                                            <!--begin: Title-->
                                            <div class="d-flex align-items-center justify-content-between flex-wrap">
                                                <div id="divSelectedPartnerHeader" runat="server" class="mr-3" style="width: 65% !important;">
                                                    <!--begin::Name-->
                                                    <a href="#" class="d-flex align-items-center text-dark text-hover-primary font-size-h5 font-weight-bold mr-3">
                                                        <asp:Label ID="LblDealPartnerName" runat="server" />

                                                        <i class="flaticon2-correct text-success icon-md ml-2"></i>

                                                    </a>
                                                    <!--end::Name-->
                                                    <!--begin::Contacts-->
                                                    <div class="d-flex flex-wrap my-2">
                                                        <a id="aEmailContent" runat="server" class="text-muted text-hover-primary font-weight-bold mr-lg-8 mr-5 mb-lg-0 mb-2">
                                                            <span class="svg-icon svg-icon-md svg-icon-gray-500 mr-1">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Mail-notification.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <rect x="0" y="0" width="24" height="24" />
                                                                        <path d="M21,12.0829584 C20.6747915,12.0283988 20.3407122,12 20,12 C16.6862915,12 14,14.6862915 14,18 C14,18.3407122 14.0283988,18.6747915 14.0829584,19 L5,19 C3.8954305,19 3,18.1045695 3,17 L3,8 C3,6.8954305 3.8954305,6 5,6 L19,6 C20.1045695,6 21,6.8954305 21,8 L21,12.0829584 Z M18.1444251,7.83964668 L12,11.1481833 L5.85557487,7.83964668 C5.4908718,7.6432681 5.03602525,7.77972206 4.83964668,8.14442513 C4.6432681,8.5091282 4.77972206,8.96397475 5.14442513,9.16035332 L11.6444251,12.6603533 C11.8664074,12.7798822 12.1335926,12.7798822 12.3555749,12.6603533 L18.8555749,9.16035332 C19.2202779,8.96397475 19.3567319,8.5091282 19.1603533,8.14442513 C18.9639747,7.77972206 18.5091282,7.6432681 18.1444251,7.83964668 Z" fill="#000000" />
                                                                        <circle fill="#000000" opacity="0.3" cx="19.5" cy="17.5" r="2.5" />
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                            <asp:Label ID="LblEmailContent" Text="" runat="server" />
                                                        </a>
                                                        <a id="aWebsiteContent" runat="server" class="text-muted text-hover-primary font-weight-bold mr-lg-8 mr-5 mb-lg-0 mb-2">
                                                            <span class="svg-icon svg-icon-md svg-icon-gray-500 mr-1">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/General/Lock.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <mask fill="white">
                                                                            <use xlink:href="#path-1" />
                                                                        </mask>
                                                                        <g />
                                                                        <path d="M7,10 L7,8 C7,5.23857625 9.23857625,3 12,3 C14.7614237,3 17,5.23857625 17,8 L17,10 L18,10 C19.1045695,10 20,10.8954305 20,12 L20,18 C20,19.1045695 19.1045695,20 18,20 L6,20 C4.8954305,20 4,19.1045695 4,18 L4,12 C4,10.8954305 4.8954305,10 6,10 L7,10 Z M12,5 C10.3431458,5 9,6.34314575 9,8 L9,10 L15,10 L15,8 C15,6.34314575 13.6568542,5 12,5 Z" fill="#000000" />
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                            <asp:Label ID="LblWebsiteContent" Text="" runat="server" />
                                                        </a>
                                                        <a id="aAddress" runat="server" class="text-muted text-hover-primary font-weight-bold">
                                                            <span class="svg-icon svg-icon-md svg-icon-gray-500 mr-1">
                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Map/Marker2.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <rect x="0" y="0" width="24" height="24" />
                                                                        <path d="M9.82829464,16.6565893 C7.02541569,15.7427556 5,13.1079084 5,10 C5,6.13400675 8.13400675,3 12,3 C15.8659932,3 19,6.13400675 19,10 C19,13.1079084 16.9745843,15.7427556 14.1717054,16.6565893 L12,21 L9.82829464,16.6565893 Z M12,12 C13.1045695,12 14,11.1045695 14,10 C14,8.8954305 13.1045695,8 12,8 C10.8954305,8 10,8.8954305 10,10 C10,11.1045695 10.8954305,12 12,12 Z" fill="#000000" />
                                                                    </g>
                                                                </svg>
                                                                <!--end::Svg Icon-->
                                                            </span>
                                                            <asp:Label ID="LblAddressContent" Text="" runat="server" />
                                                        </a>
                                                    </div>
                                                    <!--end::Contacts-->
                                                </div>
                                                <div class="my-lg-0 my-1">
                                                    <div id="divVendorActionsA" visible="false" runat="server">
                                                        <asp:Button ID="RBtnBackVendor" Visible="false" OnClick="BtnBack_OnClick" Text="Back to Deals" CssClass="btn btn-sm btn-light-primary" runat="server" />
                                                        <asp:Button ID="RBtnDelete" Visible="false" OnClick="BtnDelete_Click" Text="Delete" CssClass="btn btn-sm btn-danger" runat="server" />
                                                        <asp:Button ID="RBtnReject" OnClick="BtnReject_OnClick" Text="Reject" CssClass="btn btn-sm btn-light-danger" runat="server" />
                                                        <asp:Button ID="RBtnApprove" OnClick="BtnApprove_OnClick" Text="Approve" CssClass="btn btn-sm btn-light-success" runat="server" />
                                                    </div>
                                                    <div id="divResellerActionsA" runat="server">
                                                        <asp:Button ID="RBtnBack" OnClick="BtnBack_OnClick" Visible="false" Text="Back to Deals" CssClass="btn btn-sm btn-light-primary" runat="server" />
                                                        <asp:Button ID="BtnDelete" Visible="false" OnClick="BtnDelete_Click" Text="Delete" CssClass="btn btn-sm btn-danger" runat="server" />
                                                        <asp:Button ID="RBtnSave" OnClick="BtnSave_OnClick" Text="Send" CssClass="btn btn-sm btn-light-success" runat="server" />
                                                        <asp:Button ID="RBtnSendToCrm" Visible="false" OnClick="BtnGetLeads_Click" Text="Send lead to CRM" CssClass="btn btn-sm btn-info" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end: Title-->
                                            <!--begin: Content-->
                                            <div class="d-flex align-items-center flex-wrap justify-content-between">
                                                <div class="flex-grow-1 font-weight-bold text-dark-50 py-5 py-lg-2 mr-5" style="width: 100%; margin-bottom: 35px;">
                                                    <asp:Label ID="LblDealPartnerNameType" runat="server" />
                                                    <br />

                                                </div>
                                                <div class="d-flex flex-wrap align-items-center py-2 col-lg-12">
                                                    <div class="d-flex align-items-center col-lg-6">
                                                        <div class="col-lg-4 mr-2 mb-14">
                                                            <div class="font-weight-bold mb-2">
                                                                <asp:Label ID="LblStatus" runat="server" />
                                                            </div>
                                                            <span class="btn btn-sm btn-text btn-light-primary text-uppercase font-weight-bold">
                                                                <asp:Label ID="DdlDealStatus" runat="server" />
                                                            </span>
                                                        </div>
                                                        <div class="col-lg-8 mr-2" style="width: 60%;">
                                                            <div class="font-weight-bold">
                                                                <asp:Label ID="LblAmount" runat="server" />
                                                            </div>
                                                            <span class="btn btn-sm btn-text text-uppercase font-weight-bold">
                                                                <asp:Label ID="TbxAmount" CssClass="form-control" runat="server" />
                                                                <div class="form-group row">
                                                                    <div class="col-lg-4">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="TbxAmountEdit" placeholder="Amount" CssClass="form-control" MaxLength="9" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <span class="form-text text-muted"></span>
                                                                    </div>
                                                                    <div id="divCurrencyArea" runat="server" class="col-lg-8">
                                                                        <div class="input-group">
                                                                            <asp:DropDownList ID="DrpCurrency" Width="55" Height="36" runat="server" CssClass="form-control" />
                                                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                                                        </div>
                                                                        <span class="form-text text-muted"></span>
                                                                    </div>
                                                                </div>
                                                            </span>

                                                        </div>
                                                    </div>
                                                    <div class="d-flex align-items-center mb-10 col-lg-3">
                                                        <div class="col-lg-12 mr-8">
                                                            <div class="font-weight-bold">
                                                                <asp:Label ID="LblDealResult" runat="server" />
                                                            </div>
                                                            <span id="spanLeadResult" runat="server" class="btn btn-sm btn-text">
                                                                <asp:Label ID="DdlDealResult" CssClass="" runat="server" />
                                                                <asp:DropDownList ID="DdlDealResultEdit" CssClass="form-control" runat="server" />
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end: Content-->
                                            <controls:MessageControl ID="UcMessageControlAlert" runat="server" />
                                            <div id="divOpportSuccess" runat="server" visible="false" class="col-md-12 alert alert-success" style="text-align: center;">
                                                <strong>
                                                    <asp:Label ID="LblOpportSucc" Text="Done! " runat="server" />
                                                </strong>
                                                <asp:Label ID="LblOpportSuccCont" runat="server" />
                                            </div>
                                            <div id="divOpportError" runat="server" visible="false" class="col-md-12 alert alert-danger" style="text-align: center;">
                                                <strong>
                                                    <asp:Label ID="LblOpportError" Text="Error! " runat="server" />
                                                </strong>
                                                <asp:Label ID="LblOpportErrorCont" runat="server" />
                                            </div>
                                        </div>
                                        <!--end: Info-->
                                    </div>
                                </div>
                            </div>
                            <div class="separator separator-solid my-7"></div>

                            <div class="card-body">
                                <div class="form-group row">
                                    <label class="col-lg-2 col-form-label text-right">
                                        <span class="font-weight-bolder">
                                            <span class="text-dark-50 font-weight-bold"></span>
                                            <asp:Label ID="LblOrganiz" runat="server" />
                                        </span>
                                    </label>
                                    <div class="col-lg-3">
                                        <div class="input-group mt-3">
                                            <asp:Label ID="TbxOrganiz" runat="server" />
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                    <label class="col-lg-2 col-form-label text-right">
                                        <span class="font-weight-bolder">
                                            <span class="text-dark-50 font-weight-bold"></span>
                                            <asp:Label ID="LblFirstName" runat="server" />
                                        </span>
                                    </label>
                                    <div class="col-lg-3">
                                        <div class="input-group mt-3">
                                            <asp:Label ID="TbxFirstName" runat="server" />
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-lg-2 col-form-label text-right">
                                        <span class="font-weight-bolder">
                                            <span class="text-dark-50 font-weight-bold"></span>
                                            <asp:Label ID="LblLastName" runat="server" />
                                        </span>
                                    </label>
                                    <div class="col-lg-3">
                                        <div class="input-group mt-3">
                                            <asp:Label ID="TbxLastName" runat="server" />
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                    <label class="col-lg-2 col-form-label text-right">
                                        <span class="font-weight-bolder">
                                            <span class="text-dark-50 font-weight-bold"></span>
                                            <asp:Label ID="LblEmail" runat="server" />
                                        </span>
                                    </label>
                                    <div class="col-lg-3">
                                        <div class="input-group mt-3">
                                            <a id="aEmail" runat="server" class="text-primary">
                                                <asp:Label ID="TbxEmail" runat="server" />
                                            </a>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-lg-2 col-form-label text-right">
                                        <span class="font-weight-bolder">
                                            <span class="text-dark-50 font-weight-bold"></span>
                                            <asp:Label ID="LblWebsite" runat="server" />
                                        </span>
                                    </label>
                                    <div class="col-lg-3">
                                        <div class="input-group mt-3">
                                            <a id="aWebsite" runat="server" class="text-primary">
                                                <asp:Label ID="TbxWebsite" runat="server" />
                                            </a>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                    <label class="col-lg-2 col-form-label text-right">
                                        <span class="font-weight-bolder">
                                            <span class="text-dark-50 font-weight-bold"></span>
                                            <asp:Label ID="LblPhone" runat="server" />
                                        </span>
                                    </label>
                                    <div class="col-lg-3">
                                        <div class="input-group mt-3">
                                            <asp:Label ID="TbxPhone" runat="server" />
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-lg-2 col-form-label text-right">
                                        <span class="font-weight-bolder">
                                            <span class="text-dark-50 font-weight-bold"></span>
                                            <asp:Label ID="LblComments" runat="server" />
                                        </span>
                                    </label>
                                    <div class="col-lg-8">
                                        <div class="input-group mt-3">
                                            <asp:Label ID="TbxComments" runat="server" />
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <controls:MessageControl ID="UcMessageControl" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end::Card-->
                    <!--end::Dashboard-->
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <!-- Leads Service Modal -->
    <div id="LeadsServiceModal" class="modal fade" tabindex="-1" data-width="200">
        <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="Label8" Text="Get Leads Service" CssClass="control-label" runat="server" />
                            </h4>
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
                                                        <asp:Label ID="LblLeadsText" Text="Get leads from " CssClass="control-label" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList AutoPostBack="true" Visible="true" ID="DrpUserCrmList" CssClass="form-control" Width="280" OnSelectedIndexChanged="DrpUserCrmList_SelectedIndexChanged" runat="server">
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
                                                        <asp:Label ID="LblLeadEmail" Text="Company email" CssClass="control-label" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TbxLeadEmail" Width="415" CssClass="form-control" runat="server" />
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
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnGetData" OnClick="BtnGetData_Click" runat="server" CssClass="btn btn-primary" Text="Get Data from CRM" />
                            <asp:Button ID="BtnSendData" OnClick="BtnSendData_Click" Visible="false" runat="server" CssClass="btn btn-light-success" Text="Send Data to CRM" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblConfTitle" Text="Confirmation" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="LblConfMsg" Text="Are you sure you want to delete this deal?" runat="server" />
                                    <asp:HiddenField ID="TbxId" Value="0" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="UcPopUpConfirmationMessageAlert" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">No</button>
                            <asp:Button ID="BtnConfDelete" OnClick="BtnConfDelete_OnClick" CssClass="btn btn-primary" Text="Yes" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <!--begin::Page Scripts(used by this page)-->
        <script src="/assets/js/pages/widgets.js"></script>
        <!--end::Page Scripts-->

        <script type="text/javascript">

            function OpenLeadsServiceModal() {
                $('#LeadsServiceModal').modal('show');
            }

            function CloseLeadsServiceModal() {
                $('#LeadsServiceModal').modal('hide');
            }

            function CloseConfirmPopUp() {
                $('#divConfirm').modal('hide');
            }

            function OpenConfirmPopUp() {
                $('#divConfirm').modal('show');
            }

        </script>

    </telerik:RadScriptBlock>

</asp:Content>
