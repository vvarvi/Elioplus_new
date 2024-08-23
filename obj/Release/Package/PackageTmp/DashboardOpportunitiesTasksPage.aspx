<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardOpportunitiesTasksPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardOpportunitiesTasksPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <div class="card card-custom">
                        <div class="card-header flex-wrap border-0 pt-6 pb-0">
                            <div class="card-title">
                                <h3 class="card-label">Manage tasks
                                </h3>
                            </div>
                            <div class="card-toolbar">
                                keep them updated...
                            </div>
                        </div>

                        <div class="card card-custom gutter-b">
                            <div class="row">
                                <div class="col-xl-12">
                                    <div class="card-body">
                                        <div class="row">
                                            <controls:MessageControl ID="UcMessageAlertInfo" Message="In this page you can manage all your tasks for all your opportunities. There are two lists with those that are already overdue and those that have a later remind date. View, edit or delete at will." IsLightColor="true" MessageType="Info" runat="server" />
                                        </div>

                                        <div class="card-header flex-wrap border-0 pt-6 pb-0">
                                            <div class="card-title">
                                                <h3 class="card-label">Later tasks</h3>
                                            </div>
                                            <div class="card-toolbar">
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 120px;">
                                                            <asp:Label ID="Label2" runat="server" Text="Subject" />
                                                        </th>
                                                        <th style="width: 200px;">
                                                            <asp:Label ID="Label3" runat="server" Text="Content" />
                                                        </th>
                                                        <th style="width: 150px;">
                                                            <asp:Label ID="Label4" runat="server" Text="Date Created" />
                                                        </th>
                                                        <th style="width: 150px;">
                                                            <asp:Label ID="Label5" runat="server" Text="Last Edit Date" />
                                                        </th>
                                                        <th style="width: 150px;">
                                                            <asp:Label ID="Label6" runat="server" Text="Remind Date" />
                                                        </th>
                                                        <th style="width: 50px;">
                                                            <asp:Label ID="Label7" Text="Actions" runat="server" />
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <asp:Repeater ID="RdgTasks" OnLoad="RdgTasks_Load" runat="server">
                                                    <ItemTemplate>
                                                        <tbody>
                                                            <tr>
                                                                <td style="">
                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                        <asp:Label ID="LblSubject" Text='<%# DataBinder.Eval(Container.DataItem, "task_subject")%>' runat="server" />
                                                                    </span>
                                                                    <asp:HiddenField ID="HdnId" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' runat="server" />
                                                                </td>
                                                                <td style="">
                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                        <asp:Label ID="LblContent" Text='<%# DataBinder.Eval(Container.DataItem, "task_content")%>' runat="server" />
                                                                    </span>
                                                                </td>
                                                                <td style="">
                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                        <asp:Label ID="LblSysdate" Text='<%# DataBinder.Eval(Container.DataItem, "sysdate")%>' runat="server" />
                                                                    </span>
                                                                </td>
                                                                <td style="">
                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                        <asp:Label ID="LblLastUpdate" Text='<%# DataBinder.Eval(Container.DataItem, "last_updated")%>' runat="server" />
                                                                    </span>
                                                                </td>
                                                                <td style="">
                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                        <asp:Label ID="LblRemindDate" Text='<%# DataBinder.Eval(Container.DataItem, "remind_date")%>' runat="server" />
                                                                    </span>
                                                                </td>
                                                                <td style="width: 100px;">
                                                                    <a id="aEdit" onserverclick="BtnEdit_OnClick" runat="server" title="Edit" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
                                                                        <%--<span class="svg-icon svg-icon-md svg-icon-primary">
                                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Write.svg-->
                                                                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                    <rect x="0" y="0" width="24" height="24" />
                                                                                    <path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953)" />
                                                                                    <path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                                </g>
                                                                            </svg>
                                                                            <!--end::Svg Icon-->
                                                                        </span>--%>
                                                                    </a>
                                                                    <a id="aDelete" onserverclick="BtnDelete_OnClick" runat="server" title="Delete" class="btn btn-icon btn-light-danger btn-hover-danger btn-sm">
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
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                            <controls:MessageControl ID="UcLaterMessage" runat="server" />
                                        </div>
                                        <div class="row">
                                            <controls:MessageControl ID="UcMessageAlertControlGrid" runat="server" />
                                        </div>

                                        <div class="card-header flex-wrap border-0 pt-6 pb-0">
                                            <div class="card-title">
                                                <h3 class="card-label">Overdue tasks</h3>
                                            </div>
                                            <div class="card-toolbar">
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 100px;">
                                                            <asp:Label ID="Label12" runat="server" Text="Subject" />
                                                        </th>
                                                        <th style="width: 200px;">
                                                            <asp:Label ID="Label13" runat="server" Text="Content" />
                                                        </th>
                                                        <th style="width: 180px;">
                                                            <asp:Label ID="Label8" runat="server" Text="Opportunity" />
                                                        </th>
                                                        <th style="width: 140px;">
                                                            <asp:Label ID="Label14" runat="server" Text="Date Created" />
                                                        </th>
                                                        <th style="width: 140px;">
                                                            <asp:Label ID="Label15" runat="server" Text="Last Edit Date" />
                                                        </th>
                                                        <th style="width: 140px;">
                                                            <asp:Label ID="Label1" runat="server" Text="Remind Date" />
                                                        </th>
                                                        <th style="width: 80px;">
                                                            <asp:Label ID="Label17" Text="Actions" runat="server" />
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <asp:Repeater ID="RdgTasksOverdue" OnLoad="RdgTasksOverdue_Load" runat="server">
                                                    <ItemTemplate>
                                                        <tbody>
                                                            <tr>
                                                                <td style="">
                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                        <asp:Label ID="LblSubject" Text='<%# DataBinder.Eval(Container.DataItem, "task_subject")%>' runat="server" />
                                                                    </span>
                                                                    <asp:HiddenField ID="HdnId" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' runat="server" />
                                                                </td>
                                                                <td style="">
                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                        <asp:Label ID="LblContent" Text='<%# DataBinder.Eval(Container.DataItem, "task_content")%>' runat="server" />
                                                                    </span>
                                                                </td>
                                                                <td style="">
                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                        <asp:Label ID="LblOpportunity" Text='<%# DataBinder.Eval(Container.DataItem, "organization_name")%>' runat="server" />
                                                                    </span>
                                                                </td>
                                                                <td style="">
                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                        <asp:Label ID="LblSysdate" Text='<%# DataBinder.Eval(Container.DataItem, "sysdate")%>' runat="server" />
                                                                    </span>
                                                                </td>
                                                                <td style="">
                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                        <asp:Label ID="LblLastUpdate" Text='<%# DataBinder.Eval(Container.DataItem, "last_updated")%>' runat="server" />
                                                                    </span>
                                                                </td>
                                                                <td style="">
                                                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                        <asp:Label ID="LblRemindDate" Text='<%# DataBinder.Eval(Container.DataItem, "remind_date")%>' runat="server" />
                                                                    </span>
                                                                </td>
                                                                <td style="width: 100px;">
                                                                    <a id="aEdit" onserverclick="BtnEdit_OnClick" runat="server" title="Edit" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
                                                                        <%--<span class="svg-icon svg-icon-md svg-icon-primary">
                                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Write.svg-->
                                                                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                                                    <rect x="0" y="0" width="24" height="24" />
                                                                                    <path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953)" />
                                                                                    <path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                                                </g>
                                                                            </svg>
                                                                            <!--end::Svg Icon-->
                                                                        </span>--%>
                                                                    </a>
                                                                    <a id="aDelete" onserverclick="BtnDelete_OnClick" runat="server" title="Delete" class="btn btn-icon btn-light-danger btn-hover-danger btn-sm">
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
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                            <controls:MessageControl ID="UcOverdueMessage" runat="server" />
                                        </div>
                                    </div>
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

    <div id="EditTask" class="modal fade" tabindex="-1">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblEditTaskTitle" Text="Edit Task" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group row">
                                <div class="col-lg-12">
                                    <div class="input-group">

                                        <div class="input-group-append"><span class="input-group-text"><i class="flaticon2-website"></i></span></div>
                                        <asp:TextBox ID="TbxEditTaskSubject" MaxLength="145" EnableViewState="true" ViewStateMode="Enabled" CssClass="form-control" placeholder="Enter task subject here" data-placement="top" runat="server" />
                                    </div>
                                    <span class="form-text text-muted"></span>
                                    <asp:HiddenField ID="TbxEditTaskId" Value="0" runat="server" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-12">
                                    <div class="input-group">
                                        <div class="input-group-append"><span class="input-group-text"><i class="fa fa-paper-plane"></i></span></div>
                                        <asp:TextBox ID="TbxEditTaskContent" MaxLength="3995" TextMode="MultiLine" Rows="10" CssClass="form-control form-control-lg form-control" placeholder="Enter task content here" data-placement="top" runat="server" />

                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-3 col-form-label">Task date</label>
                                <div class="col-lg-9">
                                    <div class="input-group">
                                        <telerik:RadDateTimePicker ID="RdpRemindDateEdit" Width="290" ZIndex="11500" runat="server" />
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="row">
                                <div id="divGeneralSuccess" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblSuccess" Text="Done! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblGeneralSuccess" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">
                                                <i class="ki ki-close"></i>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                                <div id="divEditTaskFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblEditTaskError" Text="Error" runat="server" />
                                        </strong>
                                        <asp:Label ID="LblEditTaskFail" runat="server" />
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
                            <asp:Button ID="BtnDiscardEditTask" OnClick="BtnDiscardEditTask_OnClick" CssClass="btn btn-light-danger" runat="server" Text="Discard" />
                            <asp:Button ID="BtnAddEditTask" OnClick="BtnAddEditTask_OnClick" CssClass="btn btn-primary" runat="server" Text="OK" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div class="modal-dialog">
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
                                <div class="form-group">
                                    <asp:Label ID="LblConfMsg" Text="Are you sure you want to delete this note?" runat="server" />
                                    <asp:HiddenField ID="TbxTaskConfId" Value="0" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="UcPopUpConfirmationMessageAlert" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-light-primary">No</button>
                            <asp:Button ID="BtnConfDelete" OnClick="BtnConfDelete_OnClick" CssClass="btn btn-primary" Text="Yes" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">            
            function CloseEditPopUp() {
                $('#EditTask').modal('hide');
            }

            function OpenEditPopUp() {
                $('#EditTask').modal('show');
            }

            function CloseConfirmPopUp() {
                $('#divConfirm').modal('hide');
            }

            function OpenConfirmPopUp() {
                $('#divConfirm').modal('show');
            }
        </script>

    </telerik:RadScriptBlock>
</asp:Content>
