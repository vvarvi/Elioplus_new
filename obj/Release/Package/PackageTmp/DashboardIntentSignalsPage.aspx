<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardIntentSignalsPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardIntentSignalsPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>
<%@ Register Src="/Controls/Payment/Stripe_Leads_Service_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xl-5">
                            <!--begin::Card-->
                            <div class="card card-custom gutter-b card-stretch">
                                <!--begin::Body-->
                                <div class="card-body">
                                    <!--begin::Section-->
                                    <div class="row mb-7" style="width: 100%;">
                                        <div class="row align-items-center">
                                            <div class="col-md-6 my-2 my-md-0">
                                                <div class="d-flex align-items-center">
                                                    <asp:DropDownList ID="DdlDates" AutoPostBack="true" OnSelectedIndexChanged="DdlDates_SelectedIndexChanged" Width="300" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="30" Text="Last 30 days" />
                                                        <asp:ListItem Value="0" Text="Today" />
                                                        <asp:ListItem Value="1" Text="Yesterday" />
                                                        <asp:ListItem Value="7" Text="Last 7 days" />
                                                        <asp:ListItem Value="14" Text="Last 14 days" />
                                                        <asp:ListItem Value="-1" Text="All time" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="d-flex align-items-center">
                                        <div class="row" style="width: 100%;">
                                            <ul class="nav nav-pills nav-fill">
                                                <li class="nav-item">
                                                    <a id="aAll" runat="server" onserverclick="aAll_ServerClick" class="nav-link active" href="#">All Leads</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a id="aSnitcher" runat="server" onserverclick="aSnitcher_ServerClick" class="nav-link" href="#">Intent Data</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a id="aElio" runat="server" onserverclick="aElio_ServerClick" class="nav-link" href="#">RFQs</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a id="aMessages" runat="server" onserverclick="aMessages_ServerClick" class="nav-link" href="#">Messages</a>
                                                </li>
                                            </ul>
                                        </div>

                                        <!--begin::Toolbar-->
                                        <div class="card-toolbar mb-auto">
                                            <div id="divExportLeadsActions" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Quick actions" data-placement="left">
                                                <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <i class="ki ki-bold-more-hor"></i>
                                                </a>
                                                <div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">
                                                    <!--begin::Navigation-->
                                                    <ul class="navi navi-hover">
                                                        <li class="navi-header pb-1">
                                                            <span class="text-primary text-uppercase font-weight-bold font-size-sm">Export to:</span>
                                                        </li>
                                                        <li class="navi-item">
                                                            <a id="aBtnLeadsExportCsv" role="button" onserverclick="aBtnLeadsExportCsv_ServerClick" runat="server" class="navi-link">
                                                                <span class="navi-icon">
                                                                    <i class="la la-file-text-o"></i>
                                                                </span>
                                                                <span class="navi-text">
                                                                    <asp:Label ID="Label1" Text="CSV" runat="server" />
                                                                </span>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                    <!--end::Navigation-->
                                                </div>
                                            </div>
                                        </div>
                                        <!--end::Toolbar-->
                                    </div>
                                    <!--end::Section-->
                                    <!--begin::Content-->
                                    <div id="divContent" runat="server" class="d-flex flex-wrap mt-10" style="overflow-y: scroll; max-height: 580px;">
                                        <div class="tab-content mt-5" style="width: 100%;">
                                            <div class="tab-pane fade active show" id="tab_1" runat="server" role="tabpanel" aria-labelledby="all-tab">
                                                <div class="d-flex flex-wrap">
                                                    <asp:Repeater ID="RdgResults" OnLoad="RdgResults_Load" OnItemDataBound="RdgResults_OnItemDataBound" runat="server">
                                                        <ItemTemplate>
                                                            <div class="d-flex align-items-center mb-4" style="width: 100%;">
                                                                <!--begin::Pic-->
                                                                <div id="divImg" runat="server" class="flex-shrink-0 mr-4 symbol symbol-50 symbol-circle">
                                                                    <asp:Image ID="CompanyImage" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "lead_company_logo")%>' AlternateText="company image" />
                                                                </div>
                                                                <div id="divNoImg" runat="server" visible="false" class="flex-shrink-0 mr-4 symbol symbol-50 symbol-circle symbol-primary">
                                                                    <span class="symbol-label font-size-h3 font-weight-boldest">NO</span>
                                                                </div>
                                                                <!--end::Pic-->
                                                                <!--begin::Info-->
                                                                <div class="d-flex flex-column mr-auto">
                                                                    <!--begin: Title-->
                                                                    <a id="aCompanyName" role="button" runat="server" onserverclick="aCompanyName_ServerClick" class="card-title text-hover-primary font-weight-bolder font-size-h6 text-dark mb-1">
                                                                        <%# DataBinder.Eval(Container.DataItem, "lead_company_name")%>
                                                                    </a>
                                                                    <span class="text-muted font-weight-bold">
                                                                        <%# DataBinder.Eval(Container.DataItem, "lead_last_seen")%>
                                                                    </span>
                                                                    <!--end::Title-->
                                                                </div>
                                                                <!--end::Info-->
                                                                <asp:HiddenField ID="HdnId" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' runat="server" />
                                                                <asp:HiddenField ID="HdnLeadId" Value='<%# DataBinder.Eval(Container.DataItem, "lead_id")%>' runat="server" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="width: 100%;">
                                        <controls:MessageAlertControl ID="UcMessageAlertTop" runat="server" />
                                    </div>
                                    <!--end::Content-->

                                    <!--begin::Blog-->
                                    <div class="d-flex flex-wrap">
                                    </div>
                                    <!--end::Blog-->
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Card-->
                        </div>
                        <div class="col-xl-7">
                            <!--begin::Card-->
                            <div class="card card-custom gutter-b card-stretch">
                                <!--begin::Body-->
                                <div id="divFreeBanner" runat="server" visible="false" class="card-body">
                                    <div class="row">
                                        <div class="col-xl-12">
                                            <!--begin::Engage Widget 1-->
                                            <div class="card card-custom card-stretch gutter-b">
                                                <div class="card-body d-flex p-0">
                                                    <div class="flex-grow-1 p-8 card-rounded bgi-no-repeat d-flex align-items-center" style="background-color: #FFF4DE; background-position: left bottom; background-size: auto 100%; background-image: url(/assets/media/svg/humans/custom-2.svg)">
                                                        <div class="row">
                                                            <div class="col-12 col-xl-5"></div>
                                                            <div id="divFree" runat="server" class="col-12 col-xl-7">
                                                                <h4 class="text-danger font-weight-bolder">
                                                                    <asp:Label ID="LblTalkUsTitle" Text="Talk to Us" runat="server" />
                                                                </h4>
                                                                <p class="text-dark-50 my-5 font-size-xl font-weight-bold">Get in touch and start receiving leads and RFQs for your products and expertise from local businesses near you.</p>
                                                                <a id="aContactUs" runat="server" class="btn btn-danger font-weight-bold py-2 px-6">Contact Us</a>
                                                            </div>
                                                            <div id="divUpgrade" runat="server" visible="false" class="col-12 col-xl-7">
                                                                <p class="text-dark-50 my-5 font-size-xl font-weight-bold">Get in touch and start receiving leads and RFQs for your products and expertise from local businesses near you.</p>
                                                                <a id="aBtnGoPremium" runat="server" style="width: 90%;" data-target="#PaymentModal" type="button" data-toggle="modal" class="btn btn-primary font-weight-bold py-2 px-6">
                                                                    <span class="svg-icon svg-icon-md">
                                                                        <!--begin::Svg Icon | path:assets/media/svg/icons/Design/Flatten.svg-->
                                                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                <rect x="0" y="0" width="24" height="24"></rect>
                                                                                <circle fill="#000000" cx="9" cy="15" r="6"></circle>
                                                                                <path d="M8.8012943,7.00241953 C9.83837775,5.20768121 11.7781543,4 14,4 C17.3137085,4 20,6.6862915 20,10 C20,12.2218457 18.7923188,14.1616223 16.9975805,15.1987057 C16.9991904,15.1326658 17,15.0664274 17,15 C17,10.581722 13.418278,7 9,7 C8.93357256,7 8.86733422,7.00080962 8.8012943,7.00241953 Z" fill="#000000" opacity="0.3"></path>
                                                                            </g>
                                                                        </svg>
                                                                        <!--end::Svg Icon-->
                                                                    </span>
                                                                    <asp:Label ID="LblBtnGoPremium" Text="Upgrade now" runat="server" />
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end::Engage Widget 1-->
                                        </div>
                                    </div>
                                </div>
                                <div id="divLeadBody" runat="server" class="card-body">
                                    <!--begin::Section-->
                                    <div class="d-flex align-items-center">
                                        <!--begin::Pic-->
                                        <div class="flex-shrink-0 mr-4 symbol symbol-65 symbol-circle">
                                            <img id="ImgCompanyLogo" runat="server" src="/assets/media/project-logos/4.png" alt="image" />
                                        </div>
                                        <!--end::Pic-->
                                        <!--begin::Info-->
                                        <div class="d-flex flex-column mr-auto">
                                            <!--begin: Title-->
                                            <a href="#" class="card-title text-hover-primary font-weight-bolder font-size-h5 text-dark mb-1">
                                                <asp:Label ID="LblCompanyName" runat="server" />
                                            </a>
                                            <span class="text-muted font-weight-bold">
                                                <asp:Label ID="LblCompanyIndustry" runat="server" />
                                            </span>
                                            <!--end::Title-->
                                        </div>
                                        <!--end::Info-->

                                        <!--begin::Toolbar-->
                                        <div class="card-toolbar mb-auto">
                                            <div id="divExportCsvActions" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Quick actions" data-placement="left">
                                                <a href="#" class="btn btn-clean btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <i class="ki ki-bold-more-hor"></i>
                                                </a>
                                                <div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">
                                                    <!--begin::Navigation-->
                                                    <ul class="navi navi-hover">
                                                        <li class="navi-header pb-1">
                                                            <span class="text-primary text-uppercase font-weight-bold font-size-sm">Export to:</span>
                                                        </li>
                                                        <li class="navi-item">
                                                            <a id="aBtnExportCsv" role="button" onserverclick="aBtnExportCsv_ServerClick" runat="server" class="navi-link">
                                                                <span class="navi-icon">
                                                                    <i class="la la-file-text-o"></i>
                                                                </span>
                                                                <span class="navi-text">
                                                                    <asp:Label ID="LblBtnExportCsv" Text="CSV" runat="server" />
                                                                </span>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                    <!--end::Navigation-->
                                                </div>
                                            </div>
                                        </div>
                                        <!--end::Toolbar-->
                                    </div>
                                    <div class="row">
                                        <div class="d-flex flex-column flex-lg-fill float-left mb-7 mt-4">
                                            <div class="symbol-group symbol-hover">
                                                <div class="symbol symbol-30 symbol-circle" style="margin-left: 0px;" data-toggle="tooltip" title="linkedin">
                                                    <a id="aLinkendin" runat="server">
                                                        <i class="socicon-linkedin text-primary mr-5"></i>
                                                    </a>
                                                </div>
                                                <div class="symbol symbol-30 symbol-circle" style="margin-left: 0px;" data-toggle="tooltip" title="facebook">
                                                    <a id="aFacebook" runat="server">
                                                        <i class="socicon-facebook text-light-primary mr-5"></i>
                                                    </a>
                                                </div>
                                                <div class="symbol symbol-30 symbol-circle" style="margin-left: 0px;" data-toggle="tooltip" title="youtube">
                                                    <a id="aYoutube" runat="server">
                                                        <i class="socicon-youtube text-danger mr-5"></i>
                                                    </a>
                                                </div>
                                                <div class="symbol symbol-30 symbol-circle" style="margin-left: 0px;" data-toggle="tooltip" title="instagram">
                                                    <a id="aInstagram" runat="server">
                                                        <i class="socicon-instagram text-default mr-5"></i>
                                                    </a>
                                                </div>
                                                <div class="symbol symbol-30 symbol-circle" style="margin-left: 0px;" data-toggle="tooltip" title="twitter">
                                                    <a id="aTwitter" runat="server">
                                                        <i class="socicon-twitter text-primary mr-5"></i>
                                                    </a>
                                                </div>
                                                <div class="symbol symbol-30 symbol-circle" style="margin-left: 0px;" data-toggle="tooltip" title="pinterest">
                                                    <a id="aPinterest" runat="server">
                                                        <i class="socicon-pinterest text-light-danger mr-5"></i>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-5">
                                            <div class="py-9">
                                                <h5 class="font-weight-bolder font-size-h5 text-dark-75 text-hover-primary">Company Details</h5>
                                                <div class="d-flex align-items-center justify-content-between mb-2">
                                                    <span class="font-weight-bold mr-2">Website:</span>
                                                    <a id="aWebsite" runat="server" class="text-muted text-hover-primary">
                                                        <asp:Label ID="LblWebsite" runat="server" />
                                                    </a>
                                                </div>
                                                <div class="d-flex align-items-center justify-content-between mb-2">
                                                    <span class="font-weight-bold mr-2">Email:</span>
                                                    <a href="#" class="text-muted text-hover-primary">
                                                        <asp:Label ID="LblEmail" runat="server" />
                                                    </a>
                                                </div>
                                                <div class="d-flex align-items-center justify-content-between mb-2">
                                                    <span class="font-weight-bold mr-2">Phone:</span>
                                                    <span class="text-muted">
                                                        <asp:Label ID="LblPhone" runat="server" />
                                                    </span>
                                                </div>
                                                <div class="d-flex align-items-center justify-content-between">
                                                    <span class="font-weight-bold mr-2">Address:</span>
                                                    <span class="text-muted">
                                                        <asp:Label ID="LblAddress" runat="server" />
                                                        <telerik:RadToolTip ID="RttAddress" runat="server" TargetControlID="LblAddress" RenderMode="Classic" Animation="Fade" />
                                                    </span>
                                                </div>
                                                <div class="d-flex align-items-center justify-content-between">
                                                    <span class="font-weight-bold mr-2">Founded year:</span>
                                                    <span class="text-muted">
                                                        <asp:Label ID="LblFounded" runat="server" />
                                                    </span>
                                                </div>
                                                <div class="d-flex align-items-center justify-content-between">
                                                    <span class="font-weight-bold mr-2">Employees:</span>
                                                    <span class="text-muted">
                                                        <asp:Label ID="LblEmployees" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-7">
                                            <div class="d-flex flex-wrap mt-8 ml-8">
                                                <div id="divApiData" runat="server">
                                                    <div class="d-flex flex-wrap">
                                                        <!--begin: Item-->
                                                        <div class="row" style="width: 100%;">
                                                            <div class="mr-12 d-flex flex-column mb-8 ml-4">
                                                                <h4>
                                                                    <span class="font-weight-bolder mb-4">Category / Technology</span>
                                                                </h4>
                                                                <span class="font-size-10 pt-1 mt-2">
                                                                    <asp:PlaceHolder ID="PhApiProducts" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <!--end::Item-->
                                                        <!--begin::Item-->
                                                        <div class="row" style="width: 100%;">
                                                            <div class="mr-12 d-flex flex-column mb-7 ml-4">
                                                                <h4>
                                                                    <span class="font-weight-bolder mb-4">Contacts</span>
                                                                </h4>
                                                                <div class="mt-4">
                                                                    <asp:Repeater ID="RdgContacts" OnLoad="RdgContacts_Load" runat="server">
                                                                        <ItemTemplate>
                                                                            <div class="row" style="width: 100%; padding-left: 20px;">
                                                                                <div class="d-flex align-items-center justify-content-between mb-2">
                                                                                    <span class="font-weight-bold mr-2">Email:</span>
                                                                                    <a href="#" class="text-muted text-hover-primary"><%# DataBinder.Eval(Container.DataItem, "lead_company_contacts")%>
                                                                                    </a>
                                                                                </div>
                                                                            </div>

                                                                            <asp:HiddenField ID="HdnId" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' runat="server" />
                                                                            <asp:HiddenField ID="HdnLeadId" Value='<%# DataBinder.Eval(Container.DataItem, "lead_id")%>' runat="server" />

                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <div class="row" style="width: 100%;">
                                                                        <controls:MessageAlertControl ID="UcMessageContactsAlertControl" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end::Item-->
                                                    </div>
                                                </div>
                                                <div id="divRFPsData" visible="false" runat="server">
                                                    <div class="d-flex flex-wrap">
                                                        <!--begin: Item-->
                                                        <div class="row" style="width: 100%;">
                                                            <div class="mr-2 d-flex flex-column mb-8 ml-4" style="width: 100%;">
                                                                <h4>
                                                                    <span class="font-weight-bolder mb-4">Personal Info</span>
                                                                </h4>
                                                                <div class="d-flex mb-2">
                                                                    <span class="font-weight-bold mr-2">First name:</span>
                                                                    <a id="a1" runat="server" class="text-muted text-hover-primary">
                                                                        <asp:Label ID="LblFirstName" runat="server" />
                                                                    </a>
                                                                </div>
                                                                <div class="d-flex mb-2">
                                                                    <span class="font-weight-bold mr-2">Last name:</span>
                                                                    <a href="#" class="text-muted text-hover-primary">
                                                                        <asp:Label ID="LblLastName" runat="server" />
                                                                    </a>
                                                                </div>
                                                                <div class="d-flex mb-2">
                                                                    <span class="font-weight-bold mr-2">Email:</span>
                                                                    <span class="text-muted">
                                                                        <asp:Label ID="LblPersonalEmail" runat="server" />
                                                                    </span>
                                                                </div>
                                                                <div class="d-flex mb-2">
                                                                    <span class="font-weight-bold mr-2">Personal Phone:</span>
                                                                    <span class="text-muted">
                                                                        <asp:Label ID="LblPhoneNumber" runat="server" />
                                                                    </span>
                                                                </div>
                                                                <div class="d-flex mb-2">
                                                                    <span class="font-weight-bold mr-2">Company name:</span>
                                                                    <span class="text-muted">
                                                                        <asp:Label ID="LblBusinessName" runat="server" />
                                                                    </span>
                                                                </div>
                                                                <div class="d-flex mb-2">
                                                                    <span class="font-weight-bold mr-2">Country:</span>
                                                                    <span class="text-muted">
                                                                        <asp:Label ID="LblPersonalCountry" runat="server" />
                                                                    </span>
                                                                </div>
                                                                <div class="d-fle mb-2">
                                                                    <span class="font-weight-bold mr-2">City:</span>
                                                                    <span class="text-muted">
                                                                        <asp:Label ID="LblPersonalCity" runat="server" />
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end::Item-->
                                                        <!--begin: Item-->
                                                        <div class="row" style="width: 100%;">
                                                            <div class="mr-12 d-flex flex-column mb-8 ml-4">
                                                                <h4>
                                                                    <span class="font-weight-bolder mb-4">Category / Technology</span>
                                                                </h4>
                                                                <span class="font-size-10 pt-1 mt-2">
                                                                    <asp:PlaceHolder ID="PhRFPsProducts" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <!--end::Item-->
                                                        <!--begin::Item-->
                                                        <div class="row" style="width: 100%;">
                                                            <div class="mr-12 d-flex flex-column mb-8 ml-4">
                                                                <h4>
                                                                    <span class="font-weight-bolder mb-4">Users / Units</span>
                                                                </h4>
                                                                <span class="font-size-10 pt-1 mt-2">
                                                                    <asp:Label ID="LblUsersNumber" Text="0 users" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <!--end::Item-->
                                                        <!--begin::Item-->
                                                        <div class="row" style="width: 100%;">
                                                            <div class="mr-12 d-flex flex-column mb-8 ml-4">
                                                                <h4>
                                                                    <span class="font-weight-bolder mb-4">Message</span>
                                                                </h4>
                                                                <span class="font-size-10 pt-1 mt-2">
                                                                    <asp:TextBox ID="TbxMessage" BorderColor="White" BorderWidth="0" TextMode="MultiLine" Rows="10" Width="400" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <!--end::Item-->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Card-->
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <!-- Payment form (modal view) -->
    <div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <controls:UcStripe ID="UcStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">

        </script>
    </telerik:RadScriptBlock>

</asp:Content>
