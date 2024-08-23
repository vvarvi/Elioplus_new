<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InboxSentControl.ascx.cs" Inherits="WdS.ElioPlus.Controls.InboxSentControl" %>

<!--begin::List-->
<div class="flex-row-fluid ml-lg-8 d-block" id="kt_inbox_list">
    <!--begin::Card-->
    <div class="card card-custom card-stretch">
        <!--begin::Header-->
        <div class="card-header row row-marginless align-items-center flex-wrap py-5 h-auto">
            <!--begin::Toolbar-->
            <div class="col-12 col-sm-6 col-xxl-4 order-2 order-xxl-1 d-flex flex-wrap align-items-center">
                <div class="d-flex align-items-center mr-1 my-2">
                    <label data-inbox="group-select" class="checkbox checkbox-single checkbox-primary mr-3">
                        <input type="checkbox" />
                        <span class="symbol-label"></span>
                    </label>
                    <div class="btn-group">
                        <span class="btn btn-clean btn-icon btn-sm mr-1" data-toggle="dropdown">
                            <i class="ki ki-bold-arrow-down icon-sm"></i>
                        </span>
                        <div class="dropdown-menu dropdown-menu-left p-0 m-0 dropdown-menu-sm">
                            <ul class="navi py-3">
                                <li class="navi-item">
                                    <a href="#" class="navi-link">
                                        <span class="navi-text">All</span>
                                    </a>
                                </li>
                                <li class="navi-item">
                                    <a href="#" class="navi-link">
                                        <span class="navi-text">Read</span>
                                    </a>
                                </li>
                                <li class="navi-item">
                                    <a href="#" class="navi-link">
                                        <span class="navi-text">Unread</span>
                                    </a>
                                </li>
                                <li class="navi-item">
                                    <a href="#" class="navi-link">
                                        <span class="navi-text">Starred</span>
                                    </a>
                                </li>
                                <li class="navi-item">
                                    <a href="#" class="navi-link">
                                        <span class="navi-text">Unstarred</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <span class="btn btn-clean btn-icon btn-sm mr-2" data-toggle="tooltip" title="Reload list">
                        <i class="ki ki-refresh icon-1x"></i>
                    </span>
                </div>
                <div class="d-flex align-items-center mr-1 my-2">
                    <span class="btn btn-default btn-icon btn-sm mr-2" data-toggle="tooltip" title="Archive">
                        <span class="svg-icon svg-icon-md">
                            <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Mail-opened.svg-->
                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                    <rect x="0" y="0" width="24" height="24" />
                                    <path d="M6,2 L18,2 C18.5522847,2 19,2.44771525 19,3 L19,12 C19,12.5522847 18.5522847,13 18,13 L6,13 C5.44771525,13 5,12.5522847 5,12 L5,3 C5,2.44771525 5.44771525,2 6,2 Z M7.5,5 C7.22385763,5 7,5.22385763 7,5.5 C7,5.77614237 7.22385763,6 7.5,6 L13.5,6 C13.7761424,6 14,5.77614237 14,5.5 C14,5.22385763 13.7761424,5 13.5,5 L7.5,5 Z M7.5,7 C7.22385763,7 7,7.22385763 7,7.5 C7,7.77614237 7.22385763,8 7.5,8 L10.5,8 C10.7761424,8 11,7.77614237 11,7.5 C11,7.22385763 10.7761424,7 10.5,7 L7.5,7 Z" fill="#000000" opacity="0.3" />
                                    <path d="M3.79274528,6.57253826 L12,12.5 L20.2072547,6.57253826 C20.4311176,6.4108595 20.7436609,6.46126971 20.9053396,6.68513259 C20.9668779,6.77033951 21,6.87277228 21,6.97787787 L21,17 C21,18.1045695 20.1045695,19 19,19 L5,19 C3.8954305,19 3,18.1045695 3,17 L3,6.97787787 C3,6.70173549 3.22385763,6.47787787 3.5,6.47787787 C3.60510559,6.47787787 3.70753836,6.51099993 3.79274528,6.57253826 Z" fill="#000000" />
                                </g>
                            </svg>
                            <!--end::Svg Icon-->
                        </span>
                    </span>
                    <span class="btn btn-default btn-icon btn-sm mr-2 d-none" data-toggle="tooltip" title="Spam">
                        <span class="svg-icon svg-icon-md">
                            <!--begin::Svg Icon | path:assets/media/svg/icons/Code/Warning-1-circle.svg-->
                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                    <rect x="0" y="0" width="24" height="24" />
                                    <circle fill="#000000" opacity="0.3" cx="12" cy="12" r="10" />
                                    <rect fill="#000000" x="11" y="7" width="2" height="8" rx="1" />
                                    <rect fill="#000000" x="11" y="16" width="2" height="2" rx="1" />
                                </g>
                            </svg>
                            <!--end::Svg Icon-->
                        </span>
                    </span>
                    <span class="btn btn-default btn-icon btn-sm mr-2" data-toggle="tooltip" title="Delete">
                        <span class="svg-icon svg-icon-md">
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
                    <span class="btn btn-default btn-icon btn-sm mr-2" data-toggle="tooltip" title="Mark as read">
                        <span class="svg-icon svg-icon-md">
                            <!--begin::Svg Icon | path:assets/media/svg/icons/General/Duplicate.svg-->
                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                    <rect x="0" y="0" width="24" height="24" />
                                    <path d="M15.9956071,6 L9,6 C7.34314575,6 6,7.34314575 6,9 L6,15.9956071 C4.70185442,15.9316381 4,15.1706419 4,13.8181818 L4,6.18181818 C4,4.76751186 4.76751186,4 6.18181818,4 L13.8181818,4 C15.1706419,4 15.9316381,4.70185442 15.9956071,6 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                    <path d="M10.1818182,8 L17.8181818,8 C19.2324881,8 20,8.76751186 20,10.1818182 L20,17.8181818 C20,19.2324881 19.2324881,20 17.8181818,20 L10.1818182,20 C8.76751186,20 8,19.2324881 8,17.8181818 L8,10.1818182 C8,8.76751186 8.76751186,8 10.1818182,8 Z" fill="#000000" />
                                </g>
                            </svg>
                            <!--end::Svg Icon-->
                        </span>
                    </span>
                    <span class="btn btn-default btn-icon btn-sm mr-2" data-toggle="tooltip" title="Move">
                        <span class="svg-icon svg-icon-md">
                            <!--begin::Svg Icon | path:assets/media/svg/icons/Files/Media-folder.svg-->
                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                    <rect x="0" y="0" width="24" height="24" />
                                    <path d="M3.5,21 L20.5,21 C21.3284271,21 22,20.3284271 22,19.5 L22,8.5 C22,7.67157288 21.3284271,7 20.5,7 L10,7 L7.43933983,4.43933983 C7.15803526,4.15803526 6.77650439,4 6.37867966,4 L3.5,4 C2.67157288,4 2,4.67157288 2,5.5 L2,19.5 C2,20.3284271 2.67157288,21 3.5,21 Z" fill="#000000" opacity="0.3" />
                                    <path d="M10.782158,17.5100514 L15.1856088,14.5000448 C15.4135806,14.3442132 15.4720618,14.0330791 15.3162302,13.8051073 C15.2814587,13.7542388 15.2375842,13.7102355 15.1868178,13.6753149 L10.783367,10.6463273 C10.5558531,10.489828 10.2445489,10.5473967 10.0880496,10.7749107 C10.0307022,10.8582806 10,10.9570884 10,11.0582777 L10,17.097272 C10,17.3734143 10.2238576,17.597272 10.5,17.597272 C10.6006894,17.597272 10.699033,17.566872 10.782158,17.5100514 Z" fill="#000000" />
                                </g>
                            </svg>
                            <!--end::Svg Icon-->
                        </span>
                    </span>
                </div>
            </div>
            <!--end::Toolbar-->
            <!--begin::Search-->
            <div class="col-xxl-3 d-flex order-1 order-xxl-2 align-items-center justify-content-center">
                <div class="input-group input-group-lg input-group-solid my-2">
                    <input type="text" class="form-control pl-4" placeholder="Search..." />
                    <div class="input-group-append">
                        <span class="input-group-text pr-3">
                            <span class="svg-icon svg-icon-lg">
                                <!--begin::Svg Icon | path:assets/media/svg/icons/General/Search.svg-->
                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                        <rect x="0" y="0" width="24" height="24" />
                                        <path d="M14.2928932,16.7071068 C13.9023689,16.3165825 13.9023689,15.6834175 14.2928932,15.2928932 C14.6834175,14.9023689 15.3165825,14.9023689 15.7071068,15.2928932 L19.7071068,19.2928932 C20.0976311,19.6834175 20.0976311,20.3165825 19.7071068,20.7071068 C19.3165825,21.0976311 18.6834175,21.0976311 18.2928932,20.7071068 L14.2928932,16.7071068 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                        <path d="M11,16 C13.7614237,16 16,13.7614237 16,11 C16,8.23857625 13.7614237,6 11,6 C8.23857625,6 6,8.23857625 6,11 C6,13.7614237 8.23857625,16 11,16 Z M11,18 C7.13400675,18 4,14.8659932 4,11 C4,7.13400675 7.13400675,4 11,4 C14.8659932,4 18,7.13400675 18,11 C18,14.8659932 14.8659932,18 11,18 Z" fill="#000000" fill-rule="nonzero" />
                                    </g>
                                </svg>
                                <!--end::Svg Icon-->
                            </span>
                        </span>
                    </div>
                </div>
            </div>
            <!--end::Search-->
            <!--begin::Pagination-->
            <div class="col-12 col-sm-6 col-xxl-4 order-2 order-xxl-3 d-flex align-items-center justify-content-sm-end text-right my-2">
                <!--begin::Per Page Dropdown-->
                <div class="d-flex align-items-center mr-2" data-toggle="tooltip" title="Records per page">
                    <span class="text-muted font-weight-bold mr-2" data-toggle="dropdown">1 - 50 of 235</span>
                    <div class="dropdown-menu dropdown-menu-right p-0 m-0 dropdown-menu-sm">
                        <ul class="navi py-3">
                            <li class="navi-item">
                                <a href="#" class="navi-link">
                                    <span class="navi-text">20 per page</span>
                                </a>
                            </li>
                            <li class="navi-item">
                                <a href="#" class="navi-link active">
                                    <span class="navi-text">50 par page</span>
                                </a>
                            </li>
                            <li class="navi-item">
                                <a href="#" class="navi-link">
                                    <span class="navi-text">100 per page</span>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
                <!--end::Per Page Dropdown-->
                <!--begin::Arrow Buttons-->
                <span class="btn btn-default btn-icon btn-sm mr-2" data-toggle="tooltip" title="Previose page">
                    <i class="ki ki-bold-arrow-back icon-sm"></i>
                </span>
                <span class="btn btn-default btn-icon btn-sm mr-2" data-toggle="tooltip" title="Next page">
                    <i class="ki ki-bold-arrow-next icon-sm"></i>
                </span>
                <!--end::Arrow Buttons-->
                <!--begin::Sort Dropdown-->
                <div class="dropdown mr-2" data-toggle="tooltip" title="Sort">
                    <span class="btn btn-default btn-icon btn-sm" data-toggle="dropdown">
                        <i class="flaticon2-console icon-1x"></i>
                    </span>
                    <div class="dropdown-menu dropdown-menu-right p-0 m-0 dropdown-menu-sm">
                        <ul class="navi py-3">
                            <li class="navi-item">
                                <a href="#" class="navi-link active">
                                    <span class="navi-text">Newest</span>
                                </a>
                            </li>
                            <li class="navi-item">
                                <a href="#" class="navi-link">
                                    <span class="navi-text">Olders</span>
                                </a>
                            </li>
                            <li class="navi-item">
                                <a href="#" class="navi-link">
                                    <span class="navi-text">Unread</span>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
                <!--end::Sort Dropdown-->
                <!--begin::Options Dropdown-->
                <div class="dropdown" data-toggle="tooltip" title="Settings">
                    <span class="btn btn-default btn-icon btn-sm" data-toggle="dropdown">
                        <i class="ki ki-bold-more-hor icon-1x"></i>
                    </span>
                    <div class="dropdown-menu dropdown-menu-right p-0 m-0 dropdown-menu-md">
                        <!--begin::Navigation-->
                        <ul class="navi navi-hover py-5">
                            <li class="navi-item">
                                <a href="#" class="navi-link">
                                    <span class="navi-icon">
                                        <i class="flaticon2-drop"></i>
                                    </span>
                                    <span class="navi-text">New Group</span>
                                </a>
                            </li>
                            <li class="navi-item">
                                <a href="#" class="navi-link">
                                    <span class="navi-icon">
                                        <i class="flaticon2-list-3"></i>
                                    </span>
                                    <span class="navi-text">Contacts</span>
                                </a>
                            </li>
                            <li class="navi-item">
                                <a href="#" class="navi-link">
                                    <span class="navi-icon">
                                        <i class="flaticon2-rocket-1"></i>
                                    </span>
                                    <span class="navi-text">Groups</span>
                                    <span class="navi-link-badge">
                                        <span class="label label-light-primary label-inline font-weight-bold">new</span>
                                    </span>
                                </a>
                            </li>
                            <li class="navi-item">
                                <a href="#" class="navi-link">
                                    <span class="navi-icon">
                                        <i class="flaticon2-bell-2"></i>
                                    </span>
                                    <span class="navi-text">Calls</span>
                                </a>
                            </li>
                            <li class="navi-item">
                                <a href="#" class="navi-link">
                                    <span class="navi-icon">
                                        <i class="flaticon2-gear"></i>
                                    </span>
                                    <span class="navi-text">Settings</span>
                                </a>
                            </li>
                            <li class="navi-separator my-3"></li>
                            <li class="navi-item">
                                <a href="#" class="navi-link">
                                    <span class="navi-icon">
                                        <i class="flaticon2-magnifier-tool"></i>
                                    </span>
                                    <span class="navi-text">Help</span>
                                </a>
                            </li>
                            <li class="navi-item">
                                <a href="#" class="navi-link">
                                    <span class="navi-icon">
                                        <i class="flaticon2-bell-2"></i>
                                    </span>
                                    <span class="navi-text">Privacy</span>
                                    <span class="navi-link-badge">
                                        <span class="label label-light-danger label-rounded font-weight-bold">5</span>
                                    </span>
                                </a>
                            </li>
                        </ul>
                        <!--end::Navigation-->
                    </div>
                </div>
                <!--end::Options Dropdown-->
            </div>
            <!--end::Pagination-->
        </div>
        <!--end::Header-->
        <!--begin::Body-->
        <div class="card-body table-responsive px-0">
            <!--begin::Items-->
            <div class="list list-hover min-w-500px" data-inbox="list">
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <span class="symbol symbol-35 mr-3">
                                <span class="symbol-label" style="background-image: url('/assets/media/users/100_13.jpg')"></span>
                            </span>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Sean Paul</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Digital PPV Customer Confirmation -</span>
                            <span class="text-muted">Thank you for ordering UFC 240 Holloway vs Edgar Alternate camera angles...</span>
                        </div>
                        <div class="mt-2">
                            <span class="label label-light-primary font-weight-bold label-inline mr-1">inbox</span>
                            <span class="label label-light-danger font-weight-bold label-inline">task</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-bolder w-50px text-right" data-toggle="view">8:30 PM</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning active" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <div class="symbol symbol-light-danger symbol-35 mr-3">
                                <span class="symbol-label font-weight-bolder">OJ</span>
                            </div>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Oliver Jake</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Your iBuy.com grocery shopping confirmation -</span>
                            <span class="text-muted">Please make sure that you have one of the following cards with you when we deliver your order...</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-bolder w-100px text-right" data-toggle="view">day ago</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <div class="symbol symbol-light-primary symbol-35 mr-3">
                                <span class="symbol-label font-weight-bolder">EF</span>
                            </div>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Enrico Fermi</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Your Order #224820998666029 has been Confirmed -</span>
                            <span class="text-muted">Your Order #224820998666029 has been placed on Saturday, 29 June</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-normal w-100px text-right text-muted" data-toggle="view">11:20PM</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <span class="symbol symbol-35 mr-3">
                                <span class="symbol-label" style="background-image: url('/assets/media/users/100_2.jpg')"></span>
                            </span>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Jane Goodall</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Payment Notification DLOP2329KD -</span>
                            <span class="text-muted">Your payment of 4500USD to AirCar has been authorized and confirmed, thank you your account. This...</span>
                        </div>
                        <div class="mt-2">
                            <span class="label label-light-danger font-weight-bold label-inline">new</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-normal w-100px text-right text-muted" data-toggle="view">2 days ago</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <div class="symbol symbol-light-success symbol-35 mr-3">
                                <span class="symbol-label font-weight-bolder">MP</span>
                            </div>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Max O'Brien Planck</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Congratulations on your iRun Coach subscription -</span>
                            <span class="text-muted">Congratulations on your iRun Coach subscription. You made no space for excuses and you</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-normal w-100px text-right text-muted" data-toggle="view">July 25</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <span class="symbol symbol-35 mr-3">
                                <span class="symbol-label" style="background-image: url('/assets/media/users/100_4.jpg')"></span>
                            </span>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Rita Levi-Montalcini</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Pay bills &amp; win up to 600$ Cashback! -</span>
                            <span class="text-muted">Congratulations on your iRun Coach subscription. You made no space for excuses and you decided on a healthier and happier life...</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-normal w-100px text-right text-muted" data-toggle="view">July 24</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <span class="symbol symbol-35 mr-3">
                                <span class="symbol-label" style="background-image: url('/assets/media/users/100_5.jpg')"></span>
                            </span>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Stephen Hawking</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Activate your LIPO Account today -</span>
                            <span class="text-muted">Thank you for creating a LIPO Account. Please click the link below to activate your account.</span>
                        </div>
                        <div class="mt-2">
                            <span class="label label-light-warning font-weight-bold label-inline mr-2">task</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-normal w-100px text-right text-muted" data-toggle="view">July 13</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <div class="symbol symbol-light symbol-35 mr-3">
                                <span class="symbol-label text-dark-75 font-weight-bolder">WE</span>
                            </div>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Wolfgang Ernst Pauli</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">About your request for PalmLake -</span>
                            <span class="text-muted">What you requested can't be arranged ahead of time but PalmLake said they'll do their best to accommodate you upon arrival....</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-bold text-muted w-100px text-right" data-toggle="view">25 May</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <span class="symbol symbol-35 mr-3">
                                <span class="symbol-label" style="background-image: url('/assets/media/users/100_6.jpg')"></span>
                            </span>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Patty Jo Watson</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Welcome, Patty -</span>
                            <span class="text-muted">Discover interesting ideas and unique perspectives. Read, explore and follow your interests. Get personalized recommendations delivered to you....</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-normal w-100px text-right text-muted" data-toggle="view">July 24</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <span class="symbol symbol-35 mr-3">
                                <span class="symbol-label" style="background-image: url('/assets/media/users/100_8.jpg')"></span>
                            </span>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Blaise Pascal</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Free Video Marketing Guide -</span>
                            <span class="text-muted">Video has rolled into every marketing platform or channel, leaving...</span>
                        </div>
                        <div class="mt-2">
                            <span class="label label-light-success font-weight-bold label-inline">project</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-normal w-100px text-right text-muted" data-toggle="view">July 13</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning active" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <div class="symbol symbol-light-warning symbol-35 mr-3">
                                <span class="symbol-label font-weight-bolder">RO</span>
                            </div>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Roberts O'Neill Wilson</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Your iBuy.com grocery shopping confirmation -</span>
                            <span class="text-muted">Please make sure that you have one of the following cards with you when we deliver your order...</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-bolder w-100px text-right" data-toggle="view">day ago</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs text-hover-warning" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <div class="symbol symbol-light-primary symbol-35 mr-3">
                                <span class="symbol-label font-weight-bolder">EF</span>
                            </div>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Enrico Fermi</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Your Order #224820998666029 has been Confirmed -</span>
                            <span class="text-muted">Your Order #224820998666029 has been placed on Saturday, 29 June</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-normal w-100px text-right text-muted" data-toggle="view">11:20PM</div>
                    <!--end::Datetime-->
                </div>
                <!--end::Item-->
                <!--begin::Item-->
                <div class="d-flex align-items-start list-item card-spacer-x py-3" data-inbox="message">
                    <!--begin::Toolbar-->
                    <div class="d-flex align-items-center">
                        <!--begin::Actions-->
                        <div class="d-flex align-items-center mr-3" data-inbox="actions">
                            <label class="checkbox checkbox-single checkbox-primary flex-shrink-0 mr-3">
                                <input type="checkbox" />
                                <span></span>
                            </label>
                            <a href="#" class="btn btn-icon btn-xs btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Star">
                                <i class="flaticon-star text-muted"></i>
                            </a>
                            <a href="#" class="btn btn-icon btn-xs btn-hover-text-warning active" data-toggle="tooltip" data-placement="right" title="Mark as important">
                                <i class="flaticon-add-label-button text-muted"></i>
                            </a>
                        </div>
                        <!--end::Actions-->
                        <!--begin::Author-->
                        <div class="d-flex align-items-center flex-wrap w-xxl-200px mr-3" data-toggle="view">
                            <span class="symbol symbol-35 mr-3">
                                <span class="symbol-label" style="background-image: url('/assets/media/users/100_10.jpg')"></span>
                            </span>
                            <a href="#" class="font-weight-bold text-dark-75 text-hover-primary">Jane Goodall</a>
                        </div>
                        <!--end::Author-->
                    </div>
                    <!--end::Toolbar-->
                    <!--begin::Info-->
                    <div class="flex-grow-1 mt-2 mr-2" data-toggle="view">
                        <div>
                            <span class="font-weight-bolder font-size-lg mr-2">Payment Notification DLOP2329KD -</span>
                            <span class="text-muted">Your payment of 4500USD to AirCar has been authorized and confirmed, thank you your account. This...</span>
                        </div>
                    </div>
                    <!--end::Info-->
                    <!--begin::Datetime-->
                    <div class="mt-2 mr-3 font-weight-normal w-100px text-right text-muted" data-toggle="view">2 days ago</div>
                    <!--end::Datetime-->
                </div>
            </div>
            <!--end::Items-->
        </div>
        <!--end::Body-->
    </div>
    <!--end::Card-->
</div>
<!--end::List-->
