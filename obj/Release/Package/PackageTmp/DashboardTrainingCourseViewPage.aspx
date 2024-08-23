<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardTrainingCourseViewPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardTrainingCourseViewPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">

        <!--begin::Entry-->
        <div class="d-flex flex-column-fluid">
            <!--begin::Container-->
            <div class="container">
                <asp:UpdatePanel runat="server" ID="UpdatePanelContent">
                    <ContentTemplate>
                        <div id="fade-class" class="card card-custom">
                            <div class="card-body p-0">
                                <!--begin: Wizard-->
                                <div class="wizard wizard-2" id="kt_wizard_v2" data-wizard-state="step-first" data-wizard-clickable="false">
                                    <!--begin: Wizard Nav-->
                                    <div class="wizard-nav border-right py-8 px-8 py-lg-20 px-lg-10">
                                        <!--begin::Wizard Step 1 Nav-->
                                        <div class="wizard-steps">
                                            <div id="divMStep1" runat="server" class="wizard-step" data-wizard-type="step">
                                                <div class="wizard-wrapper">
                                                    <div class="wizard-icon">
                                                        <span class="svg-icon svg-icon-2x">
                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/General/User.svg-->
                                                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                    <polygon points="0 0 24 0 24 24 0 24" />
                                                                    <path d="M12,11 C9.790861,11 8,9.209139 8,7 C8,4.790861 9.790861,3 12,3 C14.209139,3 16,4.790861 16,7 C16,9.209139 14.209139,11 12,11 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                    <path d="M3.00065168,20.1992055 C3.38825852,15.4265159 7.26191235,13 11.9833413,13 C16.7712164,13 20.7048837,15.2931929 20.9979143,20.2 C21.0095879,20.3954741 20.9979143,21 20.2466999,21 C16.541124,21 11.0347247,21 3.72750223,21 C3.47671215,21 2.97953825,20.45918 3.00065168,20.1992055 Z" fill="#000000" fill-rule="nonzero" />
                                                                </g>
                                                            </svg>
                                                            <!--end::Svg Icon-->
                                                        </span>
                                                    </div>
                                                    <div class="wizard-label">
                                                        <h3 class="wizard-title">Training Category</h3>
                                                        <div class="wizard-desc">Add new training category</div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end::Wizard Step 1 Nav-->
                                            <!--begin::Wizard Step 2 Nav-->
                                            <div id="divMStep2" runat="server" class="wizard-step" data-wizard-type="step">
                                                <div class="wizard-wrapper">
                                                    <div class="wizard-icon">
                                                        <span class="svg-icon svg-icon-2x">
                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/Map/Compass.svg-->
                                                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                    <rect x="0" y="0" width="24" height="24" />
                                                                    <path d="M12,21 C7.02943725,21 3,16.9705627 3,12 C3,7.02943725 7.02943725,3 12,3 C16.9705627,3 21,7.02943725 21,12 C21,16.9705627 16.9705627,21 12,21 Z M14.1654881,7.35483745 L9.61055177,10.3622525 C9.47921741,10.4489666 9.39637436,10.592455 9.38694497,10.7495509 L9.05991526,16.197949 C9.04337012,16.4735952 9.25341309,16.7104632 9.52905936,16.7270083 C9.63705011,16.7334903 9.74423017,16.7047714 9.83451193,16.6451626 L14.3894482,13.6377475 C14.5207826,13.5510334 14.6036256,13.407545 14.613055,13.2504491 L14.9400847,7.80205104 C14.9566299,7.52640477 14.7465869,7.28953682 14.4709406,7.27299168 C14.3629499,7.26650974 14.2557698,7.29522855 14.1654881,7.35483745 Z" fill="#000000" />
                                                                </g>
                                                            </svg>
                                                            <!--end::Svg Icon-->
                                                        </span>
                                                    </div>
                                                    <div class="wizard-label">
                                                        <h3 class="wizard-title">Category Courses</h3>
                                                        <div class="wizard-desc">Add courses to category</div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end::Wizard Step 2 Nav-->
                                            <!--begin::Wizard Step 3 Nav-->
                                            <div id="divMStep3" runat="server" class="wizard-step" data-wizard-type="step">
                                                <div class="wizard-wrapper">
                                                    <div class="wizard-icon">
                                                        <span class="svg-icon svg-icon-2x">
                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/General/Thunder-move.svg-->
                                                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                    <rect x="0" y="0" width="24" height="24" />
                                                                    <path d="M16.3740377,19.9389434 L22.2226499,11.1660251 C22.4524142,10.8213786 22.3592838,10.3557266 22.0146373,10.1259623 C21.8914367,10.0438285 21.7466809,10 21.5986122,10 L17,10 L17,4.47708173 C17,4.06286817 16.6642136,3.72708173 16.25,3.72708173 C15.9992351,3.72708173 15.7650616,3.85240758 15.6259623,4.06105658 L9.7773501,12.8339749 C9.54758575,13.1786214 9.64071616,13.6442734 9.98536267,13.8740377 C10.1085633,13.9561715 10.2533191,14 10.4013878,14 L15,14 L15,19.5229183 C15,19.9371318 15.3357864,20.2729183 15.75,20.2729183 C16.0007649,20.2729183 16.2349384,20.1475924 16.3740377,19.9389434 Z" fill="#000000" />
                                                                    <path d="M4.5,5 L9.5,5 C10.3284271,5 11,5.67157288 11,6.5 C11,7.32842712 10.3284271,8 9.5,8 L4.5,8 C3.67157288,8 3,7.32842712 3,6.5 C3,5.67157288 3.67157288,5 4.5,5 Z M4.5,17 L9.5,17 C10.3284271,17 11,17.6715729 11,18.5 C11,19.3284271 10.3284271,20 9.5,20 L4.5,20 C3.67157288,20 3,19.3284271 3,18.5 C3,17.6715729 3.67157288,17 4.5,17 Z M2.5,11 L6.5,11 C7.32842712,11 8,11.6715729 8,12.5 C8,13.3284271 7.32842712,14 6.5,14 L2.5,14 C1.67157288,14 1,13.3284271 1,12.5 C1,11.6715729 1.67157288,11 2.5,11 Z" fill="#000000" opacity="0.3" />
                                                                </g>
                                                            </svg>
                                                            <!--end::Svg Icon-->
                                                        </span>
                                                    </div>
                                                    <div class="wizard-label">
                                                        <h3 class="wizard-title">Permissions</h3>
                                                        <div class="wizard-desc">Add partner permissions</div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end::Wizard Step 3 Nav-->
                                            <!--begin::Wizard Step 3 Nav-->
                                            <div id="divMStep4" runat="server" class="wizard-step" data-wizard-type="step">
                                                <div class="wizard-wrapper">
                                                    <div class="wizard-icon">
                                                        <span class="svg-icon svg-icon-2x">
                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/General/Thunder-move.svg-->
                                                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                    <rect x="0" y="0" width="24" height="24" />
                                                                    <path d="M16.3740377,19.9389434 L22.2226499,11.1660251 C22.4524142,10.8213786 22.3592838,10.3557266 22.0146373,10.1259623 C21.8914367,10.0438285 21.7466809,10 21.5986122,10 L17,10 L17,4.47708173 C17,4.06286817 16.6642136,3.72708173 16.25,3.72708173 C15.9992351,3.72708173 15.7650616,3.85240758 15.6259623,4.06105658 L9.7773501,12.8339749 C9.54758575,13.1786214 9.64071616,13.6442734 9.98536267,13.8740377 C10.1085633,13.9561715 10.2533191,14 10.4013878,14 L15,14 L15,19.5229183 C15,19.9371318 15.3357864,20.2729183 15.75,20.2729183 C16.0007649,20.2729183 16.2349384,20.1475924 16.3740377,19.9389434 Z" fill="#000000" />
                                                                    <path d="M4.5,5 L9.5,5 C10.3284271,5 11,5.67157288 11,6.5 C11,7.32842712 10.3284271,8 9.5,8 L4.5,8 C3.67157288,8 3,7.32842712 3,6.5 C3,5.67157288 3.67157288,5 4.5,5 Z M4.5,17 L9.5,17 C10.3284271,17 11,17.6715729 11,18.5 C11,19.3284271 10.3284271,20 9.5,20 L4.5,20 C3.67157288,20 3,19.3284271 3,18.5 C3,17.6715729 3.67157288,17 4.5,17 Z M2.5,11 L6.5,11 C7.32842712,11 8,11.6715729 8,12.5 C8,13.3284271 7.32842712,14 6.5,14 L2.5,14 C1.67157288,14 1,13.3284271 1,12.5 C1,11.6715729 1.67157288,11 2.5,11 Z" fill="#000000" opacity="0.3" />
                                                                </g>
                                                            </svg>
                                                            <!--end::Svg Icon-->
                                                        </span>
                                                    </div>
                                                    <div class="wizard-label">
                                                        <h3 class="wizard-title">Courses Chapters</h3>
                                                        <div class="wizard-desc">Add chapters to courses</div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end::Wizard Step 3 Nav-->
                                            <!--begin::Wizard Step 6 Nav-->
                                            <div id="divMStep5" runat="server" class="wizard-step" data-wizard-type="step">
                                                <div class="wizard-wrapper">
                                                    <div class="wizard-icon">
                                                        <span class="svg-icon svg-icon-2x">
                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/General/Like.svg-->
                                                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                    <rect x="0" y="0" width="24" height="24" />
                                                                    <path d="M9,10 L9,19 L10.1525987,19.3841996 C11.3761964,19.7920655 12.6575468,20 13.9473319,20 L17.5405883,20 C18.9706314,20 20.2018758,18.990621 20.4823303,17.5883484 L21.231529,13.8423552 C21.5564648,12.217676 20.5028146,10.6372006 18.8781353,10.3122648 C18.6189212,10.260422 18.353992,10.2430672 18.0902299,10.2606513 L14.5,10.5 L14.8641964,6.49383981 C14.9326895,5.74041495 14.3774427,5.07411874 13.6240179,5.00562558 C13.5827848,5.00187712 13.5414031,5 13.5,5 L13.5,5 C12.5694044,5 11.7070439,5.48826024 11.2282564,6.28623939 L9,10 Z" fill="#000000" />
                                                                    <rect fill="#000000" opacity="0.3" x="2" y="9" width="5" height="11" rx="1" />
                                                                </g>
                                                            </svg>
                                                            <!--end::Svg Icon-->
                                                        </span>
                                                    </div>
                                                    <div class="wizard-label">
                                                        <h3 class="wizard-title">Category Overview!</h3>
                                                        <div class="wizard-desc">Overview and Submit new category</div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end::Wizard Step 6 Nav-->
                                        </div>
                                    </div>
                                    <!--end: Wizard Nav-->
                                    <!--begin: Wizard Body-->
                                    <div class="wizard-body py-8 px-8 py-lg-20 px-lg-10">
                                        <!--begin: Wizard Form-->
                                        <div class="row">
                                            <div class="offset-xxl-2 col-xxl-8">
                                                <div class="form" id="kt_form">
                                                    <!--begin: Wizard Step 1-->
                                                    <div id="divStep1" runat="server" class="pb-5" data-wizard-type="step-content">
                                                        <h4 class="mb-10 font-weight-bold text-dark">Enter new Category Details for your Training</h4>
                                                        <!--begin::Input-->
                                                        <div id="divExistingCategories" visible="false" runat="server" class="form-group">
                                                            <label>Your existing Categories</label>
                                                            <asp:DropDownList ID="DrpCategories" AutoPostBack="true" OnSelectedIndexChanged="DrpCategories_SelectedIndexChanged" CssClass="form-control form-control-solid form-control-lg" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div id="divNewCategory" runat="server">
                                                            <div class="form-group">
                                                                <label>Add New Category</label>
                                                                <asp:TextBox ID="TxtCategoryName" runat="server" CssClass="form-control form-control-solid form-control-lg" placeholder="Category Description" />
                                                            </div>
                                                            <%--<div class="row">
                                                            <div class="col-12">
                                                                <a id="a1" onserverclick="BtnAddCategory_Click" class="btn btn-success px-6 font-weight-bold flaticon-envelope" runat="server">
                                                                    <asp:Label ID="Label9" Text="Add New Category" runat="server" />
                                                                </a>
                                                            </div>
                                                        </div>--%>
                                                        </div>
                                                    </div>
                                                    <!--end: Wizard Step 1-->
                                                    <!--begin: Wizard Step 2-->
                                                    <div id="divStep2" runat="server" visible="false" class="pb-5" data-wizard-type="step-content">
                                                        <h4 class="mb-10 font-weight-bold text-dark">Setup Courses for new Category</h4>

                                                        <div class="form-group">
                                                            <label>Course Name</label>
                                                            <asp:TextBox ID="TxtCourseName" runat="server" CssClass="form-control form-control-solid form-control-lg" placeholder="Course Name" />
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Course Overview Text</label>
                                                            <asp:TextBox ID="TxtCourseOverview" Rows="5" runat="server" CssClass="form-control form-control-solid form-control-lg" TextMode="MultiLine" placeholder="Course Overview Text" />
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-6">
                                                                <!--begin::Input-->
                                                                <div class="form-group">
                                                                    <label>Course Overview Image</label>
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                                                        <ContentTemplate>
                                                                            <div class="form-group">
                                                                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                                    <div class="fileinput-new thumbnail" style="width: 220px; height: 220px;">
                                                                                        <asp:Image ID="ImgPhotoBckgr" ImageUrl="https://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image" Height="220" Width="220" AlternateText="Update logo" runat="server" />
                                                                                    </div>
                                                                                    <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 220px; max-height: 220px;"></div>
                                                                                    <div>
                                                                                        <span class="btn default btn-file">
                                                                                            <span class="fileinput-new">
                                                                                                <asp:Label ID="LblSelectImg" class="btn btn-success" Text="Select image" runat="server" /></span>
                                                                                            <span class="fileinput-exists">
                                                                                                <asp:Label ID="LblChangeImg" Text="Change" class="btn btn-primary" runat="server" /></span>
                                                                                            <input type="file" enableviewstate="true" name="ImgCoursePreview" id="CoursePreview" runat="server" accept=".png, .jpg, .jpeg" />
                                                                                        </span>
                                                                                        <a class="btn default fileinput-exists" data-dismiss="fileinput">
                                                                                            <asp:Label ID="LblRemoveImg" Text="Remove" class="btn btn-danger" runat="server" /></a>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div id="divSubmitLogo" runat="server" class="margin-top-10">
                                                                                <a id="aAddCourse" onserverclick="BtnAddCourse_Click" class="btn btn-success px-6 font-weight-bold flaticon-envelope" runat="server">
                                                                                    <asp:Label ID="LblAddCourse" Text="Add Course" runat="server" />
                                                                                </a>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="aAddCourse" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <!--end::Input-->
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <!--begin::Input-->

                                                                <!--end::Input-->
                                                            </div>
                                                        </div>
                                                        <div class="row mt-10">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-md-9">
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:CheckBoxList ID="CbxUserCoursesList" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <div class="form-group mt-10">
                                                                    <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                                        <asp:Repeater ID="RdgCourses" OnItemDataBound="RdgCourses_ItemDataBound" runat="server">
                                                                            <HeaderTemplate>
                                                                                <thead>
                                                                                    <tr>
                                                                                        <%--<th>
                                                                                        <asp:Label ID="Label1" runat="server" Text="Select" />
                                                                                    </th>--%>
                                                                                        <th>
                                                                                            <asp:Label ID="Label2" runat="server" Text="Course Name" />
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="Label3" runat="server" Text="Preview Image" />
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="Label5" runat="server" Text="Preview Text" />
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="Label12" runat="server" Text="Actions" />
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tbody style="width: 100%;">
                                                                                    <tr>
                                                                                        <%--<td>
                                                                                        <div style="">
                                                                                            <asp:CheckBox AutoPostBack="true" OnCheckedChanged="CbxSelectCourse_CheckedChanged" ID="CbxSelectCourse" runat="server" />
                                                                                        </div>
                                                                                    </td>--%>
                                                                                        <td>
                                                                                            <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                <asp:Label ID="LblCourseName" Text='<%# DataBinder.Eval(Container.DataItem, "CourseDescription")%>' runat="server" />
                                                                                            </span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="symbol symbol-30 symbol-light">
                                                                                                <span class="symbol-label">
                                                                                                    <asp:Image ID="ImgPreview" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "OverviewImagePath")%>' runat="server" class="h-50 align-self-center" alt="" />
                                                                                                </span>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                <asp:Label ID="LblPreviewText" Text='<%# DataBinder.Eval(Container.DataItem, "OverviewText")%>' runat="server" />
                                                                                            </span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <span class="symbol-label">
                                                                                                <a id="aDeleteCourse" onserverclick="aDeleteCourse_ServerClick" runat="server" title="Delete course" class="navi-link">
                                                                                                    <span class="navi-icon">
                                                                                                        <i class="flaticon-delete"></i>
                                                                                                    </span>
                                                                                                </a>
                                                                                            </span>
                                                                                        </td>
                                                                                        <asp:HiddenField ID="HdnCourseId" Value='<%# Eval("CourseId") %>' runat="server" />
                                                                                    </tr>
                                                                                </tbody>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end: Wizard Step 2-->
                                                    <!--begin: Wizard Step 3-->
                                                    <div id="divStep3" runat="server" visible="false" class="pb-5" data-wizard-type="step-content">
                                                        <h4 class="mb-10 font-weight-bold text-dark">Setup partner permissions</h4>

                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <div class="form-group">
                                                                    <label>Select what Tier can see this course:</label>
                                                                    <asp:DropDownList ID="DdlTiers" CssClass="form-control form-control-solid form-control-lg" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <span id="spanAlert" runat="server" visible="false" class="btn btn-sm btn-text btn-light-danger font-weight-bold">
                                                                    <asp:Label ID="LblTierDescription" Text="You have no Tiers setup yet" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <label>Select what Group of Partners can see this course:</label>
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="DrpGroupParnters" CssClass="form-control form-control-solid form-control-lg" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <a id="aCreateGroup" onserverclick="aCreateGroup_ServerClick" class="btn btn-success px-6 font-weight-bold flaticon-users" runat="server">
                                                                        <asp:Label ID="LblCreateGroup" Text="Create group" runat="server" />
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <label>Select what Countries:</label>
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="DrpCountries" CssClass="form-control form-control-solid form-control-lg" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-6">
                                                                <div class="form-group">
                                                                    <a id="aAddCountry" onserverclick="BtnAddCountry_Click" class="btn btn-success px-6 font-weight-bold flaticon-add-circular-button" runat="server">
                                                                        <asp:Label ID="LblAddCountry" Text="Add country" runat="server" />
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-11">
                                                                <div class="form-group">
                                                                    <label>Selected Countries</label>
                                                                    <asp:TextBox ID="TxtSelectedCountries" runat="server" CssClass="form-control form-control-solid form-control-lg" placeholder="selected countries" />
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-1">
                                                                <div class="form-group" style="padding-bottom: 15px;"></div>
                                                                <a id="aDeleteCountry" onserverclick="aDeleteCountry_ServerClick" runat="server" title="Delete country" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="flaticon-delete"></i>
                                                                    </span>
                                                                </a>
                                                            </div>
                                                        </div>
                                                        <div class="row mt-10">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-md-9">
                                                                        <a id="aAddCoursePermissions" onserverclick="aAddCoursePermissions_ServerClick" class="btn btn-success px-6 font-weight-bold" runat="server">
                                                                            <asp:Label ID="LblAddCoursePermissions" Text="Add Permissions" runat="server" />
                                                                        </a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end: Wizard Step 3-->
                                                    <!--begin: Wizard Step 4-->
                                                    <div id="divStep4" runat="server" visible="false" class="pb-5" data-wizard-type="step-content">
                                                        <h4 class="mb-10 font-weight-bold text-dark">Setup Chapters to Course</h4>

                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <div class="form-group">
                                                                    <label>Chapter Title</label>
                                                                    <asp:TextBox ID="TxtChaTitle" runat="server" CssClass="form-control form-control-solid form-control-lg" placeholder="Chapter Title" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <div class="form-group">
                                                                    <label>Chapter Text</label>
                                                                    <asp:TextBox ID="TxtChaContent" runat="server" CssClass="form-control form-control-solid form-control-lg" placeholder="Some text as content..." TextMode="MultiLine" Rows="5" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <div class="form-group">
                                                                    <label>Chapter Link</label>
                                                                    <asp:TextBox ID="TxtChaLink" runat="server" CssClass="form-control form-control-solid form-control-lg" placeholder="https://view-online-video-link.com" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <div class="form-group">
                                                                    <label>Chapter Upload File</label>
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                                        <ContentTemplate>
                                                                            <div class="form-group">
                                                                                <div class="fileinput-new thumbnail" style="width: 220px; height: 220px;">
                                                                                    <asp:Image ID="Image1" ImageUrl="https://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no + file" Height="220" Width="220" AlternateText="Update logo" runat="server" />
                                                                                </div>
                                                                                <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 220px; max-height: 220px;"></div>
                                                                                <div>
                                                                                    <span class="btn default btn-file">
                                                                                        <span class="fileinput-new">
                                                                                            <asp:Label ID="Label9" class="btn btn-success" Text="Select file" runat="server" /></span>
                                                                                        <span class="fileinput-exists">
                                                                                            <asp:Label ID="Label10" Text="Change" Visible="false" class="btn btn-primary" runat="server" /></span>
                                                                                        <input type="file" enableviewstate="true" name="ImgChapterFile" id="ChapterFile" runat="server" accept=".doc, .pdf, .xls, .docx, .ods" />
                                                                                    </span>
                                                                                    <a class="btn default fileinput-exists" data-dismiss="fileinput">
                                                                                        <asp:Label ID="Label11" Text="Remove" Visible="false" class="btn btn-danger" runat="server" /></a>
                                                                                </div>
                                                                            </div>
                                                                            <div id="div1" runat="server" class="margin-top-10">
                                                                                <a id="aAddchapter" onserverclick="BtnAddchapter_Click" class="btn btn-danger font-weight-bold text-uppercase px-9 py-4" runat="server">
                                                                                    <asp:Label ID="LblAddchapter" Text="Add Chapter" runat="server" />
                                                                                </a>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="aAddchapter" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-xl-12">
                                                                <div class="form-group mt-10">
                                                                    <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                                        <asp:Repeater ID="RdgChapter" OnItemDataBound="RdgChapter_ItemDataBound" runat="server">
                                                                            <HeaderTemplate>
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>
                                                                                            <asp:Label ID="Label3" runat="server" Text="Chapter Title" />
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="Label5" runat="server" Text="Chapter Text" />
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="Label4" runat="server" Text="Chapter Link" />
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="Label6" runat="server" Text="Chapter File" />
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tbody style="width: 100%;">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                <asp:Label ID="LblChapterTitle" Text='<%# DataBinder.Eval(Container.DataItem, "ChapterTitle")%>' runat="server" />
                                                                                            </span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                <asp:Label ID="LblChapterText" Text='<%# DataBinder.Eval(Container.DataItem, "ChapterText")%>' runat="server" />
                                                                                            </span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <a id="aChapterLink" runat="server" target="_blank" href='<%# DataBinder.Eval(Container.DataItem, "ChapterLink")%>' class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                                                <asp:Label ID="LblChapterLink" Text="Chapter link" runat="server" Style="cursor: pointer; font-style: italic;" />
                                                                                            </a>
                                                                                        </td>
                                                                                        <td>
                                                                                            <a id="aChapterFilePath" runat="server" target="_blank" href='<%# DataBinder.Eval(Container.DataItem, "ChapterFilePath")%>' class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                    <asp:Label ID="LblChapterFileName" Text='<%# DataBinder.Eval(Container.DataItem, "ChapterFileName")%>' runat="server" />
                                                                                                </span>
                                                                                            </a>
                                                                                        </td>
                                                                                        <asp:HiddenField ID="HdnCourseId" Value='<%# Eval("ChapterId") %>' runat="server" />
                                                                                        <asp:HiddenField ID="HdnCourserId" Value='<%# Eval("CourseId") %>' runat="server" />
                                                                                    </tr>
                                                                                </tbody>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end: Wizard Step 4-->
                                                    <!--begin: Wizard Step 5-->
                                                    <div id="divStep5" runat="server" visible="false" class="pb-5" data-wizard-type="step-content">
                                                        <asp:Panel ID="PnlStep5" runat="server">
                                                            <!--begin::Section-->
                                                            <h4 class="mb-10 font-weight-bold text-dark">Overview New Training Category Details and Submit</h4>
                                                            <h6 class="font-weight-bolder mb-3">Category Name:</h6>
                                                            <div class="text-dark-50 line-height-lg">
                                                                <div>
                                                                    <asp:Label ID="LblCategoryName" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="separator separator-dashed my-5"></div>
                                                            <!--end::Section-->
                                                            <!--begin::Section-->
                                                            <h6 class="font-weight-bolder mb-3">Category Courses:</h6>
                                                            <div class="text-dark-50 line-height-lg">
                                                                <div class="row">
                                                                    <div class="col-xl-12">
                                                                        <div class="form-group mt-10">
                                                                            <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                                                <asp:Repeater ID="RdgCoursesOverview" OnItemDataBound="RdgCoursesOverview_ItemDataBound" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label2" runat="server" Text="Course Name" />
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label3" runat="server" Text="Preview Image" />
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label5" runat="server" Text="Preview Text" />
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tbody style="width: 100%;">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                        <asp:Label ID="LblCourseName" Text='<%# DataBinder.Eval(Container.DataItem, "CourseDescription")%>' runat="server" />
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <div class="symbol symbol-30 symbol-light">
                                                                                                        <span class="symbol-label">
                                                                                                            <asp:Image ID="ImgPreview" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "OverviewImagePath")%>' runat="server" class="h-50 align-self-center" alt="" />
                                                                                                        </span>
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                        <asp:Label ID="LblPreviewText" Text='<%# DataBinder.Eval(Container.DataItem, "OverviewText")%>' runat="server" />
                                                                                                    </span>
                                                                                                </td>
                                                                                                <asp:HiddenField ID="HdnCategoryId" Value='<%# Eval("CourseId") %>' runat="server" />
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="separator separator-dashed my-5"></div>
                                                            <!--end::Section-->
                                                            <!--begin::Section-->
                                                            <h6 class="font-weight-bolder mb-3">Courses Chapters:</h6>
                                                            <div class="text-dark-50 line-height-lg">
                                                                <div class="row">
                                                                    <div class="col-xl-12">
                                                                        <div class="form-group mt-10">
                                                                            <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                                                <asp:Repeater ID="RdgChapterOverview" OnItemDataBound="RdgChapterOverview_ItemDataBound" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label7" runat="server" Text="Course Name" />
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label3" runat="server" Text="Chapter Title" />
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label5" runat="server" Text="Chapter Text" />
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label4" runat="server" Text="Chapter Link" />
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label6" runat="server" Text="Chapter File" />
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tbody style="width: 100%;">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                        <asp:Label ID="Label8" Text='<%# DataBinder.Eval(Container.DataItem, "CourseDescription")%>' runat="server" />
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                        <asp:Label ID="LblChapterTitle" Text='<%# DataBinder.Eval(Container.DataItem, "ChapterTitle")%>' runat="server" />
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                        <asp:Label ID="LblChapterText" Text='<%# DataBinder.Eval(Container.DataItem, "ChapterText")%>' runat="server" />
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <a id="aChapterLink" runat="server" target="_blank" href='<%# DataBinder.Eval(Container.DataItem, "ChapterLink")%>' class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                                                        <asp:Label ID="LblChapterLink" Text="Chapter link" runat="server" Style="cursor: pointer; font-style: italic;" />
                                                                                                    </a>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <a id="aChapterFilePath" runat="server" target="_blank" href='<%# DataBinder.Eval(Container.DataItem, "ChapterFilePath")%>' class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                                                        <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                            <asp:Label ID="LblChapterFileName" Text='<%# DataBinder.Eval(Container.DataItem, "ChapterFileName")%>' runat="server" />
                                                                                                        </span>
                                                                                                    </a>
                                                                                                </td>
                                                                                                <asp:HiddenField ID="HdnCourseId" Value='<%# Eval("ChapterId") %>' runat="server" />
                                                                                                <asp:HiddenField ID="HdnCourserId" Value='<%# Eval("CourseId") %>' runat="server" />
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="separator separator-dashed my-5"></div>
                                                            <!--end::Section-->
                                                            <!--begin::Section-->
                                                            <h6 class="font-weight-bolder mb-3">Permissions:</h6>
                                                            <div class="text-dark-50 line-height-lg">
                                                                <div class="row">
                                                                    <div class="col-xl-12">
                                                                        <div class="form-group mt-10">
                                                                            <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                                                <asp:Repeater ID="RdgPermissionsOverview" OnItemDataBound="RdgPermissionsOverview_ItemDataBound" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label7" runat="server" Text="Course Name" />
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label3" runat="server" Text="Tier" />
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label5" runat="server" Text="Group Name" />
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label4" runat="server" Text="Countries" />
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tbody style="width: 100%;">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                        <asp:Label ID="Label8" Text='<%# DataBinder.Eval(Container.DataItem, "CourseDescription")%>' runat="server" />
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                        <asp:Label ID="LblTierName" Text='<%# DataBinder.Eval(Container.DataItem, "TierName")%>' runat="server" />
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                        <asp:Label ID="LblGroupName" Text='<%# DataBinder.Eval(Container.DataItem, "GroupName")%>' runat="server" />
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                                                        <asp:Label ID="LblCountries" Text='<%# DataBinder.Eval(Container.DataItem, "Countries")%>' runat="server" />
                                                                                                    </span>
                                                                                                </td>
                                                                                                <asp:HiddenField ID="HdnTierId" Value='<%# Eval("TierId") %>' runat="server" />
                                                                                                <asp:HiddenField ID="HdnCourserId" Value='<%# Eval("CourseId") %>' runat="server" />
                                                                                                <asp:HiddenField ID="HdnGroupId" Value='<%# Eval("GroupId") %>' runat="server" />
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!--end::Section-->
                                                        </asp:Panel>
                                                    </div>
                                                    <!--end: Wizard Step 5-->
                                                    <!--begin: Wizard Actions-->
                                                    <div class="d-flex justify-content-between border-top mt-5 pt-10">
                                                        <div class="mr-2">
                                                            <asp:Button ID="BtnPrevious" runat="server" Visible="false" OnClick="BtnPrevious_Click" CssClass="btn btn-light-primary font-weight-bold text-uppercase px-9 py-4" Text="Previous" />
                                                        </div>
                                                        <div>
                                                            <asp:Button ID="BtnSave" runat="server" Visible="false" OnClick="BtnSave_Click" CssClass="btn btn-success font-weight-bold text-uppercase px-9 py-4" Text="Submit" />
                                                            <asp:Button ID="BtnNext" runat="server" OnClick="BtnNext_Click" CssClass="btn btn-primary font-weight-bold text-uppercase px-9 py-4" Text="Next Step" />
                                                        </div>
                                                    </div>
                                                    <!--end: Wizard Actions-->
                                                    <div class="row">
                                                        <controls:MessageControl ID="UcWizardAlertControl" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end: Wizard-->
                                        </div>
                                    </div>
                                    <!--end: Wizard Body-->
                                </div>
                                <!--end: Wizard-->
                            </div>
                        </div>

                        <div id="loader" style="display: none; vertical-align: middle; position: fixed; left: 48%; top: 42%; background-color: #ffffff; padding-top: 20px; padding-bottom: 20px; padding-left: 40px; padding-right: 40px; border: 1px solid #0d1f39; border-radius: 5px 5px 5px 5px; -moz-box-shadow: 1px 1px 10px 2px #aaa; -webkit-box-shadow: 1px 1px 10px 2px #aaa; box-shadow: 1px 1px 10px 2px #aaa; z-index: 10000;">
                            <div id="loadermsg" style="background-color: #ffffff; padding: 10px; border-radius: 5px; background-image: url(/Images/loading.gif); background-repeat: no-repeat; background-position: center center;">
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
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

        <!-- Pop Up Create Retailor form Message (modal view) -->
        <div id="divCreateGroup" class="modal fade" tabindex="-1">
            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>
                    <div role="document" class="modal-dialog">
                        <div class="modal-content" style="width: 800px;">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="LblRetailorTitle" Text="Create Group for your training" CssClass="control-label" runat="server" />
                                </h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <i aria-hidden="true" class="ki ki-close"></i>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:TextBox ID="TbxGroupName" MaxLength="145" EnableViewState="true" ViewStateMode="Enabled" CssClass="form-control" placeholder="Name of your group" data-placement="top" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <asp:TextBox ID="TbxRetailorSearch" CssClass="form-control rounded" Width="250" placeholder="Search by Name" runat="server" />
                                            <div style="float: right; margin-top: -30px; margin-right: 25px;">
                                                <asp:ImageButton ID="ImgBtnRetailorSearch" Visible="true" runat="server" OnClick="ImgBtnRetailorSearch_OnClick" ImageUrl="images/preview.png" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-7"></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="overflow-y: scroll; height: 400px;">
                                            <div class="portlet-body todo-project-list-content">
                                                <div class="todo-project-list">
                                                    <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                        <thead>
                                                            <tr>
                                                                <td style="">
                                                                    <asp:CheckBox AutoPostBack="true" ID="CbxSelectAll" OnCheckedChanged="CbxSelectAll_OnCheckedChanged" runat="server" />
                                                                    <asp:Label ID="LblSelect" Text="All" runat="server" />
                                                                </td>
                                                                <td style="">
                                                                    <asp:Label ID="LblCompanyLogoHeader" Text="Logo" runat="server" />
                                                                </td>
                                                                <td style="">
                                                                    <asp:Label ID="LblCompanyNameHeader" Text="Name" runat="server" />
                                                                </td>
                                                                <td style="">
                                                                    <asp:Label ID="LblCompanyCountryHeader" Text="Country" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </thead>
                                                        <asp:Repeater ID="RptRetailorsList" OnItemDataBound="RptRetailorsList_OnItemDataBound" runat="server">
                                                            <ItemTemplate>
                                                                <tbody style="width: 100%;">
                                                                    <tr>
                                                                        <td style="">
                                                                            <asp:CheckBox AutoPostBack="false" ID="CbxSelectUser" runat="server" />
                                                                        </td>
                                                                        <td style="">
                                                                            <a id="aCompanyLogo" href="javascript:;" runat="server">
                                                                                <div class="media-left avatar">
                                                                                    <asp:ImageButton ID="ImgBtnCompanyLogo" Width="34" Height="34" class="media-object img-circle" runat="server" />
                                                                                </div>
                                                                            </a>
                                                                        </td>
                                                                        <td style="">
                                                                            <a id="aCompanyName" runat="server">
                                                                                <asp:Label ID="LblCompanyName" Text='<%# Eval("company_name") %>' runat="server" />
                                                                            </a>
                                                                        </td>
                                                                        <td style="">
                                                                            <asp:Label ID="LblCountry" Text='<%# Eval("country") %>' runat="server" />
                                                                            <asp:Label ID="LblRegion" Text='<%# Eval("region") %>' runat="server" />
                                                                        </td>
                                                                        <asp:HiddenField ID="id" Value='<%# Eval("id") %>' runat="server" />
                                                                        <asp:HiddenField ID="master_user_id" Value='<%# Eval("master_user_id") %>' runat="server" />
                                                                        <asp:HiddenField ID="invitation_status" Value='<%# Eval("invitation_status") %>' runat="server" />
                                                                        <asp:HiddenField ID="partner_user_id" Value='<%# Eval("partner_user_id") %>' runat="server" />
                                                                        <asp:HiddenField ID="email" Value='<%# Eval("email") %>' runat="server" />
                                                                        <asp:HiddenField ID="msgs_count" Value='<%# Eval("msgs_count") %>' runat="server" />
                                                                    </tr>
                                                                </tbody>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                    <controls:MessageControl ID="MessageControlCreateRetailors" Visible="false" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-10">
                                    <div id="divRetailorFailure" runat="server" visible="false" class="alert alert-custom alert-light-danger fade show" style="text-align: justify; width: 100%;">
                                        <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                        <div class="alert-text">
                                            <strong>
                                                <asp:Label ID="LblRetailorFailure" Text="Error! " runat="server" />
                                            </strong>
                                            <asp:Label ID="LblRetailorFailureMsg" runat="server" />
                                        </div>
                                        <div class="alert-close">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                            </button>
                                        </div>
                                    </div>
                                    <div id="divRetailorSuccess" runat="server" visible="false" class="alert alert-custom alert-light-success fade show" style="text-align: justify; width: 100%;">
                                        <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                        <div class="alert-text">
                                            <strong>
                                                <asp:Label ID="LblRetailorSuccess" Text="Done! " runat="server" />
                                            </strong>
                                            <asp:Label ID="LblRetailorSuccessMsg" runat="server" />
                                        </div>
                                        <div class="alert-close">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                                <asp:Button ID="BtnCreate" OnClick="BtnCreate_OnClick" CssClass="btn btn-primary" runat="server" Text="Create" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <!-- Pop Up Edit Retailor form Message (modal view) -->
        <div id="EditRetailor" class="modal fade" tabindex="-1" data-width="500">
            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                <ContentTemplate>
                    <div role="document" class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="LblEditRetailorTitle" Text="Add/Remove Retailors to Group" CssClass="control-label" runat="server" />
                                </h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <i aria-hidden="true" class="ki ki-close"></i>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="LblEditRetailorName" CssClass="control-label" runat="server" />
                                            <asp:TextBox ID="TbxEditRetailorName" MaxLength="145" EnableViewState="true" ViewStateMode="Enabled" CssClass="form-control" BorderStyle="None" placeholder="Enter group name here" data-placement="top" runat="server" />
                                            <asp:HiddenField ID="HdnGroupId" Value="-1" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <div class="pl-20 pr-0">
                                                <div class="form-group has-feedback">
                                                    <asp:TextBox ID="TbxEditRetailorsSearch" CssClass="form-control rounded" Width="250" placeholder="Search by Name" runat="server" />
                                                    <asp:TextBox ID="TbxEditRetailorsAdvancedSearch" Visible="false" CssClass="form-control rounded" Width="250" placeholder="Search by Country/Region/Type" runat="server" />
                                                    <div style="float: right; margin-top: -30px; margin-right: 25px;">
                                                        <asp:ImageButton ID="ImgBtnEditRetailorSearch" Visible="false" runat="server" OnClick="ImgBtnEditRetailorSearch_OnClick" ImageUrl="images/preview.png" />
                                                    </div>
                                                </div>
                                                <div style="padding-left: 10px; display: inline-block; width: 100%; font-size: 12px; padding: 5px; color: #2b3643;">
                                                    <asp:LinkButton ID="LnkBtnEditRetailorAdvancedSearch" Style="color: #2b3643 !important; text-decoration: underline;" Text="Advanced search" runat="server" />
                                                </div>

                                            </div>
                                            <asp:Label ID="LblEditRetailorList" CssClass="control-label" runat="server" />
                                            <div style="overflow-y: scroll; min-height: 250px;">
                                                <div class="portlet-body todo-project-list-content">
                                                    <div class="todo-project-list">
                                                        <asp:Repeater ID="RptEditRetailorsList" OnItemDataBound="RptEditRetailorsList_OnItemDataBound" runat="server">
                                                            <HeaderTemplate>
                                                                <ul class="chat-list m-0 media-list">
                                                                    <li class="media">
                                                                        <table>
                                                                            <tr>
                                                                                <td style="width: 90px; height: 50px;">
                                                                                    <asp:CheckBox AutoPostBack="true" ID="CbxSelectAll" runat="server" />
                                                                                    <asp:Label ID="LblSelect" Text="Select" runat="server" />
                                                                                </td>
                                                                                <td style="width: 60px; height: 50px;">
                                                                                    <asp:Label ID="LblCompanyLogoHeader" Text="Logo" runat="server" />
                                                                                </td>
                                                                                <td style="min-width: 220px;">
                                                                                    <asp:Label ID="LblCompanyNameHeader" Text="Name" runat="server" />
                                                                                </td>
                                                                                <td style="width: 100px;">
                                                                                    <asp:Label ID="LblCompanyCountryHeader" Text="Country" runat="server" />
                                                                                </td>
                                                                                <td style="width: 50px;">
                                                                                    <asp:Label ID="LblCompanyNewMessagesHeader" Visible="false" Text="New" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </li>
                                                                </ul>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <ul class="chat-list m-0 media-list">
                                                                    <li class="media">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:HiddenField ID="id" runat="server" />
                                                                                    <%--<asp:HiddenField ID="collaboration_group_id" runat="server" />--%>
                                                                                    <asp:HiddenField ID="master_user_id" runat="server" />
                                                                                    <asp:HiddenField ID="invitation_status" runat="server" />
                                                                                    <asp:HiddenField ID="partner_user_id" runat="server" />
                                                                                    <asp:HiddenField ID="email" runat="server" />
                                                                                </td>
                                                                                <td style="width: 90px;">
                                                                                    <asp:CheckBox AutoPostBack="false" ID="CbxSelectUser" runat="server" />
                                                                                </td>
                                                                                <td style="width: 70px;">
                                                                                    <a id="aCompanyLogo" href="javascript:;" runat="server">
                                                                                        <div class="media-left avatar">
                                                                                            <asp:ImageButton ID="ImgBtnCompanyLogo" CssClass="media-object img-circle" runat="server" />
                                                                                        </div>
                                                                                    </a>
                                                                                </td>
                                                                                <td style="width: 240px;">
                                                                                    <a id="aCompanyName" runat="server">
                                                                                        <asp:Label ID="LblCompanyName" runat="server" />
                                                                                    </a>
                                                                                </td>
                                                                                <td style="width: 100px;">
                                                                                    <asp:Label ID="LblCountry" runat="server" />
                                                                                    <asp:Label ID="LblRegion" runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <span id="spanMessagesCountNotification" visible="false" class="badge bg-danger" title="New unread message" runat="server">
                                                                                        <asp:Label ID="LblMessagesCount" runat="server" />
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </li>
                                                                </ul>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <controls:MessageControl ID="MessageControlEditRetailors" Visible="false" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div id="divEditRetailorFailure" runat="server" visible="false" class="alert alert-custom alert-light-danger fade show" style="text-align: justify; width: 100%;">
                                        <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                        <div class="alert-text">
                                            <strong>
                                                <asp:Label ID="LblEditRetailorFailure" Text="Error! " runat="server" />
                                            </strong>
                                            <asp:Label ID="LblEditRetailorFailureMsg" runat="server" />
                                        </div>
                                        <div class="alert-close">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                            </button>
                                        </div>
                                    </div>
                                    <div id="divEditRetailorSuccess" runat="server" visible="false" class="alert alert-custom alert-light-success fade show" style="text-align: justify; width: 100%;">
                                        <div class="alert-icon"><i class="flaticon-warning"></i></div>
                                        <div class="alert-text">
                                            <strong>
                                                <asp:Label ID="LblEditRetailorSuccess" Text="Done! " runat="server" />
                                            </strong>
                                            <asp:Label ID="LblEditRetailorSuccessMsg" runat="server" />
                                        </div>
                                        <div class="alert-close">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                                <asp:Button ID="BtnEditRetailor" OnClick="BtnEditRetailor_OnClick" CssClass="btn btn-primary" runat="server" Text="Add/Remove" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </telerik:RadAjaxPanel>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <%--<script type="text/javascript">
            //<![CDATA[
            Sys.Application.add_load(function () {
                window.upload = $find("<%=RadAsyncUpload1.ClientID %>");
                demo.initialize();
            });
            //]]>

            (function () {
                var $;

                var upload;
                var demo = window.demo = window.demo || {};

                demo.initialize = function () {
                    $ = $telerik.$;
                    upload = window.upload;
                };

                window.onClientFileUploaded = function (sender, args) {
                    //BtnUploadFile.enable();
                };

                window.updatePictureAndInfo = function () {
                    if (upload.getUploadedFiles().length > 0) {
                        __doPostBack('BtnUploadFile', 'BtnUploadFileArgs');
                    }
                    else {
                        alert("Please select file");
                    }
                };
            })();


        </script>--%>
        <script type="text/javascript">
            $(".class1").serialize();
            $(".form-horizontal").serialize();
            $("#submit_form").serialize();
            $("submit_upload_form").serialize();
        </script>


        <script type="text/javascript">

            function RapPage_OnRequestStart(sender, args) {
                $('#loader').show();
                document.getElementById("fade-class").style.opacity = "0.5";
                document.getElementById("fade-class").style.pointerEvents = "none";
            }
            function endsWith(s) {
                return this.length >= s.length && this.substr(this.length - s.length) == s;
            }
            function RapPage_OnResponseEnd(sender, args) {
                $('#loader').hide();
                document.getElementById("fade-class").style.opacity = "1";
            }

            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }

            function OpenCreateGroupPopUp() {
                $('#divCreateGroup').modal('show');
            }

            function CloseCreateGroupPopUp() {
                $('#divCreateGroup').modal('hide');
            }

        </script>

    </telerik:RadScriptBlock>

</asp:Content>
