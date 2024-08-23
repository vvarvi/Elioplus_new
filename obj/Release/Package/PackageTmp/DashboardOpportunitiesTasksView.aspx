<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardOpportunitiesTasksView.aspx.cs" Inherits="WdS.ElioPlus.DashboardOpportunitiesTasksView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">        
        <script type="text/javascript">

            function RapPage_OnRequestStart(sender, args) {
                $('#loader').show();
            }

            function endsWith(s) {
                return this.length >= s.length && this.substr(this.length - s.length) == s;
            }

            function RapPage_OnResponseEnd(sender, args) {
                $('#loader').hide();
            }

            function isNumberOnly(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">        
        <div class="page-bar">
            <ul class="page-breadcrumb">
                <li><span>
                    <asp:Label ID="LblDashboard" runat="server" /></span> <i class="fa fa-circle"></i>
                </li>
                <li><span>
                    <asp:Label ID="LblDashPage" runat="server" /></span> </li>
            </ul>
        </div>        
        <h3 class="page-title">
            <asp:Label ID="LblElioplusDashboard" runat="server" />
            <small>
                <asp:Label ID="LblDashSubTitle" runat="server" /></small>
        </h3>
        <div style="float:left">
            <a id="aBack" class="btn btn-circle green m-icon" runat="server"> Back
                <i class="m-icon-swapleft m-icon-white"></i>
            </a>
        </div>
        <div id="page-content-wrapper">
            <div id="middle">
                <div class="clearfix">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                </div>
                                <div id="divAddTaskSucc" runat="server" visible="false" class="col-md-8 alert alert-success" style="text-align:center; margin-top:30px;">
                                    <strong>
                                        <asp:Label ID="LblAddTaskErr" Text="Done! " runat="server" />
                                    </strong>
                                    <asp:Label ID="LblAddTaskSucc" runat="server" />
                                </div>
                                <div id="divTaskInfo" runat="server" visible="true" class="col-md-8 alert alert-info" style="text-align:center; margin-top:30px;">
                                    <strong>
                                        <asp:Label ID="LblTaskInfo" Text="Info! " runat="server" />
                                    </strong>
                                    <asp:Label ID="LblTaskInfoContent" Text="In this page you can manage all your tasks for all your opportunities. There are two lists with those that are already overdue and those that have a later remind date. View, edit or delete at will." runat="server" />
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>                        
                            <div class="row">
                                <div class="col-md-12 ">
                                    <div class="portlet box yellow">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-file-text-o"></i>Later tasks</div>                                    
                                        </div>
                                        <div class="portlet-body">
                                            <div class="scroller search-table table-responsive">                                        
                                                <telerik:RadGrid ID="RdgTasks" Style="margin: auto; position: relative;" HeaderStyle-CssClass="bg-blue" AllowPaging="true" AllowSorting="false" PagerStyle-Position="TopAndBottom" CssClass="table table-bordered table-striped table-condensed" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" OnItemDataBound="RdgTasks_OnItemDataBound" OnNeedDataSource="RdgTasks_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                                    <MasterTableView CssClass="table-status">                                                                        
                                                        <Columns>
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                            <telerik:GridBoundColumn Display="false" HeaderText="OppId" DataField="OppId" UniqueName="OppId" />
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Subject" DataField="task_subject" UniqueName="task_subject" />
                                                            <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Font-Size="12" HeaderStyle-Width="25%" HeaderText="Subject" UniqueName="n_subject">
                                                                <ItemTemplate>                                                                    
                                                                    <asp:TextBox ID="TbxSubject" BorderStyle="None" Font-Size="12" Width="100%" ReadOnly="true" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Content" DataField="task_content" UniqueName="task_content" />
                                                            <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Font-Size="12" HeaderStyle-Width="30%" HeaderText="Content" UniqueName="t_content">
                                                                <ItemTemplate>                                                                    
                                                                    <asp:TextBox ID="TbxContent" BorderStyle="None" Font-Size="12" Width="100%" ReadOnly="true" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Opportunity" DataField="organization_name" UniqueName="organization_name" />
                                                            <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Font-Size="12" HeaderStyle-Width="20%" HeaderText="Opportunity" UniqueName="opportunity">
                                                                <ItemTemplate>                                                                    
                                                                    <asp:TextBox ID="TbxOpportunity" BorderStyle="None" Font-Size="12" Width="100%" ReadOnly="true" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderText="Created" HeaderStyle-Font-Size="12" DataField="sysdate" UniqueName="sysdate" />
                                                            <telerik:GridBoundColumn HeaderText="Edited" HeaderStyle-Font-Size="12" DataField="last_updated" UniqueName="last_updated" />
                                                            <telerik:GridBoundColumn HeaderText="Remind" HeaderStyle-Font-Size="12" DataField="remind_date" UniqueName="remind_date" />
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Status" DataField="status" UniqueName="status" />
                                                            <telerik:GridTemplateColumn HeaderText="Actions" HeaderStyle-Font-Size="12" DataField="actions" UniqueName="actions">
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
                                                <div id="divNoResults" runat="server" visible="false" class="col-md-6 alert alert-warning" style="text-align:center; margin-top:30px;">
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
                            <div class="row">
                                <div class="col-md-12 ">
                                    <div class="portlet box yellow">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-file-text-o"></i>Overdue tasks</div>                                    
                                        </div>
                                        <div class="portlet-body">
                                            <div class="scroller search-table table-responsive">                                        
                                                <telerik:RadGrid ID="RdgTasksOverdue" Style="margin: auto; position: relative;" HeaderStyle-CssClass="bg-blue" AllowPaging="true" AllowSorting="false" PagerStyle-Position="TopAndBottom" CssClass="table table-bordered table-striped table-condensed" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" OnItemDataBound="RdgTasksOverdue_OnItemDataBound" OnNeedDataSource="RdgTasks_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                                    <MasterTableView CssClass="table-status">                                                                        
                                                        <Columns>
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                            <telerik:GridBoundColumn Display="false" HeaderText="OppId" DataField="OppId" UniqueName="OppId" />
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Subject" DataField="task_subject" UniqueName="task_subject" />
                                                            <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Font-Size="12" HeaderStyle-Width="25%" HeaderText="Subject" UniqueName="n_subject">
                                                                <ItemTemplate>                                                                    
                                                                    <asp:TextBox ID="TbxSubjectOvrd" BorderStyle="None" Font-Size="12" Width="100%" ReadOnly="true" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Content" DataField="task_content" UniqueName="task_content" />
                                                            <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Font-Size="12" HeaderStyle-Width="30%" HeaderText="Content" UniqueName="t_content">
                                                                <ItemTemplate>                                                                    
                                                                    <asp:TextBox ID="TbxContentOvrd" BorderStyle="None" Font-Size="12" Width="100%" ReadOnly="true" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Opportunity" DataField="organization_name" UniqueName="organization_name" />
                                                            <telerik:GridTemplateColumn ItemStyle-CssClass="table-title" HeaderStyle-Font-Size="12" HeaderStyle-Width="20%" HeaderText="Opportunity" UniqueName="opportunity">
                                                                <ItemTemplate>                                                                    
                                                                    <asp:TextBox ID="TbxOpportunityOvrd" BorderStyle="None" Font-Size="12" Width="100%" ReadOnly="true" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderText="Created" HeaderStyle-Font-Size="12" DataField="sysdate" UniqueName="sysdate" />
                                                            <telerik:GridBoundColumn HeaderText="Edited" HeaderStyle-Font-Size="12" DataField="last_updated" UniqueName="last_updated" />
                                                            <telerik:GridBoundColumn HeaderText="Remind" HeaderStyle-Font-Size="12" DataField="remind_date" UniqueName="remind_date" />
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Status" DataField="status" UniqueName="status" />
                                                            <telerik:GridTemplateColumn HeaderText="Actions" HeaderStyle-Font-Size="12" DataField="actions" UniqueName="actions">
                                                                <ItemTemplate>
                                                                    <a id="aEditTaskOvrd" onserverclick="BtnEdit_OnClick" runat="server">
                                                                        <i class="icon-arrow-right font-lg font-blue"></i>
                                                                    </a>
                                                                    <a id="aDeleteTaskOvrd" onserverclick="BtnDelete_OnClick" runat="server">
                                                                        <i class="icon-close font-lg font-green-soft"></i>
                                                                    </a> 
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                <div class="col-md-3">
                                                </div>
                                                <div id="divNoResultsOvrd" runat="server" visible="false" class="col-md-6 alert alert-warning" style="text-align:center; margin-top:30px;">
                                                    <strong>
                                                        <asp:Label ID="LblNoResultsTitleOvrd" Text="0 Tasks! " runat="server" />
                                                    </strong>
                                                    <asp:Label ID="LblNoResultsContentOvrd" Text="There are no Tasks yet, create your first one!" runat="server" />
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
                </div>
            </div>
        </div>        
    </telerik:RadAjaxPanel>    
    <div id="EditTask" class="modal fade" tabindex="-1" data-width="500">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title"><asp:Label ID="LblEditTaskTitle" CssClass="control-label" runat="server" /></h4>
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
                        <div id="divTaskEditFailure" runat="server" visible="false" class="col-md-12 alert alert-danger" style="text-align:center">
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
                            <h4 class="modal-title"><asp:Label ID="LblConfTitle" CssClass="control-label" runat="server" /></h4>
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
        function CloseEditPopUp() {
            $('#EditTask').modal('hide');
        }
    </script>
</asp:Content>
