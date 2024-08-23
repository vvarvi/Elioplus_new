<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardTasksAddEditPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardTasksAddEditPage" %>

<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <div class="row">
                <div class="col-xl-3">
                    <a id="aBack" class="btn btn-circle green m-icon" runat="server">Back
                        <i class="m-icon-swapleft m-icon-white"></i>
                    </a>
                </div>
                <div class="col-xl-6">
                    <a href="#AddTask" id="aAddTask" data-toggle="modal" class="btn green-sharp btn-outline  btn-block sbold uppercase" runat="server">
                        <asp:Label ID="LblAddOppTask" runat="server" />
                        <i id="iAddTask" class="fa fa-edit" runat="server"></i>
                    </a>
                </div>
                <div class="col-xl-3">
                </div>
            </div>

            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xl-12">
                            <div class="col-md-2">
                            </div>
                            <div id="divAddTaskSucc" runat="server" visible="false" class="col-md-8 alert alert-success" style="text-align: center; margin-top: 30px;">
                                <strong>
                                    <asp:Label ID="LblAddTaskErr" Text="Done! " runat="server" />
                                </strong>
                                <asp:Label ID="LblAddTaskSucc" runat="server" />
                            </div>
                            <div id="divTaskInfo" runat="server" visible="true" class="col-md-8 alert alert-info" style="text-align: center; margin-top: 30px;">
                                <strong>
                                    <asp:Label ID="LblTaskInfo" Text="Info! " runat="server" />
                                </strong>
                                <asp:Label ID="LblTaskInfoContent" Text="Create and manage your tasks for this opportunity. Click 'Add Task' above to create a new one or manage the existing ones below." runat="server" />
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xl-12">
                            <div class="portlet box yellow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-file-text-o"></i>Tasks list
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="scroller search-table table-responsive">
                                        <telerik:RadGrid ID="RdgTasks" Style="margin: auto; position: relative;" HeaderStyle-CssClass="bg-blue" AllowPaging="true" AllowSorting="false" PagerStyle-Position="TopAndBottom" CssClass="table table-bordered table-striped table-condensed" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" OnItemDataBound="RdgTasks_OnItemDataBound" OnNeedDataSource="RdgTasks_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                            <MasterTableView CssClass="table-status">
                                                <Columns>
                                                    <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                    <telerik:GridBoundColumn ItemStyle-CssClass="bg-blue" HeaderStyle-Width="80" Display="false" HeaderText="Subject" DataField="task_subject" UniqueName="task_subject" />
                                                    <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Font-Size="12" HeaderStyle-Width="120" HeaderText="Subject" UniqueName="n_subject">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TbxSubject" BorderStyle="None" Font-Size="12" MaxLength="200" Width="100%" ReadOnly="true" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" HeaderText="Content" DataField="task_content" UniqueName="task_content" />
                                                    <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Font-Size="12" HeaderStyle-Width="180" HeaderText="Content" UniqueName="t_content">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TbxContent" BorderStyle="None" Font-Size="12" MaxLength="200" Width="100%" ReadOnly="true" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Date Created" HeaderStyle-Font-Size="12" DataField="sysdate" UniqueName="sysdate" />
                                                    <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Last Edit Date" HeaderStyle-Font-Size="12" DataField="last_updated" UniqueName="last_updated" />
                                                    <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Remind Date" HeaderStyle-Font-Size="12" DataField="remind_date" UniqueName="remind_date" />
                                                    <telerik:GridBoundColumn HeaderStyle-Width="100" Display="false" HeaderText="Status" DataField="status" UniqueName="status" />
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="20" HeaderText="Actions" HeaderStyle-Font-Size="12" DataField="actions" UniqueName="actions">
                                                        <ItemTemplate>
                                                            <a id="aEditTask" onserverclick="BtnEdit_OnClick" runat="server">
                                                                <i class="icon-arrow-right font-lg font-blue"></i>
                                                            </a>
                                                            <a id="aDeleteTask" onserverclick="BtnDelete_OnClick" runat="server">
                                                                <i class="icon-close font-lg font-green-soft"></i>
                                                            </a>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                        <div class="col-md-3">
                                        </div>
                                        <div id="divNoResults" runat="server" visible="false" class="col-md-6 alert alert-warning" style="text-align: center; margin-top: 30px;">
                                            <strong>
                                                <asp:Label ID="LblNoResultsTitle" Text="0 Tasks! " runat="server" />
                                            </strong>
                                            <asp:Label ID="LblNoResultsContent" Text="There are no Tasks yet, create your first one!" runat="server" />
                                        </div>
                                        <div class="col-md-3">
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

    <div id="AddTask" class="modal fade" tabindex="-1" data-width="500">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title">
                                <asp:Label ID="LblAddTaskTitle" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblAddTaskSubj" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxAddTaskSubj" MaxLength="145" EnableViewState="true" ViewStateMode="Enabled" CssClass="form-control" BorderStyle="None" placeholder="Enter subject here" data-placement="top" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="LblAddTaskCont" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxAddTaskCont" MaxLength="3995" TextMode="MultiLine" Rows="10" CssClass="form-control" placeholder="Enter content here" data-placement="top" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="LblTaskDate" CssClass="control-label" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <telerik:RadDateTimePicker ID="RdpRemindDate" ZIndex="11500" Width="200" Skin="Silk" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAddTaskFailure" runat="server" visible="false" class="col-md-12 alert alert-danger" style="text-align: center">
                            <strong>
                                <asp:Label ID="LblAddTaskError" Text="Error! " runat="server" />
                            </strong>
                            <asp:Label ID="LblAddTaskFail" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnDiscardAddTask" OnClick="BtnDiscardAddTask_OnClick" CssClass="btn red-sunglo" runat="server" Text="Discard" />
                            <button type="button" data-dismiss="modal" class="btn dark btn-outline">Close</button>
                            <asp:Button ID="BtnAddTask" OnClick="BtnAddTask_OnClick" CssClass="btn green-meadow" runat="server" Text="OK" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="EditTask" class="modal fade" tabindex="-1" data-width="500">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title">
                                <asp:Label ID="LblEditTaskTitle" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblTaskEditSubj" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxEditTaskSubj" MaxLength="145" EnableViewState="true" ViewStateMode="Enabled" CssClass="form-control" BorderStyle="None" placeholder="Enter subject here" data-placement="top" runat="server" />
                                        <asp:TextBox ID="TbxEditTaskId" CssClass="hidden" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="LblTaskEditCont" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxTaskEditCont" MaxLength="3995" TextMode="MultiLine" Rows="10" CssClass="form-control" placeholder="Enter content here" data-placement="top" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="LblTaskEditDate" CssClass="control-label" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <telerik:RadDateTimePicker ID="RdpRemindDateEdit" ZIndex="11500" Width="200" Skin="Silk" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divTaskEditFailure" runat="server" visible="false" class="col-md-12 alert alert-danger" style="text-align: center">
                            <strong>
                                <asp:Label ID="LblTaskEditErr" Text="Error! " runat="server" />
                            </strong>
                            <asp:Label ID="LblTaskEditFail" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnDiscardEditTask" OnClick="BtnDiscardEditTask_OnClick" CssClass="btn red-sunglo" runat="server" Text="Discard" />
                            <button type="button" data-dismiss="modal" class="btn dark btn-outline">Close</button>
                            <asp:Button ID="BtnConfEdit" OnClick="BtnConfEdit_OnClick" CssClass="btn green-meadow" runat="server" Text="OK" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="500">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title">
                                <asp:Label ID="LblConfTitle" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblConfMsg" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxTaskConfId" CssClass="hidden" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn dark btn-outline">Back</button>
                            <asp:Button ID="BtnConfDelete" OnClick="BtnConfDelete_OnClick" CssClass="btn red-sunglo" runat="server" Text="Delete" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">
            function CloseTaskPopUp() {
                $('#AddTask').modal('hide');
            }
        </script>
        <script type="text/javascript">
            function OpenTaskPopUp() {
                $('#AddTask').modal('show');
            }
        </script>
        <script type="text/javascript">
            function CloseConfirmPopUp() {
                $('#divConfirm').modal('hide');
            }
        </script>
        <script type="text/javascript">
            function OpenConfirmPopUp() {
                $('#divConfirm').modal('show');
            }
        </script>
        <script type="text/javascript">
            function CloseEditPopUp() {
                $('#EditTask').modal('hide');
            }
        </script>
        <script type="text/javascript">
            function OpenEditPopUp() {
                $('#EditTask').modal('show');
            }
        </script>

        <!--begin::Page Scripts(used by this page)-->

        <!--end::Page Scripts-->

    </telerik:RadScriptBlock>

</asp:Content>
