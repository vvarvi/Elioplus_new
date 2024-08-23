<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardTierManagementPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardTierManagementPage" %>

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
                                <asp:Label ID="LblCreateTier" Text="Create your tiers" runat="server" />
                            </h3>
                        </div>

                        <div id="divTierManagementArea" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-body">

                                        <div class="form-group row">
                                            <div class="col-lg-12">
                                                <label class="col-3 col-form-label">Annual Sales</label>
                                                <div class="col-3">
                                                    <span class="switch switch-sm switch-icon">
                                                        <label>
                                                            <input id="RdBtnRevenuesType1" runat="server" value="1" type="checkbox" checked="checked" name="select" />
                                                            <span></span>
                                                        </label>
                                                    </span>
                                                </div>
                                                <label class="col-3 col-form-label">Total Revenues</label>
                                                <div class="col-3">
                                                    <span class="switch switch-sm switch-icon">
                                                        <label>
                                                            <input id="RdBtnRevenuesType2" runat="server" value="2" type="checkbox" name="select" />
                                                            <span></span>
                                                        </label>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divRow1" runat="server" class="form-group row">
                                            <asp:HiddenField ID="HdnTierID1" Value="0" runat="server" />
                                            <div data-repeater-list="" class="col-lg-12">
                                                <div data-repeater-item="" class="form-group row">
                                                    <div class="col-lg-3">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-layers"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TbxTierName" placeholder="Tier Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon2-percentage"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TbxCommision" placeholder="Commision %" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-coins"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TbxSalesVolumeFr" placeholder="From Volume" TextMode="Number" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-coins"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TbxSalesVolume" placeholder="To Volume" TextMode="Number" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <a id="ImgBtnRemoveTier" onserverclick="ImgBtnRemoveTier_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                            <i class="la la-remove"></i>
                                                        </a>
                                                        <a id="ImgBtnAddTier" onserverclick="aAddNewTier_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                            <i class="flaticon2-add"></i>
                                                        </a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divRow2" runat="server" visible="false" class="form-group row">
                                            <asp:HiddenField ID="HdnTierID2" Value="0" runat="server" />
                                            <div data-repeater-list="" class="col-lg-12">
                                                <div data-repeater-item="" class="form-group row">
                                                    <div class="col-lg-3">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-layers"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox1" placeholder="Tier Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon2-percentage"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox2" placeholder="Commision %" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-coins"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox3Fr" placeholder="From Volume" TextMode="Number" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-coins"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox3" placeholder="To Volume" TextMode="Number" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <a id="ImgBtnRemove1" onserverclick="ImgBtnRemove1_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                            <i class="la la-remove"></i>
                                                        </a>
                                                        <a id="ImgBtnAddTier1" onserverclick="aAddNewTier_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                            <i class="flaticon2-add"></i>
                                                        </a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divRow3" runat="server" visible="false" class="form-group row">
                                            <asp:HiddenField ID="HdnTierID3" Value="0" runat="server" />
                                            <div data-repeater-list="" class="col-lg-12">
                                                <div data-repeater-item="" class="form-group row">
                                                    <div class="col-lg-3">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-layers"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox4" placeholder="Tier Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon2-percentage"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox5" placeholder="Commision %" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-coins"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox6Fr" placeholder="From Volume" TextMode="Number" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-coins"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox6" placeholder="To Volume" TextMode="Number" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <a id="ImgBtnRemove2" onserverclick="ImgBtnRemove2_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                            <i class="la la-remove"></i>
                                                        </a>
                                                        <a id="ImgBtnAddTier2" onserverclick="aAddNewTier_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                            <i class="flaticon2-add"></i>
                                                        </a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divRow4" runat="server" visible="false" class="form-group row">
                                            <asp:HiddenField ID="HdnTierID4" Value="0" runat="server" />
                                            <div data-repeater-list="" class="col-lg-12">
                                                <div data-repeater-item="" class="form-group row">
                                                    <div class="col-lg-3">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-layers"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox7" placeholder="Tier Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon2-percentage"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox8" placeholder="Commision %" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-coins"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox9Fr" placeholder="From Volume" TextMode="Number" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-coins"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox9" placeholder="To Volume" TextMode="Number" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <a id="ImgBtnRemove3" onserverclick="ImgBtnRemove3_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                            <i class="la la-remove"></i>
                                                        </a>
                                                        <a id="ImgBtnAddTier3" onserverclick="aAddNewTier_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                            <i class="flaticon2-add"></i>
                                                        </a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divRow5" runat="server" visible="false" class="form-group row">
                                            <asp:HiddenField ID="HdnTierID5" Value="0" runat="server" />
                                            <div data-repeater-list="" class="col-lg-12">
                                                <div data-repeater-item="" class="form-group row">
                                                    <div class="col-lg-3">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-layers"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox10" placeholder="Tier Name" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon2-percentage"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox11" placeholder="Commision %" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-coins"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox12Fr" placeholder="From Volume" TextMode="Number" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="flaticon-coins"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="TextBox12" placeholder="Sales Volume" TextMode="Number" CssClass="form-control" MaxLength="45" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <a id="ImgBtnRemove4" onserverclick="ImgBtnRemove4_Click" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                            <i class="la la-remove"></i>
                                                        </a>
                                                        <a id="ImgBtnAddTier4" onserverclick="aAddNewTier_ServerClick" role="button" runat="server" class="btn font-weight-bold btn-default btn-icon">
                                                            <i class="flaticon2-add"></i>
                                                        </a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-lg-3"></div>
                                            <div class="col">
                                                <a id="aAddNewTier" runat="server" visible="false" role="button" onserverclick="aAddNewTier_ServerClick" data-repeater-create="" class="btn font-weight-bold btn-primary">
                                                    <i class="la la-plus"></i>
                                                    <asp:Label ID="LblAddNewTier" Text="Add new tier" runat="server" />
                                                </a>
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

                        <div id="divCurrencyArea" visible="false" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-header">
                                        <h3 class="card-title">
                                            <asp:Label ID="LblCurrencyTitle" Text="Currency" runat="server" />
                                        </h3>
                                    </div>
                                    <div class="card-body">
                                        <div class="form-group row">
                                            <label class="col-lg-2 col-form-label text-right">Currency:</label>
                                            <div class="col-lg-3">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="DrpCurrency" Width="55" Height="36" runat="server" CssClass="form-control" />
                                                    <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                                </div>
                                                <span class="form-text text-muted"></span>
                                            </div>
                                            <div class="col-lg-2">
                                                <a id="aDeleteCurrency" onserverclick="aDeleteCurrency_ServerClick" visible="false" role="button" runat="server" data-repeater-delete="" class="btn font-weight-bold btn-danger btn-icon">
                                                    <i class="la la-remove"></i>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer">
                                        <div class="row">
                                            <div class="col-lg-3"></div>
                                            <div class="col-lg-6">
                                                <asp:Button ID="BtnSaveCurrency" OnClick="BtnSaveCurrency_Click" Text="Save" CssClass="btn font-weight-bold btn-primary btn-shadow mr-2" runat="server" />
                                            </div>
                                            <div class="col-lg-3"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <controls:MessageControl ID="UcMessageCurrencyAlertControl" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divTierPermissionsArea" visible="false" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-header">
                                        <h3 class="card-title">
                                            <asp:Label ID="LblPermissions" Text="Permissions" runat="server" />
                                        </h3>
                                    </div>
                                    <div class="card-body">
                                        <div class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                                            <telerik:RadGrid ID="RdgFormPermissions" BackColor="Transparent" HeaderStyle-BackColor="Transparent" HeaderStyle-BorderColor="Transparent" BorderColor="Transparent" AllowPaging="true" AllowSorting="false" PagerStyle-Position="Bottom" CssClass="table table-separate table-head-custom table-checkable" PageSize="10"
                                                OnItemDataBound="RdgFormPermissions_OnItemDataBound" OnNeedDataSource="RdgFormPermissions_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                                <MasterTableView CssClass="table-status">
                                                    <Columns>
                                                        <telerik:GridBoundColumn Display="false" DataField="form_id" UniqueName="form_id" />
                                                        <telerik:GridBoundColumn DataField="menu_name" UniqueName="menu_name" />
                                                        <telerik:GridBoundColumn Display="false" HeaderText="Page name" DataField="page_name" UniqueName="page_name" />
                                                        <telerik:GridBoundColumn Display="false" DataField="description1" UniqueName="description1" />
                                                        <telerik:GridBoundColumn Display="false" DataField="description2" UniqueName="description2" />
                                                        <telerik:GridBoundColumn Display="false" DataField="description3" UniqueName="description3" />
                                                        <telerik:GridBoundColumn Display="false" DataField="description4" UniqueName="description4" />
                                                        <telerik:GridBoundColumn Display="false" DataField="description5" UniqueName="description5" />
                                                        <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CbxSelectTier1" Checked="true" runat="server" />
                                                                <asp:Label ID="LblDescription1" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CbxSelectTier2" Checked="true" runat="server" />
                                                                <asp:Label ID="LblDescription2" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CbxSelectTier3" Checked="true" runat="server" />
                                                                <asp:Label ID="LblDescription3" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CbxSelectTier4" Checked="true" runat="server" />
                                                                <asp:Label ID="LblDescription4" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CbxSelectTier5" Checked="true" runat="server" />
                                                                <asp:Label ID="LblDescription5" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
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
                                        <asp:Label ID="LblMessageAlertfMsg" Text="Save these tiers first in order to add more" runat="server" />
                                        <asp:HiddenField ID="HdnTierID" Value="0" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnRemoveTier" Text="Proceed" OnClick="BtnRemoveTier_Click" Visible="false" CssClass="btn btn-primary" runat="server" />
                            <asp:Button ID="BtnRemoveCurrency" Text="Proceed" OnClick="BtnRemoveCurrency_Click" Visible="false" CssClass="btn btn-primary" runat="server" />
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
