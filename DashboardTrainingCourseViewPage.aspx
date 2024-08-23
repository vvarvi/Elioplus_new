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
                                            <div id="divMStep0" runat="server" class="wizard-step" data-wizard-type="step">
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
                                                        <h3 class="wizard-title">Course Overview</h3>
                                                        <div class="wizard-desc">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Repeater ID="RdgChapter" OnItemDataBound="RdgChapter_ItemDataBound" runat="server">
                                                <ItemTemplate>
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
                                                                <h3 class="wizard-title">
                                                                    <asp:Label ID="LblChapterStep" Text='<%# DataBinder.Eval(Container.DataItem, "ChapterStep")%>' runat="server" />
                                                                    <img id="imgViewed" runat="server" title="completed chapter" visible="false" src="/assets/media/svg/icons/General/Shield-check.svg" width="20"/>
                                                                </h3>
                                                                <div class="wizard-desc">
                                                                    <asp:Label ID="LblChapterTitle" Text='<%# DataBinder.Eval(Container.DataItem, "ChapterTitle")%>' runat="server" />
                                                                </div>                                                               
                                                            </div>
                                                        </div>
                                                        <asp:HiddenField ID="HdnId" Value='<%# Eval("ChapterId") %>' runat="server" />
                                                        <asp:HiddenField ID="HdnIsViewed" Value='<%# Eval("IsViewed") %>' runat="server" />
                                                    </div>
                                                    <!--end::Wizard Step 1 Nav-->
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <controls:MessageControl ID="MessageControlChapters" Visible="false" runat="server" />
                                        </div>
                                    </div>
                                    <!--end: Wizard Nav-->
                                    <!--begin: Wizard Body-->
                                    <div class="wizard-body py-8 px-8 py-lg-20 px-lg-10">
                                        <!--begin: Wizard Form-->
                                        <div class="row">
                                            <div class="offset-xxl-2 col-xxl-8">
                                                <div class="form" id="kt_form">
                                                    <div id="divStep0" runat="server" class="pb-5" data-wizard-type="step-content">
                                                        <div class="form-group" style="text-align: center;">
                                                            <div class="mt-7" style="height: 154px;">
                                                                <div class="symbol symbol-circle symbol-lg-90">
                                                                    <asp:Image ID="ImgCoursePreview" Width="150" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <h4 class="mb-10 font-weight-bold text-dark">Course Description</h4>
                                                        <!--begin::Input-->
                                                        <div class="form-group">
                                                            <p>
                                                                <asp:Label ID="LblCourseOverview" runat="server" />
                                                            </p>
                                                        </div>

                                                        <h4 class="mb-10 font-weight-bold text-dark">Course Content</h4>
                                                        <!--begin::Input-->
                                                        <div class="form-group">
                                                            <asp:CheckBoxList ID="CbxChaptersList" runat="server" />
                                                        </div>
                                                    </div>

                                                    <!--begin: Wizard Step 1-->
                                                    <div id="divStep1" visible="false" runat="server" class="pb-5" data-wizard-type="step-content">
                                                        <h4 class="mb-10 font-weight-bold text-dark">
                                                            <asp:Label ID="LblChapterTitle" runat="server" />
                                                        </h4>

                                                        <h4 class="mb-10 font-weight-bold text-dark">Chapter Link</h4>
                                                        <!--begin::Input-->
                                                        <div class="form-group">
                                                            <a id="aChapterLink" runat="server" target="_blank" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                <asp:Label ID="LblChapterLink" Text="Chapter link" runat="server" Style="cursor: pointer; font-style: italic;" />
                                                            </a>
                                                        </div>

                                                        <h4 class="mb-10 font-weight-bold text-dark">Chapter File</h4>
                                                        <!--begin::Input-->
                                                        <div class="form-group">
                                                            <a id="aChapterFilePath" runat="server" target="_blank" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                    <asp:Label ID="LblChapterFileName" runat="server" />
                                                                </span>
                                                            </a>
                                                        </div>

                                                        <h4 class="mb-10 font-weight-bold text-dark">Chapter Content</h4>
                                                        <!--begin::Input-->
                                                        <div class="form-group">
                                                            <p>
                                                                <asp:Label ID="LblChapterText" runat="server" />
                                                            </p>
                                                        </div>
                                                    </div>
                                                    <!--end: Wizard Step 1-->

                                                    <!--begin: Wizard Actions-->
                                                    <div class="d-flex justify-content-between border-top mt-5 pt-10">
                                                        <div class="mr-2">
                                                            <asp:Button ID="BtnPrevious" runat="server" Visible="false" OnClick="BtnPrevious_Click" CssClass="btn btn-light-primary font-weight-bold text-uppercase px-9 py-4" Text="Previous" />
                                                        </div>
                                                        <div>
                                                            <asp:Button ID="BtnComplete" runat="server" Visible="false" OnClick="BtnComplete_Click" CssClass="btn btn-success font-weight-bold text-uppercase px-9 py-4" Text="Complete" />
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

    </telerik:RadAjaxPanel>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

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

        </script>

    </telerik:RadScriptBlock>

</asp:Content>
