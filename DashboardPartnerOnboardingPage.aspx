<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardPartnerOnboardingPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardPartnerOnboardingPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DANotificationControl.ascx" TagName="NotificationControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadAjaxManager ID="masterRadAjaxManager" runat="server" RestoreOriginalRenderDelegate="false">
    </telerik:RadAjaxManager>
    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--begin::Todo Files-->
                    <div class="d-flex flex-row">
                        <!--begin::Aside-->
                        <div class="flex-row-auto offcanvas-mobile w-200px w-xxl-275px" id="kt_todo_aside">
                            <!--begin::Card-->
                            <div class="card card-custom card-stretch" style="width: 290px;">
                                <!--begin::Body-->
                                <div class="card-body px-5">
                                    <!--begin:Nav-->
                                    <div class="navi navi-hover navi-active navi-link-rounded navi-bold navi-icon-center navi-light-icon">
                                        <!--begin:Item-->
                                        <div class="navi-item my-2">
                                            <a href="#" class="navi-link active">
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
                                                <span class="navi-text font-weight-bolder font-size-lg">Categories</span>
                                                <controls:NotificationControl ID="UcControl" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                        <!--begin:Item-->
                                        <div class="navi-item my-2">
                                            <a id="aTabProductUpdates" onserverclick="aProductUpdates_OnClick" runat="server" class="navi-link">
                                                <span class="navi-icon mr-4">
                                                    <i class="fa fa-genderless text-danger"></i>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">
                                                    <asp:Label ID="LblProductUpdates" Text="Product Updates" runat="server" />
                                                </span>
                                                <controls:NotificationControl ID="UcControl1" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                        <!--begin:Item-->
                                        <div class="navi-item my-2">
                                            <a id="aTabMarketingMaterial" onserverclick="aTabMarketingMaterial_OnClick" runat="server" class="navi-link">
                                                <span class="navi-icon mr-4">
                                                    <i class="fa fa-genderless text-danger"></i>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">
                                                    <asp:Label ID="LblMarketingMaterial" Text="Marketing Material" runat="server" />
                                                </span>
                                                <controls:NotificationControl ID="UcControl2" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                        <!--begin:Item-->
                                        <div class="navi-item my-2">
                                            <a id="aTabNewCampaign" onserverclick="aTabNewCampaign_OnClick" runat="server" class="navi-link">
                                                <span class="navi-icon mr-4">
                                                    <i class="fa fa-genderless text-danger"></i>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">
                                                    <asp:Label ID="LblNewCampaign" Text="New Campaign" runat="server" />
                                                </span>
                                                <controls:NotificationControl ID="UcControl3" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                        <!--begin:Item-->
                                        <div class="navi-item my-2">
                                            <a id="aTabNewsLetter" onserverclick="aTabNewsLetter_OnClick" runat="server" class="navi-link">
                                                <span class="navi-icon mr-4">
                                                    <i class="fa fa-genderless text-danger"></i>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">
                                                    <asp:Label ID="LblbNewsLetter" Text="NewsLetter Issue" runat="server" />
                                                </span>
                                                <controls:NotificationControl ID="UcControl4" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                        <!--begin:Item-->
                                        <div class="navi-item my-2">
                                            <a id="aTabBanner" onserverclick="aTabBanner_OnClick" runat="server" class="navi-link">
                                                <span class="navi-icon mr-4">
                                                    <i class="fa fa-genderless text-danger"></i>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">
                                                    <asp:Label ID="LblBanner" Text="Banners" runat="server" />
                                                </span>
                                                <controls:NotificationControl ID="UcControl5" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                        <!--begin:Item-->
                                        <div class="navi-item my-2">
                                            <a id="aTabDocumentationPdf" onserverclick="aTabDocumentationPdf_OnClick" runat="server" class="navi-link">
                                                <span class="navi-icon mr-4">
                                                    <i class="fa fa-genderless text-danger"></i>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">
                                                    <asp:Label ID="LblDocumentationPdf" Text="Documentation(Pdf)" runat="server" />
                                                </span>
                                                <controls:NotificationControl ID="UcControl6" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                        <!--begin:Item-->
                                        <div class="navi-item my-2">
                                            <a id="aTabEmailTemplate" onserverclick="aTabEmailTemplate_OnClick" runat="server" class="navi-link">
                                                <span class="navi-icon mr-4">
                                                    <i class="fa fa-genderless text-danger"></i>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">
                                                    <asp:Label ID="LblEmailTemplate" Text="Email Template" runat="server" />
                                                </span>
                                                <controls:NotificationControl ID="UcControl7" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                        <!--begin:Item-->
                                        <div class="navi-item my-2">
                                            <a id="aTabSalesMaterial" onserverclick="aTabSalesMaterial_OnClick" runat="server" class="navi-link">
                                                <span class="navi-icon mr-4">
                                                    <i class="fa fa-genderless text-danger"></i>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">
                                                    <asp:Label ID="LblSalesMaterial" Text="Sales Material" runat="server" />
                                                </span>
                                                <controls:NotificationControl ID="UcControl8" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                            </a>
                                        </div>
                                        <!--begin:Item-->
                                        <div class="navi-item my-2">
                                            <a id="aTabContacts" onserverclick="aTabContacts_OnClick" runat="server" class="navi-link">
                                                <span class="navi-icon mr-4">
                                                    <i class="fa fa-genderless text-danger"></i>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">
                                                    <asp:Label ID="LblContacts" Text="Contacts" runat="server" />
                                                </span>
                                                <controls:NotificationControl ID="UcControl9" MessageTypes="Success" IsBold="false" HasLeftMargin="false" HasRightMargin="false" IsNumber="true" IsLightColor="true" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                        <!--begin:Item-->
                                        <div class="navi-item my-2">
                                            <a id="aTabAll" onserverclick="aTabAll_ServerClick" runat="server" class="navi-link">
                                                <span class="navi-icon mr-4">
                                                    <i class="fa fa-genderless text-success"></i>
                                                </span>
                                                <span class="navi-text font-weight-bolder font-size-lg">
                                                    <asp:Label ID="LblAll" Text="All" runat="server" />
                                                </span>
                                                <controls:NotificationControl ID="UcControl10" runat="server" />
                                            </a>
                                        </div>
                                        <!--end:Item-->
                                    </div>
                                    <!--end:Nav-->
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Card-->
                        </div>
                        <!--end::Aside-->
                        <!--begin::List-->
                        <div class="flex-row-fluid d-flex flex-column ml-lg-8">
                            <div class="d-flex flex-column flex-grow-1" style="margin-left: 80px;">
                                <!--begin::Head-->
                                <div class="card card-custom gutter-b">
                                    <!--begin::Body-->
                                    <div class="card-body d-flex align-items-center justify-content-between flex-wrap py-3" style="padding: 2rem 1.75rem !important;">
                                        <div class="row">
                                            <div id="divVendorsList" visible="false" runat="server">
                                                <div class="form-group">
                                                    <div class="col-md-6" style="width: 65%;"></div>
                                                    <div class="col-md-6" style="width: 34%;">
                                                        <asp:DropDownList AutoPostBack="true" Width="400" ID="DrpVendors" OnSelectedIndexChanged="DrpVendors_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--begin::Info-->
                                        <div class="row">
                                            <div class="d-flex align-items-center mr-2 py-2">
                                                <!--begin::Navigation-->
                                                <div class="d-flex mr-0">
                                                    <!--begin::Navi-->
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                                        <ContentTemplate>
                                                            <div id="divVendorsUploadFilesArea" runat="server" class="navi navi-hover navi-active navi-link-rounded navi-bold d-flex flex-row">
                                                                <!--begin::Item-->
                                                                <div class="navi-item mr-2">
                                                                    <asp:DropDownList ID="Ddlcategory" Width="165" CssClass="form-control" runat="server" />
                                                                </div>
                                                                <!--end::Item-->
                                                                <!--begin::Item-->
                                                                <div class="navi-item mr-4" style="width: 255px;">
                                                                    <table>
                                                                        <tr id="tr1" runat="server">
                                                                            <td>
                                                                                <a id="ImgBtnAddLibraryFile" class="icon text-dark-50 flaticon2-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td></td>
                                                                            <td>
                                                                                <div id="divFileUpload" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                                                    <div class="">
                                                                                        <span>
                                                                                            <input type="file" name="uploadFile" id="inputFile" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                                                data-iconname="ion-image mr-5"
                                                                                                class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                                                runat="server" />
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr2" runat="server" visible="false">
                                                                            <td>
                                                                                <a id="ImgBtnAddLibraryFile2" class="icon text-dark-50 flaticon2-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <a id="ImgBtnDeleteLibraryFile2" class="icon text-dark-50 flaticon2-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divFileUpload2" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                                                    <div class="">
                                                                                        <span>
                                                                                            <input type="file" name="uploadFile" id="inputFile2" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                                                data-iconname="ion-image mr-5"
                                                                                                class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                                                runat="server" />
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr3" runat="server" visible="false">
                                                                            <td>
                                                                                <a id="ImgBtnAddLibraryFile3" class="icon text-dark-50 flaticon2-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <a id="ImgBtnDeleteLibraryFile3" class="icon text-dark-50 flaticon2-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divFileUpload3" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                                                    <div class="">
                                                                                        <span>
                                                                                            <input type="file" name="uploadFile" id="inputFile3" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                                                data-iconname="ion-image mr-5"
                                                                                                class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                                                runat="server" />
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr4" runat="server" visible="false">
                                                                            <td>
                                                                                <a id="ImgBtnAddLibraryFile4" class="icon text-dark-50 flaticon2-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <a id="ImgBtnDeleteLibraryFile4" class="icon text-dark-50 flaticon2-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divFileUpload4" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                                                    <div class="">
                                                                                        <span>
                                                                                            <input type="file" name="uploadFile" id="inputFile4" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                                                data-iconname="ion-image mr-5"
                                                                                                class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                                                runat="server" />
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr5" runat="server" visible="false">
                                                                            <td>
                                                                                <a id="ImgBtnAddLibraryFile5" class="icon text-dark-50 flaticon2-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <a id="ImgBtnDeleteLibraryFile5" class="icon text-dark-50 flaticon2-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divFileUpload5" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                                                    <div class="">
                                                                                        <span>
                                                                                            <input type="file" name="uploadFile" id="inputFile5" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                                                data-iconname="ion-image mr-5"
                                                                                                class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                                                runat="server" />
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr6" runat="server" visible="false">
                                                                            <td>
                                                                                <a id="ImgBtnAddLibraryFile6" class="icon text-dark-50 flaticon2-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <a id="ImgBtnDeleteLibraryFile6" class="icon text-dark-50 flaticon2-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divFileUpload6" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                                                    <div class="">
                                                                                        <span>
                                                                                            <input type="file" name="uploadFile" id="inputFile6" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                                                data-iconname="ion-image mr-5"
                                                                                                class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                                                runat="server" />
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr7" runat="server" visible="false">
                                                                            <td>
                                                                                <a id="ImgBtnAddLibraryFile7" class="icon text-dark-50 flaticon2-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <a id="ImgBtnDeleteLibraryFile7" class="icon text-dark-50 flaticon2-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divFileUpload7" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                                                    <div class="">
                                                                                        <span>
                                                                                            <input type="file" name="uploadFile" id="inputFile7" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                                                data-iconname="ion-image mr-5"
                                                                                                class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                                                runat="server" />
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr8" runat="server" visible="false">
                                                                            <td>
                                                                                <a id="ImgBtnAddLibraryFile8" class="icon text-dark-50 flaticon2-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <a id="ImgBtnDeleteLibraryFile8" class="icon text-dark-50 flaticon2-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divFileUpload8" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                                                    <div class="">
                                                                                        <span>
                                                                                            <input type="file" name="uploadFile" id="inputFile8" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                                                data-iconname="ion-image mr-5"
                                                                                                class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                                                runat="server" />
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr9" runat="server" visible="false">
                                                                            <td>
                                                                                <a id="ImgBtnAddLibraryFile9" class="icon text-dark-50 flaticon2-add" runat="server" onserverclick="ImgBtnAddLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <a id="ImgBtnDeleteLibraryFile9" class="icon text-dark-50 flaticon2-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divFileUpload9" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                                                    <div class="">
                                                                                        <span>
                                                                                            <input type="file" name="uploadFile" id="inputFile9" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                                                data-iconname="ion-image mr-5"
                                                                                                class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                                                runat="server" />
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr10" runat="server" visible="false">
                                                                            <td></td>
                                                                            <td>
                                                                                <a id="ImgBtnDeleteLibraryFile10" class="icon text-dark-50 flaticon2-delete" runat="server" onserverclick="ImgBtnDeleteLibraryFile_Click"></a>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divFileUpload10" class="fileinput fileinput-new" data-provides="fileinput" runat="server">
                                                                                    <div class="">
                                                                                        <span>
                                                                                            <input type="file" name="uploadFile" id="inputFile10" style="width: 225px !important;" data-buttonname="btn btn-light-primary"
                                                                                                data-iconname="ion-image mr-5"
                                                                                                class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .xlsm, .doc, .docx, .bmp, .txt, .avi, .mpeg4, .wmv, .flv, .wav, .mp3, .mp4, .avi, .mov, .mkv, .webm, .html5, audio/mpeg, audio, audio/mpeg4, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html, .rar, .zip, .tar"
                                                                                                runat="server" />
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <!--end::Item-->
                                                                <!--begin::Item-->
                                                                <div class="navi-item mr-2">
                                                                    <a id="BtnUploadFile" role="button" onserverclick="BtnUploadFile_OnCick" style="width: 110px;" runat="server" title="Upload/Save file" class="btn btn-light-primary px-6 font-weight-bold">Save
                                                                    </a>
                                                                </div>
                                                                <!--end::Item-->
                                                                <!--begin::Item-->
                                                                <div class="navi-item mr-2" style="width: 50px;">
                                                                </div>
                                                                <!--end::Item-->
                                                            </div>

                                                            <div id="divVideoUpload" visible="false" runat="server" class="navi navi-hover navi-active navi-link-rounded navi-bold d-flex flex-row">
                                                                <div class="row" style="margin-top: 20px;">
                                                                    <div class="col-lg-12">
                                                                        <!--begin::Item-->
                                                                        <div class="navi-item mr-2">
                                                                            <asp:TextBox ID="TbxVideoLink" CssClass="form-control" Width="635" placeholder="https://paste-your-video-link-here" runat="server" />
                                                                        </div>
                                                                        <!--end::Item-->
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="BtnUploadFile" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    <!--end::Navi-->
                                                </div>
                                                <!--end::Navigation-->
                                            </div>
                                        </div>
                                        <!--end::Info-->
                                    </div>
                                    <!--end::Body-->
                                </div>
                                <!--end::Head-->
                                <controls:MessageAlertControl ID="UcMessageAlert" runat="server" />

                                <div class="tab-content">
                                    <div class="tab-pane fade" id="tab_0" runat="server" visible="false" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <controls:MessageControl ID="UcNoPartnersMessageControl" runat="server" />
                                        <div class="col-xl-12">
                                            <div class="row">
                                                <div id="divInvitationToPartners" runat="server">
                                                    <a id="aInvitationToPartners" runat="server" class="btn btn-primary" style="color: #fff;">
                                                        <i class="ion-plus-round mr-5"></i>
                                                        <asp:Label ID="LblInvitationToPartners" Text="Invite your technology partners" runat="server" />
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <asp:Repeater ID="Rdg1" OnLoad="Rdg1_Load" OnItemDataBound="Rdg1_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <!--begin::Row-->
                                                <div class="row">
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete1" onserverclick="aDelete1_OnClick" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm">
                                                                            <%--<span class="svg-icon svg-icon-md svg-icon-primary">
                                                                                <!--begin::Svg Icon | path:assets/media/svg/icons/General/Trash.svg-->
                                                                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                        <rect x="0" y="0" width="24" height="24" />
                                                                                        <path d="M6,8 L6,20.5 C6,21.3284271 6.67157288,22 7.5,22 L16.5,22 C17.3284271,22 18,21.3284271 18,20.5 L18,8 L6,8 Z" fill="#000000" fill-rule="nonzero" />
                                                                                        <path d="M14,4.5 L14,4 C14,3.44771525 13.5522847,3 13,3 L11,3 C10.4477153,3 10,3.44771525 10,4 L10,4.5 L5.5,4.5 C5.22385763,4.5 5,4.72385763 5,5 L5,5.5 C5,5.77614237 5.22385763,6 5.5,6 L18.5,6 C18.7761424,6 19,5.77614237 19,5.5 L19,5 C19,4.72385763 18.7761424,4.5 18.5,4.5 L14,4.5 Z" fill="#000000" opacity="0.3" />
                                                                                    </g>
                                                                                </svg>
                                                                                <!--end::Svg Icon-->
                                                                            </span>--%>
                                                                        </a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                        <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a1" onserverclick="HpLnkFile1_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete2" onserverclick="aDelete2_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                        <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete3" onserverclick="aDelete3_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                        <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete4" onserverclick="aDelete4_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                        <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                </div>
                                                <!--end::Row-->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcRgd1" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <asp:Repeater ID="Rdg2" OnLoad="Rdg2_Load" OnItemDataBound="Rdg2_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <!--begin::Row-->
                                                <div class="row">
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete1" onserverclick="aDelete1_OnClick" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                        <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a1" onserverclick="HpLnkFile1_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete2" onserverclick="aDelete2_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                        <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete3" onserverclick="aDelete3_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                        <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete4" onserverclick="aDelete4_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                        <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                </div>
                                                <!--end::Row-->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcRgd2" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_3" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <asp:Repeater ID="Rdg3" OnLoad="Rdg3_Load" OnItemDataBound="Rdg3_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <!--begin::Row-->
                                                <div class="row">
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete1" onserverclick="aDelete1_OnClick" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                        <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a1" onserverclick="HpLnkFile1_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete2" onserverclick="aDelete2_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                        <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete3" onserverclick="aDelete3_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                        <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete4" onserverclick="aDelete4_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                        <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                </div>
                                                <!--end::Row-->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcRgd3" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_4" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <asp:Repeater ID="Rdg4" OnLoad="Rdg4_Load" OnItemDataBound="Rdg4_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <!--begin::Row-->
                                                <div class="row">
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete1" onserverclick="aDelete1_OnClick" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                        <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a1" onserverclick="HpLnkFile1_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete2" onserverclick="aDelete2_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                        <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete3" onserverclick="aDelete3_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                        <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete4" onserverclick="aDelete4_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                        <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                </div>
                                                <!--end::Row-->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcRgd4" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_5" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <asp:Repeater ID="Rdg5" OnLoad="Rdg5_Load" OnItemDataBound="Rdg5_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <!--begin::Row-->
                                                <div class="row">
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete1" onserverclick="aDelete1_OnClick" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                        <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a1" onserverclick="HpLnkFile1_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete2" onserverclick="aDelete2_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                        <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete3" onserverclick="aDelete3_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                        <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete4" onserverclick="aDelete4_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                        <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                </div>
                                                <!--end::Row-->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcRgd5" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_6" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <asp:Repeater ID="Rdg6" OnLoad="Rdg6_Load" OnItemDataBound="Rdg6_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <!--begin::Row-->
                                                <div class="row">
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete1" onserverclick="aDelete1_OnClick" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                        <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a1" onserverclick="HpLnkFile1_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete2" onserverclick="aDelete2_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                        <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete3" onserverclick="aDelete3_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                        <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete4" onserverclick="aDelete4_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                        <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                </div>
                                                <!--end::Row-->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcRgd6" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_7" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <asp:Repeater ID="Rdg7" OnLoad="Rdg7_Load" OnItemDataBound="Rdg7_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <!--begin::Row-->
                                                <div class="row">
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete1" onserverclick="aDelete1_OnClick" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                        <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a1" onserverclick="HpLnkFile1_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete2" onserverclick="aDelete2_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                        <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete3" onserverclick="aDelete3_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                        <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete4" onserverclick="aDelete4_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                        <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                </div>
                                                <!--end::Row-->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcRgd7" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_8" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <asp:Repeater ID="Rdg8" OnLoad="Rdg8_Load" OnItemDataBound="Rdg8_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <!--begin::Row-->
                                                <div class="row">
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete1" onserverclick="aDelete1_OnClick" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                        <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a1" onserverclick="HpLnkFile1_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete2" onserverclick="aDelete2_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                        <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete3" onserverclick="aDelete3_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                        <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete4" onserverclick="aDelete4_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                        <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                </div>
                                                <!--end::Row-->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcRgd8" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_9" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <asp:Repeater ID="Rdg9" OnLoad="Rdg9_Load" OnItemDataBound="Rdg9_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <!--begin::Row-->
                                                <div class="row">
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete1" onserverclick="aDelete1_OnClick" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                        <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a1" onserverclick="HpLnkFile1_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete2" onserverclick="aDelete2_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                        <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete3" onserverclick="aDelete3_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                        <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete4" onserverclick="aDelete4_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                        <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                </div>
                                                <!--end::Row-->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcRgd9" runat="server" />
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_10" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <asp:Repeater ID="Rdg10" OnLoad="Rdg10_Load" OnItemDataBound="Rdg10_ItemDataBound" runat="server">
                                            <ItemTemplate>
                                                <!--begin::Row-->
                                                <div class="row">
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete1" onserverclick="aDelete1_OnClick" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                        <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a1" onserverclick="HpLnkFile1_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_name1")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete2" onserverclick="aDelete2_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                        <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_name2")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete3" onserverclick="aDelete3_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                        <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_name3")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                    <!--begin::Col-->
                                                    <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                        <!--begin::Card-->
                                                        <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                            <div class="card-header border-0">
                                                                <h3 class="card-title"></h3>
                                                                <div class="card-toolbar">
                                                                    <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                    <div class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                        <a id="aDelete4" onserverclick="aDelete4_OnClick" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="d-flex flex-column align-items-center">
                                                                    <!--begin: Icon-->
                                                                    <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                        <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                    </a>
                                                                    <!--end: Icon-->
                                                                    <!--begin: Tite-->
                                                                    <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" title='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                        <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_name4")%>' runat="server" />
                                                                    </a>
                                                                    <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                    <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                    <!--end: Tite-->
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--end:: Card-->
                                                    </div>
                                                    <!--end::Col-->
                                                </div>
                                                <!--end::Row-->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <controls:MessageControl ID="UcRgd10" runat="server" />
                                    </div>
                                </div>

                            </div>
                        </div>
                        <!--end::List-->
                    </div>
                    <!--end::Todo Files-->
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
                                <asp:Label ID="LblFileUploadTitle" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <controls:MessageControl ID="UploadMessageAlert" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-light-primary" data-dismiss="modal">Close</button>
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
                                <asp:Label ID="LblConfTitle" Text="Confirmation" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <controls:MessageControl ID="UcConfirmationMessageControl" runat="server" />
                            </div>
                            <div class="row" style="margin-top: 25px;">
                                <div id="divSuccess" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblSuccess" Text="Done!" runat="server" />
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
                                <div id="divFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-questions-circular-button"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblFailure" Text="Error!" runat="server" />
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
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-light-primary">Close</button>
                            <asp:Button ID="BtnDeleteFile" OnClick="BtnDeleteFile_OnClick" Text="Delete" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Confirmation Delete Partner form (modal view) -->
    <div id="divVideoConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="Label1" Text="Confirmation" CssClass="control-label" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="width: 100%;">
                                <asp:Label ID="Label2" Text="Are you sure you want to delete this video file from your library?" CssClass="control-label" runat="server" />
                            </div>
                            <div class="row" style="margin-top: 25px;">
                                <div id="div2" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-questions-circular-button"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="Label3" Text="Error! " runat="server" />
                                        </strong>
                                        <asp:Label ID="Label4" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">
                                                <i class="ki ki-close"></i>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                                <div id="div3" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="Label5" Text="Done! " runat="server" />
                                        </strong>
                                        <asp:Label ID="Label6" runat="server" />
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
                            <asp:Button ID="BtnVideoDeleteFile" OnClick="BtnVideoDeleteFile_OnClick" Text="Delete" CssClass="btn btn-primary" runat="server" />
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
        </script>

    </telerik:RadScriptBlock>

</asp:Content>
