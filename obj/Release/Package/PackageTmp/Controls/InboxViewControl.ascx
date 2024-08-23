<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InboxViewControl.ascx.cs" Inherits="WdS.ElioPlus.Controls.InboxViewControl" %>

<!--begin::View-->
<div class="flex-row-fluid ml-lg-8 d-none" id="kt_inbox_view">
    <!--begin::Card-->
    <div class="card card-custom card-stretch">
        <!--begin::Header-->
        <div class="card-header align-items-center flex-wrap justify-content-between py-5 h-auto">
            <!--begin::Left-->
            <div class="d-flex align-items-center my-2">
                <a href="#" class="btn btn-clean btn-icon btn-sm mr-6" data-inbox="back">
                    <i class="flaticon2-left-arrow-1"></i>
                </a>
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
                <span class="btn btn-default btn-icon btn-sm mr-2" data-toggle="tooltip" title="Spam">
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
            <!--end::Left-->
            <!--begin::Right-->
            <div class="d-flex align-items-center justify-content-end text-right my-2">
                <span class="text-muted font-weight-bold mr-4" data-toggle="dropdown">1 - 50 of 235</span>
                <span class="btn btn-default btn-icon btn-sm mr-2" data-toggle="tooltip" title="Previose message">
                    <i class="ki ki-bold-arrow-back icon-sm"></i>
                </span>
                <span class="btn btn-default btn-icon btn-sm mr-2" data-toggle="tooltip" title="Next message">
                    <i class="ki ki-bold-arrow-next icon-sm"></i>
                </span>
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
            </div>
            <!--end::Right-->
        </div>
        <!--end::Header-->
        <!--begin::Body-->
        <div class="card-body p-0">
            <!--begin::Header-->
            <div class="d-flex align-items-center justify-content-between flex-wrap card-spacer-x py-5">
                <!--begin::Title-->
                <div class="d-flex align-items-center mr-2 py-2">
                    <div class="font-weight-bold font-size-h3 mr-3">Trip Reminder. Thank you for flying with us!</div>
                    <span class="label label-light-primary font-weight-bold label-inline mr-2">inbox</span>
                    <span class="label label-light-danger font-weight-bold label-inline">important</span>
                </div>
                <!--end::Title-->
                <!--begin::Toolbar-->
                <div class="d-flex py-2">
                    <span class="btn btn-default btn-sm btn-icon mr-2">
                        <i class="flaticon2-sort"></i>
                    </span>
                    <span class="btn btn-default btn-sm btn-icon" data-dismiss="modal">
                        <i class="flaticon2-fax"></i>
                    </span>
                </div>
                <!--end::Toolbar-->
            </div>
            <!--end::Header-->
            <!--begin::Messages-->
            <div class="mb-3">
                <div class="cursor-pointer shadow-xs toggle-on" data-inbox="message">
                    <div class="d-flex align-items-center card-spacer-x py-6">
                        <span class="symbol symbol-50 mr-4">
                            <span class="symbol-label" style="background-image: url('/assets/media/users/100_13.jpg')"></span>
                        </span>
                        <div class="d-flex flex-column flex-grow-1 flex-wrap mr-2">
                            <div class="d-flex">
                                <a href="#" class="font-size-lg font-weight-bolder text-dark-75 text-hover-primary mr-2">Chris Muller</a>
                                <div class="font-weight-bold text-muted">
                                    <span class="label label-success label-dot mr-2"></span>1 Day ago
                                </div>
                            </div>
                            <div class="d-flex flex-column">
                                <div class="toggle-off-item">
                                    <span class="font-weight-bold text-muted cursor-pointer" data-toggle="dropdown">to me
																	
                                        <i class="flaticon2-down icon-xs ml-1 text-dark-50"></i></span>
                                    <div class="dropdown-menu dropdown-menu-lg dropdown-menu-left p-5">
                                        <table>
                                            <tr>
                                                <td class="text-muted min-w-75px py-2">From</td>
                                                <td>Mark Andre</td>
                                            </tr>
                                            <tr>
                                                <td class="text-muted py-2">Date:</td>
                                                <td>Jul 30, 2019, 11:27 PM</td>
                                            </tr>
                                            <tr>
                                                <td class="text-muted py-2">Subject:</td>
                                                <td>Trip Reminder. Thank you for flying with us!</td>
                                            </tr>
                                            <tr>
                                                <td class="text-muted py-2">Reply to:</td>
                                                <td>mark.andre@gmail.com</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="text-muted font-weight-bold toggle-on-item" data-inbox="toggle">With resrpect, i must disagree with Mr.Zinsser. We all know the most part of important part....</div>
                            </div>
                        </div>
                        <div class="d-flex align-items-center">
                            <div class="font-weight-bold text-muted mr-2">Jul 15, 2019, 11:19AM</div>
                            <div class="d-flex align-items-center" data-inbox="toolbar">
                                <span class="btn btn-clean btn-sm btn-icon mr-2" data-toggle="tooltip" data-placement="top" title="Star">
                                    <i class="flaticon-star icon-1x"></i>
                                </span>
                                <span class="btn btn-clean btn-sm btn-icon mr-2" data-toggle="tooltip" data-placement="top" title="Mark as important">
                                    <i class="flaticon-add-label-button icon-1x"></i>
                                </span>
                                <span class="btn btn-clean btn-sm btn-icon mr-2" data-toggle="tooltip" data-placement="top" title="Reply">
                                    <i class="flaticon2-reply-1 icon-1x"></i>
                                </span>
                                <span class="btn btn-clean btn-sm btn-icon" data-toggle="tooltip" data-placement="top" title="Settings">
                                    <i class="flaticon-more icon-1x"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="card-spacer-x py-3 toggle-off-item">
                        <p>Hi Bob,</p>
                        <p>With resrpect, i must disagree with Mr.Zinsser. We all know the most part of important part of any article is the title.Without a compelleing title, your reader won't even get to the first sentence.After the title, however, the first few sentences of your article are certainly the most important part.</p>
                        <p>Jornalists call this critical, introductory section the "Lede," and when bridge properly executed, it's the that carries your reader from an headine try at attention-grabbing to the body of your blog post, if you want to get it right on of these 10 clever ways to omen your next blog posr with a bang</p>
                        <p>Best regards,</p>
                        <p>Jason Muller</p>
                    </div>
                </div>
                <div class="cursor-pointer shadow-xs toggle-off" data-inbox="message">
                    <div class="d-flex align-items-center card-spacer-x py-6">
                        <span class="symbol symbol-50 mr-4" data-toggle="expand">
                            <span class="symbol-label" style="background-image: url('/assets/media/users/100_11.jpg')"></span>
                        </span>
                        <div class="d-flex flex-column flex-grow-1 flex-wrap mr-2">
                            <div class="d-flex" data-toggle="expand">
                                <a href="#" class="font-size-lg font-weight-bolder text-dark-75 text-hover-primary mr-2">Lina Nilson</a>
                                <div class="font-weight-bold text-muted">
                                    <span class="label label-success label-dot mr-2"></span>2 Day ago
                                </div>
                            </div>
                            <div class="d-flex flex-column">
                                <div class="toggle-off-item">
                                    <span class="font-weight-bold text-muted cursor-pointer" data-toggle="dropdown">to me
																	
                                        <i class="flaticon2-down icon-xs ml-1 text-dark-50"></i></span>
                                    <div class="dropdown-menu dropdown-menu-md dropdown-menu-left p-5">
                                        <table>
                                            <tr>
                                                <td class="text-muted w-75px py-2">From</td>
                                                <td>Mark Andre</td>
                                            </tr>
                                            <tr>
                                                <td class="text-muted py-2">Date:</td>
                                                <td>Jul 30, 2019, 11:27 PM</td>
                                            </tr>
                                            <tr>
                                                <td class="text-muted py-2">Subject:</td>
                                                <td>Trip Reminder. Thank you for flying with us!</td>
                                            </tr>
                                            <tr>
                                                <td class="text-muted py-2">Reply to:</td>
                                                <td>mark.andre@gmail.com</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="text-muted font-weight-bold toggle-on-item" data-toggle="expand">Jornalists call this critical, introductory section the "Lede," and when bridge properly executed....</div>
                            </div>
                        </div>
                        <div class="d-flex align-items-center">
                            <div class="font-weight-bold text-muted mr-2" data-toggle="expand">Jul 20, 2019, 03:20PM</div>
                            <div class="d-flex align-items-center">
                                <span class="btn btn-clean btn-sm btn-icon mr-2" data-toggle="tooltip" data-placement="top" title="Star">
                                    <i class="flaticon-star icon-1x text-warning"></i>
                                </span>
                                <span class="btn btn-clean btn-sm btn-icon mr-2" data-toggle="tooltip" data-placement="top" title="Mark as important">
                                    <i class="flaticon-add-label-button icon-1x"></i>
                                </span>
                                <span class="btn btn-clean btn-sm btn-icon mr-2" data-toggle="tooltip" data-placement="top" title="Reply">
                                    <i class="flaticon2-reply-1 icon-1x"></i>
                                </span>
                                <span class="btn btn-clean btn-sm btn-icon" data-toggle="tooltip" data-placement="top" title="Settings">
                                    <i class="flaticon-more icon-1x"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="card-spacer-x py-3 toggle-off-item">
                        <p>Hi Bob,</p>
                        <p>With resrpect, i must disagree with Mr.Zinsser. We all know the most part of important part of any article is the title.Without a compelleing title, your reader won't even get to the first sentence.After the title, however, the first few sentences of your article are certainly the most important part.</p>
                        <p>Jornalists call this critical, introductory section the "Lede," and when bridge properly executed, it's the that carries your reader from an headine try at attention-grabbing to the body of your blog post, if you want to get it right on of these 10 clever ways to omen your next blog posr with a bang</p>
                        <p>Best regards,</p>
                        <p>Jason Muller</p>
                    </div>
                </div>
                <div class="cursor-pointer shadow-xs toggle-off" data-inbox="message">
                    <div class="d-flex align-items-center card-spacer-x py-6">
                        <span class="symbol symbol-50 mr-4" data-toggle="expand">
                            <span class="symbol-label" style="background-image: url('/assets/media/users/100_14.jpg')"></span>
                        </span>
                        <div class="d-flex flex-column flex-grow-1 flex-wrap mr-2">
                            <div class="d-flex" data-toggle="expand">
                                <a href="#" class="font-size-lg font-weight-bolder text-dark-75 text-hover-primary mr-2">Sean Stone</a>
                                <div class="font-weight-bold text-muted">
                                    <span class="label label-success label-dot mr-2"></span>1 Day ago
                                </div>
                            </div>
                            <div class="d-flex flex-column">
                                <div class="toggle-off-item">
                                    <span class="font-weight-bold text-muted cursor-pointer" data-toggle="dropdown">to me
																	
                                        <i class="flaticon2-down icon-xs ml-1 text-dark-50"></i></span>
                                    <div class="dropdown-menu dropdown-menu-md dropdown-menu-left p-5">
                                        <table>
                                            <tr>
                                                <td class="text-muted w-75px py-2">From</td>
                                                <td>Mark Andre</td>
                                            </tr>
                                            <tr>
                                                <td class="text-muted py-2">Date:</td>
                                                <td>Jul 30, 2019, 11:27 PM</td>
                                            </tr>
                                            <tr>
                                                <td class="text-muted py-2">Subject:</td>
                                                <td>Trip Reminder. Thank you for flying with us!</td>
                                            </tr>
                                            <tr>
                                                <td class="text-muted py-2">Reply to:</td>
                                                <td>mark.andre@gmail.com</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="text-muted font-weight-bold toggle-on-item" data-toggle="expand">With resrpect, i must disagree with Mr.Zinsser. We all know the most part of important part....</div>
                            </div>
                        </div>
                        <div class="d-flex align-items-center">
                            <div class="font-weight-bold text-muted mr-2" data-toggle="expand">Jul 15, 2019, 11:19AM</div>
                            <div class="d-flex align-items-center">
                                <span class="btn btn-clean btn-sm btn-icon mr-2" data-toggle="tooltip" data-placement="top" title="Star">
                                    <i class="flaticon-star icon-1x"></i>
                                </span>
                                <span class="btn btn-clean btn-sm btn-icon mr-2" data-toggle="tooltip" data-placement="top" title="Mark as important">
                                    <i class="flaticon-add-label-button icon-1x"></i>
                                </span>
                                <span class="btn btn-clean btn-sm btn-icon mr-2" data-toggle="tooltip" data-placement="top" title="Reply">
                                    <i class="flaticon2-reply-1 icon-1x"></i>
                                </span>
                                <span class="btn btn-clean btn-sm btn-icon" data-toggle="tooltip" data-placement="top" title="Settings">
                                    <i class="flaticon-more icon-1x"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="card-spacer-x py-3 toggle-off-item">
                        <p>Hi Bob,</p>
                        <p>With resrpect, i must disagree with Mr.Zinsser. We all know the most part of important part of any article is the title.Without a compelleing title, your reader won't even get to the first sentence.After the title, however, the first few sentences of your article are certainly the most important part.</p>
                        <p>Jornalists call this critical, introductory section the "Lede," and when bridge properly executed, it's the that carries your reader from an headine try at attention-grabbing to the body of your blog post, if you want to get it right on of these 10 clever ways to omen your next blog posr with a bang</p>
                        <p>Best regards,</p>
                        <p>Jason Muller</p>
                    </div>
                </div>
            </div>
            <!--end::Messages-->
            <!--begin::Reply-->
            <div class="card-spacer mb-3" id="kt_inbox_reply">
                <div class="card card-custom shadow-sm">
                    <div class="card-body p-0">
                        <!--begin::Form-->
                        <form id="kt_inbox_reply_form">
                            <!--begin::Body-->
                            <div class="d-block">
                                <!--begin::To-->
                                <div class="d-flex align-items-center border-bottom inbox-to px-8 min-h-50px">
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
                                <div class="d-none align-items-center border-bottom inbox-to-cc pl-8 pr-5 min-h-50px">
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
                                <div class="d-none align-items-center border-bottom inbox-to-bcc pl-8 pr-5 min-h-50px">
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
                                <div id="kt_inbox_reply_editor" class="border-0" style="height: 250px"></div>
                                <!--end::Message-->
                                <!--begin::Attachments-->
                                <div class="dropzone dropzone-multi px-8 py-4" id="kt_inbox_reply_attachments">
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
                                    <span class="btn btn-icon btn-sm btn-clean mr-2" id="kt_inbox_reply_attachments_select">
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
            <!--end::Reply-->
        </div>
        <!--end::Body-->
    </div>
    <!--end::Card-->
</div>
<!--end::View-->
