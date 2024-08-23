<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardOpportunitiesAddEditPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardOpportunitiesAddEditPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="card card-custom gutter-b example example-compact">
                        <div class="card-header">
                            <h3 class="card-title">
                                <asp:Label ID="LblHeaderInfo" Text="Add / Edit Opportunity" runat="server" />
                            </h3>
                            <div class="card-toolbar">
                                keep your opportunities list updated...
                                <asp:ImageButton ID="ImgBtnExportCsv" OnClick="BtnExportToCsv_OnClick" CssClass="btn btn alert-light-danger mr-2" ToolTip="Download to csv" ImageUrl="~/images/csv_image.PNG" runat="server" />
                            </div>
                        </div>

                        <div class="card-body">
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Opportunity status:</label>
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-map-pin"></i></span></div>
                                        <asp:DropDownList ID="DdlOppStatus" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                        </div>
                        <div class="card-header" style="margin-bottom: 0px;">
                            <h3 class="card-title">
                                <asp:Label ID="LblDealPersonalTitle" Text="Personal Information" runat="server" />
                            </h3>
                            <div class="card-toolbar">
                            </div>
                        </div>
                        <asp:HiddenField ID="TbxId" Value="0" runat="server" />
                        <asp:HiddenField ID="TbxUserId" Value="0" runat="server" />

                        <div class="card-body">
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">First Name:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxFirstName" placeholder="Customer's First Name" CssClass="form-control" MaxLength="45" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Last Name:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxLastName" placeholder="Customer's Last Name" CssClass="form-control" MaxLength="45" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="flaticon2-phone"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Organization:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxOrganiz" placeholder="Customer's Company Name" CssClass="form-control" MaxLength="95" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Role / Position:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxPosition" placeholder="Customer's Company Address" CssClass="form-control" MaxLength="145" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="flaticon2-phone"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                        </div>

                        <div class="card-header" style="margin-bottom: 0px;">
                            <h3 class="card-title">
                                <asp:Label ID="Label1" Text="Contact Information" runat="server" />
                            </h3>
                            <div class="card-toolbar">
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Email:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxEmail" placeholder="Customer's Company Email" CssClass="form-control" MaxLength="95" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Website:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxWebsite" placeholder="Customer's Company Website" CssClass="form-control" MaxLength="95" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="flaticon2-phone"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Address:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxAddress" placeholder="Customer's Company Product" CssClass="form-control" MaxLength="145" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="flaticon2-phone"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Phone:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxPhone" placeholder="Customer's Company Phone" CssClass="form-control" MaxLength="45" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-2 col-form-label text-right">Linkedin:</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxLinkedin" placeholder="Linkedin" CssClass="form-control" MaxLength="145" runat="server"></asp:TextBox>
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                                <label class="col-lg-2 col-form-label text-right">Twitter:</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="TbxTwitter" placeholder="Linkedin" CssClass="form-control" MaxLength="145" runat="server"></asp:TextBox>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-4"></div>
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <a id="aAddNote" onserverclick="BtnOpenNote_OnClick" class="btn btn-success font-weight-bold mr-12" runat="server">
                                            <i class="fa fa-comment-alt"></i>
                                            <asp:Label ID="LblAddOppNote" runat="server" />
                                        </a>
                                        <a id="aAddTask" onserverclick="BtnOpenTask_OnClick" class="btn btn-danger font-weight-bold mr-2" runat="server">
                                            <i class="fas fa-tasks"></i>
                                            <asp:Label ID="LblAddOppTask" runat="server" />
                                        </a>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                </div>
                            </div>

                        </div>
                        <div class="card-footer">
                            <div class="row">
                                <div class="col-lg-10">
                                    <asp:Button ID="RbtnDelete" OnClick="BtnDelete_OnClick" Text="Delete" Visible="false" CssClass="btn btn-danger mr-2" runat="server" />
                                    <asp:Button ID="RbtnClear" OnClick="RbtnClear_OnClick" Text="Clear" CssClass="btn btn-light-primary" runat="server" />
                                    <asp:Button ID="RbtnEdit" OnClick="RbtnEdit_OnClick" Text="Edit" CssClass="btn btn-secondary mr-2" runat="server" />
                                    <asp:Button ID="RbtnCancel" Visible="false" OnClick="RbtnCancel_OnClick" Text="Cancel" CssClass="btn btn-secondary mr-2" runat="server" />
                                    <asp:Button ID="RbtnSave" OnClick="RbtnSave_OnClick" Text="Save" CssClass="btn btn-success mr-2" runat="server" />
                                </div>
                                <div class="col-lg-2 text-right">
                                    <asp:Button ID="RbtnBack" Visible="false" OnClick="RbtnBack_OnClick" Text="Back to Opportunities" CssClass="btn btn-secondary mr-2" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="UcMessageAlert" runat="server" />
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

    <div id="AddNote" class="modal fade" tabindex="-1">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblNoteTitle" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group row">
                                <div class="col-lg-12">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxSubject" MaxLength="145" EnableViewState="true" ViewStateMode="Enabled" CssClass="form-control" placeholder="Enter note subject here" data-placement="top" runat="server" />
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-12">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxMsg" MaxLength="3995" TextMode="MultiLine" Rows="10" CssClass="form-control form-control-lg form-control" placeholder="Enter note content here" data-placement="top" runat="server" />
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
                                <div id="divGeneralFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblGeneralFailure" Text="Error" runat="server" />
                                        </strong>
                                        <asp:Label ID="LblFailure" runat="server" />
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
                            <asp:Button ID="BtnDiscardNote" OnClick="BtnDiscardNote_OnClick" CssClass="btn btn-light-primary" runat="server" Text="Discard" />
                            <asp:Button ID="BtnAddNote" OnClick="BtnAddNote_OnClick" CssClass="btn btn-primary" runat="server" Text="OK" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="AddTask" class="modal fade" tabindex="-1" data-width="500">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblTaskTitle" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group row">
                                <div class="col-lg-12">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxTaskSubject" MaxLength="145" CssClass="form-control" placeholder="Enter subject here" data-placement="top" runat="server" />
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-12">
                                    <div class="input-group">
                                        <asp:TextBox ID="TbxTaskContent" MaxLength="3995" TextMode="MultiLine" Rows="10" CssClass="form-control form-control-lg form-control" placeholder="Enter task content here" data-placement="top" runat="server" />
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-lg-3 col-form-label">Task date</label>
                                <div class="col-lg-9">
                                    <div class="input-group">
                                        <telerik:RadDateTimePicker ID="RdpRemindDate" Width="290" ZIndex="11500" runat="server" />
                                        <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                    </div>
                                    <span class="form-text text-muted"></span>
                                </div>
                            </div>
                            <div class="row">
                                <div id="divTaskSuccess" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblTaskSuccess" Text="Done! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblTaskSuccessContent" runat="server" />
                                    </div>
                                    <div class="alert-close">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">
                                                <i class="ki ki-close"></i>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                                <div id="divTaskFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                    <div class="alert-icon">
                                        <i class="flaticon-warning"></i>
                                    </div>
                                    <div class="alert-text">
                                        <strong>
                                            <asp:Label ID="LblTaskError" Text="Error! " runat="server" />
                                        </strong>
                                        <asp:Label ID="LblTaskErrorContent" runat="server" />
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
                            <asp:Button ID="BtnDiscardTask" OnClick="BtnDiscardTask_OnClick" CssClass="btn btn-light-primary" runat="server" Text="Discard" />
                            <asp:Button ID="BtnAddTask" OnClick="BtnAddTask_OnClick" CssClass="btn btn-primary" runat="server" Text="OK" />
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
                                    <asp:Label ID="LblConfMsg" Text="Delete this opportunity permanently?" runat="server" />
                                    <asp:HiddenField ID="TbxOpportConfId" Value="0" runat="server" />
                                    <asp:HiddenField ID="TbxOpportAction" Value="0" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-light-primary">No</button>
                            <asp:Button ID="RbtnConfirmDelete" OnClick="RbtnConfirmDelete_OnClick" CssClass="btn btn-primary" Text="Yes" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        function OpenConfPopUp() {
            $('#divConfirm').modal('show');
        }
    </script>
    <script type="text/javascript">
        function CloseConfPopUp() {
            $('#divConfirm').modal('hide');
        }
    </script>
    <script type="text/javascript">
        function CloseNotePopUp() {
            $('#AddNote').modal('hide');
        }
    </script>
    <script type="text/javascript">
        function CloseTaskPopUp() {
            $('#AddTask').modal('hide');
        }
    </script>
    <script type="text/javascript">
        function OpenNotePopUp() {
            $('#AddNote').modal('show');
        }
    </script>
    <script type="text/javascript">
        function OpenTaskPopUp() {
            $('#AddTask').modal('show');
        }
    </script>

</asp:Content>
