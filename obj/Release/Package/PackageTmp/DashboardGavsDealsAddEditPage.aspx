<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardGavsDealsAddEditPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardGavsDealsAddEditPage" %>

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
                        <div id="divPartnerHeaderInfo" runat="server" class="card-header">
                            <h3 class="card-title">
                                <asp:Label ID="LblPartnerHeaderInfo" Text="Step 1: Select your Partner" runat="server" />
                            </h3>
                            <div class="card-toolbar">
                            </div>
                        </div>
                        <div id="divVendorsList" visible="false" runat="server" class="card-body">
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Select Partner:</label>
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-map-pin"></i></span></div>
                                        <asp:DropDownList AutoPostBack="true" ID="DrpPartners" OnSelectedIndexChanged="DrpPartners_SelectedIndexChanged" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                        </div>

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

                        <div class="card-header" style="margin-bottom: 0px;">
                            <h3 class="card-title">
                                <asp:Label ID="LblDealInfo" Text="Deal Status Information" runat="server" />
                            </h3>
                            <div class="card-toolbar">
                            </div>
                        </div>

                        <div class="card-body">
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Open/Expired Status:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DdlDealStatus" Enabled="false" Width="255" Height="36" runat="server">
                                        </asp:DropDownList>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Won/Lost Status:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DdlDealResult" Enabled="false" Width="255" Height="36" runat="server">
                                        </asp:DropDownList>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Approved/Rejected Status:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DdlIsActive" Enabled="false" Width="255" Height="36" runat="server">
                                        </asp:DropDownList>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-map-marker"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Month Duration:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DdlMonthDuration" Width="255" Height="36" runat="server">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TbxMonthDuration" Width="100" Visible="false" ReadOnly="true" placeholder="Deal's month duration" CssClass="form-control" MaxLength="45" runat="server" />
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                        </div>

                        <div class="card-header">
                            <h3 class="card-title">
                                <asp:Label ID="LblPersonalInfo" Text="Step 2: Fill in Details" runat="server" />
                            </h3>
                            <div class="card-toolbar">
                            </div>
                        </div>

                        <div class="card-body">
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Full Company Name:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxFullName" placeholder="Full Company Name" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Company URL:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxCompanyUrl" placeholder="Company URL" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Divisional Name:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxCompanyDivName" placeholder="Divisional Name" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Divisional Street Address:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxStreetAddress" placeholder="Divisional Street Address" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Divisional City:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxCompanyCity" placeholder="Divisional City" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Divisional Country:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxCompanyCountry" placeholder="Divisional Country" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">First Name:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxCompanyFirstName" placeholder="Customer Prospect’s First Name" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Last Name:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxCompanyLastName" placeholder="Customer Prospect’s Last Name" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Job Title:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxCompanyJobTitle" placeholder="Customer's Company Name" CssClass="form-control" MaxLength="95" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Phone Number:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxCompanyPhoneNumber" placeholder="Customer Prospect’s Phone Number" CssClass="form-control" MaxLength="145" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Divisional Postal Code:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxCompanyPostalCode" placeholder="Divisional Postal Code" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">E-mail Address:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxCompanyEmailAddress" placeholder="Customer Prospect’s E-mail Address" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Total Sales Value:</label>
                                <div class="col-lg-1" style="padding-right: 0px;">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxTotalSalesValue" placeholder="Projected Total Sales Value" CssClass="form-control" MaxLength="9" runat="server"></asp:TextBox>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <div id="divCurrencyArea" runat="server" class="col-lg-2" style="margin-left: 0px; padding-left: 20px;">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DrpCurrency" Width="55" Height="36" runat="server" CssClass="form-control" />
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Quarter/Year Close Date:</label>
                                <div class="col-lg-3">
                                    <telerik:RadDatePicker ID="RdpExpectedClosedDate" DateFormat="MM/DD/YYYY" Width="260" runat="server" />
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Solutions Proposed:</label>
                                <div class="col-lg-3" style="padding-right: 0px;">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxSolutionsProposed" placeholder="Competitive Solutions Proposed" CssClass="form-control" MaxLength="9" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Opportunity Description:</label>
                                <div class="col-lg-8">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxCompanyOpportunityDescription" placeholder="Customer's Company Description" TextMode="MultiLine" Rows="5" CssClass="form-control form-control-lg form-control" MaxLength="500" runat="server" />
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-bookmark-o"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="row" style="padding:15px;">
                                <asp:CheckBox ID="CbxTermsConditions" AutoPostBack="true" Text="I accept terms and conditions below about the deal" OnCheckedChanged="CbxTermsConditions_CheckedChanged" runat="server" />
                                <asp:Label ID="LblTerms1" runat="server"  />
                            </div>
                            <div class="row">                                
                                <div class="col-lg-6">
                                    <div id="divVendorActionsA" visible="false" runat="server">
                                        <asp:Button ID="BtnBackVendor" Visible="false" OnClick="BtnBack_OnClick" Text="Back to Leads" CssClass="btn btn-secondary mr-2" runat="server" />
                                        <asp:Button ID="BtnReject" OnClick="BtnReject_OnClick" Text="Reject" CssClass="btn btn-danger mr-2" runat="server" />
                                        <asp:Button ID="BtnApprove" OnClick="BtnApprove_OnClick" Text="Approve" CssClass="btn btn-success mr-2" runat="server" />
                                        <asp:Button ID="RBtnSendToCrmVendor" Visible="false" OnClick="BtnGetLeads_Click" Text="Send lead to CRM" CssClass="btn btn-primary" runat="server" />
                                        <asp:Button ID="RBtnCheckCrmLead" Visible="false" OnClick="BtnCheckCrmLead_Click" Text="Check lead to CRM" CssClass="btn btn-light-primary" runat="server" />
                                    </div>

                                    <div id="divResellerActionsA" runat="server">
                                        <asp:Button ID="BtnBack" Visible="false" OnClick="BtnBack_OnClick" Text="Back to Deals" CssClass="btn btn-secondary mr-2" runat="server" />
                                        <asp:Button ID="BtnClear" OnClick="BtnClear_OnClick" Text="Clear" CssClass="btn btn-light-primary mr-2" runat="server" />
                                        <asp:Button ID="BtnSave" OnClick="BtnSave_OnClick" Enabled="false" Text="Send" CssClass="btn btn-success mr-2" runat="server" />
                                        <asp:Button ID="BtnSendToCrm" Visible="false" OnClick="BtnGetLeads_Click" Text="Send lead to CRM" CssClass="btn blue-primary mr-2" runat="server" />
                                    </div>
                                </div>
                                <div id="divCrmPullText" runat="server" visible="false" class="col-lg-4 text-right hidden">
                                    <h3>
                                        <asp:Label ID="LblCrmInfo" Text="OR Pull data from CRM" runat="server" /></h3>
                                </div>
                                <div id="divCrmPullBtn" runat="server" visible="false" class="col-lg-2 text-right hidden">
                                    <asp:Button ID="BtnGetLeads" OnClick="BtnGetLeads_Click" Text="Pull Data" CssClass="btn btn-danger" runat="server" />
                                    <a id="aGoToIntegrations" runat="server" visible="false">
                                        <asp:Label ID="LblGoToIntegrations" CssClass="btn btn-danger" Text="Integration with CRM" runat="server" />
                                    </a>
                                </div>
                            </div>
                            <div class="row">
                                <div class="p-10">
                                    <ul style="padding-top:10px; font-style:italic;">
                                        <li>
                                            <asp:Label ID="LblLi0" runat="server" Text="Deal Registrations are valid for 90 days from the approval date." />
                                        </li>
                                        <li>
                                            <asp:Label ID="LblLi1" runat="server" Text="Requests for extensions beyond 90 days are at GAV’s exclusive discretion and must be approved in writing." />
                                        </li>
                                        <li>
                                            <asp:Label ID="LblLi2" runat="server" Text="Partners may only register opportunities in territories where they are authorized to operate. If an exception is made, partner will be notified in writing by GAVS of such approval." />
                                        </li>
                                        <li>
                                            <asp:Label ID="LblLi3" runat="server" Text="Partners will be required to introduce a GAVS sales executive to the prospective customer within 30 days of the registration approval date." />
                                        </li>
                                        <li>
                                            <asp:Label ID="LblLi4" runat="server" Text="GAVS requires the partner to exclusively present ZIF as the only AIOps product for each specific registered opportunity. If it is discovered that competing products to ZIF are being presented by the partner to the customer, GAVS may, at its discretion, revoke the registration approval, and by extension, any programmatic product discounts." />
                                        </li>
                                        <li>
                                            <asp:Label ID="LblLi5" runat="server" Text="Exclusive registrations for RFPs will be granted to the partner only if the partner is specifically responsible for the creation of the RFP and was instrumental in the introduction of ZIF to the customer.  If an opportunity is registered in response to a generic RFP where the partner was not instrumental in introducing ZIF to the customer, GAVS reserves the right to grant similar pricing to additional partners." />
                                        </li>
                                        <li>
                                            <asp:Label ID="LblLi6" runat="server" Text="GAVS reserves the right to market and sell to any potential customer whose registration has been disqualified, lost, or revoked due to noncompliance with the terms and conditions of the registration program as described herein." />
                                        </li>
                                    </ul>
                                </div>
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

    <!-- Pop Up Invitation form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblMessageTitle" CssClass="control-label" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group" style="width: 100%;">
                                    <controls:MessageControl ID="UcMessageAlert" runat="server" />
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

            function OpenLeadsServiceModal() {
                $('#LeadsServiceModal').modal('show');
            }

            function CloseLeadsServiceModal() {
                $('#LeadsServiceModal').modal('hide');
            }
        </script>

    </telerik:RadScriptBlock>

</asp:Content>
