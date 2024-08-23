<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardTeamEditPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardTeamEditPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RapPage" runat="server" RestoreOriginalRenderDelegate="false">
        <!--begin::Entry-->
        <div class="d-flex flex-column-fluid">
            <!--begin::Container-->
            <div class="container">
                <asp:UpdatePanel runat="server" ID="UpdatePnl" UpdateMode="Conditional">
                    <ContentTemplate>
                        <!--begin::Dashboard-->
                        <div class="card card-custom">
                            <div class="card-header">
                                <div class="card-title">
                                    <span class="card-icon">
                                        <i class="flaticon2-chart text-primary"></i>
                                    </span>
                                    <h3 class="card-label">
                                        <asp:Label ID="LblName" Text="Complete your sub-account!" runat="server" />
                                    </h3>
                                </div>
                                <div class="card-toolbar">
                                    <asp:Label ID="LblDashSubTitle" runat="server" />
                                </div>
                            </div>
                            <div class="card-body">
                                <h3 class="font-size-lg text-dark font-weight-bold mb-6">Sub-Account Details Info:</h3>
                                <div class="mb-15">
                                    <div class="form-group row">
                                        <label class="col-lg-3 col-form-label text-right">Email:</label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="RtbxEmail" CssClass="form-control" placeholder="Enter email" MaxLength="45" runat="server" />
                                            <span class="form-text text-muted">Please enter email</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-3 col-form-label text-right">Change Password:</label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="RtbxPassword" CssClass="form-control" placeholder="Enter password" MaxLength="45" runat="server" />
                                            <span class="form-text text-muted">Please enter your password</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-3 col-form-label text-right">Retype Password:</label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="RtbxRetypePassword" CssClass="form-control" placeholder="Enter password again" MaxLength="45" runat="server" />
                                            <span class="form-text text-muted">Please retype password</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-3 col-form-label text-right">Last name:</label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="RtbxLastName" CssClass="form-control" placeholder="Enter last name" MaxLength="45" runat="server" />
                                            <span class="form-text text-muted">Please enter last name</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-3 col-form-label text-right">First name:</label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="RtbxFirstName" CssClass="form-control" placeholder="Enter first name" MaxLength="20" runat="server" />
                                            <span class="form-text text-muted">Please enter first name</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-3 col-form-label text-right">Job position:</label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="RtbxJobPosition" CssClass="form-control" placeholder="Enter job position" MaxLength="20" runat="server" />
                                            <span class="form-text text-muted">Please enter job position</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-3 col-form-label text-right">Personal image:</label>
                                        <div class="col-lg-6">
                                            <telerik:RadAsyncUpload ID="PersonalImage" RenderMode="Lightweight" Width="257" OnFileUploaded="PersonalImage_OnFileUploaded" MaxFileSize="100000" AllowedFileExtensions="png,jpg,jpeg,gif" MaxFileInputsCount="1" runat="server" />
                                            <span class="form-text text-muted">Please upoad personal image</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-3 col-form-label text-right">Activate account:</label>
                                        <div class="col-lg-6 checkbox-inline ml-4">
                                            <label class="checkbox checkbox-primary">
                                                <input id="CbxActive" runat="server" type="checkbox" checked="checked" />
                                                Is Active
                                                <span></span>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-3 col-form-label text-right">Select Role:</label>
                                        <div class="col-lg-7 checkbox-inline ml-2">
                                            <div class="col-lg-12">
                                                <div id="div1" class="col-lg-4" runat="server">
                                                    <label class="checkbox checkbox-circle checkbox-primary">
                                                        <input id="CbxRl1" value="0" runat="server" type="checkbox">
                                                        <asp:Label ID="LblCbx1" Style="display: contents;" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>
                                                <div id="div2" class="col-lg-4" runat="server">
                                                    <label class="checkbox checkbox-circle checkbox-primary">
                                                        <input id="CbxRl2" value="0" runat="server" type="checkbox">
                                                        <asp:Label ID="LblCbx2" Style="display: contents;" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>
                                                <div id="div3" class="col-lg-4" runat="server">
                                                    <label class="checkbox checkbox-circle checkbox-primary">
                                                        <input id="CbxRl3" value="0" runat="server" type="checkbox">
                                                        <asp:Label ID="LblCbx3" Style="display: contents;" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div id="div4" class="col-lg-4" runat="server">
                                                    <label class="checkbox checkbox-circle checkbox-primary">
                                                        <input id="CbxRl4" value="0" runat="server" type="checkbox">
                                                        <asp:Label ID="LblCbx4" Style="display: contents;" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>
                                                <div id="div5" class="col-lg-4" runat="server">
                                                    <label class="checkbox checkbox-circle checkbox-primary">
                                                        <input id="CbxRl5" value="0" runat="server" type="checkbox">
                                                        <asp:Label ID="LblCbx5" Style="display: contents;" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>
                                                <div id="div6" class="col-lg-4" runat="server">
                                                    <label class="checkbox checkbox-circle checkbox-primary">
                                                        <input id="CbxRl6" visible="false" value="0" runat="server" type="checkbox">
                                                        <asp:Label ID="LblCbx6" Style="display: contents;" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div id="div7" class="col-lg-4" runat="server">
                                                    <label class="checkbox checkbox-circle checkbox-primary">
                                                        <input id="CbxRl7" visible="false" value="0" runat="server" type="checkbox">
                                                        <asp:Label ID="LblCbx7" Style="display: contents;" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>
                                                <div id="div8" class="col-lg-4" runat="server">
                                                    <label class="checkbox checkbox-circle checkbox-primary">
                                                        <input id="CbxRl8" visible="false" value="0" runat="server" type="checkbox">
                                                        <asp:Label ID="LblCbx8" Style="display: contents;" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>
                                                <div id="div9" class="col-lg-4" runat="server">
                                                    <label class="checkbox checkbox-circle checkbox-primary">
                                                        <input id="CbxRl9" visible="false" value="0" runat="server" type="checkbox">
                                                        <asp:Label ID="LblCbx9" Style="display: contents;" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div id="div10" class="col-lg-4" runat="server">
                                                    <label class="checkbox checkbox-circle checkbox-primary">
                                                        <input id="CbxRl10" visible="false" value="0" runat="server" type="checkbox">
                                                        <asp:Label ID="LblCbx10" Style="display: contents;" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                </div>
                                <controls:MessageControl ID="UcMessage" runat="server" />
                            </div>
                            <div class="card-footer">
                                <div class="row">
                                    <div class="col-12">
                                        <a id="aRbtnBack" runat="server" class="btn btn-light-danger mr-2">Back to Team
                                        </a>
                                        <a id="aRbtnDelete" role="button" runat="server" onserverclick="RbtnDelete_OnClick" class="btn btn-danger mr-2">Delete
                                        </a>
                                        <asp:Button ID="BtnClearMessage" OnClick="BtnClearMessage_OnClick" Text="Clear" CssClass="btn btn-light-primary mr-2" runat="server" />
                                        <asp:Button ID="BtnComplete" OnClick="BtnComplete_OnClick" Text="Save" CssClass="btn btn-success mr-2" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end::Dashboard-->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!--end::Container-->
        </div>
        <!--end::Entry-->

        <!-- Confirmation Delete form (modal view) -->
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
                                        <asp:Label ID="LblConfMsg" Text="Are you sure you want to completely delete this sub-account?" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <controls:MessageControl ID="UcPopUpConfirmationMessageAlert" runat="server" />
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" data-dismiss="modal" class="btn btn-danger">No</button>
                                <a id="aBtnConfDelete" runat="server" onserverclick="BtnConfDelete_OnClick" class="btn btn-primary">Yes
                                </a>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </telerik:RadAjaxPanel>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">

            function UpdateCheckBoxes() {
                alert('1');
                var cbx1 = document.getElementById('<%= CbxRl1.ClientID%>').value;
                alert(cbx1.value);
                var cbx2 = document.getElementById('<%= CbxRl2.ClientID%>').value;
                var cbx3 = document.getElementById('<%= CbxRl3.ClientID%>').value;
                var cbx4 = document.getElementById('<%= CbxRl4.ClientID%>').value;
                var cbx5 = document.getElementById('<%= CbxRl5.ClientID%>').value;
                var cbx6 = document.getElementById('<%= CbxRl6.ClientID%>').value;
                var cbx7 = document.getElementById('<%= CbxRl7.ClientID%>').value;
                var cbx8 = document.getElementById('<%= CbxRl8.ClientID%>').value;
                var cbx9 = document.getElementById('<%= CbxRl9.ClientID%>').value;
                var cbx10 = document.getElementById('<%= CbxRl10.ClientID%>').value;
                alert('2');
                if (cbx1.value == "0") {
                    alert('3');
                    cbx1.checked = true;
                    cbx1.value = "1";
                }
                else {
                    alert('4');
                    cbx1.checked = false;
                    cbx1.value = "0";
                }

                if (cbx2 == "0") {
                    cbx2.checked = true;
                    cbx2.value = "1";
                }
                else {
                    cbx2.checked = false;
                    cbx2.value = "0";
                }

                if (cbx3 == "0") {
                    cbx3.checked = true;
                    cbx3.value = "1";
                }
                else {
                    cbx3.checked = false;
                    cbx3.value = "0";
                }

                if (cbx4 == "0") {
                    cbx4.checked = true;
                    cbx4.value = "1";
                }
                else {
                    cbx4.checked = false;
                    cbx4.value = "0";
                }

                if (cbx5 == "0") {
                    cbx5.checked = true;
                    cbx5.value = "1";
                }
                else {
                    cbx5.checked = false;
                    cbx5.value = "0";
                }

                if (cbx6 == "0") {
                    cbx6.checked = true;
                    cbx6.value = "1";
                }
                else {
                    cbx6.checked = false;
                    cbx6.value = "0";
                }

                if (cbx7 == "0") {
                    cbx7.checked = true;
                    cbx7.value = "1";
                }
                else {
                    cbx7.checked = false;
                    cbx7.value = "0";
                }

                if (cbx8 == "0") {
                    cbx8.checked = true;
                    cbx8.value = "1";
                }
                else {
                    cbx8.checked = false;
                    cbx8.value = "0";
                }

                if (cbx9 == "0") {
                    cbx9.checked = true;
                    cbx9.value = "1";
                }
                else {
                    cbx9.checked = false;
                    cbx9.value = "0";
                }

                if (cbx10 == "0") {
                    cbx10.checked = true;
                    cbx10.value = "1";
                }
                else {
                    cbx10.checked = false;
                    cbx10.value = "0";
                }
            }

            function SetChecked() {

                var isChkd1 = '<%=IsCbx1Ckecked%>';
                var isChkd2 = '<%=IsCbx2Ckecked%>';
                var isChkd3 = '<%=IsCbx3Ckecked%>';
                var isChkd4 = '<%=IsCbx4Ckecked%>';
                var isChkd5 = '<%=IsCbx5Ckecked%>';
                var isChkd6 = '<%=IsCbx6Ckecked%>';
                var isChkd7 = '<%=IsCbx7Ckecked%>';
                var isChkd8 = '<%=IsCbx8Ckecked%>';
                var isChkd9 = '<%=IsCbx9Ckecked%>';
                var isChkd10 = '<%=IsCbx10Ckecked%>';

                if (isChkd1) {
                    document.getElementById("CbxRl1").checked = true;
                }
                if (isChkd2) {
                    document.getElementById("CbxRl2").checked = true;
                }
                if (isChkd3) {
                    document.getElementById("CbxRl3").checked = true;
                }
                if (isChkd4) {
                    document.getElementById("CbxRl4").checked = true;
                }
                if (isChkd5) {
                    document.getElementById("CbxRl5").checked = true;
                }
                if (isChkd6) {
                    document.getElementById("CbxRl6").checked = true;
                }
                if (isChkd7) {
                    document.getElementById("CbxRl7").checked = true;
                }
                if (isChkd8) {
                    document.getElementById("CbxRl8").checked = true;
                }
                if (isChkd9) {
                    document.getElementById("CbxRl9").checked = true;
                }
                if (isChkd10) {
                    document.getElementById("CbxRl10").checked = true;
                }
            }

            function CloseConfirmPopUp() {
                $('#divConfirm').modal('hide');
            }

            function OpenConfirmPopUp() {
                $('#divConfirm').modal('show');
            }
        </script>

        <script type="text/javascript">
            //<![CDATA[
            Sys.Application.add_load(function () {
                window.upload = $find("<%=PersonalImage.ClientID %>");
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
                        alert("Please select file/category");
                    }
                };
            })();
        </script>

        <style>
            .RadUpload_MetroTouch .ruSelectWrap .ruFakeInput {
                border-color: #e0e0e0;
                color: #333;
                background-color: #fff;
                height: 30px;
            }

            .RadUpload_MetroTouch .ruSelectWrap .ruButton {
                border-color: #e0e0e0;
                color: #000;
                background-color: #f9f9f9;
                height: 30px;
            }
        </style>
    </telerik:RadScriptBlock>

</asp:Content>
