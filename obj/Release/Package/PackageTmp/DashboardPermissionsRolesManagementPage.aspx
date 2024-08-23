<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardPermissionsRolesManagementPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardPermissionsRolesManagementPage" %>

<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>

    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="card card-custom example example-compact">
                        <div class="card-header">
                            <h3 class="card-title">
                                <asp:Label ID="LblCreateRoles" Text="Create your roles" runat="server" />
                            </h3>
                        </div>

                        <div id="divRoleManagementArea" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-lg-4">
                                                    <asp:Label ID="Label1" Text="Existing roles: " runat="server" />
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="DrpDefaultRoles" CssClass="form-control" Width="400" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mt-8">
                                            <div class="col-lg-12">

                                                <div id="divNewRolesArea" runat="server" visible="false">
                                                    <asp:Repeater ID="RdgRoles" Visible="false" runat="server">
                                                        <ItemTemplate>
                                                            <div id="divRow" runat="server" class="form-group row">
                                                                <asp:HiddenField ID="HdnRoleID" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' runat="server" />
                                                                <div data-repeater-list="" class="col-lg-12">
                                                                    <div data-repeater-item="" class="form-group row">
                                                                        <div class="col-lg-2">
                                                                            <div class="input-group">
                                                                                <div class="input-group-prepend">
                                                                                    <span class="input-group-text">
                                                                                        <i class="flaticon-avatar"></i>
                                                                                    </span>
                                                                                </div>
                                                                                <asp:TextBox ID="TbxRoleName" Text='<%# DataBinder.Eval(Container.DataItem, "role_name")%>' placeholder="Role Name" CssClass="form-control" MaxLength="45" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-3">
                                                                            <div class="input-group">
                                                                                <div class="input-group-prepend">
                                                                                    <span class="input-group-text">
                                                                                        <i class="flaticon-interface-11"></i>
                                                                                    </span>
                                                                                </div>
                                                                                <asp:TextBox ID="TbxDescription" Text='<%# DataBinder.Eval(Container.DataItem, "role_description")%>' placeholder="Description" CssClass="form-control" MaxLength="45" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-1">
                                                                            <a id="aRemoveRole" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                                <i class="la la-remove"></i>
                                                                            </a>
                                                                            <a id="aAddRole" onserverclick="aAddRole_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                                                <i class="flaticon2-add"></i>
                                                                            </a>
                                                                        </div>
                                                                        <div class="col-lg-6"></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>

                                                <div id="divOldRolesArea" runat="server">
                                                    <div id="divRow1" runat="server" class="form-group row">
                                                        <asp:HiddenField ID="HdnRoleID1" Value="0" runat="server" />
                                                        <div data-repeater-list="" class="col-lg-12">
                                                            <div data-repeater-item="" class="form-group row">
                                                                <div class="col-lg-2">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-avatar"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TbxRoleName" placeholder="Role Name" CssClass="form-control" MaxLength="45" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-interface-11"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TbxDescription" placeholder="Description" CssClass="form-control" MaxLength="45" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-1">
                                                                    <a id="ImgBtnRemoveRole" onserverclick="ImgBtnRemoveRole_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                        <i class="la la-remove"></i>
                                                                    </a>
                                                                    <a id="ImgBtnAddRole" onserverclick="aAddNewRole_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                                        <i class="flaticon2-add"></i>
                                                                    </a>
                                                                </div>
                                                                <div class="col-lg-6"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divRow2" runat="server" visible="false" class="form-group row">
                                                        <asp:HiddenField ID="HdnRoleID2" Value="0" runat="server" />
                                                        <div data-repeater-list="" class="col-lg-12">
                                                            <div data-repeater-item="" class="form-group row">
                                                                <div class="col-lg-2">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-avatar"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox1" placeholder="Role Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-interface-11"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox2" placeholder="Description" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-1">
                                                                    <a id="ImgBtnRemove1" onserverclick="ImgBtnRemove1_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                        <i class="la la-remove"></i>
                                                                    </a>
                                                                    <a id="ImgBtnAddRole1" onserverclick="aAddNewRole_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                                        <i class="flaticon2-add"></i>
                                                                    </a>
                                                                </div>
                                                                <div class="col-lg-6"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divRow3" runat="server" visible="false" class="form-group row">
                                                        <asp:HiddenField ID="HdnRoleID3" Value="0" runat="server" />
                                                        <div data-repeater-list="" class="col-lg-12">
                                                            <div data-repeater-item="" class="form-group row">
                                                                <div class="col-lg-2">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-avatar"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox4" placeholder="Role Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-interface-11"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox5" placeholder="Description" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-1">
                                                                    <a id="ImgBtnRemove2" onserverclick="ImgBtnRemove2_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                        <i class="la la-remove"></i>
                                                                    </a>
                                                                    <a id="ImgBtnAddRole2" onserverclick="aAddNewRole_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                                        <i class="flaticon2-add"></i>
                                                                    </a>
                                                                </div>
                                                                <div class="col-lg-6"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divRow4" runat="server" visible="false" class="form-group row">
                                                        <asp:HiddenField ID="HdnRoleID4" Value="0" runat="server" />
                                                        <div data-repeater-list="" class="col-lg-12">
                                                            <div data-repeater-item="" class="form-group row">
                                                                <div class="col-lg-2">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-avatar"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox7" placeholder="Role Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-interface-11"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox8" placeholder="Description" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-1">
                                                                    <a id="ImgBtnRemove3" onserverclick="ImgBtnRemove3_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                        <i class="la la-remove"></i>
                                                                    </a>
                                                                    <a id="ImgBtnAddRole3" onserverclick="aAddNewRole_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                                        <i class="flaticon2-add"></i>
                                                                    </a>
                                                                </div>
                                                                <div class="col-lg-6"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divRow5" runat="server" visible="false" class="form-group row">
                                                        <asp:HiddenField ID="HdnRoleID5" Value="0" runat="server" />
                                                        <div data-repeater-list="" class="col-lg-12">
                                                            <div data-repeater-item="" class="form-group row">
                                                                <div class="col-lg-2">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-avatar"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox10" placeholder="Role Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-interface-11"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox11" placeholder="Description" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-1">
                                                                    <a id="ImgBtnRemove4" onserverclick="ImgBtnRemove4_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                        <i class="la la-remove"></i>
                                                                    </a>
                                                                    <a id="ImgBtnAddRole4" onserverclick="aAddNewRole_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                                        <i class="flaticon2-add"></i>
                                                                    </a>
                                                                </div>
                                                                <div class="col-lg-6"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divRow6" runat="server" visible="false" class="form-group row">
                                                        <asp:HiddenField ID="HdnRoleID6" Value="0" runat="server" />
                                                        <div data-repeater-list="" class="col-lg-12">
                                                            <div data-repeater-item="" class="form-group row">
                                                                <div class="col-lg-2">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-avatar"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox12" placeholder="Role Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-interface-11"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox13" placeholder="Description" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-1">
                                                                    <a id="ImgBtnRemove5" onserverclick="ImgBtnRemove5_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                        <i class="la la-remove"></i>
                                                                    </a>
                                                                    <a id="ImgBtnAddRole5" onserverclick="aAddNewRole_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                                        <i class="flaticon2-add"></i>
                                                                    </a>
                                                                </div>
                                                                <div class="col-lg-6"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divRow7" runat="server" visible="false" class="form-group row">
                                                        <asp:HiddenField ID="HdnRoleID7" Value="0" runat="server" />
                                                        <div data-repeater-list="" class="col-lg-12">
                                                            <div data-repeater-item="" class="form-group row">
                                                                <div class="col-lg-2">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-avatar"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox14" placeholder="Role Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-interface-11"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox15" placeholder="Description" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-1">
                                                                    <a id="ImgBtnRemove6" onserverclick="ImgBtnRemove6_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                        <i class="la la-remove"></i>
                                                                    </a>
                                                                    <a id="ImgBtnAddRole6" onserverclick="aAddNewRole_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                                        <i class="flaticon2-add"></i>
                                                                    </a>
                                                                </div>
                                                                <div class="col-lg-6"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divRow8" runat="server" visible="false" class="form-group row">
                                                        <asp:HiddenField ID="HdnRoleID8" Value="0" runat="server" />
                                                        <div data-repeater-list="" class="col-lg-12">
                                                            <div data-repeater-item="" class="form-group row">
                                                                <div class="col-lg-2">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-avatar"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox16" placeholder="Role Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-interface-11"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox17" placeholder="Description" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-1">
                                                                    <a id="ImgBtnRemove7" onserverclick="ImgBtnRemove7_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                        <i class="la la-remove"></i>
                                                                    </a>
                                                                    <a id="ImgBtnAddRole7" onserverclick="aAddNewRole_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                                        <i class="flaticon2-add"></i>
                                                                    </a>
                                                                </div>
                                                                <div class="col-lg-6"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divRow9" runat="server" visible="false" class="form-group row">
                                                        <asp:HiddenField ID="HdnRoleID9" Value="0" runat="server" />
                                                        <div data-repeater-list="" class="col-lg-12">
                                                            <div data-repeater-item="" class="form-group row">
                                                                <div class="col-lg-2">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-avatar"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox18" placeholder="Role Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-interface-11"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox19" placeholder="Description" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-1">
                                                                    <a id="ImgBtnRemove8" onserverclick="ImgBtnRemove8_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                        <i class="la la-remove"></i>
                                                                    </a>
                                                                    <a id="ImgBtnAddRole8" onserverclick="aAddNewRole_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                                        <i class="flaticon2-add"></i>
                                                                    </a>
                                                                </div>
                                                                <div class="col-lg-6"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divRow10" runat="server" visible="false" class="form-group row">
                                                        <asp:HiddenField ID="HdnRoleID10" Value="0" runat="server" />
                                                        <div data-repeater-list="" class="col-lg-12">
                                                            <div data-repeater-item="" class="form-group row">
                                                                <div class="col-lg-2">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-avatar"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox20" placeholder="Role Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text">
                                                                                <i class="flaticon-interface-11"></i>
                                                                            </span>
                                                                        </div>
                                                                        <asp:TextBox ID="TextBox21" placeholder="Description" CssClass="form-control" MaxLength="45" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-1">
                                                                    <a id="ImgBtnRemove9" onserverclick="ImgBtnRemove9_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                                        <i class="la la-remove"></i>
                                                                    </a>
                                                                    <a id="ImgBtnAddRole9" onserverclick="aAddNewRole_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                                        <i class="flaticon2-add"></i>
                                                                    </a>
                                                                </div>
                                                                <div class="col-lg-6"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-lg-3"></div>
                                                    <div class="col">
                                                        <a id="aAddNewRole" runat="server" visible="false" role="button" onserverclick="aAddNewRole_ServerClick" data-repeater-create="" class="btn font-weight-bold btn-primary">
                                                            <asp:Label ID="LblAddNewRole" Text="Add new role" runat="server" />
                                                        </a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer">
                                        <div class="row">
                                            <div class="col-lg-3"></div>
                                            <div class="col-lg-6">
                                                <asp:Button ID="RBtnSave" OnClick="RBtnSave_Click" Text="Save" CssClass="btn font-weight-bold btn-primary btn-shadow mr-2" runat="server" />
                                                <asp:Button ID="RBtnCancel" OnClick="RBtnCancel_Click" Text="Cancel" CssClass="btn font-weight-bold btn-secondary btn-shadow" runat="server" />
                                            </div>
                                            <div class="col-lg-3"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <controls:MessageControl ID="UcMessageAlertControl" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divRolePermissionsArea" visible="false" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-header">
                                        <h3 class="card-title">
                                            <asp:Label ID="LblPermissions" Text="Manage Roles" runat="server" />
                                        </h3>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-lg-4">
                                                    <asp:Label ID="LblSelectRoles" Text="Select role: " runat="server" />
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList AutoPostBack="true" ID="DrpRoles" CssClass="form-control" Width="400" OnSelectedIndexChanged="DrpRoles_SelectedIndexChanged" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                                <telerik:RadGrid ID="RdgFormPermissions" ShowHeader="true" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowPaging="false" AllowSorting="false" PagerStyle-Position="Bottom" CssClass="table table-separate table-head-custom table-checkable" PageSize="10"
                                                    OnItemDataBound="RdgFormPermissions_OnItemDataBound" OnNeedDataSource="RdgFormPermissions_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                                    <MasterTableView CssClass="table-status">
                                                        <Columns>
                                                            <telerik:GridBoundColumn Display="false" DataField="form_id" UniqueName="form_id" />
                                                            <telerik:GridBoundColumn HeaderText="Page name" DataField="form_title" UniqueName="form_title" />
                                                            <telerik:GridBoundColumn Display="false" DataField="actionID1" UniqueName="actionID1" />
                                                            <telerik:GridTemplateColumn UniqueName="action1">
                                                                <ItemTemplate>
                                                                    <div id="divNotExistAction1" runat="server" visible="false" class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <i class="icon-2x text-dark-50 flaticon-more-v4"></i>
                                                                    </div>
                                                                    <div class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <asp:CheckBox ID="CbxSelectAction1" AutoPostBack="true" OnCheckedChanged="CbxSelectAction_CheckedChanged" runat="server" />
                                                                        <asp:Label ID="LblDescription1" Visible="false" runat="server" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn Display="false" DataField="actionID2" UniqueName="actionID2" />
                                                            <telerik:GridTemplateColumn UniqueName="action2">
                                                                <ItemTemplate>
                                                                    <div id="divNotExistAction2" runat="server" visible="false" class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <i class="icon-2x text-dark-50 flaticon-more-v4"></i>
                                                                    </div>
                                                                    <div class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <asp:CheckBox ID="CbxSelectAction2" runat="server" />
                                                                        <asp:Label ID="LblDescription2" Visible="false" runat="server" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn Display="false" DataField="actionID3" UniqueName="actionID3" />
                                                            <telerik:GridTemplateColumn UniqueName="action3">
                                                                <ItemTemplate>
                                                                    <div id="divNotExistAction3" runat="server" visible="false" class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <i class="icon-2x text-dark-50 flaticon-more-v4"></i>
                                                                    </div>
                                                                    <div class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <asp:CheckBox ID="CbxSelectAction3" runat="server" />
                                                                        <asp:Label ID="LblDescription3" Visible="false" runat="server" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn Display="false" DataField="actionID4" UniqueName="actionID4" />
                                                            <telerik:GridTemplateColumn UniqueName="action4">
                                                                <ItemTemplate>
                                                                    <div id="divNotExistAction4" runat="server" visible="false" class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <i class="icon-2x text-dark-50 flaticon-more-v4"></i>
                                                                    </div>
                                                                    <div class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <asp:CheckBox ID="CbxSelectAction4" runat="server" />
                                                                        <asp:Label ID="LblDescription4" Visible="false" runat="server" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn Display="false" DataField="actionID5" UniqueName="actionID5" />
                                                            <telerik:GridTemplateColumn UniqueName="action5">
                                                                <ItemTemplate>
                                                                    <div id="divNotExistAction5" runat="server" visible="false" class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <i class="icon-2x text-dark-50 flaticon-more-v4"></i>
                                                                    </div>
                                                                    <div class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <asp:CheckBox ID="CbxSelectAction5" runat="server" />
                                                                        <asp:Label ID="LblDescription5" Visible="false" runat="server" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn Display="false" DataField="actionID6" UniqueName="actionID6" />
                                                            <telerik:GridTemplateColumn UniqueName="action6">
                                                                <ItemTemplate>
                                                                    <div id="divNotExistAction6" runat="server" visible="false" class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <i class="icon-2x text-dark-50 flaticon-more-v4"></i>
                                                                    </div>
                                                                    <div class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <asp:CheckBox ID="CbxSelectAction6" runat="server" />
                                                                        <asp:Label ID="LblDescription6" Visible="false" runat="server" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn Display="false" DataField="actionID7" UniqueName="actionID7" />
                                                            <telerik:GridTemplateColumn UniqueName="action7">
                                                                <ItemTemplate>
                                                                    <div id="divNotExistAction7" runat="server" visible="false" class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <i class="icon-2x text-dark-50 flaticon-more-v4"></i>
                                                                    </div>
                                                                    <div class="mr-4 flex-shrink-0 text-center" style="width: 40px;">
                                                                        <asp:CheckBox ID="CbxSelectAction7" runat="server" />
                                                                        <asp:Label ID="LblDescription7" Visible="false" runat="server" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer">
                                        <div class="row">
                                            <div class="col-lg-3"></div>
                                            <div class="col-lg-6">
                                                <asp:Button ID="RBtnSavePermissions" OnClick="RBtnSavePermissions_Click" Text="Save" CssClass="btn font-weight-bold btn-primary btn-shadow mr-2" runat="server" />
                                                <asp:Button ID="RBtnCancelPermissions" OnClick="RBtnCancelPermissions_Click" Text="Cancel" CssClass="btn font-weight-bold btn-secondary btn-shadow" runat="server" />
                                            </div>
                                            <div class="col-lg-3"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <controls:MessageControl ID="UcMessagePermissionsAlertControl" runat="server" />
                                            </div>
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

    <!-- Payment form (modal view) -->
    <div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <controls:UcStripe ID="UcStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Pop Up form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblMessageAlertTitle" Text="Information Message" runat="server" />
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblMessageAlertfMsg" Text="Save these roles first in order to add more" runat="server" />
                                        <asp:HiddenField ID="HdnRoleID" Value="0" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnRemoveRole" Text="Proceed" OnClick="BtnRemoveRole_Click" Visible="false" CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <style>
            .RadGrid_MetroTouch .rgAltRow {
                background-color: transparent !important;
            }

            .RadGrid_MetroTouch .rgRow {
                background-color: transparent !important;
            }
        </style>

        <script src="/assets/js/pages/crud/forms/widgets/jquery-1.11.1-repeater-custom.js"></script>
        <script src="/assets/js/pages/crud/forms/widgets/jquery.repeater-custom.js"></script>

        <script type="text/javascript">
            // Class definition
            var KTFormRepeater = function () {

                // Private functions
                var demo3 = function () {
                    $('#kt_repeater_3').repeater({
                        initEmpty: false,

                        defaultValues: {
                            'text-input': 'foo'
                        },

                        show: function () {
                            $(this).slideDown();
                        },

                        hide: function (deleteElement) {
                            if (confirm('Are you sure you want to delete this element?')) {
                                $(this).slideUp(deleteElement);
                            }
                        }
                    });
                }

                return {
                    // public functions
                    init: function () {
                        demo3();
                    }
                };
            }();

            jQuery(document).ready(function () {
                KTFormRepeater.init();
            });
        </script>



        <!--begin::Page Scripts(used by this page)-->
        <script src="/assets/js/pages/crud/forms/widgets/form-repeater.js"></script>
        <!--end::Page Scripts-->

        <script type="text/javascript">

            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }
        </script>

    </telerik:RadScriptBlock>
</asp:Content>
