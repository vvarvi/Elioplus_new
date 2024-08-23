<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Uc_Comments.ascx.cs" Inherits="WdS.ElioPlus.Controls.PRM.Uc_Comments" %>

<%@ Register Src="~/Controls/AlertControls/MessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/AlertControls/NotificationControl.ascx" TagName="NotificationControl" TagPrefix="controls" %>

<asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="col-xl-12 pt-10 pt-xl-0">
            <!--begin::Card-->
            <div class="card card-custom card-stretch" id="kt_todo_view">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card-header flex-wrap border-0 pt-6 pb-0">
                            <div class="col-md-10" style="float: left;">
                                <div class="card-title">
                                    <h3 class="card-label">Manage comments
                                    </h3>
                                </div>
                            </div>
                            <div class="col-md-2" style="float: right;">
                                <div class="card-toolbar right">
                                    keep them updated...
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--begin::Body-->
                <div class="card-body p-0">

                    <!--begin::Messages-->
                    <div class="mb-3">
                        <asp:Repeater ID="Rdg1" OnLoad="Rdg1_Load" OnItemDataBound="Rdg1_ItemDataBound" runat="server">
                            <ItemTemplate>
                                <!--begin::Message-->
                                <div class="cursor-pointer shadow-xs toggle-on" data-inbox="message">
                                    <!--begin::Info-->
                                    <div class="d-flex align-items-start card-spacer-x py-4">
                                        <!--begin::User Photo-->
                                        <span class="symbol symbol-35 mr-3 mt-1">
                                            <a id="aPersonLogo" runat="server">
                                                <img id="img1" runat="server" width="20" height="20" alt="" class="symbol-label" src='<%# DataBinder.Eval(Container.DataItem, "company_logo")%>' />
                                            </a>
                                        </span>
                                        <!--end::User Photo-->
                                        <!--begin::User Details-->
                                        <div class="d-flex flex-column flex-grow-1 flex-wrap mr-2">
                                            <div class="d-flex">
                                                <a id="aPersonImage" runat="server" class="font-size-lg font-weight-bolder text-dark-75 text-hover-primary mr-2">
                                                    <%# DataBinder.Eval(Container.DataItem, "person_name")%>
                                                </a>
                                                <div id="divDaysPast" runat="server" visible="false" class="font-weight-bold text-muted">
                                                    <span class="label label-success label-dot mr-2"></span>
                                                    <asp:Label ID="LblDaysPast" runat="server" />
                                                </div>
                                            </div>
                                            <div class="d-flex flex-column">
                                                <div class="toggle-off-item">
                                                    <span class="font-weight-bold text-muted cursor-pointer" data-toggle="dropdown">
                                                        <%# DataBinder.Eval(Container.DataItem, "deal_company_name")%>

                                                        <i class="flaticon2-down icon-xs ml-1 text-dark-50"></i></span>
                                                    <div class="dropdown-menu dropdown-menu-md dropdown-menu-left p-5" style="min-width: 265px;">
                                                        <table>
                                                            <tr>
                                                                <td class="text-muted w-75px py-2">From</td>
                                                                <td>
                                                                    <%# DataBinder.Eval(Container.DataItem, "person_name")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-muted py-2">Email:</td>
                                                                <td><%# DataBinder.Eval(Container.DataItem, "writter_email")%></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-muted py-2">Date:</td>
                                                                <td><%# DataBinder.Eval(Container.DataItem, "sysdate")%></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-muted py-2" style="width: 85px;">Deal owner:</td>
                                                                <td>
                                                                    <a id="aDealOwnerName" runat="server">
                                                                        <%# DataBinder.Eval(Container.DataItem, "owner_company_name")%>
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="text-muted font-weight-bold toggle-on-item" data-inbox="toggle">With resrpect, i must disagree with Mr.Zinsser. We all know the most part of important part....</div>
                                            </div>
                                        </div>
                                        <div class="d-flex align-items-center">
                                            <div class="font-weight-bold text-muted mr-2">
                                                <%# DataBinder.Eval(Container.DataItem, "written_time")%>
                                            </div>
                                            <div class="d-flex align-items-center" data-inbox="toolbar">
                                                <span id="spanIsNew" runat="server" class="btn btn-clean btn-xs btn-icon mr-2" data-toggle="tooltip" data-placement="top" title="is new comment">
                                                    <i id="iIsNew" runat="server" class="flaticon-star icon-1x text-warning"></i>
                                                </span>
                                                <a id="aIsRead" onserverclick="aIsRead_ServerClick" visible="false" runat="server">
                                                    <span class="btn btn-clean btn-xs btn-icon" data-toggle="tooltip" data-placement="top" title="Mark as read">
                                                        <i class="flaticon-add-label-button icon-1x"></i>
                                                    </span>
                                                </a>
                                            </div>
                                        </div>
                                        <!--end::User Details-->
                                    </div>
                                    <!--end::Info-->
                                    <!--begin::Comment-->
                                    <div class="card-spacer-x pt-2 pb-5 toggle-off-item">
                                        <!--begin::Text-->
                                        <div class="mb-1">
                                            <p>
                                                <%# DataBinder.Eval(Container.DataItem, "description")%>
                                            </p>
                                        </div>
                                        <!--end::Text-->
                                        <!--begin::Attachments-->
                                        <div id="divUploadFileArea" runat="server" visible="false" class="d-flex flex-column font-size-sm font-weight-bold">
                                            <a id="aCommentFile" runat="server" class="d-flex align-items-center text-muted text-hover-primary py-1">
                                                <span id="spanCommentFileIsNew" runat="server" class="flaticon2-clip-symbol text-warning icon-1x mr-2"></span>
                                                <asp:Label ID="LblPreViewCommentFile" runat="server" />
                                            </a>
                                        </div>
                                        <!--end::Attachments-->
                                    </div>
                                    <!--end::Comment-->
                                </div>
                                <!--end::Message-->
                                <asp:HiddenField ID="HdnCommentId" Value='<%# Eval("comment_id") %>' runat="server" />
                            </ItemTemplate>
                        </asp:Repeater>

                        <div class="row" style="padding: 0 20px;">
                            <controls:MessageControl ID="UcRgd1" runat="server" />
                        </div>
                    </div>
                    <!--end::Messages-->
                    <!--begin::Reply-->
                    <div class="card-spacer-x pb-10 pt-5" id="kt_todo_reply">
                        <div class="card card-custom shadow-sm">
                            <div class="card-body p-0">
                                <!--begin::Form-->
                                <form id="kt_todo_reply_form">
                                    <!--begin::Body-->
                                    <div class="d-block">
                                        <!--begin::Message-->
                                        <div id="kt_todo_reply_editor" class="border-0" style="height: 200px">
                                            <asp:TextBox ID="TbxMessage" runat="server" TextMode="MultiLine" Rows="10" BorderStyle="None" Width="100%" placeholder="Type your comment" />
                                        </div>
                                        <!--end::Message-->
                                        <!--begin::Attachments-->
                                        <div class="dropzone dropzone-multi px-8 py-4" id="kt_todo_reply_attachments">
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
                                                <a id="aSaveComment" runat="server" onserverclick="aSaveComment_ServerClick" class="btn btn-primary font-weight-bold px-6">
                                                    <span class="navi-icon">
                                                        <i class="flaticon2-medical-records"></i>
                                                    </span>
                                                    <span class="navi-text">Save</span>
                                                </a>
                                            </div>
                                            <!--end::Send-->
                                            <!--begin::Other-->
                                            <span runat="server" visible="false" class="btn btn-icon btn-sm btn-clean mr-2 hidden" id="kt_todo_reply_attachments_select">
                                                <i class="flaticon2-clip-symbol"></i>
                                            </span>
                                            <input type="file" name="uploadFile" id="inputFile" data-buttonname="btn-black btn-outline" data-iconname="ion-image mr-5" class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .doc, .png, .jpg, .jpeg, .rar, .zip, .tar, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html" runat="server" />

                                            <!--end::Other-->
                                        </div>
                                        <!--end::Actions-->
                                        <!--begin::Toolbar-->
                                        <div class="d-flex align-items-center">
                                            <a id="aCancelComment" runat="server" onserverclick="aCancelComment_ServerClick">
                                                <span class="btn btn-icon btn-sm btn-clean" data-inbox="dismiss" data-toggle="tooltip" title="Dismiss reply">
                                                    <i class="flaticon2-rubbish-bin-delete-button"></i>
                                                </span>
                                            </a>
                                        </div>
                                        <!--end::Toolbar-->
                                    </div>
                                    <!--end::Footer-->
                                    <div class="row" style="padding: 0 20px;">
                                        <controls:MessageControl ID="MessageCommentControl" runat="server" />
                                    </div>
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
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="aSaveComment" />
    </Triggers>
</asp:UpdatePanel>

