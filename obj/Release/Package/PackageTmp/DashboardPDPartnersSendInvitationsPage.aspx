<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardPDPartnersSendInvitationsPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardPDPartnersSendInvitationsPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl2Msgs.ascx" TagName="MessageControl2Msgs" TagPrefix="controls2" %>
<%@ Register Src="~/Controls/Collaboration/CreateNewInvitationToPartnersMessage.ascx" TagName="UcCreateNewInvitationToPartnersMessage" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePnl" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--begin::Card-->
                    <div class="card card-custom gutter-b">
                        <div class="row">
                            <div id="divPartnerSendNewInvitationTitleArea" runat="server" class="col-lg-6">
                                <div class="card-header">
                                    <div class="card-title">
                                        <h3 class="card-label">
                                            <asp:Label ID="LblTitleInvite" runat="server" />
                                        </h3>
                                    </div>
                                </div>
                            </div>

                            <div id="divPartnerPortalTitleArea" runat="server" visible="false" class="col-lg-6">
                                <div class="card-header">
                                    <div class="card-title">
                                        <h3 class="card-label">
                                            <asp:Label ID="Label2" Text="Share your partner portal sign up and login pages" runat="server" />
                                        </h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="col-lg-12">
                                <div class="row">
                                    <div style="width: 100%; text-align: center;">
                                        <div class="row">
                                            <div id="divPartnerSendNewInvitationArea" runat="server" class="col-lg-6">
                                                <p style="margin-bottom: 32px;">
                                                    <asp:Label ID="LblTitleInvite2" runat="server" />
                                                </p>
                                                <div id="divInvitationToPartnersBtnArea" runat="server" class="example-preview" style="min-height: 390px;">
                                                    <!-- Button trigger modal-->
                                                    <button id="aInvitationToPartners" style="margin-top: 50px;" runat="server" type="button" class="btn btn-primary" data-toggle="modal" data-target="#PartnersInvitationModal">
                                                        <asp:Label ID="LblSendNewInvitation" Text="Send Invitations" runat="server" />
                                                    </button>
                                                </div>
                                            </div>
                                            <div id="divPartnerPortalArea" runat="server" visible="false" class="col-lg-6">
                                                <p>
                                                    <asp:Label ID="LblTitleInvite3" Text="Add your branded login and sign up links to your website to help your current partners login or for new partners to sign up." runat="server" />
                                                </p>
                                                <div class="example">
                                                    <div class="example-preview">
                                                        <a id="aPartnerPortalLogin" runat="server" style="border: 1px solid #F3F6F9; cursor: default;" class="btn font-weight-bold py-2 px-6">

                                                            <span class="svg-icon svg-icon-primary svg-icon-2x">
                                                                <!--begin::Svg Icon | path:C:\wamp64\www\keenthemes\themes\metronic\theme\html\demo7\dist/../src/media/svg/icons\Navigation\Sign-in.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <rect x="0" y="0" width="24" height="24" />
                                                                        <rect fill="#000000" opacity="0.3" transform="translate(9.000000, 12.000000) rotate(-270.000000) translate(-9.000000, -12.000000) " x="8" y="6" width="2" height="12" rx="1" />
                                                                        <path d="M20,7.00607258 C19.4477153,7.00607258 19,6.55855153 19,6.00650634 C19,5.45446114 19.4477153,5.00694009 20,5.00694009 L21,5.00694009 C23.209139,5.00694009 25,6.7970243 25,9.00520507 L25,15.001735 C25,17.2099158 23.209139,19 21,19 L9,19 C6.790861,19 5,17.2099158 5,15.001735 L5,8.99826498 C5,6.7900842 6.790861,5 9,5 L10.0000048,5 C10.5522896,5 11.0000048,5.44752105 11.0000048,5.99956624 C11.0000048,6.55161144 10.5522896,6.99913249 10.0000048,6.99913249 L9,6.99913249 C7.8954305,6.99913249 7,7.89417459 7,8.99826498 L7,15.001735 C7,16.1058254 7.8954305,17.0008675 9,17.0008675 L21,17.0008675 C22.1045695,17.0008675 23,16.1058254 23,15.001735 L23,9.00520507 C23,7.90111468 22.1045695,7.00607258 21,7.00607258 L20,7.00607258 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" transform="translate(15.000000, 12.000000) rotate(-90.000000) translate(-15.000000, -12.000000) " />
                                                                        <path d="M16.7928932,9.79289322 C17.1834175,9.40236893 17.8165825,9.40236893 18.2071068,9.79289322 C18.5976311,10.1834175 18.5976311,10.8165825 18.2071068,11.2071068 L15.2071068,14.2071068 C14.8165825,14.5976311 14.1834175,14.5976311 13.7928932,14.2071068 L10.7928932,11.2071068 C10.4023689,10.8165825 10.4023689,10.1834175 10.7928932,9.79289322 C11.1834175,9.40236893 11.8165825,9.40236893 12.2071068,9.79289322 L14.5,12.0857864 L16.7928932,9.79289322 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.500000, 12.000000) rotate(-90.000000) translate(-14.500000, -12.000000) " />
                                                                    </g>
                                                                </svg><!--end::Svg Icon-->
                                                            </span>
                                                            <asp:Label ID="LblPartnerPortalLogin" Text="Partner portal login page" runat="server" />
                                                        </a>
                                                        <p style="font-size: 11px; margin-top: 10px;">
                                                            <div class="example-code">
                                                                <span class="example-copy" data-toggle="tooltip" title="" data-original-title="Copy code"></span>
                                                                <div class="example-highlight">
                                                                    <pre class=" language-html">
                                                                <code class=" language-html">
                                                                    <span class="token tag">
                                                                        <a id="aPartnerPortalLoginTextLink" runat="server" style="cursor: default;" onserverclick="aPartnerPortalLogin_ServerClick">
                                                                            <asp:Label ID="LblPartnerPortalLoginLink" runat="server" />
                                                                        </a>
                                                                    </span>
                                                                </code>
                                                            </pre>
                                                                </div>
                                                            </div>
                                                        </p>
                                                    </div>
                                                </div>
                                                <div class="pt-4"></div>
                                                <div class="example">
                                                    <div class="example-preview">
                                                        <a id="aPartnerPortalSignUp" runat="server" style="border: 1px solid #F3F6F9; cursor: default;" class="btn font-weight-bold py-2 px-6">

                                                            <span class="svg-icon svg-icon-primary svg-icon-2x">
                                                                <!--begin::Svg Icon | path:C:\wamp64\www\keenthemes\themes\metronic\theme\html\demo7\dist/../src/media/svg/icons\Communication\Add-user.svg-->
                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                        <polygon points="0 0 24 0 24 24 0 24" />
                                                                        <path d="M18,8 L16,8 C15.4477153,8 15,7.55228475 15,7 C15,6.44771525 15.4477153,6 16,6 L18,6 L18,4 C18,3.44771525 18.4477153,3 19,3 C19.5522847,3 20,3.44771525 20,4 L20,6 L22,6 C22.5522847,6 23,6.44771525 23,7 C23,7.55228475 22.5522847,8 22,8 L20,8 L20,10 C20,10.5522847 19.5522847,11 19,11 C18.4477153,11 18,10.5522847 18,10 L18,8 Z M9,11 C6.790861,11 5,9.209139 5,7 C5,4.790861 6.790861,3 9,3 C11.209139,3 13,4.790861 13,7 C13,9.209139 11.209139,11 9,11 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                        <path d="M0.00065168429,20.1992055 C0.388258525,15.4265159 4.26191235,13 8.98334134,13 C13.7712164,13 17.7048837,15.2931929 17.9979143,20.2 C18.0095879,20.3954741 17.9979143,21 17.2466999,21 C13.541124,21 8.03472472,21 0.727502227,21 C0.476712155,21 -0.0204617505,20.45918 0.00065168429,20.1992055 Z" fill="#000000" fill-rule="nonzero" />
                                                                    </g>
                                                                </svg><!--end::Svg Icon-->
                                                            </span>
                                                            <asp:Label ID="LblPartnerPortalSignUp" Text="Partner portal sign up page" runat="server" />
                                                        </a>
                                                        <p style="font-size: 11px; margin-top: 10px;">
                                                            <div class="example-code">
                                                                <span class="example-copy" data-toggle="tooltip" title="" data-original-title="Copy code"></span>
                                                                <div class="example-highlight">
                                                                    <pre class=" language-html">
                                                                <code class=" language-html">
                                                                    <span class="token tag">
                                                                        <a id="aPartnerPortalSignUpTextLink" runat="server" style="cursor: default;" onserverclick="aPartnerPortalLogin_ServerClick">
                                                                            <asp:Label ID="LblPartnerPortalSignUpLink" runat="server" />
                                                                        </a>
                                                                    </span>
                                                                </code>
                                                            </pre>
                                                                </div>
                                                            </div>
                                                        </p>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <controls2:MessageControl2Msgs ID="UcMessageInfo" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end::Card-->
                    <!--begin::Card-->
                    <div id="divConnectionInvitations" runat="server" visible="false" class="card card-custom">
                        <div class="card-header flex-wrap py-5">
                            <div class="card-title">
                                <h3 class="card-label">INVITATIONS TO MY CHANNEL PARTNERS										
                                </h3>
                            </div>
                            <div class="card-toolbar">
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="mb-7">
                                <div id="divSearchAreaConfirmed" runat="server" class="row align-items-center">

                                    <div class="col-lg-3 col-xl-3">
                                        <div class="row align-items-center">
                                            <div class="col-md-12 my-2 my-md-0">
                                                <div class="input-icon">
                                                    <asp:TextBox ID="RtbxCompanyNameEmail" placeholder="Name/Email" CssClass="form-control" runat="server" />
                                                    <span>
                                                        <i class="flaticon2-search-1 text-muted"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-xl-4 mt-5 mt-lg-0">
                                        <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                                    </div>
                                    <div class="col-lg-5 col-xl-5">
                                        <div class="row align-items-center">
                                            <div class="col-md-12 my-2 my-md-0">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--begin: Datatable-->
                            <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                <telerik:RadGrid ID="RdgResellers" Visible="false" AllowPaging="true" AllowSorting="false" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" PagerStyle-Position="Bottom" PageSize="10"
                                    CssClass="table table-separate table-head-custom table-checkable" AutoGenerateColumns="false" runat="server">
                                    <MasterTableView>
                                        <NoRecordsTemplate>
                                            <div class="emptyGridHolder">
                                                <asp:Literal ID="LtlNoDataFound" runat="server" />
                                            </div>
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <telerik:GridTemplateColumn Display="false" HeaderStyle-Width="10" HeaderText="Select" DataField="select" UniqueName="select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CbxSelectUser" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderStyle-Width="120" HeaderText="Company">
                                                <ItemTemplate>
                                                    <a id="aCompanyLogo" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                        <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                                            <span class="symbol-label">
                                                                <asp:Image ID="ImgCompanyLogo" runat="server" class="h-50 align-self-center" alt="" />
                                                            </span>
                                                        </div>
                                                        <asp:Label ID="LblCompanyName" Text='<%# DataBinder.Eval(Container.DataItem, "company_name")%>' runat="server" />
                                                    </a>
                                                    <div id="divNotification" runat="server" visible="false" class="text-right" style="display: none;">
                                                        <span id="spanNotificationMsg" class="label label-lg label-light-danger label-inline" title="New unread message" runat="server">
                                                            <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                                        </span>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn Display="false" HeaderStyle-Width="170" HeaderText="Company Name" DataField="company_name" UniqueName="company_name" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="170" HeaderText="Website" DataField="website" UniqueName="website" />
                                            <telerik:GridBoundColumn Display="false" HeaderStyle-Width="150" HeaderText="Email" DataField="email" UniqueName="email" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Country" DataField="country" UniqueName="country" />
                                            <telerik:GridTemplateColumn Display="false" HeaderStyle-Width="50" HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblStatus" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn Display="false" DataField="user_application_type" UniqueName="user_application_type" />
                                            <telerik:GridBoundColumn Display="false" DataField="company_logo" UniqueName="company_logo" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Id" Display="false" DataField="id" UniqueName="id" />
                                            <telerik:GridTemplateColumn HeaderStyle-Width="50" HeaderText="Actions">
                                                <ItemTemplate>
                                                    <a id="aSendInvitation" onserverclick="aSendInvitation_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
                                                        <span class="svg-icon svg-icon-md svg-icon-primary">
                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Write.svg-->
                                                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                    <rect x="0" y="0" width="24" height="24" />
                                                                    <path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953)" />
                                                                    <path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                </g>
                                                            </svg>
                                                            <!--end::Svg Icon-->
                                                        </span>
                                                    </a>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <!--end: Datatable-->
                            <controls:MessageControl ID="UcMessageAlert" runat="server" />

                            <div class="row">
                                <div class="col-md-12">
                                    <a id="aInvitationToConnections" visible="false" runat="server"
                                        data-toggle="modal" role="button" class="btn btn-primary"><i class="ion-plus-round mr-5"></i>
                                        <asp:Label ID="LblInvitationToConnections" Text="Invite your Connections" runat="server" />
                                    </a>
                                    <asp:Label ID="LblOrText" runat="server" />
                                </div>
                            </div>
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

    <!-- Invitation to Vendor Partners form (modal view) -->
    <div class="modal fade" id="PartnersInvitationModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <controls:UcCreateNewInvitationToPartnersMessage ID="UcCreateNewInvitationToPartnersMessage" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up Invitation form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblInvitationSendTitle" Text="Invitation Send" CssClass="control-label" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="LblSuccessfullSendfMsg" runat="server" />
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

            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }

            function OpenPartnersInvitationPopUp() {
                $('#PartnersInvitationModal').modal('show');
            }

            function ClosePartnersInvitationPopUp() {
                $('#PartnersInvitationModal').modal('hide');
            }

        </script>

    </telerik:RadScriptBlock>
</asp:Content>
