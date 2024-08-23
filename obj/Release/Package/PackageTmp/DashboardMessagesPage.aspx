<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardMessagesPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardMessagesPage" %>

<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <div class="d-flex flex-row">
                <!--begin::Aside-->
                <div class="flex-row-auto offcanvas-mobile w-200px w-xxl-275px" id="kt_inbox_aside">
                    <!--begin::Card-->
                    <div class="card card-custom card-stretch">
                        <!--begin::Body-->
                        <div class="card-body px-5">
                            <!--begin::Compose-->
                            <div class="px-4 mt-4 mb-10">
                                <a id="aCompose" onserverclick="Compose_OnClick" runat="server" href="#" class="btn btn-block btn-primary font-weight-bold text-uppercase py-4 px-6 text-center" data-toggle="modal" data-target="#kt_inbox_compose">New Message</a>
                            </div>
                            <!--end::Compose-->
                            <!--begin::Navigations-->
                            <div class="navi navi-hover navi-active navi-link-rounded navi-bold navi-icon-center navi-light-icon">
                                <!--begin::Item-->
                                <div class="navi-item my-2">
                                    <a id="aInbox" runat="server" class="navi-link active">
                                        <span class="navi-icon mr-4">
                                            <span class="svg-icon svg-icon-lg">
                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Mail-heart.svg-->
                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                        <rect x="0" y="0" width="24" height="24" />
                                                        <path d="M6,2 L18,2 C18.5522847,2 19,2.44771525 19,3 L19,13 C19,13.5522847 18.5522847,14 18,14 L6,14 C5.44771525,14 5,13.5522847 5,13 L5,3 C5,2.44771525 5.44771525,2 6,2 Z M13.8,4 C13.1562,4 12.4033,4.72985286 12,5.2 C11.5967,4.72985286 10.8438,4 10.2,4 C9.0604,4 8.4,4.88887193 8.4,6.02016349 C8.4,7.27338783 9.6,8.6 12,10 C14.4,8.6 15.6,7.3 15.6,6.1 C15.6,4.96870845 14.9396,4 13.8,4 Z" fill="#000000" opacity="0.3" />
                                                        <path d="M3.79274528,6.57253826 L12,12.5 L20.2072547,6.57253826 C20.4311176,6.4108595 20.7436609,6.46126971 20.9053396,6.68513259 C20.9668779,6.77033951 21,6.87277228 21,6.97787787 L21,17 C21,18.1045695 20.1045695,19 19,19 L5,19 C3.8954305,19 3,18.1045695 3,17 L3,6.97787787 C3,6.70173549 3.22385763,6.47787787 3.5,6.47787787 C3.60510559,6.47787787 3.70753836,6.51099993 3.79274528,6.57253826 Z" fill="#000000" />
                                                    </g>
                                                </svg>
                                                <!--end::Svg Icon-->
                                            </span>
                                        </span>
                                        <span class="navi-text font-weight-bolder font-size-lg">
                                            <asp:Label ID="LblInbox" runat="server" />
                                        </span>
                                        <span id="spanNumberNewMessages" runat="server" visible="false" class="navi-label">
                                            <span class="label label-rounded label-light-success font-weight-bolder">
                                                <asp:Label ID="LblNumberNewMessages" runat="server" />
                                            </span>
                                        </span>
                                    </a>
                                </div>
                                <!--end::Item-->
                                <!--begin::Item-->
                                <div class="navi-item my-2">
                                    <a id="aSent" runat="server" class="navi-link">
                                        <span class="navi-icon mr-4">
                                            <span class="svg-icon svg-icon-lg">
                                                <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Sending.svg-->
                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                        <rect x="0" y="0" width="24" height="24" />
                                                        <path d="M8,13.1668961 L20.4470385,11.9999863 L8,10.8330764 L8,5.77181995 C8,5.70108058 8.01501031,5.63114635 8.04403925,5.56663761 C8.15735832,5.31481744 8.45336217,5.20254012 8.70518234,5.31585919 L22.545552,11.5440255 C22.6569791,11.5941677 22.7461882,11.6833768 22.7963304,11.794804 C22.9096495,12.0466241 22.7973722,12.342628 22.545552,12.455947 L8.70518234,18.6841134 C8.64067359,18.7131423 8.57073936,18.7281526 8.5,18.7281526 C8.22385763,18.7281526 8,18.504295 8,18.2281526 L8,13.1668961 Z" fill="#000000" />
                                                        <path d="M4,16 L5,16 C5.55228475,16 6,16.4477153 6,17 C6,17.5522847 5.55228475,18 5,18 L4,18 C3.44771525,18 3,17.5522847 3,17 C3,16.4477153 3.44771525,16 4,16 Z M1,11 L5,11 C5.55228475,11 6,11.4477153 6,12 C6,12.5522847 5.55228475,13 5,13 L1,13 C0.44771525,13 6.76353751e-17,12.5522847 0,12 C-6.76353751e-17,11.4477153 0.44771525,11 1,11 Z M4,6 L5,6 C5.55228475,6 6,6.44771525 6,7 C6,7.55228475 5.55228475,8 5,8 L4,8 C3.44771525,8 3,7.55228475 3,7 C3,6.44771525 3.44771525,6 4,6 Z" fill="#000000" opacity="0.3" />
                                                    </g>
                                                </svg>
                                                <!--end::Svg Icon-->
                                            </span>
                                        </span>
                                        <span class="navi-text font-weight-bolder font-size-lg">
                                            <asp:Label ID="LblSent" runat="server" />
                                        </span>
                                    </a>
                                </div>
                                <!--end::Item-->
                                <!--begin::Item-->
                                <div class="navi-item my-2">
                                    <a id="aTrash" runat="server" class="navi-link">
                                        <span class="navi-icon mr-4">
                                            <span class="svg-icon svg-icon-lg">
                                                <!--begin::Svg Icon | path:assets/media/svg/icons/General/Trash.svg-->
                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                        <rect x="0" y="0" width="24" height="24" />
                                                        <path d="M6,8 L6,20.5 C6,21.3284271 6.67157288,22 7.5,22 L16.5,22 C17.3284271,22 18,21.3284271 18,20.5 L18,8 L6,8 Z" fill="#000000" fill-rule="nonzero" />
                                                        <path d="M14,4.5 L14,4 C14,3.44771525 13.5522847,3 13,3 L11,3 C10.4477153,3 10,3.44771525 10,4 L10,4.5 L5.5,4.5 C5.22385763,4.5 5,4.72385763 5,5 L5,5.5 C5,5.77614237 5.22385763,6 5.5,6 L18.5,6 C18.7761424,6 19,5.77614237 19,5.5 L19,5 C19,4.72385763 18.7761424,4.5 18.5,4.5 L14,4.5 Z" fill="#000000" opacity="0.3" />
                                                    </g>
                                                </svg>
                                                <!--end::Svg Icon-->
                                            </span>
                                        </span>
                                        <span class="navi-text font-weight-bolder font-size-lg">
                                            <asp:Label ID="LblTrash" runat="server" /></span>
                                    </a>
                                </div>
                                <!--end::Item-->
                            </div>
                            <!--end::Navigations-->
                        </div>
                        <!--end::Body-->
                    </div>
                    <!--end::Card-->
                </div>
                <!--end::Aside-->
                <div style="display: none;" class="col-md-10">
                    <div id="divWarningMessage" runat="server" visible="false" class="alert alert-custom alert-notice alert-light-warning fade show" role="alert">
                        <div class="alert-icon"><i class="flaticon-warning"></i></div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblWarning" runat="server" />
                            </strong>
                            <asp:Label ID="LblWarningContent" runat="server" />
                            <a id="aFullRegist" runat="server" class="alert-link">
                                <asp:Label ID="LblActionLink" runat="server" />

                            </a>
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                            </button>
                        </div>
                    </div>
                </div>
                <!--begin::List-->
                <asp:PlaceHolder ID="PhInboxContent" runat="server" />


                <!--end::List-->
            </div>

            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->


    <!--begin::Compose-->
    <div class="modal modal-sticky modal-sticky-lg modal-sticky-bottom-right" id="kt_inbox_compose" role="dialog" data-backdrop="false">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <!--begin::Form-->
                <form id="kt_inbox_compose_form">
                    <!--begin::Header-->
                    <div class="d-flex align-items-center justify-content-between py-5 pl-8 pr-5 border-bottom">
                        <h5 class="font-weight-bold m-0">Compose</h5>
                        <div class="d-flex ml-2">
                            <span class="btn btn-clean btn-sm btn-icon mr-2">
                                <i class="flaticon2-arrow-1 icon-1x"></i>
                            </span>
                            <span class="btn btn-clean btn-sm btn-icon" data-dismiss="modal">
                                <i class="ki ki-close icon-1x"></i>
                            </span>
                        </div>
                    </div>
                    <!--end::Header-->
                    <!--begin::Body-->
                    <div class="d-block">
                        <!--begin::To-->
                        <div class="d-flex align-items-center border-bottom inbox-to px-8 min-h-45px">
                            <div class="text-dark-50 w-75px">To:</div>
                            <div class="d-flex align-items-center flex-grow-1">
                                <input type="text" class="form-control border-0" name="compose_to" value="Chris Muller, Lina Nilson" />
                            </div>
                            <div class="ml-2">
                                <span class="text-muted font-weight-bold cursor-pointer text-hover-primary mr-2" data-inbox="cc-show">Cc</span>
                                <span class="text-muted font-weight-bold cursor-pointer text-hover-primary" data-inbox="bcc-show">Bcc</span>
                            </div>
                        </div>
                        <!--end::To-->
                        <!--begin::CC-->
                        <div class="d-none align-items-center border-bottom inbox-to-cc pl-8 pr-5 min-h-45px">
                            <div class="text-dark-50 w-75px">Cc:</div>
                            <div class="flex-grow-1">
                                <input type="text" class="form-control border-0" name="compose_cc" value="" />
                            </div>
                            <span class="btn btn-clean btn-xs btn-icon" data-inbox="cc-hide">
                                <i class="la la-close"></i>
                            </span>
                        </div>
                        <!--end::CC-->
                        <!--begin::BCC-->
                        <div class="d-none align-items-center border-bottom inbox-to-bcc pl-8 pr-5 min-h-45px">
                            <div class="text-dark-50 w-75px">Bcc:</div>
                            <div class="flex-grow-1">
                                <input type="text" class="form-control border-0" name="compose_bcc" value="" />
                            </div>
                            <span class="btn btn-clean btn-xs btn-icon" data-inbox="bcc-hide">
                                <i class="la la-close"></i>
                            </span>
                        </div>
                        <!--end::BCC-->
                        <!--begin::Subject-->
                        <div class="border-bottom">
                            <input class="form-control border-0 px-8 min-h-45px" name="compose_subject" placeholder="Subject" />
                        </div>
                        <!--end::Subject-->
                        <!--begin::Message-->
                        <div id="kt_inbox_compose_editor" class="border-0" style="height: 250px"></div>
                        <!--end::Message-->
                        <!--begin::Attachments-->
                        <div class="dropzone dropzone-multi px-8 py-4" id="kt_inbox_compose_attachments">
                            <div class="dropzone-items">
                                <div class="dropzone-item" style="display: none">
                                    <div class="dropzone-file">
                                        <div class="dropzone-filename" title="some_image_file_name.jpg">
                                            <span data-dz-name="">some_image_file_name.jpg</span>
                                            <strong>(
																	
                                                <span data-dz-size="">340kb</span>)</strong>
                                        </div>
                                        <div class="dropzone-error" data-dz-errormessage=""></div>
                                    </div>
                                    <div class="dropzone-progress">
                                        <div class="progress">
                                            <div class="progress-bar bg-primary" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0" data-dz-uploadprogress=""></div>
                                        </div>
                                    </div>
                                    <div class="dropzone-toolbar">
                                        <span class="dropzone-delete" data-dz-remove="">
                                            <i class="flaticon2-cross"></i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end::Attachments-->
                    </div>
                    <!--end::Body-->
                    <!--begin::Footer-->
                    <div class="d-flex align-items-center justify-content-between py-5 pl-8 pr-5 border-top">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3">
                            <!--begin::Send-->
                            <div class="btn-group mr-4">
                                <span class="btn btn-primary font-weight-bold px-6">Send</span>
                                <span class="btn btn-primary font-weight-bold dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" role="button"></span>
                                <div class="dropdown-menu dropdown-menu-sm dropup p-0 m-0 dropdown-menu-right">
                                    <ul class="navi py-3">
                                        <li class="navi-item">
                                            <a href="#" class="navi-link">
                                                <span class="navi-icon">
                                                    <i class="flaticon2-writing"></i>
                                                </span>
                                                <span class="navi-text">Schedule Send</span>
                                            </a>
                                        </li>
                                        <li class="navi-item">
                                            <a href="#" class="navi-link">
                                                <span class="navi-icon">
                                                    <i class="flaticon2-medical-records"></i>
                                                </span>
                                                <span class="navi-text">Save &amp; archive</span>
                                            </a>
                                        </li>
                                        <li class="navi-item">
                                            <a href="#" class="navi-link">
                                                <span class="navi-icon">
                                                    <i class="flaticon2-hourglass-1"></i>
                                                </span>
                                                <span class="navi-text">Cancel</span>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <!--end::Send-->
                            <!--begin::Other-->
                            <span class="btn btn-icon btn-sm btn-clean mr-2" id="kt_inbox_compose_attachments_select">
                                <i class="flaticon2-clip-symbol"></i>
                            </span>
                            <span class="btn btn-icon btn-sm btn-clean">
                                <i class="flaticon2-pin"></i>
                            </span>
                            <!--end::Other-->
                        </div>
                        <!--end::Actions-->
                        <!--begin::Toolbar-->
                        <div class="d-flex align-items-center">
                            <span class="btn btn-icon btn-sm btn-clean mr-2" data-toggle="tooltip" title="More actions">
                                <i class="flaticon2-settings"></i>
                            </span>
                            <span class="btn btn-icon btn-sm btn-clean" data-inbox="dismiss" data-toggle="tooltip" title="Dismiss reply">
                                <i class="flaticon2-rubbish-bin-delete-button"></i>
                            </span>
                        </div>
                        <!--end::Toolbar-->
                    </div>
                    <!--end::Footer-->
                </form>
                <!--end::Form-->
            </div>
        </div>
    </div>
    <!--end::Compose-->

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        
        <!--begin::Page Scripts(used by this page)-->
        <script src="/assets/js/pages/custom/inbox/inbox.js"></script>
        <!--end::Page Scripts-->

    </telerik:RadScriptBlock>

</asp:Content>
