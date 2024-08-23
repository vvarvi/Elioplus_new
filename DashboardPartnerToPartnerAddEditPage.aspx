<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardPartnerToPartnerAddEditPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardPartnerToPartnerAddEditPage" %>

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
                    <div class="card card-custom gutter-b example example-compact">

                        <div id="divSelectedPartnerHeader" style="margin-bottom: -30px;" visible="false" runat="server">
                            <div class="card-header" style="margin-bottom: 0px;">
                                <h3 class="card-title">
                                    <asp:Label ID="LblDealPartnerTitle" Text="Partner Information" runat="server" />
                                </h3>
                                <div class="card-toolbar">
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="d-flex">
                                    <!--begin: Pic-->
                                    <div class="flex-shrink-0 mr-7 mt-lg-0 mt-3">
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
                                            <div id="div3" runat="server" class="mr-3" style="width: 65% !important;">
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
                                        </div>
                                        <!--end: Title-->
                                    </div>
                                    <!--end: Info-->
                                </div>
                                <div class="separator separator-solid my-7"></div>
                            </div>
                        </div>

                        <div class="card-header" id="divStatusTitleSection" runat="server" style="margin-bottom: 0px;">
                            <h3 class="card-title">
                                <asp:Label ID="LblDealStatusSection" Text="Change Deal Status" runat="server" />
                            </h3>
                            <div class="card-toolbar">
                            </div>
                        </div>
                        <div class="card-body" id="divStatus" runat="server">
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Deal Status:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DdlP2pStatus" AutoPostBack="true" OnSelectedIndexChanged="DdlP2pStatus_SelectedIndexChanged" Width="245" Height="36" runat="server">
                                        </asp:DropDownList>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                        </div>

                        <div class="card-header">
                            <h3 class="card-title">
                                <asp:Label ID="LblPersonalInfo" Text="Deal Information" runat="server" />
                            </h3>
                            <div class="card-toolbar">
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Opportunity Name:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxOpportunityName" placeholder="Opportunity Name" CssClass="form-control" MaxLength="45" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Product:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxProduct" placeholder="Customer's Company Product" CssClass="form-control" MaxLength="145" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="flaticon2-phone"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Client's location:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DdlCountries" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Deal Value:</label>
                                <div class="col-lg-3">
                                    <div class="input-group" style="float: left; width: 210px;">
                                        <asp:TextBox ID="TbxDealValue" placeholder="Deal Value" CssClass="form-control" MaxLength="145" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="" style="float: right;">
                                        <asp:DropDownList ID="DdlCurrency" CssClass="form-control" Width="75" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Opportunity Description:</label>
                                <div class="col-lg-8">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxOpportunityDescription" placeholder="Customer's Company Description" TextMode="MultiLine" Rows="5" CssClass="form-control" MaxLength="500" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-bookmark-o"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                        </div>

                        <div class="card-header">
                            <h3 class="card-title">
                                <asp:Label ID="LblVerticals" Text="Your business vertical selection" runat="server" />
                            </h3>
                            <div class="card-toolbar">
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row mb-5">
                                <asp:Panel ID="PnlVerticals" runat="server">
                                    <form action="#" class="form-horizontal col-lg-12" role="form">
                                        <div class="form-group row pl-2">
                                            <div class="pt-2 mb-0">
                                                <div class="form-group mb-0 row">
                                                    <label class="col-10 col-form-label text-right">
                                                        <asp:Label ID="LblCriteria1" runat="server" />
                                                    </label>
                                                    <div class="col-2 text-right">
                                                        <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                            <label>
                                                                <input id="CbxCrit1" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                                <span></span>
                                                            </label>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="form-group mb-0 row">
                                                    <label class="col-10 col-form-label text-right">
                                                        <asp:Label ID="LblCriteria2" runat="server" />
                                                    </label>
                                                    <div class="col-2 text-right">
                                                        <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                            <label>
                                                                <input id="CbxCrit2" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                                <span></span>
                                                            </label>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="form-group mb-0 row">
                                                    <label class="col-10 col-form-label text-right">
                                                        <asp:Label ID="LblCriteria3" runat="server" />
                                                    </label>
                                                    <div class="col-2 text-right">
                                                        <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                            <label>
                                                                <input id="CbxCrit3" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                                <span></span>
                                                            </label>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="form-group mb-0 row">
                                                    <label class="col-10 col-form-label text-right">
                                                        <asp:Label ID="LblCriteria4" runat="server" />
                                                    </label>
                                                    <div class="col-2 text-right">
                                                        <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                            <label>
                                                                <input id="CbxCrit4" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                                <span></span>
                                                            </label>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="form-group mb-0 row">
                                                    <label class="col-10 col-form-label text-right">
                                                        <asp:Label ID="LblCriteria5" runat="server" />
                                                    </label>
                                                    <div class="col-2 text-right">
                                                        <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                            <label>
                                                                <input id="CbxCrit5" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                                <span></span>
                                                            </label>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="form-group mb-0 row">
                                                    <label class="col-10 col-form-label text-right">
                                                        <asp:Label ID="LblCriteria6" runat="server" />
                                                    </label>
                                                    <div class="col-2 text-right">
                                                        <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                            <label>
                                                                <input id="CbxCrit6" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                                <span></span>
                                                            </label>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="form-group mb-0 row">
                                                    <label class="col-10 col-form-label text-right">
                                                        <asp:Label ID="LblCriteria7" runat="server" />
                                                    </label>
                                                    <div class="col-2 text-right">
                                                        <span class="switch switch-sm switch-outline switch-icon switch-brand">
                                                            <label>
                                                                <input id="CbxCrit7" onclick="UpdateCheckBoxes()" value="0" runat="server" type="checkbox" name="select">
                                                                <span></span>
                                                            </label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:HiddenField ID="HdnVert1Ckd" Value="0" runat="server" />
                                        <asp:HiddenField ID="HdnVert2Ckd" Value="0" runat="server" />
                                        <asp:HiddenField ID="HdnVert3Ckd" Value="0" runat="server" />
                                        <asp:HiddenField ID="HdnVert4Ckd" Value="0" runat="server" />
                                        <asp:HiddenField ID="HdnVert5Ckd" Value="0" runat="server" />
                                        <asp:HiddenField ID="HdnVert6Ckd" Value="0" runat="server" />
                                        <asp:HiddenField ID="HdnVert7Ckd" Value="0" runat="server" />

                                        <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                                        <asp:HiddenField ID="HiddenField2" Value="0" runat="server" />
                                        <asp:HiddenField ID="HiddenField3" Value="0" runat="server" />
                                        <asp:HiddenField ID="HiddenField4" Value="0" runat="server" />
                                        <asp:HiddenField ID="HiddenField5" Value="0" runat="server" />
                                        <asp:HiddenField ID="HiddenField6" Value="0" runat="server" />
                                        <asp:HiddenField ID="HiddenField7" Value="0" runat="server" />

                                    </form>
                                </asp:Panel>
                            </div>
                        </div>

                        <div class="card-footer">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div id="divVendorActionsA" visible="false" runat="server">
                                        <asp:Button ID="BtnBackVendor" Visible="false" OnClick="BtnBack_OnClick" Text="Back to Deals" CssClass="btn btn-secondary mr-2" runat="server" />
                                        <asp:Button ID="BtnReject" OnClick="BtnReject_OnClick" Text="Reject" CssClass="btn btn-danger mr-2" runat="server" />
                                        <asp:Button ID="BtnApprove" OnClick="BtnApprove_OnClick" Text="Approve" CssClass="btn btn-success mr-2" runat="server" />
                                    </div>

                                    <div id="divResellerActionsA" runat="server">
                                        <asp:Button ID="BtnBack" Visible="false" OnClick="BtnBack_OnClick" Text="Back to Deals" CssClass="btn btn-secondary mr-2" runat="server" />
                                        <asp:Button ID="BtnClear" OnClick="BtnClear_OnClick" Text="Clear" CssClass="btn btn-light-primary mr-2" runat="server" />
                                        <asp:Button ID="BtnSave" OnClick="BtnSave_OnClick" Text="Send" CssClass="btn btn-success mr-2" runat="server" />
                                    </div>
                                </div>
                                <div class="col-lg-6 text-right">
                                    <a id="aSendMessage" visible="false" style="width: 200px;" runat="server" class="btn btn-light-success" href="#myModal" role="button" data-toggle="modal">
                                        <i style="margin: 5px;" class="fa fa-envelope-o"></i>
                                        <asp:Label ID="LblSendMessage" Text="Express your interest" runat="server" />
                                    </a>
                                </div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="UcMessageAlert" runat="server" />
                            </div>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <!-- P2P Partner form (modal view) -->
    <div id="myP2pPartnerModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="BtnP2pCloseModal" OnClick="BtnP2pCancelMsg_OnClick" Text="X" CssClass="close" aria-hidden="true" runat="server" />
                            <h3 id="H1">
                                <asp:Label ID="LblP2pMessageHeader" Text="Partner to attach deal" runat="server" /></h3>
                        </div>
                        <div class="modal-body">
                            <form class="form-horizontal col-sm-12">

                                <div class="form-group">
                                    <asp:Label ID="LblP2pCompanyEmail" Text="Partner company email" runat="server" /><asp:TextBox ID="TbxP2pCompanyEmail" MaxLength="100" CssClass="form-control email" placeholder="E-mail" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnP2pCancelMsg" OnClick="BtnP2pCancelMsg_OnClick" Text="Close" CssClass="btn" aria-hidden="true" runat="server" />
                            <asp:Button ID="BtnP2pSubmit" OnClick="BtnP2pSubmit_OnClick" Text="Submit" CssClass="btn btn-success" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <div id="divP2pWarningMsg" runat="server" visible="false" class="alert alert-danger" style="float: left; width: 100%;">
                                <strong>
                                    <asp:Label ID="LblP2pWarningMsg" runat="server" /></strong><asp:Label ID="LblP2pWarningMsgContent" runat="server" />
                            </div>
                            <div id="divP2pSuccessMsg" runat="server" visible="false" class="alert alert-success" style="float: left; width: 100%;">
                                <strong>
                                    <asp:Label ID="LblP2pSuccessMsg" runat="server" /></strong><asp:Label ID="LblP2pSuccessMsgContent" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Send message form (modal view) -->
    <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="BtnCloseModal" data-dismiss="modal" CssClass="close" aria-hidden="true" runat="server" />
                            <h3 id="myModalLabel">
                                <asp:Label ID="LblMessageHeader" runat="server" /></h3>
                        </div>
                        <div class="modal-body">
                            <form class="form-horizontal col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="LblMessageName" Text="Your company name" runat="server" /><asp:TextBox ID="TbxMessageName" CssClass="form-control" placeholder="Name" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblMessageEmail" Text="Your company email" runat="server" /><asp:TextBox ID="TbxMessageEmail" MaxLength="100" CssClass="form-control email" placeholder="E-mail" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                                <div id="divComEmail" runat="server" visible="false" class="form-group">
                                    <asp:Label ID="DdlMessageEmail" runat="server" /><asp:DropDownList ID="DdlCompanyMessageEmail" CssClass="form-control email" placeholder="E-mail" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblMessageSubject" Text="Opportunity name" runat="server" /><asp:TextBox ID="TbxMessageSubject" CssClass="form-control" MaxLength="30" placeholder="Subject" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblMessageContent" Text="Your message" runat="server" /><asp:TextBox ID="TbxMessageContent" CssClass="form-control" TextMode="MultiLine" MaxLength="2000" Rows="5" placeholder="Enter your message here" data-placement="top" data-trigger="manual" runat="server" />
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnMessageCancel" OnClick="BtnCancelMsg_OnClick" CssClass="btn" aria-hidden="true" runat="server" />
                            <asp:Button ID="BtnMessageSend" OnClick="BtnSend_OnClick" CssClass="btn btn-success" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <div id="divWarningMsg" runat="server" visible="false" class="alert alert-danger" style="float: left; width: 100%;">
                                <strong>
                                    <asp:Label ID="LblWarningMsg" runat="server" /></strong><asp:Label ID="LblWarningMsgContent" runat="server" />
                            </div>
                            <div id="divSuccessMsg" runat="server" visible="false" class="alert alert-success" style="float: left; width: 100%;">
                                <strong>
                                    <asp:Label ID="LblSuccessMsg" runat="server" /></strong><asp:Label ID="LblSuccessMsgContent" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Pop Up Invitation form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="LblFileUploadTitle" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <controls:MessageControl ID="UploadMessageAlert" Visible="false" runat="server" />
                                        <asp:Label ID="LblFileUploadfMsg" Visible="false" CssClass="control-label" runat="server" />
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

    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
        <script type="text/javascript">

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
            function OpenConfPopUpVideo() {
                $('#divVideoConfirm').modal('show');
            }

            function CloseConfPopUpVideo() {
                $('#divVideoConfirm').modal('hide');
            }

            function OpenMessagePopUp() {
                $('#myModal').modal('show');
            }

            function CloseMessagePopUp() {
                $('#myModal').modal('hide');
            }

            function OpenP2pPartnerPopUp() {
                $('#myP2pPartnerModal').modal('show');
            }

            function CloseP2pPartnerPopUp() {
                $('#myP2pPartnerModal').modal('hide');
            }
        </script>
    </telerik:RadScriptBlock>

</asp:Content>
